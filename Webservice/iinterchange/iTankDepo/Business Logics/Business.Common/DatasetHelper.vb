#Region "DatasetHelper..."
Public Class DatasetHelper
    Public ds As DataSet
    Private m_FieldInfo As System.Collections.ArrayList
    Private m_FieldList As String
    Private GroupByFieldInfo As System.Collections.ArrayList
    Private GroupByFieldList As String

    Sub New(ByRef DSt As DataSet)
        ds = DSt
    End Sub

    Sub New()
        ds = Nothing
    End Sub

    Private Class FieldInfo
        Public RelationName As String
        Public FieldName As String 'source table field name
        Public FieldAlias As String     'destination table field name
        Public Aggregate As String
    End Class
    Private Sub ParseFieldList(ByVal FieldList As String, ByVal AllowRelation As Boolean)
        If m_FieldList = FieldList Then
            Return
        End If
        m_FieldInfo = New System.Collections.ArrayList
        m_FieldList = FieldList
        Dim Field As FieldInfo
        Dim FieldParts As String()
        Dim Fields As String() = FieldList.Split(","c)
        Dim i As Integer
        i = 0
        i = 0
        For i = 0 To Fields.Length - 1
            Field = New FieldInfo
            FieldParts = Fields(i).Trim.Split(" "c)
            Select Case FieldParts.Length
                Case 1
                    'Exit For
                Case 2
                    Field.FieldAlias = FieldParts(1)
                    'Exit For
                Case Else
                    Throw New Exception("Too many spaces in field definition: '" + Fields(i) + "'.")
            End Select
            FieldParts = FieldParts(0).Split("."c)
            Select Case FieldParts.Length
                Case 1
                    Field.FieldName = FieldParts(0)
                    ' Exit For
                Case 2
                    If AllowRelation = False Then
                        Throw New Exception("Relation specifiers not permitted in field list: '" + Fields(i) + "'.")
                    End If
                    Field.RelationName = FieldParts(0).Trim
                    Field.FieldName = FieldParts(1).Trim
                    'Exit For
                Case Else
                    Throw New Exception("Invalid field definition: " + Fields(i) + "'.")
            End Select
            If Field.FieldAlias Is Nothing Then
                Field.FieldAlias = Field.FieldName
            End If
            m_FieldInfo.Add(Field)
        Next
    End Sub
    Private Sub ParseGroupByFieldList(ByVal FieldList As String)
        If GroupByFieldList = FieldList Then
            Return
        End If
        GroupByFieldInfo = New System.Collections.ArrayList
        Dim Field As FieldInfo
        Dim FieldParts As String()
        Dim Fields As String() = FieldList.Split(","c)
        Dim i As Integer = 0
        For i = 0 To Fields.Length - 1
            Field = New FieldInfo
            FieldParts = Fields(i).Trim.Split(" "c)
            Select Case FieldParts.Length
                Case 1
                    'Exit For
                Case 2
                    Field.FieldAlias = FieldParts(1)
                    'Exit For
                Case Else
                    Throw New ArgumentException("Too many spaces in field definition: '" + Fields(i) + "'.")
            End Select
            FieldParts = FieldParts(0).Split("("c)
            Select Case FieldParts.Length
                Case 1
                    Field.FieldName = FieldParts(0)
                    'Exit For
                Case 2
                    Field.Aggregate = FieldParts(0).Trim.ToLower
                    Field.FieldName = FieldParts(1).Trim(" "c, ")"c)
                    'Exit For
                Case Else
                    Throw New ArgumentException("Invalid field definition: '" + Fields(i) + "'.")
            End Select
            If Field.FieldAlias Is Nothing Then
                If Field.Aggregate Is Nothing Then
                    Field.FieldAlias = Field.FieldName
                Else
                    Field.FieldAlias = Field.Aggregate + "of" + Field.FieldName
                End If
            End If
            GroupByFieldInfo.Add(Field)

        Next
        GroupByFieldList = FieldList
    End Sub
    Public Function CreateGroupByTable(ByVal TableName As String, ByVal SourceTable As DataTable, ByVal FieldList As String) As DataTable
        If FieldList Is Nothing Then
            Throw New ArgumentException("You must specify at least one field in the field list.")
        Else
            Dim dt As DataTable = New DataTable(TableName)
            ParseGroupByFieldList(FieldList)
            For Each Field As FieldInfo In GroupByFieldInfo
                Dim dc As DataColumn = SourceTable.Columns(Field.FieldName)
                If Field.Aggregate Is Nothing Then
                    dt.Columns.Add(Field.FieldAlias, dc.DataType, dc.Expression)
                Else
                    dt.Columns.Add(Field.FieldAlias, dc.DataType)
                End If
            Next
            If Not (ds Is Nothing) Then
                If ds.Tables.Contains(dt.TableName) Then
                    ds.Tables.Remove(dt.TableName)
                End If
                ds.Tables.Add(dt)
            End If
            Return dt
        End If
    End Function
    Public Sub InsertGroupByInto(ByVal DestTable As DataTable, ByVal SourceTable As DataTable, ByVal FieldList As String, ByVal RowFilter As String, ByVal GroupBy As String)
        If FieldList Is Nothing Then
            Throw New ArgumentException("You must specify at least one field in the field list.")
        End If
        ParseGroupByFieldList(FieldList)
        ParseFieldList(GroupBy, False)
        Dim Rows As DataRow() = SourceTable.Select(RowFilter, GroupBy)
        Dim LastSourceRow As DataRow = Nothing
        Dim DestRow As DataRow = Nothing
        Dim SameRow As Boolean
        Dim RowCount As Integer = 0
        For Each SourceRow As DataRow In Rows
            SameRow = False
            If Not (LastSourceRow Is Nothing) Then
                SameRow = True
                For Each Field As FieldInfo In m_FieldInfo
                    If Not ColumnEqual(LastSourceRow(Field.FieldName), SourceRow(Field.FieldName)) Then
                        SameRow = False
                        ' break 
                    End If
                Next
                If Not SameRow Then
                    DestTable.Rows.Add(DestRow)
                End If
            End If
            If Not SameRow Then
                DestRow = DestTable.NewRow
                RowCount = 0
            End If
            RowCount += 1
            For Each Field As FieldInfo In GroupByFieldInfo
                Select Case Field.Aggregate
                    Case Nothing, "", "last"
                        DestRow(Field.FieldAlias) = SourceRow(Field.FieldName)
                        ' break 
                    Case "first"
                        If RowCount = 1 Then
                            DestRow(Field.FieldAlias) = SourceRow(Field.FieldName)
                        End If
                        ' break 
                    Case "count"
                        DestRow(Field.FieldAlias) = RowCount
                        ' break 
                    Case "sum"
                        DestRow(Field.FieldAlias) = Add(DestRow(Field.FieldAlias), SourceRow(Field.FieldName))
                    Case "avg"
                        DestRow(Field.FieldAlias) = Math.Round(CDec(SourceTable.Compute(String.Concat("Avg([", Field.FieldName, "])"), RowFilter)), 0)
                        ' break 
                    Case "max"
                        DestRow(Field.FieldAlias) = Max(DestRow(Field.FieldAlias), SourceRow(Field.FieldName))
                        ' break 
                    Case "min"
                        If RowCount = 1 Then
                            DestRow(Field.FieldAlias) = SourceRow(Field.FieldName)
                        Else
                            DestRow(Field.FieldAlias) = Min(DestRow(Field.FieldAlias), SourceRow(Field.FieldName))
                        End If
                        ' break 
                End Select
            Next
            LastSourceRow = SourceRow
        Next
        If Not (DestRow Is Nothing) Then
            DestTable.Rows.Add(DestRow)
        End If
    End Sub

    Private Function LocateFieldInfoByName(ByVal FieldList As System.Collections.ArrayList, ByVal Name As String) As FieldInfo
        For Each Field As FieldInfo In FieldList
            If Field.FieldName = Name Then
                Return Field
            End If
        Next
        Return Nothing
    End Function

    Private Function ColumnEqual(ByVal a As Object, ByVal b As Object) As Boolean
        If (TypeOf a Is DBNull) AndAlso (TypeOf b Is DBNull) Then
            Return True
        End If
        If (TypeOf a Is DBNull) OrElse (TypeOf b Is DBNull) Then
            Return False
        End If
        Return a.Equals(b)
    End Function

    Private Function Min(ByVal a As Object, ByVal b As Object) As Object
        If (TypeOf a Is DBNull) OrElse (TypeOf b Is DBNull) Then
            Return DBNull.Value
        End If
        If CType(a, IComparable).CompareTo(b) = -1 Then
            Return a
        Else
            Return b
        End If
    End Function

    Private Function Max(ByVal a As Object, ByVal b As Object) As Object
        If TypeOf a Is DBNull Then
            Return b
        End If
        If TypeOf b Is DBNull Then
            Return a
        End If
        If CType(a, IComparable).CompareTo(b) = 1 Then
            Return a
        Else
            Return b
        End If
    End Function

    Private Function Add(ByVal a As Object, ByVal b As Object) As Object
        If TypeOf a Is DBNull Then
            Return b
        End If
        If TypeOf b Is DBNull Then
            Return a
        End If
        Return (CType(a, Decimal) + CType(b, Decimal))
    End Function
    Public Function SelectGroupByInto(ByVal TableName As String, ByVal SourceTable As DataTable, ByVal FieldList As String, ByVal RowFilter As String, ByVal GroupBy As String) As DataTable
        Dim dt As DataTable = CreateGroupByTable(TableName, SourceTable, FieldList)
        InsertGroupByInto(dt, SourceTable, FieldList, RowFilter, GroupBy)
        Return dt
    End Function

    Public Function BuildFilter(ByRef br_datarow As DataRow, ByVal cols As String()) As String
        Dim strBldr As New System.Text.StringBuilder
        Dim strRes As String = String.Empty
        Dim I As Integer
        If cols.Length > 0 Then
            For I = 1 To cols.Length - 1
                strBldr.Append(cols(I))
                strBldr.Append("='")
                strBldr.Append(Replace(br_datarow.Item(cols(I)), "'", "''"))
                strBldr.Append("' and ")
            Next
            strRes = strBldr.ToString.Substring(0, strBldr.ToString.LastIndexOf(" and "))
        End If
        Return strRes
    End Function

#Region "ColToCSVstring"
    ''' <summary>
    ''' This method used to get the column values as CSV string
    ''' </summary>
    ''' <param name="br_dtData">Denotes the data to be converted</param>
    ''' <param name="bv_strColumnName">Denotes column name to be converted</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function ColToCSVstring(ByRef br_dtData As DataTable, ByVal bv_strColumnName As String) As String
        Dim strArray As Array
        Dim strResult As String = ""
        Dim alCol As New ArrayList

        If Not br_dtData Is Nothing Then
            For Each dr As DataRow In br_dtData.Rows
                'alCol.Add(CStr(dr.Item(bv_strColumnName)))
                alCol.Add(dr.Item(bv_strColumnName))
            Next
        End If
        'strArray = alCol.ToArray(GetType(System.String))
        'strResult = String.Concat("'", String.Join("','", strArray), "'")
        strResult = String.Join(",", alCol.ToArray())
        If strResult = "" Then
            strResult = "null"
        End If
        Return strResult
    End Function
#End Region
End Class
#End Region
