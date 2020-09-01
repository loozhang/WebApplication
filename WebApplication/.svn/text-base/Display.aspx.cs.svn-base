using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Common;
using DataModel;
using BLL.UserInfoBLL;

namespace WebApplication3
{
    public partial class Display : System.Web.UI.Page
    {
        public int id;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if(!ValidateUser())
            {
                return;
            }
            string username = txtUserName.Value.Trim();
            string password = txtPassword.Value.Trim();
            int userId = LoginUtil.Login(username,password);
            if(userId==0)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "登录失败，请检查用户名和密码";
                return;
            }
            LoginUtil.LoginSystem(userId.ToString(),false);
            SaveUserInfoToSession(userId);
            Response.Redirect("Index.aspx");
        }

        private bool ValidateUser()
        {
            string username;
            string password;
            username = this.txtUserName.Value.Trim();
            password = txtPassword.Value.Trim();
            if(string.IsNullOrEmpty(username))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "请输入用户名";
                return false;
            }

            if(string.IsNullOrEmpty(password))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "请输入密码";
                return false;
            }
            return true;
        }

        public void SaveUserInfoToSession(int userId)
        {
            UserInfo userInfo = UserBLL.GetInfo(userId);
            ECCommon.SetCookieValue(GlobalKeys.USERINFO, userInfo, true);
        }
    }
}
