Imports Microsoft.VisualBasic
Imports System.Data.Common
Imports System.Data
Imports System.Data.SqlClient
Imports iInterchange.iTankDepo.Business.Common

Public Class DataObjects
    Implements IDisposable

#Region "variables"
    Private pvt_ConnectionString As String
    Private pvt_Settings As Settings
    Private pvt_ObjdbFactory As DbProviderFactory
    Private pvt_DA As DbDataAdapter
    Private pvt_CMBld As DbCommandBuilder
    Private pvt_dbConn As DbConnection
    Private pvt_strQuery As String

    Private pvt_dbParmName As String
    Private pvt_dbParamvalue As String
    Private pvt_htblParm As Hashtable
    Private pvt_Command As DbCommand

#End Region

#Region "init"
    Sub init()
        pvt_Settings = New Settings
        ' the getdbfactory will return the instance of the specific dbprovider class
        pvt_ObjdbFactory = pvt_Settings.getdbFactory
        'the connection string specified in the settings will be initialised
        pvt_ConnectionString = pvt_Settings.dbConnectionString

    End Sub
#End Region

#Region " Method New "
    Sub New()
        init()
    End Sub
    Sub New(ByVal bv_strQry As String)
        init()
        pvt_strQuery = bv_strQry
        ' initAdapter()
    End Sub
    Sub New(ByVal bv_strQry As String, ByVal bv_ParamField As String, ByVal bv_ParamValue As String)
        Try
            init()

            pvt_strQuery = bv_strQry
            pvt_dbParmName = bv_ParamField
            pvt_dbParamvalue = bv_ParamValue
            ' initAdapter()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub New(ByVal bv_strQry As String, ByRef br_htblParm As Hashtable)
        Try
            init()

            pvt_strQuery = bv_strQry
            pvt_htblParm = br_htblParm
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub New(ByVal bv_strQry As String, ByVal bv_connectionStr As String)
        init()
        pvt_strQuery = bv_strQry
        pvt_ConnectionString = bv_connectionStr
    End Sub
#End Region

#Region "Get Connection"
    Public Function getConnection() As DbConnection
        pvt_dbConn = pvt_ObjdbFactory.CreateConnection
        pvt_dbConn.ConnectionString = pvt_ConnectionString
        Return pvt_dbConn
    End Function
#End Region

#Region "open connection"
    Private Sub OpenConnection()
        pvt_dbConn = pvt_ObjdbFactory.CreateConnection
        With pvt_dbConn
            .ConnectionString = pvt_ConnectionString
            .Open()
        End With
    End Sub

#End Region

#Region "Close connection"
    Private Sub CloseConnection()
        If Not pvt_dbConn Is Nothing AndAlso (pvt_dbConn.State = ConnectionState.Open OrElse pvt_dbConn.State = ConnectionState.Broken) Then
            pvt_dbConn.Close()
        End If
    End Sub
#End Region

#Region "PrepareLoadCommand"
    '----------------------------------------------------------------
    ' Function GetLoadCommand:
    '   Initialize the parameterized Load command for the DataAdapter
    '----------------------------------------------------------------
    Private Function PrepareLoadCommand(ByVal bv_strQuery As String) As DbCommand
        Try

            If pvt_Command Is Nothing Then
                'open connection
                OpenConnection()

                'create the command object and initialize the command object
                pvt_Command = pvt_ObjdbFactory.CreateCommand
                With pvt_Command
                    .Connection = pvt_dbConn
                    .CommandText = bv_strQuery
                    .CommandTimeout = pvt_Settings.CommandTimeOut
                    .CommandType = CommandType.Text
                    If Not pvt_dbParmName Is Nothing Then
                        AttachParameterandValues(pvt_dbParmName, pvt_dbParamvalue)
                    End If
                    If Not pvt_htblParm Is Nothing Then
                        If pvt_htblParm.Count > 0 Then
                            AttachParameterandValues(pvt_htblParm)
                        End If
                    End If
                End With
            End If
            PrepareLoadCommand = pvt_Command
        Catch ex As Exception

        Finally
            If Not pvt_Command Is Nothing Then
                pvt_Command = Nothing
            End If
        End Try

    End Function
#End Region

#Region "PrepareLoadCommand with transaction"
    '----------------------------------------------------------------
    ' Function GetLoadCommand:
    '   Initialize the parameterized Load command for the DataAdapter
    '----------------------------------------------------------------
    Private Function PrepareLoadCommand(ByVal bv_strQuery As String, ByRef objtrans As Transactions) As DbCommand
        Try

            If pvt_Command Is Nothing Then

                'create the command object and initialize the command object
                pvt_Command = pvt_ObjdbFactory.CreateCommand
                With pvt_Command
                    .Connection = objtrans.Connection
                    .CommandTimeout = pvt_Settings.CommandTimeOut
                    .Transaction = objtrans.Transaction
                    .CommandText = bv_strQuery
                    .CommandType = CommandType.Text
                    If Not pvt_dbParmName Is Nothing Then
                        AttachParameterandValues(pvt_dbParmName, pvt_dbParamvalue)
                    End If
                    If Not pvt_htblParm Is Nothing Then
                        If pvt_htblParm.Count > 0 Then
                            AttachParameterandValues(pvt_htblParm)
                        End If
                    End If
                End With
            End If
            PrepareLoadCommand = pvt_Command
        Catch ex As Exception

        Finally
            If Not pvt_Command Is Nothing Then
                pvt_Command = Nothing
            End If
        End Try

    End Function
#End Region

#Region " Method Fill "
    '**
    'Fills the Data set with the data from the query or table specified in the new method.

    Public Sub Fill(ByRef data As DataSet, ByVal tablename As String)

        pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
        With pvt_DA
            Try
                .SelectCommand = PrepareLoadCommand(pvt_strQuery)
                .Fill(data, tablename)

            Finally
                If Not .SelectCommand Is Nothing Then
                    If Not .SelectCommand.Connection Is Nothing Then
                        .SelectCommand.Connection.Dispose()
                    End If
                    .SelectCommand.Dispose()
                End If
                'Close Connection
                CloseConnection()
                .Dispose()

                LogQuery(pvt_strQuery, "Fill", tablename)

            End Try
        End With
    End Sub
    '**
    'Fills the Data table with the data from the query or table specified in the new method.

    Public Sub Fill(ByRef data As DataTable)


        pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
        With pvt_DA
            Try
                .SelectCommand = PrepareLoadCommand(pvt_strQuery)
                .Fill(data)

            Finally
                If Not .SelectCommand Is Nothing Then
                    If Not .SelectCommand.Connection Is Nothing Then
                        .SelectCommand.Connection.Close()
                        .SelectCommand.Connection.Dispose()
                    End If
                    .SelectCommand.Dispose()
                End If
                'Close Connection
                CloseConnection()
                .Dispose()

                LogQuery(pvt_strQuery, "Fill", "")

            End Try
        End With
    End Sub
