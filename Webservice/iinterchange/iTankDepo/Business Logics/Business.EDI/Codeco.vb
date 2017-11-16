Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports System.Text

Public Class Codeco

#Region "Declarations"
    Private bv_strCustomerCode As String
    Private bv_strDepotCode As String
    Private Const pvt_NewLine As String = "NewLine"
    Private Const pvt_TransTableKey As String = "TransTableKey"
    Dim intsegCount As Integer
    Dim intCount As Integer
#End Region

#Region "New()"
    Public Sub New(ByVal strCustomerCode As String, ByVal strDepotCode As String)
        bv_strCustomerCode = strCustomerCode
        bv_strDepotCode = strDepotCode
    End Sub
#End Region

#Region "WriteGatinFileCodeco()"
    Public Function WriteCodeco(ByVal bv_dsEdiDataSet As DataSet, ByVal bv_strEDIMovement As String, ByVal bv_strGenerationMode As String) As String
        Try
            Dim aDrMovement() As DataRow
            Dim aDrSegmentHead() As DataRow
            Dim aDrSegmentChild() As DataRow
            Dim aDrSegmentFoot() As DataRow

            Dim introwcount As Int32
            Dim rowcount As Int16

            Dim HeadFilterExp As String
            Dim ChildFilterExp As String
            Dim FooterFilterExp As String
            Dim strTableName As String = Nothing

            Dim strMsg As String
            Dim strMsgtable As String = Nothing



            If bv_strEDIMovement = "GATEIN" Then
                strTableName = EDIData._GATEIN_RET
                strMsgtable = "Gate In"
            ElseIf bv_strEDIMovement = "GATEOUT" Then
                strTableName = EDIData._GATEOUT_RET
                strMsgtable = "Gate Out"
            ElseIf bv_strEDIMovement = "REPAIRCOMPLETE" Then
                strTableName = EDIData._GATEIN_RET
                strMsgtable = "Repair Complete"

            End If

            If bv_dsEdiDataSet.Tables(strTableName).Rows.Count = 0 Then
                strMsg = "No " + strMsgtable + " Records found for EDI generation"
                Return strMsg
            End If

            Try

                aDrMovement = bv_dsEdiDataSet.Tables(EDIData._EDI_MOVEMENT).Select(String.Concat(EDIData.MVMNT_NAM, "='", bv_strEDIMovement, "'"))
                If aDrMovement.Length > 0 Then
                    HeadFilterExp = String.Concat(EDIData.MVMNT_ID, "=", aDrMovement(0).Item(EDIData.MVMNT_ID)) & " AND " & EDIData.SGMNT_TYP & "= 'HEAD'"
                    ChildFilterExp = String.Concat(EDIData.MVMNT_ID, "=", aDrMovement(0).Item(EDIData.MVMNT_ID)) & " AND " & EDIData.SGMNT_TYP & "= 'CHILD'"
                    FooterFilterExp = String.Concat(EDIData.MVMNT_ID, "=", aDrMovement(0).Item(EDIData.MVMNT_ID)) & " AND " & EDIData.SGMNT_TYP & "= 'FOOTER'"
                    aDrSegmentHead = bv_dsEdiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Select(HeadFilterExp)
                    aDrSegmentChild = bv_dsEdiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Select(ChildFilterExp)
                    aDrSegmentFoot = bv_dsEdiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Select(FooterFilterExp)


                    introwcount = bv_dsEdiDataSet.Tables(strTableName).Rows.Count

                    strMsg = pvt_strCreateEDIString(aDrSegmentHead, aDrSegmentChild, aDrSegmentFoot, bv_dsEdiDataSet, rowcount, bv_strEDIMovement, bv_strGenerationMode)
                    Return strMsg
                End If

            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pvt_strCreateEDIString"

    Private Function pvt_strCreateEDIString(ByVal bv_aDrSegmentHead() As DataRow, ByVal bv_aDrSegmentChild() As DataRow, _
                                            ByVal bv_aDrSegmentFoot() As DataRow, ByVal bv_dsEdiDataSet As DataSet, ByVal mainrowcount As Int32, _
                                            ByVal strMovement As String, ByVal bv_strGenerationMode As String) As String
        Dim sbEDIParentData As New StringBuilder
        Dim sbHeadEDIData As New StringBuilder
        Dim sbChildEDIData As New StringBuilder
        Dim sbFooterEDIData As New StringBuilder
        Dim strFINALCHILDEDIDATA As StringBuilder = New StringBuilder
        Dim strFileContents As StringBuilder = New StringBuilder
        Dim sbEDIChildDetailData As StringBuilder = New StringBuilder
        Dim aDrChildDetails() As DataRow
        Dim aDrchildrow() As DataRow
        Dim intchildrowcount As Int32
        Dim intChildDetailsIndex As Int32
        Dim intChildSegIndex, intTableIndex As Int32
        Dim strchildclear As String
        Dim strLineDelimiter1 As String
        Dim strResult As String = Nothing
        Dim strCustomize As String
        Dim strHSNO As String
        Dim strTransmissionNo As String
        Dim dateir As Date
        Dim strTime As String
        Dim strYear As String
        Dim strMonth As String
        Dim strDay As String
        Dim strSNO As New StringBuilder
        Dim strTrans As New StringBuilder
        strFileContents.Append("")
        intTableIndex = 0
        Dim strTableName As String = Nothing
        Dim objWriteEDI As New WriteEDI(bv_strCustomerCode, bv_strDepotCode)
        Dim strTrace As New StringBuilder


        ''''''''''HEADER SEGMENTS CONTENTS''''''''''''''''''''''''''''
        sbHeadEDIData = pub_HeaderSegProcess(bv_aDrSegmentHead, bv_dsEdiDataSet, mainrowcount, strMovement)
        '''''''''''END'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '''''''''CHILD'''''''''''''''''''''''''''''''''''''''''''''''''
        Try

            If strMovement = "GATEIN" Then
                strTableName = EDIData._GATEIN_RET
            ElseIf strMovement = "GATEOUT" Then
                strTableName = EDIData._GATEOUT_RET
            ElseIf strMovement = "REPAIRCOMPLETE" Then
                strTableName = EDIData._GATEIN_RET
            End If

            aDrchildrow = bv_dsEdiDataSet.Tables(strTableName).Select("")

            If aDrchildrow.Length > 0 Then
                intCount = aDrchildrow.Length
                For intchildrowcount = 0 To aDrchildrow.Length - 1
                    strHSNO = bv_dsEdiDataSet.Tables(strTableName).Rows(intchildrowcount).Item(0)
                    strTransmissionNo = bv_dsEdiDataSet.Tables(strTableName).Rows(intchildrowcount).Item(1)
                    'If intchildrowcount <> aDrchildrow.Length - 1 Then
                    strSNO.Append(strHSNO.ToString & ",")
                    'strTrans.Append(strTransmissionNo.ToString & ",")
                    strTrans.Append(strHSNO.ToString & ",")
                    'Else
                    'strSNO.Append(strHSNO.ToString)
                    'strTrans.Append(strTransmissionNo.ToString)
                    'End If

                    For intChildSegIndex = 0 To bv_aDrSegmentChild.Length - 1
                        aDrChildDetails = bv_dsEdiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Select(String.Concat(EDIData.SGMNT_HDR_ID, "=", bv_aDrSegmentChild(intChildSegIndex).Item(EDIData.SGMNT_HDR_ID)), EDIData.SGMNT_DTL_ORDR)  'segment details for each segment
                        Try
                            For intChildDetailsIndex = 0 To aDrChildDetails.Length - 1
                                If Convert.ToString(aDrChildDetails(intChildDetailsIndex).Item(EDIData.TRNS_CLMN)).Trim() <> String.Empty Then
                                    If aDrChildDetails(intChildDetailsIndex).Item(EDIData.RFRNC) = "True" Then
                                        sbEDIChildDetailData.Append(aDrchildrow(intchildrowcount)(Convert.ToString(aDrChildDetails(intChildDetailsIndex).Item(EDIData.TRNS_CLMN))).ToString.Trim)
                                    Else
                                        If Convert.ToString(aDrChildDetails(intChildDetailsIndex).Item(EDIData.SGMNT_DTL_CMMNTS)).Trim() <> String.Empty Then
                                            If aDrChildDetails(intChildDetailsIndex).Item(EDIData.SGMNT_DTL_CMMNTS) = "CUSTOMIZE" Then
                                                strCustomize = aDrChildDetails(intChildDetailsIndex).Item(EDIData.TRNS_CLMN).ToString.Trim

                                                If strCustomize = "CONDITION" And strMovement = "GATEIN" Then
                                                    If aDrchildrow(intchildrowcount)(EDIData.GI_CONDITION).ToString.Trim = "C" Then
                                                        sbEDIChildDetailData.Append("999")
                                                    Else
                                                        sbEDIChildDetailData.Append("34")
                                                    End If
                                                ElseIf strCustomize = "EIRDATE" And strMovement = "GATEIN" Then
                                                    If IsDBNull(aDrchildrow(intchildrowcount)(EDIData.GI_EIR_DATE)) = "FALSE" And IsDBNull(aDrchildrow(intchildrowcount)(EDIData.GI_EIR_TIME).ToString) = "FALSE" Then
                                                        dateir = aDrchildrow(intchildrowcount)(EDIData.GI_EIR_DATE)
                                                        strTime = aDrchildrow(intchildrowcount)(EDIData.GI_EIR_TIME).ToString.Trim
                                                        strYear = dateir.Year

                                                        If dateir.Month < 9 Then
                                                            strMonth = String.Concat("0", dateir.Month)
                                                        Else
                                                            strMonth = dateir.Month
                                                        End If
                                                        If dateir.Day < 9 Then
                                                            strDay = String.Concat("0", dateir.Day)
                                                        Else
                                                            strDay = dateir.Day
                                                        End If

                                                        sbEDIChildDetailData.Append(String.Concat(strYear, strMonth, strDay, strTime.Substring(0, 2), strTime.Substring(3, 2)))
                                                    End If
                                                ElseIf strCustomize = "EIRDATE" And strMovement = "GATEOUT" Then
                                                    If IsDBNull(aDrchildrow(intchildrowcount)(EDIData.GO_EIR_DATE)) = "FALSE" And IsDBNull(aDrchildrow(intchildrowcount)(EDIData.GO_EIR_TIME).ToString) = "FALSE" Then
                                                        dateir = aDrchildrow(intchildrowcount)(EDIData.GO_EIR_DATE)
                                                        strTime = aDrchildrow(intchildrowcount)(EDIData.GO_EIR_TIME).ToString.Trim
                                                        strYear = dateir.Year

                                                        If dateir.Month < 9 Then
                                                            strMonth = String.Concat("0", dateir.Month)
                                                        Else
                                                            strMonth = dateir.Month
                                                        End If
                                                        If dateir.Day < 9 Then
                                                            strDay = String.Concat("0", dateir.Day)
                                                        Else
                                                            strDay = dateir.Day
                                                        End If

                                                        sbEDIChildDetailData.Append(String.Concat(strYear, strMonth, strDay, strTime.Substring(0, 2), strTime.Substring(3, 2)))
                                                    End If
                                                End If
                                            Else
                                                sbEDIChildDetailData.Append(aDrChildDetails(intChildDetailsIndex).Item(EDIData.TRNS_CLMN))
                                            End If
                                        Else
                                            sbEDIChildDetailData.Append(aDrChildDetails(intChildDetailsIndex).Item(EDIData.TRNS_CLMN))
                                        End If


                                    End If
                                End If
                                sbEDIChildDetailData.Append(Convert.ToString(aDrChildDetails(intChildDetailsIndex).Item(EDIData.SGMNT_DTL_DLMTR)))
                            Next


                            If IsDBNull(bv_aDrSegmentChild(intChildSegIndex).Item(EDIData.SGMNT_HDR)) = False Then
                                sbChildEDIData.Append(bv_aDrSegmentChild(intChildSegIndex).Item(EDIData.SGMNT_HDR))
                                sbChildEDIData.Append(bv_aDrSegmentChild(intChildSegIndex).Item(EDIData.SGMNT_DLMTR))
                            End If

                            sbChildEDIData.Append(sbEDIChildDetailData.ToString())
                            strLineDelimiter1 = Convert.ToString(bv_aDrSegmentChild(intChildSegIndex).Item(EDIData.LN_DLMTR))
                            sbChildEDIData.Append(vbCrLf)
                            intsegCount = intsegCount + 1
                            strchildclear = sbEDIChildDetailData.ToString
                            sbEDIChildDetailData.Replace(strchildclear, String.Empty)

                        Catch ex As Exception
                            strTrace.AppendLine(String.Concat("EDIGenerator_pvt_strCreateEDIString", " : ", Now.ToString, " - ", ex.Message))
                            objWriteEDI.pub_WriteText(strTrace, ".txt")
                            strTrace.Clear()
                        End Try
                    Next
                Next
            End If 'end child
            ''''''''''''''END'''''''''''''''''''''''''''''
            ''''''''''''FOOTER''''''''''''''''''''''''''''''''
            sbFooterEDIData = pub_FooterSegProcess(bv_aDrSegmentFoot, bv_dsEdiDataSet, mainrowcount, strTableName)
            '''''''''''END'''''''''''''''''''''''''''''''''''''
            strFileContents.Append(sbHeadEDIData)
            strFileContents.Append(sbChildEDIData)
            strFileContents.Append(sbFooterEDIData)

            Dim strCodecoFileName As String = String.Concat(strMovement, Now.Year, Now.Month, Now.Day, Now.Hour, Now.Minute, Now.Second)
            objWriteEDI.pub_WriteSpecificCodecoFile(strCodecoFileName, strFileContents, bv_strGenerationMode)

            Dim strFileName As String = objWriteEDI.pub_RenameFilesToExtension()
            If strMovement = "GATEIN" Then
                ReportGateinUpdate(strSNO.ToString(), strTrans.ToString)
                ' strResult = "Gate In Codeco file generated successfully"
                strResult = String.Concat("Gate In Codeco file generated successfully", ",", strCodecoFileName, ".txt")
            ElseIf strMovement = "GATEOUT" Then
                ReportGateoutUpdate(strSNO.ToString, strTrans.ToString)
                If strFileName.Contains("Gateout") Then
                    strResult = "Gate Out Codeco  file generated successfully"
                Else
                    strResult = String.Concat("Gate Out Codeco  file generated successfully", ",", strCodecoFileName, ".txt")
                End If

            ElseIf strMovement = "REPAIRCOMPLETE" Then
                ReportGateinUpdate(strSNO.ToString(), strTrans.ToString)
                '   strResult = "Repair Complete Codeco file generated successfully"
                strResult = String.Concat("Repair Complete Codeco file generated successfully", ",", strCodecoFileName, ".txt")
            End If

            Return strResult
        Catch ex As Exception
            strTrace.AppendLine(String.Concat("EDIGenerator_pvt_strCreateEDIString", " : ", Now.ToString, " - ", ex.Message))
            objWriteEDI.pub_WriteText(strTrace, ".txt")
            strTrace.Clear()
            If strMovement = "GATEIN" Then
                Return "Gate In Codeco  file generation failed"
            ElseIf strMovement = "GATEIN" Then
                Return "Gate Out Codeco  file generation failed"
            End If

        End Try

    End Function

