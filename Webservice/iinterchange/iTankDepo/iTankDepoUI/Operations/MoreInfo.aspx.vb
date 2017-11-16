Option Strict On
Partial Class Operations_MoreInfo
    Inherits Pagebase
    Dim dsGateInData As New GateinDataSet
    Dim dtGateinData As DataTable
    Private Const GATE_IN As String = "GATE_IN"
    Private Const GATE_OUT As String = "GATE_OUT"
    Dim dsGateOutData As New GateOutDataSet

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Operations/MoreInfo.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim dsMoreInfo As New DataSet
            Dim drAGateIn As DataRow()
            Dim objCommondata As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
            Dim bln_051GwsBit As Boolean
            Dim str_051GWSKey As String
            str_051GWSKey = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
            bln_051GwsBit = objCommonConfig.IsKeyExists
            If bln_051GwsBit Then
                If str_051GWSKey.ToLower = "true" Then
                    lkpParty1.Visible = False
                    lkpParty2.Visible = False
                    lkpParty3.Visible = False
                Else
                    lkpPartyMaster1.Visible = False
                    lkpPartyMaster2.Visible = False
                    lkpPartyMaster3.Visible = False
                End If
            End If
            If Not Page.IsPostBack AndAlso Not Page.IsCallback Then
                If Not Request.QueryString("GateInId") Is Nothing Then
                    If Not Request.QueryString("GateOutEquipmentNo") Is Nothing Then
                        Dim strGateout As String = String.Empty
                        strGateout = (Request.QueryString("GateOutEquipmentNo")).ToString
                        dsGateOutData = CType(RetrieveData(GATE_OUT), GateOutDataSet)
                        txtEquipmentNo.Text = CStr(Request.QueryString("GateOutEquipmentNo"))
                        drAGateIn = dsGateOutData.Tables(GateinData._V_GATEIN_DETAIL).Select(String.Concat(GateinData.GTN_ID, "=", Request.QueryString("GateInId")))
                        If drAGateIn.Length > 0 Then
                            pvt_BindData(drAGateIn(0), str_051GWSKey)
                        End If
                        hdnID.Value = Request.QueryString("GateInId")
                        txtDateofManf.ReadOnly = True
                        txtAcep.ReadOnly = True
                        lkpMaterialCode.ReadOnly = True
                        txtGrossWeight.ReadOnly = True
                        txtTareWeight.ReadOnly = True
                        lkpMeasure.ReadOnly = True
                        lkpUnits.ReadOnly = True
                        txtOnhireLoc.ReadOnly = True
                        datOnhireDate.ReadOnly = True
                        txtTruckerCode.ReadOnly = True
                        lkpLoadStatus.ReadOnly = True
                        lkpCountry.ReadOnly = True
                        txtState.ReadOnly = True
                        txtNumber.ReadOnly = True
                        txtExpiry.ReadOnly = True
                        txtNotes1.ReadOnly = True
                        txtNotes2.ReadOnly = True
                        txtNotes3.ReadOnly = True
                        txtNotes4.ReadOnly = True
                        txtNotes5.ReadOnly = True
                        lkpParty1.ReadOnly = True
                        txtNumber1.ReadOnly = True
                        lkpParty2.ReadOnly = True
                        txtNumber2.ReadOnly = True
                        lkpParty3.ReadOnly = True
                        txtNumber3.ReadOnly = True
                        txtPortFuncCode.ReadOnly = True
                        txtPortLocCode.ReadOnly = True
                        txtVoyageNumber.ReadOnly = True
                        txtPortName.ReadOnly = True
                        txtPortnumber.ReadOnly = True
                        txtVesselIdCode.ReadOnly = True
                        txtShipper.ReadOnly = True
                        txtRailId.ReadOnly = True
                        txtRailRamp.ReadOnly = True
                        txthazCode.ReadOnly = True
                        txthazDesc.ReadOnly = True
                        txtVesselName.ReadOnly = True
                        PageSubmitPane.Visible = False
                        dvButton.Visible = False
                    Else
                        txtDateofManf.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                        dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
                        txtEquipmentNo.Text = CStr(Request.QueryString("EquipmentNo"))

                        Dim objGateIn As New Gatein
                        Dim objcommon As New CommonData()
                        'Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
                        dsMoreInfo = objGateIn.pub_V_GateinDetail(CLng(Request.QueryString("GateInId")), CStr(Request.QueryString("EquipmentNo")), intDepotID)
                        dsGateInData.Merge(dsMoreInfo.Tables(GateinData._V_GATEIN_DETAIL))

                        drAGateIn = dsGateInData.Tables(GateinData._V_GATEIN_DETAIL).Select(String.Concat(GateinData.GTN_ID, "=", Request.QueryString("GateInId")))
                        If drAGateIn.Length > 0 Then
                            pvt_BindData(drAGateIn(0), str_051GWSKey)
                        End If
                        hdnID.Value = Request.QueryString("GateInId")
                    End If
                Else
                    dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
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
    Private Sub pvt_BindData(ByVal bv_drGateIn As DataRow, ByVal bv_str051Key As String)
        Try
            If Not IsDBNull(bv_drGateIn.Item(GateinData.MTRL_CD)) Then
                lkpMaterialCode.Text = bv_drGateIn.Item(GateinData.MTRL_CD).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.MSR_CD)) Then
                lkpMeasure.Text = bv_drGateIn.Item(GateinData.MSR_CD).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.MNFCTR_DT)) Then
                txtDateofManf.Text = CDate(bv_drGateIn.Item(GateinData.MNFCTR_DT)).ToString("dd-MMM-yyyy").ToUpper
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.ACEP_CD)) Then
                txtAcep.Text = bv_drGateIn.Item(GateinData.ACEP_CD).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.UNT_ID)) Then
                lkpUnits.Text = bv_drGateIn.Item(GateinData.UNT_ID).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.UNT_CD)) Then
                lkpUnits.Text = bv_drGateIn.Item(GateinData.UNT_CD).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.LST_OH_LOC)) Then
                txtOnhireLoc.Text = bv_drGateIn.Item(GateinData.LST_OH_LOC).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.LST_OH_DT)) Then
                datOnhireDate.Text = CDate((bv_drGateIn.Item(GateinData.LST_OH_DT))).ToString("dd-MMM-yyyy").ToUpper
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.TRCKR_CD)) Then
                txtTruckerCode.Text = CStr(bv_drGateIn.Item(GateinData.TRCKR_CD)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.LOD_STTS_CD)) Then
                lkpLoadStatus.Text = CStr(bv_drGateIn.Item(GateinData.LOD_STTS_CD)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.CNTRY_CD)) Then
                lkpCountry.Text = CStr(bv_drGateIn.Item(GateinData.CNTRY_CD)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.LIC_STT)) Then
                txtState.Text = CStr(bv_drGateIn.Item(GateinData.LIC_STT)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.LIC_REG)) Then
                txtNumber.Text = CStr(bv_drGateIn.Item(GateinData.LIC_REG)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.LIC_EXPR)) Then
                txtExpiry.Text = CDate(bv_drGateIn.Item(GateinData.LIC_EXPR)).ToString("dd-MMM-yyyy").ToUpper
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.NT_1_VC)) Then
                txtNotes1.Text = CStr(bv_drGateIn.Item(GateinData.NT_1_VC)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.NT_2_VC)) Then
                txtNotes2.Text = CStr(bv_drGateIn.Item(GateinData.NT_2_VC)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.NT_3_VC)) Then
                txtNotes3.Text = CStr(bv_drGateIn.Item(GateinData.NT_3_VC)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.NT_4_VC)) Then
                txtNotes4.Text = CStr(bv_drGateIn.Item(GateinData.NT_4_VC)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.NT_5_VC)) Then
                txtNotes5.Text = CStr(bv_drGateIn.Item(GateinData.NT_5_VC)).ToString
            End If
            If bv_str051Key.ToLower = "true" Then
                Dim objGateIn As New Gatein

                If Not IsDBNull(bv_drGateIn.Item(GateinData.SL_PRTY_1_CD)) Then
                    lkpPartyMaster1.Text = objGateIn.pub_GetPartyById(CInt(bv_drGateIn.Item(GateinData.SL_PRTY_1_ID)))
                End If
                If Not IsDBNull(bv_drGateIn.Item(GateinData.SL_PRTY_2_CD)) Then
                    lkpPartyMaster2.Text = objGateIn.pub_GetPartyById(CInt(bv_drGateIn.Item(GateinData.SL_PRTY_2_ID)))
                End If
                If Not IsDBNull(bv_drGateIn.Item(GateinData.SL_PRTY_3_CD)) Then
                    lkpPartyMaster3.Text = objGateIn.pub_GetPartyById(CInt(bv_drGateIn.Item(GateinData.SL_PRTY_3_ID)))
                End If
            Else
                If Not IsDBNull(bv_drGateIn.Item(GateinData.SL_PRTY_1_CD)) Then
                    lkpParty1.Text = CStr(bv_drGateIn.Item(GateinData.SL_PRTY_1_CD)).ToString
                End If
                If Not IsDBNull(bv_drGateIn.Item(GateinData.SL_PRTY_2_CD)) Then
                    lkpParty2.Text = CStr(bv_drGateIn.Item(GateinData.SL_PRTY_2_CD)).ToString
                End If
                If Not IsDBNull(bv_drGateIn.Item(GateinData.SL_PRTY_3_CD)) Then
                    lkpParty3.Text = CStr(bv_drGateIn.Item(GateinData.SL_PRTY_3_CD)).ToString
                End If
            End If
         
            If Not IsDBNull(bv_drGateIn.Item(GateinData.SL_NMBR_1)) Then
                txtNumber1.Text = CStr(bv_drGateIn.Item(GateinData.SL_NMBR_1)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.SL_NMBR_2)) Then
                txtNumber2.Text = CStr(bv_drGateIn.Item(GateinData.SL_NMBR_2)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.SL_NMBR_3)) Then
                txtNumber3.Text = CStr(bv_drGateIn.Item(GateinData.SL_NMBR_3)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.PRT_FNC_CD)) Then
                txtPortFuncCode.Text = CStr(bv_drGateIn.Item(GateinData.PRT_FNC_CD)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.PRT_NM)) Then
                txtPortName.Text = CStr(bv_drGateIn.Item(GateinData.PRT_NM)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.PRT_NO)) Then
                txtPortnumber.Text = CStr(bv_drGateIn.Item(GateinData.PRT_NO)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.PRT_LC_CD)) Then
                txtPortLocCode.Text = CStr(bv_drGateIn.Item(GateinData.PRT_LC_CD)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.SHPPR_NAM)) Then
                txtShipper.Text = CStr(bv_drGateIn.Item(GateinData.SHPPR_NAM)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.RL_ID_VC)) Then
                txtRailId.Text = CStr(bv_drGateIn.Item(GateinData.RL_ID_VC)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.RL_RMP_LOC)) Then
                txtRailRamp.Text = CStr(bv_drGateIn.Item(GateinData.RL_RMP_LOC)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.VSSL_NM)) Then
                txtVesselName.Text = CStr(bv_drGateIn.Item(GateinData.VSSL_NM)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.VYG_NO)) Then
                txtVoyageNumber.Text = CStr(bv_drGateIn.Item(GateinData.VYG_NO)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.VSSL_CD)) Then
                txtVesselIdCode.Text = CStr(bv_drGateIn.Item(GateinData.VSSL_CD)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.HAZ_MTL_CD)) Then
                txthazCode.Text = CStr(bv_drGateIn.Item(GateinData.HAZ_MTL_CD)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.HAZ_MATL_DSC)) Then
                txthazDesc.Text = CStr(bv_drGateIn.Item(GateinData.HAZ_MATL_DSC)).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.GRSS_WGHT_NC)) Then
                txtGrossWeight.Text = bv_drGateIn.Item(GateinData.GRSS_WGHT_NC).ToString
            End If
            If Not IsDBNull(bv_drGateIn.Item(GateinData.TR_WGHT_NC)) Then
                txtTareWeight.Text = bv_drGateIn.Item(GateinData.TR_WGHT_NC).ToString
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "submit"
                    pvt_Submit(CInt(e.GetCallbackValue("GateInId")), _
                                    e.GetCallbackValue("MaterialId"), _
                                    e.GetCallbackValue("MaterialCode"), _
                                    e.GetCallbackValue("MeasureId"), _
                                    e.GetCallbackValue("MeasureCode"), _
                                    e.GetCallbackValue("MfgDate"), _
                                    e.GetCallbackValue("AcepCode"), _
                                    e.GetCallbackValue("UnitId"), _
                                    e.GetCallbackValue("UnitCd"), _
                                    e.GetCallbackValue("PreviousLocation"), _
                                    e.GetCallbackValue("PreviousDate"), _
                                    e.GetCallbackValue("TruckerCode"), _
                                    e.GetCallbackValue("LoadStatus"), _
                                    e.GetCallbackValue("CountryId"), _
                                    e.GetCallbackValue("CountryCode"), _
                                    e.GetCallbackValue("State"), _
                                    e.GetCallbackValue("LicNumber"), _
                                    e.GetCallbackValue("Expiry"), _
                                    e.GetCallbackValue("Notes1"), _
                                    e.GetCallbackValue("Notes2"), _
                                    e.GetCallbackValue("Notes3"), _
                                    e.GetCallbackValue("Notes4"), _
                                    e.GetCallbackValue("Notes5"), _
                                    e.GetCallbackValue("Party1Id"), _
                                    e.GetCallbackValue("Party2Id"), _
                                    e.GetCallbackValue("Party3Id"), _
                                    e.GetCallbackValue("Party1Cd"), _
                                    e.GetCallbackValue("Party2Cd"), _
                                    e.GetCallbackValue("Party3Cd"), _
                                    e.GetCallbackValue("SlNumber1"), _
                                    e.GetCallbackValue("SlNumber2"), _
                                    e.GetCallbackValue("SlNumber3"), _
                                    e.GetCallbackValue("PortFunctionCode"), _
                                    e.GetCallbackValue("Portname"), _
                                    e.GetCallbackValue("Portnumnber"), _
                                    e.GetCallbackValue("PortlocCode"), _
                                    e.GetCallbackValue("VesselName"), _
                                    e.GetCallbackValue("VoyageNumber"), _
                                    e.GetCallbackValue("VesselCode"), _
                                    e.GetCallbackValue("Shipper"), _
                                    e.GetCallbackValue("RailId"), _
                                    e.GetCallbackValue("RailRamploc"), _
                                    e.GetCallbackValue("HazCode"), _
                                    e.GetCallbackValue("HazDesc"), _
                                    e.GetCallbackValue("GrossWeight"), _
                                    e.GetCallbackValue("TareWeight"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_Submit"
    Private Sub pvt_Submit(ByVal bv_intGateInId As Integer, ByVal bv_strMaterialId As String, ByVal bv_strMaterialCode As String, ByVal bv_strMeasureId As String, ByVal bv_strMeasureCode As String, _
                           ByVal bv_strMfgDate As String, ByVal bv_strAcepCode As String, ByVal bv_strUnitId As String, ByVal bv_strUnitCd As String, ByVal bv_strPreviousLocation As String, _
                           ByVal bv_strPreviousDate As String, ByVal bv_strTruckerCode As String, ByVal bv_strLoadStatus As String, ByVal bv_strCountryId As String, ByVal bv_strCountryCode As String, _
                           ByVal bv_strState As String, ByVal bv_strLicNumber As String, ByVal bv_strExpiry As String, ByVal bv_strNotes1 As String, ByVal bv_strNotes2 As String, ByVal bv_strNotes3 As String, ByVal bv_strNotes4 As String, ByVal bv_strNotes5 As String, ByVal bv_strParty1Id As String, ByVal bv_strParty2Id As String, _
                           ByVal bv_strParty3Id As String, ByVal bv_strParty1Cd As String, ByVal bv_strParty2Cd As String, ByVal bv_strParty3Cd As String, ByVal bv_strSlNumber1 As String, ByVal bv_strSlNumber2 As String, ByVal bv_strSlNumber3 As String, _
                           ByVal bv_strPortFunctionCode As String, ByVal bv_strPortname As String, ByVal bv_strPortnumnber As String, ByVal bv_strPortlocCode As String, ByVal bv_strVesselName As String, ByVal bv_strVoyageNumber As String, _
                           ByVal bv_strVesselCode As String, ByVal bv_strShipper As String, ByVal bv_strRailId As String, ByVal bv_strRailRamploc As String, ByVal bv_strHazCode As String, ByVal bv_strHazDesc As String, ByVal bv_strGrsWeight As String, ByVal bv_strTarWeight As String)
        Try
            dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
            Dim drAGateInDeatil As DataRow()
            dsGateInData = CType(RetrieveData(GATE_IN), GateinDataSet)
            If dsGateInData Is Nothing Then
                dsGateOutData = CType(RetrieveData(GATE_OUT), GateOutDataSet)
                drAGateInDeatil = dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(GateOutData.GTOT_BIN, "=", bv_intGateInId))
            Else
                drAGateInDeatil = dsGateInData.Tables(GateinData._V_GATEIN_DETAIL).Select(String.Concat(GateinData.GTN_ID, "=", bv_intGateInId))
            End If
            Dim dtMaterial As DataTable
            Dim dtMeasure As DataTable
            Dim dtUnit As DataTable

            Dim dtCountry As DataTable
            Dim dtParty As DataTable
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim objGatein As New Gatein
            'dtCountry.Rows.Clear()


            dtParty = objGatein.getPartyDetails(intDPT_ID).Tables(GateinData._INVOICING_PARTY)
            If drAGateInDeatil.Length = 0 Then
                Dim drGateInDetail As DataRow
                drGateInDetail = dsGateInData.Tables(GateinData._V_GATEIN_DETAIL).NewRow()
                drGateInDetail.Item(GateinData.GTN_DTL_ID) = CommonWeb.GetNextIndex(dsGateInData.Tables(GateinData._V_GATEIN_DETAIL), GateinData.GTN_DTL_ID)
                iSetInt(drGateInDetail.Item(GateinData.GTN_ID), bv_intGateInId)
                iSetStr(drGateInDetail.Item(GateinData.MTRL_CD), bv_strMaterialCode)
                iSetInt(drGateInDetail.Item(GateinData.MTRL_ID), bv_strMaterialId)
                iSetInt(drGateInDetail.Item(GateinData.MSR_ID), bv_strMeasureId)
                iSetStr(drGateInDetail.Item(GateinData.MSR_CD), bv_strMeasureCode)
                If bv_strMfgDate Is Nothing Then
                    drGateInDetail.Item(GateinData.MNFCTR_DT) = DBNull.Value
                Else
                    iSetStr(drGateInDetail.Item(GateinData.MNFCTR_DT), bv_strMfgDate)
                End If
                iSetStr(drGateInDetail.Item(GateinData.ACEP_CD), bv_strAcepCode)
                iSetInt(drGateInDetail.Item(GateinData.UNT_ID), bv_strUnitId)
                iSetStr(drGateInDetail.Item(GateinData.UNT_CD), bv_strUnitCd)
                iSetStr(drGateInDetail.Item(GateinData.LST_OH_LOC), bv_strPreviousLocation)
                If bv_strPreviousDate Is Nothing Then
                    drGateInDetail.Item(GateinData.LST_OH_DT) = DBNull.Value
                Else
                    iSetStr(drGateInDetail.Item(GateinData.LST_OH_DT), bv_strPreviousDate)
                End If
                iSetStr(drGateInDetail.Item(GateinData.TRCKR_CD), bv_strTruckerCode)
                iSetStr(drGateInDetail.Item(GateinData.LOD_STTS_CD), bv_strLoadStatus)
                iSetInt(drGateInDetail.Item(GateinData.CNTRY_ID), bv_strCountryId)
                iSetStr(drGateInDetail.Item(GateinData.CNTRY_CD), bv_strCountryCode)
                iSetStr(drGateInDetail.Item(GateinData.LIC_STT), bv_strState)
                iSetStr(drGateInDetail.Item(GateinData.LIC_REG), bv_strLicNumber)
                If bv_strExpiry Is Nothing Then
                    drGateInDetail.Item(GateinData.LIC_EXPR) = DBNull.Value
                Else
                    iSetStr(drGateInDetail.Item(GateinData.LIC_EXPR), bv_strExpiry)
                End If
                iSetStr(drGateInDetail.Item(GateinData.NT_1_VC), bv_strNotes1)
                iSetStr(drGateInDetail.Item(GateinData.NT_2_VC), bv_strNotes2)
                iSetStr(drGateInDetail.Item(GateinData.NT_3_VC), bv_strNotes3)
                iSetStr(drGateInDetail.Item(GateinData.NT_4_VC), bv_strNotes4)
                iSetStr(drGateInDetail.Item(GateinData.NT_5_VC), bv_strNotes5)
                iSetInt(drGateInDetail.Item(GateinData.SL_PRTY_1_ID), bv_strParty1Id)
                iSetStr(drGateInDetail.Item(GateinData.SL_PRTY_1_CD), bv_strParty1Cd)
                iSetInt(drGateInDetail.Item(GateinData.SL_PRTY_2_ID), bv_strParty2Id)
                iSetStr(drGateInDetail.Item(GateinData.SL_PRTY_2_CD), bv_strParty2Cd)
                iSetInt(drGateInDetail.Item(GateinData.SL_PRTY_3_ID), bv_strParty3Id)
                iSetStr(drGateInDetail.Item(GateinData.SL_PRTY_3_CD), bv_strParty3Cd)
                iSetStr(drGateInDetail.Item(GateinData.SL_NMBR_1), bv_strSlNumber1)
                iSetStr(drGateInDetail.Item(GateinData.SL_NMBR_2), bv_strSlNumber2)
                iSetStr(drGateInDetail.Item(GateinData.SL_NMBR_3), bv_strSlNumber3)
                iSetStr(drGateInDetail.Item(GateinData.PRT_FNC_CD), bv_strPortFunctionCode)
                iSetStr(drGateInDetail.Item(GateinData.PRT_NM), bv_strPortname)
                iSetStr(drGateInDetail.Item(GateinData.PRT_NO), bv_strPortnumnber)
                iSetStr(drGateInDetail.Item(GateinData.PRT_LC_CD), bv_strPortlocCode)
                iSetStr(drGateInDetail.Item(GateinData.SHPPR_NAM), bv_strShipper)
                iSetStr(drGateInDetail.Item(GateinData.RL_ID_VC), bv_strRailId)
                iSetStr(drGateInDetail.Item(GateinData.RL_RMP_LOC), bv_strRailRamploc)
                iSetStr(drGateInDetail.Item(GateinData.VSSL_NM), bv_strVesselName)
                iSetStr(drGateInDetail.Item(GateinData.VYG_NO), bv_strVoyageNumber)
                iSetStr(drGateInDetail.Item(GateinData.VSSL_CD), bv_strVesselCode)
                iSetStr(drGateInDetail.Item(GateinData.HAZ_MTL_CD), bv_strHazCode)
                iSetStr(drGateInDetail.Item(GateinData.HAZ_MATL_DSC), bv_strHazDesc)
                iSetStr(drGateInDetail.Item(GateinData.GRSS_WGHT_NC), bv_strGrsWeight)
                iSetStr(drGateInDetail.Item(GateinData.TR_WGHT_NC), bv_strTarWeight)
                dsGateInData.Tables(GateinData._V_GATEIN_DETAIL).Rows.Add(drGateInDetail)

                For Each dr As DataRow In dsGateInData.Tables(GateinData._V_GATEIN).Select(String.Concat(GateinData.GTN_ID, "=", bv_intGateInId))
                    dr.Item(GateinData.ALLOW_UPDATE) = True
                Next
            Else
                For Each drGateInDetail As DataRow In dsGateInData.Tables(GateinData._V_GATEIN_DETAIL).Select(String.Concat(GateinData.GTN_ID, "=", bv_intGateInId))
                    iSetInt(drGateInDetail.Item(GateinData.GTN_ID), bv_intGateInId)
                    iSetStr(drGateInDetail.Item(GateinData.MTRL_CD), bv_strMaterialCode)
                    If (bv_strMaterialId = "") Then
                        dtMaterial = objGatein.GetMaterialDetails(intDPT_ID).Tables(GateinData._MATERIAL)
                        For Each drMaterial As DataRow In dtMaterial.Select(String.Concat(GateinData.MTRL_CD, "='", bv_strMaterialCode, "'"))
                            iSetInt(drGateInDetail.Item(GateinData.MTRL_ID), drMaterial.Item(GateinData.MTRL_ID))
                        Next
                    Else
                        iSetInt(drGateInDetail.Item(GateinData.MTRL_ID), bv_strMaterialId)
                    End If
                    If (bv_strMeasureId = "") Then
                        dtMeasure = objGatein.GetMeasureDetails(intDPT_ID).Tables(GateinData._MEASURE)
                        For Each drMaterial As DataRow In dtMeasure.Select(String.Concat(GateinData.MSR_CD, "='", bv_strMeasureCode, "'"))
                            iSetInt(drGateInDetail.Item(GateinData.MSR_ID), drMaterial.Item(GateinData.MSR_ID))
                        Next
                    Else
                        iSetInt(drGateInDetail.Item(GateinData.MSR_ID), bv_strMeasureId)
                    End If
                    iSetStr(drGateInDetail.Item(GateinData.MSR_CD), bv_strMeasureCode)
                    If bv_strMfgDate Is Nothing Then
                        drGateInDetail.Item(GateinData.MNFCTR_DT) = DBNull.Value
                    Else
                        iSetStr(drGateInDetail.Item(GateinData.MNFCTR_DT), bv_strMfgDate)
                    End If
                    iSetStr(drGateInDetail.Item(GateinData.ACEP_CD), bv_strAcepCode)
                    If (bv_strMeasureId = "") Then
                        dtUnit = objGatein.GetUnitDetails(intDPT_ID).Tables(GateinData._UNIT)
                        For Each drUnit As DataRow In dtUnit.Select(String.Concat(GateinData.UNT_CD, "='", bv_strUnitCd, "'"))
                            iSetInt(drGateInDetail.Item(GateinData.UNT_ID), drUnit.Item(GateinData.UNT_ID))
                        Next
                    Else
                        iSetInt(drGateInDetail.Item(GateinData.UNT_ID), bv_strUnitId)

                    End If
                    iSetStr(drGateInDetail.Item(GateinData.UNT_CD), bv_strUnitCd)
                    iSetStr(drGateInDetail.Item(GateinData.LST_OH_LOC), bv_strPreviousLocation)
                    If bv_strPreviousDate Is Nothing Then
                        drGateInDetail.Item(GateinData.LST_OH_DT) = DBNull.Value
                    Else
                        iSetStr(drGateInDetail.Item(GateinData.LST_OH_DT), bv_strPreviousDate)
                    End If
                    iSetStr(drGateInDetail.Item(GateinData.TRCKR_CD), bv_strTruckerCode)
                    iSetStr(drGateInDetail.Item(GateinData.LOD_STTS_CD), bv_strLoadStatus)
                    If (bv_strCountryId = "") Then
                        dtCountry = objGatein.GetCountryDetails(intDPT_ID).Tables(GateinData._COUNTRY)
                        For Each drCustomer As DataRow In dtCountry.Select(String.Concat(GateinData.CNTRY_CD, "='", bv_strCountryCode, "'"))
                            iSetInt(drGateInDetail.Item(GateinData.CNTRY_ID), drCustomer.Item(GateinData.CNTRY_ID))
                        Next
                    Else
                        iSetInt(drGateInDetail.Item(GateinData.CNTRY_ID), bv_strCountryId)
                    End If
                    iSetStr(drGateInDetail.Item(GateinData.CNTRY_CD), bv_strCountryCode)
                    iSetStr(drGateInDetail.Item(GateinData.LIC_STT), bv_strState)
                    iSetStr(drGateInDetail.Item(GateinData.LIC_REG), bv_strLicNumber)
                    If bv_strExpiry Is Nothing Then
                        drGateInDetail.Item(GateinData.LIC_EXPR) = DBNull.Value
                    Else
                        iSetStr(drGateInDetail.Item(GateinData.LIC_EXPR), bv_strExpiry)
                    End If
                    iSetStr(drGateInDetail.Item(GateinData.NT_1_VC), bv_strNotes1)
                    iSetStr(drGateInDetail.Item(GateinData.NT_2_VC), bv_strNotes2)
                    iSetStr(drGateInDetail.Item(GateinData.NT_3_VC), bv_strNotes3)
                    iSetStr(drGateInDetail.Item(GateinData.NT_4_VC), bv_strNotes4)
                    iSetStr(drGateInDetail.Item(GateinData.NT_5_VC), bv_strNotes5)
                    If (bv_strParty1Id = "") Then
                        For Each drParty As DataRow In dtParty.Select(String.Concat(GateinData.INVCNG_PRTY_CD, "='", bv_strParty1Cd, "'"))
                            iSetInt(drGateInDetail.Item(GateinData.SL_PRTY_1_ID), drParty.Item(GateinData.INVCNG_PRTY_ID))
                        Next
                    Else
                        iSetInt(drGateInDetail.Item(GateinData.SL_PRTY_1_ID), bv_strParty2Id)
                    End If
                    If (bv_strParty2Id = "") Then
                        For Each drParty As DataRow In dtParty.Select(String.Concat(GateinData.INVCNG_PRTY_CD, "='", bv_strParty2Cd, "'"))
                            iSetInt(drGateInDetail.Item(GateinData.SL_PRTY_2_ID), drParty.Item(GateinData.INVCNG_PRTY_ID))
                        Next
                    Else
                        iSetInt(drGateInDetail.Item(GateinData.SL_PRTY_2_ID), bv_strParty2Id)
                    End If
                    If (bv_strParty3Id = "") Then
                        For Each drParty As DataRow In dtParty.Select(String.Concat(GateinData.INVCNG_PRTY_CD, "='", bv_strParty3Cd, "'"))
                            iSetInt(drGateInDetail.Item(GateinData.SL_PRTY_3_ID), drParty.Item(GateinData.INVCNG_PRTY_ID))
                        Next
                    Else
                        iSetInt(drGateInDetail.Item(GateinData.SL_PRTY_3_ID), bv_strParty3Id)
                    End If
                    iSetStr(drGateInDetail.Item(GateinData.SL_PRTY_1_CD), bv_strParty1Cd)
                    iSetStr(drGateInDetail.Item(GateinData.SL_PRTY_2_CD), bv_strParty2Cd)
                    'iSetInt(drGateInDetail.Item(GateinData.SL_PRTY_3_ID), bv_strParty3Id)
                    iSetStr(drGateInDetail.Item(GateinData.SL_PRTY_3_CD), bv_strParty3Cd)
                    iSetStr(drGateInDetail.Item(GateinData.SL_NMBR_1), bv_strSlNumber1)
                    iSetStr(drGateInDetail.Item(GateinData.SL_NMBR_2), bv_strSlNumber2)
                    iSetStr(drGateInDetail.Item(GateinData.SL_NMBR_3), bv_strSlNumber3)
                    iSetStr(drGateInDetail.Item(GateinData.PRT_FNC_CD), bv_strPortFunctionCode)
                    iSetStr(drGateInDetail.Item(GateinData.PRT_NM), bv_strPortname)
                    iSetStr(drGateInDetail.Item(GateinData.PRT_NO), bv_strPortnumnber)
                    iSetStr(drGateInDetail.Item(GateinData.PRT_LC_CD), bv_strPortlocCode)
                    iSetStr(drGateInDetail.Item(GateinData.SHPPR_NAM), bv_strShipper)
                    iSetStr(drGateInDetail.Item(GateinData.RL_ID_VC), bv_strRailId)
                    iSetStr(drGateInDetail.Item(GateinData.RL_RMP_LOC), bv_strRailRamploc)
                    iSetStr(drGateInDetail.Item(GateinData.VSSL_NM), bv_strVesselName)
                    iSetStr(drGateInDetail.Item(GateinData.VYG_NO), bv_strVoyageNumber)
                    iSetStr(drGateInDetail.Item(GateinData.VSSL_CD), bv_strVesselCode)
                    iSetStr(drGateInDetail.Item(GateinData.HAZ_MTL_CD), bv_strHazCode)
                    iSetStr(drGateInDetail.Item(GateinData.HAZ_MATL_DSC), bv_strHazDesc)
                    iSetStr(drGateInDetail.Item(GateinData.GRSS_WGHT_NC), bv_strGrsWeight)
                    iSetStr(drGateInDetail.Item(GateinData.TR_WGHT_NC), bv_strTarWeight)
                    For Each dr As DataRow In dsGateInData.Tables(GateinData._V_GATEIN).Select(String.Concat(GateinData.GTN_ID, "=", bv_intGateInId))
                        dr.Item(GateinData.ALLOW_UPDATE) = True
                    Next
                Next
            End If
            CacheData(GATE_IN, dsGateInData)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"

    Private Sub pvt_SetChangesMade()
        Try
            CommonWeb.pub_AttachHasChanges(lkpMaterialCode)
            CommonWeb.pub_AttachHasChanges(lkpMeasure)
            CommonWeb.pub_AttachHasChanges(txtDateofManf)
            CommonWeb.pub_AttachHasChanges(txtAcep)
            CommonWeb.pub_AttachHasChanges(lkpUnits)
            CommonWeb.pub_AttachHasChanges(txtOnhireLoc)
            CommonWeb.pub_AttachHasChanges(datOnhireDate)
            CommonWeb.pub_AttachHasChanges(txtTruckerCode)
            CommonWeb.pub_AttachHasChanges(lkpLoadStatus)
            CommonWeb.pub_AttachHasChanges(lkpCountry)
            CommonWeb.pub_AttachHasChanges(txtState)
            CommonWeb.pub_AttachHasChanges(txtNumber)
            CommonWeb.pub_AttachHasChanges(txtExpiry)
            CommonWeb.pub_AttachHasChanges(txtNotes1)
            CommonWeb.pub_AttachHasChanges(txtNotes2)
            CommonWeb.pub_AttachHasChanges(txtNotes3)
            CommonWeb.pub_AttachHasChanges(lkpParty1)
            CommonWeb.pub_AttachHasChanges(lkpParty2)
            CommonWeb.pub_AttachHasChanges(lkpParty3)
            CommonWeb.pub_AttachHasChanges(txtNumber1)
            CommonWeb.pub_AttachHasChanges(txtNumber2)
            CommonWeb.pub_AttachHasChanges(txtNumber3)
            CommonWeb.pub_AttachHasChanges(txtPortFuncCode)
            CommonWeb.pub_AttachHasChanges(txtPortName)
            CommonWeb.pub_AttachHasChanges(txtPortnumber)
            CommonWeb.pub_AttachHasChanges(txtPortLocCode)
            CommonWeb.pub_AttachHasChanges(txtShipper)
            CommonWeb.pub_AttachHasChanges(txtRailId)
            CommonWeb.pub_AttachHasChanges(txtRailRamp)
            CommonWeb.pub_AttachHasChanges(txtVesselName)
            CommonWeb.pub_AttachHasChanges(txtVoyageNumber)
            CommonWeb.pub_AttachHasChanges(txtVesselIdCode)
            CommonWeb.pub_AttachHasChanges(txthazCode)
            CommonWeb.pub_AttachHasChanges(txthazDesc)
            CommonWeb.pub_AttachHasChanges(txtGrossWeight)
            CommonWeb.pub_AttachHasChanges(txtTareWeight)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                    MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

    Private Sub iSetInt(ByRef br_ObjItem As Object, ByVal bv_strValue As Object)
        Try
            If bv_strValue Is Nothing Then
                br_ObjItem = DBNull.Value
            Else
                br_ObjItem = CInt(bv_strValue)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub iSetStr(ByRef br_ObjItem As Object, ByVal bv_strValue As Object)
        Try
            If bv_strValue Is Nothing Then
                br_ObjItem = DBNull.Value
            Else
                br_ObjItem = CStr(bv_strValue)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
