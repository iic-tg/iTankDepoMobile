
Partial Class Operations_EDI
    Inherits Pagebase

#Region "Declarations"
    Dim bln_002KeyExist As Boolean = False
    Dim bln_033KeyExist As Boolean = False
    Dim str_002KeyValue As String
    Dim str_033KeyValue As String
    Dim strGenerationMode As String = "Manual"
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("../Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("../Script/Operations/EDI.js", MyBase.Page)
            CommonWeb.IncludeScript("../Script/EDI/EDI.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack AndAlso pub_Callback() = False Then
            Dim objCommon As New CommonData()
            str_002KeyValue = objCommon.GetConfigSetting("002", bln_002KeyExist)
            str_033KeyValue = objCommon.GetConfigSetting("033", bln_033KeyExist)
            Dim strEDIValue As String = Nothing
            If bln_002KeyExist Then
                If str_002KeyValue.Contains("CODECO") Then
                    lkpEDIFormat.ReadOnly = False
                    lkpEDIFormat.Text = "CODECO"
                ElseIf str_002KeyValue = "ANSII" Then
                    lkpEDIFormat.ReadOnly = True
                    lkpEDIFormat.Text = "ANSII"
                End If
                If str_033KeyValue.Contains("RepairComplete") Then
                    lblRepairComplete.Visible = True
                    chkRepairComplete.Visible = True
                Else
                    lblRepairComplete.Visible = False
                    chkRepairComplete.Visible = False
                End If
            End If
        End If
        CommonWeb.pub_AttachHasChanges(lkpCustomer)
        CommonWeb.pub_AttachHasChanges(lkpEDIFormat)
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "ChkFileExists"
                pvt_ChkFileExists(e.GetCallbackValue("GateIn"), e.GetCallbackValue("GateOut"), e.GetCallbackValue("RepairEstimate"), e.GetCallbackValue("Customer_cd"))
            Case "GenerateEDI"
                If e.GetCallbackValue("EdiFormat").ToString.Contains("CODECO") Then
                    pvt_GenerateEDICodeco(e.GetCallbackValue("GateIn"), e.GetCallbackValue("GateOut"), e.GetCallbackValue("RepairComplete"), e.GetCallbackValue("RepairEstimate"), e.GetCallbackValue("WfData"), e.GetCallbackValue("Customer_cd"), strGenerationMode)
                ElseIf e.GetCallbackValue("EdiFormat").ToString = "ANSII" Then
                    pvt_GenerateEDI(e.GetCallbackValue("GateIn"), e.GetCallbackValue("GateOut"), e.GetCallbackValue("RepairEstimate"), e.GetCallbackValue("WfData"), e.GetCallbackValue("Customer_cd"), strGenerationMode)
                End If
        End Select
    End Sub
#End Region


#Region "pvt_ChkFileExists"
    Private Sub pvt_ChkFileExists(ByVal bv_blnGateIn As Boolean, ByVal bv_blnGateOut As Boolean, ByVal bv_blnRepairEstimate As Boolean, ByVal bv_Customer_cd As String)
        Dim strOutFolderName As String
        Dim strTempFolderName As String
        Dim strFileExtension As String
        Dim blnExists As Boolean = False
        Try
            strOutFolderName = String.Concat(CommonWeb.pub_GetConfigValue("OutputFolder"), bv_Customer_cd, "\")
            strTempFolderName = CommonWeb.pub_GetConfigValue("TempFolder")
            strFileExtension = CommonWeb.pub_GetConfigValue("Extn")

            Dim GateInFileName As String = String.Concat(strOutFolderName, "Gatein", strFileExtension)
            Dim GateOutFileName As String = String.Concat(strOutFolderName, "Gateout", strFileExtension)
            Dim RprEstFileName As String = String.Concat(strOutFolderName, "Westim", strFileExtension)
            Dim RprEstdtFileName As String = String.Concat(strOutFolderName, "Westimdt", strFileExtension)

            If IO.File.Exists(GateInFileName) Or IO.File.Exists(GateOutFileName) Or IO.File.Exists(RprEstFileName) Or IO.File.Exists(RprEstdtFileName) Then
                blnExists = True
            End If

            pub_SetCallbackReturnValue("IsExists", blnExists)
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub
#End Region

#Region "pvt_GenerateEDI"
    Private Sub pvt_GenerateEDI(ByVal bv_blnGateIn As Boolean, ByVal bv_blnGateOut As Boolean, ByVal bv_blnRepairEstimate As Boolean, ByVal strWfdata As String, ByVal bv_Customer_cd As String, ByVal bv_strGenerationMode As String)

        Dim strDepotCode As String = ""
        Dim sbMsg As New System.Text.StringBuilder

        Try
            Dim objEDI As New EDI

            'Generate Gatein EDI file
            If bv_blnGateIn = True Then
                If CommonWeb.pub_GetConfigValue("ActGatein") = "GATEIN" Then
                    sbMsg.Append(objEDI.pub_WriteGatinFile(strWfdata, bv_Customer_cd, bv_strGenerationMode) & "<br />")
                End If

            End If
            'Generate Westim & Westimdt EDI file
            If bv_blnRepairEstimate = True Then
                If CommonWeb.pub_GetConfigValue("ActEstimate") = "ESTIMATE" Then
                    sbMsg.Append(objEDI.pub_WriteWestimFile(strWfdata, bv_Customer_cd, bv_strGenerationMode) & "<br />")
                End If

            End If
            'Generate Gateout EDI file
            If bv_blnGateOut = True Then
                If CommonWeb.pub_GetConfigValue("ActGateout") = "GATEOUT" Then
                    sbMsg.Append(objEDI.pub_WriteGateoutFile(strWfdata, bv_Customer_cd, bv_strGenerationMode) & "<br />")
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
    Private Sub pvt_GenerateEDICodeco(ByVal bv_blnGateIn As Boolean, ByVal bv_blnGateOut As Boolean, ByVal bv_blnRepairComplete As Boolean, ByVal bv_blnRepairEstimate As Boolean, ByVal strWfdata As String, ByVal bv_Customer_cd As String, ByVal bv_strGenerationMode As String)

        Dim strDepotCode As String = ""
        Dim objCommon As New CommonData()
        str_033KeyValue = objCommon.GetConfigSetting("033", bln_033KeyExist)
        Dim sbMsg As New System.Text.StringBuilder
        Dim dsEDiDataSet As New EDIDataSet
        Try
            Dim objEDI As New EDI

            If bv_blnGateIn = True OrElse bv_blnGateOut = True Then
                dsEDiDataSet.Tables(EDIData._EDI_FILE_IDENTIFIER).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_FILE_IDENTIFIER).Merge(objEDI.pub_GetEdiFileIdentifierByDpt_Id(strWfdata).Tables(EDIData._EDI_FILE_IDENTIFIER))
                dsEDiDataSet.Tables(EDIData._EDI_VERSION).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_VERSION).Merge(objEDI.pub_GetEdiVersionByDpt_Id(strWfdata).Tables(EDIData._EDI_VERSION))
                dsEDiDataSet.Tables(EDIData._EDI_MOVEMENT).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_MOVEMENT).Merge(objEDI.pub_GetEdiMovementByDpt_Id(strWfdata).Tables(EDIData._EDI_MOVEMENT))
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Merge(objEDI.pub_GetEdiSegmentHeaderByDpt_Id(strWfdata).Tables(EDIData._EDI_SEGMENT_HEADER))
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Merge(objEDI.pub_GetEdiSegmentDetailByDpt_Id(strWfdata).Tables(EDIData._EDI_SEGMENT_DETAIL))
            End If

            If bln_033KeyExist = True Then
                'Generate Gatein EDI file
                If bv_blnGateIn = True Then
                    If CommonWeb.pub_GetConfigValue("ActGatein") = "GATEIN" Then
                        dsEDiDataSet.Tables(EDIData._GATEIN_RET).Clear()
                        dsEDiDataSet.Tables(EDIData._GATEIN_RET).Merge(objEDI.pub_GetGateinRetNoRCByGI_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEIN_RET))
                        sbMsg.Append(objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEIN", bv_Customer_cd, bv_strGenerationMode, strWfdata) & "<br />")
                    End If

                End If
                'Generate Repair Complete EDI file
                If bv_blnRepairComplete = True Then
                    If CommonWeb.pub_GetConfigValue("ActRepairComplete") = "REPAIRCOMPLETE" Then
                        dsEDiDataSet.Tables(EDIData._GATEIN_RET).Clear()
                        dsEDiDataSet.Tables(EDIData._GATEIN_RET).Merge(objEDI.pub_GetRepairCompletenRetByGI_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEIN_RET))
                        sbMsg.Append(objEDI.pub_WriteCodeco(dsEDiDataSet, "REPAIRCOMPLETE", bv_Customer_cd, bv_strGenerationMode, strWfdata) & "<br />")
                    End If

                End If
            Else
                'Generate Gatein EDI file
                If bv_blnGateIn = True Then
                    If CommonWeb.pub_GetConfigValue("ActGatein") = "GATEIN" Then
                        dsEDiDataSet.Tables(EDIData._GATEIN_RET).Clear()
                        dsEDiDataSet.Tables(EDIData._GATEIN_RET).Merge(objEDI.pub_GetGateinRetByGI_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEIN_RET))
                        sbMsg.Append(objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEIN", bv_Customer_cd, bv_strGenerationMode, strWfdata) & "<br />")
                    End If

                End If
            End If

            'Generate Westim & Westimdt EDI file
            If bv_blnRepairEstimate = True Then
                If CommonWeb.pub_GetConfigValue("ActEstimate") = "ESTIMATE" Then
                    sbMsg.Append(objEDI.pub_WriteWestimFile(strWfdata, bv_Customer_cd, bv_strGenerationMode) & "<br />")
                End If

            End If
            'Generate Gateout EDI file
            If bv_blnGateOut = True Then
                If CommonWeb.pub_GetConfigValue("ActGateout") = "GATEOUT" Then
                    dsEDiDataSet.Tables(EDIData._GATEOUT_RET).Clear()
                    dsEDiDataSet.Tables(EDIData._GATEOUT_RET).Merge(objEDI.pub_GetGateoutRetByGO_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEOUT_RET))
                    sbMsg.Append(objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEOUT", bv_Customer_cd, bv_strGenerationMode, strWfdata) & "<br />")
                End If
            End If

            pub_SetCallbackReturnValue("Msg", sbMsg.ToString)


        Catch ex As Exception
            pub_SetCallbackReturnValue("Msg", "EDI file generation failed")
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
