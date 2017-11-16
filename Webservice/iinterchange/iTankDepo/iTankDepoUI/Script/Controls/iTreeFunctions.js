var previd;
var CCHasChanges = false;
var HasChanges = false;
var chkAdd = false;
var chkEdit = false;
var chkView = false;
var chkCancel = false; //Cancel
//var chkAlert=false;
//var chkReminder=false;
//var chkPrint=false;
var Page_IsValid = true;
function PageSubmit() {
    el("btnSubmit").click();
}

//Hide the Action Pane
function hide() {
    //el("btnSubmit").visible="false" ;
}
//ex: fnNC('CPN1_PN1','CP');
function fnNC(id, type) {
    var rids, rcids;
    if (type == "P") {
        var d = eval("a" + id);
        if (d) {
            // var c = document.all.item("i" + id);
            var c = document.getElementById("i" + id);
            if (c) {
                //var iPath = document.all.item("i" + id).src;
                var iPath = document.getElementById("i" + id).src;

                if (iPath.substr(iPath.length - 8, 8).toLowerCase() == "plus.jpg")
                    c.src = "../Images/minus.jpg";
                if (iPath.substr(iPath.length - 9, 9).toLowerCase() == "minus.jpg")
                    c.src = "../Images/plus.jpg";
            }
            rids = eval("a" + id).split(",");
            for (ic = 0; ic < rids.length; ic++) {
                if (rids[ic] != "") {
                    var rC = document.getElementById("ro" + rids[ic]);
                    if (rC.className == "gpnode") {
                        rC.className = "gitem";                        
                        rC.style.display = "none";
                    }
                    else if (rC.className == "gitem") {
                        rC.className = "gpnode";                                                
                        rC.style.display = "none";
                    }
                    if (c.src.indexOf("plus") > 0) {
                        rC.className = "gaitem"; 
                        rC.style.display = "none";
                        try {
                            rcids = eval("a" + rids[ic]).split(",");
                            for (ic1 = 0; ic1 < rcids.length; ic1++) {
                                var rpC = document.getElementById("ro" + rcids[ic1]);
                                rpC.className = "gaitem";                                
                                rpC.style.display = "none";
                            }
                        }
                        catch (e) { }

                    }
                    else if (c.src.indexOf("minus") > 0) {
                        rC.className = "gaitem";                       
                        rC.style.display = "table-row";
                    }
                }

            }
        }
    }
    else if (type == "CP") {
        var d = eval("a" + id);
        if (d) {
            //var c = document.all.item("i" + id);
            var c = document.getElementById("i" + id);
            if (c) {
                var iPath = document.getElementById("i" + id).src;

                if (iPath.substr(iPath.length - 8, 8).toLowerCase() == "plus.jpg")
                    c.src = "../Images/minus.jpg";
                if (iPath.substr(iPath.length - 9, 9).toLowerCase() == "minus.jpg")
                    c.src = "../Images/plus.jpg";
            }
            rids = eval("a" + id).split(",");
            for (ic = 0; ic < rids.length - 1; ic++) {
                if (rids[ic] != "") {
                    var rC = document.getElementById("ro" + rids[ic]);
                    if (rC.className == "gaitem" || c.src.indexOf("plus") > 0) {
                        rC.className = "gitem";                        
                        rC.style.display = "none";
                    }
                    else if (rC.className == "gitem" || c.src.indexOf("minus") > 0) {
                        rC.className = "gaitem";
                        rC.style.display = "table-row";
                    }
                    else if (rC.className == "gaitem" || c.src.indexOf("minus") > 0) {
                        rC.className = "gaitem";
                        rC.style.display = "table-row";
                    }
                }
            }
        }
    }
    else {
        if (previd != "") { document.getElementById(previd).style.backgroundColor = "" }
        document.getElementById("a" + id).style.backgroundColor = "#dcdcdc";
        previd = "a" + id;
    }
}



