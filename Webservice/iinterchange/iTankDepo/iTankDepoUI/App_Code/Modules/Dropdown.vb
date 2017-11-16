Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Dropdown
    Inherits System.Web.Services.WebService


    Dim arraylist As New ArrayList
    Dim conn As New Dropdown_C

    Dim arrayOfDropDown As New ArrayOfDropdowns
    Dim gateInMobile As New GateinMobile_C
    Dim objCommon As New CommonData
    



    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function Customer(ByVal UserName As String) As ArrayOfDropdowns


        gateInMobile.DepotID(UserName)

        Dim query As String = "select CSTMR_ID,CSTMR_CD,CSTMR_NAM,CHK_DGT_VLDTN_BT from CUSTOMER where DPT_ID='" + objCommon.GetDepotID + "'order by CSTMR_CD asc"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)




        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim custr As New CustomersDropdown
            custr.Code = dd.Item(GateinData.CSTMR_ID)
            custr.Name = dd.Item(GateinData.CSTMR_CD)
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(custr)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown



        '' Dim a As integer = dtPreaAdvice.l
        'Return arraylist
        'Return a1
    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function EquipmentType(ByVal UserName As String) As ArrayOfDropdowns


        gateInMobile.DepotID(UserName)
        Dim query As String = "select EQPMNT_TYP_CD,EQPMNT_TYP_ID,EQPMNT_TYP_DSCRPTN_VC from EQUIPMENT_TYPE where DPT_ID='" + objCommon.GetDepotID + "'"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)





        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim equipTyp As New EQUIPMENTTYPEDropdown
            equipTyp.Type = dd.Item(GateinData.EQPMNT_TYP_CD)
            equipTyp.Code = dd.Item(GateinData.EQPMNT_TYP_ID)
            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)
            arraylist.Add(equipTyp)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown



        '' Dim a As integer = dtPreaAdvice.l
        'Return arraylist
        'Return a1
    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function PreviousCargo(ByVal UserName As String) As ArrayOfDropdowns


        gateInMobile.DepotID(UserName)
        Dim query As String = "select PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC from [PRODUCT] where DPT_ID='" + objCommon.GetDepotID + "'"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)





        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim prevuCrg As New PreviousCargo
            prevuCrg.ID = dd.Item(GateinData.PRDCT_ID)
            prevuCrg.Code = dd.Item(GateinData.PRDCT_CD)
            prevuCrg.Description = dd.Item(GateinData.PRDCT_DSCRPTN_VC)
            'equipTyp.Description = dd.Item(GateinData.EQPMNT_TYP_DSCRPTN_VC)
            arraylist.Add(prevuCrg)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown



        '' Dim a As integer = dtPreaAdvice.l
        'Return arraylist
        'Return a1
    End Function


    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function CleaningStatusOne(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select [ENM_ID],[ENM_CD] from [ENUM] where [ENM_TYP_ID]= 20"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)




        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim ClnstOne As New CleaningStatusOne
            ClnstOne.ENM_ID = dd.Item("ENM_ID")
            ClnstOne.ENM_CD = dd.Item("ENM_CD")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(ClnstOne)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown



        '' Dim a As integer = dtPreaAdvice.l
        'Return arraylist
        'Return a1
    End Function


    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function TestType(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select [ENM_ID],[ENM_CD] from [ENUM] where [ENM_TYP_ID]= 17"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)




        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim ClnstOne As New CleaningStatusOne
            ClnstOne.ENM_ID = dd.Item("ENM_ID")
            ClnstOne.ENM_CD = dd.Item("ENM_CD")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(ClnstOne)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown



        '' Dim a As integer = dtPreaAdvice.l
        'Return arraylist
        'Return a1
    End Function



    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function CleaningStatusTwo(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select [ENM_ID],[ENM_CD] from [ENUM] where [ENM_TYP_ID]= 22"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)




        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim ClnstOne As New CleaningStatusOne
            ClnstOne.ENM_ID = dd.Item("ENM_ID")
            ClnstOne.ENM_CD = dd.Item("ENM_CD")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(ClnstOne)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown



        '' Dim a As integer = dtPreaAdvice.l
        'Return arraylist
        'Return a1
    End Function



    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function ConditionDropDown(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select [ENM_ID],[ENM_CD] from [ENUM] where [ENM_TYP_ID]= 21"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds  
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)




        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim ClnstOne As New CleaningStatusOne
            ClnstOne.ENM_ID = dd.Item("ENM_ID")
            ClnstOne.ENM_CD = dd.Item("ENM_CD")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(ClnstOne)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown



        '' Dim a As integer = dtPreaAdvice.l
        'Return arraylist
        'Return a1
    End Function







    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function GetEquipmentStatusID(ByVal UserName As String, ByVal StatusName As String) As EquipmentStatusID


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select EQPMNT_STTS_ID from [EQUIPMENT_STATUS] where [EQPMNT_STTS_CD]= '" + StatusName + "'"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds  
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        Dim equipmentStatusID As New EquipmentStatusID
        equipmentStatusID.StatusID = dtPreaAdvice.Rows.Item("EQPMNT_STTS_ID").ToString()

        Return equipmentStatusID


    End Function


    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function InvoicingPartyID(ByVal UserName As String, ByVal StatusName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select INVCNG_PRTY_CD,INVCNG_PRTY_ID from [INVOICING_PARTY] where ACTV_BT=1"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds  
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim InvoicingParty As New InvoicingParty
            InvoicingParty.INVCNG_PRTY_CD = dd.Item("INVCNG_PRTY_CD")
            InvoicingParty.INVCNG_PRTY_ID = dd.Item("INVCNG_PRTY_ID")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(InvoicingParty)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown


    End Function


    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function DepotDropdn(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select DPT_ID,DPT_CD from [DEPOT] "
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds  
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim DepotDropDown As New DepotDropDown
            DepotDropDown.DPT_ID = dd.Item("DPT_ID")
            DepotDropDown.DPT_CD = dd.Item("DPT_CD")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(DepotDropDown)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown


    End Function


    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function RepairType(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select [ENM_ID],[ENM_CD] from [ENUM] where [ENM_TYP_ID]= 19"
        Dim ds As DataSet = conn.connection(query)
       
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim ClnstOne As New CleaningStatusOne
            ClnstOne.ENM_ID = dd.Item("ENM_ID")
            ClnstOne.ENM_CD = dd.Item("ENM_CD")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(ClnstOne)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown


    End Function


    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function TariffCodeDropDn(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select TRFF_CD_ID,TRFF_CD_CD,TRFF_CD_DESCRPTN_VC+'('+TRFF_CD_CD+')' AS TRFF_CD_DESCRPTN_VC from [TARIFF_CODE] where ACTV_BT=1"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds  
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim TariffCodeDropDown As New TariffCodeDropDown
            TariffCodeDropDown.TRFF_CD_ID = dd.Item("TRFF_CD_ID")
            TariffCodeDropDown.TRFF_CD_CD = dd.Item("TRFF_CD_CD")
            TariffCodeDropDown.TRFF_CD_DESCRPTN_VC = dd.Item("TRFF_CD_DESCRPTN_VC")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(TariffCodeDropDown)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown


    End Function

    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function TariffGrpupCodeDropDn(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select TRFF_GRP_ID,TRFF_GRP_CD,TRFF_GRP_DESCRPTN_VC+'('+TRFF_GRP_CD+')' AS TRFF_GRP_DESCRPTN_VC from TARIFF_GROUP where ACTV_BT=1"
        Dim ds As DataSet = conn.connection(query)

        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim TariffGrpupCodeDropDown As New TariffGrpupCodeDropDown
            TariffGrpupCodeDropDown.TRFF_GRP_ID = dd.Item("TRFF_GRP_ID")
            TariffGrpupCodeDropDown.TRFF_GRP_CD = dd.Item("TRFF_GRP_CD")
            TariffGrpupCodeDropDown.TRFF_GRP_DESCRPTN_VC = dd.Item("TRFF_GRP_DESCRPTN_VC")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(TariffGrpupCodeDropDown)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown


    End Function

    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function LineItemCodeDropdn(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select ITM_CD,ITM_DSCRPTN_VC from ITEM"
        Dim ds As DataSet = conn.connection(query)

        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim LineItemCodeDropDown As New LineItemCodeDropDown
            LineItemCodeDropDown.ITM_CD = dd.Item("ITM_CD")
            LineItemCodeDropDown.ITM_DSCRPTN_VC = dd.Item("ITM_DSCRPTN_VC")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(LineItemCodeDropDown)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown


    End Function

    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function SubItemCodeDropdn(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select SB_ITM_CD,SB_ITM_DSCRPTN_VC+'('+SB_ITM_CD+')' as SB_ITM_DSCRPTN_VC from V_SUB_ITEM"
        Dim ds As DataSet = conn.connection(query)

        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim SubItemCodeDropDown As New SubItemCodeDropDown
            SubItemCodeDropDown.SB_ITM_CD = dd.Item("SB_ITM_CD")
            SubItemCodeDropDown.SB_ITM_DSCRPTN_VC = dd.Item("SB_ITM_DSCRPTN_VC")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(SubItemCodeDropDown)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown


    End Function

    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function DamageDropDn(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select DMG_CD,DMG_DSCRPTN_VC+'('+DMG_CD+')' as DMG_DSCRPTN_VC from DAMAGE"
        Dim ds As DataSet = conn.connection(query)

        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim DamageDropDown As New DamageDropDown
            DamageDropDown.DMG_CD = dd.Item("DMG_CD")
            DamageDropDown.DMG_DSCRPTN_VC = dd.Item("DMG_DSCRPTN_VC")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(DamageDropDown)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown


    End Function


    <WebMethod(enableSession:=True)> _
<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function RepairDropDn(ByVal UserName As String) As ArrayOfDropdowns


        'gateInMobile.DepotID(UserName)

        Dim query As String = "select RPR_ID,RPR_DSCRPTN_VC+'('+CAST(RPR_ID as varchar(max)) +')' as RPR_DSCRPTN_VC from REPAIR"
        Dim ds As DataSet = conn.connection(query)

        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim RepairDropDown As New RepairDropDown
            RepairDropDown.RPR_ID = dd.Item("RPR_ID")
            RepairDropDown.RPR_DSCRPTN_VC = dd.Item("RPR_DSCRPTN_VC")
            'custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            arraylist.Add(RepairDropDown)
        Next

        arrayOfDropDown.arrayOfDropdowns = arraylist

        Return arrayOfDropDown


    End Function
End Class
