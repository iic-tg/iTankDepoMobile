Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Imports System.Xml
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.WebControls.v4.Data
Imports iInterchange.iTankDepo.Business.Admin
Imports iInterchange.Framework.Common
Imports iInterchange.iTankDepo.Data
Imports System.Text.RegularExpressions
Imports System.Web

''' <summary>
''' This class is common data class for all pages which has repeatly used functions in entire application
''' </summary>
''' <remarks></remarks>ss
Public Class CommonData
    Inherits Pagebase

    Public pvt_strActivityId As String = Nothing

#Region "GetActivities"
    ''' <summary>
    ''' Get Activities
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetActivities() As DataTable
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._ACTIVITY)
            Return dtUser
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetCurrentUserID"

    ''' <summary>
    ''' This method used to get current user id from user configuration
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetCurrentUserID() As String
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            Dim strCurrentUserID As String = ""
            If dtUser.Rows.Count > 0 Then
                strCurrentUserID = dtUser.Rows(0).Item(UserData.USR_ID)
            End If
            Return strCurrentUserID
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetCurrentUserName"
    ''' <summary>
    ''' This method used to get current user name from user configuration
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetCurrentUserName() As String
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            Dim strCurrentUserName As String = ""
            If dtUser.Rows.Count > 0 Then
                strCurrentUserName = dtUser.Rows(0).Item(UserData.USR_NAM)
            End If
            Return strCurrentUserName
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetUserLastLoginDt"
    ''' <summary>
    ''' This method used to get last login time of current user
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetUserLastLoginDt() As String
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            Dim strLastLoginDt As String = ""
            If dtUser.Rows.Count > 0 Then
                strLastLoginDt = CDate(dtUser.Rows(0).Item(UserData.LGN_DT)).ToString("dd-MMM-yyyy hh:mm tt").ToUpper()
            End If
            Return strLastLoginDt
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetCurrentDate"
    ''' <summary>
    ''' This method used to get current date 
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetCurrentDate() As String
        Return DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt").ToUpper
    End Function
#End Region

#Region "GetCurrentRoleCode"
    ''' <summary>
    ''' This method used to get current user's role from cmacgm c3d database
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetCurrentRoleCode() As String
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            Dim strCurrentRoleCode As String = ""
            If dtUser.Rows.Count > 0 Then
                strCurrentRoleCode = dtUser.Rows(0).Item(UserData.RL_CD)
            End If
            Return strCurrentRoleCode
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetCurrentRoleID"
    ''' <summary>
    ''' This method used to get current user's role from  database
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetCurrentRoleID() As String
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            Dim strCurrentRoleID As String = ""
            If dtUser.Rows.Count > 0 Then
                strCurrentRoleID = dtUser.Rows(0).Item(UserData.RL_ID)
            End If
            Return strCurrentRoleID
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetBaseCurrencyCode"
    ''' <summary>
    ''' This method used to get base currency code from web.config
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetBaseCurrencyCode() As String
        Try
            Dim strBaseCurrencyCode As String = CommonWeb.pub_GetConfigValue("BaseCurrencyCode")
            If Not String.IsNullOrEmpty(strBaseCurrencyCode) Then
                Return strBaseCurrencyCode
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetSelectedRow"
    ''' <summary>
    ''' This method gets current row from the list grid. Used to get page attributes in fnGetData method for master screens.
    ''' </summary>
    ''' <param name="bv_intItemNo">Denotes list grid data key index</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetSelectedRow(ByVal bv_intItemNo As Integer) As Hashtable
        Dim table As DataTable
        Dim dr As DataRow
        Dim objHasTable As New Hashtable
        Try
            table = RetrieveData("listdata" + pvt_strActivityId)
            If table IsNot Nothing Then
                Dim dv As New DataView
                dv = table.DefaultView
                If table.Rows.Count > 0 And Not bv_intItemNo >= dv.Count Then
                    If bv_intItemNo >= 0 Then
                        dr = dv.ToTable.Rows(bv_intItemNo)

                        For Each dc As DataColumn In dr.Table.Columns
                            objHasTable.Add(dc.ColumnName, dr.Item(dc.ColumnName))
                        Next
                    Else
                        dr = Nothing
                    End If
                End If

                GetSelectedRow = objHasTable
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objHasTable = Nothing
        End Try
    End Function
#End Region

