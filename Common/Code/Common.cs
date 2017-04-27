using System;
using System.Security.Cryptography;

namespace Common.Code
{
    /// <summary>
    /// Common
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 随机种子
        /// </summary>
        /// <returns>结构</returns>
        public static int CreateRandomSeed()
        {
            byte[] bytes = new byte[4];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}