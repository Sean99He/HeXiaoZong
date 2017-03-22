﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPage.Areas.SysManage.Models
{
	/// <summary>
	/// 菜单栏 一级菜单
	/// </summary>
	public class MenuModule
	{
		public string id { get; set; }
		public string homePage { get; set; }
		public List<Menu> menu { get; set; }
		public MenuModule()
		{
			this.menu = new List<Menu>();
		}
	}
	/// <summary>
	/// 二级菜单
	/// </summary>
	public class Menu
	{
		public string text { get; set; }
		public List<Items> items { get; set; }
		public Menu()
		{
			this.items = new List<Items>();
		}
	}
	/// <summary>
	/// 三级菜单
	/// </summary>
	public class Items
	{
		public string id { get; set; }
		public string text { get; set; }
		public string href { get; set; }
		//public string closeable { get; set; }
	}
}