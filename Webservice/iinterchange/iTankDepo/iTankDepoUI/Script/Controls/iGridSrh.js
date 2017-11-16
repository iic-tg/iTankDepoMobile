// JScript File
// File handles the Search Functions in Grid
//var _ogridver = "3.0.4363.23113//290820121527";
//var _gridver = "4.0.0.1//031020131716";

var _fltselimg = "flrsel.gif";
var _fltspaimg = "flrspa.gif";
var _fltrvalues = new Array("Like", "Not Like", "In", "Not In", "Between", ">", ">=", "<", "<=", "!=", "=");
function MoveNextSearchCell(tdObj) {
    var trObj = tdObj.parentNode;
    var fgObj = getGridObj(tdObj);
    var rIndex = parseInt(fgObj.hdrRows);
    var cellIndex = tdObj.cellIndex;
    var cellLen = trObj.cells.length;
    var nTdObj = null;
    var nThObj = null;
    nTdObj = fgObj.rows[rIndex].cells[cellIndex + 1];
    nThObj = fgObj.rows[parseInt(fgObj.hdrRows) - 1].cells[cellIndex + 1];
    if (nThObj != null && getIAttribute(nThObj,"aS") == "True") {
        iwc_SetFocus(nTdObj.childNodes[0].id);
    }
    else if (cellIndex < cellLen - 1) {
        iFg_HideLkpGrid(tdObj.childNodes[0]);
        MoveNextSearchCell(nTdObj);
    }
    else {
        iFg_SetFocusOnFirstCell(fgObj, rIndex);
    }
}

function MovePreviousSearchCell(tdObj) {
    var trObj = tdObj.parentNode;
    var fgObj = getGridObj(tdObj);
    var rIndex = parseInt(fgObj.hdrRows);
    var cellIndex = tdObj.cellIndex;
    var cellLen = trObj.cells.length;
    var nTdObj = null;
    var nThObj = null;
    if (cellIndex != 0) {
        nTdObj = fgObj.rows[rIndex].cells[cellIndex - 1];
        nThObj = fgObj.rows[parseInt(fgObj.hdrRows) - 1].cells[cellIndex - 1];
    }
    if (nThObj != null && getIAttribute(nThObj, "aS") == "True") {
        iwc_SetFocus(nTdObj.childNodes[0].id);
    }
    else if (cellIndex != 0 && getIAttribute(nTdObj,"cT") != 'Expand') {
        iFg_HideLkpGrid(tdObj.childNodes[0]);
        MovePreviousSearchCell(nTdObj);
    }
    else {
        if (tdObj.children.length > 0)
            iwc_SetFocus(tdObj.childNodes[0].id);
    }
}

