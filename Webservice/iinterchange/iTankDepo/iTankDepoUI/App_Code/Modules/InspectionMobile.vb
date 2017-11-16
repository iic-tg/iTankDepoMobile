Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class InspectionMobile
    Inherits System.Web.Services.WebService



    Dim conn As New Dropdown_C
    Dim gateInMobile As New GateinMobile_C
    Dim objCommon As New CommonData
    Dim arraylist As New ArrayList
    Dim arrayOfInspection As New ArrayOfInspectionMobileModel
    Dim status As New UpdateStatus
    Dim objCleaning As New Cleaning
    Dim objCommonUI As New CommonUI
    Dim dsCleaning As New CleaningDataSet
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"


    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function IPendingList(ByVal UserName As String) As ArrayOfInspectionMobileModel



        gateInMobile.DepotID(UserName)
        Dim objCleaning As New Cleaning

        Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],EIR_NO,ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'CLN' AND ACTV_BT = 1"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        'Dim cleaningzz As New List(Of CleaningMobileModel)()

        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim inspection As New InspectionMobileModel
            inspection.EquipmentNo = dd.Item(GateinData.EQPMNT_NO).ToString()
            inspection.Customer = dd.Item(GateinData.CSTMR_CD).ToString()
            inspection.InDate = dd.Item(GateinData.GTN_DT).ToString()
            inspection.PrevoiusCargo = dd.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
            inspection.LastStatusDate = dd.Item(GateinData.ACTVTY_DT).ToString()
            inspection.AdditionalCleaningBit = dd.Item("ADDTNL_CLNNG_BT").ToString()
            inspection.CleaningRate = dd.Item("CLNNG_RT").ToString()
            inspection.EquipmentStatus = dd.Item(GateinData.EQPMNT_TYP_ID).ToString()
            inspection.EquipmentStatusType = dd.Item(GateinData.EQPMNT_TYP_CD).ToString()
            inspection.CustomerId = dd.Item(GateinData.CSTMR_ID).ToString()
            inspection.Remarks = dd.Item(GateinData.RMRKS_VC).ToString()
            inspection.GiTransactionNo = dd.Item(GateinData.GI_TRNSCTN_NO).ToString()
            inspection.CleaningId = dd.Item("CLNNG_ID").ToString()
            inspection.CleaningReferenceNo = dd.Item("CLNNG_RFRNC_NO").ToString()
            'inspection.EIR_NO = dd.Item("EIR_NO").ToString()

            'if dd.item("ADDTNL_CLNNG_BT") == 1 then
            'If dd.Item("ADDTNL_CLNNG_BT") = 1 Then

            '    Dim clngIn As String = "select [CLNNG_ID],[CLNNG_RFRNC_NO] from [CLEANING] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
            '    Dim dfghs1 As DataSet = New DataSet()
            '    conn.connection(clngIn).Fill(dfghs1)

            '    Dim dtPreaAdvicess As DataTable
            '    dtPreaAdvicess = dfghs1.Tables(0)

            '    cleaning.CleaningId = dtPreaAdvicess.Rows(0).Item("CLNNG_ID")
            '    cleaning.CleaningReferenceNo = dtPreaAdvicess.Rows(0).Item("CLNNG_RFRNC_NO")

            'Else
            '    cleaning.CleaningId = ""
            '    cleaning.CleaningReferenceNo = ""


            'End If

            'Dim blnCustomerProductRate As Integer
            'Dim blnProductRate As Integer
            'Dim blnSlabRate As Integer
            'blnCustomerProductRate = dd.Item("CLNNG_CSTMR_PRDCT_RT_BT").ToString()
            'blnProductRate = dd.Item("CLNNG_PRDCT_RT_BT").ToString()

            'blnSlabRate = objCleaning.pub_CheckSlabRateExists(dd.Item(CleaningData.CSTMR_ID), dd.Item(CleaningData.EQPMNT_TYP_ID))

            'If (blnProductRate = 1 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
            '    cleadning.SlabRate = 1
            'ElseIf (blnProductRate = 0 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
            '    cleadning.SlabRate = 1
            'Else
            '    cleadning.SlabRate = 0
            'End If

            'Dim ClngInstr As String = "select [Cleaning_Date] from [Cleaning_Instruction] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
            'Dim InstcDate As DataSet = New DataSet()
            'conn.connection(ClngInstr).Fill(InstcDate)

            'Dim dtPreaAdvicess1 As DataTable
            'dtPreaAdvicess1 = InstcDate.Tables(0)
            arraylist.Add(inspection)
            'cleaning.OriginalCleaningDate = dtPreaAdvicess1.Rows(0).Item("Cleaning_Date")
        Next


        arrayOfInspection.ArrayOfInspection = arraylist
        arrayOfInspection.status = "Success"
        arrayOfInspection.statusText = HttpContext.Current.Response.StatusDescription
        arrayOfInspection.stauscode = HttpContext.Current.Response.StatusCode

        Return arrayOfInspection

    End Function

    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function IPendingDetailList(ByVal UserName As String, ByVal euipmentNo As String) As ArrayOfInspectionMobileModel


        gateInMobile.DepotID(UserName)
        Dim objCleaning As New Cleaning

        Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],EIR_NO,[ADDTNL_CLNNG_BT],EIR_NO,ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'INS' AND ACTV_BT = 1"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        'Dim cleaningzz As New List(Of CleaningMobileModel)()

        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim inspection As New InspectionMobileModel
            inspection.EquipmentNo = dd.Item(GateinData.EQPMNT_NO).ToString()
            inspection.Customer = dd.Item(GateinData.CSTMR_CD).ToString()
            inspection.InDate = dd.Item(GateinData.GTN_DT).ToString()
            inspection.PrevoiusCargo = dd.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
            inspection.LastStatusDate = dd.Item(GateinData.ACTVTY_DT).ToString()
            inspection.AdditionalCleaningBit = dd.Item("ADDTNL_CLNNG_BT").ToString()
            inspection.CleaningRate = dd.Item("CLNNG_RT").ToString()
            inspection.EquipmentStatus = dd.Item(GateinData.EQPMNT_TYP_ID).ToString()
            inspection.EquipmentStatusType = dd.Item(GateinData.EQPMNT_TYP_CD).ToString()
            inspection.CustomerId = dd.Item(GateinData.CSTMR_ID).ToString()
            inspection.Remarks = dd.Item(GateinData.RMRKS_VC).ToString()
            inspection.GiTransactionNo = dd.Item(GateinData.GI_TRNSCTN_NO).ToString()
            inspection.CleaningId = dd.Item("CLNNG_ID").ToString()
            inspection.CleaningReferenceNo = dd.Item("CLNNG_RFRNC_NO").ToString()
            inspection.EIR_NO = dd.Item("EIR_NO").ToString()

            Dim ClngInstr As String = "select [Cleaning_Date],[Inspection_Date],[Clean_UnClean],[Seal_Nos] from [Cleaning_Instruction] where [Equipment_No]='" + dd.Item(GateinData.EQPMNT_NO) + "'"
            Dim InstcDate As DataSet = conn.connection(ClngInstr)
            'conn.connection(ClngInstr).Fill(InstcDate)

            Dim dtPreaAdvicess1 As DataTable
            dtPreaAdvicess1 = InstcDate.Tables(0)

            inspection.OriginalCleaningDate = dtPreaAdvicess1.Rows(0).Item("Cleaning_Date").ToString()
            inspection.OriginalInspectionDate = dtPreaAdvicess1.Rows(0).Item("Inspection_Date").ToString()
            inspection.Clean_Unclean = dtPreaAdvicess1.Rows(0).Item("Clean_UnClean").ToString()
            inspection.Seal_No = dtPreaAdvicess1.Rows(0).Item("Seal_Nos").ToString()
            arraylist.Add(inspection)
            'cleaning.OriginalCleaningDate = dtPreaAdvicess1.Rows(0).Item("Cleaning_Date")
        Next


        arrayOfInspection.ArrayOfInspection = arraylist
        arrayOfInspection.status = "Success"
        arrayOfInspection.statusText = HttpContext.Current.Response.StatusDescription
        arrayOfInspection.stauscode = HttpContext.Current.Response.StatusCode

        Return arrayOfInspection

    End Function


    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function CreateInspection(ByVal bv_strCleaningId As String, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strCrtfct_No As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_strCleaningRate As String, _
                                   ByVal bv_strOriginalCleaningDate As String, _
                                   ByVal bv_strLastCleaningDate As String, _
                                   ByVal bv_strOriginalInspectedDate As String, _
                                   ByVal bv_strLastInspectedDate As String, _
                                   ByVal bv_strEquipmentStatus As String, _
                                   ByVal bv_strEquipmentStatusCD As String, _
                                   ByVal bv_strCleanedFor As String, _
                                   ByVal bv_strLocOfCleaning As String, _
                                   ByVal bv_strEquipmentCleaningStatus1 As String, _
                                   ByVal bv_strEquipmentCleaningStatus1CD As String, _
                                   ByVal bv_strEquipmentCleaningStatus2 As String, _
                                   ByVal bv_strEquipmentCleaningStatus2CD As String, _
                                   ByVal bv_strEquipmentCondition As String, _
                                   ByVal bv_strEquipmentConditionCD As String, _
                                   ByVal bv_strValveandFittingCondition As String, _
                                   ByVal bv_strValveandFittingConditionCD As String, _
                                   ByVal bv_strInvoicingTo As String, _
                                   ByVal bv_strInvoicingToCD As String, _
                                   ByVal bv_strManLidSealNo As String, _
                                   ByVal bv_strTopSealNo As String, _
                                   ByVal bv_strBottomSealNo As String, _
                                   ByVal bv_strCustomerReferenceNo As String, _
                                   ByVal bv_strCleaningReferenceNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_CustomerId As String, _
                                   ByVal bv_GateInDate As String, _
                                   ByVal bv_strGI_TRNSCTN_NO As String, _
                                   ByVal bv_intActivityId As Integer, _
                                   ByVal bv_blnAdditionalCleaningFlag As Boolean, _
                                   ByVal bv_strAdditionalCleaning_Status As String, _
                                   ByVal UserName As String) As UpdateStatus



        Try

            gateInMobile.DepotID(UserName)
            Dim objCommon As New CommonData
            Dim lngCreated As Long

            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strActivitySubmit As String = String.Empty



            dsCleaning = objCleaning.pub_GetActivityStatusDetails(bv_strEquipmentNo, intDPT_ID, 0, bv_strGI_TRNSCTN_NO)

            lngCreated = objCleaning.pub_Create_Inspection(CLng(bv_strCleaningId), _
                                                        bv_strEquipmentNo, _
                                                        bv_strCrtfct_No, _
                                                        bv_strChemicalName, _
                                                        bv_strCleaningRate, _
                                                        CommonWeb.iDat(bv_strOriginalCleaningDate), _
                                                        CommonWeb.iDat(bv_strLastCleaningDate), _
                                                        CommonWeb.iDat(bv_strOriginalInspectedDate), _
                                                        CommonWeb.iDat(bv_strLastInspectedDate), _
                                                        CommonWeb.iLng(bv_strEquipmentStatus), _
                                                        bv_strEquipmentStatusCD, _
                                                        bv_strCleanedFor, _
                                                        bv_strLocOfCleaning, _
                                                        CommonWeb.iLng(bv_strEquipmentCleaningStatus1), _
                                                        bv_strEquipmentCleaningStatus1CD, _
                                                        CommonWeb.iLng(bv_strEquipmentCleaningStatus2), _
                                                        bv_strEquipmentCleaningStatus2CD, _
                                                        CommonWeb.iLng(bv_strEquipmentCondition), _
                                                        bv_strEquipmentConditionCD, _
                                                        CommonWeb.iLng(bv_strValveandFittingCondition), _
                                                        bv_strValveandFittingConditionCD, _
                                                        CommonWeb.iLng(bv_strInvoicingTo), _
                                                        bv_strInvoicingToCD, _
                                                        bv_strManLidSealNo, _
                                                        bv_strTopSealNo, _
                                                        bv_strBottomSealNo, _
                                                        bv_strCustomerReferenceNo, _
                                                        bv_strCleaningReferenceNo, _
                                                        bv_strRemarks, _
                                                        bv_CustomerId, _
                                                        CommonWeb.iDat(bv_GateInDate), _
                                                        bv_strGI_TRNSCTN_NO, _
                                                        intDPT_ID, _
                                                        objCommon.GetCurrentUserName(), _
                                                        CDate(objCommon.GetCurrentDate()), _
                                                        dsCleaning, _
                                                        strActivitySubmit, _
                                                        CInt(bv_intActivityId), _
                                                     bv_blnAdditionalCleaningFlag, _
                                                        bv_strAdditionalCleaning_Status)


            If lngCreated <> Nothing Then

                status.Status = "Success"
                status.StatusCode = HttpContext.Current.Response.StatusCode
                status.StatusText = HttpContext.Current.Response.StatusDescription
                Return status
                Exit Function


            End If

            'Status.Status = "Success"
            'Status.StatusCode = HttpContext.Current.Response.StatusCode
            'Status.StatusText = HttpContext.Current.Response.StatusDescription



        Catch ex As Exception
            'pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
            status.Status = ex.Message
            status.StatusCode = HttpContext.Current.Response.StatusCode
            status.StatusText = HttpContext.Current.Response.StatusDescription
            Return status
        End Try

    End Function



    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function ModifyInspection(ByVal bv_strCleaningID As String, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByRef bv_strCrtfct_No As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_strCleaningRate As String, _
                                   ByVal bv_strOriginalCleaningDate As String, _
                                   ByVal bv_strLastCleaningDate As String, _
                                   ByVal bv_strOriginalInspectedDate As String, _
                                   ByVal bv_strLastInspectedDate As String, _
                                   ByVal bv_strEquipmentStatus As String, _
                                   ByVal bv_strEquipmentStatusCD As String, _
                                   ByVal bv_strCleanedFor As String, _
                                   ByVal bv_strLocOfCleaning As String, _
                                   ByVal bv_strEquipmentCleaningStatus1 As String, _
                                   ByVal bv_strEquipmentCleaningStatus1CD As String, _
                                   ByVal bv_strEquipmentCleaningStatus2 As String, _
                                   ByVal bv_strEquipmentCleaningStatus2CD As String, _
                                   ByVal bv_strEquipmentCondition As String, _
                                   ByVal bv_strEquipmentConditionCD As String, _
                                   ByVal bv_strValveandFittingCondition As String, _
                                   ByVal bv_strValveandFittingConditionCD As String, _
                                   ByVal bv_strInvoicingTo As String, _
                                   ByVal bv_strInvoicingToCD As String, _
                                   ByVal bv_strManLidSealNo As String, _
                                   ByVal bv_strTopSealNo As String, _
                                   ByVal bv_strBottomSealNo As String, _
                                   ByVal bv_strCustomerReferenceNo As String, _
                                   ByVal bv_strCleaningReferenceNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_CustomerId As String, _
                                   ByVal bv_GateInDate As String, _
                                   ByVal bv_strGI_TRNSCTN_NO As String, _
                                   ByVal bv_intActivityId As Integer, _
                                   ByVal bv_blnAdditionalCleaningFlag As Boolean, _
                                   ByVal bv_NextAcitivity As String, _
                                   ByVal UserName As String) As UpdateStatus


        Try


            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strActivitySubmit As String = String.Empty
            Dim result As Boolean

            Dim blnBill As Boolean = False
            blnBill = objCleaning.pub_GetCleaningChargeBilled(bv_strEquipmentNo, intDPT_ID, CLng(bv_strCleaningID), bv_strGI_TRNSCTN_NO, bv_strCleaningRate)
            If blnBill = False Then
                'pub_SetCallbackError("Cleaning Invoice has been raised, Hence cannot change the cleaning rate.")
                'pub_SetCallbackStatus(False)
                status.Status = "Cleaning Invoice has been raised, Hence cannot change the cleaning rate."
                Return status
                Exit Function
            End If
            ''
            dsCleaning = objCleaning.pub_GetActivityStatusDetails(bv_strEquipmentNo, intDPT_ID, bv_strCleaningID, bv_strGI_TRNSCTN_NO)
            result = objCleaning.ModifyCleaning_Inspection(CommonWeb.iLng(bv_strCleaningID), _
                                       bv_strEquipmentNo, _
                                       bv_strCrtfct_No, _
                                       bv_strChemicalName, _
                                       CommonWeb.iDbl(bv_strCleaningRate), _
                                       CommonWeb.iDat(bv_strOriginalCleaningDate), _
                                       CommonWeb.iDat(bv_strLastCleaningDate), _
                                       CommonWeb.iDat(bv_strOriginalInspectedDate), _
                                       CommonWeb.iDat(bv_strLastInspectedDate), _
                                       CommonWeb.iLng(bv_strEquipmentStatus), _
                                       bv_strEquipmentStatusCD, _
                                       bv_strCleanedFor, _
                                       bv_strLocOfCleaning, _
                                       CommonWeb.iLng(bv_strEquipmentCleaningStatus1), _
                                       bv_strEquipmentCleaningStatus1CD, _
                                       CommonWeb.iLng(bv_strEquipmentCleaningStatus2), _
                                       bv_strEquipmentCleaningStatus2CD, _
                                       CommonWeb.iLng(bv_strEquipmentCondition), _
                                       bv_strEquipmentConditionCD, _
                                       CommonWeb.iLng(bv_strValveandFittingCondition), _
                                       bv_strValveandFittingConditionCD, _
                                       CommonWeb.iLng(bv_strInvoicingTo), _
                                       bv_strInvoicingToCD, _
                                       bv_strManLidSealNo, _
                                       bv_strTopSealNo, _
                                       bv_strBottomSealNo, _
                                       bv_strCustomerReferenceNo, _
                                       bv_strCleaningReferenceNo, _
                                       bv_strRemarks, _
                                       bv_CustomerId, _
                                       CommonWeb.iDat(bv_GateInDate), _
                                       bv_strGI_TRNSCTN_NO, _
                                       intDPT_ID, _
                                       objCommon.GetCurrentUserName(), _
                                       CDate(objCommon.GetCurrentDate()), _
                                       dsCleaning, _
                                       strActivitySubmit, _
                                       bv_intActivityId, _
                                       bv_blnAdditionalCleaningFlag, bv_NextAcitivity)


            If result Then
                status.Status = "Success"
                status.StatusCode = HttpContext.Current.Response.StatusCode
                status.StatusText = HttpContext.Current.Response.StatusDescription
                Return status
                Exit Function
            End If


        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                               MethodBase.GetCurrentMethod.Name, ex)
            status.Status = ex.Message

            status.StatusCode = HttpContext.Current.Response.StatusCode
            status.StatusText = HttpContext.Current.Response.StatusDescription
            Return status


        End Try

    End Function



    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function VaidateLastActivityDate(ByVal bv_strEquipmentNo As String, ByVal bv_strEventDate As String, ByVal bv_strOriginalDate As String) As UpdateStatus
        Try
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objCommon As New CommonData
            If (Not (CDate(bv_strEventDate) >= CDate(bv_strOriginalDate))) Then
                blnDateValid = True
            End If
            Dim Originaldate As DateTime = (bv_strOriginalDate)
            If blnDateValid = True Then
                'pub_SetCallbackReturnValue("Error", String.Concat("Latest Inspection Date should be greater than or equal to Original Inspection Date (", Originaldate.ToString("dd-MMM-yyyy"), ")"))
                status.Status = "Latest Inspection Date should be greater than or equal to Original Inspection Date (" + Originaldate.ToString("dd-MMM-yyyy") + ")"


                Return status
                Exit Function
            End If

            blnDateValid = objCommonUI.pub_GetEquipmentPreviousActivityDate(bv_strEquipmentNo, _
                                                                        CDate(bv_strEventDate), _
                                                                        dtPreviousDate, _
                                                                        "Inspection", _
                                                                        CInt(objCommon.GetDepotID()))
            If blnDateValid = True Then
                status.Status = "Latest Inspection Date Should be greater than or equal to Previous Activity Date (" + dtPreviousDate.ToString("dd-MMM-yyyy") + ")"
                Return status
                Exit Function
                'pub_SetCallbackReturnValue("Error", String.Concat("Latest Inspection Date Should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), ")"))
            End If

            status.Status = "Success"
            Return status

        Catch ex As Exception
            'pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            status.Status = ex.Message
            Return status
            Exit Function
        End Try
    End Function




    <WebMethod(enableSession:=True)> _
 <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function MySubmit(ByVal UserName As String) As ArrayOfInspectionMobileModel


        gateInMobile.DepotID(UserName)
        Dim objCleaning As New Cleaning

        Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],EIR_NO,ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'INS' AND ACTV_BT = 1"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        'Dim cleaningzz As New List(Of CleaningMobileModel)()

        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim inspection As New InspectionMobileModel
            inspection.EquipmentNo = dd.Item(GateinData.EQPMNT_NO).ToString()
            inspection.Customer = dd.Item(GateinData.CSTMR_CD).ToString()
            inspection.InDate = dd.Item(GateinData.GTN_DT).ToString()
            inspection.PrevoiusCargo = dd.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
            inspection.LastStatusDate = dd.Item(GateinData.ACTVTY_DT).ToString()
            inspection.AdditionalCleaningBit = dd.Item("ADDTNL_CLNNG_BT").ToString()
            inspection.CleaningRate = dd.Item("CLNNG_RT").ToString()
            inspection.EquipmentStatus = dd.Item(GateinData.EQPMNT_TYP_ID).ToString()
            inspection.EquipmentStatusType = dd.Item(GateinData.EQPMNT_TYP_CD).ToString()
            inspection.CustomerId = dd.Item(GateinData.CSTMR_ID).ToString()
            inspection.Remarks = dd.Item(GateinData.RMRKS_VC).ToString()
            inspection.GiTransactionNo = dd.Item(GateinData.GI_TRNSCTN_NO).ToString()
            inspection.CleaningId = dd.Item("CLNNG_ID").ToString()
            inspection.CleaningReferenceNo = dd.Item("CLNNG_RFRNC_NO").ToString()
            'inspection.EIR_NO = dd.Item("EIR_NO").ToString()


            arraylist.Add(inspection)
            'cleaning.OriginalCleaningDate = dtPreaAdvicess1.Rows(0).Item("Cleaning_Date")
        Next


        arrayOfInspection.ArrayOfInspection = arraylist
        arrayOfInspection.status = "Success"
        arrayOfInspection.statusText = HttpContext.Current.Response.StatusDescription
        arrayOfInspection.stauscode = HttpContext.Current.Response.StatusCode

        Return arrayOfInspection

    End Function



    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function filter(ByVal filterType As String, ByVal filterCondition As String, ByVal filterValue As String, ByVal UserName As String, ByVal Mode As String) As ListModel

        Dim objCommonData As New CommonData
        gateInMobile.DepotID(UserName)
        Dim listVlaues As New ListModel
        Dim arraylist As New ArrayList
        Dim filterNewConditon As String
        Dim ds As DataSet
        Dim dtPreaAdvice As DataTable
        Dim query As String
        If filterCondition = "Similar" Or filterCondition = "Contains" Then
            filterNewConditon = "LIKE"
        Else
            filterNewConditon = "Not LIKE"
        End If

        ds = New DataSet()
        Select Case Mode
            Case strNew
                If filterCondition = "Equals" Then

                    query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'CLN' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"

                    'query = "SELECT " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + "='" + filterValue + "'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                Else

                    query = "SELECT distinct " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'CLN' AND ACTV_BT = 1 AND " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"
                    'query = "SELECT distinct " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                End If

            Case strEdit

                If filterCondition = "Equals" Then

                    query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'INS' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                Else
                    query = "SELECT distinct " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'INS' AND ACTV_BT = 1 AND " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


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



    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function SearchList(ByVal UserName As String, ByVal SearchValues As SearchValues, ByVal filterType As String, ByVal Mode As String) As ArrayOfInspectionMobileModel


        gateInMobile.DepotID(UserName)
        Dim objCleaning As New Cleaning
        Dim query As String = ""

        If Mode = "new" Then
            query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'CLN' AND ACTV_BT = 1"
        Else
            query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'INS' AND ACTV_BT = 1"
        End If


        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)

        If Mode = "new" Then

            For Each dd1 In SearchValues.SearchValues

                ' Dim dd() As DataRow = ds.Tables(0).Select(String.Concat(filterType, "='", dd1.values, "'"))
                For Each dr1 As DataRow In ds.Tables(0).Select(String.Concat(filterType, "='", dd1.values, "'"))
                    Dim inspection As New InspectionMobileModel
                    inspection.EquipmentNo = dr1.Item(GateinData.EQPMNT_NO).ToString()
                    inspection.Customer = dr1.Item(GateinData.CSTMR_CD).ToString()
                    inspection.InDate = dr1.Item(GateinData.GTN_DT).ToString()
                    inspection.PrevoiusCargo = dr1.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
                    inspection.LastStatusDate = dr1.Item(GateinData.ACTVTY_DT).ToString()
                    inspection.AdditionalCleaningBit = dr1.Item("ADDTNL_CLNNG_BT").ToString()
                    inspection.CleaningRate = dr1.Item("CLNNG_RT").ToString()
                    inspection.EquipmentStatus = dr1.Item(GateinData.EQPMNT_TYP_ID).ToString()
                    inspection.EquipmentStatusType = dr1.Item(GateinData.EQPMNT_TYP_CD).ToString()
                    inspection.CustomerId = dr1.Item(GateinData.CSTMR_ID).ToString()
                    inspection.Remarks = dr1.Item(GateinData.RMRKS_VC).ToString()
                    inspection.GiTransactionNo = dr1.Item(GateinData.GI_TRNSCTN_NO).ToString()
                    inspection.CleaningId = dr1.Item("CLNNG_ID").ToString()
                    inspection.CleaningReferenceNo = dr1.Item("CLNNG_RFRNC_NO").ToString()
                    'inspection.EIR_NO = dd.Item("EIR_NO").ToString()

                    'if dd.item("ADDTNL_CLNNG_BT") == 1 then
                    'If dd.Item("ADDTNL_CLNNG_BT") = 1 Then

                    '    Dim clngIn As String = "select [CLNNG_ID],[CLNNG_RFRNC_NO] from [CLEANING] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
                    '    Dim dfghs1 As DataSet = New DataSet()
                    '    conn.connection(clngIn).Fill(dfghs1)

                    '    Dim dtPreaAdvicess As DataTable
                    '    dtPreaAdvicess = dfghs1.Tables(0)

                    '    cleaning.CleaningId = dtPreaAdvicess.Rows(0).Item("CLNNG_ID")
                    '    cleaning.CleaningReferenceNo = dtPreaAdvicess.Rows(0).Item("CLNNG_RFRNC_NO")

                    'Else
                    '    cleaning.CleaningId = ""
                    '    cleaning.CleaningReferenceNo = ""


                    'End If

                    'Dim blnCustomerProductRate As Integer
                    'Dim blnProductRate As Integer
                    'Dim blnSlabRate As Integer
                    'blnCustomerProductRate = dd.Item("CLNNG_CSTMR_PRDCT_RT_BT").ToString()
                    'blnProductRate = dd.Item("CLNNG_PRDCT_RT_BT").ToString()

                    'blnSlabRate = objCleaning.pub_CheckSlabRateExists(dd.Item(CleaningData.CSTMR_ID), dd.Item(CleaningData.EQPMNT_TYP_ID))

                    'If (blnProductRate = 1 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
                    '    cleadning.SlabRate = 1
                    'ElseIf (blnProductRate = 0 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
                    '    cleadning.SlabRate = 1
                    'Else
                    '    cleadning.SlabRate = 0
                    'End If

                    'Dim ClngInstr As String = "select [Cleaning_Date] from [Cleaning_Instruction] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
                    'Dim InstcDate As DataSet = New DataSet()
                    'conn.connection(ClngInstr).Fill(InstcDate)

                    'Dim dtPreaAdvicess1 As DataTable
                    'dtPreaAdvicess1 = InstcDate.Tables(0)
                    arraylist.Add(inspection)

                Next

            Next

        Else


            For Each dd1 In SearchValues.SearchValues

                ' Dim dd() As DataRow = ds.Tables(0).Select(String.Concat(filterType, "='", dd1.values, "'"))
                For Each dr1 As DataRow In ds.Tables(0).Select(String.Concat(filterType, "='", dd1.values, "'"))
                    Dim inspection As New InspectionMobileModel
                    inspection.EquipmentNo = dr1.Item(GateinData.EQPMNT_NO).ToString()
                    inspection.Customer = dr1.Item(GateinData.CSTMR_CD).ToString()
                    inspection.InDate = dr1.Item(GateinData.GTN_DT).ToString()
                    inspection.PrevoiusCargo = dr1.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
                    inspection.LastStatusDate = dr1.Item(GateinData.ACTVTY_DT).ToString()
                    inspection.AdditionalCleaningBit = dr1.Item("ADDTNL_CLNNG_BT").ToString()
                    inspection.CleaningRate = dr1.Item("CLNNG_RT").ToString()
                    inspection.EquipmentStatus = dr1.Item(GateinData.EQPMNT_TYP_ID).ToString()
                    inspection.EquipmentStatusType = dr1.Item(GateinData.EQPMNT_TYP_CD).ToString()
                    inspection.CustomerId = dr1.Item(GateinData.CSTMR_ID).ToString()
                    inspection.Remarks = dr1.Item(GateinData.RMRKS_VC).ToString()
                    inspection.GiTransactionNo = dr1.Item(GateinData.GI_TRNSCTN_NO).ToString()
                    inspection.CleaningId = dr1.Item("CLNNG_ID").ToString()
                    inspection.CleaningReferenceNo = dr1.Item("CLNNG_RFRNC_NO").ToString()
                    'inspection.EIR_NO = dd.Item("EIR_NO").ToString()


                    arraylist.Add(inspection)

                Next

            Next
            'Dim cleaningzz As New List(Of CleaningMobileModel)()

        End If

        arrayOfInspection.ArrayOfInspection = arraylist
        arrayOfInspection.status = "Success"
        arrayOfInspection.statusText = HttpContext.Current.Response.StatusDescription
        arrayOfInspection.stauscode = HttpContext.Current.Response.StatusCode

        Return arrayOfInspection
    End Function

End Class