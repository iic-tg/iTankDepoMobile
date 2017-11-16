var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgTariffType');
var _RowValidationFails = false;

//Initialize page while loading the page based on page mode
function initPage(sMode) {
  
    if (sMode == MODE_NEW) {
        bindGridNew();
        setDefaultValues();
        clearValues();
        resetValidators();
        hideDiv("divLkpAgnt");
        showDiv("divLkpCstmr");
        setReadOnly('lkpTRRF_TYP', false);
        setReadOnly('lkpEqpType', false);
        setReadOnly('lkpCSTMR', true);
        setReadOnly('lkpAgent', true);
        setReadOnly('chkActiveBit', false);
        setPageMode("new");
    }
    else {
        // clearValues();
        DisableControls();
        resetValidators();
        setReadOnly('lkpTRRF_TYP', true);
        setReadOnly('lkpEqpType', true);
        setReadOnly('lkpCSTMR', true);
        setReadOnly('lkpAgent', true);
        setReadOnly('chkActiveBit', true);
        setPageMode("edit");
        bindGrid();
        setDefaultValues();
    }

}
function DisableControls() {
//    DisableControls1();
    if (el("lkpTRRF_TYP").SelectedValues[1] == "STANDARD") {
        clearLookupValues("lkpCSTMR");
        clearLookupValues("lkpAgent");
        setReadOnly('lkpCSTMR', true);
        setReadOnly('lkpAgent', true);
        hideDiv("divLkpAgnt");
        showDiv("divLkpCstmr");
    }
    else if (el("lkpTRRF_TYP").SelectedValues[1] == "CUSTOMER") {
        clearLookupValues("lkpAgent");
        setReadOnly('lkpAgent', true);
        setReadOnly('lkpCSTMR', false);
        hideDiv("divLkpAgnt");
        showDiv("divLblCstmr");
        showDiv("divLkpCstmr");
        el('lkpCSTMR').tabIndex = 3;
    }
    else if (el("lkpTRRF_TYP").SelectedValues[1] == "AGENT") {
        clearLookupValues("lkpCSTMR");
        setReadOnly('lkpCSTMR', true);
        setReadOnly('lkpAgent', false);
        hideDiv("divLkpCstmr");
        showDiv("divLblCstmr");
        showDiv("divLkpAgnt");
        el('lkpAgent').tabIndex = 3;
    }
}


function RequiredValidation() {

    if (el("lkpTRRF_TYP").SelectedValues[0] == 142) {
        if (el("lkpCSTMR").SelectedValues[0] == '') {
            showErrorMessage("Customer is Required");
            return  false;
        }
    }
    else if (el("lkpTRRF_TYP").SelectedValues[0] == 143) {
         if (el("lkpAgent").SelectedValues[0] == '') {
              showErrorMessage("Agent is required");
               return false;
         }
    }

    }



//function DisableControls1() {
//    var oCallback = new Callback();

//    oCallback.add("bv_i64TRFF_CD_TYP", el("lkpTRRF_TYP").SelectedValues[0]);
//    oCallback.add("bv_i64TRFF_CD_EQP_TYP_CD", el("lkpEqpType").SelectedValues[0]);
//    oCallback.add("bv_i64GTRFF_CD_CSTMR_ID", el("lkpCSTMR").SelectedValues[0]);
//    oCallback.add("bv_i64GTRFF_CD_AGNT_ID", el("lkpAgent").SelectedValues[0]);

//    oCallback.add("bv_blnACTV_BT", el("chkActiveBit").checked);
//    oCallback.add("PageMode", getPageMode());
//    oCallback.add("wfData", el("WFDATA").value);

//    oCallback.invoke("CustomerTariffCode.aspx", "ValidateTariffHeader");
////    if (oCallback.getCallbackStatus()) {
////        setPageMode(MODE_EDIT);
////        showInfoMessage(oCallback.getReturnValue("Message"));
////        HasChanges = false;
////        setPageID(oCallback.getReturnValue("ID"));
////        bindGrid();
////        resetHasChanges("ifgTariffType");

////        //        setFocus();

////    }
////    else {
////        showErrorMessage(oCallback.getCallbackError());
////    }
////    oCallback = null;
//}

function clearValues() {
    clearLookupValues("lkpTRRF_TYP");
    clearLookupValues("lkpEqpType");
    clearLookupValues("lkpCSTMR");
    clearLookupValues("lkpAgent");
  }

