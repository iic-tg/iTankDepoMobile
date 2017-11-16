Imports iInterchange.Framework.Common

Partial Class Admin_ReportError
    Inherits Framebase
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "ReportError"
                ReportError(e.GetCallbackValue("EmailTo"), e.GetCallbackValue("EmailSubject"), _
                         e.GetCallbackValue("EmailBody"))
        End Select
    End Sub

    Private Sub ReportError(ByVal bv_strSendTo As String, ByVal bv_strSubject As String, _
                    ByVal bv_strBody As String)
        Try
            Dim objCommonUI As New CommonUI
            Dim objEmail As New iEmailHandler
            Dim objCommon As New CommonData
            objEmail.pub_Send_Email(bv_strSendTo, bv_strSubject, bv_strBody)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
End Class
