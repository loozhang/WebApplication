
//-------------------------------------------------------------------
//版权所有：版权所有(C) 2009，Microsoft(China) Co.,LTD
//系统名称：OESSP
//文件名称：MsgBLL.gen.cs
//模块名称：MsgInfoBLL
//模块编号：
//作　　者：zhanglu
//完成日期：2011/9/16
//功能说明：
//-----------------------------------------------------------------
//修改记录：
//修改人：  
//修改时间：
//修改内容：
//-----------------------------------------------------------------

using System;
using DAL.MessageInfoDAL;
using DataModel;
using Common;
using DataModel;
using OESSP.BLL.Common;
using System.Collections;
using System.Data;


namespace BLL.MessageInfoBLL
{
    /// <summary>
    ///  MsgInfoBLL 业务逻辑层
    /// </summary>
    public partial class MsgBLL : BaseBLL
    {
        /// <summary>
        /// 创建BLL对象
        /// </summary>
        /// <param name="user">系统的登录用户</param>
        public MsgBLL(IUserInfo user)
            : base(user)
        {
            ERROR_CODE_SELECT = "";    //查询异常编码
            ERROR_CODE_INSERT = "";    //添加异常编码
            ERROR_CODE_UPDATE = "";    //修改异常编码
            ERROR_CODE_DELETE = "";    //删除异常编码
        }

        /// <summary>
        /// 根据UserID字段获取MsgInfo实体对象
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <returns>MsgInfo实体对象</returns>
        public static MsgInfo GetInfo(int userID)
        {
                return MsgDAL.GetInfo(userID);
        }

        /// <summary>
        /// 根据UserID字段获取MsgInfo实体对象
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <returns>MsgInfo实体对象</returns>
        public static DataTable GetList(int userID)
        {
            return MsgDAL.GetList(userID);
        }

        /// <summary>
        /// 往MsgInfo表内添加新记录
        /// </summary>
        /// <param name="entity">MsgInfo实体对象</param>
        /// <returns>添加记录索引值</returns>
        public static int Insert(MsgInfo entity)
        {
                return MsgDAL.Insert(entity);
        }

        /// <summary>
        /// 更新MsgInfo表记录
        /// </summary>
        /// <param name="entity">MsgInfo实体对象</param>
        public void Update(MsgInfo entity)
        {
                MsgDAL.Update(entity);
        }

        /// <summary>
        /// 根据UserID字段删除MsgInfo实体对象
        /// </summary>
        /// <param name="userID">UserID</param>
        public void Delete(int userID)
        {
                MsgDAL.Delete(userID);
        }
    }
}