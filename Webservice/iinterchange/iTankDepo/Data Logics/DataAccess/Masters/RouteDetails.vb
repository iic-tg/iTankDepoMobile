Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
Public Class RouteDetails
#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_ROUTESelectQuery As String = "SELECT RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_ID,PCK_UP_LCTN_CD,DRP_OFF_LCTN_ID,DRP_OFF_LCTN_CD,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM V_ROUTE WHERE DPT_ID=@DPT_ID"
    Private Const RouteInsertQuery As String = "INSERT INTO ROUTE(RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_ID,DRP_OFF_LCTN_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID)VALUES(@RT_ID,@RT_CD,@RT_DSCRPTN_VC,@PCK_UP_LCTN_ID,@DRP_OFF_LCTN_ID,@ACTV_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@DPT_ID)"
    Private Const RouteUpdateQuery As String = "UPDATE ROUTE SET RT_CD=@RT_CD, RT_DSCRPTN_VC=@RT_DSCRPTN_VC, PCK_UP_LCTN_ID=@PCK_UP_LCTN_ID, DRP_OFF_LCTN_ID=@DRP_OFF_LCTN_ID, ACTV_BT=@ACTV_BT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE RT_ID=@RT_ID AND DPT_ID=@DPT_ID"
    Private Const RouteCodeSelectQueryByRouteCode As String = "SELECT RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_ID,PCK_UP_LCTN_CD,DRP_OFF_LCTN_ID,DRP_OFF_LCTN_CD,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM V_ROUTE WHERE (DPT_ID=@DPT_ID AND RT_CD=@RT_CD)"

    Private ds As RouteDetailDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New RouteDetailDataSet
    End Sub

#End Region

#Region "GetRouteCodeByRoute()"
    Public Function GetRouteCodeByRoute(ByVal bv_strRouteCode As String, ByVal bv_intDepotId As Int64) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(RouteDetailData.DPT_ID, bv_intDepotId)
            hshParameters.Add(RouteDetailData.RT_CD, bv_strRouteCode)
            objData = New DataObjects(RouteCodeSelectQueryByRouteCode, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVRouteBy() TABLE NAME:V_ROUTE"
    Public Function GetV_RouteByDepotID(ByVal bv_intDepotId As Integer) As RouteDetailDataSet
        Try
            objData = New DataObjects(V_ROUTESelectQuery, RouteDetailData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), RouteDetailData._V_ROUTE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateRoute() TABLE NAME:Route"

    Public Function CreateRoute(ByVal bv_strRouteCode As String, _
                                ByVal bv_strRouteDescription As String, _
                                ByVal bv_i64PickUpLocationId As Int64, _
                                ByVal bv_i64DropOffLocationId As Int64, _
                                ByVal bv_blnActiveBit As Boolean, _
                                ByVal bv_strCreatedBy As String, _
                                ByVal bv_datCreatedDate As DateTime, _
                                ByVal bv_strModifiedBy As String, _
                                ByVal bv_datModifiedDate As DateTime, _
                                ByVal bv_i32DepotId As Int32, _
         ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(RouteDetailData._ROUTE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(RouteDetailData._ROUTE, br_objTrans)
                .Item(RouteDetailData.RT_ID) = intMax
                .Item(RouteDetailData.RT_CD) = bv_strRouteCode
                .Item(RouteDetailData.RT_DSCRPTN_VC) = bv_strRouteDescription
                .Item(RouteDetailData.PCK_UP_LCTN_ID) = bv_i64PickUpLocationId
                .Item(RouteDetailData.DRP_OFF_LCTN_ID) = bv_i64DropOffLocationId
                .Item(RouteDetailData.ACTV_BT) = bv_blnActiveBit
                .Item(RouteDetailData.CRTD_BY) = bv_strCreatedBy
                .Item(RouteDetailData.CRTD_DT) = bv_datCreatedDate
                .Item(RouteDetailData.MDFD_BY) = bv_strModifiedBy
                .Item(RouteDetailData.MDFD_DT) = bv_datModifiedDate
                .Item(RouteDetailData.DPT_ID) = bv_i32DepotId
            End With
            objData.InsertRow(dr, RouteInsertQuery, br_objTrans)
            dr = Nothing
            CreateRoute = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateRoute() TABLE NAME:Route"

    Public Function UpdateRoute(ByVal bv_i64RouteID As Int64, _
                                ByVal bv_strRouteCode As String, _
                                ByVal bv_strRouteDescription As String, _
                                ByVal bv_i64PickUpLocationId As Int64, _
                                ByVal bv_i64DropOffLocationId As Int64, _
                                ByVal bv_blnActiveBit As Boolean, _
                                ByVal bv_strModifiedBy As String, _
                                ByVal bv_datModifiedDate As DateTime, _
                                ByVal bv_i32DepotId As Int32, _
                                ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RouteDetailData._ROUTE).NewRow()
            With dr
                .Item(RouteDetailData.RT_ID) = bv_i64RouteID
                .Item(RouteDetailData.RT_CD) = bv_strRouteCode
                .Item(RouteDetailData.RT_DSCRPTN_VC) = bv_strRouteDescription
                .Item(RouteDetailData.PCK_UP_LCTN_ID) = bv_i64PickUpLocationId
                .Item(RouteDetailData.DRP_OFF_LCTN_ID) = bv_i64DropOffLocationId
                .Item(RouteDetailData.ACTV_BT) = bv_blnActiveBit
                .Item(RouteDetailData.MDFD_BY) = bv_strModifiedBy
                .Item(RouteDetailData.MDFD_DT) = bv_datModifiedDate
                .Item(RouteDetailData.DPT_ID) = bv_i32DepotId
            End With
            UpdateRoute = objData.UpdateRow(dr, RouteUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
