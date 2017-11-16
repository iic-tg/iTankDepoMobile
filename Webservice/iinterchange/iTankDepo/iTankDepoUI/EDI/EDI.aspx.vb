Imports System.IO

Partial Class EDI_EDI
    Inherits Pagebase
#Region "Declarations.."
    Dim dsEDI As New EDIDataSet
    Dim dtFolder As New DataTable()
    Dim strGenerationMode As String = "Manual"
    Dim strGetFolderName() As String
    Dim strDownloadFileName As String
    Dim strFolderName As String
    Dim strFileName As String
    Dim strFileFolder As String = UCase(ConfigurationManager.AppSettings("OutputFolder"))
    Private Const EDI As String = "EDI"
    Dim objISO As New Customers
    Dim strCustomerID As Int64
    Dim dsCustomer As New CustomerDataSet
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                ' navEquipmentInfo.Visible = False
                strDownloadFileName = Request.QueryString("Customer")
                strCustomerID = Request.QueryString("CustomerID")
                If strDownloadFileName Is Nothing Then
                Else
                    fileDownload()
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEDI_ClientBind"
    Protected Sub ifgEDI_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEdi.ClientBind
        Try
            Dim objEnquiry As New EDI
            Dim objCommon As New CommonData
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            Dim strCustomer As String = e.Parameters("Customer")
            Dim strGateOut As String = e.Parameters("GateOut")
            Dim strGateIn As String = e.Parameters("GateIn")
            Dim strRepairEstimate As String = e.Parameters("chkRepairEstimate")
            Dim strRepairCompletion As String = e.Parameters("chkRepairApproval")
            dsEDI = objEnquiry.pub_GetEDI_Details(intDepotID)
            e.DataSource = dsEDI.Tables(EDIData._EDI)
            CacheData(EDI, dsEDI)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region


#Region "FileExists"
    Private Function FileExists(ByVal FileFullPath As String) _
   As Boolean
        If Trim(FileFullPath) = "" Then Return False
        Dim f As New IO.FileInfo(FileFullPath)
        Return f.Exists
    End Function
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/EDI/EDI.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)

            CommonWeb.IncludeScript("Script/UI/jquery.ui.js", MyBase.Page)
            '  CommonWeb.IncludeScript("Script/EDI/EDIEmail.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "GenerateEDI"
                If e.GetCallbackValue("FileFormat") = "ANSII" Then
                    pvt_GenerateEDI(e.GetCallbackValue("GateIn"), e.GetCallbackValue("GateOut"), e.GetCallbackValue("RepairComplete"), e.GetCallbackValue("RepairEstimate"), e.GetCallbackValue("WfData"), e.GetCallbackValue("Customer"), e.GetCallbackValue("CustomerID"), strGenerationMode)
                Else
                    pvt_GenerateEDICodeco(e.GetCallbackValue("GateIn"), e.GetCallbackValue("GateOut"), e.GetCallbackValue("RepairComplete"), e.GetCallbackValue("RepairEstimate"), e.GetCallbackValue("WfData"), e.GetCallbackValue("Customer"), e.GetCallbackValue("CustomerID"), strGenerationMode)
                End If

        End Select
    End Sub
#End Region
#Region "pvt_CreateEDI"
    Protected Sub pvt_CreateEDI(ByVal bv_Customer_cd As String, _
        ByVal bv_strActivity As String, _
        ByVal bv_strGenarated As DateTime, _
        ByVal bv_strRsnBit As String, _
        ByVal bv_strAscFileName As String, _
        ByVal bv_strFileformat As String, _
        ByVal bv_lngCstmr_id As Int64, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_strCreatedDt As DateTime, _
        ByVal bv_strwfData As String)
        Dim objEDI As New EDI
        Try
            Dim lng As Long = objEDI.pub_CreateEDI(bv_Customer_cd, bv_strActivity, _
                                                       bv_strGenarated, bv_strRsnBit, _
                                                       bv_strAscFileName, bv_strFileformat, _
                                                       bv_strCreatedBy, _
                                                       bv_strCreatedDt, bv_lngCstmr_id, bv_strwfData)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            '    pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub
