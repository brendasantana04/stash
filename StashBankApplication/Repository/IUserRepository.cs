using StashBankApplication.Model;

namespace StashBankApplication.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User? FindByUsername(string username);
    }
}
