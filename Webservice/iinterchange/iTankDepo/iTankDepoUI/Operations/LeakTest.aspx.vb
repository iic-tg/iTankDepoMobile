
Partial Class Operations_LeakTest
    Inherits Pagebase

    Dim dsLeakTestData As New LeakTestDataSet
    Dim dtLeakTestData As DataTable
    Private Const LEAK_TEST As String = "LEAK_TEST"
    Private Const LEAK_TEST_DOCUMENT As String = "LEAK_TEST_DOCUMENT"
    Private strMSGUPDATE As String = "Leak Test : Equipment(s) Updated Successfully."

#Region "Page_PreRender1"

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Not Request.QueryString("GateinTransaction") Is Nothing And Not Request.QueryString("EquipmentNo") Is Nothing Then
                    PageSubmitPane.Visible = False
                    hlnkPrint.Visible = False
                    dvLeakTest.Visible = False
                    btnSubmitLeakTest.Visible = True
                    'ClientScript.RegisterClientScriptBlock(GetType(String), "redirect6", " $(document).ready(function () {initPage('Repair');});", True)
                Else
                    btnSubmitLeakTest.Visible = False
                End If
                hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper()
                Dim objCommondata As New CommonData
                Dim strSessionId As String = objCommondata.GetSessionID()
                Dim strActivityName As String = String.Empty
                If Not pub_GetQueryString("activityname") Is Nothing Then
                    strActivityName = pub_GetQueryString("activityname")
                End If
                objCommondata.FlushLockDataByActivityName(CleaningData.EQPMNT_NO, strSessionId, strActivityName)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/LeakTest.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgLeakTest, "ITab1_0")
    End Sub

