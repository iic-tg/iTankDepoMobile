Option Strict On
Partial Class Operations_GateIn
    Inherits Pagebase

    Dim dsGateInData As New GateinDataSet
    Dim dsGateInAttchmentData As New GateinDataSet
    Dim dtGateinData As DataTable
    Private Const GATE_IN As String = "GATE_IN"
    Private Const GATE_IN_DOCUMENT As String = "GATE_IN_DOCUMENT"
    Private strMSGUPDATE As String = "Gate In Creation : Equipment(s) Updated Successfully."
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"
    Dim bln_011KeyExist As Boolean
    Dim bln_020KeyExist As Boolean
    Dim str_020KeyValue As String
    Dim bln_005EqStatus_Key As Boolean
    'Dim bln_006YardLocation_Key As Boolean
    Dim bln_007EIRNo_Key As Boolean
    Dim bln_008EIRTime_Key As Boolean
    Dim bln_009EqType_Key As Boolean
    'Dim bln_010EqCode_Key As Boolean

    Dim str_005EqStatus As String
    Dim str_006YardLocation As String
    Dim str_007EIRNo As String
    Dim str_008EIRTime As String
    Dim str_009EqType As String
    'Dim str_010EqCode As String
    Private Const GateInMode As String = "GateInMode"
    Dim strMode As String
    Dim objCommonConfig As New ConfigSetting()
    Dim bln_022RentalBit_Key As Boolean
    Dim str_022RentalBit As String
    '  Private AttachmentClear As String

#Region "Page_Load1"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim objCommon As New CommonData
                Dim objGateIn As New Gatein


                hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
                hdnCurrentTime.Value = DateTime.Now.ToString("H:mm")
                hdnDepotID.Value = CStr(objCommon.GetDepotID())
                'dsGateInData = objGateIn.GetGateInPreAdviceDetail(1)
                pvt_SetChangesMade()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/EquipmentInformation.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/GateIn.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgEquipmentDetail, "ITab1_0")
    End Sub

#End Region


#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "UpdateGateIn"
                    pvt_UpdateGateIn(e.GetCallbackValue("WFData"))
                Case "fnGetData"
                    pvt_GetData()
                Case "ValidateEquipment"
                    pvt_ValidateEquipment(e.GetCallbackValue("EquipmentId"), CInt(e.GetCallbackValue("GridIndex")), e.GetCallbackValue("RowState"))
                Case "ValidatePreviousActivityDate"
                    pvt_ValidatePreviousActivityDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("EventDate"))
                Case "validateRentalEntry"
                    pvt_validateRentalEntry(e.GetCallbackValue("EquipmentNo"))
                Case "getSupplierDetails"
                    pvt_getSupplierDetails(e.GetCallbackValue("EquipmentNo"))
                Case "checkRentalBit"
                    pvt_checkRentalBit()
                Case "GIlockData"
                    pvt_GIlockData(e.GetCallbackValue("CheckBit"), _
                                  e.GetCallbackValue("EquipmentNo"), _
                                  e.GetCallbackValue("LockBit"))
                Case "ValidateGateINAttachment"
                    pvt_ValidateGateINAttachment(e.GetCallbackValue("GateInPreAdvice"))

                Case "GetEquipmentCode"
                    pvt_GetEquipmentCode(e.GetCallbackValue("Type"))
                Case "GetAgentName"
                    pvt_getAgentName(e.GetCallbackValue("CustomerCode"))
                Case "GetEquipmentCodebyTypeId"
                    pvt_GetEquipmentCodebyTypeId(e.GetCallbackValue("TypeID"))
                Case "GetYardLocation"
                    pvt_GetYardLocation()
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region


