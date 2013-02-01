using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration.Provider;

namespace Utility.Attachment
{
    /// <summary>
    /// 附件Provider
    /// </summary>
    public abstract class AttachmentProvider : ProviderBase
    {
        /// <summary>
        /// 附件服务器URL
        /// </summary>
        public abstract string AttachmentServerUrl { get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public abstract string UserName { get; set; }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="targetCategoryAttachPath">附件的分类路径</param>
        public abstract void DownLoadFile(string targetCategoryAttachPath);

        /// <summary>
        /// 获取附件的完整Url
        /// </summary>
        /// <param name="categoryPath">附件的分类路径</param>
        /// <returns></returns>
        public abstract string GetAttachmentFullUrl(string categoryPath);

        /// <summary>
        /// 获取附件文件
        /// </summary>
        /// <param name="categoryPath">附件的分类路径</param>
        /// <returns></returns>
        public abstract FileStream GetAttachmentFile(string categoryPath);

        /// <summary>
        /// 检查附件是否存在
        /// </summary>
        /// <param name="targetCategoryPath">附件的分类路径</param>
        /// <returns></returns>
        public abstract bool HasAttachment(string targetCategoryPath);

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sourceFilePath">附件的源路径</param>
        /// <param name="targetCategory">将附件保存目标服务器上的该分类目录下</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="delSource">是否删除临时文件中的附件</param>
        /// <returns></returns>
        public abstract string SaveAttach(string sourceFilePath, string targetCategory, string fileName, bool delSource);

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="sourceFile">表示附件的文件流</param>
        /// <param name="targetCategory">要保存附件的目标分类目录</param>
        /// <param name="fileName">附件名称</param>
        /// <returns></returns>
        public abstract string SaveAttach(Stream sourceFile, string targetCategory, string fileName);

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="postedFile">上传的附件</param>
        /// <param name="targetCategory">要保存附件的目标目录</param>
        /// <param name="fileName">附件的名称</param>
        /// <returns></returns>
        public abstract string SaveAttach(System.Web.HttpPostedFile postedFile, string targetCategory, string fileName);
    }
}