#End Region

#Region " Method Fill transaction"
    '**
    'Fills the Data set with the data from the query or table specified in the new method.

    'Public Sub Fill(ByRef data As DataSet, ByVal tablename As String, ByRef objTrans As Transactions)
    '    pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
    '    With pvt_DA
    '        Try
    '            .SelectCommand = PrepareLoadCommand(pvt_strQuery, objTrans)
    '            .Fill(data, tablename)
    '        Finally
    '            If Not .SelectCommand Is Nothing Then
    '                If Not .SelectCommand.Connection Is Nothing Then
    '                    .SelectCommand.Connection.Close()
    '                    .SelectCommand.Connection.Dispose()
    '                End If
    '                .SelectCommand.Dispose()
    '            End If
    '            'Close Connection
    '            CloseConnection()
    '            .Dispose()

    '            LogQuery(pvt_strQuery, "Fill", tablename)
    '        End Try
    '    End With
    'End Sub

    Public Sub Fill(ByRef data As DataSet, ByVal tablename As String, ByRef objTrans As Transactions)
        pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
        With pvt_DA
            Try
                .SelectCommand = PrepareLoadCommand(pvt_strQuery, objTrans)
                .Fill(data, tablename)
            Finally
                'If Not .SelectCommand Is Nothing Then
                '    If Not .SelectCommand.Connection Is Nothing Then
                '        .SelectCommand.Connection.Close()
                '        .SelectCommand.Connection.Dispose()
                '    End If
                '    .SelectCommand.Dispose()
                'End If
                ''Close Connection
                'CloseConnection()
                .Dispose()

                LogQuery(pvt_strQuery, "Fill", tablename)
            End Try
        End With
    End Sub

    '**
    'Fills the Data table with the data from the query or table specified in the new method.

    Public Sub Fill(ByRef data As DataTable, ByRef objTrans As Transactions)
        pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
        With pvt_DA
            Try
                .SelectCommand = PrepareLoadCommand(pvt_strQuery, objTrans)
                .Fill(data)
            Finally
                .Dispose()
                LogQuery(pvt_strQuery, "Fill", "")
            End Try
        End With
    End Sub
#End Region

#Region " Method Update "
    Public Sub Update(ByRef br_dsChangedData As DataSet, ByVal bv_StrTableName As String)
        pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
        With pvt_DA
            Try
                'Open Connection
                OpenConnection()
                .Update(br_dsChangedData, bv_StrTableName)
                If Not br_dsChangedData.HasErrors Then
                    br_dsChangedData.AcceptChanges()
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not .InsertCommand Is Nothing Then
                    If Not .InsertCommand.Connection Is Nothing Then
                        .InsertCommand.Connection.Dispose()
                    End If
                    .InsertCommand.Dispose()
                End If

                If Not .UpdateCommand Is Nothing Then
                    If Not .UpdateCommand.Connection Is Nothing Then
                        .UpdateCommand.Connection.Dispose()
                    End If
                    .UpdateCommand.Dispose()
                End If
                CloseConnection()
                .Dispose()
                LogQuery(pvt_strQuery, "Update", bv_StrTableName)
            End Try
        End With
    End Sub

    Public Sub Update(ByRef br_dtChangedData As DataTable)
        pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
        With pvt_DA
            Try
                'Open Connection
                OpenConnection()
                .Update(br_dtChangedData)
            Catch ex As Exception
                Throw ex
            Finally
                If Not .InsertCommand Is Nothing Then
                    If Not .InsertCommand.Connection Is Nothing Then
                        .InsertCommand.Connection.Dispose()
                    End If
                    .InsertCommand.Dispose()
                End If

                If Not .UpdateCommand Is Nothing Then
                    If Not .UpdateCommand.Connection Is Nothing Then
                        .UpdateCommand.Connection.Dispose()
                    End If
                    .UpdateCommand.Dispose()
                End If
                'Close Connection
                CloseConnection()
                .Dispose()
                LogQuery(pvt_strQuery, "Update", "")
            End Try
        End With
    End Sub


#End Region

#Region "PrepareProcCommand"
    '----------------------------------------------------------------
    ' Function PrepareProcCommand:
    '----------------------------------------------------------------
    Private Function PrepareProcCommand(ByRef br_dtParam As DataTable) As DbCommand
        If pvt_Command Is Nothing Then
            'open connection
            OpenConnection()
            'create the command object and initialize the command object
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            pvt_Command.Connection = pvt_dbConn
            pvt_Command.CommandText = br_dtParam.TableName
            pvt_Command.CommandType = CommandType.StoredProcedure
            pvt_Command.CommandTimeout = pvt_Settings.CommandTimeOut
            AttachParameterandFields(br_dtParam)
        End If
        PrepareProcCommand = pvt_Command
    End Function

    Private Function PrepareProcCommand(ByRef br_dtParam As DataTable, ByRef br_ObjTrans As Transactions) As DbCommand
        If pvt_Command Is Nothing Then
            'create the command object and initialize the command object
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            pvt_Command.Connection = br_ObjTrans.Connection
            pvt_Command.Transaction = br_ObjTrans.Transaction
            pvt_Command.CommandText = br_dtParam.TableName
            pvt_Command.CommandType = CommandType.StoredProcedure
            pvt_Command.CommandTimeout = pvt_Settings.CommandTimeOut
            AttachParameterandFields(br_dtParam)
        End If
        PrepareProcCommand = pvt_Command
    End Function
#End Region

#Region "Method Execute Procedure : Insert"
    Public Function ExecuteProcedure(ByRef br_dtParam As DataTable) As Boolean
        Try
            If Not br_dtParam Is Nothing Then
                If br_dtParam.Rows.Count > 0 Then
                    pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
                    pvt_DA.InsertCommand = PrepareProcCommand(br_dtParam)
                    pvt_DA.Update(br_dtParam)
                    If br_dtParam.HasErrors Then
                        br_dtParam.GetErrors(0).ClearErrors()
                        ExecuteProcedure = False
                    Else
                        br_dtParam.AcceptChanges()
                        ExecuteProcedure = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not pvt_DA.InsertCommand Is Nothing Then
                If Not pvt_DA.InsertCommand.Connection Is Nothing Then
                    pvt_DA.InsertCommand.Connection.Close()
                    pvt_DA.InsertCommand.Connection.Dispose()
                End If
                pvt_DA.InsertCommand.Dispose()
            End If
            'Close Connection
            CloseConnection()
            pvt_DA.Dispose()
        End Try

    End Function

    Public Function ExecuteProcedure(ByRef br_dtParam As DataTable, ByRef objTrans As Transactions) As Boolean
        Try
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            If Not br_dtParam Is Nothing Then
                If br_dtParam.Rows.Count > 0 Then
                    pvt_DA.InsertCommand = PrepareProcCommand(br_dtParam, objTrans)
                    pvt_DA.Update(br_dtParam)
                    If br_dtParam.HasErrors Then
                        br_dtParam.GetErrors(0).ClearErrors()
                        ExecuteProcedure = False
                    Else
                        br_dtParam.AcceptChanges()
                        ExecuteProcedure = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            pvt_DA.Dispose()
        End Try

    End Function

