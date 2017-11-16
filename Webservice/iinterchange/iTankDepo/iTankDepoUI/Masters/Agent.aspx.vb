
Partial Class Masters_Agent
    Inherits Pagebase

#Region "Declaration"

    Private Const KEY_ID As String = "ID"
    Private Const KEY_MODE As String = "mode"
    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private pvt_lngID As Long
    Dim strCommissionDetailDuplicateRowCondition As String() = {AgentData.AGNT_EQPMNT_TYP_ID, AgentData.AGNT_EQPMNT_CD_CD}
    Dim dsAgent As AgentDataSet
    Dim dsEDISetting As AgentDataSet
    Private Const Agent As String = "Agent"
    Private Const Agent_EDI_SETTING As String = "Agent_EDI_SETTING"
    Dim dtAgentData As DataTable
    Dim blnKeyExistForNPT As Boolean
    Dim objCommonConfig As New ConfigSetting()

#End Region

#Region "Page_Load"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                pvt_SetChangesMade()
                Dim objCommon As New CommonData
                Dim intDepotID As Integer = objCommon.GetDepotID()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "fnGetData"
                pvt_GetData(e.GetCallbackValue("mode"))
            Case "CreateAgent"
                pvt_CreateAgent(e.GetCallbackValue("bv_strAGNT_CD"), _
                                   e.GetCallbackValue("bv_strAGNT_NAM"), _
                                   CInt(e.GetCallbackValue("bv_i64AGNT_CRRNCY_ID")), _
                                   e.GetCallbackValue("bv_strCNTCT_PRSN_NAM"),
                                   e.GetCallbackValue("bv_strCNTCT_ADDRSS"), _
                                   e.GetCallbackValue("bv_strBLLNG_ADDRSS"), _
                                   e.GetCallbackValue("bv_strZP_CD"), _
                                   e.GetCallbackValue("bv_strPHN_NO"), _
                                   e.GetCallbackValue("bv_strFX_NO"), _
                                   e.GetCallbackValue("bv_strINVCNG_EML_ID"), _
                                   e.GetCallbackValue("bv_strStorageTax"), _
                                   e.GetCallbackValue("bv_strHandlingTax"), _
                                   e.GetCallbackValue("bv_strServiceTax"), _
                                  CDec(e.GetCallbackValue("bv_decLBR_RT_PR_HR_NC")), _
                                   CBool(e.GetCallbackValue("bv_blnACTV_BT")), _
                                   e.GetCallbackValue("wfData"))

            Case "UpdateAgent"
                pvt_UpdateAgent(e.GetCallbackValue("ID"), _
                                   e.GetCallbackValue("bv_strAGNT_CD"), _
                                   e.GetCallbackValue("bv_strAGNT_NAM"), _
                                   CInt(e.GetCallbackValue("bv_i64AGNT_CRRNCY_ID")), _
                                   e.GetCallbackValue("bv_strCNTCT_PRSN_NAM"),
                                   e.GetCallbackValue("bv_strCNTCT_ADDRSS"), _
                                   e.GetCallbackValue("bv_strBLLNG_ADDRSS"), _
                                   e.GetCallbackValue("bv_strZP_CD"), _
                                   e.GetCallbackValue("bv_strPHN_NO"), _
                                   e.GetCallbackValue("bv_strFX_NO"), _
                                   e.GetCallbackValue("bv_strINVCNG_EML_ID"), _
                                   e.GetCallbackValue("bv_strStorageTax"), _
                                   e.GetCallbackValue("bv_strHandlingTax"), _
                                   e.GetCallbackValue("bv_strServiceTax"), _
                                   CDec(e.GetCallbackValue("bv_decLBR_RT_PR_HR_NC")), _
                                   CBool(e.GetCallbackValue("bv_blnACTV_BT")),
                                   e.GetCallbackValue("wfData"))

            Case "ValidateCode"
                pvt_ValidateAgentCode(e.GetCallbackValue("Code"))
        End Select
    End Sub
