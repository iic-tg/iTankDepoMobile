
Partial Class Operations_ReserveBooking
    Inherits Pagebase

    Private ds As ReserveBookingDataSet
    Private Const RESERVEBOOKING As String = "RESERVEBOOKING"

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                datDateOfAcceptance.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType

                Case "fnGetData"
                    pvt_GetData(e.GetCallbackValue("mode"))

                Case "CreateReserveBooking"
                    Create_ReserveBooking(e.GetCallbackValue("CustomerId"), e.GetCallbackValue("CustomerCode"), e.GetCallbackValue("DateOfAcceptance"), _
                                          e.GetCallbackValue("BookingAuthNo"), e.GetCallbackValue("Consignee"))
                Case "UpdateReserveBooking"
                    Update_ReserveBooking(e.GetCallbackValue("ReserveBookingId"), e.GetCallbackValue("CustomerId"), e.GetCallbackValue("CustomerCode"), e.GetCallbackValue("DateOfAcceptance"), _
                                          e.GetCallbackValue("BookingAuthNo"), e.GetCallbackValue("Consignee"))
                Case "UpdateReserveBooking"
                    '  GateOutRequest_Update(e.GetCallbackValue("GridMode"))
                Case "GetEquipmentInformation"
                    Get_EquipmentInformation(e.GetCallbackValue("CustomerId"))
                Case "Validate_BookedQuantity"
                    Validate_BookedQuantity(e.GetCallbackValue("bookedQty"))
                Case "Validate_MinimumSelection"
                    ValidateMinimum_Selection()

            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try

            Dim sbReserveBooking As New StringBuilder
          

            If bv_strMode = MODE_NEW Then

                sbReserveBooking.Append(CommonWeb.GetTextValuesJSO(txtbookingAuthNo, ""))
                sbReserveBooking.Append(CommonWeb.GetTextValuesJSO(datDateOfAcceptance, ""))
                sbReserveBooking.Append(CommonWeb.GetTextValuesJSO(txtConsignee, ""))
                sbReserveBooking.Append(CommonWeb.GetLookupValuesJSO(lkpCustomer, "", ""))

                ds = New ReserveBookingDataSet


            ElseIf bv_strMode = MODE_EDIT Then

                Dim objReserveBooking As New ReserveBooking
                Dim objCommon As New CommonData()
                Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
                Dim intBookedQty As Int32 = 0
                Dim intReservedQty As Int32 = 0



                'Bind Headers
                sbReserveBooking.Append(CommonWeb.GetTextValuesJSO(txtbookingAuthNo, PageSubmitPane.pub_GetPageAttribute("Booking Auth No")))
                sbReserveBooking.Append(CommonWeb.GetTextValuesJSO(datDateOfAcceptance, CDate(PageSubmitPane.pub_GetPageAttribute("Date Of Acceptance")).ToString("dd-MMM-yyyy")))

                If PageSubmitPane.pub_GetPageAttribute("Consignee") <> Nothing Then
                    sbReserveBooking.Append(CommonWeb.GetTextValuesJSO(txtConsignee, PageSubmitPane.pub_GetPageAttribute("Consignee")))
                Else
                    sbReserveBooking.Append(CommonWeb.GetTextValuesJSO(txtConsignee, ""))
                End If
                'sbReserveBooking.Append(CommonWeb.GetTextValuesJSO(txtConsignee, PageSubmitPane.pub_GetPageAttribute("Consignee")))
                sbReserveBooking.Append(CommonWeb.GetLookupValuesJSO(lkpCustomer, PageSubmitPane.pub_GetPageAttribute(ReserveBookingData.CSTMR_ID), PageSubmitPane.pub_GetPageAttribute("Customer")))

                sbReserveBooking.Append(CommonWeb.GetHiddenTextValuesJSO(hdnID, PageSubmitPane.pub_GetPageAttribute(ReserveBookingData.RSRV_BKNG_ID)))

                Dim ReserveBookingId As Long = PageSubmitPane.pub_GetPageAttribute(ReserveBookingData.RSRV_BKNG_ID)

                'Get Grid Details
                ds = objReserveBooking.Get_SubmitedEquipmentInformation(ReserveBookingId, intDepotID)

                intBookedQty = ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.BOOKED_COUNT)
                intReservedQty = ds.Tables(ReserveBookingData._V_ACTIVITY_STATUS).Select("SLCT='True'").Length

                If intBookedQty > intReservedQty Then
                    'hdnAuthNoEdit
                    sbReserveBooking.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAuthNoEdit, "edit"))
                Else
                    sbReserveBooking.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAuthNoEdit, "readonly"))
                End If

                ds.AcceptChanges()
            End If

            CacheData(RESERVEBOOKING, ds)
            pub_SetCallbackReturnValue("Message", sbReserveBooking.ToString())
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Sub ValidateMinimum_Selection()
        Try
            ds = RetrieveData(RESERVEBOOKING)

            Dim intBookedQty As Int32 = ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.BOOKED_COUNT)
            Dim intSelectCount As Int32 = ds.Tables(ReserveBookingData._V_ACTIVITY_STATUS).Select("SLCT='True'").Length

            If intSelectCount = 0 Then
                pub_SetCallbackStatus(True)
                pub_SetCallbackReturnValue("Message", "Please select atleast one Equipment details")
                Exit Sub
            End If

            If intBookedQty > intSelectCount Then
                pub_SetCallbackStatus(False)
            Else
                'pub_SetCallbackReturnValue("Message", "")
                pub_SetCallbackStatus(True)
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub


    Private Sub Validate_BookedQuantity(ByVal BookedQty As Int32)
        Try
            If BookedQty = 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackReturnValue("Message", "Booked Quantity should be greater than 0")
            End If

            ds = RetrieveData(RESERVEBOOKING)
            If ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows.Count > 0 Then

                If ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.AVAILABLE_COUNT) >= BookedQty Then
                    pub_SetCallbackStatus(True)
                Else
                    pub_SetCallbackStatus(False)
                    pub_SetCallbackReturnValue("Message", "Booked Quantity should be less then or Equal to Available Quanity")
                End If
            Else
                pub_SetCallbackStatus(False)
                pub_SetCallbackReturnValue("Message", "Equipment Quantity Information Not Available")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
    Private Sub Create_ReserveBooking(ByVal strCustomerId As String, ByVal strCustomerCode As String, ByVal datDatefAcceptance As Date, ByVal strBookingAuthNo As String, ByVal strConsignee As String)
        Try
            ds = RetrieveData(RESERVEBOOKING)

            Dim objCommonData As New CommonData
            Dim DepoId As Int32 = objCommonData.GetDepotID()
            Dim objReserveBooking As New ReserveBooking
            Dim ReserveBookingId As Long = Nothing

            ReserveBookingId = objReserveBooking.pub_CreateRESERVEBOOKING(strCustomerId, strCustomerCode, datDatefAcceptance, strBookingAuthNo, strConsignee, DepoId, ds)
            ds.AcceptChanges()
            pub_SetCallbackReturnValue("Message", "Reserve booking created Successfully.")
            pub_SetCallbackReturnValue("ReserveBookingId", ReserveBookingId)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub



    Private Sub Update_ReserveBooking(ByVal strReserveBookingId As String, ByVal strCustomerId As String, ByVal strCustomerCode As String, ByVal datDatefAcceptance As Date, ByVal strBookingAuthNo As String, ByVal strConsignee As String)
        Try
            ds = RetrieveData(RESERVEBOOKING)

            Dim objCommonData As New CommonData
            Dim DepoId As Int32 = objCommonData.GetDepotID()
            Dim objReserveBooking As New ReserveBooking

            objReserveBooking.pub_UpdateReserveBooking(strReserveBookingId, strCustomerId, strCustomerCode, datDatefAcceptance, strBookingAuthNo, strConsignee, DepoId, ds)
            ds.AcceptChanges()
            pub_SetCallbackReturnValue("Message", "Reserve booking updated Successfully.")
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Sub Get_EquipmentInformation(ByVal strCustomerId As String)

        Try
            Dim objCommonData As New CommonData
            Dim DepoId As Int32 = objCommonData.GetDepotID()
            Dim objReserveBooking As New ReserveBooking

            ds = objReserveBooking.pub_Get_EquipmentInformation(DepoId, strCustomerId)

            CacheData(RESERVEBOOKING, ds)

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/ReserveBooking.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgBookingQuantity_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgBookingQuantity.ClientBind
        Try
            ds = RetrieveData(RESERVEBOOKING)
            e.DataSource = ds.Tables(ReserveBookingData._EQUIPMENT_COUNT)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgEquipmentDetails_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentDetails.ClientBind
        Try
            ds = RetrieveData(RESERVEBOOKING)
            e.DataSource = ds.Tables(ReserveBookingData._V_ACTIVITY_STATUS)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgEquipmentDetails_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentDetails.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                If CBool(drv.Item(ReserveBookingData.GOUT_BT)) = True Then
                    CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgEquipmentDetails_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgEquipmentDetails.RowUpdated
        Try
            CacheData(RESERVEBOOKING, ds)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    
    Protected Sub ifgEquipmentDetails_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgEquipmentDetails.RowUpdating
        Try
            ds = RetrieveData(RESERVEBOOKING)

            If Not e.NewValues(ReserveBookingData.SLCT) Is Nothing AndAlso CBool(e.NewValues(ReserveBookingData.SLCT)) = True Then

                If ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.BOOKED_COUNT) Is DBNull.Value Then
                    e.Cancel = True
                    e.OutputParamters("Msg") = "Please enter booked Quantity"
                    e.OutputParamters("Index") = e.RowIndex
                    Exit Sub
                End If

                Dim intBookedQty As Int32 = ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.BOOKED_COUNT)
                Dim intSelectCount As Int32 = ds.Tables(ReserveBookingData._V_ACTIVITY_STATUS).Select("SLCT='True'").Length + 1

                If intBookedQty >= intSelectCount Then
                    e.OldValues(ReserveBookingData.SLCT) = e.NewValues(ReserveBookingData.SLCT)
                Else
                    e.Cancel = True
                    e.OutputParamters("Msg") = "Selection exceeds the booked quantity"
                    e.OutputParamters("Index") = e.RowIndex
                    Exit Sub
                End If


            Else
                e.OldValues(ReserveBookingData.SLCT) = e.NewValues(ReserveBookingData.SLCT)
            End If


        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
End Class
