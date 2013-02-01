using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 正则表达式类型静态类
    /// </summary>
    public class RegularExpressionType
    {
        /// <summary>
        /// 移动手机号码
        /// </summary>
        public static readonly string MOBILE = "Mobile";

        /// <summary>
        /// 所有手机号码
        /// </summary>
        public static readonly string ALLMOBILE = "AllMobile";

        /// <summary>
        /// 电子邮件
        /// </summary>
        public static readonly string EMAIL = "Email";

        /// <summary>
        /// 电话号码
        /// </summary>
        public static readonly string PHONE = "Phone";

        /// <summary>
        /// 密码
        /// </summary>
        public static readonly string PWD = "Pwd";

        /// <summary>
        /// 密码
        /// </summary>
        public static readonly string PWDErrorMessage = "PwdErrorMessage";

        /// <summary>
        /// 金钱
        /// </summary>
        public static readonly string MONEY = "Money";

        /// <summary>
        /// 链接地址
        /// </summary>
        public static readonly string URL = "URL";

        /// <summary>
        /// 链接地址
        /// </summary>
        public static readonly string SIURL = "SIURL";

        /// <summary>
        /// 0-100的整数
        /// </summary>
        public static readonly string Int100="Int100";

        /// <summary>
        /// 正整数
        /// </summary>
        public static readonly string Integer = "Integer";

        /// <summary>
        /// EC域名规则
        /// </summary>
        public static readonly string ECDomain = "ECDomain";

        /// <summary>
        /// 国内固定电话号码
        /// </summary>
        public static readonly string TELEPHONE = "Telephone";


        /// <summary>
        /// SI代码
        /// </summary>
        public static readonly string SICODE = "SICode";

        /// <summary>
        /// 最大长度1024
        /// </summary>
        public static readonly string MAX1024 = "Max1024";

        /// <summary>
        /// 最大长度512
        /// </summary>
        public static readonly string MAX512 = "Max512";

        /// <summary>
        /// 最大长度30
        /// </summary>
        public static readonly string MAX30 = "Max30";


        /// <summary>
        /// 不含中文
        /// </summary>
        public static readonly string NoChinese = "NoChinese";

    }
}