//Search 
function iFg_SearchClick(fgObj) {
	fgObj=document.all[fgObj.id];
    if (fgObj.rC != 0) {
        if (fgObj.Grid_IsValid == false)
            return;
        _frmme = "";
        DBC();
        fg_docClick(fgObj.id);
        fgObj = getIObject(fgObj.id);
        if (fgObj.Grid_IsValid == false)
            return;
        if (fgObj.Search == true) {
            fgObj.Search = false;
            fgObj.readOnly = false;
        }
        else if (fgObj.status != 'false') {
            fgObj.Search = true;
            fgObj.readOnly = true;
        }
        if (fgObj.Search) {
            iFg_InsertSearchRow(fgObj);
            iFg_ToggleSrhBtns(fgObj, true);
            iFg_DisablePS(fgObj);
            iFg_SetHeader(fgObj);
        }
        else {
            iFg_GetSearchData(fgObj);
            if (fgObj.Search != true) {
                iFg_ToggleSrhBtns(fgObj, false);
            }
            // iFg_DisablePS(fgObj);
        }
    }
    else {
        alert('No Records Exists');
    }
}
function iFg_CancelSearch(fgObj) {
    _frmme = "";
    fgObj.mode = "";
    fg_docClick(fgObj.id);
    iFg_RemoveSearchRow(fgObj);
    iFg_ToggleSrhBtns(fgObj, false);
    iFg_DisablePS(fgObj);
    iFg_SetHeader(fgObj);
}
function iFg_ClearSearchRow(fgObj){
     if(fgObj != null)     {
         var sRow = fgObj.rows[parseInt(fgObj.hdrRows)];
         var hdrRow = fgObj.rows[parseInt(fgObj.hdrRows) - 1];
         var sCell = null;
         var hdrCell = null;
         for(var count = 0; count< sRow.cells.length;count++)
         {
             hdrCell = hdrRow.cells[count];     
             if(getIAttribute(hdrCell,"aS") == "True")
             {
                sCell = sRow.cells[count].childNodes[0];  
                sCell.value="";
             }   
         }    
     }       
}
function iFg_ToggleSrhBtns(fgObj, bol) {
    var cancelBtn = fgObj.DeleteButton;
    if (cancelBtn) {
        if (bol) {
            cancelBtn.style.visibility = "visible";
            cancelBtn.innerHTML = "<i class='" + fgObj.sCBIC + "'></i>&nbsp;" + fgObj.scBt;
        }
        else if (fgObj.allowDelete) {
            cancelBtn.innerHTML = "<i class='" + fgObj.dBIC + "'></i>&nbsp;" + fgObj.dBt;
        }
        else {
            cancelBtn.style.visibility = "hidden";
        }
    }
    var clrBtn = fgObj.AddButton
    if (clrBtn) {
        if (bol) {
            clrBtn.style.visibility = "visible";
            clrBtn.innerHTML = "<i class='" + fgObj.cBIC + "'></i>&nbsp;" + fgObj.cBt;
        }
        else if (fgObj.allowAdd) {
            clrBtn.innerHTML = "<i class='" + fgObj.aBIC + "'></i>&nbsp;" + fgObj.aBt;
        }
        else {
            clrBtn.style.visibility = "hidden";
        }
    }
    var refBtn = fgObj.RefreshButton
    if (refBtn) {
        if (fgObj.Search) {
            refBtn.style.visibility = "hidden";
        }
        else {
            refBtn.style.visibility = "visible";
           // refBtn.innerHTML = fgObj.rBt;
        }
    }
    var acBtn = fgObj.ActionButton
    if (acBtn != null) {
        for (var count = 0; count < acBtn.length; count++) {
            if (bol && (getText(getIObject(acBtn[count])).toLowerCase().indexOf("refresh") < 0)) {
                getIObject(acBtn[count]).style.visibility = "hidden";
            }
            else {
                getIObject(acBtn[count]).style.visibility = "visible";
            }
        }
    }
}
function iFg_InsertSearchRow(fgObj) {
    if (fgObj.Grid_IsValid != false) {
           var event=getEvent(event);
           if(event.stopPropagation)
             event.stopPropagation();
        event.cancelBubble = true;
        fgObj.mode = "Search";
           var hdrRow = fgObj.rows[parseInt(fgObj.hdrRows)-1];
           var row = fgObj.rows[parseInt(fgObj.hdrRows)];                                      
        var nRow = fgObj.insertRow(parseInt(fgObj.hdrRows));
           if(document.body.mergeAttributes)
            nRow.mergeAttributes(row); 
           else
            _mergeAttributes(row,nRow);
        nRow.onclick = null;
        //nRow.onclick=new Function("event.cancelBubble=true;");
        nRow.onclick = new Function("return iFg_SearchRowClick();");
           nRow.rIndex = parseInt(getIAttribute(row,"rIndex")) + 1;                   
        nRow.id = fgObj.id + "_" + nRow.rIndex + "_SR";
        for (var count = 0; count < row.cells.length; count++) {
                var hdrCell = hdrRow.cells[count];                
                var cell = row.cells[count];
            var _w = cell.clientWidth;
            var nCell = nRow.insertCell(count);
                if(document.body.mergeAttributes)
                    nCell.mergeAttributes(cell);  
                else
                    _mergeAttributes(cell,nCell);
            nCell.validate = 'false';
            var status = nCell.removeAttribute("onclick");
                var ctrlType = getIAttribute(nCell,"cT");
            var obj = null;
                if(getIAttribute(hdrCell,"aS") == "True")                {
                if (ctrlType == 'iTextBox' || ctrlType == 'iLookup' || ctrlType == 'iDate' || ctrlType == 'iContainer') {
                    /*if (typeof (nCell.iCase) != 'undefined' && nCell.iCase != "") {
                        nCell.iC = nCell.iCase;
                        nCell.iCase = null;
                    }*/
                    var sCtrl = GetEditControl(nCell);
                    var ctrlName = GetEditCtrlName(fgObj, nCell)
                    if (fgObj.aF && typeof (hdrCell.aF) == 'undefined') //   fgObj.aF)   
                    {
                        sCtrl = iFg_GetFilterCtrl(nCell, ctrlName);
                    }
                    nCell.innerHTML = sCtrl;
                        obj = nCell.childNodes[0];
                    obj.onkeyup = new Function("iFg_SBOnKeyUp(this);");
                    obj.onfocus = new Function("iFg_SBOnFocus(this);");
                    GridMouseOver(nRow.id);
                }
                GridMouseOver(nRow.id);
                    nCell.id = fgObj.id + "_" + getIAttribute(nCell,"cN") + "_" + nRow.rIndex + "_SR";                                                                        
                    if(getIAttribute(nCell,"cT") == 'iLookup')                    {
                        init_lkpCtrl(nCell.childNodes[0]);
                }
                    if(getIAttribute(nCell,"cT") == 'iDate')                    {
                    nCell.sE = '=';
                    nCell.SelRowIndex = 10;
                        var img = getIObject(nCell.childNodes[0].id + '_img');
                    img.tdId = nCell.id;
                        img.childId = nCell.childNodes[0].id;            
                    img.onclick = function () { return DateImgClick(this); };
                }
                    else if ((typeof (getIAttribute(nCell, "iC")) != 'undefined' && getIAttribute(nCell,"iC") == 'Numeric') || getIAttribute(nCell, "dT") == 'Bool'){
                    nCell.sE = '=';
                    nCell.SelRowIndex = 10;
                }
                else if ((typeof (getIAttribute(nCell,"iCase")) != 'undefined' && getIAttribute(nCell,"iCase") == 'Numeric') || getIAttribute(nCell,"dT") == 'Bool') {
                    obj = nCell.children[0];
                    obj.iCase=null;
                    nCell.sE = '=';
                    nCell.SelRowIndex = 10;
                }
                else {
                    nCell.SelRowIndex = 0;
                    nCell.sE = 'Like';
                }
                    if (nCell.childNodes[0])
                        iFg_SetTitle4SrhBox(nCell.childNodes[0],nCell.SelRowIndex);
                SetEditCtrlStyle(nCell, true, _w);
            }
        }
        iFg_SetFocusOnFirstCell(fgObj, fgObj.hdrRows);
    }
}