#Region "GetMasterIDs"
    Public Function GetMasterIDs(ByVal bv_intActivityID As Integer) As String
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._ACTIVITY)
            Dim dtRoleRight As DataTable = dsUser.Tables(UserData._ROLE_RIGHT)
            Dim strMasterIds As String = ""
            Dim strTempMasters As String = ""
            If dtUser.Select(String.Concat(UserData.ACTVTY_ID, "='", bv_intActivityID, "'")).Length > 0 Then
                If Not IsDBNull(dtUser.Select(String.Concat(UserData.ACTVTY_ID, "='", bv_intActivityID, "'"))(0).Item(UserData.MSTR_ID_CSV)) Then
                    strTempMasters = dtUser.Select(String.Concat(UserData.ACTVTY_ID, "='", bv_intActivityID, "'"))(0).Item(UserData.MSTR_ID_CSV)
                    If strTempMasters <> "" Then
                        For Each strActivityId As String In strTempMasters.Split(",")
                            If dtRoleRight.Select(UserData.ACTVTY_ID & "='" & strActivityId & "' AND " & UserData.VW_BT & "='True'").Length > 0 Then
                                If strMasterIds <> "" Then
                                    strMasterIds = String.Concat(strMasterIds, ",", strActivityId)
                                Else
                                    strMasterIds = strActivityId
                                End If
                            End If
                        Next
                    End If
                End If
            End If
            Return strMasterIds
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetQuickLinks"
    Public Function GetQuickLinks(ByVal bv_intActivityId As Integer) As String
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._ACTIVITY)
            Dim dtRoleRight As DataTable = dsUser.Tables(UserData._ROLE_RIGHT)
            Dim strQuickLinksIDs As String = ""
            Dim strTempLinks As String = ""
            If dtUser.Select(String.Concat(UserData.ACTVTY_ID, "='", bv_intActivityId, "'")).Length > 0 Then
                If Not IsDBNull(dtUser.Select(String.Concat(UserData.ACTVTY_ID, "='", bv_intActivityId, "'"))(0).Item(UserData.QCK_LNK_ID_CSV)) Then
                    strTempLinks = dtUser.Select(String.Concat(UserData.ACTVTY_ID, "='", bv_intActivityId, "'"))(0).Item(UserData.QCK_LNK_ID_CSV)
                    If strTempLinks <> "" Then
                        For Each strActivityId As String In strTempLinks.Split(",")
                            If dtRoleRight.Select(UserData.ACTVTY_ID & "='" & strActivityId & "' AND " & UserData.VW_BT & "='True'").Length > 0 Then
                                If strQuickLinksIDs <> "" Then
                                    strQuickLinksIDs = String.Concat(strQuickLinksIDs, ",", strActivityId)
                                Else
                                    strQuickLinksIDs = strActivityId
                                End If
                            End If
                        Next
                    End If
                End If
            End If
            Return strQuickLinksIDs
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetFavouritesList"

    Public Function GetFavouritesList() As String
        Try
            If Not RetrieveData("UserData") Is Nothing Then
                Dim dsUser As DataSet = RetrieveData("UserData")
                Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
                Dim dtRoleRight As DataTable = dsUser.Tables(UserData._ROLE_RIGHT)
                If dtRoleRight.Rows.Count = 0 Then
                    Dim objCommonui As New CommonUI()
                    dtRoleRight = objCommonui.pub_GetRoleRightByRoleID(CInt(GetCurrentRoleID())).Tables(CommonUIData._ROLE_RIGHT)
                    dsUser.Tables(UserData._ROLE_RIGHT).Merge(dtRoleRight)
                End If
                Dim strFavouriteActivities As String = ""
                Dim strTempFavourites As String = ""
                If dtUser.Rows.Count > 0 Then
                    If Not IsDBNull(dtUser.Rows(0).Item(UserData.FAV_ACTVTY_ID_CSV)) Then
                        strTempFavourites = dtUser.Rows(0).Item(UserData.FAV_ACTVTY_ID_CSV)
                        If strTempFavourites <> "" Then
                            For Each strActivityId As String In strTempFavourites.Split(",")
                                If dtRoleRight.Select(UserData.ACTVTY_ID & "='" & strActivityId & "' AND " & UserData.VW_BT & "='True'").Length > 0 Then
                                    If strFavouriteActivities <> "" Then
                                        strFavouriteActivities = String.Concat(strFavouriteActivities, ",", strActivityId)
                                    Else
                                        strFavouriteActivities = strActivityId
                                    End If
                                End If
                            Next
                        End If
                    End If
                    CacheData("UserData", dsUser)
                    Return strFavouriteActivities
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region


#Region "GenerateWFData"
    Public Function GenerateWFData(ByVal bv_intActivityID As Integer) As String
        Dim strJSObj As New ArrayList
        Dim strJSValue() As String
        Dim strResult As String

        Try
            Dim intRoleID As Integer = GetCurrentRoleID()
            With strJSObj
                .Add(String.Concat("USERID=", GetCurrentUserID()))
                .Add(String.Concat("SYSTM_DT=", GetCurrentDate()))
                .Add(String.Concat("USERNAME=", GetCurrentUserName()))
                .Add(String.Concat("RL_CD=", GetCurrentRoleCode()))
                .Add(String.Concat("RL_ID=", intRoleID))
                .Add(String.Concat("DPT_ID=", GetDepotID()))
                .Add(String.Concat("DPT_CD=", GetDepotCD()))
                .Add(String.Concat("MSTR_ID_CSV=", GetMasterIDs(bv_intActivityID)))
                .Add(String.Concat("QCK_LNK_ID_CSV=", GetQuickLinks(bv_intActivityID)))

            End With

            'Get Role Rights
            Dim objCommonUI As New CommonUI()
            Dim dsCommon As CommonUIDataSet

            If bv_intActivityID > 0 Then
                dsCommon = objCommonUI.pub_GetRoleRightByRoleIDAndActivityID(intRoleID, bv_intActivityID)

                If dsCommon.Tables(CommonUIData._ROLE_RIGHT).Rows.Count > 0 Then
                    With dsCommon.Tables(CommonUIData._ROLE_RIGHT).Rows(0)
                        strJSObj.Add(String.Concat("CRT_BT=", .Item(CommonUIData.CRT_BT)))
                        strJSObj.Add(String.Concat("EDT_BT=", .Item(CommonUIData.EDT_BT)))
                        strJSObj.Add(String.Concat("VW_BT=", .Item(CommonUIData.VW_BT)))
                    End With
                End If
            End If

            strJSValue = strJSObj.ToArray(GetType(System.String))
            strResult = String.Join("&", strJSValue)
            Return strResult
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("GenerateWFData", Reflection.MethodBase.GetCurrentMethod.Name, "Error in GenerateWfData method.")
        Finally
            strJSObj = Nothing
            strResult = Nothing
            strJSValue = Nothing
        End Try
    End Function

    Public Function GenerateWFData() As String
        Dim strJSObj As New ArrayList
        Dim strJSValue() As String
        Dim strResult As String

        Try
            Dim intRoleID As Integer = GetCurrentRoleID()
            With strJSObj
                .Add(String.Concat("USERID=", GetCurrentUserID()))
                .Add(String.Concat("SYSTM_DT=", GetCurrentDate()))
                .Add(String.Concat("USERNAME=", GetCurrentUserName()))
                .Add(String.Concat("RL_CD=", GetCurrentRoleCode()))
                .Add(String.Concat("RL_ID=", intRoleID))
                .Add(String.Concat("DPT_ID=", GetDepotID()))
                .Add(String.Concat("DPT_CD=", GetDepotCD()))
            End With

            'Get Role Rights
            Dim objCommonUI As New CommonUI()
            Dim dsCommon As CommonUIDataSet
            dsCommon = objCommonUI.pub_GetRoleRightByRoleID(intRoleID)
            If dsCommon.Tables(CommonUIData._ROLE_RIGHT).Rows.Count > 0 Then
                With dsCommon.Tables(CommonUIData._ROLE_RIGHT).Rows(0)
                    strJSObj.Add(String.Concat("CRT_BT=", .Item(CommonUIData.CRT_BT)))
                    strJSObj.Add(String.Concat("EDT_BT=", .Item(CommonUIData.EDT_BT)))
                    strJSObj.Add(String.Concat("VW_BT=", .Item(CommonUIData.VW_BT)))
                End With
            End If
            strJSValue = strJSObj.ToArray(GetType(System.String))
            strResult = String.Join("&", strJSValue)
            Return strResult
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("GenerateWFData", Reflection.MethodBase.GetCurrentMethod.Name, "Error in GenerateWfData method.")
        Finally
            strJSObj = Nothing
            strResult = Nothing
            strJSValue = Nothing
        End Try
    End Function
