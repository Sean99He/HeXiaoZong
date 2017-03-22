﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Common
{
	public class JsonHelper
	{
		/// <summary>
		/// 对象序列化 created by Sean 2016-11-29 18:59:55
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string Serialize(object obj)
		{
			JavaScriptSerializer ser = new JavaScriptSerializer();
			return ser.Serialize(obj);
		}
		/// <summary>
		/// 对象反序列化
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="json"></param>
		/// <returns></returns>
		public static T Deserialize<T>(string json) where T : class
		{
			JavaScriptSerializer ser = new JavaScriptSerializer();
			if (!string.IsNullOrEmpty(json))
				return ser.Deserialize<T>(json);
			return null;
		}
	}
}