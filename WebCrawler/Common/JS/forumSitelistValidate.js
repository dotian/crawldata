
$(document).ready(function () {
    $("#tb_siteList tr").click(function () {

        $("tr.backRed").removeClass("backRed");
        $(this).addClass("backRed");

        var a = $(this).children();
        var arr = new Array();
        for (var i = 0; i < a.length; i++) {
            arr[arr.length] = a.eq(i).html();
        }

        FiltValue(arr);
    });
});
function FiltValue(arr) {

    var siteid = $.trim(arr[0]);
    var sitename = $.trim(arr[2]);
    var platename = $.trim(arr[3]);

    var siteurl = $.trim(arr[4]);
    siteurl = siteurl.substring(9, siteurl.indexOf("\" target="));
    var siteRank = Trim(arr[1]);
    if (siteid == "站点编号") {

        $("#hid_siteId").val("");
        $("#txt_siteName").val("");
        $("#txt_plateName").val("");
        $("#txt_siteUrl").val("");
        $("#txt_siteRank").val("");

        $("tr.backRed").removeClass("backRed");

    } else {
        $("#hid_siteId").val(siteid);
        $("#txt_siteName").val(sitename);
        $("#txt_plateName").val(platename);
        $("#txt_siteUrl").val(siteurl);
        $("#txt_siteRank").val(siteRank);
    }

}

function addForumSite_click() {
    //进行添加
    var sitename = $("#txt_siteName").val();
    var platename = $("#txt_plateName").val();
    var siteurl = $("#txt_siteUrl").val();
    var siteRank = $("#txt_siteRank").val();

    if (Trim(sitename) == "") {
        alert("站点名称不能为空");
        return;
    }
    if (Trim(platename) == "") {
        alert("板块名称不能为空");
        return;
    }
    if (Trim(siteurl) == "") {
        alert("站点地址不能为空");
        return;
    }
    if (Trim(siteRank) == "") {
        siteRank = 0;
    } else {
        siteRank = parseInt(siteRank);
    }

    InsertSiteList(sitename, platename, siteurl, siteRank);
  
}

function modifyForumSite_click() {
    //更新站点
    //int siteId, string siteName, string plateName, string siteUrl, int siteRank
    var siteId = $("#hid_siteId").val();
    if (Trim(siteId) != '') {
        siteId = parseInt(siteId);
    } else {
        alert("请选择要修改的站点");
        return;
    }

    var sitename = $("#txt_siteName").val();
    var platename = $("#txt_plateName").val();
    var siteurl = $("#txt_siteUrl").val();
    var siteRank = $("#txt_siteRank").val();

    if (Trim(sitename) == "") {
        alert("站点名称不能为空");
        return;
    }
    if (Trim(platename) == "") {
        alert("板块名称不能为空");
        return;
    }
    if (Trim(siteurl) == "") {
        alert("站点地址不能为空");
        return;
    }
    if (Trim(siteRank) == "") {
        siteRank = 0;
    } else {
        siteRank = parseInt(siteRank);
    }
    UpdateSiteListBySiteId(siteId, sitename, platename, siteurl, siteRank);
    
}


//function Trim(value) {
//     //用正则表达式将前后空格  用空字符串替代。 
//    return value.replace(/(^\s*)|(\s*$)/g, "");
//}