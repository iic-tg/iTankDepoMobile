Imports Business.FinanceIntegration

Partial Class Sage_Integration_FinanceIntegrationMaster
    Inherits Pagebase

#Region "Declaration"
    Private Const FINANCE_INTEGRATION As String = "FINANCE_INTEGRATION"

    Dim dsFinanceIntegration As New FinanceIntegrationDataSet
#End Region

#Region "Page Events"

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType.ToLower()
                Case "updatefinanceintegration"
                    'pvt_UpdateFinanceIntegration(e.GetCallbackValue("WFDATA"))
                    pvt_UpdateFinanceIntegration()
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/SageIntegration/FinanceIntegrationMaster.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/MaxLength.js", MyBase.Page)
        Catch ex As Exception
           iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

    Private Sub pvt_UpdateFinanceIntegration()
        Try

            dsFinanceIntegration = CType(RetrieveData(FINANCE_INTEGRATION), FinanceIntegrationDataSet)
            Dim objcommon As New CommonData
            Dim objFinanceIntegration As New FinanceIntegration
            Dim intDepotId As Integer
            If objcommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                intDepotId = objcommon.GetHeadQuarterID()
            Else
                intDepotId = objcommon.GetDepotID()
            End If
            objFinanceIntegration.pub_UpdateFINANCE_INTEGRATION(dsFinanceIntegration, intDepotId, objcommon.GetCurrentUserName(), objcommon.GetCurrentDate())
            dsFinanceIntegration.AcceptChanges()
            pub_SetCallbackStatus(True)

        Catch ex As Exception

            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)

        End Try
    End Sub

    Protected Sub ifgFinanceIntegration_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgFinanceIntegration.ClientBind
        Try
            Dim objFinanceIntegration As New FinanceIntegration
            dsFinanceIntegration = objFinanceIntegration.pub_GetFINANCE_INTEGRATION()
            'ifgFinanceIntegration.PageIndex = 0
            e.DataSource = dsFinanceIntegration.Tables(FinanceIntegrationData._FINANCE_INTEGRATION)
            CacheData(FINANCE_INTEGRATION, dsFinanceIntegration)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgFinanceIntegration_RowDataBound(sender As Object, e As iFlexGridRowEventArgs) Handles ifgFinanceIntegration.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowIndex > 6 Then
                Dim lkpInvoiceType As iLookup
                lkpInvoiceType = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                lkpInvoiceType.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom

                Dim lkpEquipmentType As iLookup
                lkpEquipmentType = CType(DirectCast(DirectCast(e.Row.Cells(1), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                lkpEquipmentType.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
            End If
        End If
    End Sub

    Protected Sub ifgFinanceIntegration_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgFinanceIntegration.RowInserted
        Try
            CacheData(FINANCE_INTEGRATION, dsFinanceIntegration)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    
    Protected Sub ifgFinanceIntegration_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgFinanceIntegration.RowInserting

        Try
            dsFinanceIntegration = CType(RetrieveData(FINANCE_INTEGRATION), FinanceIntegrationDataSet)
            Dim strFilter As String = String.Concat(FinanceIntegrationData.INVC_TYP_ID, "=", e.Values(FinanceIntegrationData.INVC_TYP_ID), " and ", FinanceIntegrationData.EQPMNT_TYP_ID, "=", e.Values(FinanceIntegrationData.EQPMNT_TYP_ID), " and ", FinanceIntegrationData.ITEM_CD, "='", e.Values(FinanceIntegrationData.ITEM_CD), "'", "  and ", FinanceIntegrationData.CTGRY_CD, "='", e.Values(FinanceIntegrationData.CTGRY_CD), "'")
            Dim dtFinanceIntegration As DataTable = dsFinanceIntegration.Tables(FinanceIntegrationData._FINANCE_INTEGRATION).Copy()
            'dtFinanceIntegration.AcceptChanges()
            Dim intResultIndex() As System.Data.DataRow = dtFinanceIntegration.Select(strFilter)
            If intResultIndex.Length > 0 Then
                e.OutputParamters.Add("Duplicate", "Same Combination Already Added.")
                e.Cancel = True
                Exit Sub
            End If
            e.Values(FinanceIntegrationData.FNC_INTGRTN_ID) = CommonWeb.GetNextIndex(dsFinanceIntegration.Tables(FinanceIntegrationData._FINANCE_INTEGRATION), FinanceIntegrationData.FNC_INTGRTN_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
        
    End Sub

    Protected Sub ifgFinanceIntegration_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgFinanceIntegration.RowUpdated
        Try
            CacheData(FINANCE_INTEGRATION, dsFinanceIntegration)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgFinanceIntegration_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgFinanceIntegration.RowUpdating
        Try
            dsFinanceIntegration = CType(RetrieveData(FINANCE_INTEGRATION), FinanceIntegrationDataSet)
            Dim strFilter As String = String.Concat(FinanceIntegrationData.INVC_TYP_ID, "=", e.NewValues(FinanceIntegrationData.INVC_TYP_ID), " and ", FinanceIntegrationData.EQPMNT_TYP_ID, "=", e.NewValues(FinanceIntegrationData.EQPMNT_TYP_ID), "  and ", FinanceIntegrationData.ITEM_CD, "='", e.NewValues(FinanceIntegrationData.ITEM_CD), "'", "  and ", FinanceIntegrationData.CTGRY_CD, "='", e.NewValues(FinanceIntegrationData.CTGRY_CD), "'", "  and ", FinanceIntegrationData.FNC_INTGRTN_ID, "<>", e.OldValues(FinanceIntegrationData.FNC_INTGRTN_ID))
            Dim dtFinanceIntegration As DataTable = dsFinanceIntegration.Tables(FinanceIntegrationData._FINANCE_INTEGRATION).Copy()
            'dtFinanceIntegration.AcceptChanges()
            Dim intResultIndex() As System.Data.DataRow = dtFinanceIntegration.Select(strFilter)
            If intResultIndex.Length > 0 Then
                e.OutputParamters.Add("Duplicate", "Same Combination Already Existing.")
                e.Cancel = True
                Exit Sub
            End If


           

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
End Class
