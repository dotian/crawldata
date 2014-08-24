using System;
using System.Collections.Generic;

using System.Text;
using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;
using System.Data;
namespace DataCrawler.BLL.Crawler
{
    public class ReadExcelBLL
    {
        HelperExcel helpExcel = new HelperExcel();
        public DataTable GetDataTable(string path)
        {

            string connExcelStr = helpExcel.GetConnStr(path);
            return helpExcel.ExcelToDataTable(connExcelStr);

        }
    }
}