#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "UpdateLeakTest"
                    pvt_CreateLeakTest(e.GetCallbackValue("ActivityName"))
                Case "ValidateEquipment"
                    pvt_ValidateEquipment(e.GetCallbackValue("EquipmentId"), CInt(e.GetCallbackValue("GridIndex")), e.GetCallbackValue("RowState"))
                Case "UpdateGenerationDetail"
                    pvt_UpdateGenerationDetail(e.GetCallbackValue("WFData"))
                Case "RecordLockData"
                    pvt_GetLockData(CBool(e.GetCallbackValue("checkBit")), _
                                    e.GetCallbackValue("EquipmentNo"), _
                                    e.GetCallbackValue("ActivityName"))

            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateLeakTest"
    Private Sub pvt_CreateLeakTest(ByVal bv_strActivityName As String)
        Try
            Dim objLeakTest As New LeakTest
            Dim objCommon As New CommonData
            Dim intDepotId As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strCurrentUserName As String = objCommon.GetCurrentUserName()
            Dim datModifiedDate As String = objCommon.GetCurrentDate()
            dsLeakTestData = CType(RetrieveData(LEAK_TEST), LeakTestDataSet)

            Dim drLeakTest As DataRow()
            drLeakTest = dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(LeakTestData.CHECKED & "='True'")
            If Not drLeakTest.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            End If

            objLeakTest.pub_CreateLeakTest(dsLeakTestData, _
                                           intDepotId, _
                                           strCurrentUserName, _
                                           datModifiedDate)
            Dim strCurrentSessionId As String = String.Empty
            strCurrentSessionId = GetSessionID()
            For Each drLock As DataRow In dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "='True'"))
                objCommon.FlushLockData(LeakTestData.EQPMNT_NO, CStr(drLock.Item(LeakTestData.EQPMNT_NO)), strCurrentSessionId, bv_strActivityName)
            Next
            dsLeakTestData.AcceptChanges()
            CacheData(LEAK_TEST, dsLeakTestData)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateGenerationDetail"
    Private Sub pvt_UpdateGenerationDetail(ByVal bv_strWfData As String)
        Try
            Dim objLeakTest As New LeakTest
            Dim objCommon As New CommonData
            Dim dtLeakTest As New DataTable
            Dim objCommonUI As New CommonUI
            Dim dsDocument As New LeakTestDataSet
            Dim drLeakTest As DataRow
            Dim CompareCustmrId As Integer
            Dim CustomerId As Integer
            Dim dsDepot As DataSet
            Dim count As Integer = 0
            Dim intDepotId As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strCurrentUserName As String = objCommon.GetCurrentUserName()
            Dim datModifiedDate As String = objCommon.GetCurrentDate()
            dsLeakTestData = CType(RetrieveData(LEAK_TEST), LeakTestDataSet)
            Dim drALeakTest As DataRow()
            For Each dr As DataRow In dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "=", "True"))
                If dr.RowState = DataRowState.Added Then
                    pub_SetCallbackStatus(False)
                    pub_SetCallbackError("Record must be submitted before generating document")
                    Exit Sub
                End If
            Next
            drALeakTest = dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(LeakTestData.CHECKED & "='True'")
            If Not drALeakTest.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            Else
                For Each dr As DataRow In dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "=", "True"))
                    CustomerId = dr(LeakTestData.CSTMR_ID)
                    If (count > 0) And (CustomerId <> CompareCustmrId) Then
                        pub_SetCallbackStatus(False)
                        pub_SetCallbackError("Leak Test document cannot be generated for multiple customers.")
                        Exit Sub
                    End If
                    CompareCustmrId = dr(LeakTestData.CSTMR_ID)
                    count = count + 1
                Next
            End If

            objLeakTest.pub_UpdateGenerationDetails(dsLeakTestData, _
                                                    intDepotId, _
                                                    strCurrentUserName, _
                                                    datModifiedDate)
            dsLeakTestData.AcceptChanges()
            CacheData(LEAK_TEST, dsLeakTestData)
            dtLeakTest = dsDocument.Tables(LeakTestData._REPORT_LEAK_TEST).Clone()

            For Each dr As DataRow In dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "=", "True"))
                drLeakTest = dtLeakTest.NewRow()
                drLeakTest(LeakTestData.LK_TST_ID) = dr(LeakTestData.LK_TST_ID)
                drLeakTest(LeakTestData.EQPMNT_NO) = dr(LeakTestData.EQPMNT_NO)
                drLeakTest(LeakTestData.GI_TRNSCTN_NO) = dr(LeakTestData.GI_TRNSCTN_NO)
                drLeakTest(LeakTestData.EQPMNT_TYP_CD) = dr(LeakTestData.EQPMNT_TYP_CD)
                drLeakTest(LeakTestData.LTST_RPRT_NO) = dr(LeakTestData.LTST_RPRT_NO)
                drLeakTest(LeakTestData.CSTMR_CD) = dr(LeakTestData.CSTMR_CD)
                drLeakTest(LeakTestData.CSTMR_ID) = dr(LeakTestData.CSTMR_ID)
                drLeakTest(LeakTestData.TST_DT) = dr(LeakTestData.TST_DT)
                drLeakTest(LeakTestData.SHLL_TST_BT) = dr(LeakTestData.SHLL_TST_BT)
                drLeakTest(LeakTestData.STM_TB_TST_BT) = dr(LeakTestData.STM_TB_TST_BT)
                drLeakTest(LeakTestData.RLF_VLV_SRL_1) = dr(LeakTestData.RLF_VLV_SRL_1)
                drLeakTest(LeakTestData.RLF_VLV_SRL_2) = dr(LeakTestData.RLF_VLV_SRL_2)
                drLeakTest(LeakTestData.PG_1) = dr(LeakTestData.PG_1)
                drLeakTest(LeakTestData.PG_2) = dr(LeakTestData.PG_2)
                drLeakTest(LeakTestData.RMRKS_VC) = dr(LeakTestData.RMRKS_VC)
                drLeakTest(LeakTestData.MDFD_BY) = dr(LeakTestData.MDFD_BY)
                dtLeakTest.Rows.Add(drLeakTest)
            Next
            dsDocument.Tables(LeakTestData._LEAK_TEST).Clear()
            dsDocument.Tables(LeakTestData._LEAK_TEST).Merge(dtLeakTest)
            'Depot Details
            dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
            dsDocument.Merge(dsDepot.Tables(CommonUIData._DEPOT))
            'Customer Details
            Dim dsCustomer As New LeakTestDataSet
            dsCustomer.Tables(GateinData._CUSTOMER).Rows.Clear()
            'Multilocation
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                dsCustomer = objLeakTest.pub_GetCustomerDetail(CInt(objCommon.GetHeadQuarterID()), dtLeakTest.Rows(0).Item(LeakTestData.CSTMR_ID))
            Else
                dsCustomer = objLeakTest.pub_GetCustomerDetail(intDepotId, dtLeakTest.Rows(0).Item(LeakTestData.CSTMR_ID))
            End If
            dsDocument.Tables(LeakTestData._CUSTOMER).Clear()
            dsDocument.Merge(dsCustomer.Tables(LeakTestData._CUSTOMER))

            CacheData(LEAK_TEST_DOCUMENT, dsDocument)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateEquipment"
    Private Sub pvt_ValidateEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_intGridIndex As Integer, ByVal bv_strRowstate As String)
        Try
            Dim blndsValid As Boolean
            dsLeakTestData = CType(RetrieveData(LEAK_TEST), LeakTestDataSet)
            dtLeakTestData = dsLeakTestData.Tables(LeakTestData._LEAK_TEST)
            Dim intResultIndex() As System.Data.DataRow = dtLeakTestData.Select(String.Concat(LeakTestData.EQPMNT_NO, "='", bv_strEquipmentNo, "' "))
            Dim strExistEquipment As String = ""
            If intResultIndex.Length > 0 Then
                If dtLeakTestData.Rows.Count > bv_intGridIndex Then
                    If dtLeakTestData.Rows(bv_intGridIndex).RowState = Data.DataRowState.Deleted Then
                        strExistEquipment = String.Empty
                    ElseIf dtLeakTestData.Rows(bv_intGridIndex).RowState <> Data.DataRowState.Deleted Then
                        strExistEquipment = dtLeakTestData.Rows(bv_intGridIndex)(LeakTestData.EQPMNT_NO).ToString
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
                    Dim objLeakTest As New LeakTest
                    Dim objCommon As New CommonData
                    blndsValid = objLeakTest.pub_ValidateEquipmentNoByDepotID(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))
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

