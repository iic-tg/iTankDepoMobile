
Partial Class Masters_CustomerTariffCode
    Inherits Pagebase

#Region "Declaration"

    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Private strMSGDUPLICATE As String = "This Combination Tariff Type already exists."
    Dim dsCustomerTariffCode As New CustomerTariffCodeDataSet
    Private Const TARIFF_CODE_DETAIL As String = "TARIFF_CODE_DETAIL"
    Dim dtCustomerTariffData As DataTable
    Dim bln_045KeyExist As Boolean
    Dim bln_044KeyExist As Boolean
    Dim str_044KeyValue As String
    Dim str_045KeyValue As String

#End Region

#Region "Parameters"

    Private pvt_strPageMode As String
    Private pvt_intID As Integer

#End Region

#Region "Page_Load"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            pvt_SetChangesMade()
            ifgTariffType.DeleteButtonText = "Delete Row"
            ifgTariffType.RefreshButtonText = "Refresh"
            ifgTariffType.AddButtonText = "Add Row"

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"

    Private Sub pvt_SetChangesMade()
       
        CommonWeb.pub_AttachHasChanges(chkActiveBit)
     
    End Sub
#End Region

#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "fnGetData"
                pvt_GetData(e.GetCallbackValue("mode"))
            Case "CreateTariffCode"
                pvt_CreateTariffCode(e.GetCallbackValue("bv_i64TRFF_CD_TYP"), _
                                        e.GetCallbackValue("bv_i64TRFF_CD_EQP_TYP_ID"), _
                                        e.GetCallbackValue("bv_i64GTRFF_CD_CSTMR_ID"), _
                                        e.GetCallbackValue("bv_i64GTRFF_CD_AGNT_ID"), _
                                        e.GetCallbackValue("bv_blnACTV_BT"), _
                                        e.GetCallbackValue("PageMode"), _
                                        e.GetCallbackValue("wfData"))

            Case "UpdateTariffCode"
                pvt_UpdateTariffCode(e.GetCallbackValue("ID"), _
                                     e.GetCallbackValue("bv_i64TRFF_CD_TYP"), _
                                        e.GetCallbackValue("bv_i64TRFF_CD_EQP_TYP_ID"), _
                                        e.GetCallbackValue("bv_i64GTRFF_CD_CSTMR_ID"), _
                                        e.GetCallbackValue("bv_i64GTRFF_CD_AGNT_ID"), _
                                          e.GetCallbackValue("bv_blnACTV_BT"), _
                                        e.GetCallbackValue("PageMode"), _
                                        e.GetCallbackValue("wfData"))
            Case "ValidateCode"
                pvt_ValidateTariffCode(e.GetCallbackValue("bv_i64TRFF_CD_TYP"), _
                                        e.GetCallbackValue("bv_i64TRFF_CD_EQP_TYP_ID"), _
                                        e.GetCallbackValue("bv_i64GTRFF_CD_CSTMR_ID"), _
                                        e.GetCallbackValue("bv_i64GTRFF_CD_AGNT_ID"))
                'Case "ValidateTariffHeader"
                '    pvt_ValidateTariffHeader(e.GetCallbackValue("bv_i64TRFF_CD_TYP"), _
                '                            e.GetCallbackValue("bv_i64TRFF_CD_EQP_TYP_ID"))
        End Select
    End Sub
#End Region

