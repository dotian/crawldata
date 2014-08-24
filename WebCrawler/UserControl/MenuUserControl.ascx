<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuUserControl.ascx.cs" Inherits="WebCrawler.UserControl.MenuUserControl" %>
<style type="text/css">
    #ul_menu
    {
       
        padding: 0px;
        margin: 0px;
        height: 700px;
    }
    #ul_menu li
    {
        list-style-type: none;
    }
    .li_one
    {
        height: auto;
        line-height: 23px;
        width: 153px;
        background-color: #D9E7F8;
        border-bottom: 1px solid #AACCF6;
    }
   
    .li_one a
    {
        text-decoration: none;
    }
    .li_one ul
    {
        background-color: White;
        height: 200px;
    }
    .li_one li
    {
        margin: 0px;
        padding: 0px;
        border-bottom: 1px solid #AACCF6;
        text-align: center;
        margin-left: -40px;
      
    }
    #ul_menu li ul li a
    {
       color:rgb(135,88,8);
    }
</style>
<div style="width: 160px; height: 20px; margin-top: 5px;">
    &nbsp;导航栏</div>
<ul id="ul_menu">
    <li id="li_1" class="li_one" onclick="SHow_click(this.id);">&nbsp;<span><a href="#" style=" width:140px;color:rgb(34,34,34);">数据采集</a></span>
        <ul>
            <li><a href="category.aspx">分类维护</a></li>
            <li><a href="add_category.aspx">添加分类</a></li>
        </ul>
    </li>
    <li id="li_2" class="li_one" onclick="SHow_click(this.id);"><span>&nbsp;<a href="#" style="color:rgb(34,34,34);">项目管理</a></span>
        <ul>
            <li><a href="projectlist.aspx">项目列表</a></li>
            <li><a href="add_project.aspx">添加项目</a></li>
            <li><a href="taglist.aspx">标签管理</a></li>
        </ul>
    </li>
    <li id="li_3" class="li_one" onclick="SHow_click(this.id);"><span>&nbsp;<a href="#"  style="color:rgb(34,34,34);">舆情监控系统</a></span>
        <ul>
            <li><a href="../index.aspx" target="_blank">前台登录</a></li>
        </ul>
    </li>
   
    <li id="li_4" class="li_one" onclick="SHow_click(this.id);"><span>&nbsp;<a href="#"  style="color:rgb(34,34,34);">系统管理</a></span>
        <ul>
         <li><a href="employee.aspx">用户管理</a></li>
          <li><a href="customer.aspx">客户管理</a></li>
            <li><a href="contend.aspx">竞争社管理</a></li>
        </ul>
    </li>
    <li id="li_5" class="li_one" onclick="SHow_click(this.id);"><span>&nbsp;<a href="#"  style="color:rgb(34,34,34);">其它</a></span>
        <ul>
               
            <li><a href="modifypwd.aspx">修改密码</a></li>
             <li><a href="login.aspx">退出</a></li>
        </ul>
    </li>
</ul>
<div style="display: none">
    <input type="hidden" value="" id="txt_id" />
    <input type="hidden" value="0" id="txt_li" />
    <input type="hidden" value="" id="txt_li_a_href" />
</div>
