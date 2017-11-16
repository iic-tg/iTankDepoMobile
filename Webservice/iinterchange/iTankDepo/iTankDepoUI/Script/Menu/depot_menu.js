
with (CCC = new menuname("MainMenu")) {
    style = menuTStyle;
    top = 23;
    left = 125;
    orientation = "horizontal";
    alwaysvisible = 1;
    aI("text=Admin;showmenu=Admin;status=Admin");
}
with (CCC = new menuname("Admin")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[2]);
    aI(Activities[1]);
}

drawMenus();
