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
            this.Set = this.Context.DbSet<T>();
        }

        protected IDbSet<T> Set { get; set; }

        protected IPhotoLifeEntities Context { get; set; }

        public T GetById(object id)
        {
            return this.Set.Find(id);
        }

        public IEnumerable<T> Entities
        {
            get { return this.Set; }
        }

        public IEnumerable<T> GetAll()
        {
            return this.Set;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filterExpression)
        {
            return this.Set
                .Where(filterExpression);
        }

        public IEnumerable<T> GetAll<T1>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression)
        {
            return this.Set
                .Where(filterExpression)
                .OrderBy(sortExpression);
        }

        public IEnumerable<T2> GetAll<T1, T2>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression, Expression<Func<T, T2>> selectExpression)
        {
            return this.Set
                 .Where(filterExpression)
                 .OrderBy(sortExpression)
                 .Select(selectExpression);
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
