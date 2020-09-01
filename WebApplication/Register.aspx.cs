using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using BLL.UserInfoBLL;
using DataModel;
using Common;

namespace WebApplication
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { 
                this.imgValidateImg.Src = "../ValidateCode/ValidateCode.aspx?r=" + DateTime.Now.Ticks.ToString();
            //初始化生日
            InitDateList();
            InitRegularExpression();
            //加入Script
            ddlMonth.Attributes["onchange"] = "javascript:SetDayList($get('" + ddlYear.UniqueID + "'), $get('" + ddlMonth.UniqueID + "'), $get('" + ddlDay.UniqueID + "'));";
            ddlDay.Attributes["onchange"] = "javascript:SetBirthday($get('" + ddlYear.UniqueID + "'), $get('" + ddlMonth.UniqueID + "'), $get('" + ddlDay.UniqueID + "'), $get('" + birth.UniqueID + "'));";
            }
           
        }
        /// <!--初始化生日DropDownList-->
        /// <summary>
        /// 初始化生日DropDownList - design By Phoenix 2008 -
        /// </summary>
        private void InitDateList()
        {
            if (ddlYear.Items.Count == 0 || ddlMonth.Items.Count == 0 || ddlDay.Items.Count == 0)
            {
                int displaymaxyear = 98;
                int currentyear = DateTime.Now.Year;
                //Init Year
                ddlYear.Items.Clear();
                ddlYear.Items.Add("Year");
                ddlYear.Items.Add("====");
                for (int listIndex = 2; listIndex <= displaymaxyear; listIndex++)
                    ddlYear.Items.Add((currentyear - listIndex + 2).ToString());
                //Init Month
                ddlMonth.Items.Clear();
                ddlMonth.Items.Add("Month");
                ddlMonth.Items.Add("=====");
                for (int listIndex = 0; listIndex < 12; listIndex++)
                    ddlMonth.Items.Add((listIndex + 1).ToString());
                //Init Day
                ddlDay.Items.Clear();
                ddlDay.Items.Add("Day");
                ddlDay.Items.Add("===");
                for (int listIndex = 0; listIndex < 31; listIndex++)
                    ddlDay.Items.Add((listIndex + 1).ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!CheckLoginName(txtUserName.Text.Trim(), false))
            {
                return;
            }
            else if(ValidateCodeResult() == true)
            {
                string mybirthday = Request[birth.UniqueID];
                DateTime dt = DateTime.Parse(mybirthday);
                Response.Write(mybirthday);
                UserInfo userInfo = ConstructEntityClass();
                UserBLL.Insert(userInfo);
                Response.Redirect("Success.aspx");
            };
        }

        private UserInfo ConstructEntityClass()
        {
            UserInfo userInfo=new UserInfo();
            userInfo.UserName = this.txtUserName.Text.Trim();
            userInfo.Password =  CryptTools.HashPassword(this.txtPassword.Text.Trim()) ;
            userInfo.NickName = this.txtNickName.Text.Trim();
            userInfo.Email = this.txtEmail.Text;
            userInfo.Blog = this.txtBlog.Text.Trim();
            userInfo.Phone = this.txtMobile.Text;
            userInfo.Name = this.txtName.Text.Trim();
            userInfo.Birthday = Request[birth.UniqueID].ToString().Trim();
            userInfo.Gender = Convert.ToInt32(this.RadioButtonList1.SelectedValue);
            userInfo.Location =this.SelectArea1.AreaUseID.ToString();
            userInfo.CreateTime = DateTime.Now;
            userInfo.CreateUser = 0;
            userInfo.Status = 1;
            return userInfo;
        }


        public bool ValidateCodeResult()
        {
            string userEntry = this.txtValidate.Text.Trim();
            bool isValidCaptchaCode = false;
            //启用验证码时，检查验证码
            if (LoginUtil.AllowValidateCode && this.Visible == true)
            {

                string sCode = ECCommon.GetCookieValue(LoginUtil.CaptchaSessionGuid);
                if (string.IsNullOrEmpty(sCode))
                {
                    lblMsg.Text = @"验证码已经失效，请刷新重试";
                    this.txtValidate.Text = "";
                    lblMsg.Visible = true;
                }
                else
                {

                    if (string.Compare(userEntry, sCode, true) != 0)
                    {
                        lblMsg.Text = @"验证码输入不正确";
                        this.txtValidate.Text = "";
                        lblMsg.Visible = true;
                    }
                    else
                    {
                        ////验证码成功
                        //lblMsg.Text = "";
                        isValidCaptchaCode = true;
                    }
                }
                //HttpContext.Current.Session.Remove(LoginUtil.CaptchaSessionGuid);
                ECCommon.RemoveCookie(LoginUtil.CaptchaSessionGuid);

                //if (isValidCaptchaCode == false) return;
                //return isValidCaptchaCode;
            }
            return isValidCaptchaCode;
        }
        private void InitRegularExpression()
        {
            this.revEMail.ValidationExpression = RegularExpressionHelper.GetCheckString(RegularExpressionType.EMAIL);
            this.revMobile.ValidationExpression = RegularExpressionHelper.GetCheckString(RegularExpressionType.MOBILE);

            this.revPassword.ValidationExpression = RegularExpressionHelper.GetCheckString(RegularExpressionType.PWD);
            this.revPassword.ErrorMessage = RegularExpressionHelper.GetCheckString(RegularExpressionType.PWDErrorMessage);
        }
        /// <summary>
        /// 检测当前的用户名是否唯一
        /// </summary>
        /// <param name="chName"></param>
        /// <param name="isShowOKMsg">
        /// true: 显示可用提示， false: 不显示可用提示
        /// </param>
        /// <returns></returns>
        private bool CheckLoginName(string userName, bool isShowOKMsg)
        {

            if (UserBLL.CheckUserNameUsability(userName))
            {
                if (isShowOKMsg)
                {
                    //lblUserNameMsg.Text = @"您可以注册这个用户名";
                    //lblUserNameMsg.Visible = true;
                    ContentUtil.AjaxAlert(upLogin, Page, "您可以注册这个用户名。");
                }
                return true;
            }
            else
            {
                //lblUserNameMsg.Text = @"这个用户名已经被注册！";
                //lblUserNameMsg.Visible = true;
                ContentUtil.AjaxAlert(upLogin, Page, "这个用户名已经被注册！");
                return false;
            }
        }
    }
}