#End Region

#Region "ParseWFDATA"
    ''' <summary>
    ''' This method used to parse the workflow data
    ''' </summary>
    ''' <param name="strWFDATA">Denotes Workflow Data</param>
    ''' <param name="strKey">Denotes Key</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function ParseWFDATA(ByVal strWFDATA As String, ByVal strKey As String) As String
        Try
            Dim hstble As New Hashtable
            Dim strItems() As String
            strItems = strWFDATA.Split(CChar("&"))
            Dim strKeyValue As String = ""
            For i As Integer = 0 To strItems.Length - 1
                If strItems(i) <> "" Then
                    If Not hstble.Contains(strItems(i).Split(CChar("="))(0)) Then
                        hstble.Add(strItems(i).Split(CChar("="))(0), strItems(i).Split(CChar("="))(1))
                    End If
                End If
            Next i

            strKeyValue = hstble.Item(strKey)

            Return strKeyValue

            hstble = Nothing
            strItems = Nothing
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("CommonData", Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetCurrentPageMode"
    ''' <summary>
    ''' This method is used to get current page mode
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetCurrentPageMode() As String
        Return CStr(RetrieveData("pagemode" + pvt_strActivityId))
    End Function
#End Region

#Region "SetCurrentPageMode"
    ''' <summary>
    ''' This method is used to set current page mode
    ''' </summary>
    ''' <param name="bv_strMode">Denotes page mode</param>
    ''' <remarks></remarks>
    Public Sub SetCurrentPageMode(ByVal bv_strMode As String)
        CacheData("pagemode" + pvt_strActivityId, bv_strMode)
    End Sub
#End Region

