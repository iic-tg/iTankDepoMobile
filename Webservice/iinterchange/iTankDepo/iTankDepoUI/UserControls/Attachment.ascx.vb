
Partial Class UserControls_Attachment
    Inherits System.Web.UI.UserControl

    ''' <summary>
    ''' This event will be on page load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("mode") = "view" Then
            aAttachFile.Visible = False
        End If
    End Sub
End Class
