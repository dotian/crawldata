var chart;
var chartData = [];

//负面话题趋势(复合) 折线图

AmCharts.ready(function () {
    var jsondata = document.getElementById("txt_columData_fmhtqsb").value;
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
    var valueAxis11 = new AmCharts.ValueAxis();
    valueAxis11.axisColor = "#FF6600";
    valueAxis11.axisThickness = 2;
    valueAxis11.gridAlpha = 0;
    chart.addValueAxis(valueAxis11);

    // second value axis (on the right)
    var valueAxis12 = new AmCharts.ValueAxis();
    valueAxis12.position = "right"; // this line makes the axis to appear on the right
    valueAxis12.axisColor = "#FCD202";
    valueAxis12.gridAlpha = 0;
    valueAxis12.axisThickness = 2;
    chart.addValueAxis(valueAxis12);

    // third value axis (on the left, detached)
    valueAxis13 = new AmCharts.ValueAxis();
    valueAxis13.offset = 50; // this line makes the axis to appear detached from plot area
    valueAxis13.gridAlpha = 0;
    valueAxis13.axisColor = "#adc5d9";
    valueAxis13.axisThickness = 2;
    chart.addValueAxis(valueAxis13);

    // third value axis (on the left, detached)
    valueAxis14 = new AmCharts.ValueAxis();
    valueAxis14.offset = 100; // this line makes the axis to appear detached from plot area
    valueAxis14.gridAlpha = 0;
    valueAxis14.axisColor = "#99FFCC";
    valueAxis14.axisThickness = 2;
    chart.addValueAxis(valueAxis14);



    // GRAPHS
    // first graph
    var graph11 = new AmCharts.AmGraph();
    graph11.valueAxis = valueAxis11; // we have to indicate which value axis should be used
    graph11.title = "新闻";
    graph11.valueField = "News_F_Num";
    graph11.bullet = "round";
    graph11.hideBulletsCount = 30;
    graph11.bulletBorderThickness = 1;
    chart.addGraph(graph11);

    // second graph
    var graph12 = new AmCharts.AmGraph();
    graph12.valueAxis = valueAxis12; // we have to indicate which value axis should be used
    graph12.title = "博客";
    graph12.valueField = "Blog_F_Num";
    graph12.bullet = "square";
    graph12.hideBulletsCount = 30;
    graph12.bulletBorderThickness = 1;
    chart.addGraph(graph12);

    // third graph
    var graph13 = new AmCharts.AmGraph();
    graph13.valueAxis = valueAxis13; // we have to indicate which value axis should be used
    graph13.valueField = "Forum_F_Num";
    graph13.lineColor = "#adc5d9";
    graph13.title = "论坛";
    graph13.bullet = "triangleUp";
    graph13.hideBulletsCount = 30;
    graph13.bulletBorderThickness = 1;
    chart.addGraph(graph13);

    // third graph
    var graph14 = new AmCharts.AmGraph();
    graph14.valueAxis = valueAxis14; // we have to indicate which value axis should be used
    graph14.valueField = "Microblog_F_Num";
    graph14.title = "微博";
    graph14.bullet = "diamond";
    graph14.hideBulletsCount = 30;
    graph14.bulletBorderThickness = 1;
    chart.addGraph(graph14);

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
    chart.write("chartdiv_fmhtqst");
});



