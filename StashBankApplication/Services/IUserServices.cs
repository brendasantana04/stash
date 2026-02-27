using StashBankApplication.Model;

namespace StashBankApplication.Services
{
    public interface IUserServices
    {
        User Create(User user);
        User FindById(long id);
        List<User> FindAll();
        User Update(User user);
        void Delete(User user);
    }
}
