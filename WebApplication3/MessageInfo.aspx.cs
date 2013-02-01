using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL.MessageInfoBLL;
using DataModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;

namespace WebApplication3
{
    public partial class MessageInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable msgInfo = new DataTable();
            string t = HttpContext.Current.User.Identity.Name.ToString();
            int id = Int32.Parse(t);
            msgInfo=MsgBLL.GetList(id);
            if (msgInfo!= null)
            {
                this.GridView1.DataSource = msgInfo;
                this.GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TextArea1.InnerText == "")
                this.Label1.Text = "不能发空微博";
            else
            {
                MsgInfo msgInfo = new MsgInfo();
                string msg = this.TextArea1.InnerText;
                msgInfo.Content = msg;
                msgInfo.UserID = Int32.Parse(HttpContext.Current.User.Identity.Name);
                msgInfo.Type = 1;
                msgInfo.CommentedCount = 0;
                msgInfo.CommentCount = 0;
                msgInfo.TransferCount = 0;
                msgInfo.CommentedCount = 0;
                msgInfo.Time = DateTime.Now;

                int i = MsgBLL.Insert(msgInfo);
                this.Label1.Text = i.ToString();
                this.TextArea1.InnerText = "";
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            DataBind();//重新绑定一遍数据
        }
        protected void btnRetweet_ServerClick(object sender, EventArgs e)
        {
            int msgId = int.Parse(hdDeleteID.Value);
            this.Label1.Text = msgId.ToString();
        }
    }
}
