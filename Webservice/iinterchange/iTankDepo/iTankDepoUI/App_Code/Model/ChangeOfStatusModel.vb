Imports Microsoft.VisualBasic

Public Class ChangeOfStatusModel


    Public Property EquipmentNo() As String
    Public Property Type() As String
    Public Property Customer() As String
    Public Property InDate() As String
    Public Property PreviousCargo() As String
    Public Property CurrentStatus() As String
    Public Property CurrentStatusDate() As String
    Public Property YardLocation() As String
    Public Property Remarks() As String
    'Public Property YardLocation() As String



End Class


Public Class SearchResult

    Public Property SearchResult() As ArrayList
    Public Property Status() As String

End Class


Public Class EquipmentListCOS

    Public Property EquipmentNo() As String
    Public Property Remarks() As String
    Public Property YardLocation() As String
    Public Property Checked() As String
    Public Property NEW_EQPMNT_STTS_CD() As String
    Public Property NEW_ACTVTY_DT() As String
    Public Property EQPMNT_STTS_ID() As String

End Class


Public Class ArrayOfEquipmentListCOS

    Public Property ArrayOfEquipmentListCOS() As List(Of EquipmentListCOS)
    


End Class


Public Class CurrentStatusList

    Public Property StatusName() As String
    Public Property StatusID() As String



End Class


Public Class TOStatusList

    Public Property StatusName() As String
    Public Property StatusID() As String




End Class