#Region "StoreLockData"
    ''' <summary>
    ''' This method is used to stores lock data
    ''' </summary>
    ''' <param name="bv_strRefNoField">Denotes Reference No field</param>
    ''' <param name="bv_strRefNo">Denotes Reference No</param>
    ''' <param name="bv_strUserName">Denotes Current User Name</param>
    ''' <param name="bv_strSessionID">Denotes Current Session ID</param>
    ''' <param name="bv_strActivityName">Denotes Activity Name</param>
    ''' <param name="bv_strIpAddress">Denotes Ip Address</param>
    ''' <param name="bv_blnEditLock">Denotes Edit Lock, Which is used to lock the record</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function StoreLockData(ByVal bv_strRefNoField As String, ByRef bv_strRefNo As String, _
                            ByRef bv_strUserName As String, ByRef bv_strSessionID As String, _
                            ByVal bv_strActivityName As String, ByRef bv_strIpAddress As String, ByVal bv_blnEditLock As Boolean) As Boolean
        Try
            Dim objLockDataCollection As New LockDataCollection
            Dim dtLockdata As New DataTable
            If objLockDataCollection.Values IsNot Nothing Then
                dtLockdata = objLockDataCollection.Values
            End If

            Dim dr As DataRow = dtLockdata.NewRow
            dr(LockData.RefNoField) = bv_strRefNoField
            dr(LockData.RefNo) = bv_strRefNo
            dr(LockData.LockedBy) = bv_strUserName
            dr(LockData.EditLock) = bv_blnEditLock
            dr(LockData.ActivityName) = bv_strActivityName
            dr(LockData.SessionID) = bv_strSessionID
            dr(LockData.LockTime) = DateTime.Now
            dr(LockData.IPAddress) = bv_strIpAddress
            dr(LockData.RecordLockID) = Guid.NewGuid().ToString()

            Dim blnRecordExists As Boolean = False
            Dim drlockdata As DataRow()

            'Check whether the record is locked
            drlockdata = dtLockdata.Select(String.Concat(LockData.RefNoField, "='", bv_strRefNoField, "' AND ", _
                                                         LockData.RefNo, "='", bv_strRefNo, "'"))
            If drlockdata.Length > 0 Then
                blnRecordExists = True
                With drlockdata(0)
                    bv_strUserName = .Item(LockData.LockedBy)
                    bv_strIpAddress = .Item(LockData.IPAddress)
                    bv_strSessionID = .Item(LockData.SessionID)
                    bv_strRefNo = .Item(LockData.RefNo)
                End With
            End If
            If Not blnRecordExists Then
                objLockDataCollection.Add(dr)
            End If
            Return blnRecordExists
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "ClearLockData"
    ''' <summary>
    ''' This method is used to clear the locked data from log out page
    ''' </summary>
    ''' <param name="bv_strSessionID">Denotes Current Session ID</param>
    ''' <remarks></remarks>
    Public Sub ClearLockData(ByVal bv_strSessionID As String)
        Try
            Dim objLockDataCollection As New LockDataCollection

            Dim drlockdata As DataRow()
            Dim dtLockdata As DataTable
            dtLockdata = objLockDataCollection.Values
            If Not dtLockdata Is Nothing Then
                drlockdata = dtLockdata.Select(String.Concat(LockData.SessionID, "='", bv_strSessionID, "'"))

                If drlockdata.Length > 0 Then
                    For Each dr As DataRow In drlockdata
                        objLockDataCollection.Remove(dr.Item(LockData.RecordLockID))
                    Next
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "FlushLockData"
    ''' <summary>
    ''' This method used to flush previously locked record on every navigation
    ''' </summary>
    ''' <param name="bv_strRefNoField">Denotes Previous Reference No Field</param>
    ''' <param name="bv_strRefNo">Denotes Previous Reference No</param>
    ''' <param name="bv_strSessionID">Denotes Previous Session ID</param>
    ''' <param name="bv_strActivityName">Denotes Previous Activity Name</param>
    ''' <remarks></remarks>
    Public Sub FlushLockData(ByVal bv_strRefNoField As String, ByVal bv_strRefNo As String, ByVal bv_strSessionID As String, ByVal bv_strActivityName As String)
        Try
            Dim objLockDataCollection As New LockDataCollection

            Dim drlockdata As DataRow()
            Dim dtLockdata As DataTable
            dtLockdata = objLockDataCollection.Values

            'drlockdata = dtLockdata.Select(String.Concat(LockData.SessionID, "='", bv_strSessionID, "' AND ", _
            '                        LockData.RefNoField, "='", bv_strRefNoField, "' AND ", LockData.RefNo, "='", bv_strRefNo, _
            '                        "' AND ", LockData.ActivityName, "='", bv_strActivityName, "'"))
            drlockdata = dtLockdata.Select(String.Concat(LockData.SessionID, "='", bv_strSessionID, "' AND ", _
                                    LockData.RefNoField, "='", bv_strRefNoField, "' AND ", LockData.RefNo, "='", bv_strRefNo, _
                                    "' AND ", LockData.ActivityName, "='", bv_strActivityName, "'"))

            If drlockdata.Length > 0 Then
                objLockDataCollection.Remove(drlockdata(0).Item(LockData.RecordLockID))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
#Region "FlushLockDataByActivity"
    ''' <summary>
    ''' This method used to flush previously locked record on every navigation
    ''' </summary>
    ''' <param name="bv_strSessionID">Denotes Previous Session ID</param>
    ''' <param name="bv_strActivityName">Denotes Previous Activity Name</param>
    ''' <remarks></remarks>
    Public Sub FlushLockDataByActivity(ByVal bv_strSessionID As String, ByVal bv_strActivityName As String)
        Try
            Dim objLockDataCollection As New LockDataCollection

            Dim drlockdata As DataRow()
            Dim dtLockdata As DataTable
            dtLockdata = objLockDataCollection.Values

            'drlockdata = dtLockdata.Select(String.Concat(LockData.SessionID, "='", bv_strSessionID, "' AND ", _
            '                        LockData.RefNoField, "='", bv_strRefNoField, "' AND ", LockData.RefNo, "='", bv_strRefNo, _
            '                        "' AND ", LockData.ActivityName, "='", bv_strActivityName, "'"))
            drlockdata = dtLockdata.Select(String.Concat(LockData.SessionID, "='", bv_strSessionID, "' AND ", _
                                    LockData.ActivityName, "='", bv_strActivityName, "'"))

            If drlockdata.Length > 0 Then
                objLockDataCollection.RemoveAll(bv_strSessionID, bv_strActivityName, GetCurrentUserName())
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
#Region "GetLockData"
    ''' <summary>
    ''' This method used to get lock data
    ''' </summary>
    ''' <param name="bv_strRefNoField">Denotes Reference No Field</param>
    ''' <param name="bv_strRefNo">Denotes Reference No</param>
    ''' <param name="bv_strSessionID">Denotes Session ID</param>
    ''' <param name="br_strUsername">Denotes User Name</param>
    ''' <param name="bv_strActivityName">Denotes Activity Name</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function GetLockData(ByVal bv_strRefNoField As String, ByVal bv_strRefNo As String, ByVal bv_strSessionID As String, _
                                            ByRef br_strUsername As String, ByRef br_strActivityName As String) As Boolean
        Try
            Dim objLockData As New LockData

            Dim objLockDataCollection As New LockDataCollection

            Dim drlockdata As DataRow
            Dim dtLockdata As DataTable

            dtLockdata = objLockDataCollection.Values

            For i As Integer = 0 To dtLockdata.Rows.Count - 1
                drlockdata = dtLockdata.Rows(i)
                If drlockdata.Item(LockData.RefNoField) = bv_strRefNoField AndAlso drlockdata.Item(LockData.RefNo) = bv_strRefNo Then
                    br_strUsername = drlockdata.Item(LockData.LockedBy)
                    br_strActivityName = drlockdata.Item(LockData.ActivityName)
                    Return drlockdata.Item(LockData.EditLock)
                End If
            Next i

            br_strUsername = Session("UserName")

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "ResetLockData"
    ''' <summary>
    ''' This method used to reset lock data
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ResetLockData()
        Try
            Dim strSessionID As String = GetSessionID()
            Dim objLockDataCollection As New LockDataCollection
            objLockDataCollection.RemoveAll(strSessionID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "HttpContextGetQueryString"
    ''' <summary>
    ''' This method used to get querystring value from current context
    ''' </summary>
    ''' <param name="bv_strKeyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HttpContextGetQueryString(ByVal bv_strKeyName As String) As String
        Return HttpContext.Current.Request.QueryString(bv_strKeyName)
    End Function
#End Region

#Region "MaintainSortandSearch"
    ''' <summary>
    ''' This method used to maintain sort and search while rebinding the list grid
    ''' </summary>
    ''' <param name="bv_dtListData"></param>
    ''' <param name="bv_strListSessionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MaintainSortandSearch(bv_dtListData As DataTable, Optional bv_strListSessionId As String = "listdata") As Boolean
        Try
            'Maintaining Sort and Search
            If Not RetrieveData(bv_strListSessionId) Is Nothing Then
                Dim strSort As String = ""
                Dim strRowFilter As String = ""
                strSort = CType(RetrieveData(bv_strListSessionId), DataTable).DefaultView.Sort
                If String.IsNullOrEmpty(strSort) = False Then
                    bv_dtListData.DefaultView.Sort = strSort
                End If
                strRowFilter = CType(RetrieveData(bv_strListSessionId), DataTable).DefaultView.RowFilter
                If String.IsNullOrEmpty(strRowFilter) = False Then
                    bv_dtListData.DefaultView.RowFilter = strRowFilter
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetEnumID"
    Public Function GetEnumID(ByVal bv_strEnumType As String, ByVal bv_strEnumName As String) As String
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtEnumContract As DataTable = dsUser.Tables(UserData._ENUM).Select(String.Concat(UserData.ENM_TYP_CD, "='", bv_strEnumType, "' and ", UserData.ENM_DSCRPTN_VC, "='", bv_strEnumName, "'")).CopyToDataTable()
            If dtEnumContract.Rows.Count > 0 Then
                Return dtEnumContract.Rows(0).Item(UserData.ENM_ID)
            End If
            Return Nothing
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetEnumCD"
    Public Function GetEnumCD(ByVal bv_intEnumId As Integer) As String
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim drArr As DataRow() = dsUser.Tables(UserData._ENUM).Select(String.Concat(UserData.ENM_ID, "='", bv_intEnumId, "'"))
            If drArr.Length > 0 Then
                Return drArr(0).Item(UserData.ENM_CD)
            End If
            Return String.Empty
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetDepotID"
    Public Function GetDepotID() As String
        Try
            Dim strDepotID As String
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strDepotID = dtUser.Rows(0).Item(UserData.DPT_ID)
            End If
            Return strDepotID
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetHeadQuarterID"
    Public Function GetHeadQuarterID() As String
        Try
            Dim strHeadQuarterID As String = String.Empty
            Dim dsDepot As New DepotDataSet
            Dim objDepot As New Depot
            Dim drDepot As DataRow()
            dsDepot = objDepot.pub_GetAllDepotDetails()
            drDepot = dsDepot.Tables(DepotData._V_DEPOT).Select(String.Concat(DepotData.ORGNZTN_TYP_CD, " = 'HQ' "))
            If drDepot.Length > 0 Then
                For Each dr As DataRow In drDepot
                    strHeadQuarterID = dr.Item(DepotData.DPT_ID)
                Next
            End If
            CacheData("HeadQuarterID", strHeadQuarterID)
            Return strHeadQuarterID
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetMultiLocationSupportConfig"
    Public Function GetMultiLocationSupportConfig() As String
        If RetrieveData("MultiLocationSupport") Is Nothing Then
            Dim strKeyValue As String = ""
            If RetrieveData("HeadQuarterID") = "" Then
                GetHeadQuarterID()
            End If

            Dim objCommonUI As New CommonUI
            Dim dsConfig As New DataSet
            dsConfig = objCommonUI.pub_GetConfigByKeyName("070", RetrieveData("HeadQuarterID"))
            If dsConfig.Tables(CommonUIData._CONFIG).Rows.Count > 0 Then
                strKeyValue = DecryptString(dsConfig.Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL).ToString)
                CacheData("MultiLocationSupport", strKeyValue)
            End If
        End If

        Return RetrieveData("MultiLocationSupport")
    End Function
