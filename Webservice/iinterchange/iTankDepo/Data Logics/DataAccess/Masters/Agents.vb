Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

Public Class Agents

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_AgentSelectQueryByAGNT_Id As String = "SELECT AGNT_CD,AGNT_NAM,AGNT_CRRNCY_ID,AGNT_CRRNCY_CD,AGNT_CNTCT_PRSN_NAM,AGNT_CNTCT_ADDRSS,AGNT_BLLNG_ADDRSS,AGNT_ZP_CD,AGNT_PHN_NO,AGNT_FX_NO,AGNT_INVCNG_EML_ID,AGNT_LBR_RT_PR_HR_NC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,DPT_CD,ISO_CD,AGNT_HANDLNG_TX,AGNT_STORG_TX,AGNT_SERVC_TX  FROM V_AGENT WHERE AGNT_ID=@AGNT_ID and DPT_ID=@DPT_ID"
    Private Const AgentInsertQuery As String = "INSERT INTO AGENT (AGNT_ID,AGNT_CD,AGNT_NAM,AGNT_CRRNCY_ID,AGNT_CNTCT_PRSN_NAM,AGNT_CNTCT_ADDRSS,AGNT_BLLNG_ADDRSS,AGNT_ZP_CD,AGNT_PHN_NO,AGNT_FX_NO,AGNT_INVCNG_EML_ID,AGNT_LBR_RT_PR_HR_NC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,ISO_CD,AGNT_HANDLNG_TX,AGNT_STORG_TX,AGNT_SERVC_TX)     VALUES(@AGNT_ID,@AGNT_CD,@AGNT_NAM,@AGNT_CRRNCY_ID,@AGNT_CNTCT_PRSN_NAM,@AGNT_CNTCT_ADDRSS,@AGNT_BLLNG_ADDRSS,@AGNT_ZP_CD,@AGNT_PHN_NO,@AGNT_FX_NO,@AGNT_INVCNG_EML_ID,@AGNT_LBR_RT_PR_HR_NC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID,@ISO_CD,@AGNT_HANDLNG_TX,@AGNT_STORG_TX,@AGNT_SERVC_TX)"
    Private Const AgentUpdateQuery As String = "UPDATE Agent SET AGNT_ID=@AGNT_ID, AGNT_CD=@AGNT_CD, AGNT_NAM=@AGNT_NAM, AGNT_CRRNCY_ID=@AGNT_CRRNCY_ID,  AGNT_CNTCT_PRSN_NAM=@AGNT_CNTCT_PRSN_NAM, AGNT_CNTCT_ADDRSS=@AGNT_CNTCT_ADDRSS, AGNT_BLLNG_ADDRSS=@AGNT_BLLNG_ADDRSS, AGNT_ZP_CD=@AGNT_ZP_CD, AGNT_PHN_NO=@AGNT_PHN_NO, AGNT_FX_NO=@AGNT_FX_NO, AGNT_INVCNG_EML_ID=@AGNT_INVCNG_EML_ID, AGNT_LBR_RT_PR_HR_NC=@AGNT_LBR_RT_PR_HR_NC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID, AGNT_HANDLNG_TX=@AGNT_HANDLNG_TX,AGNT_STORG_TX=@AGNT_STORG_TX,AGNT_SERVC_TX=@AGNT_SERVC_TX WHERE AGNT_ID=@AGNT_ID"
    Private Const AgentUpdateQueryWithLedger As String = "UPDATE Agent SET AGNT_ID=@AGNT_ID, AGNT_CD=@AGNT_CD, AGNT_NAM=@AGNT_NAM, AGNT_CRRNCY_ID=@AGNT_CRRNCY_ID,  BLLNG_TYP_ID=@BLLNG_TYP_ID, CNTCT_PRSN_NAM=@CNTCT_PRSN_NAM, CNTCT_ADDRSS=@CNTCT_ADDRSS, BLLNG_ADDRSS=@BLLNG_ADDRSS, ZP_CD=@ZP_CD, PHN_NO=@PHN_NO, FX_NO=@FX_NO, RPRTNG_EML_ID=@RPRTNG_EML_ID, INVCNG_EML_ID=@INVCNG_EML_ID, RPR_TCH_EML_ID=@RPR_TCH_EML_ID, BLK_EML_FRMT_ID=@BLK_EML_FRMT_ID, HYDR_AMNT_NC=@HYDR_AMNT_NC, PNMTC_AMNT_NC=@PNMTC_AMNT_NC, LBR_RT_PR_HR_NC=@LBR_RT_PR_HR_NC, LK_TST_RT_NC=@LK_TST_RT_NC, SRVY_ONHR_OFFHR_RT_NC=@SRVY_ONHR_OFFHR_RT_NC, PRDC_TST_TYP_ID=@PRDC_TST_TYP_ID, VLDTY_PRD_TST_YRS=@VLDTY_PRD_TST_YRS, MIN_HTNG_RT_NC=@MIN_HTNG_RT_NC, MIN_HTNG_PRD_NC=@MIN_HTNG_PRD_NC, HRLY_CHRG_NC=@HRLY_CHRG_NC, CHK_DGT_VLDTN_BT=@CHK_DGT_VLDTN_BT, TRNSPRTTN_BT=@TRNSPRTTN_BT, RNTL_BT=@RNTL_BT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID,XML_BT=@XML_BT,FTP_SRVR_URL=@FTP_SRVR_URL,FTP_USR_NAM=@FTP_USR_NAM,FTP_PSSWRD=@FTP_PSSWRD,ISO_CD=@ISO_CD,LDGR_ID=@LDGR_ID,SHELL=@SHELL, STUBE=@STUBE WHERE AGNT_ID=@AGNT_ID"
    Private Const Agent_RateInsertQuery As String = "INSERT INTO Agent_RATE(AGNT_RT_ID,AGNT_ID,EQPMNT_CD_ID,EQPMNT_TYP_ID,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,STRG_CHRGS_PR_DY_NC,FR_DYS,ACTV_BT,HNDLNG_CHRG_NC,WSHNG_CHRG_NC)VALUES(@AGNT_RT_ID,@AGNT_ID,@EQPMNT_CD_ID,@EQPMNT_TYP_ID,@HNDLNG_IN_CHRG_NC,@HNDLNG_OUT_CHRG_NC,@STRG_CHRGS_PR_DY_NC,@FR_DYS,@ACTV_BT,@HNDLNG_CHRG_NC,@WSHNG_CHRG_NC)"
    Private Const Agent_RateUpdateQuery As String = "UPDATE Agent_RATE SET AGNT_RT_ID=@AGNT_RT_ID, AGNT_ID=@AGNT_ID, EQPMNT_CD_ID=@EQPMNT_CD_ID, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, HNDLNG_IN_CHRG_NC=@HNDLNG_IN_CHRG_NC, HNDLNG_OUT_CHRG_NC=@HNDLNG_OUT_CHRG_NC, STRG_CHRGS_PR_DY_NC=@STRG_CHRGS_PR_DY_NC, FR_DYS=@FR_DYS, ACTV_BT=@ACTV_BT, HNDLNG_CHRG_NC=@HNDLNG_CHRG_NC, WSHNG_CHRG_NC=@WSHNG_CHRG_NC WHERE AGNT_RT_ID=@AGNT_RT_ID"
    Private Const V_Agent_RateSelectQueryByAGNT_id As String = "SELECT AGNT_RT_ID,AGNT_ID,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,STRG_CHRGS_PR_DY_NC,FR_DYS,ACTV_BT,HNDLNG_CHRG_NC,WSHNG_CHRG_NC FROM V_Agent_RATE WHERE AGNT_ID=@AGNT_ID"
    Private Const Agent_RateSelectQuery As String = "SELECT AGNT_RT_ID,AGNT_ID,EQPMNT_CD_ID,EQPMNT_TYP_ID,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,STRG_CHRGS_PR_DY_NC,FR_DYS,ACTV_BT,HNDLNG_CHRG_NC,WSHNG_CHRG_NC FROM Agent_RATE"
    Private Const Agent_RateDeleteQuery As String = "DELETE FROM Agent_RATE WHERE AGNT_RT_ID=@AGNT_RT_ID"
    Private Const EDISettingUpdateQuery As String = "UPDATE Agent_EDI_SETTING SET EDI_FRMT=@EDI_FRMT,TO_EML_ID=@TO_EML_ID,GNRTN_FRMT=@GNRTN_FRMT,GNRTN_TM=@GNRTN_TM,LST_RN=@LST_RN WHERE AGNT_ID=@AGNT_ID"
    Private Const EDISettingInsertQuery As String = "INSERT INTO Agent_EDI_SETTING(AGNT_EDI_STTNG_BIN,AGNT_ID,EDI_FRMT,TO_EML_ID,GNRTN_FRMT,GNRTN_TM,LST_RN)VALUES(@AGNT_EDI_STTNG_BIN,@AGNT_ID,@EDI_FRMT,@TO_EML_ID,@GNRTN_FRMT,@GNRTN_TM,@LST_RN)"
    Private Const V_Agent_EDI_SETTINGSelectQueryByAGNT_id As String = "SELECT AGNT_ID,AGNT_CD,EDI_FRMT,EDI_FRMT_CD,TO_EML_ID,GNRTN_FRMT,GNRTN_FRMT_CD,GNRTN_TM FROM V_Agent_EDI_SETTING WHERE AGNT_ID=@AGNT_ID"
    Private Const CountEDISettingQuery As String = "SELECT COUNT(AGNT_ID) FROM V_Agent_EDI_SETTING WHERE AGNT_ID=@AGNT_ID"
    Private Const EnumSelectQuery As String = "SELECT DISTINCT ENM_ID AS RPRT_ID,ENM_CD AS RPRT_CD,ENM_TYP_ID FROM ENUM WHERE ENM_TYP_ID=11 AND ENM_CD IN ("
    Private Const EmailSettingCountQuery As String = "SELECT COUNT(AGNT_ID) FROM V_Agent_EMAIL_SETTING WHERE (AGNT_ID=@AGNT_ID AND ENM_ID=@ENM_ID)"
    Private Const EmailSettingInsertQuery As String = "INSERT INTO Agent_EMAIL_SETTING(AGNT_EML_STTNG_BIN,AGNT_ID,RPRT_ID,GNRTN_TM,TO_EML,CC_EML,SBJCT_VCR,PRDC_DT_ID,PRDC_DY_ID,PRDC_FLTR_ID,NXT_RN_DT_TM,ACTV_BT)VALUES(@AGNT_EML_STTNG_BIN,@AGNT_ID,@RPRT_ID,@GNRTN_TM,@TO_EML,@CC_EML,@SBJCT_VCR,@PRDC_DT_ID,@PRDC_DY_ID,@PRDC_FLTR_ID,@NXT_RN_DT_TM,@ACTV_BT)"
    Private Const EmailSettingUpdateQuery As String = "UPDATE Agent_EMAIL_SETTING SET RPRT_ID=@ENM_ID,GNRTN_TM=@GNRTN_TM,TO_EML=@TO_EML,CC_EML=@CC_EML,BCC_EML=@BCC_EML,SBJCT_VCR=@SBJCT_VCR,ACTV_BT=@ACTV_BT WHERE (AGNT_ID=@AGNT_ID AND RPRT_ID=@ENM_ID)"
    Private Const V_Agent_EMAIL_SETTINGSelectQueryByAGNT_id As String = "SELECT AGNT_EML_STTNG_BIN,RPRT_ID,RPRT_CD,AGNT_ID,AGNT_CD,GNRTN_TM,TO_EML,CC_EML,SBJCT_VCR,ACTV_BT,PRDC_FLTR_ID,PRDC_FLTR_CD,PRDC_DY_CD,PRDC_DY_ID,PRDC_DT_ID,PRDC_DT_CD,NXT_RN_DT_TM FROM V_Agent_EMAIL_SETTING WHERE AGNT_ID=@AGNT_ID ORDER BY RPRT_ID"
    Private Const AgentEmailSettingDeleteQuery As String = "DELETE FROM Agent_EMAIL_SETTING WHERE AGNT_ID=@AGNT_ID"
    Private Const Agent_Charge_DetailInsertQuery As String = "INSERT INTO Agent_CHARGE_DETAIL(AGNT_CHRG_DTL_ID,AGNT_ID,EQPMNT_CD_ID,EQPMNT_TYP_ID,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,ACTV_BT,INSPCTN_CHRGS)VALUES(@AGNT_CHRG_DTL_ID,@AGNT_ID,@EQPMNT_CD_ID,@EQPMNT_TYP_ID,@HNDLNG_IN_CHRG_NC,@HNDLNG_OUT_CHRG_NC,@ACTV_BT,@INSPCTN_CHRGS)"
    Private Const Agent_Charge_DetailUpdateQuery As String = "UPDATE Agent_CHARGE_DETAIL SET AGNT_CHRG_DTL_ID=@AGNT_CHRG_DTL_ID, AGNT_ID=@AGNT_ID, EQPMNT_CD_ID=@EQPMNT_CD_ID, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, HNDLNG_IN_CHRG_NC=@HNDLNG_IN_CHRG_NC, HNDLNG_OUT_CHRG_NC=@HNDLNG_OUT_CHRG_NC, ACTV_BT=@ACTV_BT,INSPCTN_CHRGS=@INSPCTN_CHRGS WHERE AGNT_CHRG_DTL_ID=@AGNT_CHRG_DTL_ID"
    Private Const Agent_Storage_DetailInsertQuery As String = "INSERT INTO Agent_STORAGE_DETAIL(AGNT_STRG_DTL_ID,AGNT_CHRG_DTL_ID,AGNT_ID,AGNT_UP_TO_DYS,AGNT_STRG_CHRG_NC,AGNT_RMRKS_VC)VALUES(@AGNT_STRG_DTL_ID,@AGNT_CHRG_DTL_ID,@AGNT_ID,@AGNT_UP_TO_DYS,@AGNT_STRG_CHRG_NC,@AGNT_RMRKS_VC)"
    Private Const Agent_Storage_DetailUpdateQuery As String = "UPDATE Agent_STORAGE_DETAIL SET AGNT_STRG_DTL_ID=@AGNT_STRG_DTL_ID, AGNT_CHRG_DTL_ID=@AGNT_CHRG_DTL_ID, AGNT_ID=@AGNT_ID, AGNT_UP_TO_DYS=@AGNT_UP_TO_DYS, AGNT_STRG_CHRG_NC=@AGNT_STRG_CHRG_NC, AGNT_RMRKS_VC=@AGNT_RMRKS_VC WHERE AGNT_STRG_DTL_ID=@AGNT_STRG_DTL_ID"
    Private Const Agent_CHARGE_DETAILSelectQueryByAgentID As String = "SELECT AGNT_CHRG_DTL_ID,AGNT_CD,AGNT_NAM,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,ACTV_BT,DPT_ID,INSPCTN_CHRGS  FROM dbo.V_AGENT_CHARGE_DETAIL where AGNT_ID=@AGNT_ID"
    Private Const Agent_STORAGE_DETAILSelectQueryByAgentChargeDetailID As String = "SELECT AGNT_STRG_DTL_ID,AGNT_CHRG_DTL_ID,AGNT_ID,AGNT_UP_TO_DYS,AGNT_STRG_CHRG_NC,AGNT_RMRKS_VC FROM Agent_STORAGE_DETAIL WHERE AGNT_ID=@AGNT_ID AND AGNT_CHRG_DTL_ID=@AGNT_CHRG_DTL_ID"
    Private Const Agent_Storage_DetailDeleteQueryBySorageDetailID As String = "DELETE FROM Agent_STORAGE_DETAIL WHERE AGNT_STRG_DTL_ID=@AGNT_STRG_DTL_ID"
    Private Const Agent_Storage_DetailDeleteQueryByChargeDetailID As String = "DELETE FROM Agent_STORAGE_DETAIL WHERE AGNT_CHRG_DTL_ID=@AGNT_CHRG_DTL_ID"
    Private Const Agent_Charge_DetailDeleteQuery As String = "DELETE FROM Agent_CHARGE_DETAIL WHERE AGNT_CHRG_DTL_ID=@AGNT_CHRG_DTL_ID"
    Private Const V_SERVICE_PARTNERSelectQuery As String = "SELECT SRVC_PRTNR_ACTL_ID,SRVC_PRTNR_CD,SRVC_PRTNR_NAM,SRVC_PRTNR_ID,SRVC_PRTNR_TYP_CD,DPT_ID,ACTV_BT FROM V_SERVICE_PARTNER WHERE DPT_ID=@DPT_ID AND SRVC_PRTNR_CD=@SRVC_PRTNR_CD and SRVC_PRTNR_TYP_CD='Agent'"
    'for iso
    Private Const getAgentISOcodebyAgentIDquery As String = "SELECT AGNT_CD,ISO_CD FROM V_Agent WHERE AGNT_ID=@AGNT_ID"
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
    Dim sqlDateDbnull As DateTime = "01/01/1900"
    Private ds As AgentDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New AgentDataSet
    End Sub

