var HasChanges = false;
var _eqpmntNo = "";
var _giTransactionNo = "";
var _activityName = "";
var _activityNo = "";


function initPage(sMode) {
    _eqpmntNo = getQueryStringValue(document.location.href, "EquipmentNo");
    _giTransactionNo = getQueryStringValue(document.location.href, "GI_TRANS_NO");
    el('hdnGateinTransactionNo').value = _giTransactionNo;
    _activityName = getQueryStringValue(document.location.href, "ActivityName"); 
    _activityNo = getQueryStringValue(document.location.href, "ActivityNo"); 
    bindBulkEmailDetails(_eqpmntNo, _giTransactionNo, _activityName, _activityNo);
}

function bindBulkEmailDetails(_eqpmntNo, _giTransactionNo, _activityName, _activityNo) {
    var objGrid = new ClientiFlexGrid("ifgBulkEmailDetail");
    objGrid.parameters.add("EquipmentNo", _eqpmntNo);
    objGrid.parameters.add("GI_TRANS_NO", _giTransactionNo);
    objGrid.parameters.add("ActivityName", _activityName);
    objGrid.parameters.add("ActivityNo", _activityNo);
    objGrid.DataBind();
}

function openBulkEmailDetailView(_rowIndex, _bulkEmailId) {
    showModalWindow("Bulk Email Detail View/Equipment No-" + _eqpmntNo, "Tracking/BulkEmailDetailHistory.aspx?GateinTransactionNo=" + _giTransactionNo + "&EquipmentNo=" + _eqpmntNo + "&ActivityName=" + _activityName + "&ActivityNo=" + _activityNo + "&BulkEmailId=" + _bulkEmailId, "550px", "400px", "100px", "", "");
}

function CloseBulkEmailDetail() {
    ppsc().closeModalWindow();
}
