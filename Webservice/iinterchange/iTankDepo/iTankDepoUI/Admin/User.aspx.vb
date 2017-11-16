

Partial Class Admin_User
    Inherits Pagebase

#Region "Declaration"

    Private Const KEY_ID As String = "ID"
    Private Const KEY_MODE As String = "mode"
    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Private pvt_lngID As Long
    Dim objCommonConfig As New ConfigSetting()
    Dim dsUser As New UserDataSet
   
#End Region

#Region "Parameters"

    Private pvt_strPageMode As String
    Private pvt_intID As Integer

#End Region

#Region "PageLoad"
    ''' <summary>
    ''' Thios event is fired on page load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objCommon As New CommonData()
            pvt_SetChangesMade()
            If Not Request.QueryString("userid") Is Nothing Then
                GetUserProfile(Request.QueryString("userid"))
                divhdr.Visible = False
                tabUser.Style("height") = "230px"
                navEstimate.Visible = False
                lnkEditPassword.Style.Add("display", "block")
                lnkEditPassword.Attributes.Add("onclick", "onEditClick();")
            End If
            Dim str_070Key As String
            Dim blnKeyExist As Boolean = False
            'str_070Key = objCommonConfig.pub_GetConfigSingleValue("070", blnKeyExist)
            str_070Key = objCommon.GetConfigSetting("070", blnKeyExist)
            If str_070Key.ToString.ToUpper = "FALSE" Then
                lkpDepotCode.ReadOnly = True
                lkpDepotCode.CssClass = "lkpd"
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    ''' <summary>
    ''' This method is used to initialise call back methods
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "fnGetData"
                pvt_GetData(e.GetCallbackValue("mode"))
            Case "InsertUser"
                pvt_CreateUser(e.GetCallbackValue("UserName"), _
                                e.GetCallbackValue("Password"), _
                                e.GetCallbackValue("FirstName"), _
                                e.GetCallbackValue("LastName"), _
                                e.GetCallbackValue("EmailID"), _
                                e.GetCallbackValue("RoleID"), _
                                e.GetCallbackValue("ActiveBit"), _
                                e.GetCallbackValue("PHT_PTH"), _
                                e.GetCallbackValue("THM_NAM"), _
                                e.GetCallbackValue("DepotID"), _
                                e.GetCallbackValue("wfData"))
            Case "UpdateUser"
                pvt_UpdateUser(e.GetCallbackValue("ID"), _
                                e.GetCallbackValue("UserName"), _
                                e.GetCallbackValue("Password"), _
                                e.GetCallbackValue("FirstName"), _
                                e.GetCallbackValue("LastName"), _
                                e.GetCallbackValue("EmailID"), _
                                e.GetCallbackValue("RoleID"), _
                                e.GetCallbackValue("ActiveBit"), _
                                 e.GetCallbackValue("PHT_PTH"), _
                                e.GetCallbackValue("THM_NAM"), _
                                e.GetCallbackValue("wfData"), _
                                e.GetCallbackValue("popup"), _
                                e.GetCallbackValue("DepotID"))
            Case "ValidateUserName"
                '     pvt_ValidatePK(e.GetCallbackValue("UserName"), e.GetCallbackValue("DepotID"))
                pvt_ValidatePK(e.GetCallbackValue("UserName"), e.GetCallbackValue("DepotID"))
        End Select
    End Sub
#End Region

