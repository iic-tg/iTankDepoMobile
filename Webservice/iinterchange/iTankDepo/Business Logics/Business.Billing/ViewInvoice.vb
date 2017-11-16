#Region " ViewInvoice.vb"
'*********************************************************************************************************************
'Name :
'      ViewInvoice.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(ViewInvoice.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      02/01/2014 18:29:54
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Business.Common
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
Imports System.Xml
Imports System.IO
Imports System.Configuration

<ServiceContract()> _
Public Class ViewInvoice

#Region "pub_GetVInvoiceHistoryByDepotID() TABLE NAME:V_INVOICE_HISTORY"

    <OperationContract()> _
    Public Function pub_GetVInvoiceHistoryByDepotID(ByVal bv_i32DepotID As Int32) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices

            dsViewInvoiceData = objViewInvoices.GetVInvoiceHistoryByByDepotID(bv_i32DepotID)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetVInvoiceHistoryForAllDepot() TABLE NAME:V_INVOICE_HISTORY"
    ''' <summary>
    ''' This method is used for returning Invoice detail for all Depots - Multi location 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetVInvoiceHistoryForAllDepot() As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices

            dsViewInvoiceData = objViewInvoices.GetVInvoiceHistoryForAllDepot()
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "pub_GetCLEANINGCHARGEByByDepotIDInvoiceNo() TABLE NAME:CLEANING_CHARGE"

    <OperationContract()> _
    Public Function pub_GetCLEANINGCHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                              ByVal bv_i64ServicePartnerID As Int64, _
                                                              ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetCLEANINGCHARGEByByDepotIDInvoiceNo(bv_i32DepotID, _
                                                                                      bv_i64ServicePartnerID, _
                                                                                      bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetHEATINGCHARGEByByDepotIDInvoiceNo() TABLE NAME:HEATING_CHARGE"

    <OperationContract()> _
    Public Function pub_GetHEATINGCHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                             ByVal bv_i64ServicePartnerID As Int64, _
                                                             ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetHEATINGCHARGEByByDepotIDInvoiceNo(bv_i32DepotID, _
                                                                                     bv_i64ServicePartnerID, _
                                                                                     bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNo() TABLE NAME:MISCELLANEOUS_INVOICE"

    <OperationContract()> _
    Public Function pub_GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                                    ByVal bv_i64ServicePartnerID As Int64, _
                                                                    ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNo(bv_i32DepotID, _
                                                                                            bv_i64ServicePartnerID, _
                                                                                            bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetHANDLINGCHARGEByByDepotIDInvoiceNo() TABLE NAME:HANDLING_CHARGE"

    <OperationContract()> _
    Public Function pub_GetHANDLINGCHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                              ByVal bv_i64ServicePartnerID As Int64, _
                                                              ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetHANDLINGCHARGEByByDepotIDInvoiceNo(bv_i32DepotID, _
                                                                                      bv_i64ServicePartnerID, _
                                                                                      bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_GetHANDLINGCHARGEByByDepotIDInvoiceNoByCustomer(ByVal bv_i32DepotID As Int64, _
                                                              ByVal bv_i64ServicePartnerID As Int64, _
                                                              ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetHANDLINGCHARGEByByDepotIDInvoiceNoByCustomer(bv_i32DepotID, _
                                                                                      bv_i64ServicePartnerID, _
                                                                                      bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_GetGWSHANDLINGCHARGEByByDepotIDInvoiceNoByAgent(ByVal bv_i32DepotID As Int64, _
                                                              ByVal bv_i64ServicePartnerID As Int64, _
                                                              ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetGWSHANDLINGCHARGEByByDepotIDInvoiceNoByAgent(bv_i32DepotID, _
                                                                                      bv_i64ServicePartnerID, _
                                                                                      bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function


   

#End Region

#Region "pub_GetSTORAGECHARGEByByDepotIDInvoiceNo() TABLE NAME:STORAGE_CHARGE"

    <OperationContract()> _
    Public Function pub_GetSTORAGECHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                             ByVal bv_i64ServicePartnerID As Int64, _
                                                             ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices

            dsViewInvoiceData = objViewInvoices.GetSTORAGECHARGEByByDepotIDInvoiceNo(bv_i32DepotID, _
                                                                                     bv_i64ServicePartnerID, _
                                                                                     bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByCustomer(ByVal bv_i32DepotID As Int64, _
                                                             ByVal bv_i64ServicePartnerID As Int64, _
                                                             ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices

            dsViewInvoiceData = objViewInvoices.Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByCustomer(bv_i32DepotID, _
                                                                                     bv_i64ServicePartnerID, _
                                                                                     bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByAgent(ByVal bv_i32DepotID As Int64, _
                                                             ByVal bv_i64ServicePartnerID As Int64, _
                                                             ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices

            dsViewInvoiceData = objViewInvoices.Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByAgent(bv_i32DepotID, _
                                                                                     bv_i64ServicePartnerID, _
                                                                                     bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "pub_GetREPAIRCHARGEByByDepotIDInvoiceNo() TABLE NAME:REPAIR_CHARGE"

    <OperationContract()> _
    Public Function pub_GetREPAIRCHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                            ByVal bv_i64ServicePartnerID As Int64, _
                                                            ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetREPAIRCHARGEByByDepotIDInvoiceNo(bv_i32DepotID, _
                                                                                    bv_i64ServicePartnerID, _
                                                                                    bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    <OperationContract()> _
    Public Function pub_Get_GWS_REPAIRCHARGEByByCustomerIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                            ByVal bv_i64ServicePartnerID As Int64, _
                                                            ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.Get_GWS_REPAIRCHARGEByByCustomerIDInvoiceNo(bv_i32DepotID, _
                                                                                    bv_i64ServicePartnerID, _
                                                                                    bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    <OperationContract()> _
    Public Function pub_Get_GWS_REPAIRCHARGEByByAgentIdInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                            ByVal bv_i64AgentID As Int64, _
                                                            ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.Get_GWS_REPAIRCHARGEByByAgentIdInvoiceNo(bv_i32DepotID, _
                                                                                    bv_i64AgentID, _
                                                                                    bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_UpdateInvoiceNoBillingFlag()"
    <OperationContract()> _
    Public Function pub_UpdateInvoiceNoBillingFlag(ByVal bv_strTable1PrimaryID As String, _
                                                   ByVal bv_strTable2PrimaryID As String, _
                                                   ByVal bv_strBillingFlag As String, _
                                                   ByRef bv_strFinalInvoiceNo As String, _
                                                   ByVal bv_i64CustomerCurrencyID As Int64, _
                                                   ByVal bv_i64DepotCurrencyID As Int64, _
                                                   ByVal bv_decExchangeRate As Decimal, _
                                                   ByVal bv_datFromDate As Date, _
                                                   ByVal bv_datToDate As Date, _
                                                   ByVal bv_decTotalCustomerAmount As Decimal, _
                                                   ByVal bv_decTotalDepotAmount As Decimal, _
                                                   ByVal bv_i64CustomerID As Int64, _
                                                   ByVal bv_i64InvoicingPartyID As Int64, _
                                                   ByVal bv_i32DepotID As Int32, _
                                                   ByVal bv_strUserName As String, _
                                                   ByVal bv_blnGenerateInvoiceNo As Boolean, _
                                                   ByVal bv_i32InvoiceTypeID As Int32, _
                                                   ByVal bv_strInvoiceType As String, _
                                                   ByVal bv_strDraftInvoiceNo As String, _
                                                   ByRef br_strInvoiceFileName As String, _
                                                   ByVal bv_strGITransactionNo As String, _
                                                   ByVal bv_strEquipmentNo As String, _
                                                   ByVal bv_dsDataset As DataSet, _
                                                   ByVal intCountEquipment As Integer, _
                                                   ByVal bv_blnFinanceKey As Boolean, _
                                                   ByVal bv_dtInvoiceHeader As DataTable, _
                                                   ByRef bv_Error_Msg As String) As Boolean
        Try
            Dim objCommonUIs As New CommonUIs
            Dim strInvoicePrefix As String = Config.pub_GetAppConfigValue("InvoicePrefix")
            objCommonUIs.pub_UpdateInvoiceTableInvoiceNoBillingFlag(bv_strTable1PrimaryID, _
                                                                    bv_strTable2PrimaryID, _
                                                                    bv_strBillingFlag, _
                                                                    bv_strFinalInvoiceNo, _
                                                                    bv_i64CustomerCurrencyID, _
                                                                    bv_i64DepotCurrencyID, _
                                                                    bv_decExchangeRate, _
                                                                    bv_datFromDate, _
                                                                    bv_datToDate, _
                                                                    bv_decTotalCustomerAmount, _
                                                                    bv_decTotalDepotAmount, _
                                                                    bv_i64CustomerID, _
                                                                    bv_i64InvoicingPartyID, _
                                                                    bv_i32DepotID, _
                                                                    bv_strUserName, _
                                                                    bv_blnGenerateInvoiceNo, _
                                                                    bv_i32InvoiceTypeID, _
                                                                    bv_strInvoiceType, _
                                                                    "FINAL", _
                                                                    bv_strDraftInvoiceNo, _
                                                                    br_strInvoiceFileName, _
                                                                    bv_strGITransactionNo, _
                                                                    bv_strEquipmentNo, _
                                                                    bv_dsDataset, _
                                                                    intCountEquipment,
                                                                    strInvoicePrefix, _
                                                                    bv_blnFinanceKey, _
                                                                    bv_dtInvoiceHeader, _
                                                                    bv_Error_Msg)

            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))

        End Try
    End Function
#End Region

#Region "pub_fillDatasetforInvoiceGeneration"
    <OperationContract()> _
    Public Function pub_fillDatasetforInvoiceGeneration(ByVal bv_i32InvoiceTypeID As Int32, _
                                                        ByRef bv_dtInvoicetable As DataTable, _
                                                        ByVal bv_decExchangeRate As Decimal, _
                                                        ByVal bv_datFromDate As Date, _
                                                        ByVal bv_datToDate As Date, _
                                                        Optional ByVal bv_i32CustomerID As Int32 = 0, _
                                                        Optional ByRef bv_dsDataset As DataSet = Nothing, _
                                                        Optional ByVal bv_i32DepotID As Int32 = 0, _
                                                        Optional ByVal str_067InvoiceGenerationGWSBit As String = Nothing) As Boolean

        Try
            Dim objInvoiceGeneration As New CommonUIs
            objInvoiceGeneration.fillDatasetforInvoiceGeneration(bv_i32InvoiceTypeID, _
                                                                 bv_dtInvoicetable, _
                                                                 bv_decExchangeRate, _
                                                                 bv_datFromDate, _
                                                                 bv_datToDate, _
                                                                 0, 0, _
                                                                 bv_i32CustomerID, _
                                                                 bv_dsDataset, _
                                                                 bv_i32DepotID, _
                                                                 str_067InvoiceGenerationGWSBit)
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBankDetailByDepotId() TABLE NAME:BANK_DETAIL"

    <OperationContract()> _
    Public Function pub_GetBankDetailByDepotId(ByVal bv_intDepotId As Int32, _
                                               ByRef br_dtBankDetail As DataTable) As Boolean

        Try
            Dim objCommonUIs As New CommonUIs
            br_dtBankDetail = objCommonUIs.GetBankDetailByDepotId(bv_intDepotId).Tables(InvoiceGenerationData._V_BANK_DETAIL)
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBankDetailByDepotIdForForeignCurrency() TABLE NAME:BANK_DETAIL"

    <OperationContract()> _
    Public Function pub_GetBankDetailByDepotIdForForeignCurrency(ByVal bv_intDepotId As Int32, _
                                                                 ByRef br_dtBankDetail As DataTable) As Boolean

        Try
            Dim objCommonUIs As New CommonUIs
            br_dtBankDetail = objCommonUIs.GetBankDetailByDepotIdForForeignCurrency(bv_intDepotId).Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL)
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerByDepotIdCustomerID() TABLE NAME:V_CUSTOMER"

    <OperationContract()> _
    Public Function pub_GetCustomerByDepotIdCustomerID(ByVal bv_i64ServiceId As Int64, _
                                                       ByVal bv_strCustomerType As String, _
                                                       ByVal bv_i32DepotId As Int32, _
                                                       ByRef br_dtCustomer As DataTable) As Boolean

        Try
            Dim objCommonUIs As New CommonUIs
            br_dtCustomer = objCommonUIs.GetCustomerByDepotIdCustomerID(bv_i64ServiceId, _
                                                                        bv_strCustomerType, _
                                                                        bv_i32DepotId).Tables(InvoiceGenerationData._V_CUSTOMER)


            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNo() TABLE NAME:V_TRANSPORTATION_INVOICE"

    <OperationContract()> _
    Public Function pub_GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                                     ByVal bv_i64ServicePartnerID As Int64, _
                                                                     ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNo(bv_i32DepotID, _
                                                                                             bv_i64ServicePartnerID, _
                                                                                             bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetRentalChargeByByDepotIDInvoiceNo() TABLE NAME:RENTAL_CHARGE"

    <OperationContract()> _
    Public Function pub_GetRentalChargeByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                            ByVal bv_i64ServicePartnerID As Int64, _
                                                            ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetRentalChargeByByDepotIDInvoiceNo(bv_i32DepotID, _
                                                                                    bv_i64ServicePartnerID, _
                                                                                    bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_generateInvoiceXml() TABLE NAME:RENTAL_CHARGE"
    ''' <summary>
    ''' This method will create xml file of invoice(cleaning or transportation) for those customer's xml_bt=1 in customer master 
    ''' </summary>
    ''' <param name="bv_dsDataset"></param>
    ''' <param name="bv_dtViewInvoice"></param>
    ''' <param name="bv_strFinalinvcno"></param>
    ''' <param name="bv_i32InvoiceTypeID"></param>
    ''' <param name="bv_strUsername"></param>
    ''' <param name="bv_strInvoiceFileName"></param>
    ''' <remarks></remarks>

    <OperationContract()> _
    Public Sub pub_generateInvoiceXml(ByVal bv_dsDataset As DataSet, ByVal bv_dtViewInvoice As DataTable, ByVal bv_strFinalinvcno As String, ByVal bv_i32InvoiceTypeID As Integer, ByVal bv_strUsername As String, ByVal bv_strInvoiceFileName As String)
        Dim objTrans As New Transactions
        Try
            Dim intDepotId As Integer = 0
            Dim objViewInvoices As New ViewInvoices
            Dim strCustomerCurrencycode As String = String.Empty
            Dim strInvoiceDate As String = String.Empty
            Dim strSupplierCode As String = "14317"
            Dim strOfficecode As String = "16"
            Dim strVendorcode As String = String.Empty
            Dim strText As String = String.Empty
            Dim strType As String = String.Empty
            Dim int64CustomerId As Long = 0
            Dim strCustomercode As String = String.Empty
            Dim strInvoiceUrl As String = String.Concat(bv_strInvoiceFileName, ".pdf")
            If bv_i32InvoiceTypeID = 80 Then
                strText = "Cleaning"
                strType = "D"
            ElseIf bv_i32InvoiceTypeID = 83 Then
                strText = "Transportation"
                strType = "T"
            End If

            Dim strTotalAmt As String = String.Empty
            Dim strDraftInvoiceNo As String = String.Empty
            Dim strChargeCode As String = "BASE"
            Dim strTax As String = "0000000.00"

            If bv_dtViewInvoice.Rows.Count > 0 Then
                With bv_dtViewInvoice.Rows(0)
                    If Not IsDBNull(.Item(ViewInvoiceData.CSTMR_ID)) Then
                        int64CustomerId = CLng(.Item(ViewInvoiceData.CSTMR_ID))
                        strCustomercode = CStr(.Item(ViewInvoiceData.CSTMR_CD))
                        'strCustomerCurrencycode = CStr(.Item(ViewInvoiceData.CSTMR_CRRNCY_CD))
                        strCustomerCurrencycode = CStr(.Item(ViewInvoiceData.CUSTOMERCRRNCY))
                        strInvoiceDate = String.Format("{0:yyyy-MM-dd}", CDate(DateTime.Now)).Replace("-", "")
                        strTotalAmt = CStr(Math.Round(CDec(.Item(ViewInvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC)), 2)).PadLeft(10, CChar("0"))
                        'strTotalAmt = String.Format("{0:0000000.00}", (.Item(ViewInvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC)).ToString().PadLeft(10, CChar("0")))
                        'strInvoiceUrl = CStr(.Item(ViewInvoiceData.FL_NM))
                        strVendorcode = CStr(.Item(ViewInvoiceData.DPT_CD))
                        intDepotId = CInt(.Item(ViewInvoiceData.DPT_ID))
                        'strDraftInvoiceNo = CStr(.Item(ViewInvoiceData.INVC_NO))
                    End If
                End With
            End If
            Dim fileNameExtension As String = ".xml"
            Dim strPath As String = Config.pub_GetAppConfigValue("GenerateXml")
            Dim strFilename As String = String.Concat("JTS", Now.ToString(("yyyyMMddHHmmss")), fileNameExtension)

            Dim writter As XmlTextWriter
            Dim encoding As System.Text.UTF8Encoding
            Dim objStream As Stream = New FileStream((strPath) + strFilename, FileMode.Create)
            writter = New System.Xml.XmlTextWriter(objStream, encoding)
            'XmlWriter.Create(String.Concat(path, name, ".", fileNameExtension))
            writter.Formatting = System.Xml.Formatting.Indented
            writter.WriteProcessingInstruction("xml", "version=""1.0"" encoding=""ISO-8859-1""")

            writter.WriteStartElement("ns0:VINVH")
            writter.WriteStartAttribute("xmlns:ns0")
            writter.WriteValue("http://STC_ADINV_Vendor_Invoice_Schema")
            writter.WriteEndAttribute()
            'Record Start
            writter.WriteStartElement("Record")
            writter.WriteElementString("VendorID", strVendorcode)
            writter.WriteElementString("Supplier", strSupplierCode)

            'Invoice start
            writter.WriteStartElement("Invoice")
            writter.WriteElementString("VendorRef", pub_getValue(bv_strFinalinvcno, 20))
            writter.WriteElementString("Currency", strCustomerCurrencycode)
            writter.WriteElementString("InvoiceDate", strInvoiceDate)
            writter.WriteElementString("OfficeCode", strOfficecode)
            writter.WriteElementString("Text", strText)


            writter.WriteElementString("TotalAmount", strTotalAmt)
            writter.WriteElementString("InvoiceURL", strInvoiceUrl)
            If bv_i32InvoiceTypeID = 80 Then
                For Each drIv As DataRow In bv_dsDataset.Tables(ViewInvoiceData._CLEANING_INVOICE).Rows

                    ''Equipment Moves
                    writter.WriteStartElement("Move")
                    ''Move No
                    writter.WriteElementString("MoveNumber", pub_getValue(drIv.Item(ViewInvoiceData.RFRNC_NO).ToString, 8))
                    ''Tank On Move
                    writter.WriteStartElement("TankonMove")
                    writter.WriteElementString("TankNumber", drIv.Item(ViewInvoiceData.EQPMNT_NO).ToString)
                    '
                    writter.WriteStartElement("ChargeforTank")
                    writter.WriteElementString("ChargeCode", strChargeCode) ' String.Format("{0:000.0}", val1)
                    'writter.WriteElementString("ChargeAmount", String.Format("{0:0000000.00}", drIv.Item(ViewInvoiceData.CLNNG_RT).ToString).PadLeft(10, CChar("0")))
                    writter.WriteElementString("ChargeAmount", String.Format("{0:0000000.00}", drIv.Item(ViewInvoiceData.TTL_CSTS_NC).ToString).PadLeft(10, CChar("0")))
                    writter.WriteElementString("TaxAmount", strTax)
                    writter.WriteElementString("ServiceDate", String.Format("{0:yyyy-MM-dd}", CDate(drIv.Item(ViewInvoiceData.ORGNL_CLNNG_DT))).Replace("-", "")) 'activity date
                    writter.WriteElementString("VendorType", strType)
                    writter.WriteElementString("Text", "Cleaning")
                    writter.WriteElementString("SupportURL", "")
                    writter.WriteElementString("LineResponse", "")
                    writter.WriteEndElement()
                    ''ChargeforTank
                    writter.WriteEndElement()
                    ''
                    writter.WriteEndElement()
                Next
            ElseIf bv_i32InvoiceTypeID = 83 Then
                For Each drIv As DataRow In bv_dsDataset.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Rows

                    ''Equipment Moves
                    writter.WriteStartElement("Move")
                    ''Move No
                    writter.WriteElementString("MoveNumber", pub_getValue(drIv.Item(ViewInvoiceData.CSTMR_RF_NO).ToString, 8))
                    ''Tank On Move
                    writter.WriteStartElement("TankonMove")
                    writter.WriteElementString("TankNumber", drIv.Item(ViewInvoiceData.EQPMNT_NO).ToString)
                    '
                    writter.WriteStartElement("ChargeforTank")
                    writter.WriteElementString("ChargeCode", strChargeCode)
                    writter.WriteElementString("ChargeAmount", String.Format("{0:0000000.00}", drIv.Item(ViewInvoiceData.CSTMR_AMNT).ToString).PadLeft(10, CChar("0")))
                    writter.WriteElementString("TaxAmount", strTax)
                    writter.WriteElementString("ServiceDate", String.Format("{0:yyyy-MM-dd}", CDate(drIv.Item(ViewInvoiceData.JB_END_DT))).Replace("-", ""))
                    writter.WriteElementString("VendorType", strType)
                    writter.WriteElementString("Text", drIv.Item(ViewInvoiceData.RT_DSCRPTN_VC).ToString)
                    writter.WriteElementString("SupportURL", "")
                    writter.WriteElementString("LineResponse", "")
                    writter.WriteEndElement()
                    ''ChargeforTank
                    writter.WriteEndElement()
                    ''
                    writter.WriteEndElement()
                Next
            End If

            writter.WriteElementString("HeaderResponse", "")
            ''
            writter.WriteEndElement()
            'Invoice End
            writter.WriteEndElement()
            'Record End

            writter.WriteEndElement()

            writter.Close()
            Dim int64Invoiceediid As Long
            'Dim strTablename As String = ""
            int64Invoiceediid = objViewInvoices.CreateXMLEDI(int64CustomerId, strCustomercode, strText, bv_strFinalinvcno, strFilename, Nothing, "", Now, "Generated", "", False, bv_strUsername, intDepotId, strInvoiceUrl, objTrans) 'insert into fact table
            If bv_i32InvoiceTypeID = 80 Then
                'strTablename = "ViewInvoiceData._CLEANING_INVOICE"
                For Each dr As DataRow In bv_dsDataset.Tables(ViewInvoiceData._CLEANING_INVOICE).Rows
                    objViewInvoices.CreateXMLEDIDetail(int64Invoiceediid, CStr(dr.Item(ViewInvoiceData.EQPMNT_NO)), bv_strFinalinvcno, "", "", objTrans) ''insert into fact table detail
                Next
            ElseIf bv_i32InvoiceTypeID = 83 Then
                'strTablename = "ViewInvoiceData._TRANSPORTATION_INVOICE"
                For Each dr As DataRow In bv_dsDataset.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Rows
                    objViewInvoices.CreateXMLEDIDetail(int64Invoiceediid, CStr(dr.Item(ViewInvoiceData.EQPMNT_NO)), bv_strFinalinvcno, "", "", objTrans) 'insert into fact table detail
                Next
            End If
            objTrans.commit()
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Sub


    Public Function pub_getValue(ByVal strValue As String, ByVal bv_SpaceLen As Int32) As String
        Dim strBuild As New Text.StringBuilder
        If strValue = String.Empty Or strValue = Nothing Then
            strBuild.Append(LTrim(RTrim(strValue)))
        Else
            If strValue.Length = bv_SpaceLen Or strValue.Length < bv_SpaceLen Then
                strBuild.Append(LTrim(RTrim(strValue)))
            ElseIf strValue.Length > bv_SpaceLen Then
                strBuild.Append(strValue.Substring(0, bv_SpaceLen))
            End If
        End If
        Return strBuild.ToString.ToUpper
    End Function
#End Region

    'GWS
#Region "pub_GetINSPECTIONCHARGEByByDepotIDInvoiceNo() TABLE NAME:INSPECTION_CHARGE"

    <OperationContract()> _
    Public Function pub_GetINSPECTIONCHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                              ByVal bv_i64ServicePartnerID As Int64, _
                                                              ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetINSPECTIONCHARGEByByDepotIDInvoiceNo(bv_i32DepotID, _
                                                                                      bv_i64ServicePartnerID, _
                                                                                      bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Invoice Page Role Rights"

    'Get View invoice Role rights Information - about Logged user
    <OperationContract()> _
    Public Function GetViewInvoiceRoleRights(ByVal bv_ActivityID As Int64, ByVal bv_UserName As String) As DataTable
        Try

            Dim objViewInvoices As New ViewInvoices
            Return objViewInvoices.GetViewInvoiceRoleRights(bv_ActivityID, bv_UserName)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    'Get Handling charge for Finalized Invoice
    <OperationContract()> _
    Public Function GetHANDLINGCHARGEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                              ByVal bv_i64ServicePartnerID As Int64, _
                                                              ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetHANDLINGCHARGEByByDepotIDInvoiceNoCancel(bv_i32DepotID, _
                                                                                      bv_i64ServicePartnerID, _
                                                                                      bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Get Storage charge for Finalized Invoice
    <OperationContract()> _
    Public Function GetSTORAGECHARGEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                             ByVal bv_i64ServicePartnerID As Int64, _
                                                             ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try

            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices

            dsViewInvoiceData = objViewInvoices.GetSTORAGECHARGEByByDepotIDInvoiceNoCancel(bv_i32DepotID, _
                                                                                     bv_i64ServicePartnerID, _
                                                                                     bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get HEATING charge for Finalized Invoice
    <OperationContract()> _
    Public Function GetHEATINGCHARGEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                             ByVal bv_i64ServicePartnerID As Int64, _
                                                             ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetHEATINGCHARGEByByDepotIDInvoiceNoCancel(bv_i32DepotID, _
                                                                                     bv_i64ServicePartnerID, _
                                                                                     bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ' Get cleaning charge for Finalized Invoice
    <OperationContract()> _
    Public Function GetCLEANINGCHARGEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                              ByVal bv_i64ServicePartnerID As Int64, _
                                                              ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetCLEANINGCHARGEByByDepotIDInvoiceNoCancel(bv_i32DepotID, _
                                                                                      bv_i64ServicePartnerID, _
                                                                                      bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Get Repair charge for Finalized Invoice
    <OperationContract()> _
    Public Function GetREPAIRCHARGEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                            ByVal bv_i64ServicePartnerID As Int64, _
                                                            ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetREPAIRCHARGEByByDepotIDInvoiceNoCancel(bv_i32DepotID, _
                                                                                    bv_i64ServicePartnerID, _
                                                                                    bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get Miscellanceous and Credit Note charge for Finalized Invoice
    <OperationContract()> _
    Public Function GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                                    ByVal bv_i64ServicePartnerID As Int64, _
                                                                    ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNoCancel(bv_i32DepotID, _
                                                                                            bv_i64ServicePartnerID, _
                                                                                            bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Get Transportation charge for Finalized Invoice
    <OperationContract()> _
    Public Function GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                                     ByVal bv_i64ServicePartnerID As Int64, _
                                                                     ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNoCancel(bv_i32DepotID, _
                                                                                             bv_i64ServicePartnerID, _
                                                                                             bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get Rental charge for Finalized Invoice
    <OperationContract()> _
    Public Function GetRentalChargeByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                            ByVal bv_i64ServicePartnerID As Int64, _
                                                            ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet

        Try
            Dim dsViewInvoiceData As ViewInvoiceDataSet
            Dim objViewInvoices As New ViewInvoices
            dsViewInvoiceData = objViewInvoices.GetRentalChargeByByDepotIDInvoiceNoCancel(bv_i32DepotID, _
                                                                                    bv_i64ServicePartnerID, _
                                                                                    bv_strInvoiceNo)
            Return dsViewInvoiceData
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    <OperationContract()> _
    Public Function Sage_Invoice_Generations_InvoiceCancel(ByVal strInvoiceTypeID As Int32, ByVal bv_strInvoiceNo As String, _
                                             ByVal dtInvoiceHeader As DataTable, ByVal dsInvoice As DataSet, ByVal bv_strRemarks As String, _
                                             ByVal i32DepotID As Int32, ByVal bv_Username As String) As String

        Dim objTrans As New Transactions
        Try

            Dim objCommonUIs As New CommonUIs
            Dim objInvoiceReversals As New InvoiceReversals
            Dim objViewInvoices As New ViewInvoices
            Dim strInvoiceType As String = String.Empty
            Dim strInvoiceTypeDataTableName As String = String.Empty

            Select Case strInvoiceTypeID

                Case 78 'Handling & Storage
                    strInvoiceTypeDataTableName = "Handling_Storage_Invoice"
                    strInvoiceType = "HANDLING & STORAGE"


                Case 79 'Heating
                    strInvoiceTypeDataTableName = "HEATING_INVOICE"
                    strInvoiceType = "HEATING"

                Case 80 'Cleaning
                    strInvoiceTypeDataTableName = "Cleaning_Invoice"
                    strInvoiceType = "Cleaning"

                Case 81 'Repair
                    strInvoiceTypeDataTableName = "Repair_Invoice"
                    strInvoiceType = "Repair"

                Case 82 'Miscellaneous
                    strInvoiceTypeDataTableName = "MISCELLANEOUS_INVOICE"
                    strInvoiceType = "MISCELLANEOUS"

                Case 140 'Credit Note
                    strInvoiceTypeDataTableName = "MISCELLANEOUS_INVOICE"
                    strInvoiceType = "CREDIT NOTE"

                Case 83 'Transportation
                    strInvoiceTypeDataTableName = "TRANSPORTATION_INVOICE"
                    strInvoiceType = "TRANSPORTATION"

                Case 84 'Rental
                    strInvoiceTypeDataTableName = "RENTAL_INVOICE"
                    strInvoiceType = "RENTAL"

            End Select

            'Audit Log Information

            objCommonUIs.CreateAuditLog(Nothing, bv_strInvoiceNo, strInvoiceType, "Cancel", DateTime.Now, Nothing, Nothing, bv_strRemarks, bv_Username, i32DepotID, objTrans)

            'Invoice Cancellation Report 




            'Header
            Dim intCancelId As Int64 = Nothing
            Dim i64ServicePartnerID As Int32
            Dim strServicePartnerCode As String = String.Empty
            If Not IsDBNull(dtInvoiceHeader.Rows(0).Item(ViewInvoiceData.CSTMR_ID)) Then
                i64ServicePartnerID = CInt(dtInvoiceHeader.Rows(0).Item(ViewInvoiceData.CSTMR_ID))
                strServicePartnerCode = CStr(dtInvoiceHeader.Rows(0).Item(CustomerData.CSTMR_CD))
            End If
            If Not IsDBNull(dtInvoiceHeader.Rows(0).Item(ViewInvoiceData.INVCNG_PRTY_ID)) Then
                i64ServicePartnerID = CInt(dtInvoiceHeader.Rows(0).Item(ViewInvoiceData.INVCNG_PRTY_ID))
                strServicePartnerCode = CStr(dtInvoiceHeader.Rows(0).Item(CommonUIData.SRVC_PRTNR_CD))
            End If
            intCancelId = objInvoiceReversals.CreateINVOICE_CANCEL(strInvoiceTypeID, strInvoiceType, bv_strInvoiceNo, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.FRM_BLLNG_DT)), CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), _
                                                     i64ServicePartnerID, strServicePartnerCode, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.INVC_DT)), CStr(dtInvoiceHeader.Rows(0).Item(ViewInvoiceData.CRTD_BY)), DateTime.Now, _
                                                     CDbl(dtInvoiceHeader.Rows(0).Item(ViewInvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC)), CDbl(dtInvoiceHeader.Rows(0).Item(ViewInvoiceData.TTL_CST_IN_INVC_CRRNCY_NC)), i32DepotID, _
                                                     CInt(dtInvoiceHeader.Rows(0).Item(ViewInvoiceData.CSTMR_CRRNCY_ID)), CInt(dtInvoiceHeader.Rows(0).Item(ViewInvoiceData.DPT_CRRNCY_ID)), bv_Username, DateTime.Now, True, objTrans)

            'Details - 

            Dim strReferenceNo As String = " "
            Dim decCustomerAmount As Decimal = 0D
            Dim decDepoAmount As Decimal = 0D


            For Each dr As DataRow In dsInvoice.Tables(strInvoiceTypeDataTableName).Rows


                'If dsInvoice.Tables(strInvoiceTypeDataTableName).Columns.Contains(CommonUIData.RFRNC_EIR_NO_1) Then

                '    If Not dr.Item(InvoiceData.RFRNC_EIR_NO_1) Is DBNull.Value Then
                '        strReferenceNo = dr.Item(InvoiceData.RFRNC_EIR_NO_1).ToString()
                '    End If
                'End If

                'If dsInvoice.Tables(strInvoiceTypeDataTableName).Columns.Contains(CommonUIData.RFRNC_EIR_NO_2) Then

                '    If Not dr.Item(InvoiceData.RFRNC_EIR_NO_2) Is DBNull.Value Then
                '        strReferenceNo = dr.Item(InvoiceData.RFRNC_EIR_NO_2).ToString()
                '    End If
                'End If



                'Get Customer Currency Amount and Depo Currency Amount - based on invoice
                Select Case strInvoiceType.ToUpper()

                    Case "CLEANING"

                        If Not dr.Item(InvoiceGenerationData.TTL_CSTS_NC) Is DBNull.Value Then
                            decCustomerAmount = CDec(dr.Item(InvoiceGenerationData.TTL_CSTS_NC))
                        Else
                            decCustomerAmount = 0D
                        End If

                        If Not dr.Item(InvoiceGenerationData.DPT_TTL_NC) Is DBNull.Value Then
                            decDepoAmount = CDec(dr.Item(InvoiceGenerationData.DPT_TTL_NC))
                        Else
                            decDepoAmount = 0D
                        End If

                        If Not dr.Item(CommonUIData.RFRNC_NO) Is DBNull.Value Then
                            strReferenceNo = dr.Item(CommonUIData.RFRNC_NO).ToString()
                        End If

                    Case "REPAIR"

                        If Not dr.Item(InvoiceGenerationData.TOTALAMOUNT) Is DBNull.Value Then
                            decCustomerAmount = CDec(dr.Item(InvoiceGenerationData.TOTALAMOUNT))
                        Else
                            decCustomerAmount = 0D
                        End If

                        If Not dr.Item(InvoiceGenerationData.DPT_TTL_NC) Is DBNull.Value Then
                            decDepoAmount = CDec(dr.Item(InvoiceGenerationData.DPT_TTL_NC))
                        Else
                            decDepoAmount = 0D
                        End If

                        If Not dr.Item(RepairEstimateData.ESTMT_NO) Is DBNull.Value Then
                            strReferenceNo = dr.Item(CommonUIData.ESTMT_NO).ToString()
                        End If

                    Case "TRANSPORTATION"

                        If Not dr.Item(InvoiceGenerationData.CSTMR_AMNT) Is DBNull.Value Then
                            decCustomerAmount = CDec(dr.Item(InvoiceGenerationData.CSTMR_AMNT))
                        Else
                            decCustomerAmount = 0D
                        End If

                        If Not dr.Item(InvoiceGenerationData.DPT_AMNT) Is DBNull.Value Then
                            decDepoAmount = CDec(dr.Item(InvoiceGenerationData.DPT_AMNT))
                        Else
                            decDepoAmount = 0D
                        End If

                        If Not dr.Item(TransportationData.RQST_NO) Is DBNull.Value Then
                            strReferenceNo = dr.Item(TransportationData.RQST_NO).ToString()
                        End If

                    Case "RENTAL"

                        If Not dr.Item(InvoiceGenerationData.TTL_CSTS_NC) Is DBNull.Value Then
                            decCustomerAmount = CDec(dr.Item(InvoiceGenerationData.TTL_CSTS_NC))
                        Else
                            decCustomerAmount = 0D
                        End If

                        If Not dr.Item(InvoiceGenerationData.DPT_TTL_NC) Is DBNull.Value Then
                            decDepoAmount = CDec(dr.Item(InvoiceGenerationData.DPT_TTL_NC))
                        Else
                            decDepoAmount = 0D
                        End If

                        If Not dr.Item(InvoiceData.RFRNC_EIR_NO_1) Is DBNull.Value Then
                            strReferenceNo = dr.Item(InvoiceData.RFRNC_EIR_NO_1).ToString()
                        End If

                    Case "HANDLING & STORAGE"

                        If Not dr.Item(InvoiceGenerationData.TTL_CSTS_NC) Is DBNull.Value Then
                            decCustomerAmount = CDec(dr.Item(InvoiceGenerationData.TTL_CSTS_NC))
                        Else
                            decCustomerAmount = 0D
                        End If

                        If Not dr.Item(InvoiceGenerationData.DPT_TTL_NC) Is DBNull.Value Then
                            decDepoAmount = CDec(dr.Item(InvoiceGenerationData.DPT_TTL_NC))
                        Else
                            decDepoAmount = 0D
                        End If

                        If Not dr.Item(CommonUIData.RFRNC_EIR_NO_1) Is DBNull.Value Then
                            strReferenceNo = dr.Item(CommonUIData.RFRNC_EIR_NO_1).ToString()
                        End If

                    Case "HEATING"

                        If Not dr.Item(InvoiceGenerationData.TTL_RT_NC) Is DBNull.Value Then
                            decCustomerAmount = CDec(dr.Item(InvoiceGenerationData.TTL_RT_NC))
                        Else
                            decCustomerAmount = 0D
                        End If

                        If Not dr.Item(InvoiceGenerationData.DPT_TTL_NC) Is DBNull.Value Then
                            decDepoAmount = CDec(dr.Item(InvoiceGenerationData.DPT_TTL_NC))
                        Else
                            decDepoAmount = 0D
                        End If
                        strReferenceNo = " "

                    Case "MISCELLANEOUS"

                        If Not dr.Item(InvoiceGenerationData.AMNT_NC) Is DBNull.Value Then
                            decCustomerAmount = CDec(dr.Item(InvoiceGenerationData.AMNT_NC))
                        Else
                            decCustomerAmount = 0D
                        End If

                        If Not dr.Item(InvoiceGenerationData.DPT_TTL_NC) Is DBNull.Value Then
                            decDepoAmount = CDec(dr.Item(InvoiceGenerationData.DPT_TTL_NC))
                        Else
                            decDepoAmount = 0D
                        End If
                        strReferenceNo = " "
                End Select


                objInvoiceReversals.CreateINVOICE_CANCEL_DETAIL(intCancelId, CStr(dr.Item(InvoiceData.EQPMNT_NO)), CStr(dr.Item(InvoiceData.EQPMNT_TYP_CD)), strReferenceNo, decCustomerAmount, decDepoAmount, True, objTrans)
            Next

            Dim strFinalInvoiceNo As String = bv_strInvoiceNo
            Dim bv_errorMsg As String
            'Sage Integration - Generate Auto Credit Note for Cancelled Invoice - Except Credit Note
            bv_errorMsg = objCommonUIs.Sage_Invoice_Generations_InvoiceCancel(strInvoiceType, bv_strInvoiceNo, dtInvoiceHeader, dsInvoice.Tables(strInvoiceTypeDataTableName), bv_strRemarks, i32DepotID, objTrans)
            If Not bv_errorMsg = String.Empty Then
                Return bv_errorMsg
            End If


            'Cancel Previous Invoice
            Dim dtInvoice As New DataTable
            Dim strDraftInvoiceNo As String = Nothing
            Dim strCustomerId As String = Nothing
            Dim strInvocingPartyId As String = Nothing

            If Not IsDBNull(dtInvoiceHeader.Rows(0).Item(CustomerData.CSTMR_ID)) Then
                strCustomerId = CStr(dtInvoiceHeader.Rows(0).Item(CustomerData.CSTMR_ID))
            Else
                strCustomerId = Nothing
            End If

            If Not IsDBNull(dtInvoiceHeader.Rows(0).Item(CustomerData.INVCNG_PRTY_ID)) Then
                strInvocingPartyId = CStr(dtInvoiceHeader.Rows(0).Item(CustomerData.INVCNG_PRTY_ID))
            Else
                strInvocingPartyId = Nothing
            End If

            Select Case strInvoiceType.ToUpper()

                Case "CLEANING"

                    'Get DraftInvoice No
                    dtInvoice = objViewInvoices.Get_CleaningInvoice_DraftNo(i32DepotID, strFinalInvoiceNo, objTrans)

                    If dtInvoice.Rows.Count > 0 Then
                        strDraftInvoiceNo = CStr(dtInvoice.Rows(0).Item(ViewInvoiceData.DRFT_INVC_NO))
                    End If

                    'Cancel Final Invoice No - Charge Table
                    objViewInvoices.Cancel_CleaningInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'DeActivate Final Invoice   - Invoice history
                    objViewInvoices.DeActive_FinalInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'Activate Draft Invoice  - Invoice history
                    objViewInvoices.Active_DraftInvoice(i32DepotID, strDraftInvoiceNo, objTrans)

                    ''Create Invoice Histroy - for Generated Credit Note
                    'objViewInvoices.CreateCreditNote_InvoiceHistory(bv_strInvoiceNo, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), Nothing, "CN", CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.INVC_CRRNCY_ID)), _
                    '                                                CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.EXCHNG_RT_NC)), CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.CSTMR_CRRNCY_ID)), Nothing, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.FRM_BLLNG_DT)), _
                    '                                                CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_INVC_CRRNCY_NC)), _
                    '                                                "FINAL", CLng(strCustomerId), CLng(strInvocingPartyId), i32DepotID, True, bv_Username, DateTime.Now, Nothing, Nothing, objTrans)


                Case "REPAIR"

                    'Get DraftInvoice No
                    dtInvoice = objViewInvoices.Get_RepairInvoice_DraftNo(i32DepotID, strFinalInvoiceNo, objTrans)

                    If dtInvoice.Rows.Count > 0 Then
                        strDraftInvoiceNo = CStr(dtInvoice.Rows(0).Item(ViewInvoiceData.DRFT_INVC_NO))
                    End If

                    'Cancel Final Invoice No - Charge Table
                    objViewInvoices.Cancel_RepairInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'DeActivate Final Invoice   - Invoice history
                    objViewInvoices.DeActive_FinalInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'Activate Draft Invoice  - Invoice history
                    objViewInvoices.Active_DraftInvoice(i32DepotID, strDraftInvoiceNo, objTrans)

                    ''Create Invoice Histroy - for Generated Credit Note
                    'objViewInvoices.CreateCreditNote_InvoiceHistory(bv_strInvoiceNo, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), Nothing, "CN", CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.INVC_CRRNCY_ID)), _
                    '                                                CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.EXCHNG_RT_NC)), CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.CSTMR_CRRNCY_ID)), Nothing, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.FRM_BLLNG_DT)), _
                    '                                                CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_INVC_CRRNCY_NC)), _
                    '                                                "FINAL", CLng(strCustomerId), CLng(strInvocingPartyId), i32DepotID, True, bv_Username, DateTime.Now, Nothing, Nothing, objTrans)


                Case "TRANSPORTATION"

                    'Get DraftInvoice No
                    dtInvoice = objViewInvoices.Get_TransPortationInvoice_DraftNo(i32DepotID, strFinalInvoiceNo, objTrans)

                    If dtInvoice.Rows.Count > 0 Then
                        strDraftInvoiceNo = CStr(dtInvoice.Rows(0).Item(ViewInvoiceData.DRFT_INVC_NO))
                    End If

                    'Cancel Final Invoice No - Charge Table
                    objViewInvoices.Cancel_TransportationInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'DeActivate Final Invoice   - Invoice history
                    objViewInvoices.DeActive_FinalInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'Activate Draft Invoice  - Invoice history
                    objViewInvoices.Active_DraftInvoice(i32DepotID, strDraftInvoiceNo, objTrans)

                    'Update Transportation Table
                    Dim dtTransportation As DataTable = dsInvoice.Tables("TRANSPORTATION_INVOICE")
                    Dim strTransportationId As String = String.Empty


                    For Each drTrans As DataRow In dtTransportation.Rows
                        Dim dtTransDetail As DataTable
                        If Not strTransportationId.Contains(drTrans.Item(TransportationData.TRNSPRTTN_ID).ToString) Then
                            strTransportationId = String.Concat(drTrans.Item(TransportationData.TRNSPRTTN_ID).ToString, ",")
                            dtTransDetail = objViewInvoices.Get_BilledTransportationDetailByTransportId(CInt(drTrans.Item(TransportationData.TRNSPRTTN_ID)), objTrans)
                            If dtTransDetail.Rows.Count > 0 Then
                                objViewInvoices.Update_TransportationStatusByTransportId(CInt(drTrans.Item(TransportationData.TRNSPRTTN_ID)), 90, i32DepotID, objTrans)
                            Else
                                objViewInvoices.Update_TransportationStatusByTransportId(CInt(drTrans.Item(TransportationData.TRNSPRTTN_ID)), 95, i32DepotID, objTrans)
                            End If
                        End If
                    Next
                    '  pvt_UpdateTransportationTableOnCancel(i32DepotID, dsInvoice, objTrans)
                    ''Create Invoice Histroy - for Generated Credit Note
                    'objViewInvoices.CreateCreditNote_InvoiceHistory(bv_strInvoiceNo, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), Nothing, "CN", CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.INVC_CRRNCY_ID)), _
                    '                                                CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.EXCHNG_RT_NC)), CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.CSTMR_CRRNCY_ID)), Nothing, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.FRM_BLLNG_DT)), _
                    '                                                CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_INVC_CRRNCY_NC)), _
                    '                                                "FINAL", CLng(strCustomerId), CLng(strInvocingPartyId), i32DepotID, True, bv_Username, DateTime.Now, Nothing, Nothing, objTrans)

                Case "RENTAL"

                    'Get DraftInvoice No
                    dtInvoice = objViewInvoices.Get_RentalInvoice_DraftNo(i32DepotID, strFinalInvoiceNo, objTrans)

                    If dtInvoice.Rows.Count > 0 Then
                        strDraftInvoiceNo = CStr(dtInvoice.Rows(0).Item(ViewInvoiceData.DRFT_INVC_NO))
                    End If

                    'Cancel Final Invoice No - Charge Table
                    objViewInvoices.Cancel_RentalInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'Remove information from InvoiceHistory Details
                    objViewInvoices.Delete_InvoiceHistoryDetails(strFinalInvoiceNo, objTrans)

                    'DeActivate Final Invoice   - Invoice history
                    objViewInvoices.DeActive_FinalInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'Activate Draft Invoice  - Invoice history
                    objViewInvoices.Active_DraftInvoice(i32DepotID, strDraftInvoiceNo, objTrans)

                    ''Create Invoice Histroy - for Generated Credit Note
                    'objViewInvoices.CreateCreditNote_InvoiceHistory(bv_strInvoiceNo, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), Nothing, "CN", CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.INVC_CRRNCY_ID)), _
                    '                                                CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.EXCHNG_RT_NC)), CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.CSTMR_CRRNCY_ID)), Nothing, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.FRM_BLLNG_DT)), _
                    '                                                CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_INVC_CRRNCY_NC)), _
                    '                                                "FINAL", CLng(strCustomerId), CLng(strInvocingPartyId), i32DepotID, True, bv_Username, DateTime.Now, Nothing, Nothing, objTrans)


                Case "HANDLING & STORAGE"

                    'Get DraftInvoice No
                    dtInvoice = objViewInvoices.Get_HandlingInvoice_DraftNo(i32DepotID, strFinalInvoiceNo, objTrans)

                    If dtInvoice.Rows.Count > 0 Then
                        strDraftInvoiceNo = CStr(dtInvoice.Rows(0).Item(ViewInvoiceData.DRFT_INVC_NO))
                    End If

                    If strDraftInvoiceNo Is Nothing Then

                        'Get DraftInvoice No
                        dtInvoice = objViewInvoices.Get_StorageInvoice_DraftNo(i32DepotID, strFinalInvoiceNo, objTrans)

                        If dtInvoice.Rows.Count > 0 Then
                            strDraftInvoiceNo = CStr(dtInvoice.Rows(0).Item(ViewInvoiceData.DRFT_INVC_NO))
                        End If

                    End If

                    'Cancel Final Invoice No - Charge Table
                    objViewInvoices.Cancel_HandlingInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'Remove information from InvoiceHistory Details
                    objViewInvoices.Delete_InvoiceHistoryDetails(strFinalInvoiceNo, objTrans)

                    'Cancel Final Invoice No - Charge Table
                    objViewInvoices.Cancel_StorageInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'DeActivate Final Invoice   - Invoice history
                    objViewInvoices.DeActive_FinalInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'Activate Draft Invoice  - Invoice history
                    objViewInvoices.Active_DraftInvoice(i32DepotID, strDraftInvoiceNo, objTrans)

                    ''Create Invoice Histroy - for Generated Credit Note
                    'objViewInvoices.CreateCreditNote_InvoiceHistory(bv_strInvoiceNo, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), Nothing, "CN", CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.INVC_CRRNCY_ID)), _
                    '                                                CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.EXCHNG_RT_NC)), CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.CSTMR_CRRNCY_ID)), Nothing, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.FRM_BLLNG_DT)), _
                    '                                                CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_INVC_CRRNCY_NC)), _
                    '                                                "FINAL", CLng(strCustomerId), CLng(strInvocingPartyId), i32DepotID, True, bv_Username, DateTime.Now, Nothing, Nothing, objTrans)


                Case "HEATING"

                    'Get DraftInvoice No
                    dtInvoice = objViewInvoices.Get_HeatingInvoice_DraftNo(i32DepotID, strFinalInvoiceNo, objTrans)

                    If dtInvoice.Rows.Count > 0 Then
                        strDraftInvoiceNo = CStr(dtInvoice.Rows(0).Item(ViewInvoiceData.DRFT_INVC_NO))
                    End If

                    'Cancel Final Invoice No - Charge Table
                    objViewInvoices.Cancel_HeatingInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'DeActivate Final Invoice   - Invoice history
                    objViewInvoices.DeActive_FinalInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'Activate Draft Invoice  - Invoice history
                    objViewInvoices.Active_DraftInvoice(i32DepotID, strDraftInvoiceNo, objTrans)

                    ''Create Invoice Histroy - for Generated Credit Note
                    'objViewInvoices.CreateCreditNote_InvoiceHistory(bv_strInvoiceNo, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), Nothing, "CN", CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.INVC_CRRNCY_ID)), _
                    '                                                CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.EXCHNG_RT_NC)), CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.CSTMR_CRRNCY_ID)), Nothing, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.FRM_BLLNG_DT)), _
                    '                                                CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_INVC_CRRNCY_NC)), _
                    '                                                "FINAL", CLng(strCustomerId), CLng(strInvocingPartyId), i32DepotID, True, bv_Username, DateTime.Now, Nothing, Nothing, objTrans)


                Case "MISCELLANEOUS"

                    'Get DraftInvoice No
                    dtInvoice = objViewInvoices.Get_MISCELLANEOUSInvoice_DraftNo(i32DepotID, strFinalInvoiceNo, objTrans)

                    If dtInvoice.Rows.Count > 0 Then
                        strDraftInvoiceNo = CStr(dtInvoice.Rows(0).Item(ViewInvoiceData.DRFT_INVC_NO))
                    End If

                    'Cancel Final Invoice No - Charge Table
                    objViewInvoices.Cancel_MISCELLANEOUSInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'DeActivate Final Invoice   - Invoice history
                    objViewInvoices.DeActive_FinalInvoice(i32DepotID, strFinalInvoiceNo, objTrans)

                    'Activate Draft Invoice  - Invoice history
                    objViewInvoices.Active_DraftInvoice(i32DepotID, strDraftInvoiceNo, objTrans)

                    ''Create Invoice Histroy - for Generated Credit Note
                    'objViewInvoices.CreateCreditNote_InvoiceHistory(bv_strInvoiceNo, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), Nothing, "CN", CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.INVC_CRRNCY_ID)), _
                    '                                                CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.EXCHNG_RT_NC)), CLng(dtInvoiceHeader.Rows(0).Item(InvoiceData.CSTMR_CRRNCY_ID)), Nothing, CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.FRM_BLLNG_DT)), _
                    '                                                CDate(dtInvoiceHeader.Rows(0).Item(InvoiceData.TO_BLLNG_DT)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC)), CDec(dtInvoiceHeader.Rows(0).Item(InvoiceData.TTL_CST_IN_INVC_CRRNCY_NC)), _
                    '                                                "FINAL", CLng(strCustomerId), CLng(strInvocingPartyId), i32DepotID, True, bv_Username, DateTime.Now, Nothing, Nothing, objTrans)



            End Select

            'Make Active Draft Status


            'Add Entry in Invoice History



            objTrans.commit()
            Return "true"

        Catch ex As Exception
            objTrans.RollBack()
            Throw ex
        End Try
    End Function

#End Region


End Class
