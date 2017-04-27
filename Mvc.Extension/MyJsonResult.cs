using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Common.Extension;

namespace Mvc.Extension
{
    /// <summary>
    /// json
    /// </summary>
    public class MyJsonResult : ActionResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyJsonResult()
        {
            this.ContentEncoding = Encoding.UTF8;
            this.ContentType = "application/json";
        }

        /// <summary>
        /// 重写ExecuteResult
        /// </summary>
        /// <param name="context">ControllerContext</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding == null)
            {
                this.ContentEncoding = Encoding.UTF8;
            }
            response.ContentEncoding = this.ContentEncoding;
            if (this.Data != null)
            {
                var data = NewtonsoftSerialize(this.Data);
                byte[] bytes = this.ContentEncoding.GetBytes(data);
                response.BufferOutput = true;
                response.AddHeader("Content-Length", bytes.Length.ToString());
                response.BinaryWrite(bytes);
                response.Flush();
                response.End();
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        public Encoding ContentEncoding { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value">对象</param>
        /// <returns>结果</returns>
        private static string NewtonsoftSerialize(object value)
        {
            return value.SerializeObject();
        }
    }
}