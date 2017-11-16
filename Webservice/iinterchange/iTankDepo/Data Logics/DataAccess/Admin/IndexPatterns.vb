#Region " IndexPatterns.vb"
'*********************************************************************************************************************
'Name :
'      IndexPatterns.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(IndexPatterns.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      8/18/2014 6:21:27 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.Text

#Region "IndexPatterns"

Public Class IndexPatterns

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private ds As IndexPatternDataSet
    Private Const IndexPatternInsertQuery As String = "INSERT INTO INDEX_PATTERN(INDX_PTTRN_ID,SCRN_ID,SQNC_NO_STRT,NO_OF_DGT,INDX_PTTRN_FRMT,INDX_PTTRN_ACTL_FRMT,RST_BSS_ID,SPLT_CHR,INDX_BSS_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EDT_BT) VALUES (@INDX_PTTRN_ID,@SCRN_ID,@SQNC_NO_STRT,@NO_OF_DGT,@INDX_PTTRN_FRMT,@INDX_PTTRN_ACTL_FRMT,@RST_BSS_ID,@SPLT_CHR,@INDX_BSS_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID,@EDT_BT)"
    Private Const GetIndexPatternDetailSelectQuery As String = "SELECT INDX_PTTRN_DTL_ID,INDX_PTTRN_ID,PRMTR_ID,PRMTR_NM,STRT_CHR,END_CHR,DFLT_VL,FLD_NM FROM V_INDEX_PATTERN_DETAIL WHERE INDX_PTTRN_ID=@INDX_PTTRN_ID ORDER BY INDX_PTTRN_ID"
    Private Const IndexPatternUpdateQuery As String = "UPDATE INDEX_PATTERN SET SCRN_ID=@SCRN_ID,SQNC_NO_STRT=@SQNC_NO_STRT,NO_OF_DGT=@NO_OF_DGT,INDX_PTTRN_FRMT=@INDX_PTTRN_FRMT,INDX_PTTRN_ACTL_FRMT=@INDX_PTTRN_ACTL_FRMT,RST_BSS_ID=@RST_BSS_ID,SPLT_CHR=@SPLT_CHR,INDX_BSS_ID=@INDX_BSS_ID,MDFD_BY=@MDFD_BY,MDFD_DT=@MDFD_DT,ACTV_BT=@ACTV_BT,DPT_ID=@DPT_ID WHERE INDX_PTTRN_ID=@INDX_PTTRN_ID"
    Private Const IndexPatternDetailInsertQuery As String = "INSERT INTO INDEX_PATTERN_DETAIL(INDX_PTTRN_DTL_ID,INDX_PTTRN_ID,PRMTR_ID,STRT_CHR,END_CHR,DFLT_VL) VALUES(@INDX_PTTRN_DTL_ID,@INDX_PTTRN_ID,@PRMTR_ID,@STRT_CHR,@END_CHR,@DFLT_VL)"
    Private Const IndexPatternDetailUpdateQuery As String = "UPDATE INDEX_PATTERN_DETAIL SET PRMTR_ID=@PRMTR_ID, STRT_CHR=@STRT_CHR, END_CHR=@END_CHR, DFLT_VL=@DFLT_VL WHERE INDX_PTTRN_DTL_ID=@INDX_PTTRN_DTL_ID"
    Private Const MaxReferenceNoCreateQuery As String = "UPDATE MAX_SNO SET MX_RFRNC_NO=@MX_RFRNC_NO,MX_NO=@MX_NO WHERE TBL_NM=@TBL_NM"
    Private Const MaxReferenceNoUpdateQuery As String = "UPDATE MAX_SNO SET MX_RFRNC_NO=@MX_RFRNC_NO WHERE TBL_NM=@TBL_NM"
    Private Const GetScreenCountByScreenIdAelectQuery As String = "SELECT SCRN_ID FROM INDEX_PATTERN WHERE SCRN_ID=@SCRN_ID AND ACTV_BT <> 0"
    'Private Const IdentitySelectQry As String = "SELECT INDX_PTTRN_ID ,SCRN_ID ,INDX_PTTRN_ACTL_FRMT ,INDX_PTTRN_FRMT,SPLT_CHR,DPT_CD FROM V_INDEX_PATTERN WHERE TBL_NAM =@TBL_NAM"
    Private Const IdentitySelectQry = "SELECT MX_NO,MX_RFRNC_NO,TBL_NM FROM MAX_SNO WHERE TBL_NM = @TBL_NM"
    Private Const getIndexPatternBytableName As String = "SELECT INDX_PTTRN_ID,INDX_PTTRN_FRMT, SPLT_CHR,NO_OF_DGT, RST_BSS_ID, SQNC_NO_STRT, INDX_BSS_ID, INDX_BSS_NM FROM V_INDEX_PATTERN WHERE TBL_NAM=@TBL_NAM AND ACTV_BT=1"
    Private Const getIndexPatternDetailByPatternId As String = "SELECT STRT_CHR,END_CHR,FLD_NM FROM V_INDEX_PATTERN_DETAIL WHERE INDX_PTTRN_ID=@INDX_PTTRN_ID"
    Private Const getMaxSequenceNoByTableName As String = "SELECT SQNC_NO FROM MAX_SNO_DETAIL WHERE TBL_NM=@TBL_NM AND SQNC_CNDTN_VAL=@SQNC_CNDTN_VAL  "
    Private Const getMaxSequenceNoByMaxSNO As String = "SELECT MX_NO  FROM MAX_SNO WHERE TBL_NM=@TBL_NM"
    Private Const SequenceConditionValue As String = " AND SQNC_CNDTN_VAL=@SQNC_CNDTN_VAL"
    Private Const SequenceConditionNullValue As String = " AND SQNC_CNDTN_VAL IS NULL"
    Private Const MaxSNoDetailInsertQuery As String = "INSERT INTO MAX_SNO_DETAIL(MX_SN_DTL_ID,TBL_NM,SQNC_NO,SQNC_CNDTN_VAL,RST_CNDTN_VAL) VALUES (@MX_SN_DTL_ID,@TBL_NM,@SQNC_NO,@SQNC_CNDTN_VAL,@RST_CNDTN_VAL)"
    Private Const MaxSNoDetailUpdateQuery As String = "UPDATE MAX_SNO_DETAIL SET SQNC_NO=@SQNC_NO WHERE TBL_NM=@TBL_NM  AND SQNC_CNDTN_VAL=@SQNC_CNDTN_VAL"
    Private Const getIndexPatternByScreenName As String = "SELECT INDX_PTTRN_ID,INDX_PTTRN_FRMT, SPLT_CHR,NO_OF_DGT, RST_BSS_ID, SQNC_NO_STRT, INDX_BSS_ID, INDX_BSS_NM FROM V_INDEX_PATTERN WHERE SCRN_NM=@SCRN_NM AND ACTV_BT=1"

    Private dsCommon As CommonUIDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New IndexPatternDataSet
        dsCommon = New CommonUIDataSet
    End Sub

