using LostAndFound2.Models;

namespace LostAndFound2.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DBContext dbContext) : base(dbContext)
        {
        }
    }
}
