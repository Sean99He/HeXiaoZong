using Common;
using Domain;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPage.Areas.SysManage.Controllers
{
	public class RoleController : Controller
	{
		IRoleManage RoleManage { get; set; }
		IUserRoleManage UserRoleManage { get; set; }
		// GET: SysManage/Role
		public ActionResult Index()
		{
			return View();
		}
		/// <summary>
		/// 查询数据
		/// </summary>
		/// <returns></returns>
		public string GetList()
		{
			var list = this.RoleManage.LoadAll(null).Select(m => new
			{
				m.Id,
				m.RoleCode,
				m.RoleName,
				m.IsAvailable
			}).ToList();
			return JsonHelper.Serialize(list);
		}
		/// <summary>
		/// 新增或者编辑
		/// </summary>
		/// <param name="module"></param>
		/// <returns></returns>
		public int SaveOrUpdate(SYS_ROLE module)
		{
			if (this.RoleManage.SaveOrUpdate(module))
				return 1;
			return 0;
		}
		/// <summary>
		/// 根据id批量删除
		/// </summary>
		/// <param name="entityList"></param>
		/// <returns></returns>
		[HttpPost]
		public int Delete(List<SYS_ROLE> entityList)
		{
			var ids = entityList.Select(m => m.Id).ToList();
			return this.RoleManage.DeleteByListIds(ids);
		}
		/// <summary>
		/// 用户角色分配
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public ActionResult AssignUserRole(int userId)
		{
			ViewData["userId"] = userId;
			//所有的角色信息
			var roleList = this.RoleManage.LoadListAll(null);
			//当前用户拥有的角色
			var selectRoleList = this.UserRoleManage.LoadListAll(m => m.UserId == userId).Select(m => m.RoleId).ToList();
			ViewData["roleList"] = roleList;
			ViewData["selectRoleList"] = selectRoleList;
			return View();
		}
		/// <summary>
		/// 用户角色更新
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="perid"></param>
		/// <returns></returns>
		[HttpPost]
		public int SaveUserRoleAssign(int userId, string perid)
		{
			try
			{
				if (userId == 0)
					return 0;
				//首先删除掉所有该用户的角色
				this.UserRoleManage.Delete(m => m.UserId == userId);
				if (String.IsNullOrEmpty(perid))
					return 1;
				perid = perid.TrimEnd(',');
				List<int> roleIds = perid.Split(',').Select(m => int.Parse(m)).ToList();
				List<SYS_USER_ROLE> userRoleList = new List<SYS_USER_ROLE>();
				foreach (var item in roleIds)
				{
					var module = new SYS_USER_ROLE { RoleId = item, UserId = userId };
					userRoleList.Add(module);
				}
				this.UserRoleManage.SaveList(userRoleList);
			}
			catch (Exception e)
			{
				throw e;
			}
			return 1;
		}
	}
}