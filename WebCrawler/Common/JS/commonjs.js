
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
    // ��������ʽ��ǰ��ո�  �ÿ��ַ�������� 
    return value.replace(/(^\s*)|(\s*$)/g, "");
}

$(document).ready(function () {
    /* �������� ӫ��� Ч��*/
    $("#tb_dataList tr").css("backgroundColor", "white");

    $("#tb_dataList tr").mouseover(function () {
        $(this).css("backgroundColor", "#EFEFEF");
    });

    $("#tb_dataList tr").mouseout(function () {
        $(this).css("backgroundColor", "white");
    });

});


function ck_all_click() {
    /* CkeckBox ȫѡ��ť���¼�*/
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
    /* CkeckBox  ���� ��ť���¼�*/
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
    /* CkeckBox  ����Ψһѡ��Ч��*/

    var ckbox = document.getElementById(id);

    var b = $(ckbox).attr("checked");
    if (b) {
        $("#hid_singleId").val($(ckbox).val().trim());
        //�Ƚ� ���еĶ���ѡ��
        $("input:checkbox").each(function myfunction() {
            $(this).attr("checked", false);
        });
        //Ȼ��ѡ�����һ��
        $(ckbox).attr("checked", true);
    } else {
        //������ѡ�е�,����û��ѡ��,��hid_singleId��ֵ���
        $("#hid_singleId").val("");
    }
}