#End Region

#Region "Method Execute Procedure : Select"
    Public Function ExecuteProcedure(ByRef br_dtFill As DataTable, ByRef br_htblParam As Hashtable, ByVal bv_ProcedureName As String) As Boolean
        Try
            If Not br_htblParam Is Nothing Then
                pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
                pvt_DA.SelectCommand = PrepareUpdateCommand(br_htblParam, bv_ProcedureName, CommandType.StoredProcedure)
                pvt_DA.Fill(br_dtFill)
                If br_dtFill.HasErrors Then
                    br_dtFill.GetErrors(0).ClearErrors()
                    ExecuteProcedure = False
                Else
                    br_dtFill.AcceptChanges()
                    ExecuteProcedure = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not pvt_DA.SelectCommand Is Nothing Then
                If Not pvt_DA.SelectCommand.Connection Is Nothing Then
                    pvt_DA.SelectCommand.Connection.Close()
                    pvt_DA.SelectCommand.Connection.Dispose()
                End If
                pvt_DA.SelectCommand.Dispose()
            End If
            'Close Connection
            CloseConnection()
            pvt_DA.Dispose()
        End Try

    End Function

    Public Function ExecuteProcedure(ByRef br_dtFill As DataTable, ByRef br_htblParam As Hashtable, ByVal bv_ProcedureName As String, ByRef objTrans As Transactions) As Boolean
        Try
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            If Not br_htblParam Is Nothing Then
                pvt_DA.SelectCommand = PrepareUpdateCommand(br_htblParam, bv_ProcedureName, CommandType.StoredProcedure, objTrans)
                pvt_DA.Fill(br_dtFill)

                If br_dtFill.HasErrors Then
                    br_dtFill.GetErrors(0).ClearErrors()
                    ExecuteProcedure = False
                Else
                    br_dtFill.AcceptChanges()
                    ExecuteProcedure = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            'Close Connection
            CloseConnection()
            pvt_DA.Dispose()
        End Try
    End Function

#End Region

#Region "Method ExecuteScalar"
    Public Function ExecuteScalar() As String
        Dim dbcmd As DbCommand
        dbcmd = PrepareLoadCommand(pvt_strQuery)
        With dbcmd
            Try
                Dim Result As Object = dbcmd.ExecuteScalar
                If IsDBNull(Result) Then
                    ExecuteScalar = Nothing
                Else
                    'HAS A VALUE
                    ExecuteScalar = Result
                End If
            Catch ex As Exception
                ExecuteScalar = Nothing
                Throw ex
            Finally
                If Not dbcmd Is Nothing Then
                    If Not .Connection Is Nothing Then
                        .Connection.Dispose()
                    End If
                    .Dispose()
                End If
                'Close Connection
                CloseConnection()
                LogQuery(pvt_strQuery, "ExecuteScalar", pvt_strQuery)
            End Try
        End With
    End Function
#End Region

#Region "Method ExecuteScalar with transaction"
    Public Function ExecuteScalar(ByRef objTrans As Transactions) As String
        Dim dbcmd As DbCommand
        dbcmd = PrepareLoadCommand(pvt_strQuery, objTrans)
        With dbcmd
            Try
                ExecuteScalar = dbcmd.ExecuteScalar()
            Finally
                .Dispose()
                LogQuery(pvt_strQuery, "ExecuteScalar T", pvt_strQuery)
            End Try
        End With
    End Function
#End Region

#Region "Method Execute Non Query"
    Public Function ExecuteNonQuery(ByRef br_drTable As DataTable, ByVal CommandText As String) As Integer
        If Not br_drTable Is Nothing Then
            Dim dbcmd As DbCommand
            dbcmd = PrepareCommand(br_drTable, CommandText, CommandType.Text)
            With dbcmd
                Try
                    ExecuteNonQuery = .ExecuteNonQuery()
                Finally
                    If Not dbcmd Is Nothing Then
                        If Not dbcmd.Connection Is Nothing Then
                            .Connection.Dispose()
                        End If
                        .Dispose()
                    End If
                    'Close Connection
                    CloseConnection()
                    LogQuery(pvt_strQuery, "Execute Non Query", CommandText)
                End Try
            End With
        End If
    End Function

    Public Function ExecuteNonQuery(ByRef br_drTable As DataTable, ByVal CommandText As String, ByRef br_ObjTrans As Transactions) As Integer
        If Not br_drTable Is Nothing Then
            Dim dbcmd As DbCommand
            dbcmd = PrepareCommand(br_drTable, CommandText, CommandType.Text, br_ObjTrans)
            With dbcmd
                Try
                    ExecuteNonQuery = .ExecuteNonQuery()
                Finally
                    If Not dbcmd Is Nothing Then
                        If Not dbcmd.Connection Is Nothing Then
                            .Connection.Dispose()
                        End If
                        .Dispose()
                    End If
                    LogQuery(pvt_strQuery, "Execute Non Query T", CommandText)
                End Try
            End With
        End If
    End Function
#End Region

#Region "Method Execute Non Query : witout parameter"
    Public Function ExecuteNonQuery(ByVal CommandText As String) As Integer
        If pvt_Command Is Nothing Then
            'open connection
            OpenConnection()
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            With pvt_Command
                'create the command object and initialize the command object
                .Connection = pvt_dbConn
                .CommandText = CommandText
                .CommandType = CommandType.Text
                .CommandTimeout = pvt_Settings.CommandTimeOut
                Try
                    ExecuteNonQuery = .ExecuteNonQuery()
                Finally
                    If Not pvt_Command Is Nothing Then
                        If Not pvt_Command.Connection Is Nothing Then
                            .Connection.Dispose()
                        End If
                        .Dispose()
                    End If
                    CloseConnection()
                    LogQuery(pvt_strQuery, "Execute Non Query", CommandText)
                End Try
            End With
        End If
    End Function

    Public Function ExecuteNonQuery(ByVal CommandText As String, ByRef br_ObjTrans As Transactions) As Integer
        If pvt_Command Is Nothing Then
            'create the command object and initialize the command object
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            With pvt_Command
                .Connection = br_ObjTrans.Connection
                .Transaction = br_ObjTrans.Transaction
                .CommandText = CommandText
                .CommandType = CommandType.Text
                .CommandTimeout = pvt_Settings.CommandTimeOut
                Try
                    ExecuteNonQuery = .ExecuteNonQuery()
                Finally
                    If Not pvt_Command Is Nothing Then
                        'If Not pvt_Command.Connection Is Nothing Then
                        '    .Connection.Dispose()
                        'End If
                        .Dispose()
                    End If
                    LogQuery(pvt_strQuery, "Execute Non Query T", CommandText)
                End Try
            End With
        End If
    End Function
