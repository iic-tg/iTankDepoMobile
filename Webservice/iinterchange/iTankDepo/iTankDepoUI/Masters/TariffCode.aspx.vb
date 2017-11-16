
Partial Class Masters_TariffCode
    Inherits Pagebase
#Region "Declarations"
    Private strMSGUPDATE As String = "Tariff Code(s) Updated Successfully."
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private Const TariffCode As String = "TariffCode"
    Dim bln_045KeyExist As Boolean
    Dim bln_044KeyExist As Boolean
    Dim str_044KeyValue As String
    Dim str_045KeyValue As String
#End Region

#Region "Parameters"
    Public dsTariffCode As TariffCodeDataSet
    Public dtTariffCode As DataTable
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
#End Region

#Region "Page_Load"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objCommon As New CommonData()
            Dim strpagetitle As String = Request.QueryString("pagetitle")
            ifgTariffCode.DeleteButtonText = "Delete"
            ifgTariffCode.RefreshButtonText = "Refresh"
            ifgTariffCode.AddButtonText = "Add Tariff"
            ifgTariffCode.ActionButtons.Item(0).Text = "Upload"
            ifgTariffCode.ActionButtons.Item(1).Text = "Export"
            str_044KeyValue = objCommon.GetConfigSetting("044", bln_044KeyExist)
            str_045KeyValue = objCommon.GetConfigSetting("045", bln_045KeyExist)
            If bln_044KeyExist Then
                DirectCast(ifgTariffCode.Columns.Item(2), iInterchange.WebControls.v4.Data.LookupField).HeaderText = str_044KeyValue + " *"
            End If
            If bln_045KeyExist Then
                DirectCast(ifgTariffCode.Columns.Item(3), iInterchange.WebControls.v4.Data.LookupField).HeaderText = str_045KeyValue
            End If

            If Not Request.QueryString("Export") Is Nothing Then
                If Request.QueryString("Export") = "True" Then
                    Dim objstream As New IO.StringWriter
                    objstream = objCommon.ExportToExcel(CType(ifgTariffCode.DataSource, DataTable), "Tariff", "CODE,DESCRIPTION,ITEM_CODE,SUB_ITEM_CODE,DAMAGE_CODE,REPAIR_CODE,MAN_HOURS,MATERIAL_COST,REMARKS,ACTIVE", "TRFF_CD_ID", False)
                    Response.ContentType = "application/vnd.Excel"
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Tariff" + ".xls")
                    Response.Write(objstream.ToString())
                    Response.End()
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "ValidateCode"
                pvt_ValidateTariffCodeCode(e.GetCallbackValue("Code"), _
                               e.GetCallbackValue("GridIndex"), e.GetCallbackValue("RowState"), e.GetCallbackValue("WFDATA"))
            Case "UpdateTariffCode"
                pvt_UpdateTariffCode(e.GetCallbackValue("WFDATA"))
            Case "GetItemAliasErrorMessage"
                pvt_GetItemAliasErrorMessage()
        End Select
    End Sub
#End Region