#Region "pvt_GetData"
    ''' <summary>
    ''' This method is to load the datas in the page
    ''' </summary>
    ''' <param name="bv_strMode"></param>
    ''' <remarks></remarks>
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim objCommon As New CommonData()
            Dim sbUser As New StringBuilder
            If bv_strMode = "edit" Then
                sbUser.Append(CommonWeb.GetTextValuesJSO(txtUserName, PageSubmitPane.pub_GetPageAttribute("User Name")))
                sbUser.Append(CommonWeb.GetTextValuesJSO(txtFirstName, PageSubmitPane.pub_GetPageAttribute("First Name")))
                If (PageSubmitPane.pub_GetPageAttribute("Last Name")) = Nothing Then
                    sbUser.Append(CommonWeb.GetTextValuesJSO(txtLastName, ""))
                Else
                    sbUser.Append(CommonWeb.GetTextValuesJSO(txtLastName, PageSubmitPane.pub_GetPageAttribute("Last Name")))
                End If
                sbUser.Append(CommonWeb.GetTextValuesJSO(txtEmailId, PageSubmitPane.pub_GetPageAttribute("Email ID")))
                sbUser.Append(CommonWeb.GetLookupValuesJSO(lkpRole, PageSubmitPane.pub_GetPageAttribute(UserData.RL_ID), _
                                                    PageSubmitPane.pub_GetPageAttribute("Role Code")))

                If PageSubmitPane.pub_GetPageAttribute(UserData.THM_NAM) = "Blue" Then
                    sbUser.Append(CommonWeb.GetHtmlControlValuesJSO(rboBlue, True))

                ElseIf PageSubmitPane.pub_GetPageAttribute(UserData.THM_NAM) = "Matrix" Then
                    sbUser.Append(CommonWeb.GetHtmlControlValuesJSO(rboMatrix, True))

                ElseIf PageSubmitPane.pub_GetPageAttribute(UserData.THM_NAM) = "Classic" Then
                    sbUser.Append(CommonWeb.GetHtmlControlValuesJSO(rboClassic, True))

                Else
                    sbUser.Append(CommonWeb.GetHtmlControlValuesJSO(rboBlue, True))
                End If
                If PageSubmitPane.pub_GetPageAttribute("Active") <> Nothing Then
                    sbUser.Append(CommonWeb.GetCheckboxValuesJSO(chkActive, CBool(PageSubmitPane.pub_GetPageAttribute("Active"))))
                Else
                    sbUser.Append(CommonWeb.GetCheckboxValuesJSO(chkActive, False))
                End If
                Dim str_070Key As String

                Dim blnKeyExist As Boolean = False
                str_070Key = objCommon.GetConfigSetting("070", blnKeyExist)
                ' If str_070Key Then
                sbUser.Append(CommonWeb.GetLookupValuesJSO(lkpDepotCode, PageSubmitPane.pub_GetPageAttribute(UserData.DPT_ID), _
                                               PageSubmitPane.pub_GetPageAttribute("Depot Code")))
                'End If

                If PageSubmitPane.pub_GetPageAttribute(UserData.PHT_PTH) <> Nothing AndAlso
                    IO.File.Exists(String.Concat(Server.MapPath(Config.pub_GetAppConfigValue("UploadPhotoPath")), PageSubmitPane.pub_GetPageAttribute(UserData.PHT_PTH))) Then
                    sbUser.Append(CommonWeb.GetImageValuesJSO(imgCompanyLogo, String.Concat(Config.pub_GetAppConfigValue("UploadPhotoPath"), PageSubmitPane.pub_GetPageAttribute(UserData.PHT_PTH))))
                Else
                    sbUser.Append(CommonWeb.GetImageValuesJSO(imgCompanyLogo, "../Images/noimage.jpg"))
                End If

                sbUser.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(UserData.USR_ID), "');"))
                pub_SetCallbackReturnValue("Message", sbUser.ToString)
                pub_SetCallbackReturnValue("ID", pvt_lngID.ToString)
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateUser"
    ''' <summary>
    ''' This method is to call the business logic to insert the data
    ''' </summary>
    ''' <param name="bv_strUserName"></param>
    ''' <param name="bv_strPassword"></param>
    ''' <param name="bv_strFirstName"></param>
    ''' <param name="bv_strLastName"></param>
    ''' <param name="bv_strEmailID"></param>   
    ''' <param name="bv_i32RoleID"></param>
    ''' <param name="bv_strActiveBit"></param>
    ''' <param name="bv_strwfData"></param>
    ''' <remarks></remarks>

    Protected Sub pvt_CreateUser(ByVal bv_strUserName As String, _
                                 ByVal bv_strPassword As String, _
                                 ByVal bv_strFirstName As String, _
                                 ByVal bv_strLastName As String, _
                                 ByVal bv_strEmailID As String, _
                                 ByVal bv_i32RoleID As String, _
                                 ByVal bv_strActiveBit As String, _
                                 ByVal bv_strPhoto_PTH As String, _
                                 ByVal bv_strTheme_Name As String, _
                                 ByVal bv_i32DepotID As String, _
                                 ByVal bv_strwfData As String)

        Try
            Dim objUser As New User
            Dim objcommon As New CommonData
            Dim intDepotID As Integer = objcommon.GetDepotID()
            Dim str_070Key As String
            Dim blnKeyExist As Boolean = False
            str_070Key = objcommon.GetConfigSetting("070", blnKeyExist)
            If str_070Key.ToLower = "true" Then
                intDepotID = bv_i32DepotID
            End If

            Dim strPhoto_PTH As String = Nothing
            If bv_strPhoto_PTH <> Nothing Then
                strPhoto_PTH = bv_strPhoto_PTH.Substring(bv_strPhoto_PTH.LastIndexOf("/") + 1)
            End If
            If bv_strPassword <> Nothing Then
                bv_strPassword = objUser.pub_EncryptPassword(bv_strPassword) 'Encripts the password
            End If

            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As DateTime = objcommon.GetCurrentDate()
            Dim lng As Long = objUser.pub_CreateUserDetail(bv_strUserName, _
                bv_strPassword, _
                bv_strFirstName, _
                bv_strLastName, _
                bv_strEmailID, _
                CInt(bv_i32RoleID), _
                strModifiedby, _
                datModifiedDate, _
                bv_strActiveBit, _
                intDepotID, _
                strPhoto_PTH, bv_strTheme_Name, _
                bv_strwfData)

            pub_SetCallbackReturnValue("Message", String.Concat("User : ", bv_strUserName, " ", strMSGINSERT))
            pub_SetCallbackReturnValue("ID", CStr(lng))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateUser"
    ''' <summary>
    ''' This method is to call the business logic to update the data
    ''' </summary>
    ''' <param name="bv_strUserID"></param>
    ''' <param name="bv_strUserName"></param>
    ''' <param name="bv_strPassword"></param>
    ''' <param name="bv_strFirstName"></param>
    ''' <param name="bv_strLastName"></param>
    ''' <param name="bv_strEmailID"></param>    
    ''' <param name="bv_i32RoleID"></param>
    ''' <param name="bv_strActiveBit"></param>
    ''' <param name="bv_strwfData"></param>
    ''' <remarks></remarks>

    Protected Sub pvt_UpdateUser(ByVal bv_strUserID As String, _
                                ByVal bv_strUserName As String, _
                                ByVal bv_strPassword As String, _
                                ByVal bv_strFirstName As String, _
                                ByVal bv_strLastName As String, _
                                ByVal bv_strEmailID As String, _
                                ByVal bv_i32RoleID As String, _
                                ByVal bv_strActiveBit As String, _
                                ByVal bv_strPhoto_PTH As String, _
                                 ByVal bv_strTheme_Name As String, _
                                ByVal bv_strwfData As String, _
                                ByVal bv_strPopup As String, _
                                ByVal bv_i32DepotID As String)

        Try

            Dim objUser As New User
            Dim objcommon As New CommonData
            Dim str_070Key As String
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As DateTime = objcommon.GetCurrentDate()
            Dim intHeadQuartersID As Integer = objcommon.GetDepotID()
            Dim strPhoto_PTH As String = Nothing
            If bv_strPhoto_PTH <> Nothing Then
                strPhoto_PTH = bv_strPhoto_PTH.Substring(bv_strPhoto_PTH.LastIndexOf("/") + 1)
            End If
            If bv_strPassword <> Nothing Then
                bv_strPassword = objUser.pub_EncryptPassword(bv_strPassword) 'Encripts the password
            End If
            If bv_strUserID Is Nothing Or bv_strUserID = "" Then
                bv_strUserID = objcommon.GetCurrentUserID()
            End If
            Dim intDepotID As Integer = objcommon.GetDepotID()
            Dim blnkey As Boolean = False
            'str_070Key = objCommonConfig.pub_GetConfigSingleValue("070", blnkey)
            str_070Key = objcommon.GetConfigSetting("070", blnkey)
            If str_070Key Then
                intDepotID = bv_i32DepotID
            End If
            objUser.pub_ModifyUserDetail(bv_strUserID, _
                bv_strUserName, _
                bv_strPassword, _
                bv_strFirstName, _
                bv_strLastName, _
                bv_strEmailID, _
                CInt(bv_i32RoleID), _
                strModifiedby, _
                datModifiedDate, _
                bv_strActiveBit, _
                intDepotID, _
                strPhoto_PTH, bv_strTheme_Name, _
                bv_strwfData)
            If (bv_strPopup = "yes") Then
                pub_CacheData("theme", bv_strTheme_Name)
            End If
            pub_SetCallbackReturnValue("ThemeName", bv_strTheme_Name)
            pub_SetCallbackReturnValue("Message", String.Concat("User : ", bv_strUserName, " ", strMSGUPDATE))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ValidatePK()"
    ''' <summary>
    ''' This method is to validate the user name
    ''' </summary>
    ''' <param name="bv_strUserName"></param>
    ''' <remarks></remarks>
    Private Sub pvt_ValidatePK(ByVal bv_strUserName As String, ByVal bv_intDepoID As Integer)
        Try
            Dim objUser As New User
            Dim objCommon As New CommonData()
            Dim bolValid As Boolean
            Dim i32DepotId As Integer
            Dim str_070Key As String
            Dim blnKeyExist As Boolean = False
            str_070Key = objCommon.GetConfigSetting("070", blnKeyExist)
            If bv_intDepoID = 0 Then
                i32DepotId = CInt(objCommon.GetDepotID())
            Else
                i32DepotId = bv_intDepoID
            End If
            If str_070Key Then
                bolValid = objUser.pub_ValidatePKUserWithDepotID(bv_strUserName)
            Else
                bolValid = objUser.pub_ValidatePKUserWithDepotID(bv_strUserName, i32DepotId)
            End If

            If bolValid = True Then
                pub_SetCallbackReturnValue("pkValid", "true")
            Else
                pub_SetCallbackReturnValue("pkValid", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "GetRelativePathFromAbsolutePath"
    ''' <summary>
    ''' This method is to retrieve the relative path of the uploaded image
    ''' </summary>
    ''' <param name="bv_strAbsolutePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function pvt_GetRelativePathFromAbsolutePath(ByVal bv_strAbsolutePath As String) As String
        Dim strTemp As String = ""
        strTemp = bv_strAbsolutePath.Substring(bv_strAbsolutePath.IndexOf("/") + 1)
        While bv_strAbsolutePath.IndexOf("/") >= 0
            strTemp = strTemp.Substring(strTemp.IndexOf("/") + 1)
            If strTemp.IndexOf("/") < 0 Then
                Exit While
            End If
        End While

        Return strTemp
    End Function
#End Region

#Region "SetChangesMade"
    ''' <summary>
    ''' This method is used to set haschanges to all fields in page
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(txtUserName)
        CommonWeb.pub_AttachHasChanges(txtPassword)
        CommonWeb.pub_AttachHasChanges(txtFirstName)
        CommonWeb.pub_AttachHasChanges(txtLastName)
        CommonWeb.pub_AttachHasChanges(txtEmailId)
        CommonWeb.pub_AttachHasChanges(lkpRole)
        CommonWeb.pub_AttachHasChanges(chkActive)
        CommonWeb.pub_AttachHasChanges(rboBlue)
        CommonWeb.pub_AttachHasChanges(rboMatrix)
        CommonWeb.pub_AttachHasChanges(rboClassic)
    End Sub

#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Admin/User.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/MD5.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "GetUserProfile"

    Private Sub GetUserProfile(ByVal bv_intUserId As Integer)
        Try
            Dim objUser As New User
            Dim objCommon As New CommonData()
            dsUser = objUser.pub_GetUserDetailByUserID(bv_intUserId)
            If (dsUser.Tables(UserData._V_USER_DETAIL).Rows.Count > 0) Then
                txtUserName.Text = dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.USR_NAM)
                txtFirstName.Text = dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.FRST_NAM)
                txtLastName.Text = dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.LST_NAM)
                txtEmailId.Text = dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.EML_ID)
                lkpRole.Text = CStr(dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.RL_CD))
                lkpRole.LookupColumns(0).InitialValue = CStr(dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.RL_ID))
                lkpRole.ReadOnly = True
                lkpRole.CssClass = "lkpd"
                txtUserName.ReadOnly = True
                txtUserName.CssClass = "txtd"
                txtPassword.ReadOnly = True
                txtPassword.CssClass = "txtd"
                txtConfirmPassword.CssClass = "txtd"
                txtConfirmPassword.ReadOnly = True
                chkActive.Checked = CBool(dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.ACTV_BT))
                chkActive.Enabled = False
                If Not IsDBNull(dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.PHT_PTH)) AndAlso
                    IO.File.Exists(String.Concat(Server.MapPath(Config.pub_GetAppConfigValue("UploadPhotoPath")), dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.PHT_PTH))) Then
                    imgCompanyLogo.Src = String.Concat(Config.pub_GetAppConfigValue("UploadPhotoPath"), dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.PHT_PTH))
                Else
                    imgCompanyLogo.Src = "../Images/noimage.jpg"
                End If
                If Not IsDBNull(dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.THM_NAM)) Then
                    If dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.THM_NAM) = "Blue" Then
                        rboBlue.Checked = True

                    ElseIf dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.THM_NAM) = "Matrix" Then
                        rboMatrix.Checked = True

                    ElseIf dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.THM_NAM) = "Classic" Then
                        rboClassic.Checked = True
                    Else
                        rboBlue.Checked = True
                    End If
                Else
                    rboBlue.Checked = True
                End If
                Dim str_070Key As String
                Dim blnKeyExist As Boolean = False
                str_070Key = objCommon.GetConfigSetting("070", blnKeyExist)
                '  If str_070Key Then
                lkpDepotCode.Text = dsUser.Tables(UserData._V_USER_DETAIL).Rows(0).Item(UserData.DPT_CD)
                lkpDepotCode.ReadOnly = True
                lkpDepotCode.CssClass = "lkpd"
                'End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
End Class

