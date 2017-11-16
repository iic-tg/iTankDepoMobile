Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class RentalEntrys
#Region "Declaration Part.."
    Dim objData As DataObjects
    'Private Const V_RENTAL_ENTRYSelectQueryBy As String = "SELECT RNTL_ENTRY_ID,EQPMNT_NO,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CNTRCT_RFRNC_NO,PO_RFRNC_NO,RNTL_PR_DY,ON_HR_DT,HNDLNG_OT_NC,ON_HR_SRVY_NC,OFF_HR_DT,HNDLNG_IN_NC,OFF_HR_SRVY_NC,OTHR_CHRG_NC,RMRKS_VC,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,(SELECT EQPMNT_INFRMTN_ID FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=V.EQPMNT_NO)EQPMNT_INFRMTN_ID FROM V_RENTAL_ENTRY V WHERE DPT_ID=@DPT_ID"
    Private Const V_RENTAL_ENTRYSelectQueryBy As String = "SELECT RNTL_ENTRY_ID,EQPMNT_NO,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,RMRKS_VC,RNTL_RFRNC_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,EQPMNT_INFRMTN_ID,IS_GTOT_BT,IS_GTN_BT,BLLNG_FLG,GTOT_DT,CNTRCT_STRT_DT,GTIN_DT,OTHR_CHRG_NC,ALLOW_EDIT FROM V_RENTAL_ENTRY RE WHERE DPT_ID=@DPT_ID AND RNTL_RFRNC_NO NOT IN (SELECT RNTL_RFRNC_NO FROM RENTAL_CHARGE WHERE RNTL_RFRNC_NO=RE.RNTL_RFRNC_NO AND EQPMNT_NO=RE.EQPMNT_NO AND BLLNG_FLG='B' AND RNTL_CNTN_FLG='S') ORDER BY RNTL_ENTRY_ID"
    'Private Const V_RENTAL_ENTRYSelectQueryBy As String = "SELECT RNTL_ENTRY_ID,EQPMNT_NO,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,RMRKS_VC,RNTL_RFRNC_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,EQPMNT_INFRMTN_ID,IS_GTOT_BT,IS_GTN_BT,BLLNG_FLG,GTOT_DT,CNTRCT_STRT_DT,GTIN_DT,ALLOW_EDIT FROM V_RENTAL_ENTRY RE WHERE DPT_ID=@DPT_ID AND RNTL_RFRNC_NO NOT IN (SELECT RNTL_RFRNC_NO FROM RENTAL_CHARGE WHERE RNTL_RFRNC_NO=RE.RNTL_RFRNC_NO AND EQPMNT_NO=RE.EQPMNT_NO AND BLLNG_FLG='B' AND RNTL_CNTN_FLG='S') ORDER BY RNTL_ENTRY_ID"
    Private Const EquipmentNoSelectQuery As String = "SELECT EQPMNT_INFRMTN_ID,EQPMNT_NO,RNTL_ENTRY_ID FROM V_RENTAL_EQUIPMENT WHERE DPT_ID=@DPT_ID"
    Private Const GateOutSelectQuery As String = "SELECT EQPMNT_NO,GTOT_ID,GI_TRNSCTN_NO,DPT_ID FROM GATEOUT WHERE DPT_ID=@DPT_ID"
    Private Const V_RENTAL_OTHER_CHARGESelectQuery As String = "SELECT RNTL_OTHR_CHRG_ID,RNTL_ENTRY_ID,ADDTNL_CHRG_RT_ID,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_DSCRPTN_VC,RT_NC,(SELECT DFLT_BT FROM ADDITIONAL_CHARGE_RATE WHERE ADDTNL_CHRG_RT_ID =V.ADDTNL_CHRG_RT_ID)DFLT_BT FROM V_RENTAL_OTHER_CHARGE V WHERE RNTL_ENTRY_ID=@RNTL_ENTRY_ID"
    Private Const DefaultRateCountQuery As String = "SELECT COUNT(RNTL_OTHR_CHRG_ID) FROM RENTAL_OTHER_CHARGE WHERE RNTL_ENTRY_ID=@RNTL_ENTRY_ID AND ADDTNL_CHRG_RT_ID=@ADDTNL_CHRG_RT_ID"
    Private Const Rental_EntryInsertQuery As String = "INSERT INTO RENTAL_ENTRY(RNTL_ENTRY_ID,EQPMNT_NO,CSTMR_ID,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,RMRKS_VC,RNTL_RFRNC_NO,GI_TRNSCTN_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,OTHR_CHRG_NC,GTN_BT)VALUES(@RNTL_ENTRY_ID,@EQPMNT_NO,@CSTMR_ID,@CNTRCT_RFRNC_NO,@PO_RFRNC_NO,@ON_HR_DT,@OFF_HR_DT,@RMRKS_VC,@RNTL_RFRNC_NO,@GI_TRNSCTN_NO,@DPT_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@OTHR_CHRG_NC,@GTN_BT)"
    Private Const Rental_EntryUpdateQuery As String = "UPDATE RENTAL_ENTRY SET ON_HR_DT=@ON_HR_DT,PO_RFRNC_NO=@PO_RFRNC_NO, RMRKS_VC=@RMRKS_VC,MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT,OTHR_CHRG_NC=@OTHR_CHRG_NC,OFF_HR_DT=@OFF_HR_DT WHERE RNTL_ENTRY_ID=@RNTL_ENTRY_ID AND RNTL_RFRNC_NO=@RNTL_RFRNC_NO"
    Private Const Rental_Other_ChargeInsertQuery As String = "INSERT INTO RENTAL_OTHER_CHARGE(RNTL_OTHR_CHRG_ID,RNTL_ENTRY_ID,ADDTNL_CHRG_RT_ID,RT_NC)VALUES(@RNTL_OTHR_CHRG_ID,@RNTL_ENTRY_ID,@ADDTNL_CHRG_RT_ID,@RT_NC)"
    Private Const Rental_Other_ChargeUpdateQuery As String = "UPDATE RENTAL_OTHER_CHARGE SET RNTL_ENTRY_ID=@RNTL_ENTRY_ID, ADDTNL_CHRG_RT_ID=@ADDTNL_CHRG_RT_ID, RT_NC=@RT_NC WHERE RNTL_OTHR_CHRG_ID=@RNTL_OTHR_CHRG_ID"
    Private Const RentalOtherChargeCountQuery As String = "SELECT COUNT(RNTL_OTHR_CHRG_ID) FROM RENTAL_OTHER_CHARGE WHERE RNTL_ENTRY_ID=@RNTL_ENTRY_ID"
    Private Const RentalEntryIDQuery As String = "SELECT RNTL_ENTRY_ID FROM V_RENTAL_EQUIPMENT WHERE EQPMNT_NO=@EQPMNT_NO"
    Private Const RentalOtherChargeDeleteQuery As String = "DELETE FROM RENTAL_OTHER_CHARGE WHERE RNTL_OTHR_CHRG_ID=@RNTL_OTHR_CHRG_ID"
    Private Const SupplierEquipmentNoSelectQuery As String = "SELECT EQPMNT_NO,(SELECT CNTRCT_STRT_DT FROM V_SUPPLIER_CONTRACT_DETAIL WHERE SPPLR_CNTRCT_DTL_ID=V.SPPLR_CNTRCT_DTL_ID)CNTRCT_STRT_DT,(SELECT CNTRCT_END_DT FROM V_SUPPLIER_CONTRACT_DETAIL WHERE SPPLR_CNTRCT_DTL_ID=V.SPPLR_CNTRCT_DTL_ID)CNTRCT_END_DT FROM V_SUPPLIER_EQUIPMENT_DETAIL V "
    Private Const V_AdditionalChargeSelectQuery As String = "SELECT ADDTNL_CHRG_RT_ID,OPRTN_TYP_ID,OPRTN_TYP_CD,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_DSCRPTN_VC,RT_NC,DFLT_BT,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM V_ADDITIONAL_CHARGE_RATE WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND DFLT_BT=1 AND OPRTN_TYP_ID=88"
    Private Const EquipmentNoActivityStatusSelectQuery As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,INSTRCTNS_VC,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,CERT_GNRTD_FLG,RPR_CMPLTN_DT FROM V_ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND ACTV_BT=1"
    Private Const TrackingUpdateQuery As String = "UPDATE TRACKING SET RNTL_CSTMR_ID=@RNTL_CSTMR_ID,ACTVTY_DT=@ACTVTY_DT,ACTVTY_RMRKS=@ACTVTY_RMRKS,MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE DPT_ID=@DPT_ID AND RFRNC_NO=@RFRNC_NO AND EQPMNT_NO=@EQPMNT_NO AND ACTVTY_NAM='Rental Entry'"
    Private Const TrackingInsertQuery As String = "INSERT INTO TRACKING(TRCKNG_ID,CSTMR_ID,EQPMNT_NO,ACTVTY_NAM,EQPMNT_STTS_ID,ACTVTY_NO,ACTVTY_DT,ACTVTY_RMRKS,YRD_LCTN,GI_TRNSCTN_NO,INVCNG_PRTY_ID,RFRNC_NO,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,CNCLD_BY,CNCLD_DT,ADT_RMRKS,DPT_ID,RNTL_CSTMR_ID,RNTL_RFRNC_NO)VALUES(@TRCKNG_ID,@CSTMR_ID,@EQPMNT_NO,@ACTVTY_NAM,@EQPMNT_STTS_ID,@ACTVTY_NO,@ACTVTY_DT,@ACTVTY_RMRKS,@YRD_LCTN,@GI_TRNSCTN_NO,@INVCNG_PRTY_ID,@RFRNC_NO,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@CNCLD_BY,@CNCLD_DT,@ADT_RMRKS,@DPT_ID,@RNTL_CSTMR_ID,@RNTL_RFRNC_NO)"
    Private Const DefaultRateSelectQueryBy As String = "SELECT ADDTNL_CHRG_RT_ID,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_DSCRPTN_VC,RT_NC,DFLT_BT,DPT_ID FROM ADDITIONAL_CHARGE_RATE WHERE DFLT_BT=1 AND OPRTN_TYP_ID=88 AND DPT_ID=@DPT_ID"
    Private Const Rental_ChargeUpdateQuery As String = "UPDATE RENTAL_CHARGE SET OTHR_CHRG_NC = @OTHR_CHRG_NC,TO_BLLNG_DT=@TO_BLLNG_DT,FRM_BLLNG_DT=@FRM_BLLNG_DT WHERE EQPMNT_NO =@EQPMNT_NO AND RNTL_RFRNC_NO=@RNTL_RFRNC_NO"
    Private Const RentalEquipmentDeleteQuery As String = "DELETE FROM RENTAL_OTHER_CHARGE WHERE RNTL_ENTRY_ID=@RNTL_ENTRY_ID"
    Private Const RentalEntryDeleteQuery As String = "DELETE FROM RENTAL_ENTRY WHERE RNTL_ENTRY_ID=@RNTL_ENTRY_ID"
    Private Const V_AdditionalChargeSelectQueryById As String = "SELECT RT_NC FROM V_ADDITIONAL_CHARGE_RATE WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND ADDTNL_CHRG_RT_ID=@ADDTNL_CHRG_RT_ID AND OPRTN_TYP_ID=88"
    Private Const GetOffHireSelectQueryBy As String = "SELECT TOP(1) EQPMNT_NO,RNTL_RFRNC_NO,RNTL_ENTRY_ID,(CASE WHEN (SELECT COUNT(EQPMNT_NO) FROM RENTAL_ENTRY WHERE EQPMNT_NO=RE.EQPMNT_NO AND RNTL_ENTRY_ID>RE.RNTL_ENTRY_ID)>0 THEN OFF_HR_DT ELSE NULL END )OFF_HR_DT FROM RENTAL_ENTRY RE WHERE EQPMNT_NO=@EQPMNT_NO AND RNTL_ENTRY_ID <>@RNTL_ENTRY_ID ORDER BY RNTL_ENTRY_ID DESC"
    Private Const RentalEntrySelectQueryByEquipmentNo As String = "SELECT RNTL_ENTRY_ID,EQPMNT_NO,CSTMR_ID,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,RMRKS_VC,RNTL_RFRNC_NO,GI_TRNSCTN_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,GTN_BT,OTHR_CHRG_NC FROM V_RENTAL_ENTRY WHERE EQPMNT_NO = @EQPMNT_NO AND ON_HR_DT IS NULL AND OFF_HR_DT IS NULL AND DPT_ID = @DPT_ID"
    Private Const RentalOtherChargesSelectQuery As String = "SELECT RNTL_OTHR_CHRG_ID,RNTL_ENTRY_ID,ADDTNL_CHRG_RT_ID,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_DSCRPTN_VC,RT_NC FROM V_RENTAL_OTHER_CHARGE"
    Private ds As RentalEntryDataSet
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
#End Region

