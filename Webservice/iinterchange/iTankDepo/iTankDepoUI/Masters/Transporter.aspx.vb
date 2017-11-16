
Partial Class Masters_Transporter
    Inherits Pagebase

#Region "Declaration"
    Dim dsTransporter As New TransporterDataSet
    Dim TRANSPORTER As String = "TRANSPORTER"
    Private strMSGINSERT As String = " Inserted Successfully."
    Private strMSGUPDATE As String = " Updated Successfully."
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            '  chkDefault.Attributes.Add("onclick", "checkDefault(this)")
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/Transporter.js", MyBase.Page)
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
                Case "ValidateCode"
                    pvt_ValidateTransporterCode(e.GetCallbackValue("Code"))
                Case "CheckDefault"
                    pvt_CheckDefault()
                Case "ValidateRouteCode"
                    pvt_ValidateRouteCode(e.GetCallbackValue("Code"), _
                                          e.GetCallbackValue("GridIndex"), _
                                          e.GetCallbackValue("RowState"), _
                                          e.GetCallbackValue("WFDATA"), _
                                          e.GetCallbackValue("TransporterCode"))
                Case "CreateTransporter"
                    pvt_CreateTransporter(e.GetCallbackValue("TransporterCode"), _
                                          e.GetCallbackValue("TransporterDescription"), _
                                          e.GetCallbackValue("ContactPerson"), _
                                          e.GetCallbackValue("ContactAddress"), _
                                          e.GetCallbackValue("ZipCode"), _
                                          e.GetCallbackValue("PhoneNo"), _
                                          e.GetCallbackValue("Fax"), _
                                          e.GetCallbackValue("Email"), _
                                          e.GetCallbackValue("ActiveBit"))
                Case "UpdateTransporter"
                    pvt_UpdateTransporter(e.GetCallbackValue("TransporterId"), _
                                          e.GetCallbackValue("TransporterCode"), _
                                          e.GetCallbackValue("TransporterDescription"), _
                                          e.GetCallbackValue("ContactPerson"), _
                                          e.GetCallbackValue("ContactAddress"), _
                                          e.GetCallbackValue("ZipCode"), _
                                          e.GetCallbackValue("PhoneNo"), _
                                          e.GetCallbackValue("Fax"), _
                                          e.GetCallbackValue("Email"), _
                                          e.GetCallbackValue("ActiveBit"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(txtTransporterCode)
        CommonWeb.pub_AttachHasChanges(txtTransporterDescription)
        CommonWeb.pub_AttachHasChanges(txtContactPerson)
        CommonWeb.pub_AttachHasChanges(txtZipCode)
        CommonWeb.pub_AttachHasChanges(txtPhoneNo)
        CommonWeb.pub_AttachHasChanges(txtFax)
        CommonWeb.pub_AttachDescMaxlength(txtEmailID)
        CommonWeb.pub_AttachDescMaxlength(txtContactAddress)
        CommonWeb.pub_AttachHasChanges(chkActive)
        pub_SetGridChanges(ifgRouteDetail, "tabTransporter")
    End Sub
#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim sbTransporter As New StringBuilder
            If bv_strMode = MODE_EDIT Then
                sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtTransporterCode, PageSubmitPane.pub_GetPageAttribute(TransporterData.TRNSPRTR_CD)))
                sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtTransporterDescription, PageSubmitPane.pub_GetPageAttribute(TransporterData.TRNSPRTR_DSCRPTN)))
                sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtContactPerson, PageSubmitPane.pub_GetPageAttribute(TransporterData.CNTCT_PRSN)))
                If PageSubmitPane.pub_GetPageAttribute(TransporterData.CNTCT_ADDRSS) = Nothing Then
                    sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtContactAddress, ""))
                Else
                    sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtContactAddress, PageSubmitPane.pub_GetPageAttribute(TransporterData.CNTCT_ADDRSS)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransporterData.ZP_CD) = Nothing Then
                    sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtZipCode, ""))
                Else
                    sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtZipCode, PageSubmitPane.pub_GetPageAttribute(TransporterData.ZP_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransporterData.PHN_NO) = Nothing Then
                    sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtPhoneNo, ""))
                Else
                    sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtPhoneNo, PageSubmitPane.pub_GetPageAttribute(TransporterData.PHN_NO)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransporterData.FX_NO) = Nothing Then
                    sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtFax, ""))
                Else
                    sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtFax, PageSubmitPane.pub_GetPageAttribute(TransporterData.FX_NO)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransporterData.EML_ID) = Nothing Then
                    sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtEmailID, ""))
                Else
                    sbTransporter.Append(CommonWeb.GetTextValuesJSO(txtEmailID, PageSubmitPane.pub_GetPageAttribute(TransporterData.EML_ID)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(TransporterData.ACTV_BT) <> Nothing Then
                    sbTransporter.Append(CommonWeb.GetCheckboxValuesJSO(chkActive, CBool(PageSubmitPane.pub_GetPageAttribute(TransporterData.ACTV_BT))))
                End If
                sbTransporter.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(TransporterData.TRNSPRTR_ID), "');"))
                pub_SetCallbackReturnValue("Message", sbTransporter.ToString)
                pub_SetCallbackStatus(True)
            End If
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgRouteDetail_ClientBind"
    Protected Sub ifgRouteDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgRouteDetail.ClientBind
        Try
            Dim objCommonData As New CommonData
            Dim objTransporter As New Transporter
            Dim strModifiedBy As String = String.Empty
            Dim datModifiedDate As DateTime = Nothing
            Dim intDepotId As Integer = 0
            Dim dtBankDetail As New DataTable

            If objCommonData.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotId = CInt(objCommonData.GetHeadQuarterID())
            Else
                intDepotId = CInt(objCommonData.GetDepotID())
            End If
            datModifiedDate = objCommonData.GetCurrentDate()
            strModifiedBy = objCommonData.GetCurrentUserName()

            If e.Parameters("Mode") <> Nothing Then
                If e.Parameters("Mode") = MODE_NEW Then
                    dsTransporter = New TransporterDataSet
                    If dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Rows.Count = 0 Then
                        Dim drRoute As DataRow = Nothing
                        drRoute = dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).NewRow()
                        drRoute.Item(TransporterData.TRNSPRTR_RT_DTL_ID) = CommonWeb.GetNextIndex(dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL), TransporterData.TRNSPRTR_RT_DTL_ID)
                        drRoute.Item(TransporterData.EMPTY_TRP_SPPLR_RT_NC) = "0.00"
                        drRoute.Item(TransporterData.EMPTY_TRP_CSTMR_RT_NC) = "0.00"
                        drRoute.Item(TransporterData.FLL_TRP_SPPLR_RT_NC) = "0.00"
                        drRoute.Item(TransporterData.FLL_TRP_CSTMR_RT_NC) = "0.00"
                        dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Rows.Add(drRoute)
                    End If
                ElseIf e.Parameters("Mode") = MODE_EDIT OrElse e.Parameters("Mode") = "ReBind" Then
                    If e.Parameters("TransporterID") <> Nothing Then
                        dsTransporter = objTransporter.pub_GetTransporterRouteDetailByTransporterID(CLng(e.Parameters("TransporterID")), intDepotId)
                    End If
                ElseIf e.Parameters("Mode") = "ReBindCopy" Then
                    dsTransporter = CType(RetrieveData(TRANSPORTER), TransporterDataSet)
                End If
                objTransporter.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)
            End If
            dsTransporter.Tables(TransporterData._V_BANK_DETAIL).Merge(dtBankDetail)
            e.DataSource = dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL)
            CacheData(TRANSPORTER, dsTransporter)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateTransporterCode()"
    Public Sub pvt_ValidateTransporterCode(ByVal bv_strTransporterCode As String)

        Try
            Dim objCommon As New CommonData
            Dim objTransporter As New Transporter
            Dim intDepotID As Int32
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommon.GetDepotID())
            End If
            dsTransporter = objTransporter.pub_GetTransporterByTransporterCode(bv_strTransporterCode, intDepotID)
            If dsTransporter.Tables(TransporterData._V_TRANSPORTER).Rows.Count = 0 Then
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

