
Partial Class EDI_EdiSettings
    Inherits Pagebase

#Region "Declaration"
    Dim dsEDI As New EDIDataSet
    Private strMSGUPDATE As String = "EDI Settings Details Updated Successfully."
    Private Const EDISettings As String = "EDISettings"
#End Region


#Region "Page_Load1"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                'hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
                pvt_SetChangesMade()
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
            CommonWeb.IncludeScript("Script/EDI/EdiSettings.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region


#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgEdiSettings, "ITab1_0")
    End Sub

#End Region
#Region "ifgEdiSettings_ClientBind"
    Protected Sub ifgEdiSettings_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEdiSettings.ClientBind
        Try
            Dim ObjLeakTest As New LeakTest()
            Dim objCommon As New CommonData
            Dim intDepotID As Integer = objCommon.GetDepotID()
            hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper()
            ifgEdiSettings.AllowStaticHeader = True
            '  ifgEdiSettings.StaticHeaderHeight = "120"
            'ifgEdiSettings.Width = 930
            Dim objEdiSettings As New EDI
            dsEDI = objEdiSettings.pub_GetEDISettings_Details(intDepotID)
            e.DataSource = dsEDI.Tables(EDIData._V_EDI_SETTINGS)
            CacheData(EDISettings, dsEDI)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
             Select e.CallbackType
                Case "UpdateEdiSettings"
                    pvt_CreateEdiSettings()
                Case "ValidateCustomer"
                    pvt_ValidateCustomer(e.GetCallbackValue("CustomerID"), e.GetCallbackValue("Customer"))
            End Select

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateCustomer"
    Private Sub pvt_ValidateCustomer(ByVal bv_lngCustomer As Int64, ByVal bv_strCustomer As String)
        Try
            Dim objEdiSettings As New EDI
            Dim objCommon As New CommonData
            Dim intDepotID As Int32 = CInt(objCommon.GetDepotID())
            Dim blnValid As Boolean
            Dim strServiceType As String = String.Empty
            blnValid = objEdiSettings.pub_ValidateEDI_Settings(bv_lngCustomer, bv_strCustomer, intDepotID)
            If blnValid Then
                pub_SetCallbackReturnValue("valid", "true")
            Else
                pub_SetCallbackReturnValue("valid", "This Customer is Exist for EDI File Genaration.")
            End If

            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region


#Region "pvt_CreateEdiSettings"
    Private Sub pvt_CreateEdiSettings()
        Try
            Dim dtEdiSettings As DataTable
            dsEDI = CType(RetrieveData(EDISettings), EDIDataSet)
            '  pvt_HasChangeEquipmentInformation(dsEDI)
            Dim drEdiSettings As DataRow()
            Dim objCommon As New CommonData
            drEdiSettings = dsEDI.Tables(EDIData._V_EDI_SETTINGS).Select(EDIData.ACTV_BT & "='True'")
            dtEdiSettings = dsEDI.Tables(EDIData._V_EDI_SETTINGS).Clone()
            Dim objEdiSettings As New EDI
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            objEdiSettings.pub_UpdateEDISettingsDetails(dsEDI, intDPT_ID, objCommon.GetCurrentUserName())
            dsEDI.AcceptChanges()

            CacheData(EDISettings, dsEDI)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEdiSettings_RowInserting"

    Protected Sub ifgEdiSettings_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgEdiSettings.RowInserted
        Try
            CacheData(EDISettings, dsEDI)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
    Protected Sub ifgEdiSettings_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgEdiSettings.RowInserting
        Try
            Dim lngGateInbin As Long
            dsEDI = CType(RetrieveData(EDISettings), EDIDataSet)
            lngGateInbin = CommonWeb.GetNextIndex(dsEDI.Tables(EDIData._V_EDI_SETTINGS), EDIData.EDI_STTNGS_ID)
            e.Values(EDIData.EDI_STTNGS_ID) = lngGateInbin

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

    
    Protected Sub ifgEdiSettings_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgEdiSettings.RowUpdating
        Try
            RetrieveData(EDISettings)
        Catch ex As Exception

        End Try
        ' CacheData(EDISettings, dsEDI)
    End Sub
End Class
