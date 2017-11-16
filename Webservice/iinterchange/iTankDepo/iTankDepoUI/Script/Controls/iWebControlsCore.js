//var _iwcver = "3.0.3583.29809/29102009";
function getXMLHttpRequest()
{
	var i=null;
	try
	{
		i=new ActiveXObject("Msxml2.XMLHTTP")
	}
	catch(e)
	{
		try
		{
			i=new ActiveXObject("Microsoft.XMLHTTP")
		}
		catch(oc)
		{
			A=null
		}
	}

	if(!i&&typeof XMLHttpRequest!="undefined")
	{
		i=new XMLHttpRequest()
	}

	return i
}
//To the Object By Id
function getIObject(_Id)
{
	if (_Id!=null && typeof(_Id)=="string" && _Id!="")
	{
		return document.getElementById(_Id);
	}
	return null;
}
//To get Object Value By Id
function getIObjectValue(_Id)
{
	if (_Id!=null && typeof(_Id)=="string" && _Id!="")
	{
		if (getIObject(_Id)!=null)
		{
			return trimAll(getIObject(_Id).value);
		}
		else
		{
			return null;
		}
	}
	return null;
}
/*****************************Cross Browser-Implementation Functions*******************************************/
function getIAttribute(obj, key)
{  
 try{ 
    if(!obj.hasAttribute){
       if(obj.attributes[key].value=="null")
           return null;
       else
           return obj.attributes[key].value;
     }
    if(obj.hasAttribute(key)){
        if(obj.getAttribute(key)=="null")
            return null;
        else    
            return obj.getAttribute(key);
    }
   else
    return undefined;
   }
 catch (ex){return;}
}
function setIAttribute(obj,key,value)
{
obj.setAttribute(key,value);
}
//Function To support iCase
function changeCase(event,iCase) {
    var obj = event.srcElement || event.target;
    var CaretPos = GetCaretPosition(obj);
    if (!(obj.readOnly == true)) {
        if (document.selection) {
            if (document.selection.type != "None")
                var iOldLength, iNewLength;
            iOldLength = obj.value.length;
            document.selection.clear();
            iNewLength = obj.value.length;
            CaretPos = CaretPos - (iOldLength - iNewLength);
        }
        else
            obj.value = obj.value.substring(0, obj.selectionStart);
        if (iCase == "Lower") {
            var char = String.fromCharCode(event.which || event.keyCode);
            if (obj.value.length < obj.maxLength || obj.maxLength == -1)
                obj.value = obj.value.substring(0, CaretPos) + char.toLowerCase() + obj.value.substring(CaretPos, obj.value.length);
        }
        if (iCase == "Upper") {
            var char = String.fromCharCode(event.which || event.keyCode);
            if (obj.value.length < obj.maxLength || obj.maxLength == -1)
                obj.value = obj.value.substring(0, CaretPos) + char.toUpperCase() + obj.value.substring(CaretPos, obj.value.length);
        }
        setCaretPosition(obj, CaretPos + 1);
        obj.IsCheckedOnKd = true;
        obj.hasChanges = true;
        setHasChanges();
      }
      if (event.preventDefault) {
          event.preventDefault(true);
          event.stopPropagation();
      }
      else
        event.returnValue = false;
        event.cancelbubble = true;
}
//copy Attributes for Search Row Insertion
function _mergeAttributes(_src,_dst)
{
 for (var i=0; i < _src.attributes.length; i++) 
  {
    var attr = _src.attributes[i];
    var attrName = attr.name.toLowerCase();
    if (attrName != "id" && attrName != "name") {
    _dst.setAttribute (attr.name, attr.value);
    }
   }
}

