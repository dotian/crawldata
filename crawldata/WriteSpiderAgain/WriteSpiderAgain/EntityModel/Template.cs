using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace WriteSpiderAgain.EntityModel
{
    /// <summary>
    /// 正则模板
    /// </summary>
    public class TemplateModel
    {
        public TemplateModel() { }

        public TemplateModel(string templateContent)
        {
            //请对照 xml文件看
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(templateContent);
            XmlNode root = xmlDoc.SelectSingleNode("template");//查找<template> 

            MainPage = GetRegex(root, "MainPage");
            Node = GetRegex(root, "Node");
            TitleRegex = GetRegex(root, "Title");
            SrcUrlRegex = GetRegex(root, "SrcUrl");
            ContentRegex = GetRegex(root, "Content");
            AuthorRegex = GetRegex(root, "Author");
            ContentDateRegex = GetRegex(root, "ContentDate");
            CreateDateRegex = GetRegex(root, "CreateDate");
            RepliesRegex = GetRegex(root, "Replies");
            ViewsRegex = GetRegex(root, "Views");
        }

        private static string GetRegex(XmlNode root,string nodeName)
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

        public void TestAnalysicXml()
        {
            //测试,解析Xml

            int tid = 1094;
             string xmlPath = "TemplateXMl\\" + tid + ".xml";
             string content = System.IO.File.ReadAllText(xmlPath);
            TemplateModel t = new TemplateModel(content);
            Console.WriteLine(t.TitleRegex);
            Console.WriteLine(t.ContentRegex);
            Console.WriteLine(t.ContentDateRegex);
            Console.WriteLine(t.SrcUrlRegex);
            Trace.WriteLine(t.ViewsRegex);
            Trace.WriteLine("--完毕--");
        }



        private string mainPage;

        public string MainPage
        {
            get { return mainPage; }
            set { mainPage = value; }
        }

        private string node;
        public string Node
        {
            get { return node; }
            set { node = value; }
        }

        private string titleRegex;
        public string TitleRegex
        {
            get { return titleRegex; }
            set { titleRegex = value; }
        }

        private string srcUrlRegex;
        public string SrcUrlRegex
        {
            get { return srcUrlRegex; }
            set { srcUrlRegex = value; }
        }
        private string contentRegex;
        public string ContentRegex
        {
            get { return contentRegex; }
            set { contentRegex = value; }
        }

        private string authorRegex;
        public string AuthorRegex
        {
            get { return authorRegex; }
            set { authorRegex = value; }
        }
        private string contentDateRegex;
        public string ContentDateRegex
        {
            get { return contentDateRegex; }
            set { contentDateRegex = value; }
        }

        private string createDateRegex;
        public string CreateDateRegex
        {
            get { return createDateRegex; }
            set { createDateRegex = value; }
        }

        private string repliesRegex;
        public string RepliesRegex
        {
            get { return repliesRegex; }
            set { repliesRegex = value; }
        }

        private string viewsRegex;
        public string ViewsRegex
        {
            get { return viewsRegex; }
            set { viewsRegex = value; }
        }


    }
}
