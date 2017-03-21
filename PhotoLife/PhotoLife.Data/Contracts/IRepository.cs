using System.Linq;

namespace PhotoLife.Data.Contracts
{
    public interface IRepository<T>
         where T : class
    {
        T GetById(object id);
        IQueryable<T> GetAll { get; }

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