// Css Class Manipulation functions
function toogleCss(obj,clsName,bol)
{  if(obj!=null)
    { 
       if(bol)
        addClass(obj,clsName);
       else
        removeClass(obj,clsName);
    }
}
function hasClass(el, name) {
   return new RegExp('(\\s|^)'+name+'(\\s|$)').test(el.className);
}
function addClass(el, name)
{
   if (!hasClass(el, name)) { el.className += (el.className ? ' ' : '') +name; }
}
function removeClass(el, name)
{
   if (hasClass(el, name)) {
      el.className=el.className.replace(new RegExp('(\\s|^)'+name+'(\\s|$)'),' ').replace(/^\s+|\s+$/g, '');
   }
}

function GetCaretPosition (ctrl) {
    var CaretPos = 0;  
    if (document.selection) {
        ctrl.focus ();
        var Sel = document.selection.createRange ();
        Sel.moveStart ('character', -ctrl.value.length);
        CaretPos = Sel.text.length;
    }
    else if (ctrl.selectionStart || ctrl.selectionStart == '0')
        CaretPos = ctrl.selectionStart;
return (CaretPos);
}
function setCaretPosition(ctrl, pos){
    if(ctrl.setSelectionRange)
    {
        ctrl.focus();
        ctrl.setSelectionRange(pos,pos);
    }
    else if (ctrl.createTextRange) {
        var range = ctrl.createTextRange();
        range.collapse(true);
        range.moveEnd('character', pos);
        range.moveStart('character', pos);
        range.select();
    }
}
function isValidNC(kc) {
    if (kc == 39 && String.fromCharCode(kc) == "'")
        return false;
    if (kc == 37 || kc == 39|| kc==46)
        return true;
    if (kc > 31 && (kc < 48 || kc > 57))
        return false;
    return true;
}
String.prototype.removeCharAt = function(ind){
var string_one = this.slice(0, ind);
var string_two = this.slice(ind + 1, this.length);
var final_string = string_one.concat(string_two);
return final_string;
}
//Get Event
var eType = ["keydown", "keypress", "keyup", "click", "mouseup", "change", "focus", "blur", "dblclick"];
function getEvent(event) {
    try {
        if (window.event)
            return window.event;
        else {
            event = arguments.callee.caller.arguments.callee.caller.arguments[0];
            if (event) {
                if (event.type == eType[0] || event.type == eType[1] || event.type == eType[2] || event.type == eType[3] || event.type == eType[4] || event.type == eType[5] || event.type == eType[6] || event.type == eType[7] || event.type == eType[8])
                    return event;
            }
            event = arguments.callee.caller.arguments.callee.caller.arguments.callee.caller.arguments[0];
            if (event) {
                if (event.type == eType[0] || event.type == eType[1] || event.type == eType[2] || event.type == eType[3] || event.type == eType[4] || event.type == eType[5] || event.type == eType[6] || event.type == eType[7] || event.type == eType[8])
                    return event;
            }
            event = arguments.callee.caller.arguments.callee.caller.arguments.callee.caller.arguments.callee.caller.arguments[0];
            if (event) {
                if (event.type == eType[0] || event.type == eType[1] || event.type == eType[2] || event.type == eType[3] || event.type == eType[4] || event.type == eType[5] || event.type == eType[6] || event.type == eType[7] || event.type == eType[8])
                    return event;
            }
            event = arguments.callee.caller.arguments.callee.caller.arguments.callee.caller.arguments.callee.caller.arguments.callee.caller.arguments[0];
            if (event) {
                if (event.type == eType[0] || event.type == eType[1] || event.type == eType[2] || event.type == eType[3] || event.type == eType[4] || event.type == eType[5] || event.type == eType[6] || event.type == eType[7] || event.type == eType[8])
                    return event;
            }
            event = arguments.callee.caller.arguments.callee.caller.arguments.callee.caller.arguments.callee.caller.arguments.callee.caller.arguments.callee.caller.arguments[0];
            if (event) {
                if (event.type == eType[0] || event.type == eType[1] || event.type == eType[2] || event.type == eType[3] || event.type == eType[4] || event.type == eType[5] || event.type == eType[6] || event.type == eType[7] || event.type == eType[8])
                    return event;
            }
            else
                return undefined;
        }
    }
    catch (ex) { return undefined; }
}
/***********************************************************************************************************/
function trimAll(_str)
{
	if (_str!=null && typeof(_str)=="string")
	{
		return _str.replace(/^\s*|\s*$/g,"");
	}
	return null;
}
function iwc_SetFocus(_Id)
{
	//DisableProgress();	
	if (_Id!=null && typeof(_Id)=="string")
	{	    
	    var obj = getIObject(_Id);
		if (obj!=null )//&& IsInVisibleContainer(_Id))
		{
		    document.body.focus();
			obj.focus();
		}
	}
}
function iwc_SetQueryString(pU, name, value) {
    if (typeof (value) == "undefined")
        return pU;
    if (pU.indexOf("?") != -1) {
        pU = pU + "&" + name + "=" + escape(encodeURIComponent(value));
    }
    else {
        pU = pU + "?" + name + "=" + escape(encodeURIComponent(value));
    }
    return pU;
}
function addEvent(obj, evType, fn, useCapture){
  if (obj.addEventListener){
    obj.addEventListener(evType, fn, useCapture);
    return true;
  } else if (obj.attachEvent){
    var r = obj.attachEvent("on"+evType, fn);
    return r;
  } else {
    alert("Handler could not be attached");
  }
} 
function removeEvent(obj, evType, fn, useCapture){
  if (obj.removeEventListener){
    obj.removeEventListener(evType, fn, useCapture);
    return true;
  } else if (obj.detachEvent){
    var r = obj.detachEvent("on"+evType, fn);
    return r;
  } else {
    alert("Handler could not be removed");
  }
}
function BindOnKeyDownEvent(control,event2Attach)
{
	var ev;
    ev = control.onkeydown;
    if (typeof(ev) == "function" ) {
        ev = ev.toString();
        ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
    }
    else {
        ev = "";
    }
    if (ev.indexOf("if (ValidatedTextBoxOnKeyPress() == false) return false;")!=-1)
    {
        ev = ev.replace("if (ValidatedTextBoxOnKeyPress() == false) return false;","");
        event2Attach = "if (ValidatedTextBoxOnKeyPress() == false) return false;" + event2Attach;
    }
    if (ev.indexOf(event2Attach)=="-1")
    {
        var func = new Function(event2Attach + ev);
        control.onkeydown = func;    
    }
}
function BindFocusEvent(control,event2Attach)
{
    var ev;
    ev = control.onfocus;
	if (typeof(ev) == "function" ) {
        ev = ev.toString();
        ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
    }
    else {
        ev = "";
    }
    /*if (ev!="" && ev.indexOf(event2Attach)=="-1")
    {*/
        var func = new Function(event2Attach + ev);
        control.onfocus = func;    
    //}
}
function BindClickEvent(obj,evt2Att)
{
    var ev;
    ev = obj.onclick;
	if (typeof(ev) == "function" ) {
        ev = ev.toString();
        ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
    }
    else {
        ev = "";
    }
    if (ev.indexOf(evt2Att)=="-1")
    {
        var func = new Function(evt2Att + ev);
        obj.onclick = func;    
    }
}
function BindBlurEvent(control,event2Attach)
{
    var ev;
    ev = control.onblur;
	if (typeof(ev) == "function" ) {
        ev = ev.toString();
        ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
    }
    else {
        ev = "";
    }
    /*if (ev!="" && ev.indexOf(event2Attach)=="-1")
    {*/
        var func = new Function(event2Attach + ev);
        control.onblur = func;    
    //}
}
function GetBoolFromString(str)
{
    if (typeof(str)=="string")
    {
                    
        if (str.toLowerCase()=="false")
            return false;
        else
            return true;
    }
    else if(typeof(str)=="boolean")
    {
        return str;
    }
    return false;
}
function getEvents(ev)
{
   if(!ev)
       ev = window.event;
   return ev;    
}
function BindOnChangeEvent(control,event2Attach)
{
	var ev;
    ev = control.onchange;
    if (typeof(ev) == "function" ) {
        ev = ev.toString();
        ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
    }
    else {
        ev = "";
    }
    if (ev.indexOf("ValidatorOnChange();")!=-1)
    {
        ev = ev.replace("ValidatorOnChange();","");
        event2Attach = "ValidatorOnChange();" + event2Attach;
    }
    if (ev.indexOf(event2Attach)=="-1")
    {
        var func = new Function(event2Attach + ev);
        control.onchange = func;    
    }
}
/*Scripts for iCase*/
function enableLowerCaseAlways(event) 
{
    if(!event)
        event = getEvent(event);
    var _kc = event.keyCode ? event.keyCode : event.charCode ? event.charCode : event.which;
    if (!event.ctrlKey) {
        if (_kc >= 65 && _kc <= 90)
            changeCase(event, "Lower");
    }    
}
function enableNumericOnPaste()
{  
  if(window!=null)
  {	
	if(window.clipboardData!=null){
	var _txt=window.clipboardData.getData("Text");
	}
	else if (event.clipboardData!=null){	    	
	var _txt=event.clipboardData.getData("Text");	
	}
	else
	return;
	_txt=trimAll(_txt);
	if (_txt!='')
	{
		if (isNaN(_txt)==true){
			alert("You cannot enter 'Non-Numeric' values into this field.");
			event.returnValue=false;
			return;
		}
		else
		{
			if (trimAll(_txt.length) > event.srcElement.maxLength){
				alert("This field allows only " + event.srcElement.maxLength + " characters.\nYou cannot enter the data which is " + _txt.length + " characters.")
				event.returnValue=false;
				return;
			}
		}
    }
    if(window.clipboardData)
        window.clipboardData.setData("Text", _txt);
    event.srcElement.IsCheckedOnKd=true;
    event.srcElement.hasChanges=true;
    }
}
function enableLowerCaseOnPaste()
{    
	if (window!=null)  	
	{	
	  if(window.clipboardData!=null){
	    var _txt=window.clipboardData.getData("Text");
	    }
	  else if (event.clipboardData!=null){	    	
	    var _txt=event.clipboardData.getData("Text");
	    }  
	_txt=trimAll(_txt);
	if (_txt!="")
	{
		if (event.srcElement.maxLength!=null && event.srcElement.maxLength!="undefined" &&  event.srcElement.maxLength!="")
		{
			if (_txt.length > event.srcElement.maxLength){
				alert("This field allows only " + event.srcElement.maxLength + " characters.\nYou cannot enter the data which is " + _txt.length + " characters.")
				event.returnValue=false;
				return;
			}
		}
    }  
    if(window.clipboardData)
        window.clipboardData.setData("Text", _txt.toLowerCase());
    else{
        event.srcElement.value=_txt.toLowerCase();
        event.preventDefault(true);
        }
    event.srcElement.IsCheckedOnKd=true;
    event.srcElement.hasChanges=true;
    }
}
function enableUpperCaseAlways(event) 
{
    if(!event)
        event = getEvent(event);
    var _kc = event.keyCode || event.charCode || event.which;
    if (!event.ctrlKey) {
        if (_kc > 96 && _kc < 123) 
            changeCase(event, "Upper");
        else if (event.preventDefault) {
                if (event.keyCode == 9 || event.keyCode == 13) {
                    if (getIAttribute(event.target.parentNode, "ct") != undefined || (typeof(event.target.mozIsTextField)!='undefined'&&typeof(event.target.hasChanges)=='undefined')) {
                        event.preventDefault(true);
                        event.stopPropagation();
                    }
                }            
        }
    }
}
function enableUpperCaseOnPaste()
{   
	if (window!=null)  	
	{	
	  if(window.clipboardData!=null){
	    var _txt=window.clipboardData.getData("Text");
	    }
	  else if (event.clipboardData!=null){	    	
	    var _txt=event.clipboardData.getData("Text");
	    }  	
	_txt=trimAll(_txt);
	if (_txt!='')
	{
		if (event.srcElement.maxLength!=null && event.srcElement.maxLength!="undefined" &&  event.srcElement.maxLength!="")
		{
			if (_txt.length > event.srcElement.maxLength){
				alert("This field allows only " + event.srcElement.maxLength + " characters.\nYou cannot enter the data which is " + _txt.length + " characters.")
				event.returnValue=false;
				return;
			}
		}
    }
     if(window.clipboardData)   
        window.clipboardData.setData("Text", _txt.toUpperCase());
     else{
        event.srcElement.value= _txt.toUpperCase();
        event.preventDefault(true);
        }
   event.srcElement.IsCheckedOnKd=true;
   event.srcElement.hasChanges=true;
   }
}
function enableNumericOnly(event) {
    if (!event) {
        event = getEvent(event);
    }
    var obj = event.srcElement || event.target;
    if (obj.readOnly == true) {
        if (event.preventDefault) {
            event.preventDefault(true);
        }
        else {
            event.keyCode = 8;
        }
        return false;
    }
    if (event.shiftKey) {
        if (!(event.keyCode == 9)) {
            showWarningMessage("You cannot enter 'Non-Numeric' values into this field.");
            if (event.preventDefault)
                event.preventDefault(true);
            else
                event.keyCode = 8;
        }
    }
    else {
        if (!event.ctrlKey) {
            if (!event.altKey) {
                var _kc = event.keyCode ? event.keyCode : event.charCode ? event.charCode : event.which;
                if (!isValidNC(_kc)) {
                    var _obj = event.srcElement || event.target;
                    if (!isNaN(_obj.value))
                        showWarningMessage("You cannot enter 'Non-Numeric' values into this field.");
                    if (event.preventDefault)
                        event.preventDefault(true);
                    else
                        event.keyCode = 8;
                }
            }
        }
    }
}

