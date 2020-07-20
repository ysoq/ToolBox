using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akeem.Web
{
    public static class CommonTools
    {
        private readonly static Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        public static string Obj2Str<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Str2Obj<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static void Log(string message, LogLevel logLevel = default)
        {
            nlog.Log(logLevel == default ? LogLevel.Info : logLevel, message);
        }

        public static void Ex(string message, Exception ex)
        {
            nlog.Log(LogLevel.Error, ex, message);
        }

        public static void Debug(string message, Exception ex = null)
        {
            nlog.Log(LogLevel.Debug, ex, message);
        }
    }
}
