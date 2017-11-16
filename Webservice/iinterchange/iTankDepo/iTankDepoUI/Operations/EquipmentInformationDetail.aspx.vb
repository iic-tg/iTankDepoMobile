
Partial Class Operations_EquipmentInformationDetail
    Inherits Framebase

#Region "Declarations"
    Dim dsEquipmentInformationData As New EquipmentInformationDataSet
    Dim dsGateInData As New GateinDataSet
    Dim ds As New DataSet
    Private Const GATE_IN As String = "GATE_IN"
    Private Const EQUIPMENT_INFORMATION As String = "EQUIPMENT_INFORMATION"
    Private strSize As String = ConfigurationSettings.AppSettings("UploadEquipmentnformationSize")
    Private strFileLength As String = ConfigurationSettings.AppSettings("UploadEquipmentnformationFileLength")

#End Region

#Region "Page Events"

    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/EquipmentInformationDetail.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/swfobject.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                flashUpload.UploadFileSizeLimit = strSize
                flashUpload.TotalUploadSizeLimit = strSize
                Dim intDepotId As Integer = 0
                Dim drEqInfo() As DataRow = Nothing
                Dim drRow As DataRow = Nothing
                Dim lngEqInfoId As Long = 0
                Dim dtEquipmentInformation As New DataTable
                Dim objCommonData As New CommonData
                Dim strEquipmentNo As String = String.Empty
                Dim objEquipmentInformation As New EquipmentInformation
                Dim strPageName As String = String.Empty
                Dim hfc As HttpFileCollection = Request.Files
                intDepotId = objCommonData.GetDepotID()
                If Not Request.QueryString("EquipmentNo") Is Nothing Then
                    CacheData("EquipmentNo", Request.QueryString("EquipmentNo").ToString)
                End If
                If Not RetrieveData("EquipmentNo") Is Nothing Then
                    strEquipmentNo = RetrieveData("EquipmentNo").ToString
                End If
                If Not Request.QueryString("EquipmentInformationId") Then
                    lngEqInfoId = CLng(Request.QueryString("EquipmentInformationId"))
                End If

                dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
                If lngEqInfoId <> 0 AndAlso Not (dsEquipmentInformationData) Is Nothing Then
                    dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).Rows.Clear()
                    drRow = dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).NewRow()
                    drRow.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = lngEqInfoId
                    drRow.Item(EquipmentInformationData.PAGENAME) = strPageName
                    hdnEquipmentInformationId.Value = drRow.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID)
                    dsEquipmentInformationData.Tables(GateinData._EQUIPMENT_INFO_DETAIL).Rows.Add(drRow)

                    'Get Files from Equipment Information Detail
                    If (IsNothing(dsEquipmentInformationData) OrElse (Not IsNothing(dsEquipmentInformationData) AndAlso (dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count = 0 OrElse dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count > 0 AndAlso dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, "=", lngEqInfoId)).Length = 0))) Then
                        If Not dsEquipmentInformationData Is Nothing Then
                            dtEquipmentInformation = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
                            drEqInfo = dtEquipmentInformation.Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, "=", lngEqInfoId))
                            If drEqInfo.Length = 0 Then
                                drEqInfo = dtEquipmentInformation.Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, "=", lngEqInfoId), "", DataViewRowState.Deleted)
                                If drEqInfo.Length = 0 Then
                                    drEqInfo = Nothing
                                End If
                            End If
                        End If
                        If drEqInfo Is Nothing Then
                            If lngEqInfoId > 0 Then
                                If dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL) Is Nothing Then
                                    'dtEquipmentInformation = objEquipmentInformation.pub_GetEquipmentInformationDetailByEqpID(lngEqInfoId).Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
                                    dtEquipmentInformation = objEquipmentInformation.pub_GetEquipmentInformationDetailByEqpNo(strEquipmentNo).Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
                                ElseIf dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count = 0 Then
                                    dtEquipmentInformation = objEquipmentInformation.pub_GetEquipmentInformationDetailByEqpNo(strEquipmentNo).Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
                                Else
                                    Dim drSelect As DataRow() = Nothing
                                    drSelect = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, " = ", lngEqInfoId))
                                    If drSelect.Length = 0 Then
                                        dtEquipmentInformation = objEquipmentInformation.pub_GetEquipmentInformationDetailByEqpID(lngEqInfoId).Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
                                    End If
                                    'Else
                                    '    dtEquipmentInformation = objEquipmentInformation.pub_GetEquipmentInformationDetailByEqpID(lngEqInfoId).Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
                                End If
                            End If
                        End If
                    End If
                    dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Merge(dtEquipmentInformation)
                    dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).Merge(dsEquipmentInformationData.Tables(GateinData._EQUIPMENT_INFO_DETAIL))
                    CacheData(EQUIPMENT_INFORMATION, dsEquipmentInformationData)
                    pvt_ShwoAttachment(strPageName)
                End If
                If Not (dsEquipmentInformationData) Is Nothing Then
                    pvt_fileUpload(hfc, strPageName, strEquipmentNo)
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "deletePhoto"
                    deletePhoto(e.GetCallbackValue("EquipmentInformationDetailId"), _
                                e.GetCallbackValue("EquipmentInformationId"), _
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

#Region "btnUpload_Click()"
    Protected Sub btnUpload_Click(sender As Object, e As System.EventArgs) Handles btnUpload.Click
        Try
            Dim hfc As HttpFileCollection = Request.Files
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            pvt_fileUpload(hfc, Request.QueryString("PageName"), Request.QueryString("EquipmentNo"))

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_fileUpload()"
    Private Sub pvt_fileUpload(ByVal hfc As HttpFileCollection, _
                               ByVal bv_strPageName As String, _
                               ByVal bv_strEquipmentNo As String)
        Try
            Dim strFilename As String = String.Empty
            Dim strExtension As String = String.Empty
            Dim strClientFileName As String = String.Empty
            Dim strPageName As String = String.Empty
            Dim strAppDomainAppPath As String = System.Web.HttpRuntime.AppDomainAppPath()
            Dim strVirtualPath As String = Config.pub_GetAppConfigValue("UploadEquipmentnformation")
            Dim strEquipmentInformationId As String = String.Empty
            Dim lngFileSize As Long = 0
            Dim uploadFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString
            Dim drAttachment As DataRow = Nothing
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            If dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).Rows.Count > 0 Then
                strEquipmentInformationId = dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).Rows(0).Item(EquipmentInformationData.EQPMNT_INFRMTN_ID)
            End If
            For i As Integer = 0 To hfc.Count - 1
                Dim hpf As HttpPostedFile = hfc(i)
                If hpf.ContentLength > 0 Then
                    strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
                    strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
                    strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                    strVirtualPath = String.Concat(strVirtualPath, strFilename, strExtension)
                    'Create New Row for Attachment
                    drAttachment = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).NewRow()
                    drAttachment(EquipmentInformationData.EQPMNT_INFRMTN_DTL_ID) = CommonWeb.GetNextIndex(dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL), EquipmentInformationData.EQPMNT_INFRMTN_DTL_ID)
                    drAttachment(EquipmentInformationData.EQPMNT_INFRMTN_ID) = strEquipmentInformationId
                    drAttachment(EquipmentInformationData.EQPMNT_NO) = bv_strEquipmentNo
                    Dim regExpMatch As Match = System.Text.RegularExpressions.Regex.Match(strFilename, "^[a-zA-Z0-9-_.&, ]+$")
                    If regExpMatch.Success Then
                        lngFileSize = hpf.ContentLength
                        If lngFileSize < CLng(strSize) AndAlso strClientFileName.Length < strFileLength Then
                            If Not System.IO.Directory.Exists(String.Concat(uploadFolder, "\UpLoad\EquipmentInformation")) Then
                                System.IO.Directory.CreateDirectory(String.Concat(uploadFolder, "\UpLoad\EquipmentInformation"))
                            End If
                            hpf.SaveAs(strVirtualPath)
                            drAttachment(EquipmentInformationData.ATTCHMNT_PTH) = String.Concat(String.Concat(strFilename, strExtension))
                            drAttachment(EquipmentInformationData.ACTL_FL_NM) = strClientFileName
                            drAttachment(RepairEstimateData.ERR_FLG) = False
                        Else
                            drAttachment(EquipmentInformationData.ERR_FLG) = True
                            drAttachment(EquipmentInformationData.ERR_SMMRY) = String.Concat("Attachment File Name (", strClientFileName, " must be below ", strFileLength, "Characters.")
                        End If
                    Else
                        drAttachment(EquipmentInformationData.ERR_FLG) = True
                        drAttachment(EquipmentInformationData.ERR_SMMRY) = String.Concat("File Name (", strClientFileName, ") with special characters are not allowed. ")
                    End If
                    dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Add(drAttachment)
                End If
            Next
            CacheData(EQUIPMENT_INFORMATION, dsEquipmentInformationData)
            If dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).Rows.Count > 0 Then
                strPageName = dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).Rows(0).Item(EquipmentInformationData.PAGENAME).ToString
            End If
            If dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count = 0 Then
                Page.ClientScript.RegisterStartupScript(GetType(String), "", " el('divNoImageUpload').style.display = 'block';", True)
            End If
            pvt_ShwoAttachment(strPageName)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ShwoAttachment()"
    Private Sub pvt_ShwoAttachment(ByVal bv_strPageName As String)
        Try
            Dim sbAttachment As New StringBuilder
            Dim ulTag As New HtmlGenericControl
            Dim strEquipmentInformationId As String = String.Empty
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            If dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).Rows.Count > 0 Then
                strEquipmentInformationId = dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).Rows(0).Item(EquipmentInformationData.EQPMNT_INFRMTN_ID)
            End If
           

            If dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count > 0 Then
                Dim strPhysicalpath As String = String.Empty
                Dim strFileName As String = String.Empty
                Dim strAppDomainAppPath As String = System.Web.HttpRuntime.AppDomainAppPath()
                Dim strPath As String = Config.pub_GetAppConfigValue("DownloadEquipmentInformation")
                ulTag = CType(photoList, HtmlGenericControl)
                For Each dr As DataRow In dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, "=", strEquipmentInformationId, " AND ", RepairEstimateData.ERR_FLG, " <> ", "'True'"))
                    strPhysicalpath = dr.Item(EquipmentInformationData.ATTCHMNT_PTH).ToString
                    strFileName = dr.Item(EquipmentInformationData.ACTL_FL_NM).ToString
                    sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(EquipmentInformationData.EQPMNT_INFRMTN_DTL_ID)) + "','" + CStr(dr.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                    sbAttachment.Append("<a href=""" + String.Concat(strPath, strPhysicalpath) + """ target=""_blank"" ><span  class=""MultiFile-title"">" + strFileName + "</span></a>")
                    sbAttachment.Append("<br />")
                    ulTag.InnerHtml = sbAttachment.ToString()
                Next
                CacheData("PhotoLists", sbAttachment)
            Else
                sbAttachment = sbAttachment.Append("")
                ulTag.InnerHtml = sbAttachment.ToString()
                CacheData("PhotoLists", sbAttachment)
            End If
            If sbAttachment.Length = 0 Then
                Page.ClientScript.RegisterStartupScript(GetType(String), "", " el('divNoImageUpload').style.display = 'block';", True)
            End If
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
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            If Not (dsEquipmentInformationData) Is Nothing Then
                For Each drErrorRow As DataRow In dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Select(String.Concat(EquipmentInformationData.ERR_FLG, " = ", "True"))
                    If sbrError.Length > 0 Then
                        sbrError.Append(" , ")
                    End If
                    sbrError.Append(drErrorRow.Item(EquipmentInformationData.ERR_SMMRY))
                    drErrorRow.Delete()
                Next
                If sbrError.Length > 0 Then
                    pub_SetCallbackReturnValue("errorSummary", sbrError.ToString)
                End If
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("errorSummary", "") 'Added by Sakthivel on 15-OCT-2014 for Loaded an Important Error message in Chrome
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "deletePhoto"
    Private Sub deletePhoto(ByVal bv_strEquipmentInformationDetailId As String, ByVal bv_strEquipmentInformationId As String, ByVal bv_strPageName As String)
        Try
            Dim intCount As Integer = 0
            Dim drSelect As DataRow() = Nothing
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            Dim dr() As DataRow
            If Not IsNothing(dsEquipmentInformationData) Then
                dr = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_DTL_ID, "= ", bv_strEquipmentInformationDetailId))
                If dr.Length > 0 Then
                    dr(0).Delete()
                End If

                If dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows.Count > 0 Then
                    drSelect = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, " = ", bv_strEquipmentInformationId))
                    If drSelect.Length > 0 Then
                        intCount = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Compute(String.Concat("COUNT(", EquipmentInformationData.EQPMNT_INFRMTN_ID, ")"), String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, " = ", bv_strEquipmentInformationId))
                        drSelect(0).Item(EquipmentInformationData.COUNT_ATTACH) = intCount
                    End If
                End If
                CacheData(EQUIPMENT_INFORMATION, dsEquipmentInformationData)
                Dim sb As New StringBuilder
                Dim strPageName As String = String.Empty
                If dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).Rows.Count > 0 Then
                    strPageName = dsEquipmentInformationData.Tables(EquipmentInformationData._EQUIPMENT_INFO_DETAIL).Rows(0).Item(EquipmentInformationData.PAGENAME).ToString
                End If
                pvt_ShwoAttachment(strPageName)
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
