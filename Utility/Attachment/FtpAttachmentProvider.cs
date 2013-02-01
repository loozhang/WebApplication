using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Utility.FTP;

namespace Utility.Attachment
{
    /// <summary>
    /// FTP访问附件的Provider
    /// </summary>
    public class FtpAttachmentProvider : AttachmentProvider
    {
        private string m_User;
        private string m_AttachmentMappingPath;

        private string _ftpServer;
        private string _ftpUserName;
        private string _ftpPassword;
        private int _ftpTimeOut = 300;
        private int _ftpPort = 21;
        private string _transferMode = "Pasv";

        /// <summary>
        /// FTP服务器用户名
        /// </summary>
        public override string UserName
        {
            get { return m_User; }
            set { m_User = value; }
        }
        /// <summary>
        /// FTP服务器地址
        /// </summary>
        public string FtpServer
        {
            get { return _ftpServer; }
        }

        /// <summary>
        /// FTP用户名
        /// </summary>
        public string FtpUserName
        {
            get
            {
                return _ftpUserName;
            }
        }

        /// <summary>
        /// FTP用户密码
        /// </summary>
        public string FtpPassword
        {
            get
            {
                return _ftpPassword;
            }
        }

        /// <summary>
        /// FTP超时时间
        /// </summary>
        public int FtpTimeout
        {
            get { return _ftpTimeOut; }
        }

        /// <summary>
        /// FTP服务器端口
        /// </summary>
        public int FtpPort
        {
            get { return _ftpPort; }
        }

        /// <summary>
        /// FTP模式
        /// </summary>
        public string TransferMode
        {
            get { return _transferMode; }
        }
        private string _AttachmentServerUrl;

        /// <summary>
        /// 
        /// </summary>
        public override string AttachmentServerUrl
        {
            get { return _AttachmentServerUrl; }
        }

        private string _downLoadTempDir = "uploads";
        /// <summary>
        /// 下载临时目录
        /// </summary>
        public string DownLoadTempDir
        {
            get { return _downLoadTempDir; }
            set { _downLoadTempDir = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FtpAttachmentProvider()
        {

        }




        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userName">用户名</param>
        public FtpAttachmentProvider(string userName)
        {
            m_User = userName;
            m_AttachmentMappingPath = MappingAttachmentServerPath();
        }

        /// <summary>
        /// 获取附件的完整路径
        /// </summary>
        /// <param name="categoryPath">附件真实的相对路径</param>
        /// <returns></returns>
        public override string GetAttachmentFullUrl(string categoryPath)
        {
            return AttachmentServerUrl + "/" + categoryPath;
        }

        /// <summary>
        /// 获取附件服务器上的附件流
        /// </summary>
        /// <param name="categoryPath">真实的附件分类目录</param>
        /// <returns></returns>
        public override FileStream GetAttachmentFile(string categoryPath)
        {
            return DownFromFtp(categoryPath);
        }

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="sourceFilePath">原始文件路径</param>
        /// <param name="targetCategory">附件的分类文件夹</param>
        /// <param name="fileName">附件的新的文件名称</param>
        /// <param name="delSource">复制成功后是否删除原文件</param>
        /// <returns></returns>
        public override string SaveAttach(string sourceFilePath, string targetCategory, string fileName, bool delSource)
        {
            string realCategoryPath = GenAttachmentRealCategoryDir(targetCategory) + "/" + fileName;
            using (FileStream stream = new FileStream(sourceFilePath, FileMode.Open))
            {
                SaveAttach(stream, targetCategory, fileName);
            }

            if (delSource)
                File.Delete(sourceFilePath);

            return realCategoryPath;
        }

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="sourceFile">要上传的附件的流</param>
        /// <param name="targetCategory">附件的分类文件夹</param>
        ///  <param name="fileName">附件新名称</param>
        /// <returns></returns>
        public override string SaveAttach(Stream sourceFile, string targetCategory, string fileName)
        {
            string realCategoryPath = GenAttachmentRealCategoryDir(targetCategory) + "/" + fileName;
            SaveToFTP(sourceFile, realCategoryPath);

            return realCategoryPath;
        }

        private void SaveToFTP(Stream sourceFile, string fullPath)
        {

            FtpClient client = new FtpClient(FtpServer, FtpUserName, FtpPassword, FtpTimeout, FtpPort);
            if (!string.IsNullOrEmpty(TransferMode) && TransferMode.ToLower() == "port")
            {
                client.TransferMode = DataTransferMode.Port;
            }
            else
            {
                client.TransferMode = DataTransferMode.Pasv;
            }
            client.MakeRecursionDir(Path.GetDirectoryName(fullPath));
            client.Upload(sourceFile, Path.GetFileName(fullPath), true);
            client.Close();
        }

        private FileStream DownFromFtp(string categoryPath)
        {
            FtpClient client = new FtpClient(FtpServer, FtpUserName, FtpPassword, FtpTimeout, FtpPort);
            string path = Path.Combine(
                System.Web.HttpContext.Current.Server.MapPath(DownLoadTempDir),
                categoryPath);
            client.Download(path, true);
            return new FileStream(path, FileMode.Open);
        }

        /// <summary>
        /// 保存附件(推荐使用),并返回附件服务器上的真实的分类目录
        /// </summary>
        /// <param name="postedFile">要保存的附件</param>
        /// <param name="targetCategory">附件的分类文件夹</param>
        /// <param name="fileName">附件的新名称</param>
        /// <returns></returns>
        public override string SaveAttach(System.Web.HttpPostedFile postedFile, string targetCategory, string fileName)
        {
            string realCategoryPath = GenAttachmentRealCategoryDir(targetCategory) + "/" + fileName;

            using (Stream input = postedFile.InputStream)
            {
                SaveToFTP(postedFile.InputStream, realCategoryPath);
                input.Close();
            }

            //postedFile.SaveAs(fullPath);
            return (realCategoryPath);
        }


        /// <summary>
        /// 附件是否存在
        /// </summary>
        /// <param name="targetCategoryPath"></param>
        /// <returns></returns>
        public override bool HasAttachment(string targetCategoryPath)
        {
            return false;
            //return File.Exists(GetAttachmentFullPath(targetCategoryPath));
        }




        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="targetCategoryAttachPath"></param>
        public override void DownLoadFile(string targetCategoryAttachPath)
        {

            if (HasAttachment(targetCategoryAttachPath))
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                if (context != null)
                {
                    string fileName = Path.GetFileName(targetCategoryAttachPath);
                    System.Web.HttpResponse Response = context.Response;
                    //System.Web.UI.Page p = (System.Web.UI.Page)context.Handler;
                    //p.ClientScript.RegisterStartupScript(p.GetType(), "ddd", "<script language='javascript'>alert(0)</script>");
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Connection", "keep-alive");
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));

                    byte[] b;
                    using (FileStream fs = GetFile(targetCategoryAttachPath))
                    {
                        b = new byte[fs.Length];
                        fs.Read(b, 0, b.Length);
                        fs.Close();
                    }

                    if (b.Length > 0)
                    {
                        Response.BinaryWrite(b);
                    }

                    try
                    {
                        Response.Flush();
                        Response.End();
                    }
                    catch (ThreadAbortException tae)
                    {
                       // Logger.LogError("FTPAttachmentProvider", "DownloadFile", AppError.WARN, 0, tae, null, null);
                    }
                }

            }
        }

