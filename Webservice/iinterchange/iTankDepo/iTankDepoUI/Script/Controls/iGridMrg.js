// JScript File
// File handles the Mergable Grid
//var _ogridver = "3.0.4363.23113//290820121527";
//var _gridver = "4.0.0.1//031020131716";

function iFg_OnExpand(childGrid,obj)
{
    var pobj = obj.parentNode;
    var fgObj = getGridObj(pobj);
    iFg_HideLkpGrid(fgObj);
    var childFgObj = getIObject(childGrid);   
    iFg_HideLkpGrid(childFgObj);
    var rI = pobj.parentNode.rowIndex;
    if(iFg_CheckhasRowChanges(fgObj))
    {
        var cI = pobj.cellIndex;
        var gId = fgObj.id;    
        if (iFg_Submit(fgObj)==true)        
        {            
            var nObj = document.all[gId].rows[rI].cells[cI].children[0];
            iFg_OnExpand(childGrid,nObj);
            return;
        }
        else { return; }
    }  
    else
    {
        var RSAfgObj = iFg_GetRSAfgObj(fgObj);
        if(RSAfgObj != null)
        {
            SaveData(RSAfgObj);
            return;    
        }
    }
    if (fgObj.Search == true) {
        return;
    }
    if(fgObj.editCtrl != "" && fgObj.Grid_IsValid != false && fgObj.status != "false")
    {
        ifg_ResetGridstate(fgObj,true);
    }    
	if (typeof(obj.mode)=="undefined")
	{
		obj.mode="Expanded";
	}
	if (obj.mode=="Expanded")
	{
		iFg_Expand(obj,childGrid);
	}
	else if (obj.mode=="Collasped")
	{
	    var exRI = parseInt(getIAttribute(obj.parentNode.parentNode,"rIndex")) + parseInt(fgObj.hdrRows);	   
	    var status = iFg_Collapse(obj,exRI + 1);
	}
	if (typeof (event) != "undefined")
	    event.cancelBubble = true;
}

function iFg_ExpanderImage(obj)
{
    var _c=obj.src;
	if (_c!="")
	{
	    obj.src = getIAttribute(obj, "cimg");
	    setIAttribute(obj, "cimg", _c);
	}
}

function iFg_ExpandRow(fgObj,rIndex,cIndex)
{
    var rowIndex = rIndex + parseInt(fgObj.hdrRows);
    var obj = fgObj.rows(rIndex).cells(cIndex);
    if (obj!=null)
        iFg_Expand(obj.children[0],obj.gridId); 
}

function iFg_Expand(obj,childgridId)
{         
	var fgObj = getGridObj(obj.parentNode);		
	var childfgObj = getIObject(childgridId);
	var status = true;
	if(fgObj.Grid_IsValid != false && (fgObj.mode != "Add") && (childfgObj == null || (childfgObj.Grid_IsValid != false)))	
    {
        if(iFg_CheckIsNumeric(fgObj.exRowIndex))// && fgObj.exRowIndex!=obj.parentNode.parentNode.rIndex)
        {
	        var oobj=iFg_GetExpandCtrl(fgObj,fgObj.exRowIndex,fgObj.exCellIndex);
	        if(oobj)
	        {
	            status = iFg_Collapse(oobj,eval(parseInt(fgObj.exRowIndex)+ 1))
		    }     
        }
        if(status)
        {
            var rI = parseInt(getIAttribute(obj.parentNode.parentNode, "rIndex")) + parseInt(fgObj.hdrRows);
            status = iFg_OnBeforeExpand(fgObj, parseInt(getIAttribute(obj.parentNode.parentNode, "rIndex")));
            if (status == false)
                return false; 
            fgObj.readOnly = true;
            iFg_SetRowIndex(fgObj,getIAttribute(obj.parentNode.parentNode,"rIndex"));
            fgObj.exRowIndex = rI;
            var css = obj.parentNode.CssClass;
            fgObj.exCellIndex=obj.parentNode.cellIndex;
            var nRow = fgObj.insertRow(fgObj.exRowIndex+1);        
            var nCell = nRow.insertCell(0);
            //nCell.colSpan=fgObj.cells.length-1;
            nCell.colSpan=fgObj.rows[rI].cells.length;
            nCell.innerHTML="<div id='__fg" + childgridId + "__div'>Expand</div>";
            fgObj.mode="Expand";
            fgObj.ctrlType = "iGrid";	
            GCB(fgObj,null,childgridId);
            iFg_SetRowIndex(fgObj, getIAttribute(obj.parentNode.parentNode, "rIndex"));
    	    obj.mode="Collasped";
            iFg_ExpanderImage(obj);
        }
    }	
}

function iFg_OnBeforeExpand(fgObj,rI)
{
    var _stts = true;
    if (typeof (getIAttribute(fgObj, "cE")) == "string" && getIAttribute(fgObj, "cE") != "")
    {
        var param = new iFg_Parameters();
        _stts = eval(getIAttribute(fgObj, "cE") + "('" + rI + "',param)");
        if (_stts == null || _stts == "undefined")
            _stts = true;
        fgObj.param = iFg_ArraytoString(param.getParam());
    }
    return _stts;
}

function iFg_OnBeforeCollapse(fgObj,rI)
{
    if (typeof (getIAttribute(fgObj, "cC")) == "string" && getIAttribute(fgObj, "cC") != "")
    {
        return eval(getIAttribute(fgObj, "cC") + "('" + rI + "')");
    }
}
function iFg_Collapse(obj,rI)
{
    var status = true;
	var fgObj = getGridObj(obj.parentNode);
	var childfgObj = getIObject(getIAttribute(obj.parentNode,"gridId"));		
	if(childfgObj!=null)
    {			
	    if(childfgObj.Grid_IsValid != false && childfgObj.status != 'false')
	    {	    	    
	        var exRowIndex = fgObj.exRowIndex; 
	        if(iFg_OnBeforeCollapse(fgObj,fgObj.rowIndex)==false)
            {
                 return false;
            }
	        var oobj=iFg_GetExpandCtrl(childfgObj,childfgObj.exRowIndex,childfgObj.exCellIndex);
	        if(oobj != null)
	        {	           
	            status = iFg_Collapse(oobj,parseInt(childfgObj.exRowIndex)+1);  	            	              
	        }
	        if(status)
	        {   
	            iFg_CheckHasChanges(fgObj,childfgObj);  
	            fgObj.readOnly = false;
	            fgObj.exRowIndex="";
	            fgObj.exCellIndex="";
	            fgObj.deleteRow(rI);
	            obj.mode="Expanded";
	            iFg_ExpanderImage(obj);	    
	            status = true;
	        }
	    }
	    else
	       status = false;
	} 
	return status;
}

