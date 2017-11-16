
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework


Public Class FinanceIntegrations

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const FINANCE_INTEGRATIONSelectQueryPk As String = "SELECT FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC FROM FINANCE_INTEGRATION WHERE FNC_INTGRTN_ID=@FNC_INTGRTN_ID"
    Private Const FINANCE_INTEGRATIONSelectQuery As String = "SELECT FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT FROM FINANCE_INTEGRATION"
    Private Const FINANCE_INTEGRATIONSelectQueryByFNC_INTGRTN_ID As String = "SELECT FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC FROM FINANCE_INTEGRATION WHERE FNC_INTGRTN_ID=@FNC_INTGRTN_ID"
    Private Const FINANCE_INTEGRATIONSelectQueryByINVC_TYP_ID As String = "SELECT FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC FROM FINANCE_INTEGRATION WHERE INVC_TYP_ID=@INVC_TYP_ID"
    Private Const FINANCE_INTEGRATIONSelectQueryByINVC_TYP_CD As String = "SELECT FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC FROM FINANCE_INTEGRATION WHERE INVC_TYP_CD=@INVC_TYP_CD"
    Private Const FINANCE_INTEGRATIONSelectQueryByEQPMNT_TYP_ID As String = "SELECT FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC FROM FINANCE_INTEGRATION WHERE EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private Const FINANCE_INTEGRATIONSelectQueryByEQPMNT_TYP_CD As String = "SELECT FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC FROM FINANCE_INTEGRATION WHERE EQPMNT_TYP_CD=@EQPMNT_TYP_CD"
    Private Const FINANCE_INTEGRATIONSelectQueryByCTGRY_CD As String = "SELECT FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC FROM FINANCE_INTEGRATION WHERE CTGRY_CD=@CTGRY_CD"
    Private Const FINANCE_INTEGRATIONSelectQueryByITEM_CD As String = "SELECT FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC FROM FINANCE_INTEGRATION WHERE ITEM_CD=@ITEM_CD"
    Private Const FINANCE_INTEGRATIONSelectQueryByITEM_DSCRPTN_VC As String = "SELECT FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC FROM FINANCE_INTEGRATION WHERE ITEM_DSCRPTN_VC=@ITEM_DSCRPTN_VC"
    Private Const FINANCE_INTEGRATIONInsertQuery As String = "INSERT INTO FINANCE_INTEGRATION(FNC_INTGRTN_ID,INVC_TYP_ID,INVC_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CTGRY_CD,ITEM_CD,ITEM_DSCRPTN_VC,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT)VALUES(@FNC_INTGRTN_ID,@INVC_TYP_ID,@INVC_TYP_CD,@EQPMNT_TYP_ID,@EQPMNT_TYP_CD,@CTGRY_CD,@ITEM_CD,@ITEM_DSCRPTN_VC,@DPT_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT)"
    Private Const FINANCE_INTEGRATIONUpdateQuery As String = "UPDATE FINANCE_INTEGRATION SET FNC_INTGRTN_ID=@FNC_INTGRTN_ID, INVC_TYP_ID=@INVC_TYP_ID, INVC_TYP_CD=@INVC_TYP_CD, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_TYP_CD=@EQPMNT_TYP_CD, CTGRY_CD=@CTGRY_CD, ITEM_CD=@ITEM_CD, ITEM_DSCRPTN_VC=@ITEM_DSCRPTN_VC,MDFD_BY=@MDFD_BY,MDFD_DT=@MDFD_DT WHERE FNC_INTGRTN_ID=@FNC_INTGRTN_ID"
    Private Const FINANCE_INTEGRATIONDeleteQuery As String = "DELETE FROM FINANCE_INTEGRATION WHERE FNC_INTGRTN_ID=@FNC_INTGRTN_ID"
    Private Const GetCustomer_LedgerId_SelectQry As String = "SELECT LDGR_ID FROM CUSTOMER WHERE ACTV_BT=1 AND CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const GetInvoicingParty_LedgerId_SelectQry As String = "SELECT LDGR_ID FROM INVOICING_PARTY WHERE ACTV_BT=1 AND INVCNG_PRTY_ID=@INVCNG_PRTY_ID AND DPT_ID=@DPT_ID"
    Private Const CheckCustomer_LedgerIdExist_SelectQry = "SELECT COUNT(CSTMR_ID) FROM CUSTOMER WHERE ACTV_BT=1 AND CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND LDGR_ID IS NOT NULL"
    Private Const CheckGetInvoiceParty_LedgerIdExist_SelectQry As String = "SELECT COUNT(INVCNG_PRTY_ID) FROM INVOICING_PARTY WHERE ACTV_BT=1 AND INVCNG_PRTY_ID=@INVCNG_PRTY_ID AND DPT_ID=@DPT_ID AND LDGR_ID IS NOT NULL"
    'Procedure
    Private Const P_SAGE_INVOICESInsertQuery As String = "P_SAGE_INVOICES"
    Private Const P_SAGE_INVOICEDETAILSInsertQuery As String = "P_SAGE_INVOICEDETAILS"
    Private Const P_SAGE_INVOICE_PAYMENT_SCHEDULESInsertQuery As String = "P_SAGE_INVOICE_PAYMENT_SCHEDULES"

    Private ds As FINANCEINTEGRATIONDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New FINANCEINTEGRATIONDataSet
    End Sub

