
Partial Class Admin_RoleRight
    Inherits Pagebase

#Region "DECLARATIONS"

    '''Private pvt_wf As workflowfn
    Private pvt_dsActivity As RoleRightDataSet
    Private pvt_dsRoleRights As RoleRightDataSet

    Private clnRoleRights As New Collection
    Private Const KEY_ID As String = "ID"
    Private strRemoveProcessIDs As String = "10,11"
    Private strexceptionalActivityIDs As String = ""
    Private Const DASHBOARD_PROCESS_ID As String = "17"
    Private ChkAdd As Boolean
    Private ChkEdit As Boolean
    Private ChkView As Boolean
    Private ChkCancel As Boolean
#End Region

#Region "PageLoad"
    ''' <summary>
    ''' This event is fired on page load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                pvt_SetChangesMade()
                btnSubmit.Attributes.Add("onclick", "Submit_Click();return false;")
            End If
            Page.ClientScript.RegisterStartupScript(GetType(String), "", " el('iconFav').style.display = 'none';", True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region




#Region "SetChangesMade"
    ''' <summary>
    ''' This method is to set changes to all the fields
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(txtRoleCode)
        CommonWeb.pub_AttachHasChanges(txtRoleDescription)
        CommonWeb.pub_AttachHasChanges(chkActiveBit)
        CommonWeb.pub_AttachHasChanges(chkDashboardActiveBit)
    End Sub

#End Region

#Region "ACTION PANE LOAD"

    ''' <summary>
    ''' ACTION PANE LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub PageSubmitPane_ActionPaneLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles PageSubmitPane.SubmitPaneLoad
        Try
            If Not Page.IsPostBack Then
                Dim objRoleRight As New RoleRight
                If PageSubmitPane.pub_GetPageAttribute(KEY_ID) <> Nothing Then
                    txtRoleCode.Text = PageSubmitPane.pub_GetPageAttribute("Role")
                    txtRoleDescription.Text = PageSubmitPane.pub_GetPageAttribute("Description")
                    chkActiveBit.Checked = CBool(PageSubmitPane.pub_GetPageAttribute("Active"))
                    If PageSubmitPane.pub_GetPageAttribute("Dashboard") Is Nothing Then
                        chkDashboardActiveBit.Checked = False
                    Else
                        chkDashboardActiveBit.Checked = CBool(PageSubmitPane.pub_GetPageAttribute("Dashboard"))
                    End If
                    txtRoleCode.ReadOnly = True
                    hdnRoleId.Value = PageSubmitPane.pub_GetPageAttribute(KEY_ID)
                    hdnRoleCode.Value = PageSubmitPane.pub_GetPageAttribute("Role")
                    hdnDescription.Value = PageSubmitPane.pub_GetPageAttribute("Description")
                    hdnActive.Value = CBool(PageSubmitPane.pub_GetPageAttribute("Active"))
                    If PageSubmitPane.pub_GetPageAttribute("Dashboard") Is Nothing Then
                        hdnDashboardActive.Value = False
                    Else
                        hdnDashboardActive.Value = CBool(PageSubmitPane.pub_GetPageAttribute("Dashboard"))
                    End If
                    txtRoleCode.CssClass = "txtd"
                    txtRoleDescription.CssClass = "txto"
                    txtRoleDescription.Focus()
                Else
                    hdnRoleId.Value = 0
                    txtRoleCode.CssClass = "txto"
                    txtRoleCode.Focus()
                End If
                'TODO: Append the Activity or process Id's here to prevent them  from appearing in the Role Rights Grid.
                Dim objCommon As New CommonData()
                Dim blnKeyExist As Boolean = False
                Dim strKeyvalue As String = objCommon.GetConfigSetting("030", blnKeyExist)
                If blnKeyExist Then
                    strRemoveProcessIDs = strKeyvalue
                Else
                    strRemoveProcessIDs = ""
                End If
                hdnPageMode.Value = GetQueryString("mode")
                blnKeyExist = False
                strexceptionalActivityIDs = objCommon.GetConfigSetting("031", blnKeyExist)
                If blnKeyExist Then
                    pvt_dsActivity = objRoleRight.pub_GetActivity(strRemoveProcessIDs, strexceptionalActivityIDs)
                Else
                    pvt_dsActivity = objRoleRight.pub_GetActivity("", "")
                End If

                pvt_BuildProcessTree(pvt_dsActivity, False)
                plhTree.Controls.Add(New LiteralControl(ITreeView1.BuildTree()))
                CacheData("RoleRightData", pvt_dsRoleRights)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                               MethodBase.GetCurrentMethod.Name, ex)
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
        Try
            Select Case e.CallbackType
                Case "validateRoleCode"
                    pvt_ValidateRoleCode(e.GetCallbackValue("RL_CD"), e.GetCallbackValue("RL_ID"))
            End Select
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "SUBMIT"
    ''' <summary>
    ''' Get all keys in the form and commit with postback
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitDummy.Click
        Try
            Dim objTempCollection As EVDCollection
            Dim objRoleRight As New RoleRight
            'Dim objAct As New Activity
            Dim strIds() As String
            Dim blnHasChanges As Boolean
            Dim blnAddRight As Boolean = True
            Dim blnEditRight As Boolean = True
            Dim lngID As Long
            Dim objcommon As New CommonData
            pvt_dsRoleRights = RetrieveData("RoleRightData")
            If blnEditRight Or blnAddRight Then

                'Getting all keys present in the form
                Dim strKeys() As String = Request.Form.AllKeys
                If pvt_ProcessKeys(strKeys) Then
                    If hdnModify.Value = "true" OrElse txtRoleCode.Text <> hdnRoleCode.Value OrElse txtRoleDescription.Text <> hdnDescription.Value OrElse chkActiveBit.Checked <> hdnActive.Value OrElse chkDashboardActiveBit.Checked <> hdnDashboardActive.Value Then
                        pvt_dsRoleRights.Tables(RoleRightData._ROLE_RIGHT).Clear()
                        For Each objTempCollection In clnRoleRights
                            strIds = objTempCollection.Id.Split("N")
                            'Only child nodes are activities parent nodes are process so select child node 
                            'specified by char(C) for child nodes and for parent nodes char(P).
                            If strIds(0) = "C" Then
                                Dim dtRow As DataRow = pvt_dsRoleRights.Tables(RoleRightData._ROLE_RIGHT).NewRow
                                dtRow(RoleRightData.ACTVTY_ID) = strIds(1)
                                dtRow(RoleRightData.VW_BT) = objTempCollection.View
                                dtRow(RoleRightData.EDT_BT) = objTempCollection.Edit
                                dtRow(RoleRightData.CRT_BT) = objTempCollection.Add
                                dtRow(RoleRightData.CNCL_BT) = objTempCollection.Cancel
                                pvt_dsRoleRights.Tables(RoleRightData._ROLE_RIGHT).Rows.Add(dtRow)
                            End If
                        Next
                        'If chkDashboardActiveBit.Checked = False Then
                        '    Page.ClientScript.RegisterStartupScript(GetType(String), "adashboard", _
                        '                "psc().el(""aDashboard"").style.visibility=""hidden"";", True)
                        'Else
                        '    Page.ClientScript.RegisterStartupScript(GetType(String), "adashboard", _
                        '             "psc().el(""aDashboard"").style.visibility=""visible"";", True)

                        'End If
                        If hdnPageMode.Value = "edit" Then
                            objRoleRight.pub_ModifyRole(hdnRoleId.Value, txtRoleCode.Text, txtRoleDescription.Text, pvt_dsRoleRights, objcommon.GetCurrentUserName(), objcommon.GetCurrentDate(), chkActiveBit.Checked, objcommon.GetDepotID(), chkDashboardActiveBit.Checked)
                            Page.ClientScript.RegisterStartupScript(GetType(String), "insertkey", _
                                           String.Concat("ppsc(", Request.QueryString("activityid"), ").refreshListPane();showInfoMessage(""", "Role Rights : ", txtRoleCode.Text, " Updated Successfully"");"), True)
                        Else
                            lngID = objRoleRight.pub_CreateRole(txtRoleCode.Text, txtRoleDescription.Text, pvt_dsRoleRights, objcommon.GetCurrentUserName(), objcommon.GetCurrentDate(), chkActiveBit.Checked, objcommon.GetDepotID(), chkDashboardActiveBit.Checked)
                            hdnRoleId.Value = CInt(lngID)
                            hdnPageMode.Value = "edit"
                            Page.ClientScript.RegisterStartupScript(GetType(String), "updatekey", _
                                            String.Concat("ppsc(", Request.QueryString("activityid"), ").refreshListPane();showInfoMessage(""", "Role Rights : ", txtRoleCode.Text, " Inserted Successfully"");"), True)
                        End If

                        hdnDescription.Value = txtRoleDescription.Text
                        hdnRoleCode.Value = txtRoleCode.Text
                        hdnActive.Value = chkActiveBit.Checked
                        hdnDashboardActive.Value = chkDashboardActiveBit.Checked
                        hdnModify.Value = "false"
                        blnHasChanges = True
                    Else
                        blnHasChanges = False
                        Page.ClientScript.RegisterStartupScript(GetType(String), "nochangeskey", _
                                                            "showInfoMessage(""No Changes to Save"");", True)
                    End If
                End If
            End If
            If Not hdnRoleId.Value = "" Then
                Dim bln031KeyExist As Boolean
                Dim bln030KeyExist As Boolean
                strRemoveProcessIDs = objcommon.GetConfigSetting("030", bln030KeyExist)
                strexceptionalActivityIDs = objcommon.GetConfigSetting("031", bln031KeyExist)
                pvt_dsActivity = objRoleRight.pub_GetActivity(strRemoveProcessIDs, strexceptionalActivityIDs)
                pvt_BuildProcessTree(pvt_dsActivity, blnHasChanges)
                plhTree.Controls.Add(New LiteralControl(ITreeView1.BuildTree()))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                               MethodBase.GetCurrentMethod.Name, ex)

        End Try
    End Sub
#End Region

#Region "BUILD PROCESS TREE"
    ''' <summary>
    ''' Building Process Tree
    ''' </summary>
    ''' <param name="bv_dsActivity"></param>
    ''' <remarks></remarks>
    Private Sub pvt_BuildProcessTree(ByRef bv_dsActivity As RoleRightDataSet, ByVal bv_blnHasChanges As Boolean)
        Dim dtProcess As New DataTable(RoleRightData._PROCESS)
        Dim dtRoleRights As New DataTable(RoleRightData._ROLE_RIGHT)
        Dim dtActivity As New DataTable(RoleRightData._ACTIVITY)

        Dim dtParentProcess As New DataTable(RoleRightData._PROCESS_PARENT)


        Dim sbrRoleIds As New StringBuilder

        Dim drsActivity As DataRow()

        Dim drsRoleRights As DataRow()
        Dim drsAddRight As DataRow()
        Dim drsEditRight As DataRow()
        Dim drsViewRight As DataRow()
        Dim drsCancelRight As DataRow()
        Dim drsProcess As DataRow()

        Dim intCheckedAddRightCount As Integer = 0
        Dim intCheckedEditRightCount As Integer = 0
        Dim intCheckedViewRight As Integer = 0
        Dim intCheckedCancelRight As Integer = 0
        Dim intTotalActivityCount As Integer = 0
        Dim intTotalProcessCount As Integer = 0

        Dim intCreateRight As Integer = 0
        Dim intEditRight As Integer = 0
        Dim intCancelRight As Integer = 0

        Dim blnViewCheckboxReadOnly As Boolean = False

        Dim objRoleRights As New RoleRight
        Dim strParentNodeKey As String
        Dim strChildNodeKey As String

        dtProcess = bv_dsActivity.Tables(RoleRightData._PROCESS)
        dtActivity = bv_dsActivity.Tables(RoleRightData._ACTIVITY)
        For Each drActvtyRl As DataRow In dtActivity.Rows
            If Not drActvtyRl(RoleRightData.ACTVTY_RL).ToString = "" And Not drActvtyRl(RoleRightData.ACTVTY_RL).ToString = String.Empty Then
                Dim strItems() As String
                strItems = drActvtyRl(RoleRightData.ACTVTY_RL).ToString.Split(CChar(","))
                For i As Integer = 0 To strItems.Length - 1
                    If strItems(i).Substring(strItems(i).IndexOf("<") + 1, strItems(i).IndexOf(":", strItems(i).IndexOf("<")) - strItems(i).IndexOf("<") - 1) = "M" Then
                        Dim result As String = strItems(i).Substring(strItems(i).IndexOf(":") + 1, strItems(i).IndexOf(">", strItems(i).IndexOf(":")) - strItems(i).IndexOf(":") - 1)
                        Dim bln_KeyExist As Boolean
                        Dim objcommon As New CommonData
                        Dim strKeyValue As String = objcommon.GetConfigSetting(result, bln_KeyExist)
                        If bln_KeyExist Then
                            drActvtyRl(RoleRightData.ACTVTY_NAM) = strKeyValue
                        End If
                    End If
                Next
            End If
        Next

        pvt_dsRoleRights = objRoleRights.pub_GetRoleRightByRoleID(CInt(hdnRoleId.Value))
        If pvt_dsRoleRights.Tables(RoleRightData._ACTIVITY).Rows.Count > 0 Then
            For Each drActvtyRl As DataRow In pvt_dsRoleRights.Tables(RoleRightData._ACTIVITY).Rows
                If Not drActvtyRl(RoleRightData.ACTVTY_RL).ToString = "" And Not drActvtyRl(RoleRightData.ACTVTY_RL).ToString = String.Empty Then
                    Dim strItems() As String
                    strItems = drActvtyRl(RoleRightData.ACTVTY_RL).ToString.Split(CChar(","))
                    For i As Integer = 0 To strItems.Length - 1
                        If strItems(i).Substring(strItems(i).IndexOf("<") + 1, strItems(i).IndexOf(":", strItems(i).IndexOf("<")) - strItems(i).IndexOf("<") - 1) = "M" Then
                            Dim result As String = strItems(i).Substring(strItems(i).IndexOf(":") + 1, strItems(i).IndexOf(">", strItems(i).IndexOf(":")) - strItems(i).IndexOf(":") - 1)
                            Dim bln_KeyExist As Boolean
                            Dim objcommon As New CommonData
                            Dim strKeyValue As String = objcommon.GetConfigSetting(result, bln_KeyExist)
                            If bln_KeyExist Then
                                drActvtyRl(RoleRightData.ACTVTY_NAM) = strKeyValue
                            End If
                        End If
                    Next
                End If
            Next
        End If
        'If pvt_dsRoleRights.Tables(RoleRightData._ROLE_RIGHT).Rows.Count > 0 Then
        '    hdnPageMode.Value = "edit"
        'Else
        '    hdnPageMode.Value = "new"
        'End If
        dtParentProcess = pvt_dsRoleRights.Tables(RoleRightData._PROCESS_PARENT)
        dtRoleRights = pvt_dsRoleRights.Tables(RoleRightData._ROLE_RIGHT)

        If dtRoleRights.Rows.Count > 0 Then
            hdnMode.Value = "False"
        Else
            hdnMode.Value = "True"
        End If

        For Each drParentProcess As DataRow In dtParentProcess.Rows
            'If Not IsDBNull(drParentProcess(RoleRightData.PROCESSID)) Then
            drsProcess = dtProcess.Select(String.Concat(RoleRightData.PRNT_ID, "=", _
            drParentProcess(RoleRightData.PRCSS_ID)))
            'End If


            intTotalProcessCount = drsProcess.Length

            If intTotalProcessCount > 0 Then
                Dim strProcessIDs As String = String.Empty

                For Each drProcessdup As DataRow In drsProcess
                    If strProcessIDs <> Nothing Then
                        strProcessIDs = String.Concat(strProcessIDs, ",")
                    End If
                    strProcessIDs = String.Concat(strProcessIDs, drProcessdup.Item(RoleRightData.PRCSS_ID))
                Next

                'Checking all child activities are checked. 
                'If all child activities are checked then the corresponding parent row checkbox is checked
                drsAddRight = dtRoleRights.Select(String.Concat(RoleRightData.CRT_BT, "=1 and ", _
                                RoleRightData.PRCSS_ID, " in (", strProcessIDs, ")"))

                drsEditRight = dtRoleRights.Select(String.Concat(RoleRightData.EDT_BT, "=1 and ", _
                                  RoleRightData.PRCSS_ID, " in (", strProcessIDs, ")"))

                drsViewRight = dtRoleRights.Select(String.Concat(RoleRightData.VW_BT, "=1 and ", _
                                  RoleRightData.PRCSS_ID, " in (", strProcessIDs, ")"))

                drsCancelRight = dtRoleRights.Select(String.Concat(RoleRightData.CNCL_BT, "=1 and ", _
                                  RoleRightData.PRCSS_ID, " in (", strProcessIDs, ")"))
            Else
                'Checking all child activities are checked. 
                'If all child activities are checked then the corresponding parent row checkbox is checked
                drsAddRight = dtRoleRights.Select(String.Concat(RoleRightData.CRT_BT, "=1 and ", _
                                 RoleRightData.PRCSS_ID, "=", drParentProcess(RoleRightData.PRCSS_ID)))

                drsEditRight = dtRoleRights.Select(String.Concat(RoleRightData.EDT_BT, "=1 and ", _
                                 RoleRightData.PRCSS_ID, "=", drParentProcess(RoleRightData.PRCSS_ID)))

                drsViewRight = dtRoleRights.Select(String.Concat(RoleRightData.VW_BT, "=1 and ", _
                                 RoleRightData.PRCSS_ID, "=", drParentProcess(RoleRightData.PRCSS_ID)))

                drsCancelRight = dtRoleRights.Select(String.Concat(RoleRightData.CNCL_BT, "=1 and ", _
                                RoleRightData.PRCSS_ID, "=", drParentProcess(RoleRightData.PRCSS_ID)))
            End If

            'check dynamic checked items count for each process if its is equal to total no 
            'of activities the corresponding parent checkbox is checked
            If drsAddRight.Length > 0 Then
                ChkAdd = True
            Else
                ChkAdd = False
            End If

            If drsEditRight.Length > 0 Then
                ChkEdit = True
            Else
                ChkEdit = False
            End If

            If drsViewRight.Length > 0 Then
                ChkView = True
            Else
                ChkView = False
            End If

            If drsCancelRight.Length > 0 Then
                ChkCancel = True
            Else
                ChkCancel = False
            End If

            'set dynamic checked items count for each process not for activities
            If drsAddRight.Length > 0 Then
                intCheckedAddRightCount = drsAddRight.Length
            Else
                intCheckedAddRightCount = 0
            End If

            If drsEditRight.Length > 0 Then
                intCheckedEditRightCount = drsEditRight.Length
            Else
                intCheckedEditRightCount = 0
            End If

            If drsViewRight.Length > 0 Then
                intCheckedViewRight = drsViewRight.Length
            Else
                intCheckedViewRight = 0
            End If

            If drsCancelRight.Length > 0 Then
                intCheckedCancelRight = drsCancelRight.Length
            Else
                intCheckedCancelRight = 0
            End If

            '  strParentNodeKey = String.Concat("CPN", drParentProcess(RoleRightData.PRCSS_ID), "_PN", drParentProcess(RoleRightData.PRCSS_ID))
            strParentNodeKey = String.Concat("PN", drParentProcess(RoleRightData.PRCSS_ID))

            If drsProcess.Length > 0 Then

                drsActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                               drParentProcess(RoleRightData.PRCSS_ID)))

                For Each drstemp As DataRow In drsProcess
                    drsActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                                   drstemp(RoleRightData.PRCSS_ID)))
                Next


                If drsActivity.Length > 0 Then
                    If drsAddRight.Length > 0 Then
                        intCreateRight = True
                    Else
                        intCreateRight = False
                    End If

                    If drsEditRight.Length > 0 Then
                        intEditRight = True
                    Else
                        intEditRight = False
                    End If

                    If drsCancelRight.Length > 0 Then
                        intCancelRight = True
                    Else
                        intCancelRight = False
                    End If

                End If
                'intCreateRight = CInt(drsActivity(RoleRightData.CRT_RGHT_BT).ToString())
                ' intEditRight = CInt(drsActivity(RoleRightData.EDT_RGHT_BT).ToString)
                'Binding Parent Nodes(Process) 
                ITreeView1.AddNodeDiv(drParentProcess(RoleRightData.PRCSS_NAM), _
                                    UserControls_iTreeView.mrNodeType.mrParent, _
                                    strParentNodeKey, ChkAdd, ChkEdit, ChkView, ChkCancel, _
                                     "", intCheckedAddRightCount, intCheckedEditRightCount, _
                                    intCheckedViewRight, intCheckedCancelRight, intCreateRight, intEditRight, intCancelRight)


                'TODO
                'Applying filter to activity with process id
                drsActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                                drParentProcess(RoleRightData.PRCSS_ID)))
                If drsActivity.Length > 0 Then
                    Dim drsDisableActivity As DataRow()
                    drsDisableActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                              drParentProcess(RoleRightData.PRCSS_ID), " AND ", RoleRightData.CRT_RGHT_BT, "=0"))

                    If (drsDisableActivity.Length = drsActivity.Length) Then
                        intCreateRight = 0
                    Else
                        intCreateRight = 1

                    End If

                    drsDisableActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                              drParentProcess(RoleRightData.PRCSS_ID), " AND ", RoleRightData.EDT_RGHT_BT, "=0"))

                    If (drsDisableActivity.Length = drsActivity.Length) Then
                        intEditRight = 0
                    Else
                        intEditRight = 1

                    End If

                    drsDisableActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                           drParentProcess(RoleRightData.PRCSS_ID), " AND ", RoleRightData.CNCL_RGHT_BT, "=0"))

                    If (drsDisableActivity.Length = drsActivity.Length) Then
                        intCancelRight = 0
                    Else
                        intCancelRight = 1

                    End If

                    For Each drActivity As DataRow In drsActivity
                        drsRoleRights = dtRoleRights.Select(String.Concat(RoleRightData.ACTVTY_ID, "=", _
                                            drActivity(RoleRightData.ACTVTY_ID)))

                        If drsRoleRights.Length > 0 Then
                            ChkAdd = CBool(drsRoleRights(0)(RoleRightData.CRT_BT))
                            ChkEdit = CBool(drsRoleRights(0)(RoleRightData.EDT_BT))
                            ChkView = CBool(drsRoleRights(0)(RoleRightData.VW_BT))
                            ChkCancel = CBool(drsRoleRights(0)(RoleRightData.CNCL_BT))
                        Else
                            ChkAdd = False
                            ChkEdit = False
                            ChkView = False
                            ChkCancel = False
                        End If

                        strChildNodeKey = String.Concat("CN", drActivity(RoleRightData.ACTVTY_ID), "_PN", drParentProcess(RoleRightData.PRCSS_ID))
                        If drActivity.Item(RoleRightData.CRT_RGHT_BT) Then
                            intCreateRight = 1
                        Else
                            intCreateRight = 0
                        End If
                        If drActivity.Item(RoleRightData.EDT_RGHT_BT) Then
                            intEditRight = 1
                        Else
                            intEditRight = 0
                        End If

                        If drActivity.Item(RoleRightData.CNCL_RGHT_BT) Then
                            intCancelRight = 1
                        Else
                            intCancelRight = 0
                        End If

                        'Binding Child Nodes(Activity) 
                        ITreeView1.AddNodeDiv(drActivity(RoleRightData.ACTVTY_NAM), _
                                    UserControls_iTreeView.mrNodeType.mrChild, _
                                    strChildNodeKey, ChkAdd, ChkEdit, ChkView, ChkCancel, _
                                    strParentNodeKey, intCheckedAddRightCount = 0, intCheckedEditRightCount = 0, _
                                    intCheckedViewRight = 0, intCheckedCancelRight = 0, intCreateRight, intEditRight, intCancelRight)
                    Next
                End If


                For Each drProcess As DataRow In dtProcess.Select(String.Concat(RoleRightData.PRNT_ID, "=", _
                                                        drParentProcess(RoleRightData.PRCSS_ID), " AND ", _
                                                         "PRNT_ID IS Not NULL"), "")
                    '   RoleRightData.PRNT_ID.ToString, "<>''"), "")

                    'Applying filter to activity with process id
                    drsActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                                    drProcess(RoleRightData.PRCSS_ID)))

                    intTotalActivityCount = drsActivity.Length

                    'Checking all child activities are checked. 
                    'If all child activities are checked then the corresponding parent row checkbox is checked
                    drsAddRight = dtRoleRights.Select(String.Concat(RoleRightData.CRT_BT, "=1 and ", _
                                    RoleRightData.PRCSS_ID, "=", drProcess(RoleRightData.PRCSS_ID)))

                    drsEditRight = dtRoleRights.Select(String.Concat(RoleRightData.EDT_BT, "=1 and ", _
                                    RoleRightData.PRCSS_ID, "=", drProcess(RoleRightData.PRCSS_ID)))

                    drsViewRight = dtRoleRights.Select(String.Concat(RoleRightData.VW_BT, "=1 and ", _
                                    RoleRightData.PRCSS_ID, "=", drProcess(RoleRightData.PRCSS_ID)))

                    drsCancelRight = dtRoleRights.Select(String.Concat(RoleRightData.CNCL_BT, "=1 and ", _
                                 RoleRightData.PRCSS_ID, "=", drProcess(RoleRightData.PRCSS_ID)))


                    'check dynamic checked items count for each process if its is equal to total no 
                    'of activities the corresponding parent checkbox is checked
                    If drsAddRight.Length > 0 Then
                        ChkAdd = True
                    Else
                        ChkAdd = False
                    End If

                    If drsEditRight.Length > 0 Then
                        ChkEdit = True
                    Else
                        ChkEdit = False
                    End If

                    If drsViewRight.Length > 0 Then
                        ChkView = True
                    Else
                        ChkView = False
                    End If

                    If drsCancelRight.Length > 0 Then
                        ChkCancel = True
                    Else
                        ChkCancel = False
                    End If


                    'set dynamic checked items count for each process not for activities
                    If drsAddRight.Length > 0 Then
                        intCheckedAddRightCount = drsAddRight.Length
                    Else
                        intCheckedAddRightCount = 0
                    End If

                    If drsEditRight.Length > 0 Then
                        intCheckedEditRightCount = drsEditRight.Length
                    Else
                        intCheckedEditRightCount = 0
                    End If

                    If drsViewRight.Length > 0 Then
                        intCheckedViewRight = drsViewRight.Length
                    Else
                        intCheckedViewRight = 0
                    End If

                    If drsCancelRight.Length > 0 Then
                        intCheckedCancelRight = drsCancelRight.Length
                    Else
                        intCheckedCancelRight = 0
                    End If

                    If drsActivity.Length > 0 Then

                        strParentNodeKey = String.Concat("CPN", drProcess(RoleRightData.PRCSS_ID), "_PN", _
                       drProcess(RoleRightData.PRNT_ID))


                        'Binding Sub-Parent Nodes(Process) 
                        ITreeView1.AddNodeDiv(drProcess(RoleRightData.PRCSS_NAM), _
                                            UserControls_iTreeView.mrNodeType.mrParent, _
                                            strParentNodeKey, ChkAdd, ChkEdit, ChkView, ChkCancel, _
                                            "", intCheckedAddRightCount, intCheckedEditRightCount, _
                                            intCheckedViewRight, intCheckedCancelRight, intCreateRight, intEditRight, intCancelRight)

                        For Each drActivity As DataRow In drsActivity
                            drsRoleRights = dtRoleRights.Select(String.Concat(RoleRightData.ACTVTY_ID, "=", _
                                                drActivity(RoleRightData.ACTVTY_ID)))

                            If drsRoleRights.Length > 0 Then
                                ChkAdd = CBool(drsRoleRights(0)(RoleRightData.CRT_BT))
                                ChkEdit = CBool(drsRoleRights(0)(RoleRightData.EDT_BT))
                                ChkView = CBool(drsRoleRights(0)(RoleRightData.VW_BT))
                                ChkCancel = CBool(drsRoleRights(0)(RoleRightData.CNCL_BT))
                            Else
                                ChkAdd = False
                                ChkEdit = False
                                ChkView = False
                                ChkCancel = False
                            End If

                            strChildNodeKey = String.Concat("CN", drActivity(RoleRightData.ACTVTY_ID), "_CPN", _
                            drProcess(RoleRightData.PRCSS_ID), "_PN", drProcess(RoleRightData.PRNT_ID))
                            strParentNodeKey = String.Concat("CPN", drProcess(RoleRightData.PRCSS_ID), "_PN", _
                             drProcess(RoleRightData.PRNT_ID))
                            If drActivity.Item(RoleRightData.CRT_RGHT_BT) Then
                                intCreateRight = 1
                            Else
                                intCreateRight = 0
                            End If
                            If drActivity.Item(RoleRightData.EDT_RGHT_BT) Then
                                intEditRight = 1
                            Else
                                intEditRight = 0
                            End If

                            If drActivity.Item(RoleRightData.CNCL_RGHT_BT) Then
                                intCancelRight = 1
                            Else
                                intCancelRight = 0
                            End If

                            'Binding Child Nodes(Activity) 
                            ITreeView1.AddNodeDiv(drActivity(RoleRightData.ACTVTY_NAM), _
                                        UserControls_iTreeView.mrNodeType.mrChild, _
                                        strChildNodeKey, ChkAdd, ChkEdit, ChkView, ChkCancel, _
                                         strParentNodeKey, intCheckedAddRightCount = 0, intCheckedEditRightCount = 0, _
                                        intCheckedViewRight = 0, intCheckedCancelRight = 0, intCreateRight, intEditRight, intCancelRight)
                        Next

                    End If
                Next

            Else
                'Applying filter to activity with process id
                drsActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                                drParentProcess(RoleRightData.PRCSS_ID)))
                If drsActivity.Length > 0 Then
                    Dim drsDisableActivity As DataRow()
                    drsDisableActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                              drParentProcess(RoleRightData.PRCSS_ID), " AND ", RoleRightData.CRT_RGHT_BT, "=0"))

                    If (drsDisableActivity.Length = drsActivity.Length) Then
                        intCreateRight = 0
                    Else
                        intCreateRight = 1

                    End If

                    drsDisableActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                              drParentProcess(RoleRightData.PRCSS_ID), " AND ", RoleRightData.EDT_RGHT_BT, "=0"))

                    If (drsDisableActivity.Length = drsActivity.Length) Then
                        intEditRight = 0
                    Else
                        intEditRight = 1

                    End If

                    drsDisableActivity = dtActivity.Select(String.Concat(RoleRightData.PRCSS_ID, "=", _
                             drParentProcess(RoleRightData.PRCSS_ID), " AND ", RoleRightData.CNCL_RGHT_BT, "=0"))

                    If (drsDisableActivity.Length = drsActivity.Length) Then
                        intCancelRight = 0
                    Else
                        intCancelRight = 1

                    End If


                    'Binding Parent Nodes(Process) 
                    ITreeView1.AddNodeDiv(drParentProcess(RoleRightData.PRCSS_NAM), _
                                        UserControls_iTreeView.mrNodeType.mrParent, _
                                        strParentNodeKey, ChkAdd, ChkEdit, ChkView, ChkCancel, _
                                        "", intCheckedAddRightCount, intCheckedEditRightCount, _
                                        intCheckedViewRight, intCheckedCancelRight, intCreateRight, intEditRight, intCancelRight)

                    For Each drActivity As DataRow In drsActivity
                        drsRoleRights = dtRoleRights.Select(String.Concat(RoleRightData.ACTVTY_ID, "=", _
                                            drActivity(RoleRightData.ACTVTY_ID)))

                        If drsRoleRights.Length > 0 Then
                            ChkAdd = CBool(drsRoleRights(0)(RoleRightData.CRT_BT))
                            ChkEdit = CBool(drsRoleRights(0)(RoleRightData.EDT_BT))
                            ChkView = CBool(drsRoleRights(0)(RoleRightData.VW_BT))
                            ChkCancel = CBool(drsRoleRights(0)(RoleRightData.CNCL_BT))

                        Else
                            ChkAdd = False
                            ChkEdit = False
                            ChkView = False
                            ChkCancel = False
                        End If

                        strChildNodeKey = String.Concat("CN", drActivity(RoleRightData.ACTVTY_ID), "_CPN", _
                                          drParentProcess(RoleRightData.PRCSS_ID), "_PN", drParentProcess(RoleRightData.PRCSS_ID))
                        'strParentNodeKey = String.Concat("CPN", drParentProcess(RoleRightData.PRCSS_ID), "_PN", drParentProcess(RoleRightData.PRCSS_ID))
                        strParentNodeKey = String.Concat("PN", drParentProcess(RoleRightData.PRCSS_ID))
                        If drActivity.Item(RoleRightData.CRT_RGHT_BT) Then
                            intCreateRight = 1
                        Else
                            intCreateRight = 0
                        End If
                        If drActivity.Item(RoleRightData.EDT_RGHT_BT) Then
                            intEditRight = 1
                        Else
                            intEditRight = 0
                        End If

                        If drActivity.Item(RoleRightData.CNCL_RGHT_BT) Then
                            intCancelRight = 1
                        Else
                            intCancelRight = 0
                        End If

                        'Binding Child Nodes(Activity) 
                        ITreeView1.AddNodeDiv(drActivity(RoleRightData.ACTVTY_NAM), _
                                    UserControls_iTreeView.mrNodeType.mrChild, _
                                    strChildNodeKey, ChkAdd, ChkEdit, ChkView, ChkCancel, _
                                    strParentNodeKey, intCheckedAddRightCount = 0, intCheckedEditRightCount = 0, _
                                    intCheckedViewRight = 0, intCheckedCancelRight = 0, intCreateRight, intEditRight, intCancelRight)
                    Next
                End If
            End If
        Next

        If bv_blnHasChanges Then
            pvt_SaveMenu(pvt_dsRoleRights)
        End If
    End Sub
