Partial Class TariffTemplateDataSet
    Partial Class TARIFFDataTable

        Private Sub TARIFFDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.LCTN_CD_3Column.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class TARIFF_TEMPLATEDataTable

        Private Sub TARIFF_TEMPLATEDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.CSTMR_CDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
