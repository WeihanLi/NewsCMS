using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CommonNews.Helper
{
    class StaticHelper
    {
        /// <summary>
        /// 日志助手
        /// </summary>
        static Common.LogHelper logger = new Common.LogHelper(typeof(StaticHelper));
        //Todo:多线程，调用另外一个线程来完成静态化的工作

        /// <summary>
        /// 生成静态化内容
        /// </summary>
        /// <param name="templatePath">模板位置（物理（绝对）路径）</param>
        /// <param name="dicStringsToReplace">要替换的字符串字典词库</param>
        /// <param name="destPath">要保存的位置</param>
        public static void GenerateStaticContent(string templatePath, Dictionary<string, string> dicStringsToReplace,string destPath)
        {
            try
            {
                string dir = Path.GetDirectoryName(destPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                //创建静态文件
                using (TextWriter writer = File.CreateText(destPath))
                {
                    writer.Write(GetStringFromTemplate(templatePath, dicStringsToReplace));
                }
            }
            catch (SystemException ex)
            {
                logger.Error(ex);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// 生成静态化内容
        /// </summary>
        /// <param name="url">网址页面url</param>
        /// <param name="destPath">要保存文件的路径（FullPath）</param>
        public static void GenerateStaticContent(string url, string destPath)
        {
            try
            {
                string dir = Path.GetDirectoryName(destPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                //创建静态文件
                using (TextWriter writer = File.CreateText(destPath))
                {
                    writer.Write(GetStringByUrlViaWebClient(url));
                }
            }
            catch (SystemException ex)
            {
                logger.Error(ex);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// 通过WebRequest来获取网页代码
        /// </summary>
        /// <param name="url">要请求的网址url</param>
        /// <returns>网页html代码</returns>
        private static string GetStringByUrlViaWebRequest(string url)
        {
            string strResult = string.Empty;
            Uri u = new Uri(url);
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(u);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
                strResult = streamReader.ReadToEnd();
            }
            catch(Exception ex)
            {
                //记录日志
                logger.Error(ex);
                return "error";
            }
            return strResult;
        }

        /// <summary>
        /// 通过WebClient获取（网址）页面的html代码
        /// </summary>
        /// <param name="url">网址</param>
        /// <returns>页面的html代码</returns>
        private static string GetStringByUrlViaWebClient(string url)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            return client.DownloadString(url);
        }

        /// <summary>
        /// 从模板中获取网页html
        /// </summary>
        /// <param name="templatePath">模板页路径（绝对路径）</param>
        /// <param name="dicStringsToReplace">要替换的字符串词典</param>
        /// <returns>基于模板页面生成的网页html</returns>
        private static string GetStringFromTemplate(string templatePath, Dictionary<string, string> dicStringsToReplace)
        {
            StringBuilder sbTemp = new StringBuilder(System.IO.File.ReadAllText(templatePath));
            //替换
            foreach (string item in dicStringsToReplace.Keys)
            {
                sbTemp.Replace(item, dicStringsToReplace[item]);
            }
            return sbTemp.ToString();
        }
    }
}
