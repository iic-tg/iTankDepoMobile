
//This method is used to set the focus in the User Id field;Target:Page Load
function setFocus()
{
    el("txtUserID").focus();
}

//This method is used to Validate the Email ID;Target:Custom Validation Function
function validateEmailId(oSrc,args) {
    if (el('txtEmailId').value == "") {
        args.IsValid = false;
        showErrorPane("Email ID Required");
        return;
    }
    else if (emlregExVn(el('txtEmailId').value) == false) {
        args.IsValid = false;
        showErrorPane("Invalid Email ID");
        return;
    }
    else {
        var objCallback = new Callback();
        objCallback.add("EmailId", unescape(el('txtEmailId').value));
        objCallback.invoke("ForgotPassword.aspx", "ValidateEmailId");

        if (objCallback.getCallbackStatus()) {

            var vResult;
            var vPassword;
            vResult = objCallback.getReturnValue("pkvalid");

            if (vResult == "true") {
                vPassword = getPassword(7, '', false, false, false, false, false, true, false);
                el('hdnPwd').value = vPassword;
                vPassword = b64_md5(vPassword);
                el('hdnHashPwd').value = vPassword;
                args.IsValid = true;
                hideMsg();
            }
            else {
                showErrorPane("This is invalid Email Id");
                args.IsValid = false;
            }
        }
        else {
            showErrorPane(objCallback.getCallbackError());
        }
        objCallback = null;
    }
}

//This method is used to Send the password to respective user;Target:Submit
function sendPassword()
{
    Page_ClientValidate();
    if(!Page_IsValid)
	{
		return false;
	}
	else
	{
        var objCallback = new Callback();
        objCallback.add("RandPwd", el('hdnPwd').value);
        objCallback.add("HashPwd", el('hdnHashPwd').value);
        objCallback.add("EmailId",el('txtEmailId').value);
        objCallback.add("UserName",el('txtUserID').value);
        objCallback.invoke("ForgotPassword.aspx", "SendPassword")
        
        if(objCallback.getCallbackStatus()) {
            if (objCallback.getReturnValue("UserEmailValid") == "true") {
                showInfoPane("Your password has been reset and sent to your email address " + unescape(el('txtEmailId').value));
            } else if (objCallback.getReturnValue("UserEmailValid") == "false") {
                showErrorPane("Email ID doesn't match with this User Name");
            }            
        }
        else
        {        
           showErrorPane("Error sending email");
        }
        objCallback=null;
    }
}

//This method is used to validate the username for duplicates; Target:Custom Validation
function validateUserName(oSrc,args) {
    if (el('txtUserID').value == "") {
        args.IsValid = false;
        showErrorPane("User Name Required.");
        return;
    }
    else if (txtregExVn(el('txtUserID').value) == false) {
        args.IsValid = false;
        showErrorPane("Only Alphabets And Numbers are Allowed");
        return;
    }
    else {
        var objCallback = new Callback();
        objCallback.add("UserName", el('txtUserID').value);
        objCallback.invoke("ForgotPassword.aspx", "ValidateUserName")
        if (objCallback.getCallbackStatus()) {
            var vResult;
            var vPassword;
            vResult = objCallback.getReturnValue("pkvalid");
            if (vResult == "true") {                
                showErrorPane("This User Name not exists");
                args.IsValid = false;
            }
            else {
                vPassword = getPassword(7, '', false, false, false, false, false, true, false);
                el('hdnPwd').value = vPassword;
                vPassword = b64_md5(vPassword);
                el('hdnHashPwd').value = vPassword;
                args.IsValid = true;
                hideMsg();
            }
        }
        else {
            showErrorPane(objCallback.getCallbackError());
        }
        objCallback = null;
    }
}

//This method is used to get the Random Number.
function getRandomNum(vLbound, vUbound) 
{
    return (Math.floor(Math.random() * (vUbound - vLbound)) + vLbound);
}

//This method is used to get the Random Character.
function getRandomChar(vNumber, vLower, vUpper, vOther, vExtra) 
{
    var vNumberChars = "0123456789";
    var vLowerChars = "abcdefghijklmnopqrstuvwxyz";
    var vUpperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var vOtherChars = "`~!@#$%^&*()-_=+[{]}\\|;:'\",<.>/? ";
    var vCharSet = vExtra;
    if (vNumber == true)
    vCharSet += vNumberChars;
    if (vLower == true)
    vCharSet += vLowerChars;
    if (vUpper == true)
    vCharSet += vUpperChars;
    if (vOther == true)
    vCharSet += vOtherChars;
    return vCharSet.charAt(getRandomNum(0, vCharSet.length));
}

//This method is used to get the Random Password.
function getPassword(vLength, vExtraChars, vFirstNumber, vFirstLower, vFirstUpper, vFirstOther,
                    vLatterNumber, vLatterLower, vLatterUpper, vLatterOther) 
{
    var vRC = "";    
    if (vLength > 0)
        vRC = vRC + getRandomChar(vFirstNumber, vFirstLower, vFirstUpper, vFirstOther, vExtraChars);    
    for (var idx = 1; idx < vLength; ++idx) 
    {
        vRC = vRC + getRandomChar(vLatterNumber, vLatterLower, vLatterUpper, vLatterOther, vExtraChars);
    }   
    vRC = vRC+getRandomNum(vLatterNumber,9);    
    return vRC;
}

//This method is used to Show the Information Message.
function showInfoPane(sMessage) {
    el("messagecontent").className = "infomessage";
    setText(el("messagetitle"),"INFORMATION :");
    el("messagetitle").className = "infomessagetitle";
    el("messagecontent").innerHTML = sMessage;
    el("message").style.display = "block";
}

//This method is used to Show the Error Message.
function showErrorPane(sMessage) {
    el("messagecontent").className = "errormessage";
    setText(el("messagetitle"),"ERROR :");
    el("messagetitle").className = "errormessagetitle";
    if (sMessage == "") {
        sMessage = "SYSTEM Error. Please contact administrator.";
    }
    el("messagecontent").innerHTML = sMessage;
    el("message").style.display = "block";
}

//This method is used to Hide the Information/Error  Message.
function hideMsg() {
    el("message").style.display = "none";
}

//This mehod is used to validate the userId with regular expression.
function txtregExVn(value) {
    var  pattern =/^[a-zA-Z0-9]+$/;
    return pattern.test(value);
}

//This mehod is used to validate the Email ID  with regular expression.
function emlregExVn(value) {    
    var pattern = /^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$/;
    return pattern.test(value);
}