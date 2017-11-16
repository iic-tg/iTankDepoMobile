var _tabver = "1.0.2736.18915";
function Init_Tab(tid,sindex,_pb)
{
    if ( document.addEventListener ) {	
    window.document.addEventListener( "DOMContentLoaded", new Function("Init_TabRS('" + tid + "','" + sindex + "');"),false);
    }
    else
	window.document.attachEvent("onreadystatechange",new Function("Init_TabRS('" + tid + "','" + sindex + "');"));
}
function Init_TabRS(aid,sindex,_pb)
{       
    if (document.readyState == 'interactive'||document.readyState =='complete')
	{
		var a = getIObject(aid);
		var _t = eval(aid + "_Tps");
		a.TabPages = new Array();
		if (_t!="")
		{
		    var i=0;
		    for (i=0;i<_t.length;i++)
	        {
//	            if(getIObject(_t[i]).visible =='true')
//	            {
	                a.TabPages[i]=getIObject(_t[i]);
	                a.TabPages[i].onclick = function (event) {
	                    DBC();
	                    if (!event)
	                        event = window.event;
	                    if (event.preventDefault) {
	                        event.preventDefault(true);
	                        event.stopPropagation();
	                    }
	                    return this.Select(this);
	                };
	                a.TabPages[i].Select = function(obj){return iTab_Select(obj,a)};
	                a.Select = function(obj){return iTab_Select(obj,a)};
	                a.TabPages[i].Validate = function(obj){return iTab_Validate(obj,a)};
//	            }
	        }
		}
		a.selectedIndex = sindex;
		a.selectedTab = a.TabPages[sindex];	
		if(a.moveRow)
		{	
		a.moveRow(a.TabPages[sindex],a.rows.length-1);		
		}
		else
		{
		//moveRow(getIObject(tabPageObj.rows[tabPageObj.rowIndex].id),getIObject(tabPageObj.rows[tabObj.rows.length-1].id),tabObj);
		}		
	    iTab_Show(a.TabPages[sindex],a)
	}
}
function iTab_Validate(tabPageObj,tabObj)
{
    //if (typeof(tabPageObj.tpContent)!="undefined")
    if (typeof (getIAttribute(tabPageObj, "tpContent")) != "undefined")
    {
        Page_ClientValidate(getIAttribute(tabPageObj, "tpContent"));
        //Page_ClientValidate(tabPageObj.tpContent);
    }
    else
    {
        Page_ClientValidate(null);
    }
    return Page_IsValid;
}
function iTab_Select(tabPageObj,tabObj)
{
    if (getIAttribute(tabPageObj,"doValidate")=="True")
    {
        if (tabPageObj.Validate(tabObj.selectedTab)==false)
        {
            return false;
        }
    }   
    if (getIAttribute(tabPageObj, "onBeforeTabSelected") != "") {
        if (eval(getIAttribute(tabPageObj, "onBeforeTabSelected")) == false) {
            return false;
        }
    }
    iTab_Show(tabPageObj,tabObj);
    if (getIAttribute(tabPageObj,"onAfterTabSelected")!="")
    {
        eval(getIAttribute(tabPageObj,"onAfterTabSelected"));
    }
    if(tabObj.moveRow)
    {
    tabObj.moveRow(tabPageObj.rowIndex,tabObj.rows.length-1);
    }
    else{
    //moveRow(getIObject(tabPageObj.rows[tabPageObj.rowIndex].id),getIObject(tabPageObj.rows[tabObj.rows.length-1].id),tabObj);
    }
    //event.cancelBubble = true;
    checkGridHeader(); // Added for grid static header allignment
}
function iTab_Show(tabPageObj,tabObj)
{
    var i=0;
    for (i=0;i<tabObj.TabPages.length;i++)
    {
        //if (tabObj.TabPages[i].oClass.toString()!="")
		    tabObj.TabPages[i].className = "";
		//    tabObj.TabPages[i].className = tabObj.TabPages[i].oClass.toString();
		
        if (typeof(getIAttribute(tabObj.TabPages[i],"tpContent"))=="string" && getIAttribute(tabObj.TabPages[i],"tpContent")!="")
        {
            if (getIObject(getIAttribute(tabObj.TabPages[i],"tpContent")))
            {
                getIObject(getIAttribute(tabObj.TabPages[i],"tpContent")).style.display='none';
            }
        }
    } 
    if (typeof(getIAttribute(tabPageObj,"tpContent"))=="string" && getIAttribute(tabPageObj,"tpContent")!="")
    {
        if (getIObject(getIAttribute(tabPageObj,"tpContent")))
        {
            getIObject(getIAttribute(tabPageObj,"tpContent")).style.display='';
        }
    }
    if (getIAttribute(tabPageObj,"sClass").toString()!="")
        tabPageObj.className = getIAttribute(tabPageObj,"sClass").toString();
    tabObj.prevTab=tabObj.currentTab;
    tabObj.selectedTab=tabPageObj;
    tabObj.currentTab=tabPageObj.id;
    getIObject("iTabSelection_" + tabObj.id).value=tabPageObj.id;
    getIObject("iTabControlSelection_" + tabObj.id).value=tabPageObj.tpContent;
}
function moveRow(_rI, _rJ, _tB)
{	
	if(_rI.rowIndex == _rJ.rowIndex+1) {
		_tB.tBodies[0].insertBefore(_rI, _rJ);
	} else if(_rJ.rowIndex == _rI.rowIndex+1) {
		_tB.tBodies[0].insertBefore(_rJ, _rI);
	} else {
		var tmpNode = _tB.replaceChild(_rI, _rJ);
		if(typeof(_rI) != "undefined") {
			_tB.tBodies[0].insertBefore(tmpNode, _rI);
		} else {
			_tB.appendChild(tmpNode);
		}
	}
}
