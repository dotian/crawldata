using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.Model;

namespace ConGetData.DAL
{
    public class MockServiceDAL : IServiceDAL
    {
        public List<CrawlTarget> GetTargetService()
        {
            var targetList = new List<CrawlTarget>();

            targetList.Add(template1253());

            return targetList;
        }

        private CrawlTarget template766()
        {
            CrawlTarget crawlTarget = new CrawlTarget();

            crawlTarget.SiteUrl = "http://uni.sina.com.cn/c.php?k=<>&t=blog&ts=bpost&stype=all";

            crawlTarget.SiteEncoding = "GB2312";
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "0";
            crawlTarget.SiteId = "0";
            crawlTarget.ClassName = "blog";
            crawlTarget.Tid = "766";
            crawlTarget.KeyWords = "博士伦";

            return crawlTarget;
        }

        private CrawlTarget template2001()
        {
            CrawlTarget crawlTarget = new CrawlTarget();

            crawlTarget.SiteUrl = "http://uni.sina.com.cn/c.php?k=<>&t=blog&ts=bpost&stype=all";

            crawlTarget.SiteEncoding = "GB2312";
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "0";
            crawlTarget.SiteId = "0";
            crawlTarget.ClassName = "forum";
            crawlTarget.Tid = "2001";
            crawlTarget.KeyWords = "博士伦";

            return crawlTarget;            
        }

        private CrawlTarget testsearch()
        {
            CrawlTarget crawlTarget = new CrawlTarget();

            crawlTarget.SiteUrl = "http://sou.autohome.com.cn/luntan?q=<>&page={p}";

            crawlTarget.SiteEncoding = "GB2312";
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "0";
            crawlTarget.SiteId = "0";
            crawlTarget.ClassName = "forum";
            crawlTarget.Tid = "1253";
            crawlTarget.KeyWords = "米其林";

            return crawlTarget;
        }

        private CrawlTarget template1251()
        {
            CrawlTarget crawlTarget = new CrawlTarget();

            // 野马论坛
            crawlTarget.SiteUrl = "http://club.autohome.com.cn/bbs/forum-c-102-1.html";

            crawlTarget.SiteEncoding = "GB2312";
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "0";
            crawlTarget.SiteId = "0";
            crawlTarget.ClassName = "forum";
            crawlTarget.Tid = "1251";

            return crawlTarget;
        }

        private CrawlTarget template375()
        {
            CrawlTarget crawlTarget = new CrawlTarget();

            crawlTarget.SiteUrl = "http://sou.autohome.com.cn/luntan?q=<>";

            crawlTarget.SiteEncoding = "GB2312";
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "0";
            crawlTarget.SiteId = "0";
            crawlTarget.ClassName = "forum";
            crawlTarget.Tid = "375";
            crawlTarget.KeyWords = "米其林";

            return crawlTarget;
        }

        private CrawlTarget template373()
        {
            CrawlTarget crawlTarget = new CrawlTarget();

            // 海南论坛
            crawlTarget.SiteUrl = "http://club.autohome.com.cn/bbs/forum-a-100009-1.html";

            crawlTarget.SiteEncoding = "GB2312";
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "0";
            crawlTarget.SiteId = "0";
            crawlTarget.ClassName = "forum";
            crawlTarget.Tid = "373";

            return crawlTarget;
        }

        private CrawlTarget template22()
        {
            CrawlTarget crawlTarget = new CrawlTarget();

            crawlTarget.SiteUrl = "http://sou.autohome.com.cn/luntan?q=<>&pvareaid=100834&entry=44&clubClassBefore=0&IsSelect=0&clubOrder=Relevance&clubClass=0&clubSearchType=&clubSearchTime=";

            crawlTarget.SiteEncoding = "GBK";
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "0";
            crawlTarget.SiteId = "56429";
            crawlTarget.ClassName = "forum";
            crawlTarget.Tid = "22";
            crawlTarget.KeyWords = "梅赛德斯";

            return crawlTarget;
        }

        private CrawlTarget template1253()
        {
            CrawlTarget crawlTarget = new CrawlTarget();

            // 阿尔法罗密欧论坛
            crawlTarget.SiteUrl = "http://club.autohome.com.cn/bbs/forum-c-2288-{p}.html";

            // 摩托车论坛
            // crawlTarget.SiteUrl = "http://club.autohome.com.cn/bbs/forum-o-200063-1.html";

            // 自驾游论坛
            // crawlTarget.SiteUrl = "http://club.autohome.com.cn/bbs/forum-o-200042-1.html";

            crawlTarget.SiteEncoding = "GB2312";
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "0";
            crawlTarget.SiteId = "0";
            crawlTarget.ClassName = "forum";
            crawlTarget.Tid = "1253";

            return crawlTarget;
        }

        private CrawlTarget sampleForum1()
        {
            CrawlTarget crawlTarget = new CrawlTarget();

            crawlTarget.SiteUrl = "http://digibbs.tech.163.com/list/jiadian.html";
            crawlTarget.SiteEncoding = "GBK";
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "173";
            crawlTarget.SiteId = "99400";
            crawlTarget.ClassName = "forum";
            crawlTarget.KeyWords = "上海联通+苹果官网+iPhone5+小米+联通+中国连通+联通3G套餐";
            crawlTarget.Tid = "471";

            return crawlTarget;
        }

        private CrawlTarget sampleMicroblog()
        {
            CrawlTarget crawlTarget = new CrawlTarget();
            crawlTarget.SiteUrl = "http://s.weibo.com/weibo/<>";
            crawlTarget.SiteEncoding = "UTF-8";
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "175";
            crawlTarget.SiteId = "99463";
            crawlTarget.ClassName = "microblog";
            crawlTarget.KeyWords = "方太";
            crawlTarget.Tid = "1097";

            return crawlTarget;
        }

        public List<CK_SiteList> GetCK_listService()
        {
            throw new NotImplementedException();
        }

        public int UpdateCKStatusService(int siteId, int status)
        {
            throw new NotImplementedException();
        }
    }
}
