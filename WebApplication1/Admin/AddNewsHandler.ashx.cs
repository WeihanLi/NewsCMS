using System;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// Summary description for AddNewsHandler
    /// </summary>
    public class AddNewsHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //判断Session，验证权限
            if (context.Session["User"] != null)
            {
                string title = context.Request["newsTitle"], content = context.Request["newsContent"], type = context.Request["newsType"], imgPath = context.Request["imagePath"], attachPath = context.Request["attachPath"],externalLink = context.Request["externalLink"], publishDate = context.Request["publishDate"];
                int nType = -1;
                int.TryParse(type, out nType);
                //验证数据合法性
                if (nType<=0||String.IsNullOrEmpty(title))
                {
                    return;
                }
                Models.News n = new Models.News() { NewsTitle =title,NewsContent = Common.ConverterHelper.GetContent(content),NewsTypeId =nType,NewsImagePath= Common.ConverterHelper.GetContent(imgPath),NewsAttachPath= Common.ConverterHelper.GetContent(attachPath),NewsExternalLink = Common.ConverterHelper.GetContent(externalLink),PublishTime = String.IsNullOrEmpty(publishDate)?DateTime.Now:Convert.ToDateTime(publishDate) };
                if (CommonNews.Helper.OperateContext.Current.AddNews(n))
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
                context.Response.Write("no permission");
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