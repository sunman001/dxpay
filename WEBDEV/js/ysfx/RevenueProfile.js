//JavaScript Document

//统计维度
var showTypes = 0;
//统计类型
var sumTypes = 0;

$(function () {
    //点击不同统计维度切换样式
    $(".tool a").each(function (index) {
        $(this).click(function () {
            $(".tool a").removeClass("active");
            $(this).addClass("active");
            showTypes = this.id.substr(this.id.lastIndexOf("_") + 1, 1);
            ChartMain();
        });
    });

    //点击卡片切换样式
    $(".tab-wrap a").each(function (index) {
        $(this).click(function () {
            $(".tab-wrap a").removeClass("active");
            $(this).addClass("active");
            sumTypes = this.id.substr(this.id.lastIndexOf("_") + 1, 1);
            ChartMain();
        });
    });

    //查询按钮
    $("#btn_search").click(function () {
        ChartMain();
    });

    ChartMain();
});

//营收概况
function ChartMain() {
    var start = $("#stime").val();
    var ends = $("#etime").val();
   // var trType = $("#s_state").val();
    var apid = $("#s_applist").val();
    var queryParams = "showType=" + showTypes + "&sumType=" + sumTypes + "&begin=" + start + "&end=" + ends + "&appid=" + apid;
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: "/RevenueAnalysis/TradeMain?" + queryParams,
        success: function (retJson) {
            if (retJson == "0") {
                var childHtm = '<div style="font-size:14px;color:#5e788a;font-weight:lighter;text-align:center;width:100%;">未查询到符合条件的数据</div>';
                $("#chart").html(childHtm);
            } else {
                var data = eval('(' + retJson + ')');
                var chart = new FusionCharts({
                    type: "zoomline",
                    renderAt: "chart",
                    width: "100%",
                    height: "550px",
                    dataSource: data,
                    dataFormat: "json"
                }).render();
            }
        }
    });
}