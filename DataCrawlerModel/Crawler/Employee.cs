using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Crawler
{
    public class Employee
    {
      //  EmpId	LoginPwd	SecretKey	EmpName	Permissions	RunStatus	CreateDate	Company	District	Email	Contend

        public string EmpId { get; set; }

        public string LoginPwd { get; set; }

        public string SecretKey { get; set; }

        public string EmpName { get; set; }
        /// <summary>
        /// 权限 0 普通用户 1
        /// </summary>
        public int UserPermissions { get; set; }

        /// <summary>
        /// 0 表示运行正常 1表示 冻结,99表示删除
        /// </summary>
        public int RunStatus { get; set; }


        public DateTime CreateDate { get; set; }


        public int Company { get; set; }

       // public string District { get; set; }

        /// <summary>
        /// 竞争社
        /// </summary>
        public string Contend { get; set; }


    }
}
