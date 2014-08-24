<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="WebCrawler.Admin.category" %>

<%@ Register Src="../UserControl/MenuUserControl.ascx" TagName="MenuUserControl"
    TagPrefix="uc1" %>
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
    <title>数据挖掘系统--分类维护</title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Common/JS/menu.js" type="text/javascript"></script>
   
    <script type="text/javascript">

        $(document).ready(function () {
            $("#ul_menu ul").each(function () {
                $(this).hide();
            });
            SHow_click("li_1");
            SHow_li_Selected("li_1", "category.aspx");
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
                <div style="width: 100%; height: 9%; padding: 0px; margin: 0px;">
                    <h4>
                        搜索</h4>
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
                <div style="width: 100%; height: 88.6%; padding: 0px; margin: 0px;">
                    <div class="div_contentLeft">
                        <h4>
                            &nbsp;分类列表维护</h4>
                        <hr />
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    &nbsp; <select id="sel_category" runat="server" style=" width:160px;">
                                        <option value="value"></option>
                                    </select>
                                  &nbsp;
                                    <input type="button" value="查询" />
                                </td>
                            </tr>
                        
                        </table>
                        <input type="hidden" id="hid_siteId" runat="server" value="" />
                    </div>
                    <div class="div_contentRight">
                        <h4>
                            &nbsp;&nbsp; 站点列表</h4>
                        <hr />
                        <div id="div_tb_dataList" runat="server" style="width: auto; height: 600px; border: 4px solid #DFE8F6;
                            padding: 1px;">
                            <table id="tb_dataList" border="0" cellpadding="0" width="99%" cellspacing="0">
                                <tr>
                                    <th style="width: 5%">
                                         <input type="checkbox" id='ck_all' value='' />
                                    </th>
                                    <th style="width: 20%">
                                        版块名
                                    </th>
                                    <th style="width: 20%">
                                        网站名
                                    </th>
                                    <th style="width: 40%">
                                        地址
                                    </th>
                                </tr>
                                <asp:Repeater ID="rep_sitelist" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                               
                                               <input type="checkbox" id='<%#Eval("SiteId") %>' />
                                            </td>
                                            <td>
                                                <%#Eval("PlateName")%>
                                            </td>
                                            <td>
                                                <%#Eval("SiteName")%>
                                            </td>
                                            <td>
                                                <%#Eval("SiteUrl")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                </tr>
                            </table>
                        </div>
                        <div class="paging">
                        </div>
                        &nbsp;&nbsp;
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
       <input type="hidden" id="hid_cateTypeClass" value="" />
          <span style="visibility: hidden">hid_singleId 里面存放的是单个的 Idyle="visibility: hidden">hid_singleId
            里面存放的是单个的 Id</span>
        <input type="hidden" id="hid_singleId" runat="server" value="" />
        <span style="visibility: hidden;">hid_ck_DateIds 里面存放的是全选的Ids,以 逗号分隔开</span>
        <input type="hidden" style="width: 600px;" id="hid_ck_DateIds_1" runat="server" value="" />
        <input type="hidden" style="width: 600px;" id="hid_ck_DateIds" runat="server" value="" />
    </div>
    </form>
</body>
</html>
