using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using RionSoft.IBRS.Business.DAL;


using DataCrawler.Model.Crawler;


namespace DataCrawler.DAL.Crawler
{
    public class PageDAL : IBRSCommonDAL
    {
        //public PageDAL() { }


        /// <summary>
        /// 得到一个datatable
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public DataTable GetDropListStore(string sql)
        {
            DataTable dt = base.SelectData(sql);

            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() == "")
                {
                    row.Delete();
                }
            }
            return dt;
        }

        public int GetObject(string sql)
        {
            object obj = base.ExecuteScalar(sql);
            return obj != null ? (int)obj : 0;
        }

        static public int GetInt(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return 0;
            return rec.GetInt32(fldnum);
        }

    }
}

