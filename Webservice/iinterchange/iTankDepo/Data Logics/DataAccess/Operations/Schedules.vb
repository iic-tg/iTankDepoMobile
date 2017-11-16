Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

#Region "Schedules"
Public Class Schedules

    Dim objData As DataObjects

    'Private Const RepairSelectQuery As String = "SELECT 'False' AS [CHECKED],EQPMNT_NO [Equipment No],CSTMR_CD [Customer],SCHDL_DT [Schedule Date],(SELECT GTN_DT FROM ACTIVITY_STATUS WHERE EQPMNT_NO=VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO) AS [In Date],(SELECT PRDCT_DSCRPTN_VC FROM V_ACTIVITY_STATUS WHERE EQPMNT_NO=VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO) AS [Previous Cargo],RPR_ESTMT_DT [Estimate Date],ACTVTY_DT[Approval Date],OWNR_APPRVL_RF[Approval Ref No],RPR_TYP_CD [Repair Type],(SELECT TOP 1 EQPMNT_INFRMTN_RMRKS_VC FROM TRACKING WHERE EQPMNT_NO = VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO AND CSTMR_ID=VRE.CSTMR_ID AND EQPMNT_STTS_ID = 10 ORDER BY TRCKNG_ID DESC) AS [Remarks],RPR_ESTMT_ID,RPR_ESTMT_ID AS [ID],RPR_ESTMT_NO,RVSN_NO,GI_TRNSCTN_NO,RPR_ESTMT_DT,ACTVTY_DT,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,DPT_ID,DPT_CD,RPR_TYP_ID,RPR_TYP_CD,ORGNL_ESTMN_DT,APPRVL_AMNT_NC,OWNR_APPRVL_RF,PRTY_APPRVL_RF,RMRKS_VC,'Repair Approval' AS PAGENAME,SCHDL_DT FROM V_REPAIR_ESTIMATE VRE WHERE GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE EQPMNT_STTS_ID IN (SELECT DEFAULT_STATUS_ID FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME ='Repair Approval' AND DPT_ID=VRE.DPT_ID)) AND DPT_ID=VRE.DPT_ID AND ACTVTY_NM = 'Repair Approval' AND RPR_ESTMT_NO NOT IN (SELECT RPR_ESTMT_NO FROM REPAIR_ESTIMATE WHERE EQPMNT_STTS_ID IN (SELECT DEFAULT_STATUS_ID FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME ='Repair Completion' AND DPT_ID=VRE.DPT_ID)) @SCHEDULEDATE ORDER BY RPR_ESTMT_ID DESC"
    Private Const RepairSelectQuery As String = "SELECT 'False' AS [CHECKED],EQPMNT_NO [Equipment No],CSTMR_CD [Customer],SCHDL_DT [Schedule Date],(SELECT GTN_DT FROM ACTIVITY_STATUS WHERE EQPMNT_NO=VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO) AS [In Date],(SELECT PRDCT_DSCRPTN_VC FROM V_ACTIVITY_STATUS WHERE EQPMNT_NO=VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO) AS [Previous Cargo],RPR_ESTMT_DT [Estimate Date],ACTVTY_DT[Approval Date],OWNR_APPRVL_RF[Approval Ref No],RPR_TYP_CD [Repair Type],(SELECT TOP 1 EQPMNT_INFRMTN_RMRKS_VC FROM TRACKING WHERE EQPMNT_NO = VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO AND CSTMR_ID=VRE.CSTMR_ID AND EQPMNT_STTS_ID = 10 ORDER BY TRCKNG_ID DESC) AS [Remarks],RPR_ESTMT_ID,RPR_ESTMT_ID AS [ID],RPR_ESTMT_NO,RVSN_NO,GI_TRNSCTN_NO,RPR_ESTMT_DT,ACTVTY_DT,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,DPT_ID,DPT_CD,RPR_TYP_ID,RPR_TYP_CD,ORGNL_ESTMN_DT,APPRVL_AMNT_NC,OWNR_APPRVL_RF,PRTY_APPRVL_RF,RMRKS_VC,'Repair Approval' AS PAGENAME,SCHDL_DT FROM V_REPAIR_ESTIMATE VRE WHERE GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE EQPMNT_STTS_ID IN (SELECT DEFAULT_STATUS_ID FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME in ('Repair Approval','Inspection') AND DPT_ID=VRE.DPT_ID)) AND DPT_ID=VRE.DPT_ID AND ACTVTY_NM = 'Repair Approval' AND RPR_ESTMT_NO NOT IN (SELECT RPR_ESTMT_NO FROM REPAIR_ESTIMATE WHERE EQPMNT_STTS_ID IN (SELECT DEFAULT_STATUS_ID FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME ='Repair Completion' AND DPT_ID=VRE.DPT_ID)) @SCHEDULEDATE ORDER BY RPR_ESTMT_ID DESC"
    'Private Const CleaningSelectQuery As String = "SELECT 'False' AS [CHECKED],EQPMNT_NO[Equipment No],CSTMR_CD[Customer],SCHDL_DT [Schedule Date],GTN_DT[In Date],PRDCT_DSCRPTN_VC[Previous Cargo],ACTVTY_DT[Last Status Date],(CASE WHEN ADDTNL_CLNNG_BT = 1 THEN 'Yes' ELSE 'No' END) AS [Additional Cleaning],(SELECT TOP 1 EQPMNT_INFRMTN_RMRKS_VC FROM TRACKING WHERE EQPMNT_NO = VAS.EQPMNT_NO AND GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO AND CSTMR_ID=VAS.CSTMR_ID AND EQPMNT_STTS_ID = 3 ORDER BY TRCKNG_ID DESC) AS [Remarks],ACTVTY_STTS_ID[ID],EQPMNT_NO,CSTMR_NAM,CSTMR_CD,CSTMR_ID,GTN_DT,PRDCT_CD,PRDCT_ID,PRDCT_DSCRPTN_VC,EQPMNT_STTS_CD,YRD_LCTN,EQPMNT_TYP_CD,EQPMNT_STTS_ID,PRDCT_ID,GI_TRNSCTN_NO,DPT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_CERT_NO,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RMRKS_VC,ADDTNL_CLNNG_BT,CLNNG_ID,LST_CLNNG_DT,LST_INSPCTD_DT,ADDTNL_CLNNG_FLG,BTTM_SL_NO,MN_LID_SL_NO,TP_SL_NO,CLNNG_RFRNC_NO,ACTVTY_STTS_ID,SCHDL_DT FROM V_ACTIVITY_STATUS VAS WHERE EQPMNT_STTS_ID IN (SELECT PENDING_STATUS FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME ='Cleaning')  @SCHEDULEDATE ORDER BY GTN_DT ASC"
    'Private Const CleaningSelectQuery As String = "SELECT 'False' AS [CHECKED],EQPMNT_NO[Equipment No],CSTMR_CD[Customer],SCHDL_DT [Schedule Date],GTN_DT[In Date],PRDCT_DSCRPTN_VC[Previous Cargo],ACTVTY_DT[Last Status Date],(CASE WHEN ADDTNL_CLNNG_BT = 1 THEN 'Yes' ELSE 'No' END) AS [Additional Cleaning],(SELECT TOP 1 EQPMNT_INFRMTN_RMRKS_VC FROM TRACKING WHERE EQPMNT_NO = VAS.EQPMNT_NO AND GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO AND CSTMR_ID=VAS.CSTMR_ID AND EQPMNT_STTS_ID = 3 ORDER BY TRCKNG_ID DESC) AS [Remarks],ACTVTY_STTS_ID[ID],EQPMNT_NO,CSTMR_NAM,CSTMR_CD,CSTMR_ID,GTN_DT,PRDCT_CD,PRDCT_ID,PRDCT_DSCRPTN_VC,EQPMNT_STTS_CD,YRD_LCTN,EQPMNT_TYP_CD,EQPMNT_STTS_ID,PRDCT_ID,GI_TRNSCTN_NO,DPT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_CERT_NO,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RMRKS_VC,ADDTNL_CLNNG_BT,CLNNG_ID,LST_CLNNG_DT,LST_INSPCTD_DT,ADDTNL_CLNNG_FLG,BTTM_SL_NO,MN_LID_SL_NO,TP_SL_NO,CLNNG_RFRNC_NO,ACTVTY_STTS_ID,SCHDL_DT FROM V_ACTIVITY_STATUS VAS WHERE EQPMNT_STTS_ID IN (3)  @SCHEDULEDATE ORDER BY GTN_DT ASC "
    Private Const CleaningSelectQuery As String = "SELECT 'False' AS [CHECKED],EQPMNT_NO[Equipment No],CSTMR_CD[Customer],SCHDL_DT [Schedule Date],GTN_DT[In Date],PRDCT_DSCRPTN_VC[Previous Cargo],ACTVTY_DT[Last Status Date],(CASE WHEN ADDTNL_CLNNG_BT = 1 THEN 'Yes' ELSE 'No' END) AS [Additional Cleaning],RMRKS_VC AS [Remarks],ACTVTY_STTS_ID[ID],EQPMNT_NO,CSTMR_NAM,CSTMR_CD,CSTMR_ID,GTN_DT,PRDCT_CD,PRDCT_ID,PRDCT_DSCRPTN_VC,EQPMNT_STTS_CD,YRD_LCTN,EQPMNT_TYP_CD,EQPMNT_STTS_ID,PRDCT_ID,GI_TRNSCTN_NO,DPT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_CERT_NO,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RMRKS_VC,ADDTNL_CLNNG_BT,CLNNG_ID,LST_CLNNG_DT,LST_INSPCTD_DT,ADDTNL_CLNNG_FLG,BTTM_SL_NO,MN_LID_SL_NO,TP_SL_NO,CLNNG_RFRNC_NO,ACTVTY_STTS_ID,SCHDL_DT FROM V_ACTIVITY_STATUS VAS WHERE EQPMNT_STTS_ID IN (3)  @SCHEDULEDATE ORDER BY GTN_DT ASC "
    Private Const InspectionSelectQuery As String = "SELECT 'False' AS [CHECKED],EQPMNT_NO[Equipment No],CSTMR_CD[Customer],SCHDL_DT [Schedule Date],GTN_DT[In Date],PRDCT_DSCRPTN_VC[Previous Cargo],ACTVTY_DT[Last Status Date],(CASE WHEN ADDTNL_CLNNG_BT = 1 THEN 'Yes' ELSE 'No' END) AS [Additional Cleaning],(SELECT TOP 1 EQPMNT_INFRMTN_RMRKS_VC FROM TRACKING WHERE EQPMNT_NO = VAS.EQPMNT_NO AND GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO AND CSTMR_ID=VAS.CSTMR_ID AND EQPMNT_STTS_ID = 5 ORDER BY TRCKNG_ID DESC) AS [Remarks],ACTVTY_STTS_ID[ID],EQPMNT_NO,CSTMR_NAM,CSTMR_CD,CSTMR_ID,GTN_DT,PRDCT_CD,PRDCT_ID,PRDCT_DSCRPTN_VC,EQPMNT_STTS_CD,YRD_LCTN,EQPMNT_TYP_CD,EQPMNT_STTS_ID,PRDCT_ID,GI_TRNSCTN_NO,DPT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_CERT_NO,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RMRKS_VC,ADDTNL_CLNNG_BT,CLNNG_ID,LST_CLNNG_DT,LST_INSPCTD_DT,ADDTNL_CLNNG_FLG,BTTM_SL_NO,MN_LID_SL_NO,TP_SL_NO,CLNNG_RFRNC_NO,ACTVTY_STTS_ID,SCHDL_DT FROM V_ACTIVITY_STATUS VAS WHERE EQPMNT_STTS_ID IN (5)  @SCHEDULEDATE ORDER BY GTN_DT ASC "
    Private Const SurveySelectQuery As String = "SELECT 'False' AS [CHECKED],EQPMNT_NO [Equipment No],CSTMR_CD [Customer],(SELECT SCHDL_DT FROM ACTIVITY_STATUS WHERE EQPMNT_NO =VRE.EQPMNT_NO AND GI_TRNSCTN_NO =VRE.GI_TRNSCTN_NO AND CSTMR_ID=VRE.CSTMR_ID) [Schedule Date],(SELECT GTN_DT FROM ACTIVITY_STATUS WHERE EQPMNT_NO=VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO) AS [In Date],(SELECT PRDCT_DSCRPTN_VC FROM PRODUCT WHERE PRDCT_ID = (SELECT PRDCT_ID FROM ACTIVITY_STATUS WHERE EQPMNT_NO=VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO AND CSTMR_ID=VRE.CSTMR_ID)) AS [Previous Cargo],(SELECT TOP 1 EQPMNT_INFRMTN_RMRKS_VC FROM TRACKING WHERE EQPMNT_NO = VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO AND CSTMR_ID=VRE.CSTMR_ID AND EQPMNT_STTS_ID = 18 ORDER BY TRCKNG_ID DESC) AS [Remarks],RPR_ESTMT_ID [ID],GI_TRNSCTN_NO,RPR_ESTMT_DT,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,DPT_ID,DPT_CD,'Survey Completion' AS PAGENAME,(SELECT SCHDL_DT FROM ACTIVITY_STATUS WHERE EQPMNT_NO =VRE.EQPMNT_NO AND GI_TRNSCTN_NO =VRE.GI_TRNSCTN_NO AND CSTMR_ID=VRE.CSTMR_ID) AS SCHDL_DT,(SELECT ACTVTY_STTS_ID FROM ACTIVITY_STATUS WHERE EQPMNT_NO =VRE.EQPMNT_NO AND GI_TRNSCTN_NO =VRE.GI_TRNSCTN_NO AND CSTMR_ID=VRE.CSTMR_ID) AS ACTVTY_STTS_ID FROM V_REPAIR_ESTIMATE VRE WHERE GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE EQPMNT_STTS_ID IN (SELECT PENDING_STATUS FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME ='Survey Completion' AND DPT_ID=VRE.DPT_ID)) AND DPT_ID=VRE.DPT_ID AND ACTVTY_NM='Repair Approval' AND RPR_ESTMT_NO NOT IN (SELECT RPR_ESTMT_NO FROM REPAIR_ESTIMATE WHERE EQPMNT_STTS_ID IN (SELECT DEFAULT_STATUS_ID FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME ='Survey Completion' AND DPT_ID=VRE.DPT_ID)) AND EQPMNT_NO IN (SELECT EQPMNT_NO FROM ACTIVITY_STATUS WHERE GI_TRNSCTN_NO =VRE.GI_TRNSCTN_NO @SCHEDULEDATE)  ORDER BY RPR_ESTMT_ID DESC"
    Private Const RepairUpdateQuery As String = "UPDATE REPAIR_ESTIMATE SET SCHDL_DT=@SCHDL_DT WHERE RPR_ESTMT_ID=@RPR_ESTMT_ID"
    Private Const ActivityStatusUpdateQuery As String = "UPDATE ACTIVITY_STATUS SET SCHDL_DT=@SCHDL_DT WHERE ACTVTY_STTS_ID=@ACTVTY_STTS_ID"
    'Private Const RepairScheduleReportQuery As String = "SELECT RPR_ESTMT_ID,RPR_ESTMT_NO,GI_TRNSCTN_NO,RVSN_NO,RPR_ESTMT_DT,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,DPT_ID,DPT_CD,DPT_NAM,DPT_CRRNCY_ID,SRVYR_NM,LBR_RT_NC,RPR_TYP_ID,RPR_TYP_CD,CRT_OF_CLNLNSS_BT,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,INVCNG_PRTY_CRRNCY_ID,PRTY_CRRNCY_CD,PRTY_EXCHANGE_RATE_NC,CSTMR_EXCHANGE_RATE_NC,ORGNL_ESTMN_DT,ESTMTN_TTL_NC,OWNR_APPRVL_RF,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTVTY_DT,ACTVTY_NM,APPRVL_AMNT_NC,ORGNL_ESTMTN_AMNT_NC,CSTMR_ESTMTN_TTL_NC,CSTMR_APPRVL_AMNT_NC,ACTL_MN_HR_NC,MAIL_SEND,PRTY_APPRVL_RF,COUNT_ATTACH,SCHDL_DT,(SELECT PRDCT_DSCRPTN_VC FROM PRODUCT WHERE PRDCT_ID = (SELECT PRDCT_ID FROM ACTIVITY_STATUS WHERE EQPMNT_NO=VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO AND CSTMR_ID=VRE.CSTMR_ID)) AS PRDCT_DSCRPTN_VC FROM V_REPAIR_ESTIMATE VRE WHERE SCHDL_DT<=@SCHDL_DT ORDER BY SCHDL_DT DESC"
    Private Const RepairScheduleReportQuery As String = "SELECT RPR_ESTMT_ID,RPR_ESTMT_NO,GI_TRNSCTN_NO,RVSN_NO,RPR_ESTMT_DT,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,DPT_ID,DPT_CD,DPT_NAM,DPT_CRRNCY_ID,SRVYR_NM,LBR_RT_NC,RPR_TYP_ID,RPR_TYP_CD,CRT_OF_CLNLNSS_BT,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,INVCNG_PRTY_CRRNCY_ID,PRTY_CRRNCY_CD,PRTY_EXCHANGE_RATE_NC,CSTMR_EXCHANGE_RATE_NC,ORGNL_ESTMN_DT,ESTMTN_TTL_NC,OWNR_APPRVL_RF,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTVTY_DT,ACTVTY_NM,APPRVL_AMNT_NC,ORGNL_ESTMTN_AMNT_NC,CSTMR_ESTMTN_TTL_NC,CSTMR_APPRVL_AMNT_NC,ACTL_MN_HR_NC,MAIL_SEND,PRTY_APPRVL_RF,COUNT_ATTACH,SCHDL_DT,(SELECT PRDCT_DSCRPTN_VC FROM PRODUCT WHERE PRDCT_ID = (SELECT PRDCT_ID FROM ACTIVITY_STATUS WHERE EQPMNT_NO=VRE.EQPMNT_NO AND GI_TRNSCTN_NO=VRE.GI_TRNSCTN_NO AND CSTMR_ID=VRE.CSTMR_ID)) AS PRDCT_DSCRPTN_VC,DPT_CD FROM V_REPAIR_ESTIMATE VRE WHERE GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE EQPMNT_STTS_ID IN (SELECT DEFAULT_STATUS_ID FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME in ('Repair Approval','Inspection') AND DPT_ID=VRE.DPT_ID)) AND DPT_ID=VRE.DPT_ID AND ACTVTY_NM = 'Repair Approval' AND RPR_ESTMT_NO NOT IN (SELECT RPR_ESTMT_NO FROM REPAIR_ESTIMATE WHERE EQPMNT_STTS_ID IN (SELECT DEFAULT_STATUS_ID FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME ='Repair Completion' AND DPT_ID=VRE.DPT_ID)) and SCHDL_DT <=@SCHDL_DT AND DPT_ID=@DPT_ID ORDER BY VRE.SCHDL_DT DESC"
    Private Const ActivityStatusReportQuery As String = "SELECT EQPMNT_NO,CSTMR_CD,PRDCT_CD,PRDCT_DSCRPTN_VC,SCHDL_DT,ADDTNL_CLNNG_BT,CLNNG_DT,(SELECT TOP 1 RMRKS_VC FROM CLEANING WHERE EQPMNT_NO =VAS.EQPMNT_NO AND GI_TRNSCTN_NO = VAS.GI_TRNSCTN_NO AND  CSTMR_ID = VAS.CSTMR_ID ORDER BY CLNNG_ID DESC) AS CLNNG_RMRKS_VC,DPT_CD FROM V_ACTIVITY_STATUS VAS WHERE SCHDL_DT<=@SCHDL_DT AND EQPMNT_STTS_ID=@EQPMNT_STTS_ID AND DPT_ID=@DPT_ID ORDER BY VAS.SCHDL_DT DESC"
    Private ds As ScheduleDataSet

    'Schedule Role Rights
    Private Const GetScheduleRights_SelectQuery As String = "SELECT RL_RGHT_ID,RL_ID,ACTVTY_ID,ACTVTY_NAM,USR_ID,USR_NAM,CRT_BT,EDT_BT,VW_BT FROM V_SCHEDULE_USER_RIGHTS WHERE USR_NAM=@USR_NAM"
    Private Const GetScheduleActivity_SelectQuery As String = "SELECT ENM_ID,ENM_CD,ENM_DSCRPTN_VC,ENM_TYP_ID,ENM_TYP_CD FROM ENUM WHERE ENM_TYP_ID=39"

