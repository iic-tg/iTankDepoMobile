Option Strict On
Partial Class Operations_CleaningInspection
    Inherits Pagebase

#Region "Declarations"
    Dim dsCleaningInstruction As New CleaningInspectionDataSet
    Private Const CLEANING_INSTRUCTION As String = "CLEANING_INSTRUCTION"
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "PreRender"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/CleaningInspection.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GenerateInstructionNo"
                    pvt_CleaningInstruction()
                Case "CIlockData"
                    pvt_CIlockData(e.GetCallbackValue("CheckBit"), _
                                  e.GetCallbackValue("EquipmentNo"), _
                                  e.GetCallbackValue("LockBit"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CleaningInstruction"
    Private Sub pvt_CleaningInstruction()
        Try
            Dim objcommon As New CommonData
            Dim objCleaningInspection As New CleaningInspection
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim dtCleaningInspection As New DataTable
            dtCleaningInspection = dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Clone()
            dsCleaningInstruction = CType(RetrieveData(CLEANING_INSTRUCTION), CleaningInspectionDataSet)
            Dim strCleaningInspectionNo As String = String.Empty
            If dsCleaningInstruction.Tables(CleaningInspectionData._V_CLEANING_INSTRUCTION).Select("CHECKED=True").Length > 0 Then

                ''Lock Implementation - Unlock after submit
                ''Lock Implementation 
                For Each dr As DataRow In dsCleaningInstruction.Tables(CleaningInspectionData._V_CLEANING_INSTRUCTION).Select(String.Concat(RepairCompletionData.CHECKED, "= 'True'"))
                    pvt_CIlockData("FALSE", dr.Item(RepairCompletionData.EQPMNT_NO).ToString, "FALSE")
                Next


                objCleaningInspection.UpdateCleaningInstructionNo(strCleaningInspectionNo,
                                                                  strModifiedby, _
                                                                  CDate(datModifiedDate), _
                                                                  intDepotID, _
                                                                  dsCleaningInstruction)

                dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Rows.Clear()
                For Each drInspection As DataRow In dsCleaningInstruction.Tables(CleaningInspectionData._V_CLEANING_INSTRUCTION).Select("CHECKED=True")
                    Dim drNewRow As DataRow = dtCleaningInspection.NewRow
                    For Each drColumn As DataColumn In dsCleaningInstruction.Tables(CleaningInspectionData._V_CLEANING_INSTRUCTION).Columns
                        If dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Columns.Contains(drColumn.ColumnName) Then
                            drNewRow.Item(drColumn.ColumnName) = drInspection.Item(drColumn.ColumnName)
                        End If
                    Next
                    drNewRow.Item(CleaningInspectionData.CLNNG_INSPCTN_NO) = strCleaningInspectionNo
                    dtCleaningInspection.Rows.Add(drNewRow)
                Next
                'CR- 003 [CLEANING INSTRUCTION RAISED THEN IND_TO_ACN]
                dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Merge(dtCleaningInspection)
                CacheData(CLEANING_INSTRUCTION, dsCleaningInstruction)
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackReturnValue("SELECT", "True")
                pub_SetCallbackStatus(False)
            End If
            CacheData(CLEANING_INSTRUCTION, dsCleaningInstruction)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCleaningInstruction_ClientBind"
    Protected Sub ifgCleaningInstruction_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgCleaningInstruction.ClientBind
        Try
            Dim objCleaningInstruction As New CleaningInspection
            Dim objCommon As New CommonData
            Dim dtCleaningInstruction As New DataTable
            'CR- 003 [CLEANING INSTRUCTION RAISED THEN IND_TO_ACN]
            dsCleaningInstruction = CType(RetrieveData(CLEANING_INSTRUCTION), CleaningInspectionDataSet)
            If dsCleaningInstruction Is Nothing Then
                dsCleaningInstruction = New CleaningInspectionDataSet
            Else
                dsCleaningInstruction.Tables(CleaningInspectionData._V_CLEANING_INSTRUCTION).Rows.Clear()
            End If
            dtCleaningInstruction = dsCleaningInstruction.Tables(CleaningInspectionData._V_CLEANING_INSTRUCTION).Clone()
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            dtCleaningInstruction = objCleaningInstruction.pub_GetCleaningInstruction(intDepotID).Tables(CleaningInspectionData._V_CLEANING_INSTRUCTION)
            dsCleaningInstruction.Tables(CleaningInspectionData._V_CLEANING_INSTRUCTION).Merge(dtCleaningInstruction)
            e.DataSource = dsCleaningInstruction.Tables(CleaningInspectionData._V_CLEANING_INSTRUCTION)
            CacheData(CLEANING_INSTRUCTION, dsCleaningInstruction)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCleaningInstruction_RowDataBound"
    Protected Sub ifgCleaningInstruction_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgCleaningInstruction.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drvCI As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim strEquipmentNo As String = drvCI.Item(GateOutData.EQPMNT_NO).ToString
                Dim chkActive As iFgCheckBox
                chkActive = CType(e.Row.Cells(7).Controls(0), iFgCheckBox)
                chkActive.Attributes.Add("onClick", String.Concat("CIlockData(this,'", strEquipmentNo, "');"))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgCleaningInstruction, "ITab1_0")
    End Sub
#End Region

#Region "CleaningInstructionLocking"
    Private Sub pvt_CIlockData(ByVal bv_strCheckBitFlag As String, _
                                ByVal bv_strEquipmentNo As String, _
                                ByRef LockBit As String)
        Try
            Dim objCommonData As New CommonData
            Dim objCommonUI As New CommonUI
            Dim strEquipmentStatus As String = ""
            Dim strCurrentEquipmentStatus As String = ""
            Dim strCurrentSessionId As String = String.Empty
            Dim strCurrentUserName As String = String.Empty
            Dim strCurrentIpAddress As String = String.Empty
            Dim strSessionId As String = String.Empty
            Dim strUserName As String = String.Empty
            Dim strIpAddress As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim intDepotID As Integer = CInt(objCommonData.GetDepotID())
            Dim strErrorMessage As String = ""
            strCurrentSessionId = objCommonData.GetSessionID()
            strCurrentUserName = objCommonData.GetCurrentUserName()
            strCurrentIpAddress = GetClientIPAddress()
            Dim strActivity As String = ""
            strEquipmentStatus = "IND"
            LockBit = "FALSE"
            If bv_strCheckBitFlag.ToUpper = "TRUE" Then
                strCurrentEquipmentStatus = objCommonUI.pub_GetEquipmentStaus(bv_strEquipmentNo, intDepotID)
                blnLockData = objCommonData.StoreLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentUserName, strCurrentSessionId, "Cleaning Instruction", strCurrentIpAddress, True)
                If strCurrentEquipmentStatus <> strEquipmentStatus Then
                    strErrorMessage = "This Equipment is  already awaiting for cleaning certificate."
                Else
                    If blnLockData Then
                        LockBit = "TRUE"
                        ''Get Activity Name
                        Dim blnGetLock As Boolean = objCommonData.GetLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, strCurrentUserName, strActivity)
                        strErrorMessage = String.Concat("This record (", bv_strEquipmentNo, ") cannot be modified because it is already being used by <b>", strCurrentUserName, "</b> user ")
                        strSessionId = objCommonData.GetSessionID()
                        strUserName = objCommonData.GetCurrentUserName()
                        strIpAddress = GetClientIPAddress()
                        If strCurrentIpAddress <> strIpAddress Then
                            strErrorMessage = String.Concat(strErrorMessage, " in another place. ")
                        Else
                            strErrorMessage = String.Concat(strErrorMessage, " in another session. ")
                        End If

                    End If
                End If
            Else
                objCommonData.FlushLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, "Cleaning Instruction")
            End If
            pub_SetCallbackReturnValue("ErrorMessage", strErrorMessage)
            pub_SetCallbackReturnValue("Activity", strActivity)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

End Class