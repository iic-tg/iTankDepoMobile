Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
Imports System.Text

<ServiceContract()> _
Public Class EquipmentUpdate


#Region "pub_GetActivityStatusByEqpmntNo"
    <OperationContract()> _
    Public Function pub_GetEquipmentInformationByEqpmntNo(ByVal bv_strEqpmntNo As String, _
                                                          ByVal bv_i32DepotId As Int32) As EquipmentUpdateDataSet
        Try
            Dim objEquipmentUpdates As New EquipmentUpdates
            Dim dsEquipmentUpdate As New EquipmentUpdateDataSet
            dsEquipmentUpdate = objEquipmentUpdates.GetEquipmentInformationByEqpmntNo(bv_strEqpmntNo, bv_i32DepotId)
            Return dsEquipmentUpdate
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_UpdateEquipmentUpdate"
    <OperationContract()> _
    Public Function pub_UpdateEquipmentUpdate(ByVal bv_strNewEquipmentNo As String, _
                                              ByVal bv_strNewCustomerId As String, _
                                              ByVal bv_strNewCustomerCd As String, _
                                              ByVal bv_strNewTypeId As String, _
                                              ByVal bv_strNewTypeCd As String, _
                                              ByVal bv_strNewCodeId As String, _
                                              ByVal bv_strNewCodeCd As String, _
                                              ByVal bv_strOldEquipmentNo As String, _
                                              ByVal bv_strOldTypeId As String, _
                                              ByVal bv_strOldTypeCd As String, _
                                              ByVal bv_strOldCodeId As String, _
                                              ByVal bv_strOldCodeCd As String, _
                                              ByVal bv_strOldCustomerId As String, _
                                              ByVal bv_strOldCustomerCd As String, _
                                              ByRef br_dsEquipmentUpdate As EquipmentUpdateDataSet, _
                                              ByVal bv_blnBillingBit As Boolean, _
                                              ByVal bv_strReason As String, _
                                              ByVal bv_intDepotId As Int32, _
                                              ByVal bv_strUserName As String, _
                                              ByVal bv_datModifiedDate As DateTime) As Boolean
        Dim dtEquipmentUpdate As New DataTable
        Dim dtTable As New DataTable
        Dim objTransaction As New Transactions
        Dim strEquipmentNumber As String = ""
        Try
            Dim objEquipmentUpdates As New EquipmentUpdates
            Dim drNewEqUpdate As DataRow
            Dim sbrOldValue As New StringBuilder
            Dim sbrNewValue As New StringBuilder
            Dim sbrOldEqpUpdate As New StringBuilder
            Dim sbrNewEqpUpdate As New StringBuilder
            Dim sbrHeaderName As New StringBuilder
            Dim objEquipmentInformations As New EquipmentInformations
            Dim i64EquipmentTypeId As Int64
            Dim strCustomer As String = String.Empty
            Dim strEquipmentNo As String = String.Empty
            Dim strEquipmentType As String = String.Empty
            Dim strEquipmentCode As String = String.Empty
            Dim i64EquipmentCodeId As Int64
            Dim strOldJTSEIRNo As String = ""
            Dim strNewJTSEIRNo As String = ""
            Dim strEIRNo As String = ""
            Dim strEIRNoUpdate As String = ""
            Dim blnActivityStatus As Boolean = False
            Dim strEqpUpdate As String = ""
            Dim strGateinTransaction As String = ""
            If bv_strNewTypeId <> "" Then
                i64EquipmentTypeId = CLng(bv_strNewTypeId)
            Else
                i64EquipmentTypeId = CLng(bv_strOldTypeId)
            End If

            'Type & Code Merge 
            If bv_strNewEquipmentNo <> "" Then
                strEquipmentNo = bv_strNewEquipmentNo
            Else
                strEquipmentNo = bv_strOldEquipmentNo
            End If

            If bv_strNewCustomerCd <> "" Then
                strCustomer = bv_strNewCustomerCd
            Else
                strCustomer = bv_strOldCustomerCd
            End If

            If bv_strNewTypeCd <> "" Then
                strEquipmentType = bv_strNewTypeCd
            Else
                strEquipmentType = bv_strOldTypeCd
            End If

            If bv_strNewCodeCd <> "" Then
                strEquipmentCode = bv_strNewCodeCd
            Else
                strEquipmentCode = bv_strOldCodeCd
            End If

            If bv_strNewCodeId <> "" Then
                i64EquipmentCodeId = CLng(bv_strNewCodeId)
            Else
                i64EquipmentCodeId = CLng(bv_strOldCodeId)
            End If

            If br_dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows.Count > 0 Then
                If Not IsDBNull(br_dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows(0).Item(EquipmentUpdateData.GI_TRNSCTN_NO)) Then
                    strGateinTransaction = br_dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows(0).Item(EquipmentUpdateData.GI_TRNSCTN_NO).ToString
                    strNewJTSEIRNo = br_dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString
                End If
            End If
            If Not br_dsEquipmentUpdate.Tables("OLD_V_EQUIPMENT_UPDATE") Is Nothing Then
                If br_dsEquipmentUpdate.Tables("OLD_V_EQUIPMENT_UPDATE").Rows.Count > 0 Then
                    strOldJTSEIRNo = br_dsEquipmentUpdate.Tables("OLD_V_EQUIPMENT_UPDATE").Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString
                End If
            End If

            br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).Clear()
            drNewEqUpdate = br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).NewRow
            drNewEqUpdate.Item(EquipmentUpdateData.EQPMNT_NO) = bv_strNewEquipmentNo
            drNewEqUpdate.Item(EquipmentUpdateData.EQPMNT_TYP_ID) = bv_strNewTypeId
            drNewEqUpdate.Item(EquipmentUpdateData.EQPMNT_TYP_CD) = bv_strNewTypeCd
            drNewEqUpdate.Item(EquipmentUpdateData.EQPMNT_CD_ID) = bv_strNewCodeId
            drNewEqUpdate.Item(EquipmentUpdateData.EQPMNT_CD_CD) = bv_strNewCodeCd
            drNewEqUpdate.Item(EquipmentUpdateData.GI_TRNSCTN_NO) = strGateinTransaction
            drNewEqUpdate.Item(EquipmentUpdateData.RFRNC_EIR_NO_1) = strNewJTSEIRNo
            drNewEqUpdate.Item(EquipmentUpdateData.CSTMR_ID) = bv_strNewCustomerId
            drNewEqUpdate.Item(EquipmentUpdateData.CSTMR_CD) = bv_strNewCustomerCd
            'drNewEqUpdate.Item(EquipmentUpdateData.RFRNC_NO) = strEIRNoUpdate
            drNewEqUpdate.Item(EquipmentUpdateData.OLD_EQPMNT_NO) = bv_strOldEquipmentNo
            drNewEqUpdate.Item(EquipmentUpdateData.OLD_EQPMNT_TYP_ID) = bv_strOldTypeId
            drNewEqUpdate.Item(EquipmentUpdateData.OLD_EQPMNT_TYP_CD) = bv_strOldTypeCd
            drNewEqUpdate.Item(EquipmentUpdateData.OLD_EQPMNT_CD_ID) = bv_strOldCodeId
            drNewEqUpdate.Item(EquipmentUpdateData.OLD_EQPMNT_CD_CD) = bv_strOldCodeCd
            drNewEqUpdate.Item(EquipmentUpdateData.OLD_GI_TRNSCTN_NO) = strGateinTransaction
            drNewEqUpdate.Item(EquipmentUpdateData.OLD_RFRNC_EIR_NO_1) = strOldJTSEIRNo
            drNewEqUpdate.Item(EquipmentUpdateData.OLD_CSTMR_ID) = bv_strOldCustomerId
            drNewEqUpdate.Item(EquipmentUpdateData.OLD_CSTMR_CD) = bv_strOldCustomerCd
            'drNewEqUpdate.Item(EquipmentUpdateData.OLD_RFRNC_NO) = strEIRNoUpdate
            br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).Rows.Add(drNewEqUpdate)

            'Type & Code Merge

            Dim objCommon As New CommonUIs
            Dim dtEquipmentType As DataTable
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                dtEquipmentType = objEquipmentUpdates.GetEquipmentType(CInt(objCommon.GetHeadQuarterID()), objTransaction)
            Else
                dtEquipmentType = objEquipmentUpdates.GetEquipmentType(bv_intDepotId, objTransaction)
            End If

            Dim strEquipmentTypeDescription As String = String.Empty

            If dtEquipmentType.Rows.Count > 0 Then

                Dim dr() As DataRow = dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", i64EquipmentTypeId))

                If dr.Length > 0 Then
                    strEquipmentTypeDescription = dr(0).Item(EquipmentTypeData.EQPMNT_TYP_DSCRPTN_VC).ToString()
                    strEquipmentType = dr(0).Item(EquipmentTypeData.EQPMNT_GRP_CD).ToString()
                End If

            End If

           

            For Each drAdditionalUpdate As DataRow In br_dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows
                'If drAdditionalUpdate.RowState <> DataRowState.Unchanged Then
                Dim datLastTestDate As DateTime = Nothing
                Dim datNextTestDate As DateTime = Nothing
                Dim i64LastTestTypeId As Int64 = 0
                Dim i64NextTestTypeId As Int64 = 0
                Dim strRemarks As String = String.Empty
                Dim strValidityYears As String = String.Empty
                If Not IsDBNull(drAdditionalUpdate.Item(EquipmentUpdateData.LST_TST_DT)) Then
                    datLastTestDate = CDate(drAdditionalUpdate.Item(EquipmentUpdateData.LST_TST_DT))
                End If
                If Not IsDBNull(drAdditionalUpdate.Item(EquipmentUpdateData.NXT_TST_DT)) Then
                    datNextTestDate = CDate(drAdditionalUpdate.Item(EquipmentUpdateData.NXT_TST_DT))
                End If
                If Not IsDBNull(drAdditionalUpdate.Item(EquipmentUpdateData.LST_TST_TYP_ID)) Then
                    i64LastTestTypeId = CLng(drAdditionalUpdate.Item(EquipmentUpdateData.LST_TST_TYP_ID))
                End If
                If Not IsDBNull(drAdditionalUpdate.Item(EquipmentUpdateData.NXT_TST_TYP_ID)) Then
                    i64NextTestTypeId = CLng(drAdditionalUpdate.Item(EquipmentUpdateData.NXT_TST_TYP_ID))
                End If
                If Not IsDBNull(drAdditionalUpdate.Item(EquipmentUpdateData.INSTRCTNS_VC)) Then
                    strRemarks = CStr(drAdditionalUpdate.Item(EquipmentUpdateData.INSTRCTNS_VC))
                End If
                If Not IsDBNull(drAdditionalUpdate.Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS)) Then
                    strValidityYears = CStr(drAdditionalUpdate.Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS))
                End If
                If bv_strNewEquipmentNo <> "" Then
                    strEquipmentNumber = bv_strNewEquipmentNo
                Else
                    strEquipmentNumber = bv_strOldEquipmentNo
                End If
                Dim dtActivityStatus As New DataTable
                Dim dtEquipmentInformation As New DataTable
                Dim strEIEquipmentNo As String = String.Empty
                Dim strEqpNo As String = String.Empty
                dtActivityStatus = objEquipmentUpdates.GetActivityStatusByEqpmntNo(bv_strOldEquipmentNo, bv_intDepotId, objTransaction)
                If bv_strNewEquipmentNo <> "" Then
                    dtEquipmentInformation = objEquipmentUpdates.GetEquipmentInformationByEqpmntNo(bv_strNewEquipmentNo, bv_intDepotId, objTransaction)
                    If dtEquipmentInformation.Rows.Count > 0 Then
                        If CInt(dtEquipmentInformation.Rows(0).Item(EquipmentUpdateData.EQPMNT_INFRMTN_ID)) = 0 AndAlso dtActivityStatus.Rows.Count >= 1 Then
                            blnActivityStatus = True
                            strEIEquipmentNo = bv_strNewEquipmentNo
                        ElseIf CInt(dtEquipmentInformation.Rows(0).Item(EquipmentUpdateData.EQPMNT_INFRMTN_ID)) = 1 AndAlso dtActivityStatus.Rows.Count = 1 Then
                            blnActivityStatus = False
                            strEIEquipmentNo = bv_strNewEquipmentNo
                        End If

                    Else
                        If CInt(dtEquipmentInformation.Rows(0).Item(EquipmentUpdateData.EQPMNT_INFRMTN_ID)) = 1 Then
                            blnActivityStatus = False
                            strEIEquipmentNo = bv_strOldEquipmentNo
                        End If
                    End If
                Else
                    blnActivityStatus = False
                    strEIEquipmentNo = bv_strOldEquipmentNo
                End If

                Dim blnInvoiceGenBit As Boolean = False
                If Not IsDBNull(drAdditionalUpdate.Item(EquipmentUpdateData.INVC_GNRT_BT)) Then
                    blnInvoiceGenBit = CBool(drAdditionalUpdate.Item(EquipmentUpdateData.INVC_GNRT_BT))
                End If

                If blnInvoiceGenBit Then
                    objEquipmentUpdates.UpdateActivityStatus(drAdditionalUpdate.Item(EquipmentUpdateData.EQPMNT_NO).ToString(),
                                                             CLng(drAdditionalUpdate.Item(EquipmentUpdateData.PRDCT_ID)), _
                                                             drAdditionalUpdate.Item(EquipmentUpdateData.GI_RF_NO).ToString(), _
                                                             drAdditionalUpdate.Item(EquipmentUpdateData.INSTRCTNS_VC).ToString(), _
                                                             drAdditionalUpdate.Item(EquipmentUpdateData.DPT_ID).ToString(), _
                                                             drAdditionalUpdate.Item(EquipmentUpdateData.EQPMNT_TYP_ID).ToString(), _
                                                             drAdditionalUpdate.Item(EquipmentUpdateData.OLD_EQPMNT_CD_ID).ToString(), _
                                                             drAdditionalUpdate.Item(EquipmentUpdateData.CSTMR_ID).ToString(), _
                                                             strGateinTransaction, _
                                                             objTransaction)


                    objEquipmentUpdates.UpdateGatein(drAdditionalUpdate.Item(EquipmentUpdateData.EQPMNT_NO).ToString(), _
                                                     drAdditionalUpdate.Item(EquipmentUpdateData.GI_TRNSCTN_NO).ToString(), _
                                                     CLng(drAdditionalUpdate.Item(EquipmentUpdateData.PRDCT_ID).ToString()), _
                                                     bv_intDepotId, _
                                                     objTransaction)
                    strEquipmentNumber = drAdditionalUpdate.Item(EquipmentUpdateData.EQPMNT_NO).ToString()
                    strEIRNo = drAdditionalUpdate.Item(EquipmentUpdateData.GI_RF_NO).ToString()
                Else
                    objEquipmentUpdates.UpdateActivityStatus(strEquipmentNumber, _
                                                             CLng(drAdditionalUpdate.Item(EquipmentUpdateData.PRDCT_ID)), _
                                                             drAdditionalUpdate.Item(EquipmentUpdateData.GI_RF_NO).ToString(), _
                                                             drAdditionalUpdate.Item(EquipmentUpdateData.INSTRCTNS_VC).ToString(), _
                                                             drAdditionalUpdate.Item(EquipmentUpdateData.DPT_ID).ToString(), _
                                                             bv_strNewTypeId, _
                                                             bv_strNewCodeId, _
                                                             bv_strNewCustomerId, _
                                                             strGateinTransaction, _
                                                             objTransaction)
                    objEquipmentUpdates.UpdateGatein(strEquipmentNumber, _
                                                     drAdditionalUpdate.Item(EquipmentUpdateData.GI_TRNSCTN_NO).ToString(), _
                                                     CLng(drAdditionalUpdate.Item(EquipmentUpdateData.PRDCT_ID).ToString()), _
                                                     bv_intDepotId, _
                                                     objTransaction)
                    strEIRNo = drAdditionalUpdate.Item(EquipmentUpdateData.GI_RF_NO).ToString()
                End If

                If blnActivityStatus Then
                    objEquipmentInformations.CreateEquipmentInformation(strEIEquipmentNo, _
                                                                        i64EquipmentTypeId, _
                                                                        Nothing, _
                                                                        0, _
                                                                        0, _
                                                                        0, _
                                                                        bv_strUserName, _
                                                                        bv_datModifiedDate, _
                                                                        bv_strUserName, _
                                                                        bv_datModifiedDate, _
                                                                        bv_intDepotId, _
                                                                        True, _
                                                                        drAdditionalUpdate.Item(EquipmentUpdateData.LST_SRVYR_NM).ToString(), _
                                                                        datLastTestDate, _
                                                                        i64LastTestTypeId, _
                                                                        datNextTestDate, _
                                                                        i64NextTestTypeId, _
                                                                        strValidityYears, _
                                                                        False, _
                                                                        strRemarks, _
                                                                        objTransaction)
                Else
                    objEquipmentUpdates.UpdateEquipmentInformation(strEIEquipmentNo, _
                                                                    i64EquipmentTypeId, _
                                                                   drAdditionalUpdate.Item(EquipmentUpdateData.LST_SRVYR_NM).ToString(), _
                                                                   datLastTestDate, _
                                                                   datNextTestDate, _
                                                                   i64LastTestTypeId, _
                                                                   i64NextTestTypeId, _
                                                                   drAdditionalUpdate.Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString(), _
                                                                   strRemarks, _
                                                                   bv_intDepotId, _
                                                                   objTransaction)
                End If
                ''TFS id: 19280, 
                If bv_strOldEquipmentNo <> bv_strNewEquipmentNo AndAlso bv_strNewEquipmentNo <> Nothing Then

                    'Get Information ID for Delete Detail Table

                    'GetEquipmentInformationIDByEquipmentNo
                    Dim dtREquip As DataTable = objEquipmentUpdates.GetEquipmentInformationIDByEquipmentNo(bv_strOldEquipmentNo, objTransaction)
                    Dim strInformationId As String = String.Empty

                    If dtREquip.Rows.Count > 0 Then

                        'Get InformationID
                        strInformationId = CStr(dtREquip.Rows(0).Item(EquipmentUpdateData.EQPMNT_INFRMTN_ID))

                        'Delete from Child
                        objEquipmentUpdates.DeleteEquipmentInformationDetailsByEquipmentInformationId(strInformationId, objTransaction)

                        'Delete Header
                        objEquipmentUpdates.DeleteEquipmentInformationByEquipmentNo(bv_strOldEquipmentNo, objTransaction)
                    End If


                    'objEquipmentUpdates.DeleteEquipmentInformationByEquipmentNo(bv_strOldEquipmentNo, objTransaction)
                End If
                ''

                Dim strGateOut As String = String.Empty
                strGateOut = objEquipmentUpdates.GetGateOutDateByEqpmntNo(strEquipmentNumber, _
                                                                         strGateinTransaction, _
                                                                         bv_intDepotId, _
                                                                         objTransaction)
                objEquipmentUpdates.UpdateGateinEirNo(strEquipmentNumber, _
                                                          strGateinTransaction, _
                                                          strEIRNo, _
                                                          bv_intDepotId, _
                                                          objTransaction)
            Next

            dtEquipmentUpdate = objEquipmentUpdates.GetEquipmentUpdate(objTransaction).Tables(EquipmentUpdateData._EQUIPMENT_UPDATE)
            For Each drEquipmentUpdate As DataRow In dtEquipmentUpdate.Rows
                Dim strUpdateCondition As String = ""
                Dim sbrQuery As New StringBuilder
                Dim sbrSetCondition As New StringBuilder
                Dim sbrWhereCondition As New StringBuilder
                Dim strSplitUpdateCondition() As String = Nothing
                If Not IsDBNull(drEquipmentUpdate.Item(EquipmentUpdateData.UPDATE_FIELDS)) Then
                    strUpdateCondition = (CStr(drEquipmentUpdate.Item(EquipmentUpdateData.UPDATE_FIELDS)))
                    strSplitUpdateCondition = strUpdateCondition.Split(CChar(","))
                    For i = 0 To strSplitUpdateCondition.Length - 1
                        If sbrSetCondition.Length > 0 Then
                            sbrSetCondition.Append(" , ")
                        End If
                        If sbrWhereCondition.Length > 0 Then
                            sbrWhereCondition.Append(" AND ")
                        End If
                        If br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).Rows.Count > 0 Then
                            For Each drColEqUpdate As DataColumn In br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).Columns
                                If drColEqUpdate.ColumnName = strSplitUpdateCondition(i) Then
                                    If br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).Rows(0).Item(strSplitUpdateCondition(i)).ToString <> "" Or Not IsDBNull(br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).Rows(0).Item(strSplitUpdateCondition(i))) Then
                                        ' If drColEqUpdate.ColumnName <> EquipmentUpdateData.GI_TRNSCTN_NO Then
                                        sbrSetCondition.Append(String.Concat(strSplitUpdateCondition(i), "='", br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).Rows(0).Item(strSplitUpdateCondition(i)), "'"))
                                    Else
                                        sbrSetCondition.Append(String.Concat(strSplitUpdateCondition(i), "='", br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).Rows(0).Item(String.Concat("OLD_", strSplitUpdateCondition(i))), "'"))
                                        'End If
                                    End If
                                    sbrWhereCondition.Append(String.Concat(Trim(strSplitUpdateCondition(i)), "='", br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).Rows(0).Item(String.Concat("OLD_", strSplitUpdateCondition(i))), "'"))
                                    Exit For
                                End If
                            Next
                        End If
                    Next
                    objEquipmentUpdates.UpdateEquipment(sbrSetCondition.ToString, _
                                                        drEquipmentUpdate.Item(EquipmentUpdateData.TABLE_NAME).ToString, _
                                                        sbrWhereCondition.ToString,
                                                        objTransaction)
                End If
            Next
            For Each dcNewValues As DataColumn In br_dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Columns
                If br_dsEquipmentUpdate.Tables("OLD_V_EQUIPMENT_UPDATE").Columns.Contains(dcNewValues.ColumnName) Then
                    If Not (br_dsEquipmentUpdate.Tables("OLD_V_EQUIPMENT_UPDATE").Rows(0).Item(dcNewValues.ColumnName).ToString = br_dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows(0).Item(dcNewValues.ColumnName).ToString) Then
                        If Not dcNewValues.DataType Is GetType(Int64) Then
                            If sbrOldEqpUpdate.Length > 0 Then
                                sbrOldEqpUpdate.Append(", ")
                            End If
                            If sbrNewEqpUpdate.Length > 0 Then
                                sbrNewEqpUpdate.Append(", ")
                            End If
                            If dcNewValues.DataType Is GetType(DateTime) Then
                                Dim strOldDate As String = String.Empty
                                Dim strNewDate As String = String.Empty
                                If Not IsDBNull(br_dsEquipmentUpdate.Tables("OLD_V_EQUIPMENT_UPDATE").Rows(0).Item(dcNewValues.ColumnName)) Then
                                    strOldDate = CDate(br_dsEquipmentUpdate.Tables("OLD_V_EQUIPMENT_UPDATE").Rows(0).Item(dcNewValues.ColumnName)).ToString("dd-MMM-yyyy")
                                End If
                                If Not IsDBNull(br_dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows(0).Item(dcNewValues.ColumnName)) Then
                                    strNewDate = CDate(br_dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows(0).Item(dcNewValues.ColumnName)).ToString("dd-MMM-yyyy")
                                End If
                                sbrOldEqpUpdate.Append(String.Concat(dcNewValues.Caption, ": "))
                                sbrOldEqpUpdate.Append(strOldDate)
                                sbrNewEqpUpdate.Append(String.Concat(dcNewValues.Caption, ": "))
                                sbrNewEqpUpdate.Append(strNewDate)
                            Else
                                sbrOldEqpUpdate.Append(String.Concat(dcNewValues.Caption, ": "))
                                sbrOldEqpUpdate.Append(br_dsEquipmentUpdate.Tables("OLD_V_EQUIPMENT_UPDATE").Rows(0).Item(dcNewValues.ColumnName).ToString)
                                sbrNewEqpUpdate.Append(String.Concat(dcNewValues.Caption, ": "))
                                sbrNewEqpUpdate.Append(br_dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows(0).Item(dcNewValues.ColumnName).ToString)
                            End If
                        End If
                    End If
                End If
            Next


            For Each drAuditLog As DataRow In br_dsEquipmentUpdate.Tables(EquipmentUpdateData._NEW_EQUIPMENT_UPDATE).Rows
                If Not drAuditLog.Item(EquipmentUpdateData.EQPMNT_NO) Is Nothing And Not IsDBNull(drAuditLog.Item(EquipmentUpdateData.EQPMNT_NO)) Then
                    sbrNewValue.Append("Equipment No :")
                    sbrNewValue.Append(drAuditLog.Item(EquipmentUpdateData.EQPMNT_NO).ToString)
                End If
                If Not IsDBNull(drAuditLog.Item(String.Concat("OLD_", EquipmentUpdateData.EQPMNT_NO))) Then
                    If Not bv_strNewEquipmentNo Is Nothing Then
                        If drAuditLog.Item(String.Concat("OLD_", EquipmentUpdateData.EQPMNT_NO)).ToString <> bv_strNewEquipmentNo Then
                            sbrOldValue.Append("Equipment No :")
                            sbrOldValue.Append(drAuditLog.Item(String.Concat("OLD_", EquipmentUpdateData.EQPMNT_NO)).ToString)
                        End If
                    End If
                End If

                If Not drAuditLog.Item(EquipmentUpdateData.EQPMNT_TYP_ID) Is Nothing And Not IsDBNull(drAuditLog.Item(EquipmentUpdateData.EQPMNT_TYP_ID)) Then
                    If sbrNewValue.Length > 0 Then
                        sbrNewValue.Append(", ")
                    End If
                    If sbrOldValue.Length > 0 Then
                        sbrOldValue.Append(", ")
                    End If
                    sbrNewValue.Append("Equipment Type :")
                    sbrNewValue.Append(drAuditLog.Item(EquipmentUpdateData.EQPMNT_TYP_CD).ToString)
                    sbrOldValue.Append("Equipment Type :")
                    sbrOldValue.Append(drAuditLog.Item(String.Concat("OLD_", EquipmentUpdateData.EQPMNT_TYP_CD)).ToString)
                End If
                If Not drAuditLog.Item(EquipmentUpdateData.EQPMNT_CD_ID) Is Nothing And Not IsDBNull(drAuditLog.Item(EquipmentUpdateData.EQPMNT_CD_ID)) Then
                    If sbrNewValue.Length > 0 Then
                        sbrNewValue.Append(", ")
                    End If
                    If sbrOldValue.Length > 0 Then
                        sbrOldValue.Append(", ")
                    End If
                    sbrNewValue.Append("Equipment Code :")
                    sbrNewValue.Append(drAuditLog.Item(EquipmentUpdateData.EQPMNT_CD_CD).ToString)
                    sbrOldValue.Append("Equipment Code :")
                    sbrOldValue.Append(drAuditLog.Item(String.Concat("OLD_", EquipmentUpdateData.EQPMNT_CD_CD)).ToString)
                End If
                If Not drAuditLog.Item(EquipmentUpdateData.CSTMR_ID) Is Nothing And Not IsDBNull(drAuditLog.Item(EquipmentUpdateData.CSTMR_ID)) Then
                    If sbrNewValue.Length > 0 Then
                        sbrNewValue.Append(", ")
                    End If
                    If sbrOldValue.Length > 0 Then
                        sbrOldValue.Append(", ")
                    End If
                    sbrNewValue.Append("Customer Code :")
                    sbrNewValue.Append(drAuditLog.Item(EquipmentUpdateData.CSTMR_CD).ToString)
                    sbrOldValue.Append("Customer Code :")
                    sbrOldValue.Append(drAuditLog.Item(String.Concat("OLD_", EquipmentUpdateData.CSTMR_CD)).ToString)
                End If
            Next

            If Not bv_strOldCustomerId Is Nothing Then
                Dim lngCustomerId As Long = 0
                Dim decHandlingCharge As Decimal = 0
                Dim decGateOut As Decimal = 0

                If bv_strNewCustomerId Is Nothing Then
                    lngCustomerId = CLng(bv_strOldCustomerId)
                Else
                    lngCustomerId = CLng(bv_strNewCustomerId)
                End If
                '    'Check if gateout happened for equipment, if so handling out amount taken from customer
                decGateOut = objEquipmentUpdates.GetActivityStatusGateoutByEqpmntNo(strEquipmentNumber, strGateinTransaction, bv_intDepotId, objTransaction)
                'If Customer will change we need to update handling in charge amount from customer master              
                If decGateOut > 0 Then
                    decHandlingCharge = objEquipmentUpdates.GetCustomerHandlingOutRatesByCustomerId(lngCustomerId, _
                                                                                                i64EquipmentTypeId, _
                                                                                                i64EquipmentCodeId, _
                                                                                                objTransaction)
                    objEquipmentUpdates.UpdateHandlingCharge(strEquipmentNumber, _
                                                             strGateinTransaction, _
                                                             "HNDOT", _
                                                             decHandlingCharge, _
                                                             objTransaction)
                End If
                decHandlingCharge = objEquipmentUpdates.GetCustomerHandlingInRatesByCustomerId(lngCustomerId, _
                                                                                              i64EquipmentTypeId, _
                                                                                              i64EquipmentCodeId, _
                                                                                              objTransaction)
                objEquipmentUpdates.UpdateHandlingCharge(strEquipmentNumber, _
                                                        strGateinTransaction, _
                                                       "HNDIN", _
                                                        decHandlingCharge, _
                                                        objTransaction)


            End If

            Dim lngAuditLog As Long
            Dim blnAudit As Boolean = False

            If bv_strNewCustomerCd <> "" Or bv_strNewTypeCd <> "" Or bv_strNewCodeCd <> "" Or bv_strNewEquipmentNo <> "" Then
                If sbrNewValue.Length > 0 Then
                    lngAuditLog = objEquipmentUpdates.CreateAuditLog(bv_strOldEquipmentNo, _
                                                                     "Equipment Update (New Details)", _
                                                                     "Update", _
                                                                     CDate(Now), _
                                                                     sbrOldValue.ToString(), _
                                                                     sbrNewValue.ToString(), _
                                                                     bv_strReason, _
                                                                     bv_strUserName, _
                                                                     bv_intDepotId, _
                                                                     objTransaction)
                    blnAudit = True

                End If

            End If
            If sbrNewEqpUpdate.Length > 0 Then
                lngAuditLog = objEquipmentUpdates.CreateAuditLog(bv_strOldEquipmentNo, _
                                                                 "Equipment Update (Additional Update)", _
                                                                 "Update", _
                                                                 CDate(Now), _
                                                                 sbrOldEqpUpdate.ToString, _
                                                                 sbrNewEqpUpdate.ToString, _
                                                                 bv_strReason, _
                                                                 bv_strUserName, _
                                                                 bv_intDepotId, _
                                                                 objTransaction)
                blnAudit = True
            End If

            If blnAudit = True Then


                'GateIn Ret
                objEquipmentUpdates.UpdateGateInRet(strGateinTransaction, strEquipmentNo, strCustomer, strEquipmentType, strEquipmentCode, strEquipmentTypeDescription, objTransaction)

                'GateOut Ret
                objEquipmentUpdates.UpdateGateOutRet(strGateinTransaction, strEquipmentNo, strCustomer, strEquipmentType, strEquipmentCode, strEquipmentTypeDescription, objTransaction)

                'RepairEstimate Ret
                objEquipmentUpdates.UpdateRepairEstimateRet(strGateinTransaction, strEquipmentNo, strCustomer, strEquipmentType, strEquipmentCode, strEquipmentTypeDescription, objTransaction)

                'Repair Estimate Details Ret
                objEquipmentUpdates.UpdateRepairEstimateDetailsRet(strGateinTransaction, strEquipmentNo, objTransaction)

            End If

            objTransaction.commit()
            Return True
        Catch ex As Exception
            objTransaction.RollBack()
            Throw ex
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region

