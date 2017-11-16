Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class BulkEmail
#Region "GetTrackingDetails() TABLE NAME:TRACKING"
    <OperationContract()> _
    Public Function GetTrackingDetails(ByVal bv_intCstmr_ID As String, _
                                       ByVal bv_strEquipmentNo As String, _
                                       ByVal bv_strInDateFrom As String, _
                                       ByVal bv_strInDateTo As String, _
                                       ByVal bv_strActivityId As String, _
                                       ByVal bv_strEventDateFrom As String, _
                                       ByVal bv_strEventDateTo As String, _
                                       ByVal bv_strEmail As String, _
                                       ByVal intDPT_ID As Integer) As BulkEmailDataSet
        Try
            Dim objBulkEmails As New BulkEmails
            Dim dsBulkEmail As New BulkEmailDataSet
            dsBulkEmail = objBulkEmails.GetTrackingDetails(bv_intCstmr_ID, _
                                                          bv_strEquipmentNo, _
                                                          bv_strInDateFrom, _
                                                          bv_strInDateTo, _
                                                          bv_strActivityId, _
                                                          bv_strEventDateFrom, _
                                                          bv_strEventDateTo, _
                                                          bv_strEmail, _
                                                          CStr(intDPT_ID))
            Return dsBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "getBulkEmailDetail()"
    <OperationContract()> _
    Public Function getBulkEmailDetail(ByVal EquipmentNo As String, ByVal GITransNo As String, ByVal ActivityName As String, _
                                       ByVal bv_strActivityNo As String) As BulkEmailDataSet
        Try
            Dim dsBulkEamilDataset As BulkEmailDataSet
            Dim objBulkEmails As New BulkEmails
            dsBulkEamilDataset = objBulkEmails.pub_GetBulkEmailDetail(EquipmentNo, GITransNo, ActivityName, bv_strActivityNo)
            Return dsBulkEamilDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerDetail()"
    <OperationContract()> _
    Public Function pub_GetCustomerDetail(ByVal intDepot As Integer, ByVal intCustomerID As Integer) As BulkEmailDataSet
        Try
            Dim dsBulkEamilDataset As BulkEmailDataSet
            Dim objBulkEmails As New BulkEmails
            dsBulkEamilDataset = objBulkEmails.pub_GetCustomerDetail(intDepot, intCustomerID)
            Return dsBulkEamilDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_BulkEmailCreateBulkEmail() TABLE NAME:Bulk_Email"

    <OperationContract()> _
    Public Function pub_BulkEmailCreateBulkEmail(ByVal bv_i64CSTMR_ID As Int64, _
                                                ByVal bv_strFRM_EML As String, _
                                                ByVal bv_strTO_EML As String, _
                                                ByVal bv_strCC_EML As String, _
                                                ByVal bv_strBCC_EML As String, _
                                                ByVal bv_strSBJCT_VC As String, _
                                                ByVal bv_strBDY_VC As String, _
                                                ByVal bv_i32DPT_ID As Int32, _
                                                ByVal bv_strCRTD_BY As String, _
                                                ByVal bv_datCRTD_DT As DateTime, _
                                                ByVal bv_strMailMode As String, _
                                                ByVal bv_blnResendBit As Boolean, _
                                                ByVal bv_strActivityFlow As String, _
                                                ByVal bv_strGWSSettings As String, _
                                                ByVal bv_dsBUlkEMailDetail As BulkEmailDataSet) As Long
        Dim objTrans As New Transactions
        Try
            Dim objBulk_Email As New BulkEmails
            Dim lngEquipmentStatusId As Long
            Dim strEquipmentStatusCode As String = "AAR"
            Dim strCurrentStatus As String = String.Empty
            Dim dtActivityStatus As New DataTable
            Dim drBulkEmailDetail As DataRow() = Nothing
            Dim strAttachmentPath As String = String.Empty
            Dim datActivityDate As DateTime
            Dim drActivityDate As DataRow() = Nothing
            Dim lngCreated As Long
            lngCreated = objBulk_Email.CreateBulkEmail(bv_i64CSTMR_ID, _
                                                       bv_strFRM_EML, _
                                                       bv_strTO_EML, _
                                                       bv_strCC_EML, _
                                                       bv_strBCC_EML, _
                                                       bv_strSBJCT_VC, _
                                                       bv_strBDY_VC,
                                                       False, _
                                                       bv_i32DPT_ID, _
                                                       bv_datCRTD_DT, _
                                                       bv_strCRTD_BY, _
                                                       False, _
                                                       " ", _
                                                       objTrans)
            If bv_blnResendBit Then
                drBulkEmailDetail = bv_dsBUlkEMailDetail.Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND).Select(String.Concat(BulkEmailData.BLK_EML_ID, " IS NOT NULL"))
                With bv_dsBUlkEMailDetail.Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND).Rows(0)
                    Dim dt As New DataTable
                    dt = objBulk_Email.GetAttachmentFileNameByResenBit(CStr(.Item(BulkEmailData.GI_TRNSCTN_NO)), _
                                                                       CStr(.Item(BulkEmailData.EQPMNT_NO)), _
                                                                       CStr(.Item(Bulk_EmailData.ACTVTY_NAM)), _
                                                                       objTrans)
                    If dt.Rows.Count > 0 Then
                        strAttachmentPath = dt.Rows(0).Item(BulkEmailData.ATTCHMNT_PTH).ToString
                    End If
                End With
                'End If
            Else
                drBulkEmailDetail = bv_dsBUlkEMailDetail.Tables(BulkEmailData._BULK_EMAIL_DETAIL).Select(BulkEmailData.CHECKED & "='True'")
            End If

            For Each dr As DataRow In drBulkEmailDetail
                Dim dblAmount As Double = 0
                Dim intCurrencyID As Integer = 0
                Dim strServicePartnerCode As String = String.Empty

                If Not IsDBNull(dr.Item(BulkEmailData.AMNT_NC)) Then
                    dblAmount = CDbl(dr.Item(BulkEmailData.AMNT_NC))
                End If

                If Not IsDBNull(dr.Item(BulkEmailData.CRRNCY_ID)) Then
                    intCurrencyID = CInt(dr.Item(BulkEmailData.CRRNCY_ID))
                End If

                If Not CStr(dr.Item(BulkEmailData.ACTVTY_NAM)) = "Cleaning" Then
                    Dim dtRepairEstimateDetail As New DataTable
                    Dim Distinctdata As New DatasetHelpers(CType(bv_dsBUlkEMailDetail, DataSet))
                    Dim dtSummary As New DataTable
                    Dim strField As String = String.Concat("SUM(", BulkEmailData.TTL_CST_NC, "),", BulkEmailData.RSPNSBLTY_ID, ",", BulkEmailData.CSTMR_ID, ",", BulkEmailData.INVCNG_PRTY_ID, ",", BulkEmailData.SRVC_PRTNR_TYP_CD)
                    dtRepairEstimateDetail = bv_dsBUlkEMailDetail.Tables(BulkEmailData._V_REPAIR_ESTIMATE_DETAIL).Clone()
                    dtRepairEstimateDetail = objBulk_Email.GetRepairEstimateDetailByRepairEstimationId(CInt(dr.Item(BulkEmailData.ACTVTY_NO)))
                    dtSummary = Distinctdata.SelectGroupByInto("CUSTOMER_PARTY_SUMMARY", dtRepairEstimateDetail, strField, "", BulkEmailData.RSPNSBLTY_ID)

                    For Each drSummary As DataRow In dtSummary.Rows
                        Dim decTotalCost As Decimal = 0
                        Dim lngServicePartnerId As Long = 0
                        If Not IsDBNull(drSummary.Item(String.Concat("SUMof", BulkEmailData.TTL_CST_NC))) Then
                            decTotalCost = CDec(drSummary.Item(String.Concat("SUMof", BulkEmailData.TTL_CST_NC)))
                        End If

                        If CLng(drSummary.Item(BulkEmailData.RSPNSBLTY_ID)) = 66 OrElse CLng(drSummary.Item(BulkEmailData.RSPNSBLTY_ID)) = 67 Then
                            If Not IsDBNull(drSummary.Item(BulkEmailData.INVCNG_PRTY_ID)) Then
                                lngServicePartnerId = CLng(drSummary.Item(BulkEmailData.INVCNG_PRTY_ID))
                                strServicePartnerCode = "PARTY"
                            Else
                                lngServicePartnerId = CLng(drSummary.Item(BulkEmailData.CSTMR_ID))
                                strServicePartnerCode = "CUSTOMER"
                            End If
                        Else
                            lngServicePartnerId = CLng(drSummary.Item(BulkEmailData.RSPNSBLTY_ID))
                            ' strServicePartnerCode = Convert.ToString(drSummary.Item(BulkEmailData.SRVC_PRTNR_TYP_CD))
                            strServicePartnerCode = Convert.ToString(drSummary.Item(BulkEmailData.SRVC_PRTNR_TYP_CD))

                        End If
                        If bv_strGWSSettings.ToUpper = "TRUE" Then
                            If strServicePartnerCode = "PARTY" Then
                                strServicePartnerCode = "AGENT"
                            End If

                        End If


                        objBulk_Email.CreateBulkEmailDetail(lngCreated, _
                                                            dr.Item(BulkEmailData.EQPMNT_NO).ToString(), _
                                                            CInt(dr.Item(BulkEmailData.EQPMNT_STTS_ID)), _
                                                            dr.Item(BulkEmailData.ACTVTY_NAM).ToString, _
                                                            CInt(dr.Item(Bulk_EmailData.ACTVTY_NO)), _
                                                            decTotalCost, _
                                                            intCurrencyID, _
                                                            dr.Item(BulkEmailData.GI_TRNSCTN_NO).ToString, _
                                                            strAttachmentPath, _
                                                            bv_blnResendBit, _
                                                            lngServicePartnerId, _
                                                            strServicePartnerCode, _
                                                            objTrans)
                    Next
                Else
                    Dim dsClean As New BulkEmailDataSet
                    Dim lngCustomerId As Long = 0
                    dsClean = objBulk_Email.GetCleaningDetails(bv_i32DPT_ID, CInt(dr.Item(Bulk_EmailData.ACTVTY_NO)))
                    If dsClean.Tables(BulkEmailData._CLEANING).Rows.Count > 0 Then
                        lngCustomerId = CLng(dsClean.Tables(BulkEmailData._CLEANING).Rows(0).Item(BulkEmailData.CSTMR_ID))
                    End If
                    objBulk_Email.CreateBulkEmailDetail(lngCreated, _
                                                           dr.Item(BulkEmailData.EQPMNT_NO).ToString(), _
                                                           CInt(dr.Item(BulkEmailData.EQPMNT_STTS_ID)), _
                                                           dr.Item(BulkEmailData.ACTVTY_NAM).ToString, _
                                                           CInt(dr.Item(Bulk_EmailData.ACTVTY_NO)), _
                                                           0, _
                                                           intCurrencyID, _
                                                           dr.Item(BulkEmailData.GI_TRNSCTN_NO).ToString, _
                                                           strAttachmentPath, _
                                                           bv_blnResendBit, _
                                                           lngCustomerId, _
                                                           "CUSTOMER", _
                                                           objTrans)

                End If

                If bv_strActivityFlow <> "" AndAlso bv_blnResendBit = False Then
                    Dim strActivityStatus As String = String.Empty
                    If Not IsDBNull(dr.Item(BulkEmailData.ACTVTY_NAM)) Then
                        strActivityStatus = CStr(dr.Item(BulkEmailData.ACTVTY_NAM))
                    End If
                    lngEquipmentStatusId = CLng(objBulk_Email.GetEquipmentStatusDetail(strEquipmentStatusCode, bv_i32DPT_ID, objTrans))
                    dtActivityStatus = objBulk_Email.GetActivityStatusDetail(dr.Item(BulkEmailData.EQPMNT_NO).ToString(), _
                                                                              dr.Item(BulkEmailData.GI_TRNSCTN_NO).ToString, _
                                                                              bv_i32DPT_ID, _
                                                                              objTrans)
                    If dtActivityStatus.Rows.Count > 0 Then
                        strCurrentStatus = dtActivityStatus.Rows(0).Item(BulkEmailData.EQPMNT_STTS_CD).ToString()
                    End If
                    Dim strTrackingId As String = Nothing
                    drActivityDate = bv_dsBUlkEMailDetail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Select(String.Concat(BulkEmailData.CHECKED, " ='True' AND ", BulkEmailData.EQPMNT_NO, " ='", dr.Item(BulkEmailData.EQPMNT_NO).ToString(), "' AND ", BulkEmailData.GI_TRNSCTN_NO, " = '", dr.Item(BulkEmailData.GI_TRNSCTN_NO).ToString(), "' AND ", BulkEmailData.ACTVTY_NO, " = ", dr.Item(BulkEmailData.ACTVTY_NO).ToString(), " AND ", BulkEmailData.ACTVTY_NAM, " = 'Repair Estimate'"))


                    If bv_dsBUlkEMailDetail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Select(String.Concat(BulkEmailData.CHECKED, " ='True' AND ", BulkEmailData.EQPMNT_NO, " ='", dr.Item(BulkEmailData.EQPMNT_NO).ToString(), "' AND ", BulkEmailData.GI_TRNSCTN_NO, " = '", dr.Item(BulkEmailData.GI_TRNSCTN_NO).ToString(), "' AND ", BulkEmailData.ACTVTY_NO, " = ", dr.Item(BulkEmailData.ACTVTY_NO).ToString(), " AND ", BulkEmailData.ACTVTY_NAM, " = 'Repair Estimate'")).Length > 0 Then
                        Dim drTrack() As DataRow = bv_dsBUlkEMailDetail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Select(String.Concat(BulkEmailData.CHECKED, " ='True' AND ", BulkEmailData.EQPMNT_NO, " ='", dr.Item(BulkEmailData.EQPMNT_NO).ToString(), "' AND ", BulkEmailData.GI_TRNSCTN_NO, " = '", dr.Item(BulkEmailData.GI_TRNSCTN_NO).ToString(), "' AND ", BulkEmailData.ACTVTY_NO, " = ", dr.Item(BulkEmailData.ACTVTY_NO).ToString(), " AND ", BulkEmailData.ACTVTY_NAM, " = 'Repair Estimate'"))
                        strTrackingId = drTrack(0).Item(BulkEmailData.TRCKNG_ID).ToString()
                    End If
                    If drActivityDate.Length > 0 Then
                        datActivityDate = CDate(drActivityDate(0).Item(BulkEmailData.ACTVTY_DT))
                    End If
                    'REPAIR FLOW SETTING: 037
                    'CR-001 (AWE_TO_AWP)
                    If strActivityStatus = "Repair Estimate" AndAlso strCurrentStatus = bv_strActivityFlow Then
                        objBulk_Email.UpdateActivityStatus(lngEquipmentStatusId, _
                                                           dr.Item(BulkEmailData.EQPMNT_NO).ToString(), _
                                                           dr.Item(BulkEmailData.GI_TRNSCTN_NO).ToString, _
                                                           datActivityDate, _
                                                           dr.Item(BulkEmailData.ACTVTY_NAM).ToString, _
                                                           objTrans)





                    End If

                    'Update Tracking Equipment Status
                    If strActivityStatus = "Repair Estimate" Then
                        Dim objCommonUIs As New CommonUIs
                        If objCommonUIs.GetMultiLocationSupportConfig.ToLower = "true" Then
                            lngEquipmentStatusId = CLng(objBulk_Email.GetEquipmentStatusDetail("AAR", CInt(objCommonUIs.GetHeadQuarterID()), objTrans))
                        Else
                            lngEquipmentStatusId = CLng(objBulk_Email.GetEquipmentStatusDetail("AAR", bv_i32DPT_ID, objTrans))
                        End If


                        objBulk_Email.UpdateTrackingFromBulkEmail(strTrackingId, _
                                                                  dr.Item(BulkEmailData.EQPMNT_NO).ToString(), _
                                                                  lngEquipmentStatusId, _
                                                                  dr.Item(BulkEmailData.GI_TRNSCTN_NO).ToString(), _
                                                                  bv_i32DPT_ID, _
                                                                  objTrans)
                    End If
                End If
            Next
            objTrans.commit()

            Return lngCreated
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function

