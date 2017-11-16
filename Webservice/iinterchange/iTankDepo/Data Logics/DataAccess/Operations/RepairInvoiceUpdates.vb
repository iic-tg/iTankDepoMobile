Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class RepairInvoiceUpdates

#Region "Declaration Part"
    Dim objData As DataObjects

    Private Const V_REPAIR_CHARGESelectQuery As String = "SELECT RPR_CHRG_ID,EQPMNT_NO,RPR_ESTMT_ID,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,ESTMT_NO,RPR_APPRVL_DT,RPR_CMPLTN_DT,MTRL_CST_NC,LBR_CST_NC,RPR_TX_RT_NC,RPR_TX_AMNT_NC,TTL_CSTS_NC,RPR_APPRVL_AMNT_NC,BLLNG_FLG,YRD_LCTN,DPT_ID,DPT_CD,DPT_NAM,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_CD,GTN_DT,CLN_CST_NC,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ACTV_BT,TTL_SRV_TAX,TTL_EST_INCL_SRV_TAX,INVC_TYPE,LEAK_TEST,PTI,SURVEY,TOTALAMOUNT,SRVC_PRTNR_ID,APPRVL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,CSTMR_TTL_CSTS_NC,PRTY_TTL_CSTS_NC,PRTY_APPRVL_RF,APPRVL_RF_NO FROM V_REPAIR_CHARGE WHERE DPT_ID=@DPT_ID AND BLLNG_FLG <>'B' ORDER BY RPR_CMPLTN_DT DESC"
    Private Const Repair_ChargeUpdateQuery As String = "UPDATE REPAIR_CHARGE SET INVCNG_PRTY_ID=@INVCNG_PRTY_ID,APPRVL_RF_NO=@APPRVL_RF_NO WHERE EQPMNT_NO=@EQPMNT_NO AND RPR_CHRG_ID=@RPR_CHRG_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const RepairEstimateSelectQueryByDepotId As String = "SELECT RPR_ESTMT_ID,RPR_ESTMT_NO,GI_TRNSCTN_NO,RVSN_NO,RPR_ESTMT_DT,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,DPT_ID,DPT_CD,DPT_NAM,SRVYR_NM,LBR_RT_NC,RPR_TYP_ID,RPR_TYP_CD,CRT_OF_CLNLNSS_BT,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ORGNL_ESTMN_DT,ESTMTN_TTL_NC,OWNR_APPRVL_RF,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTVTY_DT,ACTVTY_NM,APPRVL_AMNT_NC,ORGNL_ESTMTN_AMNT_NC,CSTMR_ESTMTN_TTL_NC,CSTMR_APPRVL_AMNT_NC,ACTL_MN_HR_NC FROM V_REPAIR_ESTIMATE WHERE RPR_ESTMT_ID IN (SELECT RPR_ESTMT_ID FROM REPAIR_CHARGE WHERE RPR_CHRG_ID=@RPR_CHRG_ID) AND EQPMNT_NO=@EQPMNT_NO "
    Private Const Repair_EstimateUpdatePartyQuery As String = "UPDATE REPAIR_ESTIMATE SET PRTY_APPRVL_RF=@PRTY_APPRVL_RF WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND RPR_ESTMT_NO=@RPR_ESTMT_NO "
    Private Const Repair_EstimateUpdateCustomerQuery As String = "UPDATE REPAIR_ESTIMATE SET OWNR_APPRVL_RF=@OWNR_APPRVL_RF WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND RPR_ESTMT_NO=@RPR_ESTMT_NO "
    Private Const V_REPAIR_CHARGESelectQueryAllDepots As String = "SELECT RPR_CHRG_ID,EQPMNT_NO,RPR_ESTMT_ID,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,ESTMT_NO,RPR_APPRVL_DT,RPR_CMPLTN_DT,MTRL_CST_NC,LBR_CST_NC,RPR_TX_RT_NC,RPR_TX_AMNT_NC,TTL_CSTS_NC,RPR_APPRVL_AMNT_NC,BLLNG_FLG,YRD_LCTN,DPT_ID,DPT_CD,DPT_NAM,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_CD,GTN_DT,CLN_CST_NC,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ACTV_BT,TTL_SRV_TAX,TTL_EST_INCL_SRV_TAX,INVC_TYPE,LEAK_TEST,PTI,SURVEY,TOTALAMOUNT,SRVC_PRTNR_ID,APPRVL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,CSTMR_TTL_CSTS_NC,PRTY_TTL_CSTS_NC,PRTY_APPRVL_RF,APPRVL_RF_NO FROM V_REPAIR_CHARGE WHERE BLLNG_FLG <>'B' ORDER BY RPR_CMPLTN_DT DESC"
    Private ds As RepairInvoiceUpdateDataSet
