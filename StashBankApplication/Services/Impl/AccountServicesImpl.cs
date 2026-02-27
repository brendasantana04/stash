using StashBankApplication.Model;
using StashBankApplication.Model.Context;
using StashBankApplication.Repository;

namespace StashBankApplication.Services.Impl
{
    public class AccountServicesImpl : IAccountServices
    {
        private MSSQLContext _context;
        private IRepository<Account> _repository;

        public AccountServicesImpl(IRepository<Account> repository)
        {
            _repository = repository;
        }
        public List<Account> FindAll()
        {
            return _repository.FindAll();
        }
        public Account FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Account Create(Account account)
        {
            return _repository.Create(account);
        }

        public Account Update(Account account)
        {
            return _repository.Update(account);
        }

        public void Delete(Account account)
        {
            _repository.Delete(account);
        }
    }
}
