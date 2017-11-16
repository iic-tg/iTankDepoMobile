Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Operations_PhotoViewer
    Inherits System.Web.UI.Page

#Region "Declarations"
    Dim pdsPhotos As PagedDataSource
    Dim intCurrentPosition As Integer
    Private Const CURRENT_POSITION As String = "CURRENT_POSITION"
    Private Const PHOTO_SESSION As String = "PHOTO_SESSION"
    Dim sqlConnection As SqlConnection
    Dim sqlCommand As SqlCommand
    Dim sqlDataAdatper As SqlDataAdapter
    Private strConnection As String = ConfigurationManager.ConnectionStrings("iTankDepoConnectionString").ConnectionString.ToString
    Private Const AttachmentSelectQueryByEstimateNo As String = "SELECT ATTCHMNT_ID,RPR_ESTMT_ID,ACTVTY_NAM,RPR_ESTMT_NO,GI_TRNSCTN_NO,ATTCHMNT_PTH,ACTL_FL_NM,MDFD_BY,MDFD_DT,DPT_ID FROM ATTACHMENT"
    Dim dtPhotos As DataTable
#End Region

#Region "pvt_OpenConnection"
    Private Sub pvt_OpenConnection(ByVal bv_strConnectionString As String)
        Try
            If sqlConnection.State = ConnectionState.Open Then
                sqlConnection.Close()
            End If
            sqlConnection = New SqlConnection(bv_strConnectionString)
            sqlConnection.Open()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                If Not Request.QueryString("Estimation_No") Is Nothing Then
                    Dim dtPhotoUpload As New DataTable
                    Dim intDepotId As Integer = 1
                    pvt_OpenConnection(strConnection)
                    sqlCommand = New SqlCommand(String.Concat(AttachmentSelectQueryByEstimateNo, "WHERE DPT_ID =" + intDepotId + " , AND , RPR_ESTMT_NO = '", Request.QueryString("Estimation_No").ToString, "'"), sqlConnection)
                    sqlDataAdatper = New SqlDataAdapter(sqlCommand)
                    sqlDataAdatper.Fill(dtPhotoUpload)
                    'dtPhotoUpload = objRepairEstimate.Pub_GetAttachmentByRepairEstimateNo(intDepotId, Request.QueryString("Estimation_No").ToString).Tables(RepairEstimateData._ATTACHMENT)
                    For Each drAttchment As DataRow In dtPhotoUpload.Rows
                        drAttchment.Item("ATTCHMNT_PTH") = String.Concat("iTankdepoUI/", drAttchment.Item("ATTCHMNT_PTH"))
                    Next
                    If dtPhotoUpload.Rows.Count > 0 Then
                        divnoRecords.Visible = False
                        Session(CURRENT_POSITION) = 0
                        intCurrentPosition = 0
                        Session(PHOTO_SESSION) = dtPhotoUpload
                        pvt_DataBind(dtPhotoUpload)
                        divPhotos.Visible = True
                        tdEstimationNo.InnerText = String.Concat("Estimation No : ", CStr(Request.QueryString("Estimation_No")))
                    Else
                        divPhotos.Visible = False
                        divnoRecords.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_DataBind"
    Private Sub pvt_DataBind(ByVal bv_dtPhotos As DataTable)
        Try
            pdsPhotos = New PagedDataSource()
            pdsPhotos.DataSource = bv_dtPhotos.DefaultView
            pdsPhotos.PageSize = 5
            pdsPhotos.AllowPaging = True
            pdsPhotos.CurrentPageIndex = intCurrentPosition
            btnfirst.Enabled = Not pdsPhotos.IsFirstPage
            btnprevious.Enabled = Not pdsPhotos.IsFirstPage
            btnlast.Enabled = Not pdsPhotos.IsLastPage
            btnnext.Enabled = Not pdsPhotos.IsLastPage
            dlPhotos.DataSource = pdsPhotos
            dlPhotos.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Button_Click (First,Next,Last,Previous)"
    Protected Sub btnfirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfirst.Click
        Try
            intCurrentPosition = 0
            dtPhotos = Session(PHOTO_SESSION)
            If Not dtPhotos Is Nothing Then
                pvt_DataBind(dtPhotos)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnlast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnlast.Click
        Try
            intCurrentPosition = pdsPhotos.PageCount - 1
            dtPhotos = Session(PHOTO_SESSION)
            If Not dtPhotos Is Nothing Then
                pvt_DataBind(dtPhotos)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnext.Click
        Try
            intCurrentPosition = CInt(Session(CURRENT_POSITION))
            intCurrentPosition += 1
            Session(CURRENT_POSITION) = intCurrentPosition
            dtPhotos = Session(PHOTO_SESSION)
            If Not dtPhotos Is Nothing Then
                pvt_DataBind(dtPhotos)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprevious.Click
        Try
            intCurrentPosition = CInt(Session(CURRENT_POSITION))
            intCurrentPosition -= 1
            Session(CURRENT_POSITION) = intCurrentPosition
            dtPhotos = Session(PHOTO_SESSION)
            If Not dtPhotos Is Nothing Then
                pvt_DataBind(dtPhotos)
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class