#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim sbAgent As New StringBuilder
            Dim objCommon As New CommonData
            Dim intDepotID As Integer = objCommon.GetDepotID()
            Dim strDepotCurrency As String = String.Empty
            strDepotCurrency = objCommon.GetDepotLocalCurrencyCode()
            If bv_strMode = MODE_EDIT Then
                sbAgent.Append(CommonWeb.GetTextValuesJSO(txtAgentCode, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_CD)))
                sbAgent.Append(CommonWeb.GetTextValuesJSO(txtAgentName, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_NAM)))
                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_CNTCT_PRSN_NAM) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtContactPersonName, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtContactPersonName, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_CNTCT_PRSN_NAM)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_CNTCT_ADDRSS) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtContactAddress, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtContactAddress, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_CNTCT_ADDRSS)))
                End If
                'for EDI Code

                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_BLLNG_ADDRSS) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtBillingAddress, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtBillingAddress, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_BLLNG_ADDRSS)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_ZP_CD) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtZipCode, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtZipCode, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_ZP_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_PHN_NO) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtPhoneNo, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtPhoneNo, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_PHN_NO)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_FX_NO) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtFax, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtFax, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_FX_NO)))
                End If

                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_INVCNG_EML_ID) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtEmailforInvoicing, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtEmailforInvoicing, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_INVCNG_EML_ID)))
                End If

                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_SERVC_TX) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtSrvc_Tx_Rt, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtSrvc_Tx_Rt, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_SERVC_TX)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_STORG_TX) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtStorage_Tx_Rt, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtStorage_Tx_Rt, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_STORG_TX)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_HANDLNG_TX) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtHndlng_Tx_Rt, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtHndlng_Tx_Rt, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_HANDLNG_TX)))
                End If

                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_LBR_RT_PR_HR_NC) Is Nothing Then
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtLbr_rt_pr_hr, ""))
                Else
                    sbAgent.Append(CommonWeb.GetTextValuesJSO(txtLbr_rt_pr_hr, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_LBR_RT_PR_HR_NC)))
                End If

                If PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_CRRNCY_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_CRRNCY_ID) <> "" Then
                    sbAgent.Append(CommonWeb.GetLookupValuesJSO(lkpAgentCurrency, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_CRRNCY_ID), PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_CRRNCY_CD)))
                Else
                    sbAgent.Append(CommonWeb.GetLookupValuesJSO(lkpAgentCurrency, "", ""))
                End If

                If strDepotCurrency <> "NULLABLE" And strDepotCurrency <> "" Then
                    sbAgent.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotCurrency, strDepotCurrency))
                Else
                    sbAgent.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotCurrency, "", ""))
                End If

                sbAgent.Append(CommonWeb.GetCheckboxValuesJSO(chkActiveBit, CBool(PageSubmitPane.pub_GetPageAttribute(AgentData.ACTV_BT))))
                sbAgent.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_ID), "');"))
                sbAgent.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAgentId, PageSubmitPane.pub_GetPageAttribute(AgentData.AGNT_ID)))
            Else
                dsAgent = New AgentDataSet
                sbAgent.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotCurrency, strDepotCurrency))
            End If
            pub_SetCallbackReturnValue("Message", sbAgent.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgChargeDetail_ClientBind"
    Protected Sub ifgChargeDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgChargeDetail.ClientBind
        Try
            Dim strWFData As String = e.Parameters("WFDATA").ToString()
            Dim strAgentID As String = e.Parameters("AgentID").ToString()
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(strWFData, CommonUIData.DPT_ID))
            Dim objAgent As New Agent
            dsAgent = objAgent.pub_GetAgentChargeDetailByAgentID(strAgentID)
            e.DataSource = dsAgent.Tables(AgentData._V_Agent_CHARGE_DETAIL)
            CacheData(Agent, dsAgent)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgChargeDetail_RowInserting"
    Protected Sub ifgChargeDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgChargeDetail.RowInserting
        Dim lngAgentChargeDetailID As Long
        Dim lngAgentID As Long
        Dim strPageMode As String
        Dim objAgent As New Agent
        lngAgentID = CLng(e.InputParamters("AgentID"))
        strPageMode = CStr(e.InputParamters("mode"))
        Try
            dsAgent = CType(RetrieveData(Agent), AgentDataSet)
            If CommonWeb.pub_IsRowAlreadyExists(dsAgent.Tables(AgentData._V_Agent_CHARGE_DETAIL), CType(e.Values, OrderedDictionary), strCommissionDetailDuplicateRowCondition, strPageMode, AgentData.AGNT_CHRG_DTL_ID, CInt(e.Values(AgentData.AGNT_CHRG_DTL_ID))) Then
                e.OutputParamters.Add("Error", "Same Combination cannot be entered when there is already record existing with the given combination.")
                e.Cancel = True
                Exit Sub
            Else
                If strPageMode = "edit" And CommonWeb.pub_IsRowAlreadyExists(objAgent.pub_GetAgentChargeDetailByAgentID(lngAgentID).Tables(AgentData._V_Agent_CHARGE_DETAIL), CType(e.Values, OrderedDictionary), strCommissionDetailDuplicateRowCondition, "new", AgentData.AGNT_CHRG_DTL_ID, CInt(e.Values(AgentData.AGNT_CHRG_DTL_ID))) Then
                    e.OutputParamters.Add("Error", "Same Combination cannot be entered when there is already record existing with the given combination.")
                    e.Cancel = True
                    Exit Sub
                Else
                    lngAgentChargeDetailID = CommonWeb.GetNextIndex(dsAgent.Tables(AgentData._V_Agent_CHARGE_DETAIL), AgentData.AGNT_CHRG_DTL_ID)
                    e.Values(AgentData.AGNT_CHRG_DTL_ID) = lngAgentChargeDetailID
                    e.Values(AgentData.AGNT_ID) = lngAgentID
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                       MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgChargeDetail_RowUpdating"
    Protected Sub ifgChargeDetail_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgChargeDetail.RowUpdating
        Dim objAgent As New Agent
        Try
            Dim strPageMode As String = CStr(e.InputParamters("mode"))
            dsAgent = CType(RetrieveData(Agent), AgentDataSet)
            If CommonWeb.pub_IsRowAlreadyExists(dsAgent.Tables(AgentData._V_Agent_CHARGE_DETAIL), CType(e.NewValues, OrderedDictionary), strCommissionDetailDuplicateRowCondition, "edit", AgentData.AGNT_CHRG_DTL_ID, CInt(e.OldValues(AgentData.AGNT_CHRG_DTL_ID))) Then
                e.OutputParamters.Add("Error", "Same Combination cannot be entered when there is already record existing with the given combination.")
                e.Cancel = True
                e.RequiresRebind = True
                Exit Sub
            Else
                If CommonWeb.pub_IsRowAlreadyExists(objAgent.pub_GetAgentChargeDetailByAgentID(CInt(e.OldValues(AgentData.AGNT_ID))).Tables(AgentData._V_Agent_CHARGE_DETAIL), CType(e.NewValues, OrderedDictionary), strCommissionDetailDuplicateRowCondition, "New", AgentData.AGNT_CHRG_DTL_ID, CInt(e.OldValues(AgentData.AGNT_CHRG_DTL_ID))) Then
                    e.OutputParamters.Add("Error", "Same Combination cannot be entered when there is already record existing with the given combination.")
                    e.Cancel = True
                    e.RequiresRebind = True
                    Exit Sub
                Else
                    e.NewValues(AgentData.AGNT_CHRG_DTL_ID) = e.OldValues(AgentData.AGNT_CHRG_DTL_ID)
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                     MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgChargeDetail_RowDeleting"
    Protected Sub ifgChargeDetail_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgChargeDetail.RowDeleting
        Try
            dsAgent = CType(RetrieveData(Agent), AgentDataSet)
            dtAgentData = dsAgent.Tables(AgentData._V_Agent_CHARGE_DETAIL).Copy()
            Dim lngAgentId As Long = CLng(e.InputParamters("AgentID"))
            Dim dr As DataRow() = dtAgentData.Select(String.Concat(AgentData.AGNT_ID, "=", lngAgentId))
            If dr.Length = 1 Then
                e.OutputParamters.Add("Error", "Atleast one charge detail should be present.")
                e.Cancel = True
                Exit Sub
            Else
                e.OutputParamters("Delete") = String.Concat("Agent Rates : Code ", dtAgentData.Rows(e.RowIndex).Item(AgentData.AGNT_EQPMNT_CD_CD).ToString, " has been be deleted from Agent. Click submit to save changes.")
            End If
            Dim lngAgentChargeDetailID As Long
            lngAgentChargeDetailID = CLng(dsAgent.Tables(AgentData._V_Agent_CHARGE_DETAIL).Rows(e.RowIndex)(AgentData.AGNT_CHRG_DTL_ID).ToString)
            For Each drChargeHead As DataRow In dsAgent.Tables(AgentData._Agent_STORAGE_DETAIL).Select(String.Concat(AgentData.AGNT_CHRG_DTL_ID, "=", lngAgentChargeDetailID))
                drChargeHead.Delete()
            Next
            pub_CacheData(Agent, dsAgent)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgStorageDetail_Expanded"
    Protected Sub ifgStorageDetail_Expanded(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridExpandedEventArgs) Handles ifgStorageDetail.Expanded
        Dim intAgentChargeDetailID As Integer = Nothing
        Dim objAgent As New Agent
        Dim intAgentID As Integer
        Dim strFilter As String = String.Empty
        dsAgent = CType(RetrieveData(Agent), AgentDataSet)
        intAgentID = CInt(e.Parameters("AgentID").ToString())
        Try
            If e.Parameters("AgentChargeDetailID").ToString() <> String.Empty Then
                intAgentChargeDetailID = CInt(e.Parameters("AgentChargeDetailID"))
                strFilter = String.Concat(AgentData.AGNT_CHRG_DTL_ID, "=", intAgentChargeDetailID)
            End If
            If (strFilter <> String.Empty AndAlso dsAgent.Tables(AgentData._Agent_STORAGE_DETAIL).Select(strFilter).Length = 0) Then
                dtAgentData = objAgent.pub_GetAgentChargeDetailByAgentID(intAgentID, intAgentChargeDetailID).Tables(AgentData._Agent_STORAGE_DETAIL)
                dsAgent.Tables(AgentData._Agent_STORAGE_DETAIL).Merge(dtAgentData)
            End If
            Dim dv As DataView = dsAgent.Tables(AgentData._Agent_STORAGE_DETAIL).DefaultView
            dv.RowFilter = strFilter
            e.DataSource = dv
            CacheData(Agent, dsAgent)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgStorageDetail_RowInserting"
    Protected Sub ifgStorageDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgStorageDetail.RowInserting
        Dim lngAgentStorageDetailID As Long
        Dim lngAgentChargeDetailID As Long
        Dim lngAgentID As Long
        lngAgentID = CLng(e.InputParamters("AgentID"))
        lngAgentChargeDetailID = CLng(e.InputParamters("AgentChargeDetailID"))
        Try
            dsAgent = CType(RetrieveData(Agent), AgentDataSet)
            lngAgentStorageDetailID = CommonWeb.GetNextIndex(dsAgent.Tables(AgentData._Agent_STORAGE_DETAIL), AgentData.AGNT_STRG_DTL_ID)
            e.Values(AgentData.AGNT_STRG_DTL_ID) = lngAgentStorageDetailID
            e.Values(AgentData.AGNT_CHRG_DTL_ID) = lngAgentChargeDetailID
            e.Values(AgentData.AGNT_ID) = lngAgentID
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                     MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgStorageDetail_RowDeleting"
    Protected Sub ifgStorageDetail_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgStorageDetail.RowDeleting
        Try
            dsAgent = CType(RetrieveData(Agent), AgentDataSet)
            dtAgentData = dsAgent.Tables(AgentData._Agent_STORAGE_DETAIL).Copy()
            Dim intAgentChargeDetailID As Long = CLng(e.InputParamters("AgentChargeDetailID"))
            Dim dr As DataRow() = dtAgentData.Select(String.Concat(AgentData.AGNT_CHRG_DTL_ID, "=", intAgentChargeDetailID))
            Dim intCount = e.RowIndex + 1
            If dr.Length = 1 Then
                e.OutputParamters.Add("Error", "At least one storage detail must be present")
                e.Cancel = True
                Exit Sub
            Else
                e.OutputParamters("Delete") = String.Concat("Storage Charge : Up to Days ", dtAgentData.Rows(e.RowIndex).Item(AgentData.AGNT_UP_TO_DYS).ToString, " has been be deleted from Agent. Click submit to save changes.")
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateAgent"
    Public Function pvt_CreateAgent(ByVal bv_strAGNT_CD As String, _
                                       ByVal bv_strAGNT_NAM As String, _
                                       ByVal bv_i64AGNT_CRRNCY_ID As Int64, _
                                       ByVal bv_strCNTCT_PRSN_NAM As String, _
                                       ByVal bv_strCNTCT_ADDRSS As String, _
                                       ByVal bv_strBLLNG_ADDRSS As String, _
                                       ByVal bv_strZP_CD As String, _
                                       ByVal bv_strPHN_NO As String, _
                                       ByVal bv_strFX_NO As String, _
                                       ByVal bv_strINVCNG_EML_ID As String, _
                                       ByVal bv_strStorageTax As String, _
                                       ByVal bv_strHandlingTax As String, _
                                       ByVal bv_strServiceTax As String, _
                                       ByVal bv_decLBR_RT_PR_HR_NC As Decimal, _
                                       ByVal bv_blnACTV_BT As Boolean, _
                                       ByVal bv_strWfData As String) As Long
        Dim objcommon As New CommonData
        Try
            Dim objAgent As New Agent
            Dim lngCreated As Long
            Dim dtAgentChargeDetail As DataTable
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            dsAgent = CType(RetrieveData(Agent), AgentDataSet)
            dtAgentChargeDetail = CType(ifgChargeDetail.DataSource, DataTable)

            If dtAgentChargeDetail.Rows.Count = 0 Then
                pub_SetCallbackError("Agent Rate is a must for atleast an Equipment Code and Type.")
                pub_SetCallbackStatus(False)
                Return 0
            End If

            If Not pvt_CheckEmptyAgentStorageDetail(dtAgentChargeDetail.GetChanges()) Then
                pub_SetCallbackError("Storage Charge is a must for selected Equipment Code and Type.")
                pub_SetCallbackStatus(False)
                Return 0
            End If

            If bv_strCNTCT_ADDRSS.Contains("""") Or bv_strCNTCT_ADDRSS.Contains("'") Then
                pub_SetCallbackError("Single or Double Quotes are not allowed in Billing Address.")
                pub_SetCallbackStatus(False)
                Return 0
            End If

            If bv_strBLLNG_ADDRSS <> Nothing Then
                If bv_strBLLNG_ADDRSS.Contains("""") Or bv_strBLLNG_ADDRSS.Contains("'") Then
                    pub_SetCallbackError("Single or Double Quotes are not allowed in Billing Address.")
                    pub_SetCallbackStatus(False)
                    Return 0
                End If
            End If

            lngCreated = objAgent.pub_CreateAgent((bv_strAGNT_CD), _
                                                         bv_strAGNT_NAM, _
                                                         bv_i64AGNT_CRRNCY_ID, _
                                                         bv_strCNTCT_PRSN_NAM, _
                                                         bv_strCNTCT_ADDRSS, _
                                                         bv_strBLLNG_ADDRSS, _
                                                         bv_strZP_CD, _
                                                         bv_strPHN_NO, _
                                                         bv_strFX_NO, _
                                                         bv_strINVCNG_EML_ID, _
                                                         bv_decLBR_RT_PR_HR_NC, _
                                                         strModifiedby, _
                                                         datModifiedDate, _
                                                         strModifiedby, _
                                                         datModifiedDate, _
                                                         bv_blnACTV_BT, _
                                                         intDepotID, _
                                                         bv_strStorageTax, _
                                                         bv_strHandlingTax, _
                                                         bv_strServiceTax, _
                                                         bv_strWfData, _
                                                         dsAgent)
            dsAgent.AcceptChanges()
            pub_SetCallbackReturnValue("ID", CStr(lngCreated))
            pub_SetCallbackReturnValue("Message", String.Concat("Agent : ", bv_strAGNT_CD, " ", strMSGINSERT))
            pub_SetCallbackStatus(True)
            Return lngCreated
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "pvt_UpdateAgent"
    Private Sub pvt_UpdateAgent(ByVal bv_strAGNT_ID As String, _
                                   ByVal bv_strAGNT_CD As String, _
                                   ByVal bv_strAGNT_NAM As String, _
                                   ByVal bv_i64AGNT_CRRNCY_ID As Int64, _
                                   ByVal bv_strCNTCT_PRSN_NAM As String, _
                                   ByVal bv_strCNTCT_ADDRSS As String, _
                                   ByVal bv_strBLLNG_ADDRSS As String, _
                                   ByVal bv_strZP_CD As String, _
                                   ByVal bv_strPHN_NO As String, _
                                   ByVal bv_strFX_NO As String, _
                                   ByVal bv_strINVCNG_EML_ID As String, _
                                   ByVal bv_strStorageTax As String, _
                                   ByVal bv_strHandlingTax As String, _
                                   ByVal bv_strServiceTax As String, _
                                   ByVal bv_decLBR_RT_PR_HR_NC As Decimal, _
                                   ByVal bv_blnACTV_BT As Boolean, _
                                   ByVal bv_strWfData As String)
        Try
            Dim objAgent As New Agent
            Dim boolUpdated As Boolean

            Dim objcommon As New CommonData
            Dim dtAgentChargeDetail As DataTable
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            dsAgent = CType(RetrieveData(Agent), AgentDataSet)
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))

            Dim dsAgentData As DataSet = CType(RetrieveData(Agent), AgentDataSet)
            dtAgentChargeDetail = CType(ifgChargeDetail.DataSource, DataTable)
            If Not pvt_CheckEmptyAgentStorageDetail(dtAgentChargeDetail) Then
                pub_SetCallbackError("Agent Rate is a must for atleast an Equipment Code and Type")
                pub_SetCallbackStatus(False)
                Exit Sub
            End If

            If bv_strCNTCT_ADDRSS.Contains("""") Or bv_strCNTCT_ADDRSS.Contains("'") Then
                pub_SetCallbackError("Single or Double Quotes are not allowed in Contact Address.")
                pub_SetCallbackStatus(False)
            End If

            If bv_strBLLNG_ADDRSS <> Nothing Then
                If bv_strBLLNG_ADDRSS.Contains("""") Or bv_strBLLNG_ADDRSS.Contains("'") Then
                    pub_SetCallbackError("Single or Double Quotes are not allowed in Billing Address.")
                    pub_SetCallbackStatus(False)
                End If
            End If


            boolUpdated = objAgent.pub_ModifyAgent(CLng(bv_strAGNT_ID), _
                                                        bv_strAGNT_CD, _
                                                        bv_strAGNT_NAM, _
                                                        bv_i64AGNT_CRRNCY_ID, _
                                                        bv_strCNTCT_PRSN_NAM, _
                                                        bv_strCNTCT_ADDRSS, _
                                                        bv_strBLLNG_ADDRSS, _
                                                        bv_strZP_CD, _
                                                        bv_strPHN_NO, _
                                                        bv_strFX_NO, _
                                                        bv_strINVCNG_EML_ID, _
                                                        bv_decLBR_RT_PR_HR_NC, _
                                                        strModifiedby, _
                                                        datModifiedDate, _
                                                        bv_blnACTV_BT, _
                                                        intDepotID, _
                                                        bv_strStorageTax, _
                                                        bv_strHandlingTax, _
                                                        bv_strServiceTax, _
                                                        bv_strWfData, _
                                                        dsAgent)
            dsAgent.AcceptChanges()
            pub_SetCallbackReturnValue("Message", String.Concat("Agent : ", bv_strAGNT_CD, " ", strMSGUPDATE))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "pvt_ValidateAgentCode"
    Public Sub pvt_ValidateAgentCode(ByVal bv_strAgentCode As String)

        Try
            Dim objAgent As New Agent
            Dim objCommon As New CommonData
            Dim intDepotID As Int32 = CInt(objCommon.GetDepotID())
            Dim blnValid As Boolean
            Dim strServiceType As String = String.Empty
            blnValid = objAgent.pub_GetServicePartnerByCode(bv_strAgentCode, strServiceType, intDepotID)
            If blnValid Then
                pub_SetCallbackReturnValue("valid", "true")
            Else

                pub_SetCallbackReturnValue("valid", "The code is already present for an existing Agent.")
               
            End If

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgChargeDetail, tabAgentRates)
        pub_SetGridChanges(ifgStorageDetail, tabAgentRates)
        CommonWeb.pub_AttachHasChanges(txtAgentCode)
        CommonWeb.pub_AttachHasChanges(txtAgentName)
        CommonWeb.pub_AttachHasChanges(txtContactPersonName)
        CommonWeb.pub_AttachDescMaxlength(txtContactAddress)
        CommonWeb.pub_AttachDescMaxlength(txtBillingAddress)
        CommonWeb.pub_AttachHasChanges(chkActiveBit)
        CommonWeb.pub_AttachHasChanges(txtZipCode)
        CommonWeb.pub_AttachHasChanges(txtPhoneNo)
        CommonWeb.pub_AttachHasChanges(txtFax)
        CommonWeb.pub_AttachHasChanges(txtLbr_rt_pr_hr)
        CommonWeb.pub_AttachHasChanges(txtHndlng_Tx_Rt)
        CommonWeb.pub_AttachHasChanges(txtStorage_Tx_Rt)
        CommonWeb.pub_AttachHasChanges(txtSrvc_Tx_Rt)
    End Sub
