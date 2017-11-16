#Region " TariffCodes.vb"
'*********************************************************************************************************************
'Name :
'      TariffCodes.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(TariffCodes.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      10/10/2013 2:02:54 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "TariffCodes"

Public Class TariffCodes

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const TariffCodeSelectQueryByTariffCode As String = "SELECT TRFF_CD_ID,TRFF_CD_CD,TRFF_CD_DESCRPTN_VC,ITM_ID,(SELECT ITM_CD FROM ITEM WHERE ITM_ID=TC.ITM_ID)AS ITM_CD,SB_ITM_ID,(SELECT SB_ITM_CD FROM SUB_ITEM WHERE SB_ITM_ID=TC.SB_ITM_ID)AS SB_ITM_CD,DMG_ID,(SELECT DMG_CD FROM DAMAGE WHERE DMG_ID=TC.DMG_ID)AS DMG_CD,RPR_ID,(SELECT RPR_CD FROM REPAIR WHERE RPR_ID=TC.RPR_ID)AS RPR_CD,MN_HR,MTRL_CST,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM TARIFF_CODE AS TC WHERE ( DPT_ID=@DPT_ID AND TRFF_CD_CD=@TRFF_CD_CD)"
    Private Const Tariff_CodeSelectQueryPk As String = "SELECT TRFF_CD_ID,TRFF_CD_CD,TRFF_CD_DESCRPTN_VC,ITM_ID,(SELECT ITM_CD FROM ITEM WHERE ITM_ID=TC.ITM_ID)AS ITM_CD,SB_ITM_ID,(SELECT SB_ITM_CD FROM SUB_ITEM WHERE SB_ITM_ID=TC.SB_ITM_ID)AS SB_ITM_CD,DMG_ID,(SELECT DMG_CD FROM DAMAGE WHERE DMG_ID=TC.DMG_ID)AS DMG_CD,RPR_ID,(SELECT RPR_CD FROM REPAIR WHERE RPR_ID=TC.RPR_ID)AS RPR_CD,MN_HR,MTRL_CST,RMRKS_VC,ACTV_BT,DPT_ID FROM TARIFF_CODE AS TC WHERE DPT_ID=@DPT_ID"
    Private Const Tariff_CodeInsertQuery As String = "INSERT INTO TARIFF_CODE(TRFF_CD_ID,TRFF_CD_CD,TRFF_CD_DESCRPTN_VC,ITM_ID,SB_ITM_ID,DMG_ID,RPR_ID,MN_HR,MTRL_CST,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@TRFF_CD_ID,@TRFF_CD_CD,@TRFF_CD_DESCRPTN_VC,@ITM_ID,@SB_ITM_ID,@DMG_ID,@RPR_ID,@MN_HR,@MTRL_CST,@RMRKS_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const Tariff_CodeUpdateQuery As String = "UPDATE TARIFF_CODE SET TRFF_CD_CD=@TRFF_CD_CD, TRFF_CD_DESCRPTN_VC=@TRFF_CD_DESCRPTN_VC, ITM_ID=@ITM_ID, SB_ITM_ID=@SB_ITM_ID, DMG_ID=@DMG_ID, RPR_ID=@RPR_ID, MN_HR=@MN_HR, MTRL_CST=@MTRL_CST, RMRKS_VC=@RMRKS_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE TRFF_CD_ID=@TRFF_CD_ID"
    Private Const Tariff_CodeDeleteQuery As String = "DELETE FROM TARIFF_CODE WHERE TRFF_CD_ID=@TRFF_CD_ID"
    Private Const Tariff_GroupSelectQueryByTariffCode As String = "SELECT TRFF_GRP_DTL_ID,TRFF_CD_ID,TRFF_CD_CD,TRFF_CD_DESCRPTN_VC,TRFF_GRP_ID,TRFF_GRP_CD,TRFF_GRP_DESCRPTN_VC,RMRKS_VC,ACTV_BT FROM V_TARIFF_GROUP_DETAIL WHERE TRFF_CD_ID = @TRFF_CD_ID AND ACTV_BT =@ACTV_BT"
    Private Const Tariff_GroupDetailDeleteQuery As String = "DELETE FROM TARIFF_GROUP_DETAIL WHERE TRFF_CD_ID=@TRFF_CD_ID AND ACTV_BT = 0"

    'For GWS
    Private Const GetTariffCodeByCustomerID_SelectQry As String = "SELECT TRFF_CD_DTL_ID,TRFF_CD_DTL_CD,TRFF_CD_DTL_DSC,DTL_TYP FROM V_TARIFF_CODE_DETAIL WHERE ACTV_BT=1 AND TRFF_CD_DTL_ACTV_BT=1 AND DTL_TYP='CUSTOMER' AND TRFF_CD_CSTMR_ID=@TRFF_CD_CSTMR_ID AND TRFF_CD_EQP_TYP_ID=@TRFF_CD_EQP_TYP_ID AND DPT_ID=@DPT_ID"
    Private Const GetTariffCodeByAgentID_SelectQry As String = "SELECT TRFF_CD_DTL_ID,TRFF_CD_DTL_CD,TRFF_CD_DTL_DSC,DTL_TYP FROM V_TARIFF_CODE_DETAIL WHERE ACTV_BT=1 AND TRFF_CD_DTL_ACTV_BT=1 AND DTL_TYP='AGENT' AND TRFF_CD_AGNT_ID=TRFF_CD_AGNT_ID AND TRFF_CD_EQP_TYP_ID=@TRFF_CD_EQP_TYP_ID AND DPT_ID=@DPT_ID"
    Private Const GetStandardTariffCode_SelectQry As String = "SELECT TRFF_CD_DTL_ID,TRFF_CD_DTL_CD,TRFF_CD_DTL_DSC,DTL_TYP FROM V_TARIFF_CODE_DETAIL WHERE ACTV_BT=1 AND TRFF_CD_DTL_ACTV_BT=1 AND DTL_TYP='STANDARD' AND TRFF_CD_CSTMR_ID=0 AND TRFF_CD_AGNT_ID=0 AND TRFF_CD_EQP_TYP_ID=@TRFF_CD_EQP_TYP_ID AND DPT_ID=@DPT_ID"

    Private ds As TariffCodeDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New TariffCodeDataSet
    End Sub

