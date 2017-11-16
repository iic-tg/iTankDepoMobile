Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

Public Class Transportations
#Region "Declaration Part.. "
    Dim objData As DataObjects
    Private Const V_TransportationSelectQueryByTransportationId As String = "SELECT TRNSPRTTN_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_CD,RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_ID,PCK_UP_LCTN_CD,PCK_UP_LCTN_DSCRPTN_VC,DRP_OFF_LCTN_ID,DRP_OFF_LCTN_CD,DRP_OFF_LCTN_DSCRPTN_VC,ACTVTY_ID,ACTVTY_CD,ACTVTY_LCTN_ID,ACTVTY_LCTN_CD,TRNSPRTTN_STTS_ID,TRNSPRTTN_STTS_CD,RQST_NO,RQST_DT,RMRKS_VC,MDFD_BY,MDFD_DT,CRTD_BY,CRTD_DT,CNCLD_BY,CNCLD_DT,DPT_ID,DPT_CD,DPT_NAM,ADDRSS_LN1_VC,ADDRSS_LN2_VC,ADDRSS_LN3_VC,EML_ID,PHN_NO,FX_NO,EQPMNT_STT_ID,EQPMNT_STT_CD,TRP_RT_NC,ACTVTY_LCTN_DSCRPTN_VC,TRNSPRTR_ID,TRNSPRTR_CD FROM V_TRANSPORTATION WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID AND DPT_ID=@DPT_ID"
    Private Const Transportation_DetailSelectQueryByTransportationID As String = "SELECT TRNSPRTTN_DTL_ID,TRNSPRTTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EMPTY_SNGL_ID,EMPTY_SNGL_CD,CSTMR_RF_NO,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,TTL_RT_NC,JB_STRT_DT,JB_END_DT,RMRKS_VC,BLLNG_FLG FROM V_TRANSPORTATION_DETAIL WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID"
    Private Const V_CustomerSelectQuery As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,CSTMR_CRRNCY_CD,BLLNG_TYP_ID,CNTCT_PRSN_NAM,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,RPR_TCH_EML_ID,BLK_EML_FRMT_ID,HYDR_AMNT_NC,PNMTC_AMNT_NC,LBR_RT_PR_HR_NC,LK_TST_RT_NC,SRVY_ONHR_OFFHR_RT_NC,PRDC_TST_TYP_ID,VLDTY_PRD_TST_YRS,MIN_HTNG_RT_NC,MIN_HTNG_PRD_NC,HRLY_CHRG_NC,CHK_DGT_VLDTN_BT,TRNSPRTTN_BT,RNTL_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,XML_BT FROM V_CUSTOMER WHERE CSTMR_ID=@CSTMR_ID AND TRNSPRTTN_BT=1 AND DPT_ID=@DPT_ID"
    Private Const TransportationDetailSelectQuery As String = "SELECT TRNSPRTTN_DTL_ID,TRNSPRTTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,CSTMR_RF_NO,PRDCT_ID,TTL_RT_NC,JB_STRT_DT,JB_END_DT,RMRKS_VC,EMPTY_SNGL_ID FROM TRANSPORTATION_DETAIL WHERE EQPMNT_NO = @EQPMNT_NO AND TRNSPRTTN_ID IN (SELECT TRNSPRTTN_ID FROM TRANSPORTATION WHERE CNCLD_BY IS NULL)  ORDER BY TRNSPRTTN_DTL_ID DESC"
    Private Const TransportationDetailEndDateSelectQuery As String = "SELECT COUNT(TRNSPRTTN_DTL_ID) AS TRNSPRTTN_DTL_ID  FROM TRANSPORTATION_DETAIL WHERE EQPMNT_NO = @EQPMNT_NO AND JB_END_DT < GETDATE() GROUP BY TRNSPRTTN_DTL_ID"
    Private Const V_Transportation_DetailSelectQuery As String = "SELECT TRNSPRTTN_DTL_RT_ID,TRNSPRTTN_DTL_ID,TRNSPRTTN_ID,ADDTNL_CHRG_RT_ID,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_NC,DFLT_BT FROM V_TRANSPORTATION_DETAIL_RATE WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID AND TRNSPRTTN_DTL_ID=@TRNSPRTTN_DTL_ID"
    Private Const V_AdditionalChargeSelectQuery As String = "SELECT ADDTNL_CHRG_RT_ID,OPRTN_TYP_ID,OPRTN_TYP_CD,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_DSCRPTN_VC,RT_NC,DFLT_BT,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM V_ADDITIONAL_CHARGE_RATE WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND DFLT_BT=1 AND OPRTN_TYP_ID=87"
    Private Const V_AdditionalChargeSelectQueryById As String = "SELECT RT_NC FROM V_ADDITIONAL_CHARGE_RATE WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND ADDTNL_CHRG_RT_ID=@ADDTNL_CHRG_RT_ID AND OPRTN_TYP_ID=87"
    Private Const TransportationInsertQuery As String = "INSERT INTO TRANSPORTATION(TRNSPRTTN_ID,CSTMR_ID,RT_ID,TRNSPRTR_ID,TRNSPRTTN_STTS_ID,RQST_NO,RQST_DT,EQPMNT_STT_ID,TRP_RT_NC,RMRKS_VC,NO_OF_TRPS,MDFD_BY,MDFD_DT,CRTD_BY,CRTD_DT,DPT_ID,ACTVTY_LCTN_ID)VALUES(@TRNSPRTTN_ID,@CSTMR_ID,@RT_ID,@TRNSPRTR_ID,@TRNSPRTTN_STTS_ID,@RQST_NO,@RQST_DT,@EQPMNT_STT_ID,@TRP_RT_NC,@RMRKS_VC,@NO_OF_TRPS,@MDFD_BY,@MDFD_DT,@CRTD_BY,@CRTD_DT,@DPT_ID,@ACTVTY_LCTN_ID)"
    Private Const TransportationUpdateQuery As String = "UPDATE TRANSPORTATION SET TRNSPRTTN_ID=@TRNSPRTTN_ID, CSTMR_ID=@CSTMR_ID, RT_ID=@RT_ID,TRNSPRTR_ID=@TRNSPRTR_ID, TRNSPRTTN_STTS_ID=@TRNSPRTTN_STTS_ID,RQST_DT=@RQST_DT, RMRKS_VC=@RMRKS_VC, NO_OF_TRPS=@NO_OF_TRPS, EQPMNT_STT_ID=@EQPMNT_STT_ID,TRP_RT_NC=@TRP_RT_NC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, DPT_ID=@DPT_ID WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID"
    Private Const TransportationUpdateCancelQuery As String = "UPDATE TRANSPORTATION SET TRNSPRTTN_STTS_ID=@TRNSPRTTN_STTS_ID, CNCLD_BY=@CNCLD_BY, CNCLD_DT=@CNCLD_DT WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID AND RQST_NO=@RQST_NO AND DPT_ID=@DPT_ID"
    Private Const Transportation_DetailInsertQuery As String = "INSERT INTO TRANSPORTATION_DETAIL(TRNSPRTTN_DTL_ID,TRNSPRTTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,EMPTY_SNGL_ID,CSTMR_RF_NO,PRDCT_ID,TTL_RT_NC,TRP_RT_NC,JB_STRT_DT,JB_END_DT,RMRKS_VC,SPPLR_RT_NC) VALUES (@TRNSPRTTN_DTL_ID,@TRNSPRTTN_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@EMPTY_SNGL_ID,@CSTMR_RF_NO,@PRDCT_ID,@TTL_RT_NC,@TRP_RT_NC,@JB_STRT_DT,@JB_END_DT,@RMRKS_VC,@SPPLR_RT_NC)"
    Private Const Transportation_Detail_RateInsertQuery As String = "INSERT INTO TRANSPORTATION_DETAIL_RATE(TRNSPRTTN_DTL_RT_ID,TRNSPRTTN_DTL_ID,TRNSPRTTN_ID,ADDTNL_CHRG_RT_ID,ADDTNL_CHRG_RT_NC)VALUES(@TRNSPRTTN_DTL_RT_ID,@TRNSPRTTN_DTL_ID,@TRNSPRTTN_ID,@ADDTNL_CHRG_RT_ID,@ADDTNL_CHRG_RT_NC)"
    Private Const V_Transportation_Detail_RateSelectQuery As String = "SELECT TRNSPRTTN_DTL_RT_ID,TRNSPRTTN_DTL_ID,TRNSPRTTN_ID,ADDTNL_CHRG_RT_ID,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_NC FROM V_TRANSPORTATION_DETAIL_RATE WHERE TRNSPRTTN_DTL_ID=@TRNSPRTTN_DTL_ID"
    Private Const V_Transportation_Detail_RateSelectQueryByTransportationId As String = "SELECT TRNSPRTTN_DTL_RT_ID,TRNSPRTTN_DTL_ID,TRNSPRTTN_ID,ADDTNL_CHRG_RT_ID,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_NC,DFLT_BT FROM V_TRANSPORTATION_DETAIL_RATE WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID"
    Private Const Transportation_Detail_RateUpdateQuery As String = "UPDATE TRANSPORTATION_DETAIL_RATE SET TRNSPRTTN_DTL_RT_ID=@TRNSPRTTN_DTL_RT_ID, TRNSPRTTN_DTL_ID=@TRNSPRTTN_DTL_ID, TRNSPRTTN_ID=@TRNSPRTTN_ID, ADDTNL_CHRG_RT_ID=@ADDTNL_CHRG_RT_ID, ADDTNL_CHRG_RT_NC=@ADDTNL_CHRG_RT_NC WHERE TRNSPRTTN_DTL_RT_ID=@TRNSPRTTN_DTL_RT_ID"
    'Private Const Transportation_Detail_RateDeleteQuery As String = "DELETE FROM TRANSPORTATION_DETAIL_RATE WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID AND TRNSPRTTN_DTL_RT_ID=@TRNSPRTTN_DTL_RT_ID"
    Private Const Transportation_Detail_RateDeleteQueryByDetailRateId As String = "DELETE FROM TRANSPORTATION_DETAIL_RATE WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID AND TRNSPRTTN_DTL_ID=@TRNSPRTTN_DTL_ID"
    Private Const Transportation_Detail_RateDeleteQueryByTransportationId As String = "DELETE FROM TRANSPORTATION_DETAIL_RATE WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID AND TRNSPRTTN_DTL_ID=@TRNSPRTTN_DTL_ID"
    Private Const Transportation_ChargeByTransportationId As String = "DELETE FROM TRANSPORTATION_CHARGE WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID"
    Private Const Transportation_ChargeByTransportationDetailId As String = "DELETE FROM TRANSPORTATION_CHARGE WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID AND TRNSPRTTN_DTL_ID=@TRNSPRTTN_DTL_ID"
    Private Const Transportation_DetailUpdateQuery As String = "UPDATE TRANSPORTATION_DETAIL SET SPPLR_RT_NC=@SPPLR_RT_NC, EQPMNT_NO=@EQPMNT_NO, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EMPTY_SNGL_ID=@EMPTY_SNGL_ID, CSTMR_RF_NO=@CSTMR_RF_NO, PRDCT_ID=@PRDCT_ID, TTL_RT_NC=@TTL_RT_NC, TRP_RT_NC=@TRP_RT_NC, JB_STRT_DT=@JB_STRT_DT, JB_END_DT=@JB_END_DT, RMRKS_VC=@RMRKS_VC WHERE TRNSPRTTN_DTL_ID=@TRNSPRTTN_DTL_ID"
    Private Const Transportation_DetailDeleteQuery As String = "DELETE FROM TRANSPORTATION_DETAIL WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID AND TRNSPRTTN_DTL_ID=@TRNSPRTTN_DTL_ID"
    Private Const EnumCodeSelectQuery As String = "SELECT ENM_ID,ENM_CD,ENM_DSCRPTN_VC,ENM_TYP_ID,ENM_TYP_CD FROM ENUM WHERE ENM_ID=@ENM_ID"
    Private Const TransportationDetailRateSelectQueryByDetailId As String = "SELECT COUNT(TRNSPRTTN_DTL_RT_ID) AS TRNSPRTTN_DTL_RT_ID FROM TRANSPORTATION_DETAIL_RATE WHERE TRNSPRTTN_DTL_ID = @TRNSPRTTN_DTL_ID AND TRNSPRTTN_ID=@TRNSPRTTN_ID AND ADDTNL_CHRG_RT_ID=@ADDTNL_CHRG_RT_ID"
    Private Const Transportation_ChargeInsertQuery As String = "INSERT INTO TRANSPORTATION_CHARGE(TRNSPRTTN_CHRG_ID,TRNSPRTTN_ID,TRNSPRTTN_DTL_ID,CSTMR_ID,INVC_DT,RT_ID,TRNSPRTR_ID,RQST_NO,EQPMNT_NO,EQPMNT_TYP_ID,EMPTY_SNGL_ID,CSTMR_RF_NO,EVNT_DT,TTL_RT_NC,TRP_RT_NC,DPT_ID,BLLNG_FLG,DRFT_INVC_NO,FNL_INVC_NO)VALUES(@TRNSPRTTN_CHRG_ID,@TRNSPRTTN_ID,@TRNSPRTTN_DTL_ID,@CSTMR_ID,@INVC_DT,@RT_ID,@TRNSPRTR_ID,@RQST_NO,@EQPMNT_NO,@EQPMNT_TYP_ID,@EMPTY_SNGL_ID,@CSTMR_RF_NO,@EVNT_DT,@TTL_RT_NC,@TRP_RT_NC,@DPT_ID,@BLLNG_FLG,@DRFT_INVC_NO,@FNL_INVC_NO)"
    Private Const TRANSPORTATION_CHARGESelectQueryByEqpmntNo As String = "SELECT TRNSPRTTN_CHRG_ID,INVC_DT,CSTMR_ID,RT_ID,RQST_NO,EQPMNT_NO,EQPMNT_TYP_ID,EMPTY_SNGL_ID,CSTMR_RF_NO,EVNT_DT,TTL_RT_NC,DPT_ID,BLLNG_FLG,DRFT_INVC_NO,FNL_INVC_NO,TRNSPRTR_ID FROM TRANSPORTATION_CHARGE WHERE RQST_NO=@RQST_NO AND TRNSPRTTN_ID=@TRNSPRTTN_ID AND TRNSPRTTN_DTL_ID=@TRNSPRTTN_DTL_ID  AND DPT_ID=@DPT_ID"
    Private Const TRANSPORTATION_CHARGESelectQueryByRequestNo As String = "SELECT TRNSPRTTN_CHRG_ID,INVC_DT,CSTMR_ID,RT_ID,RQST_NO,EQPMNT_NO,EQPMNT_TYP_ID,EMPTY_SNGL_ID,CSTMR_RF_NO,EVNT_DT,TTL_RT_NC,DPT_ID,BLLNG_FLG,DRFT_INVC_NO,FNL_INVC_NO,TRNSPRTR_ID FROM TRANSPORTATION_CHARGE WHERE RQST_NO=@RQST_NO AND DPT_ID=@DPT_ID"
    Private Const Transportation_ChargeUpdateQuery As String = "UPDATE TRANSPORTATION_CHARGE SET EQPMNT_NO=@EQPMNT_NO, EQPMNT_TYP_ID=@EQPMNT_TYP_ID,RT_ID = @RT_ID,TRNSPRTR_ID=@TRNSPRTR_ID, EMPTY_SNGL_ID=@EMPTY_SNGL_ID, CSTMR_RF_NO=@CSTMR_RF_NO, EVNT_DT=@EVNT_DT, TTL_RT_NC=@TTL_RT_NC, TRP_RT_NC=@TRP_RT_NC WHERE TRNSPRTTN_CHRG_ID=@TRNSPRTTN_CHRG_ID AND TRNSPRTTN_DTL_ID=@TRNSPRTTN_DTL_ID AND TRNSPRTTN_ID=@TRNSPRTTN_ID"
    Private Const V_Transportation_CustomerSelectQuery As String = "SELECT CSTMR_TRNSPRTTN_ID,CSTMR_ID,CSTMR_CD,CSTMR_CRRNCY_CD,RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_CD,PCK_UP_LCTN_DSCRPTN_VC,DRP_OFF_LCTN_CD,DRP_OFF_LCTN_DSCRPTN_VC,ACTVTY_CD,ACTVTY_LCTN_CD,ACTVTY_LCTN_DSCRPTN_VC,EMPTY_TRP_RT_NC,FLL_TRP_RT_NC FROM V_CUSTOMER_TRANSPORTATION WHERE CSTMR_ID=@CSTMR_ID AND RT_ID=@RT_ID"
    Private Const V_RouteSelectQueryByRouteId As String = "SELECT RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_ID,PCK_UP_LCTN_CD,DRP_OFF_LCTN_ID,DRP_OFF_LCTN_CD,ACTVTY_ID,ACTVTY_CD,ACTVTY_LCTN_ID,ACTVTY_LCTN_CD,EMPTY_TRP_RT_NC,FLL_TRP_RT_NC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM V_ROUTE WHERE RT_ID=@RT_ID AND DPT_ID=@DPT_ID"
    Private Const ExchangeRateSelectQuery As String = "SELECT EXCHNG_RT_PR_UNT_NC FROM EXCHANGE_RATE WHERE FRM_CRRNCY_ID= (SELECT CRRNCY_ID FROM BANK_DETAIL WHERE BNK_TYP_ID = 44 AND DPT_ID=@DPT_ID) AND TO_CRRNCY_ID=(SELECT CSTMR_CRRNCY_ID FROM CUSTOMER WHERE CSTMR_ID=@CSTMR_ID))"
    Private Const V_TransporterRouteDetailSelectQuery As String = "SELECT TRNSPRTR_RT_DTL_ID,TRNSPRTR_ID,TRNSPRTR_CD,RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_CD,DRP_OFF_LCTN_CD,EMPTY_TRP_SPPLR_RT_NC,EMPTY_TRP_CSTMR_RT_NC,FLL_TRP_SPPLR_RT_NC,FLL_TRP_CSTMR_RT_NC,DPT_ID,DPT_CRRNCY_CD FROM V_TRANSPORTER_ROUTE_DETAIL WHERE RT_ID=@RT_ID AND TRNSPRTR_ID=@TRNSPRTR_ID AND DPT_ID=@DPT_ID"
    Private Const V_TransporterRouteDetailSelectQueryByRouteId As String = "SELECT TRNSPRTR_RT_DTL_ID,TRNSPRTR_ID,TRNSPRTR_CD,RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_CD,DRP_OFF_LCTN_CD,EMPTY_TRP_SPPLR_RT_NC,EMPTY_TRP_CSTMR_RT_NC,FLL_TRP_SPPLR_RT_NC,FLL_TRP_CSTMR_RT_NC,DPT_ID,DPT_CRRNCY_CD FROM V_TRANSPORTER_ROUTE_DETAIL WHERE RT_ID=@RT_ID AND DPT_ID=@DPT_ID"
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
    Dim sqlDateDbnull As DateTime = "01/01/1900"

    'Supplier Rate
    Private Const SupplierEmptyTripRate_SelectQry As String = "SELECT EMPTY_TRP_SPPLR_RT_NC FROM TRANSPORTER_ROUTE_DETAIL WHERE TRNSPRTR_ID=@TRNSPRTR_ID AND RT_ID=@RT_ID"
    Private Const SupplierFullTripRate_SelectQry As String = "SELECT FLL_TRP_SPPLR_RT_NC FROM TRANSPORTER_ROUTE_DETAIL WHERE TRNSPRTR_ID=@TRNSPRTR_ID AND RT_ID=@RT_ID"

    Private ds As TransportationDataSet

