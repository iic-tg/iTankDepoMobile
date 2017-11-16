
Partial Class Common_Popup
    Inherits Framebase


#Region "Page_LoadComplete"
    ''' <summary>
    ''' Page load complete event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            Dim sbrClientScript As New StringBuilder()
            sbrClientScript.Append(vbCrLf)
            sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""../Script/Callback.js""></script>")
            sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""../Script/Common.js""></script>")
            sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""../Script/UI/jquery.js""></script>")
            sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""../Script/UI/jquery.corner.js""></script>")
            ClientScript.RegisterStartupScript(GetType(System.String), "Pagebase", sbrClientScript.ToString)
            ClientScript.RegisterStartupScript(GetType(System.String), "roundedcorner", "$('.btncorner').corner();", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class
