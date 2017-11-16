Imports Microsoft.VisualBasic
Imports System.Web.HttpContext
Imports iInterchange.Framework.Common

#Region "LockData"
''' <summary>
''' This class is used for locking the record or estimate in whole application
''' </summary>
''' <remarks></remarks>
Public Class LockData
    Inherits DataTable

#Region "Declarations"
    Public Const RecordLockID As String = "RecordLockID"
    Public Const IPAddress As String = "IPAddress"
    Public Const ActivityName As String = "ActivityName"
    Public Const SessionID As String = "SessionID"
    Public Const RefNoField As String = "RefNoField"
    Public Const RefNo As String = "RefNo"
    Public Const EditLock As String = "EditLock"
    Public Const LockedBy As String = "LockedBy"
    Public Const LockTime As String = "LockedTime"
    Public Const ReleaseTime As String = "ReleaseTime"
#End Region

#Region "Constructor"
    Sub New()
        Me.Columns.Add(RecordLockID, GetType(String))
        Me.Columns.Add(IPAddress, GetType(String))
        Me.Columns.Add(ActivityName, GetType(String))
        Me.Columns.Add(SessionID, GetType(String))
        Me.Columns.Add(RefNoField, GetType(String))
        Me.Columns.Add(RefNo, GetType(String))
        Me.Columns.Add(EditLock, GetType(Boolean))
        Me.Columns.Add(LockedBy, GetType(String))
        Me.Columns.Add(LockTime, GetType(DateTime))
        Me.Columns.Add(ReleaseTime, GetType(DateTime))
    End Sub
#End Region

End Class

''' <summary>
''' This class is used for storing locked data collection
''' </summary>
''' <remarks></remarks>
Public Class LockDataCollection

    Sub New()
    End Sub

    ''' <summary>
    ''' This method is used for adding the locked record in application memory
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Public Sub Add(ByVal dr As DataRow)
        Try
            Dim dtRecordLock As LockData
            dtRecordLock = Current.Application("LockData")
            dtRecordLock.Rows.Add(dr)
            Current.Application("LockData") = dtRecordLock
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    ''' <summary>
    ''' This method is used for removing the locked record from application memory
    ''' </summary>
    ''' <param name="strLockID">Denotes Lock ID</param>
    ''' <remarks></remarks>
    Public Sub Remove(ByVal strLockID As String)
        Try
            Dim dtRecordLock As LockData
            dtRecordLock = Current.Application("LockData")
            Dim drsRecordLock As DataRow()
            drsRecordLock = dtRecordLock.Select(String.Concat("RecordLockID='", strLockID, "'"), "")
            If drsRecordLock.Length > 0 Then
                drsRecordLock(0).Delete()
            End If
            Current.Application("LockData") = dtRecordLock
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)

        End Try
    End Sub

    ''' <summary>
    ''' This method is used for removing all the locked records based on session id from application memory
    ''' </summary>
    ''' <param name="strSessionID">Denotes Session ID</param>
    ''' <remarks></remarks>
    Public Sub RemoveAll(ByVal strSessionID As String)
        Try
            Dim dtRecordLock As LockData
            dtRecordLock = Current.Application("LockData")
            Dim drsRecordLock As DataRow()
            drsRecordLock = dtRecordLock.Select(String.Concat("SessionID='", strSessionID, "'"), "")
            If drsRecordLock.Length > 0 Then
                For Each dr As DataRow In drsRecordLock
                    dr.Delete()
                Next
            End If
            Current.Application("LockData") = dtRecordLock
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)

        End Try
    End Sub

    ''' <summary>
    ''' This method is used for removing all the locked records based on session id, activity name, locked by user from application memory
    ''' </summary>
    ''' <param name="bv_strSessionID">Denotes Session ID</param>
    ''' <param name="bv_strActivityName">Denotes Activity Name</param>
    ''' <param name="bv_strLockedBy">Denotes Locked By</param>
    ''' <remarks></remarks>
    Public Sub RemoveAll(ByVal bv_strSessionID As String, ByVal bv_strActivityName As String, ByVal bv_strLockedBy As String)
        Try
            Dim dtRecordLock As LockData
            dtRecordLock = Current.Application("LockData")
            Dim drsRecordLock As DataRow()
            drsRecordLock = dtRecordLock.Select(String.Concat("SessionID='", bv_strSessionID, _
                                "' AND ActivityName='", bv_strActivityName, "' AND LockedBy='", bv_strLockedBy, "'"), "")
            If drsRecordLock.Length > 0 Then
                For Each dr As DataRow In drsRecordLock
                    dr.Delete()
                Next
            End If
            Current.Application("LockData") = dtRecordLock
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    ''' <summary>
    ''' This method is used to get locked record values from memory
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function Values() As DataTable
        Try
            Dim dtRecordLock As LockData
            If Not Current Is Nothing Then
                dtRecordLock = Current.Application("LockData")
            Else
                dtRecordLock = Nothing
            End If

            Return dtRecordLock
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
End Class
#End Region