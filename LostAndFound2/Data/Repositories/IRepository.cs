using System.Linq.Expressions;

namespace LostAndFound2.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity:class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity,bool>> predicate);
        bool Add(TEntity entity);
        bool Delete(TEntity entity);
        bool AddRange(IEnumerable<TEntity> entities);
        bool DeleteRange(IEnumerable<TEntity> entities);
    }
}
