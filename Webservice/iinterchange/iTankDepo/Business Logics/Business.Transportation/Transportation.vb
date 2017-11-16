Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class Transportation

#Region "pub_TransportationInvoiceGetTransportation() TABLE NAME:Transportation_Detail"

    <OperationContract()> _
    Public Function pub_GetTransportationDetailByTransportationID(ByVal bv_i64TransportationID As Int64) As TransportationDataSet

        Try
            Dim dsTransportationInvoiceData As TransportationDataSet
            Dim objTransportationInvoices As New Transportations
            dsTransportationInvoiceData = objTransportationInvoices.GetTransportationDetailByTransportationID(bv_i64TransportationID)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTransportationByTransportationID() TABLE NAME:Transportation"

    <OperationContract()> _
    Public Function pub_GetTransportationByTransportationID(ByVal bv_i64TransportationID As Int64, _
                                                            ByVal bv_intDepotId As Integer) As TransportationDataSet

        Try
            Dim dsTransportationInvoiceData As TransportationDataSet
            Dim objTransportationInvoices As New Transportations
            dsTransportationInvoiceData = objTransportationInvoices.GetTransportationByTransportationID(bv_i64TransportationID, bv_intDepotId)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


#Region "pub_GetCustomerCurrencyByCustomerId() Table Name: Customer"

    <OperationContract()> _
    Public Function pub_GetCustomerCurrencyByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                        ByVal bv_intDepotId As Int32) As TransportationDataSet
        Try
            Dim dsTransportationInvoiceData As TransportationDataSet
            Dim objTransportationInvoices As New Transportations
            dsTransportationInvoiceData = objTransportationInvoices.GetCustomerCurrencyByCustomerId(bv_i64CustomerId, bv_intDepotId)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTransportationDetailByEquipmentNo()  TABLE NAME:TRANSPORTATION_DETAIL"
    <OperationContract()> _
    Public Function pub_GetTransportationDetailByEquipmentNo(ByVal bv_strEquipmentNo As String) As TransportationDataSet

        Try
            Dim objTransportations As New Transportations
            Dim dsTransportationData As New TransportationDataSet
            dsTransportationData = objTransportations.GetTransportationDetailByEquipmentNo(bv_strEquipmentNo)
            Return dsTransportationData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ValidateJobEndDateByEquipmentNo()  TABLE NAME:TRANSPORTATION_DETAIL"
    <OperationContract()> _
    Public Function pub_ValidateJobEndDateByEquipmentNo(ByVal bv_strEquipmentNo As String) As Boolean

        Try
            Dim objTransportations As New Transportations
            Dim blnExists As Boolean
            Dim strEndDate As String
            strEndDate = objTransportations.GetEquipmentInformationEndDateByEquipmentNo(bv_strEquipmentNo)
            If strEndDate = "" Then
                blnExists = False
            Else
                blnExists = True
            End If
            Return blnExists
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTransportationDetailRate() Table Name: Transportation_Detail_Rate"

    <OperationContract()> _
    Public Function pub_GetTransportationDetailRate(ByVal bv_i64TransportationId As Int64, _
                                                    ByVal bv_i64TransportationDetailId As Int64) As TransportationDataSet
        Try
            Dim dsTransportationInvoiceData As TransportationDataSet
            Dim objTransportationInvoices As New Transportations
            dsTransportationInvoiceData = objTransportationInvoices.GetTransportationDetailRate(bv_i64TransportationId, bv_i64TransportationDetailId)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTransportationDetailRateById() Table Name: Transportation_Detail_Rate"

    <OperationContract()> _
    Public Function pub_GetTransportationDetailRateById(ByVal bv_i64TransportationId As Int64) As TransportationDataSet
        Try
            Dim dsTransportationInvoiceData As TransportationDataSet
            Dim objTransportationInvoices As New Transportations
            dsTransportationInvoiceData = objTransportationInvoices.GetTransportationDetailRateById(bv_i64TransportationId)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetAdditionalChargeRateByDepotId() Table Name: Additional_Charge_Rate"
    <OperationContract()> _
    Public Function pub_GetAdditionalChargeRateByDepotId(ByVal bv_i32DepotId As Int32) As TransportationDataSet
        Try
            Dim dsTransportationInvoiceData As TransportationDataSet
            Dim objTransportationInvoices As New Transportations
            dsTransportationInvoiceData = objTransportationInvoices.GetAdditionalChargeRateByDepotId(bv_i32DepotId)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetAdditionalChargeRateById() Table Name: Additional_Charge_Rate"
    <OperationContract()> _
    Public Function pub_GetAdditionalChargeRateById(ByVal bv_i64AdditionalChargeRateId As Int64, _
                                                    ByVal bv_i32DepotId As Int32) As Double
        Try
            Dim objTransportationInvoices As New Transportations
            Dim dblChargeRate As Double = 0
            dblChargeRate = objTransportationInvoices.GetAdditionalChargeRateById(bv_i64AdditionalChargeRateId, bv_i32DepotId)
            Return dblChargeRate
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateTransportation() Table Name: Transportation"
    <OperationContract()> _
    Public Function pub_CreateTransportation(ByVal bv_i64CustomerId As Int64, _
                                             ByVal bv_i64RouteId As Int64, _
                                             ByVal bv_i64TransporterId As Int64, _
                                             ByVal bv_i64ActivityLocationId As Int64, _
                                             ByVal bv_i64ActivityId As Int64, _
                                             ByVal bv_strActivityCode As String, _
                                             ByRef br_i64StatusId As Int64, _
                                             ByRef br_strStatusCode As String, _
                                             ByVal bv_datRequestDate As DateTime, _
                                             ByVal bv_decTripRate As Decimal, _
                                             ByVal bv_strRemarks As String, _
                                             ByRef br_strRequestNo As String, _
                                             ByVal bv_intDepotId As Int32, _
                                             ByVal bv_strCreatedBy As String, _
                                             ByVal bv_datCreatedDate As DateTime, _
                                             ByVal bv_intNoOfTrip As Integer, _
                                             ByRef br_dsTransportation As TransportationDataSet) As Long
        Dim objTransaction As New Transactions
        Try
            Dim objTransportations As New Transportations
            Dim lngTransportation As Long = 0
            Dim intNoofTrips As Integer = 0
            Dim intNoofEquipment As Integer = 0
            Dim decTotalRate As Decimal = 0
            Dim decAdditionalRate As Decimal = 0
            pvt_CalculateTripsDetails(intNoofTrips, intNoofEquipment, bv_decTripRate, decTotalRate, decAdditionalRate, bv_strActivityCode, br_dsTransportation)
            validateTransportationStatus(br_i64StatusId, br_strStatusCode, br_strRequestNo, bv_intDepotId, br_dsTransportation, objTransaction)
            Dim objIndexPattern As New IndexPatterns
            'Dim bv_strRQST_NO As String
            br_strRequestNo = objIndexPattern.GetMaxReferenceNo(TransportationData._TRANSPORTATION, bv_datRequestDate, objTransaction, Nothing, bv_intDepotId)
            lngTransportation = objTransportations.CreateTransportation(bv_i64CustomerId, _
                                                                         bv_datRequestDate, _
                                                                        bv_i64RouteId, _
                                                                        bv_i64TransporterId, _
                                                                        bv_i64ActivityLocationId, _
                                                                        bv_i64ActivityId, _
                                                                        br_i64StatusId, _
                                                                        br_strRequestNo, _
                                                                        bv_decTripRate, _
                                                                        bv_strRemarks, _
                                                                        bv_intNoOfTrip, _
                                                                        bv_strCreatedBy, _
                                                                        bv_datCreatedDate, _
                                                                        bv_strCreatedBy, _
                                                                        bv_datCreatedDate, _
                                                                        bv_intDepotId, _
                                                                        objTransaction)

            UpdateTransportationDetail(lngTransportation, bv_i64CustomerId, bv_i64RouteId, br_strRequestNo, decTotalRate, bv_decTripRate, decAdditionalRate, bv_intDepotId, bv_i64TransporterId, bv_strActivityCode, br_dsTransportation, objTransaction)
            objTransaction.commit()
            Return lngTransportation
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function
#End Region

