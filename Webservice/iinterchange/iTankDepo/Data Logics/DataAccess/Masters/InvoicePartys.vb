Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

#Region "InvoicePartys"

Public Class InvoicePartys

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const INVOICING_PARTYSelectQueryPk As String = "SELECT INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,CNTCT_PRSN_NM,CNTCT_JB_TTL,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,RMRKS_VC,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,BS_CRRNCY_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,ACTV_BT FROM INVOICING_PARTY WHERE INVCNG_PRTY_ID=@INVCNG_PRTY_ID"
    Private Const INVOICING_PARTYSelectQuery As String = "SELECT INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,CNTCT_PRSN_NM,CNTCT_JB_TTL,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,RMRKS_VC,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,BS_CRRNCY_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,ACTV_BT FROM INVOICING_PARTY"
    Private Const INVOICING_PARTYSelectQueryByInvoicePartyID As String = "SELECT INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,CNTCT_PRSN_NM,CNTCT_JB_TTL,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,RMRKS_VC,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,BS_CRRNCY_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,ACTV_BT FROM INVOICING_PARTY WHERE INVCNG_PRTY_ID=@INVCNG_PRTY_ID"
    Private Const INVOICING_PARTYSelectQueryByInvoicePartyCode As String = "SELECT INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,CNTCT_PRSN_NM,CNTCT_JB_TTL,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,RMRKS_VC,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,BS_CRRNCY_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,ACTV_BT FROM INVOICING_PARTY WHERE INVCNG_PRTY_CD=@INVCNG_PRTY_CD"
    Private Const INVOICING_PARTYSelectQueryByInvoicePartyName As String = "SELECT INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,CNTCT_PRSN_NM,CNTCT_JB_TTL,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,RMRKS_VC,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,BS_CRRNCY_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,ACTV_BT FROM INVOICING_PARTY WHERE INVCNG_PRTY_NM=@INVCNG_PRTY_NM"
    Private Const INVOICING_PARTYSelectQueryByDepotId As String = "SELECT INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,CNTCT_PRSN_NM,CNTCT_JB_TTL,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,RMRKS_VC,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,BS_CRRNCY_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,ACTV_BT FROM INVOICING_PARTY WHERE DPT_ID=@DPT_ID"
    Private Const INVOICING_PARTYInsertQuery As String = "INSERT INTO INVOICING_PARTY(INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,CNTCT_PRSN_NM,CNTCT_JB_TTL,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,RMRKS_VC,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,BS_CRRNCY_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,ACTV_BT,LDGR_ID)VALUES(@INVCNG_PRTY_ID,@INVCNG_PRTY_CD,@INVCNG_PRTY_NM,@CNTCT_PRSN_NM,@CNTCT_JB_TTL,@CNTCT_ADDRSS,@BLLNG_ADDRSS,@ZP_CD,@RMRKS_VC,@PHN_NO,@FX_NO,@RPRTNG_EML_ID,@INVCNG_EML_ID,@BS_CRRNCY_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@DPT_ID,@ACTV_BT,@LDGR_ID)"
    Private Const INVOICING_PARTYUpdateQuery As String = "UPDATE INVOICING_PARTY SET INVCNG_PRTY_ID=@INVCNG_PRTY_ID, INVCNG_PRTY_CD=@INVCNG_PRTY_CD, INVCNG_PRTY_NM=@INVCNG_PRTY_NM, CNTCT_PRSN_NM=@CNTCT_PRSN_NM, CNTCT_JB_TTL=@CNTCT_JB_TTL, CNTCT_ADDRSS=@CNTCT_ADDRSS,  BLLNG_ADDRSS=@BLLNG_ADDRSS, ZP_CD=@ZP_CD, RMRKS_VC=@RMRKS_VC, PHN_NO=@PHN_NO, FX_NO=@FX_NO, RPRTNG_EML_ID=@RPRTNG_EML_ID, INVCNG_EML_ID=@INVCNG_EML_ID, BS_CRRNCY_ID=@BS_CRRNCY_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, DPT_ID=@DPT_ID, ACTV_BT=@ACTV_BT WHERE INVCNG_PRTY_ID=@INVCNG_PRTY_ID"
    Private Const INVOICING_PARTYDeleteQuery As String = "DELETE FROM INVOICING_PARTY WHERE INVCNG_PRTY_ID=@INVCNG_PRTY_ID"
    Private Const INVOICING_PARTY_With_Ledger_UpdateQuery As String = "UPDATE INVOICING_PARTY SET INVCNG_PRTY_ID=@INVCNG_PRTY_ID, LDGR_ID=@LDGR_ID, INVCNG_PRTY_CD=@INVCNG_PRTY_CD, INVCNG_PRTY_NM=@INVCNG_PRTY_NM, CNTCT_PRSN_NM=@CNTCT_PRSN_NM, CNTCT_JB_TTL=@CNTCT_JB_TTL, CNTCT_ADDRSS=@CNTCT_ADDRSS,  BLLNG_ADDRSS=@BLLNG_ADDRSS, ZP_CD=@ZP_CD, RMRKS_VC=@RMRKS_VC, PHN_NO=@PHN_NO, FX_NO=@FX_NO, RPRTNG_EML_ID=@RPRTNG_EML_ID, INVCNG_EML_ID=@INVCNG_EML_ID, BS_CRRNCY_ID=@BS_CRRNCY_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, DPT_ID=@DPT_ID, ACTV_BT=@ACTV_BT WHERE INVCNG_PRTY_ID=@INVCNG_PRTY_ID"
    Private ds As InvoicePartyDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New InvoicePartyDataSet
    End Sub

