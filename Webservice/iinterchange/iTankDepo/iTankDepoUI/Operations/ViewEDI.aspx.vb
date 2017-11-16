Option Strict On
Imports System.Data
Imports System.IO
Imports System.Security.AccessControl.DirectorySecurity
Imports System.Configuration
Partial Class Operations_ViewEDI
    'Inherits System.Web.UI.Page
    Inherits Pagebase
#Region "Declaration"
    Dim dtFolder As New DataTable()
    Dim strFolderName As String
    Dim strGetFolderName() As String
    Dim strDownloadFileName As String
    Dim strFileName As String
    Dim strFileFolder As String = UCase(ConfigurationManager.AppSettings("OutputFolder"))
#End Region

#Region "PageLoad"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                strDownloadFileName = Request.QueryString("Customer")
                If strDownloadFileName Is Nothing Then
                    UploadMethod()
                    dtFolder.DefaultView.Sort = "GeneratedDate DESC"
                    ifgEDIDetail.DataSource = dtFolder
                    ifgEDIDetail.DataBind()
                Else
                    fileDownload()
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)

        End Try
    End Sub
#End Region

#Region "Download File"
    Public Sub fileDownload()
        Try
            Dim downloadFilePath As String
            strDownloadFileName = Request.QueryString("Customer")
            strFileName = Request.QueryString("FileName")
            downloadFilePath = String.Concat(strFileFolder, strDownloadFileName, "\", strFileName)
            If System.IO.File.Exists(downloadFilePath) Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & strFileName)
                Response.TransmitFile(downloadFilePath)
                Response.End()
            Else
                Page.ClientScript.RegisterStartupScript(GetType(String), "error", "ShowErrorMessage(""File not available. It is deleted or archived from the server."");", True)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)

        End Try
    End Sub
#End Region

#Region "BindingGrid"
    Public Sub UploadMethod()
        Try
            Dim File As String = ""
            Dim GeneratedDate As String = ""
            Dim GeneratedTime As String = ""
            Dim Customer As String = ""
            Dim EDItype As String = ""
            Dim Activity As String = ""
            Dim getActivity As String = ""
            Dim creationDateTime As DateTime
            Dim fileCount As Integer
            Dim intNoFiles As Integer
            strGetFolderName = Directory.GetFileSystemEntries(strFileFolder)
            dtFolder.Columns.Add("Customer")
            dtFolder.Columns.Add("EDItype")
            dtFolder.Columns.Add("GeneratedDate")
            dtFolder.Columns.Add("GeneratedTime")
            dtFolder.Columns.Add("Activity")
            dtFolder.Columns.Add("FileName")

            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())


            For intNoFiles = 0 To strGetFolderName.Length - 1
                Dim strfile As String = String.Concat(strGetFolderName(intNoFiles), "\")
                Dim strfiles() As String = Directory.GetFileSystemEntries(strfile)
                strFolderName = strGetFolderName(intNoFiles).ToString.Substring(strGetFolderName(intNoFiles).LastIndexOf("\") + 1).ToString()
                For fileCount = 0 To strfiles.Length - 1
                    Customer = strFolderName
                    If FileExists(strfiles(fileCount)) Then
                        File = Path.GetFileName(strfiles(fileCount).Substring(strfiles(fileCount).LastIndexOf("\") + 1))
                        Dim dirinfo As New DirectoryInfo(Path.GetFileName(strfiles(fileCount)))
                        Dim f As New FileInfo(strfiles(fileCount))
                        If UCase(strfiles(fileCount)).EndsWith(".EXP") Or UCase(strfiles(fileCount)).EndsWith(".EXP") Then
                            creationDateTime = f.CreationTime
                            EDItype = "ANSCII"
                            GeneratedDate = creationDateTime.Date.ToString("dd-MMM-yyyy")
                            GeneratedTime = creationDateTime.ToString("HH:mm:ss")
                            getActivity = UCase(Regex.Replace(Path.GetFileNameWithoutExtension(strfiles(fileCount).Substring(strfiles(fileCount).LastIndexOf("\") + 1)), "[^A-Z][^a-z]", ""))
                            If getActivity = "WESTIM" Or getActivity = "WESTIMDT" Then
                                Activity = "ESTIMATE"
                            Else
                                Activity = getActivity
                            End If
                        End If
                        If UCase(strfiles(fileCount)).EndsWith(".TXT") Or UCase(strfiles(fileCount)).EndsWith(".TXT") Then
                            EDItype = "CODECO"
                            creationDateTime = f.CreationTime
                            GeneratedDate = creationDateTime.Date.ToString("dd-MMM-yyyy")
                            GeneratedTime = creationDateTime.ToString("HH:mm:ss")
                            getActivity = Regex.Replace(File, "[^A-Z]", "")
                            If getActivity = "WESTIM" Or getActivity = "WESTIMDT" Then
                                Activity = "ESTIMATE"
                            Else
                                Activity = getActivity
                            End If
                        End If
                        If UCase(strfiles(fileCount)).EndsWith(".RCV") Or UCase(strfiles(fileCount)).EndsWith(".RCV") Then
                            EDItype = ""
                        End If
                        If EDItype <> "" Then
                            Dim dr As DataRow
                            dr = dtFolder.NewRow()
                            dr.Item("Customer") = Customer
                            dr.Item("EDItype") = EDItype
                            dr.Item("GeneratedTime") = GeneratedTime
                            dr.Item("GeneratedDate") = GeneratedDate
                            dr.Item("Activity") = Activity
                            dr.Item("FileName") = File
                            dtFolder.Rows.Add(dr)
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)

        End Try
    End Sub
#End Region

#Region "FileExists"
    Private Function FileExists(ByVal FileFullPath As String) _
   As Boolean
        If Trim(FileFullPath) = "" Then Return False
        Dim f As New IO.FileInfo(FileFullPath)
        Return f.Exists
    End Function
#End Region

#Region "Grid RowDataBound"
    Protected Sub ifgEDIDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEDIDetail.RowDataBound
        Try
            strGetFolderName = Directory.GetFileSystemEntries(strFileFolder)
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strPath As String = String.Empty
                Dim strFileName As String = String.Empty
                Dim strFilePath As String = String.Empty

                For intNoFiles = 0 To strGetFolderName.Length - 1
                    Dim strfile As String = String.Concat(strGetFolderName(intNoFiles), "\")
                    Dim strfiles() As String = Directory.GetFileSystemEntries(strfile)
                    strFolderName = strGetFolderName(intNoFiles).ToString.Substring(strGetFolderName(intNoFiles).LastIndexOf("\") + 1).ToString
                    strFileName = strGetFolderName(intNoFiles)
                    Dim hlkAttachments1 As HyperLink
                    hlkAttachments1 = CType(e.Row.Cells(5).Controls(0), HyperLink)
                    hlkAttachments1.NavigateUrl = "#"
                    hlkAttachments1.Attributes.Add("onclick", "fnDownloadExcelFile();return false;")
                    hlkAttachments1.Text = CStr(strfiles(intNoFiles))
                Next
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script\Operations\ViewEDI.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
