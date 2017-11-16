
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

Public Class GateOutRequests

#Region "Declaration Part.. "

    Dim objData As DataObjects

    Private Const GetGateoutReuestRecords_SelectQry As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTV_BT,DPT_ID,DPT_CD,STTS_CD,RQST_APPRVL_CNGNE,RQST_APPRVL_RMKRS,GI_TRNSCTN_NO,'FALSE' AS SLCT FROM V_ACTIVITY_STATUS WHERE EQPMNT_STTS_CD IN ('AVL','STR','REJ') AND DPT_ID=@DPT_ID AND GTRQST_DT IS NULL ORDER BY ACTVTY_STTS_ID DESC"
    Private Const UpdateActivity_Status_UpdateQuery As String = "UPDATE ACTIVITY_STATUS SET RQST_APPRVL_CNGNE=@RQST_APPRVL_CNGNE, RQST_APPRVL_RMKRS=@RQST_APPRVL_RMKRS, GTRQST_DT=@GTRQST_DT, GTRQST_BY=@GTRQST_BY WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=GI_TRNSCTN_NO  AND DPT_ID=@DPT_ID"
    Private ds As GateOutRequestDataSet

#End Region

#Region "Constructor.."

    Sub New()
        ds = New GateOutRequestDataSet
    End Sub

#End Region

#Region "GetGateoutReuestRecords() TABLE NAME:Outward_Pass"

    Public Function GetGateoutReuestRecords(ByVal bv_DepotId As Int64) As GateOutRequestDataSet
        Try
            objData = New DataObjects(GetGateoutReuestRecords_SelectQry, GateOutData.DPT_ID, bv_DepotId)
            objData.Fill(CType(ds, DataSet), GateOutRequestData._V_ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function UpdateActivity_Status(ByVal bv_strConsignee As String, ByVal bv_strRemarks As String, _
                                          ByVal bv_strRequestDate As DateTime, ByVal bv_strRequestedBy As String, _
                                          ByVal bv_strEquipmentNo As String, ByVal bv_strGI_Transaction As String, _
                                          ByVal bv_DepoID As Int32, ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutRequestData._ACTIVITY_STATUS).NewRow()
            With dr

                If bv_strConsignee <> Nothing Then
                    .Item(GateOutRequestData.RQST_APPRVL_CNGNE) = bv_strConsignee
                Else
                    .Item(GateOutRequestData.RQST_APPRVL_CNGNE) = DBNull.Value
                End If

                If bv_strRemarks <> Nothing Then
                    .Item(GateOutRequestData.RQST_APPRVL_RMKRS) = bv_strRemarks
                Else
                    .Item(GateOutRequestData.RQST_APPRVL_RMKRS) = DBNull.Value
                End If

                If bv_strRequestDate <> Nothing Then
                    .Item(GateOutRequestData.GTRQST_DT) = bv_strRequestDate
                Else
                    .Item(GateOutRequestData.GTRQST_DT) = DBNull.Value
                End If

                If bv_strRequestedBy <> Nothing Then
                    .Item(GateOutRequestData.GTRQST_BY) = bv_strRequestedBy
                Else
                    .Item(GateOutRequestData.GTRQST_BY) = DBNull.Value
                End If

                If bv_strEquipmentNo <> Nothing Then
                    .Item(GateOutRequestData.EQPMNT_NO) = bv_strEquipmentNo
                Else
                    .Item(GateOutRequestData.EQPMNT_NO) = DBNull.Value
                End If

                If bv_strGI_Transaction <> Nothing Then
                    .Item(GateOutRequestData.GI_TRNSCTN_NO) = bv_strGI_Transaction
                Else
                    .Item(GateOutRequestData.GI_TRNSCTN_NO) = DBNull.Value
                End If

                If bv_DepoID <> Nothing Then
                    .Item(GateOutRequestData.DPT_ID) = bv_DepoID
                Else
                    .Item(GateOutRequestData.DPT_ID) = DBNull.Value
                End If
               


            End With
            UpdateActivity_Status = objData.UpdateRow(dr, UpdateActivity_Status_UpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
