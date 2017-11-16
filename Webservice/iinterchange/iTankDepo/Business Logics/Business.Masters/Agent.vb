Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class Agent
    Inherits CodeBase

#Region "pub_GetV_AgentByAGNT_Id() TABLE NAME:Agent"
    <OperationContract()>
    Public Function pub_GetV_AgentByAGNT_Id(ByVal bv_intAGNT_ID As Integer, ByVal bv_strWFDATA As String) As AgentDataSet

        Try
            Dim objAgent As AgentDataSet
            Dim obAgents As New Agents
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            objAgent = obAgents.GetV_AgentByAGNT_Id(bv_intAGNT_ID, intDepotID)
            Return objAgent
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateAgent() TABLE NAME:Agent"
    <OperationContract()> _
    Public Function pub_CreateAgent(ByVal bv_strAgentCode As String, _
                                       ByVal bv_strAgentName As String, _
                                       ByVal bv_i64AgentCurrencyID As Int64, _
                                       ByVal bv_strContactPersonName As String, _
                                       ByVal bv_strContactAddress As String, _
                                       ByVal bv_strBillingAddress As String, _
                                       ByVal bv_strZipCode As String, _
                                       ByVal bv_strPhoneNumber As String, _
                                       ByVal bv_strFaxNumber As String, _
                                       ByVal bv_strInvoicingEmailID As String, _
                                       ByVal bv_decLBR_RT_PR_HR_NC As Decimal, _
                                       ByVal bv_strCreatedBy As String, _
                                       ByVal bv_datCreatedDate As DateTime, _
                                       ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_blnActiveBit As Boolean, _
                                       ByVal bv_i32DepotID As Int32, _
                                       ByVal bv_strStorageTax As String, _
                                       ByVal bv_strHandlingTax As String, _
                                       ByVal bv_strServiceTax As String, _
                                       ByVal bv_strWfData As String, _
                                       ByRef br_dsAgent As AgentDataSet) As Long
        Dim objTransaction As New Transactions()

        Try
            Dim objAgent As New Agents
            Dim objCommonUI As New CommonUIs
            Dim lngCreated As Long
            lngCreated = objAgent.CreateAgent(bv_strAgentCode, _
                                                    bv_strAgentName, _
                                                    bv_i64AgentCurrencyID, _
                                                    bv_strContactPersonName, _
                                                    bv_strContactAddress, _
                                                    bv_strBillingAddress, _
                                                    bv_strZipCode, _
                                                    bv_strPhoneNumber, _
                                                    bv_strFaxNumber, _
                                                    bv_strInvoicingEmailID, _
                                                    bv_decLBR_RT_PR_HR_NC, _
                                                    bv_strCreatedBy, _
                                                    bv_datCreatedDate, _
                                                    bv_strModifiedBy, _
                                                    bv_datModifiedDate, _
                                                    bv_blnActiveBit, _
                                                    bv_i32DepotID, _
                                                    bv_strStorageTax, _
                                                    bv_strHandlingTax, _
                                                    bv_strServiceTax, _
                                                    objTransaction)

            objCommonUI.CreateServicePartner(lngCreated, "AGENT", bv_i32DepotID, objTransaction)

            pub_UpdateChargeDetail(br_dsAgent, CLng(lngCreated), objTransaction)


            objTransaction.commit()
            Return lngCreated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function
#End Region

#Region "pub_ModifyAgent() TABLE NAME:Agent"
    <OperationContract()> _
    Public Function pub_ModifyAgent(ByVal bv_i64AGNT_ID As Int64, _
                                       ByVal bv_strAgentCode As String, _
                                       ByVal bv_strAgentName As String, _
                                       ByVal bv_i64AgentCurrencyID As Int64, _
                                       ByVal bv_strContactPersonName As String, _
                                       ByVal bv_strContactAddress As String, _
                                       ByVal bv_strBillingAddress As String, _
                                       ByVal bv_strZipCode As String, _
                                       ByVal bv_strPhoneNumber As String, _
                                       ByVal bv_strFaxNumber As String, _
                                       ByVal bv_strInvoicingEmailID As String, _
                                       ByVal bv_decLBR_RT_PR_HR_NC As Decimal, _
                                       ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_blnActiveBit As Boolean, _
                                       ByVal bv_i32DepotID As Int32, _
                                       ByVal bv_strStorageTax As String, _
                                       ByVal bv_strHandlingTax As String, _
                                       ByVal bv_strServiceTax As String, _
                                       ByVal bv_strWfData As String, _
                                       ByRef br_dsAgent As AgentDataSet) As Boolean
        Dim objTransaction As New Transactions()
        Try
            Dim objAgent As New Agents
            Dim blnUpdated As Boolean

            blnUpdated = objAgent.UpdateAgent(bv_i64AGNT_ID, _
                                                    bv_strAgentCode, _
                                                    bv_strAgentName, _
                                                    bv_i64AgentCurrencyID, _
                                                    bv_strContactPersonName, _
                                                    bv_strContactAddress, _
                                                    bv_strBillingAddress, _
                                                    bv_strZipCode, _
                                                    bv_strPhoneNumber, _
                                                    bv_strFaxNumber, _
                                                    bv_strInvoicingEmailID, _
                                                    bv_decLBR_RT_PR_HR_NC, _
                                                    bv_strModifiedBy, _
                                                    bv_datModifiedDate, _
                                                    bv_blnActiveBit, _
                                                    bv_i32DepotID, _
                                                    bv_strStorageTax, _
                                                    bv_strHandlingTax, _
                                                    bv_strServiceTax, _
                                                    objTransaction)
            pub_UpdateChargeDetail(br_dsAgent, bv_i64AGNT_ID, objTransaction)

            objTransaction.commit()
            Return blnUpdated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region

