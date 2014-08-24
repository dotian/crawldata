<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view.aspx.cs" Inherits="WebCrawler.view" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>舆情监控系统</title>
    <style type="text/css">
        body
        {
            font-size: 80%;
            width: 1200px;
            height: auto;
            margin: 0px auto;
        }
        table tr
        {
            line-height: 25px;
        }
    </style>
    <link href="Report/Style.css" rel="stylesheet" type="text/css" />
    <link href="Common/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="Common/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Report/amcharts.js" type="text/javascript"></script>
    <script src="Report/pie.js" type="text/javascript"></script>
    <script src="Report/serial.js" type="text/javascript"></script>
    <script src="Report/pie_gmjslly.js" type="text/javascript"></script>
    <script src="Report/pie_gdxhtsl.js" type="text/javascript"></script>
    <script src="Report/line_htslqst.js" type="text/javascript"></script>
    <script src="Report/line_fmhtslqst.js" type="text/javascript"></script>
    <script src="Report/line_zmhtqst.js" type="text/javascript"></script>
    <script src="Report/line_fmhtqst.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; background-color: #DFE8F6;">
        <div style="height: 36px; width: 520px; margin: 0px auto;">
            &nbsp;时间:<input id="txt_start" runat="server" style="width: 110px;" class="Wdate"
                onfocus="WdatePicker();" type="text" />
            &nbsp; ~&nbsp;&nbsp;<input id="txt_end" runat="server" style="width: 110px;" class="Wdate"
                onfocus="WdatePicker();" type="text" />&nbsp;
            <input type="button" name="btn_Query" id="btn_Query" value="查询" runat="server" onserverclick="btn_Query_click" />
          
        </div>
    </div>
    <div style="width: 100%;">
        <div style="height: auto; width: 520px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">话题社区来源排名</span></div>
            <table cellpadding="0" cellspacing="0" style="width: 520px; border: 2px solid #9db7e8;">
                <tr style="background-color: #f0f1f3">
                    <td style="width: 25px; border-right: 1px solid #d0d0d0;">
                    </td>
                    <td style="width: 255px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                        站点
                    </td>
                    <td style="width: 125px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                        数量
                    </td>
                    <td style="width: 115; padding-left: 5px;">
                        比例
                    </td>
                </tr>
                <asp:Repeater ID="rep_htsqlypm" runat="server">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #fdffff;">
                            <td style="width: 25px; text-align: right; background: -webkit-gradient(linear, left top,right top, from(rgb(249, 249, 249)), color-stop(0.5, rgb(241,242,244)), to(rgb(235, 235, 235)));
                                padding-right: 5px; border-right: 1px solid #d0d0d0;">
                                <%#Eval("Id")%>
                            </td>
                            <td style="width: 255px; padding-left: 5px;">
                                <%#Eval("MessTitle")%>
                            </td>
                            <td style="width: 125px; padding-left: 5px;">
                                <%#Eval("AppearCount")%>
                            </td>
                            <td style="width: 115; padding-left: 5px;">
                                <%#Eval("AppearRate")%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #d2e9ff">
                            <td style="width: 25px; text-align: right; padding-right: 5px; border-right: 1px solid #d0d0d0;">
                                <%#Eval("Id")%>
                            </td>
                            <td style="width: 255px; padding-left: 5px;">
                                <%#Eval("MessTitle")%>
                            </td>
                            <td style="width: 125px; padding-left: 5px;">
                                <%#Eval("AppearCount")%>
                            </td>
                            <td style="width: 115; padding-left: 5px;">
                                <%#Eval("AppearRate")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="height: auto; width: 520px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center; font-weight: bold;">
                <span style="color: Red; font-size: large; font-weight: bold;">负面话题社区来源排名</span></div>
            <table cellpadding="0" cellspacing="0" style="width: 520px; border: 2px solid #9db7e8;">
                <tr style="background-color: #f0f1f3">
                    <td style="width: 25px; border-right: 1px solid #d0d0d0;">
                    </td>
                    <td style="width: 255px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                        站点
                    </td>
                    <td style="width: 125px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                        数量
                    </td>
                    <td style="width: 115; padding-left: 5px;">
                        比例
                    </td>
                </tr>
                <asp:Repeater ID="rep_fmhtsqlypm" runat="server">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #fdffff;">
                            <td style="width: 25px; text-align: right; background: -webkit-gradient(linear, left top,right top, from(rgb(249, 249, 249)), color-stop(0.5, rgb(241,242,244)), to(rgb(235, 235, 235)));
                                padding-right: 5px; border-right: 1px solid #d0d0d0;">
                                <%#Eval("Id")%>
                            </td>
                            <td style="width: 255px; padding-left: 5px;">
                                <%#Eval("MessTitle")%>
                            </td>
                            <td style="width: 125px; padding-left: 5px;">
                                <%#Eval("AppearCount")%>
                            </td>
                            <td style="width: 115; padding-left: 5px;">
                                <%#Eval("AppearRate")%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #d2e9ff">
                            <td style="width: 25px; text-align: right; padding-right: 5px; border-right: 1px solid #d0d0d0;">
                                <%#Eval("Id")%>
                            </td>
                            <td style="width: 255px; padding-left: 5px;">
                                <%#Eval("MessTitle")%>
                            </td>
                            <td style="width: 125px; padding-left: 5px;">
                                <%#Eval("AppearCount")%>
                            </td>
                            <td style="width: 115; padding-left: 5px;">
                                <%#Eval("AppearRate")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="height: auto; width: 520px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">重复话题排名</span></div>
            <table cellpadding="0" cellspacing="0" style="width: 520px; border: 2px solid #9db7e8;">
                <tr style="background-color: #f0f1f3">
                    <td style="width: 25px; border-right: 1px solid #d0d0d0;">
                    </td>
                    <td style="width: 370px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                        标题
                    </td>
                    <td style="width: 125px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                        数量
                    </td>
                </tr>
                <asp:Repeater ID="rep_cfhtpm" runat="server">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #fdffff;">
                            <td style="width: 25px; text-align: right; background: -webkit-gradient(linear, left top,right top, from(rgb(249, 249, 249)), color-stop(0.5, rgb(241,242,244)), to(rgb(235, 235, 235)));
                                padding-right: 5px; border-right: 1px solid #d0d0d0;">
                                <%#Eval("Id")%>
                            </td>
                            <td style="width: 370px; padding-left: 5px;">
                                <%#IsSubString(DataBinder.Eval(Container.DataItem, "MessTitle").ToString(), 12)%>
                            </td>
                            <td style="width: 125px; padding-left: 5px;">
                                <%#Eval("AppearCount")%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #d2e9ff">
                            <td style="width: 25px; text-align: right; padding-right: 5px; border-right: 1px solid #d0d0d0;">
                                <%#Eval("Id")%>
                            </td>
                            <td style="width: 370px; padding-left: 5px;">
                                <%#IsSubString(DataBinder.Eval(Container.DataItem, "MessTitle").ToString(), 12)%>
                            </td>
                            <td style="width: 125px; padding-left: 5px;">
                                <%#Eval("AppearCount")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="height: auto; width: 520px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">负面重复话题排名</span></div>
            <table cellpadding="0" cellspacing="0" style="width: 520px; border: 2px solid #9db7e8;">
                <tr style="background-color: #f0f1f3">
                    <td style="width: 25px; border-right: 1px solid #d0d0d0;">
                    </td>
                    <td style="width: 370px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                        站点
                    </td>
                    <td style="width: 125px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                        数量
                    </td>
                </tr>
                <asp:Repeater ID="rep_fmcfhtpm" runat="server">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #fdffff;">
                            <td style="width: 25px; text-align: right; background: -webkit-gradient(linear, left top,right top, from(rgb(249, 249, 249)), color-stop(0.5, rgb(241,242,244)), to(rgb(235, 235, 235)));
                                padding-right: 5px; border-right: 1px solid #d0d0d0;">
                                <%#Eval("Id")%>
                            </td>
                            <td style="width: 370px; padding-left: 5px;">
                                <%#IsSubString(DataBinder.Eval(Container.DataItem, "MessTitle").ToString(), 12)%>
                            </td>
                            <td style="width: 125px; padding-left: 5px;">
                                <%#Eval("AppearCount")%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #d2e9ff">
                            <td style="width: 25px; text-align: right; padding-right: 5px; border-right: 1px solid #d0d0d0;">
                                <%#Eval("Id")%>
                            </td>
                            <td style="width: 370px; padding-left: 5px;">
                                <%#IsSubString(DataBinder.Eval(Container.DataItem, "MessTitle").ToString(), 12)%>
                            </td>
                            <td style="width: 125px; padding-left: 5px;">
                                <%#Eval("AppearCount")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="height: 400px; width: 700px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">各媒介来源数量</span></div>
            <div id="chartdiv_gmjslly" style="width: 100%; height: 100%;">
            </div>
        </div>
        <div style="height: 400px; width: 700px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">各调性话题数量</span></div>
            <div id="chartdiv_gdxhtsl" style="width: 100%; height: 100%;">
            </div>
        </div>

         <div style="height: 400px; width: 700px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">话题声量趋势图</span></div>
            <div id="chartdiv_htslqst" style="width: 100%; height: 100%;">


            </div>
        </div>
          <div style="height: 400px; width: 700px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">负面话题声量趋势图</span></div>
            <div id="chartdiv_fmhtslqst" style="width: 100%; height: 100%;">


            </div>
        </div>
         <div style="height: 400px; width: 520px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">话题趋势表</span></div>
            <div id="Div1" style="width: 100%; height: 100%;">
              <table cellpadding="0" cellspacing="0" style="width: 520px; border: 2px solid #9db7e8;">
                <tr style="background-color: #f0f1f3">
                    <td style="width: 66px; border-right: 1px solid #d0d0d0;">
                    日期
                    </td>
                    <td style="width: 50px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       新闻(正)
                    </td>
                     <td style="width: 50px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       新闻(负)
                    </td>
                    <td style="width: 50px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       博客(正)
                    </td>
                     <td style="width: 50px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       博客(负)
                    </td>
                     <td style="width: 50px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       论坛(正)
                    </td>
                     <td style="width: 50px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       论坛(负)
                    </td>
                     <td style="width: 50px; border-right: 1px solid #d0d0d0; padding-left: 5px;">
                       微博(负)
                    </td>
                </tr>
                <asp:Repeater ID="rep_htqsb" runat="server">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #fdffff;">
                            <td style="width: 66px; padding-left: 5px;">
                              <%#Eval("ContentDate")%>
                            </td>
                            <td style="width: 50px; padding-left: 5px;">
                               <%#Eval("News_Z_Num")%>
                            </td>
                              <td style="width: 50px; padding-left: 5px;">
                                <%#Eval("News_F_Num")%>
                            </td>
                              <td style="width: 50px; padding-left: 5px;">
                                  <%#Eval("Blog_Z_Num")%>
                            </td>
                             <td style="width: 50px; padding-left: 5px;">
                                  <%#Eval("Blog_F_Num")%>
                            </td>
                              <td style="width: 50px; padding-left: 5px;">
                                  <%#Eval("Forum_Z_Num")%>
                            </td>
                             <td style="width: 50px; padding-left: 5px;">
                                  <%#Eval("Forum_F_Num")%>
                            </td>
                            
                              <td style="width: 50px; padding-left: 5px;">
                                <%#Eval("Microblog_F_Num")%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #d2e9ff">
                            <td style="width: 66px; padding-left: 5px;">
                              <%#Eval("ContentDate")%>
                            </td>
                            <td style="width: 50px; padding-left: 5px;">
                               <%#Eval("News_Z_Num")%>
                            </td>
                              <td style="width: 50px; padding-left: 5px;">
                                <%#Eval("News_F_Num")%>
                            </td>
                              <td style="width: 50px; padding-left: 5px;">
                                  <%#Eval("Blog_Z_Num")%>
                            </td>
                             <td style="width: 50px; padding-left: 5px;">
                                  <%#Eval("Blog_F_Num")%>
                            </td>
                             <td style="width: 50px; padding-left: 5px;">
                                  <%#Eval("Forum_Z_Num")%>
                            </td>
                             <td style="width: 50px; padding-left: 5px;">
                                  <%#Eval("Forum_F_Num")%>
                            </td>
                              <td style="width: 50px; padding-left: 5px;">
                                <%#Eval("Microblog_F_Num")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            </div>
        </div>
         <div style="height: 600px; width: 800px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">正面话题趋势图(复合图)</span></div>
            <div id="chartdiv_zmhtqst" style="width: 100%; height: 100%;">
              

            </div>
        </div>
         <div style="height: 600px; width: 800px; margin: 0px auto; margin-top: 50px;">
            <div style="margin: 0px auto; text-align: center;">
                <span style="color: Red; font-size: large; font-weight: bold;">负面话题趋势图(复合图)</span></div>
            <div id="chartdiv_fmhtqst" style="width: 100%; height: 100%;">
            </div>
        </div>
       
        <div style="height: 100px; width: 700px; margin: 0px auto; margin-top: 50px;">
           
        </div>
    </div>
    <div>
        <input type="hidden" runat="server" id="txt_columData_gmjslly" value="" />
        <input type="hidden" runat="server" id="txt_columData_gdxhtsl" value="" />
         <input type="hidden" runat="server" id="txt_columData_htslqst" value="" />
         <input type="hidden" runat="server" id="txt_columData_fmhtslqst" value="" />

         <input type ="hidden" runat="server" id="txt_columData_zmhtqsb" value="" />

         <input type ="hidden" runat="server" id="txt_columData_fmhtqsb" value="" />

         <input type ="hidden" runat="server" id="txt_hidden_dt" value="" />
    </div>
    </form>
  
</body>
</html>
