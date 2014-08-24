<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employee.aspx.cs" Inherits="WebCrawler.Admin.employee" %>

<%@ Register Src="../UserControl/MenuUserControl.ascx" TagName="MenuUserControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        #sel_permissions
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
            width: 100px;
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
    </style>
    <script type="text/javascript">
        $.fn.getHexBackgroundColor = function () { var rgb = $(this).css('background-color'); if (!$.browser.msie) { rgb = rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/); function hex(x) { return ("0" + parseInt(x).toString(16)).slice(-2); } rgb = "#" + hex(rgb[1]) + hex(rgb[2]) + hex(rgb[3]); } return rgb; }

        $(document).ready(function () {
            SHow_click("li_4");
            SHow_li_Selected("li_4", "employee.aspx");

            $("#tb_empIdList tr").click(function () {
                var id = $(this).attr("id");
                if (id == "tr_emp_title") {
                    return;
                }
                $("tr.backRed").css("backgroundColor", "rgb(255,255,255)");
                $(this).addClass("backRed");
                $(this).css("backgroundColor", "rgb(223,232,246)");

                var a = $(this).children();
                var arr = new Array();
                for (var i = 0; i < a.length; i++) {
                    arr[arr.length] = a.eq(i).text();
                }

                FiltValue(arr);
            });
        });

        function FiltValue(arr) {
            var empId = arr[0].trim();
            if (empId == "员工账号") {
                return;
            }
            var empName = arr[1].trim();
            var promession = arr[2].trim();
            var createdate = arr[3].trim();

            $("#txt_empId").val(empId);
            $("#txt_empName").val(empName);
            if (promession == "后台高级用户") {
                $("#sel_permissions").val(2);
            } else {
                $("#sel_permissions").val(1);
            }

            $("#txt_EndDate").val(createdate);
            $("#txt_newpwd").val("");
            $("#txt_newpwd2").val("");
            $("#tb_empid span").text("");
        }
        function btn_addEmp_onclick() {
            $("#hid_valedate").val("0");
            var empId = $("#txt_empId").val().trim();
            var empName = $("#txt_empName").val().trim();
            var pwd = $("#txt_newpwd").val().trim();
            var pwd2 = $("#txt_newpwd2").val().trim();
            var permission = $("#sel_permissions").val();
            var createdate = $("#txt_createdate").val().trim();
            var b = vlaedateFunc(empId, empName, pwd, pwd2, permission, createdate);
            if (b) {
                $("#hid_valedate").val("1");
            }
            return b;
        }

        function vlaedateFunc(empId, empName, pwd, pwd2, permission, createdate) {
            var b = true;
            var reg = /^\w{5,15}$/;
            if (!reg.test(empId)) {
                $("#txt_empId").next("span").first().text("数字或字母5-15位");
                b = false;
            } else {
                $("#txt_empId").next("span").first().text("");
            }
            if (empName == "") {
                $("#txt_empName").next("span").first().text("*员工名必填");
                b = false;
            } else if (empName.length > 20) {
                $("#txt_empName").next("span").first().text("*员工名长度必须小于20");
                b = false;
            } else {
                $("#txt_empName").next("span").first().text("");
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
            return b;
        }

        function btn_updateEmp_onclick() {
            $("#hid_valedate").val("0");
            var empId = $("#txt_empId").val().trim();
            var empName = $("#txt_empName").val().trim();
            var pwd = $("#txt_newpwd").val().trim();
            var pwd2 = $("#txt_newpwd2").val().trim();
            var permission = $("#sel_permissions").val();
            var createdate = $("#txt_createdate").val().trim();
            var b = vlaedateFunc(empId, empName, pwd, pwd2, permission, createdate);
            if (b) {
                $("#hid_valedate").val("1");
            }
            return b;
        }
        function btn_deleteEmp_onclick() {
            $("#hid_valedate").val("0");
            var empId = $("#txt_empId").val().trim();
            var b = true;
            if (empId == "") {
                $("#txt_empId").next("span").first().text("数字或字母5-15位");
                b = false;
            } else {
                b = true;
                $("#txt_empId").next("span").first().text("");
            }
            if (b) {
                if (confirm('确定要删除吗?')) {
                    $("#hid_valedate").val("1");
                }
                else {
                    b = false;
                }
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
                        runat="server" />
                    <hr />
                </div>
                <div id="div_main">
                    <div style="float: left; width: 40%">
                        <div style="width: 25px; width: 420px; margin-bottom: 5px; color: #15428b;">
                            &nbsp;员工 添加/修改/删除</div>
                        <table id="tb_empid" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align: right;" class="style1">
                                    员工账号:&nbsp;
                                </td>
                                <td>
                                    <input id="txt_empId" runat="server" type="text" /><span style="color: red"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="style1">
                                    员工名:&nbsp;
                                </td>
                                <td>
                                    <input id="txt_empName" runat="server" type="text" /><span style="color: red"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="style1">
                                    <span style="color: red"></span>新密码:&nbsp;
                                </td>
                                <td>
                                    <input id="txt_newpwd" runat="server" type="password" /><span style="color: red"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="style1">
                                    <span style="color: red"></span>新密码确认:&nbsp;
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
                                        <option value="1">后台低级用户</option>
                                        <option value="2">后台高级用户</option>
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
                                <td colspan="2" style="padding-left: 100px;">
                                    <asp:Button Text="添加" OnClientClick="return btn_addEmp_onclick();" OnClick="btn_addEmp_onclick"
                                        runat="server" />&nbsp;
                                    <asp:Button ID="btn_updateEmp" Text="修改" OnClientClick="return btn_updateEmp_onclick();"
                                        OnClick="btn_updateEmp_onclick" runat="server" />&nbsp;
                                    <asp:Button ID="btn_deleteEmp" Text="删除" OnClientClick="return btn_deleteEmp_onclick()"
                                        OnClick="btn_deleteEmp_onclick" runat="server" />&nbsp;
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
                                    <td style="width: 25%;">
                                        员工账号
                                    </td>
                                    <td style="width: 25%;">
                                        员工名
                                    </td>
                                    <td style="width: 25%;">
                                        权限
                                    </td>
                                    <td style="width: 25%;">
                                        创建时间
                                    </td>
                                </tr>
                                <asp:Repeater ID="rep_empIdList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%#Eval("EmpId")%>
                                            </td>
                                            <td>
                                                <%#Eval("EmpName")%>
                                            </td>
                                            <td>
                                                <%#FormatPromession(DataBinder.Eval(Container.DataItem, "UserPermissions").ToString())%>
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
