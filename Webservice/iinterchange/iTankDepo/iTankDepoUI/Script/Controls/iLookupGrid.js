<!--
var _lgver="1.0.2456.23649";
function g_bD(_Ele,xmlRequest)
{
	//alert(xmlRequest.responseText)
	var g=_Ele.opg;
	var s=xmlRequest.responseText.substr(xmlRequest.responseText.indexOf("opg'>")+5,xmlRequest.responseText.substr(xmlRequest.responseText.indexOf("opg'>")+5).lastIndexOf("</div>"));
	if (s!="")
	{
	    var sv="";
	    if (typeof(_Ele.grid.selectedDataKeyValues)!="undefined")
	        sv = _Ele.grid.selectedDataKeyValues;
		var o=g.innerHTML;
		g.innerHTML="";
		g.innerHTML=s;
		//alert(s);
		if (document.all[_Ele.grid.id]==null)
		{
			g.innerHTML="";
			g.innerHTML=o;
		}
		else
		{
		    Init_LkpGdRS(_Ele.grid.id);
			_Ele.rowEle=null;
			_Ele.grid.selectedDataKeyValues = sv;
			//g_Af(_Ele.grid.id,_Ele.grid.id+"_Footer");
			if (typeof(_Ele.rowEle)=="undefined" || _Ele.rowEle==null)
				_Ele.rowEle=_Ele.grid.id + "_tr_0";
			//g.selectedDataKeyValues= getIObject(_Ele.grid.id + "__hidden").value;
			var _lkpRow=getIObject(_Ele.rowEle);
			if (_lkpRow!=null)
				g_onLkpMover(_lkpRow.id);
		}
	}
	ilg_SetFocus(_Ele);
}
function ilg_SetFocus(obj)
{
	try{
		if (obj.style.display!="none" && obj.style.visibility!="hidden")
			obj.focus();
	}catch (e){}
}
function Init_LkpGdRS(gId)
{
	if (document.readyState=="complete")
	{
		var g = getIObject(gId);
		var a = getIObject(g.parentElement.sB);
		g.selectedDataKeyValues= getIObject(gId + "__hidden").value;
		g.searchBox=a;
		g.searchLabel=l;
		var l = getIObject(g.parentElement.sL);
		a.grid=g;
		a.opg=getIObject(gId + "_opg");
		a.onkeyup=lg_onKeyup;
		a.onpropertychange=lg_onPropertyChange;
		a.onkeydown=lg_onKeydown;
		a.hasChanges=false;
		a.onsubmit=new Function("return fSGD();");
		a.oldValue=a.value;
		a.rowEle=null;
		a.cpIndex=0;
		if (a.aPage=="true")
			g_Af(gId,gId+"_Footer");
		if (typeof(a.sInfo)=="undefined" || a.sInfo=="true")
			g_cD(gId);
		if (typeof(a.rowEle)=="undefined" || a.rowEle==null)
			a.rowEle=gId + "_tr_0";
		var _lkpRow=getIObject(a.rowEle);
		if (_lkpRow!=null)
			g_onLkpMover(_lkpRow.id);
		ilg_SetFocus(a);
	}
}
function lG_pC(gId,pIndex,mode)
{
	if (pIndex==null)
	{
		if (event.srcElement.innerHTML!="")
			pIndex = eval(event.srcElement.innerHTML)-1;
	}
	g_pc(getSearchBox(gId).id,pIndex,null);
	return false;
}
function g_pc(tId,pIndex,mode)
{
	if (mode!=null)
	{
		if (mode=="up")
			pIndex=eval(pIndex)+1;
		else if (mode=="down")
			pIndex=eval(pIndex)-1;
		if (!(pIndex >= 0 && pIndex < eval(getIObject(tId).tpSize)))
			return false;
	}
	
	getIObject(tId).cpIndex=pIndex;
	g_CB(getIObject(tId),"page");
}
function g_Af(g,f)
{
	var myTR;
	var gD=document.getElementById(g);
	myTR=gD.rows(gD.rows.length-1);
	var mytable = document.getElementById(f);
	var mytbody = document.getElementById(f+"_Tbody");
	var myNewtbody = document.createElement("tbody");
	myNewtbody.id = f + "_myTbody";
	var docFragment = document.createDocumentFragment();
	docFragment.appendChild(myTR);
	myNewtbody.appendChild(docFragment);
	mytable.replaceChild(myNewtbody, mytbody);
	if (gD.clientWidth)
	    mytable.width=gD.clientWidth;
	mytable.style.cssText=gD.style.cssText;
	mytable.className=gD.className;/*alert(gD.rows(gD.rows.length-1).style.cssText)
	mytable.rows(0).style.cssText=gD.rows(gD.rows.length-1).style.cssText;
	mytable.rows(0).className=gD.rows(gD.rows.length-1).className;*/
}
function CheckAll(me)
{
    var index = me.name.indexOf('_');  
    var prefix = me.name.substr(0,index); 
    for(i=0; i<document.forms[0].length; i++) 
    { 
        var o = document.forms[0][i]; 
        if (o.type == 'checkbox') 
        { 
            if (me.name != o.name) 
            {
                if (o.name.substring(0, prefix.length) == prefix) 
                {
                    // Must be this way
                    o.checked = !me.checked; 
                    o.click(); 
                }
            }
        } 
    }
}
function CheckMe(me)
{
    if (!me.checked)
    {
        var index = me.name.indexOf('$');  
        var prefix = me.name.substr(0,index);
        if (document.all[prefix+"_Hdr"])
        {
            document.all[prefix+"_Hdr"].checked=false;
        }
    }
    TogSelDK(me.gId,me.dv,me.checked)
}
function TogSelDK(gId,dv,checked)
{
    var g = getIObject(gId);
    if (typeof(g.selectedDataKeyValues)=="undefined")
        return;
    var SelDKs = g.selectedDataKeyValues;
	var i=SelDKs.indexOf(dv);
	var idLen = dv.length;
	if (i>=0 && !checked)
	{
		if(SelDKs.substring(i+idLen,i+idLen+1)==',')
		{
			SelDKs = SelDKs.substring(0, i-1)+ SelDKs.substring(idLen+i,SelDKs.length);
			if (SelDKs.indexOf(",")==0)
			    SelDKs = SelDKs.substring(1,SelDKs.length);
		}
		else
		{
			SelDKs = SelDKs.substring(0, i-1);
		}   
	}
	else if (checked && i==-1)
	{
		if(SelDKs=='')
		{
			SelDKs=SelDKs+dv;
		}
		else
		{
			SelDKs=SelDKs+','+dv;
		}
	}
	g.selectedDataKeyValues = SelDKs;
}
function onSortCommand(gId,pIndex)
{
	var _txt=getSearchBox(gId);
	if (_txt)
	    _txt.srcCol=event.srcElement.innerHTML;
	g_CB(_txt,"sort",pIndex,event.srcElement.sDir)
	ilg_SetFocus(_txt);
	return false;
}
function onHdrClick(gId)
{
	var _txt=getSearchBox(gId);
	if (_txt)
	    _txt.srcCol=event.srcElement.innerHTML;
}
function fSGD()
{
	return false;
}
function Init_LkpGd(gid)
{
	window.document.attachEvent("onreadystatechange",new Function("Init_LkpGdRS('" + gid + "');"));
}
//On Keyup event of TextBox
function lg_onKeyup()
{
	if (event.srcElement.oldValue!=null)
	{	
		if (event.srcElement.oldValue==event.srcElement.value)
		{
			if (IsArrowKeys())
				g_sg();
			else
				return;
		}
	}
	if (event.keyCode==13)
	{	
		if (event.srcElement.doSearch=="true")
		{
			ilg_SetFocus(event.srcElement.id);
			event.keyCode=0;
			event.returnValue=false;
			event.cancelBubble=true;
			return false;
		}else
		{
			return false;
		}
	}
	if (IsValidKey())
	{
		event.srcElement.range=null;
		event.srcElement.hasChanges=true;
		if (event.srcElement.doSearch=="true")
		{
			if (!IsArrowKeys())
				g_CB(event.srcElement,"search");
			else if (event.keyCode==40 && event.srcElement.value=="")
				g_CB(event.srcElement,"search");
			else if (event.keyCode==33)
				g_pc(event.srcElement.id,event.srcElement.cpIndex,"down");
			else if (event.keyCode==34)
				g_pc(event.srcElement.id,event.srcElement.cpIndex,"up");
			else
				g_sg();
		}
	}
}
var reQ;
function g_CB(obj,mode,rIndex,d)
{
	var pU = "";
	pU = pU + obj.u;
	pU = pU + "?l=true";
	pU = pU + "&v=" + obj.value;
	pU = pU + "&g=" + obj.grid.id;
	pU = pU + "&i=" + obj.id;
	var g = getIObject(obj.grid.id);
    if (typeof(g.selectedDataKeyValues)!="undefined")
    {
        if (mode=="page")
            pU = pU + "&sel=" + g.selectedDataKeyValues;
        else if(g.selectedDataKeyValues!="" && confirm("Selection will be cleared. Do you want to continue?"))
            g.selectedDataKeyValues="";
    }
		
	var postData="";
	postData = postData + "__CALLBACKID=" + obj.grid.id;
	//postData = postData + "&__VIEWSTATE=" + getIObject("__VIEWSTATE").value;
	postData = postData + "&__EVENTTARGET=" + obj.grid.id;
	if (mode!=null && mode!="")
	{
		pU = pU + "&m=" + mode;
		if (mode=="page")
		{
			pU = pU + "&p=" + obj.cpIndex;
		}
		else if (mode=="info")
		{
			pU = pU + "&r=" + rIndex;
		}
		else if (mode=="search")
		{
			if (typeof(obj.srcCol)=="undefined")
			{
				return false;
			}
			pU = pU + "&c=" + obj.srcCol;
		}
		else if (mode=="sort"){
			pU = pU + "&c=" + obj.srcCol;
			pU = pU + "&d=" + d;
			pU = pU + "&p=" + obj.cpIndex;
		}
	}
	obj.oldValue=obj.value;
	reQ = getXMLHttpRequest();
	reQ.open("POST", pU, false);
    reQ.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    reQ.onreadystatechange=new Function(" g_lLGRs('" + obj.id + "','" + mode + "');");
    reQ.send(postData);
}
function g_lLGRs(_id,mode)
{
	if (reQ.readyState==4)
	{
		if (reQ.status!="200")
		{
			document.write(reQ.responseText);
			return;
		}
		var obj=getIObject(_id);
		if (reQ.responseText!="Error" && reQ.responseText!="False")
		{
			if (mode!="info")
				g_bD(obj,reQ)
			if (reQ.getResponseHeader("cpIndex")!=null)
				obj.cpIndex=reQ.getResponseHeader("cpIndex");
			if (reQ.getResponseHeader("tpSize")!=null)
				obj.tpSize=reQ.getResponseHeader("tpSize");
		}
		else if (reQ.responseText=="Error")
			alert(reQ.getResponseHeader("errmsg"));
		else if (reQ.responseText=="False")
		{
			/*document.write(reQ.responseText);
			return;*/
		}
	}
}
function g_sg()
{
	if (event==null || event.srcElement==null)// && typeof(event.srcElement.rowEle)!="undefined")
		return;
	if (event.keyCode == 40)
	{
		event.keyCode =0;
		event.returnValue = false;
		var _lkpRow=getIObject(event.srcElement.rowEle);
		var _hdr=0;
		var _tot=1;
		if (event.srcElement.sHead!=null && event.srcElement.sHead=="true")
			_hdr=1;
		/*if (event.srcElement.aPage!=null && event.srcElement.aPage=="true")
			_tot=_tot+1;*/
		if (_lkpRow!=null)
		{
			if (_lkpRow.parentElement.rows.length-_tot!=_lkpRow.rowIndex)
			{
				event.srcElement.rowEle=_lkpRow.parentElement.rows(_lkpRow.rowIndex+1).id;
				_lkpRow=_lkpRow.parentElement.rows(_lkpRow.rowIndex+1);
				g_onLkpMover(_lkpRow.id);
				if (_lkpRow.rowIndex!=_hdr)
				{
					g_onLkpMout(_lkpRow.parentElement.rows(_lkpRow.rowIndex-1).id);
				}
				if (_lkpRow.parentElement.parentElement.parentElement.scrollHeight>_lkpRow.parentElement.parentElement.parentElement.clientHeight)
					_lkpRow.scrollIntoView(false);
			}
			else
			{
				if (_lkpRow.rowIndex!=1)
				{
					g_onLkpMout(_lkpRow.parentElement.rows(_lkpRow.parentElement.rows.length-_tot).id);
				}
				event.srcElement.rowEle=_lkpRow.parentElement.rows(_hdr).id;
				_lkpRow=_lkpRow.parentElement.rows(_hdr);
				g_onLkpMover(_lkpRow.id);
				_lkpRow.scrollIntoView(false);
			}
		}
		return;
	}
	else if (event.keyCode == 38)
	{
		event.keyCode =0;
		event.returnValue = false;
		var _lkpRow=getIObject(event.srcElement.rowEle);
		var _hdr=0;
		var _tot=1;
		if (event.srcElement.sHead!=null && event.srcElement.sHead=="true")
			_hdr=1;
		if (event.srcElement.aPage!=null && event.srcElement.aPage=="true")
			_tot=_tot+1;
		if (_lkpRow!=null)
		{
			if (_lkpRow.rowIndex!=_hdr)
			{
				event.srcElement.rowEle=_lkpRow.parentElement.rows(_lkpRow.rowIndex-1).id;
				_lkpRow=_lkpRow.parentElement.rows(_lkpRow.rowIndex-1);
				g_onLkpMover(_lkpRow.id);
				if (_lkpRow.rowIndex!=_lkpRow.parentElement.rows.length-_tot)
				{
					g_onLkpMout(_lkpRow.parentElement.rows(_lkpRow.rowIndex+1).id);
				}
				if (_lkpRow.parentElement.parentElement.parentElement.scrollHeight>_lkpRow.parentElement.parentElement.parentElement.clientHeight)
					_lkpRow.scrollIntoView(false);
			}
			else
			{
				event.srcElement.rowEle=_lkpRow.parentElement.rows(_lkpRow.parentElement.rows.length-_tot).id;
				_lkpRow=_lkpRow.parentElement.rows(_lkpRow.parentElement.rows.length-_tot);
				g_onLkpMover(_lkpRow.id);
				g_onLkpMout(_lkpRow.parentElement.rows(_hdr).id);
				_lkpRow.scrollIntoView(false);
			}
			return;
		}
	}
}
function IsValidKey()
{
	if ((event.keyCode>96 && event.keyCode<112) || (event.keyCode>64 && event.keyCode<91) || (event.keyCode>48 && event.keyCode<91) || (event.keyCode==40 || event.keyCode==38 || event.keyCode==46 || event.keyCode==8 || event.keyCode==33 || event.keyCode==34) && (event.keyCode!=27))
		return true;
	else
		return false;
}
function IsArrowKeys()
{
	if (event.keyCode!=40 && event.keyCode!=38 && event.keyCode!=33 && event.keyCode!=34)
		return false;
	else 
		return true;
}
//On KeyDown event of TextBox
function lg_onKeydown()
{
	if (event.srcElement.hasChanges==true && (event.keyCode==13 || event.keyCode==9))
	{
		if (event.srcElement.onchange!=null)
			event.srcElement.onchange();
		event.srcElement.hasChanges=false;
		return;
	}
	if (event.keyCode==27)
	{
		return;
	}	
	if ((event.keyCode>96 && event.keyCode<112) || (event.keyCode>64 && event.keyCode<91) || (event.keyCode>48 && event.keyCode<91) || (event.keyCode==40 || event.keyCode==38 || event.keyCode==46 || event.keyCode==8) && (event.keyCode!=27))
	{
		if (event.srcElement.range!=null)
		{	
			event.srcElement.range=null;
		}
	}
}
function lg_onPropertyChange()
{
	if (event.propertyName=="srcCol")
	{
		getSearchCaption(event.srcElement.grid.id).innerHTML=event.srcElement.srcCap + event.srcElement.srcCol + "&nbsp;";
	}
}
function g_iSp(iG,s)
{
	if(iG)
	{
		if (s)
		{//alert(event.y)
			iG.style.left=event.x+20;
			iG.style.top=event.y+10;
			iG.style.display="block";
		}
		else
		{
			iG.style.display="none";
			iG.innerHTML="";
		}
	}
}
function g_cD(gid)
{
	var c = document.createElement("DIV");
	c.id = gid + "_lgInfo";
	c.style.position="absolute";
	c.style.zindex=999;
	c.style.top="0px";
	c.style.left="0px";
	c.style.display="none";
	window.document.body.appendChild(c);
}
function g_infoOver(obj)
{
	g_CB(getSearchBox(obj.gId),"info",obj.RowIndex);
	if (reQ.responseText!="" && reQ.responseText!="s")
	{//alert(reQ.responseText)
		getInfoContainer(obj.gId).innerHTML=reQ.responseText;
		g_iSp(getInfoContainer(obj.gId),true);
	}
}
function g_infoOut(obj)
{
	g_iSp(getInfoContainer(obj.gId),false);
}
function g_onLkpMover(_id)
{
	getIObject(_id).style.cssText = getMoverCssText(_id);
	if (getMoverCssClass(_id)!="")
		getIObject(_id).className = getMoverCssClass(_id);
}
function g_onLkpMout(_id)
{
	var obj=getIObject(_id);
	/*var _tbl=getIObject[obj.gId];
	if (typeof(_tbl.currentRowIndex)=="undefined")
		_tbl.currentRowIndex=1;
	if (eval(_tbl.currentRowIndex)!=obj.rowIndex || (event!=null && event.type=="blur"))
	{*/
		obj.style.cssText = getMoutCssText(_id);
		if (getMoutCssClass(_id)!="")
			obj.className = getMoutCssClass(_id);
	//}
}
function getSearchBox(gId)
{
	return getIObject(gId + "_SrcBox");
}
function getSearchCaption(gId)
{
	return getIObject(gId + "_SrcLbl");
}
function getInfoContainer(gId)
{
	return getIObject(gId + "_lgInfo");
}
function getMoverCssText(_id)
{
	if (getIObject(_id).parentElement.tagName=="TABLE")
		return getIObject(_id).parentElement.parentElement.movercssText.toString();
	else if (getIObject(_id).parentElement.tagName=="TBODY")
		return getIObject(_id).parentElement.parentElement.parentElement.movercssText.toString();
}
function getMoverCssClass(_id)
{
	if (getIObject(_id).parentElement.tagName=="TABLE")
		return getIObject(_id).parentElement.parentElement.moverClass.toString();
	else if (getIObject(_id).parentElement.tagName=="TBODY")
		return getIObject(_id).parentElement.parentElement.parentElement.moverClass.toString();
}
function getMoutCssText(_id)
{
	if (getIObject(_id).parentElement.tagName=="TABLE")
		return getIObject(_id).parentElement.parentElement.moutcssText.toString();
	else if (getIObject(_id).parentElement.tagName=="TBODY")
		return getIObject(_id).parentElement.parentElement.parentElement.moutcssText.toString();
}
function getMoutCssClass(_id)
{
	if (getIObject(_id).parentElement.tagName=="TABLE")
		return getIObject(_id).parentElement.parentElement.moutClass.toString();
	else if (getIObject(_id).parentElement.tagName=="TBODY")
		return getIObject(_id).parentElement.parentElement.parentElement.moutClass.toString();
}
-->