function iTxt_OP() {
    if (window != null) {
        if (window.clipboardData != null)
            var _txt = window.clipboardData.getData("Text");
        else if (event.clipboardData != null)
            var _txt = event.clipboardData.getData("Text");
        _txt = trimAll(_txt);
        if (_txt != '') {
            if (getIAttribute(event.srcElement, "maxLength") != null && getIAttribute(event.srcElement, "maxLength") != "undefined" && getIAttribute(event.srcElement, "maxLength").maxLength != "") {
                if (_txt.length > getIAttribute(event.srcElement, "maxLength")) {
                    showWarningMessage("This field allows only " + getIAttribute(event.srcElement, "maxLength") + " characters.\nYou cannot enter the data which is " + _txt.length + " characters.");
                    event.returnValue = false;
                    return;
                }
            }
        }
        if (window.clipboardData)
            window.clipboardData.setData("Text", _txt);
        event.srcElement.IsCheckedOnKd = true;
        event.srcElement.hasChanges = true;
    }
}
/*Scripts for Help and Input Elements Highlighting*/
function get_icHlpEnb()
{
    if (typeof(_icHlpEnb)!="undefined" && _icHlpEnb!="")
        return GetBoolFromString(_icHlpEnb);
    else
        return null;
}
function get_icCHlp()
{
    if (typeof(_icCHlp)!="undefined" && _icCHlp!="")
        return GetBoolFromString(_icCHlp);
    else
        return null;
}
function get_icHlpEvt()
{
    if (typeof(_icHlpEvt)!="undefined" && _icHlpEvt!="")
        return _icHlpEvt;
    else
        return null;
}
function get_icHlpKeyEvt()
{
    if (typeof(_icHlpKeyEvt)!="undefined" && _icHlpKeyEvt!="")
        return _icHlpKeyEvt;
    else
        return null;
}
function get_icHlpKey()
{
    if (typeof(_icHlpKey)!="undefined" && _icHlpKey!="")
        return _icHlpKey;
    else
        return null;
}
function get_icFEvt()
{
    if (typeof(_icFEvt)!="undefined" && _icFEvt!="")
        return _icFEvt;
    else
        return null;
}
function get_icBEvt()
{
    if (typeof(_icBEvt)!="undefined" && _icBEvt!="")
        return _icBEvt;
    else
        return null;
}
function get_icFCss()
{
    if (typeof(_icFCss)!="undefined" && _icFCss!="")
        return _icFCss;
    else
        return null;
}
function get_icBCss()
{
    if (typeof(_icBCss)!="undefined" && _icBCss!="")
        return _icBCss;
    else
        return null;
}
function get_icHt(event)
{   var _obj=event.srcElement||event.target;
    if (typeof(getIAttribute(_obj,"hT"))!="undefined" && getIAttribute(_obj,"hT")!="")
        return getIAttribute(_obj,"hT");
    else if (_obj.parentNode && typeof (getIAttribute(_obj.parentNode, "hT")) != "undefined" && getIAttribute(_obj.parentNode, "hT") != "")
        return getIAttribute(_obj.parentNode,"hT");
    else
        return "";
}
function lKd(event)
{   if(!event)
        event = getEvent(event);
    var _obj = event.srcElement || event.target;
    var _kc = event.keyCode ? event.keyCode : event.charCode ? event.charCode :event.which;
    if (_kc==get_icHlpKey() && get_icHlpEnb())
    {
        lHp(event);
    }
    _obj.IsCheckedOnKd = true;
    _obj.hasChanges = true;
}
function lFs(event)
{if (!event)
        event = getEvent(event);
    var _obj=event.srcElement||event.target;
    var _fc = get_icFCss();
    if (_fc && (_obj.type.toUpperCase() == "TEXT" || _obj.tagName.toUpperCase() == "TEXTAREA" || _obj.type.toUpperCase() == "PASSWORD") && _obj.readOnly!=true)
    {
        var _cls = _obj.className;
        if (_cls.lastIndexOf("o") != -1)
            _cls = _cls.substring(0, _cls.length - 1)
        if (_cls.lastIndexOf("_e") != -1)
            _cls = _cls.substring(0, _cls.length - 4)
        if (_obj.isvalid == false) {
            _obj.className = _cls + "o_err";
        }
        else {
            _obj.className = _cls + "o";
        }

    }
    var _fe = get_icFEvt();
    if (_fe)
    {
        eval(_fe + "();");
    }
    if (get_icHlpEnb())
    {
        lHp(event);
        //lSHp();
    }
}
function lSHp(event)
{
    window.status = get_icHt(event);
    //event.srcElement.hS = true;
}
function lHHp()
{
    window.status = "";
    //event.srcElement.hS = false;
}
function lBr(event)
{
    if (!event)
        event = getEvent(event);
    var _obj=event.srcElement||event.target;
    var _bc = get_icBCss();
    if (_bc && (_obj.type.toUpperCase() == "TEXT" || _obj.tagName.toUpperCase() == "TEXTAREA" || _obj.type.toUpperCase() == "PASSWORD") && _obj.readOnly != true) {
        var _cls = _obj.className;
        if (_cls.lastIndexOf("o") != -1)
            _cls = _cls.substring(0, _cls.length - 1)
        if (_cls.lastIndexOf("_e") != -1)
            _cls = _cls.substring(0, _cls.length - 4)
        if (_obj.isvalid == false) {
            _obj.className = _cls + "_err";
        }
        else {
            _obj.className = _cls;
        }
    }
    var _be = get_icBEvt();
    if (_be)
    {
        eval(_be + "();");
    }
    if (get_icHlpEnb())
    {
        lHp(event)
        //lHHp();
    }
}
function lHp(event)
{   
    var _m = "";
    var _hT = "";
    var _obj=event.srcElement||event.target;
    var tdObj = _obj.parentNode;
    if (getIAttribute(_obj,"hS")==true)
        _m = "hide";
    else
        _m = "show";
    if (get_icCHlp())
    {
        if(typeof(getIAttribute(_obj,"hT"))=="undefined"&&typeof(getIAttribute(tdObj,"hT"))=="undefined")
        {
            _hT = "";
        }
        else if(typeof(getIAttribute(tdObj,"hT"))!="undefined")
        {
            _hT = getIAttribute(tdObj,"hT");
        }
        else
        {
            _hT = getIAttribute(_obj,"hT");
        }
        var _cH;
        if (event!=null && event.type=="keydown")
        {        
            _cH = get_icHlpKeyEvt();
            if (_cH!="")
                eval(_cH + "('" + _hT + "',event.srcElement||event.target);");
        }
        else
        {
            _cH = get_icHlpEvt();
            if (_cH!="")
                eval(_cH + "('" + _m + "','" + _hT + "',event.srcElement||event.target);");
        }
        if (_cH!="")
        {
            //eval(_cH + "('" + _m + "','" + _hT + "');");
            if (_m == "show")
                setIAttribute(_obj,"hS",true);
            else
                setIAttribute(_obj,"hS",false);
        }
    }
    else
    {
        if (_m == "show")
            lSHp(event);
        else
            lHHp();
    }    
}
function get_icImgFldr()
{
    if (typeof(_imf)!="undefined" && _imf!="")
        return unescape(_imf);
}
function get_icImgs(fname)
{
    return get_icImgFldr() + fname;
}
function hlp_keyev(Helptext)
{
    window.status = Helptext;
}
function hlp_ev(mode,Helptext)
{
    //mode - Shall have the two values Show and Hide
    //HelpText - Value in the property HelpText of TextBox controls
    if (mode=="show")
        window.status = Helptext;
    else
        window.status = "";
}

