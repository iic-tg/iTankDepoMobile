Partial Class UploadDataSet
    Partial Class CURRENCYDataTable

        Private Sub CURRENCYDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.ACTV_BTColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class UPLOAD_KEYSDataTable

        Private Sub UPLOAD_KEYSDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.CLMN_NAMColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class UNITDataTable

        Private Sub UNITDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.UNT_DSCRPTN_VCColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class RESPONSIBILITYDataTable

        Private Sub RESPONSIBILITYDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.RSPNSBLTY_CDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class TARIFFDataTable

        Private Sub TARIFFDataTable_TARIFFRowChanging(sender As System.Object, e As TARIFFRowChangeEvent) Handles Me.TARIFFRowChanging

        End Sub

        Private Sub TARIFFDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.TRFF_CDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class EQUIPMENT_STATUSDataTable

        Private Sub EQUIPMENT_STATUSDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.EQPMNT_STTS_CDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class COMPONENTDataTable

        Private Sub COMPONENTDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.CRTD_BYColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class PARTYDataTable

        Private Sub PARTYDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.PRTY_CDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class COUNTRYDataTable

        Private Sub COUNTRYDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.CNTRY_DSCRPTN_VCColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class COMPANIONDataTable

        Private Sub COMPANIONDataTable_COMPANIONRowChanging(sender As System.Object, e As COMPANIONRowChangeEvent) Handles Me.COMPANIONRowChanging

        End Sub

        Private Sub COMPANIONDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.CMPNN_CDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class UPLOAD_SCHEMA_DETAILDataTable

        Private Sub UPLOAD_SCHEMA_DETAILDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.CHRCTR_MX_LNGTHColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

        Private Sub UPLOAD_SCHEMA_DETAILDataTable_UPLOAD_SCHEMA_DETAILRowChanging(sender As System.Object, e As UPLOAD_SCHEMA_DETAILRowChangeEvent) Handles Me.UPLOAD_SCHEMA_DETAILRowChanging

        End Sub

    End Class

End Class