#End Region

#Region "GetIndexPatternDetailByIndexPatternID"

    Public Function GetIndexPatternDetailByIndexPatternID(ByVal bv_i32IndexPatternId As Int32) As IndexPatternDataSet
        Try
            objData = New DataObjects(GetIndexPatternDetailSelectQuery, IndexPatternData.INDX_PTTRN_ID, CStr(bv_i32IndexPatternId))
            objData.Fill(CType(ds, DataSet), IndexPatternData._V_INDEX_PATTERN_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetSreenCountByScreenId"
    Public Function GetSreenCountByScreenId(ByVal bv_i32ScreenNameId As Int32) As IndexPatternDataSet
        Try
            objData = New DataObjects(GetScreenCountByScreenIdAelectQuery, IndexPatternData.SCRN_ID, bv_i32ScreenNameId)
            objData.Fill(CType(ds, DataSet), IndexPatternData._INDEX_PATTERN)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateMaxSNo() Table Name : MAX_SNO"
    Public Function UpdateMaxReferenceNo(ByVal bv_strTableName As String, ByVal bv_strMaxReferenceNo As String, ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(IndexPatternData.MAX_SNO).NewRow()
            With dr
                .Item(IndexPatternData.MX_RFRNC_NO) = bv_strMaxReferenceNo
                .Item(IndexPatternData.TBL_NM) = bv_strTableName
            End With
            objData.UpdateRow(dr, MaxReferenceNoUpdateQuery, objTrans)
        Catch ex As Exception
            Throw ex
        End Try
        Return True
    End Function
#End Region

#Region "CreateMaxSNo() Table Name : MAX_SNO"
    Public Function CreateMaxReferenceNo(ByVal bv_strTableName As String, ByVal bv_strMaxReferenceNo As String, ByVal bv_strSequenceNoStart As String, ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(IndexPatternData.MAX_SNO).NewRow()
            With dr
                .Item(IndexPatternData.MX_RFRNC_NO) = bv_strMaxReferenceNo
                .Item(IndexPatternData.TBL_NM) = bv_strTableName
                .Item(IndexPatternData.MX_NO) = bv_strSequenceNoStart
            End With
            objData.UpdateRow(dr, MaxReferenceNoCreateQuery, objTrans)
        Catch ex As Exception
            Throw ex
        End Try
        Return True
    End Function
#End Region

#Region "CreateIndexPattern() Table Name: Index_Pattern"
    Public Function CreateIndexPattern(ByVal bv_i32ScreenNameId As Int32, _
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
                                       ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_i32DepotId As Int32, _
                                       ByRef objTrans As Transactions) As Int32
        Try
            Dim dr As DataRow
            Dim lngMax As Long
            objData = New DataObjects()
            dr = ds.Tables(IndexPatternData._INDEX_PATTERN).NewRow()
            With dr
                lngMax = CommonUIs.GetIdentityValue(IndexPatternData._INDEX_PATTERN, objTrans)
                .Item(IndexPatternData.INDX_PTTRN_ID) = lngMax
                .Item(IndexPatternData.SCRN_ID) = bv_i32ScreenNameId
                .Item(IndexPatternData.SQNC_NO_STRT) = bv_strSequenceNoStart
                .Item(IndexPatternData.NO_OF_DGT) = bv_i16NoOfDigits
                .Item(IndexPatternData.INDX_PTTRN_FRMT) = bv_strIndexPattern
                .Item(IndexPatternData.INDX_PTTRN_ACTL_FRMT) = bv_strIndexPatternActual
                .Item(IndexPatternData.RST_BSS_ID) = bv_i32ResetBasisId
                If bv_strSplitChar = Nothing Or bv_strSplitChar = "" Then
                    .Item(IndexPatternData.SPLT_CHR) = DBNull.Value
                Else
                    .Item(IndexPatternData.SPLT_CHR) = bv_strSplitChar
                End If
                If bv_i32IndexBasisId = Nothing Then
                    .Item(IndexPatternData.INDX_BSS_ID) = DBNull.Value
                Else
                    .Item(IndexPatternData.INDX_BSS_ID) = bv_i32IndexBasisId
                End If
                .Item(IndexPatternData.ACTV_BT) = bv_blnActive
                .Item(IndexPatternData.CRTD_BY) = bv_strCreatedBy
                .Item(IndexPatternData.CRTD_DT) = bv_datCreatedDate
                .Item(IndexPatternData.MDFD_BY) = bv_strModifiedBy
                .Item(IndexPatternData.MDFD_DT) = bv_datModifiedDate
                .Item(IndexPatternData.DPT_ID) = bv_i32DepotId
                .Item(IndexPatternData.EDT_BT) = 1
            End With
            objData.InsertRow(dr, IndexPatternInsertQuery, objTrans)

            dr = Nothing
            CreateIndexPattern = lngMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "UpdateIndexPattern() Table Name: Index_Pattern"
    Public Function UpdateIndexPattern(ByVal br_i32IndexPatternId As Int32, _
                                       ByVal bv_i32ScreenNameId As Int32, _
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
                                       ByVal bv_i32DepotID As Int32, _
                                       ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(IndexPatternData._INDEX_PATTERN).NewRow()
            With dr
                .Item(IndexPatternData.INDX_PTTRN_ID) = br_i32IndexPatternId
                .Item(IndexPatternData.SCRN_ID) = bv_i32ScreenNameId
                .Item(IndexPatternData.SQNC_NO_STRT) = bv_strSequenceNoStart
                .Item(IndexPatternData.NO_OF_DGT) = bv_i16NoOfDigits
                .Item(IndexPatternData.INDX_PTTRN_FRMT) = bv_strIndexPattern
                .Item(IndexPatternData.INDX_PTTRN_ACTL_FRMT) = bv_strIndexPatternActual
                .Item(IndexPatternData.RST_BSS_ID) = bv_i32ResetBasisId
                If bv_strSplitChar = Nothing Or bv_strSplitChar = "" Then
                    .Item(IndexPatternData.SPLT_CHR) = DBNull.Value
                Else
                    .Item(IndexPatternData.SPLT_CHR) = bv_strSplitChar
                End If
                If bv_i32IndexBasisId = Nothing Then
                    .Item(IndexPatternData.INDX_BSS_ID) = DBNull.Value
                Else
                    .Item(IndexPatternData.INDX_BSS_ID) = bv_i32IndexBasisId
                End If
                .Item(IndexPatternData.ACTV_BT) = bv_blnActive
                .Item(IndexPatternData.MDFD_BY) = bv_strModifiedBy
                .Item(IndexPatternData.MDFD_DT) = bv_datModifiedDate
                .Item(IndexPatternData.DPT_ID) = bv_i32DepotID

            End With
            objData.UpdateRow(dr, IndexPatternUpdateQuery, objTrans)
            dr = Nothing
            Return True
        Catch ex As Exception
            Return False
            Throw ex
        End Try

    End Function
#End Region

#Region "CreateIndexPatternDetail() Table Name: Index_Pattern_Details"
    Public Function CreateIndexPatternDetail(ByVal bv_i32IndexPatternId As Int32, _
                                            ByVal bv_i32ParameterId As Int32, _
                                            ByVal bv_i32StartChar As Int32, _
                                            ByVal bv_i32EndChar As Int32, _
                                            ByVal bv_strDefaultVaue As String, _
                                            ByRef objTrans As Transactions) As Int32

        Try
            Dim dr As DataRow
            Dim lngMax As Long
            objData = New DataObjects()
            dr = ds.Tables(IndexPatternData._INDEX_PATTERN_DETAIL).NewRow()
            With dr
                lngMax = CommonUIs.GetIdentityValue(IndexPatternData._INDEX_PATTERN_DETAIL, objTrans)
                .Item(IndexPatternData.INDX_PTTRN_DTL_ID) = lngMax
                .Item(IndexPatternData.INDX_PTTRN_ID) = bv_i32IndexPatternId
                .Item(IndexPatternData.PRMTR_ID) = bv_i32ParameterId
                If bv_i32StartChar = 0 Then
                    .Item(IndexPatternData.STRT_CHR) = DBNull.Value
                Else
                    .Item(IndexPatternData.STRT_CHR) = bv_i32StartChar
                End If
                If bv_i32EndChar = 0 Then
                    .Item(IndexPatternData.END_CHR) = DBNull.Value
                Else
                    .Item(IndexPatternData.END_CHR) = bv_i32EndChar
                End If
                .Item(IndexPatternData.DFLT_VL) = bv_strDefaultVaue
            End With
            objData.InsertRow(dr, IndexPatternDetailInsertQuery, objTrans)
            dr = Nothing
            CreateIndexPatternDetail = lngMax
        Catch ex As Exception
            Throw ex
        End Try


    End Function
#End Region

#Region "UpdateIndexPatternDetail() Table Name: Index_Pattern_Details"
    Public Function UpdateIndexPatternDetail(ByVal bv_i32IndexPatterDetailId As Int32, _
                                            ByVal bv_i32IndexPatternId As Int32, _
                                            ByVal bv_i32ParameterId As Int32, _
                                            ByVal bv_i32StartChar As Int32, _
                                            ByVal bv_i32EndChar As Int32, _
                                            ByVal bv_strDefaultVaue As String, _
                                            ByRef objTrans As Transactions) As Boolean

        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(IndexPatternData._INDEX_PATTERN_DETAIL).NewRow()
            With dr
                .Item(IndexPatternData.INDX_PTTRN_DTL_ID) = bv_i32IndexPatterDetailId
                .Item(IndexPatternData.INDX_PTTRN_ID) = bv_i32IndexPatternId
                .Item(IndexPatternData.PRMTR_ID) = bv_i32ParameterId
                If bv_i32StartChar = 0 Then
                    .Item(IndexPatternData.STRT_CHR) = DBNull.Value
                Else
                    .Item(IndexPatternData.STRT_CHR) = bv_i32StartChar
                End If
                If bv_i32EndChar = 0 Then
                    .Item(IndexPatternData.END_CHR) = DBNull.Value
                Else
                    .Item(IndexPatternData.END_CHR) = bv_i32EndChar
                End If
                .Item(IndexPatternData.DFLT_VL) = bv_strDefaultVaue
            End With
            UpdateIndexPatternDetail = objData.UpdateRow(dr, IndexPatternDetailUpdateQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

    'Implement sequence number for all activity
#Region "GetIdentityCode"
    Public Shared Function GetIdentityCode(ByVal bv_strTableName As String, _
                                              ByVal lngMaxNo As Int64, _
                                              ByVal dtActivityDate As DateTime, _
                                               ByVal bv_Activityno As Int64, _
                                              ByRef br_objTrans As Transactions) As String
        Try
            Dim objData As DataObjects
            Dim ds As New DataSet
            Dim sbrFrmt As New StringBuilder
            Dim strFormat As String = String.Empty
            Dim hshTable As New Hashtable
            ' hshTable.Add("@SCRN_ID", bv_Activityno)
            hshTable.Add("@TBL_NAM", bv_strTableName)
            objData = New DataObjects(IdentitySelectQry, hshTable)
            objData.Fill(CType(ds, DataSet), "V_INDEX_PATTERN", br_objTrans)
            If ds.Tables("V_INDEX_PATTERN").Rows.Count > 0 Then
                sbrFrmt.Append(ds.Tables("V_INDEX_PATTERN").Rows(0).Item("INDX_PTTRN_ACTL_FRMT"))
            End If
            sbrFrmt.Replace("MM", dtActivityDate.ToString("MM"))
            sbrFrmt.Replace("YYYY", dtActivityDate.Year())
            sbrFrmt.Replace("Sequence No", lngMaxNo)
            If sbrFrmt.ToString.Contains("Depot Code") Then
                sbrFrmt.Replace("Depot Code", ds.Tables("V_INDEX_PATTERN").Rows(0).Item("DPT_CD"))
            End If
            If sbrFrmt.ToString.Contains("/") Then
                Return sbrFrmt.Replace("/", "").ToString()
            ElseIf (sbrFrmt.ToString.Contains("\")) Then

                Return sbrFrmt.Replace("\", "").ToString()
            Else
                Return sbrFrmt.ToString()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "MaxReferenceNo() TABLE NAME : MAX_SNO"
    Public Function GetMaxReferenceNo(bv_strTableName As String, ByVal bv_dtActivityDate As DateTime, _
                                      ByRef br_objTrans As Transactions, Optional ByVal bv_drIndexPattern As DataRow = Nothing, _
                                      Optional ByVal bv_i32DepotId As Int32 = Nothing) As String
        Dim strIndexPattern As String = String.Empty
        Dim objCommen As New CommonUIs
        Dim strTable As String()
        'Dim strTableName As String
        Dim strScreenName As String = String.Empty
        Dim lngCstmerid As Integer = 0
        Dim strCustomercode As String = String.Empty
        Try
            Dim htParam As New Hashtable
            If Not (dsCommon.Tables(CommonUIData._INDEX_PATTERN) Is Nothing) Then
                dsCommon.Tables(CommonUIData._INDEX_PATTERN).Clear()
            End If

            If bv_strTableName.Contains(",") Then
                strTable = bv_strTableName.Split(",")
                bv_strTableName = strTable(0)
                strScreenName = strTable(1)
                If strTable.Count > 2 Then
                    lngCstmerid = strTable(2)
                End If
                htParam.Add(CommonUIData.SCRN_NM, strScreenName)
                objData = New DataObjects(getIndexPatternByScreenName, htParam)
            Else
                htParam.Add(CommonUIData.TBL_NAM, bv_strTableName)
                objData = New DataObjects(getIndexPatternBytableName, htParam)
            End If
            objData.Fill(CType(dsCommon, DataSet), CommonUIData._INDEX_PATTERN, br_objTrans)
            If dsCommon.Tables(CommonUIData._INDEX_PATTERN).Rows.Count > 0 AndAlso Not IsDBNull(dsCommon.Tables(CommonUIData._INDEX_PATTERN).Rows(0).Item(CommonUIData.INDX_PTTRN_FRMT)) Then
                With dsCommon.Tables(CommonUIData._INDEX_PATTERN).Rows(0)
                    strIndexPattern = CStr(.Item(CommonUIData.INDX_PTTRN_FRMT))
                    Dim strSplitChar As String = ""
                    If Not IsDBNull(.Item(CommonUIData.SPLT_CHR)) Then
                        strSplitChar = .Item(CommonUIData.SPLT_CHR)
                    Else
                        strSplitChar = "$"
                    End If
                    Dim strMaxRefSplit As String() = strIndexPattern.Split(CChar(strSplitChar))
                    If strMaxRefSplit.Length > 0 Then
                        Dim htParamPatternDetail As New Hashtable
                        htParamPatternDetail.Add(CommonUIData.INDX_PTTRN_ID, .Item(CommonUIData.INDX_PTTRN_ID))
                        objData = New DataObjects(getIndexPatternDetailByPatternId, htParamPatternDetail)
                        objData.Fill(CType(dsCommon, DataSet), CommonUIData._V_INDEX_PATTERN_DETAIL, br_objTrans)
                        '//
                        Dim i32ResetYear As Int32 = 0
                        If Not (strScreenName.ToLower.Contains("repair estimate")) AndAlso Not strMaxRefSplit.Contains("CSTMR_CD") Then
                            lngCstmerid = 0
                        End If
                        For i = 0 To strMaxRefSplit.Length - 1
                            Dim strYear As String = ""
                            Select Case strMaxRefSplit(i)
                                Case "DD"
                                    If i = 0 Then
                                        strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), bv_dtActivityDate.ToString("dd"))
                                    Else
                                        strIndexPattern = strIndexPattern.Replace(String.Concat(strSplitChar, strMaxRefSplit(i)), String.Concat(strSplitChar, bv_dtActivityDate.ToString("dd")))
                                    End If
                                Case "MM"
                                    If i = 0 Then
                                        strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), bv_dtActivityDate.ToString(strMaxRefSplit(i)))
                                    Else
                                        strIndexPattern = strIndexPattern.Replace(String.Concat(strSplitChar, strMaxRefSplit(i)), String.Concat(strSplitChar, bv_dtActivityDate.ToString(strMaxRefSplit(i))))
                                    End If
                                Case "MMM"
                                    If i = 0 Then
                                        strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), bv_dtActivityDate.ToString(strMaxRefSplit(i)).ToUpper())
                                    Else
                                        strIndexPattern = strIndexPattern.Replace(String.Concat(strSplitChar, strMaxRefSplit(i)), String.Concat(strSplitChar, bv_dtActivityDate.ToString(strMaxRefSplit(i)).ToUpper()))
                                    End If
                                Case "YY"
                                    If .Item(CommonUIData.RST_BSS_ID) = "119" Then 'Financial
                                        If bv_dtActivityDate.Month >= 4 Then
                                            strYear = bv_dtActivityDate.AddYears(1).ToString(strMaxRefSplit(i).ToLower)
                                            '//
                                            i32ResetYear = CInt(bv_dtActivityDate.AddYears(1).ToString("yyyy"))

                                        End If
                                    End If

                                    If strYear = "" Then
                                        strYear = bv_dtActivityDate.ToString(strMaxRefSplit(i).ToLower)
                                        '//
                                        i32ResetYear = CInt(bv_dtActivityDate.ToString("yyyy"))
                                    End If
                                    If i = 0 Then
                                        strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), strYear)
                                    Else
                                        strIndexPattern = strIndexPattern.Replace(String.Concat(strSplitChar, strMaxRefSplit(i)), String.Concat(strSplitChar, strYear))
                                    End If
                                Case "YYYY"
                                    If .Item(CommonUIData.RST_BSS_ID) = "119" Then 'Financial
                                        If bv_dtActivityDate.Month >= 4 Then
                                            strYear = bv_dtActivityDate.AddYears(1).ToString(strMaxRefSplit(i).ToLower)
                                            '//
                                            i32ResetYear = CInt(bv_dtActivityDate.AddYears(1).ToString("yyyy"))

                                        End If
                                    End If

                                    If strYear = "" Then
                                        strYear = bv_dtActivityDate.ToString(strMaxRefSplit(i).ToLower)
                                    End If
                                    If i = 0 Then
                                        strIndexPattern = strIndexPattern.Replace(String.Concat(strSplitChar, strMaxRefSplit(i)), String.Concat(strSplitChar, strYear))
                                    Else
                                        strIndexPattern = strIndexPattern.Replace(String.Concat(strSplitChar, strMaxRefSplit(i)), String.Concat(strSplitChar, strYear))
                                    End If
                                Case "MAXNO"
                                    Dim htParamMaxSNODetail As New Hashtable
                                    Dim strSequenceConditionValue As String = ""
                                    '--
                                    Dim resetConditionValue As Int32 = 0
                                    Dim lngMaxSNoDetailId As Long = 0
                                    Dim strAppendQuery As String = ""
                                    htParamMaxSNODetail.Add(CommonUIData.TBL_NM, bv_strTableName)
                                    If Not IsDBNull(.Item(CommonUIData.INDX_BSS_ID)) Then
                                        'htParamMaxSNODetail.Add(CommonUIData.SQNC_CNDTN_VAL, .Item(IndexPatternData.INDX_BSS_ID))
                                        ' strSequenceConditionValue = .Item(CommonUIData.INDX_BSS_ID)
                                        'strAppendQuery = SequenceConditionValue
                                        'From grand
                                        'htParamMaxSNODetail.Add(CommonUIData.SQNC_CNDTN_VAL, bv_drIndexPattern.Item(CStr(.Item(CommonUIData.INDX_BSS_FLD_NM))))
                                        'strSequenceConditionValue = bv_drIndexPattern.Item(CStr(.Item(CommonUIData.INDX_BSS_FLD_NM)))
                                        'strAppendQuery = SequenceConditionValue
                                        'coded
                                        If lngCstmerid <> 0 Then
                                            If strCustomercode = "" Then
                                                dsCommon = objCommen.pub_GetCustomerDetail(lngCstmerid)
                                                strCustomercode = dsCommon.Tables(CommonUIData._V_SERVICE_PARTNER).Rows(0).Item(CommonUIData.SRVC_PRTNR_CD)
                                            End If
                                            htParamMaxSNODetail.Add(CommonUIData.SQNC_CNDTN_VAL, strCustomercode)
                                        End If
                                        strSequenceConditionValue = strCustomercode
                                        strAppendQuery = SequenceConditionValue

                                    Else
                                        'strAppendQuery = SequenceConditionNullValue
                                    End If
                                    'check
                                    If Not IsDBNull(.Item(CommonUIData.RST_BSS_ID)) Then
                                        If i32ResetYear = 0 Then
                                            i32ResetYear = DateTime.Now.Year
                                        End If
                                        htParamMaxSNODetail.Add(CommonUIData.RST_CNDTN_VAL, i32ResetYear)
                                        resetConditionValue = i32ResetYear
                                        strAppendQuery = String.Concat(strAppendQuery, " AND RST_CNDTN_VAL=@RST_CNDTN_VAL")
                                    Else
                                        strAppendQuery = String.Concat(strAppendQuery, " AND RST_CNDTN_VAL IS NULL")
                                    End If

                                    'Check

                                    dsCommon.Tables(CommonUIData._MAX_SNO).Clear()

                                    'objData = New DataObjects(String.Concat(getMaxSequenceNoByTableName, strAppendQuery), htParamMaxSNODetail)
                                    '  objData.Fill(CType(dsCommon, DataSet), CommonUIData._MAX_SNO, br_objTrans)
                                    'objData.Fill(CType(dsCommon, DataSet), CommonUIData._MAX_SNO_DETAIL, br_objTrans)
                                    objData = New DataObjects(String.Concat(getMaxSequenceNoByMaxSNO), htParamMaxSNODetail)
                                    objData.Fill(CType(dsCommon, DataSet), CommonUIData._MAX_SNO, br_objTrans)
                                    Dim intMaxNo As Int32 = 0
                                    If lngCstmerid <> 0 And Not .Item(CommonUIData.INDX_PTTRN_FRMT).ToString.Contains("DPTCD") Then
                                        If Not htParamMaxSNODetail.ContainsKey(CommonUIData.SQNC_CNDTN_VAL) Then
                                            If strCustomercode = "" Then
                                                dsCommon = objCommen.pub_GetCustomerDetail(lngCstmerid)
                                                strCustomercode = dsCommon.Tables(CommonUIData._V_SERVICE_PARTNER).Rows(0).Item(CommonUIData.SRVC_PRTNR_CD)
                                            End If
                                            strSequenceConditionValue = strCustomercode
                                            htParamMaxSNODetail.Add(CommonUIData.SQNC_CNDTN_VAL, strCustomercode)
                                        End If
                                        objData = New DataObjects(String.Concat(getMaxSequenceNoByTableName, strAppendQuery), htParamMaxSNODetail)
                                        objData.Fill(CType(dsCommon, DataSet), CommonUIData._MAX_SNO_DETAIL, br_objTrans)
                                        If dsCommon.Tables(CommonUIData._MAX_SNO_DETAIL).Rows.Count > 0 Then
                                            ' Update Max Sno Detail
                                            If Now.Month = 1 And Now.Day = 1 Then
                                                strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), FormatSequenceNo(1, .Item(CommonUIData.NO_OF_DGT)))
                                                intMaxNo = 1
                                            Else
                                                strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), FormatSequenceNo(dsCommon.Tables(CommonUIData._MAX_SNO_DETAIL).Rows(0).Item(CommonUIData.SQNC_NO), .Item(CommonUIData.NO_OF_DGT)))
                                                intMaxNo = dsCommon.Tables(CommonUIData._MAX_SNO_DETAIL).Rows(0).Item(CommonUIData.SQNC_NO)
                                            End If

                                        Else
                                            ' Insert Max Sno Detail
                                            lngMaxSNoDetailId = CreateMaxSNoDetail(bv_strTableName, .Item(CommonUIData.SQNC_NO_STRT), strSequenceConditionValue, resetConditionValue, br_objTrans)
                                            If lngMaxSNoDetailId <> 0 Then
                                                strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), FormatSequenceNo(.Item(CommonUIData.SQNC_NO_STRT), .Item(CommonUIData.NO_OF_DGT)))
                                                intMaxNo = .Item(CommonUIData.SQNC_NO_STRT)
                                            End If

                                        End If
                                        UpdateMaxSNoDetailSequenceNo(bv_strTableName, intMaxNo, strSequenceConditionValue, resetConditionValue, br_objTrans)
                                    ElseIf .Item(CommonUIData.INDX_PTTRN_FRMT).ToString.Contains("DPTCD") Then  'Max Sno To be get based on Depot Code
                                        Dim htParamMaxSNODepotDetail As New Hashtable
                                        Dim strResetConditionVal As String = getMaxSequenceNoByTableName
                                        Dim strDepotCode As String = objCommen.pub_GetDepotDetail(bv_i32DepotId).Tables(CommonUIData._DEPOT).Rows(0).Item(CommonUIData.DPT_CD)
                                        htParamMaxSNODepotDetail.Add(IndexPatternData.TBL_NM, bv_strTableName)
                                        htParamMaxSNODepotDetail.Add(CommonUIData.SQNC_CNDTN_VAL, strDepotCode)
                                        If Not IsDBNull(.Item(CommonUIData.RST_BSS_ID)) Then
                                            If i32ResetYear = 0 Then
                                                i32ResetYear = DateTime.Now.Year
                                            End If
                                            htParamMaxSNODepotDetail.Add(CommonUIData.RST_CNDTN_VAL, i32ResetYear)
                                            resetConditionValue = i32ResetYear
                                            strResetConditionVal = String.Concat(strResetConditionVal, " AND RST_CNDTN_VAL=@RST_CNDTN_VAL")
                                        Else
                                            strResetConditionVal = String.Concat(strResetConditionVal, " AND RST_CNDTN_VAL IS NULL")
                                        End If
                                        objData = New DataObjects(String.Concat(strResetConditionVal), htParamMaxSNODepotDetail)
                                        objData.Fill(CType(dsCommon, DataSet), CommonUIData._MAX_SNO_DETAIL, br_objTrans)
                                        If dsCommon.Tables(CommonUIData._MAX_SNO_DETAIL).Rows.Count > 0 Then
                                            ' Update Max Sno Detail
                                            If Now.Month = 1 And Now.Day = 1 Then
                                                strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), FormatSequenceNo(1, .Item(CommonUIData.NO_OF_DGT)))
                                                intMaxNo = 1
                                            Else
                                                strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), FormatSequenceNo(dsCommon.Tables(CommonUIData._MAX_SNO_DETAIL).Rows(0).Item(CommonUIData.SQNC_NO), .Item(CommonUIData.NO_OF_DGT)))
                                                intMaxNo = dsCommon.Tables(CommonUIData._MAX_SNO_DETAIL).Rows(0).Item(CommonUIData.SQNC_NO)
                                            End If
                                        Else
                                            ' Insert Max Sno Detail
                                            lngMaxSNoDetailId = CreateMaxSNoDetail(bv_strTableName, .Item(CommonUIData.SQNC_NO_STRT), strDepotCode, resetConditionValue, br_objTrans)
                                            If lngMaxSNoDetailId <> 0 Then
                                                strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), FormatSequenceNo(.Item(CommonUIData.SQNC_NO_STRT), .Item(CommonUIData.NO_OF_DGT)))
                                                intMaxNo = .Item(CommonUIData.SQNC_NO_STRT)
                                            End If

                                        End If
                                        UpdateMaxSNoDetailSequenceNo(bv_strTableName, intMaxNo, strDepotCode, resetConditionValue, br_objTrans)
                                    Else 'as uscal
                                        If dsCommon.Tables(CommonUIData._MAX_SNO).Rows.Count > 0 Then
                                            ' Update Max Sno Detail
                                            'strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), FormatSequenceNo(dsCommon.Tables(CommonUIData._MAX_SNO_DETAIL).Rows(0).Item(CommonUIData.SQNC_NO), .Item(CommonUIData.NO_OF_DGT)))
                                            strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), FormatSequenceNo(dsCommon.Tables(CommonUIData._MAX_SNO).Rows(0).Item(CommonUIData.MX_NO), .Item(CommonUIData.NO_OF_DGT)))
                                            intMaxNo = dsCommon.Tables(CommonUIData._MAX_SNO).Rows(0).Item(CommonUIData.MX_NO)
                                        Else
                                            ' Insert Max Sno Detail
                                            lngMaxSNoDetailId = CreateMaxSNoDetail(bv_strTableName, .Item(CommonUIData.SQNC_NO_STRT), strSequenceConditionValue, resetConditionValue, br_objTrans)
                                            If lngMaxSNoDetailId <> 0 Then
                                                strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), FormatSequenceNo(.Item(CommonUIData.SQNC_NO_STRT), .Item(CommonUIData.NO_OF_DGT)))
                                                intMaxNo = .Item(CommonUIData.SQNC_NO_STRT)
                                            End If

                                        End If
                                    End If


                                Case "DPTCD"
                                    dsCommon = objCommen.pub_GetDepotDetail(bv_i32DepotId)
                                    strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), dsCommon.Tables(CommonUIData._DEPOT).Rows(0).Item(CommonUIData.DPT_CD))

                                Case "CSTMR_CD"
                                    dsCommon = objCommen.pub_GetCustomerDetail(lngCstmerid)
                                    strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), dsCommon.Tables(CommonUIData._V_SERVICE_PARTNER).Rows(0).Item(CommonUIData.SRVC_PRTNR_CD))
                                    strCustomercode = dsCommon.Tables(CommonUIData._V_SERVICE_PARTNER).Rows(0).Item(CommonUIData.SRVC_PRTNR_CD)
                                Case Else
                                    If Not bv_drIndexPattern Is Nothing Then
                                        If bv_drIndexPattern.Table.Columns.Contains(strMaxRefSplit(i)) = True Then
                                            Dim strPortCode As String = CStr(bv_drIndexPattern.Item(strMaxRefSplit(i)))
                                            If dsCommon.Tables(CommonUIData._V_INDEX_PATTERN_DETAIL).Rows.Count > 0 Then
                                                Dim drVIndexPatternDetail() As DataRow = dsCommon.Tables(CommonUIData._V_INDEX_PATTERN_DETAIL).Select("FLD_NM='" & strMaxRefSplit(i) & "'")
                                                If drVIndexPatternDetail.Length > 0 Then
                                                    Dim i32CharStartIndex As Int32 = 0
                                                    Dim i32CharEndLength As Int32 = 0
                                                    If Not IsDBNull(drVIndexPatternDetail(0).Item(CommonUIData.STRT_CHR)) Then
                                                        i32CharStartIndex = CInt(drVIndexPatternDetail(0).Item(CommonUIData.STRT_CHR))
                                                    End If
                                                    If Not IsDBNull(drVIndexPatternDetail(0).Item(CommonUIData.END_CHR)) Then
                                                        i32CharEndLength = CInt(drVIndexPatternDetail(0).Item(CommonUIData.END_CHR))
                                                    End If
                                                    If i32CharStartIndex <> 0 And i32CharEndLength <> 0 Then
                                                        i32CharStartIndex = i32CharStartIndex - 1
                                                        i32CharEndLength = i32CharEndLength
                                                        If strPortCode.Length > i32CharStartIndex And strPortCode.Length > (i32CharStartIndex + i32CharEndLength) Then
                                                            strPortCode = strPortCode.Substring(i32CharStartIndex, i32CharEndLength)
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            strIndexPattern = strIndexPattern.Replace(strMaxRefSplit(i), strPortCode)
                                            Continue For
                                        End If
                                    End If
                            End Select
                        Next

                        strIndexPattern = strIndexPattern.Replace("$", "")
                    End If
                End With
            Else

                strIndexPattern = CStr(GetIdentityValueWithOutUpdate(bv_strTableName))


            End If
            Return strIndexPattern
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateMaxSNoDetail Table Name : MAX_SNO_DETAIL"
    Public Function CreateMaxSNoDetail(bv_strTableName As String, bv_strSequenceNo As String, bv_strSequenceConditionValue As String, ByVal bv_resetCondition As String, ByRef br_objTrans As Transactions) As Long
        Try
            Dim dsCommonUI As New CommonUIDataSet
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = dsCommonUI.Tables(CommonUIData._MAX_SNO_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CommonUIData._MAX_SNO_DETAIL, br_objTrans)
                .Item(CommonUIData.MX_SN_DTL_ID) = intMax
                .Item(CommonUIData.TBL_NM) = bv_strTableName
                .Item(CommonUIData.SQNC_NO) = bv_strSequenceNo
                If bv_strSequenceConditionValue = "" Then
                    .Item(CommonUIData.SQNC_CNDTN_VAL) = DBNull.Value
                Else
                    .Item(CommonUIData.SQNC_CNDTN_VAL) = bv_strSequenceConditionValue
                End If
                .Item(CommonUIData.RST_CNDTN_VAL) = bv_resetCondition
            End With
            objData.InsertRow(dr, MaxSNoDetailInsertQuery, br_objTrans)
            dr = Nothing
            CreateMaxSNoDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "UpdateMaxSNoDetailSequenceNo"
    Public Function UpdateMaxSNoDetailSequenceNo(bv_strTableName As String, bv_strSequenceNo As String, bv_strSequenceConditionValue As String, ByVal strresetConditionValue As String, ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dsCommonUI As New CommonUIDataSet
            Dim dr As DataRow
            objData = New DataObjects()
            dr = dsCommonUI.Tables(CommonUIData._MAX_SNO_DETAIL).NewRow()
            Dim strAppendQuery As String = ""
            Dim strNewSequenceNo As String = ""
            If Now.Month = 1 And Now.Day = 1 Then
                strNewSequenceNo = 1
            Else
                strNewSequenceNo = bv_strSequenceNo + 1
            End If
            With dr
                .Item(CommonUIData.TBL_NM) = bv_strTableName
                .Item(CommonUIData.SQNC_NO) = strNewSequenceNo
                .Item(CommonUIData.SQNC_CNDTN_VAL) = strresetConditionValue
                If bv_strSequenceConditionValue = "" Then
                    strAppendQuery = SequenceConditionNullValue
                Else
                    .Item(CommonUIData.SQNC_CNDTN_VAL) = bv_strSequenceConditionValue
                    strAppendQuery = SequenceConditionValue
                End If
            End With
            objData.UpdateRow(dr, String.Concat(MaxSNoDetailUpdateQuery, strAppendQuery), br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
        Return True
    End Function
#End Region

    Public Shared Function GetIdentityValueWithOutUpdate(ByVal bv_strTableName As String) As Long
        Try
            Dim objData As DataObjects
            Dim intIdentityValue As Int32
            Dim hshTable As New Hashtable
            hshTable.Add("@TBL_NM", bv_strTableName)
            objData = New DataObjects(IdentitySelectQry, hshTable)
            intIdentityValue = objData.ExecuteScalar()
            Return intIdentityValue
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    
#Region "FormatSequenceNo"
    Private Shared Function FormatSequenceNo(ByVal bv_i64IdentityValue As Int64, ByVal bv_i32ValueLength As Int32) As String
        Try
            Dim strZero As String = String.Empty
            If bv_i64IdentityValue <= 9 Then
                strZero = FormatZeroForSequenceNo(bv_i32ValueLength, 1)
            ElseIf bv_i64IdentityValue <= 99 Then
                strZero = FormatZeroForSequenceNo(bv_i32ValueLength, 2)
            ElseIf bv_i64IdentityValue <= 999 Then
                strZero = FormatZeroForSequenceNo(bv_i32ValueLength, 3)
            ElseIf bv_i64IdentityValue <= 9999 Then
                strZero = FormatZeroForSequenceNo(bv_i32ValueLength, 4)
            ElseIf bv_i64IdentityValue <= 99999 Then
                strZero = FormatZeroForSequenceNo(bv_i32ValueLength, 5)
            ElseIf bv_i64IdentityValue <= 999999 Then
                strZero = FormatZeroForSequenceNo(bv_i32ValueLength, 6)
            End If
            Return String.Concat(strZero, bv_i64IdentityValue)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "FormatZeroForSequenceNo"
    Private Shared Function FormatZeroForSequenceNo(ByVal bv_i32ValueLength As Int32, ByVal bv_i32Length As Int32) As String
        Try
            Dim i32Count As Int32
            Dim sbrZero As New StringBuilder
            For i32Count = 1 To bv_i32ValueLength - bv_i32Length
                sbrZero.Append("0")
            Next
            Return sbrZero.ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class

#End Region
