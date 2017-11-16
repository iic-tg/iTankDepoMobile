
Partial Class Transportation_Transportation
    Inherits Pagebase

#Region "Declaration"
    Dim dsTransportation As New TransportationDataSet
    Dim dsTransportationRate As TransportationDataSet
    Private Const TRANSPORTATION As String = "TRANSPORTATION"
    Private Const TRANSPORTATION_DETAIL_RATE As String = "TRANSPORTATION_DETAIL_RATE"
    Private strMSGINSERT As String = " Created Successfully."
    Private strMSGUPDATE As String = " Updated Successfully."
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Transportation/Transportation.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                pvt_SetChangesMade()
                datRequestDate.Validator.ValueToCompare = Now.ToString("dd-MMM-yyyy")
                hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                Dim objCommondata As New CommonData
                Dim strSessionId As String = objCommondata.GetSessionID()
                Dim strActivityName As String = String.Empty
                If Not pub_GetQueryString("activityname") Is Nothing Then
                    strActivityName = pub_GetQueryString("activityname")
                End If
                objCommondata.FlushLockDataByActivityName(TransportationData.RQST_NO, strSessionId, strActivityName)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GetCustomerCurrency"
                    pvt_GetCustomerCurrency(e.GetCallbackValue("CustomerId"))
                Case "CreateTransportation"
                    pvt_CreateTransportation(e.GetCallbackValue("CustomerId"), _
                                             e.GetCallbackValue("RequestDate"), _
                                             e.GetCallbackValue("RouteId"), _
                                             e.GetCallbackValue("TransporterId"), _
                                             e.GetCallbackValue("ActivityLocationId"), _
                                             e.GetCallbackValue("ActivityId"), _
                                             e.GetCallbackValue("ActivityCode"), _
                                             e.GetCallbackValue("StatusId"), _
                                             e.GetCallbackValue("TripRate"), _
                                             e.GetCallbackValue("Remarks"), _
                                             e.GetCallbackValue("NoOfTrip"))
                Case "UpdateTransportation"
                    pvt_UpdateTransportation(e.GetCallbackValue("TransportationId"), _
                                             e.GetCallbackValue("CustomerId"), _
                                             e.GetCallbackValue("RequestDate"), _
                                             e.GetCallbackValue("RouteId"), _
                                             e.GetCallbackValue("TransporterId"), _
                                             e.GetCallbackValue("ActivityLocationId"), _
                                             e.GetCallbackValue("ActivityId"), _
                                             e.GetCallbackValue("ActivityCode"), _
                                             e.GetCallbackValue("StatusId"), _
                                             e.GetCallbackValue("TripRate"), _
                                             e.GetCallbackValue("Remarks"), _
                                             e.GetCallbackValue("NoOfTrip"), _
                                             e.GetCallbackValue("RequestNo"))
                Case "fnGetData"
                    pvt_GetData(e.GetCallbackValue("Mode"))
                Case "ValidateEquipment"
                    pvt_ValidateEquipment(e.GetCallbackValue("EquipmentId"), CInt(e.GetCallbackValue("GridIndex")), _
                                          e.GetCallbackValue("RowState"), e.GetCallbackValue("RequestNo"))
                Case "checkSelectBit"
                    pvt_CheckSelectBit()
                Case "calculateTripDetail"
                    pvt_CalculateTripDetail(e.GetCallbackValue("CustomerId"), _
                                            e.GetCallbackValue("RouteId"), _
                                            e.GetCallbackValue("TransporterId"), _
                                            e.GetCallbackValue("EquipmentStatusId"), _
                                            e.GetCallbackValue("TransporterCheck"))
                Case "cancelRequest"
                    pvt_CancelRequest(e.GetCallbackValue("TransportationId"), _
                                      e.GetCallbackValue("RequestNo"), _
                                      e.GetCallbackValue("Reason"))
                Case "stateChange"
                    pvt_StateChange(e.GetCallbackValue("EqpState"))
                Case "onTripRateChange"
                    pvt_onTripRateChange(e.GetCallbackValue("TripRate"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_onTripRateChange"
    Private Sub pvt_onTripRateChange(ByVal bv_strTripRate As String)
        Try
            Dim decRate As Decimal = 0
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "").ToString = "" Then
                decRate = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "")
            End If
            Dim decTriprate As Decimal = 0
            If bv_strTripRate <> Nothing Then
                decTriprate = CDec(decTriprate)
            End If

            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count > 0 Then
                dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows(0).Item(TransportationData.TRNSPRTR_TRP_RT_NC) = decTriprate
            End If
            pub_SetCallbackReturnValue("TotalRate", decRate)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_CancelRequest"
    Private Sub pvt_CancelRequest(ByVal bv_strTransportationId As String, _
                                  ByVal bv_strRequestNo As String, _
                                  ByVal bv_strReason As String)
        Try
            Dim objTransportation As New Transportation
            Dim objCommonData As New CommonData
            Dim intDepotId As Integer = objCommonData.GetDepotID()
            Dim strModifiedby As String = objCommonData.GetCurrentUserName()
            Dim strModifiedDate As String = objCommonData.GetCurrentDate()
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            Dim lngAuditLog As Long = 0
            lngAuditLog = objTransportation.pub_CancelRequest(CLng(bv_strTransportationId), _
                                                              bv_strRequestNo, _
                                                              dsTransportation, _
                                                              strModifiedby, _
                                                              CDate(strModifiedDate), _
                                                              bv_strReason, _
                                                              intDepotId)
            dsTransportation.AcceptChanges()
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_StateChange"
    Private Sub pvt_StateChange(ByVal bv_strEqpState As String)
        Try
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            If bv_strEqpState <> Nothing Then
                If bv_strEqpState.ToUpper = "EMPTY" Then
                    For Each drTransport As DataRow In dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows
                        drTransport.Item(TransportationData.EMPTY_SNGL_ID) = "109"
                        drTransport.Item(TransportationData.EMPTY_SNGL_CD) = "N"
                    Next
                Else
                    For Each drTransport As DataRow In dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows
                        drTransport.Item(TransportationData.EMPTY_SNGL_ID) = DBNull.Value
                        drTransport.Item(TransportationData.EMPTY_SNGL_CD) = DBNull.Value
                    Next
                End If
                Dim decTriprate As Decimal = 0
                Dim decEmptyRate As Decimal = 0
                Dim decFullRate As Decimal = 0
                If Not RetrieveData("EmptyRate") Is Nothing Then
                    decEmptyRate = CDec(RetrieveData("EmptyRate"))
                End If
                If Not RetrieveData("FullRate") Is Nothing Then
                    decFullRate = CDec(RetrieveData("FullRate"))
                End If

                If bv_strEqpState.ToUpper = "EMPTY" Then
                    decTriprate = decEmptyRate
                Else                    decTriprate = decFullRate
                End If
                Dim decRate As Decimal = 0
                If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "").ToString = "" Then
                    decRate = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "")
                End If
                If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count > 0 Then
                    dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows(0).Item(TransportationData.TRNSPRTR_TRP_RT_NC) = decTriprate
                End If
                CacheData(TRANSPORTATION, dsTransportation)
                pub_SetCallbackReturnValue("EmptyTripRate", RetrieveData("EmptyRate"))
                pub_SetCallbackReturnValue("FullTripRate", RetrieveData("FullRate"))
                pub_SetCallbackReturnValue("TotalRate", decRate)
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        Try
            CommonWeb.pub_AttachHasChanges(lkpCustomer)
            CommonWeb.pub_AttachHasChanges(lkpRoute)
            CommonWeb.pub_AttachDescMaxlength(txtRemarks)
            CommonWeb.pub_AttachHasChanges(datRequestDate)
            CommonWeb.pub_AttachHasChanges(lkpStatus)

            pub_SetGridChanges(ifgTransportation, "tabTransportation")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_checkDoubleTrip"
    Private Function pvt_checkDoubleTrip() As Integer
        Try
            Dim intDoubleTrip As Integer = 0
            Dim intRemainder As Integer = 0
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='N'")).ToString = "" Then
                intDoubleTrip = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='N'"))
            End If
            If intDoubleTrip <> 0 Then
                Math.DivRem(intDoubleTrip, 2, intRemainder)
            End If
            Return intRemainder
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pvt_CreateTransportation"
    Private Sub pvt_CreateTransportation(ByVal bv_strCustomerId As String, _
                                         ByVal bv_strRequestDate As String, _
                                         ByVal bv_strRouteId As String, _
                                         ByVal bv_strTransporterId As String, _
                                         ByVal bv_strActivityLocationId As String, _
                                         ByVal bv_strActivityId As String, _
                                         ByVal bv_strActivityCode As String, _
                                         ByVal bv_strStatusId As String, _
                                         ByVal bv_strTripRate As String, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strNoOfTrip As String)
        Try
            Dim objTransportation As New Transportation
            Dim objCommonData As New CommonData
            Dim dtTransaction As New DataTable
            Dim sbrDateValidation As New StringBuilder

            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            Dim intDoubleTrip As Integer = 0
            intDoubleTrip = pvt_checkDoubleTrip()
            If intDoubleTrip <> 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError(String.Concat("Two Equipments of Empty Single ", """N""", " make one trip, Please select Empty Single ", """N""", " for one more Equipment or change it to ", """Y""", "."))
                Exit Sub
            End If
            Dim intNoOfTrip As Integer = 0
            If bv_strNoOfTrip <> Nothing Then
                intNoOfTrip = CInt(bv_strNoOfTrip)
            End If
            Dim lngActivityLocationId As Long = 0
            Dim strStatusCode As String = String.Empty
            Dim strRequestNo As String = String.Empty
            Dim intDepotId As Integer = objCommonData.GetDepotID()
            Dim strModifiedby As String = objCommonData.GetCurrentUserName()
            Dim strModifiedDate As String = objCommonData.GetCurrentDate()
            Dim decTripRate As Decimal = 0
            If bv_strTripRate <> Nothing Then
                decTripRate = CDec(bv_strTripRate)
            End If

            If bv_strActivityLocationId <> Nothing Then
                lngActivityLocationId = CLng(bv_strActivityLocationId)
            End If

            For Each drTransportationDetail As DataRow In dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows
                If drTransportationDetail.RowState <> DataRowState.Deleted Then
                    Dim intDateCompare As Integer
                    If Not IsDBNull(drTransportationDetail.Item(TransportationData.JB_STRT_DT)) AndAlso Not IsDBNull(drTransportationDetail.Item(TransportationData.JB_END_DT)) Then
                        intDateCompare = CInt(DateTime.Compare(CDate(drTransportationDetail.Item(TransportationData.JB_STRT_DT)), CDate(drTransportationDetail.Item(TransportationData.JB_END_DT))))
                        If intDateCompare > 0 Then
                            sbrDateValidation.Append(drTransportationDetail.Item(TransportationData.EQPMNT_NO))
                            sbrDateValidation.Append(", ")
                        End If
                    ElseIf IsDBNull(drTransportationDetail.Item(TransportationData.JB_STRT_DT)) AndAlso Not IsDBNull(drTransportationDetail.Item(TransportationData.JB_END_DT)) Then
                        sbrDateValidation.Append(drTransportationDetail.Item(TransportationData.EQPMNT_NO))
                        sbrDateValidation.Append(", ")
                    End If
                End If
            Next

            If sbrDateValidation.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError(String.Concat("The Following Equipment Job End Date should be greater than Job Start Date ", sbrDateValidation.ToString()))
                Exit Sub
            End If

            Dim lngTransportationId As Long = objTransportation.pub_CreateTransportation(CLng(bv_strCustomerId), _
                                                                                         CLng(bv_strRouteId), _
                                                                                         CLng(bv_strTransporterId), _
                                                                                         lngActivityLocationId, _
                                                                                         CLng(bv_strActivityId), _
                                                                                         bv_strActivityCode, _
                                                                                         CLng(bv_strStatusId), _
                                                                                         strStatusCode, _
                                                                                         CDate(bv_strRequestDate), _
                                                                                         decTripRate, _
                                                                                         bv_strRemarks, _
                                                                                         strRequestNo, _
                                                                                         intDepotId, _
                                                                                         strModifiedby, _
                                                                                         CDate(strModifiedDate), _
                                                                                         intNoOfTrip, _
                                                                                         dsTransportation)
            dsTransportation.AcceptChanges()
            dsTransportationRate = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            pub_SetCallbackReturnValue("Message", String.Concat("Transportation Request : ", strRequestNo, strMSGINSERT))
            pub_SetCallbackReturnValue("TransportationId", lngTransportationId)
            pub_SetCallbackReturnValue("StatusId", bv_strStatusId)
            pub_SetCallbackReturnValue("StatusCode", strStatusCode)
            pub_SetCallbackReturnValue("RequestNo", strRequestNo)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateTransportation"
    Private Sub pvt_UpdateTransportation(ByVal bv_strTransportationId As String, _
                                         ByVal bv_strCustomerId As String, _
                                         ByVal bv_strRequestDate As String, _
                                         ByVal bv_strRouteId As String, _
                                         ByVal bv_strTransporterId As String, _
                                         ByVal bv_strActivityLocationId As String, _
                                         ByVal bv_strActivityId As String, _
                                         ByVal bv_strActivityCode As String, _
                                         ByVal bv_strStatusId As String, _
                                         ByVal bv_strTripRate As String, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strNoOfTrip As String, _
                                         ByVal bv_strRequestNo As String)
        Try
            Dim objTransportation As New Transportation
            Dim objCommonData As New CommonData
            Dim intDoubleTrip As Integer = 0
            Dim sbrDateValidation As New StringBuilder
            Dim intNoOfTrip As Integer = 0
            If bv_strNoOfTrip <> Nothing Then
                intNoOfTrip = CInt(bv_strNoOfTrip)
            End If
            Dim lngActivityLocationId As Long = 0
            Dim strTransportationStatusCode As String = String.Empty
            Dim intDepotId As Integer = objCommonData.GetDepotID()
            Dim strModifiedby As String = objCommonData.GetCurrentUserName()
            Dim strModifiedDate As String = objCommonData.GetCurrentDate()
            ''22373
            Dim strEquipmentNos As String = ""
            Dim strErrorMessage As String = ""
            ''
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            pvt_AddTotalTransportationRate(CType(ifgTransportation.DataSource, DataTable))
            Dim decTripRate As Decimal = 0
            If bv_strTripRate <> Nothing Then
                decTripRate = CDec(bv_strTripRate)
            End If
            intDoubleTrip = pvt_checkDoubleTrip()
            If intDoubleTrip <> 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError(String.Concat("Two Equipments of Empty Single ", """N""", " make one trip, Please select Empty Single ", """N""", " for one more Equipment or change it to ", """Y""", "."))
                Exit Sub
            End If
            If bv_strActivityLocationId <> Nothing Then
                lngActivityLocationId = CLng(bv_strActivityLocationId)
            End If
            For Each drTransportationDetail As DataRow In dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows
                If drTransportationDetail.RowState <> DataRowState.Deleted Then
                    Dim intDateCompare As Integer
                    If Not IsDBNull(drTransportationDetail.Item(TransportationData.JB_STRT_DT)) AndAlso Not IsDBNull(drTransportationDetail.Item(TransportationData.JB_END_DT)) Then
                        intDateCompare = CInt(DateTime.Compare(CDate(drTransportationDetail.Item(TransportationData.JB_STRT_DT)), CDate(drTransportationDetail.Item(TransportationData.JB_END_DT))))
                        If intDateCompare > 0 Then
                            sbrDateValidation.Append(drTransportationDetail.Item(TransportationData.EQPMNT_NO))
                            sbrDateValidation.Append(", ")
                        End If
                    ElseIf IsDBNull(drTransportationDetail.Item(TransportationData.JB_STRT_DT)) AndAlso Not IsDBNull(drTransportationDetail.Item(TransportationData.JB_END_DT)) Then
                        sbrDateValidation.Append(drTransportationDetail.Item(TransportationData.EQPMNT_NO))
                        sbrDateValidation.Append(", ")
                    End If
                    ''22373''
                    If strEquipmentNos <> String.Empty Then
                        strEquipmentNos = String.Concat(strEquipmentNos, ",'", drTransportationDetail.Item(TransportationData.EQPMNT_NO), "'")
                    Else
                        strEquipmentNos = String.Concat("'", drTransportationDetail.Item(TransportationData.EQPMNT_NO), "'")
                    End If
                    ''
                End If
            Next

            If sbrDateValidation.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError(String.Concat("The Following Equipment Job End Date should be greater than Job Start Date ", sbrDateValidation.ToString()))
                Exit Sub
            End If


            ''22373'' Billing Finalize validation
            Dim objCommonUI As New CommonUI
            objCommonUI.pub_validateFinalizedInvoiceActivityWise(CommonUIData._TRANSPORTATION_CHARGE, intDepotId, bv_strTransportationId, "", strEquipmentNos, strErrorMessage, dsTransportation)
            If strErrorMessage <> "" Then
                pub_SetCallbackError("This action will not be allowed since one of the equipment(s) transportation invoice is already finalized.")
                pub_SetCallbackStatus(False)
                Exit Sub
            End If
            ''

            objTransportation.pub_UpdateTransportation(CLng(bv_strTransportationId), _
                                                       CLng(bv_strCustomerId), _
                                                       CLng(bv_strRouteId), _
                                                       CLng(bv_strTransporterId), _
                                                       lngActivityLocationId, _
                                                       CLng(bv_strActivityId), _
                                                       bv_strActivityCode, _
                                                       CLng(bv_strStatusId), _
                                                       strTransportationStatusCode, _
                                                       CDate(bv_strRequestDate), _
                                                       decTripRate, _
                                                       bv_strRemarks, _
                                                       bv_strRequestNo, _
                                                       intDepotId, _
                                                       strModifiedby, _
                                                       CDate(strModifiedDate), _
                                                       intNoOfTrip, _
                                                       dsTransportation)
            dsTransportation.AcceptChanges()
            dsTransportationRate = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            pub_SetCallbackReturnValue("TransportationId", bv_strTransportationId)
            pub_SetCallbackReturnValue("StatusId", bv_strStatusId)
            pub_SetCallbackReturnValue("StatusCode", strTransportationStatusCode)
            pub_SetCallbackReturnValue("Message", String.Concat("Transportation Request : ", bv_strRequestNo, strMSGUPDATE))
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetData()"
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim objCommonData As New CommonData
            Dim intDepotId = objCommonData.GetDepotID()
            Dim sbrTransportation As New StringBuilder
            Dim objTransportation As New Transportation
            Dim strDepotCurrency As String = String.Empty
            Dim strCustomerCurrency As String = String.Empty
            Dim dtCustomerTransportation As New DataTable
            Dim dsEqpmntType As New DataSet
            dtCustomerTransportation = objTransportation.pub_GetCustomerCurrencyByCustomerId(CLng(PageSubmitPane.pub_GetPageAttribute(TransportationData.CSTMR_ID)), intDepotId).Tables(TransportationData._V_CUSTOMER)
            If dtCustomerTransportation.Rows.Count > 0 Then
                strCustomerCurrency = dtCustomerTransportation.Rows(0).Item(TransportationData.CSTMR_CRRNCY_CD).ToString()
                sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCustomerCurrency, strCustomerCurrency))
            End If
            strDepotCurrency = objCommonData.GetDepotLocalCurrencyCode()

            If strDepotCurrency <> "" Then
                sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotCurrency, strDepotCurrency))
            Else
                sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotCurrency, ""))
            End If
            If PageSubmitPane.pub_GetPageAttribute(TransportationData.EXCHNG_RT_PR_UNT_NC) Is Nothing Then
                sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnExchangeRate, "0"))
            Else
                sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnExchangeRate, PageSubmitPane.pub_GetPageAttribute(TransportationData.EXCHNG_RT_PR_UNT_NC)))
            End If

            If PageSubmitPane.pub_GetPageAttribute(CustomerData.XML_BT) Is Nothing Then
                sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnXml, False))
            Else
                sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnXml, PageSubmitPane.pub_GetPageAttribute(CustomerData.XML_BT)))
            End If

            If bv_strMode = MODE_EDIT Then
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.CSTMR_CD) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpCustomer, "", ""))
                Else
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpCustomer, PageSubmitPane.pub_GetPageAttribute(TransportationData.CSTMR_ID), PageSubmitPane.pub_GetPageAttribute(TransportationData.CSTMR_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.RQST_NO) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnRequestNo, ""))
                Else
                    sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnRequestNo, PageSubmitPane.pub_GetPageAttribute(TransportationData.RQST_NO)))
                    sbrTransportation.Append("toggleTransportationDiv('" + PageSubmitPane.pub_GetPageAttribute(TransportationData.RQST_NO) + "');")
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.RQST_DT) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetTextDateValuesJSO(datRequestDate, ""))
                Else
                    sbrTransportation.Append(CommonWeb.GetTextDateValuesJSO(datRequestDate, CDate(PageSubmitPane.pub_GetPageAttribute(TransportationData.RQST_DT)).ToString("dd-MMM-yyyy").ToUpper()))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.RT_DSCRPTN_VC) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpRoute, "", ""))
                Else
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpRoute, PageSubmitPane.pub_GetPageAttribute(TransportationData.RT_ID), PageSubmitPane.pub_GetPageAttribute(TransportationData.RT_DSCRPTN_VC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.PCK_UP_LCTN_CD) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpPickupLocation, "", ""))
                Else
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpPickupLocation, PageSubmitPane.pub_GetPageAttribute(TransportationData.PCK_UP_LCTN_ID), PageSubmitPane.pub_GetPageAttribute(TransportationData.PCK_UP_LCTN_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.TRNSPRTR_CD) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpTransporter, "", ""))
                Else
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpTransporter, PageSubmitPane.pub_GetPageAttribute(TransportationData.TRNSPRTR_ID), PageSubmitPane.pub_GetPageAttribute(TransportationData.TRNSPRTR_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.DRP_OFF_LCTN_CD) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpDropLocation, "", ""))
                Else
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpDropLocation, PageSubmitPane.pub_GetPageAttribute(TransportationData.DRP_OFF_LCTN_ID), PageSubmitPane.pub_GetPageAttribute(TransportationData.DRP_OFF_LCTN_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.ACTVTY_LCTN_CD) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpActivityLocation, "", ""))
                Else
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpActivityLocation, PageSubmitPane.pub_GetPageAttribute(TransportationData.ACTVTY_LCTN_ID), PageSubmitPane.pub_GetPageAttribute(TransportationData.ACTVTY_LCTN_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.EQPMNT_STT_CD) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpActivity, "", ""))
                Else
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpActivity, PageSubmitPane.pub_GetPageAttribute(TransportationData.EQPMNT_STT_ID), PageSubmitPane.pub_GetPageAttribute(TransportationData.EQPMNT_STT_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.TRP_RT_NC) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetTextValuesJSO(txtTripRate, "0.00"))
                Else
                    sbrTransportation.Append(CommonWeb.GetTextValuesJSO(txtTripRate, PageSubmitPane.pub_GetPageAttribute(TransportationData.TRP_RT_NC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.RMRKS_VC) Is Nothing Then
                    sbrTransportation.Append(CommonWeb.GetTextValuesJSO(txtRemarks, ""))
                Else
                    sbrTransportation.Append(CommonWeb.GetTextValuesJSO(txtRemarks, PageSubmitPane.pub_GetPageAttribute(TransportationData.RMRKS_VC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransportationData.TRNSPRTTN_STTS_CD) Is Nothing Then
                    Dim bln028Exist As Boolean = False
                    Dim str028KeyName As String = objCommonData.GetConfigSetting("028", bln028Exist)
                    If bln028Exist Then
                        Dim strEnumId As String = objCommonData.GetEnumID("REQUEST_STATUS", str028KeyName)
                        sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, CType(strEnumId, Object), str028KeyName))
                    Else
                        sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, "", ""))
                    End If
                Else
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, PageSubmitPane.pub_GetPageAttribute(TransportationData.TRNSPRTTN_STTS_ID), PageSubmitPane.pub_GetPageAttribute(TransportationData.TRNSPRTTN_STTS_CD)))
                    CacheData("TransportationStatusId", PageSubmitPane.pub_GetPageAttribute(TransportationData.TRNSPRTTN_STTS_ID))
                End If
                sbrTransportation.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(TransportationData.TRNSPRTTN_ID), "');"))
                sbrTransportation.Append("setRequestNo('" + bv_strMode + "');")
                sbrTransportation.Append("setCheckDigit('" + PageSubmitPane.pub_GetPageAttribute(TransportationData.CHK_DGT_VLDTN_BT).ToString + "');")
                If CInt(PageSubmitPane.pub_GetPageAttribute(TransportationData.TRNSPRTTN_STTS_ID)) <> 91 Then
                    Dim strUserName As String = String.Empty
                    Dim strIpError As String = String.Empty
                    Dim strActivityLockName As String = String.Empty
                    strUserName = pvt_GetLockData(CStr(PageSubmitPane.pub_GetPageAttribute(TransportationData.RQST_NO)), strIpError, strActivityLockName)
                    sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnLockUserName, strUserName))
                    sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnIpError, strIpError))
                    sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnLockActivityName, strActivityLockName))
                End If
            Else
                sbrTransportation.Append(CommonWeb.GetHiddenTextValuesJSO(hdnRequestNo, "#"))
                Dim bln028Exist As Boolean = False
                Dim str028KeyName As String = objCommonData.GetConfigSetting("028", bln028Exist)
                If bln028Exist Then
                    Dim strEnumId As String = objCommonData.GetEnumID("REQUEST_STATUS", str028KeyName)
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, CType(strEnumId, Object), str028KeyName))
                Else
                    sbrTransportation.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, "", ""))
                End If
                sbrTransportation.Append("setRequestNo('" + bv_strMode + "');")
                sbrTransportation.Append(CommonWeb.GetTextDateValuesJSO(datRequestDate, (Date.Now).ToString("dd-MMM-yyyy").ToUpper()))
            End If
            CacheData(TRANSPORTATION, dsTransportation)
            pub_SetCallbackReturnValue("Message", sbrTransportation.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTransportationDetail_ClientBind"
    Protected Sub ifgTransportation_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgTransportation.ClientBind
        Dim objTransportation As New Transportation
        Try
            Dim objCommonData As New CommonData
            Dim dtTransportationDetailRate As New DataTable
            Dim dtTransportationDetail As New DataTable
            Dim dtTransportation As New DataTable
            Dim dtAdditionalRate As New DataTable
            Dim decTripsRate As Decimal = 0
            Dim intNoOfTrips As Integer = 0
            Dim intNoOfEquipment As Integer = 0
            Dim objCommonUI As New CommonUI()
            Dim intDepotID As Integer = objCommonData.GetDepotID()
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            Dim lngCustomerId As Long = 0
            Dim lngRouteId As Long = 0
            Dim dsEqpmntTyp As New DataSet
            Dim bln013EqTypeExists As Boolean
            Dim str013EqType As String = String.Empty
            Dim dsEqpmntType As New DataSet
            Dim objCommonConfig As New ConfigSetting()
            Dim intHQId As Integer
            If objCommonData.GetMultiLocationSupportConfig.ToLower = "true" Then
                intHQId = objCommonData.GetHeadQuarterID()
            End If
            If Not e.Parameters("Mode") Is Nothing Then
                If IsNothing(dsTransportation) Then
                    dsTransportation = New TransportationDataSet
                End If
                If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count = 0 Then
                    Dim drTransportation As DataRow = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).NewRow()
                    drTransportation.Item(TransportationData.TRNSPRTR_TRP_RT_NC) = 0
                    dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Add(drTransportation)
                End If
                dtAdditionalRate = objTransportation.pub_GetAdditionalChargeRateByDepotId(intHQId).Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE)
                dsTransportation.Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE).Merge(dtAdditionalRate)
                If e.Parameters("Mode") = "ReBind" Then
                    dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
                ElseIf e.Parameters("Mode") = "new" Then
                    str013EqType = objCommonData.GetConfigSetting("013", bln013EqTypeExists)
                    bln013EqTypeExists = objCommonConfig.IsKeyExists

                    If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows.Count = 0 Then
                        Dim drTransportation As DataRow = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).NewRow()
                        If Not str013EqType = "" Then
                            dsEqpmntType = objCommonUI.pub_GetEquipmentType(str013EqType, intHQId)
                            If dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                                drTransportation.Item(TransportationData.TRNSPRTTN_DTL_ID) = CommonWeb.GetNextIndex(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL), TransportationData.TRNSPRTTN_DTL_ID)
                                drTransportation.Item(TransportationData.EQPMNT_TYP_ID) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                                drTransportation.Item(TransportationData.EQPMNT_TYP_CD) = str013EqType
                                drTransportation.Item(TransportationData.JB_STRT_DT) = (Date.Now).ToString("dd-MMM-yyyy").ToUpper()
                            End If
                        End If
                        dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows.Add(drTransportation)
                        pvt_BindTransportationDetailRate(0, drTransportation.Item(TransportationData.TRNSPRTTN_DTL_ID), dsTransportation)
                    End If

                ElseIf e.Parameters("Mode") = "bind" Then
                    Dim strTransportationId As String = e.Parameters("TransportationID").ToString
                    dtTransportationDetail = objTransportation.pub_GetTransportationDetailByTransportationID(strTransportationId).Tables(TransportationData._V_TRANSPORTATION_DETAIL)
                    dtTransportation = objTransportation.pub_GetTransportationByTransportationID(CLng(strTransportationId), intDepotID).Tables(TransportationData._V_TRANSPORTATION)
                    dtTransportationDetailRate = objTransportation.pub_GetTransportationDetailRateById(CLng(strTransportationId)).Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE)
                    dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Merge(dtTransportationDetail)
                    dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Merge(dtTransportation)
                    dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(dtTransportationDetailRate)
                End If
                If Not e.Parameters("TransportationStatusId") Is Nothing AndAlso e.Parameters("TransportationStatusId").ToString <> "" Then
                    If CLng(e.Parameters("TransportationStatusId")) = 91 OrElse CLng(e.Parameters("TransportationStatusId")) = 92 Then
                        CacheData("TransportationStatusId", e.Parameters("TransportationStatusId"))
                        ifgTransportation.AllowAdd = False
                        ifgTransportation.AllowDelete = False
                        ifgTransportation.ActionButtons(0).Visible = False
                    Else                        ifgTransportation.AllowAdd = True
                        ifgTransportation.AllowDelete = True
                        ifgTransportation.ActionButtons(0).Visible = True
                    End If
                End If
                If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows.Count = 0 Then
                    If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows.Count = 0 Then
                        Dim drTransportation As DataRow = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).NewRow()
                        If Not str013EqType = "" Then
                            dsEqpmntType = objCommonUI.pub_GetEquipmentType(str013EqType, intDepotID)
                            If dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                                drTransportation.Item(TransportationData.TRNSPRTTN_DTL_ID) = CommonWeb.GetNextIndex(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL), TransportationData.TRNSPRTTN_DTL_ID)
                                drTransportation.Item(TransportationData.EQPMNT_TYP_ID) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                                drTransportation.Item(TransportationData.EQPMNT_TYP_CD) = str013EqType
                                drTransportation.Item(TransportationData.JB_STRT_DT) = (Date.Now).ToString("dd-MMM-yyyy").ToUpper()
                            End If
                        End If
                        dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows.Add(drTransportation)
                    End If
                End If
                e.DataSource = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL)
                If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count > 0 Then
                    dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows(0).Item(TransportationData.TRNSPRTR_TRP_RT_NC) = CDec(e.Parameters("TripRate"))
                End If
                CacheData(TRANSPORTATION, dsTransportation)
                If e.Parameters("Mode").ToString.ToLower <> "new" Then
                    pvt_CalculateTripsDetails(intNoOfTrips, intNoOfEquipment, decTripsRate)
                End If
                e.OutputParameters.Add("NoofTrips", intNoOfTrips)
                e.OutputParameters.Add("NoofEquipment", intNoOfEquipment)
                e.OutputParameters.Add("TotalTripsRate", decTripsRate)
                'Else
                '    If e.Parameters("Mode") = "new" Then
                '        dsTransportation = New TransportationDataSet
                '        str013EqType = objCommonData.GetConfigSetting("013", bln013EqTypeExists)
                '        bln013EqTypeExists = objCommonConfig.IsKeyExists

                '        ' If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows.Count = 0 Then
                '        Dim drTransportation As DataRow = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).NewRow()
                '        If Not str013EqType = "" Then
                '            dsEqpmntType = objCommonUI.pub_GetEquipmentType(str013EqType, intDepotID)
                '            If dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                '                drTransportation.Item(TransportationData.TRNSPRTTN_DTL_ID) = CommonWeb.GetNextIndex(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL), TransportationData.TRNSPRTTN_DTL_ID)
                '                drTransportation.Item(TransportationData.EQPMNT_TYP_ID) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                '                drTransportation.Item(TransportationData.EQPMNT_TYP_CD) = str013EqType
                '                drTransportation.Item(TransportationData.JB_STRT_DT) = (Date.Now).ToString("dd-MMM-yyyy").ToUpper()
                '            End If
                '        End If
                '        dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows.Add(drTransportation)
                '        pvt_BindTransportationDetailRate(0, drTransportation.Item(TransportationData.TRNSPRTTN_DTL_ID), dsTransportation)
                '        ifgTransportation.AllowAdd = True
                '        ifgTransportation.AllowDelete = True
                '        ifgTransportation.ActionButtons(0).Visible = True
                '        If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count > 0 Then
                '            dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows(0).Item(TransportationData.TRNSPRTR_TRP_RT_NC) = CDec(e.Parameters("TripRate"))
                '        End If
                '        e.OutputParameters.Add("NoofTrips", intNoOfTrips)
                '        e.OutputParameters.Add("NoofEquipment", intNoOfEquipment)
                '        e.OutputParameters.Add("TotalTripsRate", decTripsRate)
                '    End If
                'End If
                '  End If

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTransportationDetail_RowDataBound"
    Protected Sub ifgTransportation_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgTransportation.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hypRate As HyperLink
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                hypRate = CType(e.Row.Cells(4).Controls(0), HyperLink)
                hypRate.Attributes.Add("onclick", String.Concat("onClickRate(", e.Row.RowIndex, ");return false;"))
                hypRate.NavigateUrl = "#"
                hypRate.Text = "Add/Edit"
                hypRate.ToolTip = "Additional Rate"

                If Not e.Row.DataItem Is Nothing Then
                    If Not RetrieveData("EquipmentStateCode") = Nothing Then
                        If RetrieveData("EquipmentStateCode").ToString.ToUpper = "EMPTY" Then
                            CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        Else
                            CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        End If
                    End If

                    If Not IsDBNull(drv.Item(TransportationData.BLLNG_FLG)) Then
                        If drv.Item(TransportationData.BLLNG_FLG).ToString = "Y" Then
                            CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False
                        End If
                    End If
                End If
                If e.Row.RowIndex > 2 Then
                    Dim lkpControlType As iLookup
                    lkpControlType = CType(DirectCast(DirectCast(e.Row.Cells(3), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpControlType.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom

                    Dim lkpControlCargo As iLookup
                    lkpControlCargo = CType(DirectCast(DirectCast(e.Row.Cells(1), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpControlCargo.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTransportation_RowInserting"
    Protected Sub ifgTransportation_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgTransportation.RowInserting
        Try
            Dim lngTransportDetailId As Long = 0
            Dim lngTransportationId As Long = 0
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            lngTransportDetailId = CommonWeb.GetNextIndex(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL), TransportationData.TRNSPRTTN_DTL_ID)
            e.Values(TransportationData.TRNSPRTTN_DTL_ID) = lngTransportDetailId
            If Not e.InputParamters("TransportationId") Is Nothing Then
                lngTransportationId = CLng(e.InputParamters("TransportationId"))
            End If
            pvt_BindTransportationDetailRate(lngTransportationId, lngTransportDetailId, dsTransportation)
            CacheData("EquipmentStateCode", e.InputParamters("EquipmentStateCode"))
            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count > 0 Then
                dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows(0).Item(TransportationData.TRNSPRTR_TRP_RT_NC) = CDec(e.InputParamters("TripRate"))
            End If
            CacheData(TRANSPORTATION, dsTransportation)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgTransportation_RowInserted"
    Protected Sub ifgTransportation_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgTransportation.RowInserted
        Try
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            Dim intEmptyTrips As Integer = 0
            Dim intFullTrips As Integer = 0
            Dim intNoofTrips As Integer = 0
            Dim intNoofEquipment As Integer = 0
            Dim decTripsRate As Decimal = 0
            pvt_CalculateTripsDetails(intNoofTrips, intNoofEquipment, decTripsRate)

            e.OutputParamters.Add("NoofTrips", intNoofTrips)
            e.OutputParamters.Add("NoofEquipment", intNoofEquipment)
            e.OutputParamters.Add("TotalTripsRate", decTripsRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgTransportation_RowUpdating"
    Protected Sub ifgTransportation_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgTransportation.RowUpdating
        Try
            Dim lngTransportDetailId As Long = 0
            Dim lngTransportationId As Long = 0
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)

            If Not e.InputParamters("TransportationId") Is Nothing Then
                lngTransportationId = CLng(e.InputParamters("TransportationId"))
            End If
            CacheData("EquipmentStateCode", e.InputParamters("EquipmentStateCode"))
            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count > 0 Then
                dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows(0).Item(TransportationData.TRNSPRTR_TRP_RT_NC) = CDec(e.InputParamters("TripRate"))
            End If
            CacheData(TRANSPORTATION, dsTransportation)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgTransportation_RowUpdated"
    Protected Sub ifgTransportation_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgTransportation.RowUpdated
        Try
            Dim intNoofTrips As Integer = 0
            Dim intNoofEquipment As Integer = 0
            Dim decTripsRate As Decimal = 0

            pvt_CalculateTripsDetails(intNoofTrips, intNoofEquipment, decTripsRate)
            e.OutputParamters.Add("NoofTrips", intNoofTrips)
            e.OutputParamters.Add("NoofEquipment", intNoofEquipment)
            e.OutputParamters.Add("TotalTripsRate", decTripsRate)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgTransportation_RowDeleting"
    Protected Sub ifgTransportation_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgTransportation.RowDeleting
        Try
            Dim dsTransportationTemp As New TransportationDataSet
            Dim dtTransportationTemp As New DataTable
            Dim dtTransportation As New DataTable
            Dim objTransportation As New Transportation
            Dim objCommonData As New CommonData
            Dim lngTransportationId As Long = CLng(e.InputParamters("TransportationId"))
            CacheData("lngTransportationId", lngTransportationId)
            Dim lngTransportationDetailId As Long = CLng(e.Keys(TransportationData.TRNSPRTTN_DTL_ID))
            Dim intDepotID As Integer = CInt(objCommonData.GetDepotID())
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            dsTransportationTemp = objTransportation.pub_GetTransportationDetailRateByTransportationDetailId(lngTransportationDetailId)

            Dim drBilled As DataRow() = Nothing
            drBilled = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", e.Keys(TransportationData.TRNSPRTTN_DTL_ID), " AND ", TransportationData.BLLNG_FLG, "= 'Y'"))
            If drBilled.Length > 0 Then
                e.Cancel = True
                e.OutputParamters("Delete") = String.Concat("Equipment No: ", e.Values.Item(TransportationData.EQPMNT_NO).ToString, " cannot be deleted.")
                Exit Sub
            End If

            If dsTransportationTemp.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows.Count <> 0 Then
                For Each drTransportationDetailRateTemp As DataRow In dsTransportationTemp.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows
                    drTransportationDetailRateTemp.Delete()
                Next
            End If

            If dsTransportationTemp.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows.Count <> 0 Then
                For Each drTransportationDetail As DataRow In dsTransportationTemp.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows
                    drTransportationDetail.Delete()
                Next
            End If
            dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(dsTransportationTemp.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE))
            dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Merge(dsTransportationTemp.Tables(TransportationData._V_TRANSPORTATION_DETAIL))


            For Each drTransportationDetailRate As DataRow In dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", lngTransportationDetailId))
                drTransportationDetailRate.Delete()
            Next

            CacheData(TRANSPORTATION, dsTransportation)
            dsTransportationTemp = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            CacheData(TRANSPORTATION_DETAIL_RATE, dsTransportationTemp)
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgTransportation.PageSize * ifgTransportation.PageIndex + e.RowIndex
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            Dim dtTransTemp As Data.DataTable = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Copy

            If CType(ifgTransportation.DataSource, DataTable).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.OutputParamters("Delete") = String.Concat("Equipment No: ", dtTransTemp.Rows(intRowIndex).Item(TransportationData.EQPMNT_NO).ToString, " has been deleted from Transportation Detail. Click submit to save changes.")
                Exit Sub
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                        MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "ifgTransportation_RowDeleted"
    Protected Sub ifgTransportation_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgTransportation.RowDeleted
        Try
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            Dim intEmptyTrips As Integer = 0
            Dim intFullTrips As Integer = 0
            Dim intNoofTrips As Integer = 0
            Dim intNoofEquipment As Integer = 0
            Dim decTripsRate As Decimal = 0
            Dim bln013EqTypeExists As Boolean
            Dim str013EqType As String
            Dim bln033EqState As Boolean
            Dim str033EqState As String
            Dim objCommonConfig As New ConfigSetting()
            Dim objCommonData As New CommonData
            Dim objCommonUI As New CommonUI()
            Dim intDepotID As Integer = objCommonData.GetDepotID()
            Dim lngTransportationDetailId As Long = 0
            Dim dtTransportationDetail As New DataTable
            Dim dtTransportation As New DataTable
            Dim strEquipmentStatus As String = String.Empty
            dtTransportation = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Copy()
            dtTransportation.AcceptChanges()
            If Not RetrieveData("EquipmentStateCode") Is Nothing Then
                strEquipmentStatus = CStr(RetrieveData("EquipmentStateCode"))
            End If
            If dtTransportation.Rows.Count = 0 Then
                Dim dsEqpmntType As New DataSet

                str013EqType = objCommonData.GetConfigSetting("013", bln013EqTypeExists)
                bln013EqTypeExists = objCommonConfig.IsKeyExists

                str033EqState = objCommonData.GetConfigSetting("033", bln033EqState)
                bln033EqState = objCommonConfig.IsKeyExists

                If dtTransportation.Rows.Count = 0 Then
                    Dim drTransportation As DataRow = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).NewRow()
                    If Not str013EqType = "" Then
                        dsEqpmntType = objCommonUI.pub_GetEquipmentType(str013EqType, intDepotID)
                        If dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                            drTransportation.Item(TransportationData.TRNSPRTTN_DTL_ID) = CommonWeb.GetNextIndex(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL), TransportationData.TRNSPRTTN_DTL_ID)
                            lngTransportationDetailId = drTransportation.Item(TransportationData.TRNSPRTTN_DTL_ID)
                            drTransportation.Item(TransportationData.EQPMNT_TYP_ID) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                            drTransportation.Item(TransportationData.EQPMNT_TYP_CD) = str013EqType
                            If strEquipmentStatus.ToUpper = "EMPTY" Then
                                drTransportation.Item(TransportationData.EMPTY_SNGL_CD) = str033EqState
                            End If
                            drTransportation.Item(TransportationData.JB_STRT_DT) = (Date.Now).ToString("dd-MMM-yyyy").ToUpper()
                        End If
                    End If
                    dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows.Add(drTransportation)
                End If
                Dim lngTransportationId As Long = 0
                If Not IsNothing(RetrieveData("lngTransportationId")) Then
                    lngTransportationId = CLng(RetrieveData("lngTransportationId"))
                End If
                pvt_BindTransportationDetailRate(lngTransportationId, lngTransportationDetailId, dsTransportation)
            End If

            pvt_CalculateTripsDetails(intNoofTrips, intNoofEquipment, decTripsRate)
            e.OutputParamters.Add("NoofTrips", intNoofTrips)
            e.OutputParamters.Add("NoofEquipment", intNoofEquipment)
            e.OutputParamters.Add("TotalTripsRate", decTripsRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CalculateTripDetail"
    Private Sub pvt_CalculateTripDetail(ByVal bv_strCustomerId As String, _
                                        ByVal bv_strRouteId As String, _
                                        ByVal bv_strTransporterId As String, _
                                        ByVal bv_strEquipmentStatusCode As String, _
                                        ByVal bv_strTransporterCheck As String)
        Try
            Dim decEmptyRate As Decimal = 0
            Dim decFullRate As Decimal = 0
            Dim decAdditionalRate As Decimal = 0
            Dim lngCustomerId As Long = 0
            Dim lngRouteId As Long = 0
            Dim decRate As Decimal = 0
            Dim lngTransporterId As Long = 0
            Dim blnTransporter As Boolean = False
            Dim lngEquipmentStatusCode As String
            Dim dsTrips As New TransportationDataSet
            Dim objCommon As New CommonData
            Dim objTransportation As New Transportation
            Dim intDepotID As Integer = objCommon.GetDepotID()
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = objCommon.GetHeadQuarterID()
            End If
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            If bv_strCustomerId <> Nothing Then
                lngCustomerId = CLng(bv_strCustomerId)
            End If
            If bv_strRouteId <> Nothing Then
                lngRouteId = CLng(bv_strRouteId)
            End If
            If bv_strEquipmentStatusCode <> Nothing Then
                lngEquipmentStatusCode = bv_strEquipmentStatusCode
            End If
            If bv_strTransporterId <> Nothing Then
                lngTransporterId = bv_strTransporterId
            End If
            If lngCustomerId <> 0 AndAlso lngRouteId <> 0 Then
                dsTrips = objTransportation.pub_GetCustomerTransportationByCustomerId(lngCustomerId, lngRouteId, bv_strEquipmentStatusCode)
                If dsTrips.Tables(TransportationData._V_CUSTOMER_TRANSPORTATION).Rows.Count > 0 Then
                    decEmptyRate = dsTrips.Tables(TransportationData._V_CUSTOMER_TRANSPORTATION).Rows(0).Item(TransportationData.EMPTY_TRP_RT_NC)
                    decFullRate = dsTrips.Tables(TransportationData._V_CUSTOMER_TRANSPORTATION).Rows(0).Item(TransportationData.FLL_TRP_RT_NC)
                Else
                    dsTrips = objTransportation.pub_GetTripRateByRouteId(lngRouteId, lngTransporterId, intDepotID)
                    If dsTrips.Tables(TransportationData._V_TRANSPORTER_ROUTE_DETAIL).Rows.Count > 0 Then
                        '    blnTransporter = True
                        decEmptyRate = dsTrips.Tables(TransportationData._V_TRANSPORTER_ROUTE_DETAIL).Rows(0).Item(TransportationData.EMPTY_TRP_CSTMR_RT_NC)
                        decFullRate = dsTrips.Tables(TransportationData._V_TRANSPORTER_ROUTE_DETAIL).Rows(0).Item(TransportationData.FLL_TRP_CSTMR_RT_NC)
                    End If
                End If
            End If
            'If bv_strTransporterCheck Is Nothing Then
            dsTrips = objTransportation.pub_GetTransporterByRouteId(lngRouteId, intDepotID)
            If dsTrips.Tables(TransportationData._V_TRANSPORTER_ROUTE_DETAIL).Rows.Count > 0 Then
                blnTransporter = True
                '  End If
            End If
            If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "").ToString = "" Then
                decRate = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "")
            End If

            Dim decTripRate As Decimal
            If bv_strEquipmentStatusCode = "120" Then
                decTripRate = decEmptyRate
            Else
                decTripRate = decFullRate
            End If
            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count > 0 Then
                dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows(0).Item(TransportationData.TRNSPRTR_TRP_RT_NC) = decTripRate
            End If

            CacheData("EmptyRate", decEmptyRate)
            CacheData("FullRate", decFullRate)
            CacheData(TRANSPORTATION, dsTransportation)
            pub_SetCallbackReturnValue("EmptyTripRate", decEmptyRate)
            pub_SetCallbackReturnValue("FullTripRate", decFullRate)
            pub_SetCallbackReturnValue("TotalRate", decRate)
            pub_SetCallbackReturnValue("NoRecords", blnTransporter)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_BindTransportationDetailRate"
    Private Sub pvt_BindTransportationDetailRate(ByVal bv_i64TransportationId As Int64, _
                                                 ByVal bv_i64TransportationDetailId As Int64, _
                                                 ByRef br_dsTransportation As TransportationDataSet)
        Try
            Dim dtTransportationDetailRate As New DataTable
            Dim drTransportationDetailRate As DataRow = Nothing
            Dim lngNextIndex As Long = 0
            dtTransportationDetailRate = br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Clone()
            Dim dtTemp As New DataTable
            Dim drSelect As DataRow()
            drSelect = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select("", String.Concat(TransportationData.TRNSPRTTN_DTL_RT_ID, " ASC"))

            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows.Count > 0 Then
                If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows.Count - 1).RowState = DataRowState.Deleted Then
                    lngNextIndex = CLng(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows.Count - 1).Item(TransportationData.TRNSPRTTN_DTL_RT_ID, DataRowVersion.Original)) + 1
                Else
                    For Each dr As DataRow In drSelect
                        If dr.RowState <> DataRowState.Deleted Then
                            lngNextIndex = CLng(dr.Item(TransportationData.TRNSPRTTN_DTL_RT_ID))
                        End If
                    Next
                    lngNextIndex = lngNextIndex + 1
                End If
            Else
                lngNextIndex = 1
            End If
            For Each drAdditionalRate As DataRow In dsTransportation.Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE).Rows
                drTransportationDetailRate = dtTransportationDetailRate.NewRow()
                drTransportationDetailRate.Item(TransportationData.TRNSPRTTN_DTL_RT_ID) = lngNextIndex
                drTransportationDetailRate.Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationId
                drTransportationDetailRate.Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_i64TransportationDetailId
                drTransportationDetailRate.Item(TransportationData.ADDTNL_CHRG_RT_ID) = drAdditionalRate.Item(TransportationData.ADDTNL_CHRG_RT_ID)
                drTransportationDetailRate.Item(TransportationData.ADDTNL_CHRG_RT_CD) = drAdditionalRate.Item(TransportationData.ADDTNL_CHRG_RT_CD)
                drTransportationDetailRate.Item(TransportationData.ADDTNL_CHRG_RT_NC) = drAdditionalRate.Item(TransportationData.RT_NC)
                drTransportationDetailRate.Item(TransportationData.DFLT_BT) = drAdditionalRate.Item(TransportationData.DFLT_BT)
                dtTransportationDetailRate.Rows.Add(drTransportationDetailRate)
                lngNextIndex = lngNextIndex + 1
            Next
            dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(dtTransportationDetailRate)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_GetCustomerCurrency"
    Private Sub pvt_GetCustomerCurrency(ByVal bv_strCustomerId As String)
        Dim objTransportation As New Transportation
        Dim strCurrency As String = String.Empty
        Dim objCommonData As New CommonData
        Try
            Dim dtCustomer As New DataTable
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            dtCustomer = objTransportation.pub_GetCustomerCurrencyByCustomerId(CLng(bv_strCustomerId), CInt(objCommonData.GetDepotID())).Tables(TransportationData._V_CUSTOMER)
            If dtCustomer.Rows.Count > 0 Then
                dsTransportation.Tables(TransportationData._V_CUSTOMER).Merge(dtCustomer)
                If dsTransportation.Tables(TransportationData._V_CUSTOMER).Rows.Count > 0 Then
                    With dsTransportation.Tables(TransportationData._V_CUSTOMER).Rows(0)
                        If (Not .Item(TransportationData.CSTMR_CRRNCY_CD) Is Nothing) And (Not IsDBNull(.Item(TransportationData.CSTMR_CRRNCY_CD))) Then
                            strCurrency = .Item(TransportationData.CSTMR_CRRNCY_CD).ToString()
                        End If
                    End With
                End If
            End If
            CacheData(TRANSPORTATION, dsTransportation)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Currency", strCurrency)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_CalculateTripsDetails"
    Private Sub pvt_CalculateTripsDetails(ByRef br_intNoOfTrips As Integer, _
                                          ByRef br_intNoOfEquipment As Integer, _
                                          ByRef br_decTripsRate As Decimal)
        Try
            Dim intSingleTrip As Integer = 0
            Dim intDoubleTrip As Integer = 0
            Dim intDefaultTrip As Integer = 0
            Dim decAdditionalRate As Decimal = 0
            Dim intDoubleTotalRate As Integer = 0
            Dim intDefaultTotalRate As Integer = 0
            Dim intRemainder As Integer = 0
            Dim intDivider As Integer = 0
            Dim intDefaultRemainder As Integer = 0
            Dim intDefaultDivider As Integer = 0
            Dim decStateRate As Decimal = 0
            Dim strEquipmentStatus As String = String.Empty

            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)

            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count > 0 Then
                decStateRate = CDec(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows(0).Item(TransportationData.TRNSPRTR_TRP_RT_NC))
            End If

            'If decStateRate = 0 AndAlso Not RetrieveData("TripRate") Is Nothing Then
            '    decStateRate = CDec(RetrieveData("TripRate"))
            'End If


            If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "").ToString = "" Then
                decAdditionalRate = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "")
            End If

            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Rows.Count > 0 Then
                strEquipmentStatus = dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Rows(0).Item(TransportationData.EQPMNT_STT_CD).ToString
            Else
                If Not RetrieveData("EquipmentStateCode") Is Nothing Then
                    strEquipmentStatus = CStr(RetrieveData("EquipmentStateCode"))
                End If
            End If

            If strEquipmentStatus.ToUpper = "EMPTY" Then
                If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='Y'")).ToString = "" Then
                    intSingleTrip = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='Y'"))
                End If
                If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='N'")).ToString = "" Then
                    intDoubleTrip = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='N'"))
                End If
                If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, " IS NULL")).ToString = "" Then
                    intDefaultTrip = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, " IS NULL"))
                End If
                'If intDoubleTrip <> 0 Then
                '    intDivider = Math.Truncate(intDoubleTrip / 2)
                '    Math.DivRem(intDoubleTrip, 2, intRemainder)
                '    intDoubleTotalRate = intRemainder + intDivider
                'End If

                intDefaultTrip = intDefaultTrip + intDoubleTrip

                If intDefaultTrip <> 0 Then
                    intDefaultDivider = Math.Truncate(intDefaultTrip / 2)
                    Math.DivRem(intDefaultTrip, 2, intDefaultRemainder)
                    intDefaultTotalRate = intDefaultDivider + intDefaultRemainder
                End If

                br_intNoOfTrips = intDoubleTotalRate + intSingleTrip + intDefaultTotalRate
            Else
                If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), "").ToString = "" Then
                    br_intNoOfTrips = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), "")
                End If
            End If
            br_decTripsRate = (br_intNoOfTrips * decStateRate) + decAdditionalRate

            If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.EQPMNT_NO, ")"), "").ToString = "" Then
                br_intNoOfEquipment = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.EQPMNT_NO, ")"), "")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateEquipment"
    Private Sub pvt_ValidateEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_intGridIndex As Integer, _
                                      ByVal bv_strRowstate As String, ByVal bv_strRequestNo As String)
        Try
            Dim blndsValid As Boolean
            Dim dtTransportation As New DataTable
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            dtTransportation = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL)
            Dim intResultIndex() As System.Data.DataRow = dtTransportation.Select(String.Concat(TransportationData.EQPMNT_NO, "='", bv_strEquipmentNo, "' "))
            Dim strExistEquipment As String = ""
            If intResultIndex.Length > 0 Then
                If dtTransportation.Rows.Count > bv_intGridIndex Then
                    If dtTransportation.Rows(bv_intGridIndex).RowState = Data.DataRowState.Deleted Then
                        strExistEquipment = String.Empty
                    ElseIf dtTransportation.Rows(bv_intGridIndex).RowState <> Data.DataRowState.Deleted Then
                        strExistEquipment = dtTransportation.Rows(bv_intGridIndex)(TransportationData.EQPMNT_NO).ToString
                    End If
                End If

                If bv_strEquipmentNo = strExistEquipment Then
                    blndsValid = True
                Else
                    blndsValid = False
                End If
            Else
                blndsValid = True
            End If
            'Checking whether the entered code is available in database
            Dim dsTransportationData As New TransportationDataSet
            Dim strJobEndDate As String = String.Empty
            Dim blnJobEndDate As Boolean
            If blndsValid = True Then
                If bv_strRowstate = "Added" Or bv_strRowstate = "Modified" Or bv_strRowstate = Nothing Then
                    Dim objTransportation As New Transportation
                    Dim objCommon As New CommonData
                    dsTransportationData = objTransportation.pub_GetTransportationDetailByEquipmentNo(bv_strEquipmentNo)
                    If dsTransportationData.Tables(TransportationData._TRANSPORTATION_DETAIL).Rows.Count > 0 Then
                        If Not IsDBNull(dsTransportationData.Tables(TransportationData._TRANSPORTATION_DETAIL).Rows(0).Item(TransportationData.JB_END_DT)) Then
                            blnJobEndDate = objTransportation.pub_ValidateJobEndDateByEquipmentNo(bv_strEquipmentNo)
                            strJobEndDate = CDate(dsTransportationData.Tables(TransportationData._TRANSPORTATION_DETAIL).Rows(0).Item(TransportationData.JB_END_DT)).ToString("dd-MMM-yyyy").ToUpper()
                        Else
                            blnJobEndDate = False
                        End If
                        If blnJobEndDate Then
                            blndsValid = True
                        Else
                            blndsValid = False
                        End If
                    Else
                        blndsValid = True
                    End If
                End If
            End If
            If blndsValid = True Then
                pub_SetCallbackReturnValue("bNotExists", "true")
                pub_SetCallbackReturnValue("jobEndDate", strJobEndDate)
            Else
                pub_SetCallbackReturnValue("bNotExists", "false")
                pub_SetCallbackReturnValue("jobEndDate", strJobEndDate)
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_CheckSelectBit"
    Private Sub pvt_CheckSelectBit()
        Try
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            Dim intCount As Integer = 0
            Dim blnSelect As Boolean
            If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.CHK_BT, "= 'True'")).ToString = "" Then
                intCount = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.CHK_BT, "= 'True'"))
            End If
            If intCount > 0 Then
                blnSelect = True
            Else
                blnSelect = False
            End If
            pub_SetCallbackReturnValue("blnSelect", blnSelect)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                         MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_AddTotalCustomerRate"
    Private Sub pvt_AddTotalTransportationRate(ByRef br_dtAdditionalChargeData As DataTable)
        Try
            Dim blnUpdated As Boolean = True
            Dim dsTransTemp As New TransportationDataSet
            dsTransTemp = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            If dsTransTemp IsNot Nothing Then
                Dim dtTemp As DataTable = dsTransTemp.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).GetChanges()
                If dtTemp IsNot Nothing Then
                    For Each dr As DataRow In dtTemp.Rows
                        Dim lngTransportationDetailId As Long = 0
                        If dr.RowState = DataRowState.Deleted Then
                            lngTransportationDetailId = CLng(dr.Item(TransportationData.TRNSPRTTN_DTL_ID, DataRowVersion.Original))
                        Else
                            lngTransportationDetailId = CLng(dr.Item(TransportationData.TRNSPRTTN_DTL_ID))
                        End If
                        Dim drs As DataRow() = br_dtAdditionalChargeData.Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", lngTransportationDetailId), "")
                        Dim dblCharges As Double = pvt_GetRate(lngTransportationDetailId, blnUpdated)
                        If blnUpdated And drs.Length > 0 Then
                            drs(0).Item(TransportationData.TTL_RT_NC) = dblCharges
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetCustomerRate"
    Private Function pvt_GetRate(ByVal bv_lngTransportationDetailId As Long, ByRef br_blnUpdated As Boolean) As Double
        Try
            Dim dblCharges As Double = 0
            dsTransportation = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            If Not dsTransportation Is Nothing Then
                For Each dr As DataRow In dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", bv_lngTransportationDetailId))
                    If Not dr.RowState = DataRowState.Deleted Then
                        If Not IsDBNull(dr.Item(TransportationData.ADDTNL_CHRG_RT_NC)) Then
                            dblCharges += CType(dr.Item(TransportationData.ADDTNL_CHRG_RT_NC), Double)
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
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "pvt_GetLockData"
    Private Function pvt_GetLockData(ByVal bv_strRequestNo As String, _
                                     ByRef br_strIpError As String, _
                                     ByRef br_strActivityName As String) As String
        Try
            Dim objCommonData As New CommonData
            Dim strErrorMessage As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim strIpAddress As String = GetClientIPAddress()
            Dim strCurrentIpAddress As String = String.Empty
            Dim strUserName As String = String.Empty
            If Not pub_GetQueryString("activityname") Is Nothing Then
                br_strActivityName = pub_GetQueryString("activityname")
            End If
            blnLockData = objCommonData.pub_GetLockData(False, bv_strRequestNo, strUserName, br_strActivityName, strIpAddress, True, TransportationData.RQST_NO)

            If blnLockData Then
                strCurrentIpAddress = GetClientIPAddress()
                If strCurrentIpAddress = strIpAddress Then
                    br_strIpError = "true"
                Else
                    br_strIpError = "false"
                End If
            End If
            Return strUserName
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
            Return String.Empty
        End Try
    End Function
#End Region

End Class