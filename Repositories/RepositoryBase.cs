﻿using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T>
		where T : class, new()
	{
		protected readonly RepositoryContext _context;

		protected RepositoryBase(RepositoryContext context)
		{
			_context = context;
		}

		public void Create(T t)
		{
			_context.Set<T>().Add(t);
		}

		public IQueryable<T> FindAll(bool trackChanges)
		{
			return trackChanges
				? _context.Set<T>()
				: _context.Set<T>().AsNoTracking();
		}

		public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
		{
			return trackChanges
				? _context.Set<T>().Where(expression).SingleOrDefault()
				: _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
		}

		public void Remove(T t)
		{
			_context.Set<T>().Remove(t);
		}

		public void Update(T t)
		{
			_context.Set<T>().Update(t);	
		}
	}
}