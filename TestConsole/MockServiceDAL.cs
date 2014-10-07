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

            // targetList.Add(getTestCrawlTarget("http://ks.pcauto.com.cn/auto_bbs.shtml?q=<>","gbk",3000,"韩泰"));

            //targetList.Add(getTestCrawlTarget(
            //    "POST:http://www.jiemeng.com.cn/search.php?searchsubmit=yes",
            //    "utf-8", 3001, "韩泰", "mod=forum&formhash=13b6c12b&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&srchtxt=<>&searchsubmit=true"));

            //targetList.Add(getTestCrawlTarget(
            //    "http://search.discuz.qq.com/f/discuz?sId=9340908&ts=1411828585&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=f972917a14646ea599b2bda9ea49ec6a&charset=gbk&source=forum&q=<>&module=forum",
            //    "gbk", 3002, "韩泰"));

            //targetList.Add(getTestCrawlTarget(
            //   "http://search.xgo.com.cn/bbs.php?keyword=<>",
            //   "gbk", 3003, "韩泰"));

            //targetList.Add(getTestCrawlTarget(
            //   "http://www.88cheyou.com/search.php?mod=forum&searchid=4&orderby=lastpost&ascdesc=desc&searchsubmit=yes&kw=<>",
            //   "gbk", 3004, "韩泰"));

            //targetList.Add(getTestCrawlTarget(
            //       "POST:http://www.changan.biz/search.php?searchsubmit=yes",
            //       "gbk", 3005, "韩泰",
            //       "mod=forum&formhash=afff770a&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&srchtxt=<>&searchsubmit=true"));

            //targetList.Add(getTestCrawlTarget(
            //       "http://bbs.suv.cn/search.php?mod=my&q=<>",
            //       "gbk", 3006, "韩泰"));

            //targetList.Add(getTestCrawlTarget(
            //       "POST:http://www.cnjac.com/search.php?searchsubmit=yes",
            //       "gbk", 3007, "韩泰",
            //       "formhash=22fb863a&srchtxt=<>&searchsubmit=yes"));

            //targetList.Add(getTestCrawlTarget(
            //       "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=fdd658c0&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&sId=22962727&ts=1411883902&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=42b26c00732d68fd7cd6156d75dd5a25&charset=utf-8&source=discuz&fId=0&q=%E9%9F%A9%E6%B3%B0&srchtxt=%E9%9F%A9%E6%B3%B0&searchsubmit=true",
            //       "gbk", 3008, "韩泰"));

            //targetList.Add(getTestCrawlTarget(
            //       "http://hui.cn357.com/search.php?mod=my&q=<>",
            //       "gbk", 3009, "韩泰"));

            //targetList.Add(getTestCrawlTarget(
            //           "POST:http://bbs.hyqcw.com/search.php?searchsubmit=yes",
            //           "gbk", 3010, "韩泰",
            //           "mod=forum&formhash=f876d8d0&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&srchtxt=<>&searchsubmit=true"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://so.360che.com/cse/search?q=<>&s=3494890232603857038&nsid=3",
            //           "gbk", 3011, "韩泰"));

            //targetList.Add(getTestCrawlTarget(
            //           "POST:http://www.ahauto.com/bbs/search.php?searchsubmit=yes",
            //           "gbk", 3012, "大众",
            //           "mod=forum&formhash=582ba756&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&srchtxt=<>&searchsubmit=true"));

            //targetList.Add(getTestCrawlTarget(
            //           "POST:http://www.hubai.com/club/search.php?searchsubmit=yes",
            //           "gbk", 3013, "大众","mod=forum&formhash=f0fe912d&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&srchtxt=<>&searchsubmit=true"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=4f63c18c&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&sId=2856804&ts=1411887954&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=d7fb3d58641886f456d33434335f9bb6&charset=utf-8&source=discuz&fId=0&q=<>&srchtxt=<>&searchsubmit=true",
            //           "utf-8", 3014, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=search&formhash=e2124516&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&sId=8742880&ts=1411888553&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=3b1569dfb8905e0d8482964fd125aea2&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3015, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=54fbed6e&srchtype=title&srhfid=0&srhlocality=portal%3A%3Aindex&sId=15701784&ts=1411881143&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=cb48c052cc260bd247759e8cb371de40&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3016, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=a8dc27ba&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&sId=2667439&ts=1411889531&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=eefc43b0c1e4021a5c58fbf4daacd063&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3017, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=9e2e4f68&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&sId=16140105&ts=1411889796&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=14eec51a6d865792d1916f71b57ddbe8&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3018, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/search?q=<>&sId=15702456&ts=1411890222&mySign=44dc34f9&menu=1&rfh=1&qs=txt.form.a",
            //           "utf-8", 3019, "上海"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.xcar.com.cn/search.php?c=5&k=<>&s=dateline_desc&f=_all&p=10&d=0&i=0&fid=0&fctbox=0&brbox=0&special=0",
            //           "utf-8", 3020, "上海"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=search&formhash=0da79e6c&srchtype=title&srhfid=0&srhlocality=portal%3A%3Aindex&sId=9206497&ts=1411892651&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=eb1e5c99c1184e1ad0da7b5be6dee848&charset=utf-8&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "utf-8", 3021, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://www.cheyisou.com/luntan/<>/",
            //           "utf-8", 3022, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=c3d9f9d2&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&sId=13689299&ts=1411892824&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=75335b4df396a6e5fd6ca27cf41f3531&charset=utf-8&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "utf-8", 3023, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://so.fblife.com/searchinfo.fb?query=<>&fromtype=0",
            //           "utf-8", 3024, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=9e08e1d6&srchtype=title&srhfid=&srhlocality=forum%3A%3Aindex&sId=5404118&ts=1411896561&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=76281340319cccd43207410001afc345&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3025, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=9e98cb6d&srchtype=title&srhfid=0&srhlocality=portal%3A%3Aindex&sId=1757184&ts=1411897269&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=618e199764ce02eab3c856a767d42c98&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3026, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://so.ddqcw.com/f/discuz?sId=8252412&ts=1411897746&cuId=0&cuName=&gId=7&egIds=&fmSign=&ugSign7=&sign=b2646f71af04775b629c5ba64583cc4b&q=<>&charset=gbk",
            //           "gbk", 3027, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://so.t56.net/f/discuz?mod=forum&formhash=9efad17d&srchtype=title&srhfid=0&srhlocality=portal%3A%3Aindex&sId=9669700&ts=1411897962&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=7b3217b889635a8b4d21ae6fb0d02f2e&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3028, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.tianya.cn/bbs?q=<>",
            //           "utf-8", 3029, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://club.pchome.net/forum_1_26____md__1_<>.html",
            //           "gbk", 3030, "福特"));
            
            //targetList.Add(getTestCrawlTarget(
            //           "http://www.shouduren.com/so/?lx=2&key=<>",
            //           "gbk", 3031, "北京"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://www.cqsq.com/?m=search&key=<>",
            //           "gbk", 3032, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.bbs.xwh.cn/f/discuz?mod=forum&formhash=65de4b29&srchtype=title&srhfid=276&srhlocality=forum%3A%3Aforumdisplay&sId=1642005&ts=1411902626&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=0a56258206a862d42b85425f2faed6d6&charset=gbk&source=discuz&fId=276&q=<>&searchsubmit=true",
            //           "gbk", 3033, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=3f8c111f&srchtype=title&srhfid=13316&srhlocality=forum%3A%3Aforumdisplay&sId=28804822&ts=1411903071&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=76c774cae175bad54bb4a2703633f97b&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3034, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://soso.hb130.com/f/discuz?mod=forum&formhash=ea2860d2&srchtype=title&srhfid=30&srhlocality=forum%3A%3Aforumdisplay&sId=2825460&ts=1411903188&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=81d995d3e035d93ca5c38bbd3f92ea4c&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3035, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://s.linfen365.com/f/discuz?mod=forum&formhash=efbb78d9&srchtype=title&srhfid=146&srhlocality=forum%3A%3Aindex&sId=1697386&ts=1411903480&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=0b60dda3e5f6ee89055cfb1349848dd6&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3036, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=97881459&srchtype=title&srhfid=82&srhlocality=forum%3A%3Aforumdisplay&sId=2845608&ts=1411903086&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=4a678e7d4ab3650cd150f9fcc50a7683&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3037, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=1455399b&srchtype=title&srhfid=56&srhlocality=forum%3A%3Aforumdisplay&sId=1842877&ts=1411904611&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=52889ed07a2ba2fcfda47606ce9c5591&charset=utf-8&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "utf-8", 3038, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?sId=5482506&ts=1411904887&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=49f123e2c2db269ca685fdea7afc0b49&charset=gbk&source=discuz&q=<>&module=forum",
            //           "gbk", 3039, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=b435029f&srchtype=title&srhfid=59&srhlocality=forum%3A%3Aforumdisplay&sId=15025902&ts=1411905154&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=6af726d7bef95a0a863256958c4f7bec&charset=utf-8&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "utf-8", 3040, "大众"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=f66c22cb&srchtype=title&srhfid=579&srhlocality=forum%3A%3Aforumdisplay&sId=1076049&ts=1411905528&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=c5e957edd53418dc6ae266b49b422005&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3041, "福特"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?sId=19883530&ts=1411905730&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=383369694f92b60cdd8beba07a995c46&charset=gbk&source=forum_thread&q=<>&module=forum&fId=46",
            //           "gbk", 3042, "大众"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=6958912b&srchtype=title&srhfid=130&srhlocality=forum%3A%3Aforumdisplay&sId=2695678&ts=1411905804&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&sign=04c1f274d41a685c3aacd756401c954d&charset=utf-8&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "utf-8", 3043, "大众"));

            //targetList.Add(getTestCrawlTarget(
            //           "http://search.discuz.qq.com/f/discuz?mod=forum&formhash=590dbb34&srchtype=title&srhfid=93&srhlocality=forum%3A%3Aforumdisplay&sId=5861890&ts=1411906890&cuId=0&cuName=&gId=7&agId=0&egIds=&fmSign=&ugSign7=&ext_vgIds=0&sign=e3a7b78bec95109fad9ed06e67637987&charset=gbk&source=discuz&fId=0&q=<>&searchsubmit=true",
            //           "gbk", 3044, "大众"));

            //targetList.Add(getTestCrawlTarget(
            //          "POST:http://www.jinbifun.com/search.php?searchsubmit=yes",
            //          "utf-8", 3045, "轮胎",
            //          "mod=curforum&formhash=497e6402&srchtype=title&srhfid=73405&srhlocality=forum%3A%3Aforumdisplay&srchtxt=<>&searchsubmit=true"
            //          ));

            targetList.Add(getTestCrawlTarget(
                       "POST:http://www.gslzw.com/search.php?mod=forum",
                       "gbk", 3046, "甘肃",
                       "formhash=d7bcd4b4&srchtxt=<>&searchsubmit=yes"
                       ));

            return targetList;
        }

        private CrawlTarget getTestCrawlTarget(string siteUrl, string encoding, int tid, string keyword,string postContent=null)
        {
            CrawlTarget crawlTarget = new CrawlTarget();

            crawlTarget.SiteUrl = siteUrl;
            crawlTarget.SiteEncoding = encoding;
            crawlTarget.RunStatus = 1;
            crawlTarget.ProjectId = "0";
            crawlTarget.SiteId = "0";
            crawlTarget.ClassName = null;
            crawlTarget.Tid = tid.ToString();
            crawlTarget.KeyWords = keyword;
            crawlTarget.PostContent = postContent;            

            return crawlTarget;
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
