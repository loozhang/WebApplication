//-------------------------------------------------------------------
//版权所有：版权所有(C) 2009，Microsoft(China) Co.,LTD
//系统名称：OESSP
//文件名称：UserInfo.cs
//模块名称：UserInfo
//模块编号：
//作　　者：张陆
//完成日期：2011/1/24
//功能说明：
//-----------------------------------------------------------------
//修改记录：
//修改人：  
//修改时间：
//修改内容：
//-----------------------------------------------------------------

using System;

namespace DataModel
{
    /// <summary>
    /// UserInfo 实体类
    /// </summary>
    public partial class UserInfo
    {
        /// <summary>
        /// UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Blog
        /// </summary>
        public string Blog { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Birthday
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Photo
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ModifyTime
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// CreateUser
        /// </summary>
        public int CreateUser { get; set; }

        /// <summary>
        /// ModifyUser
        /// </summary>
        public int ModifyUser { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }

    }
}