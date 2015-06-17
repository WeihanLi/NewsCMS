using log4net;
using System;

namespace Common
{
    /// <summary>
    /// 日志助手
    /// </summary>
    public class LogHelper
    {
        private readonly ILog logger = null;

        public LogHelper(Type t)
        {
            logger = LogManager.GetLogger(t);
        }

        public LogHelper(string name)
        {
            logger = LogManager.GetLogger(name);
        }

        public static void LogInit()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public void Debug(string msg)
        {
            logger.Debug(msg);
        }

        public void Debug(string msg, Exception ex)
        {
            logger.Debug(msg, ex);
        }

        public void Error(string msg)
        {
            logger.Error(msg);
        }

        public void Error(string msg, Exception ex)
        {
            logger.Error(msg, ex);
        }

        public void Warn(string msg)
        {
            logger.Warn(msg);
        }

        public void Warn(string msg, Exception ex)
        {
            logger.Warn(msg, ex);
        }
        
        public void Debug(Exception ex)
        {
            logger.Debug(ex.Message, ex);
        }

        public void Error(Exception ex)
        {
            logger.Error(ex.Message, ex);
        }
    }
}
