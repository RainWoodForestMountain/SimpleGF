using System.Security.Cryptography;

namespace GameFramework
{
    public static class Cryptography
    {
        public static byte[] Encrypt(byte[] _original, byte[] _key)
        {
            TripleDESCryptoServiceProvider _des = new TripleDESCryptoServiceProvider();
            _des.Key = _key;
            _des.Mode = CipherMode.ECB;

            return _des.CreateEncryptor().TransformFinalBlock(_original, 0, _original.Length);
        }
        /// <summary>
        /// 使用给定密钥字符串加密string
        /// </summary>
        /// <param name="original">原始文字</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] _original, string _key)
        {
            byte[] _kb = System.Text.Encoding.ASCII.GetBytes(_key);
            return Encrypt(_original, _kb);
        }

        /// <summary>
        /// 使用给定密钥解密数据
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] _encrypted, byte[] _key)
        {
            TripleDESCryptoServiceProvider _des = new TripleDESCryptoServiceProvider();
            _des.Key = _key;
            _des.Mode = CipherMode.ECB;

            return _des.CreateDecryptor().TransformFinalBlock(_encrypted, 0, _encrypted.Length);
        }
        /// <summary>
        /// 使用给定密钥字符串解密string,返回指定编码方式明文
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] _encrypted, string _key)
        {
            byte[] _kb = System.Text.Encoding.ASCII.GetBytes(_key);
            return Decrypt(_encrypted, _kb);
        }
    }
}