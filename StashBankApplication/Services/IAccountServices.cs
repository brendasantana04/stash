using StashBankApplication.DTOs.Deposit;
using StashBankApplication.Model;

namespace StashBankApplication.Services
{
    public interface IAccountServices
    {
        Account Create(Account account);
        Account FindById(long id);
        List<Account> FindAll();
        Account Update(Account account);
        void Delete(Account account);
        Task DepositAsync(DepositRequest request);
    }
}
