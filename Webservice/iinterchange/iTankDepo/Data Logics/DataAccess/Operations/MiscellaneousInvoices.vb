#Region " MiscellaneousInvoices.vb"
'*********************************************************************************************************************
'Name :
'      MiscellaneousInvoices.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(MiscellaneousInvoices.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      16-Nov-2013 11:14:04 AM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "MiscellaneousInvoices"

Public Class MiscellaneousInvoices

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const Miscellaneous_InvoiceSelectQuery As String = "SELECT MSCLLNS_INVC_ID,NO_OF_EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,INVCNG_TO_ID,INVCNG_TO_CD,INVCNG_TO_NAM,SRVC_PRTNR_TYP_CD,ACTVTY_DT,CHRG_DSCRPTN,AMNT_NC,BLLNG_FLG,DPT_ID,DPT_CD,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,CRRNCY_CD,MIS_TYP,MIS_CTGRY FROM V_MISCELLANEOUS_INVOICE WHERE BLLNG_FLG <> 'B' AND DPT_ID=@DPT_ID"
    Private Const Miscellaneous_InvoiceInsertQuery As String = "INSERT INTO MISCELLANEOUS_INVOICE(MSCLLNS_INVC_ID,NO_OF_EQPMNT_NO,EQPMNT_TYP_ID,INVCNG_TO_ID,ACTVTY_DT,CHRG_DSCRPTN,AMNT_NC,BLLNG_FLG,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,MIS_TYP,MIS_CTGRY)VALUES(@MSCLLNS_INVC_ID,@NO_OF_EQPMNT_NO,@EQPMNT_TYP_ID,@INVCNG_TO_ID,@ACTVTY_DT,@CHRG_DSCRPTN,@AMNT_NC,@BLLNG_FLG,@DPT_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@MIS_TYP,@MIS_CTGRY)"
    Private Const Miscellaneous_InvoiceUpdateQuery As String = "UPDATE MISCELLANEOUS_INVOICE SET NO_OF_EQPMNT_NO=@NO_OF_EQPMNT_NO,MIS_TYP=@MIS_TYP, MIS_CTGRY=@MIS_CTGRY, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, INVCNG_TO_ID=@INVCNG_TO_ID, ACTVTY_DT=@ACTVTY_DT, AMNT_NC=@AMNT_NC, BLLNG_FLG=@BLLNG_FLG, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, CHRG_DSCRPTN=@CHRG_DSCRPTN WHERE DPT_ID=@DPT_ID AND MSCLLNS_INVC_ID=@MSCLLNS_INVC_ID"
    Private Const Miscellaneous_InvoiceDeleteQuery As String = "DELETE FROM MISCELLANEOUS_INVOICE WHERE DPT_ID=@DPT_ID AND MSCLLNS_INVC_ID=@MSCLLNS_INVC_ID"
    Private ds As MiscellaneousInvoiceDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New MiscellaneousInvoiceDataSet
    End Sub

#End Region

