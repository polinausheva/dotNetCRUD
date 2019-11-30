﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotNetCrud.Services;
using DotNetCrud.Web.Models;

namespace DotNetCrud.Web.Data.Services.EF
{
    public class GenericEFDataService<T> : IGenericEFDataService<T> where T : class
    {
        protected DbSet<T> _dbSet;
        protected ApplicationDbContext context;

        public GenericEFDataService(ApplicationDbContext dbContext)
        {
            this._dbSet = dbContext.Set<T>();
            this.context = dbContext;
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return _dbSet.ToListAsync<T>();
        }

        public virtual Task<List<T>> GetListAsync(Func<T, bool> where)
        {
            return Task.Run(() => _dbSet.AsEnumerable().Where(where).ToList());
        }

        public virtual Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return _dbSet.SingleOrDefaultAsync(where);
        }

        public T GetSingleOrDefault(Expression<Func<T, bool>> where)
        {
            return _dbSet.SingleOrDefault(where);
        }

        public virtual void Add(params T[] items)
        {
            foreach (T item in items)
            {
                _dbSet.Add(item);
            }

            context.SaveChanges();
        }

        public virtual void Update(params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public virtual void Remove(params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Deleted;
            }

            context.SaveChanges();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return _dbSet.AnyAsync(where);
        }

        public bool Any(Expression<Func<T, bool>> where)
        {
            return _dbSet.Any(where);
        }

        public Task<bool> AnyAsync()
        {
            return _dbSet.AnyAsync();
        }

        public bool Any()
        {
            return _dbSet.Any();
        }
    }
}
