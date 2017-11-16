
Option Strict On
Partial Class Operations_MiscellaneousInvoice
    Inherits Pagebase

#Region "Declaration"
    Private Const MISCELLANEOUS_INVOICE As String = "MISCELLANEOUS_INVOICE"
    Private Const KEY_ID As String = "ID"
    Private Const KEY_MODE As String = "mode"
    Private strMSGUPDATE As String = "Miscellaneous Invoice Entry : Miscellaneous Invoice(s) Updated Successfully."
    Private pvt_lngID As Long
    Dim bln_009EqType_Key As Boolean
    Dim str_009EqType As String
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"
    Dim dsMiscInvoiceData As New MiscellaneousInvoiceDataSet
    Dim strDepotCurrency As String = String.Empty
#End Region

#Region "Parameters"

    Private pvt_strPageMode As String
    Private pvt_intID As Integer

#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                pvt_SetChangesMade()
                ifgMiscellaneousInvoiceDetail.DeleteButtonText = "Delete Row"
                ifgMiscellaneousInvoiceDetail.RefreshButtonText = "Refresh"
                ifgMiscellaneousInvoiceDetail.AddButtonText = "Add Row"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/MiscellaneousInvoice.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgMiscellaneousInvoiceDetail, "ITab1_0")
    End Sub

