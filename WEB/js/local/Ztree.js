var setting = {
    check: {
        enable: true,
        chkStyle: "radio",
        radioType: "all"
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {
        onClick: zTreeOnClick,
        onCheck: zTreeOnCheck
    }

};
function setCheck() {
    $.fn.zTree.init($("#treeDemo"), setting, zNodes);
}
function zTreeOnClick(event, treeId, treeNode) {
   // alert(treeNode.tId + ", " + treeNode.name);
};
function zTreeOnCheck(event, treeId, treeNode) {
   // alert(treeNode.pId);
    if (treeNode.pId != 0 && treeNode.pId!=null)
    {
        document.getElementById("Voids").style = "display:block";
    }
    else 
    {
        document.getElementById("Voids").style = "display:none";
    }
};