function iFg_SearchRowClick() {
    try {
        window.document.onclick();
        event.cancelBubble = true;
        return false;
    }
    catch (e)
    { }
}

function iFg_SetFocusOnFirstCell(fgObj,rIndex){
   fgObj.rows[parseInt(rIndex)].scrollIntoView(false); 
   var tdObj = fgObj.rows[parseInt(rIndex)].cells[0];
   var thObj = fgObj.rows[parseInt(fgObj.hdrRows)-1].cells[0];
   if(getIAttribute(thObj,"aS") == "True")
   {          
        iwc_SetFocus(tdObj.childNodes[0].id);                
   }
   else
   {
        MoveNextSearchCell(tdObj);
    }
}

function iFg_RemoveSearchRow(fgObj){
   if(fgObj != null)   {
      var row = fgObj.rows[parseInt(fgObj.hdrRows)];    
      fgObj.deleteRow(parseInt(fgObj.hdrRows)); 
      fgObj.readOnly = false;
      fgObj.Search = false;
      iFg_HideFilterDiv(fgObj);      
   }
}

function iFg_HideFilterDiv(fgObj) {
    var fEDiv = getIObject(fgObj.id + "_FE");
    var tdObj = null;
    if (fEDiv != null && fEDiv.style.display == 'block') {
        fEDiv.style.display = 'none';
        iFg_tgFlrFrm(fEDiv, "none")
    }
}

function iFg_GetSearchData(fgObj) {
    iFg_HideFilterDiv(fgObj);
    var sRow = fgObj.rows[parseInt(fgObj.hdrRows)];
    var sCell = null;     
    var sC = "";
    for (var count = 0; count < sRow.cells.length; count++) {
        sCell = sRow.cells[count];
        if(sCell.childNodes[0]!=null && sCell.childNodes[0].value != ""){
            sC += getIAttribute(sCell,"cN") + ifg_lkpsplitter; 
            sC += sCell.sE + ifg_lkpsplitter;
                sC += sCell.childNodes[0].value + ifg_colsplitter; 
        }
    }
    sC = sC.substring(0, sC.length - ifg_colsplitter.length);
    fgObj.param = sC;
    GCB(fgObj);
}

