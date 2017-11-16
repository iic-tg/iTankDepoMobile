Option Strict On
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.ServiceModel
Imports System.Security.Cryptography
Public Class ConfigSetting

#Region "Properties"

    Private _RemoveProcessIds As String
    Property RemoveProcessIDS As String
        Get
            Return _RemoveProcessIds
        End Get
        Set(ByVal value As String)
            _RemoveProcessIds = value
        End Set
    End Property

    Private _IncludeExceptionalActivity As String
    Property IncludeExceptionalActivityIDs As String
        Get
            Return _IncludeExceptionalActivity
        End Get
        Set(ByVal value As String)
            _IncludeExceptionalActivity = value
        End Set
    End Property

    Private _maxsessions As String
    Property MaxSessions As String
        Get
            Return _maxsessions
        End Get
        Set(ByVal value As String)
            _maxsessions = value
        End Set
    End Property

    Private _KeyExists As Boolean
    Public Property IsKeyExists() As Boolean
        Get
            Return _KeyExists
        End Get
        Set(ByVal value As Boolean)
            _KeyExists = value
        End Set
    End Property

    Private _TabLimit As Integer
    Public Property TabLimit() As Integer
        Get
            Return _TabLimit
        End Get
        Set(ByVal value As Integer)
            _TabLimit = value
        End Set
    End Property

    Private _ContainerCheckDigit As String
    Property ContainerCheckDigit As String
        Get
            Return _ContainerCheckDigit
        End Get
        Set(ByVal value As String)
            _ContainerCheckDigit = value
        End Set
    End Property

    Private _DefautGIContainerStatus As String
    Property DefautGIContainerStatus As String
        Get
            Return _DefautGIContainerStatus
        End Get
        Set(ByVal value As String)
            _DefautGIContainerStatus = value
        End Set
    End Property

    Private _DefaultGIYardLocation As String
    Property DefaultGIYardLocation As String
        Get
            Return _DefaultGIYardLocation
        End Get
        Set(ByVal value As String)
            _DefaultGIYardLocation = value
        End Set
    End Property

    Private _EIRNoAlias As String
    Property EIRNoAlias As String
        Get
            Return _EIRNoAlias
        End Get
        Set(ByVal value As String)
            _EIRNoAlias = value
        End Set
    End Property

    Private _DefaultEquipmentCode As String
    Property DefaultEquipmentCode As String
        Get
            Return _DefaultEquipmentCode
        End Get
        Set(ByVal value As String)
            _DefaultEquipmentCode = value
        End Set
    End Property

    Private _DefaultEquipmentType As String
    Property DefaultEquipmentType As String
        Get
            Return _DefaultEquipmentType
        End Get
        Set(ByVal value As String)
            _DefaultEquipmentType = value
        End Set
    End Property

    Private _DefaultEIRTime As String
    Property DefaultEIRTime As String
        Get
            Return _DefaultEIRTime
        End Get
        Set(ByVal value As String)
            _DefaultEIRTime = value
        End Set
    End Property

    Private _DefautEqType As String
    Property DefautEqType As String
        Get
            Return _DefautEqType
        End Get
        Set(ByVal value As String)
            _DefautEqType = value
        End Set
    End Property

    Private _DefautPreAdviceType As String
    Property DefautPreAdviceType As String
        Get
            Return _DefautPreAdviceType
        End Get
        Set(ByVal value As String)
            _DefautPreAdviceType = value
        End Set
    End Property

    Private _DefaultCleaningCleanedFor As String
    Property DefaultCleaningCleanedFor As String
        Get
            Return _DefaultCleaningCleanedFor
        End Get
        Set(ByVal value As String)
            _DefaultCleaningCleanedFor = value
        End Set
    End Property

    Private _DefaultCleaningLocOfCleaning As String
    Property DefaultCleaningLocOfCleaning As String
        Get
            Return _DefaultCleaningLocOfCleaning
        End Get
        Set(ByVal value As String)
            _DefaultCleaningLocOfCleaning = value
        End Set
    End Property

    Private _DefaultCleaningEqpmntCondition As String
    Property DefaultCleaningEqpmntCondition As String
        Get
            Return _DefaultCleaningEqpmntCondition
        End Get
        Set(ByVal value As String)
            _DefaultCleaningEqpmntCondition = value
        End Set
    End Property

    Private _DefaultCleaningValvFttngCndtn As String
    Property DefaultCleaningValvFttngCndtn As String
        Get
            Return _DefaultCleaningValvFttngCndtn
        End Get
        Set(ByVal value As String)
            _DefaultCleaningValvFttngCndtn = value
        End Set
    End Property

    Private _DefaultCleaningEqpmntClngStts1 As String
    Property DefaultCleaningEqpmntClngStts1 As String
        Get
            Return _DefaultCleaningEqpmntClngStts1
        End Get
        Set(ByVal value As String)
            _DefaultCleaningEqpmntClngStts1 = value
        End Set
    End Property

    Private _DefaultCleaningEqpmntClngStts2 As String
    Property DefaultCleaningEqpmntClngStts2 As String
        Get
            Return _DefaultCleaningEqpmntClngStts2
        End Get
        Set(ByVal value As String)
            _DefaultCleaningEqpmntClngStts2 = value
        End Set
    End Property

    Private _DefaultCleaningCertNo As String
    Property DefaultCleaningCertNo As String
        Get
            Return _DefaultCleaningCertNo
        End Get
        Set(ByVal value As String)
            _DefaultCleaningCertNo = value
        End Set
    End Property

    Private _AllowRental As String
    Property AllowRental As String
        Get
            Return _AllowRental
        End Get
        Set(ByVal value As String)
            _AllowRental = value
        End Set
    End Property

    Private _AllowTransportation As String
    Property AllowTransportation As String
        Get
            Return _AllowTransportation
        End Get
        Set(ByVal value As String)
            _AllowTransportation = value
        End Set
    End Property

    Private _RepairType As String
    Property RepairType As String
        Get
            Return _RepairType
        End Get
        Set(ByVal value As String)
            _RepairType = value
        End Set
    End Property

    Private _DefaultTransPortationEmpty As String
    Property DefaultTransPortationEmpty As String
        Get
            Return _DefaultTransPortationEmpty
        End Get
        Set(value As String)
            _DefaultTransPortationEmpty = value
        End Set
    End Property

    Private _DefaultBulkEMailFormat As String

    Property DefaultBulkEmailFormat As String
        Get
            Return _DefaultBulkEMailFormat
        End Get
        Set(value As String)
            _DefaultBulkEMailFormat = value
        End Set
    End Property

    Private _DefaultBccEmailFormat As String

    Property DefaultBccEmailFormat As String
        Get
            Return _DefaultBccEmailFormat
        End Get
        Set(value As String)
            _DefaultBccEmailFormat = value
        End Set
    End Property

    Private _ExcludeActivity As String

    Property ExcludeActivity As String
        Get
            Return _ExcludeActivity
        End Get
        Set(value As String)
            _ExcludeActivity = value
        End Set
    End Property

    Private _RepairFlow As String

    Property RepairFlow As String
        Get
            Return _RepairFlow
        End Get
        Set(value As String)
            _RepairFlow = value
        End Set
    End Property

    Private _ProductSerialNo As String
    Property ProductSerialNo As String
        Get
            Return _ProductSerialNo
        End Get
        Set(value As String)
            _ProductSerialNo = value
        End Set
    End Property
    Private _WorkFlowFrameHeight As String
    Public Property WorkFlowFrameHeight() As String
        Get
            Return _WorkFlowFrameHeight
        End Get
        Set(ByVal value As String)
            _WorkFlowFrameHeight = value
        End Set
    End Property
    Private _ScheduleDefaultType As String
    Property ScheduleDefaultType As String
        Get
            Return _ScheduleDefaultType
        End Get
        Set(value As String)
            _ScheduleDefaultType = value
        End Set
    End Property
    Private _GenerateXml As String
    Property GenerateXml As String
        Get
            Return _GenerateXml
        End Get
        Set(value As String)
            _GenerateXml = value
        End Set
    End Property

    'Finance Integration
    Private _FinanceIntegration As String
    Property FinanceIntegration As String
        Get
            Return _FinanceIntegration
        End Get
        Set(value As String)
            _FinanceIntegration = value
        End Set
    End Property

    'Gate Out Approval Process
    Private _GateOutApprovalProcess As String
    Property GateOutApprovalProcess As String
        Get
            Return _GateOutApprovalProcess
        End Get
        Set(value As String)
            _GateOutApprovalProcess = value
        End Set
    End Property

    'Invoice Generation - GWS
    Private _InvoiceGenerationGWS As String
    Property InvoiceGenerationGWS As String
        Get
            Return _InvoiceGenerationGWS
        End Get
        Set(value As String)
            _InvoiceGenerationGWS = value
        End Set
    End Property

