#Region "CleaningMethod.vb"
'*********************************************************************************************************************
'Name :
'      CleaningMethod.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(CleaningMethod.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                     10/8/2014 5:15:12 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class CleaningMethod

#Region "pub_GetCleaningMethodDetail() TABLE NAME:CLEANING_METHOD_DEATAIL"
    ''' <summary>
    ''' This method is used to get cleaning Method detail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetCleaningMethodDetail(ByVal bv_i64CleaningMethodTypeID As Int64) As CleaningMethodDataSet
        Dim objCleaningMethods As New CleaningMethods
        Try

            Dim dsCleaningMethod As CleaningMethodDataSet
            dsCleaningMethod = objCleaningMethods.GetCleaningMethodDetailByCleaningMethodTypeID(bv_i64CleaningMethodTypeID)
            Return dsCleaningMethod
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCleaningMethods = Nothing
        End Try
    End Function
#End Region

#Region "CreateCleaningMethod"
    ''' <summary>
    ''' This method is used to create cleaning Method detail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_CreateCleaningMethod(ByVal bv_i64CleaningMethodTypeID As Int64, _
                                         ByVal bv_strCleaningMethodTypeCode As String, _
                                         ByVal bv_strCleaningMethodTypeDescription As String, _
                                         ByVal bv_blnActiveID As Boolean, _
                                         ByVal bv_strCreatedBy As String, _
                                         ByVal bv_datCreatedDate As DateTime, _
                                         ByVal bv_strModifiedBy As String, _
                                         ByVal bv_datModifiedDate As DateTime, _
                                         ByVal bv_i32DepotID As Int32, _
                                         ByRef br_dsCleaningMethod As CleaningMethodDataSet) As Long
        Dim objTransaction As New Transactions()
        Try
            Dim objCleaningMethods As New CleaningMethods
            Dim lngCreated As Long
            lngCreated = objCleaningMethods.CreateCleaningMethod(bv_strCleaningMethodTypeCode, _
                                                                 bv_strCleaningMethodTypeDescription, _
                                                                 bv_blnActiveID, bv_strCreatedBy, _
                                                                 bv_datCreatedDate, bv_strModifiedBy, _
                                                                 bv_datModifiedDate, bv_i32DepotID, objTransaction)
            For Each drCleaningMethod As DataRow In br_dsCleaningMethod.Tables(CleaningMethodData._V_CLEANING_METHOD_DETAIL).Rows
                If drCleaningMethod.RowState <> DataRowState.Deleted Then
                    Dim strComments As String = ""
                    If Not IsDBNull(drCleaningMethod.Item(CleaningMethodData.CMMNTS_VC)) Then
                        strComments = drCleaningMethod.Item(CleaningMethodData.CMMNTS_VC).ToString
                    End If
                    objCleaningMethods.CreateCleaningMethodDetail(lngCreated, CLng(drCleaningMethod.Item(CleaningMethodData.CLNNG_TYP_ID)), strComments, objTransaction)
                End If
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

#Region "UpdateCleaningMethod"
    ''' <summary>
    ''' This method is used to update cleaning Method detail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_UpdateCleaningMethod(ByVal bv_i64CleaningMethodTypeID As Int64, _
                                         ByVal bv_strCleaningMethodTypeCode As String, _
                                         ByVal bv_strCleaningMethodTypeDescription As String, _
                                         ByVal bv_blnActiveID As Boolean, _
                                         ByVal bv_strCreatedBy As String, _
                                         ByVal bv_datCreatedDate As DateTime, _
                                         ByVal bv_strModifiedBy As String, _
                                         ByVal bv_datModifiedDate As DateTime, _
                                         ByVal bv_i32DepotID As Int32, _
                                         ByRef br_dsCleaningMethod As CleaningMethodDataSet) As Long
        Dim objTransaction As New Transactions()
        Try
            Dim objCleaningMethods As New CleaningMethods
            Dim blnUpdated As Boolean
            Dim lngCreated As Long
            blnUpdated = objCleaningMethods.UpdateCleaningMethod(bv_i64CleaningMethodTypeID, bv_strCleaningMethodTypeCode, _
                                                                 bv_strCleaningMethodTypeDescription, _
                                                                 bv_blnActiveID, bv_strModifiedBy, _
                                                                 bv_datModifiedDate, objTransaction)
            For Each drCleaningMethod As DataRow In br_dsCleaningMethod.Tables(CleaningMethodData._V_CLEANING_METHOD_DETAIL).Rows
                Dim strComments As String = ""
                Select Case drCleaningMethod.RowState
                    Case DataRowState.Added
                        ''for new entries based on row state
                        If Not IsDBNull(drCleaningMethod.Item(CleaningMethodData.CMMNTS_VC)) Then
                            strComments = drCleaningMethod.Item(CleaningMethodData.CMMNTS_VC).ToString
                        End If
                        objCleaningMethods.CreateCleaningMethodDetail(bv_i64CleaningMethodTypeID, CLng(drCleaningMethod.Item(CleaningMethodData.CLNNG_TYP_ID)), strComments, objTransaction)
                    Case DataRowState.Modified
                        ''for modified entries based on row state
                        If Not IsDBNull(drCleaningMethod.Item(CleaningMethodData.CMMNTS_VC)) Then
                            strComments = drCleaningMethod.Item(CleaningMethodData.CMMNTS_VC).ToString
                        End If
                        objCleaningMethods.UpdateCleaningMethodDetail(CLng(drCleaningMethod.Item(CleaningMethodData.CLNNG_MTHD_DTL_ID)), _
                                                                      bv_i64CleaningMethodTypeID, CLng(drCleaningMethod.Item(CleaningMethodData.CLNNG_TYP_ID)), strComments, objTransaction)
                    Case DataRowState.Deleted
                        ''for deleted entries based on row state
                        objCleaningMethods.DeleteCleaningMethodDetail(CLng(drCleaningMethod.Item(CleaningMethodData.CLNNG_MTHD_DTL_ID, DataRowVersion.Original)), objTransaction)
                End Select
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

#Region "pub_GetCleaningMethodType() TABLE NAME:CLEANING_METHOD"
    ''' <summary>
    ''' This method is used to get cleaning method Type Code. 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetCleaningMethodTypeCode(ByVal bv_strCleaningMethodTypeCode As String, _
                                                  ByVal bv_intDepotId As Integer) As Boolean
        Dim objCleaningMethods As New CleaningMethods
        Try
            Dim i32CleaningMethodTypeCodeCount As Int32 = 0
            i32CleaningMethodTypeCodeCount = objCleaningMethods.GetCleaningMethodTypeCodeCount(bv_strCleaningMethodTypeCode, bv_intDepotId)
            If i32CleaningMethodTypeCodeCount > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCleaningMethods = Nothing
        End Try
    End Function
#End Region

End Class