//This method used to submit the changes to database
  function submitPage() {
      Page_ClientValidate();
      if (!Page_IsValid) {
          return false;
      }
      var sMode = getPageMode();

      if (getPageChanges()) {
          if (RequiredValidation() == false) {
              return false;
          }
//          var count = ifgTariffType.Rows().Count;
//          if (count == 0) {
//              showErrorMessage("Atleast one Tariff has to be entered");
//              return false;
//          }
//          //To submit the changes to server
//          if (ifgTariffType.Submit(true) == false || _RowValidationFails) {
//              return false;
//          }
          DBC();
          if (sMode == MODE_NEW) {
              if (validateCode() == true) {
                  createTariffCode();
              }
              else {
                  return false;

              }

          }
          else if (sMode == MODE_EDIT) {
              updateTariffCode();
          }
      }

      else {

          showInfoMessage('No Changes to Save');
      }

      return true;
  }



function createTariffCode() {
    var oCallback = new Callback();

    oCallback.add("bv_i64TRFF_CD_TYP", el("lkpTRRF_TYP").SelectedValues[0]);
    oCallback.add("bv_i64TRFF_CD_EQP_TYP_ID", el("lkpEqpType").SelectedValues[0]);
     oCallback.add("bv_i64GTRFF_CD_CSTMR_ID", el("lkpCSTMR").SelectedValues[0]);
     oCallback.add("bv_i64GTRFF_CD_AGNT_ID", el("lkpAgent").SelectedValues[0]);

     oCallback.add("bv_blnACTV_BT", el("chkActiveBit").checked);
    oCallback.add("PageMode", getPageMode());
    oCallback.add("wfData", el("WFDATA").value);

    oCallback.invoke("CustomerTariffCode.aspx", "CreateTariffCode");
    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setPageID(oCallback.getReturnValue("ID"));
        el("hdnTariffId").value = oCallback.getReturnValue("ID");
        bindGrid();
        resetHasChanges("ifgTariffType");

//        setFocus();
     
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

//This method used to update/insert entered row to database
function updateTariffCode() {
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.add("ID", el("hdnTariffId").value);
    oCallback.add("bv_blnACTV_BT", el("chkActiveBit").checked);
    oCallback.invoke("CustomerTariffCode.aspx", "UpdateTariffCode");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindGrid();
        resetHasChanges("ifgTariffType");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

function checkDuplicate(oSrc, args) {
    var rIndex = ifgTariffType.VirtualCurrentRowIndex();
    var cols = ifgTariffType.Rows(rIndex).GetClientColumns();
    var i;
    var sPort = cols[0];
    var icount = ifgTariffType.Rows().Count
    var colscheck;

    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgTariffType.Rows(i).GetClientColumns();
                if (sPort == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "This Code already exists";
                    return;
                }
            }
        }
    }
}

//FOR FILTER FOR SUB ITEM WHILE SELECTING ITEM IN LINE DETAIL GRID
function applySubItemFilter() {
    var rIndex = ifgTariffType.CurrentRowIndex();
    var cColumns = ifgTariffType.Rows(rIndex).GetClientColumns();
    return "ITM_ID IN ('" + cColumns[3] + "')  AND ACTV_BT=1";
}


//This method is to validate the duplicate code entered
function validateCode() {
//    var iRowIndex = ifgTariffType.rowIndex;
//    var oCols = ifgTariffType.Rows(iRowIndex).GetClientColumns();
//    var sCols = ifgTariffType.Rows(iRowIndex).Columns();
//    var sCode = oCols[0];

//    var rowState = ifgTariffType.ClientRowState();
//    if (rowState != 'Added') {
//        if (sCols[1] == sCode) {
//            return false;
//        }
//    }


    var oCallback = new Callback();
    oCallback.add("bv_i64TRFF_CD_TYP", el("lkpTRRF_TYP").SelectedValues[0]);
    oCallback.add("bv_i64TRFF_CD_EQP_TYP_ID", el("lkpEqpType").SelectedValues[0]);
    oCallback.add("bv_i64GTRFF_CD_CSTMR_ID", el("lkpCSTMR").SelectedValues[0]);
    oCallback.add("bv_i64GTRFF_CD_AGNT_ID", el("lkpAgent").SelectedValues[0]);
//    oCallback.add("Code", sCode);
//    oCallback.add("GridIndex", ifgTariffType.VirtualCurrentRowIndex());
//    //Newly added code is available in existing data only in database.
//    oCallback.add("RowState", rowState);
//    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("CustomerTariffCode.aspx", "ValidateCode");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("valid") == "true") {
            return true;
        }
        else {
            showErrorMessage("Tariff combination Already exist");
            return false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
//    oCols = null;
}