#End Region

#Region "CLASS FOR BUILDING COLLECTION"
    ''' <summary>
    ''' This class is used to initialize the controls which is appearing in role right view control.
    ''' </summary>
    ''' <remarks></remarks>
    Private Class EVDCollection
        Public Id As String
        Public Add As Boolean
        Public Edit As Boolean
        Public View As Boolean
        Public Cancel As Boolean
        Public isUpdated As Boolean = False
        Public intCountAddopt As Integer
        Public intCountEditopt As Integer
        Public intCountViewopt As Integer
        Public intCountCancelopt As Integer
    End Class

#End Region

#Region "PROCESSING KEY"
    ''' <summary>
    ''' Methods for Processing Keys
    ''' </summary>
    ''' <param name="bv_strKeys"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function pvt_ProcessKeys(ByVal bv_strKeys() As String) As Boolean
        Try
            Dim strKey As String
            Dim objCollection As New EVDCollection
            For Each strKey In bv_strKeys
                If pvt_CheckElements(strKey) = True Then
                    Dim objCollectionTemp As EVDCollection
                    objCollectionTemp = pvt_CheckCollection(strKey)
                    Dim strTemp() As String
                    strTemp = pvt_GetIDVal(strKey).Split(CChar("_"))
                    If Not objCollectionTemp Is Nothing Then
                        If strTemp(strTemp.Length - 1) = "a" Then
                            objCollectionTemp.Add = True
                        End If
                        If strTemp(strTemp.Length - 1) = "e" Then
                            objCollectionTemp.Edit = True
                        End If
                        If strTemp(strTemp.Length - 1) = "v" Then
                            objCollectionTemp.View = True
                        End If

                        If strTemp(strTemp.Length - 1) = "c" Then
                            objCollectionTemp.Cancel = True
                        End If

                    Else
                        Dim objNewCollection As New EVDCollection
                        objNewCollection.Id = CStr(strTemp(0))
                        If strTemp(strTemp.Length - 1) = "a" Then
                            objNewCollection.Add = True
                        End If
                        If strTemp(strTemp.Length - 1) = "e" Then
                            objNewCollection.Edit = True
                        End If
                        If strTemp(strTemp.Length - 1) = "v" Then
                            objNewCollection.View = True
                        End If

                        If strTemp(strTemp.Length - 1) = "c" Then
                            objNewCollection.Cancel = True
                        End If

                        clnRoleRights.Add(objNewCollection)
                    End If
                End If
            Next
            pvt_ProcessKeys = True
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                               MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function

