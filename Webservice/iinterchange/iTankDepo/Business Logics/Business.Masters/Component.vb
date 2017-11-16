Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
<ServiceContract()> _
Public Class Component

#Region "pub_ValidateComponent() TABLE NAME:COMPONENT"

    <OperationContract()> _
    Public Function pub_ValidateComponent(ByVal bv_strComponentCode As String, ByVal bv_WfData As String) As Boolean

        Try
            Dim objComponents As New Components
            Dim intRowCount As Integer
            intRowCount = CInt(objComponents.GetComponentByComponentCode(bv_strComponentCode, CLng(CommonUIs.ParseWFDATA(bv_WfData, ComponentData.DPT_ID))))
            If intRowCount > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ComponentGetComponentByDepotID() TABLE NAME:COMPONENT"

    <OperationContract()> _
    Public Function pub_ComponentGetComponentByDepotID(ByVal bv_strWFDATA As String) As ComponentDataSet

        Try
            Dim dsComponentData As ComponentDataSet
            Dim objComponents As New Components
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, ComponentData.DPT_ID))
            dsComponentData = objComponents.GetComponentByDepotID(intDepotID)
            Return dsComponentData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateComponent() TABLE NAME:Component"

    <OperationContract()> _
    Public Function pub_UpdateComponent(ByRef br_dsComponet As ComponentDataSet, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_strWfData As String) As Boolean
        Try
            Dim objComponent As New Components
            Dim dtComponent As DataTable
            dtComponent = br_dsComponet.Tables(ComponentData._COMPONENT).GetChanges(DataRowState.Added)

            If Not dtComponent Is Nothing Then
                For Each drComponent As DataRow In dtComponent.Rows
                    Dim lngComponentId As Long
                    lngComponentId = objComponent.CreateComponent(drComponent.Item(ComponentData.CMPNNT_CD).ToString(), drComponent.Item(ComponentData.CMPNNT_DSCRPTN_VC).ToString(), _
                                                                CLng(drComponent.Item(ComponentData.EQPMNT_TYP_ID)), drComponent.Item(ComponentData.ASSMBLY).ToString(), _
                                                                bv_strModifiedBy, bv_datModifiedDate, CBool(drComponent.Item(ComponentData.ACTV_BT)), CInt(CommonUIs.ParseWFDATA(bv_strWfData, ComponentData.DPT_ID)))
                Next
            End If
            dtComponent = br_dsComponet.Tables(ComponentData._COMPONENT).GetChanges(DataRowState.Modified)
            If Not dtComponent Is Nothing Then
                For Each drComponent As DataRow In dtComponent.Rows
                    objComponent.UpdateComponent(CLng(drComponent.Item(ComponentData.CMPNNT_ID)), drComponent.Item(ComponentData.CMPNNT_CD).ToString(), drComponent.Item(ComponentData.CMPNNT_DSCRPTN_VC).ToString(), _
                                                CLng(drComponent.Item(ComponentData.EQPMNT_TYP_ID)), drComponent.Item(ComponentData.ASSMBLY).ToString(), bv_strModifiedBy, bv_datModifiedDate, _
                                                CBool(drComponent.Item(ComponentData.ACTV_BT)), CInt(CommonUIs.ParseWFDATA(bv_strWfData, ComponentData.DPT_ID)))
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
