var indexUtil = {
    InitChart: function () {

        var stime = $.trim($("#stime").val());
        var etime = $.trim($("#etime").val());
        var a_name = $.trim($("#s_applist option:selected").text());

        var mychart = {
            chart: {
                type: 'line',
                renderTo: "chart"
            },
            title: {
                text: ''
            },
            xAxis: {
                categories: []
            },
            yAxis: [{ // Primary yAxis
                labels: {
                    format: '',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                },
                title: {
                    text: '',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                }
            }, { // Secondary yAxis
                title: {
                    text: '人数',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                labels: {
                    format: '{value} ',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                opposite: true
            }],
            tooltip: {
                formatter: function () {
                    return ' <span style="color:' + this.point.series.color + '">●</span> ' + this.point.series.name + '：' + this.point.y;
                }
            },
            series: [
           {
               type: 'spline',
               name: '每日新增用户',
               yAxis: 1,
               data: [],
               marker: {
                   lineWidth: 2,
                   lineColor: Highcharts.getOptions().colors[3]
               },
               tooltip: {
                   valueSuffix: ' 人'
               }
           }, {
               type: 'spline',
               name: '每日活跃用户',
               yAxis: 1,
               data: [],
               marker: {
                   lineWidth: 2,
                   lineColor: Highcharts.getOptions().colors[3]
               },
               tooltip: {
                   valueSuffix: ' 人'
               }
           }
            ],
            credits: {
                enabled: false //右下角不显示highcharts的LOGO
            }
        };

        var url = "/special/TrendsCount";
        var data = { stime: stime, etime: etime, a_name: a_name };

        $.post(url, data, function (data) {

            var seriesData1 = new Array();
            var seriesData2 = new Array();
            var categoriesData = new Array();

            if (data.Data.Table1.length > 0) {
                for (var i = 0; i < data.Data.Table1.length; i++) {
                    categoriesData.push(data.Data.Table1[i].t_time);
                    seriesData1.push(data.Data.Table1[i].t_newcount);
                    seriesData2.push(data.Data.Table1[i].t_activecount);
                }

            }

            mychart.series[0].data = seriesData1;
            mychart.series[1].data = seriesData2;

            mychart.xAxis.categories = categoriesData;

            new Highcharts.Chart(mychart);
        });

    }

}

//判断是否是手机
function ToggleMenu() {


    //var _width = window.screen.width;

    //if (_width < 767) {
    //    indexUtil.InitChartMoblie();
    //} else {
    //    indexUtil.InitChart();
    //}

    indexUtil.InitChart();
}

$(function () {

    ToggleMenu();

    $("#btn_search").click(function () {
        ToggleMenu();
    })
}
);