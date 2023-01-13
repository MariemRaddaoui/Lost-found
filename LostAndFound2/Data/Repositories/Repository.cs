using System.Diagnostics;
using System.Linq.Expressions;

namespace LostAndFound2.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DBContext _dbContext;
        public Repository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(TEntity entity)
        {
            try
            {
                _dbContext.Set<TEntity>().Add(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return true;
        }

        public bool AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbContext.Set<TEntity>().AddRange(entities);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return true;
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return true;
        }

        public bool DeleteRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbContext.Set<TEntity>().RemoveRange(entities);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return true;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id) ;
        }
        public IEnumerable<TEntity> GetLimitedNumber(int pageIndex, int pageSize)
        {
            return _dbContext.Set<TEntity>().Skip(pageSize*(pageIndex-1)).Take(pageSize);
        }

    }
}