#End Region

#Region "GetInvoicePartyByInvoicePartyID() TABLE NAME:INVOICING_PARTY"

    Public Function GetInvoicePartyByInvoicePartyID(ByVal bv_i64InvoicePartyID As Int64) As InvoicePartyDataSet
        Try
            objData = New DataObjects(INVOICING_PARTYSelectQueryPk, InvoicePartyData.INVCNG_PRTY_ID, CStr(bv_i64InvoicePartyID))
            objData.Fill(CType(ds, DataSet), InvoicePartyData._INVOICING_PARTY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetInvoiceParty() TABLE NAME:INVOICING_PARTY"

    Public Function GetInvoiceParty() As InvoicePartyDataSet
        Try
            objData = New DataObjects(INVOICING_PARTYSelectQuery)
            objData.Fill(CType(ds, DataSet), InvoicePartyData._INVOICING_PARTY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetInvoicePartyByDepotId() TABLE NAME:INVOICING_PARTY"

    Public Function GetInvoicePartyByDepotId(ByVal bv_i32DepotId As Int32) As InvoicePartyDataSet
        Try
            objData = New DataObjects(INVOICING_PARTYSelectQueryByDepotId, InvoicePartyData.DPT_ID, CStr(bv_i32DepotId))
            objData.Fill(CType(ds, DataSet), InvoicePartyData._INVOICING_PARTY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateInvoiceParty() TABLE NAME:INVOICING_PARTY"

    Public Function CreateInvoiceParty(ByVal bv_strInvoicePartyCode As String, _
                                       ByVal bv_strInvoicePartyName As String, _
                                       ByVal bv_strContactPersonName As String, _
                                       ByVal bv_strContactJobTitle As String, _
                                       ByVal bv_strContactAddressLine As String, _
                                       ByVal bv_strBillingAddressLine As String, _
                                       ByVal bv_strZipCode As String, _
                                       ByVal bv_strRemarks As String, _
                                       ByVal bv_strPhoneNumber As String, _
                                       ByVal bv_strFaxNumber As String, _
                                       ByVal bv_strReportingEmailID As String, _
                                       ByVal bv_strInvoicingEmailID As String, _
                                       ByVal bv_i64BaseCurrency As Int64, _
                                       ByVal bv_strCreatedBy As String, _
                                       ByVal bv_datCreatedDate As DateTime, _
                                       ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_i32DepotId As Int32, _
                                       ByVal bv_blnActiveBit As Boolean, _
                                       ByVal bv_FinanceIntegrationBit As Boolean, _
                                       ByVal bv_LedgerId As String, _
                                       ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(InvoicePartyData._INVOICING_PARTY).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(InvoicePartyData._SERVICE_PARTNER, br_objTransaction)
                .Item(InvoicePartyData.INVCNG_PRTY_ID) = intMax
                .Item(InvoicePartyData.INVCNG_PRTY_CD) = bv_strInvoicePartyCode
                .Item(InvoicePartyData.INVCNG_PRTY_NM) = bv_strInvoicePartyName
                If bv_strContactPersonName <> Nothing Then
                    .Item(InvoicePartyData.CNTCT_PRSN_NM) = bv_strContactPersonName
                Else
                    .Item(InvoicePartyData.CNTCT_PRSN_NM) = DBNull.Value
                End If
                If bv_strContactJobTitle <> Nothing Then
                    .Item(InvoicePartyData.CNTCT_JB_TTL) = bv_strContactJobTitle
                Else
                    .Item(InvoicePartyData.CNTCT_JB_TTL) = DBNull.Value
                End If
                If bv_strContactAddressLine <> Nothing Then
                    .Item(InvoicePartyData.CNTCT_ADDRSS) = bv_strContactAddressLine
                Else
                    .Item(InvoicePartyData.CNTCT_ADDRSS) = DBNull.Value
                End If

                If bv_strBillingAddressLine <> Nothing Then
                    .Item(InvoicePartyData.BLLNG_ADDRSS) = bv_strBillingAddressLine
                Else
                    .Item(InvoicePartyData.BLLNG_ADDRSS) = DBNull.Value
                End If

                If bv_strZipCode <> Nothing Then
                    .Item(InvoicePartyData.ZP_CD) = bv_strZipCode
                Else
                    .Item(InvoicePartyData.ZP_CD) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(InvoicePartyData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(InvoicePartyData.RMRKS_VC) = DBNull.Value
                End If
                If bv_strPhoneNumber <> Nothing Then
                    .Item(InvoicePartyData.PHN_NO) = bv_strPhoneNumber
                Else
                    .Item(InvoicePartyData.PHN_NO) = DBNull.Value
                End If
                If bv_strFaxNumber <> Nothing Then
                    .Item(InvoicePartyData.FX_NO) = bv_strFaxNumber
                Else
                    .Item(InvoicePartyData.FX_NO) = DBNull.Value
                End If
                If bv_strReportingEmailID <> Nothing Then
                    .Item(InvoicePartyData.RPRTNG_EML_ID) = bv_strReportingEmailID
                Else
                    .Item(InvoicePartyData.RPRTNG_EML_ID) = DBNull.Value
                End If
                If bv_strInvoicingEmailID <> Nothing Then
                    .Item(InvoicePartyData.INVCNG_EML_ID) = bv_strInvoicingEmailID
                Else
                    .Item(InvoicePartyData.INVCNG_EML_ID) = DBNull.Value
                End If
                If bv_i64BaseCurrency <> 0 Then
                    .Item(InvoicePartyData.BS_CRRNCY_ID) = bv_i64BaseCurrency
                Else
                    .Item(InvoicePartyData.BS_CRRNCY_ID) = DBNull.Value
                End If
                If bv_strCreatedBy <> Nothing Then
                    .Item(InvoicePartyData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(InvoicePartyData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(InvoicePartyData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(InvoicePartyData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(InvoicePartyData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(InvoicePartyData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(InvoicePartyData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(InvoicePartyData.MDFD_DT) = DBNull.Value
                End If
                .Item(InvoicePartyData.DPT_ID) = bv_i32DepotId
                .Item(InvoicePartyData.ACTV_BT) = bv_blnActiveBit

                If bv_FinanceIntegrationBit <> Nothing Then
                    .Item(CustomerData.LDGR_ID) = bv_LedgerId
                Else
                    .Item(CustomerData.LDGR_ID) = DBNull.Value
                End If

            End With

            objData.InsertRow(dr, INVOICING_PARTYInsertQuery, br_objTransaction)
            dr = Nothing
            CreateInvoiceParty = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateInvoiceParty() TABLE NAME:INVOICING_PARTY"

    Public Function UpdateInvoiceParty(ByVal bv_i64InvoicePartyID As Int64, _
                                       ByVal bv_strInvoicePartyCode As String, _
                                       ByVal bv_strInvoicePartyName As String, _
                                       ByVal bv_strContactPersonName As String, _
                                       ByVal bv_strContactJobTitle As String, _
                                       ByVal bv_strContactAddressLine As String, _
                                       ByVal bv_strBillingAddressLine As String, _
                                       ByVal bv_strZipCode As String, _
                                       ByVal bv_strRemarks As String, _
                                       ByVal bv_strPhoneNumber As String, _
                                       ByVal bv_strFaxNumber As String, _
                                       ByVal bv_strReportingEmailID As String, _
                                       ByVal bv_strInvoicingEmailID As String, _
                                       ByVal bv_i64BaseCurrency As Int64, _
                                       ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_i32DepotId As Int32, _
                                       ByVal bv_blnActiveBit As Boolean, _
                                       ByVal bv_FinanceIntegrationBit As Boolean, _
                                       ByVal bv_LedgerId As String, _
                                       ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(InvoicePartyData._INVOICING_PARTY).NewRow()
            With dr
                .Item(InvoicePartyData.INVCNG_PRTY_ID) = bv_i64InvoicePartyID
                .Item(InvoicePartyData.INVCNG_PRTY_CD) = bv_strInvoicePartyCode
                .Item(InvoicePartyData.INVCNG_PRTY_NM) = bv_strInvoicePartyName
                If bv_strContactPersonName <> Nothing Then
                    .Item(InvoicePartyData.CNTCT_PRSN_NM) = bv_strContactPersonName
                Else
                    .Item(InvoicePartyData.CNTCT_PRSN_NM) = DBNull.Value
                End If
                If bv_strContactJobTitle <> Nothing Then
                    .Item(InvoicePartyData.CNTCT_JB_TTL) = bv_strContactJobTitle
                Else
                    .Item(InvoicePartyData.CNTCT_JB_TTL) = DBNull.Value
                End If
                If bv_strContactAddressLine <> Nothing Then
                    .Item(InvoicePartyData.CNTCT_ADDRSS) = bv_strContactAddressLine
                Else
                    .Item(InvoicePartyData.CNTCT_ADDRSS) = DBNull.Value
                End If
                If bv_strBillingAddressLine <> Nothing Then
                    .Item(InvoicePartyData.BLLNG_ADDRSS) = bv_strBillingAddressLine
                Else
                    .Item(InvoicePartyData.BLLNG_ADDRSS) = DBNull.Value
                End If
                If bv_strZipCode <> Nothing Then
                    .Item(InvoicePartyData.ZP_CD) = bv_strZipCode
                Else
                    .Item(InvoicePartyData.ZP_CD) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(InvoicePartyData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(InvoicePartyData.RMRKS_VC) = DBNull.Value
                End If
                If bv_strPhoneNumber <> Nothing Then
                    .Item(InvoicePartyData.PHN_NO) = bv_strPhoneNumber
                Else
                    .Item(InvoicePartyData.PHN_NO) = DBNull.Value
                End If
                If bv_strFaxNumber <> Nothing Then
                    .Item(InvoicePartyData.FX_NO) = bv_strFaxNumber
                Else
                    .Item(InvoicePartyData.FX_NO) = DBNull.Value
                End If
                If bv_strReportingEmailID <> Nothing Then
                    .Item(InvoicePartyData.RPRTNG_EML_ID) = bv_strReportingEmailID
                Else
                    .Item(InvoicePartyData.RPRTNG_EML_ID) = DBNull.Value
                End If
                If bv_strInvoicingEmailID <> Nothing Then
                    .Item(InvoicePartyData.INVCNG_EML_ID) = bv_strInvoicingEmailID
                Else
                    .Item(InvoicePartyData.INVCNG_EML_ID) = DBNull.Value
                End If
                If bv_i64BaseCurrency <> 0 Then
                    .Item(InvoicePartyData.BS_CRRNCY_ID) = bv_i64BaseCurrency
                Else
                    .Item(InvoicePartyData.BS_CRRNCY_ID) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(InvoicePartyData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(InvoicePartyData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(InvoicePartyData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(InvoicePartyData.MDFD_DT) = DBNull.Value
                End If
                .Item(InvoicePartyData.DPT_ID) = bv_i32DepotId
                .Item(InvoicePartyData.ACTV_BT) = bv_blnActiveBit

                'Finance Integration
                If bv_FinanceIntegrationBit = True Then
                    .Item(CustomerData.LDGR_ID) = bv_LedgerId
                Else
                    .Item(CustomerData.LDGR_ID) = DBNull.Value
                End If

            End With

            If bv_FinanceIntegrationBit = True Then
                UpdateInvoiceParty = objData.UpdateRow(dr, INVOICING_PARTY_With_Ledger_UpdateQuery, br_objTransaction)
            Else
                UpdateInvoiceParty = objData.UpdateRow(dr, INVOICING_PARTYUpdateQuery, br_objTransaction)
            End If

            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteInvoiceParty() TABLE NAME:INVOICING_PARTY"

    Public Function DeleteInvoiceParty(ByVal bv_blnInvoicePartyID As Int64) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(InvoicePartyData._INVOICING_PARTY).NewRow()
            With dr
                .Item(InvoicePartyData.INVCNG_PRTY_ID) = bv_blnInvoicePartyID
            End With
            DeleteInvoiceParty = objData.DeleteRow(dr, INVOICING_PARTYDeleteQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class

#End Region
