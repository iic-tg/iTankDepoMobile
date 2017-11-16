
Partial Class Operations_LeakTestRevision
    Inherits Pagebase
    Dim dsLeakTestData As New LeakTestDataSet
    Dim dtLeakTestData As DataTable
    Private Const LEAK_TEST_DETAIL As String = "LEAK_TEST_DETAIL"

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region



#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Operations/LeakTestRevision.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region ""
    Protected Sub ifgRevisionHistory_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgRevisionHistory.ClientBind
        Try
            Dim objLeakTestDetail As New LeakTest
            If Not e.Parameters("EquipmentNo") Is Nothing And Not e.Parameters("GateinTransaction") Is Nothing Then
                Dim objCommon As New CommonData
                Dim dt As DataTable
                dt=dsLeakTestData .Tables (LeakTestData ._LEAK_TEST ).Clone ()
                Dim intDepotID As Integer = objCommon.GetDepotID()
                dsLeakTestData = objLeakTestDetail.pub_GetLeakTestRevisionHistory(e.Parameters("EquipmentNo").ToString, e.Parameters("GateinTransaction"), intDepotID)
                e.DataSource = dsLeakTestData.Tables(LeakTestData._LEAK_TEST)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