#Region "CHECK ELEMENTS"
    ''' <summary>
    ''' Checking Elements Checked or not
    ''' </summary>
    ''' <param name="bv_strID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function pvt_CheckElements(ByVal bv_strID As String) As Boolean
        Try
            If (bv_strID.IndexOf("_id") <> -1 And (bv_strID.IndexOf("_a") > 0 Or _
                    bv_strID.IndexOf("_e") > 0 Or bv_strID.IndexOf("_v") > 0 Or bv_strID.IndexOf("_c") > 0)) Then
                pvt_CheckElements = True
            Else
                pvt_CheckElements = False
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                               MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "CHECK COLLECTION"
    ''' <summary>
    ''' Checking the collection the control is already present are not
    ''' </summary>
    ''' <param name="bv_strID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function pvt_CheckCollection(ByVal bv_strID As String) As EVDCollection
        Try
            Dim objCollTemp As EVDCollection
            Dim strTemp() As String
            strTemp = pvt_GetIDVal(bv_strID).Split(CChar("_"))
            For Each objCollTemp In clnRoleRights
                If objCollTemp.Id = CStr(strTemp(0)) Then
                    pvt_CheckCollection = objCollTemp
                    Exit For
                End If
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                               MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GETTING CLIENT ID"

    ''' <summary>
    ''' Getting client control Id
    ''' </summary>
    ''' <param name="bv_strID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function pvt_GetIDVal(ByVal bv_strID As String) As String
        Try
            bv_strID = bv_strID.Trim
            bv_strID = bv_strID.Replace("_id", "")
            pvt_GetIDVal = bv_strID
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                               MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#End Region

