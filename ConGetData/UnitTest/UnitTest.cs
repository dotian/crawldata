using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.Model;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using ConGetData.DAL;
using LogNet;


namespace ConGetData.BLL
{
    //单元测试类
     class UnitTest
    {
        LogicBLL logicAction = new LogicBLL();
        public ThreadCounter threadCounter = new ThreadCounter();
        public static int CrawlListCount = 0;
        public static IList<CrawlTarget> CrawlListRun = new List<CrawlTarget>();

        /// <summary>
        /// 得到 任务的个数
        /// </summary>
        public void Test1()
        {
            ModelArgs.ProjectType = 1;
            List<CrawlTarget> listTarget = logicAction.GetCrawtarget();
            Console.WriteLine("当前站点任务的个数:" + listTarget.Count);
            Console.WriteLine("测试结束");
        }

        /// <summary>
        /// 测试任务的类型
        /// </summary>
        public void Test2()
        {
           //论坛,新闻,博客,微博
            int tid = 2005;
            string path = @"H:\2014Year\XmlTemp\新增模板\" + tid + ".xml";

            //得到了一个模板,然后需要检测抓到的数据
            //<span id="thread_\d+"><a href="(.+?)"
           
            CrawlTarget target = new CrawlTarget();
            //target.SiteUrl = "http://search.sina.com.cn/?q=%BA%AB%CC%A9%C2%D6%CC%A5&c=news&sort=time&page={p}";

            target.SiteUrl = "http://www.qihoo.com/wenda.php?r=search/index&kw=%BA%AB%CC%A9%C2%D6%CC%A5&do=search&area=2&src=wenda_tab&sort=pdate&page={p}&time=month&type=bbs";
            
            
            target.ProjectId = "2000";
            target.SiteId = "2000";
            target.TemplateContent = File.ReadAllText(path,Encoding.UTF8);
            target.XmlTemplate = new XmlTemplate(target.TemplateContent);
            target.KeyWords = "韩泰";
            target.Tid = tid.ToString();

            //SystemConst.StrCookie = "SUB=AakizsEigS01t6zKPsTplQlU6oyVtqndHoktS4eW8JLdzao40gJdWAQQOTCbraqX2lbipHBA1TQtt7%2BmRQpR1l18RMqix5vm%2FxFeK4c4DUJ3; SUBP=00170a121e2bf140000; SINAGLOBAL=7315454010413.5205.1392969010565; ULV=1393900358626:36:1:1:9383520862161.451.1393900358609:1393483267273; SUS=SID-3087668437-1393900411-JA-zubqn-fa028a7eeede62164049466e7f785d22; SUE=es%3Dd4b94ced0085017b5b99666626171536%26ev%3Dv1%26es2%3D8c5d7ea9b2f182ffeb16aa45d1aa73df%26rs0%3DBGGyAj18zmFe4AmF25TTzIO47q7z90TAm%252F9flMwX6MbQM1JodJUMK4LDn5eCoRc5NFgr3l1OdGTEKgbtFFkGqveY5EwfHstAxPr9dJV3vgYOAHqEKseWxW%252BA%252FhtBRH%252BmwBAfwxnQLN5E49pj9F5Ym9G30RSe9wqKXL2ATJD%252BsXg%253D%26rv%3D0; SUP=cv%3D1%26bt%3D1393900411%26et%3D1393986811%26d%3Dc909%26i%3Dd8fe%26us%3D1%26vf%3D0%26vt%3D0%26ac%3D%26st%3D0%26uid%3D3087668437%26name%3D996167490%2540qq.com%26nick%3D%25E9%2593%25AD%25E4%25BA%25A6%25E5%25B9%25BB%26fmp%3D%26lcp%3D; SSOLoginState=1393900411; UUG=usr1030; UV5=usrmdins312_151; _s_tentry=-; Apache=9383520862161.451.1393900358609; SWB=usrmd1073; WBStore=fdf3d991095a3826|undefined";
           // SystemConst.StrCookie = "UOR=www.a9188.com,widget.weibo.com,login.sina.com.cn; SINAGLOBAL=1859338083790.8064.1392954578791; ULV=1392974465043:2:2:2:4141450759511.8833.1392974465014:1392954579488; un=996167490@qq.com; myuid=3087668437; SUBP=00170a121e2bf140000; UV5=usrmdins312_154; ULOGIN_IMG=13929744986894; _s_tentry=-; Apache=4141450759511.8833.1392974465014; SWB=usrmd1367; WBStore=0e339435260d825e|undefined";
            CrawlTargetArgs args = new CrawlTargetArgs(new ThreadCounter(), target);    
            MainWork mainWork = new MainWork();
            MainWork.CrawlWorkAction(args);
            Console.WriteLine("完毕");
        }
    }
}