function iFg_GetFilterCtrl(tdObj, ctrlName) {
    //input += "<img src='" + get_icImgs("Search.jpg") + "' id= '" + ctrlName + "_FilterImg' language='javascript' name= '" + ctrlName + "_FilterImg'  onclick = 'iFg_GetFilterExp(this)' align='AbsMiddle'/>";
    input += "<i style='color:#ffc000'class='icon-filter' id='" + ctrlName + "_FilterImg' language='javascript' name= '" + ctrlName + "_FilterImg'  onclick = 'iFg_GetFilterExp(this)' align='AbsMiddle'></i>";
    return input;
}

function iFg_GetFilterExp(obj){
    var event = getEvent(event);
    var _obj;
    if (!obj)
        _obj = event.srcElement || event.target;
    else
        _obj = obj;
   var tdObj = _obj.parentNode; 
   var fgObj = getGridObj(tdObj);
   var fE = getIObject(fgObj.id + "_FE")
   if(fE == null){       
       fE = document.createElement("DIV");	
       fE.id = fgObj.id + "_FE"
       fE.style.position="absolute";          
       fE.style.width = "120px";
       fE.innerHTML = iFg_GetTable();
       fE.innerHTML = fE.innerHTML + " "
       fE.style.display="block";      
       setPosition(_obj,fE);  
       document.body.appendChild(fE);
       fE.cell = tdObj.id;
       iFg_HideFilterRow(tdObj,fE);  
       iFg_FlrFrm(fE,fgObj.id);
       iFg_tgFlrFrm(fE,"block")
   }
   else if(fE.style.display == "none"){
        fE.style.display="block";      
        setPosition(_obj,fE);  
        fE.cell = tdObj.id;  
        if(typeof(fE.SelRIndex)!='undefined' && fE.SelRIndex != ""){
            var tbl = fE.childNodes[0];
            var pSelRowIndex = fE.SelRIndex;
            //tbl.rows[pSelRowIndex].cells[0].childNodes[0].src = get_icImgs(_fltspaimg); 
            tbl.rows[pSelRowIndex].cells[0].childNodes[0].className = "icon-check-empty";
        }   
        iFg_HideFilterRow(tdObj,fE);   
        iFg_tgFlrFrm(fE,"block")
   }
   else{
       fE.style.display="none";
       iFg_tgFlrFrm(fE,"none")
       if(fE.cell != tdObj.id){
           iFg_GetFilterExp(_obj);
           return;           
       }
   }
    if(event.stopPropagation)
       event.stopPropagation();
    else
       event.cancelBubble = true;
}
function iFg_tgFlrFrm(flrdiv, val) {
    var fE = getIObject(flrdiv.id + "frm");
    if (fE) {
        fE.style.display = val;
        if (val == "block") {
            fE.style.width = parseInt(flrdiv.clientWidth) - 2 + "px";
            fE.style.height = parseInt(_fr * 20) + 1 + "px";
            fE.style.top = parseInt(flrdiv.style.top) + 1 + "px";
            fE.style.left = parseInt(flrdiv.style.left) + 1 + "px";
        }
        else        {
            fE.style.width = "0px";
            fE.style.height = "0px";
            fE.style.top = "0px";
            fE.style.left = "0px";
        }
    }
}
function iFg_FlrFrm(flrdiv, id) {
    var fE;
    fE = document.createElement("IFRAME");
    fE.id = id + "_FEfrm"
    fE.style.position="absolute";  
    fE.scrolling="no";  
    fE.frameBorder="0";  
    flrdiv.style.zIndex = 999;
    fE.style.zIndex = flrdiv.style.zIndex - 1;
    document.body.appendChild(fE);
    iFg_tgFlrFrm(flrdiv, "block");
}
var _fr = 0;
function iFg_HideFilterRow(tdObj, fE) {
    var iCase = tdObj.iCase;
   var cT = getIAttribute(tdObj,"cT");
    var dT = getIAttribute(tdObj,"dT");
    var tbl = fE.childNodes[0];    
    for (var count = 0; count <= 10; count++) {
        tbl.rows[count].style.display = 'block';
        //tbl.rows[count].cells[0].childNodes[0].src = get_icImgs(_fltspaimg); 
        tbl.rows[count].cells[0].childNodes[0].className = "icon-check-empty";
    }
    if (typeof (dT) != 'undefined' && dT == 'Bool') {
        for (var count = 0; count <= 10; count++) {
            tbl.rows[count].style.display = 'none';
        }
        if (typeof (tdObj.SelRowIndex) == "undefined") {
            tdObj.SelRowIndex = 10;
            fE.SelRIndex = 10;
        }
        tbl.rows[10].style.display = 'block';
        _fr = 1;
    }
    else if (cT == 'iDate') {
        if (typeof (tdObj.SelRowIndex) == "undefined") {
            tdObj.SelRowIndex = 4;
            fE.SelRIndex = 4;
        }
        tbl.rows[0].style.display = 'none';
        tbl.rows[1].style.display = 'none';
        //tbl.rows[10].style.display = 'none';
        _fr = 9;
    }
    else if (iCase != 'Numeric') {
        tbl.rows[4].style.display = 'none';
        tbl.rows[5].style.display = 'none';
        tbl.rows[6].style.display = 'none';
        tbl.rows[7].style.display = 'none';
        tbl.rows[8].style.display = 'none';
        tbl.rows[9].style.display = 'none';
        if (typeof (tdObj.SelRowIndex) == "undefined") {
            tdObj.SelRowIndex = 0;
            fE.SelRIndex = 0;
        }
        _fr = 5;
    }
    else if (iCase == 'Numeric') {
        if (typeof (tdObj.SelRowIndex) == "undefined") {
            tdObj.SelRowIndex = 10;
            fE.SelRIndex = 10;
        }
        tbl.rows[0].style.display = 'none';
        tbl.rows[1].style.display = 'none';
        _fr = 9;
    }
    if(tdObj.SelRowIndex.toString() != "") {
        //tbl.rows[tdObj.SelRowIndex].cells[0].childNodes[0].src = get_icImgs(_fltselimg);
        tbl.rows[tdObj.SelRowIndex].cells[0].childNodes[0].className = "icon-check";
        tbl.rows[tdObj.SelRowIndex].cells[0].childNodes[0].mode = 'Select';
        //tdObj.sE = tbl.rows(tdObj.SelRowIndex).cells(1).innerText;
        tdObj.sE = _fltrvalues[tdObj.SelRowIndex];
    }
}
function setPosition(l, lG) {
    if (lG) {
        var s = oL(l)
        if (s > parseInt(window.document.body.clientWidth) - 100) {
            s = s - 100;
        }
        lG.style.left = s + "px";
        lG.style.top = oT(l) + l.offsetHeight - 1 + "px";
    }
}
function iFg_MakeTable() {
	return "<table border=0 cellpadding=0 cellspacing=0 class='flrtbl' width=125px>";
}

