
Partial Class Reports_LeakTest
    Inherits Pagebase

#Region "Declarations.."

    Dim dsLeakTest As New DataSet
    Private Const LEAK_TEST_DOCUMENT As String = "LEAK_TEST_DOCUMENT"
#End Region

#Region "Page_OnCallback"

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim objCommondata As New CommonData
            If Not Page.IsPostBack Then
                If objCommondata.GetMultiLocationSupportConfig.ToLower = "true" Then
                    hdnHQID.Value = 1
                End If
                If objCommondata.GetMultiLocationSupportConfig.ToLower = "false" Then
                    lkpDepotCode.Validator.IsRequired = False
                    lkpDepotCode.Text = objCommondata.GetDepotCD()
                    lkpDepotCode.ReadOnly = True
                    lkpDepotCode.Enabled = False
                    hdnHQID.Value = 1
                Else
                    If objCommondata.GetOrganizationTypeCD = "BO" Then
                        lkpDepotCode.Text = objCommondata.GetDepotCD()
                        lkpDepotCode.ReadOnly = True
                        lkpDepotCode.Enabled = False
                        hdnHQID.Value = 0
                    End If

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "FetchLeakTest"
                    pvt_LeakTestDetail(e.GetCallbackValue("Customer"), _
                                       e.GetCallbackValue("InDateFrom"), _
                                       e.GetCallbackValue("InDateTo"), _
                                       e.GetCallbackValue("TestDateFrom"), _
                                       e.GetCallbackValue("TestDateTo"), _
                                       e.GetCallbackValue("EquipemntNo"), _
                                       e.GetCallbackValue("DepotID"))
                Case "GetLeakTestDetail"
                    pvt_GetLeakTestDetail(e.GetCallbackValue("WFData"))

            End Select
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "pvt_LeakTestDetail"
    Private Sub pvt_LeakTestDetail(ByVal bv_intCustomer_id As Integer, ByVal bv_datInDateFrom As String, _
                                    ByVal bv_datInDateTo As String, ByVal bv_datTestDateFrom As String, ByVal bv_datTestDateTo As String, _
                                    ByVal bv_strEquipemntNo As String, ByVal bv_strDepotId As Integer)
        Try
            Dim objCommon As New CommonData
            Dim objLeakTest As New LeakTest
            If bv_strDepotId = 0 Then
                bv_strDepotId = CommonWeb.iInt(objCommon.GetDepotID())
            End If
            dsLeakTest = objLeakTest.pub_GetLeakTestDetail(bv_intCustomer_id, bv_datInDateFrom, bv_datInDateTo, bv_datTestDateFrom, bv_datTestDateTo, bv_strEquipemntNo, bv_strDepotId)
            CacheData(LeakTestData._V_LEAK_TEST, dsLeakTest)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"

    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Reports/LeakTestDetail.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_GetLeakTestDetail"
    Private Sub pvt_GetLeakTestDetail(ByVal bv_strWfData As String)
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
            dsLeakTest = CType(RetrieveData(LeakTestData._V_LEAK_TEST), LeakTestDataSet)
            Dim drALeakTest As DataRow()
            'For Each dr As DataRow In dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "=", "True"))
            '    If dr.RowState = DataRowState.Added Then
            '        pub_SetCallbackStatus(False)
            '        pub_SetCallbackError("Record must be submitted before generating document")
            '        Exit Sub
            '    End If
            'Next
            drALeakTest = dsLeakTest.Tables(LeakTestData._V_LEAK_TEST).Select(LeakTestData.CHECKED & "='True'")
            If Not drALeakTest.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
                'Else
                '    For Each dr As DataRow In dsLeakTest.Tables(LeakTestData._V_LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "=", "True"))
                '        CustomerId = dr(LeakTestData.CSTMR_ID)
                '        If (count > 0) And (CustomerId <> CompareCustmrId) Then
                '            pub_SetCallbackStatus(False)
                '            pub_SetCallbackError("Leak Test document cannot be generated for multiple customers.")
                '            Exit Sub
                '        End If
                '        CompareCustmrId = dr(LeakTestData.CSTMR_ID)
                '        count = count + 1
                '    Next
            End If

            objLeakTest.pub_UpdateGenerationDetails(dsLeakTest, _
                                                    intDepotId, _
                                                    strCurrentUserName, _
                                                    datModifiedDate)
            dsLeakTest.AcceptChanges()
            CacheData(LeakTestData._V_LEAK_TEST, dsLeakTest)
            dtLeakTest = dsDocument.Tables(LeakTestData._REPORT_LEAK_TEST).Clone()

            For Each dr As DataRow In dsLeakTest.Tables(LeakTestData._V_LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "=", "True"))
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

#Region "ifgLeakTest_ClientBind"

    Protected Sub ifgLeakTest_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgLeakTest.ClientBind
        Try
            Dim dtLeakTestDetail As New DataTable
            dsLeakTest = CType(RetrieveData(LeakTestData._V_LEAK_TEST), LeakTestDataSet)
            dtLeakTestDetail = dsLeakTest.Tables(LeakTestData._V_LEAK_TEST)
            e.DataSource = dsLeakTest.Tables(LeakTestData._V_LEAK_TEST)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region


    Protected Sub ifgLeakTest_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgLeakTest.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As System.Data.DataRowView
                    drv = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                    CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
End Class
