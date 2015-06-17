using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// Summary description for RestoreContentHandler
    /// </summary>
    public class RestoreContentHandler : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Session["User"] == null)
            {
                context.Response.Write("no permission");
                context.Response.End();
                return;
            }
            int id = -1;
            int.TryParse(context.Request["id"], out id);
            if (id <= 0)
            {
                context.Response.Write("error");
                context.Response.End();
                return;
            }
            if (CommonNews.Helper.OperateContext.Current.RestoreFromRecycleBin(id))
            {
                context.Response.Write("true");
            }
            else
            {
                context.Response.Write("false");
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