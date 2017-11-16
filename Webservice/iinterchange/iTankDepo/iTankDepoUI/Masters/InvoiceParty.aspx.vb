
Option Strict On
Partial Class Masters_InvoiceParty
    Inherits Pagebase

#Region "Declaration"

    Private Const KEY_ID As String = "ID"
    Private Const KEY_MODE As String = "mode"
    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Dim dsInvoiceParty As InvoicePartyDataSet
    Private pvt_lngID As Long

#End Region

#Region "Parameters"

    Private pvt_strPageMode As String
    Private pvt_intID As Integer

#End Region

#Region "OnCallback"

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub


    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "fnGetData"
                pvt_Getdata(e.GetCallbackValue("mode"))
            Case "InsertInvoiceParty"
                pub_CreateInvoiceParty(e.GetCallbackValue("InvoicePartyCode"), _
                 e.GetCallbackValue("InvoicePartyName"), _
                 e.GetCallbackValue("ContactPersonName"), _
                 e.GetCallbackValue("ContactJobTitle"), _
                 e.GetCallbackValue("ContactAddress"), _
                 e.GetCallbackValue("BillingAddress"), _
                 e.GetCallbackValue("ZipCode"), _
                 e.GetCallbackValue("PhoneNo"), _
                 e.GetCallbackValue("FaxNo"), _
                 e.GetCallbackValue("Remarks"), _
                 e.GetCallbackValue("BaseCurrency"), _
                 e.GetCallbackValue("ReportingEmailID"), _
                 e.GetCallbackValue("InvoicingEmailID"), _
                 e.GetCallbackValue("Active"), _
                 CBool(e.GetCallbackValue("FinanceIntegrationBit")), _
                 e.GetCallbackValue("bv_LedgerId"), _
                 e.GetCallbackValue("wfData"))
            Case "UpdateInvoiceParty"
                pvt_UpdateInvoiceParty(e.GetCallbackValue("ID"), _
                 e.GetCallbackValue("InvoicePartyCode"), _
                 e.GetCallbackValue("InvoicePartyName"), _
                 e.GetCallbackValue("ContactPersonName"), _
                 e.GetCallbackValue("ContactJobTitle"), _
                 e.GetCallbackValue("ContactAddress"), _
                 e.GetCallbackValue("BillingAddress"), _
                 e.GetCallbackValue("ZipCode"), _
                 e.GetCallbackValue("PhoneNo"), _
                 e.GetCallbackValue("FaxNo"), _
                 e.GetCallbackValue("Remarks"), _
                 e.GetCallbackValue("BaseCurrency"), _
                 e.GetCallbackValue("ReportingEmailID"), _
                 e.GetCallbackValue("InvoicingEmailID"), _
                 e.GetCallbackValue("Active"), _
                 CBool(e.GetCallbackValue("FinanceIntegrationBit")), _
                 e.GetCallbackValue("bv_LedgerId"), _
                 e.GetCallbackValue("wfData"))
            Case "ValidateCode"
                pvt_ValidateInvociePartyCode(e.GetCallbackValue("Code"))
        End Select
    End Sub

#End Region

#Region "SetChangesMade"
    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(chkACTV_BT)
        CommonWeb.pub_AttachHasChanges(txtInvoicePartyCode)
        CommonWeb.pub_AttachHasChanges(txtInvoicePartyName)
        CommonWeb.pub_AttachHasChanges(txtContactPersonName)
        CommonWeb.pub_AttachHasChanges(txtContactJobTitle)
        CommonWeb.pub_AttachDescMaxlength(txtContactAddress)
        CommonWeb.pub_AttachDescMaxlength(txtBillingAddress)
        CommonWeb.pub_AttachHasChanges(txtZipCode)
        CommonWeb.pub_AttachDescMaxlength(txtRemarks)
        CommonWeb.pub_AttachHasChanges(txtPhoneNo)
        CommonWeb.pub_AttachHasChanges(txtFaxNo)
        CommonWeb.pub_AttachDescMaxlength(txtReportingEmailID)
        CommonWeb.pub_AttachDescMaxlength(txtInvoicingEmailID)
        CommonWeb.pub_AttachHasChanges(lkpBaseCrrncy)
        CommonWeb.pub_AttachHasChanges(txtLedgerId)

    End Sub
#End Region

