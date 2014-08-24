<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add_project.aspx.cs" Inherits="WebCrawler.Admin.add_project" %>

<%@ Register src="../UserControl/MenuUserControl.ascx" tagname="MenuUserControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/lyoatMenu.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Common/JS/commonjs.js" type="text/javascript"></script>
    <script src="../Common/mSelect/mSelect.js" type="text/javascript"></script>
    <script src="../Common/JS/menu.js" type="text/javascript"></script>
    <script src="../Common/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../Common/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../Common/JS/dateFormat.js" type="text/javascript"></script>

    <style type="text/css">
        #div_main
        {
            line-height: 20px;
        }
        #tb_project_add
        {
            margin-left: 20px;
            width: 400px;
            border-top:1px solid #99BBE8;
            border-left:1px solid #99BBE8;
            border-right:1px solid #c9d4e5;
        }
        #tb_project_add tr
        {
            height: 28px;
        }
        #tb_project_add td
        {
            border-bottom:1px solid #c9d4e5;
        }
        #sel_MatchType
        {
            width: 172px;
        }
        #sel_SearchType
        {
            width: 172px;
        }
        input[type="text"]
        {
             height:16px;
            width: 169px;
        }
        .style1
        {
            width: 127px;
        }
         #div_tbHtml table
        {
            margin-left: 20px;
            width: 400px;
            border-top:1px solid #99BBE8;
            border-left:1px solid #99BBE8;
        }
        #div_tbHtml tr
        {
            height: 28px;
        }
        #div_tbHtml td
        {
            border-right:1px solid #c9d4e5;
            border-bottom:1px solid #c9d4e5;
        }
         #div_tbHtml th
        {
            border-right:1px solid #c9d4e5;
            border-bottom:1px solid #c9d4e5;
        }
    </style>
    <title></title>
    <script type="text/javascript">
        function show_rule() {
          var v = $("#txt_MatchRule").val().trim();
          if (v.indexOf("@") >= 0 || v.indexOf("%") >= 0 || v.indexOf("'") >= 0 || v.indexOf(" ") >= 0) {
              alert("匹配词之中不能出现空格,%,',@");
              $("#txt_MatchRule").focus();
          }


          var c = v.indexOf("-");
          var d = v.indexOf("&");
          var e = v.indexOf("+");
          if ((c>0||d>0)&&(e>c||e>d||d>c)) {
              alert("符号的使用顺序 为+,&,-");
              $("#txt_MatchRule").focus();
          }
          
//          var ctrl =  document.getElementById("txt_MatchRule");
//          var left = ctrl.offsetLeft;
//          var top = ctrl.offsetTop;
//          while (ctrl.offsetParent != null) {
//              oParent = ctrl.offsetParent
//              left += oParent.offsetLeft
//              top += oParent.offsetTop  // Add parent top position
//              ctrl = oParent
//          }

//          $("#div_matchRule").css("left", left + 200).css("top", top);
//          $("#div_matchRule").show();
//          $("#txtRssKey").val(left + "   " + top);
        }

        function btn_resetProject_click() {
            $("#txt_ProjectName").val("");
            $("#txt_MatchRule").val("");
            $("#sel_MatchType").val("0");
            $("#txtRssKey").val("");
            $("#txt_EndDate").val("");
            $("#ck_forum").attr("checked", false);
            $("#ck_news").attr("checked", false);
            $("#ck_blog").attr("checked", false);
            $("#ck_microblog").attr("checked", false);

        }

        function btn_addProject_click() {
            var projectname = $("#txt_ProjectName").val();
            var matchRule = $("#txt_MatchRule").val().trim();
            if (matchRule=="") {
                alert("匹配规则不能为空!");
                return false;
            }

            var selType = $("#sel_MatchType").val();
            selType = parseInt(selType);

            var endDate = $("#txt_EndDate").val();
            var myDate = new Date().Format('yyyy-MM-dd');
            if (endDate == "") {
                alert("结束时间不能为空");
            }
            if (CompareDate(endDate, myDate)) {

            } else {
                alert("结束日期必须大于当前时间!");
            }
            var empname = $("#txt_EmpName").val();
            if (empname=="") {
                alert("没有登录!");
                return false;
            }
           
            var rssKey = $("#txtRssKey").val().trim();
            //----------分类Id----------------------------------//

                var sp_forum = $("#sp_forum").text().replace("论坛分类Id:","").trim();
                var sp_news = $("#sp_news").text().replace("新闻分类Id:", "").trim();
                var sp_blog = $("#sp_blog").text().replace("博客分类Id:", "").trim();
                var sp_microblog = $("#sp_microblog").text().replace("微博分类Id:", "").trim();

                add_project.addProject_click(projectname, matchRule, selType, rssKey, endDate, empname, sp_forum, sp_news, sp_blog, sp_microblog, function (data) {
                    var projectId = parseInt(data.value);
                    if (projectId > 0) {
                        alert("添加成功!");
                    } else if (projectId <0) {
                        alert("添加失败,正在运行的项目达到最大上限!");
                    } else {
                        alert("添加失败!");
                    }
                });
            }

            function ck_category_click(classId,t) {

                var b = $(t).attr("checked"); //选中状态

                $.post("ashxHelp/HandlerCategory.ashx", { classId: classId }, function (data) {
                    var theader = "<table style=' width:100%;' border='0' cellpadding='0' cellspacing='0'>";
                    theader += "<tr><th style='width:15%;'>编号</th>";
                    theader += "<th style='width:30%;'>分类名称</th>";
                    theader += "<th style='width:15%;'>站点总数</th>";
                    theader += "<th style='width:30%;'>创建时间</th></tr>";
                    var obj = eval(data);
                    var tbody = "";

                    var sp_ctrl;
                    if (classId == 1) {
                        sp_ctrl = $("#sp_forum");
                    } else if (classId == 2) {
                        sp_ctrl = $("#sp_news");
                    } else if (classId == 3) {
                        sp_ctrl = $("#sp_blog");
                    } else if (classId == 5) {
                        sp_ctrl = $("#sp_microblog");
                    }
                    if ($(sp_ctrl).text().length > 7) {
                        $(t).attr("checked", "true");
                    } else {
                        $(t).attr("checked", "");
                    }

                    for (var i = 0; i < obj.length; i++) {
                        var m = "|" + obj[i].CateId + "|";

                        var b_index = $(sp_ctrl).text().indexOf(m);
                        b_index = parseInt(b_index)

                        if (b_index > 0) {
                            tbody += "<tr><td><input type='checkbox' checked='checked' id='" + obj[i].CateId + "' value='" + obj[i].CateId + "' onclick='ck_one(" + classId + ",this);' /></td>"
                        } else {
                            tbody += "<tr><td><input type='checkbox' id='" + obj[i].CateId + "' value='" + obj[i].CateId + "' onclick='ck_one(" + classId + ",this);' /></td>"
                        }

                        tbody += "<td>" + obj[i].CategoryName + "</td>";
                        tbody += "<td>" + obj[i].SiteCount + "</td>";
                        tbody += "<td>" + obj[i].CreateDate + "</td></tr>";
                    }
                    html = theader + tbody + "</table>";
                    $("#div_tbHtml").html(html);
                });
            }

            function ck_one(classId, t) {
                classId = parseInt(classId);
                var sp_ctrl;
                var ck_ctrl;
                if (classId == 1) {
                    sp_ctrl = $("#sp_forum");
                    ck_ctrl = $("#ck_forum");
                } else if (classId == 2) {
                    sp_ctrl = $("#sp_news");
                    ck_ctrl = $("#ck_news");
                } else if (classId == 3) {
                    sp_ctrl = $("#sp_blog");
                    ck_ctrl = $("#ck_blog");
                } else if (classId == 5) {
                    sp_ctrl = $("#sp_microblog");
                    ck_ctrl = $("#ck_microblog");
                } else {
                    alert("没有此分类");
                    return false;
                }

                var b = $(t).attr("checked");
                var str = $(sp_ctrl).text();
                var m = "|" + $(t).val() + "|";
                str = str.replace(m, "");
                if (b) {
                    str += m;
                    $(sp_ctrl).text(str);

                } else {
                    $(sp_ctrl).text(str);
                }

                if ($(sp_ctrl).text().length>7) {
                    $(ck_ctrl).attr("checked","true");
                } else {
                    $(ck_ctrl).attr("checked", "");
                }

            }
            function ck_one_click(ckbox) {
                /* CkeckBox  单个 按钮的事件*/
               // var ckbox = document.getElementById(id);

                var b = $(ckbox).attr("checked");
                var m = "";
                if (b) {
                    if ($(ckbox).val() != "") {
                         m = "|" + $(ckbox).val()+"|";
                        str = str.replace(m, "");
                        str += m;
                        $("#hid_singleId").val($(ckbox).val());
                    }
                } else {
                    $("#ck_all").attr("checked", false);
                    if ($(ckbox).val() != "") {
                         m = "," + $(ckbox).val();
                        str = str.replace(m, "");
                    }
                }
                $("#hid_ck_DateIds").val(str);
            }
    </script>
     <script type="text/javascript">

         $(document).ready(function () {
             $("#ul_menu ul").each(function () {
                 $(this).hide();
             });
             SHow_click("li_2");
             SHow_li_Selected("li_2", "add_project.aspx");
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
            <div class="mian_right">
                <h4>
                    &nbsp;添加项目</h4>
                <hr />
                <div id="div_main">
                    <div style=" float:left; width:40%" >
                    <table id="tb_project_add" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style=" text-align:right;" class="style1">
                                项目名称:&nbsp;
                            </td>
                            <td>
                                <input id="txt_ProjectName" runat="server" type="text" /><br />
                            </td>
                        </tr>
                        <tr>
                            <td style=" text-align:right;" class="style1">
                                匹配方式:&nbsp;
                            </td>
                            <td>
                                <select id="sel_MatchType" runat="server">
                                    <option value="0">标题及内容</option>
                                    <option value="1">标题</option>
                                    <option value="2">内容</option>
                                </select><br />
                            </td>
                        </tr>
                        <tr>
                            <td style=" text-align:right;" class="style1">
                                匹配词:&nbsp;
                            </td>
                            <td>
                                <input id="txt_MatchRule" type="text" onblur ="show_rule();" runat="server" /><br />
                            </td>
                        </tr>
                         <tr>
                            <td style=" text-align:right;" class="style1">
                                第三方关键字:&nbsp;
                            </td>
                            <td>
                                <input id="txtRssKey" type="text" runat="server" /><br />
                            </td>
                        </tr>
                        <tr>
                            <td style=" text-align:right;" class="style1">
                                结束日期:&nbsp;
                            </td>
                            <td>
                                <input id="txt_EndDate" class="Wdate" onfocus="WdatePicker()" type="text" runat="server" />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style=" text-align:right;" class="style1">
                                添加人:&nbsp;
                            </td>
                            <td>
                                <input id="txt_EmpName" type="text" runat="server" readonly="readonly" /><br />
                            </td>
                        </tr>
                       
                        <tr>
                         <td colspan="2">
                             <div id="div_projectClass"  style=" width:217px; margin:0px auto;">
                                 <input id="ck_forum"  onclick="ck_category_click(1,this);" type="checkbox" value="1" />论坛
                                 <input id="ck_news" onclick="ck_category_click(2,this);"  type="checkbox" value="2" />新闻
                                 <input id="ck_blog"  onclick="ck_category_click(3,this);" type="checkbox" value="3"/>博客
                                 <input id="ck_microblog" onclick="ck_category_click(5,this);" type="checkbox" value="5" />微博
                             </div>
                         </td>
                        </tr>
                        <tr>
                            <td colspan="2" style=" padding-left:140px;">

                                <img alt="" id="btn_addProject" onclick="btn_addProject_click();" src="../Image/add_click.jpg" />&nbsp;
                                <img alt="" id="btn_resetProject" onclick="btn_resetProject_click();" src="../Image/reset.jpg" />
                            </td>
                        </tr>
                    </table>
                    <div style=" width:90%; margin-left:20px;">
                        <span id="sp_forum">论坛分类Id:</span><br />
                        <span id="sp_news">新闻分类Id:</span><br />
                        <span id="sp_blog">博客分类Id:</span><br />
                        <span id="sp_microblog">微博分类Id:</span><br />
                     </div>

                       <div id="div_matchRule" 
                            style=" width:485px; background-color:White; margin-left:10px; padding-left:5px; margin-top:10px; height:100px; color:Green;">
                         匹配词规则:+号表示或,&表示必须包含,-表示去掉.符号都为半角,<br />
                         使用顺序为+,&,- ;匹配词中间不要有空格<br />
                         举例1:要搜索"方太"或者"FOTILE" ,规则为: <span style="color:Red;">方太+FOTILE</span><br />
                         举例2:要搜索"三星字库门",或者"三星死机",一定要包含"字库门",不要"三星手机"<br />
                         规则为: <span style=" color:Red;">三星字库门+三星死机&字库门-三星手机</span>
                    </div>
                    </div>
                     
                      
                    <div id="div_tbHtml" style=" float:left;width:55%">
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
       <input type="hidden" id="hid_cate_click" value="" />
    </form>
</body>
</html>