#End Region

#Region "GetDepotCD"
    Public Function GetDepotCD() As String
        Try
            Dim strDepotCD As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strDepotCD = dtUser.Rows(0).Item(UserData.DPT_CD)
            End If
            Return strDepotCD
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetYardLocation"
    Public Function GetYardLocation() As String
        Try
            Dim strYardLocation As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strYardLocation = dtUser.Rows(0).Item(UserData.YRD_LCTN)
            End If
            Return strYardLocation
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetLocationOfCleaning()"
    Public Function GetLocationOfCleaning() As String
        Try
            Dim strLocationCleaning As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strLocationCleaning = dtUser.Rows(0).Item(UserData.LCTN_OF_CLNNG)
            End If
            Return strLocationCleaning
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetOrganizationTypeID"
    ''' <summary>
    ''' This Method is used to retrieve Organization Type from DEPOT detail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOrganizationTypeID() As String
        Try
            Dim strOrganizationTypeID As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strOrganizationTypeID = dtUser.Rows(0).Item(UserData.ORGNZTN_TYP_ID)
            End If
            Return strOrganizationTypeID
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region


#Region "GetOrganizationTypeCD"
    ''' <summary>
    ''' This Method is used to retrieve Organization Type from DEPOT detail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOrganizationTypeCD() As String
        Try
            Dim strOrganizationTypeID As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strOrganizationTypeID = dtUser.Rows(0).Item(UserData.ORGNZTN_TYP_CD)
            End If
            Return strOrganizationTypeID
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetDepotName"
    Public Function GetDepotName() As String
        Try
            Dim strDepotCD As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strDepotCD = dtUser.Rows(0).Item(UserData.DPT_NAM)
            End If
            Return strDepotCD
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region
#Region "GetDepotVATNo"
    Public Function GetDepotVATNo() As String
        Try
            Dim strDepotCD As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strDepotCD = dtUser.Rows(0).Item("DPT_VT_NO")
            End If
            Return strDepotCD
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region


