﻿using Common.Encrypt;
using Domain;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.ServiceImp
{
	/// <summary>
	/// 登录基类 created by Sean 2016-11-29 17:12:34
	/// </summary>
	public class UserManage : Repository<SYS_USER>, IUserManage
	{
		IUserRoleManage UserRoleManage { get; set; }
		IPermissionManage PermissionManage { get; set; }
		IRolePermissionManage RolePermissionManage { get; set; }
		IModuleManage ModuleManage { get; set; }
		/// <summary>
		/// 登录用户新增或者编辑
		/// </summary>
		/// <param name="module"></param>
		/// <returns></returns>
		public bool SaveOrUpdate(SYS_USER module)
		{
			if (module == null)
				return false;
			if (module.Id == 0)
			{
				module.Password = MD5Helper.MD5EncryptWithMove(ConfigurationManager.AppSettings["DefaultPassword"]);
				return this.SaveOrUpdate(module, false);
			}
			return this.SaveOrUpdate(module, true);
		}
		/// <summary>
		/// 批量添加
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public bool Save(List<SYS_USER> list)
		{
			if (list == null || list.Count == 0)
				return false;
			using (TransactionScope sc = new TransactionScope())
			{
				try
				{
					foreach (var item in list)
					{
						this.Save(item);
					}
					sc.Complete();
				}
				catch (Exception e)
				{
					throw e;
				}
			}
			return true;
		}
		/// <summary>
		/// 根据id批量删除
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public int DeleteByIdList(List<int> ids)
		{
			if (ids.Count == 0)
				return 0;
			return this.Delete(m => ids.Contains(m.Id));
		}
		/// <summary>
		/// 密码批量重置
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public int ResetPassword(List<int> ids)
		{
			if (ids.Count == 0)
				return 0;
			var entitys = this.LoadListAll(m => ids.Contains(m.Id));
			entitys.ForEach(m => m.Password = MD5Helper.MD5EncryptWithMove(ConfigurationManager.AppSettings["DefaultPassword"]));
			return this.UpdateList(entitys);
		}
		/// <summary>
		/// 判断登陆账户或者密码是否正确
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public SYS_USER UserLogin(string account, string password)
		{
			var entity = this.Get(m => m.Account == account);
			if (entity == null)
				return null;
			if (entity.Password != MD5Helper.MD5EncryptWithMove(password))
				return null;
			return entity;
		}
		/// <summary>
		/// 查询用户全部信息
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public Account GetAccountByUser(SYS_USER user)
		{
			if (user == null)
				return null;
			var permission = GetPermissionByUser(user);
			var module = GetModuleByPermission(permission);
			var account = new Account
			{
				Id = user.Id,
				Name = user.Name,
				LoginName = user.Account,
				PassWord = user.Password,
				IsAdmin = IsAdmin(user.Id),
				Permissions = permission,
				Modules = module
			};
			return account;
		}
		/// <summary>
		/// 是否是超级管理员
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool IsAdmin(int userId)
		{
			return this.UserRoleManage.LoadListAll(m => m.UserId == userId).Select(m => m.RoleId == (int)Enum.RoleType.Admin).Count() > 0;
		}
		/// <summary>
		/// 根据用户信息得到他的权限 created by Sean.He 2017-01-10 15:32:03
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public List<SYS_PERMISSION> GetPermissionByUser(SYS_USER user)
		{
			//是否为超级管理员
			if (IsAdmin(user.Id))
				return this.PermissionManage.LoadListAll(null);
			//用户所有的角色Id
			var roleids = this.UserRoleManage.LoadAll(m => m.UserId == user.Id).Select(m => m.RoleId).ToList();
			//用户所有的权限Id 去重
			var permissionIds = this.RolePermissionManage.LoadAll(m => roleids.Contains(m.Id)).Select(m => m.PermissionId).ToList().Distinct();
			return this.PermissionManage.LoadListAll(m => permissionIds.Contains(m.Id));
		}
		/// <summary>
		/// 根据权限获得相应的模块 created by Sean.He 2017-01-10 15:41:28
		/// </summary>
		/// <param name="permissions"></param>
		/// <returns></returns>
		public List<SYS_MODULE> GetModuleByPermission(List<SYS_PERMISSION> permissions)
		{
			if (permissions == null)
				return null;
			//权限对应的模块Id 去重
			var moduleIds = permissions.Select(m => m.ModuleId).ToList().Distinct();
			return this.ModuleManage.LoadListAll(m => moduleIds.Contains(m.Id));
		}
		/// <summary>
		/// 从cookie中获取用户信息 created by Sean.He 2017-01-10 16:29:31
		/// </summary>
		/// <returns></returns>
		public Account GetAccountByCookie()
		{
			var cookieValue = Common.CookieHelper.GetCookieValue("cookie_rememberme");
			if (!string.IsNullOrEmpty(cookieValue))
			{
				try
				{
					var jsonFormat = Common.JsonHelper.Deserialize<Account>(cookieValue);
					var user = this.UserLogin(jsonFormat.LoginName, jsonFormat.PassWord);
					if (user != null)
						return this.GetAccountByUser(user);
				}
				catch (Exception e)
				{
					throw e;
				}
			}
			return null;
		}
	}
}
