Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.IO

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class RepairEstimateMobile
    Inherits System.Web.Services.WebService
    Dim dsRepairCompletion As New RepairCompletionDataSet
    Dim gateInMobile As New GateinMobile_C
    Dim conn As New Dropdown_C
    Dim objCommon As New CommonData
    Dim Response As New ArrayList
    Dim objEstimate As New RepairEstimate
    Dim objCommonConfig As New ConfigSetting()
    Dim str_032KeyValue As String
    Dim bln_032EqType_Key As Boolean
    Dim bln_045KeyExist As Boolean
    Dim bln_044KeyExist As Boolean
    Dim str_044KeyValue As String
    Dim str_045KeyValue As String
    Dim str_057GWS As String
    Dim bln_057GWSActive_Key As Boolean
    Dim bv_strEquipmentNo As String
    Dim bv_strGateinTransaction As String
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"

    Dim updte As New RepairEstimateDataSetUpdate

    Dim arraylist As New ArrayList
    Dim arrayOfDropDown As New ArrayOfDropdowns
    'Repair List EStimate,Approval,Survey
    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function RepairList(ByVal UserName As String, ByVal Mode As String, ByVal PageName As String) As ArrayOfRepairEstimateMobileModel


        Dim ArrayOfRepairEstimateMobileModel As New ArrayOfRepairEstimateMobileModel
        Try



            gateInMobile.DepotID(UserName)
            Dim intDepotID As Integer = objCommon.GetDepotID()
            Dim objCleaning As New Cleaning
            Dim query As String = ""
            Dim EQPMNT_STTS_CD As String = ""
            If PageName = "Repair Estimate" Then
                If Mode = "new" Then
                    EQPMNT_STTS_CD = "AWE"
                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AWE' AND ACTV_BT = 1"
                Else
                    EQPMNT_STTS_CD = "AAR"
                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1"
                End If

            ElseIf PageName = "Repair Approval" Then
                If Mode = "new" Then
                    EQPMNT_STTS_CD = "AAR"
                    'AAR State
                    query = " select EQPMNT_NO  from [V_ACTIVITY_STATUS] where EQPMNT_STTS_CD = 'AAR' and ACTV_BT=1"

                    Dim dsSub As DataSet = conn.connection(query)
                    'conn.connection(query).Fill(dsSub)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds
                    Dim dtPreaAdviceSub As DataTable
                    dtPreaAdviceSub = dsSub.Tables(0)

                    Dim InValue As String = ""

                    For Each dtSub As DataRow In dtPreaAdviceSub.Rows

                        If InValue = "" Then
                            InValue = "'" + dtSub.Item("EQPMNT_NO").ToString() + "'"
                        Else
                            InValue = InValue + "," + "'" + dtSub.Item("EQPMNT_NO").ToString() + "'"
                        End If
                    Next


                    'Check Mailed or Not --Check MAil_send='yes' Condition
                    query = " select EQPMNT_NO  from [V_REPAIR_ESTIMATE] where [MAIL_SEND] ='YES' and  [EQPMNT_NO] in (" + InValue + ")"

                    Dim dsSub1 As DataSet = conn.connection(query)
                    'conn.connection(query).Fill(dsSub1)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds
                    Dim dtPreaAdviceSub1 As DataTable
                    dtPreaAdviceSub1 = dsSub1.Tables(0)

                    InValue = ""

                    For Each dtSub1 As DataRow In dtPreaAdviceSub1.Rows

                        If InValue = "" Then
                            InValue = "'" + dtSub1.Item("EQPMNT_NO").ToString() + "'"
                        Else
                            InValue = InValue + "," + "'" + dtSub1.Item("EQPMNT_NO").ToString() + "'"
                        End If
                    Next

                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 and [EQPMNT_NO] in (" + InValue + ")"

                Else
                    EQPMNT_STTS_CD = "AUR"
                    query = "select a.*,b.ACTVTY_DT  as CHK_ACTVTY_DT from (SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AUR' AND ACTV_BT = 1) as a inner join  ( select * from repair_estimate WHERE ACTVTY_NM='Repair Estimate')as b on a.EQPMNT_NO=b.EQPMNT_NO"
                End If

            ElseIf PageName = "Survey Completion" Then

                If Mode = "new" Then
                    EQPMNT_STTS_CD = "ASR"
                    'query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ASR' AND ACTVTY_NAM = 'Repair Completion' AND ACTV_BT = 1"
                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ASR'  AND ACTV_BT = 1"

                Else

                    EQPMNT_STTS_CD = "SRV"
                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'SRV' AND ACTV_BT = 1 AND ACTVTY_NAM = 'Survey Completion'"
                End If
            End If

            'query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AWE' AND ACTV_BT = 1"
            Dim ds As DataSet = conn.connection(query)
            'conn.connection(query).Fill(ds)
            'ds.Tables(Table)
            'ds.Tables()
            'Return ds
            Dim dtPreaAdvice As DataTable
            dtPreaAdvice = ds.Tables(0)

            Dim arraylist As New ArrayList
            Dim SubArrayList As New ArrayList
            Dim objRepairEstimate As New RepairEstimate
            Dim dsRepairEstimate As New RepairEstimateDataSet
            Dim objCommonConfig As New ConfigSetting()
            Dim str_061KeyValue As String
            Dim bln_061Key As Boolean
            bln_061Key = objCommonConfig.IsKeyExists
            str_061KeyValue = objCommonConfig.pub_GetConfigSingleValue("061", intDepotID)
            Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
            Dim dtEqipmentInformation As New DataTable



            For Each dt As DataRow In dtPreaAdvice.Rows


                Dim RepairEstimateMobileModel As New RepairEstimateMobileModel

                RepairEstimateMobileModel.EquipmentType_Id = dt.Item("EQPMNT_TYP_ID").ToString()
                RepairEstimateMobileModel.EquipmentType_Cd = dt.Item("EQPMNT_TYP_CD").ToString()

                RepairEstimateMobileModel.EquipmentNo = dt.Item("EQPMNT_NO").ToString()
                RepairEstimateMobileModel.CSTMR_ID = dt.Item("CSTMR_ID").ToString()
                RepairEstimateMobileModel.Customer = dt.Item("CSTMR_CD").ToString()
                RepairEstimateMobileModel.InDate = dt.Item("GTN_DT").ToString()
                RepairEstimateMobileModel.PreviousCargo = dt.Item("PRDCT_DSCRPTN_VC").ToString()
                RepairEstimateMobileModel.LastStatusDate = dt.Item("ACTVTY_DT").ToString()
                RepairEstimateMobileModel.Remarks = dt.Item("RMRKS_VC").ToString()
                RepairEstimateMobileModel.GiTransactionNo = dt.Item("GI_TRNSCTN_NO").ToString()
                RepairEstimateMobileModel.EquipmentStatusId = dt.Item("EQPMNT_STTS_ID").ToString()
                RepairEstimateMobileModel.EquipmentStatusCd = dt.Item("EQPMNT_STTS_CD").ToString()
                RepairEstimateMobileModel.CurencyCD = dt.Item("CRRNCY_CD").ToString()
                If PageName = "Repair Approval" And Mode = "edit" Then
                    RepairEstimateMobileModel.ActivityDate = dt.Item("CHK_ACTVTY_DT").ToString()
                End If

                Dim strLaborRate As String = ""
                strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByCustomerID(CInt(objCommon.GetHeadQuarterID()), dt.Item(RepairEstimateData.CSTMR_ID))
                If strLaborRate Is Nothing Then
                    RepairEstimateMobileModel.LaborRate = ""
                Else
                    RepairEstimateMobileModel.LaborRate = strLaborRate
                End If

                dsRepairEstimate = objRepairEstimate.pub_GetEquipmentInformationByEqpmntNo(intDepotID, dt.Item("EQPMNT_NO").ToString)
                dtEqipmentInformation = dsRepairEstimate.Tables(RepairEstimateData._V_EQUIPMENT_INFORMATION).Copy()
                If dtEqipmentInformation.Rows.Count > 0 Then
                    If str_061KeyValue.ToLower = "false" Then
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT).ToString = "" Then
                            RepairEstimateMobileModel.LastTestDate = ""
                        Else
                            If CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT)) = sqlDbnull Then
                                RepairEstimateMobileModel.LastTestDate = ""
                            Else
                                RepairEstimateMobileModel.LastTestDate = CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT)).ToString("dd-MMM-yyyy")
                            End If
                        End If
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT).ToString = "" Then
                            RepairEstimateMobileModel.NextTestDate = ""
                        Else
                            If CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT)) = sqlDbnull Then
                                RepairEstimateMobileModel.NextTestDate = ""
                            Else
                                RepairEstimateMobileModel.NextTestDate = CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT)).ToString("dd-MMM-yyyy")
                            End If
                        End If
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_TYP_ID) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_TYP_ID).ToString = "" Then
                            RepairEstimateMobileModel.LastTestType = ""
                        Else
                            RepairEstimateMobileModel.LastTestType = dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_TYP_ID).ToString()
                        End If
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_SRVYR_NM) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_SRVYR_NM).ToString = "" Then
                            RepairEstimateMobileModel.LastSurveyor = ""
                        Else
                            RepairEstimateMobileModel.LastSurveyor = dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_SRVYR_NM).ToString()
                        End If
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_TYP_ID) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_TYP_ID).ToString = "" Then
                            RepairEstimateMobileModel.NextTestType = ""
                        Else
                            RepairEstimateMobileModel.NextTestType = dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_TYP_CD)

                        End If
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.VLDTY_PRD_TST_YRS) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.VLDTY_PRD_TST_YRS).ToString = "" Then
                            RepairEstimateMobileModel.ValidityPeriodforTest = ""
                        Else
                            RepairEstimateMobileModel.ValidityPeriodforTest = dtEqipmentInformation.Rows(0).Item(RepairEstimateData.VLDTY_PRD_TST_YRS)
                        End If
                    End If
                End If


                Dim arrayLineItem As New ArrayList

                If PageName = "Repair Estimate" Then

                    If Mode = "new" Then
                        RepairEstimateMobileModel.RepairTypeCD = ""
                        RepairEstimateMobileModel.RepairTypeID = ""
                        RepairEstimateMobileModel.InvoicingPartyID = ""
                        RepairEstimateMobileModel.InvoicingPartyCD = ""
                        RepairEstimateMobileModel.InvoicingPartyName = ""
                        RepairEstimateMobileModel.RevisionNo = ""

                        RepairEstimateMobileModel.RepairEstimateID = ""
                        RepairEstimateMobileModel.CustomerAppRef = ""
                        RepairEstimateMobileModel.ApprovalDate = ""
                        RepairEstimateMobileModel.PartyAppRef = ""
                        RepairEstimateMobileModel.SurveyorName = ""
                        RepairEstimateMobileModel.SurveyCompletionDate = ""

                        RepairEstimateMobileModel.LineItems = arrayLineItem



                    ElseIf Mode = "edit" Then

                        RepairEstimateMobileModel = ReturnValueComman(9, intDepotID, dt.Item("GI_TRNSCTN_NO"), dt.Item("EQPMNT_NO"), RepairEstimateMobileModel)


                    End If

                ElseIf PageName = "Repair Approval" Then


                    If Mode = "new" Then

                        RepairEstimateMobileModel = ReturnValueComman(9, intDepotID, dt.Item("GI_TRNSCTN_NO"), dt.Item("EQPMNT_NO"), RepairEstimateMobileModel)

                    Else

                        RepairEstimateMobileModel = ReturnValueComman(10, intDepotID, dt.Item("GI_TRNSCTN_NO"), dt.Item("EQPMNT_NO"), RepairEstimateMobileModel)

                    End If



                ElseIf PageName = "Survey Completion" Then


                    If Mode = "new" Then

                        RepairEstimateMobileModel = ReturnValueComman(10, intDepotID, dt.Item("GI_TRNSCTN_NO"), dt.Item("EQPMNT_NO"), RepairEstimateMobileModel)

                    Else

                        RepairEstimateMobileModel = ReturnValueComman(14, intDepotID, dt.Item("GI_TRNSCTN_NO"), dt.Item("EQPMNT_NO"), RepairEstimateMobileModel)

                    End If
                End If



                Dim dtAttachment As New DataTable
                Dim objGatein As New GateIns
                Dim objTrans As New Transactions
                Dim ouput As New GateinDataSet
                Dim ss As String = RepairEstimateMobileModel.RepairEstimateID.ToString()

                If Mode = "new" And PageName = "Repair Approval" Then
                    dsRepairCompletion.Tables(GateinData._ATTACHMENT).Clear()
                    dtAttachment = dsRepairCompletion.Tables(GateinData._ATTACHMENT).Clone()
                    If dsRepairCompletion.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                        ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                        dtAttachment = objGatein.GetAttachmentByRepairEstimateId(intDepotID, "Repair Estimate", CLng(RepairEstimateMobileModel.RepairEstimateID), objTrans).Tables(GateinData._ATTACHMENT)
                        dsRepairCompletion.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                    End If



                ElseIf Mode = "edit" Then





                    ouput.Tables(GateinData._ATTACHMENT).Clear()
                    dtAttachment = ouput.Tables(GateinData._ATTACHMENT).Clone()
                    If ouput.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                        ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                        dtAttachment = objGatein.GetAttachmentByRepairEstimateId(intDepotID, PageName, CLng(RepairEstimateMobileModel.RepairEstimateID), objTrans).Tables(GateinData._ATTACHMENT)
                        ouput.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                        'End If
                    End If
                End If
                'Dim attcj1 As New attchementPro
                'attcj1.attchPath = ""
                'attcj1.fileName = ""

                'attcj1.imageUrl = ""

                Dim attch1 As New ArrayList
                Dim attch As New ArrayList
                'attch1.Add(attcj1)
                If Mode = "new" And PageName = "Repair Approval" Then
                    For Each dr As DataRow In dsRepairCompletion.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", RepairEstimateMobileModel.RepairEstimateID, " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))
                        Dim attcj As New attchementPro
                        attcj.attchPath = dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString
                        attcj.fileName = dr.Item(RepairEstimateData.ACTL_FL_NM).ToString
                        attcj.attchId = dr.Item(RepairEstimateData.ATTCHMNT_ID).ToString
                        Dim currentUrl As String = HttpContext.Current.Request.Url.Host
                        'attcj.imageUrl = "http://192.18.0.12/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                        Dim Attachment_URL As String = Config.pub_GetAppConfigValue("AttachmentURL")
                        attcj.imageUrl = Attachment_URL + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                        'h.imageUrl = "http://"+ currentUrl +"/iTankDepoUI/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                        'sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(RepairEstimateData.ATTCHMNT_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                        'sbAttachment.Append(String.Concat("<a target='_blank' href='../download.ashx?FL_NM=", strFileName, "'  title='Click to download error file' >", strFileName, "</a>"))
                        'sbAttachment.Append(String.Concat("<a target='_blank' href='//", strPath, "?FL_NM=", strPhysicalpath, "'  title='Click to download file' >", strFileName, "</a>"))
                        'sbAttachment.Append("<br />")
                        attch.Add(attcj)
                    Next
                End If

                If Mode = "edit" Then
                    For Each dr As DataRow In ouput.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", RepairEstimateMobileModel.RepairEstimateID, " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))
                        Dim attcj As New attchementPro
                        attcj.attchPath = dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString
                        attcj.fileName = dr.Item(RepairEstimateData.ACTL_FL_NM).ToString
                        attcj.attchId = dr.Item(RepairEstimateData.ATTCHMNT_ID).ToString
                        Dim currentUrl As String = HttpContext.Current.Request.Url.Host
                        'attcj.imageUrl = "http://192.18.0.12/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                        Dim Attachment_URL As String = Config.pub_GetAppConfigValue("AttachmentURL")
                        attcj.imageUrl = Attachment_URL + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                        'h.imageUrl = "http://"+ currentUrl +"/iTankDepoUI/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                        'sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(RepairEstimateData.ATTCHMNT_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                        'sbAttachment.Append(String.Concat("<a target='_blank' href='../download.ashx?FL_NM=", strFileName, "'  title='Click to download error file' >", strFileName, "</a>"))
                        'sbAttachment.Append(String.Concat("<a target='_blank' href='//", strPath, "?FL_NM=", strPhysicalpath, "'  title='Click to download file' >", strFileName, "</a>"))
                        'sbAttachment.Append("<br />")
                        attch.Add(attcj)
                    Next
                End If
                If attch.Count > 0 Then
                    RepairEstimateMobileModel.attchement = attch
                Else
                    RepairEstimateMobileModel.attchement = attch1
                End If



                Response.Add(RepairEstimateMobileModel)



            Next

            ArrayOfRepairEstimateMobileModel.Response = Response


            ArrayOfRepairEstimateMobileModel.Status = "Success"

            Return ArrayOfRepairEstimateMobileModel


        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
            ArrayOfRepairEstimateMobileModel.Status = ex.Message

            Return ArrayOfRepairEstimateMobileModel

        End Try

    End Function

    'Common Function for Repair -Mysubmit Functions&Pending List Also
    Public Function ReturnValueComman(ByVal EQPMNT_STTS_ID As String, ByVal intDepotID As String, ByVal GI_TRNSCTN_NO As String, ByVal EQPMNT_NO As String, ByVal RepairEstimateMobileModel As RepairEstimateMobileModel) As RepairEstimateMobileModel



        Dim query1 As String = "select RPR_ESTMT_NO,OWNR_APPRVL_RF,ACTVTY_DT,PRTY_APPRVL_RF,SRVYR_NM,RPR_ESTMT_ID,RVSN_NO,RPR_TYP_ID,RPR_TYP_CD,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM from [V_REPAIR_ESTIMATE] where EQPMNT_NO ='" + EQPMNT_NO + "' and EQPMNT_STTS_ID ='" + EQPMNT_STTS_ID + "' and DPT_ID='" + intDepotID + "' and GI_TRNSCTN_NO ='" + GI_TRNSCTN_NO + "'"
        Dim ds1 As DataSet = conn.connection(query1)
        'conn.connection(query1).Fill(ds1)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice1 As DataTable
        dtPreaAdvice1 = ds1.Tables(0)


        If ds1.Tables.Count > 0 Then

            RepairEstimateMobileModel.RepairTypeCD = dtPreaAdvice1.Rows(0).Item("RPR_TYP_CD").ToString()
            RepairEstimateMobileModel.RepairTypeID = dtPreaAdvice1.Rows(0).Item("RPR_TYP_ID").ToString()
            RepairEstimateMobileModel.InvoicingPartyID = dtPreaAdvice1.Rows(0).Item("INVCNG_PRTY_ID").ToString()
            RepairEstimateMobileModel.InvoicingPartyCD = dtPreaAdvice1.Rows(0).Item("INVCNG_PRTY_CD").ToString()
            RepairEstimateMobileModel.InvoicingPartyName = dtPreaAdvice1.Rows(0).Item("INVCNG_PRTY_NM").ToString()
            RepairEstimateMobileModel.RevisionNo = dtPreaAdvice1.Rows(0).Item("RVSN_NO").ToString()

            RepairEstimateMobileModel.RepairEstimateID = dtPreaAdvice1.Rows(0).Item("RPR_ESTMT_ID").ToString()
            RepairEstimateMobileModel.CustomerAppRef = dtPreaAdvice1.Rows(0).Item("OWNR_APPRVL_RF").ToString()
            RepairEstimateMobileModel.ApprovalDate = dtPreaAdvice1.Rows(0).Item("ACTVTY_DT").ToString()
            RepairEstimateMobileModel.PartyAppRef = dtPreaAdvice1.Rows(0).Item("PRTY_APPRVL_RF").ToString()
            RepairEstimateMobileModel.SurveyorName = dtPreaAdvice1.Rows(0).Item("SRVYR_NM").ToString()
            RepairEstimateMobileModel.SurveyCompletionDate = dtPreaAdvice1.Rows(0).Item("ACTVTY_DT").ToString()
            RepairEstimateMobileModel.RepairEstimateNo = dtPreaAdvice1.Rows(0).Item("RPR_ESTMT_NO").ToString()

        End If
        'Dim query2 As String = "Select EQPMNT_NO,RPR_ESTMT_DTL_ID,RPR_ESTMT_ID,RPR_ID,RPR_CD,RPR_DSCRPTN_VC,DMG_ID,DMG_CD,DMG_DSCRPTN_VC,LBR_RT,LBR_HRS,LBR_HR_CST_NC,MTRL_CST_NC,RSPNSBLTY_ID,RSPNSBLTY_CD,DMG_RPR_DSCRPTN,TTL_CST_NC,ITM_ID,ITM_CD,SB_ITM_ID,SB_ITM_CD,CHK_BT,MTRL_CD,CHNG_BIT from [V_REPAIR_ESTIMATE_DETAIL] where RPR_ESTMT_ID ='" + dtPreaAdvice1.Rows(0).Item("RPR_ESTMT_ID").ToString() + "'"
        Dim query2 As String = "select a.*,b.CRRNCY_CD from  ( SELECT RPR_ESTMT_DTL_ID,       RPR_ESTMT_ID,         DMG_ID,                  (SELECT DMG_CD FROM DAMAGE WHERE DMG_ID=RED.DMG_ID AND DPT_ID =  (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))AS DMG_CD,   (SELECT DMG_DSCRPTN_VC FROM DAMAGE WHERE DMG_ID=RED.DMG_ID AND DPT_ID =  (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))AS DMG_DSCRPTN_VC, LBR_RT,                  LBR_HRS,                  LBR_HR_CST_NC,                  MTRL_CST_NC,                  RSPNSBLTY_ID,                  (SELECT CASE WHEN RSPNSBLTY_ID='66' THEN 'C' WHEN RSPNSBLTY_ID=67 THEN 'I' ELSE (SELECT RSPNSBLTY_CD FROM RESPONSIBILITY WHERE RSPNSBLTY_ID=RED.RSPNSBLTY_ID) END) AS RSPNSBLTY_CD,   (SELECT RSPNSBLTY_DSCRPTN_VC FROM RESPONSIBILITY WHERE RSPNSBLTY_ID=RED.RSPNSBLTY_ID)AS RSPNSBLTY_DSCRPTN_VC,              DMG_RPR_DSCRPTN,           CHK_BT ,                TTL_CST_NC,         RPR_ID,    MTRL_CD,TX_RSPNSBLTY_ID,RMRKS,(SELECT RPR_CD FROM REPAIR WHERE RPR_ID=RED.RPR_ID AND DPT_ID =  (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153)) AS RPR_CD,  (SELECT RPR_DSCRPTN_VC FROM REPAIR WHERE RPR_ID=RED.RPR_ID AND DPT_ID =  (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153)) AS RPR_DSCRPTN_VC,  (SELECT CASE WHEN (SELECT RPR_CD FROM REPAIR WHERE RPR_ID=RED.RPR_ID AND DPT_ID =  (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))='CC' THEN RED.LBR_HR_CST_NC ELSE 0 END) AS TTL_CLN_CST,          (SELECT EQPMNT_NO FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID=RED.RPR_ESTMT_ID AND DPT_ID = (SELECT DPT_ID FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID = RED.RPR_ESTMT_ID))AS EQPMNT_NO,ITM_ID,        (SELECT ITM_CD FROM ITEM WHERE ITM_ID=RED.ITM_ID AND DPT_ID =  (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153)) AS ITM_CD,        SB_ITM_ID,        (SELECT SB_ITM_CD FROM SUB_ITEM WHERE SB_ITM_ID=RED.SB_ITM_ID ) AS SB_ITM_CD ,     (SELECT DPT_ID FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID = RED.RPR_ESTMT_ID) AS DPT_ID ,(SELECT CSTMR_ID FROM CUSTOMER WHERE CSTMR_ID = (SELECT CSTMR_ID FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID= RED.RPR_ESTMT_ID AND RSPNSBLTY_ID= 66)) AS CSTMR_ID,(SELECT CSTMR_CRRNCY_ID FROM CUSTOMER WHERE CSTMR_ID = (SELECT CSTMR_ID FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID= RED.RPR_ESTMT_ID AND RSPNSBLTY_ID= 66)) AS CSTMR_CRRNCY_ID,(SELECT INVCNG_PRTY_ID FROM INVOICING_PARTY WHERE INVCNG_PRTY_ID = (SELECT INVCNG_PRTY_ID FROM REPAIR_ESTIMATE WHERE RPR_ESTMT_ID= RED.RPR_ESTMT_ID AND RSPNSBLTY_ID <>66)) AS INVCNG_PRTY_ID ,(SELECT SRVC_PRTNR_TYP_CD FROM SERVICE_PARTNER WHERE SRVC_PRTNR_ACTL_ID = RED.RSPNSBLTY_ID) AS SRVC_PRTNR_TYP_CD,TRFF_CD_DTL_ID,CHNG_BIT FROM         dbo.REPAIR_ESTIMATE_DETAIL RED)  as a inner join currency b on a.CSTMR_CRRNCY_ID=b.CRRNCY_ID where RPR_ESTMT_ID ='" + dtPreaAdvice1.Rows(0).Item("RPR_ESTMT_ID").ToString() + "'"
        Dim ds2 As DataSet = conn.connection(query2)
        'conn.connection(query2).Fill(ds2)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice2 As DataTable
        dtPreaAdvice2 = ds2.Tables(0)

        Dim arrayLineItem As New ArrayList

        For Each dt1 As DataRow In dtPreaAdvice2.Rows

            Dim LineItem1 As New LineItem

            LineItem1.RPR_ESTMT_DTL_ID = dt1.Item("RPR_ESTMT_DTL_ID").ToString()
            LineItem1.RPR_ESTMT_ID = dt1.Item("RPR_ESTMT_ID").ToString()
            LineItem1.Item = dt1.Item("ITM_ID").ToString()
            LineItem1.ItemCd = dt1.Item("ITM_CD").ToString()
            LineItem1.SubItem = dt1.Item("SB_ITM_ID").ToString()
            LineItem1.SubItemCd = dt1.Item("SB_ITM_CD").ToString()
            LineItem1.Damage = dt1.Item("DMG_ID").ToString()
            LineItem1.DamageCd = dt1.Item("DMG_CD").ToString()
            LineItem1.Repair = dt1.Item("RPR_ID").ToString()
            LineItem1.RepairCd = dt1.Item("RPR_CD").ToString()
            LineItem1.ManHour = dt1.Item("LBR_HRS").ToString()
            LineItem1.DmgRprRemarks = dt1.Item("DMG_RPR_DSCRPTN").ToString()
            LineItem1.MHR = dt1.Item("LBR_RT").ToString()
            LineItem1.MHC = dt1.Item("LBR_HR_CST_NC").ToString()
            LineItem1.MC = dt1.Item("MTRL_CST_NC").ToString()
            LineItem1.TC = dt1.Item("TTL_CST_NC").ToString()
            LineItem1.Responsibility = dt1.Item("RSPNSBLTY_ID").ToString()
            LineItem1.ResponsibilityCd = dt1.Item("RSPNSBLTY_CD").ToString()
            LineItem1.DamageDescription = dt1.Item("DMG_RPR_DSCRPTN").ToString()
            LineItem1.RepairDescription = dt1.Item("RPR_DSCRPTN_VC").ToString()
            LineItem1.CheckButton = dt1.Item("CHK_BT").ToString()
            LineItem1.ChangingBit = dt1.Item("CHNG_BIT").ToString()
            LineItem1.CURRENCY_CD = dt1.Item("CRRNCY_CD").ToString()


            arrayLineItem.Add(LineItem1)




        Next


        RepairEstimateMobileModel.LineItems = arrayLineItem

        Return RepairEstimateMobileModel

    End Function

    'Sumbit Function
    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function RepairUpdate(UserName As String, ByVal EquipmentNo As String, ByVal GITransactionNo As String, ByVal LineItem As ArrayOfLineItems,
                                 ByVal SummaryDetail As ArrayOfSummaryDetail, ByVal hfc As ArrayOfFileParams, ByVal Attchment As String, ByVal PageName As String,
                                 ByVal bv_strRepairEstimateId As String, _
                                         ByVal bv_strCustomerID As String, _
                                         ByVal bv_strCustomerCode As String, _
                                         ByVal bv_strEstimateDate As String, _
                                         ByVal bv_strOrginalEstimateDate As String, _
                                         ByVal bv_strStatusID As String, _
                                         ByVal bv_strStatusCode As String, _
                                         ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_strEIRNo As String, _
                                         ByVal bv_strGateInTransaction As String, _
                                         ByVal bv_strLastTestDate As String, _
                                         ByVal bv_strLastTestTypeID As String, _
                                         ByVal bv_strLastTestTypeCode As String, _
                                         ByVal bv_strValidityYear As String, _
                                         ByVal bv_strNextTestDate As String, _
                                         ByVal bv_strLastSurveyor As String, _
                                         ByVal bv_strNextTestTypeID As String, _
                                         ByVal bv_strNextTestTypeCode As String, _
                                         ByVal bv_strRepairTypeID As String, _
                                         ByVal bv_strRepairTypeCode As String, _
                                         ByVal bv_strCertOfCleanlinessBit As String, _
                                         ByVal bv_strInvoicingPartyID As String, _
                                         ByVal bv_strInvoicingPartyCode As String, _
                                         ByVal bv_strLaborRate As String, _
                                         ByVal bv_strApprovalAmount As String, _
                                         ByVal bv_strApprovalDate As String, _
                                         ByVal bv_strApprovalRef As String, _
                                         ByVal bv_strSurveyDate As String, _
                                         ByVal bv_strSurveyName As String, _
                                         ByVal bv_strWFData As String, _
                                         ByVal bv_strMode As String, _
                                         ByVal bv_strEstimateId As String, _
                                         ByVal bv_strRevisionNo As String, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strEstimationNo As String, _
                                         ByVal bv_strCustomerEstimatedCost As String, _
                                         ByVal bv_strCustomerApprovedCost As String, _
                                         ByVal bv_strPartyApprovalRef As String, _
                                         ByVal bv_intActivityId As Integer) As RepairEstimateDataSetUpdate
        'Try

        Dim objRepairEstimate As New RepairEstimate
        Dim dtEquipmentDetail As New DataTable
        Dim dsRepairEstimate As New RepairEstimateDataSet


        gateInMobile.DepotID(UserName)
        dtEquipmentDetail = objRepairEstimate.pub_GetGetActivityStatusByEqpmntNo(CommonWeb.iInt(objCommon.GetDepotID), EquipmentNo, GITransactionNo).Tables(RepairEstimateData._V_ACTIVITY_STATUS)
        dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Merge(dtEquipmentDetail)

        dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Clear()
        Dim drNew As DataRow
        Dim deletedIds As String = ""
        Dim modifiedIds As String = ""
        Dim addedIds As String = ""
        For Each dr In LineItem.LineItemList
            'Line Items Insert
            drNew = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).NewRow()
            drNew.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = dr.RPR_ESTMT_DTL_ID
            drNew.Item(RepairEstimateData.RPR_ESTMT_ID) = dr.RPR_ESTMT_ID
            drNew.Item(RepairEstimateData.ITM_ID) = dr.Item
            drNew.Item(RepairEstimateData.ITM_CD) = dr.ItemCd
            drNew.Item(RepairEstimateData.SB_ITM_ID) = dr.SubItem
            drNew.Item(RepairEstimateData.SB_ITM_CD) = dr.SubItemCd
            drNew.Item(RepairEstimateData.DMG_ID) = dr.Damage
            drNew.Item(RepairEstimateData.DMG_CD) = dr.DamageCd
            'drNew.Item(RepairEstimateData.MTRL_ID) = dr.ma
            'drNew.Item(RepairEstimateData.MTRL_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CD)
            drNew.Item(RepairEstimateData.RPR_ID) = dr.Repair
            drNew.Item(RepairEstimateData.RPR_CD) = dr.RepairCd
            drNew.Item(RepairEstimateData.LBR_HRS) = dr.ManHour
            drNew.Item(RepairEstimateData.RSPNSBLTY_CD) = dr.ResponsibilityCd
            drNew.Item(RepairEstimateData.RSPNSBLTY_ID) = dr.Responsibility
            drNew.Item(RepairEstimateData.LBR_RT) = dr.MHR
            drNew.Item(RepairEstimateData.LBR_HR_CST_NC) = dr.MHC
            drNew.Item(RepairEstimateData.MTRL_CST_NC) = dr.MC
            drNew.Item(RepairEstimateData.TTL_CST_NC) = dr.TC
            drNew.Item(RepairEstimateData.DMG_RPR_DSCRPTN) = dr.DmgRprRemarks

            drNew.Item(RepairEstimateData.DMG_DSCRPTN_VC) = dr.DamageDescription
            drNew.Item(RepairEstimateData.RPR_DSCRPTN_VC) = dr.RepairDescription
            drNew.Item(RepairEstimateData.CHK_BT) = dr.CheckButton
            drNew.Item(RepairEstimateData.MTRL_ID) = 0
            drNew.Item(RepairEstimateData.MTRL_CD) = "NULL"
            drNew.Item(RepairEstimateData.RMRKS) = "NULL"
            drNew.Item(RepairEstimateData.TX_RSPNSBLTY_ID) = 0
            drNew.Item(RepairEstimateData.CHNG_BIT) = dr.ChangingBit
            drNew.Item(RepairEstimateData.TRFF_CD_DTL_ID) = 0

            Select Case dr.RowState
                Case "Deleted"
                    If deletedIds <> "" Then
                        deletedIds = deletedIds & ","
                    End If
                    deletedIds = deletedIds & dr.RPR_ESTMT_DTL_ID

                Case "Modified"
                    If modifiedIds <> "" Then
                        modifiedIds = modifiedIds & ","
                    End If
                    modifiedIds = modifiedIds & dr.RPR_ESTMT_DTL_ID

                Case "Added"
                    If addedIds <> "" Then
                        addedIds = addedIds & ","
                    End If
                    addedIds = addedIds & dr.RPR_ESTMT_DTL_ID
            End Select
            

            dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Add(drNew)

        Next

        dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).AcceptChanges()

        If deletedIds <> "" Then
            For Each dr As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairEstimateData.RPR_ESTMT_DTL_ID, " IN (", deletedIds, ")"))
                dr.Delete()
            Next
        End If

        If modifiedIds <> "" Then
            For Each dr As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairEstimateData.RPR_ESTMT_DTL_ID, " IN (", modifiedIds, ")"))
                dr.SetModified()
            Next
        End If

        If addedIds <> "" Then
            For Each dr As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairEstimateData.RPR_ESTMT_DTL_ID, " IN (", addedIds, ")"))
                dr.SetAdded()
            Next
        End If

        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Clear()
        Dim drNew1 As DataRow
        'Summary Details
        For Each dr1 In SummaryDetail.LineSummaryDetail

            drNew1 = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).NewRow()
            drNew1.Item(RepairEstimateData.MN_HR_SMMRY) = dr1.MH
            drNew1.Item(RepairEstimateData.MN_HR_RT_SMMRY) = dr1.MHC
            drNew1.Item(RepairEstimateData.MTRL_CST_SMMRY) = dr1.MC
            drNew1.Item(RepairEstimateData.TTL_CST_SMMRY) = dr1.TC
            drNew1.Item(RepairEstimateData.SMMRY_ID) = dr1.ResponsibiltyID
            drNew1.Item(RepairEstimateData.LBR_RT_SMMRY) = dr1.MHR

            drNew1.Item(RepairEstimateData.MH_CST_SMMRY) = dr1.MHCSTSummary
            drNew1.Item(RepairEstimateData.RSPNSBLTY_CD) = dr1.ResponsibiltyCD

            dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows.Add(drNew1)

        Next


        If Attchment = "True" Then



            dsRepairEstimate = Attachment(dsRepairEstimate, hfc, LineItem.LineItemList(0).RPR_ESTMT_ID)
        End If

        If bv_strMode = "new" Then

            Dim objcommon As New CommonData
            Dim objCommonUI As New CommonUI
            Dim str_037KeyValue As String = String.Empty
            Dim bln_037KeyValue As Boolean = False
            Dim strActivitySubmit As String = String.Empty
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()

            ' Dim PageMode As String = RetrieveData(REPAIR_ESTIMATEMODE).ToString


            'If Not dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
            '    pub_SetCallbackStatus(False)
            '    pub_SetCallbackError("At least one line detail is mandatory for an Estimate.")
            '    Exit Function
            'End If
            Dim blnMode As Boolean = False
            ' Dim objRepairEstimate As New RepairEstimate
            Dim lngCreated As Long
            Dim bv_strEIRNumber As String = ""
            Dim datGateinDate As DateTime
            If dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows.Count > 0 Then
                datGateinDate = CDate(dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.GTN_DT))
            End If
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            str_032KeyValue = objCommonConfig.pub_GetConfigSingleValue("032", intDepotID)
            bln_032EqType_Key = objCommonConfig.IsKeyExists
            If bln_032EqType_Key And bv_strRepairTypeID = Nothing Then
                bv_strRepairTypeID = objcommon.GetEnumID("REPAIR TYPE", str_032KeyValue)
            End If
            'REPAIR FLOW SETTING: 037
            'CR-001 (AWE_TO_AWP)
            str_037KeyValue = objcommon.GetConfigSetting("037", bln_037KeyValue)
            If bln_037KeyValue = False Then
                str_037KeyValue = String.Empty
            End If

            'GWS
            Dim str_063GWS As String
            Dim bln_063GWSKey As Boolean
            str_057GWS = objCommonConfig.pub_GetConfigSingleValue("057", intDepotID)
            bln_057GWSActive_Key = objCommonConfig.IsKeyExists
            bln_063GWSKey = objCommonConfig.IsKeyExists
            If bln_063GWSKey Then
                str_063GWS = objCommonConfig.pub_GetConfigSingleValue("063", intDepotID)
            Else
                str_063GWS = "false"
            End If
            lngCreated = objRepairEstimate.pub_CreateRepairEstimate(CommonWeb.iLng(bv_strCustomerID),
                                                                    bv_strCustomerCode, _
                                                                    CommonWeb.iDat(bv_strEstimateDate), _
                                                                    CommonWeb.iDat(bv_strOrginalEstimateDate), _
                                                                    datGateinDate, _
                                                                    bv_strEquipmentNo, _
                                                                    CommonWeb.iLng(bv_strStatusID), _
                                                                    bv_strStatusCode, _
                                                                    bv_strEIRNo, _
                                                                    CommonWeb.iDat(bv_strLastTestDate), _
                                                                    CommonWeb.iLng(bv_strLastTestTypeID), _
                                                                    bv_strLastTestTypeCode, _
                                                                    bv_strValidityYear, _
                                                                    CommonWeb.iDat(bv_strNextTestDate), _
                                                                    bv_strLastSurveyor, _
                                                                    CommonWeb.iLng(bv_strNextTestTypeID), _
                                                                    bv_strNextTestTypeCode, _
                                                                    CommonWeb.iLng(bv_strRepairTypeID), _
                                                                    bv_strRepairTypeCode, _
                                                                    CBool(bv_strCertOfCleanlinessBit), _
                                                                    CommonWeb.iLng(bv_strInvoicingPartyID), _
                                                                    bv_strInvoicingPartyCode, _
                                                                    CommonWeb.iDec(bv_strLaborRate), _
                                                                    CommonWeb.iDat(bv_strApprovalDate), _
                                                                    bv_strApprovalRef, _
                                                                    CommonWeb.iDat(bv_strSurveyDate), _
                                                                    bv_strSurveyName, _
                                                                    CommonWeb.iInt(objcommon.GetDepotID()), _
                                                                    bv_strWFData, _
                                                                    bv_strMode, _
                                                                    bv_strEstimateId, _
                                                                    bv_strRevisionNo, _
                                                                    bv_strEstimationNo, _
                                                                    bv_strRemarks, _
                                                                    dsRepairEstimate, _
                                                                    bv_strEIRNumber, _
                                                                    PageName, _
                                                                    bv_strApprovalRef, _
                                                                    CDec(bv_strCustomerEstimatedCost), _
                                                                    CDec(bv_strCustomerApprovedCost), _
                                                                    strModifiedby, _
                                                                    CDate(datModifiedDate), _
                                                                    strModifiedby, _
                                                                    CDate(datModifiedDate), _
                                                                    str_037KeyValue, _
                                                                    bv_strPartyApprovalRef, _
                                                                    strActivitySubmit, _
                                                                    bv_intActivityId, _
                                                                    str_057GWS, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    str_063GWS, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    "False", _
                                                                    Nothing, _
                                                                    Nothing)



        ElseIf bv_strMode = "edit" Then

            Dim objcommon As New CommonData
            Dim objCommonUI As New CommonUI
            Dim str_037KeyValue As String = String.Empty
            Dim bln_037KeyValue As Boolean = False
            Dim strActivitySubmit As String = String.Empty
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()


            Dim blnMode As Boolean = False
            '

            Dim bv_strEIRNumber As String = ""



            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            str_057GWS = objCommonConfig.pub_GetConfigSingleValue("057", intDepotID)
            bln_057GWSActive_Key = objCommonConfig.IsKeyExists
            'REPAIR FLOW SETTING: 037
            'CR-001 (AWE_TO_AWP)
            str_037KeyValue = objcommon.GetConfigSetting("037", bln_037KeyValue)
            If bln_037KeyValue = False Then
                str_037KeyValue = String.Empty
            End If

            Dim lngCreated As Long
            lngCreated = objRepairEstimate.pub_ModifyRepairEstimate(CommonWeb.iLng(bv_strRepairEstimateId), _
                                                                   CommonWeb.iLng(bv_strCustomerID),
                                                                   bv_strCustomerCode, _
                                                                   CommonWeb.iDat(bv_strEstimateDate), _
                                                                   CommonWeb.iDat(bv_strOrginalEstimateDate), _
                                                                   bv_strEquipmentNo, _
                                                                   CommonWeb.iLng(bv_strStatusID), _
                                                                   bv_strStatusCode, _
                                                                   bv_strEIRNo, _
                                                                   bv_strGateInTransaction, _
                                                                   CommonWeb.iDat(bv_strLastTestDate), _
                                                                   CommonWeb.iLng(bv_strLastTestTypeID), _
                                                                   bv_strLastTestTypeCode, _
                                                                   bv_strValidityYear, _
                                                                   CommonWeb.iDat(bv_strNextTestDate), _
                                                                   bv_strLastSurveyor, _
                                                                   CommonWeb.iLng(bv_strNextTestTypeID), _
                                                                   bv_strNextTestTypeCode, _
                                                                   CommonWeb.iLng(bv_strRepairTypeID), _
                                                                   bv_strRepairTypeCode, _
                                                                   CBool(bv_strCertOfCleanlinessBit), _
                                                                   CommonWeb.iLng(bv_strInvoicingPartyID), _
                                                                   bv_strInvoicingPartyCode, _
                                                                   CommonWeb.iDec(bv_strLaborRate), _
                                                                   CommonWeb.iDec(bv_strApprovalAmount), _
                                                                   CommonWeb.iDat(bv_strApprovalDate), _
                                                                   bv_strApprovalRef, _
                                                                   bv_strPartyApprovalRef, _
                                                                   CommonWeb.iDat(bv_strSurveyDate), _
                                                                   bv_strSurveyName, _
                                                                   PageName, _
                                                                   CommonWeb.iInt(objcommon.GetDepotID()), _
                                                                   bv_strWFData, _
                                                                   bv_strMode, _
                                                                   bv_strEstimateId, _
                                                                   bv_strRevisionNo, _
                                                                   bv_strRemarks, _
                                                                   bv_strEstimationNo, _
                                                                   CDec(bv_strCustomerEstimatedCost), _
                                                                   CDec(bv_strCustomerApprovedCost), _
                                                                   strModifiedby, _
                                                                   CDate(datModifiedDate), _
                                                                   str_037KeyValue, _
                                                                   dsRepairEstimate, _
                                                                   strActivitySubmit, _
                                                                   bv_intActivityId, _
                                                                   Nothing, _
                                                                   Nothing, _
                                                                   Nothing, _
                                                                   Nothing, _
                                                                   Nothing, _
                                                                   Nothing, _
                                                                   Nothing, _
                                                                   Nothing, _
                                                                   Nothing, _
                                                                   Nothing, _
                                                                   "False", _
                                                                   Nothing, _
                                                                   Nothing, _
                                                                   str_057GWS)




        End If

        updte.RepairUpdate = "Success"
        'updte.statusText = HttpContext.Current.Response.StatusDescription
        'updte.stausCode = HttpContext.Current.Response.SubStatusCode

        Return updte
        'Return dsRepairEstimate
        ' Catch ex As Exception
        'pub_SetCallbackStatus(False)
        ' iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
        '                                  MethodBase.GetCurrentMethod.Name, ex)
        '    '  End Try
        'Catch ex As Exception
        '    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        '    updte.equipmentUpdate = ex.Message
        '    updte.statusText = HttpContext.Current.Response.StatusDescription
        '    updte.stausCode = HttpContext.Current.Response.StatusCode

        '    Return updte
        'End Try

    End Function


    'Attachmet List
    'Public Function Attachment(ByVal dsRepairEstimate As RepairEstimateDataSet, ByVal hfc As ArrayOfFileParams, ByVal RepairEstimateId As String) As RepairEstimateDataSet

    '    Dim objCommon As New CommonUI
    '    Dim objCommonData As New CommonData
    '    Dim objRepairEstimate As New RepairEstimate
    '    Dim strSize As String = ConfigurationManager.AppSettings("UploadPhotoSize")
    '    Dim strPhotoLength As String = ConfigurationManager.AppSettings("UploadPhotoFileLength")

    '    Dim intDepotId As Integer
    '    Dim strModifiedBy As String
    '    Dim strVirtualPath As String = ""
    '    Dim uploadFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString
    '    Dim drAttachment As DataRow
    '    Dim strFilename As String = ""
    '    Dim strExtension As String = ""
    '    Dim strClientFileName As String = ""
    '    intDepotId = objCommonData.GetDepotID()
    '    strModifiedBy = objCommonData.GetCurrentUserName()
    '    'Dim strRepairEstimateId As String = RetrieveData("RepairEstimateId").ToString
    '    Dim strRepairEstimateId As String = RepairEstimateId
    '    Dim lngMaxSize As Long = CLng(strSize)
    '    lngMaxSize = lngMaxSize / 1024000


    '    For i As Integer = 0 To hfc.ArrayOfFileParams.Count - 1
    '        Dim hpf As FileParams = hfc.ArrayOfFileParams(i)
    '        If hpf.ContentLength > 0 Then
    '            Dim lngFileSize As Long
    '            Dim sbPath As New StringBuilder
    '            strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
    '            ' strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
    '            strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
    '            lngFileSize = hpf.ContentLength

    '            drAttachment = dsRepairEstimate.Tables(RepairEstimateData._ATTACHMENT).NewRow()
    '            drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)
    '            If Not IsDBNull(strRepairEstimateId) AndAlso strRepairEstimateId <> String.Empty AndAlso strRepairEstimateId <> " " Then
    '                drAttachment(RepairEstimateData.RPR_ESTMT_ID) = strRepairEstimateId
    '            End If

    '            Dim myMatch As Match = System.Text.RegularExpressions.Regex.Match(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), "^[a-zA-Z0-9 _.-]*$")
    '            If myMatch.Success Then
    '                strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName).Replace("+", ""), Now.ToFileTime)
    '                strFilename = String.Concat(strFilename, strExtension)
    '                strVirtualPath = String.Concat(Config.pub_GetAppConfigValue("UploadAttachPath"), strFilename)
    '                strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
    '                lngFileSize = hpf.ContentLength
    '                If strClientFileName.Length < strPhotoLength Then
    '                    If Not System.IO.Directory.Exists(String.Concat(uploadFolder, "\UpLoad\Attachment")) Then
    '                        System.IO.Directory.CreateDirectory(String.Concat(uploadFolder, "\UpLoad\Attachment"))
    '                    End If
    '                    File.WriteAllBytes(strVirtualPath, Convert.FromBase64String(hpf.base64imageString))
    '                    'hpf.SaveAs(strVirtualPath)
    '                    drAttachment(RepairEstimateData.ATTCHMNT_PTH) = strFilename
    '                    drAttachment(RepairEstimateData.ACTL_FL_NM) = strClientFileName
    '                    drAttachment(RepairEstimateData.MDFD_BY) = strModifiedBy
    '                    drAttachment(RepairEstimateData.MDFD_DT) = CDate(Now)
    '                    drAttachment(RepairEstimateData.DPT_ID) = intDepotId
    '                    drAttachment(RepairEstimateData.ERR_FLG) = False
    '                Else
    '                    drAttachment(RepairEstimateData.ERR_FLG) = True
    '                    drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("Attachment File Name (", strClientFileName, " must be below ", strPhotoLength, "Characters.")
    '                End If
    '            Else
    '                drAttachment(RepairEstimateData.ERR_FLG) = True
    '                drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("File Name (", strClientFileName, ") with special characters are not allowed. ")
    '            End If
    '            dsRepairEstimate.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(drAttachment)

    '            'If bv_strPageName = "GateIn" Then
    '            '    'CacheData(GATE_IN, dsGateInData)
    '            'End If

    '        End If
    '    Next


    '    Return dsRepairEstimate

    'End Function

    ' MySubmitList Tariff Based Items
    Public Function Attachment(ByVal dsRepairEstimate As RepairEstimateDataSet, ByVal hfc As ArrayOfFileParams, ByVal RepairEstimateId As String) As RepairEstimateDataSet




        Dim objCommon As New CommonUI
        Dim objCommonData As New CommonData
        Dim objRepairEstimate As New RepairEstimate
        Dim strSize As String = ConfigurationManager.AppSettings("UploadPhotoSize")
        Dim strPhotoLength As String = ConfigurationManager.AppSettings("UploadPhotoFileLength")

        Dim intDepotId As Integer
        Dim strModifiedBy As String
        Dim strVirtualPath As String = ""
        Dim uploadFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString
        Dim drAttachment As DataRow
        Dim strFilename As String = ""
        Dim strExtension As String = ""
        Dim strClientFileName As String = ""
        Dim actualfleName As String = ""
        intDepotId = objCommonData.GetDepotID()
        strModifiedBy = objCommonData.GetCurrentUserName()
        'Dim strRepairEstimateId As String = RetrieveData("RepairEstimateId").ToString
        Dim strRepairEstimateId As String = RepairEstimateId
        Dim lngMaxSize As Long = CLng(strSize)
        lngMaxSize = lngMaxSize / 1024000


        For i As Integer = 0 To hfc.ArrayOfFileParams.Count - 1
            Dim hpf As FileParams = hfc.ArrayOfFileParams(i)
            drAttachment = dsRepairEstimate.Tables(RepairEstimateData._ATTACHMENT).NewRow()

            Dim lngFileSize As Long
            Dim sbPath As New StringBuilder
            strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
            'actualfleName="FFFFFFFFFFFF.jpg"


            ' strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
            strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
            lngFileSize = hpf.ContentLength


            drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)
            If Not IsDBNull(strRepairEstimateId) AndAlso strRepairEstimateId <> String.Empty AndAlso strRepairEstimateId <> " " Then
                drAttachment(RepairEstimateData.RPR_ESTMT_ID) = strRepairEstimateId
            End If
            If hpf.ContentLength > 0 Then
                Dim myMatch As Match = System.Text.RegularExpressions.Regex.Match(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), "^[a-zA-Z0-9 _.-]*$")
                If myMatch.Success Then
                    strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName).Replace("+", ""), Now.ToFileTime)
                    strFilename = String.Concat(strFilename, strExtension)
                    strVirtualPath = String.Concat(Config.pub_GetAppConfigValue("UploadAttachPath"), strFilename)
                    strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                    lngFileSize = hpf.ContentLength
                    If strClientFileName.Length < strPhotoLength Then
                        If Not System.IO.Directory.Exists(String.Concat(uploadFolder, "\UpLoad\Attachment")) Then
                            System.IO.Directory.CreateDirectory(String.Concat(uploadFolder, "\UpLoad\Attachment"))
                        End If
                        File.WriteAllBytes(strVirtualPath, Convert.FromBase64String(hpf.base64imageString))
                        'hpf.SaveAs(strVirtualPath)
                        drAttachment(RepairEstimateData.ATTCHMNT_PTH) = strFilename
                        drAttachment(RepairEstimateData.ACTL_FL_NM) = strClientFileName
                        drAttachment(RepairEstimateData.MDFD_BY) = strModifiedBy
                        drAttachment(RepairEstimateData.MDFD_DT) = CDate(Now)
                        drAttachment(RepairEstimateData.DPT_ID) = intDepotId
                        drAttachment(RepairEstimateData.ERR_FLG) = False
                    Else
                        drAttachment(RepairEstimateData.ERR_FLG) = True
                        drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("Attachment File Name (", strClientFileName, " must be below ", strPhotoLength, "Characters.")
                    End If
                Else
                    drAttachment(RepairEstimateData.ERR_FLG) = True
                    drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("File Name (", strClientFileName, ") with special characters are not allowed. ")
                End If


                'If bv_strPageName = "GateIn" Then
                '    'CacheData(GATE_IN, dsGateInData)
                'End If
            Else
                actualfleName = System.IO.Path.GetFileName(hpf.base64imageString)
                drAttachment(RepairEstimateData.ATTCHMNT_PTH) = actualfleName
                drAttachment(RepairEstimateData.ACTL_FL_NM) = strClientFileName
                drAttachment(RepairEstimateData.MDFD_BY) = strModifiedBy
                drAttachment(RepairEstimateData.MDFD_DT) = CDate(Now)
                drAttachment(RepairEstimateData.DPT_ID) = intDepotId
                drAttachment(RepairEstimateData.ERR_FLG) = False
            End If
            dsRepairEstimate.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(drAttachment)
        Next


        Return dsRepairEstimate

    End Function

 

    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function RepairLineItems(ByVal Mode As String, ByVal bv_strEstimateId As String, ByVal UserName As String) As ArrayOfRepairEstimateMobileModel


        Dim ArrayOfRepairEstimateMobileModel As New ArrayOfRepairEstimateMobileModel
        Try



            Dim arrayLineItem As New ArrayList
            Dim dsRepairEstimate As New RepairEstimateDataSet
            Dim RepairEstimateMobile_C As New RepairEstimateMobile_C
            gateInMobile.DepotID(UserName)

            dsRepairEstimate = objEstimate.pub_GetRepairEstimateDetailByRepairEstimationId(CLng(bv_strEstimateId))
            'RepairEstimateMobile_C.FetchLineItems(TariffId, TariffGroupId, bv_strEstimateId, Mode)


            For Each dt1 As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows

                Dim LineItem As New LineItem

                LineItem.RPR_ESTMT_DTL_ID = dt1.Item("RPR_ESTMT_DTL_ID").ToString()
                LineItem.RPR_ESTMT_ID = dt1.Item("RPR_ESTMT_ID").ToString()
                LineItem.Item = dt1.Item("ITM_ID").ToString()
                LineItem.ItemCd = dt1.Item("ITM_CD").ToString()
                LineItem.SubItem = dt1.Item("SB_ITM_ID").ToString()
                LineItem.SubItemCd = dt1.Item("SB_ITM_CD").ToString()
                LineItem.Damage = dt1.Item("DMG_ID").ToString()
                LineItem.DamageCd = dt1.Item("DMG_CD").ToString()
                LineItem.Repair = dt1.Item("RPR_ID").ToString()
                LineItem.RepairCd = dt1.Item("RPR_CD").ToString()
                LineItem.ManHour = dt1.Item("LBR_HRS").ToString()
                LineItem.DmgRprRemarks = dt1.Item("DMG_RPR_DSCRPTN").ToString()
                LineItem.MHR = dt1.Item("LBR_RT").ToString()
                LineItem.MHC = dt1.Item("LBR_HR_CST_NC").ToString()
                LineItem.MC = dt1.Item("MTRL_CST_NC").ToString()
                LineItem.TC = dt1.Item("TTL_CST_NC").ToString()
                LineItem.Responsibility = dt1.Item("RSPNSBLTY_ID").ToString()
                LineItem.ResponsibilityCd = dt1.Item("RSPNSBLTY_CD").ToString()


                arrayLineItem.Add(LineItem)
            Next



            ArrayOfRepairEstimateMobileModel.Response = arrayLineItem
            ArrayOfRepairEstimateMobileModel.Status = "Success"

            Return ArrayOfRepairEstimateMobileModel


        Catch ex As Exception

            'ArrayOfRepairEstimateMobileModel.Response = arrayLineItem
            ArrayOfRepairEstimateMobileModel.Status = ex.Message

            Return ArrayOfRepairEstimateMobileModel

        End Try

    End Function

    'Delete Line Item
    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function DeleteLineItem(ByVal bv_strEstimateId As String) As ArrayOfRepairEstimateMobileModel

        Dim query1 As String = "Select count(*) as Count from [REPAIR_ESTIMATE_DETAIL] where [RPR_ESTMT_DTL_ID] = '" + bv_strEstimateId + "'"
        Dim ds1 As Long = conn.Scalar(query1)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds

        Dim ArrayOfRepairEstimateMobileModel As New ArrayOfRepairEstimateMobileModel


        If ds1 > 0 Then



            Dim query As String = "delete from [REPAIR_ESTIMATE_DETAIL] where [RPR_ESTMT_DTL_ID] = '" + bv_strEstimateId + "'"
            Dim ds As DataSet = conn.connection(query)
            'conn.connection(query).Fill(ds)
            'ds.Tables(Table)
            'ds.Tables()
            'Return ds
            Dim dtPreaAdvice As DataTable
            dtPreaAdvice = ds.Tables(0)



            ArrayOfRepairEstimateMobileModel.Status = "Success"

            Return ArrayOfRepairEstimateMobileModel


        Else
            ArrayOfRepairEstimateMobileModel.Status = "LineItem Not Exist"


        End If

    End Function

    'Delete Attachment
    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function DeleteAttachment(ByVal bv_strAttachmentId As String) As ArrayOfRepairEstimateMobileModel

        Dim query1 As String = "Select count(*) as Count from [ATTACHMENT] where [ATTCHMNT_ID] = '" + bv_strAttachmentId + "'"
        Dim ds1 As Long = conn.Scalar(query1)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds

        Dim ArrayOfRepairEstimateMobileModel As New ArrayOfRepairEstimateMobileModel


        If ds1 > 0 Then



            Dim query As String = "delete from [ATTACHMENT] where [ATTCHMNT_ID] = '" + bv_strAttachmentId + "'"
            Dim ds As DataSet = conn.connection(query)
            'conn.connection(query).Fill(ds)
            'ds.Tables(Table)
            'ds.Tables()
            'Return ds
            Dim dtPreaAdvice As DataTable
            dtPreaAdvice = ds.Tables(0)



            ArrayOfRepairEstimateMobileModel.Status = "Success"

            Return ArrayOfRepairEstimateMobileModel


        Else
            ArrayOfRepairEstimateMobileModel.Status = "Attachment Not Exist"


        End If

    End Function

    'PendingList Tariff Based Items
    <WebMethod(enableSession:=True)> _
 <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function FetchTariff(ByVal UserName As String, ByVal Mode As String, ByVal TariffId As String, ByVal TariffGroupId As String,
                                ByVal bv_strEstimateId As String, ByVal NextIndex As String, ByVal LaborRate As String) As ArrayOfRepairEstimateMobileModel


        Dim ArrayOfRepairEstimateMobileModel As New ArrayOfRepairEstimateMobileModel
        Try


            Dim arrayLineItem As New ArrayList
            Dim dsRepairEstimate As New RepairEstimateDataSet
            Dim RepairEstimateMobile_C As New RepairEstimateMobile_C
            gateInMobile.DepotID(UserName)
            'RepairEstimateMobile_C.GetLineItems(bv_strEstimateId, Mode)
            'dsRepairEstimate = RepairEstimateMobile_C.FetchLineItems(TariffId, TariffGroupId, bv_strEstimateId, Mode)


            Dim objCommon As New CommonData()
            Dim intDepotId As Integer
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''






            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            Dim str_061KeyValue As String
            Dim bln_061KeyExist As Boolean
            str_061KeyValue = objCommon.GetConfigSetting("061", bln_061KeyExist)
            Dim dtTariff As New DataTable
            Dim drNew As DataRow
            'Dim decManHourRate As Decimal = 0D
            intDepotId = CInt(objCommon.GetDepotID())
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotId = CInt(objCommon.GetHeadQuarterID())
            End If



            If bln_061KeyExist Then


                dtTariff = objEstimate.GetTariffCodeTable(intDepotId, TariffId).Tables(RepairEstimateData._V_TARIFF_CODE_DETAIL)
                If dtTariff.Rows.Count > 0 And bln_061KeyExist Then
                    For Each dr As DataRow In dtTariff.Rows
                        drNew = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).NewRow()
                        drNew.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = NextIndex + 1
                        drNew.Item(RepairEstimateData.ITM_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_LCTN_ID)
                        drNew.Item(RepairEstimateData.ITM_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_LCTN_CD)
                        drNew.Item(RepairEstimateData.SB_ITM_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_CMPNT_ID)
                        drNew.Item(RepairEstimateData.SB_ITM_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_CMPNT_CD)
                        drNew.Item(RepairEstimateData.DMG_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_DMG_ID)
                        drNew.Item(RepairEstimateData.DMG_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_DMG_CD)
                        drNew.Item(RepairEstimateData.MTRL_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_ID)
                        drNew.Item(RepairEstimateData.MTRL_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CD)
                        drNew.Item(RepairEstimateData.RPR_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_RPR_ID)
                        drNew.Item(RepairEstimateData.RPR_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_RPR_CD)
                        If Not dr.Item(RepairEstimateData.TRFF_CD_DTL_MNHR) Is DBNull.Value Then
                            drNew.Item(RepairEstimateData.LBR_HRS) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MNHR)
                        Else
                            drNew.Item(RepairEstimateData.LBR_HRS) = "0.00"
                        End If
                        drNew.Item(RepairEstimateData.CHNG_BIT) = False
                        drNew.Item(RepairEstimateData.RSPNSBLTY_CD) = "O"
                        drNew.Item(RepairEstimateData.RSPNSBLTY_ID) = 9

                        drNew.Item(RepairEstimateData.TRFF_CD_DTL_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_ID)

                        drNew.Item(RepairEstimateData.LBR_RT) = LaborRate
                        drNew.Item(RepairEstimateData.LBR_HR_CST_NC) = CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HRS)) * CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_RT))
                        If Not dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CST) Is DBNull.Value Then
                            drNew.Item(RepairEstimateData.MTRL_CST_NC) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CST)
                        Else
                            drNew.Item(RepairEstimateData.MTRL_CST_NC) = "0.00"
                        End If

                        drNew.Item(RepairEstimateData.TTL_CST_NC) = CommonWeb.iDec(dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CST)) + CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HR_CST_NC))
                        drNew.Item(RepairEstimateData.DMG_RPR_DSCRPTN) = dr.Item(RepairEstimateData.TRFF_CD_DTL_RMRKS)
                        dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Add(drNew)
                    Next
                    ' End If
                Else
                    dtTariff = objEstimate.pub_GetLineDeatilbyTariffCodeId(intDepotId, CStr(TariffId), CStr(TariffGroupId)).Tables(RepairEstimateData._V_TARIFF_CODE)
                    If dtTariff.Rows.Count > 0 Then
                        For Each dr As DataRow In dtTariff.Rows
                            drNew = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).NewRow()
                            drNew.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), RepairEstimateData.RPR_ESTMT_DTL_ID)
                            drNew.Item(RepairEstimateData.ITM_ID) = dr.Item(RepairEstimateData.ITM_ID)
                            drNew.Item(RepairEstimateData.ITM_CD) = dr.Item(RepairEstimateData.ITM_CD)
                            drNew.Item(RepairEstimateData.SB_ITM_ID) = dr.Item(RepairEstimateData.SB_ITM_ID)
                            drNew.Item(RepairEstimateData.SB_ITM_CD) = dr.Item(RepairEstimateData.SB_ITM_CD)
                            drNew.Item(RepairEstimateData.DMG_ID) = dr.Item(RepairEstimateData.DMG_ID)
                            drNew.Item(RepairEstimateData.DMG_CD) = dr.Item(RepairEstimateData.DMG_CD)
                            drNew.Item(RepairEstimateData.RPR_ID) = dr.Item(RepairEstimateData.RPR_ID)
                            drNew.Item(RepairEstimateData.RPR_CD) = dr.Item(RepairEstimateData.RPR_CD)
                            drNew.Item(RepairEstimateData.LBR_HRS) = dr.Item(RepairEstimateData.MN_HR)
                            drNew.Item(RepairEstimateData.LBR_RT) = LaborRate
                            drNew.Item(RepairEstimateData.LBR_HR_CST_NC) = CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HRS)) * CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_RT))
                            drNew.Item(RepairEstimateData.MTRL_CST_NC) = dr.Item(RepairEstimateData.MTRL_CST)
                            drNew.Item(RepairEstimateData.TTL_CST_NC) = CommonWeb.iDec(dr.Item(RepairEstimateData.MTRL_CST_NC)) + CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HR_CST_NC))
                            drNew.Item(RepairEstimateData.DMG_RPR_DSCRPTN) = dr.Item(RepairEstimateData.RMRKS_VC)
                            drNew.Item(RepairEstimateData.RSPNSBLTY_CD) = "C"
                            drNew.Item(RepairEstimateData.RSPNSBLTY_ID) = 66
                            dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Add(drNew)
                        Next
                    End If
                End If
            End If



            For Each dt1 As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows

                Dim LineItem As New LineItem

                LineItem.RPR_ESTMT_DTL_ID = dt1.Item("RPR_ESTMT_DTL_ID").ToString()
                LineItem.RPR_ESTMT_ID = dt1.Item("RPR_ESTMT_ID").ToString()
                LineItem.Item = dt1.Item("ITM_ID").ToString()
                LineItem.ItemCd = dt1.Item("ITM_CD").ToString()
                LineItem.SubItem = dt1.Item("SB_ITM_ID").ToString()
                LineItem.SubItemCd = dt1.Item("SB_ITM_CD").ToString()
                LineItem.Damage = dt1.Item("DMG_ID").ToString()
                LineItem.DamageCd = dt1.Item("DMG_CD").ToString()
                LineItem.Repair = dt1.Item("RPR_ID").ToString()
                LineItem.RepairCd = dt1.Item("RPR_CD").ToString()
                LineItem.ManHour = dt1.Item("LBR_HRS").ToString()
                LineItem.DmgRprRemarks = dt1.Item("DMG_RPR_DSCRPTN").ToString()
                LineItem.MHR = dt1.Item("LBR_RT").ToString()
                LineItem.MHC = dt1.Item("LBR_HR_CST_NC").ToString()
                LineItem.MC = dt1.Item("MTRL_CST_NC").ToString()
                LineItem.TC = dt1.Item("TTL_CST_NC").ToString()
                LineItem.Responsibility = dt1.Item("RSPNSBLTY_ID").ToString()
                LineItem.ResponsibilityCd = dt1.Item("RSPNSBLTY_CD").ToString()


                arrayLineItem.Add(LineItem)

            Next




            ArrayOfRepairEstimateMobileModel.Response = arrayLineItem
            ArrayOfRepairEstimateMobileModel.Status = "Success"

            Return ArrayOfRepairEstimateMobileModel

        Catch ex As Exception


            'ArrayOfRepairEstimateMobileModel.Response = arrayLineItem
            ArrayOfRepairEstimateMobileModel.Status = ex.Message

            Return ArrayOfRepairEstimateMobileModel

        End Try

    End Function

    'Filter
    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function filter(ByVal filterType As String, ByVal filterCondition As String, ByVal filterValue As String, ByVal UserName As String, ByVal Mode As String, ByVal PageName As String) As ListModel

        Dim objCommonData As New CommonData
        gateInMobile.DepotID(UserName)
        Dim listVlaues As New ListModel
        Dim arraylist As New ArrayList
        Dim filterNewConditon As String
        Dim ds As DataSet
        Dim dtPreaAdvice As DataTable
        Dim query As String = ""
        If filterCondition = "Similar" Or filterCondition = "Contains" Then
            filterNewConditon = "LIKE"
        Else
            filterNewConditon = "Not LIKE"
        End If



        ds = New DataSet()
        Select Case Mode
            Case strNew
                If filterCondition = "Equals" Then

                    If PageName = "Repair Estimate" Then
                        query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AWE' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"
                    ElseIf PageName = "Repair Approval" Then
                        'EQPMNT_STTS_CD = "AUR"
                        query = "select EQPMNT_NO from [V_ACTIVITY_STATUS] where EQPMNT_STTS_CD = 'AAR' and ACTV_BT=1"

                        Dim dsSub As DataSet = conn.connection(query)
                        'conn.connection(query).Fill(dsSub)
                        'ds.Tables(Table)
                        'ds.Tables()
                        'Return ds
                        Dim dtPreaAdviceSub As DataTable
                        dtPreaAdviceSub = dsSub.Tables(0)

                        Dim InValue As String = ""

                        For Each dtSub As DataRow In dtPreaAdviceSub.Rows

                            If InValue = "" Then
                                InValue = "'" + dtSub.Item("EQPMNT_NO").ToString() + "'"
                            Else
                                InValue = InValue + "," + "'" + dtSub.Item("EQPMNT_NO").ToString() + "'"
                            End If
                        Next



                        query = "select EQPMNT_NO from [V_REPAIR_ESTIMATE] where [MAIL_SEND] ='YES' and  [EQPMNT_NO] in (" + InValue + ")"

                        Dim dsSub1 As DataSet = conn.connection(query)
                        'conn.connection(query).Fill(dsSub1)
                        'ds.Tables(Table)
                        'ds.Tables()
                        'Return ds
                        Dim dtPreaAdviceSub1 As DataTable
                        dtPreaAdviceSub1 = dsSub1.Tables(0)

                        InValue = ""

                        For Each dtSub1 As DataRow In dtPreaAdviceSub1.Rows

                            If InValue = "" Then
                                InValue = "'" + dtSub1.Item("EQPMNT_NO").ToString() + "'"
                            Else
                                InValue = InValue + "," + "'" + dtSub1.Item("EQPMNT_NO").ToString() + "'"
                            End If
                        Next

                        query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "' [EQPMNT_NO] in (" + InValue + ")"
                        'query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 and [EQPMNT_NO] in (" + InValue + ")"

                    ElseIf PageName = "Survey Completion" Then

                        query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ASR' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"

                    End If

                    'query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AWE' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"

                    'query = "SELECT " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + "='" + filterValue + "'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                Else


                    If PageName = "Repair Estimate" Then

                        query = "SELECT distinct " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AWE' AND ACTV_BT = 1 AND " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"

                    ElseIf PageName = "Repair Approval" Then
                        'EQPMNT_STTS_CD = "AUR"
                        query = "select EQPMNT_NO from [V_ACTIVITY_STATUS] where EQPMNT_STTS_CD = 'AAR' and ACTV_BT=1"

                        Dim dsSub As DataSet = conn.connection(query)
                        'conn.connection(query).Fill(dsSub)
                        'ds.Tables(Table)
                        'ds.Tables()
                        'Return ds
                        Dim dtPreaAdviceSub As DataTable
                        dtPreaAdviceSub = dsSub.Tables(0)

                        Dim InValue As String = ""

                        For Each dtSub As DataRow In dtPreaAdviceSub.Rows

                            If InValue = "" Then
                                InValue = "'" + dtSub.Item("EQPMNT_NO").ToString() + "'"
                            Else
                                InValue = InValue + "," + "'" + dtSub.Item("EQPMNT_NO").ToString() + "'"
                            End If
                        Next



                        query = "select EQPMNT_NO from [V_REPAIR_ESTIMATE] where [MAIL_SEND] ='YES' and  [EQPMNT_NO] in (" + InValue + ")"

                        Dim dsSub1 As DataSet = conn.connection(query)
                        'conn.connection(query).Fill(dsSub1)
                        'ds.Tables(Table)
                        'ds.Tables()
                        'Return ds
                        Dim dtPreaAdviceSub1 As DataTable
                        dtPreaAdviceSub1 = dsSub1.Tables(0)

                        InValue = ""

                        For Each dtSub1 As DataRow In dtPreaAdviceSub1.Rows

                            If InValue = "" Then
                                InValue = "'" + dtSub1.Item("EQPMNT_NO").ToString() + "'"
                            Else
                                InValue = InValue + "," + "'" + dtSub1.Item("EQPMNT_NO").ToString() + "'"
                            End If
                        Next

                        query = "SELECT distinct " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 AND " + filterType + " " + filterNewConditon + " '%" + filterValue + "%' AND [EQPMNT_NO] in (" + InValue + ")"
                        'query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 and [EQPMNT_NO] in (" + InValue + ")"

                    ElseIf PageName = "Survey Completion" Then

                        query = "SELECT distinct " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ASR' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"

                    End If

                    'query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AWE' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"

                    'query = "SELECT " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + "='" + filterValue + "'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds




                End If

            Case strEdit

                If filterCondition = "Equals" Then

                    If PageName = "Repair Estimate" Then

                        query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"

                    ElseIf PageName = "Repair Approval" Then
                        'EQPMNT_STTS_CD = "AUR"


                        query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AUR' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"
                        'query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 and [EQPMNT_NO] in (" + InValue + ")"

                    ElseIf PageName = "Survey Completion" Then

                        query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'SRV' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"

                    End If


                Else

                    If PageName = "Repair Estimate" Then

                        query = "SELECT distinct " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 AND " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"

                    ElseIf PageName = "Repair Approval" Then
                        'EQPMNT_STTS_CD = "AUR"


                        query = "SELECT distinct " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AUR' AND ACTV_BT = 1 AND " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"
                        'query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 and [EQPMNT_NO] in (" + InValue + ")"

                    ElseIf PageName = "Survey Completion" Then

                        query = "SELECT distinct " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'SRV' AND ACTV_BT = 1 AND " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"

                    End If

                    ds = conn.connection(query)

                End If

        End Select

        dtPreaAdvice = ds.Tables(0)

        For Each dd As DataRow In dtPreaAdvice.Rows

            Dim filteredvalues As New FilteredValues
            filteredvalues.Values = dd.Item(filterType).ToString()

            arraylist.Add(filteredvalues)
        Next


        listVlaues.ListGateInss = arraylist

        Return listVlaues

    End Function


    'Search
    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function SearchList(ByVal UserName As String, ByVal Mode As String, ByVal PageName As String,
                               ByVal SearchValues As SearchValues, ByVal filterType As String) As ArrayOfRepairEstimateMobileModel


        Dim ArrayOfRepairEstimateMobileModel As New ArrayOfRepairEstimateMobileModel
        Try



            gateInMobile.DepotID(UserName)
            Dim intDepotID As Integer = objCommon.GetDepotID()
            Dim objCleaning As New Cleaning
            Dim query As String = ""
            Dim EQPMNT_STTS_CD As String = ""
            If PageName = "Repair Estimate" Then
                If Mode = "new" Then
                    EQPMNT_STTS_CD = "AWE"
                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AWE' AND ACTV_BT = 1"
                Else
                    EQPMNT_STTS_CD = "AAR"
                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1"
                End If

            ElseIf PageName = "Repair Approval" Then
                If Mode = "new" Then
                    EQPMNT_STTS_CD = "AUR"
                    query = " select EQPMNT_NO  from [V_ACTIVITY_STATUS] where EQPMNT_STTS_CD = 'AAR' and ACTV_BT=1"

                    Dim dsSub As DataSet = conn.connection(query)
                    'conn.connection(query).Fill(dsSub)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds
                    Dim dtPreaAdviceSub As DataTable
                    dtPreaAdviceSub = dsSub.Tables(0)

                    Dim InValue As String = ""

                    For Each dtSub As DataRow In dtPreaAdviceSub.Rows

                        If InValue = "" Then
                            InValue = "'" + dtSub.Item("EQPMNT_NO").ToString() + "'"
                        Else
                            InValue = InValue + "," + "'" + dtSub.Item("EQPMNT_NO").ToString() + "'"
                        End If
                    Next



                    query = " select EQPMNT_NO  from [V_REPAIR_ESTIMATE] where [MAIL_SEND] ='YES' and  [EQPMNT_NO] in (" + InValue + ")"

                    Dim dsSub1 As DataSet = conn.connection(query)
                    'conn.connection(query).Fill(dsSub1)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds
                    Dim dtPreaAdviceSub1 As DataTable
                    dtPreaAdviceSub1 = dsSub1.Tables(0)

                    InValue = ""

                    For Each dtSub1 As DataRow In dtPreaAdviceSub1.Rows

                        If InValue = "" Then
                            InValue = "'" + dtSub1.Item("EQPMNT_NO").ToString() + "'"
                        Else
                            InValue = InValue + "," + "'" + dtSub1.Item("EQPMNT_NO").ToString() + "'"
                        End If
                    Next

                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 and [EQPMNT_NO] in (" + InValue + ")"

                Else
                    EQPMNT_STTS_CD = "AUR"
                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AUR' AND ACTV_BT = 1"
                End If

            ElseIf PageName = "Survey Completion" Then

                If Mode = "new" Then
                    EQPMNT_STTS_CD = "ASR"
                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ASR' AND ACTVTY_NAM = 'Repair Completion' AND ACTV_BT = 1"


                Else

                    EQPMNT_STTS_CD = "SRV"
                    query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'SRV' AND ACTV_BT = 1"
                End If
            End If

            'query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AWE' AND ACTV_BT = 1"
            Dim ds As DataSet = conn.connection(query)
            'conn.connection(query).Fill(ds)
            'ds.Tables(Table)
            'ds.Tables()
            'Return ds
            Dim dtPreaAdvice As DataTable
            dtPreaAdvice = ds.Tables(0)

            Dim arraylist As New ArrayList
            Dim SubArrayList As New ArrayList
            Dim objRepairEstimate As New RepairEstimate
            Dim dsRepairEstimate As New RepairEstimateDataSet
            Dim objCommonConfig As New ConfigSetting()
            Dim str_061KeyValue As String
            Dim bln_061Key As Boolean
            bln_061Key = objCommonConfig.IsKeyExists
            str_061KeyValue = objCommonConfig.pub_GetConfigSingleValue("061", intDepotID)
            Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
            Dim dtEqipmentInformation As New DataTable


            For Each dd In SearchValues.SearchValues
                ' Dim dt() As DataRow = dtPreaAdvice.Select(String.Concat(filterType, "='", dd.values, "'"))
                For Each dr1 As DataRow In dtPreaAdvice.Select(String.Concat(filterType, "='", dd.values, "'"))

                    Dim RepairEstimateMobileModel As New RepairEstimateMobileModel

                    RepairEstimateMobileModel.EquipmentNo = dr1.Item("EQPMNT_NO").ToString()
                    RepairEstimateMobileModel.Customer = dr1.Item("CSTMR_CD").ToString()
                    RepairEstimateMobileModel.InDate = dr1.Item("GTN_DT").ToString()
                    RepairEstimateMobileModel.PreviousCargo = dr1.Item("PRDCT_DSCRPTN_VC").ToString()
                    RepairEstimateMobileModel.LastStatusDate = dr1.Item("ACTVTY_DT").ToString()
                    RepairEstimateMobileModel.Remarks = dr1.Item("RMRKS_VC").ToString()
                    RepairEstimateMobileModel.GiTransactionNo = dr1.Item("GI_TRNSCTN_NO").ToString()

                    Dim strLaborRate As String = ""
                    strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByCustomerID(CInt(objCommon.GetHeadQuarterID()), dr1.Item(RepairEstimateData.CSTMR_ID))
                    If strLaborRate Is Nothing Then
                        RepairEstimateMobileModel.LaborRate = ""
                    Else
                        RepairEstimateMobileModel.LaborRate = strLaborRate
                    End If

                    dsRepairEstimate = objRepairEstimate.pub_GetEquipmentInformationByEqpmntNo(intDepotID, dr1.Item("EQPMNT_NO").ToString)
                    dtEqipmentInformation = dsRepairEstimate.Tables(RepairEstimateData._V_EQUIPMENT_INFORMATION).Copy()
                    If dtEqipmentInformation.Rows.Count > 0 Then
                        If str_061KeyValue.ToLower = "false" Then
                            If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT).ToString = "" Then
                                RepairEstimateMobileModel.LastTestDate = ""
                            Else
                                If CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT)) = sqlDbnull Then
                                    RepairEstimateMobileModel.LastTestDate = ""
                                Else
                                    RepairEstimateMobileModel.LastTestDate = CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT)).ToString("dd-MMM-yyyy")
                                End If
                            End If
                            If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT).ToString = "" Then
                                RepairEstimateMobileModel.NextTestDate = ""
                            Else
                                If CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT)) = sqlDbnull Then
                                    RepairEstimateMobileModel.NextTestDate = ""
                                Else
                                    RepairEstimateMobileModel.NextTestDate = CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT)).ToString("dd-MMM-yyyy")
                                End If
                            End If
                            If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_TYP_ID) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_TYP_ID).ToString = "" Then
                                RepairEstimateMobileModel.LastTestType = ""
                            Else
                                RepairEstimateMobileModel.LastTestType = dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_TYP_ID).ToString()
                            End If
                            If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_SRVYR_NM) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_SRVYR_NM).ToString = "" Then
                                RepairEstimateMobileModel.LastSurveyor = ""
                            Else
                                RepairEstimateMobileModel.LastSurveyor = dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_SRVYR_NM).ToString()
                            End If
                            If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_TYP_ID) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_TYP_ID).ToString = "" Then
                                RepairEstimateMobileModel.NextTestType = ""
                            Else
                                RepairEstimateMobileModel.NextTestType = dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_TYP_CD)

                            End If
                            If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.VLDTY_PRD_TST_YRS) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.VLDTY_PRD_TST_YRS).ToString = "" Then
                                RepairEstimateMobileModel.ValidityPeriodforTest = ""
                            Else
                                RepairEstimateMobileModel.ValidityPeriodforTest = dtEqipmentInformation.Rows(0).Item(RepairEstimateData.VLDTY_PRD_TST_YRS)
                            End If
                        End If
                    End If


                    Dim arrayLineItem As New ArrayList

                    If PageName = "Repair Estimate" Then

                        If Mode = "new" Then
                            RepairEstimateMobileModel.RepairTypeCD = ""
                            RepairEstimateMobileModel.RepairTypeID = ""
                            RepairEstimateMobileModel.InvoicingPartyID = ""
                            RepairEstimateMobileModel.InvoicingPartyCD = ""
                            RepairEstimateMobileModel.InvoicingPartyName = ""
                            RepairEstimateMobileModel.RevisionNo = ""

                            RepairEstimateMobileModel.RepairEstimateID = ""
                            RepairEstimateMobileModel.CustomerAppRef = ""
                            RepairEstimateMobileModel.ApprovalDate = ""
                            RepairEstimateMobileModel.PartyAppRef = ""
                            RepairEstimateMobileModel.SurveyorName = ""
                            RepairEstimateMobileModel.SurveyCompletionDate = ""

                            RepairEstimateMobileModel.LineItems = arrayLineItem



                        ElseIf Mode = "edit" Then

                            RepairEstimateMobileModel = ReturnValueComman(9, intDepotID, dr1.Item("GI_TRNSCTN_NO"), dr1.Item("EQPMNT_NO"), RepairEstimateMobileModel)

                        End If

                    ElseIf PageName = "Repair Approval" Then


                        If Mode = "new" Then

                            RepairEstimateMobileModel = ReturnValueComman(9, intDepotID, dr1.Item("GI_TRNSCTN_NO"), dr1.Item("EQPMNT_NO"), RepairEstimateMobileModel)

                        Else

                            RepairEstimateMobileModel = ReturnValueComman(10, intDepotID, dr1.Item("GI_TRNSCTN_NO"), dr1.Item("EQPMNT_NO"), RepairEstimateMobileModel)

                        End If



                    ElseIf PageName = "Survey Completion" Then


                        If Mode = "new" Then

                            RepairEstimateMobileModel = ReturnValueComman(10, intDepotID, dr1.Item("GI_TRNSCTN_NO"), dr1.Item("EQPMNT_NO"), RepairEstimateMobileModel)

                        Else

                            RepairEstimateMobileModel = ReturnValueComman(14, intDepotID, dr1.Item("GI_TRNSCTN_NO"), dr1.Item("EQPMNT_NO"), RepairEstimateMobileModel)

                        End If
                    End If



                    Dim dtAttachment As New DataTable
                    Dim objGatein As New GateIns
                    Dim objTrans As New Transactions
                    Dim ouput As New GateinDataSet
                    Dim ss As String = RepairEstimateMobileModel.RepairEstimateID.ToString()


                    If Mode = "edit" Then





                        ouput.Tables(GateinData._ATTACHMENT).Clear()
                        dtAttachment = ouput.Tables(GateinData._ATTACHMENT).Clone()
                        If ouput.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                            ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                            dtAttachment = objGatein.GetAttachmentByRepairEstimateId(intDepotID, PageName, CLng(RepairEstimateMobileModel.RepairEstimateID), objTrans).Tables(GateinData._ATTACHMENT)
                            ouput.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                            'End If
                        End If
                        'Dim attcj1 As New attchementPro
                        'attcj1.attchPath = ""
                        'attcj1.fileName = ""

                        'attcj1.imageUrl = ""

                        Dim attch1 As New ArrayList
                        Dim attch As New ArrayList
                        'attch1.Add(attcj1)

                        For Each dr As DataRow In ouput.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", RepairEstimateMobileModel.RepairEstimateID, " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))
                            Dim attcj As New attchementPro
                            attcj.attchPath = dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString
                            attcj.fileName = dr.Item(RepairEstimateData.ACTL_FL_NM).ToString
                            attcj.attchId = dr.Item(RepairEstimateData.ATTCHMNT_ID).ToString
                            Dim currentUrl As String = HttpContext.Current.Request.Url.Host
                            'attcj.imageUrl = "http://192.18.0.12/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                            Dim Attachment_URL As String = Config.pub_GetAppConfigValue("AttachmentURL")
                            attcj.imageUrl = Attachment_URL + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                            'h.imageUrl = "http://"+ currentUrl +"/iTankDepoUI/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                            'sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(RepairEstimateData.ATTCHMNT_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                            'sbAttachment.Append(String.Concat("<a target='_blank' href='../download.ashx?FL_NM=", strFileName, "'  title='Click to download error file' >", strFileName, "</a>"))
                            'sbAttachment.Append(String.Concat("<a target='_blank' href='//", strPath, "?FL_NM=", strPhysicalpath, "'  title='Click to download file' >", strFileName, "</a>"))
                            'sbAttachment.Append("<br />")
                            attch.Add(attcj)
                        Next

                        If attch.Count > 0 Then
                            RepairEstimateMobileModel.attchement = attch
                        Else
                            RepairEstimateMobileModel.attchement = attch1
                        End If

                    End If

                    Response.Add(RepairEstimateMobileModel)

                Next

            Next
            ArrayOfRepairEstimateMobileModel.Response = Response


            ArrayOfRepairEstimateMobileModel.Status = "Success"

            Return ArrayOfRepairEstimateMobileModel


        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
            ArrayOfRepairEstimateMobileModel.Status = ex.Message

            Return ArrayOfRepairEstimateMobileModel

        End Try

    End Function


    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function EstDetSummary(ByVal CustomerID As Integer, ByVal UserName As String) As EstDetSummary
        Dim estDtSummary As New EstDetSummary
        Try

            gateInMobile.DepotID(UserName)
            Dim dt As DataTable
            dt = objEstimate.GetAgentIdByCustomer(CustomerID, objCommon.GetDepotID())

            Dim objRepairEstimate As New RepairEstimate
            Dim dtCurrcy As New DataTable
            Dim dtExchange As New DataTable
            Dim intDepotID As Integer = objCommon.GetDepotID()
            Dim dtCurrency As DataTable

            dtCurrcy = objRepairEstimate.GetCurrByAgntId(CInt(dt.Rows(0).Item("AGNT_ID")), intDepotID)
            dtExchange = objRepairEstimate.Pub_GetAgentCurrencyExchangeRateByDptId(objCommon.GetDepotID(), CInt(dt.Rows(0).Item("AGNT_ID")))
            dtCurrency = objRepairEstimate.Pub_GetCurrencyExchangeRateByDptId(intDepotID, CustomerID).Tables(RepairEstimateData._V_REPAIR_ESTIMATE_REPORT)


            If dtCurrcy.Rows.Count > 0 Then
                estDtSummary.Currency = dtCurrcy.Rows(0).Item(1).ToString
                'sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnToCurrencyCD, dtCurrcy.Rows(0).Item(1).ToString)
            Else
                estDtSummary.Currency = dtCurrency.Rows(0).Item(RepairEstimateData.CSTMR_CRRNCY_CD).ToString
                'sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnToCurrencyCD, dtCurrency.Rows(0).Item(RepairEstimateData.CSTMR_CRRNCY_CD).ToString))
            End If


            If dtExchange.Rows.Count > 0 Then
                estDtSummary.ExchangeRate = dtExchange.Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC).ToString()
                'sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnExchangeRate, dtExchange.Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC).ToString()))
            Else
                estDtSummary.ExchangeRate = dtCurrency.Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC).ToString
                'sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnExchangeRate, dtCurrency.Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC).ToString))
            End If

            estDtSummary.Message = "Success"
            Return estDtSummary

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
            estDtSummary.Message = ex.Message
        End Try

    End Function

    'Esimated Summary
    <WebMethod(enableSession:=True)> _
 <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function EstimateCostSummary(ByVal CustomerId As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select CUS.CSTMR_ID,CUS.CSTMR_CD,CUR.CRRNCY_ID,CUR.CRRNCY_CD,isnull(ER.EXCHNG_RT_PR_UNT_NC ,0)EXCHNG_RT_PR_UNT_NC from customer CUS INNER JOIN currency CUR ON CUR.CRRNCY_ID=CUS.CSTMR_CRRNCY_ID LEFT JOIN Exchange_rate ER ON ER.TO_CRRNCY_ID=CUS.CSTMR_CRRNCY_ID where cstmr_id ='" + CustomerId + "'"
        Dim ds As DataSet = conn.connection(query)

        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim EstimateCost As New RepairEstimateSummary
            EstimateCost.CSTMR_ID = dd.Item("CSTMR_ID")
            EstimateCost.CSTMR_CD = dd.Item("CSTMR_CD")
            EstimateCost.CRRNCY_ID = dd.Item("CRRNCY_ID")
            EstimateCost.CRRNCY_CD = dd.Item("CRRNCY_CD")
            EstimateCost.EXCHNG_RT_PR_UNT_NC = dd.Item("EXCHNG_RT_PR_UNT_NC")
            arraylist.Add(EstimateCost)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown


    End Function


End Class