#Region "pvt_CheckDefault()"
    Private Sub pvt_CheckDefault()
        Try
            Dim objCommon As New CommonData
            Dim objTransporter As New Transporter
            Dim intDepotID As Int32 = CInt(objCommon.GetDepotID())
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommon.GetDepotID())
            End If
            dsTransporter = objTransporter.pub_CheckDefaultByDepotId(intDepotID)
            If dsTransporter.Tables(TransporterData._V_TRANSPORTER).Rows.Count = 0 Then
                pub_SetCallbackReturnValue("valid", "true")
            Else
                pub_SetCallbackReturnValue("valid", "false")
                pub_SetCallbackReturnValue("Code", dsTransporter.Tables(TransporterData._V_TRANSPORTER).Rows(0).Item(TransporterData.TRNSPRTR_CD).ToString)
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateRouteCode"
    Private Sub pvt_ValidateRouteCode(ByVal bv_strCode As String, _
                                      ByVal bv_intGridRowIndex As Integer, _
                                      ByVal bv_strRowState As String, _
                                      ByVal bv_strWFDATA As String, _
                                      ByVal bv_strTransporterCode As String)
        Try
            Dim blnValid As Boolean
            Dim objCommonData As New CommonData
            Dim strExistCode As String = String.Empty
            Dim intDepotId As Integer
            If objCommonData.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotId = CInt(objCommonData.GetHeadQuarterID())
            Else
                intDepotId = CInt(objCommonData.GetDepotID())
            End If
            dsTransporter = CType(RetrieveData(TRANSPORTER), TransporterDataSet)
            Dim dtRouteDetail As DataTable = dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Copy
            Dim intResultIndex() As System.Data.DataRow = dtRouteDetail.Select(String.Concat(TransporterData.RT_CD, "='", bv_strCode, "' "))
            Dim strExistRoute As String = ""
            If intResultIndex.Length > 0 Then
                If dtRouteDetail.Rows.Count > bv_intGridRowIndex Then
                    If dtRouteDetail.Rows(bv_intGridRowIndex).RowState = Data.DataRowState.Deleted Then
                        strExistRoute = String.Empty
                    ElseIf dtRouteDetail.Rows(bv_intGridRowIndex).RowState <> Data.DataRowState.Deleted Then
                        strExistRoute = dtRouteDetail.Rows(bv_intGridRowIndex)(CustomerData.RT_CD).ToString
                    End If
                End If
                If strExistRoute = strExistRoute Then
                    blnValid = True
                Else
                    blnValid = False
                End If
            Else
                blnValid = True
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgRouteDetail_RowInserting"
    Protected Sub ifgRouteDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgRouteDetail.RowInserting
        Try
            Dim dtTransporter As New DataTable
            dsTransporter = CType(RetrieveData(TRANSPORTER), TransporterDataSet)
            dtTransporter = dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL)
            e.Values(TransporterData.TRNSPRTR_RT_DTL_ID) = CommonWeb.GetNextIndex(dtTransporter, TransporterData.TRNSPRTR_RT_DTL_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgRouteDetail_RowDataBound"
    Protected Sub ifgRouteDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgRouteDetail.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                dsTransporter = CType(RetrieveData(TRANSPORTER), TransporterDataSet)
                If Not IsNothing(dsTransporter) Then
                    CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Empty Trip Supplier Rate (", dsTransporter.Tables(TransporterData._V_BANK_DETAIL).Rows(0).Item(TransporterData.CRRNCY_CD), ")")
                    CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Empty Trip Customer Rate (", dsTransporter.Tables(TransporterData._V_BANK_DETAIL).Rows(0).Item(TransporterData.CRRNCY_CD), ")")
                    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Full Trip Supplier Rate (", dsTransporter.Tables(TransporterData._V_BANK_DETAIL).Rows(0).Item(TransporterData.CRRNCY_CD), ")")
                    CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Full Trip Customer Rate (", dsTransporter.Tables(TransporterData._V_BANK_DETAIL).Rows(0).Item(TransporterData.CRRNCY_CD), ")")
                End If

            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowIndex > 1 Then
                    Dim lkpControl As iLookup
                    lkpControl = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpControl.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateTransporter()"
    Private Sub pvt_CreateTransporter(ByVal bv_strTransporterCode As String, _
                                      ByVal bv_strTransporterDescription As String, _
                                      ByVal bv_strContactPerson As String, _
                                      ByVal bv_strContactAddress As String, _
                                      ByVal bv_strZipCode As String, _
                                      ByVal bv_strPhoneNo As String, _
                                      ByVal bv_strFax As String, _
                                      ByVal bv_strEmail As String, _
                                      ByVal bv_strActiveBit As String)
        Try
            Dim objTransporter As New Transporter
            Dim objCommonData As New CommonData
            Dim intDepotID As Integer
            If objCommonData.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommonData.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommonData.GetDepotID())
            End If
            Dim strModifiedby As String = objCommonData.GetCurrentUserName()
            Dim strModifiedDate As String = objCommonData.GetCurrentDate()
            Dim lngCreateTransporterId As Long = 0
            dsTransporter = CType(RetrieveData(TRANSPORTER), TransporterDataSet)
            If dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Rows.Count < 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("At least one route detail is mandatory for Transporter.")
                Exit Sub
            End If
            lngCreateTransporterId = objTransporter.pub_CreateTransporter(bv_strTransporterCode, _
                                                                          bv_strTransporterDescription, _
                                                                          bv_strContactPerson, _
                                                                          bv_strContactAddress, _
                                                                          bv_strZipCode, _
                                                                          bv_strPhoneNo, _
                                                                          bv_strFax, _
                                                                          bv_strEmail, _
                                                                          CBool(bv_strActiveBit), _
                                                                          strModifiedby, _
                                                                          CDate(strModifiedDate), _
                                                                          strModifiedby, _
                                                                          CDate(strModifiedDate), _
                                                                          intDepotId, _
                                                                          dsTransporter)
            dsTransporter.AcceptChanges()
            pub_SetCallbackReturnValue("Message", String.Concat("Transporter Code : ", bv_strTransporterCode, strMSGINSERT))
            pub_SetCallbackReturnValue("TransporterId", lngCreateTransporterId)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateTransporter()"
    Private Sub pvt_UpdateTransporter(ByVal bv_strTransporterID As String, _
                                      ByVal bv_strTransporterCode As String, _
                                      ByVal bv_strTransporterDescription As String, _
                                      ByVal bv_strContactPerson As String, _
                                      ByVal bv_strContactAddress As String, _
                                      ByVal bv_strZipCode As String, _
                                      ByVal bv_strPhoneNo As String, _
                                      ByVal bv_strFax As String, _
                                      ByVal bv_strEmail As String, _
                                      ByVal bv_strActiveBit As String)
        Try
            Dim objTransporter As New Transporter
            Dim objCommonData As New CommonData
            Dim intDepotID As Integer
            If objCommonData.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommonData.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommonData.GetDepotID())
            End If
            Dim strModifiedby As String = objCommonData.GetCurrentUserName()
            Dim strModifiedDate As String = objCommonData.GetCurrentDate()

            dsTransporter = CType(RetrieveData(TRANSPORTER), TransporterDataSet)

            objTransporter.pub_ModifyTransporter(CLng(bv_strTransporterID), _
                                                 bv_strTransporterCode, _
                                                 bv_strTransporterDescription, _
                                                 bv_strContactPerson, _
                                                 bv_strContactAddress, _
                                                 bv_strZipCode, _
                                                 bv_strPhoneNo, _
                                                 bv_strFax, _
                                                 bv_strEmail, _
                                                 CBool(bv_strActiveBit), _
                                                 strModifiedby, _
                                                 CDate(strModifiedDate), _
                                                 strModifiedby, _
                                                 CDate(strModifiedDate), _
                                                 intDepotId, _
                                                 dsTransporter)
            dsTransporter.AcceptChanges()
            pub_SetCallbackReturnValue("TransporterId", bv_strTransporterID)
            pub_SetCallbackReturnValue("Message", String.Concat("Transporter Code: ", bv_strTransporterCode, strMSGUPDATE))
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgRouteDetail_RowDeleting"
    Protected Sub ifgRouteDetail_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgRouteDetail.RowDeleting
        Try
            dsTransporter = CType(RetrieveData(TRANSPORTER), TransporterDataSet)
            If Not dsTransporter.Tables(TransporterData._V_TRANSPORTER_ROUTE_DETAIL).Rows.Count > 1 Then
                e.Cancel = True
                e.OutputParamters.Add("Error", "At least one route detail is mandatory for Transporter.")
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
