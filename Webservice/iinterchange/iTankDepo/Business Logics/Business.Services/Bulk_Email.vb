Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class Bulk_Email
#Region "pub_GetBulk_Email() TABLE NAME:BULK_EMAIL"
    <OperationContract()> _
    Public Function pvt_UpdateSentStatus(ByVal bv_BulkEmailID As Integer, ByVal intDpt_ID As Integer) As Bulk_EmailDataSet
        Try

            Dim objBulkEmails As New Bulk_Emails
            objBulkEmails.pvt_UpdateSentStatus(bv_BulkEmailID, _
                                               intDpt_ID, _
                                               Now, _
                                               True)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetCleaningDetails"
    <OperationContract()> _
    Public Function GetCleaningDetails(ByVal bv_intCleaningId As Int64, ByVal intDPT_ID As Int64) As Bulk_EmailDataSet
        Try
            Dim dsCleaningDataset As Bulk_EmailDataSet
            Dim objCleanings As New Bulk_Emails
            dsCleaningDataset = objCleanings.GetCleaningDetails(bv_intCleaningId, intDPT_ID)
            Return dsCleaningDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "getRepairWorkOrderCustomerDetails"
    <OperationContract()> _
    Public Function getRepairWorkOrderCustomerDetails(ByVal CustomerID As Integer, ByVal bv_intDepotID As Integer) As Bulk_EmailDataSet
        Try
            Dim dsBulk_EmailDataSet As Bulk_EmailDataSet
            Dim objBulk_EmailDataSet As New Bulk_Emails
            dsBulk_EmailDataSet = objBulk_EmailDataSet.getRepairWorkOrderCustomerDetails(CustomerID, bv_intDepotID)
            Return dsBulk_EmailDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBulk_Email() TABLE NAME:BULK_EMAIL"
    <OperationContract()> _
    Public Function pub_GetBulk_Email(ByVal bv_intDepotID As Integer) As Bulk_EmailDataSet
        Try
            Dim objBulkEmail As Bulk_EmailDataSet
            Dim objBulkEmails As New Bulk_Emails
            objBulkEmail = objBulkEmails.GetBulk_Email(bv_intDepotID)
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBulk_EmailForAllDepots() TABLE NAME:BULK_EMAIL"
    ''' <summary>
    ''' This Method is used to get Bulk Email for all Depots
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetBulk_EmailForAllDepots() As Bulk_EmailDataSet
        Try
            Dim objBulkEmail As Bulk_EmailDataSet
            Dim objBulkEmails As New Bulk_Emails
            objBulkEmail = objBulkEmails.GetBulk_EmailForAllDepots()
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBulkEmailDetailbyblk_eml_bin() TABLE NAME:BULK_EMAIL_DETAIL"
    <OperationContract()> _
    Public Function pub_GetBulkEmailDetailbyBulkEmailID(ByVal bv_intBulkEmailID As Int64) As Bulk_EmailDataSet

        Try
            Dim objBulkEmail As Bulk_EmailDataSet
            Dim objBulkEmails As New Bulk_Emails
            objBulkEmail = objBulkEmails.GetBulkEmailDetailbyBulkEmailID(bv_intBulkEmailID)
            Dim Distinctdata As New DatasetHelpers(CType(objBulkEmail, DataSet))
            Dim dtSummary As New DataTable
            Dim strField As String = String.Concat(BulkEmailData.RSPNSBLTY_ID, ",", BulkEmailData.CSTMR_ID, ",", BulkEmailData.INVCNG_PRTY_ID, ",", Bulk_EmailData.ACTVTY_NO)
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRepair_EstimateBy_Transmission_No() TABLE NAME:V_REPAIR_ESTIMATE"
    <OperationContract()> _
    Public Function pub_GetRepair_EstimateBy_Transmission_No(ByVal bv_strWM_Transmission_No As String, ByVal bv_strWM_Unit_Nbr As String) As Bulk_EmailDataSet

        Try
            Dim objBulkEmail As Bulk_EmailDataSet
            Dim objBulkEmails As New Bulk_Emails
            objBulkEmail = objBulkEmails.GetRepair_EstimateBy_Transmission_No(bv_strWM_Transmission_No, bv_strWM_Unit_Nbr)
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBulkEmailTable() "
    <OperationContract()> _
    Public Function pub_GetBulkEmailTable(ByVal depotId As Integer) As Bulk_EmailDataSet

        Try
            Dim objBulkEmail As Bulk_EmailDataSet
            Dim objBulkEmails As New Bulk_Emails
            objBulkEmail = objBulkEmails.GetBulkMailTable(depotId)
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "getRepairWorkOrderCompiledDetails"
    <OperationContract()> _
    Public Function getRepairWorkOrderCompiledDetails(ByVal Sno As String) As Bulk_EmailDataSet
        Try
            Dim dsBulk_EmailDataSet As Bulk_EmailDataSet
            Dim objBulk_EmailDataSet As New Bulk_Emails
            dsBulk_EmailDataSet = objBulk_EmailDataSet.getRepairWorkOrderCompiledDetails(Sno)
            Return dsBulk_EmailDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


