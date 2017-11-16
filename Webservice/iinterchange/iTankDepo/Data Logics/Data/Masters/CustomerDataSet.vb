Partial Class CustomerDataSet
    Partial Class CUSTOMER_EMAIL_SETTINGDataTable

        Private Sub CUSTOMER_EMAIL_SETTINGDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging

        End Sub

    End Class

    Partial Class V_CUSTOMER_EDI_SETTINGDataTable


    End Class

    Partial Class CUSTOMER_EDI_SETTINGDataTable

        Private Sub CUSTOMER_EDI_SETTINGDataTable_CUSTOMER_EDI_SETTINGRowChanging(sender As System.Object, e As CUSTOMER_EDI_SETTINGRowChangeEvent) Handles Me.CUSTOMER_EDI_SETTINGRowChanging

        End Sub

    End Class

    Partial Class CUSTOMERDataTable

        Private Sub CUSTOMERDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.LBR_RT_PR_HR_NCColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

        Private Sub CUSTOMERDataTable_CUSTOMERRowChanging(sender As System.Object, e As CUSTOMERRowChangeEvent) Handles Me.CUSTOMERRowChanging

        End Sub

    End Class

    Partial Class V_CUSTOMERDataTable

        Private Sub V_CUSTOMERDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.LBR_RT_PR_HR_NCColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
