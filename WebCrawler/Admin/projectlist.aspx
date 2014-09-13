<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="projectlist.aspx.cs" Inherits="WebCrawler.Admin.projectlist" %>
<%@ Register src="../UserControl/MenuUserControl.ascx" tagname="MenuUserControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/lyoatMenu.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Common/JS/commonjs.js" type="text/javascript"></script>
    <script src="../Common/JS/menu.js" type="text/javascript"></script>



    <title></title>
    <style type="text/css">
        #div_project
        {
            padding: 0px;
            margin: 0px;
            border-top: 2px solid #99BBE8;
            background-color: #DFE8F6;
        }
    </style>

    <script type="text/javascript">
        function a_stopOrStartProject(t) {
           // a_stop_1013
            var id = $.trim($(t).attr("id")).substring(7);
            var title = $(t).text();
            if (title=="开启") {
                if (confirm('确定要开启运行该项目吗?')) {
                    projectlist.a_startProject_click(id, function (data) {
                        var result = parseInt(data.value);
                        if (result > 0) {
                            $(t).text("暂停");
                            //alert("已开启该项目");
                        } else {
                            alert("操作失败!");
                        }

                    });
                }
            } else {
                if (confirm('确定要暂停运行该项目吗?')) {
                    projectlist.a_stopProject_click(id, function (data) {
                        var result = parseInt(data.value);
                        if (result > 0) {
                            $(t).text("开启");
                           // alert("已暂停该项目");
                        } else {
                            alert("操作失败!");
                        }

                    });
                }
            }
        }
        function a_deleteProject(t) {
            var id = $.trim($(t).attr("id")).substring(9);
            if (confirm('确定要删除该项目吗?删除后无法恢复')) {
                projectlist.a_deleteProject_click(id, function (data) {
                    var result = parseInt(data.value);
                    if (result > 0) {
                        $(t).parent().parent().hide();
                        //alert("该项目已成功删除!");
                    } else {
                        alert("操作失败!");
                    }
                });
            }
        }
    
    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#ul_menu ul").each(function () {
                $(this).hide();
            });
            SHow_click("li_2");
            SHow_li_Selected("li_2", "projectlist.aspx");
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
                <div>
                    <h4>
                         &nbsp;搜索</h4>
                    <input type="text" id="txt_selectKey" runat="server" />&nbsp;
                    <select runat="server" id="sel_SearchType">
                        <option value="0">项目名</option>
                        <option value="1">匹配词</option>
                    </select>&nbsp;
                    <input type="button" value="查询" name="btn_select" id="btn_select" onserverclick="btn_select_Click"
                        runat="server" />
                    <hr />
                </div>

               
                <div id="div_project" style=" width:100%; height:88.6%; padding:0px; margin:0px;">
                    <h4>
                        &nbsp;项目列表</h4>
                    <hr />
                   <%-- <div style="width: auto; height: 90.6%; border: 4px solid #DFE8F6;">--%>
                    <div style="width: auto; height:650px; overflow-y: auto; border: 4px solid #DFE8F6;">
                        <table id="tb_dataList" style="width: 1100px;" cellpadding="0" cellspacing="0">
                            <asp:Repeater ID="rep_projectlist" runat="server">
                                <HeaderTemplate>
                                    <th style="width: 10%;">
                                        项目名
                                    </th>
                                    <th style="width: 8%;">
                                        匹配规则
                                    </th>
                                    <th style="width: 15%;">
                                        匹配词
                                    </th>
                                    <th style="width: 5%;">
                                        添加人
                                    </th>
                                    <th style="width: 10%;">
                                        结束时间
                                    </th>
                                    <th style="width: 5%;">
                                        论坛数
                                    </th>
                                    <th style="width: 5%;">
                                        新闻数
                                    </th>
                                    <th style="width: 5%;">
                                        博客数
                                    </th>
                                    <th style="width: 5%;">
                                        微博数
                                    </th>
                                    <th style="width: 6%;">
                                        状态
                                    </th>
                                    <th style="width: 5%;">
                                        网站
                                    </th>
                                    <th style="width: 5%;">
                                        分类
                                    </th>
                                    <th style="width: 5%;">
                                        操作
                                    </th>
                                    <th style="width: 6%;">
                                        操作
                                    </th>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <span style="visibility: hidden;">
                                            <%#Eval("ProjectId")%></span>
                                        <td>
                                            <%#Eval("ProjectName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MatchingRuleTypeName")%>
                                        </td>
                                        <td>
                                            <%#  Eval("MatchingRule").ToString().Length > 10 ? Eval("MatchingRule").ToString().Substring(0, 10) + "..." : Eval("MatchingRule")%>
                                        </td>
                                        <td>
                                            <%#Eval("EmpId")%>
                                        </td>
                                        <td>
                                            <%#Eval("EndDate", "{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                            <%#Eval("ForumNum")%>
                                        </td>
                                        <td>
                                            <%#Eval("NewsNum")%>
                                        </td>
                                        <td>
                                            <%#Eval("BlogNum")%>
                                        </td>
                                        <td>
                                            <%#Eval("MicroBlogNum")%>
                                        </td>
                                        <td>
                                            状态通过
                                        </td>
                                        <td>
                                            <a>查看</a>
                                        </td>
                                        <td>
                                            <a href='#'>查看</a></td>
                                         <td>
                                          <a target="_blank" href='datadetail.aspx?pid=<%#Eval("ProjectId")%>'>看项目</a>
                                            </td>
                                           <td>
                                               <a  href="#" id='a_stop_<%#Eval("ProjectId")%>' onclick="a_stopOrStartProject(this);return false;"><%#Eval("PList_RunStatus").ToString()=="0"?"开启":"暂停"%></a>
                                                <a href="#" id='a_delete_<%#Eval("ProjectId")%>'  onclick="a_deleteProject(this);return false;">删除</a>  
                                          </td>
                                           
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>