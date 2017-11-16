Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

Public Class DARs

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_Eqpmnt_ActvtySelectQuery As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,(SELECT EQPMNT_STTS_DSCRPTN_VC FROM EQUIPMENT_STATUS WHERE EQPMNT_STTS_ID=VAS.EQPMNT_STTS_ID)EQPMNT_STTS_DSCRPTN_VC, ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,(SELECT VHCL_NO FROM GATEIN WHERE EQPMNT_NO=VAS.EQPMNT_NO AND GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO)VHCL_NO,(SELECT TRNSPRTR_CD FROM GATEIN WHERE EQPMNT_NO=VAS.EQPMNT_NO AND GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO)TRNSPRTR_CD,INSPCTN_DT,INSTRCTNS_VC,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,CERT_GNRTD_FLG,RPR_CMPLTN_DT FROM V_ACTIVITY_STATUS VAS WHERE ACTVTY_DT < CONVERT(VARCHAR,GETDATE (),106) AND DPT_ID=@DPT_ID AND CSTMR_CD=@CSTMR_ID ORDER BY ACTVTY_STTS_ID"
    Private Const V_Eqpmnt_Actvty_SttsSelectQuery As String = "SELECT EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_STTS_ID,EQPMNT_STTS_CD,(SELECT EQPMNT_STTS_DSCRPTN_VC FROM EQUIPMENT_STATUS WHERE EQPMNT_STTS_ID=VAS.EQPMNT_STTS_ID)EQPMNT_STTS_DSCRPTN_VC,ACTVTY_DT,DPT_ID FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID=@DPT_ID "
    Private Const V_InventorySelectQuery As String = "SELECT INVNTRY_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_LNGTH,EQPMNT_SZ_ID,EQPMNT_HGT,MNFCTR_DT,EQPMNT_MTRL,YRD_LCTN,VSSL_NM,VYG_NO,EQPMNT_STTS,ACTVTY_TYPE,EQPMNT_STTS_ID,LSS_ID,LSS_CD,GT_IN_DT,GT_IN_RMRKS,RPR_ESTMT_NO,RPR_ESTMT_DT,RPR_ESTMT_AMNT,RPR_ESTMT_AMNT_TX,RPR_MTRL_CST_NC,RPR_CLNG_CST_NC,RPR_LBR_CST_NC,RPR_ESTMT_RVSN_NO,DPP_AMNT_NC,OWNR_TTL_NC,USR_TTL_NC,INSRR_TTL_NC,DPT_TTL_NC,SPCL_BLLNG_TTL_NC,PRTY_NT_SPCFD_TTL_NC,RPR_RMRKS,LSS_APPRVL_DT,LSS_APPRVL_AMNT_NC,OWNR_APPRVL_DT,OWNR_APPRVL_AMNT_NC,RPR_CMPLTN_DT,RPR_CMPLTN_RMRKS,SRVYR_NM,SRVY_DT,GT_OUT_DT,ACTV_BT,CSTMR_ID,CSTMR_CD,DPT_ID,DPT_CD,EQPMNT_STTS_DSCRPTN_VC,CSTMR_NAM FROM V_INVENTORY WHERE DPT_ID=@DPT_ID AND CSTMR_CD=@CSTMR_CD"
    Private Const Depot_SttsSelectQuery As String = "SELECT DPT_ID,DPT_CD,DPT_NAM,CNTCT_PRSN_NAM,ADDRSS_LN1_VC,ADDRSS_LN2_VC,ADDRSS_LN3_VC,VT_NO,EML_ID,PHN_NO,FX_NO,CMPNY_LG_PTH,MDFD_BY,MDFD_DT FROM V_DEPOT WHERE DPT_ID=@DPT_ID"
    Private Const Depot_Customer_SelectQuery As String = "SELECT DISTINCT(CSTMR_CD),CSTMR_ID FROM V_EQPMNT_ACTVTY WHERE DPT_ID =@DPT_ID"
    Private Const Depot_Customer_SelectQuery_inventory As String = "SELECT DISTINCT(CSTMR_CD),CSTMR_ID FROM V_INVENTORY WHERE DPT_ID =@DPT_ID"
    Private Const Eqpmnt_ActvtyUpdateQuery As String = "UPDATE EQUIPMENT_ACTIVITY SET RPT_STTS =1 WHERE RPT_STTS=0 OR RPT_STTS=NULL AND CSTMR_ID =@CSTMR_ID AND DPT_ID =@DPT_ID"
    Private Const CustomerEmailSettingSelectQuery As String = "SELECT CSTMR_EML_STTNG_BIN,CSTMR_ID,CSTMR_CD,RPRT_ID,RPRT_CD,GNRTN_TM,TO_EML,CC_EML,SBJCT_VCR,ACTV_BT,DPT_ID,PRDC_FLTR_ID,PRDC_FLTR_CD,PRDC_DY_ID,PRDC_DY_CD,PRDC_DT_ID,PRDC_DT_CD,NXT_RN_DT_TM FROM V_CUSTOMER_EMAIL_SETTING WHERE DPT_ID=@DPT_ID AND CSTMR_ID IN (select distinct(TRACKING.CSTMR_ID) FROM TRACKING inner join V_CUSTOMER_EMAIL_SETTING on V_CUSTOMER_EMAIL_SETTING.CSTMR_ID=TRACKING.CSTMR_ID where TRACKING.mdfd_dt>=V_CUSTOMER_EMAIL_SETTING.LST_RN_DT_TM)"
    Private Const CustomerEmailSettingUpdateQuery As String = "UPDATE CUSTOMER_EMAIL_SETTING SET NXT_RN_DT_TM=@NXT_RN_DT_TM ,LST_RN_DT_TM=@LST_RN_DT_TM WHERE CSTMR_ID=@CSTMR_ID AND RPRT_ID=@RPRT_ID AND CSTMR_EML_STTNG_BIN=@CSTMR_EML_STTNG_BIN"
    Private Const CustomerScheduleTimeSelectQuery As String = "SELECT CSTMR_EML_STTNG_BIN,CSTMR_ID,CSTMR_CD,RPRT_ID,RPRT_CD,GNRTN_TM,TO_EML,CC_EML,SBJCT_VCR,ACTV_BT,DPT_ID,PRDC_FLTR_ID,PRDC_FLTR_CD,PRDC_DY_ID,PRDC_DY_CD,PRDC_DT_ID,PRDC_DT_CD,NXT_RN_DT_TM FROM V_CUSTOMER_EMAIL_SETTING WHERE NXT_RN_DT_TM<=getdate() AND DPT_ID=@DPT_ID"
    Private Const CustomerEmailSettingUpdatNextRunDateQuery As String = "UPDATE CUSTOMER_EMAIL_SETTING SET NXT_RN_DT_TM=@NXT_RN_DT_TM,LST_RN_DT_TM=@LST_RN_DT_TM WHERE CSTMR_ID=@CSTMR_ID AND RPRT_ID=@RPRT_ID AND CSTMR_EML_STTNG_BIN=@CSTMR_EML_STTNG_BIN"
    Private ds As DARDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New DARDataSet
    End Sub

