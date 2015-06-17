using System;
using System.Web;

namespace WebApplication1.Handlers
{
    /// <summary>
    /// Summary description for loadVisitCountHandler
    /// </summary>
    public class loadVisitCountHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int newsId = -1;
            int.TryParse(context.Request["id"], out newsId);
            if (newsId<=0)
            {
                context.Response.Write("error");
            }
            try
            {
                int cnt = CommonNews.Helper.OperateContext.Current.GetNewsVisitCount(newsId);
                context.Response.Write(cnt);
            }
            catch (Exception ex)
            {
                new Common.LogHelper(typeof(loadVisitCountHandler)).Error(ex);
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