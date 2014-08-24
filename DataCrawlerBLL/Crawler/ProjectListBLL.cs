using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;

namespace DataCrawler.BLL.Crawler
{
    public class ProjectListBLL 
    {
        /// <summary>
        /// 添加新项目
        /// </summary>
        /// <param name="pList">项目ProjectList对象</param>
        /// <param name="sp_forum">论坛分类|cateId =1|</param>
        /// <param name="sp_news">新闻分类|cateId =4|</param>
        /// <param name="sp_blog">博客分类|cateId =2|</param>
        /// <param name="sp_microblog">微博分类|cateId =6|</param>
        /// <returns>返回项目ID</returns>
        public int InsertProjectListManager(ProjectList pList, string sp_forum, string sp_news, string sp_blog, string sp_microblog)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            int projectId = sqlDalAction.InsertProjectListService(pList);
            try
            {
                if (projectId > 0)
                {
                    int result = 0;

                    //得到配置文件里面的 常抓的 论坛 新闻,博客,不带搜索功能的 CateId 配置


                    //项目添加成功, 接下来是 分配分类
                    if (sp_forum != "")
                    {
                        List<string> list = GetCateIds(sp_forum);
                        result = AllotCategoryManager(projectId, list);
                        sqlDalAction.InsertPullInfoService(projectId, 1, 5000);
                        LogNet.LogBLL.Info(projectId + "项目分配论坛站点" + result);
                    }
                    if (sp_news != "")
                    {
                        List<string> list = GetCateIds(sp_news);
                      result= AllotCategoryManager(projectId, list);
                      sqlDalAction.InsertPullInfoService(projectId, 2, 1000);
                      LogNet.LogBLL.Info(projectId + "项目分配新闻站点" + result);
                    }
                    if (sp_blog != "")
                    {
                        List<string> list = GetCateIds(sp_blog);
                       result= AllotCategoryManager(projectId, list);
                       sqlDalAction.InsertPullInfoService(projectId, 3, 1000);
                       LogNet.LogBLL.Info(projectId + "项目分配博客站点" + result);
                    }
                    if (sp_microblog != "")
                    {
                        List<string> list = GetCateIds(sp_microblog);
                       result= AllotCategoryManager(projectId, list);
                       sqlDalAction.InsertPullInfoService(projectId, 5, 1000);
                       LogNet.LogBLL.Info(projectId + "项目分配微博站点" + result);
                    }
                }
            }
            catch(Exception ex)
            {
               LogNet.LogBLL.Error("InsertProjectListManager", ex);
                return 0;
            }
            return projectId;
        }


        public List<string> GetCateIds(string cateIds)
        {
            cateIds = cateIds.Replace("||", "|");
            string[] array = cateIds.Split('|');
            List<string> list = new List<string>();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i]!="")
                {
                    list.Add(array[i]);
                }
            }

            return list;
        }


        /// <summary>
        /// 给项目分类,分配站点
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="cateIds"></param>
        /// <returns></returns>
        public int AllotCategoryManager(int projectId, List<string> cateidsList)
        {
            int result = 0;
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            try
            {
                int cateId = 0;
                foreach (string cateidStr in cateidsList)
                {
                    int.TryParse(cateidStr, out cateId);
                    if (cateId == ModelArgs.RC_Forum_CateId)
                    {
                        //获取基础数据 #xiang
                        // 更新 ProjectList 的 RC_Forum 为 1
                        result = sqlDalAction.UpdateProjectListRC_ForumService(projectId,1);
                    }
                    else if (cateId == ModelArgs.RC_News_CateId)
                    {
                        //获取基础数据 #xiang
                        result = sqlDalAction.UpdateProjectListRC_NewsService(projectId, 1);
                    }
                    else if (cateId == ModelArgs.RC_Blog_CateId)
                    {
                        //获取基础数据 #xiang
                        result = sqlDalAction.UpdateProjectListRC_BlogService(projectId, 1);
                    }
                    else
                    {
                        result += sqlDalAction.AllotCategoryService(projectId, cateId);
                    }
                }
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("AllotCategoryManager", ex);
            }
            return result;
        }

        public List<ProjectList> GetProjectListManager(int searchType, string sarchkey, int runStatus,string loginName)
        {
            //只有 admin 和所有高级帐号你呢个看到所有的项目,其余的只能看到自己的项目
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            List<ProjectList> list = sqlDalAction.GetProjectListService(searchType, sarchkey, runStatus, loginName);
            foreach (ProjectList pList in list)
            {
                pList.MatchingRuleTypeName = pList.MatchingRuleType == 0 ? "标题及内容" : (pList.MatchingRuleType == 1 ? "标题" : "内容");
            }
            return list;
        }

        public ProjectDetailModel GetProjectListDetailByProjectidManager(int projectId)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            ProjectDetailModel pModel = sqlDalAction.GetProjectListDetailByProjectidService(projectId);
            return pModel;
        }

        public List<SiteList> GetUseableSiteListByProjectIdManager(int projectId, int siteType, int pageSize, int pageIndex)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            List<SiteList> list = sqlDalAction.GetUseableSiteListByProjectIdService(projectId, siteType, pageSize, pageIndex);
            return list;
        }

        public int GetRecordCountUseableSiteListByProjectIdManager(int projectId, int siteType)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            int recordCount = sqlDalAction.GetRecordCountUseableSiteListByProjectIdService(projectId, siteType);
            return recordCount;
        }

        public int UpdateProjectListStatusByProjectIdManager(int projectId,int runStatus)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            int result = sqlDalAction.UpdateProjectListStatusByProjectIdService(projectId, runStatus);
            return result; 
        }

    }
}