#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "UpdateMiscellaneousInvoice"
                    pvt_UpdateMiscellaneousInvoice(CInt(e.GetCallbackValue("ActivityId")))
                Case "fnGetData"
                    pvt_GetData()
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData()
        Try
            Dim objCommon As New CommonData()
            Dim datGetDateTime As DateTime = CDate(objCommon.GetCurrentDate())
            Dim sbGateIn As New StringBuilder
            pub_SetCallbackReturnValue("Message", sbGateIn.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgMiscellaneousInvoiceDetail_ClientBind"
    Protected Sub ifgMiscellaneousInvoiceDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgMiscellaneousInvoiceDetail.ClientBind
        Try
            Dim objcommon As New CommonData()
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())

            Dim objMiscellaneousInvoice As New MiscellaneousInvoice
            dsMiscInvoiceData = objMiscellaneousInvoice.pub_GetMiscellaneousInvoice(intDepotID)

            If dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).Rows.Count = 0 Then
                Dim objCommonUI As New CommonUI()
                Dim dsEqpmntTyp As New DataSet
                Dim objCommonConfig As New ConfigSetting()

                str_009EqType = objCommonConfig.pub_GetConfigSingleValue("009", intDepotID)
                bln_009EqType_Key = objCommonConfig.IsKeyExists
                If bln_009EqType_Key Then
                    If objcommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                        dsEqpmntTyp = objCommonUI.pub_GetEquipmentType(str_009EqType, CInt(objcommon.GetHeadQuarterID()))
                    Else
                        dsEqpmntTyp = objCommonUI.pub_GetEquipmentType(str_009EqType, intDepotID)
                    End If
                    Dim drMiscInvoice As DataRow = dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).NewRow()
                    drMiscInvoice.Item(MiscellaneousInvoiceData.EQPMNT_TYP_CD) = str_009EqType
                    drMiscInvoice.Item(MiscellaneousInvoiceData.EQPMNT_TYP_ID) = dsEqpmntTyp.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                    drMiscInvoice.Item(MiscellaneousInvoiceData.MSCLLNS_INVC_ID) = CommonWeb.GetNextIndex(dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE), MiscellaneousInvoiceData.MSCLLNS_INVC_ID)
                    dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).Rows.Add(drMiscInvoice)
                End If
                ifgMiscellaneousInvoiceDetail.AllowSearch = False
            Else
                strDepotCurrency = CStr(dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).Rows(0).Item(CommonUIData.CRRNCY_CD))
            End If

            e.DataSource = dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE)
            CacheData(MISCELLANEOUS_INVOICE, dsMiscInvoiceData)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateMiscellaneousInvoice"

    Private Sub pvt_UpdateMiscellaneousInvoice(ByVal bv_intActivityId As Integer)
        Try
            dsMiscInvoiceData = CType(RetrieveData(MISCELLANEOUS_INVOICE), MiscellaneousInvoiceDataSet)

            Dim objMiscellaneousInvoice As New MiscellaneousInvoice
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strActivitySubmit As String = String.Empty

            ''22432
            Dim strMisInvoiceID As String = ""
            Dim strErrorMessage As String = ""
            For Each drMiscInvoice As DataRow In dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).Rows
                If drMiscInvoice.RowState = DataRowState.Modified Then
                    If strMisInvoiceID <> String.Empty Then
                        strMisInvoiceID = String.Concat(strMisInvoiceID, ",", drMiscInvoice.Item(MiscellaneousInvoiceData.MSCLLNS_INVC_ID))
                    Else
                        strMisInvoiceID = String.Concat(drMiscInvoice.Item(MiscellaneousInvoiceData.MSCLLNS_INVC_ID))
                    End If
                End If
            Next
            If strMisInvoiceID <> "" Then
                Dim objCommonUI As New CommonUI
                objCommonUI.pub_validateFinalizedInvoiceActivityWise(CommonUIData._MISCELLANEOUS_INVOICE, intDPT_ID, strMisInvoiceID, "", "", strErrorMessage, dsMiscInvoiceData)
                If strErrorMessage <> "" Then
                    pub_SetCallbackError("This action will not be allowed since modified  miscellaneous invoice is already finalized.")
                    pub_SetCallbackStatus(False)
                    Exit Sub
                End If
            End If
            ''
            objMiscellaneousInvoice.pub_UpdateMiscellaneous_Invoice(dsMiscInvoiceData, intDPT_ID, objCommon.GetCurrentUserName(), CDate(objCommon.GetCurrentDate()), strActivitySubmit, bv_intActivityId)
            dsMiscInvoiceData.AcceptChanges()
            CacheData(MISCELLANEOUS_INVOICE, dsMiscInvoiceData)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgMiscellaneousInvoiceDetail_RowDataBound"

    Protected Sub ifgMiscellaneousInvoiceDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgMiscellaneousInvoiceDetail.RowDataBound
        Try
            Dim bln_043Finance_Key As Boolean
            Dim str_043Finance_Value As String
            Dim objCommonConfig As New ConfigSetting()
            Dim objcommon As New CommonData()
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            str_043Finance_Value = objCommonConfig.pub_GetConfigSingleValue("043", intDepotID)
            bln_043Finance_Key = objCommonConfig.IsKeyExists
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim datControl As iDate
                datControl = CType(DirectCast(DirectCast(e.Row.Cells(6), iFgFieldCell).ContainingField, 
                            DateField).iDate, iDate)
                datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                If bln_043Finance_Key Then
                    If str_043Finance_Value.ToLower = "false" Then
                        e.Row.Cells(5).Style.Add("display", "none")
                        Dim lkpCategory As iLookup
                        lkpCategory = CType(DirectCast(DirectCast(e.Row.Cells(5), iFgFieldCell).ContainingField, 
                                    LookupField).Lookup, iLookup)
                        lkpCategory.Validator.IsRequired = False
                        '   e.Row.Cells(5).Visible = False
                    End If
                End If
            ElseIf e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty Then
                    dsMiscInvoiceData = CType(RetrieveData(MISCELLANEOUS_INVOICE), MiscellaneousInvoiceDataSet)
                    If dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).Rows.Count > 0 AndAlso Not IsDBNull(dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).Rows(0).Item(CommonUIData.CRRNCY_CD)) Then
                        strDepotCurrency = CStr(dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    Else
                        Dim objDepot As New Depot
                        Dim dtBankDetail As New DataTable
                        dtBankDetail = objDepot.pub_GetBankDetail(CInt(objcommon.GetDepotID())).Tables(DepotData._V_BANK_DETAIL).Select("BNK_TYP_ID=44").CopyToDataTable
                        strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(DepotData.CRRNCY_CD))
                    End If
                End If
                If bln_043Finance_Key Then
                    If str_043Finance_Value.ToLower = "false" Then
                        Dim lkpCategory As iLookup
                        lkpCategory = CType(DirectCast(DirectCast(e.Row.Cells(5), iFgFieldCell).ContainingField, 
                                    LookupField).Lookup, iLookup)
                        e.Row.Cells(5).Style.Add("display", "none")
                        lkpCategory.Validator.IsRequired = False
                        '  e.Row.Cells(5).Visible = False
                    End If
                End If
                CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strDepotCurrency)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgMiscellaneousInvoiceDetail_RowInserted"

    Protected Sub ifgMiscellaneousInvoiceDetail_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgMiscellaneousInvoiceDetail.RowInserted
        Try
            CacheData(MISCELLANEOUS_INVOICE, dsMiscInvoiceData)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgMiscellaneousInvoiceDetail_RowInserting"

    Protected Sub ifgMiscellaneousInvoiceDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgMiscellaneousInvoiceDetail.RowInserting
        Try
            Dim lngMiscInvcBin As Long
            dsMiscInvoiceData = CType(RetrieveData(MISCELLANEOUS_INVOICE), MiscellaneousInvoiceDataSet)
            lngMiscInvcBin = CommonWeb.GetNextIndex(dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE), MiscellaneousInvoiceData.MSCLLNS_INVC_ID)
            e.Values(MiscellaneousInvoiceData.MSCLLNS_INVC_ID) = lngMiscInvcBin
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgMiscellaneousInvoiceDetail_RowDeleting"

    Protected Sub ifgMiscellaneousInvoiceDetail_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgMiscellaneousInvoiceDetail.RowDeleting
        Try
            ifgMiscellaneousInvoiceDetail.AllowSearch = True
            Dim strEquipmentNo As String = e.InputParamters("NoofEquipment").ToString()

            If strEquipmentNo <> Nothing Then
                e.OutputParamters("Success") = String.Concat("Selected Equipment No / No of Equipment : ", strEquipmentNo, " has been Deleted. Please click submit")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgMiscellaneousInvoiceDetail_RowDeleted"

    Protected Sub ifgMiscellaneousInvoiceDetail_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgMiscellaneousInvoiceDetail.RowDeleted
        Try

            Dim objcommon As New CommonData()
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            dsMiscInvoiceData = CType(RetrieveData(MISCELLANEOUS_INVOICE), MiscellaneousInvoiceDataSet)

            Dim dtMiscInvoice As New DataTable
            dtMiscInvoice = dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).Copy()
            dtMiscInvoice.AcceptChanges()
            If dtMiscInvoice.Rows.Count = 0 Then
                Dim objCommonUI As New CommonUI()
                Dim dsEqpmntTyp As New DataSet
                Dim objCommonConfig As New ConfigSetting()
                str_009EqType = objCommonConfig.pub_GetConfigSingleValue("009", intDepotID)
                bln_009EqType_Key = objCommonConfig.IsKeyExists

                If bln_009EqType_Key Then
                    dsEqpmntTyp = objCommonUI.pub_GetEquipmentType(str_009EqType, intDepotID)
                    Dim drMiscInvoice As DataRow = dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).NewRow()
                    drMiscInvoice.Item(MiscellaneousInvoiceData.EQPMNT_TYP_CD) = str_009EqType
                    drMiscInvoice.Item(MiscellaneousInvoiceData.EQPMNT_TYP_ID) = dsEqpmntTyp.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                    drMiscInvoice.Item(MiscellaneousInvoiceData.MSCLLNS_INVC_ID) = CommonWeb.GetNextIndex(dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE), MiscellaneousInvoiceData.MSCLLNS_INVC_ID)
                    dsMiscInvoiceData.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).Rows.Add(drMiscInvoice)
                End If
                ifgMiscellaneousInvoiceDetail.AllowSearch = False
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub

#End Region

End Class