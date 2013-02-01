using System;
using System.Security.Principal;
using DataModel;
using System.Web;
using BLL.UserInfoBLL;

namespace WebApplication3
{
    public class PortalIdentity : GenericIdentity
    {
        private UserInfo _user;
        public UserInfo userInfo
        {
            get
            {
                if (_user == null)
                {
                    _user = GetUserInfo();
                }
                return _user;
            }
            set { _user = value; }
        }

        public PortalIdentity(string userId)
            : base(userId)
        {
        }

        public PortalIdentity(UserInfo user)
            : base(user.UserID.ToString())
        {
            if (user == null)
            {
                throw new ArgumentNullException(@"用户对象不能为空");
            }
            _user = user;
        }
        protected UserInfo GetUserInfo()
        {
            if (_user == null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                int userId = 0;
                System.Int32.TryParse(HttpContext.Current.User.Identity.Name, out userId);
                this._user = UserBLL.GetInfo(userId);
            }
            return this._user;
        }
    } 
}