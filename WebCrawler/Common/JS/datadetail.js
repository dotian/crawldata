$(document).ready(function () {
    $("#tb_dataList tr").click(function () {
        //找到Id
        var control_tr = $(this).find("td input[type='checkbox']").first().val();
        if (control_tr != null) {
            $("#hid_sd_id").val(control_tr);
        } else {
            $("#hid_sd_id").val("0");
        }
    });

    $("#div_showContent").hide();
});

function a_examine_Click(showstatus, t) {
    var p_tr = $(t).parent().parent();
    var sd_id = $(p_tr).find("input:checkbox").attr("id");
    if (showstatus < 0) {
        return false;
    }
    if (sd_id == "") {
        return false;
    }

    var pid = $("#hid_projectId").val();
    var p = $("#hid_pageIndex").val();
    var dataType = $("#select_dataType").val();
    datadetail.examine_Click(sd_id, showstatus, function (data) {
        var result = data.value;
        if (result > 0) {
            getReloadJson_click();
        }
    });
}

function a_delete_Click(showstatus, t) {
    if (showstatus != 99) {
        return false;
    }
    var p_tr = $(t).parent().parent();
    var sd_id = $(p_tr).find("input:checkbox").attr("id");
    if (showstatus < 0) {
        return false;
    }
    if (sd_id == "") {
        return false;
    }
    if (confirm('确定要删除吗?')) {
        datadetail.delete_Click(sd_id, function (data) {
            var b = data.value; //重新绑定这一行数据
            if (b == true) {
                $(p_tr).hide();
            } else {
                alert("删除失败!");
            }
        });
    }
}


function QueryShowStatus(statusNum) {
    //预审核 1,已审核 2,已删除 99
    $("#hid_showStatus").val(statusNum);
    $("#hid_attention").val("-1");
    $("#hid_hot").val("0");
    $("#hid_pageIndex").val("1");
    var jsonStr = getQueryJson_click();
    //查出所有的数据
    datadetail.img_queryShowStatus_click(jsonStr, function (data) {
        Bind(data.value);
    });
}


function a_batchTag_click(t) {
    var p_tr = $(t).parent().parent();
    var sd_id = $(p_tr).find("input:checkbox").attr("id");
    $("#hid_ck_DateIds").val("," + sd_id);
    show_tagdiv();
}

function getReloadJson_click() {
    var jsonStr = getQueryJson_click();
    datadetail.reloadTable_click(jsonStr, function (data) {
        Bind(data.value);
    });
}

function getQueryJson_click() {
    //得到 页面上选择的参数
    //传到 后台
    var pid = $("#hid_projectId").val();
    var timeStart = $("#txt_timeStart").val(); //开始时间
    var timeEnd = $("#txt_timeEnd").val(); //结束时间
    var analysis = $("#select_analysis").val(); //调性
    var dataType = $("#select_dataType").val(); //数据类型
    var matchKey = $("#txt_matchKey").val(); //搜索关键字
    var matchRule = $("#select_matchRule").val(); //匹配规则
    var showStatus = $("#hid_showStatus").val(); //显示状态
    var attention = parseInt($("#hid_attention").val()); //显示状态
    var hot = parseInt($("#hid_hot").val()); //显示状态

    var pageIndex = parseInt($("#hid_pageIndex").val());
    pageIndex = pageIndex <= 0 ? 1 : pageIndex;

    var json = "";
    json += "[{\"pid\":" + pid + ",";
    json += "\"timeStart\":" + "\"" + timeStart + "\",";
    json += "\"timeEnd\":" + "\"" + timeEnd + "\",";
    json += "\"analysis\":" + analysis + ",";
    json += "\"dataType\":" + dataType + ",";
    json += "\"matchKey\":" + "\"" + matchKey + "\",";
    json += "\"matchRule\":" + matchRule + ",";
    json += "\"attention\":" + attention + ",";
    json += "\"hot\":" + hot + ",";
    json += "\"showStatus\":" + showStatus + ",";
    json += "\"pageIndex\":" + pageIndex + "";
    json += "}]";

    var m = eval(json);
   
    return json;
}


function img_attention_click(attention) {
    $("#hid_attention").val(attention);
    $("#hid_showStatus").val("0");
    $("#hid_hot").val("0");
    $("#hid_pageIndex").val("1");
    var jsonStr = getQueryJson_click();

    //查出所有的数据
    datadetail.img_attention_click(jsonStr, function (data) {
        Bind(data.value);
    });
}

