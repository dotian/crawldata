<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contend.aspx.cs" Inherits="WebCrawler.Admin.contend" %>

<%@ Register src="../UserControl/MenuUserControl.ascx" tagname="MenuUserControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/lyoatMenu.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Common/JS/commonjs.js" type="text/javascript"></script>
    <script src="../Common/JS/menu.js" type="text/javascript"></script>

    <script src="../Common/mSelect/mSelect.js" type="text/javascript"></script>
  
    <style type="text/css">
        .style1
        {
            width: 83px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            SHow_click("li_4");
            SHow_li_Selected("li_4", "contend.aspx");
        });

        var isT = false;
            
        function btn_addContend_Click() {
           //先进行验证

            var projectId = $.trim($("#sel_ContendProject").val());
            if (projectId == "") {
                alert("请选择项目列表");
                return false;
            }

            var ids = $.trim($("#hid_ck_DateIds").val());
            if (ids=="") {
                alert("请选择竞争社列表");
                return false;
            }

            if (isT == false) {
                isT = true;
            } else {
                return;
            }

            $.ajax({
                type: "POST",
                url: "ashxHelp/HandlerAddContend.ashx",
                data: "projectId=" + projectId + "&ids=" + ids,
                async: false,
                success: function (msg) {
                    if (msg.indexOf("r") > 0) {
                        //True 和 False

                        //清空已经选择的数据
                        $("#hid_ck_DateIds").val();
                        $("#tb_dataList input[type='checkbox']").each(function () {
                            $(this).attr("checked", false);
                        });
                        $("#sel_ContendProject").val("");
                        alert("添加成功!");
                    } else {
                        alert("添加失败!");
                    }
                }
            });
            isT = false;
        }
        
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
                    &nbsp;搜索</h4>
                 &nbsp;<input type="text" id="txt_selectKey" runat="server" />&nbsp;
                    <select style=" width:100px;" runat="server" id="sel_SearchType">
                        <option value="1">项目名</option>
                    </select>&nbsp;
                    <input type="button" value="查询" name="btn_select" id="btn_select" onserverclick="btn_select_Click"
                        runat="server" />
                    <hr />
                <div id="div_main">
                      <div style=" width:100%; height:88.6%; padding:0px; margin:0px;">
                    <div class="div_contentLeft">
                        <h4>
                             &nbsp; 选择项目</h4>
                              <hr />
                        <table class="tb_manager">
                            <tr style="height:5px;"><td colspan="2">&nbsp;</td></tr>
                            <tr>
                                <td style="text-align:right;" class="style1">
                                    项目:
                                </td>
                                <td  style=" text-align:left;">
                                    <select runat="server" id="sel_ContendProject" style=" width:160px;">
                                        
                                    </select>
                                </td>
                            </tr>
                            <tr style=" height:2px;"><td colspan="2">&nbsp;</td></tr>
                            <tr>
                               <td colspan="2" style=" text-align:center;">
                                 <input type="button" name="btn_addContend"  onclick="return btn_addContend_Click();"  value="添加竞争社" />
                                </td>
                            </tr>
                             <tr style=" line-height:10px;"><td colspan="2"></td></tr>
                        </table>
                        <input type="hidden" id="hid_siteId" runat="server" value="" />
                    </div>
                    <div class="div_contentRight">
                        <h4>
                            &nbsp;竞争社列表</h4>
                            <hr />
                       <div style=" width:auto; height:633px; border:4px solid #DFE8F6; padding:1px;">
                         <table id="tb_dataList" style="width: 100%; text-align: center;" cellpadding="0px;"
                            cellspacing="0px;">
                            <asp:Repeater ID="rep_contendList" runat="server">
                                <HeaderTemplate>
                                    <th style="width: 5%;">
                                       <input type='checkbox' id='ck_all' value='' onclick='ck_all_click();' />
                                    </th>
                                   <th style="width: 8%;">
                                        编号
                                    </th>
                                    <th style="width: 20%;">
                                        项目名
                                    </th>
                                     <th>
                                        Rss关键字
                                     </th>
                                       <th> 运行状态
                                     </th>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <input type="checkbox" id='ck_<%#Eval("ProjectId")%>' value='<%#Eval("ProjectId")%>'  onclick='ck_one_click(this.id);'  />
                                        </td>
                                        <td>
                                            <%#Eval("ProjectId")%>
                                        </td>
                                        <td>
                                            <%#Eval("ProjectName")%>
                                        </td>
                                        <td>
                                         <%#Eval("RssKey")%>
                                        </td>
                                        <td>
                                            <%#Eval("PList_RunStatus").ToString()=="1"?"正常":"暂停"%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        </div>
                        &nbsp;&nbsp;
                    </div>
                </div>
                </div>
            </div>
        </div>
       
    </div>
     <div style="width: 600px; position:absolute;bottom: 100px; margin-left:200px; display:none;
               height: auto; border: 4px solid #666666;">
           <input type="hidden" id="hid_cate_click" value="" />
           <input type="hidden" runat="server" id="hid_ck_DateIds" value="" />
        </div>
    </form>
</body>
</html>
