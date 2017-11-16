Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Business
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class Gatein

#Region "pub_ValidateEquipmentNoByDepotID"
    <OperationContract()> _
    Public Function pub_ValidateEquipmentNoByDepotID(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32, ByRef br_strCustomer As String) As Boolean

        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGatein As New GateIns
            Dim blnValid As Boolean = True
            dsGateinDataSet = objGatein.GetGateInEquipmentByID(bv_strEquipmentNo, bv_intDepotID)
            If dsGateinDataSet.Tables(GateinData._GATEIN).Rows.Count > 0 Then
                blnValid = CBool(dsGateinDataSet.Tables(GateinData._GATEIN).Rows(0).Item(GateinData.GTOT_BT))
                br_strCustomer = CStr(dsGateinDataSet.Tables(GateinData._GATEIN).Rows(0).Item(GateinData.CSTMR_CD))
            End If
            Return blnValid
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region




#Region "pub_UpdateGateIn"
    <OperationContract()> _
    Public Function pub_UpdateGateIn(ByRef br_dsGateInDataset As GateinDataSet, _
                                     ByVal bv_strWfdata As String, _
                                     ByVal bv_blnGenerateEDI As Boolean, _
                                     ByVal bv_strModifiedBy As String, _
                                     ByVal bv_datModifiedDate As DateTime, _
                                     ByVal strMode As String, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByVal bv_strRemarks As String, _
                                     ByVal bv_ClearAttachment As String, _
                                     ByRef br_strLockingRecords As String, _
                                     ByVal bv_strGWSValue As String, _
                                     ByVal bv_strNotesStdEdi As String, _
                                     ByVal bv_str067InvoiceGenerationGWSBit As String) As Boolean
        Dim objTrans As New Transactions
        Dim strYardLocation As String = ""
        Dim strEventTime As String = ""
        Dim strEirNo As String = ""
        Dim strVechicleNo As String = ""
        Dim objCommonUIs As New CommonUIs
        Dim strTransporter As String = ""
        Dim strRemarks As String = ""
        Dim strGI_TRNSCTN_NO As String = String.Empty
        Dim strEIRnumber As String = String.Empty
        Dim blnHeatingBit As Boolean = False
        Dim objGatein As New GateIns
        Dim blnRentalBit As Boolean = False
        Dim intRentalCustomerID As Integer
        Dim dblOtherCharge As Decimal = 0
        Dim strEquipmentInfoRemarks As String = String.Empty
        Dim lngCreateAttachment As Long = 0
        Dim lngEquipmentInformation As Long = 0
        Dim lng_GTN_ID As Long = 0
        Dim objISO As New Customers
        Dim dsCustomer As New CustomerDataSet
        Dim dsProduct As New GateinDataSet
        Dim dsCleaning As New GateinDataSet
        Dim lngCreatedAttachment As Long
        Dim lngGateINId As Long
        Dim EirIndex As String
        Try
            If (strMode = "new") Then
                For Each drGateIn As DataRow In br_dsGateInDataset.Tables(GateinData._V_GATEIN).Select(GateinData.CHECKED & "='True'")
                    Dim lngCreated As Long
                    Dim strEquipmentLength As String = ""
                    Dim strEquipmentHeight As String = ""
                    Dim datMNFCTR_DT As DateTime
                    Dim datLST_OH_DT As DateTime
                    Dim datLIC_EXPR As DateTime
                    Dim intFreeDays As Integer = 0
                    Dim intNoDays As Integer = 0
                    Dim intProdId As Integer = 0
                    Dim decHandlingCost As Decimal = 0
                    Dim decHandlingTaxRate As Decimal = 0
                    Dim decHandlingTotal As Decimal = 0
                    Dim decStorageCost As Decimal = 0
                    Dim decStorageTaxRate As Decimal = 0
                    Dim decStorageTotal As Decimal = 0
                    Dim intBillId As String
                    Dim strRedelAuth As String = ""
                    Dim strConsignee As String = ""
                    Dim intGradeID As String
                    Dim strRentalReferenceNo As String = String.Empty
                    Dim dtRentalEntry As New DataTable
                    Dim dtRentalCustomer As New DataTable
                    Dim drAGateIn As DataRow()
                    Dim intAgentID As String
                    Dim blnLock As Boolean = False
                    Dim strNote1 As String
                    Dim strNote2 As String
                    Dim strNote3 As String
                    Dim strNote4 As String
                    Dim strNote5 As String

                    If drGateIn.RowState = DataRowState.Added Or drGateIn.RowState = DataRowState.Modified Then
                        blnLock = objGatein.GetGateInLockingEquipmentByID(CStr(drGateIn.Item(GateinData.EQPMNT_NO)), CLng(drGateIn.Item(GateinData.CSTMR_ID)), bv_intDepotID, objTrans)
                        If blnLock = False Then

                            If (drGateIn.Item(GateinData.YRD_LCTN) Is DBNull.Value) Then
                                strYardLocation = String.Empty
                            Else
                                strYardLocation = CStr(drGateIn.Item(GateinData.YRD_LCTN))
                            End If
                            If (drGateIn.Item(GateinData.GTN_TM) Is DBNull.Value) Then
                                strEventTime = String.Empty
                            Else
                                strEventTime = CStr(drGateIn.Item(GateinData.GTN_TM))
                            End If
                            If (drGateIn.Item(GateinData.EIR_NO) Is DBNull.Value) Then
                                strEirNo = String.Empty
                            Else
                                strEirNo = CStr(drGateIn.Item(GateinData.EIR_NO))
                            End If
                            If (drGateIn.Item(GateinData.VHCL_NO) Is DBNull.Value) Then
                                strVechicleNo = String.Empty
                            Else
                                strVechicleNo = CStr(drGateIn.Item(GateinData.VHCL_NO))
                            End If
                            If (drGateIn.Item(GateinData.TRNSPRTR_CD) Is DBNull.Value) Then
                                strTransporter = String.Empty
                            Else
                                strTransporter = CStr(drGateIn.Item(GateinData.TRNSPRTR_CD))
                            End If
                            If (drGateIn.Item(GateinData.RMRKS_VC) Is DBNull.Value) Then
                                strRemarks = String.Empty
                            Else
                                strRemarks = CStr(drGateIn.Item(GateinData.RMRKS_VC))
                            End If
                            If Not IsDBNull(drGateIn.Item(GateinData.HTNG_BT)) Then
                                blnHeatingBit = CBool(drGateIn.Item(GateinData.HTNG_BT))
                            Else
                                blnHeatingBit = False
                            End If
                            If Not IsDBNull(drGateIn.Item(GateinData.RNTL_BT)) Then
                                blnRentalBit = CBool(drGateIn.Item(GateinData.RNTL_BT))
                            Else
                                blnRentalBit = False
                            End If
                            If bv_strGWSValue.ToLower = "true" Then
                            If (drGateIn.Item(GateinData.CNSGNE) Is DBNull.Value) Then
                                    strConsignee = String.Empty
                                Else
                                    strConsignee = CStr(drGateIn.Item(GateinData.CNSGNE))
                                End If
                                If (drGateIn.Item(GateinData.RDL_ATH) Is DBNull.Value) Then
                                    strRedelAuth = String.Empty
                                Else
                                    strRedelAuth = CStr(drGateIn.Item(GateinData.RDL_ATH))
                                End If
                                If (drGateIn.Item(GateinData.BLL_ID) Is DBNull.Value) Then
                                    intBillId = String.Empty
                                Else
                                    intBillId = CStr(drGateIn.Item(GateinData.BLL_ID))
                                    'If drGateIn.Item(GateinData.BLL_ID).ToString = "CUSTOMER" Then
                                    '    intBillId = 144
                                    'ElseIf drGateIn.Item(GateinData.BLL_ID).ToString = "AGENT" Then
                                    '    intBillId = 145
                                    'End If

                                End If
                                If (drGateIn.Item(GateinData.BLL_CD) Is DBNull.Value) Then
                                    intBillId = String.Empty
                                Else
                                    intBillId = CStr(drGateIn.Item(GateinData.BLL_CD))
                                End If
                                If (drGateIn.Item(GateinData.AGNT_ID) Is DBNull.Value) Then
                                    intAgentID = CStr(objGatein.GetAgentIDByCode(drGateIn.Item(GateinData.AGNT_CD).ToString))
                                Else
                                    intAgentID = CStr(drGateIn.Item(GateinData.AGNT_ID))
                                End If


                                If (drGateIn.Item(GateinData.GRD_ID) Is DBNull.Value OrElse drGateIn.Item(GateinData.GRD_ID).ToString = "") Then
                                    intGradeID = CStr(objGatein.GetGradeIDByCode(drGateIn.Item(GateinData.GRD_CD).ToString))
                                Else
                                    intGradeID = CStr(drGateIn.Item(GateinData.GRD_ID))
                                End If
                            End If
                            If (drGateIn.Item(GateinData.PRDCT_ID) Is DBNull.Value) Then
                                intProdId = 0
                            Else
                                intProdId = CInt(drGateIn.Item(GateinData.PRDCT_ID))
                            End If
                            Dim dsEqpData As CommonUIDataSet
                            Dim objConfigs As New CommonUIs
                            dtRentalCustomer.Rows.Clear()
                            dtRentalEntry.Rows.Clear()
                            dtRentalEntry = br_dsGateInDataset.Tables(GateinData._V_RENTAL_ENTRY).Clone()
                            If blnRentalBit = True Then

                                dtRentalEntry = objGatein.GetRentalEntryDetails(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                                           CommonUIs.iLng(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                                           bv_intDepotID, _
                                                                           objTrans)
                            End If
                            If dtRentalEntry.Rows.Count > 0 Then
                                strRentalReferenceNo = CStr(dtRentalEntry.Rows(0).Item(GateinData.RNTL_RFRNC_NO))
                                intRentalCustomerID = CInt(dtRentalEntry.Rows(0).Item(GateinData.CSTMR_ID))
                                dblOtherCharge = CDec(dtRentalEntry.Rows(0).Item(GateinData.OTHR_CHRG_NC))
                            Else
                                strRentalReferenceNo = String.Empty
                                intRentalCustomerID = Nothing
                                dblOtherCharge = Nothing
                            End If
                            'If drGateIn.Item(GateinData.EQPMNT_CD_ID) Is DBNull.Value Then
                            '    dsEqpData = objConfigs.GetEquipmentCode(CStr(drGateIn.Item(GateinData.EQPMNT_CD_CD)), bv_intDepotID)
                            '    drGateIn.Item(GateinData.EQPMNT_CD_ID) = dsEqpData.Tables(CommonUIData._EQUIPMENT_CODE).Rows(0).Item(CommonUIData.EQPMNT_CD_ID).ToString
                            '    dsEqpData.Tables(CommonUIData._EQUIPMENT_CODE).Rows.Clear()
                            'End If

                            If drGateIn.Item(GateinData.EQPMNT_TYP_ID) Is DBNull.Value Then
                                If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                                    dsEqpData = objConfigs.GetEquipmentType(CStr(drGateIn.Item(GateinData.EQPMNT_TYP_CD)), CInt(objCommonUIs.GetHeadQuarterID()))
                                Else
                                    dsEqpData = objConfigs.GetEquipmentType(CStr(drGateIn.Item(GateinData.EQPMNT_TYP_CD)), bv_intDepotID)
                                End If
                                drGateIn.Item(GateinData.EQPMNT_TYP_ID) = dsEqpData.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                                dsEqpData.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Clear()
                            End If

                            If drGateIn.Item(GateinData.EQPMNT_STTS_ID) Is DBNull.Value Then
                                If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                                    dsEqpData = objConfigs.GetEquipmentStatus(CStr(drGateIn.Item(GateinData.EQPMNT_STTS_CD)), CInt(objCommonUIs.GetHeadQuarterID()))
                                Else
                                    dsEqpData = objConfigs.GetEquipmentStatus(CStr(drGateIn.Item(GateinData.EQPMNT_STTS_CD)), bv_intDepotID)
                                End If
                                drGateIn.Item(GateinData.EQPMNT_STTS_ID) = dsEqpData.Tables(CommonUIData._EQUIPMENT_STATUS).Rows(0).Item(CommonUIData.EQPMNT_STTS_ID).ToString
                                dsEqpData.Tables(CommonUIData._EQUIPMENT_STATUS).Rows.Clear()
                            End If

                            'From Index Pattern
                            Dim objIndexPattern As New IndexPatterns
                            strGI_TRNSCTN_NO = objIndexPattern.GetMaxReferenceNo(GateinData._GATEIN, CDate(drGateIn.Item(GateinData.GTN_DT)), objTrans, Nothing, bv_intDepotID)
                            lngGateINId = CLng(drGateIn.Item(GateinData.GTN_ID))

                            If bv_strGWSValue.ToLower = "true" Then

                                'strEirNo = strGI_TRNSCTN_NO
                                'From index pattern
                                strEirNo = objIndexPattern.GetMaxReferenceNo(String.Concat("GATEIN_EIR", ",", "Gatein EIR"), CDate(drGateIn.Item(GateinData.GTN_DT)), objTrans, Nothing, bv_intDepotID)
                                EirIndex = strEirNo
                                strEIRnumber = CStr(IIf(strEirNo.Length > 11, strEirNo.Substring(strEirNo.Length - 11, 11), strEirNo))
                            Else
                                strEIRnumber = strEirNo

                            End If

                            'based on 067 - Invoice Generation - GWS Key is True 
                            If bv_str067InvoiceGenerationGWSBit <> Nothing AndAlso bv_str067InvoiceGenerationGWSBit.ToUpper() = "TRUE" AndAlso intAgentID <> String.Empty AndAlso Not drGateIn.Item(GateinData.BLL_CD) Is DBNull.Value AndAlso drGateIn.Item(GateinData.BLL_CD).ToString().ToUpper() = "AGENT" Then

                            Else
                                intAgentID = Nothing

                            End If


                            lngCreated = objGatein.CreateGatein(CommonUIs.iLng(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                                drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                                CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                                CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                                CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_STTS_ID)), _
                                                                strYardLocation, _
                                                                CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                                strEventTime, _
                                                                intProdId, _
                                                                strEirNo, _
                                                                strVechicleNo, _
                                                                strTransporter, _
                                                                blnHeatingBit, _
                                                                strRemarks, _
                                                                strGI_TRNSCTN_NO, _
                                                                strRentalReferenceNo, _
                                                                False, _
                                                                blnRentalBit, _
                                                                bv_intDepotID, _
                                                                bv_strModifiedBy, _
                                                                bv_datModifiedDate, _
                                                                bv_strModifiedBy, _
                                                                bv_datModifiedDate, _
                                                                intBillId, _
                                                                strRedelAuth, _
                                                                strConsignee, _
                                                                intGradeID, _
                                                                intAgentID, _
                                                                objTrans)
                            If bv_strGWSValue.ToLower = "true" Then
                                drGateIn.Item(GateinData.BLL_ID) = intBillId
                                drGateIn.Item(GateinData.RDL_ATH) = strRedelAuth
                                drGateIn.Item(GateinData.CNSGNE) = strConsignee
                                drGateIn.Item(GateinData.GRD_ID) = intGradeID
                                'drGateIn.Item(GateinData.EIR_NO) = strGI_TRNSCTN_NO
                            End If


                            If blnRentalBit = True Then
                                Dim dtRental As New DataTable
                                Dim decHandlingInCharge As Decimal = 0
                                Dim decOffHireCharge As Decimal = 0
                                dtRentalCustomer.Rows.Clear()
                                dtRentalCustomer = br_dsGateInDataset.Tables(GateinData._CUSTOMER_RENTAL).Clone()
                                dtRentalCustomer = objGatein.GetRentalCustomer(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                                                   CommonUIs.iInt(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                                                   objTrans)

                                If dtRentalCustomer.Rows.Count > 0 Then
                                    If Not IsDBNull(dtRentalCustomer.Rows(0).Item(GateinData.OFF_HR_SRVY)) Then
                                        decOffHireCharge = CDec(dtRentalCustomer.Rows(0).Item(GateinData.OFF_HR_SRVY))
                                    End If
                                    If Not IsDBNull(dtRentalCustomer.Rows(0).Item(GateinData.HNDLNG_IN)) Then
                                        decHandlingInCharge = CDec(dtRentalCustomer.Rows(0).Item(GateinData.HNDLNG_IN))
                                    End If
                                End If

                                objGatein.UpdateRentalEntry(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                            CommonUIs.iLng(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                            CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                            strRentalReferenceNo, _
                                                            bv_intDepotID, _
                                                            True,
                                                            objTrans)

                                objGatein.UpdateRentalCharge(drGateIn.Item(GateOutData.EQPMNT_NO).ToString, _
                                                               CDate(drGateIn.Item(GateOutData.GTN_DT)), _
                                                               strYardLocation, _
                                                               strRentalReferenceNo, _
                                                               bv_intDepotID, _
                                                               decOffHireCharge, _
                                                               decHandlingInCharge, _
                                                               dblOtherCharge, _
                                                               objTrans)
                            End If

                            Dim intEquipCount As Integer = 0
                            intEquipCount = CInt(objGatein.GetEquipmentInfo(drGateIn.Item(GateinData.EQPMNT_NO).ToString, bv_intDepotID, objTrans))

                            If intEquipCount > 0 Then
                                objGatein.UpdateEquipment_Info(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                               CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                               bv_intDepotID, bv_strRemarks, objTrans)
                            Else
                                Dim objEquipmentInfo As New EquipmentInformations
                                lngEquipmentInformation = objEquipmentInfo.CreateEquipmentInformation(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                                        CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                                        Nothing, _
                                                                        0, _
                                                                        0, _
                                                                        0, _
                                                                        bv_strModifiedBy, _
                                                                        bv_datModifiedDate, _
                                                                        bv_strModifiedBy, _
                                                                        bv_datModifiedDate, _
                                                                        bv_intDepotID, _
                                                                        True, _
                                                                        Nothing, _
                                                                        Nothing, _
                                                                        0, _
                                                                        Nothing, _
                                                                        0, _
                                                                        Nothing, _
                                                                        blnRentalBit, _
                                                                        Nothing, _
                                                                        objTrans)
                            End If
                            'Attchemnt
                            Dim dtPreAdvice As DataTable = objGatein.GetPreAdviceNo(drGateIn.Item(GateinData.EQPMNT_NO).ToString, bv_intDepotID, objTrans)
                            If (dtPreAdvice.Rows.Count > 0 AndAlso CInt(dtPreAdvice.Rows(0).Item("COUNT_ATTACH")) > 0) Then
                                '    For Each drPreAdvice As DataRow In dtPreAdvice.Select(GateinData.GI_TRNSCTN_NO & "'IS NULL'")
                                lngGateINId = CLng(dtPreAdvice.Rows(0).Item("GTN_ID"))
                                '  Next

                            End If

                            objGatein.UpdatePre_Advice(strGI_TRNSCTN_NO, drGateIn.Item(GateinData.EQPMNT_NO).ToString, bv_intDepotID, objTrans)
                            objGatein.UpdateTracking_PreAdvice(strGI_TRNSCTN_NO, drGateIn.Item(GateinData.EQPMNT_NO).ToString, "Pre-Advice", bv_intDepotID, objTrans)

                            Dim strFilter As String = String.Concat(GateinData.GTN_ID, " ='", drGateIn.Item(GateinData.GTN_ID), "'")
                            drAGateIn = br_dsGateInDataset.Tables(GateinData._V_GATEIN_DETAIL).Select(strFilter)

                            If drAGateIn.Length > 0 Then
                                For Each drGateinDetail As DataRow In br_dsGateInDataset.Tables(GateinData._V_GATEIN_DETAIL).Select(GateinData.GTN_ID & "='" & drGateIn.Item(GateinData.GTN_ID).ToString & "'")
                                    If (drGateinDetail.Item(GateinData.MNFCTR_DT) Is DBNull.Value) Then
                                        datMNFCTR_DT = Nothing
                                    Else
                                        If IsDate(drGateinDetail.Item(GateinData.MNFCTR_DT)) Then
                                            datMNFCTR_DT = CDate(drGateinDetail.Item(GateinData.MNFCTR_DT))
                                        Else
                                            datMNFCTR_DT = Nothing
                                        End If
                                    End If
                                    If (drGateinDetail.Item(GateinData.LST_OH_DT) Is DBNull.Value) Then
                                        datLST_OH_DT = Nothing
                                    Else
                                        If IsDate(drGateinDetail.Item(GateinData.LST_OH_DT)) Then
                                            datLST_OH_DT = CDate(drGateinDetail.Item(GateinData.LST_OH_DT))
                                        Else
                                            datLST_OH_DT = Nothing
                                        End If
                                    End If
                                    If (drGateinDetail.Item(GateinData.LIC_EXPR) Is DBNull.Value) Then
                                        datLIC_EXPR = Nothing
                                    Else
                                        If IsDate(drGateinDetail.Item(GateinData.LIC_EXPR)) Then
                                            datLIC_EXPR = CDate(drGateinDetail.Item(GateinData.LIC_EXPR))
                                        Else
                                            datLIC_EXPR = Nothing
                                        End If
                                    End If

                                    objGatein.CreateGateinDetail(lngCreated, datMNFCTR_DT, _
                                                                drGateinDetail.Item(GateinData.ACEP_CD).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.MTRL_ID)), _
                                                                CommonUIs.iDbl(drGateinDetail.Item(GateinData.GRSS_WGHT_NC)), CommonUIs.iDbl(drGateinDetail.Item(GateinData.TR_WGHT_NC)), _
                                                                CommonUIs.iLng(drGateinDetail.Item(GateinData.MSR_ID)), CommonUIs.iLng(drGateinDetail.Item(GateinData.UNT_ID)), _
                                                                drGateinDetail.Item(GateinData.LST_OH_LOC).ToString, datLST_OH_DT, _
                                                                drGateinDetail.Item(GateinData.TRCKR_CD).ToString, drGateinDetail.Item(GateinData.LOD_STTS_CD).ToString, _
                                                                CommonUIs.iLng(drGateinDetail.Item(GateinData.CNTRY_ID)), drGateinDetail.Item(GateinData.LIC_STT).ToString, _
                                                                drGateinDetail.Item(GateinData.LIC_REG).ToString, datLIC_EXPR, _
                                                                drGateinDetail.Item(GateinData.NT_1_VC).ToString, drGateinDetail.Item(GateinData.NT_2_VC).ToString, _
                                                                drGateinDetail.Item(GateinData.NT_3_VC).ToString, drGateinDetail.Item(GateinData.NT_4_VC).ToString, _
                                                                drGateinDetail.Item(GateinData.NT_5_VC).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.SL_PRTY_1_ID)), _
                                                                drGateinDetail.Item(GateinData.SL_NMBR_1).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.SL_PRTY_2_ID)), _
                                                                drGateinDetail.Item(GateinData.SL_NMBR_2).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.SL_PRTY_3_ID)), _
                                                                drGateinDetail.Item(GateinData.SL_NMBR_3).ToString, drGateinDetail.Item(GateinData.PRT_FNC_CD).ToString, _
                                                                drGateinDetail.Item(GateinData.PRT_NM).ToString, drGateinDetail.Item(GateinData.PRT_NO).ToString, _
                                                                drGateinDetail.Item(GateinData.PRT_LC_CD).ToString, drGateinDetail.Item(GateinData.VSSL_NM).ToString, _
                                                                drGateinDetail.Item(GateinData.VYG_NO).ToString, drGateinDetail.Item(GateinData.VSSL_CD).ToString, _
                                                                drGateinDetail.Item(GateinData.SHPPR_NAM).ToString, drGateinDetail.Item(GateinData.RL_ID_VC).ToString, _
                                                                drGateinDetail.Item(GateinData.RL_RMP_LOC).ToString, _
                                                                drGateinDetail.Item(GateinData.HAZ_MTL_CD).ToString, drGateinDetail.Item(GateinData.HAZ_MATL_DSC).ToString, objTrans)
                                    If bv_blnGenerateEDI Then
                                        Dim strGateInTime As String = CDate(drGateIn.Item(GateinData.GTN_DT)).ToString("hh:mm")
                                    End If
                                Next
                            End If

                            If bv_strNotesStdEdi.ToLower = "false" Then
                                If Not IsDBNull(drGateIn.Item(GateinData.PRDCT_ID)) Then
                                    dsProduct = objGatein.GetProductUN_NO(CInt(drGateIn.Item(GateinData.PRDCT_ID)), objTrans)
                                End If

                                If dsProduct.Tables(GateinData._PRODUCT).Rows.Count > 0 Then
                                    If dsProduct.Tables(GateinData._PRODUCT).Rows(0).Item(0) Is DBNull.Value Then
                                        strNote1 = ""
                                    Else
                                        strNote1 = dsProduct.Tables(GateinData._PRODUCT).Rows(0).Item("UN_NO").ToString
                                        ''Adding prefix for Note1,2,3
                                        If strNote1 <> "" Or strNote1 <> Nothing Then
                                            strNote1 = String.Concat("AAA", strNote1)
                                        Else
                                            strNote1 = ""
                                        End If
                                    End If
                                End If
                                dsCleaning = objGatein.GetCleaningDetails(CStr((drGateIn.Item(GateinData.EQPMNT_NO))), objTrans)
                                If Not dsCleaning Is Nothing Then
                                    If (dsCleaning.Tables(GateinData._CLEANING).Rows.Count) > 0 Then
                                        strNote3 = CStr(dsCleaning.Tables(GateinData._CLEANING).Rows(0).Item(GateinData.CHMCL_NM)).ToString
                                        strNote4 = CStr(dsCleaning.Tables(GateinData._CLEANING).Rows(0).Item(GateinData.LST_CLNNG_DT)).ToString
                                        If strNote3 <> "" Or strNote3 <> Nothing Then
                                            strNote3 = String.Concat("CCC", strNote3)
                                        Else
                                            strNote3 = Nothing
                                        End If
                                        If strNote4 <> "" Or strNote4 <> Nothing Then
                                            strNote4 = String.Concat("DDD", strNote4)
                                        Else
                                            strNote4 = Nothing
                                        End If
                                    Else
                                        strNote3 = Nothing
                                        strNote4 = Nothing
                                    End If

                                End If

                                If drGateIn.Item(GateinData.PRDCT_DSCRPTN_VC) Is DBNull.Value Then
                                    strNote2 = Nothing
                                Else
                                    strNote2 = drGateIn.Item(GateinData.PRDCT_DSCRPTN_VC).ToString
                                    strNote2 = String.Concat("BBB", drGateIn.Item(GateinData.PRDCT_DSCRPTN_VC).ToString)
                                End If
                                strNote5 = drGateIn.Item(GateinData.TRNSPRTR_CD).ToString
                            Else
                                drAGateIn = br_dsGateInDataset.Tables(GateinData._V_GATEIN_DETAIL).Select(strFilter)
                                If drAGateIn.Length > 0 Then
                                    strNote1 = CStr(drAGateIn(0).Item(GateinData.NT_1_VC))
                                    strNote2 = CStr(drAGateIn(0).Item(GateinData.NT_2_VC))
                                    strNote3 = CStr(drAGateIn(0).Item(GateinData.NT_3_VC))
                                    strNote4 = CStr(drAGateIn(0).Item(GateinData.NT_4_VC))
                                    strNote5 = CStr(drAGateIn(0).Item(GateinData.NT_5_VC))
                                End If
                            End If

                            Dim dtCustomerCharge As New DataTable
                            If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                                dtCustomerCharge = objGatein.GetHanldingInCharge(CInt(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                         CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                         CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                         CInt(objCommonUIs.GetHeadQuarterID()), _
                                                         objTrans)
                            Else
                                dtCustomerCharge = objGatein.GetHanldingInCharge(CInt(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                             CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                             CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                             bv_intDepotID, _
                                                             objTrans)
                            End If


                            If dtCustomerCharge.Rows.Count > 0 Then
                                decHandlingCost = CDec(dtCustomerCharge.Rows(0).Item(GateinData.HNDLNG_IN_CHRG_NC))
                            Else
                                decHandlingCost = 0
                            End If

                            'based on 067 - Invoice Generation - GWS Key is True 
                            If bv_str067InvoiceGenerationGWSBit <> Nothing AndAlso bv_str067InvoiceGenerationGWSBit.ToUpper() = "TRUE" AndAlso intAgentID <> String.Empty AndAlso Not drGateIn.Item(GateinData.BLL_CD) Is DBNull.Value AndAlso drGateIn.Item(GateinData.BLL_CD).ToString().ToUpper() = "AGENT" Then

                                Dim dtAgentCharge As DataTable = Nothing

                                dtAgentCharge = objGatein.GetAgentHanldingInCharge(intAgentID, _
                                                          CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                          CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                          bv_intDepotID, _
                                                          objTrans)

                                If dtAgentCharge.Rows.Count > 0 Then
                                    decHandlingCost = CDec(dtAgentCharge.Rows(0).Item(GateinData.HNDLNG_IN_CHRG_NC))
                                Else
                                    decHandlingCost = 0
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

                            If bv_strGWSValue.ToLower = "false" Then
                                strEirNo = strGI_TRNSCTN_NO
                            End If
                            If blnRentalBit = False Then
                                objGatein.CreateHandlingCharge(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                         CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                         CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                         "HNDIN", _
                                                         strEIRnumber, _
                                                         strEirNo, _
                                                         CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                         CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                         intFreeDays, _
                                                         0, _
                                                         decHandlingCost, _
                                                         decHandlingTaxRate, _
                                                         decHandlingTotal, _
                                                         "U", _
                                                         True, _
                                                         bv_intDepotID, _
                                                         "I", _
                                                         drGateIn.Item(GateinData.YRD_LCTN).ToString, _
                                                         drGateIn.Item(GateinData.BLLNG_TYP).ToString, _
                                                         CommonUIs.iLng(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                         blnHeatingBit, _
                                                         strGI_TRNSCTN_NO, _
                                                         intAgentID, _
                                                         objTrans)

                                objGatein.CreateStorageCharge(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                        CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                        CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                        "STR", _
                                                        strEIRnumber, _
                                                        strEirNo, _
                                                       CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                        Nothing, _
                                                        intFreeDays, _
                                                        Nothing, _
                                                        decStorageCost, _
                                                        decStorageTaxRate, _
                                                        decStorageTotal, _
                                                        "C", _
                                                        "U", _
                                                        "I", _
                                                        True, _
                                                        Nothing, _
                                                        Nothing, _
                                                        drGateIn.Item(GateinData.YRD_LCTN).ToString, _
                                                        "C", _
                                                        CommonUIs.iLng(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                        bv_intDepotID, _
                                                        blnHeatingBit, _
                                                        strGI_TRNSCTN_NO, _
                                                        intAgentID, _
                                                        objTrans)
                            End If


                            drGateIn.Item(GateinData.GTN_ID) = lngCreated


                            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drGateIn.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                           bv_intDepotID, _
                                                                                           objTrans)
                            Dim dtTracking As New DataTable
                            Dim strHistroyRemarks As String = String.Empty
                            dtTracking = br_dsGateInDataset.Tables(GateinData._TRACKING).Clone()
                            dtTracking = objGatein.GetEquipmentInfoRemaksTracking(drGateIn.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                  bv_intDepotID, _
                                                                                  "Gate Out", _
                                                                                  drGateIn.Item(CleaningInspectionData.GI_TRNSCTN_NO).ToString, _
                                                                                  objTrans).Tables(GateinData._TRACKING)
                            If dtTracking.Rows.Count > 0 Then
                                If Not IsDBNull(dtTracking.Rows(0).Item(GateinData.EQPMNT_INFRMTN_RMRKS_VC)) Then
                                    strHistroyRemarks = CStr(dtTracking.Rows(0).Item(GateinData.EQPMNT_INFRMTN_RMRKS_VC))
                                End If
                            End If

                            If strHistroyRemarks = strEquipmentInfoRemarks Then
                                strEquipmentInfoRemarks = String.Empty
                            End If
                            Dim intInvParty As Integer
                            If drGateIn.Item(GateinData.BLL_CD).ToString().ToUpper() = "AGENT" Then
                                intInvParty = CInt(intAgentID)
                            End If
                            objCommonUIs.CreateTracking(lngCreated, _
                                                        CommonUIs.iLng(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                        drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                        "Gate In", _
                                                        CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_STTS_ID)), _
                                                        CStr(lngCreated), _
                                                        CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                        drGateIn.Item(GateinData.RMRKS_VC).ToString, _
                                                        drGateIn.Item(GateinData.YRD_LCTN).ToString, _
                                                        strGI_TRNSCTN_NO, _
                                                        intInvParty, _
                                                        strEirNo, _
                                                        bv_strModifiedBy, _
                                                        bv_datModifiedDate, _
                                                        bv_strModifiedBy, _
                                                        bv_datModifiedDate, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        bv_intDepotID, _
                                                        intRentalCustomerID, _
                                                        strRentalReferenceNo, _
                                                        strEquipmentInfoRemarks, _
                                                        False, _
                                                        objTrans)
                            Dim strEIRNoActivty As String = ""
                            If drGateIn.Item(GateinData.EIR_NO).ToString.Length > 14 Then
                                strEIRNoActivty = drGateIn.Item(GateinData.EIR_NO).ToString.Substring(0, 14)
                            Else
                                strEIRNoActivty = drGateIn.Item(GateinData.EIR_NO).ToString
                            End If
                            ''Insert into Activity_Status Table
                            objGatein.CreateActivityStatus(CommonUIs.iLng(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                           drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                           CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                           CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                           CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                           Nothing, _
                                                           CommonUIs.iLng(drGateIn.Item(GateinData.PRDCT_ID)), _
                                                           Nothing, _
                                                           CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_STTS_ID)), _
                                                           "Gate In", _
                                                           CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                           drGateIn.Item(GateinData.RMRKS_VC).ToString, _
                                                           strGI_TRNSCTN_NO, _
                                                           True, _
                                                           strEIRNoActivty, _
                                                           bv_intDepotID, _
                                                           drGateIn.Item(GateinData.YRD_LCTN).ToString, _
                                                           False, _
                                                           objTrans)
                        Else
                            If br_strLockingRecords.Length > 0 Then
                                br_strLockingRecords = String.Concat(br_strLockingRecords, ", ")
                            End If
                            br_strLockingRecords = String.Concat(br_strLockingRecords, CStr(drGateIn.Item(GateinData.EQPMNT_NO)))
                        End If
                    End If
                    Dim strGateInTime1 As String = CDate(drGateIn.Item(GateinData.GTN_DT)).ToString("hh:mm")
                    Dim strMnfctrDt As String = String.Empty
                    If (datMNFCTR_DT.ToString() <> "") Then
                        strMnfctrDt = CDate(datMNFCTR_DT).ToString("dd/MM")
                    Else
                        strMnfctrDt = Nothing
                    End If
                    Dim strLstOHDt1 As String = strGateInTime1
                    Dim strLICExpDt As String = String.Empty
                    If (datLIC_EXPR.ToString() <> "") Then
                        strLICExpDt = CDate(datLIC_EXPR).ToString("dd/MM")
                    Else
                        strLICExpDt = Nothing
                    End If
                    Dim strlessor As String

                    dsCustomer = objISO.getISOCODEbyCustomer(CLng(drGateIn.Item(GateinData.CSTMR_ID)))
                    If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                        strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
                    Else
                        strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
                    End If


                    '  CStr(datLIC_EXPR)
                    Dim strEquipmentDescription As String = objGatein.GetEquipmentDescription(drGateIn.Item(GateinData.EQPMNT_TYP_ID).ToString, objTrans)
                    Dim intEIRlenghth As Int32
                    Dim intLenghth As Int32
                    Dim strTrimGI_TRNSNumber As String
                    If strEquipmentDescription.Length > 30 Then
                        strEquipmentDescription = strEquipmentDescription.Substring(0, 30)
                    End If
                    If strGI_TRNSCTN_NO.Length > 14 Then
                        intEIRlenghth = strGI_TRNSCTN_NO.Length - 14
                        intLenghth = strGI_TRNSCTN_NO.Length
                        strTrimGI_TRNSNumber = strGI_TRNSCTN_NO.Substring(intEIRlenghth, 14)
                    Else
                        strTrimGI_TRNSNumber = strGI_TRNSCTN_NO
                    End If
                    Dim strEirNumGateInRet As String
                    If strEIRnumber.ToString.Trim.Length > 14 Then
                        strEirNumGateInRet = strEIRnumber.Substring(strEIRnumber.Length - 14, 14).ToString()
                    Else
                        strEirNumGateInRet = strEIRnumber.ToString()
                    End If

                    Dim strGICndtn As String = ""
                    Dim strRedelRet As String = ""
                    Dim strGI_Advice As String = ""
                    If drGateIn.Item(GateinData.EQPMNT_STTS_ID).ToString = "23" Then 'STR
                        strGICndtn = "A"
                        strRedelRet = drGateIn.Item(GateinData.RDL_ATH).ToString
                        strGI_Advice = strEirNumGateInRet
                    ElseIf drGateIn.Item(GateinData.EQPMNT_STTS_ID).ToString = "6" Then  'INS
                        strGICndtn = "I"
                        strRedelRet = drGateIn.Item(GateinData.RDL_ATH).ToString
                        strGI_Advice = strEirNumGateInRet
                    Else
                        strGICndtn = "D"
                        strRedelRet = strEirNumGateInRet
                        strGI_Advice = strEirNumGateInRet
                    End If
                    If strGI_Advice.ToString.Length >= 14 Then
                        strGI_Advice = strGI_Advice.Substring(0, 14)
                    End If
                    Dim strEquipType As String
                    Dim objEquipType As New EquipmentTypes
                    Dim dsEquipType As EquipmentTypeDataSet = objEquipType.GetEquipmentGroupByEquipmentTypeId(CStr(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), CInt(CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_ID")))
                    If dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                        strEquipType = CStr(dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows(0).Item(EquipmentTypeData.EQPMNT_GRP_CD))
                    Else
                        strEquipType = String.Empty
                    End If
                    objGatein.CreateGateinRet(lngCreated, strGI_TRNSCTN_NO, Nothing, Nothing, CStr(DateTime.Now.ToString("yyyyMMdd")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strTrimGI_TRNSNumber, strGI_Advice, drGateIn.Item(GateinData.EQPMNT_NO).ToString, strEquipType, _
                                            strEquipmentDescription, drGateIn.Item(GateinData.EQPMNT_CD_CD).ToString, strGICndtn, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, CDate(drGateIn.Item(GateinData.GTN_DT)), strGateInTime1, _
                                            drGateIn.Item(GateinData.CSTMR_CD).ToString, strMnfctrDt, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing, strLICExpDt, strlessor, _
                                            Nothing, "", Nothing, Nothing, Nothing, CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD"), Nothing, Nothing, Nothing, Nothing, Nothing, strNote1, strNote2, Nothing, _
                                            Nothing, Nothing, strLstOHDt1, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, strNote3, strNote4, strNote5, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "U", Nothing, Nothing, Nothing, _
                                            CommonUIs.ParseWFDATA(bv_strWfdata, "USERNAME"), 1, True, drGateIn.Item(GateinData.YRD_LCTN).ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, objTrans)

                    If CDbl(bv_ClearAttachment) = 0 Then

                        'And drAGateIn.item(GateinData.COUNT_ATTACH) > 0) Then

                    End If
                    If bv_ClearAttachment = Nothing Then
                        Dim dtAttachment As New DataTable
                        dtAttachment = br_dsGateInDataset.Tables(GateinData._ATTACHMENT).Clone()
                        If br_dsGateInDataset.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                            ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                            dtAttachment = objGatein.GetAttachmentByRepairEstimateId(bv_intDepotID, "Pre-Advice", lngGateINId, objTrans).Tables(GateinData._ATTACHMENT)
                            br_dsGateInDataset.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                            'End If
                        End If
                    End If


                    For Each drAttachment As DataRow In br_dsGateInDataset.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", lngGateINId))
                        lngCreatedAttachment = objCommonUIs.CreateAttachment(lngCreated,
                                                            "GateIn", _
                                                            "", _
                                                            strGI_TRNSCTN_NO, _
                                                            CStr(drAttachment.Item(CommonUIData.ATTCHMNT_PTH)), _
                                                            CStr(drAttachment.Item(CommonUIData.ACTL_FL_NM)),
                                                            bv_strModifiedBy, _
                                                            bv_datModifiedDate, _
                                                            bv_intDepotID,
                                                            objTrans)
                    Next
                    If bv_strGWSValue.ToLower = "true" Then
                        drGateIn.Item(GateinData.EIR_NO) = EirIndex
                    End If

                Next
            ElseIf strMode = "edit" Then
                Dim datMNFCTR_DT As DateTime
                Dim datLST_OH_DT As DateTime
                Dim datLIC_EXPR As DateTime
                Dim drAGateIn As DataRow()
                '  Dim lngGateInID As Long
                Dim dtRentalEntry As New DataTable
                Dim dtRentalCustomer As New DataTable
                Dim strConsigne As String
                Dim strRedelAuth As String
                Dim bv_strBillTo As String
                Dim bv_strBillID As String
                Dim bv_strGradeID As String
                Dim bv_strGradeCD As String
                Dim intAgentID As String
                '    For Each drGateIn As DataRow In br_dsGateInDataset.Tables(GateinData._V_GATEIN).Rows
                For Each drGateIn As DataRow In br_dsGateInDataset.Tables(GateinData._V_GATEIN).Select(GateinData.CHECKED & "='True'")
                    Select Case drGateIn.RowState
                        Case DataRowState.Modified
                            If (drGateIn.Item(GateinData.YRD_LCTN) Is DBNull.Value) Then
                                strYardLocation = String.Empty
                            Else
                                strYardLocation = CStr(drGateIn.Item(GateinData.YRD_LCTN))
                            End If
                            If (drGateIn.Item(GateinData.GTN_TM) Is DBNull.Value) Then
                                strEventTime = String.Empty
                            Else
                                strEventTime = CStr(drGateIn.Item(GateinData.GTN_TM))
                            End If
                            If (drGateIn.Item(GateinData.EIR_NO) Is DBNull.Value) Then
                                strEirNo = String.Empty
                            Else
                                strEirNo = CStr(drGateIn.Item(GateinData.EIR_NO))
                            End If
                            If (drGateIn.Item(GateinData.VHCL_NO) Is DBNull.Value) Then
                                strVechicleNo = String.Empty
                            Else
                                strVechicleNo = CStr(drGateIn.Item(GateinData.VHCL_NO))
                            End If
                            If (drGateIn.Item(GateinData.TRNSPRTR_CD) Is DBNull.Value) Then
                                strTransporter = String.Empty
                            Else
                                strTransporter = CStr(drGateIn.Item(GateinData.TRNSPRTR_CD))
                            End If
                            If (drGateIn.Item(GateinData.RMRKS_VC) Is DBNull.Value) Then
                                strRemarks = String.Empty
                            Else
                                strRemarks = CStr(drGateIn.Item(GateinData.RMRKS_VC))
                            End If
                            If Not IsDBNull(drGateIn.Item(GateinData.HTNG_BT)) Then
                                blnHeatingBit = CBool(drGateIn.Item(GateinData.HTNG_BT))
                            Else
                                blnHeatingBit = False
                            End If
                            If Not IsDBNull(drGateIn.Item(GateinData.RNTL_BT)) Then
                                blnRentalBit = CBool(drGateIn.Item(GateinData.RNTL_BT))
                            Else
                                blnRentalBit = False
                            End If
                            If bv_strGWSValue.ToLower = "true" Then
                                EirIndex = CStr(drGateIn.Item(GateinData.EIR_NO))
                            End If
                            If Not IsDBNull(drGateIn.Item(GateinData.CNSGNE)) Then
                                strConsigne = drGateIn.Item(GateinData.CNSGNE).ToString
                            Else
                                strConsigne = ""
                            End If
                            If Not IsDBNull(drGateIn.Item(GateinData.RDL_ATH)) Then
                                strRedelAuth = drGateIn.Item(GateinData.RDL_ATH).ToString
                            Else
                                strRedelAuth = ""
                            End If
                            If Not IsDBNull(drGateIn.Item(GateinData.BLL_ID)) Then
                                bv_strBillID = drGateIn.Item(GateinData.BLL_ID).ToString
                            Else
                                bv_strBillID = ""
                            End If
                            If Not IsDBNull(drGateIn.Item(GateinData.BLL_CD)) Then
                                bv_strBillTo = drGateIn.Item(GateinData.BLL_CD).ToString
                            Else
                                bv_strBillTo = ""
                            End If
                            If Not IsDBNull(drGateIn.Item(GateinData.GRD_ID)) Then
                                bv_strGradeID = drGateIn.Item(GateinData.GRD_ID).ToString
                            Else
                                bv_strGradeID = ""
                            End If
                            If Not IsDBNull(drGateIn.Item(GateinData.GRD_CD)) Then
                                bv_strGradeCD = drGateIn.Item(GateinData.GRD_CD).ToString
                            Else
                                bv_strGradeCD = ""
                            End If
                            Dim intProdID As Integer
                            If Not IsDBNull(drGateIn.Item(GateinData.PRDCT_ID)) Then
                                intProdID = CInt(drGateIn.Item(GateinData.PRDCT_ID))
                            Else
                                intProdID = 0
                            End If


                            'Aove For Gate in RET Submit
                            Dim dtCustomerCharge As New DataTable
                            Dim decHandlingCost As Decimal = 0

                            If (drGateIn.Item(GateinData.AGNT_ID) Is DBNull.Value) Then
                                intAgentID = CStr(objGatein.GetMySubmitAgentIDByCode(drGateIn.Item(GateinData.AGNT_CD).ToString))
                            Else
                                intAgentID = CStr(drGateIn.Item(GateinData.AGNT_ID))
                            End If
                            If objCommonUIs.GetMultiLocationSupportConfig().ToLower = "true" Then
                                dtCustomerCharge = objGatein.GetHanldingInCharge(CInt(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                          CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                          CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                          CInt(objCommonUIs.GetHeadQuarterID()), _
                                                          objTrans)

                            Else
                                dtCustomerCharge = objGatein.GetHanldingInCharge(CInt(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                          CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                          CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                          bv_intDepotID, _
                                                          objTrans)

                            End If

                            If dtCustomerCharge.Rows.Count > 0 Then
                                decHandlingCost = CDec(dtCustomerCharge.Rows(0).Item(GateinData.HNDLNG_IN_CHRG_NC))
                            Else
                                decHandlingCost = 0
                            End If


                            ''based on 067 - Invoice Generation - GWS Key is True 
                            'If bv_str067InvoiceGenerationGWSBit <> Nothing AndAlso bv_str067InvoiceGenerationGWSBit.ToUpper() = "TRUE" AndAlso intAgentID <> String.Empty AndAlso Not drGateIn.Item(GateinData.BLL_CD) Is DBNull.Value AndAlso drGateIn.Item(GateinData.BLL_CD).ToString().ToUpper() = "AGENT" Then

                            '    Dim dtAgentCharge As DataTable = Nothing

                            '    dtAgentCharge = objGatein.GetAgentHanldingInCharge(intAgentID, _
                            '                              CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                            '                              CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                            '                              bv_intDepotID, _
                            '                              objTrans)

                            '    If dtAgentCharge.Rows.Count > 0 Then
                            '        decHandlingCost = CDec(dtAgentCharge.Rows(0).Item(GateinData.HNDLNG_IN_CHRG_NC))
                            '    Else
                            '        decHandlingCost = 0
                            '    End If

                            'Else

                            '    intAgentID = Nothing
                            'End If

                            'based on 067 - Invoice Generation - GWS Key is True 
                            If bv_str067InvoiceGenerationGWSBit <> Nothing AndAlso bv_str067InvoiceGenerationGWSBit.ToUpper() = "TRUE" AndAlso Not drGateIn.Item(GateinData.BLL_CD) Is DBNull.Value AndAlso drGateIn.Item(GateinData.BLL_CD).ToString().ToUpper() = "AGENT" Then

                                Dim dtAgentCharge As DataTable = Nothing
                                Dim dtAgent As DataTable = Nothing
                                Dim objGateOuts As New GateOuts

                                'Get Agent Id from Customer Master+
                                dtAgent = objGateOuts.GetAgenIdFromCustomer(drGateIn.Item(GateOutData.CSTMR_ID).ToString(), bv_intDepotID, objTrans)

                                If dtAgent.Rows.Count > 0 Then

                                    'Dim objGatein As New GateIns

                                    intAgentID = dtAgent.Rows(0).Item(GateOutData.AGENT_ID).ToString()

                                    dtAgentCharge = objGatein.GetAgentHanldingInCharge(intAgentID, _
                                                              CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                              CInt(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), _
                                                              bv_intDepotID, _
                                                              objTrans)

                                    If dtAgentCharge.Rows.Count > 0 Then
                                        decHandlingCost = CDec(dtAgentCharge.Rows(0).Item(GateinData.HNDLNG_IN_CHRG_NC))
                                    Else
                                        decHandlingCost = 0
                                    End If
                                End If

                            Else
                                intAgentID = Nothing

                            End If


                            objGatein.UpdateGatein(CLng(drGateIn.Item(GateinData.GTN_ID)), _
                                                   drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                   strYardLocation, _
                                                   CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                   strEventTime, _
                                                   intProdID, _
                                                   strEirNo, _
                                                   strVechicleNo, _
                                                   strTransporter, _
                                                   blnHeatingBit, _
                                                   strRemarks, _
                                                   CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)), _
                                                   bv_intDepotID, _
                                                   bv_strModifiedBy, _
                                                   bv_datModifiedDate, _
                                                   strConsigne, _
                                                   strRedelAuth, _
                                                   bv_strBillTo, _
                                                   bv_strBillID, _
                                                   bv_strGradeID, _
                                                   bv_strGradeCD, _
                                                   intAgentID, _
                                                   objTrans)

                            'gate in RET
                            Dim strGateInTime1 As String = CDate(drGateIn.Item(GateinData.GTN_DT)).ToString("hh:mm")
                            Dim strMnfctrDt1 As String = strGateInTime1
                            Dim strLstOHDt1 As String = strGateInTime1
                            Dim strLICExpDt1 As String = strGateInTime1


                            If blnRentalBit = True Then

                                Dim decHandlingInCharge As Decimal = 0
                                Dim decOffHireCharge As Decimal = 0
                                dtRentalEntry.Rows.Clear()
                                dtRentalCustomer.Rows.Clear()
                                dtRentalCustomer = br_dsGateInDataset.Tables(GateinData._CUSTOMER_RENTAL).Clone()
                                dtRentalEntry = br_dsGateInDataset.Tables(GateinData._V_RENTAL_ENTRY).Clone()
                                dtRentalCustomer = objGatein.GetRentalCustomer(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                                                   CommonUIs.iInt(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                                                   objTrans)
                                If dtRentalCustomer.Rows.Count > 0 Then
                                    If Not IsDBNull(dtRentalCustomer.Rows(0).Item(GateinData.OFF_HR_SRVY)) Then
                                        decOffHireCharge = CDec(dtRentalCustomer.Rows(0).Item(GateinData.OFF_HR_SRVY))
                                    End If
                                    If Not IsDBNull(dtRentalCustomer.Rows(0).Item(GateinData.HNDLNG_IN)) Then
                                        decHandlingInCharge = CDec(dtRentalCustomer.Rows(0).Item(GateinData.HNDLNG_IN))
                                    End If
                                End If

                                intRentalCustomerID = CInt(drGateIn.Item(GateinData.CSTMR_ID))
                                objGatein.UpdateRentalEntry(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                   CommonUIs.iLng(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                   CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                   CStr(drGateIn.Item(GateinData.RNTL_RFRNC_NO)), _
                                                   bv_intDepotID, _
                                                   True, _
                                                   objTrans)
                                objGatein.UpdateRentalChargeDetail(drGateIn.Item(GateOutData.EQPMNT_NO).ToString, _
                                                         CDate(drGateIn.Item(GateOutData.GTN_DT)), _
                                                         strYardLocation, _
                                                         CStr(drGateIn.Item(GateinData.RNTL_RFRNC_NO)), _
                                                         bv_intDepotID, _
                                                         decOffHireCharge, _
                                                         decHandlingInCharge, _
                                                         objTrans)
                            Else
                                intRentalCustomerID = Nothing
                            End If
                            Dim strFilter As String = String.Concat(GateinData.GTN_ID, " ='", drGateIn.Item(GateinData.GTN_ID), "'")
                            drAGateIn = br_dsGateInDataset.Tables(GateinData._V_GATEIN_DETAIL).Select(strFilter)
                            lngGateINId = CLng(drGateIn.Item(GateinData.GTN_ID))
                            strGI_TRNSCTN_NO = CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO))
                            strEIRnumber = strGI_TRNSCTN_NO
                            If drAGateIn.Length > 0 Then
                                For Each drGateinDetail As DataRow In br_dsGateInDataset.Tables(GateinData._V_GATEIN_DETAIL).Select(GateinData.GTN_ID & "='" & drGateIn.Item(GateinData.GTN_ID).ToString & "'")
                                    Dim intCount As Integer = CInt(objGatein.GetGateinDetailCount(CLng(drGateinDetail.Item(GateinData.GTN_ID)), objTrans))
                                    If (drGateinDetail.Item(GateinData.MNFCTR_DT) Is DBNull.Value) Then
                                        datMNFCTR_DT = Nothing
                                    Else
                                        If IsDate(drGateinDetail.Item(GateinData.MNFCTR_DT)) Then
                                            datMNFCTR_DT = CDate(drGateinDetail.Item(GateinData.MNFCTR_DT))
                                        Else
                                            datMNFCTR_DT = Nothing
                                        End If
                                    End If
                                    If (drGateinDetail.Item(GateinData.LST_OH_DT) Is DBNull.Value) Then
                                        datLST_OH_DT = Nothing
                                    Else
                                        If IsDate(drGateinDetail.Item(GateinData.LST_OH_DT)) Then
                                            datLST_OH_DT = CDate(drGateinDetail.Item(GateinData.LST_OH_DT))
                                        Else
                                            datLST_OH_DT = Nothing
                                        End If
                                    End If
                                    If (drGateinDetail.Item(GateinData.LIC_EXPR) Is DBNull.Value) Then
                                        datLIC_EXPR = Nothing
                                    Else
                                        If IsDate(drGateinDetail.Item(GateinData.LIC_EXPR)) Then
                                            datLIC_EXPR = CDate(drGateinDetail.Item(GateinData.LIC_EXPR))
                                        Else
                                            datLIC_EXPR = Nothing
                                        End If
                                    End If

                                    If intCount > 0 Then
                                        objGatein.UpdateGateinDetail(CommonUIs.iLng(drGateinDetail.Item(GateinData.GTN_DTL_ID)), CommonUIs.iLng(drGateinDetail.Item(GateinData.GTN_ID)), datMNFCTR_DT, _
                                                                                                   drGateinDetail.Item(GateinData.ACEP_CD).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.MTRL_ID)), _
                                                                                                   CommonUIs.iDbl(drGateinDetail.Item(GateinData.GRSS_WGHT_NC)), CommonUIs.iDbl(drGateinDetail.Item(GateinData.TR_WGHT_NC)), _
                                                                                                   CommonUIs.iLng(drGateinDetail.Item(GateinData.MSR_ID)), CommonUIs.iLng(drGateinDetail.Item(GateinData.UNT_ID)), _
                                                                                                   drGateinDetail.Item(GateinData.LST_OH_LOC).ToString, datLST_OH_DT, _
                                                                                                   drGateinDetail.Item(GateinData.TRCKR_CD).ToString, drGateinDetail.Item(GateinData.LOD_STTS_CD).ToString, _
                                                                                                   CommonUIs.iLng(drGateinDetail.Item(GateinData.CNTRY_ID)), drGateinDetail.Item(GateinData.LIC_STT).ToString, _
                                                                                                   drGateinDetail.Item(GateinData.LIC_REG).ToString, datLIC_EXPR, _
                                                                                                   drGateinDetail.Item(GateinData.NT_1_VC).ToString, drGateinDetail.Item(GateinData.NT_2_VC).ToString, _
                                                                                                   drGateinDetail.Item(GateinData.NT_3_VC).ToString, drGateinDetail.Item(GateinData.NT_4_VC).ToString, _
                                                                                                   drGateinDetail.Item(GateinData.NT_5_VC).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.SL_PRTY_1_ID)), _
                                                                                                   drGateinDetail.Item(GateinData.SL_NMBR_1).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.SL_PRTY_2_ID)), _
                                                                                                   drGateinDetail.Item(GateinData.SL_NMBR_2).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.SL_PRTY_3_ID)), _
                                                                                                   drGateinDetail.Item(GateinData.SL_NMBR_3).ToString, drGateinDetail.Item(GateinData.PRT_FNC_CD).ToString, _
                                                                                                   drGateinDetail.Item(GateinData.PRT_NM).ToString, drGateinDetail.Item(GateinData.PRT_NO).ToString, _
                                                                                                   drGateinDetail.Item(GateinData.PRT_LC_CD).ToString, drGateinDetail.Item(GateinData.VSSL_NM).ToString, _
                                                                                                   drGateinDetail.Item(GateinData.VYG_NO).ToString, drGateinDetail.Item(GateinData.VSSL_CD).ToString, _
                                                                                                   drGateinDetail.Item(GateinData.SHPPR_NAM).ToString, drGateinDetail.Item(GateinData.RL_ID_VC).ToString, _
                                                                                                   drGateinDetail.Item(GateinData.RL_RMP_LOC).ToString, _
                                                                                                   drGateinDetail.Item(GateinData.HAZ_MTL_CD).ToString, drGateinDetail.Item(GateinData.HAZ_MATL_DSC).ToString, _
                                                                                                   objTrans)
                                    Else
                                        objGatein.CreateGateinDetail(lngGateINId, datMNFCTR_DT, _
                                                              drGateinDetail.Item(GateinData.ACEP_CD).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.MTRL_ID)), _
                                                              CommonUIs.iDbl(drGateinDetail.Item(GateinData.GRSS_WGHT_NC)), CommonUIs.iDbl(drGateinDetail.Item(GateinData.TR_WGHT_NC)), _
                                                              CommonUIs.iLng(drGateinDetail.Item(GateinData.MSR_ID)), CommonUIs.iLng(drGateinDetail.Item(GateinData.UNT_ID)), _
                                                              drGateinDetail.Item(GateinData.LST_OH_LOC).ToString, datLST_OH_DT, _
                                                              drGateinDetail.Item(GateinData.TRCKR_CD).ToString, drGateinDetail.Item(GateinData.LOD_STTS_CD).ToString, _
                                                              CommonUIs.iLng(drGateinDetail.Item(GateinData.CNTRY_ID)), drGateinDetail.Item(GateinData.LIC_STT).ToString, _
                                                              drGateinDetail.Item(GateinData.LIC_REG).ToString, datLIC_EXPR, _
                                                              drGateinDetail.Item(GateinData.NT_1_VC).ToString, drGateinDetail.Item(GateinData.NT_2_VC).ToString, _
                                                              drGateinDetail.Item(GateinData.NT_3_VC).ToString, drGateinDetail.Item(GateinData.NT_4_VC).ToString, _
                                                              drGateinDetail.Item(GateinData.NT_5_VC).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.SL_PRTY_1_ID)), _
                                                              drGateinDetail.Item(GateinData.SL_NMBR_1).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.SL_PRTY_2_ID)), _
                                                              drGateinDetail.Item(GateinData.SL_NMBR_2).ToString, CommonUIs.iLng(drGateinDetail.Item(GateinData.SL_PRTY_3_ID)), _
                                                              drGateinDetail.Item(GateinData.SL_NMBR_3).ToString, drGateinDetail.Item(GateinData.PRT_FNC_CD).ToString, _
                                                              drGateinDetail.Item(GateinData.PRT_NM).ToString, drGateinDetail.Item(GateinData.PRT_NO).ToString, _
                                                              drGateinDetail.Item(GateinData.PRT_LC_CD).ToString, drGateinDetail.Item(GateinData.VSSL_NM).ToString, _
                                                              drGateinDetail.Item(GateinData.VYG_NO).ToString, drGateinDetail.Item(GateinData.VSSL_CD).ToString, _
                                                              drGateinDetail.Item(GateinData.SHPPR_NAM).ToString, drGateinDetail.Item(GateinData.RL_ID_VC).ToString, _
                                                              drGateinDetail.Item(GateinData.RL_RMP_LOC).ToString, _
                                                              drGateinDetail.Item(GateinData.HAZ_MTL_CD).ToString, drGateinDetail.Item(GateinData.HAZ_MATL_DSC).ToString, objTrans)
                                    End If
                                    If bv_blnGenerateEDI Then
                                        'TODO : Insert into Gatein-Ret Table 
                                        Dim strMnfctrDt As String = String.Empty
                                        If (drGateinDetail.Item(GateinData.MNFCTR_DT).ToString() <> "") Then
                                            strMnfctrDt = CDate(drGateinDetail.Item(GateinData.MNFCTR_DT)).ToString("dd/MM")
                                        Else
                                            strMnfctrDt = Nothing
                                        End If
                                        Dim strLstOHDt As String = String.Empty
                                        If (drGateinDetail.Item(GateinData.LST_OH_DT).ToString() <> "") Then
                                            strLstOHDt = CDate(drGateinDetail.Item(GateinData.LST_OH_DT)).ToString("yyyyMMdd")
                                        Else
                                            strLstOHDt = Nothing
                                        End If
                                        Dim strLICExpDt As String = String.Empty
                                        If (drGateinDetail.Item(GateinData.LIC_EXPR).ToString() <> "") Then
                                            strLICExpDt = CDate(drGateinDetail.Item(GateinData.LIC_EXPR)).ToString("dd/MM")
                                        Else
                                            strLICExpDt = Nothing
                                        End If
                                        Dim strGateInTime As String = CDate(drGateIn.Item(GateinData.GTN_DT)).ToString("hh:mm")
                                    End If

                                    'Dim strlessor As String
                                    'Dim strNote1 As String
                                    'Dim strNote3 As String
                                    'Dim strNote4 As String
                                    'Dim strNote2 As String
                                    'dsCustomer = objISO.getISOCODEbyCustomer(CLng(drGateIn.Item(GateinData.CSTMR_ID)))
                                    'If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                                    '    strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
                                    'Else
                                    '    strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
                                    'End If

                                    'dsProduct = objGatein.GetProductUN_NO(CInt(drGateIn.Item(GateinData.PRDCT_ID)))
                                    'If dsProduct.Tables(GateinData._PRODUCT).Rows(0).Item(0) Is DBNull.Value Then
                                    '    strNote1 = ""
                                    'Else
                                    '    strNote1 = CStr(dsProduct.Tables(GateinData._PRODUCT).Rows(0).Item("UN_NO")).ToString
                                    '    ' strNote1 = String.Concat("AAA", strNote1)
                                    'End If

                                    'dsCleaning = objGatein.GetCleaningDetails(CStr((drGateIn.Item(GateinData.EQPMNT_NO))))
                                    'If (dsCleaning.Tables(GateinData._CLEANING).Rows.Count) > 0 Then
                                    '    strNote3 = CStr(dsCleaning.Tables(GateinData._CLEANING).Rows(0).Item(GateinData.CHMCL_NM)).ToString
                                    '    strNote4 = CStr(dsCleaning.Tables(GateinData._CLEANING).Rows(0).Item(GateinData.LST_CLNNG_DT)).ToString
                                    '    '  strNote3 = String.Concat("CCC", strNote3)
                                    '    ' strNote4 = String.Concat("DDD", strNote4)
                                    'Else
                                    '    strNote3 = Nothing
                                    '    strNote4 = Nothing
                                    'End If

                                    'If drGateIn.Item(GateinData.PRDCT_DSCRPTN_VC) Is DBNull.Value Then
                                    '    strNote2 = Nothing
                                    'Else
                                    '    strNote2 = drGateIn.Item(GateinData.PRDCT_DSCRPTN_VC).ToString
                                    '    '  strNote2 = String.Concat("BBB", drGateIn.Item(GateinData.PRDCT_DSCRPTN_VC).ToString)
                                    'End If
                                    'Dim strEquipmentDescription As String = objGatein.GetEquipmentDescription(drGateIn.Item(GateinData.EQPMNT_CD_CD).ToString, objTrans)
                                    ''  Dim intEIRlenghth As Int32 = CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)).Length - 3
                                    'Dim strGITrans As String = CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO))

                                    ''from index pattern
                                    'Dim intEIRlenghth As Int32
                                    'Dim intLenghth As Int32
                                    'Dim strTrimEirNumber As String
                                    'If CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)).Length > 11 Then
                                    '    intEIRlenghth = CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)).Length - 11
                                    '    intLenghth = CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)).Length
                                    '    strTrimEirNumber = strEIRnumber.Substring(intEIRlenghth, 11)
                                    'Else
                                    '    strTrimEirNumber = strEIRnumber
                                    'End If

                                    'objGatein.CreateGateinRet(CLng(drGateIn.Item(GateinData.GTN_ID)), CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)), Nothing, Nothing, CStr(DateTime.Now.ToString("yyyyMMdd")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    '                          Nothing, strTrimEirNumber, drGateIn.Item(GateinData.EIR_NO).ToString, drGateIn.Item(GateinData.EQPMNT_NO).ToString, drGateIn.Item(GateinData.EQPMNT_TYP_CD).ToString, _
                                    'strEquipmentDescription, drGateIn.Item(GateinData.EQPMNT_CD_CD).ToString, "D", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, CDate(drGateIn.Item(GateinData.GTN_DT)), strGateInTime1, _
                                    'drGateIn.Item(GateinData.CSTMR_CD).ToString, strMnfctrDt1, Nothing, Nothing, Nothing, Nothing, _
                                    'Nothing, Nothing, Nothing, Nothing, strLICExpDt1, strlessor, _
                                    'Nothing, "", Nothing, Nothing, Nothing, CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD"), Nothing, Nothing, Nothing, Nothing, Nothing, strNote1, strNote2, Nothing, _
                                    'Nothing, Nothing, strLstOHDt1, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    'Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    'Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    'Nothing, strNote3, strNote4, drGateIn.Item(GateinData.TRNSPRTR_CD).ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    'Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "U", Nothing, Nothing, Nothing, _
                                    'CommonUIs.ParseWFDATA(bv_strWfdata, "USERNAME"), 1, True, drGateIn.Item(GateinData.YRD_LCTN).ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, objTrans)
                                Next
                            End If
                            'For Gate in RET

                            Dim strlessor As String
                            Dim strNote1 As String
                            Dim strNote3 As String
                            Dim strNote4 As String
                            Dim strNote2 As String
                            Dim strNote5 As String
                            dsCustomer = objISO.getISOCODEbyCustomer(CLng(drGateIn.Item(GateinData.CSTMR_ID)), objTrans)
                            If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                                strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
                            Else
                                strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
                            End If
                            If bv_strNotesStdEdi.ToLower = "false" Then
                                If Not IsDBNull(drGateIn.Item(GateinData.PRDCT_ID)) Then
                                    dsProduct = objGatein.GetProductUN_NO(CInt(drGateIn.Item(GateinData.PRDCT_ID)), objTrans)
                                    If dsProduct.Tables(GateinData._PRODUCT).Rows.Count > 0 Then
                                        If dsProduct.Tables(GateinData._PRODUCT).Rows(0).Item(0) Is DBNull.Value Or dsProduct.Tables(GateinData._PRODUCT).Rows(0).Item("UN_NO") Is DBNull.Value Then
                                            strNote1 = ""
                                        Else
                                            strNote1 = CStr(dsProduct.Tables(GateinData._PRODUCT).Rows(0).Item("UN_NO")).ToString
                                            If strNote1 <> "" Or strNote1 <> Nothing Then
                                                strNote1 = String.Concat("AAA", strNote1)
                                            Else
                                                strNote1 = ""
                                            End If

                                        End If

                                    End If
                                End If
                                dsCleaning = objGatein.GetCleaningDetails(CStr((drGateIn.Item(GateinData.EQPMNT_NO))), objTrans)
                                If (dsCleaning.Tables(GateinData._CLEANING).Rows.Count) > 0 Then
                                    strNote3 = CStr(dsCleaning.Tables(GateinData._CLEANING).Rows(0).Item(GateinData.CHMCL_NM)).ToString
                                    strNote4 = CStr(dsCleaning.Tables(GateinData._CLEANING).Rows(0).Item(GateinData.LST_CLNNG_DT)).ToString
                                    If strNote3 <> "" Or strNote3 <> Nothing Then
                                        strNote3 = String.Concat("CCC", strNote3)
                                    Else
                                        strNote3 = Nothing
                                    End If
                                    If strNote4 <> "" Or strNote4 <> Nothing Then
                                        strNote4 = String.Concat("DDD", strNote4)
                                    Else
                                        strNote4 = Nothing
                                    End If

                                Else
                                    strNote3 = Nothing
                                    strNote4 = Nothing
                                End If

                                If drGateIn.Item(GateinData.PRDCT_DSCRPTN_VC) Is DBNull.Value Then
                                    strNote2 = Nothing
                                Else
                                    strNote2 = drGateIn.Item(GateinData.PRDCT_DSCRPTN_VC).ToString
                                    strNote2 = String.Concat("BBB", drGateIn.Item(GateinData.PRDCT_DSCRPTN_VC).ToString)
                                End If
                                strNote5 = drGateIn.Item(GateinData.TRNSPRTR_CD).ToString
                            Else
                                '  Dim strFilter As String = String.Concat(GateinData.GTN_ID, " ='", drGateIn.Item(GateinData.GTN_ID), "'")
                                drAGateIn = br_dsGateInDataset.Tables(GateinData._V_GATEIN_DETAIL).Select(strFilter)
                                If drAGateIn.Length > 0 Then
                                    strNote1 = CStr(drAGateIn(0).Item(GateinData.NT_1_VC))
                                    strNote2 = CStr(drAGateIn(0).Item(GateinData.NT_2_VC))
                                    strNote3 = CStr(drAGateIn(0).Item(GateinData.NT_3_VC))
                                    strNote4 = CStr(drAGateIn(0).Item(GateinData.NT_4_VC))
                                    strNote5 = CStr(drAGateIn(0).Item(GateinData.NT_5_VC))
                                End If
                            End If
                            Dim strEquipmentDescription As String = objGatein.GetEquipmentDescription(drGateIn.Item(GateinData.EQPMNT_TYP_ID).ToString, objTrans)
                            '  Dim intEIRlenghth As Int32 = CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)).Length - 3
                            Dim strGITrans As String = CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO))

                            'from index pattern
                            Dim intEIRlenghth As Int32
                            Dim intLenghth As Int32
                            Dim strTrimEirNumber As String
                            If CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)).Length > 14 Then
                                intEIRlenghth = CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)).Length - 14
                                intLenghth = CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)).Length
                                strTrimEirNumber = strEIRnumber.Substring(intEIRlenghth, 14)
                            Else
                                strTrimEirNumber = strEIRnumber
                            End If
                            Dim strEirNumGateInRet As String
                            If drGateIn.Item(GateinData.EIR_NO).ToString.Trim.Length > 14 Then
                                strEirNumGateInRet = drGateIn.Item(GateinData.EIR_NO).ToString.Trim.Substring(drGateIn.Item(GateinData.EIR_NO).ToString.Trim.Length - 14, 14).ToString()
                            Else
                                strEirNumGateInRet = drGateIn.Item(GateinData.EIR_NO).ToString()
                            End If

                            Dim strGICndtn As String = ""
                            Dim strRedelRet As String = ""
                            Dim strGI_Advice As String = ""
                            If drGateIn.Item(GateinData.EQPMNT_STTS_ID).ToString = "23" Then 'STR
                                strGICndtn = "A"
                                strRedelRet = drGateIn.Item(GateinData.RDL_ATH).ToString
                                strGI_Advice = strEirNumGateInRet
                            ElseIf drGateIn.Item(GateinData.EQPMNT_STTS_ID).ToString = "6" Then  'INS
                                strGICndtn = "I"
                                strRedelRet = drGateIn.Item(GateinData.RDL_ATH).ToString
                                strGI_Advice = strEirNumGateInRet
                            Else
                                strGICndtn = "D"
                                strRedelRet = strEirNumGateInRet
                                strGI_Advice = strEirNumGateInRet
                            End If
                            If strEquipmentDescription.Length > 30 Then
                                strEquipmentDescription = strEquipmentDescription.Substring(0, 30)
                            End If
                            Dim strEquipType As String
                            Dim objEquipType As New EquipmentTypes
                            Dim dsEquipType As EquipmentTypeDataSet = objEquipType.GetEquipmentGroupByEquipmentTypeId(CStr(drGateIn.Item(GateinData.EQPMNT_TYP_ID)), CInt(CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_ID")))
                            If dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                                strEquipType = CStr(dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows(0).Item(EquipmentTypeData.EQPMNT_GRP_CD))
                            Else
                                strEquipType = String.Empty
                            End If
                            objGatein.CreateGateinRet(CLng(drGateIn.Item(GateinData.GTN_ID)), CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)), Nothing, Nothing, CStr(DateTime.Now.ToString("yyyyMMdd")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                      Nothing, strTrimEirNumber, strGI_Advice, drGateIn.Item(GateinData.EQPMNT_NO).ToString, strEquipType, _
                            strEquipmentDescription, drGateIn.Item(GateinData.EQPMNT_CD_CD).ToString, strGICndtn, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, CDate(drGateIn.Item(GateinData.GTN_DT)), strGateInTime1, _
                            drGateIn.Item(GateinData.CSTMR_CD).ToString, strMnfctrDt1, Nothing, Nothing, Nothing, Nothing, _
                            Nothing, Nothing, Nothing, Nothing, strLICExpDt1, strlessor, _
                            Nothing, "", Nothing, Nothing, Nothing, CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD"), Nothing, Nothing, Nothing, Nothing, Nothing, strNote1, strNote2, Nothing, _
                            Nothing, Nothing, strLstOHDt1, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                            Nothing, Nothing, Nothing, Nothing, Nothing, _
                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                            Nothing, strNote3, strNote4, strNote5, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "U", Nothing, Nothing, Nothing, _
                            CommonUIs.ParseWFDATA(bv_strWfdata, "USERNAME"), 1, True, drGateIn.Item(GateinData.YRD_LCTN).ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, objTrans)


                            If blnRentalBit = False Then

                                objGatein.UpdateHandling(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                         CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                         drGateIn.Item(GateinData.YRD_LCTN).ToString, _
                                                         strGI_TRNSCTN_NO, _
                                                         bv_intDepotID, _
                                                         blnHeatingBit, _
                                                         strEirNo, _
                                                         decHandlingCost, _
                                                         intAgentID, _
                                                         objTrans)

                                objGatein.UpdateStorage(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                        CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                        drGateIn.Item(GateinData.YRD_LCTN).ToString, _
                                                        bv_intDepotID, _
                                                        blnHeatingBit, _
                                                        strGI_TRNSCTN_NO, _
                                                        strEirNo, _
                                                        intAgentID, _
                                                        objTrans)

                            End If

                            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                                                          bv_intDepotID, _
                                                                                          objTrans)

                            Dim dtTracking As New DataTable
                            Dim strHistroyRemarks As String = String.Empty
                            dtTracking = br_dsGateInDataset.Tables(GateinData._TRACKING).Clone()
                            dtTracking = objGatein.GetEquipmentInfoRemaksTracking(drGateIn.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                  bv_intDepotID, _
                                                                                  "Gate Out", _
                                                                                  drGateIn.Item(CleaningInspectionData.GI_TRNSCTN_NO).ToString, _
                                                                                  objTrans).Tables(GateinData._TRACKING)

                            If dtTracking.Rows.Count > 0 Then
                                If Not IsDBNull(dtTracking.Rows(0).Item(GateinData.EQPMNT_INFRMTN_RMRKS_VC)) Then
                                    strHistroyRemarks = CStr(dtTracking.Rows(0).Item(GateinData.EQPMNT_INFRMTN_RMRKS_VC))
                                End If
                            End If

                            If strHistroyRemarks = strEquipmentInfoRemarks Then
                                strEquipmentInfoRemarks = String.Empty
                            End If

                            objGatein.UpdateTracking(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                     CommonUIs.iLng(drGateIn.Item(GateinData.CSTMR_ID)), _
                                                     CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                     drGateIn.Item(GateinData.RMRKS_VC).ToString, _
                                                     strGI_TRNSCTN_NO, _
                                                     strEirNo, _
                                                     bv_intDepotID, _
                                                     bv_strModifiedBy, _
                                                     bv_datModifiedDate, _
                                                     intRentalCustomerID, _
                                                     strEquipmentInfoRemarks, _
                                                     objTrans)

                            If drGateIn.Item(GateinData.EIR_NO).ToString.Length > 14 Then
                                drGateIn.Item(GateinData.EIR_NO) = drGateIn.Item(GateinData.EIR_NO).ToString.Substring(0, 14)
                            Else
                                drGateIn.Item(GateinData.EIR_NO) = drGateIn.Item(GateinData.EIR_NO).ToString
                            End If
                            Dim int_PrdId As Integer
                            If Not IsDBNull(drGateIn.Item(GateinData.PRDCT_ID)) Then
                                int_PrdId = CInt(drGateIn.Item(GateinData.PRDCT_ID))
                            Else
                                int_PrdId = 0
                            End If
                            objGatein.UpdateActivityStatus(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                           CommonUIs.iLng(drGateIn.Item(GateinData.EQPMNT_STTS_ID)), _
                                                           "Gate In", _
                                                           CDate(drGateIn.Item(GateinData.GTN_DT)), _
                                                           True, _
                                                           strGI_TRNSCTN_NO, _
                                                           drGateIn.Item(GateinData.EIR_NO).ToString, _
                                                           bv_intDepotID, _
                                                           drGateIn.Item(GateinData.RMRKS_VC).ToString, _
                                                           drGateIn.Item(GateinData.YRD_LCTN).ToString, _
                                                           int_PrdId, _
                                                           objTrans)

                            objGatein.DeleteHeating_Charge(drGateIn.Item(GateinData.EQPMNT_NO).ToString, _
                                                    strGI_TRNSCTN_NO, _
                                                    bv_intDepotID, _
                                                    objTrans)
                            'Attachment : Delete old record and add new
                            Dim blnDeleteAttachment As Boolean = False
                            '    If bv_ClearAttachment = Nothing Then
                            Dim dtAttachment As New DataTable
                            dtAttachment = br_dsGateInDataset.Tables(GateinData._ATTACHMENT).Clone()
                            If br_dsGateInDataset.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                                If Not IsDBNull(drGateIn.Item(GateinData.GTN_ID)) Then
                                    dtAttachment = objGatein.GetAttachmentByRepairEstimateId(bv_intDepotID, CLng(drGateIn.Item(GateinData.GTN_ID)), objTrans).Tables(GateinData._ATTACHMENT)
                                    br_dsGateInDataset.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                                End If
                            End If
                            ' End If
                            'Check for Photo delete
                            If CInt(bv_ClearAttachment) = 0 Then
                                Dim dtGAttachment As New DataTable
                                dtGAttachment = objGatein.GetAttachmentGateIN(bv_intDepotID, CLng(drGateIn.Item(GateinData.GTN_ID)), "GateIn", objTrans).Tables(GateinData._ATTACHMENT)

                            End If
                            For Each drAttachment As DataRow In br_dsGateInDataset.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", drGateIn.Item(GateinData.GTN_ID)))
                                If drAttachment.RowState <> DataRowState.Deleted Then
                                    blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drGateIn.Item(CommonUIData.GI_TRNSCTN_NO).ToString, _
                                                                                                      CLng(drGateIn.Item(GateinData.GTN_ID)), _
                                                                                                      drAttachment.Item(CommonUIData.RPR_ESTMT_NO).ToString, _
                                                                                                      bv_intDepotID, _
                                                                                                      objTrans)
                                Else
                                    blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO, DataRowVersion.Original).ToString, _
                                                                                                        CLng(drGateIn.Item(GateinData.GTN_ID)), _
                                                                                                     drAttachment.Item(CommonUIData.RPR_ESTMT_NO, DataRowVersion.Original).ToString, _
                                                                                                     bv_intDepotID, _
                                                                                                     objTrans)
                                End If
                            Next
                            If br_dsGateInDataset.Tables(CommonUIData._ATTACHMENT).Rows.Count = 0 Then
                                For Each drAttachment As DataRow In br_dsGateInDataset.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drGateIn.Item(GateinData.GTN_ID))))
                                    If drAttachment.RowState <> DataRowState.Deleted Then
                                        blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drGateIn.Item(CommonUIData.GI_TRNSCTN_NO).ToString, _
                                                                                                          CLng(drGateIn.Item(GateinData.GTN_ID)), _
                                                                                                          drAttachment.Item(CommonUIData.RPR_ESTMT_NO).ToString, _
                                                                                                          bv_intDepotID, _
                                                                                                          objTrans)
                                    End If
                                Next
                            End If
                            For Each drAttachment As DataRow In br_dsGateInDataset.Tables(CommonUIData._ATTACHMENT).Rows
                                If drAttachment.RowState = DataRowState.Deleted Then
                                    blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO, DataRowVersion.Original).ToString, _
                                                                                                      CLng(drGateIn.Item(GateinData.GTN_ID)), _
                                                                                                     drAttachment.Item(CommonUIData.RPR_ESTMT_NO, DataRowVersion.Original).ToString, _
                                                                                                     bv_intDepotID, _
                                                                                                     objTrans)
                                End If
                            Next

                            For Each drAttachment As DataRow In br_dsGateInDataset.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", drGateIn.Item(GateinData.GTN_ID)))
                                If CInt(bv_ClearAttachment) <> 0 Then
                                    lngCreatedAttachment = objCommonUIs.CreateAttachment(CLng(drGateIn.Item(GateinData.GTN_ID)),
                                                                        "GateIn", _
                                                                        "", _
                                                                        CStr(drGateIn.Item(GateinData.GI_TRNSCTN_NO)), _
                                                                        CStr(drAttachment.Item(CommonUIData.ATTCHMNT_PTH)), _
                                                                        CStr(drAttachment.Item(CommonUIData.ACTL_FL_NM)),
                                                                        bv_strModifiedBy, _
                                                                        bv_datModifiedDate, _
                                                                        bv_intDepotID,
                                                                        objTrans)
                                End If
                            Next


                    End Select

                    If bv_strGWSValue.ToLower = "true" Then
                        drGateIn.Item(GateinData.EIR_NO) = EirIndex
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

