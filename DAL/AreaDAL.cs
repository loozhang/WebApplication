using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Common;
using DataModel;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DAL
{
    public class AreaDAL
    {
        public static AreaInfo GetInfo(int areaID)
        {
            if (areaID == -1)
                return null;
            //创建Database对象
            Database db = DatabaseFactory.CreateDatabase();
            //创建DbCommand对象
            DbCommand dbCommand = db.GetStoredProcCommand("UP_SYAreaInfo_Select");

            //添加输入输出参数
            db.AddInParameter(dbCommand, "@AreaID", DbType.Int32, areaID);

            //执行命令返回DataReader对象
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateSYAreaInfo(dr);
                else
                    return null;
            }
        }
        private static AreaInfo CreateSYAreaInfo(IDataReader rdr)
        {
            AreaInfo mySYAreaInfo = new AreaInfo();

            
            mySYAreaInfo.AreaID = DBConvert.GetInt32(rdr, "AreaID");
            mySYAreaInfo.ParentAreaID = DBConvert.GetInt32(rdr, "ParentAreaID");
            mySYAreaInfo.Language = DBConvert.GetString(rdr, "Language");
            mySYAreaInfo.PathCode = DBConvert.GetString(rdr, "PathCode");
            mySYAreaInfo.AreaCode = DBConvert.GetString(rdr, "AreaCode");
            mySYAreaInfo.AreaName = DBConvert.GetString(rdr, "AreaName");
            mySYAreaInfo.Grade = DBConvert.GetInt32(rdr, "Grade");
            mySYAreaInfo.PhoneCode = DBConvert.GetString(rdr, "PhoneCode");
            mySYAreaInfo.Comments = DBConvert.GetString(rdr, "Comments");
            mySYAreaInfo.CreateUser = DBConvert.GetInt32(rdr, "CreateUser");
            mySYAreaInfo.CreateTime = DBConvert.GetDateTime(rdr, "CreateTime");
            mySYAreaInfo.ModifyUser = DBConvert.GetInt32(rdr, "ModifyUser");
            mySYAreaInfo.ModifyTime = DBConvert.GetDateTime(rdr, "ModifyTime");

            return mySYAreaInfo;
        }

        public static IList<AreaInfo> TreeByAreaID(int areaID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("UP_SYAreaInfo_TreeByAreaID");
            db.AddInParameter(dbCommand, "@AreaID", DbType.Int32, areaID);

            var table = db.ExecuteDataSet(dbCommand).Tables[0];
            List<AreaInfo> ls = new List<AreaInfo>();
            foreach (DataRow row in table.Rows)
            {
                ls.Add(CreateSYAreaInfo(row));
            }
            return ls;

        }
        public static AreaInfo CreateSYAreaInfo(DataRow row)
        {
            var entity = new AreaInfo();
            entity.AreaID = DBConvert.GetInt32(row, "AreaID");
            entity.ParentAreaID = DBConvert.GetInt32(row, "ParentAreaID");
            entity.Language = DBConvert.GetString(row, "Language");
            entity.PathCode = DBConvert.GetString(row, "PathCode");
            entity.AreaCode = DBConvert.GetString(row, "AreaCode");
            entity.AreaName = DBConvert.GetString(row, "AreaName");
            entity.Grade = DBConvert.GetInt32(row, "Grade");
            entity.PhoneCode = DBConvert.GetString(row, "PhoneCode");
            entity.Comments = DBConvert.GetString(row, "Comments");
            entity.CreateUser = DBConvert.GetInt32(row, "CreateUser");
            entity.CreateTime = DBConvert.GetDateTime(row, "CreateTime");
            entity.ModifyUser = DBConvert.GetInt32(row, "ModifyUser");
            entity.ModifyTime = DBConvert.GetDateTime(row, "ModifyTime");
            return entity;
        }
        public static IList<AreaInfo> SelectAll()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("UP_SYAreaInfo_SelectAll");

            var table = db.ExecuteDataSet(dbCommand).Tables[0];
            List<AreaInfo> ls = new List<AreaInfo>();
            foreach (DataRow row in table.Rows)
            {
                ls.Add(CreateSYAreaInfo(row));
            }
            return ls;
        }
    }
}
