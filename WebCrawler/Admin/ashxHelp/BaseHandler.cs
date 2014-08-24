using System;
using System.Web;
using System.Web.SessionState;


namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    ///  后台页面 ashx 封装的处理的基类
    /// </summary>
    public abstract class BaseHandler : IHttpHandler, IRequiresSessionState
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

        protected bool checkAccess(bool NeedRedirect)
        {
            if (NeedRedirect)
            {
                System.Web.HttpContext.Current.Response.Redirect(@"~/Index.aspx");
            }
            return true;
        }

        protected bool RequestIsGET()
        {
            return System.Web.HttpContext.Current.Request.RequestType.ToUpper() == "GET";
        }

    }
}