#Region "pvt_CreateGateinRet"

    Private Function pvt_CreateGateinRet(ByVal bv_strGI_TRANSMISSION_NO As String, _
     ByVal bv_strGI_COMPLETE As String, _
     ByVal bv_strGI_SENT_EIR As String, _
     ByVal bv_strGI_SENT_DATE As String, _
     ByVal bv_strGI_REC_EIR As String, _
     ByVal bv_strGI_REC_DATE As String, _
     ByVal bv_strGI_REC_ADDR As String, _
     ByVal bv_strGI_REC_TYPE As String, _
     ByVal bv_strGI_EXPORTED As String, _
     ByVal bv_strGI_EXPOR_DATE As String, _
     ByVal bv_strGI_IMPORTED As String, _
     ByVal bv_strGI_IMPOR_DATE As String, _
     ByVal bv_strGI_TRNSXN As String, _
     ByVal bv_strGI_ADVICE As String, _
     ByVal bv_strGI_UNIT_NBR As String, _
     ByVal bv_strGI_EQUIP_TYPE As String, _
     ByVal bv_strGI_EQUIP_DESC As String, _
     ByVal bv_strGI_EQUIP_CODE As String, _
     ByVal bv_strGI_CONDITION As String, _
     ByVal bv_strGI_COMP_ID_A As String, _
     ByVal bv_strGI_COMP_ID_N As String, _
     ByVal bv_strGI_COMP_ID_C As String, _
     ByVal bv_strGI_COMP_TYPE As String, _
     ByVal bv_strGI_COMP_DESC As String, _
     ByVal bv_strGI_COMP_CODE As String, _
     ByVal bv_datGI_EIR_DATE As DateTime, _
     ByVal bv_strGI_EIR_TIME As String, _
     ByVal bv_strGI_REFERENCE As String, _
     ByVal bv_strGI_MANU_DATE As String, _
     ByVal bv_strGI_MATERIAL As String, _
     ByVal bv_strGI_WEIGHT As String, _
     ByVal bv_strGI_MEASURE As String, _
     ByVal bv_strGI_UNITS As String, _
     ByVal bv_strGI_CSC_REEXAM As String, _
     ByVal bv_strGI_COUNTRY As String, _
     ByVal bv_strGI_LIC_STATE As String, _
     ByVal bv_strGI_LIC_REG As String, _
     ByVal bv_strGI_LIC_EXPIRE As String, _
     ByVal bv_strGI_LSR_OWNER As String, _
     ByVal bv_strGI_SEND_EDI_1 As String, _
     ByVal bv_strGI_SSL_LSE As String, _
     ByVal bv_strGI_SEND_EDI_2 As String, _
     ByVal bv_strGI_HAULIER As String, _
     ByVal bv_strGI_SEND_EDI_3 As String, _
     ByVal bv_strGI_DPT_TRM As String, _
     ByVal bv_strGI_SEND_EDI_4 As String, _
     ByVal bv_strGI_OTHERS_1 As String, _
     ByVal bv_strGI_OTHERS_2 As String, _
     ByVal bv_strGI_OTHERS_3 As String, _
     ByVal bv_strGI_OTHERS_4 As String, _
     ByVal bv_strGI_NOTE_1 As String, _
     ByVal bv_strGI_NOTE_2 As String, _
     ByVal bv_strGI_LOAD As String, _
     ByVal bv_strGI_FHWA As String, _
     ByVal bv_strGI_LAST_OH_LOC As String, _
     ByVal bv_strGI_LAST_OH_DATE As String, _
     ByVal bv_strGI_SENDER As String, _
     ByVal bv_strGI_ATTENTION As String, _
     ByVal bv_strGI_REVISION As String, _
     ByVal bv_strGI_SEND_EDI_5 As String, _
     ByVal bv_strGI_SEND_EDI_6 As String, _
     ByVal bv_strGI_SEND_EDI_7 As String, _
     ByVal bv_strGI_SEND_EDI_8 As String, _
     ByVal bv_strGI_SEAL_PARTY_1 As String, _
     ByVal bv_strGI_SEAL_NUMBER_1 As String, _
     ByVal bv_strGI_SEAL_PARTY_2 As String, _
     ByVal bv_strGI_SEAL_NUMBER_2 As String, _
     ByVal bv_strGI_SEAL_PARTY_3 As String, _
     ByVal bv_strGI_SEAL_NUMBER_3 As String, _
     ByVal bv_strGI_SEAL_PARTY_4 As String, _
     ByVal bv_strGI_SEAL_NUMBER_4 As String, _
     ByVal bv_strGI_PORT_FUNC_CODE As String, _
     ByVal bv_strGI_PORT_NAME As String, _
     ByVal bv_strGI_VESSEL_NAME As String, _
     ByVal bv_strGI_VOYAGE_NUM As String, _
     ByVal bv_strGI_HAZ_MAT_CODE As String, _
     ByVal bv_strGI_HAZ_MAT_DESC As String, _
     ByVal bv_strGI_NOTE_3 As String, _
     ByVal bv_strGI_NOTE_4 As String, _
     ByVal bv_strGI_NOTE_5 As String, _
     ByVal bv_strGI_COMP_ID_A_2 As String, _
     ByVal bv_strGI_COMP_ID_N_2 As String, _
     ByVal bv_strGI_COMP_ID_C_2 As String, _
     ByVal bv_strGI_COMP_TYPE_2 As String, _
     ByVal bv_strGI_COMP_CODE_2 As String, _
     ByVal bv_strGI_COMP_DESC_2 As String, _
     ByVal bv_strGI_SHIPPER As String, _
     ByVal bv_strGI_DRAY_ORDER As String, _
     ByVal bv_strGI_RAIL_ID As String, _
     ByVal bv_strGI_RAIL_RAMP As String, _
     ByVal bv_strGI_VESSEL_CODE As String, _
     ByVal bv_strGI_WGHT_CERT_1 As String, _
     ByVal bv_strGI_WGHT_CERT_2 As String, _
     ByVal bv_strGI_WGHT_CERT_3 As String, _
     ByVal bv_strGI_SEA_RAIL As String, _
     ByVal bv_strGI_LOC_IDENT As String, _
     ByVal bv_strGI_PORT_LOC_QUAL As String, _
     ByVal bv_strGI_STATUS As String, _
     ByVal bv_datGI_PICK_DATE As DateTime, _
     ByVal bv_strGI_ESTSTATUS As String, _
     ByVal bv_strGI_ERRSTATUS As String, _
     ByVal bv_strGI_USERNAME As String, _
     ByVal bv_i32GI_LIVE_STATUS As Int32, _
     ByVal bv_blnGI_ISACTIVE As Boolean, _
     ByVal bv_strGI_YARD_LOC As String, _
     ByVal bv_strGI_MODE_PAYMENT As String, _
     ByVal bv_strGI_BILLING_TYPE As String, _
     ByVal bv_blnGI_RESERVE_BKG As Boolean, _
     ByVal bv_strGI_RCESTSTATUS As String, _
     ByVal bv_i32OP_SNO As Int32, _
     ByVal bv_blnOP_STATUS As Boolean, _
     ByRef br_objTrans As Transactions) As Long
        Try
            Dim objGatein_Ret As New GateIns
            Dim lngCreated As Long = 0
            lngCreated = objGatein_Ret.CreateGateinRet(lngCreated, bv_strGI_TRANSMISSION_NO, _
                  bv_strGI_COMPLETE, bv_strGI_SENT_EIR, _
                  bv_strGI_SENT_DATE, bv_strGI_REC_EIR, _
                  bv_strGI_REC_DATE, bv_strGI_REC_ADDR, _
                  bv_strGI_REC_TYPE, bv_strGI_EXPORTED, _
                  bv_strGI_EXPOR_DATE, bv_strGI_IMPORTED, _
                  bv_strGI_IMPOR_DATE, bv_strGI_TRNSXN, _
                  bv_strGI_ADVICE, bv_strGI_UNIT_NBR, _
                  bv_strGI_EQUIP_TYPE, bv_strGI_EQUIP_DESC, _
                  bv_strGI_EQUIP_CODE, bv_strGI_CONDITION, _
                  bv_strGI_COMP_ID_A, bv_strGI_COMP_ID_N, _
                  bv_strGI_COMP_ID_C, bv_strGI_COMP_TYPE, _
                  bv_strGI_COMP_DESC, bv_strGI_COMP_CODE, _
                  bv_datGI_EIR_DATE, bv_strGI_EIR_TIME, _
                  bv_strGI_REFERENCE, bv_strGI_MANU_DATE, _
                  bv_strGI_MATERIAL, bv_strGI_WEIGHT, _
                  bv_strGI_MEASURE, bv_strGI_UNITS, _
                  bv_strGI_CSC_REEXAM, bv_strGI_COUNTRY, _
                  bv_strGI_LIC_STATE, bv_strGI_LIC_REG, _
                  bv_strGI_LIC_EXPIRE, bv_strGI_LSR_OWNER, _
                  bv_strGI_SEND_EDI_1, bv_strGI_SSL_LSE, _
                  bv_strGI_SEND_EDI_2, bv_strGI_HAULIER, _
                  bv_strGI_SEND_EDI_3, bv_strGI_DPT_TRM, _
                  bv_strGI_SEND_EDI_4, bv_strGI_OTHERS_1, _
                  bv_strGI_OTHERS_2, bv_strGI_OTHERS_3, _
                  bv_strGI_OTHERS_4, bv_strGI_NOTE_1, _
                  bv_strGI_NOTE_2, bv_strGI_LOAD, _
                  bv_strGI_FHWA, bv_strGI_LAST_OH_LOC, _
                  bv_strGI_LAST_OH_DATE, bv_strGI_SENDER, _
                  bv_strGI_ATTENTION, bv_strGI_REVISION, _
                  bv_strGI_SEND_EDI_5, bv_strGI_SEND_EDI_6, _
                  bv_strGI_SEND_EDI_7, bv_strGI_SEND_EDI_8, _
                  bv_strGI_SEAL_PARTY_1, bv_strGI_SEAL_NUMBER_1, _
                  bv_strGI_SEAL_PARTY_2, bv_strGI_SEAL_NUMBER_2, _
                  bv_strGI_SEAL_PARTY_3, bv_strGI_SEAL_NUMBER_3, _
                  bv_strGI_SEAL_PARTY_4, bv_strGI_SEAL_NUMBER_4, _
                  bv_strGI_PORT_FUNC_CODE, bv_strGI_PORT_NAME, _
                  bv_strGI_VESSEL_NAME, bv_strGI_VOYAGE_NUM, _
                  bv_strGI_HAZ_MAT_CODE, bv_strGI_HAZ_MAT_DESC, _
                  bv_strGI_NOTE_3, bv_strGI_NOTE_4, _
                  bv_strGI_NOTE_5, bv_strGI_COMP_ID_A_2, _
                  bv_strGI_COMP_ID_N_2, bv_strGI_COMP_ID_C_2, _
                  bv_strGI_COMP_TYPE_2, bv_strGI_COMP_CODE_2, _
                  bv_strGI_COMP_DESC_2, bv_strGI_SHIPPER, _
                  bv_strGI_DRAY_ORDER, bv_strGI_RAIL_ID, _
                  bv_strGI_RAIL_RAMP, bv_strGI_VESSEL_CODE, _
                  bv_strGI_WGHT_CERT_1, bv_strGI_WGHT_CERT_2, _
                  bv_strGI_WGHT_CERT_3, bv_strGI_SEA_RAIL, _
                  bv_strGI_LOC_IDENT, bv_strGI_PORT_LOC_QUAL, _
                  bv_strGI_STATUS, bv_datGI_PICK_DATE, _
                  bv_strGI_ESTSTATUS, bv_strGI_ERRSTATUS, _
                  bv_strGI_USERNAME, bv_i32GI_LIVE_STATUS, _
                  bv_blnGI_ISACTIVE, bv_strGI_YARD_LOC, _
                  bv_strGI_MODE_PAYMENT, bv_strGI_BILLING_TYPE, _
                  bv_blnGI_RESERVE_BKG, bv_strGI_RCESTSTATUS, _
                  bv_i32OP_SNO, bv_blnOP_STATUS, br_objTrans)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "GetGateInDetailforMoreInfo() TABLE NAME:gatein"

    <OperationContract()> _
    Public Function GetGateInPreAdviceDetail(ByVal bv_DeoptID As Integer) As GateinDataSet

        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.GetGateInPreAdviceDetail(bv_DeoptID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetGateIn() TABLE NAME:GATEIN"
    Public Function pub_GetGateIn(ByVal bv_DeoptID As Integer) As GateinDataSet
        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.GetGateIn(bv_DeoptID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEqupimentStatus() TABLE NAME:EQUIPMENT_STATUS"
    <OperationContract()> _
    Public Function pub_GetEqupimentStatus(ByVal bv_intDPT_ID As Integer) As GateinDataSet
        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.pub_GetEqupimentStatus(bv_intDPT_ID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerDetail() TABLE NAME:CUSTOMER"
    <OperationContract()> _
    Public Function pub_GetCustomerDetail(ByVal bv_intDPT_ID As Integer) As GateinDataSet
        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.pub_GetCustomerDetail(bv_intDPT_ID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function GetEquipmentInformation(ByVal strEquipmentNo As String) As GateinDataSet
        Try
            Dim dsgateindata As GateinDataSet
            Dim objEquipmentInformations As New GateIns
            dsgateindata = objEquipmentInformations.GetEquipmentInformation(strEquipmentNo)
            Return dsgateindata
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_V_GateinDetail() TABLE NAME:V_GATEIN_DETAIL"

    <OperationContract()> _
    Public Function pub_V_GateinDetail(ByVal bv_intGateInID As Int64, ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Integer) As GateinDataSet

        Try
            Dim dsVGateinDetailData As GateinDataSet
            Dim objVGateinDetails As New GateIns
            dsVGateinDetailData = objVGateinDetails.GetVGateinDetailBy(bv_intGateInID, bv_strEquipmentNo, bv_intDepotID)
            Return dsVGateinDetailData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCountryDetails"
    Public Function GetCountryDetails(ByVal bv_DeoptID As Integer) As GateinDataSet
        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.GetCountryDetails(bv_DeoptID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetMaterialDetails"
    Public Function GetMaterialDetails(ByVal bv_DeoptID As Integer) As GateinDataSet
        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.GetMaterialDetails(bv_DeoptID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetMeasureDetails"
    Public Function GetMeasureDetails(ByVal bv_DeoptID As Integer) As GateinDataSet
        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.GetMeasureDetails(bv_DeoptID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetUnitDetails"
    Public Function GetUnitDetails(ByVal bv_DeoptID As Integer) As GateinDataSet
        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.GetUnitDetails(bv_DeoptID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "getPartyDetails"
    Public Function getPartyDetails(ByVal bv_DeoptID As Integer) As GateinDataSet
        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.getPartyDetails(bv_DeoptID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetRentalDetails"
    <OperationContract()> _
    Public Function GetRentalDetails(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32, ByRef br_strCustomer As String) As Boolean

        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGatein As New GateIns
            Dim blnValid As Boolean = True
            dsGateinDataSet = objGatein.GetRentalDetails(bv_strEquipmentNo, bv_intDepotID)
            If dsGateinDataSet.Tables(GateinData._V_RENTAL_ENTRY).Rows.Count > 0 Then
                blnValid = False
                br_strCustomer = CStr(dsGateinDataSet.Tables(GateinData._V_RENTAL_ENTRY).Rows(0).Item(GateinData.CSTMR_CD))
            End If
            Return blnValid
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRentalEntry() "
    <OperationContract()> _
    Public Function pub_GetRentalEntry(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32, ByRef br_GateOutBit As Boolean) As Boolean

        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGatein As New GateIns
            Dim blnValid As Boolean = True
            dsGateinDataSet = objGatein.GetRentalEntry(bv_strEquipmentNo, bv_intDepotID, br_GateOutBit)
            If dsGateinDataSet.Tables(GateinData._V_RENTAL_ENTRY).Rows.Count > 0 Then
                br_GateOutBit = CBool(dsGateinDataSet.Tables(GateinData._V_RENTAL_ENTRY).Rows(0).Item(GateinData.IS_GTOT_BT))
                blnValid = True
            Else
                blnValid = False
            End If
            Return blnValid
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetSupplierDetails() TABLE NAME:SUPPLIER_EQUIPMENT_DETAIL"
    <OperationContract()> _
    Public Function pub_GetSupplierDetails(ByVal bv_strEquipmentNo As String, ByVal bv_DeoptID As Integer) As GateinDataSet
        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.GetSupplierDetails(bv_strEquipmentNo, bv_DeoptID)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetAttchemntbyGateIN"
    <OperationContract()> _
    Public Function pub_GetAttchemntbyGateIN(ByVal bv_GateINID As Integer, ByVal bv_strActivity As String) As GateinDataSet

        Try
            Dim dsGateoutDataset As GateinDataSet
            Dim objGateouts As New GateIns
            dsGateoutDataset = objGateouts.pub_GetAttchemntbyGateIN(bv_GateINID, bv_strActivity)
            Return dsGateoutDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

    Function GetAgentNameByCustmrId(bv_strCustCode As String) As String
        Dim objGateIns As New GateIns
        Return objGateIns.GetAgentNameByCustmrId(bv_strCustCode)
    End Function

#Region "pub_GetEquipmentInformation() TABLE NAME:EQUIPMENT_INFORMATION"
    <OperationContract()> _
    Public Function pub_GetEquipmentInformation(ByVal bv_strEquipment As String) As GateinDataSet
        Try
            Dim dsGateinDataSet As GateinDataSet
            Dim objGateins As New GateIns
            dsGateinDataSet = objGateins.pub_GetEquipmentInformation(bv_strEquipment)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetInvoicingPartyById() TABLE NAME:EQUIPMENT_INFORMATION"
    Function pub_GetPartyById(ByVal bv_intPartyId As Integer) As String
        Dim objGatein As New GateIns
        Dim strInvoicingPartyCode As String
        strInvoicingPartyCode = objGatein.GetPartyById(bv_intPartyId)
        Return strInvoicingPartyCode
    End Function

#End Region

#Region "pub_ValidateStatusOfEquipment"
    <OperationContract()> _
    Public Function pub_ValidateStatusOfEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32) As Boolean

        Try
            Dim objGatein As New GateIns
            Dim intRowCount As Integer
            intRowCount = CInt(objGatein.ValidateStatusOfEquipment(bv_strEquipmentNo, bv_intDepotID))
            If intRowCount > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ValidateEquipmentNoInPreAdvice"
    <OperationContract()> _
    Public Function pub_ValidateEquipmentNoInPreAdvice(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32) As Boolean

        Try
            Dim objGatein As New GateIns
            Dim intRowCount As Integer
            Dim blnValid As Boolean = True
            intRowCount = CInt(objGatein.GetValidateEquipmentNoInPreAdvice(bv_strEquipmentNo, bv_intDepotID))
            If intRowCount > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
End Class
