
with (CCC = new menuname("MainMenu")) {
    style = menuTStyle;
    top = 23;
    left = 125;
    orientation = "horizontal";
    alwaysvisible = 1;
    aI("text=Master;showmenu=Master;status=Master");
}
with (CCC = new menuname("Master")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[3]);
    aI(Activities[4]);
    aI(Activities[5]);
    aI(Activities[6]);
    aI(Activities[7]);
    aI(Activities[8]);
    aI(Activities[9]);
    aI(Activities[10]);
    aI(Activities[11]);
    aI(Activities[12]);
    aI(Activities[13]);
    aI(Activities[14]);
    aI(Activities[15]);
    aI(Activities[16]);
    aI(Activities[18]);
    aI(Activities[19]);
    aI(Activities[20]);
    aI(Activities[21]);
    aI(Activities[22]);
    aI(Activities[23]);
}

drawMenus();
