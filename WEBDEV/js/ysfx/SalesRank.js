//JavaScript Document

var sumTypes = 0;
//top10
function ChartTopTen(type) {
    var stime = $("#stime").val();
    var etime = $("#etime").val();
    var apid = $("#s_applist").val();
    var queryStr = "begin=" + stime + "&end=" + etime + "&sumType=" + sumTypes + "&appid=" + apid;
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: "/RevenueAnalysis/SalesTopTen?" + queryStr,
        success: function (retJson) {
            if (retJson == "0") {
                var childHtm = '<div style="font-size:14px;color:#5e788a;font-weight:lighter;text-align:center;width:100%;">未查询到符合条件的数据</div>';
                $("#chart").html(childHtm);
            } else {
                var data = eval('(' + retJson + ')');
                var chart = new FusionCharts({
                    type: "bar2d",
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

$(function () {
    //点击卡片切换样式
    $(".tab-wrap a").each(function (index) {
        $(this).click(function () {
            $(".tab-wrap a").removeClass("active");
            $(this).addClass("active");
            sumTypes = this.id.substr(this.id.lastIndexOf("_") + 1, 1);
            ChartTopTen();
        });
    });

    //查询按钮
    $("#btn_search").click(function () {
        ChartTopTen();
    });

    ChartTopTen();
});