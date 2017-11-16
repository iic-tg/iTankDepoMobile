function fnDownloadExcelFile() {
    var RowIndex = ifgEDIDetail.CurrentRowIndex();
    var _col = ifgEDIDetail.Rows(RowIndex).Columns();
    el('fmDownloadFile').src = "../Operations/ViewEDI.aspx?Customer=" + _col[0] + "&Filename=" + _col[5];
}