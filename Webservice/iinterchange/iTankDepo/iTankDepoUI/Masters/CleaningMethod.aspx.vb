
Partial Class Masters_CleaningMethod
    Inherits Pagebase

#Region "Declaration"
    Dim dsCleaningMethod As New CleaningMethodDataSet
    Dim CLEANINGMETHOD As String = "CLEANINGMETHOD"
    Private strMSGDUPLICATE As String = "This Cleaning Type already exists."
    Private strMSGINSERT As String = " Inserted Successfully."
    Private strMSGUPDATE As String = " Updated Successfully."
    Dim dtCleaningMethod As DataTable
    Dim strCleaningMethodDuplicateRowCondition As String() = {CleaningMethodData.CLNNG_TYP_CD}
#End Region

#Region "Page_Load1"
    ''' <summary>
    ''' This event is fired on page load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "SetChangesMade"
    ''' <summary>
    ''' This method is to set changes to all the fields
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(txtType)
        CommonWeb.pub_AttachHasChanges(txtDescription)
        CommonWeb.pub_AttachHasChanges(chkActive)
        pub_SetGridChanges(ifgCleaningMethodDetail, "tabCleaningMethod")
    End Sub
#End Region

#Region "Page_PreRender1"
    ''' <summary>
    ''' This method is used to render scripts
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/CleaningMethod.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    ''' <summary>
    ''' This method is used to initialise call back methods
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "fnGetData"
                    pvt_GetData(e.GetCallbackValue("mode"))
                Case "ValidateType"
                    pvt_ValidateType(e.GetCallbackValue("Type"))
                Case "CreateCleaningMethod"
                    pvt_CreateCleaningMethod(e.GetCallbackValue("Type"), _
                                             e.GetCallbackValue("Description"), _
                                             e.GetCallbackValue("ActiveBit"))
                Case "UpdateCleaningMethod"
                    pvt_UpdateCleaningMethod(e.GetCallbackValue("ID"), _
                                            e.GetCallbackValue("Type"), _
                                            e.GetCallbackValue("Description"), _
                                            e.GetCallbackValue("ActiveBit"), _
                                            e.GetCallbackValue("wfData"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetData"
    ''' <summary>
    ''' This method is used to get date in edit mode and binding purpose
    ''' </summary>
    ''' <param name="bv_strMode"></param>
    ''' <remarks></remarks>
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim sbCleaningMethod As New StringBuilder
            If bv_strMode = MODE_EDIT Then
                sbCleaningMethod.Append(CommonWeb.GetTextValuesJSO(txtType, PageSubmitPane.pub_GetPageAttribute(CleaningMethodData.CLNNG_MTHD_TYP_CD)))
                sbCleaningMethod.Append(CommonWeb.GetTextValuesJSO(txtDescription, PageSubmitPane.pub_GetPageAttribute(CleaningMethodData.CLNNG_MTHD_TYP_DSCRPTN_VC)))
                sbCleaningMethod.Append(CommonWeb.GetCheckboxValuesJSO(chkActive, CBool(PageSubmitPane.pub_GetPageAttribute("Active"))))
                sbCleaningMethod.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(CleaningMethodData.CLNNG_MTHD_TYP_ID), "');"))
                pub_SetCallbackReturnValue("Message", sbCleaningMethod.ToString)
                pub_SetCallbackStatus(True)
            End If
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateType"
    ''' <summary>
    ''' This method is used to validate Type
    ''' </summary>
    ''' <param name="bv_strType"></param>
    ''' <remarks></remarks>
    Private Sub pvt_ValidateType(ByVal bv_strType As String)
        Try
            Dim objCleaningMethod As New CleaningMethod
            Dim objCommonData As New CommonData
            Dim strValidateMessage As String = ""
            Dim intDepotId As Int32 = objCommonData.GetDepotID()
            Dim blnTypeCheck As Boolean = objCleaningMethod.pub_GetCleaningMethodTypeCode(bv_strType, intDepotId)
            If blnTypeCheck = True Then
                strValidateMessage = "Cleaning Method already exists"
            End If
            pub_SetCallbackReturnValue("Message", strValidateMessage)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateCleaningMethod"
    ''' <summary>
    ''' This method is used to create cleaning method
    ''' </summary>
    ''' <param name="bv_strType"></param>
    ''' <param name="bv_strDescription"></param>
    ''' <param name="bv_strActiveBit"></param>
    ''' <remarks></remarks>
    Private Sub pvt_CreateCleaningMethod(ByVal bv_strType As String, _
                                      ByVal bv_strDescription As String, _
                                      ByVal bv_strActiveBit As String)
        Dim objCleaningMethod As New CleaningMethod
        Dim objCommonData As New CommonData
        Try

            Dim intDepotID As Integer
            If objCommonData.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommonData.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommonData.GetDepotID())
            End If
            Dim strModifiedby As String = objCommonData.GetCurrentUserName()
            Dim strModifiedDate As String = objCommonData.GetCurrentDate()
            Dim lngCleaningTypeId As Long = 0
            dsCleaningMethod = CType(RetrieveData(CLEANINGMETHOD), CleaningMethodDataSet)
            ''Detail entry validation
            If dsCleaningMethod.Tables(CleaningMethodData._V_CLEANING_METHOD_DETAIL).Rows.Count < 1 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("At least one cleaning method detail is mandatory.")
                Exit Sub
            End If
            lngCleaningTypeId = objCleaningMethod.pub_CreateCleaningMethod(lngCleaningTypeId, bv_strType, _
                                                                          bv_strDescription, _
                                                                          CBool(bv_strActiveBit), _
                                                                          strModifiedby, _
                                                                          CDate(strModifiedDate), _
                                                                          strModifiedby, _
                                                                          CDate(strModifiedDate), _
                                                                          intDepotId, _
                                                                          dsCleaningMethod)

            pub_SetCallbackReturnValue("Message", String.Concat("Cleaning Method : ", bv_strType, strMSGINSERT))
            pub_SetCallbackReturnValue("ID", lngCleaningTypeId)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        Finally
            objCleaningMethod = Nothing
            objCommonData = Nothing
        End Try
    End Sub
#End Region

#Region "pvt_UpdateCleaningMethod"
    ''' <summary>
    ''' This method is used to update cleaning method
    ''' </summary>
    ''' <param name="bv_strTypeID"></param>
    ''' <param name="bv_strType"></param>
    ''' <param name="bv_strDescription"></param>
    ''' <param name="bv_strActiveBit"></param>
    ''' <param name="bv_strWfData"></param>
    ''' <remarks></remarks>
    Private Sub pvt_UpdateCleaningMethod(ByVal bv_strTypeID As String, _
                                       ByVal bv_strType As String, _
                                      ByVal bv_strDescription As String, _
                                      ByVal bv_strActiveBit As String, ByVal bv_strWfData As String)
        Dim objCleaningMethod As New CleaningMethod
        Dim objCommonData As New CommonData
        Try

            Dim intDepotID As Integer
            If objCommonData.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommonData.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            End If
            Dim strModifiedby As String = objCommonData.GetCurrentUserName()
            Dim strModifiedDate As String = objCommonData.GetCurrentDate()
            dsCleaningMethod = CType(RetrieveData(CLEANINGMETHOD), CleaningMethodDataSet)
            ''Detail entry validation
            If dsCleaningMethod.Tables(CleaningMethodData._V_CLEANING_METHOD_DETAIL).Rows.Count < 1 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("At least one cleaning method detail is mandatory.")
                Exit Sub
            End If
            objCleaningMethod.pub_UpdateCleaningMethod(bv_strTypeID, bv_strType, _
                                                                          bv_strDescription, _
                                                                          CBool(bv_strActiveBit), _
                                                                          strModifiedby, _
                                                                          CDate(strModifiedDate), _
                                                                          strModifiedby, _
                                                                          CDate(strModifiedDate), _
                                                                          intDepotID, _
                                                                          dsCleaningMethod)
            dsCleaningMethod.AcceptChanges()
            pub_SetCallbackReturnValue("Message", String.Concat("Cleaning Method : ", bv_strType, strMSGUPDATE))
            pub_SetCallbackReturnValue("ID", bv_strTypeID)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        Finally
            objCleaningMethod = Nothing
            objCommonData = Nothing
        End Try
    End Sub
#End Region

#Region "ifgCleaningMethodDetail_ClientBind"
    ''' <summary>
    ''' This method is used to bind grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ifgCleaningMethodDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgCleaningMethodDetail.ClientBind
        Try
            Dim objCommonData As New CommonData
            Dim objCleaningMethod As New CleaningMethod
            If e.Parameters("Mode") <> Nothing Then
                If e.Parameters("Mode") = MODE_NEW Then
                    dsCleaningMethod = New CleaningMethodDataSet

                ElseIf e.Parameters("Mode") = MODE_EDIT Then
                    If e.Parameters("CleaningMethodTypeID") <> Nothing Then
                        dsCleaningMethod = objCleaningMethod.pub_GetCleaningMethodDetail(CLng(e.Parameters("CleaningMethodTypeID")))
                    End If
                End If
            End If
            e.DataSource = dsCleaningMethod.Tables(CleaningMethodData._V_CLEANING_METHOD_DETAIL)
            CacheData(CLEANINGMETHOD, dsCleaningMethod)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCleaningMethodDetail_RowInserting"

    Protected Sub ifgCleaningMethodDetail_RowDataBound(sender As Object, e As iFlexGridRowEventArgs) Handles ifgCleaningMethodDetail.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowIndex > 1 Then
                Dim lkpCleaningMethod As iLookup
                lkpCleaningMethod = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                lkpCleaningMethod.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
            End If
        End If
    End Sub


    ''' <summary>
    ''' VALIDATION: This method is used to validate duplicate data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ifgCleaningMethodDetail_RowInserting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgCleaningMethodDetail.RowInserting
        Try
            dsCleaningMethod = CType(RetrieveData(CLEANINGMETHOD), CleaningMethodDataSet)
            Dim dtCleaningMethod As DataTable = dsCleaningMethod.Tables(CleaningMethodData._V_CLEANING_METHOD_DETAIL).Copy
            dtCleaningMethod.AcceptChanges()
            Dim intResultIndex() As System.Data.DataRow = dtCleaningMethod.Select(String.Concat(CleaningMethodData.CLNNG_TYP_ID, "='", e.Values(CleaningMethodData.CLNNG_TYP_ID), "' AND ", CleaningMethodData.CLNNG_MTHD_DTL_ID, " is not null"))
            If intResultIndex.Length > 0 Then
                e.OutputParamters.Add("Duplicate", "Cleaning Type already added ")
                e.Cancel = True
                Exit Sub
            End If
            e.Values(CleaningMethodData.CLNNG_MTHD_DTL_ID) = CommonWeb.GetNextIndex(dsCleaningMethod.Tables(CleaningMethodData._V_CLEANING_METHOD_DETAIL), CleaningMethodData.CLNNG_MTHD_DTL_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