//fnCN('CPN3_PN3', '');
//fnCN('CN23_CPN3_PN3', '');
function fnCN(rowKey, pids) {
    var event = window.event || arguments.callee.caller.arguments[0];
    var obj = event.srcElement || event.target;
    if (obj.tagName != "IMG") {
        var keys = rowKey.split("_");
        var keyCount = keys.length;
        var controlCheckBox;
        var controlStatus;
        if (keyCount == 3) {
            var parentRowKey = rowKey.split("_")[1] + "_" + rowKey.split("_")[2]

            controlCheckBox = "_id" + rowKey + "_a"
            controlStatus = !el("_id" + rowKey + "_v").checked;
            tickControl(controlCheckBox, rowKey, "a", controlStatus);

            controlCheckBox = "_id" + rowKey + "_e";
            tickControl(controlCheckBox, rowKey, "e", controlStatus);

            controlCheckBox = "_id" + rowKey + "_v";
            tickControl(controlCheckBox, rowKey, "v", controlStatus);

            //Cancel
            controlCheckBox = "_id" + rowKey + "_c";
            tickControl(controlCheckBox, rowKey, "c", controlStatus);

            if (controlStatus == false) {
                var parentRowKeys = eval("a" + parentRowKey).split(",");
                var maxUnCheckedCount = 0;
                var controlStatus = true;
                for (i = 0; i < parentRowKeys.length - 1; i++) {
                    controlCheckBox = "_id" + parentRowKeys[i] + "_" + "a";
                    if (!el(controlCheckBox).checked) {
                        maxUnCheckedCount++;
                    }
                }
                controlCheckBox = "_id" + parentRowKey + "_a";

                if (maxUnCheckedCount == parentRowKeys.length - 1) {
                    tickParentControl(controlCheckBox, parentRowKey, "a", false);
                }

                maxUnCheckedCount = 0;

                for (i = 0; i < parentRowKeys.length - 1; i++) {
                    controlCheckBox = "_id" + parentRowKeys[i] + "_" + "e";
                    if (!el(controlCheckBox).checked) {
                        maxUnCheckedCount++;
                    }
                }
                controlCheckBox = "_id" + parentRowKey + "_e";

                if (maxUnCheckedCount == parentRowKeys.length - 1) {
                    tickParentControl(controlCheckBox, parentRowKey, "e", false);
                }

                maxUnCheckedCount = 0;

                for (i = 0; i < parentRowKeys.length - 1; i++) {
                    controlCheckBox = "_id" + parentRowKeys[i] + "_" + "v";
                    if (!el(controlCheckBox).checked) {
                        maxUnCheckedCount++;
                    }
                }
                controlCheckBox = "_id" + parentRowKey + "_v";

                if (maxUnCheckedCount == parentRowKeys.length - 1) {
                    tickParentControl(controlCheckBox, parentRowKey, "v", false);
                }

                //Cancel
                for (i = 0; i < parentRowKeys.length - 1; i++) {
                    controlCheckBox = "_id" + parentRowKeys[i] + "_" + "c";
                    if (!el(controlCheckBox).checked) {
                        maxUnCheckedCount++;
                    }
                }
                controlCheckBox = "_id" + parentRowKey + "_c";

                if (maxUnCheckedCount == parentRowKeys.length - 1) {
                    tickParentControl(controlCheckBox, parentRowKey, "c", false);
                }

            }
            else if (controlStatus == true) {
                controlCheckBox = "_id" + parentRowKey + "_a"
                tickParentControl(controlCheckBox, parentRowKey, "a", controlStatus);

                controlCheckBox = "_id" + parentRowKey + "_e"
                tickParentControl(controlCheckBox, parentRowKey, "e", controlStatus);

                controlCheckBox = "_id" + parentRowKey + "_v"
                tickParentControl(controlCheckBox, parentRowKey, "v", controlStatus);

                //Cancel
                controlCheckBox = "_id" + parentRowKey + "_c"
                tickParentControl(controlCheckBox, parentRowKey, "c", controlStatus);
            }

        }
        else if (keyCount == 2) {

            controlCheckBox = "_id" + rowKey + "_a"
            controlStatus = !el("_id" + rowKey + "_v").checked;
            tickParentControl(controlCheckBox, rowKey, "a", controlStatus);
            fnCAN(el(controlCheckBox), "a", rowKey);

            controlCheckBox = "_id" + rowKey + "_e";
            tickParentControl(controlCheckBox, rowKey, "e", controlStatus)
            fnCAN(el(controlCheckBox), "e", rowKey);

            controlCheckBox = "_id" + rowKey + "_v";
            tickParentControl(controlCheckBox, rowKey, "v", controlStatus);
            fnCAN(el(controlCheckBox), "v", rowKey, true);

            //Cancel

            controlCheckBox = "_id" + rowKey + "_c";
            tickParentControl(controlCheckBox, rowKey, "c", controlStatus);
            fnCAN(el(controlCheckBox), "c", rowKey, true);
        }
        CCHasChanges = true;
        HasChanges = true;
    }

    el("hdnModify").value = HasChanges;
}


