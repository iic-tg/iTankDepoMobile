Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class BulkEmails

#Region "Declarations"
    Dim objData As DataObjects
    Private Const TRACKINGSelectQueryByDPT_ID As String = "SELECT TRCKNG_ID,DPT_ID,EQPMNT_NO,CSTMR_ID,CSTMR_CD,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_DT,ACTVTY_NAM,ACTVTY_NO,GI_TRNSCTN_NO,GTN_DT,LST_SNT_BY,LST_SNT_DT,TMS_SNT,ACTVTY_ID,ERR_RMRKS,ERR_FLG FROM V_BULKEMAIL_TRACKING WHERE CNCLD_DT IS NULL AND "
    Private Const getBulkEmailDetail As String = "SELECT BLK_EML_ID,TO_EML,BCC_EML,SBJCT_VC,SNT_DT FROM BULK_EMAIL WHERE BLK_EML_ID IN(SELECT BLK_EML_ID FROM BULK_EMAIL_DETAIL WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND EQPMNT_NO=@EQPMNT_NO AND ACTVTY_NAM=@ACTVTY_NAM AND ACTVTY_NO =@ACTVTY_NO) AND SNT_BT = 1  ORDER BY SNT_DT DESC"
    Private Const GetCustomerDetailQurey As String = "SELECT CSTMR_ID,CSTMR_CRRNCY_ID,RPRTNG_EML_ID,INVCNG_EML_ID,RPR_TCH_EML_ID,BLK_EML_FRMT_ID FROM CUSTOMER WHERE DPT_ID=@DPT_ID AND CSTMR_ID=@CSTMR_ID"
    Private Const Bulk_EmailInsertQuery As String = "INSERT INTO BULK_EMAIL(BLK_EML_ID,CSTMR_ID,FRM_EML,TO_EML,BCC_EML,SBJCT_VC,BDY_VC,SNT_BT,DPT_ID,CRTD_DT,CRTD_BY,ERR_RMRKS,ERR_FLG,CC_EML)VALUES(@BLK_EML_ID,@CSTMR_ID,@FRM_EML,@TO_EML,@BCC_EML,@SBJCT_VC,@BDY_VC,@SNT_BT,@DPT_ID,@CRTD_DT,@CRTD_BY,@ERR_RMRKS,@ERR_FLG,@CC_EML)"
    Private Const GetRepairEstimateQurey As String = "SELECT RPR_ESTMT_ID,EQPMNT_NO,GI_TRNSCTN_NO,DPT_ID,ACTVTY_NM,CASE WHEN ACTVTY_NM IN('Repair Approval','Repair Completion') THEN APPRVL_AMNT_NC ELSE ESTMTN_TTL_NC END ESTMTN_TTL_NC FROM REPAIR_ESTIMATE WHERE DPT_ID=@DPT_ID AND ACTVTY_NM IN('Repair Estimate','Repair Approval','Repair Completion') AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND RPR_ESTMT_ID=@RPR_ESTMT_ID"
    Private Const Bulk_Email_DetailInsertQuery As String = "INSERT INTO BULK_EMAIL_DETAIL(BLK_EML_DTL_ID,BLK_EML_ID,EQPMNT_NO,EQPMNT_STTS_ID,ACTVTY_NAM,ACTVTY_NO,AMNT_NC,CRRNCY_ID,GI_TRNSCTN_NO,ATTCHMNT_PTH,RSND_BT,SRVC_PRTNR_ID,SRVC_PRTNR_TYP_CD)VALUES(@BLK_EML_DTL_ID,@BLK_EML_ID,@EQPMNT_NO,@EQPMNT_STTS_ID,@ACTVTY_NAM,@ACTVTY_NO,@AMNT_NC,@CRRNCY_ID,@GI_TRNSCTN_NO,@ATTCHMNT_PTH,@RSND_BT,@SRVC_PRTNR_ID,@SRVC_PRTNR_TYP_CD)"
    Private Const CleaningDetailsQuery As String = "SELECT CLNNG_ID,CSTMR_ID,CLNNG_RT,GI_TRNSCTN_NO FROM V_CLEANING WHERE DPT_ID=@DPT_ID AND CLNNG_ID=@CLNNG_ID "
    Private Const DepotDetailsDetailsQuery As String = "SELECT BNK_TYP_ID,DPT_ID,CRRNCY_ID,CRRNCY_CD FROM V_BANK_DETAIL WHERE BNK_TYP_ID=44 AND DPT_ID=@DPT_ID"
    Private Const V_BULK_EMAIL_DETAILSelectQuerybyTransactionNo As String = "SELECT TOP 1 BLK_EML_ID,CSTMR_ID,CSTMR_CD,FRM_EML,TO_EML,BCC_EML,SBJCT_VC,BDY_VC,DPT_ID,DPT_CD,SNT_BT,SNT_DT,CRTD_DT,CRTD_BY,BLK_EML_DTL_ID,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NO,ACTVTY_NAM,AMNT_NC,CRRNCY_ID,CRRNCY_CD,GI_TRNSCTN_NO,GI_RF_NO,ATTCHMNT_PTH,RSND_BT FROM V_BULK_EMAIL_DETAIL_RESEND WHERE GI_TRNSCTN_NO =@GI_TRNSCTN_NO AND ACTVTY_NAM= @ACTVTY_NAM AND EQPMNT_NO= @EQPMNT_NO ORDER BY BLK_EML_DTL_ID DESC "
    Private Const Bulk_Email_DetailAttachmentPathSelectQuery As String = "SELECT BLK_EML_DTL_ID,BLK_EML_ID,EQPMNT_NO,EQPMNT_STTS_ID,ACTVTY_NO,ACTVTY_NAM,AMNT_NC,CRRNCY_ID,GI_TRNSCTN_NO,ATTCHMNT_PTH,RSND_BT FROM BULK_EMAIL_DETAIL WHERE  GI_TRNSCTN_NO =@GI_TRNSCTN_NO AND ACTVTY_NAM= @ACTVTY_NAM AND EQPMNT_NO= @EQPMNT_NO AND RSND_BT= 0 ORDER BY BLK_EML_DTL_ID DESC"
    Private Const Bulk_Email_DetailViewSelectQuery As String = "SELECT BLK_EML_DTL_ID,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NO,ACTVTY_NAM,AMNT_NC,CRRNCY_ID,ATTCHMNT_PTH,RSND_BT,CRRNCY_CD,GI_TRNSCTN_NO,BLK_EML_ID,CSTMR_ID,CSTMR_CD,FRM_EML,TO_EML,BCC_EML,SBJCT_VC,BDY_VC,DPT_ID,DPT_CD,SNT_BT,SNT_DT,CRTD_DT,CRTD_BY,GI_RF_NO,SRVC_PRTNR_ID,SRVC_PRTNR_CD,SRVC_PRTNR_TYP_CD FROM V_BULK_EMAIL_DETAIL_RESEND WHERE BLK_EML_ID= @BLK_EML_ID AND EQPMNT_NO =@EQPMNT_NO AND ACTVTY_NAM =@ACTVTY_NAM AND GI_TRNSCTN_NO= @GI_TRNSCTN_NO AND ACTVTY_NO =@ACTVTY_NO"
    Private Const EquipmentStatusSelectQuery As String = "SELECT EQPMNT_STTS_ID FROM EQUIPMENT_STATUS WHERE EQPMNT_STTS_CD=@EQPMNT_STTS_CD AND DPT_ID = @DPT_ID"
    'REPAIR FLOW SETTING: 037
    'CR-001 (AWE_TO_AWP)
    Private Const Activity_StatusSelectQuery As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,GTN_DT,GTOT_DT,CLNNG_DT,INSPCTN_DT,RPR_CMPLTN_DT,PRDCT_ID,EQPMNT_STTS_ID,(SELECT EQPMNT_STTS_CD FROM EQUIPMENT_STATUS WHERE EQPMNT_STTS_ID=A.EQPMNT_STTS_ID) AS EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSTRCTNS_VC,YRD_LCTN,ACTV_BT,DPT_ID FROM ACTIVITY_STATUS A WHERE EQPMNT_NO =@EQPMNT_NO AND GI_TRNSCTN_NO= @GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const Activity_StatusUpdateQuery As String = "UPDATE ACTIVITY_STATUS SET EQPMNT_STTS_ID=@EQPMNT_STTS_ID,  ACTVTY_DT=@ACTVTY_DT WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND EQPMNT_NO=@EQPMNT_NO AND ACTVTY_NAM=@ACTVTY_NAM"
    Private Const Repair_Estimate_DetailSelectQueryByRepairEstimationId As String = "SELECT RPR_ESTMT_DTL_ID,RPR_ESTMT_ID,RPR_ID,RPR_CD,DMG_ID,DMG_CD,LBR_RT,LBR_HRS,LBR_HR_CST_NC,MTRL_CST_NC,RSPNSBLTY_ID,RSPNSBLTY_CD,DMG_RPR_DSCRPTN,TTL_CST_NC,EQPMNT_NO,ITM_ID,ITM_CD,SB_ITM_ID,SB_ITM_CD,CHK_BT,CSTMR_ID,INVCNG_PRTY_ID,SRVC_PRTNR_TYP_CD FROM V_REPAIR_ESTIMATE_DETAIL WHERE RPR_ESTMT_ID=@RPR_ESTMT_ID"
    Dim sqlDtnull = "19000101"

    'For GWS
    Private Const UpdateTrackingFromBulkEmail_Qry As String = "UPDATE TRACKING SET EQPMNT_STTS_ID=@EQPMNT_STTS_ID WHERE TRCKNG_ID=@TRCKNG_ID AND EQPMNT_NO=@EQPMNT_NO  AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"

    Private ds As BulkEmailDataSet

