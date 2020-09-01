using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace WebApplication3
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Session["currentColorIndex"] = 0;
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //AutoComplete ac = new AutoComplete();
            SimpleWebService ws = new SimpleWebService();
            Label1.Text = ws.HelloWorld();
        }
 }
}
