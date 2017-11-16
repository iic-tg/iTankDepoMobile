Option Strict On
Partial Class Masters_CustomerEmailSetting
    Inherits Pagebase
    Dim dsCustomerData As New CustomerDataSet
    Dim objCustomer As New Customer
    Private Const CUSTOMER As String = "CUSTOMER"
    'Private Const CUSTOMER_EMAIL_SETTING As String = "CUSTOMER_EMAIL_SETTING"
    Private strMSGUPDATE As String = "Record Updated Successfully."

#Region "Page_Load"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack AndAlso Not Page.IsCallback Then
                If Not Request.QueryString("CustomerId") Is Nothing Then
                    hdnCustomerID.Value = Request.QueryString("CustomerId")
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "fnGetData"
                    pvt_GetData(e.GetCallbackValue("mode"))
                Case "updateCustomerEmailSetting"
                    'CacheData(CUSTOMER_EMAIL_SETTING, dsCustomerData)
            End Select

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                 MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_bindCustomerEmailSetting"
    Private Sub pvt_bindCustomerEmailSetting(ByVal bv_strCustomerID As String)
        Try
            Dim dtCustomerData As New DataTable

            dsCustomerData = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            dtCustomerData = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING)
            Dim dsCustomerDataTemp As New CustomerDataSet
            dsCustomerData.Tables(CustomerData._REPORT_NAME).Rows.Clear()
           
            Dim str_011KeyValue As String

            Dim bln_011KeyExist As Boolean
            Dim objcommon As New CommonData()
            str_011KeyValue = objcommon.GetConfigSetting("011", bln_011KeyExist)

            Dim strReportName() As String = Nothing
            Dim sbrReportValue As New StringBuilder
            If bln_011KeyExist Then
                strReportName = str_011KeyValue.ToString.Split(CChar(","))
            End If

            For i = 0 To strReportName.Length - 1
                If sbrReportValue.ToString <> Nothing Then
                    sbrReportValue.Append(",")
                End If
                sbrReportValue.Append("'")
                sbrReportValue.Append(UCase(Trim(strReportName(i))))
                sbrReportValue.Append("'")
            Next

            dsCustomerDataTemp = objCustomer.pub_getReportType(sbrReportValue.ToString())

            If dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows.Count = 0 Then
                If bv_strCustomerID <> Nothing Then
                    For i As Integer = 0 To (dsCustomerDataTemp.Tables(CustomerData._REPORT_NAME).Rows.Count) - 1
                        Dim dr As DataRow
                        dr = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).NewRow
                        dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows.Add(dr)
                        dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows(i).Item(CustomerData.RPRT_ID) = CStr(dsCustomerDataTemp.Tables(CustomerData._REPORT_NAME).Rows(i).Item(CustomerData.RPRT_ID))
                        dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows(i).Item(CustomerData.RPRT_CD) = CStr(dsCustomerDataTemp.Tables(CustomerData._REPORT_NAME).Rows(i).Item(CustomerData.RPRT_CD))
                        dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows(i).Item(CustomerData.CSTMR_ID) = bv_strCustomerID
                        dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows(i).Item(CustomerData.CSTMR_EML_STTNG_BIN) = CommonWeb.GetNextIndex(dtCustomerData, CustomerData.CSTMR_EML_STTNG_BIN) + i
                    Next
                End If
            Else
                If dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows.Count <= dsCustomerDataTemp.Tables(CustomerData._REPORT_NAME).Rows.Count Then
                    dsCustomerData.Merge(dsCustomerDataTemp.Tables(CustomerData._REPORT_NAME))
                    Dim strEnum As New StringBuilder
                    For Each dr As DataRow In dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows
                        If strEnum.Length > 0 Then
                            strEnum.AppendLine(",")
                        End If
                        strEnum.AppendLine(String.Concat("'", CStr(dr.Item(CustomerData.RPRT_ID)), "'"))
                    Next
                    Dim intCount As Integer = 0
                    For Each drEmailDetail As DataRow In dsCustomerData.Tables(CustomerData._REPORT_NAME).Select(String.Concat(CustomerData.RPRT_ID, " NOT IN (", _
                                                                                              strEnum.ToString, ")"))
                        Dim dr As DataRow
                        dr = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).NewRow

                        dr.Item(CustomerData.RPRT_ID) = drEmailDetail.Item(CustomerData.RPRT_ID)
                        dr.Item(CustomerData.RPRT_CD) = drEmailDetail.Item(CustomerData.RPRT_CD)
                        dr.Item(CustomerData.CSTMR_ID) = bv_strCustomerID
                        dr.Item(CustomerData.CSTMR_EML_STTNG_BIN) = CommonWeb.GetNextIndex(dtCustomerData, CustomerData.CSTMR_EML_STTNG_BIN) + intCount
                        dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows.Add(dr)
                        intCount = intCount + 1
                    Next
                End If
            End If
            dsCustomerData.AcceptChanges()
            ifgEmailSetting.DataSource = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING)
            ifgEmailSetting.DataBind()
            CacheData(CUSTOMER, dsCustomerData)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_GetData"

    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim sbCustomer As New StringBuilder
            pub_SetCallbackReturnValue("Message", sbCustomer.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "Page_PreRenderComplete"
    Protected Sub Page_PreRenderComplete(sender As Object, e As System.EventArgs) Handles Me.PreRenderComplete
        Try
            CommonWeb.IncludeScript("Script\Masters\CustomerEmailSetting.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEmailSetting_RowUpdated"

    Protected Sub ifgEmailSetting_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgEmailSetting.RowUpdated
        Try
            dsCustomerData = CType(RetrieveData(CUSTOMER), CustomerDataSet)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                    MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region



#Region "ifgEmailSetting_ClientBind"

    Protected Sub ifgEmailSetting_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEmailSetting.ClientBind
        Try
            Dim strCustomerID As String = e.Parameters("CustomerID").ToString()
            pvt_bindCustomerEmailSetting(strCustomerID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "ifgEmailSetting_RowDataBound"

    Protected Sub ifgEmailSetting_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEmailSetting.RowDataBound
        Dim chkcancel As iInterchange.WebControls.v4.Data.iFgCheckBox
        Try
            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
                If Not IsNothing(CType(e.Row.DataItem, DataRowView)) AndAlso Not IsDBNull(CType(e.Row.DataItem, DataRowView).Item(CustomerData.PRDC_FLTR_ID)) Then
                    If CStr(CType(e.Row.DataItem, DataRowView).Item(CustomerData.PRDC_FLTR_ID)) = "33" Then
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    ElseIf CStr(CType(e.Row.DataItem, DataRowView).Item(CustomerData.PRDC_FLTR_ID)) = "34" Then
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    ElseIf CStr(CType(e.Row.DataItem, DataRowView).Item(CustomerData.PRDC_FLTR_ID)) = "35" Then
                        CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    ElseIf CStr(CType(e.Row.DataItem, DataRowView).Item(CustomerData.PRDC_FLTR_ID)) = "36" Then
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If

                chkcancel = CType(e.Row.Cells(8).Controls(0), iInterchange.WebControls.v4.Data.iFgCheckBox)
                chkcancel.Attributes.Add("onclick", String.Concat("fnActiveCheck(this);"))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class