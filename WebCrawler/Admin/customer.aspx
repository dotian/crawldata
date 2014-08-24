<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer.aspx.cs" Inherits="WebCrawler.Admin.customer" %>
<%@ Register Src="../UserControl/MenuUserControl.ascx" TagName="MenuUserControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/lyoatMenu.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Common/JS/commonjs.js" type="text/javascript"></script>
    <script src="../Common/mSelect/mSelect.js" type="text/javascript"></script>
    <script src="../Common/JS/menu.js" type="text/javascript"></script>
    <script src="../Common/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../Common/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../Common/JS/dateFormat.js" type="text/javascript"></script>
    <style type="text/css">
        #div_empIdList
        {
            background-color: white;
        }
        #div_main
        {
            line-height: 20px;
        }
        #tb_empid
        {
            margin-left: 20px;
            width: 400px;
            border-top: 1px solid #99BBE8;
            border-left: 1px solid #99BBE8;
            border-right: 1px solid #c9d4e5;
        }
        #tb_empid tr
        {
            height: 28px;
        }
        
        #tb_empid td
        {
            border-bottom: 1px solid #c9d4e5;
        }
        #sel_employee
        {
            width: 172px;
        }
        #sel_permissions
        {
            width: 172px;
        }
        input[type="text"]
        {
            height: 16px;
            width: 169px;
        }
        input[type="password"]
        {
            height: 16px;
            width: 169px;
        }
        .style1
        {
            width: 90px;
        }
        #div_tbHtml table
        {
            margin-left: 20px;
            width: 400px;
            border-top: 1px solid #99BBE8;
            border-left: 1px solid #99BBE8;
        }
        #div_tbHtml tr
        {
            height: 28px;
        }
        #div_tbHtml td
        {
            border-right: 1px solid #c9d4e5;
            border-bottom: 1px solid #c9d4e5;
        }
        #div_tbHtml th
        {
            border-right: 1px solid #c9d4e5;
            border-bottom: 1px solid #c9d4e5;
        }
        #tb_empIdList tr
        {
            height: 28px;
            background-color: white;
        }
        
        #tb_empIdList
        {
            width: 650px;
            border-top: 1px solid #99BBE8;
            border-left: 1px solid #99BBE8;
        }
        #tb_empIdList td
        {
            border-right: 1px solid #c9d4e5;
            border-bottom: 1px solid #c9d4e5;
            padding-left: 3px;
        }
    </style>
    <style type="text/css">
        .backRed
        {
            background-color: #dfe8f6;
        }
        .backMiddle
        {
            background-color: rgb(239,239,239);
        }
        .style2
        {
            height: 27px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            SHow_click("li_4");
            SHow_li_Selected("li_4", "customer.aspx");
        });
        function vlaedateFunc(sel_empId,customerId, pwd, pwd2, permission, createdate) {
            var b = true;

            var reg = /^\w{5,15}$/;
            if (!reg.test(customerId)) {
                $("#txt_customerId").next("span").first().text("数字或字母5-15位");
                b = false;
            } else {
                $("#txt_customerId").next("span").first().text("");
            }

            var reg = /^\w{6,15}$/;
            if (!reg.test(pwd)) {
                $("#txt_newpwd").next("span").first().text("*数字或字母6-15位");
                b = false;
            } else {
                $("#txt_newpwd").next("span").first().text("");
            }
          
            if (pwd != pwd2) {
                $("#txt_newpwd2").next("span").first().text("*输入的密码不一致");
                b = false;
            } else {
                $("#txt_newpwd2").next("span").first().text("");
            }
            if (createdate == "") {
                $("#txt_createdate").next("span").first().text("*时间不能为空");
                b = false;
            } else {
                $("#txt_createdate").next("span").first().text("");
            }
            if (sel_empId == "") {
                $("#sel_employee").next("span").first().text("*选择关联后台的用户");
                b = false;
            } else {
                $("#sel_employee").next("span").first().text("");
            }
            return b;
        }
        function btn_addCustomer_onclick() {
            $("#hid_valedate").val("0");
            var customerId = $("#txt_customerId").val().trim();

            var pwd = $("#txt_newpwd").val().trim();
            var pwd2 = $("#txt_newpwd2").val().trim();
            var permission = $("#sel_permissions").val();
            var createdate = $("#txt_createdate").val().trim();
            var sel_empId = $("#sel_employee").val();
            var b = vlaedateFunc(sel_empId, customerId, pwd, pwd2, permission, createdate);
          
            if (b) {
                $("#hid_valedate").val("1");
            }

            return b;
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
                <div>
                    <h4>
                        &nbsp;搜索</h4>
                    &nbsp;<input type="text" id="txt_selectKey" runat="server" />&nbsp;
                    <select runat="server" id="sel_empType">
                        <option value="0">员工账号</option>
                        <option value="1">员工名</option>
                    </select>&nbsp;
                    <input type="button" value="查询" name="btn_select" id="btn_select" onserverclick="btn_select_Click"
                        runat="server"  />
                    <hr />
                </div>
                <div id="div_main">
                    <div style="float: left; width: 40%">
                        <div style="width: 25px; width: 420px; margin-bottom: 5px; color: #15428b;">
                            &nbsp; 客户 添加/修改/删除</div>
                        <table id="tb_empid" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align: right;" class="style1">
                                    后台员工:&nbsp;
                                </td>
                                <td>
                                     <select id="sel_employee" runat="server">
                                     
                                    </select><span
                                        style="color: red"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="style1">
                                    前台账号:&nbsp;
                                </td>
                                <td>
                                    <input id="txt_customerId" runat="server"  type="text" /><span style="color: red"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="style1">
                                   新密码:&nbsp;
                                </td>
                                <td>
                                    <input id="txt_newpwd" runat="server" type="password" /><span style="color: red"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="style1">
                                    新密码确认:&nbsp;
                                </td>
                                <td>
                                    <input id="txt_newpwd2" runat="server" type="password" /><span style="color: red"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="style1">
                                    权限:&nbsp;
                                </td>
                                <td>
                                    <select id="sel_permissions" runat="server">
                                        <option value="4">前台高级账号</option>
                                    </select>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="text-align: right;" class="style1">
                                    创建日期:&nbsp;
                                </td>
                                <td>
                                    <input id="txt_createdate" class="Wdate" onfocus="WdatePicker()" type="text" runat="server" /><span
                                        style="color: red"></span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-left: 150px;" class="style2">
                                    <asp:Button ID="btn_addCustomer" Text="添加" OnClientClick="return btn_addCustomer_onclick();" OnClick="btn_addCustomer_onclick"
                                        runat="server" />&nbsp; 
                             <%--       <asp:Button ID="btn_updateCustomer" Text="修改" OnClientClick="return btn_updateCustomer_onclick();" OnClick="btn_updateCustomer_onclick"
                                        runat="server" />&nbsp; 
                                           <asp:Button ID="btn_deleteCustomer" Text="删除" OnClientClick="return btn_deleteCustomer_onclick();" OnClick="btn_deleteCustomer_onclick"
                                        runat="server" />&nbsp; --%>
                                    &nbsp;
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                   <div style="float: left; width: 60%">
                        <div style="width: 25px; width: 640px; margin-bottom: 5px; color: #15428b;">
                            &nbsp;员工列表</div>
                        <div id="div_empIdList" style="width: 100%; height: 680px; overflow-y: scroll; overflow-x: none;">
                            <table id="tb_empIdList" cellspacing="0" cellpadding="0">
                                <tr id="tr_emp_title" style="background: #eff0f2;">
                                    <td style="width: 20%;">
                                        客户账号
                                    </td>
                                    <td style="width: 20%;">
                                        客户名
                                    </td>
                                    <td style="width: 20%;">
                                        权限
                                    </td>
                                    <td style="width: 20%;">
                                       关联账号
                                    </td>
                                    <td style="width: 20%;">
                                        创建时间
                                    </td>
                                </tr>
                                <asp:Repeater ID="rep_empCustomerList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%#Eval("CustomerId")%>
                                            </td>
                                            <td>
                                                <%#Eval("EmpName")%>
                                            </td>
                                            <td>
                                                <%#FormatPromession(DataBinder.Eval(Container.DataItem, "UserPermissions").ToString())%>
                                            </td>
                                            <td>
                                              <%#Eval("EmpId")%>
                                            </td>
                                            <td>
                                                <%#Eval("CreateDate","{0:yyyy-MM-dd}")%>
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
    </div>
       <input type="hidden" id="hid_cate_click" value="" />
       <input type="hidden" runat="server" id="hid_valedate" value="0" />
    </form>
</body>
</html>
