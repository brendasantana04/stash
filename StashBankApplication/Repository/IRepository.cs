using StashBankApplication.Model;
using StashBankApplication.Model.Base;

namespace StashBankApplication.Repository
{
    public interface IRepository <T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(T item);
    }
}
