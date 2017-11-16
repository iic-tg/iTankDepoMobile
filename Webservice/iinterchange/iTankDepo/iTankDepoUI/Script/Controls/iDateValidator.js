<!--
//var _dtver="1.0.3127.230609";
var dtCh= "-";
var dtCh1= "/";  // Added by SRT
var minYear=00;
var maxYear=99;
var _calvis=0;
function isInteger(s){
	var i;
    for (i = 0; i < s.length; i++){   
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag){
	var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++){   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary (year){
	// February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}
function DaysArray(n) {
	for (var i = 1; i <= n; i++) {
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		if (i==2) {this[i] = 29}
   } 
   return this
}
function isDate(oSrc, dtStr)
{
    if((trim(dtStr).indexOf(".") != -1)||(trim(dtStr).indexOf(" ") != -1)) 	 	
	{
	    return false;
	}
	else if((trim(dtStr).charAt(2) == '-' || trim(dtStr).charAt(1) == '-') && (trim(dtStr).length >= 7 || trim(dtStr).length <= 11))
	{
		return isDD_MM_YYDate(oSrc, dtStr);	
	}	
	else if((trim(dtStr).charAt(2) == '/' || trim(dtStr).charAt(1) == '/') && (trim(dtStr).length >= 8 || trim(dtStr).length <= 10))
	{
		return isDD_MM_YYDate(oSrc, dtStr);	
	}	
	else if((parseInt(trim(dtStr).charAt(2)) >= 0 || parseInt(trim(dtStr).charAt(1)) <= 9) && (trim(dtStr).length >= 5 || trim(dtStr).length <= 8))
	{
		return isDDMMYYDate(oSrc, dtStr);	
	}	
	else
	{
		return false;
	}
}
//DDMMYY
function isDDMMYYDate(oSrc, dtStr)
{
	var daysInMonth = DaysArray(12)	
	var strDay=dtStr.substring(0,2)
	var strMonth=dtStr.substring(2,4)
	var strYear=dtStr.substring(4)
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
//	for (var i = 1; i <= 3; i++) {
//		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
//	}
	//Check Month
	if (isNaN(strMonth))
		return false;
	else
		month=parseInt(strMonth)
	//Check Day
	if (isNaN(strDay))
		return false;
	else
		day=parseInt(strDay)
	//Check Year
	if (isNaN(strYr))
		return false;
	else
	{
		year = GetYear(strYr);
	}
	var calobj = new Calendar();
	if (year<calobj.minYear)
	{
	    return false;
	}
	if (month < 1 || month > 12){
		//alert("Please enter a valid month")
		return false
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		//alert("Please enter a valid day")
		return false
	}
	//if (strYear.length != 2 || year<minYear || year>maxYear){ - Changed By SRT
	if (!(strYr.length == 2 || strYr.length == 4)) { //|| year<minYear || year>maxYear){
		//alert("Please enter a valid 2 digit year between "+minYear+" and "+maxYear)		
		return false
	}
	// Set DD-MON-YY format.
	month = month - 1;
	var fdate = new Date(year,month,day);
	//added by SRT
	var fdaystr=fdate.getDate().toString();
	if (fdaystr.length==1)
		fdaystr="0" + fdaystr;
	var fstr = fdaystr+"-"+JsMonthArray[fdate.getMonth()]+"-" + new String(fdate.getFullYear()).substring(0,4);
	//var fstr = fdate.getDate()+"-"+JsMonthArray[fdate.getMonth()]+"-" + new String(fdate.getFullYear()).substring(2,4);
	document.getElementById(getIAttribute(oSrc,"controltovalidate")).value = fstr
	ValidationSummaryOnSubmit();
	return true
}
//DD-MM-YY
function isDD_MM_YYDate(oSrc, dtStr)
{
	var daysInMonth = DaysArray(12)
	//ADDED by SRT
	if (dtStr.indexOf(dtCh)>0)
	{
		var pos1=dtStr.indexOf(dtCh)
		var pos2=dtStr.indexOf(dtCh,pos1+1)
	}
	else if (dtStr.indexOf(dtCh1)>0)
	{
		var pos1=dtStr.indexOf(dtCh1)
		var pos2=dtStr.indexOf(dtCh1,pos1+1)
	}
	else
	{
		return false;
	}
	var strDay=dtStr.substring(0,pos1)
	var strMonth=dtStr.substring(pos1+1,pos2)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)

	//Check Month
	if (isNaN(strMonth))
	{
		//ADDED BY SRT
		strMonth=parseMonth(strMonth);
		if (isNaN(strMonth))
			return false;
		else
			month=parseInt(strMonth)
			//return false;
	}
	else
	{
		month=parseInt(strMonth)
	}
	if (isNaN(month))
	    return false;
	//Check Day
	if (isNaN(strDay))
		return false;
	else
		day=parseInt(strDay)
		
	//Check Year
	if (isNaN(strYr))
		return false;
	else
	{
	    year = GetYear(strYr);
	    if (isNaN(year))
		    return false;
	}
	var calobj = new Calendar();
	if (year<calobj.minYear)
	{
	    return false;
	}
		
	if (pos1==-1 || pos2==-1){
		//alert("The date format should be : dd/mm/yyyy")
		return false
	}
	if (month < 1 || month > 12){
		//alert("Please enter a valid month")
		return false
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		//alert("Please enter a valid day")
		return false
	}
	if (!(strYr.length == 2 || strYr.length == 4)) { //|| year<minYear || year>maxYear){
		//alert("Please enter a valid 2 digit year between "+minYear+" and "+maxYear)		
		return false
	}
    
	// Set DD-MON-YY format.
	month = month - 1;
	var fdate = new Date(year,month,day);
	//added by SRT
	var fdaystr=fdate.getDate().toString();
	if (fdaystr.length==1)
		fdaystr="0" + fdaystr;
	var fstr = fdaystr+"-"+JsMonthArray[fdate.getMonth()]+"-" + new String(fdate.getFullYear()).substring(0,4);
	//var fstr = fdate.getDate()+"-"+JsMonthArray[fdate.getMonth()]+"-" + new String(fdate.getFullYear()).substring(2,4);
	document.getElementById(getIAttribute(oSrc,"controltovalidate")).value = fstr
	ValidationSummaryOnSubmit();
	return true
}

//DDMONYY
function isDD_MON_YYDate(oSrc,dtStr)
{
	var daysInMonth = DaysArray(12)
	if (dtStr.indexOf(dtCh)>0)
	{
		var pos1=dtStr.indexOf(dtCh)
		var pos2=dtStr.indexOf(dtCh,pos1+1)
		var strDay=dtStr.substring(0,pos1)
		var strMonth=dtStr.substring(pos1+1,pos2)
		var strYear=dtStr.substring(pos2+1)
	}
	else
	{
		var pos1=2
		var pos2=5
		var strDay=dtStr.substring(0,pos1)
		var strMonth=dtStr.substring(pos1,pos2)
		var strYear=dtStr.substring(pos2)
	}
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	
	month=parseMonth(strMonth)
	//Check Day
	if (isNaN(strDay))
		return false;
	else
		day=parseInt(strDay)
	//Check Year
	if (isNaN(strYr))
		return false;
	else
	{
    	year = GetYear(strYr);
	}

	var calobj = new Calendar();
	if (year<calobj.minYear)
	{
	    return false;
	}
	if (pos1==-1 || pos2==-1){
//		alert("The date format should be : dd/mm/yyyy")
		return false;
	}
	if (strMonth.length<1 || isMonth(strMonth)){
//		alert("Please enter a valid month")
		return false;
	}
//	alert(daysInMonth[month])
	if (strDay.length<1 || day<1 || day>31 || (strMonth=="FEB" && day>daysInFebruary(year)) || day > daysInMonth[month]){
//		alert("Please enter a valid day")
		return false;
	}
	if (!(strYear.length == 2 || strYear.length == 4)){ //  year<minYear || year>maxYear){
//		alert("Please enter a valid 2 digit year between "+minYear+" and "+maxYear)
		return false;
	}
	if (dtStr.length==9)
	{
		month = month - 1;
		var fdate = new Date(year,month,day);
		//added by SRT
		var fdaystr=fdate.getDate().toString();
		if (fdaystr.length==1)
			fdaystr="0" + fdaystr;
		var fstr = fdaystr+"-"+JsMonthArray[fdate.getMonth()]+"-" + new String(fdate.getFullYear()).substring(0,4);
		//var fstr = fdate.getDate()+"-"+JsMonthArray[fdate.getMonth()]+"-" + new String(fdate.getFullYear()).substring(2,4);
		document.getElementById(getIAttribute(oSrc,"controltovalidate")).value = fstr
	}
	ValidationSummaryOnSubmit();
	return true;
}
function GetYear(strYr)
{
    var year=strYr*1;
	if (strYr.length == 2 && year<=49){
	    strYr = "20" + strYr		  	
        year=strYr*1
    }
    else if(strYr.length == 2 && (year<99 || year>50))
    {
        strYr = "19" +strYr 
        year=strYr*1
    }
    else{
        year=strYr;
        //year=strYr*1
    }
    return year;
}
function isMonth(strMonth)
{
	if (strMonth =="JAN" ||strMonth =="FEB"||strMonth =="MAR"||strMonth =="APR"||strMonth =="MAY"||strMonth =="JUN"||strMonth =="JUL"||strMonth =="AUG"||strMonth =="SEP"||strMonth =="OCT"||strMonth =="NOV"||strMonth =="DEC")
		return false;
	else
		return true;
}
//Return Month index number
function parseMonth(strMonth)
{
    if (typeof(strMonth)=="undefined")
        return;
	switch (strMonth.toUpperCase())
	{
		case "JAN":
			return 1;
		break;
		case "FEB":
			return 2;
		break;
		case "MAR":
			return 3;
		break;
		case "APR":
			return 4;
		break;
		case "MAY":
			return 5;
		break;
		case "JUN":
			return 6;
		break;
		case "JUL":
			return 7;
		break;
		case "AUG":
			return 8;
		break;
		case "SEP":
			return 9;
		break;
		case "OCT":
			return 10;
		break;
		case "NOV":
			return 11;
		break;
		case "DEC":
			return 12;
		break;
	}
}
function validateDate(oSrc, args)
{
	if (args.Value=="")
		return;
	if (isDate(oSrc, args.Value.toUpperCase())==false)
	{
		args.IsValid = false;		
	}
	else
	{
	    args.IsValid = true;
	}
 }
 var JsMonthArray = new Array();
 JsMonthArray[0] = "JAN";
 JsMonthArray[1] = "FEB";
 JsMonthArray[2] = "MAR";
 JsMonthArray[3] = "APR";
 JsMonthArray[4] = "MAY";
 JsMonthArray[5] = "JUN";
 JsMonthArray[6] = "JUL";
 JsMonthArray[7] = "AUG";
 JsMonthArray[8] = "SEP";
 JsMonthArray[9] = "OCT";
 JsMonthArray[10] = "NOV";
 JsMonthArray[11] = "DEC";
//*****************************************************************************/
//	DATE VALIDATION END
//*****************************************************************************/ 

//*************************************************************************//
//**		This is to trim a string. Left trim and Right trim of 		 **//
//**		a string. This is called by other routines in				 **//
//**		this JS file.												 **//
//*************************************************************************//
function trim(s) 
{
  while (s.substring(0,1) == ' ') {
    s = s.substring(1,s.length);
  }
  while (s.substring(s.length-1,s.length) == ' ') {
    s = s.substring(0,s.length-1);
  }
  return s;
}
//*************************************************************************//
//**		This is to open datepicker window to allow dateselection.	 **//
//**		it has top,left property to determine in which X(pixel)		 **//
//**		and Y(pixel) position of monitor it should display .		 **//
//*************************************************************************//
var gObjCtrl = null;
var iCalendar = null;
function GetDatePicker(obj,lp,tp)
{
	try
	{
		if (obj!=null)
		{
			var objCtrl;
			if (obj.type=="text"){
				objCtrl=obj;
			}
			else{
				objCtrl=document.all[obj.lpParent];
			}
			if (objCtrl.className=="DisTxt" || objCtrl.readOnly==true || objCtrl.disabled==true)
				return;
			//JS Calendar Initialization.
			gObjCtrl = objCtrl;
			iCalendar = new Calendar(0, new Date(), oniCalendarSelect, oniCalendarClose);
			//iCalendar.calObj=objCtrl;
			//iCalendar.setDisabledHandler(disallowDate);
			iCalendar.create();
			//iCalendar.create(parent.document.body);
			//iCalendar.showAtElement(document.getElementById("1"+CtrlName));
            iCalendar.showAtElement(gObjCtrl,lp,tp);            
			_calvis=1;
		}
	}catch (e) {
		alert(e.message);
	}
} 
function GetDatePickerGrid(obj,y,lp,tp)
{
	try
	{
		if (obj!=null)
		{
			var objCtrl;
			if (obj.type=="text"){
				objCtrl=obj;
			}
			else{
				objCtrl=document.all[obj.lpParent];
			}
			if (objCtrl.className=="DisTxt" || objCtrl.readOnly==true || objCtrl.disabled==true)
				return;
			//JS Calendar Initialization.
			gObjCtrl = objCtrl;
			iCalendar = new Calendar(0, new Date(), oniCalendarSelect, oniCalendarClose);
			iCalendar.create();			
            var p=Calendar.getAbsolutePos(gObjCtrl);
			iCalendar.showAt(p.x,y,lp,tp);
			_calvis=1;
		}
	}catch (e) {
		alert(e.message);
	}
} 
function disallowDate(date) 
{
  var today = new Date();
  // date is a JS Date object
  if (date > today)
  {
    return true; // disable 
  }
  return false; // enable other dates
}
function oniCalendarSelect(calendar, date)
{
	var iDay = calendar.date.getDate();
	if (iDay < 10)
	{
		iDay = ("0" + iDay);
	}
	var strFormatedDate = iDay + "-" + JsMonthArray[calendar.date.getMonth()] + "-" + new String(calendar.date.getFullYear()).substring(0,4);
	var _valid=true;
	if (strFormatedDate!=gObjCtrl.value && gObjCtrl.onchange!=null)
	{
		gObjCtrl.value = strFormatedDate;
		var ev;
		gObjCtrl.IsCheckedOnKd=true;
		ev = gObjCtrl.onchange;
		if (typeof(ev) == "function" ) {
			ev = ev.toString();
			ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
			if (ev.indexOf("ValidatorOnChange();")!=-1){
				ev=ev.replace("ValidatorOnChange();","ValidatorOnChange('" + gObjCtrl.id + "');");
			}
		}
		if (ev!="")
			eval(ev)
		gObjCtrl.hasChanges=false;
		if (ev.indexOf("ValidatorOnChange")!=-1){
		    if (AllValidatorsValid(gObjCtrl.Validators,gObjCtrl))
		        _valid=true;
		    else
		        _valid=false;
		}
	}
	gObjCtrl.value = strFormatedDate;
	calendar._focus = true;
	var event=getEvent(event);
	event.cancelBubble = true;
	gObjCtrl.focus();
	if(!Calendar.is_ie){
	    if(!Calendar.is_opera){
	        if(Calendar.dateClicked){
	          calendar.hide();
	          iCalendar = null;
	          }
    
	  }
	}
	try
	{
		//if (event.keyCode==13)
		if (event.keyCode==13 || _valid)
			gObjCtrl.focus();
		else
		    calendar._focus=false;
	}
	catch(e){}
}
function oniCalendarClose(calendar)
{
	calendar.hide();
	iCalendar = null;
	if (typeof(calendar._focus)!="undefined" && calendar._focus==false)
	    return;
	SetFocustogObjCtrl();
	//setTimeout("SetFocustogObjCtrl()",100);
}
function SetFocustogObjCtrl()
{
    try{gObjCtrl.focus() }catch(e){};
}
//Key Down to fire Onchange event of iDate Control
function idKeyDown(event)
{   
    if(!event)
        event=getEvent(event);
    var _obj=event.srcElement||event.target;
    if ((event.keyCode==9 || event.keyCode==13) && (_obj.hasChanges==true))
    {
        if (_obj.onchange!=null)
            _obj.onchange();
        _obj.hasChanges=false;
        if(event.preventDefault){
            event.preventDefault(true);
            event.stopPropagation();
        }
    }
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
//*************************************************************************//
//**		Datepicker window END.										 **//
//*************************************************************************//
-->