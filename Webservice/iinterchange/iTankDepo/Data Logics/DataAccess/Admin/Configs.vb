#Region " Configs.vb"
'*********************************************************************************************************************
'Name :
'      Configs.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Configs.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      10/5/2012 2:11:24 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
#Region "Configs"

Public Class Configs

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private ds As ConfigDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New ConfigDataSet
    End Sub

#End Region

#Region "Config"
#Region "Declaration Part.. "
    Private Const ConfigSelectQueryByDepotId As String = "SELECT CNFG_ID,CNFG_TMPLT_ID,KY_NAM,KY_DSCRPTION,KY_VL,DPT_ID,ENBLD_BT,ACTV_BT FROM CONFIG WHERE DPT_ID=@DPT_ID AND ACTV_BT=1"
    Private Const ConfigSelectQueryByKeyNameDepotId As String = "SELECT CNFG_ID,CNFG_TMPLT_ID,KY_NAM,KY_DSCRPTION,KY_VL,DPT_ID,ENBLD_BT,ACTV_BT FROM CONFIG WHERE KY_NAM=@KY_NAM AND DPT_ID=@DPT_ID AND ACTV_BT=1 AND ENBLD_BT=1"
    Private Const ConfigTemplateSelectQuery As String = "SELECT CNFG_TMPLT_ID,KY_NAM,KY_DSCRPTION,CNFG_TYP,ACTV_BT FROM CONFIG_TEMPLATE"
    Private Const ConfigInsertQuery As String = "INSERT INTO CONFIG(CNFG_ID,CNFG_TMPLT_ID,KY_NAM,KY_DSCRPTION,KY_VL,DPT_ID,ENBLD_BT,ACTV_BT)VALUES(@CNFG_ID,@CNFG_TMPLT_ID,@KY_NAM,@KY_DSCRPTION,@KY_VL,@DPT_ID,@ENBLD_BT,@ACTV_BT)"
    Private Const ConfigUpdateQuery As String = "UPDATE Config SET CNFG_TMPLT_ID=@CNFG_TMPLT_ID, KY_NAM=@KY_NAM, KY_DSCRPTION=@KY_DSCRPTION, KY_VL=@KY_VL, DPT_ID=@DPT_ID, ENBLD_BT=@ENBLD_BT, ACTV_BT=@ACTV_BT WHERE CNFG_ID=@CNFG_ID"
    Private Const DepoSelectQueryPk As String = "SELECT DPT_ID FROM DEPOT WHERE DPT_CD=@DPT_CD"
    Private Const ConfigSelectQueryByKeyNameDepotIdMultiple As String = "SELECT CNFG_ID,CNFG_TMPLT_ID,KY_NAM,KY_DSCRPTION,KY_VL,DPT_ID,ENBLD_BT,ACTV_BT FROM CONFIG WHERE KY_NAM=@KY_NAM  AND ACTV_BT=1 AND ENBLD_BT=1"
#End Region

