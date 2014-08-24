var chart;
var chartData = [];

AmCharts.ready(function () {
    // generate some random data first
    var jsondata = document.getElementById("txt_columData_zmhtqsb").value;
    chartData = eval(jsondata);
  
    // SERIAL CHART
    chart = new AmCharts.AmSerialChart();
    chart.pathToImages = "Report/images/";
    chart.dataProvider = chartData;
    chart.categoryField = "ContentDate";

    // listen for "dataUpdated" event (fired when chart is inited) and call zoomChart method when it happens
   // chart.addListener("dataUpdated", zoomChart);

    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    categoryAxis.parseDates = true; // as our data is date-based, we set parseDates to true
    categoryAxis.minPeriod = "DD"; // our data is daily, so we set minPeriod to DD
    categoryAxis.minorGridEnabled = true;
    categoryAxis.axisColor = "#DADADA";

    // first value axis (on the left)
    var valueAxis1 = new AmCharts.ValueAxis();
    valueAxis1.axisColor = "#FF6600";
    valueAxis1.axisThickness = 2;
    valueAxis1.gridAlpha = 0;
    chart.addValueAxis(valueAxis1);

    // second value axis (on the right)
    var valueAxis2 = new AmCharts.ValueAxis();
    valueAxis2.position = "right"; // this line makes the axis to appear on the right
    valueAxis2.axisColor = "#FCD202";
    valueAxis2.gridAlpha = 0;
    valueAxis2.axisThickness = 2;
    chart.addValueAxis(valueAxis2);

    // third value axis (on the left, detached)
    valueAxis3 = new AmCharts.ValueAxis();
    valueAxis3.offset = 50; // this line makes the axis to appear detached from plot area
    valueAxis3.gridAlpha = 0;
    valueAxis3.axisColor = "#B0DE09";
    valueAxis3.axisThickness = 2;
    chart.addValueAxis(valueAxis3);

    // GRAPHS
    // first graph
    var graph1 = new AmCharts.AmGraph();
    graph1.valueAxis = valueAxis1; // we have to indicate which value axis should be used
    graph1.title = "新闻";
    graph1.valueField = "News_Z_Num";
    graph1.bullet = "round";
    graph1.hideBulletsCount = 30;
    graph1.bulletBorderThickness = 1;
    chart.addGraph(graph1);

    // second graph
    var graph2 = new AmCharts.AmGraph();
    graph2.valueAxis = valueAxis2; // we have to indicate which value axis should be used
    graph2.title = "博客";
    graph2.valueField = "Blog_Z_Num";
    graph2.bullet = "square";
    graph2.hideBulletsCount = 30;
    graph2.bulletBorderThickness = 1;
    chart.addGraph(graph2);

    // third graph
    var graph3 = new AmCharts.AmGraph();
    graph3.valueAxis = valueAxis3; // we have to indicate which value axis should be used
    graph3.valueField = "Forum_Z_Num";
    graph3.title = "论坛";
    graph3.bullet = "triangleUp";
    graph3.hideBulletsCount = 30;
    graph3.bulletBorderThickness = 1;
    chart.addGraph(graph3);

    // CURSOR
    var chartCursor = new AmCharts.ChartCursor();
    chartCursor.cursorPosition = "mouse";
    chart.addChartCursor(chartCursor);

    // SCROLLBAR
    var chartScrollbar = new AmCharts.ChartScrollbar();
    chart.addChartScrollbar(chartScrollbar);

    // LEGEND
    var legend = new AmCharts.AmLegend();
    legend.marginLeft = 110;
    legend.useGraphSettings = true;
    chart.addLegend(legend);

    // WRITE
    chart.write("chartdiv_zmhtqst");
});

