Imports System.IO
Imports System.Data


Partial Class UserControls_iMenu
    Inherits System.Web.UI.UserControl

#Region "Declarations"
    Private _MenuLeft As Integer = 200
    Private _MenuTop As Integer = 26
    Private _OnCss As String = ""
    Private _OffCss As String = ""
    Private _Pageclass As String = ""
    Private _MenuPosition As String = ""
    Dim strMenuBuilder As New StringBuilder
    Private dtPARENTPROCESS As New DataTable("PARENTPROCESS")
    Private dtParentList As New DataTable
    Private _intTop As Integer = 30
    Private _intSubTop As Integer = 5
    Public _intcount As Integer = 0
#End Region

#Region "Properties of Menu"

    Public Property MenuLeft() As Integer
        Get
            MenuLeft = _MenuLeft
        End Get
        Set(ByVal Value As Integer)
            _MenuLeft = Value
        End Set
    End Property

    Public Property MenuTop() As Integer
        Get
            MenuTop = _MenuTop
        End Get
        Set(ByVal Value As Integer)
            _MenuTop = Value
        End Set
    End Property

    Public Property OnCss() As String
        Get
            OnCss = _OnCss
        End Get
        Set(ByVal Value As String)
            _OnCss = Value
        End Set
    End Property

    Public Property OffCss() As String
        Get
            OffCss = _OffCss
        End Get
        Set(ByVal Value As String)
            _OffCss = Value
        End Set
    End Property

    Public Property Pageclass() As String
        Get
            Pageclass = _Pageclass
        End Get
        Set(ByVal Value As String)
            _Pageclass = Value
        End Set
    End Property

    Public Property MenuPosition() As String
        Get
            MenuPosition = _MenuPosition
        End Get
        Set(ByVal Value As String)
            _MenuPosition = Value
        End Set
    End Property

#End Region