#Region "pub_ValidateEquipmentNoByDepotID"
    <OperationContract()> _
    Public Function pub_ValidateEquipmentNoByDepotID(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32) As Boolean

        Try
            Dim objEquipmentUpdates As New EquipmentUpdates
            Dim dtActivityStatus As New DataTable
            Dim dtSupplier As New DataTable
            dtActivityStatus = objEquipmentUpdates.GetEquipmentInformationByID(bv_strEquipmentNo, bv_intDepotID)
            If dtActivityStatus.Rows.Count > 0 Then
                Return False
            Else
                dtSupplier = objEquipmentUpdates.GetSupplierDetailsByEqpNo(bv_strEquipmentNo, bv_intDepotID)
                If dtSupplier.Rows.Count > 0 Then
                    Return False
                Else
                    Return True
                End If

            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetPreAdviceByEqpmntNo"
    <OperationContract()> _
    Public Function pub_GetPreAdviceByEqpmntNo(ByVal bv_strEquipmentNo As String, _
                                               ByVal bv_intDepotId As Int32) As Boolean
        Try
            Dim objEqUpdates As New EquipmentUpdates
            Dim intRowCount As Integer
            intRowCount = CInt(objEqUpdates.GetPreAdviceByEqpmntNo(bv_strEquipmentNo, bv_intDepotId))
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

