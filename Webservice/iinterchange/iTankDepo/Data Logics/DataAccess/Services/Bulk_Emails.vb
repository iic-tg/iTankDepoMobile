Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class Bulk_Emails
#Region "Declaration Part.. "
    Dim objData As DataObjects
    Private Const Bulk_EmailSelectQueryByDpt_Id As String = "SELECT BLK_EML_ID,CSTMR_ID,CSTMR_CD,FRM_EML,TO_EML,BCC_EML,SBJCT_VC,BDY_VC,DPT_ID,DPT_CD,SNT_BT,SNT_DT,CRTD_DT,CRTD_BY,BLK_EML_FRMT_ID,BLK_EML_FRMT_CD,CC_EML FROM V_BULK_EMAIL BE WHERE DPT_ID=@DPT_ID AND SNT_BT=0"
    Private Const Bulk_Email_DetailSelectQueryByBulkEmailID As String = "SELECT BLK_EML_DTL_ID,BLK_EML_ID,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,AMNT_NC,CRRNCY_ID,CRRNCY_CD,GI_TRNSCTN_NO,ACTVTY_NO,(SELECT GI_RF_NO FROM ACTIVITY_STATUS WHERE GI_TRNSCTN_NO =BE.GI_TRNSCTN_NO)AS GI_RF_NO,ATTCHMNT_PTH,RSND_BT,SRVC_PRTNR_ID,SRVC_PRTNR_TYP_CD,SRVC_PRTNR_CD FROM V_BULK_EMAIL_DETAIL BE WHERE BLK_EML_ID=@BLK_EML_ID"
    Private Const V_REPAIR_ESTIMATESelectQueryByTrnsmssn_No As String = "SELECT RPR_ESTMT_ID,TRNSMSSN_NO,RPR_ESTMT_TRNSXN,GI_TRNSCTN_NO,RVSN_NO,RPR_ESTMT_DT,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,MNFCTR_DT,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,DPT_ID,DPT_CD,DPT_NAM,LST_SRVYR_NM,SRVYR_NM,LBR_RT_NC,TR_WGHT_NC,GRSS_WGHT_NC,RPR_TYP_ID,EIR_NO,RPR_TYP_CD,CRT_OF_CLANLNSS_BT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,LST_TST_DT,LST_TST_TYP_ID,LST_TST_TYP_CD,VLDTY_PRD_TST_YRS,YRD_LCTN,NXT_TST_DT,NXT_TST_TYP_ID,NXT_TST_TYP_CD,CPCTY_NC,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ORGNL_ESTMN_DT,ESTMTN_TTL_NC,OWNR_APPRVL_RF,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTVTY_DT,ACTVTY_NM FROM V_REPAIR_ESTIMATE WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND EQPMNT_NO=@EQPMNT_NO"
    Private Const Bulk_EmailSelectQuery As String = "SELECT BLK_EML_ID,CSTMR_ID,CSTMR_CD,FRM_EML,TO_EML,BCC_EML,SBJCT_VC,BDY_VC,DPT_ID,DPT_CD,SNT_BT,SNT_DT,CRTD_DT,CRTD_BY,BLK_EML_FRMT_ID,BLK_EML_FRMT_CD FROM V_BULK_EMAIL WHERE DPT_ID=@DPT_ID AND SNT_BT=0"
    Private Const Bulk_Email_Query As String = "SELECT BLK_EML_ID,CSTMR_ID,FRM_EML,TO_EML,BCC_EML,SBJCT_VC,BDY_VC,DPT_ID,SNT_BT,SNT_DT,CRTD_DT,CRTD_BY FROM BULK_EMAIL WHERE DPT_ID=@DPT_ID AND SNT_BT=0"
    Private Const UpdateBulkEmailQuery As String = "UPDATE BULK_EMAIL SET SNT_DT=@SNT_DT,SNT_BT=@SNT_BT WHERE BLK_EML_ID=@BLK_EML_ID AND DPT_ID=@DPT_ID"
    Private Const CleaningDetailsSelectQuery As String = "SELECT CLNNG_ID,CLNNG_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,CLNNG_CERT_NO,PRDCT_ID,PRDCT_CD,CHMCL_NM,CLNNG_RT,ORGNL_CLNNG_DT,LST_CLNNG_DT,ORGNL_INSPCTD_DT,LST_INSPCTD_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,CLND_FR_VCR,LCTN_OF_CLNG,EQPMNT_CLNNG_STTS_1,EQPMNT_CLNNG_STTS_1_CD,EQPMNT_CLNNG_STTS_2,EQPMNT_CLNNG_STTS_2_CD,EQPMNT_CNDTN_CD,EQPMNT_CNDTN_ID,VLV_FTTNG_CNDTN,VLV_FTTNG_CNDTN_CD,MN_LID_SL_NO,TP_SL_NO,BTTM_SL_NO,INVCNG_PRTY_ID,INVCNG_PRTY_CD,CSTMR_RFRNC_NO,CLNNG_RFRNC_NO,RMRKS_VC,DPT_ID,GI_TRNSCTN_NO,CRTD_BY,GI_TRNSCTN_NO,CRTD_BY,CERT_GNRTD_FLG,PRDCT_DSCRPTN_VC,IMO_CLSS,UN_NO FROM V_CLEANING WHERE CLNNG_ID=@CLNNG_ID AND DPT_ID=@DPT_ID"
    Private Const RepairWorkOrderCustomerDetailsQuery As String = "SELECT CSTMR_CRRNCY_CD AS CNVRT_TO_CRRNCY_CD,CSTMR_ID,CSTMR_NAM,(SELECT DPT_NAM FROM DEPOT WHERE DPT_ID=@DPT_ID)DPT_NAM,LBR_RT_PR_HR_NC,(SELECT EXCHNG_RT_PR_UNT_NC FROM EXCHANGE_RATE WHERE TO_CRRNCY_ID=(SELECT CSTMR_CRRNCY_ID FROM CUSTOMER CM WHERE CSTMR_ID =@CSTMR_ID) AND FRM_CRRNCY_ID =(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID =@DPT_ID AND BNK_TYP_ID=44))AS EXCHNG_RT_PR_UNT_NC,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=@DPT_ID AND BNK_TYP_ID=44)) AS CSTMR_CRRNCY_CD FROM V_CUSTOMER WHERE CSTMR_ID=@CSTMR_ID"
    Private Const RepairWorkOrderCompiledQuery As String = "SELECT RPR_ESTMT_DTL_ID,RPR_ESTMT_ID,(SELECT CSTMR_CD FROM V_REPAIR_ESTIMATE WHERE RPR_ESTMT_ID=RE.RPR_ESTMT_ID)AS CSTMR_CD,(SELECT CSTMR_NAM FROM V_REPAIR_ESTIMATE WHERE RPR_ESTMT_ID=RE.RPR_ESTMT_ID)AS CSTMR_NAM,DMG_DSCRPTN_VC,DMG_ID,DMG_CD,ITM_ID,ITM_CD,(SELECT ITM_DSCRPTN_VC FROM ITEM WHERE ITM_ID=RE.ITM_ID)ITM_DSCRPTN_VC,SB_ITM_ID,SB_ITM_CD,(SELECT SB_ITM_DSCRPTN_VC FROM SUB_ITEM WHERE SB_ITM_ID=RE.SB_ITM_ID)SB_ITM_DSCRPTN_VC,DMG_RPR_DSCRPTN,RPR_DSCRPTN_VC,LBR_HRS,LBR_HR_CST_NC,MTRL_CST_NC,EQPMNT_NO,(SELECT PRDCT_DSCRPTN_VC FROM PRODUCT WHERE PRDCT_ID=(SELECT PRDCT_ID FROM ACTIVITY_STATUS WHERE GI_TRNSCTN_NO=(SELECT GI_TRNSCTN_NO FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID=RE.RPR_ESTMT_ID))) AS PRDCT_DSCRPTN_VC,(SELECT PRDCT_CD FROM PRODUCT WHERE PRDCT_ID=(SELECT PRDCT_ID FROM ACTIVITY_STATUS WHERE GI_TRNSCTN_NO=(SELECT GI_TRNSCTN_NO FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID=RE.RPR_ESTMT_ID))) AS PRDCT_CD,(SELECT RPR_ESTMT_NO FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID=RE.RPR_ESTMT_ID)RPR_ESTMT_TRNSXN,(SELECT RPR_ESTMT_DT FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID=RE.RPR_ESTMT_ID)RPR_ESTMT_DT,(SELECT GI_RF_NO FROM ACTIVITY_STATUS WHERE GI_TRNSCTN_NO=(SELECT GI_TRNSCTN_NO FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID=RE.RPR_ESTMT_ID))AS GI_RF_NO FROM V_REPAIR_ESTIMATE_DETAIL RE WHERE RPR_ESTMT_ID IN("
    Private Const RepairWorkOrderSummaryQuery As String = "SELECT EQPMNT_NO,RPR_ESTMT_NO AS RPR_ESTMT_TRNSXN,ESTMTN_TTL_NC,LBR_RT_NC FROM REPAIR_ESTIMATE RE WHERE ACTVTY_NM IN ('REPAIR ESTIMATE','REPAIR APPROVAL') AND RPR_ESTMT_ID IN ("
    Private Const Depot_SelectQuery As String = "SELECT DPT_ID,DPT_CD,DPT_NAM,CNTCT_PRSN_NAM,ADDRSS_LN1_VC,ADDRSS_LN2_VC,ADDRSS_LN3_VC,VT_NO,EML_ID,PHN_NO,FX_NO,CMPNY_LG_PTH,MDFD_BY ,MDFD_DT FROM DEPOT WHERE DPT_ID=@DPT_ID"
    Private Const Bulk_Email_DetailUpdateQuery As String = "UPDATE BULK_EMAIL_DETAIL SET ATTCHMNT_PTH=@ATTCHMNT_PTH WHERE BLK_EML_DTL_ID=@BLK_EML_DTL_ID"
    'MultiLocation Query
    Private Const Bulk_EmailSelectQueryFrAllDpt As String = "SELECT BLK_EML_ID,CSTMR_ID,CSTMR_CD,FRM_EML,TO_EML,BCC_EML,SBJCT_VC,BDY_VC,DPT_ID,DPT_CD,SNT_BT,SNT_DT,CRTD_DT,CRTD_BY,BLK_EML_FRMT_ID,BLK_EML_FRMT_CD,CC_EML FROM V_BULK_EMAIL BE WHERE SNT_BT=0"
    Private Const UpdateErrorBulkEmailQuery As String = "UPDATE BULK_EMAIL SET ERR_RMRKS=@ERR_RMRKS,ERR_FLG=@ERR_FLG WHERE BLK_EML_ID=@BLK_EML_ID AND DPT_ID=@DPT_ID"

    Private ds As Bulk_EmailDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New Bulk_EmailDataSet
    End Sub

