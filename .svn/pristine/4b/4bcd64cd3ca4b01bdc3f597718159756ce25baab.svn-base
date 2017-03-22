using Common;
using Domain;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Service.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPage.Areas.SysManage.Models;
using WebPage.Models;

namespace WebPage.Areas.SysManage.Controllers
{
	public class UserController : Controller
	{
		// GET: SysManage/User
		IUserManage UserManage { get; set; }
		[UserAuthorizeAttribute(ModuleAlias = "UserManage", OperateAction = "View")]
		public ActionResult Index()
		{
			return View();
		}
		/// <summary>
		/// 数据查询
		/// </summary>
		/// <returns></returns>
		public string GetList(GridPager pager, string name)
		{
			List<SYS_USER> list;
			if (string.IsNullOrEmpty(name))
				list = this.UserManage.LoadListAll(null);
			else
				list = this.UserManage.LoadListAll(m => m.Name == name);
			var json = new
			{
				results = list.Count(),
				rows = list.Select(p => new
				{
					p.Id,
					p.Name,
					p.Account,
					p.Enabled
				}).Skip((pager.pageIndex) * pager.limit).Take(pager.limit)
			};
			return JsonHelper.Serialize(json);
		}
		/// <summary>
		/// 编辑或者新增
		/// </summary>
		/// <param name="module"></param>
		public int SaveOrUpdate(SYS_USER module)
		{
			if (UserManage.SaveOrUpdate(module))
				return 1;
			return 0;
		}
		/// <summary>
		/// 根据id批量删除
		/// </summary>
		/// <param name="entityList"></param>
		/// <returns></returns>
		[HttpPost]
		public int Delete(List<SYS_USER> entityList)
		{
			var ids = entityList.Select(p => p.Id).ToList();
			return UserManage.DeleteByIdList(ids);
		}
		/// <summary>
		/// 密码重置
		/// </summary>
		/// <param name="entityList"></param>
		/// <returns></returns>
		[HttpPost]
		public int ResetPassword(List<SYS_USER> entityList)
		{
			var ids = entityList.Select(p => p.Id).ToList();
			return UserManage.ResetPassword(ids);
		}
		/// <summary>
		/// 导出数据
		/// </summary>
		/// <returns></returns>
		public ActionResult ExportExcel()
		{
			//创建一个excel对象
			HSSFWorkbook book = new HSSFWorkbook();
			//创建一个sheet
			ISheet sheet1 = book.CreateSheet("Sheet1");
			var list = this.UserManage.LoadListAll(null);
			IRow row1 = sheet1.CreateRow(0);
			row1.CreateCell(0).SetCellValue("用户姓名");
			row1.CreateCell(1).SetCellValue("用户账户");
			for (int i = 0; i < list.Count; i++)
			{
				IRow rowtemp = sheet1.CreateRow(i + 1);
				rowtemp.CreateCell(0).SetCellValue(list[i].Name);
				rowtemp.CreateCell(1).SetCellValue(list[i].Account);
			}
			MemoryStream ms = new MemoryStream();
			book.Write(ms);
			ms.Seek(0, SeekOrigin.Begin);
			return File(ms, "application/vnd.ms-excel", "用户.xls");
		}
		/// <summary>
		/// 导入数据
		/// </summary>
		[HttpPost]
		public ActionResult ImportExcel()
		{
			var file = Request.Files["file"];
			//以流的形式存放结果
			Stream inputStream = file.InputStream;
			//读取到exl
			HSSFWorkbook hssfworkbook = new HSSFWorkbook(inputStream);
			ISheet sheet = hssfworkbook.GetSheetAt(0);
			int rowCount = sheet.LastRowNum;
			//存放到List<SYS_USER>
			var list = new List<SYS_USER>();
			for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
			{
				IRow row = sheet.GetRow(i);
				SYS_USER model = new SYS_USER();
				if (row != null)
				{
					model.Name = row.GetCell(0).ToString();
					model.Account = row.GetCell(1).ToString();
				}
				list.Add(model);
			}
			this.UserManage.Save(list);
			return RedirectToAction("Index");
		}
		/// <summary>
		/// 下载模板
		/// </summary>
		/// <returns></returns>
		public ActionResult GetFile()
		{
			string path = @"C:\Users\HBD\Desktop\用户.xls";
			string fileName = "用户模版.xls";
			return File(path, "text/plain", fileName);
		}
	}
}