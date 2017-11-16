Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Xml

Partial Class Home
    Inherits Framebase
    Dim strTheme As String = Nothing
    Dim objCommonData As New CommonData
    Shared Container As System.Net.CookieContainer = Nothing
    Shared DashboardApi As New DashboardApi.DashboardApi
    Dim strReportList As String = ""
    Dim strSessionToken As String = ""
    Dim strToken As String = ""
    Dim username As String = ConfigurationManager.AppSettings("username")
    Dim password As String = ConfigurationManager.AppSettings("password")
    Dim Companyshortname As String = ConfigurationManager.AppSettings("CompanyShortName")
    Dim CompanyID As Integer = ConfigurationManager.AppSettings("CompanyID")
    Dim ReportID As Integer = ConfigurationManager.AppSettings("ReportID")
    Dim strUsername As String = ""
    Dim strRoleId As String = ""
    '  Dim objUserRole As New UserDetail
    Dim objRoleRight As New RoleRight
#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
        CommonWeb.IncludeScript("Script/UI/jquery.ui.js", MyBase.Page)
        CommonWeb.IncludeScript("Script/UI/Home.js", MyBase.Page)
        CommonWeb.IncludeScript("Script/Callback.js", MyBase.Page)
        CommonWeb.IncludeScript("Script/Controls/MenuSrc.js", MyBase.Page)
        If Not pub_RetrieveData("theme") Is Nothing Then
            strTheme = CStr(pub_RetrieveData("theme"))
        Else
            strTheme = "Default"
        End If
        CommonWeb.IncludeScript("Styles/" + strTheme + "/MenuStyle.js", MyBase.Page)
        CommonWeb.IncludeScript("Script/UI/Settings.js", MyBase.Page)
        CommonWeb.IncludeScript("Script/UI/ModalWindow.js", MyBase.Page)
        CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
    End Sub
#End Region