#End Region

#Region "Get Ledger ID Informations"

    Public Function GetCustomer_LedgerId(ByVal bv_DPT_ID As Int32, ByVal bv_CSTMR_ID As Int32, ByRef br_objtrans As Transactions) As String
        Try
            Dim hshParam As New Hashtable
            hshParam.Add(CustomerData.CSTMR_ID, bv_CSTMR_ID)
            hshParam.Add(CustomerData.DPT_ID, bv_DPT_ID)

            objData = New DataObjects(GetCustomer_LedgerId_SelectQry, hshParam)
            Return objData.ExecuteScalar(br_objtrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CheckCustomer_LedgerIdExist(ByVal bv_DPT_ID As Int32, ByVal bv_CSTMR_ID As Int32, ByRef br_objtrans As Transactions) As Int32
        Try
            Dim hshParam As New Hashtable
            hshParam.Add(CustomerData.CSTMR_ID, bv_CSTMR_ID)
            hshParam.Add(CustomerData.DPT_ID, bv_DPT_ID)

            objData = New DataObjects(CheckCustomer_LedgerIdExist_SelectQry, hshParam)
            Return objData.ExecuteScalar(br_objtrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetInvoiceParty_LedgerId(ByVal bv_DPT_ID As Int32, ByVal bv_InvoicePartyId As Int32, ByRef br_objtrans As Transactions) As String
        Try
            Dim hshParam As New Hashtable
            hshParam.Add(CustomerData.DPT_ID, bv_DPT_ID)
            hshParam.Add(InvoicePartyData.INVCNG_PRTY_ID, bv_InvoicePartyId)

            objData = New DataObjects(GetInvoicingParty_LedgerId_SelectQry, hshParam)
            Return objData.ExecuteScalar(br_objtrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CheckGetInvoiceParty_LedgerIdExist(ByVal bv_DPT_ID As Int32, ByVal bv_InvoicePartyId As Int32, ByRef br_objtrans As Transactions) As String
        Try
            Dim hshParam As New Hashtable
            hshParam.Add(CustomerData.DPT_ID, bv_DPT_ID)
            hshParam.Add(InvoicePartyData.INVCNG_PRTY_ID, bv_InvoicePartyId)

            objData = New DataObjects(CheckGetInvoiceParty_LedgerIdExist_SelectQry, hshParam)
            Return objData.ExecuteScalar(br_objtrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region
#Region "GetFINANCE_INTEGRATIONByFNC_INTGRTN_ID() TABLE NAME:FINANCE_INTEGRATION"

    Public Function GetFINANCE_INTEGRATIONByFNC_INTGRTN_ID(ByVal bv_i32FNC_INTGRTN_ID As Int32) As FinanceIntegrationDataSet
        Try
            objData = New DataObjects(FINANCE_INTEGRATIONSelectQueryPk, FinanceIntegrationData.FNC_INTGRTN_ID, CStr(bv_i32FNC_INTGRTN_ID))
            objData.Fill(CType(ds, DataSet), FinanceIntegrationData._FINANCE_INTEGRATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetFINANCE_INTEGRATION() TABLE NAME:FINANCE_INTEGRATION"

    Public Function GetFINANCE_INTEGRATION() As FinanceIntegrationDataSet
        Try
            objData = New DataObjects(FINANCE_INTEGRATIONSelectQuery)
            objData.Fill(CType(ds, DataSet), FinanceIntegrationData._FINANCE_INTEGRATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

   

#End Region

#Region "GetFINANCE_INTEGRATIONByINVC_TYP_CD() TABLE NAME:FINANCE_INTEGRATION"

    Public Function GetFINANCE_INTEGRATIONByINVC_TYP_CD(ByVal bv_strINVC_TYP_CD As String, ByRef br_objtrans As Transactions) As DataTable
        Try
            Dim dt As New DataTable

            objData = New DataObjects(FINANCE_INTEGRATIONSelectQueryByINVC_TYP_CD, FinanceIntegrationData.INVC_TYP_CD, bv_strINVC_TYP_CD)
            objData.Fill(dt, br_objtrans)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetFINANCE_INTEGRATIONByINVC_TYP_CD(ByVal bv_strINVC_TYP_CD As String) As DataTable
        Try
            Dim dt As New DataTable

            objData = New DataObjects(FINANCE_INTEGRATIONSelectQueryByINVC_TYP_CD, FinanceIntegrationData.INVC_TYP_CD, bv_strINVC_TYP_CD)
            objData.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetFINANCE_INTEGRATIONByEQPMNT_TYP_CD() TABLE NAME:FINANCE_INTEGRATION"

    Public Function GetFINANCE_INTEGRATIONByEQPMNT_TYP_CD(ByVal bv_strEQPMNT_TYP_CD As String) As FinanceIntegrationDataSet
        Try
            objData = New DataObjects(FINANCE_INTEGRATIONSelectQueryByEQPMNT_TYP_CD, FinanceIntegrationData.EQPMNT_TYP_CD, bv_strEQPMNT_TYP_CD)
            objData.Fill(CType(ds, DataSet), FinanceIntegrationData._FINANCE_INTEGRATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateFINANCE_INTEGRATION() TABLE NAME:FINANCE_INTEGRATION"

    Public Function CreateFINANCE_INTEGRATION(ByVal bv_i32INVC_TYP_ID As Int32, _
                                              ByVal bv_strINVC_TYP_CD As String, _
                                              ByVal bv_i32EQPMNT_TYP_ID As Int32, _
                                              ByVal bv_strEQPMNT_TYP_CD As String, _
                                              ByVal bv_strCTGRY_CD As String, _
                                              ByVal bv_strITEM_CD As String, _
                                              ByVal bv_strITEM_DSCRPTN_VC As String, _
                                              ByVal bv_Dpt_id As Int32, _
                                              ByVal bv_Created_By As String, _
                                              ByVal bv_Created_Date As DateTime, _
                                              ByRef br_objtrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(FinanceIntegrationData._FINANCE_INTEGRATION).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(FinanceIntegrationData._FINANCE_INTEGRATION, br_objtrans)
                .Item(FinanceIntegrationData.FNC_INTGRTN_ID) = intMax
                If bv_i32INVC_TYP_ID <> 0 Then
                    .Item(FinanceIntegrationData.INVC_TYP_ID) = bv_i32INVC_TYP_ID
                Else
                    .Item(FinanceIntegrationData.INVC_TYP_ID) = DBNull.Value
                End If
                If bv_strINVC_TYP_CD <> Nothing Then
                    .Item(FinanceIntegrationData.INVC_TYP_CD) = bv_strINVC_TYP_CD
                Else
                    .Item(FinanceIntegrationData.INVC_TYP_CD) = DBNull.Value
                End If
                If bv_i32EQPMNT_TYP_ID <> 0 Then
                    .Item(FinanceIntegrationData.EQPMNT_TYP_ID) = bv_i32EQPMNT_TYP_ID
                Else
                    .Item(FinanceIntegrationData.EQPMNT_TYP_ID) = DBNull.Value
                End If
                If bv_strEQPMNT_TYP_CD <> Nothing Then
                    .Item(FinanceIntegrationData.EQPMNT_TYP_CD) = bv_strEQPMNT_TYP_CD
                Else
                    .Item(FinanceIntegrationData.EQPMNT_TYP_CD) = DBNull.Value
                End If
                If bv_strCTGRY_CD <> Nothing Then
                    .Item(FinanceIntegrationData.CTGRY_CD) = bv_strCTGRY_CD
                Else
                    .Item(FinanceIntegrationData.CTGRY_CD) = DBNull.Value
                End If
                If bv_strITEM_CD <> Nothing Then
                    .Item(FinanceIntegrationData.ITEM_CD) = bv_strITEM_CD
                Else
                    .Item(FinanceIntegrationData.ITEM_CD) = DBNull.Value
                End If
                If bv_strITEM_DSCRPTN_VC <> Nothing Then
                    .Item(FinanceIntegrationData.ITEM_DSCRPTN_VC) = bv_strITEM_DSCRPTN_VC
                Else
                    .Item(FinanceIntegrationData.ITEM_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_Dpt_id <> Nothing Then
                    .Item(FinanceIntegrationData.DPT_ID) = bv_Dpt_id
                Else
                    .Item(FinanceIntegrationData.DPT_ID) = DBNull.Value
                End If
                If bv_Created_By <> Nothing Then
                    .Item(FinanceIntegrationData.CRTD_BY) = bv_Created_By
                Else
                    .Item(FinanceIntegrationData.CRTD_BY) = DBNull.Value
                End If
                If bv_Created_Date <> Nothing Then
                    .Item(FinanceIntegrationData.CRTD_DT) = bv_Created_Date
                Else
                    .Item(FinanceIntegrationData.CRTD_DT) = DBNull.Value
                End If
                If bv_Created_By <> Nothing Then
                    .Item(FinanceIntegrationData.MDFD_BY) = bv_Created_By
                Else
                    .Item(FinanceIntegrationData.MDFD_BY) = DBNull.Value
                End If
                If bv_Created_Date <> Nothing Then
                    .Item(FinanceIntegrationData.MDFD_DT) = bv_Created_Date
                Else
                    .Item(FinanceIntegrationData.MDFD_DT) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, FINANCE_INTEGRATIONInsertQuery, br_objtrans)
            dr = Nothing
            CreateFINANCE_INTEGRATION = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateFINANCE_INTEGRATION() TABLE NAME:FINANCE_INTEGRATION"

    Public Function UpdateFINANCE_INTEGRATION(ByVal bv_i32FNC_INTGRTN_ID As Int32, _
                                              ByVal bv_i32INVC_TYP_ID As Int32, _
                                              ByVal bv_strINVC_TYP_CD As String, _
                                              ByVal bv_i32EQPMNT_TYP_ID As Int32, _
                                              ByVal bv_strEQPMNT_TYP_CD As String, _
                                              ByVal bv_strCTGRY_CD As String, _
                                              ByVal bv_strITEM_CD As String, _
                                              ByVal bv_strITEM_DSCRPTN_VC As String, _
                                              ByVal bv_Modified_By As String, _
                                              ByVal bv_Modified_Date As DateTime, _
                                              ByRef br_objtrans As Transactions) As Boolean

        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(FinanceIntegrationData._FINANCE_INTEGRATION).NewRow()
            With dr
                .Item(FinanceIntegrationData.FNC_INTGRTN_ID) = bv_i32FNC_INTGRTN_ID
                If bv_i32INVC_TYP_ID <> 0 Then
                    .Item(FinanceIntegrationData.INVC_TYP_ID) = bv_i32INVC_TYP_ID
                Else
                    .Item(FinanceIntegrationData.INVC_TYP_ID) = DBNull.Value
                End If
                If bv_strINVC_TYP_CD <> Nothing Then
                    .Item(FinanceIntegrationData.INVC_TYP_CD) = bv_strINVC_TYP_CD
                Else
                    .Item(FinanceIntegrationData.INVC_TYP_CD) = DBNull.Value
                End If
                If bv_i32EQPMNT_TYP_ID <> 0 Then
                    .Item(FinanceIntegrationData.EQPMNT_TYP_ID) = bv_i32EQPMNT_TYP_ID
                Else
                    .Item(FinanceIntegrationData.EQPMNT_TYP_ID) = DBNull.Value
                End If
                If bv_strEQPMNT_TYP_CD <> Nothing Then
                    .Item(FinanceIntegrationData.EQPMNT_TYP_CD) = bv_strEQPMNT_TYP_CD
                Else
                    .Item(FinanceIntegrationData.EQPMNT_TYP_CD) = DBNull.Value
                End If
                If bv_strCTGRY_CD <> Nothing Then
                    .Item(FinanceIntegrationData.CTGRY_CD) = bv_strCTGRY_CD
                Else
                    .Item(FinanceIntegrationData.CTGRY_CD) = DBNull.Value
                End If
                If bv_strITEM_CD <> Nothing Then
                    .Item(FinanceIntegrationData.ITEM_CD) = bv_strITEM_CD
                Else
                    .Item(FinanceIntegrationData.ITEM_CD) = DBNull.Value
                End If
                If bv_strITEM_DSCRPTN_VC <> Nothing Then
                    .Item(FinanceIntegrationData.ITEM_DSCRPTN_VC) = bv_strITEM_DSCRPTN_VC
                Else
                    .Item(FinanceIntegrationData.ITEM_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_Modified_By <> Nothing Then
                    .Item(FinanceIntegrationData.MDFD_BY) = bv_Modified_By
                Else
                    .Item(FinanceIntegrationData.MDFD_BY) = DBNull.Value
                End If
                If bv_Modified_Date <> Nothing Then
                    .Item(FinanceIntegrationData.MDFD_DT) = bv_Modified_Date
                Else
                    .Item(FinanceIntegrationData.MDFD_DT) = DBNull.Value
                End If
            End With
            UpdateFINANCE_INTEGRATION = objData.UpdateRow(dr, FINANCE_INTEGRATIONUpdateQuery, br_objtrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteFINANCE_INTEGRATION() TABLE NAME:FINANCE_INTEGRATION"

    Public Function DeleteFINANCE_INTEGRATION(ByVal bv_FNC_INTGRTN_ID As Int32, ByRef br_objtrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(FinanceIntegrationData._FINANCE_INTEGRATION).NewRow()
            With dr
                .Item(FinanceIntegrationData.FNC_INTGRTN_ID) = bv_FNC_INTGRTN_ID
            End With
            DeleteFINANCE_INTEGRATION = objData.DeleteRow(dr, FINANCE_INTEGRATIONDeleteQuery, br_objtrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "Procedure Methouds"

    Public Function CreateInvoice_Procedure(ByVal hshParam As Hashtable, ByRef br_objtrans As Transactions) As Long

        Try
            Dim dt As New DataTable
            Dim intMax As Long

            intMax = CommonUIs.GetIdentityValue(FinanceIntegrationData._INVOICES, br_objtrans)

            hshParam.Add("@SEQUENCE", intMax) 'int

            objData = New DataObjects()
            objData.ExecuteProcedure(dt, hshParam, P_SAGE_INVOICESInsertQuery, br_objtrans)
            Return intMax

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CreateInvoiceDetails_Procedure(ByVal hshParam As Hashtable, ByRef br_objtrans As Transactions) As Long

        Try
            objData = New DataObjects()
            Dim dt As New DataTable

            Dim intMax As Long

            intMax = CommonUIs.GetIdentityValue(FinanceIntegrationData._INVOICEDETAILS, br_objtrans)

            hshParam.Add("@SEQUENCE", intMax) 'int

            objData.ExecuteProcedure(dt, hshParam, P_SAGE_INVOICEDETAILSInsertQuery, br_objtrans)

            Return intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CreateInvoicePaymentSchedules_Procedure(ByVal hshParam As Hashtable, ByRef br_objtrans As Transactions) As Boolean

        Try
            'Dim hshParam As New Hashtable
            'hshParam.Add("@SEQUENCE", 1)
            'hshParam.Add("@CNTPAYM", 1)
            'hshParam.Add("@DATEDUE", DateTime.Now.Date)
            'hshParam.Add("@AMTDUE", 100D)
            'hshParam.Add("@INVIMPTD", 1)
            'hshParam.Add("@DATESYS", DateTime.Now.Date)
            Dim dt As New DataTable

            objData = New DataObjects()
            objData.ExecuteProcedure(dt, hshParam, P_SAGE_INVOICE_PAYMENT_SCHEDULESInsertQuery, br_objtrans)
            Return True
            'Dim strQry As String = "INSERT INTO SAGE300INT..INVOICE_PAYMENT_SCHEDULES (SEQUENCE,CNTPAYM,DATEDUE,AMTDUE,INVIMPTD,DATESYS) VALUES(@SEQUENCE,@CNTPAYM,@DATEDUE,@AMTDUE,@INVIMPTD,@DATESYS)"
            'Return objData.InsertRow(dr, strQry, br_objtrans)
            'Return objData.ExecuteNonQuery("", br_objtrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
