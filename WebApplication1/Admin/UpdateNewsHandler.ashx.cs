using System;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// UpdateNewsHandler
    /// </summary>
    public class UpdateNewsHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //判断Session，验证权限
            if (context.Session["User"] != null)
            {
                int id = -1,typeId=-1;
                int.TryParse(context.Request["id"],out id);
                string nid=context.Request["newsId"],title = context.Request["newsTitle"], content = context.Request["newsContent"], type = context.Request["newsType"], imgPath = context.Request["imagePath"], attachPath = context.Request["attachPath"], externalLink = context.Request["externalLink"], publishDate = context.Request["publishDate"],newsPath = context.Request["newsPath"];
                int.TryParse(nid, out id);
                int.TryParse(type, out typeId);
                //验证数据合法性
                if (id<=0||typeId<=0||String.IsNullOrEmpty(title))
                {
                    return;
                }
                try
                {
                        Models.News n = new Models.News() { NewsId=id,NewsTitle = title, NewsContent = Common.ConverterHelper.GetContent(content), NewsTypeId = typeId, NewsImagePath = Common.ConverterHelper.GetContent(imgPath), NewsAttachPath = Common.ConverterHelper.GetContent(attachPath), NewsExternalLink = Common.ConverterHelper.GetContent(externalLink), PublishTime = String.IsNullOrEmpty(publishDate)?DateTime.Now:Convert.ToDateTime(publishDate),NewsPath=newsPath };
                        if (CommonNews.Helper.OperateContext.Current.UpdateNews(n))
                        {
                            context.Response.Write("true");
                        }
                        else
                        {
                            context.Response.Write("false");
                        }
                }
                catch (Exception ex)
                {
                    new Common.LogHelper(typeof(UpdateNewsHandler)).Error(ex);
                    context.Response.Write("false");
                }
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