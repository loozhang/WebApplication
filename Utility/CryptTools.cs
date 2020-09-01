using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utility
{
    /// <summary>
    /// 类名称   ：CryptTools
    /// 类说明   ：加解密算法
    /// 作者     ：
    /// 完成日期 ：
    /// </summary>
    public static class CryptTools
    {
        /// <summary>
        /// EC登录密钥字符串
        /// </summary>
        public static string ECLOGIN_PASSWORD_SECRET = "ECLOGIN_PASSWORD_SECRET";

        /// <summary>
        /// 方法说明　：加密方法
        /// 作者    　： 
        /// 完成日期　：
        /// </summary>
        /// <param name="content">需要加密的明文内容</param>
        /// <param name="secret">加密密钥</param>
        /// <returns>返回加密后密文字符串</returns>
        public static string Encrypt(string content, string secret)
        {
            if ((content == null) || (secret == null) || (content.Length == 0) || (secret.Length == 0))
                throw new ArgumentNullException("Invalid Argument");

            byte[] Key = GetKey(secret);
            byte[] ContentByte = Encoding.Unicode.GetBytes(content);
            MemoryStream MSTicket = new MemoryStream();

            MSTicket.Write(ContentByte, 0, ContentByte.Length);

            byte[] ContentCryptByte = Crypt(MSTicket.ToArray(), Key);

            string ContentCryptStr = Encoding.ASCII.GetString(Base64Encode(ContentCryptByte));

            return ContentCryptStr;
        }

        /// <summary>
        /// 方法说明　：解密方法
        /// 作者    　： 
        /// 完成日期　：
        /// </summary>
        /// <param name="content">需要解密的密文内容</param>
        /// <param name="secret">解密密钥</param>
        /// <returns>返回解密后明文字符串</returns>
        public static string Decrypt(string content, string secret)
        {
            if ((content == null) || (secret == null) || (content.Length == 0) || (secret.Length == 0))
                throw new ArgumentNullException("Invalid Argument");

            byte[] Key = GetKey(secret);

            byte[] CryByte = Base64Decode(Encoding.ASCII.GetBytes(content));
            byte[] DecByte = Decrypt(CryByte, Key);

            byte[] RealDecByte = DecByte;
            byte[] Prefix = new byte[Constants.Operation.UnicodeReversePrefix.Length];
            Array.Copy(RealDecByte, Prefix, 2);

            if (CompareByteArrays(Constants.Operation.UnicodeReversePrefix, Prefix))
            {
                byte SwitchTemp = 0;
                for (int i = 0; i < RealDecByte.Length - 1; i = i + 2)
                {
                    SwitchTemp = RealDecByte[i];
                    RealDecByte[i] = RealDecByte[i + 1];
                    RealDecByte[i + 1] = SwitchTemp;
                }
            }

            string RealDecStr = Encoding.Unicode.GetString(RealDecByte);
            return RealDecStr;
        }

        /// <summary>
        /// 使用TripleDES加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Crypt(byte[] source, byte[] key)
        {
            if ((source.Length == 0) || (source == null) || (key == null) || (key.Length == 0))
            {
                throw new ArgumentException("Invalid Argument");
            }

            TripleDESCryptoServiceProvider dsp = new TripleDESCryptoServiceProvider();
            dsp.Mode = CipherMode.ECB;

            ICryptoTransform des = dsp.CreateEncryptor(key, null);

            return des.TransformFinalBlock(source, 0, source.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] source, byte[] key)
        {
            if ((source.Length == 0) || (source == null) || (key == null) || (key.Length == 0))
            {
                throw new ArgumentNullException("Invalid Argument");
            }

            TripleDESCryptoServiceProvider dsp = new TripleDESCryptoServiceProvider();
            dsp.Mode = CipherMode.ECB;

            ICryptoTransform des = dsp.CreateDecryptor(key, null);

            byte[] ret = new byte[source.Length + 8];

            int num;
            num = des.TransformBlock(source, 0, source.Length, ret, 0);

            ret = des.TransformFinalBlock(source, 0, source.Length);
            ret = des.TransformFinalBlock(source, 0, source.Length);
            num = ret.Length;

            byte[] RealByte = new byte[num];
            Array.Copy(ret, RealByte, num);
            ret = RealByte;
            return ret;
        }

        /// <summary>
        /// 原始base64编码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] Base64Encode(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            ToBase64Transform tb64 = new ToBase64Transform();
            MemoryStream stm = new MemoryStream();
            int pos = 0;
            byte[] buff;

            while (pos + 3 < source.Length)
            {
                buff = tb64.TransformFinalBlock(source, pos, 3);
                stm.Write(buff, 0, buff.Length);
                pos += 3;
            }

            buff = tb64.TransformFinalBlock(source, pos, source.Length - pos);
            stm.Write(buff, 0, buff.Length);

            return stm.ToArray();

        }

        /// <summary>
        /// 原始base64解码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] Base64Decode(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            FromBase64Transform fb64 = new FromBase64Transform();
            MemoryStream stm = new MemoryStream();
            int pos = 0;
            byte[] buff;

            while (pos + 4 < source.Length)
            {
                buff = fb64.TransformFinalBlock(source, pos, 4);
                stm.Write(buff, 0, buff.Length);
                pos += 4;
            }

            buff = fb64.TransformFinalBlock(source, pos, source.Length - pos);
            stm.Write(buff, 0, buff.Length);
            return stm.ToArray();

        }

        /// <summary>
        /// 
        /// </summary>
        public static byte[] GetKey(string secret)
        {
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentException("Secret is not valid");

            ASCIIEncoding ae = new ASCIIEncoding();
            byte[] temp = Hash(ae.GetBytes(secret));

            byte[] ret = new byte[Constants.Operation.KeySize];

            int i;

            if (temp.Length < Constants.Operation.KeySize)
            {
                System.Array.Copy(temp, 0, ret, 0, temp.Length);
                for (i = temp.Length; i < Constants.Operation.KeySize; i++)
                {
                    ret[i] = 0;
                }
            }
            else
                System.Array.Copy(temp, 0, ret, 0, Constants.Operation.KeySize);

            return ret;
        }

        /// <summary>
        /// 比较两个byte数组是否相同
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static bool CompareByteArrays(byte[] source, byte[] dest)
        {
            if ((source == null) || (dest == null))
                throw new ArgumentException("source or dest is not valid");

            bool ret = true;

            if (source.Length != dest.Length)
                return false;
            else
                if (source.Length == 0)
                    return true;

            for (int i = 0; i < source.Length; i++)
                if (source[i] != dest[i])
                {
                    ret = false;
                    break;
                }
            return ret;
        }

        /// <summary>
        /// 使用md5计算散列
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] Hash(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            MD5 m = MD5.Create();
            return m.ComputeHash(source);
        }

        /// <summary>
        /// 对传入的明文密码进行Hash加密,密码不能为中文
        /// </summary>
        /// <param name="oriPassword">需要加密的明文密码</param>
        /// <returns>经过Hash加密的密码</returns>
        public static string HashPassword(string oriPassword)
        {
            if (string.IsNullOrEmpty(oriPassword))
                throw new ArgumentException("oriPassword is valid");

            ASCIIEncoding acii = new ASCIIEncoding();
            byte[] hashedBytes = Hash(acii.GetBytes(oriPassword));

            StringBuilder sb = new StringBuilder(30);
            foreach (byte b in hashedBytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="originalStr">原字符串</param>
        /// <returns></returns>
        public static string EncryptString(string originalStr)
        {
            SymmetrySecret s = new SymmetrySecret(originalStr);
            return s.Encrypt().Replace("=", "aaaaadddd").Replace("?", "cccc99").Replace("/", "dddd99").Replace("&", "rrrr66").Replace(@"\", "ffffii").Replace("+", "ssss876");
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="encryptStr">要解密的字符串</param>
        /// <returns></returns>
        public static string DecryptString(string encryptStr)
        {
            SymmetrySecret s = new SymmetrySecret(encryptStr.Replace("aaaaadddd", "=").Replace("cccc99", "?").Replace("dddd99", "/").Replace("rrrr66", "&").Replace("ffffii", @"\").Replace("ssss876", "+"));
            return s.Decrypt();
        }



        #region 获取可逆密码

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static byte[] EncryptDESPassword(byte[] pwd)
        {
            byte[] Key = GetKey(ECLOGIN_PASSWORD_SECRET);
            return Crypt(pwd, Key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static byte[] DecryptDESPassword(byte[] pwd)
        {
            byte[] Key = GetKey(ECLOGIN_PASSWORD_SECRET);
            return Decrypt(pwd, Key);
        }


        /// <summary>
        /// 加密可逆密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string EncryptDESPassword(string pwd)
        {
                return Encrypt(pwd, ECLOGIN_PASSWORD_SECRET);
        }

        /// <summary>
        /// 解密可逆密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string DecryptDESPassword(string pwd)
        {
                if (string.IsNullOrEmpty(pwd))
                {
                    return string.Empty;
                }
                return Decrypt(pwd, ECLOGIN_PASSWORD_SECRET);
        }

        #endregion



    }
    /// <summary>
    /// 类名称   ：Constants
    /// 类说明   ：加解密算法常量.
    /// 作者     ：
    /// 完成日期 ：
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// 
        /// </summary>
        public struct Operation
        {
            /// <summary>
            /// 
            /// </summary>
            public static readonly int KeySize = 24;
            /// <summary>
            /// 
            /// </summary>
            public static readonly byte[] UnicodeOrderPrefix = new byte[2] { 0xFF, 0xFE };
            /// <summary>
            /// 
            /// </summary>
            public static readonly byte[] UnicodeReversePrefix = new byte[2] { 0xFE, 0xFF };
        }
    }

    internal class SymmetrySecret
    {
        #region 私有变量

        /// <summary>
        /// 待加密和解密的字符序列变量
        /// </summary>
        private string _CryptText;
        /// <summary>
        /// 加密解密私钥变量
        /// </summary>
        private byte[] _CryptKey;
        /// <summary>
        /// 加密解密初始化向量IV变量
        /// </summary>
        private byte[] _CryptIV;

        #endregion

        #region 属性
        /// <summary>
        /// 待加密或解密的字符序列
        /// </summary>
        public string CryptText
        {
            set
            {
                _CryptText = value;
            }
            get
            {
                return _CryptText;
            }
        }

        /// <summary>
        /// 加密私钥
        /// </summary>
        public byte[] CryptKey
        {
            set
            {
                _CryptKey = value;
            }
            get
            {
                return _CryptKey;
            }
        }

        /// <summary>
        /// 加密的初始化向量IV
        /// </summary>
        public byte[] CryptIV
        {
            set
            {
                _CryptIV = value;
            }
            get
            {
                return _CryptIV;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cryptText">待加密和解密的字符序列变量</param>
        /// <param name="cryptKey">加密解密私钥向量Key变量</param>
        /// <param name="cryptIV">加密解密向量IV变量</param>
        public SymmetrySecret(string cryptText, byte[] cryptKey, byte[] cryptIV)
        {
            this.CryptText = cryptText;
            this.CryptKey = cryptKey;
            this.CryptIV = cryptIV;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cryptText">待加密和解密的字符序列变量</param>
        /// <param name="key">加密解密私钥变量字符串</param>
        /// <param name="IV">加密解密IV变量字符串</param>
        public SymmetrySecret(string cryptText, string key, string IV)
        {
            this.CryptText = cryptText;
            this.CryptKey = Convert.FromBase64String(key);
            this.CryptIV = Convert.FromBase64String(IV);
        }
        public SymmetrySecret(string cryptText)
        {
            this.CryptText = cryptText;
            this.CryptKey = Convert.FromBase64String("0t3q4fHJnyAVndj66+gBmmj6FkemW3xt/7uOgMHoKLg=");
            this.CryptIV = Convert.FromBase64String("DZVKpnvwxRhErICEoMTTOw==");
        }
        #endregion

        #region Encrypt 加密函数
        /// <summary>
        /// 加密函数,用于对字符串进行加密。需要提供相应的密钥和IV。
        /// </summary>
        /// <returns></returns>
        public string Encrypt()
        {
            string strEnText = CryptText;
            byte[] EnKey = CryptKey;
            byte[] EnIV = CryptIV;

            byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(strEnText);

            //此处也可以创建其他的解密类实例，但注意不同(长度)的加密类要求不同的密钥Key和初始化向量IV
            RijndaelManaged RMCrypto = new RijndaelManaged();

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, RMCrypto.CreateEncryptor(EnKey, EnIV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }
        #endregion

        #region Decrypt 解密函数
        /// <summary>
        /// 解密函数，用于经过加密的字符序列进行加密。需要提供相应的密钥和IV。
        /// </summary>
        /// <returns></returns>
        public string Decrypt()
        {
            string strDeText = CryptText;
            byte[] DeKey = CryptKey;
            byte[] DeIV = CryptIV;

            byte[] inputByteArray = Convert.FromBase64String(strDeText);

            //此处也可以创建其他的解密类实例，但注意不同的加密类要求不同(长度)的密钥Key和初始化向量IV
            RijndaelManaged RMCrypto = new RijndaelManaged();

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, RMCrypto.CreateDecryptor(DeKey, DeIV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }
        #endregion
    }
}
