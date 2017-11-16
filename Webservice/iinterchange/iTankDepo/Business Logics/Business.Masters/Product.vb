Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class Product
#Region "pub_ProductCreateProduct() TABLE NAME:PRODUCT"

    <OperationContract()> _
    Public Function pub_CreateProduct(ByVal bv_strPRDCT_CD As String, _
     ByVal bv_strPRDCT_DSCRPTN_VC As String, _
     ByVal bv_strCHMCL_NM As String, _
     ByVal bv_strIMO_CLSS As String, _
     ByVal bv_strUN_NO As String, _
     ByVal bv_i64CLSSFCTN_ID As Int64, _
     ByVal bv_i64GRP_CLSSFCTN_ID As Int64, _
     ByVal bv_strRMRKS_VC As String, _
     ByRef bv_dblCLNNG_TTL_AMNT_NC As Double, _
     ByVal bv_blnCLNBL_BT As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_blnACTV_BT As Boolean, _
     ByVal bv_i32DPT_ID As Int32, _
     ByVal bv_strTypeID As String, _
     ByVal bv_strPageMode As String, _
     ByVal bv_strWfData As String, _
     ByRef br_dsProductDataset As ProductDataSet) As Long
        Dim objTransaction As New Transactions()
        Try
            Dim objProduct As New Products

            If br_dsProductDataset.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Rows.Count > 0 Then
                If Not IsDBNull(br_dsProductDataset.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Compute("sum(AMNT_NC)", "")) Then
                    bv_dblCLNNG_TTL_AMNT_NC = CDbl(br_dsProductDataset.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Compute("sum(AMNT_NC)", ""))
                Else
                    bv_dblCLNNG_TTL_AMNT_NC = 0
                End If
            Else
                bv_dblCLNNG_TTL_AMNT_NC = 0
            End If

            Dim lngCreated As Long
            lngCreated = objProduct.CreateProduct(bv_strPRDCT_CD, _
                                                  bv_strPRDCT_DSCRPTN_VC, _
                                                  bv_strCHMCL_NM, _
                                                  bv_strIMO_CLSS, _
                                                  bv_strUN_NO, _
                                                  bv_i64CLSSFCTN_ID, _
                                                  bv_i64GRP_CLSSFCTN_ID, _
                                                  bv_strRMRKS_VC, _
                                                  bv_dblCLNNG_TTL_AMNT_NC, _
                                                  bv_blnCLNBL_BT, _
                                                  bv_strCRTD_BY, _
                                                  bv_datCRTD_DT, _
                                                  bv_strMDFD_BY, _
                                                  bv_datMDFD_DT, _
                                                  bv_blnACTV_BT, _
                                                  bv_i32DPT_ID, _
                                                  bv_strTypeID, _
                                                  objTransaction)

            pub_UpdateCleaningRate(br_dsProductDataset, CLng(lngCreated), bv_strPageMode, objTransaction)
            pub_UpdateProductCustomer(br_dsProductDataset, CLng(lngCreated), bv_strPageMode, objTransaction)
            objProduct.DeleteProductAttachmentDetailByProductId(lngCreated, objTransaction)
            For Each drProductAttachment As DataRow In br_dsProductDataset.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Rows
                If drProductAttachment.RowState <> DataRowState.Deleted Then
                    objProduct.CreateProductAttachmentDetail(lngCreated, _
                                                             CStr(drProductAttachment.Item(ProductData.ATTCHMNT_PTH)), _
                                                             CStr(drProductAttachment.Item(ProductData.ACTL_FL_NM)), _
                                                             objTransaction)
                End If
            Next
            objTransaction.commit()
            Return lngCreated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region