#Region "pvt_ValidateProductCode"

    Public Sub pvt_ValidateTariffCode(ByVal bv_i64TRFF_CD_TYP As String, _
                                      ByVal bv_i64TRFF_CD_EQP_TYP_ID As Long, _
                                      ByVal bv_i64GTRFF_CD_CSTMR_ID As Long, _
                                      ByVal bv_i64GTRFF_CD_AGNT_ID As Long)

        Try
            Dim objCustomerTariffCode As New CustomerTariffCode
            dsCustomerTariffCode = objCustomerTariffCode.pub_GetCustomerTariffCodeHeaderByTariffEqpmntID(bv_i64TRFF_CD_TYP, bv_i64TRFF_CD_EQP_TYP_ID, bv_i64GTRFF_CD_CSTMR_ID, bv_i64GTRFF_CD_AGNT_ID)
            If dsCustomerTariffCode.Tables(CustomerTariffCodeData._TARIFF_CODE_HEADER).Rows.Count = 0 Then
                pub_SetCallbackReturnValue("valid", "true")
            Else
                pub_SetCallbackReturnValue("valid", "false")
            End If

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region


    '#Region "pvt_ValidateTariffHeader"

    '    Public Sub pvt_ValidateTariffHeader(ByVal bv_i64TRFF_CD_TYP As String, _
    '                                      ByVal bv_i64TRFF_CD_EQP_TYP_CD As Long)

    '        Try
    '            If bv_i64TRFF_CD_TYP = 143 Then
    '                lkpCSTMR.Validator.IsRequired = False
    '                lkpAgent.ReadOnly = False
    '                lkpCSTMR.ReadOnly = True
    '                lkpAgent.Validator.IsRequired = True
    '                lkpAgent.Validator.ReqErrorMessage = "Agent is Required"
    '            ElseIf bv_i64TRFF_CD_TYP = 142 Then
    '                lkpAgent.ReadOnly = True
    '                lkpCSTMR.ReadOnly = False
    '                lkpAgent.Validator.IsRequired = False
    '                lkpCSTMR.Validator.IsRequired = True
    '                lkpCSTMR.Validator.ReqErrorMessage = "Customer is Required"
    '            ElseIf bv_i64TRFF_CD_TYP = 141 Then
    '                lkpAgent.ReadOnly = True
    '                lkpCSTMR.ReadOnly = True
    '                lkpAgent.Validator.IsRequired = False
    '                lkpCSTMR.Validator.IsRequired = False
    '            End If
    '            ''Dim objCustomerTariffCode As New CustomerTariffCode
    '            ''dsCustomerTariffCode = objCustomerTariffCode.pub_GetCustomerTariffCodeHeaderByTariffEqpmntID(bv_i64TRFF_CD_TYP, bv_i64TRFF_CD_EQP_TYP_CD)
    '            'If dsCustomerTariffCode.Tables(CustomerTariffCodeData._TARIFF_CODE_HEADER).Rows.Count = 0 Then
    '            '    pub_SetCallbackReturnValue("valid", "true")
    '            'Else
    '            '    pub_SetCallbackReturnValue("valid", "false")
    '            'End If

    '            'pub_SetCallbackStatus(True)

    '        Catch ex As Exception
    '            pub_SetCallbackStatus(False)
    '            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '        End Try
    '    End Sub

    '#End Region


