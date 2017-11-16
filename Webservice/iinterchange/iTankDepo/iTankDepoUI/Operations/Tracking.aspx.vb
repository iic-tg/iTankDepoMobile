
Partial Class Operations_Tracking
    Inherits Pagebase
    Dim bln_020KeyExist As Boolean
    Dim str_020KeyValue As String

#Region "Declarations"
    Dim dsTrackingDataSet As New TrackingDataSet
    Private Const TRACKING As String = "TRACKING"
#End Region

#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "GetTrackingDetails"
                pvt_GetTrackingDetails(e.GetCallbackValue("bv_strCstmr_ID"), e.GetCallbackValue("bv_strContainer_No"), e.GetCallbackValue("bv_datPickUpDate"), _
                                      e.GetCallbackValue("bv_datReceivedDate"), e.GetCallbackValue("bv_strTransmission_No"), e.GetCallbackValue("bv_strLss_ID"), _
                                      e.GetCallbackValue("bv_strActvty_ID"), e.GetCallbackValue("wfData"))

        End Select
    End Sub

#End Region

#Region "ifgTracking_ClientBind"

    Protected Sub ifgTracking_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgTracking.ClientBind
        Try
            dsTrackingDataSet = CType(RetrieveData(TRACKING), TrackingDataSet)
            e.DataSource = dsTrackingDataSet.Tables(TrackingData._V_TRACKING)
            CacheData(TRACKING, dsTrackingDataSet)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objcommon As New CommonData()
            str_020KeyValue = objcommon.GetConfigSetting("020", bln_020KeyExist)
            If bln_020KeyExist Then
                lblLessee.InnerText = str_020KeyValue
                lkpLessee.HelpText = String.Concat("Enter or select ", str_020KeyValue)
                DirectCast(ifgTracking.Columns.Item(2), iInterchange.WebControls.v4.Data.TextboxField).HeaderText = str_020KeyValue
                DirectCast(ifgTracking.Columns.Item(2), iInterchange.WebControls.v4.Data.TextboxField).HeaderTitle = str_020KeyValue
            End If
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetTrackingDetails"

    Private Sub pvt_GetTrackingDetails(ByVal bv_strCstmr_Id As String, ByVal bv_strCntnr_No As String, ByVal bv_strPckUpDte As String, _
                                      ByVal bv_strRcvdDte As String, ByVal bv_strTrnsmssn_No As String, ByVal bv_strLss_Id As String, _
                                      ByVal bv_strActvty As String, ByVal bv_strWFData As String)
        Try
            Dim objTracking As New Tracking
            dsTrackingDataSet.Tables(TrackingData._V_TRACKING).Clear()
            dsTrackingDataSet = objTracking.GetTrackingDetails(bv_strCstmr_Id, bv_strCntnr_No, bv_strPckUpDte, bv_strRcvdDte, _
                                                                 bv_strTrnsmssn_No, bv_strLss_Id, bv_strActvty, bv_strWFData)

            Dim dtTracking As New DataTable
            dtTracking = dsTrackingDataSet.Tables(TrackingData._V_TRACKING)
            For Each drTracking As DataRow In dtTracking.Rows
                Dim strRcvdDt As String = drTracking.Item(TrackingData.TRNSMSSN_NO)
                drTracking.Item(TrackingData.RCVD_DT) = CDate(strRcvdDt.Substring(1, 4) + "/" + strRcvdDt.Substring(5, 2) + "/" + strRcvdDt.Substring(7, 2))
            Next

            Dim intRowCount As Integer = dsTrackingDataSet.Tables(TrackingData._V_TRACKING).Rows.Count

            CacheData(TRACKING, dsTrackingDataSet)
            pub_SetCallbackReturnValue("RowCount", intRowCount)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"

    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(lkpCustomer)
        CommonWeb.pub_AttachHasChanges(txtContainerNo)
        CommonWeb.pub_AttachHasChanges(datPickUpDate)
        CommonWeb.pub_AttachHasChanges(datReceivedDate)
        CommonWeb.pub_AttachHasChanges(txtTransmissionNo)
        CommonWeb.pub_AttachHasChanges(lkpLessee)
        CommonWeb.pub_AttachHasChanges(lkpActivity)
    End Sub

#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Operations/Tracking.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTracking_RowDataBound"

    Protected Sub ifgTracking_RowDataBound(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgTracking.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                'If bln_020KeyExist Then
                '    CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = str_020KeyValue
                'End If
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim chkBulkEmail As New iInterchange.WebControls.v4.Data.iFgCheckBox
                chkBulkEmail = CType(e.Row.Cells(7).Controls(0), iInterchange.WebControls.v4.Data.iFgCheckBox)
                Dim chkResend As New iInterchange.WebControls.v4.Data.iFgCheckBox
                chkResend = CType(e.Row.Cells(8).Controls(0), iInterchange.WebControls.v4.Data.iFgCheckBox)
                If (drv.Item(TrackingData.ACTVTY_NAM).ToString.Trim = "Repair Complete") Or (drv.Item(TrackingData.ACTVTY_NAM).ToString.Trim = "Gate In") Or (drv.Item(TrackingData.ACTVTY_NAM).ToString.Trim = "Gate Out") Then
                    chkBulkEmail.Enabled = False
                End If
                If (drv.Item(TrackingData.PCKP_STTS_VC).ToString = "P") Then
                    chkResend.Enabled = True
                Else
                    chkResend.Enabled = False
                End If
                Dim hyplnk As HyperLink
                hyplnk = CType(e.Row.Cells(3).Controls(0), HyperLink)
                hyplnk.Attributes.Add("onclick", String.Concat("openActivityReport('", drv.Item(TrackingData.RFRNC_NO).ToString(), "','", drv.Item(TrackingData.ACTVTY_NAM).ToString(), "','", drv.Item(TrackingData.EIR_DT).ToString(), "','", drv.Item(TrackingData.EQPMNT_STTS_CD).ToString(), "');"))
                hyplnk.NavigateUrl = "#"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

End Class
