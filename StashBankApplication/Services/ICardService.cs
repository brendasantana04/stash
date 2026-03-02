using StashBankApplication.Model;

namespace StashBankApplication.Services
{
    public interface ICardService 
    {
        Card Create(Card card);
        Card FindById(long id);
        List<Card> FindAll();
        Card Update(Card card);
        void Delete(Card card);
        Task MakeCreditTransaction(long accountId, decimal amount);
        Task PayCreditBill(long accountId, decimal amount);
        Task UpgradeCard(long accountId);
    }
}
