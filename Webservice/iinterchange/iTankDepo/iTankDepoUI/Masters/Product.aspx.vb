
Partial Class Masters_Product
    Inherits Pagebase

#Region "Declaration"

    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Private strMSGDUPLICATE As String = "This Cleaning Type already exists."
    Dim strProductRateDuplicateRowCondition As String() = {ProductData.CLNNG_TYP_CD}
    Dim dsProduct As ProductDataSet
    Dim dsProductCustomerRate As ProductDataSet
    Private Const Product As String = "Product"
    Private Const PRODUCT_CUSTOMER_RATE As String = "PRODUCT_CUSTOMER_RATE"
    Dim dtProductData As DataTable
#End Region

#Region "Parameters"

    Private pvt_strPageMode As String
    Private pvt_intID As Integer

#End Region

#Region "Page_Load"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            pvt_SetChangesMade()
            ifgCleaningType.DeleteButtonText = "Delete Row"
            ifgCleaningType.RefreshButtonText = "Refresh"
            ifgCleaningType.AddButtonText = "Add Row"

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "fnGetData"
                pvt_GetData(e.GetCallbackValue("mode"))
            Case "CreateProduct"
                pvt_CreateProduct(e.GetCallbackValue("bv_strPRDCT_CD"), _
                        e.GetCallbackValue("bv_strPRDCT_DSCRPTN_VC"), _
                        e.GetCallbackValue("bv_strCHMCL_NM"), _
                        e.GetCallbackValue("bv_strIMO_CLSS"), _
                        e.GetCallbackValue("bv_strUN_NO"), _
                        e.GetCallbackValue("bv_i64CLSSFCTN_ID"),
                        e.GetCallbackValue("bv_i64GRP_CLSSFCTN_ID"), _
                        e.GetCallbackValue("bv_strRMRKS_VC"), _
                        e.GetCallbackValue("bv_dblCLNNG_TTL_AMNT_NC"), _
                        CBool(e.GetCallbackValue("bv_blnCLNBL_BT")), _
                        CBool(e.GetCallbackValue("bv_blnACTV_BT")), _
                        e.GetCallbackValue("TypeID"), _
                        e.GetCallbackValue("PageMode"), _
                        e.GetCallbackValue("wfData"))

            Case "UpdateProduct"
                pvt_UpdateProduct(e.GetCallbackValue("ID"), _
                        e.GetCallbackValue("bv_strPRDCT_CD"), _
                        e.GetCallbackValue("bv_strPRDCT_DSCRPTN_VC"), _
                        e.GetCallbackValue("bv_strCHMCL_NM"), _
                        e.GetCallbackValue("bv_strIMO_CLSS"), _
                        e.GetCallbackValue("bv_strUN_NO"), _
                        e.GetCallbackValue("bv_i64CLSSFCTN_ID"),
                        e.GetCallbackValue("bv_i64GRP_CLSSFCTN_ID"), _
                        e.GetCallbackValue("bv_strRMRKS_VC"), _
                        e.GetCallbackValue("bv_dblCLNNG_TTL_AMNT_NC"), _
                        CBool(e.GetCallbackValue("bv_blnCLNBL_BT")), _
                        CBool(e.GetCallbackValue("bv_blnACTV_BT")), _
                        e.GetCallbackValue("TypeID"), _
                        e.GetCallbackValue("PageMode"), _
                        e.GetCallbackValue("wfData"))
            Case "ValidateCode"
                pvt_ValidateProductCode(e.GetCallbackValue("Code"))
            Case "ValidateCleaningTypeCode"
                pvt_ValidateCleaningTypeCode(e.GetCallbackValue("CleaningTypeID"), e.GetCallbackValue("ProductID"))
            Case "AttachmentCount"
                pvt_AttachmentCount(e.GetCallbackValue("ProductId"))
        End Select
    End Sub
#End Region