#Region "pvt_GetData"

    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim sbCustomerTariffCode As New StringBuilder
            If bv_strMode = MODE_EDIT Then
                If PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_TYP) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_TYP) <> "" Then
                    sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpTRRF_TYP, PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_TYP), PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_TYP_CD)))
                Else
                    sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpTRRF_TYP, "", ""))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_EQP_TYP_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_EQP_TYP_ID) <> "" Then
                    sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpEqpType, PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_EQP_TYP_ID), PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_EQP_TYP_CD)))
                Else
                    sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpEqpType, "", ""))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_TYP_CD) = "CUSTOMER" Then
                    If PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_CSTMR_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_CSTMR_ID) <> "" AndAlso IsDBNull(PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_CSTMR_CD)) = False Then
                        sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpCSTMR, PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_CSTMR_ID), PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_CSTMR_CD)))
                    Else
                        sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpCSTMR, "", ""))
                    End If
                Else
                    sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpCSTMR, "", ""))
                End If

                If PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_TYP_CD) = "AGENT" Then
                    If PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_AGNT_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_AGNT_ID) <> "" AndAlso IsDBNull(PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_AGNT_CD)) = False Then

                        sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpAgent, PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_AGNT_ID), PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_AGNT_CD)))
                    Else
                        sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpAgent, "", ""))
                    End If
                Else
                    sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpAgent, "", ""))
                End If

                If PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_TYP_CD) <> "AGENT" And PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_TYP_CD) <> "CUSTOMER" Then
                    sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpCSTMR, "", ""))
                    sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpAgent, "", ""))
                End If
                If Not PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.ACTV_BT) = Nothing Then
                    sbCustomerTariffCode.Append(CommonWeb.GetCheckboxValuesJSO(chkActiveBit, CBool(PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.ACTV_BT))))
                Else
                    sbCustomerTariffCode.Append(CommonWeb.GetCheckboxValuesJSO(chkActiveBit, "FALSE"))
                End If


                sbCustomerTariffCode.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_ID), "');"))
                sbCustomerTariffCode.Append(CommonWeb.GetHiddenTextValuesJSO(hdnTariffId, PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_ID)))
                'Dim strWFData As Integer = GetQueryString("strWFData")
                'Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(strWFData, CommonUIData.DPT_ID))
                Dim i32TariffCodeID As Int32 = CInt(PageSubmitPane.pub_GetPageAttribute(CustomerTariffCodeData.TRFF_CD_ID))
                Dim objCustomerTariffCode As New CustomerTariffCode
                dsCustomerTariffCode = objCustomerTariffCode.pub_GetCustomerTariffCodeDetailByTariffID(i32TariffCodeID)
            Else
                sbCustomerTariffCode.Append(CommonWeb.GetLookupValuesJSO(lkpCSTMR, "", ""))
                sbCustomerTariffCode.Append(CommonWeb.GetCheckboxValuesJSO(chkActiveBit, "TRUE"))
                sbCustomerTariffCode.Append(CommonWeb.GetHiddenTextValuesJSO(hdnTariffId, ""))
            End If
            CacheData(TARIFF_CODE_DETAIL, dsCustomerTariffCode)
            pub_SetCallbackReturnValue("Message", sbCustomerTariffCode.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_CreateTariffCode"

    Public Function pvt_CreateTariffCode(ByVal bv_i64TRFF_CD_TYP As String, _
                                            ByVal bv_i64TRFF_CD_EQP_TYP_ID As Int64, _
                                            ByVal bv_i64GTRFF_CD_CSTMR_ID As Int64, _
                                            ByVal bv_i64GTRFF_CD_AGNT_ID As Int64, _
                                            ByVal bv_blnACTV_BT As Boolean, _
                                            ByVal bv_strPageMode As String, _
                                            ByVal bv_strWfData As String) As Long
        Dim objcommon As New CommonData
        Try
            Dim objTariff As New CustomerTariffCode
            Dim lngCreated As Long
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))


            dsCustomerTariffCode = CType(RetrieveData(TARIFF_CODE_DETAIL), CustomerTariffCodeDataSet)

            lngCreated = objTariff.pub_CreateTariffCode(bv_i64TRFF_CD_TYP, _
                                                        bv_i64TRFF_CD_EQP_TYP_ID, _
                                                        bv_i64GTRFF_CD_CSTMR_ID, _
                                                        bv_i64GTRFF_CD_AGNT_ID, _
                                                        bv_blnACTV_BT, _
                                                        strModifiedby, _
                                                        datModifiedDate, _
                                                        strModifiedby, _
                                                        datModifiedDate, _
                                                        intDepotID, _
                                                        bv_strPageMode, _
                                                        bv_strWfData, _
                                                        dsCustomerTariffCode)

            dsCustomerTariffCode.AcceptChanges()


            pub_SetCallbackReturnValue("ID", CStr(lngCreated))
            pub_SetCallbackReturnValue("Message", strMSGINSERT)
            pub_SetCallbackStatus(True)
            Return lngCreated

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function

#End Region

