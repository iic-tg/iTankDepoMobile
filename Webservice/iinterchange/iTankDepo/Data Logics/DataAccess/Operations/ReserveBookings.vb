
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

Public Class ReserveBookings

    'Declarations
    Dim objData As DataObjects
    Private Const RESERVEBOOKINGSelectQuery As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING"
    Private Const RESERVEBOOKINGSelectQueryByRSRV_BKNG_ID As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE RSRV_BKNG_ID=@RSRV_BKNG_ID"
    Private Const RESERVEBOOKINGSelectQueryByDT_OF_ACCPTNC As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE DT_OF_ACCPTNC=@DT_OF_ACCPTNC"
    Private Const RESERVEBOOKINGSelectQueryByCSTMR_ID As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE CSTMR_ID=@CSTMR_ID"
    Private Const RESERVEBOOKINGSelectQueryByCSTMR_CD As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE CSTMR_CD=@CSTMR_CD"
    Private Const RESERVEBOOKINGSelectQueryByCNCSGN As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE CNCSGN=@CNCSGN"
    Private Const RESERVEBOOKINGSelectQueryByGT_CNT As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE GT_CNT=@GT_CNT"
    Private Const RESERVEBOOKINGSelectQueryByGOUT_CNT As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE GOUT_CNT=@GOUT_CNT"
    Private Const RESERVEBOOKINGSelectQueryByAVL_CNT As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE AVL_CNT=@AVL_CNT"
    Private Const RESERVEBOOKINGSelectQueryByBKD_QTY As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE BKD_QTY=@BKD_QTY"
    Private Const RESERVEBOOKINGSelectQueryByRSRV_QTY As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE RSRV_QTY=@RSRV_QTY"
    Private Const RESERVEBOOKINGSelectQueryByPNDNG As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE PNDNG=@PNDNG"
    Private Const RESERVEBOOKINGSelectQueryByGOUT_BT As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE GOUT_BT=@GOUT_BT"
    Private Const RESERVEBOOKINGSelectQueryByACTV_BT As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT FROM RESERVEBOOKING WHERE ACTV_BT=@ACTV_BT"
    Private Const RESERVEBOOKINGInsertQuery As String = "INSERT INTO RESERVEBOOKING(RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,ACTV_BT,DPT_ID)VALUES(@RSRV_BKNG_ID,@BKNG_AUTH_NO,@DT_OF_ACCPTNC,@CSTMR_ID,@CSTMR_CD,@CNCSGN,@GT_CNT,@GOUT_CNT,@AVL_CNT,@BKD_QTY,@RSRV_QTY,@PNDNG,@GOUT_BT,@ACTV_BT,@DPT_ID)"
    Private Const RESERVEBOOKINGUpdateQuery As String = "UPDATE RESERVEBOOKING SET RSRV_BKNG_ID=@RSRV_BKNG_ID, BKNG_AUTH_NO=@BKNG_AUTH_NO, DT_OF_ACCPTNC=@DT_OF_ACCPTNC, CSTMR_ID=@CSTMR_ID, CSTMR_CD=@CSTMR_CD, CNCSGN=@CNCSGN, GT_CNT=@GT_CNT, GOUT_CNT=@GOUT_CNT, AVL_CNT=@AVL_CNT, BKD_QTY=@BKD_QTY, RSRV_QTY=@RSRV_QTY, PNDNG=@PNDNG, GOUT_BT=@GOUT_BT, ACTV_BT=@ACTV_BT WHERE RSRV_BKNG_ID=@RSRV_BKNG_ID"
    Private Const RESERVEBOOKINGDeleteQuery As String = "DELETE FROM RESERVEBOOKING WHERE RSRV_BKNG_ID=@RSRV_BKNG_ID"
    Private Const RESERVEBOOKING_DETAILSelectQuery As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL"
    Private Const RESERVEBOOKING_DETAILSelectQueryByRSRV_BKNG_DTL_ID As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE RSRV_BKNG_DTL_ID=@RSRV_BKNG_DTL_ID"
    Private Const RESERVEBOOKING_DETAILSelectQueryByRSRV_BKNG_ID As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE RSRV_BKNG_ID=@RSRV_BKNG_ID"
    Private Const RESERVEBOOKING_DETAILSelectQueryByEQPMNT_NO As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE EQPMNT_NO=@EQPMNT_NO"
    Private Const RESERVEBOOKING_DETAILSelectQueryByEQPMNT_TYP_ID As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private Const RESERVEBOOKING_DETAILSelectQueryByEQPMNT_STTS_ID As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE EQPMNT_STTS_ID=@EQPMNT_STTS_ID"
    Private Const RESERVEBOOKING_DETAILSelectQueryByEQPMNT_STTS_CD As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE EQPMNT_STTS_CD=@EQPMNT_STTS_CD"
    Private Const RESERVEBOOKING_DETAILSelectQueryByCSTMR_ID As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE CSTMR_ID=@CSTMR_ID"
    Private Const RESERVEBOOKING_DETAILSelectQueryByCSTMR_CD As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE CSTMR_CD=@CSTMR_CD"
    Private Const RESERVEBOOKING_DETAILSelectQueryByGI_TRNSCTN_NO As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const RESERVEBOOKING_DETAILSelectQueryByGOUT_BT As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE GOUT_BT=@GOUT_BT"
    Private Const RESERVEBOOKING_DETAILSelectQueryByACTV_BT As String = "SELECT RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT FROM RESERVEBOOKING_DETAIL WHERE ACTV_BT=@ACTV_BT"
    Private Const RESERVEBOOKING_DETAILInsertQuery As String = "INSERT INTO RESERVEBOOKING_DETAIL(RSRV_BKNG_DTL_ID,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,GOUT_BT,ACTV_BT,DPT_ID,BKNG_AUTH_NO,STTS_CD)VALUES(@RSRV_BKNG_DTL_ID,@RSRV_BKNG_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@EQPMNT_STTS_CD,@CSTMR_ID,@CSTMR_CD,@GI_TRNSCTN_NO,@GOUT_BT,@ACTV_BT,@DPT_ID,@BKNG_AUTH_NO,@STTS_CD)"
    Private Const RESERVEBOOKING_DETAILUpdateQuery As String = "UPDATE RESERVEBOOKING_DETAIL SET RSRV_BKNG_DTL_ID=@RSRV_BKNG_DTL_ID, RSRV_BKNG_ID=@RSRV_BKNG_ID, EQPMNT_NO=@EQPMNT_NO, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_STTS_ID=@EQPMNT_STTS_ID, EQPMNT_STTS_CD=@EQPMNT_STTS_CD, CSTMR_ID=@CSTMR_ID, CSTMR_CD=@CSTMR_CD, GI_TRNSCTN_NO=@GI_TRNSCTN_NO, GOUT_BT=@GOUT_BT, ACTV_BT=@ACTV_BT WHERE RSRV_BKNG_DTL_ID=@RSRV_BKNG_DTL_ID"
    Private Const RESERVEBOOKING_DETAILDeleteQuery As String = "DELETE FROM RESERVEBOOKING_DETAIL WHERE RSRV_BKNG_DTL_ID=@RSRV_BKNG_DTL_ID"


    Private Const Get_GateInCountByCustomerId_SelectQry As String = "SELECT COUNT(ACTVTY_STTS_ID) FROM V_ACTIVITY_STATUS WHERE CSTMR_ID =@CSTMR_ID AND DPT_ID=@DPT_ID AND EQPMNT_STTS_CD in('STR','INS')"
    Private Const Get_GateInAvailableCountByCustomerId_SelectQry As String = "SELECT COUNT(ACTVTY_STTS_ID) FROM V_ACTIVITY_STATUS WHERE CSTMR_ID =@CSTMR_ID AND DPT_ID=@DPT_ID  AND EQPMNT_STTS_CD IN ('AVL','STR')"
    Private Const Get_EquipmentsFromActivityStatus_SelectQry As String = "SELECT ACTVTY_STTS_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_CD,GI_TRNSCTN_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,STTS_CD,'FALSE' AS SLCT FROM V_ACTIVITY_STATUS WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND BKNG_AUTH_NO IS NULL AND EQPMNT_STTS_CD IN('AVL','STR')"

    Private Const Update_AuthNo_ActivityStatus_UpdateQry As String = "UPDATE ACTIVITY_STATUS SET BKNG_AUTH_NO=@BKNG_AUTH_NO WHERE EQPMNT_NO=@EQPMNT_NO AND CSTMR_ID=@CSTMR_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"

    Private Const GetSubmited_RESERVEBOOKINGDetailActiveRecords_SelectQry As String = "SELECT RSRV_BKNG_DTL_ID AS ACTVTY_STTS_ID ,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,(SELECT EQPMNT_TYP_CD FROM EQUIPMENT_TYPE ET WHERE ET.EQPMNT_TYP_ID =RD.EQPMNT_TYP_ID) EQPMNT_TYP_CD,(SELECT EQPMNT_CD_CD FROM EQUIPMENT_TYPE ET WHERE ET.EQPMNT_TYP_ID =RD.EQPMNT_TYP_ID) EQPMNT_CD_CD,EQPMNT_STTS_CD, STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,BKNG_AUTH_NO,GOUT_BT,DPT_ID,ACTV_BT AS SLCT FROM RESERVEBOOKING_DETAIL RD WHERE RD.RSRV_BKNG_ID=@RSRV_BKNG_ID AND RD.DPT_ID =@DPT_ID"
    Private Const GetSubmited_RESERVEBOOKINGDetailInActiveRecords_SelectQry As String = "SELECT RSRV_BKNG_DTL_ID AS ACTVTY_STTS_ID ,RSRV_BKNG_ID,EQPMNT_NO,EQPMNT_TYP_ID,(SELECT EQPMNT_TYP_CD FROM EQUIPMENT_TYPE ET WHERE ET.EQPMNT_TYP_ID =RD.EQPMNT_TYP_ID) EQPMNT_TYP_CD,(SELECT EQPMNT_CD_CD FROM EQUIPMENT_TYPE ET WHERE ET.EQPMNT_TYP_ID =RD.EQPMNT_TYP_ID) EQPMNT_CD_CD,EQPMNT_STTS_CD, STTS_CD,CSTMR_ID,CSTMR_CD,GI_TRNSCTN_NO,BKNG_AUTH_NO,GOUT_BT,DPT_ID,ACTV_BT AS SLCT FROM RESERVEBOOKING_DETAIL WHERE RSRV_BKNG_ID=@RSRV_BKNG_ID AND ACTV_BT =0 and GOUT_BT=0 AND DPT_ID =@DPT_ID"
    Private Const Delete_InActiveRecordsforDifferentAuthNo_DeleteQry As String = "DELETE FROM RESERVEBOOKING_DETAIL  WHERE EQPMNT_NO =@EQPMNT_NO AND GI_TRNSCTN_NO =@GI_TRNSCTN_NO AND CSTMR_ID =@CSTMR_ID AND BKNG_AUTH_NO <> @BKNG_AUTH_NO AND DPT_ID=@DPT_ID AND ACTV_BT=0"
    Private Const GetRESERVEBOOKINGById_SelectQry As String = "SELECT RSRV_BKNG_ID,BKNG_AUTH_NO,DT_OF_ACCPTNC,CSTMR_ID,CSTMR_CD,CNCSGN,GT_CNT,GOUT_CNT,AVL_CNT,BKD_QTY,RSRV_QTY,PNDNG,GOUT_BT,DPT_ID,ACTV_BT FROM RESERVEBOOKING WHERE RSRV_BKNG_ID=@RSRV_BKNG_ID AND DPT_ID=@DPT_ID"
    Private Const Update_ReserveBooking_Qry As String = "UPDATE RESERVEBOOKING SET AVL_CNT =@AVL_CNT, GT_CNT=@GT_CNT, BKD_QTY =@BKD_QTY, RSRV_QTY =@RSRV_QTY,PNDNG =@PNDNG,BKNG_AUTH_NO=@BKNG_AUTH_NO,DT_OF_ACCPTNC=@DT_OF_ACCPTNC,CNCSGN=@CNCSGN WHERE RSRV_BKNG_ID=@RSRV_BKNG_ID AND DPT_ID=@DPT_ID "
    Private Const Update_ReserveBookingDetails_Qry As String = "UPDATE RESERVEBOOKING_DETAIL SET ACTV_BT=@ACTV_BT WHERE EQPMNT_NO =@EQPMNT_NO AND RSRV_BKNG_ID =@RSRV_BKNG_ID AND CSTMR_ID =@CSTMR_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND BKNG_AUTH_NO=@BKNG_AUTH_NO AND DPT_ID =@DPT_ID"

    'From GateOut
    Private Const Update_ReserveBookingFromGateOut_Qry As String = "UPDATE RESERVEBOOKING_DETAIL SET GOUT_BT=@GOUT_BT WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID AND CSTMR_ID=@CSTMR_ID AND ACTV_BT=1 AND BKNG_AUTH_NO IS NOT NULL"
    Private Const Delete_ReserveBookingFromGateOut_Qry As String = "DELETE RESERVEBOOKING_DETAIL  WHERE EQPMNT_NO =@EQPMNT_NO AND GI_TRNSCTN_NO= @GI_TRNSCTN_NO AND DPT_ID =@DPT_ID  AND ACTV_BT =0 AND BKNG_AUTH_NO IS NOT NULL"

    Private ds As ReserveBookingDataSet