#Region "MenuControl Source"
    Public Function pub_SaveMenuFile(ByVal bv_strScript As String, ByVal bv_strPath As String, ByVal bv_strRoleName As String) As Boolean
        Try
            Dim strPath As String
            strPath = String.Concat(bv_strPath, bv_strRoleName.Trim.ToLower, "_menu.js")
            Dim aFileInfo As New System.IO.FileInfo(strPath)
            If aFileInfo.Exists = True Then
                If (aFileInfo.Attributes And FileAttributes.ReadOnly) = _
                FileAttributes.ReadOnly Then
                    aFileInfo.Attributes = aFileInfo.Attributes _
                           And Not FileAttributes.ReadOnly
                End If
            End If
            Dim fsOutputFile As New FileStream(strPath, FileMode.Create, FileAccess.ReadWrite)
            Dim srrOutputFile As New StreamWriter(fsOutputFile)
            srrOutputFile.Write(bv_strScript)
            srrOutputFile.Close()
            pub_SaveMenuFile = True
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
    Public Function pub_CreateMenuFile(ByVal bv_dsRoleRights As RoleRightDataSet, ByVal bolShowMenu As Boolean) As String
        Try
            If bolShowMenu Then
                strMenuBuilder.Append("<script language='javascript'>")
                strMenuBuilder.Append(vbCrLf)
                pvt_CreateMenu(bv_dsRoleRights)
                strMenuBuilder.Append("drawMenus();")
                strMenuBuilder.Append(vbCrLf)
                strMenuBuilder.Append("</script>")
                Page.ClientScript.RegisterClientScriptBlock(GetType(String), "MenuBlock", strMenuBuilder.ToString)
            Else
                strMenuBuilder.Append(vbCrLf)
                pvt_CreateMenu(bv_dsRoleRights)
                strMenuBuilder.Append(vbCrLf)
                strMenuBuilder.Append("drawMenus();")
                strMenuBuilder.Append(vbCrLf)
            End If
            pub_CreateMenuFile = strMenuBuilder.ToString
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function

    Private Sub pvt_CreateMenu(ByVal bv_dsRoleRightData As RoleRightDataSet)
        Try
            Dim drs As DataRow()
            Dim drs1 As DataRow()
            drs = bv_dsRoleRightData.Tables(RoleRightData._PARENTPROCESS_ROLE).Select("PRNT_ID IS NULL")
            drs1 = bv_dsRoleRightData.Tables(RoleRightData._PROCESS).Select("PRNT_ID IS NOT NULL")
            strMenuBuilder.Append(pvt_CreateParentMenugroup("menuTStyle", _MenuLeft, _MenuTop, "horizontal", drs))
            RecMenu(drs, bv_dsRoleRightData, "SubParent")
            RecMenu(drs1, bv_dsRoleRightData, "Child")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

    Private Sub RecMenu(ByVal drsProcess As DataRow(), ByVal bv_dsRoleRightData As RoleRightDataSet, ByVal bv_strNodeType As String)
        Try
            Dim drProcess As DataRow
            For Each drProcess In drsProcess
                Dim strRowfilter As String
                Dim drsActivities As DataRow()
                Dim drsSubProcess As DataRow()
                If bv_strNodeType = "SubParent" Then

                    strRowfilter = String.Concat(RoleRightData.PRNT_ID, "=", drProcess.Item("prcss_id"))
                    drsSubProcess = bv_dsRoleRightData.Tables(RoleRightData._PROCESS).Select(strRowfilter, "prcss_id ASC")
                    If drsSubProcess.Length > 0 Then
                        Dim strMenuTxt As New StringBuilder()
                        strMenuTxt.Append("with(CCC=new menuname(""")
                        strMenuTxt.Append(drProcess.Item(RoleRightData.PRCSS_NAM).ToString)
                        strMenuTxt.Append(""")){")
                        strMenuTxt.Append(vbCrLf & vbTab)
                        strMenuTxt.Append("style = ")
                        strMenuTxt.Append("menuStyle")
                        strMenuTxt.Append(";")
                        strMenuTxt.Append(vbCrLf & vbTab)
                        strMenuTxt.Append("overflow = ")
                        strMenuTxt.Append("""scroll""")
                        strMenuTxt.Append(";")
                        strMenuTxt.Append(vbCrLf)

                        Dim strSubProcessID As String = ","
                        For Each drSubProcess As DataRow In drsSubProcess
                            If strSubProcessID <> "," Then
                                strSubProcessID = String.Concat(strSubProcessID, ",")
                            End If
                            strSubProcessID = String.Concat(strSubProcessID, drSubProcess.Item("prcss_id"))
                        Next
                        strMenuBuilder.Append(strMenuTxt.ToString())

                        strRowfilter = String.Concat(RoleRightData.PRCSS_ID, " in (", drProcess.Item("prcss_id"), strSubProcessID, ")")
                        drsActivities = bv_dsRoleRightData.Tables(RoleRightData._ACTIVITY).Select(strRowfilter, "ordr_no ASC")

                        Dim processId As Integer = 0
                        Dim lstProcessId As New List(Of Int32)
                        'Duplicate menu (Repeated Same Menu) - Fixed
                        For Each drActivity As DataRow In drsActivities
                            If drActivity.Item("prcss_id") <> drProcess.Item("prcss_id") Then
                                'If processId <> drActivity.Item("prcss_id") Then
                                If Not lstProcessId.Contains(drActivity.Item("prcss_id")) Then
                                    drsSubProcess = bv_dsRoleRightData.Tables(RoleRightData._PROCESS).Select("prcss_id = " & drActivity.Item("prcss_id"), "prcss_id ASC")
                                    strMenuBuilder.Append(pvt_CreateMenuItems(CStr(drsSubProcess(0).Item(RoleRightData.PRCSS_NAM)).Trim, _
                                                            CStr(drsSubProcess(0).Item(RoleRightData.PRCSS_NAM)).Trim, CStr(drsSubProcess(0).Item(RoleRightData.PRCSS_NAM)).Trim, True))
                                End If
                                'processId = drActivity.Item("prcss_id")
                                lstProcessId.Add(drActivity.Item("prcss_id"))
                            Else
                                strMenuBuilder.Append(pvt_CreateMenuItems(CStr(drActivity.Item(RoleRightData.ACTVTY_ID)).Trim, CStr(drActivity.Item(RoleRightData.ACTVTY_NAM)).Trim, False))
                            End If
                        Next
                        strMenuBuilder.Append(vbCrLf & "}" & vbCrLf)
                    Else
                        strRowfilter = String.Concat(RoleRightData.PRCSS_ID, "=", drProcess.Item("prcss_id"))
                        drsActivities = bv_dsRoleRightData.Tables(RoleRightData._ACTIVITY).Select(strRowfilter, "ordr_no ASC")
                        strMenuBuilder.Append(pvt_CreateChildMenugroup("menuStyle", drProcess.Item(RoleRightData.PRCSS_NAM).ToString, drsActivities, bv_dsRoleRightData, bv_strNodeType, _intcount))
                    End If
                ElseIf bv_strNodeType = "Child" Then
                    strRowfilter = String.Concat(RoleRightData.PRCSS_ID, "=", drProcess.Item("prcss_id"))
                    drsActivities = bv_dsRoleRightData.Tables(RoleRightData._ACTIVITY).Select(strRowfilter, "ordr_no ASC")
                    strMenuBuilder.Append(pvt_CreateChildMenugroup("menuStyle", drProcess.Item(RoleRightData.PRCSS_NAM).ToString, drsActivities, bv_dsRoleRightData, bv_strNodeType, _intcount))
                End If
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
    Private Function pvt_CreateParentMenugroup(ByVal bv_strStyle As String, ByVal bv_intLeft As Integer, ByVal bv_intTop As Integer, ByVal bv_strOrientation As String, ByVal bv_drcMnuParentitem As DataRow()) As String
        Try
            Dim strMenuTxt As New StringBuilder
            strMenuTxt.Append("with(CCC=new menuname(""MainMenu"")){")
            strMenuTxt.Append(vbCrLf & vbTab)
            strMenuTxt.Append("style = ")
            strMenuTxt.Append(bv_strStyle)
            strMenuTxt.Append(";")
            strMenuTxt.Append(vbCrLf & vbTab)
            strMenuTxt.Append("top=")
            strMenuTxt.Append(bv_intTop)
            strMenuTxt.Append(";")
            strMenuTxt.Append(vbCrLf & vbTab)
            strMenuTxt.Append("left = ")
            strMenuTxt.Append(bv_intLeft)
            strMenuTxt.Append(";")
            strMenuTxt.Append(vbCrLf & vbTab)

            strMenuTxt.Append("orientation=""")
            strMenuTxt.Append(bv_strOrientation)
            strMenuTxt.Append(""";")
            strMenuTxt.Append(vbCrLf & vbTab)
            strMenuTxt.Append("alwaysvisible=1;")
            strMenuTxt.Append(vbCrLf)
            Dim drMenuitem As DataRow
            For i = 0 To bv_drcMnuParentitem.Length - 1
                drMenuitem = bv_drcMnuParentitem(i)
                If i = 0 Then
                    strMenuTxt.Append(pvt_CreateMenuItems(CStr(drMenuitem.Item(RoleRightData.PRCSS_NAM)).Trim, CStr(drMenuitem.Item(RoleRightData.PRCSS_NAM)).Trim, CStr(drMenuitem.Item(RoleRightData.PRCSS_NAM)).Trim, True))
                Else
                    strMenuTxt.Append(vbTab & "aI(""text=|;itemwidth=3"");")
                    strMenuTxt.Append(vbCrLf)
                    strMenuTxt.Append(pvt_CreateMenuItems(CStr(drMenuitem.Item(RoleRightData.PRCSS_NAM)).Trim, CStr(drMenuitem.Item(RoleRightData.PRCSS_NAM)).Trim, CStr(drMenuitem.Item(RoleRightData.PRCSS_NAM)).Trim, True))
                End If
            Next
            strMenuTxt.Append("}")
            strMenuTxt.Append(vbCrLf)
            pvt_CreateParentMenugroup = strMenuTxt.ToString
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function

    Private Function pvt_CreateSubParentMenugroup(ByVal bv_strStyle As String, ByVal bv_strMnuName As String, ByVal drsSubProcess As DataRow(), ByVal bv_dsRoleRightData As RoleRightDataSet, ByRef strMenuTxt As StringBuilder) As String
        Try

            Dim drMenuitem As DataRow
            For Each drMenuitem In drsSubProcess
                strMenuTxt.Append(pvt_CreateMenuItems(CStr(drMenuitem.Item(RoleRightData.PRCSS_NAM)).Trim, CStr(drMenuitem.Item(RoleRightData.PRCSS_NAM)).Trim, CStr(drMenuitem.Item(RoleRightData.PRCSS_NAM)).Trim, True))
            Next
            strMenuTxt.Append("}")
            strMenuTxt.Append(vbCrLf)
            pvt_CreateSubParentMenugroup = strMenuTxt.ToString
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function

    Private Function pvt_CreateChildMenugroup(ByVal bv_strStyle As String, ByVal bv_strMnuName As String, _
                                              ByVal bv_drsActivities As DataRow(), _
                                              ByVal bv_dsRoleRightData As RoleRightDataSet, _
                                              ByVal bv_strNodeType As String, ByVal bv_intCount As Integer, Optional skipParentMenu As Boolean = False) As String
        Try
            Dim bv_ParentMenu As Boolean = False
            Dim strMenuTxt As New StringBuilder
            If Not skipParentMenu Then
                strMenuTxt.Append("with(CCC=new menuname(""")
                strMenuTxt.Append(bv_strMnuName)
                strMenuTxt.Append(""")){")
                strMenuTxt.Append(vbCrLf & vbTab)
                strMenuTxt.Append("style = ")
                strMenuTxt.Append(bv_strStyle)
                strMenuTxt.Append(";")
                strMenuTxt.Append(vbCrLf & vbTab)
                strMenuTxt.Append("overflow = ")
                strMenuTxt.Append("""scroll""")
                strMenuTxt.Append(";")
                strMenuTxt.Append(vbCrLf)
            End If
          
            Dim drActivity As DataRow

            For Each drActivity In bv_drsActivities
                Try
                    strMenuTxt.Append(pvt_CreateMenuItems(CStr(drActivity.Item(RoleRightData.ACTVTY_ID)).Trim, CStr(drActivity.Item(RoleRightData.ACTVTY_NAM)).Trim, False))
                Catch ex As Exception
                    Dim sbrMenuName As New StringBuilder  'TT3513
                    Dim bv_xParentMenu As Boolean = True
                    sbrMenuName.Append(CStr(drActivity.Item(1)).Trim)
                    sbrMenuName.Append("_")
                    sbrMenuName.Append(CStr(drActivity.Item(0)).Trim)
                    strMenuTxt.Append(pvt_CreateMenuItems(CStr(drActivity.Item(1)).Trim, sbrMenuName.ToString, CStr(drActivity.Item(1)).Trim, bv_ParentMenu, bv_xParentMenu))
                End Try
            Next
            If Not skipParentMenu Then
                strMenuTxt.Append("}")
            End If
            strMenuTxt.Append(vbCrLf)
            pvt_CreateChildMenugroup = strMenuTxt.ToString
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
    Private Function pvt_CreateMenuItems(ByVal bv_strText As String, ByVal bv_strUrl As String, ByVal bv_strStatus As String, Optional ByVal bv_ParentMenu As Boolean = False, Optional ByVal bv_xParentMenu As Boolean = False) As String
        Try
            Dim strbValue As New StringBuilder
            If bv_ParentMenu Then
                strbValue.Append(vbTab)
                strbValue.Append("aI(""")
                strbValue.Append("text=")
                strbValue.Append(bv_strText)
                If bv_ParentMenu Then
                    strbValue.Append(";showmenu=")
                    strbValue.Append(bv_strUrl)
                Else
                    strbValue.Append(";url=")
                    strbValue.Append(bv_strUrl)
                End If
                strbValue.Append(";status=")
                strbValue.Append(bv_strStatus)
                strbValue.Append(""");")
                strbValue.Append(vbCrLf)
            Else
                strbValue.Append(vbTab)
                strbValue.Append("aI(")
                strbValue.Append("Activities[")
                strbValue.Append(bv_strText)
                strbValue.Append("]")
                strbValue.Append(");")
                strbValue.Append(vbCrLf)
            End If
            pvt_CreateMenuItems = strbValue.ToString
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region
End Class