#End Region

#Region "Method Insert Row parameter datarow"
    Public Function InsertRow(ByRef br_drInsert As DataRow, ByVal CommandText As String) As Boolean
        If Not br_drInsert Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareInsertRowCommand(br_drInsert, "Insert", CommandText, CommandType.Text)
                    .InsertCommand.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    If Not .InsertCommand Is Nothing Then
                        If Not .InsertCommand.Connection Is Nothing Then
                            .InsertCommand.Connection.Dispose()
                        End If
                        .InsertCommand.Dispose()
                    End If
                    CloseConnection()
                    .Dispose()
                End Try
            End With
        End If
    End Function
    Public Function InsertRow(ByRef br_drInsert As DataRow, ByVal CommandText As String, ByRef br_ObjTrans As Transactions) As Boolean
        If Not br_drInsert Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareInsertRowCommand(br_drInsert, "Insert", CommandText, CommandType.Text, br_ObjTrans)
                    .InsertCommand.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    .Dispose()
                End Try
            End With
        End If
    End Function
    
#End Region

#Region "Method Update Row parameter datarow"

    Public Function UpdateRow(ByRef br_drUpdate As DataRow, ByVal CommandText As String) As Boolean
        If Not br_drUpdate Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareInsertRowCommand(br_drUpdate, "Update", CommandText, CommandType.Text)
                    .InsertCommand.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    If Not .InsertCommand Is Nothing Then
                        If Not .InsertCommand.Connection Is Nothing Then
                            .InsertCommand.Connection.Dispose()
                        End If
                        .InsertCommand.Dispose()
                    End If
                    CloseConnection()
                    .Dispose()
                End Try
            End With
        End If
    End Function
    Public Function UpdateRow(ByRef br_drUpdate As DataRow, ByVal CommandText As String, ByRef br_ObjTrans As Transactions) As Boolean
        If Not br_drUpdate Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareInsertRowCommand(br_drUpdate, "Update", CommandText, CommandType.Text, br_ObjTrans)
                    .InsertCommand.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    .Dispose()
                End Try
            End With
        End If
    End Function

#End Region

#Region "Method Update Row parameter hastable"

    Public Function UpdateRow(ByRef br_htblUpdate As Hashtable, ByVal CommandText As String) As Boolean
        If Not br_htblUpdate Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareUpdateCommand(br_htblUpdate, CommandText, CommandType.Text)
                    .InsertCommand.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    If Not .InsertCommand Is Nothing Then
                        If Not .InsertCommand.Connection Is Nothing Then
                            .InsertCommand.Connection.Dispose()
                        End If
                        .InsertCommand.Dispose()
                    End If
                    CloseConnection()
                    .Dispose()
                End Try
            End With
        End If
    End Function
    Public Function UpdateRow(ByRef br_htblUpdate As Hashtable, ByVal CommandText As String, ByRef br_ObjTrans As Transactions) As Boolean
        If Not br_htblUpdate Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareUpdateCommand(br_htblUpdate, CommandText, CommandType.Text, br_ObjTrans)
                    .InsertCommand.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    .Dispose()
                End Try
            End With
        End If
    End Function

#End Region

#Region "Method Delete Row"

    Public Function DeleteRow(ByRef br_drDelete As DataRow, ByVal CommandText As String) As Boolean
        If Not br_drDelete Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareInsertRowCommand(br_drDelete, "Delete", CommandText, CommandType.Text)
                    .InsertCommand.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    If Not .InsertCommand Is Nothing Then
                        If Not .InsertCommand.Connection Is Nothing Then
                            .InsertCommand.Connection.Dispose()
                        End If
                        .InsertCommand.Dispose()
                    End If
                    CloseConnection()
                    .Dispose()
                End Try
            End With
        End If
    End Function
    Public Function DeleteRow(ByRef br_drDelete As DataRow, ByVal CommandText As String, ByRef br_ObjTrans As Transactions) As Boolean
        If Not br_drDelete Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareInsertRowCommand(br_drDelete, "Delete", CommandText, CommandType.Text, br_ObjTrans)
                    .InsertCommand.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    .Dispose()
                End Try
            End With
        End If
    End Function

#End Region

#Region "Method Insert"
    Public Function Insert(ByRef br_dtInsert As DataTable, ByVal CommandText As String) As Boolean
        If Not br_dtInsert Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareCommand(br_dtInsert, CommandText, CommandType.Text)
                    .Update(br_dtInsert)
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    If Not .InsertCommand Is Nothing Then
                        If Not .InsertCommand.Connection Is Nothing Then
                            .InsertCommand.Connection.Dispose()
                        End If
                        .InsertCommand.Dispose()
                    End If
                    .Dispose()
                    CloseConnection()
                    LogQuery(pvt_strQuery, "Insert", CommandText)
                End Try
            End With
        End If
    End Function
    Public Function Insert(ByRef br_dtInsert As DataTable, ByVal CommandText As String, ByRef br_ObjTrans As Transactions) As Boolean
        If Not br_dtInsert Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareCommand(br_dtInsert, CommandText, CommandType.Text, br_ObjTrans)
                    .Update(br_dtInsert)
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    .Dispose()
                    LogQuery(pvt_strQuery, "Insert T", CommandText)
                End Try
            End With
        End If
    End Function
#End Region

#Region "Method Update"
    Public Function Update(ByRef br_drUpdate As DataTable, ByVal CommandText As String) As Boolean
        If Not br_drUpdate Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareCommand(br_drUpdate, CommandText, CommandType.Text)
                    .Update(br_drUpdate)
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    If Not .InsertCommand Is Nothing Then
                        If Not .InsertCommand.Connection Is Nothing Then
                            .InsertCommand.Connection.Dispose()
                        End If
                        .InsertCommand.Dispose()
                    End If
                    .Dispose()
                    CloseConnection()
                    LogQuery(pvt_strQuery, "Update", CommandText)
                End Try
            End With
        End If
    End Function

    Public Function Update(ByRef br_drUpdate As DataTable, ByVal CommandText As String, ByRef br_ObjTrans As Transactions) As Boolean
        If Not br_drUpdate Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareCommand(br_drUpdate, CommandText, CommandType.Text, br_ObjTrans)
                    .Update(br_drUpdate)
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    .Dispose()
                    LogQuery(pvt_strQuery, "Update T", CommandText)
                End Try
            End With
        End If
    End Function
