using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using BLL.UserInfoBLL;
using DataModel;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace WebApplication3
{
    /// <summary>
    /// SimpleWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class SimpleWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string SayHello(string name)
        {
            if (name != "")
            {
                UserInfo userInfo = UserBLL.GetInfo(Int32.Parse(name));
                string v = userInfo.Name;
                return string.Format("Hello {0}!", v);
            }
            else
                return "";
        }


        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string[] GetCompletionList(string prefixText)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand("select username FROM UserInfo where username like '" + prefixText + "%'");
            DataSet ds = db.ExecuteDataSet(dbCommand);
            //SqlConnection _sqlConnection = new SqlConnection();
            //_sqlConnection.ConnectionString = ConfigurationManager.AppSettings["Conn"];
            //_sqlConnection.Open();
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = new SqlCommand("_selectWords FROM [AutoComplete] where Words like '" + prefixText + "%'", _sqlConnection);
            //DataSet ds = new DataSet();
            //da.Fill(ds);

            List<string> items = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                items.Add(dr["Words"].ToString());
            }
            return items.ToArray();
        }
    }
}
