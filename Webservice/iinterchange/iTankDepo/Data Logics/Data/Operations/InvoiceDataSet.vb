

Partial Public Class InvoiceDataSet
    Partial Class V_CHARGE_DETAILSDataTable

        Private Sub V_CHARGE_DETAILSDataTable_V_CHARGE_DETAILSRowChanging(sender As System.Object, e As V_CHARGE_DETAILSRowChangeEvent) Handles Me.V_CHARGE_DETAILSRowChanging

        End Sub

    End Class

    Partial Class V_STORAGE_CHARGEDataTable

        Private Sub V_STORAGE_CHARGEDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.YRD_LCTNColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class V_HANDLING_CHARGEDataTable

        Private Sub V_HANDLING_CHARGEDataTable_V_HANDLING_CHARGERowChanging(sender As System.Object, e As V_HANDLING_CHARGERowChangeEvent) Handles Me.V_HANDLING_CHARGERowChanging

        End Sub

        Private Sub V_HANDLING_CHARGEDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.EQPMNT_CD_IDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class V_REPAIR_CHARGEDataTable

        Private Sub V_REPAIR_CHARGEDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.YRD_LCTNColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class V_HANDLING_STORAGE_INVOICEDataTable

        Private Sub V_HANDLING_STORAGE_INVOICEDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.EQPMNT_CD_CDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

        Private Sub V_HANDLING_STORAGE_INVOICEDataTable_V_HANDLING_STORAGE_INVOICERowChanging(sender As System.Object, e As V_HANDLING_STORAGE_INVOICERowChangeEvent) Handles Me.V_HANDLING_STORAGE_INVOICERowChanging

        End Sub

    End Class

    Partial Class CUSTOMERDataTable

        Private Sub CUSTOMERDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.DPT_IDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
