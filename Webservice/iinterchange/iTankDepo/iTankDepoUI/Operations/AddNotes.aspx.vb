
Partial Class Operations_AddNotes
    Inherits Pagebase
    Dim dsGateInData As New GateinDataSet
    Dim dtGateinData As DataTable
    Private Const GATE_IN As String = "GATE_IN"
    Dim dsRepairEstimate As New RepairEstimateDataSet
    Dim dtRepairEstimate As DataTable
    Private Const REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"
#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack AndAlso Not Page.IsCallback Then
                If Not Request.QueryString("GateInBin") Is Nothing Then
                    Dim drAGateIn As DataRow()
                    dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
                    drAGateIn = dsGateInData.Tables(GateinData._V_GATEIN).Select(String.Concat(GateinData.GTN_BIN, "='", Request.QueryString("GateInBin"), "'"))
                    If drAGateIn.Length > 0 Then
                        pvt_BindData(drAGateIn(0))
                    End If
                    hdnID.Value = Request.QueryString("GateInBin")
                    divDescription.Visible = False
                Else
                    If Not Request.QueryString("DetailBin") Is Nothing Then
                        dsRepairEstimate = RetrieveData(REPAIR_ESTIMATE)
                        Dim drARepair As DataRow()
                        drARepair = dsRepairEstimate.Tables(RepairEstimateData._REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairEstimateData.RPR_ESTMT_DTL_ID, "='", Request.QueryString("DetailBin"), "'"))
                        If drARepair.Length > 0 Then
                            lblDescription.InnerText = drARepair(0).Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString
                        End If
                        divNotes.Visible = False
                        divDescription.Visible = True
                    End If
                End If
            End If
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_BindData"
    Private Sub pvt_BindData(ByVal bv_drGateIn As DataRow)
        Try
            If Not IsDBNull(bv_drGateIn.Item(GateinData.NT_1_VC)) Then
                txtNotes1.Text = CStr(bv_drGateIn.Item(GateinData.NT_1_VC)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.NT_2_VC)) Then
                txtNotes2.Text = CStr(bv_drGateIn.Item(GateinData.NT_2_VC)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.NT_3_VC)) Then
                txtNotes3.Text = CStr(bv_drGateIn.Item(GateinData.NT_3_VC)).ToString
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"

    Private Sub pvt_SetChangesMade()
      
        CommonWeb.pub_AttachHasChanges(txtNotes1)
        CommonWeb.pub_AttachHasChanges(txtNotes2)
        CommonWeb.pub_AttachHasChanges(txtNotes3)
        
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "submit"
                pvt_Submit(e.GetCallbackValue("Notes1"), _
                                e.GetCallbackValue("Notes2"), _
                                e.GetCallbackValue("Notes3"), e.GetCallbackValue("GateInId"))
        End Select
    End Sub
#End Region

#Region "pvt_Submit"
    Protected Sub pvt_Submit(ByVal bv_strNotes1 As String, ByVal bv_strNotes2 As String, ByVal bv_strNotes3 As String, ByVal bv_lngGateinId As String)
        Try
            dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
            Dim drAGateIn As DataRow()
            drAGateIn = dsGateInData.Tables(GateinData._V_GATEIN).Select(String.Concat(GateinData.GTN_BIN, "='", CLng(bv_lngGateinId), "'"))
            If drAGateIn.Length > 0 Then
                If Not bv_strNotes1 Is Nothing Then
                    drAGateIn(0).Item(GateinData.NT_1_VC) = bv_strNotes1.Trim()
                Else
                    drAGateIn(0).Item(GateinData.NT_1_VC) = DBNull.Value
                End If
                If Not bv_strNotes2 Is Nothing Then
                    drAGateIn(0).Item(GateinData.NT_2_VC) = bv_strNotes2.Trim()
                Else
                    drAGateIn(0).Item(GateinData.NT_2_VC) = DBNull.Value
                End If
                If Not bv_strNotes3 Is Nothing Then
                    drAGateIn(0).Item(GateinData.NT_3_VC) = bv_strNotes3.Trim()
                Else
                    drAGateIn(0).Item(GateinData.NT_3_VC) = DBNull.Value
                End If
            End If
            CacheData(GATE_IN, dsGateInData)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
