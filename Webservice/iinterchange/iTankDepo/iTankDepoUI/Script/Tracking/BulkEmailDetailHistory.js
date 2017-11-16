var _eqpmntNo = "";
var _giTransactionNo = "";
var _activityName = "";
var _activityNo = "";

function initPage() {
    _eqpmntNo = getQueryStringValue(document.location.href, "EquipmentNo");
    _giTransactionNo = getQueryStringValue(document.location.href, "GateinTransactionNo");   
    _activityName = getQueryStringValue(document.location.href, "ActivityName");
    _activityNo = getQueryStringValue(document.location.href, "ActivityNo");  
}

function CloseBulkEmailDetailView() {  
    showModalWindow("Bulk Email Detail/Equipment No-" + _eqpmntNo, "Tracking/BulkEmailDetail.aspx?GI_TRANS_NO=" + _giTransactionNo + "&EquipmentNo=" + _eqpmntNo + "&ActivityName=" + _activityName + "&ActivityNo=" + _activityNo, "900px", "250px", "100px", "", "");
    return true;
    ppsc().closeModalWindow();
}
