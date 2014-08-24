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
            //颜色一样,不用设置
            $(this).css("backgroundColor", "#EFEFEF");
        }
        else {
            $(this).css("backgroundColor", "white");
        }

    });
   
    findDimensions();
    //调用函数，获取数值
    window.onresize = findDimensions;
});
//选中的 子菜单
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
//打开的 导航菜单
function SHow_click(id) {
    var m = $("#txt_li").val();
    if (m != id) {
        $("#txt_li").val(id);
        $("#" + id + " ul").show();
        $("#" + m + " ul").hide();
    } else {
        // $("#" + id + " ul").hide(); //两个相等,表示 点击Li下面的子标签
    }
   // alert(id);
}

function findDimensions() //函数：获取尺寸
{
    var winWidth = 0;
    var winHeight = 0;

    //获取窗口宽度
    if (window.innerWidth)
        winWidth = window.innerWidth;
    else if ((document.body) && (document.body.clientWidth))
        winWidth = document.body.clientWidth;
    //获取窗口高度

    if (window.innerHeight)
        winHeight = window.innerHeight;
    else if ((document.body) && (document.body.clientHeight))

        winHeight = document.body.clientHeight;

    //通过深入Document内部对body进行检测，获取窗口大小

    if (document.documentElement && document.documentElement.clientHeight && document.documentElement.clientWidth) {
        winHeight = document.documentElement.clientHeight;
        winWidth = document.documentElement.clientWidth;
    }

    //结果输出至两个文本框
    //document.form1.availHeight.value = winHeight;
    // document.form1.availWidth.value = winWidth;

    var hei = (winHeight - 170) > 656 ? 656 : (winHeight - 170);
    // alert(winHeight); 901
   
    var m = $(".li_one ul").height(hei);
    //alert(m);
}