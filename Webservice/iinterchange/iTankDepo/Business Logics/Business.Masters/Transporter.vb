Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class Transporter

#Region "pub_GetTransporterRouteDetailByTransporterID() TABLE NAME:TRANSPORTER_ROUTE_DETAIL"

    <OperationContract()> _
    Public Function pub_GetTransporterRouteDetailByTransporterID(ByVal bv_i64TransporterID As Int64, _
                                                                 ByVal bv_intDepotId As Integer) As TransporterDataSet

        Try
            Dim dsTransporterRouteDetailData As TransporterDataSet
            Dim objTransporterRouteDetails As New Transporters
            dsTransporterRouteDetailData = objTransporterRouteDetails.GetTransporterRouteDetailByTransporterID(bv_i64TransporterID, bv_intDepotId)
            Return dsTransporterRouteDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateTransporter() TABLE NAME:Transporter"

    <OperationContract()> _
    Public Function pub_CreateTransporter(ByVal bv_strTransporterCD As String, _
                                          ByVal bv_strTransporterDescription As String, _
                                          ByVal bv_strContactPerson As String, _
                                          ByVal bv_strContactAddress As String, _
                                          ByVal bv_strZipCode As String, _
                                          ByVal bv_strPhoneNo As String, _
                                          ByVal bv_strFaxNo As String, _
                                          ByVal bv_strEmailID As String, _
                                          ByVal bv_blnActiveID As Boolean, _
                                          ByVal bv_strCreatedBy As String, _
                                          ByVal bv_datCreatedDate As DateTime, _
                                          ByVal bv_strModifiedBy As String, _
                                          ByVal bv_datModifiedDate As DateTime, _
                                          ByVal bv_i32DepotID As Int32, _
                                          ByRef br_dsTransporter As TransporterDataSet) As Long
        Dim objTransaction As New Transactions()

        Try
            Dim objTransporters As New Transporters
            Dim lngCreated As Long
            lngCreated = objTransporters.CreateTransporter(bv_strTransporterCD, _
                                                         bv_strTransporterDescription, bv_strContactPerson, _
                                                         bv_strContactAddress, bv_strZipCode, _
                                                         bv_strPhoneNo, bv_strFaxNo, _
                                                         bv_strEmailID, bv_blnActiveID, _
                                                         bv_strCreatedBy, _
                                                         bv_datCreatedDate, bv_strModifiedBy, _
                                                         bv_datModifiedDate, bv_i32DepotID, objTransaction)

            For Each drRoute As DataRow In br_dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Rows
                Dim decEmptryTripSupplierRate As Decimal = 0
                Dim decEmptyTripCustomerRate As Decimal = 0
                Dim decFullTripSupplierRate As Decimal = 0
                Dim decFullTripCustomerRate As Decimal = 0
                Dim strRouteDescription As String = String.Empty
                If drRoute.RowState <> DataRowState.Deleted Then
                    If Not IsDBNull(drRoute.Item(TransporterData.EMPTY_TRP_SPPLR_RT_NC)) Then
                        decEmptryTripSupplierRate = CDec(drRoute.Item(TransporterData.EMPTY_TRP_SPPLR_RT_NC))
                    End If
                    If Not IsDBNull(drRoute.Item(TransporterData.EMPTY_TRP_CSTMR_RT_NC)) Then
                        decEmptyTripCustomerRate = CDec(drRoute.Item(TransporterData.EMPTY_TRP_CSTMR_RT_NC))
                    End If
                    If Not IsDBNull(drRoute.Item(TransporterData.FLL_TRP_SPPLR_RT_NC)) Then
                        decFullTripSupplierRate = CDec(drRoute.Item(TransporterData.FLL_TRP_SPPLR_RT_NC))
                    End If
                    If Not IsDBNull(drRoute.Item(TransporterData.FLL_TRP_CSTMR_RT_NC)) Then
                        decFullTripCustomerRate = CDec(drRoute.Item(TransporterData.FLL_TRP_CSTMR_RT_NC))
                    End If
                    If Not IsDBNull(drRoute.Item(TransporterData.RT_DSCRPTN_VC)) Then
                        strRouteDescription = CStr(drRoute.Item(TransporterData.RT_DSCRPTN_VC))
                    End If
                End If
                objTransporters.CreateTransporterRouteDetail(lngCreated, _
                                                            CLng(drRoute.Item(TransporterData.RT_ID)), _
                                                            strRouteDescription, _
                                                            CStr(drRoute.Item(TransporterData.PCK_UP_LCTN_CD)), _
                                                            CStr(drRoute.Item(TransporterData.DRP_OFF_LCTN_CD)), _
                                                            decEmptryTripSupplierRate, _
                                                            decEmptyTripCustomerRate, _
                                                            decFullTripSupplierRate, _
                                                            decFullTripCustomerRate, _
                                                            objTransaction)
            Next
            objTransaction.commit()
            Return lngCreated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region