function iFg_MakeTableEnd() {
    return "</table >";
}

function iFg_MakeRow(strText, strTitle) {
    return "<tr class ='flrmot' height=20px onmouseover='iFg_FlrMvr(this);' onmouseout='iFg_FlrMot(this);'>" +
	       "<td width = '20px' class ='flrmnu' align='center'><i class=''></i></td>" +
	       "<td width = '100px' onclick ='iFg_Onselect(this)' title='" + strTitle + "'><nobr>&nbsp;" + strText +
           "&nbsp;</nobr></td></tr>\r\n";
}
function iFg_FlrMvr(obj)
{   if(navigator.userAgent.indexOf("AppleWebKit")!=-1){
     toogleCss(obj,"_flrmot",false);    
     toogleCss(obj,"_flrmvr",true);
    }
    else{
     toogleCss(obj,"flrmot",false);    
     toogleCss(obj,"flrmvr",true);
    }
         
}
function iFg_FlrMot(obj)
{   
    if(navigator.userAgent.indexOf("AppleWebKit")!=-1){
    toogleCss(obj,"_flrmvr",false);
    toogleCss(obj,"_flrmot",true);    
    }
    else{
    toogleCss(obj,"flrmvr",false);
    toogleCss(obj,"flrmot",true); 
    }
}
function iFg_GetTable() {
    var valArray = iFg_GetFilterValues();
    var tAr = iFg_GetFilterTitle();
    var tbl = iFg_MakeTable();
    for (var count = 0; count <= 10; count++) {
        tbl += iFg_MakeRow(valArray[count], tAr[count]);
    }
    tbl += iFg_MakeTableEnd();
    return tbl;
}