#End Region

#Region "Method Delete"
    Public Function Delete(ByRef br_drDelete As DataTable, ByVal CommandText As String) As Boolean
        If Not br_drDelete Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareCommand(br_drDelete, CommandText, CommandType.Text)
                    .Update(br_drDelete)
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    If Not .InsertCommand Is Nothing Then
                        If Not .InsertCommand.Connection Is Nothing Then
                            .InsertCommand.Connection.Dispose()
                        End If
                        .InsertCommand.Dispose()
                    End If
                    CloseConnection()
                    .Dispose()
                    LogQuery(pvt_strQuery, "Delete", CommandText)
                End Try
            End With
        End If
    End Function

    Public Function Delete(ByRef br_drDelete As DataTable, ByVal CommandText As String, ByRef br_ObjTrans As Transactions) As Boolean
        If Not br_drDelete Is Nothing Then
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            With pvt_DA
                Try
                    .InsertCommand = PrepareCommand(br_drDelete, CommandText, CommandType.Text, br_ObjTrans)
                    .Update(br_drDelete)
                    Return True
                Catch ex As Exception
                    Throw ex
                Finally
                    .Dispose()
                    LogQuery(pvt_strQuery, "Delete T", CommandText)
                End Try
            End With
        End If
    End Function
#End Region

#Region "PrepareCommand"
    Private Function PrepareCommand(ByRef br_dtInsert As DataTable, ByVal commandText As String, ByVal commandtype As CommandType) As DbCommand
        If pvt_Command Is Nothing Then
            'open connection
            OpenConnection()

            'create the command object and initialize the command object
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            pvt_Command.Connection = pvt_dbConn
            pvt_Command.CommandText = commandText
            pvt_Command.CommandType = commandtype
            pvt_Command.CommandTimeout = pvt_Settings.CommandTimeOut

            'AttachParameterandValues(br_dtInsert)
            AttachParameterandFields(br_dtInsert)
        End If
        Return pvt_Command
    End Function
    Private Function PrepareCommand(ByRef br_dtInsert As DataTable, ByVal commandText As String, ByVal commandtype As CommandType, ByRef br_ObjTrans As Transactions) As DbCommand
        If pvt_Command Is Nothing Then
            'create the command object and initialize the command object
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            pvt_Command.Connection = br_ObjTrans.Connection
            pvt_Command.Transaction = br_ObjTrans.Transaction
            pvt_Command.CommandText = commandText
            pvt_Command.CommandType = commandtype
            pvt_Command.CommandTimeout = pvt_Settings.CommandTimeOut

            'AttachParameterandValues(br_dtInsert)
            AttachParameterandFields(br_dtInsert)
        End If
        Return pvt_Command
    End Function
#End Region

#Region "prepare insertrow command  with data row parameter"
    Private Function PrepareInsertRowCommand(ByRef br_drInsert As DataRow, ByVal bv_strQueryType As String, ByVal commandText As String, ByVal commandtype As CommandType) As DbCommand

        If pvt_Command Is Nothing Then
            'open connection
            OpenConnection()

            'create the command object and initialize the command object
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            pvt_Command.Connection = pvt_dbConn
            pvt_Command.CommandText = commandText
            pvt_Command.CommandType = commandtype
            pvt_Command.CommandTimeout = pvt_Settings.CommandTimeOut
            'AttachParameterandValues(br_drInsert)

            Dim objHashParameterArray As New Hashtable

            If pvt_Settings.Provider = "System.Data.OracleClient" Then
                Select Case bv_strQueryType
                    Case "Update"
                        objHashParameterArray = GetParametersFromUpdateQuery(commandText)
                    Case "Delete"
                        objHashParameterArray = GetParametersFromDeleteQuery(commandText)
                    Case "Insert"
                        objHashParameterArray = GetParametersFromInsertQuery(commandText)
                End Select
                AttachParameterandValues(br_drInsert, objHashParameterArray)
            Else
                AttachParameterandValues(br_drInsert)
            End If
        End If
        Return pvt_Command
    End Function
    Private Function PrepareInsertRowCommand(ByRef br_drInsert As DataRow, ByVal bv_strQueryType As String, ByVal commandText As String, ByVal commandtype As CommandType, ByRef br_ObjTrans As Transactions) As DbCommand
        If pvt_Command Is Nothing Then
            'create the command object and initialize the command object
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            pvt_Command.Connection = br_ObjTrans.Connection
            pvt_Command.Transaction = br_ObjTrans.Transaction
            pvt_Command.CommandText = commandText
            pvt_Command.CommandType = commandtype
            pvt_Command.CommandTimeout = pvt_Settings.CommandTimeOut

            Dim objHashParameterArray As New Hashtable

            If pvt_Settings.Provider = "System.Data.OracleClient" Then
                Select Case bv_strQueryType
                    Case "Update"
                        objHashParameterArray = GetParametersFromUpdateQuery(commandText)
                    Case "Delete"
                        objHashParameterArray = GetParametersFromDeleteQuery(commandText)
                    Case "Insert"
                        objHashParameterArray = GetParametersFromInsertQuery(commandText)
                End Select
                AttachParameterandValues(br_drInsert, objHashParameterArray)
            Else
                AttachParameterandValues(br_drInsert)
            End If
        End If
        Return pvt_Command
    End Function


#End Region

#Region "Get Parameters from InsertQuery()"
    Function GetParametersFromInsertQuery(ByVal bv_strInsertQuery As String) As Hashtable

        Dim arr_ParameterArray As String()
        Dim StrTemp As String
        Dim objHashTable As New Hashtable

        StrTemp = bv_strInsertQuery.Substring(bv_strInsertQuery.ToUpper().IndexOf("VALUES"))
        StrTemp = StrTemp.Substring(7)
        StrTemp = StrTemp.Replace("(", "")
        StrTemp = StrTemp.Replace(":", "")
        StrTemp = StrTemp.Replace(")", "")
        arr_ParameterArray = StrTemp.Split(",")

        For count As Integer = 0 To arr_ParameterArray.Length - 1
            StrTemp = arr_ParameterArray(count).ToUpper().Replace(" ", "")
            If objHashTable(StrTemp) = Nothing Then
                objHashTable.Add(StrTemp, StrTemp)
            End If
        Next
        Return objHashTable
    End Function
#End Region

#Region "Get Parameters from DeleteQuery()"
    Function GetParametersFromDeleteQuery(ByVal bv_strDeleteQuery As String) As Hashtable
        Dim StrTemp As String
        Dim arr_ParamArray As String()


        Dim objHashTable As New Hashtable

        arr_ParamArray = bv_strDeleteQuery.Split(" ")

        Dim count As Integer

        For count = 0 To arr_ParamArray.Length - 1
            StrTemp = arr_ParamArray(count)
            If StrTemp.IndexOf(":") >= 0 Then
                StrTemp = StrTemp.Substring(StrTemp.IndexOf(":")).Replace(" ", "").Replace(":", "").ToUpper()
                If objHashTable(StrTemp) = Nothing Then
                    objHashTable.Add(StrTemp, StrTemp)
                End If

            End If
        Next


        Return objHashTable
    End Function
