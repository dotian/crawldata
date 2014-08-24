using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.Linq;
using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;
namespace DataCrawler.BLL.Crawler
{
    public class ContendBLL
    {
        ContendDAL dalService = new ContendDAL();
        public List<ProjectList> GetProjectInfoManager(string projectname)
        {

            return dalService.GetProjectInfoService(projectname);

        }

        /// <summary>
        /// 给项目添加 竞争社
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <param name="ids">竞争社项目Id列表</param>
        /// <param name="empId">员工Id, 由 谁创建的这个项目</param>
        /// <param name="contendKey">读取Rss所需要的关键字</param>
        /// <returns></returns>
        public int InsertContendManager(int projectId,string ids,string empId)
        {
            // 选择一个项目, 然后把它添加到 竞争社表

            int result = 0;
            try
            {
                List<string> list = ids.Split(',').ToList();
                list.RemoveAll(c => c == "" || c == projectId.ToString());

                for (int i = 0; i < list.Count; i++)
                {
                    int contendId = int.Parse(list[i]);
                    result += dalService.InsertContendByProjectService(projectId, contendId, empId);
                }
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("InsertContendManager",ex);
            }
           
            return result;
        }
    }
}
