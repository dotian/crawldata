
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.ltrim = function () {
    return this.replace(/(^\s*)/g, "");
}
String.prototype.rtrim = function () {
    return this.replace(/(\s*$)/g, "");
}


function Trim(value) {
    // 用正则表达式将前后空格  用空字符串替代。 
    return value.replace(/(^\s*)|(\s*$)/g, "");
}

$(document).ready(function () {
    /* 数据内容 荧光棒 效果*/
    $("#tb_dataList tr").css("backgroundColor", "white");

    $("#tb_dataList tr").mouseover(function () {
        $(this).css("backgroundColor", "#EFEFEF");
    });

    $("#tb_dataList tr").mouseout(function () {
        $(this).css("backgroundColor", "white");
    });

});


function ck_all_click() {
    /* CkeckBox 全选按钮的事件*/
    var str = $("#hid_ck_DateIds").val();
    var b = $("#ck_all").attr("checked");
    if (b) {
        $("input:checkbox").each(function () {
            if ($(this).val() != "") {
                var m = "," + $(this).val();
                str = str.replace(m, "");
                str += m;
                $(this).attr("checked", true);
            }
        });
    }
    else {
        $("input:checkbox").each(function () {
            if ($(this).val() != "") {
                var m = "," + $(this).val();
                str = str.replace(m, "");
                $(this).attr("checked", false);
            }
        });
    }
    $("#hid_ck_DateIds").val(str);
}

function ck_one_click(id) {
    /* CkeckBox  单个 按钮的事件*/
    var ckbox = document.getElementById(id);
    var str = $("#hid_ck_DateIds").val();
    var b = $(ckbox).attr("checked");
    if (b) {
        if ($(ckbox).val() != "") {
            var m = "," + $(ckbox).val();
            str = str.replace(m, "");
            str += m;
            $("#hid_singleId").val($(ckbox).val());
        }
    } else {
        $("#ck_all").attr("checked", false);
        if ($(ckbox).val() != "") {
            var m = "," + $(ckbox).val();
            str = str.replace(m, "");
        }
    }
    str = str.replace(",,", ",");
    $("#hid_ck_DateIds").val(str);
}

function ck_single_click(id) {
    /* CkeckBox  保持唯一选中效果*/

    var ckbox = document.getElementById(id);

    var b = $(ckbox).attr("checked");
    if (b) {
        $("#hid_singleId").val($(ckbox).val().trim());
        //先将 所有的都不选中
        $("input:checkbox").each(function myfunction() {
            $(this).attr("checked", false);
        });
        //然后选中这个一个
        $(ckbox).attr("checked", true);
    } else {
        //本来是选中的,现在没有选中,将hid_singleId的值清空
        $("#hid_singleId").val("");
    }
}