#Region "pvt_ValidateTariffCodeCode"
    Private Sub pvt_ValidateTariffCodeCode(ByVal bv_strCode As String, ByVal bv_intGridRowIndex As Integer, ByVal bv_strRowState As String, ByVal bv_strWFDATA As String)
        Try
            Dim blnValid As Boolean
            Dim strExistCode As String = String.Empty
            dsTariffCode = CType(RetrieveData(TariffCode), TariffCodeDataSet)
            Dim dtTariffCode As DataTable = dsTariffCode.Tables(TariffCodeData._TARIFF_CODE).Copy
            dtTariffCode.AcceptChanges()

            'Checking whether the entered code is available in grid
            Dim intResultIndex() As System.Data.DataRow = dtTariffCode.Select("Code" & "='" & bv_strCode & "'")

            If intResultIndex.Length > 0 Then
                blnValid = False
            Else
                blnValid = True
            End If

            'Checking whether the entered code is available in database
            If blnValid = True AndAlso bv_strCode <> Nothing Then
                If bv_strRowState = "Added" Then
                    Dim objTariffCode As New TariffCode
                    blnValid = objTariffCode.pub_ValidateTariffCode(bv_strCode, bv_strWFDATA)
                End If
            End If

            If blnValid = True Then
                pub_SetCallbackReturnValue("pkValid", "true")
            Else
                pub_SetCallbackReturnValue("pkValid", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgTariffCode_ClientBind"
    Protected Sub ifgTariffCode_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgTariffCode.ClientBind
        Try
            Dim objTariffCode As New TariffCode
            Dim objCommondata As New CommonData
            Dim strWfData As String = CStr(e.Parameters("WFDATA"))
            ifgTariffCode.UseCachedDataSource = True
            If objCommondata.GetMultiLocationSupportConfig().ToLower = "true" Then
                strWfData = String.Concat("DPT_ID=", objCommondata.GetHeadQuarterID().ToString)
            End If
            dsTariffCode = objTariffCode.pub_TariffCodeGetTariffCodeByDepotID(strWfData)
            objTariffCode.pub_GetColumnAliasName(dsTariffCode)
            dtTariffCode = dsTariffCode.Tables(TariffCodeData._TARIFF_CODE)
            e.DataSource = dtTariffCode
            CacheData(TariffCode, dsTariffCode)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgTariffCode_RowInserting"
    Protected Sub ifgTariffCode_RowInserting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgTariffCode.RowInserting
        Try
            dsTariffCode = CType(RetrieveData(TariffCode), TariffCodeDataSet)
            dtTariffCode = dsTariffCode.Tables(TariffCodeData._TARIFF_CODE)
            e.Values(TariffCodeData.TRFF_CD_ID) = CommonWeb.GetNextIndex(dtTariffCode, TariffCodeData.TRFF_CD_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateTariffCode"
    Private Sub pvt_UpdateTariffCode(ByVal bv_strWFData As String)
        Try
            Dim objTariffCode As New TariffCode
            Dim objCommondata As New CommonData
            dtTariffCode = CType(ifgTariffCode.DataSource, DataTable)
            If objCommondata.GetMultiLocationSupportConfig().ToLower = "true" Then
                bv_strWFData = String.Concat("DPT_ID=", objCommondata.GetHeadQuarterID().ToString)
            End If
            objTariffCode.pub_UpdateTariffCode(CType(dtTariffCode.DataSet, TariffCodeDataSet), objCommondata.GetCurrentUserName(), CDate(objCommondata.GetCurrentDate()), bv_strWFData)
            dtTariffCode.AcceptChanges()
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgTariffCode_RowDataBound"
    Protected Sub ifgTariffCode_RowDataBound(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgTariffCode.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As System.Data.DataRowView
                    drv = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                    If bln_044KeyExist Then
                        Dim lkpItemCode As iLookup = CType(DirectCast(DirectCast(e.Row.Cells(2),  _
                            iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                        lkpItemCode.Validator.ReqErrorMessage = str_044KeyValue.Trim + " is Required"
                        lkpItemCode.Validator.LkpErrorMessage = "Invalid " + str_044KeyValue.Trim + ".Click on the List for Valid Values "
                    End If
                    If bln_045KeyExist Then
                        Dim lkpSubItemCode As iLookup = CType(DirectCast(DirectCast(e.Row.Cells(3),  _
                            iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                        lkpSubItemCode.Validator.ReqErrorMessage = str_045KeyValue.Trim + " is Required"
                        lkpSubItemCode.Validator.LkpErrorMessage = "Invalid " + str_045KeyValue.Trim + ".Click on the List for Valid Values "
                    End If
                End If
                If e.Row.RowIndex > 10 Then
                    Dim lkpItem As iLookup
                    lkpItem = CType(DirectCast(DirectCast(e.Row.Cells(2), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpItem.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                    Dim lkpSubItem As iLookup
                    lkpSubItem = CType(DirectCast(DirectCast(e.Row.Cells(3), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpSubItem.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                    Dim lkpDamage As iLookup
                    lkpDamage = CType(DirectCast(DirectCast(e.Row.Cells(4), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpDamage.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                    Dim lkpRepair As iLookup
                    lkpRepair = CType(DirectCast(DirectCast(e.Row.Cells(5), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpRepair.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/TariffCode.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTariffCode_RowDeleting"
    Protected Sub ifgTariffCode_RowDeleting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgTariffCode.RowDeleting
        Try
            dsTariffCode = CType(RetrieveData(TariffCode), TariffCodeDataSet)
            Dim dtTariff As Data.DataTable = dsTariffCode.Tables(TariffCodeData._TARIFF_CODE).Copy
            If CType(ifgTariffCode.DataSource, DataTable).Select(String.Concat(TariffCodeData.TRFF_CD_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                Dim strTariffGroup As String = pvt_GetTariffGroup(CInt(e.Keys(0))).ToString
                If strTariffGroup.Length > 0 Then
                    e.Cancel = True
                    e.OutputParamters("Delete") = String.Concat("Tariff Code : ", dtTariff.Select(String.Concat(TariffCodeData.TRFF_CD_ID, "=", e.Keys(0)))(0).Item("Code").ToString, " cannot be deleted ", _
                                                                "as this is mapped to Tariff group - ", strTariffGroup, ". Please delete the Tariff code mapped to Tariff group in the Tariff Group master.")
                    Exit Sub
                Else
                    e.OutputParamters("Delete") = String.Concat("Tariff Code : ", dtTariff.Select(String.Concat(TariffCodeData.TRFF_CD_ID, "=", e.Keys(0)))(0).Item("Code").ToString, " has been be deleted. Click submit to save changes.")
                    Exit Sub
                End If
            Else
                e.OutputParamters("Delete") = String.Concat("Tariff Code : ", dtTariff.Select(String.Concat(TariffCodeData.TRFF_CD_ID, "=", e.Keys(0)))(0).Item("Code").ToString, " has been be deleted. Click submit to save changes.")
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgTariffCode_ClientBind"
    Private Function pvt_GetTariffGroup(ByVal bv_intTariffID As Integer) As String
        Try
            Dim objTariffCode As New TariffCode
            Dim dsTariff_Code As New TariffCodeDataSet
            Dim sbrTariffGroup As New StringBuilder
            dsTariff_Code = objTariffCode.pub_TariffGroupCodeGetTariffCodeByDepot(bv_intTariffID, True)
            If dsTariff_Code.Tables(TariffCodeData._V_TARIFF_GROUP_DETAIL).Rows.Count > 0 Then
                For Each dr As DataRow In dsTariff_Code.Tables(TariffCodeData._V_TARIFF_GROUP_DETAIL).Rows
                    If sbrTariffGroup.ToString() <> Nothing Then
                        sbrTariffGroup.Append(", ")
                    End If
                    sbrTariffGroup.Append(dr.Item(TariffCodeData.TRFF_GRP_CD))
                Next
            End If

            Return sbrTariffGroup.ToString()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

    Private Sub pvt_GetItemAliasErrorMessage()
        If bln_044KeyExist Then
            pub_SetCallbackReturnValue("ReqErrorMsg", str_044KeyValue.Trim() + " is required")
            pub_SetCallbackStatus(True)
        End If
    End Sub

End Class
