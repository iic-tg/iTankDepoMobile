Option Strict On

Partial Class Admin_Settings
    Inherits Framebase
    Dim dsSettings As New ConfigDataSet
    Dim dtSettings As DataTable
    Private Const CONFIG_SETTINGS As String = "CONFIG_SETTINGS"
    Private strMSGINSERT As String = "Record Updated Successfully."

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Admin/Settings.js", MyBase.Page)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            pvt_SetChangesMade()
            'Dim objCommon As New CommonData
            'If objCommon.GetMultiLocationSupportConfig().ToLower = "false" Then
            '    navSettings.Visible = False
            'End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "CreateConfigurationTemplate"
                    pvt_CreateSettings(CInt(e.GetCallbackValue("Depot")), e.GetCallbackValue("wfData"))

            End Select

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateSettings"
    Private Sub pvt_CreateSettings(ByVal bv_DepotId As Integer, ByVal bv_wfData As String)
        Try
            Dim objSettings As New CommonUI
            dsSettings = CType(RetrieveData(CONFIG_SETTINGS), ConfigDataSet)
            For Each drSettings As DataRow In dsSettings.Tables(CommonUIData._CONFIG).Select("", "", DataViewRowState.ModifiedCurrent)
                drSettings.Item(CommonUIData.DPT_ID) = bv_DepotId
                If Not IsDBNull(drSettings.Item(CommonUIData.KY_VL)) Then
                    drSettings.Item(CommonUIData.KY_VL) = CommonWeb.EncryptData(drSettings.Item(CommonUIData.KY_VL).ToString)
                Else
                    drSettings.Item(CommonUIData.KY_VL) = String.Empty
                End If
            Next
            objSettings.pub_UpdateConfig(dsSettings)
            For Each drSettings As DataRow In dsSettings.Tables(CommonUIData._CONFIG).Select("", "", DataViewRowState.ModifiedCurrent)
                If Not IsDBNull(drSettings.Item(CommonUIData.KY_VL)) Then
                    If drSettings.Item(CommonUIData.KY_VL).ToString <> "" Then
                        drSettings.Item(CommonUIData.KY_VL) = CommonWeb.DecryptString(drSettings.Item(CommonUIData.KY_VL).ToString)
                    End If
                End If
            Next
            dsSettings.AcceptChanges()
            CacheData(CONFIG_SETTINGS, dsSettings)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", strMSGINSERT)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgConfigurationTemplate_ClientBind"
    Protected Sub ifgConfigurationTemplate_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgConfigurationTemplate.ClientBind
        Try
            Dim dvSettings As New DataView
            Dim dtSettings As DataTable
            If Not e.Parameters("DepotID") Is Nothing Then
                Dim objSettings As New CommonUI
                Dim objcommon As New CommonData
                Dim strSelectOrderBy As String = String.Concat(CommonUIData.CNFG_TMPLT_ID, " ASC")

                Dim strFilter As String = String.Concat(CommonUIData.DPT_ID, "='", e.Parameters("DepotID").ToString, "'")
                dtSettings = objSettings.pub_GetConfigTemplate(CInt(e.Parameters("DepotID"))).Tables(CommonUIData._CONFIG)
                For Each drSettings As DataRow In dtSettings.Select(String.Empty, strSelectOrderBy)
                    Dim drArr As DataRow() = dsSettings.Tables(CommonUIData._CONFIG).Select(CommonUIData.CNFG_TMPLT_ID & "='" & drSettings.Item(CommonUIData.CNFG_TMPLT_ID).ToString & "'")
                    If Not drArr.Length > 0 Then
                        Dim drNew As DataRow = dsSettings.Tables(CommonUIData._CONFIG).NewRow()
                        drNew.Item("TMP_CNFG_ID") = CommonWeb.GetNextIndex(dsSettings.Tables(CommonUIData._CONFIG), "TMP_CNFG_ID")
                        drNew.Item(CommonUIData.CNFG_ID) = drSettings.Item(CommonUIData.CNFG_ID)
                        drNew.Item(CommonUIData.CNFG_TMPLT_ID) = drSettings.Item(CommonUIData.CNFG_TMPLT_ID)
                        drNew.Item(CommonUIData.KY_NAM) = drSettings.Item(CommonUIData.KY_NAM)
                        drNew.Item(CommonUIData.KY_DSCRPTION) = drSettings.Item(CommonUIData.KY_DSCRPTION)
                        drNew.Item(CommonUIData.CNFG_TYP) = drSettings.Item(CommonUIData.CNFG_TYP)
                        drNew.Item(CommonUIData.ENBLD_BT) = drSettings.Item(CommonUIData.ENBLD_BT)
                        drNew.Item(CommonUIData.ACTV_BT) = drSettings.Item(CommonUIData.ACTV_BT)
                        drNew.Item(CommonUIData.DPT_ID) = drSettings.Item(CommonUIData.DPT_ID)
                        If Not IsDBNull(drSettings.Item(CommonUIData.KY_VL)) Then
                            If drSettings.Item(CommonUIData.KY_VL).ToString <> "" Then
                                drNew.Item(CommonUIData.KY_VL) = CommonWeb.DecryptString(drSettings.Item(CommonUIData.KY_VL).ToString)
                            End If
                        End If
                        dsSettings.Tables(CommonUIData._CONFIG).Rows.Add(drNew)
                    End If
                Next
                dsSettings.AcceptChanges()
            End If
            If dsSettings.Tables(CommonUIData._CONFIG).Rows.Count = 0 Then
                e.OutputParameters.Add("norecordsfound", "True")
            Else
                e.OutputParameters.Add("norecordsfound", "False")
            End If
            e.DataSource = dsSettings.Tables(CommonUIData._CONFIG)
            CacheData(CONFIG_SETTINGS, dsSettings)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"

        Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(lkpDepotCode)
        pub_SetGridChanges(ifgConfigurationTemplate, "ITab1_0")
        End Sub

#End Region

End Class
