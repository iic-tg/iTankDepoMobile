var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgAudit');
//Initialize page while loading the page based on page mode


function initPage(sMode) {     
   
    bindGrid();
    reSizePane();
}
function bindGrid() {
    var ifgAudit = new ClientiFlexGrid("ifgAudit");
    ifgAudit.DataBind();
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
        if ($(window.parent).height() < 680) {
            el("tabAudit").style.height = $(window.parent).height() - 209 + "px";
            if (el("ifgAudit") != null) {
                el("ifgAudit").SetStaticHeaderHeight($(window.parent).height() - 269 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("tabAudit").style.height = $(window.parent).height() - 220 + "px";
            if (el("ifgAudit") != null) {
                el("ifgAudit").SetStaticHeaderHeight($(window.parent).height() - 290 + "px");
            }

        }
        else {
            el("tabAudit").style.height = $(window.parent).height() - 206 + "px";
            if (el("ifgAudit") != null) {
                el("ifgAudit").SetStaticHeaderHeight($(window.parent).height() - 259 + "px");
            }
            
        }
    }

}