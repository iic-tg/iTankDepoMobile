Imports Microsoft.VisualBasic

Public Class CleaningMobileModel

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
    'Public Property LatestCleaningDate() As String
    Public Property CleaningRate() As String
    Public Property CleaningMethod() As String
    Public Property SlabRate() As String
    Public Property GiTransactionNo() As String

End Class


Public Class ArrayOfCleaningMobileModel

    Public Property ArrayOfCleaning() As ArrayList
    Public Property status() As String
    Public Property stauscode() As Integer
    Public Property statusText() As String

End Class


Public Class CleaningInstruction

    Public Property InspectionReportNo() As String
    Public Property InDate() As String
    Public Property CustomerName() As String
    Public Property EquipmentNo() As String
    Public Property Type() As String
    'Public Property EIRNo() As String
    Public Property NextTestType() As String
    Public Property NextTestDate() As String
    Public Property IMOClass() As String
    Public Property EIRNo() As String
    Public Property UN() As String
    Public Property CleaningMethod() As String
    Public Property PreviousCargo() As String
    Public Property ProductCode() As String
    

    Public Property Cleaning_Date() As String
    Public Property Steaming_Start_Time() As String
    Public Property Steaming_End_Time() As String
    Public Property Clng_Start_Time() As String
    Public Property Completed_Time() As String
    Public Property Cleaned_By() As String
    Public Property Remarks() As String

    Public Property O2() As String
    Public Property LEL() As String
    Public Property H2S() As String
    Public Property CO() As String
    Public Property PID() As String
    'Public Property EIRNo() As String
    Public Property InspectionDate() As String
    Public Property Clean_UnClean() As String
    Public Property SyphonTube() As String
    Public Property ManlidGasket() As String
    Public Property Foot_Valve_Seat() As String
    Public Property FV_fortyt_O_ring() As String
    Public Property Leak_Test() As String
    Public Property Shell_D_End() As String
    Public Property Top_Discharge_Valve() As String
    Public Property Airline_Valve() As String
    Public Property Relief_Valve_Bursting_Disc() As String
    Public Property Last_Test_Date() As String
    Public Property Other_Remarks_Comments() As String
    Public Property Seal_Nos() As String
    Public Property M_L() As String
    Public Property AV_TD() As String
    Public Property BD() As String
    Public Property Inspected_By() As String

End Class


Public Class UpdateStatus

    Public Property Status() As String
    Public Property StatusCode() As String
    Public Property StatusText() As String
    

End Class
