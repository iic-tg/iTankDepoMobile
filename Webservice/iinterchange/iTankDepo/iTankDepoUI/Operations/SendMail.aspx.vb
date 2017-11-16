
Partial Class Operations_BulkMail
    Inherits Pagebase

#Region "Declaration"
    Private Const TRACKING As String = "TRACKING"
    Dim dsTrackingDataSet As New TrackingDataSet
    Private strMSGINSERT As String = "Mail Sent Successfully."
#End Region

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
   Select e.CallbackType
            Case "CreateBulkMail"
                pvt_CreateBulkMail(e.GetCallbackValue("bv_strFrom"), e.GetCallbackValue("bv_strTo"), _
                                   e.GetCallbackValue("bv_strSubject"), e.GetCallbackValue("bv_strBody"))
        End Select

    End Sub
#End Region

#Region "Page_PreRender"

    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/SendMail.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_CreateBulkMail"

    Private Sub pvt_CreateBulkMail(ByVal bv_strFrom As String, ByVal bv_strTo As String, ByVal bv_strSubject As String, ByVal bv_strBody As String)
        Try
            dsTrackingDataSet = CType(RetrieveData(TRACKING), TrackingDataSet)
            Dim objTracking As New Tracking
            Dim objcommon As New CommonData
            Dim strCreatedby As String = objcommon.GetCurrentUserName()
            Dim datCreatedDate As String = objcommon.GetCurrentDate()
            Dim intDepotId As Integer = objcommon.GetDepotID()
            Dim lngCreated As Long = objTracking.pub_CreateBulk_Email(bv_strFrom, bv_strTo, bv_strSubject, bv_strBody, strCreatedby, _
                                                                      datCreatedDate, intDepotId, dsTrackingDataSet)
            pub_SetCallbackReturnValue("Message", strMSGINSERT)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
