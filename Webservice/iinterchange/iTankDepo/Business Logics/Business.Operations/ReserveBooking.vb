Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.Text

<ServiceContract()> _
Public Class ReserveBooking



    <OperationContract()> _
    Public Function Get_SubmitedEquipmentInformation(ByVal bv_RSRV_BKNG_ID As Int64, ByVal bv_DPT_ID As Int32) As ReserveBookingDataSet
        Try
            Dim ds As New ReserveBookingDataSet
            Dim dtReserveBookingHeader As New DataTable
            Dim dtReserveBookingDetails As New DataTable

            Dim objReserveBookings As New ReserveBookings

            'Get ReserveBooking - Header Information
            dtReserveBookingHeader = objReserveBookings.GetRESERVEBOOKINGById(bv_RSRV_BKNG_ID, bv_DPT_ID)

            'Get ReserveBooking_Details - Details Information
            dtReserveBookingDetails = objReserveBookings.GetSubmited_ReserveDetail(bv_RSRV_BKNG_ID, bv_DPT_ID)


            'Booked Quantity Count
            ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Clear()

            Dim dr As DataRow = ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).NewRow()

            If dtReserveBookingHeader.Rows.Count > 0 Then
                dr.Item(ReserveBookingData.EQPMNT_CNT_ID) = 1
                dr.Item(ReserveBookingData.GATEIN_COUNT) = dtReserveBookingHeader.Rows(0).Item(ReserveBookingData.GT_CNT)
                dr.Item(ReserveBookingData.AVAILABLE_COUNT) = dtReserveBookingHeader.Rows(0).Item(ReserveBookingData.AVL_CNT)
                dr.Item(ReserveBookingData.BOOKED_COUNT) = dtReserveBookingHeader.Rows(0).Item(ReserveBookingData.BKD_QTY)
            Else
                dr.Item(ReserveBookingData.EQPMNT_CNT_ID) = 1
                dr.Item(ReserveBookingData.GATEIN_COUNT) = 0
                dr.Item(ReserveBookingData.AVAILABLE_COUNT) = 0
            End If
            ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows.Add(dr)


            'Equipment Information

            ds.Tables(ReserveBookingData._V_ACTIVITY_STATUS).Clear()

            ds.Tables(ReserveBookingData._V_ACTIVITY_STATUS).Merge(dtReserveBookingDetails)

            ''Get In Active Records
            'dtInActiveRecords = objReserveBookings.GetSubmited_RESERVEBOOKINGDetail_InActiveRecords(bv_RSRV_BKNG_ID, bv_DPT_ID)
            Return ds

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


#Region "pub_GetRESERVEBOOKING() TABLE NAME:RESERVEBOOKING"

    <OperationContract()> _
    Public Function GetEquipmentInformation(ByVal bv_RSRV_BKNG_ID As Int64) As ReserveBookingDataSet

        Try
            Dim objReserveBookings As New ReserveBookings
            Return objReserveBookings.GetRESERVEBOOKING()
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

    <OperationContract()> _
    Public Function pub_Get_EquipmentInformation(ByVal strDepoId As String, ByVal strCustomerId As String) As ReserveBookingDataSet

        Try
            Dim ds As ReserveBookingDataSet
            Dim objReserveBookings As New ReserveBookings
            Dim intGateinCount As Int32 = objReserveBookings.Get_GateInCountByCustomerId(strDepoId, strCustomerId)
            Dim intAvailableCount As Int32 = objReserveBookings.Get_GateInAvailableCountByCustomerId(strDepoId, strCustomerId)

            ds = objReserveBookings.Get_EquipmentsFromActivityStatus(strDepoId, strCustomerId)

            ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Clear()

            Dim dr As DataRow = ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).NewRow()

            dr.Item(ReserveBookingData.EQPMNT_CNT_ID) = 1
            dr.Item(ReserveBookingData.GATEIN_COUNT) = intGateinCount
            dr.Item(ReserveBookingData.AVAILABLE_COUNT) = intAvailableCount
            ' dr.Item(ReserveBookingData.BOOKED_COUNT) = 0

            ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows.Add(dr)

            Return ds

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


