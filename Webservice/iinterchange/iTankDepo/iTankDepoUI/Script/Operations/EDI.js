var HasChanges = false;

//Initialize page while loading the page based on page mode

function initPage(sMode) {
    setPageMode("edit");
    resetValidatorByGroup("tabEDI");
    clearTextValues("lkpCustomer");
    setFocusToField("lkpCustomer");
    el("chkGateIn").checked = false;
    el("chkRepairEstimate").checked = false;
    el("chkGateOut").checked = false;
    el("chkRepairComplete").checked = false;
    $('.btncorner').corner();
}

//This method used to submit the changes
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    document.body.click();
    var objCallback = new Callback();
    var GateIn = el("chkGateIn").checked;
    var RepairEstimate = el("chkRepairEstimate").checked;
    var GateOut = el("chkGateOut").checked;
    var _034KeyValue = getConfigSetting('034');
    if (_034KeyValue = "RepairComplete") {
        var RepairComplete = el("chkRepairComplete").checked;
        }
        else
        {
         var RepairComplete = false;
    }
    
    var EDIFormat = el("lkpEDIFormat").value;
    objCallback.add("GateIn", GateIn);
    objCallback.add("RepairEstimate", RepairEstimate);
    objCallback.add("GateOut", GateOut);
    objCallback.add("GateOut", GateOut);
    objCallback.add("RepairComplete", RepairComplete);
    objCallback.add("WfData", el("WFDATA").value);
    objCallback.add("Customer_cd", el("lkpCustomer").value);
    objCallback.add("EdiFormat", EDIFormat);
    if ((GateIn == true || RepairEstimate == true || GateOut == true || RepairComplete ==true)) {
        objCallback.invoke("EDI.aspx", "ChkFileExists")

        if (objCallback.getCallbackStatus()) {
            //            if (objCallback.getReturnValue("Is031KeyExist")) {
            //                showErrorMessage("Automatic EDI has been configured hence cannot generate EDI manually.");
            //            }

            if (objCallback.getReturnValue("IsExists")) {
                var status = objCallback.getReturnValue("IsExists")
                var result;
                var Msg;
                if (status == "True") {
                    Msg = "EDI files already exist.Please clear the files to proceed or it will be overwritten. Do you want to continue?"
                    result = confirm(Msg);
                }
                if (result == true || status == "False") {

                    objCallback.invoke("EDI.aspx", "GenerateEDI");
                    Msg = objCallback.getReturnValue("Msg")
                    showInfoMessage(Msg);
                    objCallback = null;
                }
            }
        }
        else {
            showErrorMessage(objCallback.getCallbackError());
        }
        objCallback = null;
    }
    else {
        showErrorMessage("Select Atleast One Event.");
    }
    el("chkGateIn").checked = false;
    el("chkRepairEstimate").checked = false;
    el("chkGateOut").checked = false;
    el("chkRepairComplete").checked = false;
}

function Check031KeyExist(oSrc, args) {
    var _CustomerCode = args.Value
    var _031KeyValue = getConfigSetting('031');
    var _031KeyArr = new Array();
    _031KeyArr = _031KeyValue.split(",");
    if (_031KeyArr.length > 0) {
        for (var i = 0; i < _031KeyArr.length; i++) {
            if (_CustomerCode == _031KeyArr[i]) {
                oSrc.errormessage = "Automatic EDI has been configured hence cannot generate EDI manually.";
                args.IsValid = false;
                return;
            }
        }
    }   
}