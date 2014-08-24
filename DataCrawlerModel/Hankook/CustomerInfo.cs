using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Hankook
{
     public  class CustomerInfo
    {
         /// <summary>
         /// 登录名 主键
         /// </summary>
        public string CustomerId { get; set; }
         /// <summary>
         /// 密码
         /// </summary>
        public string LoginPwd { get; set; }
         /// <summary>
         /// 密钥
         /// </summary>
        public string SecretKey { get; set; }
         /// <summary>
         /// 谁建的这个账号
         /// </summary>
        public string EmpName { get; set; }
         /// <summary>
         /// 用户权限 3是 低级用户, 4是高级用户
         /// </summary>
        public int UserPermissions { get; set; }

         /// <summary>
        /// 可以查看的项目Id, UserPermissions为4,ProjectId 为0时,可以查看到所有的项目
         /// </summary>
        public int ProjectId { get; set; }

         /// <summary>
         ///  账号状态 1有用, 0 冻结无效状态
         /// </summary>
        public int RunStatus { get; set; }

    }
}