#Region "Page_Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                lblVersion.Text = String.Concat("Version : ", objCommonData.GetVersionNo())
                lblWelcome2.Text = String.Concat("<B>", objCommonData.GetCurrentUserName(), "</B>")

                If objCommonData.GetMultiLocationSupportConfig().ToLower = "true" Then
                    lblWelcome2.Style.Add("display", "none")
                    lblDepotCode2.Text = String.Concat("<B>", objCommonData.GetCurrentUserName(), ", ", objCommonData.GetDepotCD(), "</B>")
                Else
                    'lblWelcome2.Style.Add("display", "block")
                    lblDepotCode2.Text = "<B></B>"
                End If

                'hlkUser.Text = String.Concat("<B>", objCommonData.GetCurrentUserName(), "</B>")
                divProfile.Attributes.Add("onclick", String.Concat("ViewUserProfile('", objCommonData.GetCurrentUserID, "');"))
                lblLastLoginDt.Text = objCommonData.GetUserLastLoginDt()
                lblWelcome1.Attributes.Add("title", Request.Url.AbsoluteUri)
                If objCommonData.GetCustomerLogo() <> "" Then
                    Dim strLogoPath As String = Config.pub_GetAppConfigValue("UploadPhotoPath").Remove(0, 3)
                    If IO.File.Exists(String.Concat(Server.MapPath(strLogoPath) & objCommonData.GetCustomerLogo())) Then
                        customerlogo.Attributes.Add("src", strLogoPath & objCommonData.GetCustomerLogo())
                    Else
                        customerlogo.Attributes.Add("src", "images/logo.png")
                    End If
                Else
                    customerlogo.Attributes.Add("src", "images/logo.png")
                End If
                Dim strTitle As String
                strTitle = Config.pub_GetAppConfigValue("Title")
                If strTitle <> Nothing Then
                    lblWelcomeTitle.Text = String.Concat("<B>", strTitle, "</B>")
                End If
                'Menu Handler
                If Not pub_RetrieveData("theme") Is Nothing Then
                    strTheme = CStr(pub_RetrieveData("theme"))
                Else
                    strTheme = "Default"
                End If

                Page.ClientScript.RegisterStartupScript(GetType(String), "SetThemeName", _
                                  "fnSetThemeName('" & strTheme & "');", True)

                Dim versionNo As String = IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath(String.Concat("Styles/", strTheme, "/MenuStyle.js"))).ToString("yyMMddHHHmmss")
                Page.ClientScript.RegisterClientScriptInclude(GetType(String), "mstyle", String.Concat("Styles/", strTheme, "/MenuStyle.js?", versionNo))
                Page.ClientScript.RegisterClientScriptInclude(GetType(String), "settings", _
                                             "Settings.ashx?DepotId=" & objCommonData.GetDepotID() & "&ts=" & Format(Now, "yyyyMMddHHmmssffff"))
                'Page.ClientScript.RegisterClientScriptInclude(GetType(String), "roleright", _
                '                                   String.Concat("Menu.ashx?RL_CD=", _
                '                                                 objCommonData.GetCurrentRoleCode()))
                Page.ClientScript.RegisterClientScriptInclude(GetType(String), "roleright", _
                                                  String.Concat("Menu.ashx?RL_CD=", _
                                                                objCommonData.GetCurrentRoleCode(), "&ts=", DateTime.Now.ToFileTime())) 'added on 27.08.14
                'Maxlength Handler
                'Configuration
                Dim objConfigSetting As ConfigSetting
                If objCommonData.GetMultiLocationSupportConfig.ToLower = "true" Then
                    objConfigSetting = New ConfigSetting(objCommonData.GetHeadQuarterID())
                Else
                    objConfigSetting = New ConfigSetting(objCommonData.GetDepotID())
                End If
                Dim _ConfigJSON As String = New JavaScriptSerializer().Serialize(objConfigSetting)
                _ConfigJSON = "var ConfigClass= " & _ConfigJSON

                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "JsonKey", _ConfigJSON, True)
                Page.ClientScript.RegisterClientScriptInclude(GetType(String), "maxlength", _
                                              "Maxlength.ashx")

                'Settings Handler

                Dim strFavourites As String = objCommonData.GetFavouritesList()
                If strFavourites <> "" Then
                    hdnFavourites.Value = strFavourites
                End If
                Dim Role As String = objCommonData.GetCurrentRoleID()
                Dim blnRole As Boolean = objRoleRight.pub_ShowDashboard(CInt(Role))
                If blnRole Then
                    Page.ClientScript.RegisterStartupScript(GetType(String), "adashboard", _
                                         "el('dbFrame').src='Dashboard.aspx?ts=" & Format(Now, "yyyyMMddHHmmssffff") & "';", True)
                    '  pvt_getDashboardLoad()

                End If

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

    'Dashboard Load
    Private Sub pvt_getDashboardLoad()
        Try
            strSessionToken = DashboardApi.ivnInitializeSession(Companyshortname, username, "password", 0, 1, 1, "en", "")
            Dim Doc As XmlDocument = New XmlDocument()
            Doc.LoadXml(strSessionToken)
            Dim XmlNodeList As XmlNodeList = Doc.SelectNodes("//ResponseMessage/Session")
            For Each Node As XmlNode In XmlNodeList
                strToken = Node.Attributes("Token").Value
            Next
            strReportList = DashboardApi.ivnGetReportDetails(strToken, CompanyID, "")
            DashboardApi.ivnSetUIElementStatus(strToken, CompanyID, 1, True)
            DashboardApi.ivnSetUIElementStatus(strToken, CompanyID, 2, False)
            'Max Tool
            DashboardApi.ivnSetUIElementStatus(strToken, CompanyID, 3, True)
            Dim strToolXML As New StringBuilder
            Dim ToolEnable As String = "0"
            strToolXML.Append("<?xml version='1.0' encoding='utf-8'?>")
            strToolXML.Append("<WidgetMenu>")
            strToolXML.Append("<widgetlayouts enable='0' />")
            strToolXML.Append("<viewastable enable='0' />")
            strToolXML.Append("<viewaschart enable='0' />")
            strToolXML.Append("<showcolumnlist enable='0' />")
            strToolXML.Append("<editmode enable='0' />")
            strToolXML.Append("<refresh enable='1' />")
            strToolXML.Append("<maximizewidget enable='1' />")
            strToolXML.Append("<export enable='1' pdf='1' png='1' jpeg='1' csv='1' rtf='1' xls='1' xlsx='1' />")
            strToolXML.Append("<edit enable='0' />")
            strToolXML.Append("<info enable='0' />")
            strToolXML.Append("<closewidget enable='0'/>")
            strToolXML.Append("</WidgetMenu>")
            DashboardApi.ivnSetWidgetMenus(strToken, CompanyID, ReportID, strToolXML.ToString)

            Dim dashURL As String = DashboardApi.ivnShowReport(strToken, CompanyID, ReportID, "iDepo", "")
            Dim strDash As String = dashURL.Substring(dashURL.IndexOf("src"), dashURL.IndexOf("frameborder"))
            dbFrame.Attributes.Add("src", strDash.Replace("src=", "").Replace("framebor", "").Replace("'", "").Trim())
        Catch ex As Exception

        End Try
    End Sub

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GetKeyValue"
                    pvt_getConfigValue(e.GetCallbackValue("KeyName"))
                Case "SetFavourites"
                    pvt_setFavourites(e.GetCallbackValue("FavActivityIds"), e.GetCallbackValue("State"))
                Case "CreateTab"
                    pvt_CreateTab(e.GetCallbackValue("TableName"), e.GetCallbackValue("Title"), e.GetCallbackValue("Url"), e.GetCallbackValue("Type"))
                Case "CheckIsMaintenance"
                    pvt_CheckIsMaintenanace()
                Case "RemoveLockedRecordsClosingTab"
                    pvt_RemoveLockedRecordsClosingTab(e.GetCallbackValue("TabID"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_getConfigValue"
    Private Sub pvt_getConfigValue(ByVal bv_strKeyName As String)
        Try
            Dim objcommon As New CommonData
            Dim blnKeyExist As Boolean = False
            Dim strKeyValue As String
            Dim objConfigSetting As New ConfigSetting()
            strKeyValue = objConfigSetting.pub_GetConfigSingleValue(bv_strKeyName, objcommon.GetDepotID())
            blnKeyExist = objConfigSetting.IsKeyExists
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("KeyValue", strKeyValue)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "CreateTab"
    Private Sub pvt_CreateTab(ByVal bv_strTableName As String, _
                                           ByVal bv_strTitle As String, _
                                           ByVal bv_strUrl As String, _
                                           ByVal bv_strType As String)

        Try


            Dim objcommon As New CommonData
            Dim strCreatedTab As String = objcommon.Pub_CreateTab(bv_strTableName, bv_strType)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("TabPage", strCreatedTab)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_setFavourites"
    Private Sub pvt_setFavourites(ByVal bv_strFavActivityIds As String, ByVal bv_State As String)
        Try
            Dim objcommon As New CommonData()
            Dim objCommonui As New CommonUI()
            objCommonui.pub_UpdateUserDetailFavourites(CInt(objcommon.GetCurrentUserID()), bv_strFavActivityIds)
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                dtUser.Rows(0).Item(UserData.FAV_ACTVTY_ID_CSV) = bv_strFavActivityIds
            End If
            pub_SetCallbackStatus(True)
            pub_CacheData("UserData", dsUser)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CheckIsMaintenanace"

    Private Sub pvt_CheckIsMaintenanace()
        Try
            If ConfigurationManager.AppSettings("IsUnderMaintenance") Then
                Dim strTime As String = ConfigurationManager.AppSettings("MaintenanceTime")
                Dim strTimeStart As String = ConfigurationManager.AppSettings("MaintenanceStartTime")
                Dim strCollection As String() = Nothing
                strCollection = strTimeStart.Split(":")
                Dim curDateTime As Date = DateTime.Now.ToString("dd-MMM-yy")
                Dim curEndTime As DateTime = DateTime.Now
                curDateTime = curDateTime.AddHours(strCollection(0))
                curDateTime = curDateTime.AddMinutes(strCollection(1))
                curEndTime = curDateTime.AddMinutes(strTime)
                pub_SetCallbackReturnValue("SystemMessage", String.Concat("iTankdepo will be down for maintenance work from <b>", curDateTime.ToString("dd-MMM-yy hh:mm tt"), "</b> to <b>", curEndTime.ToString("dd-MMM-yy hh:mm tt"), "</b>. We apologize for any inconvenience."))
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackStatus(False)
            End If
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_RemoveLockedRecordsClosingTab"
    ''' <summary>
    ''' Record Locking
    ''' </summary>
    ''' <param name="bv_strTabID"></param>
    ''' <remarks></remarks>
    Private Sub pvt_RemoveLockedRecordsClosingTab(ByVal bv_strTabID As String)
        Try
            Dim objCommonData As New CommonData
            Dim strCurrentSessionId As String = objCommonData.GetSessionID()
            objCommonData.FlushLockDataByActivity(strCurrentSessionId, bv_strTabID)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub

#End Region



End Class
