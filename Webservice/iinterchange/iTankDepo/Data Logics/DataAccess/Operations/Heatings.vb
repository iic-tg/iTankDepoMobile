Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class Heatings
#Region "Declaration Part.. "

    Dim objData As DataObjects

    Private Const GateinSelectQueryfromGateIn = "SELECT CSTMR_ID,CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,YRD_LCTN,GTN_DT,PRDCT_ID,PRDCT_CD,GI_TRNSCTN_NO,PRDCT_DSCRPTN_VC FROM V_GATEIN WHERE DPT_ID=@DPT_ID AND HTNG_BT=1 AND GI_TRNSCTN_NO NOT IN (SELECT GI_TRNSCTN_NO FROM HEATING_CHARGE WHERE BLLNG_FLG = 'B' AND DPT_ID=@DPT_ID) ORDER BY GTN_DT DESC"
    Private Const CUSTOMERSelectQueryByDepotID As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=C.CSTMR_CRRNCY_ID) AS CSTMR_CRRNCY_CD, BLLNG_TYP_ID,CNTCT_PRSN_NAM,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,RPR_TCH_EML_ID,BLK_EML_FRMT_ID,HYDR_AMNT_NC,PNMTC_AMNT_NC,LBR_RT_PR_HR_NC,LK_TST_RT_NC,SRVY_ONHR_OFFHR_RT_NC,PRDC_TST_TYP_ID,VLDTY_PRD_TST_YRS,MIN_HTNG_RT_NC,MIN_HTNG_PRD_NC,HRLY_CHRG_NC,CHK_DGT_VLDTN_BT,TRNSPRTTN_BT,RNTL_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=C.DPT_ID AND BNK_TYP_ID=44))AS CRRNCY_CD  FROM CUSTOMER C WHERE DPT_ID=@DPT_ID"
    Private Const V_HEATING_CHARGESelectQueryByDepot As String = "SELECT HTNG_ID,HTNG_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CSTMR_ID,CSTMR_CD,HTNG_STRT_DT,HTNG_STRT_TM,HTNG_END_DT,HTNG_END_TM,HTNG_TMPRTR,TTL_HTN_PRD,MIN_HTNG_RT_NC,HRLY_CHRG_NC,TTL_RT_NC,BLLNG_FLG,GI_TRNSCTN_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT FROM V_HEATING_CHARGE WHERE BLLNG_FLG <> 'B' AND DPT_ID=@DPT_ID"
    Private Const Heating_ChargeInsertQuery As String = "INSERT INTO HEATING_CHARGE(HTNG_ID,HTNG_CD,EQPMNT_NO,EQPMNT_TYP_ID,CSTMR_ID,HTNG_STRT_DT,HTNG_STRT_TM,HTNG_END_DT,HTNG_END_TM,HTNG_TMPRTR,TTL_HTN_PRD,MIN_HTNG_RT_NC,HRLY_CHRG_NC,TTL_RT_NC,BLLNG_FLG,GI_TRNSCTN_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT)VALUES(@HTNG_ID,@HTNG_CD,@EQPMNT_NO,@EQPMNT_TYP_ID,@CSTMR_ID,@HTNG_STRT_DT,@HTNG_STRT_TM,@HTNG_END_DT,@HTNG_END_TM,@HTNG_TMPRTR,@TTL_HTN_PRD,@MIN_HTNG_RT_NC,@HRLY_CHRG_NC,@TTL_RT_NC,@BLLNG_FLG,@GI_TRNSCTN_NO,@DPT_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT)"
    Private Const Heating_ChargeUpdateQuery As String = "UPDATE HEATING_CHARGE SET HTNG_STRT_DT=@HTNG_STRT_DT, HTNG_STRT_TM=@HTNG_STRT_TM, HTNG_END_DT=@HTNG_END_DT, HTNG_END_TM=@HTNG_END_TM, HTNG_TMPRTR=@HTNG_TMPRTR, TTL_HTN_PRD=@TTL_HTN_PRD, MIN_HTNG_RT_NC=@MIN_HTNG_RT_NC, HRLY_CHRG_NC=@HRLY_CHRG_NC, TTL_RT_NC=@TTL_RT_NC, BLLNG_FLG=@BLLNG_FLG, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND  DPT_ID=@DPT_ID"
    Private Const Handling_ChargeUpdateQuery As String = "UPDATE HANDLING_CHARGE SET HTNG_BT=@HTNG_BT WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND  DPT_ID=@DPT_ID"
    Private Const HeatingCountQuery As String = "SELECT COUNT(HTNG_ID) FROM HEATING_CHARGE WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"

    Private ds As HeatingDataSet
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
#End Region

#Region "Constructor.."

    Sub New()
        ds = New HeatingDataSet
    End Sub

#End Region

