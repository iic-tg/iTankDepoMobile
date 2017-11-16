Partial Class TransporterDataSet
    Partial Class TRANSPORTER_ROUTE_DETAILDataTable

    End Class

    Partial Class TRANSPORTERDataTable

        Private Sub TRANSPORTERDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.CNTCT_ADDRSSColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
