using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common
{
    public class DBConvert
    {
        #region 转化为数据库中值

        /// <summary>
        /// 功能现修改为：
        /// 将日期型转换成有效的数据库字段值
        /// 范围在System.Data.SqlTypes.SqlDateTime.MinValue和System.Data.SqlTypes.SqlDateTime.MaxValue之间
        /// 返回类型为其相应的DateTime值
        /// 
        /// 旧版功能：
        /// 将日期型转换成数据库字段值
        /// 如果time值为time.MinValue，则转换为DBNull.Value
        /// </summary>
        /// <param name="time">要转换的日期</param>
        /// <returns></returns>
        public static Object ToDBValue(DateTime time)
        {
            //if (time == DateTime.MinValue)
            //    return DBNull.Value;

            DateTime minTime = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime maxTime = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

            if (time == DateTime.MinValue)
                return minTime;

            if (time <= minTime)
                return minTime;

            if (time >= maxTime)
                return maxTime;

            return time;
        }

        /// <summary>
        /// 将Boolean型转换成数据库字段值
        /// </summary>
        /// <param name="value">要转换的Boolean值</param>
        /// <returns>Object</returns>
        public static Object ToDBValue(Boolean value)
        {
            //if (value == null)
            //    return false;
            //else
            return value;
        }

        /// <summary>
        /// 将Decimal型转换成数据库字段值
        /// </summary>
        /// <param name="value">要转换的decimal值</param>
        /// <returns>Object</returns>
        public static Object ToDBValue(decimal value)
        {
            if (value == decimal.MinValue)
                return DBNull.Value;
            else
                return value;
        }

        /// <summary>
        /// 将Double型转换成数据库字段值
        /// </summary>
        /// <param name="value">要转换的Double值</param>
        /// <returns>Object</returns>
        public static Object ToDBValue(double value)
        {
            if (value == Double.MinValue)
                return DBNull.Value;
            else
                return value;
        }

        /// <summary>
        /// 将string型转换成数据库字段值
        /// </summary>
        /// <param name="value">要转换的string值</param>
        /// <returns>Object</returns>
        public static Object ToDBValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                //return DBNull.Value;
                return String.Empty;
            }
            return FilterSQLStr(value);
        }


        /// <summary>
        /// 过滤SQL注入字段
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        /// <remarks>/Add By LiZhao 11/07/2009</remarks>
        private static string FilterSQLStr(string strSQL)
        {
            strSQL = strSQL.Replace("'", "''");
            return strSQL;
        }


        /// <summary>
        /// 将Int32型转换成数据库字段值
        /// </summary>
        /// <param name="value">要转换的int值</param>
        /// <returns>Object</returns>
        public static Object ToDBValue(int value)
        {
            if (value == int.MinValue)
                return DBNull.Value;
            else
                return value;
        }

        #endregion 转化为数据库中值

        /// <summary>
        /// 将数据库中的日期字段转换为DateTime,如果dbValue为DBNull.Value,则返回DateTime.MinValue;
        /// </summary>
        /// <param name="dbValue">要转换的数据库字段值</param>
        /// <returns>日期</returns>
        public static DateTime ToDateTime(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            }

            //避免null.ToString()
            if (dbValue.ToString() == string.Empty)
            {
                return (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            }

            return Convert.ToDateTime(dbValue);
        }

        /// <summary>
        /// 将数据库中的Bool字段转换为Bool类型,如果dbValue为DBNull.Value,则返回False;
        /// </summary>
        /// <param name="dbValue">要转换的数据库字段值</param>
        /// <returns>日期</returns>
        public static bool ToBoolean(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
                return false;
            else if (dbValue.ToString() == string.Empty)
                return false;
            else
                return Convert.ToBoolean(dbValue);
        }


        /// <summary>
        /// 将整型的数据库字段值转换为System.Int32，如果值为DBNull.Value,则转换为 -1
        /// </summary>
        /// <param name="dbValue">整型的数据库值</param>
        /// <returns></returns>
        public static int ToInt32(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
                return Int32.MinValue;
            else if (dbValue.ToString() == string.Empty)
                return Int32.MinValue;
            else
                return Convert.ToInt32(dbValue);
        }

        /// <summary>
        ///  将整型的数据库字段值转换为System.Int64，如果值为DBNull.Value,则转换为 -1
        /// </summary>
        /// <param name="dbValue">整型的数据库值</param>
        /// <returns></returns>
        public static Int64 ToInt64(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
                return Int64.MinValue;
            else
                return Convert.ToInt64(dbValue);
        }

        /// <summary>
        /// 直接从dataReader中读取第i列的日期值，如果值为空，则返回DateTime.MinValue
        /// </summary>
        /// <param name="dataReader">要从中读取数据的dataReader</param>
        /// <param name="i">dataReader中的列索引</param>
        /// <returns></returns>
        public static DateTime GetDateTime(IDataReader dataReader, int i)
        {
            if (dataReader.IsDBNull(i))
                return (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            else
                return dataReader.GetDateTime(i);
        }

        /// <summary>
        /// 直接从dataReader中读取字段名为field的值，如果值为空，则返回DateTime.MinValue
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static DateTime GetDateTime(IDataReader dataReader, string field)
        {
            if (dataReader[field] == DBNull.Value)
                return (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            else
                return Convert.ToDateTime(dataReader[field]);
        }

        /// <summary>
        /// 直接从dataReader中读取第i列的值,如果值为空，则返回Int.MinValue
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="i">dataReader中的列索引</param>
        /// <returns></returns>
        public static int GetInt32(IDataReader dataReader, int i)
        {
            if (dataReader.IsDBNull(i))
                return int.MinValue;
            else
                return dataReader.GetInt32(i);
        }

        /// <summary>
        /// 直接从dataReader中读取第i列的值,如果值为空，则返回Int.MinValue
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="i">dataReader中的列索引</param>
        /// <returns></returns>
        public static double GetDouble(IDataReader dataReader, int i)
        {
            if (dataReader.IsDBNull(i))
                return double.MinValue;
            else
                return dataReader.GetDouble(i);
        }

        /// <summary>
        /// 直接从dataReader中读取第i列的值,如果值为空，则返回Int.MinValue
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="field">dataReader中的列</param>
        /// <returns></returns>
        public static double GetDouble(IDataReader dataReader, string field)
        {
            if (dataReader[field] == DBNull.Value)
            {
                return double.MinValue;
            }

            return Convert.ToDouble(dataReader[field]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static decimal GetDecimal(IDataReader dataReader, int i)
        {
            if (dataReader.IsDBNull(i))
            {
                return decimal.MinValue;
            }
            return dataReader.GetDecimal(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static decimal GetDecimal(IDataReader dataReader, string field)
        {
            if (dataReader[field] == DBNull.Value)
            {
                return decimal.MinValue;
            }

            //return (int)dataReader[field];
            return Convert.ToDecimal(dataReader[field]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static decimal GetCurrency(IDataReader dataReader, string field)
        {
            if (dataReader[field] == DBNull.Value)
            {
                return decimal.MinValue;
            }

            //return (int)dataReader[field];
            return Convert.ToDecimal(dataReader[field]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static byte GetByte(IDataReader dataReader, string field)
        {
            if (dataReader[field] == DBNull.Value)
            {
                return byte.MinValue;
            }

            //return (int)dataReader[field];
            return Convert.ToByte(dataReader[field]);
        }

        /// <summary>
        /// 直接从dataReader中读取字段名为field的值，如果值为空，则返回Int.MinValue
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static Int16 GetInt16(IDataReader dataReader, string field)
        {
            if (dataReader[field] == DBNull.Value)
            {
                return Int16.MinValue;
            }

            //return (int)dataReader[field];
            return Convert.ToInt16(dataReader[field]);
        }

        /// <summary>
        /// 直接从dataReader中读取字段名为field的值，如果值为空，则返回Int.MinValue
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static int GetInt32(IDataReader dataReader, string field)
        {
            if (dataReader[field] == DBNull.Value)
            {
                return int.MinValue;
            }

            //return (int)dataReader[field];
            return Convert.ToInt32(dataReader[field]);
        }

        /// <summary>
        /// 直接从dataReader中读取第i列的值,如果值为空，则返回空串
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="i">dataReader中的列索引</param>
        /// <returns></returns>
        public static string GetString(IDataReader dataReader, int i)
        {
                if (dataReader.IsDBNull(i))
                {
                    return String.Empty;
                }
                else
                {
                    return dataReader.GetString(i);
                }
        }

        /// <summary>
        /// 直接从dataReader中读取字段名为field的值，如果值为空，则返回空串
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static string GetString(IDataReader dataReader, string field)
        {
            try
            {
                if (dataReader[field] == DBNull.Value)
                {
                    return String.Empty;
                }
                else
                {
                    //return dataReader[field].ToString();
                    return Convert.ToString(dataReader[field]);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 直接从dataReader中读取字段名为field的值，如果值为空，则返回空串
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static string GetAnsiString(IDataReader dataReader, string field)
        {
            //todo:完成ansi字符处理。目前直接读取utf-8字符串
            return GetString(dataReader, field);
        }

        /// <summary>
        /// 直接从dataReader中读取字段名为field的值，如果值为空，则返回空串
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static string GetXml(IDataReader dataReader, string field)
        {
            //to do:完成ansi字符处理。目前直接读取utf-8字符串
            return GetString(dataReader, field);
        }
        /// <summary>
        /// 格式化字符串，防注入式攻击
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatString(string str)
        {
            if (!String.IsNullOrEmpty(str))
                return str.Replace("'", "").Replace("-", "").Replace(";", "");
            else
                return String.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatStringGL(string str)
        {
            return FormatString(str);
        }


        /// <summary>
        /// 直接从row中读取第i列的日期值，如果值为空，则返回DateTime.MinValue
        /// </summary>
        /// <param name="row">要从中读取数据的row</param>
        /// <param name="i">row中的列索引</param>
        /// <returns></returns>
        public static DateTime GetDateTime(DataRow row, int i)
        {
            if (row.IsNull(i))
            {
                return (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            }

            //return (DateTime)row[i];
            return Convert.ToDateTime(row[i]);
        }

        /// <summary>
        /// 直接从row中读取字段名为field的值，如果值为空，则返回DateTime.MinValue
        /// </summary>
        /// <param name="row">要读取数据的row</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static DateTime GetDateTime(DataRow row, string field)
        {
            if (row.Table.Columns.Contains(field) == false || row[field] == DBNull.Value)
            {
                return (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            }

            //return (DateTime)row[field];
            return Convert.ToDateTime(row[field]);
        }


        /// <summary>
        /// 直接从row中读取第i列的值,如果值为空，则返回Int.MinValue
        /// </summary>
        /// <param name="row">要读取数据的row</param>
        /// <param name="i">row中的列索引</param>
        /// <returns></returns>
        public static int GetInt32(DataRow row, int i)
        {
            if (row.IsNull(i))
            {
                return int.MinValue;
            }

            //return (int)row[i];
            return Convert.ToInt32(row[i]);
        }

        /// <summary>
        /// 直接从row中读取第i列的值,如果值为空，则返回Int.MinValue
        /// </summary>
        /// <param name="row">要读取数据的row</param>
        /// <param name="i">row中的列索引</param>
        /// <returns>值</returns>
        public static double GetDouble(DataRow row, int i)
        {
            if (row.IsNull(i))
            {
                return double.MinValue;
            }

            //return (double)row[i];
            return Convert.ToDouble(row[i]);
        }

        /// <summary>
        /// 直接从row中读取第i列的值,如果值为空，则返回decimal.MinValue
        /// </summary>
        /// <param name="row">要读取数据的row</param>
        /// <param name="i">row中的列索引</param>
        /// <returns>值</returns>
        public static decimal GetDecimal(DataRow row, int i)
        {
            if (row.IsNull(i))
            {
                return decimal.MinValue;
            }

            //return (decimal)row[i];
            return Convert.ToDecimal(row[i]);
        }

        /// <summary>
        /// 直接从row中读取某字段的值,如果值为空，则返回double.MinValue
        /// </summary>
        /// <param name="row">要读取数据的row</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        public static double GetDouble(DataRow row, string field)
        {
            if (row.Table.Columns.Contains(field) == false || row[field] == DBNull.Value)
            {
                return double.MinValue;
            }

            //return (double)row[field];
            return Convert.ToDouble(row[field]);
        }

        /// <summary>
        /// 读取row中字段field的值，若为空，则返回decimal.MinValue
        /// </summary>
        /// <param name="row">要去数据的row</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        public static decimal GetDecimal(DataRow row, string field)
        {
            if (row.Table.Columns.Contains(field) == false || row[field] == DBNull.Value)
            {
                return decimal.MinValue;
            }
            //return (decimal)row[field];
            return Convert.ToDecimal(row[field]);
        }

        /// <summary>
        /// 直接从row中读取字段名为field的值，如果值为空，则返回Int.MinValue
        /// </summary>
        /// <param name="row">要读取数据的row</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static int GetInt32(DataRow row, string field)
        {
            if (row.Table.Columns.Contains(field) == false || row[field] == DBNull.Value)
            {
                return int.MinValue;
            }
            //return (int)row[field];
            return Convert.ToInt32(row[field]);
        }

        /// <summary>
        /// 直接从row中读取第i列的值,如果值为空，则返回空串
        /// </summary>
        /// <param name="row">要读取数据的row</param>
        /// <param name="i">row中的列索引</param>
        /// <returns></returns>
        public static string GetString(DataRow row, int i)
        {
            if (row.IsNull(i))
            {
                return String.Empty;
            }
            else
            {
                //return row[i].ToString();
                return Convert.ToString(row[i]);
            }
        }

        /// <summary>
        /// 直接从row中读取字段名为field的值，如果值为空，则返回空串
        /// </summary>
        /// <param name="row">要读取数据的row</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static string GetString(DataRow row, string field)
        {
            if (row.Table.Columns.Contains(field) == false || row[field] == DBNull.Value)
            {
                return String.Empty;
            }
            else
            {
                //return row[field].ToString();
                return Convert.ToString(row[field]);
            }
        }

        /// <summary>
        /// 直接从dataReader中读取字段名为field的值，如果值为空，则返回false
        /// </summary>
        /// <param name="row">要读取数据的dataReader</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static bool GetBoolean(DataRow row, string field)
        {
            if (row.Table.Columns.Contains(field) == false || row[field] == DBNull.Value)
            {
                return false;
            }
            else
            {
                //return row[field].ToString();
                return Convert.ToBoolean(row[field]);
            }
        }


        //zuoliangbin 2009-9-9
        /// <summary>
        /// 直接从dataReader中读取字段名为field的值，如果值为空，则返回false
        /// </summary>
        /// <param name="dataReader">要读取数据的dataReader</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static bool GetBoolean(IDataReader dataReader, string field)
        {
            if (dataReader[field] == DBNull.Value)
            {
                return false;
            }

            //return (bool)dataReader[field];
            return Convert.ToBoolean(dataReader[field]);
        }

        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        //public static ADCList<TResult> ToList<TResult>(this DataTable dt) where TResult : class, new()
        //{
        //    //创建一个属性的列表
        //    ADCList<PropertyInfo> prlist = new ADCList<PropertyInfo>();
        //    //获取TResult的类型实例  反射的入口
        //    Type t = typeof(TResult);
        //    //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
        //    Array.ForEach(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
        //    //创建返回的集合
        //    ADCList<TResult> oblist = new ADCList<TResult>();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        //创建TResult的实例
        //        TResult ob = new TResult();
        //        //找到对应的数据  并赋值
        //        DataRow row1 = row;
        //        prlist.ForEach(p => { if (row1[p.Name] != DBNull.Value) p.SetValue(ob, row1[p.Name], null); });
        //        //放入到返回的集合中.
        //        oblist.Add(ob);
        //    }
        //    return oblist;
        //}

        /// <summary>
        /// 从数据库中取Int32?
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? GetInt32(object obj)
        {
            if (obj != DBNull.Value)
            {
                return (int?)obj;
            }
            return null;
        }

        /// <summary>
        /// 从数据库中取DateTime?
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? GetDateTime(object obj)
        {
            if (obj != DBNull.Value)
            {
                return (DateTime?)obj;
            }
            return null;
        }

        /// <summary>
        /// 从数据库中取Int64?
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int64? GetInt64(object obj)
        {
            if (obj != DBNull.Value)
            {
                return (Int64?)obj;
            }
            return null;
        }

        /// <summary>
        /// 直接从dataReader中读取第i列的日期值，如果值为空，则返回DateTime.MinValue
        /// </summary>
        /// <param name="dataReader">要从中读取数据的dataReader</param>
        /// <param name="field">dataReader中的列索引</param>
        /// <returns></returns>
        public static Int64 GetInt64(IDataReader dataReader, string field)
        {
            if (dataReader[field] == DBNull.Value)
            {
                return Int64.MinValue;
            }
            return Convert.ToInt64(dataReader[field]);
        }

        /// <summary>
        /// 从数据库中取String
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetString(object obj)
        {
            return obj != DBNull.Value ? obj.ToString() : null;
        }

        /// <summary>
        /// 从数据库中去Decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal? GetDecimal(object obj)
        {
            if (obj != DBNull.Value)
            {
                return (decimal?)obj;
            }
            return null;
        }
    }
}