#Region "pvt_GetEquipmentCode"

    Private Sub pvt_GetEquipmentCodebyTypeId(ByVal bv_strTypeId As String)
        Try
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData

            Dim intDepotID As Integer
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommon.GetDepotID())
            End If

            Dim dt As New DataTable

            dt = objCommonUI.GetEquipmentCodeByTypeID(bv_strTypeId, intDepotID)

            If dt.Rows.Count > 0 Then
                pub_SetCallbackReturnValue("Code", dt.Rows(0).Item(GateinData.EQPMNT_CD_CD).ToString())
                pub_SetCallbackReturnValue("ID", dt.Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString())
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackStatus(False)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Sub pvt_GetEquipmentCode(ByVal bv_strType As String)
        Try
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData

            Dim intDepotID As Integer
            Dim dt As New DataTable
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommon.GetDepotID())
            End If
            dt = objCommonUI.GetEquipmentCodeByType(bv_strType, intDepotID)

            If dt.Rows.Count > 0 Then
                pub_SetCallbackReturnValue("Code", dt.Rows(0).Item(GateinData.EQPMNT_CD_CD).ToString())
                pub_SetCallbackReturnValue("ID", dt.Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString())
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackStatus(False)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_checkRentalBit()"
    Private Sub pvt_checkRentalBit()
        Try
            Dim blnRentalBit As Boolean = False
            dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
            Dim drRentalBit As DataRow() = Nothing
            drRentalBit = dsGateInData.Tables(GateinData._V_GATEIN).Select(String.Concat(GateinData.CHECKED, " = ", "True AND ", GateinData.RNTL_BT, " = ", "True"))
            If drRentalBit.Length > 0 Then
                blnRentalBit = True
            End If
            pub_SetCallbackReturnValue("RenalBit", blnRentalBit.ToString.ToLower)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData()
        Try
            Dim objCommon As New CommonData()
            Dim datGetDateTime As DateTime = CDate(objCommon.GetCurrentDate())
            Dim sbGateIn As New StringBuilder
            pub_SetCallbackReturnValue("Message", sbGateIn.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateEquipment"
    Private Sub pvt_ValidateEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_intGridIndex As Integer, ByVal bv_strRowstate As String)
        Try
            Dim objGateIn As New Gatein
            Dim objCommon As New CommonData
            Dim strCustomer As String = String.Empty
            Dim blndsValid As Boolean
            Dim blnRental As Boolean
            Dim blnRentalEntry As Boolean
            Dim blnDuplicateEquipment As Boolean = True
            Dim blnPreadviceEquipment As Boolean = True
            Dim blnGTOT_BT As Boolean
            dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
            dtGateinData = dsGateInData.Tables(GateinData._V_GATEIN)
            Dim intResultIndex() As System.Data.DataRow = dtGateinData.Select(String.Concat(GateinData.EQPMNT_NO, "='", bv_strEquipmentNo, "' "))
            Dim strExistEquipment As String = ""
            If intResultIndex.Length > 0 Then
                If dtGateinData.Rows.Count > bv_intGridIndex Then
                    If dtGateinData.Rows(bv_intGridIndex).RowState = Data.DataRowState.Deleted Then
                        strExistEquipment = String.Empty
                    ElseIf dtGateinData.Rows(bv_intGridIndex).RowState <> Data.DataRowState.Deleted Then
                        strExistEquipment = dtGateinData.Rows(bv_intGridIndex)(GateinData.EQPMNT_NO).ToString
                    End If
                End If

                If bv_strEquipmentNo = strExistEquipment Then
                    blndsValid = True
                    'pub_SetCallbackReturnValue("bNotExists", "false")
                    'pub_SetCallbackReturnValue("Customer", dtGateinData.Rows(bv_intGridIndex)(GateinData.CSTMR_ID).ToString)
                    'pub_SetCallbackStatus(True)
                    'Exit Sub
                Else
                    blndsValid = False
                End If
            Else
                blndsValid = True
            End If

            If blndsValid = False Then
                For Each drGateIn As DataRow In dtGateinData.Select(String.Concat(GateinData.EQPMNT_NO, "='", bv_strEquipmentNo, "'"))
                    strCustomer = CStr(drGateIn.Item(GateinData.CSTMR_CD))
                Next
            End If

            'Checking whether the entered code is available in database
            If blndsValid = True Then

                blndsValid = objGateIn.pub_ValidateEquipmentNoByDepotID(bv_strEquipmentNo, CInt(objCommon.GetDepotID()), strCustomer)
            End If
            blnRental = objGateIn.GetRentalDetails(bv_strEquipmentNo, CInt(objCommon.GetDepotID()), strCustomer)
            blnRentalEntry = objGateIn.pub_GetRentalEntry(bv_strEquipmentNo, CInt(objCommon.GetDepotID()), blnGTOT_BT)

            'Code here to check whether this Equpt no is used for Gate In in any other Depot start here---
            ''        
            If blndsValid = True Then
                blnPreadviceEquipment = objGateIn.pub_ValidateEquipmentNoInPreAdvice(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))
                If blnPreadviceEquipment = False Then
                    'Newly added if Equiment No already available in Preadvice in some other depot                
                    pub_SetCallbackReturnValue("EquipmentNoInAnotherDepot", "false")
                End If

                blnDuplicateEquipment = objGateIn.pub_ValidateStatusOfEquipment(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))
                If blnDuplicateEquipment = False Then
                    'Newly added if Equiment No already available in Preadvice in some other depot                
                    pub_SetCallbackReturnValue("StatusOfEquipment", "false")
                End If
            End If
            ''--Ends here

            If blndsValid = True Then

                If blnRental = False Then
                    pub_SetCallbackReturnValue("Customer", strCustomer)
                    pub_SetCallbackReturnValue("bRentalNotExists", "false")
                    pub_SetCallbackReturnValue("GateOutNotExists", CStr(blnGTOT_BT))
                    pub_SetCallbackStatus(True)
                Else
                    pub_SetCallbackReturnValue("bNotExists", "true")

                End If
            Else
                If blnRental = True Then
                    pub_SetCallbackReturnValue("bRentalNotExists", "true")
                    pub_SetCallbackReturnValue("Customer", strCustomer)
                    pub_SetCallbackReturnValue("bNotExists", "false")
                Else
                    pub_SetCallbackReturnValue("Customer", strCustomer)
                    pub_SetCallbackReturnValue("bRentalNotExists", "false")
                    pub_SetCallbackReturnValue("GateOutNotExists", CStr(blnGTOT_BT))
                    pub_SetCallbackStatus(True)
                End If

            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_UpdateGateIn"
    Private Sub pvt_UpdateGateIn(ByVal bv_strWfData As String)
        Try
            strMode = CType(RetrieveData(GateInMode), String)
            Dim dtGateInDocument As DataTable
            Dim drgateIn As DataRow

            Dim objCommonUI As New CommonUI
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer = CInt(objCommondata.GetDepotID())
            Dim dsDepot As DataSet
            dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
            Dim drAGatein As DataRow()
            Dim blnAllowRental As Boolean = False
            drAGatein = dsGateInData.Tables(GateinData._V_GATEIN).Select(GateinData.CHECKED & "='True'")
            dtGateInDocument = dsGateInData.Tables(GateinData._V_GATEIN).Clone()
            If Not drAGatein.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            End If
            ''Lock Implementation - Unlock after submit
            ''Lock Implementation 
            For Each dr As DataRow In dsGateInData.Tables(GateinData._V_GATEIN).Select(GateinData.CHECKED & "='True'")
                pvt_GIlockData("FALSE", dr.Item(GateinData.EQPMNT_NO).ToString, "FALSE")
                dr.Item(GateinData.CHECKED) = True
            Next

            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim str_051GWSBit As String
            Dim bln_051GWSBit_Key As Boolean
            str_051GWSBit = objCommonConfig.pub_GetConfigSingleValue("051", intDPT_ID)
            bln_051GWSBit_Key = objCommonConfig.IsKeyExists
            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            Dim str_068GWSBit As String
            Dim bln_068GWSBit_Key As Boolean
            bln_068GWSBit_Key = objCommonConfig.IsKeyExists
            str_068GWSBit = objCommonConfig.pub_GetConfigSingleValue("068", intDPT_ID)
            If bln_068GWSBit_Key = False Then
                str_068GWSBit = "false"
            End If

            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", intDPT_ID)

            Dim objgateIn As New Gatein

            Dim strRemarks As String = String.Empty
            Dim strLockingRecords As String = String.Empty

            If Not pub_RetrieveData("Equip_Remarks") Is Nothing Then
                strRemarks = pub_RetrieveData("Equip_Remarks").ToString()
            End If
            pub_CacheData("Equip_Remarks", "")
            RetrieveData("AttachmentClear")
            objgateIn.pub_UpdateGateIn(dsGateInData, bv_strWfData, _
                                       CBool(ConfigurationManager.AppSettings("GenerateEDI")), _
                                       objCommon.GetCurrentUserName(), CDate(objCommon.GetCurrentDate()), _
                                       strMode, intDPT_ID, strRemarks, _
                                       CStr(RetrieveData("AttachmentClear")), strLockingRecords, str_051GWSBit, str_068GWSBit, str_067InvoiceGenerationGWSBit)

            CacheData("AttachmentClear", Nothing)
            Dim dsGateInStatus As New GateinDataSet
            dsGateInStatus.Tables(GateinData._EQUIPMENT_STATUS).Rows.Clear()
            dsGateInStatus = objgateIn.pub_GetEqupimentStatus(intDPT_ID)
            dsGateInData.Tables(GateinData._EQUIPMENT_STATUS).Clear()
            dsGateInData.Merge(dsGateInStatus.Tables(GateinData._EQUIPMENT_STATUS))

            Dim dsGateInCustomer As New GateinDataSet
            dsGateInCustomer.Tables(GateinData._CUSTOMER).Rows.Clear()
            'MultiLocation
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                dsGateInCustomer = objgateIn.pub_GetCustomerDetail(CInt(objCommon.GetHeadQuarterID()))
            Else
                dsGateInCustomer = objgateIn.pub_GetCustomerDetail(intDPT_ID)
            End If
            dsGateInData.Tables(GateinData._CUSTOMER).Clear()
            dsGateInData.Merge(dsGateInCustomer.Tables(GateinData._CUSTOMER))
            'If strMode = MODE_NEW Then
            str_022RentalBit = objCommonConfig.pub_GetConfigSingleValue("022", intDPT_ID)
            bln_022RentalBit_Key = objCommonConfig.IsKeyExists


            If bln_022RentalBit_Key Then
                If str_022RentalBit.ToLower = "false" Then
                    blnAllowRental = False
                Else
                    blnAllowRental = True
                End If
            End If
            For Each dr As DataRow In dsGateInData.Tables(GateinData._V_GATEIN).Select(GateinData.CHECKED & "='True'")
                drgateIn = dtGateInDocument.NewRow()
                drgateIn.Item(GateinData.GTN_ID) = dr.Item(GateinData.GTN_ID)
                drgateIn.Item(GateinData.EQPMNT_NO) = dr.Item(GateinData.EQPMNT_NO)
                drgateIn.Item(GateinData.EQPMNT_TYP_ID) = dr.Item(GateinData.EQPMNT_TYP_ID)
                drgateIn.Item(GateinData.EQPMNT_TYP_CD) = dr.Item(GateinData.EQPMNT_TYP_CD)
                drgateIn.Item(GateinData.EQPMNT_CD_ID) = dr.Item(GateinData.EQPMNT_TYP_ID)
                drgateIn.Item(GateinData.EQPMNT_CD_CD) = dr.Item(GateinData.EQPMNT_CD_CD)
                drgateIn.Item(GateinData.EQPMNT_STTS_ID) = dr.Item(GateinData.EQPMNT_STTS_ID)
                drgateIn.Item(GateinData.EQPMNT_STTS_CD) = dr.Item(GateinData.EQPMNT_STTS_CD)
                drgateIn.Item(GateinData.YRD_LCTN) = dr.Item(GateinData.YRD_LCTN)
                drgateIn.Item(GateinData.GTN_DT) = dr.Item(GateinData.GTN_DT)
                drgateIn.Item(GateinData.GTN_TM) = dr.Item(GateinData.GTN_TM)
                drgateIn.Item(GateinData.PRDCT_DSCRPTN_VC) = dr.Item(GateinData.PRDCT_DSCRPTN_VC)
                Dim strEirNumGateInRet As String
                Dim bln_051GwsBit As Boolean
                Dim str_051GWSKey As String
                str_051GWSKey = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
                bln_051GwsBit = objCommonConfig.IsKeyExists
                If bln_051GwsBit Then

                    Dim dsGateInEquipmentInformation As New GateinDataSet
                    dsGateInEquipmentInformation.Tables(GateinData._EQUIPMENT_INFORMATION).Rows.Clear()
                    dsGateInEquipmentInformation = objgateIn.pub_GetEquipmentInformation(CStr(drgateIn.Item(GateinData.EQPMNT_NO)))
                    dsGateInData.Tables(GateinData._EQUIPMENT_INFORMATION).Clear()
                    dsGateInData.Merge(dsGateInEquipmentInformation.Tables(GateinData._EQUIPMENT_INFORMATION))

                    drgateIn.Item(GateinData.EIR_NO) = dr.Item(GateinData.EIR_NO)
                    drgateIn.Item(GateinData.AGNT_CD) = dr.Item(GateinData.AGNT_CD)
                    drgateIn.Item(GateinData.AGNT_ID) = dr.Item(GateinData.AGNT_ID)
                    For Each drEqpDetail As DataRow In dsGateInData.Tables(GateinData._EQUIPMENT_INFORMATION).Rows
                        drgateIn.Item(GateinData.CSC_VLDTY) = drEqpDetail.Item(GateinData.CSC_VLDTY)
                        drgateIn.Item(GateinData.MNFCTR_DT) = drEqpDetail.Item(GateinData.MNFCTR_DT)

                    Next
                    'drgateIn.Item(GateinData.CSC_VLDTY) = dr.Item(GateinData.CSC_VLDTY)
                    'drgateIn.Item(GateinData.MNFCTR_DT) = dr.Item(GateinData.MNFCTR_DT)
                Else
                    If drgateIn.Item(GateinData.EIR_NO).ToString.Trim.Length > 14 Then
                        strEirNumGateInRet = drgateIn.Item(GateinData.EIR_NO).ToString.Trim.Substring(0, 14).ToString()
                    Else
                        strEirNumGateInRet = drgateIn.Item(GateinData.EIR_NO).ToString()
                    End If
                    drgateIn.Item(GateinData.EIR_NO) = strEirNumGateInRet
                End If

                drgateIn.Item(GateinData.VHCL_NO) = dr.Item(GateinData.VHCL_NO)
                drgateIn.Item(GateinData.AGNT_ID) = dr.Item(GateinData.AGNT_ID)
                drgateIn.Item(GateinData.CNSGNE) = dr.Item(GateinData.CNSGNE)
                drgateIn.Item(GateinData.RDL_ATH) = dr.Item(GateinData.RDL_ATH)
                drgateIn.Item(GateinData.BLL_ID) = dr.Item(GateinData.BLL_ID)
                drgateIn.Item(GateinData.GRD_ID) = dr.Item(GateinData.GRD_ID)
                drgateIn.Item(GateinData.TRNSPRTR_CD) = dr.Item(GateinData.TRNSPRTR_CD)
                drgateIn.Item(GateinData.HTNG_BT) = dr.Item(GateinData.HTNG_BT)
                drgateIn.Item(GateinData.RMRKS_VC) = dr.Item(GateinData.RMRKS_VC)
                drgateIn.Item(GateinData.RNTL_BT) = dr.Item(GateinData.RNTL_BT)
                drgateIn.Item(GateinData.ALLOW_RENTAL) = dr.Item(GateinData.ALLOW_RENTAL)
                If Not (dr.Item(GateinData.CSTMR_NAM) Is DBNull.Value) Then
                    drgateIn.Item(GateinData.CSTMR_ID) = dr.Item(GateinData.CSTMR_ID)
                    drgateIn.Item(GateinData.CSTMR_NAM) = dr.Item(GateinData.CSTMR_NAM)
                    drgateIn.Item(GateinData.CSTMR_CD) = dr.Item(GateinData.CSTMR_CD)

                Else
                    For Each drCstmrDetail As DataRow In dsGateInData.Tables(GateinData._CUSTOMER).Select(String.Concat(GateinData.CSTMR_CD, "='", _
                                                                                                dr.Item(GateinData.CSTMR_CD), "'"))
                        drgateIn.Item(GateinData.CSTMR_NAM) = drCstmrDetail.Item(GateinData.CSTMR_NAM)
                        drgateIn.Item(GateinData.CSTMR_CD) = dr.Item(GateinData.CSTMR_CD)
                        drgateIn.Item(GateinData.CSTMR_ID) = dr.Item(GateinData.CSTMR_ID)
                        Exit For
                    Next
                End If

                If Not (dr.Item(GateinData.EQPMNT_STTS_DSCRPTN_VC) Is DBNull.Value) Then
                    drgateIn.Item(GateinData.EQPMNT_STTS_DSCRPTN_VC) = dr.Item(GateinData.EQPMNT_STTS_DSCRPTN_VC)
                Else
                    For Each drEqpStatus As DataRow In dsGateInData.Tables(GateinData._EQUIPMENT_STATUS).Select(String.Concat(GateinData.EQPMNT_STTS_CD, "='", _
                                                                                dr.Item(GateinData.EQPMNT_STTS_CD), "'"))
                        drgateIn.Item(GateinData.EQPMNT_STTS_DSCRPTN_VC) = drEqpStatus.Item(GateinData.EQPMNT_STTS_DSCRPTN_VC)
                        Exit For
                    Next
                    drgateIn.Item(GateinData.CHECKED) = dr.Item(GateinData.CHECKED)
                End If

                dtGateInDocument.Rows.Add(drgateIn)
            Next
            dsGateInData.Tables(GateinData._V_GATEIN).Clear()
            dsGateInData.Tables(GateinData._V_GATEIN).Merge(dtGateInDocument)
            'End If

            If strLockingRecords <> Nothing Then
                Dim intCheckedRows As Integer = 0
                Dim strSplitLockingRecords() As String = Nothing
                Dim intSplitActivity As Integer = 0
                intCheckedRows = CInt(dsGateInData.Tables(GateinData._V_GATEIN).Compute(String.Concat("COUNT(", GateinData.GTN_ID, ")"), String.Concat(GateinData.CHECKED, "= 'True'")))
                strSplitLockingRecords = strLockingRecords.Split(CChar(","))
                intSplitActivity = strSplitLockingRecords.Length

                If intCheckedRows = intSplitActivity Then
                    pub_SetCallbackReturnValue("LockRecordBit", "true")
                Else
                    pub_SetCallbackReturnValue("LockRecordBit", "false")
                End If
                pub_SetCallbackReturnValue("LockRecord", strLockingRecords)
            End If

            dsGateInData.AcceptChanges()
            dsDepot = objCommonUI.pub_GetDepoDetail(intDPT_ID)
            dsGateInData.Merge(dsDepot.Tables(CommonUIData._DEPOT))
            dsGateInData.AcceptChanges()
            CacheData(GATE_IN, dsGateInData)
            CacheData(GATE_IN_DOCUMENT, dsGateInData)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            CacheData("DeleteFlag", Nothing)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_ClientBind"
    Protected Sub ifgEquipmentDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentDetail.ClientBind
        Try
            If Not e.Parameters("Mode") Is Nothing Then
                Dim objCommon As New CommonData
                Dim strCurrentSessionId As String = objCommon.GetSessionID()
                objCommon.FlushLockDataByActivity(strCurrentSessionId, "Gate In")
                Dim intDepotID As Integer = CInt(objCommon.GetDepotID())

                hdnMode.Value = e.Parameters("Mode").ToString()
                CacheData("GateInMode", e.Parameters("Mode").ToString())
                Select Case e.Parameters("Mode").ToString()
                    Case strNew
                        Dim dtPreaAdvice As DataTable
                        Dim objGateIn As New Gatein
                        Dim dsEqpStatus As New DataSet

                        dsGateInData = objGateIn.GetGateInPreAdviceDetail(intDepotID)
                        dtPreaAdvice = dsGateInData.Tables(GateinData._V_GATEIN)

                        Dim objCommonUI As New CommonUI()

                        Dim dsEqpmntTyp As New DataSet
                        'Dim dsEqpmntCode As New DataSet
                        Dim dtEquipmentType As New DataTable
                        Dim objCommonConfig As New ConfigSetting()
                        Dim blnShowEqStatus As Boolean = False



                        str_006YardLocation = objCommon.GetYardLocation()


                        str_008EIRTime = objCommonConfig.pub_GetConfigSingleValue("008", intDepotID)
                        bln_008EIRTime_Key = objCommonConfig.IsKeyExists

                        str_009EqType = objCommonConfig.pub_GetConfigSingleValue("009", intDepotID)
                        bln_009EqType_Key = objCommonConfig.IsKeyExists

                        'str_010EqCode = objCommonConfig.pub_GetConfigSingleValue("010", intDepotID)
                        'bln_010EqCode_Key = objCommonConfig.IsKeyExists



                       
                        Dim bln_051GwsBit As Boolean
                        Dim str_051GWSKey As String
                        str_051GWSKey = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
                        bln_051GwsBit = objCommonConfig.IsKeyExists
                        If bln_051GwsBit Then
                            If str_051GWSKey.ToLower = "true" Then
                                dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("GateIn-GWS", True, intDepotID)
                                dsEqpmntTyp = objCommonUI.pub_GetEquipmentType(str_009EqType, intDepotID)
                                dtEquipmentType = objCommonUI.GetAllEquipmentCode(intDepotID)
                            Else
                                If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                                    dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate In", True, CInt(objCommon.GetHeadQuarterID()))
                                    dsEqpmntTyp = objCommonUI.pub_GetEquipmentType(str_009EqType, CInt(objCommon.GetHeadQuarterID()))
                                    ifgEquipmentDetail.Columns.Item(4).IsEditable = True
                                    dtEquipmentType = objCommonUI.GetAllEquipmentCode(CInt(objCommon.GetHeadQuarterID()))
                                Else
                                    dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate In", True, intDepotID)
                                    dsEqpmntTyp = objCommonUI.pub_GetEquipmentType(str_009EqType, intDepotID)
                                    ifgEquipmentDetail.Columns.Item(4).IsEditable = False
                                    dtEquipmentType = objCommonUI.GetAllEquipmentCode(intDepotID)
                                End If

                            End If
                        End If
                        If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                            blnShowEqStatus = True
                        End If
                        For Each drPreAdvice As DataRow In dtPreaAdvice.Rows
                            drPreAdvice.Item(GateinData.GTN_ID) = CommonWeb.GetNextIndex(dsGateInData.Tables(GateinData._V_GATEIN), GateinData.GTN_ID)


                            drPreAdvice.Item(GateinData.YRD_LCTN) = str_006YardLocation

                            If bln_008EIRTime_Key Then
                                drPreAdvice.Item(GateinData.GTN_TM) = str_008EIRTime
                            End If
                            If bln_051GwsBit Then
                                If str_051GWSKey.ToLower = "true" Then
                                    drPreAdvice.Item(GateinData.GTN_TM) = DateTime.Now.ToString("H:mm").ToUpper
                                End If
                            End If

                            If blnShowEqStatus Then
                                drPreAdvice.Item(GateinData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS)
                                drPreAdvice.Item(GateinData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                            End If
                            'Type & Code Merge
                            'If bln_010EqCode_Key Then
                            '    If Not str_010EqCode = "" Then
                            '        If dsEqpmntCode.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then  ' Type & code Merge
                            '            'drPreAdvice.Item(GateinData.EQPMNT_CD_ID) = dsEqpmntCode.Tables(CommonUIData._EQUIPMENT_CODE).Rows(0).Item(CommonUIData.EQPMNT_CD_ID).ToString
                            '            drPreAdvice.Item(GateinData.EQPMNT_CD_CD) = str_010EqCode
                            '        End If
                            '    End If
                            'End If

                            If Not dtEquipmentType Is Nothing AndAlso dtEquipmentType.Rows.Count > 0 Then

                                If dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drPreAdvice.Item(GateinData.EQPMNT_TYP_ID))).Length > 0 Then
                                    Dim dr() As DataRow = dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drPreAdvice.Item(GateinData.EQPMNT_TYP_ID)))

                                    drPreAdvice.Item(GateinData.EQPMNT_CD_ID) = dr(0).Item(GateinData.EQPMNT_TYP_ID)
                                    drPreAdvice.Item(GateinData.EQPMNT_CD_CD) = dr(0).Item(GateinData.EQPMNT_CD_CD)
                                End If
                            End If
                            drPreAdvice.Item(GateinData.PR_ADVC_BT) = True
                            drPreAdvice.Item(GateinData.GTN_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                            hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
                        Next

                        If dsGateInData.Tables(GateinData._V_GATEIN).Rows.Count = 0 Then
                            Dim drGateIn As DataRow = dsGateInData.Tables(GateinData._V_GATEIN).NewRow()
                            drGateIn.Item(GateinData.YRD_LCTN) = str_006YardLocation
                            If bln_008EIRTime_Key Or bln_009EqType_Key Then
                                If blnShowEqStatus Then
                                    drGateIn.Item(GateinData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS)
                                    drGateIn.Item(GateinData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                                    drGateIn.Item(GateinData.GTN_ID) = CommonWeb.GetNextIndex(dsGateInData.Tables(GateinData._V_GATEIN), GateinData.GTN_ID)
                                    drGateIn.Item(GateinData.CHECKED) = True
                                End If
                               
                                If bln_008EIRTime_Key Then
                                    drGateIn.Item(GateinData.GTN_TM) = str_008EIRTime
                                End If
                                If bln_051GwsBit Then
                                    If str_051GWSKey.ToLower = "true" Then
                                        drGateIn.Item(GateinData.GTN_TM) = DateTime.Now.ToString("H:mm").ToUpper
                                    End If
                                End If
                                If Not str_009EqType = "" Then
                                    If dsEqpmntTyp.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                                        drGateIn.Item(GateinData.EQPMNT_TYP_ID) = dsEqpmntTyp.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                                        drGateIn.Item(GateinData.EQPMNT_TYP_CD) = str_009EqType
                                    End If
                                End If
                                'Type & Code Merge
                                'If Not str_010EqCode = "" Then
                                '    dsEqpmntCode = objCommonUI.pub_GetEquipmentCode(str_010EqCode, intDepotID)
                                '    If dsEqpmntCode.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then ' Type & code Merge
                                '        'drGateIn.Item(GateinData.EQPMNT_CD_ID) = dsEqpmntCode.Tables(CommonUIData._EQUIPMENT_CODE).Rows(0).Item(CommonUIData.EQPMNT_CD_ID).ToString
                                '        drGateIn.Item(GateinData.EQPMNT_CD_CD) = str_010EqCode
                                '    End If
                                'End If

                                If Not dtEquipmentType Is Nothing AndAlso dtEquipmentType.Rows.Count > 0 Then

                                    If dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drGateIn.Item(GateinData.EQPMNT_TYP_ID))).Length > 0 Then
                                        Dim dr() As DataRow = dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drGateIn.Item(GateinData.EQPMNT_TYP_ID)))

                                        drGateIn.Item(GateinData.EQPMNT_CD_ID) = dr(0).Item(GateinData.EQPMNT_TYP_ID)
                                        drGateIn.Item(GateinData.EQPMNT_CD_CD) = dr(0).Item(GateinData.EQPMNT_CD_CD)
                                    End If
                                End If
                                drGateIn.Item(GateinData.PR_ADVC_BT) = False
                                drGateIn.Item(GateinData.GTN_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                                '  drGateIn.Item(GateinData.GTN_TM) = DateTime.Now.ToString("H:mm")
                                hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
                                dsGateInData.Tables(GateinData._V_GATEIN).Rows.Add(drGateIn)
                            End If
                        End If

                        If bln_051GwsBit Then
                            If str_051GWSKey.ToLower = "true" Then
                                'ifgEquipmentDetail.Columns.Item(8).ControlStyle.CssClass = "hide"
                                'ifgEquipmentDetail.Columns.Item(8).ItemStyle.CssClass = "hide"
                                'ifgEquipmentDetail.Columns.Item(8).HeaderStyle.CssClass = "hide"

                                ifgEquipmentDetail.Columns.Item(8).Visible = False
                                ifgEquipmentDetail.Columns.Item(9).ControlStyle.CssClass = "hide"
                                ifgEquipmentDetail.Columns.Item(9).ItemStyle.CssClass = "hide"
                                ifgEquipmentDetail.Columns.Item(9).HeaderStyle.CssClass = "hide"
                                ifgEquipmentDetail.Columns.Item(16).Visible = False
                                'ifgEquipmentDetail.Columns.Item(16).ControlStyle.CssClass = "hide"
                                'ifgEquipmentDetail.Columns.Item(16).ItemStyle.CssClass = "hide"
                                'ifgEquipmentDetail.Columns.Item(16).HeaderStyle.CssClass = "hide"
                                'CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                                '' CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                                'CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                                'CType(e.Row.Cells(16), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                                'CType(e.Row.Cells(24), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                            Else
                                ifgEquipmentDetail.Columns.Item(4).IsEditable = False
                            End If
                        End If
                        ifgEquipmentDetail.AllowAdd = True
                        ifgEquipmentDetail.AllowDelete = True
                    Case strEdit

                        Dim objCommonUI As New CommonUI()
                        Dim objGateIn As New Gatein
                        Dim dsStatusID As New DataSet
                        Dim dsEqpmntTyp As New DataSet
                        Dim dsEqpmntCode As New DataSet
                        Dim objCommonConfig As New ConfigSetting()


                        str_005EqStatus = objCommonConfig.pub_GetConfigSingleValue("005", intDepotID)
                        bln_005EqStatus_Key = objCommonConfig.IsKeyExists

                        str_006YardLocation = objCommon.GetYardLocation()


                        str_008EIRTime = objCommonConfig.pub_GetConfigSingleValue("008", intDepotID)
                        bln_008EIRTime_Key = objCommonConfig.IsKeyExists

                        str_009EqType = objCommonConfig.pub_GetConfigSingleValue("009", intDepotID)
                        bln_009EqType_Key = objCommonConfig.IsKeyExists

                        'str_010EqCode = objCommonConfig.pub_GetConfigSingleValue("010", intDepotID)
                        'bln_010EqCode_Key = objCommonConfig.IsKeyExists
                        dsGateInData = objGateIn.pub_GetGateIn(intDepotID)
                        ifgEquipmentDetail.Columns.Item(0).IsEditable = False
                        ifgEquipmentDetail.Columns.Item(1).IsEditable = False
                        ifgEquipmentDetail.Columns.Item(2).IsEditable = False
                        ifgEquipmentDetail.Columns.Item(3).IsEditable = False
                        ifgEquipmentDetail.Columns.Item(4).IsEditable = False

                        Dim bln_051GwsBit As Boolean
                        Dim str_051GWSKey As String
                        str_051GWSKey = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
                        bln_051GwsBit = objCommonConfig.IsKeyExists
                        If bln_051GwsBit Then
                            If str_051GWSKey.ToLower = "true" Then
                                'ifgEquipmentDetail.Columns.Item(8).ControlStyle.CssClass = "hide"
                                'ifgEquipmentDetail.Columns.Item(8).ItemStyle.CssClass = "hide"
                                'ifgEquipmentDetail.Columns.Item(8).HeaderStyle.CssClass = "hide"
                                ifgEquipmentDetail.Columns.Item(8).Visible = False
                                ifgEquipmentDetail.Columns.Item(9).ControlStyle.CssClass = "show"
                                ifgEquipmentDetail.Columns.Item(9).ItemStyle.CssClass = "show"
                                'ifgEquipmentDetail.Columns.Item(9).HeaderStyle.CssClass = "show"
                                ifgEquipmentDetail.Columns.Item(9).IsEditable = False
                                'ifgEquipmentDetail.Columns.Item(16).ControlStyle.CssClass = "hide"
                                'ifgEquipmentDetail.Columns.Item(16).ItemStyle.CssClass = "hide"
                                'ifgEquipmentDetail.Columns.Item(16).HeaderStyle.CssClass = "hide"
                                ifgEquipmentDetail.Columns.Item(16).Visible = False
                                'CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                                '' CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                                'CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                                'CType(e.Row.Cells(16), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                                'CType(e.Row.Cells(24), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                            End If
                        Else
                            ifgEquipmentDetail.Columns.Item(4).IsEditable = False
                        End If
                        ifgEquipmentDetail.AllowAdd = False
                        ifgEquipmentDetail.AllowDelete = False
                End Select
                If Not e.Parameters("AttchMode") Is Nothing Then
                    If e.Parameters("AttchMode").ToString() = "ReBind" Then
                        dsGateInData = CType(pub_RetrieveData(GATE_IN), GateinDataSet)
                        Dim lngGateinId As Long = 0
                        Dim intFilesCount As Integer = 0
                        Dim drGateIn1 As DataRow() = Nothing
                        If Not e.Parameters("RepairEstimateId") Is Nothing Then
                            lngGateinId = CLng(e.Parameters("RepairEstimateId"))
                            drGateIn1 = dsGateInData.Tables(GateinData._V_GATEIN).Select(String.Concat(GateinData.GTN_ID, " = ", lngGateinId))
                            If drGateIn1.Length > 0 Then
                                intFilesCount = CInt(dsGateInData.Tables(GateinData._ATTACHMENT).Compute(String.Concat("COUNT(", RepairCompletionData.RPR_ESTMT_ID, ")"), String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = '", lngGateinId, "'")))
                                drGateIn1(0).Item(GateinData.COUNT_ATTACH) = intFilesCount
                            End If
                            CacheData("AttachmentClear", intFilesCount)
                        End If
                    End If
                End If

                Dim objCommondata As New CommonData


                ifgEquipmentDetail.AllowSearch = True
                ifgEquipmentDetail.AllowRefresh = True
                ifgEquipmentDetail.ShowPageSizer = True
                ifgEquipmentDetail.ShowEmptyPager = True
                e.DataSource = dsGateInData.Tables(GateinData._V_GATEIN)
                CacheData(GATE_IN, dsGateInData)
                pub_CacheData(GATE_IN, dsGateInData)
                CacheData("GateInMode", e.Parameters("Mode").ToString())
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_RowDataBound"
    Protected Sub ifgEquipmentDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentDetail.RowDataBound
        Try
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
            Dim bln_051GwsBit As Boolean
            Dim str_051GWSKey As String
            str_051GWSKey = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
            bln_051GwsBit = objCommonConfig.IsKeyExists
            strMode = CType(RetrieveData(GateInMode), String)
            Dim strGateInMode As String = CStr(RetrieveData("GateInMode"))
            Select Case strGateInMode
                Case strEdit
                    ifgEquipmentDetail.Columns.Item(9).ControlStyle.CssClass = "show"
                    ifgEquipmentDetail.Columns.Item(9).ItemStyle.CssClass = "show"
                    'ifgEquipmentDetail.Columns.Item(9).HeaderStyle.CssClass = "show"
            End Select
            Dim strEquipmentNo As String = String.Empty
            If e.Row.RowType = DataControlRowType.Header Then

                str_007EIRNo = objCommonConfig.pub_GetConfigSingleValue("007", intDepotID)
                bln_007EIRNo_Key = objCommonConfig.IsKeyExists

                If bln_007EIRNo_Key Then
                    CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = str_007EIRNo
                End If
                str_022RentalBit = objCommonConfig.pub_GetConfigSingleValue("022", intDepotID)
                bln_022RentalBit_Key = objCommonConfig.IsKeyExists

                If bln_022RentalBit_Key Then
                    If str_022RentalBit.ToLower = "false" OrElse objCommondata.GetDepotID <> objCommondata.GetHeadQuarterID Then
                        ifgEquipmentDetail.Columns.Item(23).ControlStyle.CssClass = "hide"
                        ifgEquipmentDetail.Columns.Item(23).ItemStyle.CssClass = "hide"
                        ifgEquipmentDetail.Columns.Item(23).HeaderStyle.CssClass = "hide"
                        ''CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        'CType(e.Row.Cells(18), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    Else
                        CType(e.Row.Cells(23), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(23), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = True
                        '  CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                        'CType(e.Row.Cells(18), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                    End If
                End If

                If bln_051GwsBit Then
                    If str_051GWSKey.ToLower = "true" Then
                        'ifgEquipmentDetail.Columns.Item(8).ControlStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(8).ItemStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(8).HeaderStyle.CssClass = "hide"
                        ifgEquipmentDetail.Columns.Item(8).Visible = False
                        ifgEquipmentDetail.Columns.Item(11).IsEditable = True
                        ifgEquipmentDetail.Columns.Item(9).IsEditable = False
                        CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        'ifgEquipmentDetail.Columns.Item(9).ControlStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(9).ItemStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(9).HeaderStyle.CssClass = "hide"

                        'ifgEquipmentDetail.Columns.Item(16).ControlStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(16).ItemStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(16).HeaderStyle.CssClass = "hide"
                        ifgEquipmentDetail.Columns.Item(16).Visible = False
                        'CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        '' CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        'CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        'CType(e.Row.Cells(16), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        'CType(e.Row.Cells(24), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    Else
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 9, "true")
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 10, "false")
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 17, "false")
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 11, "false")
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 12, "false")
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 13, "false")

                        ifgEquipmentDetail.Columns.Item(9).IsEditable = True
                        ifgEquipmentDetail.Columns.Item(10).Visible = False
                        ifgEquipmentDetail.Columns.Item(11).Visible = False
                        ifgEquipmentDetail.Columns.Item(12).Visible = False
                        ifgEquipmentDetail.Columns.Item(13).Visible = False
                        Dim lkpPrevCargo As iLookup
                        lkpPrevCargo = CType(DirectCast(DirectCast(e.Row.Cells(8),  _
                            iFgFieldCell).ContainingField,  _
                            LookupField).Lookup, iLookup)
                        lkpPrevCargo.Validator.IsRequired = False
                        lkpPrevCargo.Validator.ReqErrorMessage = "Previous Cargo Required"
                        Dim lkpGrade As iLookup
                        lkpGrade = CType(DirectCast(DirectCast(e.Row.Cells(17),  _
                            iFgFieldCell).ContainingField,  _
                            LookupField).Lookup, iLookup)
                        lkpGrade.Validator.CustomValidation = False
                        lkpGrade.Validator.CustomValidationFunction = ""
                        ifgEquipmentDetail.Columns.Item(17).Visible = False
                    End If
                End If
            End If
            Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim datControl As iDate
                Dim imgLink As Image
                If Not e.Row.DataItem Is Nothing Then
                    ' Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        datControl = CType(DirectCast(DirectCast(e.Row.Cells(6),  _
                                    iFgFieldCell).ContainingField,  _
                                    DateField).iDate, iDate)
                        datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                        datControl.Validator.RangeValidation = True
                        datControl.Validator.RngMaximumValue = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                    End If
                    If drv.Row.RowState = DataRowState.Added Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        'CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                    Else
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True

                        'CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        'CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False
                        'CType(e.Row.Cells(18), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        'CType(e.Row.Cells(18), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False
                    End If
                    If Not drv.Item(GateinData.RNTL_BT) Is DBNull.Value Then
                        If drv.Row.RowState = DataRowState.Added AndAlso CBool(drv.Item(GateinData.RNTL_BT)) = True Then
                            CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        End If
                    End If
                    If Not drv.Item(GateinData.PR_ADVC_BT) Is DBNull.Value Then
                        If CBool(drv.Item(GateinData.PR_ADVC_BT)) = True Then
                            'CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                            'CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = True
                            CType(e.Row.Cells(23), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                            CType(e.Row.Cells(23), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = True
                        End If
                    End If
                    imgLink = CType(e.Row.Cells(19).Controls(0), Image)
                    imgLink.ToolTip = "More Info"
                    imgLink.ImageUrl = "../Images/info.png"
                    strEquipmentNo = String.Empty
                    Dim strGateinTransactionNo As String = String.Empty
                    'Dim intHeatingCount As Integer
                    Dim strGateInID As String = String.Empty
                    If Not drv.Row.Item(GateinData.EQPMNT_NO) Is DBNull.Value Then
                        strEquipmentNo = CStr(drv.Row.Item(GateinData.EQPMNT_NO))
                    End If
                    If Not drv.Row.Item(GateinData.GTN_ID) Is DBNull.Value Then
                        strGateInID = CStr(drv.Row.Item(GateinData.GTN_ID))
                    End If
                    If Not drv.Row.Item(GateinData.GI_TRNSCTN_NO) Is DBNull.Value Then
                        strGateinTransactionNo = CStr(drv.Row.Item(GateinData.GI_TRNSCTN_NO))
                    End If
                    If Not drv.Row.Item(GateinData.HTNG_EDIT) Is DBNull.Value Then
                        If (CInt(drv.Row.Item(GateinData.HTNG_EDIT)) > 0) Then
                            CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            'CType(e.Row.Cells(11), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(13), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        End If
                    End If
                    '    drv.Row.Item(GateinData.GTN_TM) = DateTime.Now.ToString("H:mm")
                    imgLink.Attributes.Add("onclick", String.Concat("openMoreInfo('", strGateInID, "','", strEquipmentNo, "');return false;"))
                    imgLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                    imgLink = CType(e.Row.Cells(20).Controls(0), Image)

                    imgLink.ToolTip = "Equipment Info"
                    imgLink.ImageUrl = "../Images/einfo.png"
                    imgLink.Attributes.Add("onclick", String.Concat("openEquipmentInfo('", strGateInID, "','", strEquipmentNo, "','", strGateinTransactionNo, "');return false;"))
                    imgLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                    If Not IsDBNull(CType(e.Row.DataItem, Data.DataRowView).Row.Item(GateinData.PR_ADVC_BT)) Then
                        If CBool(CType(e.Row.DataItem, Data.DataRowView).Row.Item(GateinData.PR_ADVC_BT)) = True Then
                            CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        End If
                    End If
                    If bln_022RentalBit_Key Then
                        If str_022RentalBit.ToLower = "false" OrElse objCommondata.GetDepotID <> objCommondata.GetHeadQuarterID Then
                            ifgEquipmentDetail.Columns.Item(23).ControlStyle.CssClass = "hide"
                            ifgEquipmentDetail.Columns.Item(23).ItemStyle.CssClass = "hide"
                            ifgEquipmentDetail.Columns.Item(23).HeaderStyle.CssClass = "hide"
                            ''CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                            'CType(e.Row.Cells(18), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        ElseIf drv.Item(GateinData.PR_ADVC_BT) Is DBNull.Value Then
                            If drv.Item(GateinData.RNTL_BT) Is DBNull.Value OrElse strGateInMode = "edit" Then
                                CType(e.Row.Cells(23), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                                CType(e.Row.Cells(23), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False
                                'CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                                ' CType(e.Row.Cells(18), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                            End If
                        End If
                    End If
                End If

                Dim chk As iFgCheckBox
                '  chk = CType(e.Row.Cells(17).Controls(0), iFgCheckBox)
                chk = CType(e.Row.Cells(23).Controls(0), iFgCheckBox)
                chk.Attributes.Add("onclick", "validateRentalEntry(this);")

                Dim chkActive As iFgCheckBox
                'chkActive = CType(e.Row.Cells(16).Controls(0), iFgCheckBox)
                chkActive = CType(e.Row.Cells(22).Controls(0), iFgCheckBox)
                chkActive.Attributes.Add("onClick", String.Concat("GIlockData(this,'", strEquipmentNo, "');"))

                datControl = CType(DirectCast(DirectCast(e.Row.Cells(6), iFgFieldCell).ContainingField, 
                            DateField).iDate, iDate)
                datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                'If bln_007EIRNo_Key Then
                '    Dim txtEIRNO As iTextBox = CType(DirectCast(DirectCast(e.Row.Cells(9), iFgFieldCell).ContainingField, TextboxField).TextBox, iTextBox)
                '    txtEIRNO.HelpText = String.Concat("Enter ", str_007EIRNo)
                'End If
                'Added for Attchement
                Dim hypPhotoUpload As Image

                Dim GateInPreAdvice As String = drv(GateinData.PR_ADVC_ID).ToString
                Dim GateInId As String = drv(GateinData.GTN_ID).ToString
                hypPhotoUpload = CType(e.Row.Cells(21).Controls(0), Image)
                If GateInPreAdvice <> "" Then
                    'GateInId = GateInPreAdvice
                End If
                hypPhotoUpload.Attributes.Add("onclick", "showPhotoUpload('" + e.Row.RowIndex.ToString + "','" + GateInId + "','" + GateInPreAdvice + "' );return false;")
                hypPhotoUpload.ToolTip = "Attach Files"
                hypPhotoUpload.ImageUrl = "../Images/attachment.png"
                hypPhotoUpload.Attributes.Add("style", "cursor:pointer;margin-left:5px;")

                Dim imgFileUpload As Image
                imgFileUpload = CType(e.Row.Cells(21).Controls(0), Image)
                imgFileUpload.ToolTip = "Attach Files"
                If Not IsDBNull(drv.Item(GateinData.COUNT_ATTACH)) Then
                    If Not CInt(drv.Item(GateinData.COUNT_ATTACH)) > 0 Then
                        imgFileUpload.ImageUrl = "../Images/noattachment.png"
                    Else
                        imgFileUpload.ImageUrl = "../Images/attachment.png"
                    End If
                Else
                    imgFileUpload.ImageUrl = "../Images/noattachment.png"
                End If

                'Above Added for Attchement
                If e.Row.RowIndex > 5 Then
                    Dim lkpCustomer As iLookup
                    lkpCustomer = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpCustomer.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
                If e.Row.RowIndex > 5 Then
                    Dim lkpType As iLookup
                    lkpType = CType(DirectCast(DirectCast(e.Row.Cells(2), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpType.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
                If e.Row.RowIndex > 5 Then
                    Dim lkpCode As iLookup
                    lkpCode = CType(DirectCast(DirectCast(e.Row.Cells(3), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpCode.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
                If e.Row.RowIndex > 5 Then
                    Dim lkpStatus As iLookup
                    lkpStatus = CType(DirectCast(DirectCast(e.Row.Cells(4), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpStatus.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
                If e.Row.RowIndex > 5 Then
                    Dim lkpPreviousCargo As iLookup
                    lkpPreviousCargo = CType(DirectCast(DirectCast(e.Row.Cells(8), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpPreviousCargo.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
                str_051GWSKey = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
                bln_051GwsBit = objCommonConfig.IsKeyExists
                If bln_051GwsBit Then
                    If str_051GWSKey.ToLower = "true" Then
                        'ifgEquipmentDetail.Columns.Item(8).ControlStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(8).ItemStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(8).HeaderStyle.CssClass = "hide"
                        ifgEquipmentDetail.Columns.Item(8).Visible = False
                        ifgEquipmentDetail.Columns.Item(11).IsEditable = True
                        CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        ifgEquipmentDetail.Columns.Item(9).IsEditable = False
                        CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        'ifgEquipmentDetail.Columns.Item(9).ControlStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(9).ItemStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(9).HeaderStyle.CssClass = "hide"
                        If drv.Item(GateinData.BLL_CD).ToString = "CUSTOMER" AndAlso drv.Item(GateinData.AGNT_CD).ToString = "" Then
                            CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        End If
                        'ifgEquipmentDetail.Columns.Item(16).ControlStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(16).ItemStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(16).HeaderStyle.CssClass = "hide"
                        ifgEquipmentDetail.Columns.Item(16).Visible = False
                    Else
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 9, "true")
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 10, "false")
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 17, "false")
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 11, "false")
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 12, "false")
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 13, "false")
                        If Not drv.Row.RowState = DataRowState.Added Then
                            CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        End If
                        ifgEquipmentDetail.Columns.Item(9).IsEditable = True
                        ifgEquipmentDetail.Columns.Item(10).Visible = False
                        ifgEquipmentDetail.Columns.Item(11).Visible = False
                        ifgEquipmentDetail.Columns.Item(12).Visible = False
                        ifgEquipmentDetail.Columns.Item(13).Visible = False
                        'Dim lkpPrevCargo As iLookup
                        'lkpPrevCargo = CType(DirectCast(DirectCast(e.Row.Cells(8),  _
                        '    iFgFieldCell).ContainingField,  _
                        '    LookupField).Lookup, iLookup)
                        'lkpPrevCargo.Validator.IsRequired = False

                        Dim lkpGrade As iLookup
                        lkpGrade = CType(DirectCast(DirectCast(e.Row.Cells(17),  _
                            iFgFieldCell).ContainingField,  _
                            LookupField).Lookup, iLookup)
                        lkpGrade.Validator.CustomValidation = False
                        lkpGrade.Validator.CustomValidationFunction = ""
                        ifgEquipmentDetail.Columns.Item(17).Visible = False
                        'CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        '' CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        'CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        'CType(e.Row.Cells(16), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        'CType(e.Row.Cells(24), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    End If
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_RowInserting"
    Protected Sub ifgEquipmentDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgEquipmentDetail.RowInserting
        Try
            Dim lngGateInbin As Long
            dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
            lngGateInbin = CommonWeb.GetNextIndex(dsGateInData.Tables(GateinData._V_GATEIN), GateinData.GTN_ID)
            e.Values(GateinData.GTN_ID) = lngGateInbin
            If Not e.Values(GateinData.GTN_TM).ToString = "" Then
                Dim strInwardTime As String = CStr(e.Values(GateinData.GTN_TM))
                Dim strHours As String
                Dim strMinutes As String
                If strInwardTime.Length = 5 Then
                    strHours = strInwardTime.Substring(0, 2)
                    strMinutes = strInwardTime.Substring(3, 2)
                Else
                    strHours = strInwardTime.Substring(0, 1)
                    strMinutes = strInwardTime.Substring(2, 2)
                End If
                Dim datOldDatetime As DateTime = CDate(e.Values(GateinData.GTN_DT))
                Dim datNewDatetime As New DateTime(datOldDatetime.Year, datOldDatetime.Month, datOldDatetime.Day, CInt(strHours), CInt(strMinutes), 0)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_RowDeleting"
    Protected Sub ifgEquipmentDetail_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgEquipmentDetail.RowDeleting
        Try
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgEquipmentDetail.PageSize * ifgEquipmentDetail.PageIndex + e.RowIndex
            dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
            Dim dtGateIn As Data.DataTable = dsGateInData.Tables(GateinData._V_GATEIN).Copy
            If CType(ifgEquipmentDetail.DataSource, DataTable).Select(String.Concat(GateinData.GTN_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                e.OutputParamters("Delete") = String.Concat("Gate In cannot be deleted as Pre-Advice exist for this Equipment : ", dtGateIn.Rows(intRowIndex).Item(GateinData.EQPMNT_NO).ToString)
                Exit Sub
            End If
            ''Lock Implementation
            If pvt_CheckLock(dtGateIn.Rows(intRowIndex).Item(GateinData.EQPMNT_NO).ToString) <> "" Then
                e.Cancel = True
                e.OutputParamters("Delete") = pvt_CheckLock(dtGateIn.Rows(intRowIndex).Item(GateinData.EQPMNT_NO).ToString)
                Exit Sub
            Else
                '' remove lock done by delete lock check
                Dim objCommonData As New CommonData
                Dim strCurrentSessionId As String = objCommonData.GetSessionID()
                objCommonData.FlushLockData(GateinData.EQPMNT_NO, dtGateIn.Rows(intRowIndex).Item(GateinData.EQPMNT_NO).ToString, strCurrentSessionId, "Gate In")
            End If
            ''
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_RowDeleted"
    Protected Sub ifgEquipmentDetail_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgEquipmentDetail.RowDeleted
        Try
            Dim objGateIn As New Gatein
            Dim objcommon As New CommonData()

            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
            Dim objCommonUI As New CommonUI()

            Dim dsEqpStatus As New DataSet
            Dim dsEqpmntTyp As New DataSet
            'Dim dsEqpmntCode As New DataSet
            Dim objCommonConfig As New ConfigSetting()
            Dim blnShowEqStatus As Boolean = False

            dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate In", True, intDepotID)

            str_006YardLocation = objcommon.GetYardLocation()
            str_008EIRTime = objCommonConfig.pub_GetConfigSingleValue("008", intDepotID)
            bln_008EIRTime_Key = objCommonConfig.IsKeyExists

            str_009EqType = objCommonConfig.pub_GetConfigSingleValue("009", intDepotID)
            bln_009EqType_Key = objCommonConfig.IsKeyExists

            'Type & Code Merge
            'str_010EqCode = objCommonConfig.pub_GetConfigSingleValue("010", intDepotID)
            'bln_010EqCode_Key = objCommonConfig.IsKeyExists

            'dsEqpmntCode = objCommonUI.pub_GetEquipmentCode(str_010EqCode, intDepotID)

            Dim dtEquipmentType As New DataTable
            dtEquipmentType = objCommonUI.GetAllEquipmentCode(intDepotID)

            If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                blnShowEqStatus = True
            End If

            If dsGateInData.Tables(GateinData._V_GATEIN).Rows.Count = 0 Then
                Dim drGateIn As DataRow = dsGateInData.Tables(GateinData._V_GATEIN).NewRow()
                drGateIn.Item(GateinData.YRD_LCTN) = str_006YardLocation
                If bln_008EIRTime_Key Or bln_009EqType_Key Then
                    If blnShowEqStatus Then
                        drGateIn.Item(GateinData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS)
                        drGateIn.Item(GateinData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                        drGateIn.Item(GateinData.GTN_ID) = CommonWeb.GetNextIndex(dsGateInData.Tables(GateinData._V_GATEIN), GateinData.GTN_ID)
                        drGateIn.Item(GateinData.CHECKED) = True
                    End If
                    
                    If bln_008EIRTime_Key Then
                        drGateIn.Item(GateinData.GTN_TM) = str_008EIRTime
                    End If
                    If Not str_009EqType = "" Then
                        dsEqpmntTyp = objCommonUI.pub_GetEquipmentType(str_009EqType, intDepotID)
                        If dsEqpmntTyp.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                            drGateIn.Item(GateinData.EQPMNT_TYP_ID) = dsEqpmntTyp.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                            drGateIn.Item(GateinData.EQPMNT_TYP_CD) = str_009EqType
                        End If
                    End If
                    'Type & Code Merge
                    'If Not str_010EqCode = "" Then
                    '    dsEqpmntCode = objCommonUI.pub_GetEquipmentCode(str_010EqCode, intDepotID)
                    '    If dsEqpmntCode.Tables(CommonUIData._EQUIPMENT_CODE).Rows.Count > 0 Then
                    '        ' drGateIn.Item(GateinData.EQPMNT_CD_ID) = dsEqpmntCode.Tables(CommonUIData._EQUIPMENT_CODE).Rows(0).Item(CommonUIData.EQPMNT_CD_ID).ToString
                    '        drGateIn.Item(GateinData.EQPMNT_CD_CD) = str_010EqCode
                    '    End If
                    'End If

                    If Not dtEquipmentType Is Nothing AndAlso dtEquipmentType.Rows.Count > 0 Then

                        If dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drGateIn.Item(GateinData.EQPMNT_TYP_ID))).Length > 0 Then
                            Dim dr() As DataRow = dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drGateIn.Item(GateinData.EQPMNT_TYP_ID)))

                            drGateIn.Item(GateinData.EQPMNT_CD_ID) = dr(0).Item(GateinData.EQPMNT_TYP_ID)
                            drGateIn.Item(GateinData.EQPMNT_CD_CD) = dr(0).Item(GateinData.EQPMNT_CD_CD)
                        End If
                    End If
                    dsGateInData.Tables(GateinData._V_GATEIN).Rows.Add(drGateIn)
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub

#End Region

#Region "pvt_ValidatePreviousActivityDate"
    Private Sub pvt_ValidatePreviousActivityDate(ByVal bv_strEquipmentNo As String, ByVal bv_strEventDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objGateIn As New Gatein
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim dsCommonUI As New CommonUIDataSet

            If bv_strEquipmentNo <> Nothing And bv_strEventDate <> Nothing Then
                blnDateValid = objCommonUI.pub_GetEquipmentPreviousActivityDate(bv_strEquipmentNo, _
                                                                         CDate(bv_strEventDate), _
                                                                         dtPreviousDate, _
                                                                         "Gate In", _
                                                                         CInt(objCommon.GetDepotID()))
                If blnDateValid = True Then
                    pub_SetCallbackReturnValue("Error", String.Concat("Equipment's Activity Date Should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), ")"))
                End If
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "") 'Added By Sakthivel on 20 OCT 2014 for Date Issue in UIG
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_validateRentalEntry"
    Private Sub pvt_validateRentalEntry(ByVal bv_strEquipmentNo As String)
        Try
            Dim objGatein As New Gatein
            Dim objCommonData As New CommonData
            Dim intDepotId As Integer = CInt(objCommonData.GetDepotID())
            Dim blnRentalEntry As Boolean = False
            Dim blnGTOT_BT As Boolean = False
            blnRentalEntry = objGatein.pub_GetRentalEntry(bv_strEquipmentNo, intDepotId, blnGTOT_BT)
            If blnRentalEntry = False Or blnGTOT_BT = False Then
                pub_SetCallbackReturnValue("isRentalExist", CStr(False))
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackReturnValue("isRentalExist", CStr(True))
                pub_SetCallbackStatus(True)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_getSupplierDetails()"
    Private Sub pvt_getSupplierDetails(ByVal bv_strEquipmentNo As String)
        Try
            Dim objCommonData As New CommonData
            Dim dsSupplierDetails As New GateinDataSet
            Dim objGatein As New Gatein
            Dim intDepotId As Integer = CInt(objCommonData.GetDepotID())
            dsSupplierDetails = objGatein.pub_GetSupplierDetails(bv_strEquipmentNo, intDepotId)
            If dsSupplierDetails.Tables(GateinData._V_GATEOUT).Rows.Count > 0 Then
                pub_SetCallbackReturnValue("EquipmentTypeId", dsSupplierDetails.Tables(GateinData._V_GATEOUT).Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString)
                pub_SetCallbackReturnValue("EquipmentTypeCode", dsSupplierDetails.Tables(GateinData._V_GATEOUT).Rows(0).Item(GateinData.EQPMNT_TYP_CD).ToString)
                pub_SetCallbackReturnValue("EquipmentCodeId", dsSupplierDetails.Tables(GateinData._V_GATEOUT).Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString)
                pub_SetCallbackReturnValue("EquipmentCode", dsSupplierDetails.Tables(GateinData._V_GATEOUT).Rows(0).Item(GateinData.EQPMNT_CD_CD).ToString)
            Else
                pub_SetCallbackReturnValue("EquipmentTypeId", "")
                pub_SetCallbackReturnValue("EquipmentTypeCode", "")
                pub_SetCallbackReturnValue("EquipmentCodeId", "")
                pub_SetCallbackReturnValue("EquipmentCode", "")
            End If
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "GateInLocking"
    Private Sub pvt_GIlockData(ByVal bv_strCheckBitFlag As String, _
                                ByVal bv_strEquipmentNo As String, _
                                ByRef LockBit As String)
        Try
            Dim objCommonData As New CommonData
            Dim objCommonUI As New CommonUI
            Dim strEquipmentStatus As String = ""
            Dim strCurrentEquipmentStatus As String = ""
            Dim strCurrentSessionId As String = String.Empty
            Dim strCurrentUserName As String = String.Empty
            Dim strCurrentIpAddress As String = String.Empty
            Dim strSessionId As String = String.Empty
            Dim strUserName As String = String.Empty
            Dim strIpAddress As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim intDepotID As Integer = CInt(objCommonData.GetDepotID())
            Dim strErrorMessage As String = ""
            Dim strActivity As String = ""
            strCurrentSessionId = objCommonData.GetSessionID()
            strCurrentUserName = objCommonData.GetCurrentUserName()
            strCurrentIpAddress = GetClientIPAddress()
            strMode = CType(RetrieveData(GateInMode), String)
            Dim str_051GWSBit As String
            Dim bln_051GWSBit_Key As Boolean
            str_051GWSBit = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
            bln_051GWSBit_Key = objCommonConfig.IsKeyExists

            If strMode = MODE_EDIT Then
                strEquipmentStatus = "IND"
            Else
                strEquipmentStatus = "OUT"
            End If
            If bln_051GWSBit_Key Then
                If str_051GWSBit.ToLower = "true" Then
                    strEquipmentStatus = "INS/STR"
                End If
            End If

            dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
            Dim dr() As DataRow = dsGateInData.Tables("V_GATEIN").Select(String.Concat(GateinData.EQPMNT_NO, "='", bv_strEquipmentNo, "'"))
            If dr.Length > 0 Then
                dr(0).Item(GateinData.CHECKED) = bv_strCheckBitFlag
            End If
            CacheData(GATE_IN, dsGateInData)
            LockBit = "FALSE"
            If bv_strCheckBitFlag.ToUpper = "TRUE" Then
                strCurrentEquipmentStatus = objCommonUI.pub_GetEquipmentStaus(bv_strEquipmentNo, intDepotID)
                blnLockData = objCommonData.StoreLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentUserName, strCurrentSessionId, "Gate In", strCurrentIpAddress, True)
                If strCurrentEquipmentStatus <> Nothing AndAlso Not strEquipmentStatus.Contains(strCurrentEquipmentStatus) Then
                    strErrorMessage = "GateIn already done for this equipment."
                Else
                    If blnLockData Then
                        LockBit = "TRUE"
                        ''Get Activity Name
                        Dim blnGetLock As Boolean = objCommonData.GetLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, strCurrentUserName, strActivity)                        ''
                        strErrorMessage = String.Concat("This record (", bv_strEquipmentNo, ") cannot be modified because it is already being used by <b>", strCurrentUserName, "</b> user ")
                        strSessionId = objCommonData.GetSessionID()
                        strUserName = objCommonData.GetCurrentUserName()
                        strIpAddress = GetClientIPAddress()
                        If strCurrentIpAddress <> strIpAddress Then
                            strErrorMessage = String.Concat(strErrorMessage, " in another place. ")
                        Else
                            strErrorMessage = String.Concat(strErrorMessage, " in another session. ")
                        End If
                    End If
                End If
            Else
                objCommonData.FlushLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, "Gate In")
            End If
            pub_SetCallbackReturnValue("ErrorMessage", strErrorMessage)
            pub_SetCallbackReturnValue("Activity", strActivity)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_CheckLock()"
    Private Function pvt_CheckLock(ByVal bv_strEquipmentNo As String) As String

        Try
            Dim strCurrentEquipmentStatus As String = ""
            Dim blnLockData As Boolean = False
            Dim strErrorMessage As String = ""
            Dim strEquipmentStatus As String = ""
            Dim strSessionId As String = String.Empty
            Dim strUserName As String = String.Empty
            Dim strIpAddress As String = String.Empty
            Dim objCommonData As New CommonData
            Dim objCommonUI As New CommonUI
            Dim strCurrentSessionId As String = objCommonData.GetSessionID()
            Dim strCurrentUserName As String = objCommonData.GetCurrentUserName()
            Dim strCurrentIpAddress As String = GetClientIPAddress()
            Dim intDepotID As Integer = CInt(objCommonData.GetDepotID())
            strMode = CType(RetrieveData(GateInMode), String)
            Dim strActivity As String = ""
            If strMode = MODE_EDIT Then
                strEquipmentStatus = "IND"
            Else
                strEquipmentStatus = "OUT"
            End If

            strCurrentEquipmentStatus = objCommonUI.pub_GetEquipmentStaus(bv_strEquipmentNo, intDepotID)
            blnLockData = objCommonData.StoreLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentUserName, strCurrentSessionId, "Gate In", strCurrentIpAddress, True)
            If strCurrentEquipmentStatus <> strEquipmentStatus AndAlso strCurrentEquipmentStatus <> Nothing Then
                strErrorMessage = String.Concat("This equipment(", bv_strEquipmentNo, ") is already submitted by another user.")
            Else
                If blnLockData Then
                    ''Get Activity Name
                    Dim blnGetLock As Boolean = objCommonData.GetLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, strCurrentUserName, strActivity)                        ''
                    strErrorMessage = String.Concat("This record (", bv_strEquipmentNo, ") cannot be modified because it is already being used by <b>", strCurrentUserName, "</b>  user ")
                    strSessionId = objCommonData.GetSessionID()
                    strUserName = objCommonData.GetCurrentUserName()
                    strIpAddress = GetClientIPAddress()
                    If strCurrentIpAddress <> strIpAddress Then
                        strErrorMessage = String.Concat(strErrorMessage, " in another place. ")
                    Else
                        strErrorMessage = String.Concat(strErrorMessage, " in another session. ")
                    End If
                End If
            End If
            '' only if equipment blocked error message is mandatory 27-OCT-2014 issue found during functionality check
            If strErrorMessage <> "" Then
                strErrorMessage = String.Concat(strErrorMessage, " ( Activity :", strActivity, ")")
            End If
            Return strErrorMessage
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "pvt_ValidateGateINAttachment"
    Private Sub pvt_ValidateGateINAttachment(ByVal bv_strGateINPreAdviceID As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objGateIn As New Gatein
            Dim dsGateOutAttchemnt As GateinDataSet
            dsGateOutAttchemnt = objGateIn.pub_GetAttchemntbyGateIN(CInt(bv_strGateINPreAdviceID), "Pre-Advice")
            If CInt(dsGateOutAttchemnt.Tables(GateinData._V_GATEIN).Rows(0).Item("COUNT_ATTACH")) > 0 Then
                pub_SetCallbackReturnValue("Message", "Yes")
            Else
                pub_SetCallbackReturnValue("Message", "No")
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub pvt_getAgentName(ByVal bv_strCustCode As String)
        Dim objGatein As New Gatein
        Dim strAgentname As String
        strAgentname = objGatein.GetAgentNameByCustmrId(bv_strCustCode)
        If strAgentname = "" OrElse strAgentname Is Nothing Then
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("AgentName", "")
            'pub_SetCallbackError("Please select Only Customer for this Equipment")
        Else
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("AgentName", strAgentname)
        End If
    End Sub

    Protected Sub ifgEquipmentDetail_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgEquipmentDetail.RowUpdating

    End Sub

    Protected Sub ifgEquipmentDetail_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgEquipmentDetail.RowUpdated


    End Sub

    Protected Sub ifgEquipmentDetail_RowCreated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentDetail.RowCreated
        Dim strGateInMode As String = CStr(RetrieveData("GateInMode"))
        Dim objCommondata As New CommonData
        Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
        Dim bln_051GwsBit As Boolean
        Dim str_051GWSKey As String
        str_051GWSKey = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
        bln_051GwsBit = objCommonConfig.IsKeyExists
        If bln_051GwsBit Then
            If str_051GWSKey.ToLower = "true" Then
                Select Case strGateInMode
                    Case strEdit
                        ifgEquipmentDetail.Columns.Item(9).ControlStyle.CssClass = "show"
                        ifgEquipmentDetail.Columns.Item(9).ItemStyle.CssClass = "show"
                        'ifgEquipmentDetail.Columns.Item(9).HeaderStyle.CssClass = "show"
                    Case strNew
                        ifgEquipmentDetail.Columns.Item(9).ControlStyle.CssClass = "hide"
                        ifgEquipmentDetail.Columns.Item(9).ItemStyle.CssClass = "hide"
                        ifgEquipmentDetail.Columns.Item(9).HeaderStyle.CssClass = "hide"
                End Select
            End If
        End If
    End Sub

    Protected Sub ifgEquipmentDetail_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgEquipmentDetail.RowInserted

    End Sub

#Region "pvt_GetYardLocation()"
    Private Sub pvt_GetYardLocation()
        Try
            Dim objCommon As New CommonData
            Dim strYardLocation As String
            strYardLocation = objCommon.GetYardLocation()
            pub_SetCallbackReturnValue("YardLocation", strYardLocation)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class