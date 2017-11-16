
Partial Class Masters_Item
    Inherits Pagebase
#Region "Declaration"

    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Private strMSGDUPLICATE As String = "This Sub Item already exists."
    Dim strSubItemDuplicateRowCondition As String() = {ItemData.SB_ITM_CD}
    Dim dsItem As ItemDataSet
    Private Const Item As String = "Item"
    Dim dtItemData As DataTable
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
            Dim objCommon As New CommonData()
            pvt_SetChangesMade()
            ifgSubItem.AddButtonText = "Add Row"
            ifgSubItem.DeleteButtonText = "Delete Row"
            ifgSubItem.RefreshButtonText = "Refresh"
            str_044KeyValue = objCommon.GetConfigSetting("044", bln_044KeyExist)
            str_045KeyValue = objCommon.GetConfigSetting("045", bln_045KeyExist)
            If bln_044KeyExist Then
                spnHeader.InnerText = str_044KeyValue
                lblItemCode.Text = str_044KeyValue.Trim() + " Code"
                lblItemDescription.Text = str_044KeyValue.Trim() + " Description"
                txtItemCode.ToolTip = str_044KeyValue.Trim() + " Code"
                If str_045KeyValue.Trim = "Component" Then
                    'txtItemCode.HelpText = "599," + "ITEM_ITM_CD"
                    'txtItemDescription.HelpText = "600," + "ITEM_ITM_DSCRPTN_VC"
                Else
                    txtItemCode.HelpText = "212," + "ITEM_ITM_CD"
                    txtItemDescription.HelpText = "213," + "ITEM_ITM_DSCRPTN_VC"
                End If
                txtItemCode.Validator.ReqErrorMessage = str_044KeyValue.Trim() + " Code is required"
                txtItemDescription.Validator.ReqErrorMessage = str_044KeyValue.Trim() + " Description is required"
            End If

            If bln_045KeyExist Then
                lblSubItemHeader.InnerHtml = "<strong><u>" + str_045KeyValue + "</u></strong>"
                'lblSubItemHeader.InnerText = str_045KeyValue
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
            Case "CreateItem"
                pvt_CreateItem(e.GetCallbackValue("bv_strItemCode"), _
                               e.GetCallbackValue("bv_strItemDescription"), _
                               CBool(e.GetCallbackValue("bv_blnActiveBit")), _
                               e.GetCallbackValue("PageMode"), _
                               e.GetCallbackValue("wfData"))

            Case "UpdateItem"
                pvt_UpdateItem(e.GetCallbackValue("ID"), _
                               e.GetCallbackValue("bv_strItemCode"), _
                               e.GetCallbackValue("bv_strItemDescription"), _
                               CBool(e.GetCallbackValue("bv_blnActiveBit")), _
                               e.GetCallbackValue("PageMode"), _
                               e.GetCallbackValue("wfData"))
            Case "ValidateCode"
                pvt_ValidateItemCode(e.GetCallbackValue("Code"))
            Case "ValidateSubItemCode"
                pvt_ValidateSubItemCode(e.GetCallbackValue("SubItemCode"), e.GetCallbackValue("TableName"), e.GetCallbackValue("ItemID"))

        End Select
    End Sub

#End Region

#Region "pvt_GetData"

    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim sbProduct As New StringBuilder
            If bv_strMode = MODE_EDIT Then
                sbProduct.Append(CommonWeb.GetTextValuesJSO(txtItemCode, PageSubmitPane.pub_GetPageAttribute(ItemData.ITM_CD)))
                sbProduct.Append(CommonWeb.GetTextValuesJSO(txtItemDescription, PageSubmitPane.pub_GetPageAttribute(ItemData.ITM_DSCRPTN_VC)))
                sbProduct.Append(CommonWeb.GetCheckboxValuesJSO(chkActiveBit, CBool(PageSubmitPane.pub_GetPageAttribute(ItemData.ACTV_BT))))
                sbProduct.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(ItemData.ITM_ID), "');"))
            End If
            pub_SetCallbackReturnValue("Message", sbProduct.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgSubItem_ClientBind"

    Protected Sub ifgSubItem_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgSubItem.ClientBind
        Try
            Dim strWFData As String = e.Parameters("WFDATA").ToString()
            Dim strItemID As String = e.Parameters("ItemID").ToString()
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(strWFData, CommonUIData.DPT_ID))
            Dim objItem As New Item
            dsItem = objItem.pub_GetSubItemByItemID(strItemID)
            e.DataSource = dsItem.Tables(ItemData._SUB_ITEM)
            CacheData(Item, dsItem)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub

