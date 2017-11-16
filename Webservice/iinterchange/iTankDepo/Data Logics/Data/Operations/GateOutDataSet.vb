

Partial Public Class GateOutDataSet
    Partial Class GATEOUTDataTable

        Private Sub GATEOUTDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.YRD_LCTNColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
