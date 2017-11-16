
Partial Class Masters_ProductAttachmentDetail
    Inherits Framebase

    Dim dsProduct As New ProductDataSet
    Private Const Product As String = "Product"
    Private strSize As String = ConfigurationSettings.AppSettings("UploadProductSize")
    Private strFileLength As String = ConfigurationSettings.AppSettings("UploadProductFileLength")

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/ProductAttachmentDetail.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/swfobject.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreLoad"
    Protected Sub Page_PreLoad(sender As Object, e As System.EventArgs) Handles Me.PreLoad
        Try
            flashUpload.UploadFileSizeLimit = strSize
            flashUpload.TotalUploadSizeLimit = strSize
            Dim intDepotId As Integer = 0
            Dim drEqInfo() As DataRow = Nothing
            Dim drRow As DataRow = Nothing
            Dim lngProductId As Long = 0
            Dim dtProductData As New DataTable
            Dim objCommonData As New CommonData
            Dim objProduct As New Product
            intDepotId = objCommonData.GetDepotID()

            Dim hfc As HttpFileCollection = Request.Files

            If Not Request.QueryString("ProductId") Is Nothing Then
                CacheData("ProductId", Request.QueryString("ProductId").ToString)
            End If
            If Not RetrieveData("ProductId") Is Nothing Then
                lngProductId = RetrieveData("ProductId").ToString
            End If
            'If Not Request.QueryString("ProductId") Then
            '    lngProductId = CLng(Request.QueryString("ProductId"))
            'End If
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            If Not (dsProduct) Is Nothing Then
                dsProduct.Tables(ProductData._V_PRODUCT_ATTACHMENT_DETAIL_INFO).Rows.Clear()
                drRow = dsProduct.Tables(ProductData._V_PRODUCT_ATTACHMENT_DETAIL_INFO).NewRow()
                drRow.Item(ProductData.PRDCT_ID) = lngProductId
                dsProduct.Tables(ProductData._V_PRODUCT_ATTACHMENT_DETAIL_INFO).Rows.Add(drRow)

                'Get Files from Equipment Information Detail
                If (IsNothing(dsProduct) OrElse (Not IsNothing(dsProduct) AndAlso (dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Rows.Count = 0 OrElse dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Rows.Count > 0 AndAlso dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Select(String.Concat(ProductData.PRDCT_ID, "=", lngProductId)).Length = 0))) Then
                    If Not dsProduct Is Nothing Then
                        dtProductData = dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL)
                        drEqInfo = dtProductData.Select(String.Concat(ProductData.PRDCT_ID, "=", lngProductId))
                        If drEqInfo.Length = 0 Then
                            drEqInfo = dtProductData.Select(String.Concat(ProductData.PRDCT_ID, "=", lngProductId), "", DataViewRowState.Deleted)
                            If drEqInfo.Length = 0 Then
                                drEqInfo = Nothing
                            End If
                        End If
                    End If
                    If drEqInfo Is Nothing Then
                        If lngProductId > 0 Then
                            If dsProduct Is Nothing Then
                                dtProductData = objProduct.pub_GetProductAttachmentDetailByProductId(lngProductId).Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL)
                            Else
                                dtProductData = objProduct.pub_GetProductAttachmentDetailByProductId(lngProductId).Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL)
                            End If
                        End If
                    End If
                End If
                dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Merge(dtProductData)
                dsProduct.Tables(ProductData._V_PRODUCT_ATTACHMENT_DETAIL_INFO).Merge(dsProduct.Tables(ProductData._V_PRODUCT_ATTACHMENT_DETAIL_INFO))
                CacheData(Product, dsProduct)
                pvt_ShwoAttachment()
            End If
            If Not (dsProduct) Is Nothing Then
                pvt_fileUpload(hfc)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback()"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "deletePhoto"
                    deletePhoto(e.GetCallbackValue("EquipmentInformationDetailId"), _
                                e.GetCallbackValue("EquipmentInformationId"), _
                                e.GetCallbackValue("PageName"))
                Case "getErrorSummary"
                    pvt_getErrorSummary(e.GetCallbackValue("PageName"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region


#Region "btnUpload_Click()"
    Protected Sub btnUpload_Click(sender As Object, e As System.EventArgs) Handles btnUpload.Click
        Try
            Dim hfc As HttpFileCollection = Request.Files
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            pvt_fileUpload(hfc)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ShwoAttachment()"
    Private Sub pvt_ShwoAttachment()
        Try
            Dim sbAttachment As New StringBuilder
            Dim ulTag As New HtmlGenericControl
            Dim strProductId As String = String.Empty
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            If dsProduct.Tables(ProductData._V_PRODUCT_ATTACHMENT_DETAIL_INFO).Rows.Count > 0 Then
                strProductId = dsProduct.Tables(ProductData._V_PRODUCT_ATTACHMENT_DETAIL_INFO).Rows(0).Item(ProductData.PRDCT_ID)
            End If

            If dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Rows.Count > 0 Then
                Dim strPhysicalpath As String = String.Empty
                Dim strFileName As String = String.Empty
                Dim strAppDomainAppPath As String = System.Web.HttpRuntime.AppDomainAppPath()
                Dim strPath As String = Config.pub_GetAppConfigValue("DownloadProduct")
                ulTag = CType(photoList, HtmlGenericControl)
                For Each dr As DataRow In dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Select(String.Concat(ProductData.PRDCT_ID, "=", strProductId, " AND ", RepairEstimateData.ERR_FLG, " <> ", "'True'"))
                    strPhysicalpath = dr.Item(ProductData.ATTCHMNT_PTH).ToString
                    strFileName = dr.Item(ProductData.ACTL_FL_NM).ToString
                    sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(ProductData.PRDCT_ATTCHMNT_DTL_ID)) + "','" + CStr(dr.Item(ProductData.PRDCT_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                    sbAttachment.Append("<a href=""" + String.Concat(strPath, strPhysicalpath) + """ target=""_blank"" ><span  class=""MultiFile-title"">" + strFileName + "</span></a>")
                    sbAttachment.Append("<br />")
                    ulTag.InnerHtml = sbAttachment.ToString()
                Next
                CacheData("PhotoLists", sbAttachment)
            Else
                sbAttachment = sbAttachment.Append("")
                ulTag.InnerHtml = sbAttachment.ToString()
                CacheData("PhotoLists", sbAttachment)
            End If
            If sbAttachment.Length = 0 Then
                Page.ClientScript.RegisterStartupScript(GetType(String), "", " el('divNoImageUpload').style.display = 'block';", True)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_fileUpload()"
    Private Sub pvt_fileUpload(ByVal hfc As HttpFileCollection)

        Try
            Dim strFilename As String = String.Empty
            Dim strExtension As String = String.Empty
            Dim strClientFileName As String = String.Empty
            Dim strPageName As String = String.Empty
            Dim strAppDomainAppPath As String = System.Web.HttpRuntime.AppDomainAppPath()
            Dim strVirtualPath As String = Config.pub_GetAppConfigValue("UploadProduct")
            Dim strProductId As String = String.Empty
            Dim lngFileSize As Long = 0
            Dim uploadFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString
            Dim drAttachment As DataRow = Nothing
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            If dsProduct.Tables(ProductData._V_PRODUCT_ATTACHMENT_DETAIL_INFO).Rows.Count > 0 Then
                strProductId = dsProduct.Tables(ProductData._V_PRODUCT_ATTACHMENT_DETAIL_INFO).Rows(0).Item(ProductData.PRDCT_ID)
            End If
            For i As Integer = 0 To hfc.Count - 1
                Dim hpf As HttpPostedFile = hfc(i)
                If hpf.ContentLength > 0 Then
                    strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
                    strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
                    strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                    strVirtualPath = String.Concat(strVirtualPath, strFilename, strExtension)
                    'Create New Row for Attachment
                    drAttachment = dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).NewRow()
                    drAttachment(ProductData.PRDCT_ATTCHMNT_DTL_ID) = CommonWeb.GetNextIndex(dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL), ProductData.PRDCT_ATTCHMNT_DTL_ID)
                    drAttachment(ProductData.PRDCT_ID) = strProductId
                    Dim regExpMatch As Match = System.Text.RegularExpressions.Regex.Match(strFilename, "^[a-zA-Z0-9-_.&, ]+$")
                    If regExpMatch.Success Then
                        lngFileSize = hpf.ContentLength
                        If lngFileSize < CLng(strSize) AndAlso strClientFileName.Length < strFileLength Then
                            If Not System.IO.Directory.Exists(String.Concat(uploadFolder, "\UpLoad\Product")) Then
                                System.IO.Directory.CreateDirectory(String.Concat(uploadFolder, "\UpLoad\Product"))
                            End If
                            hpf.SaveAs(strVirtualPath)
                            drAttachment(ProductData.ATTCHMNT_PTH) = String.Concat(String.Concat(strFilename, strExtension))
                            drAttachment(ProductData.ACTL_FL_NM) = strClientFileName
                            drAttachment(RepairEstimateData.ERR_FLG) = False
                        Else
                            drAttachment(ProductData.ERR_FLG) = True
                            drAttachment(ProductData.ERR_SMMRY) = String.Concat("Attachment File Name (", strClientFileName, " must be below ", strFileLength, "Characters.")
                        End If
                    Else
                        drAttachment(ProductData.ERR_FLG) = True
                        drAttachment(ProductData.ERR_SMMRY) = String.Concat("File Name (", strClientFileName, ") with special characters are not allowed. ")
                    End If
                    dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Rows.Add(drAttachment)
                End If
            Next
            If dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Rows.Count = 0 Then
                Page.ClientScript.RegisterStartupScript(GetType(String), "", " el('divNoImageUpload').style.display = 'block';", True)
            End If
            CacheData(Product, dsProduct)
            pvt_ShwoAttachment()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_getErrorSummary()"
    Private Sub pvt_getErrorSummary(ByVal bv_strPageName As String)
        Try
            Dim sbrError As New StringBuilder
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            If Not (dsProduct) Is Nothing Then
                For Each drErrorRow As DataRow In dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Select(String.Concat(ProductData.ERR_FLG, " = ", "True"))
                    If sbrError.Length > 0 Then
                        sbrError.Append(" , ")
                    End If
                    sbrError.Append(drErrorRow.Item(ProductData.ERR_SMMRY))
                    drErrorRow.Delete()
                Next
                If sbrError.Length > 0 Then
                    pub_SetCallbackReturnValue("errorSummary", sbrError.ToString)
                Else
                    pub_SetCallbackReturnValue("errorSummary", "") ' Added by Sakthivel for UIG Fix
                End If
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "deletePhoto"
    Private Sub deletePhoto(ByVal bv_strProductAttachmentDetailId As String, ByVal bv_strProductId As String, ByVal bv_strPageName As String)
        Try
            Dim intCount As Integer = 0
            Dim drSelect As DataRow() = Nothing
            dsProduct = CType(RetrieveData(Product), ProductDataSet)
            Dim dr() As DataRow
            If Not IsNothing(dsProduct) Then
                dr = dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).Select(String.Concat(ProductData.PRDCT_ATTCHMNT_DTL_ID, "= ", bv_strProductAttachmentDetailId, " AND ", ProductData.PRDCT_ID, " = ", bv_strProductId))
                If dr.Length > 0 Then
                    dr(0).Delete()
                End If
                '  dsProduct.Tables(ProductData._PRODUCT_ATTACHMENT_DETAIL).AcceptChanges()
                CacheData(Product, dsProduct)
                Dim sb As New StringBuilder
                pvt_ShwoAttachment()
                sb = CType(RetrieveData("PhotoLists"), StringBuilder)
                pub_SetCallbackStatus(True)
                pub_SetCallbackReturnValue("fileList", sb.ToString())
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
End Class
