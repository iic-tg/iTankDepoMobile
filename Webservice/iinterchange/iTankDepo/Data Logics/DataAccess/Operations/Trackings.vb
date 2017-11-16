#Region " Trackings.vb"
'*********************************************************************************************************************
'Name :
'      Trackings.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Trackings.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      6/13/2013 5:51:34 PM
'*********************************************************************************************************************
#End Region
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

#Region "Trackings"

Public Class Trackings

#Region "Declaration Part.. "

	Dim objData As DataObjects
    Private Const TRACKINGSelectQueryByDPT_ID As String = "SELECT TRCKNG_ID,RFRNC_NO,TRNSMSSN_NO,ACTVTY_NAM,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EIR_DT,PCKP_STTS_VC,PCKP_DT,CRTD_BY,RSND_BT,DPT_ID,DPT_CD,LSS_ID,LSS_CD,CSTMR_ID,CSTMR_CD FROM V_TRACKING"
    Private Const Bulk_EmailInsertQuery As String = "INSERT INTO BULK_EMAIL(BLK_EML_BIN,FRM_EML,TO_EML,SBJCT_VC,BDY_VC,SNT_DT,CRTD_DT,CRTD_BY,ACTV_BT,DPT_ID)VALUES(@BLK_EML_BIN,@FRM_EML,@TO_EML,@SBJCT_VC,@BDY_VC,@SNT_DT,@CRTD_DT,@CRTD_BY,@ACTV_BT,@DPT_ID)"
    Private Const Bulk_Email_DetailInsertQuery As String = "INSERT INTO BULK_EMAIL_DETAIL(BLK_EML_DTL_BIN,BLK_EML_BIN,ACTVTY_NAM,TRNSMSSN_NO,EQPMNT_NO,STTS_VC)VALUES(@BLK_EML_DTL_BIN,@BLK_EML_BIN,@ACTVTY_NAM,@TRNSMSSN_NO,@EQPMNT_NO,@STTS_VC)"
    Dim sqlDtnull = "19000101"
    Private ds As TrackingDataSet

# End Region

#Region "Constructor.."

	Sub New()
        ds = New TrackingDataSet
	End Sub

#End Region

