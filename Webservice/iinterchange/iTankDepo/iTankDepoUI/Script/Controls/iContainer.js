<!--
//*************************************************************************//
//**		This function called by validation method for				 **//
//**		container number validation 4A,6N,1C.						 **//
//*************************************************************************//
//var _cntrver="1.0.3127.290908";
var a=new Array(10,12,13,14,15,16,17,18,19,20,21,23,24,25,26,27,28,29,30,31,32,34,35,36,37,38);
function validateCNTRNO(oSrc, args)
{
		var str = trimAll(args.Value);
		var UnitIDA = str.substr(0,4).toUpperCase();
		var UnitIDN = str.substr(4,6);
	    if (trimAll(UnitIDN).length!=6)
	    {
	        args.IsValid = false;
			return;
	    }
		var oParent=document.getElementById(getIAttribute(oSrc,"controltovalidate"));
		//if (typeof(oParent.lpCheckDigit)=='undefined' || oParent.lpCheckDigit=='false')
		if (typeof(getIAttribute(oParent,"lpCheckDigit"))=='undefined' || ((getIAttribute(oParent,"lpCheckDigit")=='false' || getIAttribute(oParent,"lpCheckDigit")=='true') && str.length==11))//if (typeof(oParent.lpCheckDigit)=='undefined' || oParent.lpCheckDigit=='false')
		{
			if (typeof(getIAttribute(oParent,"lpCheckDigit"))=='undefined')
				setIAttribute(oParent,"lpCheckDigit","false");
			var UnitIDC = str.substr(10,1);
		}

		//error message format.
		var msg;
		var spmsg = ""; // this is specific error message.
		msg = "<ul type=square><li>";
		msg += "4 Alpha characters - mandatory.</li>";
		msg += "<li>6 digit Number - mandatory.</li>";
		msg += "<li>1 check digit - optional.</li></ul>";

		if(str == "" || str.length < 10)
		{
			spmsg = "Enter 11 digit container no!<br>";
			spmsg +=  msg;
			oSrc.errormessage = spmsg;
			args.IsValid = false;
			return;
		}
		else if((UnitIDA.charCodeAt(0)<65 || UnitIDA.charCodeAt(0)>91) || (UnitIDA.charCodeAt(1)<65 || UnitIDA.charCodeAt(1)>91) || (UnitIDA.charCodeAt(2)<65 || UnitIDA.charCodeAt(2)>91) || (UnitIDA.charCodeAt(3)<65 || UnitIDA.charCodeAt(3)>91))
		{//alpha check.
			spmsg = "First 4 digit should be Alpha characters!<br>";
			spmsg +=  msg;
			oSrc.errormessage=spmsg;
			args.IsValid= false;
			return;
		}
		else if(isNaN(UnitIDN))
		{//numeric check.
			spmsg = "Invalid 6 digit numeric in 11 digit container number!<br>";
			spmsg +=  msg;
			//alert(spmsg);
			oSrc.errormessage=spmsg;
			args.IsValid= false;
			return;
		}
		else if ((isNaN(UnitIDC) || UnitIDC=="") && getIAttribute(oParent,"lpCheckDigit")=='false')
		{//Check Digit
			spmsg = "Check Digit Should be Numeric!<br>";
			spmsg += msg;
			//alert(spmsg);
			oSrc.errormessage=spmsg;
			args.IsValid= false;
			return;
		}
		else if (typeof(getIAttribute(oParent,"lpCheckDigit"))=='undefined' || (getIAttribute(oParent,"lpCheckDigit")=='false' || (getIAttribute(oParent,"lpCheckDigit")=='true' && str.length==11)))//else if (typeof(oParent.lpCheckDigit)=='undefined' || oParent.lpCheckDigit=='false')
		{
			if (UnitIDC != getCheckSum(str.substr(0,10)))
			{
				spmsg = "Invalid Check Digit!<br>";
				spmsg +=  msg;
				//alert(spmsg);	
				oSrc.errormessage=spmsg;
				args.IsValid= false;
				return;
			}
			else
			{
				args.IsValid= true;
				return;
			}
		}
		else if (getIAttribute(oParent,"lpCheckDigit")=='true')
		{
			oParent.value=str.substr(0,4).toUpperCase() + str.substr(4,6) + getCheckSum(str.substr(0,10));
			args.IsValid= true;
			return;
		}
		else
		{
			//oParent.value=str.substr(0,4).toUpperCase() + str.substr(4,6) + getCheckSum(str.substr(0,10));
			args.IsValid= true;
			return;
		}
}


function getCheckSum(strNumber) 
{	
	strNumber=strNumber.toUpperCase();
	var retVal=true;
	var total=parseInt("0")
	var product;
	if(strNumber.length==10) 
	{
		for(var i=0;retVal=true,i<strNumber.length;i++) 
		{	      
			var strCharCode=strNumber.charCodeAt(i);
			// Check For Alphabet
			if(strCharCode>=65 && strCharCode<=90) 
			{							
				product=a[strCharCode-65]
			}
			else 
			{
				// Check for Number
				if(strCharCode>=48 && strCharCode<=57) 
				{
					product=(strCharCode-48) 
				}
				//Not a Number or Alphabet
				else
				retVal=false;
			}
			total=total + ((parseInt(product))*Math.pow(2,i))
		}

		if(retVal) 
		{
			var cs=(total%11)%10;
			return cs;
		}  
	}
} 

-->