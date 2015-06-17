using System;
using System.Web;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {
        static Common.LogHelper logger = new Common.LogHelper(typeof(Global));

        protected void Application_Start(object sender, EventArgs e)
        {
            //日志初始化
            Common.LogHelper.LogInit();
            //
            logger.Debug("log4net启动");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            #region Url重写，网站伪静态化

            HttpApplication app = sender as HttpApplication;
            //获取请求路径
            string url = app.Context.Request.Path;
            if (url.Contains("-"))
            {
                //获取文件名
                string preUrl = url.Substring(0, url.LastIndexOf('-'));
                //获取请求参数
                string id = url.Substring(url.LastIndexOf('-') + 1, url.LastIndexOf('.') - url.LastIndexOf('-') - 1);
                //构建实际文件路径
                string newPath = preUrl + ".aspx?id=" + id;
                //重写Url请求
                app.Context.RewritePath(newPath);
            }

            #endregion
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            logger.Error(app.Server.GetLastError());
        }
    }
}