#End Region


#Region "pub_HeaderSegProcess"
    Public Function pub_HeaderSegProcess(ByVal bv_aDrSegmentHead() As DataRow, ByVal bv_dsEdiDataSet As DataSet, ByVal mainrowcount As Int32, ByVal strMovement As String) As StringBuilder
        Try
            Dim strTrace As New StringBuilder
            Dim intHeadSegIndex, intHeadDetailsIndex As Int32
            Dim aDrHeadDetails() As DataRow
            Dim sbEDIHeaderDetailData As New StringBuilder
            Dim sbHeadEDIData As New StringBuilder
            Dim strLineDelimiter As String
            Dim strHeadclear As String
            Dim strSentDate As String
            Dim strTime As String
            Dim strTrnsxn As String
            Dim strSentdt As String

            Dim strAmendcheck As String
            Dim strTablename As String = Nothing

            If strMovement = "GATEIN" Then
                strTablename = EDIData._GATEIN_RET
            ElseIf strMovement = "GATEOUT" Then
                strTablename = EDIData._GATEOUT_RET
            ElseIf strMovement = "REPAIRCOMPLETE" Then
                strTablename = EDIData._GATEIN_RET
            End If

            For intHeadSegIndex = 0 To bv_aDrSegmentHead.Length - 1

                aDrHeadDetails = bv_dsEdiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Select(String.Concat(EDIData.SGMNT_HDR_ID, "=", bv_aDrSegmentHead(intHeadSegIndex).Item(EDIData.SGMNT_HDR_ID)), EDIData.SGMNT_DTL_ORDR)
                Try
                    For intHeadDetailsIndex = 0 To aDrHeadDetails.Length - 1   'each segment field no
                        If Convert.ToString(aDrHeadDetails(intHeadDetailsIndex).Item(EDIData.TRNS_CLMN)) = pvt_TransTableKey Then
                            sbEDIHeaderDetailData.Append(bv_dsEdiDataSet.Tables(strTablename).TableName)
                        ElseIf Convert.ToString(aDrHeadDetails(intHeadDetailsIndex).Item(EDIData.TRNS_CLMN)).Trim() <> String.Empty Then
                            If (aDrHeadDetails(intHeadDetailsIndex).Item(EDIData.RFRNC)) = "True" Then
                                If IsDBNull(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount)(Convert.ToString(aDrHeadDetails(intHeadDetailsIndex).Item(EDIData.TRNS_CLMN)))) <> True Then
                                    sbEDIHeaderDetailData.Append(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount)(Convert.ToString(aDrHeadDetails(intHeadDetailsIndex).Item(EDIData.TRNS_CLMN))).ToString.Trim)
                                End If

                            Else
                                strAmendcheck = aDrHeadDetails(intHeadDetailsIndex).Item(EDIData.TRNS_CLMN).ToString.Trim

                                If strAmendcheck = "REFERENCE" And strMovement = "GATEIN" Then
                                    sbEDIHeaderDetailData.Append(String.Concat(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_DPT_TRM).ToString.Trim, Now.Day, Now.Month, Now.Year, Now.Hour, Now.Minute, Now.Second))
                                ElseIf strAmendcheck = "REFERENCE" And strMovement = "GATEOUT" Then
                                    sbEDIHeaderDetailData.Append(String.Concat(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GO_DPT_TRM).ToString.Trim, Now.Day, Now.Month, Now.Year, Now.Hour, Now.Minute, Now.Second))
                                ElseIf strAmendcheck = "SENTDATE" And strMovement = "GATEIN" Then
                                    If IsDBNull(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_SENT_DATE)) <> True Then
                                        strSentDate = bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_SENT_DATE).ToString.Trim
                                        'strSentdt = String.Concat(strSentDate.Substring(4, 4), strSentDate.Substring(0, 4))
                                        strSentdt = String.Concat(strSentDate.Substring(2, 6))
                                        sbEDIHeaderDetailData.Append(strSentdt)
                                    End If
                                ElseIf strAmendcheck = "SENTDATE" And strMovement = "REPAIRCOMPLETE" Then
                                    If IsDBNull(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_SENT_DATE)) <> True Then
                                        strSentDate = bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_SENT_DATE).ToString.Trim
                                        'strSentdt = String.Concat(strSentDate.Substring(4, 4), strSentDate.Substring(0, 4))
                                        strSentdt = String.Concat(strSentDate.Substring(2, 6))
                                        sbEDIHeaderDetailData.Append(strSentdt)
                                    End If

                                ElseIf strAmendcheck = "SENTDATE" And strMovement = "GATEOUT" Then
                                    If IsDBNull(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GO_SENT_DATE)) <> True Then
                                        strSentDate = bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GO_SENT_DATE).ToString.Trim
                                        strSentdt = String.Concat(strSentDate.Substring(2, 6))
                                        sbEDIHeaderDetailData.Append(strSentdt)
                                    End If
                                ElseIf strAmendcheck = "EIRTIME" And strMovement = "GATEIN" Then
                                    If IsDBNull(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_EIR_TIME)) <> True Then
                                        strTime = bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_EIR_TIME).ToString.Trim
                                        sbEDIHeaderDetailData.Append(String.Concat(strTime.Substring(0, 2), strTime.Substring(3, 2)))
                                    End If
                                ElseIf strAmendcheck = "EIRTIME" And strMovement = "REPAIRCOMPLETE" Then
                                    If IsDBNull(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_EIR_TIME)) <> True Then
                                        strTime = bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_EIR_TIME).ToString.Trim
                                        sbEDIHeaderDetailData.Append(String.Concat(strTime.Substring(0, 2), strTime.Substring(3, 2)))
                                    End If
                                ElseIf strAmendcheck = "EIRTIME" And strMovement = "GATEOUT" Then
                                    If IsDBNull(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GO_EIR_TIME)) <> True Then
                                        strTime = bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GO_EIR_TIME).ToString.Trim
                                        sbEDIHeaderDetailData.Append(String.Concat(strTime.Substring(0, 2), strTime.Substring(3, 2)))
                                    End If
                                ElseIf strAmendcheck = "TRNSXN" And strMovement = "GATEIN" Then
                                    If IsDBNull(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_TRNSXN)) <> True Then
                                        strTrnsxn = bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_TRNSXN).ToString.Trim
                                        sbEDIHeaderDetailData.Append(strTrnsxn.Substring(strTrnsxn.Length - 5, 5))
                                    End If
                                ElseIf strAmendcheck = "TRNSXN" And strMovement = "REPAIRCOMPLETE" Then
                                    If IsDBNull(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_TRNSXN)) <> True Then
                                        strTrnsxn = bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GI_TRNSXN).ToString.Trim
                                        sbEDIHeaderDetailData.Append(strTrnsxn.Substring(strTrnsxn.Length - 5, 5))
                                    End If
                                ElseIf strAmendcheck = "TRNSXN" And strMovement = "GATEOUT" Then
                                    If IsDBNull(bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GO_TRNSXN)) <> True Then
                                        strTrnsxn = bv_dsEdiDataSet.Tables(strTablename).Rows(mainrowcount).Item(EDIData.GO_TRNSXN).ToString.Trim
                                        sbEDIHeaderDetailData.Append(strTrnsxn.Substring(strTrnsxn.Length - 5, 5))
                                    End If
                                Else
                                    sbEDIHeaderDetailData.Append(strAmendcheck)
                                End If
                            End If
                        End If

                        sbEDIHeaderDetailData.Append(Convert.ToString(aDrHeadDetails(intHeadDetailsIndex).Item(EDIData.SGMNT_DTL_DLMTR)))
                    Next

                Catch ex As Exception
                    Dim objWriteEDI As New WriteEDI(bv_strCustomerCode, bv_strDepotCode)
                    strTrace.AppendLine(String.Concat("EDIGenerator_pub_HeaderSegProcess", " : ", Now.ToString, " - ", ex.Message))
                    objWriteEDI.pub_WriteText(strTrace, ".txt")
                    strTrace.Clear()
                End Try


                If IsDBNull(bv_aDrSegmentHead(intHeadSegIndex).Item(EDIData.SGMNT_HDR)) = False Then
                    sbHeadEDIData.Append(bv_aDrSegmentHead(intHeadSegIndex).Item(EDIData.SGMNT_HDR))
                    sbHeadEDIData.Append(bv_aDrSegmentHead(intHeadSegIndex).Item(EDIData.SGMNT_DLMTR))
                End If

                sbHeadEDIData.Append(sbEDIHeaderDetailData.ToString())
                strLineDelimiter = Convert.ToString(bv_aDrSegmentHead(intHeadSegIndex).Item(EDIData.LN_DLMTR))
                sbHeadEDIData.Append(vbCrLf)
                intsegCount = intsegCount + 1
                strHeadclear = sbEDIHeaderDetailData.ToString
                sbEDIHeaderDetailData.Replace(strHeadclear, String.Empty)
            Next
            Return sbHeadEDIData
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "pub_FooterSegProcess"
    Public Function pub_FooterSegProcess(ByVal bv_aDrSegmentFoot() As DataRow, ByVal bv_dsEdiDataSet As DataSet, ByVal mainrowcount As Int32, ByVal strTableName As String) As StringBuilder
        Dim intSegFootIndex, intFooterDetailsIndex As Int32
        Dim aDrFootDetails() As DataRow
        Dim sbEDIFooterDetailData As StringBuilder = New StringBuilder
        Dim sbFooterEDIData As New StringBuilder
        Dim strLineDelimiter1 As String
        Dim STRFOORCLEAR As String
        Dim strTrace As New StringBuilder

        For intSegFootIndex = 0 To bv_aDrSegmentFoot.Length - 1

            aDrFootDetails = bv_dsEdiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Select(String.Concat(EDIData.SGMNT_HDR_ID, "=", bv_aDrSegmentFoot(intSegFootIndex).Item(EDIData.SGMNT_HDR_ID)), EDIData.SGMNT_DTL_ORDR)  'segment details for each segment
            Try
                For intFooterDetailsIndex = 0 To aDrFootDetails.Length - 1  'each segment field no

                    If Convert.ToString(aDrFootDetails(intFooterDetailsIndex).Item(EDIData.TRNS_CLMN)) = pvt_TransTableKey Then
                        sbEDIFooterDetailData.Append(bv_dsEdiDataSet.Tables(strTableName).TableName)
                    ElseIf Convert.ToString(aDrFootDetails(intFooterDetailsIndex).Item(EDIData.TRNS_CLMN)).Trim() <> String.Empty Then
                        If aDrFootDetails(intFooterDetailsIndex).Item(EDIData.RFRNC) = "True" Then
                            sbEDIFooterDetailData.Append(bv_dsEdiDataSet.Tables(strTableName).Rows(mainrowcount)(Convert.ToString(aDrFootDetails(intFooterDetailsIndex).Item(EDIData.TRNS_CLMN))).ToString.Trim)
                        Else
                            sbEDIFooterDetailData.Append(aDrFootDetails(intFooterDetailsIndex).Item(EDIData.TRNS_CLMN).ToString.Trim)
                        End If
                    End If
                    If (intFooterDetailsIndex = 1 And bv_aDrSegmentFoot(intSegFootIndex).Item(EDIData.SGMNT_HDR_ID) = 10) Then
                        sbEDIFooterDetailData.Append(intCount)
                    ElseIf (intFooterDetailsIndex = 1 And bv_aDrSegmentFoot(intSegFootIndex).Item(EDIData.SGMNT_HDR_ID) = 22) Then
                        sbEDIFooterDetailData.Append(intCount)
                    ElseIf (intFooterDetailsIndex = 0 And bv_aDrSegmentFoot(intSegFootIndex).Item(EDIData.SGMNT_HDR_ID) = 11) Then
                        sbEDIFooterDetailData.Append(intsegCount)
                    ElseIf (intFooterDetailsIndex = 0 And bv_aDrSegmentFoot(intSegFootIndex).Item(EDIData.SGMNT_HDR_ID) = 23) Then
                        sbEDIFooterDetailData.Append(intsegCount)
                    End If
                    sbEDIFooterDetailData.Append(Convert.ToString(aDrFootDetails(intFooterDetailsIndex).Item(EDIData.SGMNT_DTL_DLMTR)))

                Next

                If IsDBNull(bv_aDrSegmentFoot(intSegFootIndex).Item(EDIData.SGMNT_HDR)) = False Then
                    sbFooterEDIData.Append(bv_aDrSegmentFoot(intSegFootIndex).Item(EDIData.SGMNT_HDR))
                    sbFooterEDIData.Append(bv_aDrSegmentFoot(intSegFootIndex).Item(EDIData.SGMNT_DLMTR))
                End If

                sbFooterEDIData.Append(sbEDIFooterDetailData.ToString())
                strLineDelimiter1 = Convert.ToString(bv_aDrSegmentFoot(intSegFootIndex).Item(EDIData.LN_DLMTR))
                sbFooterEDIData.Append(vbCrLf)
                intsegCount = intsegCount + 1
                STRFOORCLEAR = sbEDIFooterDetailData.ToString
                sbEDIFooterDetailData.Replace(STRFOORCLEAR, String.Empty)

            Catch ex As Exception
                Dim objWriteEDI As New WriteEDI(bv_strCustomerCode, bv_strDepotCode)
                strTrace.AppendLine(String.Concat("EDIGenerator_pub_HeaderSegProcess", " : ", Now.ToString, " - ", ex.Message))
                objWriteEDI.pub_WriteText(strTrace, ".txt")
                strTrace.Clear()
            End Try
        Next 'End Footer

        Return sbFooterEDIData
    End Function
