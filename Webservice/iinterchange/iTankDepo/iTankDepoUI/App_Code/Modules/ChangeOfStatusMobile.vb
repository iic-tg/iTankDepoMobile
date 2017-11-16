Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ChangeOfStatusMobile
    Inherits System.Web.Services.WebService


    Dim gateInMobile As New GateinMobile_C
    Dim dsChangeOfStatus As New ChangeOfStatusDataSet
    Dim objCommon As New CommonData
    Dim arraylist As New ArrayList
    Dim SearchResult As New SearchResult
    Dim objChangeofStatus As New ChangeOfStatus()
    Dim conn As New Dropdown_C

    <WebMethod(enableSession:=True)> _
     <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function Search(ByVal StatusID As String, ByVal EquipmentNo As String, ByVal CustomerID As String, ByVal UserName As String) As SearchResult

        gateInMobile.DepotID(UserName)

        Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
        Dim dtTemp As New DataTable

        dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows.Clear()
        dsChangeOfStatus = objChangeofStatus.pub_GetActivityStatusBySearch(StatusID, _
                                                                               EquipmentNo, _
                                                                               CustomerID, _
                                                                               intDepotID)

        dtTemp = dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS)

        If dtTemp.Rows.Count > 0 Then

            For Each dt As DataRow In dtTemp.Rows

                Dim COS As New ChangeOfStatusModel

                COS.EquipmentNo = dt.Item("EQPMNT_NO").ToString()
                COS.Type = dt.Item("EQPMNT_TYP_CD").ToString()
                COS.Customer = dt.Item("CSTMR_CD").ToString()
                COS.InDate = dt.Item("GTN_DT").ToString()
                COS.PreviousCargo = dt.Item("PRDCT_DSCRPTN_VC").ToString()
                COS.CurrentStatus = dt.Item("EQPMNT_STTS_CD").ToString()
                COS.CurrentStatusDate = dt.Item("ACTVTY_DT").ToString()
                COS.YardLocation = dt.Item("YRD_LCTN").ToString()
                COS.Remarks = dt.Item("RMRKS_VC").ToString()

                arraylist.Add(COS)

            Next

            SearchResult.SearchResult = arraylist
            SearchResult.Status = "Success"
        Else

            'SearchResult.SearchResult = arraylist
            SearchResult.Status = "No Records"
        End If





        Return SearchResult



    End Function



    <WebMethod(enableSession:=True)> _
     <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function UpdateStatus(ByVal StatusID As String, ByVal EquipmentNo As String, ByVal CustomerID As String, ByVal UserName As String, ByVal EquipmentListCOS As ArrayOfEquipmentListCOS) As SearchResult


        Try

       
        gateInMobile.DepotID(UserName)
        Dim objChangeofStatus As New ChangeOfStatus()
        Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
        Dim dtTemp As New DataTable
        Dim objActivityStatus As New ChangeOfStatus

        dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows.Clear()
        dsChangeOfStatus = objChangeofStatus.pub_GetActivityStatusBySearch(StatusID, _
                                                                               EquipmentNo, _
                                                                               CustomerID, _
                                                                               intDepotID)

        dtTemp = dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Copy()

        For Each arrayofCOS In EquipmentListCOS.ArrayOfEquipmentListCOS

                Dim dt() As DataRow = dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Select(String.Concat(ChangeOfStatusData.EQPMNT_NO, "='", arrayofCOS.EquipmentNo, "'"))

                If dt.Length > 0 Then

                    If arrayofCOS.Remarks <> "" Then
                        dt(0).Item("RMRKS_VC") = arrayofCOS.Remarks
                    End If

                    If arrayofCOS.YardLocation <> "" Then
                        dt(0).Item("YRD_LCTN") = arrayofCOS.YardLocation
                    End If

                    dt(0).Item("CHECKED") = CBool(arrayofCOS.Checked)
                    dt(0).Item("NEW_EQPMNT_STTS_CD") = arrayofCOS.NEW_EQPMNT_STTS_CD
                    dt(0).Item("NEW_ACTVTY_DT") = CDate(arrayofCOS.NEW_ACTVTY_DT)
                    dt(0).Item("EQPMNT_STTS_ID") = arrayofCOS.EQPMNT_STTS_ID

                    'Dim ss As ChangeOfStatusDataSet = dsChangeOfStatus
                Else

                    SearchResult.Status = "Equipment Not Found"

                    Return SearchResult

                End If

            
        Next

        Dim strModifiedby As String = objCommon.GetCurrentUserName()
        Dim datModifiedDate As String = objCommon.GetCurrentDate()
        Dim strCleaningFromCode As String = "ACN"
        Dim strCleaningToCode As String = "CLN"
        Dim str_014KeyValue As String = String.Empty
        Dim str_015KeyValue As String = String.Empty
        Dim str_016KeyValue As String = String.Empty
        Dim str_017KeyValue As String = String.Empty
        Dim str_018KeyValue As String = String.Empty
        Dim str_019KeyValue As String = String.Empty
        Dim intCleaningStatus1 As Integer = 0
        Dim intCleaningStatus2 As Integer = 0
        Dim intCondition As Integer = 0
        Dim intValveConditionId As Integer = 0

        Dim bv_strWfData As String = objCommon.GenerateWFData(83)

        Dim objCommonConfig As New ConfigSetting()

        str_014KeyValue = objCommonConfig.pub_GetConfigSingleValue("014", intDepotID)
        str_015KeyValue = objCommon.GetYardLocation()
        str_016KeyValue = objCommonConfig.pub_GetConfigSingleValue("016", intDepotID)
        str_017KeyValue = objCommonConfig.pub_GetConfigSingleValue("017", intDepotID)
        str_018KeyValue = objCommonConfig.pub_GetConfigSingleValue("018", intDepotID)
        str_019KeyValue = objCommonConfig.pub_GetConfigSingleValue("019", intDepotID)

        intCondition = CInt(objCommon.GetEnumID("EQUIPMENTCONDTN", str_016KeyValue))
        intValveConditionId = CInt(objCommon.GetEnumID("EQUIPMENTCONDTN", str_017KeyValue))

        intCleaningStatus1 = CInt(objCommon.GetEnumID("EQUIPMENTSTATUS", str_018KeyValue))
        intCleaningStatus2 = CInt(objCommon.GetEnumID("EQUIPMENTSTATUS2", str_019KeyValue))

            Dim result As Boolean = objActivityStatus.pub_UpdateChangeofStatus(dsChangeOfStatus, _
                                                       strModifiedby, _
                                                       CDate(datModifiedDate), _
                                                       intDepotID, _
                                                       bv_strWfData, _
                                                       strCleaningFromCode, _
                                                       strCleaningToCode, _
                                                       str_014KeyValue, _
                                                       str_015KeyValue, _
                                                       intCleaningStatus1, _
                                                       intCleaningStatus2, _
                                                       intCondition, _
                                                       intValveConditionId)

            'If result Then
            SearchResult.Status = "Success"
            'End If


            Return SearchResult

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            SearchResult.Status = ex.Message
            Return SearchResult
        End Try

    End Function


    <WebMethod(enableSession:=True)> _
     <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function currrentStatusList(ByVal UserName As String) As SearchResult
        gateInMobile.DepotID(UserName)
        Dim intDepotID As Integer = objCommon.GetDepotID()

        Dim query As String = "select [CURRENT_STATUS] ,[EQPMNT_STTS_ID] from [WF_CHANGE_OF_STATUS] " +
            " inner join [EQUIPMENT_STATUS] on [CURRENT_STATUS] = [EQPMNT_STTS_CD] " +
 "and [EQUIPMENT_STATUS].[DPT_ID] = [WF_CHANGE_OF_STATUS].[DPT_ID] " +
 "where  [WF_CHANGE_OF_STATUS].[DPT_ID] ='" + objCommon.GetDepotID() + "'order by CURRENT_STATUS asc"

        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)

        For Each dt As DataRow In dtPreaAdvice.Rows
            Dim CurrentStatusList As New CurrentStatusList

            CurrentStatusList.StatusName = dt.Item("CURRENT_STATUS").ToString()
            CurrentStatusList.StatusID = dt.Item("EQPMNT_STTS_ID").ToString()

            arraylist.Add(CurrentStatusList)
        Next

        SearchResult.SearchResult = arraylist
        SearchResult.Status = "Success"

        Return SearchResult
    End Function


    <WebMethod(enableSession:=True)> _
     <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function ToStatusList(ByVal UserName As String, ByVal CurrentStatus As String) As SearchResult


        gateInMobile.DepotID(UserName)
        Dim intDepotID As Integer = objCommon.GetDepotID()

        Dim query As String = "SELECT [TO_STATUS] from [WF_CHANGE_OF_STATUS] where DPT_ID ='" + objCommon.GetDepotID() + "' and [CURRENT_STATUS] ='" + CurrentStatus + "'"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)

        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)



        Dim arrayList1 As New ArrayList
        For Each dt As DataRow In dtPreaAdvice.Rows

            Dim query1 As String = "select EQPMNT_STTS_ID, EQPMNT_STTS_CD from [EQUIPMENT_STATUS] where EQPMNT_STTS_CD in (" + dt.Item("TO_STATUS") + ")"
            Dim ds1 As DataSet = conn.connection(query1)
            'conn.connection(query).Fill(ds)

            Dim dtPreaAdvice1 As DataTable
            dtPreaAdvice1 = ds1.Tables(0)


            For Each dt1 As DataRow In dtPreaAdvice1.Rows

                Dim tostusList As New TOStatusList

                tostusList.StatusName = dt1.Item("EQPMNT_STTS_CD").ToString()
                tostusList.StatusID = dt1.Item("EQPMNT_STTS_ID").ToString()

                arraylist.Add(tostusList)


            Next
        Next

        SearchResult.SearchResult = arraylist
        SearchResult.Status = "Success"
        Return SearchResult



    End Function

End Class