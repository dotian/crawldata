<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employee_a.aspx.cs" Inherits="WebCrawler.Admin.employee_a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <style type="text/css">
        body
        {
            font-size:80%;
        }
        table tr
        {    
            height:30px;
             border:0px;
             margin:0px;
        }
        .sp_left
        {
            width: 30%;
            text-align: right;
           
        }
        .sp_right
        {
            padding-left: 10px;
            width: 375px;
            text-align: left;
        }
        input[type='text']
        {
             width:155px;
        }
         input[type='password']
        {
             width:155px;
        }
        #btn_login
        {
            width: 54px;
        }
    </style>
      <link href="../Common/mSelect/mSelect.css" rel="stylesheet" type="text/css" />
   
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Common/JS/menu.js" type="text/javascript"></script>
    <script src="../Common/mSelect/mSelect.js" type="text/javascript"></script>
     
    <script type="text/javascript">

     
        $(document).ready(function () {
            SHow_click("li_2");
            if ($("#hid_adminPermission").val() != "1") {
                $("#div_showMess").fadeIn("fast");
                //定时器跳转
                countDown(10);
            } else {
                $("#div_showMess").hide();
            }
        });
        function countDown(secs) {
            if (--secs > 0) {
                $("#div_showMess span").text("请用管理员帐号登录," + secs + "秒以后自动跳转至登录页面!");
                setTimeout("countDown(" + secs + ")", 1000);
            } else {
              window.location.href = "login.aspx";
            }
        }
        function btn_addAccount_click() {
           
             //这里 统一验证
            valedate_txtname($("#txt_name"));
            valedate_txtpwd($("#txt_pwd"));
            valedate_txtempname($("#txt_empname"));
            sel_permission_change($("#sel_permission"));

            if ($("#hid_adminPermission").val() != "1") {
                //定时器跳转
                //countDown(10);
                return false;
            }

            var username = $("#txt_name").val().trim();
            var pwd = $("#txt_pwd").val().trim();
            var empname = $("#txt_empname").val().trim();
        

            var permission = parseInt($("#sel_permission").val());
            if (permission <= 0) {
                $("#sel_permission").parent().find("span").text("*请选择权限!");
                return false;
            }

            $.post("ashxHelp/HandlerAddEmployee.ashx", { username: username, pwd: pwd, empname: empname, permission: permission }, function (data) {
                if (data == "添加成功!") {
                    alert("添加成功!");
                    $("#txt_name").val("");
                    $("#txt_pwd").val("");
                    $("#txt_empname").val("");
                    $("#sel_permission").val("0");
                } else {
                    alert(data);
                }
            });
        }
        function valedate_txtname(t) {
            // alert("a");
            var username = $(t).val().trim();
            var reg = /^\w{5,15}$/;
            if (username.length < 5 | username.length > 15 | !reg.test(username)) {
                $(t).parent().find("span").text("*长度5~15位,只能使用字母或数字");
                $("#hid_valedate").val("false");
                $(t).focus();
            } else {
                $("#hid_valedate").val("true");
                $(t).parent().find("span").text("");
            }
        }
        function valedate_txtpwd(t) {
            var pwd = $(t).val().trim();
            var reg = /^\w{5,15}$/;
            if (pwd.length < 5 | pwd.length > 15 | !reg.test(pwd)) {
                $(t).parent().find("span").text("*长度5~15位,只能使用字母或数字");
                $("#hid_valedate").val("false");
                $(t).focus();
            } else {
                $("#hid_valedate").val("true");
                $(t).parent().find("span").text("");
            }
        }
        function valedate_txtempname(t) {
            var empname = $(t).val().trim();
            if (empname.length < 2 | empname.length > 15) {
                $(t).parent().find("span").text("*人名2到15位");
                $("#hid_valedate").val("false");
                $(t).focus();
            } else {
                $("#hid_valedate").val("true");
                $(t).parent().find("span").text("");
            }
        }

        function sel_permission_change(t) {
            var permission = parseInt((t).val());
            if (permission > 0) {
                $(t).parent().find("span").text("");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div  style=" width:100%; height:250px;">
      <div id="div_showMess" 
            style=" width:485px; height:62px; margin:80px auto 0px auto; font-weight:700; font-size:150%; background:#dfe8f6; padding-top:20px; color:Red; text-align:center;">
          <span></span>
      </div>

      <br />

    </div>
    <div style="margin: 0px auto; width: 450px; border:1px solid #99bbe8;">
        <div style="width: 100%; height:22px; text-align: center; background-image:url('../Image/login_b.jpg'); background-repeat:repeat-x;">
            <div style="width: 100%; padding:0px; margin:0px; height:4px;"> </div>
           <div><span style="">添加用户</span></div>
        </div>
        <div style="line-height: 26px;">
        <div style="width:100%; height:3px;background-color:#dfe8f6;"></div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;background-color:#dfe8f6;">
                <tr>
                    <td class="sp_left">
                        登录名:
                    </td>
                    <td class="sp_right">
                 <%--   onblur="valedate_txtname(this);" --%>
                        <input id="txt_name" value="" type="text" /><span style=" color:Red;"></span>
                    </td>
                   
                </tr>
                <tr>
                    <td class="sp_left">
                        密码:
                    </td>
                    <td class="sp_right">
                 <%--   onblur="valedate_txtpwd(this);"--%>
                        <input type="password" value=""  id="txt_pwd" /><span style=" color:Red;"></span>
                    </td>
                </tr>
                 <tr>
                    <td class="sp_left">
                      员工名:
                    </td>
                    <td class="sp_right">
                   <%-- onblur="valedate_txtempname(this);"--%>
                        <input id="txt_empname" value=""  type="text" /><span style=" color:Red;"></span>
                    </td>
                </tr>
                  <tr>
                    <td class="sp_left">
                        权限：
                    </td>
                    <td class="sp_right">
                        <select id="sel_permission" msty="redLine" onchange="sel_permission_change(this);"  style=" width:160px; height:21px;">
                            <option value="0">--请选择权限--</option>
                            <option value="1">后台低级用户</option>
                            <option value="2">后台高级用户</option>
                        <%--    <option value="3">前台低级用户</option>
                            <option value="4">前台高级用户</option>--%>
                        </select>&nbsp;<span style=" color:Red;"></span>
                    </td>
                </tr>
                 <tr style=" height:40px;">
                    <td colspan="2" style=" text-align:center;"><input type="button" id="btn_addAccount" onclick="btn_addAccount_click();" value="添加" /></td>
                 </tr>
            </table>
        </div>
    </div>
    <input type="hidden" runat="server" id="hid_adminPermission" value="0" />
    <input type="hidden" id="hid_valedate" value="false" />
    </form>
</body>
</html>
