
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
    if (siteid == "վ����") {

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
    //�������
    var sitename = $("#txt_siteName").val();
    var platename = $("#txt_plateName").val();
    var siteurl = $("#txt_siteUrl").val();
    var siteRank = $("#txt_siteRank").val();

    if (Trim(sitename) == "") {
        alert("վ�����Ʋ���Ϊ��");
        return;
    }
    if (Trim(platename) == "") {
        alert("������Ʋ���Ϊ��");
        return;
    }
    if (Trim(siteurl) == "") {
        alert("վ���ַ����Ϊ��");
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
    //����վ��
    //int siteId, string siteName, string plateName, string siteUrl, int siteRank
    var siteId = $("#hid_siteId").val();
    if (Trim(siteId) != '') {
        siteId = parseInt(siteId);
    } else {
        alert("��ѡ��Ҫ�޸ĵ�վ��");
        return;
    }

    var sitename = $("#txt_siteName").val();
    var platename = $("#txt_plateName").val();
    var siteurl = $("#txt_siteUrl").val();
    var siteRank = $("#txt_siteRank").val();

    if (Trim(sitename) == "") {
        alert("վ�����Ʋ���Ϊ��");
        return;
    }
    if (Trim(platename) == "") {
        alert("������Ʋ���Ϊ��");
        return;
    }
    if (Trim(siteurl) == "") {
        alert("վ���ַ����Ϊ��");
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
//     //��������ʽ��ǰ��ո�  �ÿ��ַ�������� 
//    return value.replace(/(^\s*)|(\s*$)/g, "");
//}