#Region "pub_UpdateCleaningRate() TABLE NAME: PRODUCT_CLEANING_RATE"
    <OperationContract()> _
    Public Function pub_UpdateCleaningRate(ByRef dsProduct As ProductDataSet, _
                                           ByVal bv_ProductID As Long, _
                                           ByVal bv_strPageMode As String, _
                                           ByRef br_ObjTransactions As Transactions) As Boolean

        Try
            Dim dtProduct As New DataTable
            Dim ObjProducts As New Products
            Dim bolupdatebt As Boolean

            dtProduct = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE)
            For Each drProduct As DataRow In dtProduct.Rows
                Select Case drProduct.RowState
                    Case DataRowState.Added
                        Dim lngCreated As Long = ObjProducts.CreateCleaning_Rate(bv_ProductID, _
                                                                                 CommonUIs.iLng(drProduct.Item(ProductData.CLNNG_TYP_ID)), _
                                                                                 CommonUIs.iDec(drProduct.Item(ProductData.AMNT_NC)), _
                                                                                 br_ObjTransactions)
                        drProduct.Item(ProductData.PRDCT_CLNNG_RT_ID) = lngCreated
                    Case DataRowState.Modified
                        dtProduct = ObjProducts.pub_CheckProductCleaningRate(bv_ProductID, CommonUIs.iLng(drProduct.Item(ProductData.CLNNG_TYP_ID)), br_ObjTransactions).Tables(ProductData._PRODUCT_CLEANING_RATE)
                        If (dtProduct.Rows.Count = 0) Then
                            Dim lngCreated As Long = ObjProducts.CreateCleaning_Rate(bv_ProductID, _
                                                                                     CommonUIs.iLng(drProduct.Item(ProductData.CLNNG_TYP_ID)), _
                                                                                     CommonUIs.iDec(drProduct.Item(ProductData.AMNT_NC)), _
                                                                                     br_ObjTransactions)
                        Else
                            bolupdatebt = ObjProducts.UpdateCleaning_Rate(CInt(drProduct.Item(ProductData.PRDCT_CLNNG_RT_ID)), _
                                                                         bv_ProductID, _
                                                                         CommonUIs.iLng(drProduct.Item(ProductData.CLNNG_TYP_ID)), _
                                                                         CommonUIs.iDec(drProduct.Item(ProductData.AMNT_NC)), _
                                                                         br_ObjTransactions)
                        End If
                    Case (DataRowState.Deleted)
                        ObjProducts.DeleteProduct_Rate(CInt(drProduct.Item(ProductData.PRDCT_CLNNG_RT_ID, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next
            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateProductCustomer() TABLE NAME: PRODUCT_CUSTOMER"
    <OperationContract()> _
    Public Function pub_UpdateProductCustomer(ByRef bv_dsProduct As ProductDataSet, _
                                              ByVal bv_ProductID As Long, _
                                              ByVal bv_strPageMode As String, _
                                              ByRef br_ObjTransactions As Transactions) As Boolean

        Try
            Dim dtProduct As DataTable
            Dim ObjProducts As New Products
            Dim bolupdatebt As Boolean

            dtProduct = bv_dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER)
            For Each drCustomerProduct As DataRow In dtProduct.Rows
                Dim drCleaningCharges As DataRow()
                Select Case drCustomerProduct.RowState
                    Case DataRowState.Added
                        Dim lngCreated As Long = ObjProducts.CreateProduct_Customer(bv_ProductID, _
                                                                                    CommonUIs.iLng(drCustomerProduct.Item(ProductData.CSTMR_ID)), _
                                                                                    CommonUIs.iDec(drCustomerProduct.Item(ProductData.TTL_AMNT_NC)), _
                                                                                    br_ObjTransactions)
                        drCleaningCharges = bv_dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Select(String.Concat(ProductData.PRDCT_CSTMR_ID, "=", CommonUIs.iLng(drCustomerProduct.Item(ProductData.PRDCT_CSTMR_ID))))
                        If drCleaningCharges IsNot Nothing Then
                            pub_UpdateProductCustomerRate(drCleaningCharges, lngCreated, bv_ProductID, br_ObjTransactions)
                        End If
                    Case DataRowState.Modified
                        bolupdatebt = ObjProducts.UpdateProduct_Customer(CInt(drCustomerProduct.Item(ProductData.PRDCT_CSTMR_ID)), _
                                                                        bv_ProductID, _
                                                                        CommonUIs.iLng(drCustomerProduct.Item(ProductData.CSTMR_ID)), _
                                                                        CommonUIs.iDec(drCustomerProduct.Item(ProductData.TTL_AMNT_NC)), _
                                                                        br_ObjTransactions)
                        drCleaningCharges = bv_dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Select(String.Concat(ProductData.PRDCT_CSTMR_ID, "=", CommonUIs.iLng(drCustomerProduct.Item(ProductData.PRDCT_CSTMR_ID))))
                        If drCleaningCharges IsNot Nothing Then
                            pub_UpdateProductCustomerRate(drCleaningCharges, CInt(drCustomerProduct.Item(ProductData.PRDCT_CSTMR_ID)), bv_ProductID, br_ObjTransactions)
                        End If
                        For Each drCleaningCharge As DataRow In bv_dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Rows
                            If drCleaningCharge.RowState = DataRowState.Deleted Then
                                ObjProducts.DeleteProduct_Customer_Rate(CommonUIs.iLng(drCleaningCharge.Item(ProductData.PRDCT_CSTMR_RT_ID, DataRowVersion.Original)), br_ObjTransactions)
                            End If
                        Next

                    Case DataRowState.Deleted
                        ObjProducts.DeleteProduct_Customer_RateByProductCustomerId(CInt(drCustomerProduct.Item(ProductData.PRDCT_CSTMR_ID, DataRowVersion.Original)), br_ObjTransactions)
                        ObjProducts.DeleteProduct_Customer(CInt(drCustomerProduct.Item(ProductData.PRDCT_CSTMR_ID, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next
            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateProductCustomerRate() TABLE NAME: PRODUCT_CUSTOMER_RATE"
    <OperationContract()> _
    Public Function pub_UpdateProductCustomerRate(ByRef br_ProductCustomerRate As DataRow(), _
                                                  ByVal bv_ProductCustomerID As Long, _
                                                  ByVal bv_ProductID As Long, _
                                                  ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim ObjProducts As New Products
            Dim bolupdatebt As Boolean
            For Each drCustomerProductRate As DataRow In br_ProductCustomerRate
                Select Case drCustomerProductRate.RowState
                    Case DataRowState.Added
                        Dim lngProductCustomerRateID As Long = ObjProducts.CreateProduct_Customer_Rate(bv_ProductCustomerID, _
                                                                                                     bv_ProductID, _
                                                                                                     CommonUIs.iLng(drCustomerProductRate.Item(ProductData.CLNNG_TYP_ID)), _
                                                                                                     CommonUIs.iDec(drCustomerProductRate.Item(ProductData.AMNT_NC)), _
                                                                                                     br_ObjTransactions)

                        drCustomerProductRate.Item(ProductData.PRDCT_CSTMR_RT_ID) = lngProductCustomerRateID
                        drCustomerProductRate.Item(ProductData.PRDCT_CSTMR_ID) = bv_ProductCustomerID
                    Case DataRowState.Modified
                        bolupdatebt = ObjProducts.UpdateProduct_Customer_Rate(CommonUIs.iLng(drCustomerProductRate.Item(ProductData.PRDCT_CSTMR_RT_ID)), _
                                                                                             CommonUIs.iLng(drCustomerProductRate.Item(ProductData.PRDCT_CSTMR_ID)), _
                                                                                             bv_ProductID, _
                                                                                             CommonUIs.iLng(drCustomerProductRate.Item(ProductData.CLNNG_TYP_ID)), _
                                                                                             CommonUIs.iDec(drCustomerProductRate.Item(ProductData.AMNT_NC)), _
                                                                                             br_ObjTransactions)
                    Case DataRowState.Deleted
                        ObjProducts.DeleteProduct_Customer_Rate(CommonUIs.iLng(drCustomerProductRate.Item(ProductData.PRDCT_CSTMR_RT_ID, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ProductModifyProductProduct() TABLE NAME:PRODUCT"

    <OperationContract()> _
    Public Function pub_ModifyProduct(ByVal bv_i64PRDCT_ID As Int64, _
       ByVal bv_strPRDCT_CD As String, _
        ByVal bv_strPRDCT_DSCRPTN_VC As String, _
        ByVal bv_strCHMCL_NM As String, _
        ByVal bv_strIMO_CLSS As String, _
        ByVal bv_strUN_NO As String, _
        ByVal bv_i64CLSSFCTN_ID As Int64, _
        ByVal bv_i64GRP_CLSSFCTN_ID As Int64, _
        ByVal bv_strRMRKS_VC As String, _
        ByRef bv_dblCLNNG_TTL_AMNT_NC As Double, _
        ByVal bv_blnCLNBL_BT As Boolean, _
        ByVal bv_strCRTD_BY As String, _
        ByVal bv_datCRTD_DT As DateTime, _
        ByVal bv_strMDFD_BY As String, _
        ByVal bv_datMDFD_DT As DateTime, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_strTypeID As String, _
        ByVal bv_strPageMode As String, _
        ByVal bv_strWfData As String, _
        ByRef br_dsProductDataset As ProductDataSet) As Boolean
        Dim objTransaction As New Transactions()
        Try
            Dim objProduct As New Products
            Dim blnUpdated As Boolean
            Dim dtProductAttachDetail As New DataTable
            If br_dsProductDataset.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Rows.Count > 0 Then
                If Not IsDBNull(br_dsProductDataset.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Compute("sum(AMNT_NC)", "")) Then
                    bv_dblCLNNG_TTL_AMNT_NC = CDbl(br_dsProductDataset.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Compute("sum(AMNT_NC)", ""))
                Else
                    bv_dblCLNNG_TTL_AMNT_NC = 0
                End If
            Else
                bv_dblCLNNG_TTL_AMNT_NC = 0
            End If
            'dtProductAttachDetail = objProduct.GetProductAttachmentDetailByProductId(bv_i64PRDCT_ID).Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL)
            'br_dsProductDataset.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Merge(dtProductAttachDetail)
            blnUpdated = objProduct.UpdateProduct(bv_i64PRDCT_ID, _
                                                  bv_strPRDCT_CD, _
                                                  bv_strPRDCT_DSCRPTN_VC, _
                                                  bv_strCHMCL_NM, _
                                                  bv_strIMO_CLSS, _
                                                  bv_strUN_NO, _
                                                  bv_i64CLSSFCTN_ID, _
                                                  bv_i64GRP_CLSSFCTN_ID, _
                                                  bv_strRMRKS_VC, _
                                                  bv_dblCLNNG_TTL_AMNT_NC, _
                                                  bv_blnCLNBL_BT, _
                                                  bv_strCRTD_BY, _
                                                  bv_datCRTD_DT, _
                                                  bv_strMDFD_BY, _
                                                  bv_datMDFD_DT, _
                                                  bv_blnACTV_BT, _
                                                  bv_i32DPT_ID, _
                                                  bv_strTypeID, _
                                                  objTransaction)
            pub_UpdateCleaningRate(br_dsProductDataset, bv_i64PRDCT_ID, bv_strPageMode, objTransaction)
            pub_UpdateProductCustomer(br_dsProductDataset, bv_i64PRDCT_ID, bv_strPageMode, objTransaction)
            objProduct.DeleteProductAttachmentDetailByProductId(bv_i64PRDCT_ID, objTransaction)
            For Each drProductAttachment As DataRow In br_dsProductDataset.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Rows
                If drProductAttachment.RowState <> DataRowState.Deleted Then
                    objProduct.CreateProductAttachmentDetail(bv_i64PRDCT_ID, _
                                                    CStr(drProductAttachment.Item(ProductData.ATTCHMNT_PTH)), _
                                                    CStr(drProductAttachment.Item(ProductData.ACTL_FL_NM)), _
                                                    objTransaction)
                End If
            Next
            objTransaction.commit()
            Return blnUpdated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region

#Region "pub_GetCleaningType() TABLE NAME:PRODUCT_CLEANING_RATE"
    <OperationContract()> _
    Public Function pub_GetCleaningType(ByVal bv_intDepotID As Integer) As ProductDataSet
        Try
            Dim dsProductDataSet As ProductDataSet
            Dim objProducts As New Products
            dsProductDataSet = objProducts.GetCleaningType(bv_intDepotID)
            Return dsProductDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCleaninTypeByProductID() TABLE NAME:PRODUCT_CLEANING_RATE"
    <OperationContract()> _
    Public Function pub_GetCleaninTypeByProductID(ByVal bv_i32ProductID As Int64) As ProductDataSet

        Try
            Dim dsProductDataSet As ProductDataSet
            Dim objProducts As New Products
            dsProductDataSet = objProducts.pub_GetCleaninTypeByProductID(bv_i32ProductID)
            Return dsProductDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetProductByProductCode() TABLE NAME:PRODUCT"

    <OperationContract()> _
    Public Function pub_GetProductByProductCode(ByVal bv_strProductCode As String) As ProductDataSet
        Try
            Dim dsProductData As ProductDataSet
            Dim objProducts As New Products
            dsProductData = objProducts.pub_GetProductByProductCode(bv_strProductCode)
            Return dsProductData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetProductRateByProductID() TABLE NAME:PRODUCT_CLEANING_RATE"
    <OperationContract()> _
    Public Function pub_GetCleaningRateByProductID(ByVal bv_i32ProductID As Int32) As ProductDataSet
        Try
            Dim dsProductDataSet As ProductDataSet
            Dim objProducts As New Products
            dsProductDataSet = objProducts.pub_GetCleaningRateByProductID(bv_i32ProductID)
            Return dsProductDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCleaningTypeByProductID() VIEW NAME:V_PRODUCT_CLEANING_RATE"

    <OperationContract()> _
    Public Function pub_GetCleaningTypeByProductID(ByVal bv_i64ClngTypeID As Int64, ByVal bv_i64PdtID As Int64) As ProductDataSet
        Try
            Dim dsProductData As ProductDataSet
            Dim objItems As New Products
            dsProductData = objItems.pub_GetCleaningTypeByProductID(bv_i64ClngTypeID, bv_i64PdtID)
            Return dsProductData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCleaningTypeGridByProductID() VIEW NAME:V_PRODUCT_CLEANING_RATE AND CLEANING_TYPE"

    Public Function pub_GetCleaningTypeGridByProductID(ByRef br_dsProduct As ProductDataSet, ByVal bv_i64PdtID As Int64) As Boolean
        Try
            Dim dsProductData As ProductDataSet
            Dim objItems As New Products
            dsProductData = objItems.pub_GetCleaningTypeGridByProductID(bv_i64PdtID)

            br_dsProduct.Merge(dsProductData.Tables(ProductData._V_PRODUCT_CLEANING_RATE))
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetProductCustomerByProductID() TABLE NAME:PRODUCT_CUSTOMER"
    <OperationContract()> _
    Public Function pub_GetProductCustomerByProductID(ByVal bv_i64ProductID As Int64) As ProductDataSet
        Try
            Dim dsProductDataSet As ProductDataSet
            Dim objProducts As New Products
            dsProductDataSet = objProducts.pub_GetProductCustomerByProductID(bv_i64ProductID)
            Return dsProductDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetProductCustomerRateBy_ProductID() TABLE NAME:PRODUCT_CUSTOMER_RATE"
    <OperationContract()> _
    Public Function pub_GetProductCustomerRateBy_ProductID(ByVal bv_i64PrdctID As Int64, ByVal BV_I64PrdctCstmrID As Int64, ByVal bv_int64CustomerID As Int64) As ProductDataSet
        Try
            Dim dsProductDataSet As ProductDataSet
            Dim objProducts As New Products
            dsProductDataSet = objProducts.pub_GetProductCustomerRateBy_ProductID(bv_i64PrdctID, BV_I64PrdctCstmrID, bv_int64CustomerID)
            Return dsProductDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetProductCustomerByProductCustomerID() TABLE NAME:PRODUCT_CUSTOMER"
    <OperationContract()> _
    Public Function pub_GetProductCustomerByProductCustomerID(ByVal bv_i64ProductCustomerID As Int64, ByVal bv_intDepotID As Int64) As ProductDataSet
        Try
            Dim dsProductDataSet As ProductDataSet
            Dim objProducts As New Products
            objProducts.GetProductCustomerRateByProductCustomerID(bv_i64ProductCustomerID, bv_intDepotID)
            dsProductDataSet = objProducts.GetProductCustomerByProductCustomerID(bv_i64ProductCustomerID, bv_intDepotID)
            Return dsProductDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetProductAttachmentDetailByProductId() TABLE NAME: PRODUCT_ATTACHMENT_DETAIL"
    Public Function pub_GetProductAttachmentDetailByProductId(ByVal bv_lngProductId As Long) As ProductDataSet
        Try
            Dim dsProductDataSet As ProductDataSet
            Dim objProducts As New Products
            dsProductDataSet = objProducts.GetProductAttachmentDetailByProductId(bv_lngProductId)
            Return dsProductDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class
