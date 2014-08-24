<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add_category.aspx.cs" Inherits="WebCrawler.Admin.add_category" %>

<%@ Register src="../UserControl/MenuUserControl.ascx" tagname="MenuUserControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/lyoatMenu.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Common/JS/menu.js" type="text/javascript"></script>


    <style type="text/css">
        #div_main
        {
            line-height: 20px;
        }
        #tb_project_add
        {
            margin-left: 20px;
            width: 400px;
            border-top:1px solid #99BBE8;
            border-left:1px solid #99BBE8;
            border-right:1px solid #c9d4e5;
        }
        #tb_project_add tr
        {
            height: 28px;
          
        }
        #tb_project_add td
        {
           
            border-bottom:1px solid #c9d4e5;
        }
        #sel_MatchType
        {
            width: 172px;
        }
        #sel_SearchType
        {
            width: 172px;
        }
        input[type="text"]
        {
             height:16px;
            width: 169px;
        }
        .style1
        {
            width: 127px;
        }
    </style>
    <title></title>


    <script type="text/javascript">

        function btn_resetCategory_click() {
            $("#txt_CategoryName").val("");

        }
        function btn_addCategory_click() {
            var cateName = $("#txt_CategoryName").val();
            var empname = $("#txt_EmpName").val();
            if (empname == "") {
                alert("没有登录!");
                return false;
            }

            var cateClass = $("input[name='cateClass']:checked").val();
            cateClass = parseInt(cateClass);

            add_category.addCategory_click(cateName, empname, cateClass, function (data) {
                var cateId = parseInt(data.value);
                if (cateId > 0) {
                    //跳转
                    location.href = "category.aspx?cateId=" + cateId;
                } else {
                    alert("添加失败!");
                }
            });
           
        }
    </script>
   <script type="text/javascript">

       $(document).ready(function () {
           $("#ul_menu ul").each(function () {
               $(this).hide();
           });
           SHow_click("li_1");
           SHow_li_Selected("li_1", "add_category.aspx");
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
                <h4>
                    &nbsp;添加分类</h4>
                <hr />
                <div id="div_main">
                    <table id="tb_project_add" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style=" text-align:right;" class="style1">
                                分类名称:&nbsp;
                            </td>
                            <td>
                                <input id="txt_CategoryName" runat="server" type="text" /><br />
                            </td>
                        </tr>
                     
                        <tr>
                            <td style=" text-align:right;" class="style1">
                                添加人:&nbsp;
                            </td>
                            <td>
                                <input id="txt_EmpName" type="text" runat="server" readonly="readonly" /><br />
                            </td>
                        </tr>
                        <tr>
                         <td colspan="2">
                             <div id="div_CategoryClass"  style=" width:217px; margin:0px auto;">

                             <input type="radio"  name="cateClass"  checked="checked" value="1" />论坛
                             <input type="radio"  name="cateClass" value="2" />新闻
                             <input type="radio"  name="cateClass" value="3" />博客
                             <input type="radio"  name="cateClass" value="5" />微博
                             </div>
                         </td>
                        </tr>
                        <tr>
                            <td colspan="2" style=" padding-left:140px;">
                                <img alt="" id="btn_addCategory" onclick="btn_addCategory_click();" src="../Image/add_click.jpg" />&nbsp;
                                <img alt="" id="btn_resetCategory" onclick="btn_resetCategory_click();" src="../Image/reset.jpg" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