#Region "GetTracking() TABLE NAME:V_TRACKING"

    Public Function GetTracking(ByVal bv_intDpt_ID As Int32) As TrackingDataSet
        Try
            Dim strWhere As String = " WHERE "
            strWhere = String.Concat(strWhere, TrackingData.DPT_ID, " = ", bv_intDpt_ID, " ")
            objData = New DataObjects(String.Concat(TRACKINGSelectQueryByDPT_ID, strWhere))
            objData.Fill(CType(ds, DataSet), TrackingData._V_TRACKING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetTrackingDetails() TABLE NAME:V_TRACKING"

    Public Function GetTrackingDetails(ByVal bv_strCstmr_ID As String, ByVal bv_strContainerNo As String, _
                                ByVal bv_strPickUpDate As String, ByVal bv_strReceivedDate As String, _
                                ByVal bv_strTransmission As String, ByVal bv_strLss_ID As String, ByVal bv_strActvty_Nam As String, ByVal bv_strDpt_ID As String) As TrackingDataSet
        Try
            Dim strWhere As String = " WHERE "
            If bv_strDpt_ID <> "" Then
                strWhere = String.Concat(strWhere, TrackingData.DPT_ID, " IN (", bv_strDpt_ID, ")")
            End If
            If bv_strCstmr_ID <> "" Then
                If strWhere <> "" Then

                    strWhere = String.Concat(strWhere, " AND ", TrackingData.CSTMR_ID, " IN (", bv_strCstmr_ID, ") ")
                Else
                    strWhere = String.Concat(TrackingData.CSTMR_ID, " IN ( ", bv_strCstmr_ID, ")")
                End If
            End If
            If bv_strContainerNo <> "" Then
                If strWhere <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", TrackingData.EQPMNT_NO, " IN ('", bv_strContainerNo, "')")
                Else
                    strWhere = String.Concat(TrackingData.EQPMNT_NO, " IN ('", bv_strContainerNo, "')")
                End If
            End If
            If bv_strPickUpDate <> "" Then
                Dim bv_datPickUpDate As Date = CDate(bv_strPickUpDate)
                If strWhere <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", "(Convert(varchar,", TrackingData.PCKP_DT, ",103) = '", bv_datPickUpDate.ToString("dd/MM/yyyy"), "')")
                Else
                    strWhere = String.Concat("(Convert(varchar,", TrackingData.PCKP_DT, ",103) = '", bv_datPickUpDate.ToString("dd/MM/yyyy"), "') ")
                End If
            End If

            If bv_strReceivedDate <> "" Then
                Dim bv_strReceived As String = CDate(bv_strReceivedDate).ToString("dd/MM/yyyy")
                bv_strReceivedDate = "W" + bv_strReceived.Substring(6, 4) + bv_strReceived.Substring(3, 2) + bv_strReceived.Substring(0, 2)
                If strWhere <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", TrackingData.TRNSMSSN_NO, " LIKE ('", bv_strReceivedDate, "%')")
                Else
                    strWhere = String.Concat(TrackingData.TRNSMSSN_NO, " LIKE ('", bv_strReceivedDate, "%') ")
                End If
            End If
            If bv_strTransmission <> "" Then
                If strWhere <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", TrackingData.TRNSMSSN_NO, " IN ('", bv_strTransmission, "')")
                Else
                    strWhere = String.Concat(TrackingData.TRNSMSSN_NO, " IN ('", bv_strTransmission, "')")
                End If
            End If
            If bv_strLss_ID <> "" Then
                If strWhere <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", TrackingData.LSS_ID, " IN (", bv_strLss_ID, ") ")
                Else
                    strWhere = String.Concat(TrackingData.LSS_ID, " IN (", bv_strLss_ID, ")")
                End If
            End If
            If bv_strActvty_Nam <> "" And bv_strActvty_Nam <> "ALL" Then
                If strWhere <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", TrackingData.ACTVTY_NAM, " IN ('", bv_strActvty_Nam, "') ")
                Else
                    strWhere = String.Concat(TrackingData.ACTVTY_NAM, " IN ('", bv_strActvty_Nam, "')")
                End If
            End If
            If strWhere <> "" Then
                strWhere = String.Concat(strWhere, " ORDER BY TRNSMSSN_NO DESC,TRCKNG_ID DESC ")
            Else
                strWhere = String.Concat(" ORDER BY TRNSMSSN_NO DESC,TRCKNG_ID DESC ")
            End If
            objData = New DataObjects(String.Concat(TRACKINGSelectQueryByDPT_ID, strWhere))
            objData.Fill(CType(ds, DataSet), TrackingData._V_TRACKING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateBulk_Email() TABLE NAME:BULK_EMAIL"

    Public Function CreateBulk_Email(ByVal bv_strFRM_EML As String, _
        ByVal bv_strTO_EML As String, _
        ByVal bv_strSBJCT_VC As String, _
        ByVal bv_strBDY_VC As String, _
        ByVal bv_datCRTD_DT As DateTime, _
        ByVal bv_strCRTD_BY As String, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_i32DPT_ID As Int32, _
        ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TrackingData._BULK_EMAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TrackingData._BULK_EMAIL, br_ObjTransactions)
                .Item(TrackingData.BLK_EML_BIN) = intMax
                .Item(TrackingData.FRM_EML) = bv_strFRM_EML
                .Item(TrackingData.TO_EML) = bv_strTO_EML
                If bv_strSBJCT_VC <> Nothing Then
                    .Item(TrackingData.SBJCT_VC) = bv_strSBJCT_VC
                Else
                    .Item(TrackingData.SBJCT_VC) = DBNull.Value
                End If
                If bv_strBDY_VC <> Nothing Then
                    .Item(TrackingData.BDY_VC) = bv_strBDY_VC
                Else
                    .Item(TrackingData.BDY_VC) = DBNull.Value
                End If
                .Item(TrackingData.SNT_DT) = DBNull.Value
                'If bv_datSNT_DT <> Nothing Then
                '    .Item(TrackingData.SNT_DT) = bv_datSNT_DT
                'Else
                '    .Item(TrackingData.SNT_DT) = DBNull.Value
                'End If
                .Item(TrackingData.CRTD_DT) = bv_datCRTD_DT
                .Item(TrackingData.CRTD_BY) = bv_strCRTD_BY
                .Item(TrackingData.ACTV_BT) = bv_blnACTV_BT
                If bv_i32DPT_ID <> 0 Then
                    .Item(TrackingData.DPT_ID) = bv_i32DPT_ID
                Else
                    .Item(TrackingData.DPT_ID) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Bulk_EmailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateBulk_Email = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateBulk_Email_Detail() TABLE NAME:BULK_EMAIL_DETAIL"

    Public Function CreateBulk_Email_Detail(ByVal bv_i64BLK_EML_BIN As Int64, _
        ByVal bv_strACTVTY_NAM As String, _
        ByVal bv_strTRNSMSSN_NO As String, _
        ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_strSTTS_VC As String, _
        ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TrackingData._BULK_EMAIL_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TrackingData._BULK_EMAIL_DETAIL, br_ObjTransactions)
                .Item(TrackingData.BLK_EML_DTL_BIN) = intMax
                .Item(TrackingData.BLK_EML_BIN) = bv_i64BLK_EML_BIN
                If bv_strACTVTY_NAM <> Nothing Then
                    .Item(TrackingData.ACTVTY_NAM) = bv_strACTVTY_NAM
                Else
                    .Item(TrackingData.ACTVTY_NAM) = DBNull.Value
                End If
                If bv_strTRNSMSSN_NO <> Nothing Then
                    .Item(TrackingData.TRNSMSSN_NO) = bv_strTRNSMSSN_NO
                Else
                    .Item(TrackingData.TRNSMSSN_NO) = DBNull.Value
                End If
                If bv_strEQPMNT_NO <> Nothing Then
                    .Item(TrackingData.EQPMNT_NO) = bv_strEQPMNT_NO
                Else
                    .Item(TrackingData.EQPMNT_NO) = DBNull.Value
                End If
                If bv_strSTTS_VC <> Nothing Then
                    .Item(TrackingData.STTS_VC) = bv_strSTTS_VC
                Else
                    .Item(TrackingData.STTS_VC) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Bulk_Email_DetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateBulk_Email_Detail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class

#End Region