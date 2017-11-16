
Partial Class Admin_Depot
    Inherits Pagebase
#Region "Declaration"

    Private Const KEY_ID As String = "ID"
    Private Const KEY_MODE As String = "mode"
    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Private pvt_lngID As Long
    Private Const DEPOT As String = "DEPOT"
    Private pvt_strTotalDepotCount As String
    Dim dsDepot As DepotDataSet
    Dim dtDepot As DataTable
#End Region

#Region "Parameters"

    Private pvt_strPageMode As String
    Private pvt_intID As Integer

#End Region

#Region "PageLoad"

    ''' <summary>
    ''' Thios event is fired on page load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            pvt_SetChangesMade()
            Dim strKeyValue As String
            Dim objcommon As New CommonData()
            Dim blnKeyExist As Boolean = False
            pvt_strTotalDepotCount = Config.pub_GetAppConfigValue("TotalDepotCount")
            strKeyValue = objcommon.GetConfigSetting("070", blnKeyExist)
            If Not strKeyValue.ToLower = "true" Then
                lkpOrgizationType.Validator.IsRequired = False
                lkpDepotCode.Validator.IsRequired = False
            End If
            'strKeyValue = objcommon.GetConfigSetting("016", blnKeyExist)
            'If blnKeyExist Then
            '    If CBool(strKeyValue) Then
            '        txtVAT_NO.Validator.IsRequired = False
            '    End If
            'End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "SetChangesMade"
    ''' <summary>
    ''' This method is used to set haschanges to all fields in page
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(txtDpt_cd)
        CommonWeb.pub_AttachHasChanges(txtDpt_Nam)
        CommonWeb.pub_AttachHasChanges(txtCntct_Addrss_Ln1)
        CommonWeb.pub_AttachHasChanges(txtCntct_Addrss_Ln2)
        CommonWeb.pub_AttachHasChanges(txtCntct_Addrss_Ln3)
        CommonWeb.pub_AttachHasChanges(txtCntctPrsn)
        CommonWeb.pub_AttachHasChanges(txtEml_ID)
        CommonWeb.pub_AttachHasChanges(txtphn_no)
        CommonWeb.pub_AttachHasChanges(txtfx_no)
        CommonWeb.pub_AttachHasChanges(txtVAT_NO)
        CommonWeb.pub_AttachHasChanges(lkpDepotCode)
        CommonWeb.pub_AttachHasChanges(lkpOrgizationType)
        pub_SetGridChanges(ifgBankDetail, "ITab1_0")
    End Sub

#End Region

