using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WriteSpiderAgain.EntityModel;
using System.IO;


namespace WriteSpiderAgain
{

    public class TargetEntit
    {
        private static string path = @"F:\2013Year\PersonalDevelop\WinApplication\RegxFormTemplate\RegxFormTemplate\Template\";
      


        public static CrawlTarget f= new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText(path + "f2.xml"),
            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "55653",
            SiteUrl = "http://jinhua.19lou.com/forum-1425-1.html",
            RunStatus = 0
        };

    }


    public class TargetEntit1
    {
        private static string path = @"F:\2013Year\PersonalDevelop\WinApplication\RegxFormTemplate\RegxFormTemplate\Template\";
        #region forum


        public static CrawlTarget f2 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText(path+"f2.xml"),
            SiteEncoding = "utf-8",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "55653",
            SiteUrl = "http://www.17se.com/showforum-100.aspx",
            RunStatus = 0
        };


        public static CrawlTarget crawlTarget_1094 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1094.xml"),
            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99416",
            SiteUrl = "http://bbs.thethirdmedia.com/group/l84p1.html",
            RunStatus = 0
        };


        public static CrawlTarget crawlTarget_1088 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1088.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99421",
            SiteUrl = "http://www.eabbs.com/forum.php?mod=forumdisplay&fid=38",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_1019 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1019.xml"),
            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99454",
            SiteUrl = "http://club.kdnet.net/list.asp?boardid=78",
            RunStatus = 0
        };


        public static CrawlTarget crawlTarget_866 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_866.xml"),
            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99378",
            SiteUrl = "http://bbs.jjwxc.com/board.php?board=52&page=1",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_860 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_860.xml"),
            SiteEncoding = "UTF-8",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99365",
            SiteUrl = "http://bbs.chinago.cn/forumdisplay.php?fid=11&filter=0&orderby=dateline&ascdesc=DESC",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_854 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_854.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99389",
            SiteUrl = "http://www.actoys.net/bbs/thread.php?fid=99",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_555 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_555.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99376",
            SiteUrl = "http://club.alimama.com/thread-htm-fid-68.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_419 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_419.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99444",
            SiteUrl = "http://tt.mop.com/topic/list_159_162_0_0.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_295 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_295.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99398",
            SiteUrl = "http://bbs.gzhu.edu.cn/forumdisplay.php?fid=62&sid=Nrn1PP",
            RunStatus = 0
        };
        public static CrawlTarget crawlTarget_282 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_282.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99397",
            SiteUrl = "http://bbs.xfwed.com/forum-40-1.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_259 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_259.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99375",
            SiteUrl = "http://bbs.xuefa.com/forum-94-1.html",
            RunStatus = 0
        };
        public static CrawlTarget crawlTarget_238 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_238.xml"),
            SiteEncoding = "utf-8",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99442",
            SiteUrl = "http://www.tianya.cn/techforum/articleslist/0/930.shtml",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_61 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_61.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99383",
            SiteUrl = "http://bbs.newwise.com/forum-14-1.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_42 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_42.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99373",
            SiteUrl = "http://bbs.imp3.net/forum-9-1.html7",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_1095 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1095.xml"),
            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99419",
            SiteUrl = "http://bbs.abi.com.cn/index.asp?boardid=7",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_1093 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1093.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99417",
            SiteUrl = "http://bbs.hc360.com/forum-362-1.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_1092 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1092.xml"),
            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99426",
            SiteUrl = "http://www.jd-bbs.com/forum-43-1.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_1091 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1091.xml"),
            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99431",
            SiteUrl = "http://www.51mjd.com/bbs/index.asp?id=77&s=default",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_1090 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1090.xml"),

            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99415",
            SiteUrl = "http://itbbs.pconline.com.cn/tv/f767804.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_1089 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1089.xml"),

            SiteEncoding = "UTF-8",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99462",
            SiteUrl = "http://forum.china.com.cn/forum-174-1.html",
            RunStatus = 0
        };
        public static CrawlTarget crawlTarget_1087 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1087.xml"),

            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99405",
            SiteUrl = "http://jdbbs.zol.com.cn/subcate_list_74.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_1086 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1086.xml"),

            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99401",
            SiteUrl = "http://forum.ea3w.com/forum-2-1.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_1015 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1015.xml"),

            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99437",
            SiteUrl = "http://bbs.qianlong.com/forum-399-1.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_968 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_968.xml"),

            SiteEncoding = "utf-8",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99450",
            SiteUrl = "http://beta.club.sohu.com/food/threads#p1",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_583 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_583.xml"),

            SiteEncoding = "utf-8",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99364",
            SiteUrl = "http://bbs.heiguang.com/index.asp?boardid=60",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_286 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_286.xml"),

            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99390",
            SiteUrl = "http://www.bbszjj.com/thread.php?fid=22",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_361 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_361.xml"),

            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99384",
            SiteUrl = "http://bbs.arsenal.com.cn/forum-2-1.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_281 = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_281.xml"),

            SiteEncoding = "GBK",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99385",
            SiteUrl = "http://bbs.changde.gov.cn/thread.php?fid-6.html",
            RunStatus = 0
        };



        public static CrawlTarget crawlTarget_254 = new CrawlTarget()
        {
            /* 7 - 16 测试通过 */
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_254.xml"),

            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99394",
            SiteUrl = "http://bbs.512ms.com/forum-6-1.html",
            RunStatus = 0
        };


        public static CrawlTarget crawlTarget_471 = new CrawlTarget()
        {
            /* 7 - 16 测试通过 */
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_471.xml"),

            SiteEncoding = "gb2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99400",
            SiteUrl = "http://digibbs.tech.163.com/list/jiadian.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_179 = new CrawlTarget()
        {
            /* 7 - 16 测试通过 */
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_179.xml"),

            SiteEncoding = "UTF-8",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99438",
            SiteUrl = "http://bbs.ifeng.com/forumdisplay.php?fid=460",
            RunStatus = 0
        };


        public static CrawlTarget crawlTarget_334 = new CrawlTarget()
        {
            /* 7 - 16 测试通过 */
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_334.xml"),

            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99452",
            SiteUrl = "http://club.tech.sina.com.cn/forum-335-1.html?t=1",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_41 = new CrawlTarget()
        {
            /* 7 - 16 测试通过 */
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_41.xml"),

            SiteEncoding = "gbk",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99366",
            SiteUrl = "http://bbs.htpc1.com/forum-104-1.html",
            RunStatus = 0
        };

        public static CrawlTarget crawlTarget_208 = new CrawlTarget()
        {
            /* 7 - 16 测试通过 */
            ClassName = "forum",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_208.xml"),

            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "99377",
            SiteUrl = "http://www.watchstore.com.cn/index.asp?boardid=1",
            RunStatus = 0
        };
        #endregion

        #region  news

        //news_773.xml
        public static CrawlTarget crawlTarget_773 = new CrawlTarget()
        {
            ClassName = "news",
            TemplateContent = File.ReadAllText("../../TsetFile/news_773.xml"),
            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "88040",
            SiteUrl = "http://news.sogou.com/news?query=<>&mode=1",
            RunStatus = 0
        };


        public static CrawlTarget crawlTarget_775 = new CrawlTarget()
        {
            ClassName = "news",
            TemplateContent = File.ReadAllText("../../TsetFile/news_775.xml"),
            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "88042",
            SiteUrl = "http://news.baidu.com/ns?word=<>&tn=news&from=news&cl=2&rn=20&ct=1",
            RunStatus = 0
        };

        #endregion

        #region  blog

        public static CrawlTarget crawlTarget_765 = new CrawlTarget()
        {
            ClassName = "blog",
            TemplateContent = File.ReadAllText("../../TsetFile/forum_1094.xml"),
            SiteEncoding = "GB2312",
            KeyWords = "韩泰",
            NextPageCount = 1,
            ProjectId = "180",
            SiteId = "88038",
            SiteUrl = "http://blogsearch.sogou.com/blog?query=<>",
            RunStatus = 0
        };
        #endregion

        
    }
}
