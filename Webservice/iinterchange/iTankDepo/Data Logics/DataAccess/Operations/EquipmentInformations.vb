Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

#Region "EquipmentInformations"

Public Class EquipmentInformations

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_Equipment_InformationSelectQuery As String = "SELECT EQPMNT_INFRMTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,MNFCTR_DT,TR_WGHT_NC,GRSS_WGHT_NC,CPCTY_NC,LST_TST_DT,NXT_TST_DT,VLDTY_PRD_TST_YRS,LST_TST_TYP_ID,LST_TST_TYP_CD,NXT_TST_TYP_ID,NXT_TST_TYP_CD,LST_SRVYR_NM,ACTV_BT,RNTL_BT,DPT_ID,DPT_CD,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,CASE WHEN (SELECT EQPMNT_NO FROM ACTIVITY_STATUS WHERE DPT_ID=V_EQUIPMENT_INFORMATION.DPT_ID AND EQPMNT_NO=V_EQUIPMENT_INFORMATION.EQPMNT_NO) = NULL THEN 1 ELSE 0 END ALLOW_EDIT,RMRKS_VC,RNTL_EDT_BT,COUNT_ATTACH,PRVS_ONHR_LCTN_ID,PRVS_ONHR_LCTN_CD,PRVS_ONHR_DT,CSC_VLDTY,EQPMNT_CD_ID,EQPMNT_CD_CD FROM V_EQUIPMENT_INFORMATION WHERE DPT_ID=@DPT_ID ORDER BY ACTV_BT DESC , EQPMNT_INFRMTN_ID "
    Private Const Equipment_InformationInsertQuery As String = "INSERT INTO EQUIPMENT_INFORMATION(EQPMNT_INFRMTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,MNFCTR_DT,TR_WGHT_NC,GRSS_WGHT_NC,CPCTY_NC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,ACTV_BT,LST_SRVYR_NM,LST_TST_DT,LST_TST_TYP_ID,NXT_TST_DT,NXT_TST_TYP_ID,VLDTY_PRD_TST_YRS,RNTL_BT,RMRKS_VC,PRVS_ONHR_LCTN_ID,PRVS_ONHR_DT,CSC_VLDTY,EQPMNT_CD_ID)VALUES(@EQPMNT_INFRMTN_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@MNFCTR_DT,@TR_WGHT_NC,@GRSS_WGHT_NC,@CPCTY_NC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@DPT_ID,@ACTV_BT,@LST_SRVYR_NM,@LST_TST_DT,@LST_TST_TYP_ID,@NXT_TST_DT,@NXT_TST_TYP_ID,@VLDTY_PRD_TST_YRS,@RNTL_BT,@RMRKS_VC,@PRVS_ONHR_LCTN_ID,@PRVS_ONHR_DT,@CSC_VLDTY,@EQPMNT_CD_ID)"
    Private Const Equipment_InformationUpdateQuery As String = "UPDATE EQUIPMENT_INFORMATION SET EQPMNT_INFRMTN_ID=@EQPMNT_INFRMTN_ID, EQPMNT_NO=@EQPMNT_NO, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, MNFCTR_DT=@MNFCTR_DT, TR_WGHT_NC=@TR_WGHT_NC, GRSS_WGHT_NC=@GRSS_WGHT_NC, CPCTY_NC=@CPCTY_NC,LST_SRVYR_NM=@LST_SRVYR_NM, LST_TST_DT=@LST_TST_DT, LST_TST_TYP_ID=@LST_TST_TYP_ID, NXT_TST_DT=@NXT_TST_DT, NXT_TST_TYP_ID=@NXT_TST_TYP_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, DPT_ID=@DPT_ID, ACTV_BT=@ACTV_BT,VLDTY_PRD_TST_YRS=@VLDTY_PRD_TST_YRS,RNTL_BT=@RNTL_BT, RMRKS_VC=@RMRKS_VC,PRVS_ONHR_LCTN_ID=@PRVS_ONHR_LCTN_ID,PRVS_ONHR_DT=@PRVS_ONHR_DT,CSC_VLDTY=@CSC_VLDTY,EQPMNT_CD_ID=@EQPMNT_CD_ID  WHERE EQPMNT_INFRMTN_ID=@EQPMNT_INFRMTN_ID"
    Private Const Equipment_InformationDeleteQuery As String = "DELETE FROM EQUIPMENT_INFORMATION WHERE EQPMNT_INFRMTN_ID=@EQPMNT_INFRMTN_ID AND DPT_ID=@DPT_ID"
    Private Const EquipmentInformationSelectQueryByDepotId As String = "SELECT EQPMNT_INFRMTN_ID FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const EquipmentInformationSelectQueryByEquipIDFromActivityStatus As String = "SELECT ACTVTY_STTS_ID FROM ACTIVITY_STATUS WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO"
    Private Const V_Equipment_InformationGateinSelectQuery As String = "SELECT EQPMNT_INFRMTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,MNFCTR_DT,TR_WGHT_NC,GRSS_WGHT_NC,CPCTY_NC,LST_TST_DT,NXT_TST_DT,VLDTY_PRD_TST_YRS,LST_TST_TYP_ID,LST_TST_TYP_CD,NXT_TST_TYP_ID,NXT_TST_TYP_CD,LST_SRVYR_NM,ACTV_BT,RNTL_BT,DPT_ID,DPT_CD,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,RMRKS_VC,RNTL_EDT_BT,COUNT_ATTACH,PRVS_ONHR_LCTN_ID,PRVS_ONHR_LCTN_CD,PRVS_ONHR_DT,CSC_VLDTY,EQPMNT_CD_ID,EQPMNT_CD_CD FROM V_EQUIPMENT_INFORMATION WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO"
    ''CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
    'Private Const Activity_StatusUpdateQuery As String = "UPDATE ACTIVITY_STATUS SET EQPMNT_INFRMTN_RMRKS_VC =@EQPMNT_INFRMTN_RMRKS_VC WHERE EQPMNT_NO = (SELECT TOP 1 EQPMNT_NO FROM ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO ORDER BY ACTVTY_STTS_ID DESC) AND ACTV_BT=1"
    Private Const V_EquipmentInformationDetailSelectQueryById As String = "SELECT EQPMNT_INFRMTN_DTL_ID,EQPMNT_INFRMTN_ID,ATTCHMNT_PTH,ACTL_FL_NM,EQPMNT_NO FROM V_EQUIPMENT_INFORMATION_DETAIL WHERE EQPMNT_INFRMTN_ID=@EQPMNT_INFRMTN_ID"
    Private Const V_EquipmentInformationDetailSelectQueryByEqpNo As String = "SELECT EQPMNT_INFRMTN_DTL_ID,EQPMNT_INFRMTN_ID,ATTCHMNT_PTH,ACTL_FL_NM,EQPMNT_NO FROM V_EQUIPMENT_INFORMATION_DETAIL WHERE EQPMNT_NO=@EQPMNT_NO"
    Private Const V_EquipmentInformationDetailSelectQuery As String = "SELECT EQPMNT_INFRMTN_DTL_ID,EQPMNT_INFRMTN_ID,ATTCHMNT_PTH,ACTL_FL_NM,EQPMNT_NO FROM V_EQUIPMENT_INFORMATION_DETAIL"
    Private Const Equipment_Information_DetailDeleteQuery As String = "DELETE FROM EQUIPMENT_INFORMATION_DETAIL WHERE EQPMNT_INFRMTN_ID=@EQPMNT_INFRMTN_ID"
    Private Const Equipment_Information_DetailInsertQuery As String = "INSERT INTO EQUIPMENT_INFORMATION_DETAIL (EQPMNT_INFRMTN_DTL_ID,EQPMNT_INFRMTN_ID,ATTCHMNT_PTH,ACTL_FL_NM) VALUES(@EQPMNT_INFRMTN_DTL_ID,@EQPMNT_INFRMTN_ID,@ATTCHMNT_PTH,@ACTL_FL_NM)"
    Private Const TrackingEquipmentInfoRemarksSelectQuery As String = "SELECT TRCKNG_ID,CSTMR_ID,EQPMNT_NO,ACTVTY_NAM,EQPMNT_STTS_ID,ACTVTY_NO,ACTVTY_DT,ACTVTY_RMRKS,YRD_LCTN,GI_TRNSCTN_NO,INVCNG_PRTY_ID,RFRNC_NO,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,CNCLD_BY,CNCLD_DT,ADT_RMRKS,DPT_ID,RNTL_CSTMR_ID,RNTL_RFRNC_NO,EQPMNT_INFRMTN_RMRKS_VC FROM TRACKING"

    Private Const TrackingEquipmentInfoRemarksUpdateQuery As String = "Update TRACKING set EQPMNT_INFRMTN_RMRKS_VC=@EQPMNT_INFRMTN_RMRKS_VC where TRCKNG_ID =(select MAX(TRCKNG_ID) from TRACKING where EQPMNT_NO=@EQPMNT_NO)"
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"

    Private ds As EquipmentInformationDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New EquipmentInformationDataSet
    End Sub

