$(document).ready(function () {
    $(".li_one ul").each(function () {
        $(this).hide();
    });

    $(".li_one li").css("backgroundColor", "white");

    $(".li_one li").mouseover(function () {
        $(this).css("backgroundColor", "#EFEFEF");
    });

    $(".li_one li").mouseout(function () {
        var m_href = $("#txt_li_a_href").val();
        var li_a_href = $(this).find("a").attr("href");
        if (m_href == li_a_href) {
            //��ɫһ��,��������
            $(this).css("backgroundColor", "#EFEFEF");
        }
        else {
            $(this).css("backgroundColor", "white");
        }

    });
   
    findDimensions();
    //���ú�������ȡ��ֵ
    window.onresize = findDimensions;
});
//ѡ�е� �Ӳ˵�
function SHow_li_Selected(id, m_url) {
    var ctrl = document.getElementById(id);
    var li_a_href = $("#txt_li_a_href").val();
    if (m_url != li_a_href) {
        $("#" + id + " li").each(function () {
            var li_url = $(this).find("a").attr("href");
            if (m_url == li_url) {
                $(this).css("backgroundColor", "#EFEFEF");
                $(this).find("a").css("color", "rgb(81,53,5)");
                $("#txt_li_a_href").val(li_url);
            }
        });
    } else {

    }
}
//�򿪵� �����˵�
function SHow_click(id) {
    var m = $("#txt_li").val();
    if (m != id) {
        $("#txt_li").val(id);
        $("#" + id + " ul").show();
        $("#" + m + " ul").hide();
    } else {
        // $("#" + id + " ul").hide(); //�������,��ʾ ���Li������ӱ�ǩ
    }
   // alert(id);
}

function findDimensions() //��������ȡ�ߴ�
{
    var winWidth = 0;
    var winHeight = 0;

    //��ȡ���ڿ��
    if (window.innerWidth)
        winWidth = window.innerWidth;
    else if ((document.body) && (document.body.clientWidth))
        winWidth = document.body.clientWidth;
    //��ȡ���ڸ߶�

    if (window.innerHeight)
        winHeight = window.innerHeight;
    else if ((document.body) && (document.body.clientHeight))

        winHeight = document.body.clientHeight;

    //ͨ������Document�ڲ���body���м�⣬��ȡ���ڴ�С

    if (document.documentElement && document.documentElement.clientHeight && document.documentElement.clientWidth) {
        winHeight = document.documentElement.clientHeight;
        winWidth = document.documentElement.clientWidth;
    }

    //�������������ı���
    //document.form1.availHeight.value = winHeight;
    // document.form1.availWidth.value = winWidth;

    var hei = (winHeight - 170) > 656 ? 656 : (winHeight - 170);
    // alert(winHeight); 901
   
    var m = $(".li_one ul").height(hei);
    //alert(m);
}