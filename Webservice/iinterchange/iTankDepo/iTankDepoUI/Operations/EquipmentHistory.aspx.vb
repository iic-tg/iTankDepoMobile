Partial Class Operations_EquipmentHistory
    Inherits Pagebase

#Region "Declarations"
    Dim dsEquipmentHistory As New EquipmentHistoryDataSet
    Private Const EQUIPMENT_HISTORY As String = "EQUIPMENT_HISTORY"
    Private strMSGUPDATE As String = "Record Updated Successfully."
    Dim objCommon As New CommonUI
    Dim objTracking As New Tracking
    Dim str_062GWS As String
    Dim bln_062GWSActive_Key As Boolean
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ifgEquipmentHistory.ShowFooter = True
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region


#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "ValidateContainer"
                pvt_ValidateContainer(e.GetCallbackValue("bv_strCstmr_ID"), e.GetCallbackValue("Container"))
            Case "EquipmentDeleteActivity"
                pvt_EquipmentDeleteActivity(e.GetCallbackValue("TrackingID"), _
                                            e.GetCallbackValue("ActivityName"), _
                                            e.GetCallbackValue("AuditRemarks"))
            Case "Validate_Histroy_Delete"

                Validate_Histroy_Delete(e.GetCallbackValue("EquipmentNo"), e.GetCallbackValue("GI_TRNSCTN_NO"))
        End Select
    End Sub

#End Region

    Public Sub Validate_Histroy_Delete(ByVal bv_strEquipmentNO As String, ByVal bv_strGI_Trnsctn_NO As String)

        Try

            Dim objEqUpdates As New EquipmentUpdate
            Dim strTrackingId As String
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer = CInt(objCommondata.GetDepotID())


            strTrackingId = objEqUpdates.Validate_Histroy_Delete(bv_strEquipmentNO, bv_strGI_Trnsctn_NO, intDepotID)

            If (Not String.IsNullOrEmpty(strTrackingId) AndAlso Not String.IsNullOrWhiteSpace(strTrackingId)) Then

                pub_SetCallbackStatus(True)
                pub_SetCallbackReturnValue("TrackingId", strTrackingId)
            Else
                pub_SetCallbackStatus(False)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub

