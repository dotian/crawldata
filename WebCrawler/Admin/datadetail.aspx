<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="datadetail.aspx.cs" Inherits="WebCrawler.Admin.datadetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/ShowData.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/lyoatMenu.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/datadetail.css" rel="stylesheet" type="text/css" />
    <link href="../Common/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../Common/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script src="../Common/JS/sitedate_addtagDoalog.js" type="text/javascript"></script>
    <script src="../Common/JS/sitedata_show.js" type="text/javascript"></script>
    <script src="../Common/JS/datadetail.js" type="text/javascript"></script>
    <style type="text/css">
        .style3
        {
            text-align: left;
            padding-left: 2px;
            width: 552px;
            visibility: visible;
        }
        #tb_tagList tr
        {
            line-height: 20px;
        }
        #tb_tagList
        {
            margin: 2px;
            border-top: 1px solid #99BBE8;
            border-left: 1px solid #99BBE8;
        }
        #tb_tagList td, th
        {
            border-right: 1px solid #c9d4e5;
            border-bottom: 1px solid #c9d4e5;
            padding-left: 5px;
            padding-top: 2px;
            vertical-align: middle;
        }
        #dv_tag_content div
        {
            margin: 0px auto;
        }
        #tb_dataList a
        {
            color: Green;
        }
    </style>
    <script type="text/javascript">

        function checkType() {
            var pid = $.trim($("#hid_projectId").val());
            pid = parseInt(pid);
            if (pid <= 0) {
                alert('没有找到要导入的项目');
                return false;
            }
            var seldataType = $("#select_dataType").val();
            seldataType = parseInt(seldataType);
            if (seldataType <= 0) {
                alert("请选择导入的数据类型");
                return false;
            }

            var fileName = $("#fileUpload1").val();
            if (fileName == "") {
                alert("请选择要导入的文件路径");
                return false;
            }
            var selDataName = $("#select_dataType").find("option:selected").text();
            if (confirm("确定要导入" + selDataName + "数据吗")) {
                //得到上传文件的值

                //返回String对象中子字符串最后出现的位置.
                var seat = fileName.lastIndexOf(".");
                //返回位于String对象中指定位置的子字符串并转换为小写.
                var extension = fileName.substring(seat).toLowerCase();
                //判断允许上传的文件格式
                if (extension != ".xls" && extension != ".xlsx") {
                    alert("不支持" + extension + "文件的上传!");
                    return false;
                } else {
                    return true;
                }
            }
        }


        //批量删除  #xiang_add  2014-4-26 12:48
        function img_pi_shan_click(status) {

            var ids = $("#hid_ck_DateIds").val();
            if (ids.length <= 0) {
                alert("没有选中要删除的数据!");
                return false;
            } else if (ids.length > 5000) {

                alert("要删除的数据量过多, 请限制在 800条以内!");
                return false;
            }

            if (confirm("确定要批量删除数据吗?")) {
                $.ajax({
                    type: "POST",
                    url: "ashxHelp/HandlerDeletePiAction.ashx",
                    data: "sd_id=" + ids,
                    async: false,
                    success: function (msg) {
                        if (parseInt(msg) > 0) {
                            //成功提交, 然后重新绑定 当前的数据
                            var jsonStr = getQueryJson_click();
                            $("#hid_ck_DateIds").val("");
                            //查出所有的数据
                            QueryShowStatus(99); //调用查询 "已删除 " 的按钮
                        } else {
                            alert("批量删除失败!");
                        }
                    }
                }); // ajax 结束
            }
        }


        //批量恢复数据,恢复数据以后, 数据状态为 0, 表示未审核
        //  #xiang_add  2014-4-26 12:48
        function img_pi_hui_click(status) {
            var ids = $("#hid_ck_DateIds").val();
            if (ids.length <= 0) {
                alert("没有选中要恢复的数据!");
                return false;
            } else if (ids.length > 5000) {
                alert("要恢复的数据量过多, 请限制在 800条以内!");
                return false;
            }

            if (confirm("确定要恢复删除的数据吗?")) {
                $.ajax({
                    type: "POST",
                    url: "ashxHelp/HandlerHuiPiAction.ashx",
                    data: "sd_id=" + ids,
                    async: false,
                    success: function (msg) {
                        if (parseInt(msg) > 0) {
                            //成功提交, 然后重新绑定 当前的数据
                            var jsonStr = getQueryJson_click();
                            $("#hid_ck_DateIds").val("");
                            //查出所有的数据
                            QueryShowStatus(99); //调用查询 "已删除 " 的按钮
                        } else {
                            alert("批量恢复失败!");
                        }
                    }
                }); // ajax 结束
            }
        }

        // show  显示批量打标签
        function img_batchtTags_click() {
            //传过去选中的Sdid
            //传过去 项目Id
            var ids = $("#hid_ck_DateIds").val();
            if (ids.length <= 0) {
                alert("没有选中要打标签的数据!");
                return false;
            }

            var sel_count = parseInt($("#hid_sel_count").val());

            var tagTitleCount = "";
            var m = $("#dv_tag_content"); //标签 内容

            var ctrl_sp1 = $(m).find("span:eq(0)");
            var ctrl_sp2 = $(m).find("span:eq(1)");
            var ctrl_sp3 = $(m).find("span:eq(2)");
            var ctrl_sp4 = $(m).find("span:eq(3)");
            var ctrl_sp5 = $(m).find("span:eq(4)");
            var ctrl_sp6 = $(m).find("span:eq(5)");

            if (sel_count <= 0) {
                alert("没有给该项目分配标签!");
                return false;
            }


            if (sel_count > 0) {
                var sel_sp_1 = $("#sp_tag_1").html();
                $(ctrl_sp1).html(sel_sp_1);
            }


            if (sel_count > 1) {
                var sel_sp_2 = $("#sp_tag_2").html();
                $(ctrl_sp2).html(sel_sp_2);
            }

            if (sel_count > 2) {
                var sel_sp_3 = $("#sp_tag_3").html();
                $(ctrl_sp3).html(sel_sp_3);
            }
            if (sel_count > 3) {
                var sel_sp_4 = $("#sp_tag_4").html();
                $(ctrl_sp4).html(sel_sp_4);
            }
            if (sel_count > 4) {
                var sel_sp_5 = $("#sp_tag_5").html();
                $(ctrl_sp5).html(sel_sp_5);
            }
            if (sel_count > 5) {
                var sel_sp_6 = $("#sp_tag_6").html();
                $(ctrl_sp6).html(sel_sp_6 + " <br />");
            }

            $("#show").css("display", "block");
        }

        // //批量 提交
        function batchatagGroup() {
            var b_issubmit = false;
            $("#txt_mess").val($("#show").html());
            var ids = $("#hid_ck_DateIds").val();
            var sel_count = parseInt($("#hid_sel_count").val());
            var tagTitleCount = "";
            var m = $("#dv_tag_content"); //这里是 td
            var ctrl_sp1 = $(m).find("div:eq(1)").find("span");
            var ctrl_sp2 = $(m).find("div:eq(2)").find("span");
            var ctrl_sp3 = $(m).find("div:eq(3)").find("span");
            var ctrl_sp4 = $(m).find("div:eq(4)").find("span");
            var ctrl_sp5 = $(m).find("div:eq(5)").find("span");
            var ctrl_sp6 = $(m).find("div:eq(6)").find("span");
            if (sel_count <= 0) {
                alert("没有给该项目分配标签!");
                return false;
            }
            var tag_1 = "";
            var tag_2 = "";
            var tag_3 = "";
            var tag_4 = "";
            var tag_5 = "";
            var tag_6 = "";
            if (sel_count > 0) {
                var tag_1_id = $(ctrl_sp1).find("select").val();
                if (tag_1_id != "") {
                    tag_1 = $.trim($(ctrl_sp1).find("option:selected").text());

                }
            }

            if (sel_count > 1) {
                var tag_2_id = $(ctrl_sp2).find("select").val();
                if (tag_2_id != "") {
                    tag_2 = $.trim($(ctrl_sp2).find("option:selected").text());
                }
            }

            if (sel_count > 2) {
                var tag_3_id = $(ctrl_sp3).find("select").val();
                if (tag_3_id != "") {
                    tag_3 = $.trim($(ctrl_sp3).find("select").find("option:selected").text());
                }
            }

            if (sel_count > 3) {
                var tag_4_id = $(ctrl_sp4).find("select").val();
                if (tag_4_id != "") {
                    tag_4 = $.trim($(ctrl_sp4).find("select").find("option:selected").text());
                    $(ctrl_sp4).html(tag_4).css("color", "green");
                }
            }
            if (sel_count > 4) {
                var tag_5_id = $(ctrl_sp5).find("select").val();
                if (tag_5_id != "") {
                    tag_5 = $.trim($(ctrl_sp5).find("select").find("option:selected").text());
                }
            }

            if (sel_count > 5) {
                var tag_6_id = $(ctrl_sp6).find("select").val();
                if (tag_6_id != "") {
                    tag_6 = $.trim($(ctrl_sp6).find("select").find("option:selected").text());
                }
            }

            // 得到标签的Id,以及该条记录的Id,进行tag处理
            var tag_mess = tag_1 + "|" + tag_2 + "|" + tag_3 + "|" + tag_4 + "|" + tag_5 + "|" + tag_6;
            if (ids.length > 0 && tag_mess.length > 5) {
                b_issubmit = true;
            } else {
                b_issubmit = false;
            }

            if (b_issubmit) {
                // 为true 正式提交, 提交成功以后下面的数据重新绑定
                $.ajax({
                    type: "POST",
                    url: "ashxHelp/HandlerbarchTagAction.ashx",
                    data: "piids=1&sd_id=" + ids + "&tagStr=" + tag_mess,
                    async: false,
                    success: function (msg) {
                        if (parseInt(msg) > 0) {
                            //成功提交, 然后重新绑定 当前的数据
                            var jsonStr = getQueryJson_click();
                            $("#hid_ck_DateIds").val("");
                            //查出所有的数据
                            datadetail.img_hot_click(jsonStr, function (data) {
                                Bind(data.value);
                            });
                        } else {
                            alert("打标签失败!");
                        }
                    }
                });
            }
            $("#show").css("display", "none");
        }

        function closetag() {
            $("#show").css("display", "none");
        }
    </script>
    <script type="text/javascript">

        function btn_batchTag_click() {
            //得到选中的Tag集合
            var tagId = $.trim($("#txt_tag").val());
            var sd_ids = $.trim($("#hid_ck_DateIds").val());
            var projectid = $.trim($("#hid_projectId").val());

            if (tagId.lenth <= 1) {
                alert("请选中标签");
                return false;
            }
            if (sd_ids.lenth <= 1) {
                alert("请重新选择要打标签的数据");
                return false;
            }
            $.post("ashxHelp/HandlerbarchTagAction.ashx", { tagId: tagId, sd_ids: sd_ids, pid: projectid }, function (b) {
                if (b > 0) {
                    //$("body").css("backgroundColor", "white");
                    alert("操作成功");
                    var pid = $("#hid_projectId").val();
                    var p = $("#hid_pageIndex").val();
                    var dataType = $("#select_dataType").val();
                    hidetagdiv();
                    getReloadJson_click();
                } else {
                    alert("操作失败");
                }
            });
        }
    </script>
    <script type="text/javascript">
        function sp_tag_t(t) {
            var b_v = $(t).text();
            var sel_count = parseInt($("#hid_sel_count").val());
            var tagTitleCount = "";
            var m = $(t).parent().parent().parent(); //这里是 td
            var ctrl_sp1 = $(m).find("div:eq(0)").find("span");
            var ctrl_sp2 = $(m).find("div:eq(1)").find("span");
            var ctrl_sp3 = $(m).find("div:eq(2)").find("span");
            var ctrl_sp4 = $(m).find("div:eq(3)").find("span");
            var ctrl_sp5 = $(m).find("div:eq(4)").find("span");
            var ctrl_sp6 = $(m).find("div:eq(5)").find("span");

            if (sel_count <= 0) {
                alert("没有给该项目分配标签!");
                return false;
            }
            if (b_v == "标签") {
                $("#hid_savetag").val("false");
                if (sel_count > 0) {
                    var sel_sp_1 = $("#sp_tag_1").html();
                    $(m).find("div:eq(0)").find("span").html(sel_sp_1);
                }
                if (sel_count > 1) {
                    var sel_sp_2 = $("#sp_tag_2").html();
                    $(m).find("div:eq(1)").find("span").html(sel_sp_2);
                }
                if (sel_count > 2) {
                    var sel_sp_3 = $("#sp_tag_3").html();
                    $(m).find("div:eq(2)").find("span").html(sel_sp_3);
                }
                if (sel_count > 3) {
                    var sel_sp_4 = $("#sp_tag_4").html();
                    $(m).find("div:eq(3)").find("span").html(sel_sp_4);
                }
                if (sel_count > 4) {
                    var sel_sp_5 = $("#sp_tag_5").html();
                    $(m).find("div:eq(4)").find("span").html(sel_sp_5);
                }
                if (sel_count > 5) {
                    var sel_sp_6 = $("#sp_tag_6").html();
                    $(m).find("div:eq(5)").find("span").html(sel_sp_6);
                }
                $(t).text("保存");
            } else {

                $("#hid_savetag").val("true");
                var tag_1 = "";
                var tag_2 = "";
                var tag_3 = "";
                var tag_4 = "";
                var tag_5 = "";
                var tag_6 = "";

                if (sel_count > 0) {
                    var tag_1_id = $(ctrl_sp1).find("select").val();
                    if (tag_1_id != "") {
                        tag_1 = $.trim($(ctrl_sp1).find("option:selected").text());
                        $(ctrl_sp1).html(tag_1).css("color", "green");
                    } else {
                        $(ctrl_sp1).html("&nbsp;");
                    }
                }

                if (sel_count > 1) {
                    var tag_2_id = $(ctrl_sp2).find("select").val();
                    if (tag_2_id != "") {
                        tag_2 = $.trim($(ctrl_sp2).find("option:selected").text());
                        $(ctrl_sp2).html(tag_2).css("color", "green"); ;
                    } else {
                        $(ctrl_sp2).html("&nbsp;");
                    }
                }
                if (sel_count > 2) {
                    var tag_3_id = $(ctrl_sp3).find("select").val();
                    if (tag_3_id != "") {
                        tag_3 = $.trim($(ctrl_sp3).find("select").find("option:selected").text());
                        $(ctrl_sp3).html(tag_3).css("color", "green");
                    } else {
                        $(ctrl_sp3).html("&nbsp;");
                    }
                }

                if (sel_count > 3) {
                    var tag_4_id = $(ctrl_sp4).find("select").val();
                    if (tag_4_id != "") {
                        tag_4 = $.trim($(ctrl_sp4).find("select").find("option:selected").text());
                        $(ctrl_sp4).html(tag_4).css("color", "green");
                    } else {
                        $(ctrl_sp4).html("&nbsp;");
                    }
                }
                if (sel_count > 4) {
                    var tag_5_id = $(ctrl_sp5).find("select").val();
                    if (tag_5_id != "") {
                        tag_5 = $.trim($(ctrl_sp5).find("select").find("option:selected").text());
                        $(ctrl_sp5).html(tag_5).css("color", "green");
                    } else {
                        $(ctrl_sp5).html("&nbsp;");
                    }
                }
                if (sel_count > 5) {
                    var tag_6_id = $(ctrl_sp6).find("select").val();

                    if (tag_6_id != "") {
                        tag_6 = $.trim($(ctrl_sp6).find("select").find("option:selected").text());
                        $(ctrl_sp6).html(tag_6).css("color", "green");
                    } else {
                        $(ctrl_sp6).html("&nbsp;");
                    }

                }
                //alert(sel_count);
                // 得到标签的Id,以及该条记录的Id,进行tag处理
                var tag_mess = tag_1 + "|" + tag_2 + "|" + tag_3 + "|" + tag_4 + "|" + tag_5 + "|" + tag_6;

                $(t).text("标签");
                // alert(tag_mess.length);//5个 |
                if (tag_mess.length <= 6) {
                    if ($.trim($("#sp_tag_title_1").text()).replace("[", "").replace("]", "").length < 1) {
                        return false;
                    }
                    $(ctrl_sp1).text($.trim($("#sp_tag_title_1").text()).replace("[", "").replace("]", ""));
                    if ($.trim($("#sp_tag_title_2").text()).replace("[", "").replace("]", "").length < 1) {
                        return false;
                    }
                    $(ctrl_sp2).text($.trim($("#sp_tag_title_2").text()).replace("[", "").replace("]", ""));
                    if ($.trim($("#sp_tag_title_3").text()).replace("[", "").replace("]", "").length < 1) {
                        return false;
                    }
                    $(ctrl_sp3).text($.trim($("#sp_tag_title_3").text()).replace("[", "").replace("]", ""));
                    if ($.trim($("#sp_tag_title_4").text()).replace("[", "").replace("]", "").length < 1) {
                        return false;
                    }
                    $(ctrl_sp4).text($.trim($("#sp_tag_title_4").text()).replace("[", "").replace("]", ""));
                    if ($.trim($("#sp_tag_title_5").text()).replace("[", "").replace("]", "").length < 1) {
                        return false;
                    }
                    $(ctrl_sp5).text($.trim($("#sp_tag_title_5").text()).replace("[", "").replace("]", ""));
                    if ($.trim($("#sp_tag_title_6").text()).replace("[", "").replace("]", "").length < 1) {
                        return false;
                    }
                    $(ctrl_sp6).text($.trim($("#sp_tag_title_6").text()).replace("[", "").replace("]", ""));
                    return false;
                }

                var sd_id = $(m).parent().find("input[type='checkbox']").attr("id");

                $.ajax({
                    type: "POST",
                    url: "ashxHelp/HandlerbarchTagAction.ashx",
                    data: "sd_id=" + sd_id + "&tagStr=" + tag_mess,
                    async: false,
                    success: function (msg) {
                        if (parseInt(msg) > 0) {
                            //打标签成功

                        } else {
                            alert("打标签失败!");
                        }
                    }
                });
            }
            return false;
        }
    </script>