#Region "GetCustomerLogo"
    Public Function GetCustomerLogo() As String
        Try
            Dim strDepotCD As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strDepotCD = dtUser.Rows(0).Item(UserData.CMPNY_LG_PTH)
            End If
            Return strDepotCD
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetUserPhoto"
    Public Function GetUserPhoto() As String
        Try
            Dim strUserPhotoPath As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strUserPhotoPath = dtUser.Rows(0).Item(UserData.PHT_PTH)
            End If
            Return strUserPhotoPath
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetEnumTable"
    Public Function GetEnumTableByTypeID(ByVal bv_intEnumTypeID As Integer) As DataTable
        Try
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtEnumTable As DataTable = dsUser.Tables(UserData._ENUM).Select(String.Concat(UserData.ENM_TYP_ID, "=", bv_intEnumTypeID)).CopyToDataTable
            Return dtEnumTable
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "ExtractConsontant"
    Public Function ExtractConsontant(ByVal inputString As String, ByVal includeY As Boolean) As String
        Try
            If Not inputString = "" Then
                Dim charstart As Char
                Dim pattern As String = If(includeY, "[aeiouyAEIOUY]", "[aeiouAEIOU]")
                inputString = Regex.Replace(inputString.Trim(), "((^[a-z]+)|([A-Z]{1}[a-z]+)|([A-Z]+(?=([A-Z][a-z])|($))))", "$1 ").Trim()
                Dim strarray As String() = inputString.Split(" ")
                If strarray.Length = 1 Then
                    charstart = inputString(0)
                    inputString = inputString.Substring(1, inputString.Length - 1)
                    Return String.Concat(charstart.ToString, System.Text.RegularExpressions.Regex.Replace(inputString, pattern, String.Empty).Replace(" ", String.Empty))
                Else
                    inputString = ""
                    For i = 0 To strarray.Length - 1
                        Dim bolFormated = True
                        Dim strng = strarray(i)
                        If Not strng = "" Then
                            If strng.ToUpper = "NAME" Or strng.ToUpper = "NO" Then
                                If strng.ToUpper = "NAME" Then
                                    inputString = String.Concat(inputString, "NAM")
                                    bolFormated = False
                                Else
                                    inputString = String.Concat(inputString, "NO")
                                    bolFormated = False
                                End If
                            End If
                            If bolFormated Then
                                charstart = strng(0)
                                strng = strng.Substring(1, strng.Length - 1)
                                Dim res As String = System.Text.RegularExpressions.Regex.Replace(strng, pattern, String.Empty).Replace(" ", String.Empty)
                                If i = strarray.Length - 1 Then
                                    inputString = String.Concat(inputString, charstart, res)
                                Else
                                    inputString = String.Concat(inputString, charstart, res, "_")
                                End If
                            Else
                                If Not i = strarray.Length - 1 Then
                                    inputString = String.Concat(inputString, "_")
                                End If
                            End If
                        End If
                    Next
                    If inputString.ToUpper.IndexOf("DSCRPTN") > 0 Or inputString.ToUpper.IndexOf("RMRKS") > 0 Then
                        inputString = String.Concat(inputString, "_VC")
                    End If
                    Return inputString
                End If
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetDepotLocalCurrencyID"
    Public Function GetDepotLocalCurrencyID() As Integer
        Try
            Dim intDepotCurrencyID As Integer
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                intDepotCurrencyID = dtUser.Rows(0).Item(UserData.LCL_CRRNCY_ID)
            End If
            Return intDepotCurrencyID
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "GetDepotLocalCurrencyCode"
    Public Function GetDepotLocalCurrencyCode() As String
        Try
            Dim strDepotCurrencyCD As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
            If dtUser.Rows.Count > 0 Then
                strDepotCurrencyCD = dtUser.Rows(0).Item(UserData.LCL_CRRNCY_CD)
            End If
            Return strDepotCurrencyCD
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

    '#Region "ExportToExcel"
    '    Public Function ExportToExcel(ByVal bv_dtSourceTable As DataTable, ByVal bv_ExpFilename As String, ByVal bv_strColumNames As String, ByVal bv_pkColumnName As String, ByVal bv_blnChangeColumn As Boolean) As StringWriter
    '        Try
    '            Dim dtSourceTable As New DataTable
    '            Dim strColumNames As String = ""
    '            dtSourceTable = bv_dtSourceTable.Copy()
    '            Dim objDataSet As New DatasetHelper()
    '            If bv_blnChangeColumn = True Then
    '                If bv_strColumNames.IndexOf(",") > 0 Then
    '                    For Each strCol As String In bv_strColumNames.Split(",")
    '                        If Not strCol = "" Then
    '                            strColumNames = String.Concat(strColumNames, dtSourceTable.Columns(strCol).Caption.ToString, ",")
    '                            dtSourceTable.Columns(strCol).ColumnName = dtSourceTable.Columns(strCol).Caption
    '                        End If
    '                    Next
    '                    If strColumNames.EndsWith(",") Then
    '                        strColumNames = strColumNames.Remove(strColumNames.Length - 1, 1)
    '                    End If
    '                Else
    '                    dtSourceTable.Columns(bv_strColumNames).ColumnName = dtSourceTable.Columns(bv_strColumNames).Caption
    '                    strColumNames = bv_strColumNames
    '                End If
    '                dtSourceTable.AcceptChanges()
    '            Else
    '                strColumNames = bv_strColumNames
    '            End If

    '            Dim dtExportTable As DataTable = objDataSet.SelectGroupByInto(bv_ExpFilename, dtSourceTable, strColumNames, "", bv_pkColumnName)
    '            Dim objExport As New ExportToExcel
    '            Dim objstream As New StringWriter
    '            objstream = objExport.ExportToExcel(dtExportTable)
    '            Return objstream
    '        Catch ex As Exception
    '            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '        End Try
    '    End Function
    '#End Region
    '#Region "ExportToExcel"
    '    Public Function ExportToExcelByPassingDataTable(ByVal bv_dtSourceTable As DataTable, ByVal bv_ExpFilename As String, ByVal bv_strColumNames As String, ByVal bv_pkColumnName As String, ByVal bv_blnChangeColumn As Boolean) As StringWriter
    '        Try
    '            Dim dtSourceTable As New DataTable
    '            Dim strColumNames As String = ""
    '            Dim str_051GWSKey As String
    '            Dim objCommon As New CommonData
    '            Dim dtExportTable As New DataTable
    '            If GetMultiLocationSupportConfig.ToLower = "true" Then
    '                str_051GWSKey = GetConfigSetting("051", GetHeadQuarterID())
    '            Else
    '                str_051GWSKey = GetConfigSetting("051", GetDepotID())
    '            End If

    '            dtSourceTable = bv_dtSourceTable.Copy()
    '            Dim objDataSet As New DatasetHelper()
    '            If bv_blnChangeColumn = True Then
    '                If bv_strColumNames.IndexOf(",") > 0 Then
    '                    For Each strCol As String In bv_strColumNames.Split(",")
    '                        If Not strCol = "" Then
    '                            strColumNames = String.Concat(strColumNames, dtSourceTable.Columns(strCol).Caption.ToString, ",")
    '                            dtSourceTable.Columns(strCol).ColumnName = dtSourceTable.Columns(strCol).Caption
    '                        End If
    '                    Next
    '                    If strColumNames.EndsWith(",") Then
    '                        strColumNames = strColumNames.Remove(strColumNames.Length - 1, 1)
    '                    End If
    '                Else
    '                    dtSourceTable.Columns(bv_strColumNames).ColumnName = dtSourceTable.Columns(bv_strColumNames).Caption
    '                    strColumNames = bv_strColumNames
    '                End If
    '                dtSourceTable.AcceptChanges()
    '            Else
    '                strColumNames = bv_strColumNames
    '            End If
    '            If str_051GWSKey.ToLower = "true" Then
    '                dtExportTable = objDataSet.SelectGroupByInto(bv_ExpFilename, dtSourceTable, strColumNames, "", bv_pkColumnName)
    '            End If
    '            Dim objExport As New ExportToExcel
    '            Dim objstream As New StringWriter
    '            If str_051GWSKey.ToLower = "true" Then
    '                objstream = objExport.ExportToExcel(dtExportTable)
    '            Else
    '                objstream = objExport.ExportToExcel(bv_dtSourceTable)
    '            End If

    '            Return objstream
    '        Catch ex As Exception
    '            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '        End Try
    '    End Function
    '#End Region

