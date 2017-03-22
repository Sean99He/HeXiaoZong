using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
	/// <summary>
	/// 登录用户管理基类接口
	/// </summary>
	public interface IUserManage : IRepository<SYS_USER>
	{
		/// <summary>
		/// 新增或者编辑
		/// </summary>
		/// <param name="modele"></param>
		/// <returns></returns>
		bool SaveOrUpdate(SYS_USER module);
		/// <summary>
		/// 根据id批量删除
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		int DeleteByIdList(List<int> ids);
		/// <summary>
		/// 密码批量重置
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		int ResetPassword(List<int> ids);
		/// <summary>
		/// 批量添加
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		bool Save(List<SYS_USER> list);
		/// <summary>
		/// 判断登陆用户账户和密码是否正确
		/// </summary>
		/// <param name="account"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		SYS_USER UserLogin(string account, string password);
		/// <summary>
		/// 查询用户全部信息
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		Account GetAccountByUser(SYS_USER user);
		/// <summary>
		/// 是否是超级管理员
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		bool IsAdmin(int userId);
		/// <summary>
		/// 从cookie中获取用户信息
		/// </summary>
		/// <returns></returns>
		Account GetAccountByCookie();
	}
}
