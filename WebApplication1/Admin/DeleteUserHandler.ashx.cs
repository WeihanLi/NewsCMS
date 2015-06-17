using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// Summary description for DeleteUserHandler
    /// </summary>
    public class DeleteUserHandler : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //判断Session，验证权限
            if (context.Session["User"] != null&&context.Session["Admin"] != null)
            {
                int id = -1;
                int.TryParse(context.Request["id"], out id);
                if (id > 0)
                {
                    if (CommonNews.Helper.OperateContext.Current.DeleteUser(id))
                    {
                        context.Response.Write("true");
                    }
                    else
                    {
                        context.Response.Write("false");
                    }
                }
                else
                {
                    context.Response.Write("parameters error!");
                }
            }
            else
            {
                context.Response.Write("no permission！");
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