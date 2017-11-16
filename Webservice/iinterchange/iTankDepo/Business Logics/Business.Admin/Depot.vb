#Region " Depot.vb"
'*********************************************************************************************************************
'Name :
'      Depot.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Depot.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/18/2013 9:59:06 AM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.Xml
Imports System.Configuration
Imports System.IO

<ServiceContract()> _
Public Class Depot

#Region "VALIDATE : pub_ValidatePKDepotr() TABLE NAME:DEPOT"
    ''' <summary>
    ''' This method is used to validate the User Name
    ''' </summary>
    ''' <param name="bv_strUserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_ValidatePKDepot(ByVal bv_strDepotCode As String) As Boolean
        Dim dsDepotDataSet As DepotDataSet
        Dim objDepots As New Depots
        Try
            dsDepotDataSet = objDepots.GetDEPOTByDPT_CD(bv_strDepotCode)
            If dsDepotDataSet.Tables(DepotData._DEPOT).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateDEPOT() TABLE NAME:DEPOT"
    <OperationContract()> _
    Public Function pub_CreateDEPOT(ByVal bv_strDPT_CD As String, _
        ByVal bv_strDPT_NAM As String, _
        ByVal bv_strCNTCT_PRSN_NAM As String, _
        ByVal bv_strADDRSS_LN1_VC As String, _
        ByVal bv_strADDRSS_LN2_VC As String, _
        ByVal bv_strADDRSS_LN3_VC As String, _
        ByVal bv_strVT_NO As String, _
        ByVal bv_strEML_ID As String, _
        ByVal bv_strPHN_NO As String, _
        ByVal bv_strFX_NO As String, _
        ByVal bv_strLG_PTH As String, _
        ByVal bv_strMDFD_BY As String, _
        ByVal bv_datMDFD_DT As DateTime, _
        ByVal bv_strWfData As String, _
        ByVal bv_strOrganizationType As Int32, _
        ByVal bv_strReportingTo As Int32, _
        ByRef br_dsDepotDataset As DepotDataSet) As Long
        Dim objTransaction As New Transactions()
        Try

            Dim objDEPOT As New Depots
            Dim lngCreated As Long
            lngCreated = objDEPOT.CreateDEPOT(bv_strDPT_CD, _
                  bv_strDPT_NAM, bv_strCNTCT_PRSN_NAM, _
                  bv_strADDRSS_LN1_VC, bv_strADDRSS_LN2_VC, _
                  bv_strADDRSS_LN3_VC, _
                  bv_strVT_NO, bv_strEML_ID, _
                  bv_strPHN_NO, bv_strFX_NO, _
                  bv_strLG_PTH, _
                  bv_strMDFD_BY, bv_datMDFD_DT, _
                  bv_strOrganizationType, bv_strReportingTo, objTransaction)
            pub_UpdateBankDetails(br_dsDepotDataset, CInt(lngCreated), objTransaction)
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

#Region "pub_ModifyDEPOT() TABLE NAME:DEPOT"
    <OperationContract()> _
    Public Function pub_ModifyDEPOT(ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_strDPT_CD As String, _
        ByVal bv_strDPT_NAM As String, _
        ByVal bv_strCNTCT_PRSN_NAM As String, _
        ByVal bv_strADDRSS_LN1_VC As String, _
        ByVal bv_strADDRSS_LN2_VC As String, _
        ByVal bv_strADDRSS_LN3_VC As String, _
        ByVal bv_strVT_NO As String, _
        ByVal bv_strEML_ID As String, _
        ByVal bv_strPHN_NO As String, _
        ByVal bv_strFX_NO As String, _
        ByVal bv_strLG_PTH As String, _
        ByVal bv_strMDFD_BY As String, _
        ByVal bv_datMDFD_DT As DateTime, _
        ByVal bv_strWfData As String, _
         ByVal bv_strOrganizationType As Int32, _
        ByVal bv_strReportingTo As Int32, _
        ByRef br_dsDepotDataset As DepotDataSet) As Boolean
        Dim objTransaction As New Transactions()
        Try

            Dim objDEPOT As New Depots
            Dim blnUpdated As Boolean
            blnUpdated = objDEPOT.UpdateDEPOT(bv_i32DPT_ID, _
                  bv_strDPT_CD, bv_strDPT_NAM, _
                  bv_strCNTCT_PRSN_NAM, bv_strADDRSS_LN1_VC, _
                  bv_strADDRSS_LN2_VC, bv_strADDRSS_LN3_VC, _
                  bv_strVT_NO, _
                  bv_strEML_ID, bv_strPHN_NO, _
                  bv_strFX_NO, _
                  bv_strLG_PTH, bv_strMDFD_BY, _
                  bv_datMDFD_DT, _
                  bv_strOrganizationType, bv_strReportingTo, objTransaction)
            pub_UpdateBankDetails(br_dsDepotDataset, CInt(bv_i32DPT_ID), objTransaction)
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

