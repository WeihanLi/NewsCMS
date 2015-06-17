using System.Web;

namespace WebApplication1.Admin
{
    /// <summary>
    /// Summary description for SelectNewsByIdHandler
    /// </summary>
    public class SelectNewsByIdHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //Model.FormatModel.JsonMsgModel jsonMsg;
            int id = -1;
            int.TryParse(context.Request["id"],out id);
            if (id<=0)
            {
                //jsonMsg = new Model.FormatModel.JsonMsgModel() { BackupUrl = context.Request.UrlReferrer.ToString(), Msg = "error", Status = Model.FormatModel.JsonMsgStatus.error };
                context.Response.Write("error");
                context.Response.End();
                return;
            }
            try
            {

                Models.News news = CommonNews.Helper.OperateContext.Current.SelectNews(id);
                //jsonMsg = new Model.FormatModel.JsonMsgModel() { BackupUrl = context.Request.UrlReferrer.ToString(), Data = news, Msg = "success", Status = Model.FormatModel.JsonMsgStatus.ok };
                string jsonData = Common.ConverterHelper.ObjectToJson(news);
                context.Response.Write(jsonData);
            }
            catch (System.Exception)
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