#End Region

#Region "pvt_GenerateEDI"
    Private Sub pvt_GenerateEDI(ByVal bv_blnGateIn As Boolean, ByVal bv_blnGateOut As Boolean, ByVal bv_blnRepairComplete As Boolean, ByVal bv_blnRepairEstimate As Boolean, ByVal strWfdata As String, ByVal bv_Customer_cd As String, ByVal bv_CustomerID As Long, ByVal bv_strGenerationMode As String)

        Dim strDepotCode As String = ""
        Dim sbMsg As New System.Text.StringBuilder
        Dim by_strTimeStamp As String = String.Empty
        Dim objcommon As New CommonData
        Dim bv_Customer_Orginal As String
      
        Try
            Dim objEDI As New EDI
            bv_Customer_Orginal = bv_Customer_cd
            'for Passing EDI code
            dsCustomer = objISO.getISOCODEbyCustomer(CLng(bv_CustomerID))
            If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                bv_Customer_cd = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
            Else
                bv_Customer_cd = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
            End If

            'Generate Gatein EDI file and Repepair Completion
            If bv_blnGateIn = True Or bv_blnRepairComplete = True Then
                Dim bv_strActivity1 As String
                Dim bv_strActivity2 As String
                Dim strActivity As String
                If CommonWeb.pub_GetConfigValue("ActGatein") = "GATEIN" Or CommonWeb.pub_GetConfigValue("ActRepairComplete") = "REPAIRCOMPLETE" Then
                    If bv_blnGateIn Then
                        bv_strActivity1 = "D,I,A"
                    Else
                        bv_strActivity1 = ""
                    End If
                    If bv_blnRepairComplete Then
                        bv_strActivity2 = "C"
                    Else
                        bv_strActivity2 = ""
                    End If
                    If bv_blnGateIn Then
                        strActivity = "GATEIN"
                    Else
                        strActivity = "REPAIRCOMPLETE"
                    End If
                    '    bv_Customer_Lessor = String.Concat(bv_Customer_cd, ",", bv_Customer_Lessor)
                    by_strTimeStamp = String.Concat(bv_strActivity1, ",", bv_strActivity2)
                    Dim strResult As String = objEDI.pub_WriteGatinFile(strWfdata, bv_Customer_cd, bv_strGenerationMode, by_strTimeStamp)
                    If strResult <> "No Gate In Records found for EDI generation" Then
                        Dim strmsg As String() = strResult.Split(",")
                        Dim fileName As String = String.Concat("", ",", Path.GetFileName(strmsg(1)))
                        pvt_CreateEDI(bv_Customer_Orginal, "GATEIN", DateTime.Now, 0, fileName, "ANSII", bv_CustomerID, objcommon.GetCurrentUserName(), DateTime.Now, strWfdata)
                        sbMsg.Append(strmsg(0) & "<br />")
                    Else
                        sbMsg.Append(strResult & "<br />")
                    End If

                End If

            End If
            'Generate Westim & Westimdt EDI file
            If bv_blnRepairEstimate = True Then
                If CommonWeb.pub_GetConfigValue("ActEstimate") = "ESTIMATE" Then
                    Dim strResult As String = objEDI.pub_WriteWestimFile(strWfdata, bv_Customer_cd, bv_strGenerationMode)
                    If strResult <> "No Estimate Records found for EDI generation" Then
                        Dim strmsg As String() = strResult.Split(",")

                        '  Dim fileName As String = String.Concat(strmsg(1), "Westimdt.exp")
                        Dim fileName As String = Path.GetFileName(strmsg(1))
                        Dim strfilename As String = String.Empty
                        For i = 1 To strmsg.Length - 1
                            strfilename = String.Concat(strfilename, ",", Path.GetFileName(strmsg(i)))
                        Next
                        '  pvt_CreateEDI(bv_Customer_cd, "ESTIMATE", DateTime.Now, 0, fileName, fileName, objcommon.GetCurrentUserName(), DateTime.Now, strWfdata)
                        pvt_CreateEDI(bv_Customer_Orginal, "ESTIMATE", DateTime.Now, 0, strfilename, "ANSII", bv_CustomerID, objcommon.GetCurrentUserName(), DateTime.Now, strWfdata)
                        sbMsg.Append(strmsg(0) & "<br />")
                    Else
                        sbMsg.Append(strResult & "<br />")
                    End If
                End If
            End If

            'Generate Gateout EDI file
            If bv_blnGateOut = True Then
                If CommonWeb.pub_GetConfigValue("ActGateout") = "GATEOUT" Then

                    Dim strResult As String = objEDI.pub_WriteGateoutFile(strWfdata, bv_Customer_cd, bv_strGenerationMode)
                    If strResult <> "No Gate out Records found for EDI generation" Then
                        Dim strmsg As String() = strResult.Split(",")

                        ' Dim fileName As String = String.Concat(strmsg(1), "Gateout.exp")
                        Dim fileName As String = String.Concat("", ",", Path.GetFileName(strmsg(1)))
                        pvt_CreateEDI(bv_Customer_Orginal, "GATEOUT", DateTime.Now, 0, fileName, "ANSII", bv_CustomerID, objcommon.GetCurrentUserName(), DateTime.Now, strWfdata)
                        sbMsg.Append(strmsg(0) & "<br />")
                    Else
                        sbMsg.Append(strResult & "<br />")
                    End If

                    '  sbMsg.Append(objEDI.pub_WriteGateoutFile(strWfdata, bv_Customer_cd, bv_strGenerationMode) & "<br />")
                End If
            End If

            pub_SetCallbackReturnValue("Msg", sbMsg.ToString)


        Catch ex As Exception
            pub_SetCallbackReturnValue("Msg", "EDI file generation failed")
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GenerateEDICodeco"
    Private Sub pvt_GenerateEDICodeco(ByVal bv_blnGateIn As Boolean, ByVal bv_blnGateOut As Boolean, ByVal bv_blnRepairComplete As Boolean, ByVal bv_blnRepairEstimate As Boolean, ByVal strWfdata As String, ByVal bv_Customer_cd As String, ByVal bv_CustomerID As Long, ByVal bv_strGenerationMode As String)

        Dim strDepotCode As String = ""
        Dim objCommon As New CommonData()
        ' str_033KeyValue = objCommon.GetConfigSetting("033", bln_033KeyExist)
        Dim sbMsg As New System.Text.StringBuilder
        Dim dsEDiDataSet As New EDIDataSet
        Dim by_strTimeStamp As String = ""
        Dim bv_Customer_Orginal As String
        Dim strDepotCodition As String = String.Concat("DPT_ID=", objCommon.GetHeadQuarterID().ToString)
        Try
            Dim objEDI As New EDI
            'for EDI code passed as Customer 
            bv_Customer_Orginal = bv_Customer_cd
            dsCustomer = objISO.getISOCODEbyCustomer(CLng(bv_CustomerID))
            If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                bv_Customer_cd = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
            Else
                bv_Customer_cd = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
            End If
            If bv_blnGateIn = True OrElse bv_blnGateOut = True OrElse bv_blnRepairComplete = True Then
                dsEDiDataSet.Tables(EDIData._EDI_FILE_IDENTIFIER).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_VERSION).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_MOVEMENT).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Clear()
                If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                    dsEDiDataSet.Tables(EDIData._EDI_FILE_IDENTIFIER).Merge(objEDI.pub_GetEdiFileIdentifierByDpt_Id(strDepotCodition).Tables(EDIData._EDI_FILE_IDENTIFIER))
                    dsEDiDataSet.Tables(EDIData._EDI_VERSION).Merge(objEDI.pub_GetEdiVersionByDpt_Id(strDepotCodition).Tables(EDIData._EDI_VERSION))
                    dsEDiDataSet.Tables(EDIData._EDI_MOVEMENT).Merge(objEDI.pub_GetEdiMovementByDpt_Id(strDepotCodition).Tables(EDIData._EDI_MOVEMENT))
                    dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Merge(objEDI.pub_GetEdiSegmentHeaderByDpt_Id(strDepotCodition).Tables(EDIData._EDI_SEGMENT_HEADER))
                    dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Merge(objEDI.pub_GetEdiSegmentDetailByDpt_Id(strDepotCodition).Tables(EDIData._EDI_SEGMENT_DETAIL))
                Else
                    dsEDiDataSet.Tables(EDIData._EDI_FILE_IDENTIFIER).Merge(objEDI.pub_GetEdiFileIdentifierByDpt_Id(strWfdata).Tables(EDIData._EDI_FILE_IDENTIFIER))
                    dsEDiDataSet.Tables(EDIData._EDI_VERSION).Merge(objEDI.pub_GetEdiVersionByDpt_Id(strWfdata).Tables(EDIData._EDI_VERSION))
                    dsEDiDataSet.Tables(EDIData._EDI_MOVEMENT).Merge(objEDI.pub_GetEdiMovementByDpt_Id(strWfdata).Tables(EDIData._EDI_MOVEMENT))
                    dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Merge(objEDI.pub_GetEdiSegmentHeaderByDpt_Id(strWfdata).Tables(EDIData._EDI_SEGMENT_HEADER))
                    dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Merge(objEDI.pub_GetEdiSegmentDetailByDpt_Id(strWfdata).Tables(EDIData._EDI_SEGMENT_DETAIL))
                End If


             
            End If

            '    If bln_033KeyExist = True Then
            'Generate Gatein EDI file
            If bv_blnGateIn = True Then
                If CommonWeb.pub_GetConfigValue("ActGatein") = "GATEIN" Then
                    dsEDiDataSet.Tables(EDIData._GATEIN_RET).Clear()
                    dsEDiDataSet.Tables(EDIData._GATEIN_RET).Merge(objEDI.pub_GetGateinRetNoRCByGI_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEIN_RET))
                    '  sbMsg.Append(objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEIN", bv_Customer_cd, bv_strGenerationMode) & "<br />")
                    Dim strResult As String = objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEIN", bv_Customer_cd, bv_strGenerationMode, strWfdata)
                    If strResult <> "No Gate In Records found for EDI generation" Then
                        Dim strmsg As String() = strResult.Split(",")
                        Dim fileName As String = String.Concat("", ",", Path.GetFileName(strmsg(1)))
                        pvt_CreateEDI(bv_Customer_Orginal, "GATEIN", DateTime.Now, 0, fileName, "CODECO", bv_CustomerID, objCommon.GetCurrentUserName(), DateTime.Now, strWfdata)
                        sbMsg.Append(strmsg(0) & "<br />")
                    Else
                        sbMsg.Append(strResult & "<br />")
                    End If
                End If

            End If
          
            'Generate Repair Complete EDI file
            If bv_blnRepairComplete = True Then
                If CommonWeb.pub_GetConfigValue("ActRepairComplete") = "REPAIRCOMPLETE" Then
                    dsEDiDataSet.Tables(EDIData._GATEIN_RET).Clear()
                    dsEDiDataSet.Tables(EDIData._GATEIN_RET).Merge(objEDI.pub_GetRepairCompletenRetByGI_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEIN_RET))
                    'sbMsg.Append(objEDI.pub_WriteCodeco(dsEDiDataSet, "REPAIRCOMPLETE", bv_Customer_cd, bv_strGenerationMode) & "<br />")
                    Dim strResult As String = objEDI.pub_WriteCodeco(dsEDiDataSet, "REPAIRCOMPLETE", bv_Customer_cd, bv_strGenerationMode, strWfdata)
                    If strResult <> "No Repair Complete Records found for EDI generation" Then
                        Dim strmsg As String() = strResult.Split(",")
                        Dim fileName As String = String.Concat("", ",", Path.GetFileName(strmsg(1)))
                        pvt_CreateEDI(bv_Customer_Orginal, "REPAIRCOMPLETE", DateTime.Now, 0, fileName, "CODECO", bv_CustomerID, objCommon.GetCurrentUserName(), DateTime.Now, strWfdata)
                        sbMsg.Append(strmsg(0) & "<br />")
                    Else
                        sbMsg.Append(strResult & "<br />")
                    End If
                End If

            End If
            'Generate Westim & Westimdt EDI file
            If bv_blnRepairEstimate = True Then
                '  If CommonWeb.pub_GetConfigValue("ActEstimate") = "REPAIRCOMPLETE" Then
                If CommonWeb.pub_GetConfigValue("ActEstimate") = "ESTIMATE" Then
                    '   sbMsg.Append(objEDI.pub_WriteWestimFile(strWfdata, bv_Customer_cd, bv_strGenerationMode) & "<br />")
                    Dim strResult As String = objEDI.pub_WriteWestimFile(strWfdata, bv_Customer_cd, bv_strGenerationMode)
                    If strResult <> "No Estimate Records found for EDI generation" Then
                        Dim strmsg As String() = strResult.Split(",")

                        '  Dim fileName As String = String.Concat(strmsg(1), "Westimdt.exp")
                        Dim fileName As String = Path.GetFileName(strmsg(1))
                        Dim strfilename As String = String.Empty
                        For i = 1 To strmsg.Length - 1
                            strfilename = String.Concat(strfilename, ",", Path.GetFileName(strmsg(i)))
                        Next
                        '  pvt_CreateEDI(bv_Customer_cd, "ESTIMATE", DateTime.Now, 0, fileName, fileName, objcommon.GetCurrentUserName(), DateTime.Now, strWfdata)
                        pvt_CreateEDI(bv_Customer_Orginal, "ESTIMATE", DateTime.Now, 0, strfilename, "ANSII", bv_CustomerID, objCommon.GetCurrentUserName(), DateTime.Now, strWfdata)
                        sbMsg.Append(strmsg(0) & "<br />")
                    Else
                        sbMsg.Append(strResult & "<br />")
                    End If
                End If

            End If
            'Generate Gateout EDI file
            If bv_blnGateOut = True Then
                If CommonWeb.pub_GetConfigValue("ActGateout") = "GATEOUT" Then
                    dsEDiDataSet.Tables(EDIData._GATEOUT_RET).Clear()
                    dsEDiDataSet.Tables(EDIData._GATEOUT_RET).Merge(objEDI.pub_GetGateoutRetByGO_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEOUT_RET))
                    ' sbMsg.Append(objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEOUT", bv_Customer_cd, bv_strGenerationMode) & "<br />")
                    Dim strResult As String = objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEOUT", bv_Customer_cd, bv_strGenerationMode, strWfdata)
                    If strResult <> "No Gate Out Records found for EDI generation" Then
                        Dim strmsg As String() = strResult.Split(",")
                        Dim fileName As String = String.Concat("", ",", Path.GetFileName(strmsg(1)))
                        pvt_CreateEDI(bv_Customer_Orginal, "GATEOUT", DateTime.Now, 0, fileName, "CODECO", bv_CustomerID, objCommon.GetCurrentUserName(), DateTime.Now, strWfdata)
                        sbMsg.Append(strmsg(0) & "<br />")
                    Else
                        sbMsg.Append(strResult & "<br />")
                    End If
                End If
            End If

            pub_SetCallbackReturnValue("Msg", sbMsg.ToString)
        Catch ex As Exception
            pub_SetCallbackReturnValue("Msg", "EDI file generation failed")
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

    Protected Sub ifgEdi_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEdi.RowDataBound
        Try
            strGetFolderName = Directory.GetFileSystemEntries(strFileFolder)
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim strPath As String = String.Empty
                Dim strFileName As String = String.Empty
                Dim strFilePath As String = String.Empty

                For intNoFiles = 0 To strGetFolderName.Length - 1
                    Dim strfile As String = String.Concat(strGetFolderName(intNoFiles), "\")
                    Dim strfiles() As String = Directory.GetFileSystemEntries(strfile)
                    strFolderName = strGetFolderName(intNoFiles).ToString.Substring(strGetFolderName(intNoFiles).LastIndexOf("\") + 1).ToString
                    strFileName = strGetFolderName(intNoFiles)
                    Dim hlkAttachments1 As HyperLink
                    'hlkAttachments1 = CType(e.Row.Cells(4).Controls(0), HyperLink)
                    hlkAttachments1 = CType(e.Row.Cells(5).Controls(0), HyperLink)
                    hlkAttachments1.NavigateUrl = "#"
                    hlkAttachments1.Attributes.Add("onclick", "fnDownloadExcelFile();return false;")
                    hlkAttachments1.Text = CStr(strfiles(intNoFiles))

                    If CInt(drv.Item(EDIData.TMS_SNT)) > 0 Then
                        Dim SentMailhyplnk As HyperLink
                        SentMailhyplnk = CType(e.Row.Cells(8).Controls(0), HyperLink)
                        'SentMailhyplnk.Attributes.Add("onclick", String.Concat("openEDIEmailDetail('", drv.Item(EDI.GI_TRNSCTN_NO).ToString(), "','", drv.Item(BulkEmailData.ACTVTY_NAM).ToString(), "','", drv.Item(BulkEmailData.EQPMNT_NO).ToString(), "','", drv.Item(BulkEmailData.ACTVTY_NO), "'    );return false;"))
                        SentMailhyplnk.Attributes.Add("onclick", String.Concat("openEDIEmailDetail('", drv.Item(EDIData.EDI_ID).ToString(), "');return false;"))
                        SentMailhyplnk.NavigateUrl = "#"
                    End If
                    Dim imgSendLink As Image
                    imgSendLink = CType(e.Row.Cells(9).Controls(0), Image)
                    imgSendLink.ToolTip = "send"
                    imgSendLink.Visible = True
                    imgSendLink.ImageUrl = "../Images/BulkEMail.png"
                    imgSendLink.Attributes.Add("onclick", String.Concat("fnEdimailSend('", e.Row.RowIndex.ToString, "','", drv.Item(EDIData.CSTMR_ID), "','", drv.Item(EDIData.EDI_ACTVTY_NAM), "','", drv.Item(EDIData.ANC_FL_NM), "','", drv.Item(EDIData.EDI_ID), "','", drv.Item(EDIData.LST_SNT_DT), "','", drv.Item(EDIData.CSTMR_CD), "'    );return false;"))

                    imgSendLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                Next
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#Region "Download File"
    Public Sub fileDownload()
        Try
            Dim downloadFilePath As String
            Dim bv_Customer_Lessor As String
            Dim objCommon As New CommonData
            Dim strDepotCD As String = objCommon.GetDepotCD()
            'Request.QueryString("Customer")
            dsCustomer = objISO.getISOCODEbyCustomer(strCustomerID)
            If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                bv_Customer_Lessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
            Else
                bv_Customer_Lessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
            End If
            strDownloadFileName = bv_Customer_Lessor
            strFileName = Request.QueryString("FileName")
            downloadFilePath = String.Concat(strFileFolder, strDepotCD, "\", strDownloadFileName, "\", strFileName)
            If System.IO.File.Exists(downloadFilePath) Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & strFileName)
                Response.TransmitFile(downloadFilePath)
                Response.End()
            Else
                Page.ClientScript.RegisterStartupScript(GetType(String), "error", "showErrorMessage(""File not available. It is deleted or archived from the server."");", True)

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)

        End Try
    End Sub
#End Region
End Class