//ex:fnCAN('a','CPN2_PN2');
function fnCAN(objCheckBox, controlType, controlKey, overRideView) {
    var controlKeys = eval("a" + controlKey).split(",");
    var maxCheckedCount = 0;
    var maxUnCheckedCount = 0;
    var controlStatus = objCheckBox.checked;

    if (typeof (overRideView) == "undefined" && controlType == "v" && controlStatus == false) {
        for (i = 0; i < controlKeys.length; i++) {
            var parentControlKeys = eval("a" + controlKey).split(",");
            var maxUnCheckedCount = 0;
            var controlStatus = true;
            var controlCheckBox;
            for (i = 0; i < parentControlKeys.length - 1; i++) {
                controlCheckBox = "_id" + parentControlKeys[i] + "_" + controlType;
                if (!el(controlCheckBox).checked) {
                    maxUnCheckedCount++;
                }
            }

            if (maxUnCheckedCount != parentControlKeys.length - 1) {
                objCheckBox.checked = true;
                return;
            }
        }
    }

    for (i = 0; i < controlKeys.length; i++) {
        if (controlKeys[i] != "") {
            var controlCheckBox = "_id" + controlKeys[i] + "_" + controlType;
            switch (controlType) {
                case "a":
                    tickControl(controlCheckBox, controlKeys[i], controlType, controlStatus);
                    if (controlStatus == true) {
                        var controlCheckBox = "_id" + controlKey + "_v";
                        tickParentControl(controlCheckBox, controlKey, "v", true);
                    }
                    break;
                case "e":
                    tickControl(controlCheckBox, controlKeys[i], controlType, controlStatus);
                    if (controlStatus == true) {
                        var controlCheckBox = "_id" + controlKey + "_v";
                        tickParentControl(controlCheckBox, controlKey, "v", true);
                    }
                    break;
                case "v":
                    tickControl(controlCheckBox, controlKeys[i], controlType, controlStatus);
                    break;
                case "c":    //Cancel
//                    tickControl(controlCheckBox, controlKeys[i], controlType, controlStatus);
                    //                    break;
                    tickControl(controlCheckBox, controlKeys[i], controlType, controlStatus);
                    if (controlStatus == true) {
                        var controlCheckBox = "_id" + controlKey + "_v";
                        tickParentControl(controlCheckBox, controlKey, "v", true);
                    }
                    break;
            }
        }
    }

    if (overRideView == true && controlType == "v" && controlStatus == false) {
        for (i = 0; i < controlKeys.length; i++) {
            var parentControlKeys = eval("a" + controlKey).split(",");
            var maxUnCheckedCount = 0;
            var controlStatus = true;
            var controlCheckBox;
            for (i = 0; i < parentControlKeys.length - 1; i++) {
                controlCheckBox = "_id" + parentControlKeys[i] + "_" + controlType;
                if (!el(controlCheckBox).checked) {
                    maxUnCheckedCount++;
                }
            }

            if (maxUnCheckedCount != parentControlKeys.length - 1) {
                objCheckBox.checked = true;
                return;
            }
        }
    }

    HasChanges = true;
    el("hdnModify").value = HasChanges;
}

