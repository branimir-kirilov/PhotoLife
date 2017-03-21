using System;
using System.Linq;
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

        public IQueryable<T> GetAll
        {
            get { return this.Context.DbSet<T>(); }
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
