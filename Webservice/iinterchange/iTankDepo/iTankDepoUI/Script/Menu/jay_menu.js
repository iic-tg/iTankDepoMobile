
with (CCC = new menuname("MainMenu")) {
    style = menuTStyle;
    top = 23;
    left = 125;
    orientation = "horizontal";
    alwaysvisible = 1;
    aI("text=Admin;showmenu=Admin;status=Admin");
    aI("text=|;itemwidth=3");
    aI("text=Master;showmenu=Master;status=Master");
    aI("text=|;itemwidth=3");
    aI("text=Operations;showmenu=Operations;status=Operations");
    aI("text=|;itemwidth=3");
    aI("text=Tracking;showmenu=Tracking;status=Tracking");
    aI("text=|;itemwidth=3");
    aI("text=Billing;showmenu=Billing;status=Billing");
    aI("text=|;itemwidth=3");
    aI("text=Reports;showmenu=Reports;status=Reports");
}
with (CCC = new menuname("Admin")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[2]);
    aI(Activities[1]);
    aI(Activities[26]);
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
    aI(Activities[66]);
}
with (CCC = new menuname("Operations")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[17]);
    aI("text=Inward Pass;showmenu=Inward Pass;status=Inward Pass");
    aI(Activities[27]);
    aI("text=Gate In;showmenu=Gate In;status=Gate In");
    aI("text=Repair Estimate;showmenu=Repair Estimate;status=Repair Estimate");
    aI("text=Lessee Repair Approval;showmenu=Lessee Repair Approval;status=Lessee Repair Approval");
    aI("text=Owner Repair Approval;showmenu=Owner Repair Approval;status=Owner Repair Approval");
    aI(Activities[50]);
    aI("text=Repair ReEstimate;showmenu=Repair ReEstimate;status=Repair ReEstimate");
    aI(Activities[49]);
    aI(Activities[52]);
    aI(Activities[53]);
    aI("text=Outward Pass;showmenu=Outward Pass;status=Outward Pass");
    aI(Activities[56]);
    aI(Activities[65]);

}
with (CCC = new menuname("Tracking")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[38]);
}
with (CCC = new menuname("Billing")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[36]);
    aI(Activities[37]);
}
with (CCC = new menuname("Reports")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[30]);
    aI(Activities[31]);
    aI(Activities[32]);
    aI(Activities[57]);
    aI(Activities[58]);
    aI(Activities[59]);
    aI(Activities[60]);
    aI(Activities[61]);
    aI(Activities[28]);
    aI(Activities[62]);
    aI(Activities[33]);
    aI(Activities[63]);
    aI(Activities[64]);
}
with (CCC = new menuname("Inward Pass")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[24]);
    aI(Activities[25]);
}
with (CCC = new menuname("Gate In")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[29]);
    aI(Activities[34]);
    aI(Activities[35]);
}
with (CCC = new menuname("Repair Estimate")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[39]);
    aI(Activities[40]);
}
with (CCC = new menuname("Lessee Repair Approval")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[41]);
    aI(Activities[42]);
}
with (CCC = new menuname("Owner Repair Approval")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[43]);
    aI(Activities[44]);
}
with (CCC = new menuname("Repair ReEstimate")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[45]);
    aI(Activities[46]);
    aI(Activities[47]);
    aI(Activities[48]);
    aI(Activities[51]);
}
with (CCC = new menuname("Outward Pass")) {
    style = menuStyle;
    overflow = "scroll";
    aI(Activities[54]);
    aI(Activities[55]);
}

drawMenus();
