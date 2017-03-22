﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPage.Models
{
	/// <summary>
	/// webpage公用方法 create by Sean.He 2016-12-20 21:11:26
	/// </summary>
	public class CommonFuction
	{
		/// <summary>
		/// 模块名称模块化 created by Sean.He 2016-12-20 21:11:55
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static List<SYS_MODULE> FormartModuleList(List<SYS_MODULE> list)
		{
			var result = new List<SYS_MODULE>();
			foreach (var item in list.Where(m => m.ParentId == 0))
			{
				result.Add(item);
				foreach (var item1 in list.Where(m => m.ParentId == item.Id))
				{
					item1.Name = "&nbsp;&nbsp;|--" + item1.Name;
					result.Add(item1);
					foreach (var item2 in list.Where(m => m.ParentId == item1.Id))
					{
						item2.Name = "&nbsp;&nbsp;&nbsp;&nbsp;|--" + item2.Name;
						result.Add(item2);
					}
				}
			}
			return result;
		}
	}
}