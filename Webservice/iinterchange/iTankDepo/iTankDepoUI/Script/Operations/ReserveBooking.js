var validate = true;
var bookedQuantity = "";

function initPage(sMode) {
//    reSizePane();
    //$("#spnHeader").text(getPageTitle());
    //setReadOnly("txtTopSealNo", false);
    //setFocusToField("datOriginalInspectedDate");
    //clearTextValues("lkpInvcngTo");
    //clearLookupValues("lkpInvcngTo");
  
    resetHasChanges("ifgBookingQuantity");
    resetHasChanges("ifgEquipmentDetails");
    //resetValidators(true);
    resetValidators();
    if (sMode == "new") {
        bookedQuantity = "";

        //Hide
       
    //Clear Values
        clearTextValues("txtbookingAuthNo");
        clearTextValues("datDateOfAcceptance");
        clearTextValues("txtConsignee");
        clearLookupValues("lkpCustomer");

        //Make Editable
        setReadOnly("txtbookingAuthNo", false);
        setReadOnly("datDateOfAcceptance", false);
        setReadOnly("txtConsignee", false);
        setReadOnly("lkpCustomer", false);
        showDiv("divFetch");
        resetValidatorByGroup("divGridArea");
        hideDiv("divGridArea");
        resetValidators();
    }

    if (sMode == "edit") {
        //Make Readonly
        setReadOnly("txtbookingAuthNo", true);
        setReadOnly("datDateOfAcceptance", true);
        setReadOnly("txtConsignee", true);
        setReadOnly("lkpCustomer", true);

        showDiv("divGridArea");

        BookingQty_bindGrid();
        Equipment_bindGrid();
        resetValidatorByGroup("divGridArea");
        hideDiv("divFetch");
       
        if (el('hdnAuthNoEdit').value == "edit") {
            setReadOnly("txtbookingAuthNo", false);
            setReadOnly("datDateOfAcceptance", false);
            setReadOnly("txtConsignee", false);
        }
        else {
            setReadOnly("txtbookingAuthNo", true);
            setReadOnly("datDateOfAcceptance", true);
            setReadOnly("txtConsignee", true);
        }
    }

    el('hdnMode').value = sMode;
    return false;
}


//Fetch Click
function onFetchClick() {

    if (el('lkpCustomer').SelectedValues[0] == "") {
        showErrorMessage("Please Select Customer");
        return false;
    }
    setReadOnly("lkpCustomer", true);
    hideDiv("divFetch");
    
    var oCallback = new Callback();
    oCallback.add("CustomerId", el('lkpCustomer').SelectedValues[0]);
    oCallback.invoke("ReserveBooking.aspx", "GetEquipmentInformation");
    if (oCallback.getCallbackStatus()) {
        //showInfoMessage("Gate Out Request Updated Successfully.");
        showDiv("divGridArea");
        BookingQty_bindGrid();
        Equipment_bindGrid();
    }
    else {
        showInfoMessage(oCallback.getReturnValue("Message"));
    }
    bookedQuantity = "true";
    return false;
}


//Reset Click
function onResetClick() {

    return false;
}

//booking Quantity Grid Bind
function BookingQty_bindGrid() {

    var objGrid = new ClientiFlexGrid("ifgBookingQuantity");
    //objGrid.parameters.add("Mode", Mode);
    objGrid.DataBind();
    return false;
}


//Equipment Details Grid Bind
function Equipment_bindGrid() {

    var objGrid = new ClientiFlexGrid("ifgEquipmentDetails");
    //objGrid.parameters.add("Mode", Mode);
    objGrid.DataBind();
    return false;
}

//Submit Page

function submitPage() {
   
    GetLookupChanges();
    Page_ClientValidate();

    if (!Page_IsValid) {
        return false;
    }
    else {


        if (ifgBookingQuantity.Submit(true) == false) {
            return false;
        }

        if (ifgEquipmentDetails.Submit(true) == false) {
            return false;
        }

        ifgBookingQuantity.Submit();
        ifgEquipmentDetails.Submit();

        DBC();

        if (ValidateMinBookedQuantity() == false && validate == true) {

            showConfirmMessage("Selected quantity is less than the booked quantity. Are you sure you want to reserve the booking. Click yes to proceed and No to select more equipment’s.",
                                "wfs().yesClickReserve();", "wfs().noClickReserve();");

        }
       
        
        //CreateReserveBooking();

    }
    return false;
}


function yesClickReserve() {

    if (el('hdnMode').value == "new") {

        CreateReserveBooking();
    }

    if (el('hdnMode').value == "edit" && validate == true) {
        UpdateReserveBooking();
    }

    return true;
}