//Check Parent checkbox
//ex: fnCP('_idCN5_CPN4_PN4_a','CN5_CPN4_PN4');
function fnCP(objCheckBox, controlType, controlKey) {
    var parentControlKey = controlKey.split("_")[1] + "_" + controlKey.split("_")[2]
    switch (controlType) {
        case "a":
            controlCheckBox = "_id" + controlKey + "_a"
            tickControl(controlCheckBox, controlKey, "a", objCheckBox.checked);
            if (objCheckBox.checked == false) {
                var parentControlKeys = eval("a" + parentControlKey).split(",");
                var maxUnCheckedCount = 0;
                var controlStatus = true;
                for (i = 0; i < parentControlKeys.length - 1; i++) {
                    controlCheckBox = "_id" + parentControlKeys[i] + "_" + controlType;
                    if (!el(controlCheckBox).checked) {
                        maxUnCheckedCount++;
                    }
                }
                controlCheckBox = "_id" + parentControlKey + "_a";

                if (maxUnCheckedCount == parentControlKeys.length - 1) {
                    tickParentControl(controlCheckBox, parentControlKey, "a", false);
                }

            }
            else if (objCheckBox.checked == true) {
                controlCheckBox = "_id" + parentControlKey + "_a";
                tickParentControl(controlCheckBox, parentControlKey, "a", true);

            }
            break;

        case "e":
            controlCheckBox = "_id" + controlKey + "_e";
            tickControl(controlCheckBox, controlKey, "e", objCheckBox.checked);
            if (objCheckBox.checked == false) {
                var parentControlKeys = eval("a" + parentControlKey).split(",");
                var maxUnCheckedCount = 0;
                var controlStatus = true;
                for (i = 0; i < parentControlKeys.length - 1; i++) {
                    controlCheckBox = "_id" + parentControlKeys[i] + "_" + controlType;
                    if (!el(controlCheckBox).checked) {
                        maxUnCheckedCount++;
                    }
                }
                controlCheckBox = "_id" + parentControlKey + "_e";

                if (maxUnCheckedCount == parentControlKeys.length - 1) {
                    tickParentControl(controlCheckBox, parentControlKey, "e", false);
                }

            }
            else if (objCheckBox.checked == true) {
                controlCheckBox = "_id" + parentControlKey + "_e";
                tickParentControl(controlCheckBox, parentControlKey, "e", true);

            }
            break;

        case "v":
            controlCheckBox = "_id" + controlKey + "_v"
            tickControl(controlCheckBox, controlKey, "v", objCheckBox.checked);
            if (objCheckBox.checked == false) {
                var parentControlKeys = eval("a" + parentControlKey).split(",");
                var maxUnCheckedCount = 0;
                var controlStatus = true;
                for (i = 0; i < parentControlKeys.length - 1; i++) {
                    controlCheckBox = "_id" + parentControlKeys[i] + "_" + controlType;
                    if (!el(controlCheckBox).checked) {
                        maxUnCheckedCount++;
                    }
                }
                controlCheckBox = "_id" + parentControlKey + "_v";

                if (maxUnCheckedCount == parentControlKeys.length - 1) {
                    tickParentControl(controlCheckBox, parentControlKey, "v", false);
                }

            }
            else if (objCheckBox.checked == true) {
                controlCheckBox = "_id" + parentControlKey + "_v";
                tickParentControl(controlCheckBox, parentControlKey, "v", true);

            }
            break;
//Cancel
        case "v":
            controlCheckBox = "_id" + controlKey + "_c";
            tickControl(controlCheckBox, controlKey, "c", objCheckBox.checked);
            if (objCheckBox.checked == false) {
                var parentControlKeys = eval("a" + parentControlKey).split(",");
                var maxUnCheckedCount = 0;
                var controlStatus = true;
                for (i = 0; i < parentControlKeys.length - 1; i++) {
                    controlCheckBox = "_id" + parentControlKeys[i] + "_" + controlType;
                    if (!el(controlCheckBox).checked) {
                        maxUnCheckedCount++;
                    }
                }
                controlCheckBox = "_id" + parentControlKey + "_c";

                if (maxUnCheckedCount == parentControlKeys.length - 1) {
                    tickParentControl(controlCheckBox, parentControlKey, "c", false);
                }

            }
            else if (objCheckBox.checked == true) {
                controlCheckBox = "_id" + parentControlKey + "_c";
                tickParentControl(controlCheckBox, parentControlKey, "c", true);

            }
            break;
    }

    HasChanges = true;
    el("hdnModify").value = HasChanges;
}