#Region "pvt_UpdateTariffCode"

    Private Sub pvt_UpdateTariffCode(ByVal bv_strTRFF_ID As String, _
                                    ByVal bv_i64TRFF_CD_TYP As String, _
                                    ByVal bv_i64TRFF_CD_EQP_TYP_ID As Int64, _
                                    ByVal bv_i64GTRFF_CD_CSTMR_ID As Int64, _
                                    ByVal bv_i64GTRFF_CD_AGNT_ID As Int64, _
                                    ByVal bv_blnACTV_BT As Boolean, _
                                    ByVal bv_strPageMode As String, _
                                    ByVal bv_strWfData As String)
        Try
            Dim objCustomerTariffCode As New CustomerTariffCode
            Dim boolUpdated As Boolean

            Dim objcommon As New CommonData
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim dtAttachmentDetail As New DataTable
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))

            dsCustomerTariffCode = CType(RetrieveData(TARIFF_CODE_DETAIL), CustomerTariffCodeDataSet)
            'dsCustomerTariffCode.Tables(CustomerTariffCodeData._V_TARIFF_CODE_DETAIL).Rows.Clear()

            'dsCustomerTariffCode.Tables(CustomerTariffCodeData._V_TARIFF_CODE_DETAIL).Merge(ifgTariffType.DataSource)
            boolUpdated = objCustomerTariffCode.pub_ModifyCustomerTariffCode(CInt(bv_strTRFF_ID), _
                                                        bv_i64TRFF_CD_TYP, _
                                                        bv_i64TRFF_CD_EQP_TYP_ID, _
                                                        bv_i64GTRFF_CD_CSTMR_ID, _
                                                        bv_i64GTRFF_CD_AGNT_ID, _
                                                        bv_blnACTV_BT, _
                                                        strModifiedby, _
                                                        datModifiedDate, _
                                                        strModifiedby, _
                                                        datModifiedDate, _
                                                        intDepotID, _
                                                        bv_strPageMode, _
                                                        bv_strWfData, _
                                                        dsCustomerTariffCode)

            dsCustomerTariffCode.AcceptChanges()
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region


#Region "ifgTariffCode_ClientBind"

    Protected Sub ifgTariffType_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgTariffType.ClientBind
        Try
            Dim strWFData As String = e.Parameters("WFDATA").ToString()
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(strWFData, CommonUIData.DPT_ID))
            Dim strTariffID As String = e.Parameters("TariffID").ToString()
            Dim objCustomerTariffCode As New CustomerTariffCode
            If strTariffID = "" Then
                dsCustomerTariffCode = CType(RetrieveData(TARIFF_CODE_DETAIL), CustomerTariffCodeDataSet)
            Else
                dsCustomerTariffCode = objCustomerTariffCode.pub_GetCustomerTariffCodeDetailByTariffID(strTariffID)
            End If
            e.DataSource = dsCustomerTariffCode.Tables(CustomerTariffCodeData._V_TARIFF_CODE_DETAIL)
            CacheData(TARIFF_CODE_DETAIL, dsCustomerTariffCode)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub

#End Region