#End Region


#Region "ReportGateinUpdate"
    Public Sub ReportGateinUpdate(ByVal bv_strGatein As String, ByVal bv_strGateTransNo As String)

        Dim objtrans As New Transactions()
        Try
            Dim objEDIs As New EDIs
            Dim strGatein As String = Nothing
            Dim strGateTransNo As String = Nothing
            If bv_strGatein <> "" Then
                strGatein = bv_strGatein.Substring(0, bv_strGatein.Length - 1)
                For Each strGI_SNO As String In strGatein.Split(",")
                    If Not strGI_SNO.Trim = "" Then
                        objEDIs.DeleteGateinRet(CommonUIs.iLng(strGI_SNO), objtrans)
                    End If
                Next
            End If

            If bv_strGateTransNo <> "" Then
                strGateTransNo = bv_strGateTransNo.Substring(0, bv_strGateTransNo.Length - 1)
                For Each strGI_TNO As String In strGateTransNo.Split(",")
                    If Not strGI_TNO.Trim = "" Then
                        objEDIs.UpdateTracking("P", Now, strGI_TNO.Trim(), objtrans)
                    End If
                Next
            End If

            objtrans.commit()

        Catch ex As Exception
            objtrans.RollBack()
            Throw ex
        End Try
    End Sub
