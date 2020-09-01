using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using BLL.UserInfoBLL;

class LoginUtil
    {
        /// <summary>
        /// EC注册界面
        /// </summary>
        public static readonly string JoinPage = "~/Agreement.aspx";

        public static readonly string LoginPage = "~/MainFrame.aspx";


        /// <summary>
        /// EC门户-登录验证码Cookie名称
        /// </summary>
        private static readonly string captchaSessionGuid = "ECPortalVcode";

        public static string CaptchaSessionGuid
        {
            get { return LoginUtil.captchaSessionGuid; }
        }

        /// <summary>
        /// 获取登录用户,暂时使用虚拟用户
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <returns></returns>
        //public static ECUserInfo GetUser(string eccode, string userName)
        //{
        //    return ECUserBLL.GetECUserInfo(eccode, userName);

        //}

        //public static ECUserInfo GetUserByDomain(string domainName, string userName)
        //{
        //    return ECUserBLL.GetECUserInfoByDomain(domainName, userName);

        //}
        /// <summary>
        /// 校验用户登录
        /// </summary>
        /// <param name="corpCode"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int Login(string userName, string password)
        {
            string md5Pwd = CryptTools.HashPassword(password);
            //string desPwd = CryptTools.EncryptDESPassword(password);
            int iUserID = 0;
            iUserID = UserBLL.UserLogon(userName, md5Pwd);
            return iUserID;
        }

        /// <summary>
        /// 获得登录ticket
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="isPersistent"></param>
        public static void LoginSystem(string userID, bool isPersistent)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                                                     1,
                                                                     userID,
                                                                     DateTime.Now,
                                                                     DateTime.Now.AddMinutes(80),
                                                                     isPersistent,
                                                                     userID,
                                                                     FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            if (isPersistent)
            {
                authCookie.Expires = ticket.Expiration;
            }

            // 创建Cookie
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        /// <summary>
        /// 是否允许在登录时使用验证码，默认为"True"
        /// </summary>
        public static bool AllowValidateCode
        {
            get
            {
                bool allowValidateCode = true;

                string strAllowvalidateCode = System.Web.Configuration.WebConfigurationManager.AppSettings["AllowValidateCode"];
                if (!string.IsNullOrEmpty(strAllowvalidateCode))
                {
                    if (strAllowvalidateCode.Equals("false", StringComparison.OrdinalIgnoreCase))
                    {
                        allowValidateCode = false;
                    }
                }

                return allowValidateCode;
            }
        }
    }