#Region "pvt_SaveMenu"
    Private Sub pvt_SaveMenu(ByVal bv_dsActivityData As RoleRightDataSet)
        Try
            Dim strMenuSource As String
            Dim dsActivityData As New RoleRightDataSet
            dsActivityData = bv_dsActivityData.Copy()
            Dim strProcessFilter As String = String.Concat(RoleRightData.PRCSS_ID, "='", DASHBOARD_PROCESS_ID, "'")
            Dim dtActivity As DataTable
            dtActivity = bv_dsActivityData.Tables(RoleRightData._ACTIVITY)
            Dim dvActivity As DataView
            Dim dvProcessParent As DataView
            'Remove Dashboard Activities from the Activity table as to prevent them from generating the menu
            dvActivity = dtActivity.DefaultView
            If dtActivity.Select(strProcessFilter).Length > 0 Then
                dvActivity.RowFilter = String.Concat(RoleRightData.ACTVTY_ID, " NOT IN(", CommonWeb.ColToCSVstring(dtActivity.Select(strProcessFilter).CopyToDataTable(), RoleRightData.ACTVTY_ID), ")")
            End If
            dvProcessParent = bv_dsActivityData.Tables(RoleRightData._PARENTPROCESS_ROLE).DefaultView
            dvProcessParent.RowFilter = String.Concat(RoleRightData.PRCSS_ID, " NOT IN ('", DASHBOARD_PROCESS_ID, "')")

            dsActivityData.Tables(RoleRightData._ACTIVITY).Clear()
            dsActivityData.Tables(RoleRightData._PARENTPROCESS_ROLE).Clear()
            dsActivityData.Tables(RoleRightData._ACTIVITY).Merge(dvActivity.ToTable)
            dsActivityData.Tables(RoleRightData._PARENTPROCESS_ROLE).Merge(dvProcessParent.ToTable)

            strMenuSource = iMenuControl.pub_CreateMenuFile(dsActivityData, False)
            iMenuControl.pub_SaveMenuFile(strMenuSource, Server.MapPath("../Script/Menu/"), txtRoleCode.Text)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                               MethodBase.GetCurrentMethod.Name, ex)

        End Try
    End Sub
#End Region

#Region "pvt_ValidateRoleCode"

    Private Sub pvt_ValidateRoleCode(ByVal bv_strRoleCode As String, ByVal bv_intRoleID As Integer)
        Dim objRoleRight As New RoleRight
        Dim blnValid As Boolean
        blnValid = objRoleRight.pub_GetRoleCodeByRoleId(bv_strRoleCode, bv_intRoleID)
        If blnValid = True Then
            pub_SetCallbackReturnValue("pkValid", "true")
        Else
            pub_SetCallbackReturnValue("pkValid", "false")
        End If
        pub_SetCallbackStatus(True)
    End Sub

#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Controls/iTreeFunctions.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Admin/RoleRight.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
