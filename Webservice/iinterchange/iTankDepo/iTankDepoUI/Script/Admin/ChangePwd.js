
var pwd;

//This mehod is used to validate the Password.
function validatePassword(oSrc,args)
{
       var objCallback = new Callback();
       var epwd='';
       pwd = el('txtOldPassword').value;
       if((typeof(pwd)!='') ||(typeof(pwd)!="undefined"))
       {
        epwd = b64_md5(el('txtOldPassword').value);
        el('hdnOpd').value = epwd;
       }
       objCallback.add("OldPassword",el('hdnOpd').value );
              
       objCallback.invoke("ChangePassword.aspx","ValidatePassword")
       
       if(objCallback.getCallbackStatus())
       {
            var Validateresult;
            Validateresult = objCallback.getReturnValue("pkvalid");
           
            if(Validateresult == "true")
            {
                args.IsValid = true;
            }
            else
            {
                oSrc.errormessage="The Old Password is Invalid";
                args.IsValid = false;
            }
       }
       else
       {
           showErrorMessage(objCallback.getCallbackError());           
       }
}

//This mehod is used to Initialize the page.
function initPage()
{
   setFocusToField("txtOldPassword");
}

//This method is used to encrypt the password with MD5.
function encryptPassword()
{
    document.getElementById("hdnNpd").value = b64_md5(document.getElementById("txtNewPassword").value);
}

//This method is used to Submit the page
function changePassword()
{
    Page_ClientValidate();
    if(!Page_IsValid)
	{
		return false;
	}
	else
	{
        var objCallback = new Callback();

        objCallback.add("NewPwd",  b64_md5(el('txtNewPassword').value));
        
        objCallback.invoke("ChangePassword.aspx","ChangePassword")
        
        if(objCallback.getCallbackStatus())
        {
            showInfoMessage("Your password has been changed successfully");            
        }
        else
        {
           showErrorMessage("There is an Error in changing password");
        }
        objCallback=null;
    }
}
