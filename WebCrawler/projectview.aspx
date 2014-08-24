<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="projectview.aspx.cs" Inherits="WebCrawler.projectview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>舆情监控系统</title>
    <style type="text/css"> 
        #div_main
        {
            width:1200px;
            height:800px;
            margin:0px auto;
        }
        tr
        {
            line-height:25px;
        }
        
        tr td,th
        {
            font-size:80%;
            padding-left:5px;
            text-align:left;
        }
    </style>

    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SHow_click("li_2");
        });
        var isT = false;
        function load_project(t) {
            var projectId = $(t).parent().find("input[type='hidden']").first().val();
          
            if (isT==false) {
                isT = true;
            } else {
               return;
            }
            $.ajax({
                type: "POST",
                url: "hankookAshx/projectviewskip.ashx",
                data: "projectId=" + projectId,
                async: false,
                success: function (msg) {
                    if (msg.indexOf("r") > 0) {
                        //True 和 False
                        window.location.href = "customer.aspx?type=sitedata&c=1&a=1";
                    } else {
                        alert(msg);
                    }
                }
            });
            isT = false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="div_main" style="overflow-y:scroll; overflow-x:none;">

         <div style=" width: 1000px; margin-top: 50px; margin-left: auto; margin-right: auto; margin-bottom: 0px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">项目列表</span></div>
            <div id="div_projectView" style="width: 100%; height: 100%;">
           <table cellpadding="0" cellspacing="0"  style="width: 100%; border: 2px solid #9db7e8;">
                <tr style="background-color: #f0f1f3">
                    <th style="width: 125px; border-right: 1px solid #d0d0d0;padding-left: 5px;">
                       项目名称
                    </th>
                    <th style="width: 250; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       匹配规则
                    </th>
                    <th style="width: 125px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                        创建人
                    </th>
                    <th style="width: 125px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       开始时间
                    </th>
                     <th style="width: 125px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       结束时间
                    </th>
                      <th style="width: 125px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       运行状态
                    </th>
                    <th style="width: 125px; padding-left: 5px;">
                       操作
                    </th>
                </tr>
                <asp:Repeater ID="rep_projectview" runat="server">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #fdffff;">
                            <td style="width: 25px;padding-left:5px; border-right: 1px solid #d0d0d0;">
                                <%#Eval("ProjectName")%>
                            </td>
                            <td style="width: 255px; padding-left: 5px;">
                                <%#Eval("MatchingRule")%>
                            </td>
                            <td style="width: 115px; padding-left: 5px;">
                                <%#Eval("EmpId")%>
                            </td>
                            <td style="width: 115; padding-left: 5px;">
                                <%#Eval("CreateDate","{0:yyyy-MM-dd}")%>
                            </td>
                             <td style="width: 115; padding-left: 5px;">
                                <%#Eval("EndDate", "{0:yyyy-MM-dd}")%>
                            </td>
                             <td style="width: 115; padding-left: 5px;">
                                <%#Eval("RunStatus").ToString()=="1"?"正常":"暂停"%>
                            </td>
                            <td style="width: 115; padding-left: 5px;">
                            <a href="#" onclick="load_project(this); return false;">查看</a>
                                <input type="hidden" value='<%#Eval("ProjectId")%>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #d2e9ff">
                           <td style="width: 25px; margin-left:10px; padding-right: 5px; border-right: 1px solid #d0d0d0;">
                                <%#Eval("ProjectName")%>
                            </td>
                            <td style="width: 255px; padding-left: 5px;">
                                <%#Eval("MatchingRule")%>
                            </td>
                            <td style="width: 125px; padding-left: 5px;">
                                <%#Eval("EmpId")%>
                            </td>
                             <td style="width: 115; padding-left: 5px;">
                                <%#Eval("CreateDate","{0:yyyy-MM-dd}")%>
                            </td>
                             <td style="width: 115; padding-left: 5px;">
                                <%#Eval("EndDate", "{0:yyyy-MM-dd}")%>
                            </td>
                             <td style="width: 115; padding-left: 5px;">
                                 <%#Eval("RunStatus").ToString()=="1"?"正常":"暂停"%>
                            </td>
                            <td style="width: 115; padding-left: 5px;">
                               <a href="#" onclick="load_project(this); return false;">查看</a>
                                <input type="hidden" value='<%#Eval("ProjectId")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
