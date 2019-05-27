using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace lab5
{
    public static class SessionExtention
    {
        public static void SetObject(this ISession session, string key, object obj)
        {
            session.SetString(key, JsonConvert.SerializeObject(obj));
        }

        public static P GetObject<P>(this ISession session, string key)
        {
            string json = session.GetString(key);
            if (json != null)
            {
                return JsonConvert.DeserializeObject<P>(json);
            }

            return default(P);
        }

        public static object GetObject(this ISession session, string key)
        {
            string json = session.GetString(key);
            if (json != null)
            {
                return JsonConvert.DeserializeObject(json);
            }

            return null;
        }
    }
}
