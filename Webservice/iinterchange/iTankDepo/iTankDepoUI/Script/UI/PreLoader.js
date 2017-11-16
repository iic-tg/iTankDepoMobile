

var imagenames = ['../images/play_guitar_md.gif', '../images/meditation_tutor_md.gif', '../images/print_this_article.gif', '../images/print_this_page.gif', '../images/sm_logo.gif', '../images/down.gif'];

var loadedcolor = 'white';         // PROGRESS BAR COLOR
var unloadedcolor = '#FDC558';      // BGCOLOR OF UNLOADED AREA
var barheight = 0;                 // HEIGHT OF PROGRESS BAR IN PIXELS
var barwidth = 100;                 // WIDTH OF THE BAR IN PIXELS
var bordercolor = '#910029';        // COLOR OF THE BORDER
var textColor = 'black';            // COLOR OF TEXT IN LOADING BAR
var textSize = '10px';              // SIZE OF TEXT IN LOADING BAR
var textFont = 'verdana';           // FONT FAMILY OF TEXT IN LOADING BAR
var loadonce = false;                // IF THIS VALUE IS true THE BAR WILL NOT DISPLAY IF THE USER

var action = function() {
    hidebar();
}

// THE FUNCTION BELOW CONTAINS THE ACTION(S) TO TAKE PLACE IF THE USER
// CLICKS THE PROGRESSBAR. THIS CAN BE USED TO CANCEL THE PROGRESSBAR.
// IF YOU WISH NOTHING TO HAPPEN, SIMPLY REMOVE EVERYTHING BETWEEN THE CURLY BRACES.
// NOTE: EVEN THOUGH THE BAR IS HIDDEN, THE IMAGES ARE STILL BEING PRELOADED, UNLESS
// YOU MADE THE SCRIPT REDIRECT TO A DIFFERENT PAGE.

var clickBar = function() {
    hidebar();
    //alert('Progressbar cancelled.');
}

//*****************************************************//
//********  NO NEED TO EDIT BEYOND THIS POINT  ********//
//*****************************************************//

var NS4 = (document.layers) ? true : false;
var IE4 = (document.all) ? true : false;
var blocksize = (barwidth - 2) / (imagenames.length);
barheight = Math.max(barheight, 4);
var loaded = 0;
var perouter = null;
var perdone = null;
var images = new Array();
var st = 0;
var hidethebar = false;
var txt = '';
if (NS4) {
    txt += '<div id="perouteroverlay" class="opaqueLayer">'
    txt += '</div>'
    txt += '<div id="perouter" class="PreLoadLayer">'
    txt += '   <table id="tblmodalwindow1" >'
    txt += '       <tr>'
    txt += '           <td id="tdinfomodalwindowtitleimage1" style="width: 40px;">'
    txt += '               <input id="imgloading1" type="image" class="loadingimg" src="Images/ajloader.gif" />'
    txt += '           </td>'
    txt += '          <td id="tdmodalwindowtitle1" class="lmodalwindowtitle" style="width: 110px;">'
    txt += '<table cellspacing=0 cellpadding=0 border=0><tr><td>';
    txt += '<ilayer name="perouter" visibility="hide" height="' + barheight + '" width="' + barwidth + '">';
    txt += '<layer width="' + barwidth + '" height="' + barheight + '" bgcolor="' + bordercolor + '" top="0" left="0"></layer>';
    txt += '<layer width="' + (barwidth - 2) + '" height="' + (barheight - 2) + '" top="1" left="1"></layer>';
    txt += '<layer name="perdone" width="' + (barwidth - 2) + '" height="' + (barheight - 2) + '" bgcolor="' + loadedcolor + '" top="1" left="1"></layer>';
    txt += '<layer width="' + (barwidth - 2) + '" height="' + (barheight - 2) + '" top="1" left="1">';
    txt += '<table cellpadding=0 cellspacing=0 border=0 width="' + (barwidth - 2) + '" height="' + (barheight - 2) + '"><tr><td align="center" valign="middle">';
    txt += '<span style="color:' + textColor + '; font-size:' + textSize + '; font-family:' + textFont + '">Please wait...</span>';
    txt += '</td></tr></table>';
    txt += '</layer>';
    txt += '</ilayer>';
    txt += '</td></tr></table>'
    txt += '          </td>'
    txt += '       </tr>'
    txt += '   </table>'
    txt += '</div>'
} else {
    txt += '<div id="perouteroverlay" class="opaqueLayer">'
    txt += '</div>'
    txt += '<div id="perouter" class="PreLoadLayer">'
    txt += '   <table id="tblmodalwindow1">'
    txt += '       <tr>'
    txt += '           <td id="tdinfomodalwindowtitleimage1" style="width: 40px;">'
    txt += '               <input id="imgloading1" type="image" class="loadingimg" src="Images/ajloader.gif" />'
    txt += '           </td>'
    txt += '          <td id="tdmodalwindowtitle1" class="lmodalwindowtitle" style="width: 110px;">'
    txt += '            <div style="position:relative; width:' + barwidth + 'px; height:' + barheight + 'px;">';
    txt += '            <div style="width:' + (barwidth - 2) + 'px; height:' + (barheight - 2) + 'px; font-size:1px;"></div>';
    txt += '            <div id="perdone" style="width:0px; height:' + (barheight - 2) + 'px; background-color:' + loadedcolor + '; font-size:1px;"></div>';
    txt += '            <div style="width:' + (barwidth - 2) + 'px; height:' + (barheight - 2) + 'px; color:' + textColor + '; font-size:' + textSize + '; font-family:' + textFont + '; text-align:center; cursor:default">Please wait...</div>';
    txt += '            </div>'
    txt += '          </td>'
    txt += '       </tr>'
    txt += '   </table>'
    txt += '</div>'
}
document.write(txt);
setProgressPosition();
function setProgressPosition() {
    var shadow;
    var question;

    if (document.getElementById("perouteroverlay") == null)
        shadow = document.getElementById("perouteroverlay");
    else
        shadow = document.getElementById("perouteroverlay");

    if (document.getElementById("perouter") == null)
        question = document.getElementById("perouter");
    else
        question = document.getElementById("perouter");


    var bws = getBrowserHeight();
    shadow.style.width = bws.width + "px";
    shadow.style.height = bws.height + "px";

    question.style.left = parseInt((bws.width - 230) / 2);
    question.style.top = parseInt((bws.height - 150) / 2);

    shadow = null;
    question = null;
}