//This method is used to show warning message after a row is deleted
function onAfterCB(param) {
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

function bindGrid() {
    var sPageTitle = getPageTitle();
    var ifgTariffType = new ClientiFlexGrid("ifgTariffType");
    ifgTariffType.parameters.add("TariffID", getPageID());
    ifgTariffType.parameters.add("WFDATA", el("WFDATA").value);
    ifgTariffType.DataBind();
    //Fix for BreadCrums
    // $("#spnHeader").text(sPageTitle);
    $("#spnHeader").text("Master >>Repair Process >>" + "Tariff");
}

function bindGridNew() {
    var sPageTitle = getPageTitle();
    var ifgTariffType = new ClientiFlexGrid("ifgTariffType");
    ifgTariffType.parameters.add("TariffID", "");
    ifgTariffType.parameters.add("WFDATA", el("WFDATA").value);
    ifgTariffType.DataBind();
    //Fix for BreadCrums
    // $("#spnHeader").text(sPageTitle);
    $("#spnHeader").text("Master >>Repair Process >>" + "Tariff");
}

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgTariffType.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgTariffType.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    if (ifgTariffType.Rows().Count == "0")
        ifgTariffType.Rows(0).SetColumnValuesByIndex(10, true);
    else if (ISNullorEmpty(iCurrentIndex) == false)
        ifgTariffType.Rows(iCurrentIndex).SetColumnValuesByIndex(10, true);
    resetHasChanges("ifgTariffType");
    return true;
}

function resetGridChanges() {
    resetHasChanges("ifgTariffType");
}

function openUpload() {
    if (el("hdnTariffId").value == 0) {
        showErrorMessage("Please submit header detail before uploading...");
        return false;
    }
    if (checkRights()) {
        var sPageTitle = getPageTitle();
        var SchemaId = getQStr("activityid");
        var tablename = "TARIFF_CODE_DETAIL";
        showModalWindow("Tariff - Upload", "Upload.aspx?SchemaID=" + SchemaId + "&tablename=" + tablename + "&" + el("WFDATA").value + "&TariffId=" + el("hdnTariffId").value, "1000px", "460px", "", "", "");
    }
    else
        return false;
}

function SubItemFilter() {
    var iRowIndex = ifgTariffType.rowIndex;
    var oCols = ifgTariffType.Rows(iRowIndex).GetClientColumns();
    var str;
    str = "ITM_ID=" + oCols[3];
    return str;
}


function formatManHours(obj) {
if (!obj.value ==''){
    var ManHours = new Number;
    ManHours = parseFloat(obj.value);
    obj.value = ManHours.toFixed(2);
    }
}

//Window Resize
if (window.$) {
    $(document).ready(function () {

        reSizePane();
    });
}

$(window.parent).resize(function () {
    reSizePane();
});

//function HideField()
//{
//    if (el("lkpTRRF_TYP").SelectedValues[1] == 'CUSTOMER') {
//        el("tdLkpAGNT").style.display = "none";
//        el("tdLblAGNT").style.display = "none";
//        }
//        else if (el("lkpTRRF_TYP").SelectedValues[1] == 'AGENT') {
//            el("tdLkpCSTMR").style.display = "none";
//            el("tdLblCSTMR").style.display = "none";
//        }
//        else if (el("lkpTRRF_TYP").SelectedValues[1] == 'DEFAULT') {
//            el("tdLkpAGNT").style.display = "none";
//            el("tdLblAGNT").style.display = "none";
//            el("tdLkpCSTMR").style.display = "none";
//            el("tdLblCSTMR").style.display = "none";
//        }
//}


function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
          //  el("tabTariffCode").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgTariffType") != null) {
                el("ifgTariffType").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }

        }
        else {
         //   el("tabTariffCode").style.height = $(window.parent).height() - 360 + "px";
            if (el("ifgTariffType") != null) {
                el("ifgTariffType").SetStaticHeaderHeight($(window.parent).height() - 460 + "px");
            }

        }
    }


}