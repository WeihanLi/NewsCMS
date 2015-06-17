using System;
using System.Web;

namespace WebApplication1.Handlers
{
    /// <summary>
    /// Summary description for getAllTypesTreeHandler
    /// </summary>
    public class getAllTypesTreeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                //转换为tree模型
                Models.FormatModel.TreeModel tree = CommonNews.Helper.OperateContext.Current.LoadAllTypesTreeNode();
                //转换为json格式数据输出
                string jsonData = Common.ConverterHelper.ObjectToJson(tree);
                context.Response.Write(jsonData);
            }
            catch (Exception ex)
            {
                new Common.LogHelper(typeof(getTypesNodeHandler)).Error(ex);
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