#Region "pub_DeleteDEPOT() TABLE NAME:DEPOT"
    <OperationContract()> _
    Public Function pub_DeleteDEPOT(ByVal bv_i32DPT_ID As Int32) As Boolean
        Try
            Dim objDEPOT As New Depots
            Dim blnDeleted As Boolean
            blnDeleted = objDEPOT.DeleteDEPOT(bv_i32DPT_ID)
            Return blnDeleted
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBankDetail()"
    <OperationContract()> _
    Public Function pub_GetBankDetail(ByVal bv_i32DPT_ID As Int32) As DepotDataSet

        Try
            Dim dsDepotDataSet As DepotDataSet
            Dim objDepots As New Depots
            dsDepotDataSet = objDepots.GetBankDetail(bv_i32DPT_ID)
            Return (dsDepotDataSet)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateBankDetails"
    <OperationContract()> _
    Public Function pub_UpdateBankDetails(ByRef dsDepot As DepotDataSet, _
                                         ByVal depotID As Integer, ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dtDepot As DataTable
            Dim ObjDepots As New Depots
            dtDepot = dsDepot.Tables(DepotData._V_BANK_DETAIL)
            For Each drDepot As DataRow In dtDepot.Rows
                Dim IBAN_no As String = ""
                Dim SWIFT As String = ""
                If drDepot.RowState = DataRowState.Added Or drDepot.RowState = DataRowState.Modified Then

                    If ((drDepot.Item(DepotData.IBAN_NO)) Is DBNull.Value) Then
                        IBAN_no = String.Empty
                    Else
                        IBAN_no = CStr(drDepot.Item(DepotData.IBAN_NO))
                    End If
                    If ((drDepot.Item(DepotData.SWIFT_CD)) Is DBNull.Value) Then
                        SWIFT = String.Empty
                    Else
                        SWIFT = CStr(drDepot.Item(DepotData.SWIFT_CD))
                    End If
                End If

                Select Case drDepot.RowState
                    Case DataRowState.Added
                        Dim lngCreated As Long = ObjDepots.CreateBankDetail(depotID, _
                                                    CInt(drDepot.Item(DepotData.BNK_TYP_ID)), _
                                                    CStr(drDepot.Item(DepotData.BNK_NM)), _
                                                    CStr(drDepot.Item(DepotData.BNK_ADDRSS)), _
                                                    CStr(drDepot.Item(DepotData.ACCNT_NO)), _
                                                    IBAN_no, _
                                                    SWIFT, _
                                                    CInt(drDepot.Item(DepotData.CRRNCY_ID)), _
                                                    br_ObjTransactions)
                        drDepot.Item(DepotData.BNK_DTL_BIN) = lngCreated
                    Case DataRowState.Modified
                        ObjDepots.UpdateBankDetail(depotID, _
                                                    CInt(drDepot.Item(DepotData.BNK_DTL_BIN)), _
                                                    CInt(drDepot.Item(DepotData.BNK_TYP_ID)), _
                                                    CStr(drDepot.Item(DepotData.BNK_NM)), _
                                                    CStr(drDepot.Item(DepotData.BNK_ADDRSS)), _
                                                    CStr(drDepot.Item(DepotData.ACCNT_NO)), _
                                                    IBAN_no, _
                                                    SWIFT, _
                                                    CInt(drDepot.Item(DepotData.CRRNCY_ID)), _
                                                    br_ObjTransactions)
                    Case DataRowState.Deleted
                        ObjDepots.DeleteBankDetail(CInt(drDepot.Item(DepotData.BNK_DTL_BIN, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetAllDepotDetails() TABLE NAME: V_DEPOT"

    <OperationContract()> _
    Public Function pub_GetAllDepotDetails() As DepotDataSet

        Try
            Dim dsDepotDataSet As DepotDataSet
            Dim objDepots As New Depots
            dsDepotDataSet = objDepots.GetAllDepot()
            Return (dsDepotDataSet)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region
End Class