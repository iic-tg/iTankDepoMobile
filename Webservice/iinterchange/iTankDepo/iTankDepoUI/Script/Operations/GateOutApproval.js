
var tabMode = "";
//Init
//iFg_SetHeader(document.all['ifgGateOutApprovalPending']);
function initPage(sMode) {
    tabMode = "Pending";
    bindGrid('Pending');
    iFg_SetHeader(document.all['ifgGateOutApprovalPending']);
    document.getElementById("Pending").click();
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
//    el('iconFav').style.display = 'none';
    return false;
}


//Submit
function submitPage() {
    DBC();

    ifgGateOutApprovalPending.Submit();
    var oCallback = new Callback();
    oCallback.add("GridMode", tabMode);
    oCallback.invoke("GateOutApproval.aspx", "GateOutApproval");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        bindGrid(tabMode);
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
}

///Pending Grid Bind
function bindGrid(Mode) {

    var objGrid = new ClientiFlexGrid("ifgGateOutApprovalPending");
    objGrid.parameters.add("Mode", Mode);
    objGrid.DataBind();
    return false;
}


//Pending Tab Selected
function pendingTab() {
    tabMode = "Pending";
    bindGrid('Pending');
    return false;

}

//My Submit tab Selected
function mySubmitTab() {

    tabMode = "MySubmit";
    bindGrid('MySubmit');
    return false;
}

//Refresh Grid
function RefreshGrid() {
    bindGrid(tabMode);
    return false;
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



function reSizePane() {

    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
            el("divPending").style.height = $(window.parent).height() - 250 + "px";
            if (el("ifgGateOutApprovalPending") != null) {
                el("ifgGateOutApprovalPending").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }

        }
        else {
            el("divPending").style.height = $(window.parent).height() - 260 + "px";
            if (el("ifgGateOutApprovalPending") != null) {
                el("ifgGateOutApprovalPending").SetStaticHeaderHeight($(window.parent).height() - 460 + "px");
            }

        }
    }

}