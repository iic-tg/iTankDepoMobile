#Region " AddDynamicReports.vb"
'*********************************************************************************************************************
'Name :
'      AddDynamicReports.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(AddDynamicReports.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      6/28/2013 10:02:10 AM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

#Region "AddDynamicReports"

Public Class AddDynamicReports

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const ActivitySelectQueryByProcessIDPageURL As String = "SELECT ACTVTY_ID,ACTVTY_NAM,PRCSS_ID,LST_QRY,LST_URL,LST_TTL,LST_CLCNT,MY_SBMTS_CLCNT,MY_SBMTS_QRY,PG_URL,PG_TTL,TBL_NAM,ORDR_NO,MNU_TXT,CRT_RGHT_BT,EDT_RGHT_BT,ACTV_BT,ACTVTY_RL,EXCPTN_BT,MSTR_ID_CSV,QCK_LNK_ID_CSV FROM ACTIVITY WHERE PRCSS_ID=@PRCSS_ID AND PG_URL=@PG_URL"
    Private Const ACTIVITYSelectQueryByActivityNameProcessID As String = "SELECT COUNT(ACTVTY_ID) FROM ACTIVITY WHERE ACTVTY_NAM=@ACTVTY_NAM AND PRCSS_ID=@PRCSS_ID"
    Private Const OrderNoSelectQuery As String = "SELECT COUNT(ACTVTY_ID) FROM ACTIVITY WHERE PRCSS_ID=@PRCSS_ID"
    Private Const ActivitySelectQuery As String = "SELECT COUNT(ACTVTY_ID) FROM ACTIVITY WHERE PRCSS_ID=@PRCSS_ID"
    Private Const REPORT_PARAMETERSelectQueryPk As String = "SELECT PRMTR_ID,RPRT_ID,PRMTR_NAM,PRMTR_DSPLY_NAM,PRMTR_TYP,PRMTR_QRY,PRMTR_OPT,PRMTR_RL,REQ_DPT_ID,PRMTR_ORDR_CLMN,PRMTR_ORDR_TYP,PRMTR_DRPDWN,PRMTR_DPNDNT FROM REPORT_PARAMETER WHERE PRMTR_ID=@PRMTR_ID"
    Private Const REPORT_PARAMETERSelectQuery As String = "SELECT PRMTR_ID,RPRT_ID,PRMTR_NAM,PRMTR_DSPLY_NAM,PRMTR_TYP,PRMTR_QRY,PRMTR_OPT,PRMTR_RL,REQ_DPT_ID,PRMTR_ORDR_CLMN,PRMTR_ORDR_TYP,PRMTR_DRPDWN,PRMTR_DPNDNT FROM REPORT_PARAMETER"
    'Valid
    Private Const ProcessInsertQuery As String = "INSERT INTO PROCESS(PRCSS_ID,PRCSS_NAM,PRNT_ID,PRCSS_ORDR,PRCSS_RL)VALUES(@PRCSS_ID,@PRCSS_NAM,@PRNT_ID,@PRCSS_ORDR,@PRCSS_RL)"
    Private Const PROCESSSelectQueryByProcessName As String = "SELECT PRCSS_ID FROM PROCESS WHERE PRCSS_NAM=@PRCSS_NAM"
    Private Const ProcessSelectQueryByParentID As String = "SELECT CASE WHEN MAX(PRCSS_ORDR) IS NULL THEN 0 ELSE MAX(PRCSS_ORDR) END  FROM PROCESS WHERE PRNT_ID=@PRNT_ID"
    Private Const ActivitySelectQueryByProcessID As String = "SELECT CASE WHEN MAX(ORDR_NO) IS NULL THEN 0 ELSE MAX(ORDR_NO) END FROM ACTIVITY WHERE PRCSS_ID=@PRCSS_ID"
    Private Const ActivityInsertQuery As String = "INSERT INTO ACTIVITY(ACTVTY_ID,ACTVTY_NAM,PRCSS_ID,LST_QRY,LST_URL,LST_TTL,LST_CLCNT,MY_SBMTS_CLCNT,MY_SBMTS_QRY,PG_URL,PG_TTL,TBL_NAM,ORDR_NO,MNU_TXT,CRT_RGHT_BT,EDT_RGHT_BT,ACTV_BT,ACTVTY_RL,EXCPTN_BT,MSTR_ID_CSV,QCK_LNK_ID_CSV,CNCL_RGHT_BT)VALUES(@ACTVTY_ID,@ACTVTY_NAM,@PRCSS_ID,@LST_QRY,@LST_URL,@LST_TTL,@LST_CLCNT,@MY_SBMTS_CLCNT,@MY_SBMTS_QRY,@PG_URL,@PG_TTL,@TBL_NAM,@ORDR_NO,@MNU_TXT,@CRT_RGHT_BT,@EDT_RGHT_BT,@ACTV_BT,@ACTVTY_RL,@EXCPTN_BT,@MSTR_ID_CSV,@QCK_LNK_ID_CSV,@CNCL_RGHT_BT)"
    Private Const ActivityUpdateQueryOrder As String = "UPDATE ACTIVITY SET PG_TTL=@PG_TTL,PRCSS_ID=@PRCSS_ID,ACTV_BT=@ACTV_BT,MNU_TXT=@MNU_TXT,ORDR_NO=@ORDR_NO WHERE ACTVTY_ID=@ACTVTY_ID"
    Private Const ActivityUpdateQuery As String = "UPDATE ACTIVITY SET PG_TTL=@PG_TTL,PRCSS_ID=@PRCSS_ID,ACTV_BT=@ACTV_BT,MNU_TXT=@MNU_TXT WHERE ACTVTY_ID=@ACTVTY_ID"
    Private Const ActivitySelectProcessIDActivityName As String = "SELECT Count(ACTVTY_ID) FROM ACTIVITY WHERE PRCSS_ID=@PRCSS_ID AND ACTVTY_NAM=@ACTVTY_NAM"
    Private Const ReportParameterInsertQuery As String = "INSERT INTO REPORT_PARAMETER(PRMTR_ID,RPRT_ID,PRMTR_NAM,PRMTR_DSPLY_NAM,PRMTR_TYP,PRMTR_QRY,PRMTR_OPT,PRMTR_RL,REQ_DPT_ID,PRMTR_ORDR_CLMN,PRMTR_ORDR_TYP,PRMTR_DRPDWN,PRMTR_DPNDNT)VALUES(@PRMTR_ID,@RPRT_ID,@PRMTR_NAM,@PRMTR_DSPLY_NAM,@PRMTR_TYP,@PRMTR_QRY,@PRMTR_OPT,@PRMTR_RL,@REQ_DPT_ID,@PRMTR_ORDR_CLMN,@PRMTR_ORDR_TYP,@PRMTR_DRPDWN,@PRMTR_DPNDNT)"
    Private Const ReportParameterSelectQueryByReportID As String = "SELECT PRMTR_ID,RPRT_ID,PRMTR_NAM,PRMTR_DSPLY_NAM,PRMTR_TYP,PRMTR_QRY,PRMTR_OPT,PRMTR_RL,REQ_DPT_ID,PRMTR_ORDR_CLMN,PRMTR_ORDR_TYP,PRMTR_DRPDWN,PRMTR_DPNDNT FROM REPORT_PARAMETER WHERE RPRT_ID=@RPRT_ID"
    Private Const ReportParameterSelectQueryByParameterID As String = "SELECT COUNT(PRMTR_ID) FROM REPORT_PARAMETER WHERE RPRT_ID=@RPRT_ID AND PRMTR_NAM=@PRMTR_NAM"
    Private Const ReportParameterUpdateQuery As String = "UPDATE REPORT_PARAMETER SET PRMTR_DSPLY_NAM=@PRMTR_DSPLY_NAM, PRMTR_OPT=@PRMTR_OPT, PRMTR_DRPDWN=@PRMTR_DRPDWN, PRMTR_DPNDNT=@PRMTR_DPNDNT WHERE PRMTR_ID=@PRMTR_ID"
    Private Const Report_ParameterDeleteQuery As String = "DELETE FROM Report_Parameter WHERE PRMTR_ID=@PRMTR_ID"
    Private ds As AddDynamicReportDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New AddDynamicReportDataSet
    End Sub