#End Region

#Region "Declaration"
    'Public ds As New DataSet
    Private dtKeyValues As New DataTable
    Private intDepotID As Integer

#End Region

#Region "Constructor"

    Sub New()
        Me.IsKeyExists = False
    End Sub

    Sub New(ByVal bv_intDepotID As Integer)
        Select Case bv_intDepotID
            Case 1 'BASE
                dtKeyValues = pvt_GetKeyValuesList(bv_intDepotID)
                Me.MaxSessions = pvt_GetKeyValues("001", dtKeyValues)
                Me.ContainerCheckDigit = pvt_GetKeyValues("002", dtKeyValues)
                If pvt_GetKeyValues("003", dtKeyValues) <> "" Then
                    Me.TabLimit = CInt(pvt_GetKeyValues("003", dtKeyValues))
                End If
                Me.IncludeExceptionalActivityIDs = pvt_GetKeyValues("004", dtKeyValues)
                Me.DefautGIContainerStatus = pvt_GetKeyValues("005", dtKeyValues)
                Me.DefaultGIYardLocation = pvt_GetKeyValues("006", dtKeyValues)
                Me.EIRNoAlias = pvt_GetKeyValues("007", dtKeyValues)
                Me.DefaultEIRTime = pvt_GetKeyValues("008", dtKeyValues)
                Me.DefaultEquipmentType = pvt_GetKeyValues("009", dtKeyValues)
                Me.DefaultEquipmentCode = pvt_GetKeyValues("010", dtKeyValues)
                Me.DefautEqType = pvt_GetKeyValues("012", dtKeyValues)
                Me.DefautPreAdviceType = pvt_GetKeyValues("013", dtKeyValues)
                Me.DefaultCleaningCleanedFor = pvt_GetKeyValues("014", dtKeyValues)
                Me.DefaultCleaningLocOfCleaning = pvt_GetKeyValues("015", dtKeyValues)
                Me.DefaultCleaningEqpmntCondition = pvt_GetKeyValues("016", dtKeyValues)
                Me.DefaultCleaningValvFttngCndtn = pvt_GetKeyValues("017", dtKeyValues)
                Me.DefaultCleaningEqpmntClngStts1 = pvt_GetKeyValues("018", dtKeyValues)
                Me.DefaultCleaningEqpmntClngStts2 = pvt_GetKeyValues("019", dtKeyValues)
                Me.DefaultCleaningCertNo = pvt_GetKeyValues("020", dtKeyValues)
                Me.AllowRental = pvt_GetKeyValues("022", dtKeyValues)
                Me.AllowTransportation = pvt_GetKeyValues("023", dtKeyValues)
                Me.RepairType = pvt_GetKeyValues("032", dtKeyValues)
                Me.DefaultTransPortationEmpty = pvt_GetKeyValues("033", dtKeyValues)
                Me.DefaultBulkEmailFormat = pvt_GetKeyValues("034", dtKeyValues)
                Me.DefaultBccEmailFormat = pvt_GetKeyValues("035", dtKeyValues)
                Me.ExcludeActivity = pvt_GetKeyValues("036", dtKeyValues)
                Me.RepairFlow = pvt_GetKeyValues("037", dtKeyValues)
                Me.ProductSerialNo = pvt_GetKeyValues("038", dtKeyValues)
                Me.WorkFlowFrameHeight = pvt_GetKeyValues("039", dtKeyValues) 'WorkFrmae Height
                Me.ScheduleDefaultType = pvt_GetKeyValues("040", dtKeyValues)
                Me.GenerateXml = pvt_GetKeyValues("41", dtKeyValues)
                Me.FinanceIntegration = pvt_GetKeyValues("043", dtKeyValues)
                Me.GateOutApprovalProcess = pvt_GetKeyValues("066", dtKeyValues)
                Me.InvoiceGenerationGWS = pvt_GetKeyValues("067", dtKeyValues)
        End Select
        Me.IsKeyExists = False
    End Sub
