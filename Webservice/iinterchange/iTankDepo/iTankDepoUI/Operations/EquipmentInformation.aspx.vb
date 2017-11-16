
Partial Class Operations_EQUIPMENTINFORMATION
    Inherits Pagebase
    Dim dsEquipmentInformationData As New EquipmentInformationDataSet
    Dim dtEquipmentInformationData As DataTable
    Private Const EQUIPMENT_INFORMATION As String = "EQUIPMENT_INFORMATION"
    Private Const EQUIPMENT_INFORMATION_DOCUMENT As String = "EQUIPMENT_INFORMATION_DOCUMENT"
    Private strMSGUPDATE As String = "Equipment Information : Equipment(s) Updated Successfully."
    Private Const strNew As String = "New"
    Dim strMode As String
    Dim bln_012EqType_Key As Boolean
    Dim str_012EqType As String
    Dim bln_022RentalBit_Key As Boolean
    Dim str_022RentalBit As String
    Dim str_053GWS As String
    Dim bln_053GWSActive_Key As Boolean
    Dim strEqpmntInfoId As String
    Dim objCommonConfig As New ConfigSetting()

#Region "Page_Load1"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                Dim strEquipmentNo As String = Request.QueryString("EquipmentNo")

                If Not pub_Callback() Then
                    CacheData("EquipmentNo_key", strEquipmentNo)
                End If


                hdnEquipmentNo.Value = strEquipmentNo
                If strEquipmentNo <> Nothing Then
                    PageSubmitPane.Visible = False
                    navEquipmentInfo.Visible = False
                    btnSubmitEq.Visible = True
                    '  ClientScript.RegisterClientScriptBlock(GetType(String), "redirect6", " $().ready(function () {initPage('GateIn');});", True)
                Else
                    btnSubmitEq.Visible = False
                End If
            Else

            End If
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/EquipmentInformation.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgEquipmentInformation, "ITab1_0")
    End Sub

