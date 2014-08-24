<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="taglist.aspx.cs" Inherits="WebCrawler.Admin.taglist" %>

<%@ Register Src="../UserControl/MenuUserControl.ascx" TagName="MenuUserControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>标签列表</title>
    <link href="../Styles/lyoatMenu.css" rel="stylesheet" type="text/css" />
    <script src="../Common/JS/commonjs.js" type="text/javascript"></script>
    <script src="../Common/mSelect/mSelect.js" type="text/javascript"></script>
    <script src="../Common/JS/menu.js" type="text/javascript"></script>
    <script src="../jzae/lib/jquery.js" type="text/javascript"></script>
    <script src="../jzae/lib/jquery.cookie.js" type="text/javascript"></script>
    <script src="../jzae/jquery.treeview.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../jzae/jquery.treeview.css" />
    <script src="../jzae/jquery.treeview.edit.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jzae/demo/demo.js"></script>
    <style type="text/css">
        .a_update, .a_delete, .a_update_t, .a_add_t
        {
            margin-left: 5px;
        }
        #red span input[type='text']
        {
            width: 100px;
        }
        .a_addTag
        {
        }
        #red li
        {
            line-height: 22px;
            font-size: large;
            background-color: #f0f2f6;
        }
        #red li a
        {
            font-size: small;
        }
        
        #blue li
        {
            line-height: 22px;
            font-size: large;
            background-color: #f0f2f6;
        }
    </style>
    <script type="text/javascript">
        function a_update_registClick(t) {
            //修改的 方法
            var txtValue = $(t).text();
            if (txtValue == "修改") {
                var m = $(t).parent().parent().find("span").first();
                var oriTag = $(m).text();
                $(m).html("<input type = 'text' value='" + oriTag + "'>");
                $(t).text("保存");
            } else {
                var m = $(t).parent().parent().find("span").first();
                var new_tag = $(m).find("input[type='text']").first().val();
                $(m).text(new_tag);
                $(t).text("修改");
            }
            return false;
        }
        function a_delete_registClick(t) {
            //删除的方法
            if (confirm("删除不可恢复,确定要删除吗?")) {
                //将在这一个 li 隐藏,然后删除
                var tid = $(t).parent().parent().attr("id");
                tid = tid.replace("tag_", "");
                $.post("ashxHelp/deleteTagById.ashx", { tid: tid }, function (b) {
                    if (b <= 0) {
                        alert("删除失败!");
                    } else {
                        $(ctrl).hide();
                        //alert("删除成功!");
                    }
                });
            }
            return false;
        }

        function a_addTag_registClick(t) {
            var txtValue = $(t).text();
            if (txtValue == "添加标签") {
                var m = $(t).parent().parent().find("span").first();
                $(m).html("<input type = 'text' value=''>");
                $(t).text("保存标签");
                return false;
            } else {
                var m = $(t).parent().parent().find("span").first();
                var tagValue = $(m).find("input[type='text']").first().val().trim();
                if (tagValue == "") {
                    alert("标签内容不能为空");
                } else {
                    var htl = "<span>" + tagValue + "</span><span class='a_update'><a href='#' onclick='return a_update_registClick(this);'>修改</a></span><span class='a_delete'><a href='#' onclick='return a_delete_registClick(this);'>删除</a></span>";
                    $(t).parent().parent().html(htl).after("<li><span></span><span class='a_addTag'><a href='#'  onclick='return a_addTag_registClick(this);'>添加标签</a></span></li>"); // 这一行 li
                    $("#blue").treeview({
                        add: branches
                    });
                }
                //保存标签
                return false;
            }
        }
        $(document).ready(function () {
            $(".a_update").each(function (t) {
                $(this).find("a").click(function () {
                    //修改的 方法
                    var txtValue = $(this).text();
                    if (txtValue == "修改") {
                        var isa = $("#hid_tagId").val();
                        if (isa != "") {
                            $("#sp_errmess").text("请先保存修改的数据.");
                        } else {
                            var m = $(this).parent().parent().find("span").first();
                            var id = $(this).parent().parent().attr("id");

                            $("#hid_tagId").val(id.replace("tag_", ""));
                            var oriTag = $(m).text();
                            $(m).html("<input type = 'text' value='" + oriTag + "'>");
                            $(this).text("保存");
                        }
                    } else {
                        var m = $(this).parent().parent().find("span").first();
                        var new_tag = $(m).find("input[type='text']").first().val().trim();
                        var tagId = $("#hid_tagId").val();
                        //这里 传递修改
                        $.post("ashxHelp/modifyTagById.ashx", { id: tagId, tagname: new_tag }, function (res) { });
                        $(m).text(new_tag);
                        $("#hid_tagId").val("");
                        $("#sp_errmess").text("");
                        $(this).text("修改");
                    }
                    return false;
                });
            });
            $(".a_delete").each(function () {
                $(this).find("a").click(function () {
                    //删除的方法
                    if (confirm("删除不可恢复,确定要删除吗?")) {
                        //将在这一个 li 隐藏,然后删除
                        var id = $(this).parent().parent().attr("id");
                        var tid = id.replace("tag_", "");
                        var ctrl = $(this).parent().parent();
                        $.post("ashxHelp/deleteTagById.ashx", { tid: tid }, function (b) {
                            if (b <= 0) {
                                alert("删除失败!");
                            } else {
                                $(ctrl).hide();
                                //alert("删除成功!");
                            }
                        });
                    }
                    return false;
                });
            });

            //添加
            $(".a_addTag").each(function () {
                $(this).find("a").click(function () {
                    var txtValue = $(this).text();
                    if (txtValue == "添加标签") {
                        var m = $(this).parent().parent().find("span").first();
                        $(m).html("<input type = 'text' value=''>");
                        $(this).text("保存标签");
                        return false;
                    } else {
                        var m = $(this).parent().parent().find("span").first();
                        var tagValue = $(m).find("input[type='text']").first().val().trim();
                        if (tagValue == "") {
                            alert("标签内容不能为空");
                        } else {
                            var htl = "<span>" + tagValue + "</span><span class='a_update'><a href='#' onclick='return a_update_registClick(this);'>修改</a></span><span class='a_delete'><a href='#' onclick='return a_delete_registClick(this);'>删除</a></span>";
                            $(this).parent().parent().html(htl).after("<li><span></span><span class='a_addTag'><a href='#' onclick='return a_addTag_registClick(this);'>添加标签</a></span></li>"); // 这一行 li

                            //<span>恶意攻击</span> <span class="a_update"><a href="#">修改</a></span><span class="a_delete"><a href="#">删除</a></span>
                        }
                        //保存标签
                        return false;
                    }
                });
            });
        });

        $(function () {
            $("#btn_add").click(function () {
                var branches = "";
                branches = $("<li><span><input type='text' value=''></span><span class='a_update_t'><a href='#' onclick='a_update_t_click(this);'>保存</a></span><span class='a_add_t'><a href='#' onclick='a_add__t_click(this);'>添加</a></span><ul>" +
					"</ul></li>").appendTo("#red");
                $("#red").treeview({
                    add: branches
                });
            });
            $("#red").bind("contextmenu", function (event) {
                if ($(event.target).is("li") || $(event.target).parents("li").length) {
                    $("#red").treeview({
                        remove: $(event.target).parents("li").filter(":first")
                    });
                    return false;
                }
            });
        });
        function a_add__t_click(t) {
            var branches = "";
            var ul_t = $(t).parent().parent().find("ul").first();
            branches = $("<li><span><input type = 'text' value=''></span><span class='a_update'><a href='#' onclick='return btn_save_click(this);'>保存</a></span></li>").prependTo(ul_t);
            $("#red").treeview({
                add: branches
            });
        }

        function btn_save_click(t) {
            var tagValue = $(t).parent().parent().find("span").first().find("input[type='text']").val().trim();
            var htl = "<span>" + tagValue + "</span><span class='a_update'><a href='#' onclick='return a_update_registClick(this);'>修改</a></span><span class='a_delete'><a href='#' onclick='return a_delete_registClick(this);'>删除</a></span>";

            // 新增 二级 标签
            // 先找到 一级标签的Id,插入 数据库二级标签,返回二级标签的id,然后绑定  二级标签的Id
            var id = $(t).parent().parent().parent().parent().attr("id");
            id = id.replace("tag_", "");
            if (tagValue == "") {
                $("#sp_errmess").text("标签不你为空");
            } else {
                $.post("ashxHelp/addTagById.ashx", { id: id, tagname: tagValue }, function (tid) {
                    $(t).parent().parent().attr("id", "tag_" + tid);
                });
                $(t).parent().parent().html(htl)
            }
            //新增
            return false;
        }

        //一级标签
        function a_update_t_click(t) {
            var txtValue = $(t).text();
            if (txtValue == "修改") {
                var m = $(t).parent().parent().find("span").first();
                var oriTag = $(m).text();
                $(m).html("<input type = 'text' value='" + oriTag + "'>");
                $(t).text("保存");
            } else {
                //新增出来的一级标签,或者是 修改的一级标签
                //取出这个标签的 Id,如果没有,那就是新增的
                var id = $(t).parent().parent().attr("id");
                id = id.replace("tag_", "");
                var m = $(t).parent().parent().find("span").first();
                var new_tag = $(m).find("input[type='text']").first().val().trim();
                if (new_tag == "") {
                    alert("标签不允许为空!");
                } else {
                    if (id == "") {
                        id = "0";
                        $.post("ashxHelp/addTagById.ashx", { id: id, tagname: new_tag }, function (tid) {
                            $(t).parent().parent().attr("id", "tag_" + tid);
                        });
                    } else {
                        $.post("ashxHelp/modifyTagById.ashx", { id: id, tagname: new_tag }, function (res) { });
                    }
                    $(m).text(new_tag);
                    $(t).text("修改");
                }
            }
            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".li_tag_t").each(function () {
                $(this).find("span").first().find("input[type='checkbox']").first().click(function () {
                    var t = $(this).attr("checked");
                    if (t == true) {
                        $(this).parent().parent().find("ul").find("li").find("input[type='checkbox']").each(function () {
                            $(this).attr("checked", true);
                        });
                    } else {
                        $(this).parent().parent().find("ul").find("li").find("input[type='checkbox']").each(function () {
                            $(this).attr("checked", false);
                            //alert( $(this).attr("checked"));
                        });
                    }
                });
            });
            $(".ul_tag_t").each(function () {
                $(this).find("li").find("input[type='checkbox']").each(function () {
                    $(this).click(function () {
                        var t = $(this).attr("checked");
                        if (t == false) {
                            var m = $(this).parent().parent().parent().parent().find("span").first().find("input[type='checkbox']");
                            $(m).attr("checked", false);
                        }
                    });
                });
            });
        });

        $(document).ready(function () {
            var pid = $.cookie('pid');
            $("#sel_projectList").val(pid);
            sel_projectList_change();
        });
        function sel_projectList_change() {
            //得到下拉列表的值,然后进行操作
            //得到 下拉列表的值
            var pid = $("#sel_projectList").val().trim();
            if (pid == "") {
                $.cookie('pid', null);
                //全部清空
            } else {
                $.cookie('pid', pid);
                //直接绑定
                //获取这个项目的 当前的Id,查找出这个项目的 tag
                //先把全部清除掉
                $(".ul_tag_t").parent().find("input[type='checkbox']").attr("checked", false);
                $("#blue").find("ul").find("li").find("input[type='checkbox']").each(function () {
                    $(this).attr("checked", false);
                });
              
                taglist.GetAllTagByProjectId(pid, function (data) {
                    var result = data.value;
                    var json = eval(result);
                    for (var i = 0; i < json.length; i++) {
                        var tagId = "ck_tag_" + json[i];
                        var s = document.getElementById(tagId);
                        var m = $(s).attr("checked", true);
                    }

                  
                    $("#blue .ul_tag_t").each(function () {
                        var t = $(this).find("input[type='checkbox']").length;
                        if (t > 0) {
                            var i = 0;
                            $(this).find("input[type='checkbox']").each(function () {
                                var b = $(this).attr("checked");
                                if (b) {
                                    i++;
                                }
                            });
                            if (i ==t) {
                                //alert("父级要全选");
                                $(this).parent().find("input[type='checkbox']").attr("checked", true);
                            }
                        }
                    });


                });


            }
        }

        $(function () {
            var m = $.cookie('pid');
            if (m != "") {
                $("#sel_projectList").val(m);
                //绑定下面的 所有的数据
            }
        });

        function btn_AllotProjectTag_click() {
            //得到项目的ProjectId
            var projectId = $("#sel_projectList").val().trim();
            if (projectId == "") {
                alert("请选择项目");
                return false;
            }
            //得到所有被选中的 ck的id
            var i = 0;
            var str = "";
            $("#blue").find("ul").find("li").find("input[type='checkbox']").each(function () {
                var t = $(this).attr("checked");
                if (t == true) {
                    var id = $(this).attr("id");
                    id = id.replace("ck_tag_", "");
                    str += "," + id;
                }
            });

            var ids = str;
            if (ids.length <= 1) {
                alert("没有被选中的值");
                return;
            }
            taglist.AllotProjectTag(projectId, ids, function (b) {
                var isadd = parseInt(b.value);
                if (isadd < 0) {
                    alert("分配标签的大类 不允许超过6个");
                } else if (isadd > 0) {
                    alert("分配成功");
                } else {
                    alert("分配失败");
                }
            });
        }
    </script>
    <script type="text/javascript">
        /* 本JS支持滑动展开，支持原下拉的onchange事件，支持selected/disabled属性，支持上下键选择，支持表单的reset，应该说该有的都有了吧？ 作者：AngusYoung */
        var mySelect = new mSelect('mySelect', '../Common/mSelect/mSelect.css');
        window.onload = function () {
            var aS = document.getElementsByTagName('select');
            for (var i = 0; i < aS.length; i++) {
                switch (aS[i].getAttribute('mSty')) {
                    case 'redLine': mySelect.Create(aS[i], 'redLine');
                        break;
                    case 'blueCircle': mySelect.Create(aS[i], 'blueCircle', true);
                        break;
                    case 'orangeHeart': mySelect.Create(aS[i], 'orangeHeart', true);
                        break;
                }
            }
        }

        $(document).ready(function () {
            $("#ul_menu ul").each(function () {
                $(this).hide();
            });
            SHow_click("li_2");
            SHow_li_Selected("li_2", "taglist.aspx");
                findDimensions();
                //调用函数，获取数值
                window.onresize = findDimensions;
        });
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="margin: 0px auto; width: 1280px; height: auto;">
            <div class="mian_left">
                <uc1:MenuUserControl ID="MenuUserControl1" runat="server" />
            </div>
            <div class="mian_right" style="border-top: 0px;">
                <div style="width: 100%; height: 98.6%; padding: 0px; margin: 0px;">
                    <div class="div_contentLeft" style="width: 100%;" id="div_data">
                        <h4>
                            &nbsp;&nbsp; 标签管理&nbsp;&nbsp; <span id="sp_errmess" style="color: Red; font-size: small;">
                            </span>
                        </h4>
                        <hr style="margin-left: 0px" />
                        <div style="width: 400px; height: 700px; float: left; padding-left: 20px; overflow-y: auto;
                            border: 4px solid #DFE8F6;">
                            <select id="sel_projectList" msty="bluecircle" onchange="sel_projectList_change();"
                                style="width: 120px;" runat="server">
                            </select>&nbsp;
                            <input type="button" id="btn_AllotProjectTag" onclick="btn_AllotProjectTag_click();"
                                value="分配标签" />
                            &nbsp;
                          
                                <ul id="blue" runat="server" style="margin-top: 5px;" class="treeview-red">
                                </ul>
                           
                        </div>
                        <div style="width: 400px; height: 700px; float: left; margin-left: 20px; overflow-y: auto;
                            border: 4px solid #DFE8F6;">
                            <input type="button" id="btn_add" name="btn_add" value="新建一级标签" />&nbsp;
                         
                                <ul id="red" runat="server" style="margin-top: 5px;" class="treeview-red">
                                </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="visibility: hidden;">
        <input type="hidden" id="hid_tagId" value="" />
    </div>
    </form>
</body>
</html>
