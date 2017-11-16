Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Business
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework

Public Class EquipmentInspection
    <OperationContract()> _
    Function GetEquipmentInspectionDetail(ByVal bv_DeoptID As Integer) As EquipmentInspectionDataSet
        Try
            Dim dsEquipmentInspection As EquipmentInspectionDataSet
            Dim objEquipInspections As New EquipmentInspections
            dsEquipmentInspection = objEquipInspections.GetEquipmentInspectionDetail(bv_DeoptID)
            Return dsEquipmentInspection
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Function GetEquipmentInspectionMySubmits(ByVal bv_DeoptID As Integer) As EquipmentInspectionDataSet
        Try
            Dim dsEquipmentInspection As EquipmentInspectionDataSet
            Dim objEquipInspections As New EquipmentInspections
            dsEquipmentInspection = objEquipInspections.GetEquipmentInspectionMySubmits(bv_DeoptID)
            Return dsEquipmentInspection
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


#Region "pub_GetEqupimentStatus() TABLE NAME:EQUIPMENT_STATUS"
    <OperationContract()> _
    Public Function pub_GetEqupimentStatus(ByVal bv_intDPT_ID As Integer) As EquipmentInspectionDataSet
        Try
            Dim dsEquipmentInspection As EquipmentInspectionDataSet
            Dim objEquipInspection As New EquipmentInspections
            dsEquipmentInspection = objEquipInspection.pub_GetEqupimentStatus(bv_intDPT_ID)
            Return dsEquipmentInspection
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerDetail() TABLE NAME:CUSTOMER"
    <OperationContract()> _
    Public Function pub_GetCustomerDetail(ByVal bv_intDPT_ID As Integer) As EquipmentInspectionDataSet
        Try
            Dim dsEquipmentInspection As EquipmentInspectionDataSet
            Dim objEquipInspections As New EquipmentInspections
            dsEquipmentInspection = objEquipInspections.pub_GetCustomerDetail(bv_intDPT_ID)
            Return dsEquipmentInspection
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateEquipInspection"
    <OperationContract()> _
    Public Function pub_UpdateEquipInspection(ByRef br_dsEquipmentInspectionDataset As EquipmentInspectionDataSet, _
                                     ByVal bv_strWfdata As String, _
                                     ByVal bv_strModifiedBy As String, _
                                     ByVal bv_datModifiedDate As DateTime, _
                                     ByVal strMode As String, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByVal bv_strRemarks As String, _
                                     ByRef br_strLockingRecords As String) As Boolean
        Dim objTrans As New Transactions
        Dim strEirNo As String = ""
        Dim strYardLocation As String = ""
        Dim objCommonUIs As New CommonUIs
        Dim strRemarks As String = ""
        Dim strGI_TRNSCTN_NO As String = String.Empty
        Dim strEIRnumber As String = String.Empty
        Dim objEquipInspections As New EquipmentInspections
        Dim strEquipmentInfoRemarks As String = String.Empty
        Dim lngEquipmentInformation As Long = 0
        Dim lng_EQPMNT_INSPCTN_ID As Long = 0
        Dim objISO As New Customers
        Dim dsCustomer As New CustomerDataSet
        Dim lngEquipInspectionId As Long
        Try
            If (strMode = "new") Then
                For Each drEquipInspection As DataRow In br_dsEquipmentInspectionDataset.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION).Select(EquipmentInspectionData.CHECKED & "='True'")
                    Dim lngCreated As Long
                    Dim intBillId As String
                    Dim intGradeID As String
                    Dim drAEquipInspection As DataRow()
                    Dim intAgentID As String
                    Dim blnLock As Boolean = False
                    Dim bv_datEquipInspectionDate As DateTime
                    Dim bv_strInspctdBy As String
                    Dim bv_strAgentCD As String
                    Dim decInspctionCharge As Decimal

                    If drEquipInspection.RowState = DataRowState.Added Or drEquipInspection.RowState = DataRowState.Modified Then
                        ' blnLock = objEquipInspections.GetEquipInspectionLockingEquipmentByID(CStr(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO)), CLng(drEquipInspection.Item(EquipmentInspectionData.CSTMR_ID)), bv_intDepotID, objTrans)
                        If blnLock = False Then


                            bv_datEquipInspectionDate = CDate(drEquipInspection.Item(EquipmentInspectionData.INSPCTD_DT))

                            If (drEquipInspection.Item(EquipmentInspectionData.INSPCTD_BY) Is DBNull.Value) Then
                                bv_strInspctdBy = String.Empty
                            Else
                                bv_strInspctdBy = CStr(drEquipInspection.Item(EquipmentInspectionData.INSPCTD_BY))
                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.YRD_LCTN) Is DBNull.Value) Then
                                strYardLocation = String.Empty
                            Else
                                strYardLocation = CStr(drEquipInspection.Item(EquipmentInspectionData.YRD_LCTN))
                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.EIR_NO) Is DBNull.Value) Then
                                strEirNo = String.Empty
                            Else
                                strEirNo = CStr(drEquipInspection.Item(EquipmentInspectionData.EIR_NO))
                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.RMRKS_VC) Is DBNull.Value) Then
                                strRemarks = String.Empty
                            Else
                                strRemarks = CStr(drEquipInspection.Item(EquipmentInspectionData.RMRKS_VC))
                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.BLL_ID) Is DBNull.Value) Then
                                intBillId = String.Empty
                            Else
                                intBillId = CStr(drEquipInspection.Item(EquipmentInspectionData.BLL_ID))
                                'If drEquipInspection.Item(EquipmentInspectionData.BLL_ID).ToString = "CUSTOMER" Then
                                '    intBillId = 144
                                'ElseIf drEquipInspection.Item(EquipmentInspectionData.BLL_ID).ToString = "AGENT" Then
                                '    intBillId = 145
                                'End If

                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.AGNT_ID) Is DBNull.Value) Then
                                intAgentID = CStr(objEquipInspections.GetAgentIDByCode(drEquipInspection.Item(EquipmentInspectionData.AGNT_CD).ToString))
                            Else
                                intAgentID = CStr(drEquipInspection.Item(EquipmentInspectionData.AGNT_ID))
                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.AGNT_CD) Is DBNull.Value) Then
                                bv_strAgentCD = String.Empty
                                'intAgentID = CStr(objEquipInspections.GetAgentIDByCode(drEquipInspection.Item(EquipmentInspectionData.AGNT_CD).ToString))
                            Else
                                bv_strAgentCD = drEquipInspection.Item(EquipmentInspectionData.AGNT_CD).ToString
                            End If

                            If (drEquipInspection.Item(EquipmentInspectionData.GRD_ID) Is DBNull.Value OrElse drEquipInspection.Item(EquipmentInspectionData.GRD_ID).ToString = "") Then
                                'intGradeID = CStr(objEquipInspections.GetGradeIDByCode(drEquipInspection.Item(EquipmentInspectionData.GRD_CD).ToString))
                            Else
                                intGradeID = CStr(drEquipInspection.Item(EquipmentInspectionData.GRD_ID))
                            End If
                            Dim str_gradeCD As String
                            If (drEquipInspection.Item(EquipmentInspectionData.GRD_CD) Is DBNull.Value) Then
                                'intGradeID = CStr(objEquipInspections.GetGradeIDByCode(drEquipInspection.Item(EquipmentInspectionData.GRD_CD).ToString))
                            Else
                                str_gradeCD = CStr(drEquipInspection.Item(EquipmentInspectionData.GRD_CD))
                            End If
                            Dim dsEqpData As CommonUIDataSet
                            Dim objConfigs As New CommonUIs

                            If drEquipInspection.Item(EquipmentInspectionData.EQPMNT_TYP_ID) Is DBNull.Value Then
                                dsEqpData = objConfigs.GetEquipmentType(CStr(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_TYP_CD)), bv_intDepotID)
                                drEquipInspection.Item(EquipmentInspectionData.EQPMNT_TYP_ID) = dsEqpData.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                                dsEqpData.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Clear()
                            End If

                            If drEquipInspection.Item(EquipmentInspectionData.EQPMNT_STTS_ID) Is DBNull.Value Then
                                dsEqpData = objConfigs.GetEquipmentStatus(CStr(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_STTS_CD)), bv_intDepotID)
                                drEquipInspection.Item(EquipmentInspectionData.EQPMNT_STTS_ID) = dsEqpData.Tables(CommonUIData._EQUIPMENT_STATUS).Rows(0).Item(CommonUIData.EQPMNT_STTS_ID).ToString
                                dsEqpData.Tables(CommonUIData._EQUIPMENT_STATUS).Rows.Clear()
                            End If
                            strGI_TRNSCTN_NO = CStr(drEquipInspection.Item(EquipmentInspectionData.GI_TRNSCTN_NO))
                            'From Index Pattern
                            'strGI_TRNSCTN_NO = objIndexPattern.GetMaxReferenceNo(EquipmentInspectionData._EquipInspection, CDate(drEquipInspection.Item(EquipmentInspectionData.GTN_DT)), objTrans, Nothing, bv_intDepotID)
                            ' lngEquipInspectionId = CLng(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID))

                            lngCreated = objEquipInspections.CreateEquipInspection(CommonUIs.iLng(drEquipInspection.Item(EquipmentInspectionData.CSTMR_ID)), _
                                                                drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO).ToString, _
                                                                CommonUIs.iLng(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_TYP_ID)), _
                                                                CommonUIs.iLng(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_TYP_ID)), _
                                                                CommonUIs.iLng(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_STTS_ID)), _
                                                                strYardLocation, _
                                                                CDate(drEquipInspection.Item(EquipmentInspectionData.GTN_DT)), _
                                                                strEirNo, _
                                                                strRemarks, _
                                                                strGI_TRNSCTN_NO, _
                                                                bv_intDepotID, _
                                                                bv_strModifiedBy, _
                                                                bv_datModifiedDate, _
                                                                bv_strModifiedBy, _
                                                                bv_datModifiedDate, _
                                                                intBillId, _
                                                                intGradeID, _
                                                                intAgentID, _
                                                                bv_strAgentCD, _
                                                                bv_datEquipInspectionDate, _
                                                                bv_strInspctdBy, _
                                                                str_gradeCD, _
                                                                objTrans)

                            strEIRnumber = strGI_TRNSCTN_NO

                            Dim strFilter As String = String.Concat(EquipmentInspectionData.EQPMNT_INSPCTN_ID, " ='", drEquipInspection.Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID), "'")




                            Dim dtCustomerCharge As New DataTable

                            drEquipInspection.Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID) = lngCreated


                            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drEquipInspection.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                           bv_intDepotID, _
                                                                                           objTrans)
                            Dim dtTracking As New DataTable
                            Dim strHistroyRemarks As String = String.Empty

                            If strHistroyRemarks = strEquipmentInfoRemarks Then
                                strEquipmentInfoRemarks = String.Empty
                            End If
                            Dim intInvParty As Integer
                            If intBillId = "AGENT" Then
                                intInvParty = CInt(intAgentID)
                            Else
                                intInvParty = 0
                            End If
                            objCommonUIs.CreateTracking(lngCreated, _
                                                        CommonUIs.iLng(drEquipInspection.Item(EquipmentInspectionData.CSTMR_ID)), _
                                                        drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO).ToString, _
                                                        "Equipment Inspection", _
                                                        CommonUIs.iLng(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_STTS_ID)), _
                                                        CStr(lngCreated), _
                                                        CDate(drEquipInspection.Item(EquipmentInspectionData.GTN_DT)), _
                                                        drEquipInspection.Item(EquipmentInspectionData.RMRKS_VC).ToString, _
                                                        drEquipInspection.Item(EquipmentInspectionData.YRD_LCTN).ToString, _
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
                                                        0, _
                                                        Nothing, _
                                                        strEquipmentInfoRemarks, _
                                                        False, _
                                                        objTrans)
                            Dim strEIRNoActivty As String = ""
                            If drEquipInspection.Item(EquipmentInspectionData.EIR_NO).ToString.Length > 14 Then
                                strEIRNoActivty = drEquipInspection.Item(EquipmentInspectionData.EIR_NO).ToString.Substring(0, 14)
                            End If
                       
                            objEquipInspections.UpdateActivityStatus(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO).ToString, _
                                                       CommonUIs.iLng(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_STTS_ID)), _
                                                       "Equipment Inspection", _
                                                       CDate(drEquipInspection.Item(EquipmentInspectionData.GTN_DT)), _
                                                       True, _
                                                       strGI_TRNSCTN_NO, _
                                                       strEIRNoActivty, _
                                                       bv_intDepotID, _
                                                       drEquipInspection.Item(EquipmentInspectionData.RMRKS_VC).ToString, _
                                                       drEquipInspection.Item(EquipmentInspectionData.YRD_LCTN).ToString, _
                                                       CDate(drEquipInspection.Item(EquipmentInspectionData.INSPCTD_DT)), _
                                                       objTrans)
                            Dim dtCustChargeDetail As New DataTable
                            If intBillId = "CUSTOMER" Then
                                dtCustChargeDetail = objEquipInspections.GetHanldingInChargeByCustomer(CInt(drEquipInspection.Item(EquipmentInspectionData.CSTMR_ID)), _
                                                          CInt(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_CD_ID)), _
                                                          CInt(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_TYP_ID)), _
                                                          bv_intDepotID, _
                                                          objTrans)


                            ElseIf intBillId = "AGENT" And Not IsDBNull(drEquipInspection.Item(EquipmentInspectionData.AGNT_ID)) Then
                                dtCustChargeDetail = objEquipInspections.GetHanldingInChargeByAgent(CInt(drEquipInspection.Item(EquipmentInspectionData.AGNT_ID)), _
                                                          CInt(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_CD_ID)), _
                                                          CInt(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_TYP_ID)), _
                                                          bv_intDepotID, _
                                                          objTrans)
                            End If
                            Dim strAgentCustomerID As Integer
                            If dtCustChargeDetail.Rows.Count > 0 Then
                                If Not IsDBNull(dtCustChargeDetail.Rows(0).Item(EquipmentInspectionData.INSPCTN_CHRGS)) Then
                                    decInspctionCharge = CDec(dtCustChargeDetail.Rows(0).Item(EquipmentInspectionData.INSPCTN_CHRGS))
                                Else
                                    decInspctionCharge = 0
                                End If

                                If intBillId = "CUSTOMER" Then
                                    strAgentCustomerID = CInt(dtCustChargeDetail.Rows(0).Item(EquipmentInspectionData.CSTMR_ID))
                                ElseIf intBillId = "AGENT" Then
                                    strAgentCustomerID = CInt(dtCustChargeDetail.Rows(0).Item(EquipmentInspectionData.AGNT_ID))
                                End If
                                objEquipInspections.CreateInspectionCharges(CStr(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO)), _
                                                                               strAgentCustomerID, _
                                                                               bv_datEquipInspectionDate, _
                                                                               decInspctionCharge, _
                                                                               "U", _
                                                                               bv_strInspctdBy, _
                                                                               True, _
                                                                               strGI_TRNSCTN_NO, _
                                                                               Nothing, _
                                                                               Nothing, _
                                                                               bv_intDepotID, _
                                                                               CLng(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID)), _
                                                                               objTrans)
                            Else
                                decInspctionCharge = 0
                            End If


                        Else
                            If br_strLockingRecords.Length > 0 Then
                                br_strLockingRecords = String.Concat(br_strLockingRecords, ", ")
                            End If
                            br_strLockingRecords = String.Concat(br_strLockingRecords, CStr(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO)))
                        End If
                    End If
                    'Dim strEquipInspectionTime1 As String = CDate(drEquipInspection.Item(EquipmentInspectionData.GTN_DT)).ToString("hh:mm")
                    Dim strMnfctrDt As String = String.Empty

                    'Dim strLstOHDt1 As String = strEquipInspectionTime1
                    'Dim strLICExpDt As String = String.Empty

                    Dim strlessor As String
                    dsCustomer = objISO.getISOCODEbyCustomer(CLng(drEquipInspection.Item(EquipmentInspectionData.CSTMR_ID)))
                    If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                        strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
                    Else
                        strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
                    End If


                    Dim intEIRlenghth As Int32
                    Dim intLenghth As Int32
                    Dim strTrimEirNumber As String
                    If strEIRnumber.Length > 11 Then
                        intEIRlenghth = strEIRnumber.Length - 11
                        intLenghth = strEIRnumber.Length
                        strTrimEirNumber = strEIRnumber.Substring(intEIRlenghth, 11)
                    Else
                        strTrimEirNumber = strEIRnumber
                    End If
                    Dim strEirNumEquipInspectionRet As String
                    If drEquipInspection.Item(EquipmentInspectionData.EIR_NO).ToString.Trim.Length > 14 Then
                        strEirNumEquipInspectionRet = drEquipInspection.Item(EquipmentInspectionData.EIR_NO).ToString.Trim.Substring(0, 14).ToString()
                    Else
                        strEirNumEquipInspectionRet = drEquipInspection.Item(EquipmentInspectionData.EIR_NO).ToString()
                    End If
                Next
            ElseIf strMode = "edit" Then
                Dim drAEquipInspection As DataRow()
                '  Dim lngEquipInspectionID As Long
                Dim dtRentalEntry As New DataTable
                Dim dtRentalCustomer As New DataTable
                For Each drEquipInspection As DataRow In br_dsEquipmentInspectionDataset.Tables(EquipmentInspectionData._EQUIPMENT_INSPECTION).Select(EquipmentInspectionData.CHECKED & "='True'")
                    Select Case drEquipInspection.RowState
                        Case DataRowState.Modified
                            Dim strEqpmntStts As Integer
                            Dim strGradeID As String
                            Dim strGradeCD As String
                            Dim intBillId As String
                            Dim strEir_No As String
                            Dim bv_datEquipInspectionDate As DateTime
                            bv_datEquipInspectionDate = CDate(drEquipInspection.Item(EquipmentInspectionData.INSPCTD_DT))
                            If (drEquipInspection.Item(EquipmentInspectionData.YRD_LCTN) Is DBNull.Value) Then
                                strYardLocation = String.Empty
                            Else
                                strYardLocation = CStr(drEquipInspection.Item(EquipmentInspectionData.YRD_LCTN))
                            End If

                            If (drEquipInspection.Item(EquipmentInspectionData.EQPMNT_STTS_ID) Is DBNull.Value) Then
                                strEqpmntStts = 0
                            Else
                                strEqpmntStts = CInt(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_STTS_ID))
                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.EIR_NO) Is DBNull.Value) Then
                                strEir_No = String.Empty
                            Else
                                strEir_No = CStr(drEquipInspection.Item(EquipmentInspectionData.EIR_NO))
                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.GRD_ID) Is DBNull.Value) Then
                                strGradeID = String.Empty
                            Else
                                strGradeID = CStr(drEquipInspection.Item(EquipmentInspectionData.GRD_ID))
                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.GRD_CD) Is DBNull.Value) Then
                                strGradeCD = String.Empty
                            Else
                                strGradeCD = CStr(drEquipInspection.Item(EquipmentInspectionData.GRD_CD))
                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.RMRKS_VC) Is DBNull.Value) Then
                                strRemarks = String.Empty
                            Else
                                strRemarks = CStr(drEquipInspection.Item(EquipmentInspectionData.RMRKS_VC))
                            End If
                            If (drEquipInspection.Item(EquipmentInspectionData.BLL_ID) Is DBNull.Value) Then
                                intBillId = String.Empty
                            Else
                                intBillId = CStr(drEquipInspection.Item(EquipmentInspectionData.BLL_ID))
                            End If
                            objEquipInspections.UpdateEquipInspection(CLng(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID)), _
                                                   drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO).ToString, _
                                                   strYardLocation, _
                                                   CDate(drEquipInspection.Item(EquipmentInspectionData.GTN_DT)), _
                                                   strEqpmntStts, _
                                                   strEir_No, _
                                                   strRemarks, _
                                                   CStr(drEquipInspection.Item(EquipmentInspectionData.GI_TRNSCTN_NO)), _
                                                   bv_intDepotID, _
                                                   bv_strModifiedBy, _
                                                   bv_datModifiedDate, _
                                                   strGradeCD, _
                                                   strGradeID, _
                                                   intBillId, _
                                                   bv_datEquipInspectionDate, _
                                                   objTrans)
                            Dim dsCustChargeDetail As New EquipmentInspectionDataSet
                            Dim dtCustChargeDetail As DataTable = dsCustChargeDetail.Tables(EquipmentInspectionData._EQUIPMENT_INSPECTION)
                            If intBillId = "CUSTOMER" Then
                                dtCustChargeDetail = objEquipInspections.GetHanldingInChargeByCustomer(CInt(drEquipInspection.Item(EquipmentInspectionData.CSTMR_ID)), _
                                                          CInt(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_CD_ID)), _
                                                          CInt(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_TYP_ID)), _
                                                          bv_intDepotID, _
                                                          objTrans)


                            ElseIf intBillId = "AGENT" Then
                                Dim intAgentID As String
                                If (drEquipInspection.Item(EquipmentInspectionData.AGNT_ID) Is DBNull.Value) Then
                                    intAgentID = CStr(objEquipInspections.GetAgentIDByCode(drEquipInspection.Item(EquipmentInspectionData.AGNT_CD).ToString))
                                Else
                                    intAgentID = CStr(drEquipInspection.Item(EquipmentInspectionData.AGNT_ID))
                                End If
                                dtCustChargeDetail = objEquipInspections.GetHanldingInChargeByAgent(CInt(intAgentID), _
                                                          CInt(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_CD_ID)), _
                                                          CInt(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_TYP_ID)), _
                                                          bv_intDepotID, _
                                                          objTrans)
                            End If
                            If dtCustChargeDetail.Rows.Count > 0 Then
                                Dim decInspctionCharge As Decimal
                                Dim strAgentCustomerID As Integer
                                If Not IsDBNull(dtCustChargeDetail.Rows(0).Item(EquipmentInspectionData.INSPCTN_CHRGS)) Then
                                    decInspctionCharge = CDec(dtCustChargeDetail.Rows(0).Item(EquipmentInspectionData.INSPCTN_CHRGS))
                                Else
                                    decInspctionCharge = 0
                                End If

                                If intBillId = "CUSTOMER" Then
                                    strAgentCustomerID = CInt(dtCustChargeDetail.Rows(0).Item(EquipmentInspectionData.CSTMR_ID))
                                ElseIf intBillId = "AGENT" Then
                                    strAgentCustomerID = CInt(dtCustChargeDetail.Rows(0).Item(EquipmentInspectionData.AGNT_ID))
                                End If
                                Dim intInsChargeId As Integer
                                intInsChargeId = objEquipInspections.GetInspectionChargeID(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO).ToString, _
                                                                                           strAgentCustomerID, _
                                                                          bv_intDepotID)
                                objEquipInspections.UpdateInspectionCharges(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO).ToString, _
                                                                            intInsChargeId, _
                                                                            CDate(drEquipInspection.Item(EquipmentInspectionData.INSPCTD_DT)), _
                                                                            bv_intDepotID, _
                                                                            objTrans)
                            End If
                         
                            lngEquipInspectionId = CLng(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID))
                            strGI_TRNSCTN_NO = CStr(drEquipInspection.Item(EquipmentInspectionData.GI_TRNSCTN_NO))
                            strEIRnumber = strGI_TRNSCTN_NO

                            Dim strGITrans As String = CStr(drEquipInspection.Item(EquipmentInspectionData.GI_TRNSCTN_NO))

                            objEquipInspections.UpdateTracking(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO).ToString, _
                                                     CommonUIs.iLng(drEquipInspection.Item(EquipmentInspectionData.CSTMR_ID)), _
                                                     CDate(drEquipInspection.Item(EquipmentInspectionData.INSPCTD_DT)), _
                                                     drEquipInspection.Item(EquipmentInspectionData.RMRKS_VC).ToString, _
                                                     strGI_TRNSCTN_NO, _
                                                     strEirNo, _
                                                     bv_intDepotID, _
                                                     bv_strModifiedBy, _
                                                     bv_datModifiedDate, _
                                                     objTrans)

                            If drEquipInspection.Item(EquipmentInspectionData.EIR_NO).ToString.Length > 14 Then
                                drEquipInspection.Item(EquipmentInspectionData.EIR_NO) = drEquipInspection.Item(EquipmentInspectionData.EIR_NO).ToString.Substring(0, 14)
                            End If
                            objEquipInspections.UpdateActivityStatus(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_NO).ToString, _
                                                           CommonUIs.iLng(drEquipInspection.Item(EquipmentInspectionData.EQPMNT_STTS_ID)), _
                                                           "Equipment Inspection", _
                                                           CDate(drEquipInspection.Item(EquipmentInspectionData.GTN_DT)), _
                                                           True, _
                                                           strGI_TRNSCTN_NO, _
                                                           drEquipInspection.Item(EquipmentInspectionData.EIR_NO).ToString, _
                                                           bv_intDepotID, _
                                                           drEquipInspection.Item(EquipmentInspectionData.RMRKS_VC).ToString, _
                                                           drEquipInspection.Item(EquipmentInspectionData.YRD_LCTN).ToString, _
                                                           CDate(drEquipInspection.Item(EquipmentInspectionData.INSPCTD_DT)), _
                                                           objTrans)

                    End Select
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

End Class
