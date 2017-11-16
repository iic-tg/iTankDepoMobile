
Partial Class Masters_RouteDetail
    Inherits Pagebase

#Region "Parameters"
    Public dsRoute As New RouteDetailDataSet
    Public dtRoute As DataTable
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
#End Region

#Region "Declarations"
    Private strMSGUPDATE As String = "Route(s) Updated Successfully"
    Private Const ROUTE As String = "ROUTE"
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
#End Region
   
#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "ValidateCode"
                pvt_ValidateRouteCode(e.GetCallbackValue("Code"), _
                               e.GetCallbackValue("GridIndex"), e.GetCallbackValue("RowState"), e.GetCallbackValue("WFDATA"))
            Case "UpdateRoute"
                UpdateRoute(e.GetCallbackValue("WFDATA"))
        End Select
    End Sub
#End Region

#Region "UpdateRoute"
    Private Sub UpdateRoute(ByVal bv_strWFData As String)
        Try
            Dim objRoute As New RouteDetail
            Dim objCommondata As New CommonData
            Dim objcommon As New CommonData()
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWFData, CommonUIData.DPT_ID))
            End If
            dtRoute = CType(ifgRoute.DataSource, DataTable)
            objRoute.pub_UpdateRoute(CType(dtRoute.DataSet, RouteDetailDataSet), objCommondata.GetCurrentUserName(), CDate(objCommondata.GetCurrentDate()), bv_strWFData, intDepotID)
            dtRoute.AcceptChanges()
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/RouteDetail.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRoute_ClientBind"
    Protected Sub ifgRoute_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgRoute.ClientBind
        Try
            Dim objcommon As New CommonData()
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            Dim objRoute As New RouteDetail
            Dim strWfData As String = CStr(e.Parameters("WFDATA"))
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = objcommon.GetHeadQuarterID()
            End If
            dsRoute = objRoute.pub_VRouteGetVRouteByDepotId(intDepotID)
            e.DataSource = dsRoute.Tables(RouteDetailData._V_ROUTE)
            CacheData(ROUTE, dsRoute)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRoute_RowDataBound"
    Protected Sub ifgRoute_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgRoute.RowDataBound
        Try
            Dim objCommondata As New CommonData
            'Dim intDeporCurrencyID As Integer = CommonWeb.iInt(objCommondata.GetDepotLocalCurrencyID())
            Dim strDeporCurrencyCD As String = (objCommondata.GetDepotLocalCurrencyCode()).ToString
            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Empty Trip Rate", " (", strDeporCurrencyCD, ")")
                CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Full Trip Rate", " (", strDeporCurrencyCD, ")")
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As System.Data.DataRowView
                    drv = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If
                If e.Row.RowIndex > 6 Then
                    Dim lkpPickup As iLookup
                    lkpPickup = CType(DirectCast(DirectCast(e.Row.Cells(2), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpPickup.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom

                    Dim lkpDrop As iLookup
                    lkpDrop = CType(DirectCast(DirectCast(e.Row.Cells(3), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpDrop.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
   
#Region "ifgRoute_RowDeleting"
    Protected Sub ifgRoute_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgRoute.RowDeleting
        Try
            dsRoute = CType(RetrieveData(ROUTE), RouteDetailDataSet)
            Dim dtRouteDetail As Data.DataTable = dsRoute.Tables(RouteDetailData._V_ROUTE).Copy
            If CType(ifgRoute.DataSource, DataTable).Select(String.Concat(RouteDetailData.RT_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                e.OutputParamters("Delete") = String.Concat("Route : Code ", dtRouteDetail.Select(String.Concat(RouteDetailData.RT_ID, "=", e.Keys(0)))(0).Item(RouteDetailData.RT_CD).ToString, " cannot be deleted")
                Exit Sub
            Else
                e.OutputParamters("Delete") = String.Concat("Route : Code ", dtRouteDetail.Select(String.Concat(RouteDetailData.RT_ID, "=", e.Keys(0)))(0).Item(RouteDetailData.RT_CD).ToString, " has been be deleted. Click submit to save changes.")
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
   
#Region "ifgRoute_RowInserting"
    Protected Sub ifgRoute_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgRoute.RowInserting
        Try
            dsRoute = CType(RetrieveData(ROUTE), RouteDetailDataSet)
            dtRoute = dsRoute.Tables(RouteDetailData._V_ROUTE)
            e.Values(RouteDetailData.RT_ID) = CommonWeb.GetNextIndex(dtRoute, RouteDetailData.RT_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateRouteCode"
    Private Sub pvt_ValidateRouteCode(ByVal bv_strCode As String, ByVal bv_intGridRowIndex As Integer, ByVal bv_strRowState As String, ByVal bv_strWFDATA As String)
        Try
            Dim blnValid As Boolean
            Dim strExistCode As String = String.Empty
            Dim objCommon As New CommonData
            dsRoute = CType(RetrieveData(ROUTE), RouteDetailDataSet)
            Dim dtRouteDetail As DataTable = dsRoute.Tables(RouteDetailData._V_ROUTE).Copy
            dtRouteDetail.AcceptChanges()

            'Checking whether the entered code is available in grid
            Dim intResultIndex() As System.Data.DataRow = dtRouteDetail.Select("RT_CD" & "='" & bv_strCode & "'")

            If intResultIndex.Length > 0 Then
                blnValid = False
            Else
                blnValid = True
            End If
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                bv_strWFDATA = String.Concat("DPT_ID=", objCommon.GetHeadQuarterID().ToString)
            End If
            'Checking whether the entered code is available in database
            If blnValid = True AndAlso bv_strCode <> Nothing Then
                If bv_strRowState = "Added" Then
                    Dim objRoute As New RouteDetail
                    blnValid = objRoute.pub_ValidateRoute(bv_strCode, bv_strWFDATA)
                End If
            End If

            If blnValid = True Then
                pub_SetCallbackReturnValue("pkValid", "true")
            Else
                pub_SetCallbackReturnValue("pkValid", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

End Class
