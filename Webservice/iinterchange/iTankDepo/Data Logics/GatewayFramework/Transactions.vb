Imports Microsoft.VisualBasic
Imports System.Data.Common
Imports iInterchange.iTankDepo.Business.Common

Public Class Transactions
    Implements IDisposable
#Region "Variables"
    Private pvt_dbcon As DbConnection
    Private pvt_dbTrans As DbTransaction
#End Region

#Region "Constructor"
    Public Sub New()
        Dim ObjDao As New DataObjects
        pvt_dbcon = ObjDao.getConnection()
        pvt_dbcon.Open()
        pvt_dbTrans = pvt_dbcon.BeginTransaction()
        LogQuery("BeginTransaction", "New", "Transaction")
    End Sub
#End Region

#Region "Property Connection"
    Public ReadOnly Property Connection() As DbConnection
        Get
            Return pvt_dbcon
        End Get
    End Property
#End Region

#Region "Property Transaction"
    Public ReadOnly Property Transaction() As DbTransaction
        Get
            Return pvt_dbTrans
        End Get
    End Property
#End Region

#Region "sub Commit"
    Public Sub commit()
        pvt_dbTrans.Commit()
        pvt_dbcon.Close()
        LogQuery("Commit Transaction", "New", "Transaction")
    End Sub
#End Region

#Region "sub RollBack"
    Public Sub RollBack()
        pvt_dbTrans.Rollback()
        pvt_dbcon.Close()
        LogQuery("Rollback Transaction", "New", "Transaction")
    End Sub
#End Region

#Region "Log Queries"

    Private Sub LogQuery(ByVal Query As String, ByVal Method As String, ByVal Table As String)
        iErrorHandler.SaveQueryToLog(Method, Table, Query)
    End Sub

#End Region

#Region " dispose"
    '----------------------------------------------------------------
    ' Sub Dispose:
    '     Dispose of this object's resources.
    '----------------------------------------------------------------
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(True) ' as a service to those who might inherit from us
    End Sub

    '----------------------------------------------------------------
    ' Sub Dispose:
    '     Free the instance variables of this object.
    '----------------------------------------------------------------
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If (Not disposing) Then
            Exit Sub  ' we're being collected, so let the GC take care of this object
        End If

        If Not pvt_dbTrans Is Nothing Then
            pvt_dbTrans.Dispose()
        End If
        If Not pvt_dbcon Is Nothing Then
            pvt_dbcon.Dispose()
        End If

        pvt_dbTrans = Nothing
        pvt_dbcon = Nothing

    End Sub
#End Region
End Class
