var iGateIns, iRepairCompletions, iGateOuts = new Array();

function setOperationTrendGraph() {
    el("divOperationTrend").innerHTML = "";
    var oCallback = new Callback();
    oCallback.add("CustomerID", el("lkpCustomerCode").SelectedValues[0]);
    oCallback.invoke("OperationsTurnOver.aspx", "SetTrend");
    if (oCallback.getCallbackStatus()) {
        eval(oCallback.getReturnValue("GraphData"));
        showDiv("divOperationTrend");
        hideDiv("divMessage");       
        var plot2 = $.jqplot('divOperationTrend', [iGateIns, iRepairCompletions, iGateOuts],
		{
		    seriesColors: ["#4bb2c5", "#c5b47f", "#EAA228", "#579575", "#839557", "#958c12", "#953579", "#4b5de4", "#d8b83f", "#ff5800", "#0085cc"],
		    title: 'Operation-Trends',
		    cursor: { show: true, zoom: true, showTooltip: true },
		    axesDefaults: { labelRenderer: $.jqplot.CanvasAxisLabelRenderer },
		    seriesDefaults: { rendererOptions: { smooth: false },
                showMarker:true,
		        pointLabels: { show: true }
		    },
		    axes: {
		        xaxis: { renderer: $.jqplot.DateAxisRenderer, tickOptions: { formatString: '%b-%y' }, label: "Month Year", pad: 0 },
		        yaxis: { label: "No of Equipment", pad: 0,min: 0 }
		    }
		});
        showDiv("divGraphLegend");
    }
    else {
        showDiv("divMessage");
        hideDiv("divOperationTrend");
    }

    oCallback = null;
}
