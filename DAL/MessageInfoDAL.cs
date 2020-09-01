
//-------------------------------------------------------------------
//版权所有：版权所有(C) 2009，Microsoft(China) Co.,LTD
//系统名称：OESSP
//文件名称：MsgDAL.gen.cs
//模块名称：Message
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

using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataModel;
using Utility;
using System.Collections;

namespace DAL.MessageInfoDAL
{
    /// <summary>
    /// Message 数据访问层
    /// </summary>
    public partial class MsgDAL
    {
        /// <summary>
        /// 根据UserID字段获取MsgInfo实体对象
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <returns>MsgInfo实体对象</returns>
        public static MsgInfo GetInfo(int userID)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT UserID,MsgID,Content,Type,CommentedCount,CommentCount,TransferedCount,TransferCount,Time 
	FROM MsgInfo --WITH(NOLOCK)
	WHERE UserID=@UserID");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@UserID", DbType.Int32, userID);

            //执行命令返回DataReader对象
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateMsgInfo(dr);
                else
                    return null;
            }
        }

        /// <summary>
        /// 获取MsgInfo实体对象列表
        /// </summary>
        /// <returns>MsgInfo实体对象列表</returns>
        public static DataTable GetList(int userID)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetStoredProcCommand("UP_GetMessageList");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@UserID", DbType.Int32, userID);

            //实例化实体列表对象
            DataTable list = new DataTable();

            list = db.ExecuteDataSet(dbCommand).Tables[0];

            //执行命令返回DataReader对象
            //using (IDataReader dr = db.ExecuteReader(dbCommand))
            //{
            //    while (dr.Read())
            //        list.Add(DBConvert.GetString(dr, "Content"));
            //}
            return list;
        }

        /// <summary>
        /// 往MsgInfo表内添加新记录
        /// </summary>
        /// <param name="msgInfo">MsgInfo实体对象</param>
        /// <returns>添加记录索引值</returns>
        public static int Insert(MsgInfo msgInfo)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetSqlStringCommand(@"INSERT INTO MsgInfo(UserID,Content,Type,CommentedCount,CommentCount,TransferedCount,TransferCount,Time)
	VALUES(@UserID,@Content,@Type,@CommentedCount,@CommentCount,@TransferedCount,@TransferCount,@Time)
	SELECT SCOPE_IDENTITY()");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@UserID", DbType.Int32, msgInfo.UserID);
            db.AddInParameter(dbCommand, "@Content", DbType.String, msgInfo.Content);
            db.AddInParameter(dbCommand, "@Type", DbType.Byte, msgInfo.Type);
            db.AddInParameter(dbCommand, "@CommentedCount", DbType.Int32, msgInfo.CommentedCount);
            db.AddInParameter(dbCommand, "@CommentCount", DbType.Int32, msgInfo.CommentCount);
            db.AddInParameter(dbCommand, "@TransferedCount", DbType.Int32, msgInfo.TransferedCount);
            db.AddInParameter(dbCommand, "@TransferCount", DbType.Int32, msgInfo.TransferCount);
            db.AddInParameter(dbCommand, "@Time", DbType.DateTime, msgInfo.Time);

            //返回新添加记录索引
            return int.Parse("0" + db.ExecuteScalar(dbCommand));
            //db.ExecuteNonQuery(dbCommand);
            
           }

        /// <summary>
        /// 更新MsgInfo表记录
        /// </summary>
        /// <param name="msgInfo">MsgInfo实体对象</param>
        public static void Update(MsgInfo msgInfo)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetSqlStringCommand(@"UPDATE MsgInfo SET
	Content = @Content,
	Type = @Type,
	CommentedCount = @CommentedCount,
	CommentCount = @CommentCount,
	TransferedCount = @TransferedCount,
	TransferCount = @TransferCount,
	Time = @Time
	WHERE UserID=@UserID");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@UserID", DbType.Int32, msgInfo.UserID);
            db.AddInParameter(dbCommand, "@Content", DbType.String, msgInfo.Content);
            db.AddInParameter(dbCommand, "@Type", DbType.Byte, msgInfo.Type);
            db.AddInParameter(dbCommand, "@CommentedCount", DbType.Int32, msgInfo.CommentedCount);
            db.AddInParameter(dbCommand, "@CommentCount", DbType.Int32, msgInfo.CommentCount);
            db.AddInParameter(dbCommand, "@TransferedCount", DbType.Int32, msgInfo.TransferedCount);
            db.AddInParameter(dbCommand, "@TransferCount", DbType.Int32, msgInfo.TransferCount);
            db.AddInParameter(dbCommand, "@Time", DbType.DateTime, msgInfo.Time);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 根据UserID字段删除MsgInfo表信息
        /// </summary>
        /// <param name="userID">UserID</param>
        public static void Delete(int userID)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetSqlStringCommand(@"DELETE FROM MsgInfo WHERE UserID=@UserID");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@UserID", DbType.Int32, userID);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 生成MsgInfo实体对象
        /// </summary>
        private static MsgInfo CreateMsgInfo(IDataReader rdr)
        {
            MsgInfo myMsgInfo = new MsgInfo();

            myMsgInfo.UserID = DBConvert.GetInt32(rdr, "UserID");
            myMsgInfo.Content = DBConvert.GetString(rdr, "Content");
            myMsgInfo.Type = DBConvert.GetByte(rdr, "Type");
            myMsgInfo.CommentedCount = DBConvert.GetInt32(rdr, "CommentedCount");
            myMsgInfo.CommentCount = DBConvert.GetInt32(rdr, "CommentCount");
            myMsgInfo.TransferedCount = DBConvert.GetInt32(rdr, "TransferedCount");
            myMsgInfo.TransferCount = DBConvert.GetInt32(rdr, "TransferCount");
            myMsgInfo.Time = DBConvert.GetDateTime(rdr, "Time");

            return myMsgInfo;
        }
    }
}