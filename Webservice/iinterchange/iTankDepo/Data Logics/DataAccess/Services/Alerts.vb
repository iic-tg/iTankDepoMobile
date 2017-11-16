Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.Text

Public Class Alerts


#Region "Declaration Part.. "
    Dim objData As DataObjects
    Private Const AlertSettingSelectQueryByactvty_id As String = "SELECT actvty_id,alrt_tmplt_id,alrt_rf_fld_vcr,alrt_qry_vcr,alrt_tp_fld FROM AlertSetting WHERE actvty_id=@actvty_id"
    Private Const AlertSettingSelectQueryByalrt_tmplt_id As String = "SELECT actvty_id,alrt_tmplt_id,alrt_rf_fld_vcr,alrt_qry_vcr,alrt_tp_fld FROM AlertSetting WHERE alrt_tmplt_id=@alrt_tmplt_id"
    Private Const AlertTemplateSelectQueryByalrt_tmplt_id As String = "SELECT alrt_tmplt_id,alrt_tmplt_nam,alrt_fl_pth FROM AlertTemplate WHERE alrt_tmplt_id=@alrt_tmplt_id"
    Private Const PHAlertInsertQuery As String = "INSERT INTO PHAlert(alrt_bin,actvty_id,trnsctn_id,orgnstn_id,alrt_de_dt,alrt_sbjct_vcr,alrt_cntnt_xml,alrt_to,alrt_cc,alrt_stts_bt,crtd_by,crtd_dt)VALUES(@alrt_bin,@actvty_id,@trnsctn_id,@orgnstn_id,@alrt_de_dt,@alrt_sbjct_vcr,@alrt_cntnt_xml,@alrt_to,@alrt_cc,@alrt_stts_bt,@crtd_by,@crtd_dt)"
    Private Const PHAlertUpdateQuery As String = "UPDATE PHAlert SET alrt_bin=@alrt_bin, actvty_id=@actvty_id, trnsctn_id=@trnsctn_id, orgnstn_id=@orgnstn_id,alrt_de_dt=@alrt_de_dt, alrt_sbjct_vcr=@alrt_sbjct_vcr, alrt_cntnt_xml=@alrt_cntnt_xml, alrt_to=@alrt_to, alrt_cc=@alrt_cc, alrt_stts_bt=@alrt_stts_bt, alrt_lst_snd_dt=@alrt_lst_snd_dt, crtd_by=@crtd_by, crtd_dt=@crtd_dt WHERE alrt_bin=@alrt_bin"
    Private Const PHAlertSelectQueryByActivityTransID As String = "SELECT alrt_bin,actvty_id,trnsctn_id,orgnstn_id,alrt_de_dt,alrt_sbjct_vcr,alrt_cntnt_xml,alrt_to,alrt_cc,alrt_stts_bt,alrt_lst_snd_dt,crtd_by,crtd_dt FROM PHAlert WHERE actvty_id=@actvty_id and trnsctn_id=@trnsctn_id and orgnstn_id=@orgnstn_id"
    Private Const PQAlertInsertQuery As String = "INSERT INTO PQAlert(alrt_bin,actvty_id,trnsctn_id,orgnstn_id,alrt_de_dt,alrt_sbjct_vcr,alrt_cntnt_xml,alrt_to,alrt_cc,crtd_by,crtd_dt)VALUES(@alrt_bin,@actvty_id,@trnsctn_id,@orgnstn_id,@alrt_de_dt,@alrt_sbjct_vcr,@alrt_cntnt_xml,@alrt_to,@alrt_cc,@crtd_by,@crtd_dt)"
    Private Const PQAlertUpdateQuery As String = "UPDATE PQAlert SET alrt_bin=@alrt_bin, actvty_id=@actvty_id, trnsctn_id=@trnsctn_id, orgnstn_id=@orgnstn_id,alrt_de_dt=@alrt_de_dt,alrt_sbjct_vcr=@alrt_sbjct_vcr, alrt_cntnt_xml=@alrt_cntnt_xml, alrt_to=@alrt_to, alrt_cc=@alrt_cc, crtd_by=@crtd_by, crtd_dt=@crtd_dt WHERE alrt_bin=@alrt_bin"
    Private Const PQAlertSelectQueryByActivityTransID As String = "SELECT alrt_bin,actvty_id,trnsctn_id,orgnstn_id,alrt_de_dt,alrt_sbjct_vcr,alrt_cntnt_xml,alrt_to,alrt_cc,crtd_by,crtd_dt FROM PQAlert WHERE actvty_id=@actvty_id and trnsctn_id=@trnsctn_id and orgnstn_id=@orgnstn_id"
    Private Const PQAlertSelectQuery As String = "SELECT alrt_bin,actvty_id,trnsctn_id,orgnstn_id,alrt_de_dt,alrt_sbjct_vcr,alrt_cntnt_xml,alrt_to,alrt_cc,crtd_by,crtd_dt FROM PQAlert"
    Private Const PQAlertDeleteQuery As String = "DELETE FROM PQAlert WHERE alrt_bin=@alrt_bin"
    Private Const PHAlertUpdateStatusQuery As String = "UPDATE PHAlert SET alrt_stts_bt=@alrt_stts_bt,alrt_lst_snd_dt=@alrt_lst_snd_dt WHERE alrt_bin=@alrt_bin"
    Private ds As AlertDataSet

