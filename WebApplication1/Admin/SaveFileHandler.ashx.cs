using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// SaveFileHandler
    /// </summary>
    public class SaveFileHandler : IHttpHandler,IRequiresSessionState
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
            string type = context.Request["type"], fileContents = context.Request["contents"],fName=context.Request["fName"];
            int t = int.Parse(type);
            if (CommonNews.Helper.OperateContext.Current.SaveFile(fileContents,(Common.FileType)t,fName))
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