#End Region

#Region "GetActivityByProcessId() TABLE NAME:ACTIVITY"

    Public Function GetActivityByProcessId(ByVal bv_i32ProcessId As Int32, ByVal bv_strType As String) As AddDynamicReportDataSet
        Try
            Dim hashParameters As New Hashtable()
            hashParameters.Add(AddDynamicReportData.PRCSS_ID, bv_i32ProcessId)
            hashParameters.Add(AddDynamicReportData.PG_URL, bv_strType)
            objData = New DataObjects(ActivitySelectQueryByProcessIDPageURL, hashParameters)
            objData.Fill(CType(ds, DataSet), AddDynamicReportData._ACTIVITY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateActivity() TABLE NAME:Activity"

    Public Function CreateActivity(ByRef bv_intActivityID As Integer, _
                                   ByVal bv_strActivityName As String, _
                                   ByVal bv_i32ProcessID As Int32, _
                                   ByVal bv_strListURL As String, _
                                   ByVal bv_strListTitle As String, _
                                   ByVal bv_i32ListColumnCount As Int32, _
                                   ByVal bv_i32MySubmitColumnCount As Int32, _
                                   ByVal bv_strPageURL As String, _
                                   ByVal bv_strPageTitle As String, _
                                   ByVal bv_strTableName As String, _
                                   ByVal bv_blnCreateRightBit As Boolean, _
                                   ByVal bv_blnEditRightBit As Boolean, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                   ByVal bv_strActivityRole As String, _
                                   ByVal bv_blnExceptionBit As Boolean, _
                                   ByVal bv_strMasterIDCSV As String, _
                                   ByVal bv_strQuickLinkIDCSV As String, _
                                   ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(AddDynamicReportData._ACTIVITY).NewRow()
            With dr
                Dim strMenuText As String = String.Empty
                bv_intActivityID = CommonUIs.GetIdentityValue(AddDynamicReportData._ACTIVITY, br_objTrans)
                .Item(AddDynamicReportData.ACTVTY_ID) = bv_intActivityID
                .Item(AddDynamicReportData.ACTVTY_NAM) = bv_strActivityName
                .Item(AddDynamicReportData.PRCSS_ID) = bv_i32ProcessID
                strMenuText = String.Concat("text=", bv_strActivityName, ";url=", bv_strListURL, "?apu=", bv_strPageURL, "&activityid=", bv_intActivityID, "&activityname=", bv_strActivityName, "&type=Master&tablename=", bv_strTableName, "&listpanetitle=Reports >> ", bv_strActivityName, "&pagetitle=", bv_strPageTitle, "&pagetype=PostBack&mode=edit&qrytype=new;status=", bv_strActivityName)
                Dim strListQuery As String = String.Empty
                strListQuery = "SELECT (SELECT ACTVTY_NAM FROM ACTIVITY WHERE ACTVTY_ID=RH.ACTVTY_ID ) [Report Name],RH.RPRT_TTLE AS TITLE,RH.CRTD_BY AS CREATEDBY, RH.RPRT_HDR_ID,DPT_ID FROM REPORT_HEADER RH WHERE RH.ACTVTY_ID=" & bv_intActivityID
                If strListQuery <> Nothing Then
                    .Item(AddDynamicReportData.LST_QRY) = strListQuery
                Else
                    .Item(AddDynamicReportData.LST_QRY) = DBNull.Value
                End If
                If bv_strListURL <> Nothing Then
                    .Item(AddDynamicReportData.LST_URL) = bv_strListURL
                Else
                    .Item(AddDynamicReportData.LST_URL) = DBNull.Value
                End If
                If bv_strListTitle <> Nothing Then
                    .Item(AddDynamicReportData.LST_TTL) = bv_strListTitle
                Else
                    .Item(AddDynamicReportData.LST_TTL) = DBNull.Value
                End If
                If bv_i32ListColumnCount <> 0 Then
                    .Item(AddDynamicReportData.LST_CLCNT) = bv_i32ListColumnCount
                Else
                    .Item(AddDynamicReportData.LST_CLCNT) = DBNull.Value
                End If
                If bv_i32MySubmitColumnCount <> 0 Then
                    .Item(AddDynamicReportData.MY_SBMTS_CLCNT) = bv_i32MySubmitColumnCount
                Else
                    .Item(AddDynamicReportData.MY_SBMTS_CLCNT) = DBNull.Value
                End If
                .Item(AddDynamicReportData.PG_URL) = bv_strPageURL
                .Item(AddDynamicReportData.PG_TTL) = bv_strPageTitle
                If bv_strTableName <> Nothing Then
                    .Item(AddDynamicReportData.TBL_NAM) = bv_strTableName
                Else
                    .Item(AddDynamicReportData.TBL_NAM) = DBNull.Value
                End If
                Dim i32OrderNo As Integer = GetActivityOrderbyProcessId(bv_i32ProcessID, br_objTrans)
                If i32OrderNo = 0 Then
                    .Item(AddDynamicReportData.ORDR_NO) = 1
                Else
                    .Item(AddDynamicReportData.ORDR_NO) = DBNull.Value
                End If
                If strMenuText <> String.Empty Then
                    .Item(AddDynamicReportData.MNU_TXT) = strMenuText
                Else
                    .Item(AddDynamicReportData.MNU_TXT) = DBNull.Value
                End If
                If bv_blnCreateRightBit <> Nothing Then
                    .Item(AddDynamicReportData.CRT_RGHT_BT) = bv_blnCreateRightBit
                Else
                    .Item(AddDynamicReportData.CRT_RGHT_BT) = DBNull.Value
                End If
                If bv_blnEditRightBit <> Nothing Then
                    .Item(AddDynamicReportData.EDT_RGHT_BT) = bv_blnEditRightBit
                Else
                    .Item(AddDynamicReportData.EDT_RGHT_BT) = DBNull.Value
                End If
                If bv_blnActiveBit <> Nothing Then
                    .Item(AddDynamicReportData.ACTV_BT) = bv_blnActiveBit
                Else
                    .Item(AddDynamicReportData.ACTV_BT) = DBNull.Value
                End If
                If bv_strActivityRole <> String.Empty Then
                    .Item(AddDynamicReportData.ACTVTY_RL) = bv_strActivityRole
                Else
                    .Item(AddDynamicReportData.ACTVTY_RL) = DBNull.Value
                End If
                    .Item(AddDynamicReportData.EXCPTN_BT) = bv_blnExceptionBit
                If bv_strMasterIDCSV <> String.Empty Then
                    .Item(AddDynamicReportData.MSTR_ID_CSV) = bv_strMasterIDCSV
                Else
                    .Item(AddDynamicReportData.MSTR_ID_CSV) = DBNull.Value
                End If
                If bv_strQuickLinkIDCSV <> String.Empty Then
                    .Item(AddDynamicReportData.QCK_LNK_ID_CSV) = bv_strQuickLinkIDCSV
                Else
                    .Item(AddDynamicReportData.QCK_LNK_ID_CSV) = DBNull.Value
                End If
                .Item(AddDynamicReportData.CNCL_RGHT_BT) = 0
            End With
            objData.InsertRow(dr, ActivityInsertQuery, br_objTrans)
            dr = Nothing
            CreateActivity = bv_intActivityID
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateActivity() TABLE NAME:Activity"

    Public Function UpdateActivity(ByVal bv_i32ActivityID As Int32, _
                                      ByVal bv_strActivityName As String, _
                                         ByVal bv_strListURL As String, _
                                     ByVal bv_strPageURL As String, _
                                   ByVal bv_strPageTitle As String, _
                                   ByVal bv_strTableName As String, _
                                    ByVal bv_i32ProcessID As Int32, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                    ByVal bv_blnOrderFlag As Boolean, _
                                    ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            Dim UpdateQuery As String = ""
            objData = New DataObjects()
            dr = ds.Tables(AddDynamicReportData._ACTIVITY).NewRow()
            With dr
                Dim strMenuText As String = String.Empty
                strMenuText = String.Concat("text=", bv_strActivityName, ";url=", bv_strListURL, "?apu=", bv_strPageURL, "&activityid=", bv_i32ActivityID, "&activityname=", bv_strActivityName, "&type=Master&tablename=", bv_strTableName, "&listpanetitle=Reports >> ", bv_strActivityName, "&pagetitle=", bv_strPageTitle, "&pagetype=PostBack&mode=edit&qrytype=new;status=", bv_strActivityName)

                .Item(AddDynamicReportData.ACTVTY_ID) = bv_i32ActivityID
                .Item(AddDynamicReportData.PG_TTL) = bv_strPageTitle
                .Item(AddDynamicReportData.PRCSS_ID) = bv_i32ProcessID
                Dim i32OrderNo As Integer
                If bv_blnOrderFlag = True Then
                    i32OrderNo = GetActivityOrderbyProcessId(bv_i32ProcessID, br_objTrans)
                    If i32OrderNo = 0 Then
                        .Item(AddDynamicReportData.ORDR_NO) = 1
                    Else
                        .Item(AddDynamicReportData.ORDR_NO) = DBNull.Value
                    End If
                    UpdateQuery = ActivityUpdateQueryOrder
                Else
                    UpdateQuery = ActivityUpdateQuery
                End If
              

                If strMenuText <> String.Empty Then
                    .Item(AddDynamicReportData.MNU_TXT) = strMenuText
                Else
                    .Item(AddDynamicReportData.MNU_TXT) = DBNull.Value
                End If
                If bv_blnActiveBit <> Nothing Then
                    .Item(AddDynamicReportData.ACTV_BT) = bv_blnActiveBit
                Else
                    .Item(AddDynamicReportData.ACTV_BT) = DBNull.Value
                End If
            End With
            UpdateActivity = objData.UpdateRow(dr, UpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetActivityByActivityNameProcessID() TABLE NAME:ACTIVITY"

    Public Function GetActivityByActivityNameProcessID(ByVal bv_strActivityName As String, ByVal bv_i32ProcessID As Int32) As String
        Try
            Dim hashParameters As New Hashtable()
            hashParameters.Add(AddDynamicReportData.ACTVTY_NAM, bv_strActivityName)
            hashParameters.Add(AddDynamicReportData.PRCSS_ID, bv_i32ProcessID)
            objData = New DataObjects(ACTIVITYSelectQueryByActivityNameProcessID, hashParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetProcessIDByProcessName() TABLE NAME:ACTIVITY"

    Public Function GetProcessIDByProcessName(ByVal bv_strProcessName As String) As String
        Try
            objData = New DataObjects(PROCESSSelectQueryByProcessName, AddDynamicReportData.PRCSS_NAM, bv_strProcessName)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetProcessIDByProcessIDActivityName() TABLE NAME:ACTIVITY"

    Public Function GetProcessIDByProcessIDActivityName(ByVal bv_strProcessID As Int32, ByVal bv_strActivityName As String) As Integer
        Try
            Dim hashParameters As New Hashtable()
            hashParameters.Add(AddDynamicReportData.PRCSS_ID, bv_strProcessID)
            hashParameters.Add(AddDynamicReportData.ACTVTY_NAM, bv_strActivityName)
            objData = New DataObjects(ActivitySelectProcessIDActivityName, hashParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetActivityByProcessId() TABLE NAME:ACTIVITY"

    Public Function GetActivityByProcessId(ByVal bv_i32ProcessId As Int32) As Integer
        Try
            objData = New DataObjects(OrderNoSelectQuery, AddDynamicReportData.PRCSS_ID, bv_i32ProcessId)
            Return objData.ExecuteScalar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetActivity() TABLE NAME:ACTIVITY"

    Public Function GetActivity() As Integer
        Try
            objData = New DataObjects(ActivitySelectQuery)
            Return objData.ExecuteScalar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateProcess() TABLE NAME:Process"

    Public Function CreateProcess(ByVal bv_strProcessName As String, _
        ByVal bv_i32ParentID As Int32, _
        ByVal bv_strProcessRole As String,
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(AddDynamicReportData._PROCESS).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(AddDynamicReportData._PROCESS, br_objTrans)
                .Item(AddDynamicReportData.PRCSS_ID) = intMax
                .Item(AddDynamicReportData.PRCSS_NAM) = bv_strProcessName
                If bv_i32ParentID <> 0 Then
                    .Item(AddDynamicReportData.PRNT_ID) = bv_i32ParentID
                Else
                    .Item(AddDynamicReportData.PRNT_ID) = DBNull.Value
                End If
                Dim i32ProcessOrder As Integer = GetProcessOrderbyParentId(bv_i32ParentID, br_objTrans)
                If i32ProcessOrder = 0 Then
                    .Item(AddDynamicReportData.PRCSS_ORDR) = 1
                Else
                    .Item(AddDynamicReportData.PRCSS_ORDR) = i32ProcessOrder
                End If
                If bv_strProcessRole <> Nothing Then
                    .Item(AddDynamicReportData.PRCSS_RL) = bv_strProcessRole
                Else
                    .Item(AddDynamicReportData.PRCSS_RL) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, ProcessInsertQuery, br_objTrans)
            dr = Nothing
            CreateProcess = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetProcessOrderbyParentId() TABLE NAME:PROCESS"

    Public Function GetProcessOrderbyParentId(ByVal bv_i32ProcessID As Int32, ByRef br_objTrans As Transactions) As Integer
        Try
            objData = New DataObjects(ProcessSelectQueryByParentID, AddDynamicReportData.PRNT_ID, bv_i32ProcessID)
            Return objData.ExecuteScalar(br_objTrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetActivityOrderbyProcessId() TABLE NAME:ACTIVITY"

    Public Function GetActivityOrderbyProcessId(ByVal bv_i32ProcessID As Int32, ByRef br_objTrans As Transactions) As Integer
        Try
            objData = New DataObjects(ActivitySelectQueryByProcessID, AddDynamicReportData.PRCSS_ID, bv_i32ProcessID)
            Return objData.ExecuteScalar(br_objTrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateReportParameter() TABLE NAME:REPORT_PARAMETER"

    Public Function CreateReportParameter(ByVal bv_i32ReportID As Int32, _
        ByVal bv_strParameterName As String, _
        ByVal bv_strParameterDisplayName As String, _
        ByVal bv_strParameterType As String, _
        ByVal bv_strParamaterQuery As String, _
        ByVal bv_blnParameterOptional As Boolean, _
        ByVal bv_strParameterRole As String, _
        ByVal bv_blnRequiredDepotID As Boolean, _
        ByVal bv_strParameterOrderColumn As String, _
        ByVal bv_strParamterOrderType As String, _
        ByVal bv_blnParameterDropdown As Boolean, _
        ByVal bv_strParameterDependent As String, _
         ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(AddDynamicReportData._REPORT_PARAMETER).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(AddDynamicReportData._REPORT_PARAMETER, br_objTrans)
                .Item(AddDynamicReportData.PRMTR_ID) = intMax
                .Item(AddDynamicReportData.RPRT_ID) = bv_i32ReportID
                .Item(AddDynamicReportData.PRMTR_NAM) = bv_strParameterName
                If bv_strParameterDisplayName <> Nothing Then
                    .Item(AddDynamicReportData.PRMTR_DSPLY_NAM) = bv_strParameterDisplayName
                Else
                    .Item(AddDynamicReportData.PRMTR_DSPLY_NAM) = DBNull.Value
                End If
                .Item(AddDynamicReportData.PRMTR_TYP) = bv_strParameterType
                If bv_strParamaterQuery <> Nothing Then
                    .Item(AddDynamicReportData.PRMTR_QRY) = bv_strParamaterQuery
                Else
                    .Item(AddDynamicReportData.PRMTR_QRY) = DBNull.Value
                End If
                .Item(AddDynamicReportData.PRMTR_OPT) = bv_blnParameterOptional
                If bv_strParameterRole <> Nothing Then
                    .Item(AddDynamicReportData.PRMTR_RL) = bv_strParameterRole
                Else
                    .Item(AddDynamicReportData.PRMTR_RL) = DBNull.Value
                End If
                If bv_blnRequiredDepotID <> Nothing Then
                    .Item(AddDynamicReportData.REQ_DPT_ID) = bv_blnRequiredDepotID
                Else
                    .Item(AddDynamicReportData.REQ_DPT_ID) = DBNull.Value
                End If
                If bv_strParameterOrderColumn <> Nothing Then
                    .Item(AddDynamicReportData.PRMTR_ORDR_CLMN) = bv_strParameterOrderColumn
                Else
                    .Item(AddDynamicReportData.PRMTR_ORDR_CLMN) = DBNull.Value
                End If
                If bv_strParamterOrderType <> Nothing Then
                    .Item(AddDynamicReportData.PRMTR_ORDR_TYP) = bv_strParamterOrderType
                Else
                    .Item(AddDynamicReportData.PRMTR_ORDR_TYP) = DBNull.Value
                End If
                    .Item(AddDynamicReportData.PRMTR_DRPDWN) = bv_blnParameterDropdown
                If bv_strParameterDependent <> Nothing Then
                    .Item(AddDynamicReportData.PRMTR_DPNDNT) = bv_strParameterDependent
                Else
                    .Item(AddDynamicReportData.PRMTR_DPNDNT) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, ReportParameterInsertQuery, br_objTrans)
            dr = Nothing
            CreateReportParameter = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateReportParameter() TABLE NAME:REPORT_PARAMETER"

    Public Function UpdateReportParameter(ByVal bv_i32ParamterID As Int32, _
       ByVal bv_i32ReportID As Int32, _
        ByVal bv_strParameterDisplayName As String, _
        ByVal bv_blnParameterOptional As Boolean, _
        ByVal bv_blnParameterDropdown As Boolean, _
        ByVal bv_strParameterDependent As String, _
         ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(AddDynamicReportData._REPORT_PARAMETER).NewRow()
            With dr
                .Item(AddDynamicReportData.PRMTR_ID) = bv_i32ParamterID
                .Item(AddDynamicReportData.RPRT_ID) = bv_i32ReportID
                If bv_strParameterDisplayName <> Nothing Then
                    .Item(AddDynamicReportData.PRMTR_DSPLY_NAM) = bv_strParameterDisplayName
                Else
                    .Item(AddDynamicReportData.PRMTR_DSPLY_NAM) = DBNull.Value
                End If
                .Item(AddDynamicReportData.PRMTR_OPT) = bv_blnParameterOptional
                
                .Item(AddDynamicReportData.PRMTR_DRPDWN) = bv_blnParameterDropdown
                If bv_strParameterDependent <> "" Then
                    .Item(AddDynamicReportData.PRMTR_DPNDNT) = bv_strParameterDependent
                Else
                    .Item(AddDynamicReportData.PRMTR_DPNDNT) = DBNull.Value
                End If
            End With
            UpdateReportParameter = objData.UpdateRow(dr, ReportParameterUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetReportParameterByReportID() TABLE NAME:REPORT_PARAMETER"

    Public Function GetReportParameterByReportID(ByVal bv_i32ReportId As Int32, ByRef objTrans As Transactions) As AddDynamicReportDataSet
        Try
            objData = New DataObjects(ReportParameterSelectQueryByReportID, AddDynamicReportData.RPRT_ID, CStr(bv_i32ReportId))
            objData.Fill(CType(ds, DataSet), AddDynamicReportData._REPORT_PARAMETER, objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetReportParameterByReportIDValue() TABLE NAME:REPORT_PARAMETER"

    Public Function GetReportParameterByReportIDValue(ByVal bv_i32ReportId As Int32) As AddDynamicReportDataSet
        Try
            objData = New DataObjects(ReportParameterSelectQueryByReportID, AddDynamicReportData.RPRT_ID, CStr(bv_i32ReportId))
            objData.Fill(CType(ds, DataSet), AddDynamicReportData._REPORT_PARAMETER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetReportParameterByParameterID() TABLE NAME:REPORT_PARAMETER"

    Public Function GetReportParameterByParameterID(ByVal bv_strParameterName As String, ByVal bv_i32ReportId As Int32, ByRef objTrans As Transactions) As Integer
        Try
            Dim hashParameters As New Hashtable()
            hashParameters.Add(AddDynamicReportData.PRMTR_NAM, bv_strParameterName)
            hashParameters.Add(AddDynamicReportData.RPRT_ID, bv_i32ReportId)
            objData = New DataObjects(ReportParameterSelectQueryByParameterID, hashParameters)
            Return objData.ExecuteScalar(objTrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "DeleteReportParameter() TABLE NAME:Report_Parameter"

    Public Function DeleteReportParameter(ByVal bv_strParameterId As Int32, ByRef objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(AddDynamicReportData._REPORT_PARAMETER).NewRow()
            With dr
                .Item(AddDynamicReportData.PRMTR_ID) = bv_strParameterId
            End With
            DeleteReportParameter = objData.DeleteRow(dr, Report_ParameterDeleteQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class

#End Region