#Region "pvt_EquipmentDeleteActivity"

    Public Sub pvt_EquipmentDeleteActivity(ByVal bv_strTrackingID As String, _
                                           ByVal bv_strActivityName As String, _
                                           ByVal bv_strRemarks As String)
        Try
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer = CInt(objCommondata.GetDepotID())
            Dim objEquipmentHistory As New EquipmentHistory
            Dim strCode As String = String.Empty
            dsEquipmentHistory = CType(RetrieveData(EQUIPMENT_HISTORY), EquipmentHistoryDataSet)

            If Trim(bv_strRemarks).Length > 0 Then
                objEquipmentHistory.pub_DeleteEquipmentActivity(dsEquipmentHistory, bv_strTrackingID, bv_strActivityName, bv_strRemarks, _
                                                                objCommondata.GetCurrentUserName(), CDate(objCommondata.GetCurrentDate()), _
                                                                intDepotID)

                If dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows.Count > 0 Then
                    If Not IsDBNull(dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0).Item(EquipmentHistoryData.EQPMNT_STTS_CD)) Then
                        strCode = String.Concat("Activity : ", dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0).Item(EquipmentHistoryData.EQPMNT_STTS_CD))
                    Else
                        strCode = String.Concat("Activity Name : ", dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0).Item(EquipmentHistoryData.ACTVTY_NAM))
                    End If
                    Dim strMsg As String = String.Concat(strCode, " has been deleted from Equipment History")
                    pub_SetCallbackStatus(True)
                    pub_SetCallbackReturnValue("Message", strMsg)
                End If
            Else
                pub_SetCallbackStatus(True)
                pub_SetCallbackReturnValue("validRemarks", "true")
            End If

            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentHistory_ClientBind"

    Protected Sub ifgEquipmentHistory_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentHistory.ClientBind
        Try
            Dim objCommonConfig As New ConfigSetting()
            Dim objcommon As New CommonData()
            If e.Parameters("Mode") = "Search" Then
                Dim strEqType As String = String.Empty
                Dim strEqCode As String = String.Empty
                dsEquipmentHistory.Tables.Clear()
                Dim objcommonData As New CommonData
                Dim objEquipmentHistory As New EquipmentHistory
                Dim intDepotID As Integer = CInt(objcommonData.GetDepotID())
                dsEquipmentHistory = objEquipmentHistory.pub_GetEquipmentHistory(e.Parameters("EquipmentNo").ToString(), intDepotID)

                If dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows.Count > 0 Then
                    With dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0)
                        If Not IsDBNull(.Item(EquipmentHistoryData.EQPMNT_TYP_CD)) Then
                            strEqType = CStr(.Item(EquipmentHistoryData.EQPMNT_TYP_CD))
                        Else
                            strEqType = "NA"
                        End If
                        If Not IsDBNull(.Item(EquipmentHistoryData.EQPMNT_CD_CD)) Then
                            strEqCode = CStr(.Item(EquipmentHistoryData.EQPMNT_CD_CD))
                        Else
                            strEqCode = "NA"
                        End If
                        e.OutputParameters.Add("Type", strEqType)
                        e.OutputParameters.Add("Code", strEqCode)
                    End With
                    'GES
                    str_062GWS = objCommonConfig.pub_GetConfigSingleValue("062", intDepotID)
                    bln_062GWSActive_Key = objCommonConfig.IsKeyExists
                    If str_062GWS.ToString = "True" Then
                        objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 1, "False")
                        objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 3, "False")
                    Else
                        objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 2, "False")
                        objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 7, "False")

                    End If

                    e.DataSource = dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY)
                Else
                    e.OutputParameters.Add("norecordsfound", "true")
                End If
            ElseIf e.Parameters("Mode") = "Reset" Then
                dsEquipmentHistory.Tables.Clear()
            End If
            CacheData(EQUIPMENT_HISTORY, dsEquipmentHistory)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "ifgEquipmentHistory_RowDataBound"
    Protected Sub ifgEquipmentHistory_RowDataBound(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentHistory.RowDataBound
        Try
            Dim objCommonConfig As New ConfigSetting()
            Dim objcommon As New CommonData()
            Dim objcommonData As New CommonData
            Dim intDepotID As Integer = CInt(objcommonData.GetDepotID())
            bln_062GWSActive_Key = objCommonConfig.IsKeyExists
            str_062GWS = objCommonConfig.pub_GetConfigSingleValue("062", intDepotID)
            If e.Row.RowType = DataControlRowType.DataRow Then
                dsEquipmentHistory = CType(RetrieveData(EQUIPMENT_HISTORY), EquipmentHistoryDataSet)
                Dim dtTracking As DataTable = dsEquipmentHistory.Tables(EquipmentHistoryData._V_TRACKING)
                Dim drv As System.Data.DataRowView
                drv = CType(e.Row.DataItem, Data.DataRowView)

                Dim imgLink As Image
                'imgLink = CType(e.Row.Cells(12).Controls(0), Image)
                imgLink = CType(e.Row.Cells(14).Controls(0), Image)
                imgLink.ToolTip = "Delete"

                Dim count As Integer
                Dim dt As DataTable
                dt = ifgEquipmentHistory.DataSource
                count = dt.Rows.Count
                If e.Row.RowIndex = 0 Then
                    'Billing Flag B means Delete is Disable 
                    If dt.Rows(0).Item(EquipmentHistoryData.INVC_GNRTN).ToString = "Y" Then
                        'e.Row.Cells(12).Enabled = False
                        e.Row.Cells(14).Enabled = False
                        imgLink.ImageUrl = "../Images/trash_ash.png"
                        imgLink.Attributes.Add("onclick", "")
                        imgLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                    Else
                        ' e.Row.Cells(12).Enabled = True
                        e.Row.Cells(14).Enabled = True
                        imgLink.Visible = True
                        imgLink.ImageUrl = "../Images/trash.png"
                        'imgLink.Attributes.Add("onclick", String.Concat("fnAuditRemarksEntry('", drv.Item(EquipmentHistoryData.TRCKNG_ID), "','", drv.Item(EquipmentHistoryData.ACTVTY_NAM), "','", drv.Item(EquipmentHistoryData.EQPMNT_STTS_CD), "'); return false;"))
                        imgLink.Attributes.Add("onclick", String.Concat("fnAuditRemarksEntry('", drv.Item(EquipmentHistoryData.TRCKNG_ID), "','", drv.Item(EquipmentHistoryData.ACTVTY_NAM), "','", drv.Item(EquipmentHistoryData.EQPMNT_NO), "','", drv.Item(EquipmentHistoryData.GI_TRNSCTN_NO), "','", drv.Item(EquipmentHistoryData.EQPMNT_STTS_CD), "'); return false;"))
                        imgLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                    End If
                Else
                    'e.Row.Cells(12).Enabled = False
                    e.Row.Cells(14).Enabled = False
                    imgLink.ImageUrl = "../Images/trash_ash.png"
                    imgLink.Attributes.Add("onclick", "")
                    imgLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                End If


                If str_062GWS.ToString = "True" Then
                    'objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 1, "False")
                    'objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 3, "False")
                    e.Row.Cells(1).Style.Add("display", "none")
                    e.Row.Cells(3).Style.Add("display", "none")
                Else
                    'objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 7, "False")
                    'objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 2, "False")
                    e.Row.Cells(7).Style.Add("display", "none")
                    e.Row.Cells(2).Style.Add("display", "none")
                End If
            End If
            If e.Row.RowType = DataControlRowType.Header Then
                If str_062GWS.ToString = "True" Then
                    'objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 1, "False")
                    'objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 3, "False")
                    e.Row.Cells(1).Style.Add("display", "none")
                    e.Row.Cells(3).Style.Add("display", "none")
                Else
                    'objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 7, "False")
                    'objcommon.SetGridVisibilitybyIndex(ifgEquipmentHistory, "hide", 2, "False")
                    e.Row.Cells(7).Style.Add("display", "none")
                    e.Row.Cells(2).Style.Add("display", "none")
                End If
            End If
            
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateContainer"

    Public Sub pvt_ValidateContainer(ByVal bv_strCustomerId As String, ByVal bv_strContainer As String)
        Dim objcommonData As New CommonData
        Dim intDepotID As Integer = CInt(objcommonData.GetDepotID())
        Try
            If dsEquipmentHistory.Tables(EquipmentHistoryData._V_TRACKING).Rows.Count = 0 Then
                pub_SetCallbackReturnValue("valid", "false")
            Else
                pub_SetCallbackReturnValue("valid", "true")
            End If

            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/EquipmentHistory.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class