using Common;
using Service;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPage.Controllers
{
	/// <summary>
	/// 登录验证类 created by Sean.He 2017-01-10 20:35:43
	/// </summary>
	public class BaseController : Controller
	{
		public IUserManage UserManage = Spring.Context.Support.ContextRegistry.GetContext().GetObject("Service.User") as IUserManage;
		public Account CurrentUser
		{
			get
			{
				if (SessionHelper.GetSession("CurrentUser") != null)
					return SessionHelper.GetSession("CurrentUser") as Account;
				//session过期 通过cookie获取对象 重新存放到session
				var account = this.UserManage.GetAccountByCookie();
				SessionHelper.SetSession("CurrentUser", account);
				return account;
			}
		}
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			//判断session对象是否存在
			if (filterContext.HttpContext.Session == null)
			{
				filterContext.HttpContext.Response.Write("<script>alert('登陆过期，请重新登陆！');window.top.location='/';</script>");
				filterContext.HttpContext.Response.End();
				filterContext.Result = new EmptyResult();
				return;
			}
			//登录验证
			if (this.CurrentUser == null)
			{
				filterContext.HttpContext.Response.Write("<script>alert('登陆过期，请重新登陆！');window.top.location='/';</script>");
				filterContext.HttpContext.Response.End();
				filterContext.Result = new EmptyResult();
				return;
			}
		}
	}
}