#End Region

#Region "Constructor.."
    Sub New()
        ds = New BulkEmailDataSet
    End Sub
#End Region

#Region "GetTrackingDetails() TABLE NAME:V_TRACKING"

    Public Function GetTrackingDetails(ByVal bv_strCstmr_ID As String, _
                                       ByVal bv_strEquipmentNo As String, _
                                       ByVal bv_strInDateFrom As String, _
                                       ByVal bv_strInDateTo As String, _
                                       ByVal bv_strActivityId As String, _
                                       ByVal bv_strEventDateFrom As String, _
                                       ByVal bv_strEventDateTo As String, _
                                       ByVal bv_strEmail As String, _
                                       ByVal bv_strDepotId As String) As BulkEmailDataSet
        Try
            Dim strWhere As String = String.Empty

            strWhere = String.Concat(strWhere, BulkEmailData.DPT_ID, " IN (", bv_strDepotId, ") AND ", BulkEmailData.CSTMR_ID, " IN (", bv_strCstmr_ID, ") ")

            If bv_strEquipmentNo <> "" Then
                If strWhere <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", BulkEmailData.EQPMNT_NO, " IN ('", bv_strEquipmentNo, "')")
                Else
                    strWhere = String.Concat(BulkEmailData.EQPMNT_NO, " IN ('", bv_strEquipmentNo, "')")
                End If
            End If

            If bv_strInDateFrom <> Nothing And bv_strInDateTo <> Nothing Then
                Dim InDateFrom As Date = CDate(bv_strInDateFrom)
                Dim InDateTo As Date = CDate(bv_strInDateTo)
                If strWhere <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", BulkEmailData.GTN_DT, " BETWEEN '", InDateFrom.ToString("dd-MMM-yyyy"), "' AND '", _
                                             InDateTo.ToString("dd-MMM-yyyy"), "'")
                End If
            End If

            If bv_strEventDateFrom <> Nothing And bv_strEventDateTo <> Nothing Then
                Dim EventDateFrom As Date = CDate(bv_strEventDateFrom)
                Dim EventDateTo As Date = CDate(bv_strEventDateTo)
                If strWhere <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", BulkEmailData.ACTVTY_DT, " BETWEEN '", EventDateFrom.ToString("dd-MMM-yyyy"), "' AND '", _
                                             EventDateTo.ToString("dd-MMM-yyyy"), "'")
                End If
            End If

            If bv_strActivityId <> String.Empty Then
                strWhere = String.Concat(strWhere, " AND ", BulkEmailData.ACTVTY_ID, " IN (", bv_strActivityId, ")")
            End If

            If bv_strEmail <> "" And bv_strEmail <> "75" Then
                If bv_strEmail = "77" Then
                    strWhere = String.Concat(strWhere, " AND ", BulkEmailData.TMS_SNT, " =0 ")
                ElseIf bv_strEmail = "76" Then
                    strWhere = String.Concat(strWhere, " AND ", BulkEmailData.TMS_SNT, " >0 ")
                End If
            End If

            If strWhere <> "" Then
                strWhere = String.Concat(strWhere, " ORDER BY ACTVTY_DT DESC ")
            End If

            objData = New DataObjects(String.Concat(TRACKINGSelectQueryByDPT_ID, strWhere))
            objData.Fill(CType(ds, DataSet), BulkEmailData._V_BULKEMAIL_TRACKING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetBulkEmailDetail() "

    Public Function pub_GetBulkEmailDetail(ByVal bv_strEquipmentNo As String, ByVal bv_GITransNo As String, ByVal bv_ActivityName As String, _
                                           ByVal bv_strActivityNo As String) As BulkEmailDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(BulkEmailData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(BulkEmailData.GI_TRNSCTN_NO, bv_GITransNo)
            hshParameters.Add(BulkEmailData.ACTVTY_NAM, bv_ActivityName)
            hshParameters.Add(BulkEmailData.ACTVTY_NO, bv_strActivityNo)
            objData = New DataObjects(getBulkEmailDetail, hshParameters)
            objData.Fill(CType(ds, DataSet), BulkEmailData._BULK_EMAILDETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetCustomerDetail"
    Public Function pub_GetCustomerDetail(ByVal intDepot As Integer, ByVal intCustomerId As Integer) As BulkEmailDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(BulkEmailData.DPT_ID, intDepot)
            hshParameters.Add(BulkEmailData.CSTMR_ID, intCustomerId)
            objData = New DataObjects(GetCustomerDetailQurey, hshParameters)
            objData.Fill(CType(ds, DataSet), BulkEmailData._CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetRepairEstimate"
    Public Function pub_GetRepairEstimate(ByVal intDepot As Integer, _
                                          ByVal intActivityNo As Integer, _
                                          ByVal giTransactionNo As String) As BulkEmailDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(BulkEmailData.DPT_ID, intDepot)
            hshParameters.Add(BulkEmailData.RPR_ESTMT_ID, intActivityNo)
            hshParameters.Add(BulkEmailData.GI_TRNSCTN_NO, giTransactionNo)

            objData = New DataObjects(GetRepairEstimateQurey, hshParameters)
            objData.Fill(CType(ds, DataSet), BulkEmailData._REPAIR_ESTIMATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningDetails"
    Public Function GetCleaningDetails(ByVal intDepot As Integer, ByVal intActivityNo As Integer) As BulkEmailDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(BulkEmailData.DPT_ID, intDepot)
            hshParameters.Add(BulkEmailData.CLNNG_ID, intActivityNo)
            objData = New DataObjects(CleaningDetailsQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), BulkEmailData._CLEANING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetDepot"
    Public Function pub_GetDepot(ByVal intDepot As Integer) As BulkEmailDataSet
        Try
            objData = New DataObjects(DepotDetailsDetailsQuery, BulkEmailData.DPT_ID, intDepot)
            objData.Fill(CType(ds, DataSet), BulkEmailData._DEPOT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateBulkEmail() TABLE NAME:Bulk_Email"

    Public Function CreateBulkEmail(ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_strFRM_EML As String, _
        ByVal bv_strTO_EML As String, _
        ByVal bv_strCC_EML As String, _
        ByVal bv_strBCC_EML As String, _
        ByVal bv_strSBJCT_VC As String, _
        ByVal bv_strBDY_VC As String, _
        ByVal bv_blnSntBt As Boolean, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_datCRTD_DT As DateTime, _
        ByVal bv_strCRTD_BY As String, _
        ByVal bv_blnErrorFlag As Boolean, _
        ByVal bv_strErrorRemarks As String, _
        ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(BulkEmailData._BULK_EMAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(BulkEmailData._BULK_EMAIL, br_ObjTransactions)
                .Item(BulkEmailData.BLK_EML_ID) = intMax
                .Item(BulkEmailData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(BulkEmailData.FRM_EML) = bv_strFRM_EML
                .Item(BulkEmailData.TO_EML) = bv_strTO_EML
                .Item(BulkEmailData.CC_EML) = bv_strCC_EML
                If bv_strBCC_EML <> Nothing Then
                    .Item(BulkEmailData.BCC_EML) = bv_strBCC_EML
                Else
                    .Item(BulkEmailData.BCC_EML) = DBNull.Value
                End If
                If bv_strCC_EML <> Nothing Then
                    .Item(BulkEmailData.CC_EML) = bv_strCC_EML
                Else
                    .Item(BulkEmailData.CC_EML) = DBNull.Value
                End If
                If bv_strSBJCT_VC <> Nothing Then
                    .Item(BulkEmailData.SBJCT_VC) = bv_strSBJCT_VC
                Else
                    .Item(BulkEmailData.SBJCT_VC) = DBNull.Value
                End If
                If bv_strBDY_VC <> Nothing Then
                    .Item(BulkEmailData.BDY_VC) = bv_strBDY_VC
                Else
                    .Item(BulkEmailData.BDY_VC) = DBNull.Value
                End If
                .Item(BulkEmailData.SNT_BT) = bv_blnSntBt
                .Item(BulkEmailData.DPT_ID) = bv_i32DPT_ID
                .Item(BulkEmailData.CRTD_DT) = bv_datCRTD_DT
                .Item(BulkEmailData.CRTD_BY) = bv_strCRTD_BY
                .Item(BulkEmailData.ERR_RMRKS) = bv_strErrorRemarks
                .Item(BulkEmailData.ERR_FLG) = bv_blnErrorFlag
            End With
            objData.InsertRow(dr, Bulk_EmailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateBulkEmail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateBulkEmailDetail() TABLE NAME:Bulk_Email_Detail"

    Public Function CreateBulkEmailDetail(ByVal bv_i64BLK_EML_ID As Int64, _
                                          ByVal bv_strEQPMNT_NO As String, _
                                          ByVal bv_i64EQPMNT_STTS_ID As Int64, _
                                          ByVal bv_strACTVTY_NAM As String, _
                                          ByVal bv_intACTIVITYNO As Integer, _
                                          ByVal bv_dblAMNT_NC As Double, _
                                          ByVal bv_i64CRRNCY_ID As Int64, _
                                          ByVal bv_strGI_TRNSCTN_NO As String, _
                                          ByVal bv_strAttachmentPath As String, _
                                          ByVal bv_blnResendBit As Boolean, _
                                          ByVal bv_i64ServicePartnerId As Long, _
                                          ByVal bv_strServicePartnerCode As String, _
                                          ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(BulkEmailData._BULK_EMAIL_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(BulkEmailData._BULK_EMAIL_DETAIL, br_ObjTransactions)
                .Item(BulkEmailData.BLK_EML_DTL_ID) = intMax
                .Item(BulkEmailData.BLK_EML_ID) = bv_i64BLK_EML_ID
                .Item(BulkEmailData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(BulkEmailData.EQPMNT_STTS_ID) = bv_i64EQPMNT_STTS_ID
                .Item(BulkEmailData.ACTVTY_NAM) = bv_strACTVTY_NAM
                .Item(BulkEmailData.ACTVTY_NO) = bv_intACTIVITYNO
                If Not bv_strACTVTY_NAM = "Cleaning" Then
                    .Item(BulkEmailData.AMNT_NC) = bv_dblAMNT_NC
                Else
                    .Item(BulkEmailData.AMNT_NC) = DBNull.Value
                End If
                If bv_i64CRRNCY_ID <> 0 Then
                    .Item(BulkEmailData.CRRNCY_ID) = bv_i64CRRNCY_ID
                Else
                    .Item(BulkEmailData.CRRNCY_ID) = DBNull.Value
                End If
                If bv_strAttachmentPath <> Nothing Then
                    .Item(BulkEmailData.ATTCHMNT_PTH) = bv_strAttachmentPath
                Else
                    .Item(BulkEmailData.ATTCHMNT_PTH) = DBNull.Value
                End If
                .Item(BulkEmailData.GI_TRNSCTN_NO) = bv_strGI_TRNSCTN_NO
                .Item(BulkEmailData.RSND_BT) = bv_blnResendBit

                If bv_i64ServicePartnerId <> Nothing AndAlso bv_i64ServicePartnerId <> 0 Then
                    .Item(BulkEmailData.SRVC_PRTNR_ID) = bv_i64ServicePartnerId
                Else
                    .Item(BulkEmailData.SRVC_PRTNR_ID) = DBNull.Value
                End If
                If bv_strServicePartnerCode <> Nothing Then
                    .Item(BulkEmailData.SRVC_PRTNR_TYP_CD) = bv_strServicePartnerCode
                Else
                    .Item(BulkEmailData.SRVC_PRTNR_TYP_CD) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Bulk_Email_DetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateBulkEmailDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetBulkEmailDetailReSendbyTransactionNo() TABLE NAME:Bulk_Email_Detail"

    Public Function GetBulkEmailDetailReSendbyTransactionNo(ByVal bv_strGateinTransactionNo As String, _
                                                            ByVal bv_strEquipmentNo As String, _
                                                            ByVal bv_strActivityName As String) As BulkEmailDataSet
        Try
            Dim hshparameters As New Hashtable
            Dim dsTemp As New BulkEmailDataSet
            hshparameters.Add(BulkEmailData.GI_TRNSCTN_NO, bv_strGateinTransactionNo)
            hshparameters.Add(BulkEmailData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(BulkEmailData.ACTVTY_NAM, bv_strActivityName)
            objData = New DataObjects(V_BULK_EMAIL_DETAILSelectQuerybyTransactionNo, hshparameters)
            objData.Fill(CType(dsTemp, DataSet), BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND)
            Return dsTemp
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetAttachmentFileNameByResenBit()  TABLE NAME:Bulk_Email_Detail"
    Public Function GetAttachmentFileNameByResenBit(ByVal bv_strGateinTransactionNo As String, _
                                                    ByVal bv_strEquipmentNo As String, _
                                                    ByVal bv_strActivityName As String, _
                                                    ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshparameters As New Hashtable
            Dim dt As New DataTable
            Dim dsTemp As New BulkEmailDataSet
            hshparameters.Add(BulkEmailData.GI_TRNSCTN_NO, bv_strGateinTransactionNo)
            hshparameters.Add(BulkEmailData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(BulkEmailData.ACTVTY_NAM, bv_strActivityName)
            objData = New DataObjects(Bulk_Email_DetailAttachmentPathSelectQuery, hshparameters)
            objData.Fill(dt, br_ObjTransactions)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetBulkEmailDetailView()   TABLE NAME:Bulk_Email_Detail"

    Public Function GetBulkEmailDetailView(ByVal bv_strEquipmentNo As String, _
                                           ByVal bv_GITransNo As String, _
                                           ByVal bv_ActivityName As String, _
                                           ByVal bv_strActivityNo As String, _
                                           ByVal bv_strBulkEmailId As String) As BulkEmailDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(BulkEmailData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(BulkEmailData.GI_TRNSCTN_NO, bv_GITransNo)
            hshParameters.Add(BulkEmailData.ACTVTY_NAM, bv_ActivityName)
            hshParameters.Add(BulkEmailData.ACTVTY_NO, bv_strActivityNo)
            hshParameters.Add(BulkEmailData.BLK_EML_ID, bv_strBulkEmailId)
            objData = New DataObjects(Bulk_Email_DetailViewSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), BulkEmailData._BULK_EMAIL_DETAIL_VIEW)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateActivityStatus() TABLE NAME:Activity_Status"
    'REPAIR FLOW SETTING: 037
    'CR-001 (AWE_TO_AWP)
    Public Function UpdateActivityStatus(ByVal bv_i64ActivityStatusId As Int64, _
                                         ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_strGateinTransaction As String, _
                                         ByVal bv_datActivityDate As Date, _
                                         ByVal bv_strActivityName As String, _
                                         ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(BulkEmailData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(BulkEmailData.EQPMNT_STTS_ID) = bv_i64ActivityStatusId
                .Item(BulkEmailData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(BulkEmailData.GI_TRNSCTN_NO) = bv_strGateinTransaction
                .Item(BulkEmailData.ACTVTY_DT) = bv_datActivityDate
                .Item(BulkEmailData.ACTVTY_NAM) = bv_strActivityName
            End With
            UpdateActivityStatus = objData.UpdateRow(dr, Activity_StatusUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentStatusDetail() TABLE NAME:EQUIPMENT_STATUS"

    Public Function GetEquipmentStatusDetail(ByVal bv_strEquipmentStatusCode As String, _
                                             ByVal bv_i32DepotId As Int32, _
                                             ByRef objTrans As Transactions) As String
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(Bulk_EmailData.EQPMNT_STTS_CD, bv_strEquipmentStatusCode)
            hshparameters.Add(BulkEmailData.DPT_ID, bv_i32DepotId)
            objData = New DataObjects(EquipmentStatusSelectQuery, hshparameters)
            Return objData.ExecuteScalar(objTrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetActivityStatusDetail() TABLE NAME:ACTIVITY_STATUS"

    Public Function GetActivityStatusDetail(ByVal bv_strEquipmentNo As String, _
                                            ByVal bv_strGateinTransaction As String, _
                                            ByVal bv_i32DepotId As Int32, _
                                            ByRef objTrans As Transactions) As DataTable
        Try
            Dim hshparameters As New Hashtable
            Dim dt As New DataTable
            hshparameters.Add(BulkEmailData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(BulkEmailData.GI_TRNSCTN_NO, bv_strGateinTransaction)
            hshparameters.Add(BulkEmailData.DPT_ID, bv_i32DepotId)
            objData = New DataObjects(Activity_StatusSelectQuery, hshparameters)
            objData.Fill(dt, objTrans)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRepairEstimateDetailByRepairEstimationId() TABLE NAME:REPAIR_ESTIMATE_DETAIL"

    Public Function GetRepairEstimateDetailByRepairEstimationId(ByVal bv_i64EstimateId As Int64) As DataTable
        Try
            Dim dtRepairEstimateDetail As New DataTable
            objData = New DataObjects(Repair_Estimate_DetailSelectQueryByRepairEstimationId, BulkEmailData.RPR_ESTMT_ID, CStr(bv_i64EstimateId))
            objData.Fill(dtRepairEstimateDetail)
            Return dtRepairEstimateDetail
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


    'Update tracking

    Public Function UpdateTrackingFromBulkEmail(ByVal bv_i64TrackingId As String, _
                                         ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_i64EquipmentStatusId As Int64, _
                                         ByVal bv_strGateinTransaction As String, _
                                         ByVal bv_DepoId As Int32, _
                                         ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(BulkEmailData._TRACKING).NewRow()
            With dr
                .Item(BulkEmailData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                .Item(BulkEmailData.TRCKNG_ID) = bv_i64TrackingId
                .Item(BulkEmailData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(BulkEmailData.GI_TRNSCTN_NO) = bv_strGateinTransaction
                .Item(BulkEmailData.DPT_ID) = bv_DepoId
            End With
            UpdateTrackingFromBulkEmail = objData.UpdateRow(dr, UpdateTrackingFromBulkEmail_Qry, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
