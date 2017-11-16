Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Products"

Public Class Products

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const ProductSelectQueryPk As String = "SELECT PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CHMCL_NM,IMO_CLSS,UN_NO,CLSSFCTN_ID,GRP_CLSSFCTN_ID,RMRKS_VC,CLNNG_TTL_AMNT_NC,CLNBL_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM PRODUCT WHERE PRDCT_ID=@PRDCT_ID"
    Private Const ProductInsertQuery As String = "INSERT INTO PRODUCT(PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CHMCL_NM,IMO_CLSS,UN_NO,CLSSFCTN_ID,GRP_CLSSFCTN_ID,RMRKS_VC,CLNNG_TTL_AMNT_NC,CLNBL_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,CLNNG_MTHD_TYP_ID)VALUES(@PRDCT_ID,@PRDCT_CD,@PRDCT_DSCRPTN_VC,@CHMCL_NM,@IMO_CLSS,@UN_NO,@CLSSFCTN_ID,@GRP_CLSSFCTN_ID,@RMRKS_VC,@CLNNG_TTL_AMNT_NC,@CLNBL_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID,@CLNNG_MTHD_TYP_ID)"
    Private Const ProductUpdateQuery As String = "UPDATE Product SET PRDCT_ID=@PRDCT_ID, PRDCT_CD=@PRDCT_CD, PRDCT_DSCRPTN_VC=@PRDCT_DSCRPTN_VC, CHMCL_NM=@CHMCL_NM, IMO_CLSS=@IMO_CLSS, UN_NO=@UN_NO, CLSSFCTN_ID=@CLSSFCTN_ID, GRP_CLSSFCTN_ID=@GRP_CLSSFCTN_ID, RMRKS_VC=@RMRKS_VC, CLNNG_TTL_AMNT_NC=@CLNNG_TTL_AMNT_NC, CLNBL_BT=@CLNBL_BT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID,CLNNG_MTHD_TYP_ID=@CLNNG_MTHD_TYP_ID WHERE PRDCT_ID=@PRDCT_ID"
    Private Const ProductDeleteQuery As String = "DELETE FROM Product WHERE PRDCT_ID=@PRDCT_ID"
    Private Const CleaningTypeSelectQuery As String = "SELECT CLNNG_TYP_ID AS PRDCT_CLNNG_RT_ID,CLNNG_TYP_ID,CLNNG_TYP_CD,CLNNG_TYP_DSCRPTN_VC,DFLT_BT FROM CLEANING_TYPE WHERE DPT_ID=@DPT_ID AND DFLT_BT=1 AND ACTV_BT = 1"
    Private Const V_Product_CleaningRateSelectQueryByProductID As String = "SELECT PRDCT_CLNNG_RT_ID,CLNNG_TYP_ID,DFLT_BT,AMNT_NC,CLNNG_TYP_CD,CLNNG_TYP_DSCRPTN_VC FROM V_PRODUCT_CLEANING_RATE WHERE PRDCT_ID=@PRDCT_ID"
    Private Const V_ProductSelectQueryByProductCode As String = "SELECT PRDCT_CD,PRDCT_DSCRPTN_VC,CHMCL_NM,IMO_CLSS,UN_NO,CLSSFCTN_ID,GRP_CLSSFCTN_ID,RMRKS_VC,CLNNG_TTL_AMNT_NC,CLNBL_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM V_PRODUCT WHERE PRDCT_CD=@PRDCT_CD"
    Private Const V_ProductSelectQueryByProductID As String = "SELECT PRDCT_CD,PRDCT_DSCRPTN_VC,CHMCL_NM,IMO_CLSS,UN_NO,CLSSFCTN_ID,GRP_CLSSFCTN_ID,RMRKS_VC,CLNNG_TTL_AMNT_NC,CLNBL_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM V_PRODUCT WHERE PRDCT_ID=@PRDCT_ID"
    Private Const Cleaning_RateInsertQuery As String = "INSERT INTO PRODUCT_CLEANING_RATE(PRDCT_CLNNG_RT_ID,PRDCT_ID,CLNNG_TYP_ID,AMNT_NC)VALUES (@PRDCT_CLNNG_RT_ID,@PRDCT_ID,@CLNNG_TYP_ID,@AMNT_NC)"
    Private Const Cleaning_RateUpdateQuery As String = "UPDATE PRODUCT_CLEANING_RATE SET PRDCT_CLNNG_RT_ID=@PRDCT_CLNNG_RT_ID,PRDCT_ID=@PRDCT_ID,CLNNG_TYP_ID=@CLNNG_TYP_ID,AMNT_NC=@AMNT_NC WHERE PRDCT_CLNNG_RT_ID=@PRDCT_CLNNG_RT_ID"
    Private Const Cleaning_RateDeleteQuery As String = "DELETE FROM PRODUCT_CLEANING_RATE WHERE PRDCT_CLNNG_RT_ID=@PRDCT_CLNNG_RT_ID"
    Private Const V_Product_Cleaning_RateSelectQuery As String = "SELECT PRDCT_CLNNG_RT_ID,PRDCT_ID,CLNNG_TYP_ID,AMNT_NC FROM V_PRODUCT_CLEANING_RATE WHERE PRDCT_ID=@PRDCT_ID AND CLNNG_TYP_ID=@CLNNG_TYP_ID"
    Private Const V_Product_cleaning_Type_GridQuery As String = "SELECT CLNNG_TYP_CD,CLNNG_TYP_ID,CLNNG_TYP_ID AS PRDCT_CLNNG_RT_ID, 'True' AS DFLT_BT FROM CLEANING_TYPE WHERE DFLT_BT=1 AND ACTV_BT = 1 AND CLNNG_TYP_CD NOT IN (SELECT CLNNG_TYP_CD FROM V_PRODUCT_CLEANING_RATE WHERE PRDCT_ID=@PRDCT_ID)"
    Private Const Product_Cleaning_RateSelectQuery As String = "SELECT PRDCT_CLNNG_RT_ID,PRDCT_ID,CLNNG_TYP_ID,AMNT_NC FROM PRODUCT_CLEANING_RATE WHERE PRDCT_ID=@PRDCT_ID AND CLNNG_TYP_ID=@CLNNG_TYP_ID"
    Private Const V_Product_CustomerSelectQueryByProductID As String = "SELECT PRDCT_CSTMR_ID,PRDCT_ID,PRDCT_CD,CSTMR_ID,CSTMR_CD,TTL_AMNT_NC FROM V_PRODUCT_CUSTOMER WHERE PRDCT_ID=@PRDCT_ID"
    Private Const V_Product_Customer_RateSelectQueryByProductID As String = "SELECT PRDCT_CSTMR_RT_ID,PRDCT_CSTMR_ID,CLNNG_TYP_ID,CLNNG_TYP_CD,AMNT_NC,PRDCT_ID FROM V_PRODUCT_CUSTOMER_RATE WHERE PRDCT_CSTMR_ID=@PRDCT_CSTMR_ID AND PRDCT_ID=@PRDCT_ID"
    Private Const Product_Customer_InsertQuery As String = "INSERT INTO PRODUCT_CUSTOMER(PRDCT_CSTMR_ID,PRDCT_ID,CSTMR_ID,TTL_AMNT_NC) VALUES (@PRDCT_CSTMR_ID,@PRDCT_ID,@CSTMR_ID,@TTL_AMNT_NC)"
    Private Const Product_CustomerUpdateQuery As String = "UPDATE PRODUCT_CUSTOMER SET PRDCT_CSTMR_ID=@PRDCT_CSTMR_ID,PRDCT_ID=@PRDCT_ID,CSTMR_ID=@CSTMR_ID,TTL_AMNT_NC=@TTL_AMNT_NC WHERE PRDCT_CSTMR_ID=@PRDCT_CSTMR_ID"
    Private Const Product_CustomerDeleteQuery As String = "DELETE FROM PRODUCT_CUSTOMER WHERE PRDCT_CSTMR_ID=@PRDCT_CSTMR_ID"
    Private Const Product_Customer_Rate_InsertQuery As String = "INSERT INTO PRODUCT_CUSTOMER_RATE(PRDCT_CSTMR_RT_ID,PRDCT_CSTMR_ID,PRDCT_ID,CLNNG_TYP_ID,AMNT_NC) VALUES (@PRDCT_CSTMR_RT_ID,@PRDCT_CSTMR_ID,@PRDCT_ID,@CLNNG_TYP_ID,@AMNT_NC)"
    Private Const Product_Customer_Rate_UpdateQuery As String = "UPDATE PRODUCT_CUSTOMER_RATE SET PRDCT_CSTMR_RT_ID=@PRDCT_CSTMR_RT_ID,PRDCT_CSTMR_ID=@PRDCT_CSTMR_ID,PRDCT_ID=@PRDCT_ID,CLNNG_TYP_ID=@CLNNG_TYP_ID,AMNT_NC=@AMNT_NC WHERE PRDCT_CSTMR_RT_ID=@PRDCT_CSTMR_RT_ID"
    Private Const Product_Customer_RateDeleteQuery As String = "DELETE FROM PRODUCT_CUSTOMER_RATE WHERE PRDCT_CSTMR_RT_ID=@PRDCT_CSTMR_RT_ID"
    Private Const Product_Customer_RateDeleteQueryByProductCustomerId As String = "DELETE FROM PRODUCT_CUSTOMER_RATE WHERE PRDCT_CSTMR_ID=@PRDCT_CSTMR_ID"
    Private Const V_Product_CustomerSelectQueryByProductCustomerID As String = "SELECT PRDCT_CSTMR_ID,PRDCT_ID,PRDCT_CD,CSTMR_ID,CSTMR_CD,TTL_AMNT_NC FROM V_PRODUCT_CUSTOMER WHERE PRDCT_CSTMR_ID=@PRDCT_CSTMR_ID"
    Private Const V_Product_Customer_RateSelectQueryByProductCustomerID As String = "SELECT PRDCT_CSTMR_RT_ID,PRDCT_CSTMR_ID,CLNNG_TYP_ID,CLNNG_TYP_CD,AMNT_NC,PRDCT_ID FROM V_PRODUCT_CUSTOMER_RATE WHERE PRDCT_CSTMR_ID=@PRDCT_CSTMR_ID"
    Private Const ProductAttachmentDetailInsertQuery As String = "INSERT INTO PRODUCT_ATTACHMENT_DETAIL (PRDCT_ATTCHMNT_DTL_ID,PRDCT_ID,ATTCHMNT_PTH,ACTL_FL_NM) VALUES(@PRDCT_ATTCHMNT_DTL_ID,@PRDCT_ID,@ATTCHMNT_PTH,@ACTL_FL_NM)"
    Private Const ProductAttachmentDetailDeleteQuery As String = "DELETE FROM PRODUCT_ATTACHMENT_DETAIL WHERE PRDCT_ID=@PRDCT_ID"
    Private Const ProductAttachmentDetailSelectQuery As String = "SELECT PRDCT_ATTCHMNT_DTL_ID,PRDCT_ID,ATTCHMNT_PTH,ACTL_FL_NM FROM PRODUCT_ATTACHMENT_DETAIL WHERE PRDCT_ID=@PRDCT_ID"
    Private ds As ProductDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New ProductDataSet
    End Sub