#End Region

#Region "ReportGateoutUpdate"
    Public Sub ReportGateoutUpdate(ByVal bv_strGateout As String, ByVal bv_strGateTransNo As String)
        Dim objtrans As New Transactions()
        Try

            Dim objEDIs As New EDIs
            Dim strGateTransNo As String = Nothing
            Dim strGateout As String
            If bv_strGateout <> "" Then
                strGateout = bv_strGateout.Substring(0, bv_strGateout.Length - 1)
                For Each strGO_SNO As String In strGateout.Split(",")
                    If Not strGO_SNO.Trim = "" Then
                        objEDIs.DeleteGateoutRet(CommonUIs.iLng(strGO_SNO), objtrans)
                    End If
                Next
            End If

            If bv_strGateTransNo <> "" Then
                strGateTransNo = bv_strGateTransNo.Substring(0, bv_strGateTransNo.Length - 1)
                For Each strGI_TNO As String In strGateTransNo.Split(",")
                    If Not strGI_TNO.Trim = "" Then
                        objEDIs.UpdateTracking("P", Now, strGI_TNO.Trim(), objtrans)
                    End If
                Next
            End If

            objtrans.commit()

        Catch ex As Exception
            objtrans.RollBack()
            Throw ex
        End Try
    End Sub
#End Region

End Class
