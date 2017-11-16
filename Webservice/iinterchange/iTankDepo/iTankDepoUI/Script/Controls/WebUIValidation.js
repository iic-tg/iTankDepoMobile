<!--
//var _valver = "1.0.3363.26711";
var Page_ValidationVer = "125";
var Page_IsValid = true;
var Grid_IsValid = true;
var Page_BlockSubmit = false;
var Page_BlockValidate = false; // Added by SRT to Enable Pagewise validation
var Page_BlockLookupValidate = false; // Added by SRT to Enable Pagewise Lookup validation
var Page_FocusingInvalidControl = false;
function ValidatorUpdateDisplay(val,valMsgBox) {
    var ctrl = document.all[getIAttribute(val,"controltovalidate")];
    if (ctrl != null){
        var _cls = document.all[getIAttribute(val,"controltovalidate")].className;
        if (_cls.lastIndexOf("_err")!=-1)
            _cls = _cls.substring(0,_cls.length-4);
        if (val.isvalid==false)
            document.all[getIAttribute(val,"controltovalidate")].className=_cls+"_err";
        else
            document.all[getIAttribute(val,"controltovalidate")].className=_cls;
    }
    if (typeof(val.display) == "string") {
        if (val.display == "None") {
            return;
        }
        if (val.display == "Dynamic") {
            val.style.display = val.isvalid ? "none" : "inline";
            return;
        }
    }
    if ((navigator.userAgent.indexOf("Mac") > -1) &&
        (navigator.userAgent.indexOf("MSIE") > -1)) {
        val.style.display = "inline";
    }
    val.style.visibility = val.isvalid ? "hidden" : "visible";
    Grid_IsValid =  val.isvalid;    
    if(valMsgBox == null)
    {
        if (val.type!=null && val.type!="undefined" && val.type=="iDgValidator"){
		    //DgValidationSummaryOnSubmit();//by SRT
	    }
	    else{
		    ValidationSummaryOnSubmit(getIAttribute(val,"validationGroup"));//by SRT
	    }
	}
}
function ValidatorUpdateIsValid(validationgroup) {
    Page_IsValid = AllValidatorsValid(Page_Validators, null, validationgroup);
    //ValidationSummaryOnSubmit();//by SRT
}
function AllValidatorsValid(validators,ctrl, validationgroup) {
    if (validators != null) {
        var i;
        for (i = 0; i < validators.length; i++) {
            if (validators[i].isvalid==false) {
                if(typeof(validationgroup)=="undefined"){
                    if (ctrl!=null && ctrl.isvalid!=false && validators[i].focusOnError != "t")
                        i=i;
                    else
                        return false;
                }
                else{
                    if(getIAttribute(validators[i],"validationgroup")==validationgroup){
                        if (ctrl!=null && ctrl.isvalid!=false && validators[i].focusOnError != "t")
                            i=i;
                        else
                            return false;
                    }
                }
            }            
        }
    }
    return true;
}
function SetValidatorsValid(ctrl) {
    if (ctrl != null && ctrl.Validators!=null && !ctrl.readOnly) {
        var validators = ctrl.Validators;
        var i;
        for (i = 0; i < validators.length; i++) {
            validators[i].isvalid=true;            
            ctrl.islkpval=true;            
            Grid_IsValid = true;
            ValidatorUpdateIsValid();
            ValidatorUpdateDisplay(validators[i]);            
        }
        ctrl.isvalid=true;
    }
}
function ClearValidators(id,val)
{
    val.isvalid=true;
    ValidatorUpdateDisplay(val);
    ValidatorUpdateIsValid();
}
function ResetLookupValidator(id,bol)
{
    Page_BlockLookupValidate = true;
    SetValidatorsValid(document.all[id]);
    //ValidationSummaryOnSubmit();
    //document.all[id].IsCheckedOnKd=true;
    if (bol==null)
        ValidatorOnChange(id);    
  Page_BlockLookupValidate = false;    
}
function ValidatorHookupControlID(controlID, val) {
    if (typeof(controlID) != "string") {
        return;
    }   
    var ctrl = document.all[controlID];
    if (typeof(ctrl) != "undefined") {        
        ValidatorHookupControl(ctrl, val);
    }
    else {
        val.isvalid = true;
        val.enabled = false;
    }
}
function ValidatorHookupControl(control, val) {
    if (typeof(control.tagName) == "undefined" && typeof(control.length) == "number") {
        var i;
        for (i = 0; i < control.length; i++) {
            var inner = control[i];
            if (typeof(inner.value) == "string") {
                ValidatorHookupControl(inner, val);
            }
        }
        return;
    }
    else if (control.tagName != "INPUT" && control.tagName != "TEXTAREA" && control.tagName != "SELECT") {
        var i;
        for (i = 0; i < control.children.length; i++) {
            ValidatorHookupControl(control.childNodes[i], val);
        }
        return;
    }
    else {
        if (typeof(control.Validators) == "undefined") {
            control.Validators = new Array;
            var ev;
            if (control.type == "radio") {
                ev = control.onclick;
            } else {
                ev = control.onchange;
            }
            if (typeof(ev) == "function" ) {
                ev = ev.toString();
                ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
            }
            else {
                ev = "";
            }
            if (val.type!=null && val.type!="undefined" && val.type=="iDgValidator")
				var func = new Function("DgValidatorOnChange(); " + ev);
			else
				var func = new Function("ValidatorOnChange(); " + ev);
			
            if (control.type == "radio") {
                control.onclick = func;
            } else {
                control.onchange = func;
            }
            if (control.type == "text" ||
                control.type == "password" ||
                control.type == "file" || control.tagName.toLowerCase() =="textarea" ) {
                ev = control.onkeydown;//Changed From onkeypress to onkeydown by SRT
                if (typeof(ev) == "function") {
                    ev = ev.toString();
                    ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
                }
                else {
                    ev = "";
                }
                if (val.type!=null && val.type!="undefined" && val.type=="iDgValidator")
		            func = new Function("if (ValidatedDgTextBoxOnKeyPress() == false) return false; " + ev);
				else
					func = new Function("if (ValidatedTextBoxOnKeyPress() == false) return false; " + ev);
                
                control.onkeydown = func;//Changed From onkeypress to onkeydown by SRT
            }
        }
        control.IsCheckedOnKd=true;
        control.isvalid=true;
        if (control.id != getIAttribute(val,"controltovalidate") && control.Validators.length == 0)
        {
            control.Validators[0] = null;
            control.Validators[control.Validators.length] = val;
        }
        else if (control.Validators.length > 0 && control.id == getIAttribute(val,"controltovalidate"))
        {
            if (control.Validators[0] == null)
            {
                control.Validators[0] = val;
            }
            else
            {
                control.Validators[control.Validators.length] = val;
            }
        }
        else
        {
            control.Validators[control.Validators.length] = val;
        }
    }
}
function ValidatorGetValue(id) {
    var control;
    control = document.all[id];
    if (typeof(control.value) == "string") {
        return control.value;
    }
    if (typeof(control.tagName) == "undefined" && typeof(control.length) == "number") {
        var j;
        for (j=0; j < control.length; j++) {
            var inner = control[j];
            if (typeof(inner.value) == "string" && (inner.type != "radio" || inner.status == true)) {
                return inner.value;
            }
        }
    }
    else {
        return ValidatorGetValueRecursive(control);
    }
    return "";
}
function ValidatorGetValueRecursive(control)
{
    if (typeof(control.value) == "string" && (control.type != "radio" || control.status == true)) {
        return control.value;
    }
    var i, val;
    for (i = 0; i<control.children.length; i++) {
        val = ValidatorGetValueRecursive(control.childNodes[i]);
        if (val != "") return val;
    }
    return "";
}
function Page_ClientValidate(bol) {
	Page_ClientValidate(null,bol);
}
function Page_ClientValidate(validationGroup,bol) {
    if (bol==null)
        bol = false;
	Page_BlockLookupValidate=true;
	Page_FocusingInvalidControl = false;
	if (typeof(Page_Validators)=="undefined" || Page_Validators==null)
	    return true;
    var i;
    for (i = 0; i < Page_Validators.length; i++) {
        if (getIAttribute(Page_Validators[i],"controltovalidate")!=""){
            if (document.all[getIAttribute(Page_Validators[i],"controltovalidate")]!=null){
                if (document.all[getIAttribute(Page_Validators[i],"controltovalidate")].IsCheckedOnKd==false && bol==false){
                }
                else{
                    ValidatorValidate(Page_Validators[i], validationGroup);
                }
            }
        }
    }
    ValidatorUpdateIsValid(validationGroup);
    ValidationSummaryOnSubmit();
//    ValidationSummaryOnSubmit(validationGroup);
    Page_BlockSubmit = !Page_IsValid;
Page_BlockLookupValidate=false;
    return Page_IsValid;
}
function ValidatePage() {
	ValidatePage(null);
}
function ValidatePage(validationGroup) {
	Page_BlockLookupValidate=false;
	Page_FocusingInvalidControl = false;
    var i;
    for (i = 0; i < Page_Validators.length; i++) {
        ValidatorValidate(Page_Validators[i], validationGroup);
    }
    ValidatorUpdateIsValid();
    ValidationSummaryOnSubmit(validationGroup);
    Page_BlockSubmit = !Page_IsValid;
    return Page_IsValid;
}
function ValidatorCommonOnSubmit() {
    Page_FocusingInvalidControl = false;
    var result = !Page_BlockSubmit;
    var event=getEvent(event)
    if (event != null&&event!=undefined) {
        event.returnValue = result;
    }
    Page_BlockSubmit = false;
    return result;
}
function ValidatorEnable(val, enable) {
    val.enabled = (enable != false);
    ValidatorValidate(val);
    ValidatorUpdateIsValid();
}
function ValidatorOnChange(objid)
{    
    var _ise=false;
	if (objid==null||objid==undefined){	    
	    var event=getEvent(event);
		var obj=event.srcElement||event.target;	
        _ise=true;	
		}
    else if(objid.type==eType[0]||objid.type==eType[1]||objid.type==eType[2]||objid.type==eType[3]||objid.type==eType[4]||objid.type==eType[5]){
         var event=objid;
         var obj=event.srcElement||event.target;         
         _ise=true;
        }
	else
		var obj=document.all[objid];
	//To Clear Desc Value
	if (getIAttribute(obj,"cT")=="iLookup" && obj.value=="" && getIAttribute(obj,"clrdes")=="true")	
		ClearLookupDesc(obj);
	if (obj.IsCheckedOnKd==false)
		return;
	//SetCustomError(true,null);    
	Page_FocusingInvalidControl = false;
    var vals = obj.Validators;
    var i;
    if(_ise){
        if(vals.length>1){
         var _kc = event.keyCode ? event.keyCode : event.charCode ? event.charCode :event.which;
         if(_kc==9){
         event.keyCode=0;
         if(event.preventDefault){
			event.preventDefault(true);
            event.stopPropagation();              
			}
         }
        }
    }
    for (i = 0; i < vals.length; i++) {
        if(vals[i] != null)
        {
            var _p = Page_BlockLookupValidate;
            if (getIAttribute(vals[i],"controltovalidate")!= obj.id)
                Page_BlockLookupValidate = true;
            vals[i].isvalid=true;
            ValidatorValidate(vals[i]);
            if (getIAttribute(vals[i],"controltovalidate")!= obj.id && _p!=Page_BlockLookupValidate)
                Page_BlockLookupValidate = false;
            if(vals[i].isvalid==false)
                break;
        }    
    }
    ValidatorUpdateIsValid();
}
function ValidatorValidate(val) {
    ValidatorValidate(val, null);
}
function ValidateTextBox(ctrl)
{ 
    if(ctrl != null)
    { 
       ctrl.IsCheckedOnKd = true;
       var vals = ctrl.Validators;
       if (vals!=null)
       {
           var i;
           for (i = 0; i < vals.length; i++) 
           {                                                               
                ValidatorValidate(vals[i], null);
                ValidatorUpdateIsValid();
                if(ctrl.isvalid==false)
                {
                    return false;        
                }
           }
        }     
    }
}
function ValidatedTextBoxOnKeyPress() {     
    var event=getEvent(event);
    var _kc = event.keyCode ? event.keyCode : event.charCode ? event.charCode : event.which;
    var _obj= event.srcElement||event.target; 
    if (_kc == 13 || _kc == 9) {                
        if(typeof(_obj.readOnly) != 'undefined' && _obj.readOnly == true)
            return true;       
		if (_obj.IsCheckedOnKd==false && AllValidatorsValid(_obj.Validators,_obj)==true)
			return true;
		else if (_obj.IsCheckedOnKd==false && AllValidatorsValid(_obj.Validators)==false)
			return false;
		ValidatorOnChange(event);
		//event.srcElement.onchange();		
		if (AllValidatorsValid(_obj.Validators,_obj)==false){
			_obj.IsCheckedOnKd=false;
			event.returnValue=false;
			event.cancelBubble=true;
			if(event.preventDefault){
			event.preventDefault(true);
            event.stopPropagation();              
			}
			if(_obj.type=="password"){return true;}
			return false;
		}else{
			if (Page_BlockValidate) 
				_obj.IsCheckedOnKd=true;
			else
				_obj.IsCheckedOnKd=false;			
			return true;
		}
    }
    else if (CheckKey(event))
	{
		_obj.IsCheckedOnKd=true;
		_obj.hasChanges=true;
		setHasChanges();
    }
}
function CheckKey(event)
{
	//Function Keys
	 if(!event)
        event=getEvent(event);
	var obj=event.srcElement||event.target;
	var _kc = event.keyCode ? event.keyCode : event.charCode ? event.charCode : event.which;
    if (obj.readOnly==true)
        return false;
	if (_kc==38 || _kc==33 || _kc==34)
		return false;
    if (event.shiftKey==true && (_kc==9 || _kc==13))
        return false;
    return true;
}
function ValidatorValidate(val, validationGroup,valMsgBox) {
    var _valMsgBox = null;
    if(valMsgBox!=null)
	{ 
	    _valMsgBox = valMsgBox;
	}
	if (val.id!="undefined")
	{
		if (val.id=='csvRecords'){
			val.isvalid = true;
			return;
		}
	}
	if(document.all[getIAttribute(val,"controltovalidate")] == null)
	    return;
	if (Page_BlockValidate){
		val.isvalid = true;
		document.all[getIAttribute(val,"controltovalidate")].IsCheckedOnKd=true;
		ValidatorUpdateDisplay(val,_valMsgBox);
		return;
	}
	if(typeof(document.all[getIAttribute(val,"controltovalidate")].readOnly)!= 'undefined')
	{
	    if (document.all[getIAttribute(val,"controltovalidate")].readOnly==true)
	    {
	        val.isvalid = true;
//		    document.all[val.controltovalidate].IsCheckedOnKd=true;
	        ValidatorUpdateDisplay(val,_valMsgBox);
	        return;
	    }
	}
	if (val.enabled != false && IsValidationGroupMatch(val, validationGroup) && !Page_BlockValidate && CanIValidateLookup(val)) {
        if (typeof(val.evaluationfunction) == "function") {
            val.isvalid = val.evaluationfunction(val);
            var ctrl = document.all[getIAttribute(val,"controltovalidate")];
            if (ctrl)
            {
                ctrl.isvalid = val.isvalid;
                Grid_IsValid = ctrl.isvalid;
            }
            if (!val.isvalid && !Page_FocusingInvalidControl && getIAttribute(val,"focusOnError") == "t") {                
                if (typeof(ctrl) != "undefined") {
                    if ((ctrl != null) &&
                        ((ctrl.tagName.toLowerCase() != "input") || (ctrl.type.toLowerCase() != "hidden") || (ctrl.tagName.toLowerCase() != "textarea")) &&
                        (ctrl.disabled == false) &&
                        (ctrl.visible != false) &&
                        IsInVisibleContainer(ctrl)) {
                        try{
                        ctrl.focus();
                        }catch(e){}
                        Page_FocusingInvalidControl = true;
                    }
                }
            }
        }
    }
    ValidatorUpdateDisplay(val,_valMsgBox);  
}

