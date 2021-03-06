﻿
//-------------------------------------------------------------------
//版权所有：版权所有(C) 2009，Microsoft(China) Co.,LTD
//系统名称：OESSP
//文件名称：UserDAL.gen.cs
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
using System.Data;
using System.Data.Common;
using DataModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Utility;

namespace DAL.UserInfoDAL
{
    /// <summary>
    /// UserInfo 数据访问层
    /// </summary>
    public partial class UserDAL
    {
        /// <summary>
        /// 根据UserID字段获取UserInfo实体对象
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <returns>UserInfo实体对象</returns>
        public static UserInfo GetInfo(int userID)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT UserID,UserName,Password,NickName,Email,Blog,Phone,Name,Birthday,Gender,Location,Photo,CreateTime,ModifyTime,CreateUser,ModifyUser,Status 
	FROM UserInfo --WITH(NOLOCK)
	WHERE UserID=@UserID");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@UserID", DbType.Int32, userID);

            //执行命令返回DataReader对象
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateUserInfo(dr);
                else
                    return null;
            }
        }

        /// <summary>
        /// 获取UserInfo实体对象列表
        /// </summary>
        /// <returns>UserInfo实体对象列表</returns>
        public static WA3List<UserInfo> GetList()
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetSqlStringCommand("SELECT UserName,Password,NickName,Email,Blog,Phone,Name,Birthday,Gender,Location,CreateTime,ModifyTime,CreateUser,ModifyUser,Status WITH(NOLOCK) FROM UserInfo");

            //添加输入输出参数
            //db.AddInParameter(dbCommand, "@UserID", DbType.Int32, userID);

            //实例化实体列表对象
            WA3List<UserInfo> list = new WA3List<UserInfo>();

            //执行命令返回DataReader对象
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                    list.Add(CreateUserInfo(dr));
            }
            return list;
        }

        /// <summary>
        /// 往UserInfo表内添加新记录
        /// </summary>
        /// <param name="userInfo">UserInfo实体对象</param>
        /// <returns>添加记录索引值</returns>
        public static void Insert(UserInfo userInfo)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetStoredProcCommand("SP_UserInfo_Insert");

            //添加输入输出参数
