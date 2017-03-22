using Common;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPage.Areas.SysManage.Models;
using WebPage.Controllers;

namespace WebPage.Areas.SysManage.Controllers
{
	public class HomeController : BaseController
	{
		IModuleManage ModuleManage { get; set; }
		// GET: SysManage/Home
		public ActionResult Index()
		{
			bool flag = true;
			//一级菜单栏
			var list = new List<MenuModule>();
			var userModuleIds = this.CurrentUser.Modules.Select(m => m.Id).ToList();
			//用户权限所有的全部模块
			var moduleList = this.ModuleManage.LoadAll(m => userModuleIds.Contains(m.Id));
			foreach (var item in moduleList.Where(m => m.ParentId == 0))
			{
				var menuModule = new MenuModule();
				menuModule.id = item.Alias;
				foreach (var item2 in moduleList.Where(m => m.ParentId == item.Id))
				{
					//二级菜单
					var menu = new Menu();
					menu.text = item2.Name;
					foreach (var item3 in moduleList.Where(m => m.ParentId == item2.Id))
					{
						//三级菜单
						var items = new Items();
						if (flag)
						{
							//定义默认首页
							menuModule.homePage = item3.Alias;
							flag = false;
						}
						items.id = item3.Alias;
						items.text = item3.Name;
						items.href = item3.ModulePath;
						menu.items.Add(items);
					}
					menuModule.menu.Add(menu);
				}
				list.Add(menuModule);
			}
			var result = JsonHelper.Serialize(list);
			ViewData["Menu"] = result;
			return View();
		}
	}
}