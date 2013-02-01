
//-------------------------------------------------------------------
//版权所有：版权所有(C) 2009，Microsoft(China) Co.,LTD
//系统名称：OESSP
//文件名称：UserBLL.gen.cs
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
using DAL.UserInfoDAL;
using DataModel;
using OESSP.BLL.Common;


namespace BLL.UserInfoBLL
{
    /// <summary>
    ///  UserInfo 业务逻辑层
    /// </summary>
    public partial class UserBLL : BaseBLL
    {
        /// <summary>
        /// 创建BLL对象
        /// </summary>
        /// <param name="user">系统的登录用户</param>
        public UserBLL(IUserInfo user)
            : base(user)
        {
            ERROR_CODE_SELECT = "";    //查询异常编码
            ERROR_CODE_INSERT = "";    //添加异常编码
            ERROR_CODE_UPDATE = "";    //修改异常编码
            ERROR_CODE_DELETE = "";    //删除异常编码
        }

        /// <summary>
        /// 根据UserID字段获取UserInfo实体对象
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <returns>UserInfo实体对象</returns>
        public static UserInfo GetInfo(int userID)
        {
               
                return UserDAL.GetInfo(userID);
           
        }

        /// <summary>
        /// 往UserInfo表内添加新记录
        /// </summary>
        /// <param name="entity">UserInfo实体对象</param>
        /// <returns>添加记录索引值</returns>
        public static void Insert(UserInfo entity)
        {
                UserDAL.Insert(entity);
                //ActionLogBLL.LogAdd(OperationLogType.Add, User, "BLL.UserInfoBLL.UserBLL", identity, "UserInfo", "添加成功");
                //return identity;
        }

        /// <summary>
        /// 更新UserInfo表记录
        /// </summary>
        /// <param name="entity">UserInfo实体对象</param>
        public void Update(UserInfo entity)
        {
            try
            {
                UserDAL.Update(entity);
                //ActionLogBLL.LogAdd(OperationLogType.Update, User, "BLL.UserInfoBLL.UserBLL", entity.UserID, "UserInfo", "修改成功");
            }
            catch (Exception ex)
            {
                //Logger.LogError("UserBLL", "Update", AppError.EROR, 0, ex, "更新UserInfo信息出错。",String.Format("UserID = {0}", entity.UserID));
                //throw new WebApplication3SystemException(ERROR_CODE_UPDATE, ex);
            }
        }

        /// <summary>
        /// 根据UserID字段删除UserInfo实体对象
        /// </summary>
        /// <param name="userID">UserID</param>
        public void Delete(int userID)
        {
            try
            {
                UserDAL.Delete(userID);
                //ActionLogBLL.LogAdd(OperationLogType.Delete, User, "BLL.UserInfoBLL.UserBLL", userID, "UserInfo", "删除成功");
            }
            catch (Exception ex)
            {
                //Logger.LogError("UserBLL", "Delete", AppError.EROR, 0, ex, "删除UserInfo信息出错。",String.Format("userID = {0}", userID));
                //throw new WebApplication3SystemException(ERROR_CODE_DELETE, ex);
            }
        }
        /// <summary>
        /// 用户名唯一性
        /// </summary>
        /// <param name="ecUserName">用户名</param>
        /// <param name="ecCode">编码</param>
        /// <returns></returns>
        public static bool CheckUserNameUsability(string UserName)
        {
                int n =UserDAL.CheckUserInfoUsability(UserName, 1);
                return n == 1;             
       }

        /// <summary>
        /// EC成员登录
        /// </summary>
        /// <param name="userName">EC成员用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="eccodeOrDomain">EC编码或域名</param>
        /// <returns></returns>
        public static int UserLogon(string userName, string pwd)
        {
                int userID = UserDAL.UserLogon(userName, pwd);
                UserInfo user = new UserInfo();
                    user.UserID = userID;
                return userID;
        }
    }
}