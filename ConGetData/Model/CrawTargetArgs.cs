using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.BLL;

namespace  ConGetData.Model
{
    public class CrawlTargetArgs
    {
       public CrawlTargetArgs() { }
       public CrawlTargetArgs(ThreadCounter treadCounter, CrawlTarget CrawlTarget)
       {
           this.ThreadCounter = treadCounter;
           this.CrawlTarget = CrawlTarget;
       }
       public ThreadCounter ThreadCounter { get; set; }
       public CrawlTarget  CrawlTarget{ get; set; }

    }
    public class CrawlSiteArgs
    {
        public CrawlSiteArgs() { }
        public CrawlSiteArgs(ThreadCounter treadCounter, CK_SiteList CrawlTarget)
        {
            this.ThreadCounter = treadCounter;
            this.CrawlTarget = CrawlTarget;
        }
        public ThreadCounter ThreadCounter { get; set; }
        public CK_SiteList CrawlTarget { get; set; }

    }
}
