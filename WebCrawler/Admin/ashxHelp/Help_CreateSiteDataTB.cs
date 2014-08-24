using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class Help_CreateSiteDataTB
{
    public Help_CreateSiteDataTB()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public string HtmlHead
    {
        get
        {
            string html_head = @"<table id='tb_dataList' style='width: auto; margin:0px auto;' cellpadding='0' cellspacing='0'>
             <tr><th style='width: 41px;'><input type='checkbox' id='ck_all' value='' onclick='ck_all_click();' /></th>
            <th style='width: 265px;'>帖子主题</th><th style='width: auto;text-align:center;'>标签处理<br />#TagHead</th>
            <th style='width: 154px;'>操作</th><th style='width: 41px;text-align:center;'>镜</th><th style='width: 81px;text-align:center;'>媒体名</th><th style='width: 97px;text-align:center;'>抓取日期</th></tr>";
            return Regex.Replace(html_head, @"\r\n|\r|\n", ""); 
        }
    }

    public string HtmlContent
    {
        get
        {
            string html_content =
            @"<td><input type='checkbox' id='#SD_Id' value='#SD_Id' onclick='ck_one_click(this.id);' /></td>
            <td class='style_content'>#Attention<span style='color:red'>#Analysis</span><span style='color:red;'>#Hot</span>#Title
            <div style='float:right;margin-right:3px;'><span style='color: #875805;'>同</span><span style='color: #875805;' class='style_content' onmouseover='sp_content_mouseover(this);' onmouseout='sp_content_mouseout(this);'>详</span><input type='hidden' value='#Content_det' /><div></td>
            </td><td style='text-align:center;'>#TagList <div style='float:left;width:50px;'><span><a href='#' style='text-decoration:underline;' onclick='sp_tag_t(this);'>标签</a></span></div></td>
            <td style='padding-left: 15px;'>
            <a onclick='a_analysis_Click(1,this);' href='#'>正</a>&nbsp;<a onclick='a_analysis_Click(2,this);'href='#'>中</a> &nbsp;<a runat='server' onclick='a_analysis_Click(3,this);' href='#'>负</a>
             &nbsp;<a onclick='a_analysis_Click(4,this);' href='#'>争</a>&nbsp; <a onclick='a_hot_5_Click(1,this);' href='#'>热</a> <br />
            <a  href='#'  onclick='a_attention_click(1,this);'>关注</a>&nbsp;
            <a href='#' runat='server' onclick='a_examine_Click(2,this);'>审核</a>&nbsp;<a  href='#' runat='server' onclick='a_delete_Click(99,this);'>删除</a></td>
            <td style='text-align:center;'><a href='#SrcUrl' target='_blank'>镜</a></td><td style='text-align:center;'>#PlateName</td><td style='text-align:center;'>#SiteName</td><td style='text-align:center;'> #CreateDate</td>";
            return Regex.Replace(html_content, @"\r\n|\r|\n", "");
        }
        //<a class='a_id_tag' href='#'  onclick='a_batchTag_click(this);' >标签</a>&nbsp;
    }

    public string TagListContent = "";
   
                       
    public string HtmlDivPage
    {
        get {
            string htmldiv_page = @"<a id='a_firstPage' #firstPage_click style='margin-right:5px;#text_dec_first'>首页</a>
                 <a id='a_lastPage' #lastPage_click style='margin-right:5px;#text_dec_last'>上一页</a>
                 <a id='a_nextPage' #nextPage_click style='margin-right:5px;#text_dec_next'>下一页</a>
                 <a id='a_finalPage' #finalPage_click style='margin-right:5px;#text_dec_final'>尾页</a>&nbsp;&nbsp;&nbsp;
                 <a id='sp_pageMess' runat='server'>#sp_pageMess_text</a>";
             return Regex.Replace(htmldiv_page, @"\r\n|\r|\n", ""); 
           
        }
    }

    /// <summary>
    /// 得到项目的一级标签
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    public string GetTagHeadByProjectId(int projectId,ref string[]tagAttr)
    {
        StringBuilder sbulider = new StringBuilder();
        List<TagList> list = new TagBLL().Get1stTagByProjectIdManager(projectId);
        tagAttr = new string[list.Count];
        for (int i = 0; i < list.Count; i++)
		{
            int index = i + 1; //sp_tag_title_1 索引从1 开始
            tagAttr[i] = list[i].TagName;
            sbulider.Append("&nbsp;<span id='sp_tag_title_" + index + "'>[" + list[i].TagName + "]</span>");
		}
        string result = sbulider.ToString();
        return result.Length > 6 ? result.Substring(6) : result; //去掉第一个 &nbsp;
    }


    #region 翻页控件
    public string CreateDivPage(int projectId,int currentPage,int pageCount)
    {
       
        string html_pageDiv = HtmlDivPage;
        string text_dec_Underline = "text-decoration:underline; cursor:hand;";

        string firstPage_click = "href='' onclick='a_firstPage_click(this);return false;' ";
        string lastPage_click = "href='' onclick='a_lastPage_click(this,-1);return false;' ";
        string nextPage_click = "href='' onclick='a_nextPage_click(this,1);return false;' ";
        string finalPage_click = "href='' onclick='a_finalPage_click(this);return false;' ";
      

        string sp_pageMess = string.Format("当前第{0}页,共{1}页", currentPage, pageCount);
        if (currentPage == 1 && pageCount== 1)
        {
            //只有1页
            html_pageDiv = html_pageDiv.Replace("#text_dec_first", "").Replace("#firstPage_click", ""); 
            html_pageDiv = html_pageDiv.Replace("#text_dec_last", "").Replace("#lastPage_click", ""); 
            html_pageDiv = html_pageDiv.Replace("#text_dec_next", "").Replace("#nextPage_click", ""); 
            html_pageDiv = html_pageDiv.Replace("#text_dec_final", "").Replace("#finalPage_click", ""); 
        }
        else if (currentPage == 1 && pageCount>1)
        {
            //大于一页的 首页
            html_pageDiv = html_pageDiv.Replace("#text_dec_first", "").Replace("#firstPage_click", "");
            html_pageDiv = html_pageDiv.Replace("#text_dec_last", "").Replace("#lastPage_click", "");
            html_pageDiv = html_pageDiv.Replace("#text_dec_next", text_dec_Underline).Replace("#nextPage_click", nextPage_click);
            html_pageDiv = html_pageDiv.Replace("#text_dec_final", text_dec_Underline).Replace("#finalPage_click", finalPage_click);
           
        }
        else if (currentPage > 1 && pageCount > currentPage)
        {
            //大于一页的 中间页

            html_pageDiv = html_pageDiv.Replace("#text_dec_first", text_dec_Underline).Replace("#firstPage_click", firstPage_click);
            html_pageDiv = html_pageDiv.Replace("#text_dec_last", text_dec_Underline).Replace("#lastPage_click", lastPage_click);
            html_pageDiv = html_pageDiv.Replace("#text_dec_next", text_dec_Underline).Replace("#nextPage_click", nextPage_click);
            html_pageDiv = html_pageDiv.Replace("#text_dec_final", text_dec_Underline).Replace("#finalPage_click", finalPage_click);
        }
        else if (currentPage > 1 && currentPage ==pageCount)
        {
            //大于一页的最后一页

            html_pageDiv = html_pageDiv.Replace("#text_dec_first", text_dec_Underline).Replace("#firstPage_click", firstPage_click);
            html_pageDiv = html_pageDiv.Replace("#text_dec_last", text_dec_Underline).Replace("#lastPage_click", lastPage_click);

            html_pageDiv = html_pageDiv.Replace("#text_dec_next", "").Replace("#nextPage_click", "");
            html_pageDiv = html_pageDiv.Replace("#text_dec_final", "").Replace("#finalPage_click", ""); 

        }
        html_pageDiv = html_pageDiv.Replace("#sp_pageMess_text", sp_pageMess);
       
        return html_pageDiv;
    }

    #endregion


    #region 得到Tag的Content

  
    #endregion


    /// <summary>
    /// 更新某一行的信息
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public string CreateSiteDataTrHtml(SiteDataModel item)
    {
        string[] tagAttr = { };
        GetTagHeadByProjectId(item.ProjectId, ref tagAttr);


        string tb_contend = HtmlContent;
        tb_contend = tb_contend.Replace("#SD_Id", item.SD_Id.ToString());
        tb_contend = tb_contend.Replace("#Attention", FormatAttention(item.Attention));
        tb_contend = tb_contend.Replace("#Analysis", FormatAnalysis(item.Analysis));
        tb_contend = tb_contend.Replace("#Hot", FormatHot(item.Hot));
        tb_contend = tb_contend.Replace("#Title", IsSubStr(item.Title, 17, 3));

        //#xiang 

        string TagListContent = GetTagContent(item.Tag1, item.Tag2, item.Tag3, item.Tag4, item.Tag5, item.Tag6, tagAttr);
        tb_contend = tb_contend.Replace("#TagList", TagListContent);

        tb_contend = tb_contend.Replace("#Content_det", IsSubStr(item.Content, 350, 3));
        tb_contend = tb_contend.Replace("#Content", IsSubStr(item.Content, 17, 3));
        tb_contend = tb_contend.Replace("#CreateDate", item.CreateDate.ToString("yyyy-MM-dd"));
        tb_contend = tb_contend.Replace("#SrcUrl", item.SrcUrl);
        tb_contend = tb_contend.Replace("#SiteName", item.SiteName);
        tb_contend = tb_contend.Replace("#PlateName", item.PlateName);
      
        return tb_contend;
    }


    public string tagHead = "";
    
    public string CreateSiteDataTBHtml(List<SiteDataModel> list)
    {
        StringBuilder sBulider = new StringBuilder();

        string tb_head = HtmlHead;
       
        string[] tagAttr ={};
        if (list.Count > 0)
        {
            tagHead = GetTagHeadByProjectId(list[0].ProjectId, ref tagAttr);
        }
        tb_head = tb_head.Replace("#TagHead", tagHead);

        sBulider.Append(tb_head);
        
        foreach (SiteDataModel item in list)
        {
            sBulider.Append("<tr>");
            string tb_contend = HtmlContent;
            tb_contend = tb_contend.Replace("#SD_Id", item.SD_Id.ToString());
            tb_contend = tb_contend.Replace("#Attention", FormatAttention(item.Attention));
            tb_contend = tb_contend.Replace("#Analysis", FormatAnalysis(item.Analysis));
            tb_contend = tb_contend.Replace("#Hot", FormatHot(item.Hot));
            tb_contend = tb_contend.Replace("#Title", IsSubStr(item.Title, 17, 3));
           
            //#xiang 

            TagListContent = GetTagContent(item.Tag1, item.Tag2, item.Tag3, item.Tag4, item.Tag5, item.Tag6, tagAttr);
            tb_contend = tb_contend.Replace("#TagList", TagListContent);

            tb_contend = tb_contend.Replace("#Content_det", IsSubStr(item.Content, 350, 3));
            tb_contend =tb_contend.Replace("#Content", IsSubStr(item.Content, 15, 3));
            tb_contend =tb_contend.Replace("#CreateDate", item.CreateDate.ToString("yyyy-MM-dd"));
            tb_contend =tb_contend.Replace("#SrcUrl", item.SrcUrl);
            tb_contend = tb_contend.Replace("#SiteName", item.SiteName);
            tb_contend = tb_contend.Replace("#PlateName", item.PlateName);
            sBulider.Append(tb_contend);
            sBulider.Append("</tr>");
        }
        sBulider.Append("</table>");
        sBulider.Append("<script src='../Common/JS/commonjs.js' type='text/javascript'></script>");
        sBulider.Append("<script src='../Common/JS/sitedata_show.js' type='text/javascript'></script>");
        sBulider.Append("<script src='../Common/JS/netPage.js' type='text/javascript'></script>");
        string result = Regex.Replace(sBulider.ToString().Replace("\"", "\\\""), @"\r\n|\r|\n", "");
        return result;
    }

    public string GetTagContent(string tag1, string tag2, string tag3, string tag4, string tag5, string tag6, string[] tagArr)
    {
        StringBuilder sbulider = new StringBuilder();
        string mess = tag1 + tag2 + tag3 + tag4 + tag5 + tag6;
       
        int width = 80;
        if (mess.Trim() == "")
        {
            if (tagArr.Length > 0)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;' ><span>" + (tag1 == "" ? tagArr[0] : tag1) + "</span></div>");
            }
            if (tagArr.Length > 1)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;'><span>" + (tag2 == "" ? tagArr[1] : tag2) + "</span></div>");
            }
            if (tagArr.Length > 2)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;'><span>" + (tag3 == "" ? tagArr[2] : tag3) + "</span></div>");
            }
            if (tagArr.Length > 3)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;'><span>" + (tag4 == "" ? tagArr[3] : tag4) + "</span></div>");
            }
            if (tagArr.Length > 4)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;'><span>" + (tag5 == "" ? tagArr[4] : tag5) + "</span></div>");
            }
            if (tagArr.Length > 5)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;'><span>" + (tag6 == "" ? tagArr[5] : tag6) + "</span></div>");
            }
        }
        else
        {
            if (tagArr.Length > 0)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;color:green;'><span>" + (tag1 == "" ? "&nbsp;" : tag1) + "</span></div>");
            }
            if (tagArr.Length > 1)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;color:green;'><span>" + (tag2 == "" ? "&nbsp;" : tag2) + "</span></div>");
            }
            if (tagArr.Length > 2)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;color:green;'><span>" + (tag3 == "" ? "&nbsp;" : tag3) + "</span></div>");
            }
            if (tagArr.Length > 3)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;color:green;'><span>" + (tag4 == "" ? "&nbsp;" : tag4) + "</span></div>");
            }
            if (tagArr.Length > 4)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;color:green;'><span>" + (tag5 == "" ? "&nbsp;" : tag5) + "</span></div>");
            }
            if (tagArr.Length > 5)
            {
                sbulider.Append("<div style='float:left;width:" + width + "px;color:green;'><span>" + (tag6 == "" ? "&nbsp;" : tag6) + "</span></div>");
            }
        }
      
        return sbulider.ToString();

    }

    public string FormatAnalysis(int analysis)
    {
        string result = "";
        switch (analysis)
        {
            case 0:
                result = "";
                break;
            case 1:
                result = "(正)";
                break;
            case 2:
                result = "(中)";
                break;
            case 3:
                result = "(负)";
                break;
            case 4:
                result = "(争)";
                break;
            default:
                result = "(同)";
                break;
        }

        return result;
    }

    public string FormatHot(int hot)
    {
        string result = "";
        if (hot == 1)
        {
            result = "(热)";
        }
        return result;
    }

    public string FormatAttention(int attention)
    {
         string result = "";
        if (attention == 1)
        {
            result = "<span style='color:red'>★</span>";
        }
        return result;
    }

    public string IsSubStr(string strShow, int length, int trip)
    {
        string show = strShow.Length > length ? strShow.Substring(0, length - trip) + "..." : strShow;

        return show.Replace("\""," ").Replace("'"," ").Replace("<"," ").Replace(">"," ");
    }

    #region #red
    public string Tag_ul = "<li id='tag_#id'><span>#TagName</span><span class='a_update_t'><a href='#' onclick='a_update_t_click(this);'>修改</a></span><span class='a_add_t'><a href='#' onclick='a_add__t_click(this);'>添加</a></span><ul>#Tag_li</ul></li>";
    public string Tag_li = "<li id='tag_#id'><span>#TagName</span><span class='a_update'><a href='#'>修改</a></span><span class='a_delete'><a href='#'>删除</a></span></li>";
    #endregion

    #region #blue
    public string projectTag_ul = " <li class='li_tag_t'><span><input type='checkbox' id='ck_tag_#id' /></span><span>#TagName</span> <ul class='ul_tag_t'>#Tag_li</ul></li>";
    public string projectTag_li = "<li><span><input type='checkbox' id='ck_tag_#id'><span>#TagName</span></li>";
    #endregion

    public string CreateTagList()
    {
        StringBuilder sbulider = new StringBuilder();
        //显示 标签
       TagBLL bllAction =  new TagBLL();
        List<TagList> list = bllAction.GetTagListManager();

        foreach (TagList tag in list)
        {
            string tag_ul = Tag_ul.Replace("#TagName", tag.TagName).Replace("tag_#id", "tag_" + tag.Id);

            List<TagList> innerList = bllAction.GetTagListByTidManager(tag.Id);

            StringBuilder sb_li = new StringBuilder();
            foreach (TagList inner_tag in innerList)
            {
                sb_li.Append(Tag_li.Replace("#TagName", inner_tag.TagName).Replace("tag_#id", "tag_" + inner_tag.Id));
            }
            sbulider.Append(tag_ul.Replace("#Tag_li", sb_li.ToString()));
        }  
        //查询出 所有的 一级标签
        //查询 一级标签 下面的 2级标签,
        //绑定 一级标签的 二级标签
        return sbulider.ToString();
    }

    public string CreateProjectTagList()
    {
        //查出 所有的标签
        StringBuilder sbulider = new StringBuilder();
        //显示 标签
        TagBLL bllAction = new TagBLL();
        List<TagList> list = bllAction.GetTagListManager();

        foreach (TagList tag in list)
        {
            string tag_ul = projectTag_ul.Replace("#TagName", tag.TagName).Replace("ck_tag_#id", "ck_tag_" + tag.Id);

            List<TagList> innerList = bllAction.GetTagListByTidManager(tag.Id);

            StringBuilder sb_li = new StringBuilder();
            foreach (TagList inner_tag in innerList)
            {
                sb_li.Append(projectTag_li.Replace("#TagName", inner_tag.TagName).Replace("ck_tag_#id", "ck_tag_" + inner_tag.Id));
            }
            sbulider.Append(tag_ul.Replace("#Tag_li", sb_li.ToString()));
        }
        //查询出 所有的 一级标签
        //查询 一级标签 下面的 2级标签,
        //绑定 一级标签的 二级标签
        return sbulider.ToString();

    }

    public string CreateSelectHtml(List<TagList> tagList)
    {
        StringBuilder sbulider =  new StringBuilder();
        sbulider.Append("<select><option value=''>--选择--</option>");

        foreach (TagList item in tagList)
	    {
            sbulider.Append("<option value='op_" + item.Id + "'>" + item.TagName + "</option>");
	    }
        sbulider.Append("</select>");
        return sbulider.ToString();
    }


}