//this method used to tick the controls
function tickControl(controlCheckBox, controlKey, controlType, controlStatus) {
    if (controlStatus == true) {
        if (controlType == "v") {
            check(controlCheckBox);
        }
        else {
            check(controlCheckBox);
            check("_id" + controlKey + "_v");
        }
       
    }
    else if (controlStatus == false) {
        if (controlType == "v") {
            if (el("_id" + controlKey + "_a").checked == false && el("_id" + controlKey + "_e").checked == false)
                uncheck(controlCheckBox);
            else
                check(controlCheckBox);
        }
        else {
            uncheck(controlCheckBox);
        }      
    }
}

//this method used to tick the parent controls
function tickParentControl(controlCheckBox, controlKey, controlType, controlStatus) {
    if (controlStatus == true) {
        if (controlType == "v") {
            check(controlCheckBox);
        }
        else {
            check(controlCheckBox);
            check("_id" + controlKey + "_v");
        }
       
    }
    else if (controlStatus == false) {
        if (controlType == "v") {
            if (el("_id" + controlKey + "_a").checked == false && el("_id" + controlKey + "_e").checked == false)
                uncheck(controlCheckBox);
        }
        else {
            uncheck(controlCheckBox);
        }       
    }
}


//ex:fnHAN('CPN1_PN1,CPN2_PN2,CPN3_PN3,CPN4_PN4,CPN5_PN5,CPN9_PN9,CPN10_PN10,');
//Hide all child nodes.
function fnHAN(hideallm) {
    var allids = hideallm.split(",");

    //PN1,CPN2_PN1,CPN3_PN1
    //aPN1 = CPN2_PN1,CPN3_PN1
    for (i = 0; i < allids.length - 1; i++) {
        var chkId;
        chkId = allids[i];
        var vCPN = chkId.substr(0, 2);
        if (vCPN == "CP") {
            fnNC(allids[i], "CP");
        }
        else if (vCPN == "PN") {
            var ChildID = "";
            for (j = 0; j < allids.length - 1; j++) {
                var len = chkId.length;
                if (allids[j].substr(allids[j].length - len, len) == chkId && allids[j] != chkId) {
                    if (ChildID != "")
                        ChildID = ChildID + ",";
                    ChildID = ChildID + allids[j];
                }
            }
            if (self["a" + allids[i]] == "") {
                self["a" + allids[i]] = ChildID;
            }
            fnNC(allids[i], "P");
        }
    }
}

