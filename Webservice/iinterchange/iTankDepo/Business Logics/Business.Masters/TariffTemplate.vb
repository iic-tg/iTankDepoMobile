Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
<ServiceContract()> _
Public Class TariffTemplate
    '#Region "pub_TariffTemplateCreateTariffTemplate() TABLE NAME:Tariff_Template"

    '    <OperationContract()> _
    '    Public Function pub_TariffTemplateCreateTariffTemplate(ByVal bv_strTRFF_SZ As String, _
    '     ByVal bv_strTRFF_TYP As String, _
    '     ByVal bv_i32 As Int32, _
    '     ByVal bv_i32ACTV_BT As Boolean, _
    '     ByVal bv_strWfData As String) As Long

    '        Try
    '            Dim objTariff_Template As New TariffTemplates
    '            Dim lngCreated As Long
    '            lngCreated = objTariff_Template.CreateTariffTemplate(bv_strTRFF_SZ, _
    '                  bv_strTRFF_TYP, bv_i32ACTV_BT, _
    '                  bv_i32DPT_ID)
    '            Return lngCreated
    '        Catch ex As Exception
    '            Throw New FaultException(New FaultReason(ex.Message))
    '        End Try
    '    End Function

    '#End Region

    '#Region "pub_TariffTemplateModifyTariffTemplateTariff_Template() TABLE NAME:Tariff_Template"

    '    <OperationContract()> _
    '    Public Function pub_TariffTemplateModifyTariff_Template(ByVal bv_i32 As Int32, _
    '     ByVal bv_str As String, _
    '     ByVal bv_str As String, _
    '     ByVal bv_i32 As Int32, _
    '     ByVal bv_i32 As Int32, _
    '     ByVal bv_strWfData As String) As Boolean

    '        Try
    '            Dim objTariff_Template As New TariffTemplates
    '            Dim blnUpdated As Boolean
    '            blnUpdated = objTariff_Template.UpdateTariff_Template(bv_i32TRFF_TMPLT_ID, _
    '                  bv_strTRFF_SZ, bv_strTRFF_TYP, _
    '                  bv_i32ACTV_BT, bv_i32DPT_ID)
    '            Return blnUpdated
    '        Catch ex As Exception
    '            Throw New FaultException(New FaultReason(ex.Message))
    '        End Try
    '    End Function

    '#End Region

    '#Region "pub_TariffTemplateDeleteTariffTemplate() TABLE NAME:Tariff_Template"

    '    <OperationContract()> _
    '    Public Function pub_TariffTemplateDeleteTariffTemplate(ByVal bv_i32 As Int32) As Boolean
    '        Try
    '            Dim objTariff_Template As New TariffTemplates
    '            Dim blnDeleted As Boolean
    '            blnDeleted = objTariff_Template.DeleteTariffTemplate(bv_i32)
    '            Return blnDeleted
    '        Catch ex As Exception
    '            Throw New FaultException(New FaultReason(ex.Message))
    '        End Try
    '    End Function
    '#End Region



    '#Region "pub_ValidateTarifftemplate() TABLE NAME:Tarifftemplate"

    '    <OperationContract()> _
    '    Public Function pub_ValidateTarifftemplate(ByVal bv_strTarifftemplateCode As String, ByVal bv_WfData As String) As Boolean

    '        Try
    '            Dim objTarifftemplates As New TariffTemplates
    '            Dim intRowCount As Integer
    '            intRowCount = CInt(objTarifftemplates.GetTarifftemplateByTarifftemplateCode(bv_strTarifftemplateCode, CLng(CommonUIs.ParseWFDATA(bv_WfData, TariffTemplateData.DPT_ID))))
    '            If intRowCount > 0 Then
    '                Return False
    '            Else
    '                Return True
    '            End If
    '        Catch ex As Exception
    '            Throw New FaultException(New FaultReason(ex.Message))
    '        End Try
    '    End Function
    '#End Region

#Region "pub_TarifftemplateGetTarifftemplateByDepotID() TABLE NAME:Tarifftemplate"

    <OperationContract()> _
    Public Function pub_TarifftemplateGetTarifftemplateByDepotID(ByVal bv_strWFDATA As String) As TariffTemplateDataSet

        Try
            Dim dsTarifftemplateData As TariffTemplateDataSet
            Dim objTarifftemplates As New TariffTemplates
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, TariffTemplateData.DPT_ID))
            dsTarifftemplateData = objTarifftemplates.GetTarifftemplateByDepotID(intDepotID)
            Return dsTarifftemplateData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateTariffTemplate() TABLE NAME:Tariff_template"

    <OperationContract()> _
    Public Function pub_UpdateTariffTemplate(ByRef br_dsTariffTemplate As TariffTemplateDataSet, _
                                        ByVal bv_strWfData As String) As Boolean
        Try
            Dim objTarifftemplate As New TariffTemplates
            Dim dtTarifftemplate As DataTable
            dtTarifftemplate = br_dsTariffTemplate.Tables(TariffTemplateData._TARIFF_TEMPLATE).GetChanges(DataRowState.Added)

            If Not dtTarifftemplate Is Nothing Then
                For Each drTarifftemplate As DataRow In dtTarifftemplate.Rows
                    Dim lngTarifftemplateId As Long
                    lngTarifftemplateId = objTarifftemplate.CreateTariffTemplate(drTarifftemplate.Item(TariffTemplateData.EQPMNT_SZ).ToString(), drTarifftemplate.Item(TariffTemplateData.EQPMNT_TYP).ToString(), _
                                                                CBool(drTarifftemplate.Item(TariffTemplateData.ACTV_BT)), CommonUIs.iInt(CommonUIs.ParseWFDATA(bv_strWfData, TariffTemplateData.DPT_ID)), CommonUIs.iInt(drTarifftemplate.Item(TariffTemplateData.CSTMR_ID)))
                Next
            End If
            dtTarifftemplate = br_dsTariffTemplate.Tables(TariffTemplateData._TARIFF_TEMPLATE).GetChanges(DataRowState.Modified)
            If Not dtTarifftemplate Is Nothing Then
                For Each drTarifftemplate As DataRow In dtTarifftemplate.Rows
                    objTarifftemplate.UpdateTariffTemplate(CommonUIs.iInt(drTarifftemplate.Item(TariffTemplateData.TRFF_TMPLT_ID)), drTarifftemplate.Item(TariffTemplateData.EQPMNT_SZ).ToString(), drTarifftemplate.Item(TariffTemplateData.EQPMNT_TYP).ToString(), _
                                                                CBool(drTarifftemplate.Item(TariffTemplateData.ACTV_BT)), CommonUIs.iInt(CommonUIs.ParseWFDATA(bv_strWfData, TariffTemplateData.DPT_ID)), CommonUIs.iInt(drTarifftemplate.Item(TariffTemplateData.CSTMR_ID)))
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_GetTariffByCstmrID() TABLE NAME:Tariff"

    <OperationContract()> _
    Public Function pub_GetTariffByCstmrID(ByVal bv_strEqpmnt_type As String, ByVal bv_strEqpmnt_Size As String, ByVal bv_i64CstmrID As Int64, ByVal bv_i32DepotID As Int32) As TariffTemplateDataSet

        Try
            Dim dsTarifftemplateData As TariffTemplateDataSet
            Dim objTarifftemplates As New TariffTemplates
            dsTarifftemplateData = objTarifftemplates.GetTariffByCstmrID(bv_strEqpmnt_type, bv_strEqpmnt_Size, bv_i64CstmrID, bv_i32DepotID)
            Return dsTarifftemplateData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


End Class
