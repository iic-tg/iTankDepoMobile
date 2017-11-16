Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

#Region "EquipmentInformations"



Public Class EquipmentInspections
#Region "Declaration"
    Dim objData As DataObjects
    Private ds As EquipmentInspectionDataSet
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
    Private Const EquipmentInspectionUpdateQuery As String = "UPDATE EQUIPMENT_INSPECTION SET YRD_LCTN=@YRD_LCTN, GTN_DT=@GTN_DT,EQPMNT_STTS_ID=@EQPMNT_STTS_ID, GRD_CD=@GRD_CD, GRD_ID=@GRD_ID, BLL_ID=@BLL_ID, EIR_NO=@EIR_NO, RMRKS_VC=@RMRKS_VC,MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE EQPMNT_NO=@EQPMNT_NO AND EQPMNT_INSPCTN_ID=@EQPMNT_INSPCTN_ID AND DPT_ID=@DPT_ID"
    Private Const InspectionSelectQueryfromEquipInspection As String = "SELECT GTN_ID,CSTMR_ID,CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_TYP_CD,EQPMNT_CD_CD,EQPMNT_STTS_DSCRPTN_VC,EQPMNT_STTS_ID,YRD_LCTN,GTN_DT,EIR_NO,GI_TRNSCTN_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,GRD_ID,BLL_ID,AGNT_ID,AGNT_CD,GRD_CD,BLL_CD FROM V_EQUIPMENT_INSPECTION EI WHERE DPT_ID=@DPT_ID AND EQPMNT_NO in (SELECT EQPMNT_NO FROM ACTIVITY_STATUS where actv_bt=1 and EQPMNT_STTS_ID = 6 and GI_TRNSCTN_NO=EI.GI_TRNSCTN_NO) ORDER BY GTN_ID DESC"
    Private Const Equipment_Status_SelectQuery As String = "SELECT EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC FROM EQUIPMENT_STATUS WHERE ACTV_BT = 1 AND DPT_ID=@DPT_ID"
    Private Const Customer_SelectQuery As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM FROM CUSTOMER WHERE ACTV_BT = 1 AND DPT_ID=@DPT_ID"
    Private Const Activity_StatusInsertQuery As String = "INSERT INTO ACTIVITY_STATUS(ACTVTY_STTS_ID,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,GTN_DT,GTOT_DT,EQPMNT_STTS_ID,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,ACTV_BT,GI_RF_NO,DPT_ID,YRD_LCTN,INVC_GNRT_BT)VALUES(@ACTVTY_STTS_ID,@CSTMR_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@EQPMNT_CD_ID,@GTN_DT,@GTOT_DT,@EQPMNT_STTS_ID,@ACTVTY_NAM,@ACTVTY_DT,@RMRKS_VC,@GI_TRNSCTN_NO,@ACTV_BT,@GI_RF_NO,@DPT_ID,@YRD_LCTN,@INVC_GNRT_BT)"
    Private Const EquipInspectionInsertQuery As String = "INSERT INTO EQUIPMENT_INSPECTION (EQPMNT_INSPCTN_ID,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_STTS_ID,YRD_LCTN,GTN_DT,EIR_NO,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,GRD_ID,AGNT_ID,GRD_CD,BLL_ID,GI_TRNSCTN_NO,INSPCTD_BY,INSPCTD_DT,AGNT_CD,DPT_ID) VALUES (@EQPMNT_INSPCTN_ID,@CSTMR_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@EQPMNT_CD_ID,@EQPMNT_STTS_ID,@YRD_LCTN,@GTN_DT,@EIR_NO,@RMRKS_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@GRD_ID,@AGNT_ID,@GRD_CD,@BLL_ID,@GI_TRNSCTN_NO,@INSPCTD_BY,@INSPCTD_DT,@AGNT_CD,@DPT_ID)"
    Private Const TrackingUpdateQuery As String = "UPDATE TRACKING SET CSTMR_ID=@CSTMR_ID, ACTVTY_DT=@ACTVTY_DT,RFRNC_NO=@RFRNC_NO, ACTVTY_RMRKS=@ACTVTY_RMRKS,MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND EQPMNT_NO=@EQPMNT_NO AND ACTVTY_NAM='Equipment Inspection'"
    Private Const Activity_StatusUpdateQuery As String = "UPDATE ACTIVITY_STATUS SET EQPMNT_STTS_ID=@EQPMNT_STTS_ID,ACTVTY_DT=@ACTVTY_DT,GTN_DT=@GTN_DT,GI_RF_NO=@GI_RF_NO, YRD_LCTN=@YRD_LCTN,ACTVTY_NAM=@ACTVTY_NAM,INSPCTN_DT=@INSPCTN_DT WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const InspectionSelectQueryforMySubmits As String = "SELECT CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_STTS_ID,YRD_LCTN,GTN_DT,EIR_NO,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,GRD_ID,AGNT_ID,BLL_ID,GI_TRNSCTN_NO,(SELECT GRD_CD FROM GRADE WHERE GRD_ID=EI.GRD_ID) AS 'GRD_CD',INSPCTD_BY,INSPCTD_DT,EQPMNT_INSPCTN_ID,AGNT_CD,DPT_ID,(SELECT CSTMR_CD FROM CUSTOMER WHERE CSTMR_ID=EI.CSTMR_ID) CSTMR_CD,(SELECT EQPMNT_TYP_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_ID=EI.EQPMNT_TYP_ID) AS EQPMNT_TYP_CD,(SELECT EQPMNT_STTS_CD FROM EQUIPMENT_STATUS WHERE EQPMNT_STTS_ID=EI.EQPMNT_STTS_ID) AS EQPMNT_STTS_CD, (SELECT EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_ID=EI.EQPMNT_TYP_ID) AS EQPMNT_CD_CD FROM EQUIPMENT_INSPECTION EI WHERE DPT_ID=@DPT_ID AND GI_TRNSCTN_NO NOT IN (SELECT GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE EQPMNT_STTS_ID IN (SELECT EQPMNT_STTS_ID FROM V_WF_ACTIVITY WHERE WF_ACTIVITY_NAME='Inspection')) ORDER BY EQPMNT_INSPCTN_ID DESC "
    Private Const CutomerInspectionDetailSelectQuery As String = "SELECT CSTMR_CHRG_DTL_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,INSPCTN_CHRGS,ACTV_BT,DPT_ID FROM V_CUSTOMER_CHARGE_DETAIL WHERE DPT_ID=@DPT_ID AND EQPMNT_CD_ID=@EQPMNT_CD_ID AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID AND CSTMR_ID=@CSTMR_ID"
    Private Const AgentInspectionDetailSelectQuery As String = "SELECT AGNT_CHRG_DTL_ID,AGNT_ID,AGNT_CD,AGNT_NAM,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,INSPCTN_CHRGS,ACTV_BT,DPT_ID FROM V_AGENT_CHARGE_DETAIL WHERE DPT_ID=@DPT_ID AND EQPMNT_CD_ID=@EQPMNT_CD_ID AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID AND AGNT_ID=@AGNT_ID"
    Private Const InspectionCharges_InsertQuery As String = "INSERT INTO INSPECTION_CHARGES (INSPCTN_CHRG_ID,EQPMNT_NO,CSTMR_AGNT_ID,INSPCTD_DT,INSPCTN_CHRG,BLLNG_FLG,INSPCTD_BY,ACTV_BT,DPT_ID,GI_TRNSCTN_NO,DRFT_INVC_NO,FNL_INVC_NO,EQPMNT_INSPCTN_ID) VALUES (@INSPCTN_CHRG_ID,@EQPMNT_NO,@CSTMR_AGNT_ID,@INSPCTD_DT,@INSPCTN_CHRG,@BLLNG_FLG,@INSPCTD_BY,@ACTV_BT,@DPT_ID,@GI_TRNSCTN_NO,@DRFT_INVC_NO,@FNL_INVC_NO,@EQPMNT_INSPCTN_ID)"
    Private Const INSPECTIONCHARGES_UPDATEQUERY As String = "UPDATE INSPECTION_CHARGES SET INSPCTD_DT = @INSPCTD_DT WHERE INSPCTN_CHRG_ID=@INSPCTN_CHRG_ID AND EQPMNT_NO =@EQPMNT_NO AND DPT_ID=@DPT_ID"
