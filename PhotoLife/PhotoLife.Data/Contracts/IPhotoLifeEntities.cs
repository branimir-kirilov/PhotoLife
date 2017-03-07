using System.Data.Entity;

namespace PhotoLife.Data.Contracts
{
    public interface IPhotoLifeEntities
    {
        IDbSet<TEntity> DbSet<TEntity>()
         where TEntity : class;

        int SaveChanges();

        void SetAdded<TEntry>(TEntry entity)
            where TEntry : class;

        void SetDeleted<TEntry>(TEntry entity)
            where TEntry : class;

        void SetUpdated<TEntry>(TEntry entity)
            where TEntry : class;
    }
}
