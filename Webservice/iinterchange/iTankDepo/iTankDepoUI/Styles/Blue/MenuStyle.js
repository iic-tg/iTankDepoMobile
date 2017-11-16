_menuCloseDelay = 500
_menuOpenDelay = 150
_followSpeed = 5
_followRate = 50
_subOffsetTop = 5
_subOffsetLeft = 0
_scrollAmount = 3
_scrollDelay = 20
retainClickValue = true;

if (!_pageabspath)
    var _pageabspath = '';
var _mnu_url = '';
var _CurrentMenu;



with (mBaseStyle = new mm_style()) {

    onbgcolor = "#336699";
    oncolor = "yellow";
    offbgcolor = "#336699";
    offcolor = "#ffffff";
    bordercolor = "#000000";
    borderstyle = "none";
    borderwidth = 0;
    horseparatorclass = "horsepclass";
    verseparatorclass = "versepclass";
    separatorcolor = "#000000";
    separatorsize = "0";
    padding = 4;
    fontsize = "8pt";
    fontfamily = "Verdana,Arial, Tahoma ";
    subimagepadding = "1";
    overfilter = "";
    outfilter = "randomdissolve(duration=0.0)";
}

menuTStyle = new copyOf(mBaseStyle);

menuStyle = new copyOf(mBaseStyle);
menuStyle.onbgcolor = "#d9e9b6";
menuStyle.oncolor = "#000000";
menuStyle.offbgcolor = "#dbdae0";
menuStyle.offcolor = "black";
menuStyle.bordercolor = "#336699";
menuStyle.borderstyle = "solid";
menuStyle.borderwidth = 1;
menuStyle.separatorcolor = "#336699";
menuStyle.separatorsize = "1";
menuStyle.padding = 4;
menuStyle.overfilter = "";
menuStyle.outfilter = "randomdissolve(duration=0.0)";
menuStyle.subimage = _pageabspath + "Images/menu_arrow.png";