#End Region

#Region "GetTariffCodeBy() TABLE NAME:Tariff_Code"

    Public Function GetTariffCodeByDepotID(ByVal bv_DepotID As Int64) As TariffCodeDataSet
        Try
            objData = New DataObjects(Tariff_CodeSelectQueryPk, TariffCodeData.DPT_ID, CStr(bv_DepotID))
            objData.Fill(CType(ds, DataSet), TariffCodeData._TARIFF_CODE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTariffCodeByTariffCodeCode() TABLE NAME:Sub_Item"

    Public Function GetTariffCodeByTariffCodeCode(ByVal bv_strTariffCodeCode As String, ByVal bv_intDepotId As Int64) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(TariffCodeData.DPT_ID, bv_intDepotId)
            hshParameters.Add(TariffCodeData.TRFF_CD_CD, bv_strTariffCodeCode)
            objData = New DataObjects(TariffCodeSelectQueryByTariffCode, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateTariffCode() TABLE NAME:Tariff_Code"

    Public Function CreateTariffCode(
        ByVal bv_strTariffCode As String, _
        ByVal bv_strDescription As String, _
        ByVal bv_i64ItemId As Int64, _
        ByVal bv_i64SubItemId As Int64, _
        ByVal bv_i64DamageId As Int64, _
        ByVal bv_i64RepairId As Int64, _
        ByVal bv_strManHours As String, _
        ByVal bv_dblMaterialCost As Double, _
        ByVal bv_strRemarks As String, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActive As Boolean, _
        ByVal bv_i32DepotId As Int32, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TariffCodeData._TARIFF_CODE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TariffCodeData._TARIFF_CODE, br_objTrans)
                .Item(TariffCodeData.TRFF_CD_ID) = intMax
                .Item(TariffCodeData.TRFF_CD_CD) = bv_strTariffCode
                .Item(TariffCodeData.TRFF_CD_DESCRPTN_VC) = bv_strDescription
                .Item(TariffCodeData.ITM_ID) = bv_i64ItemId
                If bv_i64SubItemId <> Nothing Then
                    .Item(TariffCodeData.SB_ITM_ID) = bv_i64SubItemId
                Else
                    .Item(TariffCodeData.SB_ITM_ID) = DBNull.Value
                End If
                '.Item(TariffCodeData.SB_ITM_ID) = bv_i64SubItemId
                .Item(TariffCodeData.DMG_ID) = bv_i64DamageId
                .Item(TariffCodeData.RPR_ID) = bv_i64RepairId
                If bv_strManHours <> Nothing Then
                    .Item(TariffCodeData.MN_HR) = bv_strManHours
                Else
                    .Item(TariffCodeData.MN_HR) = DBNull.Value
                End If
                If bv_dblMaterialCost <> Nothing Then
                    .Item(TariffCodeData.MTRL_CST) = bv_dblMaterialCost
                Else
                    .Item(TariffCodeData.MTRL_CST) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(TariffCodeData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(TariffCodeData.RMRKS_VC) = DBNull.Value
                End If
                .Item(TariffCodeData.CRTD_BY) = bv_strCreatedBy
                .Item(TariffCodeData.CRTD_DT) = bv_datCreatedDate
                .Item(TariffCodeData.MDFD_BY) = bv_strModifiedBy
                .Item(TariffCodeData.MDFD_DT) = bv_datModifiedDate
                .Item(TariffCodeData.ACTV_BT) = bv_blnActive
                .Item(TariffCodeData.DPT_ID) = bv_i32DepotId
            End With
            objData.InsertRow(dr, Tariff_CodeInsertQuery, br_objTrans)
            dr = Nothing
            CreateTariffCode = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTariffCode() TABLE NAME:Tariff_Code"

    Public Function UpdateTariffCode(ByVal bv_i64TariffID As Int64, _
       ByVal bv_strTariffCode As String, _
        ByVal bv_strDescription As String, _
        ByVal bv_i64ItemId As Int64, _
        ByVal bv_i64SubItemId As Int64, _
        ByVal bv_i64DamageId As Int64, _
        ByVal bv_i64RepairId As Int64, _
        ByVal bv_strManHours As String, _
        ByVal bv_dblMaterialCost As Double, _
        ByVal bv_strRemarks As String, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActive As Boolean, _
        ByVal bv_i32DepotId As Int32, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TariffCodeData._TARIFF_CODE).NewRow()
            With dr
                .Item(TariffCodeData.TRFF_CD_ID) = bv_i64TariffID
                .Item(TariffCodeData.TRFF_CD_CD) = bv_strTariffCode
                .Item(TariffCodeData.TRFF_CD_DESCRPTN_VC) = bv_strDescription
                .Item(TariffCodeData.ITM_ID) = bv_i64ItemId
                If bv_i64SubItemId <> Nothing Then
                    .Item(TariffCodeData.SB_ITM_ID) = bv_i64SubItemId
                Else
                    .Item(TariffCodeData.SB_ITM_ID) = DBNull.Value
                End If
                '.Item(TariffCodeData.SB_ITM_ID) = bv_i64SubItemId
                .Item(TariffCodeData.DMG_ID) = bv_i64DamageId
                .Item(TariffCodeData.RPR_ID) = bv_i64RepairId
                If bv_strManHours <> Nothing Then
                    .Item(TariffCodeData.MN_HR) = bv_strManHours
                Else
                    .Item(TariffCodeData.MN_HR) = DBNull.Value
                End If
                If bv_dblMaterialCost <> Nothing Then
                    .Item(TariffCodeData.MTRL_CST) = bv_dblMaterialCost
                Else
                    .Item(TariffCodeData.MTRL_CST) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(TariffCodeData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(TariffCodeData.RMRKS_VC) = DBNull.Value
                End If
                .Item(TariffCodeData.CRTD_BY) = bv_strCreatedBy
                .Item(TariffCodeData.CRTD_DT) = bv_datCreatedDate
                .Item(TariffCodeData.MDFD_BY) = bv_strModifiedBy
                .Item(TariffCodeData.MDFD_DT) = bv_datModifiedDate
                .Item(TariffCodeData.ACTV_BT) = bv_blnActive
                .Item(TariffCodeData.DPT_ID) = bv_i32DepotId
            End With
            UpdateTariffCode = objData.UpdateRow(dr, Tariff_CodeUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTariffGroupCodeByDepot() TABLE NAME:Tariff_Code"

    Public Function GetTariffGroupCodeByDepot(ByVal bv_intTariffCodeID As Integer, ByVal bv_blnActive As Boolean) As TariffCodeDataSet
        Try
            Dim hshConfiguration As New Hashtable()
            hshConfiguration.Add(TariffCodeData.TRFF_CD_ID, bv_intTariffCodeID)
            hshConfiguration.Add(TariffCodeData.ACTV_BT, bv_blnActive)
            objData = New DataObjects(Tariff_GroupSelectQueryByTariffCode, hshConfiguration)
            objData.Fill(CType(ds, DataSet), TariffCodeData._V_TARIFF_GROUP_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "DeleteTariff_Code() TABLE NAME:Tariff_Code"

    Public Function DeleteTariff_Code(ByVal bv_intTariffCodeID As Int64, ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TariffCodeData._TARIFF_CODE).NewRow()
            With dr
                .Item(TariffCodeData.TRFF_CD_ID) = bv_intTariffCodeID
            End With
            DeleteTariff_Code = objData.DeleteRow(dr, Tariff_CodeDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteTariff_Group_Detail() TABLE NAME:Tariff_Group_Detail"

    Public Function DeleteTariff_Group_Detail(ByVal bv_intTariffGroupCodeID As Int64, ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TariffCodeData._TARIFF_CODE).NewRow()
            With dr
                .Item(TariffCodeData.TRFF_CD_ID) = bv_intTariffGroupCodeID
            End With
            DeleteTariff_Group_Detail = objData.DeleteRow(dr, Tariff_GroupDetailDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTariffGroupCodeByDepot() TABLE NAME:Tariff_Code"

    Public Function GetTariffGroupCodeByDepot(ByVal bv_intTariffCodeID As Integer, ByVal bv_blnActive As Boolean, ByRef br_objTrans As Transactions) As TariffCodeDataSet
        Try
            Dim hshConfiguration As New Hashtable()
            hshConfiguration.Add(TariffCodeData.TRFF_CD_ID, bv_intTariffCodeID)
            hshConfiguration.Add(TariffCodeData.ACTV_BT, bv_blnActive)
            objData = New DataObjects(Tariff_GroupSelectQueryByTariffCode, hshConfiguration)
            objData.Fill(CType(ds, DataSet), TariffCodeData._V_TARIFF_GROUP_DETAIL, br_objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


#Region "Trariff Codes for Repair Estimate"


    'Customer Trariff
    Public Function GetTariffCodeByCustomerID(ByVal bv_CustomerID As String, ByVal bv_EquipmentTypeID As String, ByVal bv_DepotID As String) As DataTable
        Try

            Dim hshParam As New Hashtable
            hshParam.Add(CustomerTariffCodeData.TRFF_CD_CSTMR_ID, bv_CustomerID)
            hshParam.Add(CustomerTariffCodeData.TRFF_CD_EQP_TYP_ID, bv_EquipmentTypeID)
            hshParam.Add(CustomerTariffCodeData.DPT_ID, bv_DepotID)

            Dim dt As New DataTable
            objData = New DataObjects(GetTariffCodeByCustomerID_SelectQry, hshParam)
            objData.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Agent Trariff
    Public Function GetTariffCodeByAgentID(ByVal bv_AgentID As String, ByVal bv_EquipmentTypeID As String, ByVal bv_DepotID As String) As DataTable
        Try

            Dim hshParam As New Hashtable
            hshParam.Add(CustomerTariffCodeData.TRFF_CD_CSTMR_ID, bv_AgentID)
            hshParam.Add(CustomerTariffCodeData.TRFF_CD_EQP_TYP_ID, bv_EquipmentTypeID)
            hshParam.Add(CustomerTariffCodeData.DPT_ID, bv_DepotID)

            Dim dt As New DataTable
            objData = New DataObjects(GetTariffCodeByAgentID_SelectQry, hshParam)
            objData.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Standard Trariff
    Public Function GetStandardTariffCode(ByVal bv_EquipmentTypeID As String, ByVal bv_DepotID As String) As DataTable
        Try
            Dim hshParam As New Hashtable
            hshParam.Add(CustomerTariffCodeData.TRFF_CD_EQP_TYP_ID, bv_EquipmentTypeID)
            hshParam.Add(CustomerTariffCodeData.DPT_ID, bv_DepotID)

            Dim dt As New DataTable
            objData = New DataObjects(GetStandardTariffCode_SelectQry, hshParam)
            objData.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class

#End Region