function noClickReserve() {
    return false;
}


//Create Reserve Booking
function CreateReserveBooking() {

    var oCallback = new Callback();
    oCallback.add("CustomerId", el('lkpCustomer').SelectedValues[0]);
    oCallback.add("CustomerCode", el('lkpCustomer').SelectedValues[1]);
    oCallback.add("DateOfAcceptance", el('datDateOfAcceptance').value);
    oCallback.add("BookingAuthNo", el('txtbookingAuthNo').value);
    oCallback.add("Consignee", el('txtConsignee').value);
    oCallback.invoke("ReserveBooking.aspx", "CreateReserveBooking");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        el('hdnID').value = oCallback.getReturnValue("ReserveBookingId");
        el('hdnMode').value = "edit";
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;

    return false;
}


// Update Reserve Booking

function UpdateReserveBooking() {

    var oCallback = new Callback();
    oCallback.add("ReserveBookingId", el('hdnID').value);
    oCallback.add("CustomerId", el('lkpCustomer').SelectedValues[0]);
    oCallback.add("CustomerCode", el('lkpCustomer').SelectedValues[1]);
    oCallback.add("DateOfAcceptance", el('datDateOfAcceptance').value);
    oCallback.add("BookingAuthNo", el('txtbookingAuthNo').value);
    oCallback.add("Consignee", el('txtConsignee').value);
    oCallback.invoke("ReserveBooking.aspx", "UpdateReserveBooking");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));     
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;

    return false;
}


//Validate Booked Quantity

function ValidateBookedQuantity(oSrc, args) {
    if (bookedQuantity == "true") {
        var cols = ifgBookingQuantity.Rows(0).GetClientColumns();
        if (cols[2] == "" || cols[2] == null) {
            oSrc.errormessage = "Booked Quantity Required";
            args.IsValid = false;
            return false;
        }
    }
    else {
        return true;
    }
    var oCallback = new Callback();
    oCallback.add("bookedQty", args.Value);

    oCallback.invoke("ReserveBooking.aspx", "Validate_BookedQuantity");
    if (oCallback.getCallbackStatus()) {
        args.IsValid = true;
    }
    else {
        args.IsValid = false;
        showErrorMessage(oCallback.getReturnValue("Message"));
    }

    return true;
}


//Equipment Detail On AfterCallback - Validate Maximum Selection
function onAfterCallBack(param, mode) {
    if (typeof (param["Msg"]) != 'undefined') {
        showWarningMessage(param["Msg"]);
        ifgEquipmentDetails.Rows(param["Index"]).SetColumnValuesByIndex(4, false);
        validate = false;
    }
    else {
        validate = true;
    }
    return false;
}


//Validate Minimum Selection
function ValidateMinBookedQuantity() {
    if (ifgBookingQuantity.Submit(true) == false) {
        return false;
    }

    if (ifgEquipmentDetails.Submit(true) == false) {
        return false;
    }

    ifgBookingQuantity.Submit();
    ifgEquipmentDetails.Submit();
//    DBC();
    var oCallback = new Callback();
    oCallback.invoke("ReserveBooking.aspx", "Validate_MinimumSelection");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("Message") != "" && oCallback.getReturnValue("Message") != null) {
            showErrorMessage(oCallback.getReturnValue("Message"));
        }
        else {

            //            CreateReserveBooking();

            if (el('hdnMode').value == "new") {

                CreateReserveBooking();
            }

            if (el('hdnMode').value == "edit" && validate == true) {
                UpdateReserveBooking();
            }
        }
        return true;
    }
    else {
        // showErrorMessage(oCallback.getReturnValue("Message"));
        return false;
    }

    return false;
}







////Window Resize
//if (window.$) {
//    $(document).ready(function () {

//        reSizePane();
//    });
//}

//$(window.parent).resize(function () {
//    reSizePane();
//});



//function reSizePane() {

//    if ($(window) != null) {
//        if ($(window.parent).height() < 768) {
//            el("divdisplayGatePass").style.height = $(window.parent).height() - 250 + "px";
//            if (el("ifgEquipmentTypeCode") != null) {
//                el("ifgEquipmentTypeCode").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
//            }

//        }
//        else {
//            el("divdisplayGatePass").style.height = $(window.parent).height() - 260 + "px";
//            if (el("ifgEquipmentTypeCode") != null) {
//                el("ifgEquipmentTypeCode").SetStaticHeaderHeight($(window.parent).height() - 460 + "px");
//            }

//        }
//    }

//}