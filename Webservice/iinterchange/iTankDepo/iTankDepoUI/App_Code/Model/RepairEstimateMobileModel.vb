Imports Microsoft.VisualBasic

Public Class RepairEstimateMobileModel


    Public Property EquipmentNo() As String
    Public Property Customer() As String
    Public Property CSTMR_ID() As String
    Public Property InDate() As String
    Public Property PreviousCargo() As String
    Public Property LastStatusDate() As String
    Public Property LaborRate() As String
    'Public Property CeartificateOfCleanliness() As String
    Public Property LastTestType() As String
    Public Property NextTestType() As String
    Public Property LastTestDate() As String
    Public Property NextTestDate() As String
    Public Property LastSurveyor() As String


    Public Property ValidityPeriodforTest() As String
    Public Property RepairTypeID() As String
    Public Property RepairTypeCD() As String

    Public Property Remarks() As String
    Public Property InvoicingPartyCD() As String
    Public Property InvoicingPartyID() As String
    Public Property InvoicingPartyName() As String
    Public Property GiTransactionNo() As String


    Public Property RepairEstimateID() As String
    Public Property RevisionNo() As String
    Public Property CustomerAppRef() As String  'OWNR_APPRVL_RF
    Public Property ApprovalDate() As String  'ACTVTY_DT
    Public Property PartyAppRef() As String  'PRTY_APPRVL_RF
    Public Property SurveyorName() As String  'SRVYR_NM
    Public Property SurveyCompletionDate() As String 'ACTVTY_DT

    Public Property LineItems() As ArrayList
    Public Property attchement() As ArrayList




    Public Property RepairEstimateNo() As String

    'Public Property RepairEstimateNo() As String

    Public Property EquipmentStatusId() As String
    Public Property EquipmentStatusCd() As String

    Public Property CurencyCD() As String

    Public Property ActivityDate() As String


    Public Property EquipmentType_Id() As String
    Public Property EquipmentType_Cd() As String

    


End Class


Public Class ArrayOfRepairEstimateMobileModel

    Public Property Status() As String
    Public Property Response() As ArrayList

End Class

Public Class EstDetSummary
    Public Property Message As String
    Public Property Currency As String
    Public Property ExchangeRate As String
End Class



Public Class LineItem

    Public Property RPR_ESTMT_DTL_ID() As String
    Public Property RPR_ESTMT_ID() As String
    Public Property Item() As String
    Public Property ItemCd() As String
    Public Property SubItem() As String
    Public Property SubItemCd() As String
    Public Property Damage() As String
    Public Property DamageCd() As String
    Public Property Repair() As String
    Public Property RepairCd() As String
    Public Property ManHour() As String
    Public Property DmgRprRemarks() As String
    Public Property MHR() As String
    Public Property MHC() As String
    Public Property MC() As String
    Public Property TC() As String
    Public Property Responsibility() As String
    Public Property ResponsibilityCd() As String

    Public Property DamageDescription() As String
    Public Property RepairDescription() As String
    Public Property CheckButton() As String
    Public Property ChangingBit() As String

    Public Property CURRENCY_CD() As String
    Public Property RowState() As String



End Class



Public Class SummaryDetail

    Public Property MH() As String
    Public Property MHC() As String
    Public Property MC() As String
    Public Property TC() As String
    Public Property MHR() As String
    Public Property ResponsibiltyID() As String
    Public Property MHCSTSummary() As String
    Public Property ResponsibiltyCD() As String

End Class


Public Class ArrayOfRepairEstimate


    Public Property RepairList() As ArrayList

End Class


Public Class ArrayOfLineItems


    Public Property LineItemList() As List(Of LineItem)

End Class


Public Class ArrayOfSummaryDetail


    Public Property LineSummaryDetail() As List(Of SummaryDetail)

End Class


Public Class RepairEstimateDataSetUpdate

    Public Property RepairUpdate() As String
    'Public Property stausCode() As Integer
    'Public Property statusText() As String

End Class