#End Region

#Region "pub_GetRepairEstimate()"
    <OperationContract()> _
    Public Function pub_GetRepairEstimate(ByVal intDepot As Integer, _
                                          ByVal intActivityNo As Integer, _
                                          ByVal giTransactionNo As String) As BulkEmailDataSet
        Try
            Dim dsBulkEamilDataset As BulkEmailDataSet
            Dim objBulkEmails As New BulkEmails
            dsBulkEamilDataset = objBulkEmails.pub_GetRepairEstimate(intDepot, intActivityNo, giTransactionNo)
            Return dsBulkEamilDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetCleaningDetails()"
    <OperationContract()> _
    Public Function GetCleaningDetails(ByVal intDepot As Integer, ByVal intActivityNo As Integer) As BulkEmailDataSet
        Try
            Dim dsBulkEamilDataset As BulkEmailDataSet
            Dim objBulkEmails As New BulkEmails
            dsBulkEamilDataset = objBulkEmails.GetCleaningDetails(intDepot, intActivityNo)
            Return dsBulkEamilDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_BulkEmailDetailCreateBulkEmailDetail() TABLE NAME:Bulk_Email_Detail"

    '<OperationContract()> _
    'Public Function pub_BulkEmailDetailCreateBulkEmailDetail(ByVal dsBulkEmailDetail As DataSet) As Long
    '    Dim objTrans As New Transactions
    '    Try
    '        Dim objBulkEmails As New BulkEmails
    '        Dim strEquipmentNo As String = String.Empty
    '        Dim intBulkEmailId As Integer
    '        Dim intEqpmntSttsID As Integer
    '        Dim strActivityName As String = String.Empty
    '        Dim dblAmount As Double
    '        Dim intCurrencyID As Integer
    '        Dim strGITransNo As String = String.Empty
    '        For Each dr As DataRow In dsBulkEmailDetail.Tables(BulkEmailData._BULK_EMAIL_DETAIL).Select(BulkEmailData.CHECKED & "='True'")
    '            intBulkEmailId = CInt(dr.Item(BulkEmailData.BLK_EML_ID))
    '            strEquipmentNo = dr.Item(BulkEmailData.EQPMNT_NO).ToString()
    '            intEqpmntSttsID = CInt(dr.Item(BulkEmailData.EQPMNT_STTS_ID))
    '            strActivityName = dr.Item(BulkEmailData.ACTVTY_NAM).ToString
    '            If dr.Item(BulkEmailData.AMNT_NC) Is DBNull.Value Then
    '                dblAmount = vbNull
    '            Else
    '                dblAmount = CDbl(dr.Item(BulkEmailData.AMNT_NC))
    '            End If
    '            If dr.Item(BulkEmailData.CRRNCY_ID) Is DBNull.Value Then
    '                intCurrencyID = vbNull
    '            Else
    '                intCurrencyID = CInt(dr.Item(BulkEmailData.CRRNCY_ID))
    '            End If
    '            'dblAmount = CDbl(dr.Item(BulkEmailData.AMNT_NC))
    '            'intCurrencyID = CInt(dr.Item(BulkEmailData.CRRNCY_ID))
    '            strGITransNo = dr.Item(BulkEmailData.GI_TRNSCTN_NO).ToString
    '            objBulkEmails.CreateBulkEmailDetail(intBulkEmailId, _
    '                                                  strEquipmentNo, _
    '                                                  intEqpmntSttsID, _
    '                                                  strActivityName, _
    '                                                  dblAmount, _
    '                                                  intCurrencyID, _
    '                                                  strGITransNo, _
    '                                                  objTrans)
    '        Next
    '        objTrans.commit()
    '    Catch ex As Exception
    '        objTrans.RollBack()
    '        Throw New FaultException(New FaultReason(ex.Message))
    '    Finally
    '        objTrans = Nothing
    '    End Try
    'End Function

