using Domain;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp
{
	/// <summary>
	/// created by Sean.He 2016-12-13 18:50:32
	/// </summary>
	public class RoleManage : Repository<SYS_ROLE>, IRoleManage
	{
		/// <summary>
		/// 根据id批量删除
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public int DeleteByListIds(List<int> ids)
		{
			if (ids.Count < 1)
				return 0;
			return this.Delete(m => ids.Contains(m.Id));
		}
		/// <summary>
		/// 新增或者编辑
		/// </summary>
		/// <param name="module"></param>
		/// <returns></returns>
		public bool SaveOrUpdate(SYS_ROLE module)
		{
			if (module == null)
				return false;
			if (module.Id == 0)
				return this.SaveOrUpdate(module, false);
			return this.SaveOrUpdate(module, true);
		}
	}
}