//Key Down to fire Onchange event of iContainer Control
function icKeyDown(event) {
    if (!event)
        event = getEvent(event);
    var _obj = event.srcElement || event.target;
    if ((event.keyCode == 9 || event.keyCode == 13) && _obj.hasChanges == true)
    {
        if (_obj.onchange != null)
            _obj.onchange();
        _obj.hasChanges = false;
        if (_obj.mozIsTextField) {
            if (getIAttribute(_obj.nextSibling.nextSibling, "q") == "t") {
                event.preventDefault(true);
                event.stopPropagation();
            }
        }    
    }
}
/*Functions to positioning Div*/
function oL(r)
{
	return oPL(r,"offsetLeft")
}
function oT(r)
{
	return oPT(r,"offsetTop")
}
function oPT(t,i)
{
	var lp=0;

	while(t)
	{
		lp+=t[i];
		if (t.tagName.toLowerCase()=="div" && t["scrollTop"]>0)		
		    lp-=t["scrollTop"];
		t=t.offsetParent;
	}

	return lp
}
function oPL(t,i)
{
	var lp=0;

	while(t)
	{
		lp+=t[i];
		if (t.tagName.toLowerCase()=="div" && t["scrollLeft"]>0)		
		    lp-=t["scrollLeft"];
		t=t.offsetParent;
	}

	return lp
}
function CheckMaxLen(obj,maxLen)
{
    return (obj.value.length <= maxLen);  
}
function SetControlValue(ctrlId,value)
{
    var obj = getIObject(ctrlId);
    obj.value = value;
    obj.IsCheckedOnKd = true;    
}
function GetClientDate()
{
    var fdate = new Date();
	//added by SRT
	var fdaystr=fdate.getDate().toString();
	if (fdaystr.length==1)
		fdaystr="0" + fdaystr;
    return fdaystr+"-"+JsMonthArray[fdate.getMonth()]+"-" + new String(fdate.getFullYear()).substring(0,4);
}