#Region " EquipmentInformation.vb"
'*********************************************************************************************************************
'Name :
'      EquipmentInformation.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EquipmentInformation.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      16/Oct/13 11:57:36 a.m.
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

<ServiceContract()> _
Public Class EquipmentInformation

#Region "pub_ValidateEquipmentNoByDepotID"
    <OperationContract()> _
    Public Function pub_ValidateEquipmentNoByDepotID(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32) As Boolean

        Try
            Dim objEqpInfo As New EquipmentInformations
            Dim intRowCount As Integer
            intRowCount = CInt(objEqpInfo.GetEquipmentInformationByID(bv_strEquipmentNo, bv_intDepotID))
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

#Region "GetEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function GetEquipmentInformation(ByVal bv_intDepotID As Integer) As EquipmentInformationDataSet
        Try
            Dim dsEquipmentInformationData As EquipmentInformationDataSet
            Dim objEquipmentInformations As New EquipmentInformations
            dsEquipmentInformationData = objEquipmentInformations.GetEquipmentInformation(bv_intDepotID)
            Return dsEquipmentInformationData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_UpdateEquipmentInformation"
    <OperationContract()> _
    Public Function pub_UpdateEquipmentInformation(ByRef br_dsEqpInfoDataset As EquipmentInformationDataSet, ByVal bv_intDepotID As Integer, ByVal bv_strUserName As String) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objEquipmentInfo As New EquipmentInformations
            Dim lngCreated As Long
            Dim bolupdatebt As Boolean
           
            For Each drEqpInfo As DataRow In br_dsEqpInfoDataset.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows
                Dim decTareWt As Decimal = 0
                Dim decGrossWt As Decimal = 0
                Dim decCapacity As Decimal = 0
                Dim strLastSurveyor As String = String.Empty
                Dim dtLastTestDate As DateTime = Nothing
                Dim dtNextTestDate As DateTime = Nothing
                Dim intLastTestTypeID As Integer = 0
                Dim intNextTestTypeID As Integer = 0
                Dim strValidityPeriod As String = String.Empty
                Dim strRemarks As String = String.Empty
                Dim strPrvOnhrLocation As String = String.Empty
                Dim strPrvOnhrDate As DateTime = Nothing
                Dim strDppCurrency As String = String.Empty
                Dim strCSCValidity As String = String.Empty
                Select Case drEqpInfo.RowState
                    Case DataRowState.Added
                        If Not drEqpInfo.Item(EquipmentInformationData.TR_WGHT_NC) Is DBNull.Value Then
                            decTareWt = CDec(drEqpInfo.Item(EquipmentInformationData.TR_WGHT_NC))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.GRSS_WGHT_NC) Is DBNull.Value Then
                            decGrossWt = CDec(drEqpInfo.Item(EquipmentInformationData.GRSS_WGHT_NC))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.CPCTY_NC) Is DBNull.Value Then
                            decCapacity = CDec(drEqpInfo.Item(EquipmentInformationData.CPCTY_NC))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.LST_SRVYR_NM) Is DBNull.Value Then
                            strLastSurveyor = CStr(drEqpInfo.Item(EquipmentInformationData.LST_SRVYR_NM))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.LST_TST_DT) Is DBNull.Value AndAlso IsDate(drEqpInfo.Item(EquipmentInformationData.LST_TST_DT)) Then
                            dtLastTestDate = CDate(drEqpInfo.Item(EquipmentInformationData.LST_TST_DT))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.NXT_TST_DT) Is DBNull.Value AndAlso IsDate(drEqpInfo.Item(EquipmentInformationData.NXT_TST_DT)) Then
                            dtNextTestDate = CDate(drEqpInfo.Item(EquipmentInformationData.NXT_TST_DT))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.LST_TST_TYP_ID) Is DBNull.Value Then
                            intLastTestTypeID = CInt(drEqpInfo.Item(EquipmentInformationData.LST_TST_TYP_ID))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.NXT_TST_TYP_ID) Is DBNull.Value Then
                            intNextTestTypeID = CInt(drEqpInfo.Item(EquipmentInformationData.NXT_TST_TYP_ID))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.RMRKS_VC) Is DBNull.Value Then
                            strRemarks = CStr(drEqpInfo.Item(EquipmentInformationData.RMRKS_VC))
                        End If

                        If Not drEqpInfo.Item(EquipmentInformationData.PRVS_ONHR_LCTN_ID) Is DBNull.Value Then
                            strPrvOnhrLocation = CStr(drEqpInfo.Item(EquipmentInformationData.PRVS_ONHR_LCTN_ID))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.PRVS_ONHR_DT) Is DBNull.Value Then
                            strPrvOnhrDate = CDate((drEqpInfo.Item(EquipmentInformationData.PRVS_ONHR_DT)))
                        End If

                        If Not drEqpInfo.Item(EquipmentInformationData.CSC_VLDTY) Is DBNull.Value Then
                            strCSCValidity = CStr(drEqpInfo.Item(EquipmentInformationData.CSC_VLDTY))
                        End If

                        If intLastTestTypeID = 52 Then
                            strValidityPeriod = "5"
                        ElseIf intLastTestTypeID = 51 Then
                            strValidityPeriod = "2.5"
                        End If

                        If drEqpInfo.Item(EquipmentInformationData.EQPMNT_TYP_ID) Is DBNull.Value Then
                            Dim dsEqpData As CommonUIDataSet
                            Dim objConfigs As New CommonUIs
                            dsEqpData = objConfigs.GetEquipmentType(CStr(drEqpInfo.Item(EquipmentInformationData.EQPMNT_TYP_CD)), bv_intDepotID)
                            drEqpInfo.Item(EquipmentInformationData.EQPMNT_TYP_ID) = dsEqpData.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                            dsEqpData.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Clear()
                        End If

                        If Not IsDBNull(drEqpInfo.Item(EquipmentInformationData.EQPMNT_NO)) Then
                            lngCreated = objEquipmentInfo.CreateEquipmentInformation(drEqpInfo.Item(EquipmentInformationData.EQPMNT_NO).ToString, _
                                                                    CommonUIs.iLng(drEqpInfo.Item(EquipmentInformationData.EQPMNT_TYP_ID)), _
                                                                    CommonUIs.iDat(drEqpInfo.Item(EquipmentInformationData.MNFCTR_DT)), _
                                                                    decTareWt, _
                                                                    decGrossWt, _
                                                                    decCapacity, _
                                                                    bv_strUserName, _
                                                                    Now, _
                                                                    bv_strUserName, _
                                                                    Now, _
                                                                    bv_intDepotID, _
                                                                    CommonUIs.iBool(drEqpInfo.Item(EquipmentInformationData.ACTV_BT)), _
                                                                    strLastSurveyor, _
                                                                    dtLastTestDate, _
                                                                    intLastTestTypeID, _
                                                                    dtNextTestDate, _
                                                                    intNextTestTypeID, _
                                                                    strValidityPeriod, _
                                                                    CommonUIs.iBool(drEqpInfo.Item(EquipmentInformationData.RNTL_BT)), _
                                                                    strRemarks, _
                                                                    strPrvOnhrLocation, _
                                                                    CDate(strPrvOnhrDate), _
                                                                    CStr(strCSCValidity), _
                                                                    CommonUIs.iLng(drEqpInfo.Item(EquipmentInformationData.EQPMNT_TYP_ID)), _
                                                                    objTrans)
                            drEqpInfo.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = lngCreated
                            For Each drEqpDetail As DataRow In br_dsEqpInfoDataset.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows
                                If drEqpDetail.RowState = DataRowState.Deleted Then
                                    objEquipmentInfo.DeleteEquipmentInformationDetail(CLng(drEqpDetail.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID, DataRowVersion.Original)), _
                                                                                      objTrans)
                                Else

                                    objEquipmentInfo.CreateEquipmentInformationDetail(lngCreated, _
                                                                                 CStr(drEqpDetail.Item(EquipmentInformationData.ATTCHMNT_PTH)), _
                                                                                 CStr(drEqpDetail.Item(EquipmentInformationData.ACTL_FL_NM)), _
                                                                                 objTrans)
                                End If
                            Next
                        End If
                    Case DataRowState.Modified
                        If Not drEqpInfo.Item(EquipmentInformationData.TR_WGHT_NC) Is DBNull.Value Then
                            decTareWt = CDec(drEqpInfo.Item(EquipmentInformationData.TR_WGHT_NC))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.GRSS_WGHT_NC) Is DBNull.Value Then
                            decGrossWt = CDec(drEqpInfo.Item(EquipmentInformationData.GRSS_WGHT_NC))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.CPCTY_NC) Is DBNull.Value Then
                            decCapacity = CDec(drEqpInfo.Item(EquipmentInformationData.CPCTY_NC))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.LST_SRVYR_NM) Is DBNull.Value Then
                            strLastSurveyor = CStr(drEqpInfo.Item(EquipmentInformationData.LST_SRVYR_NM))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.LST_TST_DT) Is DBNull.Value AndAlso IsDate(drEqpInfo.Item(EquipmentInformationData.LST_TST_DT)) Then
                            dtLastTestDate = CDate(drEqpInfo.Item(EquipmentInformationData.LST_TST_DT))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.NXT_TST_DT) Is DBNull.Value AndAlso IsDate(drEqpInfo.Item(EquipmentInformationData.NXT_TST_DT)) Then
                            dtNextTestDate = CDate(drEqpInfo.Item(EquipmentInformationData.NXT_TST_DT))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.LST_TST_TYP_ID) Is DBNull.Value Then
                            intLastTestTypeID = CInt(drEqpInfo.Item(EquipmentInformationData.LST_TST_TYP_ID))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.NXT_TST_TYP_ID) Is DBNull.Value Then
                            intNextTestTypeID = CInt(drEqpInfo.Item(EquipmentInformationData.NXT_TST_TYP_ID))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.RMRKS_VC) Is DBNull.Value Then
                            strRemarks = CStr(drEqpInfo.Item(EquipmentInformationData.RMRKS_VC))
                        End If

                        If Not drEqpInfo.Item(EquipmentInformationData.PRVS_ONHR_LCTN_ID) Is DBNull.Value Then
                            strPrvOnhrLocation = CStr(drEqpInfo.Item(EquipmentInformationData.PRVS_ONHR_LCTN_ID))
                        End If
                        If Not drEqpInfo.Item(EquipmentInformationData.PRVS_ONHR_DT) Is DBNull.Value Then
                            strPrvOnhrDate = CDate(CStr(drEqpInfo.Item(EquipmentInformationData.PRVS_ONHR_DT)))
                        End If


                        If Not drEqpInfo.Item(EquipmentInformationData.CSC_VLDTY) Is DBNull.Value Then
                            strCSCValidity = CStr(drEqpInfo.Item(EquipmentInformationData.CSC_VLDTY))
                        End If
                        If intLastTestTypeID = 52 Then
                            strValidityPeriod = "5"
                        ElseIf intLastTestTypeID = 51 Then
                            strValidityPeriod = "2.5"
                        End If

                        bolupdatebt = objEquipmentInfo.UpdateEquipmentInformation(CommonUIs.iLng(drEqpInfo.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID)), _
                                                                                  drEqpInfo.Item(EquipmentInformationData.EQPMNT_NO).ToString, _
                                                                                  CommonUIs.iLng(drEqpInfo.Item(EquipmentInformationData.EQPMNT_TYP_ID)), _
                                                                                  CommonUIs.iDat(drEqpInfo.Item(EquipmentInformationData.MNFCTR_DT)), _
                                                                                  decTareWt, _
                                                                                  decGrossWt, _
                                                                                  decCapacity, _
                                                                                  bv_strUserName, _
                                                                                  Now, _
                                                                                  bv_intDepotID, _
                                                                                  CommonUIs.iBool(drEqpInfo.Item(EquipmentInformationData.ACTV_BT)), _
                                                                                  strLastSurveyor, _
                                                                                  dtLastTestDate, _
                                                                                  intLastTestTypeID, _
                                                                                  dtNextTestDate, _
                                                                                  intNextTestTypeID, _
                                                                                  strValidityPeriod, _
                                                                                  CommonUIs.iBool(drEqpInfo.Item(EquipmentInformationData.RNTL_BT)), _
                                                                                  strRemarks, _
                                                                                  strPrvOnhrLocation, _
                                                                                  CDate(strPrvOnhrDate), _
                                                                                  CStr(strCSCValidity), CommonUIs.iLng(drEqpInfo.Item(EquipmentInformationData.EQPMNT_TYP_ID)), _
                                                                                  objTrans)

                        'for remarks updated in tracking table 14jan15

                        objEquipmentInfo.UpdateRemarks_Tracking(drEqpInfo.Item(EquipmentInformationData.EQPMNT_NO).ToString, strRemarks, objTrans)

                        If br_dsEqpInfoDataset.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count > 0 Then
                            objEquipmentInfo.DeleteEquipmentInformationDetail(CLng(drEqpInfo.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID)), _
                                                                              objTrans)
                        End If
                        For Each drEqpDetail As DataRow In br_dsEqpInfoDataset.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, " = ", CommonUIs.iLng(drEqpInfo.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID))))
                            If drEqpDetail.RowState = DataRowState.Deleted Then
                                objEquipmentInfo.DeleteEquipmentInformationDetail(CLng(drEqpInfo.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID, DataRowVersion.Original)), _
                                                                                  objTrans)
                            Else
                                objEquipmentInfo.CreateEquipmentInformationDetail(CLng(drEqpDetail.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID)), _
                                                                                  CStr(drEqpDetail.Item(EquipmentInformationData.ATTCHMNT_PTH)), _
                                                                                  CStr(drEqpDetail.Item(EquipmentInformationData.ACTL_FL_NM)), _
                                                                                  objTrans)
                            End If
                        Next

                        ''CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                        'objEquipmentInfo.UpdateActivityStatus(CStr(drEqpInfo.Item(EquipmentInformationData.EQPMNT_NO)), _
                        '                                      strRemarks, _
                        '                                      objTrans)
                    Case DataRowState.Deleted
                        Dim objEquipment_Information As New EquipmentInformations
                        objEquipment_Information.DeleteEquipmentInformationDetail(CLng(drEqpInfo.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID, DataRowVersion.Original)), _
                                                                                  objTrans)
                        objEquipmentInfo.DeleteEquipmentInformation(CLng(drEqpInfo.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID, DataRowVersion.Original)), _
                                                                      bv_intDepotID, _
                                                                      objTrans)
                End Select
                
            Next
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function
#End Region

