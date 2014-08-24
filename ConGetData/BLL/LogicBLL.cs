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
    public class LogicBLL
    {
        IServiceDAL dalAction = ServiceLocator.GetServiceDAL();
        public List<CrawlTarget> GetCrawtarget()
        {
            List<CrawlTarget> listTarget = dalAction.GetTargetService();
            //这里给模板
            foreach (CrawlTarget item in listTarget)
            {
                try
                {
                    item.XmlTemplate = GetXmlTemplate(int.Parse(item.Tid));
                }
                catch (Exception ex)
                {
                    LogBLL.Error("模板Id : " + item.Tid + "解析错误", ex);
                }
            }
            return listTarget;
        }

        private static XmlTemplate[] xml_Arr = new XmlTemplate[20000];
        public static XmlTemplate GetXmlTemplate(int tid)
        {
            XmlTemplate xmlTemplate = new XmlTemplate();
            if (xml_Arr[tid] == null)
            {
                xml_Arr[tid] = new XmlTemplate(File.ReadAllText(ModelArgs.XmlTempPath.Replace("#tid", tid.ToString()), Encoding.UTF8));
            }
            return xml_Arr[tid];
        }
    }
}
