using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
	/// <summary>
	/// 模块操作 created by Sean.He 2016-12-05 13:36:54
	/// </summary>
	public interface IModuleManage : IRepository<SYS_MODULE>
	{
		/// <summary>
		/// add or update created by Sean.He
		/// </summary>
		/// <param name="module"></param>
		/// <returns></returns>
		int SaveOrUpdate(SYS_MODULE module);
		/// <summary>
		/// 根据Id批量删除
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		int DeleteByIds(List<int> ids);
	}
}
