using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// Summary description for DisableUserHandler
    /// </summary>
    public class DisableUserHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Session[CommonNews.Helper.OperateContext.AdminSESSION]==null)
            {
                context.Response.Write("no permission");
                context.Response.End();
                return;
            }
            int uid = -1;
            int.TryParse(context.Request["uid"], out uid);
            if (uid<=0)
            {
                context.Response.Write("error");
                context.Response.End();
                return;
            }
            if (CommonNews.Helper.OperateContext.Current.DisableUser(uid))
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