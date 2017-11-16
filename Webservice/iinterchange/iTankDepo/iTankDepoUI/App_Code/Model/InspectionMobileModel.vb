Imports Microsoft.VisualBasic

Public Class InspectionMobileModel


    Public Property EquipmentNo() As String
    Public Property EquipmentStatus() As String
    Public Property EquipmentStatusType() As String
    Public Property Customer() As String
    Public Property CustomerId() As String
    Public Property InDate() As String
    Public Property PrevoiusCargo() As String
    Public Property LastStatusDate() As String
    Public Property AdditionalCleaningBit() As String
    Public Property CleaningId() As String
    Public Property CleaningReferenceNo() As String
    Public Property Remarks() As String
    Public Property OriginalCleaningDate() As String
    Public Property OriginalInspectionDate() As String
    Public Property Clean_Unclean() As String
    Public Property Seal_No() As String
    Public Property EIR_NO() As String
    'Public Property LatestCleaningDate() As String
    Public Property CleaningRate() As String
    Public Property SlabRate() As String
    Public Property GiTransactionNo() As String

End Class


Public Class ArrayOfInspectionMobileModel

    Public Property ArrayOfInspection() As ArrayList
    Public Property status() As String
    Public Property stauscode() As Integer
    Public Property statusText() As String

End Class
