using Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	/// <summary>
	/// 数据操作基类接口 created by Sean 2016-11-29 14:05:11
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRepository<T> where T : class
	{
		#region 数据操作对象
		/// <summary>
		/// 上下文
		/// </summary>
		DbContext Context { get; }
		/// <summary>
		/// 数据模型操作
		/// </summary>
		DbSet<T> dbSet { get; }
		#endregion
		#region 单模型操作
		/// <summary>
		/// 获取实体
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		T Get(Expression<Func<T, bool>> predicate);
		/// <summary>
		/// 插入实体
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		bool Save(T entity);
		/// <summary>
		/// 修改实体
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		bool Update(T entity);
		/// <summary>
		/// 修改或者保存
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="isEdit"></param>
		/// <returns></returns>
		bool SaveOrUpdate(T entity, bool isEdit);
		/// <summary>
		/// 删除实体
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		int Delete(Expression<Func<T, bool>> predicate);
		/// <summary>
		/// 执行sql删除
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="para"></param>
		/// <returns></returns>
		int DeleteBySql(string sql, params DbParameter[] para);
		/// <summary>
		/// 验证实体是否存在
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		bool IsExist(Expression<Func<T, bool>> predicate);
		/// <summary>
		/// 根据sql语句验证实体是否存在
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="para"></param>
		/// <returns></returns>
		bool IsExist(string sql, params DbParameter[] para);
		#endregion
		#region 多模型操作
		/// <summary>
		/// 增加多模型数据
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		int SaveList(List<T> list);
		/// <summary>
		/// 更新多模型数据
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		int UpdateList(List<T> list);
		/// <summary>
		/// 删除多模型数据
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		int DeleteList(List<T> list);
		#endregion
		#region 存储过程操作
		///// <summary>
		///// 执行增删改存储过程
		///// </summary>
		///// <param name="procName"></param>
		///// <param name="para"></param>
		///// <returns></returns>
		//object ExecuteProc(string procName, params DbParameter[] para);
		///// <summary>
		///// 执行查询操作过程
		///// </summary>
		///// <param name="procName"></param>
		///// <param name="para"></param>
		///// <returns></returns>
		//object ExecuteQueryProc(string procName, params DbParameter[] para);
		#endregion
		#region 查询多条数据
		/// <summary>
		/// 查询多条 IQueryable
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		IQueryable<T> LoadAll(Expression<Func<T, bool>> predicate);
		/// <summary>
		/// 查询多条数据 IList
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		List<T> LoadListAll(Expression<Func<T, bool>> predicate);
		/// <summary>
		/// 根据sql查询数据
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="para"></param>
		/// <returns></returns>
		List<T> SelectBySql(string sql, params DbParameter[] para);
		#endregion
	}
}
