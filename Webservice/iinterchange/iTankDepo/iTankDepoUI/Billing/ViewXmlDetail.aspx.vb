Partial Class Billing_ViewXmlDetail
    Inherits Pagebase
#Region "Declarations.."
    Dim dsViewXmlEdi As New ViewXmlEdiDataSet
    Private Const VIEW_XMLEDI As String = "VIEW_XMLEDI"
#End Region

#Region "Page_Load"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack AndAlso Not Page.IsCallback Then
                If Not Request.QueryString("EDIId") Is Nothing Then
                    hdnEDIId.Value = Request.QueryString("EDIId")
                    pvt_bindXmlDetail(hdnEDIId.Value)
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "pvt_bindXmlDetail"
    Private Sub pvt_bindXmlDetail(ByVal bv_intInvoiceEdiId As Integer)
        Try
            Dim objViewXmlEdi As New ViewXmlEdi
            Dim dtInvoiceEDI As New DataTable
            Dim dtEdiDetail As New DataTable
            dsViewXmlEdi = CType(RetrieveData(VIEW_XMLEDI), ViewXmlEdiDataSet)
            dtInvoiceEDI = dsViewXmlEdi.Tables(ViewXmlEdiData._INVOICE_EDI_HISTORY).Select(String.Concat("INVC_EDI_HSTRY_ID=", bv_intInvoiceEdiId)).CopyToDataTable
            dtEdiDetail = objViewXmlEdi.pub_GetInvoiceXmlEDIDetail(CInt(hdnEDIId.Value))
            ifgViewEdi.DataSource = dtInvoiceEDI
            ifgViewEdi.DataBind()
            ifgViewXmlEDI.DataSource = dtEdiDetail
            ifgViewXmlEDI.DataBind()
            CacheData(VIEW_XMLEDI, dsViewXmlEdi)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
