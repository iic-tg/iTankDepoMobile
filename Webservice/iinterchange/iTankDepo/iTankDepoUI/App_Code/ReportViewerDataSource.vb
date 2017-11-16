Imports Microsoft.VisualBasic
Imports System.Runtime.Serialization

Public Class ReportViewerDataSource
    Inherits Pagebase
    Private pvt_DatasourceCollection As Data.DataSet
    Private pvt_filename As String
    Private pvt_ReportString As String
    Private Const PARAMETERDATA As String = "ParameterData"

#Region "Documents"
    Sub New(ByVal documentname As String, ByVal ParamCollection As String, ByVal strPageName As String, ByRef bv_strCustomer As String)
        Dim dsReport As New DataSet
        Dim dtDistinctCustomer As New DataTable
        Select Case documentname
            Case "HSINVOICE"
                Dim dsInvoiceDataSet As InvoiceDataSet = CType(RetrieveData("INVOICE"), InvoiceDataSet)
                pvt_DatasourceCollection = dsInvoiceDataSet
            Case "RPINVOICE"
                Dim dsInvoiceDataSet As InvoiceDataSet = CType(RetrieveData("INVOICE"), InvoiceDataSet)
                pvt_DatasourceCollection = dsInvoiceDataSet
            Case "GATEIN DOCUMENT"
                pvt_DatasourceCollection = CType(RetrieveData("GATE_IN_DOCUMENT"), DataSet)
                dsReport = CType(RetrieveData("GATE_IN_DOCUMENT"), DataSet)
                If dsReport.Tables(GateinData._V_GATEIN).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("HS_DISTINCT", dsReport.Tables(CommonUIData._V_GATEIN), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
                'GWC
            Case "GATEIN GWC"
                pvt_DatasourceCollection = CType(RetrieveData("GATE_IN_DOCUMENT"), DataSet)
                dsReport = CType(RetrieveData("GATE_IN_DOCUMENT"), DataSet)
                If dsReport.Tables(GateinData._V_GATEIN).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("HS_DISTINCT", dsReport.Tables(CommonUIData._V_GATEIN), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "GATEOUT GWC"
                dsReport = CType(RetrieveData("GATE_OUT_DOCUMENT"), DataSet)
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(GateOutData._V_GATEOUT).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(GateOutData._V_GATEOUT), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If

                'GWC
            Case "REPAIR COMPLETION"
                dsReport = CType(RetrieveData("REPAIR_COMPLETION_DOCUMENT"), DataSet)
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "GATE OUT"
                dsReport = CType(RetrieveData("GATE_OUT_DOCUMENT"), DataSet)
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(GateOutData._V_GATEOUT).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(GateOutData._V_GATEOUT), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "REPAIR WORK ORDER"
                Dim objRepairEstimate As New RepairEstimate
                Dim objCommon As New CommonData
                ParamCollection = String.Concat(ParamCollection + "&MultiLocation=" + objCommon.GetMultiLocationSupportConfig())
                ParamCollection = String.Concat(ParamCollection + "&HeadQuarterID=" + objCommon.GetHeadQuarterID())
                dsReport = objRepairEstimate.RepairWorkOrder(ParamCollection, CType(RetrieveData("REPAIR_ESTIMATE"), RepairEstimateDataSet), pvt_getAcaciaSpecificConfig())
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "REPAIR WORK ORDER GWC"
                Dim objRepairEstimate As New RepairEstimate
                Dim objCommon As New CommonData
                ParamCollection = String.Concat(ParamCollection + "&MultiLocation=" + objCommon.GetMultiLocationSupportConfig())
                ParamCollection = String.Concat(ParamCollection + "&HeadQuarterID=" + objCommon.GetHeadQuarterID())
                dsReport = objRepairEstimate.RepairWorkOrder(ParamCollection, CType(RetrieveData("REPAIR_ESTIMATE"), RepairEstimateDataSet), pvt_getAcaciaSpecificConfig())
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "REPAIR ESTIMATE"
                Dim objRepairEstimate As New RepairEstimate
                Dim objCommon As New CommonData
                ParamCollection = String.Concat(ParamCollection + "&MultiLocation=" + objCommon.GetMultiLocationSupportConfig())
                ParamCollection = String.Concat(ParamCollection + "&HeadQuarterID=" + objCommon.GetHeadQuarterID())
                dsReport = objRepairEstimate.RepairWorkOrder(ParamCollection, CType(RetrieveData("REPAIR_ESTIMATE"), RepairEstimateDataSet), pvt_getAcaciaSpecificConfig())
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "REPAIR ESTIMATE ACACIA"
                Dim objRepairEstimate As New RepairEstimate
                dsReport = objRepairEstimate.RepairWorkOrder(ParamCollection, CType(RetrieveData("REPAIR_ESTIMATE"), RepairEstimateDataSet), pvt_getAcaciaSpecificConfig())
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "REPAIR ESTIMATE GWC"
                Dim objRepairEstimate As New RepairEstimate
                Dim objCommonConfig As New ConfigSetting()
                Dim objCommonUI As New CommonUI()
                Dim objCommon As New CommonData
                Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
                ParamCollection = String.Concat(ParamCollection + "&GWS=" + objCommonConfig.pub_GetConfigSingleValue("063", intDepotID))
                ParamCollection = String.Concat(ParamCollection + "&MultiLocation=" + objCommon.GetMultiLocationSupportConfig())
                ParamCollection = String.Concat(ParamCollection + "&HeadQuarterID=" + objCommon.GetHeadQuarterID())
                dsReport = objRepairEstimate.RepairWorkOrder(ParamCollection, CType(RetrieveData("REPAIR_ESTIMATE"), RepairEstimateDataSet), pvt_getAcaciaSpecificConfig())
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(RepairEstimateData._V_REPAIR_ESTIMATE), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "CLEANING CERTIFICATE"
                Dim objCleaning As New Cleaning

                dsReport = objCleaning.pub_GenerateCleaningCertificate(ParamCollection)
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(CleaningData._V_CLEANING).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(CommonUIData._V_CLEANING), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "PRODUCT ENQUIRY"
                dsReport = CType(RetrieveData("ENQUIRY"), DataSet)
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANING_RATE).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(EnquiryData._V_ENQUIRY_CUSTOMER_CLEANING_RATE), EnquiryData.CSTMR_ID, "", EnquiryData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "CUSTOMER ENQUIRY"
                dsReport = CType(RetrieveData("ENQUIRY"), DataSet)
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(EnquiryData._V_CUSTOMER).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(EnquiryData._V_CUSTOMER), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "LEAK TEST"
                dsReport = CType(RetrieveData("LEAK_TEST_DOCUMENT"), DataSet)
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(LeakTestData._LEAK_TEST).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(LeakTestData._LEAK_TEST), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "INVOICE GENERATION"
                If strPageName = String.Empty Then
                    pvt_DatasourceCollection = CType(RetrieveData("INVOICE_GENERATION"), DataSet)
                Else
                    pvt_DatasourceCollection = CType(RetrieveData("VIEW_INVOICE"), DataSet)
                End If
            Case "PENDING INVOICE"
                pvt_DatasourceCollection = CType(RetrieveData("INVOICE_GENERATION"), DataSet)
            Case "REPAIR INVOICE"
                If strPageName = String.Empty Then
                    pvt_DatasourceCollection = CType(RetrieveData("INVOICE_GENERATION"), DataSet)
                Else
                    pvt_DatasourceCollection = CType(RetrieveData("VIEW_INVOICE"), DataSet)
                End If
            Case "CLEANING INVOICE"
                If strPageName = String.Empty Then
                    pvt_DatasourceCollection = CType(RetrieveData("INVOICE_GENERATION"), DataSet)
                Else
                    pvt_DatasourceCollection = CType(RetrieveData("VIEW_INVOICE"), DataSet)
                End If
            Case "MISCELLANEOUS INVOICE"
                If strPageName = String.Empty Then
                    pvt_DatasourceCollection = CType(RetrieveData("INVOICE_GENERATION"), DataSet)
                Else
                    pvt_DatasourceCollection = CType(RetrieveData("VIEW_INVOICE"), DataSet)
                End If

            Case "CREDIT NOTE"
                If strPageName = String.Empty Then
                    pvt_DatasourceCollection = CType(RetrieveData("INVOICE_GENERATION"), DataSet)
                Else
                    pvt_DatasourceCollection = CType(RetrieveData("VIEW_INVOICE"), DataSet)
                End If

            Case "HEATING INVOICE"
                If strPageName = String.Empty Then
                    pvt_DatasourceCollection = CType(RetrieveData("INVOICE_GENERATION"), DataSet)
                Else
                    pvt_DatasourceCollection = CType(RetrieveData("VIEW_INVOICE"), DataSet)
                End If
            Case "CLEANING INSTRUCTION"
                dsReport = CType(RetrieveData("CLEANING_INSTRUCTION"), DataSet)
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(CleaningData._CLEANING_INSPECTION).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(CleaningData._CLEANING_INSPECTION), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If

                    'Cleaning Inspection Reference No

                    If dsReport.Tables(CleaningData._CLEANING_INSPECTION).Rows(0).Item(CleaningData.CLNNG_INSPCTN_NO) Is DBNull.Value Then

                        Dim strEquipment As String = dsReport.Tables(CleaningData._CLEANING_INSPECTION).Rows(0).Item(CleaningData.EQPMNT_NO).ToString()

                        Dim objClean As New Cleaning
                        Dim strCleaningInspectionRefNo As String = objClean.UpdateActvity_CleaningInspectionRefNo(strEquipment)
                        dsReport.Tables(CleaningData._CLEANING_INSPECTION).Rows(0).Item(CleaningData.CLNNG_INSPCTN_NO) = strCleaningInspectionRefNo

                    End If

                End If
            Case "EQUIPMENT HISTORY"
                pvt_DatasourceCollection = CType(RetrieveData("EQUIPMENT_HISTORY"), DataSet)
            Case "TRANSPORTATION JOB ORDER"
                Dim objTransportation As New Transportation
                dsReport = objTransportation.pub_GetTransportation(ParamCollection)
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(TransportationData._V_TRANSPORTATION).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(TransportationData._V_TRANSPORTATION), TransportationData.CSTMR_ID, "", TransportationData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(TransportationData.CSTMR_ID)
                    End If
                End If
            Case "ROUTE ENQUIRY"
                dsReport = CType(RetrieveData("ENQUIRY"), DataSet)
                pvt_DatasourceCollection = dsReport
                If dsReport.Tables(EnquiryData._V_CUSTOMER_TRANSPORTATION).Rows.Count > 0 Then
                    Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
                    dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", dsReport.Tables(EnquiryData._V_CUSTOMER_TRANSPORTATION), CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
                    If dtDistinctCustomer.Rows.Count = 1 Then
                        bv_strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
                    End If
                End If
            Case "TRANSPORTATION INVOICE"
                If strPageName = String.Empty Then
                    pvt_DatasourceCollection = CType(RetrieveData("INVOICE_GENERATION"), DataSet)
                Else
                    pvt_DatasourceCollection = CType(RetrieveData("VIEW_INVOICE"), DataSet)
                End If
            Case "RENTAL INVOICE"
                If strPageName = String.Empty Then
                    pvt_DatasourceCollection = CType(RetrieveData("INVOICE_GENERATION"), DataSet)
                Else
                    pvt_DatasourceCollection = CType(RetrieveData("VIEW_INVOICE"), DataSet)
                End If
            Case "REPAIR SCHEDULE"
                Dim objSchedule As New Schedule
                Dim objCommon As New CommonData
                ParamCollection = String.Concat(ParamCollection, "&DEPOT=", objCommon.GetDepotID())
                dsReport = objSchedule.pub_GetRepairScheduleReport(ParamCollection)
                pvt_DatasourceCollection = dsReport
            Case "SURVEY SCHEDULE"
                Dim objSchedule As New Schedule
                Dim objCommon As New CommonData
                ParamCollection = String.Concat(ParamCollection, "&DEPOT=", objCommon.GetDepotID())
                dsReport = objSchedule.pub_GetSurveyScheduleReport(ParamCollection)
                pvt_DatasourceCollection = dsReport
            Case "CLEANING SCHEDULE"
                Dim objSchedule As New Schedule
                Dim objCommon As New CommonData
                ParamCollection = String.Concat(ParamCollection, "&DEPOT=", objCommon.GetDepotID())
                dsReport = objSchedule.pub_GetCleaningScheduleReport(ParamCollection)
                pvt_DatasourceCollection = dsReport
            Case "INSPECTION SCHEDULE"
                Dim objSchedule As New Schedule
                Dim objCommon As New CommonData
                ParamCollection = String.Concat(ParamCollection, "&DEPOT=", objCommon.GetDepotID())
                dsReport = objSchedule.pub_GetInspectionScheduleReport(ParamCollection)
                pvt_DatasourceCollection = dsReport
            Case "INSPECTION INVOICE"
                If strPageName = String.Empty Then
                    pvt_DatasourceCollection = CType(RetrieveData("INVOICE_GENERATION"), DataSet)
                Else
                    pvt_DatasourceCollection = CType(RetrieveData("VIEW_INVOICE"), DataSet)
                End If
            Case "EQUIPMENT HISTORY GWC"
                pvt_DatasourceCollection = CType(RetrieveData("EQUIPMENT_HISTORY"), DataSet)
        End Select
    End Sub
#End Region

#Region "Reports"
    Sub New(ByVal reportname As String, ByVal ParamCollection As String, ByVal IsReport As Boolean, ByVal bv_inDepotID As Integer, _
            ByVal bv_intReportID As Int32)
        Dim ObjCommon As New CommonUI
        Select Case reportname
            Case "Status"
                pvt_DatasourceCollection = ObjCommon.GetStatusReport(ParamCollection, bv_inDepotID, reportname, bv_intReportID)
            Case "Pending Inspection"
                pvt_DatasourceCollection = ObjCommon.GetPendingInspectionReport(ParamCollection, bv_inDepotID, reportname)
            Case "Inventory"
                pvt_DatasourceCollection = ObjCommon.GetStatusReport(ParamCollection, bv_inDepotID, reportname, bv_intReportID)
            Case "Equipment Activity"
                pvt_DatasourceCollection = ObjCommon.GetEquipemntActivityReport(ParamCollection, bv_inDepotID, reportname)
            Case "Gate Moves"
                pvt_DatasourceCollection = ObjCommon.GetGateMovesReport(ParamCollection, bv_inDepotID, reportname, bv_intReportID)
            Case "Equipment Yard Location"
                pvt_DatasourceCollection = ObjCommon.pub_GetEquipmentYardLocationReport(ParamCollection, bv_inDepotID, reportname)
            Case "Tank Test Detail"
                pvt_DatasourceCollection = ObjCommon.pub_GetTankTestDetailReport(ParamCollection, bv_inDepotID, reportname)
            Case "Heating"
                pvt_DatasourceCollection = ObjCommon.pub_GetHeatingReport(ParamCollection, bv_inDepotID, reportname)
            Case "Revenue - Heating"
                pvt_DatasourceCollection = ObjCommon.pub_GetHeatingRevenueReport(ParamCollection, bv_inDepotID, reportname)
            Case "Revenue - Cleaning"
                pvt_DatasourceCollection = ObjCommon.pub_GetCleaningRevenueReport(ParamCollection, bv_inDepotID, reportname)
            Case "Revenue - Repair"
                pvt_DatasourceCollection = ObjCommon.pub_GetRepairRevenueReport(ParamCollection, bv_inDepotID, reportname)
            Case "Cleaning Activity"
                pvt_DatasourceCollection = ObjCommon.pub_GetCleaningActivityReport(ParamCollection, bv_inDepotID, reportname)
            Case "Revenue - Invoice Register"
                pvt_DatasourceCollection = ObjCommon.pub_GetInvoiceRegisterReport(ParamCollection, bv_inDepotID, reportname)
            Case "Equipment Repair Status"
                pvt_DatasourceCollection = ObjCommon.pub_GetEquipmentRepairStatusReport(ParamCollection, bv_inDepotID, reportname)
            Case "Revenue - Pending Invoice Register"
                pvt_DatasourceCollection = ObjCommon.pub_GetPendingInvoiceRegisterReport(ParamCollection, bv_inDepotID, reportname)
            Case "KPI - Heating"
                pvt_DatasourceCollection = ObjCommon.pub_GetHeatingKPIReport(ParamCollection, bv_inDepotID, reportname)
            Case "KPI - Cleaning"
                pvt_DatasourceCollection = ObjCommon.pub_GetCleaningKPIReport(ParamCollection, bv_inDepotID, reportname)
            Case "KPI - Repair"
                pvt_DatasourceCollection = ObjCommon.pub_GetRepairKPIReport(ParamCollection, bv_inDepotID, reportname)
            Case "KPI - Repair Labor"
                pvt_DatasourceCollection = ObjCommon.pub_GetRepairLaborKPIReport(ParamCollection, bv_inDepotID, reportname)
            Case "KPI - Overall"
                pvt_DatasourceCollection = ObjCommon.GetOverallKPIReport(ParamCollection, bv_inDepotID, reportname)
            Case "Transportation"
                pvt_DatasourceCollection = ObjCommon.pub_GetTransportationDetails(ParamCollection, bv_inDepotID, reportname)
            Case "Revenue - Transportation"
                pvt_DatasourceCollection = ObjCommon.pub_GetTransportationRevenueReport(ParamCollection, bv_inDepotID, reportname)
            Case "KPI - Transportation"
                pvt_DatasourceCollection = ObjCommon.pub_GetTransportationKPIReport(ParamCollection, bv_inDepotID, reportname)
            Case "Rental"
                pvt_DatasourceCollection = ObjCommon.pub_GetRentalDetails(ParamCollection, bv_inDepotID, reportname)
            Case "KPI - Rental"
                pvt_DatasourceCollection = ObjCommon.pub_GetRentalKPIDetails(ParamCollection, bv_inDepotID, reportname)
            Case "Revenue - Rental"
                pvt_DatasourceCollection = ObjCommon.pub_GetRentalRevenueReport(ParamCollection, bv_inDepotID, reportname)
            Case "Revenue - Handling and Storage"
                pvt_DatasourceCollection = ObjCommon.pub_GetHandlingAndStorageRevenueReport(ParamCollection, bv_inDepotID, reportname)
            Case "Available Units"
                pvt_DatasourceCollection = ObjCommon.GetAvailableUnitReport(ParamCollection, bv_inDepotID, reportname)
            Case "Awaiting Estimates"
                pvt_DatasourceCollection = ObjCommon.GetAvailableUnitReport(ParamCollection, bv_inDepotID, reportname)
            Case "Revenue - Inspection"
                pvt_DatasourceCollection = ObjCommon.pub_GetInspectionRevenueReport(ParamCollection, bv_inDepotID, reportname)
            Case "Invoice Cancellation"
                pvt_DatasourceCollection = ObjCommon.pub_GetInvoiceCancelReport(ParamCollection, bv_inDepotID, reportname)
        End Select
    End Sub
#End Region

    Sub New()

    End Sub

#Region "Property"
    Public ReadOnly Property pub_GetFileName() As String
        Get
            Return pvt_filename
        End Get
    End Property

    Public ReadOnly Property pub_GetDatasource() As Data.DataSet
        Get
            Return pvt_DatasourceCollection
        End Get
    End Property
#End Region


#Region "GetDynamicReportParameters"
    Public Function GetDynamicReportParameters(ByVal bv_dt As Data.DataTable, ByVal bv_ds As Data.DataSet, ByVal bv_username As String, Optional ByVal bv_intReportID As Integer = 0) As Microsoft.Reporting.WebForms.ReportParameter()

        Dim dsParameterMasters As Data.DataSet
        Dim dtParameterLists As Data.DataTable

        dsParameterMasters = bv_ds
        dtParameterLists = bv_dt

        Dim strLogoURL As String
        strLogoURL = ConfigurationManager.AppSettings("ReportLogoURL")
        strLogoURL = pvt_GetPathFromRoot(strLogoURL)

        If Not dtParameterLists Is Nothing AndAlso dtParameterLists.Rows.Count > 0 Then
            Dim reportParamArray(dtParameterLists.Rows.Count - 1) As Microsoft.Reporting.WebForms.ReportParameter
            Dim paramvalue As String
            For icount As Integer = 0 To dtParameterLists.Rows.Count - 1
                Dim blnApplyParam As Boolean = True
                Dim dsAddDynamicReport As AddDynamicReportDataSet
                If bv_intReportID <> 0 Then
                    dsAddDynamicReport = CType(RetrieveData(PARAMETERDATA + CStr(bv_intReportID)), AddDynamicReportDataSet)
                    If Not dsAddDynamicReport Is Nothing AndAlso dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Rows.Count > 0 Then
                        Dim drColumnName As DataRow()
                        drColumnName = dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Select(String.Concat(AddDynamicReportData.PRMTR_NAM, "='", dtParameterLists.Rows(icount).Item("parameter").ToString(), "'"))
                        If drColumnName.Length > 0 Then
                            blnApplyParam = CBool(drColumnName(0).Item(AddDynamicReportData.PRMTR_OPT))
                        End If
                    End If
                End If
                Select Case CType(dtParameterLists.Rows(icount)("type"), String)
                    Case "date", "title", "string", "integer"
                        If dtParameterLists.Rows(icount)("parametervalue") Is DBNull.Value Then
                            paramvalue = Nothing
                        Else
                            paramvalue = CStr(dtParameterLists.Rows(icount)("parametervalue"))
                        End If
                        reportParamArray(icount) = New Microsoft.Reporting.WebForms.ReportParameter(dtParameterLists.Rows(icount).Item("parameter").ToString(), paramvalue)
                        'Case "date" 
                        '    If dtParameterLists.Rows(icount)("parametervalue") Is DBNull.Value Then
                        '        paramvalue = Nothing
                        '    Else
                        '        paramvalue = pvt_GetDate(CDate(dtParameterLists.Rows(icount)("parametervalue")), bv_timediff)
                        '    End If

                        '    reportParamArray(icount) = New Microsoft.Reporting.WebForms.ReportParameter(dtParameterLists.Rows(icount).Item("parameter"), paramvalue)
                    Case "dropdown"
                        If dtParameterLists.Rows(icount)("parametervalue") Is DBNull.Value OrElse dtParameterLists.Rows(icount)("parametervalue").ToString().ToLower() = "select" Then
                            paramvalue = Nothing
                        Else
                            paramvalue = CStr(dtParameterLists.Rows(icount)("parametervalue"))
                        End If
                        Dim iParamcount As Integer = 0
                        'Case "date" 
                        '    If dtParameterLists.Rows(icount)("parametervalue") Is DBNull.Value Then
                        '        paramvalue = Nothing
                        '    Else
                        '        paramvalue = pvt_GetDate(CDate(dtParameterLists.Rows(icount)("parametervalue")), bv_timediff)
                        '    End If

                        '    reportParamArray(icount) = New Microsoft.Reporting.WebForms.ReportParameter(dtParameterLists.Rows(icount).Item("parameter"), paramvalue)


                        If (paramvalue Is Nothing OrElse paramvalue = "") AndAlso Not blnApplyParam Then
                            If Not dsAddDynamicReport Is Nothing Then
                                iParamcount = dsParameterMasters.Tables(dtParameterLists.Rows(icount).Item("parameter").ToString()).Rows.Count
                            End If
                        End If
                        Dim strSelectedParam(iParamcount) As String


                        If (paramvalue Is Nothing OrElse paramvalue = "") AndAlso Not blnApplyParam Then  ' Apply all Values for parameters
                            For intTemp As Integer = 0 To dsParameterMasters.Tables(dtParameterLists.Rows(icount).Item("parameter").ToString()).Rows.Count - 1
                                strSelectedParam(intTemp) = dsParameterMasters.Tables(dtParameterLists.Rows(icount).Item("parameter").ToString()).Rows(intTemp)("ID").ToString()
                            Next
                            reportParamArray(icount) = New Microsoft.Reporting.WebForms.ReportParameter(dtParameterLists.Rows(icount).Item("parameter").ToString(), strSelectedParam)

                        Else
                            reportParamArray(icount) = New Microsoft.Reporting.WebForms.ReportParameter(dtParameterLists.Rows(icount).Item("parameter").ToString(), paramvalue)

                        End If



                    Case "master"
                        If dsParameterMasters.Tables(dtParameterLists.Rows(icount).Item("parameter").ToString()).Rows.Count > 0 Then
                            Dim drSelected() As Data.DataRow = dsParameterMasters.Tables(dtParameterLists.Rows(icount).Item("parameter").ToString()).Select("Select = 1")
                            Dim iParamcount As Integer = 0

                            If drSelected.Length = 0 AndAlso Not blnApplyParam Then
                                If Not dsAddDynamicReport Is Nothing Then
                                    iParamcount = dsParameterMasters.Tables(dtParameterLists.Rows(icount).Item("parameter").ToString()).Rows.Count
                                End If
                            Else
                                iParamcount = drSelected.Length
                            End If
                            Dim strSelectedParam(iParamcount) As String


                            If drSelected.Length = 0 AndAlso Not blnApplyParam Then  ' Apply all Values for parameters
                                For intTemp As Integer = 0 To dsParameterMasters.Tables(dtParameterLists.Rows(icount).Item("parameter").ToString()).Rows.Count - 1
                                    strSelectedParam(intTemp) = dsParameterMasters.Tables(dtParameterLists.Rows(icount).Item("parameter").ToString()).Rows(intTemp)("ID").ToString()
                                Next
                            Else
                                For intTemp As Integer = 0 To drSelected.Length - 1
                                    strSelectedParam(intTemp) = drSelected(intTemp)("ID").ToString()
                                Next
                            End If

                            reportParamArray(icount) = New Microsoft.Reporting.WebForms.ReportParameter(dtParameterLists.Rows(icount).Item("parameter").ToString(), strSelectedParam)
                        End If
                End Select
            Next
            GetDynamicReportParameters = reportParamArray
        Else
            Dim reportParamArray(1) As Microsoft.Reporting.WebForms.ReportParameter

            GetDynamicReportParameters = reportParamArray
        End If

    End Function
#End Region

#Region "GetReportParameters"
    Public Function GetReportParameters(ByVal bv_dt As Data.DataTable, ByVal bv_ds As Data.DataSet, ByVal bv_username As String) As Microsoft.Reporting.WebForms.ReportParameter()
        Try
            Dim dsParameterMasters As Data.DataSet
            Dim dtParameterLists As Data.DataTable

            dsParameterMasters = bv_ds
            dtParameterLists = bv_dt

            Dim strLogoURL As String
            strLogoURL = ConfigurationManager.AppSettings("ReportLogoURL")
            strLogoURL = pvt_GetPathFromRoot(strLogoURL)

            If Not dtParameterLists Is Nothing AndAlso dtParameterLists.Rows.Count > 0 Then

                Dim reportParamArray(dtParameterLists.Rows.Count + 1) As Microsoft.Reporting.WebForms.ReportParameter
                Dim paramvalue As String
                For icount As Integer = 0 To dtParameterLists.Rows.Count - 1
                    Select Case dtParameterLists.Rows(icount)("type")
                        Case "date", "title"
                            If dtParameterLists.Rows(icount)("parametervalue") Is DBNull.Value Then
                                paramvalue = Nothing
                            Else
                                paramvalue = dtParameterLists.Rows(icount)("parametervalue")
                            End If

                            reportParamArray(icount) = New Microsoft.Reporting.WebForms.ReportParameter(dtParameterLists.Rows(icount).Item("parameter"), paramvalue)
                            'Case "date" 
                            '    If dtParameterLists.Rows(icount)("parametervalue") Is DBNull.Value Then
                            '        paramvalue = Nothing
                            '    Else
                            '        paramvalue = pvt_GetDate(CDate(dtParameterLists.Rows(icount)("parametervalue")), bv_timediff)
                            '    End If

                            '    reportParamArray(icount) = New Microsoft.Reporting.WebForms.ReportParameter(dtParameterLists.Rows(icount).Item("parameter"), paramvalue)
                        Case "master"
                            Dim drSelected() As Data.DataRow = dsParameterMasters.Tables(dtParameterLists.Rows(icount).Item("parameter")).select("Select = 1")
                            Dim strSelectedParam(drSelected.Length) As String

                            For intTemp As Integer = 0 To drSelected.Length - 1
                                strSelectedParam(intTemp) = drSelected(intTemp)("ID")
                            Next
                            reportParamArray(icount) = New Microsoft.Reporting.WebForms.ReportParameter(dtParameterLists.Rows(icount).Item("parameter"), strSelectedParam)
                    End Select
                Next

                reportParamArray(dtParameterLists.Rows.Count) = New Microsoft.Reporting.WebForms.ReportParameter("logourl", strLogoURL)
                reportParamArray(dtParameterLists.Rows.Count + 1) = New Microsoft.Reporting.WebForms.ReportParameter("username", bv_username)

                GetReportParameters = reportParamArray

            Else
                Dim reportParamArray(1) As Microsoft.Reporting.WebForms.ReportParameter

                reportParamArray(0) = New Microsoft.Reporting.WebForms.ReportParameter("logourl", strLogoURL)
                reportParamArray(1) = New Microsoft.Reporting.WebForms.ReportParameter("username", bv_username)

                GetReportParameters = reportParamArray
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
#End Region

#Region "pvt_GetDate"
    Private Function pvt_GetDate(ByVal bv_date As DateTime, ByVal bv_timediff As Double) As Date
        Try
            bv_date = DateAdd(DateInterval.Minute, bv_timediff, bv_date)
            bv_date = DateAdd(DateInterval.Minute, DateDiff(DateInterval.Minute, Date.UtcNow, Date.Now), bv_date)
            Return bv_date
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
#End Region

#Region "pvt_GetPathFromRoot"
    Public Function pvt_GetPathFromRoot(ByVal strFolderPath As String) As String
        Try
            Dim AppPath As String = HttpContext.Current.Request.PathInfo
            Dim pathToReturn As New StringBuilder("http://")

            pathToReturn.Append(HttpContext.Current.Request.Url.Host)
            If Not HttpContext.Current.Request.Url.IsDefaultPort Then
                pathToReturn.Append(":")
                pathToReturn.Append(HttpContext.Current.Request.Url.Port)
            End If
            pathToReturn.Append(HttpContext.Current.Request.ApplicationPath)

            If Not strFolderPath.StartsWith("/") And Not strFolderPath.EndsWith("/") Then
                pathToReturn.Append("/")
            End If
            pathToReturn.Append(strFolderPath)
            Return pathToReturn.ToString
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_getCustomerName"

    Private Function pvt_getCustomerName(ByVal bv_dtCommonTable As DataTable) As String
        Try
            Dim dsReport As New DataSet
            Dim dtDistinctCustomer As New DataTable
            Dim strCustomer As String = String.Empty
            Dim strTableName As String = String.Empty
            strTableName = bv_dtCommonTable.TableName
            Dim Distinctdata As New DatasetHelpers(CType(dsReport, DataSet))
            dtDistinctCustomer = Distinctdata.SelectGroupByInto("DISTINCT", bv_dtCommonTable, CommonUIData.CSTMR_ID, "", CommonUIData.CSTMR_ID)
            If dtDistinctCustomer.Rows.Count = 1 Then
                strCustomer = dtDistinctCustomer.Rows(0).Item(CommonUIData.CSTMR_ID)
            End If
            Return strCustomer
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region

#Region "pvt_getAcaciaSpecificConfig"
    Private Function pvt_getAcaciaSpecificConfig() As Boolean
        Try
            Dim str_071AcaciaBit As String
            Dim bln_071Acacia_Key As Boolean
            Dim objCommonConfig As New ConfigSetting
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            str_071AcaciaBit = objCommonConfig.pub_GetConfigSingleValue("071", intDPT_ID)
            bln_071Acacia_Key = objCommonConfig.IsKeyExists
            If bln_071Acacia_Key Then
                If str_071AcaciaBit.ToLower = "true" Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
