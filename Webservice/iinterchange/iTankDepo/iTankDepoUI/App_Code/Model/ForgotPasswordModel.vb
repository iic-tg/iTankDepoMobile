Imports Microsoft.VisualBasic

Public Class ForgotPasswordModel

    Public Property validateEmailStatus() As String
    Public Property stauscode() As Integer
    Public Property statusText() As String

End Class


Public Class sendEmail

    Public Property emailStatus() As String
    Public Property stauscode() As Integer
    Public Property statusText() As String

End Class


Public Class exceptionModel

    Public Property exceptionStatus() As String
    Public Property stauscode() As Integer
    Public Property statusText() As String

End Class


Public Class ForgotCredentials

    Public Property bv_strEmailId() As String
    Public Property bv_strUserName() As String
    'Public Property statusText() As String

End Class

