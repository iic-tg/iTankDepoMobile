function setFocus() {
    try {
        document.getElementById("txtUserName").focus();
        fitLoginPane();
    } catch (e) { }
}
var fwWindow;
function openForgotPassword() {
    var strProperties = 'width=510px,height=250px,top=249,left=296,toolbars=no,scrollbars=no,status=no,resizable=no,minimize=yes,maximise=no';
   fwWindow = window.open('Admin/ForgotPassword.aspx','Forgot_Password', strProperties);    
    return false;
}
function showDisplay()
{
    alert("fgfg")
}
function encrptPassword() {
    
    document.getElementById("hdnPd").value = b64_md5(document.getElementById("txtPassword").value);
    return false;
}

function encrptPasswordForMobile() {
    alert('success');
    document.getElementById("hdnPd").value = b64_md5();
    return document.getElementById("hdnPd").value;
}

function checkCapsLock(event) {
    if (!event)
        event=getEvent(event)
    if (isCapslock(event)) {
        el("errcontent").style.display = "block";
        setText(el("errLabel"),"CAPS LOCK is ON");
    }
    else {
        el("errcontent").style.display = "none";
        setText(el("errLabel"),"");
    }
}

function isCapslock(e) {
    if (!e) {
        var e = window.event;
    }
    var code = false;
    if (e.keyCode) {
        code = e.keyCode;
    } else if (e.which) {
        code = e.which;
    }
    var shifton = false;
    if (e.shiftKey) {
        shifton = e.shiftKey;
    }
    if (code >= 97 && code <= 122 && shifton) {
        return true;
    }
    if (code >= 65 && code <= 90 && !shifton) {
        return true;
    }
    return false;
}
window.onresize = function () {
   // fitLoginPane();
};
function fitLoginPane() {
    if (el("tbllogin") != null) {
        el("tbllogin").style.height = (document.documentElement.clientHeight) + "px";
    }
}