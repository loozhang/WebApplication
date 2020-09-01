using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BLL;
using Common;
using DataModel;
using System.Transactions;
using System.Data.SqlTypes;
using System.Web;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// 登录用
/// </summary>
public class ECCommon
{

    #region CookieOP

    public static bool SetCookieValue(string key, string value)
    {
        return SetCookieValue(key, value, false);
    }
    public static bool SetCookieValue(string key, string value, bool allApp)
    {
        return SetCookieValue(key, value, 0, allApp);
    }
    public static bool SetCookieValue(string key, string value, int outTime)
    {
        return SetCookieValue(key, value, outTime, false);
    }
    public static bool SetCookieValue(string key, string value, int outTime, bool allApp)
    {
        try
        {
            value = CryptTools.EncryptDESPassword(value);
            value = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
            SetCookie(key, value, outTime, allApp);
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    public static bool SetCookieValue(string key, object o)
    {
        return SetCookieValue(key, o, false);
    }
    public static bool SetCookieValue(string key, object o, bool allApp)
    {
        return SetCookieValue(key, o, 0, allApp);
    }
    public static bool SetCookieValue(string key, object o, int outTime, bool allApp)
    {
        try
        {
            byte[] obj = Serialize(o);
            byte[] tmp = CryptTools.EncryptDESPassword(obj);
            string value = Convert.ToBase64String(tmp);
            SetCookie(key, value, outTime, allApp);
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    private static bool SetCookie(string key, string value, int outTime, bool allApp)
    {
        try
        {
            if (HttpContext.Current.Response.Cookies[key] != null)
            {
                HttpContext.Current.Response.Cookies.Remove(key);
            }

            HttpCookie aCookie = new HttpCookie(key);
            aCookie.Value = value;
            if (outTime > 0)
            {
                aCookie.Expires = DateTime.Now.AddMinutes(outTime);
            }
            if (allApp)
            {
                aCookie.Path = "/";
            }
            HttpContext.Current.Response.Cookies.Add(aCookie);
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    public static object GetCookieObject(string key)
    {
        try
        {
            string value = string.Empty;
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                value = System.Web.HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.Cookies[key].Value);

                if (string.IsNullOrEmpty(value))
                {
                    return null;
                }

                byte[] base64 = Convert.FromBase64String(value);

                byte[] obj = CryptTools.DecryptDESPassword(base64);
                return Deserialize(obj);
            }
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static string GetCookieValue(string key)
    {
        try
        {
            string value = string.Empty;
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                value = System.Web.HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.Cookies[key].Value);
            }
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            byte[] tmp = Convert.FromBase64String(value);
            value = Encoding.UTF8.GetString(tmp);
            value = CryptTools.DecryptDESPassword(value);

            return value;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    public static void RemoveCookie(string key)
    {
        try
        {
            string value = string.Empty;
            if (HttpContext.Current.Response.Cookies[key] != null)
            {
                HttpContext.Current.Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
            }
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary> 
    /// 序列化 
    /// </summary> 
    /// <param name="data">要序列化的对象</param> 
    /// <returns>返回存放序列化后的数据缓冲区</returns> 
    public static byte[] Serialize(object data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream rems = new MemoryStream();
        formatter.Serialize(rems, data);
        return rems.GetBuffer();
    }

    /// <summary> 
    /// 反序列化 
    /// </summary> 
    /// <param name="data">数据缓冲区</param> 
    /// <returns>对象</returns> 
    public static object Deserialize(byte[] data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream rems = new MemoryStream(data);
        data = null;
        return formatter.Deserialize(rems);
    }


    #endregion

    public static string GetServerPath()
    {
        try
        {
            string mappingServerAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["MappingServerAddress"];
            string mappingServerPath = System.Web.Configuration.WebConfigurationManager.AppSettings["MappingServerPath"];

            if (string.IsNullOrEmpty(mappingServerAddress))
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(mappingServerPath))
            {
                return string.Empty;
            }

            string[] mappingServerAddresss = mappingServerAddress.Split(';');
            string[] mappingServerPaths = mappingServerPath.Split(';');

            string host = HttpContext.Current.Request.UserHostAddress;
            if (mappingServerAddress.Contains(host))
            {//映射过来的地址
                int index = 0;
                for (int i = 0; i < mappingServerAddresss.Length; i++)
                {
                    if (mappingServerAddresss[i] == host)
                    {
                        index = i;
                        break;
                    }
                }

                if (mappingServerPaths.Length < (index - 1))
                {
                    return string.Empty;
                }

                return mappingServerPaths[index].TrimEnd('/');
            }
            else
            {
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }


    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="length">截取的长度</param>
    /// <param name="lastStr">截取后，字符串尾部的字符串</param>
    /// <returns></returns>
    public static string SubString(string str, int length, string lastStr)
    {
        if (str == null)
            return String.Empty;

        return str.Length > length ? str.Substring(0, length) + lastStr : str;
    }

    /// <summary>
    /// 获取区域名称
    /// </summary>
    /// <param name="areaID"></param>
    /// <returns></returns>
    //internal static string GetAreaValue(int areaID)
    //{
    //    try
    //    {
    //        AreaInfo info = AreaBLL.GetAreaInfo(areaID);
    //        if (info == null)
    //            return string.Empty;

    //        return info.AreaName;
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "GetAreaValue", AppError.EROR, 0, ex);
    //        return "";
    //    }
    //}

    /// <summary>
    /// 获取行业类型
    /// </summary>
    /// <param name="industoryType"></param>
    /// <returns></returns>
    //internal static string GetIndustoryTypeValue(string industoryType)
    //{
    //    try
    //    {
    //        return DictBLL.GetItemName(DictTypes.IndustoryType, industoryType.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "GetIndustoryTypeValue", AppError.EROR, 0, ex);
    //        return "";
    //    }
    //}

    /// <summary>
    /// 获取审批状态描述
    /// </summary>
    /// <param name="approvalResult"></param>
    /// <returns></returns>
    //internal static string GetApprovalResultValue(int approvalResult)
    //{
    //    try
    //    {
    //        return SYDictBLL.GetItemName(DictTypes.ApprovalResult, approvalResult.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "GetApprovalResultValue", AppError.EROR, 0, ex);
    //        return "";
    //    }
    //}

    /// <summary>
    /// 获取提交状态
    /// </summary>
    /// <param name="submitStatus"></param>
    /// <returns></returns>
    //internal static string GetSubmitStatusValue(int submitStatus)
    //{
    //    try
    //    {
    //        return SYDictBLL.GetItemName(DictTypes.SubmitStatus, submitStatus.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "GetSubmitStatusValue", AppError.EROR, 0, ex);
    //        return "";
    //    }
    //}

    /// <summary>
    /// 获取同步描述
    /// </summary>
    /// <param name="sync"></param>
    /// <returns></returns>
    //internal static string GetSyncValue(int sync)
    //{
    //    try
    //    {
    //        return SYDictBLL.GetItemName(DictTypes.SyncFlag, sync.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "GetSyncValue", AppError.EROR, 0, ex);
    //        return "";
    //    }
    //}

    /// <summary>
    /// 申请来源描述
    /// </summary>
    /// <param name="applySource"></param>
    /// <returns></returns>
    //internal static string GetApplySourceValue(int applySource)
    //{
    //    try
    //    {
    //        return SYDictBLL.GetItemName(DictTypes.ApplySource, applySource.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "GetApplySourceValue", AppError.EROR, 0, ex);
    //        return "";
    //    }
    //}

    internal static string GetApplyStatusValue(int p)
    {
            // TODO something
            //return SYDictBLL.GetItemName(DictTypes., applySource.ToString(), true);
            return "todo";
    }

    /// <summary>
    /// 申请类型
    /// </summary>
    /// <param name="opreationType"></param>
    /// <returns></returns>
    //internal static string GetOpreationTypeValue(int opreationType)
    //{
    //    try
    //    {
    //        return SYDictBLL.GetItemName(DictTypes.OperationType, opreationType.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "GetOpreationTypeValue", AppError.EROR, 0, ex);
    //        return "";
    //    }
    //}


    //public static string UpLoadFile(FileUpload fileUploadControl, out string fileName, string userName)
    //{
    //    return UpLoadFile(fileUploadControl, out fileName, userName, "EC/DownLoad");
    //}
    //public static string UpLoadFile(FileUpload fileUploadControl, out string fileName, string userName, string path)
    //{
    //    fileName = string.Empty;
    //    string filePath = string.Empty;


    //    if (fileUploadControl.HasFile)
    //    {
    //        //todo 处理文件同名的情况

    //        fileName = System.IO.Path.GetFileName(fileUploadControl.PostedFile.FileName);

    //        AttachmentUtility attch = new AttachmentUtility(userName);

    //        //上传附件并获取附件的分类路径，该路径应该保存至附件数据库表中
    //        filePath = attch.SaveAttach(fileUploadControl.PostedFile, path, fileName);
    //    }

    //    return filePath;
    //}

    //internal static string ContactPersionTypeValue(string type)
    //{
    //    try
    //    {
    //        string tmp = SYDictBLL.GetItemName(DictTypes.EC_ContactType, type, true);
    //        if (string.IsNullOrEmpty(tmp))
    //        {
    //            List<SYDictInfo> list = SYDictBLL.SelectByDictTypeSimple(DictTypes.VAR_ContactType);

    //            foreach (SYDictInfo info in list)
    //            {
    //                if (info.DictStringValue == type)
    //                    return info.DictItemName;
    //            }
    //            return string.Empty;
    //        }
    //        else
    //        {
    //            return tmp;
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "ContactPersionTypeValue", AppError.EROR, 0, ex);
    //        return string.Empty;
    //    }
    //}


    //internal static string GetEntTypeValue(string entType)
    //{
    //    try
    //    {
    //        return SYDictBLL.GetItemName(DictTypes.EC_EntType, entType, true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "GetEntTypeValue", AppError.EROR, 0, ex);
    //        return "";
    //    }

    //}


    //public static string GetAttachmentFullUrl(string userName, string path)
    //{
    //    try
    //    {
    //        AttachmentUtility attch = new AttachmentUtility(userName);
    //        return attch.GetAttachmentFullUrl(path);
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "GetAttachmentFullUrl", AppError.EROR, 0, ex);
    //        return string.Empty;
    //    }
    //}
    public static TransactionOptions GetTransactionOptions()
    {
        TransactionOptions transactionOptions = new TransactionOptions();
            switch (ConfigurationManager.AppSettings["IsolationLevel"].ToString())
            {
                case "Chaos":
                    transactionOptions.IsolationLevel = IsolationLevel.Chaos;
                    break;
                case "ReadCommitted":
                    transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
                    break;
                case "ReadUncommitted":
                    transactionOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
                    break;
                case "RepeatableRead":
                    transactionOptions.IsolationLevel = IsolationLevel.RepeatableRead;
                    break;
                case "Serializable":
                    transactionOptions.IsolationLevel = IsolationLevel.Serializable;
                    break;
                case "Snapshot":
                    transactionOptions.IsolationLevel = IsolationLevel.Snapshot;
                    break;
                case "Unspecified":
                    transactionOptions.IsolationLevel = IsolationLevel.Unspecified;
                    break;
                default:
                    transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
                    break;
            }
        return transactionOptions;
    }
    public static string ReplaceStr(string str)
    {
        if (str == null)
            return string.Empty;
        return str.Trim().Replace("'", "''");
    }


    /// <summary>
    /// 是管理员或未审核管理员
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    //public static bool IsALLAdmin(ECUserInfo userInfo)
    //{

    //    if (userInfo.IsSuperAdmin)
    //        return true;

    //    int[] curUserRights = (int[])UARoleBLL.SelectIntsByUserIDUserType(userInfo.UserID, UserType.EC);

    //    if (CheckContains(curUserRights, RoleIDHelper.RoleID_UnAudit_EC))
    //    {
    //        return true;
    //    }

    //    return false;
    //}
    private static bool CheckContains(int[] curUserRights, int value)
    {
        foreach (int item in curUserRights)
        {
            if (item == value)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 检测某个产品的允许关联数和已关联数限制
    /// </summary>
    /// <param name="ecid"></param>
    /// <param name="productID"></param>
    /// <returns></returns>
    //public static string CheckECCanUserProduct(int ecid, int productID, bool isAdmin)
    //{
    //    try
    //    {
    //        int licenseCount = ECSubscriptionAuthenticationBLL.GetCurTimeProductLicenseCount(ecid, productID);

    //        SIProductBasicInfo siProductBasicInfo = ProductBasicBLL.GetProductBasicInfo(productID);
    //        bool isBBC = false;
    //        if (siProductBasicInfo.AppMode == "B-B-C")
    //        {
    //            isBBC = true;
    //        }
    //        bool isUsed = true;
    //        ADCList<ECProductUsedInfo> productUserList = new ADCList<ECProductUsedInfo>();
    //        int recordCount = 0;
    //        productUserList = ECUserProductBLL.GetNoProductUsedListByPages(
    //                                                ecid,
    //                                                -1,
    //                                                string.Empty,
    //                                                string.Empty,
    //                                                string.Empty,
    //                                                string.Empty,
    //                                                productID,
    //                                                1,
    //                                                int.MaxValue,
    //                                                out recordCount,
    //                                                isUsed,
    //                                                isBBC);

    //        int count = licenseCount - productUserList.Count;

    //        if (count < 0)
    //        {//关联数超过限制


    //            string msg = "该产品当前允许关联数变为" + licenseCount.ToString() + "，您当前已关联数是" + productUserList.Count + "，请在“产品关联”里减少对应的关联用户数";

    //            if (isAdmin)
    //            {
    //                return "AlterRightMsg('" + msg + "'," + productID.ToString() + ")";
    //            }
    //            else
    //            {
    //                return "alert('" + msg + "');return false;";
    //            }
    //        }
    //        else
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError("ECCommon", "CheckECCanUserProduct", AppError.EROR, 0, ex);
    //        return "alert('检测产品关联出错，请与系统管理员联系。');return false;";
    //    }
    //}



    /// <summary>
    ///  取得某产品已经关联的用户数量
    /// </summary>
    /// <param name="ecID"></param>
    /// <param name="productID"></param>
    /// <returns></returns>
    //public static int GetAssignedUserNumber(int ecID, int productID)
    //{
    //    int assignedUserNumber = 0;

    //    ADCList<ECProductUsedInfo> productUserList = new ADCList<ECProductUsedInfo>();

    //    int recordCount = 0;

    //    int orgID = -1;
    //    string userName = string.Empty;
    //    string loginName = string.Empty;
    //    string mobile = string.Empty;
    //    string email = string.Empty;
    //    bool isUsed = true;
    //    SIProductBasicInfo siProductBasicInfo = ProductBasicBLL.GetProductBasicInfo(productID);
    //    bool isBBC = false;
    //    if (siProductBasicInfo.AppMode == "B-B-C")
    //    {
    //        isBBC = true;
    //    }

    //    // bool isLBS = ProductBasicBLL.IsLBS(productID);

    //    // 取得已关联用户数，因为是分页，
    //    // 所以用Int32.MaxValue作为PageDataSize
    //    productUserList = ECUserProductBLL.GetNoProductUsedListByPages(
    //                                                ecID,
    //                                                orgID,
    //                                                userName,
    //                                                loginName,
    //                                                mobile,
    //                                                email,
    //                                                productID,
    //                                               1,
    //                                               Int32.MaxValue,
    //                                                out recordCount,
    //                                                isUsed,
    //                                                isBBC);

    //    assignedUserNumber = recordCount;

    //    return assignedUserNumber;
    //}

    /// <summary>
    ///  取得某产品关联申请中和关联成功的用户数量
    /// </summary>
    /// <param name="ecID"></param>
    /// <param name="productID"></param>
    /// <returns></returns>
    //public static int GetAssigningUserNumber(int ecID, int productID)
    //{
    //    int assigningUserNumber = 0;
    //    int recordCount = 0;
    //    int orgID = -1;
    //    string userName = string.Empty;
    //    string loginName = string.Empty;
    //    string mobile = string.Empty;
    //    string email = string.Empty;
    //    int operationFlag = 0; // 添加申请
    //    int status = -1; // 取出全部的申请中的

    //    ADCList<ECProductUsedInfo> list =
    //                               ECUserProductBLL.GetProductUserApplyListByPages(
    //                                            ecID,
    //                                            orgID,
    //                                            userName,
    //                                            loginName,
    //                                            mobile,
    //                                            email,
    //                                            productID,
    //                                            status,
    //                                            operationFlag,
    //                                            (DateTime)SqlDateTime.MinValue,
    //                                             (DateTime)SqlDateTime.MaxValue,
    //                                             1,
    //                                             int.MaxValue,
    //                                             out recordCount);

    //    if (list != null)
    //    {
    //        // 排除处理成功的数据
    //        foreach (ECProductUsedInfo item in list)
    //        {
    //            // 表示处理成功
    //            if (item.Status == 1)
    //            {
    //                continue;
    //            }
    //            ++assigningUserNumber;
    //        }
    //    }

    //    return assigningUserNumber;
    //}


    /// <summary>
    /// 获取购买的产品数量
    /// </summary>
    /// <param name="ecOrderExpandInfo"></param>
    /// <param name="siProductBasicInfo"></param>
    /// <returns></returns>
  
    //public static int GetProductCount(ECOrderExpandInfo ecOrderExpandInfo, SIProductBasicInfo siProductBasicInfo)
    //{
    //    if (ecOrderExpandInfo == null)//非B类产品没有扩展信息
    //        return 1;

    //    if (siProductBasicInfo.LicenseCount == 1 && siProductBasicInfo.TimeLen == 1)
    //    {
    //        return ecOrderExpandInfo.TimeLen * ecOrderExpandInfo.TotalLicence;
    //    }

    //    return ecOrderExpandInfo.TotalLicence / siProductBasicInfo.LicenseCount;
    //}
}
