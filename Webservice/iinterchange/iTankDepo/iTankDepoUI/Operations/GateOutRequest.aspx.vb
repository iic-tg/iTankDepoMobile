
Partial Class Operations_GateOutRequest
    Inherits Pagebase

    'Declarations
    Dim ds As GateOutRequestDataSet
    Private Const GATEOUTREQUEST As String = "GATEOUTREQUEST"

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GateOutRequest"
                    GateOutRequest_Update()
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Sub GateOutRequest_Update()
        Try
            ds = RetrieveData(GATEOUTREQUEST)
            Dim objGateOutRequest As New GateOutRequest
            Dim objCommonData As New CommonData
            Dim strUserName As String = objCommonData.GetCurrentUserName()
            Dim datRequestedDate As DateTime = objCommonData.GetCurrentDate()
            Dim statusFlg As Boolean = False
            statusFlg = objGateOutRequest.UpdateActivity_Status(ds, strUserName, datRequestedDate)

            If statusFlg = True Then
                pub_SetCallbackReturnValue("Message", "Gate out request updated Successfully.")
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
            CommonWeb.IncludeScript("Script/Operations/GateOutRequest.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgGateOutRequest_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgGateOutRequest.ClientBind
        Try
            Dim objCommonData As New CommonData
            Dim objGateOutRequest As New GateOutRequest

            Dim intDepoId As Int32 = objCommonData.GetDepotID()
            ds = objGateOutRequest.GetGateoutReuestRecords(intDepoId)

            e.DataSource = ds.Tables(GateOutRequestData._V_ACTIVITY_STATUS)
            CacheData(GATEOUTREQUEST, ds)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgGateOutRequest_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgGateOutRequest.RowUpdated

    End Sub

    Protected Sub ifgGateOutRequest_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgGateOutRequest.RowUpdating

    End Sub
End Class
