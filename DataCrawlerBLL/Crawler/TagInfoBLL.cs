using System.Collections.Generic;

using System.Text;
using System.Data;
using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;
using System;
using System.Data.Linq;
namespace DataCrawler.BLL.Crawler
{
    public class TagInfoBLL
    {
       
        //public List<TagInfo> GetTagInfoManager(string searchKey, int pageSize, int pageIndex)
        //{
        //    ServerDaLAction sqlDalAction = new ServerDaLAction();
        //    List<TagInfo> list = sqlDalAction.GetTagInfoService(searchKey, pageSize, pageIndex);
        //    foreach (TagInfo item in list)
        //    {
        //        /*#xiang 这里要进行韩文转码*/
        //        item.KoreanTranslate = item.KoreanTranslate;//这里要进行韩文转码
        //    }
        //    return list;
        //}

        /// <summary>
        /// 得到 该项目 可以使用的标签
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<TagList> GetBatchTagByProjectIdManager(int projectId)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            List<ProjectTagRelation> list = sqlDalAction.GetProjectTagRelationByProjectIdService(projectId);
            //对韩语 进行解析
            List<TagList> tagList = new List<TagList>();
            foreach (ProjectTagRelation item in list)
            {
                tagList.Add(item.TagList);
            }
            return tagList;
        }

        public int GetTagDataCountManager(string searchKey)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.GetTagDataCountService(searchKey);
        }

        /// <summary>
        /// 删除单个标签,与该标签相关的项目 的标签,也同时删除
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public int DeleteTagByIdManager(int tagId)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.DeleteTagByIdService(tagId);

        }

        public int InsertTagInfoManager(string tagName, string secondTag, string koreanTranslate)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            /*#xiang koreanTranslate 韩语翻译要做转码处理*/
            koreanTranslate = koreanTranslate + "";
            return sqlDalAction.InsertTagInfoService(tagName, secondTag, koreanTranslate);
        }

        public int UpdateTagInfoByTagIdManager(int tagId, string tagName, string secondTag, string koreanTranslate)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            /*#xiang koreanTranslate 韩语翻译要做转码处理*/
            koreanTranslate = koreanTranslate + "";
            return sqlDalAction.UpdateTagInfoByTagIdService(tagId, tagName, secondTag, koreanTranslate);
        }

        public List<ProjectTagRelation> GetProjectByRunStatusManager()
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.GetProjectByRunStatusService();
        }


        public string GetProjectTagRelationByProjectIdManager(int projectId)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();

            StringBuilder sbTagIds = new StringBuilder();
            sbTagIds.Append(","); sbTagIds.Append("-1");//先给一个默认值 -1,保证 sbTagIds 长度不为0
            List<ProjectTagRelation> list = sqlDalAction.GetProjectTagRelationByProjectIdService(projectId);
            foreach (ProjectTagRelation item in list)
            {
                sbTagIds.Append(",");
                sbTagIds.Append(item.TagList.Id);
            }

            return sbTagIds.ToString();
        }

        /// <summary>
        /// 给项目添加 该项目不存在的标签
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        public int AllotProjectTagManager(int projectId, string tagIds)
        {
            
            ServerDaLAction sqlDalAction = new ServerDaLAction();
           
            //得到 当前的 所有的 tag
            //tagIds ,1,2,3,4,5,6,7,8,9
            int tag1stCount = sqlDalAction.CkeckTag1StCountService(tagIds.Substring(1));
            if (tag1stCount>6)
            {
                //一级标签 已经超过了6种
                return -1;
            }

            int result = 0;
            List<ProjectTagRelation> list = sqlDalAction.GetProjectTagRelationByProjectIdService(projectId);
            List<string> TagList = new List<string>();
            string[] array = tagIds.Split(',');
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i]!="")
                {
                    TagList.Add(array[i]);
                }   
            }
            //最后剩下的,是新增的

            List<ProjectTagRelation> list_copy = new List<ProjectTagRelation>();
            List<string> TagList_copy = new List<string>() ;

            for (int i = 0; i < list.Count; i++)
            {
                list_copy.Add(list[i]);
            }
            for (int i = 0; i < TagList.Count; i++)
            {
                TagList_copy.Add(TagList[i]);
            }

             


            try
            {
                foreach (ProjectTagRelation item in list)
                {
                    for (int i = 0; i < TagList.Count; i++)
                    {
                        if (item.TagList.Id.ToString() == TagList[i])
                        {
                            TagList_copy.Remove(TagList[i]);
                            list_copy.Remove(item); //只能备份一份,进行去除. 还在 迭代的 list,进行 remove 会有运行时异常
                            break;
                        }
                    }
                }

                
              


                if (TagList_copy.Count > 0)
                {
                    result += sqlDalAction.AllotProjectTagService(projectId, TagList_copy);
                }
                if (list_copy.Count > 0)
                {
                    //这些是要删除的
                    for (int i = 0; i < list_copy.Count; i++)
                    {
                        result += sqlDalAction.DeleteProjetTagRelationByProjectIdService(projectId, list_copy[i].TagList.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("AllotProjectTagManager",ex);

               
            }
           
            return result;
        }


        /// <summary>
        /// 删除单个项目的单个标签 ,标签还在,只是 与项目没有关系
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public int DeleteProjetTagRelationByProjectIdManager(int projectId, int tagId)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.DeleteProjetTagRelationByProjectIdService(projectId, tagId);
        }

        public int DoBatchTagManager(int pid, string sd_ids, int tagId)
        {
            //pid =1001
            //sd_ids = ,6762,6763,6764,6765
            //tags = 25,26

            //一个项目,要打最多打2个tag

            int result = 0;
            ServerDaLAction sqlDalAction = new ServerDaLAction();

        
            //#xiang

            string[]array = sd_ids.ToString().Substring(1).Split(',');
            List<string> sd_idList = new List<string>();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i]!="")
                {
                    sd_idList.Add(array[i]);
                }
            }
            foreach (string sd_id in sd_idList)
            {
                //第一个标签
                result += sqlDalAction.DoBatchTagService(pid, int.Parse(sd_id), tagId);

            }

            return result;
        }
        public void Test()
        {

            StringBuilder sbTagIds = new StringBuilder();
            sbTagIds.Append(","); sbTagIds.Append(1);
            sbTagIds.Append(","); sbTagIds.Append(2);
            sbTagIds.Append(","); sbTagIds.Append(3);
           // Console.WriteLine(sbTagIds.ToString());
            string res = sbTagIds.ToString().Substring(1); ;
            //Console.WriteLine(res);

        }
        public void Test1()
        {
            string mess = Convert.ToString(242, 16);//d6
            Console.Write(mess);
            mess = Convert.ToString(249, 16);//d6
            Console.Write(mess);
            mess = Convert.ToString(252, 16);//d6
            Console.Write(mess);
        }

        public void test2()
        {
            string mes = ",1,1,1,1,1";
            Console.WriteLine(mes.Substring(1));
        
        }
    }
}
