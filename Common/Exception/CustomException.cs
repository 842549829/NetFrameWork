using System;

namespace Common.Exception
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    [Serializable]
    public class CustomException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        public CustomException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public CustomException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public CustomException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}