#End Region

#Region "GetBulk_Email() TABLE NAME:Bulk_Email"

    Public Function GetBulk_Email(ByVal bv_intDepotID As Integer) As Bulk_EmailDataSet
        Try
            objData = New DataObjects(Bulk_EmailSelectQueryByDpt_Id, Bulk_EmailData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), Bulk_EmailData._v_BULK_EMAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetBulk_Email() TABLE NAME:Bulk_Email"
    ''' <summary>
    ''' This Method is used to Get Bulk Email Data for all Depots
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBulk_EmailForAllDepots() As Bulk_EmailDataSet
        Try
            objData = New DataObjects(Bulk_EmailSelectQueryFrAllDpt)
            objData.Fill(CType(ds, DataSet), Bulk_EmailData._V_BULK_EMAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getRepairWorkOrderCustomerDetails() TABLE NAME:Bulk_Email"

    Public Function getRepairWorkOrderCustomerDetails(ByVal bv_CustomerID As Integer, ByVal bv_DepotID As Integer) As Bulk_EmailDataSet
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(Bulk_EmailData.CSTMR_ID, bv_CustomerID)
            hshTable.Add(Bulk_EmailData.DPT_ID, bv_DepotID)
            objData = New DataObjects(RepairWorkOrderCustomerDetailsQuery, hshTable)
            objData.Fill(CType(ds, DataSet), Bulk_EmailData._REPAIR_WORKORDER_CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningDetails"
    Public Function GetCleaningDetails(ByVal bv_intCleaningid As Int64, ByVal intDPT_ID As Int64) As Bulk_EmailDataSet
        Try
            Dim dt As New DataTable
            Dim hshTable As New Hashtable()
            hshTable.Add(Bulk_EmailData.CLNNG_ID, bv_intCleaningid)
            hshTable.Add(Bulk_EmailData.DPT_ID, intDPT_ID)
            objData = New DataObjects(CleaningDetailsSelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), Bulk_EmailData._V_CLEANING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pvt_UpdateSentStatus() TABLE NAME:Bulk_Email"

    Public Function pvt_UpdateSentStatus(ByVal bv_BulkEmailID As Integer, ByVal bv_DepotID As Integer, ByVal sentdate As DateTime, ByVal SentBit As Boolean) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(Bulk_EmailData._BULK_EMAIL).NewRow()
            With dr
                .Item(Bulk_EmailData.BLK_EML_ID) = bv_BulkEmailID
                .Item(Bulk_EmailData.SNT_DT) = sentdate
                .Item(Bulk_EmailData.SNT_BT) = SentBit
                .Item(Bulk_EmailData.DPT_ID) = bv_DepotID
            End With

            pvt_UpdateSentStatus = objData.UpdateRow(dr, UpdateBulkEmailQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetBulkEmailDetailbyBulkEmailID() TABLE NAME:Bulk_Email_Detail"

    Public Function GetBulkEmailDetailbyBulkEmailID(ByVal bv_i6BulkEmailID As Int64) As Bulk_EmailDataSet
        Try
            objData = New DataObjects(Bulk_Email_DetailSelectQueryByBulkEmailID, Bulk_EmailData.BLK_EML_ID, CStr(bv_i6BulkEmailID))
            objData.Fill(CType(ds, DataSet), Bulk_EmailData._V_BULK_EMAIL_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetBulkMailTable() TABLE NAME:Bulk_Email_Detail"

    Public Function GetBulkMailTable(ByVal bv_Depot_id As Integer) As Bulk_EmailDataSet
        Try
            objData = New DataObjects(Bulk_Email_Query, Bulk_EmailData.DPT_ID, (bv_Depot_id))
            objData.Fill(CType(ds, DataSet), Bulk_EmailData._BULK_EMAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRepair_EstimateBy_Transmission_No() TABLE NAME:V_REPAIR_ESTIMATE"

    Public Function GetRepair_EstimateBy_Transmission_No(ByVal bv_strWM_Transmission_No As String, ByVal bv_strWM_Unit_Nbr As String) As Bulk_EmailDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(Bulk_EmailData.GI_TRNSCTN_NO, bv_strWM_Transmission_No)
            hshParameters.Add(Bulk_EmailData.EQPMNT_NO, bv_strWM_Unit_Nbr)
            objData = New DataObjects(V_REPAIR_ESTIMATESelectQueryByTrnsmssn_No, hshParameters)
            objData.Fill(CType(ds, DataSet), Bulk_EmailData._V_REPAIR_ESTIMATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "getRepairWorkOrderCompiledDetails() "

    Public Function getRepairWorkOrderCompiledDetails(ByVal bv_sno As String) As Bulk_EmailDataSet
        Try
            objData = New DataObjects(String.Concat(RepairWorkOrderCompiledQuery, ((bv_sno)), ")"))
            objData.Fill(CType(ds, DataSet), Bulk_EmailData._REPAIR_WORKORDER_COMPILEDESTIMATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "getRepairWorkOrderSummary() "

    Public Function getRepairWorkOrderSummary(ByVal bv_sno As String) As Bulk_EmailDataSet
        Try
            objData = New DataObjects(String.Concat(RepairWorkOrderSummaryQuery, ((bv_sno)), ")"))
            objData.Fill(CType(ds, DataSet), Bulk_EmailData._REPAIR_WORKORDER_SUMMARY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Get_Depot() TABLE NAME:Depot"

    Public Function Get_Depot(ByVal bv_intDepotId As Int64) As Bulk_EmailDataSet
        Try
            objData = New DataObjects(Depot_SelectQuery, Bulk_EmailData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), Bulk_EmailData._DEPOT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pvt_UpdateBulkEmailDetail"
    Public Function UpdateBulkEmailDetail(ByVal bv_BulkEmailDetailID As Integer, ByVal bv_strFileName As String) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(Bulk_EmailData._BULK_EMAIL_DETAIL).NewRow()
            With dr
                .Item(Bulk_EmailData.BLK_EML_DTL_ID) = bv_BulkEmailDetailID
                .Item(Bulk_EmailData.ATTCHMNT_PTH) = bv_strFileName
            End With

            UpdateBulkEmailDetail = objData.UpdateRow(dr, Bulk_Email_DetailUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pvt_UpdateErrorStatus() TABLE NAME:Bulk_Email"

    Public Function pvt_UpdateErrorStatus(ByVal bv_BulkEmailID As Integer, ByVal bv_DepotID As Integer, ByVal bv_ErrorFlag As Boolean, ByVal bv_strErrorRemarks As String) As Boolean
        Try
            Dim dr As DataRow
            objdata = New DataObjects()
            dr = ds.Tables(Bulk_EmailData._BULK_EMAIL).NewRow()
            With dr
                .Item(Bulk_EmailData.BLK_EML_ID) = bv_BulkEmailID
                .Item(Bulk_EmailData.ERR_FLG) = bv_ErrorFlag
                .Item(Bulk_EmailData.ERR_RMRKS) = bv_strErrorRemarks
                .Item(Bulk_EmailData.DPT_ID) = bv_DepotID
            End With

            pvt_UpdateErrorStatus = objData.UpdateRow(dr, UpdateErrorBulkEmailQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class
