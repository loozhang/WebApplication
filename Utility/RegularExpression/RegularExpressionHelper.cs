using Utility.RegularExpression;

namespace Utility
{
	/// <summary>
	/// 正则表达式助手
	/// </summary>
	public class RegularExpressionHelper
	{
		/// <summary>
		/// 获取正则表达式
		/// </summary>
		/// <param name="key">正则表达式标识</param>
		/// <returns></returns>
		public static string GetCheckString(string key)
		{
			return RegularExpressionString.ResourceManager.GetString(key);
		}
	}
}
