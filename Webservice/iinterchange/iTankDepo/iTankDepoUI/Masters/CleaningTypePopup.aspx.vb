
Partial Class Masters_CleaningTypePopup
    Inherits Pagebase

#Region "Declaration"
    Dim dsProduct As ProductDataSet
    Private Const PRODUCT_CUSTOMER_RATE As String = "PRODUCT_CUSTOMER_RATE"
    Private Const PRODUCT_CUSTOMER_RATE_TEMP As String = "PRODUCT_CUSTOMER_RATE_TEMP"
    Dim dtProductData As DataTable
    Dim objProduct As New Product
    Dim lngCustomerID As Long
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack AndAlso Not Page.IsCallback Then
                hdnProductID.Value = Request.QueryString(ProductData.PRDCT_ID)
                hdnProductCustomerID.Value = Request.QueryString(ProductData.PRDCT_CSTMR_ID)
                hdnCustomerID.Value = Request.QueryString(ProductData.CSTMR_ID)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "BindGridProductCustomer"
    Private Sub BindGridProductCustomer(ByVal bv_i64ProductID As String, ByVal bv_i64ProductCustomerID As Int64, ByVal bv_int64CustomerID As Int64)
        Try
            Dim drA As DataRow() = Nothing
            lngCustomerID = bv_int64CustomerID

            dsProduct = CType(RetrieveData(PRODUCT_CUSTOMER_RATE), ProductDataSet)

            If Not dsProduct Is Nothing Then
                dtProductData = dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE)
                drA = dtProductData.Select(String.Concat(ProductData.PRDCT_CSTMR_ID, "=", bv_i64ProductCustomerID))
                If drA.Length = 0 Then
                    drA = dtProductData.Select(String.Concat(ProductData.PRDCT_CSTMR_ID, "=", bv_i64ProductCustomerID), "", DataViewRowState.Deleted)
                    If drA.Length = 0 Then
                        drA = Nothing
                    End If
                End If
            End If
            If drA Is Nothing Then
                If bv_int64CustomerID > 0 Then
                    If dsProduct Is Nothing Then
                        dsProduct = objProduct.pub_GetProductCustomerRateBy_ProductID(bv_i64ProductID, bv_i64ProductCustomerID, bv_int64CustomerID)
                        dtProductData = dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE)
                    Else
                        dtProductData = objProduct.pub_GetProductCustomerRateBy_ProductID(bv_i64ProductID, bv_i64ProductCustomerID, bv_int64CustomerID).Tables(ProductData._V_PRODUCT_CUSTOMER_RATE)
                        dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Merge(dtProductData)
                    End If
                Else
                    'dsProduct = objProduct.pub_GetPortContractChargeDetailNew(intContractType)
                    'dtProductData = dsProduct.Tables(PortContractData._PORT_CONTRACT_CHARGE_DETAIL)
                End If
            End If
            CacheData(PRODUCT_CUSTOMER_RATE_TEMP, dsProduct)

            Dim drs() As DataRow = dtProductData.Select(String.Concat(ProductData.PRDCT_CSTMR_ID, "=", bv_i64ProductCustomerID))
            Dim dtCurrentData As DataTable = dtProductData.Clone

            If drs.Length > 0 Then
                dtCurrentData = drs.CopyToDataTable()
            End If

            ifgProductCleaningType.DataSource = dtCurrentData
            ifgProductCleaningType.DataBind()
            hdnProductCustomerID.Value = CStr(bv_i64ProductCustomerID)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GetSumTotalAmount"
                    pub_GetSumTotalAmount(e.GetCallbackValue("CustomerID"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                               MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pub_GetSumTotalAmount"
    Public Sub pub_GetSumTotalAmount(ByVal bv_i64CustomerID As Int64)
        Try

            dsProduct = CType(RetrieveData(PRODUCT_CUSTOMER_RATE_TEMP), ProductDataSet)
            dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Merge(CType(ifgProductCleaningType.DataSource, DataTable))
            CacheData(PRODUCT_CUSTOMER_RATE, dsProduct)
            CacheData(PRODUCT_CUSTOMER_RATE_TEMP, dsProduct)

            Dim dblCleaningRate As Double = 0
            Dim dtTemp As DataTable
            dtTemp = CType(ifgProductCleaningType.DataSource, DataTable)

            For Each drCustomerRate As DataRow In dtTemp.Select(String.Concat(ProductData.AMNT_NC, " IS NOT NULL"))
                dblCleaningRate += drCustomerRate.Item(ProductData.AMNT_NC)
            Next
            'If Not dtTemp Is Nothing Then
            '    dblCleaningRate = dtTemp.Compute("sum(AMNT_NC)", "")
            'Else
            '    dblCleaningRate = 0
            'End If

            Dim drDelete As DataRow()
            drDelete = dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Select(String.Concat(ProductData.PRDCT_CSTMR_ID, "=", bv_i64CustomerID))
            dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Merge(dtTemp)
            CacheData(PRODUCT_CUSTOMER_RATE, dsProduct)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("ProductCleaningRate", dblCleaningRate)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                           MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgProductCleaningType_ClientBind"

    Protected Sub ifgProductCleaningType_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgProductCleaningType.ClientBind
        Try
            Dim strProductID As String = e.Parameters("ProductID").ToString()
            Dim strProductCustomerID As String = e.Parameters("ProductCustomerID").ToString()
            Dim strCustomerID As String = e.Parameters("CustomerID").ToString()

            BindGridProductCustomer(strProductID, CommonUIs.iLng(strProductCustomerID), CommonUIs.iLng(strCustomerID))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                            MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "ifgProductCleaningType_RowInserting"
    Protected Sub ifgProductCleaningType_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgProductCleaningType.RowInserting
        Try
            Dim lngID As Long

            dsProduct = CType(RetrieveData(PRODUCT_CUSTOMER_RATE_TEMP), ProductDataSet)
            dtProductData = dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE)
            If dtProductData.Rows.Count = 0 AndAlso CType(ifgProductCleaningType.DataSource, DataTable).Rows.Count = 0 Then
                lngID = CommonWeb.GetNextIndex(dtProductData, ProductData.PRDCT_CSTMR_RT_ID)
            ElseIf dtProductData.Rows.Count > 0 AndAlso CType(ifgProductCleaningType.DataSource, DataTable).Rows.Count = 0 Then
                lngID = CommonWeb.GetNextIndex(dtProductData, ProductData.PRDCT_CSTMR_RT_ID) + 1
            Else
                lngID = CommonWeb.GetNextIndex(CType(ifgProductCleaningType.DataSource, DataTable), ProductData.PRDCT_CSTMR_RT_ID)

            End If

            e.Values(ProductData.PRDCT_CSTMR_RT_ID) = lngID
            e.Values(ProductData.PRDCT_CSTMR_ID) = CLng(e.InputParamters("ProductCustomerID"))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                             MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

    
#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Masters/CleaningTypePopup.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class