#Region "pub_ModifyTransporter() TABLE NAME: TRANSPORTER"
    <OperationContract()> _
    Public Function pub_ModifyTransporter(ByVal bv_lngTransporterId As Long, _
                                          ByVal bv_strTransporterCD As String, _
                                          ByVal bv_strTransporterDescription As String, _
                                          ByVal bv_strContactPerson As String, _
                                          ByVal bv_strContactAddress As String, _
                                          ByVal bv_strZipCode As String, _
                                          ByVal bv_strPhoneNo As String, _
                                          ByVal bv_strFaxNo As String, _
                                          ByVal bv_strEmailID As String, _
                                          ByVal bv_blnActiveID As Boolean, _
                                          ByVal bv_strCreatedBy As String, _
                                          ByVal bv_datCreatedDate As DateTime, _
                                          ByVal bv_strModifiedBy As String, _
                                          ByVal bv_datModifiedDate As DateTime, _
                                          ByVal bv_i32DepotID As Int32, _
                                          ByRef br_dsTransporter As TransporterDataSet) As Boolean

        Dim objTransaction As New Transactions()
        Try
            Dim objTransporters As New Transporters
            Dim blnUpdated As Boolean
            blnUpdated = objTransporters.UpdateTransporter(bv_lngTransporterId, _
                                                          bv_strTransporterCD, _
                                                          bv_strTransporterDescription, bv_strContactPerson, _
                                                          bv_strContactAddress, bv_strZipCode, _
                                                          bv_strPhoneNo, bv_strFaxNo, _
                                                          bv_strEmailID, bv_blnActiveID, _
                                                          bv_strModifiedBy, _
                                                          bv_datModifiedDate, bv_i32DepotID, objTransaction)

            For Each drRoute As DataRow In br_dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Rows

                Dim decEmptryTripSupplierRate As Decimal = 0
                Dim decEmptyTripCustomerRate As Decimal = 0
                Dim decFullTripSupplierRate As Decimal = 0
                Dim decFullTripCustomerRate As Decimal = 0
                Dim strRouteDescription As String = String.Empty
                If drRoute.RowState <> DataRowState.Deleted Then
                    If Not IsDBNull(drRoute.Item(TransporterData.EMPTY_TRP_SPPLR_RT_NC)) Then
                        decEmptryTripSupplierRate = CDec(drRoute.Item(TransporterData.EMPTY_TRP_SPPLR_RT_NC))
                    End If
                    If Not IsDBNull(drRoute.Item(TransporterData.EMPTY_TRP_CSTMR_RT_NC)) Then
                        decEmptyTripCustomerRate = CDec(drRoute.Item(TransporterData.EMPTY_TRP_CSTMR_RT_NC))
                    End If
                    If Not IsDBNull(drRoute.Item(TransporterData.FLL_TRP_SPPLR_RT_NC)) Then
                        decFullTripSupplierRate = CDec(drRoute.Item(TransporterData.FLL_TRP_SPPLR_RT_NC))
                    End If
                    If Not IsDBNull(drRoute.Item(TransporterData.FLL_TRP_CSTMR_RT_NC)) Then
                        decFullTripCustomerRate = CDec(drRoute.Item(TransporterData.FLL_TRP_CSTMR_RT_NC))
                    End If
                    If Not IsDBNull(drRoute.Item(TransporterData.RT_DSCRPTN_VC)) Then
                        strRouteDescription = CStr(drRoute.Item(TransporterData.RT_DSCRPTN_VC))
                    End If
                End If
                Select Case drRoute.RowState
                    Case DataRowState.Added
                        objTransporters.CreateTransporterRouteDetail(bv_lngTransporterId, _
                                                                    CLng(drRoute.Item(TransporterData.RT_ID)), _
                                                                    strRouteDescription, _
                                                                    CStr(drRoute.Item(TransporterData.PCK_UP_LCTN_CD)), _
                                                                    CStr(drRoute.Item(TransporterData.DRP_OFF_LCTN_CD)), _
                                                                    decEmptryTripSupplierRate, _
                                                                    decEmptyTripCustomerRate, _
                                                                    decFullTripSupplierRate, _
                                                                    decFullTripCustomerRate, _
                                                                    objTransaction)
                    Case DataRowState.Modified
                        objTransporters.UpdateTransporterRouteDetail(CLng(drRoute.Item(TransporterData.TRNSPRTR_RT_DTL_ID)), _
                                                                    bv_lngTransporterId, _
                                                                    CLng(drRoute.Item(TransporterData.RT_ID)), _
                                                                    strRouteDescription, _
                                                                    CStr(drRoute.Item(TransporterData.PCK_UP_LCTN_CD)), _
                                                                    CStr(drRoute.Item(TransporterData.DRP_OFF_LCTN_CD)), _
                                                                    decEmptryTripSupplierRate, _
                                                                    decEmptyTripCustomerRate, _
                                                                    decFullTripSupplierRate, _
                                                                    decFullTripCustomerRate, _
                                                                    objTransaction)

                    Case DataRowState.Deleted
                        objTransporters.DeleteTransporterRouteDetail(CLng(drRoute.Item(TransporterData.TRNSPRTR_RT_DTL_ID, DataRowVersion.Original)), objTransaction)
                End Select
            Next
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

