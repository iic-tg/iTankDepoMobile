
Partial Class Masters_EquipmentTypeCode
    Inherits Pagebase

#Region "Declaration"

    Private Const EQUPMENT_TYPE_CODE As String = "EQUPMENT_TYPE_CODE"
    Dim ds As New EquipmentTypeDataSet
#End Region

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType.ToLower()
                Case "updateequipmenttypecode"
                    pvt_UpdateEquipmentTypeCode()
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub


    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/EquipmentTypeCode.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/MaxLength.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Sub pvt_UpdateEquipmentTypeCode()
        Try

            ds = CType(RetrieveData(EQUPMENT_TYPE_CODE), EquipmentTypeDataSet)
            Dim objcommon As New CommonData
            Dim objEquipmentType As New EquipmentType
            Dim blnFlag As Boolean = False
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(objcommon.GetDepotID())
            End If
            blnFlag = objEquipmentType.pub_UpdateEquipemtTypeandCode(ds, intDepotID, objcommon.GetCurrentUserName(), objcommon.GetCurrentDate())
            ds.AcceptChanges()
            If blnFlag = True Then
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackStatus(False)
                pub_SetCallbackReturnValue("Message", "No Changes to Save")
            End If


        Catch ex As Exception

            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)

        End Try
    End Sub


    Protected Sub ifgEquipmentTypeCode_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentTypeCode.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objEquipmentType As New EquipmentType
            Dim intDepotID As Integer
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommon.GetDepotID())
            End If
            ds = objEquipmentType.GetEquipmentType(intDepotID)
            'ifgEquipmentTypeCode.PageIndex = 0
            e.DataSource = ds.Tables(EquipmentTypeData._EQUIPMENT_TYPE)
            CacheData(EQUPMENT_TYPE_CODE, ds)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgEquipmentTypeCode_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentTypeCode.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim bln_069KeyExist As Boolean
                Dim str_069KeyValue As String
                Dim objCommondata As New CommonData
                str_069KeyValue = objCommondata.GetConfigSetting("069", bln_069KeyExist)
                If bln_069KeyExist AndAlso str_069KeyValue.ToLower = "true" Then

                Else
                    Dim txtEquipGroup As iTextBox = CType(DirectCast(DirectCast(e.Row.Cells(0),  _
                                                             iFgFieldCell).ContainingField, TextboxField).TextBox, iTextBox)
                    txtEquipGroup.Validator.IsRequired = False
                    ifgEquipmentTypeCode.Columns.Item(3).Visible = False
                End If
                If Not e.Row.DataItem Is Nothing Then
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgEquipmentTypeCode_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgEquipmentTypeCode.RowDeleting
        Try
            If CType(ifgEquipmentTypeCode.DataSource, DataTable).Select("EQPMNT_TYP_ID=" & e.Keys(0))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                e.OutputParamters("Warning") = String.Concat(e.Values(FinanceIntegrationData.EQPMNT_TYP_CD), " - ", e.Values(EquipmentTypeData.EQPMNT_CD_CD), " cannot be deleted")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgEquipmentTypeCode_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgEquipmentTypeCode.RowInserted
        Try
            CacheData(EQUPMENT_TYPE_CODE, ds)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub


    Protected Sub ifgEquipmentTypeCode_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgEquipmentTypeCode.RowInserting
        Try
            ds = CType(RetrieveData(EQUPMENT_TYPE_CODE), EquipmentTypeDataSet)
            Dim bln_069KeyExist As Boolean
            Dim str_069KeyValue As String
            Dim objCommondata As New CommonData
            str_069KeyValue = objCommondata.GetConfigSetting("069", bln_069KeyExist)
          
            Dim strFilter As String = String.Concat(EquipmentTypeData.EQPMNT_TYP_CD, "='", e.Values(FinanceIntegrationData.EQPMNT_TYP_CD), "' and ", EquipmentTypeData.EQPMNT_CD_CD, "='", e.Values(EquipmentTypeData.EQPMNT_CD_CD), "'")
            Dim dtEquipmentType As DataTable = ds.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Copy()
            Dim intResultIndex() As System.Data.DataRow = dtEquipmentType.Select(strFilter)
            If intResultIndex.Length > 0 Then
                'e.OutputParamters.Add("Duplicate", "Same Combination Already Added.")
                Dim strError As String = String.Concat(e.Values(FinanceIntegrationData.EQPMNT_TYP_CD), " and ", e.Values(EquipmentTypeData.EQPMNT_CD_CD), " Combination Already Existing.")
                e.OutputParamters.Add("Error", strError)
                e.Cancel = True
                Exit Sub
            End If
            e.Values(EquipmentTypeData.EQPMNT_TYP_ID) = CommonWeb.GetNextIndex(ds.Tables(EquipmentTypeData._EQUIPMENT_TYPE), FinanceIntegrationData.EQPMNT_TYP_ID)
            If bln_069KeyExist AndAlso str_069KeyValue.ToLower = "true" Then

            Else
                If e.Values(EquipmentTypeData.EQPMNT_TYP_CD).ToString.Length > 3 Then
                    e.Values(EquipmentTypeData.EQPMNT_GRP_CD) = e.Values(EquipmentTypeData.EQPMNT_TYP_CD).ToString.Substring(0, 3)
                Else
                    e.Values(EquipmentTypeData.EQPMNT_GRP_CD) = e.Values(FinanceIntegrationData.EQPMNT_TYP_CD)
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgEquipmentTypeCode_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgEquipmentTypeCode.RowUpdated
        Try
            CacheData(EQUPMENT_TYPE_CODE, ds)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgEquipmentTypeCode_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgEquipmentTypeCode.RowUpdating
        Try
            ds = CType(RetrieveData(EQUPMENT_TYPE_CODE), EquipmentTypeDataSet)
            Dim strFilter As String = String.Concat(EquipmentTypeData.EQPMNT_TYP_CD, "='", e.NewValues(FinanceIntegrationData.EQPMNT_TYP_CD), "' and ", EquipmentTypeData.EQPMNT_CD_CD, "='", e.NewValues(EquipmentTypeData.EQPMNT_CD_CD), "'")
            Dim strFilter1 As String = String.Concat(EquipmentTypeData.EQPMNT_TYP_CD, "='", e.NewValues(FinanceIntegrationData.EQPMNT_TYP_CD), "' and ", EquipmentTypeData.EQPMNT_CD_CD, "='", e.NewValues(EquipmentTypeData.EQPMNT_CD_CD), "'", "  and ", EquipmentTypeData.EQPMNT_TYP_ID, "<>", e.OldValues(EquipmentTypeData.EQPMNT_TYP_ID))
            Dim dtEquipmentType As DataTable = ds.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Copy()
            Dim intResultIndex() As System.Data.DataRow = dtEquipmentType.Select(strFilter1)
            If intResultIndex.Length > 0 Then
                'e.OutputParamters.Add("Duplicate", "Same Combination Already Existing.")
                Dim strError As String = String.Concat(e.NewValues(FinanceIntegrationData.EQPMNT_TYP_CD), " and ", e.NewValues(EquipmentTypeData.EQPMNT_CD_CD), " Combination Already Existing.")
                e.OutputParamters.Add("Error", strError)
                e.Cancel = True
                Exit Sub
            Else
                e.OldValues("ACTV_BT") = e.NewValues("ACTV_BT")
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
End Class
