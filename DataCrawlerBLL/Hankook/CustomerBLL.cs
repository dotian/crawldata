using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCrawler.Model.Hankook;
using DataCrawler.DAL.Hankook;
using DataCrawler.BLL.Crawler;

namespace DataCrawler.BLL.Hankook
{
    public class CustomerBLL
    {
        CostomerDAL dalService = new CostomerDAL();
        public List<ShowDataInfo> GetShowDataListManager(QueryHankookArgs queryArgs)
        {
            List<ShowDataInfo> list = new List<ShowDataInfo>();
             list = dalService.GetCustomerSiteDataService(queryArgs);
            return list;
        }

        public int GetShowDataCountManager(QueryHankookArgs queryArgs)
        {
            int result = dalService.GetCustomerSiteDataCountService(queryArgs);
            return result;
        }

        public int UpdateShowDataMess(int id, string datatype, string mess)
        {
            int result = dalService.UpdateSiteDataMessByIdService(id, mess);
            return result;
        }

        public List<ProjectInfo> GetProjectListManager(string userName)
        {
            return dalService.GetProjectListService(userName);
        }

        public int GetCustomerByCustomerIdManager(string customerId, string loginpwd, ref int projectId, ref int permissions)
        {
           CustomerInfo customer = dalService.GetCustomerByCustomerIdService(customerId);
           int loginResult = 0;
           bool b = Md5Helper.CompareMd5pwd(loginpwd, customer.SecretKey, customer.LoginPwd);
            if (b)
            {
                if (customer.RunStatus==0)
                {
                    loginResult = 3; //用户状态冻结
                }
                else
                {
                    loginResult = 1; //登录成功
                    projectId = customer.ProjectId;
                    //低级用户 看单个项目, 高级用户 看所有项目
                    permissions = customer.UserPermissions;//3 低级, 4 高级
                }
            }
            else
            {
                //密码错误
                loginResult = 2;
            }
            return loginResult;
        }

        public bool GetExistContentIdManager(int projectId, int contendId)
        {
            return dalService.GetExistContentIdService(projectId,contendId);
        }
        public List<ContendTB> GetContendTbListByProjectIdManager(int projectId)
        {
            return dalService.GetContendTbListByProjectIdService(projectId);
        }
       
    }
}
