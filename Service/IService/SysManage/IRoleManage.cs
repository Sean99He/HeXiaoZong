using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
	/// <summary>
	/// created by Sean.He 2016-12-13 18:50:00
	/// </summary>
	public interface IRoleManage : IRepository<SYS_ROLE>
	{
		/// <summary>
		/// 新增或者编辑
		/// </summary>
		/// <param name="module"></param>
		/// <returns></returns>
		bool SaveOrUpdate(SYS_ROLE module);
		/// <summary>
		/// 根据id批量删除
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		int DeleteByListIds(List<int> ids);
	}
}