//            db.AddInParameter(dbCommand, "@UserID", DbType.Int32, userInfo.UserID);
            db.AddInParameter(dbCommand, "@UserName", DbType.String, userInfo.UserName);
            db.AddInParameter(dbCommand, "@Password", DbType.String, userInfo.Password);
            db.AddInParameter(dbCommand, "@NickName", DbType.String, userInfo.NickName);
            db.AddInParameter(dbCommand, "@Email", DbType.String, userInfo.Email);
            db.AddInParameter(dbCommand, "@Blog", DbType.String, userInfo.Blog);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, userInfo.Phone);
            db.AddInParameter(dbCommand, "@Name", DbType.String, userInfo.Name);
            db.AddInParameter(dbCommand, "@Birthday", DbType.String, userInfo.Birthday);
            db.AddInParameter(dbCommand, "@Gender", DbType.Int32, userInfo.Gender);
            db.AddInParameter(dbCommand, "@Location", DbType.String, userInfo.Location);
            //db.AddInParameter(dbCommand, "@Photo", DbType.String, userInfo.Photo);
            db.AddInParameter(dbCommand, "@CreateTime", DbType.DateTime, userInfo.CreateTime);
            db.AddInParameter(dbCommand, "@CreateUser", DbType.Int32, userInfo.CreateUser);
            db.AddInParameter(dbCommand, "@Status", DbType.Int32, userInfo.Status);

            db.ExecuteNonQuery(dbCommand);

            //返回新添加记录索引
            //return int.Parse("0" + db.ExecuteScalar(dbCommand));
        }

        /// <summary>
        /// 更新UserInfo表记录
        /// </summary>
        /// <param name="userInfo">UserInfo实体对象</param>
        public static void Update(UserInfo userInfo)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetSqlStringCommand(@"UPDATE UserInfo SET
	UserName = @UserName,
	Password = @Password,
	NickName = @NickName,
	Email = @Email,
	Blog = @Blog,
	Phone = @Phone,
	Name = @Name,
	Birthday = @Birthday,
	Gender = @Gender,
	Location = @Location,
	ModifyTime = @ModifyTime,
	ModifyUser = @ModifyUser,
	Status = @Status
	WHERE UserID=@UserID");

            //添加输入输出参数
            //db.AddInParameter(dbCommand, "@UserID", DbType.Int32, userInfo.UserID);
            db.AddInParameter(dbCommand, "@UserName", DbType.String, userInfo.UserName);
            db.AddInParameter(dbCommand, "@Password", DbType.String, userInfo.Password);
            db.AddInParameter(dbCommand, "@NickName", DbType.String, userInfo.NickName);
            db.AddInParameter(dbCommand, "@Email", DbType.String, userInfo.Email);
            db.AddInParameter(dbCommand, "@Blog", DbType.String, userInfo.Blog);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, userInfo.Phone);
            db.AddInParameter(dbCommand, "@Name", DbType.String, userInfo.Name);
            db.AddInParameter(dbCommand, "@Birthday", DbType.String, userInfo.Birthday);
            db.AddInParameter(dbCommand, "@Gender", DbType.Int32, userInfo.Gender);
            db.AddInParameter(dbCommand, "@Location", DbType.String, userInfo.Location);
            //db.AddInParameter(dbCommand, "@Photo", DbType.String, userInfo.Photo);
            db.AddInParameter(dbCommand, "@ModifyTime", DbType.DateTime, userInfo.ModifyTime);
            db.AddInParameter(dbCommand, "@ModifyUser", DbType.Int32, userInfo.ModifyUser);
            db.AddInParameter(dbCommand, "@Status", DbType.Int32, userInfo.Status);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 根据UserID字段删除UserInfo表信息
        /// </summary>
        /// <param name="userID">UserID</param>
        public static void Delete(int userID)
        {
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetSqlStringCommand(@"DELETE FROM UserInfo WHERE UserID=@UserID");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@UserID", DbType.Int32, userID);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 生成UserInfo实体对象
        /// </summary>
        private static UserInfo CreateUserInfo(IDataReader rdr)
        {
            UserInfo myUserInfo = new UserInfo();

            //myUserInfo.UserID = DBConvert.GetInt32(rdr, "UserID");
            myUserInfo.UserName = DBConvert.GetString(rdr, "UserName");
            myUserInfo.Password = DBConvert.GetString(rdr, "Password");
            myUserInfo.NickName = DBConvert.GetString(rdr, "NickName");
            myUserInfo.Email = DBConvert.GetString(rdr, "Email");
            myUserInfo.Blog = DBConvert.GetString(rdr, "Blog");
            myUserInfo.Phone = DBConvert.GetString(rdr, "Phone");
            myUserInfo.Name = DBConvert.GetString(rdr, "Name");
            myUserInfo.Birthday = DBConvert.GetString(rdr, "Birthday");
            myUserInfo.Gender = DBConvert.GetInt32(rdr, "Gender");
            myUserInfo.Location = DBConvert.GetString(rdr, "Location");
            //myUserInfo.Photo = DBConvert.GetString(rdr, "Photo");
            myUserInfo.CreateTime = DBConvert.GetDateTime(rdr, "CreateTime");
            myUserInfo.ModifyTime = DBConvert.GetDateTime(rdr, "ModifyTime");
            myUserInfo.CreateUser = DBConvert.GetInt32(rdr, "CreateUser");
            myUserInfo.ModifyUser = DBConvert.GetInt32(rdr, "ModifyUser");
            myUserInfo.Status = DBConvert.GetInt32(rdr, "Status");

            return myUserInfo;
        }
        /// <summary>
        /// 用户注册信息判重
        /// </summary>
        /// <param name="info">要查询的信息</param>
        /// <param name="eccode"></param>
        /// <param name="type">1、用户名2、手机3、邮箱</param>
        /// <returns></returns>
        public static int CheckUserInfoUsability(string info, int type)
        {

            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetStoredProcCommand("UP_UserInfo_CheckUserInfoUsability");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@Info", DbType.String, info);
            db.AddInParameter(dbCommand, "@Type", DbType.String, type);

            return Convert.ToInt32(db.ExecuteScalar(dbCommand));
        }

        public static int UserLogon(string UserName, string Password)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("UP_UserInfo_Logon");
            db.AddInParameter(dbCommand, "@username", DbType.String, UserName);
            db.AddInParameter(dbCommand, "@password", DbType.String, Password);

            DataSet ds = db.ExecuteDataSet(dbCommand);

            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
    }
}