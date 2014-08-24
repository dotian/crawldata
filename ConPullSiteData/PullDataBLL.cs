using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using DataCrawler.DAL.Crawler;

using System.Threading;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using ConPullSiteData;
namespace DataCrawler.BLL.Crawler
{
    /// <summary>
    /// 拉数据
    /// </summary>
    public class PullDataBLL
    {
        public static Dictionary<int, string> RssP_And_KDict = new Dictionary<int, string>();
        public void PullRunManagerAction()
        {
            int sleepPullTime = int.Parse(ConfigurationManager.AppSettings["SleepPullTime"].ToString());

            while (true)
            {
                LogNet.LogBLL.Info("PullRunManagerThread 轮循拉数据开始");
                
                PullRunManager();
                LogNet.LogBLL.Info("PullRunManagerThread 轮循拉数据成功");
                Thread.Sleep(sleepPullTime * 1000);
            }
        }

         PullDataDAL sqlDalAction = new PullDataDAL();
         public void PullRunManager()
         {
             try
             {
                 RssP_And_KDict.Clear();
                 RssP_And_KDict = sqlDalAction.GetRssP_And_KDictServie();
                 PullNewsRSSWork();

                 sqlDalAction.PullDataFirstService();
             }
             catch (Exception ex)
             {
                 LogNet.LogBLL.Error("PullRunManager", ex);
                
             }
         }

          
        /// <summary>
        /// 定时 获取Rss数据
        /// </summary>
         private static int pullHouerTask = 0; // 0, 1,2
        /// <summary>
        /// 韩泰新闻 Rss 数据获取
        /// </summary>
         public void PullNewsRSSWork()
         {
             int houer = DateTime.Now.Hour/8;
             if (houer == pullHouerTask)
             {
                 pullHouerTask = pullHouerTask < 2 ? pullHouerTask++ :0;
                 //然后 xiaoyong 

                 //先获取一个项目的Rsskey,这个因该是键值
                 List<RssDataInfo> list = PullNewsRSSAction();
                 int result = sqlDalAction.InsertSiteDataByRssData(list);
                if (result>0)
                {
                    LogNet.LogBLL.Info("韩泰 Rss 数据 获取成功!");
                    Console.WriteLine("韩泰 Rss 数据 获取成功!");
                }
                else
                {
                    Console.WriteLine("韩泰 Rss 数据已获悉,数据已存在.");
                }
             }
         }

       




         /// <summary>
         /// 拉取 Rss数据
         /// </summary>
         public List<RssDataInfo> PullNewsRSSAction()
         {
             XmlReader reader = null;
             List<RssDataInfo> listRss = new List<RssDataInfo>();
             try
             {
                 reader = XmlTextReader.Create("http://hankook.xlmediawatch.com/main/hankook.nsf/rss");
                 XmlDocument xmlDoc = new XmlDocument();
                 xmlDoc.Load(reader);

                 XmlNode root = xmlDoc.GetElementsByTagName("channel")[0];

                 foreach (XmlNode item in root.SelectNodes("item"))
                 {
                     RssDataInfo contendRss = new RssDataInfo();
                     string title = item.SelectSingleNode("title").InnerText;
                     int start  = title.LastIndexOf("(")+1;
                   
                     contendRss.title = title.Substring(0, start-1);
                     contendRss.Published = item.SelectSingleNode("Published").InnerText;
                     contendRss.description = item.SelectSingleNode("description").InnerText;
                     contendRss.link = item.SelectSingleNode("link").InnerText;
                     contendRss.type = item.SelectSingleNode("type").InnerText;
                     contendRss.tags = item.SelectSingleNode("tags").InnerText;
                     contendRss.contend = item.SelectSingleNode("contend").InnerText;
                     contendRss.analysis = item.SelectSingleNode("tonality").InnerText=="正"?1:3;
                     contendRss.sitename = title.Substring(start, title.Length - start-1);
                     contendRss.ProjectId = GetProjectKeyByRssKey(contendRss.contend);
                     if (contendRss.ProjectId>0)
                     {
                         listRss.Add(contendRss);
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogNet.LogBLL.Error("PullNewsRSSAction", ex);
             }
             finally
             {
                 if (reader != null)
                 {
                     reader.Close();
                 }
             }
             return listRss;
         }


          public Func<string, int> GetProjectKeyByRssKey = (string rsskey) =>
            {
                return RssP_And_KDict.FirstOrDefault(q => q.Value == rsskey).Key;
            };

    }
}
