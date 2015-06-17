using System;

namespace Common
{
    /// <summary>
    /// 配置文件助手
    /// </summary>
    public class ConfigurationHelper
    {
        /// <summary>
        /// 网站根路径
        /// </summary>
        private static string siteroot = System.Web.Hosting.HostingEnvironment.MapPath("~/");

        /// <summary>
        /// 获取配置文件中AppSetting节点的相对路径对应的绝对路径
        /// </summary>
        /// <param name="key">相对路径设置的键值</param>
        /// <returns>绝对路径</returns>
        public static string AppSettingMapPath(string key)
        {
            if (String.IsNullOrEmpty(siteroot))
            {
                siteroot = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            }
            //拼接路径
            string path = siteroot + System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            return path;
        }

        /// <summary>
        /// 将虚拟路径转换为物理路径
        /// </summary>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>虚拟路径对应的物理路径</returns>
        public static string MapPath(string virtualPath)
        {
            if (String.IsNullOrEmpty(siteroot))
            {
                siteroot = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            }
            //拼接路径
            string path = siteroot + virtualPath;
            return path;
        }

        /// <summary>
        /// 获取配置文件中AppSetting节点的值
        /// </summary>
        /// <param name="key">设置的键值</param>
        /// <returns>键值对应的值</returns>
        public static string AppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
        }

        /// <summary>
        /// 获取配置文件中ConnectionStrings节点的值
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>键值对应的连接字符串值</returns>
        public static string ConnectionString(string key)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}