#Region "pub_ValidatePreviousActivityDate()"
    Public Function pub_ValidatePreviousActivityDate(ByVal bv_strEquipmentNo As String, _
                                                     ByVal bv_dtEventDate As DateTime, _
                                                     ByRef br_dtPreviousDate As DateTime, _
                                                     ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dsEquipmentUpdate As New EquipmentUpdateDataSet
            Dim objEquipmentUpdates As New EquipmentUpdates
            Dim blnDateValid As Boolean = False

            '     dsEquipmentUpdate = objEquipmentUpdates.ValidatePreviousActivityDate(bv_strEquipmentNo, bv_intDepotID)
            If dsEquipmentUpdate.Tables(CommonUIData._TRACKING).Rows.Count > 0 Then
                With dsEquipmentUpdate.Tables(CommonUIData._TRACKING).Rows(0)
                    If CDate(.Item(CommonUIData.ACTVTY_DT)) > CDate(bv_dtEventDate) Then
                        br_dtPreviousDate = CDate(.Item(CommonUIData.ACTVTY_DT))
                        blnDateValid = True
                    End If
                End With
            End If
            Return blnDateValid
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "Equipment Update - Release 3 Changes"

    <OperationContract()> _
    Public Function Delete_Tracking(ByVal bv_TrackingId As Int32, ByVal bv_intDepotId As Int32, ByRef br_objTransaction As Transactions) As Long
        Try
            Dim objEqUpdates As New EquipmentUpdates
           
            objEqUpdates.Delete_Tracking(bv_TrackingId, bv_intDepotId, br_objTransaction)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function ValidateRentalStatus(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32) As Boolean
        Try

            Dim objEqUpdates As New EquipmentUpdates
            Dim lng_Rental_infoId As Long
            lng_Rental_infoId = objEqUpdates.ValidateRentalStatus(bv_strEquipmentNo, bv_intDepotId)

            If lng_Rental_infoId > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function get_LastActivityStatus(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String) As String
        Try

            Dim objEqUpdates As New EquipmentUpdates
            Return objEqUpdates.get_LastActivityStatus(bv_strEquipmentNo, bv_intDepotId, bv_strGITransaction_No)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function ValidateActivityStatus(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32) As Boolean
        Try

            Dim objEqUpdates As New EquipmentUpdates
            Dim lng_Actvty_SttsId As Long

            lng_Actvty_SttsId = objEqUpdates.ValidateActivityStatus(bv_strEquipmentNo, bv_intDepotId)

            If lng_Actvty_SttsId > 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function Get_GateIn_GITransaction_No(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32) As String
        Try
            Dim objEqUpdates As New EquipmentUpdates
            Dim strGI_Tran_No As String

            strGI_Tran_No = objEqUpdates.Get_GateIn_GITransaction_No(bv_strEquipmentNo, bv_intDepotId)

            If String.IsNullOrEmpty(strGI_Tran_No) Or String.IsNullOrWhiteSpace(strGI_Tran_No) Then
                strGI_Tran_No = Nothing
            End If

            Return strGI_Tran_No

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function Get_BillingStatus_Validation(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, _
                                                     ByVal bv_strGITransaction_No As String) As Boolean
        Try
            Dim objEqUpdates As New EquipmentUpdates
            Dim lngCleaning As Long
            Dim lngHandling As Long
            Dim lngStorage As Long
            Dim lngRepair As Long
            Dim lngHeating As Long
            Dim lngRental As Long

            lngCleaning = objEqUpdates.BillingValidation_CleaningCharge(bv_strEquipmentNo, bv_intDepotId, bv_strGITransaction_No)
            lngHandling = objEqUpdates.BillingValidation_HandlingCharge(bv_strEquipmentNo, bv_intDepotId, bv_strGITransaction_No)
            lngStorage = objEqUpdates.BillingValidation_StorageCharge(bv_strEquipmentNo, bv_intDepotId, bv_strGITransaction_No)
            lngRepair = objEqUpdates.BillingValidation_RepairCharge(bv_strEquipmentNo, bv_intDepotId, bv_strGITransaction_No)
            lngHeating = objEqUpdates.BillingValidation_HeatingCharge(bv_strEquipmentNo, bv_intDepotId, bv_strGITransaction_No)
            lngRental = objEqUpdates.BillingValidation_RentalCharge(bv_strEquipmentNo, bv_intDepotId, bv_strGITransaction_No)

            If lngCleaning > 0 Or lngHandling > 0 Or lngStorage > 0 Or lngRepair > 0 Or lngHeating > 0 Or lngRental > 0 Then

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function Get_LastInvoiceDate(ByVal bv_strEquipmentNo As String, ByVal bv_strGITransaction_No As String, _
                                              ByVal bv_intDepotId As Int32) As DateTime
        Try

            Dim objEqUpdates As New EquipmentUpdates
            Dim lst_invc_dt As DateTime
            Dim str_Invc_dt As String


            str_Invc_dt = objEqUpdates.Get_LastInvoiceDate(bv_strEquipmentNo, bv_strGITransaction_No, bv_intDepotId)

            If (Not String.IsNullOrEmpty(str_Invc_dt)) AndAlso (Not String.IsNullOrWhiteSpace(str_Invc_dt)) Then
                lst_invc_dt = CDate(str_Invc_dt)
            Else
                lst_invc_dt = Nothing
            End If

            Return lst_invc_dt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function get_LastActivity_Date(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String) As String
        Try
            Dim objEqUpdates As New EquipmentUpdates
            Return objEqUpdates.get_LastActivity_Date(bv_strEquipmentNo, bv_intDepotId, bv_strGITransaction_No)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    Public Function get_LastActivityStatus_Date(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String) As String
        Try
            Dim objEqUpdates As New EquipmentUpdates


            Return objEqUpdates.get_LastActivityStatus_Date(bv_strEquipmentNo, bv_intDepotId, bv_strGITransaction_No)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    <OperationContract()> _
    Public Function Validate_Histroy_Delete(ByVal bv_strEquipmentNo As String, ByVal bv_strGITransaction_No As String, ByVal bv_intDepotId As Int32) As String
        Try
            Dim objEqUpdates As New EquipmentUpdates

            Return objEqUpdates.Validate_Histroy_Delete(bv_strGITransaction_No, bv_intDepotId)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    '<OperationContract()> _
    'Public Function get_EffectiveFromDate_Info(ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String) As DataTable
    '    Try

    '        Dim objEqUpdates As New EquipmentUpdates
    '        Return objEqUpdates.get_EffectiveFromDate_Info(bv_intDepotId, bv_strGITransaction_No)

    '    Catch ex As Exception
    '        Throw New FaultException(New FaultReason(ex.Message))
    '    End Try
    'End Function

    <OperationContract()> _
    Public Function get_EffectiveFrom_Date(ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String) As String
        Try

            Dim objEqUpdates As New EquipmentUpdates
            Dim dt As New DataTable

            dt = objEqUpdates.get_EffectiveFromDate_Info(bv_intDepotId, bv_strGITransaction_No)

            If (Not dt Is Nothing) AndAlso dt.Rows.Count > 1 Then
                Return objEqUpdates.get_EffectiveFrom_Date(bv_intDepotId, bv_strGITransaction_No)

            Else
                Return Nothing
            End If

            'Return objEqUpdates.get_EffectiveFrom_Date(bv_intDepotId, bv_strGITransaction_No)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    <OperationContract()> _
    Public Function Update_Equipment_Information(ByVal bv_strNewEquipmentNo As String, _
                                                 ByVal bv_strNewCustomerId As String, _
                                                 ByVal bv_strNewCustomerCd As String, _
                                                 ByVal bv_strNewTypeId As String, _
                                                 ByVal bv_strNewTypeCd As String, _
                                                 ByVal bv_strNewCodeId As String, _
                                                 ByVal bv_strNewCodeCd As String, _
                                                 ByVal bv_strOldEquipmentNo As String, _
                                                 ByVal bv_strOldEquipmentTypeId As String, _
                                                 ByVal bv_strOldEquipmentTypeCd As String, _
                                                 ByVal bv_strOldEquipmentCodeId As String, _
                                                 ByVal bv_strOldEquipmentCodeCd As String, _
                                                 ByVal bv_strOldCustomerId As String, _
                                                 ByVal bv_strOldCustomerCd As String, _
                                                 ByVal bv_strBillingBit As String, _
                                                 ByVal bv_strEffective_dt As String, _
                                                 ByVal bv_strUserName As String, _
                                                 ByVal bv_strModifiedDate As String, _
                                                 ByVal bv_strReason As String, _
                                                 ByVal bv_intDepotId As Int32, _
                                                 ByVal bv_blnCustomerChange As Boolean, _
                                                 ByVal dsEquipmentUpdate As EquipmentUpdateDataSet) As Boolean
        Dim objTransaction As New Transactions
        Try

            Dim bln_RentalStatus As Boolean
            Dim bln_ActivityStatus As Boolean
            Dim str_GITransaction_No As String
            Dim str_LastActivityStatus As String
            Dim bln_BillingStatus As Boolean
            Dim datLastInvc_dt As DateTime
            Dim dat_Gate_In_Date As DateTime = Nothing
            Dim dat_EffectiveFrom_Date As DateTime = Nothing
            Dim dtGate_In As New DataTable
            Dim dat_LastActivityDate As DateTime
            Dim strDate As String
            Dim blnStatus As Boolean = False


            If bv_strEffective_dt <> Nothing Then
                dat_EffectiveFrom_Date = CDate(bv_strEffective_dt)
            End If

            Dim objEqUpdates As New EquipmentUpdates


            If (Not String.IsNullOrEmpty(bv_strNewEquipmentNo)) AndAlso (Not String.IsNullOrWhiteSpace(bv_strNewEquipmentNo)) AndAlso bv_strNewEquipmentNo <> bv_strOldEquipmentNo Then

                bln_RentalStatus = ValidateRentalStatus(bv_strOldEquipmentNo, bv_intDepotId) ' Get Rental Status
                bln_ActivityStatus = ValidateActivityStatus(bv_strOldEquipmentNo, bv_intDepotId) ' get Activity Status
                str_GITransaction_No = Get_GateIn_GITransaction_No(bv_strOldEquipmentNo, bv_intDepotId) 'Get GI_Transaction No
                dat_Gate_In_Date = CDate(objEqUpdates.Get_GateIn_Date(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId)) 'Get Gate_In Date
                str_LastActivityStatus = get_LastActivityStatus(bv_strOldEquipmentNo, bv_intDepotId, str_GITransaction_No) 'Get Last Activity Status
                bln_BillingStatus = Get_BillingStatus_Validation(bv_strOldEquipmentNo, bv_intDepotId, str_GITransaction_No) ' Get Billing Status
                datLastInvc_dt = Get_LastInvoiceDate(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId) ' Get Last invoice Date

                strDate = objEqUpdates.get_LastActivity_Date(bv_strOldEquipmentNo, bv_intDepotId, str_GITransaction_No)

                If (Not String.IsNullOrEmpty(strDate)) AndAlso (Not String.IsNullOrWhiteSpace(strDate)) Then
                    dat_LastActivityDate = CDate(strDate)
                Else
                    dat_LastActivityDate = Nothing
                End If

                dtGate_In = objEqUpdates.Get_GateIn_Info(bv_intDepotId, str_GITransaction_No, objTransaction)

                'Validate Rental Status - Equipment should not be Rental Activity  
                If bln_RentalStatus = False Then

                    'Validate Activity Status - IND or AVL Only 
                    If bln_ActivityStatus = True Then

                        ' Check Only Gate In alone Done  - Unbilled Equipment  - Effective Date is Equal To Gate_in_Date 
                        If str_LastActivityStatus = "IND" AndAlso str_GITransaction_No <> Nothing AndAlso bln_BillingStatus = False Then

                            'Update Gate_In and Its Related Tables
                            If dat_Gate_In_Date = dat_EffectiveFrom_Date Then

                                Dim sb_OldVal As New StringBuilder
                                Dim sb_NewVal As New StringBuilder

                                Dim EQPMNT_TYP_ID As Int32
                                Dim EQPMNT_CD_ID As Int32
                                Dim intCustomerId As Int32
                                Dim RMRKS_VC As String
                                Dim strJTSEIR_No As String

                                Dim objEquipmentInfo As New EquipmentInformations

                                Dim MNFCTR_DT As DateTime
                                Dim TR_WGHT_NC As Decimal
                                Dim GRSS_WGHT_NC As Decimal
                                Dim CPCTY_NC As Decimal
                                Dim LST_TST_DT As DateTime
                                Dim NXT_TST_DT As DateTime
                                Dim VLDTY_PRD_TST_YRS As String
                                Dim LST_TST_TYP_ID As Long
                                Dim NXT_TST_TYP_ID As Long
                                Dim LST_SRVYR_NM As String
                                Dim Prdct_id As Int64



                                sb_OldVal.Append(String.Concat("Equipment No :", bv_strOldEquipmentNo))
                                sb_NewVal.Append(String.Concat("Equipment No :", bv_strNewEquipmentNo))


                                If Not String.IsNullOrEmpty(bv_strNewTypeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewTypeId) Then 'Type
                                    EQPMNT_TYP_ID = CInt(bv_strNewTypeId)
                                    sb_OldVal.Append(String.Concat(" ,Type :", bv_strOldEquipmentTypeCd))
                                    sb_NewVal.Append(String.Concat(" ,Type :", bv_strNewTypeCd))
                                Else
                                    EQPMNT_TYP_ID = CInt(bv_strOldEquipmentTypeId)
                                End If

                                If Not String.IsNullOrEmpty(bv_strNewCodeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCodeId) Then 'Code
                                    EQPMNT_CD_ID = CInt(bv_strNewCodeId)
                                    sb_OldVal.Append(String.Concat(" ,Code :", bv_strOldEquipmentCodeCd))
                                    sb_NewVal.Append(String.Concat(" ,Code :", bv_strNewCodeCd))
                                Else
                                    EQPMNT_CD_ID = CInt(bv_strOldEquipmentCodeId)
                                End If

                                If Not String.IsNullOrEmpty(bv_strNewCustomerId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCustomerId) Then 'Customer
                                    intCustomerId = CInt(bv_strNewCustomerId)
                                    sb_OldVal.Append(String.Concat(" ,Customer :", bv_strOldCustomerCd))
                                    sb_NewVal.Append(String.Concat(" ,Customer :", bv_strNewCustomerCd))
                                Else
                                    intCustomerId = CInt(bv_strOldCustomerId)
                                End If

                                Dim dtEquipment As DataTable = dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE)


                                'Gare in JTS EIR No & Equipment Remarks

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT).ToString() <> Nothing Then
                                    MNFCTR_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT))
                                Else
                                    MNFCTR_DT = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC).ToString() <> Nothing Then
                                    TR_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC))
                                Else
                                    TR_WGHT_NC = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC).ToString() <> Nothing Then
                                    GRSS_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC))
                                Else
                                    GRSS_WGHT_NC = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC).ToString() <> Nothing Then
                                    CPCTY_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC))
                                Else
                                    CPCTY_NC = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT).ToString() <> Nothing Then
                                    LST_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT))
                                Else
                                    LST_TST_DT = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT).ToString() <> Nothing Then
                                    NXT_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT))
                                Else
                                    NXT_TST_DT = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString() <> Nothing Then
                                    VLDTY_PRD_TST_YRS = dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString()
                                Else
                                    VLDTY_PRD_TST_YRS = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID).ToString() <> Nothing Then
                                    LST_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID))
                                Else
                                    LST_TST_TYP_ID = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID).ToString() <> Nothing Then
                                    NXT_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID))
                                Else
                                    NXT_TST_TYP_ID = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString() <> Nothing Then
                                    LST_SRVYR_NM = dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString()
                                Else
                                    LST_SRVYR_NM = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString() <> Nothing Then
                                    RMRKS_VC = dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString()
                                Else
                                    RMRKS_VC = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString() <> Nothing Then
                                    strJTSEIR_No = dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString()
                                Else
                                    strJTSEIR_No = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString() <> Nothing Then
                                    Prdct_id = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString())
                                Else
                                    Prdct_id = Nothing
                                End If

                                '''' Scenario 8 Fix E2-E1	C1-C2	EQUP	20-Aug-14
                                Dim intEquipmentCount As Int64 = objEqUpdates.ValidateEquipment_Exist(bv_strNewEquipmentNo, bv_intDepotId, objTransaction)

                                If intEquipmentCount > 0 Then
                                    objEqUpdates.DeleteEquipmentInformationByEquipmentNo(bv_strNewEquipmentNo, objTransaction)
                                End If
                                ''
                                'Update Equipment_information
                                objEqUpdates.UpdateEquipmentNo(bv_strOldEquipmentNo, bv_strNewEquipmentNo, EQPMNT_TYP_ID, bv_intDepotId, RMRKS_VC, objTransaction)

                                'Additional Details
                                objEqUpdates.UpdateEquipmentInformation(bv_strNewEquipmentNo, EQPMNT_TYP_ID, LST_SRVYR_NM, NXT_TST_DT, NXT_TST_DT, LST_TST_TYP_ID, NXT_TST_TYP_ID, VLDTY_PRD_TST_YRS, RMRKS_VC, bv_intDepotId, objTransaction)

                                'Gate In Operation
                                objEqUpdates.UpdateGateIn_Equipment(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, objTransaction) 'Equipment , code, type, customer

                                'JTS EIR NO
                                objEqUpdates.UpdateGateinEirNo(bv_strNewEquipmentNo, str_GITransaction_No, strJTSEIR_No, bv_intDepotId, objTransaction)

                                'Product Id or Previous Cargo
                                objEqUpdates.UpdateGatein(bv_strNewEquipmentNo, str_GITransaction_No, Prdct_id, bv_intDepotId, objTransaction)

                                objEqUpdates.UpdateRental_Entry(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)
                                objEqUpdates.UpdateRental_Charge(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)
                                objEqUpdates.UpdatePre_Advice(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, objTransaction)
                                objEqUpdates.UpdateTracking(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)

                                'Handling Charge
                                Dim dtCustomerCharge As New DataTable
                                Dim objGatein As New GateIns
                                Dim decHandlingCost As Decimal
                                Dim objCommon As New CommonUIs
                                If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                                    dtCustomerCharge = objGatein.GetHanldingInCharge(intCustomerId, _
                                                             EQPMNT_CD_ID, _
                                                              EQPMNT_TYP_ID, _
                                                              CInt(objCommon.GetHeadQuarterID()), _
                                                              objTransaction)
                                Else
                                    dtCustomerCharge = objGatein.GetHanldingInCharge(intCustomerId, _
                                                             EQPMNT_CD_ID, _
                                                              EQPMNT_TYP_ID, _
                                                              bv_intDepotId, _
                                                              objTransaction)
                                End If
                                

                                If dtCustomerCharge.Rows.Count > 0 Then
                                    decHandlingCost = CDec(dtCustomerCharge.Rows(0).Item(GateinData.HNDLNG_IN_CHRG_NC))
                                Else
                                    decHandlingCost = 0
                                End If

                                objEqUpdates.UpdateHandling_Charge(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, decHandlingCost, decHandlingCost, objTransaction)
                                objEqUpdates.UpdateStorage_Charge(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, objTransaction)
                                objEqUpdates.UpdateActivity_Status(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, Prdct_id, strJTSEIR_No, RMRKS_VC, objTransaction)
                                objEqUpdates.UpdateHeating_Charge(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)

                                'Add Audit_Log

                                objEqUpdates.CreateAuditLog(bv_strNewEquipmentNo, "Equipment Update", "Update", dat_EffectiveFrom_Date, sb_OldVal.ToString(), sb_NewVal.ToString(), _
                                                             bv_strReason, bv_strUserName, bv_intDepotId, objTransaction)

                                blnStatus = True


                            ElseIf dat_EffectiveFrom_Date > dat_Gate_In_Date Then

                                ' ''Bug 18989 Scenario 2 Fix
                                'Dim dtPreviousStorage As DataTable = objEqUpdates.GetPreviousStorageChargeByGITransactionNo(str_GITransaction_No, bv_intDepotId)
                                'Dim i32PreviousCustomerID As Int32 = 0
                                'Dim i32EquipmentStatusID As Int32 = 0
                                'Dim intNewCustomerId As Int32 = 0
                                'If dtPreviousStorage.Rows.Count > 0 Then
                                '    i32PreviousCustomerID = CInt(dtPreviousStorage.Rows(0).Item("PREV_CSTMR_ID"))
                                'End If
                                'If Not String.IsNullOrEmpty(bv_strNewCustomerId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCustomerId) Then 'Customer
                                '    intNewCustomerId = CInt(bv_strNewCustomerId)
                                'End If
                                'i32EquipmentStatusID = objEqUpdates.GetLastEquipmentStatus(str_GITransaction_No)

                                '' C1->C2->C1
                                If bv_blnCustomerChange = True Then
                                    'Dim dtPreviousStorage As DataTable = objEqUpdates.GetPreviousStorageChargeByGITransactionNo(str_GITransaction_No, bv_intDepotId, bv_strNewCustomerId)
                                    pvt_subUpdateEquipmentInfo(bv_strOldEquipmentNo, bv_strNewCustomerId, bv_strNewEquipmentNo, bv_strNewTypeId, _
                                         bv_strNewCustomerCd, bv_strOldEquipmentTypeCd, bv_strNewTypeCd, bv_strOldEquipmentTypeId, _
                                         bv_strNewCodeId, bv_strOldEquipmentCodeCd, bv_strNewCodeCd, bv_strOldEquipmentCodeId, _
                                         bv_strOldCustomerId, bv_strOldCustomerCd, str_GITransaction_No, bv_intDepotId, _
                                         bv_strEffective_dt, blnStatus, bv_strReason, bv_strUserName,
                                          dat_Gate_In_Date.ToString, _
                                         dsEquipmentUpdate, objTransaction)


                                Else


                                    'Create Equipment
                                    Dim objEquipmentInfo As New EquipmentInformations
                                    Dim lngEquipmentInformation As Int32
                                    Dim EQPMNT_TYP_ID As Int32
                                    Dim MNFCTR_DT As DateTime
                                    Dim TR_WGHT_NC As Decimal
                                    Dim GRSS_WGHT_NC As Decimal
                                    Dim CPCTY_NC As Decimal
                                    Dim LST_TST_DT As DateTime
                                    Dim NXT_TST_DT As DateTime
                                    Dim VLDTY_PRD_TST_YRS As String
                                    Dim LST_TST_TYP_ID As Long
                                    Dim NXT_TST_TYP_ID As Long
                                    Dim LST_SRVYR_NM As String
                                    Dim DPT_ID As Int32
                                    Dim ACTV_BT As Boolean
                                    Dim RNTL_BT As Boolean
                                    Dim RMRKS_VC As String
                                    Dim EQPMNT_CD_ID As Int32
                                    Dim intCustomerId As Int32
                                    Dim strJTSEIR_No As String
                                    Dim Prdct_id As Long

                                    Dim sb_OldVal As New StringBuilder
                                    Dim sb_NewVal As New StringBuilder

                                    sb_OldVal.Append(String.Concat("Equipment No :", bv_strOldEquipmentNo))
                                    sb_NewVal.Append(String.Concat("Equipment No :", bv_strNewEquipmentNo))

                                    Dim dtEquipment As DataTable = dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE)

                                    If Not String.IsNullOrEmpty(bv_strNewTypeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewTypeId) Then 'Type
                                        EQPMNT_TYP_ID = CInt(bv_strNewTypeId)
                                        sb_OldVal.Append(String.Concat(" ,Type :", bv_strOldEquipmentTypeCd))
                                        sb_NewVal.Append(String.Concat(" ,Type :", bv_strNewTypeCd))
                                    Else
                                        EQPMNT_TYP_ID = CInt(bv_strOldEquipmentTypeId)
                                    End If

                                    If Not String.IsNullOrEmpty(bv_strNewCodeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCodeId) Then 'Code
                                        EQPMNT_CD_ID = CInt(bv_strNewCodeId)
                                        sb_OldVal.Append(String.Concat(" ,Code :", bv_strOldEquipmentCodeCd))
                                        sb_NewVal.Append(String.Concat(" ,Code :", bv_strNewCodeCd))
                                    Else
                                        EQPMNT_CD_ID = CInt(bv_strOldEquipmentCodeId)
                                    End If

                                    If Not String.IsNullOrEmpty(bv_strNewCustomerId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCustomerId) Then 'Customer
                                        intCustomerId = CInt(bv_strNewCustomerId)
                                        sb_OldVal.Append(String.Concat(" ,Customer :", bv_strOldCustomerCd))
                                        sb_NewVal.Append(String.Concat(" ,Customer :", bv_strNewCustomerCd))
                                    Else
                                        intCustomerId = CInt(bv_strOldCustomerId)
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT).ToString() <> Nothing Then
                                        MNFCTR_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT))
                                    Else
                                        MNFCTR_DT = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC).ToString() <> Nothing Then
                                        TR_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC))
                                    Else
                                        TR_WGHT_NC = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC).ToString() <> Nothing Then
                                        GRSS_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC))
                                    Else
                                        GRSS_WGHT_NC = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC).ToString() <> Nothing Then
                                        CPCTY_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC))
                                    Else
                                        CPCTY_NC = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT).ToString() <> Nothing Then
                                        LST_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT))
                                    Else
                                        LST_TST_DT = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT).ToString() <> Nothing Then
                                        NXT_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT))
                                    Else
                                        NXT_TST_DT = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString() <> Nothing Then
                                        VLDTY_PRD_TST_YRS = dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString()
                                    Else
                                        VLDTY_PRD_TST_YRS = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID).ToString() <> Nothing Then
                                        LST_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID))
                                    Else
                                        LST_TST_TYP_ID = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID).ToString() <> Nothing Then
                                        NXT_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID))
                                    Else
                                        NXT_TST_TYP_ID = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString() <> Nothing Then
                                        LST_SRVYR_NM = dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString()
                                    Else
                                        LST_SRVYR_NM = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString() <> Nothing Then
                                        RMRKS_VC = dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString()
                                    Else
                                        RMRKS_VC = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString() <> Nothing Then
                                        strJTSEIR_No = dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString()
                                    Else
                                        strJTSEIR_No = Nothing
                                    End If

                                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString() <> Nothing Then
                                        Prdct_id = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString())
                                    Else
                                        Prdct_id = Nothing
                                    End If


                                    DPT_ID = CInt(dtEquipment.Rows(0).Item(EquipmentUpdateData.DPT_ID))
                                    ACTV_BT = CBool(dtEquipment.Rows(0).Item(EquipmentUpdateData.ACTV_BT))
                                    RNTL_BT = CBool(dtEquipment.Rows(0).Item(EquipmentUpdateData.RNTL_BT))


                                    Dim intEquipmentCount As Int64 = objEqUpdates.ValidateEquipment_Exist(bv_strNewEquipmentNo, bv_intDepotId, objTransaction)

                                    If intEquipmentCount > 0 Then

                                        'Additional Details
                                        objEqUpdates.UpdateEquipmentInformation(bv_strNewEquipmentNo, EQPMNT_TYP_ID, LST_SRVYR_NM, NXT_TST_DT, NXT_TST_DT, LST_TST_TYP_ID, NXT_TST_TYP_ID, VLDTY_PRD_TST_YRS, RMRKS_VC, bv_intDepotId, objTransaction)
                                    Else

                                        'Create New Equipment with Additional Details
                                        lngEquipmentInformation = CInt(objEquipmentInfo.CreateEquipmentInformation(bv_strNewEquipmentNo, EQPMNT_TYP_ID, MNFCTR_DT, TR_WGHT_NC, GRSS_WGHT_NC, CPCTY_NC, _
                                                                                 bv_strUserName, CDate(bv_strModifiedDate), bv_strUserName, CDate(bv_strModifiedDate), bv_intDepotId, _
                                                                                ACTV_BT, LST_SRVYR_NM, LST_TST_DT, LST_TST_TYP_ID, NXT_TST_DT, NXT_TST_TYP_ID, VLDTY_PRD_TST_YRS, RNTL_BT, RMRKS_VC, objTransaction))
                                    End If



                                    ''Additional Details
                                    'objEqUpdates.UpdateEquipmentInformation(bv_strNewEquipmentNo, EQPMNT_TYP_ID, LST_SRVYR_NM, NXT_TST_DT, NXT_TST_DT, LST_TST_TYP_ID, NXT_TST_TYP_ID, VLDTY_PRD_TST_YRS, RMRKS_VC, bv_intDepotId, objTransaction)

                                    'JTS EIR NO
                                    objEqUpdates.UpdateGateinEirNo(bv_strNewEquipmentNo, str_GITransaction_No, strJTSEIR_No, bv_intDepotId, objTransaction)


                                    'Create Storage Charge with Old GI_Transctsn No
                                    Dim objGatein As New GateIns
                                    Dim intFreeDays As Int32 = 0
                                    Dim decStorageCost As Decimal = 0
                                    Dim decStorageTaxRate As Decimal = 0
                                    Dim decStorageTotal As Decimal = 0
                                    Dim str_Yard As String
                                    Dim blnHeatingBit As Boolean

                                    If dtGate_In.Rows(0).Item(GateinData.YRD_LCTN).ToString() <> Nothing Then
                                        str_Yard = dtGate_In.Rows(0).Item(GateinData.YRD_LCTN).ToString()
                                    Else
                                        str_Yard = Nothing
                                    End If

                                    If dtGate_In.Rows(0).Item(GateinData.HTNG_BT).ToString() <> Nothing Then
                                        blnHeatingBit = CBool(dtGate_In.Rows(0).Item(GateinData.HTNG_BT))
                                    Else
                                        blnHeatingBit = Nothing
                                    End If

                                    Dim dat_Effective As DateTime = dat_EffectiveFrom_Date

                                    Dim strEqpmnet As String
                                    dtGate_In = objEqUpdates.Get_GateIn_Info(bv_intDepotId, str_GITransaction_No, objTransaction)
                                    strEqpmnet = CStr(dtGate_In.Rows(0).Item(GateinData.EQPMNT_NO))


                                    'Delete Previous Storage - avoid Duplicate
                                    objEqUpdates.Delete_StorageCharge(bv_intDepotId, str_GITransaction_No, dat_Effective.AddDays(-1), objTransaction)

                                    'Update Storage Charge
                                    objEqUpdates.UpdatePrevious_Storage_Charge(strEqpmnet, str_GITransaction_No, bv_intDepotId, CInt(bv_strOldCustomerId), dat_EffectiveFrom_Date.AddDays(-1), objTransaction)
                                    objEqUpdates.UpdatePrevious_Storage_Charge(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(bv_strOldCustomerId), dat_EffectiveFrom_Date.AddDays(-1), objTransaction)

                                    objGatein.CreateStorageCharge(bv_strNewEquipmentNo, EQPMNT_CD_ID, EQPMNT_TYP_ID, "STR", str_GITransaction_No, str_GITransaction_No, dat_EffectiveFrom_Date, _
                                                                  Nothing, intFreeDays, Nothing, decStorageCost, decStorageTaxRate, decStorageTotal, "C", "U", "I", True, Nothing, Nothing, str_Yard, "C",
                                                                 intCustomerId, bv_intDepotId, blnHeatingBit, str_GITransaction_No, Nothing, objTransaction)


                                    'Add Audit_Log

                                    objEqUpdates.CreateAuditLog(bv_strNewEquipmentNo, "Equipment Update", "Update", dat_EffectiveFrom_Date, sb_OldVal.ToString(), sb_NewVal.ToString(), _
                                                                 bv_strReason, bv_strUserName, bv_intDepotId, objTransaction)
                                    'Activity Status
                                    objEqUpdates.UpdateActivity_Status(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, Prdct_id, strJTSEIR_No, RMRKS_VC, objTransaction)

                                    blnStatus = True
                                    ''C1->C2->C2 AND E1->E2->E1
                                End If
                            End If

                        ElseIf str_LastActivityStatus = "AVL" AndAlso ((dat_EffectiveFrom_Date > CDate(strDate) AndAlso bln_BillingStatus = False) Or
                               (dat_EffectiveFrom_Date > datLastInvc_dt AndAlso bln_BillingStatus = True)) Then
                            '' C1->C2->C1
                            If bv_blnCustomerChange = True Then
                                'Dim dtPreviousStorage As DataTable = objEqUpdates.GetPreviousStorageChargeByGITransactionNo(str_GITransaction_No, bv_intDepotId, bv_strNewCustomerId)
                                pvt_subUpdateEquipmentInfo(bv_strOldEquipmentNo, bv_strNewCustomerId, bv_strNewEquipmentNo, bv_strNewTypeId, _
                                     bv_strNewCustomerCd, bv_strOldEquipmentTypeCd, bv_strNewTypeCd, bv_strOldEquipmentTypeId, _
                                     bv_strNewCodeId, bv_strOldEquipmentCodeCd, bv_strNewCodeCd, bv_strOldEquipmentCodeId, _
                                     bv_strOldCustomerId, bv_strOldCustomerCd, str_GITransaction_No, bv_intDepotId, _
                                     bv_strEffective_dt, blnStatus, bv_strReason, bv_strUserName,
                                      dat_Gate_In_Date.ToString, _
                                     dsEquipmentUpdate, objTransaction)


                            Else


                                Dim objEquipmentInfo As New EquipmentInformations
                                Dim lngEquipmentInformation As Int32
                                Dim MNFCTR_DT As DateTime
                                Dim TR_WGHT_NC As Decimal
                                Dim GRSS_WGHT_NC As Decimal
                                Dim CPCTY_NC As Decimal
                                Dim LST_TST_DT As DateTime
                                Dim NXT_TST_DT As DateTime
                                Dim VLDTY_PRD_TST_YRS As String
                                Dim LST_TST_TYP_ID As Long
                                Dim NXT_TST_TYP_ID As Long
                                Dim LST_SRVYR_NM As String
                                Dim ACTV_BT As Boolean
                                Dim RNTL_BT As Boolean
                                Dim RMRKS_VC As String
                                Dim strEquipmentNo As String
                                Dim strEqp_Code As String
                                Dim strEqp_Type As String
                                Dim strCustomerId As String
                                Dim strJTSEIR_No As String
                                Dim Prdct_id As Long
                                Dim EQPMNT_CD_ID As Int32
                                Dim EQPMNT_TYP_ID As Int32


                                Dim sb_OldVal As New StringBuilder
                                Dim sb_NewVal As New StringBuilder

                                Dim dtEquipment As DataTable = dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE)

                                strDate = objEqUpdates.get_LastActivity_Date(bv_strOldEquipmentNo, bv_intDepotId, str_GITransaction_No)

                                If Not String.IsNullOrEmpty(bv_strNewTypeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewTypeId) Then 'Type
                                    EQPMNT_TYP_ID = CInt(bv_strNewTypeId)
                                    sb_OldVal.Append(String.Concat(" ,Type :", bv_strOldEquipmentTypeCd))
                                    sb_NewVal.Append(String.Concat(" ,Type :", bv_strNewTypeCd))
                                Else
                                    EQPMNT_TYP_ID = CInt(bv_strOldEquipmentTypeId)
                                End If

                                If Not String.IsNullOrEmpty(bv_strNewCodeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCodeId) Then 'Code
                                    EQPMNT_CD_ID = CInt(bv_strNewCodeId)
                                    sb_OldVal.Append(String.Concat(" ,Code :", bv_strOldEquipmentCodeCd))
                                    sb_NewVal.Append(String.Concat(" ,Code :", bv_strNewCodeCd))
                                Else
                                    EQPMNT_CD_ID = CInt(bv_strOldEquipmentCodeId)
                                End If


                                If datLastInvc_dt <> Nothing Then
                                    strDate = datLastInvc_dt.ToString()
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT).ToString() <> Nothing Then
                                    MNFCTR_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT))
                                Else
                                    MNFCTR_DT = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC).ToString() <> Nothing Then
                                    TR_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC))
                                Else
                                    TR_WGHT_NC = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC).ToString() <> Nothing Then
                                    GRSS_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC))
                                Else
                                    GRSS_WGHT_NC = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC).ToString() <> Nothing Then
                                    CPCTY_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC))
                                Else
                                    CPCTY_NC = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT).ToString() <> Nothing Then
                                    LST_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT))
                                Else
                                    LST_TST_DT = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT).ToString() <> Nothing Then
                                    NXT_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT))
                                Else
                                    NXT_TST_DT = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString() <> Nothing Then
                                    VLDTY_PRD_TST_YRS = dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString()
                                Else
                                    VLDTY_PRD_TST_YRS = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID).ToString() <> Nothing Then
                                    LST_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID))
                                Else
                                    LST_TST_TYP_ID = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID).ToString() <> Nothing Then
                                    NXT_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID))
                                Else
                                    NXT_TST_TYP_ID = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString() <> Nothing Then
                                    LST_SRVYR_NM = dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString()
                                Else
                                    LST_SRVYR_NM = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString() <> Nothing Then
                                    RMRKS_VC = dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString()
                                Else
                                    RMRKS_VC = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString() <> Nothing Then
                                    strJTSEIR_No = dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString()
                                Else
                                    strJTSEIR_No = Nothing
                                End If

                                If dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString() <> Nothing Then
                                    Prdct_id = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString())
                                Else
                                    Prdct_id = Nothing
                                End If

                                ACTV_BT = CBool(dtEquipment.Rows(0).Item(EquipmentUpdateData.ACTV_BT))
                                RNTL_BT = CBool(dtEquipment.Rows(0).Item(EquipmentUpdateData.RNTL_BT))


                                'Equipment No
                                If Not String.IsNullOrEmpty(bv_strNewEquipmentNo) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewEquipmentNo) AndAlso bv_strNewEquipmentNo <> bv_strOldEquipmentNo Then
                                    strEquipmentNo = bv_strNewEquipmentNo
                                    sb_OldVal.Append(String.Concat("Equipment No :", bv_strOldEquipmentNo))
                                    sb_NewVal.Append(String.Concat("Equipment No  :", bv_strNewEquipmentNo))
                                Else
                                    strEquipmentNo = bv_strOldEquipmentNo
                                End If

                                'Equipment Code
                                If Not String.IsNullOrEmpty(bv_strNewCodeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCodeId) AndAlso bv_strNewCodeId <> bv_strOldEquipmentCodeId Then
                                    strEqp_Code = bv_strNewCodeId
                                    sb_OldVal.Append(String.Concat(" ,Code :", bv_strOldEquipmentCodeCd))
                                    sb_NewVal.Append(String.Concat(" ,Code :", bv_strNewCodeCd))
                                Else
                                    strEqp_Code = bv_strOldEquipmentCodeId
                                End If

                                'Equipment Type
                                If Not String.IsNullOrEmpty(bv_strNewTypeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewTypeId) AndAlso bv_strNewTypeId <> bv_strOldEquipmentTypeId Then
                                    strEqp_Type = bv_strNewTypeId
                                    sb_OldVal.Append(String.Concat(" ,Type :", bv_strOldEquipmentTypeCd))
                                    sb_NewVal.Append(String.Concat(" ,Type :", bv_strNewTypeCd))
                                Else
                                    strEqp_Type = bv_strOldEquipmentTypeId
                                End If

                                'Customer
                                If Not String.IsNullOrEmpty(bv_strNewCustomerId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCustomerId) AndAlso bv_strNewCustomerId <> bv_strOldCustomerId Then
                                    strCustomerId = bv_strNewCustomerId
                                    sb_OldVal.Append(String.Concat(" ,Customer :", bv_strOldCustomerCd))
                                    sb_NewVal.Append(String.Concat(" ,Customer :", bv_strNewCustomerCd))
                                Else
                                    strCustomerId = bv_strOldCustomerId
                                End If

                                Dim intEquipmentCount As Int64 = objEqUpdates.ValidateEquipment_Exist(strEquipmentNo, bv_intDepotId, objTransaction)

                                If intEquipmentCount > 0 Then

                                    'Additional Details
                                    objEqUpdates.UpdateEquipmentInformation(strEquipmentNo, EQPMNT_TYP_ID, LST_SRVYR_NM, NXT_TST_DT, NXT_TST_DT, LST_TST_TYP_ID, NXT_TST_TYP_ID, VLDTY_PRD_TST_YRS, RMRKS_VC, bv_intDepotId, objTransaction)

                                Else

                                    'Create Equipment
                                    lngEquipmentInformation = CInt(objEquipmentInfo.CreateEquipmentInformation(strEquipmentNo, CLng(strEqp_Type), MNFCTR_DT, TR_WGHT_NC, GRSS_WGHT_NC, CPCTY_NC, _
                                                                             bv_strUserName, CDate(bv_strModifiedDate), bv_strUserName, CDate(bv_strModifiedDate), bv_intDepotId, _
                                                                            ACTV_BT, LST_SRVYR_NM, LST_TST_DT, LST_TST_TYP_ID, NXT_TST_DT, NXT_TST_TYP_ID, VLDTY_PRD_TST_YRS, RNTL_BT, RMRKS_VC, objTransaction))
                                End If



                                'Create Storage Charge with Old GI_Transctsn No
                                Dim objGatein As New GateIns
                                Dim intFreeDays As Int32 = 0
                                Dim decStorageCost As Decimal = 0
                                Dim decStorageTaxRate As Decimal = 0
                                Dim decStorageTotal As Decimal = 0
                                Dim str_Yard As String
                                Dim blnHeatingBit As Boolean

                                If dtGate_In.Rows(0).Item(GateinData.YRD_LCTN).ToString() <> Nothing Then
                                    str_Yard = dtGate_In.Rows(0).Item(GateinData.YRD_LCTN).ToString()
                                Else
                                    str_Yard = Nothing
                                End If

                                If dtGate_In.Rows(0).Item(GateinData.HTNG_BT).ToString() <> Nothing Then
                                    blnHeatingBit = CBool(dtGate_In.Rows(0).Item(GateinData.HTNG_BT))
                                Else
                                    blnHeatingBit = Nothing
                                End If

                                Dim dat_Effective As DateTime = dat_EffectiveFrom_Date

                                'Get GateIn Eqpment No
                                Dim strEqpmnet As String
                                dtGate_In = objEqUpdates.Get_GateIn_Info(bv_intDepotId, str_GITransaction_No, objTransaction)
                                strEqpmnet = CStr(dtGate_In.Rows(0).Item(GateinData.EQPMNT_NO))

                                'Delete Previous Storage - avoid Duplicate
                                objEqUpdates.Delete_StorageCharge(bv_intDepotId, str_GITransaction_No, dat_Effective.AddDays(-1), objTransaction)

                                'Update Storage Charge
                                objEqUpdates.UpdatePrevious_Storage_Charge(strEqpmnet, str_GITransaction_No, bv_intDepotId, CInt(bv_strOldCustomerId), dat_EffectiveFrom_Date.AddDays(-1), objTransaction)
                                objEqUpdates.UpdatePrevious_Storage_Charge(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(bv_strOldCustomerId), dat_EffectiveFrom_Date.AddDays(-1), objTransaction)

                                objGatein.CreateStorageCharge(strEquipmentNo, CLng(strEqp_Code), CLng(strEqp_Type), "STR", str_GITransaction_No, str_GITransaction_No, dat_EffectiveFrom_Date, _
                                                              Nothing, intFreeDays, Nothing, decStorageCost, decStorageTaxRate, decStorageTotal, "C", "U", "I", True, Nothing, Nothing, str_Yard, "C",
                                                              CLng(strCustomerId), bv_intDepotId, blnHeatingBit, str_GITransaction_No, Nothing, objTransaction)


                                'Add Audit_Log
                                objEqUpdates.CreateAuditLog(strEquipmentNo, "Equipment Update", "Update", dat_EffectiveFrom_Date, sb_OldVal.ToString(), sb_NewVal.ToString(), _
                                                             bv_strReason, bv_strUserName, bv_intDepotId, objTransaction)

                                'Activity Status
                                objEqUpdates.UpdateActivity_Status(strEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(strCustomerId), CInt(strEqp_Type), CInt(strEqp_Code), Prdct_id, strJTSEIR_No, RMRKS_VC, objTransaction)

                                blnStatus = True
                                ''c1->c2->c3
                            End If
                            End If

                    End If

                End If

                'Scenario : 6
            ElseIf ((Not String.IsNullOrEmpty(bv_strNewCustomerId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCustomerId)) Or
                (Not String.IsNullOrEmpty(bv_strNewTypeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewTypeId)) Or
                (Not String.IsNullOrEmpty(bv_strNewCodeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCodeId))) Then


                str_GITransaction_No = Get_GateIn_GITransaction_No(bv_strOldEquipmentNo, bv_intDepotId) 'Get GI_Transaction No
                dat_Gate_In_Date = CDate(objEqUpdates.Get_GateIn_Date(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId)) 'Get Gate_In Date
                str_LastActivityStatus = get_LastActivityStatus(bv_strOldEquipmentNo, bv_intDepotId, str_GITransaction_No) 'Get Last Activity Status
                datLastInvc_dt = Get_LastInvoiceDate(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId) ' Get Last invoice Date
                dtGate_In = objEqUpdates.Get_GateIn_Info(bv_intDepotId, str_GITransaction_No, objTransaction)

                strDate = objEqUpdates.get_LastActivity_Date(bv_strOldEquipmentNo, bv_intDepotId, str_GITransaction_No)

                If (str_LastActivityStatus = "IND" AndAlso dat_Gate_In_Date = dat_EffectiveFrom_Date) Then

                    Dim sb_OldVal As New StringBuilder
                    Dim sb_NewVal As New StringBuilder

                    Dim EQPMNT_TYP_ID As Int32
                    Dim EQPMNT_CD_ID As Int32
                    Dim intCustomerId As Int32

                    Dim MNFCTR_DT As DateTime
                    Dim TR_WGHT_NC As Decimal
                    Dim GRSS_WGHT_NC As Decimal
                    Dim CPCTY_NC As Decimal
                    Dim LST_TST_DT As DateTime
                    Dim NXT_TST_DT As DateTime
                    Dim VLDTY_PRD_TST_YRS As String
                    Dim LST_TST_TYP_ID As Long
                    Dim NXT_TST_TYP_ID As Long
                    Dim LST_SRVYR_NM As String
                    Dim RMRKS_VC As String
                    Dim strJTSEIR_No As String
                    Dim Prdct_id As Int64

                    sb_OldVal.Append(String.Concat("Equipment No :", bv_strOldEquipmentNo))
                    sb_NewVal.Append(String.Concat("Equipment No :", bv_strNewEquipmentNo))


                    If Not String.IsNullOrEmpty(bv_strNewTypeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewTypeId) Then 'Type
                        EQPMNT_TYP_ID = CInt(bv_strNewTypeId)
                        sb_OldVal.Append(String.Concat(" ,Type :", bv_strOldEquipmentTypeCd))
                        sb_NewVal.Append(String.Concat(" ,Type :", bv_strNewTypeCd))
                    Else
                        EQPMNT_TYP_ID = CInt(bv_strOldEquipmentTypeId)
                    End If

                    If Not String.IsNullOrEmpty(bv_strNewCodeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCodeId) Then 'Code
                        EQPMNT_CD_ID = CInt(bv_strNewCodeId)
                        sb_OldVal.Append(String.Concat(" ,Code :", bv_strOldEquipmentCodeCd))
                        sb_NewVal.Append(String.Concat(" ,Code :", bv_strNewCodeCd))
                    Else
                        EQPMNT_CD_ID = CInt(bv_strOldEquipmentCodeId)
                    End If

                    If Not String.IsNullOrEmpty(bv_strNewCustomerId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCustomerId) Then 'Customer
                        intCustomerId = CInt(bv_strNewCustomerId)
                        sb_OldVal.Append(String.Concat(" ,Customer :", bv_strOldCustomerCd))
                        sb_NewVal.Append(String.Concat(" ,Customer :", bv_strNewCustomerCd))
                    Else
                        intCustomerId = CInt(bv_strOldCustomerId)
                    End If

                    Dim dtEquipment As DataTable = dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE)

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT).ToString() <> Nothing Then
                        MNFCTR_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT))
                    Else
                        MNFCTR_DT = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC).ToString() <> Nothing Then
                        TR_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC))
                    Else
                        TR_WGHT_NC = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC).ToString() <> Nothing Then
                        GRSS_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC))
                    Else
                        GRSS_WGHT_NC = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC).ToString() <> Nothing Then
                        CPCTY_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC))
                    Else
                        CPCTY_NC = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT).ToString() <> Nothing Then
                        LST_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT))
                    Else
                        LST_TST_DT = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT).ToString() <> Nothing Then
                        NXT_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT))
                    Else
                        NXT_TST_DT = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString() <> Nothing Then
                        VLDTY_PRD_TST_YRS = dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString()
                    Else
                        VLDTY_PRD_TST_YRS = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID).ToString() <> Nothing Then
                        LST_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID))
                    Else
                        LST_TST_TYP_ID = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID).ToString() <> Nothing Then
                        NXT_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID))
                    Else
                        NXT_TST_TYP_ID = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString() <> Nothing Then
                        LST_SRVYR_NM = dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString()
                    Else
                        LST_SRVYR_NM = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString() <> Nothing Then
                        RMRKS_VC = dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString()
                    Else
                        RMRKS_VC = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString() <> Nothing Then
                        strJTSEIR_No = dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString()
                    Else
                        strJTSEIR_No = Nothing
                    End If

                    If dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString() <> Nothing Then
                        Prdct_id = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString())
                    Else
                        Prdct_id = Nothing
                    End If


                    'Update Equipment_information
                    objEqUpdates.UpdateEquipmentNo(bv_strOldEquipmentNo, bv_strOldEquipmentNo, EQPMNT_TYP_ID, bv_intDepotId, RMRKS_VC, objTransaction)

                    'Additional Details
                    objEqUpdates.UpdateEquipmentInformation(bv_strOldEquipmentNo, EQPMNT_TYP_ID, LST_SRVYR_NM, NXT_TST_DT, NXT_TST_DT, LST_TST_TYP_ID, NXT_TST_TYP_ID, VLDTY_PRD_TST_YRS, RMRKS_VC, bv_intDepotId, objTransaction)

                    'Gate In Operation
                    objEqUpdates.UpdateGateIn_Equipment(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, objTransaction)

                    'JTS EIR NO
                    objEqUpdates.UpdateGateinEirNo(bv_strOldEquipmentNo, str_GITransaction_No, strJTSEIR_No, bv_intDepotId, objTransaction)

                    'Product Id or Previous Cargo
                    objEqUpdates.UpdateGatein(bv_strOldEquipmentNo, str_GITransaction_No, Prdct_id, bv_intDepotId, objTransaction)

                    'Charge Details
                    objEqUpdates.UpdateRental_Entry(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)
                    objEqUpdates.UpdateRental_Charge(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)
                    objEqUpdates.UpdatePre_Advice(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, objTransaction)
                    objEqUpdates.UpdateTracking(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)

                    'Handling Charge
                    Dim dtCustomerCharge As New DataTable
                    Dim objGatein As New GateIns
                    Dim decHandlingCost As Decimal
                    Dim objCommon As New CommonUIs
                    If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                        dtCustomerCharge = objGatein.GetHanldingInCharge(intCustomerId, _
                                                 EQPMNT_CD_ID, _
                                                  EQPMNT_TYP_ID, _
                                                  CInt(objCommon.GetHeadQuarterID()), _
                                                  objTransaction)

                    Else
                        dtCustomerCharge = objGatein.GetHanldingInCharge(intCustomerId, _
                                                                         EQPMNT_CD_ID, _
                                                                          EQPMNT_TYP_ID, _
                                                                          bv_intDepotId, _
                                                                          objTransaction)

                    End If
                    
                    If dtCustomerCharge.Rows.Count > 0 Then
                        decHandlingCost = CDec(dtCustomerCharge.Rows(0).Item(GateinData.HNDLNG_IN_CHRG_NC))
                    Else
                        decHandlingCost = 0
                    End If

                    objEqUpdates.UpdateHandling_Charge(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, decHandlingCost, decHandlingCost, objTransaction)
                    objEqUpdates.UpdateStorage_Charge(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, objTransaction)
                    objEqUpdates.UpdateActivity_Status(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, Prdct_id, strJTSEIR_No, RMRKS_VC, objTransaction)
                    objEqUpdates.UpdateHeating_Charge(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)

                    'Add Audit_Log
                    objEqUpdates.CreateAuditLog(bv_strOldEquipmentNo, "Equipment Update", "Update", dat_EffectiveFrom_Date, sb_OldVal.ToString(), sb_NewVal.ToString(), _
                                                 bv_strReason, bv_strUserName, bv_intDepotId, objTransaction)

                    blnStatus = True

                ElseIf (str_LastActivityStatus = "IND" AndAlso dat_EffectiveFrom_Date > dat_Gate_In_Date) Or
                       (str_LastActivityStatus = "AVL" AndAlso (dat_EffectiveFrom_Date > CDate(strDate) Or dat_EffectiveFrom_Date > datLastInvc_dt)) Then

                    ' ''Bug 18989 Scenario 2 Fix
                    'Dim dtPreviousStorage As DataTable = objEqUpdates.GetPreviousStorageChargeByGITransactionNo(str_GITransaction_No, bv_intDepotId, objTransaction)
                    'Dim i32PreviousCustomerID As Int32 = 0
                    'Dim i32EquipmentStatusID As Int32 = 0
                    'Dim intNewCustomerId As Int32 = 0
                    'If dtPreviousStorage.Rows.Count > 0 Then
                    '    i32PreviousCustomerID = CInt(dtPreviousStorage.Rows(0).Item("PREV_CSTMR_ID"))
                    'End If
                    'If Not String.IsNullOrEmpty(bv_strNewCustomerId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCustomerId) Then 'Customer
                    '    intNewCustomerId = CInt(bv_strNewCustomerId)
                    'End If
                    'i32EquipmentStatusID = objEqUpdates.GetLastEquipmentStatus(str_GITransaction_No, objTransaction)

                    '' C1->C2->C1
                    If bv_blnCustomerChange = True Then
                        '' Dim dtPreviousStorage As DataTable = objEqUpdates.GetPreviousStorageChargeByGITransactionNo(str_GITransaction_No, bv_intDepotId, bv_strNewCustomerId)
                        pvt_subUpdateEquipmentInfo(bv_strOldEquipmentNo, bv_strNewCustomerId, bv_strNewEquipmentNo, bv_strNewTypeId, _
                             bv_strNewCustomerCd, bv_strOldEquipmentTypeCd, bv_strNewTypeCd, bv_strOldEquipmentTypeId, _
                             bv_strNewCodeId, bv_strOldEquipmentCodeCd, bv_strNewCodeCd, bv_strOldEquipmentCodeId, _
                             bv_strOldCustomerId, bv_strOldCustomerCd, str_GITransaction_No, bv_intDepotId, _
                             bv_strEffective_dt, blnStatus, bv_strReason, bv_strUserName,
                             dat_Gate_In_Date.ToString, _
                             dsEquipmentUpdate, objTransaction)

                    Else

                        'Create Equipment
                        Dim objEquipmentInfo As New EquipmentInformations
                        Dim EQPMNT_TYP_ID As Int32
                        Dim EQPMNT_CD_ID As Int32
                        Dim intCustomerId As Int32

                        Dim MNFCTR_DT As DateTime
                        Dim TR_WGHT_NC As Decimal
                        Dim GRSS_WGHT_NC As Decimal
                        Dim CPCTY_NC As Decimal
                        Dim LST_TST_DT As DateTime
                        Dim NXT_TST_DT As DateTime
                        Dim VLDTY_PRD_TST_YRS As String
                        Dim LST_TST_TYP_ID As Long
                        Dim NXT_TST_TYP_ID As Long
                        Dim LST_SRVYR_NM As String
                        Dim RMRKS_VC As String
                        Dim strJTSEIR_No As String
                        Dim Prdct_id As Int64
                        Dim lngEquipmentInformation As Long
                        Dim ACTV_BT As Boolean
                        Dim RNTL_BT As Boolean

                        Dim sb_OldVal As New StringBuilder
                        Dim sb_NewVal As New StringBuilder

                        sb_OldVal.Append(String.Concat("Equipment No :", bv_strOldEquipmentNo))
                        sb_NewVal.Append(String.Concat("Equipment No :", bv_strNewEquipmentNo))


                        Dim dtEquipment As DataTable = dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE)

                        If Not String.IsNullOrEmpty(bv_strNewTypeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewTypeId) Then 'Type
                            EQPMNT_TYP_ID = CInt(bv_strNewTypeId)
                            sb_OldVal.Append(String.Concat(" ,Type :", bv_strOldEquipmentTypeCd))
                            sb_NewVal.Append(String.Concat(" ,Type :", bv_strNewTypeCd))
                        Else
                            EQPMNT_TYP_ID = CInt(bv_strOldEquipmentTypeId)
                        End If

                        If Not String.IsNullOrEmpty(bv_strNewCodeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCodeId) Then 'Code
                            EQPMNT_CD_ID = CInt(bv_strNewCodeId)
                            sb_OldVal.Append(String.Concat(" ,Code :", bv_strOldEquipmentCodeCd))
                            sb_NewVal.Append(String.Concat(" ,Code :", bv_strNewCodeCd))
                        Else
                            EQPMNT_CD_ID = CInt(bv_strOldEquipmentCodeId)
                        End If

                        If Not String.IsNullOrEmpty(bv_strNewCustomerId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCustomerId) Then 'Customer
                            intCustomerId = CInt(bv_strNewCustomerId)
                            sb_OldVal.Append(String.Concat(" ,Customer :", bv_strOldCustomerCd))
                            sb_NewVal.Append(String.Concat(" ,Customer :", bv_strNewCustomerCd))
                        Else
                            intCustomerId = CInt(bv_strOldCustomerId)
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT).ToString() <> Nothing Then
                            MNFCTR_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT))
                        Else
                            MNFCTR_DT = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC).ToString() <> Nothing Then
                            TR_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC))
                        Else
                            TR_WGHT_NC = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC).ToString() <> Nothing Then
                            GRSS_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC))
                        Else
                            GRSS_WGHT_NC = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC).ToString() <> Nothing Then
                            CPCTY_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC))
                        Else
                            CPCTY_NC = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT).ToString() <> Nothing Then
                            LST_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT))
                        Else
                            LST_TST_DT = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT).ToString() <> Nothing Then
                            NXT_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT))
                        Else
                            NXT_TST_DT = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString() <> Nothing Then
                            VLDTY_PRD_TST_YRS = dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString()
                        Else
                            VLDTY_PRD_TST_YRS = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID).ToString() <> Nothing Then
                            LST_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID))
                        Else
                            LST_TST_TYP_ID = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID).ToString() <> Nothing Then
                            NXT_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID))
                        Else
                            NXT_TST_TYP_ID = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString() <> Nothing Then
                            LST_SRVYR_NM = dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString()
                        Else
                            LST_SRVYR_NM = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString() <> Nothing Then
                            RMRKS_VC = dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString()
                        Else
                            RMRKS_VC = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString() <> Nothing Then
                            strJTSEIR_No = dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString()
                        Else
                            strJTSEIR_No = Nothing
                        End If

                        If dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString() <> Nothing Then
                            Prdct_id = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString())
                        Else
                            Prdct_id = Nothing
                        End If

                        ACTV_BT = CBool(dtEquipment.Rows(0).Item(EquipmentUpdateData.ACTV_BT))
                        RNTL_BT = CBool(dtEquipment.Rows(0).Item(EquipmentUpdateData.RNTL_BT))




                        'Create Storage Charge with Old GI_Transctsn No
                        Dim objGatein As New GateIns
                        Dim intFreeDays As Int32 = 0
                        Dim decStorageCost As Decimal = 0
                        Dim decStorageTaxRate As Decimal = 0
                        Dim decStorageTotal As Decimal = 0
                        Dim str_Yard As String
                        Dim blnHeatingBit As Boolean

                        'Get GateIn Eqpment No
                        dtGate_In = objEqUpdates.Get_GateIn_Info(bv_intDepotId, str_GITransaction_No, objTransaction)
                        Dim strEqpmnet As String
                        strEqpmnet = CStr(dtGate_In.Rows(0).Item(GateinData.EQPMNT_NO))

                        If dtGate_In.Rows(0).Item(GateinData.YRD_LCTN).ToString() <> Nothing Then
                            str_Yard = dtGate_In.Rows(0).Item(GateinData.YRD_LCTN).ToString()
                        Else
                            str_Yard = Nothing
                        End If

                        If dtGate_In.Rows(0).Item(GateinData.HTNG_BT).ToString() <> Nothing Then
                            blnHeatingBit = CBool(dtGate_In.Rows(0).Item(GateinData.HTNG_BT))
                        Else
                            blnHeatingBit = Nothing
                        End If

                        Dim dat_Effective As DateTime = dat_EffectiveFrom_Date

                        Dim intEquipmentCount As Int64 = objEqUpdates.ValidateEquipment_Exist(bv_strOldEquipmentNo, bv_intDepotId, objTransaction)

                        If intEquipmentCount > 0 Then

                            'Additional Details
                            objEqUpdates.UpdateEquipmentInformation(bv_strOldEquipmentNo, EQPMNT_TYP_ID, LST_SRVYR_NM, NXT_TST_DT, NXT_TST_DT, LST_TST_TYP_ID, NXT_TST_TYP_ID, VLDTY_PRD_TST_YRS, RMRKS_VC, bv_intDepotId, objTransaction)
                        Else

                            'Create Equipment
                            lngEquipmentInformation = CInt(objEquipmentInfo.CreateEquipmentInformation(bv_strOldEquipmentNo, EQPMNT_TYP_ID, MNFCTR_DT, TR_WGHT_NC, GRSS_WGHT_NC, CPCTY_NC, _
                                                                     bv_strUserName, CDate(bv_strModifiedDate), bv_strUserName, CDate(bv_strModifiedDate), bv_intDepotId, _
                                                                    ACTV_BT, LST_SRVYR_NM, LST_TST_DT, LST_TST_TYP_ID, NXT_TST_DT, NXT_TST_TYP_ID, VLDTY_PRD_TST_YRS, RNTL_BT, RMRKS_VC, objTransaction))
                        End If


                        'Gate In Operation
                        objEqUpdates.UpdateGateIn_Equipment(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(bv_strOldCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, objTransaction) 'Equipment , code, type, customer

                        'JTS EIR NO
                        objEqUpdates.UpdateGateinEirNo(bv_strOldEquipmentNo, str_GITransaction_No, strJTSEIR_No, bv_intDepotId, objTransaction)

                        'Product Id or Previous Cargo
                        objEqUpdates.UpdateGatein(bv_strOldEquipmentNo, str_GITransaction_No, Prdct_id, bv_intDepotId, objTransaction)


                        'Delete Previous Storage - avoid Duplicate
                        objEqUpdates.Delete_StorageCharge(bv_intDepotId, str_GITransaction_No, dat_Effective.AddDays(-1), objTransaction)


                        'Update Storage Charge
                        objEqUpdates.UpdatePrevious_Storage_Charge(strEqpmnet, str_GITransaction_No, bv_intDepotId, CInt(bv_strOldCustomerId), dat_EffectiveFrom_Date.AddDays(-1), objTransaction)
                        objEqUpdates.UpdatePrevious_Storage_Charge(bv_strOldEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(bv_strOldCustomerId), dat_EffectiveFrom_Date.AddDays(-1), objTransaction)

                        If bv_strNewEquipmentNo = Nothing Then
                            bv_strNewEquipmentNo = bv_strOldEquipmentNo
                        End If

                        objGatein.CreateStorageCharge(bv_strNewEquipmentNo, EQPMNT_CD_ID, EQPMNT_TYP_ID, "STR", str_GITransaction_No, str_GITransaction_No, dat_EffectiveFrom_Date, _
                                                      Nothing, intFreeDays, Nothing, decStorageCost, decStorageTaxRate, decStorageTotal, "C", "U", "I", True, Nothing, Nothing, str_Yard, "C",
                                                     intCustomerId, bv_intDepotId, blnHeatingBit, str_GITransaction_No, Nothing, objTransaction)


                        'Add Audit_Log

                        objEqUpdates.CreateAuditLog(bv_strNewEquipmentNo, "Equipment Update", "Update", dat_EffectiveFrom_Date, sb_OldVal.ToString(), sb_NewVal.ToString(), _
                                                     bv_strReason, bv_strUserName, bv_intDepotId, objTransaction)
                        'Activity Status
                        objEqUpdates.UpdateActivity_Status(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, intCustomerId, EQPMNT_TYP_ID, EQPMNT_CD_ID, Prdct_id, strJTSEIR_No, RMRKS_VC, objTransaction)

                        blnStatus = True

                        ''C1->C2->C2
                    End If

                End If

            End If

            objTransaction.commit()

            Return blnStatus

        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "GateIn=EffectiveFrom"
    '' This method can be used to update GateIn relevant information while changing  customer  C1->C2->C1
    Private Sub pvt_subUpdateEquipmentInfo(ByVal bv_strOldEquipmentNo As String, _
                                           ByVal bv_strNewCustomerId As String, _
                                           ByVal bv_strNewEquipmentNo As String, _
                                           ByVal bv_strNewTypeId As String, _
                                           ByVal bv_strNewCustomerCd As String, _
                                           ByVal bv_strOldEquipmentTypeCd As String, _
                                           ByVal bv_strNewTypeCd As String, _
                                           ByVal bv_strOldEquipmentTypeId As String, _
                                           ByVal bv_strNewCodeId As String, _
                                           ByVal bv_strOldEquipmentCodeCd As String, _
                                           ByVal bv_strNewCodeCd As String, _
                                           ByVal bv_strOldEquipmentCodeId As String, _
                                           ByVal bv_strOldCustomerId As String, _
                                           ByVal bv_strOldCustomerCd As String, _
                                           ByVal str_GITransaction_No As String, _
                                           ByVal bv_intDepotId As Int32, _
                                           ByVal bv_strEffective_dt As String, _
                                           ByRef blnStatus As Boolean, _
                                           ByVal bv_strReason As String, _
                                           ByVal bv_strUserName As String, _
                                           ByVal bv_strGateIn_dt As String, _
                                           ByVal dsEquipmentUpdate As EquipmentUpdateDataSet, _
                                           ByRef objTransaction As Transactions)

        Try
            Dim objEqUpdates As New EquipmentUpdates
            Dim sb_OldVal As New StringBuilder
            Dim sb_NewVal As New StringBuilder
            Dim EQPMNT_TYP_ID As Int32
            Dim EQPMNT_CD_ID As Int32
            Dim intCustomerId As Int32
            Dim RMRKS_VC As String
            Dim strJTSEIR_No As String
            Dim objEquipmentInfo As New EquipmentInformations
            Dim dat_EffectiveFrom_Date As DateTime = Nothing
            Dim dat_PrevBillingFrom_Date As DateTime = Nothing
            Dim dat_StorageEffectiveFrom_Date As DateTime = Nothing
            Dim MNFCTR_DT As DateTime
            Dim TR_WGHT_NC As Decimal
            Dim GRSS_WGHT_NC As Decimal
            Dim CPCTY_NC As Decimal
            Dim LST_TST_DT As DateTime
            Dim NXT_TST_DT As DateTime
            Dim VLDTY_PRD_TST_YRS As String
            Dim LST_TST_TYP_ID As Long
            Dim NXT_TST_TYP_ID As Long
            Dim LST_SRVYR_NM As String
            Dim Prdct_id As Int64
            Dim bv_intPreviousStorageChargeID As Int32 = 0
            Dim blnUpdateGate As Boolean = False


            Dim dtPreviousStorage As DataTable = objEqUpdates.GetPreviousStorageChargeByGITransactionNo(str_GITransaction_No, bv_intDepotId, bv_strNewCustomerId)
            If dtPreviousStorage.Rows.Count > 0 Then
                bv_intPreviousStorageChargeID = CInt(dtPreviousStorage.Rows(0).Item("PREV_STRG_CHRG_ID"))
                dat_PrevBillingFrom_Date = CDate(dtPreviousStorage.Rows(0).Item("FRM_BLLNG_DT"))
            End If

            Dim result As Int32 = DateTime.Compare(CDate(dat_PrevBillingFrom_Date), CDate(bv_strGateIn_dt))

            If result = 0 Then
                blnUpdateGate = True
            End If
            If bv_strEffective_dt <> Nothing Then
                dat_EffectiveFrom_Date = CDate(bv_strEffective_dt)
            End If


            If bv_strGateIn_dt <> Nothing Then
                dat_StorageEffectiveFrom_Date = CDate(dat_PrevBillingFrom_Date)
            End If


            sb_OldVal.Append(String.Concat("Equipment No :", bv_strOldEquipmentNo))
            sb_NewVal.Append(String.Concat("Equipment No :", bv_strNewEquipmentNo))


            If Not String.IsNullOrEmpty(bv_strNewTypeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewTypeId) Then 'Type
                EQPMNT_TYP_ID = CInt(bv_strNewTypeId)
                sb_OldVal.Append(String.Concat(" ,Type :", bv_strOldEquipmentTypeCd))
                sb_NewVal.Append(String.Concat(" ,Type :", bv_strNewTypeCd))
            Else
                EQPMNT_TYP_ID = CInt(bv_strOldEquipmentTypeId)
            End If

            If Not String.IsNullOrEmpty(bv_strNewCodeId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCodeId) Then 'Code
                EQPMNT_CD_ID = CInt(bv_strNewCodeId)
                sb_OldVal.Append(String.Concat(" ,Code :", bv_strOldEquipmentCodeCd))
                sb_NewVal.Append(String.Concat(" ,Code :", bv_strNewCodeCd))
            Else
                EQPMNT_CD_ID = CInt(bv_strOldEquipmentCodeId)
            End If

            If Not String.IsNullOrEmpty(bv_strNewCustomerId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCustomerId) Then 'Customer
                intCustomerId = CInt(bv_strNewCustomerId)
                sb_OldVal.Append(String.Concat(" ,Customer :", bv_strOldCustomerCd))
                sb_NewVal.Append(String.Concat(" ,Customer :", bv_strNewCustomerCd))
            Else
                intCustomerId = CInt(bv_strOldCustomerId)
            End If

            Dim dtEquipment As DataTable = dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE)


            'Gare in JTS EIR No & Equipment Remarks

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT).ToString() <> Nothing Then
                MNFCTR_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.MNFCTR_DT))
            Else
                MNFCTR_DT = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC).ToString() <> Nothing Then
                TR_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.TR_WGHT_NC))
            Else
                TR_WGHT_NC = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC).ToString() <> Nothing Then
                GRSS_WGHT_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.GRSS_WGHT_NC))
            Else
                GRSS_WGHT_NC = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC).ToString() <> Nothing Then
                CPCTY_NC = CDec(dtEquipment.Rows(0).Item(EquipmentUpdateData.CPCTY_NC))
            Else
                CPCTY_NC = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT).ToString() <> Nothing Then
                LST_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_DT))
            Else
                LST_TST_DT = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT).ToString() <> Nothing Then
                NXT_TST_DT = CDate(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_DT))
            Else
                NXT_TST_DT = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString() <> Nothing Then
                VLDTY_PRD_TST_YRS = dtEquipment.Rows(0).Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS).ToString()
            Else
                VLDTY_PRD_TST_YRS = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID).ToString() <> Nothing Then
                LST_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_TST_TYP_ID))
            Else
                LST_TST_TYP_ID = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID).ToString() <> Nothing Then
                NXT_TST_TYP_ID = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.NXT_TST_TYP_ID))
            Else
                NXT_TST_TYP_ID = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString() <> Nothing Then
                LST_SRVYR_NM = dtEquipment.Rows(0).Item(EquipmentUpdateData.LST_SRVYR_NM).ToString()
            Else
                LST_SRVYR_NM = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString() <> Nothing Then
                RMRKS_VC = dtEquipment.Rows(0).Item(EquipmentUpdateData.INSTRCTNS_VC).ToString()
            Else
                RMRKS_VC = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString() <> Nothing Then
                strJTSEIR_No = dtEquipment.Rows(0).Item(EquipmentUpdateData.GI_RF_NO).ToString()
            Else
                strJTSEIR_No = Nothing
            End If

            If dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString() <> Nothing Then
                Prdct_id = CLng(dtEquipment.Rows(0).Item(EquipmentUpdateData.PRDCT_ID).ToString())
            Else
                Prdct_id = Nothing
            End If



            Dim intEquipmentCount As Int64 = objEqUpdates.ValidateEquipment_Exist(bv_strNewEquipmentNo, bv_intDepotId, objTransaction)

            If intEquipmentCount > 0 AndAlso bv_strOldEquipmentNo <> bv_strNewEquipmentNo Then
                objEqUpdates.DeleteEquipmentInformationByEquipmentNo(bv_strNewEquipmentNo, objTransaction)
            End If
            ''


            'Update Equipment_information
            objEqUpdates.UpdateEquipmentNo(bv_strOldEquipmentNo, bv_strNewEquipmentNo, EQPMNT_TYP_ID, bv_intDepotId, RMRKS_VC, objTransaction)

            'Additional Details
            objEqUpdates.UpdateEquipmentInformation(bv_strNewEquipmentNo, EQPMNT_TYP_ID, LST_SRVYR_NM, NXT_TST_DT, NXT_TST_DT, LST_TST_TYP_ID, NXT_TST_TYP_ID, VLDTY_PRD_TST_YRS, RMRKS_VC, bv_intDepotId, objTransaction)

            ''Update for Equipment in GateIn Status


            'Delete Previous Storage - avoid Duplicate
            objEqUpdates.Delete_StorageCharge(bv_intDepotId, str_GITransaction_No, dat_StorageEffectiveFrom_Date, objTransaction)


            If blnUpdateGate = True Then

                'Gate In Operation
                objEqUpdates.UpdateGateIn_Equipment(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, objTransaction) 'Equipment , code, type, customer
                'JTS EIR NO
                objEqUpdates.UpdateGateinEirNo(bv_strNewEquipmentNo, str_GITransaction_No, strJTSEIR_No, bv_intDepotId, objTransaction)
                'Product Id or Previous Cargo
                objEqUpdates.UpdateGatein(bv_strNewEquipmentNo, str_GITransaction_No, Prdct_id, bv_intDepotId, objTransaction)
                objEqUpdates.UpdateRental_Entry(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)
                objEqUpdates.UpdateRental_Charge(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)
                objEqUpdates.UpdatePre_Advice(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, objTransaction)
                objEqUpdates.UpdateTracking(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)
                'Handling Charge
                Dim dtCustomerCharge As New DataTable
                Dim objGatein As New GateIns
                Dim decHandlingCost As Decimal

                dtCustomerCharge = objGatein.GetHanldingInCharge(intCustomerId, _
                                             EQPMNT_CD_ID, _
                                              EQPMNT_TYP_ID, _
                                              bv_intDepotId, _
                                              objTransaction)

                If dtCustomerCharge.Rows.Count > 0 Then
                    decHandlingCost = CDec(dtCustomerCharge.Rows(0).Item(GateinData.HNDLNG_IN_CHRG_NC))
                Else
                    decHandlingCost = 0
                End If
                objEqUpdates.UpdateHandling_Charge(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, decHandlingCost, decHandlingCost, objTransaction)
                objEqUpdates.UpdateHeating_Charge(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), objTransaction)
            End If


            ''Common Updates
            objEqUpdates.UpdatePrevious_Storage_ChargeByStorageChargeID(bv_intPreviousStorageChargeID, bv_strNewEquipmentNo, objTransaction)
            objEqUpdates.UpdateActivity_Status(bv_strNewEquipmentNo, str_GITransaction_No, bv_intDepotId, CInt(intCustomerId), EQPMNT_TYP_ID, EQPMNT_CD_ID, Prdct_id, strJTSEIR_No, RMRKS_VC, objTransaction)
            objEqUpdates.CreateAuditLog(bv_strNewEquipmentNo, "Equipment Update", "Update", dat_EffectiveFrom_Date, sb_OldVal.ToString(), sb_NewVal.ToString(), _
                                         bv_strReason, bv_strUserName, bv_intDepotId, objTransaction)

            blnStatus = True

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub


#End Region
#Region "pub_GetPreviousStorageChargeByGITransactionNo"
    '' This method can be used to check C1->C2->C1
    <OperationContract()> _
    Public Function pub_GetPreviousStorageChargeByGITransactionNo(ByVal bv_strGITransactionNo As String, _
                                                                  ByVal bv_i32DepotId As Int32, _
                                                                  ByVal bv_strOldCustomerId As String, _
                                                                  ByVal bv_strNewCustomerId As String, ByRef bv_strMessage As String) As Boolean
        Try
            Dim objEqUpdates As New EquipmentUpdates
            ''Bug 18989 Scenario 2 Fix
            Dim dtPreviousStorage As DataTable = objEqUpdates.GetPreviousStorageChargeByGITransactionNo(bv_strGITransactionNo, bv_i32DepotId, bv_strNewCustomerId)
            Dim i32PreviousCustomerID As Int32 = 0
            Dim i32EquipmentStatusID As Int32 = 0
            Dim intNewCustomerId As Int32 = 0
            If dtPreviousStorage.Rows.Count > 0 Then
                i32PreviousCustomerID = CInt(dtPreviousStorage.Rows(0).Item("PREV_CSTMR_ID"))
            End If
            If Not String.IsNullOrEmpty(bv_strNewCustomerId) AndAlso Not String.IsNullOrWhiteSpace(bv_strNewCustomerId) Then 'Customer
                intNewCustomerId = CInt(bv_strNewCustomerId)
            End If
            i32EquipmentStatusID = objEqUpdates.GetLastEquipmentStatus(bv_strGITransactionNo)
            If i32PreviousCustomerID = intNewCustomerId AndAlso i32PreviousCustomerID > 0 Then
                'If i32EquipmentStatusID = 1 Then
                bv_strMessage = "Show Confirmation Message"
                'Else
                '    bv_strMessage = "Show Error Message"
                'End If
            Else
                bv_strMessage = "Allow"
            End If
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetPreviousStorageChargeByGITransactionNo"
    '' This method can be used to check C1->C2->C1
    <OperationContract()> _
    Public Function pub_GetGITransactionNoByEquipmentNo(ByVal bv_strOldEquipmentNo As String, _
                                                                  ByVal bv_i32DepotId As Int32) As String
        Try
            Dim objEqUpdates As New EquipmentUpdates
            Dim strGI_Transaction_No As String = Get_GateIn_GITransaction_No(bv_strOldEquipmentNo, bv_i32DepotId)
            Return strGI_Transaction_No
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class
