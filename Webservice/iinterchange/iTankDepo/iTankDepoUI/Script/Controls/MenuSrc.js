var tempfrmnm;
var tmpbool;

tmpbool = true;
_mD = 2;
_d = document;
_n = navigator;
_nv = $tL(_n.appVersion);
_nu = $tL(_n.userAgent);
_ps = parseInt(_n.productSub);

_f = false;
_t = true;
_n = null;

_wp = window.createPopup;

ie = (_d.all) ? _t: _f;
ie4 = (!_d.getElementById && ie) ? _t: _f;
ie5 = (!ie4 && ie && !_wp) ? _t: _f;
ie55 = (!ie4 && ie && _wp) ? _t: _f;
ns6 = (_nu.indexOf("gecko") != -1) ? _t: _f;
konq = (_nu.indexOf("konqueror") != -1) ? _t: _f;
sfri = (_nu.indexOf("safari") != -1) ? _t: _f;

if (konq || sfri) {
    _ps = 0;
    ns6 = 0
}
ns4 = (_d.layers) ? _t: _f;
ns61 = (_ps >= 20010726) ? _t: _f;
ns7 = (_ps >= 20020823) ? _t: _f;
op = (window.opera) ? _t: _f;

if (op || konq)
    ie = 0;

op5 = (_nu.indexOf("opera 5") != -1) ? _t: _f;
op6 = (_nu.indexOf("opera 6") != -1) ? _t: _f;
op7 = (_nu.indexOf("opera 7") != -1 || _nu.indexOf("opera/7") != -1) ? _t: _f;
mac = (_nv.indexOf("mac") != -1) ? _t: _f;
mac45 = (_nv.indexOf("msie 4.5") != -1) ? _t: _f;

if (ns6 || ns4 || op || sfri)
    mac = _f;

ns60 = _f;

if (ns6 && !ns61)
    ns60 = _t;

IEDtD = 0;

if (!op && (_d.all && _d.compatMode == "CSS1Compat") || (mac && _d.doctype && _d.doctype.name.indexOf(".dtd") != -1))
    IEDtD = 1;
if (op7)
    op = _f;

if (op)
    ie55 = _f;

_st = 0;
_en = 0;
$ = " ";

_m = new Array();
_mi = new Array();
_sm = new Array();
_tsm = new Array();
_cip = new Array();

_mn = -1;
_el = 0;
_ael = 0;
_Bel = 0;
_bl = 0;
_Omenu = 0;

_MT = setTimeout("", 0);
_oMT = setTimeout("", 0);
_cMT = setTimeout("", 0);
_scrmt = setTimeout("", 0);
_mst = setTimeout("", 0);
_Mtip = setTimeout("", 0);

_zi = 999;
_c = 1;
_mt = "";
_oldel = -1;
_sH = 0;
_sW = 0;
_bH = 500;
_oldbH = 0;
_bW = 0;
_oldbW = 0;
_cD = 0;
_ofMT = 0;
_startM = 1;
_sT = 0;
_sL = 0;
_mcnt = 0;
_mnuD = 0;
_itemRef = -1;
inopenmode = 0;

function M_hideLayer() {}

function opentree() {}

function chop(_ar, _pos) {
    var _tar = new Array();

    for (_a = 0; _a < _ar.length; _a++) {
        if (_a != _pos) {
            _tar[_tar.length] = _ar[_a]
            }
    }
    return _tar
}

function remove(_ar, _dta) {
    var _tar = new Array();
    for (_a = 0; _a < _ar.length; _a++) {
        if (_ar[_a] != _dta) {
            _tar[_tar.length] = _ar[_a]
            }
    }
    return _tar
}
function copyOf(_w) {
    for (_cO in _w) {
        this[_cO] = _w[_cO]
        }
}
function $tL($S) {
    return $S.toLowerCase()
    }
function drawMenus() {
    for (_a = _mcnt; _a < _m.length; _a++) {
        _drawMenu(_a, 1)
        }
}

_$S = {
    menu: 0,
    text: 1,
    url: 2,
    showmenu: 3,
    status: 4,
    onbgcolor: 5,
    oncolor: 6,
    offbgcolor: 7,
    offcolor: 8,
    offborder: 9,
    separatorcolor: 10,
    padding: 11,
    fontsize: 12,
    fontstyle: 13,
    fontweight: 14,
    fontfamily: 15,
    high3dcolor: 16,
    low3dcolor: 17,
    pagecolor: 18,
    pagebgcolor: 19,
    headercolor: 20,
    headerbgcolor: 21,
    subimagepadding: 22,
    subimageposition: 23,
    subimage: 24,
    onborder: 25,
    ondecoration: 26,
    separatorsize: 27,
    itemheight: 28,
    image: 29,
    imageposition: 30,
    imagealign: 31,
    overimage: 32,
    decoration: 33,
    type: 34,
    target: 35,
    align: 36,
    imageheight: 37,
    imagewidth: 38,
    openonclick: 39,
    closeonclick: 40,
    keepalive: 41,
    onfunction: 42,
    offfunction: 43,
    onbold: 44,
    onitalic: 45,
    bgimage: 46,
    overbgimage: 47,
    onsubimage: 48,
    separatorheight: 49,
    separatorwidth: 50,
    separatorpadding: 51,
    separatoralign: 52,
    onclass: 53,
    offclass: 54,
    itemwidth: 55,
    pageimage: 56,
    targetfeatures: 57,
    imagealt: 58,
    pointer: 59,
    imagepadding: 60,
    valign: 61,
    clickfunction: 62,
    bordercolor: 63,
    borderstyle: 64,
    borderwidth: 65,
    overfilter: 66,
    outfilter: 67,
    margin: 68,
    pagebgimage: 69,
    swap3d: 70,
    separatorimage: 71,
    pageclass: 72,
    menubgimage: 73,
    headerborder: 74,
    pageborder: 75
};

function mm_style() {
    for ($i in _$S)
        this[$i] = _n
}

_$M = {
    items: 0,
    name: 1,
    top: 2,
    left: 3,
    itemwidth: 4,
    screenposition: 5,
    style: 6,
    alwaysvisible: 7,
    align: 8,
    orientation: 9,
    keepalive: 10,
    openstyle: 11,
    margin: 12,
    overflow: 13,
    position: 14,
    overfilter: 15,
    outfilter: 16,
    menuwidth: 17,
    itemheight: 18,
    followscroll: 19,
    menualign: 20,
    mm_callItem: 21,
    mm_obj_ref: 22,
    mm_built: 23
};

