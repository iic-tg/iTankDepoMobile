
Partial Class Alerts
    Inherits Framebase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CStr(Request("se")) = "1" Then
            ClientScript.RegisterClientScriptBlock(GetType(String), "SessionExpired", "sessionExpired()", True)
        ElseIf CStr(Request("se")) = "2" Then
            ClientScript.RegisterClientScriptBlock(GetType(String), "MaliciousScriptFound", "maliciousScript()", True)
        End If
    End Sub

    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
End Class
