#Region " SubItem.vb"
'*********************************************************************************************************************
'Name :
'      SubItem.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(SubItem.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 11:15:02 AM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class SubItem
    Inherits CodeBase
#Region "pub_ValidateSubItem() TABLE NAME:SUB_ITEM"

    <OperationContract()> _
    Public Function pub_ValidateSubItem(ByVal bv_strSubItemCode As String, ByVal bv_WfData As String) As Boolean

        Try
            Dim objSubItems As New SubItems
            Dim intRowCount As Integer
            intRowCount = CInt(objSubItems.GetSubItemBySubItemCode(bv_strSubItemCode, CLng(CommonUIs.ParseWFDATA(bv_WfData, SubItemData.DPT_ID))))
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

#Region "pub_ValidatePK() TABLE NAME:SUB_ITEM"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strSubItemCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsSubItemData As SubItemDataSet
            Dim objSubItems As New SubItems
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsSubItemData = objSubItems.GetSubItemBySubItemCode(bv_strSubItemCode, intDepotID)
            If dsSubItemData.Tables(SubItemData._SUB_ITEM).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_SubItemGetSubItemByDepotID() TABLE NAME:SUB_ITEM"

    <OperationContract()> _
    Public Function pub_SubItemGetSubItemByDepotID(ByVal bv_strWFDATA As String) As SubItemDataSet
        Try
            Dim dsSubItemData As SubItemDataSet
            Dim objSubItems As New SubItems
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, SubItemData.DPT_ID))
            dsSubItemData = objSubItems.GetSubItemByDepotID(intDepotID)
            Return dsSubItemData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateSubItem() TABLE NAME:SUB_ITEM"

    <OperationContract()> _
    Public Function pub_UpdateSubItem(ByRef br_dsSubItem As SubItemDataSet, _
                                        ByVal bv_strCreatedBy As String, _
                                        ByVal bv_datCreatedDate As DateTime, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_strWfData As String) As Boolean
        Try
            Dim objSubItem As New SubItems
            Dim dtSubItem As DataTable
            dtSubItem = br_dsSubItem.Tables(SubItemData._SUB_ITEM).GetChanges(DataRowState.Added)

            If Not dtSubItem Is Nothing Then
                For Each drSubItem As DataRow In dtSubItem.Rows
                    Dim lngSubItemId As Long
                    lngSubItemId = objSubItem.CreateSubItem(drSubItem.Item(SubItemData.SB_ITM_CD).ToString(), _
                                                            drSubItem.Item(SubItemData.SB_ITM_DSCRPTN_VC).ToString(), _
                                                            CLng(drSubItem.Item(SubItemData.ITM_ID)), _
                                                            bv_strCreatedBy, _
                                                            bv_datCreatedDate, _
                                                            bv_strModifiedBy, _
                                                            bv_datModifiedDate, _
                                                            CBool(drSubItem.Item(SubItemData.ACTV_BT)), _
                                                            CInt(CommonUIs.ParseWFDATA(bv_strWfData, SubItemData.DPT_ID)))
                Next
            End If
            dtSubItem = br_dsSubItem.Tables(SubItemData._SUB_ITEM).GetChanges(DataRowState.Modified)
            If Not dtSubItem Is Nothing Then
                For Each drSubItem As DataRow In dtSubItem.Rows
                    objSubItem.UpdateSubItem(CLng(drSubItem.Item(SubItemData.SB_ITM_ID)), _
                                             drSubItem.Item(SubItemData.SB_ITM_CD).ToString(), _
                                             drSubItem.Item(SubItemData.SB_ITM_DSCRPTN_VC).ToString(), _
                                             CLng(drSubItem.Item(SubItemData.ITM_ID)), _
                                             bv_strModifiedBy, _
                                             bv_datModifiedDate, _
                                             CBool(drSubItem.Item(SubItemData.ACTV_BT)), _
                                             CInt(CommonUIs.ParseWFDATA(bv_strWfData, SubItemData.DPT_ID)))
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
