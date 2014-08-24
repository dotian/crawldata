<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer.aspx.cs" Inherits="WebCrawler.customer" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" language="javascript">
    var gContextpath = "";
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title runat="server" id="title_title">舆情监控系统</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <link href="css/share.css" rel="stylesheet" type="text/css" />
    <link href="css/cssmain.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .logo_change
        {
		    width:153px;
		    height:35px;
		    margin:15px 0px 0px 5px;
		    padding:0px;
        }
        .logo_change img
        {
            margin:0px;
            width:auto;
            height:auto;
		    padding:0px;
        }
        .greenImgBg
        {
             background:url(../img/small_green.jpg) no-repeat;
        }
        
        .orangeImgBg
        {
             background:url(../img/small_orange.jpg) no-repeat;
        }
       
        .nav_1
        {
            display:block;
		    width:100%;
		    height:100%;
		    text-align:center;
		    color:#fff;
        }
          .nav_1 .greenImgBg
	    {
		    width:172px;
		    height:30px;		
		    line-height:30px;
		    color:#fff;
		    font-weight:bold;
	    }
	    .greenImgBg a
	    {
		    display:block;
		    width:100%;
		    height:100%;
		    text-align:center;
		    color:#fff;
	    }
	    .nav_1 .orangeImgBg
	    {
	         width:172px;
		    height:30px;		
		    line-height:30px;
		    color:#fff;
		    font-weight:bold;
	    }
	    .orangeImgBg a
	    {
	         display:block;
		    width:100%;
		    height:100%;
		    text-align:center;
		    color:#fff;
	    }
	    
	     .li_txtleft
	    {
	        text-align:left; margin-left:8px;
	    }
	    
    </style>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>

    <script type="text/javascript">
   
        //+---------------------------------------------------  
        //| 字符串转成日期类型   
        //| 格式 MM/dd/YYYY MM-dd-YYYY YYYY/MM/dd YYYY-MM-dd  
        //+---------------------------------------------------  
        function StringToDate(DateStr) {

            var converted = Date.parse(DateStr);
            var myDate = new Date(converted);
            if (isNaN(myDate)) {
                //var delimCahar = DateStr.indexOf('/')!=-1?'/':'-';  
                var arys = DateStr.split('-');
                myDate = new Date(arys[0], --arys[1], arys[2]);
            }
            return myDate;
        }

        //---------------------------------------------------  
        // 日期格式化  
        // 格式 YYYY/yyyy/YY/yy 表示年份  
        // MM/M 月份  
        // W/w 星期  
        // dd/DD/d/D 日期  
        // hh/HH/h/H 时间  
        // mm/m 分钟  
        // ss/SS/s/S 秒  
        //---------------------------------------------------  
        Date.prototype.Format = function (formatStr) {
            var str = formatStr;
            var Week = ['日', '一', '二', '三', '四', '五', '六'];

            str = str.replace(/yyyy|YYYY/, this.getFullYear());
            str = str.replace(/yy|YY/, (this.getYear() % 100) > 9 ? (this.getYear() % 100).toString() : '0' + (this.getYear() % 100));
            var month = this.getMonth() + 1;
            str = str.replace(/MM/, month > 9 ? month.toString() : '0' + month);
            str = str.replace(/M/g, this.getMonth());

            str = str.replace(/w|W/g, Week[this.getDay()]);

            str = str.replace(/dd|DD/, this.getDate() > 9 ? this.getDate().toString() : '0' + this.getDate());
            str = str.replace(/d|D/g, this.getDate());

            str = str.replace(/hh|HH/, this.getHours() > 9 ? this.getHours().toString() : '0' + this.getHours());
            str = str.replace(/h|H/g, this.getHours());
            str = str.replace(/mm/, this.getMinutes() > 9 ? this.getMinutes().toString() : '0' + this.getMinutes());
            str = str.replace(/m/g, this.getMinutes());

            str = str.replace(/ss|SS/, this.getSeconds() > 9 ? this.getSeconds().toString() : '0' + this.getSeconds());
            str = str.replace(/s|S/g, this.getSeconds());

            return str;
        }    
    </script>
    <script type="text/javascript">
        function show_title(i) {
            $(".tx_lt a,").hover(function (e) {
                var left_a = e.pageX + 20;
                var top_a = e.pageY;
                if (i == 'null') {
                    $(".pointQut_wod").css({ "display": "none" });
                    document.getElementById("pointQut").innerHTML = '<label></label>';

                } else {
                    $(".pointQut_wod").css({ "display": "block", "top": top_a, "left": left_a });
                    document.getElementById("pointQut").innerHTML = '<label><p>' + unescape(i) + '</p></label>';
                }
            }, function () {
                $(".pointQut_wod").css({ "display": "none" })
            })
        }

     
        function goAnalysis() {
            $.cookie('li_gb_id_ana', "li_gb_2");
            window.location.href = "analysis.aspx?type=sitedata&c=1";
        }

        function gobackPList() {
            window.location.href = "projectview.aspx";
        }
        function goIndex() {
            window.location.href = "index.aspx";
        }

        $(document).ready(function () {
            //aspnetpager 的样式
            $("#AspNetPager1").find("tr td").first().attr("valign", "middle");

            var li_gb_id = $.cookie('li_gb_id');
            if (li_gb_id != null) {
                //var ctrl = document.getElementById("li_gb_id");
                $(".greenImgBg").removeClass("greenImgBg").addClass("orangeImgBg");
                $("#" + li_gb_id).removeClass("orangeImgBg").addClass("greenImgBg");
            } else {
                $(".greenImgBg").removeClass("greenImgBg").addClass("orangeImgBg");
                $("#li_gb_2").removeClass("orangeImgBg").addClass("greenImgBg");
            }

            $(".nav_1 li").click(function () {
                $(".greenImgBg").removeClass("greenImgBg").addClass("orangeImgBg");
                var li_gb_id = $(this).parent().find("li").first().attr("id");
                $.cookie('li_gb_id', li_gb_id);
                $("#" + li_gb_id).removeClass("orangeImgBg").addClass("greenImgBg");
            });


            ShowLogImg();
        });

        function ShowLogImg() {
            var projecId = $("#hid_projectId").val();
            var imgUrl = "img/small_project_" + projecId + ".jpg";
            $("#img_logo").attr("src", imgUrl);
        }
        $(function () {
            $(".title").click(function () {
                $(".title").removeClass("hover");
                $(this).addClass("hover");
            });
           
            // 重新绑定
            $(".orangeImgBg").click(function () {
                //li_gb_2
               var li_gb_id = $(this).attr("id");

               $.cookie('li_gb_id', li_gb_id);
                //记下这个ID

                $(".greenImgBg").removeClass("greenImgBg").addClass("greenImgBg");
                $(this).removeClass("orangeImgBg").addClass("greenImgBg");
            });
        });
        
    </script>
