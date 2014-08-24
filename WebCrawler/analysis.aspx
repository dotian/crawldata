<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="analysis.aspx.cs" Inherits="WebCrawler.analysis" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>舆情监控系统</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <link href="css/share.css" rel="stylesheet" type="text/css" />
    <link href="css/cssmain.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
       
        
        
        
    </style>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script type="text/javascript">



        var cid;
        var arr = new Array();

        $(function () {
            $(".content .sp").click(function () {
                $("#translate_1").css("display", "block");
            });
            $(".dingwei").click(function () {
                $("#translate_1").css("display", "none");
            });

            $("#show").css("display", "none");
            $(".title").click(function () {
                $(".title").removeClass("hover");
                $(this).addClass("hover");
            })
            $("#close").click(function () { $("#show").css("display", "none"); })
            $(".tick").click(function () {
            })
        })

        function show() {
            $("#show").css("display", "block");
        }


        var type = 'sitedata';
        var a = 'null';
        var nowPage = 1;
        var att = 0;

        function openDiv(i) {
            cid = i;
            document.getElementById("note").innerHTML = "";
        }

        function openNote(i, k) {
            cid = i;
            $("#note").val($(k).text().trim());

        }

        function openTitle(i, k) {
            cid = i;
            //$("#tra").val($(k).text());
            document.getElementById("tra").value = unescape(document.getElementById(k).innerHTML);
        }


        function goCustomer() {
            $.cookie('li_gb_id', "li_gb_2");
            window.location.href = "customer.aspx?type=sitedata&c=1&a=1";
        }
        function gobackPList() {
            window.location.href = "projectview.aspx";
        }
        function goIndex() {
            window.location.href = "index.aspx";
        }

        function down() {
            //            var url = "/datadown.do?id=70&start=&end=&file=&value=&a=" + a + "&att=" + att + "&type=" + type + "&att=null";
            //            if (window.XMLHttpRequest) {// 非IE浏览器，用xmlhttprequest对象创建
            //                req = new XMLHttpRequest();
            //            } else if (window.ActiveXObject) {// IE浏览器用activexobject对象创建
            //                req = new ActiveXObject("Microsoft.XMLHttp");
            //            }
            //            if (req) {// 成功创建xmlhttprequest
            //                req.open("POST", url, false);
            //                //req.onReadyStateChange = callback;// 指定回调函数
            //                req.send(null); // 发送请求
            //                ap = req.responseText;
            //            }
            //            window.location.href = gContextpath + "/data.csv";
        }

        function note1() {
            var txt = $("#note").val();
            //将数据更新到数据库
            //重新绑定 页面的数据
            //隐藏 show,清空 历史数据
            var ctrl_tr_id = "tr_data_" + cid;
            var ctrl = document.getElementById(ctrl_tr_id);
            // 查看当前的 url

            if (txt.trim() == "") {
                $("#show").css("display", "none");
                return;
            }
            $.ajax({
                type: "POST",
                url: "hankookAshx/maintainSuggest.ashx",
                data: { id: cid, datatype: "sitedata", mess: txt },
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "text",
                async: false,
                success: function (data) {
                    $(ctrl).find("td").eq(3).text(txt);
                    $("#show").css("display", "none");
                    $("#note").val("");
                },
                error: function (msg) {
                    alert(msg);
                }

            });


        }

        function Title() {
            var n = document.getElementById("tra").value;
            var url = "/addtitle.do?c=" + cid + "&type=" + type + "&n=" + escape(escape(n));
            if (window.XMLHttpRequest) {// 非IE浏览器，用xmlhttprequest对象创建
                req = new XMLHttpRequest();
            } else if (window.ActiveXObject) {// IE浏览器用activexobject对象创建
                req = new ActiveXObject("Microsoft.XMLHttp");
            }
            if (req) {// 成功创建xmlhttprequest
                req.open("POST", url, false);
                //req.onReadyStateChange = callback;// 指定回调函数
                req.send(null); // 发送请求
                ap = req.responseText;
            }
            window.location.reload();
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#tb_data").find("td[name='td_suggest']").each(function () {
                var sug = $(this).find("span").text().trim();

                var id = $(this).attr("id").replace("td_suggest_", "");
                var html = "<img alt='' src='img/weihuzhong.jpg' style='position: relative; top: 0px; cursor: pointer;' ";
                html += " class='tick' onclick='javascipt:openDiv(" + id + ");show()' />";
                if (sug == "") {
                    $(this).html(html);
                }
            });

        });
        function btn_checkSitedata() {
            var url = window.location.href;
            if (url.indexOf("sitedata") > 0) {

            } else {
                alert("新闻没有该功能");
                return false;
            }

        }


        $(document).ready(function () {
            //aspnetpager 的样式
            $("#AspNetPager1").find("tr td").first().attr("valign", "middle");

            var li_gb_id = $.cookie('li_gb_id_ana');
            if (li_gb_id != null) {
                $(".greenImgBg").removeClass("greenImgBg").addClass("orangeImgBg");
                $("#" + li_gb_id).removeClass("orangeImgBg").addClass("greenImgBg");
            } else {
                $(".greenImgBg").removeClass("greenImgBg").addClass("orangeImgBg");
                $("#li_gb_2").removeClass("orangeImgBg").addClass("greenImgBg");
            }

            $(".nav_1 li").click(function () {
                $(".greenImgBg").removeClass("greenImgBg").addClass("orangeImgBg");
                var li_gb_id = $(this).parent().find("li").first().attr("id");
                $.cookie('li_gb_id_ana', li_gb_id);
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
            // 重新绑定
            $(".orangeImgBg").click(function () {
                //li_gb_2
                var li_gb_id = $(this).attr("id");
                $.cookie('li_gb_id_ana', li_gb_id);
                //记下这个ID
                $(".greenImgBg").removeClass("greenImgBg").addClass("greenImgBg");
                $(this).removeClass("orangeImgBg").addClass("greenImgBg");
            });
        });


       
    </script>
</head>
<body>
    <form runat="server" id="form1">
    <div class="pointQut_wod" style="background: url('img/pointOut.png'); width: 250px;
        height: 80px;">
        <span class="pointQut" id="pointQut"></span>
    </div>
    <div id="show" style="width: 99%; height: 99%; display: none; position: absolute;
        z-index: 99; top: 0px; left: 0px; background: url('img/bg.png'); text-align: center;">
        <div id="alt" style="width: 469px; height: 195px; margin: 100px auto 0; position: relative;
            overflow: hidden; background: #FFF;">
            <div style="width: 469px; height: 28px; background: url('img/tiao.jpg') repeat-x;">
                <a href="#" style="display: block; width: 20px; height: 20px; background: url(img/close.jpg) no-repeat;
                    float: right; margin: 4px;" id="close"></a>
            </div>
            <textarea name="" id="note" cols="" rows="" style="border: 1px solid #c9c9c9; position: absolute;
                top: 32px; left: 3px; width: 460px; height: 100px;"></textarea>
            <img alt="" src="img/tijiao.jpg" style="position: relative; top: 140px;" onclick="javascipt:note1()" />
        </div>
    </div>
    <div class="wrap">
        <div class="head fl ht2">
            <a href="customer.aspx?type=news">
                <div id="div_logo" runat="server" class="logo_change fl">
                    <img alt="" runat="server" id="img_logo" src="" />
                </div>
            </a>
            <img alt="" src="img/puit.gif" onclick="javascipt:goIndex()" />
            <img alt="" src="img/back.jpg" onclick="javascipt:goCustomer()" />
            <img alt="" id="img_retplist" runat="server" src="img/retplist.jpg" onclick="javascipt:gobackPList()" />
            <div class="split">
            </div>
        </div>
        <!--top over-->
        <div class="sideNav fl">
            <div class="nav">
                <ul class="nav_1">
                    <li class="orangeImgBg" id="li1"><a href="analysis.aspx?type=sitedata&c=2&a=0">新闻 News</a></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=2&a=1">正面
                        Positive</a></label></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=2&a=2">中性
                        Neutral</a></label></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=2&a=3">负面
                        Negative</a></label></li>
                </ul>
                <ul class="nav_1">
                    <li class="greenImgBg" id="li2"><a href="analysis.aspx?type=sitedata&c=1&a=0">论坛 BBS</a></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=1&a=1">正面
                        Positive</a></label></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=1&a=2">中性
                        Neutral</a></label></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=1&a=3">负面
                        Negative</a></label></li>
                </ul>
                <ul class="nav_1">
                    <li class="orangeImgBg" id="li3"><a href="analysis.aspx?type=sitedata&c=3&a=0">博客 BLOG</a></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=3&a=1">正面
                        Positive</a></label></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=3&a=2">中性
                        Neutral</a></label></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=3&a=3">负面
                        Negative</a></label></li>
                </ul>
                <ul class="nav_1">
                    <li class="orangeImgBg" id="li4"><a href="analysis.aspx?type=sitedata&c=5&a=0">微博 Microblog</a></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=5&a=1">正面
                        Positive</a></label></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=5&a=2">中性
                        Neutral</a></label></li>
                    <li><span>img</span><label class="li_txtleft"><a href="analysis.aspx?type=sitedata&c=5&a=3">负面
                        Negative</a></label></li>
                </ul>
                <ul class="nav_5">
                    <li class="title"><a href="view.aspx">数据分析 Report</a></li>
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
                    <div>
                        <select class="search_Xiala" runat="server" id="file1" onchange="javascript:initSite(this.value)">
                            <option value="1">标题+内容</option>
                            <option value="2">媒体</option>
                            <option value="3">标签</option>
                        </select>
                        <select class="search_Xiala" style="width: 10px; visibility: hidden;" runat="server"
                            id="file2">
                            <option value=""></option>
                        </select>
                        <input type="text" runat="server" class="text_ipt" id="file3" style="" />
                    </div>
                </div>
                <div>
                    <input name="btn_search" id="btn_search" type="button" runat="server" onserverclick="btn_search_click"
                        class="search_btn" />
                </div>
            </div>
            <div>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="img/HT_2_btn1.png" OnClientClick="return btn_checkSitedata();"
                    OnClick="btn_attention_click" />
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="img/HT_2_btn2.png" OnClientClick="return btn_checkSitedata();"
                    OnClick="btn_dofinish_click" />
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="img/HT_2_btn3.png" OnClientClick="return btn_checkSitedata();"
                    OnClick="btn_finish_click" />
                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="img/HT_2_btn4.png" OnClick="btn_download_click" />
            </div>
            <div style="margin: 0px auto; height: 614px; width: 100%;">
                <table id="tb_data" cellpadding="0" cellspacing="0" border="0" width="100%;" class="HT_tb_2">
                    <tr id="tr_title" style="width: 100%; margin-bottom: 5px;">
                        <th class="style1">
                            序号
                        </th>
                        <th class="style2">
                            标题
                        </th>
                        <th class="style3">
                            媒体
                        </th>
                        <th class="style4">
                            维护建议
                        </th>
                        <th style="background:none;" class="style5">
                            时间
                        </th>
                    </tr>
                    <asp:Repeater ID="rep_data" runat="server">
                        <ItemTemplate>
                            <tr id='tr_data_<%#Eval("Id")%>' class="content">
                                <td>
                                    <%#Eval("Id")%>
                                </td>
                                <td class="tx_lt">
                                    <img alt="" src="img/<%#Eval("Analysis")%>.png" />
                                    <a href='<%#Eval("SrcUrl") %>' target="_blank">
                                        <%#IsSubString(DataBinder.Eval(Container.DataItem, "Title").ToString(), 12)%></a>&nbsp;
                                    <span class="sp" onclick="javascipt:openTitle('2474801','t0')"><a href="#" onclick="return false;">
                                        翻译</a></span>
                                </td>
                                <td>
                                    <%#Eval("SiteName")%>
                                </td>
                                <td name="td_suggest" id='td_suggest_<%#Eval("Id")%>'>
                                    <span class="tick" id='sp_suggest_<%#Eval("Id")%>' onclick="javascipt:openNote('<%#Eval("Id")%>',this);show();"
                                        style="cursor: pointer">
                                        <%#Eval("Suggest")%>
                                    </span>
                                </td>
                                <td>
                                    <%#Eval("Time")%>
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
    <div id="translate_1" style="display: none; margin: 100px auto; width: 469px; height: 190px;
        z-index: 9999; position: absolute; top: 0px; left: 35%; background: #fff; border: 1px solid #ccc;">
        <div style="width: 469px; height: 28px; background: url('img/tiao_1.jpg')">
            <strong style="color: #000; display: block; width: 100%; text-align: center; line-height: 30px;">
                提交翻译内容</strong>
            <img src="img/close.jpg" class="dingwei" style="cursor: pointer; position: absolute;
                right: 2px; top: 2px;" />
        </div>
        <textarea name="" id="tra" cols="" rows="" style="border-bottom: #c9c9c9 1px solid;
            position: absolute; border-left: #c9c9c9 1px solid; width: 460px; height: 100px;
            border-top: #c9c9c9 1px solid; top: 32px; border-right: #c9c9c9 1px solid; left: 3px;"></textarea>
        <div style="text-align: center; margin-top: 134px;">
            <input name="" type="image" src="img/tijiao.jpg" style="" onclick="javascipt:Title()" />
        </div>
    </div>
    <input type="hidden" runat="server" id="hid_attention" value="0" />
    <input type="hidden" runat="server" id="hid_showstatus" value="0" />
    <input runat="server" type="hidden" id="hid_projectId" value="" />
    </form>
</body>
</html>
