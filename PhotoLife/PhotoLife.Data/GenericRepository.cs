using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PhotoLife.Data.Contracts;

namespace PhotoLife.Data
{
    public class GenericRepository<T> : IRepository<T>
     where T : class
    {
        public GenericRepository(IPhotoLifeEntities dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("Context cannot be null");
            }

            this.Context = dbContext;
        }

        protected IPhotoLifeEntities Context { get; set; }

        public T GetById(object id)
        {
            return this.Context.DbSet<T>().Find(id);
        }

        public IEnumerable<T> Entities => this.Context.DbSet<T>().ToList();

        public IEnumerable<T> GetAll()
        {
            return this.Context.DbSet<T>().ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filterExpression)
        {
            return this.Context.DbSet<T>()
                .Where(filterExpression)
                .ToList();
        }

        public IEnumerable<T> GetAll<T1>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression, bool isAscending)
        {
            return isAscending
                ? this.Context.DbSet<T>()
                    .Where(filterExpression)
                    .OrderBy(sortExpression)
                    .ToList()
                : this.Context.DbSet<T>()
                    .Where(filterExpression)
                    .OrderByDescending(sortExpression)
                    .ToList();
        }

        public IEnumerable<T2> GetAll<T1, T2>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression, Expression<Func<T, T2>> selectExpression)
        {
            return this.Context.DbSet<T>()
                 .Where(filterExpression)
                 .OrderBy(sortExpression)
                 .Select(selectExpression)
                 .ToList();
        }

        public void Add(T entity)
        {
            this.Context.SetAdded(entity);
        }

        public void Update(T entity)
        {
            this.Context.SetUpdated(entity);

        }

        public void Delete(T entity)
        {
            this.Context.SetDeleted(entity);
        }
    }
}
