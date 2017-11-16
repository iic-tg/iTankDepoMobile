// JScript File
var _ipbver = "1.0.2510.21733";
function Init_ipb(aid,iid)
{
  	window.document.attachEvent("onreadystatechange",new Function("Init_ipbRS('" + aid + "','" + iid + "');"));
}
function Init_ipbRS(aid,iid)
{    
	if (document.readyState=="complete")
	{
		var a = getIObject(aid);
		BindOnKeyDownEvent(a,"ipb_kd();");
		a.hasChanges = false;
		a.language = "javascript";
		a.cT = "iInputBox";
		a.AUTOCOMPLETE = "off";		
		BindOnChangeEvent(a,"ipb_cg();");
        if (iid!="")
		{
		    var img = getIObject(iid);
		    img.ipb = a;
	        img.onclick = function(){return ipb_imgClick(this);};
        }
        ipb_cd(a);
		window.document.attachEvent("onclick",new Function("return ipb_dc('" + aid + "');"));		
	}
}
function ipb_kd()
{
	if (event.keyCode==13 || event.keyCode==9)
    {
        ipb_tb(event.srcElement,false);
        if (event.srcElement.hasChanges==true)
	    {
	        if (event.srcElement.onchange!=null)
			    event.srcElement.onchange();    
	        event.srcElement.hasChanges=false;
		    return false;
	    }
	}
}
function ipb_cg()
{
	
}
function ipb_imgClick(obj)
{    
    obj.ipb.focus();
    _frmme = obj.ipb.id;
    //event.cancelBubble = true;
}
function ipb_dc()
{
	
}
function ipb_ob()
{
	
}
function ipb_cd(a)
{
	var c = getIObject("_ipbCntr");
	if(c == null)
	{
		var c = document.createElement("DIV");
		a.cId = "_ipbCntr";
		c.id = "_ipbCntr";
		c.style.position="absolute";
		c.style.top="0px";
		c.style.left="0px";
		c.style.zIndex=99999;
		c.style.display="none";
		window.document.body.appendChild(c);
	}
	else
	{
		a.cId = a.id + "_ipbCntr";
		c.style.position="absolute";
		c.style.top="0px";
		c.style.left="0px";
		c.style.zIndex=99999;
		c.style.display="block";
	}  
}
function ipb_tb(obj,bol)
{
	if(obj != null)
	{	
	    var lG = obj.mLB;
	    if (typeof(lG)!="undefined" && lG!=null)
	    {
	        if (bol)
	            lG.style.display = "block";
	        else
	            lG.style.display = "none";
		}
	}
}
function ipb_st()
{
	
}