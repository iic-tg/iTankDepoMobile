Imports System.Drawing
Imports System.Drawing.Imaging
Partial Class Admin_PhotoUpload
    Inherits System.Web.UI.Page
    Private strSize As String = ConfigurationSettings.AppSettings("UploadPhotoSize")

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim strFileName As String
        Dim strFileExtension As String
        Dim intLastIndex As Integer
        Dim strLogoPath As String
        Dim sbrFileSize As New StringBuilder
        If Request.Files.Count = 1 Then
            Try
                strFileName = imagebrowse.PostedFile.FileName
                'intLastIndex = strFileName.LastIndexOf("\")
                'If intLastIndex > 0 Then
                'intLastIndex += 1
                If strFileName <> "" Then
                    strFileName = strFileName.Substring(intLastIndex, (strFileName.Length - intLastIndex))
                    strFileExtension = UCase(strFileName.Substring(strFileName.Length - 4, 4))
                    If UCase(strFileExtension) = ".JPG" Or UCase(strFileExtension) = ".PNG" Then
                        If imagebrowse.PostedFile.ContentLength > strSize Then
                            sbrFileSize.Append("showErrorMessage('File size exceeds allowed limit. Maximum of ")
                            sbrFileSize.Append(String.Concat(strSize / 1024000, " MB "))
                            sbrFileSize.Append("is allowed');")

                            Page.ClientScript.RegisterStartupScript(GetType(String), "error1key",
                                      sbrFileSize.ToString(), True)
                            Exit Sub
                        End If

                        Dim strGuid As String = pvt_fnGuid()
                        strFileName = strGuid + strFileExtension
                        imagebrowse.PostedFile.SaveAs(Server.MapPath(Config.pub_GetAppConfigValue("UploadPhotoPath")) + strFileName)
                        strLogoPath = Config.pub_GetAppConfigValue("UploadPhotoPath") + strFileName
                        hdnLogoPath.Value = strLogoPath

                        Page.ClientScript.RegisterStartupScript(GetType(String), "setimgkey",
                         String.Concat("pel('imgCompanyLogo').src=""", strLogoPath, """;"), True)
                    Else
                        Page.ClientScript.RegisterStartupScript(GetType(String), "error1key",
                                       "showErrorMessage(""Only JPEG Image or PNG Image (.jpg,.png) can be uploaded."");", True)
                    End If
                    'ElseIf intLastIndex = -1 Then
                    'Exit Sub
                Else
                    Page.ClientScript.RegisterStartupScript(GetType(String), "error2key",
                 "showErrorMessage(""Please Select a Image!"");", True)
                End If
            Catch ex As Exception
                iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase. _
                                                   GetCurrentMethod.Name, ex.Message)
            End Try
        End If
    End Sub

    Public Shared Function GetImageCodec(ByVal extension As String) As ImageCodecInfo
        extension = extension.ToUpperInvariant()
        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders()
        For Each codec As ImageCodecInfo In codecs
            If codec.FilenameExtension.Contains(extension) Then
                Return codec
            End If
        Next
        Return codecs(1)
    End Function

    Function pvt_fnGuid() As String
        Dim strGuid As String = ""
        Try
            With Today
                strGuid = DateTime.Now.ToFileTime() '.Year & .Month & .Day & Now.Hour & Now.Minute & Now.Second
            End With
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase. _
                                               GetCurrentMethod.Name, ex.Message)
        End Try
        Return strGuid
    End Function

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Admin/Depot.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
End Class