#Region "pub_UpdateTransportation() Table Name: Transportation"
    <OperationContract()> _
    Public Function pub_UpdateTransportation(ByVal bv_i64TransportationId As Int64, _
                                             ByVal bv_i64CustomerId As Int64, _
                                             ByVal bv_i64RouteId As Int64, _
                                             ByVal bv_i64TransporterId As Int64, _
                                             ByVal bv_i64ActivityLocationId As Int64, _
                                             ByVal bv_i64ActivityId As Int64, _
                                             ByVal bv_strActivityCode As String, _
                                             ByRef br_i64StatusId As Int64, _
                                             ByRef br_strStatusCode As String, _
                                             ByVal bv_datRequestDate As DateTime, _
                                             ByVal bv_decTripRate As Decimal, _
                                             ByVal bv_strRemarks As String, _
                                             ByRef br_strRequestNo As String, _
                                             ByVal bv_intDepotId As Int32, _
                                             ByVal bv_strModifiedBy As String, _
                                             ByVal bv_datModifiedDate As DateTime, _
                                             ByVal bv_intNoOfTrip As Integer, _
                                             ByRef br_dsTransportation As TransportationDataSet) As Boolean
        Dim objTransaction As New Transactions
        Try
            Dim objTransportations As New Transportations
            Dim blnUpdated As Boolean
            Dim intNoofTrips As Integer = 0
            Dim intNoofEquipment As Integer = 0
            Dim decTotalRate As Decimal = 0
            Dim lngCancel As Long = 0
            Dim decAdditionalRate As Decimal = 0
            pvt_CalculateTripsDetails(intNoofTrips, intNoofEquipment, bv_decTripRate, decTotalRate, decAdditionalRate, bv_strActivityCode, br_dsTransportation)
            validateTransportationStatus(br_i64StatusId, br_strStatusCode, br_strRequestNo, bv_intDepotId, br_dsTransportation, objTransaction)
            blnUpdated = objTransportations.UpdateTransportation(bv_i64TransportationId, _
                                                                 bv_i64CustomerId, _
                                                                 bv_i64RouteId, _
                                                                 bv_i64TransporterId, _
                                                                 bv_i64ActivityLocationId, _
                                                                 bv_i64ActivityId, _
                                                                 br_i64StatusId, _
                                                                 bv_datRequestDate, _
                                                                 bv_decTripRate, _
                                                                 bv_strRemarks, _
                                                                 bv_intNoOfTrip, _
                                                                 bv_strModifiedBy, _
                                                                 bv_datModifiedDate, _
                                                                 bv_intDepotId, _
                                                                 objTransaction)
            UpdateTransportationDetail(bv_i64TransportationId, bv_i64CustomerId, bv_i64RouteId, br_strRequestNo, decTotalRate, bv_decTripRate, decAdditionalRate, bv_intDepotId, bv_i64TransporterId, bv_strActivityCode, br_dsTransportation, objTransaction)
            objTransaction.commit()
            Return blnUpdated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function