#End Region

#Region "pvt_CreateProduct"

    Public Function pvt_CreateItem(ByVal bv_strItemCode As String, _
                                   ByVal bv_strItemDescription As String, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                   ByVal bv_strPageMode As String, _
                                   ByVal bv_strWfData As String) As Long
        Dim objcommon As New CommonData
        Try
            Dim objItem As New Item
            Dim lngCreated As Long
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            End If
            dsItem = CType(RetrieveData(Item), ItemDataSet)
            dtItemData = CType(ifgSubItem.DataSource, DataTable)

            lngCreated = objItem.pub_CreateItem((bv_strItemCode), _
                                                bv_strItemDescription, _
                                                strModifiedby, _
                                                datModifiedDate, _
                                                strModifiedby, _
                                                datModifiedDate, _
                                                CommonUIs.iBool(bv_blnActiveBit), _
                                                intDepotID, _
                                                bv_strWfData, _
                                                dsItem)

            pub_SetCallbackReturnValue("ID", CStr(lngCreated))
            If bln_044KeyExist Then
                pub_SetCallbackReturnValue("Message", String.Concat(str_044KeyValue, " : ", bv_strItemCode, " ", strMSGINSERT))
            Else
                pub_SetCallbackReturnValue("Message", String.Concat("Item : ", bv_strItemCode, " ", strMSGINSERT))
            End If
            pub_SetCallbackStatus(True)
            Return lngCreated

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function

#End Region

