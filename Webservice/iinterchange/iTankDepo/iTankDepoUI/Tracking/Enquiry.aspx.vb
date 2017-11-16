Option Strict On
Partial Class Tracking_Enquiry
    Inherits Pagebase

#Region "Declarations"
    Dim dsEnquiry As New EnquiryDataSet
    Dim dtEnquiry As DataTable
    Private Const ENQUIRY As String = "ENQUIRY"
    Dim bln_023EqType_Key As Boolean
    Dim str_023EqType As String
    Dim bln_022EqType_Key As Boolean
    Dim str_022EqType As String
    Dim bln_073EqType_Key As Boolean
    Dim str_073EqType As String
    Dim objCommonConfig As New ConfigSetting()

#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim objCommon As New CommonData
            If Not Page.IsPostBack Then
                ifgProduct.ActionButtons.Item(0).Text = "Generate"
              

                ' ifgCustomer.ActionButtons.Item(0).Text = "Generate"
            End If
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                If objCommon.GetHeadQuarterID = objCommon.GetDepotID() Then
                    lkpEnquiryType.TableName = "58"
                Else
                    lkpEnquiryType.TableName = "103"
                End If
            Else
                lkpEnquiryType.TableName = "58"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GenerateProductDocument"
                    pvt_GenerateProductDocument()
                Case "GenerateCustomerDocument"
                    pvt_GenerateCustomerDocument(e.GetCallbackValue("CustomerId"))
                Case "GenerateRouteDocument"
                    pvt_GenerateRouteDocument(e.GetCallbackValue("RouteID"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GenerateRouteDocument"

    Private Sub pvt_GenerateRouteDocument(ByVal str_RouteId As String)
        Try
            Dim objCommon As New CommonData
            Dim objEnquiry As New Enquiry
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            Dim dtRoute As DataTable
            Dim dtCustomerRoute As New DataTable
            Dim dtRouteTransporter As New DataTable
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            End If
            dtRoute = objEnquiry.pub_GetRouteByRouteID(CInt(str_RouteId), intDepotID).Tables(EnquiryData._V_ROUTE)
            dtCustomerRoute = objEnquiry.pub_GetCustomerRouteDetails(CInt(str_RouteId), intDepotID).Tables(EnquiryData._V_CUSTOMER_TRANSPORTATION)
            dtRouteTransporter = objEnquiry.pub_GetTransporterRouteDetailByRouteID(CInt(str_RouteId), intDepotID).Tables(EnquiryData._V_TRANSPORTER_ROUTE_DETAIL)
            For Each dr As DataRow In dtRoute.Rows
                dr.Item(EnquiryData.DPT_CD) = objCommon.GetDepotCD()
            Next
            dsEnquiry.Tables(EnquiryData._V_ROUTE).Clear()
            dsEnquiry.Tables(EnquiryData._V_CUSTOMER_TRANSPORTATION).Clear()
            dsEnquiry.Tables(EnquiryData._V_TRANSPORTER_ROUTE_DETAIL).Clear()
            dsEnquiry.Tables(EnquiryData._V_ROUTE).Merge(dtRoute)
            dsEnquiry.Tables(EnquiryData._V_CUSTOMER_TRANSPORTATION).Merge(dtCustomerRoute)
            dsEnquiry.Tables(EnquiryData._V_TRANSPORTER_ROUTE_DETAIL).Merge(dtRouteTransporter)
            CacheData(ENQUIRY, dsEnquiry)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_GenerateCustomerDocument"

    Private Sub pvt_GenerateCustomerDocument(ByVal str_CustomerId As String)
        Try
            Dim objCommon As New CommonData
            Dim objEnquiry As New Enquiry
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            End If
            str_022EqType = objCommonConfig.pub_GetConfigSingleValue("022", intDepotID)
            bln_022EqType_Key = objCommonConfig.IsKeyExists
            str_073EqType = objCommonConfig.pub_GetConfigSingleValue("073", intDepotID)
            bln_073EqType_Key = objCommonConfig.IsKeyExists
            str_023EqType = objCommonConfig.pub_GetConfigSingleValue("023", intDepotID)
            bln_023EqType_Key = objCommonConfig.IsKeyExists
            Dim dtCustomerRate As DataTable
            Dim dtCustomer As New DataTable
            Dim dtRentalCustomer As New DataTable
            Dim dtCustomerTransportaiton As DataTable
            Dim dtCustomerCleaningRate As DataTable
            dtCustomer = objEnquiry.pub_GetV_CustomerByCustomerID(CInt(str_CustomerId), intDepotID).Tables(EnquiryData._V_CUSTOMER)
            dtCustomerRate = objEnquiry.pub_GetCustomerChargeByDepotID(CInt(str_CustomerId), intDepotID).Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CHARGE)
            dtCustomerCleaningRate = objEnquiry.pub_GetCustomerCleaningSlabByDepotID(CInt(str_CustomerId), intDepotID).Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANINGSLAB)
            dtRentalCustomer = objEnquiry.pub_GetRentalCustomerDetails(CInt(str_CustomerId)).Tables(EnquiryData._V_CUSTOMER_RENTAL)
            dtCustomerTransportaiton = objEnquiry.pub_GetCustomerTransportation(CInt(str_CustomerId)).Tables(EnquiryData._V_CUSTOMER_TRANSPORTATION)
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CHARGE).Clear()
            dsEnquiry.Tables(EnquiryData._V_CUSTOMER).Clear()
            dsEnquiry.Tables(EnquiryData._V_CUSTOMER_RENTAL).Clear()
            dsEnquiry.Tables(EnquiryData._V_CUSTOMER_TRANSPORTATION).Clear()
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANINGSLAB).Clear()
            For Each dr As DataRow In dtCustomer.Rows
                If bln_022EqType_Key Then
                    If str_022EqType = "True" Then
                        dr.Item(EnquiryData.ALLOW_RNTL) = "True"
                    Else
                        dr.Item(EnquiryData.ALLOW_RNTL) = "False"
                    End If
                End If
                If bln_023EqType_Key Then
                    If str_023EqType = "True" Then
                        dr.Item(EnquiryData.ALLOW_TRNSPRTTN) = "True"
                    Else
                        dr.Item(EnquiryData.ALLOW_TRNSPRTTN) = "False"
                    End If
                End If
                dr.Item(EnquiryData.DPT_CD) = objCommon.GetDepotCD()
                If bln_073EqType_Key Then
                    If str_073EqType.ToLower = "true" Then
                        dr.Item(EnquiryData.ALLOW_CLNNG_SLAB) = "True"
                    Else
                        dr.Item(EnquiryData.ALLOW_CLNNG_SLAB) = "False"
                    End If
                End If
            Next
            dsEnquiry.Tables(EnquiryData._V_CUSTOMER).Merge(dtCustomer)
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CHARGE).Merge(dtCustomerRate)
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANINGSLAB).Merge(dtCustomerCleaningRate)
            dsEnquiry.Tables(EnquiryData._V_CUSTOMER_RENTAL).Merge(dtRentalCustomer)
            dsEnquiry.Tables(EnquiryData._V_CUSTOMER_TRANSPORTATION).Merge(dtCustomerTransportaiton)
            CacheData(ENQUIRY, dsEnquiry)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_GenerateProductDocument"

    Private Sub pvt_GenerateProductDocument()
        Try
            Dim objCommon As New CommonData
            Dim objEnquiry As New Enquiry
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            End If
            dsEnquiry = CType(RetrieveData(ENQUIRY), EnquiryDataSet)
            Dim dtProductRate As DataTable
            Dim drProductRate As DataRow
            Dim drCleaningRate As DataRow
            Dim dtCleaningRate As DataTable
            Dim drAProduct As DataRow()
            drAProduct = dsEnquiry.Tables(EnquiryData._V_PRODUCT).Select(EnquiryData.CHECKED & "='True'")
            If Not drAProduct.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Product.")
                Exit Sub
            End If
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_PRODUCT_CLEANING_RATE).Clear()
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANING_RATE).Clear()
            dtProductRate = objEnquiry.pub_GetProductCleaningRateByDepotID(intDepotID).Tables(EnquiryData._V_ENQUIRY_PRODUCT_CLEANING_RATE)
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_PRODUCT_CLEANING_RATE).Merge(dtProductRate)
            dtCleaningRate = objEnquiry.pub_GetCustomerCleaningRateByDepotID(intDepotID).Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANING_RATE)
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANING_RATE).Merge(dtCleaningRate)
            dtProductRate.Rows.Clear()
            dtCleaningRate.Rows.Clear()
            For Each dr As DataRow In dsEnquiry.Tables(EnquiryData._V_PRODUCT).Select(EnquiryData.CHECKED & "='True'")
                Dim ProductId As Integer = CInt(dr.Item(EnquiryData.PRDCT_ID))
                For Each drPrdctRate As DataRow In dsEnquiry.Tables(EnquiryData._V_ENQUIRY_PRODUCT_CLEANING_RATE).Select(String.Concat(EnquiryData.PRDCT_ID, "='", ProductId, "'"))
                    drProductRate = dtProductRate.NewRow()
                    drProductRate.Item(EnquiryData.PRDCT_ID) = drPrdctRate.Item(EnquiryData.PRDCT_ID)
                    drProductRate.Item(EnquiryData.PRDCT_DSCRPTN_VC) = drPrdctRate.Item(EnquiryData.PRDCT_DSCRPTN_VC)
                    drProductRate.Item(EnquiryData.CLNNG_AMNT_NC) = drPrdctRate.Item(EnquiryData.CLNNG_AMNT_NC)
                    drProductRate.Item(EnquiryData.SCRBBNG_AMNT_NC) = drPrdctRate.Item(EnquiryData.SCRBBNG_AMNT_NC)
                    drProductRate.Item(EnquiryData.HTNG_AMNT_NC) = drPrdctRate.Item(EnquiryData.HTNG_AMNT_NC)
                    drProductRate.Item(EnquiryData.HNDLNG_AMNT_NC) = drPrdctRate.Item(EnquiryData.HNDLNG_AMNT_NC)
                    drProductRate.Item(EnquiryData.DSPSNG_AMNT_NC) = drPrdctRate.Item(EnquiryData.DSPSNG_AMNT_NC)
                    drProductRate.Item(EnquiryData.OTHR_AMNT_NC) = drPrdctRate.Item(EnquiryData.OTHR_AMNT_NC)
                    drProductRate.Item(EnquiryData.DPT_LCL_CRRNCY) = drPrdctRate.Item(EnquiryData.DPT_LCL_CRRNCY)
                    drProductRate.Item(EnquiryData.CLNNG_TTL_AMNT_NC) = drPrdctRate.Item(EnquiryData.CLNNG_TTL_AMNT_NC)
                    dtProductRate.Rows.Add(drProductRate)
                Next
                dr.Item(EnquiryData.DPT_CD) = objCommon.GetDepotCD()

            Next
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_PRODUCT_CLEANING_RATE).Clear()
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_PRODUCT_CLEANING_RATE).Merge(dtProductRate)
            For Each dr As DataRow In dsEnquiry.Tables(EnquiryData._V_PRODUCT).Select(EnquiryData.CHECKED & "='True'")
                Dim ProductId As Integer = CInt(dr.Item(EnquiryData.PRDCT_ID))
                For Each drCustmrRate As DataRow In dsEnquiry.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANING_RATE).Select(String.Concat(EnquiryData.PRDCT_ID, "='", ProductId, "'"))
                    drCleaningRate = dtCleaningRate.NewRow()
                    drCleaningRate.Item(EnquiryData.PRDCT_ID) = drCustmrRate.Item(EnquiryData.PRDCT_ID)
                    drCleaningRate.Item(EnquiryData.PRDCT_CSTMR_NAM) = drCustmrRate.Item(EnquiryData.PRDCT_CSTMR_NAM)
                    drCleaningRate.Item(EnquiryData.PRDCT_DSCRPTN_VC) = drCustmrRate.Item(EnquiryData.PRDCT_DSCRPTN_VC)
                    drCleaningRate.Item(EnquiryData.CLNNG_AMNT_NC) = drCustmrRate.Item(EnquiryData.CLNNG_AMNT_NC)
                    drCleaningRate.Item(EnquiryData.SCRBBNG_AMNT_NC) = drCustmrRate.Item(EnquiryData.SCRBBNG_AMNT_NC)
                    drCleaningRate.Item(EnquiryData.HTNG_AMNT_NC) = drCustmrRate.Item(EnquiryData.HTNG_AMNT_NC)
                    drCleaningRate.Item(EnquiryData.HNDLNG_AMNT_NC) = drCustmrRate.Item(EnquiryData.HNDLNG_AMNT_NC)
                    drCleaningRate.Item(EnquiryData.DSPSNG_AMNT_NC) = drCustmrRate.Item(EnquiryData.DSPSNG_AMNT_NC)
                    drCleaningRate.Item(EnquiryData.OTHR_AMNT_NC) = drCustmrRate.Item(EnquiryData.OTHR_AMNT_NC)
                    drCleaningRate.Item(EnquiryData.CSTMR_CRRNCY) = drCustmrRate.Item(EnquiryData.CSTMR_CRRNCY)
                    drCleaningRate.Item(EnquiryData.TTL_AMNT_NC) = drCustmrRate.Item(EnquiryData.TTL_AMNT_NC)
                    drCleaningRate.Item(EnquiryData.CSTMR_ID) = drCustmrRate.Item(EnquiryData.CSTMR_ID)
                    dtCleaningRate.Rows.Add(drCleaningRate)
                Next
            Next
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANING_RATE).Clear()
            dsEnquiry.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANING_RATE).Merge(dtCleaningRate)
            CacheData(ENQUIRY, dsEnquiry)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Tracking/Enquiry.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgProduct_ClientBind"
    Protected Sub ifgProduct_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgProduct.ClientBind
        Try
            Dim objEnquiry As New Enquiry
            Dim objCommon As New CommonData
            Dim selectAll As String = e.Parameters("checkselect").ToString
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            CacheData("CheckAll", selectAll)
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            End If
            If e.Parameters("btnType").ToString = "Fetch" Then
                Dim strEnquiryType As String = e.Parameters("EnquiryType").ToString

                dsEnquiry = objEnquiry.pub_GetProductDetails(intDepotID)
                If selectAll = "true" Then
                    For Each drProductEnquiry As DataRow In dsEnquiry.Tables(EnquiryData._V_PRODUCT).Rows
                        drProductEnquiry.Item(EnquiryData.CHECKED) = True
                    Next
                ElseIf selectAll = "false" Then
                    For Each drProductEnquiry As DataRow In dsEnquiry.Tables(EnquiryData._V_PRODUCT).Rows
                        drProductEnquiry.Item(EnquiryData.CHECKED) = False
                    Next
                End If
                If dsEnquiry.Tables(EnquiryData._V_PRODUCT).Rows.Count = 0 Then
                    e.OutputParameters.Add("norecordsfound", True)
                Else
                    e.OutputParameters.Add("norecordsfound", False)
                End If
                e.DataSource = dsEnquiry.Tables(EnquiryData._V_PRODUCT)
            Else
                dsEnquiry.Tables(EnquiryData._V_PRODUCT).Rows.Clear()
            End If
            CacheData(ENQUIRY, dsEnquiry)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Header ProductRowCreate"
    Sub ProductRowCreate(ByVal writer As HtmlTextWriter, ByVal ctl As Control)
        Try
            Dim iLoop As Integer
            For iLoop = 0 To ctl.Controls.Count - 1
                If iLoop <> 8 Then
                    CType(ctl.Controls.Item(iLoop), WebControl).CssClass = "gHdr"
                    ctl.Controls.Item(iLoop).RenderControl(writer)

                    If iLoop = 7 Then
                        writer.Write("<TH class=""gHdr"">")
                        writer.Write("<input id=""chbSelectAll"" type=""checkbox""")
                      
                        If String.Equals(RetrieveData("CheckAll").ToString, "true") Then
                            writer.Write("onclick=""ProductSelectAll(this);"" checked />")
                        ElseIf String.Equals(RetrieveData("CheckAll").ToString, "false") Then
                            writer.Write("onclick=""ProductSelectAll(this);"" unchecked />")
                        Else
                            writer.Write("onclick=""ProductSelectAll(this);"" />")
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCustomer_ClientBind"
    Protected Sub ifgCustomer_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgCustomer.ClientBind
        Try
            If e.Parameters("btnType").ToString = "Fetch" Then
                Dim strEnquiryType As String = e.Parameters("EnquiryType").ToString
                Dim objEnquiry As New Enquiry

                Dim objCommon As New CommonData
                Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
                If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                    intDepotID = CInt(objCommon.GetHeadQuarterID())
                End If
                dsEnquiry = objEnquiry.pub_GetVCustomerByDepotId(intDepotID)
                If dsEnquiry.Tables(EnquiryData._V_CUSTOMER).Rows.Count = 0 Then
                    e.OutputParameters.Add("norecordsfound", True)
                Else
                    e.OutputParameters.Add("norecordsfound", False)
                End If
                e.DataSource = dsEnquiry.Tables(EnquiryData._V_CUSTOMER)
            Else
                dsEnquiry.Tables(EnquiryData._V_CUSTOMER).Rows.Clear()
            End If
            CacheData(ENQUIRY, dsEnquiry)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgProduct_RowCreated"
    Protected Sub ifgProduct_RowCreated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgProduct.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.SetRenderMethodDelegate(New RenderMethod(AddressOf ProductRowCreate))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCustomer_RowCreated"
    Protected Sub ifgCustomer_RowCreated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgCustomer.RowCreated
        Try
            'If e.Row.RowType = DataControlRowType.Header Then
            '    e.Row.SetRenderMethodDelegate(New RenderMethod(AddressOf CustomerRowCreate))
            'End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Header CustomerRowCreate"
    Sub CustomerRowCreate(ByVal writer As HtmlTextWriter, ByVal ctl As Control)
        Try
            Dim iLoop As Integer
            For iLoop = 0 To ctl.Controls.Count - 1
                If iLoop <> 2 Then
                    CType(ctl.Controls.Item(iLoop), WebControl).CssClass = "gHdr"
                    ctl.Controls.Item(iLoop).RenderControl(writer)
                End If
                If iLoop = 2 Then
                    writer.Write("<TH class=""gHdr"">")
                    writer.Write("<input id=""chbSelectAll"" type=""checkbox""")
                    writer.Write("onclick=""CustomerSelectAll(this);"" />")
                End If
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgRoute_ClientBind"
    Protected Sub ifgRoute_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgRoute.ClientBind
        Try
            If e.Parameters("btnType").ToString = "Fetch" Then
                Dim strEnquiryType As String = e.Parameters("EnquiryType").ToString
                Dim objEnquiry As New Enquiry

                Dim objCommon As New CommonData
                Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
                If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                    intDepotID = CInt(objCommon.GetHeadQuarterID())
                End If
                dsEnquiry = objEnquiry.pub_RouteDetailByDepotId(intDepotID)
                If dsEnquiry.Tables(EnquiryData._V_ROUTE).Rows.Count = 0 Then
                    e.OutputParameters.Add("norecordsfound", True)
                Else
                    e.OutputParameters.Add("norecordsfound", False)
                End If
                e.DataSource = dsEnquiry.Tables(EnquiryData._V_ROUTE)
            Else
                dsEnquiry.Tables(EnquiryData._V_ROUTE).Rows.Clear()
            End If
            CacheData(ENQUIRY, dsEnquiry)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgRoute_RowDataBound"
    Protected Sub ifgRoute_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgRoute.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim Routehyplnk As HyperLink
                Routehyplnk = CType(e.Row.Cells(2).Controls(0), HyperLink)
                Routehyplnk.Attributes.Add("onclick", String.Concat("openRouteReport('", drv.Item(EnquiryData.RT_ID).ToString(), "');"))
                Routehyplnk.NavigateUrl = "#"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                  MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCustomer_RowDataBound"
    Protected Sub ifgCustomer_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgCustomer.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim Customerhyplnk As HyperLink
                Customerhyplnk = CType(e.Row.Cells(2).Controls(0), HyperLink)
                Customerhyplnk.Attributes.Add("onclick", String.Concat("GenerateCustomerDocument('", drv.Item(EnquiryData.CSTMR_ID).ToString(), "');"))
                Customerhyplnk.NavigateUrl = "#"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex)
        End Try
 
    End Sub
#End Region
   
End Class
