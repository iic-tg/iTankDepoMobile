Partial Class Operations_PhotoUpload
    Inherits Framebase

    Dim dsRepairEstimate As New RepairEstimateDataSet
    Dim dsRepairCompletion As New RepairCompletionDataSet
    Dim dsPreAdvice As New PreAdviceDataSet
    Dim ds As DataSet
    Private Const REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"
    Private Const REPAIR_COMPLETION As String = "REPAIR_COMPLETION"
    Private Const COMMON As String = "COMMON"
    Private Const PREADVICE As String = "PREADVICE"
    Private Const GATE_IN As String = "GATE_IN"
    Private Const GATE_OUT As String = "GATE_OUT"
    Dim dsGateIn As DataSet
    Dim sbrInValid As New StringBuilder
    Private strSize As String = ConfigurationSettings.AppSettings("UploadPhotoSize")
    Private strPhotoLength As String = ConfigurationSettings.AppSettings("UploadPhotoFileLength")
    Dim strFileName As String
    Dim DeleteFlag As Boolean = False


#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/PhotoUpload.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/swfobject.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                flashUpload.UploadFileSizeLimit = strSize
                flashUpload.TotalUploadSizeLimit = strSize
                Dim strPageName As String = ""
                Dim hfc As HttpFileCollection = Request.Files
                If Not Request.QueryString("PageName") Is Nothing Then
                    CacheData("PageName", Request.QueryString("PageName").ToString)
                End If
                Dim strActivityName As String

                If Not RetrieveData("PageName") Is Nothing Then
                    strPageName = RetrieveData("PageName").ToString
                End If
                If Not Request.QueryString("RepairEstimateId") Is Nothing Then
                    CacheData("RepairEstimateId", Request.QueryString("RepairEstimateId").ToString)
                    hdnRepairEstimteId.Value = RetrieveData("RepairEstimateId").ToString
                    Dim dtAttachment As New DataTable
                    Dim intDepotId As Integer
                    Dim objCommonData As New CommonData
                    Dim objRepairEstimate As New RepairEstimate
                    strActivityName = strPageName
                    If strPageName = "Repair Completion" Then
                        ds = CType(RetrieveData(REPAIR_COMPLETION), RepairCompletionDataSet)
                        If Request.QueryString("SubName") = "Repair Approval" Then
                            strActivityName = "Repair Approval"
                        Else
                            strActivityName = "Repair Completion"

                        End If

                    ElseIf strPageName = "Pre-Advice" Then
                        ds = CType(pub_RetrieveData(PREADVICE), PreAdviceDataSet)
                    ElseIf strPageName = "GateIn" Then
                        ds = New DataSet
                        ds = CType(pub_RetrieveData(GATE_IN), GateinDataSet)
                        If Request.QueryString("SubName") = "Pre-Advice" Then
                            strActivityName = "Pre-Advice"
                        Else
                            strActivityName = strPageName
                        End If
                       
                    ElseIf strPageName = "GateOut" Then
                        ds = CType(RetrieveData(GATE_OUT), GateOutDataSet)
                        If Request.QueryString("SubName") = "GateIn" Then
                            strActivityName = "GateIn"
                        Else
                            strActivityName = strPageName
                        End If
                      
                    Else
                        ds = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
                        If Request.QueryString("SubName") = "Repair Estimate" Then
                            strActivityName = "Repair Estimate"
                        Else
                            strActivityName = "Repair Approval"

                        End If
                    End If
                    'Check Delete operation
                    If DeleteFlag = False Then
                        CacheData("DeleteFlag", Nothing)
                    End If
                    RetrieveData("DeleteFlag")

                    intDepotId = objCommonData.GetDepotID()
                    '  If DeleteFlag = False Then
                    If RetrieveData("DeleteFlag") <> True Then
                        If Not Request.QueryString("RepairEstimateId") Is Nothing AndAlso Not Request.QueryString("RepairEstimateId") = "" AndAlso (IsNothing(ds) _
                            OrElse (Not IsNothing(ds) AndAlso (ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Count = 0 OrElse ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Count > 0 AndAlso ds.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", CommonWeb.iLng(Request.QueryString("RepairEstimateId")))).Length = 0))) Then
                            dtAttachment = objRepairEstimate.Pub_GetAttachmentByRepairEstimateId(intDepotId, strActivityName, CommonWeb.iLng(Request.QueryString("RepairEstimateId"))).Tables(RepairEstimateData._ATTACHMENT)
                        End If
                    End If
                    If dtAttachment.Rows.Count > 0 Then
                        If strPageName <> "Repair Completion" AndAlso strPageName <> "Pre-Advice" AndAlso strPageName <> "GateIn" AndAlso strPageName <> "GateOut" Then
                            ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Clear()
                        End If

                        '   drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(ds.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)

                        If strPageName = "GateIn" Or strPageName = "GateOut" Then

                            For Each dr As DataRow In dtAttachment.Rows

                                Dim newRow As DataRow = ds.Tables(RepairEstimateData._ATTACHMENT).NewRow()
                                '  Dim maxSno As Int64 = FindMaxDataTableValue(ds.Tables(RepairEstimateData._ATTACHMENT))
                                Dim maxSno As Int64
                                If ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Count = 0 AndAlso dtAttachment.Rows.Count > 0 Then
                                    maxSno = dtAttachment.Compute("Max(ATTCHMNT_ID)", "") + 1
                                ElseIf ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Count > 0 Then
                                    maxSno = ds.Tables(RepairEstimateData._ATTACHMENT).Compute("Max(ATTCHMNT_ID)", "") + 1
                                Else
                                    maxSno = 1
                                End If

                                newRow.Item(RepairEstimateData.ATTCHMNT_ID) = maxSno
                                newRow.Item(RepairEstimateData.RPR_ESTMT_ID) = dr.Item(RepairEstimateData.RPR_ESTMT_ID)

                                newRow.Item(RepairEstimateData.RPR_ESTMT_ID) = dr.Item(RepairEstimateData.RPR_ESTMT_ID)
                                newRow.Item(RepairEstimateData.ACTVTY_NAM) = dr.Item(RepairEstimateData.ACTVTY_NAM)


                                newRow.Item(RepairEstimateData.RPR_ESTMT_NO) = dr.Item(RepairEstimateData.RPR_ESTMT_NO)
                                newRow.Item(RepairEstimateData.GI_TRNSCTN_NO) = dr.Item(RepairEstimateData.GI_TRNSCTN_NO)

                                newRow.Item(RepairEstimateData.ATTCHMNT_PTH) = dr.Item(RepairEstimateData.ATTCHMNT_PTH)
                                newRow.Item(RepairEstimateData.ACTL_FL_NM) = dr.Item(RepairEstimateData.ACTL_FL_NM)

                                newRow.Item(RepairEstimateData.MDFD_BY) = dr.Item(RepairEstimateData.MDFD_BY)
                                newRow.Item(RepairEstimateData.MDFD_DT) = dr.Item(RepairEstimateData.MDFD_DT)

                                newRow.Item(RepairEstimateData.DPT_ID) = dr.Item(RepairEstimateData.DPT_ID)
                                newRow.Item(RepairEstimateData.CHK_FLG) = dr.Item(RepairEstimateData.CHK_FLG)
                                newRow.Item(RepairEstimateData.ERR_FLG) = dr.Item(RepairEstimateData.ERR_FLG)
                                newRow.Item(RepairEstimateData.ERR_SMMRY) = dr.Item(RepairEstimateData.ERR_SMMRY)

                                ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(newRow)
                            Next

                        Else
                            ds.Tables(RepairEstimateData._ATTACHMENT).Merge(dtAttachment)
                        End If


                    End If
                    CheckAttachment(RetrieveData("RepairEstimateId").ToString, strPageName)
                End If
                pvt_fileUpload(hfc, strPageName)
                CacheData("DeleteFlag", Nothing)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Function FindMaxDataTableValue(ByRef dt As DataTable) As Integer
        Dim currentValue As Integer, maxValue As Integer
        Dim dv As DataView = dt.DefaultView
        For c As Integer = 0 To dt.Columns.Count - 1
            dv.Sort = dt.Columns(c).ColumnName + " DESC"
            currentValue = CInt(dv(0).Item(c))
            If currentValue > maxValue Then maxValue = currentValue
        Next
        Return maxValue
    End Function
#End Region

#Region "pvt_fileUpload"
    Private Sub pvt_fileUpload(ByVal hfc As HttpFileCollection, _
                               ByVal bv_strPageName As String)
        Try
            Dim objCommon As New CommonUI
            Dim objCommonData As New CommonData
            Dim objRepairEstimate As New RepairEstimate
            Dim intDepotId As Integer
            Dim strModifiedBy As String
            Dim strVirtualPath As String = ""
            Dim uploadFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString
            Dim drAttachment As DataRow
            Dim strFilename As String = ""
            Dim strExtension As String = ""
            Dim strClientFileName As String = ""
            intDepotId = objCommonData.GetDepotID()
            strModifiedBy = objCommonData.GetCurrentUserName()
            Dim strRepairEstimateId As String = RetrieveData("RepairEstimateId").ToString
            Dim lngMaxSize As Long = CLng(strSize)
            lngMaxSize = lngMaxSize / 1024000

            ' Get the HttpFileCollection
            If bv_strPageName = "Repair Completion" Then
                ds = CType(RetrieveData(REPAIR_COMPLETION), RepairCompletionDataSet)
            ElseIf bv_strPageName = "Pre-Advice" Then
                ds = CType(RetrieveData(PREADVICE), PreAdviceDataSet)
            ElseIf bv_strPageName = "GateIn" Then
                ds = CType(RetrieveData(GATE_IN), GateinDataSet)
            ElseIf bv_strPageName = "GateOut" Then
                ds = CType(RetrieveData(GATE_OUT), GateOutDataSet)
            Else
                ds = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            End If
            '   Dim hfc As HttpFileCollection = Request.Files
            For i As Integer = 0 To hfc.Count - 1
                Dim hpf As HttpPostedFile = hfc(i)
                If hpf.ContentLength > 0 Then
                    Dim lngFileSize As Long
                    Dim sbPath As New StringBuilder
                    strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
                    ' strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
                    strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                    lngFileSize = hpf.ContentLength

                    drAttachment = ds.Tables(RepairEstimateData._ATTACHMENT).NewRow()
                    drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(ds.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)
                    If Not IsDBNull(strRepairEstimateId) AndAlso strRepairEstimateId <> String.Empty AndAlso strRepairEstimateId <> " " Then
                        drAttachment(RepairEstimateData.RPR_ESTMT_ID) = strRepairEstimateId
                    End If

                    Dim myMatch As Match = System.Text.RegularExpressions.Regex.Match(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), "^[a-zA-Z0-9 _.-]*$")
                    If myMatch.Success Then
                        strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName).Replace("+", ""), Now.ToFileTime)
                        strFilename = String.Concat(strFilename, strExtension)
                        strVirtualPath = String.Concat(Config.pub_GetAppConfigValue("UploadAttachPath"), strFilename)
                        strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                        lngFileSize = hpf.ContentLength
                        If strClientFileName.Length < strPhotoLength Then
                            If Not System.IO.Directory.Exists(String.Concat(uploadFolder, "\UpLoad\Attachment")) Then
                                System.IO.Directory.CreateDirectory(String.Concat(uploadFolder, "\UpLoad\Attachment"))
                            End If
                            hpf.SaveAs(strVirtualPath)
                            drAttachment(RepairEstimateData.ATTCHMNT_PTH) = strFilename
                            drAttachment(RepairEstimateData.ACTL_FL_NM) = strClientFileName
                            drAttachment(RepairEstimateData.MDFD_BY) = strModifiedBy
                            drAttachment(RepairEstimateData.MDFD_DT) = CDate(Now)
                            drAttachment(RepairEstimateData.DPT_ID) = intDepotId
                            drAttachment(RepairEstimateData.ERR_FLG) = False
                        Else
                            drAttachment(RepairEstimateData.ERR_FLG) = True
                            drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("Attachment File Name (", strClientFileName, " must be below ", strPhotoLength, "Characters.")
                        End If
                    Else
                        drAttachment(RepairEstimateData.ERR_FLG) = True
                        drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("File Name (", strClientFileName, ") with special characters are not allowed. ")
                    End If
                    ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(drAttachment)
                    If ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Count = 0 Then
                        Page.ClientScript.RegisterStartupScript(GetType(String), "", " el('divNoImageUpload').style.display = 'block';", True)
                    End If
                    If bv_strPageName = "GateIn" Then
                        CacheData(GATE_IN, ds)
                    End If

                End If
            Next
            CacheData(COMMON, ds)
            CheckAttachment(strRepairEstimateId, Request.QueryString("PageName"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "deletePhoto"
                    deletePhoto(e.GetCallbackValue("AttachmentId"), _
                                e.GetCallbackValue("RepairEstimateId"), _
                                e.GetCallbackValue("PageName"))
                Case "getErrorSummary"
                    pvt_getErrorSummary(e.GetCallbackValue("PageName"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_getErrorSummary()"
    Private Sub pvt_getErrorSummary(ByVal bv_strPageName As String)
        Try
            Dim sbrError As New StringBuilder
            If bv_strPageName.Replace("+", " ") = "Repair Completion" Then
                ds = CType(RetrieveData(REPAIR_COMPLETION), RepairCompletionDataSet)
            ElseIf bv_strPageName = "Pre-Advice" Then
                ds = CType(RetrieveData(PREADVICE), PreAdviceDataSet)
            ElseIf bv_strPageName = "GateIn" Then
                ds = CType(RetrieveData(GATE_IN), GateinDataSet)
            ElseIf bv_strPageName = "GateOut" Then
                ds = CType(RetrieveData(GATE_OUT), GateOutDataSet)
            Else
                ds = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            End If
            For Each drErrorRow As DataRow In ds.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.ERR_FLG, " = ", "True"))
                If sbrError.Length > 0 Then
                    sbrError.Append(" , ")
                End If
                sbrError.Append(drErrorRow.Item(RepairEstimateData.ERR_SMMRY))
                drErrorRow.Delete()
            Next
            If sbrError.Length > 0 Then
                pub_SetCallbackReturnValue("errorSummary", sbrError.ToString)
            Else
                'pub_SetCallbackReturnValue("errorSummary", "")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "CheckAttachment"
    Private Sub CheckAttachment(ByVal bv_strRepairEstimateId As String, ByVal bv_strPageName As String)
        Try
            Dim sbAttachment As New StringBuilder

            Dim strPhysicalpath As String = ""
            Dim strFileName As String = ""
            Dim json As String = ""
            Dim strPath As String = Config.pub_GetAppConfigValue("DownloadAttachPath")
            If Not bv_strPageName Is Nothing Then
                If bv_strPageName.Replace("+", " ") = "Repair Completion" Then
                    ds = CType(RetrieveData(REPAIR_COMPLETION), RepairCompletionDataSet)
                ElseIf bv_strPageName = "Pre-Advice" Then
                    ds = CType(RetrieveData(PREADVICE), PreAdviceDataSet)
                ElseIf bv_strPageName = "GateIn" Then
                    ds = CType(RetrieveData(GATE_IN), GateinDataSet)
                ElseIf bv_strPageName = "GateOut" Then
                    ds = CType(RetrieveData(GATE_OUT), GateOutDataSet)
                Else
                    ds = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
                End If
                Dim ulTag As New HtmlGenericControl
                ulTag = CType(photoList, HtmlGenericControl)
                If ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Count > 0 Then
                    If bv_strPageName = "Repair Completion" Or bv_strPageName = "Pre-Advice" Or bv_strPageName = "GateIn" Or bv_strPageName = "GateOut" Then
                        For Each dr As DataRow In ds.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", bv_strRepairEstimateId, " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))
                            strPhysicalpath = dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString
                            strFileName = dr.Item(RepairEstimateData.ACTL_FL_NM).ToString
                            sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(RepairEstimateData.ATTCHMNT_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                            'sbAttachment.Append(String.Concat("<a target='_blank' href='../download.ashx?FL_NM=", strFileName, "'  title='Click to download error file' >", strFileName, "</a>"))
                            sbAttachment.Append(String.Concat("<a target='_blank' href='//", strPath, "?FL_NM=", strPhysicalpath, "'  title='Click to download file' >", strFileName, "</a>"))
                            sbAttachment.Append("<br />")
                        Next
                        ulTag.InnerHtml = sbAttachment.ToString()
                     
                    Else
                        For Each dr As DataRow In ds.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.ERR_FLG, " <> ", "True"))
                            strPhysicalpath = dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString
                            strFileName = dr.Item(RepairEstimateData.ACTL_FL_NM).ToString
                            sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(RepairEstimateData.ATTCHMNT_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                            'sbAttachment.Append("<a href=""" + String.Concat(strPath, strPhysicalpath) + """  target=""_blank""><span  class=""MultiFile-title"">" + strFileName + "</span></a>")
                            'sbAttachment.Append(String.Concat("<a target='_blank' href='//icpu0089/PhotoViewer/Download.ashx?FL_NM=", strFileName, "'  title='Click to download file' >", strFileName, "</a>"))
                            sbAttachment.Append(String.Concat("<a target='_blank' href='//", strPath, "?FL_NM=", strPhysicalpath, "'  title='Click to download file' >", strFileName, "</a>"))
                            sbAttachment.Append("<br />")
                            ulTag.InnerHtml = sbAttachment.ToString()
                        Next
                    End If
                    CacheData("PhotoLists", sbAttachment)
                Else
                    sbAttachment = sbAttachment.Append("")
                    ulTag.InnerHtml = sbAttachment.ToString()
                    CacheData("PhotoLists", sbAttachment)
                End If
                If sbAttachment.Length = 0 Then
                    Page.ClientScript.RegisterStartupScript(GetType(String), "", " el('divNoImageUpload').style.display = 'block';", True)
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "btnUpload_Click"
    Protected Sub btnUpload_Click(sender As Object, e As System.EventArgs) Handles btnUpload.Click
        Try
            Dim objCommon As New CommonUI
            Dim objCommonData As New CommonData
            Dim objRepairEstimate As New RepairEstimate
            Dim intDepotId As Integer
            Dim strModifiedBy As String
            Dim strVirtualPath As String = ""
            Dim uploadFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString
            Dim drAttachment As DataRow
            Dim strFilename As String = ""
            Dim strExtension As String = ""
            Dim strClientFileName As String = ""
            intDepotId = objCommonData.GetDepotID()
            strModifiedBy = objCommonData.GetCurrentUserName()
            Dim strRepairEstimateId As String = RetrieveData("RepairEstimateId").ToString
            Dim lngMaxSize As Long = CLng(strSize)
            lngMaxSize = lngMaxSize / 1024000
            ' Get the HttpFileCollection
            If Request.QueryString("PageName") = "Repair Completion" Then
                ds = CType(RetrieveData(REPAIR_COMPLETION), RepairCompletionDataSet)
            ElseIf Request.QueryString("PageName") = "Pre-Advice" Then
                ds = CType(RetrieveData(PREADVICE), PreAdviceDataSet)
            ElseIf Request.QueryString("PageName") = "GateIn" Then
                ds = CType(RetrieveData(GATE_IN), GateinDataSet)
            ElseIf Request.QueryString("PageName") = "GateOut" Then
                ds = CType(RetrieveData(GATE_OUT), GateOutDataSet)
            Else
                ds = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            End If
            Dim hfc As HttpFileCollection = Request.Files
            For i As Integer = 0 To hfc.Count - 1
                Dim hpf As HttpPostedFile = hfc(i)
                If hpf.ContentLength > 0 Then
                    Dim lngFileSize As Long
                    strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
                    strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
                    strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                    strVirtualPath = String.Concat(Config.pub_GetAppConfigValue("UploadAttachPath"), strFilename, strExtension)
                    lngFileSize = hpf.ContentLength
                    drAttachment = ds.Tables(RepairEstimateData._ATTACHMENT).NewRow()
                    drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(ds.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)
                    If Not IsDBNull(strRepairEstimateId) AndAlso strRepairEstimateId <> String.Empty AndAlso strRepairEstimateId <> " " Then
                        drAttachment(RepairEstimateData.RPR_ESTMT_ID) = strRepairEstimateId
                    End If
                    Dim myMatch As Match = System.Text.RegularExpressions.Regex.Match(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), "^[a-zA-Z0-9-_.&, ]+$")
                    If myMatch.Success Then
                        strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName).Replace("+", ""), Now.ToFileTime)
                        strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                        lngFileSize = hpf.ContentLength

                        If lngFileSize < CLng(strSize) Then
                            If strClientFileName.Length < strPhotoLength Then
                                If Not System.IO.Directory.Exists(String.Concat(uploadFolder, "\UpLoad\Attachment")) Then
                                    System.IO.Directory.CreateDirectory(String.Concat(uploadFolder, "\UpLoad\Attachment"))
                                End If
                                hpf.SaveAs(strVirtualPath)
                                drAttachment(RepairEstimateData.ATTCHMNT_PTH) = String.Concat(String.Concat(strFilename, strExtension))
                                drAttachment(RepairEstimateData.ACTL_FL_NM) = strClientFileName
                                drAttachment(RepairEstimateData.MDFD_BY) = strModifiedBy
                                drAttachment(RepairEstimateData.MDFD_DT) = CDate(Now)
                                drAttachment(RepairEstimateData.DPT_ID) = intDepotId

                            Else
                                drAttachment(RepairEstimateData.ERR_FLG) = True
                                drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("Attachment File Name (", strClientFileName, " must be below ", strPhotoLength, "Characters.")
                            End If
                        End If
                    Else
                        drAttachment(RepairEstimateData.ERR_FLG) = True
                        drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("File Name (", strClientFileName, ") with special characters are not allowed. ")
                    End If
                    ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(drAttachment)
                    If ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Count = 0 Then
                        Page.ClientScript.RegisterStartupScript(GetType(String), "", " el('divNoImageUpload').style.display = 'block';", True)
                    End If
                End If
            Next
            CacheData(COMMON, ds)
            CheckAttachment(strRepairEstimateId, Request.QueryString("PageName"))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "deletePhoto"
    Private Sub deletePhoto(ByVal bv_strAttachmentId As String, ByVal bv_strRepairEstimateId As String, ByVal bv_strPageName As String)
        Try
            If bv_strPageName.Replace("+", " ") = "Repair Completion" Then
                ds = CType(RetrieveData(REPAIR_COMPLETION), RepairCompletionDataSet)
            ElseIf bv_strPageName = "Pre-Advice" Then
                ds = CType(RetrieveData(PREADVICE), PreAdviceDataSet)
            ElseIf bv_strPageName = "GateIn" Then
                ds = CType(RetrieveData(GATE_IN), GateinDataSet)
            ElseIf bv_strPageName = "GateOut" Then
                ds = CType(RetrieveData(GATE_OUT), GateOutDataSet)
            Else
                ds = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            End If
            Dim dr() As DataRow
            If Not IsNothing(ds) Then
                dr = ds.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.ATTCHMNT_ID, "= ", bv_strAttachmentId))
                If dr.Length > 0 Then
                    dr(0).Delete()
                    'Know Delete operation occur
                    CacheData("DeleteFlag", True)
                    DeleteFlag = True
                End If
                'ds.AcceptChanges()

                If bv_strPageName.Replace("+", " ") = "Repair Completion" Then
                    CacheData(REPAIR_COMPLETION, ds)
                ElseIf bv_strPageName = "Pre-Advice" Then
                    CacheData(PREADVICE, ds)
                ElseIf bv_strPageName = "GateIn" Then
                    CacheData(GATE_IN, ds)
                ElseIf bv_strPageName = "GateOut" Then
                    CacheData(GATE_OUT, ds)
                Else
                    CacheData(REPAIR_ESTIMATE, ds)
                End If
                'CacheData(REPAIR_COMPLETION, ds)
                'CacheData(REPAIR_ESTIMATE, ds)
                'CacheData(PREADVICE, ds)
                'CacheData(GATE_IN, ds)
                'CacheData(GATE_OUT, ds)
                Dim sb As New StringBuilder
                CheckAttachment(bv_strRepairEstimateId, bv_strPageName.Replace("+", " "))
                sb = CType(RetrieveData("PhotoLists"), StringBuilder)
                pub_SetCallbackStatus(True)
                pub_SetCallbackReturnValue("fileList", sb.ToString())
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region



End Class