#Region "pub_GetRESERVEBOOKINGByBKNGAUTHNO() TABLE NAME:RESERVEBOOKING"

    '<OperationContract()> _
    'Public Function pub_GetRESERVEBOOKINGByBKNGAUTHNO(ByVal bv_BKNG_AUTH_NO As String) As ReserveBookingDataSet

    '    Try
    '        Dim objRESERVEBOOKING As ReserveBookingDataSet
    '        Dim objReserveBookings As New ReserveBookings
    '        objRESERVEBOOKING = objReserveBookings.GetRESERVEBOOKINGByBKNG_AUTH_NO(bv_BKNG_AUTH_NO)
    '        Return objRESERVEBOOKING
    '    Catch ex As Exception
    '        Throw New FaultException(New FaultReason(ex.Message))
    '    End Try
    'End Function
#End Region

#Region "pub_CreateRESERVEBOOKING() TABLE NAME:RESERVEBOOKING"

    <OperationContract()> _
    Public Function pub_CreateRESERVEBOOKING(ByVal strCustomerId As String, ByVal strCustomerCode As String, ByVal datDateOfAccept As Date, _
                                             ByVal strBookingAuthNo As String, ByVal strConsignee As String, ByVal strDepoId As String, ByRef ds As ReserveBookingDataSet) As Long

        Dim objTrans As New Transactions
        Try

            Dim objReserveBookings As New ReserveBookings

            Dim intGateInCount As Int32 = CInt(ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.GATEIN_COUNT))
            Dim intAvailableCount As Int32 = CInt(ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.AVAILABLE_COUNT))
            Dim intBookedCount As Int32 = CInt(ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.BOOKED_COUNT))

            Dim intEquipmentCount As Int32 = CInt(ds.Tables(ReserveBookingData._V_ACTIVITY_STATUS).Select("SLCT='True'").Length)
            Dim PendingCount As Int32 = intBookedCount - intEquipmentCount
            Dim BookingAuthId As Long = Nothing

            'Create Header
            BookingAuthId = objReserveBookings.CreateRESERVEBOOKING(strBookingAuthNo, datDateOfAccept, CLng(strCustomerId), strCustomerCode, strConsignee, intGateInCount, 0, intAvailableCount, _
                                                    intBookedCount, intEquipmentCount, PendingCount, False, True, CInt(strDepoId), objTrans)


            'Create Details and Update ActivityStatus (Booking Auth No)

            For Each dr As DataRow In ds.Tables(ReserveBookingData._V_ACTIVITY_STATUS).Rows

                objReserveBookings.CreateRESERVEBOOKING_DETAIL(BookingAuthId, CStr(dr.Item(ReserveBookingData.EQPMNT_NO)), CLng(dr.Item(ReserveBookingData.EQPMNT_TYP_ID)), _
                                                               CInt(dr.Item(ReserveBookingData.EQPMNT_STTS_ID)), CStr(dr.Item(ReserveBookingData.EQPMNT_STTS_CD)), CLng(strCustomerId), strCustomerCode, _
                                                               CStr(dr.Item(ReserveBookingData.GI_TRNSCTN_NO)), strBookingAuthNo, CStr(dr.Item(ReserveBookingData.STTS_CD)), False, CBool(dr.Item(ReserveBookingData.SLCT)), CInt(strDepoId), objTrans)

                'For Selected Equipments only updated in Activity Status

                If Not dr.Item(ReserveBookingData.SLCT) Is DBNull.Value AndAlso CBool(dr.Item(ReserveBookingData.SLCT)) = True Then
                    objReserveBookings.Update_AuthNo_ActivityStatus(strBookingAuthNo, CStr(dr.Item(ReserveBookingData.EQPMNT_NO)), _
                                                               strCustomerId, CStr(dr.Item(ReserveBookingData.GI_TRNSCTN_NO)), strDepoId, objTrans)


                    'Delete Existing InActive Records for Different Auth No - Applied for Seleted Equipments
                    objReserveBookings.Delete_InActiveRecordsforDifferentAuthNo(CStr(dr.Item(ReserveBookingData.EQPMNT_NO)), CStr(dr.Item(ReserveBookingData.GI_TRNSCTN_NO)), _
                                                                                strCustomerId, strBookingAuthNo, CInt(strDepoId), objTrans)
                End If

              
            Next

            objTrans.commit()
            Return BookingAuthId
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_ModifyRESERVEBOOKING() TABLE NAME:RESERVEBOOKING"

    <OperationContract()> _
    Public Function pub_UpdateReserveBooking(ByVal strReserveBookingId As String, ByVal strCustomerId As String, ByVal strCustomerCode As String, ByVal datDateOfAccept As Date, _
                                             ByVal strBookingAuthNo As String, ByVal strConsignee As String, ByVal strDepoId As String, ByRef ds As ReserveBookingDataSet) As Long

        Dim objTrans As New Transactions
        Try

            Dim objReserveBookings As New ReserveBookings

            Dim intGateInCount As Int32 = CInt(ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.GATEIN_COUNT))
            Dim intAvailableCount As Int32 = CInt(ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.AVAILABLE_COUNT))
            Dim intBookedCount As Int32 = CInt(ds.Tables(ReserveBookingData._EQUIPMENT_COUNT).Rows(0).Item(ReserveBookingData.BOOKED_COUNT))

            Dim intEquipmentCount As Int32 = CInt(ds.Tables(ReserveBookingData._V_ACTIVITY_STATUS).Select("SLCT='True'").Length)
            Dim PendingCount As Int32 = intBookedCount - intEquipmentCount
            Dim BookingAuthId As Long = Nothing

            'Update Header
            objReserveBookings.Update_ReserveBooking(strReserveBookingId, intAvailableCount, intGateInCount, intBookedCount, intEquipmentCount, PendingCount, strBookingAuthNo, datDateOfAccept, strConsignee, strDepoId, objTrans)


            'Create Details and Update ActivityStatus (Booking Auth No)

            For Each dr As DataRow In ds.Tables(ReserveBookingData._V_ACTIVITY_STATUS).Rows



                Select Case dr.RowState

                    Case DataRowState.Modified

                        objReserveBookings.Update_ReserveBookingDetails(strReserveBookingId, CStr(dr.Item(ReserveBookingData.EQPMNT_NO)), strCustomerId, _
                                                                          CStr(dr.Item(ReserveBookingData.GI_TRNSCTN_NO)), strBookingAuthNo, CBool(dr.Item(ReserveBookingData.SLCT)), _
                                                                          strDepoId, objTrans)


                        'Selected Equipment
                        If Not dr.Item(ReserveBookingData.SLCT) Is DBNull.Value AndAlso CBool(dr.Item(ReserveBookingData.SLCT)) = True Then
                            objReserveBookings.Update_AuthNo_ActivityStatus(strBookingAuthNo, CStr(dr.Item(ReserveBookingData.EQPMNT_NO)), _
                                                                       strCustomerId, CStr(dr.Item(ReserveBookingData.GI_TRNSCTN_NO)), strDepoId, objTrans)


                            'Delete Existing InActive Records for Different Auth No - Applied for Seleted Equipments
                            objReserveBookings.Delete_InActiveRecordsforDifferentAuthNo(CStr(dr.Item(ReserveBookingData.EQPMNT_NO)), CStr(dr.Item(ReserveBookingData.GI_TRNSCTN_NO)), _
                                                                                        strCustomerId, strBookingAuthNo, CInt(strDepoId), objTrans)
                        End If

                        'UnSelected Equipment
                        If Not dr.Item(ReserveBookingData.SLCT) Is DBNull.Value AndAlso CBool(dr.Item(ReserveBookingData.SLCT)) = False Then

                            objReserveBookings.Update_AuthNo_ActivityStatus(Nothing, CStr(dr.Item(ReserveBookingData.EQPMNT_NO)), _
                                                                       strCustomerId, CStr(dr.Item(ReserveBookingData.GI_TRNSCTN_NO)), strDepoId, objTrans)
                        End If


                End Select

            Next

            objTrans.commit()
            Return BookingAuthId
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region
End Class
