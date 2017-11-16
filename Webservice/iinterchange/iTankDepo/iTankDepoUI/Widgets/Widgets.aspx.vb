Option Strict On
Partial Class Widgets_Widgets
    Inherits Framebase

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack And Not Page.IsCallback Then
            pvt_BindWidgets()
        End If
    End Sub

    Private Sub pvt_BindWidgets()
        Try
            Dim dsDashboard As New DashboardDataSet
            Dim objDashboard As New Dashboard
            Dim objcommon As New CommonData()
            Dim intRoleID As Integer = CInt(objcommon.GetCurrentRoleID())
            dsDashboard = objDashboard.pub_GetDashboardWidgets(intRoleID)
            WidgetList.DataSource = dsDashboard.Tables(DashboardData._WIDGET)
            WidgetList.DataBind()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
End Class
