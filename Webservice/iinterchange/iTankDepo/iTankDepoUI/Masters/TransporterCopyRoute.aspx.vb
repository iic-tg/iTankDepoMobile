
Partial Class Masters_TransporterCopyRoute
    Inherits Pagebase

    Dim dsTransporter As New TransporterDataSet
    Dim TRANSPORTER As String = "TRANSPORTER"

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Masters/TransporterCopyRoute.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region


#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "applyRouteDetail"
                    pvt_ApplyRouteDetails(e.GetCallbackValue("TransporterID"), _
                                          e.GetCallbackValue("chkCustomerRate"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ApplyRouteDetails()"
    Private Sub pvt_ApplyRouteDetails(ByVal bv_strTransporterId As String, _
                                      ByVal bv_strCustomerRate As String)
        Try
            Dim dtRouteDetail As New DataTable
            Dim dtCopyRoute As New DataTable
            Dim objCommonData As New CommonData
            Dim objTransporter As New Transporter
            Dim drAddRoute As DataRow = Nothing
            Dim drSelect As DataRow() = Nothing
            Dim intDepotId As Integer = objCommonData.GetDepotID()
            dsTransporter = CType(RetrieveData(TRANSPORTER), TransporterDataSet)
            If objCommonData.GetMultiLocationSupportConfig.ToLower = "true" Then
                dtRouteDetail = objTransporter.pub_GetTransporterRouteDetailByTransporterID(CLng(bv_strTransporterId), objCommonData.GetHeadQuarterID()).Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL)
            Else
                dtRouteDetail = objTransporter.pub_GetTransporterRouteDetailByTransporterID(CLng(bv_strTransporterId), intDepotId).Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL)
            End If
            dtCopyRoute = dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Clone()
            If CBool(bv_strCustomerRate) = False Then
                For Each drRouteCopy As DataRow In dtRouteDetail.Rows
                    drSelect = dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Select(String.Concat(TransporterData.RT_CD, " = '", drRouteCopy.Item(TransporterData.RT_CD), "'"))
                    If drSelect.Length = 0 Then
                        drAddRoute = dtCopyRoute.NewRow()
                        drAddRoute.Item(TransporterData.TRNSPRTR_RT_DTL_ID) = CInt(CommonWeb.GetNextIndex(dtCopyRoute, TransporterData.TRNSPRTR_RT_DTL_ID) + CommonWeb.GetNextIndex(dtRouteDetail, TransporterData.TRNSPRTR_RT_DTL_ID) + CommonWeb.GetNextIndex(dsTransporter.Tables(TransporterData._V_TRANSPORTER), TransporterData.TRNSPRTR_ID))
                        drAddRoute.Item(TransporterData.TRNSPRTR_ID) = drRouteCopy.Item(TransporterData.TRNSPRTR_ID)
                        drAddRoute.Item(TransporterData.RT_ID) = drRouteCopy.Item(TransporterData.RT_ID)
                        drAddRoute.Item(TransporterData.RT_CD) = drRouteCopy.Item(TransporterData.RT_CD)
                        drAddRoute.Item(TransporterData.RT_DSCRPTN_VC) = drRouteCopy.Item(TransporterData.RT_DSCRPTN_VC)
                        drAddRoute.Item(TransporterData.PCK_UP_LCTN_CD) = drRouteCopy.Item(TransporterData.PCK_UP_LCTN_CD)
                        drAddRoute.Item(TransporterData.DRP_OFF_LCTN_CD) = drRouteCopy.Item(TransporterData.DRP_OFF_LCTN_CD)
                        drAddRoute.Item(TransporterData.EMPTY_TRP_SPPLR_RT_NC) = 0.0
                        drAddRoute.Item(TransporterData.EMPTY_TRP_CSTMR_RT_NC) = 0.0
                        drAddRoute.Item(TransporterData.FLL_TRP_SPPLR_RT_NC) = 0.0
                        drAddRoute.Item(TransporterData.FLL_TRP_CSTMR_RT_NC) = 0.0
                        dtCopyRoute.Rows.Add(drAddRoute)
                    End If
                Next
            Else
                For Each drRouteCopy As DataRow In dtRouteDetail.Rows
                    drSelect = dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Select(String.Concat(TransporterData.RT_CD, " = '", drRouteCopy.Item(TransporterData.RT_CD), "'"))
                    If drSelect.Length = 0 Then
                        drAddRoute = dtCopyRoute.NewRow()
                        drAddRoute.Item(TransporterData.TRNSPRTR_RT_DTL_ID) = CInt(CommonWeb.GetNextIndex(dtCopyRoute, TransporterData.TRNSPRTR_RT_DTL_ID) + CommonWeb.GetNextIndex(dtRouteDetail, TransporterData.TRNSPRTR_RT_DTL_ID) + CommonWeb.GetNextIndex(dsTransporter.Tables(TransporterData._V_TRANSPORTER), TransporterData.TRNSPRTR_ID))
                        drAddRoute.Item(TransporterData.TRNSPRTR_ID) = drRouteCopy.Item(TransporterData.TRNSPRTR_ID)
                        drAddRoute.Item(TransporterData.RT_ID) = drRouteCopy.Item(TransporterData.RT_ID)
                        drAddRoute.Item(TransporterData.RT_CD) = drRouteCopy.Item(TransporterData.RT_CD)
                        drAddRoute.Item(TransporterData.RT_DSCRPTN_VC) = drRouteCopy.Item(TransporterData.RT_DSCRPTN_VC)
                        drAddRoute.Item(TransporterData.PCK_UP_LCTN_CD) = drRouteCopy.Item(TransporterData.PCK_UP_LCTN_CD)
                        drAddRoute.Item(TransporterData.DRP_OFF_LCTN_CD) = drRouteCopy.Item(TransporterData.DRP_OFF_LCTN_CD)
                        drAddRoute.Item(TransporterData.EMPTY_TRP_SPPLR_RT_NC) = drRouteCopy.Item(TransporterData.EMPTY_TRP_SPPLR_RT_NC)
                        drAddRoute.Item(TransporterData.EMPTY_TRP_CSTMR_RT_NC) = drRouteCopy.Item(TransporterData.EMPTY_TRP_CSTMR_RT_NC)
                        drAddRoute.Item(TransporterData.FLL_TRP_SPPLR_RT_NC) = drRouteCopy.Item(TransporterData.FLL_TRP_SPPLR_RT_NC)
                        drAddRoute.Item(TransporterData.FLL_TRP_CSTMR_RT_NC) = drRouteCopy.Item(TransporterData.FLL_TRP_CSTMR_RT_NC)
                        dtCopyRoute.Rows.Add(drAddRoute)
                    End If
                Next
            End If
            dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Merge(dtCopyRoute)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
