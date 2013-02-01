using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Utility.Attachment;

namespace Utility
{
    class AttachmentUtility
    {
        private readonly string m_User;

        #region Member Variables

        private static bool m_isInitialized = false;
        private static AttachmentProviderCollection m_providers = null;
        private static AttachmentProvider m_provider = null;

        #endregion


        private void Initialize()
        {
            try
            {
                AttachmentProviderConfigurationSectionHandler attachmentConfig;

                if (!m_isInitialized)
                {

                    // get the configuration section for the feature
                    attachmentConfig = (AttachmentProviderConfigurationSectionHandler)ConfigurationManager.GetSection("attachment");

                    if (attachmentConfig == null)
                        throw new ConfigurationErrorsException("配置节错误!");

                    m_providers = new AttachmentProviderCollection();

                    if (m_providers[attachmentConfig.DefaultProvider] == null)
                    {
                        // use the ProvidersHelper class to call Initialize() on each provider
                        System.Web.Configuration.ProvidersHelper.InstantiateProviders(attachmentConfig.Providers, m_providers, typeof(AttachmentProvider));
                    }

                    // set a reference to the default provider
                    m_provider = m_providers[attachmentConfig.DefaultProvider] as AttachmentProvider;
                    if (m_provider != null)
                    {
                        m_provider.UserName = m_User;
                        m_isInitialized = true;
                    }
                    else
                    {
                        m_isInitialized = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.LogDebug("AttachmentUtility", "Initialize()", "附件处理对象初始化出错：" + ex.Message, null);
            }
        }

        /// <summary>
        /// 附件服务器的URL
        /// </summary>
        public string AttachmentServerUrl
        {
            get
            {

                string url = ConfigurationManager.AppSettings["AttachmentServerUrl"];
                if (string.IsNullOrEmpty(url))
                {
                    throw new ConfigurationErrorsException("缺少AttachmentServerUrl配置，请在web.config或app.config中的<AppSettings>中配置附件服务器的URL");

                }
                return url;
            }
        }

        /// <summary>
        /// 附件服务器
        /// </summary>
        private string AttachmentServerRemotePath
        {
            get
            {
                string url = ConfigurationManager.AppSettings["AttachmentServerPath"];
                if (string.IsNullOrEmpty(url))
                {
                    throw new ConfigurationErrorsException("缺少AttachmentServerPath配置，请在web.config或app.config中的<AppSettings>中配置附件服务器的物理路径AttachmentServerPath");
                }
                return url;
            }
        }

        /// <summary>
        /// 附件服务器登录用户名
        /// </summary>
        private string AttachmentServerUserName
        {
            get
            {
                string userName = ConfigurationManager.AppSettings["AttachmentServerUserName"];
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ConfigurationErrorsException("缺少AttachmentServerUserName配置，请在web.config或app.config中的<AppSettings>中配置附件服务器的物理路径AttachmentServerUserName");
                }
                return userName;

            }
        }

        /// <summary>
        /// 附件服务器登录用户密码
        /// </summary>
        private string AttachmentServerUserPwd
        {
            get
            {
                string userPwd = ConfigurationManager.AppSettings["AttachmentServerUserPwd"];
                if (string.IsNullOrEmpty(userPwd))
                {
                    throw new ConfigurationErrorsException("缺少AttachmentServerUserPwd配置，请在web.config或app.config中的<AppSettings>中配置附件服务器的物理路径AttachmentServerUserPwd");
                }
                return userPwd;

            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        /// <param name="userName"></param>
        public AttachmentUtility(string userName)
        {
            m_User = userName;
            Initialize();
        }

        /// <summary>
        /// 获取附件的完整路径
        /// </summary>
        /// <param name="categoryPath">附件真实的相对路径</param>
        /// <returns></returns>
        public string GetAttachmentFullUrl(string categoryPath)
        {
            if (m_provider != null)
                return m_provider.GetAttachmentFullUrl(categoryPath);
            return "";
        }

        /// <summary>
        /// 获取附件路径
        /// </summary>
        /// <param name="categoryPath"></param>
        /// <returns></returns>
        public FileStream GetAttachmentFile(string categoryPath)
        {
            return m_provider.GetAttachmentFile(categoryPath);
        }

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="sourceFilePath">原始文件路径</param>
        /// <param name="targetCategory">附件的分类文件夹</param>
        /// <param name="fileName">附件的新的文件名称</param>
        /// <param name="delSource">复制成功后是否删除原文件</param>
        /// <returns></returns>
        public string SaveAttach(string sourceFilePath, string targetCategory, string fileName, bool delSource)
        {
            return m_provider.SaveAttach(sourceFilePath, targetCategory, fileName, delSource);

        }

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="sourceFile">要上传的附件的流</param>
        /// <param name="targetCategory">附件的分类文件夹</param>
        ///  <param name="fileName">附件新名称</param>
        /// <returns></returns>
        public string SaveAttach(Stream sourceFile, string targetCategory, string fileName)
        {
            return m_provider.SaveAttach(sourceFile, targetCategory, fileName);
        }

        /// <summary>
        /// 保存附件(推荐使用),并返回附件服务器上的真实的分类目录
        /// </summary>
        /// <param name="postedFile">要保存的附件</param>
        /// <param name="targetCategory">附件的分类文件夹</param>
        /// <param name="fileName">附件的新名称</param>
        /// <returns></returns>
        public string SaveAttach(System.Web.HttpPostedFile postedFile, string targetCategory, string fileName)
        {
            return m_provider.SaveAttach(postedFile, targetCategory, fileName);
        }


        /// <summary>
        /// 附件是否存在
        /// </summary>
        /// <param name="targetCategoryPath"></param>
        /// <returns></returns>
        public bool HasAttachment(string targetCategoryPath)
        {
            return m_provider.HasAttachment(targetCategoryPath);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="targetCategoryAttachPath">附件路径</param>
        public void DownLoadFile(string targetCategoryAttachPath)
        {
            m_provider.DownLoadFile(targetCategoryAttachPath);
        }

        /// <summary>
        /// 获取附件的绝对物理路径
        /// </summary>
        /// <param name="categoryPath">附件路径</param>
        /// <returns></returns>
        public string GetAttachmentFullPath(string categoryPath)
        {
            return m_provider.GetAttachmentFullUrl(categoryPath);
        }
    }
}
