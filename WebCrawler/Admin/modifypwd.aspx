<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modifypwd.aspx.cs" Inherits="WebCrawler.Admin.ModifyPwd" %>

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
        }
         input[type='password']
        {
             width:150px;
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
     <script src="../Common/JS/menu.js" type="text/javascript"></script>
    <script type="text/javascript">
        function btn_modifyPwd_click() {
                var oriPwd = $.trim($("#txt_oriPwd").val());
                if (oriPwd == "") {
                    $("#txt_oriPwd").parent().find("span").text("*原始密码不能为空");
                    $("#txt_oriPwd").focus();
                    return false;
                }
                var pwd = $.trim($("#txt_pwd").val());
                if (oriPwd == "") {
                    $("#txt_pwd").parent().find("span").text("*新密码不能为空");
                    $("#txt_pwd").focus();
                    return false;
                }
                var pwd2 = $.trim($("#txt_pwd2").val());
                if (oriPwd == "") {
                    $("#txt_pwd2").parent().find("span").text("*确认密码不能为空");
                    $("#txt_pwd2").focus();
                    return false;
                }
                var code = $.trim($("#txt_code").val());
                if (code == "") {
                    $("#txt_code").parent().find("span").text("*请输入验证码");
                    $("#txt_code").focus();
                    return false;
                }
                return true;
            
           //修改密码
        }

        

        //原始密码
        function valedate_txtpwdOri() {
            var pwd = $.trim($(t).val());
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
        //新密码
        function valedate_txtpwd(t) {
            var pwd = $.trim($(t).val());
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
        //新密码确认密码
        function valedate_txtpwd2(t) {
            var pwd = $.trim($(t).val());
            var reg = /^\w{5,15}$/;
            var pwd1 = $.trim($("#txt_pwd").val());
            if (pwd.length < 5 | pwd.length > 15 | !reg.test(pwd)) {
                $(t).parent().find("span").text("*长度5~15位,只能使用字母或数字");
                $("#hid_valedate").val("false");
                $(t).focus();
            } else if (pwd1 != pwd) {
                $(t).parent().find("span").text("*两次输入的密码不一致");
                $("#hid_valedate").val("false");
                $(t).focus();
            } else {
                $("#hid_valedate").val("true");
                $(t).parent().find("span").text("");
            }  
        }
     
    </script>
</head>
<body>
<body>
    <form id="form1" runat="server">
    <div style="margin: 250px auto; width: 450px; border:1px solid #99bbe8;">
        <div style="width: 100%; height:22px; text-align: center; background-image:url('../Image/login_b.jpg'); background-repeat:repeat-x;">
            <div style="width: 100%; padding:0px; margin:0px; height:4px;"> </div>
           <div><span style="">修改密码</span></div>
        </div>
        <div style="line-height: 26px;">
        <div style="width:100%; height:3px;background-color:#dfe8f6;"></div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;background-color:#dfe8f6;">
                <tr>
                    <td class="sp_left">
                        旧密码:
                    </td>
                    <td class="sp_right">
                        <input id="txt_oriPwd" runat="server" value="" onblur="valedate_txtpwdOri(this);" type="password" /><span style=" color:Red;"></span>
                    </td>
                </tr>
                <tr>
                    <td class="sp_left">
                        新密码:
                    </td>
                    <td class="sp_right">
                        <input type="password" runat="server" value="" onblur="valedate_txtpwd(this);" id="txt_pwd" /><span style=" color:Red;"></span>
                    </td>
                </tr>
                  <tr>
                    <td class="sp_left">
                        确认密码：
                    </td>
                    <td class="sp_right">
                         <input type="password" runat="server" value="" onblur="valedate_txtpwd2(this);" id="txt_pwd2" /><span style=" color:Red;"></span>
                    </td>
                </tr>
                <tr>
                    <td class="sp_left">
                        验证码:
                    </td>
                    <td class="sp_right">
                    <div style=" float:left;"><input id="txt_code" runat="server" style="width:67px;" type="text" /></div>
                       <div style=" float:left;">&nbsp;&nbsp; <asp:Image Height="20px" CssClass="img_code_style"  ID="img_code" ImageUrl="VerifyCode.aspx" runat="server" /></div>
                    <span style=" color:Red;"></span>
                    </td>
                </tr>
                 <tr style=" height:40px;">
                    
                 <td colspan="2" style=" text-align:center;"> 
                     <asp:Button ID="btn_modifyPwd" 
                         runat="server" OnClientClick="return btn_modifyPwd_click();" Text="确定" onclick="btn_modifyPwd_Click" /></td>
                 </tr>
            </table>
        </div>
      
    </div>
    <input type="hidden" id="hid_valedate" name="hid_valedate" value="false" />
   <input type="hidden" runat="server" value="" id="hid_loginAfterUrl" />
   &nbsp;</form>
</body>
</body>
</html>
