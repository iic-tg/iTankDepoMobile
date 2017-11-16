Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Web.HttpContext

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Login
    Inherits System.Web.Services.WebService

    Dim conn As New Dropdown_C

    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function Login(ByVal Credentials As credentails) As Message


        Dim loginCallFunc As New Login_C

        'Dim rediect As New System.Web.HttpResponse

        'Dim dd As New HttpResponse
        'HttpContext.Current.Response.Redirect("Default.aspx?Password=" + Password)
        Dim returnValue As Message = loginCallFunc.LoginReturn(Credentials.Username, Credentials.Password)
        'HttpResponse.Redirect("../../LoginMobile.aspx?Password=" + Password)
        'window.location.href = "../../LoginMobile.aspx?Password=" + Password + ""
        'HttpContext.Current.Response.Redirect("../../LoginMobile.aspx?Password=" + Password + "" & DateTime.Now.ToFileTime())
        Return returnValue

    End Function

    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function RoleDetails(ByVal RL_ID As String, ByVal UserName As String
                                ) As RoleDetails



        Dim Auth() As String = HttpContext.Current.Request.Headers.GetValues("Authorization")
        Dim RoDet As New RoleDetails

        ' If HttpRuntime.Cache(Auth.GetValue(0)) IsNot Nothing Then



        Dim arr As New ArrayList
        Dim query As String = "select * from [ROLE_RIGHT] where [ACTVTY_ID] IN (82,83,90,84,148,170,171,87,92,93,88,97,108) and RL_ID='" + RL_ID + "'"
        Dim ds As DataSet = conn.connection(query)

        'ds.Tables(Table)
        'ds.Tables()
        'Return ds



        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)

        For Each dt As DataRow In dtPreaAdvice.Rows
            Dim autori As New Authorization
            ' Dim ss As Integer = dt.ItemArray(2)
            Dim ddd As String = dt.Item("ACTVTY_ID")
            'Dim querysssss As String = "select [ACTVTY_NAM] from [ACTIVITY] where [ACTVTY_ID] = '" + dt.Item("ACTVTY_ID") + "'"
            Dim quekhhugry As String = "select [ACTVTY_NAM] from [ACTIVITY] where [ACTVTY_ID] = '" + ddd + "'"
            Dim ds1 As DataSet = conn.connection(quekhhugry)
            'conn.connection(quekhhugry).Fill(ds1)
            Dim dtPreaAdvice1 As DataTable
            dtPreaAdvice1 = ds1.Tables(0)



            autori.ActivityId = dt.Item("ACTVTY_ID").ToString()
            autori.AtivityName = dtPreaAdvice1.Rows(0).Item("ACTVTY_NAM").ToString()
            autori.Add = dt.Item("CRT_BT").ToString()
            autori.View = dt.Item("VW_BT").ToString()
            autori.Edit = dt.Item("EDT_BT").ToString()

            arr.Add(autori)

        Next


        Dim dsGateInData As New GateinDataSet
        Dim dsGateOut As New GateOutDataSet
        Dim dsCleaning As New DataSet
        Dim dsInspection As New DataSet
        Dim dsHeating As New HeatingDataSet
        Dim dsLeakTest As New LeakTestDataSet

        Dim dsRepairEstimateCount As New DataSet
        Dim dsRepairApprovalCount As New DataSet
        Dim dsRepairCompletionCount As New DataSet
        Dim dsSurrveyCompletionCount As New DataSet
        Dim dsRepairCount As New DataSet


        Dim GateinMobile_C As New GateinMobile_C
        dsGateInData = GateinMobile_C.PreAdvice(UserName, "new")

        Dim CountMobile As New CountMobile
        dsGateOut = CountMobile.GateoutCount(UserName)
        dsCleaning = CountMobile.CleaningCount(UserName)
        dsInspection = CountMobile.InspectionCount(UserName)
        dsHeating = CountMobile.HeatingCount(UserName)
        dsLeakTest = CountMobile.LeakTestCount(UserName)
        dsRepairEstimateCount = CountMobile.RepairEstimateCount(UserName)
        dsRepairApprovalCount = CountMobile.RepairApprovalCount(UserName)
        dsRepairCompletionCount = CountMobile.RepairCompletionCount(UserName)
        dsSurrveyCompletionCount = CountMobile.SurveyCompletionCount(UserName)
        dsRepairCount = CountMobile.RepairCount(UserName)


        RoDet.Status = "Success"
        RoDet.RL_ID = RL_ID
        RoDet.RoleDetails = arr
        RoDet.GateinCount = dsGateInData.Tables(GateinData._V_GATEIN).Rows.Count
        RoDet.GateoutCount = dsGateOut.Tables(GateOutData._V_GATEOUT).Rows.Count
        RoDet.CleaningCount = dsCleaning.Tables(0).Rows.Count
        RoDet.InspectionCount = dsInspection.Tables(0).Rows.Count
        RoDet.HeatingCount = dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Rows.Count
        RoDet.LeakTestCount = dsLeakTest.Tables(LeakTestData._LEAK_TEST).Rows.Count
        RoDet.RepairEstimateCount = dsRepairEstimateCount.Tables(0).Rows.Count
        RoDet.RepairApprovalCount = dsRepairApprovalCount.Tables(0).Rows.Count
        RoDet.RepairCompletionCount = dsRepairCompletionCount.Tables(0).Rows.Count
        RoDet.SurveyCompletionCount = dsSurrveyCompletionCount.Tables(0).Rows.Count
        RoDet.RepairCount = RoDet.RepairEstimateCount + RoDet.RepairApprovalCount + RoDet.RepairCompletionCount + RoDet.SurveyCompletionCount




        Return RoDet


        ' Else

        RoDet.Status = "Authentication Required"

        ' End If



        Return RoDet

        'If Ca = Auth.GetValue(0) Then

        '    Dim ss As String

        'End If

        'Dim authorization As String = Auth.GetValue(0)


    End Function

End Class