#End Region

#Region "Constructor.."
    Sub New()
        ds = New EquipmentInspectionDataSet
    End Sub
#End Region


    Function GetEquipmentInspectionDetail(ByVal bv_intDepotId As Integer) As EquipmentInspectionDataSet
        Try

            objData = New DataObjects(InspectionSelectQueryfromEquipInspection, EquipmentInspectionData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), EquipmentInspectionData._V_EQUIPMENT_INSPECTION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetEquipmentInspectionMySubmits(ByVal bv_intDepotId As Integer) As EquipmentInspectionDataSet
        Try

            objData = New DataObjects(InspectionSelectQueryforMySubmits, EquipmentInspectionData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), EquipmentInspectionData._EQUIPMENT_INSPECTION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "pub_GetEquipInspectionStatusId"
    Public Function pub_GetEqupimentStatus(ByVal bv_intDPT_ID As Integer) As EquipmentInspectionDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EquipmentInspectionData.DPT_ID, bv_intDPT_ID)
            objData = New DataObjects(Equipment_Status_SelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), EquipmentInspectionData._EQUIPMENT_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetCustomerDetail"
    Public Function pub_GetCustomerDetail(bv_intDPT_ID As Integer) As EquipmentInspectionDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EquipmentInspectionData.DPT_ID, bv_intDPT_ID)
            objData = New DataObjects(Customer_SelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), EquipmentInspectionData._CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


    '#Region "CreateActivityStatus() TABLE NAME:Activity_Status"

    '    Public Function CreateActivityStatus(ByVal bv_i64CustomerId As Int64, _
    '                                         ByVal bv_strEquipmentNo As String, _
    '                                         ByVal bv_i64EquipmentTypeId As Int64, _
    '                                         ByVal bv_i64EquipmentCodeId As Int64, _
    '                                         ByVal bv_datEquipInspectionDate As DateTime, _
    '                                         ByVal bv_datGateOutDate As DateTime, _
    '                                         ByVal bv_i64EquipmentStatusId As Int64, _
    '                                         ByVal bv_strActivityId As String, _
    '                                         ByVal bv_datActivityDate As DateTime, _
    '                                         ByVal bv_strRemarks As String, _
    '                                         ByVal bv_strEquipInspectionTransactionNo As String, _
    '                                         ByVal bv_blnActivityBit As Boolean, _
    '                                         ByVal bv_GI_Ref_no As String, _
    '                                         ByVal bv_i32DepotId As Int32, _
    '                                         ByVal bv_strYardLocation As String,
    '                                         ByVal bv_blnInvoiceGeneratedBit As Boolean, _
    '                                         ByRef br_objTrans As Transactions) As Long
    '        Try
    '            Dim dr As DataRow
    '            Dim intMax As Long
    '            objData = New DataObjects()
    '            dr = ds.Tables(EquipmentInspectionData._ACTIVITY_STATUS).NewRow()
    '            With dr
    '                intMax = CommonUIs.GetIdentityValue(EquipmentInspectionData._ACTIVITY_STATUS, br_objTrans)
    '                .Item(EquipmentInspectionData.ACTVTY_STTS_ID) = intMax
    '                .Item(EquipmentInspectionData.CSTMR_ID) = bv_i64CustomerId
    '                .Item(EquipmentInspectionData.EQPMNT_NO) = bv_strEquipmentNo
    '                .Item(EquipmentInspectionData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
    '                If bv_i64EquipmentCodeId <> 0 Then
    '                    .Item(EquipmentInspectionData.EQPMNT_CD_ID) = bv_i64EquipmentCodeId
    '                Else
    '                    .Item(EquipmentInspectionData.EQPMNT_CD_ID) = DBNull.Value
    '                End If
    '                .Item(EquipmentInspectionData.GTN_DT) = bv_datEquipInspectionDate
    '                If bv_datGateOutDate <> Nothing Then
    '                    .Item(EquipmentInspectionData.GTOT_DT) = bv_datGateOutDate
    '                Else
    '                    .Item(EquipmentInspectionData.GTOT_DT) = DBNull.Value
    '                End If

    '                .Item(EquipmentInspectionData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
    '                .Item(EquipmentInspectionData.ACTVTY_NAM) = bv_strActivityId
    '                .Item(EquipmentInspectionData.ACTVTY_DT) = bv_datActivityDate
    '                If bv_strRemarks <> Nothing Then
    '                    .Item(EquipmentInspectionData.RMRKS_VC) = bv_strRemarks
    '                Else
    '                    .Item(EquipmentInspectionData.RMRKS_VC) = DBNull.Value
    '                End If
    '                .Item(EquipmentInspectionData.GI_TRNSCTN_NO) = bv_strEquipInspectionTransactionNo
    '                .Item(EquipmentInspectionData.ACTV_BT) = bv_blnActivityBit
    '                .Item(EquipmentInspectionData.GI_RF_NO) = bv_GI_Ref_no
    '                .Item(EquipmentInspectionData.DPT_ID) = bv_i32DepotId
    '                If bv_strYardLocation <> Nothing Then
    '                    .Item(EquipmentInspectionData.YRD_LCTN) = bv_strYardLocation
    '                Else
    '                    .Item(EquipmentInspectionData.YRD_LCTN) = DBNull.Value
    '                End If
    '            End With
    '            objData.InsertRow(dr, Activity_StatusInsertQuery, br_objTrans)
    '            dr = Nothing
    '            CreateActivityStatus = intMax
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '#End Region


