//JavaScript Document

$(function () {
    //点击卡片切换样式
    $(".tool a").each(function (index) {
        $(this).click(function () {
            $(".tool a").removeClass("active");
            $(this).addClass("active");
            showTypes = this.id.substr(this.id.lastIndexOf("_") + 1, 1);
            ChartMain(showTypes);
        });
    });

    ChartMain(0);
});

//报表统计方法
function ChartMain(thatDay) {
    //应用id
    var appid = $("#s_applist").val();
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: "/REPORT/BuildChart?days=" + thatDay + "&appid=" + appid,
        success: function (retJson) {
            if (retJson == "0") {
                var childHtm = '<div style="font-size:14px;color:#5e788a;font-weight:lighter;text-align:center;width:100%;">未查询到符合条件的数据</div>';
                $("#chart").html(childHtm);
            } else {
                var data = eval('(' + retJson + ')');
                var chart = new FusionCharts({
                    type: "msline",
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