// JavaScript Document

function ArticleManage(pageIndex, pageSize) {
    var url = "/Financial/BillList?curr=" + pageIndex + "&psize=" + pageSize;

    var stime = $("#stime").val();
    var etime = $("#etime").val();

    url += "&stime=" + stime + "&etime=" + etime;
    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//查询
function SerachBillList() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//跳转营收列表
function ysBillList(type) {

    var name = "营收列表";
    var isLeaf = true;//是否套用
    var id = $(this).attr("data-id");//id
    var href = "/Report/AppReport?time=" + type;//链接
    AddTab(name, isLeaf, href, 'AppReport' + id, 'child');
}

//提现
function btnWithdrawals()
{
    var WithdrawalsStart = $("#WithdrawalsStart").val();
    if (WithdrawalsStart > 0) {

        var valArr = new Array;
        $("#table :checkbox[checked]").each(function (i) {
            valArr[i] = $(this).val();
        });
        var vals = valArr.join(',');
        if (vals == "") {
            window.parent.ShowMsg("请选择提现账单数据！", "error", "");
            return false;
        }

        if (WithdrawalsStart == 1)
        {
            window.parent.ShouwDiaLogWan("单卡提款", 900, 400, "/Financial/pays_single?payid=" + vals);
        }
        else if (WithdrawalsStart == 2)
        {
            window.parent.ShouwDiaLogWan("多卡提款", 900, 360, "/Financial/pays?payid=" + vals);
        }


    }
    else {
        window.parent.ShowMsg("请选择提现方式！", "error", "");
        return false;
    }
}
