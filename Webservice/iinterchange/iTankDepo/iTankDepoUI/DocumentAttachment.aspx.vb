
Partial Class DocumentAttachment
    Inherits Framebase

#Region "Declarations"
    'DataSet
    Dim dsRepairEstimate As New RepairEstimateDataSet
    Dim dsRepairCompletion As New RepairCompletionDataSet
    Dim dsEquipmentInformationData As New EquipmentInformationDataSet
    Dim dsGateInData As New GateinDataSet
    Dim dsProduct As New ProductDataSet

    'Session Variable
    Private Const REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"
    Private Const REPAIR_COMPLETION As String = "REPAIR_COMPLETION"
    Private Const COMMON As String = "COMMON"
    Private Const EQUIPMENT_INFORMATION As String = "EQUIPMENT_INFORMATION"
    Private Const GATE_IN As String = "GATE_IN"
    Private Const Product As String = "Product"

    'Configuration
    Private strProductSize As String = ConfigurationSettings.AppSettings("UploadProductSize")
    Private strProductFileLength As String = ConfigurationSettings.AppSettings("UploadProductFileLength")

#End Region

#Region "Page_PreRender1()"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/DocumentAttachment.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/swfobject.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_Load1()"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
