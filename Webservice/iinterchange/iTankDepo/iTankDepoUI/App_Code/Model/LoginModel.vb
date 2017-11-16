Imports Microsoft.VisualBasic

Public Class Message


    Public Property ResponseText() As String
    Public Property stauscode() As Integer
    Public Property statusText() As String
    Public Property RL_ID() As String
    Public Property Token() As String
    

    

End Class

Public Class ActivityRights

    Public Property Add() As String
    Public Property Edit() As Integer
    Public Property View() As String


End Class


Public Class ArrayOfActivityRights

    Public Property Cleaning() As ArrayList
    Public Property Heating() As ArrayList
    Public Property GateIn() As ArrayList
    Public Property GateOut() As ArrayList
    Public Property Inspection() As ArrayList
    Public Property EquipmentHistory() As ArrayList
    Public Property StockReport() As ArrayList
    Public Property Repair() As ArrayList


End Class


Public Class Authorization

    Public Property ActivityId() As String
    Public Property AtivityName() As String
    Public Property Add() As String
    Public Property View() As String
    Public Property Edit() As String

End Class



Public Class credentails

    Public Property Username() As String
    Public Property Password() As String

End Class

Public Class Credentailss

    Public Property credentails() As ArrayList
End Class



Public Class RoleDetails

    Public Property RoleDetails() As ArrayList

    Public Property RL_ID() As String
    Public Property GateinCount() As Integer
    Public Property GateoutCount() As Integer
    Public Property CleaningCount() As Integer

    Public Property RepairEstimateCount() As Integer
    Public Property RepairApprovalCount() As Integer
    Public Property RepairCompletionCount() As Integer
    Public Property SurveyCompletionCount() As Integer
    Public Property RepairCount() As Integer

    Public Property InspectionCount() As Integer
    Public Property HeatingCount() As Integer
    Public Property LeakTestCount() As Integer
    Public Property Status() As String

End Class


'Public Class Testigbb

'    Public Property CSTMR_ID() As Integer
'    Public Property CSTMR_CD() As String
'    Public Property CSTMR_NAM() As String
'    Public Property CHK_DGT_VLDTN_BT() As Boolean


'End Class

'Public Class TestigbbList

'    Public Property ListOfTest() As IEnumerable(Of Testigbb)


'End Class
