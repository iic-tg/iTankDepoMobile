
Partial Class Masters_ExchangeRate
    Inherits Pagebase

#Region "Declarations"
    Private strMSGUPDATE As String = "Exchange Rate(s) Updated Successfully."
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private Const EXCHANGE_RATE As String = "EXCHANGE_RATE"
    Dim strExchangeRateDuplicateRowCondition As String() = {ExchangeRateData.FRM_CRRNCY_ID, ExchangeRateData.TO_CRRNCY_ID}
#End Region

#Region "Parameters"
    Public dsExchangeRate As ExchangeRateDataSet
    Public dtExchangeRate As DataTable
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
#End Region

#Region "Page_Load"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strpagetitle As String = Request.QueryString("pagetitle")
            ifgExchangeRate.DeleteButtonText = "Delete"
            ifgExchangeRate.RefreshButtonText = "Refresh"
            ifgExchangeRate.AddButtonText = "Add Exchange Rate"
            ifgExchangeRate.ActionButtons.Item(0).Text = "Export"
            If Not Request.QueryString("Export") Is Nothing Then
                If Request.QueryString("Export") = "True" Then
                    Dim objCommon As New CommonData
                    Dim objstream As New IO.StringWriter
                    objstream = objCommon.ExportToExcel(CType(ifgExchangeRate.DataSource, DataTable), "Exchange Rate", "FRM_CRRNCY_CD,TO_CRRNCY_CD,EXCHNG_RT_PR_UNT_NC,WTH_EFFCT_FRM_DT,ACTV_BT", "EXCHNG_RT_ID", True)
                    Response.ContentType = "application/vnd.Excel"
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Exchange Rate" + ".xls")
                    Response.Write(objstream.ToString())
                    Response.End()
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "UpdateExchangeRate"
                pvt_UpdateExchangeRate()
                ' Case "ValidateExchangeCode"
                ' pvt_ValidateExchangeRateCode(e.GetCallbackValue("FrmCrrncy"), e.GetCallbackValue("ToCrrncy"), e.GetCallbackValue("Date"))
        End Select
    End Sub
#End Region

#Region "ifgExchangeRate_ClientBind"
    Protected Sub ifgExchangeRate_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgExchangeRate.ClientBind
        Try
            Dim obj As New ExchangeRate
            Dim objCommon As New CommonData
            Dim strWfData As String = CStr(e.Parameters("WFDATA"))
            ifgExchangeRate.UseCachedDataSource = True
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                strWfData = String.Concat("DPT_ID=", objCommon.GetHeadQuarterID().ToString)
            End If
            dsExchangeRate = obj.pub_GetEXCHANGERATEByDPTID(strWfData)
            dtExchangeRate = dsExchangeRate.Tables(ExchangeRateData._EXCHANGE_RATE)
            e.DataSource = dtExchangeRate
            CacheData(EXCHANGE_RATE, dsExchangeRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgExchangeRate_RowInserting"
    Protected Sub ifgExchangeRate_RowInserting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgExchangeRate.RowInserting

        Try
            dsExchangeRate = CType(RetrieveData(EXCHANGE_RATE), ExchangeRateDataSet)
            dtExchangeRate = dsExchangeRate.Tables(ExchangeRateData._EXCHANGE_RATE)

            Dim strPageMode As String = CStr(e.InputParamters("mode"))
            Dim lngID As Long
            lngID = CommonWeb.GetNextIndex(dtExchangeRate, ExchangeRateData.EXCHNG_RT_ID)

            If CommonWeb.pub_IsRowAlreadyExists(dtExchangeRate, CType(e.Values, OrderedDictionary), strExchangeRateDuplicateRowCondition, "new", ExchangeRateData.EXCHNG_RT_ID, CInt(e.Values(ExchangeRateData.EXCHNG_RT_ID))) Then
                e.OutputParamters("Duplicate") = strMSGDUPLICATE
                e.Cancel = True
                Exit Sub
            Else
                e.Values(ExchangeRateData.EXCHNG_RT_ID) = lngID
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgExchangeRate_RowUpdating"

    Protected Sub ifgExchangeRate_RowUpdating(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgExchangeRate.RowUpdating
   
        Try
            dsExchangeRate = CType(RetrieveData(EXCHANGE_RATE), ExchangeRateDataSet)
            If CommonWeb.pub_IsRowAlreadyExists(dsExchangeRate.Tables(ExchangeRateData._EXCHANGE_RATE), CType(e.NewValues, OrderedDictionary), strExchangeRateDuplicateRowCondition, "new", ExchangeRateData.EXCHNG_RT_ID, CInt(e.OldValues(ExchangeRateData.EXCHNG_RT_ID))) Then
                e.OutputParamters("Duplicate") = strMSGDUPLICATE
                e.Cancel = True
                e.RequiresRebind = True
                Exit Sub
            Else
                e.NewValues(ExchangeRateData.EXCHNG_RT_ID) = e.OldValues(ExchangeRateData.EXCHNG_RT_ID)
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgExchangeRate_RowDeleting"
    Protected Sub ifgExchangeRate_RowDeleting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgExchangeRate.RowDeleting
        Try
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgExchangeRate.PageSize * ifgExchangeRate.PageIndex + e.RowIndex
            If CType(ifgExchangeRate.DataSource, DataTable).Select(String.Concat(ExchangeRateData.EXCHNG_RT_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                e.OutputParamters("Delete") = "Exchange Rate cannot be deleted"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateExchangeRate"
    Private Sub pvt_UpdateExchangeRate()
        Try
            Dim objExchangeRate As New ExchangeRate
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer
            If objCommondata.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommondata.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommondata.GetDepotID())
            End If
            dtExchangeRate = CType(ifgExchangeRate.DataSource, DataTable)
            objExchangeRate.pub_UpdateExchangeRate(CType(dtExchangeRate.DataSet, ExchangeRateDataSet), objCommondata.GetCurrentUserName(), CDate(objCommondata.GetCurrentDate()), intDepotID)
            dtExchangeRate.AcceptChanges()
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgExchangeRate_RowDataBound"
    Protected Sub ifgExchangeRate_RowDataBound(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgExchangeRate.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
                Dim datControl As iDate
                datControl = CType(DirectCast(DirectCast(e.Row.Cells(3),  _
                            iFgFieldCell).ContainingField,  _
                            DateField).iDate, iDate)
                datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper

            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As System.Data.DataRowView
                    drv = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        'CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If
                If e.Row.RowIndex > 5 Then
                    Dim lkpFromCurrecny As iLookup
                    lkpFromCurrecny = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpFromCurrecny.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
                If e.Row.RowIndex > 5 Then
                    Dim lkpToCurrecny As iLookup
                    lkpToCurrecny = CType(DirectCast(DirectCast(e.Row.Cells(1), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpToCurrecny.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/ExchangeRate.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class