#Region "GetGateIn() "
    Public Function GetGateInDetails(ByVal bv_intDepotId As Integer) As HeatingDataSet
        Try
            objData = New DataObjects(GateinSelectQueryfromGateIn, HeatingData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), HeatingData._V_HEATING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetVHeatingChargeBy() TABLE NAME:V_HEATING_CHARGE"

    Public Function GetVHeatingChargeBy(ByVal bv_intDepotId As Integer) As HeatingDataSet
        Try
            objData = New DataObjects(V_HEATING_CHARGESelectQueryByDepot, HeatingData.DPT_ID, CStr(bv_intDepotId))
            objData.Fill(CType(ds, DataSet), HeatingData._HEATING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetCustomerDetail() "
    Public Function pub_GetCustomerDetail(ByVal bv_intDepotId As Integer) As HeatingDataSet
        Try
            objData = New DataObjects(CUSTOMERSelectQueryByDepotID, HeatingData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), HeatingData._CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateHeatingCharge() TABLE NAME:Heating_Charge"

    Public Function CreateHeatingCharge(ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_i64EquipmentTypeID As Int64, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_datHeatingStartDate As DateTime, _
        ByVal bv_strHeatingStartTime As String, _
        ByVal bv_datHeatingEndDate As DateTime, _
        ByVal bv_strHeatingEndTime As String, _
        ByVal bv_strHeatingTemperature As String, _
        ByVal bv_dblTotalHeatingPreriod As Double, _
        ByVal bv_dblMinHeatingRateNc As Double, _
        ByVal bv_dblHourlyChargeNc As Double, _
        ByVal bv_dblTotalRateNc As Double, _
        ByVal bv_strBillingFlag As String, _
        ByVal bv_strGI_TRNSCTN_NO As String, _
        ByVal bv_i32Depot_Id As Int32, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
         ByRef br_objtrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(HeatingData._HEATING_CHARGE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(HeatingData._HEATING_CHARGE, br_objtrans)
                .Item(HeatingData.HTNG_ID) = intMax
                .Item(HeatingData.HTNG_CD) = intMax
                .Item(HeatingData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(HeatingData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                .Item(HeatingData.CSTMR_ID) = bv_i64CSTMR_ID
                If bv_datHeatingStartDate <> Nothing Then
                    .Item(HeatingData.HTNG_STRT_DT) = bv_datHeatingStartDate
                Else
                    .Item(HeatingData.HTNG_STRT_DT) = DBNull.Value
                End If
                If bv_strHeatingStartTime <> Nothing Then
                    .Item(HeatingData.HTNG_STRT_TM) = bv_strHeatingStartTime
                Else
                    .Item(HeatingData.HTNG_STRT_TM) = DBNull.Value
                End If
                If bv_datHeatingEndDate <> Nothing Then
                    .Item(HeatingData.HTNG_END_DT) = bv_datHeatingEndDate
                Else
                    .Item(HeatingData.HTNG_END_DT) = DBNull.Value
                End If
                If bv_strHeatingEndTime <> Nothing Then
                    .Item(HeatingData.HTNG_END_TM) = bv_strHeatingEndTime
                Else
                    .Item(HeatingData.HTNG_END_TM) = DBNull.Value
                End If
                If bv_strHeatingTemperature <> Nothing Then
                    .Item(HeatingData.HTNG_TMPRTR) = bv_strHeatingTemperature
                Else
                    .Item(HeatingData.HTNG_TMPRTR) = DBNull.Value
                End If
                .Item(HeatingData.TTL_HTN_PRD) = bv_dblTotalHeatingPreriod
                .Item(HeatingData.MIN_HTNG_RT_NC) = bv_dblMinHeatingRateNc
                .Item(HeatingData.HRLY_CHRG_NC) = bv_dblHourlyChargeNc
                .Item(HeatingData.TTL_RT_NC) = bv_dblTotalRateNc
                .Item(HeatingData.BLLNG_FLG) = bv_strBillingFlag
                If bv_strGI_TRNSCTN_NO <> Nothing Then
                    .Item(HeatingData.GI_TRNSCTN_NO) = bv_strGI_TRNSCTN_NO
                Else
                    .Item(HeatingData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(HeatingData.DPT_ID) = bv_i32Depot_Id
                .Item(HeatingData.CRTD_BY) = bv_strCreatedBy
                .Item(HeatingData.CRTD_DT) = bv_datCreatedDate
                .Item(HeatingData.MDFD_BY) = bv_strModifiedBy
                .Item(HeatingData.MDFD_DT) = bv_datModifiedDate
            End With
            objData.InsertRow(dr, Heating_ChargeInsertQuery, br_objtrans)
            dr = Nothing
            CreateHeatingCharge = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateHeatingCharge() TABLE NAME:Heating_Charge"

    Public Function UpdateHeatingCharge(ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_datHeatingStartDate As DateTime, _
        ByVal bv_strHeatingStartTime As String, _
        ByVal bv_datHeatingEndDate As DateTime, _
        ByVal bv_strHeatingEndTime As String, _
        ByVal bv_strHeatingTemperature As String, _
        ByVal bv_dblTotalHeatingPreriod As Double, _
        ByVal bv_dblMinHeatingRateNc As Double, _
        ByVal bv_dblHourlyChargeNc As Double, _
        ByVal bv_dblTotalRateNc As Double, _
        ByVal bv_strBillingFlag As String, _
        ByVal bv_strGI_TRNSCTN_NO As String, _
        ByVal bv_i32Depot_Id As Int32, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
         ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(HeatingData._HEATING_CHARGE).NewRow()
            With dr
                .Item(HeatingData.EQPMNT_NO) = bv_strEQPMNT_NO
                If bv_datHeatingStartDate <> Nothing Then
                    .Item(HeatingData.HTNG_STRT_DT) = bv_datHeatingStartDate
                Else
                    .Item(HeatingData.HTNG_STRT_DT) = DBNull.Value
                End If
                If bv_strHeatingStartTime <> Nothing Then
                    .Item(HeatingData.HTNG_STRT_TM) = bv_strHeatingStartTime
                Else
                    .Item(HeatingData.HTNG_STRT_TM) = DBNull.Value
                End If
                If bv_datHeatingEndDate <> Nothing Then
                    .Item(HeatingData.HTNG_END_DT) = bv_datHeatingEndDate
                Else
                    .Item(HeatingData.HTNG_END_DT) = DBNull.Value
                End If
                If bv_strHeatingEndTime <> Nothing Then
                    .Item(HeatingData.HTNG_END_TM) = bv_strHeatingEndTime
                Else
                    .Item(HeatingData.HTNG_END_TM) = DBNull.Value
                End If
                If bv_strHeatingTemperature <> Nothing Then
                    .Item(HeatingData.HTNG_TMPRTR) = bv_strHeatingTemperature
                Else
                    .Item(HeatingData.HTNG_TMPRTR) = DBNull.Value
                End If
                .Item(HeatingData.TTL_HTN_PRD) = bv_dblTotalHeatingPreriod
                .Item(HeatingData.MIN_HTNG_RT_NC) = bv_dblMinHeatingRateNc
                .Item(HeatingData.HRLY_CHRG_NC) = bv_dblHourlyChargeNc
                .Item(HeatingData.TTL_RT_NC) = bv_dblTotalRateNc
                .Item(HeatingData.BLLNG_FLG) = bv_strBillingFlag
                If bv_strGI_TRNSCTN_NO <> Nothing Then
                    .Item(HeatingData.GI_TRNSCTN_NO) = bv_strGI_TRNSCTN_NO
                Else
                    .Item(HeatingData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(HeatingData.DPT_ID) = bv_i32Depot_Id
                .Item(HeatingData.MDFD_BY) = bv_strModifiedBy
                .Item(HeatingData.MDFD_DT) = bv_datModifiedDate
            End With
            UpdateHeatingCharge = objData.UpdateRow(dr, Heating_ChargeUpdateQuery, br_objtrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateHandlingCharge() TABLE NAME:Handling_Charge"

    Public Function UpdateHandlingCharge(ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_strHeatingFlag As String, _
        ByVal bv_strGI_TRNSCTN_NO As String, _
        ByVal bv_i32Depot_Id As Int32, _
         ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(HeatingData._HANDLING_CHARGE).NewRow()
            With dr
                .Item(HeatingData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(HeatingData.HTNG_BT) = bv_strHeatingFlag
                If bv_strGI_TRNSCTN_NO <> Nothing Then
                    .Item(HeatingData.GI_TRNSCTN_NO) = bv_strGI_TRNSCTN_NO
                Else
                    .Item(HeatingData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(HeatingData.DPT_ID) = bv_i32Depot_Id
            End With
            UpdateHandlingCharge = objData.UpdateRow(dr, Handling_ChargeUpdateQuery, br_objtrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getHeatingCount"
    Public Function getHeatingCount(ByVal bv_strEqpmntNO As String, _
                                    ByVal bv_GITransNo As String, _
                                    ByVal bv_dptID As Int64, _
                                    ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(HeatingData.DPT_ID, bv_dptID)
            hshParameters.Add(HeatingData.GI_TRNSCTN_NO, bv_GITransNo)
            hshParameters.Add(HeatingData.EQPMNT_NO, bv_strEqpmntNO)
            objData = New DataObjects(HeatingCountQuery, hshParameters)
            Return objData.ExecuteScalar(br_ObjTransactions)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class