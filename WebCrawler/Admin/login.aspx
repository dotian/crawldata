<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebCrawler.Admin.Login" %>

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
            width: 202px;
            text-align: left;
        }
        input[type='text']
        {
             width:150px;
              height:18px;
        }
         input[type='password']
        {
             width:150px;
               height:18px;
        }
        #btn_login
        {
            width: 54px;
        }
        .img_code_style
        {
             margin-top:3px;
        }
    </style>

    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txt_name").focus();
        });
        function btn_login_click() {
            var loginValedate = true;
            var username = $.trim($("#txt_name").val());
            if (username == "") {
                loginValedate = false;
                alert("登录名不能为空!");
                return false;
             }
            var pwd = $.trim($("#txt_pwd").val());
            if (pwd == "") {
                loginValedate = false;
                alert("密码不能为空!");
                return false;
             }
            var code = $.trim($("#txt_code").val());
            if (code == "") {
                loginValedate = false;
                alert("验证码不能为空!");
                return false;
            }

            $.post("ashxHelp/HandlerLogin.ashx", { username: username, pwd: pwd, code: code }, function (data) {
                if (data == "登录成功") {
                    loginResult = true;
                    var loginAfterUrl = $("#hid_loginAfterUrl").val();
                    if (loginAfterUrl == "") {
                        location.href = "projectlist.aspx";
                    } else {
                        location.href = loginAfterUrl;
                    }
                } else {
                    alert(data);
                    loginResult = false;
                }
            });
            return loginResult;
        }

        function checkLogin(t,evt) {
            var code = $.trim($(t).val());
            if (code.length==4) {
                evt = (evt) ? evt : ((window.event) ? window.event : "")
                keyCode = evt.keyCode ? evt.keyCode : (evt.which ? evt.which : evt.charCode);
                if (keyCode == 13) {
                    btn_login_click();
                } 
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 250px auto; width: 380px; border:1px solid #99bbe8;">
        <div style="width: 100%; height:22px; text-align: center; background-image:url('../Image/login_b.jpg'); background-repeat:repeat-x;">
            <div style="width: 100%; padding:0px; margin:0px; height:4px;"> </div>
           <div><span style="">管理员登录</span></div>
        </div>
        <div style="line-height: 26px;">
        <div style="width:100%; height:3px;background-color:#dfe8f6;"></div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;background-color:#dfe8f6;">
                <tr>
                    <td class="sp_left">
                        用户名:
                    </td>
                    <td class="sp_right">
                        <input runat="server" id="txt_name" value="" type="text" />
                    </td>
                </tr>
                <tr>
                    <td class="sp_left">
                        密码:
                    </td>
                    <td class="sp_right">
                        <input runat="server" type="password" value="admin001" id="txt_pwd" />
                    </td>
                </tr>
                <tr>
                    <td class="sp_left">
                        验证码:
                    </td>
                    <td class="sp_right">
                    <div style=" float:left; padding-top:2px;"><input id="txt_code" onkeydown="checkLogin(this,event);"style="width:80px;" type="text" /></div>
                     <div style=" float:left; margin-left:8px;">
                       <asp:Image  CssClass="img_code_style" ID="img_code"  ImageUrl="VerifyCode.aspx" runat="server" Width="56px" />
                              </div>
                    </td>
                </tr>
                 <tr style=" height:40px;">
                 <td colspan="2" style=" text-align:center;">
                   <input type="button" id="btn_login" onclick="btn_login_click();" value="登入" />
                 </td>
                 </tr>
            </table>
        </div>
      
    </div>

   <input type="hidden" runat="server" value="" id="hid_loginAfterUrl" />
    </form>
</body>
</html>
