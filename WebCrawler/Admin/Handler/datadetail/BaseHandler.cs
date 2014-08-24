using System;
using System.Web;
using System.Web.SessionState;

 namespace WebCrawler.Admin.Handler
{
    /// <summary>
    /// BaseHandler ��ժҪ˵����
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
        ///// �ύ����
        ///// </summary>
        ///// <param name="argBLLName">��Ӧ��ҵ�������������ռ䣫������</param>
        ///// <param name="argMethodName">������</param>
        ///// <param name="argParameters">ִ�з����������</param>
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
