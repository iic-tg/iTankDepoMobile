
Partial Class Admin_IndexPattern
    Inherits Pagebase
#Region "Declarations"
    Private Const INDEX_PATTERN As String = "INDEX_PATTERN"
#End Region

#Region "Parameters"
    Public dsIndexPattern As New IndexPatternDataSet
    Public dtIndexPattern As New DataTable
#End Region

#Region "Page_Load"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                pvt_SetChangesMade()
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        Try
            CommonWeb.pub_AttachHasChanges(lkpScreenName)
            ' CommonWeb.pub_AttachHasChanges(txtSequenceNoStart)
            'CommonWeb.pub_AttachHasChanges(txtNoOfDigits)
            CommonWeb.pub_AttachHasChanges(txtIndexPattern)
            ' CommonWeb.pub_AttachHasChanges(lkpResetBasis)
            CommonWeb.pub_AttachHasChanges(txtSplitChar)
            ' CommonWeb.pub_AttachHasChanges(lkpIndexBasis)
            CommonWeb.pub_AttachHasChanges(chkActivebit)
            pub_SetGridChanges(ifgIndexPattern, "ITab1_0")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub
#End Region
#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Admin/IndexPattern.js", MyBase.Page)
            'CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "fnGetData"
                    pvt_GetData(e.GetCallbackValue("mode"))

                Case "CreateIndexPattern"
                    pvt_CreateIndexPattern(CInt(e.GetCallbackValue("ScreenNameId")), _
                                           CStr(e.GetCallbackValue("ScreenName")), _
                                           CStr(e.GetCallbackValue("TableName")), _
                                           e.GetCallbackValue("SequenceNoStart"), _
                                           CInt(e.GetCallbackValue("NoOfDigits")), _
                                           CStr(e.GetCallbackValue("IndexPatternActual")), _
                                           CStr(e.GetCallbackValue("IndexPattern")), _
                                           CInt(e.GetCallbackValue("ResetBasisId")), _
                                           CStr(e.GetCallbackValue("SplitChar")), _
                                           CInt(e.GetCallbackValue("IndexBasisId")), _
                                           CBool(e.GetCallbackValue("Active")))

                Case "UpdateIndexPattern"
                    pvt_UpdateIndexPattern(CInt(e.GetCallbackValue("ID")), _
                                           CInt(e.GetCallbackValue("ScreenNameId")), _
                                           CStr(e.GetCallbackValue("ScreenName")), _
                                           CStr(e.GetCallbackValue("TableName")), _
                                           e.GetCallbackValue("SequenceNoStart"), _
                                           CInt(e.GetCallbackValue("NoOfDigits")), _
                                           CStr(e.GetCallbackValue("IndexPatternActual")), _
                                           CStr(e.GetCallbackValue("IndexPattern")), _
                                           CInt(e.GetCallbackValue("ResetBasisId")), _
                                           CStr(e.GetCallbackValue("SplitChar")), _
                                           CInt(e.GetCallbackValue("IndexBasisId")), _
                                           CBool(e.GetCallbackValue("Active")))
                Case "ValidateScreen"
                    pvt_GetScreenAvailability(CInt(e.GetCallbackValue("ScreenNameId")))


            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                               MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetScreenAvailability"
    Private Sub pvt_GetScreenAvailability(ByVal bv_i32ScreenNameId As Int32)
        Try
            Dim objIndexPattern As New IndexPattern
            Dim i32ScreenCount As Int32 = 0
            i32ScreenCount = objIndexPattern.pub_GetScreenCountByScreenId(bv_i32ScreenNameId)
            pub_SetCallbackReturnValue("Message", i32ScreenCount)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim objIndexPattern As New IndexPattern
            Dim sbrIndexPattern As New StringBuilder
            Dim objCommon As New CommonData
            If bv_strMode = "edit" Then
                If CBool(PageSubmitPane.pub_GetPageAttribute(IndexPatternData.ACTV_BT)) = True Then
                    sbrIndexPattern.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute("ID"), "');"))
                    If PageSubmitPane.pub_GetPageAttribute(IndexPatternData.TBL_NAM) = Nothing Then
                        sbrIndexPattern.Append(CommonWeb.GetLookupValuesJSO(lkpScreenName, PageSubmitPane.pub_GetPageAttribute(IndexPatternData.SCRN_ID), PageSubmitPane.pub_GetPageAttribute(IndexPatternData.SCRN_NM), ""))
                    Else
                        sbrIndexPattern.Append(CommonWeb.GetLookupValuesJSO(lkpScreenName, PageSubmitPane.pub_GetPageAttribute(IndexPatternData.SCRN_ID), PageSubmitPane.pub_GetPageAttribute(IndexPatternData.SCRN_NM), PageSubmitPane.pub_GetPageAttribute(IndexPatternData.TBL_NAM)))
                    End If
                    If (PageSubmitPane.pub_GetPageAttribute(IndexPatternData.INDX_BSS_ID)) = Nothing Or (PageSubmitPane.pub_GetPageAttribute(IndexPatternData.INDX_BSS_ID)) = 0 Then
                        sbrIndexPattern.Append(CommonWeb.GetLookupValuesJSO(lkpResetBasis, "", ""))
                    Else
                        sbrIndexPattern.Append(CommonWeb.GetLookupValuesJSO(lkpResetBasis, PageSubmitPane.pub_GetPageAttribute(IndexPatternData.RST_BSS_ID), PageSubmitPane.pub_GetPageAttribute(IndexPatternData.RST_BSS_NM)))
                    End If

                    If (PageSubmitPane.pub_GetPageAttribute(IndexPatternData.INDX_BSS_ID)) = Nothing Then
                        sbrIndexPattern.Append(CommonWeb.GetLookupValuesJSO(lkpIndexBasis, "", ""))
                    Else
                        sbrIndexPattern.Append(CommonWeb.GetLookupValuesJSO(lkpIndexBasis, PageSubmitPane.pub_GetPageAttribute(IndexPatternData.INDX_BSS_ID), PageSubmitPane.pub_GetPageAttribute(IndexPatternData.INDX_BSS_NM)))
                    End If
                    'sbrIndexPattern.Append(CommonWeb.GetTextValuesJSO(txtSequenceNoStart, PageSubmitPane.pub_GetPageAttribute(IndexPatternData.SQNC_NO_STRT)))
                    sbrIndexPattern.Append(CommonWeb.GetTextValuesJSO(txtNoOfDigits, PageSubmitPane.pub_GetPageAttribute(IndexPatternData.NO_OF_DGT)))
                    sbrIndexPattern.Append(CommonWeb.GetTextValuesJSO(txtIndexPattern, PageSubmitPane.pub_GetPageAttribute(IndexPatternData.INDX_PTTRN_ACTL_FRMT)))
                    sbrIndexPattern.Append(CommonWeb.GetHiddenTextValuesJSO(hdnIndexPattern, PageSubmitPane.pub_GetPageAttribute(IndexPatternData.INDX_PTTRN_FRMT)))
                    If (PageSubmitPane.pub_GetPageAttribute(IndexPatternData.SPLT_CHR)) = Nothing Then
                        sbrIndexPattern.Append(CommonWeb.GetTextValuesJSO(txtSplitChar, ""))
                    Else
                        sbrIndexPattern.Append(CommonWeb.GetTextValuesJSO(txtSplitChar, PageSubmitPane.pub_GetPageAttribute(IndexPatternData.SPLT_CHR)))
                    End If
                    sbrIndexPattern.Append(CommonWeb.GetCheckboxValuesJSO(chkActivebit, CBool(PageSubmitPane.pub_GetPageAttribute(IndexPatternData.ACTV_BT))))
                    sbrIndexPattern.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEditBit, CBool(PageSubmitPane.pub_GetPageAttribute(IndexPatternData.EDT_BT))))
                    If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                        dsIndexPattern = objIndexPattern.pub_GetIndexPatternDetailByIndexPatternId(CInt(PageSubmitPane.pub_GetPageAttribute("ID")), CInt(objCommon.GetHeadQuarterID()))
                    Else
                        dsIndexPattern = objIndexPattern.pub_GetIndexPatternDetailByIndexPatternId(CInt(PageSubmitPane.pub_GetPageAttribute("ID")), CInt(objCommon.GetDepotID()))
                    End If
                    sbrIndexPattern.Append(CommonWeb.GetHiddenTextValuesJSO(hdnActiveBit, CBool(PageSubmitPane.pub_GetPageAttribute(IndexPatternData.ACTV_BT))))
                End If
            Else
                dsIndexPattern.Tables.Clear()
                ' dsIndexPattern = objIndexPattern.pub_GetIndexPatternDetailByIndexPatternId(CInt(PageSubmitPane.pub_GetPageAttribute("ID")), CInt(objCommon.GetDepotID()))
                sbrIndexPattern.Append(CommonWeb.GetHiddenTextValuesJSO(hdnActiveBit, True))
            End If

            CacheData(INDEX_PATTERN, dsIndexPattern)
            pub_SetCallbackReturnValue("Message", sbrIndexPattern.ToString)
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgIndexPattern_ClientBind"
    ' Protected Sub ifgEquipmentDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentDetail.ClientBind
    Protected Sub ifgIndexPattern_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgIndexPattern.ClientBind
        Try
            Dim strFetchMode As String = CStr(e.Parameters("mode"))
            Dim blnActiveBit As Boolean = CBool(e.Parameters("Activebit"))
            Dim strPageMode As String = CStr(e.Parameters("pageMode"))
            If strFetchMode = "new" Then
                dsIndexPattern.Tables(IndexPatternData._V_INDEX_PATTERN_DETAIL).Rows.Clear()
            ElseIf strFetchMode = "edit" Then
                dsIndexPattern = CType(RetrieveData(INDEX_PATTERN), IndexPatternDataSet)
            End If
            If blnActiveBit = False And strPageMode = "edit" Then
                ifgIndexPattern.AllowAdd = False
                ifgIndexPattern.AllowDelete = False
                ifgIndexPattern.AllowEdit = False
            Else
                ifgIndexPattern.AllowAdd = True
                ifgIndexPattern.AllowDelete = True
                ifgIndexPattern.AllowEdit = True
            End If
            e.DataSource = dsIndexPattern.Tables(IndexPatternData._V_INDEX_PATTERN_DETAIL)
            CacheData(INDEX_PATTERN, dsIndexPattern)
            'CacheData("splitChar", e.Parameters("splitChar"))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateIndexPattern"
    Private Sub pvt_CreateIndexPattern(ByVal bv_i32ScreenNameId As Int32, _
                                       ByVal bv_strScreenName As String, _
                                       ByVal bv_strTableName As String, _
                                       ByVal bv_strSequenceNoStart As String, _
                                       ByVal bv_i16NoOfDigits As Int16, _
                                       ByVal bv_strIndexPatternActual As String, _
                                       ByVal bv_strIndexPattern As String, _
                                       ByVal bv_i32ResetBasisId As Int32, _
                                       ByVal bv_strSplitChar As String, _
                                       ByVal bv_i32IndexBasisId As Int32, _
                                       ByVal bv_blnActive As Boolean)
        Try
            Dim objIndexPattern As New IndexPattern
            Dim bln_FinanceIntegration As Boolean
            Dim i32IndexPatternId As Int32 = 0
            Dim objCommon As New CommonData
            dsIndexPattern = CType(RetrieveData(INDEX_PATTERN), IndexPatternDataSet)
            Dim strInvoiceScreenID As String = "1,2,5,6,7,8,9,10,12"
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" AndAlso objCommon.GetConfigSetting("043", bln_FinanceIntegration) Then
                If strInvoiceScreenID.Contains(bv_i32ScreenNameId) Then
                    If dsIndexPattern.Tables(IndexPatternData._V_INDEX_PATTERN_DETAIL).Select(String.Concat(IndexPatternData.PRMTR_ID, "=5")).Length = 0 Then
                        pub_SetCallbackStatus(False)
                        pub_SetCallbackError("Depot Code should be configured in Index Pattern")
                        Exit Sub
                    End If
                End If
            End If
           

            i32IndexPatternId = objIndexPattern.pub_CreateIndexPattern(bv_i32ScreenNameId, _
                                                                        bv_strTableName, _
                                                                        bv_strSequenceNoStart, _
                                                                        bv_i16NoOfDigits, _
                                                                        bv_strIndexPatternActual, _
                                                                        bv_strIndexPattern, _
                                                                        bv_i32ResetBasisId, _
                                                                        bv_strSplitChar, _
                                                                        bv_i32IndexBasisId, _
                                                                        bv_blnActive, _
                                                                        objCommon.GetCurrentUserName(), _
                                                                        objCommon.pub_GetDate(), _
                                                                        CInt(objCommon.GetDepotID()), _
                                                                        dsIndexPattern)
            dsIndexPattern.AcceptChanges()

            If i32IndexPatternId <> 0 Then
                pub_SetCallbackReturnValue("Message", String.Concat("Index Pattern : ", bv_strScreenName, " Inserted Successfully"))
                pub_SetCallbackReturnValue("ID", CStr(i32IndexPatternId))
            Else
                pub_SetCallbackReturnValue("Message", "")
            End If
            pub_SetCallbackStatus(True)
            CacheData(INDEX_PATTERN, dsIndexPattern)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "pvt_UpdateIndexPattern"
    Private Sub pvt_UpdateIndexPattern(ByRef br_i32IndexPatternId As Int32, _
                                       ByVal bv_i32ScreenNameId As Int32, _
                                       ByVal bv_strScreenName As String, _
                                       ByVal bv_strTableName As String, _
                                       ByVal bv_strSequenceNoStart As String, _
                                       ByVal bv_i16NoOfDigits As Int16, _
                                       ByVal bv_strIndexPatternActual As String, _
                                       ByVal bv_strIndexPattern As String, _
                                       ByVal bv_i32ResetBasisId As Int32, _
                                       ByVal bv_strSplitChar As String, _
                                       ByVal bv_i32IndexBasisId As Int32, _
                                       ByVal bv_blnActive As Boolean)
        Try
            Dim objIndexPattern As New IndexPattern
            Dim bln_FinanceIntegration As Boolean
            Dim blnValid As Boolean
            Dim objCommon As New CommonData
            dsIndexPattern = CType(RetrieveData(INDEX_PATTERN), IndexPatternDataSet)
            Dim strInvoiceScreenID As String = "1,2,5,6,7,8,9,10,12"
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" AndAlso objCommon.GetConfigSetting("043", bln_FinanceIntegration) Then
                If strInvoiceScreenID.Contains(bv_i32ScreenNameId) Then
                    If dsIndexPattern.Tables(IndexPatternData._V_INDEX_PATTERN_DETAIL).Select(String.Concat(IndexPatternData.PRMTR_ID, "=5")).Length = 0 Then
                        pub_SetCallbackStatus(False)
                        pub_SetCallbackError("Depot Code should be configured in Index Pattern")
                        Exit Sub
                    End If
                End If
            End If
            blnValid = objIndexPattern.pub_UpdateIndexPattern(br_i32IndexPatternId, _
                                                              bv_i32ScreenNameId, _
                                                              bv_strTableName, _
                                                                bv_strSequenceNoStart, _
                                                                bv_i16NoOfDigits, _
                                                                bv_strIndexPatternActual, _
                                                                bv_strIndexPattern, _
                                                                bv_i32ResetBasisId, _
                                                                bv_strSplitChar, _
                                                                bv_i32IndexBasisId, _
                                                                bv_blnActive, _
                                                                objCommon.GetCurrentUserName(), _
                                                                objCommon.pub_GetDate(), _
                                                                CInt(objCommon.GetDepotID()), _
                                                                dsIndexPattern)
            dsIndexPattern.AcceptChanges()
            If blnValid = True Then
                If bv_blnActive = True Then
                    pub_SetCallbackReturnValue("Message", String.Concat("Index Pattern : ", bv_strScreenName, " Updated Successfully"))
                Else
                    pub_SetCallbackReturnValue("Message", String.Concat("Index Pattern : ", bv_strScreenName, " DeActivated Successfully"))
                End If
                pub_SetCallbackReturnValue("ID", CStr(br_i32IndexPatternId))
            Else
                pub_SetCallbackReturnValue("Message", "")
            End If
            pub_SetCallbackStatus(True)
            CacheData(INDEX_PATTERN, dsIndexPattern)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try


    End Sub