//Select all checkbox by click the header row cell.
function fnSA() {
    var doc = document.forms[0];
    var event = window.event || arguments.callee.caller.arguments[0];    
    var srcEl = event.srcElement || event.target;

    if ((srcEl.tagName == "TD") && (srcEl.id == "tdAdd") && (chkAdd == false)) {
        for (i = 0; i < doc.elements.length; i++) {
            var chkId = doc.elements[i].id;
            if (doc.elements[i].type == "checkbox") {
                var chkStr = fnCTp(chkId);
                if (chkStr == "a" || chkStr == "v") {
                    check(doc.elements[i].id);
                    chkAdd = true;
                }
            }

        }

    }
    else if ((srcEl.tagName == "TD") && (srcEl.id == "tdAdd") && (chkAdd == true)) {
        for (i = 0; i < doc.elements.length; i++) {
            var chkId = doc.elements[i].id;
            if (doc.elements[i].type == "checkbox") {
                var chkStr = fnCTp(chkId);
                var ChkEl = chkId.substr(0, chkId.length - 1)
                var PkRole = chkId.substr(3, 2);
                if (chkStr == "a") {
                    uncheck(doc.elements[i].id); ;
                    chkAdd = false;
                }
            }
        }
    }

    if ((srcEl.tagName == "TD") && (srcEl.id == "tdEdit") && (chkEdit == false)) {
        for (i = 0; i < doc.elements.length; i++) {
            var chkId = doc.elements[i].id;
            if (doc.elements[i].type == "checkbox") {
                var chkStr = chkId.substr((chkId.length - 1), 1)
                if (chkStr == "e" || chkStr == "v") {
                    check(doc.elements[i].id);
                    chkEdit = true;
                }
            }
        }

    }
    else if ((srcEl.tagName == "TD") && (srcEl.id == "tdEdit") && (chkEdit == true)) {
        for (i = 0; i < doc.elements.length; i++) {
            var chkId = doc.elements[i].id;
            if (doc.elements[i].type == "checkbox") {
                var chkStr = fnCTp(chkId);
                var ChkEl = chkId.substr(0, chkId.length - 1);
                var PkRole = chkId.substr(3, 2);
                if (chkStr == "e") {
                    uncheck(doc.elements[i].id); ;
                    chkEdit = false;
                }
            }
        }

    }
    if ((srcEl.tagName == "TD") && (srcEl.id == "tdView") && (chkView == false)) {
        for (i = 0; i < doc.elements.length; i++) {
            var chkId = doc.elements[i].id;
            if (doc.elements[i].type == "checkbox") {
                var chkStr = fnCTp(chkId);
                if (chkStr == "v") {
                    check(doc.elements[i].id);
                    chkView = true;
                }
            }
        }
    }
    else if ((srcEl.tagName == "TD") && (srcEl.id == "tdView") && (chkView == true)) {
        for (i = 0; i < doc.elements.length; i++) {
            var chkId = doc.elements[i].id;
            var chkStr = chkId.substr((chkId.length - 1), 1);
            var ChkEl = chkId.substr(0, chkId.length - 1);
            if (doc.elements[i].type == "checkbox") {
                if (chkStr == "v" && el(ChkEl + "a").checked == false &&
		            el(ChkEl + "e").checked == false)
                    uncheck(doc.elements[i].id); ;
                chkView = false;
            }
        }
    }

    //Cancel

    if ((srcEl.tagName == "TD") && (srcEl.id == "tdCancel") && (chkCancel == false)) {
        for (i = 0; i < doc.elements.length; i++) {
            var chkId = doc.elements[i].id;
            if (doc.elements[i].type == "checkbox") {
                var chkStr = chkId.substr((chkId.length - 1), 1)
                if (chkStr == "c" || chkStr == "v") {
                    check(doc.elements[i].id);
                    chkCancel = true;
                }
            }
        }

    }
    else if ((srcEl.tagName == "TD") && (srcEl.id == "tdCancel") && (chkCancel == true)) {
        for (i = 0; i < doc.elements.length; i++) {
            var chkId = doc.elements[i].id;
            if (doc.elements[i].type == "checkbox") {
                var chkStr = fnCTp(chkId);
                var ChkEl = chkId.substr(0, chkId.length - 1);
                var PkRole = chkId.substr(3, 2);
                if (chkStr == "c") {
                    uncheck(doc.elements[i].id); ;
                    chkCancel = false;
                }
            }
        }

    }

    HasChanges = true;
    el("hdnModify").value = HasChanges;
}


//get 'e' from '_idPN2_e' or '_idCN32_PN2_e'
function fnCTp(chkId) {
    return chkId.substr((chkId.length - 1), 1);
}


//Show Info Message
function fnSIM(_msg) {
    ShowInfoMessage(_msg);
}
//Show Error Message
function fnSEM(_msg) {
    ShowErrorMessage(_msg);
}

function uncheck(chkId) {
    if (el(chkId).disabled == false)
        el(chkId).checked = false;
}

function check(chkId) {
    if (el(chkId).disabled == false)
        el(chkId).checked = true;
}

//Roleright.aspx.vb - 858
//treeview.ascx.vb 351
//treeview.js - 292, 613
