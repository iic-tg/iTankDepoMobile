Partial Class GateinDataSet

    Partial Class V_ACTIVITY_STATUSDataTable

        Private Sub V_ACTIVITY_STATUSDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.DPT_CDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
