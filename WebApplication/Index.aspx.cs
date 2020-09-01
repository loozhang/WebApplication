using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;
using DataModel;
using System.Web.Security;
using BLL.MessageInfoBLL;
using System.Security;

namespace WebApplication3
{
    public partial class Index : System.Web.UI.Page
    {
        public new PortalUser User
        {
            get { return base.User as PortalUser; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            string t=HttpContext.Current.User.Identity.Name.ToString();
            int id = Int32.Parse(t);
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("GetUserInfo_ByID");
            db.AddInParameter(dbCommand,"@ID",DbType.Int32,id);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            //if (Request.IsAuthenticated == false)
            //{
            //    Response.Write("<script language='javascript'>javascript:top.document.location='Display.aspx';</script>");
            //    return;
            //}
            //Session.Clear();
            //Session.Abandon();

            //Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            //Response.Expires = 0;
            //Response.Buffer = true;
            //Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            //Response.AddHeader("pragma", "no-cache");
            //Response.CacheControl = "no-cache";
            Response.Redirect("Display.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Message.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Test1.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Test3.aspx");
        }
   }
}