#End Region

#Region "Get Parameters from UpdateQuery()"
    Function GetParametersFromUpdateQuery(ByVal bv_strUpdateQuery As String) As Hashtable

        Dim arr_ParameterArray As String()
        Dim StrTemp As String
        Dim StrWhereTemp As String
        Dim objHashTable As New Hashtable
        Dim count As Integer
        StrTemp = bv_strUpdateQuery.Substring(bv_strUpdateQuery.ToUpper().IndexOf("SET"))

        StrTemp = StrTemp.Substring(3)

        If StrTemp.ToLower().IndexOf("where") >= 0 Then
            StrWhereTemp = StrTemp.Substring(StrTemp.ToLower().IndexOf("where"))
            StrWhereTemp = StrWhereTemp.Substring(5)
            arr_ParameterArray = StrWhereTemp.Split(" ")

            For count = 0 To arr_ParameterArray.Length - 1
                StrWhereTemp = arr_ParameterArray(count)
                If StrWhereTemp.IndexOf(":") >= 0 Then
                    StrWhereTemp = StrWhereTemp.Substring(StrWhereTemp.IndexOf(":")).Replace(" ", "").Replace(":", "").ToUpper()
                    If objHashTable(StrWhereTemp) = Nothing Then
                        objHashTable.Add(StrWhereTemp, StrWhereTemp)
                    End If

                End If
            Next
        End If


        StrTemp = StrTemp.Substring(0, StrTemp.ToLower().IndexOf("where"))
        arr_ParameterArray = StrTemp.Split(",")

        For count = 0 To arr_ParameterArray.Length - 1
            StrTemp = arr_ParameterArray(count).Substring(arr_ParameterArray(count).IndexOf(":")).ToUpper()
            StrTemp = StrTemp.Replace(":", "")
            StrTemp = StrTemp.Replace(" ", "")
            If objHashTable(StrTemp) = Nothing Then
                objHashTable.Add(StrTemp, StrTemp)
            End If

        Next


        Return objHashTable

    End Function
#End Region

#Region "prepare Update command with hastable parameter"
    Private Function PrepareUpdateCommand(ByRef br_htblParam As Hashtable, ByVal commandText As String, ByVal commandtype As CommandType) As DbCommand
        If pvt_Command Is Nothing Then
            'open connection
            OpenConnection()

            'create the command object and initialize the command object
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            pvt_Command.Connection = pvt_dbConn
            pvt_Command.CommandText = commandText
            pvt_Command.CommandType = commandtype
            pvt_Command.CommandTimeout = pvt_Settings.CommandTimeOut

            If Not br_htblParam Is Nothing Then
                If br_htblParam.Count > 0 Then
                    AttachParameterandValues(br_htblParam)
                End If
            End If

            'AttachParameterandFields(br_dtInsert)
        End If
        Return pvt_Command
    End Function
    Private Function PrepareUpdateCommand(ByRef br_htblParam As Hashtable, ByVal commandText As String, ByVal commandtype As CommandType, ByRef br_ObjTrans As Transactions) As DbCommand
        If pvt_Command Is Nothing Then
            'create the command object and initialize the command object
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            pvt_Command.Connection = br_ObjTrans.Connection
            pvt_Command.Transaction = br_ObjTrans.Transaction
            pvt_Command.CommandText = commandText
            pvt_Command.CommandType = commandtype
            pvt_Command.CommandTimeout = pvt_Settings.CommandTimeOut

            If Not br_htblParam Is Nothing Then
                If br_htblParam.Count > 0 Then
                    AttachParameterandValues(br_htblParam)
                End If
            End If
            'AttachParameterandFields(br_dtInsert)
        End If
        Return pvt_Command
    End Function

#End Region

#Region "Attach Parameter values"
    Private Sub AttachParameterandValues(ByRef br_dtParm As DataTable)
        Dim param As DbParameter
        If Not pvt_Command Is Nothing Then
            For Each dc As DataColumn In br_dtParm.Columns
                param = pvt_ObjdbFactory.CreateParameter
                param.ParameterName = dc.ColumnName
                With pvt_Command.Parameters
                    .Add(param)
                    ' Define the parameter mappings from the data table in the
                    .Item(dc.ColumnName).Value = br_dtParm.Rows(0).Item(dc.ColumnName)
                End With
            Next
        End If
    End Sub
    Private Sub AttachParameterandValues(ByRef br_dtParm As DataRow)
        Dim param As DbParameter
        If Not pvt_Command Is Nothing Then
            For Each dc As DataColumn In br_dtParm.Table.Columns
                param = pvt_ObjdbFactory.CreateParameter
                param.ParameterName = dc.ColumnName
                With pvt_Command.Parameters
                    .Add(param)
                    ' Define the parameter mappings from the data table in the
                    .Item(dc.ColumnName).Value = br_dtParm.Item(dc.ColumnName)
                End With
            Next
        End If
    End Sub
    Private Sub AttachParameterandValues(ByRef br_dtParm As DataRow, ByVal bv_HashSelectedColumns As Hashtable)
        Dim param As DbParameter
        'pvt_Command = pvt_ObjdbFactory.CreateCommand
        If Not pvt_Command Is Nothing Then
            For Each dc As DataColumn In br_dtParm.Table.Columns
                If bv_HashSelectedColumns.Item(dc.ColumnName) = dc.ColumnName Then
                    param = pvt_ObjdbFactory.CreateParameter
                    param.ParameterName = dc.ColumnName
                    With pvt_Command.Parameters
                        .Add(param)
                        ' Define the parameter mappings from the data table in the
                        .Item(dc.ColumnName).Value = br_dtParm.Item(dc.ColumnName)
                    End With
                End If
            Next
        End If
    End Sub
    Private Sub AttachParameterandValues(ByVal bv_ParmName As String, ByVal bv_ParamValue As String)
        Dim param As DbParameter
        If Not pvt_Command Is Nothing Then
            param = pvt_ObjdbFactory.CreateParameter
            param.ParameterName = bv_ParmName
            With pvt_Command.Parameters
                .Add(param)
                ' Define the parameter mappings from the data table in the
                .Item(bv_ParmName).Value = bv_ParamValue
            End With
        End If
    End Sub
    Private Sub AttachParameterandValues(ByRef br_htblParm As Hashtable)
        Dim param As DbParameter
        Dim I As Integer
        Dim kys As ICollection

        If Not pvt_Command Is Nothing Then
            kys = br_htblParm.Keys
            For Each ky As Object In kys
                param = pvt_ObjdbFactory.CreateParameter
                param.ParameterName = ky.ToString
                With pvt_Command.Parameters
                    .Add(param)
                    ' Define the parameter mappings from the data table in the
                    .Item(ky.ToString).Value = br_htblParm.Item(ky.ToString)
                End With
            Next
        End If
    End Sub
