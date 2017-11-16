Option Strict On

Partial Class Admin_ChangePassword
    Inherits Framebase

    Private Const pvt_strCHANGEPWD As String = "Change Password Failed"

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "ValidatePassword"
                pvt_ValidatePassword(e.GetCallbackValue("OldPassword"))
            Case "ChangePassword"
                pvt_ChangePassword(e.GetCallbackValue("NewPwd"))
        End Select
    End Sub
#End Region

#Region "pvt_ValidatePassword"
    Private Sub pvt_ValidatePassword(ByVal bv_strOldPassword As String)
        Try
            Dim objUser As New User
            Dim bolValid As Boolean
            Dim objcommon As New CommonData
            bv_strOldPassword = objUser.pub_EncryptPassword(bv_strOldPassword)

            bolValid = objUser.pub_ValidateUserPassword(bv_strOldPassword, objcommon.GetCurrentUserName())

            If bolValid = True Then
                pub_SetCallbackReturnValue("pkValid", "true")
            Else
                pub_SetCallbackReturnValue("pkValid", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            pub_SetCallbackError("Validation Fails")
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub
#End Region

#Region "pvt_ChangePassword"
    Private Sub pvt_ChangePassword(ByVal bv_strNewPwd As String)
        Try
            Dim objUserDetail As New User
            Dim blnUpdated As Boolean
            Dim strPassword As String
            Dim objCommonData As New CommonData
            strPassword = objUserDetail.pub_EncryptPassword(bv_strNewPwd)
            blnUpdated = objUserDetail.pub_ModifyUserPassword(objCommonData.GetCurrentUserName(), strPassword)
            If blnUpdated Then
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackStatus(False)
            End If
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            pub_SetCallbackError(pvt_strCHANGEPWD)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                   MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/MD5.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Callback.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Admin/ChangePwd.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.corner.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.ui.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/HelpTip.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region
End Class