#Region "GetConfigByConfigId() TABLE NAME:CONFIG"

    Public Function GetConfigByKeyName(ByVal bv_strKeyName As String, ByVal bv_intDepoId As Int64) As ConfigDataSet
        Try
            Dim ds As New ConfigDataSet
            Dim hshConfiguration As New Hashtable()
            hshConfiguration.Add(ConfigData.KY_NAM, bv_strKeyName)
            hshConfiguration.Add(ConfigData.DPT_ID, bv_intDepoId)
            objData = New DataObjects(ConfigSelectQueryByKeyNameDepotId, hshConfiguration)
            objData.Fill(CType(ds, DataSet), ConfigData._CONFIG)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetConfigByConfigId() TABLE NAME:CONFIG"

    Public Function GetConfigByKeyName(ByVal bv_strKeyName As String) As ConfigDataSet
        Try
            Dim ds As New ConfigDataSet
            Dim hshConfiguration As New Hashtable()
            hshConfiguration.Add(ConfigData.KY_NAM, bv_strKeyName)
            '  hshConfiguration.Add(ConfigData.DPT_ID, bv_intDepoId)
            objData = New DataObjects(ConfigSelectQueryByKeyNameDepotIdMultiple, hshConfiguration)
            objData.Fill(CType(ds, DataSet), ConfigData._CONFIG)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetConfigByConfigId() TABLE NAME:CONFIG"

    Public Function GetConfigByDepoId(ByVal bv_intDepoId As Int64) As ConfigDataSet
        Try
            Dim ds As New ConfigDataSet
            objData = New DataObjects(ConfigSelectQueryByDepotId, ConfigData.DPT_ID, CStr(bv_intDepoId))
            objData.Fill(CType(ds, DataSet), ConfigData._CONFIG)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetConfigTemplate() TABLE NAME:CONFIG"

    Public Function GetConfigTemplate() As ConfigDataSet
        Try
            Dim ds As New ConfigDataSet
            objData = New DataObjects(ConfigTemplateSelectQuery)
            objData.Fill(CType(ds, DataSet), ConfigData._CONFIG_TEMPLATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateConfig() TABLE NAME:Config"

    Public Function CreateConfig(ByVal bv_i64ConfigTemplateId As Int64, _
                                 ByVal bv_strKeyName As String, _
                                 ByVal bv_strKeyDescription As String, _
                                 ByVal bv_strKeyValue As String, _
                                 ByVal bv_intDepoId As Int32, _
                                 ByVal bv_blnEnabledBit As Boolean, _
                                 ByVal bv_blnActiveBit As Boolean) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            Dim ds As New ConfigDataSet
            objData = New DataObjects()
            dr = ds.Tables(ConfigData._CONFIG).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ConfigData._CONFIG)
                .Item(ConfigData.CNFG_ID) = intMax
                .Item(ConfigData.CNFG_TMPLT_ID) = bv_i64ConfigTemplateId
                .Item(ConfigData.KY_NAM) = bv_strKeyName
                .Item(ConfigData.KY_DSCRPTION) = bv_strKeyDescription
                If bv_strKeyValue <> Nothing Then
                    .Item(ConfigData.KY_VL) = bv_strKeyValue
                Else
                    .Item(ConfigData.KY_VL) = String.Empty
                End If
                .Item(ConfigData.DPT_ID) = bv_intDepoId
                If bv_blnEnabledBit <> Nothing Then
                    .Item(ConfigData.ENBLD_BT) = bv_blnEnabledBit
                Else
                    .Item(ConfigData.ENBLD_BT) = DBNull.Value
                End If
                .Item(ConfigData.ACTV_BT) = bv_blnActiveBit
            End With
            objData.InsertRow(dr, ConfigInsertQuery)
            dr = Nothing
            CreateConfig = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateConfig() TABLE NAME:Config"

    Public Function UpdateConfig(ByVal bv_i64ConfigId As Int64, _
                                 ByVal bv_i64ConfigTemplateId As Int64, _
                                 ByVal bv_strKeyName As String, _
                                 ByVal bv_strKeyDescription As String, _
                                 ByVal bv_strKeyValue As String, _
                                 ByVal bv_intDepoId As Int32, _
                                 ByVal bv_blnEnabledBit As Boolean, _
                                 ByVal bv_blnActiveBit As Boolean) As Boolean
        Try
            Dim dr As DataRow
            Dim ds As New ConfigDataSet
            objData = New DataObjects()
            dr = ds.Tables(ConfigData._CONFIG).NewRow()
            With dr
                .Item(ConfigData.CNFG_ID) = bv_i64ConfigId
                .Item(ConfigData.CNFG_TMPLT_ID) = bv_i64ConfigTemplateId
                .Item(ConfigData.KY_NAM) = bv_strKeyName
                .Item(ConfigData.KY_DSCRPTION) = bv_strKeyDescription
                If bv_strKeyValue <> Nothing Then
                    .Item(ConfigData.KY_VL) = bv_strKeyValue
                Else
                    .Item(ConfigData.KY_VL) = String.Empty
                End If
                .Item(ConfigData.DPT_ID) = bv_intDepoId
                If bv_blnEnabledBit <> Nothing Then
                    .Item(ConfigData.ENBLD_BT) = bv_blnEnabledBit
                Else
                    .Item(ConfigData.ENBLD_BT) = False
                End If
                .Item(ConfigData.ACTV_BT) = bv_blnActiveBit
            End With
            UpdateConfig = objData.UpdateRow(dr, ConfigUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GET : GetHeadQuarterIDByHeadQuarterCode() TABLE NAME:HEAD_QUARTERS"

    Public Function GetDepoIDByDepoCode(ByVal bv_strHeadQuartersCD As String) As Integer
        Try
            objData = New DataObjects(DepoSelectQueryPk, ConfigData.DPT_CD, bv_strHeadQuartersCD)
            Return CInt(objData.ExecuteScalar())
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#End Region


End Class

#End Region
