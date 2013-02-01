//-------------------------------------------------------------------
//版权所有：版权所有(C) 2009，Microsoft(China) Co.,LTD
//系统名称：SXMCC-ADC
//文件名称：ContentUtil.cs
//模块名称：
//模块编号：
//作　　者：ZHAOLIGUO
//完成日期：2009/08/19
//功能说明：
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 界面常用的方法
/// </summary>
public class ContentUtil
{
    /// <summary>
    /// 读取手机验证正则表达式
    /// </summary>
    /// <returns></returns>
    public static string GetMobileCheckString()
    {
        return ConfigurationManager.AppSettings["MobileCheckString"].ToString();
    }

    /// <summary>
    /// 取得地区码
    /// </summary>
    public static string AreaCode
    {
        get
        {
            string areaCode = ConfigurationManager.AppSettings["AreaCode"];
            if (string.IsNullOrEmpty(areaCode))
            {
                return "999";
            }
            return areaCode;
        }
    }

    /// <summary>
    ///  将字符串安全转换为int类型
    /// </summary>
    /// <param name="text"></param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static int SafeInt(string text, int defaultValue)
    {
        if (!string.IsNullOrEmpty(text))
        {
            try
            {
                return Int32.Parse(text);
            }
            catch (Exception) { }
        }
        return defaultValue;
    }

    /// <summary>
    /// 将字符串安全转换为DateTime类型
    /// </summary>
    /// <param name="date"></param>
    /// <param name="defautlValue"></param>
    /// <returns></returns>
    public static DateTime SafeDate(string date, DateTime defautlValue)
    {
        if (!string.IsNullOrEmpty(date))
        {
            try
            {
                return DateTime.Parse(date);
            }
            catch (Exception)
            {
            }
        }

        return defautlValue;
    }

    /// <summary>
    ///  将数据库中的分为单位的金额，转换成元为单位的金额
    ///  返回类型为string
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static string ConvertMoneyToYuanString(int price)
    {
        double yuanPrice = price / 100.00;
        return yuanPrice.ToString("0.00");
    }

    /// <summary>
    ///  将数据库中的分为单位的金额，转换成元为单位的金额
    ///  返回类型为decimal
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal ConvertMoneyToYuanDecimal(int price)
    {
        decimal yuanPrice = Convert.ToDecimal(price / 100.00);
        return yuanPrice;
    }

    /// <summary>
    ///  将元为单位的金额，转换成分为单位的金额
    ///  返回类型为decimal
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static int ConvertMoneyToFenInt(decimal price)
    {
        int fenPrice = Convert.ToInt32(price * 100);
        return fenPrice;
    }


    /// <summary>
    /// 普通提示信息
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    public static void MsgAlert(Page page, string msg)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "error",
                           "<script language='javascript'>alert('" + msg + "')</script>");
    }

    /// <summary>
    /// ajax提示信息
    /// </summary>
    /// <param name="ajax"></param>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    public static void AjaxAlert(Control ajax, Page page, string msg)
    {
        RegisterStartupScript(ajax,
            page.GetType(), "error", "alert('" + msg + "');", true);
    }

    /// <summary>
    /// Ajax用的注册JavaScript函数
    /// </summary>
    /// <param name="control"></param>
    /// <param name="type"></param>
    /// <param name="key"></param>
    /// <param name="script"></param>
    /// <param name="addScriptTags"></param>
    public static void RegisterStartupScript(Control control, Type type,
                                                                 string key, string script, bool addScriptTags)
    {
        ScriptManager.RegisterStartupScript(control, type, key,
                                                                 script.Replace("\r\n", ""), addScriptTags);
    }


    /// <summary>
    /// 向Head中添加脚本文件
    /// </summary>
    /// <param name="scriptFilePath">脚本路径</param>
    /// <returns></returns>
    public static void RegisterHeadScriptInclude(Page page, string scriptFilePath)
    {
        HtmlGenericControl hgcHeader = new HtmlGenericControl("script");
        hgcHeader.Attributes.Add("type", "text/javascript");

        if (scriptFilePath.StartsWith("~"))
        {
            hgcHeader.Attributes.Add("src", page.ResolveClientUrl(scriptFilePath));
        }
        else
        {
            hgcHeader.Attributes.Add("src", scriptFilePath);
        }

        page.Header.Controls.Add(hgcHeader);
    }

    /// <summary>
    /// 向Head中添加脚本内容
    /// </summary>
    /// <param name="scriptContent">脚本路径</param>
    /// <returns></returns>
    public static void RegisterHeadScriptBlocks(Page page, string scriptContent)
    {
        HtmlGenericControl hgcHeader = new HtmlGenericControl("script");
        hgcHeader.Attributes.Add("type", "text/javascript");
        hgcHeader.InnerHtml = scriptContent;
        page.Header.Controls.Add(hgcHeader);
    }

    /// <summary>
    /// 字符串截取
    /// </summary>
    /// <param name="str">原始字符串</param>
    /// <param name="len">截取长度</param>
    /// <returns>处理后的字符串</returns>
    public static string SubString(string str, int len)
    {
        if (str.Length > len)
            str = (str.Substring(0, len - 1) + "...");

        return str;
    }

    /// <summary>
    /// 获得营销方案门户部署地址
    /// </summary>
    /// <returns></returns>
    public static string GetMarketingUrl()
    {
        return ConfigurationManager.AppSettings["MarketingUrl"].ToString();
    }

    /// <summary>
    /// 获得用户手册下载地址
    /// </summary>
    /// <returns></returns>
    public static string GetHelperWordUrl()
    {
        return ConfigurationManager.AppSettings["HelperWordUrl"].ToString();
    }

    /// <summary>
    /// 获得图片路径
    /// </summary>
    /// <param name="url">图片路径</param>
    /// <returns></returns>
    //public static string GetImageUrl(string url)
    //{
    //    AttachmentUtility attch = new AttachmentUtility("");
    //    return attch.GetAttachmentFullUrl(url);
    //}
}
