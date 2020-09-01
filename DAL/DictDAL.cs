////////////////////////////////////////////////////////////////////////////////////////////
// 文件名: SYDictDAL.cs
//
// 模块名称: 数据字典管理数据访问层
//
// 作者: sunlihua
//
// 历史记录（记录修改记录，修改文件后请添加修改记录，注明修改时间、修改人，修改内容）:
// 2009/8/23	sunlihua	创建文件
////////////////////////////////////////////////////////////////////////////////////////////


using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataModel;
using Utility;

namespace DAL
{
    public class DictDAL
    {

        /// <summary>
        /// 根据字典类型查询
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <param name="dictStatus">字典状态</param>
        /// <param name="dictTypeStatus">字典类型状态</param>
        /// <returns>包含字典类型名称的字典信息</returns>
        public static DataTable SelectByDictType(string dictType, DictStatusEnum dictStatus, DictTypeStatusEnum dictTypeStatus)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetStoredProcCommand("UP_SYDict_SelectByDictType");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@DictStatus", DbType.Int32, dictStatus);
            db.AddInParameter(dbCommand, "@DictType", DbType.String, dictType);
            db.AddInParameter(dbCommand, "@DictTypeStatus", DbType.Int32, dictTypeStatus);


            return db.ExecuteDataSet(dbCommand).Tables[0];
        }


        /// <summary>
        /// 检查某字典类型的字典名称是否存在
        /// </summary>
        /// <param name="dictItemName">字典名称</param>
        /// <param name="dictType">字典类型</param>
        /// <returns></returns>
        public static bool CheckName(string dictItemName, string dictType)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetStoredProcCommand("UP_SYDictInfo_CheckName");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@DictItemName", DbType.String, dictItemName);
            db.AddInParameter(dbCommand, "@DictType", DbType.String, dictType);

            return (int)(db.ExecuteScalar(dbCommand)) > 0;
        }

        /// <summary>
        /// 检查某字典类型的字典代码是否存在
        /// </summary>
        /// <param name="dictItemCode">字典名称</param>
        /// <param name="dictType">字典类型</param>
        /// <returns></returns>
        public static bool CheckCode(string dictItemCode, string dictType)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetStoredProcCommand("UP_SYDictInfo_CheckCode");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@DictItemCode", DbType.String, dictItemCode);
            db.AddInParameter(dbCommand, "@DictType", DbType.String, dictType);

            return (int)(db.ExecuteScalar(dbCommand)) > 0;
        }

        /// <summary>
        /// 检查某字典类型的字典值否存在
        /// </summary>
        /// <param name="dictStringValue">字典值</param>
        /// <param name="dictType">字典类型</param>
        /// <returns></returns>
        public static bool CheckValue(string dictStringValue, string dictType)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetStoredProcCommand("UP_SYDictInfo_CheckValue");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@DictStringValue", DbType.String, dictStringValue);
            db.AddInParameter(dbCommand, "@DictType", DbType.String, dictType);

            return (int)(db.ExecuteScalar(dbCommand)) > 0;
        }

        ///<summary>
        ///</summary>
        ///<param name="row"></param>
        ///<returns></returns>
        public static DictInfo CreateSYDictInfo(DataRow row)
        {
            var entity = new DictInfo();
            entity.DictType = DBConvert.GetString(row, "DictType");
            entity.DictItemName = DBConvert.GetString(row, "DictItemName");
            entity.DictItemCode = DBConvert.GetString(row, "DictItemCode");
            entity.DictStringValue = DBConvert.GetString(row, "DictStringValue");
            entity.DictIntValue = DBConvert.GetInt32(row, "DictIntValue");
            entity.Status = DBConvert.GetInt32(row, "Status");
            entity.Comments = DBConvert.GetString(row, "Comments");
            entity.CreateUser = DBConvert.GetInt32(row, "CreateUser");
            entity.CreateTime = DBConvert.GetDateTime(row, "CreateTime");
            entity.ModifyUser = DBConvert.GetInt32(row, "ModifyUser");
            entity.ModifyTime = DBConvert.GetDateTime(row, "ModifyTime");
            entity.DisOrder = DBConvert.GetInt32(row, "DisOrder");
            return entity;
        }

        public static DictInfo GetInfo(string dictType, string dictStringValue)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetStoredProcCommand("UP_SYDictInfo_Select");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@DictType", DbType.String, dictType);
            db.AddInParameter(dbCommand, "@DictStringValue", DbType.String, dictStringValue);

            //执行命令返回DataReader对象
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateSYDictInfo(dr);
                else
                    return null;
            }
        }

        private static DictInfo CreateSYDictInfo(IDataReader rdr)
        {
            DictInfo mySYDictInfo = new DictInfo();

            mySYDictInfo.DictType = DBConvert.GetString(rdr, "DictType");
            mySYDictInfo.DictItemName = DBConvert.GetString(rdr, "DictItemName");
            mySYDictInfo.DictItemCode = DBConvert.GetString(rdr, "DictItemCode");
            mySYDictInfo.DictStringValue = DBConvert.GetString(rdr, "DictStringValue");
            mySYDictInfo.DictIntValue = DBConvert.GetInt32(rdr, "DictIntValue");
            mySYDictInfo.Status = DBConvert.GetInt32(rdr, "Status");
            mySYDictInfo.Comments = DBConvert.GetString(rdr, "Comments");
            mySYDictInfo.CreateUser = DBConvert.GetInt32(rdr, "CreateUser");
            mySYDictInfo.CreateTime = DBConvert.GetDateTime(rdr, "CreateTime");
            mySYDictInfo.ModifyUser = DBConvert.GetInt32(rdr, "ModifyUser");
            mySYDictInfo.ModifyTime = DBConvert.GetDateTime(rdr, "ModifyTime");
            mySYDictInfo.DisOrder = DBConvert.GetInt32(rdr, "DisOrder");

            return mySYDictInfo;

        }

    }
}
