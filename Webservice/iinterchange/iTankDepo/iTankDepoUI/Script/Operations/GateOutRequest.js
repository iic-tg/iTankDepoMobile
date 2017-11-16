
function initPage(sMode) {

    bindGrid();
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

    ifgGateOutRequest.Submit();
    var oCallback = new Callback();
    oCallback.invoke("GateOutRequest.aspx", "GateOutRequest");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        bindGrid();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
}

//Grid Bind

function bindGrid() {

    var objGrid = new ClientiFlexGrid("ifgGateOutRequest");
    objGrid.DataBind();
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
            el("divdisplayGatePass").style.height = $(window.parent).height() - 250 + "px";
            if (el("ifgEquipmentTypeCode") != null) {
                el("ifgEquipmentTypeCode").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }

        }
        else {
            el("divdisplayGatePass").style.height = $(window.parent).height() - 260 + "px";
            if (el("ifgEquipmentTypeCode") != null) {
                el("ifgEquipmentTypeCode").SetStaticHeaderHeight($(window.parent).height() - 460 + "px");
            }

        }
    }

}