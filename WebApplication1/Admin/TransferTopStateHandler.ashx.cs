using System;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// Summary description for TransferTopStateHandler
    /// </summary>
    public class TransferTopStateHandler : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Session["User"]==null)
            {
                context.Response.Write("no permission");
            }
            int id = -1;
            bool topState = false;
            int.TryParse(context.Request["id"], out id);
            topState = Convert.ToBoolean(context.Request["topState"]);
            if (id<=0)
            {
                context.Response.Write("error");
                context.Response.End();
                return;
            }
            Models.News n = new Models.News() { NewsId = id, IsTop = topState };
            if (CommonNews.Helper.OperateContext.Current.TransferTopState(n))
            {
                context.Response.Write("true");
            }
            else
            {
                context.Response.Write("error");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}