#Region "GetConfigSetting"
    Public Function GetConfigSetting(ByVal bv_strKeyName As String, ByRef br_blnKeyExist As Boolean) As String
        Try
            Dim intDepotID As Integer = CInt(GetDepotID())
            Dim strGetMultiLocationSupport As String = GetMultiLocationSupportConfig()
            If strGetMultiLocationSupport.ToLower = "true" Then
                intDepotID = GetHeadQuarterID()
            End If
            Dim objCommonui As New CommonUI
            Dim dsConfiguration As ConfigDataSet
            Dim strKeyValue As String = ""
            dsConfiguration = objCommonui.pub_GetConfigByKeyName(bv_strKeyName, intDepotID)
            If Not dsConfiguration.Tables(CommonUIData._CONFIG).Rows.Count > 0 Then
                br_blnKeyExist = False
            Else
                br_blnKeyExist = True
                strKeyValue = DecryptString(dsConfiguration.Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL).ToString)
            End If
            Return strKeyValue
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function

#End Region

#Region "DecryptString"
    Private Function DecryptString(Message As String) As String
        Dim Results As Byte()
        Dim UTF8 As New System.Text.UTF8Encoding()
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        Dim pvt_strKeyPhrase As String
        Try
            'pvt_strKeyPhrase = IO.File.GetLastWriteTime(String.Concat(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings("KeyFileName"))).ToString("yyMMddHHHmmss")
            pvt_strKeyPhrase = "IIC"
            Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(pvt_strKeyPhrase))
            TDESAlgorithm.Key = TDESKey
            TDESAlgorithm.Mode = CipherMode.ECB
            TDESAlgorithm.Padding = PaddingMode.PKCS7
            Dim DataToDecrypt As Byte() = Convert.FromBase64String(Message)
            Dim Decryptor As ICryptoTransform = TDESAlgorithm.CreateDecryptor()
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("Config.DecryptString", Message, ex.ToString())
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return UTF8.GetString(Results)
    End Function
#End Region

#Region "GetVersionNo"
    Public Function GetVersionNo() As String
        Try
            Dim strVersionNo As String = ""
            Dim dsUser As DataSet = RetrieveData("UserData")
            Dim dtUser As DataTable = dsUser.Tables(UserData._APP_VERSION)
            If dtUser.Rows.Count > 0 Then
                strVersionNo = dtUser.Rows(0).Item(UserData.APP_VERSION_NO)
            End If
            Return strVersionNo
        Catch ex As Exception
            Return Nothing
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "Processing  parameters and construct xslt template - Create Page in New Tab"

    Public Function Pub_CreateTab(ByVal bv_strTableName As String, _
                                           ByVal bv_strType As String) As String


        Dim objXSLTrans As New Xsl.XslCompiledTransform
        Dim objXMLDoc As New XmlDocument
        Dim swFileWriter As New StringWriter()
        Dim strCreatedTab As String = Nothing

        Dim strDomainPath As String = AppDomain.CurrentDomain.BaseDirectory.ToString
        Dim XSLargsList As New Xsl.XsltArgumentList
        Dim strTemplateFolder As String


        Try

            strTemplateFolder = String.Concat(strDomainPath, "Templates\HomePage.xslt")
            XSLargsList.AddParam("TableName", "", bv_strTableName)
            XSLargsList.AddParam("Type", "", bv_strType)


            objXSLTrans.Load(strTemplateFolder)
            objXSLTrans.Transform(objXMLDoc, XSLargsList, swFileWriter)

            objXMLDoc = Nothing
            objXSLTrans = Nothing

            strCreatedTab = swFileWriter.ToString()
            strCreatedTab = strCreatedTab.Remove(0, strCreatedTab.LastIndexOf(">") + 1)  'Removes the first tag, it is xml tag
            strCreatedTab = strCreatedTab.Replace("&lt;", "<")
            strCreatedTab = strCreatedTab.Replace("&gt;", ">")
            strCreatedTab = strCreatedTab.Replace("&amp;", "&")

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
        Return strCreatedTab
    End Function
