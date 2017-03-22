﻿using Domain;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp
{
	public class ModuleManage : Repository<SYS_MODULE>, IModuleManage
	{
		/// <summary>
		/// add or edit
		/// </summary>
		/// <param name="module"></param>
		/// <returns></returns>
		public int SaveOrUpdate(SYS_MODULE module)
		{
			module.Name = module.Name.Replace("&nbsp;", "").Replace("|--", "");
			if (module.Id == 0 ? this.SaveOrUpdate(module, false) : this.SaveOrUpdate(module, true))
				return 1;
			return 0;
		}
		/// <summary>
		/// 根据Id批量删除
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public int DeleteByIds(List<int> ids)
		{
			if (ids.Count == 0)
				return 0;
			return this.Delete(m => ids.Contains(m.Id));
		}
	}
}
