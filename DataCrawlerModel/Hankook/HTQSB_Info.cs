using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Hankook
{
    /// <summary>
    /// 话题趋势表
    /// </summary>
    public class HTQSB_Info
    {
        public int Id { get; set; }
        public string ContentDate { get; set; }
        public int News_Z_Num { get; set; }
        public int News_F_Num { get; set; }
        public int Blog_Z_Num { get; set; }
        public int Blog_F_Num { get; set; }
        public int Forum_Z_Num { get; set; }
        public int Forum_F_Num { get; set; }
        

        public int Microblog_F_Num { get; set; }
    }

    public class HTQSB_Z_Info
    {
        public string ContentDate { get; set; }
        public int News_Z_Num { get; set; }
        public int Blog_Z_Num { get; set; }
        public int Forum_Z_Num { get; set; }

    }
    public class HTQSB_F_Info
    {
        public string ContentDate { get; set; }
        public int News_F_Num { get; set; }
        public int Blog_F_Num { get; set; }
        public int Forum_F_Num { get; set; }
        public int Microblog_F_Num { get; set; }
    }
}
