Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class ReportParams

    Dim ds As ReportParamDataSet

#Region "Constructor.."

    Sub New()
        ds = New ReportParamDataSet
    End Sub

#End Region

#Region "Declaration Part.. "
    Private Const Report_HeaderSelectQueryPk As String = "SELECT RPRT_HDR_ID,RPRT_TTLE,PRMTR_VL,ACTVTY_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM REPORT_HEADER WHERE RPRT_HDR_ID=@RPRT_HDR_ID"
    Private Const Report_HeaderSelectQueryByReportTitle As String = "SELECT Count(RPRT_HDR_ID) FROM REPORT_HEADER WHERE RPRT_TTLE=@RPRT_TTLE AND ACTVTY_ID=@ACTVTY_ID AND DPT_ID=@DPT_ID"
    Private Const Report_HeaderInsertQuery As String = "INSERT INTO REPORT_HEADER(RPRT_HDR_ID,RPRT_TTLE,PRMTR_VL,ACTVTY_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID)VALUES(@RPRT_HDR_ID,@RPRT_TTLE,@PRMTR_VL,@ACTVTY_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@DPT_ID)"
    Private Const Report_HeaderUpdateQuery As String = "UPDATE REPORT_HEADER SET RPRT_TTLE=@RPRT_TTLE, PRMTR_VL=@PRMTR_VL, ACTVTY_ID=@ACTVTY_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE RPRT_HDR_ID=@RPRT_HDR_ID AND DPT_ID=@DPT_ID"
    Private Const ReportNameSelectQueryByActivityID As String = "SELECT ACTVTY_NAM FROM ACTIVITY WHERE ACTVTY_ID=@ACTVTY_ID"
    Dim objData As DataObjects
#End Region

#Region "GetParamTable"
    Public Function GetParamTable(ByVal bv_strQuery As String, ByVal bv_StrTableName As String) As ReportParamDataSet
        Try
            objData = New DataObjects(bv_strQuery)
            ds = New ReportParamDataSet
            ds.Tables.Add(bv_StrTableName)
            objData.Fill(ds.Tables(bv_StrTableName))
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetReport_HeaderByrprt_hdr_id() TABLE NAME:REPORT_HEADER"
    Public Function GetReport_HeaderByReportHeaderID(ByVal bv_rprt_hdr_id As Int32) As ReportParamDataSet
        objData = New DataObjects(Report_HeaderSelectQueryPk, ReportParamData.RPRT_HDR_ID, CStr(bv_rprt_hdr_id))
        objData.Fill(CType(ds, DataSet), ReportParamData._REPORT_HEADER)
        Return ds
    End Function
#End Region

#Region "GetReport_HeaderbyReportTitle() TABLE NAME:Report_Header"
    Public Function ValidateReport_HeaderbyReportTitle(ByVal bv_strReportTitle As String, ByVal bv_int32ActivityId As Int32, ByVal bv_int32DepotId As String) As Integer
        Dim hshTable As New Hashtable
        hshTable.Add(ReportParamData.RPRT_TTLE, bv_strReportTitle)
        hshTable.Add(ReportParamData.ACTVTY_ID, bv_int32ActivityId)
        hshTable.Add(ReportParamData.DPT_ID, bv_int32DepotId)
        objData = New DataObjects(Report_HeaderSelectQueryByReportTitle, hshTable)
        Return objData.ExecuteScalar()
    End Function
#End Region


#Region "GetReportNamebyReportID() TABLE NAME:Activity"
    Public Function GetReportNamebyReportID(ByVal bv_int32ActivityId As Int32) As String
        objData = New DataObjects(ReportNameSelectQueryByActivityID, ReportParamData.ACTVTY_ID, bv_int32ActivityId)
        Return objData.ExecuteScalar()
    End Function
#End Region

#Region "CreateReport_Header() TABLE NAME:Report_Header"
    Public Function CreateReport_Header(ByVal bv_strReportTitle As String, _
                                        ByVal bv_ParameterValue As Byte(), _
                                        ByVal bv_i32ActivityID As Int32, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_i32DepotID As Int32) As Long
        Dim dr As DataRow
        Dim intMax As Long
        objData = New DataObjects()
        Dim insQry As String
        dr = ds.Tables(ReportParamData._REPORT_HEADER).NewRow()
        With dr
            intMax = CommonUIs.GetIdentityValue(ReportParamData._REPORT_HEADER)
            .Item(ReportParamData.RPRT_HDR_ID) = intMax
            .Item(ReportParamData.RPRT_TTLE) = bv_strReportTitle
            .Item(ReportParamData.PRMTR_VL) = bv_ParameterValue
            .Item(ReportParamData.ACTVTY_ID) = bv_i32ActivityID
            .Item(ReportParamData.CRTD_BY) = bv_strModifiedBy
            .Item(ReportParamData.CRTD_DT) = bv_datModifiedDate
            .Item(ReportParamData.MDFD_BY) = bv_strModifiedBy
            .Item(ReportParamData.MDFD_DT) = bv_datModifiedDate
            .Item(ReportParamData.DPT_ID) = bv_i32DepotID
        End With
        insQry = Report_HeaderInsertQuery
        objData.InsertRow(dr, insQry)
        dr = Nothing
        CreateReport_Header = intMax
    End Function

#End Region

#Region "UpdateReport_Header() TABLE NAME:Report_Header"

    Public Function UpdateReport_Header(ByVal bv_i32ReportHeaderID As Int32, _
                                        ByVal bv_strReportTitle As String, _
                                        ByVal bv_ParameterValue As Byte(), _
                                        ByVal bv_i32ActivityID As Int32, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_i32DepotID As Int32) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        dr = ds.Tables(ReportParamData._REPORT_HEADER).NewRow()
        With dr
            .Item(ReportParamData.RPRT_HDR_ID) = bv_i32ReportHeaderID
            .Item(ReportParamData.RPRT_TTLE) = bv_strReportTitle
            .Item(ReportParamData.PRMTR_VL) = bv_ParameterValue
            .Item(ReportParamData.ACTVTY_ID) = bv_i32ActivityID
            .Item(ReportParamData.MDFD_BY) = bv_strModifiedBy
            .Item(ReportParamData.MDFD_DT) = bv_datModifiedDate
            .Item(ReportParamData.DPT_ID) = bv_i32DepotID
        End With
        UpdateReport_Header = objData.UpdateRow(dr, Report_HeaderUpdateQuery)
        dr = Nothing
    End Function
#End Region

End Class
