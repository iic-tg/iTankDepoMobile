Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

#Region "Components"

Public Class Components

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const ComponentSelectQueryByComponentCode As String = "SELECT COUNT(*) FROM COMPONENT WHERE CMPNNT_CD=@CMPNNT_CD AND DPT_ID=@DPT_ID"
    Private Const ComponentSelectQueryByDepotID As String = "SELECT CMPNNT_ID,CMPNNT_CD,CMPNNT_DSCRPTN_VC,EQPMNT_TYP_ID,ASSMBLY,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,(SELECT EQPMNT_TYP_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_ID=a.EQPMNT_TYP_ID) AS EQPMNT_TYP_CD  FROM COMPONENT a WHERE DPT_ID=@DPT_ID"
    Private Const ComponentInsertQuery As String = "INSERT INTO COMPONENT(CMPNNT_ID,CMPNNT_CD,CMPNNT_DSCRPTN_VC,EQPMNT_TYP_ID,ASSMBLY,CRTD_BY,CRTD_DT,ACTV_BT,DPT_ID)VALUES(@CMPNNT_ID,@CMPNNT_CD,@CMPNNT_DSCRPTN_VC,@EQPMNT_TYP_ID,@ASSMBLY,@CRTD_BY,@CRTD_DT,@ACTV_BT,@DPT_ID)"
    Private Const ComponentUpdateQuery As String = "UPDATE Component SET CMPNNT_ID=@CMPNNT_ID, CMPNNT_CD=@CMPNNT_CD, CMPNNT_DSCRPTN_VC=@CMPNNT_DSCRPTN_VC, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, ASSMBLY=@ASSMBLY, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE CMPNNT_ID=@CMPNNT_ID"
    Private ds As ComponentDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New ComponentDataSet
    End Sub

#End Region

#Region "GetComponentByComponentCode() TABLE NAME:COMPONENT"

    Public Function GetComponentByComponentCode(ByVal bv_strComponentCode As String, ByVal bv_intDepotId As Int64) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(ComponentData.CMPNNT_CD, bv_strComponentCode)
            hshParameters.Add(ComponentData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(ComponentSelectQueryByComponentCode, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetComponentByDepotID() TABLE NAME:COMPONENT"

    Public Function GetComponentByDepotID(ByVal bv_i32DepotID As Int32) As ComponentDataSet
        Try
            objData = New DataObjects(ComponentSelectQueryByDepotID, ComponentData.DPT_ID, CStr(bv_i32DepotID))
            objData.Fill(CType(ds, DataSet), ComponentData._COMPONENT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateComponent() TABLE NAME:Component"

    Public Function CreateComponent(ByVal bv_strComponentCode As String, _
        ByVal bv_strComponentDescription As String, _
        ByVal bv_strEquipmentTypeId As Int64, _
        ByVal bv_strAssembly As String, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_i32DepotID As Int32) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ComponentData._COMPONENT).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ComponentData._COMPONENT)
                .Item(ComponentData.CMPNNT_ID) = intMax
                .Item(ComponentData.CMPNNT_CD) = bv_strComponentCode
                .Item(ComponentData.CMPNNT_DSCRPTN_VC) = bv_strComponentDescription
                .Item(ComponentData.EQPMNT_TYP_ID) = bv_strEquipmentTypeId
                .Item(ComponentData.ASSMBLY) = bv_strAssembly
                If bv_strCreatedBy <> Nothing Then
                    .Item(ComponentData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(ComponentData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(ComponentData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(ComponentData.CRTD_DT) = DBNull.Value
                End If
                .Item(ComponentData.ACTV_BT) = bv_blnActiveBit
                .Item(ComponentData.DPT_ID) = bv_i32DepotID
            End With
            objData.InsertRow(dr, ComponentInsertQuery)
            dr = Nothing
            CreateComponent = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateComponent() TABLE NAME:Component"

    Public Function UpdateComponent(ByVal bv_i64ComponentId As Int64, _
        ByVal bv_strComponentCode As String, _
        ByVal bv_strComponentDescription As String, _
        ByVal bv_strComponentTypeId As Int64, _
        ByVal bv_strAssembly As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_i32DepotID As Int32) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ComponentData._COMPONENT).NewRow()
            With dr
                .Item(ComponentData.CMPNNT_ID) = bv_i64ComponentId
                .Item(ComponentData.CMPNNT_CD) = bv_strComponentCode
                .Item(ComponentData.CMPNNT_DSCRPTN_VC) = bv_strComponentDescription
                .Item(ComponentData.EQPMNT_TYP_ID) = bv_strComponentTypeId
                .Item(ComponentData.ASSMBLY) = bv_strAssembly
                If bv_strModifiedBy <> Nothing Then
                    .Item(ComponentData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(ComponentData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(ComponentData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(ComponentData.MDFD_DT) = DBNull.Value
                End If
                .Item(ComponentData.ACTV_BT) = bv_blnActiveBit
                .Item(ComponentData.DPT_ID) = bv_i32DepotID
            End With
            UpdateComponent = objData.UpdateRow(dr, ComponentUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