#End Region

#Region "pvt_CheckEmptyTarrifDetail"
    Private Function pvt_CheckEmptyAgentStorageDetail(ByVal bv_dtAgentChargeDetail As DataTable) As Boolean
        Dim intAgentChargeDetailID As Long
        Dim dr() As DataRow
        Try
            dsAgent = CType(RetrieveData(Agent), AgentDataSet)
            If Not bv_dtAgentChargeDetail Is Nothing Then
                For Each drStorage As DataRow In bv_dtAgentChargeDetail.Rows
                    If drStorage.RowState = DataRowState.Added Then
                        intAgentChargeDetailID = CLng(drStorage.Item(AgentData.AGNT_CHRG_DTL_ID))
                        dr = dsAgent.Tables(AgentData._Agent_STORAGE_DETAIL).Select(String.Concat(AgentData.AGNT_CHRG_DTL_ID, "= ", intAgentChargeDetailID))
                        If dr.Length = 0 Then
                            Return False
                        End If
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                               MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/Agent.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgChargeDetail_RowDataBound"
    Protected Sub ifgChargeDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgChargeDetail.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                If Not e.Row.DataItem Is Nothing Then
                    drv = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

    Protected Sub ifgChargeDetail_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgChargeDetail.RowInserted

    End Sub
End Class
