using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using System.Text;
using Common.Extension;

namespace Common.Utility
{
    /// <summary>
    /// 调用WebApi代理类
    /// 4.5 POST GET JSON 数据
    /// </summary>
    public class HttpClientUtility
    {
        #region 类型转化
        /// <summary>
        /// 将键值对转化成实体对象
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="dic">键值对</param>
        /// <returns>实体对象</returns>
        public T Assign<T>(Dictionary<string, string> dic) where T : new()
        {
            Type t = typeof(T);
            T entity = new T();
            var fields = t.GetProperties();

            object obj = null;
            foreach (var field in fields)
            {
                if (!dic.Keys.Contains(field.Name))
                {
                    continue;
                }
                var val = dic[field.Name];
                //非泛型
                if (!field.PropertyType.IsGenericType)
                {
                    obj = string.IsNullOrEmpty(val) ? null : Convert.ChangeType(val, field.PropertyType);
                }
                else //泛型Nullable<>
                {
                    Type genericTypeDefinition = field.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                    {
                        obj = string.IsNullOrEmpty(val)
                                  ? null
                                  : Convert.ChangeType(val, Nullable.GetUnderlyingType(field.PropertyType));
                    }
                }
                field.SetValue(entity, obj, null);
            }


            return entity;
        }

        /// <summary>
        /// 将对象属性转换为key-value对
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToMap(object o)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            Type t = o.GetType();
            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();
                if (mi != null && mi.IsPublic)
                {
                    //map.Add(p.Name, mi.Invoke(o, new object[] { }));

                }

                map.Add(p.Name, p.GetValue(o));
            }

