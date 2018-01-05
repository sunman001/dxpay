var indexUtil = {
    InitChart: function () {

        var mychart = {
            chart: {
                type: 'line',
                renderTo: "PriceChart"
            },
            title: {
                text: ''
            },
            xAxis: {
                categories: []
            },
            yAxis: [{ // Primary yAxis
                labels: {
                    format: '{value}个',
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
                    return this.point.x + '-' + (this.point.x + 1) + '点 <span style="color:' + this.point.series.color + '">●</span> ' + this.point.series.name + '：' + this.point.y;
                }
            },
            series: [
           {
               type: 'spline',
               name: '成功交易笔数',
               yAxis: 1,
               data: [],
               marker: {
                   lineWidth: 2,
                   lineColor: Highcharts.getOptions().colors[3],
                   fillColor: 'white'
               },
               tooltip: {
                   valueSuffix: ' 元'
               }
           }, {
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
           }, {
               type: 'spline',
               name: '前七日平均成功量',
               yAxis: 1,
               data: [],
               marker: {
                   lineWidth: 2,
                   lineColor: Highcharts.getOptions().colors[3]
               },
               tooltip: {
                   valueSuffix: ' 元'
               }
           }
          ],
            credits: {
                enabled: false //右下角不显示highcharts的LOGO
            }
        };

        var url = "/Home/HomeCharts";
        var data = { days:3 };

        $.post(url, data, function (data) {

            var seriesData1 = new Array();
            var seriesData2 = new Array();
            var seriesData3 = new Array();

            var categoriesData = new Array();
            if (data.Data.Table1.length > 0) {
                for (var i = 0; i < data.Data.Table1.length; i++) {
                    categoriesData.push(parseInt(data.Data.Table1[i].Hours));
                    seriesData1.push(data.Data.Table1[i].a_success);
                    seriesData2.push(data.Data.Table1[i].a_curr);
                    seriesData3.push(data.Data.Table1[i].b_success);

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


}
);
