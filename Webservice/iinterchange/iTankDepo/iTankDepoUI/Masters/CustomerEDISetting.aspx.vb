Partial Class Masters_EDIGenerationInfo
    Inherits Pagebase
    Dim dsCustomerData As CustomerDataSet
    Private Const EDI_INFO As String = "EDI_INFO"
    Private Const CUSTOMER As String = "CUSTOMER"
    Private Const CUSTOMER_EDI_SETTING As String = "CUSTOMER_EDI_SETTING"
#Region "Page_OnCallback"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            Dim intCstmrId As Integer = Request.QueryString("CustomerId")
            Dim s As String = txtEDIemailid.Text
            Dim sbCustomer As New StringBuilder
            hdnCustomerID.Value = Request.QueryString("CustomerId")
            dsCustomerData = CType(RetrieveData(CUSTOMER), CustomerDataSet)

            If dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows.Count = 0 Then
                If dsCustomerData.Tables(CustomerData._V_CUSTOMER_EDI_SETTING).Rows.Count > 0 Then
                    hdnCustomerID.Value = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.CSTMR_ID)
                    lkpEDIFormat.Text = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.EDI_FRMT_CD)
                    txtEDIemailid.Text = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.TO_EML_ID)
                    lkpGenerationFormat.Text = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.GNRTN_FRMT_CD)
                    txtGenerationTime.Text = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.GNRTN_TM)
                End If
            Else
                hdnCustomerID.Value = dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.CSTMR_ID)
                lkpEDIFormat.LookupColumns(0).InitialValue = dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.EDI_FRMT)
                lkpEDIFormat.Text = GetEDIFormat(dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.EDI_FRMT).ToString)
                If lkpEDIFormat.Text = "" Then
                    lkpEDIFormat.Text = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.EDI_FRMT_CD)
                End If

                txtEDIemailid.Text = dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.TO_EML_ID)
                lkpGenerationFormat.LookupColumns(0).InitialValue = dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.GNRTN_FRMT)

                lkpGenerationFormat.Text = GetEDIFormat(dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.GNRTN_FRMT).ToString)
                If lkpGenerationFormat.Text = "" Then
                    lkpGenerationFormat.Text = dsCustomerData.Tables(CustomerData._V_CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.GNRTN_FRMT_CD)
                End If
                txtGenerationTime.Text = dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows(0).Item(CustomerData.GNRTN_TM)
            End If
            dsCustomerData.Merge(dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING))
            CacheData(CUSTOMER, dsCustomerData)
            pub_SetCallbackReturnValue("Message", sbCustomer.ToString)
            pub_SetCallbackStatus(True)
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "GetEDIFormat"
    Private Function GetEDIFormat(ByVal bv_editype As Integer) As String
        Try
            Dim objcommon As New CommonData
            Dim editype As String
            editype = objcommon.GetEnumCD(bv_editype)
            Return editype
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "submit"
                    pvt_Submit((e.GetCallbackValue("ToEDIEmail")), _
                                  CInt(e.GetCallbackValue("GenerationFormatID")), _
                                   (e.GetCallbackValue("GenerationFormatCode")), _
                                   e.GetCallbackValue("GenerationTime"),
                                   e.GetCallbackValue("EDIFormatID"), _
                                   e.GetCallbackValue("EDIFormatCode"), _
                            e.GetCallbackValue("HiddenID"))

            End Select

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                 MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Masters/CustomerEDISetting.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub pvt_Submit(ByVal bv_strToEDIEmail As String, ByVal bv_strGenerationFormatID As Integer, ByVal bv_strGenerationFormatCode As String, ByVal bv_strGenerationTime As String, ByVal bv_strEDIFormatID As Integer, ByVal bv_strEDIFormatCode As String, ByVal bv_intHiddenID As Integer)
        Try
            Dim strGenerationTime As String = bv_strGenerationTime
            Dim splitGenerationTime = strGenerationTime.Split(":")
            If UCase(bv_strGenerationFormatCode) = "PERIODIC" Then
                If splitGenerationTime.Length <> 1 Then
                    pub_SetCallbackReturnValue("blnPeriodic", True)
                    pub_SetCallbackStatus(True)
                    Exit Sub
                End If
            ElseIf UCase(bv_strGenerationFormatCode) = "SPECIFIC" Then
                If splitGenerationTime.Length <> 2 Then
                    pub_SetCallbackReturnValue("blnSpecific", True)
                    pub_SetCallbackStatus(True)
                    Exit Sub
                End If
            End If

            Dim drEDISetting As DataRow
            dsCustomerData = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            If dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows.Count = 0 Then
                drEDISetting = dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).NewRow()
                drEDISetting.Item(CustomerData.CSTMR_ID) = bv_intHiddenID
                drEDISetting.Item(CustomerData.EDI_FRMT) = bv_strEDIFormatID
                drEDISetting.Item(CustomerData.EDI_FRMT_CD) = bv_strEDIFormatCode
                drEDISetting.Item(CustomerData.TO_EML_ID) = bv_strToEDIEmail
                drEDISetting.Item(CustomerData.GNRTN_FRMT) = bv_strGenerationFormatID
                drEDISetting.Item(CustomerData.GNRTN_FRMT_CD) = bv_strGenerationFormatCode
                drEDISetting.Item(CustomerData.GNRTN_TM) = bv_strGenerationTime
                dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows.Add(drEDISetting)
            Else
                dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Clear()
                drEDISetting = dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).NewRow()
                drEDISetting.Item(CustomerData.CSTMR_ID) = bv_intHiddenID
                drEDISetting.Item(CustomerData.EDI_FRMT) = bv_strEDIFormatID
                drEDISetting.Item(CustomerData.EDI_FRMT_CD) = bv_strEDIFormatCode
                drEDISetting.Item(CustomerData.TO_EML_ID) = bv_strToEDIEmail
                drEDISetting.Item(CustomerData.GNRTN_FRMT) = bv_strGenerationFormatID
                drEDISetting.Item(CustomerData.GNRTN_FRMT_CD) = bv_strGenerationFormatCode
                drEDISetting.Item(CustomerData.GNRTN_TM) = bv_strGenerationTime
                dsCustomerData.Tables(CustomerData._CUSTOMER_EDI_SETTING).Rows.Add(drEDISetting)
            End If
            CacheData(CUSTOMER_EDI_SETTING, dsCustomerData)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        Try
            CommonWeb.pub_AttachHasChanges(txtEDIemailid)
            CommonWeb.pub_AttachHasChanges(txtGenerationTime)
            CommonWeb.pub_AttachHasChanges(lblEDIemailid)
            CommonWeb.pub_AttachHasChanges(lblEDIFormat)
            CommonWeb.pub_AttachHasChanges(lblGenerationFormat)
            CommonWeb.pub_AttachHasChanges(lblGenerationTime)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class