
Partial Class MoreInfo
    Inherits Framebase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtDescription.Text = Server.UrlDecode(GetQueryString("vl"))
        txtDescription.MaxLength = Server.UrlDecode(GetQueryString("mL"))
        If CBool(Server.UrlDecode(GetQueryString("readonly"))) Then
            txtDescription.ReadOnly = True
            txtDescription.CssClass = "txtd"
        Else
            txtDescription.ReadOnly = False
        End If
        txtDescription.Attributes.Add("mL", Server.UrlDecode(GetQueryString("mL")))
        txtDescription.Attributes.Add("onkeypress", String.Concat("return imposeMaxLength(this,", txtDescription.MaxLength - 1, ");"))
        txtDescription.Attributes.Add("onpaste", "checkDataLength(this);return false;")
    End Sub
End Class
