var HasChanges = false;

//function openEDIEmailDetail(_ediEmailId) {
//    showModalWindow("EDI Email Detail View", "EDI/EdiEmailDetail.aspx?EdiId=" + _ediEmailId , "550px", "400px", "100px", "", "");
//}

function initPage(sMode) {
    var _ediid = getQueryStringValue(document.location.href, "EdiId");
    bindEdiEmailDetails(_ediid);
}

function bindEdiEmailDetails(_ediid) {
    var objGrid = new ClientiFlexGrid("ifgEdiEmailDetail");
    objGrid.parameters.add("EDIId", _ediid);
    objGrid.DataBind();
}

function CloseBulkEmailDetail() {
    ppsc().closeModalWindow();
}
