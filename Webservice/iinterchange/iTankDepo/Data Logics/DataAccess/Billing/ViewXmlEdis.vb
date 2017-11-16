#Region " ViewXmlEdis.vb"
'*********************************************************************************************************************
'Name :
'      ViewXmlEdis.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(ViewXmlEdis.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      12/01/2015 18:29:54
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

#Region "ViewXmlEdis"

Public Class ViewXmlEdis
#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const INVOICE_EDI_HISTORYStausSelectQuery As String = "SELECT INVC_EDI_HSTRY_ID,CSTMR_ID,CSTMR_CD,ACTVTY_NAM,INVC_NO,SNT_FL_NAM,SNT_DT,RCVD_FL_NAM,GNRTD_DT,STTS,RMRKS_VC,RSND_BT,GNRTD_BY,ERRR_DSCRPTN FROM INVOICE_EDI_HISTORY WHERE CSTMR_ID=@CSTMR_ID AND ACTVTY_NAM=@ACTVTY_NAM AND STTS=@STTS AND CONVERT(DATE,GNRTD_DT)>=@FRM_DT AND CONVERT(DATE,GNRTD_DT)<=@TO_DT order by GNRTD_DT DESC"
    Private Const INVOICE_EDI_HISTORY_DETAILSelectQuery As String = "SELECT INVC_EDI_HSTRY_DTL_ID,INVC_EDI_HSTRY_ID,EQPMNT_NO,INVC_NO,LN_RSPNS_VC,SPPRT_URL FROM INVOICE_EDI_HISTORY_DETAIL WHERE INVC_EDI_HSTRY_ID=@INVC_EDI_HSTRY_ID"
    Private Const XMLStatusUpdateQuery As String = "UPDATE INVOICE_EDI_HISTORY SET STTS=@STTS,SNT_DT=@SNT_DT,ERRR_DSCRPTN=@ERRR_DSCRPTN WHERE SNT_FL_NAM=@SNT_FL_NAM"
    Private Const LineResponseUpdateQuery As String = "UPDATE INVOICE_EDI_HISTORY_DETAIL SET LN_RSPNS_VC=@LN_RSPNS_VC,SPPRT_URL=@SPPRT_URL WHERE INVC_NO=@INVC_NO AND EQPMNT_NO=@EQPMNT_NO"
    Private Const HeaderResponseUpdateQuery As String = "UPDATE INVOICE_EDI_HISTORY SET RCVD_FL_NAM=@RCVD_FL_NAM,RMRKS_VC=@RMRKS_VC,STTS=@STTS,RCVD_DT=@RCVD_DT WHERE INVC_NO=@INVC_NO"
    Private Const CustomerDetailSelectQuery As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,CNTCT_PRSN_NAM,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,RPR_TCH_EML_ID,BLK_EML_FRMT_ID,HYDR_AMNT_NC,PNMTC_AMNT_NC,LBR_RT_PR_HR_NC,LK_TST_RT_NC,SRVY_ONHR_OFFHR_RT_NC,PRDC_TST_TYP_ID,VLDTY_PRD_TST_YRS,MIN_HTNG_RT_NC,MIN_HTNG_PRD_NC,HRLY_CHRG_NC,CHK_DGT_VLDTN_BT,TRNSPRTTN_BT,RNTL_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,XML_BT,FTP_SRVR_URL,FTP_USR_NAM,FTP_PSSWRD FROM CUSTOMER WHERE XML_BT=1"
    Private Const PDFDetailSelectQuery As String = "SELECT INVC_EDI_HSTRY_ID,CSTMR_ID,CSTMR_CD,ACTVTY_NAM,INVC_NO,SNT_FL_NAM,SNT_DT,RCVD_FL_NAM,GNRTD_DT,STTS,RMRKS_VC,RSND_BT,GNRTD_BY,DPT_ID,ERRR_DSCRPTN,INVC_FL_NAM,FL_SNT_DT FROM INVOICE_EDI_HISTORY WHERE FL_SNT_DT IS NULL AND CSTMR_ID=@CSTMR_ID"
    Private Const PDFStatusUpdateQuery As String = "UPDATE INVOICE_EDI_HISTORY SET FL_SNT_DT=@FL_SNT_DT WHERE INVC_FL_NAM=@INVC_FL_NAM"
    Private Const InvoiceStatusUpdateQuery As String = "UPDATE INVOICE_EDI_HISTORY SET STTS=@STTS WHERE INVC_NO=@INVC_NO"
    Private Const INVOICE_EDI_HISTORYSelectQuery As String = "SELECT INVC_EDI_HSTRY_ID,CSTMR_ID,CSTMR_CD,ACTVTY_NAM,INVC_NO,SNT_FL_NAM,SNT_DT,RCVD_FL_NAM,GNRTD_DT,STTS,RMRKS_VC,RSND_BT,GNRTD_BY,ERRR_DSCRPTN FROM INVOICE_EDI_HISTORY WHERE CSTMR_ID=@CSTMR_ID AND ACTVTY_NAM=@ACTVTY_NAM AND CONVERT(DATE,GNRTD_DT)>=@FRM_DT AND CONVERT(DATE,GNRTD_DT)<=@TO_DT order by GNRTD_DT DESC"
    Private Const DepotDetailSelectQuery As String = "select DPT_ID,DPT_CD,DPT_NAM,CNTCT_PRSN_NAM,ADDRSS_LN1_VC,ADDRSS_LN2_VC,ADDRSS_LN3_VC,VT_NO,EML_ID,PHN_NO,FX_NO,CMPNY_LG_PTH,MDFD_BY,MDFD_DT FROM DEPOT where DPT_ID=@DPT_ID "
    Private Const XMLDetailSelectQuery As String = "SELECT INVC_EDI_HSTRY_ID,CSTMR_ID,CSTMR_CD,ACTVTY_NAM,INVC_NO,SNT_FL_NAM,SNT_DT,RCVD_FL_NAM,GNRTD_DT,STTS,RMRKS_VC,RSND_BT,GNRTD_BY,DPT_ID,ERRR_DSCRPTN,INVC_FL_NAM,FL_SNT_DT FROM INVOICE_EDI_HISTORY WHERE SNT_DT IS NULL AND CSTMR_ID=@CSTMR_ID"
    Private ds As ViewXmlEdiDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New ViewXmlEdiDataSet
    End Sub