#End Region

#Region "Constructor.."

    Sub New()
        ds = New AlertDataSet
    End Sub

#End Region

#Region "GetAlertRecordByTransactionID"

    Public Function GetAlertRecordByTransactionID(ByVal bv_strAlertPendingQuery As String, _
                                                  ByVal bv_strSubmitRefField As String, _
                                                  ByVal bv_strTransactionNo As String, _
                                                  ByVal bv_strOrderBy As String, ByVal bv_strDBKey As String) As DataSet
        Try
            Dim dsPending As New DataSet
            Dim sbBuildQuery As New StringBuilder
            Dim sbAttachCondition As New StringBuilder

            sbAttachCondition.Append(bv_strSubmitRefField)
            sbAttachCondition.Append("='")
            sbAttachCondition.Append(bv_strTransactionNo)
            sbAttachCondition.Append("'")

            bv_strAlertPendingQuery = AlterSQLQuery(bv_strAlertPendingQuery, sbAttachCondition.ToString())

            sbBuildQuery.Append(bv_strAlertPendingQuery)

            If bv_strOrderBy <> Nothing Then
                sbBuildQuery.Append(" order by ")
                sbBuildQuery.Append(bv_strOrderBy)
            End If

            objData = New DataObjects(sbBuildQuery.ToString())
            objData.Fill(dsPending, "AlertTable")

            Return dsPending
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "AlterSQLQuery"
    Private Function AlterSQLQuery(ByVal baseQry As String, ByVal strattachCondition As String) As String
        Try
            Dim strQry As New StringBuilder
            Dim orderbyindex As Integer
            Dim temp2Str As String
            Dim tempStr As String
            orderbyindex = baseQry.LastIndexOf(" ORDER BY ", System.StringComparison.OrdinalIgnoreCase)
            If orderbyindex > 0 Then
                tempStr = baseQry.Substring(0, orderbyindex)
                temp2Str = baseQry.Substring(orderbyindex)
                strQry.Append("Select TEMP.* from (")
                strQry.Append(tempStr)
                strQry.Append(") TEMP WHERE ")
                strQry.Append(strattachCondition)
                strQry.Append(temp2Str)

            Else
                strQry.Append("Select TEMP.* from (")
                strQry.Append(baseQry)
                strQry.Append(") TEMP WHERE ")
                strQry.Append(strattachCondition)
            End If
            Return strQry.ToString
            strQry = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreatePHAlert() TABLE NAME:Alert"

    Public Function CreatePHAlert(ByVal bv_stractvty_id As String, ByVal bv_strtrnsctn_id As String, _
                                  ByVal bv_intorgnstn_id As String, ByVal bv_datalrt_de_dt As DateTime, _
                                  ByVal bv_stralrt_sbjct_vcr As String, ByVal bv_stralrt_cntnt_xml As String, _
                                  ByVal bv_stralrt_to As String, ByVal bv_stralrt_cc As String, _
                                  ByVal bv_blnalrt_stts_bt As Boolean, ByVal bv_strcrtd_by As String, _
                                  ByVal bv_datcrtd_dt As DateTime) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects(True)
            Dim insQry As String
            dr = ds.Tables(AlertData._PHALERT).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(AlertData._PHALERT)
                .Item(AlertData.ALRT_BIN) = intMax
                .Item(AlertData.ACTVTY_ID) = bv_stractvty_id
                .Item(AlertData.TRNSCTN_ID) = bv_strtrnsctn_id
                .Item(AlertData.ORGNSTN_ID) = 0
                If bv_datalrt_de_dt <> Nothing Then
                    .Item(AlertData.ALRT_DE_DT) = bv_datalrt_de_dt
                Else
                    .Item(AlertData.ALRT_DE_DT) = DBNull.Value
                End If
                .Item(AlertData.ALRT_SBJCT_VCR) = bv_stralrt_sbjct_vcr
                .Item(AlertData.ALRT_CNTNT_XML) = bv_stralrt_cntnt_xml
                .Item(AlertData.ALRT_TO) = bv_stralrt_to
                If bv_stralrt_cc <> String.Empty Then
                    .Item(AlertData.ALRT_CC) = bv_stralrt_cc
                Else
                    .Item(AlertData.ALRT_CC) = DBNull.Value
                End If
                .Item(AlertData.ALRT_STTS_BT) = bv_blnalrt_stts_bt
                .Item(AlertData.CRTD_BY) = bv_strcrtd_by
                .Item(AlertData.CRTD_DT) = bv_datcrtd_dt
            End With
            insQry = PHAlertInsertQuery
            objData.InsertRow(dr, insQry)
            dr = Nothing
            CreatePHAlert = intMax
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "UpdatePHAlert() TABLE NAME:Alert"

    Public Function UpdatePHAlert(ByVal bv_i64alrt_bin As Int64, ByVal bv_i32actvty_id As Int32, _
                                ByVal bv_strtrnsctn_id As String, ByVal bv_intorgnstn_id As String, _
                                ByVal bv_datalrt_de_dt As DateTime, ByVal bv_stralrt_sbjct_vcr As String, _
                                ByVal bv_stralrt_cntnt_xml As String, ByVal bv_stralrt_to As String, _
                                ByVal bv_stralrt_cc As String, ByVal bv_blnalrt_stts_bt As Boolean, _
                                ByVal bv_strcrtd_by As String, ByVal bv_datcrtd_dt As DateTime) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects(True)
            dr = ds.Tables(AlertData._PHALERT).NewRow()
            With dr
                .Item(AlertData.ALRT_BIN) = bv_i64alrt_bin
                .Item(AlertData.ACTVTY_ID) = bv_i32actvty_id
                .Item(AlertData.TRNSCTN_ID) = bv_strtrnsctn_id
                .Item(AlertData.ORGNSTN_ID) = 0
                If bv_datalrt_de_dt <> Nothing Then
                    .Item(AlertData.ALRT_DE_DT) = bv_datalrt_de_dt
                Else
                    .Item(AlertData.ALRT_DE_DT) = DBNull.Value
                End If
                .Item(AlertData.ALRT_SBJCT_VCR) = bv_stralrt_sbjct_vcr
                .Item(AlertData.ALRT_CNTNT_XML) = bv_stralrt_cntnt_xml
                .Item(AlertData.ALRT_TO) = bv_stralrt_to
                If bv_stralrt_cc <> String.Empty Then
                    .Item(AlertData.ALRT_CC) = bv_stralrt_cc
                Else
                    .Item(AlertData.ALRT_CC) = DBNull.Value
                End If
                .Item(AlertData.ALRT_STTS_BT) = bv_blnalrt_stts_bt
                .Item(AlertData.CRTD_BY) = bv_strcrtd_by
                .Item(AlertData.CRTD_DT) = bv_datcrtd_dt
            End With
            UpdatePHAlert = objData.UpdateRow(dr, PHAlertUpdateQuery)
            dr = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetPHAlertByActivity_TransID"
    Public Function GetPHAlertByActivity_TransID(ByVal bv_intActivityID As Integer, _
                                               ByVal bv_strRefValue As Integer, _
                                               ByVal bv_intOrganisationID As Integer) As AlertDataSet

        Try
            Dim hstbl As New Hashtable
            hstbl.Add(AlertData.ACTVTY_ID, bv_intActivityID)
            hstbl.Add(AlertData.TRNSCTN_ID, bv_strRefValue)
            hstbl.Add(AlertData.ORGNSTN_ID, bv_intOrganisationID)
            objData = New DataObjects(PHAlertSelectQueryByActivityTransID, hstbl)
            objData.Fill(CType(ds, DataSet), AlertData._PHALERT)
            Return ds

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "GetAlertSettingByActivityID"
    Public Function GetAlertSettingByActivityID(ByVal bv_intActivityID As Integer) As AlertDataSet
        Try
            objData = New DataObjects(AlertSettingSelectQueryByactvty_id, AlertData.ACTVTY_ID, CStr(bv_intActivityID))
            objData.Fill(CType(ds, DataSet), AlertData._ALERTSETTING)
            Return ds

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "GetAlertSettingByTemplateID"
    Public Function GetAlertSettingByTemplateID(ByVal bv_intTemplateID As Integer) As AlertDataSet
        Try
            objData = New DataObjects(AlertSettingSelectQueryByalrt_tmplt_id, AlertData.ALRT_TMPLT_ID, CStr(bv_intTemplateID))
            objData.Fill(CType(ds, DataSet), AlertData._ALERTSETTING)
            Return ds

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "GetTemplatePathByTemplateID"
    Public Function GetTemplatePathByTemplateID(ByVal bv_intTemplateID As Integer) As AlertDataSet
        Try
            objData = New DataObjects(AlertTemplateSelectQueryByalrt_tmplt_id, AlertData.ALRT_TMPLT_ID, CStr(bv_intTemplateID))
            objData.Fill(CType(ds, DataSet), AlertData._ALERTTEMPLATE)
            Return ds

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "CreatePQAlert() TABLE NAME:PQAlert"

    Public Function CreatePQAlert(ByVal bv_i32actvty_id As Int32, ByVal bv_strtrnsctn_id As String, _
                                  ByVal bv_intorgnstn_id As String, ByVal bv_datalrt_de_dt As DateTime, _
                                  ByVal bv_stralrt_sbjct_vcr As String, ByVal bv_stralrt_cntnt_xml As String, _
                                  ByVal bv_stralrt_to As String, ByVal bv_stralrt_cc As String, _
                                  ByVal bv_strcrtd_by As String, ByVal bv_datcrtd_dt As DateTime) As Long

        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects(True)
            Dim insQry As String
            dr = ds.Tables(AlertData._PQALERT).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(AlertData._PQALERT)
                .Item(AlertData.ALRT_BIN) = intMax
                .Item(AlertData.ACTVTY_ID) = bv_i32actvty_id
                .Item(AlertData.TRNSCTN_ID) = bv_strtrnsctn_id
                .Item(AlertData.ORGNSTN_ID) = 0
                If bv_datalrt_de_dt <> Nothing Then
                    .Item(AlertData.ALRT_DE_DT) = bv_datalrt_de_dt
                Else
                    .Item(AlertData.ALRT_DE_DT) = DBNull.Value
                End If

                .Item(AlertData.ALRT_SBJCT_VCR) = bv_stralrt_sbjct_vcr
                .Item(AlertData.ALRT_CNTNT_XML) = bv_stralrt_cntnt_xml
                .Item(AlertData.ALRT_TO) = bv_stralrt_to
                If bv_stralrt_cc <> String.Empty Then
                    .Item(AlertData.ALRT_CC) = bv_stralrt_cc
                Else
                    .Item(AlertData.ALRT_CC) = DBNull.Value
                End If
                .Item(AlertData.CRTD_BY) = bv_strcrtd_by
                .Item(AlertData.CRTD_DT) = bv_datcrtd_dt
            End With
            insQry = PQAlertInsertQuery
            objData.InsertRow(dr, insQry)
            dr = Nothing
            CreatePQAlert = intMax

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdatePQAlert() TABLE NAME:PQAlert"

    Public Function UpdatePQAlert(ByVal bv_i64alrt_bin As Int64, ByVal bv_i32actvty_id As Int32, _
                                  ByVal bv_strtrnsctn_id As String, ByVal bv_intorgnstn_id As String, _
                                  ByVal bv_datalrt_de_dt As DateTime, ByVal bv_stralrt_sbjct_vcr As String, _
                                  ByVal bv_stralrt_cntnt_xml As String, _
                                  ByVal bv_stralrt_to As String, ByVal bv_stralrt_cc As String, _
                                  ByVal bv_strcrtd_by As String, ByVal bv_datcrtd_dt As DateTime) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects(True)
            dr = ds.Tables(AlertData._PQALERT).NewRow()
            With dr
                .Item(AlertData.ALRT_BIN) = bv_i64alrt_bin
                .Item(AlertData.ACTVTY_ID) = bv_i32actvty_id
                .Item(AlertData.TRNSCTN_ID) = bv_strtrnsctn_id
                .Item(AlertData.ORGNSTN_ID) = 0
                If bv_datalrt_de_dt <> Nothing Then
                    .Item(AlertData.ALRT_DE_DT) = bv_datalrt_de_dt
                Else
                    .Item(AlertData.ALRT_DE_DT) = DBNull.Value
                End If
                .Item(AlertData.ALRT_SBJCT_VCR) = bv_stralrt_sbjct_vcr
                .Item(AlertData.ALRT_CNTNT_XML) = bv_stralrt_cntnt_xml
                .Item(AlertData.ALRT_TO) = bv_stralrt_to
                If bv_stralrt_cc <> String.Empty Then
                    .Item(AlertData.ALRT_CC) = bv_stralrt_cc
                Else
                    .Item(AlertData.ALRT_CC) = DBNull.Value
                End If
                .Item(AlertData.CRTD_BY) = bv_strcrtd_by
                .Item(AlertData.CRTD_DT) = bv_datcrtd_dt
            End With
            UpdatePQAlert = objData.UpdateRow(dr, PQAlertUpdateQuery)
            dr = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetPQAlertByActivity_TransID"
    Public Function GetPQAlertByActivity_TransID(ByVal bv_intActivityID As Integer, _
                                               ByVal bv_strRefValue As Integer, _
                                               ByVal bv_intOrganisationID As Integer) As AlertDataSet
        Try
            Dim hstbl As New Hashtable
            hstbl.Add(AlertData.ACTVTY_ID, bv_intActivityID)
            hstbl.Add(AlertData.TRNSCTN_ID, bv_strRefValue)
            hstbl.Add(AlertData.ORGNSTN_ID, bv_intOrganisationID)
            objData = New DataObjects(PQAlertSelectQueryByActivityTransID, hstbl)
            objData.Fill(CType(ds, DataSet), AlertData._PQALERT)
            Return ds

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "GetPQAlerts"
    Public Function GetPQAlerts() As AlertDataSet
        Try
            objData = New DataObjects(PQAlertSelectQuery)
            objData.Fill(CType(ds, DataSet), AlertData._PQALERT)
            Return ds

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeletePQAlert() TABLE NAME:PQAlert"

    Public Function DeletePQAlert(ByVal bv_alrt_bin As Int64) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects(True)

            dr = ds.Tables(AlertData._PQALERT).NewRow()
            With dr
                .Item(AlertData.ALRT_BIN) = bv_alrt_bin
            End With
            DeletePQAlert = objData.DeleteRow(dr, PQAlertDeleteQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdatePHAlertStatus() TABLE NAME:PHAlert"

    Public Function UpdatePHAlertStatus(ByVal bv_i64alrt_bin As Int64) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects(True)
            dr = ds.Tables(AlertData._PHALERT).NewRow()
            With dr
                .Item(AlertData.ALRT_BIN) = bv_i64alrt_bin
                .Item(AlertData.ALRT_STTS_BT) = True
                .Item(AlertData.ALRT_LST_SND_DT) = DateTime.Now
            End With
            UpdatePHAlertStatus = objData.UpdateRow(dr, PHAlertUpdateStatusQuery)
            dr = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
