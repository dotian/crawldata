using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.Model;
using System.Text.RegularExpressions;

namespace ConGetData.BLL
{
    public interface ISpiderData
    {
        void RunWork(CrawlTarget target);

        XmlTemplate GetXmlTemplate(string tempContent);

        MatchCollection GetMatchCollection(Regex reg,string html);

        DataModel GetDataModel(XmlTemplate xmlTemp, string inputMatchHtml, string parentUrl);

        int InsertSiteData(DataModel dataModel);
    }
}
