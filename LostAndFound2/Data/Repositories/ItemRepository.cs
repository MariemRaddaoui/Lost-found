using LostAndFound2.Models;
namespace LostAndFound2.Data.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public int GetOwnerId(int id)
        {
            return GetById(id).Id;
        }
    }
}