function iFg_GetFilterValues() {
    return new Array("Similar", "Not Similar", "Contains", "Does Not Contain", "Between", ">", ">=", "<", "<=", "!=", "Equal")
}

function iFg_GetFilterTitle() {
    return new Array("Similar", "Not Similar", "To search the values in a list. Enter the values separated by comma.", "To search the values not in a list. Enter the values separated by comma.", "To search between the two values. Enter the values separated by comma.StartValue should be less than EndValue.", "Greater than", "Greater than or Equal to", "Less than", "Less than or Equal to", "Not Equal to", "Equal to")
}
function iFg_GetSrhBoxTitle() {
    return new Array("Like 'val'", "Not Like 'val'", "In ('val')", "Not in 'val'", "Between 'val' and 'val1'", "Greater than 'val'", "Greater than or Equal to 'val'", "Less than 'val'", "Less than or Equal to 'val'", "Not Equal to 'val'", "Equal to 'val'")
}
function iFg_GetFilterIndex(value) {
    var valArray = iFg_GetFilterValues();
    for (var count = 0; count <= 10; count++) {
        if (trimAll(value) == trimAll(valArray[count]))
            return count;
    }
}

function iFg_Onselect(obj) {
    var pCellIndex = obj.cellIndex;
    if (typeof (obj.mode) == 'undefined') {
        obj.mode = 'Select';
    }
    else if (obj.mode == 'Select') {
        obj.mode = 'UnSelect';
    }
    else {
        obj.mode = 'Select';
    }
    iFg_SetFilter(obj);
}

function iFg_SetFilter(obj){    
    var d = obj.parentNode.parentNode.parentNode.parentNode;
    var tbl = obj.parentNode.parentNode.parentNode;        
    var rIndex = obj.parentNode.rowIndex;
    var tdObj = getIObject(d.cell);   
    var fgObj = getGridObj(tdObj);
    var tdImg = tbl.rows[rIndex].cells[0];       
    if (typeof(tdObj.SelRowIndex)!="undefined" && tdObj.SelRowIndex.toString() != "" && tdObj.SelRowIndex == rIndex) {    
        obj.mode = 'Select';  
        iFg_HideFilterDiv(fgObj); 
        iwc_SetFocus(tdObj.childNodes[0].id);    
        return;
    }
    else if (typeof(tdObj.SelRowIndex)!="undefined" && tdObj.SelRowIndex.toString() != "" && tdObj.SelRowIndex != rIndex)
    {
        //tbl.rows[tdObj.SelRowIndex].cells[0].childNodes[0].src = get_icImgs(_fltspaimg);     
        tbl.rows[tdObj.SelRowIndex].cells[0].childNodes[0].className = "icon-check-empty";        
        obj.mode = "Select";
    }
    if(obj.mode == 'Select')
    {
        //tdImg.childNodes[0].src = get_icImgs(_fltselimg);        
        tdImg.childNodes[0].className = "icon-check";
    }          
    else
    {
        //tdImg.childNodes[0].src = get_icImgs(_fltspaimg);              
        tdImg.childNodes[0].className = "icon-check-empty";
    }    
    if(obj.mode == 'Select')
    {  
        tdObj.sE = _fltrvalues[rIndex];
        //tdObj.sE = obj.innerText;
        tdObj.SelRowIndex = rIndex;
        d.SelRIndex = rIndex;
        iFg_SetTitle4SrhBox(tdObj.childNodes[0],rIndex);
    }
    else {
        tdObj.sE = "";
        tdObj.SelRowIndex = "";
        d.SelRIndex = "";
    }     
    iFg_HideFilterDiv(fgObj); 
    iwc_SetFocus(tdObj.childNodes[0].id);    
}
function iFg_SetTitle4SrhBox(txt, index) {
    if (typeof (index) != "undefined" && index.toString() != "") {
        if (index == 4)
            txt.title = iFg_GetSrhBoxTitle()[index].replace("val", txt.value.split(",")[0]).replace("val1", txt.value.split(",")[1]);
        else
            txt.title = iFg_GetSrhBoxTitle()[index].replace("val", txt.value);
    }
    txt.removeAttribute("maxLength");
}
function iFg_SBOnKeyUp(obj){
    var tdObj = obj.parentNode;
    iFg_SetTitle4SrhBox(obj,tdObj.SelRowIndex);             
}
function iFg_SBOnFocus(obj) {
    getGridObj(obj.parentNode).editCtrl = obj.id;
}