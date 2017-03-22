using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
	/// <summary>
	/// Session帮助类 created by Sean.He 2017-01-10 16:09:26
	/// </summary>
	public class SessionHelper
	{
		/// <summary>
		/// 根据Session名获得Session对象
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static object GetSession(string name)
		{
			return HttpContext.Current.Session[name];
		}
		/// <summary>
		/// 设置Session
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetSession(string name, object value)
		{
			HttpContext.Current.Session.Remove(name);
			HttpContext.Current.Session.Add(name, value);
		}
		/// <summary>
		/// 添加Session
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="expiress"></param>
		public static void SetSession(string name, object value, int expiress)
		{
			HttpContext.Current.Session[name] = value;
			HttpContext.Current.Session.Timeout = expiress;
		}
		/// <summary>
		/// 清空Session
		/// </summary>
		/// <param name="name"></param>
		public static void ClearSession(string name)
		{
			HttpContext.Current.Session[name] = null;
		}
	}
}
