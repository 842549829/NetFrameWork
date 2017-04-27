using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Common.Extension
{
    /// <summary>
    /// 序列化
    /// </summary>
    public static class SerializerExtension
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON字符串</returns>
        public static string SerializeObject(this object obj)
        {
            try
            {
                var jsonSerializerSettings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
                return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>对象</returns>
        public static T DeserializeObject<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns>对象</returns>
        public static dynamic DeserializeObject(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject(json);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// XML序列化方式深复制
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>复制对象</returns>
        public static T DeepCopy<T>(this T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = xml.Deserialize(ms);
                ms.Close();
            }

            return (T)retval;
        }
    }
}