<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebCrawler.index" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" " http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>舆情监控系统</title>
<style type="text/css">
body {
	background-image: url(img/background.jpg);
	background-repeat:no-repeat;
}

#div-1{
	position:relative;
	top:320px;
	left:320px;
}

#div-2{
	position:relative;
	top:210px;
	left:600px;
}

input
{
	width:120px;
}
td.name
{
	text-align:right;
	font-family:宋体;
}

</style>

    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
</head>
<script type="text/javascript" language="javascript">
    function login() {

        var user = $("#user").val().trim();
        var pwd = $("#pwd").val().trim();
        var code = $("#rand").val().trim();

        $.post("hankookAshx/login.ashx", { username: user, pwd: pwd, code: code }, function (data) {
            if (data == "登录成功") {
                window.location.href = "projectview.aspx";
            } else {
                loadimage();//重新刷新验证码
                alert(data);
            }

        });

       
    }

    function loadimage() {
        document.getElementById("randImage").src = "Admin/VerifyCode.aspx?" + Math.random();
    }

   
    function picShow() {
        var pic = document.getElementById("pic");
        pic.src = "img/login1.png";
    }

    function picShow1() {
        var pic = document.getElementById("pic");
        pic.src = "img/login.png";
    }
</script>
<body>
<form>
<div id="div-1">
<table width="280"  border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td class="name">用户名:&nbsp;</td>
		<td><input type = "text" value="" name="user" id="user"/></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td class="name">密&nbsp;&nbsp;码:&nbsp;</td>
		<td><input type = "password" name="pwd" id="pwd"/></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td class="name">验证码:&nbsp;</td>
		<td><input type="text" name="rand" id="rand" size="15" />&nbsp;&nbsp;<img alt="code..." name="randImage" id="randImage" src="Admin/VerifyCode.aspx" width="60" height="20" border="1" align="absmiddle" /></td>
	</tr>
</table>
</div>
<div id="div-2">
<table width="80%"  border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td><img src="img/login.png" id="pic" onmouseover="javascipt:picShow()" onmouseout="javascipt:picShow1()" onclick="javascipt:login()"/></td>
	</tr>
</table>

</div>
</form>
</body>
</html>
