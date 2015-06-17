using System;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// Summary description for LogOutHandler
    /// </summary>
    public class LogOutHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                context.Session.Abandon();
                context.Response.Write("true");
            }
            catch (Exception ex)
            {
                new Common.LogHelper(typeof(LogOutHandler)).Error(ex);
                context.Response.Write("false");
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}