#End Region

#Region "CreateProduct() TABLE NAME:Product"

    Public Function CreateProduct(ByVal bv_strPRDCT_CD As String, _
                                  ByVal bv_strPRDCT_DSCRPTN_VC As String, _
                                  ByVal bv_strCHMCL_NM As String, _
                                  ByVal bv_strIMO_CLSS As String, _
                                  ByVal bv_strUN_NO As String, _
                                  ByVal bv_i64CLSSFCTN_ID As Int64, _
                                  ByVal bv_i64GRP_CLSSFCTN_ID As Int64, _
                                  ByVal bv_strRMRKS_VC As String, _
                                  ByVal bv_dblCLNNG_TTL_AMNT_NC As Double, _
                                  ByVal bv_blnCLNBL_BT As Boolean, _
                                  ByVal bv_strCRTD_BY As String, _
                                  ByVal bv_datCRTD_DT As DateTime, _
                                  ByVal bv_strMDFD_BY As String, _
                                  ByVal bv_datMDFD_DT As DateTime, _
                                  ByVal bv_blnACTV_BT As Boolean, _
                                  ByVal bv_i32DPT_ID As Int32, _
                                  ByVal bv_strTypeID As String, _
                                  ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ProductData._PRODUCT).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ProductData._PRODUCT, br_ObjTransactions)
                .Item(ProductData.PRDCT_ID) = intMax
                .Item(ProductData.PRDCT_CD) = bv_strPRDCT_CD
                .Item(ProductData.PRDCT_DSCRPTN_VC) = bv_strPRDCT_DSCRPTN_VC
                .Item(ProductData.CHMCL_NM) = bv_strCHMCL_NM
                .Item(ProductData.IMO_CLSS) = bv_strIMO_CLSS
                If bv_i64CLSSFCTN_ID <> 0 Then
                    .Item(ProductData.CLSSFCTN_ID) = bv_i64CLSSFCTN_ID
                Else
                    .Item(ProductData.CLSSFCTN_ID) = DBNull.Value
                End If
                .Item(ProductData.UN_NO) = bv_strUN_NO
                If bv_i64GRP_CLSSFCTN_ID <> Nothing Then
                    .Item(ProductData.GRP_CLSSFCTN_ID) = bv_i64GRP_CLSSFCTN_ID
                Else
                    .Item(ProductData.GRP_CLSSFCTN_ID) = DBNull.Value
                End If

                If bv_strRMRKS_VC <> Nothing Then
                    .Item(ProductData.RMRKS_VC) = bv_strRMRKS_VC
                Else
                    .Item(ProductData.RMRKS_VC) = DBNull.Value
                End If
                .Item(ProductData.CLNNG_TTL_AMNT_NC) = bv_dblCLNNG_TTL_AMNT_NC
                .Item(ProductData.CLNBL_BT) = bv_blnCLNBL_BT
                .Item(ProductData.CRTD_BY) = bv_strCRTD_BY
                .Item(ProductData.CRTD_DT) = bv_datCRTD_DT
                .Item(ProductData.MDFD_BY) = bv_strMDFD_BY
                .Item(ProductData.MDFD_DT) = bv_datMDFD_DT
                .Item(ProductData.ACTV_BT) = bv_blnACTV_BT
                .Item(ProductData.DPT_ID) = bv_i32DPT_ID

                If bv_strTypeID <> Nothing Then
                    .Item(ProductData.CLNNG_MTHD_TYP_ID) = CLng(bv_strTypeID)
                Else
                    .Item(ProductData.CLNNG_MTHD_TYP_ID) = DBNull.Value
                End If

            End With
            objData.InsertRow(dr, ProductInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateProduct = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateProduct() TABLE NAME:Product"

    Public Function UpdateProduct(ByVal bv_i64PRDCT_ID As Int64, _
                                 ByVal bv_strPRDCT_CD As String, _
                                 ByVal bv_strPRDCT_DSCRPTN_VC As String, _
                                 ByVal bv_strCHMCL_NM As String, _
                                 ByVal bv_strIMO_CLSS As String, _
                                 ByVal bv_strUN_NO As String, _
                                 ByVal bv_i64CLSSFCTN_ID As Int64, _
                                 ByVal bv_i64GRP_CLSSFCTN_ID As Int64, _
                                 ByVal bv_strRMRKS_VC As String, _
                                 ByVal bv_dblCLNNG_TTL_AMNT_NC As Double, _
                                 ByVal bv_blnCLNBL_BT As Boolean, _
                                 ByVal bv_strCRTD_BY As String, _
                                 ByVal bv_datCRTD_DT As DateTime, _
                                 ByVal bv_strMDFD_BY As String, _
                                 ByVal bv_datMDFD_DT As DateTime, _
                                 ByVal bv_blnACTV_BT As Boolean, _
                                 ByVal bv_i32DPT_ID As Int32, _
                                 ByVal bv_strTypeID As String, _
                                 ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ProductData._PRODUCT).NewRow()
            With dr
                .Item(ProductData.PRDCT_ID) = bv_i64PRDCT_ID
                .Item(ProductData.PRDCT_CD) = bv_strPRDCT_CD
                .Item(ProductData.PRDCT_DSCRPTN_VC) = bv_strPRDCT_DSCRPTN_VC
                .Item(ProductData.CHMCL_NM) = bv_strCHMCL_NM
                .Item(ProductData.IMO_CLSS) = bv_strIMO_CLSS
                .Item(ProductData.UN_NO) = bv_strUN_NO

                If bv_i64CLSSFCTN_ID <> Nothing Then
                    .Item(ProductData.CLSSFCTN_ID) = bv_i64CLSSFCTN_ID
                Else
                    .Item(ProductData.CLSSFCTN_ID) = DBNull.Value
                End If
                If bv_i64GRP_CLSSFCTN_ID <> Nothing Then
                    .Item(ProductData.GRP_CLSSFCTN_ID) = bv_i64GRP_CLSSFCTN_ID
                Else
                    .Item(ProductData.GRP_CLSSFCTN_ID) = DBNull.Value
                End If
                .Item(ProductData.RMRKS_VC) = bv_strRMRKS_VC
                If bv_dblCLNNG_TTL_AMNT_NC <> Nothing And bv_dblCLNNG_TTL_AMNT_NC <> 0 Then
                    .Item(ProductData.CLNNG_TTL_AMNT_NC) = bv_dblCLNNG_TTL_AMNT_NC
                Else
                    .Item(ProductData.CLNNG_TTL_AMNT_NC) = 0
                End If

                .Item(ProductData.CLNBL_BT) = bv_blnCLNBL_BT
                .Item(ProductData.CRTD_BY) = bv_strCRTD_BY
                .Item(ProductData.CRTD_DT) = bv_datCRTD_DT
                .Item(ProductData.MDFD_BY) = bv_strMDFD_BY
                .Item(ProductData.MDFD_DT) = bv_datMDFD_DT
                .Item(ProductData.ACTV_BT) = bv_blnACTV_BT
                .Item(ProductData.DPT_ID) = bv_i32DPT_ID
                If bv_strTypeID <> Nothing Then
                    .Item(ProductData.CLNNG_MTHD_TYP_ID) = CLng(bv_strTypeID)
                Else
                    .Item(ProductData.CLNNG_MTHD_TYP_ID) = DBNull.Value
                End If
            End With
            UpdateProduct = objData.UpdateRow(dr, ProductUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteProduct() TABLE NAME:Product"

    Public Function DeleteProduct(ByVal bv_i32 As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(ProductData._PRODUCT).NewRow()
            With dr
                .Item(ProductData.PRDCT_ID) = bv_i32
            End With
            DeleteProduct = objData.DeleteRow(dr, ProductDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningType() TABLE NAME:CLEANING_TYPE"

    Public Function GetCleaningType(ByVal bv_intDepotID As Integer) As ProductDataSet
        Try
            objData = New DataObjects(CleaningTypeSelectQuery, ProductData.DPT_ID, CStr(bv_intDepotID))
            objData.Fill(CType(ds, DataSet), ProductData._V_PRODUCT_CLEANING_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetCleaninTypeByProductID() TABLE NAME:V_PRODUCT_CLEANING_RATE"

    Public Function pub_GetCleaninTypeByProductID(ByVal bv_i64PRDTID As Int64) As ProductDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(ProductData.PRDCT_ID, bv_i64PRDTID)
            objData = New DataObjects(V_Product_CleaningRateSelectQueryByProductID, hshTable)
            objData.Fill(CType(ds, DataSet), ProductData._V_PRODUCT_CLEANING_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetProductByProductCode() TABLE NAME:Product"

    Public Function pub_GetProductByProductCode(ByVal bv_strPRDT_CD As String) As ProductDataSet
        Try
            objData = New DataObjects(V_ProductSelectQueryByProductCode, ProductData.PRDCT_CD, bv_strPRDT_CD)
            objData.Fill(CType(ds, DataSet), ProductData._PRODUCT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetCleaningRateByProductID() TABLE NAME:V_PRODUCT_CLEANING_RATE"

    Public Function pub_GetCleaningRateByProductID(ByVal bv_i64PRDT_ID As Int64) As ProductDataSet
        Try
            objData = New DataObjects(V_ProductSelectQueryByProductID, ProductData.PRDCT_ID, CStr(bv_i64PRDT_ID))
            objData.Fill(CType(ds, DataSet), ProductData._V_PRODUCT_CLEANING_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateCleaning_Rate() TABLE NAME:PRODUCT_CLEANING_RATE"

    Public Function CreateCleaning_Rate(ByVal bv_i64PRDCT_ID As Int64, _
                                        ByVal bv_i64CLNNG_TYP_ID As Int64, _
                                        ByVal bv_i64AMNT_NC As Decimal, _
                                        ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ProductData._PRODUCT_CLEANING_RATE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ProductData._PRODUCT_CLEANING_RATE, br_ObjTransactions)
                .Item(ProductData.PRDCT_CLNNG_RT_ID) = intMax
                .Item(ProductData.PRDCT_ID) = bv_i64PRDCT_ID
                .Item(ProductData.CLNNG_TYP_ID) = bv_i64CLNNG_TYP_ID
                .Item(ProductData.AMNT_NC) = bv_i64AMNT_NC
            End With
            objData.InsertRow(dr, Cleaning_RateInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateCleaning_Rate = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateCleaning_Rate() TABLE NAME:PRODUCT_CLEANING_RATE"

    Public Function UpdateCleaning_Rate(ByVal bv_i64PRDCT_CLNNG_RT_ID As Int64, _
                                        ByVal bv_i64PRDCT_ID As Int64, _
                                        ByVal bv_i64CLNNG_TYP_ID As Int64, _
                                        ByVal bv_i64AMNT_NC As Decimal, _
                                        ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ProductData._V_PRODUCT_CLEANING_RATE).NewRow()
            With dr
                .Item(ProductData.PRDCT_CLNNG_RT_ID) = bv_i64PRDCT_CLNNG_RT_ID
                .Item(ProductData.PRDCT_ID) = bv_i64PRDCT_ID
                .Item(ProductData.CLNNG_TYP_ID) = bv_i64CLNNG_TYP_ID
                .Item(ProductData.AMNT_NC) = bv_i64AMNT_NC
            End With
            UpdateCleaning_Rate = objData.UpdateRow(dr, Cleaning_RateUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateProduct_Customer() TABLE NAME:PRODUCT_CUSTOMER"

    Public Function CreateProduct_Customer(ByVal bv_i64PRDCT_ID As Int64, _
                                           ByVal bv_i64CSTMR_ID As Int64, _
                                           ByVal bv_decTTL_AMNT_NC As Decimal, _
                                           ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ProductData._PRODUCT_CUSTOMER).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ProductData._PRODUCT_CUSTOMER, br_ObjTransactions)
                .Item(ProductData.PRDCT_CSTMR_ID) = intMax
                .Item(ProductData.PRDCT_ID) = bv_i64PRDCT_ID
                .Item(ProductData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(ProductData.TTL_AMNT_NC) = bv_decTTL_AMNT_NC
            End With
            objData.InsertRow(dr, Product_Customer_InsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateProduct_Customer = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateProduct_Customer_Rate() TABLE NAME:PRODUCT_CUSTOMER_RATE"

    Public Function CreateProduct_Customer_Rate(ByVal bv_i64PRDCT_CSTMR_ID As Int64, _
                                                ByVal bv_i64PRDCT_ID As Int64, _
                                                ByVal bv_i64CLNNG_TYP_ID As Int64, _
                                                ByVal bv_decAMNT_NC As Decimal, _
                                                ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ProductData._PRODUCT_CUSTOMER_RATE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ProductData._PRODUCT_CUSTOMER_RATE, br_ObjTransactions)
                .Item(ProductData.PRDCT_CSTMR_RT_ID) = intMax
                .Item(ProductData.PRDCT_CSTMR_ID) = bv_i64PRDCT_CSTMR_ID
                .Item(ProductData.PRDCT_ID) = bv_i64PRDCT_ID
                .Item(ProductData.CLNNG_TYP_ID) = bv_i64CLNNG_TYP_ID
                .Item(ProductData.AMNT_NC) = bv_decAMNT_NC
            End With
            objData.InsertRow(dr, Product_Customer_Rate_InsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateProduct_Customer_Rate = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateProduct_Customer() TABLE NAME:PRODUCT_CUSTOMER"

    Public Function UpdateProduct_Customer(ByVal bv_i64PRDCT_CSTMR_ID As Int64, _
                                           ByVal bv_i64PRDCT_ID As Int64, _
                                           ByVal bv_i64CSTMR_ID As Int64, _
                                           ByVal bv_i64TTL_AMNT_NC As Decimal, _
                                           ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ProductData._PRODUCT_CUSTOMER).NewRow()
            With dr
                .Item(ProductData.PRDCT_CSTMR_ID) = bv_i64PRDCT_CSTMR_ID
                .Item(ProductData.PRDCT_ID) = bv_i64PRDCT_ID
                .Item(ProductData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(ProductData.TTL_AMNT_NC) = bv_i64TTL_AMNT_NC
            End With
            UpdateProduct_Customer = objData.UpdateRow(dr, Product_CustomerUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateProduct_Customer_Rate() TABLE NAME:PRODUCT_CUSTOMER_RATE"

    Public Function UpdateProduct_Customer_Rate(ByVal bv_i64PRDCT_CSTMR_RT_ID As Int64, _
                                                ByVal bv_i64PRDCT_CSTMR_ID As Int64, _
                                                ByVal bv_i64PRDCT_ID As Int64, _
                                                ByVal bv_i64CLNNG_TYP_ID As Int64, _
                                                ByVal bv_i64AMNT_NC As Decimal, _
                                                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ProductData._PRODUCT_CUSTOMER_RATE).NewRow()
            With dr
                .Item(ProductData.PRDCT_CSTMR_RT_ID) = bv_i64PRDCT_CSTMR_RT_ID
                .Item(ProductData.PRDCT_CSTMR_ID) = bv_i64PRDCT_CSTMR_ID
                .Item(ProductData.PRDCT_ID) = bv_i64PRDCT_ID
                .Item(ProductData.CLNNG_TYP_ID) = bv_i64CLNNG_TYP_ID
                .Item(ProductData.AMNT_NC) = bv_i64AMNT_NC
            End With
            UpdateProduct_Customer_Rate = objData.UpdateRow(dr, Product_Customer_Rate_UpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteProduct_Customer() TABLE NAME:PRODUCT_CUSTOMER"

    Public Function DeleteProduct_Customer(ByVal bv_i64PRDCT_CSTMR_ID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(ProductData._PRODUCT_CUSTOMER).NewRow()
            With dr
                .Item(ProductData.PRDCT_CSTMR_ID) = bv_i64PRDCT_CSTMR_ID
            End With
            DeleteProduct_Customer = objData.DeleteRow(dr, Product_CustomerDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteProduct_Customer_Rate() TABLE NAME:PRODUCT_CUSTOMER_RATE"

    Public Function DeleteProduct_Customer_Rate(ByVal bv_i64PRDCT_CSTMR_RT_ID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(ProductData._PRODUCT_CUSTOMER_RATE).NewRow()
            With dr
                .Item(ProductData.PRDCT_CSTMR_RT_ID) = bv_i64PRDCT_CSTMR_RT_ID
            End With
            DeleteProduct_Customer_Rate = objData.DeleteRow(dr, Product_Customer_RateDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteProduct_Customer_Rate() TABLE NAME:PRODUCT_CUSTOMER_RATE"

    Public Function DeleteProduct_Customer_RateByProductCustomerId(ByVal bv_i64PRDCT_CSTMR_ID As Int64, _
                                                                   ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(ProductData._PRODUCT_CUSTOMER_RATE).NewRow()
            With dr
                .Item(ProductData.PRDCT_CSTMR_ID) = bv_i64PRDCT_CSTMR_ID
            End With
            DeleteProduct_Customer_RateByProductCustomerId = objData.DeleteRow(dr, Product_Customer_RateDeleteQueryByProductCustomerId, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteProduct_Rate() TABLE NAME:PRODUCT_CLEANING_RATE"

    Public Function DeleteProduct_Rate(ByVal bv_PRDCT_CLNNG_RT_ID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(ProductData._V_PRODUCT_CLEANING_RATE).NewRow()
            With dr
                .Item(ProductData.PRDCT_CLNNG_RT_ID) = bv_PRDCT_CLNNG_RT_ID
            End With
            DeleteProduct_Rate = objData.DeleteRow(dr, Cleaning_RateDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetCleaningTypeByProductID() VIEW NAME:V_PRODUCT_CLEANING_RATE"

    Public Function pub_GetCleaningTypeByProductID(ByVal bv_i64ClngTypeID As Int64, ByVal bv_i64PdtID As Int64) As ProductDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(ProductData.CLNNG_TYP_ID, bv_i64ClngTypeID)
            hshParameters.Add(ProductData.PRDCT_ID, bv_i64PdtID)
            objData = New DataObjects(V_Product_Cleaning_RateSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), ProductData._V_PRODUCT_CLEANING_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetCleaningTypeGridByProductID() VIEW NAME:V_PRODUCT_CLEANING_RATE AND CLEANING_TYPE"

    Public Function pub_GetCleaningTypeGridByProductID(ByVal bv_i64PdtID As Int64) As ProductDataSet
        Try
            objData = New DataObjects(V_Product_cleaning_Type_GridQuery, ProductData.PRDCT_ID, bv_i64PdtID)
            objData.Fill(CType(ds, DataSet), ProductData._V_PRODUCT_CLEANING_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_CheckProductCleaningRate() VIEW NAME:PRODUCT_CLEANING_RATE"

    Public Function pub_CheckProductCleaningRate(ByVal bv_i64PdtID As Int64, ByVal bv_i64ClngTypeID As Int64, ByRef br_objTransaction As Transactions) As ProductDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(ProductData.PRDCT_ID, bv_i64PdtID)
            hshParameters.Add(ProductData.CLNNG_TYP_ID, bv_i64ClngTypeID)
            objData = New DataObjects(Product_Cleaning_RateSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), ProductData._PRODUCT_CLEANING_RATE, br_objTransaction)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetProductCustomerByProductID() VIEW NAME:PRODUCT_CUSTOMER"

    Public Function pub_GetProductCustomerByProductID(ByVal bv_i64PdtID As Int64) As ProductDataSet
        Try
            objData = New DataObjects(V_Product_CustomerSelectQueryByProductID, ProductData.PRDCT_ID, bv_i64PdtID)
            objData.Fill(CType(ds, DataSet), ProductData._V_PRODUCT_CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetProductCustomerRateBy_ProductID() VIEW NAME:PRODUCT_CUSTOMER_RATE"

    Public Function pub_GetProductCustomerRateBy_ProductID(ByVal bv_i64PRDTID As Int64, ByVal BV_I64PRDTIDCSTMRID As Int64, ByVal bv_int64CustomerID As Int64) As ProductDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(ProductData.PRDCT_ID, bv_i64PRDTID)
            hshTable.Add(ProductData.PRDCT_CSTMR_ID, BV_I64PRDTIDCSTMRID)
            objData = New DataObjects(V_Product_Customer_RateSelectQueryByProductID, hshTable)
            objData.Fill(CType(ds, DataSet), ProductData._V_PRODUCT_CUSTOMER_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetProductCustomerByProductCustomerID() VIEW NAME:PRODUCT_CUSTOMER"

    Public Function GetProductCustomerByProductCustomerID(ByVal bv_i64ProductCustomerID As Int64, ByVal bv_intDepotID As Int64) As ProductDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(ProductData.PRDCT_CSTMR_ID, bv_i64ProductCustomerID)
            objData = New DataObjects(V_Product_CustomerSelectQueryByProductCustomerID, hshTable)
            objData.Fill(CType(ds, DataSet), ProductData._V_PRODUCT_CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetProductCustomerRateByProductCustomerID() VIEW NAME:PRODUCT_CUSTOMER_RATE"

    Public Function GetProductCustomerRateByProductCustomerID(ByVal bv_i64ProductCustomerID As Int64, ByVal bv_intDepotID As Int64) As ProductDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(ProductData.PRDCT_CSTMR_ID, bv_i64ProductCustomerID)
            objData = New DataObjects(V_Product_Customer_RateSelectQueryByProductCustomerID, hshTable)
            objData.Fill(CType(ds, DataSet), ProductData._V_PRODUCT_CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateProductAttachmentDetail() TABLE NAME: PRODUCT_ATTACHMENT_DETAIL"
    Public Function CreateProductAttachmentDetail(ByVal bv_lngProductId As Long, _
                                                  ByVal bv_strAttachmentPath As String, _
                                                  ByVal bv_strActualFileName As String, _
                                                  ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ProductData._PRODUCT_ATTACHMENT_DETAIL, br_objTransaction)
                .Item(ProductData.PRDCT_ATTCHMNT_DTL_ID) = intMax
                .Item(ProductData.PRDCT_ID) = bv_lngProductId
                .Item(ProductData.ATTCHMNT_PTH) = bv_strAttachmentPath
                .Item(ProductData.ACTL_FL_NM) = bv_strActualFileName
            End With
            objData.InsertRow(dr, ProductAttachmentDetailInsertQuery, br_objTransaction)
            dr = Nothing
            CreateProductAttachmentDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteEquipmentInformationDetail() Table Name: EQUIPMENT_INFORMATION_DETAIL"

    Public Function DeleteProductAttachmentDetailByProductId(ByVal bv_lngProductId As Long, _
                                                             ByRef br_objTransaction As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).NewRow()
            With dr
                .Item(ProductData.PRDCT_ID) = bv_lngProductId
            End With
            DeleteProductAttachmentDetailByProductId = objData.DeleteRow(dr, ProductAttachmentDetailDeleteQuery, br_objTransaction)
            dr = Nothing
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetProductAttachmentDetailByProductId  Table Name: EQUIPMENT_INFORMATION_DETAIL"
    Public Function GetProductAttachmentDetailByProductId(ByVal bv_lngProductId As Long) As ProductDataSet
        Try
            objData = New DataObjects(ProductAttachmentDetailSelectQuery, ProductData.PRDCT_ID, bv_lngProductId)
            objData.Fill(CType(ds, DataSet), ProductData._PRODUCT_ATTACHMENT_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class



#End Region
