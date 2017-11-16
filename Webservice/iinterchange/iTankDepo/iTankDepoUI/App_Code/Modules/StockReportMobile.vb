Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports Newtonsoft.Json

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class StockReportMobile
    Inherits System.Web.Services.WebService



    Dim gateInMobile As New GateinMobile_C
    Dim objCommon As New CommonData

    <WebMethod(enableSession:=True)> _
     <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function StockReportView(ByVal StockReportModel As StockReportModel) As StockReportView


        gateInMobile.DepotID(StockReportModel.UserName)
        Dim rprtView As New StockReportView
        Dim EquipmentType As String = ""
        Dim PrevoiusCargo As String = ""
        Dim CurrentStatus As String = ""
        Dim NextTestType As String = ""
        Dim Customer As String = ""
        Dim Depot As String = ""
        Dim finala As String

        For Each kvp1 In StockReportModel.Equipment_Type

            Dim assd As String = kvp1.Type

            If EquipmentType = "" Then
                EquipmentType = "'" + kvp1.Type + "'"

            Else
                finala = EquipmentType
                EquipmentType = finala + "," + "'" + kvp1.Type + "'"
            End If

        Next

        For Each kvp1 In StockReportModel.Previous_Cargo

            Dim assd As String = kvp1.Type

            If PrevoiusCargo = "" Then
                PrevoiusCargo = "'" + kvp1.Type + "'"

            Else
                finala = PrevoiusCargo
                PrevoiusCargo = finala + "," + "'" + kvp1.Type + "'"
            End If

        Next

        For Each kvp1 In StockReportModel.Current_Status

            Dim assd As String = kvp1.Type

            If CurrentStatus = "" Then
                CurrentStatus = "'" + kvp1.Type + "'"

            Else
                finala = CurrentStatus
                CurrentStatus = finala + "," + "'" + kvp1.Type + "'"
            End If

        Next

        For Each kvp1 In StockReportModel.Next_Test_Type

            Dim assd As String = kvp1.Type

            If NextTestType = "" Then
                NextTestType = "'" + kvp1.Type + "'"

            Else
                finala = NextTestType
                NextTestType = finala + "," + "'" + kvp1.Type + "'"
            End If

        Next


        For Each kvp1 In StockReportModel.Customer

            Dim assd As String = kvp1.Type

            If Customer = "" Then
                Customer = "'" + kvp1.Type + "'"

            Else
                finala = Customer
                Customer = finala + "," + "'" + kvp1.Type + "'"
            End If

        Next

        For Each kvp1 In StockReportModel.Depot

            Dim assd As String = kvp1.Type

            If Depot = "" Then
                Depot = "'" + kvp1.Type + "'"

            Else
                finala = Depot
                Depot = finala + "," + "'" + kvp1.Type + "'"
            End If

        Next


        Dim ssss As String = "Customer='181'&Equipment Type='1','2','3','4','5','6','7','8','9','10','11','12','13','14'&In Date From=&In Date To=&Previous Cargo='2'&Cleaning Date From=&Cleaning Date To=&Inspection Date From=&Inspection Date To=&Current Status Date From=&Current Status Date To=&Current Status='1','2','3','4','5','6','7','8','9','10','11','12','13','14','15','16','17','18','19','21','23','24','25','26','27'&Next Test Date From=&Next Test Date To=&Next Test Type='51','52'&Equipment No=&EIR No=&Out Date From=23-NOV-2016&Out Date To=25-NOV-2016&Depot='1'"

        Dim output As String = "Customer=" + Customer + "&Equipment Type=" + EquipmentType + "&In Date From=" + StockReportModel.In_Date_From + "&In Date To=" + StockReportModel.In_Date_To + "&Previous Cargo=" + PrevoiusCargo + "&Cleaning Date From=" + StockReportModel.Cleaning_Date_From + "&Cleaning Date To=" + StockReportModel.Cleaning_Date_To + "&Inspection Date From=" + StockReportModel.Inspection_Date_From + "&Inspection Date To=" + StockReportModel.Inspection_Date_To + "&Current Status Date From=" + StockReportModel.Current_Status_Date_From + "&Current Status Date To=" + StockReportModel.Current_Status_Date_To + "&Current Status=" + CurrentStatus + "&Next Test Date From=" + StockReportModel.Next_Test_Date_From + "&Next Test Date To=" + StockReportModel.Next_Test_Date_To + "&Next Test Type=" + NextTestType + "&Equipment No=" + StockReportModel.Equipment_No + "&EIR No=" + StockReportModel.EIR_No + "&Out Date From=" + StockReportModel.Out_Date_From + "&Out Date To=" + StockReportModel.Out_Date_To + "&Depot=" + Depot + ""

        'Dim SFsf As String = kvp.GetValue(Equipment_Type, Nothing).ToString()






        Try



            Dim objReportViewer As ReportViewerDataSource
            Dim objCommonData As New CommonData
            Dim intDepotID As Integer = CommonWeb.iInt(objCommonData.GetDepotID())


            objReportViewer = New ReportViewerDataSource("Inventory", output, True, intDepotID, 108)

            Dim dtAS As DataTable = objReportViewer.pub_GetDatasource.Tables("V_DAR_ACTIVITY_STATUS")
            Dim dtCS As DataTable = objReportViewer.pub_GetDatasource.Tables("V_DAR_CUSTOMER_SUMMARY")
            Dim dtTS As DataTable = objReportViewer.pub_GetDatasource.Tables("V_DAR_TYPE_SUMMARY")

            Dim arraylist As New ArrayList


            If dtAS.Rows.Count > 0 Then



                For Each dr As DataRow In dtAS.Rows

                    Dim VDAS As New VDarActivityStatus

                    VDAS.Depot = dr.Item("DPT_CD").ToString()
                    VDAS.Customer = dr.Item("CSTMR_CD").ToString()
                    VDAS.EquipmentNo = dr.Item("EQPMNT_NO").ToString()
                    VDAS.Type = dr.Item("EQPMNT_TYP_CD").ToString()
                    VDAS.Indate = dr.Item("GTN_DT").ToString()
                    VDAS.PreviousCargo = dr.Item("PRDCT_DSCRPTN_VC").ToString()
                    VDAS.EirNo = dr.Item("EIR_NO").ToString()
                    VDAS.CleaningCertNo = dr.Item("CLNNG_CERT_NO").ToString()
                    VDAS.CurrentStatusDate = dr.Item("ACTVTY_DT").ToString()
                    VDAS.CurrentStatus = dr.Item("EQPMNT_STTS_CD").ToString()
                    VDAS.CleaningDate = dr.Item("CLNNG_DT").ToString()
                    VDAS.InspectionDate = dr.Item("INSPCTN_DT").ToString()
                    VDAS.Remarks = dr.Item("RMRKS_VC").ToString()
                    VDAS.NextTestDate = dr.Item("NEXT_TEST_DATE").ToString()
                    VDAS.NextTestType = dr.Item("NXT_TYP_CD").ToString()


                    arraylist.Add(VDAS).ToString()
                Next

                Dim VDCS As New VDarCustomerSummary
                Dim VDTS As New VDarTypeSummary

                For Each dr1 As DataRow In dtCS.Rows


                    VDCS.IND = dr1.Item("IND").ToString()
                    VDCS.PHL = dr1.Item("PHL").ToString()
                    VDCS.ACN = dr1.Item("ACN").ToString()
                    VDCS.AWECLN = dr1.Item("AWECLN").ToString()
                    VDCS.AWE = dr1.Item("AWE").ToString()
                    VDCS.AAR = dr1.Item("AAR").ToString()
                    VDCS.AUR = dr1.Item("AUR").ToString()
                    VDCS.ASR = dr1.Item("ASR").ToString()
                    VDCS.SRV = dr1.Item("SRV").ToString()
                    VDCS.AVLCLN = dr1.Item("AVLCLN").ToString()

                    VDCS.AVLINS = dr1.Item("AVLINS").ToString()
                    VDCS.INSRPC = dr1.Item("INSRPC").ToString()
                    VDCS.RPC = dr1.Item("RPC").ToString()
                    VDCS.STO = dr1.Item("STO").ToString()
                    VDCS.AVL = dr1.Item("AVL").ToString()
                    VDCS.OUT = dr1.Item("OUT").ToString()
                    VDCS.TOTAL = dr1.Item("TOTAL").ToString()
                    VDCS.Customer = dr1.Item("CSTMR_CD").ToString()
                Next


                For Each dr2 As DataRow In dtCS.Rows


                    VDTS.IND = dr2.Item("IND").ToString()
                    VDTS.PHL = dr2.Item("PHL").ToString()
                    VDTS.ACN = dr2.Item("ACN").ToString()
                    VDTS.AWECLN = dr2.Item("AWECLN").ToString()
                    VDTS.AWE = dr2.Item("AWE").ToString()
                    VDTS.AAR = dr2.Item("AAR").ToString()
                    VDTS.AUR = dr2.Item("AUR").ToString()
                    VDTS.ASR = dr2.Item("ASR").ToString()
                    VDTS.SRV = dr2.Item("SRV").ToString()
                    VDTS.AVLCLN = dr2.Item("AVLCLN").ToString()

                    VDTS.AVLINS = dr2.Item("AVLINS").ToString()
                    VDTS.INSRPC = dr2.Item("INSRPC").ToString()
                    VDTS.RPC = dr2.Item("RPC").ToString()
                    VDTS.STO = dr2.Item("STO").ToString()
                    VDTS.AVL = dr2.Item("AVL").ToString()
                    VDTS.OUT = dr2.Item("OUT").ToString()
                    VDTS.TOTAL = dr2.Item("TOTAL").ToString()
                    VDTS.Type = dr2.Item("EQPMNT_TYP_CD").ToString()
                Next





                rprtView.ActivityStatus = arraylist
                rprtView.CustomerSummary = VDCS
                rprtView.TypeSummary = VDTS
                rprtView.status = "Success"


            Else


                rprtView.status = "No Records Found"

            End If



            Return rprtView

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)

            rprtView.status = ex.Message
            Return rprtView
        End Try


    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function StockDropdown(ByVal UserName As String) As DropDownType

        Dim arraylist As New ArrayList
        gateInMobile.DepotID(UserName)
        Dim conn As New Dropdown_C
        Dim DropDownType As New DropDownType



        ''''''''''''''''''''''''Customer'''''''''''''''''''''''

        Dim query As String = "select CSTMR_ID,CSTMR_CD,CSTMR_NAM,CHK_DGT_VLDTN_BT from CUSTOMER where DPT_ID='" + objCommon.GetDepotID + "'"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)

        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim Dropdowns As New Dropdowns
            Dropdowns.Id = dd.Item(GateinData.CSTMR_ID)
            Dropdowns.Description = dd.Item(GateinData.CSTMR_CD)
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(Dropdowns)
        Next

        DropDownType.Customer = arraylist


        ''''''''''''''''''''''''EquipmentType'''''''''''''''''''''''


        Dim arraylistE As New ArrayList

        Dim EquipmentQ As String = "select EQPMNT_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_DSCRPTN_VC from EQUIPMENT_TYPE where DPT_ID='" + objCommon.GetDepotID + "'"
        Dim EquipmentQds As DataSet = conn.connection(EquipmentQ)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dEquipmentPreaAdvice As DataTable
        dEquipmentPreaAdvice = EquipmentQds.Tables(0)





        For Each dd As DataRow In dEquipmentPreaAdvice.Rows
            Dim Dropdowns As New Dropdowns
            Dropdowns.Id = dd.Item(GateinData.EQPMNT_TYP_ID)
            Dropdowns.Description = dd.Item(GateinData.EQPMNT_TYP_CD)
            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)
            arraylistE.Add(Dropdowns)
        Next

        DropDownType.EquipmentType = arraylistE


        ''''''''''''''''''''''''Prevoius Cargo'''''''''''''''''''''''
        Dim arraylistP As New ArrayList
        Dim PreViousCrgoQuery As String = "select PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC from [PRODUCT] where DPT_ID='" + objCommon.GetDepotID + "'"
        Dim PreViousCrgods As DataSet = conn.connection(PreViousCrgoQuery)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim PreViousCrgodtPreaAdvice As DataTable
        PreViousCrgodtPreaAdvice = PreViousCrgods.Tables(0)





        For Each dd As DataRow In PreViousCrgodtPreaAdvice.Rows

            Dim Dropdowns As New Dropdowns
            Dropdowns.Id = dd.Item(GateinData.PRDCT_ID)
            Dropdowns.Description = dd.Item(GateinData.PRDCT_DSCRPTN_VC)
            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)


            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)
            arraylistP.Add(Dropdowns)
        Next

        DropDownType.PrevoiusCargo = arraylistP



        ''''''''''''''''''''''''CurrentStaus'''''''''''''''''''''''
        Dim arraylistC As New ArrayList
        Dim CurrentStatusQuery As String = "select [EQPMNT_STTS_ID],[EQPMNT_STTS_CD] from [EQUIPMENT_STATUS] where DPT_ID='" + objCommon.GetDepotID + "'"
        Dim CurrentStatusds As DataSet = conn.connection(CurrentStatusQuery)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim CurrentStatusPreaAdvice As DataTable
        CurrentStatusPreaAdvice = CurrentStatusds.Tables(0)





        For Each dd As DataRow In CurrentStatusPreaAdvice.Rows

            Dim Dropdowns As New Dropdowns
            Dropdowns.Id = dd.Item("EQPMNT_STTS_ID")
            Dropdowns.Description = dd.Item("EQPMNT_STTS_CD")
            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)


            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)
            arraylistC.Add(Dropdowns)
        Next

        DropDownType.CurrentStatus = arraylistC



        ''''''''''''''''''''''''NextTestType'''''''''''''''''''''''
        Dim arraylistN As New ArrayList
        Dim NextTestQuery As String = "select [ENM_ID],[ENM_CD] from [ENUM] where [ENM_TYP_ID]= 17"
        Dim NextTestds As DataSet = conn.connection(NextTestQuery)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim NextTest As DataTable
        NextTest = NextTestds.Tables(0)





        For Each dd As DataRow In NextTest.Rows

            Dim Dropdowns As New Dropdowns
            Dropdowns.Id = dd.Item("ENM_ID")
            Dropdowns.Description = dd.Item("ENM_CD")
            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)


            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)
            arraylistN.Add(Dropdowns)
        Next

        DropDownType.NextTestType = arraylistN


        ''''''''''''''''''''''''Depot'''''''''''''''''''''''
        Dim arraylistD As New ArrayList
        Dim DepotQuery As String = "select DPT_ID,DPT_CD from [DEPOT]"
        Dim Depotds As DataSet = conn.connection(DepotQuery)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim Depot As DataTable
        Depot = Depotds.Tables(0)





        For Each dd As DataRow In Depot.Rows

            Dim Dropdowns As New Dropdowns
            Dropdowns.Id = dd.Item("DPT_ID")
            Dropdowns.Description = dd.Item("DPT_CD")
            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)


            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)
            arraylistD.Add(Dropdowns)
        Next

        DropDownType.Depot = arraylistD


        Return DropDownType




    End Function

End Class