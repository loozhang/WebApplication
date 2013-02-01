using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DataModel;
using BLL;
using Common;

namespace BLL
{
    public class AreaBLL
    {
        private static readonly string typeName = typeof(DictBLL).FullName;

        /// <summary>
        /// 区域显示字段
        /// </summary>
        public static readonly string AreaTextFieldName = "AreaName";

        /// <summary>
        /// 区域值字段
        /// </summary>
        public const string AreaValueFieldName = "AreaID";
        public static AreaInfo GetInfo(int areaID)
        {
                return AreaDAL.GetInfo(areaID);
        }
        public static bool IsFatherOrUp(int fatherID, int sonID)
        {
            var father = GetInfo(fatherID, true);
            var son = GetInfo(sonID, true);
            if (son.ParentAreaID == father.AreaID)
                return true;
            if (son.PathCode.IndexOf(father.PathCode) > -1)
            {
                var arrson = son.PathCode.Split('.');
                var arrfat = father.PathCode.Split('.');
                if (arrson.Length < arrfat.Length)
                {
                    return false;
                }
                for (int i = 0; i < arrfat.Length; i++)
                {
                    if (arrfat[i] != arrson[i])
                        return false;
                }
                return true;
            }
            return false;
        }
        public static AreaInfo GetInfo(int areaID, bool isCache)
        {
            if (isCache)
            {
                var areas = SelectAll(isCache);
                var arr = from x in areas where x.AreaID == areaID select x;
                if (arr.Count() == 0)
                    return null;
                else
                    return arr.First();
            }
            else
            {
                return GetInfo(areaID);
            }
        }

        /// <summary>
        /// 根据区域代码ID获取子区域信息
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <returns>区域列表信息</returns>
        public static IList<AreaInfo> TreeByAreaID(int areaID)
        {

            return AreaDAL.TreeByAreaID(areaID);
        }
        public static IList<AreaInfo> SelectAll(bool isCache)
        {
            if (isCache)
            {
                try
                {
                    string key = typeName + "SelectAll";

                    if (CacheHelper.CacheDefault.KeyExists(key))
                    {
                        return (IList<AreaInfo>)CacheHelper.CacheDefault.Get(key);
                    }
                    else
                    {
                        var data = SelectAll();
                        CacheHelper.CacheDefault.Set(key, data);
                        return data;
                    }
                }
                catch
                {
                    return SelectAll();
                }
            }
            else
            {
                return SelectAll();
            }
        }
        public static IList<AreaInfo> SelectAll()
        {
            return AreaDAL.SelectAll();
        }

        /// <summary>
        /// 根据区域编号获取区域对象名称
        /// </summary>
        /// <param name="areaID">区域编号</param>
        /// <param name="isCache">是否用加缓存</param>
        /// <returns>区域对象名称</returns>
        public static string GetAreaName(int areaID, bool isCache)
        {
            var entity = GetInfo(areaID, isCache);
            return entity == null ? "" : entity.AreaName;
        }
    }
}