#Region "Constructor.."

    Sub New()
        ds = New ReserveBookingDataSet
    End Sub

#End Region


    Public Function GetSubmited_ReserveDetail(ByVal bv_RSRV_BKNG_ID As Int64, ByVal bv_DPT_ID As Int32) As DataTable
        Try
            Dim dt As New DataTable
            Dim hshParam As New Hashtable
            hshParam.Add(ReserveBookingData.RSRV_BKNG_ID, bv_RSRV_BKNG_ID)
            hshParam.Add(ReserveBookingData.DPT_ID, bv_DPT_ID)

            objData = New DataObjects(GetSubmited_RESERVEBOOKINGDetailActiveRecords_SelectQry, hshParam)
            objData.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetSubmited_RESERVEBOOKINGDetail_InActiveRecords(ByVal bv_RSRV_BKNG_ID As Int64, ByVal bv_DPT_ID As Int32) As DataTable
        Try
            Dim dt As New DataTable
            Dim hshParam As New Hashtable
            hshParam.Add(ReserveBookingData.RSRV_BKNG_ID, bv_RSRV_BKNG_ID)
            hshParam.Add(ReserveBookingData.DPT_ID, bv_DPT_ID)

            objData = New DataObjects(GetSubmited_RESERVEBOOKINGDetailInActiveRecords_SelectQry, hshParam)
            objData.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Delete the In Active Records for Different Auth No for same Customer and GI_transactions
    Public Function Delete_InActiveRecordsforDifferentAuthNo(ByVal bv_strEquipmentNo As String, _
                                                             ByVal bv_strGI_Transaction As String, _
                                                             ByVal bv_strCustomerId As String, _
                                                             ByVal bv_strBookingAuthNo As String, _
                                                             ByVal bv_DPT_ID As Int32, _
                                                             ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ReserveBookingData._RESERVEBOOKING_DETAIL).NewRow()
            With dr

                .Item(ReserveBookingData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(ReserveBookingData.GI_TRNSCTN_NO) = bv_strGI_Transaction
                .Item(ReserveBookingData.CSTMR_ID) = bv_strCustomerId
                .Item(ReserveBookingData.BKNG_AUTH_NO) = bv_strBookingAuthNo
                .Item(ReserveBookingData.DPT_ID) = bv_DPT_ID

            End With

            Delete_InActiveRecordsforDifferentAuthNo = objData.DeleteRow(dr, Delete_InActiveRecordsforDifferentAuthNo_DeleteQry, objTrans)
            dr = Nothing

        Catch ex As Exception

        End Try
    End Function


#Region "GetRESERVEBOOKING() TABLE NAME:RESERVEBOOKING"

    Public Function GetRESERVEBOOKING() As ReserveBookingDataSet
        Try
            objData = New DataObjects(RESERVEBOOKINGSelectQuery)
            objData.Fill(CType(ds, DataSet), ReserveBookingData._RESERVEBOOKING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRESERVEBOOKINGByBKNG_AUTH_NO() TABLE NAME:RESERVEBOOKING"

    Public Function GetRESERVEBOOKINGById(ByVal bv_strReservebookingId As Int64, ByVal bv_strDepoId As Int32) As DataTable
        Try
            Dim hshParam As New Hashtable
            hshParam.Add(ReserveBookingData.RSRV_BKNG_ID, bv_strReservebookingId)
            hshParam.Add(ReserveBookingData.DPT_ID, bv_strDepoId)

            Dim dt As New DataTable
            objData = New DataObjects(GetRESERVEBOOKINGById_SelectQry, hshParam)
            objData.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateRESERVEBOOKING() TABLE NAME:RESERVEBOOKING"

    Public Function CreateRESERVEBOOKING(ByVal bv_strBKNG_AUTH_NO As String, _
        ByVal bv_datDT_OF_ACCPTNC As DateTime, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_strCSTMR_CD As String, _
        ByVal bv_strCNCSGN As String, _
        ByVal bv_i32GT_CNT As Int32, _
        ByVal bv_i32GOUT_CNT As Int32, _
        ByVal bv_i32AVL_CNT As Int32, _
        ByVal bv_i32BKD_QTY As Int32, _
        ByVal bv_i32RSRV_QTY As Int32, _
        ByVal bv_i32PNDNG As Int32, _
        ByVal bv_blnGOUT_BT As Boolean, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_DepoId As Int32, _
        ByRef objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ReserveBookingData._RESERVEBOOKING).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ReserveBookingData._RESERVEBOOKING)
                .Item(ReserveBookingData.RSRV_BKNG_ID) = intMax
                .Item(ReserveBookingData.BKNG_AUTH_NO) = bv_strBKNG_AUTH_NO
                .Item(ReserveBookingData.DT_OF_ACCPTNC) = bv_datDT_OF_ACCPTNC
                .Item(ReserveBookingData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(ReserveBookingData.CSTMR_CD) = bv_strCSTMR_CD
                If bv_strCNCSGN <> Nothing Then
                    .Item(ReserveBookingData.CNCSGN) = bv_strCNCSGN
                Else
                    .Item(ReserveBookingData.CNCSGN) = DBNull.Value
                End If
                .Item(ReserveBookingData.GT_CNT) = bv_i32GT_CNT
                .Item(ReserveBookingData.GOUT_CNT) = bv_i32GOUT_CNT
                .Item(ReserveBookingData.AVL_CNT) = bv_i32AVL_CNT
                .Item(ReserveBookingData.BKD_QTY) = bv_i32BKD_QTY
                .Item(ReserveBookingData.RSRV_QTY) = bv_i32RSRV_QTY
                .Item(ReserveBookingData.PNDNG) = bv_i32PNDNG
                .Item(ReserveBookingData.GOUT_BT) = bv_blnGOUT_BT
                .Item(ReserveBookingData.ACTV_BT) = bv_blnACTV_BT
                .Item(ReserveBookingData.DPT_ID) = bv_DepoId
            End With
            objData.InsertRow(dr, RESERVEBOOKINGInsertQuery, objTrans)
            dr = Nothing
            CreateRESERVEBOOKING = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    Public Function Update_AuthNo_ActivityStatus(ByVal bv_strBookingAuthNo As String, ByVal bv_strEquipmentNo As String, _
                                                 ByVal bv_strCustomerId As String, ByVal bv_strGI_Transaction As String, _
                                                 ByVal bv_DepoId As String, ByRef objTrans As Transactions)
        Try

            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ReserveBookingData._ACTIVITY_STATUS).NewRow()

            With dr

                If bv_strBookingAuthNo <> Nothing Then
                    .Item(ReserveBookingData.BKNG_AUTH_NO) = bv_strBookingAuthNo
                Else
                    .Item(ReserveBookingData.BKNG_AUTH_NO) = DBNull.Value
                End If

                .Item(ReserveBookingData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(ReserveBookingData.CSTMR_ID) = bv_strCustomerId
                .Item(ReserveBookingData.GI_TRNSCTN_NO) = bv_strGI_Transaction
                .Item(ReserveBookingData.DPT_ID) = bv_DepoId

               

            End With

            Update_AuthNo_ActivityStatus = objData.UpdateRow(dr, Update_AuthNo_ActivityStatus_UpdateQry, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function




#Region "UpdateRESERVEBOOKING() TABLE NAME:RESERVEBOOKING"

    Public Function UpdateRESERVEBOOKING(ByVal bv_i64RSRV_BKNG_ID As Int64, _
        ByVal bv_strBKNG_AUTH_NO As String, _
        ByVal bv_datDT_OF_ACCPTNC As DateTime, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_strCSTMR_CD As String, _
        ByVal bv_strCNCSGN As String, _
        ByVal bv_i32GT_CNT As Int32, _
        ByVal bv_i32GOUT_CNT As Int32, _
        ByVal bv_i32AVL_CNT As Int32, _
        ByVal bv_i32BKD_QTY As Int32, _
        ByVal bv_i32RSRV_QTY As Int32, _
        ByVal bv_i32PNDNG As Int32, _
        ByVal bv_blnGOUT_BT As Boolean, _
        ByVal bv_blnACTV_BT As Boolean) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ReserveBookingData._RESERVEBOOKING).NewRow()
            With dr
                .Item(ReserveBookingData.RSRV_BKNG_ID) = bv_i64RSRV_BKNG_ID
                .Item(ReserveBookingData.BKNG_AUTH_NO) = bv_strBKNG_AUTH_NO
                .Item(ReserveBookingData.DT_OF_ACCPTNC) = bv_datDT_OF_ACCPTNC
                .Item(ReserveBookingData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(ReserveBookingData.CSTMR_CD) = bv_strCSTMR_CD
                If bv_strCNCSGN <> Nothing Then
                    .Item(ReserveBookingData.CNCSGN) = bv_strCNCSGN
                Else
                    .Item(ReserveBookingData.CNCSGN) = DBNull.Value
                End If
                .Item(ReserveBookingData.GT_CNT) = bv_i32GT_CNT
                .Item(ReserveBookingData.GOUT_CNT) = bv_i32GOUT_CNT
                .Item(ReserveBookingData.AVL_CNT) = bv_i32AVL_CNT
                .Item(ReserveBookingData.BKD_QTY) = bv_i32BKD_QTY
                .Item(ReserveBookingData.RSRV_QTY) = bv_i32RSRV_QTY
                .Item(ReserveBookingData.PNDNG) = bv_i32PNDNG
                .Item(ReserveBookingData.GOUT_BT) = bv_blnGOUT_BT
                .Item(ReserveBookingData.ACTV_BT) = bv_blnACTV_BT
            End With
            UpdateRESERVEBOOKING = objData.UpdateRow(dr, RESERVEBOOKINGUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteRESERVEBOOKING() TABLE NAME:RESERVEBOOKING"

    Public Function DeleteRESERVEBOOKING(ByVal bv_RSRV_BKNG_ID As Int64) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(ReserveBookingData._RESERVEBOOKING).NewRow()
            With dr
                .Item(ReserveBookingData.RSRV_BKNG_ID) = bv_RSRV_BKNG_ID
            End With
            DeleteRESERVEBOOKING = objData.DeleteRow(dr, RESERVEBOOKINGDeleteQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRESERVEBOOKING_DETAIL() TABLE NAME:RESERVEBOOKING_DETAIL"

    Public Function GetRESERVEBOOKING_DETAIL() As ReserveBookingDataSet
        Try
            objData = New DataObjects(RESERVEBOOKING_DETAILSelectQuery)
            objData.Fill(CType(ds, DataSet), ReserveBookingData._RESERVEBOOKING_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRESERVEBOOKING_DETAILByRSRV_BKNG_ID() TABLE NAME:RESERVEBOOKING_DETAIL"

    Public Function GetRESERVEBOOKING_DETAILByRSRV_BKNG_ID(ByVal bv_i64RSRV_BKNG_ID As Int64) As ReserveBookingDataSet
        Try
            objData = New DataObjects(RESERVEBOOKING_DETAILSelectQueryByRSRV_BKNG_ID, ReserveBookingData.RSRV_BKNG_ID, CStr(bv_i64RSRV_BKNG_ID))
            objData.Fill(CType(ds, DataSet), ReserveBookingData._RESERVEBOOKING_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRESERVEBOOKING_DETAILByEQPMNT_NO() TABLE NAME:RESERVEBOOKING_DETAIL"

    Public Function GetRESERVEBOOKING_DETAILByEQPMNT_NO(ByVal bv_strEQPMNT_NO As String) As ReserveBookingDataSet
        Try
            objData = New DataObjects(RESERVEBOOKING_DETAILSelectQueryByEQPMNT_NO, ReserveBookingData.EQPMNT_NO, bv_strEQPMNT_NO)
            objData.Fill(CType(ds, DataSet), ReserveBookingData._RESERVEBOOKING_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateRESERVEBOOKING_DETAIL() TABLE NAME:RESERVEBOOKING_DETAIL"

    Public Function CreateRESERVEBOOKING_DETAIL(ByVal bv_i64RSRV_BKNG_ID As Int64, _
        ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_i64EQPMNT_TYP_ID As Int64, _
        ByVal bv_i32EQPMNT_STTS_ID As Int32, _
        ByVal bv_strEQPMNT_STTS_CD As String, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_strCSTMR_CD As String, _
        ByVal bv_strGI_TRNSCTN_NO As String, _
        ByVal bv_strBookingAuthNo As String, _
        ByVal bv_strStatusCode As String, _
        ByVal bv_blnGOUT_BT As Boolean, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_DepoId As Int32, _
        ByRef objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ReserveBookingData._RESERVEBOOKING_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ReserveBookingData._RESERVEBOOKING_DETAIL)
                .Item(ReserveBookingData.RSRV_BKNG_DTL_ID) = intMax
                .Item(ReserveBookingData.RSRV_BKNG_ID) = bv_i64RSRV_BKNG_ID
                .Item(ReserveBookingData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(ReserveBookingData.EQPMNT_TYP_ID) = bv_i64EQPMNT_TYP_ID
                '.Item(ReserveBookingData.EQPMNT_STTS_ID) = bv_i32EQPMNT_STTS_ID
                .Item(ReserveBookingData.EQPMNT_STTS_CD) = bv_strEQPMNT_STTS_CD
                .Item(ReserveBookingData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(ReserveBookingData.CSTMR_CD) = bv_strCSTMR_CD
                .Item(ReserveBookingData.GI_TRNSCTN_NO) = bv_strGI_TRNSCTN_NO

                'If bv_blnACTV_BT = True Then
                '    .Item(ReserveBookingData.BKNG_AUTH_NO) = bv_strBookingAuthNo
                'Else
                '    .Item(ReserveBookingData.BKNG_AUTH_NO) = DBNull.Value

                'End If
                .Item(ReserveBookingData.STTS_CD) = bv_strStatusCode
                .Item(ReserveBookingData.BKNG_AUTH_NO) = bv_strBookingAuthNo

                .Item(ReserveBookingData.GOUT_BT) = bv_blnGOUT_BT
                .Item(ReserveBookingData.ACTV_BT) = bv_blnACTV_BT
                .Item(ReserveBookingData.DPT_ID) = bv_DepoId
            End With
            objData.InsertRow(dr, RESERVEBOOKING_DETAILInsertQuery, objTrans)
            dr = Nothing
            CreateRESERVEBOOKING_DETAIL = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateRESERVEBOOKING_DETAIL() TABLE NAME:RESERVEBOOKING_DETAIL"

    'Public Function UpdateRESERVEBOOKING_DETAIL(ByVal bv_i64RSRV_BKNG_DTL_ID As Int64, _
    '    ByVal bv_i64RSRV_BKNG_ID As Int64, _
    '    ByVal bv_strEQPMNT_NO As String, _
    '    ByVal bv_i64EQPMNT_TYP_ID As Int64, _
    '    ByVal bv_i32EQPMNT_STTS_ID As Int32, _
    '    ByVal bv_strEQPMNT_STTS_CD As String, _
    '    ByVal bv_i64CSTMR_ID As Int64, _
    '    ByVal bv_strCSTMR_CD As String, _
    '    ByVal bv_strGI_TRNSCTN_NO As String, _
    '    ByVal bv_blnGOUT_BT As Boolean, _
    '    ByVal bv_blnACTV_BT As Boolean) As Boolean
    '    Try
    '        Dim dr As DataRow
    '        objData = New DataObjects()
    '        dr = ds.Tables(ReserveBookingData._RESERVEBOOKING_DETAIL).NewRow()
    '        With dr
    '            .Item(ReserveBookingData.RSRV_BKNG_DTL_ID) = bv_i64RSRV_BKNG_DTL_ID
    '            .Item(ReserveBookingData.RSRV_BKNG_ID) = bv_i64RSRV_BKNG_ID
    '            .Item(ReserveBookingData.EQPMNT_NO) = bv_strEQPMNT_NO
    '            .Item(ReserveBookingData.EQPMNT_TYP_ID) = bv_i64EQPMNT_TYP_ID
    '            .Item(ReserveBookingData.EQPMNT_STTS_ID) = bv_i32EQPMNT_STTS_ID
    '            .Item(ReserveBookingData.EQPMNT_STTS_CD) = bv_strEQPMNT_STTS_CD
    '            .Item(ReserveBookingData.CSTMR_ID) = bv_i64CSTMR_ID
    '            .Item(ReserveBookingData.CSTMR_CD) = bv_strCSTMR_CD
    '            .Item(ReserveBookingData.GI_TRNSCTN_NO) = bv_strGI_TRNSCTN_NO
    '            .Item(ReserveBookingData.GOUT_BT) = bv_blnGOUT_BT
    '            .Item(ReserveBookingData.ACTV_BT) = bv_blnACTV_BT
    '        End With
    '        UpdateRESERVEBOOKING_DETAIL = objData.UpdateRow(dr, RESERVEBOOKING_DETAILUpdateQuery)
    '        dr = Nothing
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
#End Region

#Region "DeleteRESERVEBOOKING_DETAIL() TABLE NAME:RESERVEBOOKING_DETAIL"

    'Public Function DeleteRESERVEBOOKING_DETAIL(ByVal bv_RSRV_BKNG_DTL_ID As Int64) As Boolean

    '    Dim dr As DataRow
    '    objData = New DataObjects()
    '    Try
    '        dr = ds.Tables(ReserveBookingData._RESERVEBOOKING_DETAIL).NewRow()
    '        With dr
    '            .Item(ReserveBookingData.RSRV_BKNG_DTL_ID) = bv_RSRV_BKNG_DTL_ID
    '        End With
    '        DeleteRESERVEBOOKING_DETAIL = objData.DeleteRow(dr, RESERVEBOOKING_DETAILDeleteQuery)
    '        dr = Nothing
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
#End Region

    Public Function Get_GateInCountByCustomerId(ByVal bv_Dpt_Id As String, ByVal bv_CustomerId As String) As Int32
        Try
            Dim hshParam As New Hashtable
            hshParam.Add(ReserveBookingData.DPT_ID, bv_Dpt_Id)
            hshParam.Add(ReserveBookingData.CSTMR_ID, bv_CustomerId)

            objData = New DataObjects(Get_GateInCountByCustomerId_SelectQry, hshParam)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_GateInAvailableCountByCustomerId(ByVal bv_Dpt_Id As String, ByVal bv_CustomerId As String) As Int32
        Try
            Dim hshParam As New Hashtable
            hshParam.Add(ReserveBookingData.DPT_ID, bv_Dpt_Id)
            hshParam.Add(ReserveBookingData.CSTMR_ID, bv_CustomerId)

            objData = New DataObjects(Get_GateInAvailableCountByCustomerId_SelectQry, hshParam)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Get_EquipmentsFromActivityStatus(ByVal bv_Dpt_Id As String, ByVal bv_CustomerId As String) As ReserveBookingDataSet
        Try
            Dim hshParam As New Hashtable
            hshParam.Add(ReserveBookingData.DPT_ID, bv_Dpt_Id)
            hshParam.Add(ReserveBookingData.CSTMR_ID, bv_CustomerId)

            objData = New DataObjects(Get_EquipmentsFromActivityStatus_SelectQry, hshParam)
            objData.Fill(CType(ds, DataSet), ReserveBookingData._V_ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Update_ReserveBooking(ByVal ReserveBookingId As String, _
                                          ByVal intAvailbleCount As Int32, _
                                          ByVal intGateInCount As Int32, _
                                          ByVal intBookedQty As Int32, _
                                          ByVal intReserveQty As Int32, _
                                          ByVal intPendingQty As Int32, _
                                          ByVal bv_strBookingAuthNo As String, _
                                          ByVal bv_DateOfAcceptance As Date, _
                                          ByVal bv_Consinee As String, _
                                          ByVal bv_DepoId As String, _
                                          ByRef objTrans As Transactions)
        Try

            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ReserveBookingData._RESERVEBOOKING).NewRow()

            With dr

                .Item(ReserveBookingData.RSRV_BKNG_ID) = ReserveBookingId
                .Item(ReserveBookingData.AVL_CNT) = intAvailbleCount
                .Item(ReserveBookingData.GT_CNT) = intGateInCount
                .Item(ReserveBookingData.BKD_QTY) = intBookedQty
                .Item(ReserveBookingData.RSRV_QTY) = intReserveQty
                .Item(ReserveBookingData.PNDNG) = intPendingQty
                .Item(ReserveBookingData.BKNG_AUTH_NO) = bv_strBookingAuthNo
                .Item(ReserveBookingData.DT_OF_ACCPTNC) = bv_DateOfAcceptance
                .Item(ReserveBookingData.CNCSGN) = bv_Consinee
                .Item(ReserveBookingData.DPT_ID) = bv_DepoId

            End With

            Update_ReserveBooking = objData.UpdateRow(dr, Update_ReserveBooking_Qry, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Update_ReserveBookingDetails(ByVal ReserveBookingId As String, _
                                         ByVal strEquipmentNo As String, _
                                         ByVal strCustomerId As String, _
                                         ByVal strGI_Transaction As String, _
                                         ByVal bv_strBookingAuthNo As String, _
                                         ByVal bv_ActiveBit As Boolean, _
                                         ByVal bv_DepoId As String, _
                                         ByRef objTrans As Transactions)
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ReserveBookingData._RESERVEBOOKING_DETAIL).NewRow()

            With dr

                .Item(ReserveBookingData.RSRV_BKNG_ID) = ReserveBookingId
                .Item(ReserveBookingData.EQPMNT_NO) = strEquipmentNo
                .Item(ReserveBookingData.CSTMR_ID) = strCustomerId
                .Item(ReserveBookingData.GI_TRNSCTN_NO) = strGI_Transaction
                .Item(ReserveBookingData.BKNG_AUTH_NO) = bv_strBookingAuthNo
                .Item(ReserveBookingData.ACTV_BT) = bv_ActiveBit
                .Item(ReserveBookingData.DPT_ID) = bv_DepoId

            End With

            Update_ReserveBookingDetails = objData.UpdateRow(dr, Update_ReserveBookingDetails_Qry, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Update_ReserveBookingFromGateOut(ByVal bv_strEquipmentNo As String, _
                                             ByVal bv_strCustomerId As String, ByVal bv_strGI_Transaction As String, _
                                             ByVal bv_DepoId As String, ByVal bv_Gout_Bit As Boolean, ByRef objTrans As Transactions)
        Try

            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ReserveBookingData._RESERVEBOOKING_DETAIL).NewRow()

            With dr

                .Item(ReserveBookingData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(ReserveBookingData.CSTMR_ID) = bv_strCustomerId
                .Item(ReserveBookingData.GI_TRNSCTN_NO) = bv_strGI_Transaction
                .Item(ReserveBookingData.DPT_ID) = bv_DepoId
                If bv_Gout_Bit = True Then
                    .Item(ReserveBookingData.GOUT_BT) = 1
                Else
                    .Item(ReserveBookingData.GOUT_BT) = 0
                End If


            End With

            Update_ReserveBookingFromGateOut = objData.UpdateRow(dr, Update_ReserveBookingFromGateOut_Qry, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Delete_ReserveBookingFromGateOut(ByVal bv_strEquipmentNo As String, _
                                                     ByVal bv_strGI_Transaction As String, _
                                                     ByVal bv_DepoId As String, ByRef objTrans As Transactions)
        Try

            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ReserveBookingData._RESERVEBOOKING_DETAIL).NewRow()

            With dr

                .Item(ReserveBookingData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(ReserveBookingData.GI_TRNSCTN_NO) = bv_strGI_Transaction
                .Item(ReserveBookingData.DPT_ID) = bv_DepoId

            End With

            Delete_ReserveBookingFromGateOut = objData.DeleteRow(dr, Delete_ReserveBookingFromGateOut_Qry, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
