//分页
function ArticleManage(pageIndex, pageSize) {

    var PoolName = $("#PoolName").val();
    var searchKey = $("#searchKey").val();
    var IsEnabled = $("#IsEnabled").val();

    var url = "/Risk/ChannelPoolList?pageIndexs=" + pageIndex + "&psize=" + pageSize;

    if (PoolName) {
        url += "&PoolName=" + PoolName;
    }
    if (searchKey) {
        url += "&searchKey=" + searchKey;
    }
    if (IsEnabled) {
        url += "&IsEnabled=" + IsEnabled;
    }

    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//查询
function poolCx() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//添加通道应用池弹窗
function CPoolAdd() {
    window.parent.ShouwDiaLogWan("通道应用池", 650, 400, "/Risk/ChannelPoolAdd");
}

//修改通道池弹窗
function handleEdit(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择通道池！", "error", "");
        return;
    }
    else {
        window.parent.ShouwDiaLogWan("通道应用池", 650, 400, "/Risk/ChannelPoolAdd?id=" + id);
    }
}

//通道池应用配置弹窗
function handleAppMapping(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择通道池！", "error", "");
        return;
    }
    else {
        window.parent.ShouwDiaLogWan("通道池应用配置", 700, 200, "/Risk/ChannelAppMappingAdd?id=" + id);
    }
}

//通道池通道数配置弹窗
function handleAmount(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择通道池！", "error", "");
        return;
    }
    else {
        window.parent.ShouwDiaLogWan("通道池通道数配置", 800, 600, "/Risk/ChannelAmountAdd?id=" + id);
    }
}

//一键启用或禁用
function UpdatePoolState(state, id) {
    var url = "/Risk/PoolStart";
    var data = { state: state, ids: id };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });
        }
        else {
            window.parent.ShowMsg(retJson.msg, "error", "");
            return false;
        }
    });
}

//添加应用通道池方法
function onckPool() {

    var poolid = $("#poolid").val();
    var PoolName = $("#PoolName").val();

    if (isRequestNotNull(PoolName)) {

        $("#PoolNameYz").attr("class", "Validform_checktip Validform_wrong");
        $("#PoolNameYz").html("请填写通道池名称!");
        return false;

    }
    else {

        $("#PoolNameYz").attr("class", "Validform_checktip  Validform_right");
        $("#PoolNameYz").html("验证通过");
    }

    var Description = $("#Description").val();

    var url = "/Risk/PooLAdd";
    var data = { PoolName: $.trim(PoolName), Description: $.trim(Description), Id: $.trim(poolid) };
    $("#AddChannelPool").attr("disabled", "disabled");
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });
        }
        else {
            window.parent.ShowMsg(retJson.msg, "error", "");
            return false;
        }
        $("#AddChannelPool").attr("disabled", false);
    });

}

//验证通道池名称
function PoolNameYz() {
    var PoolName = $("#PoolName").val();

    if (isRequestNotNull(PoolName)) {

        $("#PoolNameYz").attr("class", "Validform_checktip Validform_wrong");
        $("#PoolNameYz").html("请填写通道池名称!");
        return false;

    }
    else {

        $("#PoolNameYz").attr("class", "Validform_checktip  Validform_right");
        $("#PoolNameYz").html("验证通过");
    }

}

//应用弹窗
function apptc() {
    var appid = $.trim($("#appName").val());
    var cid = $.trim($("#cid").val());
    window.parent.ShouwDiaLogWan("选择应用", 1000, 700, "/Risk/AppListTc?appstr=" + appid + "&cid=" + cid);

}

//应用配置
function onckAppMapping() {

    var cid = $("#cid").val();
    var appName = $("#appName").val();

    if (isRequestNotNull(appName)) {
        $("#appNameyy").attr("class", "Validform_checktip Validform_wrong");
        $("#appNameyy").html("请填选择应用!");
        return false;
    }
    else {
        $("#appNameyy").attr("class", "Validform_checktip  Validform_right");
        $("#appNameyy").html("验证通过");
    }

    var url = "/Risk/AppMappingAdd";
    var data = { appId: $.trim(appName), cid: $.trim(cid) };

    $("#AddChannelAppMapping").attr("disabled", "disabled");
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });
        }
        else {
            window.parent.ShowMsg(retJson.msg, "error", "");
            return false;
        }
        $("#AddChannelAppMapping").attr("disabled", false);
    });


}