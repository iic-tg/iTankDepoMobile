var HasChanges = false;
var HasMoreInfoChanges = false;
var _RowValidationFails = false;
function initPage(sMode) {
    setFocusToField("txtDateofManf");
}

//This function is used to submit the page to the server.
function submitPage() {    
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    submitMoreInfo();
    return true;
}

function submitMoreInfo() {
    var oCallback = new Callback();
    oCallback.add("GateInId", el("hdnID").value);
    oCallback.add("MaterialId", el("lkpMaterialCode").SelectedValues[0]);
    oCallback.add("MaterialCode", el("lkpMaterialCode").SelectedValues[1]);
    oCallback.add("MeasureId", el("lkpMeasure").SelectedValues[0]);
    oCallback.add("MeasureCode", el("lkpMeasure").SelectedValues[1]);
    oCallback.add("MfgDate", el("txtDateofManf").value);
    oCallback.add("AcepCode", el("txtAcep").value);    
    oCallback.add("UnitId", el("lkpUnits").SelectedValues[0]);
    oCallback.add("UnitCd", el("lkpUnits").SelectedValues[1]);
    oCallback.add("PreviousLocation", el("txtOnhireLoc").value);
    oCallback.add("PreviousDate", el("datOnhireDate").value);
    oCallback.add("TruckerCode", el("txtTruckerCode").value);
    oCallback.add("LoadStatus", el("lkpLoadStatus").value);
    oCallback.add("CountryId", el("lkpCountry").SelectedValues[0]);
    oCallback.add("CountryCode", el("lkpCountry").SelectedValues[1]);
    oCallback.add("State", el("txtState").value);
    oCallback.add("LicNumber", el("txtNumber").value);
    oCallback.add("Expiry", el("txtExpiry").value);
    oCallback.add("Notes1", el("txtNotes1").value);
    oCallback.add("Notes2", el("txtNotes2").value);
    oCallback.add("Notes3", el("txtNotes3").value);
    oCallback.add("Notes4", el("txtNotes4").value);
    oCallback.add("Notes5", el("txtNotes5").value);
    if (el("lkpParty1") != null) {
        oCallback.add("Party1Id", el("lkpParty1").SelectedValues[0]);
        oCallback.add("Party2Id", el("lkpParty2").SelectedValues[0]);
        oCallback.add("Party3Id", el("lkpParty3").SelectedValues[0]);
        oCallback.add("Party1Cd", el("lkpParty1").SelectedValues[1]);
        oCallback.add("Party2Cd", el("lkpParty2").SelectedValues[1]);
        oCallback.add("Party3Cd", el("lkpParty3").SelectedValues[1]);
    }
    else {
        oCallback.add("Party1Id", el("lkpPartyMaster1").SelectedValues[0]);
        oCallback.add("Party2Id", el("lkpPartyMaster2").SelectedValues[0]);
        oCallback.add("Party3Id", el("lkpPartyMaster3").SelectedValues[0]);
        oCallback.add("Party1Cd", el("lkpPartyMaster1").SelectedValues[1]);
        oCallback.add("Party2Cd", el("lkpPartyMaster2").SelectedValues[1]);
        oCallback.add("Party3Cd", el("lkpPartyMaster3").SelectedValues[1]);
    }
    oCallback.add("SlNumber1", el("txtNumber1").value);
    oCallback.add("SlNumber2", el("txtNumber2").value);
    oCallback.add("SlNumber3", el("txtNumber3").value);
    oCallback.add("PortFunctionCode", el("txtPortFuncCode").value);
    oCallback.add("Portname", el("txtPortName").value);
    oCallback.add("Portnumnber", el("txtPortnumber").value);
    oCallback.add("PortlocCode", el("txtPortLocCode").value);
    oCallback.add("VesselName", el("txtVesselName").value);
    oCallback.add("VoyageNumber", el("txtVoyageNumber").value);
    oCallback.add("VesselCode", el("txtVesselIdCode").value);
    oCallback.add("Shipper", el("txtShipper").value);
    oCallback.add("RailId", el("txtRailId").value);
    oCallback.add("RailRamploc", el("txtRailRamp").value);
    oCallback.add("HazCode", el("txthazCode").value);
    oCallback.add("HazDesc", el("txthazDesc").value);
    oCallback.add("GrossWeight", el("txtGrossWeight").value);
    oCallback.add("TareWeight", el("txtTareWeight").value);
    oCallback.invoke("MoreInfo.aspx", "submit");
    pdfs("wfFrame" + pdf("CurrentDesk")).HasMoreInfoChanges = true;
    ppsc().closeModalWindow();
    oCallback = null;
    return true;
}

function makeReadOnly() {
    setReadOnly("lkpMaterialCode", true);
    setReadOnly("lkpMeasure", true);
    setReadOnly("txtDateofManf", true);
    setReadOnly("txtAcep", true);
    setReadOnly("lkpUnits", true);
    setReadOnly("txtOnhireLoc", true);
    setReadOnly("datOnhireDate", true);
    setReadOnly("txtTruckerCode", true);
    setReadOnly("lkpLoadStatus", true);
    setReadOnly("lkpCountry", true);
    setReadOnly("txtState", true);
    setReadOnly("txtNumber", true);
    setReadOnly("txtExpiry", true);
    setReadOnly("txtNotes1", true);
    setReadOnly("txtNotes2", true);
    setReadOnly("txtNotes3", true);
    setReadOnly("lkpParty1", true);
    setReadOnly("lkpParty2", true);
    setReadOnly("lkpParty3", true);
    setReadOnly("txtNumber1", true);
    setReadOnly("txtNumber2", true);
    setReadOnly("txtNumber3", true);
    setReadOnly("txtPortFuncCode", true);
    setReadOnly("txtPortName", true);
    setReadOnly("txtPortnumber", true);
    setReadOnly("txtPortLocCode", true);
    setReadOnly("txtVesselName", true);
    setReadOnly("txtVoyageNumber", true);
    setReadOnly("txtVesselIdCode", true);
    setReadOnly("txtShipper", true);
    setReadOnly("txtRailId", true);
    setReadOnly("txtRailRamp", true);
    setReadOnly("txthazCode", true);
    setReadOnly("txthazDesc", true);
    setReadOnly("txtGrossWeight", true);
    setReadOnly("txtTareWeight", true);
}