</head>
<body>
    <form runat="server" id="form1">
    <div class="pointQut_wod">
        <span class="pointQut" id="pointQut">
            <label>
            </label>
        </span>
    </div>
    <div class="wrap">
        <div class="head fl">
                <div id="div_logo" runat="server" class="logo_change fl">
                 <img alt="" runat="server" id="img_logo" src="" />
                </div>
            <img alt="" src="img/puit.gif" onclick="javascipt:goIndex()" />
            <img  alt=""  src="img/small_img_02.jpg" onclick="javascipt:goAnalysis()" />
          <img  alt="" id="img_retplist"  runat="server" src="img/retplist.jpg" onclick="javascipt:gobackPList()" />
            <div class="split">
            </div>
        </div>
        <!--top over-->
        <div class="sideNav fl">
            <div class="nav">
                <ul class="nav_1">
              <%--  <li class="title" style="background-position: 0 -66px;">--%>
                    <li class="orangeImgBg" id="li_gb_1"><a href="customer.aspx?type=sitedata&c=2&a=0">
                        新闻 News</a></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=2&a=1">正面
                        Positive</a></label></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=2&a=2">中性
                        Neutral</a></label></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=2&a=3">负面
                        Negative</a></label></li>
                </ul>
                <ul class="nav_1">
                  <%-- <li class="title"> --%>
                    <li class="greenImgBg" id="li_gb_2"><a href="customer.aspx?type=sitedata&c=1&a=0">论坛 BBS</a></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=1&a=1">正面 Positive</a></label></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=1&a=2">中性 Neutral</a></label></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=1&a=3">负面 Negative</a></label></li>
                </ul>
                <ul class="nav_1">
                    <li class="orangeImgBg" id="li_gb_3"><a href="customer.aspx?type=sitedata&c=3&a=0">博客 BLOG</a></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=3&a=1">正面 Positive</a></label></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=3&a=2">中性 Neutral</a></label></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=3&a=3">负面 Negative</a></label></li>
                </ul>
                <ul class="nav_1" >
                    <li class="orangeImgBg" id="li_gb_4"><a href="customer.aspx?type=sitedata&c=5&a=0">微博 Microblog</a></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=5&a=1">正面 Positive</a></label></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=5&a=2">中性 Neutral</a></label></li>
                    <li><span>img</span><label  class="li_txtleft"><a href="customer.aspx?type=sitedata&c=5&a=3">负面 Negative</a></label></li>
                </ul>
                <ul class="nav_4">
                    <li class="title"><a runat="server" id="a_contendFhref" href="customer.aspx?type=sitedata&c=2&a=1&contendId="><span style="text-indent: 15px;
                        display: block; width: 150px; _width: 140px; background: none; margin-top: 13px;
                        color: #458eb7; font-weight: bold; text-align: center; _text-align: right; margin-left: 17px;
                        _margin-left: 5px;">竞争社 Competitor</span></a></li>

                    <asp:Repeater ID="rep_contend" runat="server">
                     <ItemTemplate>
                         <li><span>img</span><label><a href='customer.aspx?type=sitedata&c=2&a=1&contendId=<%#Eval("ContendId")%>'><%#Eval("ContendName")%></a></label></li>
                     </ItemTemplate>
                    </asp:Repeater>

                </ul>
                <ul class="nav_4" style="text-indent: 0px;">
                    <li class="title" style="background: url('img/hybg.jpg') no-repeat; background-position: -1px 0px;
                        background-position: 0px 0px; width: 176px;"><a href="customer.aspx?type=report"
                            style="color: #000"></a></li>
                </ul>
                <br />
                <ul class="nav_5">
                    <li class="title"><a href="view.aspx" target="_blank">数据分析 Report</a></li>
                </ul>
            </div>
        </div>
        <div class="main fl">
            <div class="main_title_bg">
                <div class="search">
                    <div id="startDate">
                        <input id="txt_startdate" runat="server" style="width: 100px;" type="text" value="" />&nbsp;</div>
                    <div id="endDate">
                        <input id="txt_enddate" runat="server" style="width: 100px;" type="text" value="" />&nbsp;</div>
                    <div style="">
                        <select class="search_Xiala" runat="server" id="file1" onchange="javascript:initSite(this.value)">
                            <option value="1">标题+内容</option>
                            <option value="2">媒体</option>
                            <option value="3">标签</option>
                        </select>
                        <select class="search_Xiala" style="visibility: hidden;width:10px;" runat="server" id="file2">
                            <option value=""></option>
                        </select>
                        <input type="text" runat="server" class="text_ipt" id="file3" />
                    </div>
                </div>
                <div>
                    <input name="btn_search" id="btn_search" type="button" runat="server" onserverclick="btn_search_click"
                        class="search_btn" />
                </div>
            </div>
            <!--search list over-->
            <div class="joinus">
          

            </div>
             <div style="margin: 0px auto; height: 614px; width: 100%;">
                <table id="tb_data" cellpadding="0" cellspacing="0" border="0" width="100%;" class="HT_tb_2">
                <tr id="tr_title" style="width: 100%; margin-bottom: 5px;">
                        <th class="style1">
                            序号ID
                        </th>
                        <th class="style2">
                            标题 Title
                        </th>
                        <th class="style3">
                            标签 Tag
                        </th>
                        <th class="style4">
                             媒体 Media
                        </th>
                        <th style="background:none;" class="style5">
                             时间 Date
                        </th>
                    </tr>
                    <asp:Repeater ID="rep_data" runat="server">
                        <ItemTemplate>
                            <tr class="content">
                                <%-- <td style=" visibility:hidden;"> <%#Eval("Id")%></td>--%>
                                <td>
                                    <%#Eval("DataName")%>
                                </td>
                                <td class="tx_lt">
                                    <img alt="" src="img/<%#Eval("Analysis")%>.png" />
                                    <a href='<%#Eval("SrcUrl") %>' target="_blank" onmouseover="javascript:show_title('null')">
                                        <%#IsSubString(DataBinder.Eval(Container.DataItem, "Title").ToString(), 12)%></a>
                                </td>
                                <td>
                                    <span>
                                    <%#IsSubString(DataBinder.Eval(Container.DataItem, "Tag").ToString(), 8)%>
                                </td>
                                <td>
                                    <%#Eval("SiteName")%>
                                </td>
                                <td>
                                    <%#Eval("Time", "{0:yyyy-MM-dd}")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div style="width: 70%; height: 34px;">
                <webdiyer:AspNetPager ID="AspNetPager1" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"
                    runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
                    PageSize="20" PrevPageText="上一页" ShowCustomInfoSection="left" ShowInputBox="Never"
                    OnPageChanged="AspNetPager1_PageChanged" CustomInfoTextAlign="left" LayoutType="Table"
                    NumericButtonCount="5">
                </webdiyer:AspNetPager>
            </div>
        </div>
    </div>
    <div style="visibility: hidden;">
        <span runat="server" id="sp_hid_startDate"></span><span runat="server" id="sp_hid_endDate">
        </span>
        <input runat="server" type="hidden" id="hid_datetype" value="news" />
        <input runat="server" type="hidden" id="hid_pageIndex" value="1" />
        <input runat="server" type="hidden" id="hid_project" value="hankook" />
        <input runat="server" type="hidden" id="hid_projectId" value="" />
     
    </div>
    </form>
</body>
</html>
