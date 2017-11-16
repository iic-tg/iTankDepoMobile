var HasChanges = false;
var vrGridIds = new Array('tabEquipmentDetails;ifgEquipmentDetail');
var _RowValidationFails = false;
function initPage() {
    bindEquipmentDetails();

}
function bindEquipmentDetails() {
    var ifgEquipmentDetails = new ClientiFlexGrid("ifgEquipmentDetails");
    ifgEquipmentDetails.parameters.add("SupplierID", el('hdnSupplierId').value);
    ifgEquipmentDetails.parameters.add("SupplierContractId", el('hdnSupplierContractId').value);
    ifgEquipmentDetails.parameters.add("ContractRefNo", el('hdnContractRefNo').value);
    ifgEquipmentDetails.parameters.add("ContractEndDate", el("hdnEndDate").value);
    ifgEquipmentDetails.DataBind();
}
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgEquipmentDetails.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgEquipmentDetails.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
        ifgEquipmentDetails.Rows(iCurrentIndex).SetReadOnlyColumn(1, false);
    }
    return true;
}
function Close() {
//    Page_ClientValidate();
//    ifgEquipmentDetails.Submit(true);
//    GetLookupChanges();
//    if (!Page_IsValid) {
//        return false;
//    }
    //    else {
    Page_ClientValidate();
    ifgEquipmentDetails.Submit(true);
    if (!Page_IsValid) {
        return false;
    }
    if (ifgEquipmentDetails.Submit() == false)
        return false;
        ppsc().closeModalWindow();
//    }
    }

    function addEquipmentDetails() {
        Page_ClientValidate();
//        ifgEquipmentDetails.Submit(true);
        if (!Page_IsValid) {
            return false;
        }
        if (getPageChanges()) {
            if (ifgEquipmentDetails.Submit() == false || _RowValidationFails) {
                return false;
            }
            updateEquipmentDetails();
            var rIndex = pdfs("wfFrame" + pdf("CurrentDesk")).ifgContractDetails.CurrentRowIndex();
            pdfs("wfFrame" + pdf("CurrentDesk")).ifgContractDetails.Rows(rIndex).SetColumnValuesByIndex(4, "Add/Edit");
            pdfs("wfFrame" + pdf("CurrentDesk")).HasChargeHeadChanges = true;
            ppsc().closeModalWindow();
        }
        else {
            showInfoMessage('No Changes to Save');
        }
        return true;
    }
//function addEquipmentDetails() {
//    Page_ClientValidate();
//    GetLookupChanges();
//        if (!Page_IsValid) {
//            return false;
//        }
//       
//        if (getPageChanges()) {
//            if (ifgEquipmentDetails.Submit() == false) {
//                return false;
//            }

//            updateEquipmentDetails();
//            ppsc().closeModalWindow();
//        }
//        else {
//            var rIndex = ifgEquipmentDetails.VirtualCurrentRowIndex();

