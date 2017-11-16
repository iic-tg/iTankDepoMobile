Imports Microsoft.VisualBasic

Public Class EquipmentInfoMobile_C
    Inherits Framebase


    Public Function EquipmentInfoList(ByVal EquipmentNo As String, ByVal PageName As String, ByVal GateinTransactionNo As String, ByVal Attachment As String) As EquipmentInformationDataSet
        ' Dim dtEquipmentInformationDocument As DataTable


        Dim ObjEquipInfo As New EquipmentInformation()
        Dim objCommon As New CommonData
        Dim objCommonConfig As New ConfigSetting()
        Dim intDepotID As Integer = objCommon.GetDepotID()
        Dim strEqpmntNo As String = String.Empty
        Dim dtEqDetail As New DataTable
        Dim str_053GWS As String
        Dim bln_053GWSActive_Key As Boolean
        Dim str_012EqType As String

        Dim bln_012EqType_Key As Boolean
        ''Release - 2
        Dim dsEquipmentInformationData As New EquipmentInformationDataSet
        strEqpmntNo = EquipmentNo
        If PageName <> Nothing AndAlso PageName = "GateIn" Then

            Dim strGateinTransactionNo As String = String.Empty
            If GateinTransactionNo <> Nothing Then
                strGateinTransactionNo = GateinTransactionNo
            End If
            If Attachment.ToUpper <> "TRUE" Then
                dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows.Clear()
                dtEqDetail = ObjEquipInfo.GetEquipmentInfoGateIn(strEqpmntNo, intDepotID).Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION)

                'CHECK WHETHER REMARKS IS NEW FOR CONSIGNMENT OR LAST CYCLE.
                If dtEqDetail.Rows.Count > 0 Then
                    Dim strEquipmentInfoRemarks As String = String.Empty
                    Dim strHistroyRemarks As String = String.Empty
                    Dim dtTracking As New DataTable
                    dtTracking = dsEquipmentInformationData.Tables(EquipmentInformationData._TRACKING).Clone()
                    If Not IsDBNull(dtEqDetail.Rows(0).Item(EquipmentInformationData.RMRKS_VC)) Then
                        strEquipmentInfoRemarks = dtEqDetail.Rows(0).Item(EquipmentInformationData.RMRKS_VC)
                    End If
                    dtTracking = ObjEquipInfo.pub_GetEquipmentInfoRemaksTracking(strEqpmntNo, intDepotID, "Gate Out", strGateinTransactionNo).Tables(EquipmentInformationData._TRACKING)
                    If dtTracking.Rows.Count > 0 Then
                        If Not IsDBNull(dtTracking.Rows(0).Item(EquipmentInformationData.EQPMNT_INFRMTN_RMRKS_VC)) Then
                            strHistroyRemarks = dtTracking.Rows(0).Item(EquipmentInformationData.EQPMNT_INFRMTN_RMRKS_VC)
                        End If
                    End If
                    If strEquipmentInfoRemarks = strHistroyRemarks Then
                        dtEqDetail.Rows(0).Item(EquipmentInformationData.RMRKS_VC) = DBNull.Value
                    Else
                        dtEqDetail.Rows(0).Item(EquipmentInformationData.RMRKS_VC) = strEquipmentInfoRemarks
                    End If
                End If
                ''END CHECK

            End If

        Else
            ''ifgEquipmentInformation.AllowSearch = True
            ''btnSubmitEq.Visible = False
            'If Attachment.ToUpper() <> "TRUE" Then
            '    dtEqDetail = ObjEquipInfo.GetEquipmentInformation(intDepotID).Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION)
            'Else
            '    dsEquipmentInformationData = CType(RetrieveData(EQUIPMENT_INFORMATION), EquipmentInformationDataSet)
            'End If
        End If
        dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Merge(dtEqDetail)
        If dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows.Count = 0 Then
            Dim drEqInfo As DataRow = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).NewRow()

            Dim objCommonUI As New CommonUI()
            Dim dsEqpmntType As New CommonUIDataSet
            str_012EqType = objCommonConfig.pub_GetConfigSingleValue("012", intDepotID)
            bln_012EqType_Key = objCommonConfig.IsKeyExists

            If bln_012EqType_Key Then
                If Not str_012EqType = "" Then
                    If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                        dsEqpmntType = objCommonUI.pub_GetEquipmentType(str_012EqType, objCommon.GetHeadQuarterID())
                    Else
                        dsEqpmntType = objCommonUI.pub_GetEquipmentType(str_012EqType, intDepotID)
                    End If

                    If dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                        drEqInfo.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID) = CommonWeb.GetNextIndex(dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION), EquipmentInformationData.EQPMNT_INFRMTN_ID)
                        drEqInfo.Item(EquipmentInformationData.EQPMNT_TYP_ID) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                        drEqInfo.Item(EquipmentInformationData.EQPMNT_TYP_CD) = str_012EqType
                        drEqInfo.Item(EquipmentInformationData.EQPMNT_TYP_DSCRPTN_VC) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_DSCRPTN_VC).ToString
                        If strEqpmntNo <> Nothing Then
                            drEqInfo.Item(EquipmentInformationData.EQPMNT_NO) = strEqpmntNo
                            drEqInfo.Item(EquipmentInformationData.ALLOW_EDIT) = False
                        End If
                        drEqInfo.Item(EquipmentInformationData.ACTV_BT) = True
                    End If
                End If
            End If
            dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows.Add(drEqInfo)
        End If

        str_053GWS = objCommonConfig.pub_GetConfigSingleValue("053", intDepotID)
        bln_053GWSActive_Key = objCommonConfig.IsKeyExists
        Dim objCommondata As New CommonData
        If str_053GWS Then

        End If
        'e.DataSource = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION)
        If Attachment.ToUpper = "TRUE" Then
            Dim drSelect As DataRow() = Nothing
            Dim intCount As Integer = 0
            If dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count > 0 Then
                drSelect = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_NO, " = '", strEqpmntNo, "'"))
                If drSelect.Length > 0 Then
                    intCount = CInt(dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Compute(String.Concat("COUNT(", EquipmentInformationData.EQPMNT_INFRMTN_ID, ")"), String.Concat(EquipmentInformationData.EQPMNT_NO, " = '", strEqpmntNo, "'")))
                    drSelect(0).Item(EquipmentInformationData.COUNT_ATTACH) = intCount
                End If
            Else
                drSelect = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_NO, " = '", strEqpmntNo, "'"))
                If drSelect.Length > 0 Then
                    intCount = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Compute(String.Concat("COUNT(", EquipmentInformationData.EQPMNT_INFRMTN_ID, ")"), String.Concat(EquipmentInformationData.EQPMNT_NO, " = '", strEqpmntNo, "'"))
                    drSelect(0).Item(EquipmentInformationData.COUNT_ATTACH) = intCount
                End If
            End If
        End If


        Return dsEquipmentInformationData
    End Function










    Public Function pvt_HasChangeEquipmentInformation(ByRef br_dsEquipmentInformation As EquipmentInformationDataSet) As EquipmentInformationDataSet


        Try
            Dim dtEqpInforDetail As New DataTable
            Dim distnictData As New DataTable
            dtEqpInforDetail = br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Clone()
            For Each drEqDetail As DataRow In br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows
                If drEqDetail.RowState <> DataRowState.Deleted Then
                    dtEqpInforDetail.ImportRow(drEqDetail)
                End If
            Next
            If br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count > 0 Then
                distnictData = br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).GetChanges()
                For Each drEqpInfoDetail As DataRow In distnictData.Rows
                    Dim drs As DataRow()
                    If drEqpInfoDetail.RowState = DataRowState.Deleted Then
                        drs = br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, "=", drEqpInfoDetail.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID, DataRowVersion.Original)), "")
                    Else
                        drs = br_dsEquipmentInformation.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_INFRMTN_ID, "=", drEqpInfoDetail.Item(EquipmentInformationData.EQPMNT_INFRMTN_ID)), "")
                    End If
                    If drs.Length > 0 Then
                        drs(0).Item(EquipmentInformationData.HAS_CHANGE) = True
                    End If
                Next
            End If


            Return br_dsEquipmentInformation
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)


        End Try

    End Function

End Class