#Region "Page_OnCallback"
    ''' <summary>
    ''' This method is used to initialise call back methods
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "fnGetData"
                pvt_GetData(e.GetCallbackValue("mode"))
            Case "InsertDepot"
                pvt_CreateDepot(e.GetCallbackValue("DepotCode"), _
                                e.GetCallbackValue("DepotName"), _
                                e.GetCallbackValue("ContactPerson"), _
                                e.GetCallbackValue("ContactAddressLine1"), _
                                e.GetCallbackValue("ContactAddressLine2"), _
                                e.GetCallbackValue("ContactAddressLine3"), _
                                e.GetCallbackValue("EmailID"), _
                                e.GetCallbackValue("Phone"), _
                                e.GetCallbackValue("Fax"), _
                                e.GetCallbackValue("VAT"), _
                                e.GetCallbackValue("LG_PTH"), _
                                e.GetCallbackValue("wfData"), _
                                e.GetCallbackValue("OrganizationType"), _
                                e.GetCallbackValue("ReportingTo"))
            Case "UpdateDepot"
                pvt_UpdateDepot(e.GetCallbackValue("ID"), _
                                e.GetCallbackValue("DepotCode"), _
                                e.GetCallbackValue("DepotName"), _
                                e.GetCallbackValue("ContactPerson"), _
                                e.GetCallbackValue("ContactAddressLine1"), _
                                e.GetCallbackValue("ContactAddressLine2"), _
                                e.GetCallbackValue("ContactAddressLine3"), _
                                e.GetCallbackValue("EmailID"), _
                                e.GetCallbackValue("Phone"), _
                                e.GetCallbackValue("Fax"), _
                                e.GetCallbackValue("VAT"), _
                                e.GetCallbackValue("LG_PTH"), _
                                e.GetCallbackValue("wfData"), _
                                e.GetCallbackValue("OrganizationType"), _
                                e.GetCallbackValue("ReportingTo"))
            Case "ValidateDepotCode"
                pvt_ValidatePK(e.GetCallbackValue("DepotCode"))
            Case "GetHQDepotID"
                pvt_GetHQDepotID(e.GetCallbackValue("DepotCode"))
        End Select
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
            Dim sbDepot As New StringBuilder
            If bv_strMode = "edit" Then
                sbDepot.Append(CommonWeb.GetTextValuesJSO(txtDpt_cd, PageSubmitPane.pub_GetPageAttribute(DepotData.DPT_CD)))
                sbDepot.Append(CommonWeb.GetTextValuesJSO(txtDpt_Nam, PageSubmitPane.pub_GetPageAttribute(DepotData.DPT_NAM)))

                sbDepot.Append(CommonWeb.GetTextValuesJSO(txtCntctPrsn, PageSubmitPane.pub_GetPageAttribute(DepotData.CNTCT_PRSN_NAM)))
                sbDepot.Append(CommonWeb.GetTextValuesJSO(txtCntct_Addrss_Ln1, PageSubmitPane.pub_GetPageAttribute(DepotData.ADDRSS_LN1_VC)))
                If (PageSubmitPane.pub_GetPageAttribute(DepotData.ADDRSS_LN2_VC)) = Nothing Then
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtCntct_Addrss_Ln2, ""))
                Else
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtCntct_Addrss_Ln2, PageSubmitPane.pub_GetPageAttribute(DepotData.ADDRSS_LN2_VC)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(DepotData.ADDRSS_LN3_VC)) = Nothing Then
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtCntct_Addrss_Ln3, ""))
                Else
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtCntct_Addrss_Ln3, PageSubmitPane.pub_GetPageAttribute(DepotData.ADDRSS_LN3_VC)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(DepotData.EML_ID)) = Nothing Then
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtEml_ID, ""))
                Else
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtEml_ID, PageSubmitPane.pub_GetPageAttribute(DepotData.EML_ID)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(DepotData.PHN_NO)) = Nothing Then
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtphn_no, ""))
                Else
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtphn_no, PageSubmitPane.pub_GetPageAttribute(DepotData.PHN_NO)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(DepotData.FX_NO)) = Nothing Then
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtfx_no, ""))
                Else
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtfx_no, PageSubmitPane.pub_GetPageAttribute(DepotData.FX_NO)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(DepotData.VT_NO)) = Nothing Then
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtVAT_NO, ""))
                Else
                    sbDepot.Append(CommonWeb.GetTextValuesJSO(txtVAT_NO, PageSubmitPane.pub_GetPageAttribute(DepotData.VT_NO)))
                End If
                Dim pvt_strDomainPath As String = AppDomain.CurrentDomain.BaseDirectory.ToString
                If PageSubmitPane.pub_GetPageAttribute(DepotData.CMPNY_LG_PTH) <> Nothing AndAlso
                    IO.File.Exists(String.Concat(Server.MapPath(Config.pub_GetAppConfigValue("UploadPhotoPath")), PageSubmitPane.pub_GetPageAttribute(DepotData.CMPNY_LG_PTH))) Then
                    sbDepot.Append(CommonWeb.GetImageValuesJSO(imgCompanyLogo, String.Concat(Config.pub_GetAppConfigValue("UploadPhotoPath") + PageSubmitPane.pub_GetPageAttribute(DepotData.CMPNY_LG_PTH))))
                Else
                    sbDepot.Append(CommonWeb.GetImageValuesJSO(imgCompanyLogo, "../Images/noimage.jpg"))
                End If
                If PageSubmitPane.pub_GetPageAttribute(DepotData.RPRTNG_TO_ID) <> Nothing Then
                    If PageSubmitPane.pub_GetPageAttribute(DepotData.ORGNZTN_TYP_ID) = 153 Then
                        sbDepot.Append(CommonWeb.GetLookupValuesJSO(lkpDepotCode, PageSubmitPane.pub_GetPageAttribute(DepotData.RPRTNG_TO_ID), ""))
                    Else
                        sbDepot.Append(CommonWeb.GetLookupValuesJSO(lkpDepotCode, PageSubmitPane.pub_GetPageAttribute(DepotData.RPRTNG_TO_ID), PageSubmitPane.pub_GetPageAttribute(DepotData.RPRTNG_TO_CD)))
                    End If
                Else
                    sbDepot.Append(CommonWeb.GetLookupValuesJSO(lkpDepotCode, "", ""))
                End If
                If PageSubmitPane.pub_GetPageAttribute(DepotData.ORGNZTN_TYP_ID) <> Nothing Then
                    sbDepot.Append(CommonWeb.GetLookupValuesJSO(lkpOrgizationType, PageSubmitPane.pub_GetPageAttribute(DepotData.ORGNZTN_TYP_ID), PageSubmitPane.pub_GetPageAttribute(DepotData.ORGNZTN_TYP_CD)))
                    If PageSubmitPane.pub_GetPageAttribute(DepotData.ORGNZTN_TYP_ID) = 153 Then
                        sbDepot.Append("setReadOnly('lkpDepotCode', true); setReadOnly('lkpOrgizationType', true); ")
                    Else
                        sbDepot.Append("setReadOnly('lkpDepotCode', false); setReadOnly('lkpOrgizationType', false);")
                    End If
                Else
                    sbDepot.Append(CommonWeb.GetLookupValuesJSO(lkpOrgizationType, "", ""))
                End If
              
                sbDepot.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(DepotData.DPT_ID), "');"))
                pub_SetCallbackReturnValue("Message", sbDepot.ToString)
                pub_SetCallbackReturnValue("ID", pvt_lngID.ToString)
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateDepot"

    Protected Sub pvt_CreateDepot(ByVal bv_strDPT_CD As String, _
        ByVal bv_strDPT_NAM As String, _
        ByVal bv_strCNTCT_PRSN_NAM As String, _
        ByVal bv_strADDRSS_LN1_VC As String, _
        ByVal bv_strADDRSS_LN2_VC As String, _
        ByVal bv_strADDRSS_LN3_VC As String, _
        ByVal bv_strEML_ID As String, _
        ByVal bv_strPHN_NO As String, _
        ByVal bv_strFX_NO As String, _
        ByVal bv_strVT_NO As String, _
        ByVal bv_strLG_PTH As String, _
        ByVal bv_strwfData As String, _
        ByVal bv_strOrganizationType As String, _
        ByVal bv_strReportingTo As String)

        Try
            dsDepot = CType(RetrieveData(DEPOT), DepotDataSet)
            If dsDepot.Tables(DepotData._V_BANK_DETAIL).Rows.Count = 0 Then
                pub_SetCallbackReturnValue("checkBankDetail", True)
                pub_SetCallbackStatus(True)
                Exit Sub
            End If
            Dim objDepot As New Depot
            Dim objcommon As New CommonData
            If Not pvt_strTotalDepotCount > objDepot.pub_GetAllDepotDetails().Tables(DepotData._V_DEPOT).Rows.Count Then
                pub_SetCallbackReturnValue("DepotTotalCount", True)
                pub_SetCallbackStatus(True)
                Exit Sub
            End If
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As DateTime = objcommon.GetCurrentDate()
            Dim strLogoPath As String = bv_strLG_PTH.Substring(bv_strLG_PTH.LastIndexOf("/") + 1)
            Dim lng As Long = objDepot.pub_CreateDEPOT(bv_strDPT_CD, bv_strDPT_NAM, _
                                                          bv_strCNTCT_PRSN_NAM, bv_strADDRSS_LN1_VC, _
                                                          bv_strADDRSS_LN2_VC, bv_strADDRSS_LN3_VC, _
                                                          bv_strVT_NO, _
                                                          bv_strEML_ID, bv_strPHN_NO, _
                                                          bv_strFX_NO, _
                                                          strLogoPath, strModifiedby, _
                                                          datModifiedDate, bv_strwfData, CommonWeb.iLng(bv_strOrganizationType), CommonWeb.iLng(bv_strReportingTo), dsDepot)

            pub_SetCallbackReturnValue("Message", String.Concat("Depot : ", bv_strDPT_CD, " ", strMSGINSERT))
            pub_SetCallbackReturnValue("ID", CStr(lng))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateDepot"

    Protected Sub pvt_UpdateDepot(ByVal bv_strDPT_ID As String, _
                            ByVal bv_strDPT_CD As String, _
                            ByVal bv_strDPT_NAM As String, _
                            ByVal bv_strCNTCT_PRSN_NAM As String, _
                            ByVal bv_strADDRSS_LN1_VC As String, _
                            ByVal bv_strADDRSS_LN2_VC As String, _
                            ByVal bv_strADDRSS_LN3_VC As String, _
                            ByVal bv_strEML_ID As String, _
                            ByVal bv_strPHN_NO As String, _
                            ByVal bv_strFX_NO As String, _
                            ByVal bv_strVT_NO As String, _
                            ByVal bv_strLG_PTH As String, _
                            ByVal bv_strwfData As String, _
                            ByVal bv_strOrganizationType As String, _
                            ByVal bv_strReportingTo As String)

        Try
            dsDepot = New DepotDataSet
            dtDepot = CType(ifgBankDetail.DataSource, DataTable)
            dsDepot = CType(RetrieveData(DEPOT), DepotDataSet)
            If dsDepot.Tables(DepotData._V_BANK_DETAIL).Rows.Count = 0 Then
                pub_SetCallbackReturnValue("checkBankDetail", True)
                pub_SetCallbackStatus(True)
                Exit Sub
            End If
            Dim objDepot As New Depot
            Dim objcommon As New CommonData
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As DateTime = objcommon.GetCurrentDate()
            Dim strLogoPath As String = bv_strLG_PTH.Substring(bv_strLG_PTH.LastIndexOf("/") + 1)
            objDepot.pub_ModifyDEPOT(bv_strDPT_ID, _
                  bv_strDPT_CD, bv_strDPT_NAM, _
                  bv_strCNTCT_PRSN_NAM, bv_strADDRSS_LN1_VC, _
                  bv_strADDRSS_LN2_VC, bv_strADDRSS_LN3_VC, _
                  bv_strVT_NO, _
                  bv_strEML_ID, bv_strPHN_NO, _
                  bv_strFX_NO, _
                  strLogoPath, strModifiedby, _
                  datModifiedDate, bv_strwfData, CommonWeb.iLng(bv_strOrganizationType), CommonWeb.iLng(bv_strReportingTo), dsDepot)

            pub_SetCallbackReturnValue("Message", String.Concat("Depot : ", bv_strDPT_CD, " ", strMSGUPDATE))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetHQDepotID"
    Protected Sub pvt_GetHQDepotID(ByVal bv_strDepotCode As String)
        dsDepot = New DepotDataSet
        Dim objDepot As New Depot
        dsDepot = objDepot.pub_GetAllDepotDetails()
        If dsDepot.Tables(DepotData._V_DEPOT).Select(String.Concat(DepotData.ORGNZTN_TYP_CD, " = 'HQ' AND ", DepotData.DPT_CD, " <> '", bv_strDepotCode, "'")).Length > 0 Then
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("HQDepotCount", dsDepot.Tables(DepotData._V_DEPOT).Select(String.Concat(DepotData.ORGNZTN_TYP_CD, " = 'HQ' ")).Length.ToString)
        End If
    End Sub
