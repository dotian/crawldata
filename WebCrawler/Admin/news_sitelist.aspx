<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news_sitelist.aspx.cs" Inherits="WebCrawler.Admin.news_sitelist" %>
<%@ Register src="../UserControl/MenuUserControl.ascx" tagname="MenuUserControl" tagprefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/lyoatMenu.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .backRed
        {
            background-color: Red;
        }
    </style>
    <title>数据挖掘系统--新闻列表维护</title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Common/JS/commonjs.js" type="text/javascript"></script>
    <script src="../Common/JS/menu.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#tb_dataList tr").click(function () {

                $("tr.backRed").removeClass("backRed");
                $(this).addClass("backRed");

                var a = $(this).children();
                var arr = new Array();
                for (var i = 0; i < a.length; i++) {
                    arr[arr.length] = a.eq(i).text();
                }

                FiltValue(arr);
            });
        });

        function FiltValue(arr) {

            var siteid = Trim(arr[0]);
            var sitename = Trim(arr[2]);
            var platename = Trim(arr[3])
            var siteurl = Trim(arr[4])
            var siteRank = Trim(arr[1])
            if (siteid == "站点编号") {

                $("#hid_siteId").val("");
                $("#txt_siteName").val("");
                $("#txt_plateName").val("");
                $("#txt_siteUrl").val("");
                $("#txt_siteRank").val("");

                $("tr.backRed").removeClass("backRed");

            } else {
                $("#hid_siteId").val(siteid);
                $("#txt_siteName").val(sitename);
                $("#txt_plateName").val(platename);
                $("#txt_siteUrl").val(siteurl);
                $("#txt_siteRank").val(siteRank);
            }

        }

        function addForumSite_click() {
            //进行添加
            var sitename = $("#txt_siteName").val();
            var platename = $("#txt_plateName").val();
            var siteurl = $("#txt_siteUrl").val();
            var siteRank = $("#txt_siteRank").val();

            if (Trim(sitename) == "") {
                alert("站点名称不能为空");
                return;
            }
            if (Trim(platename) == "") {
                alert("板块名称不能为空");
                return;
            }
            if (Trim(siteurl) == "") {
                alert("站点地址不能为空");
                return;
            }
            if (Trim(siteRank) == "") {
                siteRank = 0;
            } else {
                siteRank = parseInt(siteRank);
            }

            admin_news_sitelist.InsertSiteList(sitename, platename, siteurl, siteRank, function (b) {
                var isPass = b.value;
                if (isPass) {
                    //将 文本清空
                    $("#txt_siteName").val("");
                    $("#txt_plateName").val("");
                    $("#txt_siteUrl").val("");
                    $("#txt_siteRank").val("");
                    alert('添加成功');
                }
                else {
                    alert('添加失败');
                }
            })
        }

        function modifyForumSite_click() {
            //更新站点
            //int siteId, string siteName, string plateName, string siteUrl, int siteRank
            var siteId = $("#hid_siteId").val();
            if (Trim(siteId) != '') {
                siteId = parseInt(siteId);
            } else {
                alert("请选择要修改的站点");
                return;
            }

            var sitename = $("#txt_siteName").val();
            var platename = $("#txt_plateName").val();
            var siteurl = $("#txt_siteUrl").val();
            var siteRank = $("#txt_siteRank").val();

            if (Trim(sitename) == "") {
                alert("站点名称不能为空");
                return;
            }
            if (Trim(platename) == "") {
                alert("板块名称不能为空");
                return;
            }
            if (Trim(siteurl) == "") {
                alert("站点地址不能为空");
                return;
            }
            if (Trim(siteRank) == "") {
                siteRank = 0;
            } else {
                siteRank = parseInt(siteRank);
            }

            admin_news_sitelist.UpdateSiteListBySiteId(siteId, sitename, platename, siteurl, siteRank, function (b) {
                var isPass = b.value;
                if (isPass) {
                    //将 文本清空
                    $("#txt_siteName").val("");
                    $("#txt_plateName").val("");
                    $("#txt_siteUrl").val("");
                    $("#txt_siteRank").val("");
                    $("#hid_siteId").val("");
                    alert('更新成功');
                }
                else {
                    alert('更新失败');
                }
            })
        }

        function deleteForumSite_click() {
            //删除站点

            var siteId = $("#hid_siteId").val();
            if (Trim(siteId) != '') {
                siteId = parseInt(siteId);
            } else {
                alert("请选择要删除的站点");
                return;
            }
            admin_news_sitelist.DeleteSiteListBySiteId(siteId, function (b) {
                var isPass = b.value;
                if (isPass) {

                    //将 文本清空
                    $("#txt_siteName").val("");
                    $("#txt_plateName").val("");
                    $("#txt_siteUrl").val("");
                    $("#txt_siteRank").val("");
                    $("#hid_siteId").val("");
                    alert('删除成功');
                }
                else {
                    alert('删除失败');
                }
            })
        }


    </script>

     <script type="text/javascript">

         $(document).ready(function () {
             $("#ul_menu ul").each(function () {
                 $(this).hide();
             });
             SHow_click("li_1");
             //findDimensions();
             //调用函数，获取数值
             //  window.onresize = findDimensions;
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
                 <div style=" width:100%; height:9%; padding:0px; margin:0px;">
                    <h4>
                         &nbsp; 搜索</h4>
                    &nbsp;
                    <input type="text" id="txt_selectKey" runat="server" />&nbsp;
                    <select runat="server" id="sel_SearchType">
                        <option value="1">板块名</option>
                        <option value="2">网站名</option>
                    </select>&nbsp;
                    <input type="button" value="查询" name="btn_select" id="btn_select" onserverclick="btn_select_Click"
                        runat="server" />
                         <hr />
                </div>
                 <div style=" width:100%; height:88.6%; padding:0px; margin:0px;">
                    <div class="div_contentLeft">
                        <h4>
                             &nbsp; 项目</h4>
                              <hr />
                        <table class="tb_manager">
                            <tr>
                                <td class="td_left">
                                    网站名称:
                                </td>
                                <td class="td_right">
                                    <input type="text" id="txt_siteName" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_left">
                                    版块名称:
                                </td>
                                <td class="td_right">
                                    <input type="text" id="txt_plateName" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_left">
                                    网址:
                                </td>
                                <td class="td_right">
                                    <input type="text" id="txt_siteUrl" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_left">
                                    排名:
                                </td>
                                <td class="td_right">
                                    <input type="text" id="txt_siteRank" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_button" colspan="2">
                                    <input type="button" name="btn_addForumSite" id="btn_addForumSite" onclick="addForumSite_click();"
                                        value="添加" />&nbsp;
                                    <input type="button" name="btn_modifyForumSite" id="btn_modifyForumSite" onclick="modifyForumSite_click();"
                                        value="修改" />&nbsp;
                                    <input type="button" name="btn_deleteForumSite" id="btn_deleteForumSite" onclick="deleteForumSite_click()"
                                        value="删除" />&nbsp;
                                </td>
                            </tr>
                        </table>
                        <input type="hidden" id="hid_siteId" runat="server" value="" />
                    </div>
                    <div class="div_contentRight">
                        <h4>
                            项目列表</h4>
                            <hr />
                       <div style=" width:auto; height:600px; border:4px solid #DFE8F6; padding:1px;">
                         <table id="tb_dataList" style="width: 100%; text-align: center;" cellpadding="0px;"
                            cellspacing="0px;">
                            <asp:Repeater ID="rep_siteListBySiteType" runat="server">
                                <HeaderTemplate>
                                   <th style="width: 8%;">
                                        编号
                                    </th>
                                    <th style="width: 5%;">
                                        排名
                                    </th>
                                    <th style="width: 15%;">
                                      网站名称   
                                    </th>
                                    <th style="width: 20%;">
                                      版块名称
                                    </th>
                                    <th style="width: 10%;">
                                        网址
                                    </th>
                                    <th style="width: 12%;">
                                        创建时间
                                    </th>
                                    <th style="width: 12%;">
                                        修改时间
                                    </th>
                                    <th style="width: 10%;">
                                        备注
                                    </th>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td >
                                            <%#Eval("SiteId")%>
                                        </td>
                                        <td>
                                            <%#Eval("SiteRank").ToString()=="0"?"":Eval("SiteRank")%>
                                        </td>
                                        <td>
                                            <%#Eval("SiteName")%>
                                        </td>
                                        <td >
                                            <%#Eval("PlateName")%>
                                        </td>
                                        <td>
                                           <a href='<%#Eval("SiteUrl")%>' target="_blank" title='<%#Eval("SiteUrl")%>'>镜像</a> 
                                        </td>
                                        <td>
                                            <%#Eval("CreateDate","{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                           <%#Eval("UpdateDate", "{0:yyyy-MM-dd}").StartsWith("0") ? "" : Eval("UpdateDate", "{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                       
                                          <%#IsSubString(DataBinder.Eval(Container.DataItem, "Remark").ToString(), 5)%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        </div>
                        <div class="paging">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="首页"
                                LastPageText="尾页" NextPageText="下一页" NumericButtonCount="1" PrevPageText="上一页"
                                ShowPageIndex="false" OnPageChanged="AspNetPager1_PageChanged" SubmitButtonClass="page"
                                ShowCustomInfoSection="Right" CustomInfoTextAlign="Right" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条" LayoutType="Table">
                            </webdiyer:AspNetPager>
                           

                        </div>
                        &nbsp;&nbsp;
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