#End Region

#Region "Methods"

#Region "GET : pub_GetConfigSingleValue() TABLE NAME:CONFIG"
    <OperationContract()> _
    Public Function pub_GetConfigSingleValue(ByVal bv_strKeyName As String, ByVal bv_intDepotId As Int32) As String

        Try
            Dim dsConfigData As ConfigDataSet
            Dim objConfigs As New Configs
            Dim objConfig As New CommonUI
            Dim strKeyValue As String
            Dim strGetMultiLocationSupport As String = objConfig.GetMultiLocationSupportConfig()
            If strGetMultiLocationSupport.ToLower = "true" Then
                bv_intDepotId = CInt(objConfig.GetHeadQuarterID())
            End If
            dsConfigData = objConfigs.GetConfigByKeyName(bv_strKeyName, bv_intDepotId)
            If Not dsConfigData.Tables(ConfigData._CONFIG).Rows.Count > 0 OrElse dsConfigData.Tables(ConfigData._CONFIG).Rows(0).Item(ConfigData.KY_VL).ToString = "" Then
                strKeyValue = ""
                Me.IsKeyExists = False
            Else
                strKeyValue = DecryptString(dsConfigData.Tables(ConfigData._CONFIG).Rows(0).Item(ConfigData.KY_VL).ToString)
                Me.IsKeyExists = True
            End If
            Return strKeyValue
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET : pub_GetConfigMultiValue() TABLE NAME:CONFIG"
    <OperationContract()> _
    Public Function pub_GetConfigMultiValue(ByVal bv_strKeyName As String, ByVal bv_strDepoIDCD As String) As DataSet
        ' Can be Modifed based on the need
        Try
            Dim dsConfig As DataSet
            Dim objConfigs As New Configs
            Dim strKeyValue As String
            'dsConfigData = objConfigs.GetConfigByKeyName(bv_strKeyName, intHeadQuartersId)
            'If Not dsConfigData.Tables(ConfigData._CONFIG).Rows.Count > 0 Then
            '    strKeyValue = ""
            'Else
            '    strKeyValue = DecryptString(dsConfigData.Tables(ConfigData._CONFIG).Rows(0).Item(ConfigData.KY_VL).ToString)
            'End If
            Return dsConfig
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET :pub_GetConfigTemplate() TABLE NAME:CONFIG"
    <OperationContract()> _
    Public Function pub_GetConfigTemplate(ByVal bv_i64DepoIDId As Int64) As ConfigDataSet

        Try
            Dim dsConfigData As ConfigDataSet
            Dim objConfigs As New Configs
            Dim dtConfigData As DataTable
            dsConfigData = objConfigs.GetConfigByDepoId(bv_i64DepoIDId)
            dtConfigData = objConfigs.GetConfigTemplate().Tables(ConfigData._CONFIG_TEMPLATE)
            For Each drconfig As DataRow In dsConfigData.Tables(ConfigData._CONFIG).Rows
                Dim drArr As DataRow() = dtConfigData.Select(ConfigData.CNFG_TMPLT_ID & "= '" & drconfig.Item(ConfigData.CNFG_TMPLT_ID).ToString & "'")
                drconfig.Item(ConfigData.CNFG_TYP) = drArr(0).Item(ConfigData.CNFG_TYP)
            Next
            For Each drConfig As DataRow In dtConfigData.Rows
                Dim drNewConfig As DataRow = dsConfigData.Tables(ConfigData._CONFIG).NewRow()
                drNewConfig.Item(ConfigData.CNFG_TMPLT_ID) = drConfig.Item(ConfigData.CNFG_TMPLT_ID)
                drNewConfig.Item(ConfigData.KY_NAM) = drConfig.Item(ConfigData.KY_NAM)
                drNewConfig.Item(ConfigData.KY_DSCRPTION) = drConfig.Item(ConfigData.KY_DSCRPTION)
                drNewConfig.Item(ConfigData.KY_VL) = drConfig.Item(ConfigData.KY_VL)
                drNewConfig.Item(ConfigData.CNFG_TYP) = drConfig.Item(ConfigData.CNFG_TYP)
                dsConfigData.Tables(ConfigData._CONFIG).Rows.Add(drNewConfig)
            Next
            dsConfigData.AcceptChanges()
            Return dsConfigData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "UPDATE : pub_UpdateConfig() TABLE NAME:Config"
    <OperationContract()> _
    Public Function pub_UpdateConfig(ByRef br_dsConfigDataset As ConfigDataSet) As Boolean
        Try
            Dim objConfig As New Configs
            For Each drConfig As DataRow In br_dsConfigDataset.Tables(ConfigData._CONFIG).Rows
                Select Case drConfig.RowState
                    Case DataRowState.Modified
                        If IsDBNull(drConfig.Item(ConfigData.CNFG_ID)) Then
                            Dim lngCreated As Long
                            If CommonUIs.iBool(drConfig.Item(ConfigData.ENBLD_BT)) Then
                                lngCreated = pvt_CreateConfig(CommonUIs.iLng(drConfig.Item(ConfigData.CNFG_TMPLT_ID)), _
                                                              drConfig.Item(ConfigData.KY_NAM).ToString, _
                                                              drConfig.Item(ConfigData.KY_DSCRPTION).ToString, _
                                                              drConfig.Item(ConfigData.KY_VL).ToString, _
                                                              CommonUIs.iInt(drConfig.Item(ConfigData.DPT_ID)), _
                                                              CommonUIs.iBool(drConfig.Item(ConfigData.ENBLD_BT)), _
                                                              True)
                                drConfig.Item(ConfigData.CNFG_ID) = lngCreated
                            End If
                        Else
                            pvt_ModifyConfig(CommonUIs.iLng(drConfig.Item(ConfigData.CNFG_ID)), _
                                             CommonUIs.iLng(drConfig.Item(ConfigData.CNFG_TMPLT_ID)), _
                                             drConfig.Item(ConfigData.KY_NAM).ToString, _
                                             drConfig.Item(ConfigData.KY_DSCRPTION).ToString, _
                                             drConfig.Item(ConfigData.KY_VL).ToString, _
                                             CommonUIs.iInt(drConfig.Item(ConfigData.DPT_ID)), _
                                             CommonUIs.iBool(drConfig.Item(ConfigData.ENBLD_BT)), True)
                        End If
                End Select
            Next
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "EncryptData()"
    <OperationContract()> _
    Public Function EncryptData(Message As String) As String
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        Dim Results As Byte()
        Dim pvt_strKeyPhrase As String
        Try
            ' pvt_strKeyPhrase = IO.File.GetLastWriteTime(String.Concat(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings("KeyFileName"))).ToString("yyMMddHHHmmss")
            pvt_strKeyPhrase = "IIC"
            Dim UTF8 As New System.Text.UTF8Encoding()
            Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(pvt_strKeyPhrase))
            TDESAlgorithm.Key = TDESKey
            TDESAlgorithm.Mode = CipherMode.ECB
            TDESAlgorithm.Padding = PaddingMode.PKCS7
            Dim DataToEncrypt As Byte() = UTF8.GetBytes(Message)
            Dim Encryptor As ICryptoTransform = TDESAlgorithm.CreateEncryptor()
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))

        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return Convert.ToBase64String(Results)
    End Function
