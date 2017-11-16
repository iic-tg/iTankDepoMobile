Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "TariffGroups"

Public Class TariffGroups

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const Tariff_GroupSelectQueryPk As String = "SELECT TRFF_GRP_ID,TRFF_GRP_CD,TRFF_GRP_DESCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM TARIFF_GROUP WHERE TRFF_GRP_CD=@TRFF_GRP_CD"
    Private Const Tariff_GroupSelectQuery As String = "SELECT TRFF_GRP_ID,TRFF_GRP_CD,TRFF_GRP_DESCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM TARIFF_GROUP"
    Private Const Tariff_GroupInsertQuery As String = "INSERT INTO TARIFF_GROUP(TRFF_GRP_ID,TRFF_GRP_CD,TRFF_GRP_DESCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@TRFF_GRP_ID,@TRFF_GRP_CD,@TRFF_GRP_DESCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const Tariff_GroupUpdateQuery As String = "UPDATE TARIFF_GROUP SET TRFF_GRP_CD=@TRFF_GRP_CD, TRFF_GRP_DESCRPTN_VC=@TRFF_GRP_DESCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT WHERE (TRFF_GRP_ID=@TRFF_GRP_ID AND DPT_ID=@DPT_ID )"
    Private Const TariffGroupDetailSelectQuerynew As String = "SELECT TRFF_CD_CD,TRFF_CD_ID, TRFF_CD_ID AS TRFF_GRP_DTL_ID,TRFF_CD_ID AS TRFF_GRP_ID FROM TARIFF_CODE WHERE ACTV_BT = 1 AND DPT_ID=@DPT_ID"
    Private Const Tariff_Group_DetailInsertQuery As String = "INSERT INTO TARIFF_GROUP_DETAIL(TRFF_GRP_DTL_ID,TRFF_CD_ID,TRFF_GRP_ID,ACTV_BT)VALUES(@TRFF_GRP_DTL_ID,@TRFF_CD_ID,@TRFF_GRP_ID,@ACTV_BT)"
    Private Const Tariff_Group_DetailUpdateQuery As String = "UPDATE TARIFF_GROUP_DETAIL SET TRFF_CD_ID=@TRFF_CD_ID, TRFF_GRP_ID=@TRFF_GRP_ID, ACTV_BT=@ACTV_BT WHERE TRFF_GRP_DTL_ID=@TRFF_GRP_DTL_ID"
    Private Const TariffGroupDetailSelectQueryID As String = "SELECT TRFF_GRP_DTL_ID,TRFF_CD_ID,TRFF_CD_CD,TRFF_GRP_ID,TRFF_CD_DESCRPTN_VC,RMRKS_VC,ACTV_BT FROM V_TARIFF_GROUP_DETAIL AS TGD WHERE TRFF_GRP_ID=@TRFF_GRP_ID"

    Private ds As TariffGroupDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New TariffGroupDataSet
    End Sub

#End Region

