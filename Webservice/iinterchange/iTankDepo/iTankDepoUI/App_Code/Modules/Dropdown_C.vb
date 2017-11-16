Imports Microsoft.VisualBasic
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Web.Services

Public Class Dropdown_C
    'Private ds As DataSet

    Public Function Scalar(ByVal query As String) As Long

        Dim objData As DataObjects
        objData = New DataObjects(query)
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim objTrans As New Transactions

        Return objData.ExecuteScalar()



    End Function

    Public Function connection(ByVal query As String) As DataSet


        Dim objData As DataObjects
        objData = New DataObjects(query)
        Dim ds As New DataSet
        Dim dt As New DataTable

        objData.Fill(dt)

        ds.Tables().Add(dt)

        Return ds

        'Using connection1 As New SqlConnection

        '    Dim sqlCmd As SqlCommand
        '    Dim adapter As SqlDataAdapter
        '    Dim ds As DataSet
        '    connection1.ConnectionString = "Data Source=B-PC\SQLEXPRESS;Initial Catalog=iTankDepoDB;User ID=sa;Password=meta@123"
        '    connection1.Open()
        '    sqlCmd = New SqlCommand(query, connection1)
        '    'Dim adp As SqlDataAdapter = New SqlDataAdapter _
        '    '(query, connection1)
        '    adapter = New SqlDataAdapter
        '    ds = New DataSet
        '    adapter.SelectCommand = sqlCmd
        '    adapter.Fill(ds)


        '    'connection1.Close()
        '    Return ds

        'End Using



        'Dim connetionString As String
        'Dim sqlCnn As SqlConnection
        'Dim sqlCmd As SqlCommand
        'Dim adapter As New SqlDataAdapter
        'Dim ds As New DataSet
        'Dim i As Integer
        'Dim sql As String

        'connetionString = "Data Source=B-PC\SQLEXPRESS;Initial Catalog=iTankDepoDB;User ID=sa;Password=meta@123"
        'sql = query

        'sqlCnn = New SqlConnection(connetionString)
        'Try
        '    sqlCnn.Open()
        '    sqlCmd = New SqlCommand(sql, sqlCnn)
        '    adapter.SelectCommand = sqlCmd
        '    adapter.Fill(ds)

        '    adapter.Dispose()
        '    sqlCmd.Dispose()
        '    sqlCnn.Close()

        'Catch ex As Exception

        'End Try
        'Return ds

    End Function


End Class