        ////获取附件的绝对物理路径
        //private string GetAttachmentFullPath(string categoryPath)
        //{

        //    string fullPath = "";
        //    string dir = Path.GetDirectoryName(fullPath);
        //    if (!Directory.Exists(dir))
        //        Directory.CreateDirectory(dir);

        //    return fullPath;
        //}

        //生成一个将要保存附件的真实分类路径,根据年份和月分进一步分类
        private static string GenAttachmentRealCategoryDir(string targetCategory)
        {
            if (targetCategory == string.Empty)
                throw new ArgumentException("targetCategory不能为空串，请提供一个分类信息,如: SI/images");

            if (targetCategory.StartsWith("/") || targetCategory.StartsWith("\\"))
                throw new ArgumentException("targetCategory不能以/或\\开始");

            return targetCategory.TrimEnd('/').TrimEnd('\\') + "/" + DateTime.Now.ToString("yyyyMM");
        }


        //获取一个附件的文件流
        private FileStream GetFile(string targetCategoryPath)
        {
            return DownFromFtp(targetCategoryPath);
        }

        //在本地虚拟一个磁盘，并映谢到附件服务器的共享文件夹，该方法的好处是可以使用登录名和密码登录远程服务器
        private static string MappingAttachmentServerPath()
        {
            //暂时不使用
            return "";

            //ConnectionOptions options = new ConnectionOptions();
            //options.Username = AttachmentServerUserName; //could be in domain\user format
            //options.Password = AttachmentServerUserPwd;
            //ManagementScope scope = new ManagementScope(
            //AttachmentServerRemotePath,
            //options);

            //scope.Connect();
            //ManagementObject disk = new ManagementObject(
            //scope,
            //new ManagementPath("Win32_logicaldisk='x:'"),
            //null);

            //disk.Get();
            //return "x:";
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        public override void Initialize(string name, NameValueCollection config)
        {
            if (string.IsNullOrEmpty(name))
                name = "FtpAttachmentProvider";

            if (null == config)
                throw new ArgumentException("配置参数不能为空");

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "通过FTP方式上传附件");
            }

            base.Initialize(name, config);

            GetConfig(config);
        }

        private void GetConfig(NameValueCollection config)
        {
            _ftpServer = config["FtpServer"];
            _ftpUserName = config["FtpUserName"];
            _ftpPassword = config["FtpPassword"];

            _transferMode = config["TransferMode"];

            if (!int.TryParse(config["FtpPort"], out _ftpPort))
                throw new ArgumentException("端口配置错误，端口必需为整型值");

            if (!int.TryParse(config["FtpTimeOut"], out _ftpTimeOut))
                throw new ArgumentException("超时时长配置错误，超时时长必需为整型值");

            _AttachmentServerUrl = config["AttachmentServerUrl"];

        }
    }
}