#Region "pub_GetEquipmentInformationFromActivityStatus() TABLE NAME:EQUIPMENT_INFORMATION"

    <OperationContract()> _
    Public Function pub_GetEquipmentInformationFromActivityStatus(ByVal bv_strEquipmentNo As String, ByVal bv_strDepotId As Int32) As Boolean

        Try
            Dim objEquipmentInformations As New EquipmentInformations
            Dim intRowCount As Integer = 0
            intRowCount = CInt(objEquipmentInformations.GetEquipmentInformationFromActivityStatus(bv_strEquipmentNo, bv_strDepotId))
            If intRowCount = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetEquipmentInfoGateIn() TABLE NAME:Equipment_Information"

    Public Function GetEquipmentInfoGateIn(ByVal strEquipmentNo As String, ByVal bv_intDepotID As Integer) As EquipmentInformationDataSet
        Try
            Dim dsEquipmentInformationData As EquipmentInformationDataSet
            Dim objEquipmentInformations As New EquipmentInformations
            dsEquipmentInformationData = objEquipmentInformations.GetEquipmentInfoGateIn(strEquipmentNo, bv_intDepotID)
            Return dsEquipmentInformationData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentInformationDetailByEqpID() Table Name: Equipment_Information_Detail"
    <OperationContract()> _
    Public Function pub_GetEquipmentInformationDetailByEqpID(ByVal bv_lngEquipmentInformationId As Long) As EquipmentInformationDataSet
        Try
            Dim dsEquipmentInformationData As EquipmentInformationDataSet
            Dim objEquipmentInformations As New EquipmentInformations
            dsEquipmentInformationData = objEquipmentInformations.GetEquipmentInformationDetailByEqpID(bv_lngEquipmentInformationId)
            Return dsEquipmentInformationData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetEquipmentInformationDetailByEqpNo() Table Name: pub_GetEquipmentInformationDetailByEqpNo"
    <OperationContract()> _
    Public Function pub_GetEquipmentInformationDetailByEqpNo(ByVal bv_strEquipmentNo As String) As EquipmentInformationDataSet
        Try
            Dim dsEquipmentInformationData As EquipmentInformationDataSet
            Dim objEquipmentInformations As New EquipmentInformations
            dsEquipmentInformationData = objEquipmentInformations.GetEquipmentInformationDetailByEqpNo(bv_strEquipmentNo)
            Return dsEquipmentInformationData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEquipmentInformationDetail() Table Name: Equipment_Information_Detail"
    <OperationContract()> _
    Public Function pub_GetEquipmentInformationDetail() As EquipmentInformationDataSet
        Try
            Dim dsEquipmentInformationData As EquipmentInformationDataSet
            Dim objEquipmentInformations As New EquipmentInformations
            dsEquipmentInformationData = objEquipmentInformations.GetEquipmentInformationDetail()
            Return dsEquipmentInformationData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEquipmentInfoRemaksTracking() TABLE NAME:Tracking"

    Public Function pub_GetEquipmentInfoRemaksTracking(ByVal bv_strEquipmentNo As String, _
                                                       ByVal bv_intDepotID As Integer, _
                                                       ByVal bv_strActivityName As String, _
                                                       ByVal bv_strGateinTransactionNo As String) As EquipmentInformationDataSet
        Try
            Dim dsEquipmentInformationData As EquipmentInformationDataSet
            Dim objEquipmentInformations As New EquipmentInformations
            Dim strWhere As String = String.Empty
            strWhere = " WHERE "
            If bv_intDepotID <> 0 Then
                strWhere = String.Concat(strWhere, "", EquipmentInformationData.DPT_ID, "=", bv_intDepotID, " ")
            End If
            If bv_strEquipmentNo <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", EquipmentInformationData.EQPMNT_NO, "='", bv_strEquipmentNo, "' ")
            End If
            If bv_strActivityName <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", EquipmentInformationData.ACTVTY_NAM, "='", bv_strActivityName, "' ")
            End If
            If bv_strGateinTransactionNo <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", EquipmentInformationData.GI_TRNSCTN_NO, "<>'", bv_strGateinTransactionNo, "' ")
            End If
            If strWhere.Length > 1 Then
                strWhere = String.Concat(strWhere, " ", "ORDER BY TRCKNG_ID DESC")
            End If
            dsEquipmentInformationData = objEquipmentInformations.GetEquipmentInfoRemaksTracking(strWhere)
            Return dsEquipmentInformationData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class
