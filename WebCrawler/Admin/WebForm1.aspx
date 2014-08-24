<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebCrawler.Admin.WebForm1" %>
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
            width: 477px;
           visibility:visible;
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
            margin:0px auto;
        }
        body
        {
            width:1300px;
        }
        #show
        {
            margin:0px auto;
        }
    </style>
 
      
    </script>
    <script type="text/javascript">
        function closetag() {
            $("#show").css("display", "none");
        }
        function btn_batchTag_click() {
            //得到选中的Tag集合
            var tagId = $("#txt_tag").val().trim();
            var sd_ids = $("#hid_ck_DateIds").val().trim();
            var projectid = $("#hid_projectId").val().trim();

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
                        tag_1 = $(ctrl_sp1).find("option:selected").text().trim();
                        $(ctrl_sp1).html(tag_1).css("color", "green");
                    } else {
                        //var str = $("#sp_tag_title_1").text().trim().replace("[", "").replace("]", "");
                        $(ctrl_sp1).html("&nbsp;");
                    }
                }

                if (sel_count > 1) {
                    var tag_2_id = $(ctrl_sp2).find("select").val();
                    if (tag_2_id != "") {
                        tag_2 = $(ctrl_sp2).find("option:selected").text().trim();
                        $(ctrl_sp2).html(tag_2).css("color", "green"); ;
                    } else {
                        //var str = $("#sp_tag_title_2").text().trim().replace("[", "").replace("]", "");
                        $(ctrl_sp2).html("&nbsp;");
                    }
                }
                if (sel_count > 2) {
                    var tag_3_id = $(ctrl_sp3).find("select").val();
                    if (tag_3_id != "") {
                        tag_3 = $(ctrl_sp3).find("select").find("option:selected").text().trim();
                        $(ctrl_sp3).html(tag_3).css("color", "green");
                    } else {
                        //var str = $("#sp_tag_title_3").text().trim().replace("[", "").replace("]", "");
                        $(ctrl_sp3).html("&nbsp;");
                    }
                }

                if (sel_count > 3) {
                    var tag_4_id = $(ctrl_sp4).find("select").val();
                    if (tag_4_id != "") {
                        tag_4 = $(ctrl_sp4).find("select").find("option:selected").text().trim();
                        $(ctrl_sp4).html(tag_4).css("color", "green");
                    } else {
                        //var str = $("#sp_tag_title_4").text().trim().replace("[", "").replace("]", "");
                        $(ctrl_sp4).html("&nbsp;");
                    }
                }
                if (sel_count > 4) {
                    var tag_5_id = $(ctrl_sp5).find("select").val();
                    if (tag_5_id != "") {
                        tag_5 = $(ctrl_sp5).find("select").find("option:selected").text().trim();
                        $(ctrl_sp5).html(tag_5).css("color", "green");
                    } else {
                        //var str = $("#sp_tag_title_5").text().trim().replace("[", "").replace("]", "");
                        $(ctrl_sp5).html("&nbsp;");
                    }
                }
                if (sel_count > 5) {
                    var tag_6_id = $(ctrl_sp6).find("select").val();

                    if (tag_6_id != "") {
                        tag_6 = $(ctrl_sp6).find("select").find("option:selected").text().trim();
                        $(ctrl_sp6).html(tag_6).css("color", "green");
                    } else {
                        //var str = $("#sp_tag_title_6").text().trim().replace("[", "").replace("]", "");
                        $(ctrl_sp6).html("&nbsp;");
                    }

                }

                // 得到标签的Id,以及该条记录的Id,进行tag处理
                var tag_mess = tag_1 + "|" + tag_2 + "|" + tag_3 + "|" + tag_4 + "|" + tag_5 + "|" + tag_6;
                $(t).text("标签");
                if (tag_mess.length <= 6) {
                    $(ctrl_sp1).text($("#sp_tag_title_1").text().trim().replace("[", "").replace("]", ""));
                    $(ctrl_sp2).text($("#sp_tag_title_2").text().trim().replace("[", "").replace("]", ""));
                    $(ctrl_sp3).text($("#sp_tag_title_3").text().trim().replace("[", "").replace("]", ""));
                    $(ctrl_sp4).text($("#sp_tag_title_4").text().trim().replace("[", "").replace("]", ""));
                    $(ctrl_sp5).text($("#sp_tag_title_5").text().trim().replace("[", "").replace("]", ""));
                    $(ctrl_sp6).text($("#sp_tag_title_6").text().trim().replace("[", "").replace("]", ""));
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
     <div id="show" style="width: 99%; height: 99%; display:block; position: absolute;
        z-index: 99; top: 0px; left: 0px; background: url('../Image/bg.png'); text-align: center;">
        <div id="alt" style="width: 277px; height: 200px; margin: 250px auto 0 auto; position: relative;
            overflow: hidden; background: #FFF; top: 0px; left: 0px;">
            <div style="width: 100%; height: 28px; background: url('../Image/bg_c.jpg') repeat-x; position:relative;">
                <div style="width:100%; height:26px; text-align:center; font-size:large; font-weight:bold; padding-top:5px;"><span>批量打标签</span></div>
                <div style="width:24px; height:26px; float:right; position:absolute; top:0px; right:5px;"><span><a href="#" style="display: block; width: 20px; height: 20px; background: url('../Image/close.jpg') no-repeat;
                    float: right; margin: 4px;" onclick="javascipt:closetag()" id="close"></a></span></div> 
            </div>
           
           <div style=" width:90%; height:40px; margin:0px auto; ">
              <div id="dv_tag_title" style=" width:100%; font-size:80%;" runat="server">
                  <h2><span id='sp_tag_title_1'>[产品]</span></h2>
              </div>
              <div id="dv_tag_content" style=" width:100%; text-align:center;" runat="server" >
                  <span id="sp_tag_3"><select><option value=''>--选择--</option><option value='op_20'>售后质量</option></select></span>
              </div>
           </div>
           <div style=" margin:40px auto 0px auto; width:200px;">
            <img alt="" src="../Image/tijiao.jpg" style="position: relative;" onclick="javascipt:batchatagGroup()" />
           </div>
        </div>
    </div>
    <form>
    </form>
</body>
</html>
