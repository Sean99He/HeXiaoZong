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
	/// 数据操作基类 created by Sean 2016-11-29 14:06:17
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Repository<T> : IRepository<T> where T : class
	{
		#region 数据操作对象
		private DbContext _Context = new MyConfig().db;
		/// <summary>
		/// 上下文
		/// </summary>
		public DbContext Context
		{
			get
			{
				_Context.Configuration.ValidateOnSaveEnabled = false;
				//关闭导航属性
				_Context.Configuration.ProxyCreationEnabled = false;
				return _Context;
			}
		}
		/// <summary>
		/// 数据模型操作
		/// </summary>
		public DbSet<T> dbSet
		{
			get
			{
				return this.Context.Set<T>();
			}
		}
		#endregion
		#region 单模型操作
		/// <summary>
		/// 获取实体
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public virtual T Get(Expression<Func<T, bool>> predicate)
		{
			try
			{
				return dbSet.AsNoTracking().SingleOrDefault(predicate);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 插入实体
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual bool Save(T entity)
		{
			try
			{
				int row = 0;
				var entry = this.Context.Entry<T>(entity);
				entry.State = EntityState.Added;
				row = this.Context.SaveChanges();
				entry.State = EntityState.Detached;
				return row > 0;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 更新实体
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual bool Update(T entity)
		{
			try
			{
				int row = 0;
				var entry = this.Context.Entry<T>(entity);
				entry.State = EntityState.Modified;
				row = this.Context.SaveChanges();
				entry.State = EntityState.Detached;
				return row > 0;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 新增或者编辑实体
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="isEdit"></param>
		/// <returns></returns>
		public virtual bool SaveOrUpdate(T entity, bool isEdit)
		{
			try
			{
				return isEdit ? Update(entity) : Save(entity);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 删除实体
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public virtual int Delete(Expression<Func<T, bool>> predicate)
		{
			try
			{
				int rows = 0;
				var entry = predicate == null ? this.dbSet.AsQueryable() : this.dbSet.Where(predicate);
				List<T> list = entry.ToList();
				if (list.Count > 0)
				{
					foreach (var item in list)
					{
						this.dbSet.Remove(item);
					}
					rows = this.Context.SaveChanges();
				}
				return rows;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 执行sql删除
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="para"></param>
		/// <returns></returns>
		public virtual int DeleteBySql(string sql, params DbParameter[] para)
		{
			try
			{
				return this.Context.Database.ExecuteSqlCommand(sql, para);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 验证实体是否存在
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public virtual bool IsExist(Expression<Func<T, bool>> predicate)
		{
			var entity = this.dbSet.Where(predicate);
			return entity.Any();
		}
		/// <summary>
		/// 验证实体是否存在
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="para"></param>
		/// <returns></returns>
		public virtual bool IsExist(string sql, params DbParameter[] para)
		{
			var result = this.Context.Database.SqlQuery(typeof(int), sql, para);
			if (result.GetEnumerator().Current == null || result.GetEnumerator().Current.ToString() == "0")
				return false;
			return true;
		}
		#endregion
		#region 多模型操作
		/// <summary>
		/// 新增多模型数据
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public virtual int SaveList(List<T> list)
		{
			try
			{
				this.dbSet.Local.Clear();
				foreach (var item in list)
				{
					this.dbSet.Add(item);
				}
				return this.Context.SaveChanges();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 更新多模型数据
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public virtual int UpdateList(List<T> list)
		{
			if (list.Count == 0) return 0;
			try
			{
				foreach (var item in list)
				{
					this.Context.Entry(item).State = EntityState.Modified;
				}
				return this.Context.SaveChanges();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 删除多模型数据
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public virtual int DeleteList(List<T> list)
		{
			if (list == null || list.Count == 0) return 0;
			foreach (var item in list)
			{
				this.dbSet.Remove(item);
			}
			return this.Context.SaveChanges();
		}
		#endregion
		#region 存储过程操作
		//public virtual object ExecuteProc(string procName, params DbParameter[] para)
		//{
		//	try
		//	{

		//	}
		//	catch (Exception e)
		//	{
		//		throw e;
		//	}
		//}
		#endregion
		#region 查询多条数据
		/// <summary>
		/// 查询多条 IQueryable 延时加载数据
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public virtual IQueryable<T> LoadAll(Expression<Func<T, bool>> predicate)
		{
			try
			{
				if (predicate != null)
				{
					return this.dbSet.Where(predicate).AsNoTracking<T>();
				}
				return this.dbSet.AsQueryable<T>().AsNoTracking<T>();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 查询多条数据 IList
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public virtual List<T> LoadListAll(Expression<Func<T, bool>> predicate)
		{
			try
			{
				if (predicate != null)
				{
					return this.dbSet.Where(predicate).AsNoTracking().ToList();
				}
				return this.dbSet.AsQueryable<T>().AsNoTracking().ToList();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 根据sql查询数据
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="para"></param>
		/// <returns></returns>
		public virtual List<T> SelectBySql(string sql, params DbParameter[] para)
		{
			try
			{
				return this.Context.Database.SqlQuery(typeof(int), sql, para).Cast<T>().ToList();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		#endregion
	}
}
