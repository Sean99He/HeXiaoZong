using Common;
using Domain;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebPage.Models;

namespace WebPage.Areas.SysManage.Controllers
{
	public class ModuleController : Controller
	{
		IModuleManage ModuleManage { get; set; }
		// GET: SysManage/Module
		public ActionResult Index()
		{
			var list = this.ModuleManage.LoadAll(m => m.ParentId == 0);
			StringBuilder builder = new StringBuilder();
			builder.Append("<option value=''>请选择</option><option value='0'>一级菜单</option>");
			foreach (var item in list)
			{
				builder.Append("<option value = '").Append(item.Id).Append("'>&nbsp;&nbsp;|--").Append(item.Name).Append("</option >");
				var list2 = this.ModuleManage.LoadAll(m => m.ParentId == item.Id);
				foreach (var item2 in list2)
				{
					builder.Append("<option value = '").Append(item2.Id).Append("'>&nbsp;&nbsp;&nbsp;&nbsp;|--").Append(item2.Name).Append("</option>");
				}
			}
			//父级菜单下拉框
			ViewData["ParentList"] = builder.ToString();
			return View();
		}
		/// <summary>
		/// 数据查询
		/// </summary>
		/// <returns></returns>
		public string GetList()
		{
			var list = this.ModuleManage.LoadListAll(null);
			CommonFuction.FormartModuleList(list);
			return JsonHelper.Serialize(list);
		}
		/// <summary>
		/// 编辑或者新增
		/// </summary>
		/// <param name="module"></param>
		/// <returns></returns>
		public int SaveOrUpdate(SYS_MODULE module)
		{
			if (module == null)
				return 0;
			return this.ModuleManage.SaveOrUpdate(module);
		}
		/// <summary>
		/// 删除操作
		/// </summary>
		/// <param name="entityList"></param>
		/// <returns></returns>
		public int Delete(List<SYS_MODULE> entityList)
		{
			var ids = entityList.Select(m => m.Id).ToList();
			return this.ModuleManage.DeleteByIds(ids);
		}
	}
}