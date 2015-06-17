using System;
using System.Collections.Generic;
using System.Web;

namespace WebApplication1.Admin
{
    /// <summary>
    /// Summary description for ExistNameHandler
    /// </summary>
    public class ExistNameHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string username = context.Request["uid"];
            if (CommonNews.Helper.OperateContext.Current.ExistUserName(username))
            {
                context.Response.Write("true");
            }
            else
            {
                context.Response.Write("flase");
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