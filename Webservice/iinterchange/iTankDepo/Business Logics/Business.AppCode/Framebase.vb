Imports Microsoft.VisualBasic

''' <summary>
''' This class is base class for modal window/popup page.
''' </summary>
''' <remarks></remarks>
Public Class Framebase
    Inherits iPageBase
    Private sbrClientScript As New StringBuilder
    Private arlGridIDs As ArrayList

#Region "Page_Load"
    ''' <summary>
    ''' This event will be fired on every page load for popup/modal window related pages alone
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsCallback Or Page.Request.QueryString("callbacktype") <> "fnGetData" Or pub_FramePage() Then
            Else
                pub_CheckAccess()
            End If
            
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_SessionCheck"
    ''' <summary>
    ''' This method used to check the session on every request
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_SessionCheck()
        If RetrieveData("UserData") Is Nothing Then
            HttpContext.Current.Response.AddHeader("sessiontimedout", "true")
            CommonWeb.pub_SessionExpired()
        End If
    End Sub
#End Region

#Region "pub_SetGridChanges"
    ''' <summary>
    ''' This method used set the grid changes for every tab
    ''' </summary>
    ''' <param name="bv_objFlexGrid">Denotes Grid object</param>
    ''' <param name="br_objTabPage">Denotes Tab page object</param>
    ''' <remarks></remarks>
    Public Sub pub_SetGridChanges(ByRef bv_objFlexGrid As iFlexGrid, ByRef br_objTabPage As TabPage)
        If arlGridIDs Is Nothing Then
            arlGridIDs = New ArrayList
        End If
        arlGridIDs.Add(String.Concat(br_objTabPage.ClientID, ";", bv_objFlexGrid.ClientID))
    End Sub

    ''' <summary>
    ''' This method used set the grid changes for every tab
    ''' </summary>
    ''' <param name="bv_objFlexGrid">Denotes Grid object</param>
    ''' <param name="br_strTabPageClientID">Denotes Tab page name</param>
    ''' <remarks></remarks>
    Public Sub pub_SetGridChanges(ByRef bv_objFlexGrid As iFlexGrid, ByRef br_strTabPageClientID As String)
        If arlGridIDs Is Nothing Then
            arlGridIDs = New ArrayList
        End If
        arlGridIDs.Add(String.Concat(br_strTabPageClientID, ";", bv_objFlexGrid.ClientID))
    End Sub
#End Region

#Region "pub_CheckAccess"
    ''' <summary>
    ''' This method used for code protection
    ''' </summary>
    ''' <remarks>Presently disabled</remarks>
    Public Shared Sub pub_CheckAccess()
        'If Not HttpContext.Current.Request.ServerVariables("SERVER_NAME").ToLower = "180.179.104.17" Then
        '        HttpContext.Current.Response.Status = "403 Access Forbidden"
        '        HttpContext.Current.Session.Abandon()
        '        HttpContext.Current.Response.End()
        'End If
    End Sub
#End Region

#Region "Page_LoadComplete"
    ''' <summary>
    ''' Page load complete event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            If Page.IsCallback = False Then
                sbrClientScript.Append(vbCrLf)
                sbrClientScript.Append(CommonWeb.RenderCommonScripts)
                ClientScript.RegisterStartupScript(GetType(System.String), "Pagebase", sbrClientScript.ToString)
                ClientScript.RegisterStartupScript(GetType(System.String), "roundedcorner", "$('.btncorner').corner();", True)
                If Page.Request.Url.ToString().ToUpper().IndexOf("DEFAULT.ASPX") = -1 AndAlso _
                    Page.Request.Url.ToString().ToUpper().IndexOf("LOGIN.ASPX") = -1 AndAlso _
                    Page.Request.Url.ToString().ToUpper().IndexOf("LOGOUT.ASPX") = -1 AndAlso _
   Page.Request.Url.ToString().ToUpper().IndexOf("PHOTOUPLOAD.ASPX") = -1 AndAlso _
                     Page.Request.Url.ToString().ToUpper().IndexOf("REPORTDIALOG.ASPX") = -1 AndAlso _
                     Page.Request.Url.ToString().ToUpper().IndexOf("REPORTVIEWER.ASPX") = -1 AndAlso _
                    Page.Request.Url.ToString().ToUpper().IndexOf("ALERTS.ASPX") = -1 Then
                    ClientScript.RegisterStartupScript(GetType(System.String), "checkpagelocation", "checkPageLocation();", True)
                    ClientScript.RegisterStartupScript(GetType(System.String), "loadInputControls", "loadInputControls();", True)
                End If
                Dim metaDescription As New HtmlMeta()
                metaDescription.HttpEquiv = "X-UA-Compatible"
                metaDescription.Content = "IE=8"
                Page.Header.Controls.AddAt(0, metaDescription)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "GetClientIPAddress"
    ''' <summary>
    ''' This method is used to get client ip adress for user tracking purpose
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetClientIPAddress() As String
        Return Request.UserHostAddress.ToString()
    End Function
#End Region

#Region "GetSessionID"
    ''' <summary>
    ''' This method used to get the current browsers session id
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetSessionID() As String
        Return Session.SessionID
    End Function
#End Region

#Region "GenerateLockMessage"
    ''' <summary>
    ''' This method used to build locking message
    ''' </summary>
    ''' <param name="bv_strUserName">Denotes User Name</param>
    ''' <param name="bv_strIPAddress">Denotes IP Address</param>
    ''' <param name="bv_strSessionID">Denotes Session ID</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GenerateLockMessage(ByVal bv_strUserName As String, ByVal bv_strIPAddress As String, ByVal bv_strSessionID As String) As String
        Dim objCommon As New CommonData()
        Dim strCurrentIPAddress As String = GetClientIPAddress()
        Dim strCurrentUserName As String = objCommon.GetCurrentUserName()
        Dim strCurrentSessionID As String = GetSessionID()
        Dim strMsg As String = ""

        If bv_strSessionID <> strCurrentSessionID And bv_strIPAddress = strCurrentIPAddress Then
            strMsg = String.Concat("This record is locked for editing by ", "<b>", bv_strUserName, "</b>", " in different session.")
        ElseIf bv_strIPAddress <> strCurrentIPAddress Then
            strMsg = String.Concat("This record is locked for editing by ", "<b>", bv_strUserName, "</b>", " in different place.")
        Else
            strMsg = String.Concat("This record is locked for editing by ", "<b>", bv_strUserName, "</b>", ".")
        End If
        Return strMsg
    End Function
#End Region
End Class
