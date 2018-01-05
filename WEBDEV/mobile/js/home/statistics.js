var indexUtil = {
    InitChart: function (type) {

        var mychart = {
            chart: {
                type: 'line',
                renderTo: "PriceChartDev"
            },
            title: {
                text: ''
            },
            xAxis: {
                categories: []
            },
            yAxis: [{ // Primary yAxis
                labels: {
                    format: '{value}笔',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                },
                title: {
                    text: '笔数',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                }
            }, { // Secondary yAxis
                title: {
                    text: '金额',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                labels: {
                    format: '{value} 元',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                opposite: true
            }],
            tooltip: {
                formatter: function () {
                    return this.point.x + '点 <span style="color:' + this.point.series.color + '">●</span> ' + this.point.series.name + '：' + this.point.y;
                }
            },
            series: [{
                type: 'spline',
                name: '交易金额',
                yAxis: 1,
                data: [],
                marker: {
                    lineWidth: 2,
                    lineColor: Highcharts.getOptions().colors[3]
                },
                tooltip: {
                    valueSuffix: ' 元'
                }
            },

             {
                 type: 'column',
                 name: '成功交易笔数',
                 data: [],
                 tooltip: {
                     valueSuffix: ' 笔'
                 }

             },
        {
            type: 'column',
            name: '前三日平均成功量',
            data: [],
            tooltip: {
                valueSuffix: ' 笔'
            }
        }
            ],
            credits: {
                enabled: false //右下角不显示highcharts的LOGO
            }
        };

        var url = "/Home/HomeCharts";
        var data = { days: type };

        $.post(url, data, function (data) {

            var seriesData1 = new Array();
            var seriesData2 = new Array();
            var seriesData3 = new Array();

            var categoriesData = new Array();
            if (data.Data.ds.length > 0) {
                for (var i = 0; i < data.Data.ds.length; i++) {
                    categoriesData.push(parseInt(data.Data.ds[i].Hours));
                    seriesData1.push(data.Data.ds[i].a_curr);
                    seriesData2.push(data.Data.ds[i].a_success);
                    seriesData3.push(data.Data.ds[i].b_success);

                }

            }

            mychart.series[0].data = seriesData1;
            mychart.series[1].data = seriesData2;
            mychart.series[2].data = seriesData3;

            mychart.xAxis.categories = categoriesData;

            new Highcharts.Chart(mychart);
        });


    }

}

//判断是否是手机
function ToggleMenu(type) {


    //var _width = window.screen.width;

    //if (_width < 767) {
    //    indexUtil.InitChartMoblie();
    //} else {
    //    indexUtil.InitChart();
    //}

    indexUtil.InitChart(type);
}

$(function () {

    ToggleMenu(0);

}
);

//Tab控制函数
function dialogTabJson(tabObj, type) {
    //设置点击后的切换样式
    $("a[name='xzk']").removeClass("active");
    $(tabObj).addClass("active");
    ToggleMenu(type);
}
