using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using RionSoft.IBRS.Business.DAL;
using DataCrawler.Model.Crawler;
using LogNet;


namespace DataCrawler.DAL.Crawler
{
    public class TagDAL
    {
        public TagDAL() { }

        /// <summary>
        ///  QueryTag
        /// </summary>
        /// <returns>返回一个DataTable</returns>
        public DataTable QueryTag()
        {

            DataTable dt = new DataTable();
            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_Mining_Select_Tag_All");
                dt = dal.SelectData("usp_Mining_Select_Tag_All", null);
            }
            catch (Exception ex)
            {
                LogBLL.Error("QueryTagById", ex);

            }
            return dt;

        }

        public DataTable QueryTagById(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_Mining_Select_Tag_ById");
                dt = dal.SelectData("usp_Mining_Select_Tag_ById", null);
            }
            catch (Exception ex)
            {
                LogBLL.Error("QueryTagById", ex);
            }
            return dt;
        }

        /// <summary>
        /// 得到一级标签
        /// </summary>
        /// <returns></returns>
        public List<TagList> GetTagListService()
        {
            List<TagList> list = new List<TagList>();
            DataTable dt = new DataTable();
            string sql = "select Id,Tid,TagName from TagList where Tid = 0";
            IBRSCommonDAL dal = new IBRSCommonDAL();
            dt = dal.SelectData(sql);
            foreach (DataRow row in dt.Rows)
            {
                TagList t = new TagList();
                t.Id = Convert.ToInt32(row["Id"]);
                t.Tid = Convert.ToInt32(row["Tid"]);
                t.TagName = row["TagName"].ToString();
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// 得到二级标签
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public List<TagList> GetTagListByTidService(int tid)
        {
            List<TagList> list = new List<TagList>();
            DataTable dt = new DataTable();
            string sql = "select Id,Tid,TagName from TagList where Tid = " + tid + " order by id desc";
            IBRSCommonDAL dal = new IBRSCommonDAL();

            dt = dal.SelectData(sql);
            foreach (DataRow row in dt.Rows)
            {
                TagList t = new TagList();
                t.Id = Convert.ToInt32(row["Id"]);
                t.Tid = Convert.ToInt32(row["Tid"]);
                t.TagName = row["TagName"].ToString();
                list.Add(t);
            }
            return list;
        }

        public int InsertTagService(int tid, string tagname)
        {
            IBRSCommonDAL dal = new IBRSCommonDAL();
            int tagId = 0;
            try
            {
                string sql = "insert into TagList(Tid,TagName) values(@tid,@tagname) select SCOPE_IDENTITY()";

                SqlParameter[] parms = new SqlParameter[] { 
                    new  SqlParameter("tid",tid),
                    new  SqlParameter("tagname",tagname)
                };
                object objResult = dal.ExecuteScalar(sql, parms, CommandType.Text);
                tagId = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {
                LogBLL.Error("InsertTagService", ex);
            }

            return tagId;
        }

        public int UpdateTagByIdService(int tid, string tagname)
        {

            int result = 0;
            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();
                string sql = "update TagList set TagName=@tagname where Id = @tid";
                SqlParameter[] parms = new SqlParameter[] { 
                  new  SqlParameter("tid",tid),
                    new  SqlParameter("tagname",tagname)
                };
                result = dal.ExecuteNonQuery(sql, parms, CommandType.Text);

            }
            catch (Exception ex)
            {

                LogBLL.Error("UpdateTagByIdService", ex);
            }
            return result;
        }

        public int DeleteTagByIdService(int tid)
        {
            int result = 0;
            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();
                string sql = "delete from TagList where Id = " + tid;
                result = dal.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                LogBLL.Error("DeleteTagByIdService", ex);
            }
            return result;
        }

        public List<TagList> Get1stTagByProjectIdService(int projectId)
        {
            List<TagList> list = new List<TagList>();
            string sql = "select Id,Tid,TagName  from TagList where Id in (select distinct Tid from view_mining_ProjetTagRelation where projectid =" + projectId + ")";

            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt = dal.SelectData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    TagList t = new TagList();
                    t.Id = Convert.ToInt32(row["Id"]);
                    t.Tid = Convert.ToInt32(row["Tid"]);
                    t.TagName = row["TagName"].ToString();
                    list.Add(t);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Log(sql);
                LogBLL.Error("DeleteTagByIdService", ex);
            }
            return list;
        }
    }
}