#End Region

#Region "Constructor.."
    Sub New()
        ds = New RepairInvoiceUpdateDataSet
    End Sub
#End Region

#Region "GetRepairChargevalues"
    Public Function GetRepairChargevalues(ByVal bv_intDepotID As Integer) As RepairInvoiceUpdateDataSet
        Try
            Try
                objData = New DataObjects(V_REPAIR_CHARGESelectQuery, RepairInvoiceUpdateData.DPT_ID, bv_intDepotID)
                objData.Fill(CType(ds, DataSet), RepairInvoiceUpdateData._V_REPAIR_CHARGE)
                Return ds
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateRepairCharge"
    Public Function UpdateRepairCharge(ByVal bv_intRepairChargeID As Int32, _
                                       ByVal bv_strEqpmntNo As String, _
                                       ByVal bv_strGI_trans_no As String, _
                                       ByVal bv_RefNo As String, _
                                       ByVal bv_amount As Double, _
                                       ByVal bv_invoicingPartyId As Integer, _
                                       ByVal bv_modifiedby As String, _
                                       ByVal bv_modifieddate As Date, _
                                       ByVal bv_intDepotID As Integer, _
                                       ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RepairInvoiceUpdateData._REPAIR_CHARGE).NewRow()
            With dr
                .Item(RepairInvoiceUpdateData.RPR_CHRG_ID) = bv_intRepairChargeID
                .Item(RepairInvoiceUpdateData.EQPMNT_NO) = bv_strEqpmntNo
                .Item(RepairInvoiceUpdateData.GI_TRNSCTN_NO) = bv_strGI_trans_no
                .Item(RepairInvoiceUpdateData.RPR_APPRVL_AMNT_NC) = bv_amount
                .Item(RepairInvoiceUpdateData.APPRVL_RF_NO) = bv_RefNo
                .Item(RepairInvoiceUpdateData.DPT_ID) = bv_intDepotID
                If bv_invoicingPartyId <> 0 Then
                    .Item(RepairInvoiceUpdateData.INVCNG_PRTY_ID) = bv_invoicingPartyId
                Else
                    .Item(RepairInvoiceUpdateData.INVCNG_PRTY_ID) = DBNull.Value
                End If
            End With
            UpdateRepairCharge = objData.UpdateRow(dr, Repair_ChargeUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex

        End Try
    End Function
#End Region

#Region "UpdateRepairEstimateParty"
    Public Function UpdateRepairEstimateParty(ByVal bv_strEqpmntNo As String, _
                                                 ByVal bv_strGI_trans_no As String, _
                                                 ByVal bv_strEstimationNo As String, _
                                                 ByVal bv_amount As Double, _
                                                 ByVal bv_invoicingPartyId As Integer, _
                                                 ByVal bv_modifiedby As String, _
                                                 ByVal bv_modifieddate As Date, _
                                                 ByVal bv_intDepotID As Integer, _
                                                 ByVal bv_strPartyRefNo As String, _
                                                 ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RepairInvoiceUpdateData._REPAIR_ESTIMATE).NewRow()
            With dr
                .Item(RepairInvoiceUpdateData.EQPMNT_NO) = bv_strEqpmntNo
                .Item(RepairInvoiceUpdateData.GI_TRNSCTN_NO) = bv_strGI_trans_no
                .Item(RepairInvoiceUpdateData.RPR_ESTMT_NO) = bv_strEstimationNo
                .Item(RepairInvoiceUpdateData.DPT_ID) = bv_intDepotID
                If bv_strPartyRefNo <> Nothing Then
                    .Item(RepairInvoiceUpdateData.PRTY_APPRVL_RF) = bv_strPartyRefNo
                Else
                    .Item(RepairInvoiceUpdateData.PRTY_APPRVL_RF) = DBNull.Value
                End If
                .Item(RepairInvoiceUpdateData.MDFD_BY) = bv_modifiedby
                .Item(RepairInvoiceUpdateData.MDFD_DT) = bv_modifieddate
            End With
            UpdateRepairEstimateParty = objData.UpdateRow(dr, Repair_EstimateUpdatePartyQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex

        End Try
    End Function
#End Region

#Region "UpdateRepairEstimateCustomer"
    Public Function UpdateRepairEstimateCustomer(ByVal bv_strEqpmntNo As String, _
                                                 ByVal bv_strGI_trans_no As String, _
                                                 ByVal bv_strEstimationNo As String, _
                                                 ByVal bv_amount As Double, _
                                                 ByVal bv_invoicingPartyId As Integer, _
                                                 ByVal bv_modifiedby As String, _
                                                 ByVal bv_modifieddate As Date, _
                                                 ByVal bv_intDepotID As Integer, _
                                                 ByVal bv_strRefNo As String, _
                                                 ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RepairInvoiceUpdateData._REPAIR_ESTIMATE).NewRow()
            With dr
                .Item(RepairInvoiceUpdateData.EQPMNT_NO) = bv_strEqpmntNo
                .Item(RepairInvoiceUpdateData.GI_TRNSCTN_NO) = bv_strGI_trans_no
                .Item(RepairInvoiceUpdateData.RPR_ESTMT_NO) = bv_strEstimationNo
                .Item(RepairInvoiceUpdateData.DPT_ID) = bv_intDepotID
                If bv_strRefNo <> Nothing Then
                    .Item(RepairInvoiceUpdateData.OWNR_APPRVL_RF) = bv_strRefNo
                Else
                    .Item(RepairInvoiceUpdateData.OWNR_APPRVL_RF) = DBNull.Value
                End If
                .Item(RepairInvoiceUpdateData.MDFD_BY) = bv_modifiedby
                .Item(RepairInvoiceUpdateData.MDFD_DT) = bv_modifieddate
            End With
            UpdateRepairEstimateCustomer = objData.UpdateRow(dr, Repair_EstimateUpdateCustomerQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex

        End Try
    End Function
#End Region

#Region "GetRepairChargeDetails() "

    Public Function GetRepairEstimateDetails(ByVal bv_strRepairChargeID As Integer, _
                                           ByVal bv_strEquipmentNo As String,
                                           ByVal bv_strGI_Trans_no As String, _
                                           ByVal bv_strEstimationNo As String, _
                                           ByVal bv_i32DepotID As Int32, _
                                           ByRef br_objTrans As Transactions) As RepairInvoiceUpdateDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(RepairInvoiceUpdateData.RPR_CHRG_ID, bv_strRepairChargeID)
            hshParameters.Add(RepairInvoiceUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(RepairInvoiceUpdateData.DPT_ID, bv_i32DepotID)
            hshParameters.Add(RepairInvoiceUpdateData.GI_TRNSCTN_NO, bv_strGI_Trans_no)
            hshParameters.Add(RepairInvoiceUpdateData.RPR_ESTMT_NO, bv_strEstimationNo)
            objData = New DataObjects(RepairEstimateSelectQueryByDepotId, hshParameters)
            objData.Fill(CType(ds, DataSet), RepairInvoiceUpdateData._V_REPAIR_ESTIMATE, br_objTrans)
            'Return objData.ExecuteScalar()
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRepairChargevaluesForAllDepots"
    Public Function GetRepairChargevaluesAllDepots() As RepairInvoiceUpdateDataSet
        Try
            Try
                objData = New DataObjects(V_REPAIR_CHARGESelectQueryAllDepots)
                objData.Fill(CType(ds, DataSet), RepairInvoiceUpdateData._V_REPAIR_CHARGE)
                Return ds
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class
