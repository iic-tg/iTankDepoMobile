Option Strict On
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.ServiceModel
<ServiceContract()> _
Public Class IndexPattern

#Region "pub_GetIndexPatternDetailByIndexPatternId"
    <OperationContract()> _
    Public Function pub_GetIndexPatternDetailByIndexPatternId(ByVal bv_i32IndexPatternId As Int32, ByVal bv_i32DepotId As Int32) As IndexPatternDataSet

        Try
            Dim dsIndexPattern As IndexPatternDataSet
            Dim objIndexPatterns As New IndexPatterns
            dsIndexPattern = objIndexPatterns.GetIndexPatternDetailByIndexPatternID(bv_i32IndexPatternId)
            Return dsIndexPattern
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
#Region "pub_GetScreenCountByScreenId"
    Public Function pub_GetScreenCountByScreenId(ByVal bv_i32ScreenNameId As Int32) As Int32
        Try
            Dim dsIndexPattern As IndexPatternDataSet
            Dim objIndexPatterns As New IndexPatterns
            dsIndexPattern = objIndexPatterns.GetSreenCountByScreenId(bv_i32ScreenNameId)
            Return dsIndexPattern.Tables(IndexPatternData._INDEX_PATTERN).Rows.Count
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
#Region "pub_CreateIndexPattern"
    <OperationContract()> _
    Public Function pub_CreateIndexPattern(ByVal bv_i32ScreenNameId As Int32, _
                                        ByVal bv_strTableName As String, _
                                       ByVal bv_strSequenceNoStart As String, _
                                       ByVal bv_i16NoOfDigits As Int16, _
                                       ByVal bv_strIndexPatternActual As String, _
                                       ByVal bv_strIndexPattern As String, _
                                       ByVal bv_i32ResetBasisId As Int32, _
                                       ByVal bv_strSplitChar As String, _
                                       ByVal bv_i32IndexBasisId As Int32, _
                                       ByVal bv_blnActive As Boolean, _
                                       ByVal bv_strCreatedBy As String, _
                                       ByVal bv_datCreatedDate As DateTime, _
                                       ByVal bv_i32DepotId As Int32, _
                                       ByVal bv_dsIndexPattern As IndexPatternDataSet) As Int32
        Dim objTrans As New Transactions
        Dim i32IndexPatternId As Int32 = 0
        Try
            Dim objIndexPatterns As New IndexPatterns
            i32IndexPatternId = objIndexPatterns.CreateIndexPattern(bv_i32ScreenNameId, _
                                                                        bv_strSequenceNoStart, _
                                                                        bv_i16NoOfDigits, _
                                                                        bv_strIndexPatternActual, _
                                                                        bv_strIndexPattern, _
                                                                        bv_i32ResetBasisId, _
                                                                        bv_strSplitChar, _
                                                                        bv_i32IndexBasisId, _
                                                                        bv_blnActive, _
                                                                        bv_strCreatedBy, _
                                                                        bv_datCreatedDate, _
                                                                        bv_strCreatedBy, _
                                                                        bv_datCreatedDate, _
                                                                        bv_i32DepotId, _
                                                                        objTrans)

            'Inserting Index Pattern Details
            If bv_dsIndexPattern.Tables(IndexPatternData._V_INDEX_PATTERN_DETAIL).Rows.Count > 0 Then
                For Each drIndexPattern As DataRow In bv_dsIndexPattern.Tables(IndexPatternData._V_INDEX_PATTERN_DETAIL).Rows
                    If drIndexPattern.RowState <> DataRowState.Deleted Then
                        Dim i32ParameterId As Int32 = 0
                        Dim i32StartChar As Int32 = 0
                        Dim i32EndChar As Int32 = 0
                        Dim strDefaultVaue As String = ""
                        If Not IsDBNull(drIndexPattern(IndexPatternData.PRMTR_ID)) Then
                            i32ParameterId = CInt(drIndexPattern(IndexPatternData.PRMTR_ID))
                        End If
                        If Not IsDBNull(drIndexPattern(IndexPatternData.STRT_CHR)) Then
                            i32StartChar = CInt(drIndexPattern(IndexPatternData.STRT_CHR))
                        End If
                        If Not IsDBNull(drIndexPattern(IndexPatternData.END_CHR)) Then
                            i32EndChar = CInt(drIndexPattern(IndexPatternData.END_CHR))
                        End If
                        If Not IsDBNull(drIndexPattern(IndexPatternData.DFLT_VL)) Then
                            strDefaultVaue = CStr(drIndexPattern(IndexPatternData.DFLT_VL))
                        End If
                        objIndexPatterns.CreateIndexPatternDetail(i32IndexPatternId, i32ParameterId, i32StartChar, i32EndChar, strDefaultVaue, objTrans)
                        drIndexPattern.Item(IndexPatternData.INDX_PTTRN_ID) = i32IndexPatternId

                    End If
                Next
            End If
            'objIndexPatterns.CreateMaxReferenceNo(bv_strTableName, bv_strIndexPattern, bv_strSequenceNoStart, objTrans)
            objTrans.commit()
            Return i32IndexPatternId
        Catch ex As Exception
            objTrans.RollBack()
            Return Nothing
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateIndexPattern"
    <OperationContract()> _
    Public Function pub_UpdateIndexPattern(ByRef br_i32IndexPatternId As Int32, _
                                           ByVal bv_i32ScreenNameId As Int32, _
                                           ByVal bv_strTableName As String, _
                                           ByVal bv_strSequenceNoStart As String, _
                                           ByVal bv_i16NoOfDigits As Int16, _
                                           ByVal bv_strIndexPatternActual As String, _
                                           ByVal bv_strIndexPattern As String, _
                                           ByVal bv_i32ResetBasisId As Int32, _
                                           ByVal bv_strSplitChar As String, _
                                           ByVal bv_i32IndexBasisId As Int32, _
                                           ByVal bv_blnActive As Boolean, _
                                           ByVal bv_strModifiedBy As String, _
                                           ByVal bv_datModifiedDate As DateTime, _
                                           ByVal bv_i32DepotId As Int32, _
                                           ByVal bv_dsIndexPattern As IndexPatternDataSet) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objIndexPatterns As New IndexPatterns
            objIndexPatterns.UpdateIndexPattern(br_i32IndexPatternId, _
                                                bv_i32ScreenNameId, _
                                                bv_strSequenceNoStart, _
                                                bv_i16NoOfDigits, _
                                                bv_strIndexPatternActual, _
                                                bv_strIndexPattern, _
                                                bv_i32ResetBasisId, _
                                                bv_strSplitChar, _
                                                bv_i32IndexBasisId, _
                                                bv_blnActive, _
                                                bv_strModifiedBy, _
                                                bv_datModifiedDate, _
                                                bv_i32DepotId, _
                                                objTrans)

            'Updating Index Pattern Details
            If bv_dsIndexPattern.Tables(IndexPatternData._V_INDEX_PATTERN_DETAIL).Rows.Count > 0 Then
                For Each drIndexPattern As DataRow In bv_dsIndexPattern.Tables(IndexPatternData._V_INDEX_PATTERN_DETAIL).Rows
                    If drIndexPattern.RowState <> DataRowState.Deleted Then
                        Dim i32IndexPatternDetailId = 0
                        Dim i32ParameterId As Int32 = 0
                        Dim i32StartChar As Int32 = 0
                        Dim i32EndChar As Int32 = 0
                        Dim strDefaultVaue As String = ""
                        If Not IsDBNull(drIndexPattern(IndexPatternData.PRMTR_ID)) Then
                            i32ParameterId = CInt(drIndexPattern(IndexPatternData.PRMTR_ID))
                        End If
                        If Not IsDBNull(drIndexPattern(IndexPatternData.STRT_CHR)) Then
                            i32StartChar = CInt(drIndexPattern(IndexPatternData.STRT_CHR))
                        End If
                        If Not IsDBNull(drIndexPattern(IndexPatternData.END_CHR)) Then
                            i32EndChar = CInt(drIndexPattern(IndexPatternData.END_CHR))
                        End If
                        If Not IsDBNull(drIndexPattern(IndexPatternData.DFLT_VL)) Then
                            strDefaultVaue = CStr(drIndexPattern(IndexPatternData.DFLT_VL))
                        End If
                        If Not IsDBNull(drIndexPattern(IndexPatternData.INDX_PTTRN_DTL_ID)) Then
                            i32IndexPatternDetailId = CInt(drIndexPattern(IndexPatternData.INDX_PTTRN_DTL_ID))
                        End If
                        If drIndexPattern.RowState = DataRowState.Added Then
                            objIndexPatterns.CreateIndexPatternDetail(br_i32IndexPatternId, i32ParameterId, i32StartChar, i32EndChar, strDefaultVaue, objTrans)
                            drIndexPattern.Item(IndexPatternData.INDX_PTTRN_ID) = br_i32IndexPatternId
                        ElseIf drIndexPattern.RowState = DataRowState.Modified Then
                            objIndexPatterns.UpdateIndexPatternDetail(i32IndexPatternDetailId, br_i32IndexPatternId, i32ParameterId, i32StartChar, i32EndChar, strDefaultVaue, objTrans)
                            drIndexPattern.Item(IndexPatternData.INDX_PTTRN_ID) = br_i32IndexPatternId
                        End If
                    End If
                Next
            End If
            'objIndexPatterns.UpdateMaxReferenceNo(bv_strTableName, bv_strIndexPattern, objTrans)
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
            Return False
        End Try
    End Function
#End Region
End Class
