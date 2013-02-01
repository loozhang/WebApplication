using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Configuration;
using System.Web;
using System.Threading;

namespace Common
{
    ///<summary>
    ///</summary>
    public class PublicMethod
    {
        /// <summary>
        /// 获取事务隔离级别
        /// </summary>
        /// <returns></returns>
        public static TransactionOptions GetTransactionOptions()
        {
            TransactionOptions transactionOptions = new TransactionOptions();
                switch (ConfigurationManager.AppSettings["IsolationLevel"].ToString())
                {
                    case "Chaos":
                        transactionOptions.IsolationLevel = IsolationLevel.Chaos;
                        break;
                    case "ReadCommitted":
                        transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
                        break;
                    case "ReadUncommitted":
                        transactionOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
                        break;
                    case "RepeatableRead":
                        transactionOptions.IsolationLevel = IsolationLevel.RepeatableRead;
                        break;
                    case "Serializable":
                        transactionOptions.IsolationLevel = IsolationLevel.Serializable;
                        break;
                    case "Snapshot":
                        transactionOptions.IsolationLevel = IsolationLevel.Snapshot;
                        break;
                    case "Unspecified":
                        transactionOptions.IsolationLevel = IsolationLevel.Unspecified;
                        break;
                    default:
                        transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
                        break;
                }
            return transactionOptions;
        }

        /// <summary>
        /// 安全的Response.End()
        /// </summary>
        public static void SafeResponseEnd(HttpResponse response, String title)
        {
            try
            {
                response.Flush();

                response.End();

                Thread.Sleep(2000);
            }
            catch (ThreadAbortException tae)
            {
                //Logger.LogInfo("PublicMethodBLL", "SafeResponseEnd", 0, 
                //    "\"" + title + "\"页面在Response.End()时发生异常，只做为记录，不影响程序正常运行。",
                //    tae.StackTrace + tae.Source);
            }
        }

        /// <summary>
        /// 安全的Response.Redirect()
        /// </summary>
        public static void SafeResponseRedirect(HttpResponse response, string title, string url, bool endResponse)
        {
            try
            {
                response.Buffer = true;
                response.Redirect(url);
            }
            catch (ThreadAbortException tae)
            {
                //Logger.LogInfo("PublicMethodBLL", "SafeResponseRedirect", 0, "\"" + title + "\"页面在使用Response.Redirect()转向到\"" + url + "\"页面时发生异常，只做为记录，不影响程序正常运行。", null);
            }
        }

        /// <summary>
        /// 安全的Response.Redirect()
        /// </summary>
        public static void SafeResponseRedirect(HttpResponse response, string title, string url)
        {
            try
            {
                response.Buffer = true;
                response.Redirect(url);
            }
            catch (ThreadAbortException tae)
            {
                //Logger.LogInfo("PublicMethodBLL", "SafeResponseRedirect", 0, "\"" + title + "\"页面在使用Response.Redirect()转向到\"" + url + "\"页面时发生异常，只做为记录，不影响程序正常运行。", null);
            }
        }
    }
}
