using System.Linq;

namespace LibrarySystem.Data.Repository.Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        void Add(T entity);
        void Update(T entity);
    }
}
