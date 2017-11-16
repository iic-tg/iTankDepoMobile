//Init Page
function initPage(sMode) {
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    var ifgUnlock = new ClientiFlexGrid("ifgUnlock");
    ifgUnlock.DataBind();
    reSizePane();
}

//TO Unlock the Record
function unlockRecord(refNoField, refno, actvty) {
    var oCallback = new Callback();
    oCallback.add("ReferenceNoField", refNoField);
    oCallback.add("ReferenceNo", refno);
    oCallback.add("Activity", actvty);
    oCallback.invoke("Unlock.aspx", "UnlockRecord");
    if (oCallback.getCallbackStatus()) {

    }
    var ifgUnlock = new ClientiFlexGrid("ifgUnlock");
    ifgUnlock.DataBind();

}

//List the Records
function showHideRecords(param) 
{
    if (param["norecordsfound"] == "True")
     { 
     el("divUnlock").style.display = "none";
     el("divRecordNotFound").style.display = "block";
     }
     else
     {
         el("divUnlock").style.display = "block";
         el("divRecordNotFound").style.display = "none";
     }

 }
 try {
     $(window.parent).resize(function () {
         reSizePane();
     });
 }
 catch (e) { }
 

 function reSizePane() {
     if ($(window) != null) {
         if ($(window.parent)) {
             if ($(window.parent).height() < 670) {
                 el("divUnlock").style.height = $(window.parent).height() - 221 + "px";
                 if (el("ifgUnlock") != null) {
                     el("ifgUnlock").SetStaticHeaderHeight($(window.parent).height() - 292 + "px");
                 }
             }
             else if ($(window.parent).height() < 768) {
                 el("divUnlock").style.height = $(window.parent).height() - 350 + "px";
                 if (el("ifgUnlock") != null) {
                     el("ifgUnlock").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
                 }
             }
             else {
                 el("divUnlock").style.height = $(window.parent).height() - 231 + "px";
                 if (el("ifgUnlock") != null) {
                     el("ifgUnlock").SetStaticHeaderHeight($(window.parent).height() - 282 + "px");
                 }
             }
         }
     }
 }
