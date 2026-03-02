using StashBankApplication.Model;
using StashBankApplication.Model.Base;
using System.Linq.Expressions;

namespace StashBankApplication.Repository
{
    public interface IRepository <T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(T item);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByIdAsync(long id);
        Task<T?> GetByIdAsync(
            long id,
            params Expression<Func<T, object>>[] includes
        );

    }
}
