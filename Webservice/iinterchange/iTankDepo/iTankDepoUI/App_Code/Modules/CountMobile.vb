Imports Microsoft.VisualBasic
Imports System.Web.Script.Serialization
Imports System.Globalization
Imports System.IO
Public Class CountMobile
    Inherits Framebase

    Dim gateInMobile As New GateinMobile_C
    Dim conn As New Dropdown_C


    Public Function InspectionCount(ByVal UserName As String) As DataSet

        gateInMobile.DepotID(UserName)
        Dim objCommon As New CommonData
        Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],EIR_NO,ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'CLN' AND ACTV_BT = 1"
        Dim ds As DataSet = conn.connection(query)
        Return ds


    End Function
   

    Public Function CleaningCount(ByVal UserName As String) As DataSet

        gateInMobile.DepotID(UserName)
        ' Dim intDepotID As Integer = objCommon.GetDepotID()
        Dim objCommon As New CommonData
        Dim query As String = "SELECT ADDTNL_CLNNG_BT,ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ACN' AND ACTV_BT = 1"
        Dim ds As DataSet = conn.connection(query)
        Return ds



    End Function


    Public Function RepairEstimateCount(ByVal UserName As String) As DataSet

        gateInMobile.DepotID(UserName)
        ' Dim intDepotID As Integer = objCommon.GetDepotID()
        Dim objCommon As New CommonData
        Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='1' AND EQPMNT_STTS_CD = 'AWE' AND ACTV_BT = 1"
        Dim ds As DataSet = conn.connection(query)
        Return ds



    End Function

    Public Function RepairApprovalCount(ByVal UserName As String) As DataSet

        gateInMobile.DepotID(UserName)
        ' Dim intDepotID As Integer = objCommon.GetDepotID()
        Dim objCommon As New CommonData
        Dim query As String = "select EQPMNT_NO  from [V_ACTIVITY_STATUS] where EQPMNT_STTS_CD = 'AAR' and ACTV_BT=1"
        ' Query = " select EQPMNT_NO  from [V_ACTIVITY_STATUS] where EQPMNT_STTS_CD = 'AAR' and ACTV_BT=1"

        Dim dsSub As DataSet = conn.connection(Query)
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

        Dim dsSub1 As DataSet = conn.connection(Query)
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
        If InValue <> "" Then
            query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 and [EQPMNT_NO] in (" + InValue + ")"


        Else
            query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'AAR' AND ACTV_BT = 1 and [EQPMNT_NO] in (NULL)"
        End If







        Dim ds As DataSet = conn.connection(query)
        Return ds



    End Function

    'Public Function RepairApprovalCount(ByVal UserName As String) As DataSet

    '    gateInMobile.DepotID(UserName)
    '    ' Dim intDepotID As Integer = objCommon.GetDepotID()
    '    Dim objCommon As New CommonData
    '    Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='1' AND EQPMNT_STTS_CD = 'AUR' AND ACTV_BT = 1"
    '    Dim ds As DataSet = conn.connection(query)
    '    Return ds



    'End Function

    'Public Function RepairCompletionCount(ByVal UserName As String) As DataSet

    '    gateInMobile.DepotID(UserName)
    '    ' Dim intDepotID As Integer = objCommon.GetDepotID()
    '    Dim objCommon As New CommonData
    '    Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='1' AND EQPMNT_STTS_CD = 'AUR' AND ACTV_BT = 1"
    '    Dim ds As DataSet = conn.connection(query)
    '    Return ds



    'End Function

    Public Function RepairCompletionCount(ByVal UserName As String) As DataSet

        gateInMobile.DepotID(UserName)
        ' Dim intDepotID As Integer = objCommon.GetDepotID()
        Dim objCommon As New CommonData
        Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='1' AND EQPMNT_STTS_CD = 'AUR' AND ACTV_BT = 1"
        Dim ds As DataSet = conn.connection(query)
        Return ds



    End Function

    Public Function SurveyCompletionCount(ByVal UserName As String) As DataSet

        gateInMobile.DepotID(UserName)
        ' Dim intDepotID As Integer = objCommon.GetDepotID()
        Dim objCommon As New CommonData
        Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='1' AND EQPMNT_STTS_CD = 'ASR' AND ACTV_BT = 1"
        Dim ds As DataSet = conn.connection(query)
        Return ds



    End Function

    Public Function RepairCount(ByVal UserName As String) As DataSet

        gateInMobile.DepotID(UserName)
        ' Dim intDepotID As Integer = objCommon.GetDepotID()
        Dim objCommon As New CommonData
        Dim query As String = " SELECT * FROM REPAIR_ESTIMATE WHERE EQPMNT_STTS_ID in ('7','9','11','14')"
        Dim ds As DataSet = conn.connection(query)
        Return ds



    End Function
    Public Function LeakTestCount(ByVal UserName As String) As LeakTestDataSet

        gateInMobile.DepotID(UserName)
        Dim ObjLeakTest As New LeakTest()
        Dim objCommon As New CommonData
        Dim dsLeakTestData As New LeakTestDataSet
        dsLeakTestData = ObjLeakTest.pub_GetLeakTestByActiveBit(objCommon.GetDepotID())

        Return dsLeakTestData

    End Function


    Public Function HeatingCount(ByVal UserName As String) As HeatingDataSet


        gateInMobile.DepotID(UserName)
        Dim dsHeating As HeatingDataSet
        Dim objHeating As New Heating
        Dim objCommon As New CommonData
        Dim drHeating As DataRow
        Dim dtHeating As DataTable
        Dim strDepotCurrency As String = String.Empty
        Dim intDepotId As Integer
        Dim strWfData As String = objCommon.GenerateWFData(90)
        intDepotId = CommonWeb.iInt(objCommon.GetDepotID())
        'ifgHeating.UseCachedDataSource = True
        dsHeating = objHeating.pub_GetHeatingFromGateIn(intDepotId)
        Dim dsHeatingCustomer As New HeatingDataSet
        Dim dsHeatingCharge As New HeatingDataSet
        dtHeating = dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clone()
        dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Rows.Clear()
        If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
            dsHeatingCustomer = objHeating.pub_GetCustomerDetail(objCommon.GetHeadQuarterID())
        Else
            dsHeatingCustomer = objHeating.pub_GetCustomerDetail(intDepotId)
        End If

        dsHeating.Merge(dsHeatingCustomer.Tables(HeatingData._CUSTOMER))
        dsHeatingCustomer.Tables(HeatingData._HEATING_CHARGE).Rows.Clear()
        dsHeatingCharge = objHeating.pub_VHeatingChargeByDepot(intDepotId)
        dsHeating.Merge(dsHeatingCustomer.Tables(HeatingData._HEATING_CHARGE))

        For Each dr As DataRow In dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Rows
            drHeating = dtHeating.NewRow()
            drHeating.Item(HeatingData.HTNG_ID) = CommonWeb.GetNextIndex(dtHeating, HeatingData.HTNG_ID)
            drHeating.Item(HeatingData.CSTMR_ID) = dr.Item(HeatingData.CSTMR_ID)
            drHeating.Item(HeatingData.CSTMR_CD) = dr.Item(HeatingData.CSTMR_CD)
            drHeating.Item(HeatingData.EQPMNT_NO) = dr.Item(HeatingData.EQPMNT_NO)
            drHeating.Item(HeatingData.EQPMNT_TYP_ID) = dr.Item(HeatingData.EQPMNT_TYP_ID)
            drHeating.Item(HeatingData.EQPMNT_TYP_CD) = dr.Item(HeatingData.EQPMNT_TYP_CD)
            drHeating.Item(HeatingData.EQPMNT_CD_CD) = dr.Item(HeatingData.EQPMNT_CD_CD)
            drHeating.Item(HeatingData.EQPMNT_TYP_ID) = dr.Item(HeatingData.EQPMNT_TYP_ID)
            drHeating.Item(HeatingData.EQPMNT_CD_ID) = dr.Item(HeatingData.EQPMNT_CD_ID)
            drHeating.Item(HeatingData.PRDCT_ID) = dr.Item(HeatingData.PRDCT_ID)
            drHeating.Item(HeatingData.PRDCT_DSCRPTN_VC) = dr.Item(HeatingData.PRDCT_DSCRPTN_VC)
            drHeating.Item(HeatingData.GTN_DT) = CDate(dr.Item(HeatingData.GTN_DT)).ToString("dd-MMM-yyyy")
            drHeating.Item(HeatingData.GI_TRNSCTN_NO) = dr.Item(HeatingData.GI_TRNSCTN_NO)
            drHeating.Item(HeatingData.YRD_LCTN) = dr.Item(HeatingData.YRD_LCTN)

            Dim drHeatingcharge As DataRow()
            drHeatingcharge = dsHeatingCharge.Tables(HeatingData._HEATING_CHARGE).Select(String.Concat(HeatingData.GI_TRNSCTN_NO, "='", dr.Item(HeatingData.GI_TRNSCTN_NO), "'"))
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_CD) = drHeatingcharge(0).Item(HeatingData.HTNG_CD)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_STRT_DT) = drHeatingcharge(0).Item(HeatingData.HTNG_STRT_DT)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_STRT_TM) = drHeatingcharge(0).Item(HeatingData.HTNG_STRT_TM)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_END_DT) = drHeatingcharge(0).Item(HeatingData.HTNG_END_DT)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_END_TM) = drHeatingcharge(0).Item(HeatingData.HTNG_END_TM)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_TMPRTR) = drHeatingcharge(0).Item(HeatingData.HTNG_TMPRTR)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.TTL_HTN_PRD) = drHeatingcharge(0).Item(HeatingData.TTL_HTN_PRD)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.MIN_HTNG_RT_NC) = drHeatingcharge(0).Item(HeatingData.MIN_HTNG_RT_NC)
            Else
                For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                            dr.Item(GateinData.CSTMR_CD), "'"))
                    drHeating.Item(HeatingData.MIN_HTNG_RT_NC) = drCstmrDetail.Item(HeatingData.MIN_HTNG_RT_NC)
                    Exit For
                Next
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HRLY_CHRG_NC) = drHeatingcharge(0).Item(HeatingData.HRLY_CHRG_NC)
            Else
                For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                            dr.Item(GateinData.CSTMR_CD), "'"))
                    drHeating.Item(HeatingData.HRLY_CHRG_NC) = drCstmrDetail.Item(HeatingData.HRLY_CHRG_NC)
                    Exit For
                Next
            End If
            For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                            dr.Item(GateinData.CSTMR_CD), "'"))
                strDepotCurrency = drCstmrDetail.Item(CommonUIData.CRRNCY_CD)
                drHeating.Item(HeatingData.CSTMR_CRRNCY_CD) = drCstmrDetail.Item(HeatingData.CSTMR_CRRNCY_CD)
                Exit For
            Next
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.TTL_RT_NC) = drHeatingcharge(0).Item(HeatingData.TTL_RT_NC)
            End If
            If Not (dr.Item(HeatingData.MIN_HTNG_PRD_NC) Is DBNull.Value) Then
                drHeating.Item(HeatingData.MIN_HTNG_PRD_NC) = dr.Item(HeatingData.MIN_HTNG_PRD_NC)
            Else
                For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                            dr.Item(GateinData.CSTMR_CD), "'"))
                    drHeating.Item(HeatingData.MIN_HTNG_PRD_NC) = drCstmrDetail.Item(HeatingData.MIN_HTNG_PRD_NC)
                    Exit For
                Next
            End If
            dtHeating.Rows.Add(drHeating)
        Next

        dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clear()
        dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Merge(dtHeating)

        Return dsHeating

    End Function

    Public Function GateoutCount(ByVal UserName As String) As GateOutDataSet


        Dim dsGateOutData As New GateOutDataSet


        gateInMobile.DepotID(UserName)
        Dim strFilterName As String = ""
        Dim strFilterValue As String = ""
        Dim objCommonUI As New CommonUI()

        Dim dtRental As New DataTable
        Dim objGateOut As New GateOut()
        Dim objCommon As New CommonData
        Dim objCommonConfig As New ConfigSetting()
        Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
        Dim str_059Gateout As String = String.Empty
        Dim strGateOutApprovalProcess As String = Nothing
        Dim GOTList As New GOTList

        str_059Gateout = objCommonConfig.pub_GetConfigSingleValue("059", intDPT_ID)
        strGateOutApprovalProcess = objCommonConfig.pub_GetConfigSingleValue("066", intDPT_ID)


        'hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
        '

        Dim strCurrentSessionId As String = objCommon.GetSessionID()
        'objCommon.FlushLockDataByActivity(strCurrentSessionId, "Gate Out")

        ' hdnMode.Value = e.Parameters("Mode").ToString()

        Dim dsEqpStatus As New DataSet
        dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate Out", True, intDPT_ID)

        If str_059Gateout.Trim.ToUpper = "TRUE" Then
            dsGateOutData = objGateOut.pub_GetGateOutDetailsGWS(intDPT_ID)
        Else
            dsGateOutData = objGateOut.pub_GetGateOutDetails(intDPT_ID)
        End If



        If strGateOutApprovalProcess <> Nothing AndAlso CBool(strGateOutApprovalProcess) = True Then

            dsGateOutData = New GateOutDataSet
            dsGateOutData = objGateOut.pub_GetGateOutDetailsGWSWithApproval(intDPT_ID)

        End If





        Return dsGateOutData



    End Function

End Class