</head>
<body>
    <div id="show" style="width: 99%; height: 99%; display: none; position: absolute;
        z-index: 99; top: 0px; left: 0px; background: url('../Image/bg.png'); text-align: center;">
        <div runat="server" id="alt" style="width: 200px; height: 200px; margin: 250px auto 0 auto;
            position: relative; overflow: hidden; background: #FFF;">
            <div style="width: 100%; height: 28px; background: url('../Image/bg_c.jpg') repeat-x;
                position: relative;">
                <div style="width: 100%; height: 26px; text-align: center; font-size: large; font-weight: bold;
                    padding-top: 5px;">
                    <span>批量打标签</span></div>
                <div style="width: 24px; height: 26px; float: right; position: absolute; top: 0px;
                    right: 5px;">
                    <span><a href="#" style="display: block; width: 20px; height: 20px; background: url('../Image/close.jpg') no-repeat;
                        float: right; margin: 4px;" onclick="javascipt:closetag()" id="close"></a></span>
                </div>
            </div>
            <div style="width: 90%; height: 40px; margin: 0px auto;">
                <div id="dv_tag_title" style="width: auto; font-size: 80%;" runat="server">
                </div>
                <div id="dv_tag_content" style="width: auto; text-align: center;" runat="server">
                </div>
            </div>
            <div style="margin: 40px auto 0px auto; width: 200px;">
                <img alt="" src="../Image/tijiao.jpg" style="position: relative;" onclick="javascipt:batchatagGroup()" />
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <div id="div_showContent">
        <table border="0" style="margin: 0px; padding: 0px;" cellpadding="0" cellspacing="0">
            <tr>
                <td id="div_td_content">
                </td>
            </tr>
        </table>
    </div>
    <div style="margin: 0px auto; width: 1280px; height: auto;">
        <div class="mian_middle" style="border: 2px solid #99BBE8; padding-left: 5px; width: 100%;">
            <div>
                <h4>
                    搜索
                </h4>
                <hr style="margin: 0px;" />
                <div id="div_search">
                    <table id="table_search" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="style1">
                                <span>时间:</span>
                                <input type="text" class="Wdate" style="width: 90px;" id="txt_timeStart" runat="server"
                                    onfocus="WdatePicker()" />&nbsp;~&nbsp;
                                <input type="text" class="Wdate" style="width: 90px;" id="txt_timeEnd" runat="server"
                                    onfocus="WdatePicker()" />&nbsp;调性:
                                <select runat="server" id="select_analysis" class="Select_5">
                                    <option value="-1">所有</option>
                                    <option value="1">正</option>
                                    <option value="2">中</option>
                                    <option value="3">负</option>
                                    <option value="4">争</option>
                                </select>&nbsp; 数据类型:
                                <asp:DropDownList ID="select_dataType" class="Select_5" runat="server" OnSelectedIndexChanged="btn_select_Click"
                                    AutoPostBack="True">
                                    <asp:ListItem Value="1">论坛</asp:ListItem>
                                    <asp:ListItem Value="2">新闻</asp:ListItem>
                                    <asp:ListItem Value="3">博客</asp:ListItem>
                                    <asp:ListItem Value="5">微博</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style3">
                                匹配词:&nbsp;<input type="text" id="txt_matchKey" runat="server" />&nbsp;
                                <select runat="server" id="select_matchRule">
                                    <option value="0">标题及内容</option>
                                    <option value="1">标题</option>
                                    <option value="2">内容</option>
                                </select>&nbsp;
                                <asp:Button ID="btn_select" runat="server" OnClientClick="return func_resetShowStatus();"
                                    OnClick="btn_select_Click" Text="搜" Width="35px" />&nbsp;
                                <asp:Button ID="btn_goback" runat="server" OnClientClick="return goback_click();"
                                    OnClick="btn_goback_click" Text="返回" CssClass="Button_3" />&nbsp;
                            </td>
                        </tr>
                        <tr style="height: 10px;">
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:FileUpload ID="fileUpload1" runat="server" Style="vertical-align: top" Width="229px" />&nbsp;
                                <asp:Button ID="btn_importExcel" runat="server" Text="导入数据" OnClientClick="return checkType();"
                                    OnClick="btn_importExcel_Click" />&nbsp;
                                <asp:Button ID="btnExport" runat="server" OnClientClick="return btnExportParms();"
                                    OnClick="btn_Export_click" Text="导出数据" />&nbsp;
                                <input type="button" value="下载模板" name="" id="btn_downloadTemplate" onserverclick="btn_downloadTemplate_click"
                                    runat="server" />
                            </td>
                            <td class="style3">
                                <img alt="" src="../Image/img_btn_pi.jpg" onclick="img_batchtTags_click();" />&nbsp;
                                <img alt="" src="../Image/attention.jpg" id="img_attention" onclick="return img_attention_click(1);" />
                                &nbsp;
                                <img alt="" src="../Image/hot.jpg" id="img_hot" onclick="return img_hot_click(1)" />
                                &nbsp;
                                <img alt="" src="../Image/showStatus_1.jpg" onclick="return QueryShowStatus(1);" />
                                &nbsp;
                                <img alt="" src="../Image/showStatus_2.jpg" onclick="return QueryShowStatus(2);" />&nbsp;
                                <img alt="" src="../Image/showStatus_99.jpg" onclick="return QueryShowStatus(99);" />&nbsp;
                                <img alt="" src="../Image/pi_shan.jpg" onclick="return img_pi_shan_click(99);">&nbsp;
                                <img alt="" src="../Image/pi_hui.jpg" onclick="return img_pi_hui_click(0);">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <hr style="margin-top: 8px;" />
            <div id="div_data">
                <div style="width: 200px; float: left; margin-bottom: 5px;">
                    &nbsp;&nbsp;&nbsp; &nbsp;<span id="sp_projectName" runat="server" style="font-size: 20px;
                        color: blue;">项目</span>
                </div>
                <div style="width: 848px; float: left;">
                    <span style="font-size: 20px;">
                        <%-- <img alt="" src="../Image/testJpg.jpg" onclick = "getQueryJson_click();" />--%>
                        <span runat="server" style="font-size: 70%;" id="sp_uploadresult"></span></span>
                </div>
                <div id="div_tb_dataList" runat="server" style="margin-top: 30px; border: 0px; height: 600px;
                    padding: 0px;">
                    <table id='tb_dataList' style='width: auto; margin: 0px auto;' cellpadding='0' cellspacing='0'>
                        <tr>
                            <th style='width: 41px;'>
                                <input type='checkbox' id='ck_all' value='' onclick='ck_all_click();' />
                            </th>
                            <th style='width: 265px;'>
                                帖子主题
                            </th>
                            <th style='width: auto; text-align: center;'>
                                标签处理<br />
                                #TagHead
                            </th>
                            <th style='width: 154px;'>
                                操作
                            </th>
                            <th style='width: 41px; text-align: center;'>
                                镜
                            </th>
                            <th style='width: 81px; text-align: center;'>
                                媒体名
                            </th>
                            <th style='width: 81px; text-align: center;'>
                                版块名
                            </th>
                            <th style='width: 97px; text-align: center;'>
                                抓取日期
                            </th>
                        </tr>
                    </table>
                    <script src="../Common/JS/commonjs.js" type="text/javascript"></script>
                </div>
            </div>
            <div>
                <div class="paging" runat="server" id="div_page" style="float: left; margin-top: 10px;">
                    <span id="a_firstPage" onclick="a_firstPage_click(this);" runat="server" style="margin-right: 5px;">
                        首页</span> <span id="a_lastPage" onclick="a_lastPage_click(this,-1);" runat="server"
                            style="margin-right: 5px;">上一页</span> <span id="a_nextPage" onclick="a_nextPage_click(this);"
                                runat="server" style="margin-right: 5px;">下一页</span> <span id="a_finalPage" onclick="a_finalPage_click(this);"
                                    runat="server" style="margin-right: 5px;">尾页</span> <span id="sp_pageMess" runat="server">
                                        当前第1页,共1页</span>
                </div>
            </div>
        </div>
    </div>
    <div style="visibility: hidden; position: absolute; bottom: 300px;">
        <span style="visibility: hidden">hid_singleId 里面存放的是单个的 Idyle="visibility: hidden">hid_singleId
            里面存放的是单个的 Id</span>
        <input type="hidden" id="hid_singleId" runat="server" value="" />
        <span style="visibility: hidden;">hid_ck_DateIds 里面存放的是全选的Ids,以 逗号分隔开</span>
        <input type="hidden" style="width: 600px;" id="hid_ck_DateIds_1" runat="server" value="" />
        <input type="hidden" style="width: 600px;" id="hid_ck_DateIds" runat="server" value="" />
        <span style="visibility: hidden">hid_showStatus 里面存放的是显示的状态 0未审核; 1预审核; 2已审核; 99已删除</span>
        <input type="text" id="hid_showStatus" runat="server" value="0" />
        <input type="text" id="hid_attention" runat="server" value="-1" />
        <input type="text" id="hid_hot" runat="server" value="-1" />
        <input type="hidden" id="hid_sd_id" runat="server" value="" />
        <input type="hidden" id="hid_projectId" runat="server" value="1001" />
        <span style="visibility: hidden">分页控件</span>
        <input type="hidden" id="hid_pageIndex" value="1" />
        <input type="hidden" runat="server" id="hid_pageCount" value="0" />
        <input type="hidden" id="hid_queryParms" runat="server" value="" />
        <input id="hid_sel_count" type="hidden" runat="server" value="" />
        <span id="sp_tag_1" runat="server"></span><span id="sp_tag_2" runat="server"></span>
        <span id="sp_tag_3" runat="server"></span><span id="sp_tag_4" runat="server"></span>
        <span id="sp_tag_5" runat="server"></span><span id="sp_tag_6" runat="server"></span>
        <input id="hid_savetag" type="hidden" value="true" />
        <span>如果要查看打标签的内容,需要将这个 提上去</span>
        <input type="hidden" id="txt_top_body" value="" style="width: 300px;" />
        <input type="hidden" id="hid_batchGropTagStatus" runat="server" value="" />
        <input type="text" id="txt_mess" value="" />
    </div>
    </form>
</body>
</html>