#End Region

#Region "GetEquipmentInformationByID() TABLE NAME:EQUIPMENT_INFORMATION"

    Public Function GetEquipmentInformationByID(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentInformationData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(EquipmentInformationData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(EquipmentInformationSelectQueryByDepotId, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentInformationDetailByEqpID() TABLE NAME:EQUIPMENT_INFORMATION"

    Public Function GetEquipmentInformationDetailByEqpID(ByVal bv_lngEquipmentInformationId As Long) As EquipmentInformationDataSet
        Try
            objData = New DataObjects(V_EquipmentInformationDetailSelectQueryById, EquipmentInformationData.EQPMNT_INFRMTN_ID, bv_lngEquipmentInformationId)
            objData.Fill(ds, EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentInformationDetailByEqpID() TABLE NAME:EQUIPMENT_INFORMATION"

    Public Function GetEquipmentInformationDetailByEqpNo(ByVal bv_strEquipmentNo As String) As EquipmentInformationDataSet
        Try
            objData = New DataObjects(V_EquipmentInformationDetailSelectQueryByEqpNo, EquipmentInformationData.EQPMNT_NO, bv_strEquipmentNo)
            objData.Fill(ds, EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentInformationDetail() TABLE NAME:EQUIPMENT_INFORMATION"

    Public Function GetEquipmentInformationDetail() As EquipmentInformationDataSet
        Try
            objData = New DataObjects(V_EquipmentInformationDetailSelectQuery)
            objData.Fill(ds, EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function GetEquipmentInformation(ByVal bv_intDepotID As Integer) As EquipmentInformationDataSet
        Try
            objData = New DataObjects(V_Equipment_InformationSelectQuery, EquipmentInformationData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), EquipmentInformationData._V_EQUIPMENT_INFORMATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function CreateEquipmentInformation(ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeID As Int64, _
        ByVal bv_datManufactureDate As DateTime, _
        ByVal bv_decTareWeight As Decimal, _
        ByVal bv_decGrossWeight As Decimal, _
        ByVal bv_decCapacity As Decimal, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_strLastSurveyorName As String, _
        ByVal bv_datLastTestDate As DateTime, _
        ByVal bv_i64LastTestTypeID As Int64, _
        ByVal bv_datNextTestDate As DateTime, _
        ByVal bv_i64NextTestTypeID As Int64, _
        ByVal bv_decValidityPeriod As String, _
        ByVal bv_blnRentalBit As Boolean, _
        ByVal bv_strRemarks As String, _
        ByVal bv_strPrvOnhrLocation As String, _
        ByVal bv_strstrPrvOnhrDate As DateTime, _
        ByVal bv_strCSC_VLDTY As String,
        ByVal bv_lngEquipmentCode As Int64, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInformationData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EquipmentInformationData._EQUIPMENT_INFORMATION, br_objTrans)
                .Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = intMax
                .Item(EquipmentInformationData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_i64EquipmentTypeID <> 0 Then
                    .Item(EquipmentInformationData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                Else
                    .Item(EquipmentInformationData.EQPMNT_TYP_ID) = DBNull.Value
                End If
                .Item(EquipmentInformationData.EQPMNT_CD_ID) = bv_lngEquipmentCode
                If bv_datManufactureDate <> Nothing And bv_datManufactureDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.MNFCTR_DT) = bv_datManufactureDate
                Else
                    .Item(EquipmentInformationData.MNFCTR_DT) = DBNull.Value
                End If
                If bv_decTareWeight = 0 Then
                    .Item(EquipmentInformationData.TR_WGHT_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.TR_WGHT_NC) = bv_decTareWeight
                End If
                If bv_decGrossWeight = 0 Then
                    .Item(EquipmentInformationData.GRSS_WGHT_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.GRSS_WGHT_NC) = bv_decGrossWeight
                End If
                If bv_decCapacity = 0 Then
                    .Item(EquipmentInformationData.CPCTY_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.CPCTY_NC) = bv_decCapacity
                End If
                If bv_strLastSurveyorName <> Nothing Then
                    .Item(EquipmentInformationData.LST_SRVYR_NM) = bv_strLastSurveyorName
                Else
                    .Item(EquipmentInformationData.LST_SRVYR_NM) = DBNull.Value
                End If
                If bv_datLastTestDate <> Nothing And bv_datLastTestDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.LST_TST_DT) = bv_datLastTestDate
                Else
                    .Item(EquipmentInformationData.LST_TST_DT) = DBNull.Value
                End If
                If bv_i64LastTestTypeID <> 0 Then
                    .Item(EquipmentInformationData.LST_TST_TYP_ID) = bv_i64LastTestTypeID
                Else
                    .Item(EquipmentInformationData.LST_TST_TYP_ID) = DBNull.Value
                End If
                If bv_datNextTestDate <> Nothing And bv_datNextTestDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.NXT_TST_DT) = bv_datNextTestDate
                Else
                    .Item(EquipmentInformationData.NXT_TST_DT) = DBNull.Value
                End If
                If bv_i64NextTestTypeID <> 0 Then
                    .Item(EquipmentInformationData.NXT_TST_TYP_ID) = bv_i64NextTestTypeID
                Else
                    .Item(EquipmentInformationData.NXT_TST_TYP_ID) = DBNull.Value
                End If
                If bv_decValidityPeriod = Nothing Then
                    .Item(EquipmentInformationData.VLDTY_PRD_TST_YRS) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.VLDTY_PRD_TST_YRS) = bv_decValidityPeriod
                End If
                If bv_strPrvOnhrLocation = Nothing Then
                    .Item(EquipmentInformationData.PRVS_ONHR_LCTN_ID) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.PRVS_ONHR_LCTN_ID) = bv_strPrvOnhrLocation
                End If
                If bv_strstrPrvOnhrDate = Nothing Then
                    .Item(EquipmentInformationData.PRVS_ONHR_DT) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.PRVS_ONHR_DT) = bv_strstrPrvOnhrDate
                End If

                If bv_strCSC_VLDTY = Nothing Then
                    .Item(EquipmentInformationData.CSC_VLDTY) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.CSC_VLDTY) = bv_strCSC_VLDTY
                End If
                .Item(EquipmentInformationData.CRTD_BY) = bv_strCreatedBy
                .Item(EquipmentInformationData.CRTD_DT) = bv_datCreatedDate
                .Item(EquipmentInformationData.MDFD_BY) = bv_strModifiedBy
                .Item(EquipmentInformationData.MDFD_DT) = bv_datModifiedDate
                .Item(EquipmentInformationData.DPT_ID) = bv_i32DepotId
                .Item(EquipmentInformationData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentInformationData.RNTL_BT) = bv_blnRentalBit
                If bv_strRemarks <> Nothing Then
                    .Item(EquipmentInformationData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(EquipmentInformationData.RMRKS_VC) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Equipment_InformationInsertQuery, br_objTrans)
            dr = Nothing
            CreateEquipmentInformation = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function UpdateEquipmentInformation(ByVal bv_i64EquipmentInformationID As Int64, _
                                               ByVal bv_strEquipmentNo As String, _
                                               ByVal bv_i64EquipmentTypeID As Int64, _
                                               ByVal bv_datManufactureDate As DateTime, _
                                               ByVal bv_dblTareWeight As Double, _
                                               ByVal bv_dblGrossWeight As Double, _
                                               ByVal bv_dblCapacity As Double, _
                                               ByVal bv_strModifiedBy As String, _
                                               ByVal bv_datModifiedDate As DateTime, _
                                               ByVal bv_i32DepotId As Int32, _
                                               ByVal bv_blnActiveBit As Boolean, _
                                               ByVal bv_strLastSurveyorName As String, _
                                               ByVal bv_datLastTestDate As DateTime, _
                                               ByVal bv_i64LastTestTypeID As Int64, _
                                               ByVal bv_datNextTestDate As DateTime, _
                                               ByVal bv_i64NextTestTypeID As Int64, _
                                               ByVal bv_decValidityPeriod As String, _
                                               ByVal bv_blnRentalBit As Boolean, _
                                               ByVal bv_strRemarks As String, _
                                              ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInformationData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                .Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = bv_i64EquipmentInformationID
                .Item(EquipmentInformationData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_i64EquipmentTypeID <> 0 Then
                    .Item(EquipmentInformationData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                Else
                    .Item(EquipmentInformationData.EQPMNT_TYP_ID) = DBNull.Value
                End If
                If bv_datManufactureDate <> Nothing And bv_datManufactureDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.MNFCTR_DT) = bv_datManufactureDate
                Else
                    .Item(EquipmentInformationData.MNFCTR_DT) = DBNull.Value
                End If
                If bv_dblTareWeight = 0 Then
                    .Item(EquipmentInformationData.TR_WGHT_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.TR_WGHT_NC) = bv_dblTareWeight
                End If
                If bv_dblGrossWeight = 0 Then
                    .Item(EquipmentInformationData.GRSS_WGHT_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.GRSS_WGHT_NC) = bv_dblGrossWeight
                End If
                If bv_dblCapacity = 0 Then
                    .Item(EquipmentInformationData.CPCTY_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.CPCTY_NC) = bv_dblCapacity
                End If
                If bv_strLastSurveyorName <> Nothing Then
                    .Item(EquipmentInformationData.LST_SRVYR_NM) = bv_strLastSurveyorName
                Else
                    .Item(EquipmentInformationData.LST_SRVYR_NM) = DBNull.Value
                End If
                If bv_datLastTestDate <> Nothing And bv_datLastTestDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.LST_TST_DT) = bv_datLastTestDate
                Else
                    .Item(EquipmentInformationData.LST_TST_DT) = DBNull.Value
                End If
                If bv_i64LastTestTypeID <> 0 Then
                    .Item(EquipmentInformationData.LST_TST_TYP_ID) = bv_i64LastTestTypeID
                Else
                    .Item(EquipmentInformationData.LST_TST_TYP_ID) = DBNull.Value
                End If
                If bv_datNextTestDate <> Nothing And bv_datNextTestDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.NXT_TST_DT) = bv_datNextTestDate
                Else
                    .Item(EquipmentInformationData.NXT_TST_DT) = DBNull.Value
                End If
                If bv_i64NextTestTypeID <> 0 Then
                    .Item(EquipmentInformationData.NXT_TST_TYP_ID) = bv_i64NextTestTypeID
                Else
                    .Item(EquipmentInformationData.NXT_TST_TYP_ID) = DBNull.Value
                End If
                If bv_decValidityPeriod = Nothing Then
                    .Item(EquipmentInformationData.VLDTY_PRD_TST_YRS) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.VLDTY_PRD_TST_YRS) = bv_decValidityPeriod
                End If
                .Item(EquipmentInformationData.MDFD_BY) = bv_strModifiedBy
                .Item(EquipmentInformationData.MDFD_DT) = bv_datModifiedDate
                .Item(EquipmentInformationData.DPT_ID) = bv_i32DepotId
                .Item(EquipmentInformationData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentInformationData.RNTL_BT) = bv_blnRentalBit
                If bv_strRemarks <> Nothing Then
                    .Item(EquipmentInformationData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(EquipmentInformationData.RMRKS_VC) = DBNull.Value
                End If
                .Item(EquipmentInformationData.EQPMNT_CD_ID) = bv_i64EquipmentTypeID
            End With
            UpdateEquipmentInformation = objData.UpdateRow(dr, Equipment_InformationUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function DeleteEquipmentInformation(ByVal bv_blnEquipmentInformationID As Int64, _
                                               ByVal bv_DPT_ID As Integer, _
                                               ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(EquipmentInformationData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                .Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = bv_blnEquipmentInformationID
                .Item(EquipmentInformationData.DPT_ID) = bv_DPT_ID
            End With
            DeleteEquipmentInformation = objData.DeleteRow(dr, Equipment_InformationDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentInformationByID() TABLE NAME:EQUIPMENT_INFORMATION"

    Public Function GetEquipmentInformationFromActivityStatus(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentInformationData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(EquipmentInformationData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(EquipmentInformationSelectQueryByEquipIDFromActivityStatus, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentInfoGateIn() TABLE NAME:Equipment_Information"

    Public Function GetEquipmentInfoGateIn(ByVal strEquipmentNo As String, ByVal bv_intDepotID As Integer) As EquipmentInformationDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentInformationData.EQPMNT_NO, strEquipmentNo)
            hshParameters.Add(EquipmentInformationData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(V_Equipment_InformationGateinSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), EquipmentInformationData._V_EQUIPMENT_INFORMATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteEquipmentInformationDetail() Table Name: EQUIPMENT_INFORMATION_DETAIL"

    Public Function DeleteEquipmentInformationDetail(ByVal bv_lngEquipmentInformationId As Long, _
                                                     ByRef br_objTransaction As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(EquipmentInformationData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                .Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = bv_lngEquipmentInformationId
            End With
            DeleteEquipmentInformationDetail = objData.DeleteRow(dr, Equipment_Information_DetailDeleteQuery, br_objTransaction)
            dr = Nothing
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateEquipmentInformationDetail() TABLE NAME:EQUIPMENT_INFORMATION_DETAIL"

    Public Function CreateEquipmentInformationDetail(ByVal bv_lngEquipmentInformationId As Int64, _
                                                     ByVal bv_strAttachmentPath As String, _
                                                     ByVal bv_strActualFileName As String, _
                                                     ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInformationData._EQUIPMENT_INFORMATION_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EquipmentInformationData._EQUIPMENT_INFORMATION_DETAIL, br_objTrans)
                .Item(EquipmentInformationData.EQPMNT_INFRMTN_DTL_ID) = intMax
                .Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = bv_lngEquipmentInformationId
                .Item(EquipmentInformationData.ATTCHMNT_PTH) = bv_strAttachmentPath
                .Item(EquipmentInformationData.ACTL_FL_NM) = bv_strActualFileName
            End With
            objData.InsertRow(dr, Equipment_Information_DetailInsertQuery, br_objTrans)
            dr = Nothing
            CreateEquipmentInformationDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentInfoRemaksTracking() TABLE NAME:Tracking"

    Public Function GetEquipmentInfoRemaksTracking(ByVal bv_strWhere As String) As EquipmentInformationDataSet
        Try
            Dim hshParameters As New Hashtable()
            Dim strQuery As String
            strQuery = String.Concat(TrackingEquipmentInfoRemarksSelectQuery, bv_strWhere)
            objData = New DataObjects(strQuery)
            objData.Fill(CType(ds, DataSet), EquipmentInformationData._TRACKING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateRemarks_Tracking() TABLE NAME:Equipment_Information"
    Public Function UpdateRemarks_Tracking(ByVal bv_i64EquipmentNo As String, ByVal bv_strRemarks As String, ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInformationData._TRACKING).NewRow()
            With dr
                .Item(EquipmentInformationData.EQPMNT_NO) = bv_i64EquipmentNo
                .Item(EquipmentInformationData.EQPMNT_INFRMTN_RMRKS_VC) = bv_strRemarks
            End With
            UpdateRemarks_Tracking = objData.UpdateRow(dr, TrackingEquipmentInfoRemarksUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception

        End Try
    End Function
#End Region

#Region "CreateEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function CreateEquipmentInformation(ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeID As Int64, _
        ByVal bv_datManufactureDate As DateTime, _
        ByVal bv_decTareWeight As Decimal, _
        ByVal bv_decGrossWeight As Decimal, _
        ByVal bv_decCapacity As Decimal, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_strLastSurveyorName As String, _
        ByVal bv_datLastTestDate As DateTime, _
        ByVal bv_i64LastTestTypeID As Int64, _
        ByVal bv_datNextTestDate As DateTime, _
        ByVal bv_i64NextTestTypeID As Int64, _
        ByVal bv_decValidityPeriod As String, _
        ByVal bv_blnRentalBit As Boolean, _
        ByVal bv_strRemarks As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInformationData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EquipmentInformationData._EQUIPMENT_INFORMATION, br_objTrans)
                .Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = intMax
                .Item(EquipmentInformationData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_i64EquipmentTypeID <> 0 Then
                    .Item(EquipmentInformationData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                Else
                    .Item(EquipmentInformationData.EQPMNT_TYP_ID) = DBNull.Value
                End If
                If bv_datManufactureDate <> Nothing And bv_datManufactureDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.MNFCTR_DT) = bv_datManufactureDate
                Else
                    .Item(EquipmentInformationData.MNFCTR_DT) = DBNull.Value
                End If
                If bv_decTareWeight = 0 Then
                    .Item(EquipmentInformationData.TR_WGHT_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.TR_WGHT_NC) = bv_decTareWeight
                End If
                If bv_decGrossWeight = 0 Then
                    .Item(EquipmentInformationData.GRSS_WGHT_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.GRSS_WGHT_NC) = bv_decGrossWeight
                End If
                If bv_decCapacity = 0 Then
                    .Item(EquipmentInformationData.CPCTY_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.CPCTY_NC) = bv_decCapacity
                End If
                If bv_strLastSurveyorName <> Nothing Then
                    .Item(EquipmentInformationData.LST_SRVYR_NM) = bv_strLastSurveyorName
                Else
                    .Item(EquipmentInformationData.LST_SRVYR_NM) = DBNull.Value
                End If
                If bv_datLastTestDate <> Nothing And bv_datLastTestDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.LST_TST_DT) = bv_datLastTestDate
                Else
                    .Item(EquipmentInformationData.LST_TST_DT) = DBNull.Value
                End If
                If bv_i64LastTestTypeID <> 0 Then
                    .Item(EquipmentInformationData.LST_TST_TYP_ID) = bv_i64LastTestTypeID
                Else
                    .Item(EquipmentInformationData.LST_TST_TYP_ID) = DBNull.Value
                End If
                If bv_datNextTestDate <> Nothing And bv_datNextTestDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.NXT_TST_DT) = bv_datNextTestDate
                Else
                    .Item(EquipmentInformationData.NXT_TST_DT) = DBNull.Value
                End If
                If bv_i64NextTestTypeID <> 0 Then
                    .Item(EquipmentInformationData.NXT_TST_TYP_ID) = bv_i64NextTestTypeID
                Else
                    .Item(EquipmentInformationData.NXT_TST_TYP_ID) = DBNull.Value
                End If
                If bv_decValidityPeriod = Nothing Then
                    .Item(EquipmentInformationData.VLDTY_PRD_TST_YRS) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.VLDTY_PRD_TST_YRS) = bv_decValidityPeriod
                End If

                .Item(EquipmentInformationData.PRVS_ONHR_LCTN_ID) = DBNull.Value
               

                .Item(EquipmentInformationData.PRVS_ONHR_DT) = DBNull.Value
              

                .Item(EquipmentInformationData.CSC_VLDTY) = DBNull.Value
              
                .Item(EquipmentInformationData.CRTD_BY) = bv_strCreatedBy
                .Item(EquipmentInformationData.CRTD_DT) = bv_datCreatedDate
                .Item(EquipmentInformationData.MDFD_BY) = bv_strModifiedBy
                .Item(EquipmentInformationData.MDFD_DT) = bv_datModifiedDate
                .Item(EquipmentInformationData.DPT_ID) = bv_i32DepotId
                .Item(EquipmentInformationData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentInformationData.RNTL_BT) = bv_blnRentalBit
                If bv_strRemarks <> Nothing Then
                    .Item(EquipmentInformationData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(EquipmentInformationData.RMRKS_VC) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Equipment_InformationInsertQuery, br_objTrans)
            dr = Nothing
            CreateEquipmentInformation = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function UpdateEquipmentInformation(ByVal bv_i64EquipmentInformationID As Int64, _
                                               ByVal bv_strEquipmentNo As String, _
                                               ByVal bv_i64EquipmentTypeID As Int64, _
                                               ByVal bv_datManufactureDate As DateTime, _
                                               ByVal bv_dblTareWeight As Double, _
                                               ByVal bv_dblGrossWeight As Double, _
                                               ByVal bv_dblCapacity As Double, _
                                               ByVal bv_strModifiedBy As String, _
                                               ByVal bv_datModifiedDate As DateTime, _
                                               ByVal bv_i32DepotId As Int32, _
                                               ByVal bv_blnActiveBit As Boolean, _
                                               ByVal bv_strLastSurveyorName As String, _
                                               ByVal bv_datLastTestDate As DateTime, _
                                               ByVal bv_i64LastTestTypeID As Int64, _
                                               ByVal bv_datNextTestDate As DateTime, _
                                               ByVal bv_i64NextTestTypeID As Int64, _
                                               ByVal bv_decValidityPeriod As String, _
                                               ByVal bv_blnRentalBit As Boolean, _
                                               ByVal bv_strRemarks As String, _
                                                ByVal bv_strPrvOnhrLocation As String, _
                                                ByVal bv_strstrPrvOnhrDate As DateTime, _
                                                ByVal bv_strCSCValidity As String, _
                                                ByVal bv_intCode As Int64, _
                                               ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInformationData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                .Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = bv_i64EquipmentInformationID
                .Item(EquipmentInformationData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_i64EquipmentTypeID <> 0 Then
                    .Item(EquipmentInformationData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                Else
                    .Item(EquipmentInformationData.EQPMNT_TYP_ID) = DBNull.Value
                End If
                .Item(EquipmentInformationData.EQPMNT_CD_ID) = bv_intCode
                If bv_datManufactureDate <> Nothing And bv_datManufactureDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.MNFCTR_DT) = bv_datManufactureDate
                Else
                    .Item(EquipmentInformationData.MNFCTR_DT) = DBNull.Value
                End If
                If bv_dblTareWeight = 0 Then
                    .Item(EquipmentInformationData.TR_WGHT_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.TR_WGHT_NC) = bv_dblTareWeight
                End If
                If bv_dblGrossWeight = 0 Then
                    .Item(EquipmentInformationData.GRSS_WGHT_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.GRSS_WGHT_NC) = bv_dblGrossWeight
                End If
                If bv_dblCapacity = 0 Then
                    .Item(EquipmentInformationData.CPCTY_NC) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.CPCTY_NC) = bv_dblCapacity
                End If
                If bv_strLastSurveyorName <> Nothing Then
                    .Item(EquipmentInformationData.LST_SRVYR_NM) = bv_strLastSurveyorName
                Else
                    .Item(EquipmentInformationData.LST_SRVYR_NM) = DBNull.Value
                End If
                If bv_datLastTestDate <> Nothing And bv_datLastTestDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.LST_TST_DT) = bv_datLastTestDate
                Else
                    .Item(EquipmentInformationData.LST_TST_DT) = DBNull.Value
                End If
                If bv_i64LastTestTypeID <> 0 Then
                    .Item(EquipmentInformationData.LST_TST_TYP_ID) = bv_i64LastTestTypeID
                Else
                    .Item(EquipmentInformationData.LST_TST_TYP_ID) = DBNull.Value
                End If
                If bv_datNextTestDate <> Nothing And bv_datNextTestDate <> sqlDbnull Then
                    .Item(EquipmentInformationData.NXT_TST_DT) = bv_datNextTestDate
                Else
                    .Item(EquipmentInformationData.NXT_TST_DT) = DBNull.Value
                End If
                If bv_i64NextTestTypeID <> 0 Then
                    .Item(EquipmentInformationData.NXT_TST_TYP_ID) = bv_i64NextTestTypeID
                Else
                    .Item(EquipmentInformationData.NXT_TST_TYP_ID) = DBNull.Value
                End If
                If bv_decValidityPeriod = Nothing Then
                    .Item(EquipmentInformationData.VLDTY_PRD_TST_YRS) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.VLDTY_PRD_TST_YRS) = bv_decValidityPeriod
                End If
                If bv_strPrvOnhrLocation = Nothing Then
                    .Item(EquipmentInformationData.PRVS_ONHR_LCTN_ID) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.PRVS_ONHR_LCTN_ID) = bv_strPrvOnhrLocation
                End If
                If bv_strstrPrvOnhrDate = Nothing Then
                    .Item(EquipmentInformationData.PRVS_ONHR_DT) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.PRVS_ONHR_DT) = bv_strstrPrvOnhrDate
                End If
                If bv_strCSCValidity = Nothing Then
                    .Item(EquipmentInformationData.CSC_VLDTY) = DBNull.Value
                Else
                    .Item(EquipmentInformationData.CSC_VLDTY) = bv_strCSCValidity
                End If

                .Item(EquipmentInformationData.MDFD_BY) = bv_strModifiedBy
                .Item(EquipmentInformationData.MDFD_DT) = bv_datModifiedDate
                .Item(EquipmentInformationData.DPT_ID) = bv_i32DepotId
                .Item(EquipmentInformationData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentInformationData.RNTL_BT) = bv_blnRentalBit
                If bv_strRemarks <> Nothing Then
                    .Item(EquipmentInformationData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(EquipmentInformationData.RMRKS_VC) = DBNull.Value
                End If
            End With
            UpdateEquipmentInformation = objData.UpdateRow(dr, Equipment_InformationUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class

#End Region