#End Region

#Region "FlushLockData"
    ''' <summary>
    ''' This method used to flush previously locked record on every navigation
    ''' </summary>
    ''' <param name="bv_strRefNoField">Denotes Previous Reference No Field</param>
    ''' <param name="bv_strRefNo">Denotes Previous Reference No</param>
    ''' <param name="bv_strSessionID">Denotes Previous Session ID</param>
    ''' <param name="bv_strActivityName">Denotes Previous Activity Name</param>
    ''' <remarks></remarks>
    Public Sub FlushLockDataByActivityName(ByVal bv_strRefNoField As String, ByVal bv_strSessionID As String, ByVal bv_strActivityName As String)
        Try
            Dim objLockDataCollection As New LockDataCollection

            Dim drlockdata As DataRow()
            Dim dtLockdata As DataTable
            dtLockdata = objLockDataCollection.Values
            drlockdata = dtLockdata.Select(String.Concat(LockData.SessionID, "='", bv_strSessionID, "' AND ", _
                                    LockData.RefNoField, "='", bv_strRefNoField, "' AND ", LockData.ActivityName, "='", bv_strActivityName, _
                                    "'"))

            If drlockdata.Length > 0 Then
                objLockDataCollection.RemoveAll(bv_strSessionID, bv_strActivityName, GetCurrentUserName())
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "FlushLockData"
    ''' <summary>
    ''' This method used to flush previously locked record on every navigation
    ''' </summary>
    ''' <param name="bv_strRefNoField">Denotes Previous Reference No Field</param>
    ''' <param name="bv_strRefNo">Denotes Previous Reference No</param>
    ''' <param name="bv_strSessionID">Denotes Previous Session ID</param>
    ''' <param name="bv_strActivityName">Denotes Previous Activity Name</param>
    ''' <remarks></remarks>
    Public Sub FlushLockDataByActivityNameandRefNo(ByVal bv_strRefNoField As String, ByVal bv_strReferenceNo As String, ByVal bv_strActivityName As String)
        Try
            Dim objLockDataCollection As New LockDataCollection

            Dim drlockdata As DataRow()
            Dim dtLockdata As DataTable
            dtLockdata = objLockDataCollection.Values
            drlockdata = dtLockdata.Select(String.Concat(LockData.RefNo, "='", bv_strReferenceNo, "' AND ", _
                                    LockData.RefNoField, "='", bv_strRefNoField, "' AND ", LockData.ActivityName, "='", bv_strActivityName, _
                                    "'"))

            If drlockdata.Length > 0 Then
                objLockDataCollection.Remove(drlockdata(0).Item(LockData.RecordLockID))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetLockData"
    Public Function pub_GetLockData(ByVal bv_blnCheckBit As Boolean, _
                                    ByVal bv_strRefNo As String, _
                                    ByRef br_strUserName As String, _
                                    ByRef br_strActivityName As String, _
                                    ByVal bv_strCurrentIpAddress As String, _
                                    ByVal bv_blnListBit As Boolean, _
                                    ByVal bv_strRefNoField As String) As Boolean
        Try

            Dim strCurrentSessionId As String = String.Empty
            Dim strCurrentUserName As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim blnLock As Boolean = False
            strCurrentSessionId = GetSessionID()
            strCurrentUserName = GetCurrentUserName()
            If bv_blnCheckBit AndAlso bv_blnListBit = False Then
                blnLockData = StoreLockData(bv_strRefNoField, bv_strRefNo, strCurrentUserName, strCurrentSessionId, br_strActivityName, bv_strCurrentIpAddress, True)
                If blnLockData Then
                    GetLockData(bv_strRefNoField, bv_strRefNo, strCurrentSessionId, br_strUserName, br_strActivityName)
                    blnLock = True
                End If
            ElseIf bv_blnCheckBit = False AndAlso bv_blnListBit = False Then
                blnLock = False
                FlushLockData(bv_strRefNoField, bv_strRefNo, strCurrentSessionId, br_strActivityName)
            ElseIf bv_blnCheckBit = False AndAlso bv_blnListBit = True Then
                blnLockData = StoreLockData(bv_strRefNoField, bv_strRefNo, strCurrentUserName, strCurrentSessionId, br_strActivityName, bv_strCurrentIpAddress, True)
                If blnLockData Then
                    GetLockData(bv_strRefNoField, bv_strRefNo, strCurrentSessionId, br_strUserName, br_strActivityName)
                    blnLock = True
                End If
            End If
            Return blnLock
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
            Return False
        End Try
    End Function
#End Region

    Public Sub SetGridVisibilitybyIndex(ByRef bv_GridId As iFlexGrid, ByVal bv_visible As String, ByVal bv_index As Integer, ByVal str_KeySettings As String)
        Try
            If str_KeySettings.ToLower = "true" Then
                bv_GridId.Columns.Item(bv_index).ControlStyle.CssClass = "show"
                bv_GridId.Columns.Item(bv_index).ItemStyle.CssClass = "show"
                ' bv_GridId.Columns.Item(bv_index).HeaderStyle.CssClass = "show"
            Else
                bv_GridId.Columns.Item(bv_index).ControlStyle.CssClass = "hide"
                bv_GridId.Columns.Item(bv_index).ItemStyle.CssClass = "hide"
                bv_GridId.Columns.Item(bv_index).HeaderStyle.CssClass = "hide"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Sub SetGridVisibility(iFlexGrid As iFlexGrid, p2 As Boolean, p3 As Integer)
        Throw New NotImplementedException
    End Sub

End Class