            return map;

        }

        /// <summary>
        /// 将对象属性转换为key-value对
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToMap<T>(T obj)
        {
            Type t = typeof(T);
            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return pi.ToDictionary(p => p.Name, p => p.GetValue(obj).ToString());

        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value">对象</param>
        /// <returns>结果</returns>
        public static string NewtonsoftSerialize(object value)
        {
            return value.SerializeObject();
        }
        #endregion

        #region GET

        /// <summary>
        /// GteParameters
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns>string</returns>
        public static string GteParameters(Dictionary<string, string> parameters)
        {
            if (parameters == null || !parameters.Any())
            {
                return string.Empty;
            }

            return parameters.Join("&", p => p.Key + "=" + p.Value);
        }

        /// <summary>
        /// 异步Get(Dictionary)
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="parameters">parameters</param>
        /// <returns>结果</returns>
        public static async Task<string> Get(string url, Dictionary<string, string> parameters)
        {
            // 设置HttpClientHandler的AutomaticDecompression
            ////var handler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip };

            url = url + "?v=1.0" + GteParameters(parameters);

            Uri uri;
            Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri);

            // 创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient())
            {
                // await异步等待回应
                var response = await http.GetAsync(uri);

                // await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                var rel = await response.Content.ReadAsStringAsync();

                // 确保HTTP成功状态值
                response.EnsureSuccessStatusCode();

                return rel;
            }
        }

        /// <summary>
        /// 异步Get(string)
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="url">url</param>
        /// <param name="t">t</param>
        /// <returns>结果</returns>
        public static string GetString<T>(string url, T t)
        {
            var map = ToMap(t);
            Task<string> result = Get(url, map);
            return result.Result;
        }

        /// <summary>
        /// 异步Get(T)
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <typeparam name="V">V</typeparam>
        /// <param name="url">url</param>
        /// <param name="t">T</param>
        /// <returns>v</returns>
        public static V GetModel<T, V>(string url, T t)
        {
            var map = ToMap(t);
            var rel = GetString(url, map);
            return rel.DeserializeObject<V>();
        }
        #endregion

        #region POST

        #region FormUrl(适用于自定义接口)
        /// <summary>
        /// 异步Post(Dictionary)
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="parameters">parameters</param>
        /// <returns>结果</returns>
        public static async Task<string> PostByFormUrl(string url, Dictionary<string, string> parameters)
        {

            // 设置HttpClientHandler的AutomaticDecompression
            ////var handler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip };

            Uri uri;
            Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri);

            // 创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient())
            {
                ////设置要的数据格式
                ////http.DefaultRequestHeaders.Add("Accept", "application/xml");
                http.DefaultRequestHeaders.Add("Accept", "application/json");

                // 使用FormUrlEncodedContent做HttpContent
                var content = new FormUrlEncodedContent(parameters);

                // await异步等待回应
                var response = await http.PostAsync(uri, content);

                // await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                var rel = await response.Content.ReadAsStringAsync();

                // 确保HTTP成功状态值
                response.EnsureSuccessStatusCode();

                return rel;
            }
        }

        /// <summary>
        /// 异步Post(string)
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="url">url</param>
        /// <param name="t">t</param>
        /// <returns>结果</returns>
        public static string PostStringByFormUrl<T>(string url, T t)
        {
            var map = ToMap(t);
            Task<string> result = PostByFormUrl(url, map);
            return result.Result;
        }

        /// <summary>
        /// 异步Post(T)
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <typeparam name="V">V</typeparam>
        /// <param name="url">url</param>
        /// <param name="t">t</param>
        /// <returns>V</returns>
        public static V PostModelByFormUrl<T, V>(string url, T t)
        {
            var map = ToMap(t);
            var rel = PostStringByFormUrl(url, map);
            return rel.DeserializeObject<V>();
        }
        #endregion

        #region Byte(适用于调用第三接口)
        /// <summary>
        /// 异步Post(byte[])
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="bytes">bytes</param>
        /// <returns>结果</returns>
        public static async Task<string> PostByByte(string url, byte[] bytes)
        {

            // 设置HttpClientHandler的AutomaticDecompression
            ////var handler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip };

            Uri uri;
            Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri);

            // 创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient())
            {
                ////设置要的数据格式
                ////http.DefaultRequestHeaders.Add("Accept", "application/xml");
                http.DefaultRequestHeaders.Add("Accept", "application/json");

                // 使用FormUrlEncodedContent做HttpContent
                var content = new ByteArrayContent(bytes);

                // await异步等待回应
                var response = await http.PostAsync(uri, content);

                // await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                var rel = await response.Content.ReadAsStringAsync();

                // 确保HTTP成功状态值
                response.EnsureSuccessStatusCode();

                return rel;
            }
        }

        /// <summary>
        /// 异步Post(string,byte[])
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="url">url</param>
        /// <param name="t">t</param>
        /// <returns>结果</returns>
        public static string PostStringByByte<T>(string url, T t)
        {
            var data = NewtonsoftSerialize(t);
            byte[] bytes = Encoding.GetEncoding(0x6faf).GetBytes(data);
            Task<string> result = PostByByte(url, bytes);
            return result.Result;
        }

        /// <summary>
        /// 异步Post(T,byte[])
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <typeparam name="V">V</typeparam>
        /// <param name="url">url</param>
        /// <param name="t">t</param>
        /// <returns>V</returns>
        public static V PostModelByByte<T, V>(string url, T t)
        {
            var rel = PostStringByByte(url, t);
            return rel.DeserializeObject<V>();
        } 
        #endregion
        #endregion
    }

    /// <summary>
    /// 二进制数组
    /// </summary>
    public class MyByteArrayContent : HttpContent
    {
        /// <summary>
        /// 数据
        /// </summary>
        private readonly byte[] content;

        /// <summary>
        /// 长度
        /// </summary>
        private readonly int count;

        /// <summary>
        /// 偏移量
        /// </summary>
        private readonly int offset;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="content">数据</param>
        public MyByteArrayContent(byte[] content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            this.content = content;
            this.offset = 0;
            this.count = content.Length;
        }

        /// <summary>
        /// Serialize the HTTP content to a memory stream as an asynchronous operation.
        /// </summary>
        /// <returns>Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous</returns>
        protected override Task<Stream> CreateContentReadStreamAsync()
        {
            return Task.FromResult<Stream>(new MemoryStream(this.content, this.offset, this.count, false, false));
        }

        /// <summary>
        /// Determines whether the HTTP content has a valid length in bytes.
        /// </summary>
        /// <param name="length">The length in bytes of the HTTP content.</param>
        /// <returns>Returns System.Boolean.true if length is a valid length; otherwise, false.</returns>
        protected override bool TryComputeLength(out long length)
        {
            length = this.count;
            return true;
        }

        /// <summary>
        ///  Serialize the HTTP content to a stream as an asynchronous operation.
        /// </summary>
        /// <param name="stream">The target stream.</param>
        /// <param name="context">Information about the transport (channel binding token, for example). This parameter may be null.</param>
        /// <returns>Returns System.Threading.Tasks.Task.The task object representing the asynchronous</returns>
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, this.content, this.offset, this.count, null);
        }
    }
}