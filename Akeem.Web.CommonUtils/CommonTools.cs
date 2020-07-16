using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akeem.Web
{
    public static class CommonTools
    {
        public static string Obj2Str<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Str2Obj<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