function Bind(dataJson) {
    if (dataJson != null) {
        var objJson = eval(dataJson);
        $("#div_tb_dataList").html(objJson[0].alltableHtml);
        $("#div_page").html(objJson[0].pagediv);
        $("#hid_pageCount").val(objJson[0].pageCount);
    } else {
        alert("没有获取到数据");
    }
}

function img_hot_click() {
    $("#hid_attention").val("0");
    $("#hid_showStatus").val("0");
    $("#hid_hot").val("1"); //显示状态
    $("#hid_pageIndex").val("1");
    var jsonStr = getQueryJson_click();
    //查出所有的数据
    datadetail.img_hot_click(jsonStr, function (data) {
        Bind(data.value);
    });
}


//重新加载当前页面
function reloadCurrentTable(pid, p, dataType) {
    $.post("ashxHelp/HandlerreloadCurrentTable.ashx", { pid: pid, p: p, dataType: dataType }, function (data) {
        if (data == "") {
            return false;
        } else {
            var objJson = eval(data);
            $("#div_tb_dataList").html(objJson[0].alltableHtml);
            var i = 0;
            var coun = $("#tb_dataList").find("tr").each(function () {
                i++;
            });
            //alert(i);
        }
    });
}



function a_firstPage_click(t) {
    $("#hid_pageIndex").val("1");
    BondJson();
    countDown(15);
}
function a_lastPage_click(t) {

    var pageCount = parseInt($("#hid_pageCount").val());
    var pageIndex = parseInt($("#hid_pageIndex").val());
    pageIndex--;
    pageIndex = pageIndex <= 0 ? 1 : pageIndex;
    $("#hid_pageIndex").val(pageIndex);
    BondJson();
    countDown(15);
}
function a_nextPage_click(t) {
    var pageCount = $("#hid_pageCount").val();
    pageCount = parseInt(pageCount);
    var pageIndex = parseInt($.trim($("#hid_pageIndex").val()));
    pageIndex++;
    pageIndex = pageIndex >= pageCount ? pageCount : pageIndex;
    $("#hid_pageIndex").val(pageIndex);
    BondJson();
    countDown(15);
}
function a_finalPage_click(t) {
    var jsonStr = getQueryJson_click();
    var pageCount = $("#hid_pageCount").val();
    pageCount = parseInt(pageCount);
    var currentPage = pageCount;
    $("#hid_pageIndex").val(currentPage);
    BondJson();
    countDown(15);
}

function BondJson() {
    var jsonStr = getQueryJson_click();
    datadetail.nextPage_click(jsonStr, function (data) {
        Bind(data.value);
    });
}

function btnExportParms() {
    var jsonStr = getQueryJson_click();
    $("#hid_queryParms").val(jsonStr);
    return true;
}

function func_resetShowStatus() {
    $("#hid_showStatus").val("0"); //显示状态
    $("#hid_attention").val("-1"); //显示状态
    $("#hid_hot").val("-1"); //显示状态
}

function goback_click() {
    $("#txt_matchKey").val("");
    $("#txt_timeStart").val("");
    $("#txt_timeEnd").val("");
    $("#hid_showStatus").val("0");
    $("#select_analysis").val("-1");
    $("#hid_attention").val("-1");
    $("#hid_hot").val("-1");
    $("#hid_pageIndex").val("1");
    return true;
}

function sp_content_mouseover(t) {
    var content = $(t).next("input[type='hidden']").val();
    $("#div_td_content").text(content);
    $("#div_showContent").show();

}
function sp_content_mouseout(t) {
    $("#div_td_content").text("");
    $("#div_showContent").hide();
}

function countDown(secs) {
    if (--secs > 0) {
        setTimeout("countDown(" + secs + ")", 100); // 100毫秒 
    } else {
        Load_Sd_idCheck();
    }
}
function Load_Sd_idCheck() {

    var tagIds = $("#hid_ck_DateIds").val();
    var tagArray = new Array();
    tagArray = tagIds.split(',');
    $("#tb_dataList").find("input[type='checkbox']").each(function () {
        var id = $(this).attr("id");
        for (var i = 0; i < tagArray.length; i++) {
            if (id == tagArray[i]) {
                $(this).attr("checked", true);
            }
        }
    });
}