#Region "pub_AgentChargeDetailDeleteAgentChargeDetail() TABLE NAME:Agent_CHARGE_DETAIL"

    <OperationContract()> _
    Public Function pub_AgentChargeDetailDeleteAgentChargeDetail(ByVal bv_i64AgentChargeID As Int64, ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim objAgents As New Agents
            Dim blnDeleted As Boolean
            blnDeleted = objAgents.DeleteAgentChargeDetail(bv_i64AgentChargeID, br_objTransaction)
            Return blnDeleted
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateChargeDetail()"
    <OperationContract()> _
    Public Function pub_UpdateChargeDetail(ByRef dsAgent As AgentDataSet, _
                                           ByVal bv_AgentID As Int64, _
                                           ByRef br_ObjTransactions As Transactions) As Boolean


        Try
            Dim dtAgentChargeDetail As DataTable
            Dim ObjAgents As New Agents
            Dim bolupdatebt As Boolean
            Dim lngCreated As Long
            dtAgentChargeDetail = dsAgent.Tables(AgentData._V_Agent_CHARGE_DETAIL)
            For Each drCharge As DataRow In dtAgentChargeDetail.Rows
                Select Case drCharge.RowState
                    Case DataRowState.Added
                        lngCreated = ObjAgents.CreateAgentChargeDetail(bv_AgentID, _
                                                                             CommonUIs.iLng(drCharge.Item(AgentData.AGNT_EQPMNT_TYP_ID)), _
                                                                             CommonUIs.iLng(drCharge.Item(AgentData.AGNT_EQPMNT_TYP_ID)), _
                                                                             CommonUIs.iDec(drCharge.Item(AgentData.AGNT_HNDLNG_IN_CHRG_NC)), _
                                                                             CommonUIs.iDec(drCharge.Item(AgentData.AGNT_HNDLNG_OUT_CHRG_NC)), _
                                                                             CommonUIs.iDec(drCharge.Item(AgentData.INSPCTN_CHRGS)), _
                                                                             CommonUIs.iBool(drCharge.Item(AgentData.ACTV_BT)), _
                                                                             br_ObjTransactions)

                        pub_UpdateStorageDetail(dsAgent, bv_AgentID, lngCreated, CommonUIs.iLng(drCharge.Item(AgentData.AGNT_CHRG_DTL_ID)), br_ObjTransactions)
                        drCharge.Item(AgentData.AGNT_CHRG_DTL_ID) = lngCreated
                    Case DataRowState.Modified
                        bolupdatebt = ObjAgents.UpdateAgentChargeDetail(CommonUIs.iLng(drCharge.Item(AgentData.AGNT_CHRG_DTL_ID)), _
                                                                              bv_AgentID, _
                                                                              CommonUIs.iLng(drCharge.Item(AgentData.AGNT_EQPMNT_TYP_ID)), _
                                                                              CommonUIs.iLng(drCharge.Item(AgentData.AGNT_EQPMNT_TYP_ID)), _
                                                                              CommonUIs.iDec(drCharge.Item(AgentData.AGNT_HNDLNG_IN_CHRG_NC)), _
                                                                              CommonUIs.iDec(drCharge.Item(AgentData.AGNT_HNDLNG_OUT_CHRG_NC)), _
                                                                             CommonUIs.iDec(drCharge.Item(AgentData.INSPCTN_CHRGS)), _
                                                                              CommonUIs.iBool(drCharge.Item(AgentData.ACTV_BT)), _
                                                                              br_ObjTransactions)
                        pub_UpdateStorageDetail(dsAgent, bv_AgentID, lngCreated, CommonUIs.iLng(drCharge.Item(AgentData.AGNT_CHRG_DTL_ID)), br_ObjTransactions)
                        lngCreated = CommonUIs.iLng(drCharge.Item(AgentData.AGNT_CHRG_DTL_ID))
                    Case DataRowState.Deleted
                        ObjAgents.DeleteAgentStorageDetailByChargeDetailID(CommonUIs.iLng(drCharge.Item(AgentData.AGNT_CHRG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                        pub_AgentChargeDetailDeleteAgentChargeDetail(CommonUIs.iLng(drCharge.Item(AgentData.AGNT_CHRG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                    Case DataRowState.Unchanged
                        lngCreated = CommonUIs.iLng(drCharge.Item(AgentData.AGNT_CHRG_DTL_ID))
                        pub_UpdateStorageDetail(dsAgent, bv_AgentID, lngCreated, CommonUIs.iLng(drCharge.Item(AgentData.AGNT_CHRG_DTL_ID)), br_ObjTransactions)
                End Select
            Next

            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateStorageDetail()"
    <OperationContract()> _
    Public Function pub_UpdateStorageDetail(ByRef br_dsAgent As AgentDataSet, _
                                            ByVal bv_i64AgentID As Int64, _
                                            ByVal bv_AgentChargeDetailID As Int64, _
                                            ByVal bv_oldAgentChargeDetailID As Int64, _
                                            ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dtAgentStorageDetail As DataTable
            Dim ObjAgents As New Agents
            Dim bolupdatebt As Boolean

            dtAgentStorageDetail = br_dsAgent.Tables(AgentData._Agent_STORAGE_DETAIL)
            For Each drStorage As DataRow In dtAgentStorageDetail.Select(String.Concat(AgentData.AGNT_CHRG_DTL_ID, "=", bv_oldAgentChargeDetailID))
                Select Case drStorage.RowState
                    Case DataRowState.Added
                        Dim lngCreated As Long = ObjAgents.CreateAgentStorageDetail(bv_AgentChargeDetailID, _
                                                                                          bv_i64AgentID, _
                                                                                          CommonUIs.iInt(drStorage.Item(AgentData.AGNT_UP_TO_DYS)), _
                                                                                          CommonUIs.iDec(drStorage.Item(AgentData.AGNT_STRG_CHRG_NC)), _
                                                                                          drStorage.Item(AgentData.AGNT_RMRKS_VC).ToString, _
                                                                                          br_ObjTransactions)

                        drStorage.Item(AgentData.AGNT_STRG_DTL_ID) = lngCreated

                    Case DataRowState.Modified
                        bolupdatebt = ObjAgents.UpdateAgentStorageDetail(CommonUIs.iLng(drStorage.Item(AgentData.AGNT_STRG_DTL_ID)), _
                                                                               bv_AgentChargeDetailID, _
                                                                               bv_i64AgentID, _
                                                                               CommonUIs.iInt(drStorage.Item(AgentData.AGNT_UP_TO_DYS)), _
                                                                               CommonUIs.iDec(drStorage.Item(AgentData.AGNT_STRG_CHRG_NC)), _
                                                                               drStorage.Item(AgentData.AGNT_RMRKS_VC).ToString, _
                                                                               br_ObjTransactions)
                    Case DataRowState.Deleted
                        ObjAgents.DeleteAgentStorageDetailByStorageDetailID(CInt(drStorage.Item(AgentData.AGNT_STRG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next

            For Each drStorage As DataRow In dtAgentStorageDetail.Rows
                Select Case drStorage.RowState
                    Case DataRowState.Deleted
                        ObjAgents.DeleteAgentStorageDetailByStorageDetailID(CInt(drStorage.Item(AgentData.AGNT_STRG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next

            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetAgentChargeDetailByAgentID() TABLE NAME:Agent_CHARGE_DETAIL"

    <OperationContract()> _
    Public Function pub_GetAgentChargeDetailByAgentID(ByVal bv_i64AgentID As Int64) As AgentDataSet
        Try
            Dim dsAgentStorageDetailData As AgentDataSet
            Dim objAgents As New Agents
            dsAgentStorageDetailData = objAgents.AgentChargeDetailByAgentID(bv_i64AgentID)
            'objAgents.GetV_Agent_EMAIL_SETTING(bv_i64AgentID)
            Return dsAgentStorageDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetAgentChargeDetailByAgentID() TABLE NAME:Agent_CHARGE_DETAIL"

    <OperationContract()> _
    Public Function pub_GetAgentChargeDetailByAgentID(ByVal bv_i64AgentID As Int64, ByVal bv_i64AgentChargeDetailID As Int64) As AgentDataSet
        Try
            Dim dsAgentStorageDetailData As AgentDataSet
            Dim objAgents As New Agents
            dsAgentStorageDetailData = objAgents.AgentStorageDetailByAgentChargeDetailID(bv_i64AgentID, bv_i64AgentChargeDetailID)
            Return dsAgentStorageDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetServicePartnerByCode"
    Public Function pub_GetServicePartnerByCode(ByVal bv_strAgentCode As String, ByVal strServiceType As String, ByVal intDepotID As Integer) As Boolean
        Dim dsAgent As New AgentDataSet
        Dim objAgent As New Agents
        dsAgent = objAgent.GetAgentByCode(bv_strAgentCode, strServiceType, intDepotID)
        If dsAgent.Tables(AgentData._V_SERVICE_PARTNER).Rows.Count > 0 Then
            strServiceType = CStr(dsAgent.Tables(AgentData._V_SERVICE_PARTNER).Rows(0).Item(AgentData.SRVC_PRTNR_TYP_CD))
            Return False
        End If
        Return True
    End Function
#End Region
End Class