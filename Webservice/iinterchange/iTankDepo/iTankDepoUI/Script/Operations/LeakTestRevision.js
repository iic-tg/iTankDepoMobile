var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgRevisionHistory');
var _RowValidationFails = false;

function initPage(sMode) {
    var EquipmentNo = getQueryStringValue(document.location.href, "EquipmentNo");
    var GateinTransaction = getQueryStringValue(document.location.href, "GateinTransaction");
    bindRevisionHistory(EquipmentNo, GateinTransaction);
}

function bindRevisionHistory(EquipmentNo, GateinTransaction) {
    var objGrid = new ClientiFlexGrid("ifgRevisionHistory");
    objGrid.parameters.add("EquipmentNo", EquipmentNo);
    objGrid.parameters.add("GateinTransaction", GateinTransaction);
    objGrid.DataBind();
}