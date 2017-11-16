Option Strict On
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.ServiceModel
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class CodeMaster
    Private objCodemaster As CodeBase
    Private pvt_dsCodeMaster As DataSet
    Private pvt_Tablename As String
    <OperationContract()> _
    Public Sub _New(ByVal bv_Tablename As String)
        Try
            pvt_Tablename = bv_Tablename
            Select Case bv_Tablename.ToUpper
      
                Case CountryData._COUNTRY.ToUpper
                    objCodemaster = New Country
                    pvt_dsCodeMaster = New CountryDataSet
                Case CurrencyData._CURRENCY.ToUpper
                    objCodemaster = New Currency
                    pvt_dsCodeMaster = New CurrencyDataSet
                Case DamageData._DAMAGE.ToUpper
                    objCodemaster = New Damage
                    pvt_dsCodeMaster = New DamageDataSet
                Case EquipmentCodeData._EQUIPMENT_CODE.ToUpper
                    objCodemaster = New EquipmentCode
                    pvt_dsCodeMaster = New EquipmentCodeDataSet
                Case EquipmentSizeData._EQUIPMENT_SIZE.ToUpper
                    objCodemaster = New EquipmentSize
                    pvt_dsCodeMaster = New EquipmentSizeDataSet
                Case EquipmentStatusData._EQUIPMENT_STATUS.ToUpper
                    objCodemaster = New EquipmentStatus
                    pvt_dsCodeMaster = New EquipmentStatusDataSet
                Case EquipmentTypeData._EQUIPMENT_TYPE.ToUpper
                    objCodemaster = New EquipmentType
                    pvt_dsCodeMaster = New EquipmentTypeDataSet
                Case LocationData._LOCATION.ToUpper
                    objCodemaster = New Location
                    pvt_dsCodeMaster = New LocationDataSet
                Case MaterialData._MATERIAL.ToUpper
                    objCodemaster = New Material
                    pvt_dsCodeMaster = New MaterialDataSet
                Case MeasureData._MEASURE.ToUpper
                    objCodemaster = New Measure
                    pvt_dsCodeMaster = New MeasureDataSet
                Case RepairData._REPAIR.ToUpper
                    objCodemaster = New Repair
                    pvt_dsCodeMaster = New RepairDataSet
                Case ResponsibilityData._RESPONSIBILITY.ToUpper
                    objCodemaster = New Responsibility
                    pvt_dsCodeMaster = New ResponsibilityDataSet
                Case UnitData._UNIT.ToUpper
                    objCodemaster = New Unit
                    pvt_dsCodeMaster = New UnitDataSet
                Case ItemData._ITEM.ToUpper
                    objCodemaster = New Item
                    pvt_dsCodeMaster = New ItemDataSet
                Case PortData._Port.ToUpper
                    objCodemaster = New Port
                    pvt_dsCodeMaster = New PortDataSet
                Case PartyData._Party.ToUpper
                    objCodemaster = New Party
                    pvt_dsCodeMaster = New PartyDataSet
                Case GradeData._Grade.ToUpper
                    objCodemaster = New Grade
                    pvt_dsCodeMaster = New GradeDataSet
                Case StatusData._Status.ToUpper
                    objCodemaster = New Status
                    pvt_dsCodeMaster = New StatusDataSet
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Dim dsTypeMasterData As New DataSet

    <OperationContract()> _
    Public Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As DataSet
        Try
            Dim dtCodeMasterData As New DataTable

            pvt_dsCodeMaster = objCodemaster.pub_GetCodeMaster(bv_strWFDATA)

            dtCodeMasterData = pvt_dsCodeMaster.Tables(pvt_Tablename)
            dtCodeMasterData.Columns(0).Caption = dtCodeMasterData.Columns(0).Caption
            dtCodeMasterData.Columns(0).ColumnName = "ID"
            dtCodeMasterData.Columns(1).Caption = dtCodeMasterData.Columns(1).Caption
            dtCodeMasterData.Columns(1).ColumnName = "Code"
            dtCodeMasterData.Columns(2).Caption = dtCodeMasterData.Columns(2).Caption
            dtCodeMasterData.Columns(2).ColumnName = "Description"
            dtCodeMasterData.Columns(3).Caption = dtCodeMasterData.Columns(3).Caption
            dtCodeMasterData.Columns(3).ColumnName = "CreatedBy"
            dtCodeMasterData.Columns(4).Caption = dtCodeMasterData.Columns(4).Caption
            dtCodeMasterData.Columns(4).ColumnName = "CreatedDate"
            dtCodeMasterData.Columns(5).Caption = dtCodeMasterData.Columns(5).Caption
            dtCodeMasterData.Columns(5).ColumnName = "ModifiedBy"
            dtCodeMasterData.Columns(6).Caption = dtCodeMasterData.Columns(6).Caption
            dtCodeMasterData.Columns(6).ColumnName = "ModifiedDate"
            dtCodeMasterData.Columns(7).Caption = dtCodeMasterData.Columns(7).Caption
            dtCodeMasterData.Columns(7).ColumnName = "Active"
            dtCodeMasterData.Columns(8).Caption = dtCodeMasterData.Columns(8).Caption
            dtCodeMasterData.Columns(8).ColumnName = "DepotID"

            pvt_dsCodeMaster.Tables.Remove(pvt_Tablename)


            If Not dsTypeMasterData.Tables.Contains(pvt_Tablename) Then
                dsTypeMasterData.Tables.Add(dtCodeMasterData)
            End If
            Return dsTypeMasterData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    Dim dsGetMaster As New DataSet

    <OperationContract()> _
    Public Function pub_GetCodeMasterSchema() As DataSet
        Try
            Dim dtMasterData As DataTable
            dtMasterData = pvt_dsCodeMaster.Tables(pvt_Tablename)
            pvt_dsCodeMaster.Tables.Remove(pvt_Tablename)
            If Not dsGetMaster.Tables.Contains(pvt_Tablename) Then
                dsGetMaster.Tables.Add(dtMasterData)
            End If
            Return dsGetMaster
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_ValidatePK(ByVal bv_strCode As String, ByVal bv_strWFDATA As String) As Boolean
        Try
            Return objCodemaster.pub_ValidatePK(bv_strCode, bv_strWFDATA)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_UpdateCodeMaster(ByRef br_dsCodeMaster As DataSet, ByVal bv_strModifiedby As String, ByVal bv_datModifiedDate As DateTime, ByVal bv_strWFDATA As String) As Boolean
        Try
            Dim dtCodeMaster As DataTable
            dtCodeMaster = br_dsCodeMaster.Tables(pvt_Tablename).GetChanges(DataRowState.Added)

            If Not dtCodeMaster Is Nothing Then
                For Each drCodeMaster As DataRow In dtCodeMaster.Rows
                    objCodemaster.pub_CreateCodeMaster(CStr(drCodeMaster.Item("Code")), CStr(drCodeMaster.Item("Description")), CBool(drCodeMaster.Item("Active")), bv_strModifiedby, bv_datModifiedDate, bv_strModifiedby, bv_datModifiedDate, bv_strWFDATA)
                Next
            End If
            dtCodeMaster = br_dsCodeMaster.Tables(pvt_Tablename).GetChanges(DataRowState.Modified)
            If Not dtCodeMaster Is Nothing Then
                For Each drCodeMaster As DataRow In dtCodeMaster.Rows
                    objCodemaster.pub_ModifyCodeMaster(CInt(drCodeMaster.Item("ID")), CStr(drCodeMaster.Item("Code")), CStr(drCodeMaster.Item("Description")), CBool(drCodeMaster.Item("Active")), bv_strModifiedby, bv_datModifiedDate, bv_strWFDATA)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_GetData(ByVal bv_Tablename As String) As CodeMastersObjectData
        Try
            Dim objTypeData As New CodeMastersObjectData(bv_Tablename)
            Return objTypeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


    <DataContract()> _
    Public Class CodeMastersObjectData
        <DataMember()> _
        Public pvt_IDField As String
        <DataMember()> _
        Public pvt_CodeField As String
        <DataMember()> _
        Public pvt_DescriptionField As String
        <DataMember()> _
        Public pvt_ActiveField As String
        <DataMember()> _
        Public pvt_Created_BY_Field As String
        <DataMember()> _
        Public pvt_Created_Date_Field As DateTime
        <DataMember()> _
        Public pvt_Modified_BY_Field As String
        <DataMember()> _
        Public pvt_Modified_Date_Field As DateTime
        <DataMember()> _
        Public pvt_DepotID As Long

        Sub New(ByVal bv_Tablename As String)
            Select Case bv_Tablename.ToUpper

                Case CountryData._COUNTRY.ToUpper
                    pvt_IDField = CountryData.CNTRY_ID
                    pvt_CodeField = CountryData.CNTRY_CD
                    pvt_DescriptionField = CountryData.CNTRY_DSCRPTN_VC
                    pvt_ActiveField = CountryData.ACTV_BT
                    pvt_Created_BY_Field = CountryData.CRTD_BY
                    pvt_Created_Date_Field = CDate(CountryData.CRTD_DT)
                    pvt_Modified_BY_Field = CountryData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(CountryData.MDFD_DT)
                    pvt_DepotID = CLng(CountryData.DPT_ID)
                Case CurrencyData._CURRENCY.ToUpper
                    pvt_IDField = CurrencyData.CRRNCY_ID
                    pvt_CodeField = CurrencyData.CRRNCY_CD
                    pvt_DescriptionField = CurrencyData.CRRNCY_DSCRPTN_VC
                    pvt_ActiveField = CurrencyData.ACTV_BT
                    pvt_Created_BY_Field = CurrencyData.CRTD_BY
                    pvt_Created_Date_Field = CDate(CurrencyData.CRTD_DT)
                    pvt_Modified_BY_Field = CurrencyData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(CurrencyData.MDFD_DT)
                    pvt_DepotID = CLng(CurrencyData.DPT_ID)
                Case DamageData._DAMAGE.ToUpper
                    pvt_IDField = DamageData.DMG_ID
                    pvt_CodeField = DamageData.DMG_CD
                    pvt_DescriptionField = DamageData.DMG_DSCRPTN_VC
                    pvt_ActiveField = DamageData.ACTV_BT
                    pvt_Created_BY_Field = DamageData.CRTD_BY
                    pvt_Created_Date_Field = CDate(DamageData.CRTD_DT)
                    pvt_Modified_BY_Field = DamageData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(DamageData.MDFD_DT)
                    pvt_DepotID = CLng(DamageData.DPT_ID)
                Case EquipmentCodeData._EQUIPMENT_CODE.ToUpper
                    pvt_IDField = EquipmentCodeData.EQPMNT_CD_ID
                    pvt_CodeField = EquipmentCodeData.EQPMNT_CD_CD
                    pvt_DescriptionField = EquipmentCodeData.EQPMNT_CD_DSCRPTN_VC
                    pvt_ActiveField = EquipmentCodeData.ACTV_BT
                    pvt_Created_BY_Field = EquipmentCodeData.CRTD_BY
                    pvt_Created_Date_Field = CDate(EquipmentCodeData.CRTD_DT)
                    pvt_Modified_BY_Field = EquipmentCodeData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(EquipmentCodeData.MDFD_DT)
                    pvt_DepotID = CLng(EquipmentCodeData.DPT_ID)
                Case EquipmentSizeData._EQUIPMENT_SIZE.ToUpper
                    pvt_IDField = EquipmentSizeData.EQPMNT_SZ_ID
                    pvt_CodeField = EquipmentSizeData.EQPMNT_SZ_CD
                    pvt_DescriptionField = EquipmentSizeData.EQPMNT_SZ_DSCRPTN_VC
                    pvt_ActiveField = EquipmentSizeData.ACTV_BT
                    pvt_Created_BY_Field = EquipmentSizeData.CRTD_BY
                    pvt_Created_Date_Field = CDate(EquipmentSizeData.CRTD_DT)
                    pvt_Modified_BY_Field = EquipmentSizeData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(EquipmentSizeData.MDFD_DT)
                    pvt_DepotID = CLng(EquipmentSizeData.DPT_ID)
                Case EquipmentStatusData._EQUIPMENT_STATUS.ToUpper
                    pvt_IDField = EquipmentStatusData.EQPMNT_STTS_ID
                    pvt_CodeField = EquipmentStatusData.EQPMNT_STTS_CD
                    pvt_DescriptionField = EquipmentStatusData.EQPMNT_STTS_DSCRPTN_VC
                    pvt_ActiveField = EquipmentStatusData.ACTV_BT
                    pvt_Created_BY_Field = EquipmentStatusData.CRTD_BY
                    pvt_Created_Date_Field = CDate(EquipmentStatusData.CRTD_DT)
                    pvt_Modified_BY_Field = EquipmentStatusData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(EquipmentStatusData.MDFD_DT)
                    pvt_DepotID = CLng(EquipmentStatusData.DPT_ID)
                Case EquipmentTypeData._EQUIPMENT_TYPE.ToUpper
                    pvt_IDField = EquipmentTypeData.EQPMNT_TYP_ID
                    pvt_CodeField = EquipmentTypeData.EQPMNT_TYP_CD
                    pvt_DescriptionField = EquipmentTypeData.EQPMNT_TYP_DSCRPTN_VC
                    pvt_ActiveField = EquipmentTypeData.ACTV_BT
                    pvt_Created_BY_Field = EquipmentTypeData.CRTD_BY
                    pvt_Created_Date_Field = CDate(EquipmentTypeData.CRTD_DT)
                    pvt_Modified_BY_Field = EquipmentTypeData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(EquipmentTypeData.MDFD_DT)
                    pvt_DepotID = CLng(EquipmentTypeData.DPT_ID)
                Case LocationData._LOCATION.ToUpper
                    pvt_IDField = LocationData.LCTN_ID
                    pvt_CodeField = LocationData.LCTN_CD
                    pvt_DescriptionField = LocationData.LCTN_DSCRPTN_VC
                    pvt_ActiveField = LocationData.ACTV_BT
                    pvt_Created_BY_Field = LocationData.CRTD_BY
                    pvt_Created_Date_Field = CDate(LocationData.CRTD_DT)
                    pvt_Modified_BY_Field = LocationData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(LocationData.MDFD_DT)
                    pvt_DepotID = CLng(LocationData.DPT_ID)
                Case MaterialData._MATERIAL.ToUpper
                    pvt_IDField = MaterialData.MTRL_ID
                    pvt_CodeField = MaterialData.MTRL_CD
                    pvt_DescriptionField = MaterialData.MTRL_DSCRPTN_VC
                    pvt_ActiveField = MaterialData.ACTV_BT
                    pvt_Created_BY_Field = MaterialData.CRTD_BY
                    pvt_Created_Date_Field = CDate(MaterialData.CRTD_DT)
                    pvt_Modified_BY_Field = MaterialData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(MaterialData.MDFD_DT)
                    pvt_DepotID = CLng(MaterialData.DPT_ID)
                Case MeasureData._MEASURE.ToUpper
                    pvt_IDField = MeasureData.MSR_ID
                    pvt_CodeField = MeasureData.MSR_CD
                    pvt_DescriptionField = MeasureData.MSR_DSCRPTN_VC
                    pvt_ActiveField = MeasureData.ACTV_BT
                    pvt_Created_BY_Field = MeasureData.CRTD_BY
                    pvt_Created_Date_Field = CDate(MeasureData.CRTD_DT)
                    pvt_Modified_BY_Field = MeasureData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(MeasureData.MDFD_DT)
                    pvt_DepotID = CLng(MeasureData.DPT_ID)
                Case RepairData._REPAIR.ToUpper
                    pvt_IDField = RepairData.RPR_ID
                    pvt_CodeField = RepairData.RPR_CD
                    pvt_DescriptionField = RepairData.RPR_DSCRPTN_VC
                    pvt_ActiveField = RepairData.ACTV_BT
                    pvt_Created_BY_Field = RepairData.CRTD_BY
                    pvt_Created_Date_Field = CDate(RepairData.CRTD_DT)
                    pvt_Modified_BY_Field = RepairData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(RepairData.MDFD_DT)
                    pvt_DepotID = CLng(RepairData.DPT_ID)
                Case ResponsibilityData._RESPONSIBILITY.ToUpper
                    pvt_IDField = ResponsibilityData.RSPNSBLTY_ID
                    pvt_CodeField = ResponsibilityData.RSPNSBLTY_CD
                    pvt_DescriptionField = ResponsibilityData.RSPNSBLTY_DSCRPTN_VC
                    pvt_ActiveField = ResponsibilityData.ACTV_BT
                    pvt_Created_BY_Field = ResponsibilityData.CRTD_BY
                    pvt_Created_Date_Field = CDate(ResponsibilityData.CRTD_DT)
                    pvt_Modified_BY_Field = ResponsibilityData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(ResponsibilityData.MDFD_DT)
                    pvt_DepotID = CLng(ResponsibilityData.DPT_ID)
                Case UnitData._UNIT.ToUpper
                    pvt_IDField = UnitData.UNT_ID
                    pvt_CodeField = UnitData.UNT_CD
                    pvt_DescriptionField = UnitData.UNT_DSCRPTN_VC
                    pvt_ActiveField = UnitData.ACTV_BT
                    pvt_Created_BY_Field = UnitData.CRTD_BY
                    pvt_Created_Date_Field = CDate(UnitData.CRTD_DT)
                    pvt_Modified_BY_Field = UnitData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(UnitData.MDFD_DT)
                    pvt_DepotID = CLng(UnitData.DPT_ID)
                Case ItemData._ITEM.ToUpper
                    pvt_IDField = ItemData.ITM_ID
                    pvt_CodeField = ItemData.ITM_CD
                    pvt_DescriptionField = ItemData.ITM_DSCRPTN_VC
                    pvt_ActiveField = ItemData.ACTV_BT
                    pvt_Created_BY_Field = ItemData.CRTD_BY
                    pvt_Created_Date_Field = CDate(ItemData.CRTD_DT)
                    pvt_Modified_BY_Field = ItemData.MDFD_BY
                    pvt_Modified_Date_Field = CDate(ItemData.MDFD_DT)
                    pvt_DepotID = CLng(ItemData.DPT_ID)
            End Select
        End Sub
    End Class
End Class
