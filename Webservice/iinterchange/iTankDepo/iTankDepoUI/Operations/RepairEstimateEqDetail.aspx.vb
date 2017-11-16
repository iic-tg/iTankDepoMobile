
Partial Class Operations_RepairEstimateEqDetail
    Inherits Pagebase

    Dim dsRepairEstimate As New RepairEstimateDataSet
    Dim dtRepairEstimate As DataTable
    Private Const REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"


#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                If Not (Request.QueryString("EquipmentNo") Is Nothing And Request.QueryString("GateinTransaction") Is Nothing) Then
                    bindData(Request.QueryString("EquipmentNo").ToString, Request.QueryString("GateinTransaction").ToString)
                End If
                Dim objCommon As New CommonData
                Dim bln_061Key As Boolean
                Dim str_061Value As String
                str_061Value = objCommon.GetConfigSetting("061", bln_061Key)
                If bln_061Key Then
                    If str_061Value.ToLower = "true" Then
                        lblprevCargo.Visible = False
                        txtPreviousCargo.Visible = False
                        lblCapacity.Text = "Max Payload"
                    Else
                        txtLastSurveyor.Visible = False
                        lblLastSurveyor.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "bindData"
    Private Sub bindData(ByVal bv_strEquipmentNo As String, ByVal bv_strGateinTransaction As String)
        Try
            Dim objCommon As New CommonData
            Dim dtEquipmentDetail As DataTable
            Dim dtEquipmentInformation As New DataTable
            Dim objRepairEstimate As New RepairEstimate
            dtEquipmentDetail = objRepairEstimate.pub_GetGetActivityStatusByEqpmntNo(CommonWeb.iInt(objCommon.GetDepotID), bv_strEquipmentNo, bv_strGateinTransaction).Tables(RepairEstimateData._V_ACTIVITY_STATUS)
            If dtEquipmentDetail.Rows.Count > 0 Then
                txtCode.Text = dtEquipmentDetail.Rows(0).Item(RepairEstimateData.EQPMNT_CD_CD).ToString
                txtType.Text = dtEquipmentDetail.Rows(0).Item(RepairEstimateData.EQPMNT_TYP_CD).ToString
                txtPreviousCargo.Text = dtEquipmentDetail.Rows(0).Item(RepairEstimateData.PRDCT_DSCRPTN_VC).ToString
            End If
            dtEquipmentDetail = objRepairEstimate.pub_GetEquipmentInformationByEqpmntNo(CommonWeb.iInt(objCommon.GetDepotID), bv_strEquipmentNo).Tables(RepairEstimateData._V_EQUIPMENT_INFORMATION)
            If dtEquipmentDetail.Rows.Count > 0 Then
                txtTareWeight.Text = dtEquipmentDetail.Rows(0).Item(RepairEstimateData.TR_WGHT_NC).ToString
                txtGrossWeight.Text = dtEquipmentDetail.Rows(0).Item(RepairEstimateData.GRSS_WGHT_NC).ToString
                txtCapacity.Text = dtEquipmentDetail.Rows(0).Item(RepairEstimateData.CPCTY_NC).ToString
                txtLastSurveyor.Text = dtEquipmentDetail.Rows(0).Item(RepairEstimateData.LST_SRVYR_NM).ToString
                If dtEquipmentDetail.Rows(0).Item(RepairEstimateData.MNFCTR_DT).ToString <> Nothing AndAlso dtEquipmentDetail.Rows(0).Item(RepairEstimateData.MNFCTR_DT).ToString <> String.Empty Then
                    txtMfgDate.Text = CDate(dtEquipmentDetail.Rows(0).Item(RepairEstimateData.MNFCTR_DT)).ToString("dd-MMM-yyyy").ToUpper
                Else
                    txtMfgDate.Text = ""
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class
