
Partial Class Operations_GateOutApproval
    Inherits Pagebase

    'Declarations
    Dim ds As GateOutApprovalDataSet
    Private Const GATEOUTAPPROVAL As String = "GATEOUTAPPROVAL"
    Private Const GRIDMODE As String = "GRIDMODE"

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GateOutApproval"
                    GateOutRequest_Update(e.GetCallbackValue("GridMode"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Sub GateOutRequest_Update(ByVal strGridMode As String)
        Try
            ds = RetrieveData(GATEOUTAPPROVAL)
            Dim objGateOutApproval As New GateOutApproval
            Dim objCommonData As New CommonData
            Dim strUserName As String = objCommonData.GetCurrentUserName()
            Dim datRequestedDate As DateTime = objCommonData.GetCurrentDate()
            Dim statusFlg As Boolean = False
            Select Case strGridMode.ToUpper()

                Case "PENDING"
                    statusFlg = objGateOutApproval.UpdatePendingActivity_Status(ds, strUserName, datRequestedDate)

                Case "MYSUBMIT"
                    statusFlg = objGateOutApproval.UpdatemySubmitActivity_Status(ds, strUserName, datRequestedDate)
            End Select

            If statusFlg = True Then
                pub_SetCallbackReturnValue("Message", "Gate out approval updated successfully.")
            Else
                pub_SetCallbackReturnValue("Message", "No changes to save.")
            End If

            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub

    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/GateOutApproval.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
            '  CommonWeb.IncludeScript("Script/Controls/iGrid.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgGateOutApprovalPending_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgGateOutApprovalPending.ClientBind
        Try
            Dim objCommonData As New CommonData
            Dim objGateOutApproval As New GateOutApproval

            Dim intDepoId As Int32 = objCommonData.GetDepotID()

            If Not e.Parameters("Mode") Is Nothing AndAlso e.Parameters("Mode").ToString().ToUpper() = "PENDING" Then
                ds = New GateOutApprovalDataSet
                ds = objGateOutApproval.GetGateoutApprovalPending_Records(intDepoId)

                CacheData(GRIDMODE, "PENDING")
            ElseIf Not e.Parameters("Mode") Is Nothing AndAlso e.Parameters("Mode").ToString().ToUpper() = "MYSUBMIT" Then
                ds = New GateOutApprovalDataSet
                ds = objGateOutApproval.GetGateoutApprovalMySubmit_Records(intDepoId)
                CacheData(GRIDMODE, "MYSUBMIT")
            End If


            e.DataSource = ds.Tables(GateOutApprovalData._V_ACTIVITY_STATUS)
            ds.AcceptChanges()
            CacheData(GATEOUTAPPROVAL, ds)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgGateOutApprovalPending_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgGateOutApprovalPending.RowDataBound
        Try
            Dim strGridMode As String = RetrieveData(GRIDMODE)

            If e.Row.RowType = DataControlRowType.Header Then

                If strGridMode = "PENDING" Then
                    'e.Row.Cells(9).Style.Add("display", "none")
                    'e.Row.Cells(10).Style.Add("display", "none")
                    'e.Row.Cells(11).Text = "Approve"
                    'e.Row.Cells(12).Style.Add("display", "none")

                    e.Row.Cells(9).Visible = False
                    e.Row.Cells(10).Visible = False
                    e.Row.Cells(12).Visible = False
                    e.Row.Cells(11).Text = "Approve"

                Else
                    'e.Row.Cells(9).Style.Add("display", "block")
                    'e.Row.Cells(10).Style.Add("display", "block")
                    'e.Row.Cells(11).Text = "Select"
                    'e.Row.Cells(12).Style.Add("display", "block")

                    e.Row.Cells(9).Visible = True
                    e.Row.Cells(10).Visible = True
                    e.Row.Cells(12).Visible = True
                    e.Row.Cells(11).Text = "Select"
                End If

            End If

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)

                If strGridMode = "PENDING" Then
                    'e.Row.Cells(9).Style.Add("display", "none")
                    'e.Row.Cells(10).Style.Add("display", "none")
                    'e.Row.Cells(12).Style.Add("display", "none")

                    e.Row.Cells(9).Visible = False
                    e.Row.Cells(10).Visible = False
                    e.Row.Cells(12).Visible = False

                    'If Not drv.Item(ReserveBookingData.BKNG_AUTH_NO) Is DBNull.Value Then
                    '    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    'Else
                    '    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                    'End If


                Else
                    'e.Row.Cells(9).Style.Add("display", "block")
                    'e.Row.Cells(10).Style.Add("display", "block")
                    'e.Row.Cells(12).Style.Add("display", "block")

                    e.Row.Cells(9).Visible = True
                    e.Row.Cells(10).Visible = True
                    e.Row.Cells(12).Visible = True

                    'If Not drv.Item(ReserveBookingData.BKNG_AUTH_NO) Is DBNull.Value Then
                    '    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    'Else
                    '    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                    'End If

                End If


                If Not drv.Item(ReserveBookingData.BKNG_AUTH_NO) Is DBNull.Value AndAlso drv.Item(ReserveBookingData.BKNG_AUTH_NO).ToString().Trim() <> Nothing Then
                    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                Else
                    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    'Protected Sub ifgGateOutApprovalPending_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgGateOutApprovalPending.RowUpdated
    '    Try
    '        CacheData(GATEOUTAPPROVAL, ds)

    '    Catch ex As Exception
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '    End Try
    'End Sub

    'Protected Sub ifgGateOutApprovalPending_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgGateOutApprovalPending.RowUpdating
    '    Try
    '        ds = RetrieveData(GATEOUTAPPROVAL)

    '        If Not e.NewValues(GateOutApprovalData.GTAPPRVL_BY) Is DBNull.Value AndAlso e.NewValues(GateOutApprovalData.GTAPPRVL_BY).ToString().ToUpper() = "TRUE" Then
    '            e.OldValues(GateOutApprovalData.SLCT) = True
    '        ElseIf Not e.NewValues(GateOutApprovalData.GTAPPRVL_BY) Is DBNull.Value AndAlso e.NewValues(GateOutApprovalData.GTAPPRVL_BY).ToString().ToUpper() = "FALSE" Then
    '            e.OldValues(GateOutApprovalData.SLCT) = False
    '        End If

    '    Catch ex As Exception
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '    End Try
    'End Sub
End Class