#End Region

#Region "Attach Parameter Fields"
    Private Sub AttachParameterandFields(ByRef br_dtParm As DataTable)
        Dim param As DbParameter
        If Not pvt_Command Is Nothing Then
            For Each dc As DataColumn In br_dtParm.Columns
                param = pvt_ObjdbFactory.CreateParameter
                param.ParameterName = dc.ColumnName
                With pvt_Command.Parameters
                    .Add(param)
                    ' Define the parameter mappings from the data table in the
                    .Item(dc.ColumnName).SourceColumn = dc.ColumnName
                End With
            Next
        End If
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

        If Not pvt_DA Is Nothing Then
            If Not pvt_DA.SelectCommand Is Nothing Then
                If Not pvt_DA.SelectCommand.Connection Is Nothing Then
                    pvt_DA.SelectCommand.Connection.Dispose()
                End If
                pvt_DA.SelectCommand.Dispose()
            End If
            pvt_DA.Dispose()
            pvt_DA = Nothing
        End If
    End Sub
#End Region

#Region "Log Queries"

    Private Sub LogQuery(ByVal Query As String, ByVal Method As String, ByVal Table As String)
        If pvt_Settings.QueryCacheEnabled Then
            iErrorHandler.SaveQueryToLog(Method, Table, Query)
        End If
    End Sub

#End Region