#Region "Constructor.."

    Sub New()
        ds = New ScheduleDataSet
    End Sub

#End Region

#Region "GetActivityByActivityId()"
    ''' <summary>
    ''' Get Activity Details based on selected by page (Repair,Cleaning,Survey)
    ''' </summary>
    ''' <param name="bv_intActivityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetActivityByActivityId(ByVal bv_intActivityId As Integer, _
                                            ByVal bv_strTableName As String, _
                                            ByVal bv_intScheduleId As Integer, _
                                            ByVal bv_intDepotID As Int32) As ScheduleDataSet
        Try
            Dim strQuery As String = String.Empty
            Dim strScheduleDateNotNUllFilter As String = String.Concat(" AND ", ScheduleData.SCHDL_DT, " IS NOT NULL AND ", ScheduleData.DPT_ID, "=", bv_intDepotID)
            Dim strScheduleDateNUllFilter As String = String.Concat(" AND ", ScheduleData.SCHDL_DT, " IS NULL AND ", ScheduleData.DPT_ID, "=", bv_intDepotID)
            Dim strScheduleDateAllFilter As String = String.Concat(" AND ", ScheduleData.DPT_ID, "=", bv_intDepotID)
            If bv_intActivityId = 123 Then ' Cleaning
                strQuery = CleaningSelectQuery
            ElseIf bv_intActivityId = 124 Then ' Repair Estimate
                strQuery = RepairSelectQuery
            ElseIf bv_intActivityId = 125 Then ' Survey Completion
                strQuery = SurveySelectQuery
            ElseIf bv_intActivityId = 137 Then ' Inspection
                strQuery = InspectionSelectQuery
            End If
            If strQuery.Length > 0 Then
                If bv_intScheduleId = 126 Then 'Yes
                    strQuery = String.Concat(strQuery.Replace("@SCHEDULEDATE", strScheduleDateNotNUllFilter))
                ElseIf bv_intScheduleId = 127 Then 'No
                    strQuery = String.Concat(strQuery.Replace("@SCHEDULEDATE", strScheduleDateNUllFilter))
                Else 'All
                    strQuery = String.Concat(strQuery.Replace("@SCHEDULEDATE", strScheduleDateAllFilter))
                End If
            End If
            objData = New DataObjects(strQuery)
            objData.Fill(CType(ds, DataSet), bv_strTableName)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateActivityById()"
    ''' <summary>
    ''' Update activity based on selected by user (Repair, Cleaning, Survey)
    ''' </summary>
    ''' <param name="bv_lngId"></param>
    ''' <param name="bv_datScheduleDate"></param>
    ''' <param name="br_objTransaction"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateActivityById(ByVal bv_lngId As Int64, _
                                       ByVal bv_intActivityId As Integer, _
                                       ByVal bv_datScheduleDate As DateTime, _
                                       ByVal bv_strUpdateTableName As String, _
                                       ByRef br_objTransaction As Transactions)
        Try            Dim dr As DataRow
            Dim lngId As Int64 = 0
            Dim strQuery As String = String.Empty
            objData = New DataObjects()
            dr = ds.Tables(bv_strUpdateTableName).NewRow()
            With dr
                If bv_intActivityId = 123 OrElse bv_intActivityId = 125 Then ' Cleaning
                    dr.Item(ScheduleData.ACTVTY_STTS_ID) = bv_lngId
                    strQuery = ActivityStatusUpdateQuery
                ElseIf bv_intActivityId = 137 Then ' Inspection
                    dr.Item(ScheduleData.ACTVTY_STTS_ID) = bv_lngId
                    strQuery = ActivityStatusUpdateQuery
                Else 'Repair , Survey
                    dr.Item(ScheduleData.RPR_ESTMT_ID) = bv_lngId
                    strQuery = RepairUpdateQuery
                End If
                dr.Item(ScheduleData.SCHDL_DT) = bv_datScheduleDate
            End With
            UpdateActivityById = objData.UpdateRow(dr, strQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRepairScheduleReport() TABLE NAME: REPAIR_ESTIMATE"
    ''' <summary>
    ''' This function is used to fetch the details for report
    ''' </summary>
    ''' <param name="bv_datScheduleDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRepairScheduleReport(ByVal bv_datScheduleDate As DateTime, ByVal bv_intDepot As Int32) As ScheduleDataSet
        Try
            ' objData = New DataObjects(RepairScheduleReportQuery, ScheduleData.SCHDL_DT, bv_datScheduleDate)
            Dim hshparameters As New Hashtable
            hshparameters.Add(ScheduleData.SCHDL_DT, bv_datScheduleDate)
            hshparameters.Add(ScheduleData.DPT_ID, bv_intDepot)
            objData = New DataObjects(RepairScheduleReportQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), ScheduleData._V_REPAIR_ESTIMATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetSurveyScheduleReport() TABLE NAME: ACTIVITY_STATUS"
    ''' <summary>
    ''' This function is used to fetch the details for report
    ''' </summary>
    ''' <param name="bv_datScheduleDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSurveyScheduleReport(ByVal bv_datScheduleDate As DateTime, ByVal bv_intDepot As Int32) As ScheduleDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(ScheduleData.SCHDL_DT, bv_datScheduleDate)
            hshparameters.Add(ScheduleData.EQPMNT_STTS_ID, "18")
            hshparameters.Add(ScheduleData.DPT_ID, bv_intDepot)
            objData = New DataObjects(ActivityStatusReportQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), ScheduleData._V_ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningScheduleReport() TABLE NAME: ACTIVITY_STATUS"
    ''' <summary>
    ''' This function is used to fetch the details for report
    ''' </summary>
    ''' <param name="bv_datScheduleDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCleaningScheduleReport(ByVal bv_datScheduleDate As DateTime, ByVal bv_intDepotId As Int32) As ScheduleDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(ScheduleData.SCHDL_DT, bv_datScheduleDate)
            hshparameters.Add(ScheduleData.EQPMNT_STTS_ID, "3")
            hshparameters.Add(ScheduleData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(ActivityStatusReportQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), ScheduleData._V_ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Schedule Role Rights"

    Public Function GetScheduleRights(ByVal bv_strUserName As String) As ScheduleDataSet
        Try
            objData = New DataObjects(GetScheduleRights_SelectQuery, CommonUIData.USR_NAM, bv_strUserName)
            objData.Fill(CType(ds, DataSet), ScheduleData._V_SCHEDULE_USER_RIGHTS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetScheduleActivity() As ScheduleDataSet
        Try
            objData = New DataObjects(GetScheduleActivity_SelectQuery)
            objData.Fill(CType(ds, DataSet), ScheduleData._ENUM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Print
    Public Function GetInspectionScheduleReport(ByVal bv_datScheduleDate As DateTime, ByVal bv_intDepot As Int32) As ScheduleDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(ScheduleData.SCHDL_DT, bv_datScheduleDate)
            hshparameters.Add(ScheduleData.EQPMNT_STTS_ID, "5")
            hshparameters.Add(ScheduleData.DPT_ID, bv_intDepot)
            objData = New DataObjects(ActivityStatusReportQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), ScheduleData._V_ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
#End Region




