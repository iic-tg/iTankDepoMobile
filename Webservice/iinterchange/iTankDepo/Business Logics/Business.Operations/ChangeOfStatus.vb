Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Business.Common

<ServiceContract()> _
Public Class ChangeOfStatus

#Region "pub_ActivityStatusGetActivityStatusByEquipmentNo() TABLE NAME:ACTIVITY_STATUS"

    <OperationContract()> _
    Public Function pub_GetActivityStatusBySearch(ByVal bv_strEquipmentStatusID As String, _
                                                  ByVal bv_strEquipmentNo As String, _
                                                  ByVal bv_strCustomerID As String, _
                                                  ByVal bv_strDepotID As Integer) As ChangeOfStatusDataSet

        Try
            Dim dsActivityStatusData As New ChangeOfStatusDataSet
            Dim dtActivityStatus As New DataTable
            Dim objActivityStatuss As New ChangeOfStatuss
            Dim strFilterColumn As String = String.Empty
            Dim strEquipmentStatusCode As String = String.Empty

            If Not (bv_strEquipmentNo) Is Nothing AndAlso bv_strEquipmentStatusID = Nothing Then
                dsActivityStatusData = objActivityStatuss.Get_Activity_Status(bv_strEquipmentNo, bv_strDepotID)
                dtActivityStatus = objActivityStatuss.Get_ActivityStatus(bv_strEquipmentNo, bv_strDepotID)
                If dtActivityStatus.Rows.Count > 0 Then
                    If Not IsDBNull(dtActivityStatus.Rows(0).Item(ChangeOfStatusData.EQPMNT_STTS_CD)) Then
                        bv_strEquipmentStatusID = CStr(dtActivityStatus.Rows(0).Item(ChangeOfStatusData.EQPMNT_STTS_ID))
                        strEquipmentStatusCode = CStr(dtActivityStatus.Rows(0).Item(ChangeOfStatusData.EQPMNT_STTS_CD))
                        dsActivityStatusData.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows.Clear()
                    End If
                End If
            End If

            If bv_strEquipmentStatusID <> Nothing Then
                dsActivityStatusData = objActivityStatuss.Get_WF_CHANGE_OF_STATUS(bv_strEquipmentStatusID, bv_strDepotID)
                If dsActivityStatusData.Tables(ChangeOfStatusData._WF_CHANGE_OF_STATUS).Rows.Count > 0 Then
                    If Not IsDBNull(dsActivityStatusData.Tables(ChangeOfStatusData._WF_CHANGE_OF_STATUS).Rows(0).Item(ChangeOfStatusData.FILTER_COLUMN)) Then
                        strFilterColumn = CStr(dsActivityStatusData.Tables(ChangeOfStatusData._WF_CHANGE_OF_STATUS).Rows(0).Item(ChangeOfStatusData.FILTER_COLUMN))
                    End If
                    dsActivityStatusData = objActivityStatuss.pub_ActivityStatusByActivityName(bv_strEquipmentStatusID, _
                                                                                      bv_strEquipmentNo, _
                                                                                      bv_strCustomerID, _
                                                                                      bv_strDepotID, _
                                                                                      strFilterColumn)
                End If
            End If

            Return dsActivityStatusData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateChangeofStatus() TABLE NAME:Activity_Status"

    <OperationContract()> _
    Public Function pub_UpdateChangeofStatus(ByRef br_dsCommon As ChangeOfStatusDataSet, _
                                             ByVal bv_strModifyBy As String, _
                                             ByVal bv_datModifyDate As DateTime, _
                                             ByVal bv_intDepotID As Int32, _
                                             ByVal bv_strWfData As String, _
                                             ByVal bv_strCleaningFromCode As String, _
                                             ByVal bv_strCleaningToCode As String, _
                                             ByVal bv_strCleanedFor As String, _
                                             ByVal bv_strLocOfCleaning As String, _
                                             ByVal bv_intCleaningStatus1 As Integer, _
                                             ByVal bv_intCleaningStatus2 As Integer, _
                                             ByVal bv_intConditionId As Integer, _
                                             ByVal bv_intValveConditionId As Integer) As Boolean

        Dim objTransaction As New Transactions
        Try
            Dim objActivityStatus As New ChangeOfStatuss
            Dim objCommonUIs As New CommonUIs
            Dim blnUpdated As Boolean
            Dim dtEquipmentInformation As New DataTable
            Dim strEquipmentInfoRemarks As String = String.Empty
            Dim objCleanings As New Cleanings
            For Each drActivityStatus As DataRow In br_dsCommon.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Select(String.Concat(ChangeOfStatusData.CHECKED, "=", "True"))

                If Not IsDBNull(drActivityStatus.Item(ChangeOfStatusData.NEW_EQPMNT_STTS_CD)) AndAlso Not IsDBNull(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)) Then
                    'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                    strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drActivityStatus.Item(GateinData.EQPMNT_NO).ToString, _
                                                                                   bv_intDepotID, _
                                                                                   objTransaction)

                    If dtEquipmentInformation.Rows.Count > 0 Then
                        With dtEquipmentInformation.Rows(0)
                            If Not IsDBNull(.Item(GateinData.RMRKS_VC)) Then
                                strEquipmentInfoRemarks = CStr(.Item(GateinData.RMRKS_VC))
                            End If
                        End With
                    End If

                    If CStr(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_STTS_CD)) = bv_strCleaningFromCode AndAlso CStr(drActivityStatus.Item(ChangeOfStatusData.NEW_EQPMNT_STTS_CD)) = bv_strCleaningToCode Then
                       
                        Dim str073 As String = objCommonUIs.DecryptString(CStr(objCommonUIs.GetConfigByKeyName("073", CLng(IIf(objCommonUIs.GetMultiLocationSupportConfig.ToLower = "true", objCommonUIs.GetHeadQuarterID(), bv_intDepotID))).Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL)))

                        Dim lngCreated As Long = 0
                        Dim decCleaningRate As String
                        Dim dtCleaningRateDetail As DataTable
                        Dim strCleaningCertificateNo As String = String.Empty
                        Dim strGiRefNo As String = String.Empty
                        Dim strCstmr_id As Integer
                        Dim blnCustomerProductRate As Boolean
                        Dim blnProductRate As Boolean
                        Dim blnSlabRateExists As Boolean
                        Dim blnSlabRate As Boolean
                        Dim strEquipType_id As Integer
                        If Not IsDBNull(drActivityStatus.Item(ChangeOfStatusData.GI_RF_NO)) Then
                            strGiRefNo = CStr(drActivityStatus.Item(ChangeOfStatusData.GI_RF_NO))
                        End If

                        Dim intCleaningCombination As Int32 = 0

                        intCleaningCombination = objActivityStatus.CheckCleaningCombination(CStr(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO)), CStr(drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO)), CInt(drActivityStatus.Item(ChangeOfStatusData.CSTMR_ID)), bv_intDepotID, objTransaction)

                        If CBool(drActivityStatus.Item(ChangeOfStatusData.ADDTNL_CLNNG_BT)) = False Then
                            dtCleaningRateDetail = objActivityStatus.GetCleaningRate(CStr(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO)), _
                                                                                bv_intDepotID, _
                                                                                CStr(drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO)), _
                                                                                objTransaction)
                            decCleaningRate = CStr(dtCleaningRateDetail.Rows(0).Item(ChangeOfStatusData.CLNNG_RT))
                            ''Slab Rate changes
                            If str073.ToLower = "true" Then
                                Dim objCleaning As New Cleaning
                                blnCustomerProductRate = CBool(dtCleaningRateDetail.Rows(0).Item("CLNNG_CSTMR_PRDCT_RT_BT"))
                                blnProductRate = CBool(dtCleaningRateDetail.Rows(0).Item("CLNNG_PRDCT_RT_BT"))
                                strCstmr_id = CInt(drActivityStatus.Item(ChangeOfStatusData.CSTMR_ID))
                                strEquipType_id = CInt(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_TYP_ID))
                                blnSlabRateExists = objCleaning.pub_CheckSlabRateExists(strCstmr_id, strEquipType_id)
                                If blnProductRate = True AndAlso decCleaningRate = "0.00" AndAlso blnCustomerProductRate = False AndAlso blnSlabRateExists = False Then      'No rates are available
                                    decCleaningRate = Nothing
                                    blnSlabRate = False

                                ElseIf blnProductRate = True AndAlso blnCustomerProductRate = False AndAlso blnSlabRateExists = False Then  'Customer specific product rate available
                                    blnSlabRate = False

                                ElseIf blnProductRate = True AndAlso blnCustomerProductRate = True AndAlso blnSlabRateExists = False Then  'Customer specific product rate and Product Rate available
                                    blnSlabRate = False

                                ElseIf blnProductRate = True AndAlso blnCustomerProductRate = True AndAlso blnSlabRateExists = True Then  'Customer specific product rate, product rate and slab rate available 
                                    blnSlabRate = False

                                ElseIf blnProductRate = True AndAlso blnCustomerProductRate = False AndAlso blnSlabRateExists = True Then  'Customer specific product rate And Slab rate available
                                    blnSlabRate = True

                                ElseIf blnProductRate = False AndAlso blnCustomerProductRate = False AndAlso blnSlabRateExists = True Then  'Only Slab Rate available
                                    blnSlabRate = True
                                End If

                            End If
                            If intCleaningCombination = 0 Then
                                lngCreated = objCleanings.CreateCleaning(CLng(drActivityStatus.Item(ChangeOfStatusData.CSTMR_ID)), _
                                                                     CStr(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO)), _
                                                                     strCleaningCertificateNo, _
                                                                     CStr(drActivityStatus.Item(ChangeOfStatusData.CHMCL_NM)), _
                                                                     CStr(decCleaningRate), _
                                                                     CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                                     CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                                     CDate(Now),
                                                                     CDate(Now), _
                                                                     CInt(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_STTS_ID)), _
                                                                     bv_strCleanedFor, _
                                                                     bv_strLocOfCleaning, _
                                                                     bv_intCleaningStatus1, _
                                                                     bv_intCleaningStatus2, _
                                                                     bv_intConditionId, _
                                                                     bv_intValveConditionId, _
                                                                     Nothing, _
                                                                     Nothing, _
                                                                     Nothing, _
                                                                     Nothing, _
                                                                     strGiRefNo, _
                                                                     Nothing, _
                                                                     Nothing,
                                                                     bv_intDepotID, _
                                                                     CStr(drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO)), _
                                                                     bv_strModifyBy, _
                                                                     bv_datModifyDate, _
                                                                     bv_strModifyBy, _
                                                                     bv_datModifyDate, _
                                                                     False, _
                                                                     "C", _
                                                                     False, _
                                                                     False, _
                                                                     objTransaction)

                                objCleanings.CreateCleaningCharge(CStr(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO)), _
                                                                  CLng(drActivityStatus.Item(ChangeOfStatusData.CSTMR_ID)), _
                                                                  Nothing, _
                                                                  lngCreated, _
                                                                  CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                                  CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                                  CStr(decCleaningRate), _
                                                                  "U", _
                                                                  strCleaningCertificateNo, _
                                                                  True, _
                                                                  bv_intDepotID, _
                                                                  CStr(drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO)), _
                                                                  blnSlabRate, _
                                                                  objTransaction)
                            Else
                                objCleanings.UpdateCleaning_CleanCombination(CLng(drActivityStatus.Item(ChangeOfStatusData.CSTMR_ID)), CStr(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO)), _
                                                             CStr(drActivityStatus.Item(ChangeOfStatusData.CHMCL_NM)), CStr(decCleaningRate), _
                                                            CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), CInt(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_STTS_ID)), _
                                                            Nothing, _
                                                           Nothing, bv_intDepotID, _
                                                           CStr(drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO)), bv_strModifyBy, _
                                                           bv_datModifyDate, bv_strModifyBy, _
                                                           bv_datModifyDate, Nothing, Nothing, CBool(drActivityStatus.Item(ChangeOfStatusData.ADDTNL_CLNNG_BT)), False, objTransaction)

                                objCleanings.UpdateCleaningCharge_CleanCombination(CStr(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO)), _
                                                                CLng(drActivityStatus.Item(ChangeOfStatusData.CSTMR_ID)), _
                                                               lngCreated, _
                                                                CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                               CStr(decCleaningRate), _
                                                               "U", _
                                                               True, _
                                                               bv_intDepotID, _
                                                               CStr(drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO)), _
                                                               blnSlabRate, _
                                                               objTransaction)
                            End If

                        Else
                            objActivityStatus.UpdateCleaning(CLng(drActivityStatus.Item(ChangeOfStatusData.CLNNG_ID)), _
                                                             bv_intDepotID, _
                                                             CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                             CDate(Now), _
                                                             5, _
                                                             drActivityStatus.Item(ChangeOfStatusData.RMRKS_VC).ToString, _
                                                             False, _
                                                             objTransaction)
                            lngCreated = CLng(drActivityStatus.Item(ChangeOfStatusData.CLNNG_ID))



                        End If

                        objCleanings.UpdateActivityStatus(CStr(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO)), _
                                                          CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                          CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                          CInt(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_STTS_ID)), _
                                                          "Cleaning", _
                                                          CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                          drActivityStatus.Item(ChangeOfStatusData.RMRKS_VC).ToString, _
                                                          CStr(drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO)), _
                                                          strGiRefNo, _
                                                          bv_intDepotID, _
                                                          objTransaction)
                        objCommonUIs.CreateTracking(lngCreated, _
                                                   CLng(drActivityStatus.Item(ChangeOfStatusData.CSTMR_ID)), _
                                                   CStr(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO)), _
                                                   "Cleaning", _
                                                   CInt(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_STTS_ID)), _
                                                   CStr(lngCreated), _
                                                   CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                   drActivityStatus.Item(ChangeOfStatusData.RMRKS_VC).ToString, _
                                                   drActivityStatus.Item(ChangeOfStatusData.YRD_LCTN).ToString, _
                                                   CStr(drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO)), _
                                                   Nothing, _
                                                   strCleaningCertificateNo, _
                                                   bv_strModifyBy, _
                                                   bv_datModifyDate, _
                                                   bv_strModifyBy, _
                                                   bv_datModifyDate, _
                                                   Nothing, _
                                                   Nothing, _
                                                   Nothing, _
                                                   bv_intDepotID, _
                                                   0, _
                                                   Nothing, _
                                                   strEquipmentInfoRemarks, _
                                                   False, _
                                                   objTransaction)

                        'objCommonUIs.CreateTracking(lngCreated, _
                        '                            CLng(drActivityStatus.Item(ChangeOfStatusData.CSTMR_ID)), _
                        '                            CStr(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO)), _
                        '                            "Inspection", _
                        '                            6, _
                        '                            CStr(lngCreated), _
                        '                            CDate(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                        '                            drActivityStatus.Item(ChangeOfStatusData.RMRKS_VC).ToString, _
                        '                            drActivityStatus.Item(ChangeOfStatusData.YRD_LCTN).ToString, _
                        '                            CStr(drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO)), _
                        '                            Nothing, _
                        '                            strCleaningCertificateNo, _
                        '                            bv_strModifyBy, _
                        '                            bv_datModifyDate, _
                        '                            bv_strModifyBy, _
                        '                            bv_datModifyDate, _
                        '                            Nothing, _
                        '                            Nothing, _
                        '                            Nothing, _
                        '                            bv_intDepotID, _
                        '                            0, _
                        '                            Nothing, _
                        '                            strEquipmentInfoRemarks, _
                        '                            False, _
                        '                            objTransaction)

                        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, String.Concat("EQPMNT_NO : ", drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO), _
                                                                         "  GI_TRNSCTN_NO : ", drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO), _
                                                                        "   ADDTNL_CLNNG_BT : ", drActivityStatus.Item(ChangeOfStatusData.ADDTNL_CLNNG_BT), _
                                                                        "  EQUIPMENT STATUS ID : ", drActivityStatus.Item(ChangeOfStatusData.EQPMNT_STTS_ID)))
                    Else
                        objCommonUIs.CreateTracking(Nothing, _
                                                    CommonUIs.iLng(drActivityStatus.Item(ChangeOfStatusData.CSTMR_ID)), _
                                                    drActivityStatus.Item(ChangeOfStatusData.EQPMNT_NO).ToString, _
                                                    "Change of Status", _
                                                    CommonUIs.iLng(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_STTS_ID)), _
                                                    drActivityStatus.Item(ChangeOfStatusData.ACTVTY_STTS_ID).ToString, _
                                                    CommonUIs.iDat(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                    drActivityStatus.Item(ChangeOfStatusData.RMRKS_VC).ToString, _
                                                    drActivityStatus.Item(ChangeOfStatusData.YRD_LCTN).ToString, _
                                                    drActivityStatus.Item(ChangeOfStatusData.GI_TRNSCTN_NO).ToString, _
                                                    Nothing, _
                                                    Nothing, _
                                                    bv_strModifyBy, _
                                                    bv_datModifyDate, _
                                                    bv_strModifyBy, _
                                                    bv_datModifyDate, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    bv_intDepotID, _
                                                    0, _
                                                    Nothing, _
                                                    strEquipmentInfoRemarks, _
                                                    False, _
                                                    objTransaction)

                        blnUpdated = objCommonUIs.UpdateActivityStatus(CommonUIs.iLng(drActivityStatus.Item(ChangeOfStatusData.ACTVTY_STTS_ID)), _
                                                                       CommonUIs.iLng(drActivityStatus.Item(ChangeOfStatusData.EQPMNT_STTS_ID)), _
                                                                       CommonUIs.iDat(drActivityStatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT)), _
                                                                       drActivityStatus.Item(ChangeOfStatusData.RMRKS_VC).ToString, _
                                                                       bv_intDepotID, _
                                                                       drActivityStatus.Item(ChangeOfStatusData.YRD_LCTN).ToString, _
                                                                       objTransaction)
                    End If

                End If
            Next
            objTransaction.commit()
            Return blnUpdated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function
#End Region


    Public Function pub_GetNewEquipStatus(ByVal equipStatus As String, ByVal depotId As Integer) As Integer
        Dim objChangeofStatuss As New ChangeOfStatuss
        Dim equipStatusID As Integer
        equipStatusID = CInt(objChangeofStatuss.pub_GetNewEquipStatus(equipStatus, depotId))
        Return equipStatusID
    End Function

End Class