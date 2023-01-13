using LostAndFound2.Models;

namespace LostAndFound2.Data.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        int GetOwnerId(int id);
    }
}
