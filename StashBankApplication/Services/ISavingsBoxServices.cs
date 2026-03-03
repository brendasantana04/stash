using StashBankApplication.Model;

namespace StashBankApplication.Services
{
    public interface ISavingsBoxServices
    {
        SavingsBox Create(SavingsBox savingsBox);
        SavingsBox FindById(long id);
        List<SavingsBox> FindAll();
        SavingsBox Update(SavingsBox savingsBox);
        void Delete(SavingsBox savingsBox);
        Task<bool> DepositToBox(long accountId, long boxId, decimal amount);
        Task<bool> WithdrawFromBox(long accountId, long boxId, decimal amount);
    }
}
