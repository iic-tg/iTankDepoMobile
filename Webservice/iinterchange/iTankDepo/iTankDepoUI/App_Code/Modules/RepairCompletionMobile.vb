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
Public Class RepairCompletionMobile
    Inherits System.Web.Services.WebService
    Dim dsRepairCompletion As New RepairCompletionDataSet
    Dim gateInMobile As New GateinMobile_C
    Dim conn As New Dropdown_C
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"


    <WebMethod(enableSession:=True)> _
     <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function RepairList(ByVal UserName As String, ByVal Mode As String) As RC

        Try

            Dim RC As New ArrayList
            gateInMobile.DepotID(UserName)
            Dim objcommon As New CommonData()
            Dim objCommonConfig As New ConfigSetting()
            Dim dtActivityStatus As New DataTable
            Dim objRepairCompletion As New RepairCompletion()
            Dim strMode As String = ""
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            Dim bln_056GwsBit As Boolean
            Dim str_056GWSKey As String
            str_056GWSKey = objCommonConfig.pub_GetConfigSingleValue("056", intDepotID)
            bln_056GwsBit = objCommonConfig.IsKeyExists



            Dim objCommonUI As New CommonUI()
            Dim dsEqpStatus As New DataSet
            Dim blnShowEqStatus As Boolean = False
            dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Repair Completion", True, intDepotID)

            If Mode = "new" Then

                If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                    blnShowEqStatus = True
                End If
                If bln_056GwsBit Then
                    If str_056GWSKey.ToLower = "true" Then
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimatePendingByDepotId(intDepotID)
                    Else
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimatePendingJTSByDepotId(intDepotID)
                    End If
                End If

                For Each drRepairCompletion As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Rows
                    drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                    drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM) = "00:00"
                    If bln_056GwsBit Then
                        If str_056GWSKey.ToLower = "true" Then
                            drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM) = DateTime.Now.ToString("H:mm")
                        End If
                    End If
                Next


            ElseIf Mode = "edit" Then


                If bln_056GwsBit Then
                    If str_056GWSKey.ToLower = "true" Then
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimateMySubmitByDepotId(intDepotID)
                    Else
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimateMySubmitJTSByDepotId(intDepotID)
                    End If
                End If


            End If




            For Each dt As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Rows

                ' Dim query2 As String = "select a.*,b.ACTVTY_DT  as CHK_ACTVTY_DT from(SELECT RPR_ESTMT_ID, RPR_ESTMT_NO, GI_TRNSCTN_NO, RVSN_NO, RPR_ESTMT_DT, EQPMNT_NO, EQPMNT_STTS_ID, (SELECT EQPMNT_STTS_CD FROM EQUIPMENT_STATUS WHERE RE.EQPMNT_STTS_ID= EQPMNT_STTS_ID AND DPT_ID = (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))AS EQPMNT_STTS_CD, CSTMR_ID, (SELECT CSTMR_CD FROM CUSTOMER WHERE RE.CSTMR_ID=CSTMR_ID AND DPT_ID = (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153)) AS CSTMR_CD, (SELECT CSTMR_NAM FROM CUSTOMER WHERE RE.CSTMR_ID = CSTMR_ID AND DPT_ID = (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))AS CSTMR_NAM, (SELECT CSTMR_CRRNCY_ID FROM CUSTOMER WHERE RE.CSTMR_ID = CSTMR_ID AND DPT_ID = (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))AS CSTMR_CRRNCY_ID, DPT_ID, (SELECT DPT_CD FROM DEPOT WHERE RE.DPT_ID=DPT_ID) AS DPT_CD, (SELECT DPT_NAM FROM DEPOT WHERE RE.DPT_ID=DPT_ID) AS DPT_NAM, (SELECT CRRNCY_ID FROM BANK_DETAIL WHERE RE.DPT_ID=DPT_ID AND BNK_TYP_ID=44) AS DPT_CRRNCY_ID, SRVYR_NM, LBR_RT_NC, RPR_TYP_ID,  (SELECT ENM_CD FROM ENUM WHERE RE.RPR_TYP_ID= ENM_ID) AS RPR_TYP_CD,  CRT_OF_CLNLNSS_BT,  INVCNG_PRTY_ID, (SELECT INVCNG_PRTY_CD FROM INVOICING_PARTY WHERE RE.INVCNG_PRTY_ID=INVCNG_PRTY_ID AND DPT_ID = (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153)) AS INVCNG_PRTY_CD,  (SELECT INVCNG_PRTY_NM FROM INVOICING_PARTY WHERE RE.INVCNG_PRTY_ID=INVCNG_PRTY_ID AND DPT_ID = (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153)) AS INVCNG_PRTY_NM,  (SELECT BS_CRRNCY_ID FROM INVOICING_PARTY WHERE RE.INVCNG_PRTY_ID=INVCNG_PRTY_ID AND DPT_ID = (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153)) AS INVCNG_PRTY_CRRNCY_ID,  (SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID = (SELECT BS_CRRNCY_ID FROM INVOICING_PARTY WHERE RE.INVCNG_PRTY_ID=INVCNG_PRTY_ID AND DPT_ID = (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))) AS PRTY_CRRNCY_CD,  (CASE WHEN INVCNG_PRTY_ID IS NOT NULL AND (SELECT BS_CRRNCY_ID FROM INVOICING_PARTY WHERE RE.INVCNG_PRTY_ID=INVCNG_PRTY_ID AND DPT_ID = (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153)) = (SELECT CRRNCY_ID FROM BANK_DETAIL WHERE RE.DPT_ID=DPT_ID AND BNK_TYP_ID=44) THEN 1.00 	ELSE (SELECT EXCHNG_RT_PR_UNT_NC FROM EXCHANGE_RATE WHERE FRM_CRRNCY_ID = (SELECT CRRNCY_ID FROM BANK_DETAIL WHERE RE.DPT_ID=DPT_ID AND BNK_TYP_ID=44) AND TO_CRRNCY_ID= (SELECT BS_CRRNCY_ID FROM INVOICING_PARTY WHERE RE.INVCNG_PRTY_ID=INVCNG_PRTY_ID AND DPT_ID =(SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))) END) AS PRTY_EXCHANGE_RATE_NC, (CASE WHEN CSTMR_ID IS NOT NULL AND (SELECT CSTMR_CRRNCY_ID FROM CUSTOMER WHERE RE.CSTMR_ID=CSTMR_ID AND DPT_ID = RE.DPT_ID) = (SELECT CRRNCY_ID FROM BANK_DETAIL WHERE RE.DPT_ID=DPT_ID AND BNK_TYP_ID=44) THEN 1.00 	ELSE (SELECT EXCHNG_RT_PR_UNT_NC FROM EXCHANGE_RATE WHERE FRM_CRRNCY_ID = (SELECT CRRNCY_ID FROM BANK_DETAIL WHERE RE.DPT_ID=DPT_ID AND BNK_TYP_ID=44) AND TO_CRRNCY_ID= (SELECT CSTMR_CRRNCY_ID FROM CUSTOMER WHERE RE.CSTMR_ID=CSTMR_ID AND DPT_ID = (SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))) END) AS CSTMR_EXCHANGE_RATE_NC, ORGNL_ESTMN_DT, ESTMTN_TTL_NC, OWNR_APPRVL_RF, RMRKS_VC, CRTD_BY, CRTD_DT, MDFD_BY, MDFD_DT, ACTVTY_DT, ACTVTY_NM, APPRVL_AMNT_NC, ORGNL_ESTMTN_AMNT_NC, CSTMR_ESTMTN_TTL_NC, CSTMR_APPRVL_AMNT_NC, ACTL_MN_HR_NC, (CASE WHEN (SELECT COUNT(TRCKNG_ID) FROM TRACKING WHERE ACTVTY_NAM ='REPAIR ESTIMATE' AND ACTVTY_NO IN(SELECT ACTVTY_NO FROM BULK_EMAIL_DETAIL WHERE EQPMNT_NO = RE.EQPMNT_NO AND ACTVTY_NAM ='REPAIR ESTIMATE' AND GI_TRNSCTN_NO= RE.GI_TRNSCTN_NO  and ACTVTY_NO=RE.RPR_ESTMT_ID)) >0 THEN 'YES' 	ELSE 'NO' END)AS MAIL_SEND, PRTY_APPRVL_RF, (SELECT COUNT(RPR_ESTMT_ID) FROM ATTACHMENT WHERE RPR_ESTMT_ID = RE.RPR_ESTMT_ID) AS COUNT_ATTACH, SCHDL_DT, (SELECT ADDTNL_CLNNG_BT FROM V_ACTIVITY_STATUS VAS WHERE VAS.EQPMNT_NO=RE.EQPMNT_NO AND VAS.GI_TRNSCTN_NO=RE.GI_TRNSCTN_NO AND VAS.DPT_ID=RE.DPT_ID) AS ADDTNL_CLNNG_BT, (SELECT ADDTNL_CLNNG_FLG FROM V_ACTIVITY_STATUS VAS WHERE VAS.EQPMNT_NO=RE.EQPMNT_NO AND VAS.GI_TRNSCTN_NO=RE.GI_TRNSCTN_NO AND VAS.DPT_ID=RE.DPT_ID) AS ADDTNL_CLNNG_FLG, (SELECT EQPMNT_TYP_ID FROM GATEIN GI WHERE RE.GI_TRNSCTN_NO=GI.GI_TRNSCTN_NO AND GI.DPT_ID=RE.DPT_ID) AS 'EQPMNT_TYP_ID', (SELECT EQPMNT_CD_ID FROM GATEIN GI WHERE RE.GI_TRNSCTN_NO=GI.GI_TRNSCTN_NO AND GI.DPT_ID=RE.DPT_ID) AS 'EQPMNT_CD_ID', UNT_ID, MSR_ID, PRV_ONH_LCTN, (SELECT YRD_LCTN FROM ACTIVITY_STATUS GI WHERE RE.GI_TRNSCTN_NO=GI.GI_TRNSCTN_NO AND GI.DPT_ID=RE.DPT_ID) AS 'YRD_LCTN', CNSGNE, (SELECT ENM_CD FROM ENUM WHERE ENM_ID =RE.BLL_TO)BLL_CD, BLL_TO, AGNT_ID, PRV_ONH_LCTN_DT, TX_RT_PRCNT, (SELECT UNT_CD FROM UNIT WHERE UNT_ID=RE.UNT_ID) AS 'UNT_CD', (SELECT MSR_CD FROM MEASURE WHERE MSR_ID=RE.MSR_ID) AS 'MSR_CD', (SELECT PRT_CD FROM PORT WHERE PRT_ID=RE.PRV_ONH_LCTN) AS 'PRT_CD', (SELECT AGNT_NAM FROM AGENT AGNT WHERE AGNT.AGNT_ID=RE.AGNT_ID) AS 'AGNT_NAM', (SELECT AGNT_CD FROM AGENT AGNT WHERE AGNT.AGNT_ID=RE.AGNT_ID) AS 'AGNT_CD', (SELECT EQPMNT_TYP_CD FROM EQUIPMENT_TYPE ETYPE WHERE ETYPE.EQPMNT_TYP_ID IN (SELECT EQPMNT_TYP_ID FROM GATEIN GI WHERE RE.GI_TRNSCTN_NO=GI.GI_TRNSCTN_NO AND GI.DPT_ID=(SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))) AS 'EQPMNT_TYP_CD', (SELECT EQPMNT_CD_CD FROM EQUIPMENT_TYPE ETYPE WHERE ETYPE.EQPMNT_TYP_ID IN (SELECT EQPMNT_TYP_ID FROM GATEIN GI WHERE RE.GI_TRNSCTN_NO=GI.GI_TRNSCTN_NO AND GI.DPT_ID=(SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153))) AS EQPMNT_CD_CD, STTS_ID, (SELECT STTS_CD FROM [STATUS] SS WHERE SS.STTS_ID=(SELECT DPT_ID FROM DEPOT WHERE ORGNZTN_TYP_ID=153)) AS STTS_CD FROM REPAIR_ESTIMATE AS RE )as a inner join  ( select * from repair_estimate WHERE ACTVTY_NM='Repair Approval')as b on a.EQPMNT_NO=b.EQPMNT_NO"

                Dim query2 As String = "select ACTVTY_DT as CHK_ACTVTY_DT from repair_estimate WHERE ACTVTY_NM='Repair Approval' and EQPMNT_NO='" + dt.Item("EQPMNT_NO").ToString() + "'"

                Dim ds2 As DataSet = conn.connection(query2)

                Dim dtPreaAdvice2 As DataTable
                dtPreaAdvice2 = ds2.Tables(0)

                Dim arrayLineItem As New ArrayList

                For Each dt1 As DataRow In dtPreaAdvice2.Rows


                    Dim RepairCompletionMobileModel As New RepairCompletionMobileModel

                    RepairCompletionMobileModel.EquipmentNo = dt.Item("EQPMNT_NO").ToString()
                    RepairCompletionMobileModel.Type = dt.Item("EQPMNT_TYP_CD").ToString()
                    RepairCompletionMobileModel.Code = dt.Item("EQPMNT_CD_CD").ToString()
                    RepairCompletionMobileModel.Status = dt.Item("EQPMNT_STTS_CD").ToString()
                    RepairCompletionMobileModel.EstimateNo = dt.Item("RPR_ESTMT_NO").ToString()
                    RepairCompletionMobileModel.Customer = dt.Item("CSTMR_CD").ToString()
                    RepairCompletionMobileModel.RepaiType = dt.Item("RPR_TYP_CD").ToString()
                    RepairCompletionMobileModel.YardLocation = dt.Item("YRD_LCTN").ToString()
                    RepairCompletionMobileModel.EstimatedManHor = dt.Item("ESTMTD_LBR_HRS").ToString()
                    RepairCompletionMobileModel.ActualManHour = dt.Item("ACTL_MN_HR_NC").ToString()
                    RepairCompletionMobileModel.CompletionDate = dt.Item("RPR_CMPLTN_DT").ToString()
                    RepairCompletionMobileModel.Time = dt.Item("RPR_CMPLTN_TM").ToString()
                    RepairCompletionMobileModel.Remarks = dt.Item("RMRKS_VC").ToString()
                    RepairCompletionMobileModel.Chk_Activity_Date = dt1.Item("CHK_ACTVTY_DT").ToString()


                    Dim dtAttachment As New DataTable
                    Dim objGatein As New GateIns
                    Dim objTrans As New Transactions

                    If Mode = "new" Then
                        dsRepairCompletion.Tables(GateinData._ATTACHMENT).Clear()
                        dtAttachment = dsRepairCompletion.Tables(GateinData._ATTACHMENT).Clone()
                        If dsRepairCompletion.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                            ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                            dtAttachment = objGatein.GetAttachmentByRepairEstimateId(intDepotID, "Repair Approval", dt.Item("RPR_ESTMT_ID"), objTrans).Tables(GateinData._ATTACHMENT)
                            dsRepairCompletion.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                            'End If
                        End If

                    ElseIf Mode = "edit" Then
                        dsRepairCompletion.Tables(GateinData._ATTACHMENT).Clear()
                        dtAttachment = dsRepairCompletion.Tables(GateinData._ATTACHMENT).Clone()
                        If dsRepairCompletion.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                            ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                            dtAttachment = objGatein.GetAttachmentByRepairEstimateId(intDepotID, "Repair Completion", dt.Item("RPR_ESTMT_ID"), objTrans).Tables(GateinData._ATTACHMENT)
                            dsRepairCompletion.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                            'End If
                        End If

                    End If

                    Dim attch1 As New ArrayList
                    Dim attch As New ArrayList
                    'attch1.Add(attcj1)

                    For Each dr As DataRow In dsRepairCompletion.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", dt.Item("RPR_ESTMT_ID"), " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))
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
                        RepairCompletionMobileModel.attchement = attch
                    Else
                        RepairCompletionMobileModel.attchement = attch1
                    End If

                    'Repair Completion


                    RC.Add(RepairCompletionMobileModel)

                Next
            Next


            Dim RCArray As New RC


            RCArray.RC = RC
            RCArray.Status = "Success"

            Return RCArray




        Catch ex As Exception


            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)

            Dim RCArray As New RC
            RCArray.Status = ex.Message

            Return RCArray
        End Try


    End Function



    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function RCUpdate(ByVal UserName As String, ByVal Mode As String, ByVal EquipmentNo As String, ByVal Attchment As String, ByVal hfc As ArrayOfFileParams,
                             ByVal YardLocation As String, ByVal ActualManHours As String, ByVal CompletedDate As String, CompletedTime As String, ByVal Remarks As String) As RC


        Try



            Dim RC As New ArrayList
            gateInMobile.DepotID(UserName)
            Dim objcommon As New CommonData()
            Dim objCommonConfig As New ConfigSetting()
            Dim dtActivityStatus As New DataTable
            Dim objRepairCompletion As New RepairCompletion()
            Dim strMode As String = ""
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            Dim bln_056GwsBit As Boolean
            Dim str_056GWSKey As String
            str_056GWSKey = objCommonConfig.pub_GetConfigSingleValue("056", intDepotID)
            bln_056GwsBit = objCommonConfig.IsKeyExists



            Dim objCommonUI As New CommonUI()
            Dim dsEqpStatus As New DataSet
            Dim blnShowEqStatus As Boolean = False
            dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Repair Completion", True, intDepotID)

            If Mode = "new" Then

                If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                    blnShowEqStatus = True
                End If
                If bln_056GwsBit Then
                    If str_056GWSKey.ToLower = "true" Then
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimatePendingByDepotId(intDepotID)
                    Else
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimatePendingJTSByDepotId(intDepotID)
                    End If
                End If

                For Each drRepairCompletion As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Rows
                    drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                    drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM) = "00:00"
                    If bln_056GwsBit Then
                        If str_056GWSKey.ToLower = "true" Then
                            drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM) = DateTime.Now.ToString("H:mm")
                        End If
                    End If
                Next


            ElseIf Mode = "edit" Then


                If bln_056GwsBit Then
                    If str_056GWSKey.ToLower = "true" Then
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimateMySubmitByDepotId(intDepotID)
                    Else
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimateMySubmitJTSByDepotId(intDepotID)
                    End If
                End If


            End If


            Dim dr1() As DataRow = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(GateinData.EQPMNT_NO, "='", EquipmentNo, "'"))

            'Attachment
            If Attchment = "True" And Mode = "new" Then


                dsRepairCompletion = AttachmentPending(dsRepairCompletion, hfc, dr1(0).Item("RPR_ESTMT_ID"))

                Dim lngGateinId As Long = 0
                Dim intFilesCount As Integer = 0
                'Dim dsGateInData As GateinDataSet = CType(RetrieveData(GATE_IN), GateinDataSet)
                Dim drGateIn1 As DataRow() = Nothing

                lngGateinId = CLng(dr1(0).Item("RPR_ESTMT_ID"))
                drGateIn1 = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", lngGateinId))
                If drGateIn1.Length > 0 Then
                    intFilesCount = CInt(dsRepairCompletion.Tables(GateinData._ATTACHMENT).Compute(String.Concat("COUNT(", RepairCompletionData.RPR_ESTMT_ID, ")"), String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = '", lngGateinId, "'")))
                    drGateIn1(0).Item(GateinData.COUNT_ATTACH) = intFilesCount
                End If

            End If

            If Attchment = "True" And Mode = "edit" Then


                dsRepairCompletion = Attachment(dsRepairCompletion, hfc, dr1(0).Item("RPR_ESTMT_ID"))

                Dim lngGateinId As Long = 0
                Dim intFilesCount As Integer = 0
                'Dim dsGateInData As GateinDataSet = CType(RetrieveData(GATE_IN), GateinDataSet)
                Dim drGateIn1 As DataRow() = Nothing

                lngGateinId = CLng(dr1(0).Item("RPR_ESTMT_ID"))
                drGateIn1 = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", lngGateinId))
                If drGateIn1.Length > 0 Then
                    intFilesCount = CInt(dsRepairCompletion.Tables(GateinData._ATTACHMENT).Compute(String.Concat("COUNT(", RepairCompletionData.RPR_ESTMT_ID, ")"), String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = '", lngGateinId, "'")))
                    drGateIn1(0).Item(GateinData.COUNT_ATTACH) = intFilesCount
                End If

            End If

            dr1(0).Item("YRD_LCTN") = YardLocation
            dr1(0).Item("ACTL_MN_HR_NC") = ActualManHours
            dr1(0).Item("RPR_CMPLTN_DT") = CDate(CompletedDate)
            dr1(0).Item("RPR_CMPLTN_TM") = CompletedTime
            dr1(0).Item("RMRKS_VC") = Remarks
            dr1(0).Item("CHECKED") = "TRUE"



            Dim dtRepairCompltion As DataTable


            Dim strCurrentUserName As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()

            Dim blnEdI As Boolean = CBool(ConfigurationManager.AppSettings("GenerateEDI"))

            Dim objRepairEstimate As New RepairCompletion

            dtRepairCompltion = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Clone()

            Dim bv_strWfData As String = objcommon.GenerateWFData(87)



            Dim str_067InvoiceGenerationGWSBit As String = Nothing


            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", intDepotID)


            Dim bv_strRevisionNo As String = Nothing

            Dim output As Boolean = objRepairEstimate.pub_UpdateRepairCompletion(dsRepairCompletion, _
                                                         bv_strWfData, _
                                                         bv_strRevisionNo, _
                                                         CommonUIs.iDat(datModifiedDate), _
                                                         Mode, _
                                                         intDepotID, _
                                                         strCurrentUserName, _
                                                         str_067InvoiceGenerationGWSBit)


            Dim RCArray As New RC
            If output Then

                RCArray.Status = "Equipment Updated Successfully"


            End If
            Return RCArray

        Catch ex As Exception


            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)

            Dim RCArray As New RC

            RCArray.Status = ex.Message

            Return RCArray

        End Try



    End Function




    Public Function Attachment(ByVal dsRepairCompletion As RepairCompletionDataSet, ByVal hfc As ArrayOfFileParams, ByVal RepairEstimateId As String) As RepairCompletionDataSet




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
        intDepotId = objCommonData.GetDepotID()
        strModifiedBy = objCommonData.GetCurrentUserName()
        'Dim strRepairEstimateId As String = RetrieveData("RepairEstimateId").ToString
        Dim strRepairEstimateId As String = RepairEstimateId
        Dim lngMaxSize As Long = CLng(strSize)
        lngMaxSize = lngMaxSize / 1024000


        For i As Integer = 0 To hfc.ArrayOfFileParams.Count - 1
            Dim hpf As FileParams = hfc.ArrayOfFileParams(i)
            If hpf.ContentLength > 0 Then
                Dim lngFileSize As Long
                Dim sbPath As New StringBuilder
                strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
                ' strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
                strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                lngFileSize = hpf.ContentLength

                drAttachment = dsRepairCompletion.Tables(RepairEstimateData._ATTACHMENT).NewRow()
                drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(dsRepairCompletion.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)
                If Not IsDBNull(strRepairEstimateId) AndAlso strRepairEstimateId <> String.Empty AndAlso strRepairEstimateId <> " " Then
                    drAttachment(RepairEstimateData.RPR_ESTMT_ID) = strRepairEstimateId
                End If

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
                dsRepairCompletion.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(drAttachment)

                'If bv_strPageName = "GateIn" Then
                '    'CacheData(GATE_IN, dsGateInData)
                'End If

            End If
        Next


        Return dsRepairCompletion

    End Function

    Public Function AttachmentPending(ByVal dsRepairCompletion As RepairCompletionDataSet, ByVal hfc As ArrayOfFileParams, ByVal RepairEstimateId As String) As RepairCompletionDataSet




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
            drAttachment = dsRepairCompletion.Tables(RepairEstimateData._ATTACHMENT).NewRow()

            Dim lngFileSize As Long
            Dim sbPath As New StringBuilder
            strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
            'actualfleName="FFFFFFFFFFFF.jpg"


            ' strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
            strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
            lngFileSize = hpf.ContentLength


            drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(dsRepairCompletion.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)
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
            dsRepairCompletion.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(drAttachment)
        Next


        Return dsRepairCompletion

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
        Dim RC As New ArrayList
        gateInMobile.DepotID(UserName)
        Dim objcommon As New CommonData()
        Dim objCommonConfig As New ConfigSetting()
        Dim dtActivityStatus As New DataTable
        Dim objRepairCompletion As New RepairCompletion()
        Dim strMode As String = ""
        Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
        Dim bln_056GwsBit As Boolean
        Dim str_056GWSKey As String
        str_056GWSKey = objCommonConfig.pub_GetConfigSingleValue("056", intDepotID)
        bln_056GwsBit = objCommonConfig.IsKeyExists



        Dim objCommonUI As New CommonUI()
        Dim dsEqpStatus As New DataSet
        Dim blnShowEqStatus As Boolean = False
        dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Repair Completion", True, intDepotID)

        Select Case Mode
            Case strNew

                If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                    blnShowEqStatus = True
                End If
                If bln_056GwsBit Then
                    If str_056GWSKey.ToLower = "true" Then

                        'query = "SELECT " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + "='" + filterValue + "'"
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimatePendingByDepotId(intDepotID)
                    Else

                        'query = "SELECT " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + "='" + filterValue + "'"
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimatePendingJTSByDepotId(intDepotID)
                    End If
                End If

                For Each drRepairCompletion As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Rows
                    drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                    drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM) = "00:00"
                    If bln_056GwsBit Then
                        If str_056GWSKey.ToLower = "true" Then
                            drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM) = DateTime.Now.ToString("H:mm")
                        End If
                    End If
                Next

                If filterCondition = "Equals" Then



                    For Each dt As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(filterType, "='", filterValue, "'"))

                        Dim filteredvalues As New FilteredValues
                        filteredvalues.Values = dt.Item(filterType).ToString()

                        arraylist.Add(filteredvalues)

                    Next

                Else


                    For Each dt As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(filterType, " ", filterNewConditon, " '%", filterValue, "%'"))

                        Dim filteredvalues As New FilteredValues
                        filteredvalues.Values = dt.Item(filterType).ToString()

                        arraylist.Add(filteredvalues)

                    Next


                End If

            Case strEdit

                If bln_056GwsBit Then
                    If str_056GWSKey.ToLower = "true" Then
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimateMySubmitByDepotId(intDepotID)
                    Else
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimateMySubmitJTSByDepotId(intDepotID)
                    End If
                End If

                If filterCondition = "Equals" Then



                    For Each dt As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(filterType, "='", filterValue, "'"))

                        Dim filteredvalues As New FilteredValues
                        filteredvalues.Values = dt.Item(filterType).ToString()

                        arraylist.Add(filteredvalues)

                    Next

                Else


                    For Each dt As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(filterType, " ", filterNewConditon, " '%", filterValue, "%'"))

                        Dim filteredvalues As New FilteredValues
                        filteredvalues.Values = dt.Item(filterType).ToString()

                        arraylist.Add(filteredvalues)
                    Next


                End If

        End Select

        listVlaues.ListGateInss = arraylist
        listVlaues.status = "Success"

        Return listVlaues

    End Function





    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function SearchList(ByVal UserName As String, ByVal SearchValues As SearchValues, ByVal filterType As String, ByVal Mode As String) As RC

        Try

            Dim RC As New ArrayList
            gateInMobile.DepotID(UserName)
            Dim objcommon As New CommonData()
            Dim objCommonConfig As New ConfigSetting()
            Dim dtActivityStatus As New DataTable
            Dim objRepairCompletion As New RepairCompletion()
            Dim strMode As String = ""
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            Dim bln_056GwsBit As Boolean
            Dim str_056GWSKey As String
            str_056GWSKey = objCommonConfig.pub_GetConfigSingleValue("056", intDepotID)
            bln_056GwsBit = objCommonConfig.IsKeyExists



            Dim objCommonUI As New CommonUI()
            Dim dsEqpStatus As New DataSet
            Dim blnShowEqStatus As Boolean = False
            dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Repair Completion", True, intDepotID)

            If Mode = "new" Then

                If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                    blnShowEqStatus = True
                End If
                If bln_056GwsBit Then
                    If str_056GWSKey.ToLower = "true" Then
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimatePendingByDepotId(intDepotID)
                    Else
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimatePendingJTSByDepotId(intDepotID)
                    End If
                End If

                For Each drRepairCompletion As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Rows
                    drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                    drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM) = "00:00"
                    If bln_056GwsBit Then
                        If str_056GWSKey.ToLower = "true" Then
                            drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM) = DateTime.Now.ToString("H:mm")
                        End If
                    End If
                Next


            ElseIf Mode = "edit" Then


                If bln_056GwsBit Then
                    If str_056GWSKey.ToLower = "true" Then
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimateMySubmitByDepotId(intDepotID)
                    Else
                        dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimateMySubmitJTSByDepotId(intDepotID)
                    End If
                End If


            End If

            For Each dd In SearchValues.SearchValues

                'Dim dt() As DataRow = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(filterType, "='", dd.values, "'"))

                For Each dr1 As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(filterType, "='", dd.values, "'"))
                    Dim RepairCompletionMobileModel As New RepairCompletionMobileModel

                    RepairCompletionMobileModel.EquipmentNo = dr1.Item("EQPMNT_NO").ToString()
                    RepairCompletionMobileModel.Type = dr1.Item("EQPMNT_TYP_CD").ToString()
                    RepairCompletionMobileModel.Code = dr1.Item("EQPMNT_CD_CD").ToString()
                    RepairCompletionMobileModel.Status = dr1.Item("EQPMNT_STTS_CD").ToString()
                    RepairCompletionMobileModel.EstimateNo = dr1.Item("RPR_ESTMT_NO").ToString()
                    RepairCompletionMobileModel.Customer = dr1.Item("CSTMR_CD").ToString()
                    RepairCompletionMobileModel.RepaiType = dr1.Item("RPR_TYP_CD").ToString()
                    RepairCompletionMobileModel.YardLocation = dr1.Item("YRD_LCTN").ToString()
                    RepairCompletionMobileModel.EstimatedManHor = dr1.Item("ESTMTD_LBR_HRS").ToString()
                    RepairCompletionMobileModel.ActualManHour = dr1.Item("ACTL_MN_HR_NC").ToString()
                    RepairCompletionMobileModel.CompletionDate = dr1.Item("RPR_CMPLTN_DT").ToString()
                    RepairCompletionMobileModel.Time = dr1.Item("RPR_CMPLTN_TM").ToString()
                    RepairCompletionMobileModel.Remarks = dr1.Item("RMRKS_VC").ToString()


                    Dim dtAttachment As New DataTable
                    Dim objGatein As New GateIns
                    Dim objTrans As New Transactions

                    dsRepairCompletion.Tables(GateinData._ATTACHMENT).Clear()
                    dtAttachment = dsRepairCompletion.Tables(GateinData._ATTACHMENT).Clone()
                    If dsRepairCompletion.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                        ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                        dtAttachment = objGatein.GetAttachmentByRepairEstimateId(intDepotID, "Repair Completion", dr1.Item("RPR_ESTMT_ID"), objTrans).Tables(GateinData._ATTACHMENT)
                        dsRepairCompletion.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                        'End If
                    End If



                    Dim attch1 As New ArrayList
                    Dim attch As New ArrayList
                    'attch1.Add(attcj1)

                    For Each dr As DataRow In dsRepairCompletion.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", dr1.Item("RPR_ESTMT_ID"), " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))
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
                        RepairCompletionMobileModel.attchement = attch
                    Else
                        RepairCompletionMobileModel.attchement = attch1
                    End If

                    'Repair Completion

                    RC.Add(RepairCompletionMobileModel)



                Next

            Next


            Dim RCArray As New RC


            RCArray.RC = RC
            RCArray.Status = "Success"

            Return RCArray




        Catch ex As Exception


            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)

            Dim RCArray As New RC
            RCArray.Status = ex.Message

            Return RCArray
        End Try

    End Function





End Class