#Region "Bulk Update"
    Public Sub BatchUpdate(ByVal dt As Data.DataTable, ByVal strQuery As String, ByRef br_objtrans As Transactions)
        If pvt_Command Is Nothing Then
            pvt_Command = pvt_ObjdbFactory.CreateCommand
            With pvt_Command
                Try
                    Dim str As String() = New String(dt.Columns.Count - 1) {}
                    Dim index As Integer = 0
                    For Each dc As DataColumn In dt.Columns
                        str(index) = Convert.ToString(dc.ColumnName)
                        index += 1
                    Next
                    For i As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To str.Length - 1
                            AttachParameterandValues(str(j), dt.Rows(i).Item(str(j)), pvt_Command)
                        Next
                        ExecuteNonQuery(strQuery, br_objtrans)
                        .Parameters.Clear()
                    Next
                Catch ex As Exception
                    Throw ex
                Finally
                    If Not pvt_Command Is Nothing Then
                        .Dispose()
                    End If
                End Try

            End With
        End If
    End Sub

    Private Sub AttachParameterandValues(ByVal bv_ParmName As String, ByVal bv_ParamValue As Object, ByRef cmd As DbCommand)
        Try
            Dim param As DbParameter
            param = pvt_ObjdbFactory.CreateParameter
            With param
                If Not cmd Is Nothing Then
                    .Direction = ParameterDirection.Input
                    .ParameterName = bv_ParmName
                    If IsDBNull(bv_ParamValue) Then
                        .Value = DBNull.Value
                    Else
                        .Value = bv_ParamValue
                    End If
                    cmd.Parameters.Add(param)
                End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetInsertQuery(ByVal bv_strsync_tbl_nam As String) As String
        Try
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            pvt_DA.SelectCommand = PrepareLoadCommand(String.Concat("SELECT * FROM ", bv_strsync_tbl_nam))
            pvt_CMBld = pvt_ObjdbFactory.CreateCommandBuilder
            With pvt_CMBld
                .DataAdapter = pvt_DA
                Return .GetInsertCommand(True).CommandText
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUpdateQuery(ByVal bv_strtbl_nam As String) As String
        Try
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            pvt_DA.SelectCommand = PrepareLoadCommand(String.Concat("SELECT * from ", bv_strtbl_nam))
            pvt_CMBld = pvt_ObjdbFactory.CreateCommandBuilder
            With pvt_CMBld
                .DataAdapter = pvt_DA
                Dim updatecmd As String = .GetUpdateCommand(True).CommandText
                updatecmd = updatecmd.Substring(0, updatecmd.IndexOf("WHERE"))
                Return updatecmd
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUpdateQueryWithWC(ByVal bv_strtbl_nam As String) As String
        Try
            pvt_DA = pvt_ObjdbFactory.CreateDataAdapter
            pvt_DA.SelectCommand = PrepareLoadCommand(String.Concat("SELECT * from ", bv_strtbl_nam))
            pvt_CMBld = pvt_ObjdbFactory.CreateCommandBuilder
            With pvt_CMBld
                .DataAdapter = pvt_DA
                .ConflictOption = ConflictOption.OverwriteChanges
                Dim updatecmd As String = .GetUpdateCommand(True).CommandText
                updatecmd = updatecmd.Replace("Original_", String.Empty)
                Return updatecmd
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Create Table"
    Public Function CreateTable(ByVal _tableName As String, ByVal schema As DataTable, ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim sql As String = GetCreateSQL(_tableName, schema)
            ExecuteNonQuery(sql, br_objTrans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CreateTable(ByVal _tableName As String, ByVal schema As DataTable) As Boolean
        Try
            Dim sql As String = GetCreateSQL(_tableName, schema)
            ExecuteNonQuery(sql)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCreateSQL(ByVal tableName As String, ByVal schema As DataTable) As String
        Try
            Dim sql As String = "CREATE TABLE [" & tableName & "] (" & vbLf
            ' columns
            For Each column As DataRow In schema.Rows
                sql += vbTab & "[" & column("ColumnName").ToString() & "] " & SQLGetType(column)
                If schema.Columns.Contains("AllowDBNull") AndAlso CBool(column("AllowDBNull")) = False Then
                    sql += " NOT NULL"
                End If
                sql += "," & vbLf
            Next
            sql = sql.TrimEnd(New Char() {","c, ControlChars.Lf}) & vbLf
            Dim pk As String = ", CONSTRAINT PK_" & tableName & " PRIMARY KEY CLUSTERED ("
            Dim fk As String = ""
            Dim hasKeys As Boolean
            Dim hasFk As Boolean
            ' check schema for primary keys
            Dim pkeys As String = String.Join(", ", GetPrimaryKeys(schema))
            pk += pkeys
            hasKeys = pkeys.Length > 0
            ' check schema for Foregin keys
            Dim fkeys As String() = GetForeginKeys(schema)
            hasFk = fkeys.Length > 0
            If hasFk Then
                GetForeginKeys(fkeys, fk, tableName, schema)
            End If
            pk = pk.TrimEnd(New Char() {","c, " "c, ControlChars.Lf}) & ")" & vbLf
            If hasKeys Then
                sql += pk
            End If
            If hasFk Then
                sql += fk
            End If
            sql += ")"
            Return sql
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetForeginKeys(ByVal fkeys As String(), ByRef fk As String, ByVal tableName As String, ByVal Schema As DataTable)
        Try
            For Each _key In fkeys
                fk += "CONSTRAINT Fk_" & tableName & "_" & _key & " FOREIGN KEY(" & _key & ") REFERENCES "
                For Each column As DataRow In Schema.Rows
                    If column.Item("ColumnName") = _key Then
                        fk += column.Item("FkTable").ToString & "(" & column.Item("FkTableColumn").ToString & ")" & vbLf
                    End If
                Next
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetPrimaryKeys(ByVal schema As DataTable) As String()
        Try
            Dim keys As New List(Of String)()

            For Each column As DataRow In schema.Rows
                If schema.Columns.Contains("IsPK") AndAlso CBool(column("IsPK")) Then
                    keys.Add(column("ColumnName").ToString())
                End If
            Next
            Return keys.ToArray()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetForeginKeys(ByVal schema As DataTable) As String()
        Try
            Dim keys As New List(Of String)()
            For Each column As DataRow In schema.Rows
                If schema.Columns.Contains("IsFK") AndAlso CBool(column("IsFK")) Then
                    keys.Add(column("ColumnName").ToString())
                End If
            Next
            Return keys.ToArray()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function SQLGetType(ByVal type As Object, ByVal columnSize As Integer, ByVal numericPrecision As Integer, ByVal numericScale As Integer) As String
        Try
            Select Case type.ToString()
                Case "System.Byte[]"
                    Return "VARBINARY(MAX)"
                Case "System.Boolean"
                    Return "BIT"
                Case "System.DateTime"
                    Return "DATETIME"
                Case "System.DateTimeOffset"
                    Return "DATETIMEOFFSET"
                Case "System.Decimal"
                    If numericPrecision <> -1 AndAlso numericScale <> -1 Then
                        Return String.Concat("DECIMAL(", numericPrecision, ",", numericScale, ")")
                    Else
                        Return "DECIMAL"
                    End If
                Case "System.Double"
                    Return "FLOAT"
                Case "System.Single"
                    Return "REAL"
                Case "System.Int64"
                    Return "BIGINT"
                Case "System.Int32"
                    Return "INT"
                Case "System.Int16"
                    Return "SMALLINT"
                Case "System.String"
                    Return String.Concat("VARCHAR(", (If((columnSize = -1 OrElse columnSize > 8000), " MAX ", columnSize.ToString())), ")")
                Case "System.Byte"
                    Return "TINYINT"
                Case "System.Guid"
                    Return "UNIQUEIDENTIFIER"
                Case Else
                    Throw New Exception(String.Concat(type.ToString(), " not implemented."))
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function SQLGetType(ByVal schemaRow As DataRow) As String
        Try
            Dim numericPrecision As Integer
            Dim numericScale As Integer
            If Not Integer.TryParse(schemaRow("NumericPrecision").ToString(), numericPrecision) Then
                numericPrecision = -1
            End If
            If Not Integer.TryParse(schemaRow("NumericScale").ToString(), numericScale) Then
                numericScale = -1
            End If
            Return SQLGetType(schemaRow("DataType"), Integer.Parse(schemaRow("ColumnSize").ToString()), numericPrecision, numericScale)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AlterAddColumns(ByVal strname As String, ByVal schema As DataTable, ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim sql As String = GetAlterAddSQL(strname, schema)
            ExecuteNonQuery(sql, br_objtrans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAlterAddSQL(ByVal strname As String, ByVal schema As DataTable) As String
        Try
            Dim sql As String = "ALTER TABLE [" & strname & "] ADD " & vbLf
            For Each column As DataRow In schema.Rows
                sql += vbTab & "[" & column("ColumnName").ToString() & "] " & SQLGetType(column)
                If schema.Columns.Contains("AllowDBNull") AndAlso CBool(column("AllowDBNull")) = False Then
                    sql += " NOT NULL"
                End If
                sql += "," & vbLf
            Next
            Dim fk As String = ""
            Dim hasFk As Boolean
            Dim fkeys As String() = GetForeginKeys(schema)
            hasFk = fkeys.Length > 0
            If hasFk Then
                GetForeginKeys(fkeys, fk, strname, schema)
            End If
            If hasFk Then
                sql += fk
            End If
            sql = sql.TrimEnd(New Char() {","c, ControlChars.Lf}) & vbLf
            Return sql
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AlterModifyColumns(ByVal strname As String, ByVal schema As DataTable, ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim sql As String = GetAlterModifySQL(strname, schema)
            ExecuteNonQuery(sql, br_objtrans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAlterModifySQL(ByVal strname As String, ByVal schema As DataTable) As String
        Try
            Dim sql As String = ""
            For Each column As DataRow In schema.Rows
                sql += "ALTER TABLE [" & strname & "] ALTER " & vbLf
                sql += "COLUMN " & vbTab & "[" & column("ColumnName").ToString() & "] " & SQLGetType(column)
                If schema.Columns.Contains("AllowDBNull") AndAlso CBool(column("AllowDBNull")) = False Then
                    sql += " NOT NULL"
                End If
                sql += vbLf
            Next
            sql = sql.TrimEnd(New Char() {","c, ControlChars.Lf}) & vbLf
            Return sql
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AlterDeleteColumns(ByVal strname As String, ByVal schema As DataTable, ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim sql As String = GetAlterDeleteSQL(strname, schema)
            ExecuteNonQuery(sql, br_objtrans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAlterDeleteSQL(ByVal strname As String, ByVal schema As DataTable) As String
        Try
            Dim sql As String = ""
            For Each column As DataRow In schema.Rows
                sql += "ALTER TABLE [" & strname & "] DROP " & vbLf
                sql += "COLUMN " & vbTab & "[" & column("ColumnName").ToString() & "] "
                sql += vbLf
            Next
            sql = sql.TrimEnd(New Char() {","c, ControlChars.Lf}) & vbLf
            Return sql
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DropTable(ByVal strtblname As String, ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim sql As String = GetDropTableSQL(strtblname)
            ExecuteNonQuery(sql, br_objtrans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function TruncateTable(ByVal strtblname As String, ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim sql As String = GetAlterTruncateTableSQL(strtblname)
            ExecuteNonQuery(sql, br_objtrans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetDropTableSQL(ByVal strtblname As String) As String
        Dim sql As String = "DROP TABLE " & strtblname
        Return sql
    End Function

    Public Function GetAlterTruncateTableSQL(ByVal strtblname As String) As String
        Dim sql As String = "TRUNCATE TABLE " & strtblname
        Return sql
    End Function
#End Region


End Class
