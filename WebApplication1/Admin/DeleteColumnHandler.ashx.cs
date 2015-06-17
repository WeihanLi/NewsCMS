using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// DeleteNewsType
    /// </summary>
    public class DeleteColumnHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //判断Session，验证权限
            if (context.Session["User"] != null)
            {
                int id = -1;
                int.TryParse(context.Request["id"], out id);
                if (id > 0)
                {
                    if (CommonNews.Helper.OperateContext.Current.DelNewsType(id))
                    {
                        context.Response.Write("true");
                        context.Response.End();
                    }
                }
                context.Response.Write("false");
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