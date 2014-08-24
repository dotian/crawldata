
using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;

namespace DataCrawler.BLL.Crawler
{
    public class TagBLL
    {

          public List<TagList> GetTagListManager()
          {
               TagDAL dal = new TagDAL();
               return dal.GetTagListService();
          }

            /// <summary>
            /// 得到 二级标签
            /// </summary>
            /// <param name="tid">一级标签的Tid</param>
            /// <returns></returns>
          public List<TagList> GetTagListByTidManager(int tid)
          {
              TagDAL dal = new TagDAL();
              return dal.GetTagListByTidService(tid);
          }

         public int InsertTagManager(int tid,string tagname)
         {
              TagDAL dal = new TagDAL();
              return dal.InsertTagService(tid,tagname);
             
         }
         public int UpdateTagByIdManager(int tid, string tagname)
         {
             TagDAL dal = new TagDAL();
             return dal.UpdateTagByIdService(tid, tagname);
         }
         public int DeleteTagByIdManager(int tid)
         {
             TagDAL dal = new TagDAL();
             return dal.DeleteTagByIdService(tid);
             
         }

        /// <summary>
        /// 根据项目得到项目的一级标签
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
         public List<TagList> Get1stTagByProjectIdManager(int projectId)
          {
               TagDAL dal = new TagDAL();
               return dal.Get1stTagByProjectIdService(projectId);
          }
        
    }
}
