Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class GateOut

#Region "pub_GetCustomerDetail() TABLE NAME:CUSTOMER"
    <OperationContract()> _
    Public Function pub_GetCustomerDetail(ByVal bv_intDPT_ID As Integer) As GateOutDataSet
        Try
            Dim dsGateinDataSet As GateOutDataSet
            Dim objGateins As New GateOuts
            dsGateinDataSet = objGateins.pub_GetCustomerDetail(bv_intDPT_ID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_V_GateinDetail() TABLE NAME:V_GATEIN_DETAIL"

    <OperationContract()> _
    Public Function pub_V_GateinDetail(ByVal bv_intDepotID As Integer) As GateOutDataSet
        Try
            Dim dsVGateoutDetailData As GateOutDataSet
            Dim objVGateoutDetails As New GateOuts
            dsVGateoutDetailData = objVGateoutDetails.GetVGateinDetailBy(bv_intDepotID)
            Return dsVGateoutDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "getGateinId() "

    <OperationContract()> _
    Public Function getGateinId(ByVal transno As String) As GateOutDataSet

        Try
            Dim dsVGateoutDetailData As GateOutDataSet
            Dim objVGateoutDetails As New GateOuts
            dsVGateoutDetailData = objVGateoutDetails.getGateinId(transno)
            Return dsVGateoutDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_CreateHandlingCharge() TABLE NAME:Handling_Charge"
    Private Function pvt_CreateHandlingCharge(ByVal bv_strEQPMNT_NO As String, _
     ByVal bv_i64EQPMNT_CD_ID As Int64, _
     ByVal bv_i64EQPMNT_TYP_ID As Int64, _
     ByVal bv_strCstType As String, _
     ByVal bv_strRFRNC_EIR_NO_1 As String, _
     ByVal bv_strRFRNC_EIR_NO_2 As String, _
     ByVal bv_datFRM_BLLNG_DT As DateTime, _
     ByVal bv_datTO_BLLNG_DT As DateTime, _
     ByVal bv_i32FR_DYS As Int32, _
     ByVal bv_i32NO_OF_DYS As Int32, _
     ByVal bv_strHNDLNG_CST_NC As Decimal, _
     ByVal bv_strHNDLNG_TX_RT_NC As Decimal, _
     ByVal bv_strTTL_CSTS_NC As Decimal, _
     ByVal bv_strBLLNG_FLG As String, _
     ByVal bv_blnACTV_BT As Boolean, _
     ByVal bv_i32DPT_ID As Int32, _
     ByVal bv_strIS_GT_OT_FLG As String, _
     ByVal bv_strYRD_LCTN As String, _
     ByVal bv_strBLLNG_TYP_CD As String, _
     ByVal bv_i64CSTMR_ID As Int64, _
     ByVal bv_heating As Boolean, _
     ByVal bv_TransNO As String, _
     ByVal strAgentId As String, _
     ByRef br_objTrans As Transactions) As Long

        Try
            Dim objHandling_Charge As New GateOuts
            Dim lngCreated As Long
            lngCreated = objHandling_Charge.CreateHandlingCharge(bv_strEQPMNT_NO, _
                  bv_i64EQPMNT_CD_ID, bv_i64EQPMNT_TYP_ID, _
                  bv_strCstType, bv_strRFRNC_EIR_NO_1, _
                  bv_strRFRNC_EIR_NO_2, bv_datFRM_BLLNG_DT, _
                  bv_datTO_BLLNG_DT, bv_i32FR_DYS, _
                  bv_i32NO_OF_DYS, bv_strHNDLNG_CST_NC, _
                  bv_strHNDLNG_TX_RT_NC, bv_strTTL_CSTS_NC, _
                  bv_strBLLNG_FLG, bv_blnACTV_BT, _
                  bv_i32DPT_ID, bv_strIS_GT_OT_FLG, _
                  bv_strYRD_LCTN, _
                  bv_strBLLNG_TYP_CD, _
                  bv_i64CSTMR_ID, bv_heating, bv_TransNO, strAgentId, br_objTrans)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_GetEqupimentStatus() TABLE NAME:EQUIPMENT_STATUS"
    <OperationContract()> _
    Public Function pub_GetEqupimentStatus(ByVal bv_inDPT_ID As Integer) As GateOutDataSet
        Try
            Dim dsGateOutDataSet As GateOutDataSet
            Dim objGateOuts As New GateOuts
            dsGateOutDataSet = objGateOuts.pub_GetEqupimentStatus(bv_inDPT_ID)
            Return dsGateOutDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetGateOutDetailsGWS"
    <OperationContract()> _
    Public Function pub_GetGateOutDetailsGWS(ByVal bv_DeoptID As Integer) As GateOutDataSet

        Try
            Dim dsGateoutDataset As GateOutDataSet
            Dim objGateouts As New GateOuts
            dsGateoutDataset = objGateouts.pub_GetGateOutDetailsGWS(bv_DeoptID)
            Return dsGateoutDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetGateOutDetails"
    <OperationContract()> _
    Public Function pub_GetGateOutDetails(ByVal bv_DeoptID As Integer) As GateOutDataSet

        Try
            Dim dsGateoutDataset As GateOutDataSet
            Dim objGateouts As New GateOuts
            dsGateoutDataset = objGateouts.pub_GetGateOutDetails(bv_DeoptID)
            Return dsGateoutDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "Gate Out Approval Process"

    <OperationContract()> _
    Public Function pub_GetGateOutDetailsGWSWithApproval(ByVal bv_DeoptID As Integer) As GateOutDataSet

        Try
            Dim dsGateoutDataset As GateOutDataSet
            Dim objGateouts As New GateOuts
            dsGateoutDataset = objGateouts.pub_GetGateOutDetailsGWSWithApproval(bv_DeoptID)
            Return dsGateoutDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region


#Region "pub_GetGateOutMySubmitDetails"
    <OperationContract()> _
    Public Function pub_GetGateOutMySubmitDetails(ByVal bv_DeoptID As Integer) As GateOutDataSet

        Try
            Dim dsGateoutDataset As GateOutDataSet
            Dim objGateouts As New GateOuts
            dsGateoutDataset = objGateouts.pub_GetGateOutMySubmitDetails(bv_DeoptID)
            Return dsGateoutDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateGateOut"
    <OperationContract()> _
    Public Function pub_UpdateGateOut(ByRef br_dsGateOutDataSet As GateOutDataSet, ByVal bv_strWfdata As String, _
                                             ByVal bv_blnGenerateEDI As Boolean, _
                                             ByVal bv_strModifiedBy As String, _
                                             ByVal bv_datModifiedDate As DateTime, ByVal strMode As String, _
                                             ByVal bv_intDepotID As Integer, _
                                             ByVal bv_ClearAttachment As String, _
                                             ByVal bv_GWSSettings As String, _
                                             ByVal bv_str067InvoiceGenerationGWSBit As String) As Boolean
        Dim objTrans As New Transactions
        Dim strYardLocation As String = ""
        Dim strEventTime As String = ""
        Dim strEirNo As String = ""
        Dim strVechicleNo As String = ""
        Dim objCommonUIs As New CommonUIs
        Dim strTransporter As String = ""
        Dim strAuthNo As String = ""
        Dim strGI_TRNSCTN_NO As String = String.Empty
        Dim strEIRnumber As String = String.Empty
        Dim objGateOut As New GateOuts
        Dim blnHeatingBit As Boolean = False
        Dim intFreeDays As Integer = 0
        Dim intNoDays As Integer = 0
        Dim decHandlingCost As Decimal = 0
        Dim decHandlingTaxRate As Decimal = 0
        Dim decHandlingTotal As Decimal = 0
        Dim decStorageCost As Decimal = 0
        Dim decStorageTaxRate As Decimal = 0
        Dim decStorageTotal As Decimal = 0
        Dim strRemarks As String = String.Empty
        Dim strRentalRefNo As String = String.Empty
        Dim intEqpmnt_stts_id As Integer
        Dim intRntlCstmrID As Integer
        Dim intCstmr_id As Integer = 0
        Dim blnRntl_bt As Boolean = False
        Dim strEquipmentInfoRemarks As String = String.Empty
        Dim dtRentalEntry As New DataTable
        Dim dtRentalCustomer As New DataTable
        Dim objISO As New Customers
        Dim dsCustomer As New CustomerDataSet
        Dim lngGateOutId As Long
        Dim lngCreatedAttachment As Long
        Dim EirIndex As String
        Dim strSLNO As String = ""

        Try
            Dim drAGateOut As DataRow()

            Dim dsEqpStatus As CommonUIDataSet
            Dim objConfigs As New CommonUIs
            dsEqpStatus = objConfigs.GetWorkFlowActivity("Gate Out", True, bv_intDepotID)

            If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                intEqpmnt_stts_id = CInt(dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID))
            End If

            If (strMode = "new") Then
                For Each drGateOut As DataRow In br_dsGateOutDataSet.Tables(GateOutData._V_GATEOUT).Select(GateOutData.CHECKED & "='True'")
                    Dim lngCreated As Long
                    Dim lngRntlChargeCreated As Long
                    Dim strEquipmentLength As String = ""
                    Dim strEquipmentHeight As String = ""
                    Dim datMNFCTR_DT As DateTime
                    Dim datLST_OH_DT As DateTime
                    Dim datLIC_EXPR As DateTime


                    If drGateOut.RowState = DataRowState.Added Or drGateOut.RowState = DataRowState.Modified Then
                        If (drGateOut.Item(GateOutData.YRD_LCTN) Is DBNull.Value) Then
                            strYardLocation = String.Empty
                        Else
                            strYardLocation = CStr(drGateOut.Item(GateOutData.YRD_LCTN))
                        End If
                        If (drGateOut.Item(GateOutData.GTOT_TM) Is DBNull.Value) Then
                            strEventTime = String.Empty
                        Else
                            strEventTime = CStr(drGateOut.Item(GateOutData.GTOT_TM))
                        End If
                        If (drGateOut.Item(GateOutData.SL_NO) Is DBNull.Value) Then
                            strSLNO = String.Empty
                        Else
                            strSLNO = CStr(drGateOut.Item(GateOutData.SL_NO))
                        End If
                        If (drGateOut.Item(GateOutData.EIR_NO) Is DBNull.Value) Then
                            strEirNo = String.Empty
                        Else
                            strEirNo = CStr(drGateOut.Item(GateOutData.EIR_NO))
                        End If
                        If (drGateOut.Item(GateOutData.VHCL_NO) Is DBNull.Value) Then
                            strVechicleNo = String.Empty
                        Else
                            strVechicleNo = CStr(drGateOut.Item(GateOutData.VHCL_NO))
                        End If
                        If (drGateOut.Item(GateOutData.TRNSPRTR_CD) Is DBNull.Value) Then
                            strTransporter = String.Empty
                        Else
                            strTransporter = CStr(drGateOut.Item(GateOutData.TRNSPRTR_CD))
                        End If

                        If (drGateOut.Item(GateOutData.AUTH_NO) Is DBNull.Value) Then
                            strAuthNo = String.Empty
                        Else
                            strAuthNo = CStr(drGateOut.Item(GateOutData.AUTH_NO))
                        End If

                        If Not IsDBNull(drGateOut.Item(GateOutData.HTNG_BT)) Then
                            blnHeatingBit = CBool(drGateOut.Item(GateOutData.HTNG_BT))
                        Else
                            blnHeatingBit = True
                        End If
                        If (drGateOut.Item(GateOutData.RMRKS_VC) Is DBNull.Value) Then
                            strRemarks = String.Empty
                        Else
                            strRemarks = CStr(drGateOut.Item(GateOutData.RMRKS_VC))
                        End If
                        If (drGateOut.Item(GateOutData.RNTL_RFRNC_NO) Is DBNull.Value) Then
                            strRentalRefNo = String.Empty
                        Else
                            strRentalRefNo = CStr(drGateOut.Item(GateOutData.RNTL_RFRNC_NO))
                        End If
                        If (drGateOut.Item(GateOutData.RNTL_CSTMR_ID) Is DBNull.Value) Then
                            intRntlCstmrID = Nothing
                        Else
                            intRntlCstmrID = CInt(drGateOut.Item(GateOutData.RNTL_CSTMR_ID))
                        End If
                        If Not IsDBNull(drGateOut.Item(GateOutData.RNTL_BT)) Then
                            blnRntl_bt = CBool(drGateOut.Item(GateOutData.RNTL_BT))
                        Else
                            blnRntl_bt = False
                        End If
                        If (drGateOut.Item(GateOutData.GI_TRNSCTN_NO) Is DBNull.Value) Then
                            strGI_TRNSCTN_NO = String.Empty
                        Else
                            strGI_TRNSCTN_NO = CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO))
                        End If
                        'strGI_TRNSCTN_NO = (drGateOut.Item(GateOutData.GI_TRNSCTN_NO)).ToString
                        If blnRntl_bt = True Then
                            intCstmr_id = intRntlCstmrID
                            If strGI_TRNSCTN_NO = String.Empty Then
                                strGI_TRNSCTN_NO = strRentalRefNo
                            End If
                        Else
                            intCstmr_id = CommonUIs.iInt(drGateOut.Item(GateOutData.ORGNL_CSTMR_ID))
                            strGI_TRNSCTN_NO = (drGateOut.Item(GateOutData.GI_TRNSCTN_NO)).ToString
                        End If

                        Dim objIndexPattern As New IndexPatterns
                        Dim strGTOTCD As String
                        strGTOTCD = objIndexPattern.GetMaxReferenceNo(GateOutData._GATEOUT, CDate(drGateOut.Item(GateOutData.GTOT_DT)), objTrans, Nothing, bv_intDepotID)
                        lngGateOutId = CLng(drGateOut.Item(GateOutData.GTOT_ID))

                        'GWS
                        If bv_GWSSettings = "True" Then
                            strEirNo = objIndexPattern.GetMaxReferenceNo(String.Concat("GATEOUT_EIR", ",", "Gateout EIR"), CDate(drGateOut.Item(GateinData.GTN_DT)), objTrans, Nothing, bv_intDepotID)
                            EirIndex = strEirNo
                        End If
                        lngCreated = objGateOut.CreateGateOut(intCstmr_id, _
                                                            drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                            CommonUIs.iLng(drGateOut.Item(GateOutData.EQPMNT_TYP_ID)), _
                                                            CommonUIs.iLng(drGateOut.Item(GateOutData.EQPMNT_CD_ID)), _
                                                             intEqpmnt_stts_id, _
                                                            CommonUIs.iLng(drGateOut.Item(GateOutData.GRD_ID)), _
                                                            strYardLocation, _
                                                            CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                            strEventTime, _
                                                            strEirNo, _
                                                            strSLNO, _
                                                            strVechicleNo, _
                                                            strTransporter, _
                                                            strAuthNo, _
                                                            strGI_TRNSCTN_NO, _
                                                            bv_intDepotID, _
                                                            bv_strModifiedBy, _
                                                            bv_datModifiedDate, _
                                                            bv_strModifiedBy, _
                                                            bv_datModifiedDate, _
                                                            strRemarks, _
                                                            intRntlCstmrID, _
                                                            strRentalRefNo, _
                                                            blnRntl_bt, _
                                                            strGTOTCD, _
                                                            objTrans)


                        'for insert Gate out RET strEIRnumber
                        'drGateOut.Item(GateOutData.EQPMNT_STTS_CD).ToString() A
                        ' Dim strEIRTime As String = CDate(drGateOut.Item(GateOutData.EIR_DT)).ToString("hh:mm")

                        Dim strlessor As String
                        dsCustomer = objISO.getISOCODEbyCustomer(CLng(drGateOut.Item(GateOutData.CSTMR_ID)))
                        If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                            strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
                        Else
                            strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
                        End If
                        'strTransNo.Trim().ToString
                        'From Index pattern
                        'Dim strTransNo As String
                        'strTransNo = objIndexPattern.GetMaxReferenceNo(GateOutData._GATEOUT, CDate(drGateOut.Item(GateOutData.GTOT_DT)), objTrans, Nothing, bv_intDepotID)
                        ' Dim strTransNo As String = objGateOut.GetGateOutRetTransxNo(lngCreated, CDate(drGateOut.Item(GateOutData.GTOT_DT)), objTrans)


                        'EDI EIR
                        Dim strEDI_EIR As String = Nothing
                        If strEirNo.Length > 14 Then
                            strEDI_EIR = Right(strEirNo, 14)
                        Else
                            strEDI_EIR = strEirNo
                        End If

                        Dim strEquipmentDescription As String = objGateOut.GetEquipmentDescription(drGateOut.Item(GateOutData.EQPMNT_CD_CD).ToString, objTrans)
                        If strEquipmentDescription.Length > 30 Then
                            strEquipmentDescription = strEquipmentDescription.Substring(0, 30)
                        End If
                        Dim strEquipType As String
                        Dim objEquipType As New EquipmentTypes
                        Dim dsEquipType As EquipmentTypeDataSet
                        If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                            dsEquipType = objEquipType.GetEquipmentGroupByEquipmentTypeId(CStr(drGateOut.Item(GateinData.EQPMNT_TYP_ID)), CInt(objCommonUIs.GetHeadQuarterID()))
                        Else
                            dsEquipType = objEquipType.GetEquipmentGroupByEquipmentTypeId(CStr(drGateOut.Item(GateinData.EQPMNT_TYP_ID)), CInt(CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_ID")))
                        End If

                        If dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                            strEquipType = CStr(dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows(0).Item(EquipmentTypeData.EQPMNT_GRP_CD))
                        Else
                            strEquipType = String.Empty
                        End If
                        objGateOut.CreateGateoutRet(lngCreated, strGTOTCD, " ", " ", CStr(DateTime.Now.ToString("yyyyMMdd")), " ", " ", " ", " ", " ", " ", " ", " ", strEDI_EIR.Trim(), strAuthNo, drGateOut.Item(GateOutData.EQPMNT_NO).ToString, strEquipType, _
                                             strEquipmentDescription, drGateOut.Item(GateOutData.EQPMNT_CD_CD).ToString, "A", " ", " ", " ", " ", " ", " ", CDate(drGateOut.Item(GateOutData.GTOT_DT)), CStr(CDate(drGateOut.Item(GateOutData.GTOT_TM))), _
                                              Nothing, Nothing, Nothing, Nothing, Nothing, drGateOut.Item(GateOutData.EQPMNT_NO).ToString(), _
                                              drGateOut.Item(GateOutData.EQPMNT_TYP_CD).ToString(), drGateOut.Item(GateOutData.EQPMNT_CD_CD).ToString(), drGateOut.Item(GateOutData.EQPMNT_STTS_CD).ToString(), Nothing, Nothing, strlessor, _
                                              " ", " ", " ", Nothing, " ", CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD"), " ", " ", " ", " ", " ", "", "", Nothing, _
                                              " ", Nothing, Nothing, " ", " ", " ", " ", " ", " ", " ", "", "", _
                                             "", "", "", "", "", _
                                              "", Nothing, Nothing, Nothing, Nothing, Nothing, _
                                              Nothing, "", Nothing, Nothing, " ", " ", " ", " ", " ", " ", drGateOut.Item(GateOutData.CSTMR_CD).ToString, " ", _
                                              Nothing, Nothing, Nothing, " ", " ", " ", " ", " ", Nothing, Nothing, "U", Nothing, Nothing, _
                                                CStr(CommonUIs.ParseWFDATA(bv_strWfdata, "USERNAME")), 1, Nothing, Nothing, Nothing, Nothing, objTrans)


                        If blnRntl_bt = True And (drGateOut.Item(GateOutData.GI_TRNSCTN_NO)).ToString = Nothing Then

                            objGateOut.UpdateRentalEntry(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                         strRentalRefNo, _
                                                         CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                         objTrans)

                            dtRentalCustomer.Rows.Clear()
                            dtRentalEntry.Rows.Clear()
                            Dim decRentalTax As Decimal = 0
                            Dim decTotalCost As Decimal = 0
                            intFreeDays = 0
                            dtRentalCustomer = br_dsGateOutDataSet.Tables(GateOutData._CUSTOMER_RENTAL).Clone()
                            dtRentalEntry = br_dsGateOutDataSet.Tables(GateOutData._V_RENTAL_ENTRY).Clone()
                            dtRentalCustomer = objGateOut.GetRentalCustomer(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                            CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                            objTrans)
                            dtRentalEntry = objGateOut.GetRentalEntryDetails(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                       CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                       strRentalRefNo, _
                                                                       objTrans)
                            Dim decOtherCharge As Decimal = 0

                            If Not IsDBNull(dtRentalEntry.Rows(0).Item(GateOutData.OTHR_CHRG_NC)) Then
                                decOtherCharge = CDec(dtRentalEntry.Rows(0).Item(GateOutData.OTHR_CHRG_NC))
                            End If

                            For Each drRentalCustomer As DataRow In dtRentalCustomer.Select(String.Concat(GateOutData.CSTMR_ID, " ='", drGateOut.Item(GateOutData.CSTMR_ID), "'"))

                                Dim blnIsLateFlag As Boolean = False
                                Dim dtRentalCharge As New DataTable
                                If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                                    dtRentalCharge = objGateOut.GetRentalChargeDetails(CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                                                      CInt(objCommonUIs.GetHeadQuarterID()), _
                                                                                                                      objTrans)
                                Else
                                    dtRentalCharge = objGateOut.GetRentalChargeDetails(CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                  bv_intDepotID, _
                                                                                  objTrans)

                                End If
                                If dtRentalCharge.Rows.Count > 0 AndAlso Not IsDBNull(dtRentalCharge.Rows(0).Item(GateOutData.BLLNG_TLL_DT)) AndAlso _
                                    CDate(dtRentalCharge.Rows(0).Item(GateOutData.BLLNG_TLL_DT)) > CDate(drGateOut.Item(GateOutData.GTOT_DT)) Then
                                    blnIsLateFlag = True
                                End If

                                lngRntlChargeCreated = objGateOut.CreateRentalCharge(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                                     "RNTL", _
                                                                                     (drRentalCustomer.Item(GateOutData.CNTRCT_RFRNC_NO)).ToString, _
                                                                                     dtRentalEntry.Rows(0).Item(GateOutData.PO_RFRNC_NO).ToString, _
                                                                                     CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                                                     Nothing, _
                                                                                     CDec(drRentalCustomer.Item(GateOutData.HNDLNG_OT)), _
                                                                                     0, _
                                                                                     CDec(drRentalCustomer.Item(GateOutData.ON_HR_SRVY)), _
                                                                                     0, _
                                                                                     intFreeDays, _
                                                                                     Nothing, _
                                                                                     CDbl(drRentalCustomer.Item(GateOutData.RNTL_PR_DY)), _
                                                                                     decRentalTax, _
                                                                                     decTotalCost, _
                                                                                     "C", _
                                                                                     "U", _
                                                                                     "O", _
                                                                                     True, _
                                                                                     blnIsLateFlag, _
                                                                                     Nothing, _
                                                                                     strYardLocation, _
                                                                                     "C", _
                                                                                     CInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                     bv_intDepotID, _
                                                                                     strGI_TRNSCTN_NO, _
                                                                                     strRentalRefNo, _
                                                                                     "N", _
                                                                                     "N", _
                                                                                     objTrans)

                            Next

                        ElseIf Not (drGateOut.Item(GateOutData.GI_TRNSCTN_NO)).ToString = Nothing Then
                            Dim strFilter As String = String.Concat(GateOutData.GTN_ID, " ='", drGateOut.Item(GateOutData.GTN_ID), "'")
                            drAGateOut = br_dsGateOutDataSet.Tables(GateOutData._V_GATEIN_DETAIL).Select(strFilter)
                            strEIRnumber = strGI_TRNSCTN_NO
                            If drAGateOut.Length > 0 Then
                                For Each drGateinDetail As DataRow In br_dsGateOutDataSet.Tables(GateOutData._V_GATEIN_DETAIL).Select(GateOutData.GTN_ID & "='" & drGateOut.Item(GateOutData.GTN_ID).ToString & "'")
                                    If ((drGateinDetail.Item(GateOutData.MNFCTR_DT)) Is DBNull.Value) Then
                                        datMNFCTR_DT = Nothing
                                    Else
                                        datMNFCTR_DT = CDate(drGateinDetail.Item(GateOutData.MNFCTR_DT))
                                    End If
                                    If ((drGateinDetail.Item(GateOutData.LST_OH_DT)) Is DBNull.Value) Then
                                        datLST_OH_DT = Nothing
                                    Else
                                        datLST_OH_DT = CDate(drGateinDetail.Item(GateOutData.LST_OH_DT))
                                    End If
                                    If ((drGateinDetail.Item(GateOutData.LIC_EXPR)) Is DBNull.Value) Then
                                        datLIC_EXPR = Nothing
                                    Else
                                        datLIC_EXPR = CDate(drGateinDetail.Item(GateOutData.LIC_EXPR))
                                    End If

                                    If bv_blnGenerateEDI Then
                                        'TODO : Insert into Gatein-Ret Table 
                                        Dim strMnfctrDt As String = String.Empty
                                        If (drGateinDetail.Item(GateOutData.MNFCTR_DT).ToString() <> "") Then
                                            strMnfctrDt = CDate(drGateinDetail.Item(GateOutData.MNFCTR_DT)).ToString("dd/MM")
                                        Else
                                            strMnfctrDt = Nothing
                                        End If
                                        Dim strLstOHDt As String = String.Empty
                                        If (drGateinDetail.Item(GateOutData.LST_OH_DT).ToString() <> "") Then
                                            strLstOHDt = CDate(drGateinDetail.Item(GateOutData.LST_OH_DT)).ToString("yyyyMMdd")
                                        Else
                                            strLstOHDt = Nothing
                                        End If
                                        Dim strLICExpDt As String = String.Empty
                                        If (drGateinDetail.Item(GateOutData.LIC_EXPR).ToString() <> "") Then
                                            strLICExpDt = CDate(drGateinDetail.Item(GateOutData.LIC_EXPR)).ToString("dd/MM")
                                        Else
                                            strLICExpDt = Nothing
                                        End If
                                        Dim GateoutTime As String = CDate(drGateOut.Item(GateOutData.GTOT_TM)).ToString("hh:mm")
                                        Dim strEquDescription As String = objGateOut.GetEquipmentDescription(drGateOut.Item(GateOutData.EQPMNT_CD_CD).ToString, objTrans)
                                        If strEquDescription.Length > 30 Then
                                            strEquDescription = strEquDescription.Substring(0, 30)
                                        End If

                                        objGateOut.CreateGateoutRet(CLng(GateOutData.GTN_ID), strGI_TRNSCTN_NO, _
                                                                    " ", _
                                                                    " ", _
                                                                    CStr(DateTime.Now.ToString("yyyyMMdd")), _
                                                                    " ", " ", " ", " ", " ", " ", " ", " ", _
                                                                    strEIRnumber, drGateOut.Item(GateOutData.EIR_NO).ToString, _
                                                                    drGateOut.Item(GateOutData.EQPMNT_NO).ToString, strEquipType, _
                                                                     strEquDescription, drGateOut.Item(GateOutData.EQPMNT_CD_CD).ToString, _
                                                                     drGateOut.Item(GateOutData.EQPMNT_STTS_CD).ToString(), _
                                                                     " ", " ", " ", " ", " ", " ", CDate(drGateOut.Item(GateOutData.GTOT_DT)), GateoutTime, _
                                                                                drGateOut.Item(GateOutData.CSTMR_CD).ToString, _
                                                                                strMnfctrDt, drGateinDetail.Item(GateOutData.MTRL_CD).ToString, _
                                                                                drGateinDetail.Item(GateOutData.GRSS_WGHT_NC).ToString(), _
                                                                                drGateinDetail.Item(GateOutData.MSR_CD).ToString(), _
                                                                                drGateinDetail.Item(GateOutData.UNT_CD).ToString(), _
                                                                                drGateinDetail.Item(GateOutData.ACEP_CD).ToString, _
                                                                                drGateinDetail.Item(GateOutData.CNTRY_CD).ToString(), _
                                                                                drGateinDetail.Item(GateOutData.LIC_STT).ToString(), _
                                                                                drGateinDetail.Item(GateOutData.LIC_REG).ToString, _
                                                                                strLICExpDt, drGateOut.Item(GateOutData.CSTMR_CD).ToString, _
                                                                                " ", " ", " ", drGateinDetail.Item(GateOutData.TRCKR_CD).ToString, _
                                                                                " ", CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD"), " ", " ", " ", " ", " ", _
                                                                                drGateinDetail.Item(GateOutData.NT_1_VC).ToString, _
                                                                                drGateinDetail.Item(GateOutData.NT_2_VC).ToString, drGateinDetail.Item(GateOutData.LOD_STTS_CD).ToString, _
                                                                                " ", drGateinDetail.Item(GateOutData.LST_OH_LOC).ToString, _
                                                                                strLstOHDt, " ", " ", " ", " ", " ", " ", " ", _
                                                                                drGateinDetail.Item(GateOutData.SL_PRTY_1_CD).ToString, _
                                                                                drGateinDetail.Item(GateOutData.SL_NMBR_1).ToString, _
                                                                                drGateinDetail.Item(GateOutData.SL_PRTY_2_CD).ToString, _
                                                                                drGateinDetail.Item(GateOutData.SL_NMBR_2).ToString, _
                                                                                drGateinDetail.Item(GateOutData.SL_PRTY_3_CD).ToString, _
                                                                                drGateinDetail.Item(GateOutData.SL_NMBR_3).ToString, _
                                                                               " ", _
                                                                               " ", _
                                                                                drGateinDetail.Item(GateOutData.PRT_FNC_CD).ToString, _
                                                                                drGateinDetail.Item(GateOutData.PRT_NM).ToString, _
                                                                                drGateinDetail.Item(GateOutData.VSSL_NM).ToString, _
                                                                                drGateinDetail.Item(GateOutData.VYG_NO).ToString, _
                                                                                drGateinDetail.Item(GateOutData.HAZ_MTL_CD).ToString, _
                                                                                drGateinDetail.Item(GateOutData.HAZ_MATL_DSC).ToString, _
                                                                                drGateinDetail.Item(GateOutData.NT_3_VC).ToString, _
                                                                                " ", _
                                                                                "", _
                                                                                " ", " ", " ", " ", " ", " ", drGateinDetail.Item(GateOutData.SHPPR_NAM).ToString, " ", _
                                                                                drGateinDetail.Item(GateOutData.RL_ID_VC).ToString, _
                                                                                drGateinDetail.Item(GateOutData.RL_RMP_LOC).ToString, _
                                                                                drGateinDetail.Item(GateOutData.VSSL_CD).ToString, _
                                                                                " ", " ", " ", " ", " ", drGateinDetail.Item(GateOutData.PRT_NO).ToString, _
                                                                                drGateinDetail.Item(GateOutData.PRT_LC_CD).ToString, "U", Nothing, Nothing, _
                                                                                  CommonUIs.ParseWFDATA(bv_strWfdata, "USERNAME"), 1, _
                                                                                  drGateOut.Item(GateOutData.YRD_LCTN).ToString, Nothing, Nothing, CInt(lngCreated), objTrans)
                                    End If

                                Next
                            End If

                            Dim dtCustomerCharge As New DataTable
                            If dtCustomerCharge.Rows.Count > 0 Then
                                dtCustomerCharge.Rows.Clear()
                            End If

                            If blnRntl_bt Then
                                Dim dsGatein As New GateOutDataSet
                                dsGatein = objGateOut.GetGateInByGateinTransactionNo(CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), CInt(drGateOut.Item(GateOutData.DPT_ID)), objTrans)
                                If dsGatein.Tables(GateOutData._GATEIN).Rows.Count > 0 Then
                                    intCstmr_id = CInt(dsGatein.Tables(GateOutData._GATEIN).Rows(0).Item(GateOutData.CSTMR_ID))
                                End If
                            End If

                            If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                                dtCustomerCharge = objGateOut.GetCustomerHanldingInCharge(intCstmr_id, _
                                                                                    CInt(drGateOut.Item(GateOutData.EQPMNT_CD_ID)), _
                                                                                    CInt(drGateOut.Item(GateOutData.EQPMNT_TYP_ID)), _
                                                                                    CInt(objCommonUIs.GetHeadQuarterID()), _
                                                                                    objTrans)
                            Else
                                dtCustomerCharge = objGateOut.GetCustomerHanldingInCharge(intCstmr_id, _
                                                                                    CInt(drGateOut.Item(GateOutData.EQPMNT_CD_ID)), _
                                                                                    CInt(drGateOut.Item(GateOutData.EQPMNT_TYP_ID)), _
                                                                                    bv_intDepotID, _
                                                                                    objTrans)
                            End If
                          
                            If dtCustomerCharge.Rows.Count > 0 Then
                                decHandlingCost = CDec(dtCustomerCharge.Rows(0).Item(GateinData.HNDLNG_OUT_CHRG_NC))
                            Else
                                decHandlingCost = 0
                            End If
                            Dim intAgentID As String = Nothing
                            'based on 067 - Invoice Generation - GWS Key is True 
                            If bv_str067InvoiceGenerationGWSBit <> Nothing AndAlso bv_str067InvoiceGenerationGWSBit.ToUpper() = "TRUE" AndAlso Not drGateOut.Item(GateinData.BLL_CD) Is DBNull.Value AndAlso drGateOut.Item(GateinData.BLL_CD).ToString().ToUpper() = "AGENT" Then

                                Dim dtAgentCharge As DataTable = Nothing
                                Dim dtAgent As DataTable = Nothing


                                'Get Agent Id from Customer Master+
                                dtAgent = objGateOut.GetAgenIdFromCustomer(drGateOut.Item(GateOutData.ORGNL_CSTMR_ID).ToString(), bv_intDepotID, objTrans)

                                If dtAgent.Rows.Count > 0 Then

                                    Dim objGatein As New GateIns

                                    intAgentID = dtAgent.Rows(0).Item(GateOutData.AGENT_ID).ToString()

                                    dtAgentCharge = objGatein.GetAgentHanldingInCharge(intAgentID, _
                                                              CInt(drGateOut.Item(GateinData.EQPMNT_TYP_ID)), _
                                                              CInt(drGateOut.Item(GateinData.EQPMNT_TYP_ID)), _
                                                              bv_intDepotID, _
                                                              objTrans)

                                    If dtAgentCharge.Rows.Count > 0 Then
                                        decHandlingCost = CDec(dtAgentCharge.Rows(0).Item(GateinData.HNDLNG_OUT_CHRG_NC))
                                    Else
                                        decHandlingCost = 0
                                    End If
                                End If

                            Else
                                intAgentID = Nothing

                            End If

                            intFreeDays = 0
                            decHandlingTaxRate = 0

                            decHandlingTotal = decHandlingCost + decHandlingTaxRate
                            decStorageCost = 0
                            decStorageTaxRate = 0
                            decStorageTotal = decStorageCost + decStorageTaxRate
                            If blnRntl_bt = False Then
                                pvt_CreateHandlingCharge(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                         CommonUIs.iLng(drGateOut.Item(GateOutData.EQPMNT_CD_ID)), _
                                                         CommonUIs.iLng(drGateOut.Item(GateOutData.EQPMNT_TYP_ID)), _
                                                         "HNDOT", _
                                                         strEirNo, _
                                                         strEIRnumber, _
                                                         CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                         CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                         intFreeDays, 0, _
                                                         decHandlingCost, _
                                                         decHandlingTaxRate, _
                                                         decHandlingTotal, _
                                                         "U", _
                                                         True, _
                                                         CInt(CommonUIs.ParseWFDATA(bv_strWfdata, GateOutData.DPT_ID)), _
                                                         "I", _
                                                         drGateOut.Item(GateOutData.YRD_LCTN).ToString, _
                                                         drGateOut.Item(GateOutData.BLLNG_TYP).ToString, _
                                                         CommonUIs.iLng(drGateOut.Item(GateOutData.ORGNL_CSTMR_ID)), _
                                                         blnHeatingBit, _
                                                         strGI_TRNSCTN_NO, _
                                                         intAgentID, _
                                                         objTrans)
                                intNoDays = CInt(DateDiff(DateInterval.Day, CDate(drGateOut.Item(GateOutData.GTN_DT)), CDate(drGateOut.Item(GateOutData.GTOT_DT)))) + 1
                                If intFreeDays > 0 Then
                                    intNoDays = intNoDays - intFreeDays
                                End If
                                If intNoDays <= 0 Then
                                    decStorageTotal = 0
                                Else
                                    decStorageTotal = decStorageCost * intNoDays
                                    decStorageTotal = decStorageTotal + decStorageTaxRate
                                End If
                                objGateOut.UpdateStorage(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                        strEirNo, _
                                                        CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                        intFreeDays, _
                                                        intNoDays, _
                                                        decStorageCost, _
                                                        decStorageTaxRate, _
                                                        decStorageTotal, _
                                                        CommonUIs.iLng(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                        True, _
                                                        CommonUIs.iInt(CommonUIs.ParseWFDATA(bv_strWfdata, GateOutData.DPT_ID)), _
                                                        CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), _
                                                        "O", _
                                                        intAgentID, _
                                                        objTrans)

                                ''21787

                                objGateOut.UpdateStorageGateOutFlag(CommonUIs.iInt(CommonUIs.ParseWFDATA(bv_strWfdata, GateOutData.DPT_ID)), _
                                                                     CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), _
                                                                     "O", objTrans)



                            Else 'Issue: 20025
                                objGateOut.UpdateStorage(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                         strEirNo, _
                                                         CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                         intFreeDays, _
                                                         intNoDays, _
                                                         decStorageCost, _
                                                         decStorageTaxRate, _
                                                         decStorageTotal, _
                                                         CommonUIs.iLng(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                         True, _
                                                         CommonUIs.iInt(CommonUIs.ParseWFDATA(bv_strWfdata, GateOutData.DPT_ID)), _
                                                         CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), _
                                                         "O", _
                                                         intAgentID, _
                                                         objTrans)
                                ''21787

                                objGateOut.UpdateStorageGateOutFlag(CommonUIs.iInt(CommonUIs.ParseWFDATA(bv_strWfdata, GateOutData.DPT_ID)), _
                                                                     CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), _
                                                                     "O", objTrans)
                            End If
                            ''Insert into Tracking Table
                            drGateOut.Item(GateOutData.GTOT_ID) = lngCreated
                            If blnRntl_bt = True Then
                                objGateOut.UpdateRentalEntry(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                             strRentalRefNo, _
                                                             CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                             objTrans)
                                Dim decRentalTax As Decimal = 0
                                Dim decTotalCost As Decimal = 0
                                intFreeDays = 0
                                dtRentalCustomer.Rows.Clear()
                                dtRentalEntry.Rows.Clear()
                                dtRentalCustomer = br_dsGateOutDataSet.Tables(GateOutData._CUSTOMER_RENTAL).Clone()
                                dtRentalEntry = br_dsGateOutDataSet.Tables(GateOutData._V_RENTAL_ENTRY).Clone()
                                dtRentalCustomer = objGateOut.GetRentalCustomer(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                                CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                objTrans)
                                dtRentalEntry = objGateOut.GetRentalEntryDetails(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                            CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                            strRentalRefNo, _
                                                                            objTrans)

                                Dim decOtherCharge As Decimal = 0

                                If Not IsDBNull(dtRentalEntry.Rows(0).Item(GateOutData.OTHR_CHRG_NC)) Then
                                    decOtherCharge = CDec(dtRentalEntry.Rows(0).Item(GateOutData.OTHR_CHRG_NC))
                                End If

                                For Each drRentalCustomer As DataRow In dtRentalCustomer.Select(String.Concat(GateOutData.CSTMR_ID, " ='", drGateOut.Item(GateOutData.CSTMR_ID), "'"))
                                    Dim blnIsLateFlag As Boolean = False
                                    Dim dtRentalCharge As New DataTable
                                    If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                                        dtRentalCharge = objGateOut.GetRentalChargeDetails(CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                     CInt(objCommonUIs.GetHeadQuarterID()), _
                                                                                     objTrans)
                                    Else
                                        dtRentalCharge = objGateOut.GetRentalChargeDetails(CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                                                             bv_intDepotID, _
                                                                                                                             objTrans)
                                    End If
                                  

                                    If dtRentalCharge.Rows.Count > 0 AndAlso Not IsDBNull(dtRentalCharge.Rows(0).Item(GateOutData.BLLNG_TLL_DT)) AndAlso _
                                        CDate(dtRentalCharge.Rows(0).Item(GateOutData.BLLNG_TLL_DT)) > CDate(drGateOut.Item(GateOutData.GTOT_DT)) Then
                                        blnIsLateFlag = True
                                    End If

                                    lngRntlChargeCreated = objGateOut.CreateRentalCharge(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                                         "RNTL", _
                                                                                         (drRentalCustomer.Item(GateOutData.CNTRCT_RFRNC_NO)).ToString, _
                                                                                         (dtRentalEntry.Rows(0).Item(GateOutData.PO_RFRNC_NO).ToString), _
                                                                                         CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                                                         Nothing, _
                                                                                         CDec(drRentalCustomer.Item(GateOutData.HNDLNG_OT)), _
                                                                                         0, _
                                                                                         CDec(drRentalCustomer.Item(GateOutData.ON_HR_SRVY)), _
                                                                                         0, _
                                                                                         intFreeDays, _
                                                                                         Nothing, _
                                                                                         CDbl(drRentalCustomer.Item(GateOutData.RNTL_PR_DY)), _
                                                                                         decRentalTax, _
                                                                                         decTotalCost, _
                                                                                         "C", _
                                                                                         "U", _
                                                                                         "O", _
                                                                                         True, _
                                                                                         blnIsLateFlag, _
                                                                                         Nothing, _
                                                                                         strYardLocation, _
                                                                                         "C", _
                                                                                         CInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                         bv_intDepotID, _
                                                                                         strGI_TRNSCTN_NO, _
                                                                                         strRentalRefNo, _
                                                                                         "N", _
                                                                                         "N", _
                                                                                         objTrans)
                                Next
                            End If
                        End If

                        objGateOut.UpdateActivityStatus_NewMode(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                intEqpmnt_stts_id, _
                                                                "Gate Out", _
                                                                CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                                False, _
                                                                strGI_TRNSCTN_NO, _
                                                                bv_intDepotID, _
                                                                strYardLocation, _
                                                                objTrans)

                        'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]

                        strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drGateOut.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                       bv_intDepotID, _
                                                                                       objTrans)
                        Dim intInvcParty As Integer
                        If drGateOut.Item(GateinData.BLL_CD).ToString().ToUpper() = "AGENT" Then
                            Dim dtAgent As DataTable
                            dtAgent = objGateOut.GetAgenIdFromCustomer(drGateOut.Item(GateOutData.ORGNL_CSTMR_ID).ToString(), bv_intDepotID, objTrans)
                            If dtAgent.Rows.Count > 0 Then
                                intInvcParty = CInt(dtAgent.Rows(0).Item(GateOutData.AGENT_ID))
                            End If
                        End If
                        objCommonUIs.CreateTracking(lngCreated, _
                                                    CommonUIs.iLng(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                    drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                    "Gate Out", _
                                                    intEqpmnt_stts_id, _
                                                    CStr(lngCreated), _
                                                    CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                    strRemarks, _
                                                    strYardLocation, _
                                                    strGI_TRNSCTN_NO, _
                                                    intInvcParty, _
                                                    strEirNo, _
                                                    bv_strModifiedBy, _
                                                    bv_datModifiedDate, _
                                                    bv_strModifiedBy, _
                                                    bv_datModifiedDate, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    bv_intDepotID, _
                                                    intRntlCstmrID, _
                                                    strRentalRefNo, _
                                                    strEquipmentInfoRemarks, _
                                                    False, _
                                                    objTrans)

                        objGateOut.UpdateGatein((drGateOut.Item(GateOutData.GI_TRNSCTN_NO)).ToString, _
                                                True, _
                                                objTrans)

                        'Attchment
                        'Check for : Rental record have not GTN_ID
                        Dim blnGIAttachemnt As Boolean = False
                        If drGateOut.Item(GateOutData.GTN_ID).ToString <> "" Then
                            Dim dsGateInAttachment As GateOutDataSet = objGateOut.pub_GetAttchemntbyGateIN(CInt(drGateOut.Item(GateOutData.GTN_ID)), "GateIn", objTrans)
                            If CInt(dsGateInAttachment.Tables("V_GATEOUT").Rows(0).Item("COUNT_ATTACH")) > 0 Then
                                lngGateOutId = CInt(drGateOut.Item(GateOutData.GTN_ID))
                                blnGIAttachemnt = True
                            End If
                        End If

                        'If (br_dsGateOutDataSet.Tables(CommonUIData._ATTACHMENT).Rows.Count = 0 And blnGIAttachemnt = True) Then
                        '    Dim dsAttachment As GateOutDataSet = objGateOut.pub_GetAttchemntbyGateINAttachment(CInt(drGateOut.Item(GateOutData.GTN_ID)), "GateIn", objTrans)
                        '    For Each drAttachment As DataRow In dsAttachment.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", lngGateOutId))
                        '        lngCreatedAttachment = objCommonUIs.CreateAttachment(lngCreated,
                        '                                            "GateOut", _
                        '                                            "", _
                        '                                            strGI_TRNSCTN_NO, _
                        '                                            CStr(drAttachment.Item(CommonUIData.ATTCHMNT_PTH)), _
                        '                                            CStr(drAttachment.Item(CommonUIData.ACTL_FL_NM)),
                        '                                            bv_strModifiedBy, _
                        '                                            bv_datModifiedDate, _
                        '                                            bv_intDepotID,
                        '                                            objTrans)
                        '    Next
                        'End If
                        If bv_ClearAttachment = Nothing Then
                            Dim dtAttachment As New DataTable
                            dtAttachment = br_dsGateOutDataSet.Tables(GateOutData._ATTACHMENT).Clone()
                            If br_dsGateOutDataSet.Tables(GateOutData._ATTACHMENT).Rows.Count = 0 Then
                                ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                                dtAttachment = objGateOut.GetAttachmentByRepairEstimateId(bv_intDepotID, "GateIn", lngGateOutId, objTrans).Tables(GateinData._ATTACHMENT)
                                br_dsGateOutDataSet.Tables(GateOutData._ATTACHMENT).Merge(dtAttachment)
                                'End If
                            End If
                        End If

                        For Each drAttachment As DataRow In br_dsGateOutDataSet.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", lngGateOutId))
                            lngCreatedAttachment = objCommonUIs.CreateAttachment(lngCreated,
                                                                "GateOut", _
                                                                "", _
                                                                strGI_TRNSCTN_NO, _
                                                                CStr(drAttachment.Item(CommonUIData.ATTCHMNT_PTH)), _
                                                                CStr(drAttachment.Item(CommonUIData.ACTL_FL_NM)),
                                                                bv_strModifiedBy, _
                                                                bv_datModifiedDate, _
                                                                bv_intDepotID,
                                                                objTrans)
                        Next
                    End If
                    If bv_GWSSettings.ToLower = "true" Then
                        drGateOut.Item(GateOutData.EIR_NO) = EirIndex

                        'Reserve Booking Operations
                        Dim objReserveBookings As New ReserveBookings

                        'Update Gate Out Bit for Reserved Equipments
                        objReserveBookings.Update_ReserveBookingFromGateOut(CStr(drGateOut.Item(GateOutData.EQPMNT_NO)), CStr(drGateOut.Item(GateOutData.ORGNL_CSTMR_ID)), CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), CStr(bv_intDepotID), True, objTrans)

                        'Delete Unused Equipments - UnSelected
                        objReserveBookings.Delete_ReserveBookingFromGateOut(CStr(drGateOut.Item(GateOutData.EQPMNT_NO)), CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), CStr(bv_intDepotID), objTrans)

                    End If

                Next
            ElseIf strMode = "edit" Then
                Dim datMNFCTR_DT As DateTime
                Dim datLST_OH_DT As DateTime
                Dim datLIC_EXPR As DateTime
                Dim strSealNo As String = Nothing
                Dim GradeId As Int32 = Nothing
                Dim strGradCode As String = Nothing
                Dim strBookingAuthNo As String = Nothing
                Dim strGoutCode As String = Nothing

                For Each drGateOut As DataRow In br_dsGateOutDataSet.Tables(GateOutData._V_GATEOUT).Select(GateOutData.CHECKED & "='True'")
                    Select Case drGateOut.RowState
                        Case DataRowState.Modified
                            If (drGateOut.Item(GateOutData.YRD_LCTN) Is DBNull.Value) Then
                                strYardLocation = String.Empty
                            Else
                                strYardLocation = CStr(drGateOut.Item(GateOutData.YRD_LCTN))
                            End If
                            If (drGateOut.Item(GateOutData.GTOT_TM) Is DBNull.Value) Then
                                strEventTime = String.Empty
                            Else
                                strEventTime = CStr(drGateOut.Item(GateOutData.GTOT_TM))
                            End If
                            If (drGateOut.Item(GateOutData.EIR_NO) Is DBNull.Value) Then
                                strEirNo = String.Empty
                            Else
                                strEirNo = CStr(drGateOut.Item(GateOutData.EIR_NO))
                            End If
                            If (drGateOut.Item(GateOutData.VHCL_NO) Is DBNull.Value) Then
                                strVechicleNo = String.Empty
                            Else
                                strVechicleNo = CStr(drGateOut.Item(GateOutData.VHCL_NO))
                            End If
                            If (drGateOut.Item(GateOutData.TRNSPRTR_CD) Is DBNull.Value) Then
                                strTransporter = String.Empty
                            Else
                                strTransporter = CStr(drGateOut.Item(GateOutData.TRNSPRTR_CD))
                            End If
                            If (drGateOut.Item(GateOutData.RMRKS_VC) Is DBNull.Value) Then
                                strRemarks = String.Empty
                            Else
                                strRemarks = CStr(drGateOut.Item(GateOutData.RMRKS_VC))
                            End If
                            If (drGateOut.Item(GateOutData.RNTL_RFRNC_NO) Is DBNull.Value) Then
                                strRentalRefNo = String.Empty
                            Else
                                strRentalRefNo = CStr(drGateOut.Item(GateOutData.RNTL_RFRNC_NO))
                            End If
                            If (drGateOut.Item(GateOutData.RNTL_CSTMR_ID) Is DBNull.Value) Then
                                intRntlCstmrID = Nothing
                            Else
                                intRntlCstmrID = CInt(drGateOut.Item(GateOutData.RNTL_CSTMR_ID))
                            End If
                            If Not IsDBNull(drGateOut.Item(GateOutData.RNTL_BT)) Then
                                blnRntl_bt = CBool(drGateOut.Item(GateOutData.RNTL_BT))
                            Else
                                blnRntl_bt = True
                            End If
                            If (drGateOut.Item(GateOutData.GI_TRNSCTN_NO) Is DBNull.Value) Then
                                strGI_TRNSCTN_NO = String.Empty
                            Else
                                strGI_TRNSCTN_NO = CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO))
                            End If
                            If blnRntl_bt = True Then
                                intCstmr_id = intRntlCstmrID
                                If strGI_TRNSCTN_NO = Nothing Then
                                    strGI_TRNSCTN_NO = strRentalRefNo
                                End If
                            Else
                                intCstmr_id = CommonUIs.iInt(drGateOut.Item(GateOutData.ORGNL_CSTMR_ID))

                                If intCstmr_id = 0 Then
                                    intCstmr_id = CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID))
                                End If

                                strGI_TRNSCTN_NO = (drGateOut.Item(GateOutData.GI_TRNSCTN_NO)).ToString
                            End If
                            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]

                            If bv_GWSSettings.ToLower = "true" Then
                                EirIndex = CStr(drGateOut.Item(GateOutData.EIR_NO))
                            End If


                            If Not drGateOut.Item(GateOutData.SL_NO) Is DBNull.Value Then
                                strSealNo = CStr(drGateOut.Item(GateOutData.SL_NO))
                            End If

                            If Not drGateOut.Item(GateOutData.GRD_ID) Is DBNull.Value Then
                                GradeId = CInt(drGateOut.Item(GateOutData.GRD_ID))
                            End If

                            If Not drGateOut.Item(GateOutData.GRD_CD) Is DBNull.Value Then
                                strGradCode = CStr(drGateOut.Item(GateOutData.GRD_CD))
                            End If

                            If Not drGateOut.Item(GateOutData.AUTH_NO) Is DBNull.Value Then
                                strBookingAuthNo = CStr(drGateOut.Item(GateOutData.AUTH_NO))
                            End If

                            If Not drGateOut.Item(GateOutData.GTOT_CD) Is DBNull.Value Then
                                strGoutCode = drGateOut.Item(GateOutData.GTOT_CD).ToString()
                            End If

                            ''strGoutCode

                            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drGateOut.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                           bv_intDepotID, _
                                                                                           objTrans)
                            objGateOut.UpdateGateOut(CLng(drGateOut.Item(GateOutData.GTOT_ID)), _
                                                   strYardLocation, _
                                                   CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                   strEventTime, _
                                                   strEirNo, _
                                                   strVechicleNo, _
                                                   strTransporter, _
                                                   CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), _
                                                   bv_intDepotID, _
                                                   bv_strModifiedBy, _
                                                   bv_datModifiedDate, _
                                                   strRemarks, _
                                                   intRntlCstmrID, _
                                                   strSealNo, _
                                                   GradeId, _
                                                   strGradCode, _
                                                   strBookingAuthNo, _
                                                   objTrans)

                            'Create Gate Out RET

                            Dim strlessor As String
                            dsCustomer = objISO.getISOCODEbyCustomer(CLng(drGateOut.Item(GateOutData.CSTMR_ID)), objTrans)
                            If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                                strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
                            Else
                                strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
                            End If

                            'Delete Old exsting Record
                            objGateOut.DeleteGateoutRET(CLng(drGateOut.Item(GateOutData.GTOT_ID)), objTrans)

                            'EDI EIR
                            Dim strEDI_EIR As String = Nothing
                            If strEirNo.Length > 14 Then
                                strEDI_EIR = Right(strEirNo, 14)
                            Else
                                strEDI_EIR = strEirNo
                            End If

                            'strTransNo.Trim().ToString()
                            ' Dim strTransNo As String = objGateOut.GetGateOutRetTransxNo(CLng(drGateOut.Item(GateOutData.GTOT_ID)), CDate(drGateOut.Item(GateOutData.GTOT_DT)), objTrans)
                            Dim strEquipmentDescription As String = objGateOut.GetEquipmentDescription(drGateOut.Item(GateOutData.EQPMNT_CD_CD).ToString, objTrans)
                            If strEquipmentDescription.Length > 30 Then
                                strEquipmentDescription = strEquipmentDescription.Substring(0, 30)
                            End If
                            Dim strEquipType As String
                            Dim objEquipType As New EquipmentTypes
                            Dim dsEquipType As EquipmentTypeDataSet = objEquipType.GetEquipmentGroupByEquipmentTypeId(CStr(drGateOut.Item(GateinData.EQPMNT_TYP_ID)), CInt(CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_ID")))
                            If dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                                strEquipType = CStr(dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows(0).Item(EquipmentTypeData.EQPMNT_GRP_CD))
                            Else
                                strEquipType = String.Empty
                            End If
                            objGateOut.CreateGateoutRet(CLng(drGateOut.Item(GateOutData.GTOT_ID)), strGoutCode, " ", " ", CStr(DateTime.Now.ToString("yyyyMMdd")), " ", " ", " ", " ", " ", " ", " ", " ", strEDI_EIR.Trim(), strBookingAuthNo, drGateOut.Item(GateOutData.EQPMNT_NO).ToString, strEquipType, _
                                                  strEquipmentDescription, drGateOut.Item(GateOutData.EQPMNT_CD_CD).ToString, "A", " ", " ", " ", " ", " ", " ", CDate(drGateOut.Item(GateOutData.GTOT_DT)), CStr(CDate(drGateOut.Item(GateOutData.GTOT_TM))), _
                                                  Nothing, Nothing, Nothing, Nothing, Nothing, drGateOut.Item(GateOutData.EQPMNT_NO).ToString(), _
                                                  Nothing, Nothing, drGateOut.Item(GateOutData.EQPMNT_STTS_CD).ToString(), Nothing, Nothing, strlessor, _
                                                  " ", " ", " ", Nothing, " ", CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD"), " ", " ", " ", " ", " ", "", "", Nothing, _
                                                  " ", Nothing, Nothing, " ", " ", " ", " ", " ", " ", " ", "", "", _
                                                 "", "", "", "", "", _
                                                  "", Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                  Nothing, "", Nothing, Nothing, " ", " ", " ", " ", " ", " ", drGateOut.Item(GateOutData.CSTMR_CD).ToString, " ", _
                                                  Nothing, Nothing, Nothing, " ", " ", " ", " ", " ", Nothing, Nothing, "U", Nothing, Nothing, _
                                                    CStr(CommonUIs.ParseWFDATA(bv_strWfdata, "USERNAME")), 1, Nothing, Nothing, Nothing, Nothing, objTrans)


                            If blnRntl_bt = True And (drGateOut.Item(GateOutData.GI_TRNSCTN_NO)).ToString = Nothing Then

                                Dim blnIsLateFlag As Boolean = False
                                Dim dtRentalCharge As New DataTable
                                If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                                    dtRentalCharge = objGateOut.GetRentalChargeDetails(CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                 CInt(objCommonUIs.GetHeadQuarterID()), _
                                                                                 objTrans)
                                Else
                                    dtRentalCharge = objGateOut.GetRentalChargeDetails(CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                 bv_intDepotID, _
                                                                                 objTrans)
                                End If
                              

                                If dtRentalCharge.Rows.Count > 0 AndAlso Not IsDBNull(dtRentalCharge.Rows(0).Item(GateOutData.BLLNG_TLL_DT)) AndAlso _
                                    CDate(dtRentalCharge.Rows(0).Item(GateOutData.BLLNG_TLL_DT)) > CDate(drGateOut.Item(GateOutData.GTOT_DT)) Then
                                    blnIsLateFlag = True
                                End If

                                objGateOut.UpdateRentalEntry(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                             strRentalRefNo, _
                                                             CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                             objTrans)
                                objGateOut.UpdateRentalCharge(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                              CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                              strYardLocation, _
                                                              strRentalRefNo, _
                                                              bv_intDepotID, _
                                                              blnIsLateFlag, _
                                                              objTrans)
                                'End If

                                'If blnRntl_bt = False Then
                            ElseIf Not (drGateOut.Item(GateOutData.GI_TRNSCTN_NO)).ToString = Nothing Then

                                strGI_TRNSCTN_NO = CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO))
                                If Not drGateOut.Item(GateOutData.GTN_ID) Is DBNull.Value Then
                                    Dim strFilter As String = String.Concat(GateOutData.GTN_ID, " ='", drGateOut.Item(GateOutData.GTN_ID), "'")
                                    drAGateOut = br_dsGateOutDataSet.Tables(GateOutData._V_GATEIN_DETAIL).Select(strFilter)
                                    strEIRnumber = strGI_TRNSCTN_NO
                                    If drAGateOut.Length > 0 Then
                                        For Each drGateinDetail As DataRow In br_dsGateOutDataSet.Tables(GateOutData._V_GATEIN_DETAIL).Select(GateOutData.GTN_ID & "='" & drGateOut.Item(GateOutData.GTN_ID).ToString & "'")
                                            If (drGateinDetail.Item(GateOutData.MNFCTR_DT) Is DBNull.Value) Then
                                                datMNFCTR_DT = Nothing
                                            Else
                                                datMNFCTR_DT = CDate(drGateinDetail.Item(GateOutData.MNFCTR_DT))
                                            End If
                                            If (drGateinDetail.Item(GateOutData.LST_OH_DT) Is DBNull.Value) Then
                                                datLST_OH_DT = Nothing
                                            Else
                                                datLST_OH_DT = CDate(drGateinDetail.Item(GateOutData.LST_OH_DT))
                                            End If
                                            If (drGateinDetail.Item(GateOutData.LIC_EXPR) Is DBNull.Value) Then
                                                datLIC_EXPR = Nothing
                                            Else
                                                datLIC_EXPR = CDate(drGateinDetail.Item(GateOutData.LIC_EXPR))
                                            End If
                                        Next
                                    End If
                                End If
                                If blnRntl_bt Then
                                    Dim dsGatein As New GateOutDataSet
                                    dsGatein = objGateOut.GetGateInByGateinTransactionNo(CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), CInt(drGateOut.Item(GateOutData.DPT_ID)), objTrans)
                                    If dsGatein.Tables(GateOutData._GATEIN).Rows.Count > 0 Then
                                        intCstmr_id = CInt(dsGatein.Tables(GateOutData._GATEIN).Rows(0).Item(GateOutData.CSTMR_ID))
                                    End If
                                End If

                                Dim dtCustomerCharge As New DataTable
                                If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                                    dtCustomerCharge = objGateOut.GetCustomerHanldingInCharge(intCstmr_id, _
                                                                                          CInt(drGateOut.Item(GateOutData.EQPMNT_CD_ID)), _
                                                                                          CInt(drGateOut.Item(GateOutData.EQPMNT_TYP_ID)), _
                                                                                          CInt(objCommonUIs.GetHeadQuarterID()), _
                                                                                          objTrans)
                                Else
                                    dtCustomerCharge = objGateOut.GetCustomerHanldingInCharge(intCstmr_id, _
                                                                                          CInt(drGateOut.Item(GateOutData.EQPMNT_CD_ID)), _
                                                                                          CInt(drGateOut.Item(GateOutData.EQPMNT_TYP_ID)), _
                                                                                          bv_intDepotID, _
                                                                                          objTrans)
                                End If
                                


                                If dtCustomerCharge.Rows.Count > 0 Then
                                    decHandlingCost = CDec(dtCustomerCharge.Rows(0).Item(GateinData.HNDLNG_OUT_CHRG_NC))
                                Else
                                    decHandlingCost = 0
                                End If
                                Dim intAgentID As String = Nothing

                                'based on 067 - Invoice Generation - GWS Key is True 
                                If bv_str067InvoiceGenerationGWSBit <> Nothing AndAlso bv_str067InvoiceGenerationGWSBit.ToUpper() = "TRUE" AndAlso Not drGateOut.Item(GateinData.BLL_CD) Is DBNull.Value AndAlso drGateOut.Item(GateinData.BLL_CD).ToString().ToUpper() = "AGENT" Then

                                    Dim dtAgentCharge As DataTable = Nothing
                                    Dim dtAgent As DataTable = Nothing


                                    'Get Agent Id from Customer Master+
                                    dtAgent = objGateOut.GetAgenIdFromCustomer(CStr(intCstmr_id), bv_intDepotID, objTrans)

                                    If dtAgent.Rows.Count > 0 Then

                                        Dim objGatein As New GateIns

                                        intAgentID = dtAgent.Rows(0).Item(GateOutData.AGENT_ID).ToString()

                                        dtAgentCharge = objGatein.GetAgentHanldingInCharge(intAgentID, _
                                                                  CInt(drGateOut.Item(GateinData.EQPMNT_TYP_ID)), _
                                                                  CInt(drGateOut.Item(GateinData.EQPMNT_TYP_ID)), _
                                                                  bv_intDepotID, _
                                                                  objTrans)

                                        If dtAgentCharge.Rows.Count > 0 Then
                                            decHandlingCost = CDec(dtAgentCharge.Rows(0).Item(GateinData.HNDLNG_OUT_CHRG_NC))
                                        Else
                                            decHandlingCost = 0
                                        End If
                                    End If

                                End If


                                If blnRntl_bt = False Then

                                    intFreeDays = 0
                                    decHandlingTaxRate = 0

                                    decHandlingTotal = decHandlingCost + decHandlingTaxRate
                                    decStorageCost = 0
                                    decStorageTaxRate = 0
                                    decStorageTotal = decStorageCost + decStorageTaxRate
                                    objGateOut.UpdateHandling_Charge(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                     CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                                     decHandlingCost, _
                                                                     decHandlingTaxRate, _
                                                                     decHandlingTotal, _
                                                                     drGateOut.Item(GateOutData.YRD_LCTN).ToString, _
                                                                     strGI_TRNSCTN_NO, _
                                                                     bv_intDepotID, _
                                                                     strEirNo, _
                                                                     objTrans)

                                    objGateOut.UpdateStorage_Charge(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                            CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                            drGateOut.Item(GateOutData.YRD_LCTN).ToString, _
                                                            bv_intDepotID, _
                                                            strGI_TRNSCTN_NO, _
                                                            strEirNo, _
                                                            objTrans)
                                Else

                                    objGateOut.UpdateStorage_Charge(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                    CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                                    drGateOut.Item(GateOutData.YRD_LCTN).ToString, _
                                                                    bv_intDepotID, _
                                                                    strGI_TRNSCTN_NO, _
                                                                    strEirNo, _
                                                                    objTrans)
                                End If
                                If blnRntl_bt = True Then
                                    Dim blnIsLateFlag As Boolean = False
                                    Dim dtRentalCharge As New DataTable
                                    If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                                        dtRentalCharge = objGateOut.GetRentalChargeDetails(CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                      CInt(objCommonUIs.GetHeadQuarterID()), _
                                                                                      objTrans)
                                    Else
                                        dtRentalCharge = objGateOut.GetRentalChargeDetails(CommonUIs.iInt(drGateOut.Item(GateOutData.CSTMR_ID)), _
                                                                                      bv_intDepotID, _
                                                                                      objTrans)
                                    End If
                                   
                                    If dtRentalCharge.Rows.Count > 0 AndAlso Not IsDBNull(dtRentalCharge.Rows(0).Item(GateOutData.BLLNG_TLL_DT)) AndAlso _
                                        CDate(dtRentalCharge.Rows(0).Item(GateOutData.BLLNG_TLL_DT)) > CDate(drGateOut.Item(GateOutData.GTOT_DT)) Then
                                        blnIsLateFlag = True
                                    End If

                                    objGateOut.UpdateRentalEntry(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                 strRentalRefNo, _
                                                                 CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                                 objTrans)

                                    objGateOut.UpdateRentalCharge(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                  CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                                  strYardLocation, _
                                                                  strRentalRefNo, _
                                                                  bv_intDepotID, _
                                                                  blnIsLateFlag, _
                                                                  objTrans)
                                    'Else

                                    '    objCommonUIs.UpdateTracking_Date_Remarks_And_YardLocation(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                    '                                                              CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                    '                                                             "Gate Out", _
                                    '                                                              strGI_TRNSCTN_NO, _
                                    '                                                              strEirNo, _
                                    '                                                              strRemarks, _
                                    '                                                              strYardLocation, _
                                    '                                                              bv_intDepotID, _
                                    '                                                              bv_strModifiedBy, _
                                    '                                                              bv_datModifiedDate, _
                                    '                                                              strEquipmentInfoRemarks, _
                                    '                                                              objTrans)
                                End If
                                objCommonUIs.UpdateTracking_Date_Remarks_And_YardLocation(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                                                             CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                                                            "Gate Out", _
                                                                                             strGI_TRNSCTN_NO, _
                                                                                             strEirNo, _
                                                                                             strRemarks, _
                                                                                             strYardLocation, _
                                                                                             bv_intDepotID, _
                                                                                             bv_strModifiedBy, _
                                                                                             bv_datModifiedDate, _
                                                                                             strEquipmentInfoRemarks, _
                                                                                             objTrans)
                                objGateOut.UpdateActivityStatus(drGateOut.Item(GateOutData.EQPMNT_NO).ToString, _
                                                               CDate(drGateOut.Item(GateOutData.GTOT_DT)), _
                                                               strGI_TRNSCTN_NO, _
                                                               bv_intDepotID, _
                                                               strYardLocation, _
                                                               objTrans)

                            End If

                            'Attachment : Delete old record and add new

                            Dim dtAttachment As New DataTable
                            dtAttachment = br_dsGateOutDataSet.Tables(GateOutData._ATTACHMENT).Clone()
                            If br_dsGateOutDataSet.Tables(GateOutData._ATTACHMENT).Rows.Count = 0 Then
                                dtAttachment = objGateOut.GetAttachmentByRepairEstimateId(bv_intDepotID, "GateOut", CLng(drGateOut.Item(GateOutData.GTOT_ID)), objTrans).Tables(GateinData._ATTACHMENT)
                                br_dsGateOutDataSet.Tables(GateOutData._ATTACHMENT).Merge(dtAttachment)
                            End If

                            Dim blnDeleteAttachment As Boolean = False
                            For Each drAttachment As DataRow In br_dsGateOutDataSet.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", drGateOut.Item(GateOutData.GTOT_ID)))
                                If drAttachment.RowState <> DataRowState.Deleted Then
                                    blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), _
                                                                                                      CLng(drGateOut.Item(GateOutData.GTOT_ID)), _
                                                                                                      drAttachment.Item(CommonUIData.RPR_ESTMT_NO).ToString, _
                                                                                                      bv_intDepotID, _
                                                                                                      objTrans)
                                Else
                                    blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO, DataRowVersion.Original).ToString, _
                                                                                                        CLng(drGateOut.Item(GateOutData.GTOT_ID)), _
                                                                                                     drAttachment.Item(CommonUIData.RPR_ESTMT_NO, DataRowVersion.Original).ToString, _
                                                                                                     bv_intDepotID, _
                                                                                                     objTrans)
                                End If
                            Next
                            If br_dsGateOutDataSet.Tables(CommonUIData._ATTACHMENT).Rows.Count = 0 AndAlso Not br_dsGateOutDataSet.Tables(CommonUIData._V_REPAIR_ESTIMATE) Is Nothing Then
                                For Each drAttachment As DataRow In br_dsGateOutDataSet.Tables(CommonUIData._V_REPAIR_ESTIMATE).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drGateOut.Item(GateOutData.GTOT_ID))))
                                    If drAttachment.RowState <> DataRowState.Deleted Then
                                        blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO).ToString, _
                                                                                                          CLng(drGateOut.Item(GateOutData.GTOT_ID)), _
                                                                                                          drAttachment.Item(CommonUIData.RPR_ESTMT_NO).ToString, _
                                                                                                          bv_intDepotID, _
                                                                                                          objTrans)
                                    End If
                                Next
                                For Each drAttachment As DataRow In br_dsGateOutDataSet.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", drGateOut.Item(GateOutData.GTOT_ID)))
                                    If CInt(bv_ClearAttachment) <> 0 Then

                                        lngCreatedAttachment = objCommonUIs.CreateAttachment(CLng(drGateOut.Item(GateOutData.GTOT_ID)),
                                                                            "GateOut", _
                                                                            "", _
                                                                            CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), _
                                                                            CStr(drAttachment.Item(CommonUIData.ATTCHMNT_PTH)), _
                                                                            CStr(drAttachment.Item(CommonUIData.ACTL_FL_NM)),
                                                                            bv_strModifiedBy, _
                                                                            bv_datModifiedDate, _
                                                                            bv_intDepotID,
                                                                            objTrans)
                                    End If
                                Next
                            End If
                            For Each drAttachment As DataRow In br_dsGateOutDataSet.Tables(CommonUIData._ATTACHMENT).Rows
                                If drAttachment.RowState = DataRowState.Deleted Then
                                    blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO, DataRowVersion.Original).ToString, _
                                                                                                      CLng(drGateOut.Item(GateOutData.GTOT_ID)), _
                                                                                                     drAttachment.Item(CommonUIData.RPR_ESTMT_NO, DataRowVersion.Original).ToString, _
                                                                                                     bv_intDepotID, _
                                                                                                     objTrans)
                                ElseIf drAttachment.RowState = DataRowState.Added Then
                                    lngCreatedAttachment = objCommonUIs.CreateAttachment(CLng(drGateOut.Item(GateOutData.GTOT_ID)),
                                                                           "GateOut", _
                                                                           "", _
                                                                           CStr(drGateOut.Item(GateOutData.GI_TRNSCTN_NO)), _
                                                                           CStr(drAttachment.Item(CommonUIData.ATTCHMNT_PTH)), _
                                                                           CStr(drAttachment.Item(CommonUIData.ACTL_FL_NM)),
                                                                           bv_strModifiedBy, _
                                                                           bv_datModifiedDate, _
                                                                           bv_intDepotID,
                                                                           objTrans)
                                End If
                            Next

                          
                    End Select
                    If bv_GWSSettings.ToLower = "true" Then
                        drGateOut.Item(GateOutData.EIR_NO) = EirIndex
                    End If
                Next
            End If
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function
#End Region

#Region "pub_GetAttchemntbyGateIN"
    <OperationContract()> _
    Public Function pub_GetAttchemntbyGateIN(ByVal bv_GateINID As Integer, ByVal bv_strActivity As String) As GateOutDataSet

        Try
            Dim dsGateoutDataset As GateOutDataSet
            Dim objGateouts As New GateOuts
            Dim objTrans As New Transactions
            dsGateoutDataset = objGateouts.pub_GetAttchemntbyGateIN(bv_GateINID, bv_strActivity, objTrans)
            Return dsGateoutDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEquipmentInformation() TABLE NAME:EQUIPMENT_INFORMATION"
    <OperationContract()> _
    Public Function pub_GetEquipmentInformation(ByVal bv_strEquipment As String) As GateOutDataSet
        Try
            Dim dsGateOutDataSet As GateOutDataSet
            Dim objGateouts As New GateOuts
            dsGateOutDataSet = objGateouts.pub_GetEquipmentInformation(bv_strEquipment)
            Return dsGateOutDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
End Class