#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "UpdateEquipmentInformation"
                    pvt_UpdateEquipmentInformation()
                Case "ValidateEquipment"
                    pvt_ValidateEquipment(e.GetCallbackValue("EquipmentId"), CInt(e.GetCallbackValue("GridIndex")), e.GetCallbackValue("RowState"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateEquipment"
    Private Sub pvt_ValidateEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_intGridIndex As Integer, ByVal bv_strRowstate As String)
        Try
            Dim blndsValid As Boolean
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            dtEquipmentInformationData = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION)
            Dim intResultIndex() As System.Data.DataRow = dtEquipmentInformationData.Select(String.Concat(EquipmentInformationData.EQPMNT_NO, "='", bv_strEquipmentNo, "' "))
            Dim strExistEquipment As String = ""
            If intResultIndex.Length > 0 Then
                If dtEquipmentInformationData.Rows.Count > bv_intGridIndex Then
                    If dtEquipmentInformationData.Rows(bv_intGridIndex).RowState = Data.DataRowState.Deleted Then
                        strExistEquipment = String.Empty
                    ElseIf dtEquipmentInformationData.Rows(bv_intGridIndex).RowState <> Data.DataRowState.Deleted Then
                        strExistEquipment = dtEquipmentInformationData.Rows(bv_intGridIndex)(EquipmentInformationData.EQPMNT_NO).ToString
                    End If
                End If

                If bv_strEquipmentNo = strExistEquipment Then
                    blndsValid = True
                Else
                    blndsValid = False
                End If
            Else
                blndsValid = True
            End If
            'Checking whether the entered code is available in database
            If blndsValid = True Then
                If bv_strRowstate = "Added" Or bv_strRowstate = "Modified" Then
                    Dim objEquipmentInformation As New EquipmentInformation
                    Dim objCommon As New CommonData
                    blndsValid = objEquipmentInformation.pub_ValidateEquipmentNoByDepotID(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))
                End If
            End If

            If blndsValid = True Then
                pub_SetCallbackReturnValue("bNotExists", "true")
            Else
                pub_SetCallbackReturnValue("bNotExists", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_UpdateEquipmentInformation"
    Private Sub pvt_UpdateEquipmentInformation()
        Try
            Dim dtEquipmentInformationDocument As DataTable
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            pvt_HasChangeEquipmentInformation(dsEquipmentInformationData)
            Dim drAEquipmentInformation As DataRow()
            Dim objCommon As New CommonData
            drAEquipmentInformation = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(EquipmentInformationData.ACTV_BT & "='True'")
            dtEquipmentInformationDocument = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Clone()
            Dim objEquipmentInformation As New EquipmentInformation
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            'If Not dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL) Is Nothing AndAlso dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count = 0 Then
            '    dtAttachmentDetail = objEquipmentInformation.pub_GetEquipmentInformationDetail().Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
            '    dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Merge(dtAttachmentDetail)
            'End If
            objEquipmentInformation.pub_UpdateEquipmentInformation(dsEquipmentInformationData, intDPT_ID, objCommon.GetCurrentUserName())
            dsEquipmentInformationData.AcceptChanges()

            Dim strEquipno As String = RetrieveData("EquipmentNo_key")

            If strEquipno <> Nothing AndAlso strEquipno <> String.Empty Then
                Dim strRemarks As String = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows(0).Item(EquipmentInformationData.RMRKS_VC).ToString()
                pub_CacheData("Equip_Remarks", strRemarks)
            End If

            CacheData(EQUIPMENT_INFORMATION, dsEquipmentInformationData)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentInformation_ClientBind"
    Protected Sub ifgEquipmentInformation_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentInformation.ClientBind
        Try
            Dim ObjEquipInfo As New EquipmentInformation()
            Dim objCommon As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDepotID As Integer = objCommon.GetDepotID()
            Dim strEqpmntNo As String = String.Empty
            Dim dtEqDetail As New DataTable

            ''Release - 2
            If IsNothing(dsEquipmentInformationData) Then
                dsEquipmentInformationData = New EquipmentInformationDataSet
            End If
            strEqpmntNo = e.Parameters("EquipmentNo").ToString()
            If e.Parameters("Mode") <> Nothing AndAlso e.Parameters("Mode").ToString() = "GateIn" Then
                CacheData("PageName", e.Parameters("Mode").ToString())
                Dim strGateinTransactionNo As String = String.Empty
                If e.Parameters("GateinTransactionNo") <> Nothing Then
                    strGateinTransactionNo = e.Parameters("GateinTransactionNo").ToString()
                End If
                If e.Parameters("Attachment").ToString.ToUpper <> "TRUE" Then
                    dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows.Clear()
                    dtEqDetail = ObjEquipInfo.GetEquipmentInfoGateIn(strEqpmntNo, intDepotID).Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION)

                    'CHECK WHETHER REMARKS IS NEW FOR CONSIGNMENT OR LAST CYCLE.
                    If dtEqDetail.Rows.Count > 0 Then
                        Dim strEquipmentInfoRemarks As String = String.Empty
                        Dim strHistroyRemarks As String = String.Empty
                        Dim dtTracking As New DataTable
                        dtTracking = dsEquipmentInformationData.Tables(EquipmentInformationData._TRACKING).Clone()
                        If Not IsDBNull(dtEqDetail.Rows(0).Item(EquipmentInformationData.RMRKS_VC)) Then
                            strEquipmentInfoRemarks = dtEqDetail.Rows(0).Item(EquipmentInformationData.RMRKS_VC)
                        End If
                        dtTracking = ObjEquipInfo.pub_GetEquipmentInfoRemaksTracking(strEqpmntNo, intDepotID, "Gate Out", strGateinTransactionNo).Tables(EquipmentInformationData._TRACKING)
                        If dtTracking.Rows.Count > 0 Then
                            If Not IsDBNull(dtTracking.Rows(0).Item(EquipmentInformationData.EQPMNT_INFRMTN_RMRKS_VC)) Then
                                strHistroyRemarks = dtTracking.Rows(0).Item(EquipmentInformationData.EQPMNT_INFRMTN_RMRKS_VC)
                            End If
                        End If
                        If strEquipmentInfoRemarks = strHistroyRemarks Then
                            dtEqDetail.Rows(0).Item(EquipmentInformationData.RMRKS_VC) = DBNull.Value
                        Else
                            dtEqDetail.Rows(0).Item(EquipmentInformationData.RMRKS_VC) = strEquipmentInfoRemarks
                        End If
                    End If
                    ''END CHECK

                End If
                ifgEquipmentInformation.Columns.Item(0).IsEditable = False
                ifgEquipmentInformation.Columns.Item(1).IsEditable = False
                ifgEquipmentInformation.AllowPaging = False
                ifgEquipmentInformation.AllowAdd = False
                ifgEquipmentInformation.AllowDelete = False
                ifgEquipmentInformation.AllowSearch = False
                ifgEquipmentInformation.AllowRefresh = False
                ifgEquipmentInformation.AllowStaticHeader = True
                ifgEquipmentInformation.StaticHeaderHeight = "150"
                ifgEquipmentInformation.Width = 920
                PageSubmitPane.Visible = False
                navEquipmentInfo.Visible = False
                btnSubmitEq.Visible = True
            Else
                ifgEquipmentInformation.AllowSearch = True
                btnSubmitEq.Visible = False
                If e.Parameters("Attachment").ToString.ToUpper <> "TRUE" Then
                    dtEqDetail = ObjEquipInfo.GetEquipmentInformation(intDepotID).Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION)
                Else
                    dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
                End If
            End If
            dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Merge(dtEqDetail)
            If dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows.Count = 0 Then
                Dim drEqInfo As DataRow = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).NewRow()

                Dim objCommonUI As New CommonUI()
                Dim dsEqpmntType As New CommonUIDataSet
                str_012EqType = objCommonConfig.pub_GetConfigSingleValue("012", intDepotID)
                bln_012EqType_Key = objCommonConfig.IsKeyExists

                If bln_012EqType_Key Then
                    If Not str_012EqType = "" Then
                        If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                            dsEqpmntType = objCommonUI.pub_GetEquipmentType(str_012EqType, objCommon.GetHeadQuarterID())
                        Else
                            dsEqpmntType = objCommonUI.pub_GetEquipmentType(str_012EqType, intDepotID)
                        End If

                        If dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                            drEqInfo.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = CommonWeb.GetNextIndex(dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION), EquipmentInformationData.EQPMNT_INFRMTN_ID)
                            drEqInfo.Item(EquipmentInformationData.EQPMNT_TYP_ID) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                            drEqInfo.Item(EquipmentInformationData.EQPMNT_TYP_CD) = str_012EqType
                            drEqInfo.Item(EquipmentInformationData.EQPMNT_TYP_DSCRPTN_VC) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_DSCRPTN_VC).ToString
                            If strEqpmntNo <> Nothing Then
                                drEqInfo.Item(EquipmentInformationData.EQPMNT_NO) = strEqpmntNo
                                drEqInfo.Item(EquipmentInformationData.ALLOW_EDIT) = False
                            End If
                            drEqInfo.Item(EquipmentInformationData.ACTV_BT) = True
                        End If
                    End If
                End If
                dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows.Add(drEqInfo)
            End If

            str_053GWS = objCommonConfig.pub_GetConfigSingleValue("053", intDepotID)
            bln_053GWSActive_Key = objCommonConfig.IsKeyExists
            Dim objCommondata As New CommonData
            If str_053GWS Then
                ifgEquipmentInformation.Columns.Item(6).HeaderText = "Max Pay Load (Kgs)"

                objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 11, "False")
                objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 8, "False")
                objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 9, "False")
                objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 10, "False")
            End If
            e.DataSource = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION)
            If e.Parameters("Attachment").ToString.ToUpper = "TRUE" Then
                Dim drSelect As DataRow() = Nothing
                Dim intCount As Integer = 0
                If dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count > 0 Then
                    drSelect = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_NO, " = '", strEqpmntNo, "'"))
                    If drSelect.Length > 0 Then
                        intCount = CInt(dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Compute(String.Concat("COUNT(", EquipmentInformationData.EQPMNT_INFRMTN_ID, ")"), String.Concat(EquipmentInformationData.EQPMNT_NO, " = '", strEqpmntNo, "'")))
                        drSelect(0).Item(EquipmentInformationData.COUNT_ATTACH) = intCount
                    End If
                Else
                    drSelect = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_NO, " = '", strEqpmntNo, "'"))
                    If drSelect.Length > 0 Then
                        intCount = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Compute(String.Concat("COUNT(", EquipmentInformationData.EQPMNT_INFRMTN_ID, ")"), String.Concat(EquipmentInformationData.EQPMNT_NO, " = '", strEqpmntNo, "'"))
                        drSelect(0).Item(EquipmentInformationData.COUNT_ATTACH) = intCount
                    End If
                End If
            End If
            CacheData(EQUIPMENT_INFORMATION, dsEquipmentInformationData)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentInformation_RowDataBound"
    Protected Sub ifgEquipmentInformation_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentInformation.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objCommondata As New CommonData
                Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())

                str_022RentalBit = objCommonConfig.pub_GetConfigSingleValue("022", intDepotID)
                bln_022RentalBit_Key = objCommonConfig.IsKeyExists

                'GWS
                str_053GWS = objCommonConfig.pub_GetConfigSingleValue("053", intDepotID)
                bln_053GWSActive_Key = objCommonConfig.IsKeyExists
                If str_053GWS Then
                    e.Row.Cells(6).Text = "Max Pay Load (Kgs)"
                    'e.Row.Cells(8).Style.Add("display", "none")
                    'e.Row.Cells(9).Style.Add("display", "none")
                    'e.Row.Cells(10).Style.Add("display", "none")
                    'e.Row.Cells(11).Style.Add("display", "none")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 8, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 9, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 10, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 11, "False")
                    ifgEquipmentInformation.Columns.Item(8).Visible = False
                    ifgEquipmentInformation.Columns.Item(9).Visible = False
                    ifgEquipmentInformation.Columns.Item(10).Visible = False
                    ifgEquipmentInformation.Columns.Item(11).Visible = False
                Else
                    e.Row.Cells(2).Style.Add("display", "none")
                    e.Row.Cells(12).Style.Add("display", "none")
                    e.Row.Cells(13).Style.Add("display", "none")
                    e.Row.Cells(14).Style.Add("display", "none")
                    'e.Row.Cells(15).Style.Add("display", "none")
                    'ifgEquipmentInformation.Columns.Item(12).Visible = False
                    'ifgEquipmentInformation.Columns.Item(13).Visible = False
                    'ifgEquipmentInformation.Columns.Item(14).Visible = False
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 12, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 13, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 14, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 15, "False")
                    e.Row.Cells(18).Style.Add("display", "none")
                End If

                If bln_022RentalBit_Key Then
                    If str_022RentalBit.ToLower = "false" OrElse objCommondata.GetDepotID <> objCommondata.GetHeadQuarterID Then
                        'GWS
                        e.Row.Cells(18).Style.Add("display", "none")
                        'ifgEquipmentInformation.Columns.Item(18).ControlStyle.CssClass = "hide"
                        'ifgEquipmentInformation.Columns.Item(18).ItemStyle.CssClass = "hide"
                        'ifgEquipmentInformation.Columns.Item(18).HeaderStyle.CssClass = "hide"
                    Else
                        e.Row.Cells(18).Style.Add("display", "table-cell")
                        'ifgEquipmentInformation.Columns.Item(18).ControlStyle.CssClass = "show"
                        'ifgEquipmentInformation.Columns.Item(18).ItemStyle.CssClass = "show"
                        'ifgEquipmentInformation.Columns.Item(18).HeaderStyle.CssClass = "show"
                    End If
                End If

            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim imgFileUpload As Image
                Dim datControl As iDate
                Dim datOnHire As iDate
                Dim objCommondata As New CommonData
                Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
                str_053GWS = objCommonConfig.pub_GetConfigSingleValue("053", intDepotID)
                bln_053GWSActive_Key = objCommonConfig.IsKeyExists
                If str_053GWS Then
                    e.Row.Cells(8).Style.Add("display", "none")
                    e.Row.Cells(9).Style.Add("display", "none")
                    e.Row.Cells(10).Style.Add("display", "none")
                    e.Row.Cells(11).Style.Add("display", "none")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 8, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 9, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 10, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 11, "False")
                Else
                    e.Row.Cells(2).Style.Add("display", "none")
                    e.Row.Cells(12).Style.Add("display", "none")
                    e.Row.Cells(13).Style.Add("display", "none")
                    e.Row.Cells(14).Style.Add("display", "none")
                    'ifgEquipmentInformation.Columns.Item(12).Visible = False
                    'ifgEquipmentInformation.Columns.Item(13).Visible = False
                    'ifgEquipmentInformation.Columns.Item(14).Visible = False
                    'e.Row.Cells(15).Style.Add("display", "none")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 12, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 13, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 14, "False")
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentInformation, "hide", 15, "False")

                End If

                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                    'for GWS
                    '  imgFileUpload = CType(e.Row.Cells(15).Controls(0), Image)
                    imgFileUpload = CType(e.Row.Cells(16).Controls(0), Image)
                    imgFileUpload.ToolTip = "Attach Files"
                    Dim drSelect() As DataRow = Nothing
                    Dim drEqDetail() As DataRow = Nothing
                    dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
                    drEqDetail = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Select(String.Concat(EquipmentInformationData.EQPMNT_NO, " = '", drv.Item(EquipmentInformationData.EQPMNT_NO), "'"))
                    If drEqDetail.Length > 0 Then
                        drSelect = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_NO, " = '", drv.Item(EquipmentInformationData.EQPMNT_NO), "'"))
                        If drSelect.Length > 0 Then
                            drSelect(0).Item(EquipmentInformationData.COUNT_ATTACH) = 1
                        End If
                    End If
                    If Not IsDBNull(drv.Item(EquipmentInformationData.COUNT_ATTACH)) Then
                        If Not drv.Item(EquipmentInformationData.COUNT_ATTACH) > 0 Then
                            imgFileUpload.ImageUrl = "../Images/noattachment.png"
                        Else
                            imgFileUpload.ImageUrl = "../Images/attachment.png"
                        End If
                    Else
                        imgFileUpload.ImageUrl = "../Images/noattachment.png"
                    End If
                    Dim strMode As String = String.Empty
                    If Not RetrieveData("PageName") Is Nothing Then
                        strMode = RetrieveData("PageName").ToString
                    End If

                    imgFileUpload.Attributes.Add("onclick", String.Concat("openEquipmentInformation('", e.Row.DataItem(EquipmentInformationData.EQPMNT_INFRMTN_ID), "','", e.Row.DataItem(EquipmentInformationData.EQPMNT_NO), "','", strMode, "') ;return false;"))
                    imgFileUpload.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                    '    imgFileUpload = CType(e.Row.Cells(15).Controls(0), Image)
                    imgFileUpload = CType(e.Row.Cells(16).Controls(0), Image)

                    If drv.Row.RowState = DataRowState.Added Then
                        datControl = CType(DirectCast(DirectCast(e.Row.Cells(3), iFgFieldCell).ContainingField, DateField).iDate, iDate)
                        datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper

                        datOnHire = CType(DirectCast(DirectCast(e.Row.Cells(13), iFgFieldCell).ContainingField, DateField).iDate, iDate)
                        datOnHire.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper

                        CType(e.Row.Cells(18), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = True
                    ElseIf drv.Row.RowState = DataRowState.Unchanged Then
                        datControl = CType(DirectCast(DirectCast(e.Row.Cells(3), iFgFieldCell).ContainingField, DateField).iDate, iDate)
                        datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper

                        datOnHire = CType(DirectCast(DirectCast(e.Row.Cells(13), iFgFieldCell).ContainingField, DateField).iDate, iDate)
                        datOnHire.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper

                    ElseIf drv.Row.RowState = DataRowState.Modified Then
                        datControl = CType(DirectCast(DirectCast(e.Row.Cells(3), iFgFieldCell).ContainingField, DateField).iDate, iDate)
                        datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper

                        datOnHire = CType(DirectCast(DirectCast(e.Row.Cells(13), iFgFieldCell).ContainingField, DateField).iDate, iDate)
                        datOnHire.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                    End If
                    If Not e.Row.DataItem Is Nothing Then
                        drv = CType(e.Row.DataItem, Data.DataRowView)
                        If drv.Row.RowState = DataRowState.Unchanged Then
                            CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        End If
                    End If
                    If Not IsDBNull(CType(e.Row.DataItem, Data.DataRowView).Row.Item(EquipmentInformationData.ALLOW_EDIT)) Then
                        If CBool(CType(e.Row.DataItem, Data.DataRowView).Row.Item(EquipmentInformationData.ALLOW_EDIT)) = False Then
                            CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        End If
                    End If
                    If Not IsDBNull(drv.Row.Item(EquipmentInformationData.RNTL_EDT_BT)) Then
                        If CBool(drv.Row.Item(EquipmentInformationData.RNTL_EDT_BT)) Then
                            'GWS
                            '  CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = True
                            CType(e.Row.Cells(18), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = True
                        Else
                            ' CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False
                            CType(e.Row.Cells(18), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False
                        End If
                    End If
                    If e.Row.RowIndex > 6 Then
                        Dim lkpControl As iLookup
                        lkpControl = CType(DirectCast(DirectCast(e.Row.Cells(1), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                        lkpControl.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                    End If

                    If bln_022RentalBit_Key Then
                        If str_022RentalBit.ToLower = "false" OrElse objCommondata.GetDepotID <> objCommondata.GetHeadQuarterID Then
                            'GWS
                            e.Row.Cells(18).Style.Add("display", "none")
                            'ifgEquipmentInformation.Columns.Item(18).ControlStyle.CssClass = "hide"
                            'ifgEquipmentInformation.Columns.Item(18).ItemStyle.CssClass = "hide"
                            'ifgEquipmentInformation.Columns.Item(18).HeaderStyle.CssClass = "hide"
                        Else
                            e.Row.Cells(18).Style.Add("display", "table-cell")
                            'ifgEquipmentInformation.Columns.Item(18).ControlStyle.CssClass = "show"
                            'ifgEquipmentInformation.Columns.Item(18).ItemStyle.CssClass = "show"
                            'ifgEquipmentInformation.Columns.Item(18).HeaderStyle.CssClass = "show"
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentInformation_RowInserting"
    Protected Sub ifgEquipmentInformation_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgEquipmentInformation.RowInserting
        Try
            Dim lngEquipmentInformationID As Long
            Dim objCommon As New CommonData
            Dim intDepotID As Integer = objCommon.GetDepotID()
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            lngEquipmentInformationID = CommonWeb.GetNextIndex(dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION), EquipmentInformationData.EQPMNT_INFRMTN_ID)
            e.Values(EquipmentInformationData.EQPMNT_INFRMTN_ID) = lngEquipmentInformationID
            strEqpmntInfoId = lngEquipmentInformationID
            str_053GWS = objCommonConfig.pub_GetConfigSingleValue("053", intDepotID)
            bln_053GWSActive_Key = objCommonConfig.IsKeyExists
            If bln_053GWSActive_Key = False Then
                Dim lstTestType As String
                lstTestType = e.Values(EquipmentInformationData.NXT_TST_TYP_CD).ToString.ToUpper
                If lstTestType = "HYDRO" Then
                    e.Values(EquipmentInformationData.NXT_TST_TYP_ID) = objCommon.GetEnumID("TEST TYPE", lstTestType)
                ElseIf lstTestType = "PNEUMATIC" Then
                    e.Values(EquipmentInformationData.NXT_TST_TYP_ID) = objCommon.GetEnumID("TEST TYPE", lstTestType)
                ElseIf lstTestType = "" Then
                    e.Values(EquipmentInformationData.NXT_TST_TYP_ID) = ""
                End If
            End If
            e.Values(EquipmentInformationData.CSC_VLDTY) = e.Values(EquipmentInformationData.CSC_VLDTY)
            e.Values(EquipmentInformationData.RMRKS_VC) = e.Values(EquipmentInformationData.RMRKS_VC)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentInformation_RowInserted"
    Protected Sub ifgEquipmentInformation_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgEquipmentInformation.RowInserted
        Try
            ifgEquipmentInformation.AllowSearch = True
            CacheData(EQUIPMENT_INFORMATION, dsEquipmentInformationData)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentInformation_RowUpdating"
    Protected Sub ifgEquipmentInformation_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgEquipmentInformation.RowUpdating
        Try
            Dim lstTestType As String
            Dim objCommon As New CommonData
            Dim intDepotID As Integer = objCommon.GetDepotID()
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            str_053GWS = objCommonConfig.pub_GetConfigSingleValue("053", intDepotID)
            bln_053GWSActive_Key = objCommonConfig.IsKeyExists
            If bln_053GWSActive_Key = True Then
                If str_053GWS.ToLower <> "true" Then
                    lstTestType = e.NewValues(EquipmentInformationData.LST_TST_TYP_CD).ToString.ToUpper
                    If lstTestType = "HYDRO" Then
                        e.NewValues(EquipmentInformationData.NXT_TST_TYP_ID) = objCommon.GetEnumID("TEST TYPE", "PNEUMATIC")
                    ElseIf lstTestType = "PNEUMATIC" Then
                        e.NewValues(EquipmentInformationData.NXT_TST_TYP_ID) = objCommon.GetEnumID("TEST TYPE", "HYDRO")
                    ElseIf lstTestType = "" Then
                        e.NewValues(EquipmentInformationData.NXT_TST_TYP_ID) = ""
                    End If
                End If
            End If

            e.OldValues(EquipmentInformationData.CSC_VLDTY) = e.NewValues(EquipmentInformationData.CSC_VLDTY)
            e.OldValues(EquipmentInformationData.RMRKS_VC) = e.NewValues(EquipmentInformationData.RMRKS_VC)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentInformation_RowDeleting"
    Protected Sub ifgEquipmentInformation_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgEquipmentInformation.RowDeleting
        Try
            Dim blnDelete As Boolean
            Dim ObjEquipInfo As New EquipmentInformation()
            Dim objCommon As New CommonData
            Dim dtEqTemp As New DataTable
            ifgEquipmentInformation.AllowSearch = True
            Dim lngEquipmentID As Int64 = e.InputParamters("EquipmentID").ToString()
            Dim strEquipmentNo As String = e.InputParamters("EquipmentNo").ToString()
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            If Not dsEquipmentInformationData Is Nothing Then
                If dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count > 0 Then
                    For Each dr1 As DataRow In dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows
                        If dr1.Item(EquipmentInformationData.EQPMNT_NO) = strEquipmentNo Then
                            dr1.Delete()
                        End If
                    Next
                End If
            End If
            CacheData(EQUIPMENT_INFORMATION, dsEquipmentInformationData)
            If strEquipmentNo <> Nothing Then
                blnDelete = ObjEquipInfo.pub_GetEquipmentInformationFromActivityStatus(strEquipmentNo, CInt(objCommon.GetDepotID()))

                If blnDelete Then
                    e.OutputParamters("Success") = String.Concat("Equipment : ", strEquipmentNo, " has been deleted from Equipment Information. Click submit to save changes.")
                Else
                    e.OutputParamters("Error") = String.Concat("Equipment : ", strEquipmentNo, " cannot be deleted from Equipment Information as it has been Invoiced (or) crossed Gate In.")
                    e.Cancel = True
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentInformation_RowDeleted"
    Protected Sub ifgEquipmentInformation_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgEquipmentInformation.RowDeleted
        Try
            dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            If dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows.Count = 0 Then
                Dim objCommon As New CommonData
                Dim intDepotID As Integer = objCommon.GetDepotID()
                Dim drEqInfo As DataRow = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).NewRow()

                Dim objCommonUI As New CommonUI()
                Dim dsEqpmntType As New CommonUIDataSet
                Dim objCommonConfig As New ConfigSetting()

                str_012EqType = objCommonConfig.pub_GetConfigSingleValue("012", intDepotID)
                bln_012EqType_Key = objCommonConfig.IsKeyExists

                If bln_012EqType_Key Then
                    If Not str_012EqType = "" Then
                        dsEqpmntType = objCommonUI.pub_GetEquipmentType(str_012EqType, intDepotID)
                        If dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                            drEqInfo.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = CommonWeb.GetNextIndex(dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION), EquipmentInformationData.EQPMNT_INFRMTN_ID)
                            drEqInfo.Item(EquipmentInformationData.EQPMNT_TYP_ID) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                            drEqInfo.Item(EquipmentInformationData.EQPMNT_TYP_CD) = str_012EqType
                            drEqInfo.Item(EquipmentInformationData.EQPMNT_TYP_DSCRPTN_VC) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_DSCRPTN_VC).ToString
                            drEqInfo.Item(EquipmentInformationData.ACTV_BT) = True
                            drEqInfo.Item(EquipmentInformationData.ALLOW_EDIT) = True
                        End If
                    End If
                End If
                dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows.Add(drEqInfo)
                ifgEquipmentInformation.AllowSearch = False
            Else
                ifgEquipmentInformation.AllowSearch = True
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                    MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_HasChangeEquipmentInformation()"
    Private Sub pvt_HasChangeEquipmentInformation(ByRef br_dsEquipmentInformation As EquipmentInformationDataSet)
        Try
            Dim dtEqpInforDetail As New DataTable
            Dim distnictData As New DataTable
            dtEqpInforDetail = br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Clone()
            For Each drEqDetail As DataRow In br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows
                If drEqDetail.RowState <> DataRowState.Deleted Then
                    dtEqpInforDetail.ImportRow(drEqDetail)
                End If
            Next
            If br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count > 0 Then
                distnictData = br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).GetChanges()
                For Each drEqpInfoDetail As DataRow In distnictData.Rows
                    Dim drs As DataRow()
                    If drEqpInfoDetail.RowState = DataRowState.Deleted Then
                        drs = br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, "=", drEqpInfoDetail.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID, DataRowVersion.Original)), "")
                    Else
                        drs = br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, "=", drEqpInfoDetail.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID)), "")
                    End If
                    If drs.Length > 0 Then
                        drs(0).Item(EquipmentInformationData.HAS_CHANGE) = True
                    End If
                Next
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

    Protected Sub ifgEquipmentInformation_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgEquipmentInformation.RowUpdated
        Try
            CacheData(EQUIPMENT_INFORMATION, dsEquipmentInformationData)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
End Class