#Region "CreateEquipInspection() TABLE NAME:EquipInspection"

    Public Function CreateEquipInspection(ByVal bv_i64CustomerID As Int64, _
        ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeId As Int64, _
        ByVal bv_i64EquipmentCodeId As Int64, _
        ByVal bv_i64EquipmentStatusId As Int64, _
        ByVal bv_strYardLocation As String, _
        ByVal bv_datGateInDate As DateTime, _
        ByVal bv_strEIR_NO As String, _
        ByVal bv_strRemarks As String, _
        ByRef bv_strEquipInspectionTransactionNo As String, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal intBillId As String, _
        ByVal intGradeID As String, _
        ByVal intAgentID As String, _
        ByVal bv_strAgentCD As String, _
        ByVal bv_datEquipInspectionDate As DateTime, _
        ByVal bv_strInspctdBy As String, _
        ByVal bv_strGradeCD As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInspectionData._EQUIPMENT_INSPECTION).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EquipmentInspectionData._EQUIPMENT_INSPECTION, br_objTrans)
                .Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID) = intMax

                .Item(EquipmentInspectionData.CSTMR_ID) = bv_i64CustomerID
                .Item(EquipmentInspectionData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(EquipmentInspectionData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                .Item(EquipmentInspectionData.EQPMNT_CD_ID) = bv_i64EquipmentCodeId
                .Item(EquipmentInspectionData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                If bv_strYardLocation <> Nothing Then
                    .Item(EquipmentInspectionData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(EquipmentInspectionData.YRD_LCTN) = DBNull.Value
                End If
                .Item(EquipmentInspectionData.GTN_DT) = bv_datGateInDate

                If bv_strEIR_NO <> Nothing Then
                    .Item(EquipmentInspectionData.EIR_NO) = bv_strEIR_NO
                Else
                    .Item(EquipmentInspectionData.EIR_NO) = DBNull.Value
                End If


                If bv_strRemarks <> Nothing Then
                    .Item(EquipmentInspectionData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(EquipmentInspectionData.RMRKS_VC) = DBNull.Value
                End If
                .Item(EquipmentInspectionData.GI_TRNSCTN_NO) = bv_strEquipInspectionTransactionNo

                If intBillId <> Nothing Then
                    .Item(EquipmentInspectionData.BLL_ID) = intBillId
                Else
                    .Item(EquipmentInspectionData.BLL_ID) = DBNull.Value
                End If
                If bv_strGradeCD <> Nothing Then
                    .Item(EquipmentInspectionData.GRD_CD) = bv_strGradeCD
                Else
                    .Item(EquipmentInspectionData.GRD_CD) = DBNull.Value
                End If
                If intGradeID <> Nothing Then
                    .Item(EquipmentInspectionData.GRD_ID) = intGradeID
                Else
                    .Item(EquipmentInspectionData.GRD_ID) = DBNull.Value
                End If
                If intAgentID <> Nothing Then
                    .Item(EquipmentInspectionData.AGNT_ID) = intAgentID
                Else
                    .Item(EquipmentInspectionData.AGNT_ID) = DBNull.Value
                End If
                If bv_strAgentCD <> Nothing Then
                    .Item(EquipmentInspectionData.AGNT_CD) = bv_strAgentCD
                Else
                    .Item(EquipmentInspectionData.AGNT_CD) = DBNull.Value
                End If
                If bv_strInspctdBy <> Nothing Then
                    .Item(EquipmentInspectionData.INSPCTD_BY) = bv_strInspctdBy
                Else
                    .Item(EquipmentInspectionData.INSPCTD_BY) = DBNull.Value
                End If
                .Item(EquipmentInspectionData.INSPCTD_DT) = bv_datEquipInspectionDate
                .Item(EquipmentInspectionData.DPT_ID) = bv_i32DepotId
                .Item(EquipmentInspectionData.CRTD_BY) = bv_strCreatedBy
                .Item(EquipmentInspectionData.CRTD_DT) = bv_datCreatedDate
                .Item(EquipmentInspectionData.MDFD_BY) = bv_strModifiedBy
                .Item(EquipmentInspectionData.MDFD_DT) = bv_datModifiedDate
            End With
            objData.InsertRow(dr, EquipInspectionInsertQuery, br_objTrans)
            dr = Nothing
            CreateEquipInspection = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateActivityStatus() TABLE NAME:Activity_Status"
    Public Function UpdateActivityStatus(ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_i64EquipmentStatusId As Int64, _
                                         ByVal bv_strActivity As String, _
                                         ByVal bv_datActivityDate As DateTime, _
                                         ByVal bv_avtivity_bit As Boolean, _
                                         ByVal bv_strEquipmentInspectionTransactionNo As String, _
                                         ByVal bv_GI_Ref_no As String, _
                                         ByVal bv_i32DepoID As Int32, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strYardLocation As String, _
                                         ByVal bv_datInsptnDate As DateTime, _
                                         ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInspectionData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(EquipmentInspectionData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(EquipmentInspectionData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                .Item(EquipmentInspectionData.ACTVTY_NAM) = bv_strActivity
                .Item(EquipmentInspectionData.ACTVTY_DT) = bv_datActivityDate
                .Item(EquipmentInspectionData.GTN_DT) = bv_datActivityDate
                .Item(EquipmentInspectionData.ACTV_BT) = bv_avtivity_bit
                .Item(EquipmentInspectionData.GI_TRNSCTN_NO) = bv_strEquipmentInspectionTransactionNo
                .Item(EquipmentInspectionData.GI_RF_NO) = bv_GI_Ref_no
                If bv_strRemarks <> Nothing Then
                    .Item(EquipmentInspectionData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(EquipmentInspectionData.RMRKS_VC) = DBNull.Value
                End If
                If bv_strYardLocation <> Nothing Then
                    .Item(EquipmentInspectionData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(EquipmentInspectionData.YRD_LCTN) = DBNull.Value
                End If
                .Item(EquipmentInspectionData.DPT_ID) = bv_i32DepoID
                .Item(EquipmentInspectionData.INSPCTN_DT) = bv_datInsptnDate
            End With
            UpdateActivityStatus = objData.UpdateRow(dr, Activity_StatusUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateEquipmentInspection() TABLE NAME:EquipmentInspection"

    Public Function UpdateEquipInspection(ByVal bv_i64EquipmentInspectionID As Int64, _
        ByVal bv_strEequipmentNo As String, _
        ByVal bv_strYardLocation As String, _
        ByVal bv_datGateInDat As DateTime, _
        ByVal bv_strEquipStts As Integer, _
        ByVal bv_strEIR_NO As String, _
        ByVal bv_strRemarks As String, _
        ByVal bv_strEquipmentInspectionTransactionNo As String, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_strGradeID As String, _
        ByVal bv_strGradeCd As String, _
        ByVal bv_bllID As String, _
        ByVal bv_datEquipInspection As DateTime, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInspectionData._EQUIPMENT_INSPECTION).NewRow()
            With dr
                .Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID) = bv_i64EquipmentInspectionID
                .Item(EquipmentInspectionData.EQPMNT_NO) = bv_strEequipmentNo
                .Item(EquipmentInspectionData.INSPCTD_DT) = bv_datEquipInspection
                If bv_strYardLocation <> Nothing Then
                    .Item(EquipmentInspectionData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(EquipmentInspectionData.YRD_LCTN) = DBNull.Value
                End If
                .Item(EquipmentInspectionData.GTN_DT) = bv_datGateInDat
                If bv_strEIR_NO <> Nothing Then
                    .Item(EquipmentInspectionData.EIR_NO) = bv_strEIR_NO
                Else
                    .Item(EquipmentInspectionData.EIR_NO) = DBNull.Value
                End If
                If bv_strEquipStts <> Nothing Then
                    .Item(EquipmentInspectionData.EQPMNT_STTS_ID) = bv_strEquipStts
                Else
                    .Item(EquipmentInspectionData.EQPMNT_STTS_ID) = DBNull.Value
                End If
                If bv_strGradeID <> Nothing Then
                    .Item(EquipmentInspectionData.GRD_CD) = bv_strGradeID
                Else
                    .Item(EquipmentInspectionData.GRD_CD) = DBNull.Value
                End If
                If bv_strGradeCd <> Nothing Then
                    .Item(EquipmentInspectionData.GRD_ID) = bv_strGradeCd
                Else
                    .Item(EquipmentInspectionData.GRD_ID) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(EquipmentInspectionData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(EquipmentInspectionData.RMRKS_VC) = DBNull.Value
                End If
                If bv_strEquipmentInspectionTransactionNo <> Nothing Then
                    .Item(EquipmentInspectionData.GI_TRNSCTN_NO) = bv_strEquipmentInspectionTransactionNo
                Else
                    .Item(EquipmentInspectionData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                If bv_bllID <> Nothing Then
                    .Item(EquipmentInspectionData.BLL_ID) = bv_bllID
                Else
                    .Item(EquipmentInspectionData.BLL_ID) = DBNull.Value
                End If
                .Item(EquipmentInspectionData.DPT_ID) = bv_i32DepotId
                .Item(EquipmentInspectionData.MDFD_BY) = bv_strModifiedBy
                .Item(EquipmentInspectionData.MDFD_DT) = bv_datModifiedDate
            End With
            UpdateEquipInspection = objData.UpdateRow(dr, EquipmentInspectionUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateTracking() TABLE NAME:Tracking"

    Public Function UpdateTracking(ByVal bv_strEQPMNT_NO As String, _
                ByVal bv_i64CustomerID As Int64, _
                ByVal bv_datActivity As DateTime, _
                ByVal bv_strActivityRemarks As String, _
                ByVal strGI_TRNSCTN_NO As String, _
                ByVal strReferenceNo As String, _
                ByVal bv_i32DepotId As Int32, _
                ByVal bv_ModifiedBy As String, _
                ByVal bv_ModifiedDate As DateTime, _
                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInspectionData._TRACKING).NewRow()
            With dr
                .Item(EquipmentInspectionData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(EquipmentInspectionData.CSTMR_ID) = bv_i64CustomerID
                .Item(EquipmentInspectionData.ACTVTY_DT) = bv_datActivity
                If bv_strActivityRemarks <> Nothing Then
                    .Item(EquipmentInspectionData.ACTVTY_RMRKS) = bv_strActivityRemarks
                Else
                    .Item(EquipmentInspectionData.ACTVTY_RMRKS) = DBNull.Value
                End If
                If strGI_TRNSCTN_NO <> Nothing Then
                    .Item(EquipmentInspectionData.GI_TRNSCTN_NO) = strGI_TRNSCTN_NO
                Else
                    .Item(EquipmentInspectionData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                If strReferenceNo <> Nothing Then
                    .Item(EquipmentInspectionData.RFRNC_NO) = strReferenceNo
                Else
                    .Item(EquipmentInspectionData.RFRNC_NO) = DBNull.Value
                End If
             
                .Item(EquipmentInspectionData.DPT_ID) = bv_i32DepotId
                .Item(EquipmentInspectionData.MDFD_BY) = bv_ModifiedBy
                .Item(EquipmentInspectionData.MDFD_DT) = bv_ModifiedDate
                
            End With
            UpdateTracking = objData.UpdateRow(dr, TrackingUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "GetHanldingInChargeByCustomer"
    Public Function GetHanldingInChargeByCustomer(ByVal bv_intCustomerID As Integer, _
                                        ByVal bv_intCodeID As Integer, _
                                        ByVal bv_intTypeID As Integer, _
                                        ByVal bv_intDepotID As Integer, _
                                        ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dthandlingCharge As New DataTable
            hshTable.Add(EquipmentInspectionData.CSTMR_ID, bv_intCustomerID)
            hshTable.Add(EquipmentInspectionData.EQPMNT_CD_ID, bv_intCodeID)
            hshTable.Add(EquipmentInspectionData.EQPMNT_TYP_ID, bv_intTypeID)
            hshTable.Add(EquipmentInspectionData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(CutomerInspectionDetailSelectQuery, hshTable)
            objData.Fill(dthandlingCharge)
            Return dthandlingCharge
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetHanldingInChargeByAgent"
    Public Function GetHanldingInChargeByAgent(ByVal bv_intAgentID As Integer, _
                                        ByVal bv_intCodeID As Integer, _
                                        ByVal bv_intTypeID As Integer, _
                                        ByVal bv_intDepotID As Integer, _
                                        ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dthandlingCharge As New DataTable
            hshTable.Add(EquipmentInspectionData.AGNT_ID, bv_intAgentID)
            hshTable.Add(EquipmentInspectionData.EQPMNT_CD_ID, bv_intCodeID)
            hshTable.Add(EquipmentInspectionData.EQPMNT_TYP_ID, bv_intTypeID)
            hshTable.Add(EquipmentInspectionData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(AgentInspectionDetailSelectQuery, hshTable)
            objData.Fill(dthandlingCharge)
            Return dthandlingCharge
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateInspectionCharges"
    Public Function CreateInspectionCharges(ByVal equpmntNo As String, _
                                            ByVal bv_strCstmrID As Integer, _
                                            ByVal dat_inspctdDate As DateTime, _
                                            ByVal dec_inspcCharge As Decimal, _
                                            ByVal bln_billingFlag As String, _
                                            ByVal bv_strInspectdBy As String, _
                                            ByVal activeBit As Boolean, _
                                            ByVal bv_giTransNo As String, _
                                            ByVal drftInvcNo As String, _
                                            ByVal finalInvNo As String, _
                                            ByVal intDepotID As Integer, _
                                            ByVal intEquipInspId As Long, _
                                            ByRef objTrans As Transactions) As Long

        Dim dr As DataRow
        Dim intMax As Integer
        objData = New DataObjects()
        dr = ds.Tables(EquipmentInspectionData._INSPECTION_CHARGES).NewRow()
        With dr
            intMax = CommonUIs.GetIdentityValue(EquipmentInspectionData._INSPECTION_CHARGES, objTrans)
            .Item(EquipmentInspectionData.INSPCTN_CHRG_ID) = intMax

            .Item(EquipmentInspectionData.EQPMNT_NO) = equpmntNo
            .Item(EquipmentInspectionData.CSTMR_AGNT_ID) = bv_strCstmrID

            .Item(EquipmentInspectionData.INSPCTD_DT) = dat_inspctdDate
            If dec_inspcCharge <> Nothing Then
                .Item(EquipmentInspectionData.INSPCTN_CHRG) = dec_inspcCharge
            Else
                .Item(EquipmentInspectionData.INSPCTN_CHRG) = DBNull.Value
            End If
            If bln_billingFlag <> Nothing Then
                .Item(EquipmentInspectionData.BLLNG_FLG) = bln_billingFlag
            Else
                .Item(EquipmentInspectionData.BLLNG_FLG) = DBNull.Value
            End If
            If bv_strInspectdBy <> Nothing Then
                .Item(EquipmentInspectionData.INSPCTD_BY) = bv_strInspectdBy
            Else
                .Item(EquipmentInspectionData.INSPCTD_BY) = DBNull.Value
            End If
            If activeBit <> Nothing Then
                .Item(EquipmentInspectionData.ACTV_BT) = activeBit
            Else
                .Item(EquipmentInspectionData.ACTV_BT) = DBNull.Value
            End If
            If bv_giTransNo <> Nothing Then
                .Item(EquipmentInspectionData.GI_TRNSCTN_NO) = bv_giTransNo
            Else
                .Item(EquipmentInspectionData.GI_TRNSCTN_NO) = DBNull.Value
            End If

            If drftInvcNo <> Nothing Then
                .Item(EquipmentInspectionData.DRFT_INVC_NO) = drftInvcNo
            Else
                .Item(EquipmentInspectionData.DRFT_INVC_NO) = DBNull.Value
            End If
            If finalInvNo <> Nothing Then
                .Item(EquipmentInspectionData.FNL_INVC_NO) = finalInvNo
            Else
                .Item(EquipmentInspectionData.FNL_INVC_NO) = DBNull.Value
            End If
            .Item(EquipmentInspectionData.DPT_ID) = intDepotID
            .Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID) = intEquipInspId
        End With
        objData.InsertRow(dr, InspectionCharges_InsertQuery, objTrans)
        dr = Nothing
        Return intMax
    End Function
#End Region

#Region "UpdateInspectionCharges()"
    Public Function UpdateInspectionCharges(ByVal bv_strEquipmentNo As String, _
                                            ByVal int_inspctChargeID As Integer, _
                                            ByVal dat_inspctdDate As DateTime, _
                                            ByVal bv_i32DepoID As Int32, _
                                            ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInspectionData._INSPECTION_CHARGES).NewRow()
            With dr
                .Item(EquipmentInspectionData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(EquipmentInspectionData.INSPCTN_CHRG_ID) = int_inspctChargeID
                If dat_inspctdDate <> Nothing Then
                    .Item(EquipmentInspectionData.INSPCTD_DT) = dat_inspctdDate
                Else
                    .Item(EquipmentInspectionData.INSPCTD_DT) = DBNull.Value
                End If
                .Item(EquipmentInspectionData.DPT_ID) = bv_i32DepoID
            End With
            UpdateInspectionCharges = objData.UpdateRow(dr, INSPECTIONCHARGES_UPDATEQUERY, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    Function GetAgentIDByCode(bv_strAgentCd As String) As String
        Dim hshTable As New Hashtable
        Dim dsAgntName As String
        hshTable = New Hashtable
        hshTable.Add(AgentData.AGNT_CD, bv_strAgentCd)
        objData = New DataObjects("Select AGNT_ID from Agent where AGNT_CD=@AGNT_CD", hshTable)
        dsAgntName = objData.ExecuteScalar()
        Return dsAgntName
    End Function
    Function GetInspectionChargeID(bv_EquipNo As String, bv_Cstmrid As Integer, bv_intDptID As Integer) As Integer
        Dim hshTable As New Hashtable
        Dim inspcChargeID As String
        hshTable = New Hashtable
        hshTable.Add(EquipmentInspectionData.EQPMNT_NO, bv_EquipNo)
        hshTable.Add(EquipmentInspectionData.CSTMR_AGNT_ID, bv_Cstmrid)
        hshTable.Add(EquipmentInspectionData.DPT_ID, bv_intDptID)
        hshTable.Add(EquipmentInspectionData.ACTV_BT, 1)
        objData = New DataObjects("select INSPCTN_CHRG_ID from INSPECTION_CHARGES where EQPMNT_NO=@EQPMNT_NO and CSTMR_AGNT_ID=@CSTMR_AGNT_ID and DPT_ID=@DPT_ID and ACTV_BT=@ACTV_BT", hshTable)
        inspcChargeID = CInt(objData.ExecuteScalar())
        Return inspcChargeID
    End Function
End Class

#End Region