#Region "GetTARIFFGROUPByCD() TABLE NAME:Tariff_Group"

    Public Function GetTARIFFGROUPByCD(ByVal bv_strTariffGroup_CD As String) As TariffGroupDataSet
        Try
            objData = New DataObjects(Tariff_GroupSelectQueryPk, TariffGroupData.TRFF_GRP_CD, CStr(bv_strTariffGroup_CD))
            objData.Fill(CType(ds, DataSet), TariffGroupData._TARIFF_GROUP)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTariffGroup() TABLE NAME:Tariff_Group"

    Public Function GetTariffGroup() As TariffGroupDataSet
        Try
            objData = New DataObjects(Tariff_GroupSelectQuery)
            objData.Fill(CType(ds, DataSet), TariffGroupData._TARIFF_GROUP)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateTariffGroup() TABLE NAME:Tariff_Group"

    Public Function CreateTariffGroup(ByVal bv_strTariffGroupCode As String, _
        ByVal bv_strTariffGroupDescription As String, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_i32DepotId As Int32, _
        ByVal br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TariffGroupData._TARIFF_GROUP).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TariffGroupData._TARIFF_GROUP, br_objTrans)
                .Item(TariffGroupData.TRFF_GRP_ID) = intMax
                .Item(TariffGroupData.TRFF_GRP_CD) = bv_strTariffGroupCode
                .Item(TariffGroupData.TRFF_GRP_DESCRPTN_VC) = bv_strTariffGroupDescription
                .Item(TariffGroupData.CRTD_BY) = bv_strCreatedBy
                .Item(TariffGroupData.CRTD_DT) = bv_datCreatedDate
                .Item(TariffGroupData.MDFD_BY) = bv_strModifiedBy
                .Item(TariffGroupData.MDFD_DT) = bv_datModifiedDate
                .Item(TariffGroupData.ACTV_BT) = bv_blnActiveBit
                .Item(TariffGroupData.DPT_ID) = bv_i32DepotId
            End With
            objData.InsertRow(dr, Tariff_GroupInsertQuery, br_objTrans)
            dr = Nothing
            CreateTariffGroup = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTariffGroup() TABLE NAME:Tariff_Group"

    Public Function UpdateTariffGroup(ByVal bv_i64TariffGroupID As Int64, _
        ByVal bv_strTariffGroupCode As String, _
        ByVal bv_strTariffGroupDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_i32DepotId As Int32, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TariffGroupData._TARIFF_GROUP).NewRow()
            With dr
                .Item(TariffGroupData.TRFF_GRP_ID) = bv_i64TariffGroupID
                .Item(TariffGroupData.TRFF_GRP_CD) = bv_strTariffGroupCode
                .Item(TariffGroupData.TRFF_GRP_DESCRPTN_VC) = bv_strTariffGroupDescription
                .Item(TariffGroupData.MDFD_BY) = bv_strModifiedBy
                .Item(TariffGroupData.MDFD_DT) = bv_datModifiedDate
                .Item(TariffGroupData.ACTV_BT) = bv_blnActiveBit
                .Item(TariffGroupData.DPT_ID) = bv_i32DepotId
            End With
            UpdateTariffGroup = objData.UpdateRow(dr, Tariff_GroupUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetBankDetail()"

    Public Function GetTariffGroupDetail(ByVal bv_DPT_ID As Int32) As TariffGroupDataSet
        Try
            objData = New DataObjects(TariffGroupDetailSelectQuerynew, TariffGroupData.DPT_ID, bv_DPT_ID)
            objData.Fill(CType(ds, DataSet), TariffGroupData._TARIFF_GROUP_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
    
#Region "CreateTariffGroupDetail() TABLE NAME:Tariff_Group_Detail"

    Public Function CreateTariffDetail(ByVal bv_i64TariffgrpID As Int64, _
                                       ByVal bv_i64TariffCodeID As Int64, _
                                       ByVal bv_blnActive As Boolean, _
                                       ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TariffGroupData._TARIFF_GROUP_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TariffGroupData._TARIFF_GROUP_DETAIL, br_ObjTransactions)
                .Item(TariffGroupData.TRFF_GRP_DTL_ID) = intMax
                .Item(TariffGroupData.TRFF_CD_ID) = bv_i64TariffCodeID
                .Item(TariffGroupData.TRFF_GRP_ID) = bv_i64TariffgrpID
                .Item(TariffGroupData.ACTV_BT) = bv_blnActive
            End With
            objData.InsertRow(dr, Tariff_Group_DetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateTariffDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTariffGroupDetail() TABLE NAME:Tariff_Group_Detail"

    Public Function UpdateTariffDetail(ByVal bv_i64TariffGroupDetailID As Int64, _
                                        ByVal bv_i64TariffCodeID As Int64, _
                                        ByVal bv_i64TariffGroupID As Int64, _
                                        ByVal bv_blnActive As Boolean, _
                                        ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TariffGroupData._TARIFF_GROUP_DETAIL).NewRow()
            With dr
                .Item(TariffGroupData.TRFF_GRP_DTL_ID) = bv_i64TariffGroupDetailID
                .Item(TariffGroupData.TRFF_CD_ID) = bv_i64TariffCodeID
                .Item(TariffGroupData.TRFF_GRP_ID) = bv_i64TariffGroupID
                .Item(TariffGroupData.ACTV_BT) = bv_blnActive
            End With
            UpdateTariffDetail = objData.UpdateRow(dr, Tariff_Group_DetailUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetBankDetail()"

    Public Function GetTariffGroupDetailID(ByVal bv_TRFF_GRP_ID As Int32) As TariffGroupDataSet
        Try
            objData = New DataObjects(TariffGroupDetailSelectQueryID, TariffGroupData.TRFF_GRP_ID, bv_TRFF_GRP_ID)
            objData.Fill(CType(ds, DataSet), TariffGroupData._TARIFF_GROUP_DETAIL)

            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class

#End Region