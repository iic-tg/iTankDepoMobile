Option Strict On
Partial Class Widgets_EquipmentTracking
    Inherits Framebase
    Private Const EQUIPMENT_TRACKING As String = "EQUIPMENT_TRACKING"
    

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Widgets/EquipmentTracking.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTracking_ClientBind"
    Protected Sub ifgTracking_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgTracking.ClientBind
        Try
            Dim objTracking As New EquipmentTracking
            Dim dsEquipmentTracking As New EquipmentTrackingDataSet
            Dim objCommon As New CommonData()
            Dim strFilterType As String = e.Parameters("Type").ToString
            Dim strFilterValue As String = e.Parameters("Value").ToString
            Dim dvEquipmentTracking As New DataView
            If strFilterType <> "" AndAlso strFilterValue <> "" Then
                dsEquipmentTracking = CType(RetrieveData(EQUIPMENT_TRACKING), EquipmentTrackingDataSet)
                If Not dsEquipmentTracking Is Nothing Then
                    dvEquipmentTracking = dsEquipmentTracking.Tables(EquipmentTrackingData._V_EQUIPMENT_TRACKING).DefaultView
                    dvEquipmentTracking.RowFilter = Nothing
                End If
                If Not dvEquipmentTracking.Count > 0 Then
                    dsEquipmentTracking = New EquipmentTrackingDataSet
                    dsEquipmentTracking = objTracking.pub_GetVEquipmentTrackingByDepotID(CInt(objCommon.GetDepotID()))
                    dvEquipmentTracking = dsEquipmentTracking.Tables(EquipmentTrackingData._V_EQUIPMENT_TRACKING).DefaultView
                End If
                dvEquipmentTracking.RowFilter = String.Concat(strFilterType, " = '", strFilterValue, "'")
            End If
            If dvEquipmentTracking.Count = 0 Then
                e.OutputParameters.Add("norecordsfound", True)
            Else
                e.OutputParameters.Add("norecordsfound", False)
            End If
            e.DataSource = dvEquipmentTracking
            CacheData(EQUIPMENT_TRACKING, dsEquipmentTracking)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
    
#Region "ifgTracking_RowDataBound"
    Protected Sub ifgTracking_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgTracking.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim hyplnkContainer As HyperLink
                hyplnkContainer = CType(e.Row.Cells(0).Controls(0), HyperLink)
                hyplnkContainer.Attributes.Add("onclick", String.Concat("openContainerHistoryReport('", drv.Item(EquipmentTrackingData.CSTMR_ID).ToString(), "','", drv.Item(EquipmentTrackingData.EQPMNT_NO).ToString(), "');  return false;"))
                hyplnkContainer.NavigateUrl = "#"
                Dim hyplnkActivity As HyperLink
                hyplnkActivity = CType(e.Row.Cells(2).Controls(0), HyperLink)
                If drv.Item(EquipmentTrackingData.EQPMNT_STTS_CD).ToString = "F" Or drv.Item(EquipmentTrackingData.EQPMNT_STTS_CD).ToString = "G" Then
                    drv.Item(EquipmentTrackingData.ACTVTY_NAM) = "Lessee Authorization"
                ElseIf drv.Item(EquipmentTrackingData.EQPMNT_STTS_CD).ToString = "J" Or drv.Item(EquipmentTrackingData.EQPMNT_STTS_CD).ToString = "K" Then
                    drv.Item(EquipmentTrackingData.ACTVTY_NAM) = "Owner Approval"
                ElseIf drv.Item(EquipmentTrackingData.EQPMNT_STTS_CD).ToString = "H" Or drv.Item(EquipmentTrackingData.EQPMNT_STTS_CD).ToString = "L" Or drv.Item(EquipmentTrackingData.EQPMNT_STTS_CD).ToString = "M" Then
                    drv.Item(EquipmentTrackingData.ACTVTY_NAM) = "Survey Completion"
                End If
                hyplnkActivity.Attributes.Add("onclick", String.Concat("openActivityReport('", drv.Item(EquipmentTrackingData.RFRNC_NO).ToString(), "','", drv.Item(EquipmentTrackingData.ACTVTY_NAM).ToString(), "','", drv.Item(EquipmentTrackingData.EIR_DT).ToString(), "','", drv.Item(EquipmentTrackingData.EQPMNT_STTS_CD).ToString(), "'); return false;"))
                hyplnkActivity.NavigateUrl = "#"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
    
End Class
