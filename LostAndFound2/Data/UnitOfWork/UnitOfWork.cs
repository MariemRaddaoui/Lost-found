using LostAndFound2.Data.Repositories;
using System.Diagnostics;

namespace LostAndFound2.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _dbContext;
        public IUserRepository UserRepository { get ; set; }
        public IItemRepository ItemRepository { get ; set ; }
        public UnitOfWork(DBContext dBContext)
        {
            _dbContext = dBContext;
            UserRepository = new UserRepository(_dbContext);
            ItemRepository = new ItemRepository(_dbContext);
        }
        public bool Complete()
        {
            try
            {
                int result = _dbContext.SaveChanges();
                if (result > 0) { return true; }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
