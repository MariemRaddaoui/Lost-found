using LostAndFound2.Data.Repositories;

namespace LostAndFound2.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; set; }
        public IItemRepository ItemRepository { get; set; }
        public bool Complete();
    }
}