#Region "getRepairWorkOrderSummary"
    <OperationContract()> _
    Public Function getRepairWorkOrderSummary(ByVal Sno As String) As Bulk_EmailDataSet
        Try
            Dim dsBulk_EmailDataSet As Bulk_EmailDataSet
            Dim objBulk_EmailDataSet As New Bulk_Emails
            dsBulk_EmailDataSet = objBulk_EmailDataSet.getRepairWorkOrderSummary(Sno)
            Return dsBulk_EmailDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetDepot() TABLE NAME:DEPOT"
    <OperationContract()> _
    Public Function pub_GetDepot(ByVal bv_intDepotId As Int64) As Bulk_EmailDataSet
        Try
            Dim dsBulk_EmailDataSet As Bulk_EmailDataSet
            Dim objBulk_EmailDataSet As New Bulk_Emails
            dsBulk_EmailDataSet = objBulk_EmailDataSet.Get_Depot(bv_intDepotId)
            Return dsBulk_EmailDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_UpdateBulkEmailDetail() TABLE NAME:BULK_EMAIL_dETAIL"
    <OperationContract()> _
    Public Function pvt_UpdateBulkEmailDetail(ByVal bv_BulkEmailDetailID As Integer, ByVal bv_strFileName As String) As Boolean
        Try

            Dim objBulkEmails As New Bulk_Emails
            objBulkEmails.UpdateBulkEmailDetail(bv_BulkEmailDetailID, _
                                                bv_strFileName)
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBulkEmailDetailbyBulkEmailID_Group() TABLE NAME:BULK_EMAIL_DETAIL"
    <OperationContract()> _
    Public Function pub_GetBulkEmailDetailbyBulkEmailID_Group(ByVal bv_intBulkEmailID As Int64) As Bulk_EmailDataSet

        Try
            Dim objBulkEmail As New Bulk_EmailDataSet
            'Dim dsBulk_Email As New Bulk_EmailDataSet
            Dim objBulkEmails As New Bulk_Emails
            Dim intCount As Integer = 1
            Dim dtBulk_email As New DataTable
            Dim drNew As DataRow = Nothing
            dtBulk_email = objBulkEmail.Tables(Bulk_EmailData._V_BULK_EMAIL_DETAIL_GROUP).Clone()
            objBulkEmail = objBulkEmails.GetBulkEmailDetailbyBulkEmailID(bv_intBulkEmailID)
            Dim Distinctdata As New DatasetHelpers(CType(objBulkEmail, DataSet))
            Dim dtSummary As New DataTable
            Dim strField As String = String.Concat(Bulk_EmailData.ACTVTY_NO, ",", Bulk_EmailData.GI_TRNSCTN_NO, ",", Bulk_EmailData.ACTVTY_NAM, ",", Bulk_EmailData.EQPMNT_NO, ",", Bulk_EmailData.RSND_BT)
            dtSummary = Distinctdata.SelectGroupByInto("BULK_EMAIL_GROUP", objBulkEmail.Tables(Bulk_EmailData._V_BULK_EMAIL_DETAIL), strField, "", Bulk_EmailData.ACTVTY_NO)
            For Each drSummary As DataRow In dtSummary.Rows
                drNew = dtBulk_email.NewRow()
                drNew(Bulk_EmailData.BLK_EML_DTL_ID) = intCount
                drNew(Bulk_EmailData.ACTVTY_NO) = drSummary.Item(Bulk_EmailData.ACTVTY_NO)
                drNew(Bulk_EmailData.GI_TRNSCTN_NO) = drSummary.Item(Bulk_EmailData.GI_TRNSCTN_NO)
                drNew(Bulk_EmailData.ACTVTY_NAM) = drSummary.Item(Bulk_EmailData.ACTVTY_NAM)
                drNew(Bulk_EmailData.EQPMNT_NO) = drSummary.Item(Bulk_EmailData.EQPMNT_NO)
                drNew(Bulk_EmailData.RSND_BT) = drSummary.Item(Bulk_EmailData.RSND_BT)
                dtBulk_email.Rows.Add(drNew)
                intCount = intCount + 1
            Next
            objBulkEmail.Tables(Bulk_EmailData._V_BULK_EMAIL_DETAIL_GROUP).Merge(dtBulk_email)
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_UpdateErrorStatus() TABLE NAME:BULK_EMAIL"
    <OperationContract()> _
    Public Function pvt_UpdateErrorStatus(ByVal bv_BulkEmailID As Integer, ByVal intDpt_ID As Integer, ByVal bv_ErrorRemarks As String) As Bulk_EmailDataSet
        Try

            Dim objBulkEmails As New Bulk_Emails
            objBulkEmails.pvt_UpdateErrorStatus(bv_BulkEmailID, _
                                               intDpt_ID, _
                                               True, _
                                               bv_ErrorRemarks)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetV_CustomerByCstmr_Id() TABLE NAME:Customer"
    <OperationContract()>
    Public Function pub_GetV_CustomerByCstmr_Id(ByVal bv_intCSTMR_ID As Integer, ByVal intDepotID As Integer) As DataTable

        Try
            Dim dtCustomer As DataTable
            Dim obCustomers As New Customers

            dtCustomer = obCustomers.GetV_CustomerByCstmr_Id(bv_intCSTMR_ID, intDepotID).Tables(CustomerData._V_CUSTOMER)
            Return dtCustomer
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetV_AgentByAGNT_Id() TABLE NAME:Agent"
    <OperationContract()>
    Public Function pub_GetV_AgentByAGNT_Id(ByVal bv_intAGNT_ID As Integer, ByVal intDepotID As Integer) As DataTable

        Try
            Dim dtAgent As DataTable
            Dim obAgents As New Agents
            dtAgent = obAgents.GetV_AgentByAGNT_Id(bv_intAGNT_ID, intDepotID).Tables(AgentData._V_Agent)
            Return dtAgent
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class
