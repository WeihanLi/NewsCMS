using System;
using System.Collections.Generic;
using System.Web;

namespace WebApplication1.Handlers
{
    /// <summary>
    /// LoadNewsHandler
    /// </summary>
    public class LoadNewsHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int typeId = -1, pageIndex = 1, pageSize = 10,rowsCount=0;
            int.TryParse(context.Request["tId"], out typeId);
            int.TryParse(context.Request["pIndex"], out pageIndex);
            int.TryParse(context.Request["pSize"], out pageSize);
            try
            {
                List<Models.News> news = CommonNews.Helper.OperateContext.Current.LoadNewsWithTopped(pageIndex,pageSize,typeId,out rowsCount);
                int pageCount = Convert.ToInt32(Math.Ceiling((rowsCount * 1.0) / pageSize));
                Models.FormatModel.PageListModel<Models.News> pageData = new Models.FormatModel.PageListModel<Models.News>() { Items = news,PageIndex =pageIndex,PageSize=pageSize,RowsCount=rowsCount,PageCount=pageCount};
                string json = Common.ConverterHelper.ObjectToJson(pageData);
                context.Response.Write(json);
            }
            catch (Exception ex)
            {
                new Common.LogHelper(typeof(LoadNewsHandler)).Error(ex);
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