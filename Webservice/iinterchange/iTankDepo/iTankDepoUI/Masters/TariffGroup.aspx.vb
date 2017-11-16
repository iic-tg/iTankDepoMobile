
Partial Class Masters_TariffGroup
    Inherits Pagebase

#Region "Declaration"

    Private Const KEY_ID As String = "ID"
    Private Const KEY_MODE As String = "mode"
    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Private pvt_lngID As Long
    Private Const TARIFFGROUP As String = "TARIFFGROUP"
    Dim dsTariffGroup As TariffGroupDataSet
    Dim dtTariffGroup As DataTable
    Dim strTariffGroupDuplicateRowCondition As String() = {TariffGroupData.TRFF_CD_ID}
    Private strMSGDUPLICATE As String = "This Tariff Code already exists in this Tariff Group."
#End Region



#Region "Page Load"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        Try
            CommonWeb.pub_AttachHasChanges(txtTRFF_GRP_CD)
            CommonWeb.pub_AttachHasChanges(txtTRFF_GRP_DSCRPTN)
            CommonWeb.pub_AttachHasChanges(chkActive)
            pub_SetGridChanges(ifgTariffGroupDetail, "ITab1_0")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "fnGetData"
                    pvt_GetData(e.GetCallbackValue("mode"))
                Case "InsertTariffGroup"
                    pvt_CreateTariffGroup(e.GetCallbackValue("TariffGroupCode"), _
                                        e.GetCallbackValue("TariffGroupDescription"), _
                                        CBool(e.GetCallbackValue("ActiveBit")), _
                                        e.GetCallbackValue("wfData"))
                Case "UpdateTariffGroup"
                    pvt_UpdateTariffGroup(e.GetCallbackValue("ID"), _
                                        e.GetCallbackValue("TariffGroupCode"), _
                                        e.GetCallbackValue("TariffGroupDescription"), _
                                        CBool(e.GetCallbackValue("ActiveBit")), _
                                        e.GetCallbackValue("wfData"))
                Case "ValidateTariffGroup"
                    pvt_ValidatePK(e.GetCallbackValue("TariffGroupCode"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidatePK()"
    ''' <summary>
    ''' This method is to validate the user name
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_ValidatePK(ByVal bv_strTariffGroupCode As String)
        Try
            Dim objTariffGroup As New TariffGroup
            Dim bolValid As Boolean
            bolValid = objTariffGroup.pub_ValidatePKTariffGroupCode(bv_strTariffGroupCode)
            If bolValid = True Then
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

#Region "pvt_GetData"
    ''' <summary>
    ''' This method is to load the datas in the page
    ''' </summary>
    ''' <param name="bv_strMode"></param>
    ''' <remarks></remarks>
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim objCommon As New CommonData()
            Dim sbTariffGroup As New StringBuilder
            If bv_strMode = MODE_EDIT Then
                If (PageSubmitPane.pub_GetPageAttribute(TariffGroupData.TRFF_GRP_CD)) = Nothing Then
                    sbTariffGroup.Append(CommonWeb.GetTextValuesJSO(txtTRFF_GRP_CD, ""))
                Else
                    sbTariffGroup.Append(CommonWeb.GetTextValuesJSO(txtTRFF_GRP_CD, PageSubmitPane.pub_GetPageAttribute(TariffGroupData.TRFF_GRP_CD)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(TariffGroupData.TRFF_GRP_DESCRPTN_VC)) = Nothing Then
                    sbTariffGroup.Append(CommonWeb.GetTextValuesJSO(txtTRFF_GRP_DSCRPTN, ""))
                Else
                    sbTariffGroup.Append(CommonWeb.GetTextValuesJSO(txtTRFF_GRP_DSCRPTN, PageSubmitPane.pub_GetPageAttribute(TariffGroupData.TRFF_GRP_DESCRPTN_VC)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(TariffGroupData.ACTV_BT)) = Nothing Then
                    sbTariffGroup.Append(CommonWeb.GetCheckboxValuesJSO(chkActive, ""))
                Else
                    sbTariffGroup.Append(CommonWeb.GetCheckboxValuesJSO(chkActive, CBool(PageSubmitPane.pub_GetPageAttribute(TariffGroupData.ACTV_BT))))
                End If
                sbTariffGroup.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(TariffGroupData.TRFF_GRP_ID), "');"))
                pub_SetCallbackReturnValue("Message", sbTariffGroup.ToString)
            Else
                dsTariffGroup = New TariffGroupDataSet
            End If

            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateTariffGroup"

    Protected Sub pvt_CreateTariffGroup(ByVal bv_strTRFF_GRP_CD As String, _
        ByVal bv_strTRFF_GRP_DSCRPTN_VC As String, _
        ByVal bv_active As String, _
        ByVal bv_strwfData As String)
        Try
            dsTariffGroup = CType(RetrieveData(TARIFFGROUP), TariffGroupDataSet)
            Dim objTariffGroup As New TariffGroup
            Dim objcommon As New CommonData
            Dim intDepotID As Integer = objcommon.GetDepotID()
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = objcommon.GetHeadQuarterID()
            End If
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As DateTime = objcommon.GetCurrentDate()
            If dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL).Rows.Count = 0 Then
                pub_SetCallbackReturnValue("checkTariffDetail", True)
                pub_SetCallbackStatus(True)
            End If
            Dim lng As Long = objTariffGroup.pub_TariffGroupCreateTariffGroup(bv_strTRFF_GRP_CD, bv_strTRFF_GRP_DSCRPTN_VC, _
                                                          objcommon.GetCurrentUserName(), CDate(objcommon.GetCurrentDate()), strModifiedby, _
                                                          datModifiedDate, intDepotID, bv_active, bv_strwfData, dsTariffGroup)

            dsTariffGroup.AcceptChanges()

            pub_SetCallbackReturnValue("Message", String.Concat("Tariff Group : ", bv_strTRFF_GRP_CD, " ", strMSGINSERT))
            pub_SetCallbackReturnValue("ID", CStr(lng))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region


#Region "pvt_UpdateTariffGroup"

    Protected Sub pvt_UpdateTariffGroup(ByVal bv_TRFF_GRP_ID As String, _
                ByVal bv_strTRFF_GRP_CD As String, _
                ByVal bv_strTRFF_GRP_DSCRPTN_VC As String, _
                ByVal bv_active As String, _
                ByVal bv_strwfData As String)

        Try
            dsTariffGroup = New TariffGroupDataSet
            dsTariffGroup = CType(RetrieveData(TARIFFGROUP), TariffGroupDataSet)
            If dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL).Rows.Count = 0 Then
                pub_SetCallbackReturnValue("checkTariffDetail", True)
                pub_SetCallbackStatus(True)
                Exit Sub
            End If
            Dim objTariffGroup As New TariffGroup
            Dim objcommon As New CommonData
            Dim intDepotID As Integer = objcommon.GetDepotID()
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = objcommon.GetHeadQuarterID()
            End If
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As DateTime = objcommon.GetCurrentDate()
            objTariffGroup.pub_TariffGroupModifyTariff_Group(bv_TRFF_GRP_ID, _
                                bv_strTRFF_GRP_CD, bv_strTRFF_GRP_DSCRPTN_VC, _
                                strModifiedby, _
                                datModifiedDate, bv_active, _
                                intDepotID, bv_strwfData, dsTariffGroup)
            dsTariffGroup.AcceptChanges()
            pub_SetCallbackReturnValue("Message", String.Concat("Tariff Group : ", bv_strTRFF_GRP_CD, " ", strMSGUPDATE))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/TariffGroup.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTariffGroupDetail_ClientBind"
    Protected Sub ifgTariffGroupDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgTariffGroupDetail.ClientBind
        Try
            Dim objTariffGroup As New TariffGroup
            Dim objcommon As New CommonData
            Dim strMode As String = e.Parameters("MODE").ToString()
            Dim strTariffGroupID As String = e.Parameters("ID").ToString()
            Dim intDepotID As Integer = objcommon.GetDepotID()
            If (strMode = MODE_EDIT) Then
                dsTariffGroup = objTariffGroup.pub_GetTariffGroupDetailbyGroupID(strTariffGroupID)
                e.DataSource = dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL)
                ifgTariffGroupDetail.Columns.Item(0).IsEditable = False
                ifgTariffGroupDetail.AllowSearch = True
            Else
                If strTariffGroupID = "" Then
                    strTariffGroupID = String.Empty
                End If
                dsTariffGroup = New TariffGroupDataSet
                e.DataSource = dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL)
                ifgTariffGroupDetail.AllowSearch = False
            End If

            CacheData(TARIFFGROUP, dsTariffGroup)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTariffGroupDetail_RowDataBound"
    Protected Sub ifgTariffGroupDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgTariffGroupDetail.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As System.Data.DataRowView
                    drv = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        Dim chkStatus As New iInterchange.WebControls.v4.Data.iFgCheckBox
                    End If
                End If
                If e.Row.RowIndex > 1 Then
                    Dim lkpTarriff As iLookup
                    lkpTarriff = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpTarriff.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTariffGroupDetail_RowDeleting"
    Protected Sub ifgTariffGroupDetail_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgTariffGroupDetail.RowDeleting
        Try
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgTariffGroupDetail.PageSize * ifgTariffGroupDetail.PageIndex + e.RowIndex
            dsTariffGroup = CType(RetrieveData(TARIFFGROUP), TariffGroupDataSet)
            Dim dtTariffGroup As Data.DataTable = dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL).Copy

            If CType(ifgTariffGroupDetail.DataSource, DataTable).Select(String.Concat(TariffGroupData.TRFF_GRP_DTL_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                e.OutputParamters("Delete") = String.Concat("Tariff : Code ", dtTariffGroup.Rows(e.RowIndex).Item(TariffGroupData.TRFF_CD_CD).ToString, " cannot be deleted")
                Exit Sub
            Else
                e.OutputParamters("Delete") = String.Concat("Tariff : Code ", dtTariffGroup.Rows(e.RowIndex).Item(TariffGroupData.TRFF_CD_CD).ToString, " has been be deleted from Tariff Group. Click submit to save changes.")
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTariffGroupDetail_RowInserting"
    Protected Sub ifgTariffGroupDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgTariffGroupDetail.RowInserting
        Try
            Dim objTariff As New TariffGroup
            dsTariffGroup = CType(RetrieveData(TARIFFGROUP), TariffGroupDataSet)
            dtTariffGroup = dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL)
            Dim strPageMode As String = CStr(e.InputParamters("mode"))
            Dim strTariffGroupID As String = CStr(e.InputParamters("ID"))

            Dim lngID As Long
            lngID = CommonWeb.GetNextIndex(dtTariffGroup, TariffGroupData.TRFF_GRP_DTL_ID)
            e.Values(TariffGroupData.TRFF_GRP_DTL_ID) = lngID

            'Validate against Current Dataset
            If CommonWeb.pub_IsRowAlreadyExists(dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL), CType(e.Values, OrderedDictionary), strTariffGroupDuplicateRowCondition, "New", TariffGroupData.TRFF_GRP_DTL_ID, CInt(e.Values(TariffGroupData.TRFF_GRP_DTL_ID))) Then
                e.OutputParamters("Duplicate") = strMSGDUPLICATE
                e.Cancel = True
                Exit Sub
            End If

            'Validate against Database
            If strTariffGroupID <> "" AndAlso strTariffGroupID <> "0" Then
                Dim dtDeletedRows As DataTable = dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL).GetChanges(DataRowState.Deleted)
                Dim dtDBRows As DataTable = objTariff.pub_GetTariffGroupDetailbyGroupID(strTariffGroupID).Tables(TariffGroupData._TARIFF_GROUP_DETAIL)

                If dtDeletedRows IsNot Nothing Then
                    For Each dr As DataRow In dtDeletedRows.Rows
                        Dim drs As DataRow() = dtDBRows.Select(String.Concat(TariffGroupData.TRFF_CD_ID, "=", dr.Item(TariffGroupData.TRFF_CD_ID, DataRowVersion.Original)), "")
                        For Each drq As DataRow In drs
                            drq.Delete()
                        Next
                    Next
                End If
                If CommonWeb.pub_IsRowAlreadyExists(dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL), CType(e.Values, OrderedDictionary), strTariffGroupDuplicateRowCondition, "edit", TariffGroupData.TRFF_GRP_DTL_ID, CInt(e.Values(TariffGroupData.TRFF_GRP_DTL_ID))) Then
                    e.OutputParamters("Duplicate") = strMSGDUPLICATE
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
    
#Region "ifgTariffGroupDetail_RowInserting"
    Protected Sub ifgTariffGroupDetail_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgTariffGroupDetail.RowUpdating
        Try
            Dim objTariff As New TariffGroup
            dsTariffGroup = CType(RetrieveData(TARIFFGROUP), TariffGroupDataSet)
            dtTariffGroup = dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL)
            Dim strPageMode As String = CStr(e.InputParamters("mode"))
            Dim strTariffGroupID As String = CStr(e.InputParamters("ID"))
            If CommonWeb.pub_IsRowAlreadyExists(dsTariffGroup.Tables(TariffGroupData._TARIFF_GROUP_DETAIL), CType(e.NewValues, OrderedDictionary), strTariffGroupDuplicateRowCondition, "edit", TariffGroupData.TRFF_GRP_DTL_ID, CInt(e.OldValues(TariffGroupData.TRFF_GRP_DTL_ID))) Then
                e.OutputParamters.Add("Duplicate", strMSGDUPLICATE)
                e.Cancel = True
                e.RequiresRebind = True
                Exit Sub
            Else
                If CommonWeb.pub_IsRowAlreadyExists(objTariff.pub_GetTariffGroupDetailbyGroupID(strTariffGroupID).Tables(TariffGroupData._TARIFF_GROUP_DETAIL), CType(e.NewValues, OrderedDictionary), strTariffGroupDuplicateRowCondition, "New", TariffGroupData.TRFF_GRP_DTL_ID, CInt(e.OldValues(TariffGroupData.TRFF_GRP_DTL_ID))) Then
                    e.OutputParamters.Add("Duplicate", strMSGDUPLICATE)
                    e.Cancel = True
                    e.RequiresRebind = True
                    Exit Sub
                Else
                    e.NewValues(TariffGroupData.TRFF_GRP_DTL_ID) = e.OldValues(TariffGroupData.TRFF_GRP_DTL_ID)
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class