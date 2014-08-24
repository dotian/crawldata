using System;
using System.Web;
using System.Web.SessionState;

 namespace WebCrawler.Admin.Handler
{
    /// <summary>
    /// BaseHandler 的摘要说明。
    /// </summary>
    public abstract class BaseHandler : IHttpHandler, IReadOnlySessionState
    {
        public abstract void ProcessRequest(HttpContext context);

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }


        protected string GetDecodeQuery(string argEncodedString)
        {
            return HttpUtility.UrlDecode(argEncodedString);
        }

        ///// <summary>
        ///// 提交事务
        ///// </summary>
        ///// <param name="argBLLName">对应的业务类名（命名空间＋类名）</param>
        ///// <param name="argMethodName">方法名</param>
        ///// <param name="argParameters">执行方法所需参数</param>
        ///// <returns></returns>
        //protected object EventSubmit(string argBLLName, string argMethodName, object[] argParameters)
        //{
        //    object result = null;
        //    IBRSInterfaceBLL biz = (IBRSInterfaceBLL)IBRSInvoker.CreateInstance(argBLLName, null);
        //    if (biz != null)
        //    {
        //        result = biz.Execute(argMethodName, argParameters);
        //    }
        //    return result;
        //}

     
        protected bool RequestIsGET()
        {
            return System.Web.HttpContext.Current.Request.RequestType.ToUpper() == "GET";
        }

    }
}