#End Region

#Region "pub_GetDepot()"
    <OperationContract()> _
    Public Function pub_GetDepot(ByVal intDepot As Integer) As BulkEmailDataSet
        Try
            Dim dsBulkEamilDataset As BulkEmailDataSet
            Dim objBulkEmails As New BulkEmails
            dsBulkEamilDataset = objBulkEmails.pub_GetDepot(intDepot)
            Return dsBulkEamilDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBulkEmailDetailReSendbyTransactionNo() TABLE NAME:BULK_EMAIL_DETAIL"
    <OperationContract()> _
    Public Function pub_GetBulkEmailDetailReSendbyTransactionNo(ByVal bv_strGateinTransactionNo As String, _
                                                                ByVal bv_strEquipmentNo As String, _
                                                                ByVal bv_strActivityName As String) As BulkEmailDataSet

        Try
            Dim objBulkEmail As BulkEmailDataSet
            Dim objBulkEmails As New BulkEmails
            objBulkEmail = objBulkEmails.GetBulkEmailDetailReSendbyTransactionNo(bv_strGateinTransactionNo, bv_strEquipmentNo, bv_strActivityName)
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


#Region "pub_GetBulkEmailDetailView()"
    <OperationContract()> _
    Public Function pub_GetBulkEmailDetailView(ByVal bv_strEquipmentNo As String, _
                                               ByVal bv_strGITransNo As String, _
                                               ByVal bv_strActivityName As String, _
                                               ByVal bv_strActivityNo As String, _
                                               ByVal bv_strBulkEmailId As String) As BulkEmailDataSet
        Try
            Dim dsBulkEamilDataset As BulkEmailDataSet
            Dim objBulkEmails As New BulkEmails
            dsBulkEamilDataset = objBulkEmails.GetBulkEmailDetailView(bv_strEquipmentNo, bv_strGITransNo, bv_strActivityName, bv_strActivityNo, bv_strBulkEmailId)
            Return dsBulkEamilDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region



End Class