#Region "GetMiscellaneousInvoice() TABLE NAME:Miscellaneous_Invoice"

    Public Function GetMiscellaneousInvoice(ByVal bv_intDepotID As Integer) As MiscellaneousInvoiceDataSet
        Try
            objData = New DataObjects(Miscellaneous_InvoiceSelectQuery, MiscellaneousInvoiceData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateMiscellaneousInvoice() TABLE NAME:Miscellaneous_Invoice"

    Public Function CreateMiscellaneousInvoice(ByVal bv_strNoofEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeID As Int64, _
        ByVal bv_i64InvoicingToID As Int64, _
        ByVal bv_datActivityDate As DateTime, _
        ByVal bv_strChargeDesc As String, _
        ByVal bv_dblAmount As Double, _
        ByVal bv_strBillingFlag As String, _
        ByVal bv_i32DepotID As Int32, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_StrMisType As String, _
        ByVal bv_StrMisCatragory As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(MiscellaneousInvoiceData._MISCELLANEOUS_INVOICE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(MiscellaneousInvoiceData._MISCELLANEOUS_INVOICE, br_objTrans)
                .Item(MiscellaneousInvoiceData.MSCLLNS_INVC_ID) = intMax
                .Item(MiscellaneousInvoiceData.NO_OF_EQPMNT_NO) = bv_strNoofEquipmentNo
                .Item(MiscellaneousInvoiceData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                .Item(MiscellaneousInvoiceData.INVCNG_TO_ID) = bv_i64InvoicingToID
                If bv_strChargeDesc <> Nothing Then
                    .Item(MiscellaneousInvoiceData.CHRG_DSCRPTN) = bv_strChargeDesc
                Else
                    .Item(MiscellaneousInvoiceData.CHRG_DSCRPTN) = DBNull.Value
                End If
                .Item(MiscellaneousInvoiceData.ACTVTY_DT) = bv_datActivityDate
                .Item(MiscellaneousInvoiceData.AMNT_NC) = bv_dblAmount
                .Item(MiscellaneousInvoiceData.BLLNG_FLG) = bv_strBillingFlag

                'Finance Integration
                If bv_StrMisType <> Nothing Then
                    .Item(MiscellaneousInvoiceData.MIS_TYP) = bv_StrMisType
                Else
                    .Item(MiscellaneousInvoiceData.MIS_TYP) = DBNull.Value
                End If

                If bv_StrMisCatragory <> Nothing Then
                    .Item(MiscellaneousInvoiceData.MIS_CTGRY) = bv_StrMisCatragory
                Else
                    .Item(MiscellaneousInvoiceData.MIS_CTGRY) = DBNull.Value
                End If

                .Item(MiscellaneousInvoiceData.DPT_ID) = bv_i32DepotID
                .Item(MiscellaneousInvoiceData.CRTD_BY) = bv_strCreatedBy
                .Item(MiscellaneousInvoiceData.CRTD_DT) = bv_datCreatedDate
                .Item(MiscellaneousInvoiceData.MDFD_BY) = bv_strModifiedBy
                .Item(MiscellaneousInvoiceData.MDFD_DT) = bv_datModifiedDate
            End With
            objData.InsertRow(dr, Miscellaneous_InvoiceInsertQuery, br_objTrans)
            dr = Nothing
            CreateMiscellaneousInvoice = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateMiscellaneousInvoice() TABLE NAME:Miscellaneous_Invoice"

    Public Function UpdateMiscellaneousInvoice(ByVal bv_i64MiscellaneousInvoiceID As Int64, _
        ByVal bv_strNoofEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeID As Int64, _
        ByVal bv_i64InvoicingToID As Int64, _
        ByVal bv_datActivityDate As DateTime, _
        ByVal bv_strChargeDesc As String, _
        ByVal bv_dblAmount As Double, _
        ByVal bv_strBillingFlag As String, _
        ByVal bv_i32DepotID As Int32, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_StrMisType As String, _
        ByVal bv_StrMisCatragory As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(MiscellaneousInvoiceData._MISCELLANEOUS_INVOICE).NewRow()
            With dr
                .Item(MiscellaneousInvoiceData.MSCLLNS_INVC_ID) = bv_i64MiscellaneousInvoiceID
                .Item(MiscellaneousInvoiceData.NO_OF_EQPMNT_NO) = bv_strNoofEquipmentNo
                .Item(MiscellaneousInvoiceData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                .Item(MiscellaneousInvoiceData.INVCNG_TO_ID) = bv_i64InvoicingToID
                .Item(MiscellaneousInvoiceData.ACTVTY_DT) = bv_datActivityDate
                .Item(MiscellaneousInvoiceData.AMNT_NC) = bv_dblAmount
                .Item(MiscellaneousInvoiceData.BLLNG_FLG) = bv_strBillingFlag
                .Item(MiscellaneousInvoiceData.DPT_ID) = bv_i32DepotID
                .Item(MiscellaneousInvoiceData.MDFD_BY) = bv_strModifiedBy
                .Item(MiscellaneousInvoiceData.MDFD_DT) = bv_datModifiedDate
                If bv_strChargeDesc <> Nothing Then
                    .Item(MiscellaneousInvoiceData.CHRG_DSCRPTN) = bv_strChargeDesc
                Else
                    .Item(MiscellaneousInvoiceData.CHRG_DSCRPTN) = DBNull.Value
                End If

                'Finance Integration
                If bv_StrMisType <> Nothing Then
                    .Item(MiscellaneousInvoiceData.MIS_TYP) = bv_StrMisType
                Else
                    .Item(MiscellaneousInvoiceData.MIS_TYP) = DBNull.Value
                End If

                If bv_StrMisCatragory <> Nothing Then
                    .Item(MiscellaneousInvoiceData.MIS_CTGRY) = bv_StrMisCatragory
                Else
                    .Item(MiscellaneousInvoiceData.MIS_CTGRY) = DBNull.Value
                End If


            End With
            UpdateMiscellaneousInvoice = objData.UpdateRow(dr, Miscellaneous_InvoiceUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteMiscellaneousInvoice() TABLE NAME:Miscellaneous_Invoice"

    Public Function DeleteMiscellaneousInvoice(ByVal bv_strMiscellaneousInvoiceID As Int64, _
                                               ByVal bv_DepotID As Integer, _
                                               ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(MiscellaneousInvoiceData._MISCELLANEOUS_INVOICE).NewRow()
            With dr
                .Item(MiscellaneousInvoiceData.MSCLLNS_INVC_ID) = bv_strMiscellaneousInvoiceID
                .Item(MiscellaneousInvoiceData.DPT_ID) = bv_DepotID
            End With
            DeleteMiscellaneousInvoice = objData.DeleteRow(dr, Miscellaneous_InvoiceDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