#End Region

#Region "GetV_AgentByAGNT_ID() TABLE NAME:V_Agent"

    Public Function GetV_AgentByAGNT_Id(ByVal bv_intAGNT_ID As Integer, ByVal bv_intDepotId As Integer) As AgentDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(AgentData.DPT_ID, bv_intDepotId)
            hshParameters.Add(AgentData.AGNT_ID, bv_intAGNT_ID)
            objData = New DataObjects(V_AgentSelectQueryByAGNT_Id, hshParameters)
            objData.Fill(CType(ds, DataSet), AgentData._V_Agent)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateAgent() TABLE NAME:Agent"
    Public Function CreateAgent(ByVal bv_strAgentCode As String, _
                                   ByVal bv_strAgentName As String, _
                                   ByVal bv_i64AgentCurrencyID As Int64, _
                                   ByVal bv_strContactPersonName As String, _
                                   ByVal bv_strContactAddress As String, _
                                   ByVal bv_strBillingAddress As String, _
                                   ByVal bv_strZipCode As String, _
                                   ByVal bv_strPhoneNumber As String, _
                                   ByVal bv_strFaxNumber As String, _
                                   ByVal bv_strInvoicingEmailID As String, _
                                   ByVal bv_decLBR_RT_PR_HR_NC As Decimal, _
                                   ByVal bv_strCreatedBy As String, _
                                   ByVal bv_datCreatedDate As DateTime, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                   ByVal bv_i32DepotID As Int32, _
                                   ByVal bv_strStorageTax As String, _
                                   ByVal bv_strHandlingTax As String, _
                                   ByVal bv_strServiceTax As String, _
                                   ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(AgentData._Agent).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(AgentData._SERVICE_PARTNER, br_ObjTransactions)
                .Item(AgentData.AGNT_ID) = intMax
                .Item(AgentData.AGNT_CD) = bv_strAgentCode
                .Item(AgentData.AGNT_NAM) = bv_strAgentName
                .Item(AgentData.AGNT_CRRNCY_ID) = bv_i64AgentCurrencyID
                .Item(AgentData.AGNT_CNTCT_PRSN_NAM) = bv_strContactPersonName
                .Item(AgentData.AGNT_CNTCT_ADDRSS) = bv_strContactAddress
                If bv_strZipCode <> Nothing Then
                    .Item(AgentData.AGNT_ZP_CD) = bv_strZipCode
                Else
                    .Item(AgentData.AGNT_ZP_CD) = DBNull.Value
                End If
                If bv_strPhoneNumber <> Nothing Then
                    .Item(AgentData.AGNT_PHN_NO) = bv_strPhoneNumber
                Else
                    .Item(AgentData.AGNT_PHN_NO) = DBNull.Value
                End If
                If bv_strFaxNumber <> Nothing Then
                    .Item(AgentData.AGNT_FX_NO) = bv_strFaxNumber
                Else
                    .Item(AgentData.AGNT_FX_NO) = DBNull.Value
                End If
                If bv_strInvoicingEmailID <> Nothing Then
                    .Item(AgentData.AGNT_INVCNG_EML_ID) = bv_strInvoicingEmailID
                Else
                    .Item(AgentData.AGNT_PHN_NO) = DBNull.Value
                End If
                If bv_strBillingAddress <> Nothing Then
                    .Item(AgentData.AGNT_BLLNG_ADDRSS) = bv_strBillingAddress
                Else
                    .Item(AgentData.AGNT_BLLNG_ADDRSS) = DBNull.Value
                End If
                If bv_decLBR_RT_PR_HR_NC <> Nothing Then
                    .Item(AgentData.AGNT_LBR_RT_PR_HR_NC) = bv_decLBR_RT_PR_HR_NC
                Else
                    .Item(AgentData.AGNT_LBR_RT_PR_HR_NC) = DBNull.Value
                End If
                .Item(AgentData.CRTD_BY) = bv_strCreatedBy
                .Item(AgentData.CRTD_DT) = bv_datCreatedDate
                .Item(AgentData.MDFD_BY) = bv_strModifiedBy
                .Item(AgentData.MDFD_DT) = bv_datModifiedDate
                .Item(AgentData.ACTV_BT) = bv_blnActiveBit
                .Item(AgentData.DPT_ID) = bv_i32DepotID


                If bv_strServiceTax <> Nothing Then
                    .Item(AgentData.AGNT_SERVC_TX) = CDec(bv_strServiceTax)
                Else
                    .Item(AgentData.AGNT_SERVC_TX) = DBNull.Value
                End If
                If bv_strStorageTax <> Nothing Then
                    .Item(AgentData.AGNT_STORG_TX) = CDec(bv_strStorageTax)
                Else
                    .Item(AgentData.AGNT_STORG_TX) = DBNull.Value
                End If
                If bv_strHandlingTax <> Nothing Then
                    .Item(AgentData.AGNT_HANDLNG_TX) = CDec(bv_strHandlingTax)
                Else
                    .Item(AgentData.AGNT_HANDLNG_TX) = DBNull.Value
                End If

            End With
            objData.InsertRow(dr, AgentInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateAgent = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateAgent() TABLE NAME:Agent"
    Public Function UpdateAgent(ByVal bv_i64AgentID As Int64, _
                                   ByVal bv_strAgentCode As String, _
                                   ByVal bv_strAgentName As String, _
                                   ByVal bv_i64AgentCurrencyID As Int64, _
                                   ByVal bv_strContactPersonName As String, _
                                   ByVal bv_strContactAddress As String, _
                                   ByVal bv_strBillingAddress As String, _
                                   ByVal bv_strZipCode As String, _
                                   ByVal bv_strPhoneNumber As String, _
                                   ByVal bv_strFaxNumber As String, _
                                   ByVal bv_strInvoicingEmailID As String, _
                                   ByVal bv_decLaborRatePerHour As Decimal, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                   ByVal bv_i32DepotID As Int32, _
                                   ByVal bv_strStorageTax As String, _
                                   ByVal bv_strHandlingTax As String, _
                                   ByVal bv_strServiceTax As String, _
                                   ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(AgentData._Agent).NewRow()
            With dr
                .Item(AgentData.AGNT_ID) = bv_i64AgentID
                .Item(AgentData.AGNT_CD) = bv_strAgentCode
                .Item(AgentData.AGNT_NAM) = bv_strAgentName
                .Item(AgentData.AGNT_CRRNCY_ID) = bv_i64AgentCurrencyID

                .Item(AgentData.AGNT_CNTCT_PRSN_NAM) = bv_strContactPersonName
                .Item(AgentData.AGNT_CNTCT_ADDRSS) = bv_strContactAddress
                If bv_strBillingAddress <> Nothing Then
                    .Item(AgentData.AGNT_BLLNG_ADDRSS) = bv_strBillingAddress
                Else
                    .Item(AgentData.AGNT_BLLNG_ADDRSS) = DBNull.Value
                End If
                If bv_strZipCode <> Nothing Then
                    .Item(AgentData.AGNT_ZP_CD) = bv_strZipCode
                Else
                    .Item(AgentData.AGNT_ZP_CD) = DBNull.Value
                End If
                If bv_strPhoneNumber <> Nothing Then
                    .Item(AgentData.AGNT_PHN_NO) = bv_strPhoneNumber
                Else
                    .Item(AgentData.AGNT_PHN_NO) = DBNull.Value
                End If
                If bv_strFaxNumber <> Nothing Then
                    .Item(AgentData.AGNT_FX_NO) = bv_strFaxNumber
                Else
                    .Item(AgentData.AGNT_FX_NO) = DBNull.Value
                End If

                If bv_strInvoicingEmailID <> Nothing Then
                    .Item(AgentData.AGNT_INVCNG_EML_ID) = bv_strInvoicingEmailID
                Else
                    .Item(AgentData.AGNT_INVCNG_EML_ID) = DBNull.Value
                End If

                .Item(AgentData.MDFD_BY) = bv_strModifiedBy
                .Item(AgentData.MDFD_DT) = bv_datModifiedDate
                .Item(AgentData.ACTV_BT) = bv_blnActiveBit
                .Item(AgentData.DPT_ID) = bv_i32DepotID

                If bv_decLaborRatePerHour <> Nothing Then
                    .Item(AgentData.AGNT_LBR_RT_PR_HR_NC) = CDec(bv_decLaborRatePerHour)
                Else
                    .Item(AgentData.AGNT_LBR_RT_PR_HR_NC) = DBNull.Value
                End If
                If bv_strServiceTax <> Nothing Then
                    .Item(AgentData.AGNT_SERVC_TX) = CDec(bv_strServiceTax)
                Else
                    .Item(AgentData.AGNT_SERVC_TX) = DBNull.Value
                End If
                If bv_strStorageTax <> Nothing Then
                    .Item(AgentData.AGNT_STORG_TX) = CDec(bv_strStorageTax)
                Else
                    .Item(AgentData.AGNT_STORG_TX) = DBNull.Value
                End If
                If bv_strHandlingTax <> Nothing Then
                    .Item(AgentData.AGNT_HANDLNG_TX) = CDec(bv_strHandlingTax)
                Else
                    .Item(AgentData.AGNT_HANDLNG_TX) = DBNull.Value
                End If
            End With
            objData.UpdateRow(dr, AgentUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "CreateAgentChargeDetail() TABLE NAME:Agent_Charge_Detail"
    Public Function CreateAgentChargeDetail(ByVal bv_i64AgentID As Int64, _
                                               ByVal bv_i64EquipmentCodeID As Int64, _
                                               ByVal bv_i64EquipmentTypeID As Int64, _
                                               ByVal bv_strHandlingInCharges As Decimal, _
                                               ByVal bv_strHandlingOutCharge As Decimal, _
                                               ByVal bv_decInspectionCharge As Decimal, _
                                               ByVal bv_blnActiveBit As Boolean, _
                                               ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(AgentData._Agent_CHARGE_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(AgentData._Agent_CHARGE_DETAIL, br_ObjTransactions)
                .Item(AgentData.AGNT_CHRG_DTL_ID) = intMax
                .Item(AgentData.AGNT_ID) = bv_i64AgentID
                .Item(AgentData.AGNT_EQPMNT_CD_ID) = bv_i64EquipmentCodeID
                .Item(AgentData.AGNT_EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                .Item(AgentData.AGNT_HNDLNG_IN_CHRG_NC) = bv_strHandlingInCharges
                .Item(AgentData.AGNT_HNDLNG_OUT_CHRG_NC) = bv_strHandlingOutCharge
                .Item(AgentData.INSPCTN_CHRGS) = bv_decInspectionCharge
                .Item(AgentData.ACTV_BT) = bv_blnActiveBit
            End With
            objData.InsertRow(dr, Agent_Charge_DetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateAgentChargeDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateAgentChargeDetail() TABLE NAME:Agent_Charge_Detail"
    Public Function UpdateAgentChargeDetail(ByVal bv_i64AgentChargeID As Int64, _
                                               ByVal bv_i64AgentID As Int64, _
                                               ByVal bv_i64EquipmentCodeID As Int64, _
                                               ByVal bv_i64EquipmentTypeID As Int64, _
                                               ByVal bv_strHandlingInCharges As Decimal, _
                                               ByVal bv_strHandlingOutCharge As Decimal, _
                                               ByVal bv_decInspectionCharge As Decimal, _
                                               ByVal bv_blnActiveBit As Boolean, _
                                               ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(AgentData._Agent_CHARGE_DETAIL).NewRow()
            With dr
                .Item(AgentData.AGNT_CHRG_DTL_ID) = bv_i64AgentChargeID
                .Item(AgentData.AGNT_ID) = bv_i64AgentID
                .Item(AgentData.AGNT_EQPMNT_CD_ID) = bv_i64EquipmentCodeID
                .Item(AgentData.AGNT_EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                .Item(AgentData.AGNT_HNDLNG_IN_CHRG_NC) = bv_strHandlingInCharges
                .Item(AgentData.AGNT_HNDLNG_OUT_CHRG_NC) = bv_strHandlingOutCharge
                .Item(AgentData.INSPCTN_CHRGS) = bv_decInspectionCharge
                .Item(AgentData.ACTV_BT) = bv_blnActiveBit
            End With
            UpdateAgentChargeDetail = objData.UpdateRow(dr, Agent_Charge_DetailUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteAgentChargeDetail() TABLE NAME:Agent_CHARGE_DETAIL"

    Public Function DeleteAgentChargeDetail(ByVal bv_blnAgentChargeID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(AgentData._Agent_CHARGE_DETAIL).NewRow()
            With dr
                .Item(AgentData.AGNT_CHRG_DTL_ID) = bv_blnAgentChargeID
            End With
            DeleteAgentChargeDetail = objData.DeleteRow(dr, Agent_Charge_DetailDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateAgentStorageDetail() TABLE NAME:Agent_Storage_Detail"
    Public Function CreateAgentStorageDetail(ByVal bv_i64AgentChargeDetailID As Int64, _
                                                ByVal bv_i64AgentID As Int64, _
                                                ByVal bv_i32UptoDays As Int32, _
                                                ByVal bv_strStorageCharge As Decimal, _
                                                ByVal bv_strRemarks As String, _
                                                ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(AgentData._Agent_STORAGE_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(AgentData._Agent_STORAGE_DETAIL, br_ObjTransactions)
                .Item(AgentData.AGNT_STRG_DTL_ID) = intMax
                .Item(AgentData.AGNT_CHRG_DTL_ID) = bv_i64AgentChargeDetailID
                .Item(AgentData.AGNT_ID) = bv_i64AgentID
                .Item(AgentData.AGNT_UP_TO_DYS) = bv_i32UptoDays
                .Item(AgentData.AGNT_STRG_CHRG_NC) = bv_strStorageCharge
                If bv_strRemarks <> Nothing Then
                    .Item(AgentData.AGNT_RMRKS_VC) = bv_strRemarks
                Else
                    .Item(AgentData.AGNT_RMRKS_VC) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Agent_Storage_DetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateAgentStorageDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateAgentStorageDetail() TABLE NAME:Agent_Storage_Detail"
    Public Function UpdateAgentStorageDetail(ByVal bv_i64AgentStorageDetailID As Int64, _
                                                ByVal bv_i64AgentChargeDetailID As Int64, _
                                                ByVal bv_i64AgentID As Int64, _
                                                ByVal bv_i32UptoDays As Int32, _
                                                ByVal bv_strStorageCharge As Decimal, _
                                                ByVal bv_strRemarks As String, _
                                                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(AgentData._Agent_STORAGE_DETAIL).NewRow()
            With dr
                .Item(AgentData.AGNT_STRG_DTL_ID) = bv_i64AgentStorageDetailID
                .Item(AgentData.AGNT_CHRG_DTL_ID) = bv_i64AgentChargeDetailID
                .Item(AgentData.AGNT_ID) = bv_i64AgentID
                .Item(AgentData.AGNT_UP_TO_DYS) = bv_i32UptoDays
                .Item(AgentData.AGNT_STRG_CHRG_NC) = bv_strStorageCharge
                If bv_strRemarks <> Nothing Then
                    .Item(AgentData.AGNT_RMRKS_VC) = bv_strRemarks
                Else
                    .Item(AgentData.AGNT_RMRKS_VC) = DBNull.Value
                End If
            End With
            UpdateAgentStorageDetail = objData.UpdateRow(dr, Agent_Storage_DetailUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "AgentChargeDetailByAgentID() TABLE NAME:Agent_CHARGE_DETAIL"

    Public Function AgentChargeDetailByAgentID(ByVal bv_i64AgentID As Int64) As AgentDataSet
        Try
            objData = New DataObjects(Agent_CHARGE_DETAILSelectQueryByAgentID, AgentData.AGNT_ID, CStr(bv_i64AgentID))
            objData.Fill(CType(ds, DataSet), AgentData._V_Agent_CHARGE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "AgentStorageDetailByAgentChargeDetailID() TABLE NAME:Agent_STORAGE_DETAIL"

    Public Function AgentStorageDetailByAgentChargeDetailID(ByVal bv_i64AgentID As Int64, ByVal bv_i64AgentChargeDetailID As Int64) As AgentDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(AgentData.AGNT_ID, bv_i64AgentID)
            hshParameters.Add(AgentData.AGNT_CHRG_DTL_ID, bv_i64AgentChargeDetailID)
            objData = New DataObjects(Agent_STORAGE_DETAILSelectQueryByAgentChargeDetailID, hshParameters)
            objData.Fill(CType(ds, DataSet), AgentData._Agent_STORAGE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "DeleteAgentStorageDetailByStorageDetailID() TABLE NAME:Agent_Storage_Detail"

    Public Function DeleteAgentStorageDetailByStorageDetailID(ByVal bv_strAgentStorageDetailID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(AgentData._Agent_STORAGE_DETAIL).NewRow()
            With dr
                .Item(AgentData.AGNT_STRG_DTL_ID) = bv_strAgentStorageDetailID
            End With
            DeleteAgentStorageDetailByStorageDetailID = objData.DeleteRow(dr, Agent_Storage_DetailDeleteQueryBySorageDetailID, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteAgentStorageDetailByChargeDetailID() TABLE NAME:Agent_Storage_Detail"

    Public Function DeleteAgentStorageDetailByChargeDetailID(ByVal bv_strAgentChargeDetailID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(AgentData._Agent_STORAGE_DETAIL).NewRow()
            With dr
                .Item(AgentData.AGNT_CHRG_DTL_ID) = bv_strAgentChargeDetailID
            End With
            DeleteAgentStorageDetailByChargeDetailID = objData.DeleteRow(dr, Agent_Storage_DetailDeleteQueryByChargeDetailID, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetAgentByCode"
    Public Function GetAgentByCode(ByVal bv_strAgentCode As String, ByVal strServiceType As String, ByVal intDepotID As Integer) As AgentDataSet
        Try
            Dim ds As New AgentDataSet
            Dim hshConfiguration As New Hashtable()
            hshConfiguration.Add(AgentData.SRVC_PRTNR_CD, bv_strAgentCode)
            hshConfiguration.Add(AgentData.DPT_ID, intDepotID)

            objData = New DataObjects(V_SERVICE_PARTNERSelectQuery, hshConfiguration)
            objData.Fill(CType(ds, DataSet), AgentData._V_SERVICE_PARTNER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class
