Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class LeakTestMobile
    Inherits System.Web.Services.WebService

    Dim conn As New Dropdown_C
    Dim arraylist As New ArrayList
    Dim LTEquipmentDD As New LTEquipmentDD
    Dim dsLeakTestData As New LeakTestDataSet
    Dim gateInMobile As New GateinMobile_C
    Dim dtLeakTestData As DataTable

    Dim LTEvalidation As New LTEvalidation
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)> _
    Public Function EquipmentList() As LTEquipmentDD


        Try



            Dim leaktest As String = "select [EQPMNT_NO],GTN_DT,EQPMNT_STTS_CD,EQPMNT_TYP_CD,CSTMR_CD,DPT_ID,[CSTMR_ID],YRD_LCTN from [V_ACTIVITY_STATUS] where [ACTV_BT] =1"
            Dim leaktestds As DataSet = conn.connection(leaktest)
            'conn.connection(leaktest).Fill(leaktestds)
            'ds.Tables(Table)
            'ds.Tables()
            'Return ds
            Dim leaktestdt As DataTable
            leaktestdt = leaktestds.Tables(0)

            For Each dt As DataRow In leaktestdt.Rows

                Dim leaktestDD As New LeakTestModel

                'Dim querysssss As String = "select [ACTVTY_NAM] from [ACTIVITY] where [ACTVTY_ID] = '" + dt.Item("ACTVTY_ID") + "'"
                leaktestDD.EQPMNT_NO = dt.Item("EQPMNT_NO").ToString()
                leaktestDD.InDate = dt.Item("GTN_DT").ToString()
                leaktestDD.CurrentStatus = dt.Item("EQPMNT_STTS_CD").ToString()
                leaktestDD.Type = dt.Item("EQPMNT_TYP_CD").ToString()
                leaktestDD.Customer = dt.Item("CSTMR_CD").ToString()
                leaktestDD.Depot = dt.Item("DPT_ID").ToString()
                leaktestDD.YrdLocation = dt.Item("YRD_LCTN").ToString()

                arraylist.Add(leaktestDD)


            Next

            LTEquipmentDD.status = "Success"
            LTEquipmentDD.arrayOfLTM = arraylist
            LTEquipmentDD.statusText = HttpContext.Current.Response.StatusDescription
            LTEquipmentDD.statusCode = HttpContext.Current.Response.StatusCode
            Return LTEquipmentDD

        Catch ex As Exception


            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)

            LTEquipmentDD.status = ex.Message
            LTEquipmentDD.statusText = HttpContext.Current.Response.StatusDescription
            LTEquipmentDD.statusCode = HttpContext.Current.Response.StatusCode

            Return LTEquipmentDD


        End Try




    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function EquipmentValidation(ByVal equipmentNo As String, ByVal UserName As String) As LTEvalidation

        Dim blndsValid As Boolean
        gateInMobile.DepotID(UserName)
        Dim ObjLeakTest As New LeakTest()
        Dim objCommon As New CommonData
        dsLeakTestData = ObjLeakTest.pub_GetLeakTestByActiveBit(objCommon.GetDepotID())
        dtLeakTestData = dsLeakTestData.Tables(LeakTestData._LEAK_TEST)
        Dim intResultIndex() As System.Data.DataRow = dtLeakTestData.Select(String.Concat(LeakTestData.EQPMNT_NO, "='", equipmentNo, "' "))

        Dim strExistEquipment As String = ""
        If intResultIndex.Length > 0 Then
            blndsValid = False
        Else
            blndsValid = True
        End If


        If blndsValid = True Then
            'If bv_strRowstate = "Added" Or bv_strRowstate = "Modified" Then
            'Dim objLeakTest As New LeakTest
            'Dim objCommon As New CommonData
            blndsValid = ObjLeakTest.pub_ValidateEquipmentNoByDepotID(equipmentNo, CInt(objCommon.GetDepotID()))
            'End If
        End If


        If blndsValid = True Then
            LTEvalidation.status = "Success"

        Else
            LTEvalidation.status = "This Equipment No already Exist"
        End If

        LTEvalidation.statusText = HttpContext.Current.Response.StatusDescription
        LTEvalidation.statusCode = HttpContext.Current.Response.StatusCode


        Return LTEvalidation

    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function LeakTestList(ByVal UserName As String) As LTEquipmentDD

        gateInMobile.DepotID(UserName)
        Dim ObjLeakTest As New LeakTest()
        Dim objCommon As New CommonData
        dsLeakTestData = ObjLeakTest.pub_GetLeakTestByActiveBit(objCommon.GetDepotID())
        dtLeakTestData = dsLeakTestData.Tables(LeakTestData._LEAK_TEST)

        For Each dt As DataRow In dtLeakTestData.Rows
            Dim LTList As New LTList
            LTList.LK_TST_ID = dt.Item("LK_TST_ID").ToString()
            LTList.EQPMNT_NO = dt.Item("EQPMNT_NO").ToString()
            LTList.GI_TRNSCTN_NO = dt.Item("GI_TRNSCTN_NO").ToString()
            LTList.ESTMT_NO = dt.Item("ESTMT_NO").ToString()
            LTList.TST_DT = dt.Item("TST_DT").ToString()
            LTList.RLF_VLV_SRL_1 = dt.Item("RLF_VLV_SRL_1").ToString()
            LTList.RLF_VLV_SRL_2 = dt.Item("RLF_VLV_SRL_2").ToString()
            LTList.PG_1 = dt.Item("PG_1").ToString()
            LTList.PG_2 = dt.Item("PG_2").ToString()
            LTList.RVSN_NO = dt.Item("RVSN_NO").ToString()
            LTList.LST_GNRTD_BY = dt.Item("LST_GNRTD_BY").ToString()
            LTList.LST_GNRTD_DT = dt.Item("LST_GNRTD_DT").ToString()
            LTList.LTST_RPRT_NO = dt.Item("LTST_RPRT_NO").ToString()
            LTList.NO_OF_TMS_GNRTD = dt.Item("NO_OF_TMS_GNRTD").ToString()
            LTList.SHLL_TST_BT = dt.Item("SHLL_TST_BT").ToString()
            LTList.STM_TB_TST_BT = dt.Item("STM_TB_TST_BT").ToString()
            LTList.RMRKS_VC = dt.Item("RMRKS_VC").ToString()
            LTList.ACTV_BT = dt.Item("ACTV_BT").ToString()
            LTList.CRTD_BY = dt.Item("CRTD_BY").ToString()
            LTList.CRTD_DT = dt.Item("CRTD_DT").ToString()
            LTList.MDFD_BY = dt.Item("MDFD_BY").ToString()
            LTList.MDFD_DT = dt.Item("MDFD_DT").ToString()
            LTList.EQPMNT_TYP_ID = dt.Item("EQPMNT_TYP_ID").ToString()
            LTList.EQPMNT_TYP_CD = dt.Item("EQPMNT_TYP_CD").ToString()
            LTList.EQPMNT_CD_ID = dt.Item("EQPMNT_CD_ID").ToString()
            LTList.EQPMNT_CD_CD = dt.Item("EQPMNT_CD_CD").ToString()
            LTList.EQPMNT_STTS_ID = dt.Item("EQPMNT_STTS_ID").ToString()
            LTList.EQPMNT_STTS_CD = dt.Item("EQPMNT_STTS_CD").ToString()
            LTList.CHECKED = dt.Item("CHECKED").ToString()
            LTList.GTN_DT = dt.Item("GTN_DT").ToString()
            LTList.CSTMR_ID = dt.Item("CSTMR_ID").ToString()
            LTList.CSTMR_CD = dt.Item("CSTMR_CD").ToString()
            LTList.DPT_ID = dt.Item("DPT_ID").ToString()
            LTList.SHL_TST = dt.Item("SHL_TST").ToString()
            LTList.STM_TB_TST = dt.Item("STM_TB_TST").ToString()
            LTList.YRD_LCTN = dt.Item("YRD_LCTN").ToString()


            arraylist.Add(LTList)


        Next

        LTEquipmentDD.status = "Success"
        LTEquipmentDD.arrayOfLTM = arraylist
        LTEquipmentDD.statusText = HttpContext.Current.Response.StatusDescription
        LTEquipmentDD.statusCode = HttpContext.Current.Response.StatusCode


        Return LTEquipmentDD

    End Function



    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function filter(ByVal filterType As String, ByVal filterCondition As String, ByVal filterValue As String, ByVal UserName As String, ByVal Mode As String) As ListModel

        Dim objCommonData As New CommonData
        gateInMobile.DepotID(UserName)
        Dim listVlaues As New ListModel
        Dim arraylist As New ArrayList
        Dim filterNewConditon As String
        Dim ds As DataSet
        Dim dtPreaAdvice As DataTable
        Dim query As String
        If filterCondition = "Similar" Or filterCondition = "Contains" Then
            filterNewConditon = "LIKE"
        Else
            filterNewConditon = "Not LIKE"
        End If


        'gateInMobile.DepotID(UserName)
        Dim ObjLeakTest As New LeakTest()
        Dim objCommon As New CommonData
        dsLeakTestData = ObjLeakTest.pub_GetLeakTestByActiveBit(objCommon.GetDepotID())
        dtLeakTestData = dsLeakTestData.Tables(LeakTestData._LEAK_TEST)


        If filterCondition = "Equals" Then



            For Each dt As DataRow In dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(filterType, "='", filterValue, "'"))

                Dim filteredvalues As New FilteredValues
                filteredvalues.Values = dt.Item(filterType).ToString()

                arraylist.Add(filteredvalues)

            Next

        Else


            For Each dt As DataRow In dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(filterType, " ", filterNewConditon, " '%", filterValue, "%'"))

                Dim filteredvalues As New FilteredValues
                filteredvalues.Values = dt.Item(filterType).ToString()

                arraylist.Add(filteredvalues)

            Next


        End If



        listVlaues.ListGateInss = arraylist
        listVlaues.status = "Success"

        Return listVlaues

    End Function



    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function SearchList(ByVal UserName As String, ByVal SearchValues As SearchValues, ByVal filterType As String, ByVal Mode As String) As LTEquipmentDD

        Try

            Dim RC As New ArrayList
            gateInMobile.DepotID(UserName)
            Dim ObjLeakTest As New LeakTest()
            Dim objCommon As New CommonData
            dsLeakTestData = ObjLeakTest.pub_GetLeakTestByActiveBit(objCommon.GetDepotID())
            dtLeakTestData = dsLeakTestData.Tables(LeakTestData._LEAK_TEST)







            For Each dd In SearchValues.SearchValues

                ' Dim dt() As DataRow = dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(filterType, "='", dd.values, "'"))

                For Each dr1 As DataRow In dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(filterType, "='", dd.values, "'"))
                    Dim LTList As New LTList
                    LTList.LK_TST_ID = dr1.Item("LK_TST_ID").ToString()
                    LTList.EQPMNT_NO = dr1.Item("EQPMNT_NO").ToString()
                    LTList.GI_TRNSCTN_NO = dr1.Item("GI_TRNSCTN_NO").ToString()
                    LTList.ESTMT_NO = dr1.Item("ESTMT_NO").ToString()
                    LTList.TST_DT = dr1.Item("TST_DT").ToString()
                    LTList.RLF_VLV_SRL_1 = dr1.Item("RLF_VLV_SRL_1").ToString()
                    LTList.RLF_VLV_SRL_2 = dr1.Item("RLF_VLV_SRL_2").ToString()
                    LTList.PG_1 = dr1.Item("PG_1").ToString()
                    LTList.PG_2 = dr1.Item("PG_2").ToString()
                    LTList.RVSN_NO = dr1.Item("RVSN_NO").ToString()
                    LTList.LST_GNRTD_BY = dr1.Item("LST_GNRTD_BY").ToString()
                    LTList.LST_GNRTD_DT = dr1.Item("LST_GNRTD_DT").ToString()
                    LTList.LTST_RPRT_NO = dr1.Item("LTST_RPRT_NO").ToString()
                    LTList.NO_OF_TMS_GNRTD = dr1.Item("NO_OF_TMS_GNRTD").ToString()
                    LTList.SHLL_TST_BT = dr1.Item("SHLL_TST_BT").ToString()
                    LTList.STM_TB_TST_BT = dr1.Item("STM_TB_TST_BT").ToString()
                    LTList.RMRKS_VC = dr1.Item("RMRKS_VC").ToString()
                    LTList.ACTV_BT = dr1.Item("ACTV_BT").ToString()
                    LTList.CRTD_BY = dr1.Item("CRTD_BY").ToString()
                    LTList.CRTD_DT = dr1.Item("CRTD_DT").ToString()
                    LTList.MDFD_BY = dr1.Item("MDFD_BY").ToString()
                    LTList.MDFD_DT = dr1.Item("MDFD_DT").ToString()
                    LTList.EQPMNT_TYP_ID = dr1.Item("EQPMNT_TYP_ID").ToString()
                    LTList.EQPMNT_TYP_CD = dr1.Item("EQPMNT_TYP_CD").ToString()
                    LTList.EQPMNT_CD_ID = dr1.Item("EQPMNT_CD_ID").ToString()
                    LTList.EQPMNT_CD_CD = dr1.Item("EQPMNT_CD_CD").ToString()
                    LTList.EQPMNT_STTS_ID = dr1.Item("EQPMNT_STTS_ID").ToString()
                    LTList.EQPMNT_STTS_CD = dr1.Item("EQPMNT_STTS_CD").ToString()
                    LTList.CHECKED = dr1.Item("CHECKED").ToString()
                    LTList.GTN_DT = dr1.Item("GTN_DT").ToString()
                    LTList.CSTMR_ID = dr1.Item("CSTMR_ID").ToString()
                    LTList.CSTMR_CD = dr1.Item("CSTMR_CD").ToString()
                    LTList.DPT_ID = dr1.Item("DPT_ID").ToString()
                    LTList.SHL_TST = dr1.Item("SHL_TST").ToString()
                    LTList.STM_TB_TST = dr1.Item("STM_TB_TST").ToString()
                    LTList.YRD_LCTN = dr1.Item("YRD_LCTN").ToString()


                    arraylist.Add(LTList)




                Next

            Next

            LTEquipmentDD.status = "Success"
            LTEquipmentDD.arrayOfLTM = arraylist
            LTEquipmentDD.statusText = HttpContext.Current.Response.StatusDescription
            LTEquipmentDD.statusCode = HttpContext.Current.Response.StatusCode


            Return LTEquipmentDD




        Catch ex As Exception


            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)

            LTEquipmentDD.status = "Success"
            LTEquipmentDD.arrayOfLTM = arraylist
            LTEquipmentDD.statusText = HttpContext.Current.Response.StatusDescription
            LTEquipmentDD.statusCode = HttpContext.Current.Response.StatusCode


            Return LTEquipmentDD
        End Try

    End Function



    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function InsertLeakTest(ByVal LK_TST_ID As String, ByVal EQPMNT_NO As String, ByVal TST_DT As String, ByVal SHLL_TST_BT As Boolean, ByVal STM_TB_TST_BT As Boolean, ByVal EQPMNT_TYP_CD As String,
                                   ByVal EQPMNT_STTS_CD As String, ByVal CHECKED As Boolean, ByVal GTN_DT As String, ByVal CSTMR_CD As String, ByVal UserName As String,
                                   ByVal RLF_VLV_SRL_1 As String,
                                   ByVal RLF_VLV_SRL_2 As String,
                                   ByVal PG_1 As String,
                                   ByVal PG_2 As String,
                                   ByVal RMRKS_VC As String
                                  ) As LTEvalidation



        gateInMobile.DepotID(UserName)
        Dim ObjLeakTest As New LeakTest()
        Dim objCommon As New CommonData
        dsLeakTestData = ObjLeakTest.pub_GetLeakTestByActiveBit(objCommon.GetDepotID())
        dtLeakTestData = dsLeakTestData.Tables(LeakTestData._LEAK_TEST)
        Dim datModifiedDate As String = objCommon.GetCurrentDate()
        Dim intDepotId As Integer = CommonWeb.iInt(objCommon.GetDepotID())

        Try



            Dim dr1() As DataRow = dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(GateinData.EQPMNT_NO, "='", EQPMNT_NO, "'"))

            If dr1.Length > 0 Then

                dr1(0).Item("LK_TST_ID") = LK_TST_ID
                dr1(0).Item("EQPMNT_NO") = EQPMNT_NO
                dr1(0).Item("TST_DT") = TST_DT
                dr1(0).Item("SHLL_TST_BT") = SHLL_TST_BT
                dr1(0).Item("STM_TB_TST_BT") = STM_TB_TST_BT
                dr1(0).Item("EQPMNT_TYP_CD") = EQPMNT_TYP_CD
                dr1(0).Item("EQPMNT_STTS_CD") = EQPMNT_STTS_CD
                dr1(0).Item("CHECKED") = CHECKED
                dr1(0).Item("GTN_DT") = GTN_DT
                dr1(0).Item("CSTMR_CD") = CSTMR_CD


                dr1(0).Item("RLF_VLV_SRL_1") = RLF_VLV_SRL_1
                dr1(0).Item("RLF_VLV_SRL_2") = RLF_VLV_SRL_2
                dr1(0).Item("PG_1") = PG_1
                dr1(0).Item("PG_2") = PG_2
                dr1(0).Item("RMRKS_VC") = RMRKS_VC

            Else

                Dim drLeakTest As DataRow
                drLeakTest = dsLeakTestData.Tables(LeakTestData._LEAK_TEST).NewRow()
                drLeakTest.Item("LK_TST_ID") = CommonWeb.GetNextIndex(dsLeakTestData.Tables(LeakTestData._LEAK_TEST), LeakTestData.LK_TST_ID)
                drLeakTest.Item("EQPMNT_NO") = EQPMNT_NO
                drLeakTest.Item("TST_DT") = TST_DT
                drLeakTest.Item("SHLL_TST_BT") = SHLL_TST_BT
                drLeakTest.Item("STM_TB_TST_BT") = STM_TB_TST_BT
                drLeakTest.Item("EQPMNT_TYP_CD") = EQPMNT_TYP_CD
                drLeakTest.Item("EQPMNT_STTS_CD") = EQPMNT_STTS_CD
                drLeakTest.Item("CHECKED") = CHECKED
                drLeakTest.Item("GTN_DT") = GTN_DT
                drLeakTest.Item("CSTMR_CD") = CSTMR_CD

                drLeakTest.Item("RLF_VLV_SRL_1") = RLF_VLV_SRL_1
                drLeakTest.Item("RLF_VLV_SRL_2") = RLF_VLV_SRL_2
                drLeakTest.Item("PG_1") = PG_1
                drLeakTest.Item("PG_2") = PG_2
                drLeakTest.Item("RMRKS_VC") = RMRKS_VC


                dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Rows.Add(drLeakTest)

            End If


            Dim result As Boolean = ObjLeakTest.pub_CreateLeakTest(dsLeakTestData, _
                                               intDepotId, _
                                               UserName, _
                                               datModifiedDate)

            If result Then
                LTEvalidation.status = "Updated Successfully"
                LTEvalidation.statusText = HttpContext.Current.Response.StatusDescription
                LTEvalidation.statusCode = HttpContext.Current.Response.StatusCode
            End If

            Return LTEvalidation
        Catch ex As Exception


            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
            LTEvalidation.status = ex.Message
            LTEvalidation.statusText = HttpContext.Current.Response.StatusDescription
            LTEvalidation.statusCode = HttpContext.Current.Response.StatusCode
            Return LTEvalidation
        End Try




    End Function

    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function RevisionList(ByVal equipmentNo As String, ByVal GITransactionNo As String, ByVal UserName As String) As LTEquipmentDD


        gateInMobile.DepotID(UserName)
        Dim objCommon As New CommonData
        Dim objLeakTestDetail As New LeakTest
        Dim dsLeakTestData As New LeakTestDataSet
        dsLeakTestData = objLeakTestDetail.pub_GetLeakTestRevisionHistory(equipmentNo, GITransactionNo, objCommon.GetDepotID())
        Dim dtLeakTestData As DataTable = dsLeakTestData.Tables(LeakTestData._LEAK_TEST)

        For Each dt As DataRow In dtLeakTestData.Rows

            Dim LTRevisionList As New LTRevisionList

            LTRevisionList.LK_TST_ID = dt.Item("LK_TST_ID").ToString()
            LTRevisionList.EQPMNT_NO = dt.Item("EQPMNT_NO").ToString()
            LTRevisionList.GI_TRNSCTN_NO = dt.Item("GI_TRNSCTN_NO").ToString()
            LTRevisionList.TST_DT = dt.Item("TST_DT").ToString()
            LTRevisionList.RLF_VLV_SRL_1 = dt.Item("RLF_VLV_SRL_1").ToString()
            LTRevisionList.RLF_VLV_SRL_2 = dt.Item("RLF_VLV_SRL_2").ToString()
            LTRevisionList.PG_1 = dt.Item("PG_1").ToString()
            LTRevisionList.PG_2 = dt.Item("PG_2").ToString()
            LTRevisionList.RVSN_NO = dt.Item("RVSN_NO").ToString()
            LTRevisionList.LST_GNRTD_BY = dt.Item("LST_GNRTD_BY").ToString()
            LTRevisionList.LST_GNRTD_DT = dt.Item("LST_GNRTD_DT").ToString()
            LTRevisionList.LTST_RPRT_NO = dt.Item("LTST_RPRT_NO").ToString()
            LTRevisionList.SHLL_TST_BT = dt.Item("SHLL_TST_BT").ToString()
            LTRevisionList.STM_TB_TST_BT = dt.Item("STM_TB_TST_BT").ToString()
            LTRevisionList.RMRKS_VC = dt.Item("RMRKS_VC").ToString()

            arraylist.Add(LTRevisionList)


        Next


        LTEquipmentDD.status = "Success"
        LTEquipmentDD.arrayOfLTM = arraylist
        LTEquipmentDD.statusText = HttpContext.Current.Response.StatusDescription
        LTEquipmentDD.statusCode = HttpContext.Current.Response.StatusCode


        Return LTEquipmentDD

    End Function


    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function GenerateDocument(ByVal LK_TST_ID As String, ByVal EQPMNT_NO As String, ByVal TST_DT As String, ByVal SHLL_TST_BT As Boolean, ByVal STM_TB_TST_BT As Boolean, ByVal EQPMNT_TYP_CD As String,
                                   ByVal EQPMNT_STTS_CD As String, ByVal CHECKED As Boolean, ByVal GTN_DT As String, ByVal CSTMR_CD As String, ByVal UserName As String) As LTEquipmentDD

        gateInMobile.DepotID(UserName)
        Try
            Dim objLeakTest As New LeakTest
            Dim objCommon As New CommonData
            Dim dtLeakTest As New DataTable
            Dim objCommonUI As New CommonUI
            Dim dsDocument As New LeakTestDataSet
            Dim drLeakTest As DataRow
            'Dim CompareCustmrId As Integer
            'Dim CustomerId As Integer
            Dim dsDepot As DataSet
            Dim count As Integer = 0
            Dim intDepotId As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strCurrentUserName As String = objCommon.GetCurrentUserName()
            Dim datModifiedDate As String = objCommon.GetCurrentDate()
            dsLeakTestData = objLeakTest.pub_GetLeakTestByActiveBit(objCommon.GetDepotID())
            dtLeakTestData = dsLeakTestData.Tables(LeakTestData._LEAK_TEST)
            'Dim drALeakTest As DataRow()
            Dim dr1() As DataRow = dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(GateinData.EQPMNT_NO, "='", EQPMNT_NO, "'"))
            If dr1.Length = 0 Then
                'Record must be submitted before generating document
                LTEquipmentDD.status = "Record must be submitted before generating document"
                Return LTEquipmentDD
                Exit Function

            Else
                dr1(0).Item("LK_TST_ID") = LK_TST_ID
                dr1(0).Item("EQPMNT_NO") = EQPMNT_NO
                dr1(0).Item("TST_DT") = TST_DT
                dr1(0).Item("SHLL_TST_BT") = SHLL_TST_BT
                dr1(0).Item("STM_TB_TST_BT") = STM_TB_TST_BT
                dr1(0).Item("EQPMNT_TYP_CD") = EQPMNT_TYP_CD
                dr1(0).Item("EQPMNT_STTS_CD") = EQPMNT_STTS_CD
                dr1(0).Item("CHECKED") = CHECKED
                dr1(0).Item("GTN_DT") = GTN_DT
                dr1(0).Item("CSTMR_CD") = CSTMR_CD

            End If




            'drALeakTest = dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(LeakTestData.CHECKED & "='True'")
            'If Not drALeakTest.Length > 0 Then
            '    pub_SetCallbackStatus(False)
            '    pub_SetCallbackError("Please Select Atleast One Equipment.")
            '    Exit Function
            'Else
            '    For Each dr As DataRow In dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "=", "True"))
            '        CustomerId = dr(LeakTestData.CSTMR_ID)
            '        If (count > 0) And (CustomerId <> CompareCustmrId) Then
            '            pub_SetCallbackStatus(False)
            '            pub_SetCallbackError("Leak Test document cannot be generated for multiple customers.")
            '            Exit Function
            '        End If
            '        CompareCustmrId = dr(LeakTestData.CSTMR_ID)
            '        count = count + 1
            '    Next
            'End If

            objLeakTest.pub_UpdateGenerationDetails(dsLeakTestData, _
                                                    intDepotId, _
                                                    strCurrentUserName, _
                                                    datModifiedDate)
            dsLeakTestData.AcceptChanges()
            'CacheData(LEAK_TEST, dsLeakTestData)
            dtLeakTest = dsDocument.Tables(LeakTestData._REPORT_LEAK_TEST).Clone()

            For Each dr As DataRow In dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "=", "True"))
                drLeakTest = dtLeakTest.NewRow()
                drLeakTest(LeakTestData.LK_TST_ID) = dr(LeakTestData.LK_TST_ID)
                drLeakTest(LeakTestData.EQPMNT_NO) = dr(LeakTestData.EQPMNT_NO)
                drLeakTest(LeakTestData.GI_TRNSCTN_NO) = dr(LeakTestData.GI_TRNSCTN_NO)
                drLeakTest(LeakTestData.EQPMNT_TYP_CD) = dr(LeakTestData.EQPMNT_TYP_CD)
                drLeakTest(LeakTestData.LTST_RPRT_NO) = dr(LeakTestData.LTST_RPRT_NO)
                drLeakTest(LeakTestData.CSTMR_CD) = dr(LeakTestData.CSTMR_CD)
                drLeakTest(LeakTestData.CSTMR_ID) = dr(LeakTestData.CSTMR_ID)
                drLeakTest(LeakTestData.TST_DT) = dr(LeakTestData.TST_DT)
                drLeakTest(LeakTestData.SHLL_TST_BT) = dr(LeakTestData.SHLL_TST_BT)
                drLeakTest(LeakTestData.STM_TB_TST_BT) = dr(LeakTestData.STM_TB_TST_BT)
                drLeakTest(LeakTestData.RLF_VLV_SRL_1) = dr(LeakTestData.RLF_VLV_SRL_1)
                drLeakTest(LeakTestData.RLF_VLV_SRL_2) = dr(LeakTestData.RLF_VLV_SRL_2)
                drLeakTest(LeakTestData.PG_1) = dr(LeakTestData.PG_1)
                drLeakTest(LeakTestData.PG_2) = dr(LeakTestData.PG_2)
                drLeakTest(LeakTestData.RMRKS_VC) = dr(LeakTestData.RMRKS_VC)
                drLeakTest(LeakTestData.MDFD_BY) = dr(LeakTestData.MDFD_BY)
                dtLeakTest.Rows.Add(drLeakTest)
            Next
            dsDocument.Tables(LeakTestData._LEAK_TEST).Clear()
            dsDocument.Tables(LeakTestData._LEAK_TEST).Merge(dtLeakTest)
            'Depot Details
            dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
            dsDocument.Merge(dsDepot.Tables(CommonUIData._DEPOT))
            'Customer Details
            Dim dsCustomer As New LeakTestDataSet
            dsCustomer.Tables(GateinData._CUSTOMER).Rows.Clear()
            'Multilocation
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                dsCustomer = objLeakTest.pub_GetCustomerDetail(CInt(objCommon.GetHeadQuarterID()), dtLeakTest.Rows(0).Item(LeakTestData.CSTMR_ID))
            Else
                dsCustomer = objLeakTest.pub_GetCustomerDetail(intDepotId, dtLeakTest.Rows(0).Item(LeakTestData.CSTMR_ID))
            End If
            dsDocument.Tables(LeakTestData._CUSTOMER).Clear()
            dsDocument.Merge(dsCustomer.Tables(LeakTestData._CUSTOMER))

            'CacheData(LEAK_TEST_DOCUMENT, dsDocument)
            'pub_SetCallbackStatus(True)

            Dim LTDocument As New LTDocument
            Dim dt() As DataRow = dsDocument.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(GateinData.EQPMNT_NO, "='", EQPMNT_NO, "'"))

            LTDocument.LK_TST_ID = dt(0).Item("LK_TST_ID").ToString()
            LTDocument.EQPMNT_NO = dt(0).Item("EQPMNT_NO").ToString()
            LTDocument.GI_TRNSCTN_NO = dt(0).Item("GI_TRNSCTN_NO").ToString()
            LTDocument.EQPMNT_TYP_CD = dt(0).Item("EQPMNT_TYP_CD").ToString()
            LTDocument.LTST_RPRT_NO = dt(0).Item("LTST_RPRT_NO").ToString()
            LTDocument.CSTMR_CD = dt(0).Item("CSTMR_CD").ToString()
            LTDocument.CSTMR_ID = dt(0).Item("CSTMR_ID").ToString()
            LTDocument.TST_DT = dt(0).Item("TST_DT").ToString()
            LTDocument.SHLL_TST_BT = dt(0).Item("SHLL_TST_BT").ToString()
            LTDocument.STM_TB_TST_BT = dt(0).Item("STM_TB_TST_BT").ToString()
            LTDocument.RLF_VLV_SRL_1 = dt(0).Item("RLF_VLV_SRL_1").ToString()
            LTDocument.RLF_VLV_SRL_2 = dt(0).Item("RLF_VLV_SRL_2").ToString()
            LTDocument.PG_1 = dt(0).Item("PG_1").ToString()

            LTDocument.PG_2 = dt(0).Item("PG_2").ToString()
            LTDocument.RMRKS_VC = dt(0).Item("RMRKS_VC").ToString()

            LTDocument.MDFD_BY = dt(0).Item("MDFD_BY").ToString()
            Dim dt1 As DataRow = dsDocument.Tables(LeakTestData._CUSTOMER).Rows(0)
            LTDocument.CSTMR_NAM = dt1.Item("CSTMR_NAM").ToString()
            LTDocument.CSTMR_CRRNCY_ID = dt1.Item("CSTMR_CRRNCY_ID").ToString()
            LTDocument.CNTCT_PRSN_NAM = dt1.Item("CNTCT_PRSN_NAM").ToString()
            LTDocument.CNTCT_ADDRSS = dt1.Item("CNTCT_ADDRSS").ToString()
            LTDocument.BLLNG_ADDRSS = dt1.Item("BLLNG_ADDRSS").ToString()
            LTDocument.ZP_CD = dt1.Item("ZP_CD").ToString()
            LTDocument.PHN_NO = dt1.Item("PHN_NO").ToString()
            LTDocument.FX_NO = dt1.Item("FX_NO").ToString()
            LTDocument.RPRTNG_EML_ID = dt1.Item("RPRTNG_EML_ID").ToString()
            LTDocument.INVCNG_EML_ID = dt1.Item("INVCNG_EML_ID").ToString()
            LTDocument.RPR_TCH_EML_ID = dt1.Item("RPR_TCH_EML_ID").ToString()
            LTDocument.BLK_EML_FRMT_ID = dt1.Item("BLK_EML_FRMT_ID").ToString()

            arraylist.Add(LTDocument)


            LTEquipmentDD.status = "Success"
            LTEquipmentDD.arrayOfLTM = arraylist
            LTEquipmentDD.statusText = HttpContext.Current.Response.StatusDescription
            LTEquipmentDD.statusCode = HttpContext.Current.Response.StatusCode


            Return LTEquipmentDD

        Catch ex As Exception
            'pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
            LTEquipmentDD.status = ex.Message
            'LTEquipmentDD.arrayOfLTM = arraylist
            LTEquipmentDD.statusText = HttpContext.Current.Response.StatusDescription
            LTEquipmentDD.statusCode = HttpContext.Current.Response.StatusCode


            Return LTEquipmentDD
        End Try

    End Function
End Class