using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Reflection;
using Microsoft.Office;
using Excel_COM = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;
using System.Text;
using DataCrawler.Model.Hankook;
using System.Linq;

/// <summary>
///ExcelCommon 的摘要说明
/// </summary>
public class ExcelCommon
{
    public ExcelCommon()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    #region 释放Excel线程
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);


    public static void KillSpecialExcel(Microsoft.Office.Interop.Excel.Application m_objExcel)
    {
        try
        {
            if (m_objExcel != null)
            {
                int processId;
                GetWindowThreadProcessId(new IntPtr(m_objExcel.Hwnd), out processId);

                Process.GetProcessById(processId).Kill();
            }
        }
        catch
        {

        }
    }
    #endregion


    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="contentRows"></param>
    /// <param name="outPutFile"></param>
    /// <returns></returns>
    public bool DoOneTimeExport(List<SiteDataModel> contentRows, string outPutFile)
    {
        bool b = false;
        ApplicationClass oExcel = new ApplicationClass();
        try
        {
            if (File.Exists(outPutFile))
            {
                File.Delete(outPutFile);
            }

            oExcel.UserControl = false;
            WorkbookClass workbookClass = (WorkbookClass)oExcel.Workbooks.Add(Missing.Value);
            workbookClass.Worksheets.Add(Missing.Value, Missing.Value, 1, Missing.Value);
            int rowIndex = 1;
            oExcel.Cells[1, 1] = "标题";
            oExcel.Cells[1, 2] = "链接";
            oExcel.Cells[1, 3] = "媒体名";
            oExcel.Cells[1, 4] = "时间";
            foreach (SiteDataModel row in contentRows)//这里确定行
            {
                rowIndex++;//从第二行开始写起
                oExcel.Cells[rowIndex, 1] = row.Title;
                oExcel.Columns.EntireColumn.AutoFit();
                oExcel.Cells[rowIndex, 2] = row.SrcUrl;
                oExcel.Columns.EntireColumn.AutoFit();
                oExcel.Cells[rowIndex, 3] = row.SiteName;
                oExcel.Columns.EntireColumn.AutoFit();
                oExcel.Cells[rowIndex, 4] = row.CreateDate.ToString("yyyy-MM-dd");
                oExcel.Columns.EntireColumn.AutoFit();
            }
            workbookClass.Saved = true;
            oExcel.ActiveWorkbook.SaveAs(outPutFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
             Excel_COM.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            b = true;
        }
        catch (Exception ex)
        {
            b = false;
            LogNet.LogBLL.Error("DoOneTimeExport", ex);
        }
        finally
        {
            try
            {
                oExcel.Quit();
                GC.Collect();
                KillSpecialExcel(oExcel);
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("DoOneTimeExport finally", ex);
            }
        }
        return b;
    }

    ReadExcelBLL excelActionBll = new ReadExcelBLL();
    public List<SiteDataModel> DoOneTimeImport(string filepath, int siteType, int projectId)
    {
        List<SiteDataModel> list = new List<SiteDataModel>();
        try
        {
            System.Data.DataTable table = excelActionBll.GetDataTable(filepath);
            LogNet.LogBLL.Debug("Total row in file: " + table.Rows.Count);

            foreach (System.Data.DataRow row in table.Rows)
            {
                try
                {
                    string title = row[0].ToString();
                    string url = row[1].ToString();
                    string siteName = row[2].ToString();
                    string plateName = row[2].ToString();
                    string contentDateStr = row[3].ToString();
                    string analysisStr = row[4].ToString();
                    string showStatusStr = row[5].ToString();
                    string siteTypeStr = row[6].ToString();

                    if (!(string.IsNullOrEmpty(title)
                        || string.IsNullOrEmpty(url)
                        || string.IsNullOrEmpty(siteName)
                        || string.IsNullOrEmpty(contentDateStr)))
                    {
                        SiteDataModel model = new SiteDataModel();
                        model.Title = title;
                        model.SrcUrl = url;
                        model.SiteName = siteName;
                        model.PlateName = siteName;
                        model.ContentDate = DateTime.Parse(contentDateStr);

                        model.Analysis = 0;
                        if (!string.IsNullOrEmpty(analysisStr))
                        {
                            if (SiteDataModel.AnalysisMap.ContainsValue(analysisStr))
                            {
                                model.Analysis = SiteDataModel.AnalysisMap.First(m => m.Value == analysisStr).Key;
                            }
                        }

                        model.ShowStatus = 0;
                        if (!string.IsNullOrEmpty(showStatusStr))
                        {
                            if (showStatusStr == "已审核")
                            {
                                model.ShowStatus = 2;
                            }
                        }

                        model.SiteType = siteType;
                        if (!string.IsNullOrEmpty(siteTypeStr))
                        {
                            if (SiteDataModel.SiteTypeMap.ContainsValue(siteTypeStr))
                            {
                                model.SiteType = SiteDataModel.SiteTypeMap.First(m => m.Value == siteTypeStr).Key;
                            }
                            else
                            {
                                throw new Exception("Not recognized site type: " + siteTypeStr);
                            }
                        }

                        model.ProjectId = projectId;

                        list.Add(model);
                    }
                }
                catch (Exception e)
                {
                    LogNet.LogBLL.Error(string.Format("Title {0} Url {1} Site name {2} content date {3} analysis {4} show status {5}"
                        , row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString()));
                    LogNet.LogBLL.Error("DoOneTimeImport", e);
                }
            }
        }
        catch (Exception ex)
        {
            LogNet.LogBLL.Error("DoOneTimeImport", ex);
        }
        return list;
    }


    public static string ServerMapPath { get; set; }

    private static string temp_Content = "";
    public static string Temp_Content
    {
        get
        {
            if (string.IsNullOrEmpty(temp_Content))
            {
                temp_Content = File.ReadAllText(ServerMapPath + "excelXmlTempContent.xml", Encoding.UTF8);
            }
            return temp_Content;
        }
    }

    private static string temp_Row = "";
    public static string Temp_Row
    {
        get
        {
            if (string.IsNullOrEmpty(temp_Row))
            {
                temp_Row = File.ReadAllText(ServerMapPath + "excelXmlTempRow.xml", Encoding.UTF8);
            }
            return temp_Row;
        }
    }


    public bool Export_XMLExcel(int projectId, List<SiteDataModel> contentRows, string outPutFile)
    {
        bool b = false;
        try
        {

            List<TagList> list = new TagBLL().Get1stTagByProjectIdManager(projectId);
            string[] tagAttr = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                tagAttr[i] = list[i].TagName;
            }
            int columCount = 5 + tagAttr.Length;
            // 根据 tagAttr.lenth 确定 模板

            //序列化为文本
            StringBuilder sBulider = new StringBuilder();
            int num = 1;
            foreach (SiteDataModel row in contentRows)
            {
                string newXmlRow = Temp_Row;
                //需要 对xml 字符转义
                newXmlRow = newXmlRow.Replace("#temp_Title", GetFormatXml(row.Title))
                    .Replace("#temp_SiteUrl", GetFormatXml(row.SrcUrl))
                    .Replace("#temp_SiteName", GetFormatXml(row.SiteName))
                    .Replace("#temp_CreateDate", row.CreateDate.ToString("yyyy-MM-ddTHH:ss:mm"))
                    .Replace("#temp_Analysis", GetFormatAnalysis(row.Analysis))
                    .Replace("#temp_TagCellList", GetFormatTagCell(tagAttr.Length, row.Tag1, row.Tag2, row.Tag3, row.Tag4, row.Tag5, row.Tag6));//这里放标签
                sBulider.Append(newXmlRow);
                num++;
            }

            //写入文件
            string newXmlContent = Temp_Content;
            newXmlContent = newXmlContent.Replace("#temp_ColumCount", columCount.ToString())
                 .Replace("#temp_RowCount", num.ToString())
                 .Replace("#temp_TagTitleList", GetFormatTagTitle(tagAttr))
                 .Replace("#temp_Row", sBulider.ToString());
            File.WriteAllText(outPutFile, newXmlContent, Encoding.UTF8);
            b = true;
        }
        catch (Exception ex)
        {
            LogNet.LogBLL.Error("DoOneTimeExport_xml", ex);
        }
        return b;
    }

    public static string temp_TagTitle = "<Cell ss:StyleID=\"s63\"><Data ss:Type=\"String\">#temp_tagTitle</Data></Cell>\n";
    public static string Temp_TagTitle { get { return temp_TagTitle; } }

    private static string temp_TagCell = "<Cell ss:StyleID=\"s67\"><Data ss:Type=\"String\">#temp_tagCell</Data></Cell>\n";
    public static string Temp_TagCell { get { return temp_TagCell; } }

    private static string csv_row = "\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"\r\n";

    public bool Export_HankookCsv(List<ShowDataInfo> contentRows, string outPutFile)
    {
        bool b = false;
        try
        {
            StringBuilder sbulider = new StringBuilder();
            string title = "\"编号\",\"标题\",\"帖子链接\",\"作者\",\"媒体名\",\"日期\",\"调性\",\"标签\"\r\n";
            sbulider.Append(title);
            foreach (ShowDataInfo item in contentRows)
            {
                sbulider.Append(string.Format(csv_row, item.Id, GetFormatCsv(item.Title), GetFormatCsv(item.SrcUrl), "null", GetFormatCsv(item.SiteName), item.Time, GetDecodeAnalysis(item.Analysis), GetFormatCsv(item.Tag)));
            }
            File.WriteAllText(outPutFile, sbulider.ToString(), Encoding.GetEncoding("gb2312"));
            b = true;
        }
        catch (Exception ex)
        {
            LogNet.LogBLL.Error("Export_HankookCsv", ex);
        }
        return b;
    }


    #region  Tool Method 方法

    public static string GetFormatTagCell(int tagCount, params string[] tagArr)
    {
        StringBuilder sbulider = new StringBuilder();
        if (tagCount > 0)
        {
            for (int i = 0; i < tagCount; i++)
            {
                sbulider.Append(Temp_TagCell.Replace("#temp_tagCell", tagArr[i]));
            }
        }

        return sbulider.ToString();
    }


    public static Func<string[], string> GetFormatTagTitle = (arr) =>
    {
        StringBuilder sbulider = new StringBuilder();
        for (int i = 0; i < arr.Length; i++)
        {
            sbulider.Append(Temp_TagTitle.Replace("#temp_tagTitle", arr[i]));
        }
        return sbulider.ToString();
    };


    public static Func<string, string> GetFormatXml = (oldXmlStr) =>
    {
        return string.IsNullOrEmpty(oldXmlStr) ? "" : oldXmlStr.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("\"", "&quot;").Replace("’", "&apos;");
    };

    public static Func<int, string> GetFormatAnalysis = (analysis) =>
      {
          string result = "";

          if (SiteDataModel.AnalysisMap.ContainsKey(analysis))
          {
              result = SiteDataModel.AnalysisMap[analysis];
          }

          return result;
      };

    public static Func<string, string> GetDecodeAnalysis = (analysisColor) =>
    {
        if (analysisColor == "blue")
            return "正";
        else if (analysisColor == "green")
            return "中";
        else
            return "负";
    };

    public static Func<string, string> GetFormatCsv = (str) => { return str.Replace("\"", "\"\""); };  //把一个",变为 两个 ""

    #endregion
}