#Region "pvt_ValidateProductCode"
    Public Sub pvt_ValidateInvociePartyCode(ByVal bv_striNVOICEPARTYCode As String)
        Try
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim intDepotID As Int32 = CInt(objCommon.GetDepotID())
            Dim blnValid As Boolean
            Dim strServiceType As String = String.Empty
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            End If
            blnValid = objCommonUI.pub_GetServicePartnerByCode(bv_striNVOICEPARTYCode, strServiceType, intDepotID)
            If blnValid Then
                pub_SetCallbackReturnValue("valid", "true")
            Else
                If strServiceType = "PARTY" Then
                    pub_SetCallbackReturnValue("valid", "The code is already present for an existing Invoicing Party.")
                Else
                    pub_SetCallbackReturnValue("valid", "The code is already present for an existing Customer.")
                End If
            End If

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "Getdata"
    ''' <summary>
    ''' Name     : pvt_GetData
    ''' Purpose  : pvt_GetData function is used to Get User Details By ID From View
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_Getdata(ByVal bv_strMode As String)

        Try
            Dim sbINVOICE_PARTY As New StringBuilder
            If bv_strMode = MODE_EDIT Then
                sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtInvoicePartyCode, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.INVCNG_PRTY_CD)))
                sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtInvoicePartyName, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.INVCNG_PRTY_NM)))
                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.CNTCT_PRSN_NM)) = Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtContactPersonName, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtContactPersonName, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.CNTCT_PRSN_NM)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.CNTCT_JB_TTL)) = Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtContactJobTitle, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtContactJobTitle, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.CNTCT_JB_TTL)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.CNTCT_ADDRSS)) = Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtContactAddress, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtContactAddress, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.CNTCT_ADDRSS)))
                End If

                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.BLLNG_ADDRSS)) = Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtBillingAddress, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtBillingAddress, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.BLLNG_ADDRSS)))
                End If

                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.ZP_CD)) = Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtZipCode, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtZipCode, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.ZP_CD)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.RMRKS_VC)) = Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtRemarks, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtRemarks, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.RMRKS_VC)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.PHN_NO)) = Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtPhoneNo, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtPhoneNo, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.PHN_NO)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.FX_NO)) = Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtFaxNo, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtFaxNo, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.FX_NO)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.RPRTNG_EML_ID)) = Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtReportingEmailID, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtReportingEmailID, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.RPRTNG_EML_ID)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.INVCNG_EML_ID)) = Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtInvoicingEmailID, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtInvoicingEmailID, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.INVCNG_EML_ID)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.BS_CRRNCY_ID)) = Nothing Then
                End If
              
                If PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.BS_CRRNCY_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.BS_CRRNCY_ID) <> "" Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetLookupValuesJSO(lkpBaseCrrncy, PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.BS_CRRNCY_ID), PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.BS_CRRNCY_CD)))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetLookupValuesJSO(lkpBaseCrrncy, "", ""))
                End If

                sbINVOICE_PARTY.Append(CommonWeb.GetCheckboxValuesJSO(chkACTV_BT, CBool(PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.ACTV_BT))))
                sbINVOICE_PARTY.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(InvoicePartyData.INVCNG_PRTY_ID), "');"))

                'Finance Integration
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.LDGR_ID) Is Nothing Then
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtLedgerId, ""))
                Else
                    sbINVOICE_PARTY.Append(CommonWeb.GetTextValuesJSO(txtLedgerId, PageSubmitPane.pub_GetPageAttribute(CustomerData.LDGR_ID)))
                End If

            End If
            pub_SetCallbackReturnValue("Message", sbINVOICE_PARTY.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pub_CreateInvoiceParty"

    Protected Sub pub_CreateInvoiceParty(ByVal bv_strINVC_PRTY_CD As String, _
                                         ByVal bv_strINVC_PRTY_NM As String, _
                                         ByVal bv_strCNTCT_PRSN_NM As String, _
                                         ByVal bv_strCNTCT_JB_TTL As String, _
                                         ByVal bv_strCNTCT_ADDRSS As String, _
                                         ByVal bv_strBLLNG_ADDRSS As String, _
                                         ByVal bv_strZP_CD As String, _
                                         ByVal bv_strPHN_NO As String, _
                                         ByVal bv_strFX_NO As String, _
                                         ByVal bv_strRMRKS_VC As String, _
                                         ByVal bv_strBS_CRRNCY_ID As String, _
                                         ByVal bv_strRPRTNG_EML_ID As String, _
                                         ByVal bv_strINVCNG_EML_ID As String, _
                                         ByVal bv_strACTV_BT As String, _
                                         ByVal bv_FinanceIntegrationBit As Boolean, _
                                         ByVal bv_LedgerId As String, _
                                         ByVal bv_strwfData As String)

        Try

            Dim objINVOICE_PARTY As New InvoiceParty
            Dim objcommon As New CommonData
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strwfData, CommonUIData.DPT_ID))
            End If
            Dim strCreatedBy As String = objcommon.GetCurrentUserName()
            Dim datCreatedDate As Date = pub_GetDate()

            Dim lngINVC_PRTY_ID As Long = objINVOICE_PARTY.pub_CreateInvoiceParty(bv_strINVC_PRTY_CD, _
                                                                                  bv_strINVC_PRTY_NM, _
                                                                                  bv_strCNTCT_PRSN_NM, _
                                                                                  bv_strCNTCT_JB_TTL, _
                                                                                  bv_strCNTCT_ADDRSS, _
                                                                                  bv_strBLLNG_ADDRSS, _
                                                                                  bv_strZP_CD, _
                                                                                  bv_strRMRKS_VC, _
                                                                                  bv_strPHN_NO, _
                                                                                  bv_strFX_NO, _
                                                                                  bv_strRPRTNG_EML_ID, _
                                                                                  bv_strINVCNG_EML_ID, _
                                                                                  CommonUIs.iLng(bv_strBS_CRRNCY_ID), _
                                                                                  strCreatedBy, _
                                                                                  CommonUIs.iDat(datCreatedDate), _
                                                                                  CommonUIs.iInt(intDepotID), _
                                                                                  CommonUIs.iBool(bv_strACTV_BT), _
                                                                                  bv_FinanceIntegrationBit, _
                                                                                  bv_LedgerId, _
                                                                                  bv_strwfData)

            pub_SetCallbackReturnValue("Message", String.Concat("Invoicing Party : ", bv_strINVC_PRTY_CD, " ", strMSGINSERT))
            pub_SetCallbackReturnValue("ID", CStr(lngINVC_PRTY_ID))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateInvoiceParty"

    Protected Sub pvt_UpdateInvoiceParty(ByVal bv_strINVC_PRTY_ID As String, _
                                         ByVal bv_strINVC_PRTY_CD As String, _
                                         ByVal bv_strINVC_PRTY_NM As String, _
                                         ByVal bv_strCNTCT_PRSN_NM As String, _
                                         ByVal bv_strCNTCT_JB_TTL As String, _
                                         ByVal bv_strCNTCT_ADDRSS As String, _
                                         ByVal bv_strBLLNG_ADDRSS As String, _
                                         ByVal bv_strZP_CD As String, _
                                         ByVal bv_strPHN_NO As String, _
                                         ByVal bv_strFX_NO As String, _
                                         ByVal bv_strRMRKS_VC As String, _
                                         ByVal bv_strBS_CRRNCY_ID As String, _
                                         ByVal bv_strRPRTNG_EML_ID As String, _
                                         ByVal bv_strINVCNG_EML_ID As String, _
                                         ByVal bv_strACTV_BT As String, _
                                         ByVal bv_FinanceIntegrationBit As Boolean, _
                                         ByVal bv_LedgerId As String, _
                                         ByVal bv_strwfData As String)

        Try

            Dim objINVOICE_PARTY As New InvoiceParty
            Dim objcommon As New CommonData
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strwfData, CommonUIData.DPT_ID))
            End If
            Dim strModifiedBy As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As Date = pub_GetDate()
            objINVOICE_PARTY.UpdateInvoiceParty(CommonUIs.iLng(bv_strINVC_PRTY_ID), _
                                                     bv_strINVC_PRTY_CD, _
                                                     bv_strINVC_PRTY_NM, _
                                                     bv_strCNTCT_PRSN_NM, _
                                                     bv_strCNTCT_JB_TTL, _
                                                     bv_strCNTCT_ADDRSS, _
                                                     bv_strBLLNG_ADDRSS, _
                                                     bv_strZP_CD, _
                                                     bv_strRMRKS_VC, _
                                                     bv_strPHN_NO, _
                                                     bv_strFX_NO, _
                                                     bv_strRPRTNG_EML_ID, _
                                                     bv_strINVCNG_EML_ID, _
                                                     CommonUIs.iLng(bv_strBS_CRRNCY_ID), _
                                                     strModifiedBy, _
                                                     CommonUIs.iDat(datModifiedDate), _
                                                     CommonUIs.iInt(intDepotID), _
                                                     CommonUIs.iBool(bv_strACTV_BT), _
                                                     bv_FinanceIntegrationBit, _
                                                     bv_LedgerId, _
                                                     bv_strwfData)
            pub_SetCallbackReturnValue("Message", String.Concat("Invoicing Party : ", bv_strINVC_PRTY_CD, " ", strMSGUPDATE))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/InvoiceParty.js", MyBase.Page)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
