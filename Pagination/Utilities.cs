using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace Pagination
{
    public static class Utilities
    {
        /// <summary>
        /// Object To String Extension
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Stringify(this object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Deserialize Object To Defined Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T MapObject<T>(this object item)
        {
            T sr = default(T);
            if (item != null)
            {
                var obj = JsonConvert.SerializeObject(item);
                sr = JsonConvert.DeserializeObject<T>(obj);
            }
            return sr;
        }
        /// <summary>
        /// Deserialize List Object To Defined List Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<T> MapObjects<T>(this object item)
        {
            List<T> sr = default(List<T>);
            if (item != null)
            {
                var obj = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                sr = JsonConvert.DeserializeObject<List<T>>(obj);
            }
            return sr;
        }
        /// <summary>
        /// Base64 To Bytes Extension
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static byte[] FromBase64ToBytes(this string item)
        {
            try
            {
                var convertedByte = Convert.FromBase64String(item);
                return convertedByte;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Bytes To String Extension
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FromBytesToString(this byte[] bytes)
        {
            if (bytes == null)
                return null;
            else
                return System.Text.Encoding.UTF8.GetString(bytes);
        }
        /// <summary>
        /// Convert String To Byte Array
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
        /// <summary>
        /// Get API Consumer IP Address
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            return string.IsNullOrEmpty(HttpContext.Current.Request.UserHostAddress) ? "::1" : HttpContext.Current.Request.UserHostAddress;
        }
        public static string EncryptString(string text)
        {
            var encText = string.Empty;
            Random rand = new Random();
            encText = Convert.ToBase64String(rand.NextDouble().ToString().ToBytes());
            return encText;
        }
        public static string LogEncryption(this string request)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            var data = Json.Decode(request, dictionary.GetType());
            var dict = data as Dictionary<string, string>;
            var keysToEncrypt = (from dic in dict
                                 where dic.Key.ToLower().Contains("password")
                                 select dic.Key);
            foreach (var item in keysToEncrypt.ToList())
            {
                dict[item] = EncryptString(dict[item]);
            }
            return JsonConvert.SerializeObject(data);
        }
        public static string GetBrowserInfo()
        {
            string browserDetails = string.Empty;
            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            browserDetails =
            "Name = " + browser.Browser + ","
            + "Version = " + browser.Version + ","
            + "Platform = " + browser.Platform;
            return browserDetails;
        }
        public static T GetSessionValue<T>(string SessionKey)
        {
            HttpContext context = HttpContext.Current;
            T t = default(T);
            if (context != null)
            {
                var Session = context.Session;
                if (Session != null)
                {
                    if (Session[SessionKey] != null)
                    {
                        t = (T)Session[SessionKey];
                    }
                }
            }
            return t;
        }
        
    }

}