#End Region

#Region "GetVInvoiceHistoryByByDepotID() TABLE NAME:V_INVOICE_HISTORY"

    Public Function GetInvoiceEDIHistoryByByCustomerID(ByVal bv_i32CustomerId As Int64, ByVal bv_datPeriodFrom As DateTime, ByVal bv_datPeriodTo As DateTime, ByVal bv_intDepotId As Integer, ByVal bv_strActivityname As String, ByVal bv_strStatus As String) As ViewXmlEdiDataSet
        Try
            Dim hshTbl As New Hashtable
            hshTbl.Add(ViewXmlEdiData.CSTMR_ID, bv_i32CustomerId)
            hshTbl.Add(ViewXmlEdiData.ACTVTY_NAM, bv_strActivityname)
            hshTbl.Add(ViewXmlEdiData.STTS, bv_strStatus)
            hshTbl.Add(ViewXmlEdiData.FRM_DT, bv_datPeriodFrom)
            hshTbl.Add(ViewXmlEdiData.TO_DT, bv_datPeriodTo)
            If bv_strStatus <> String.Empty Then
                objData = New DataObjects(INVOICE_EDI_HISTORYStausSelectQuery, hshTbl)
            Else
                objData = New DataObjects(INVOICE_EDI_HISTORYSelectQuery, hshTbl)
            End If

            objData.Fill(CType(ds, DataSet), ViewXmlEdiData._INVOICE_EDI_HISTORY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetInvoiceXmlEDIDetail(ByVal bv_intEdiId As Int64) As DataTable
        Try
            Dim dtDetail As New DataTable
            Dim hshTbl As New Hashtable
            hshTbl.Add(ViewXmlEdiData.INVC_EDI_HSTRY_ID, bv_intEdiId)
            objData = New DataObjects(INVOICE_EDI_HISTORY_DETAILSelectQuery, hshTbl)
            objData.Fill(dtDetail)
            Return dtDetail
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVInvoiceHistoryByByDepotID() TABLE NAME:V_INVOICE_HISTORY"

    Public Function UpdateXmlStatus(ByVal bv_strFilename As String, ByVal bv_strStatus As String, ByVal bv_strError As String, ByVal bv_datSentDate As DateTime, ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ViewXmlEdiData._INVOICE_EDI_HISTORY).NewRow()
            With dr
                .Item(ViewXmlEdiData.SNT_FL_NAM) = bv_strFilename
                If bv_datSentDate <> Nothing Then
                    .Item(ViewXmlEdiData.SNT_DT) = bv_datSentDate
                Else
                    .Item(ViewXmlEdiData.SNT_DT) = DBNull.Value
                End If

                .Item(ViewXmlEdiData.STTS) = bv_strStatus
                .Item(ViewXmlEdiData.ERRR_DSCRPTN) = bv_strError
            End With
            UpdateXmlStatus = objData.UpdateRow(dr, XMLStatusUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVInvoiceHistoryByByDepotID() TABLE NAME:V_INVOICE_HISTORY"

    Public Function UpdateLineResponse(ByVal bv_strInvoiceNo As String, ByVal bv_strMoveNo As String, ByVal bv_strSupportUrl As String, ByVal bv_strLineResponse As String, ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ViewXmlEdiData._INVOICE_EDI_HISTORY_DETAIL).NewRow()
            With dr
                .Item(ViewXmlEdiData.LN_RSPNS_VC) = bv_strLineResponse
                .Item(ViewXmlEdiData.INVC_NO) = bv_strInvoiceNo
                .Item(ViewXmlEdiData.EQPMNT_NO) = bv_strMoveNo
                .Item(ViewXmlEdiData.SPPRT_URL) = bv_strSupportUrl
            End With
            UpdateLineResponse = objData.UpdateRow(dr, LineResponseUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateHeaderResponse"
    Public Function UpdateHeaderResponse(ByVal bv_strIFileName As String, ByVal bv_strInvoiceNo As String, ByVal bv_strHeaderResponse As String, ByVal bv_strStatus As String, ByVal bv_datReceiveddate As DateTime, ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ViewXmlEdiData._INVOICE_EDI_HISTORY).NewRow()
            With dr
                .Item(ViewXmlEdiData.RCVD_FL_NAM) = bv_strIFileName
                .Item(ViewXmlEdiData.RMRKS_VC) = bv_strHeaderResponse
                .Item(ViewXmlEdiData.INVC_NO) = bv_strInvoiceNo
                .Item(ViewXmlEdiData.STTS) = bv_strStatus
                .Item(ViewXmlEdiData.RCVD_DT) = bv_datReceiveddate
            End With
            UpdateHeaderResponse = objData.UpdateRow(dr, HeaderResponseUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCustomerFtpDetails"
    Public Function GetCustomerFtpDetails() As DataTable
        Try
            Dim dtCustomer As New DataTable
            objData = New DataObjects(CustomerDetailSelectQuery)
            objData.Fill(dtCustomer)
            Return dtCustomer
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetPdfDetails"
    Public Function GetPdfDetails(ByVal bv_intCustomerId As Integer) As DataTable
        Try
            Dim dtPdf As New DataTable
            objData = New DataObjects(PDFDetailSelectQuery, CustomerData.CSTMR_ID, bv_intCustomerId)
            objData.Fill(dtPdf)
            Return dtPdf
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetVInvoiceHistoryByByDepotID() TABLE NAME:V_INVOICE_HISTORY"

    Public Function UpdatePDFStatus(ByVal bv_strFilename As String, ByVal bv_datSentDate As DateTime, ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ViewXmlEdiData._INVOICE_EDI_HISTORY).NewRow()
            With dr
                .Item(ViewXmlEdiData.INVC_FL_NAM) = bv_strFilename
                .Item(ViewXmlEdiData.FL_SNT_DT) = bv_datSentDate
            End With
            UpdatePDFStatus = objData.UpdateRow(dr, PDFStatusUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVInvoiceHistoryByByDepotID() TABLE NAME:V_INVOICE_HISTORY"

    Public Function UpdateInvoiceStatus(ByVal bv_strInvoiceNo As String, ByVal bv_strStatus As String, ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ViewXmlEdiData._INVOICE_EDI_HISTORY).NewRow()
            With dr
                .Item(ViewXmlEdiData.INVC_NO) = bv_strInvoiceNo
                .Item(ViewXmlEdiData.STTS) = bv_strStatus
            End With
            UpdateInvoiceStatus = objData.UpdateRow(dr, InvoiceStatusUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region



    Public Function GetDepotDetails(ByVal bv_intDepotId As Integer) As DataTable
        Try
            Dim dtDepot As New DataTable
            objData = New DataObjects(DepotDetailSelectQuery, DepotData.DPT_ID, bv_intDepotId)
            objData.Fill(dtDepot)
            Return dtDepot
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#Region "GetPdfDetails"
    Public Function GetXmlDetails(ByVal bv_intCustomerId As Integer) As DataTable
        Try
            Dim dtPdf As New DataTable
            objData = New DataObjects(XMLDetailSelectQuery, CustomerData.CSTMR_ID, bv_intCustomerId)
            objData.Fill(dtPdf)
            Return dtPdf
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
#End Region