#Region "ifgLeakTest_ClientBind"
    Protected Sub ifgLeakTest_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgLeakTest.ClientBind
        Try
            Dim ObjLeakTest As New LeakTest()
            Dim objCommon As New CommonData
            Dim intDepotID As Integer = objCommon.GetDepotID()
            hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper()
            If Not e.Parameters("Mode") Is Nothing Then
                If e.Parameters("Mode").ToString() = "Repair" Then
                    CacheData("PageName", e.Parameters("Mode").ToString())
                    If Not e.Parameters("EquipmentNo") Is Nothing And Not e.Parameters("GateinTransaction") Is Nothing Then
                        Dim strEqpmntNo As String
                        Dim strGateinTransaction As String
                        strEqpmntNo = e.Parameters("EquipmentNo").ToString()
                        strGateinTransaction = e.Parameters("GateinTransaction").ToString()
                        Dim drLeakTest As DataRow
                        dsLeakTestData = ObjLeakTest.GetLeakTestByEqpmntNoByDepotID(strEqpmntNo, strGateinTransaction, intDepotID)
                        For Each drActivityStatus As DataRow In dsLeakTestData.Tables(LeakTestData._ACTIVITY_STATUS).Rows
                            drLeakTest = dsLeakTestData.Tables(LeakTestData._LEAK_TEST).NewRow()
                            drLeakTest.Item(LeakTestData.LK_TST_ID) = CommonWeb.GetNextIndex(dsLeakTestData.Tables(LeakTestData._LEAK_TEST), LeakTestData.LK_TST_ID)
                            drLeakTest.Item(LeakTestData.EQPMNT_NO) = drActivityStatus.Item(LeakTestData.EQPMNT_NO)
                            drLeakTest.Item(LeakTestData.EQPMNT_TYP_CD) = drActivityStatus.Item(LeakTestData.EQPMNT_TYP_CD)
                            drLeakTest.Item(LeakTestData.GTN_DT) = drActivityStatus.Item(LeakTestData.GTN_DT)
                            drLeakTest.Item(LeakTestData.CSTMR_CD) = drActivityStatus.Item(LeakTestData.CSTMR_CD)
                            drLeakTest.Item(LeakTestData.CSTMR_ID) = drActivityStatus.Item(LeakTestData.CSTMR_ID)
                            drLeakTest.Item(LeakTestData.EQPMNT_STTS_CD) = drActivityStatus.Item(LeakTestData.EQPMNT_STTS_CD)
                            drLeakTest.Item(LeakTestData.CHECKED) = True
                            drLeakTest.Item(LeakTestData.TST_DT) = hdnCurrentDate.Value
                            dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Rows.Add(drLeakTest)
                        Next

                        If dsLeakTestData.Tables(LeakTestData._ACTIVITY_STATUS).Rows.Count = 0 Then
                            dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Rows(0).Item(LeakTestData.CHECKED) = True
                        End If
                        PageSubmitPane.Visible = False
                        hlnkPrint.Visible = False
                        ifgLeakTest.AllowPaging = False
                        ifgLeakTest.AllowAdd = False
                        ifgLeakTest.AllowDelete = False
                        ifgLeakTest.AllowSearch = False
                        ifgLeakTest.AllowRefresh = False

                        ifgLeakTest.AllowStaticHeader = True
                        ifgLeakTest.StaticHeaderHeight = "120"
                        ifgLeakTest.Width = 930
                        btnSubmitLeakTest.Visible = True
                    Else
                        dsLeakTestData = New LeakTestDataSet
                    End If
                Else
                    btnSubmitLeakTest.Visible = False
                    CacheData("PageName", "")
                    dsLeakTestData = ObjLeakTest.pub_GetLeakTestByActiveBit(intDepotID)
                End If
            End If
            e.DataSource = dsLeakTestData.Tables(LeakTestData._LEAK_TEST)
            CacheData(LEAK_TEST, dsLeakTestData)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgLeakTest_RowDataBound"

    Protected Sub ifgLeakTest_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgLeakTest.RowDataBound
        Try
            Dim datControl As iDate
            If e.Row.RowType = DataControlRowType.DataRow Then
                datControl = CType(DirectCast(DirectCast(e.Row.Cells(
                                6), iFgFieldCell).ContainingField, DateField).iDate, iDate)
                datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper

                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)

                Dim hypRevision As HyperLink
                Dim strPageName As String = ""
                If Not RetrieveData("PageName") Is Nothing Then
                    strPageName = RetrieveData("PageName").ToString
                End If

                hypRevision = CType(e.Row.Cells(12).Controls(0), HyperLink)
                If strPageName = "" Then
                    If (Not drv.Row.Item(LeakTestData.RVSN_NO) Is Nothing) AndAlso (drv.Row.Item(LeakTestData.RVSN_NO).ToString <> "0") Then
                        hypRevision.Attributes.Add("onClick", "showRevisionHistory();return false;")
                        hypRevision.NavigateUrl = "#"
                        hypRevision.Text = drv.Row.Item(LeakTestData.RVSN_NO).ToString
                    Else
                        hypRevision.Text = "0"
                    End If
                Else
                    hypRevision.Text = drv.Row.Item(LeakTestData.RVSN_NO).ToString
                    hypRevision.NavigateUrl = "#"
                    hypRevision.Attributes.Add("onClick", "showRepairMessage();return false;")
                    CType(e.Row.Cells(15), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    pvt_HideColumns()
                End If

                If Not e.Row.DataItem Is Nothing Then
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    Else
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                    End If
                End If
                If strPageName = "Repair" Then
                    CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                End If
                Dim chkActive As iFgCheckBox
                chkActive = CType(e.Row.Cells(15).Controls(0), iFgCheckBox)
                chkActive.Attributes.Add("onClick", String.Concat("lockData(this,'", CStr(drv.Row.Item(RepairInvoiceUpdateData.EQPMNT_NO)), "','", String.Empty, "');"))

                If e.Row.RowIndex > 6 Then
                    Dim lkpEquipmentNo As iLookup
                    lkpEquipmentNo = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpEquipmentNo.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgLeakTest_RowInserting"

    Protected Sub ifgLeakTest_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgLeakTest.RowInserting
        Try
            Dim lngLeakTestID As Long
            Dim objCommon As New CommonData
            dsLeakTestData = CType(RetrieveData(LEAK_TEST), LeakTestDataSet)
            lngLeakTestID = CommonWeb.GetNextIndex(dsLeakTestData.Tables(LeakTestData._LEAK_TEST), LeakTestData.LK_TST_ID)
            e.Values(LeakTestData.LK_TST_ID) = lngLeakTestID
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgLeakTest_RowDeleting"
    Protected Sub ifgLeakTest_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgLeakTest.RowDeleting
        Try
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgLeakTest.PageSize * ifgLeakTest.PageIndex + e.RowIndex
            dsLeakTestData = CType(RetrieveData(LEAK_TEST), LeakTestDataSet)
            Dim dtLeakTest As Data.DataTable = dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Copy
            If CType(ifgLeakTest.DataSource, DataTable).Select(String.Concat(LeakTestData.LK_TST_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                e.OutputParamters("Delete") = String.Concat("Leak Test : Equipment ", dtLeakTest.Rows(intRowIndex).Item(LeakTestData.EQPMNT_NO).ToString, " cannot be deleted")
            Else
                If dtLeakTest.Rows(intRowIndex).Item(LeakTestData.EQPMNT_NO) <> Nothing Then
                    e.OutputParamters("Delete") = String.Concat("Leak Test : Equipment ", dtLeakTest.Rows(intRowIndex).Item(LeakTestData.EQPMNT_NO).ToString, " has been be deleted. Click submit to save changes.")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_HideColumns"
    Private Sub pvt_HideColumns()
        Try
            ifgLeakTest.Columns.Item(1).Visible = False
            ifgLeakTest.Columns.Item(2).Visible = False
            ifgLeakTest.Columns.Item(3).Visible = False
            ifgLeakTest.Columns.Item(4).Visible = False
            ifgLeakTest.Columns.Item(15).Visible = False
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetLockData"
    Private Sub pvt_GetLockData(ByVal bv_blnCheckBit As Boolean, _
                                ByVal bv_strRefNo As String, _
                                ByVal bv_strActivityName As String)
        Try
            Dim objCommonData As New CommonData
            Dim strErrorMessage As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim strIpAddress As String = GetClientIPAddress()
            Dim strCurrentIpAddress As String = String.Empty
            Dim strUserName As String = String.Empty
            blnLockData = objCommonData.pub_GetLockData(bv_blnCheckBit, bv_strRefNo, strUserName, bv_strActivityName, strIpAddress, False, RepairInvoiceUpdateData.EQPMNT_NO)
            If blnLockData Then
                strCurrentIpAddress = GetClientIPAddress()
                If strCurrentIpAddress = strIpAddress Then
                    pub_SetCallbackReturnValue("IPError", "true")
                Else
                    pub_SetCallbackReturnValue("IPError", "false")
                End If
            End If
            pub_SetCallbackReturnValue("UserName", strUserName)
            pub_SetCallbackReturnValue("ActivityName", bv_strActivityName)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
   
   
End Class