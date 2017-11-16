Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class RouteDetail
#Region "pub_ValidateRoute() "

    <OperationContract()> _
    Public Function pub_ValidateRoute(ByVal bv_strRouteCode As String, ByVal bv_WfData As String) As Boolean

        Try
            Dim objRoute As New RouteDetails
            Dim intRowCount As Integer
            intRowCount = CInt(objRoute.GetRouteCodeByRoute(bv_strRouteCode, CLng(CommonUIs.ParseWFDATA(bv_WfData, TariffCodeData.DPT_ID))))
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

#Region "pub_VRouteGetVRouteBy() TABLE NAME:V_ROUTE"

    <OperationContract()> _
    Public Function pub_VRouteGetVRouteByDepotId(ByVal bv_intDepotID As Integer) As RouteDetailDataSet

        Try
            Dim dsVRouteData As RouteDetailDataSet
            Dim objVRoutes As New RouteDetails
            dsVRouteData = objVRoutes.GetV_RouteByDepotID(bv_intDepotID)
            Return dsVRouteData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_UpdateRoute()"

    <OperationContract()> _
    Public Function pub_UpdateRoute(ByRef br_dsRouteDetail As RouteDetailDataSet, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_strWfData As String, _
                                        ByVal intDepotID As Integer) As Boolean
        Dim objTrans As New Transactions

        Try
            Dim objRouteDetail As New RouteDetails
            Dim dtRouteDetail As DataTable
            Dim dblEmptyTripRate As Double
            Dim dblFullTripRate As Double
            'Dim activityId As Integer
            'Dim ActivityLocationId As Integer


            dtRouteDetail = br_dsRouteDetail.Tables(RouteDetailData._V_ROUTE)

            If Not dtRouteDetail Is Nothing Then
                For Each drRouteDetail As DataRow In dtRouteDetail.Rows
                    If drRouteDetail.RowState = DataRowState.Added Or drRouteDetail.RowState = DataRowState.Modified Then
                        If Not (drRouteDetail.Item(RouteDetailData.EMPTY_TRP_RT_NC) Is DBNull.Value) Then
                            dblEmptyTripRate = CDbl(drRouteDetail.Item(RouteDetailData.EMPTY_TRP_RT_NC))
                        Else
                            dblEmptyTripRate = vbEmpty
                        End If
                        If Not (drRouteDetail.Item(RouteDetailData.FLL_TRP_RT_NC) Is DBNull.Value) Then
                            dblFullTripRate = CDbl(drRouteDetail.Item(RouteDetailData.FLL_TRP_RT_NC))
                        Else
                            dblFullTripRate = vbEmpty
                        End If
                        'If Not (drRouteDetail.Item(RouteDetailData.ACTVTY_ID) Is DBNull.Value) Then
                        '    activityId = CInt(drRouteDetail.Item(RouteDetailData.ACTVTY_ID))
                        'Else
                        '    activityId = vbEmpty

                        'End If
                        'If Not (drRouteDetail.Item(RouteDetailData.ACTVTY_LCTN_ID) Is DBNull.Value) Then
                        '    ActivityLocationId = CInt(drRouteDetail.Item(RouteDetailData.ACTVTY_LCTN_ID))
                        'Else
                        '    ActivityLocationId = vbEmpty

                        'End If
                    End If
                    Select Case drRouteDetail.RowState
                        Case DataRowState.Added
                            Dim lngRouteId As Long
                            lngRouteId = objRouteDetail.CreateRoute((drRouteDetail.Item(RouteDetailData.RT_CD)).ToString, _
                                                                     (drRouteDetail.Item(RouteDetailData.RT_DSCRPTN_VC)).ToString, _
                                                                     CInt(drRouteDetail.Item(RouteDetailData.PCK_UP_LCTN_ID)), _
                                                                     CInt(drRouteDetail.Item(RouteDetailData.DRP_OFF_LCTN_ID)), _
                                                                     CBool(drRouteDetail.Item(RouteDetailData.ACTV_BT)), _
                                                                     bv_strModifiedBy, _
                                                                     bv_datModifiedDate, _
                                                                     bv_strModifiedBy, _
                                                                     bv_datModifiedDate, _
                                                                     intDepotID, _
                                                                     objTrans)


                        Case DataRowState.Modified
                            Dim blnUpdated As Boolean
                            blnUpdated = objRouteDetail.UpdateRoute(CInt(drRouteDetail.Item(RouteDetailData.RT_ID)), _
                                                                     (drRouteDetail.Item(RouteDetailData.RT_CD)).ToString, _
                                                                     (drRouteDetail.Item(RouteDetailData.RT_DSCRPTN_VC)).ToString, _
                                                                     CInt(drRouteDetail.Item(RouteDetailData.PCK_UP_LCTN_ID)), _
                                                                     CInt(drRouteDetail.Item(RouteDetailData.DRP_OFF_LCTN_ID)), _
                                                                     CBool(drRouteDetail.Item(RouteDetailData.ACTV_BT)), _
                                                                     bv_strModifiedBy, _
                                                                     bv_datModifiedDate, _
                                                                     intDepotID, _
                                                                     objTrans)

                    End Select
                Next
            End If
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region
End Class