#End Region

#Region "GetV_Eqpmnt_Actvty() TABLE NAME:V_EQPMNT_ACTVTY"

    Public Function GetV_Eqpmnt_Actvty(ByVal bv_intDepotId As Int64, ByVal bv_strCustomerid As Integer) As DARDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(DARData.DPT_ID, bv_intDepotId)
            hshParameters.Add(DARData.CSTMR_ID, bv_strCustomerid)
            objData = New DataObjects(V_Eqpmnt_ActvtySelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), DARData._V_ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_Eqpmnt_Actvty_Stts() TABLE NAME:V_EQPMNT_ACTVTY_STTS"

    Public Function GetV_Eqpmnt_Actvty_Stts(ByVal bv_intDepotId As Int64) As DARDataSet
        Try
            objData = New DataObjects(V_Eqpmnt_Actvty_SttsSelectQuery, DARData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), DARData._ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_Inventory() TABLE NAME:V_INVENTORY"
    Public Function GetV_Inventory(ByVal bv_intDepotId As Int64, ByVal bv_strCustomerCode As String) As DARDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(DARData.DPT_ID, bv_intDepotId)
            hshParameters.Add(DARData.CSTMR_CD, bv_strCustomerCode)
            objData = New DataObjects(V_InventorySelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), DARData._V_INVENTORY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Get_Depot() TABLE NAME:Depot"

    Public Function Get_Depot(ByVal bv_intDepotId As Int64) As DARDataSet
        Try
            objData = New DataObjects(Depot_SttsSelectQuery, DARData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), DARData._V_ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Get_Customer() TABLE NAME:Depot"

    Public Function Get_Customer(ByVal bv_intDepotId As Int64) As DARDataSet
        Try
            objData = New DataObjects(Depot_Customer_SelectQuery, DARData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), DARData._V_DEPOT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Get_Customer_inventory() TABLE NAME:Depot"

    Public Function Get_Customer_inventory(ByVal bv_intDepotId As Int64) As DARDataSet
        Try
            objData = New DataObjects(Depot_Customer_SelectQuery_inventory, DARData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), DARData._V_DEPOT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_UpdateRPT_Status() TABLE NAME:EQUIPMENT_ACTIVITY"

    Public Function UpdateRPT_Status(ByVal bv_intDepotId As Int64, ByVal bv_intCustomerID As Int64) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects
            dr = ds.Tables(DARData._EQUIPMENT_ACTIVITY).NewRow()
            With dr
                .Item(DARData.CSTMR_ID) = bv_intCustomerID
                .Item(DARData.DPT_ID) = bv_intDepotId
            End With
            UpdateRPT_Status = objData.UpdateRow(dr, Eqpmnt_ActvtyUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "Get_Customer() TABLE NAME:Depot"
    Public Function GetEmailSetting(ByVal bv_intDepotId As Int64) As DARDataSet
        Try
            objData = New DataObjects(CustomerEmailSettingSelectQuery, DARData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), DARData._V_CUSTOMER_EMAIL_SETTING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



#Region "UpdatePre_Advice"
    Public Function pub_UpdateCustomerEmailSetting(ByVal bv_intDepotId As Int64, _
                                                   ByVal bv_intCustomerId As Integer, _
                                                   ByVal bv_intReportId As Integer, _
                                                   ByVal dtNextDate As Date, _
                                                   ByVal dtLastDate As Date, _
                                                   ByVal cstmr_email_id As Integer) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(DARData._CUSTOMER_EMAIL_SETTING).NewRow()
            With dr
                .Item(DARData.CSTMR_ID) = bv_intCustomerId
                .Item(DARData.RPRT_ID) = bv_intReportId
                .Item(DARData.NXT_RN_DT_TM) = dtNextDate
                .Item(DARData.LST_RN_DT_TM) = dtLastDate
                .Item(DARData.CSTMR_EML_STTNG_BIN) = cstmr_email_id

            End With

            pub_UpdateCustomerEmailSetting = objData.UpdateRow(dr, CustomerEmailSettingUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "GetScheduleTime() TABLE NAME:Customer_EmailSetting"

    Public Function GetScheduleTime(ByVal intDepotID As Integer) As DARDataSet
        Try
            objData = New DataObjects(CustomerScheduleTimeSelectQuery, DARData.DPT_ID,intDepotID)
            objData.Fill(CType(ds, DataSet), DARData._V_EMAIL_SETTING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "UpdatePre_Advice"
    Public Function pub_UpdateNextRunDate(ByVal bv_intDepotId As Int64, _
                                                   ByVal bv_intCustomerId As Integer, _
                                                   ByVal bv_intReportId As Integer, _
                                                   ByVal dtNextDate As Date, _
                                                   ByVal dtLastDate As Date, _
                                                   ByVal cstmr_email_id As Integer) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(DARData._CUSTOMER_EMAIL_SETTING).NewRow()
            With dr
                .Item(DARData.CSTMR_ID) = bv_intCustomerId
                .Item(DARData.RPRT_ID) = bv_intReportId
                .Item(DARData.NXT_RN_DT_TM) = dtNextDate
                .Item(DARData.LST_RN_DT_TM) = dtLastDate
                .Item(DARData.CSTMR_EML_STTNG_BIN) = cstmr_email_id

            End With

            pub_UpdateNextRunDate = objData.UpdateRow(dr, CustomerEmailSettingUpdatNextRunDateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class