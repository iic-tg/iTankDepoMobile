Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class Enquirys

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_PRODUCTSelectQueryByDepot As String = "SELECT PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CHMCL_NM,IMO_CLSS,UN_NO,CLSSFCTN_ID,PRODUCT_CLASSIFICATION,GRP_CLSSFCTN_ID,GROUP_CLASSIFICATION,RMRKS_VC,CLNNG_TTL_AMNT_NC,CLNBL_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,CASE WHEN CLNBL_BT=1 THEN 'Y' ELSE 'N' END CLNBL,(SELECT DPT_CD FROM DEPOT WHERE DPT_ID=V.DPT_ID) DPT_CD FROM V_PRODUCT V WHERE DPT_ID=@DPT_ID AND ACTV_BT=1"
    Private Const V_CUSTOMERSelectQueryByDepotId As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,CSTMR_CRRNCY_CD,BLLNG_TYP_ID,BLLNG_TYP_CD,CNTCT_PRSN_NAM,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,RPR_TCH_EML_ID,BLK_EML_FRMT_ID,BLK_EML_FRMT_CD,HYDR_AMNT_NC,PNMTC_AMNT_NC,LBR_RT_PR_HR_NC,LK_TST_RT_NC,SRVY_ONHR_OFFHR_RT_NC,PRDC_TST_TYP_ID,PRDC_TST_TYP_CD,VLDTY_PRD_TST_YRS,MIN_HTNG_RT_NC,MIN_HTNG_PRD_NC,HRLY_CHRG_NC,CHK_DGT_VLDTN_BT,TRNSPRTTN_BT,RNTL_BT,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,DPT_CD FROM V_CUSTOMER WHERE DPT_ID=@DPT_ID AND ACTV_BT=1"
    Private Const V_ENQUIRY_PRODUCT_CLEANING_RATESelectQueryBy As String = "SELECT PRDCT_ID,CLNNG_TTL_AMNT_NC,PRDCT_DSCRPTN_VC,CLNNG_AMNT_NC,SCRBBNG_AMNT_NC,HTNG_AMNT_NC,HNDLNG_AMNT_NC,DSPSNG_AMNT_NC,OTHR_AMNT_NC,DPT_ID,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL BD WHERE CRRNCY_ID=BD.CRRNCY_ID AND BNK_TYP_ID=44 AND DPT_ID=@DPT_ID))DPT_LCL_CRRNCY FROM V_ENQUIRY_PRODUCT_CLEANING_RATE WHERE DPT_ID=@DPT_ID"
    Private Const V_ENQUIRY_CUSTOMER_CLEANING_RATESelectQueryBy As String = "SELECT PRDCT_ID,CSTMR_ID,TTL_AMNT_NC,CSTMR_CRRNCY,PRDCT_CSTMR_CD,PRDCT_CSTMR_NAM,PRDCT_DSCRPTN_VC,CLNNG_AMNT_NC,SCRBBNG_AMNT_NC,HTNG_AMNT_NC,HNDLNG_AMNT_NC,DSPSNG_AMNT_NC,OTHR_AMNT_NC,DPT_ID FROM V_ENQUIRY_CUSTOMER_CLEANING_RATE WHERE DPT_ID=@DPT_ID"
    Private Const V_ENQUIRY_CUSTOMER_CHARGESelectQuery As String = "SELECT CSTMR_ID,EQPMNT_CD_ID,EQPMNT_TYP_ID,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,UP_TO_DYS,STRG_CHRG_NC,EQPMNT_CD_CD,EQPMNT_TYP_CD,DPT_ID FROM V_ENQUIRY_CUSTOMER_CHARGE WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const V_CUSTOMERSelectQuery As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,CSTMR_CRRNCY_CD,BLLNG_TYP_ID,BLLNG_TYP_CD,CNTCT_PRSN_NAM,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,RPR_TCH_EML_ID,BLK_EML_FRMT_ID,BLK_EML_FRMT_CD,HYDR_AMNT_NC,PNMTC_AMNT_NC,LBR_RT_PR_HR_NC,LK_TST_RT_NC,SRVY_ONHR_OFFHR_RT_NC,PRDC_TST_TYP_ID,PRDC_TST_TYP_CD,VLDTY_PRD_TST_YRS,MIN_HTNG_RT_NC,MIN_HTNG_PRD_NC,HRLY_CHRG_NC,CHK_DGT_VLDTN_BT,TRNSPRTTN_BT,RNTL_BT,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,DPT_CD,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=@DPT_ID AND BNK_TYP_ID=44)) AS DPT_CRRNCY_CD FROM V_CUSTOMER WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const RentalCustomerSelectQuery As String = "SELECT CSTMR_RNTL_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CNTRCT_RFRNC_NO,CNTRCT_STRT_DT,CNTRCT_END_DT,RNTL_PR_DY,HNDLNG_OT,HNDLNG_IN,ON_HR_SRVY,OFF_HR_SRVY,RMRKS_VC,MN_TNR_DY FROM V_CUSTOMER_RENTAL WHERE CSTMR_ID=@CSTMR_ID"
    Private Const CustomerTransportationQuery As String = "SELECT CSTMR_TRNSPRTTN_ID,CSTMR_ID,CSTMR_CD,CSTMR_CRRNCY_CD,RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_CD,PCK_UP_LCTN_DSCRPTN_VC,DRP_OFF_LCTN_CD,DRP_OFF_LCTN_DSCRPTN_VC,ACTVTY_CD,ACTVTY_LCTN_CD,ACTVTY_LCTN_DSCRPTN_VC,EMPTY_TRP_RT_NC,FLL_TRP_RT_NC FROM V_CUSTOMER_TRANSPORTATION WHERE CSTMR_ID=@CSTMR_ID"
    Private Const RouteDetailSelectQuery As String = "SELECT RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_ID,PCK_UP_LCTN_CD,DRP_OFF_LCTN_ID,DRP_OFF_LCTN_CD,ACTVTY_ID,ACTVTY_CD,ACTVTY_LCTN_ID,ACTVTY_LCTN_CD,EMPTY_TRP_RT_NC,FLL_TRP_RT_NC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VR.DPT_ID AND BNK_TYP_ID=44)AND ACTV_BT=1)DPT_CRRNCY_CD FROM V_ROUTE VR WHERE ACTV_BT = 1 AND DPT_ID=@DPT_ID"
    Private Const RouteDetailSelectQuerybyRouteID As String = "SELECT RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_ID,PCK_UP_LCTN_CD,(SELECT LCTN_DSCRPTN_VC FROM LOCATION WHERE LCTN_ID=VR.PCK_UP_LCTN_ID) AS PCK_UP_LCTN_DSCRPTN_VC,DRP_OFF_LCTN_ID,DRP_OFF_LCTN_CD,(SELECT LCTN_DSCRPTN_VC FROM LOCATION WHERE LCTN_ID=VR.DRP_OFF_LCTN_ID) AS DRP_OFF_LCTN_DSCRPTN_VC,ACTVTY_ID,ACTVTY_CD,ACTVTY_LCTN_ID,ACTVTY_LCTN_CD,(SELECT LCTN_DSCRPTN_VC FROM LOCATION WHERE LCTN_ID=VR.ACTVTY_LCTN_ID) AS ACTVTY_LCTN_DSCRPTN_VC,EMPTY_TRP_RT_NC,FLL_TRP_RT_NC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VR.DPT_ID AND BNK_TYP_ID=44)AND ACTV_BT=1)DPT_CRRNCY_CD,(SELECT DPT_CD FROM DEPOT WHERE DPT_ID=VR.DPT_ID)DPT_CD FROM V_ROUTE VR WHERE DPT_ID=@DPT_ID AND RT_ID=@RT_ID"
    Private Const CustomerRouteDetailSelectQuerybyRouteID As String = "SELECT CSTMR_TRNSPRTTN_ID,CSTMR_ID,CSTMR_CD,(SELECT CSTMR_NAM FROM CUSTOMER WHERE CSTMR_ID=VCT.CSTMR_ID)CSTMR_NAM,CSTMR_CRRNCY_CD,RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_CD,PCK_UP_LCTN_DSCRPTN_VC,DRP_OFF_LCTN_CD,DRP_OFF_LCTN_DSCRPTN_VC,ACTVTY_CD,ACTVTY_LCTN_CD,ACTVTY_LCTN_DSCRPTN_VC,EMPTY_TRP_RT_NC,FLL_TRP_RT_NC FROM V_CUSTOMER_TRANSPORTATION VCT WHERE RT_ID=@RT_ID"
    Private Const V_TRANSPORTER_ROUTE_DETAILSelectQueryByRouteID As String = "SELECT TRNSPRTR_RT_DTL_ID,TRNSPRTR_ID,TRNSPRTR_CD,TRNSPRTR_DSCRPTN,RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_CD,DRP_OFF_LCTN_CD,EMPTY_TRP_SPPLR_RT_NC,EMPTY_TRP_CSTMR_RT_NC,FLL_TRP_SPPLR_RT_NC,FLL_TRP_CSTMR_RT_NC,DPT_CRRNCY_CD FROM V_TRANSPORTER_ROUTE_DETAIL WHERE RT_ID=@RT_ID AND DPT_ID=@DPT_ID"
    Private Const V_ENQUIRY_Customer_CleaningSlab_SelectQuery As String = "SELECT HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,CSTMR_ID,UP_TO_CNTNRS,CLNNG_RT,RMRKS_VC,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,DPT_ID FROM V_ENQUIRY_CUSTOMER_CLEANINGSLAB WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND UP_TO_CNTNRS IS NOT NULL AND CLNNG_RT IS NOT NULL"
    Private ds As EnquiryDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New EnquiryDataSet
    End Sub