#End Region

#Region "pvt_ValidatePK()"
    Private Sub pvt_ValidatePK(ByVal bv_strDepotCode As String)
        Try
            Dim objDepot As New Depot
            Dim bolValid As Boolean
            bolValid = objDepot.pub_ValidatePKDepot(bv_strDepotCode)
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

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Admin/Depot.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgBankDetail_ClientBind"
    Protected Sub ifgBankDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgBankDetail.ClientBind
        Try
            Dim objDepot As New Depot
            Dim objcommon As New CommonData
            'Dim intDepotID As Integer = objcommon.GetDepotID()
            Dim intDepotID As Integer = e.Parameters("DepotID")
            dsDepot = objDepot.pub_GetBankDetail(intDepotID)
            e.DataSource = dsDepot.Tables(DepotData._V_BANK_DETAIL)
            CacheData(DEPOT, dsDepot)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgBankDetail_RowDeleting"

    Protected Sub ifgBankDetail_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgBankDetail.RowDeleting
        Try
            dsDepot = CType(RetrieveData(DEPOT), DepotDataSet)
            dtDepot = dsDepot.Tables(DepotData._V_BANK_DETAIL)
            Dim dr As DataRow() = dtDepot.Select(String.Concat(DepotData.BNK_DTL_BIN, "=", DepotData.BNK_DTL_BIN))
            If dr.Length = 1 Then
                e.OutputParamters.Add("Duplicate", "Atleast one Bank Detail must be entered.")
                e.Cancel = True
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

    Protected Sub ifgBankDetail_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgBankDetail.RowInserted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ifgBankDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgBankDetail.RowInserting
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ifgBankDetail_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgBankDetail.RowUpdated

    End Sub

    Protected Sub ifgBankDetail_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgBankDetail.RowUpdating

    End Sub
End Class