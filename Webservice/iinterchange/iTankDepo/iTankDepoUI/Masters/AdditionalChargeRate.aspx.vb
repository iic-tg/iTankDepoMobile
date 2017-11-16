
Partial Class Masters_AdditionalChargeRate
    Inherits Pagebase

#Region "Parameters"
    Public dsCharge As New AdditionalChargeRateDataSet
    Public dtCharge As DataTable
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
#End Region

#Region "Declarations"
    Private strMSGUPDATE As String = "Additional Charge Rate(s) Updated Successfully"
    Private Const CHARGE As String = "CHARGE"
#End Region

#Region "Page_PreLoad"
    Protected Sub Page_PreLoad(sender As Object, e As System.EventArgs) Handles Me.PreLoad

    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/AdditionalChargeRate.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "ValidateCharge"
                pvt_ValidateCharge(e.GetCallbackValue("Code"), _
                                    e.GetCallbackValue("Operation"), _
                               e.GetCallbackValue("GridIndex"), _
                               e.GetCallbackValue("RowState"), _
                               e.GetCallbackValue("WFDATA"))
            Case "UpdateAdditionalCharge"
                UpdateAdditionalCharge(e.GetCallbackValue("WFDATA"))
        End Select
    End Sub
#End Region

#Region "pvt_ValidateCharge"
    Private Sub pvt_ValidateCharge(ByVal bv_strCode As String, ByVal bv_strOperation As String, ByVal bv_intGridRowIndex As Integer, ByVal bv_strRowState As String, ByVal bv_strWFDATA As String)
        Try
            Dim blnValid As Boolean
            Dim strExistCode As String = String.Empty
            dsCharge = CType(RetrieveData(CHARGE), AdditionalChargeRateDataSet)
            Dim dtRouteDetail As DataTable = dsCharge.Tables(AdditionalChargeRateData._V_ADDITIONAL_CHARGE_RATE).Copy
            dtRouteDetail.AcceptChanges()

            'Checking whether the entered code is available in grid
            Dim intResultIndex() As System.Data.DataRow = dtRouteDetail.Select(String.Concat("ADDTNL_CHRG_RT_CD", "='", bv_strCode, "'", " AND OPRTN_TYP_CD ='", bv_strOperation, "'"))

            If intResultIndex.Length > 0 Then
                blnValid = True
            Else
                blnValid = False
            End If

            'Checking whether the entered code is available in database
            If blnValid = True AndAlso bv_strCode <> Nothing Then
                If Not bv_strRowState = "Added" Then
                    Dim objAdditionalChargeRate As New AdditionalChargeRate
                    blnValid = objAdditionalChargeRate.pub_ValidateCharge(bv_strCode, bv_strWFDATA)
                End If
            End If
            'Chrome UIG fix
            pub_SetCallbackReturnValue("Error", "")
            If blnValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Charge (", bv_strCode.ToString, ") already exists for the Operation Type (", bv_strOperation, ")"))
                pub_SetCallbackStatus(True)
            End If

            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "UpdateAdditionalCharge"
    Private Sub UpdateAdditionalCharge(ByVal bv_strWFData As String)
        Try
            Dim objCharge As New AdditionalChargeRate
            Dim objCommondata As New CommonData
            Dim objcommon As New CommonData()
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWFData, CommonUIData.DPT_ID))
            End If
            dtCharge = CType(ifgAdditionalCharge.DataSource, DataTable)
            objCharge.pub_UpdateCharge(CType(dtCharge.DataSet, AdditionalChargeRateDataSet), objCommondata.GetCurrentUserName(), CDate(objCommondata.GetCurrentDate()), bv_strWFData, intDepotID)
            dtCharge.AcceptChanges()
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgAdditionalCharge_ClientBind"
    Protected Sub ifgAdditionalCharge_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgAdditionalCharge.ClientBind
        Try
            Dim objcommon As New CommonData()
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(objcommon.GetDepotID())
            End If
            Dim objCharge As New AdditionalChargeRate
            dsCharge = objCharge.pub_GetAdditionlChargesByDepotId(intDepotID)
            e.DataSource = dsCharge.Tables(AdditionalChargeRateData._V_ADDITIONAL_CHARGE_RATE)
            CacheData(CHARGE, dsCharge)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgAdditionalCharge_RowDataBound"
    Protected Sub ifgAdditionalCharge_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgAdditionalCharge.RowDataBound
        Try

            Dim objCommondata As New CommonData
            'Dim intDeporCurrencyID As Integer = CommonWeb.iInt(objCommondata.GetDepotLocalCurrencyID())
            Dim strDeporCurrencyCD As String = (objCommondata.GetDepotLocalCurrencyCode()).ToString
            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Rate", " (", strDeporCurrencyCD, ")")
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As System.Data.DataRowView
                    drv = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If

                If e.Row.RowIndex > 6 Then
                    Dim lkpOperationType As iLookup
                    lkpOperationType = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpOperationType.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgAdditionalCharge_RowDeleting"
    Protected Sub ifgAdditionalCharge_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgAdditionalCharge.RowDeleting
        Try
            dsCharge = CType(RetrieveData(CHARGE), AdditionalChargeRateDataSet)
            Dim dtAdditionalCharge As Data.DataTable = dsCharge.Tables(AdditionalChargeRateData._V_ADDITIONAL_CHARGE_RATE).Copy
            If CType(ifgAdditionalCharge.DataSource, DataTable).Select(String.Concat(AdditionalChargeRateData.ADDTNL_CHRG_RT_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                e.OutputParamters("Delete") = String.Concat("AdditionalChargeRate : Charge ", dtAdditionalCharge.Select(String.Concat(AdditionalChargeRateData.ADDTNL_CHRG_RT_ID, "=", e.Keys(0)))(0).Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_CD).ToString, " cannot be deleted")
                Exit Sub
            Else
                e.OutputParamters("Delete") = String.Concat("AdditionalChargeRate : Charge ", dtAdditionalCharge.Select(String.Concat(AdditionalChargeRateData.ADDTNL_CHRG_RT_ID, "=", e.Keys(0)))(0).Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_CD).ToString, " has been be deleted. Click submit to save changes.")
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
   
#Region "ifgAdditionalCharge_RowInserting"
    Protected Sub ifgAdditionalCharge_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgAdditionalCharge.RowInserting
        Try
            dsCharge = CType(RetrieveData(CHARGE), AdditionalChargeRateDataSet)
            dtCharge = dsCharge.Tables(AdditionalChargeRateData._V_ADDITIONAL_CHARGE_RATE)
            e.Values(AdditionalChargeRateData.ADDTNL_CHRG_RT_ID) = CommonWeb.GetNextIndex(dtCharge, AdditionalChargeRateData.ADDTNL_CHRG_RT_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
   
End Class
