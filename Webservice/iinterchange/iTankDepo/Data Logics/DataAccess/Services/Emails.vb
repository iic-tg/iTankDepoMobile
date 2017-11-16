Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

Public Class Emails

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const Bulk_EmailSelectQueryByDpt_Id As String = "SELECT BLK_EML_BIN,FRM_EML,TO_EML,SBJCT_VC,BDY_VC,SNT_DT,CRTD_DT,CRTD_BY,ACTV_BT,DPT_ID FROM BULK_EMAIL WHERE ACTV_BT=1"
    Private Const Bulk_Email_DetailSelectQueryByBlk_Eml_Bin As String = "SELECT BLK_EML_DTL_BIN,BLK_EML_BIN,ACTVTY_NAM,TRNSMSSN_NO,EQPMNT_NO,STTS_VC FROM BULK_EMAIL_DETAIL WHERE BLK_EML_BIN=@BLK_EML_BIN"
    Private Const V_REPAIR_ESTIMATESelectQueryByTrnsmssn_No As String = "SELECT RPR_ESTMT_ID,TRNSMSSN_NO,RPR_ESTMT_TRNSXN,LSS_APPRVL_DT,LSS_APPRVL_AMNT_NC,OWNR_APPRVL_DT,OWNR_APPRVL_AMNT_NC,RVSN_NO,RPR_ESTMT_DT,EQPMNT_NO,LSS_ID,LSS_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_SZ_ID,EQPMNT_SZ_CD,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,SRVC_TX_RT,DPT_ID,DPT_CD,BS_CRRNCY_ID,BS_CRRNCY_CD,DPP_CRRNCY_ID,DPP_CRRNCY_CD,GTN_DT,LST_OH_LOC,LST_OH_DT,MNFCTR_DT,SRVYR_NAM,TX_RT_NC,NTS_VC,LBR_RT_NC,DPP_AMT_NC,MSR_ID,MSR_CD,UNT_ID,UNT_CD,ESTMTN_TTL_NC,TTL_LBR_CST_NC,TTL_SRVC_TX_NC,EST_TTL_TXD_NC,SRVC_TX_RT_NC,RDL_ATH_NO,EIR_NO,AUTH_AMNT_NC,ORGNL_ESTMN_DT,EXCHG_RT_NC,EDI_STTS,PCK_DT,EST_STTS,YRD_LCTN,RC_EST_STTS,ESTMTN_TYP FROM V_REPAIR_ESTIMATE WHERE TRNSMSSN_NO=@TRNSMSSN_NO and EQPMNT_NO=@EQPMNT_NO"
    Private Const V_REPAIR_ESTIMATE_DETAILSelectQueryByRPR_ESTMT_ID As String = "SELECT RPR_ESTMT_DTL_ID,RPR_ESTMT_ID,CMMNTS,LINE,RPR_ID,RPR_CD,RPR_DSCRPTN_VC,QTY,DMG_ID,DMG_CD,DMG_DSCRPTN_VC,CMPNNT_ID,CMPNNT_CD,CMPNNT_DSCRPTN_VC,LCTN_CD,MTRL_ID,MTRL_CD,LNGTH_NC,WDTH_NC,HIGHT_NC,UNT_ID,UNT_CD,LBR_RT,LBR_HRS,LBR_HR_CST_NC,MTRL_CST_NC,RSPNSBLTY_ID,RSPNSBLTY_CD,TX_RL_ID,TX_RL_CD,TX_RT_NC,DMG_RPR_DSCRPTN,TRFF_CD FROM V_REPAIR_ESTIMATE_DETAIL WHERE RPR_ESTMT_ID=@RPR_ESTMT_ID"
    Private Const Bulk_EmailUpdateQuery As String = "UPDATE BULK_EMAIL SET BLK_EML_BIN=@BLK_EML_BIN,SNT_DT=@SNT_DT, ACTV_BT=0 WHERE BLK_EML_BIN=@BLK_EML_BIN"

    Private ds As BulkEmailDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New BulkEmailDataSet
    End Sub

#End Region

#Region "GetBulk_Email() TABLE NAME:Bulk_Email"

    Public Function GetBulk_Email() As BulkEmailDataSet
        Try
            objData = New DataObjects(Bulk_EmailSelectQueryByDpt_Id)
            objData.Fill(CType(ds, DataSet), EmailData._BULK_EMAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetBulkEmailDetailbyblk_eml_bin() TABLE NAME:Bulk_Email_Detail"

    Public Function GetBulkEmailDetailbyblk_eml_bin(ByVal bv_i64Blk_Eml_Bin As Int64) As BulkEmailDataSet
        Try
            objData = New DataObjects(Bulk_Email_DetailSelectQueryByBlk_Eml_Bin, EmailData.BLK_EML_BIN, CStr(bv_i64Blk_Eml_Bin))
            objData.Fill(CType(ds, DataSet), EmailData._BULK_EMAIL_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRepair_EstimateBy_Transmission_No() TABLE NAME:V_REPAIR_ESTIMATE"

    Public Function GetRepair_EstimateBy_Transmission_No(ByVal bv_strWM_Transmission_No As String, ByVal bv_strWM_Unit_Nbr As String) As BulkEmailDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EmailData.TRNSMSSN_NO, bv_strWM_Transmission_No)
            hshParameters.Add(EmailData.EQPMNT_NO, bv_strWM_Unit_Nbr)
            objData = New DataObjects(V_REPAIR_ESTIMATESelectQueryByTrnsmssn_No, hshParameters)
            objData.Fill(CType(ds, DataSet), EmailData._V_REPAIR_ESTIMATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_Repair_Estimate_DetailByRpr_Estmt_Id() TABLE NAME:V_REPAIR_ESTIMATE_DETAIL"

    Public Function GetV_Repair_Estimate_DetailByRpr_Estmt_Id(ByVal bv_i64RPR_ESTMT_ID As Int64) As BulkEmailDataSet
        Try
            objData = New DataObjects(V_REPAIR_ESTIMATE_DETAILSelectQueryByRPR_ESTMT_ID, EmailData.RPR_ESTMT_ID, CStr(bv_i64RPR_ESTMT_ID))
            objData.Fill(CType(ds, DataSet), EmailData._V_REPAIR_ESTIMATE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateBulk_Email() TABLE NAME:Bulk_Email"

    Public Function UpdateBulk_Email(ByVal bv_i64Blk_Eml_Bin As Int64, ByVal bv_datSntdt As DateTime) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EmailData._BULK_EMAIL).NewRow()
            With dr
                .Item(EmailData.BLK_EML_BIN) = bv_i64Blk_Eml_Bin
                .Item(EmailData.SNT_DT) = bv_datSntdt
            End With
            UpdateBulk_Email = objData.UpdateRow(dr, Bulk_EmailUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class







