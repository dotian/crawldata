using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ConGetData.Model
{
    public class XmlTemplate
    {
        public XmlTemplate() { }
        public XmlTemplate(string xmlText)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlText);
                XmlNode root = xmlDoc.SelectSingleNode("template");//查找<template> 

                Node = GetRegex(root, "Node");
                Title = GetRegex(root, "Title");
                SrcUrl = GetRegex(root, "SrcUrl");
                SiteEncoding = GetRegex(root, "SiteEncoding");
                InnerEncoding = GetRegex(root, "InnerEncoding");
                InnerContent = GetRegex(root, "Content");
                Layer = GetRegex(root, "Layer");
                // AuthorRegex = GetRegex(root, "Author");
                InnerDate = GetRegex(root, "ContentDate");
                SiteName = GetRegex(root, "SiteName");

                PageStart = GetPageNum(root, "PageStart");
                PageEnd = GetPageNum(root, "PageEnd");
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("XmlTemplate", ex);
            }
          
          
        }
        private static string GetRegex(XmlNode root, string nodeName)
        {
            try
            {
                return root.SelectSingleNode(nodeName).InnerText.Trim();
            }
            catch
            {

                return "";
            }

        }

        private static int GetPageNum(XmlNode root, string nodeName)
        {

            if (root.InnerXml.Contains(nodeName))
            {
                return int.Parse(root.SelectSingleNode(nodeName).InnerText.Trim());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 模块
        /// </summary>
        public string Node { get; private set; }

        /// <summary>
        /// 站点编码
        /// </summary>
        public string SiteEncoding { get; private set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Url
        /// </summary>
        public string SrcUrl { get; private set; }



        /// <summary>
        /// 内容编码
        /// </summary>
        public string InnerEncoding { get; private set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string InnerContent { get; private set; }

        /// <summary>
        /// 页面层级
        /// </summary>
        public string Layer { get; private set; }

        /// <summary>
        /// 内容时间
        /// </summary>
        public string InnerDate { get; private set; }

        public string SiteName { get; private set; }

        /// <summary>
        /// 开始页
        /// </summary>
        public int PageStart { get; private set; }
        /// <summary>
        /// 结束页
        /// </summary>
        public int PageEnd { get; private set; }
    }
}