#Region "pub_GetTransporterByTransporterCode() TABLE NAME:TRANSPORTER"

    <OperationContract()> _
    Public Function pub_GetTransporterByTransporterCode(ByVal bv_strTransporterCode As String, _
                                                        ByVal bv_intDepotId As Integer) As TransporterDataSet
        Try
            Dim dsTransporter As TransporterDataSet
            Dim objTransporters As New Transporters
            dsTransporter = objTransporters.GetTransporterByTransporterCode(bv_strTransporterCode, bv_intDepotId)
            Return dsTransporter
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CheckDefaultByDepotId() TABLE NAME: TRANSPORTER"
    <OperationContract()> _
    Public Function pub_CheckDefaultByDepotId(ByVal bv_intDepotId As Integer) As TransporterDataSet
        Try
            Dim dsTransporter As TransporterDataSet
            Dim objTransporters As New Transporters
            dsTransporter = objTransporters.GetCheckDefaultByDepotId(bv_intDepotId)
            Return dsTransporter
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ValidateRoute() TABLE NAME: TRANSPORTER_ROUTE_DETAIL"
    <OperationContract()> _
    Public Function pub_GetTransporterRouteCodeByRoute(ByVal bv_strRouteCode As String, _
                                                       ByVal bv_intDepotId As Integer, _
                                                       ByVal bv_strTransporterCode As String) As Boolean

        Try
            Dim objTransporters As New Transporters
            Dim intRowCount As Integer
            intRowCount = CInt(objTransporters.GetTransporterRouteCodeByRoute(bv_strRouteCode, bv_intDepotId, bv_strTransporterCode))
            If intRowCount > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBankDetailByDepotId() TABLE NAME:BANK_DETAIL"

    <OperationContract()> _
    Public Function pub_GetBankDetailByDepotId(ByVal bv_intDepotId As Int32, _
                                               ByRef br_dtBankDetail As DataTable) As Boolean

        Try
            Dim objTransporters As New Transporters
            br_dtBankDetail = objTransporters.GetBankDetailByDepotId(bv_intDepotId).Tables(InvoiceGenerationData._V_BANK_DETAIL)
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTransporterDetailByID() TABLE NAME:TRANSPORTER"

    <OperationContract()> _
    Public Function pub_GetTransporterDetailByID(ByVal bv_i64TransporterID As Int64, _
                                                 ByVal bv_intDepotId As Integer) As TransporterDataSet

        Try
            Dim dsTransporterRouteDetailData As TransporterDataSet
            Dim objTransporterRouteDetails As New Transporters
            dsTransporterRouteDetailData = objTransporterRouteDetails.GetTransporterDetailByID(bv_i64TransporterID, bv_intDepotId)
            Return dsTransporterRouteDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
End Class
