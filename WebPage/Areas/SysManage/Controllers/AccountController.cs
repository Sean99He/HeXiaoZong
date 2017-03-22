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
	public class AccountController : Controller
	{
		IUserManage UserManage { get; set; }
		// GET: SysManage/Account
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult Login(SYS_USER user)
		{
			var userLogin = this.UserManage.UserLogin(user.Account, user.Password);
			if (userLogin != null)
			{
				var account = this.UserManage.GetAccountByUser(userLogin);
				account.PassWord = user.Password;
				//写入到Session
				SessionHelper.SetSession("CurrentUser", account);
				//记录信息到Coolie
				var accountJson = JsonHelper.Serialize(account);
				CookieHelper.SetCookie("cookie_rememberme", accountJson, null);
				return RedirectToAction("../Home");
			}
			else
				return Json("账户或者密码错误");
		}
	}
}