#End Region

#Region "GetProductDetails()"

    Public Function GetProductDetails(ByVal bv_DepotIdi64 As Int64) As EnquiryDataSet
        Try
            objData = New DataObjects(V_PRODUCTSelectQueryByDepot, EnquiryData.DPT_ID, (bv_DepotIdi64))
            objData.Fill(CType(ds, DataSet), EnquiryData._V_PRODUCT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetV_CustomerByDepot() TABLE NAME:V_CUSTOMER"

    Public Function GetV_CustomerByDepot(ByVal bv_i64DepotId As Int64) As EnquiryDataSet
        Try
            objData = New DataObjects(V_CUSTOMERSelectQueryByDepotId, EnquiryData.DPT_ID, CStr(bv_i64DepotId))
            objData.Fill(CType(ds, DataSet), EnquiryData._V_CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_Enquiry_Product_Cleaning_Rate() TABLE NAME:V_ENQUIRY_PRODUCT_CLEANING_RATE"

    Public Function GetV_Enquiry_Product_Cleaning_Rate(ByVal intDepotID As Int64) As EnquiryDataSet
        Try
            objData = New DataObjects(V_ENQUIRY_PRODUCT_CLEANING_RATESelectQueryBy, EnquiryData.DPT_ID, intDepotID)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_ENQUIRY_PRODUCT_CLEANING_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCleaning_RateBy() TABLE NAME:V_ENQUIRY_CUSTOMER_CLEANING_RATE"

    Public Function GetCleaning_RateBy(ByVal intDepotID As Int64) As EnquiryDataSet
        Try
            objData = New DataObjects(V_ENQUIRY_CUSTOMER_CLEANING_RATESelectQueryBy, EnquiryData.DPT_ID, intDepotID)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_ENQUIRY_CUSTOMER_CLEANING_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_Enquiry_Customer_Charge() TABLE NAME:V_ENQUIRY_CUSTOMER_CHARGE"

    Public Function GetV_Enquiry_Customer_Charge(ByVal bv_intCustomerID As Integer, ByVal intDepotID As Int64) As EnquiryDataSet
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(EnquiryData.CSTMR_ID, bv_intCustomerID)
            hshTable.Add(EnquiryData.DPT_ID, intDepotID)
            objData = New DataObjects(V_ENQUIRY_CUSTOMER_CHARGESelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_ENQUIRY_CUSTOMER_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_Enquiry_Customer_CleaningSlab_Charge() TABLE NAME:V_ENQUIRY_CUSTOMER_CHARGE"

    Public Function GetV_Enquiry_Customer_CleaningSlab_Charge(ByVal bv_intCustomerID As Integer, ByVal intDepotID As Int64) As EnquiryDataSet
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(EnquiryData.CSTMR_ID, bv_intCustomerID)
            hshTable.Add(EnquiryData.DPT_ID, intDepotID)
            objData = New DataObjects(V_ENQUIRY_Customer_CleaningSlab_SelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_ENQUIRY_CUSTOMER_CLEANINGSLAB)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVCustomerBy() TABLE NAME:V_CUSTOMER"

    Public Function GetVCustomer(ByVal bv_CustomerId As Integer, ByVal bv_DepotID As Integer) As EnquiryDataSet
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(EnquiryData.CSTMR_ID, bv_CustomerId)
            hshTable.Add(EnquiryData.DPT_ID, bv_DepotID)
            objData = New DataObjects(V_CUSTOMERSelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetRentalCustomerDetails()"

    Public Function pub_GetRentalCustomerDetails(ByVal bv_CustomerId As Integer) As EnquiryDataSet
        Try
            objData = New DataObjects(RentalCustomerSelectQuery, EnquiryData.CSTMR_ID, bv_CustomerId)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_CUSTOMER_RENTAL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetCustomerTransportation()"

    Public Function pub_GetCustomerTransportation(ByVal bv_CustomerId As Integer) As EnquiryDataSet
        Try
            objData = New DataObjects(CustomerTransportationQuery, EnquiryData.CSTMR_ID, bv_CustomerId)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_CUSTOMER_TRANSPORTATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_RouteDetailByDepotId()"

    Public Function pub_RouteDetailByDepotId(ByVal bv_DepotId As Integer) As EnquiryDataSet
        Try
            objData = New DataObjects(RouteDetailSelectQuery, EnquiryData.DPT_ID, bv_DepotId)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_ROUTE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "RouteDetailSelectQuerybyRouteID()"

    Public Function pub_GetRouteByRouteID(ByVal bv_RouteID As Integer, ByVal bv_DepotId As Integer) As EnquiryDataSet
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(EnquiryData.RT_ID, bv_RouteID)
            hshTable.Add(EnquiryData.DPT_ID, bv_DepotId)
            objData = New DataObjects(RouteDetailSelectQuerybyRouteID, hshTable)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_ROUTE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetCustomerRouteDetails()"

    Public Function pub_GetCustomerRouteDetails(ByVal bv_RouteID As Integer, ByVal bv_DepotId As Integer) As EnquiryDataSet
        Try
            objData = New DataObjects(CustomerRouteDetailSelectQuerybyRouteID, EnquiryData.RT_ID, bv_RouteID)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_CUSTOMER_TRANSPORTATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetTransporterRouteDetailByTransporterID() TABLE NAME:TRANSPORTER_ROUTE_DETAIL"

    Public Function GetTransporterRouteDetailByRouteID(ByVal bv_i64RouteID As Int64, _
                                                       ByVal bv_intDepotId As Integer) As EnquiryDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EnquiryData.DPT_ID, bv_intDepotId)
            hshparameters.Add(EnquiryData.RT_ID, bv_i64RouteID)
            objData = New DataObjects(V_TRANSPORTER_ROUTE_DETAILSelectQueryByRouteID, hshparameters)
            objData.Fill(CType(ds, DataSet), EnquiryData._V_TRANSPORTER_ROUTE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class