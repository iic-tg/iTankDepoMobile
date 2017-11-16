
Partial Class Logout
    Inherits Framebase
    Dim objCommonData As New CommonData

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strIpAddress As String = Request.ServerVariables("REMOTE_ADDR")
            Dim strSessionId As String = Session.SessionID
            Dim strUserName As String = objCommonData.GetCurrentUserName()

            'Clear all the locked sessions by current user (Record Locking)
            objCommonData.ClearLockData(strSessionId)

            If strUserName IsNot Nothing Then
                Dim objuser As New User
                Dim objCom As New CommonData
                objuser.pub_UpdateUserLog(strUserName, objCom.GetCurrentDate(), strIpAddress, strSessionId)
                Dim objCommon As New CommonUI
                If Not objCommon.ValidateLicense(CInt(objCommonData.GetDepotID())) Then
                    Response.Redirect("Login.aspx?se=4")
                End If
                Session.Abandon()
            End If


            FormsAuthentication.SignOut()
            Session.Abandon()

            If Request.QueryString("dm") Is Nothing Then
                If Request.QueryString("lo") IsNot Nothing AndAlso Request.QueryString("lo") = "true" Then
                    Response.Redirect("Login.aspx", False)
                Else
                    Response.Write("<script language='javascript'>" & vbCrLf)
                    Response.Write("window.setTimeout('window.close();', 1000);" & vbCrLf)
                    Response.Write("</script>")
                End If
            Else
                Response.Redirect("Login.aspx?se=1")
            End If




            If CStr(Request("se")) = "1" Then
                ClientScript.RegisterClientScriptBlock(GetType(String), "session expired", _
                                                    "parent.document.Script.browserinvalid = true;parent.document.location.href='default.aspx?se=1'", True)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase. _
                                                                           GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub


End Class
