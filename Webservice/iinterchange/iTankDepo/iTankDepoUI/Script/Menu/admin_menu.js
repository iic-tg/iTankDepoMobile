
with(CCC=new menuname("MainMenu")){
	style = menuTStyle;
	top=26;
	left = 200;
	orientation="horizontal";
	alwaysvisible=1;
	aI("text=Operations;showmenu=Operations;status=Operations");
	aI("text=|;itemwidth=3");
	aI("text=Master;showmenu=Master;status=Master");
	aI("text=|;itemwidth=3");
	aI("text=Tracking;showmenu=Tracking;status=Tracking");
	aI("text=|;itemwidth=3");
	aI("text=Billing;showmenu=Billing;status=Billing");
	aI("text=|;itemwidth=3");
	aI("text=EDI;showmenu=EDI;status=EDI");
	aI("text=|;itemwidth=3");
	aI("text=Transportation;showmenu=Transportation;status=Transportation");
	aI("text=|;itemwidth=3");
	aI("text=Rental;showmenu=Rental;status=Rental");
	aI("text=|;itemwidth=3");
	aI("text=Reports;showmenu=Reports;status=Reports");
	aI("text=|;itemwidth=3");
	aI("text=Custom Reports;showmenu=Custom Reports;status=Custom Reports");
	aI("text=|;itemwidth=3");
	aI("text=Admin;showmenu=Admin;status=Admin");
}
with(CCC=new menuname("Operations")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[80]);
	aI("text=Gate In;showmenu=Gate In;status=Gate In");
	aI(Activities[83]);
	aI(Activities[90]);
	aI("text=Cleaning Procedure;showmenu=Cleaning Procedure;status=Cleaning Procedure");
	aI("text=Repair;showmenu=Repair;status=Repair");
	aI(Activities[88]);
	aI(Activities[97]);
	aI(Activities[105]);
	aI(Activities[125]);
	aI(Activities[144]);

}
with(CCC=new menuname("Master")){
	style = menuStyle;
	overflow = "scroll";
	aI("text=Equipment;showmenu=Equipment;status=Equipment");
	aI("text=Repair Process;showmenu=Repair Process;status=Repair Process");
	aI("text=Cleaning Process;showmenu=Cleaning Process;status=Cleaning Process");
	aI("text=Transport;showmenu=Transport;status=Transport");
	aI("text=Billing Process;showmenu=Billing Process;status=Billing Process");
	aI("text=Rental Process;showmenu=Rental Process;status=Rental Process");
	aI(Activities[26]);
	aI(Activities[79]);
	aI(Activities[23]);

}
with(CCC=new menuname("Tracking")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[91]);
	aI(Activities[94]);
}
with(CCC=new menuname("Billing")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[95]);
	aI(Activities[103]);
	aI(Activities[86]);
}
with(CCC=new menuname("EDI")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[146]);
	aI(Activities[147]);
}
with(CCC=new menuname("Transportation")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[102]);
}
with(CCC=new menuname("Rental")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[109]);
}
with(CCC=new menuname("Reports")){
	style = menuStyle;
	overflow = "scroll";
	aI("text=Operation;showmenu=Operation;status=Operation");
	aI("text=Metrics;showmenu=Metrics;status=Metrics");
	aI("text=Finance;showmenu=Finance;status=Finance");

}
with(CCC=new menuname("Custom Reports")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[139]);
	aI(Activities[140]);
	aI(Activities[142]);
	aI(Activities[143]);
	aI(Activities[104]);
}
with(CCC=new menuname("Admin")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[2]);
	aI(Activities[175]);
	aI(Activities[67]);
	aI(Activities[107]);
	aI(Activities[136]);
	aI(Activities[174]);
	aI(Activities[151]);
}
with(CCC=new menuname("Gate In")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[81]);
	aI(Activities[82]);
}
with(CCC=new menuname("Repair")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[170]);
	aI(Activities[171]);
	aI(Activities[87]);
	aI(Activities[92]);
	aI(Activities[93]);
}
with(CCC=new menuname("Equipment")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[11]);
	aI(Activities[10]);
}
with(CCC=new menuname("Repair Process")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[19]);
	aI(Activities[7]);
	aI(Activities[14]);
	aI(Activities[74]);
	aI(Activities[78]);
	aI(Activities[75]);
}
with(CCC=new menuname("Cleaning Process")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[76]);
	aI(Activities[77]);
	aI(Activities[138]);
}
with(CCC=new menuname("Transport")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[13]);
	aI(Activities[96]);
	aI(Activities[135]);
}
with(CCC=new menuname("Billing Process")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[6]);
	aI(Activities[21]);
	aI(Activities[98]);
}
with(CCC=new menuname("Rental Process")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[99]);
}
with(CCC=new menuname("Cleaning Procedure")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[100]);
	aI(Activities[84]);
	aI(Activities[148]);
}
with(CCC=new menuname("Operation")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[101]);
	aI(Activities[108]);
	aI(Activities[111]);
	aI(Activities[110]);
	aI(Activities[112]);
	aI(Activities[113]);
	aI(Activities[114]);
	aI(Activities[115]);
	aI(Activities[149]);
	aI(Activities[116]);
	aI(Activities[117]);
	aI(Activities[118]);
	aI(Activities[141]);
	aI(Activities[173]);
}
with(CCC=new menuname("Metrics")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[126]);
	aI(Activities[127]);
	aI(Activities[128]);
	aI(Activities[129]);
	aI(Activities[132]);
	aI(Activities[130]);
	aI(Activities[133]);
}
with(CCC=new menuname("Finance")){
	style = menuStyle;
	overflow = "scroll";
	aI(Activities[119]);
	aI(Activities[120]);
	aI(Activities[121]);
	aI(Activities[122]);
	aI(Activities[131]);
	aI(Activities[123]);
	aI(Activities[124]);
	aI(Activities[134]);
}

drawMenus();
