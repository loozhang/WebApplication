using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using BLL.UserInfoBLL;
using DataModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;

namespace WebApplication3
{
    /// <summary>
///AutoComplete 的摘要说明
/// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 （很多新手像我一样开始都没多大留意这句话）
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService
{
        [WebMethod]
        public string[] GetCompletionList(string prefixText)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand("select top 9 username FROM UserInfo where username like '" + prefixText + "%'");
            DataSet ds = db.ExecuteDataSet(dbCommand);
            List<string> items = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                items.Add(dr["username"].ToString());
            }
            return items.ToArray();
        }
}
}

