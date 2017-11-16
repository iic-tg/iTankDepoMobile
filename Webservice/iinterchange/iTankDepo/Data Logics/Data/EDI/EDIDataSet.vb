Partial Class EDIDataSet
    Partial Class V_EDI_SETTINGSDataTable

        Private Sub V_EDI_SETTINGSDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.NXT_RN_DT_TMColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
