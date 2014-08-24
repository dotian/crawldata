
var chart;
var legend;
AmCharts.ready(function () {
    var jsonData = document.getElementById("txt_columData_gdxhtsl").value;
    var chartData = eval(jsonData);
    // PIE CHART
    chart = new AmCharts.AmPieChart();
    chart.dataProvider = chartData;
    chart.titleField = "from";
    chart.valueField = "value";
    chart.colorField = "color";
    chart.outlineColor = "#FFFFFF";
    chart.outlineAlpha = 0.8;
    chart.outlineThickness = 2;
    chart.balloonText = "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>";
    // this makes the chart 3D
    chart.depth3D = 15;
    chart.angle = 30;
    // WRITE
    chart.write("chartdiv_gdxhtsl");
});

