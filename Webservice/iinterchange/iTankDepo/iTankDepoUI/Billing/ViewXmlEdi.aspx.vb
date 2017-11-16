
Partial Class Billing_ViewXmlEdi
    Inherits Pagebase
#Region "Declarations.."
    Dim dsViewXmlEdi As New ViewXmlEdiDataSet
    Private Const VIEW_XMLEDI As String = "VIEW_XMLEDI"

#End Region

#Region "Page_Load1"

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                datPeriodFrom.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
                datPeriodTo.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "PreRender"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Billing/ViewXmlEdi.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgViewXmlEDI_ClientBind"

    Protected Sub ifgViewXmlEDI_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgViewXmlEDI.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objViewXmlEdi As New ViewXmlEdi
            Dim intDepotId As Integer = objCommon.GetDepotID()
            If Not e.Parameters("CustomerId") Is Nothing Then
                Dim fromDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodFrom"))
                Dim toDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodTo"))
                Dim i64CustomerID As Long = CommonUIs.iLng(e.Parameters("CustomerId"))
                Dim strStatus As String = CStr(e.Parameters("Status"))
                Dim strActivityName As String = CStr(e.Parameters("ActivityName"))
                dsViewXmlEdi = objViewXmlEdi.pub_GetInvoiceXmlEDIByCustomerId(i64CustomerID, fromDate, _
                                                                              toDate, intDepotId, strActivityName, strStatus)
                e.DataSource = dsViewXmlEdi.Tables(ViewXmlEdiData._INVOICE_EDI_HISTORY)
                CacheData(VIEW_XMLEDI, dsViewXmlEdi)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgViewXmlEDI_RowDataBound"

    Protected Sub ifgViewXmlEDI_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgViewXmlEDI.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
                Dim drv As System.Data.DataRowView
                drv = CType(e.Row.DataItem, Data.DataRowView)
                Dim strSent As String = String.Empty
                If drv.Item(ViewXmlEdiData.RMRKS_VC).ToString <> "" Then
                    Dim hyplnkRemarks As HyperLink
                    hyplnkRemarks = CType(e.Row.Cells(8).Controls(0), HyperLink)
                    hyplnkRemarks.Attributes.Add("onclick", String.Concat("openXmlDetail(", drv.Item(ViewXmlEdiData.INVC_EDI_HSTRY_ID), "); return false;"))
                    hyplnkRemarks.Text = drv.Item(ViewXmlEdiData.RMRKS_VC).ToString
                    hyplnkRemarks.NavigateUrl = "#"
                End If
                If drv.Item(ViewXmlEdiData.SNT_FL_NAM).ToString <> "" Then
                    Dim hyplnkFile As HyperLink
                    hyplnkFile = CType(e.Row.Cells(4).Controls(0), HyperLink)
                    If Not IsDBNull(drv.Item(ViewXmlEdiData.SNT_DT)) Then
                        strSent = "SENT"
                    Else
                        strSent = "Generated"
                    End If
                    hyplnkFile.Attributes.Add("onclick", String.Concat("openXmlDocument('", drv.Item(ViewXmlEdiData.SNT_FL_NAM), "','", strSent, "'); return false;"))
                    hyplnkFile.Text = drv.Item(ViewXmlEdiData.SNT_FL_NAM).ToString
                    hyplnkFile.NavigateUrl = "#"
                End If
                If drv.Item(ViewXmlEdiData.RCVD_FL_NAM).ToString <> "" Then
                    Dim hyplnkFile As HyperLink
                    hyplnkFile = CType(e.Row.Cells(5).Controls(0), HyperLink)
                    hyplnkFile.Attributes.Add("onclick", String.Concat("openXmlDocument('", drv.Item(ViewXmlEdiData.RCVD_FL_NAM), "','Received'); return false;"))
                    hyplnkFile.Text = drv.Item(ViewXmlEdiData.RCVD_FL_NAM).ToString
                    hyplnkFile.NavigateUrl = "#"
                End If
                If drv.Item(ViewXmlEdiData.ERRR_DSCRPTN).ToString <> "" Then
                    Dim strRUrl As String = HttpContext.Current.Request.ApplicationPath & "/MoreInfo.aspx"
                    Dim hyplnkFile As HyperLink
                    hyplnkFile = CType(e.Row.Cells(9).Controls(0), HyperLink)
                    hyplnkFile.Attributes.Add("onclick", String.Concat("OpenErrorPopup('", drv.Item(ViewXmlEdiData.ERRR_DSCRPTN), "','", strRUrl, "'); return false;"))
                    hyplnkFile.Text = "View Error"
                    hyplnkFile.NavigateUrl = "#"
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
