Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class CleaningInspections

#Region "Declaration Part.. "
    Dim objData As DataObjects
    'Added IMO_CLSS,UN_NO,NXT_TST_DT,NXT_TST_TYP_CD FOR CR-003 [MMS]
    Private Const V_CLEANING_INSTRUCTIONSelectQueryByDepotID As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,GTN_DT,GI_RF_NO,GI_TRNSCTN_NO,DPT_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,ACTVTY_NAM,ACTVTY_DT,IMO_CLSS,UN_NO,NXT_TST_DT,NXT_TST_TYP_CD,YRD_LCTN,RMRKS_VC,CLNNG_MTHD_TYP_CD,SHELL,STUBE,DPT_CD FROM V_CLEANING_INSTRUCTION WHERE DPT_ID=@DPT_ID"
    'CR- 003 [CLEANING INSTRUCTION RAISED THEN IND_TO_ACN]
    Private Const Activity_StatusUpdateQuery As String = "UPDATE ACTIVITY_STATUS SET EQPMNT_STTS_ID=@EQPMNT_STTS_ID,  ACTVTY_DT=@ACTVTY_DT WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND EQPMNT_NO=@EQPMNT_NO"
    Private ds As CleaningInspectionDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New CleaningInspectionDataSet
    End Sub

#End Region

#Region "GetVCleaningInstruction() TABLE NAME:V_CLEANING_INSTRUCTION"

    Public Function GetVCleaningInstruction(ByVal bv_i32DepotID As Int64) As CleaningInspectionDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CleaningInspectionData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(V_CLEANING_INSTRUCTIONSelectQueryByDepotID, hshParameters)
            objData.Fill(CType(ds, DataSet), CleaningInspectionData._V_CLEANING_INSTRUCTION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateCleaningInspectionNo"

    Public Function UpdateCleaningInstructionNo(ByRef strCleaningInspectionNo As String, _
                                                  ByRef br_objTrans As Transactions) As String
        Try
            Dim intMax As Integer = CommonUIs.GetIdentityValue(CleaningData._CLEANING_INSPECTION, br_objTrans)
            'from index pattern

            Dim objIndexPattern As New IndexPatterns
            'strCleaningInspectionNo = objIndexPattern.GetMaxReferenceNo(String.Concat(CleaningData._CLEANING_INSPECTION, ",", "Cleaning Instruction"), Now, br_objTrans, Nothing, 1)
            ' Cleaning Instruction
            ' strCleaningInspectionNo = CommonUIs.GetIdentityCode(CleaningData._CLEANING_INSPECTION, intMax, Now, br_objTrans)
            If strCleaningInspectionNo Is Nothing Then
                strCleaningInspectionNo = objIndexPattern.GetMaxReferenceNo(String.Concat(CleaningData._CLEANING_INSPECTION, ",", "Cleaning Instruction"), Now, br_objTrans, Nothing, 1)
            End If
            CommonUIs.GetIdentityValue(CleaningData._CLEANING_CERTIFICATE, br_objTrans)
            Return strCleaningInspectionNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateActivityStatus() TABLE NAME:Activity_Status"

    Public Function UpdateActivityStatus(ByVal bv_i64ActivityStatusId As Int64, _
                                         ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_strGateinTransaction As String, _
                                         ByVal bv_datActivityDate As DateTime, _
                                         ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64ActivityStatusId
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateinTransaction
                .Item(CleaningData.ACTVTY_DT) = bv_datActivityDate
            End With
            UpdateActivityStatus = objData.UpdateRow(dr, Activity_StatusUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class
