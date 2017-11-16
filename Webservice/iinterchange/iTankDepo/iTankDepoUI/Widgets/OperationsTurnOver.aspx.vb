Option Strict On
Partial Class Widgets_OperationsTurnOver
    Inherits Framebase
    Private Const OPERATIONS_TURN_OVER As String = "OPERATIONS_TURN_OVER"
    Dim dsOperationsTurnover As New OperationsTurnOverDataSet
    Dim dtOperationsTurnover As New DataTable

#Region "Page_PreRender"

    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Widgets/OperationsTurnOver.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType

                Case "SetTrend"
                    pvt_GetOperationTrend(e.GetCallbackValue("CustomerID"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetOperationTrend"
    Private Sub pvt_GetOperationTrend(ByVal bv_strCustomerID As String)
        Try
            Dim objOperationsTurnover As New OperationsTurnOver
            Dim objCommon As New CommonData()
            Dim sbrGateIn As New StringBuilder
            Dim sbrRepairCompletion As New StringBuilder
            Dim sbrGateout As New StringBuilder
            Dim strSortExpression As String = String.Concat(OperationsTurnOverData.ACTVTY_DT, " ASC")
            Dim dtGateInActivity As New DataTable
            Dim dvGateInActivity As DataView
            Dim dtGateOutActivity As New DataTable
            Dim dvGateOutActivity As DataView
            Dim dtRepairCompletionActivity As New DataTable
            Dim dvRepairCompletionActivity As DataView
            Dim strFilter As String
            dsOperationsTurnover = CType(RetrieveData(OPERATIONS_TURN_OVER), OperationsTurnOverDataSet)
            If dsOperationsTurnover Is Nothing Then
                dsOperationsTurnover = objOperationsTurnover.pub_GetOperationsTurnOverByDepotID(CInt(objCommon.GetDepotID()))
            End If
            Dim dshOperationsTurnOver As New DatasetHelper()
            Dim strColumnList As String = String.Concat(OperationsTurnOverData.MNTH_YR, ", SUM(", OperationsTurnOverData.ACTVTY_CNT, ") ACTVTY_CNT,", OperationsTurnOverData.ACTVTY, ",", OperationsTurnOverData.ACTVTY_DT, ",", OperationsTurnOverData.CSTMR_ID, ",", OperationsTurnOverData.DPT_ID)
            dtOperationsTurnover = dsOperationsTurnover.Tables(OperationsTurnOverData._V_EQUIPMENT_ACTIVITY)
            If dtOperationsTurnover.Select(OperationsTurnOverData.ACTVTY & "='GI'").Length > 0 Then
                dtGateInActivity = dtOperationsTurnover.Select(OperationsTurnOverData.ACTVTY & "='GI'").CopyToDataTable()
            End If
            If dtOperationsTurnover.Select(OperationsTurnOverData.ACTVTY & "='GO'").Length > 0 Then
                dtGateOutActivity = dtOperationsTurnover.Select(OperationsTurnOverData.ACTVTY & "='GO'").CopyToDataTable()
            End If
            If dtOperationsTurnover.Select(OperationsTurnOverData.ACTVTY & "='RC'").Length > 0 Then
                dtRepairCompletionActivity = dtOperationsTurnover.Select(OperationsTurnOverData.ACTVTY & "='RC'").CopyToDataTable()
            End If
            dvGateInActivity = dtGateInActivity.DefaultView
            dvGateOutActivity = dtGateOutActivity.DefaultView
            dvRepairCompletionActivity = dtRepairCompletionActivity.DefaultView
            If bv_strCustomerID <> "" Then
                strFilter = String.Concat(OperationsTurnOverData.CSTMR_ID, "=", bv_strCustomerID)
                dvGateInActivity.RowFilter = strFilter
                dvGateOutActivity.RowFilter = strFilter
                dvRepairCompletionActivity.RowFilter = strFilter
            Else
                dvGateInActivity.RowFilter = Nothing
                dvGateOutActivity.RowFilter = Nothing
                dvRepairCompletionActivity.RowFilter = Nothing
            End If
            dvGateInActivity.Sort = strSortExpression
            dvGateOutActivity.Sort = strSortExpression
            dvRepairCompletionActivity.Sort = strSortExpression

            dtGateInActivity = dshOperationsTurnOver.SelectGroupByInto(OperationsTurnOverData._V_EQUIPMENT_ACTIVITY, dvGateInActivity.ToTable, strColumnList, "", OperationsTurnOverData.MNTH_YR)
            dtGateOutActivity = dshOperationsTurnOver.SelectGroupByInto(OperationsTurnOverData._V_EQUIPMENT_ACTIVITY, dvGateOutActivity.ToTable, strColumnList, "", OperationsTurnOverData.MNTH_YR)
            dtRepairCompletionActivity = dshOperationsTurnOver.SelectGroupByInto(OperationsTurnOverData._V_EQUIPMENT_ACTIVITY, dvRepairCompletionActivity.ToTable, strColumnList, "", OperationsTurnOverData.MNTH_YR)

            

            If dtGateInActivity.Rows.Count > 0 Then
                sbrGateIn.Append("iGateIns=[")
                Dim intItemCount As Integer = 0
                For Each drOperations As DataRow In dtGateInActivity.Rows
                    sbrGateIn.Append("['")
                    sbrGateIn.Append(drOperations.Item(OperationsTurnOverData.ACTVTY_DT))
                    sbrGateIn.Append(String.Concat("',", drOperations.Item(OperationsTurnOverData.ACTVTY_CNT).ToString))
                    sbrGateIn.Append("]")
                    intItemCount += 1
                    If Not intItemCount = dvGateInActivity.Count Then
                        sbrGateIn.Append(",")
                    End If
                Next
                sbrGateIn.Append("];")
            End If
            If dtRepairCompletionActivity.Rows.Count > 0 Then
                sbrRepairCompletion.Append("iRepairCompletions=[")
                Dim intItemCount As Integer = 0
                For Each drOperations As DataRow In dtRepairCompletionActivity.Rows
                    sbrRepairCompletion.Append("['")
                    sbrRepairCompletion.Append(drOperations.Item(OperationsTurnOverData.ACTVTY_DT))
                    sbrRepairCompletion.Append(String.Concat("',", drOperations.Item(OperationsTurnOverData.ACTVTY_CNT).ToString))
                    sbrRepairCompletion.Append("]")
                    intItemCount += 1
                    If Not intItemCount = dvRepairCompletionActivity.Count Then
                        sbrRepairCompletion.Append(",")
                    End If
                Next
                sbrRepairCompletion.Append("];")
            End If
            If dtGateOutActivity.Rows.Count > 0 Then
                sbrGateout.Append("iGateOuts=[")
                Dim intItemCount As Integer = 0
                For Each drOperations As DataRow In dtGateOutActivity.Rows
                    sbrGateout.Append("['")
                    sbrGateout.Append(drOperations.Item(OperationsTurnOverData.ACTVTY_DT))
                    sbrGateout.Append(String.Concat("',", drOperations.Item(OperationsTurnOverData.ACTVTY_CNT).ToString))
                    sbrGateout.Append("]")
                    intItemCount += 1
                    If Not intItemCount = dvGateOutActivity.Count Then
                        sbrGateout.Append(",")
                    End If
                Next
                sbrGateout.Append("];")
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("GraphData", String.Concat(sbrGateIn.ToString, sbrRepairCompletion.ToString, sbrGateout.ToString))
            CacheData(OPERATIONS_TURN_OVER, dsOperationsTurnover)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
