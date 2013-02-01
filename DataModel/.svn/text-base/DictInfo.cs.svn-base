
//-------------------------------------------------------------------
//版权所有：版权所有(C) 2009，Microsoft(China) Co.,LTD
//文件名称：SYDictInfo.cs
//模块名称：
//模块编号：
//作　　者：张立家
//完成日期：2009/8/27
//功能说明：
//-----------------------------------------------------------------
//修改记录：
//修改人：  
//修改时间：
//修改内容：
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel
{
    /// <summary>
    /// SYDictInfo表实体类
    /// </summary>
    /// 
    [Serializable]
    public class DictInfo
    {
        private string dictType = string.Empty;
        private string dictItemName = string.Empty;
        private string dictItemCode = string.Empty;
        private string dictStringValue = string.Empty;
        private int dictIntValue;
        private int status;
        private string comments = string.Empty;
        private int createUser;
        private DateTime createTime;
        private int modifyUser;
        private DateTime modifyTime;
        private int disOrder;

        /// <summary>
        /// 
        /// </summary>
        public DictInfo()
        {

        }

        ///<summary>
        ///</summary>
        ///<param name="_dictType"></param>
        ///<param name="_dictItemName"></param>
        ///<param name="_dictItemCode"></param>
        ///<param name="_dictStringValue"></param>
        ///<param name="_dictIntValue"></param>
        ///<param name="_status"></param>
        ///<param name="_comments"></param>
        ///<param name="_createUser"></param>
        ///<param name="_createTime"></param>
        ///<param name="_modifyUser"></param>
        ///<param name="_modifyTime"></param>
        ///<param name="_disOrder"></param>
        public DictInfo(
            string _dictType,
            string _dictItemName,
            string _dictItemCode,
            string _dictStringValue,
            int _dictIntValue,
            int _status,
            string _comments,
            int _createUser,
            DateTime _createTime,
            int _modifyUser,
            DateTime _modifyTime,
            int _disOrder
        )
        {
            dictType = _dictType;
            dictItemName = _dictItemName;
            dictItemCode = _dictItemCode;
            dictStringValue = _dictStringValue;
            dictIntValue = _dictIntValue;
            status = _status;
            comments = _comments;
            createUser = _createUser;
            createTime = _createTime;
            modifyUser = _modifyUser;
            modifyTime = _modifyTime;
            disOrder = _disOrder;
        }

        /// <summary>
        /// 数据字典类型
        /// </summary>
        public string DictType
        {
            get { return dictType; }
            set { dictType = value; }
        }

        /// <summary>
        /// 数据字典项显示名称
        /// </summary>
        public string DictItemName
        {
            get { return dictItemName; }
            set { dictItemName = value; }
        }

        /// <summary>
        /// 数据字典项编码
        /// </summary>
        public string DictItemCode
        {
            get { return dictItemCode; }
            set { dictItemCode = value; }
        }

        /// <summary>
        /// 数据字典项字符串值
        /// </summary>
        public string DictStringValue
        {
            get { return dictStringValue; }
            set { dictStringValue = value; }
        }

        /// <summary>
        /// 数据字典项数字值
        /// </summary>
        public int DictIntValue
        {
            get { return dictIntValue; }
            set { dictIntValue = value; }
        }

        /// <summary>
        /// 状态：0-正常，1-冻结
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        public int CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        public int ModifyUser
        {
            get { return modifyUser; }
            set { modifyUser = value; }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime
        {
            get { return modifyTime; }
            set { modifyTime = value; }
        }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisOrder
        {
            get { return disOrder; }
            set { disOrder = value; }
        }

    }
}