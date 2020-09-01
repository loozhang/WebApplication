using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel;
using Utility;
using DAL;

namespace BLL
{
    public class DictBLL
    {

        //Add by LEOLEE 08/29/2009,不要删
        #region 外部获取字段名称, don't remove

        /// <summary>
        /// 字典名称字段
        /// </summary>
        public const string DictTextFieldName = "DictItemName";

        /// <summary>
        /// 字典值字段
        /// </summary>
        public const string DictValueFieldName = "DictStringValue";

        private static readonly string typeName = typeof(DictBLL).FullName;

        #endregion 外部获取字段名称

        /// <summary>
        /// 根据字典类型查询
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <param name="isCache">是否用加缓存</param>
        /// <returns>包含字典类型名称的字典信息</returns>
        public static DataTable SelectByDictType(string dictType, bool isCache)
        {
            if (isCache)
            {
                string key = typeName + "SelectByDictType" + dictType;
                if (CacheHelper.CacheDefault.KeyExists(key))
                {
                    return (DataTable)CacheHelper.CacheDefault.Get(key);
                }
                else
                {
                    var data = SelectByDictType(dictType);
                    CacheHelper.CacheDefault.Set(key, data);
                    return data;
                }
            }
            else
            {
                return SelectByDictType(dictType);
            }
        }


        /// <summary>
        /// 根据字典类型查询
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns>包含字典类型名称的字典信息</returns>
        public static DataTable SelectByDictType(string dictType)
        {
            return SelectByDictType(dictType, DictStatusEnum.Normal, DictTypeStatusEnum.Normal);
        }

        /// <summary>
        /// 根据字典类型查询
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <param name="dictStatus">字典状态</param>
        /// <param name="dictTypeStatus">字典类型状态</param>
        /// <returns>包含字典类型名称的字典信息</returns>
        public static DataTable SelectByDictType(string dictType, DictStatusEnum dictStatus, DictTypeStatusEnum dictTypeStatus)
        {
            return DictDAL.SelectByDictType(dictType, dictStatus, dictTypeStatus);
        }



        /// <summary>
        /// 根据字典类型查询
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <param name="isCache">是否用加缓存</param>
        /// <returns>包含字典类型名称的字典信息</returns>
        public static List<DictInfo> SelectByDictTypeSimple(string dictType, bool isCache)
        {
            if (isCache)
            {
                string key = typeName + "SelectByDictTypeSimple" + dictType;
                if (CacheHelper.CacheDefault.KeyExists(key))
                {
                    return (List<DictInfo>)CacheHelper.CacheDefault.Get(key);
                }
                else
                {
                    var data = SelectByDictTypeSimple(dictType);
                    CacheHelper.CacheDefault.Set(key, data);
                    return data;
                }
            }
            else
            {
                return SelectByDictTypeSimple(dictType);
            }
        }

        /// <summary>
        /// 根据DictType,DictStringValue字段获取SYDictInfo实体对象
        /// </summary>
        /// <param name="dictType">数据字典类型</param>
        /// <param name="dictStringValue">数据字典项字符串值</param>
        /// <param name="isCache">是否用加缓存</param>
        /// <returns>SYDictInfo实体对象</returns>
        public static DictInfo GetInfo(string dictType, string dictStringValue, bool isCache)
        {
            if (isCache)
            {
                var arr = SelectByDictTypeSimple(dictType, true);
                return (from x in arr where x.DictStringValue == dictStringValue select x).FirstOrDefault();
            }
            else
            {
                return GetInfo(dictType, dictStringValue);
            }
        }

        /// <summary>
        /// 根据DictType,DictStringValue字段获取SYDictInfo实体对象名称
        /// </summary>
        /// <param name="dictType">数据字典类型</param>
        /// <param name="dictStringValue">数据字典项字符串值</param>
        /// <param name="isCache">是否用加缓存</param>
        /// <returns>GetItemName实体对象名称</returns>
        public static string GetItemName(string dictType, string dictStringValue, bool isCache)
        {
            var entity = GetInfo(dictType, dictStringValue, isCache);
            return entity == null ? "" : entity.DictItemName;
        }

        /// <summary>
        /// 根据字典类型查询
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns>包含字典类型名称的字典信息</returns>
        public static List<DictInfo> SelectByDictTypeSimple(string dictType)
        {
            DataTable table = SelectByDictType(dictType);

            List<DictInfo> ls = new List<DictInfo>();
            foreach (DataRow row in table.Rows)
            {
                ls.Add(DictDAL.CreateSYDictInfo(row));
            }
            return ls;
        }

        /// <summary>
        /// 检查某字典类型的字典名称是否存在
        /// </summary>
        /// <param name="dictItemName">字典名称</param>
        /// <param name="dictType">字典类型</param>
        /// <returns>是否存在</returns>
        public static bool CheckName(string dictItemName, string dictType)
        {
            return DictDAL.CheckName(dictItemName, dictType);
        }

        /// <summary>
        /// 检查某字典类型的字典代码是否存在
        /// </summary>
        /// <param name="dictItemCode">字典名称</param>
        /// <param name="dictType">字典类型</param>
        /// <returns>是否存在</returns>
        public static bool CheckCode(string dictItemCode, string dictType)
        {
            return DictDAL.CheckCode(dictItemCode, dictType);
        }

        /// <summary>
        /// 检查某字典类型的字典值否存在
        /// </summary>
        /// <param name="dictStringValue">字典值</param>
        /// <param name="dictType">字典类型</param>
        /// <returns>是否存在</returns>
        public static bool CheckValue(string dictStringValue, string dictType)
        {
            return DictDAL.CheckValue(dictStringValue, dictType);
        }

        /// <summary>
        /// 状态
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// 可用
            /// </summary>
            Enable = 0,
            /// <summary>
            /// 不可用
            /// </summary>
            Disable = 1
        }

        /// <summary>
        /// 获获取用分割符分割多个字典ID的名称
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <param name="strIDs">用分割符分割多个字典ID</param>
        /// <param name="splitID">分割符</param>
        /// <param name="splitName">显示分割符</param>
        /// <returns>名称</returns>
        public static string GetStringDictNames(string dictType, string strIDs, string splitID, string splitName)
        {
            var arr = GetArrByStrIDs(strIDs, splitID);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                sb.Append(GetItemName(dictType, arr[i], true));
                if (i < arr.Length - 1)
                {
                    sb.Append(splitName);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取相关字典IDs
        /// </summary>
        /// <param name="strIDs">相关字典IDs</param>
        /// <param name="splitID">分割符</param>
        /// <returns>ID数组</returns>
        public static string[] GetArrByStrIDs(string strIDs, string splitID)
        {
            var strs = strIDs.Split(new string[] { splitID }, StringSplitOptions.RemoveEmptyEntries);
            List<string> ls = new List<string>();
            Array.ForEach(strs, ls.Add);
            return ls.ToArray();
        }
        public static DictInfo GetInfo(string dictType, string dictStringValue)
        {
                return DictDAL.GetInfo(dictType, dictStringValue);

        }

    }
}