#Region "ifgTariffType_RowDataBound"
    Protected Sub ifgTariffType_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgTariffType.RowDataBound
        Try
            Dim objCommondata As New CommonData
            Dim strItemAlias As String
            str_044KeyValue = objCommondata.GetConfigSetting("044", bln_044KeyExist)
            str_045KeyValue = objCommondata.GetConfigSetting("045", bln_045KeyExist)
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)

                End If
            End If

            If e.Row.RowType = DataControlRowType.Header Then
                If bln_044KeyExist Then
                    CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat(str_044KeyValue, " *")
                    CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ToolTip = str_044KeyValue
                End If
                If bln_045KeyExist Then
                    CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = str_045KeyValue
                    CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ToolTip = str_045KeyValue
                End If
            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                If bln_044KeyExist Then
                    Dim lkpItemCode As iLookup = CType(DirectCast(DirectCast(e.Row.Cells(2),  _
                             iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    If str_044KeyValue.Length > 2 Then
                        If str_044KeyValue.Trim.Substring(str_044KeyValue.Length - 1, 1) = "*" Then
                            strItemAlias = str_044KeyValue.Trim.Substring(0, str_044KeyValue.Length - 2)
                        Else
                            strItemAlias = str_044KeyValue.Trim
                        End If
                    Else
                        strItemAlias = str_044KeyValue.Trim
                    End If

                    lkpItemCode.Validator.ReqErrorMessage = strItemAlias + " is Required"
                    lkpItemCode.Validator.LkpErrorMessage = "Invalid " + str_044KeyValue.Trim + ".Click on the List for Valid Values "
                    If str_045KeyValue.Trim = "Component" Then
                        lkpItemCode.HelpText = "599," + "SUB_ITEM_SB_ITM_CD"
                        'lkpItemCode.HelpText = "600," + "SUB_ITEM_SB_ITM_DSCRPTN_VC"
                    Else
                        lkpItemCode.HelpText = "212," + "SUB_ITEM_SB_ITM_CD"
                        'lkpItemCode.HelpText = "213," + "SUB_ITEM_SB_ITM_DSCRPTN_VC"
                    End If
                End If
                If bln_045KeyExist Then
                    Dim lkpSubItemCode As iLookup = CType(DirectCast(DirectCast(e.Row.Cells(3),  _
                            iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpSubItemCode.Validator.ReqErrorMessage = str_045KeyValue.Trim + " is Required"
                    lkpSubItemCode.Validator.LkpErrorMessage = "Invalid " + str_045KeyValue.Trim + ".Click on the List for Valid Values "
                    If str_045KeyValue.Trim = "Component" Then
                        lkpSubItemCode.HelpText = "597," + "SUB_ITEM_SB_ITM_CD"
                        'lkpSubItemCode.HelpText = "598," + "SUB_ITEM_SB_ITM_DSCRPTN_VC"
                    Else
                        lkpSubItemCode.HelpText = "214," + "SUB_ITEM_SB_ITM_CD"
                        'lkpSubItemCode.HelpText = "215," + "SUB_ITEM_SB_ITM_DSCRPTN_VC"
                    End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                 MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region


    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/CustomerTariffCode.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

    Protected Sub ifgTariffType_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgTariffType.RowDeleting

    End Sub

    Protected Sub ifgTariffType_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgTariffType.RowInserted
        Try

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

    Protected Sub ifgTariffType_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgTariffType.RowInserting
        Try
            Dim objCustomerTariffCode As New CustomerTariffCode
            dsCustomerTariffCode = CType(RetrieveData(TARIFF_CODE_DETAIL), CustomerTariffCodeDataSet)
            e.Values(CustomerTariffCodeData.TRFF_CD_DTL_ID) = CommonWeb.GetNextIndex(dsCustomerTariffCode.Tables(CustomerTariffCodeData._V_TARIFF_CODE_DETAIL), CustomerTariffCodeData.TRFF_CD_DTL_ID)
            Dim strPageMode As String = CStr(e.InputParamters("mode"))
            Dim strTariffID As String = CStr(e.InputParamters("TariffID"))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgTariffType_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgTariffType.RowUpdated
        Try
            'If dsCustomerTariffCode.Tables(CustomerTariffCodeData._V_TARIFF_CODE_DETAIL).Rows.Count > 0 Then

            'End If
            CacheData(TARIFF_CODE_DETAIL, dsCustomerTariffCode)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

    Protected Sub ifgTariffType_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgTariffType.RowUpdating
        Try

            dsCustomerTariffCode = CType(RetrieveData(TARIFF_CODE_DETAIL), CustomerTariffCodeDataSet)
            e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_ACTV_BT) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_ACTV_BT)
            ''e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_CD) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_CD)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_ID) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_ID)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_DSC) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_DSC)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_LCTN_CD) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_LCTN_CD)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_LCTN_ID) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_LCTN_ID)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_CMPNT_CD) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_CMPNT_CD)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_CMPNT_ID) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_CMPNT_ID)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_DMG_CD) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_DMG_CD)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_DMG_ID) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_DMG_ID)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_RPR_CD) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_RPR_CD)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_RPR_ID) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_RPR_ID)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_CD) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_CD)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_ID) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_ID)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_MNHR) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_MNHR)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_CST) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_CST)
            'e.OldValues(CustomerTariffCodeData.TRFF_CD_DTL_RMRKS) = e.NewValues(CustomerTariffCodeData.TRFF_CD_DTL_RMRKS)


        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
End Class