#End Region

#Region "Constructor.."
    Sub New()
        ds = New TransportationDataSet
    End Sub
#End Region

#Region "GetTransportationByTransportationID() TABLE NAME:TRANSPORTATION"

    Public Function GetTransportationByTransportationID(ByVal bv_i64TransportationID As Int64, _
                                                        ByVal bv_intDepotId As Int32) As TransportationDataSet
        Try
            Dim hshparameter As New Hashtable
            hshparameter.Add(TransportationData.TRNSPRTTN_ID, bv_i64TransportationID)
            hshparameter.Add(TransportationData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(V_TransportationSelectQueryByTransportationId, hshparameter)
            objData.Fill(CType(ds, DataSet), TransportationData._V_TRANSPORTATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTransportationDetailByTransportationID() TABLE NAME:TRANSPORTATION_DETAIL"

    Public Function GetTransportationDetailByTransportationID(ByVal bv_i64TransportationID As Int64) As TransportationDataSet
        Try
            objData = New DataObjects(Transportation_DetailSelectQueryByTransportationID, TransportationData.TRNSPRTTN_ID, CStr(bv_i64TransportationID))
            objData.Fill(CType(ds, DataSet), TransportationData._V_TRANSPORTATION_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCustomerCurrencyByCustomerId() Table Name: CUSTOMER"

    Public Function GetCustomerCurrencyByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                    ByVal bv_intDepotId As Int32) As TransportationDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(TransportationData.CSTMR_ID, bv_i64CustomerId)
            hshparameters.Add(TransportationData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(V_CustomerSelectQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), TransportationData._V_CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTransportationDetailByEquipmentNo() TABLE NAME: TRANSPORTATION_DETAIL"

    Public Function GetTransportationDetailByEquipmentNo(ByVal bv_strEquipmentNo As String) As TransportationDataSet
        Try
            objData = New DataObjects(TransportationDetailSelectQuery, TransportationData.EQPMNT_NO, bv_strEquipmentNo)
            objData.Fill(CType(ds, DataSet), TransportationData._TRANSPORTATION_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentInformationEndDateByEquipmentNo() TABLE NAME: TRANSPORTATION_DETAIL"

    Public Function GetEquipmentInformationEndDateByEquipmentNo(ByVal bv_strEquipmentNo As String) As String
        Try
            objData = New DataObjects(TransportationDetailEndDateSelectQuery, TransportationData.EQPMNT_NO, bv_strEquipmentNo)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetTransportationDetailRate()  Table Name: Transportation_Detail_Rate"

    Public Function GetTransportationDetailRate(ByVal bv_i64TransportationId As Int64, _
                                                ByVal bv_i64TransportationDetailId As Int64) As TransportationDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(TransportationData.TRNSPRTTN_ID, bv_i64TransportationId)
            hshparameters.Add(TransportationData.TRNSPRTTN_DTL_ID, bv_i64TransportationDetailId)
            objData = New DataObjects(V_Transportation_DetailSelectQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), TransportationData._V_TRANSPORTATION_DETAIL_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTransportationDetailRateById()  Table Name: Transportation_Detail_Rate"

    Public Function GetTransportationDetailRateById(ByVal bv_i64TransportationId As Int64) As TransportationDataSet
        Try
            objData = New DataObjects(V_Transportation_Detail_RateSelectQueryByTransportationId, TransportationData.TRNSPRTTN_ID, bv_i64TransportationId)
            objData.Fill(CType(ds, DataSet), TransportationData._V_TRANSPORTATION_DETAIL_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetAdditionalChargeRateByDepotId()  Table Name: Additional_Charge_Rate"

    Public Function GetAdditionalChargeRateByDepotId(ByVal bv_intDepotId As Int32) As TransportationDataSet
        Try
            objData = New DataObjects(V_AdditionalChargeSelectQuery, TransportationData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), TransportationData._V_ADDITIONAL_CHARGE_RATE)
            Return ds
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
            hshparameters.Add(TransportationData.ADDTNL_CHRG_RT_ID, bv_i64AditionalChargeRateId)
            hshparameters.Add(TransportationData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(V_AdditionalChargeSelectQueryById, hshparameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateTransportation() Table Name: Transportation"
    Public Function CreateTransportation(ByVal bv_i64CustomerId As Int64, _
                                         ByVal bv_datRequestDate As DateTime, _
                                         ByVal bv_i64RouteId As Int64, _
                                         ByVal bv_i64TransporterId As Int64, _
                                         ByVal bv_i64ActivityLocationId As Int64, _
                                         ByVal bv_i64ActivityId As Int64, _
                                         ByVal bv_i64StatusId As Int64, _
                                         ByRef br_strRequestNo As String, _
                                         ByVal bv_decTripRate As Decimal, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_intNoOfTrip As Integer, _
                                         ByVal bv_strModifiedBy As String, _
                                         ByVal bv_datModifiedDate As DateTime, _
                                         ByVal bv_strCreatedBy As String, _
                                         ByVal bv_datCreatedDate As DateTime, _
                                         ByVal bv_i32DepotID As Int32, _
                                         ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TransportationData._TRANSPORTATION).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TransportationData._TRANSPORTATION, br_objTransaction)
                '  .Item(TransportationData.RQST_NO) = CommonUIs.GetIdentityCode(TransportationData._TRANSPORTATION, intMax, bv_datRequestDate, br_objTransaction)
                'From Index Pattern
                ' .Item(TransportationData.RQST_NO) = IndexPatterns.GetIdentityCode(TransportationData._TRANSPORTATION, intMax, bv_datRequestDate, bv_i32DepotID, br_objTransaction)
                .Item(TransportationData.RQST_NO) = br_strRequestNo
                '.Item(TransportationData.RQST_NO)
                .Item(TransportationData.TRNSPRTTN_ID) = intMax
                .Item(TransportationData.CSTMR_ID) = bv_i64CustomerId
                .Item(TransportationData.RT_ID) = bv_i64RouteId
                .Item(TransportationData.TRNSPRTR_ID) = bv_i64TransporterId
                .Item(TransportationData.NO_OF_TRPS) = bv_intNoOfTrip
                If bv_i64StatusId <> 0 Then
                    .Item(TransportationData.TRNSPRTTN_STTS_ID) = bv_i64StatusId
                Else
                    .Item(TransportationData.TRNSPRTTN_STTS_ID) = DBNull.Value
                End If
                If bv_i64ActivityId <> 0 Then
                    .Item(TransportationData.EQPMNT_STT_ID) = bv_i64ActivityId
                Else
                    .Item(TransportationData.EQPMNT_STT_ID) = DBNull.Value
                End If
                If bv_decTripRate <> 0 Then
                    .Item(TransportationData.TRP_RT_NC) = bv_decTripRate
                Else
                    .Item(TransportationData.TRP_RT_NC) = 0
                End If
                If bv_i64ActivityLocationId <> 0 Then
                    .Item(TransportationData.ACTVTY_LCTN_ID) = bv_i64ActivityLocationId
                Else
                    .Item(TransportationData.ACTVTY_LCTN_ID) = DBNull.Value
                End If
                If bv_datRequestDate <> Nothing AndAlso bv_datRequestDate <> sqlDbnull AndAlso bv_datRequestDate <> sqlDateDbnull Then
                    .Item(TransportationData.RQST_DT) = bv_datRequestDate
                Else
                    .Item(TransportationData.RQST_DT) = DBNull.Value
                End If

                If bv_strRemarks <> Nothing Then
                    .Item(TransportationData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(TransportationData.RMRKS_VC) = DBNull.Value
                End If
                .Item(TransportationData.MDFD_BY) = bv_strModifiedBy
                .Item(TransportationData.MDFD_DT) = bv_datModifiedDate
                .Item(TransportationData.CRTD_BY) = bv_strCreatedBy
                .Item(TransportationData.CRTD_DT) = bv_datCreatedDate
                .Item(TransportationData.DPT_ID) = bv_i32DepotID
            End With
            objData.InsertRow(dr, TransportationInsertQuery, br_objTransaction)
            dr = Nothing
            CreateTransportation = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTransportation() TABLE NAME:Transportation"

    Public Function UpdateTransportation(ByVal bv_i64TransportationID As Int64, _
                                         ByVal bv_i64CustomerId As Int64, _
                                         ByVal bv_i64RouteId As Int64, _
                                         ByVal bv_i64TransporterId As Int64, _
                                         ByVal bv_i64ActivityLocationId As Int64, _
                                         ByVal bv_i64ActivityId As Int64, _
                                         ByVal bv_i64StatusId As Int64, _
                                         ByVal bv_datRequestDate As DateTime, _
                                         ByVal bv_decTripRate As Decimal, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_intNoOfTrip As Integer, _
                                         ByVal bv_strModifiedBy As String, _
                                         ByVal bv_datModifiedDate As DateTime, _
                                         ByVal bv_i32DepotID As Int32, _
                                         ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TransportationData._TRANSPORTATION).NewRow()
            With dr
                .Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationID
                .Item(TransportationData.CSTMR_ID) = bv_i64CustomerId
                .Item(TransportationData.RT_ID) = bv_i64RouteId
                .Item(TransportationData.TRNSPRTR_ID) = bv_i64TransporterId
                .Item(TransportationData.NO_OF_TRPS) = bv_intNoOfTrip
                If bv_i64StatusId <> 0 Then
                    .Item(TransportationData.TRNSPRTTN_STTS_ID) = bv_i64StatusId
                Else
                    .Item(TransportationData.TRNSPRTTN_STTS_ID) = DBNull.Value
                End If
                If bv_i64ActivityId <> 0 Then
                    .Item(TransportationData.EQPMNT_STT_ID) = bv_i64ActivityId
                Else
                    .Item(TransportationData.EQPMNT_STT_ID) = DBNull.Value
                End If
                If bv_decTripRate <> 0 Then
                    .Item(TransportationData.TRP_RT_NC) = bv_decTripRate
                Else
                    .Item(TransportationData.TRP_RT_NC) = 0
                End If
                .Item(TransportationData.RQST_DT) = bv_datRequestDate
                If bv_strRemarks <> Nothing Then
                    .Item(TransportationData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(TransportationData.RMRKS_VC) = DBNull.Value
                End If
                .Item(TransportationData.MDFD_BY) = bv_strModifiedBy
                .Item(TransportationData.MDFD_DT) = bv_datModifiedDate
                .Item(TransportationData.DPT_ID) = bv_i32DepotID
            End With
            UpdateTransportation = objData.UpdateRow(dr, TransportationUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateTransportation() TABLE NAME:Transportation"

    Public Function UpdateTransportationCancel(ByVal bv_i64TransportationID As Int64, _
                                               ByVal bv_strRequestNo As String, _
                                               ByVal bv_i64TransportationStatusId As Int64, _
                                               ByVal bv_strCancelleddBy As String, _
                                               ByVal bv_datCancelledDate As DateTime, _
                                               ByVal bv_i32DepotID As Int32, _
                                               ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TransportationData._TRANSPORTATION).NewRow()
            With dr
                .Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationID
                If bv_i64TransportationStatusId <> 0 Then
                    .Item(TransportationData.TRNSPRTTN_STTS_ID) = bv_i64TransportationStatusId
                Else
                    .Item(TransportationData.TRNSPRTTN_STTS_ID) = DBNull.Value
                End If

                .Item(TransportationData.RQST_NO) = bv_strRequestNo
                .Item(TransportationData.CNCLD_BY) = bv_strCancelleddBy
                .Item(TransportationData.CNCLD_DT) = bv_datCancelledDate
                .Item(TransportationData.DPT_ID) = bv_i32DepotID
            End With
            UpdateTransportationCancel = objData.UpdateRow(dr, TransportationUpdateCancelQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateTransportationDetail() Table Name: Transportation_Detail"
    Public Function CreateTransportationDetail(ByVal bv_i64TransportationID As Int64, _
                                               ByVal bv_strEquipmentNo As String, _
                                               ByVal bv_i64EquipmentTypeId As Int64, _
                                               ByVal bv_i64EmptySingleId As Int64, _
                                               ByVal bv_strCustomerRefNo As String, _
                                               ByVal bv_i64ProductId As Int64, _
                                               ByVal bv_decTotalRate As Decimal, _
                                               ByVal bv_decTripRate As Decimal, _
                                               ByVal bv_datJobStartDate As DateTime, _
                                               ByVal bv_datJobEndDate As DateTime, _
                                               ByVal bv_strRemarks As String, _
                                               ByVal bv_decSupplierRate As Decimal, _
                                               ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TransportationData._TRANSPORTATION_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TransportationData._TRANSPORTATION_DETAIL, br_objTransaction)
                .Item(TransportationData.TRNSPRTTN_DTL_ID) = intMax
                .Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationID
                .Item(TransportationData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(TransportationData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                If bv_i64EmptySingleId <> 0 Then
                    .Item(TransportationData.EMPTY_SNGL_ID) = bv_i64EmptySingleId
                Else
                    .Item(TransportationData.EMPTY_SNGL_ID) = DBNull.Value
                End If

                If bv_strCustomerRefNo <> Nothing Then
                    .Item(TransportationData.CSTMR_RF_NO) = bv_strCustomerRefNo
                Else
                    .Item(TransportationData.CSTMR_RF_NO) = DBNull.Value
                End If
                If bv_i64ProductId <> 0 Then
                    .Item(TransportationData.PRDCT_ID) = bv_i64ProductId
                Else
                    .Item(TransportationData.PRDCT_ID) = DBNull.Value
                End If
                If bv_decTotalRate <> 0 Then
                    .Item(TransportationData.TTL_RT_NC) = bv_decTotalRate
                Else
                    .Item(TransportationData.TTL_RT_NC) = 0
                End If
                If bv_decTripRate <> 0 Then
                    .Item(TransportationData.TRP_RT_NC) = bv_decTripRate
                Else
                    .Item(TransportationData.TRP_RT_NC) = 0
                End If
                If bv_datJobStartDate <> Nothing AndAlso bv_datJobStartDate <> sqlDbnull AndAlso bv_datJobStartDate <> sqlDateDbnull Then
                    .Item(TransportationData.JB_STRT_DT) = bv_datJobStartDate
                Else
                    .Item(TransportationData.JB_STRT_DT) = DBNull.Value
                End If
                If bv_datJobEndDate <> Nothing AndAlso bv_datJobEndDate <> sqlDbnull AndAlso bv_datJobEndDate <> sqlDateDbnull Then
                    .Item(TransportationData.JB_END_DT) = bv_datJobEndDate
                Else
                    .Item(TransportationData.JB_END_DT) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(TransportationData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(TransportationData.RMRKS_VC) = DBNull.Value
                End If

                'Supplier Rate
                If bv_decSupplierRate <> Nothing Then
                    .Item(TransportationData.SPPLR_RT_NC) = bv_decSupplierRate
                Else
                    .Item(TransportationData.SPPLR_RT_NC) = 0D
                End If

            End With
            objData.InsertRow(dr, Transportation_DetailInsertQuery, br_objTransaction)
            dr = Nothing
            CreateTransportationDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTransportationDetail() TABLE NAME:Transportation_Detail"

    Public Function UpdateTransportationDetail(ByVal bv_i64TransportationDetailID As Int64, _
                                               ByVal bv_i64TransportationID As Int64, _
                                               ByVal bv_strEquipmentNo As String, _
                                               ByVal bv_i64EquipmentTypeId As Int64, _
                                               ByVal bv_i64EmptySingleId As Int64, _
                                               ByVal bv_strCustomerRefNo As String, _
                                               ByVal bv_i64ProductId As Int64, _
                                               ByVal bv_decTotalRate As Decimal, _
                                               ByVal bv_decTripRate As Decimal, _
                                               ByVal bv_datJobStartDate As DateTime, _
                                               ByVal bv_datJobEndDate As DateTime, _
                                               ByVal bv_strRemarks As String, _
                                                ByVal bv_decSupplierRate As Decimal, _
                                               ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TransportationData._TRANSPORTATION_DETAIL).NewRow()
            With dr
                .Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_i64TransportationDetailID
                .Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationID
                .Item(TransportationData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(TransportationData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                If bv_i64EmptySingleId <> 0 Then
                    .Item(TransportationData.EMPTY_SNGL_ID) = bv_i64EmptySingleId
                Else
                    .Item(TransportationData.EMPTY_SNGL_ID) = DBNull.Value
                End If

                If bv_strCustomerRefNo <> Nothing Then
                    .Item(TransportationData.CSTMR_RF_NO) = bv_strCustomerRefNo
                Else
                    .Item(TransportationData.CSTMR_RF_NO) = DBNull.Value
                End If
                If bv_i64ProductId <> 0 Then
                    .Item(TransportationData.PRDCT_ID) = bv_i64ProductId
                Else
                    .Item(TransportationData.PRDCT_ID) = DBNull.Value
                End If
                If bv_decTotalRate <> 0 Then
                    .Item(TransportationData.TTL_RT_NC) = bv_decTotalRate
                Else
                    .Item(TransportationData.TTL_RT_NC) = 0
                End If
                If bv_decTripRate <> 0 Then
                    .Item(TransportationData.TRP_RT_NC) = bv_decTripRate
                Else
                    .Item(TransportationData.TRP_RT_NC) = 0
                End If
                If bv_datJobStartDate <> Nothing AndAlso bv_datJobStartDate <> sqlDbnull AndAlso bv_datJobStartDate <> sqlDateDbnull Then
                    .Item(TransportationData.JB_STRT_DT) = bv_datJobStartDate
                Else
                    .Item(TransportationData.JB_STRT_DT) = DBNull.Value
                End If
                If bv_datJobEndDate <> Nothing AndAlso bv_datJobEndDate <> sqlDbnull AndAlso bv_datJobEndDate <> sqlDateDbnull Then
                    .Item(TransportationData.JB_END_DT) = bv_datJobEndDate
                Else
                    .Item(TransportationData.JB_END_DT) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(TransportationData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(TransportationData.RMRKS_VC) = DBNull.Value
                End If

                'Supplier Rate
                If bv_decSupplierRate <> Nothing Then
                    .Item(TransportationData.SPPLR_RT_NC) = bv_decSupplierRate
                Else
                    .Item(TransportationData.SPPLR_RT_NC) = 0D
                End If

            End With
            UpdateTransportationDetail = objData.UpdateRow(dr, Transportation_DetailUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteTransportationDetail() TABLE NAME:Transportation_Detail"

    Public Function DeleteTransportationDetail(ByVal bv_intTransporationID As Int64, _
                                               ByVal bv_intTransportationDetailID As Int64, _
                                               ByRef br_objTransaction As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TransportationData._TRANSPORTATION_DETAIL).NewRow()
            With dr
                .Item(TransportationData.TRNSPRTTN_ID) = bv_intTransporationID
                .Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_intTransportationDetailID
            End With
            DeleteTransportationDetail = objData.DeleteRow(dr, Transportation_DetailDeleteQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateTransportationDetailRate() TABLE NAME:Transportation_Detail_Rate"

    Public Function CreateTransportationDetailRate(ByVal bv_i64TransportationID As Int64, _
                                                   ByVal bv_i64TransportationDetailID As Int64, _
                                                   ByVal bv_i64AdditionalChargeRateId As Int64, _
                                                   ByVal bv_dblAdditionalChargeRate As Decimal, _
                                                   ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TransportationData._TRANSPORTATION_DETAIL_RATE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TransportationData._TRANSPORTATION_DETAIL_RATE, br_objTransaction)
                .Item(TransportationData.TRNSPRTTN_DTL_RT_ID) = intMax
                .Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_i64TransportationDetailID
                .Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationID
                .Item(TransportationData.ADDTNL_CHRG_RT_ID) = bv_i64AdditionalChargeRateId
                If bv_dblAdditionalChargeRate <> 0 Then
                    .Item(TransportationData.ADDTNL_CHRG_RT_NC) = bv_dblAdditionalChargeRate
                Else
                    .Item(TransportationData.ADDTNL_CHRG_RT_NC) = 0
                End If

            End With
            objData.InsertRow(dr, Transportation_Detail_RateInsertQuery, br_objTransaction)
            dr = Nothing
            CreateTransportationDetailRate = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTransportationDetailRate() TABLE NAME:Transportation_Detail_Rate"

    Public Function UpdateTransportationDetailRate(ByVal bv_i64TransportationDetailRateID As Int64, _
                                                   ByVal bv_i64TransportationDetailID As Int64, _
                                                   ByVal bv_i64TransportationID As Int64, _
                                                   ByVal bv_i64AdditionalChargeRateId As Int64, _
                                                   ByVal bv_dblAdditionalChargeRate As Double, _
                                                   ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TransportationData._TRANSPORTATION_DETAIL_RATE).NewRow()
            With dr
                .Item(TransportationData.TRNSPRTTN_DTL_RT_ID) = bv_i64TransportationDetailRateID
                .Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_i64TransportationDetailID
                .Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationID
                .Item(TransportationData.ADDTNL_CHRG_RT_ID) = bv_i64AdditionalChargeRateId
                If bv_dblAdditionalChargeRate <> 0 Then
                    .Item(TransportationData.ADDTNL_CHRG_RT_NC) = bv_dblAdditionalChargeRate
                Else
                    .Item(TransportationData.ADDTNL_CHRG_RT_NC) = 0
                End If
            End With
            UpdateTransportationDetailRate = objData.UpdateRow(dr, Transportation_Detail_RateUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    '#Region "DeleteTransportationDetailRate() TABLE NAME:Transportation_Detail_Rate"

    '    Public Function DeleteTransportationDetailRate(ByVal bv_intTransporationID As Int64, _
    '                                                   ByVal bv_dblTransportationDetailRateID As Int64, _
    '                                                   ByRef br_objTransaction As Transactions) As Boolean

    '        Dim dr As DataRow
    '        objData = New DataObjects()
    '        Try
    '            dr = ds.Tables(TransportationData._TRANSPORTATION_DETAIL_RATE).NewRow()
    '            With dr
    '                .Item(TransportationData.TRNSPRTTN_ID) = bv_intTransporationID
    '                .Item(TransportationData.TRNSPRTTN_DTL_RT_ID) = bv_dblTransportationDetailRateID
    '            End With
    '            DeleteTransportationDetailRate = objData.DeleteRow(dr, Transportation_Detail_RateDeleteQuery, br_objTransaction)
    '            dr = Nothing
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function
    '#End Region

#Region "DeleteTransportationDetailRateByDetailId() TABLE NAME:Transportation_Detail_Rate"

    Public Function DeleteTransportationDetailRateByDetailId(ByVal bv_intTransporationID As Int64, _
                                                             ByVal bv_intTransportationDetailID As Int64, _
                                                             ByRef br_objTransaction As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TransportationData._TRANSPORTATION_DETAIL_RATE).NewRow()
            With dr
                .Item(TransportationData.TRNSPRTTN_ID) = bv_intTransporationID
                .Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_intTransportationDetailID
            End With
            DeleteTransportationDetailRateByDetailId = objData.DeleteRow(dr, Transportation_Detail_RateDeleteQueryByDetailRateId, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteTransportationDetailRateByTransportationId() TABLE NAME:Transportation_Detail_Rate"

    Public Function DeleteTransportationDetailRateByTransportationId(ByVal bv_dblTransportationID As Int64, _
                                                                     ByVal bv_dblTransportationDetailID As Int64, _
                                                                     ByRef br_objTransaction As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TransportationData._TRANSPORTATION_DETAIL_RATE).NewRow()
            With dr
                .Item(TransportationData.TRNSPRTTN_ID) = bv_dblTransportationID
                .Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_dblTransportationDetailID
            End With
            DeleteTransportationDetailRateByTransportationId = objData.DeleteRow(dr, Transportation_Detail_RateDeleteQueryByTransportationId, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateTransportationCharge() TABLE NAME:Transportation_Charge"

    Public Function CreateTransportationCharge(ByVal bv_i64CustomerId As Int64, _
                                               ByVal bv_i64TransportationId As Int64, _
                                               ByVal bv_i64TransportationDetailId As Int64, _
                                               ByVal bv_datInvoiceDate As DateTime, _
                                               ByVal bv_i64RouteId As Int64, _
                                               ByVal bv_i64TransporterId As Int64, _
                                               ByVal bv_strRequestNo As String, _
                                               ByVal bv_strEquipmentNo As String, _
                                               ByVal bv_i64EquipmentTypeId As Int64, _
                                               ByVal bv_i64EmptySingleId As Int64, _
                                               ByVal bv_strCustomerReferenceNo As String, _
                                               ByVal bv_datEventDate As DateTime, _
                                               ByVal bv_dblTotalRate As Decimal, _
                                               ByVal bv_decTripRate As Decimal, _
                                               ByVal bv_i32DepotID As Int32, _
                                               ByVal bv_blnBillingFlag As String, _
                                               ByVal bv_strDraftInvoiceNo As String, _
                                               ByVal bv_strFinalInvoiceNo As String, _
                                               ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TransportationData._TRANSPORTATION_CHARGE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TransportationData._TRANSPORTATION_CHARGE, br_objTransaction)
                .Item(TransportationData.TRNSPRTTN_CHRG_ID) = intMax
                .Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationId
                .Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_i64TransportationDetailId
                .Item(TransportationData.CSTMR_ID) = bv_i64CustomerId
                .Item(TransportationData.RT_ID) = bv_i64RouteId
                .Item(TransportationData.TRNSPRTR_ID) = bv_i64TransporterId
                .Item(TransportationData.RQST_NO) = bv_strRequestNo
                .Item(TransportationData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(TransportationData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                If bv_i64EmptySingleId <> 0 Then
                    .Item(TransportationData.EMPTY_SNGL_ID) = bv_i64EmptySingleId
                Else
                    .Item(TransportationData.EMPTY_SNGL_ID) = DBNull.Value
                End If
                If bv_decTripRate <> 0 Then
                    .Item(TransportationData.TRP_RT_NC) = bv_decTripRate
                Else
                    .Item(TransportationData.TRP_RT_NC) = 0
                End If
                If bv_strCustomerReferenceNo <> Nothing Then
                    .Item(TransportationData.CSTMR_RF_NO) = bv_strCustomerReferenceNo
                Else
                    .Item(TransportationData.CSTMR_RF_NO) = DBNull.Value
                End If
                If bv_datInvoiceDate <> Nothing AndAlso bv_datInvoiceDate <> sqlDbnull AndAlso bv_datInvoiceDate <> sqlDateDbnull Then
                    .Item(TransportationData.INVC_DT) = bv_datInvoiceDate
                Else
                    .Item(TransportationData.INVC_DT) = DBNull.Value
                End If
                If bv_datEventDate <> Nothing AndAlso bv_datEventDate <> sqlDbnull AndAlso bv_datEventDate <> sqlDateDbnull Then
                    .Item(TransportationData.EVNT_DT) = bv_datEventDate
                Else
                    .Item(TransportationData.EVNT_DT) = DBNull.Value
                End If
                .Item(TransportationData.TTL_RT_NC) = bv_dblTotalRate
                .Item(TransportationData.DPT_ID) = bv_i32DepotID
                .Item(TransportationData.BLLNG_FLG) = bv_blnBillingFlag
                If bv_strDraftInvoiceNo <> Nothing Then
                    .Item(TransportationData.DRFT_INVC_NO) = bv_strDraftInvoiceNo
                Else
                    .Item(TransportationData.DRFT_INVC_NO) = DBNull.Value
                End If
                If bv_strFinalInvoiceNo <> Nothing Then
                    .Item(TransportationData.FNL_INVC_NO) = bv_strFinalInvoiceNo
                Else
                    .Item(TransportationData.FNL_INVC_NO) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Transportation_ChargeInsertQuery, br_objTransaction)
            dr = Nothing
            CreateTransportationCharge = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTransportationCharge() TABLE NAME:Transportation_Charge"

    Public Function UpdateTransportationCharge(ByVal bv_i64TransportationChargeId As Int64, _
                                               ByVal bv_i64TransportationId As Int64, _
                                               ByVal bv_i64TransportationDetailId As Int64, _
                                               ByVal bv_i64CustomerId As Int64, _
                                               ByVal bv_i64RouteId As Int64, _
                                               ByVal bv_i64TransporterId As Int64, _
                                               ByVal bv_strRequestNo As String, _
                                               ByVal bv_strEquipmentNo As String, _
                                               ByVal bv_i64EquipmentTypeId As Int64, _
                                               ByVal bv_i64EmptySingleId As Int64, _
                                               ByVal bv_strCustomerReferenceNo As String, _
                                               ByVal bv_datEventDate As DateTime, _
                                               ByVal bv_dblTotalRate As Decimal, _
                                               ByVal bv_decTripRate As Decimal, _
                                               ByVal bv_i32DepotID As Int32, _
                                               ByVal bv_strDraftInvoiceNo As String, _
                                               ByVal bv_strFinalInvoiceNo As String, _
                                               ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TransportationData._TRANSPORTATION_CHARGE).NewRow()
            With dr
                .Item(TransportationData.TRNSPRTTN_CHRG_ID) = bv_i64TransportationChargeId
                .Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationId
                .Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_i64TransportationDetailId
                .Item(TransportationData.CSTMR_ID) = bv_i64CustomerId
                .Item(TransportationData.RT_ID) = bv_i64RouteId
                .Item(TransportationData.TRNSPRTR_ID) = bv_i64TransporterId
                .Item(TransportationData.RQST_NO) = bv_strRequestNo
                .Item(TransportationData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(TransportationData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                If bv_i64EmptySingleId <> 0 Then
                    .Item(TransportationData.EMPTY_SNGL_ID) = bv_i64EmptySingleId
                Else
                    .Item(TransportationData.EMPTY_SNGL_ID) = DBNull.Value
                End If
                If bv_decTripRate <> 0 Then
                    .Item(TransportationData.TRP_RT_NC) = bv_decTripRate
                Else
                    .Item(TransportationData.TRP_RT_NC) = 0
                End If
                If bv_strCustomerReferenceNo <> Nothing Then
                    .Item(TransportationData.CSTMR_RF_NO) = bv_strCustomerReferenceNo
                Else
                    .Item(TransportationData.CSTMR_RF_NO) = DBNull.Value
                End If
                If bv_datEventDate <> Nothing AndAlso bv_datEventDate <> sqlDbnull AndAlso bv_datEventDate <> sqlDateDbnull Then
                    .Item(TransportationData.EVNT_DT) = bv_datEventDate
                Else
                    .Item(TransportationData.EVNT_DT) = DBNull.Value
                End If
                .Item(TransportationData.TTL_RT_NC) = bv_dblTotalRate
                .Item(TransportationData.DPT_ID) = bv_i32DepotID
                If bv_strDraftInvoiceNo <> Nothing Then
                    .Item(TransportationData.DRFT_INVC_NO) = bv_strDraftInvoiceNo
                Else
                    .Item(TransportationData.DRFT_INVC_NO) = DBNull.Value
                End If
                If bv_strFinalInvoiceNo <> Nothing Then
                    .Item(TransportationData.FNL_INVC_NO) = bv_strFinalInvoiceNo
                Else
                    .Item(TransportationData.FNL_INVC_NO) = DBNull.Value
                End If
            End With
            UpdateTransportationCharge = objData.UpdateRow(dr, Transportation_ChargeUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteTransportationCharge() TABLE NAME:Transportation_Charge"

    Public Function DeleteTransportationChargeByTransportationId(ByVal bv_i64TransportationID As Int64, _
                                                                 ByRef br_objTransaction As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TransportationData._TRANSPORTATION_CHARGE).NewRow()
            With dr
                .Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationID
            End With
            DeleteTransportationChargeByTransportationId = objData.DeleteRow(dr, Transportation_ChargeByTransportationId, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteTransportationCharge() TABLE NAME:Transportation_Charge"

    Public Function DeleteTransportationChargeByTransportationDetailId(ByVal bv_i64TransportationID As Int64, _
                                                                       ByVal bv_i64TransportationDetailID As Int64, _
                                                                       ByRef br_objTransaction As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TransportationData._TRANSPORTATION_CHARGE).NewRow()
            With dr
                .Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationID
                .Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_i64TransportationDetailID
            End With
            DeleteTransportationChargeByTransportationDetailId = objData.DeleteRow(dr, Transportation_ChargeByTransportationDetailId, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTransportationChargeByEqpmntNo() TABLE NAME:TRANSPORTATION_CHARGE"

    Public Function GetTransportationChargeByEqpmntNo(ByVal bv_strRequestNo As String, _
                                                       ByVal bv_i64TransportationId As Int64, _
                                                       ByVal bv_i64TransportationDetailId As Int64, _
                                                       ByVal bv_intDepotId As Int32, _
                                                       ByRef br_objTransaction As Transactions) As DataTable
        Try
            Dim dtTransportationCharge As New DataTable
            Dim hshparameters As New Hashtable
            hshparameters.Add(TransportationData.RQST_NO, bv_strRequestNo)
            hshparameters.Add(TransportationData.TRNSPRTTN_ID, bv_i64TransportationId)
            hshparameters.Add(TransportationData.TRNSPRTTN_DTL_ID, bv_i64TransportationDetailId)
            hshparameters.Add(TransportationData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(TRANSPORTATION_CHARGESelectQueryByEqpmntNo, hshparameters)
            objData.Fill(dtTransportationCharge, br_objTransaction)
            Return dtTransportationCharge
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetTransportationChargeByRequestNo() TABLE NAME:TRANSPORTATION_CHARGE"

    Public Function GetTransportationChargeByRequestNo(ByVal bv_strRequestNo As String, _
                                                       ByVal bv_intDepotId As Int32, _
                                                       ByRef br_objTransaction As Transactions) As TransportationDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(TransportationData.RQST_NO, bv_strRequestNo)
            hshparameters.Add(TransportationData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(TRANSPORTATION_CHARGESelectQueryByRequestNo, hshparameters)
            objData.Fill(CType(ds, DataSet), TransportationData._TRANSPORTATION_CHARGE, br_objTransaction)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetTransportationDetailRateByTransportationDetailId()  Table Name: Transportation_Detail_Rate"

    Public Function GetTransportationDetailRateByTransportationDetailId(ByVal bv_i64TransportationDetailId As Int64) As TransportationDataSet
        Try
            Dim dsTransportationDetailRate As New TransportationDataSet
            objData = New DataObjects(V_Transportation_Detail_RateSelectQuery, TransportationData.TRNSPRTTN_DTL_ID, bv_i64TransportationDetailId)
            objData.Fill(CType(dsTransportationDetailRate, DataSet), TransportationData._V_TRANSPORTATION_DETAIL_RATE)
            Return dsTransportationDetailRate
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEnumCodeByEnumId() TABLE NAME: Enum"

    Public Function GetEnumCodeByEnumId(ByVal bv_i64EnumId As Int64) As DataTable
        Try
            Dim dtEnum As New DataTable
            objData = New DataObjects(EnumCodeSelectQuery, TransportationData.ENM_ID, bv_i64EnumId)
            objData.Fill(CType(dtEnum, DataTable))
            Return dtEnum
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetTransportationDetailRateByDetailId() TABLE NAME: TRANSPORTATION_DETAIL_RATE"

    Public Function GetTransportationDetailRateByDetailId(ByVal bv_i64TransportationDetailId As Int64, _
                                                          ByVal bv_i64TransportationId As Int64, _
                                                          ByRef br_objTransaction As Transactions) As Integer
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(TransportationData.TRNSPRTTN_DTL_ID, bv_i64TransportationDetailId)
            hshparameters.Add(TransportationData.TRNSPRTTN_ID, bv_i64TransportationId)
            objData = New DataObjects(TransportationDetailRateSelectQueryByDetailId, hshparameters)
            Return objData.ExecuteScalar(br_objTransaction)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCustomertransportationByCustomerId() TABLE NAME:CUSTOMER_TRANSPORTATION"

    Public Function GetCustomerTransportationByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                          ByVal bv_i64RouteId As Int64, _
                                                          ByVal bv_i64EquipmentStatusCode As String) As TransportationDataSet
        Try
            Dim hshparameter As New Hashtable
            hshparameter.Add(TransportationData.CSTMR_ID, bv_i64CustomerId)
            hshparameter.Add(TransportationData.RT_ID, bv_i64RouteId)
            objData = New DataObjects(V_Transportation_CustomerSelectQuery, hshparameter)
            objData.Fill(CType(ds, DataSet), TransportationData._V_CUSTOMER_TRANSPORTATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRouteByRouteId() TABLE NAME:Route"

    Public Function GetRouteByRouteId(ByVal bv_i64RouteId As Int64, _
                                      ByVal bv_intDepotId As Int32) As TransportationDataSet
        Try
            Dim hshparameter As New Hashtable
            hshparameter.Add(TransportationData.RT_ID, bv_i64RouteId)
            hshparameter.Add(TransportationData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(V_RouteSelectQueryByRouteId, hshparameter)
            objData.Fill(CType(ds, DataSet), TransportationData._V_ROUTE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTripRateByRouteId() TABLE NAME:Transporter_Route_Detail"

    Public Function GetTripRateByRouteId(ByVal bv_i64RouteId As Int64, _
                                         ByVal bv_i64TransporterId As Int64, _
                                         ByVal bv_intDepotId As Int32) As TransportationDataSet
        Try
            Dim hshparameter As New Hashtable
            hshparameter.Add(TransportationData.RT_ID, bv_i64RouteId)
            hshparameter.Add(TransportationData.TRNSPRTR_ID, bv_i64TransporterId)
            hshparameter.Add(TransportationData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(V_TransporterRouteDetailSelectQuery, hshparameter)
            objData.Fill(CType(ds, DataSet), TransportationData._V_TRANSPORTER_ROUTE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTransporterByRouteId() TABLE NAME:Transporter_Route_Detail"

    Public Function GetTransporterByRouteId(ByVal bv_i64RouteId As Int64, _
                                            ByVal bv_intDepotId As Int32) As TransportationDataSet
        Try
            Dim hshparameter As New Hashtable
            hshparameter.Add(TransportationData.RT_ID, bv_i64RouteId)
            hshparameter.Add(TransportationData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(V_TransporterRouteDetailSelectQueryByRouteId, hshparameter)
            objData.Fill(CType(ds, DataSet), TransportationData._V_TRANSPORTER_ROUTE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Supplier Rate"

    Public Function GetSupplier_FullTripRateByTransporterId(ByVal bv_i64TransporterId As Int64, ByVal bv_i64RouteId As Int64) As Decimal
        Try
            Dim hshparameter As New Hashtable
            hshparameter.Add(TransportationData.TRNSPRTR_ID, bv_i64TransporterId)
            hshparameter.Add(TransportationData.RT_ID, bv_i64RouteId)

            objData = New DataObjects(SupplierFullTripRate_SelectQry, hshparameter)

            Return CDec(objData.ExecuteScalar())

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetSupplier_EmptyTripRateByTransporterId(ByVal bv_i64TransporterId As Int64, ByVal bv_i64RouteId As Int64) As Decimal
        Try
            Dim hshparameter As New Hashtable
            hshparameter.Add(TransportationData.TRNSPRTR_ID, bv_i64TransporterId)
            hshparameter.Add(TransportationData.RT_ID, bv_i64RouteId)

            objData = New DataObjects(SupplierEmptyTripRate_SelectQry, hshparameter)

            Return CDec(objData.ExecuteScalar())

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


End Class