//            var cols = ifgEquipmentDetails.Rows(rIndex).GetClientColumns();
//            if (cols[0] == "" || cols[2] == "") {
//                ifgEquipmentDetails.Submit(true);
//            }
//            else {
//                showInfoMessage('No Changes to Save');
//            }
//        }
////            showInfoMessage('No Changes to Save');
//        pdfs("wfFrame" + pdf("CurrentDesk")).HasChargeHeadChanges = true;
////        ppsc().closeModalWindow();
//        return true;
//    }
    function updateEquipmentDetails() {
            ifgEquipmentDetails.Submit(true);
            var oCallback = new Callback();
            oCallback.add("ContractRefNo", el("hdnContractRefNo").value);
            oCallback.add("ContractId", el("hdnSupplierContractId").value);
            oCallback.invoke("SupplierEquipmentDetail.aspx", "GetEquipmentDetails");
            if (oCallback.getCallbackStatus()) {
                ppsc().closeModalWindow();
               
                return true;
            }
            else {
                showErrorMessage(oCallback.getCallbackError());
            }
            oCallback = null;
        }
        function onBeforeCallback(mode, param) {
            if (mode = "Insert" || mode == "Delete") {
                param.add("SupplierContractId", el('hdnSupplierContractId').value);
                param.add("ContractRefNo", el('hdnContractRefNo').value);
                param.add("SupplierID", el('hdnSupplierId').value);
            }
        }
        function onAfterCB(param) {
            var ValidDate = param["ValidateEndDate"];
//            if (ValidDate == "True") {
//                hideDiv('HypButton');
//                el("btnSubmit").style.visibility = "hidden";
//            }
//            else {
//                showDiv('HypButton');
//                el("btnSubmit").style.visibility = "visible";
            //            }
            if (typeof (param["CheckDefault"]) != "undefined") {
                showErrorMessage(param["CheckDefault"]);
            }
            if (typeof (param["Delete"]) != 'undefined') {
                showWarningMessage(param["Delete"]);
            }
            else if (typeof (param["Update"]) != 'undefined' && param["Update"] != '') {
                showWarningMessage(param["Update"]);
            }
            else {
                hideMessage();
            }
        }

        function checkDuplicateEquipment(oSrc, args) {
            var rIndex = ifgEquipmentDetails.VirtualCurrentRowIndex();

            var cols = ifgEquipmentDetails.Rows(rIndex).GetClientColumns();
            var i;
            var sPort = cols[0];
            var icount = ifgEquipmentDetails.Rows().Count
            var colscheck;
            if (icount > 0) {
                for (i = 0; i < icount; i++) {
                    if (rIndex != i) {
                        colscheck = ifgEquipmentDetails.Rows(i).GetClientColumns();
                        if (sPort == colscheck[0]) {
                            args.IsValid = false;
                            oSrc.errormessage = "This Equipment No already exists for this Contract Reference No";
                            return;
                        }
                    }
                }
            }
        }
        function validateEquipmentno(oSrc, args) {
            var rIndex = ifgEquipmentDetails.VirtualCurrentRowIndex();
            var cols = ifgEquipmentDetails.Rows(rIndex).GetClientColumns();
            var i;
            var sPort = cols[0];
            var icount = ifgEquipmentDetails.Rows().Count
            var colscheck;

            if (icount > 0) {
                for (i = 0; i < icount; i++) {
                    if (rIndex != i) {
                        colscheck = ifgEquipmentDetails.Rows(i).GetClientColumns();
                        if (sPort == colscheck[0]) {
                            args.IsValid = false;
                            oSrc.errormessage = "Equipment Number already exists";
                            return false;
                        }
                    }
                }
            }
            var cols = ifgEquipmentDetails.Rows(ifgEquipmentDetails.CurrentRowIndex()).GetClientColumns();
            var _rowI = ifgEquipmentDetails.rowIndex;
            var sEquipmentno = args.Value
            var checkDigit;
            var sContNo;
            var strContinue = "";
            var msg = checkContainerNo(sEquipmentno);
            if (cols[0] = "") {
                args.IsValid = false;
                oSrc.errormessage = "Equipment No Required";
                return false;
            }
            if (msg == "") {
                if (getConfigSetting('ContainerCheckDigit') == 'TRUE') {
                    if (sEquipmentno.length == 10) {
                        sEquipmentno = sEquipmentno + getCheckSum(sEquipmentno.substr(0, 10));
                        ifgEquipmentDetails.Rows(_rowI).SetColumnValuesByIndex(0, sEquipmentno);
                        strContinue = "N";
                    } else {
                        checkDigit = getCheckSum(sEquipmentno.substr(0, 10));

                        if (sEquipmentno.length >= 10) {
                            if (checkDigit != sEquipmentno.substr(10)) {
                                args.IsValid = false;
                                oSrc.errormessage = "Check Digit is incorrect for the entered Equipment. Correct check digit is  " + checkDigit;
                                return;
                            }
                        }
                    }
                }
            } else {
                args.IsValid = false;
                oSrc.errormessage = msg;
                return;
            }

            if (strContinue == "") {
                validateEquipmentNo(oSrc, args);
                if (args.IsValid == false) {
                    return false
                }
            }

            var rowState = ifgEquipmentDetails.ClientRowState();
            var oCallback = new Callback();

            oCallback.add("EquipmentId", sEquipmentno);
            oCallback.add("SupplierContractId", el('hdnSupplierContractId').value);
            oCallback.add("ContractEndDate", el("hdnEndDate").value);
            oCallback.add("GridIndex", ifgEquipmentDetails.VirtualCurrentRowIndex());
            oCallback.add("RowState", rowState);
            oCallback.invoke("SupplierEquipmentDetail.aspx", "ValidateEquipment");
//            if (oCallback.getCallbackStatus()) {
//                if (oCallback.getReturnValue("bNotExists") == "true") {
//                    args.IsValid = true;
//                }
//                else {
//                    args.IsValid = false;
//                    oSrc.errormessage = "This Equipment No already Exist";
//                }
//            }
//            else {
//                showErrorMessage(oCallback.getCallbackError());
//            }
//            oCallback = null;
            //UIG issue fix for Chrome not accepting ""
            if (oCallback.getCallbackStatus()) {
                if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {
                    args.IsValid = false;
                    oSrc.errormessage = oCallback.getReturnValue("Error");
                    return false;
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                showErrorMessage(oCallback.getCallbackError());
            }
            oCallback = null;
            return;
        }

        function checkContainerNo(str) {
            var spmsg = "";
            var UnitIDA = str.substr(0, 4).toUpperCase();
            var UnitIDN = str.substr(4, 6);

            if ((UnitIDA.charCodeAt(0) < 65 || UnitIDA.charCodeAt(0) > 91) || (UnitIDA.charCodeAt(1) < 65 || UnitIDA.charCodeAt(1) > 91) || (UnitIDA.charCodeAt(2) < 65 || UnitIDA.charCodeAt(2) > 91) || (UnitIDA.charCodeAt(3) < 65 || UnitIDA.charCodeAt(3) > 91)) {//alpha check.
                spmsg = "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit<br>";
            }
            else if (isNaN(UnitIDN)) {//numeric check.
                spmsg = "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit<br>";
            }
            return spmsg;
        }

        function ValidateEndDate(oSrc, args) {
             var oCallback = new Callback();
            oCallback.add("ContractEndDate", el("hdnEndDate").value);
            oCallback.invoke("SupplierEquipmentDetail.aspx", "ValidateDate");
            if (oCallback.getCallbackStatus()) {
                if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {
                    showErrorMessage(oCallback.getReturnValue("Error"));
                    return false;
                }
            }
            else {
                showErrorMessage(oCallback.getCallbackError());
            }
            oCallback = null;
        }
//        function onAfterCB(param) {
//            if (typeof (param["Delete"]) != 'undefined') {
//                showWarningMessage(param["Delete"]);
//            }
//            else if (typeof (param["Update"]) != 'undefined' && param["Update"] != '') {
//                showWarningMessage(param["Update"]);
//            }
//            else {
//                hideMessage();
//            }
//        }