#End Region

#Region "DecryptString()"
    <OperationContract()> _
    Public Function DecryptString(Message As String) As String
        Dim Results As Byte()
        Dim UTF8 As New System.Text.UTF8Encoding()
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        Dim pvt_strKeyPhrase As String
        Try
            'pvt_strKeyPhrase = IO.File.GetLastWriteTime(String.Concat(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings("KeyFileName"))).ToString("yyMMddHHHmmss")
            pvt_strKeyPhrase = "IIC"
            Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(pvt_strKeyPhrase))
            TDESAlgorithm.Key = TDESKey
            TDESAlgorithm.Mode = CipherMode.ECB
            TDESAlgorithm.Padding = PaddingMode.PKCS7
            Dim DataToDecrypt As Byte() = Convert.FromBase64String(Message)
            Dim Decryptor As ICryptoTransform = TDESAlgorithm.CreateDecryptor()
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return UTF8.GetString(Results)
    End Function
#End Region

#Region "pvt_CreateConfig"
    Private Function pvt_CreateConfig(ByVal bv_i64ConfigTemplateId As Int64, _
                                      ByVal bv_strKeyName As String, _
                                      ByVal bv_strKeyDescription As String, _
                                      ByVal bv_strKeyValue As String, _
                                      ByVal bv_i32DepoIDId As Int32, _
                                      ByVal bv_blnEnabledBit As Boolean, _
                                      ByVal bv_blnActiveBit As Boolean) As Long

        Try
            Dim objConfig As New Configs
            Dim lngCreated As Long
            lngCreated = objConfig.CreateConfig(bv_i64ConfigTemplateId, bv_strKeyName, _
                                                bv_strKeyDescription, bv_strKeyValue, _
                                                bv_i32DepoIDId, bv_blnEnabledBit, _
                                                bv_blnActiveBit)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pvt_ModifyConfig"
    Private Function pvt_ModifyConfig(ByVal bv_i64ConfigId As Int64, _
                                      ByVal bv_i64ConfigTemplateId As Int64, _
                                      ByVal bv_strKeyName As String, _
                                      ByVal bv_strKeyDescription As String, _
                                      ByVal bv_strKeyValue As String, _
                                      ByVal bv_i32DepoIDId As Int32, _
                                      ByVal bv_blnEnabledBit As Boolean, _
                                      ByVal bv_blnActiveBit As Boolean) As Boolean

        Try
            Dim objConfig As New Configs
            Dim blnUpdated As Boolean
            blnUpdated = objConfig.UpdateConfig(bv_i64ConfigId, _
                                                bv_i64ConfigTemplateId, bv_strKeyName, _
                                                bv_strKeyDescription, bv_strKeyValue, _
                                                bv_i32DepoIDId, bv_blnEnabledBit, _
                                                bv_blnActiveBit)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pvt_GetKeyValues()"
    Private Function pvt_GetKeyValues(ByVal strKey As String, ByVal dtKeyValue As DataTable) As String
        Try
            Dim strKeyVal As String = ""
            If Not dtKeyValue Is Nothing AndAlso dtKeyValue.Rows.Count > 0 Then
                Dim drKey As DataRow() = dtKeyValue.Select(String.Concat(ConfigData.KY_NAM, "='", strKey, "'"))
                If drKey.Length > 0 AndAlso Not drKey(0).Item(ConfigData.KY_VL).ToString = "" Then
                    strKeyVal = DecryptString(drKey(0).Item(ConfigData.KY_VL).ToString)
                End If
            End If
            Return strKeyVal
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_GetKeyValuesList()"
    Private Function pvt_GetKeyValuesList(ByVal bv_intDepotID As Integer) As DataTable
        Try
            'intDepotID = pvt_GetDepoIDByDepoCode(bv_strDepoCode)
            Dim dsConfigData As ConfigDataSet
            Dim objConfigs As New Configs
            dsConfigData = objConfigs.GetConfigByDepoId(bv_intDepotID)
            Return dsConfigData.Tables(ConfigData._CONFIG)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_GetDepoIDByDepoCode()"
    Private Function pvt_GetDepoIDByDepoCode(ByVal bv_strDepoCode As String) As Integer
        Dim objConfigs As New Configs
        Return objConfigs.GetDepoIDByDepoCode(bv_strDepoCode)
    End Function
#End Region

#End Region

End Class
