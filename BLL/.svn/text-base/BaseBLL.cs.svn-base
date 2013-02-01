//-------------------------------------------------------------------
//版权所有：版权所有(C) 2006，Microsoft(China) Co.,LTD
//系统名称：GMCC-ADC
//文件名称：BasicBLL
//模块名称：
//模块编号：
//作　　者：CWBBOY
//完成日期：11/18/2006 16:11:35
//功能说明：
//-----------------------------------------------------------------
//修改记录：
//修改人：  
//修改时间：
//修改内容：
//-----------------------------------------------------------------

using DataModel;

namespace OESSP.BLL.Common
{
    /// <summary>
    /// 业务层BLL对像的基类
    /// </summary>
    public class BaseBLL
    {
        private IUserInfo m_User = null;
        protected static string ERROR_CODE_SELECT = "";    //查询异常编码
        protected static string ERROR_CODE_INSERT = "";    //添加异常编码
        protected static string ERROR_CODE_UPDATE = "";    //修改异常编码
        protected static string ERROR_CODE_DELETE = "";    //删除异常编码

        ///<summary>
        ///</summary>
        ///<param name="user"></param>
        public BaseBLL(IUserInfo user)
        {
            m_User = user;
        }

        ///<summary>
        ///</summary>
        public IUserInfo User
        {
            get { return m_User; }
        }

        /// <summary>
        /// 用户是否为管理员
        /// </summary>
        public bool IsAdmin
        {
            get { return (string.Compare(m_User.UserName, "admin", true) == 0); }
        }
    }
}