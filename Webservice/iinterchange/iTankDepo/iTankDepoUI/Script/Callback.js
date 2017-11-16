function Callback()
{
	var _pl = new Array();
	var status = true;
	var error = '';
	this.add = function (name, value) {
	    if (typeof (value) == "undefined")
	        value = "";
	    value = value.toString();
	    if (value != '')
	        _pl[name] = encodeURIComponent(value);
	    return this;
	}
	var _returntext= '';
	var xmlRequest;
	this.invoke = function (url, callbackIdentifier, parameters) {
	    xmlRequest = getXmlHttp();

	    if (url.indexOf('?') != -1)
	        url += "&callback=true";
	    else
	        url += "?callback=true";

	    url += "&callbacktype=" + callbackIdentifier;
	    url += "&ifgActivityId=" + getQStr("activityid");
	    xmlRequest.open("POST", url, false);
	    xmlRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

	    for (var p in _pl) {
	        xmlRequest.setRequestHeader(p, _pl[p]);
	    }

	    xmlRequest.send(null);

	    if (xmlRequest.readyState == 4) {

	        _returntext = xmlRequest.responseText;
	        if (xmlRequest.getResponseHeader("CLBKStatus") == 0)
	            status = false;
	        else
	            status = true;

	        error = xmlRequest.getResponseHeader("CLBKError");

	    }
	}
    this.getReturnValue = function(name) 
	{
		return xmlRequest.getResponseHeader(name);
	}
	this.getResponseText = function(name) 
	{
	    return _returntext;
	}
	this.getCallbackStatus = function()
	{
	    return status;
	}
    this.getCallbackError = function()
	{
	    return error;
	}
	
}

function getXmlHttp()
{
	try
	{
		if(window.XMLHttpRequest) 
		{
			var req = new XMLHttpRequest();
			// Some versions of Moz do not support the readyState property and the onreadystate event so we patch it!
			if(req.readyState == null) 
			{
				req.readyState = 1;
				req.addEventListener("load",
									function () {
									    req.readyState = 4;
									    if (typeof req.onreadystatechange == "function")
									        req.onreadystatechange();

									},
									false);
					}
//					req.onreadystatechange = function () {
//					    if (req.readyState == 4) {
//					        setTimeout("hideWorkingMessage();", 1);				        
//					    }
//					    else if (req.readyState == 1) {
//					        setTimeout("showWorkingMessage();",1);
//					    }
//					}
			return req;
		}
		if(window.ActiveXObject) 
			return new ActiveXObject(getXmlHttpProgID());
	}
	catch (ex) {}
}

function getXmlHttpProgID()
{
	
	var progids = ["Msxml2.XMLHTTP.5.0", "Msxml2.XMLHTTP.4.0", "MSXML2.XMLHTTP.3.0", "MSXML2.XMLHTTP", "Microsoft.XMLHTTP"];
	var o;
	for(var i = 0; i < progids.length; i++)
	{
		try
		{
			o = new ActiveXObject(progids[i]);
			return progids[i];
		}
		catch (ex) {};
	}
}