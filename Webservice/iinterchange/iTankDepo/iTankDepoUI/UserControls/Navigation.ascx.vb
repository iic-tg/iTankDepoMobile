

Partial Class UserControls_Navigation
    Inherits System.Web.UI.UserControl

#Region "Page_Load"
    ''' <summary>
    ''' This event will be fired on everytime when navigation pane loads.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Dim objCommonData As New CommonData
        If Request.QueryString("mode") = "view" Then
            pnlNavigation.Visible = False
        End If

        'If objCommonData.GetMultiLocationSupportConfig() Then
        '    lblUserDepotCode.Text = objCommonData.GetDepotCD()
        '    divNav.Style.Add("width", "550px")
        'Else
        '    divDepotCode.Visible = False
        'End If
    End Sub
#End Region

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/Navigation.js", MyBase.Page)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