#Region "Constructor.."

    Sub New()
        ds = New RentalEntryDataSet
    End Sub

#End Region

#Region "DeleteRentalOtherCharge()"
    Public Function DeleteRentalOtherCharge(ByVal bv_strRentalEntryID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(RentalEntryData._RENTAL_OTHER_CHARGE).NewRow()
            With dr
                .Item(RentalEntryData.RNTL_OTHR_CHRG_ID) = bv_strRentalEntryID
            End With
            DeleteRentalOtherCharge = objData.DeleteRow(dr, RentalOtherChargeDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getEqpmntNoFromEquipmentInfo() "
    Public Function getEqpmntNoFromEquipmentInfo(ByVal bv_intDepotId As Integer) As RentalEntryDataSet
        Try
            objData = New DataObjects(EquipmentNoSelectQuery, RentalEntryData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), RentalEntryData._EQUIPMENT_INFORMATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEqpmntDetails() "
    Public Function GetEqpmntDetails(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Integer, ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dtActivityStatus As New DataTable
            hshTable.Add(RentalEntryData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(RentalEntryData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(EquipmentNoActivityStatusSelectQuery, hshTable)
            objData.Fill(dtActivityStatus, br_ObjTransactions)
            Return dtActivityStatus
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getRentalEntryDetails() "
    Public Function getRentalEntryDetails(ByVal bv_intDepotId As Integer) As RentalEntryDataSet
        Try
            objData = New DataObjects(V_RENTAL_ENTRYSelectQueryBy, RentalEntryData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), RentalEntryData._V_RENTAL_ENTRY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getGateOutEquipments() "
    Public Function getGateOutEquipments(ByVal bv_intDepotId As Integer) As RentalEntryDataSet
        Try
            objData = New DataObjects(GateOutSelectQuery, RentalEntryData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), RentalEntryData._GATEOUT_INFO)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetOtherChargeBy_RentalID() "
    Public Function pub_GetOtherChargeBy_RentalID(ByVal bv_RentalID As Integer) As RentalEntryDataSet
        Try
            objData = New DataObjects(V_RENTAL_OTHER_CHARGESelectQuery, RentalEntryData.RNTL_ENTRY_ID, bv_RentalID)
            objData.Fill(CType(ds, DataSet), RentalEntryData._V_RENTAL_OTHER_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getRentalEquipmentCount"
    Public Function getDefaultRateCount(ByVal RentalID As Int64, _
                                    ByVal ChrgRateID As Int64, _
                                    ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(RentalEntryData.RNTL_ENTRY_ID, RentalID)
            hshParameters.Add(RentalEntryData.ADDTNL_CHRG_RT_ID, ChrgRateID)
            objData = New DataObjects(DefaultRateCountQuery, hshParameters)
            Return objData.ExecuteScalar(br_ObjTransactions)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "getRentalOtherChargeCount"
    Public Function getRentalOtherChargeCount(ByVal bv_RentalID As Integer, _
                                    ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim hshParameters As New Hashtable()
            objData = New DataObjects(RentalOtherChargeCountQuery, RentalEntryData.RNTL_ENTRY_ID, bv_RentalID)
            Return objData.ExecuteScalar(br_ObjTransactions)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateRentalEntry() TABLE NAME:Rental_Entry"
    Public Function CreateRentalEntry(ByVal bv_strEQPMNT_NO As String, _
                                      ByVal bv_i64CSTMR_ID As Int64, _
                                      ByVal bv_strCNTRCT_RFRNC_NO As String, _
                                      ByVal bv_strPO_RFRNC_NO As String, _
                                      ByVal bv_datON_HR_DT As DateTime, _
                                      ByVal bv_datOFF_HR_DT As String, _
                                      ByVal bv_dblOTHR_CHRG_NC As Double, _
                                      ByVal bv_strRMRKS_VC As String, _
                                      ByRef br_RentalRefNo As String, _
                                      ByVal bv_GI_Trans_no As String, _
                                      ByVal bv_i32DPT_ID As Int32, _
                                      ByVal bv_strCRTD_BY As String, _
                                      ByVal bv_datCRTD_DT As DateTime, _
                                      ByVal bv_strMDFD_BY As String, _
                                      ByVal bv_datMDFD_DT As DateTime, _
                                      ByVal bv_OtherCharge As Double, _
                                      ByVal bv_gtnbt As Boolean, _
                                      ByRef br_objtrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(RentalEntryData._RENTAL_ENTRY).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(RentalEntryData._RENTAL_ENTRY, br_objtrans)
                .Item(RentalEntryData.RNTL_ENTRY_ID) = intMax
                '   br_RentalRefNo = CommonUIs.GetIdentityCode(RentalEntryData._RENTAL_REFERENCE_NO, intMax, bv_datON_HR_DT, br_objtrans)
                '  br_RentalRefNo = IndexPatterns.GetIdentityCode(RentalEntryData._RENTAL_ENTRY, intMax, bv_datON_HR_DT, bv_i32DPT_ID, br_objtrans)
                .Item(RentalEntryData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(RentalEntryData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(RentalEntryData.CNTRCT_RFRNC_NO) = bv_strCNTRCT_RFRNC_NO
                If bv_strPO_RFRNC_NO <> Nothing Then
                    .Item(RentalEntryData.PO_RFRNC_NO) = bv_strPO_RFRNC_NO
                Else
                    .Item(RentalEntryData.PO_RFRNC_NO) = DBNull.Value
                End If
                If bv_datON_HR_DT <> Nothing Then
                    .Item(RentalEntryData.ON_HR_DT) = bv_datON_HR_DT
                Else
                    .Item(RentalEntryData.ON_HR_DT) = DBNull.Value
                End If
                If bv_datOFF_HR_DT <> String.Empty Then
                    .Item(RentalEntryData.OFF_HR_DT) = CDate(bv_datOFF_HR_DT)
                Else
                    .Item(RentalEntryData.OFF_HR_DT) = DBNull.Value
                End If
                If bv_strRMRKS_VC <> Nothing Then
                    .Item(RentalEntryData.RMRKS_VC) = bv_strRMRKS_VC
                Else
                    .Item(RentalEntryData.RMRKS_VC) = DBNull.Value
                End If
                .Item(RentalEntryData.RNTL_RFRNC_NO) = br_RentalRefNo
                If bv_GI_Trans_no <> Nothing Then
                    .Item(RentalEntryData.GI_TRNSCTN_NO) = bv_GI_Trans_no
                Else
                    .Item(RentalEntryData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(RentalEntryData.DPT_ID) = bv_i32DPT_ID
                .Item(RentalEntryData.CRTD_BY) = bv_strCRTD_BY
                .Item(RentalEntryData.CRTD_DT) = bv_datCRTD_DT
                .Item(RentalEntryData.MDFD_BY) = bv_strMDFD_BY
                .Item(RentalEntryData.MDFD_DT) = bv_datMDFD_DT
                .Item(RentalEntryData.OTHR_CHRG_NC) = bv_OtherCharge
                .Item(RentalEntryData.GTN_BT) = bv_gtnbt
            End With
            objData.InsertRow(dr, Rental_EntryInsertQuery, br_objtrans)
            dr = Nothing
            CreateRentalEntry = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateRentalEntry() TABLE NAME:Rental_Entry ()"
    Public Function UpdateRentalEntry(ByVal bv_i64RNTL_ENTRY_ID As Int64, _
                                      ByVal bv_strEQPMNT_NO As String, _
                                      ByVal bv_i64CSTMR_ID As Int64, _
                                      ByVal bv_strCNTRCT_RFRNC_NO As String, _
                                      ByVal bv_strPO_RFRNC_NO As String, _
                                      ByVal bv_datOnHireDate As Date, _
                                      ByVal bv_datOFF_HR_DT As String, _
                                      ByVal bv_dblOTHR_CHRG_NC As Double, _
                                      ByVal bv_strRMRKS_VC As String, _
                                      ByRef br_RentalRefNo As String, _
                                      ByVal bv_i32DPT_ID As Int32, _
                                      ByVal bv_strMDFD_BY As String, _
                                      ByVal bv_datMDFD_DT As DateTime, _
                                      ByVal bv_AdditionalCharge As Double, _
                                      ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RentalEntryData._RENTAL_ENTRY).NewRow()
            With dr
                .Item(RentalEntryData.RNTL_ENTRY_ID) = bv_i64RNTL_ENTRY_ID
                .Item(RentalEntryData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(RentalEntryData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(RentalEntryData.CNTRCT_RFRNC_NO) = bv_strCNTRCT_RFRNC_NO
                If bv_strPO_RFRNC_NO <> Nothing Then
                    .Item(RentalEntryData.PO_RFRNC_NO) = bv_strPO_RFRNC_NO
                Else
                    .Item(RentalEntryData.PO_RFRNC_NO) = DBNull.Value
                End If
                If bv_datOnHireDate <> Nothing Then
                    .Item(RentalEntryData.ON_HR_DT) = bv_datOnHireDate
                Else
                    .Item(RentalEntryData.ON_HR_DT) = DBNull.Value
                End If
                If bv_datOFF_HR_DT <> String.Empty Then
                    .Item(RentalEntryData.OFF_HR_DT) = CDate(bv_datOFF_HR_DT)
                Else
                    .Item(RentalEntryData.OFF_HR_DT) = DBNull.Value
                End If
                If bv_strRMRKS_VC <> Nothing Then
                    .Item(RentalEntryData.RMRKS_VC) = bv_strRMRKS_VC
                Else
                    .Item(RentalEntryData.RMRKS_VC) = DBNull.Value
                End If
                .Item(RentalEntryData.RNTL_RFRNC_NO) = br_RentalRefNo
                .Item(RentalEntryData.DPT_ID) = bv_i32DPT_ID
                .Item(RentalEntryData.MDFD_BY) = bv_strMDFD_BY
                .Item(RentalEntryData.MDFD_DT) = bv_datMDFD_DT
                .Item(RentalEntryData.OTHR_CHRG_NC) = bv_AdditionalCharge
            End With
            UpdateRentalEntry = objData.UpdateRow(dr, Rental_EntryUpdateQuery, br_objtrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateRentalOtherCharge() TABLE NAME:Rental_Other_Charge"
    Public Function CreateRentalOtherCharge(ByVal bv_i64RNTL_ENTRY_ID As Int64, _
        ByVal bv_i64ADDTNL_CHRG_RT_ID As Int64, _
        ByVal bv_dblRT_NC As Double, _
            ByRef br_objtrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(RentalEntryData._RENTAL_OTHER_CHARGE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(RentalEntryData._RENTAL_OTHER_CHARGE, br_objtrans)
                .Item(RentalEntryData.RNTL_OTHR_CHRG_ID) = intMax
                .Item(RentalEntryData.RNTL_ENTRY_ID) = bv_i64RNTL_ENTRY_ID
                .Item(RentalEntryData.ADDTNL_CHRG_RT_ID) = bv_i64ADDTNL_CHRG_RT_ID
                .Item(RentalEntryData.RT_NC) = bv_dblRT_NC
            End With
            objData.InsertRow(dr, Rental_Other_ChargeInsertQuery, br_objtrans)
            dr = Nothing
            CreateRentalOtherCharge = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateRentalOtherCharge() TABLE NAME:Rental_Other_Charge"
    Public Function UpdateRentalOtherCharge(ByVal bv_i64RNTL_OTHR_CHRG_ID As Int64, _
        ByVal bv_i64RNTL_ENTRY_ID As Int64, _
        ByVal bv_i64ADDTNL_CHRG_RT_ID As Int64, _
        ByVal bv_dblRT_NC As Double, _
            ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RentalEntryData._RENTAL_OTHER_CHARGE).NewRow()
            With dr
                .Item(RentalEntryData.RNTL_OTHR_CHRG_ID) = bv_i64RNTL_OTHR_CHRG_ID
                .Item(RentalEntryData.RNTL_ENTRY_ID) = bv_i64RNTL_ENTRY_ID
                .Item(RentalEntryData.ADDTNL_CHRG_RT_ID) = bv_i64ADDTNL_CHRG_RT_ID
                .Item(RentalEntryData.RT_NC) = bv_dblRT_NC
            End With
            UpdateRentalOtherCharge = objData.UpdateRow(dr, Rental_Other_ChargeUpdateQuery, br_objtrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getRentalEntryID"
    Public Function getRentalEntryID(ByVal bv_strEqpmntNO As String, _
                                    ByRef bv_RentalEntryID As Int64, _
                                    ByRef br_ObjTransactions As Transactions) As Long
        Try
            objData = New DataObjects(RentalEntryIDQuery, RentalEntryData.EQPMNT_NO, bv_strEqpmntNO)
            Return objData.ExecuteScalar(br_ObjTransactions)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "getSupplierEquipment() "
    Public Function getSupplierEquipment(ByVal bv_intDepotId As Integer) As RentalEntryDataSet
        Try
            objData = New DataObjects(SupplierEquipmentNoSelectQuery, RentalEntryData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), RentalEntryData._SUPPLIER_EQUIPMENT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetAdditionalChargeRateByDepotId()  Table Name: Additional_Charge_Rate"

    Public Function GetAdditionalChargeRateByDepotId(ByVal bv_intDepotId As Int32) As RentalEntryDataSet
        Try
            objData = New DataObjects(V_AdditionalChargeSelectQuery, RentalEntryData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), RentalEntryData._V_ADDITIONAL_CHARGE_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateTracking() TABLE NAME:Tracking"

    Public Function UpdateTracking(ByVal bv_strEQPMNT_NO As String, _
                ByVal bv_i64CustomerID As Int64, _
                ByVal ActivityDate As DateTime, _
                ByVal bv_strActivityRemarks As String, _
                ByVal strRentalRefNo As String, _
                ByVal bv_i32DepotId As Int32, _
                ByVal bv_ModifiedBy As String, _
                ByVal bv_ModifiedDate As DateTime, _
                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RentalEntryData._TRACKING).NewRow()
            With dr
                .Item(RentalEntryData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(RentalEntryData.CSTMR_ID) = bv_i64CustomerID
                .Item(GateinData.ACTVTY_DT) = ActivityDate
                If bv_strActivityRemarks <> Nothing Then
                    .Item(RentalEntryData.ACTVTY_RMRKS) = bv_strActivityRemarks
                Else
                    .Item(RentalEntryData.ACTVTY_RMRKS) = DBNull.Value
                End If
                .Item(RentalEntryData.RFRNC_NO) = strRentalRefNo
                .Item(RentalEntryData.DPT_ID) = bv_i32DepotId
                .Item(RentalEntryData.MDFD_BY) = bv_ModifiedBy
                .Item(RentalEntryData.MDFD_DT) = bv_ModifiedDate
            End With
            UpdateTracking = objData.UpdateRow(dr, TrackingUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateTracking() TABLE NAME:Tracking"

    Public Function CreateTracking(ByVal bv_i64CustomerId As Int64, _
        ByVal bv_strEquipmentNo As String, _
        ByVal bv_strActivityName As String, _
        ByVal bv_i64Status As Int64, _
        ByVal bv_i64ActivityNo As Int64, _
        ByVal bv_datActivitydate As DateTime, _
        ByVal bv_strActivityRemarks As String, _
        ByVal bv_strYardLocation As String, _
        ByVal bv_strGateInTransactionNumber As String, _
        ByVal bv_i64InvoicingPartyId As Int64, _
        ByVal bv_strReferenceNo As String, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_strCancelledBy As String, _
        ByVal bv_datCancelledDate As DateTime, _
        ByVal bv_strADTRemarks As String, _
        ByVal bv_i32DepotID As Int32, _
        ByVal bv_i64RentalCstmrId As Int64, _
        ByVal bv_strRentalRefNo As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._TRACKING).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateOutData._TRACKING, br_objTrans)
                .Item(GateOutData.TRCKNG_ID) = intMax
                .Item(GateOutData.CSTMR_ID) = bv_i64CustomerId
                .Item(GateOutData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateOutData.ACTVTY_NAM) = bv_strActivityName
                If bv_i64Status <> 0 Then
                    .Item(GateOutData.EQPMNT_STTS_ID) = bv_i64Status
                Else
                    .Item(GateOutData.EQPMNT_STTS_ID) = DBNull.Value
                End If
                .Item(GateOutData.ACTVTY_NO) = bv_i64ActivityNo
                .Item(GateOutData.ACTVTY_DT) = bv_datActivitydate
                If bv_strActivityRemarks <> Nothing Then
                    .Item(GateOutData.ACTVTY_RMRKS) = bv_strActivityRemarks
                Else
                    .Item(GateOutData.ACTVTY_RMRKS) = DBNull.Value
                End If
                If bv_strYardLocation <> Nothing Then
                    .Item(GateOutData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(GateOutData.YRD_LCTN) = DBNull.Value
                End If
                If bv_strGateInTransactionNumber <> Nothing Then
                    .Item(GateOutData.GI_TRNSCTN_NO) = bv_strGateInTransactionNumber
                Else
                    .Item(GateOutData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                If bv_i64InvoicingPartyId <> 0 Then
                    .Item(GateOutData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyId
                Else
                    .Item(GateOutData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                If bv_strReferenceNo <> Nothing Then
                    .Item(GateOutData.RFRNC_NO) = bv_strReferenceNo
                Else
                    .Item(GateOutData.RFRNC_NO) = DBNull.Value
                End If
                .Item(GateOutData.CRTD_BY) = bv_strCreatedBy
                .Item(GateOutData.CRTD_DT) = bv_datCreatedDate
                If bv_strModifiedBy <> Nothing Then
                    .Item(GateOutData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(GateOutData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(GateOutData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(GateOutData.MDFD_DT) = DBNull.Value
                End If
                If bv_strCancelledBy <> Nothing Then
                    .Item(GateOutData.CNCLD_BY) = bv_strCancelledBy
                Else
                    .Item(GateOutData.CNCLD_BY) = DBNull.Value
                End If
                If bv_datCancelledDate <> Nothing Then
                    .Item(GateOutData.CNCLD_DT) = bv_datCancelledDate
                Else
                    .Item(GateOutData.CNCLD_DT) = DBNull.Value
                End If
                If bv_strADTRemarks <> Nothing Then
                    .Item(GateOutData.ADT_RMRKS) = bv_strADTRemarks
                Else
                    .Item(GateOutData.ADT_RMRKS) = DBNull.Value
                End If
                .Item(GateOutData.DPT_ID) = bv_i32DepotID
                If bv_i64RentalCstmrId <> 0 Then
                    .Item(GateOutData.RNTL_CSTMR_ID) = bv_i64RentalCstmrId
                Else
                    .Item(GateOutData.RNTL_CSTMR_ID) = DBNull.Value
                End If
                If bv_strRentalRefNo <> Nothing Then
                    .Item(GateOutData.RNTL_RFRNC_NO) = bv_strRentalRefNo
                Else
                    .Item(GateOutData.RNTL_RFRNC_NO) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, TrackingInsertQuery, br_objTrans)
            dr = Nothing
            CreateTracking = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "getDefaultRates() "
    Public Function getDefaultRates(ByVal bv_intDepotId As Integer) As RentalEntryDataSet
        Try
            objData = New DataObjects(DefaultRateSelectQueryBy, RentalEntryData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), RentalEntryData._DEFAULT_ADDITIONAL_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateRentalCharge() TABLE NAME:Rental_Charge"
    Public Function UpdateRentalCharge(ByVal bv_strEquipmentNo As String, _
                                       ByVal bv_strRentalRefNo As String, _
                                       ByVal bv_dblOtherCharge As Double, _
                                       ByVal bv_datOnHireDate As Date, _
                                       ByVal bv_datOffHireDate As String, _
                                       ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RentalEntryData._RENTAL_CHARGE).NewRow()
            With dr
                .Item(RentalEntryData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(RentalEntryData.RNTL_RFRNC_NO) = bv_strRentalRefNo
                .Item(RentalEntryData.OTHR_CHRG_NC) = bv_dblOtherCharge
                If bv_datOffHireDate <> String.Empty Then
                    .Item(RentalEntryData.TO_BLLNG_DT) = CDate(bv_datOffHireDate)
                Else
                    .Item(RentalEntryData.TO_BLLNG_DT) = DBNull.Value
                End If
                If bv_datOnHireDate <> Nothing Then
                    .Item(RentalEntryData.FRM_BLLNG_DT) = bv_datOnHireDate
                Else
                    .Item(RentalEntryData.FRM_BLLNG_DT) = DBNull.Value
                End If
            End With
            UpdateRentalCharge = objData.UpdateRow(dr, Rental_ChargeUpdateQuery, br_objtrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteRentalAdditionalCharge() "

    Public Function DeleteRentalAdditionalCharge(ByVal bv_RentalEntryID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).NewRow()
            With dr
                .Item(RentalEntryData.RNTL_ENTRY_ID) = bv_RentalEntryID
            End With
            DeleteRentalAdditionalCharge = objData.DeleteRow(dr, RentalEquipmentDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteRentalEntry() "

    Public Function DeleteRentalEntry(ByVal bv_RentalEntryID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(RentalEntryData._V_RENTAL_ENTRY).NewRow()
            With dr
                .Item(RentalEntryData.RNTL_ENTRY_ID) = bv_RentalEntryID
            End With
            DeleteRentalEntry = objData.DeleteRow(dr, RentalEntryDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetAdditionalChargeRateById"
    Public Function GetAdditionalChargeRateById(ByVal bv_i64AditionalChargeRateId As Int64, _
                                                 ByVal bv_intDepotId As Int32) As Double
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(RentalEntryData.ADDTNL_CHRG_RT_ID, bv_i64AditionalChargeRateId)
            hshparameters.Add(RentalEntryData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(V_AdditionalChargeSelectQueryById, hshparameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetOffHireDate() Table Name: Rental_entry"
    Public Function GetOffHireDate(ByVal bv_intRentalID As Integer, ByVal bv_strEquipmentNo As String) As RentalEntryDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(RentalEntryData.RNTL_ENTRY_ID, bv_intRentalID)
            hshTable.Add(RentalEntryData.EQPMNT_NO, bv_strEquipmentNo)
            objData = New DataObjects(GetOffHireSelectQueryBy, hshTable)
            objData.Fill(CType(ds, DataSet), RentalEntryData._V_RENTAL_ENTRY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRentalEntryEquipmentByID()  TABLE NAME:Rental_Entry"

    Public Function GetRentalEntryEquipmentByID(ByVal bv_strEquipmentNo As String, _
                                                ByVal bv_i32DepotID As Int32, _
                                                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim hshParameters As New Hashtable()
            Dim dsRental As New RentalEntryDataSet
            Dim blnRentalExist As Boolean = False
            hshParameters.Add(RentalEntryData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(RentalEntryData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(RentalEntrySelectQueryByEquipmentNo, hshParameters)
            objData.Fill(CType(dsRental, DataSet), RentalEntryData._V_RENTAL_ENTRY, br_ObjTransactions)
            If dsRental.Tables(RentalEntryData._V_RENTAL_ENTRY).Rows.Count > 0 Then
                blnRentalExist = True
            End If
            Return blnRentalExist
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region ""

#End Region
#Region "GetRentalOtherCharges"
    Function GetRentalOtherChargesByDepotId() As RentalEntryDataSet
        Try
            Dim dsRental As New RentalEntryDataSet
            objData = New DataObjects(RentalOtherChargesSelectQuery)
            objData.Fill(CType(dsRental, DataSet), RentalEntryData._V_RENTAL_OTHER_CHARGE)
            Return dsRental
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
