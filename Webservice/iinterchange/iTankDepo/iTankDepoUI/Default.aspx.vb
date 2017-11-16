
Partial Class _Default
    Inherits Framebase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

                Dim dsUser As New UserDataSet
                Dim iCache As New iNCache
                Dim strSessionID As String = Session.SessionID
                If GetNCache(strSessionID) Is Nothing Then
                    SetNCache(strSessionID, iCache) 'New Session created here
                    ClientScript.RegisterClientScriptBlock(GetType(String), "redirect6", "ShowLogin('0');", True)
                Else
                    If Request.QueryString("share") = "true" Then
                        downloadShortCut.Style.Add("display", "block")
                    Else
                        'dsUser = CType(RetrieveData("UserData"), UserDataSet)
                        'If Not dsUser Is Nothing Then
                        'ClientScript.RegisterClientScriptBlock(GetType(String), "redirect6", "ShowHome('0');", True)
                        'Else
                        ClientScript.RegisterClientScriptBlock(GetType(String), "redirect6", "ShowLogin('0');", True)
                        'End If
                    End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex)

        End Try
    End Sub

   

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Callback.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Settings.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region


End Class
