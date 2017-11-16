Imports Microsoft.VisualBasic

Public Class StockReportModel

    Public Property Customer() As List(Of Equipemt_Type)
    Public Property Equipment_Type() As List(Of Equipemt_Type)
    Public Property In_Date_From() As String
    Public Property In_Date_To() As String
    Public Property Cleaning_Date_From() As String
    Public Property Cleaning_Date_To() As String
    Public Property Inspection_Date_From() As String
    Public Property Inspection_Date_To() As String
    Public Property Current_Status_Date_From() As String
    Public Property Current_Status_Date_To() As String
    Public Property Previous_Cargo() As List(Of Equipemt_Type)
    Public Property Current_Status() As List(Of Equipemt_Type)
    Public Property Next_Test_Type() As List(Of Equipemt_Type)

    Public Property Next_Test_Date_From() As String
    Public Property Next_Test_Date_To() As String
    Public Property Equipment_No() As String
    Public Property EIR_No() As String
    Public Property Out_Date_From() As String
    Public Property Out_Date_To() As String
    Public Property Depot() As List(Of Equipemt_Type)
    Public Property UserName() As String
    



End Class


Public Class VDarActivityStatus


    Public Property Depot() As String
    Public Property Customer() As String
    Public Property EquipmentNo() As String
    Public Property Type() As String
    Public Property Indate() As String
    Public Property PreviousCargo() As String
    Public Property EirNo() As String
    Public Property CleaningCertNo() As String
    Public Property CurrentStatusDate() As String
    Public Property CurrentStatus() As String
    Public Property CleaningDate() As String
    Public Property InspectionDate() As String
    Public Property Remarks() As String
    Public Property NextTestDate() As String
    Public Property NextTestType() As String


End Class


Public Class VDarCustomerSummary


    Public Property Customer() As String
    Public Property IND() As String
    Public Property PHL() As String
    Public Property ACN() As String
    Public Property AWECLN() As String
    Public Property AWE() As String
    Public Property AAR() As String
    Public Property AUR() As String
    Public Property ASR() As String
    Public Property SRV() As String
    Public Property AVLCLN() As String
    Public Property AVLINS() As String
    Public Property INSRPC() As String
    Public Property RPC() As String
    Public Property STO() As String
    Public Property AVL() As String
    Public Property OUT() As String
    Public Property TOTAL() As String



End Class


Public Class VDarTypeSummary


    Public Property Type() As String
    Public Property IND() As String
    Public Property PHL() As String
    Public Property ACN() As String
    Public Property AWECLN() As String
    Public Property AWE() As String
    Public Property AAR() As String
    Public Property AUR() As String
    Public Property ASR() As String
    Public Property SRV() As String
    Public Property AVLCLN() As String
    Public Property AVLINS() As String
    Public Property INSRPC() As String
    Public Property RPC() As String
    Public Property STO() As String
    Public Property AVL() As String
    Public Property OUT() As String
    Public Property TOTAL() As String



End Class


Public Class StockReportView

    Public Property status As String
    Public Property ActivityStatus As ArrayList
    Public Property CustomerSummary As VDarCustomerSummary
    Public Property TypeSummary As VDarTypeSummary

End Class


Public Class Equipemt_Type

    Public Property Type() As String
End Class



Public Class Dropdowns

    Public Property Id() As String
    Public Property Description() As String

End Class



Public Class DropDownType

    Public Property Customer() As ArrayList
    Public Property EquipmentType() As ArrayList
    Public Property PrevoiusCargo() As ArrayList
    Public Property CurrentStatus() As ArrayList
    Public Property NextTestType() As ArrayList
    Public Property Depot() As ArrayList


End Class