#Region "pvt_UpdateProduct"

    Private Sub pvt_UpdateItem(ByVal bv_strItemID As String, _
                               ByVal bv_strItemCode As String, _
                               ByVal bv_strItemDescription As String, _
                               ByVal bv_blnActiveBit As Boolean, _
                               ByVal bv_strPageMode As String, _
                               ByVal bv_strWfData As String)
        Try
            Dim objItem As New Item
            Dim boolUpdated As Boolean

            Dim objcommon As New CommonData
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            dsItem = CType(RetrieveData(Item), ItemDataSet)
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            End If

            Dim dsItemData As DataSet = CType(RetrieveData(Item), ItemDataSet)

            boolUpdated = objItem.pub_UpdateItem(CommonUIs.iLng(bv_strItemID), _
                                                 bv_strItemCode, _
                                                 bv_strItemDescription, _
                                                 strModifiedby, _
                                                 datModifiedDate, _
                                                 CommonUIs.iBool(bv_blnActiveBit), _
                                                 intDepotID, _
                                                 dsItem)
            If bln_044KeyExist Then
                pub_SetCallbackReturnValue("Message", String.Concat(str_044KeyValue, " : ", bv_strItemCode, " ", strMSGUPDATE))
            Else
                pub_SetCallbackReturnValue("Message", String.Concat("Item : ", bv_strItemCode, " ", strMSGUPDATE))
            End If
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "pvt_ValidateItemCode"

    Public Sub pvt_ValidateItemCode(ByVal bv_strItemCode As String)

        Try
            Dim objItem As New Item
            dsItem = objItem.pub_GetItemByItemCode(bv_strItemCode)
            If dsItem.Tables(ItemData._ITEM).Rows.Count = 0 Then
                pub_SetCallbackReturnValue("valid", "true")
                pub_SetCallbackReturnValue("message", "")
            Else
                pub_SetCallbackReturnValue("valid", "false")
                If bln_044KeyExist Then
                    pub_SetCallbackReturnValue("message", String.Concat("This ", str_044KeyValue, " code already exists."))
                Else
                    pub_SetCallbackReturnValue("message", "This Sub item already exists")
                End If
            End If

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateSubItemCode"

    Public Sub pvt_ValidateSubItemCode(ByVal bv_strSubItemCode As String, ByVal bv_strTableName As String, ByVal bv_strItemID As String)

        Try
            Dim objItem As New Item
            Dim blndsValid As Boolean
            Dim strExistCode As String = String.Empty
            dsItem = RetrieveData(Item)

            Dim dtCodeMasterTemp As DataTable = dsItem.Tables(ItemData._SUB_ITEM).Copy
            dtCodeMasterTemp.AcceptChanges()

            ''Checking whether the entered code is available in grid
            Dim intResultIndex() As System.Data.DataRow = dtCodeMasterTemp.Select(ItemData.SB_ITM_CD & "='" & bv_strSubItemCode & "'")

            If intResultIndex.Length > 0 Then
                blndsValid = False
                pub_SetCallbackReturnValue("message", String.Concat("This ", str_045KeyValue, " code already exists."))
            Else
                blndsValid = True
            End If
            If blndsValid Then
                dsItem = objItem.pub_GetSubItemByItemCode(bv_strSubItemCode, bv_strItemID)
                If dsItem.Tables(ItemData._V_SUB_ITEM).Rows.Count = 0 Then
                    pub_SetCallbackReturnValue("valid", "true")
                    pub_SetCallbackReturnValue("message", "")
                Else
                    pub_SetCallbackReturnValue("valid", "false")
                    If bln_045KeyExist Then
                        pub_SetCallbackReturnValue("message", String.Concat("This ", str_045KeyValue, " code already exists."))
                    Else
                        pub_SetCallbackReturnValue("message", "This Sub item already exists")
                    End If
                End If
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
        pub_SetGridChanges(ifgSubItem, "ITab1_0")
        CommonWeb.pub_AttachHasChanges(txtItemCode)
        CommonWeb.pub_AttachHasChanges(txtItemDescription)
        CommonWeb.pub_AttachHasChanges(chkActiveBit)
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Masters/Item.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgSubItem_RowInserting"

    Protected Sub ifgSubItem_RowInserting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgSubItem.RowInserting
        Try
            Dim objItem As New Item
            dsItem = CType(RetrieveData(Item), ItemDataSet)
            dtItemData = dsItem.Tables(ItemData._SUB_ITEM)
            Dim strPageMode As String = CStr(e.InputParamters("mode"))
            Dim strItemID As String = CStr(e.InputParamters("ItemID"))

            Dim lngID As Long
            lngID = CommonWeb.GetNextIndex(dtItemData, ItemData.SB_ITM_ID)
            e.Values(ItemData.SB_ITM_ID) = lngID

            If strPageMode = MODE_NEW Then
                Dim dtDeletedRows As DataTable = dsItem.Tables(ItemData._SUB_ITEM).GetChanges(DataRowState.Deleted)
                Dim dtDBRows As DataTable = objItem.pub_GetSubItemByItemID(CommonUIs.iLng(strItemID)).Tables(ItemData._SUB_ITEM)

                If dtDeletedRows IsNot Nothing Then
                    For Each dr As DataRow In dtDeletedRows.Rows
                        Dim drs As DataRow() = dtDBRows.Select(String.Concat(ItemData.SB_ITM_ID, "=", dr.Item(ItemData.SB_ITM_ID, DataRowVersion.Original)), "")
                        For Each drq As DataRow In drs
                            drq.Delete()
                        Next
                    Next
                End If
                'Edit Mode
                If CommonWeb.pub_IsRowAlreadyExists(dsItem.Tables(ItemData.SB_ITM_ID), CType(e.Values, OrderedDictionary), strSubItemDuplicateRowCondition, "edit", ItemData.SB_ITM_ID, CInt(e.Values(ItemData.SB_ITM_ID))) Then
                    If bln_045KeyExist Then
                        e.OutputParamters("Duplicate") = String.Concat("This ", str_045KeyValue, " already exists.")
                    Else
                        e.OutputParamters("Duplicate") = strMSGDUPLICATE
                    End If
                    e.Cancel = True
                    Exit Sub
                End If
            ElseIf strPageMode = MODE_EDIT Then
                'New Mode
                If CommonWeb.pub_IsRowAlreadyExists(dsItem.Tables(ItemData._SUB_ITEM), CType(e.Values, OrderedDictionary), strSubItemDuplicateRowCondition, "New", ItemData.SB_ITM_ID, CInt(e.Values(ItemData.SB_ITM_ID))) Then
                    If bln_045KeyExist Then
                        e.OutputParamters("Duplicate") = String.Concat("This ", str_045KeyValue, " already exists.")
                    Else
                        e.OutputParamters("Duplicate") = strMSGDUPLICATE
                    End If
                    e.Cancel = True
                    Exit Sub
                End If
                End If
         
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgSubItem_RowUpdating"

    Protected Sub ifgSubItem_RowUpdating(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgSubItem.RowUpdating
        Try
            Dim objItem As New Item
            dsItem = CType(RetrieveData(Item), ItemDataSet)
            Dim strItemID As String = CStr(e.InputParamters("ItemID"))
            e.NewValues(ItemData.SB_ITM_ID) = e.OldValues(ItemData.SB_ITM_ID)

            If CommonWeb.pub_IsRowAlreadyExists(dsItem.Tables(ItemData._SUB_ITEM), CType(e.NewValues, OrderedDictionary), strSubItemDuplicateRowCondition, "edit", ItemData.SB_ITM_ID, CInt(e.OldValues(ItemData.SB_ITM_ID))) Then
                If bln_045KeyExist Then
                    e.OutputParamters("Duplicate") = String.Concat("This ", str_045KeyValue, " already exists.")
                Else
                    e.OutputParamters.Add("Duplicate", strMSGDUPLICATE)
                End If
                e.Cancel = True
                e.RequiresRebind = True
                Exit Sub
            Else
                If CommonWeb.pub_IsRowAlreadyExists(objItem.pub_GetSubItemByItemID(CommonUIs.iLng(strItemID)).Tables(ItemData._SUB_ITEM), CType(e.NewValues, OrderedDictionary), strSubItemDuplicateRowCondition, "New", ItemData.SB_ITM_ID, CInt(e.OldValues(ItemData.SB_ITM_ID))) Then
                    If bln_045KeyExist Then
                        e.OutputParamters("Duplicate") = String.Concat("This ", str_045KeyValue, " already exists.")
                    Else
                        e.OutputParamters.Add("Duplicate", strMSGDUPLICATE)
                    End If
                    e.Cancel = True
                    e.RequiresRebind = True
                    Exit Sub
                Else
                    e.NewValues(ItemData.SB_ITM_ID) = e.OldValues(ItemData.SB_ITM_ID)
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgSubItem_RowDeleting"
    Protected Sub ifgSubItem_RowDeleting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgSubItem.RowDeleting
        Try
            dsItem = CType(RetrieveData(Item), ItemDataSet)
            Dim dtSubItem As Data.DataTable = dsItem.Tables(ItemData._SUB_ITEM).Copy
            If CType(ifgSubItem.DataSource, DataTable).Select(String.Concat(ItemData.SB_ITM_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                If bln_045KeyExist Then
                    e.OutputParamters("Delete") = String.Concat(str_045KeyValue, " : Code ", dtSubItem.Select(String.Concat(ItemData.SB_ITM_ID, "=", e.Keys(0)))(0).Item(ItemData.SB_ITM_CD).ToString, " cannot be deleted")
                Else
                    e.OutputParamters("Delete") = String.Concat("Sub Item : Code ", dtSubItem.Select(String.Concat(ItemData.SB_ITM_ID, "=", e.Keys(0)))(0).Item(ItemData.SB_ITM_CD).ToString, " cannot be deleted")
                End If
                Exit Sub
            Else
                If bln_045KeyExist Then
                    e.OutputParamters("Delete") = String.Concat(str_045KeyValue, " : Code ", dtSubItem.Select(String.Concat(ItemData.SB_ITM_ID, "=", e.Keys(0)))(0).Item(ItemData.SB_ITM_CD).ToString, " has been be deleted from Item. Click submit to save changes.")
                Else
                    e.OutputParamters("Delete") = String.Concat("Sub Item : Code ", dtSubItem.Select(String.Concat(ItemData.SB_ITM_ID, "=", e.Keys(0)))(0).Item(ItemData.SB_ITM_CD).ToString, " has been be deleted from Item. Click submit to save changes.")
                End If
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "ifgSubItem_RowDataBound"
    Protected Sub ifgSubItem_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgSubItem.RowDataBound
        Try
            Dim objCommon As New CommonData()
            str_045KeyValue = objCommon.GetConfigSetting("045", bln_045KeyExist)
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                If bln_045KeyExist Then
                    Dim txtSubItemCode As iTextBox = CType(DirectCast(DirectCast(e.Row.Cells(0),  _
                  iFgFieldCell).ContainingField, TextboxField).TextBox, iTextBox)
                    Dim txtSubItemDesc As iTextBox = CType(DirectCast(DirectCast(e.Row.Cells(1),  _
                       iFgFieldCell).ContainingField, TextboxField).TextBox, iTextBox)
                    txtSubItemCode.Validator.ReqErrorMessage = str_045KeyValue.Trim + " Code is Required"
                    If str_045KeyValue.Trim = "Component" Then
                        txtSubItemCode.HelpText = "597," + "SUB_ITEM_SB_ITM_CD"
                        txtSubItemDesc.HelpText = "598," + "SUB_ITEM_SB_ITM_DSCRPTN_VC"
                    Else
                        txtSubItemCode.HelpText = "214," + "SUB_ITEM_SB_ITM_CD"
                        txtSubItemDesc.HelpText = "215," + "SUB_ITEM_SB_ITM_DSCRPTN_VC"
                    End If
                    txtSubItemDesc.Validator.ReqErrorMessage = str_045KeyValue.Trim + " Description is Required"

                End If
                If Not e.Row.DataItem Is Nothing Then
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class