function getBrowserHeight() {
    var intH = 0;
    var intW = 0;

    if (typeof window.innerWidth == 'number') {
        intH = window.innerHeight;
        intW = window.innerWidth;
    }
    else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        intH = document.documentElement.clientHeight;
        intW = document.documentElement.clientWidth;
    }
    else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        intH = document.body.clientHeight;
        intW = document.body.clientWidth;
    }

    return { width: parseInt(intW), height: parseInt(intH) };
}

function findlayer(name, doc) {
    var i, layer;
    for (i = 0; i < doc.layers.length; i++) {
        layer = doc.layers[i];
        if (layer.name == name) return layer;
        if (layer.document.layers.length > 0)
            if ((layer = findlayer(name, layer.document)) != null)
            return layer;
    }
    return null;
}

function loadimages() {
    perouter = (NS4) ? findlayer('perouter', document) : (IE4) ? document.all['perouter'] : document.getElementById('perouter');
    perdone = (NS4) ? perouter.document.layers['perdone'] : (IE4) ? document.all['perdone'] : document.getElementById('perdone');
    if (NS4) perouter.captureEvents(Event.MOUSEUP);
    perouter.onmouseup = function() {
        hidethebar = true;
        clearTimeout(st);
        clickBar();
    }
    clipEl(100, 0, 0, barheight - 2, 0);
    if (NS4) perouter.visibility = "show";
    else perouter.style.visibility = "visible";
    for (n = 0; n < imagenames.length; n++) {
        images[n] = new Image();
        images[n].src = imagenames[n];
        if (images[n].complete) dispbars();
        images[n].onload = dispbars;
        images[n].onerror = dispbars;
    } 
}

function dispbars() {
    loaded++;
    clipEl(perdone, 0, blocksize * loaded, barheight - 2, 0);
    if (loaded >= imagenames.length && !hidethebar) st = setTimeout('action()', 800);
}

function hidebar() {
    if (NS4) perouter.visibility = "hide";
    else perouter.style.visibility = "hidden";
}

function clipEl(el, ct, cr, cb, cl) {
//    if (NS4) {
//        el.clip.left = cl;
//        el.clip.top = ct;
//        el.clip.right = cr;
//        el.clip.bottom = cb;
//    } else el.style.width = cr + 'px';
}

//CREDIS TO DYNAMICDRIVE FOR THE FUNCTION BELOW
function get_cookie(Name) {
    var search = Name + "=";
    var returnvalue = "";
    if (document.cookie.length > 0) {
        offset = document.cookie.indexOf(search);
        if (offset != -1) {
            offset += search.length;
            end = document.cookie.indexOf(";", offset);
            if (end == -1) end = document.cookie.length;
            returnvalue = unescape(document.cookie.substring(offset, end));
        } 
    }
    return returnvalue;
}

window.onload = function() {
    var okPB = false;
    if (loadonce) {
        if (get_cookie("progB") == "") {
            okPB = true;
            document.cookie = "progB=yes";
        } 
    }
    else okPB = true;
    if (okPB) setTimeout('loadimages()', 300);
}