function CanIValidateLookup(control){
	return true;
}
function IsInVisibleContainer(ctrl) {
    if (typeof(ctrl.style) != "undefined" &&
        ( ( typeof(ctrl.style.display) != "undefined" &&
            ctrl.style.display == "none") ||
          ( typeof(ctrl.style.visibility) != "undefined" &&
            ctrl.style.visibility == "hidden") ) ) {
        return false;
    }
    else if (typeof(ctrl.parentNode) != "undefined" &&
             ctrl.parentNode != null &&
             ctrl.parentNode != ctrl) {
        return IsInVisibleContainer(ctrl.parentNode);
    }
    return true;
}
function IsValidationGroupMatch(control, validationGroup) {
    if (validationGroup == null) {
        return true;
    }
    var controlGroup = "";
    if (typeof(getIAttribute(control,"validationGroup")) == "string") {
        controlGroup = getIAttribute(control,"validationGroup");
    }
    return (controlGroup == validationGroup);
}
function ValidatorOnLoad() {
    if (typeof( Page_ValidationActive ) == "undefined")
        return;
    var i, val;
    for (i = 0; i < Page_Validators.length; i++) {
        val = Page_Validators[i];
        val.clientvalidationfunction = "CustomBoxValidation";
        if (typeof(val.evaluationfunction) == "undefined") {
           val.evaluationfunction = function(val){return CustomValidatorEvaluateIsValid(val);};
        }
        if (typeof(val.isvalid) == "string") {
            if (val.isvalid == "False") {
                val.isvalid = false;
                Page_IsValid = false;
            }
            else {
                val.isvalid = true;
            }
        } else {
            val.isvalid = true;
        }
        if (typeof(val.enabled) == "string") {
            val.enabled = (val.enabled != "False");
        }
        //val.focusOnError = "t";
        //alert(val.controltovalidate)
        //alert(val.controlhookup)
        ValidatorHookupControlID(getIAttribute(val,"controltovalidate"), val);
        ValidatorHookupControlID(getIAttribute(val,"controlhookup"), val);
    }
    Page_ValidationActive = true;
}
function ValidatorConvert(op, dataType, val) {
    function GetFullYear(year) {
        return (year + parseInt(getIAttribute(val,"century"))) - ((year <= parseInt(getIAttribute(val,"cutoffyear"), 10)) ? 0 : 100);
    }
    var num, cleanInput, m, exp;
    if (dataType == "Integer") {
		exp = /^\s*[-\+]?\d+\s*$/;
        if (op.match(exp) == null)
            return null;
        num = parseInt(op, 10);
        return (isNaN(num) ? null : num);
    }
    else if(dataType == "Double") {
        exp = new RegExp("^\\s*([-\\+])?(\\d*)\\" + getIAttribute(val,"decimalchar") + "?(\\d*)\\s*$");
        m = op.match(exp);
        if (m == null)
            return null;
        if (m[2].length == 0 && m[3].length == 0)
            return null;
        cleanInput =  (m[1] ?  m[1]:"") + (m[2].length>0 ? m[2] : "0") + (m[3].length>0 ? "." + m[3] : "");
        num = parseFloat(cleanInput);
        return (isNaN(num) ? null : num);
    }
    else if (dataType == "Currency") {
        var hasDigits = (val.digits > 0);
        var beginGroupSize, subsequentGroupSize;
        var groupSizeNum = parseInt(getIAttribute(val,"groupsize"), 10);
        if (!isNaN(groupSizeNum) && groupSizeNum > 0) {
            beginGroupSize = "{1," + groupSizeNum + "}";
            subsequentGroupSize = "{" + groupSizeNum + "}";
        }
        else {
            beginGroupSize = subsequentGroupSize = "+";
        }
        exp = new RegExp("^\\s*([-\\+])?((\\d" + beginGroupSize + "(\\" + getIAttribute(val,"groupchar") + "\\d" + subsequentGroupSize + ")+)|\\d*)"
                        + (hasDigits ? "\\" + getIAttribute(val,"decimalchar") + "?(\\d{0," + getIAttribute(val,"digits") + "})" : "")
                        + "\\s*$");
        m = op.match(exp);
        if (m == null)
            return null;
        if (m[2].length == 0 && hasDigits && m[5].length == 0)
            return null;
        cleanInput = m[1] + m[2].replace(new RegExp("(\\" + getIAttribute(val,"groupchar") + ")", "g"), "") + ((hasDigits && m[5].length > 0) ? "." + m[5] : "");
        num = parseFloat(cleanInput);
        return (isNaN(num) ? null : num);
    }
    else if (dataType == "Date") {
        var day, month, year;
        var date = op.split("-");
        day = date[0];
        month = parseMonth(date[1]);
        year = date[2];      
        month -= 1;
        var date = new Date(year, month, day);
        if (year < 100) {
            date.setFullYear(year);
        }
        return (typeof(date) == "object" && year == date.getFullYear() && month == date.getMonth() && day == date.getDate()) ? date.valueOf() : null;
    }
    else {
        return op.toString();
    }
}
function ValidatorCompare(operand1, operand2, operator, val) {
    var dataType = getIAttribute(val,"type");
    var op1, op2;
    if ((op1 = ValidatorConvert(operand1, dataType, val)) == null)
        return false;
    if (operator == "DataTypeCheck")
        return true;            
    if ((op2 = ValidatorConvert(operand2, dataType, val)) == null)
        return true;
    switch (operator) {
        case "NotEqual":
            return (op1 != op2);
        case "GreaterThan":
            return (op1 > op2);
        case "GreaterThanEqual":
            return (op1 >= op2);
        case "LessThan":
            return (op1 < op2);
        case "LessThanEqual":
            return (op1 <= op2);
        default:
            return (op1 == op2);
    }
}
function CompareValidatorEvaluateIsValid(val) {
    var value = ValidatorGetValue(getIAttribute(val,"controltovalidate"));
    if (ValidatorTrim(value).length == 0)
        return true;
    var compareTo = "";
    if (null == document.all[getIAttribute(val,"controltocompare")]) {
        if (typeof(getIAttribute(val,"valuetocompare")) == "string") {
            compareTo = getIAttribute(val,"valuetocompare");
        }
    }
    else {
        compareTo = ValidatorGetValue(getIAttribute(val,"controltocompare"));
    }
    return ValidatorCompare(value, compareTo, getIAttribute(val,"operator"), val);
}
function CustomValidatorEvaluateIsValid(val) {
    var value = "";
    if (typeof(getIAttribute(val,"controltovalidate")) == "string") {
        value = ValidatorGetValue(getIAttribute(val,"controltovalidate"));
        if ((ValidatorTrim(value).length == 0) &&
            ((typeof(getIAttribute(val,"validateemptytext")) != "string") || (getIAttribute(val,"validateemptytext")!= "true"))) {
            return true;
        }
    }
    var args = { Value:value, IsValid:true };
    if (typeof(val.clientvalidationfunction) == "string") {
			eval(val.clientvalidationfunction + "(val, args) ;");
    }
    return args.IsValid;
}
function RegularExpressionValidatorEvaluateIsValid(val) {
    var value = ValidatorGetValue(getIAttribute(val,"controltovalidate"));
    if (ValidatorTrim(value).length == 0)
        return true;
    var rx = new RegExp(getIAttribute(val,"validationexpression"));
    var matches = rx.exec(value);
    return (matches != null && value == matches[0]);
}
function ValidatorTrim(s) {
    var m = s.match(/^\s*(\S+(\s+\S+)*)\s*$/);
    return (m == null) ? "" : m[1];
}
function RequiredFieldValidatorEvaluateIsValid(val) {
    if ((ValidatorTrim(ValidatorGetValue(getIAttribute(val,"controltovalidate"))) != ValidatorTrim(getIAttribute(val,"initialvalue")))==true){    
		return true;
    }else{
		document.all[getIAttribute(val,"controltovalidate")].value=ValidatorTrim(ValidatorGetValue(getIAttribute(val,"controltovalidate")));
		return false;
    }
}
function RangeValidatorEvaluateIsValid(val) {
    var value = ValidatorGetValue(getIAttribute(val,"controltovalidate"));
    if (ValidatorTrim(value).length == 0)
        return true;
    return (ValidatorCompare(value, getIAttribute(val,"minimumvalue"), "GreaterThanEqual", val) &&
            ValidatorCompare(value, getIAttribute(val,"maximumvalue"), "LessThanEqual", val));
}
function ValidationSummaryOnSubmit() {
    ValidationSummaryOnSubmit(null);
}
function CustomLookupValidation(oSrc,args){
	var _IsValid=true;
	var ctl=document.all[getIAttribute(oSrc,"controltovalidate")]
	if (getIAttribute(ctl,"cT")!="undefined" && getIAttribute(ctl,"cT")!=null)
	{
		if (getIAttribute(ctl,"cT")=="iLookup")
		{
		    if (Page_BlockLookupValidate)
		    {
			    //ctl.islkpval = oSrc.isvalid;
		        return ctl.islkpval;
		    }   
		    else
		    {
			    _IsValid=LookupValidate(oSrc,args);
			    oSrc.errormessage=getIAttribute(oSrc,"ctlErrMsg");
			    ctl.islkpval = _IsValid;
			    return _IsValid;
			}
		}
		else if (getIAttribute(ctl,"cT")=="iDate")
		{
			validateDate(oSrc,args);    
			_IsValid=args.IsValid;
			args.Value=document.all[getIAttribute(oSrc,"controltovalidate")].value;
			oSrc.errormessage=getIAttribute(oSrc,"ctlErrMsg");
			return _IsValid;
		}
		else if (getIAttribute(ctl,"cT")=="iContainer")
		{
			validateCNTRNO(oSrc,args);
			args.Value=document.all[getIAttribute(oSrc,"controltovalidate")].value;
			_IsValid=args.IsValid;
			return _IsValid;
		}
		if (_IsValid==false)
		{
			args.IsValid=_IsValid;
			return _IsValid;
		}
//		if (oSrc.valfn!=null && oSrc.valfn!="undefined" && oSrc.valfn!="")	
//		{	
//			if (typeof(oSrc.valfn) == "string") {
//					eval(oSrc.valfn + "(oSrc, args) ;");
//			}
//			_IsValid=args.IsValid;
//			if (_IsValid==false)
//			{
//				oSrc.errormessage=oSrc.errormessage;
//				args.IsValid=_IsValid;
//				return _IsValid;
//			}
//		}
	}
}
function ValidationSummaryOnSubmit(validationGroup) {
    var summary;
    var s="";
    summary = pel("messagecontent");
    if (Page_BlockValidate){
		Page_IsValid=true;
		//summary.innerHTML = "";
        hideMessage();
		return;
	}
    if ((!Page_IsValid || !Grid_IsValid)) {
        for (i=0; i<Page_Validators.length; i++) {
            if (!Page_Validators[i].isvalid && typeof(Page_Validators[i].errormessage) == "string") {
				s += Page_Validators[i].errormessage + "<br>";
            }
        }
        if (s.length>0){
            s=s.substring(0,s.length-4);
            showErrorMessage(s);
		    return;
        }
    }
    hideMessage();
}
function CustomBoxValidation(oSrc,args){
  
	if (oSrc==null)
		return;
	var _IsValid=true;	
	if (getIAttribute(oSrc,"q")=="t")
		_IsValid=RequiredFieldValidatorEvaluateIsValid(oSrc,args);
	if (_IsValid==false)
	{
		oSrc.errormessage=getIAttribute(oSrc,"ReqErrMsg");
		args.IsValid=_IsValid;
		return;
	}
	if (getIAttribute(oSrc,"g")=="t")
		_IsValid=RegularExpressionValidatorEvaluateIsValid(oSrc);
	if (_IsValid==false)
	{
		oSrc.errormessage=getIAttribute(oSrc,"RegErrMsg");
		args.IsValid=_IsValid;
		return;
	}
    if (getIAttribute(oSrc,"l")=="t" && args.Value!=""){		
		//if (typeof(oSrc.gId)!="undefined")
		//{
		
		   oSrc.errormessage=getIAttribute(oSrc,"LkpErrMsg");
		    _IsValid=CustomLookupValidation(oSrc,args);
		    if (_IsValid==false)
            {
	            oSrc.errormessage=getIAttribute(oSrc,"LkpErrMsg");
	            args.IsValid=_IsValid;		        
	            return;
            }
		//}
	}	
	if (getIAttribute(oSrc,"n")=="t")
		_IsValid=RangeValidatorEvaluateIsValid(oSrc);
	if (_IsValid==false)
	{
		oSrc.errormessage=getIAttribute(oSrc,"RngErrMsg");
		args.IsValid=_IsValid;
		return;
	}		
    if (getIAttribute(oSrc,"cm")=="t")
		_IsValid=CompareValidatorEvaluateIsValid(oSrc);
	if (_IsValid==false)
	{
		oSrc.errormessage=getIAttribute(oSrc,"CmpErrMsg");
		args.IsValid=_IsValid;
		return;
	}
	
	/*if (_IsValid==false)
	{
		if (oSrc.pkVal=="false")
		{
			oSrc.pkVal="true";
			document.all[oSrc.controltovalidate].defaultValue=document.all[oSrc.controltovalidate].value;
		}
		oSrc.errormessage=oSrc.CtlErrMsg;
		args.IsValid=_IsValid;
		return;
	}*/
	if (getIAttribute(oSrc,"cu")=="t")
	{
		oSrc.clientvalidationfunction=getIAttribute(oSrc,"valfn");	
		_IsValid=CustomValidatorEvaluateIsValid(oSrc);
		oSrc.clientvalidationfunction="CustomBoxValidation";
	}
	if (_IsValid==false)
	{
		if (getIAttribute(oSrc,"CsvErrMsg")!='' ||oSrc.errormessage !='' )
		{
		    if(oSrc.errormessage == '')		    
		    {
			    oSrc.errormessage=getIAttribute(oSrc,"CsvErrMsg");
			}    
	    }		
		args.IsValid=_IsValid;
		return;
	}
	/*if (oSrc.pkVal=="false")
	{
		var obj=document.all[oSrc.controltovalidate];
		if (obj.value!=obj.defaultValue && obj.IsCheckedOnKd==true){
			oSrc.pkVal="true";
			args.IsValid=true;
			return
		}
		args.IsValid=false;
		if (typeof(oSrc.gId)!="undefined")
		{
			var _tbl=document.all[oSrc.gId]
			oSrc.errormessage=_tbl.parentNode.pkErrMsg;
		}
		return;
	}*/
}
function CustomValidatorUpdate(val)
{
	ValidatorUpdateDisplay(val);
	ValidatorUpdateIsValid();
}
function SetValidatorsInValid(ctrl, msg) {
    if (ctrl != null && ctrl.Validators!=null) {
        var validators = ctrl.Validators;
        var i,val;
        for (i = 0; i < validators.length; i++) {
            val = validators[i]
            val.isvalid=false;
            val.errormessage=msg;
            break;
        }
        ctrl.isvalid = val.isvalid;
        Grid_IsValid = ctrl.isvalid;
        if (!val.isvalid && !Page_FocusingInvalidControl && val.focusOnError == "t") {                
            if (typeof(ctrl) != "undefined") {
                if ((ctrl != null) &&
                    ((ctrl.tagName.toLowerCase() != "input") || (ctrl.type.toLowerCase() != "hidden") || (ctrl.tagName.toLowerCase() != "textarea")) &&
                    (ctrl.disabled == false) &&
                    (ctrl.visible != false) &&
                    IsInVisibleContainer(ctrl)) {
                    try{
                    ctrl.focus();
                    }catch(e){}
                    Page_FocusingInvalidControl = true;
                }
            }
        }
        ValidatorUpdateIsValid();
        ValidatorUpdateDisplay(val); 
    }
}
function ResetValidators(validationGroup)
{
    if (!Page_IsValid) {
        for (ValidatorsCount = 0; ValidatorsCount < Page_Validators.length; ValidatorsCount++) {
            var val = Page_Validators[ValidatorsCount];
            if (val != null && IsValidationGroupMatch(val, validationGroup)) {
                val.isvalid = true;
                if (val.controltovalidate!="" && document.all[val.controltovalidate]!=null){
                    document.all[val.controltovalidate].isvalid=true;
                    //document.all[val.controltovalidate].IsCheckedOnKd=false;
                }
                ValidatorUpdateDisplay(val);
                ValidatorUpdateIsValid();
            }
        }
        ValidationSummaryOnSubmit(validationGroup);
    }
}
/* Custom Datagrid Validator Ends*/-->