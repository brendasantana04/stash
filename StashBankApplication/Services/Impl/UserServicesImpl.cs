using StashBankApplication.Model;
using StashBankApplication.Model.Context;
using StashBankApplication.Repository;
using StashBankApplication.Services;

namespace StashBankApplication.Services.Impl
{
    public class UserServicesImpl : IUserServices
    {
        private MSSQLContext _context;
        private IRepository<User> _repository;

        public UserServicesImpl (IRepository<User> repository)
        {
            _repository= repository;
        }
        public List<User> FindAll()
        {
            return _repository.FindAll();
        }
        public User FindById(long id)
        {
            return _repository.FindById(id);
        }

        public User Create(User user)
        {
            return _repository.Create(user);
        }

        public User Update(User user)
        {
            return _repository.Update(user);
        }

        public void Delete(User user)
        {
            _repository.Delete(user);
        }
    }
}
