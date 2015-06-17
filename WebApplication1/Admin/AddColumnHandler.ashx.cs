using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// Summary description for AddColumnHandler
    /// </summary>
    public class AddColumnHandler : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int pId = 0;
            int.TryParse(context.Request["parentId"], out pId);
            string cName = context.Request["name"];
            Models.Category c = new Models.Category() { CategoryName = cName, ParentId = pId };
            try
            {
                CommonNews.Helper.OperateContext.Current.AddNewsType(c);
                context.Response.Write("true");
            }
            catch (Exception)
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