using Domain;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp
{
	public class PermissionManage : Repository<SYS_PERMISSION>, IPermissionManage
	{
		/// <summary>
		/// saveorupdate created by Sean.He 2016-12-13 18:31:21
		/// </summary>
		/// <param name="module"></param>
		/// <returns></returns>
		public bool SaveOrUpdate(SYS_PERMISSION module)
		{
			if (module == null)
				return false;
			if (module.Id == 0)
				return this.SaveOrUpdate(module, false);
			return this.SaveOrUpdate(module, true);
		}
		/// <summary>
		/// delete module created by Sean.He 2016-12-13 18:37:54
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public int DeleteByIds(List<int> ids)
		{
			if (ids.Count < 1)
				return 0;
			return this.Delete(m => ids.Contains(m.Id));
		}
	}
}