#End Region

#Region "ifgIndexPattern_RowDataBound"
    Protected Sub ifgIndexPattern_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgIndexPattern.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                If Not e.Row.DataItem Is Nothing Then
                    Dim i32ParameterId As Int32 = CInt(drv.Item(IndexPatternData.PRMTR_ID))
                    'Validate 1 - Pre-Defined Values , 2 - Month , 3 - Year To Make Readonly
                    'Validate 1 - Pre-Defined Values , 3 - Month , 4 - Year To Make Readonly
                    If i32ParameterId = 1 Or i32ParameterId = 2 Or i32ParameterId = 3 Then
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        'CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        'CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        ' validate 5 - Depot, 4 - Sequence No To Make Readonly
                    ElseIf i32ParameterId = 4 Or i32ParameterId = 5 Then
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        'CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        'CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    Else
                        'CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        'CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        ' CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgIndexPattern_RowDeleting"
    Protected Sub ifgIndexPattern_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgIndexPattern.RowDeleting
        Try
            If CType(ifgIndexPattern.DataSource, DataTable).Select(String.Concat(IndexPatternData.INDX_PTTRN_DTL_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                e.OutputParamters("Delete") = "Index Pattern(s) cannot be deleted"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgIndexPattern_RowInserting"

    Protected Sub ifgIndexPattern_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgIndexPattern.RowInserting
        Try
            dsIndexPattern = CType(RetrieveData(INDEX_PATTERN), IndexPatternDataSet)
            dtIndexPattern = dsIndexPattern.Tables(IndexPatternData._V_INDEX_PATTERN_DETAIL)
            Dim lngIndexPatternDetailId As Long
            lngIndexPatternDetailId = CommonWeb.GetNextIndex(dtIndexPattern, IndexPatternData.INDX_PTTRN_DTL_ID)
            e.Values(IndexPatternData.INDX_PTTRN_DTL_ID) = lngIndexPatternDetailId
            Dim strFieldName As String = e.Values(IndexPatternData.FLD_NM)
            Dim strResult = strFieldName
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgIndexPattern_RowInserted"
    Protected Sub ifgIndexPattern_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgIndexPattern.RowInserted
        e.OutputParamters("Message") = "Success"
        '   CacheData(INDEX_PATTERN, dsIndexPattern)
    End Sub
#End Region

#Region "ifgIndexPattern_RowUpdating"

    Protected Sub ifgIndexPattern_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgIndexPattern.RowUpdating
        Try
            dsIndexPattern = CType(RetrieveData(INDEX_PATTERN), IndexPatternDataSet)
            e.NewValues(IndexPatternData.INDX_PTTRN_DTL_ID) = e.OldValues(IndexPatternData.INDX_PTTRN_DTL_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgIndexPattern_RowUpdated"
    Protected Sub ifgIndexPattern_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgIndexPattern.RowUpdated
        e.OutputParamters("Message") = "Success"
      
    End Sub
#End Region

#Region "ifgIndexPattern_RowDeleted"
    Protected Sub ifgIndexPattern_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgIndexPattern.RowDeleted
        e.OutputParamters("Message") = "Success"
      
    End Sub
#End Region
End Class
