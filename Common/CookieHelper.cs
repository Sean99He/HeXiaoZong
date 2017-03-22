﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
	/// <summary>
	/// cookie 帮助类 created by Sean.He
	/// </summary>
	public class CookieHelper
	{
		/// <summary>
		/// 获取指定的cookie
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string GetCookieValue(string name)
		{
			HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
			string value = string.Empty;
			if (cookie != null)
				value = cookie.Value;
			return value;
		}
		/// <summary>
		/// 添加一个cookie
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="expires"></param>
		public static void SetCookie(string name, string value, int? expires)
		{
			var cookie = HttpContext.Current.Request.Cookies[name];
			if (cookie == null)
			{
				cookie = new HttpCookie(name);
			}
			ClearCookie(name);
			cookie.Value = value;
			if (expires != null && expires > 0)
			{
				cookie.Expires = DateTime.Now.AddDays(Convert.ToInt32(expires));
			}
			HttpContext.Current.Response.AppendCookie(cookie);
		}
		/// <summary>
		/// 清除一个指定的cookie
		/// </summary>
		/// <param name="name"></param>
		public static void ClearCookie(string name)
		{
			HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
			if (cookie != null)
			{
				TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
				cookie.Expires = DateTime.Now.Add(ts);
				HttpContext.Current.Response.AppendCookie(cookie);
				HttpContext.Current.Request.Cookies.Remove(name);
			}
		}
	}
}