#Region "pvt_AttachmentCount()"
    Private Sub pvt_AttachmentCount(ByVal bv_strProductId As String)
        Try
            Dim intCount As Integer = 0
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            Dim drSelect() As DataRow = Nothing
            drSelect = dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Select(String.Concat(ProductData.PRDCT_ID, " = ", bv_strProductId))
            If drSelect.Length > 0 Then
                intCount = dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Compute(String.Concat("COUNT(", ProductData.PRDCT_ID, ")"), "")
            End If
            pub_SetCallbackReturnValue("AttachmentCount", intCount)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetData"

    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim sbProduct As New StringBuilder
            Dim strCode As String = String.Empty
            If bv_strMode = MODE_EDIT Then
                sbProduct.Append(CommonWeb.GetTextValuesJSO(txtPRDCT_CD, PageSubmitPane.pub_GetPageAttribute(ProductData.PRDCT_CD)))
                strCode = CommonWeb.GetTextValuesJSO(txtPRDCT_CD, PageSubmitPane.pub_GetPageAttribute(ProductData.PRDCT_CD))
                sbProduct.Append(CommonWeb.GetTextValuesJSO(txtPRDCT_DSCRPTN_VC, PageSubmitPane.pub_GetPageAttribute(ProductData.PRDCT_DSCRPTN_VC)))
                If PageSubmitPane.pub_GetPageAttribute(ProductData.CHMCL_NM) = Nothing Then
                    sbProduct.Append(CommonWeb.GetTextValuesJSO(txtCHMCL_NM, ""))
                Else
                    sbProduct.Append(CommonWeb.GetTextValuesJSO(txtCHMCL_NM, PageSubmitPane.pub_GetPageAttribute(ProductData.CHMCL_NM)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(ProductData.IMO_CLSS) = Nothing Then
                    sbProduct.Append(CommonWeb.GetTextValuesJSO(txtIMO_CLSS, ""))
                Else
                    sbProduct.Append(CommonWeb.GetTextValuesJSO(txtIMO_CLSS, PageSubmitPane.pub_GetPageAttribute(ProductData.IMO_CLSS)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(ProductData.UN_NO) = Nothing Then
                    sbProduct.Append(CommonWeb.GetTextValuesJSO(txtUN_NO, ""))
                Else
                    sbProduct.Append(CommonWeb.GetTextValuesJSO(txtUN_NO, PageSubmitPane.pub_GetPageAttribute(ProductData.UN_NO)))
                End If

                If PageSubmitPane.pub_GetPageAttribute(ProductData.CLSSFCTN_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(ProductData.CLSSFCTN_ID) <> "" Then
                    sbProduct.Append(CommonWeb.GetLookupValuesJSO(lkpCLSSFCTN_ID, PageSubmitPane.pub_GetPageAttribute(ProductData.CLSSFCTN_ID), PageSubmitPane.pub_GetPageAttribute(ProductData.PRODUCT_CLASSIFICATION)))
                Else
                    sbProduct.Append(CommonWeb.GetLookupValuesJSO(lkpCLSSFCTN_ID, "", ""))
                End If
                If PageSubmitPane.pub_GetPageAttribute(ProductData.GRP_CLSSFCTN_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(ProductData.GRP_CLSSFCTN_ID) <> "" Then
                    sbProduct.Append(CommonWeb.GetLookupValuesJSO(lkpGRP_CLSSFCTN_ID, PageSubmitPane.pub_GetPageAttribute(ProductData.GRP_CLSSFCTN_ID), PageSubmitPane.pub_GetPageAttribute(ProductData.GROUP_CLASSIFICATION)))
                Else
                    sbProduct.Append(CommonWeb.GetLookupValuesJSO(lkpGRP_CLSSFCTN_ID, "", ""))
                End If
                If PageSubmitPane.pub_GetPageAttribute(ProductData.RMRKS_VC) Is Nothing Then
                    sbProduct.Append(CommonWeb.GetTextValuesJSO(txtRMRKS_VC, ""))
                Else
                    sbProduct.Append(CommonWeb.GetTextValuesJSO(txtRMRKS_VC, PageSubmitPane.pub_GetPageAttribute(ProductData.RMRKS_VC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(ProductData.COUNT_ATTACH) Is Nothing Then
                    sbProduct.Append("setText(el('hypFilesAttachment'),'');")
                Else
                    sbProduct.Append(String.Concat("setText(el('hypFilesAttachment'),'" + String.Concat(hypFilesAttachment.InnerText, "(", PageSubmitPane.pub_GetPageAttribute(ProductData.COUNT_ATTACH), ")") + "');"))
                End If
                If PageSubmitPane.pub_GetPageAttribute(ProductData.CLNNG_MTHD_TYP_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(ProductData.CLNNG_MTHD_TYP_ID) <> "" Then
                    sbProduct.Append(CommonWeb.GetLookupValuesJSO(lkpType, PageSubmitPane.pub_GetPageAttribute(ProductData.CLNNG_MTHD_TYP_ID), PageSubmitPane.pub_GetPageAttribute(ProductData.CLNNG_MTHD_TYP_CD)))
                Else
                    sbProduct.Append(CommonWeb.GetLookupValuesJSO(lkpType, "", ""))
                End If
                sbProduct.Append(CommonWeb.GetLabelValuesJSO(lblCLNNG_TTL_AMNT_NC, PageSubmitPane.pub_GetPageAttribute(ProductData.CLNNG_TTL_AMNT_NC)))
                sbProduct.Append(CommonWeb.GetCheckboxValuesJSO(chkCLNBL_BT, CBool(PageSubmitPane.pub_GetPageAttribute(ProductData.CLNBL_BT))))
                sbProduct.Append(CommonWeb.GetCheckboxValuesJSO(chkACTV_BT, CBool(PageSubmitPane.pub_GetPageAttribute(ProductData.ACTV_BT))))
                sbProduct.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(ProductData.PRDCT_ID), "');"))
            Else
                Dim str_038KeyValue As String = String.Empty
                Dim bln_038KeyValue As Boolean = False
                Dim objcommon As New CommonData

                str_038KeyValue = objcommon.GetConfigSetting("038", bln_038KeyValue)
                If bln_038KeyValue AndAlso str_038KeyValue.ToUpper = "TRUE" Then
                    Dim objCommonUI As New CommonUI
                    strCode = objCommonUI.pub_getIdentityValue("PRODUCT", ProductData.PRDCT_CD, ProductData.PRDCT_ID, "DESC", True, 1)
                    dsProduct = New ProductDataSet
                    sbProduct.Append(String.Concat("el('txtPRDCT_CD').value = '", strCode, "';"))
                End If
                End If
                pub_SetCallbackReturnValue("Message", sbProduct.ToString)
                pub_SetCallbackReturnValue("Code", strCode)
                pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_CreateProduct"

    Public Function pvt_CreateProduct(ByVal bv_strPRDCT_CD As String, _
        ByVal bv_strPRDCT_DSCRPTN_VC As String, _
        ByVal bv_strCHMCL_NM As String, _
        ByVal bv_strIMO_CLSS As String, _
        ByVal bv_strUN_NO As String, _
        ByVal bv_i64CLSSFCTN_ID As Int64, _
        ByVal bv_i64GRP_CLSSFCTN_ID As Int64, _
        ByVal bv_strRMRKS_VC As String, _
        ByVal bv_dblCLNNG_TTL_AMNT_NC As Double, _
        ByVal bv_blnCLNBL_BT As Boolean, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_strTypeID As String, _
        ByVal bv_strPageMode As String, _
        ByVal bv_strWfData As String) As Long
        Dim objcommon As New CommonData
        Try
            Dim objProduct As New Product
            Dim lngCreated As Long
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            End If

            pvt_AddTotalCustomerRate(CType(ifgProductCustomer.DataSource, DataTable))

            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            dtProductData = CType(ifgCleaningType.DataSource, DataTable)
            dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Merge(dtProductData)

            If RetrieveData(PRODUCT_CUSTOMER_RATE) IsNot Nothing Then
                dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Merge(CType(RetrieveData(PRODUCT_CUSTOMER_RATE), ProductDataSet).Tables(ProductData._V_PRODUCT_CUSTOMER_RATE))
            End If

            If dtProductData.Rows.Count = 0 Then
                pub_SetCallbackError("Please Enter ProductRate.")
                pub_SetCallbackStatus(False)
                Return 0
            End If

            lngCreated = objProduct.pub_CreateProduct((bv_strPRDCT_CD), _
                                                       bv_strPRDCT_DSCRPTN_VC, _
                                                       bv_strCHMCL_NM, _
                                                       bv_strIMO_CLSS, _
                                                       bv_strUN_NO, _
                                                       bv_i64CLSSFCTN_ID, _
                                                       bv_i64GRP_CLSSFCTN_ID, _
                                                       bv_strRMRKS_VC, _
                                                       bv_dblCLNNG_TTL_AMNT_NC, _
                                                       bv_blnCLNBL_BT, _
                                                       strModifiedby, _
                                                       datModifiedDate, _
                                                       strModifiedby, _
                                                       datModifiedDate, _
                                                       bv_blnACTV_BT, _
                                                       intDepotID, _
                                                       bv_strTypeID, _
                                                       bv_strPageMode, _
                                                       bv_strWfData, _
                                                       dsProduct)

            dsProduct.AcceptChanges()
            dsProductCustomerRate = CType(RetrieveData(PRODUCT_CUSTOMER_RATE), ProductDataSet)
            If Not dsProductCustomerRate Is Nothing Then
                dsProductCustomerRate.AcceptChanges()
                dsProductCustomerRate.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Rows.Clear()
            End If

            pub_SetCallbackReturnValue("ID", CStr(lngCreated))
            pub_SetCallbackReturnValue("CleaningRate", CStr(bv_dblCLNNG_TTL_AMNT_NC))
            pub_SetCallbackReturnValue("Message", String.Concat("Product : ", bv_strPRDCT_CD, " ", strMSGINSERT))
            pub_SetCallbackStatus(True)
            Return lngCreated

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function

#End Region

#Region "pvt_UpdateProduct"

    Private Sub pvt_UpdateProduct(ByVal bv_strPRDCT_ID As String, _
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
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_strTypeID As String, _
        ByVal bv_strPageMode As String, _
        ByVal bv_strWfData As String)
        Try
            Dim objProduct As New Product
            Dim boolUpdated As Boolean

            Dim objcommon As New CommonData
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim dtAttachmentDetail As New DataTable
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            End If
            pvt_AddTotalCustomerRate(CType(ifgProductCustomer.DataSource, DataTable))
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            dtProductData = CType(ifgCleaningType.DataSource, DataTable)
            dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Merge(dtProductData)
            If Not dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL) Is Nothing AndAlso dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Rows.Count = 0 Then
                dtAttachmentDetail = objProduct.pub_GetProductAttachmentDetailByProductId(CInt(bv_strPRDCT_ID)).Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL)
                dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Merge(dtAttachmentDetail)
            End If

            If RetrieveData(PRODUCT_CUSTOMER_RATE) IsNot Nothing Then
                dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Merge(CType(RetrieveData(PRODUCT_CUSTOMER_RATE), ProductDataSet).Tables(ProductData._V_PRODUCT_CUSTOMER_RATE))
            End If

            boolUpdated = objProduct.pub_ModifyProduct(CInt(bv_strPRDCT_ID), _
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
                                                       strModifiedby, _
                                                       datModifiedDate, _
                                                       strModifiedby, _
                                                       datModifiedDate, _
                                                       bv_blnACTV_BT, _
                                                       intDepotID, _
                                                       bv_strTypeID, _
                                                       bv_strPageMode, _
                                                       bv_strWfData, _
                                                       dsProduct)

            dsProduct.AcceptChanges()
            dsProductCustomerRate = CType(RetrieveData(PRODUCT_CUSTOMER_RATE), ProductDataSet)
            If Not dsProductCustomerRate Is Nothing Then
                dsProductCustomerRate.AcceptChanges()
                dsProductCustomerRate.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Rows.Clear()
            End If
            pub_SetCallbackReturnValue("CleaningRate", CStr(bv_dblCLNNG_TTL_AMNT_NC))
            pub_SetCallbackReturnValue("Message", String.Concat("Product : ", bv_strPRDCT_CD, " ", strMSGUPDATE))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "pvt_ValidateProductCode"

    Public Sub pvt_ValidateProductCode(ByVal bv_strProductCode As String)

        Try
            Dim objProduct As New Product
            dsProduct = objProduct.pub_GetProductByProductCode(bv_strProductCode)
            If dsProduct.Tables(ProductData._PRODUCT).Rows.Count = 0 Then
                pub_SetCallbackReturnValue("valid", "true")
            Else
                pub_SetCallbackReturnValue("valid", "false")
            End If

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ValidateCleaningTypeCode"

    Public Sub pvt_ValidateCleaningTypeCode(ByVal bv_strCleaningTypeID As String, ByVal bv_strProductID As String)

        Try
            Dim objProduct As New Product
            dsProduct = objProduct.pub_GetCleaningTypeByProductID(CommonUIs.iLng(bv_strCleaningTypeID), CommonUIs.iLng(bv_strProductID))
            If dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Rows.Count < 1 Then
                pub_SetCallbackReturnValue("valid", "true")
            Else
                pub_SetCallbackReturnValue("valid", "false")
            End If

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_SetChangesMade"

    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgCleaningType, "ITab1_0")
        pub_SetGridChanges(ifgProductCustomer, "ITab1_0")
        pub_SetGridChanges(ifgProductCustomer, "ITab1_0")
        CommonWeb.pub_AttachHasChanges(txtPRDCT_CD)
        CommonWeb.pub_AttachHasChanges(txtPRDCT_DSCRPTN_VC)
        CommonWeb.pub_AttachHasChanges(txtCHMCL_NM)
        CommonWeb.pub_AttachHasChanges(txtIMO_CLSS)
        CommonWeb.pub_AttachHasChanges(txtUN_NO)
        CommonWeb.pub_AttachHasChanges(lkpCLSSFCTN_ID)
        CommonWeb.pub_AttachHasChanges(lkpGRP_CLSSFCTN_ID)
        CommonWeb.pub_AttachDescMaxlength(txtRMRKS_VC)
        CommonWeb.pub_AttachHasChanges(chkCLNBL_BT)
        CommonWeb.pub_AttachHasChanges(chkACTV_BT)
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/Product.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgCleaningType_ClientBind"

    Protected Sub ifgCleaningType_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgCleaningType.ClientBind
        Try
            Dim strWFData As String = e.Parameters("WFDATA").ToString()
            Dim strProductID As String = e.Parameters("ProductID").ToString()
            Dim objcommon As New CommonData
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(strWFData, CommonUIData.DPT_ID))
            End If
            Dim objProduct As New Product
            If strProductID <> "" And strProductID <> "0" Then
                dsProduct = objProduct.pub_GetCleaninTypeByProductID(strProductID)
                If dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Rows.Count = 0 Then
                    dsProduct = objProduct.pub_GetCleaningType(intDepotID)
                Else
                    objProduct.pub_GetCleaningTypeGridByProductID(dsProduct, strProductID)
                End If
            Else
                dsProduct = objProduct.pub_GetCleaningType(intDepotID)
            End If
            e.DataSource = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE)
            CacheData(Product, dsProduct)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub

#End Region

#Region "ifgCleaningType_RowInserting"

    Protected Sub ifgCleaningType_RowInserting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgCleaningType.RowInserting
        Try
            Dim objProduct As New Product
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            dtProductData = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE)
            Dim strPageMode As String = CStr(e.InputParamters("mode"))
            Dim strProductID As String = CStr(e.InputParamters("ProductID"))

            Dim lngID As Long
            lngID = CommonWeb.GetNextIndex(dtProductData, ProductData.PRDCT_CLNNG_RT_ID)
            e.Values(ProductData.PRDCT_CLNNG_RT_ID) = lngID

            'Validate against Database
            If strProductID <> "" And strProductID <> "0" Then
                Dim dtDeletedRows As DataTable = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).GetChanges(DataRowState.Deleted)
                Dim dtDBRows As DataTable = objProduct.pub_GetCleaningRateByProductID(strProductID).Tables(ProductData._V_PRODUCT_CLEANING_RATE)

                If dtDeletedRows IsNot Nothing Then
                    For Each dr As DataRow In dtDeletedRows.Rows
                        Dim drs As DataRow() = dtDBRows.Select(String.Concat(ProductData.PRDCT_CLNNG_RT_ID, "=", dr.Item(ProductData.PRDCT_CLNNG_RT_ID, DataRowVersion.Original)), "")
                        For Each drq As DataRow In drs
                            drq.Delete()
                        Next
                    Next
                End If
                'Edit Mode
                If CommonWeb.pub_IsRowAlreadyExists(dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE), CType(e.Values, OrderedDictionary), strProductRateDuplicateRowCondition, "edit", ProductData.PRDCT_CLNNG_RT_ID, CInt(e.Values(ProductData.PRDCT_CLNNG_RT_ID))) Then
                    e.OutputParamters("Duplicate") = strMSGDUPLICATE
                    e.Cancel = True
                    Exit Sub
                End If

            Else
                'New Mode
                If CommonWeb.pub_IsRowAlreadyExists(dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE), CType(e.Values, OrderedDictionary), strProductRateDuplicateRowCondition, "New", ProductData.PRDCT_CLNNG_RT_ID, CInt(e.Values(ProductData.PRDCT_CLNNG_RT_ID))) Then
                    e.OutputParamters("Duplicate") = strMSGDUPLICATE
                    e.Cancel = True
                    Exit Sub
                End If
            End If
            Dim dblCleaningRate As Double = 0
            If dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Rows.Count > 0 Then
                dblCleaningRate = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Compute("sum(AMNT_NC)", "")
            End If
            e.OutputParamters.Add("CleaningRate", dblCleaningRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgCleaningType_RowInserted"
    Protected Sub ifgCleaningType_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgCleaningType.RowInserted
        Try
            Dim dblCleaningRate As Double = 0
            If dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Rows.Count > 0 Then
                dblCleaningRate = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Compute("sum(AMNT_NC)", "")
            End If
            e.OutputParamters.Add("CleaningRate", dblCleaningRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgCleaningType_RowUpdating"

    Protected Sub ifgCleaningType_RowUpdating(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgCleaningType.RowUpdating
        Try
            Dim objProduct As New Product
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            Dim strProductID As String = CStr(e.InputParamters("ProductID"))
            e.NewValues(ProductData.PRDCT_CLNNG_RT_ID) = e.OldValues(ProductData.PRDCT_CLNNG_RT_ID)

            If CommonWeb.pub_IsRowAlreadyExists(dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE), CType(e.NewValues, OrderedDictionary), strProductRateDuplicateRowCondition, "edit", ProductData.PRDCT_CLNNG_RT_ID, CInt(e.OldValues(ProductData.PRDCT_CLNNG_RT_ID))) Then
                e.OutputParamters.Add("Duplicate", strMSGDUPLICATE)
                e.Cancel = True
                e.RequiresRebind = True
                Exit Sub
            Else
                If CommonWeb.pub_IsRowAlreadyExists(objProduct.pub_GetCleaninTypeByProductID(CInt(strProductID)).Tables(ProductData._V_PRODUCT_CLEANING_RATE), CType(e.NewValues, OrderedDictionary), strProductRateDuplicateRowCondition, "New", ProductData.PRDCT_CLNNG_RT_ID, CInt(e.OldValues(ProductData.PRDCT_CLNNG_RT_ID))) Then
                    e.OutputParamters.Add("Duplicate", strMSGDUPLICATE)
                    e.Cancel = True
                    e.RequiresRebind = True
                    Exit Sub
                Else
                    e.NewValues(ProductData.PRDCT_CLNNG_RT_ID) = e.OldValues(ProductData.PRDCT_CLNNG_RT_ID)
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgCleaningType_RowUpdated"
    Protected Sub ifgCleaningType_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgCleaningType.RowUpdated
        Try
            Dim dblCleaningRate As Double = 0
            If dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Rows.Count > 0 Then
                dblCleaningRate = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Compute("sum(AMNT_NC)", "")
            End If
            e.OutputParamters.Add("CleaningRate", dblCleaningRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgCleaningType_RowDeleting"
    Protected Sub ifgCleaningType_RowDeleting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgCleaningType.RowDeleting
        Try
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            dtProductData = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Copy()
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgCleaningType.PageSize * ifgCleaningType.PageIndex + e.RowIndex

            Dim drChkDefault As DataRow()
            drChkDefault = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Select(String.Concat(ProductData.CLNNG_TYP_ID, "=", e.Values.Item(ProductData.CLNNG_TYP_ID), " AND ", ProductData.DFLT_BT, "=True"))
            If drChkDefault.Length > 0 Then
                e.Cancel = True
                e.OutputParamters.Add("CheckDefault", String.Concat("Cleaning Type : Default value ", dtProductData.Rows(intRowIndex).Item(ProductData.CLNNG_TYP_CD).ToString, " cannot be deleted"))
            Else
                e.OutputParamters("Delete") = String.Concat("Cleaning Type : ", dtProductData.Rows(intRowIndex).Item(ProductData.CLNNG_TYP_CD).ToString, " has been deleted from Product. Click submit to save changes.")
            End If

            Dim dblCleaningRate As Double = 0
            If dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Rows.Count > 0 Then
                dblCleaningRate = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Compute("sum(AMNT_NC)", "")
            End If
            e.OutputParamters.Add("CleaningRate", dblCleaningRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "ifgCleaningType_RowDeleted"
    Protected Sub ifgCleaningType_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgCleaningType.RowDeleted
        Try
            Dim dblCleaningRate As Double = 0
            If dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Rows.Count > 0 Then
                dblCleaningRate = dsProduct.Tables(ProductData._V_PRODUCT_CLEANING_RATE).Compute("sum(AMNT_NC)", "")
            End If
            e.OutputParamters.Add("CleaningRate", dblCleaningRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgCleaningType_RowDataBound"
    Protected Sub ifgCleaningType_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgCleaningType.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                    If CommonUIs.iBool(drv.Item(ProductData.DFLT_BT)) Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    Else
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                    End If
                End If
                If e.Row.RowIndex > 1 Then
                    Dim lkpCleaningType As iLookup
                    lkpCleaningType = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpCleaningType.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                 MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgProductCustomer_ClientBind"
    Protected Sub ifgProductCustomer_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgProductCustomer.ClientBind
        Try
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            Dim objProduct As New Product
            Dim strProductID As String = e.Parameters("ProductID").ToString()
            dtProductData = objProduct.pub_GetProductCustomerByProductID(CommonUIs.iLng(strProductID)).Tables(ProductData._V_PRODUCT_CUSTOMER)
            dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER).Merge(dtProductData)
            e.DataSource = dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER)
            CacheData(Product, dsProduct)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgProductCustomer_RowDataBound"
    Protected Sub ifgProductCustomer_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgProductCustomer.RowDataBound
        Try
            Dim objCommon As New CommonUI
            Dim hypAddLink As HyperLink
            If e.Row.RowType = DataControlRowType.DataRow Then
                hypAddLink = CType(e.Row.Cells(1).Controls(0), HyperLink)
                hypAddLink.Attributes.Add("onclick", "showCleaningType();return false;")
                hypAddLink.NavigateUrl = "#"
                hypAddLink.Text = "Add/Edit"
                hypAddLink.ToolTip = "Click the link to Add/Edit Cleaning Rate"
                If e.Row.RowIndex > 1 Then
                    Dim lkpProductCustomer As iLookup
                    lkpProductCustomer = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpProductCustomer.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                    MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgProductCustomer_RowInserting"
    Protected Sub ifgProductCustomer_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgProductCustomer.RowInserting
        Try
            Dim lngID As Long
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            dtProductData = dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER)
            lngID = CommonWeb.GetNextIndex(dtProductData, ProductData.PRDCT_CSTMR_ID)
            e.Values(ProductData.PRDCT_CSTMR_ID) = lngID
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                   MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgProductCustomer_RowDeleting"
    Protected Sub ifgProductCustomer_RowDeleting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgProductCustomer.RowDeleting
        Try
            Dim dsProductTemp As New ProductDataSet
            Dim dtProductTemp As New DataTable
            Dim lngProductCustomerID As Long
            Dim objProduct As New Product
            lngProductCustomerID = CLng(e.Keys(ProductData.PRDCT_CSTMR_ID))
            Dim objCommonData As New CommonData
            Dim intDepotID As Integer = CInt(objCommonData.GetDepotID())

            dsProductTemp = objProduct.pub_GetProductCustomerByProductCustomerID(lngProductCustomerID, intDepotID)
            If Not dsProductTemp.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Rows.Count = 0 Then
                For Each drProductCustomerRate As DataRow In dsProductTemp.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Rows
                    drProductCustomerRate.Delete()
                Next
            End If
            If Not dsProductTemp.Tables(ProductData._V_PRODUCT_CUSTOMER).Rows.Count = 0 Then
                For Each drProductCustomer As DataRow In dsProductTemp.Tables(ProductData._V_PRODUCT_CUSTOMER).Rows
                    drProductCustomer.Delete()
                Next
            End If
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Merge(dsProductTemp.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE))
            dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER).Merge(dsProductTemp.Tables(ProductData._V_PRODUCT_CUSTOMER))
            CacheData(Product, dsProduct)
            dsProductTemp = CType(RetrieveData(PRODUCT_CUSTOMER_RATE), ProductDataSet)
            If Not dsProductTemp Is Nothing Then
                dtProductTemp = dsProductTemp.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Select(String.Concat(ProductData.PRDCT_CSTMR_ID, "=", lngProductCustomerID)).CopyToDataTable()
                For Each drProductCustomerRate As DataRow In dtProductTemp.Rows
                    drProductCustomerRate.Delete()
                Next
                dsProductTemp.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Merge(dtProductTemp)
            End If
            CacheData(PRODUCT_CUSTOMER_RATE, dsProductTemp)
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgProductCustomer.PageSize * ifgProductCustomer.PageIndex + e.RowIndex
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            Dim dtProduct As Data.DataTable = dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER).Copy
            If CType(ifgProductCustomer.DataSource, DataTable).Select(String.Concat(ProductData.PRDCT_CSTMR_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.OutputParamters("Delete") = String.Concat("Customer : Code ", dtProduct.Rows(intRowIndex).Item(ProductData.CSTMR_CD).ToString, " has been deleted from Product. Click submit to save changes.")
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                 MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_AddTotalCustomerRate"
    Private Sub pvt_AddTotalCustomerRate(ByRef br_dtProductData As DataTable)
        Try
            Dim blnUpdated As Boolean = True
            dsProduct = CType(RetrieveData(PRODUCT_CUSTOMER_RATE), ProductDataSet)
            If dsProduct IsNot Nothing Then
                Dim dtProduct As DataTable = dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).GetChanges()
                If dtProduct IsNot Nothing Then
                    For Each dr As DataRow In dtProduct.Rows
                        Dim lngProductCustomerID As Long = 0
                        If dr.RowState = DataRowState.Deleted Then
                            lngProductCustomerID = CLng(dr.Item(ProductData.PRDCT_CSTMR_ID, DataRowVersion.Original))
                        Else
                            lngProductCustomerID = CLng(dr.Item(ProductData.PRDCT_CSTMR_ID))
                        End If
                        Dim drs As DataRow() = br_dtProductData.Select(String.Concat(ProductData.PRDCT_CSTMR_ID, "=", lngProductCustomerID), "")
                        Dim dblCharges As Double = pvt_GetCustomerRate(lngProductCustomerID, blnUpdated)
                        If blnUpdated And drs.Length > 0 Then
                            drs(0).Item(ProductData.TTL_AMNT_NC) = dblCharges
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_GetCustomerRate()"
    Private Function pvt_GetCustomerRate(ByVal bv_lngProductCustomerID As Long, ByRef br_blnUpdated As Boolean) As Double
        Try
            Dim dblCharges As Double = 0
            dsProduct = CType(RetrieveData(PRODUCT_CUSTOMER_RATE), ProductDataSet)
            If Not dsProduct Is Nothing Then
                For Each dr As DataRow In dsProduct.Tables(ProductData._V_PRODUCT_CUSTOMER_RATE).Select(String.Concat(ProductData.PRDCT_CSTMR_ID, "=", bv_lngProductCustomerID))
                    If Not dr.RowState = DataRowState.Deleted Then
                        If Not IsDBNull(dr.Item(ProductData.AMNT_NC)) Then
                            dblCharges += CType(dr.Item(ProductData.AMNT_NC), Double)
                        End If
                        br_blnUpdated = True
                    Else
                        br_blnUpdated = False
                    End If
                Next
            Else
                br_blnUpdated = False
            End If
            Return dblCharges
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class