function menuname(name) {
    for ($i in _$M)
        this[$i] = _n;
    this.name = $tL(name);
    _c = 1;
    _mn++;
    this.menunumber = _mn
}
function _incItem(_it) {
    _mi[_bl] = new Array();
    for ($i in _x[6])
        _mi[_bl][_$S[$i]] = _x[6][$i];
    _mi[_bl][0] = _mn;
    _it = _it.split(";");
    for (_a = 0; _a < _it.length; _a++) {
        _sp = _it[_a].indexOf("`");
        if (_sp != -1) {
            _tI = _it[_a];
            for (_b = _a; _b < _it.length; _b++) {
                _tI += ";" + _it[_b + 1];
                _a++;
                if (_it[_b + 1].indexOf("`") != -1)
                    _b = _it.length
            }
            _it[_a] = _tI.replace(/`/g, "")
            }
        _sp = _it[_a].indexOf("=");

        if (_sp == -1) {
            if (_it[_a])
                _si = _si + ";" + _it[_a]
            } else {
            _si = _it[_a].slice(_sp + 1);
            _w = _it[_a].slice(0, _sp);
            if (_w == "showmenu")
                _si = $tL(_si)
            }
        if (_it[_a]) {
            _mi[_bl][_$S[_w]] = _si
        }
    }
    _m[_mn][0][_c - 2] = _bl;
    _c++;
    _bl++;
    _mil = 1;
    if (_m[_mn][7] && _c == 3) {
        $c = 0;
        for ($i in _$S) {
            if ($c == 2)
                $T2 = ";" + $i;
            if ($c == 1)
                $T1 = $i + "=";
            $c++
        }
    }
    _mil = 2
}

function _iI(txt, _pos) {
    _oStyle = _m[_mn][6];
    _m[_mn][6] = this.style;
    this.aI(txt);
    _mil = _mi.length;
    _M = _m[this.menunumber];
    _nmi = new Array();
    if (_pos >= _M[0].length)
        _pos = _M[0].length;
    if (!_M[0][_pos])
        _M[0][_pos] = _M[0][_M[0].length - 1] + 1;
    _inum = _M[0][_pos];
    _cnt = 0;
    for (_a = 0; _a < _mil; _a++) {
        if (_inum == _a) {
            _nmi[_cnt] = _mi[_mi.length - 1];
            _nmi[_cnt][0] = this.menunumber;
            _M[0][_M[0].length] = _cnt;
            _cnt++
        }
        _nmi[_cnt] = _mi[_a];
        _cnt++
    }
    _mi = _nmi;
    _tpos = 0;
    _omnu = -1;
    for (_a = 0; _a < _mil; _a++) {
        _mnu = _mi[_a][0];
        if (_mnu != _omnu) {
            _m[_mnu][0] = new Array();
            _tpos = 0
        }
        _m[_mnu][0][_tpos] = _a;
        _tpos++;
        _omnu = _mnu
    }
    _m[_mn][6] = _oStyle
}
_c = 0;
function ami(txt) {
    _t = this;
    if (_c == 1) {
        _c++;
        _m[_mn] = new Array();
        _x = _m[_mn];
        for ($i in _t)
            _x[_$M[$i]] = _t[$i];
        _x[21] = -1;
        _x[0] = new Array();
        if (!_x[12])
            _x[12] = 0;
        _MS = _m[_mn][6];
        _MN = _m[_mn];
        if (!_MN[15])
            _MN[15] = _MS.overfilter;
        if (!_MN[16])
            _MN[16] = _MS.outfilter;
        if (!_MN[12])
            _MN[12] = _MS.margin;
        if (!_MS[65])
            _MS[65] = 0
    }
    _incItem(txt)
    }
menuname.prototype.aI = ami;
menuname.prototype.insertItem = _iI;
if (ns4) {
    _amt = "";
    _MTF = 0;
    _onTS = 0;
    var _cel = -1;
    function $CtI($ti) {
        clearTimeout($ti)
        }
    function gmobj(mtxt) {
        if (_d.layers[mtxt])
            return _d.layers[mtxt];
        re = /\d*\d/;
        fnd = re.exec(mtxt);
        if (_d.layers["menu" + _mi[fnd][0]]) {
            return _d.layers["menu" + _mi[fnd][0]].document.layers["il" + fnd].document.layers[mtxt]
            } else {
            return document.layers["il" + fnd].document.layers[mtxt]
            }
    }
    function spos(gm, t_, l_, h_, w_) {
        if (t_ != null)
            gm.top = t_;
        if (l_ != null)
            gm.left = l_;
        if (h_ != null)
            gm.height = h_;
        if (w_ != null)
            gm.width = w_
    }
    function gpos(gm) {
        var gpa = new Array();
        gpa[0] = gm.pageY;
        gpa[1] = gm.pageX;
        gpa[2] = gm.clip.height;
        gpa[3] = gm.clip.width;
        return (gpa)
        }
    function _lc(_dummy) {
        if (window.retainClickValue)
            inopenmode = 1;
        _i = nshl;
        if (_mi[_i][62])
            eval(_mi[_i][62]);
        if (_i > -1) {
            if (_mi[_i][2]) {
                location.href = _mi[_i][2]
                } else {
                if (_mi[_i][39] || _mi[_i][40]) {
                    _nullLink(_i)
                    }
            }
        }
    }
    function _nullLink(_i) {
        if (_mi[_i][3]) {
            _oldMC = _mi[_i][39];
            _mi[_i][39] = 0;
            _oldMD = _menuOpenDelay;
            _menuOpenDelay = 0;
            _gm = gmobj("menu" + getMenuByName(_mi[_i][3]));
            if (_gm.visibility == "show" && _mi[_i][40]) {
                menuDisplay(getMenuByName(_mi[_i][3]), 0);
                itemOn(_i)
                } else {
                _popi(_i)
                }
            _menuOpenDelay = _oldMD;
            _mi[_i][39] = _oldMC
        }
    }
    function itemOn(_i) {
        $CtI(_scrmt);
        if (_mi[_i][34] == "header" || _mi[_i][34] == "form")
            return;
        _gm = gmobj("oel" + _i);
        _gm.visibility = "show";
        if (_mi[_i][42])
            eval(_mi[_i][42])
        }
    function itemOff(_i) {
        if (_i > -1) {
            _gm = gmobj("oel" + _i);
            _gm.visibility = "hide";
            if (_mi[_i][43])
                eval(_mi[_i][43])
            }
    }
    _NS4S = new Array();
    function drawItem(_i) {
        _Tmt = "";
        _Dmnu = _mi[_i][0];
        var _M = _m[_Dmnu];
        var _mE = _mi[_i];
        if (!_NS4S[_i]) {
            if (!_mi[_i][33])
                _mi[_i][33] = "none";
            if (!_mi[_i][26])
                _mi[_i][26] = "none";
            if (!_mi[_i][14])
                _mi[_i][14] = "normal";
            _st = ".item" + _i + "{";
            if (_mi[_i][33])
                _st += "textDecoration:" + _mi[_i][33] + ";";
            if (_mi[_i][15])
                _st += "fontFamily:" + _mi[_i][15] + ";";
            if (_mi[_i][14])
                _st += "fontWeight:" + _mi[_i][14] + ";";
            if (_mi[_i][12])
                _st += "fontSize:" + _mi[_i][12] + ";";
            _st += "}";
            _st += ".oitem" + _i + "{";
            if (_mi[_i][15])
                _st += "fontFamily:" + _mi[_i][15] + ";";
            if (_mi[_i][14])
                _st += "fontWeight:" + _mi[_i][14] + ";";
            if (_mi[_i][33])
                _st += "textDecoration:" + _mi[_i][33] + ";";
            if (_mi[_i][44])
                _st += "fontWeight:bold;";
            if (_mi[_i][45])
                _st += "fontStyle:italic;";
            if (_mi[_i][12])
                _st += "fontSize:" + _mi[_i][12] + ";";
            if (_mi[_i][26])
                _st += "textDecoration:" + _mi[_i][26] + ";";
            _st += "}";
            _d.write("<style>" + _st + "</style>");
            _NS4S[_i] = _i
        }
        _lnk = "javascript:_nullLink(" + _i + ");";
        if (_mi[_i][2])
            _lnk = "javascript:_lc(" + _i + ")";
        _wid = "";
        if (_M[4])
            _wid = "width=" + _M[4];
        if (_mi[_i][55])
            _wid = "width=" + _mi[_i][55];
        _hgt = "";
        if (_M[18]) {
            _hgt = "height=" + _M[18]
            }
        if (_mi[_i][28]) {
            _hgt = "height=" + _mi[_i][28]
            }
        _pad = "0";
        if (_mE[11])
            _pad = _mE[11];
        if (_mi[_i][34] == "header") {
            if (_mi[_i][20])
                _mi[_i][8] = _mi[_i][20];
            if (_mi[_i][20])
                _mi[_i][7] = _mi[_i][21]
            }
        _bgc = "";
        if (_mi[_i][7] == "transparent")
            _mi[_i][7] = _n;
        if (_mi[_i][7])
            _bgc = "bgcolor=" + _mi[_i][7];
        _fgc = "";
        if (_mi[_i][8])
            _fgc = "<font color=" + _mi[_i][8] + ">";
        _bgbc = "";
        if (_mi[_i][5])
            _bgbc = "bgcolor=" + _mi[_i][5];
        _fgbc = "";
        if (_mi[_i][6])
            _fgbc = "<font color=" + _mi[_i][6] + ">";
        _algn = "";
        if (_M[8])
            _algn = " align=" + _M[8];
        if (_mi[_i][36])
            _algn = " align=" + _mi[_i][36];
        if (_mi[_i][61])
            _algn = " valign=" + _mi[_i][61];
        _nw = "";
        if (!_M[4] && !_mi[_i][55])
            _nw = " nowrap ";
        _iMS = "";
        _iME = "";
        if (_lnk) {
            _iMS = "<a href=\"" + _lnk + "\" onMouseOver=\"set_status(" + _i + ");return true\">";
            _iME = "</a>"
        }
        _Lsimg = "";
        _Rsimg = "";
        _LsimgO = "";
        _RsimgO = "";
        _itrs = "";
        _itre = "";
        if (_mi[_i][3] && _mi[_i][24]) {
            _subIR = 0;
            if (_M[11] == "rtl")
                _subIR = 1;
            _img = _iMS + "<img border=0 src=" + _mi[_i][24] + ">" + _iME;
            _oimg = _img;
            if (_mi[_i][48])
                _oimg = _iMS + "<img border=0 src=" + _mi[_i][48] + ">" + _iME;
            _simgP = "";
            if (_mi[_i][22])
                _simgP = _mi[_i][22];
            _imps = "";
            if (_mi[_i][23]) {
                _iA = "";
                _ivA = "";
                _imP = _mi[_i][23].split(" ");
                for (_ia = 0; _ia < _imP.length; _ia++) {
                    if (_imP[_ia] == "left")
                        _subIR = 1;
                    if (_imP[_ia] == "right")
                        _subIR = 0;
                    if (_imP[_ia] == "top" || _imP[_ia] == "bottom" || _imP[_ia] == "middle") {
                        _ivA = "valign=" + _imP[_ia];
                        if (_imP[_ia] == "top")
                            _subIR = 1;
                        if (_imP[_ia] == "bottom")
                            _subIR = 0
                    }
                    if (_imP[_ia] == "center") {
                        _itrs = "<tr>";
                        _itre = "</tr>";
                        _iA = "align=center"
                    }
                }
                _imps = _iA + " " + _ivA
            }
            _its = _itrs + "<td " + _imps + "><table border=0 cellspacing=" + _simgP + " cellpadding=0><td>";
            _ite = "</td></table></td>" + _itre;
            if (_subIR)
                _Lsimg = _its + _img + _ite;
            else
                _Rsimg = _its + _img + _ite;
            if (_subIR)
                _LsimgO = _its + _oimg + _ite;
            else
                _RsimgO = _its + _oimg + _ite
        }
        _Limg = "";
        _Rimg = "";
        _LimgO = "";
        _RimgO = "";
        if (_mi[_i][29]) {
            _iA = "";
            _ivA = "";
            _imps = "";
            _Iwid = "";
            if (_mi[_i][37])
                _Iwid = " width=" + _mi[_i][37];
            _Ihgt = "";
            if (_mi[_i][38])
                _Ihgt = " height=" + _mi[_i][38];
            _img = _iMS + "<img " + _Iwid + _Ihgt + " border=0 src=" + _mi[_i][29] + ">" + _iME;
            _oimg = _img;
            if (_mi[_i][32])
                _oimg = _iMS + "<img " + _Iwid + _Ihgt + " border=0 src=" + _mi[_i][32] + ">" + _iME;
            if (!_mi[_i][30])
                _mi[_i][30] = "left";
            _imP = _mi[_i][30].split(" ");
            for (_ia = 0; _ia < _imP.length; _ia++) {
                if (_imP[_ia] == "left")
                    _subIR = 1;
                if (_imP[_ia] == "right")
                    _subIR = 0;
                if (_imP[_ia] == "top" || _imP[_ia] == "bottom" || _imP[_ia] == "middle") {
                    _ivA = "valign=" + _imP[_ia];
                    if (_mi[_i][3])
                        _ivA += " colspan=2";
                    if (_imP[_ia] == "top")
                        _subIR = 1;
                    if (_imP[_ia] == "bottom")
                        _subIR = 0
                }
                if (_imP[_ia] == "center") {
                    _itrs = "<tr>";
                    _itre = "</tr>";
                    _iA = "align=center"
                }
            }
            _imps = _iA + " " + _ivA;
            _its = _itrs + "<td " + _imps + "><table border=0 cellspacing=0 cellpadding=0><tr><td>";
            _ite = "</td></tr></table></td>" + _itre;
            if (!_mi[_i][1]) {
                _its = "";
                _ite = ""
            }
            if (_subIR)
                _Limg = _its + _img + _ite;
            else
                _Rimg = _its + _img + _ite;
            if (_subIR)
                _LimgO = _its + _oimg + _ite;
            else
                _RimgO = _its + _oimg + _ite
        }
        if (!_M[9]) {
            _Tmt += "<tr>"
        }
        _Tmt += "<td  class=item" + _i + ">";
        _Tmt += "<ilayer id=il" + _i + ">";
        _txt = "";
        if (_mi[_i][1])
            _txt = _mi[_i][1];
        _acT = "onmouseover=\"_popi(" + _i + ");clearTimeout(_MTF);_MTF=setTimeout('close_el(" + _i + ")',200);\";drag_drop('menu" + _Dmnu + "');";
        if (_mi[_i][34] == "dragable") {}
        if (_mi[_i][34] == "header")
            _acT = "";
        _Tmt += "<layer id=el" + _i + " " + _acT + " width=100%>";
        _Tmt += "<div></div>";
        _Tmt += "<table " + _wid + " " + _bgc + " border=0 cellpadding=0 cellspacing=0 width=100%>";
        _Tmt += _Limg;
        _Tmt += _Lsimg;
        if (_txt) {
            _Tmt += "<td width=100%><table border=0 cellpadding=" + _pad + " cellspacing=0 width=100%><td " + _hgt + " " + _algn + _nw + " >";
            _Tmt += "<a href=\"\" class=item" + _i + " onMouseOver=\"set_status(" + _i + ");return true\">";
            _Tmt += _fgc + _txt;
            _Tmt += "</a>";
            _Tmt += "</td></table></td>"
        }
        _Tmt += _Rimg;
        _Tmt += _Rsimg;
        _Tmt += "</table>";
        _Tmt += "</layer>";
        _Tmt += "<layer visibility=hide id=oel" + _i + " zindex=999 onMouseOver=\"clearTimeout(_MTF);_back2par(" + _i + ");nshl=" + _i + ";this.captureEvents(Event.MOUSEUP);this.onMouseUp=_lc;\" onMouseOut=\"close_el(" + _i + ")\" width=100%>";
        _Tmt += "<div></div>";
        _Tmt += "<table " + _wid + " " + _bgbc + " border=0 cellpadding=0 cellspacing=0 width=100%>";
        _Tmt += _LimgO;
        _Tmt += _LsimgO;
        if (_txt) {
            _Tmt += "<td height=1 width=100%><table border=0 cellpadding=" + _pad + " cellspacing=0 width=100%><td " + _hgt + " " + _algn + _nw + " >";
            _Tmt += "<a class=oitem" + _i + " href=\"" + _lnk + "\" onMouseOver=\"set_status(" + _i + ");return true\">";
            _Tmt += _fgbc + _txt;
            _Tmt += "</a>";
            _Tmt += "</td></table></td>"
        }
        _Tmt += _RimgO;
        _Tmt += _RsimgO;
        _Tmt += "</table>";
        _Tmt += "</layer>";
        _Tmt += "</ilayer>";
        _Tmt += "</td>";
        _hgt = "";
        if (_M[18]) {
            _hgt = "height=" + (_M[18] + 6);
            _hgt = "height=20"
        }
        _spd = "";
        if (_mi[_i][51])
            _spd = _mi[_i][51];
        _sal = "align=center";
        if (_mi[_i][52])
            _sal = "align=" + _mi[_i][52];
        _sbg = "";
        if (_mi[_i][71])
            _sbg = "background=" + _mi[_i][71];
        if (!_M[9]) {
            _Tmt += "</tr>";
            if ((_i != _M[0][_M[0].length - 1]) && _mi[_i][27] > 0) {
                _swid = "100%";
                if (_mi[_i][50])
                    _swid = _mi[_i][50];
                if (_spd)
                    _Tmt += "<tr><td height=" + _spd + "></td></tr>";
                _Tmt += "<tr><td " + _sal + "><table cellpadding=0 cellspacing=0 border=0 width=" + _swid + ">";
                if (_mi[_i][16] && _mi[_i][17]) {
                    _bwid = _mi[_i][27] / 2;
                    if (_bwid < 1)
                        _bwid = 1;
                    _Tmt += "<tr><td bgcolor=" + _mi[_i][17] + ">";
                    _Tmt += "<spacer type=block height=" + _bwid + "></td></tr>";
                    _Tmt += "<tr><td bgcolor=" + _mi[_i][16] + ">";
                    _Tmt += "<spacer type=block height=" + _bwid + "></td></tr>"
                } else {
                    _Tmt += "<td " + _sbg + " bgcolor=" + _mi[_i][10] + ">";
                    _Tmt += "<spacer type=block height=" + _mi[_i][27] + "></td>"
                }
                _Tmt += "</table></td></tr>";
                if (_spd)
                    _Tmt += "<tr><td height=" + _spd + "></td></tr>"
            }
        } else {
            if ((_i != _M[0][_M[0].length - 1]) && _mi[_i][27] > 0) {
                _hgt = "height=100%";
                if (_mi[_i][16] && _mi[_i][17]) {
                    _bwid = _mi[_i][27] / 2;
                    if (_bwid < 1)
                        _bwid = 1;
                    _Tmt += "<td bgcolor=" + _mi[_i][17] + "><spacer type=block " + _hgt + " width=" + _bwid + "></td>";
                    _Tmt += "<td bgcolor=" + _mi[_i][16] + "><spacer type=block " + _hgt + " width=" + _bwid + "></td>"
                } else {
                    if (_spd)
                        _Tmt += "<td><spacer type=block width=" + _spd + "></td>";
                    _Tmt += "<td " + _sbg + " bgcolor=" + _mi[_i][10] + "><spacer type=block " + _hgt + " width=" + _mi[_i][27] + "></td>";
                    if (_spd)
                        _Tmt += "<td><spacer type=block width=" + _spd + "></td>"
                }
            }
        }
        return _Tmt
    }
    function csto(_mnu) {
        _onTS = 0;
        $CtI(_scrmt);
        $CtI(_oMT);
        _MT = setTimeout("closeAllMenus()", _menuCloseDelay)
        }
    function followScroll(_mnu, _cycles, _rate) {
        if (!_startM) {
            _M = _m[_mnu];
            _fogm = _M[22];
            _fgp = gpos(_fogm);
            if (_sT > _M[2] - _M[19])
                _tt = _sT - (_sT - _M[19]);
            else
                _tt = _M[2] - _sT;
            if (_M[6][65])
                _tt += _M[6][65];
            if ((_fgp[0] - _sT) != _tt) {
                diff = _sT + _tt;
                if (diff - _fgp[0] < 1)
                    _rcor = _rate;
                else
                    _rcor = -_rate;
                _nv = parseInt((diff - _rcor - _fgp[0]) / _rate);
                if (_nv != 0)
                    diff = _fgp[0] + _nv;
                spos(_fogm, diff);
                if (_fgp._tp)
                    _M[19] = _fgp._tp;
                if (_m[_mnu][6][65]) {
                    _fgp = gpos(_fogm);
                    spos(gmobj("bord" + _mnu), _fgp[0] - _m[_mnu][6][65])
                    }
            }
        }
        _fS = setTimeout("followScroll(\"" + _mnu + "\"," + _cycles + "," + _rate + ")", _cycles)
        }
    function _drawMenu(_mnu) {
        _mt = "";
        _mcnt++;
        var _M = _m[_mnu];
        _ms = _m[_mnu][6];
        if (_M[9] == "horizontal")
            _M[9] = 1;
        else
            _M[9] = 0;
        _visi = "";
        if (!_M[7])
            _visi = "visibility=hide";
        _top = "top=0";
        if (_M[2])
            _top = "top=" + _M[2];
        _left = "left=0";
        if (_M[3])
            _left = "left=" + _M[3];
        if (_M[9]) {
            _oldBel = _Bel;
            _d.write("<layer visibility=hide id=HT" + _mnu + "><table border=0 cellpadding=0 cellspacing=0>");
            for (_b = 0; _b < _M[0].length; _b++) {
                _d.write(drawItem(_Bel));
                _Bel++
            }
            _d.write("</table></layer>");
            _Bel = _oldBel;
            _gm = gmobj("HT" + _mnu);
            _M[18] = _gm.clip.height - 6
        }
        _bImg = "";
        if (_M[6][46])
            _bImg = "background=" + _M[6][46];
        if (_M[14] != "relative")
            _mt += "<layer zindex=999 " + _bImg + " onmouseout=\"close_menu()\" onmouseover=\"clearTimeout(_MT);\" id=menu" + _mnu + " " + _top + " " + _left + " " + _visi + ">";
        _bgc = "";
        if (_m[_mnu][6].offbgcolor == "transparent")
            _m[_mnu][6].offbgcolor = _n;
        if (_m[_mnu][6].offbgcolor)
            _bgc = "bgcolor=" + _m[_mnu][6].offbgcolor;
        _mrg = 0;
        if (_M[12])
            _mrg = _M[12];
        _mt += "<table " + _bgc + " border=0 cellpadding=" + _mrg + " cellspacing=0 >";
        _mt += "<td>";
        _mt += "<table width=1 border=0 cellpadding=0 cellspacing=0 " + _bgc + ">";
        for (_b = 0; _b < _M[0].length; _b++) {
            _mt += drawItem(_Bel);
            _Bel++
        }
        _mt += "</table>";
        _mt += "</td>";
        _mt += "</table>";
        if (_M[14] != "relative")
            _mt += "</layer>";
        _amt += _mt;
        _d.write(_mt);
        _M[22] = gmobj("menu" + _mnu);
        if (_M[19]) {
            _M[19] = _M[19].toString();
            _fs = _M[19].split(",");
            if (!_fs[1])
                _fs[1] = 20;
            if (!_fs[2])
                _fs[2] = 10;
            _M[19] = _fs[0];
            followScroll(_mnu, _fs[1], _fs[2])
            }
        if (_M[14] != "relative") {
            _st = "";
            _brdsty = "solid";
            if (_M[6].borderstyle)
                _brdsty = _M[6].borderstyle;
            if (_M[6][64])
                _brdsty = _M[6][64];
            _brdcol = "#000000";
            if (_M[6].bordercolor)
                _brdcol = _M[6].bordercolor;
            if (_M[6][63])
                _brdcol = _M[6][63];
            _brdwid = "";
            if (_M[6].borderwidth)
                _brdwid = _M[6].borderwidth;
            if (_M[6][65])
                _brdwid = _M[6][65];
            _M[6][65] = _brdwid;
            _st = ".menu" + _mnu + "{";
            _st += "borderStyle:" + _brdsty + ";";
            _st += "borderColor:" + _brdcol + ";";
            _st += "borderWidth:" + _brdwid + ";";
            if (_ms.fontsize)
                _st += "fontSize:" + 2 + ";";
            _st += "}";
            _d.write("<style>" + _st + "</style>");
            _gm = gmobj("menu" + _mnu);
            _d.write("<layer visibility=hidden id=bord" + _mnu + " zindex=0 class=menu" + _mnu + "><spacer width=" + (_gm.clip.width - 6) + " type=block height=" + (_gm.clip.height - 6) + "></layer>");
            if (_M[7]) {
                _gm = gmobj("menu" + _mnu);
                _gm.zIndex = 999;
                _gp = gpos(_gm);
                spos(_gm, _gp[0] + _M[6][65], _gp[1] + _M[6][65], _gp[2], _gp[3]);
                _gmb = gmobj("bord" + _mnu);
                _gmb.zIndex = 0;
                spos(_gmb, _gp[0], _gp[1], _gp[2], _gp[3]);
                _gmb.visibility = "show"
            }
        } else {}
        if (_m[_mnu][13] == "scroll") {
            _gm = gmobj("menu" + _mnu);
            _gm.fullHeight = _gm.clip.height;
            _scs = ";this.bgColor='" + _m[_mnu][6].onbgcolor + "'\" onmouseout=\"csto(" + _mnu + ");this.bgColor='" + _m[_mnu][6].offbgcolor + "'\"";
            _scs += " visibility=hide " + _bgc + " class=menu" + _mnu + "><table border=0 cellpadding=0 cellspacing=0 width=" + (_gm.clip.width - 6) + "><td align=center>";
            _sce = "</td></table></layer>";
            _d.write("<layer id=tscroll" + _mnu + " onmouseover=\"_is(" + _mnu + "," + _scrollAmount + ");" + _scs + "<img src=images/uparrow.gif>" + _sce);
            _d.write("<layer id=bscroll" + _mnu + " onmouseover=\"_is(" + _mnu + ",-" + _scrollAmount + ");" + _scs + "<img src=images/uparrow.gif>" + _sce);
            _ts = gmobj("tscroll" + _mnu);
            _gm.tsHeight = _ts.clip.height;
            _ts = gmobj("bscroll" + _mnu);
            _gm.bsHeight = _ts.clip.height
        }
    }
    function getMenuByItem(_gel) {
        _gel = _mi[_gel][0];
        if (_m[_gel][7])
            _gel = -1;
        return _gel
    }
    function getParentMenuByItem(_gel) {
        _tm = getMenuByItem(_gel);
        if (_tm == -1)
            return - 1;
        for (_x = 0; _x < _mi.length; _x++) {
            if (_mi[_x][3] == _m[_tm][1]) {
                return _mi[_x][0]
                }
        }
        return - 1
    }
    function getParentItemByItem(_gel) {
        _tm = getMenuByItem(_gel);
        if (_tm == -1)
            return - 1;
        for (_x = 0; _x < _mi.length; _x++) {
            if (_mi[_x][3] == _m[_tm][1]) {
                return _x
            }
        }
        return - 1
    }
    function _setPath(_mpi) {
        if (_mpi > -1) {
            _ci = _m[_mpi][21];
            while (_ci > -1) {
                itemOn(_ci);
                _ci = _m[_mi[_ci][0]][21]
                }
        }
    }
    function _back2par(_i) {
        if (_oldel > -1) {
            if (_i == _m[_mi[_oldel][0]][21]) {
                _popi(_i)
                }
        }
    }
    function closeMenusByArray(_ar) {
        for (_a = 0; _a < _ar.length; _a++) {
            menuDisplay(_ar[_a], 0)
            }
    }
    function cm() {
        _tar = getMenusToClose();
        closeMenusByArray(_tar);
        for (_b = 0; _b < _tar.length; _b++) {
            if (_tar[_b] != _mnu)
                _sm = remove(_sm, _tar[_b])
            }
    }
    function getMenusToClose() {
        _st = -1;
        _en = _sm.length;
        _mm = _iP;
        if (_iP == -1) {
            if (_sm[0] != _masterMenu)
                return _sm;
            _mm = _masterMenu
        }
        for (_b = 0; _b < _sm.length; _b++) {
            if (_sm[_b] == _mm)
                _st = _b + 1;
            if (_sm[_b] == _mnu)
                _en = _b
        }
        if (_st > -1 && _en > -1) {
            _tsm = _sm.slice(_st, _en)
            }
        return _tsm
    }
    function getMenuByName(_mname) {
        _mname = $tL(_mname);
        for (_gma = 0; _gma < _m.length; _gma++) {
            if (_mname == _m[_gma][1]) {
                return _gma
            }
        }
        return - 1
    }
    function clearELs(_i) {
        _mnu = _mi[_i][0];
        for (_q = 0; _q < _m[_mnu][0].length; _q++) {
            gmobj("oel" + _m[_mnu][0][_q]).visibility = "hide"
        }
    }
    function menuDisplay(_mnu, _show) {
        _gm = gmobj("menu" + _mnu);
        _gmb = gmobj("bord" + _mnu);
        M_hideLayer(_mnu, _show);
        for (_q = 0; _q < _m[_mnu][0].length; _q++) {
            gmobj("oel" + _m[_mnu][0][_q]).visibility = "hide"
        }
        if (_show) {
            _gm.zIndex = _zi;
            _gm.visibility = "show";
            _gmb.top = _gm.pageY - _m[_mnu][6][65];
            _gmb.left = _gm.pageX - _m[_mnu][6][65];
            _gmb.zIndex = _zi - 1;
            _gmb.visibility = "show";
            if (_el > -1)
                _m[_mnu][21] = _el;
            if (_m[_mnu][13] == "scroll") {
                if ((_gm.clip.height > _bH) || _gm.nsDoScroll) {
                    _gi = gmobj("el" + _el);
                    _tsm = gmobj("tscroll" + _mnu);
                    _bsm = gmobj("bscroll" + _mnu);
                    if (!_gm.scrollTop)
                        _gm.top = _gm.top + _tsm.clip.height - 1;
                    else
                        _gm.top = _gm.scrollTop;
                    _gm.clip.height = _bH - (_gi.pageY + _gi.clip.height) - 19;
                    _gmb.clip.height = _gm.clip.height;
                    _tsm.top = _gmb.top;
                    _tsm.left = _gmb.left;
                    _tsm.zIndex = _zi + 1;
                    _bsm.left = _gmb.left;
                    _bsm.top = (_gmb.pageY + _gmb.clip.height) - _tsm.clip.height + _gm.tsHeight;
                    _tsm.zIndex = _zi + 1;
                    _tsm.visibility = "show";
                    _bsm.zIndex = _zi + 1;
                    _bsm.visibility = "show";
                    _gm.nsDoScroll = 1
                }
            }
        } else {
            if (! (_m[_mnu][7])) {
                _gm.visibility = "hide";
                _gmb.visibility = "hide";
                if (_m[_mnu][13] == "scroll") {
                    _tsm = gmobj("tscroll" + _mnu);
                    _tsm.visibility = "hide";
                    _tsm = gmobj("bscroll" + _mnu);
                    _tsm.visibility = "hide"
                }
            }
        }
    }
    function forceCloseAllMenus() {
        _cmo = gmobj("menu" + _mi[_cel][0]);
        if (!_cmo)
            _cmo = gmobj("oel" + _cel);
        for (_a = 0; _a < _m.length; _a++) {
            if (!_m[_a][7] && !_m[_a][10])
                menuDisplay(_a, 0)
            }
        _zi = 999;
        _el = -1
    }
    function closeAllMenus() {
        _cmo = gmobj("menu" + _mi[_cel][0]);
        if (!_cmo)
            _cmo = gmobj("oel" + _cel);
        if (!_onTS && _cmo && (MouseX > (_cmo.pageX + _cmo.clip.width) || MouseY > (_cmo.pageY + _cmo.clip.height) || MouseX < _cmo.pageX || MouseY < _cmo.pageY)) {
            inopenmode = 0;
            for (_ca = 0; _ca < _m.length; _ca++) {
                if (!_m[_ca][7] && !_m[_ca][10])
                    menuDisplay(_ca, 0);
                if (_m[_ca][21] > -1) {
                    itemOff(_m[_ca][21]);
                    _m[_ca][21] = -1
                }
            }
            _zi = 999;
            _el = -1
        }
    }
    function close_menu() {
        if (_el == -1)
            _MT = setTimeout("closeAllMenus()", _menuCloseDelay)
        }
    function close_el(_i) {
        if (_mi[_i][43])
            eval(_mi[_i][43]);
        clearELs(_i);
        window.status = "";
        $CtI(_oMT);
        _MT = setTimeout("closeAllMenus()", _menuCloseDelay);
        _el = -1;
        _oldel = _i
    }
    function getParentMenuByItem(_gel) {
        _gel = _mi[_gel][0];
        if (_m[_gel][7])
            _gel = -1;
        return _gel
    }
    function getParentItemByItem(_gel) {
        _par = getParentMenuByItem(_gel);
        for (_a = 0; _a < _m[_par][0].length; _a++) {
            if (_gel == _m[_par][0][_a]) {
                return _m[_par][0]
                }
        }
        return false
    }
    function getParentsByItem(_gmi) {}
    function lc(_i) {
        if (_mi[_i] == "disabled")
            return;
        location.href = _mi[_i][2]
        }
    function _is(_mnu, _SCRam) {
        _onTS = 1;
        _cel = _m[_mnu][0][0];
        $CtI(_MT);
        $CtI(_scrmt);
        _doScroll(_mnu, _SCRam);
        _scrmt = setTimeout("_is(" + _mnu + "," + _SCRam + ")", _scrollDelay)
        }
    function _doScroll(_mnu, _SCRam) {
        gm = gmobj("menu" + _mnu);
        if (_SCRam < 0 && ((gm.clip.top + gm.clip.height) > gm.fullHeight + gm.tsHeight + _SCRam))
            return;
        if (_SCRam > 0 && gm.clip.top < _SCRam)
            return;
        gm.top = gm.top + _SCRam;
        gm.scrollTop = gm.top;
        gm.clip.top = gm.clip.top - _SCRam;
        gm.clip.height = gm.clip.height - _SCRam
    }
    function set_status(_i) {
        if (_mi[_i][4] != null) {
            status = _mi[_i][4]
            } else {
            if (_mi[_i][2])
                status = _mi[_i][2];
            else
                status = ""
        }
    }
    function getOffsetValue(_ofs) {
        _ofsv = 0;
        if (isNaN(_ofs) && _ofs.indexOf("offset=") == 0) {
            _ofsv = parseInt(_ofs.substr(7, 99))
            }
        return _ofsv
    }
    function popup() {
        _arg = arguments;
        $CtI(_MT);
        $CtI(_oMT);
        if (_arg[0] != "M_toolTips")
            if (_cel > -1)
            forceCloseAllMenus();
        if (_arg[0]) {
            if (_arg[0] != "M_toolTips") {
                _sm = new Array;
                closeAllMenus()
                }
            _ofMT = 0;
            _mnu = getMenuByName(_arg[0]);
            _cel = _m[_mnu][0][0];
            _tos = 0;
            if (_arg[2])
                _tos = _arg[2];
            _los = 0;
            if (_arg[3])
                _los = _arg[3];
            _sm[_sm.length] = _mnu;
            if (_arg[1]) {
                _gm = gmobj("menu" + _mnu);
                _gp = gpos(_gm);
                if (_arg[1] == 1) {
                    if (MouseY + _gp[2] > (_bH) + _sT)
                        _tos = -(MouseY + _gp[2] - _bH) + _sT;
                    if (MouseX + _gp[3] > (_bW) + _sL)
                        _los = -(MouseX + _gp[3] - _bW) + _sL;
                    if (_m[_mnu][2]) {
                        if (isNaN(_m[_mnu][2]))
                            _tos = getOffsetValue(_m[_mnu][2]);
                        else {
                            _tos = _m[_mnu][2];
                            MouseY = 0
                        }
                    }
                    if (_m[_mnu][3]) {
                        if (isNaN(_m[_mnu][3]))
                            _los = getOffsetValue(_m[_mnu][3]);
                        else {
                            _los = _m[_mnu][3];
                            MouseX = 0
                        }
                    }
                    if (ns6 && !ns60) {
                        _los -= _sL;
                        _tos -= _sT
                    }
                    spos(_gm, MouseY + _tos, MouseX + _los)
                    } else {
                    for (_a = 0; _a < _d.images.length; _a++) {
                        if (_d.images[_a].name == _arg[1])
                            _po = _d.images[_a]
                        }
                    spos(_gm, _po.y + _po.height + getOffsetValue(_m[_mnu][2]), _po.x + getOffsetValue(_m[_mnu][3]))
                    }
            }
            menuDisplay(_mnu, 1);
            _m[_mnu][21] = -1
        }
    }
    function Opopup(_mn, _mp) {
        $CtI(_MT);
        closeAllMenus();
        if (_mn) {
            _mnu = getMenuByName(_mn);
            _sm[_sm.length] = _mnu;
            menuDisplay(_mnu, 1);
            _m[_mnu][21] = -1
        }
    }
    function popdown() {
        _MT = setTimeout("closeAllMenus()", _menuCloseDelay)
        }
    function _popi(_i) {
        _cel = _i;
        $CtI(_MT);
        $CtI(_cMT);
        $CtI(_oMT);
        if (_mi[_i][34] == "disabled")
            return;
        clearELs(_i);
        if (_oldel > -1)
            clearELs(_oldel);
        _mnu = -1;
        _el = _i;
        _itemRef = _i;
        _mopen = _mi[_i][3];
        horiz = 0;
        if (_m[_mi[_i][0]][9])
            horiz = 1;
        itemOn(_i);
        if (!_sm.length) {
            _sm[_sm.length] = _mi[_i][0];
            _masterMenu = _mi[_i][0]
            }
        _iP = getParentMenuByItem(_el);
        if (_iP == -1)
            _masterMenu = _mi[_i][0];
        set_status(_el);
        _cMT = setTimeout("cm()", _menuOpenDelay);
        if (_mopen && (!_mi[_el][39] || inopenmode)) {
            _gel = gmobj("el" + _i);
            _gp = gpos(_gel);
            _mnu = getMenuByName(_mopen);
            if (_mi[_i][41])
                _m[_mnu][10] = 1;
            if (_mnu > -1) {
                _gp = gpos(_gel);
                _mnO = gmobj("menu" + _mnu);
                _mp = gpos(_mnO);
                if (horiz) {
                    _top = _gp[0] + _gp[2] + 1;
                    _left = _gp[1];
                    if (_m[_mnu][11] == "rtl") {
                        _left = _left - (_mp[3] - _gp[3]) - _mi[_i][27]
                        }
                    if (_m[_mi[_i][0]][5] == "bottom") {
                        _top = (_gp[0] - _mp[2])
                        }
                } else {
                    _top = _gp[0] + _subOffsetTop;
                    _left = _gp[1] + _gp[3] + _subOffsetLeft;
                    if (_m[_mnu][11] == "rtl") {
                        _left = _gp[1] - _mp[3] - _subOffsetLeft
                    }
                }
                if (_left < 0)
                    _left = 0;
                if (_top < 0)
                    _top = 0;
                if (_m[_mnu][2]) {
                    if (isNaN(_m[_mnu][2]) && _m[_mnu][2].indexOf("offset=") == 0) {
                        _os = _m[_mnu][2].substr(7, 99);
                        _top = _top + parseInt(_os)
                        } else {
                        _top = _m[_mnu][2]
                        }
                }
                if (_m[_mnu][3]) {
                    if (isNaN(_m[_mnu][3]) && _m[_mnu][3].indexOf("offset=") == 0) {
                        _os = _m[_mnu][3].substr(7, 99);
                        _left = _left + parseInt(_os)
                        } else {
                        _left = _m[_mnu][3]
                        }
                }
                if (_left + _mp[3] > _bW + _sL) {
                    if (!horiz && (_gp[1] - _mp[3]) > 0) {
                        _left = _gp[1] - _mp[3] - _subOffsetLeft
                    } else {
                        _left = (_bW - _mp[3]) - 2
                    }
                }
                if (!horiz && _top + _mp[2] > _bH + _sT) {
                    _top = (_bH - _mp[2]) - 2
                }
                if (!horiz) {
                    _top = _top - _m[_mnu][6][65]
                    } else {
                    _top--;
                    _left--
                }
                spos(_mnO, _top + _m[_mnu][6][65], _left + _m[_mnu][6][65]);
                if (_m[_mnu][5])
                    _setPosition(_mnu);
                _zi++;
                _mnb = gmobj("bord" + _mnu);
                _oMT = setTimeout("menuDisplay(" + _mnu + ",1)", _menuOpenDelay);
                if (_sm[_sm.length - 1] != _mnu)
                    _sm[_sm.length] = _mnu
            }
        }
        _setPath(_iP)
        }
    function _setPosition(_mnu) {
        if (_m[_mnu][5]) {
            _gm = gmobj("menu" + _mnu);
            _gp = gpos(_gm);
            _osl = 0;
            _omnu3 = 0;
            if (isNaN(_m[_mnu][3]) && _m[_mnu][3].indexOf("offset=") == 0) {
                _omnu3 = _m[_mnu][3];
                _m[_mnu][3] = _n;
                _osl = _omnu3.substr(7, 99);
                _gm.leftOffset = _osl
            }
            _lft = _n;
            if (!_m[_mnu][3]) {
                if (_m[_mnu][5].indexOf("left") != -1)
                    _lft = 0;
                if (_m[_mnu][5].indexOf("center") != -1)
                    _lft = (_bW / 2) - (_gp[3] / 2);
                if (_m[_mnu][5].indexOf("right") != -1)
                    _lft = _bW - _gp[3];
                if (_gm.leftOffset)
                    _lft = _lft + parseInt(_gm.leftOffset)
                }
            _ost = 0;
            _omnu2 = 0;
            if (isNaN(_m[_mnu][2]) && _m[_mnu][2].indexOf("offset=") == 0) {
                _omnu2 = _m[_mnu][2];
                _m[_mnu][2] = _n;
                _ost = _omnu2.substr(7, 99);
                _gm.topOffset = _ost
            }
            _tp = _n;
            if (!_m[_mnu][2] >= 0) {
                _tp = _n;
                if (_m[_mnu][5].indexOf("top") != -1)
                    _tp = 0;
                if (_m[_mnu][5].indexOf("middle") != -1)
                    _tp = (_bH / 2) - (_gp[2] / 2);
                if (_m[_mnu][5].indexOf("bottom") != -1)
                    _tp = _bH - _gp[2];
                if (_gm.topOffset)
                    _tp = _tp + parseInt(_gm.topOffset)
                }
            if (_lft < 0)
                _lft = 0;
            spos(_gm, _tp, _lft);
            if (_m[_mnu][19])
                _m[_mnu][19] = _tp;
            if (_tp)
                _tp = _tp - _m[_mnu][6][65];
            if (_lft)
                _lft = _lft - _m[_mnu][6][65];
            _sb = gmobj("bord" + _mnu);
            spos(_sb, _tp, _lft);
            _gm._tp = _tp
        }
    }
    function _MScan() {
        _bW = self.innerWidth - 16;
        _bH = self.innerHeight - 17;
        _sT = self.pageYOffset;
        if (_startM == 1) {
            for (_a = 0; _a < _m.length; _a++) {
                if (_m[_a][5]) {
                    _setPosition(_a)
                    }
            }
        } else {
            if ((_bH != _oldbH) && (_bW != _oldbW)) {
                location.reload()
                }
        }
        _startM = 0;
        _oldbH = _bH;
        _oldbW = _bW
    }
    setInterval("_MScan()", 200);
    function getMouseXY(e) {
        MouseX = e.pageX;
        MouseY = e.pageY
    }
    _d.captureEvents(Event.MOUSEMOVE);
    _d.onmousemove = getMouseXY;
} else {
    inDragMode = 0;
    function $CtI($ti) {
        clearTimeout($ti)
        }
    function getMouseXY(e) {
        if (ns6) {
            MouseX = e.pageX;
            MouseY = e.pageY
        } else {
            MouseX = event.clientX;
            MouseY = event.clientY
        }
        if (!op && _d.all && _d.body) {
            MouseX = MouseX + _d.body.scrollLeft;
            MouseY = MouseY + _d.body.scrollTop;
            if (IEDtD && !mac) {
                MouseY = MouseY + _sT;
                MouseX = MouseX + _sL;
            }
        }
        if (inDragMode) {
            gm = gmobj(DragLayer);
            spos(gm, MouseY - DragY, MouseX - DragX);
            return _f
        }
        return _t
    }
    _d.onmousemove = getMouseXY;
    function gmobj(_mtxt) {
        if (_d.getElementById) {
            return _d.getElementById(_mtxt)
            } else if (_d.all) {
            return _d.all[_mtxt]
            }
    }
    function spos(_gm, _t, _l, _h, _w) {
        _px = "px";
        if (op) {
            _px = "";
            _gs = _gm.style;
            if (_w != _n)
                _gs.pixelWidth = _w;
            if (_h != _n)
                _gs.pixelHeight = _h
        } else {
            _gs = _gm.style;
            if (_w != _n)
                _gs.width = _w + _px;
            if (_h != _n)
                _gs.height = _h + _px
        }
        if (_t != _n)
            _gs.top = _t + _px;
        if (_l != _n)
            _gs.left = _l + _px
    }
    function gpos(_gm) {
        _h = _gm.offsetHeight;
        _w = _gm.offsetWidth;
        if (op5) {
            _h = _gm.style.pixelHeight;
            _w = _gm.style.pixelWidth
        }
        _tgm = _gm;
        _t = 0;
        while (_tgm != _n) {
            _t += _tgm.offsetTop;
            _tgm = _tgm.offsetParent
        }
        _tgm = _gm;
        _l = 0;
        while (_tgm != _n) {
            _l += _tgm.offsetLeft;
            _tgm = _tgm.offsetParent
        }
        if (sfri) {
            _l -= _d.body.offsetLeft;
            _t -= _d.body.offsetTop
        }
        if (mac && !mac45) {
            if (_macffs = _d.body.currentStyle.marginTop) {
                _t = _t + parseInt(_macffs)
                }
            if (_macffs = _d.body.currentStyle.marginLeft) {
                _l = _l + parseInt(_macffs)
                }
        }
        _gpa = new Array(_t, _l, _h, _w);
        return (_gpa)
        }
    _flta = "return _f";
    if (ie55)
        _flta = "try{if(ap.filters){return 1}}catch(e){}";
    _d.write("<" + "script>function getflta(ap){" + _flta + "}<" + "/script>");
    function applyFilter(_gm, _mnu) {
        if (getflta(_gm)) {
            if (_gm.style.visibility == "visible")
                flt = _m[_mnu][16];
            else
                flt = _m[_mnu][15];
            if (flt) {
                if (_gm.filters[0])
                    _gm.filters[0].stop();
                iedf = "";
                iedf = "FILTER:";
                flt = flt.split(";");
                for (fx = 0; fx < flt.length; fx++) {
                    iedf += " progid:DXImageTransform.Microsoft." + flt[fx];
                    if (navigator.appVersion.indexOf("MSIE 5.5") > 0)
                        fx = 999
                }
                _gm.style.filter = iedf;
                _gm.filters[0].apply()
                }
        }
    }
    function playFilter(_gm, _mnu) {
        if (getflta(_gm)) {
            if (_gm.style.visibility == "visible")
                flt = _m[_mnu][15];
            else
                flt = _m[_mnu][16];
            if (flt)
                _gm.filters[0].play()
            }
    }
    function menuDisplay(_mnu, _show) {
        _gmD = gmobj("menu" + _mnu);
        if (!_gmD)
            return;
        _m[_mnu][22] = _gmD;
        M_hideLayer(_mnu, _show);
        if (_show) {
            if (_m[_mnu][21] > -1 && _m[_mnu][21] != _itemRef) {
                itemOff(_m[_mnu][21]);
                _m[_mnu][21] = _itemRef
            }
            if (_m[_mnu][7] == 0 && _ofMT == 1)
                return;
            if (_gmD.style.visibility.toUpperCase() != "VISIBLE") {
                _SoT(_mnu, 1);
                applyFilter(_gmD, _mnu);
                if (!_m[_mnu][7] && !_m[_mnu][14] && ns6)
                    _gmD.style.position = "fixed";
                _gmD.style.zIndex = _zi;
                _gmD.style.visibility = "visible";
                playFilter(_gmD, _mnu);
                if (!_m[_mnu][7])
                    _m[_mnu][21] = _itemRef;
                _mnuD++
            }
        } else {
            if (_m[_mnu][21] > -1 && _itemRef != _m[_mnu][21])
                itemOff(_m[_mnu][21]);
            if (_gmD.style.visibility.toUpperCase() == "VISIBLE") {
                _SoT(_mnu, 0);
                applyFilter(_gmD, _mnu);
                if (!_m[_mnu][14] && ns6)
                    _gmD.style.position = "absolute";
                _gmD.style.visibility = "hidden";
                if (mac || konq) {
                    _gmD.style.top = "-999px";
                    _gmD.style.left = "-999px"
                }
                playFilter(_gmD, _mnu);
                _mnuD--
            }
            _m[_mnu][21] = -1
        }
    }
    function closeAllMenus() {
        if (_oldel > -1)
            itemOff(_oldel);
        _oldel = -1;
        for (_a = 0; _a < _m.length; _a++) {
            if (!_m[_a][7] && !_m[_a][10])
                menuDisplay(_a, 0)
            }
        _mnuD = 0;
        _zi = 999;
        _itemRef = -1
    }
    _lcC = 0;
    function _lc(_i) {
        _I = _mi[_i];
        _lcC++;
        if (_I[62] && _lcC == 1)
            eval(_I[62]);
        if (_I[34] == "disabled")
            return;
        _feat = "";
        if (_I[57])
            _feat = _I[57];
        _feat = true;
        if (op || _feat || (sfri || ns6 || konq || mac45)) {
            _trg = "";
            if (_I[35])
                _trg = _I[35];
            _trg = "dummy";
            if (_trg)
                OpenWin(_I[2], _trg)
                else(location.href = _I[2])
            } else {
            _gm = gmobj("lnk" + _i);
            _gm.href = _I[2];
            _gm.click()
            }
        closeAllMenus();
        if (_lcC == 2)
            _lcC = 0
    }
    function getMenuByItem(_gel) {
        _gel = _mi[_gel][0];
        if (_m[_gel][7])
            _gel = -1;
        return _gel
    }
    function getParentMenuByItem(_gel) {
        _tm = getMenuByItem(_gel);
        if (_tm == -1)
            return - 1;
        for (_x = 0; _x < _mi.length; _x++) {
            if (_mi[_x][3] == _m[_tm][1]) {
                return _mi[_x][0]
                }
        }
    }
    function getParentItemByItem(_gel) {
        _tm = getMenuByItem(_gel);
        if (_tm == -1)
            return - 1;
        for (_x = 0; _x < _mi.length; _x++) {
            if (_mi[_x][3] == _m[_tm][1]) {
                return _x
            }
        }
    }
    function getMenuByName(_mn) {
        _mn = $tL(_mn);
        for (_xg = 0; _xg < _m.length; _xg++)
            if (_mn == _m[_xg][1])
            return _xg
    }
    _mot = 0;
    function itemOn(_i) {
        $CtI(_mot);
        _mot = null;
        _gmi = gmobj("el" + _i);
        if (_gmi.itemOn == 1)
            return;
        _gmi.itemOn = 1;
        _gmt = gmobj("tr" + _i);
        var _I = _mi[_i];
        if (_I[34] == "header")
            return;
        if (_gmt) {
            _gmt = gmobj("tr" + _i);
            _gs = _gmt.style;
            if (_I[53])
                _gmt.className = _I[53]
            } else {
            _gs = _gmi.style
        }
        if (_I[2] || _I[3]) {
            _mP = (ns6) ? "pointer": "hand";
            if (_I[59])
                _mP = _I[59];
            _gs.cursor = _mP;
            if (_I[29])
                gmobj("img" + _i).style.cursor = _gs.cursor;
            if (_I[24] && _I[3])
                gmobj("simg" + _i).style.cursor = _gs.cursor
        }
        if (_I[32] && _I[29]) {
            gmobj("img" + _i).src = _I[32]
            }
        if (_I[3] && _I[24] && _I[48]) {
            gmobj("simg" + _i).src = _I[48]
            }
        if (_I[53])
            _gmi.className = _I[53];
        if (_I[6])
            _gs.color = _I[6];
        if (_I[5])
            _gmi.style.background = _I[5];
        if (_I[47]) {
            _oi = "url(" + _I[47] + ")";
            if (_gmi.style.backgroundImage != _oi);
            _gmi.style.backgroundImage = _oi
        }
        if (_I[26])
            _gs.textDecoration = _I[26];
        if (!mac) {
            if (_I[44])
                _gs.fontWeight = "bold";
            if (_I[45])
                _gs.fontStyle = "italic"
        }
        if (_I[42])
            eval(_I[42]);
        if (_I[25]) {
            _gmi.style.border = _I[25];
            if (!_I[9])
                _gs.padding = _I[11] - parseInt(_gmi.style.borderWidth) + "px"
        }
    }
    function itemOff(_i) {
        _gmi = gmobj("el" + _i);
        if (!_gmi)
            return;
        if (_gmi.itemOn == 0)
            return;
        _gmi.itemOn = 0;
        _gmt = gmobj("tr" + _i);
        var _I = _mi[_i];
        if (_I[32] && _I[29]) {
            gmobj("img" + _i).src = _I[29]
            }
        if (_I[3] && _I[24] && _I[48]) {
            gmobj("simg" + _i).src = _I[24]
            }
        if (_I[4] != "none") {
            if (_I[4] != null || _I[2])
                window.status = window.defaultStatus
        }
        if (_i == -1)
            return;
        if (_gmt) {
            _gmt = gmobj("tr" + _i);
            _gs = _gmt.style;
            if (_I[54])
                _gmt.className = _I[54]
            } else {
            _gs = _gmi.style
        }
        if (_I[54])
            _gmi.className = _I[54];
        if (_I[46])
            _gmi.style.backgroundImage = "url(" + _I[46] + ")";
        else if (_I[7])
            _gmi.style.background = _I[7];
        if (_I[8])
            _gs.color = _I[8];
        if (_I[26])
            _gs.textDecoration = "none";
        if (_I[33])
            _gs.textDecoration = _I[33];
        if (!mac) {
            if (_I[44] && (_I[14] == "normal" || !_I[14]))
                _gs.fontWeight = "normal";
            if (_I[45] && (_I[13] == "normal" || !_I[13]))
                _gs.fontStyle = "normal"
        }
        if (!_startM && _I[43])
            eval(_I[43]);
        if (_I[25]) {
            _gmi.style.border = "0px";
            if (!_I[9])
                _gs.padding = _I[11] + "px"
        }
        if (_I[9]) {
            _gmi.style.border = _I[9]
            }
    }
    function closeMenusByArray(_cmnu) {
        for (_a = 0; _a < _cmnu.length; _a++)
            if (_cmnu[_a] != _mnu)
            if (!_m[_cmnu[_a]][7])
            menuDisplay(_cmnu[_a], 0)
        }
    function getMenusToClose() {
        _st = -1;
        _en = _sm.length;
        _mm = _iP;
        if (_iP == -1) {
            if (_sm[0] != _masterMenu)
                return _sm;
            _mm = _masterMenu
        }
        for (_b = 0; _b < _sm.length; _b++) {
            if (_sm[_b] == _mm)
                _st = _b + 1;
            if (_sm[_b] == _mnu)
                _en = _b
        }
        if (_st > -1 && _en > -1) {
            _tsm = _sm.slice(_st, _en)
            }
        return _tsm
    }
    function _cm() {
        _tar = getMenusToClose();
        closeMenusByArray(_tar);
        for (_b = 0; _b < _tar.length; _b++) {
            if (_tar[_b] != _mnu)
                _sm = remove(_sm, _tar[_b])
            }
    }
    function _getDims() {
        if (!op && _d.all) {
            _mc = _d.body;
            if (IEDtD && !mac && !op7)
                _mc = _d.documentElement;
            if (!_mc)
                return;
            _bH = _mc.clientHeight;
            _bW = _mc.clientWidth;
            _sT = _mc.scrollTop;
            _sL = _mc.scrollLeft
        } else {
            _bH = window.innerHeight;
            _bW = window.innerWidth;
            if (ns6) {
                if (_d.documentElement.offsetWidth != _bW) {
                    _bW = _bW - 15
                }
            }
            _sT = self.scrollY;
            _sL = self.scrollX;
            if (op) {
                _sT = _d.body.scrollTop;
                _sL = _d.body.scrollleft
            }
        }
    }
    function c_openMenu(_i) {
        var _I = _mi[_i];
        if (_I[3]) {
            _oldMC = _I[39];
            _I[39] = 0;
            _oldMD = _menuOpenDelay;
            _menuOpenDelay = 0;
            _gm = gmobj("menu" + getMenuByName(_I[3]));
            if (_gm != null && _gm.style.visibility == "visible" && _I[40]) {
                menuDisplay(getMenuByName(_I[3]), 0);
                itemOn(_i)
                } else {
                _popi(_i)
                }
            _menuOpenDelay = _oldMD;
            _I[39] = _oldMC
        } else {
            if (_I[2] && _I[39])
                eval(_I[2])
            }
    }
    function getOffsetValue(_ofs) {
        _ofsv = null;
        if (_ofs) {
            _ofsv = _ofs
        }
        if (isNaN(_ofs) && _ofs.indexOf("offset=") == 0) {
            _ofsv = parseInt(_ofs.substr(7, 99))
            }
        return _ofsv
    }
    function popup() {
        _arg = arguments;
        $CtI(_MT);
        _MT = null;
        $CtI(_oMT);
        _oMT = null;
        if (_arg[0]) {
            if (_arg[0] != "M_toolTips") {
                _sm = new Array;
                closeAllMenus()
                }
            _ofMT = 0;
            _mnu = getMenuByName(_arg[0]);
            if (_arg[0]) {
                if (!_m[_mnu][23] && !_startM) {
                    _m[_mnu][23] = _mnu;
                    BDMenu(_mnu)
                    }
            }
            _tos = 0;
            if (_arg[2])
                _tos = _arg[2];
            _los = 0;
            if (_arg[3])
                _los = _arg[3];
            _sm[_sm.length] = _mnu;
            if (ns6 && !ns60) {
                _los -= _sL;
                _tos -= _sT;
                _gm = gmobj("menu" + _mnu);
                _gp = gpos(_gm);
                spos(_gm, _m[_mnu][2] + _tos, _m[_mnu][3] + _los)
                }
            if (_arg[1]) {
                _gm = gmobj("menu" + _mnu);
                if (!_gm)
                    return;
                _gp = gpos(_gm);
                if (_arg[1] == 1) {
                    if (MouseY + _gp[2] > (_bH + _sT))
                        _tos = -(MouseY + _gp[2] - _bH) + _sT;
                    if (MouseX + _gp[3] > (_bW + _sL))
                        _los = -(MouseX + _gp[3] - _bW) + _sL;
                    if (_m[_mnu][2]) {
                        if (isNaN(_m[_mnu][2]))
                            _tos = getOffsetValue(_m[_mnu][2]);
                        else {
                            _tos = _m[_mnu][2];
                            MouseY = 0
                        }
                    }
                    if (_m[_mnu][3]) {
                        if (isNaN(_m[_mnu][3]))
                            _los = getOffsetValue(_m[_mnu][3]);
                        else {
                            _los = _m[_mnu][3];
                            MouseX = 0
                        }
                    }
                    spos(_gm, (MouseY + _tos), (MouseX + _los))
                    } else {
                    _po = gmobj(_arg[1]);
                    _pp = gpos(_po);
                    spos(_gm, _pp[0] + _pp[2] + getOffsetValue(_m[_mnu][2]) + _tos, _pp[1] + getOffsetValue(_m[_mnu][3]) + _los)
                    }
            }
            _zi = _zi + 100;
            if (_m[_mnu][13] == "scroll")
                _check4Scroll(_mnu);
            menuDisplay(_mnu, 1);
            _m[_mnu][21] = -1
        }
    }
    function popdown() {
        $CtI(_Mtip);
        _Mtip = null;
        _MT = setTimeout("closeAllMenus()", _menuCloseDelay)
        }
    function BDMenu(_mnu) {
        if (op5 || op6 || mac)
            return;
        _gm = gmobj("menu" + _mnu);
        innerText = _drawMenu(_mnu, 0);
        _mcnt--;
        _gm.innerHTML = innerText;
        _fixMenu(_mnu)
        }
    function _popi(_i) {
        $CtI(_Mtip);
        _Mtip = null;
        _itemRef = _i;
        var _I = _mi[_i];
        if (!_I || _startM)
            return;
        _pMnu = _m[_I[0]];
        $CtI(_MT);
        _MT = null;
        _mopen = _I[3];
        if (_mopen) {
            _mnu = getMenuByName(_mopen);
            if (_m[_mnu] && !_m[_mnu][23]) {
                if (!_startM)
                    _m[_mnu][23] = _mnu;
                BDMenu(_mnu)
                }
        }
        if (_oldel > -1) {
            gm = 0;
            if (_I[3]) {
                gm = gmobj("menu" + getMenuByName(_I[3]));
                if (gm && gm.style.visibility.toUpperCase() == "VISIBLE" && _i == _oldel) {
                    itemOn(_i);
                    return
                }
            }
            if (_oldel != _i)
                itemOff(_oldel);
            $CtI(_oMT);
            _oMT = null
        }
        $CtI(_cMT);
        _cMT = null;
        _mnu = -1;
        if (_I[34] == "disabled")
            return;
        horiz = 0;
        if (_pMnu[9])
            horiz = 1;
        itemOn(_i);
        if (!_sm.length) {
            _sm[_sm.length] = _I[0];
            _masterMenu = _I[0]
            }
        _iP = getMenuByItem(_i);
        if (_iP == -1)
            _masterMenu = _I[0];
        if (_I[4] != "none") {
            if (_I[4] != null)
                window.status = _I[4];
            else if (_I[2])
                window.status = _I[2]
            }
        _cMT = setTimeout("_cm()", _menuOpenDelay);
        if (_I[39]) {
            if (_mopen) {
                _gm = gmobj("menu" + _mnu);
                if (_gm && _gm.style.visibility.toUpperCase() == "VISIBLE") {
                    $CtI(_cMT);
                    _cMT = null;
                    _tsm = _sm[_sm.length - 1];
                    if (_tsm != _mnu)
                        menuDisplay(_tsm, 0)
                    }
            }
        }
        if (!window.retainClickValue)
            inopenmode = 0;
        if (_mopen && (!_I[39] || inopenmode) && _I[34] != "tree") {
            _getDims();
            _pm = gmobj("menu" + _I[0]);
            _pp = gpos(_pm);
            _mnu = getMenuByName(_mopen);
            if (_I[41])
                _m[_mnu][10] = 1;
            if (ie4 || op || konq)
                _fixMenu(_mnu);
            if (_mnu > -1) {
                if (_oldel > -1 && (_mi[_oldel][0] + _I[0]))
                    menuDisplay(_mnu, 0);
                _oMT = setTimeout("menuDisplay(" + _mnu + ",1)", _menuOpenDelay);
                _mnO = gmobj("menu" + _mnu);
                _mp = gpos(_mnO);
                if (ie4) {
                    _mnT = gmobj("tbl" + _mnu);
                    _tp = gpos(_mnT);
                    _mp[3] = _tp[3]
                    }
                _gmi = gmobj("el" + _i);
                if (!horiz && mac)
                    _gmi = gmobj("pTR" + _i);
                _gp = gpos(_gmi);
                if (horiz) {
                    _left = _gp[1];
                    _top = _pp[0] + _pp[2] - _I[65]
                    } else {
                    _left = _pp[1] + _pp[3] - _I[65];
                    _top = _gp[0]
                    }
                if (sfri) {
                    if (_pMnu[14] == "relative") {
                        _left = _left + _d.body.offsetLeft;
                        _top = _top + _d.body.offsetTop
                    }
                }
                if (_pMnu[13] == "scroll" && !op && !mac45 && !sfri && !konq) {
                    if (ns6 && !ns7)
                        _top = _top - gevent;
                    else
                        _top = _top - _pm.scrollTop
                }
                if (_m[_mnu][2] != _n) {
                    if (isNaN(_m[_mnu][2]) && _m[_mnu][2].indexOf("offset=") == 0) {
                        _top = _top + getOffsetValue(_m[_mnu][2])
                        } else {
                        _top = _m[_mnu][2]
                        }
                }
                if (_m[_mnu][3] != _n) {
                    if (isNaN(_m[_mnu][3]) && _m[_mnu][3].indexOf("offset=") == 0) {
                        _left = _left + getOffsetValue(_m[_mnu][3])
                        } else {
                        _left = _m[_mnu][3]
                        }
                }
                if (!horiz && (_top + _mp[2] + 20) > (_bH + _sT)) {
                    _top = (_bH - _mp[2]) + _sT - 16
                }
                if (_left + _mp[3] > _bW + _sL) {
                    if (!horiz && (_pp[1] - _mp[3]) > 0) {
                        _left = _pp[1] - _mp[3] - _subOffsetLeft + _pMnu[6][65]
                        } else {
                        _left = (_bW - _mp[3]) - 8
                    }
                }
                if (horiz) {
                    if (_m[_mnu][11] == "rtl")
                        _left = _left - _mp[3] + _gp[3] + 2;
                    if (_pMnu[5] && _pMnu[5].indexOf("bottom") != -1) {
                        _top = _pp[0] - _mp[2] - 1
                    }
                } else {
                    if (_m[_mnu][11] == "rtl")
                        _left = _pp[1] - _mp[3] - (_subOffsetLeft * 2);
                    _top += _subOffsetTop;
                    _left += _subOffsetLeft
                }
                if (_left < 2)
                    _left = 2;
                if (_top < 2)
                    _top = 2;
                if (ns60) {
                    _left -= +_pMnu[6][65];
                    _top -= +_pMnu[6][65]
                    }
                if (mac) {
                    _left -= _pMnu[12] + _pMnu[6][65];
                    _top -= _pMnu[12] + _pMnu[6][65]
                    }
                if (sfri || op) {
                    if (!horiz) {
                        _top -= _pMnu[6][65]
                        } else {
                        _left -= _pMnu[6][65]
                        }
                }
                if (_m[_I[0]][7] && (ns6 || ns7))
                    _top = _top - _sT;
                spos(_mnO, _top, _left);
                if (_m[_mnu][5])
                    _setPosition(_mnu);
                if (_m[_mnu][13] == "scroll")
                    _check4Scroll(_mnu);
                _zi++;
                _mnO.style.zIndex = _zi;
                if (_sm[_sm.length - 1] != _mnu)
                    _sm[_sm.length] = _mnu
            }
        }
        _setPath(_iP);
        _oldel = _i;
        _ofMT = 0
    }
    function _check4Scroll(_mnu) {
        if (op)
            return;
        _M = _m[_mnu];
        gm = gmobj("menu" + _mnu);
        _gp = gpos(gm);
        gmt = gmobj("tbl" + _mnu);
        _gt = gpos(gmt);
        _MS = _M[6];
        _cor = (_M[12] * 2) + (_MS.borderwidth * 2);
        _sdim = _gt[2] + _sT;
        if (horiz)
            _sdim = _gt[2] + _gt[0] - _sT;
        if (_m[_mnu][2] && !isNaN(_m[_mnu][2]))
            _sdim = _m[_mnu][2] + _gt[2];
        if (_sdim < (_bH + _sT)) {
            gm.style.overflow = "";
            _top = _n;
            if (!horiz && (_gt[0] + _gt[2] + 16) > (_bH + _sT)) {
                _top = (_bH - _gt[2]) + _sT - 16
            }
            _ofx = 0;
            if (op7)
                _ofx = _cor;
            _ofy = 0;
            if (mac)
                _ofy = _cor;
            spos(gm, _top, _n, _gt[2] + _ofy, _gt[3] + _ofx);
            return
        }
        gm.style.overflow = "auto";
        _sbw = _gt[3];
        if (mac) {
            if (IEDtD)
                _sbw = _sbw + 16;
            else
                _sbw = _sbw + 16 + _cor;
            _btm = gmobj("btm" + _mnu);
            _btm.style.height = _M[12] * 2 + "px"
        } else if (IEDtD) {
            if (op7) {
                _sbw = _sbw + 16
            } else {
                _sbw += _d.documentElement.offsetWidth - _d.documentElement.clientWidth - 3
            }
        } else {
            if (op7) {
                _sbw = _sbw + 16 + _cor
            } else {
                _sbw += _d.body.offsetWidth - _d.body.clientWidth - 4 + _cor
            }
            if (ie4)
                _sbw = _gt[3] + 16 + _cor;
            if (ns6 || sfri) {
                _sbw = _gt[3] + 15;
                if (!navigator.vendor)
                    _sbw = _sbw + 4
            }
        }
        _top = _n;
        if (horiz) {
            _ht = _bH - _gt[0] - 16 + _sT
        } else {
            _ht = _bH - 16;
            _top = 6 + _sT
        }
        _left = _n;
        if (_gp[1] + _sbw > (_bW + _sL)) {
            _left = (_bW - _sbw) - 2
        }
        if (_m[_mnu][2] && !isNaN(_m[_mnu][2])) {
            _top = _m[_mnu][2];
            _ht = _bH - _top - 6
        }
        if (_ht > 0)
            spos(gm, _top, _left, _ht + 2, _sbw)
        }
    function _setPath(_mpi) {
        if (_mpi > -1) {
            _ci = _m[_mpi][21];
            while (_ci > -1) {
                itemOn(_ci);
                _ci = _m[_mi[_ci][0]][21]
                }
        }
    }
    function _CAMs() {
        _MT = setTimeout("_AClose()", _menuCloseDelay);
        $CtI(_oMT);
        _oMT = null;
        _ofMT = 1
    }
    function _AClose() {
        if (_ofMT == 1) {
            closeAllMenus();
            inopenmode = 0
        }
    }
    function _setCPage(_i) {
        if (_i[18])
            _i[8] = _i[18];
        if (_i[19])
            _i[7] = _i[19];
        if (_i[56])
            _i[29] = _i[56];
        if (_i[69])
            _i[46] = _i[69];
        if (_i[48] && _i[3])
            _i[24] = _i[48];
        if (_i[25])
            _i[9] = _i[25];
        if (_i[72])
            _i[54] = _i[72];
        if (_I[75])
            _I[9] = _I[75]
        }
    function _getCurrentPage() {
        _I = _mi[_el];
        if (_I[2]) {
            _url = _I[2];
            _hrf = location.href;
            fstr = _hrf.substr((_hrf.length - _url.length), _url.length);
            if (fstr == _url) {
                _setCPage(_I);
                _cip[_cip.length] = _el
            }
        }
    }
    function _oifx(_i) {
        _G = gmobj("simg" + _i);
        spos(_G, _n, _n, _G.height, _G.width);
        spos(gmobj("el" + _i), _n, _n, _G.height, _G.width)
        }
    function _getLink(_I, _gli) {
        _link = "";
        actiontext += " onMouseOver=\"_popi(" + _gli + ")\" onclick=\"opentree();";
        if (_I[2]) {
            _targ = "";
            if (_I[35])
                _targ = "target=" + _I[35];
            _link = "<a id=lnk" + _gli + " href=\"" + _I[2] + "\" " + _targ + "></a>";
            actiontext += "_lc(" + _gli + ");c_openMenu(" + _gli + ")"
        }
        actiontext += "\"";
        return _link
    }
    function _mOUt(_i) {
        $CtI(_Mtip);
        _Mtip = null;
        itemOff(_i)
        }
    function _getItem(_i, _M) {
        _it = "";
        _el = _M[0][_i];
        _I = _mi[_el];
        _mnu = _I[0];
        var _M = _m[_mnu];
        _getCurrentPage();
        if (_I[34] == "header") {
            if (_I[20])
                _I[8] = _I[20];
            if (_I[21])
                _I[7] = _I[21];
            if (_I[74])
                _I[9] = _I[74]
            }
        _ofb = (_I[46] ? "background-image:url(" + _I[46] + ");": "");
        if (!_ofb)
            _ofb = (_I[7] ? "background:" + _I[7] : "");
        _ofc = (_I[8] ? "color:" + _I[8] : "");
        _fsize = (_I[12] ? ";font-Size:" + _I[12] : "");
        _fstyle = (_I[13] ? ";font-Style:" + _I[13] : "");
        _fweight = (_I[14] ? ";font-Weight:" + _I[14] : "");
        _ffam = (_I[15] ? ";font-Family:" + _I[15] : "");
        _tdec = (_I[33] ? ";text-Decoration:" + _I[33] : "");
        actiontext = " onmouseout=_mot=setTimeout('_mOUt(" + _el + ")',80) ";
        _link = "";
        if (_I[39]) {
            actiontext += " onclick=\"inopenmode=1;c_openMenu(" + _el + ");\" onMouseOver=\"_popi(" + _el + ");\""
        } else {
            _link = _getLink(_I, _el)
            }
        if (_I[34] == "dragable")
            actiontext += " onmousedown=\"drag_drop('menu" + _mnu + "')\"";
        _clss = "";
        if (_I[54])
            _clss = "class=" + _I[54];
        if (horiz) {
            if (_i == 0)
                _it += "<tr " + _clss + ">"
        } else {
            _it += "<tr id=pTR" + _el + " " + _clss + ">"
        }
        _subC = 0;
        if (_I[3] && _I[24])
            _subC = 1;
        _timg = "";
        _bimg = "";
        if (_I[29]) {
            _imalgn = "";
            if (_I[31])
                _imalgn = "align=" + _I[31];
            _imcspan = "";
            if (_subC && _imalgn && _I[31] != "left")
                _imcspan = "colspan=2";
            _imgwd = "width=1";
            if (_imalgn && _I[31] != "left")
                _imgwd = "";
            _Iwid = "";
            if (_I[37])
                _Iwid = " width=" + _I[37];
            _Ihgt = "";
            if (_I[38])
                _Ihgt = " height=" + _I[38];
            _imgalt = "";
            if (_I[58])
                _imgalt = "title=\"" + _I[58] + "\"";
            _timg = "<td " + _imcspan + " " + _imalgn + " " + _imgwd + "><img " + _imgalt + " " + _Iwid + _Ihgt + " id=img" + _el + " src=\"" + _I[29] + "\"></td>";
            if (_I[30] == "top")
                _timg += "</tr><tr>";
            if (_I[30] == "right") {
                _bimg = _timg;
                _timg = ""
            }
            if (_I[30] == "bottom") {
                _bimg = "<tr>" + _timg + "</tr>";
                _timg = ""
            }
        }
        _algn = "";
        if (_M[8])
            _algn = "align=" + _M[8];
        if (_I[36])
            _algn = "align=" + _I[36];
        if (_M[8])
            _algn += " valign=" + _M[8];
        if (_I[61])
            _algn += " valign=" + _I[61];
        _iw = "";
        _iheight = "";
        _padd = "padding:" + _I[11] + "px";
        _offbrd = "";
        if (_I[9])
            _offbrd = "border:" + _I[9] + ";";
        if (_subC || _I[29] || (_M[4] && horiz)) {
            _Limg = "";
            _Rimg = "";
            _itrs = "";
            _itre = "";
            if (_I[3] && _I[24]) {
                _subIR = 0;
                if (_M[11] == "rtl")
                    _subIR = 1;
                _oif = "";
                if (op7)
                    _oif = " onload=_oifx(" + _el + ") ";
                _img = "<img id=simg" + _el + " src=" + _I[24] + _oif + ">";
                _simgP = "";
                if (_I[22])
                    _simgP = ";padding:" + _I[22] + "px";
                _imps = "width=1";
                if (_I[23]) {
                    _iA = "width=1";
                    _ivA = "";
                    _imP = _I[23].split(" ");
                    for (_ia = 0; _ia < _imP.length; _ia++) {
                        if (_imP[_ia] == "left")
                            _subIR = 1;
                        if (_imP[_ia] == "right")
                            _subIR = 0;
                        if (_imP[_ia] == "top" || _imP[_ia] == "bottom" || _imP[_ia] == "middle") {
                            _ivA = "valign=" + _imP[_ia];
                            if (_imP[_ia] == "bottom")
                                _subIR = 0
                        }
                        if (_imP[_ia] == "center") {
                            _itrs = "<tr>";
                            _itre = "</tr>";
                            _iA = "align=center width=100%"
                        }
                    }
                    _imps = _iA + " " + _ivA
                }
                _its = _itrs + "<td " + _imps + " style=\"font-size:1px" + _simgP + "\">";
                _ite = "</td>" + _itre;
                if (_subIR) {
                    _Limg = _its + _img + _ite
                } else {
                    _Rimg = _its + _img + _ite
                }
            }
            if (_M[4])
                _iw = "width=" + _M[4];
            if (_iw == "" && !_I[1])
                _iw = "width=1";
            if (_I[55])
                _iw = "width=" + _I[55];
            if (!horiz)
                _iw = "width=100%";
            if (_M[18]) {
                _iheight = "style=\"height:" + _M[18] + "px;\""
            }
            if (_I[28]) {
                _iheight = "style=\"height:" + _I[28] + "px;\""
            }
            _it += "<td id=el" + _el + " " + actiontext + " style=\"" + _offbrd + _ofb + ";\">";
            _it += "<table border=0 cellpadding=0 cellspacing=0 " + _iheight + " " + _iw + " id=MTbl" + _el + ">";
            _it += "<tr id=td" + _el + " style=\"" + _ofc + ";\">";
            _it += _Limg;
            _it += _timg;
            _iw = "width=100%";
            if (ie4 || ns6)
                _iw = "";
            if (_I[1]) {
                _it += "<td " + _iw + " " + _clss + " " + _nw + " id=tr" + _el + " style=\"" + _ofc + _fsize + _ffam + _fweight + _fstyle + _tdec + ";" + _padd + "\" " + _algn + ">" + _link + " " + _I[1] + "</td>"
            } else {
                _it += _link
            }
            _it += _bimg;
            _it += _Rimg;
            _it += "</tr>";
            _it += "</table>";
            _it += "</td>"
        } else {
            if (_M[18])
                _iheight = "height:" + _M[18] + "px;";
            if (_I[28])
                _iheight = "height:" + _I[28] + "px;";
            _iw = "";
            if (_I[55]) {
                _iw = "width=" + _I[55];
                if (ns6)
                    _link = "<div style=\"width:" + _I[55] + "px;\">" + _link
            }
            _it += "<td " + _clss + " " + _iw + " " + _nw + " tabindex=" + _el + " id=el" + _el + " " + actiontext + " " + _algn + " style=\"" + _offbrd + _iheight + _ofc + _fsize + _ffam + _fweight + _fstyle + _tdec + ";" + _ofb + ";" + _padd + "\">" + _link + " " + _I[1] + "</td>"
        }
        if ((_M[0][_i] != _M[0][_M[0].length - 1]) && _I[27] > 0) {
            _sepadd = "";
            _brd = "";
            _sbg = ";background:" + _I[10];
            if (_I[71])
                _sbg = ";background-image:url(" + _I[71] + ");";
            if (_I[27]) {
                if (horiz) {
                    if (_I[49]) {
                        _sepA = "middle";
                        if (_I[52])
                            _sepA = _I[52];
                        if (_I[51])
                            _sepadd = "style=\"padding:" + _I[51] + "px;\"";
                        _it += "<td nowrap " + _sepadd + " valign=" + _sepA + " align=left width=1><div style=\"font-size:1px;width:" + _I[27] + ";height:" + _I[49] + ";" + _brd + _sbg + ";\"></div></td>"
                    } else {
                        if (_I[16] && _I[17]) {
                            _bwid = _I[27] / 2;
                            if (_bwid < 1)
                                _bwid = 1;
                            _brdP = _bwid + "px solid ";
                            _brd += "border-right:" + _brdP + _I[16] + ";";
                            _brd += "border-left:" + _brdP + _I[17] + ";";
                            if (mac || sfri || (ns6 && !ns7)) {
                                _it += "<td style=\"width:" + _I[27] + "px;empty-cells:show;" + _brd + "\"></td>"
                            } else {
                                _iT = "<table border=0 cellpadding=0 cellspacing=0><td></td></table>";
                                if (ns6 || ns7)
                                    _iT = "";
                                _it += "<td style=\"empty-cells:show;" + _brd + "\">" + _iT + "</td>"
                            }
                        } else {
                            if (_I[51])
                                _sepadd = "<td nowrap width=" + _I[51] + "></td>";
                            _it += _sepadd + "<td style=\"width:" + _I[27] + "px;" + _brd + _sbg + "\"><table border=0 cellpadding=0 cellspacing=0 width=" + _I[27] + "><td></td></table></td>" + _sepadd
                        }
                    }
                } else {
                    if (_I[16] && _I[17]) {
                        _bwid = _I[27] / 2;
                        if (_bwid < 1)
                            _bwid = 1;
                        _brdP = _bwid + "px solid ";
                        _brd = "border-bottom:" + _brdP + _I[16] + ";";
                        _brd += "border-top:" + _brdP + _I[17] + ";";
                        if (mac || ns6 || sfri)
                            _I[27] = 0
                    }
                    if (_I[51])
                        _sepadd = "<tr><td height=" + _I[51] + "></td></tr>";
                    _sepW = "100%";
                    if (_I[50])
                        _sepW = _I[50];
                    _sepA = "center";
                    if (_I[52])
                        _sepA = _I[52];
                    if (!mac)
                        _sbg += ";overflow:hidden";
                    _it += "</tr>" + _sepadd + "<tr><td align=" + _sepA + "><div style=\"" + _sbg + ";" + _brd + "width:" + _sepW + ";height:" + _I[27] + "px;font-size:1px;\"></div></td></tr>" + _sepadd + ""
                }
            }
        }
        return _it
    }
    function _fixMenu(_mnu) {
        _gmt = gmobj("tbl" + _mnu);
        _gm = gmobj("menu" + _mnu);
        if (_gm) {
            if (op5)
                _gm.style.pixelWidth = _gmt.style.pixelWidth + (_m[_mnu][12] * 2) + (_m[_mnu][6][65] * 2);
            if ((ie4) || _m[_mnu][14] == "relative")
                _gm.style.width = _gmt.offsetWidth + "px";
            if (konq)
                _gm.style.width = _gmt.offsetWidth
        }
    }
    gevent = 0;
    function getEVT(evt, _mnu) {
        if (evt.target.tagName == "TD") {
            _egm = gmobj("menu" + _mnu);
            gevent = evt.layerY - (evt.pageY - _d.body.offsetTop) + _egm.offsetTop
        }
    }
    _ifc = 0;
    function _drawMenu(_mnu, _begn) {
        _mcnt++;
        var _M = _m[_mnu];
        _mt = "";
        _nw = "";
        _MS = _M[6];
        _tablewidth = "";
        if (_M[4]) {
            _tablewidth = "width=" + _M[4];
            if (op7 && !IEDtD)
                _tablewidth = "width=" + (_M[4] - (_M[12] * 2) - (_MS.borderwidth * 2))
            } else {
            if (!_M[17])
                _nw = "nowrap"
        }
        _top = "";
        _left = "";
        if (!_M[14] && !_M[7]) {
            _top = "top:-999px";
            _left = "left:-999px"
        }
        if (_M[2] != _n)
            if (!isNaN(_M[2]))
            _top = "top:" + _M[2] + "px";
        if (_M[3] != _n)
            if (!isNaN(_M[3]))
            _left = "left:" + _M[3] + "px";
        _mnuHeight = "";
        if (_M[9] == "horizontal" || _M[9] == 1) {
            _M[9] = 1;
            horiz = 1;
            if (_M[18])
                _mnuHeight = "height=" + _M[18]
            } else {
            _M[9] = 0;
            horiz = 0
        }
        _visi = "hidden";
        _ofb = "";
        if (_MS.offbgcolor)
            _ofb = "background:" + _MS.offbgcolor;
        _brd = "";
        _brdP = "";
        _brdwid = "";
        if (_MS.borderwidth) {
            _brdsty = "solid";
            if (_MS.borderstyle)
                _brdsty = _MS.borderstyle;
            _brdcol = "none";
            if (_MS.bordercolor)
                _brdcol = _MS.bordercolor;
            if (_MS.borderwidth)
                _brdwid = _MS.borderwidth;
            _brdP = _brdwid + "px " + _brdsty + " ";
            _brd = "border:" + _brdP + _brdcol + ";"
        }
        _Mh3 = _MS.high3dcolor;
        _Ml3 = _MS.low3dcolor;
        if (_Mh3 && _Ml3) {
            _h3d = _Mh3;
            _l3d = _Ml3;
            if (_MS.swap3d) {
                _h3d = _Ml3;
                _l3d = _Mh3
            }
            _brdP = _brdwid + "px solid ";
            _brd = "border-bottom:" + _brdP + _h3d + ";";
            _brd += "border-right:" + _brdP + _h3d + ";";
            _brd += "border-top:" + _brdP + _l3d + ";";
            _brd += "border-left:" + _brdP + _l3d + ";"
        }
        _ns6ev = "";
        if (_M[13] == "scroll" && ns6 && !ns7)
            _ns6ev = "onmousemove=\"getEVT(event," + _mnu + ")\"";
        _bgimg = "";
        if (_MS.menubgimage)
            _bgimg = ";background-image:url(" + _MS.menubgimage + ");";
        _posi = "absolute";
        if (_M[14]) {
            _posi = _M[14];
            if (_M[14] == "relative") {
                _posi = "";
                if (!_M[4])
                    _wid = "width:1px;";
                _top = "";
                _left = ""
            }
        }
        _padd = "";
        if (_M[12])
            _padd = "padding:" + _M[12] + "px;";
        _wid = "";
        _cls = "mmenu";
        if (_MS.offclass)
            _cls = _MS.offclass;
        if (_posi)
            _posi = "position:" + _posi;
        _mnwid = "";
        if (_M[17])
            _mnwid = "width=" + _M[17];
        if (_begn == 1) {
            if (!op6 && _mnwid && !ns7)
                _wid = ";width:" + _M[17] + ";";
            _mt += "<div class=" + _cls + " onselectstart=\"return _f\" " + _ns6ev + " onmouseout=\"_CAMs()\" onmouseover=\"$CtI(_MT);\" id=menu" + _mnu + " style=\"" + _padd + _ofb + ";" + _brd + _wid + "z-index:499;visibility:" + _visi + ";" + _posi + ";" + _top + ";" + _left + _bgimg + "\">"
        }
        _mali = "";
        if (_M[20])
            _mali = "align=" + _M[20];
        if (_M[7] || !_startM || (op5 || op6) || mac) {
            if (mac)
                _mnwid = " width=1 ";
            if (_mnwid)
                _mt += "<table border=0 cellpadding=0 cellspacing=0 " + _mnwid + " style=\"" + _ofb + "\"><td " + _mnwid + " " + _mali + ">";
            _mt += "<table border=0 cellpadding=0 cellspacing=0 " + _mnuHeight + " " + _tablewidth + " id=tbl" + _mnu + ">";
            for (_b = 0; _b < _M[0].length; _b++) {
                _mt += _getItem(_b, _M);
                _el++
            }
            if (mac && !horiz)
                _mt += "<tr><td id=btm" + _mnu + "></td></tr>";
            _mt += "</table>";
            if (_mnwid)
                _mt += "</td></tr></table>"
        } else {
            if (_begn == 1)
                for (_b = 0; _b < _M[0].length; _b++) {
                _getCurrentPage();
                _el++
            }
        }
        if (_begn == 1)
            _mt += "</div>";
        if (_begn == 1)
            _d.write(_mt);
        else
            return _mt;
        if (_M[7])
            _M[22] = gmobj("menu" + _mnu);
        if (_M[7]) {
            if (ie55)
                drawiF(_mnu)
            } else {
            if (ie55 && _ifc < _mD)
                drawiF(_mnu);
            _ifc++
        }
        if (_M[19]) {
            _M[19] = _M[19].toString();
            _fs = _M[19].split(",");
            if (!_fs[1])
                _fs[1] = 50;
            if (!_fs[2])
                _fs[2] = 2;
            _M[19] = _fs[0];
            followScroll(_mnu, _fs[1], _fs[2])
            }
        if (_mnu == _m.length - 1) {
            $CtI(_mst);
            _mst = null;
            _mst = setTimeout("_MScan()", 150);
            _getCurPath()
            }
    }
    function _getCurPath() {
        _cmp = new Array();
        if (_cip.length > 0) {
            for (_c = 0; _c < _cip.length; _c++) {
                _ci = _cip[_c];
                _mni = getParentItemByItem(_ci);
                if (_mni == -1)
                    _mni = _ci;
                if (_mni + " " != "undefined ") {
                    while (_mni != -1) {
                        _I = _mi[_mni];
                        _setCPage(_I);
                        itemOff(_mni);
                        _cmp[_cmp.length] = _mni;
                        _mni = getParentItemByItem(_mni);
                        if (_mni + " " == "undefined ")
                            _mni = -1
                    }
                }
            }
        }
    }
    function _setPosition(_mnu) {
        if (_m[_mnu][5]) {
            _gm = gmobj("menu" + _mnu);
            _gp = gpos(_gm);
            _osl = 0;
            _omnu3 = 0;
            if (isNaN(_m[_mnu][3]) && _m[_mnu][3].indexOf("offset=") == 0) {
                _omnu3 = _m[_mnu][3];
                _m[_mnu][3] = _n;
                _osl = _omnu3.substr(7, 99);
                _gm.leftOffset = _osl
            }
            _lft = _n;
            if (!_m[_mnu][3]) {
                if (_m[_mnu][5].indexOf("left") != -1)
                    _lft = 0;
                if (_m[_mnu][5].indexOf("center") != -1)
                    _lft = (_bW / 2) - (_gp[3] / 2);
                if (_m[_mnu][5].indexOf("right") != -1)
                    _lft = _bW - _gp[3];
                if (_gm.leftOffset)
                    _lft = _lft + parseInt(_gm.leftOffset)
                }
            _ost = 0;
            _omnu2 = 0;
            if (isNaN(_m[_mnu][2]) && _m[_mnu][2].indexOf("offset=") == 0) {
                _omnu2 = _m[_mnu][2];
                _m[_mnu][2] = _n;
                _ost = _omnu2.substr(7, 99);
                _gm.topOffset = _ost
            }
            _tp = _n;
            if (!_m[_mnu][2] >= 0) {
                _tp = _n;
                if (_m[_mnu][5].indexOf("top") != -1)
                    _tp = 0;
                if (_m[_mnu][5].indexOf("middle") != -1)
                    _tp = (_bH / 2) - (_gp[2] / 2);
                if (_m[_mnu][5].indexOf("bottom") != -1)
                    _tp = _bH - _gp[2];
                if (_gm.topOffset)
                    _tp = _tp + parseInt(_gm.topOffset)
                }
            spos(_gm, _tp, _lft);
            if (_m[_mnu][19])
                _m[_mnu][19] = _tp;
            if (_m[_mnu][7])
                _SoT(_mnu, 1);
            _gm._tp = _tp
        }
    }
    function followScroll(_mnu, _cycles, _rate) {
        if (!_startM) {
            _M = _m[_mnu];
            _fogm = _M[22];
            _fgp = gpos(_fogm);
            if (_sT > _M[2] - _M[19])
                _tt = _sT - (_sT - _M[19]);
            else
                _tt = _M[2] - _sT;
            if ((_fgp[0] - _sT) != _tt) {
                diff = _sT + _tt;
                if (diff - _fgp[0] < 1)
                    _rcor = _rate;
                else
                    _rcor = -_rate;
                _nv = parseInt((diff - _rcor - _fgp[0]) / _rate);
                if (_nv != 0)
                    diff = _fgp[0] + _nv;
                spos(_fogm, diff);
                if (_fgp._tp)
                    _M[19] = _fgp._tp;
                if (ie55) {
                    _fogm = gmobj("ifM" + _mnu);
                    if (_fogm)
                        spos(_fogm, diff)
                    }
            }
        }
        _fS = setTimeout("followScroll(\"" + _mnu + "\"," + _cycles + "," + _rate + ")", _cycles)
        }
    function _MScan() {
        _getDims();
        if (_bH != _oldbH || _bW != _oldbW) {
            for (_a = 0; _a < _m.length; _a++) {
                if (_m[_a][7]) {
                    if (_startM && (ie4 || _m[_a][14] == "relative")) {
                        _fixMenu(_a)
                        }
                    menuDisplay(_a, 1)
                    }
            }
            for (_a = 0; _a < _m.length; _a++) {
                if (_m[_a][5]) {
                    _setPosition(_a)
                    }
            }
        }
        if (_startM)
            _mnuD = 0;
        _startM = 0;
        _oldbH = _bH;
        _oldbW = _bW;
        if (!op && _d.all && _d.readyState != "complete") {
            _oldbH = 0;
            _oldbW = 0
        }
        if (op) {}
        _mst = setTimeout("_MScan()", 150)
        }
    function drawiF(_mnu) {
        _gm = gmobj("menu" + _mnu);
        if (!_gm)
            return;
        _gp = gpos(_gm);
        _ssrc = "";
        if (location.protocol == "https:")
            _ssrc = "src=/blank.html";
        if (_m[_mnu][7]) {
            _mnuV = "ifM" + _mnu
        } else {
            _mnuV = "iF" + _mnuD;
            _mnuD++
        }
        if (!window._CFix)
            _d.write("<iframe class=mmenu FRAMEBORDER=0 id=" + _mnuV + " " + _ssrc + " style=\"filter:Alpha(Opacity=0);visibility:hidden;position:absolute;top:" + _gp[0] + "px;left:" + _gp[1] + "px;height:" + _gp[2] + "px;width:" + _gp[3] + "px;\"></iframe>")
        }
    function _SoT(_mnu, _on) {
        if (_m[_mnu][14] == "relative")
            return;
        if (ns6)
            return;
        if (ie55) {
            if (_on) {
                if (!_m[_mnu][7]) {
                    _iF = gmobj("iF" + _mnuD);
                    if (!_iF) {
                        if (_d.readyState != "complete")
                            return;
                        _iF = _d.createElement("iframe");
                        if (location.protocol == "https:")
                            _iF.src = "/blank.html";
                        _iF.id = "iF" + _mnuD;
                        _iF.style.filter = "Alpha(Opacity=0)";
                        _iF.style.position = "absolute";
                        _iF.style.className = "mmenu";
                        _d.body.appendChild(_iF)
                        }
                } else {
                    _iF = gmobj("ifM" + _mnu)
                    }
                _gp = gpos(_m[_mnu][22]);
                if (_iF) {
                    spos(_iF, _gp[0], _gp[1], _gp[2], _gp[3]);
                    _iF.style.visibility = "visible";
                    _iF.style.zIndex = 899
                }
            } else {
                _gm = gmobj("iF" + (_mnuD - 1));
                if (_gm)
                    _gm.style.visibility = "hidden"
            }
        }
    }
}
function CloseCalWindow() {
    try {
        if (calendar_window && !calendar_window.closed)
            calendar_window.close();
    } catch(e) {}
}
function ChangeRollOver(IdofControl) {
    try {
        if (IdofControl != '') {
            var BControl = document.getElementById(IdofControl);
            if (BControl)
                document.getElementById(IdofControl).src = BControl.src.substring(0, BControl.src.length - 5) + '2.jpg';
        }
    } catch(e) {}
}
function ChangeRollOut(IdofControl) {
    try {
        if (IdofControl != '') {
            var BControl = document.getElementById(IdofControl);
            if (BControl)
                document.getElementById(IdofControl).src = BControl.src.substring(0, BControl.src.length - 5) + '1.jpg';
        }
    } catch(e) {}
}
function openhelp(filen) {
    try {
        helpwinop = window.open(filen, name, "fullscreen=no,toolbar=no,status=no,menubar=no,scrollbars=Yes,resizable=no,directories=no,location=no,width= 700,height=500,left=20,top=20");
    } catch(e) {}
}
function select_deselectAll(chkVal, idVal) {
    var frm = document.forms[0];
    for (i = 0; i < frm.length; i++) {
        if (frm.elements[i].id.indexOf('Chk_All') != -1)
            tempfrmnm = frm.elements[i].id;
        if (idVal.indexOf('Chk_All') != -1) {
            if (chkVal == true) {
                frm.elements[i].checked = true;
            } else {
                frm.elements[i].checked = false;
            }
        } else if (idVal.indexOf('Chk_Item') != -1) {
            if (frm.elements[i].checked == false) {
                frm.elements[1].checked = false;
                if (tempfrmnm != null)
                    document.getElementById(tempfrmnm).checked = false;
            }
        }
    }
    for (i = 0; i < frm.length; i++) {
        if (frm.elements[i].id.indexOf('Chk_Item') != -1) {
            if (frm.elements[i].checked == false) {
                tmpbool = false;
                break;
            } else if (frm.elements[i].checked == true) {
                tmpbool = true;
            }
        }
    }
    if (tmpbool == true) {
        document.getElementById(tempfrmnm).checked = true;
    }
}
function closehelp() {
    try {
        if (helpwinop && !helpwinop.closed)
            helpwinop.close();
    } catch(e) {}
}

function OpenWin(_Url, _trg) {
    //_mnu_url=_Url;
    var now = new Date();
    if (_Url.indexOf("?") != -1)
        _Url = _Url + "&ses=" + now.getTime();
    else
        _Url = _Url + "?ses=" + now.getTime();

    closeAllMenus();
    onMenuClick(_Url);
}