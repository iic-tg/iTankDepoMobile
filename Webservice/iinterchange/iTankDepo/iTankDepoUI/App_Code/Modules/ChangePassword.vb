Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
 Public Class ChangePassword
    Inherits System.Web.Services.WebService

    <WebMethod(enableSession:=True)> _
     <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function ChangePassword(ByVal ChangePasswordCredentials As ChangePasswordCredentials) As ChangePasswordModel
        Dim objUser As New User
        Dim bolValid As Boolean
        Dim objcommon As New CommonData
        Dim blnUpdated As Boolean
        Dim strPassword As String
        Dim chnPssStatus As New ChangePasswordModel

        Dim bv_strOldPassword As String = ChangePasswordCredentials.bv_strOldPassword
        Dim Username As String = ChangePasswordCredentials.Username
        Dim bv_strNewPwd As String = ChangePasswordCredentials.bv_strNewPwd
        Try
            bv_strOldPassword = objUser.pub_EncryptPassword(bv_strOldPassword)

            bolValid = objUser.pub_ValidateUserPassword(bv_strOldPassword, Username)

            If bolValid = True Then
                strPassword = objUser.pub_EncryptPassword(bv_strNewPwd)
                blnUpdated = objUser.pub_ModifyUserPassword(Username, strPassword)
                If blnUpdated Then

                    chnPssStatus.changePassordStatus = "Your password has been changed successfully"
                    chnPssStatus.statusText = HttpContext.Current.Response.StatusDescription
                    chnPssStatus.stauscode = HttpContext.Current.Response.StatusCode



                    'Your password has been changed successfully
                Else
                    chnPssStatus.changePassordStatus = "There is an Error in changing password"

                    'There is an Error in changing password
                End If
            Else

                chnPssStatus.changePassordStatus = "The Old Password is Invalid"



            End If

            Return chnPssStatus

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                   MethodBase.GetCurrentMethod.Name, ex.Message)
            chnPssStatus.changePassordStatus = ex.Message
            chnPssStatus.statusText = HttpContext.Current.Response.StatusDescription
            chnPssStatus.stauscode = HttpContext.Current.Response.StatusCode

            Return chnPssStatus
        End Try



    End Function

End Class