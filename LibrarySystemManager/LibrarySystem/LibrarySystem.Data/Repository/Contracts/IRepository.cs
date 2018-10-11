using System.Linq;

namespace LibrarySystem.Data.Repository.Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
        IQueryable<T> AllAndDeleted();

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Save();
    }
}