#End Region

#Region "pvt_CalculateTripsDetails"
    Private Sub pvt_CalculateTripsDetails(ByRef br_intNoOfTrips As Integer, _
                                          ByRef br_intNoOfEquipment As Integer, _
                                          ByVal bv_decTotalRate As Decimal, _
                                          ByRef br_decTotalRate As Decimal, _
                                          ByRef br_decAdditionalRate As Decimal, _
                                          ByVal bv_strEquipmentCode As String, _
                                          ByRef br_dsTransportation As TransportationDataSet)
        Try
            Dim intSingleTrip As Integer = 0
            Dim intDoubleTrip As Integer = 0
            Dim intDefaultTrip As Integer = 0
            Dim intDoubleTotalRate As Integer = 0
            Dim intDefaultTotalRate As Integer = 0
            Dim intRemainder As Integer = 0
            Dim intDivider As Integer = 0
            Dim intDefaultRemainder As Integer = 0
            Dim intDefaultDivider As Integer = 0
            Dim decStateRate As Decimal = 0
            Dim strEquipmentStatus As String = String.Empty

            If br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count > 0 Then
                decStateRate = CDec(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows(0).Item(TransportationData.TRNSPRTR_TRP_RT_NC))
            Else
                decStateRate = bv_decTotalRate
            End If

            If Not br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "").ToString = "" Then
                br_decAdditionalRate = CDec(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), ""))

            End If

            If br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Rows.Count > 0 Then
                strEquipmentStatus = br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Rows(0).Item(TransportationData.EQPMNT_STT_CD).ToString
            Else
                strEquipmentStatus = bv_strEquipmentCode

            End If

            ' If br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Rows.Count > 0 Then
            If strEquipmentStatus = "EMPTY" Then
                If Not br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='Y'")).ToString = "" Then
                    intSingleTrip = CInt(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='Y'")))
                End If
                If Not br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='N'")).ToString = "" Then
                    intDoubleTrip = CInt(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='N'")))
                End If
                If Not br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, " IS NULL")).ToString = "" Then
                    intDefaultTrip = CInt(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, " IS NULL")))
                End If
                If intDoubleTrip <> 0 Then
                    intDivider = CInt(Math.Truncate(intDoubleTrip / 2))
                    Math.DivRem(intDoubleTrip, 2, intRemainder)
                    intDoubleTotalRate = intRemainder + intDivider
                End If
                If intDefaultTrip <> 0 Then
                    intDefaultDivider = CInt(Math.Truncate(intDefaultTrip / 2))
                    Math.DivRem(intDefaultTrip, 2, intDefaultRemainder)
                    intDefaultTotalRate = intDefaultDivider + intDefaultRemainder
                End If

                br_intNoOfTrips = intDoubleTotalRate + intSingleTrip + intDefaultTotalRate
            Else
                If Not br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), "").ToString = "" Then
                    br_intNoOfTrips = CInt(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), ""))
                End If
            End If
            br_decTotalRate = (br_intNoOfTrips * decStateRate) + br_decAdditionalRate
            ' End If
            If Not br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.EQPMNT_NO, ")"), "").ToString = "" Then
                br_intNoOfEquipment = CInt(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.EQPMNT_NO, ")"), ""))
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "validateTransportationStatus() Table Name: Transportation_Detail"
    <OperationContract()> _
    Public Sub validateTransportationStatus(ByRef br_i64TransportationStatusId As Int64, _
                                            ByRef br_strTransportationCode As String, _
                                            ByVal bv_strRequestNo As String, _
                                            ByVal bv_intDepotId As Int32, _
                                            ByRef br_dsTransportation As TransportationDataSet, _
                                            ByRef br_objTransaction As Transactions)
        Try
            Dim dtEnum As New DataTable
            Dim intRecordCount As Integer = 0
            Dim dtTransportationCharge As New DataTable
            'Dim drBilled As DataRow()
            Dim intCountBilledRecords As Integer = 0
            Dim intCountInProgressRecords As Integer = 0
            Dim intJobEndDateCount As Integer = 0
            Dim intJobEndNotNullCount As Integer = 0
            Dim objTransportations As New Transportations
            'Total Record Count
            If Not br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), "").ToString = "" Then
                intRecordCount = CInt(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), ""))
            End If
            dtTransportationCharge = objTransportations.GetTransportationChargeByRequestNo(bv_strRequestNo, bv_intDepotId, br_objTransaction).Tables(TransportationData._TRANSPORTATION_CHARGE)

            For Each drTransportationDetail As DataRow In br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows
                If Not dtTransportationCharge.Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_CHRG_ID, ")"), String.Concat(TransportationData.BLLNG_FLG, " = 'B'")).ToString = "" Then
                    intCountBilledRecords = CInt(dtTransportationCharge.Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_CHRG_ID, ")"), String.Concat(TransportationData.BLLNG_FLG, " = 'B'")))
                End If
            Next


            If Not br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.JB_END_DT, " <= '", CDate(Now), "'")).ToString = "" Then
                intJobEndDateCount = CInt(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.JB_END_DT, " <= '", CDate(Now), "'")))
            End If
            If Not br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.JB_END_DT, " IS NOT NULL")).ToString = "" Then
                intJobEndNotNullCount = CInt(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.JB_END_DT, " IS NOT NULL")))
            End If

            If intRecordCount <> 0 And intCountBilledRecords <> 0 Then
                If intRecordCount = intCountBilledRecords AndAlso intRecordCount = intJobEndDateCount Then
                    br_i64TransportationStatusId = 91
                ElseIf intRecordCount >= intCountBilledRecords Then
                    br_i64TransportationStatusId = 90
                End If
            ElseIf intRecordCount <> 0 And intJobEndDateCount <> 0 Then
                If intRecordCount >= intJobEndDateCount Then
                    br_i64TransportationStatusId = 95
                End If
            ElseIf intJobEndNotNullCount <> 0 Then
                If intRecordCount > intJobEndNotNullCount Then
                    br_i64TransportationStatusId = 95
                End If
            Else
                br_i64TransportationStatusId = 89
            End If
            'Get Enum Code
            dtEnum = objTransportations.GetEnumCodeByEnumId(br_i64TransportationStatusId)
            If Not dtEnum Is Nothing Then
                If dtEnum.Rows.Count > 0 Then
                    If Not IsDBNull(dtEnum.Rows(0).Item(TransportationData.ENM_CD)) Then
                        br_strTransportationCode = dtEnum.Rows(0).Item(TransportationData.ENM_CD).ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "UpdateTransportationDetail() Table Name: Transportation_Detail"
    <OperationContract()> _
    Public Function UpdateTransportationDetail(ByVal bv_i64TransportationId As Int64, _
                                               ByVal bv_i64CustomerId As Int64, _
                                               ByVal bv_i64RouteId As Int64, _
                                               ByVal bv_strRequestNo As String, _
                                               ByVal bv_decTotalRate As Decimal, _
                                               ByVal bv_decTripRate As Decimal, _
                                               ByVal bv_decAdditinonalRate As Decimal, _
                                               ByVal bv_intDepotId As Int32, _
                                               ByVal bv_i64TransporterId As Int64, _
                                               ByVal bv_strActivityCode As String, _
                                               ByRef br_dsTransportation As TransportationDataSet, _
                                               ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim objTransportations As New Transportations
            Dim blnUpdated As Boolean
            For Each drTransportationDetail As DataRow In br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows
                Dim lngTransportationDetailId As Long = 0
                Dim lngTransportationChargeId As Long = 0
                Dim decTripRate As Decimal = 0
                Dim decAdditionalRate As Decimal = 0
                Dim lngCargoId As Long = 0
                Dim lngEmptySingleId As Long = 0
                Dim strCustomerRefNo As String = String.Empty
                Dim strRemarks As String = String.Empty
                Dim datJobStartDate As DateTime = Nothing
                Dim datJobEndDate As DateTime = Nothing
                Dim dtTransportationCharge As New DataTable
                Dim intEquipmentTypeID As Integer
                Dim dtEqpData As New DataTable
                Dim drTransportationRate As DataRow()

                If drTransportationDetail.RowState <> DataRowState.Deleted Then
                    If Not IsDBNull(drTransportationDetail.Item(TransportationData.CSTMR_RF_NO)) Then
                        strCustomerRefNo = drTransportationDetail.Item(TransportationData.CSTMR_RF_NO).ToString()
                    End If
                    If Not IsDBNull(drTransportationDetail.Item(TransportationData.RMRKS_VC)) Then
                        strRemarks = drTransportationDetail.Item(TransportationData.RMRKS_VC).ToString()
                    End If
                    If Not IsDBNull(drTransportationDetail.Item(TransportationData.JB_STRT_DT)) Then
                        datJobStartDate = CDate(drTransportationDetail.Item(TransportationData.JB_STRT_DT))
                    End If
                    If Not IsDBNull(drTransportationDetail.Item(TransportationData.JB_END_DT)) Then
                        datJobEndDate = CDate(drTransportationDetail.Item(TransportationData.JB_END_DT))
                    End If
                    If Not IsDBNull(drTransportationDetail.Item(TransportationData.PRDCT_ID)) Then
                        lngCargoId = CLng(drTransportationDetail.Item(TransportationData.PRDCT_ID))
                    End If
                    If Not IsDBNull(drTransportationDetail.Item(TransportationData.EMPTY_SNGL_ID)) Then
                        lngEmptySingleId = CLng(drTransportationDetail.Item(TransportationData.EMPTY_SNGL_ID))
                    End If
                    If Not IsDBNull(drTransportationDetail.Item(TransportationData.EMPTY_SNGL_CD)) Then
                        If drTransportationDetail.Item(TransportationData.EMPTY_SNGL_CD).ToString = "Y" Then
                            decTripRate = bv_decTripRate
                        ElseIf drTransportationDetail.Item(TransportationData.EMPTY_SNGL_CD).ToString = "N" Then
                            decTripRate = bv_decTripRate / 2
                        Else
                            decTripRate = bv_decTripRate
                        End If
                    Else
                        decTripRate = bv_decTripRate
                    End If
                    If br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID))).ToString <> "" Then
                        decAdditionalRate = CDec(br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID))))
                    End If

                    Dim objConfigs As New CommonUIs

                    If drTransportationDetail.Item(GateinData.EQPMNT_TYP_ID) Is DBNull.Value Then
                        dtEqpData = objConfigs.GetEquipmentType(CStr(drTransportationDetail.Item(TransportationData.EQPMNT_TYP_CD)), bv_intDepotId).Tables(CommonUIData._EQUIPMENT_TYPE)
                        intEquipmentTypeID = CInt(dtEqpData.Rows(0).Item(CommonUIData.EQPMNT_TYP_ID))
                    Else
                        intEquipmentTypeID = CInt(drTransportationDetail.Item(TransportationData.EQPMNT_TYP_ID))
                    End If


                    If Not IsDBNull(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)) Then
                        dtTransportationCharge = objTransportations.GetTransportationChargeByEqpmntNo(bv_strRequestNo, bv_i64TransportationId, _
                                                                                                    CLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)), _
                                                                                                    bv_intDepotId, br_objTransaction)
                    End If
                End If

                Select Case drTransportationDetail.RowState
                    Case DataRowState.Added

                        'Calculate Supplier Rate
                        Dim decSupplierRate As Decimal = 0D

                        If bv_strActivityCode.ToLower() = "loading" Or bv_strActivityCode.ToLower() = "offloading" Then

                            decSupplierRate = CDec(objTransportations.GetSupplier_FullTripRateByTransporterId(bv_i64TransporterId, bv_i64RouteId))

                        ElseIf bv_strActivityCode.ToLower() = "empty" Then

                            If drTransportationDetail.Item(TransportationData.EMPTY_SNGL_CD).ToString().ToLower() = "y" Then
                                decSupplierRate = CDec(objTransportations.GetSupplier_EmptyTripRateByTransporterId(bv_i64TransporterId, bv_i64RouteId))
                            End If

                            If drTransportationDetail.Item(TransportationData.EMPTY_SNGL_CD).ToString().ToLower() = "n" Then
                                decSupplierRate = CDec(objTransportations.GetSupplier_EmptyTripRateByTransporterId(bv_i64TransporterId, bv_i64RouteId)) / 2
                            End If

                        End If

                            lngTransportationDetailId = objTransportations.CreateTransportationDetail(bv_i64TransportationId, _
                                                                                                      drTransportationDetail.Item(TransportationData.EQPMNT_NO).ToString(), _
                                                                                                      intEquipmentTypeID, _
                                                                                                      lngEmptySingleId, _
                                                                                                      strCustomerRefNo, _
                                                                                                      lngCargoId, _
                                                                                                      decAdditionalRate, _
                                                                                                      decTripRate, _
                                                                                                      datJobStartDate, _
                                                                                                      datJobEndDate, _
                                                                                                      strRemarks, _
                                                                                                      decSupplierRate, _
                                                                                                      br_objTransaction)

                            drTransportationRate = br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)))
                            If drTransportationRate IsNot Nothing Then
                                UpdateTransportationRateDetail(drTransportationRate, bv_i64TransportationId, lngTransportationDetailId, br_objTransaction)
                            End If

                            lngTransportationChargeId = objTransportations.CreateTransportationCharge(bv_i64CustomerId, _
                                                                                                      bv_i64TransportationId, _
                                                                                                      lngTransportationDetailId, _
                                                                                                      Nothing, _
                                                                                                      bv_i64RouteId, _
                                                                                                      bv_i64TransporterId, _
                                                                                                      bv_strRequestNo, _
                                                                                                      drTransportationDetail.Item(TransportationData.EQPMNT_NO).ToString(), _
                                                                                                      intEquipmentTypeID, _
                                                                                                      lngEmptySingleId, _
                                                                                                      strCustomerRefNo, _
                                                                                                      datJobEndDate, _
                                                                                                      decAdditionalRate, _
                                                                                                      decTripRate, _
                                                                                                      bv_intDepotId, _
                                                                                                      "U", _
                                                                                                      Nothing, _
                                                                                                      Nothing, _
                                                                                                      br_objTransaction)
                            drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID) = lngTransportationDetailId

                    Case DataRowState.Modified

                            'Calculate Supplier Rate
                            Dim decSupplierRate As Decimal = 0D

                        If bv_strActivityCode.ToLower() = "loading" Or bv_strActivityCode.ToLower() = "offloading" Then

                            decSupplierRate = CDec(objTransportations.GetSupplier_FullTripRateByTransporterId(bv_i64TransporterId, bv_i64RouteId))

                        ElseIf bv_strActivityCode.ToLower() = "empty" Then

                            If drTransportationDetail.Item(TransportationData.EMPTY_SNGL_CD).ToString().ToLower() = "y" Then
                                decSupplierRate = CDec(objTransportations.GetSupplier_EmptyTripRateByTransporterId(bv_i64TransporterId, bv_i64RouteId))
                            End If

                            If drTransportationDetail.Item(TransportationData.EMPTY_SNGL_CD).ToString().ToLower() = "n" Then
                                decSupplierRate = CDec(objTransportations.GetSupplier_EmptyTripRateByTransporterId(bv_i64TransporterId, bv_i64RouteId)) / 2
                            End If

                        End If

                            blnUpdated = objTransportations.UpdateTransportationDetail(CLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)), _
                                                                                       bv_i64TransportationId, _
                                                                                       drTransportationDetail.Item(TransportationData.EQPMNT_NO).ToString(), _
                                                                                       intEquipmentTypeID, _
                                                                                       lngEmptySingleId, _
                                                                                       strCustomerRefNo, _
                                                                                       lngCargoId, _
                                                                                       decAdditionalRate, _
                                                                                       decTripRate, _
                                                                                       datJobStartDate, _
                                                                                       datJobEndDate, _
                                                                                       strRemarks, _
                                                                                       decSupplierRate, _
                                                                                       br_objTransaction)


                            dtTransportationCharge = objTransportations.GetTransportationChargeByEqpmntNo(bv_strRequestNo, bv_i64TransportationId, _
                                                                                                  CLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)), _
                                                                                                  bv_intDepotId, br_objTransaction)
                            drTransportationRate = br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)))
                            If drTransportationRate IsNot Nothing Then
                                UpdateTransportationRateDetail(drTransportationRate, bv_i64TransportationId, CLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)), br_objTransaction)
                            End If
                            For Each drTemp As DataRow In br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows
                                If drTemp.RowState = DataRowState.Deleted Then
                                    objTransportations.DeleteTransportationDetailRateByDetailId(bv_i64TransportationId, CommonUIs.iLng(drTemp.Item(TransportationData.TRNSPRTTN_DTL_RT_ID, DataRowVersion.Original)), br_objTransaction)
                                End If
                            Next
                            If Not dtTransportationCharge Is Nothing Then
                                If dtTransportationCharge.Rows.Count > 0 Then
                                    objTransportations.UpdateTransportationCharge(CLng(dtTransportationCharge.Rows(0).Item(TransportationData.TRNSPRTTN_CHRG_ID)), _
                                                                                  bv_i64TransportationId, _
                                                                                  CLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)), _
                                                                                  bv_i64CustomerId, _
                                                                                  bv_i64RouteId, _
                                                                                  bv_i64TransporterId, _
                                                                                  bv_strRequestNo, _
                                                                                  drTransportationDetail.Item(TransportationData.EQPMNT_NO).ToString(), _
                                                                                  intEquipmentTypeID, _
                                                                                  lngEmptySingleId, _
                                                                                  strCustomerRefNo, _
                                                                                  datJobEndDate, _
                                                                                  decAdditionalRate, _
                                                                                  decTripRate, _
                                                                                  bv_intDepotId, _
                                                                                  Nothing, _
                                                                                  Nothing, _
                                                                                  br_objTransaction)


                                End If
                            End If
                    Case DataRowState.Deleted
                            objTransportations.DeleteTransportationChargeByTransportationDetailId(bv_i64TransportationId, CommonUIs.iLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID, DataRowVersion.Original)), br_objTransaction)
                            objTransportations.DeleteTransportationDetailRateByDetailId(bv_i64TransportationId, CommonUIs.iLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID, DataRowVersion.Original)), br_objTransaction)
                            objTransportations.DeleteTransportationDetail(bv_i64TransportationId, CInt(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID, DataRowVersion.Original)), br_objTransaction)
                    Case DataRowState.Unchanged
                            Dim decOldTripRate As Decimal = 0
                            If Not IsDBNull(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)) Then

                                'Calculate Supplier Rate
                                Dim decSupplierRate As Decimal = 0D

                            If bv_strActivityCode.ToLower() = "loading" Or bv_strActivityCode.ToLower() = "offloading" Then

                                decSupplierRate = CDec(objTransportations.GetSupplier_FullTripRateByTransporterId(bv_i64TransporterId, bv_i64RouteId))

                            ElseIf bv_strActivityCode.ToLower() = "empty" Then

                                If drTransportationDetail.Item(TransportationData.EMPTY_SNGL_CD).ToString().ToLower() = "y" Then
                                    decSupplierRate = CDec(objTransportations.GetSupplier_EmptyTripRateByTransporterId(bv_i64TransporterId, bv_i64RouteId))
                                End If

                                If drTransportationDetail.Item(TransportationData.EMPTY_SNGL_CD).ToString().ToLower() = "n" Then
                                    decSupplierRate = CDec(objTransportations.GetSupplier_EmptyTripRateByTransporterId(bv_i64TransporterId, bv_i64RouteId)) / 2
                                End If

                            End If

                                blnUpdated = objTransportations.UpdateTransportationDetail(CLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)), _
                                                                                           bv_i64TransportationId, _
                                                                                           drTransportationDetail.Item(TransportationData.EQPMNT_NO).ToString(), _
                                                                                           intEquipmentTypeID, _
                                                                                           lngEmptySingleId, _
                                                                                           strCustomerRefNo, _
                                                                                           lngCargoId, _
                                                                                           decAdditionalRate, _
                                                                                           decTripRate, _
                                                                                           datJobStartDate, _
                                                                                           datJobEndDate, _
                                                                                           strRemarks, _
                                                                                           decSupplierRate, _
                                                                                           br_objTransaction)
                                objTransportations.UpdateTransportationCharge(CLng(dtTransportationCharge.Rows(0).Item(TransportationData.TRNSPRTTN_CHRG_ID)), _
                                                                                bv_i64TransportationId, _
                                                                                CLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)), _
                                                                                bv_i64CustomerId, _
                                                                                bv_i64RouteId, _
                                                                                bv_i64TransporterId, _
                                                                                bv_strRequestNo, _
                                                                                drTransportationDetail.Item(TransportationData.EQPMNT_NO).ToString(), _
                                                                                intEquipmentTypeID, _
                                                                                lngEmptySingleId, _
                                                                                strCustomerRefNo, _
                                                                                datJobEndDate, _
                                                                                decAdditionalRate, _
                                                                                decTripRate, _
                                                                                bv_intDepotId, _
                                                                                Nothing, _
                                                                                Nothing, _
                                                                                br_objTransaction)
                            End If
                End Select

            Next
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "UpdateTransportationRateDetail() Table Name:Transportation_Detail_Rate"
    <OperationContract()> _
    Public Function UpdateTransportationRateDetail(ByRef br_TransportationRateDetail As DataRow(), _
                                                    ByVal bv_i64TransportationId As Int64, _
                                                    ByVal bv_i64TransportationDetailId As Int64, _
                                                    ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim objTransportations As New Transportations
            Dim intDetailRateCount As Integer = 0
            Dim blnUpdated As Boolean

            objTransportations.DeleteTransportationDetailRateByTransportationId(bv_i64TransportationId, bv_i64TransportationDetailId, br_objTransaction)
            For Each drTransportationRate As DataRow In br_TransportationRateDetail
                Dim decChargeRate As Decimal = 0
                If drTransportationRate.RowState <> DataRowState.Deleted Then
                    If Not IsDBNull(drTransportationRate.Item(TransportationData.ADDTNL_CHRG_RT_NC)) Then
                        decChargeRate = CDec(drTransportationRate.Item(TransportationData.ADDTNL_CHRG_RT_NC))
                    End If
                    Dim lngTransportationRateDetail As Int64 = objTransportations.CreateTransportationDetailRate(bv_i64TransportationId, _
                                                                                                                bv_i64TransportationDetailId, _
                                                                                                                CLng(drTransportationRate(TransportationData.ADDTNL_CHRG_RT_ID)), _
                                                                                                                decChargeRate, _
                                                                                                                br_objTransaction)
                    drTransportationRate.Item(TransportationData.TRNSPRTTN_DTL_RT_ID) = lngTransportationRateDetail
                    drTransportationRate.Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_i64TransportationDetailId

                End If
            Next
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTransportationDetailRate() Table Name: Transportation_Detail_Rate"

    <OperationContract()> _
    Public Function pub_GetTransportationDetailRateByTransportationDetailId(ByVal bv_i64TransportationDetailId As Int64) As TransportationDataSet
        Try
            Dim dsTransportationInvoiceData As New TransportationDataSet
            Dim objTransportationInvoices As New Transportations
            dsTransportationInvoiceData = objTransportationInvoices.GetTransportationDetailRateByTransportationDetailId(bv_i64TransportationDetailId)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET (Document Print) : pub_GetTransportation "
    <OperationContract()> _
    Public Function pub_GetTransportation(ByRef bv_param As String) As DataSet
        Try
            Dim objCommonUIs As New CommonUIs
            Dim objTransportations As New Transportations
            Dim dtTransportation As New DataTable
            Dim dtTransportationDetail As New DataTable
            Dim dsTransportation As New TransportationDataSet
            dsTransportation.Clear()
            dtTransportation = dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Clone()
            dtTransportationDetail = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Clone()
            Dim i64TransportationId As Int64
            Dim intDepotId As Integer
            If objCommonUIs.pub_GetParameter("TransportationId", bv_param) <> Nothing Then
                i64TransportationId = CLng(objCommonUIs.pub_GetParameter("TransportationId", bv_param))
            End If
            If objCommonUIs.pub_GetParameter("DPT_ID", bv_param) <> Nothing Then
                intDepotId = CInt(objCommonUIs.pub_GetParameter("DPT_ID", bv_param))
            End If
            dtTransportation = objTransportations.GetTransportationByTransportationID(i64TransportationId, intDepotId).Tables(TransportationData._V_TRANSPORTATION)
            dtTransportationDetail = objTransportations.GetTransportationDetailByTransportationID(i64TransportationId).Tables(TransportationData._V_TRANSPORTATION_DETAIL)
            If Not dtTransportation Is Nothing Then
                dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Merge(dtTransportation)
            End If
            If Not dtTransportationDetail Is Nothing Then
                dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Merge(dtTransportationDetail)
            End If
            Return dsTransportation
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomertransportationByCustomerId() Table Name: CUSTOMER_TRANSPORTATION"

    <OperationContract()> _
    Public Function pub_GetCustomerTransportationByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                              ByVal bv_i64RouteId As Int64, _
                                                              ByVal bv_i64EquipmentStatusCode As String) As TransportationDataSet
        Try
            Dim dsTransportationInvoiceData As TransportationDataSet
            Dim objTransportationInvoices As New Transportations
            dsTransportationInvoiceData = objTransportationInvoices.GetCustomerTransportationByCustomerId(bv_i64CustomerId, bv_i64RouteId, bv_i64EquipmentStatusCode)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRouteByRouteId() Table Name: ROUTE"

    <OperationContract()> _
    Public Function pub_GetRouteByRouteId(ByVal bv_i64CustomerId As Int64, _
                                          ByVal bv_i32DepotId As Int32) As TransportationDataSet
        Try
            Dim dsTransportationInvoiceData As TransportationDataSet
            Dim objTransportations As New Transportations
            dsTransportationInvoiceData = objTransportations.GetRouteByRouteId(bv_i64CustomerId, bv_i32DepotId)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CancelRequest() Table Name: UPDATE: Transportation/ INSERT:Audit_Log"
    <OperationContract()> _
    Public Function pub_CancelRequest(ByVal bv_i64TransportationId As Int64, _
                                      ByVal bv_strRequestNo As String, _
                                      ByRef br_dsTransportation As TransportationDataSet, _
                                      ByVal bv_strCancelledBy As String, _
                                      ByVal bv_datCancelledDate As DateTime, _
                                      ByVal bv_strReason As String, _
                                      ByVal bv_intDepotId As Integer) As Long
        Dim objTransaction As New Transactions
        Try
            Dim lngAudtiLog As Long = 0
            Dim objTransportations As New Transportations
            Dim objCommonUIs As New CommonUIs
            objTransportations.UpdateTransportationCancel(bv_i64TransportationId, _
                                                          bv_strRequestNo, _
                                                          92, _
                                                          bv_strCancelledBy, _
                                                          bv_datCancelledDate, _
                                                          bv_intDepotId, _
                                                          objTransaction)

            objTransportations.DeleteTransportationChargeByTransportationId(bv_i64TransportationId, objTransaction)

            Dim strEqpmntNo As String = String.Empty
            For Each drTransportationDetail As DataRow In br_dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows
                If drTransportationDetail.RowState = DataRowState.Deleted Then
                    strEqpmntNo = drTransportationDetail.Item(TransportationData.EQPMNT_NO, DataRowVersion.Original).ToString()
                    objTransportations.DeleteTransportationDetailRateByDetailId(bv_i64TransportationId, CLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID, DataRowVersion.Original)), objTransaction)
                    objTransportations.DeleteTransportationDetail(bv_i64TransportationId, CLng(drTransportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID, DataRowVersion.Original)), objTransaction)
                Else
                    strEqpmntNo = drTransportationDetail.Item(TransportationData.EQPMNT_NO).ToString()
                End If
                If strEqpmntNo <> "" Then
                    lngAudtiLog = objCommonUIs.CreateAuditLog(strEqpmntNo, _
                                                              bv_strRequestNo, _
                                                              "Transportation Request", _
                                                              "Cancel", _
                                                              bv_datCancelledDate, _
                                                              Nothing, _
                                                              Nothing, _
                                                              bv_strReason, _
                                                              bv_strCancelledBy, _
                                                              bv_intDepotId, _
                                                              objTransaction)
                End If
            Next
            objTransaction.commit()
            Return lngAudtiLog
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function
#End Region

#Region "GetTripRateByRouteId() Table Name: Transporter_Route_Detail"

    <OperationContract()> _
    Public Function pub_GetTripRateByRouteId(ByVal bv_i64RouteId As Int64, _
                                             ByVal bv_i64TransporterId As Int64, _
                                             ByVal bv_i32DepotId As Int32) As TransportationDataSet
        Try
            Dim dsTransportationInvoiceData As TransportationDataSet
            Dim objTransportations As New Transportations
            dsTransportationInvoiceData = objTransportations.GetTripRateByRouteId(bv_i64RouteId, bv_i64TransporterId, bv_i32DepotId)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTransporterByRouteId() Table Name: Transporter_Route_Detail"

    <OperationContract()> _
    Public Function pub_GetTransporterByRouteId(ByVal bv_i64RouteId As Int64, _
                                                ByVal bv_i32DepotId As Int32) As TransportationDataSet
        Try
            Dim dsTransportationInvoiceData As TransportationDataSet
            Dim objTransportations As New Transportations
            dsTransportationInvoiceData = objTransportations.GetTransporterByRouteId(bv_i64RouteId, bv_i32DepotId)
            Return dsTransportationInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class
