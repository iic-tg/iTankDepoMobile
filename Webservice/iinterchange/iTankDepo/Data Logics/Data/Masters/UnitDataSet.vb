Partial Class UnitDataSet
    Partial Class UNITDataTable

        Private Sub UNITDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.UNT_CDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
