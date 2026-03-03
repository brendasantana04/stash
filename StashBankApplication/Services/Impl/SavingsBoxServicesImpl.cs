using Microsoft.EntityFrameworkCore;
using StashBankApplication.Domain.Policies;
using StashBankApplication.Model;
using StashBankApplication.Model.Context;
using StashBankApplication.Repository;

namespace StashBankApplication.Services.Impl
{
    public class SavingsBoxServicesImpl : ISavingsBoxServices
    {
        private MSSQLContext _context;
        private IRepository<SavingsBox> _repository;

        public SavingsBoxServicesImpl(IRepository<SavingsBox> repository,
            MSSQLContext context)
        {
            _repository = repository;
            _context = context;
        }

        #region CRUD
        public List<SavingsBox> FindAll()
        {
            return _repository.FindAll();
        }
        public SavingsBox FindById(long id)
        {
            return _repository.FindById(id);
        }

        public SavingsBox Create(SavingsBox savingsBox)
        {
            return _repository.Create(savingsBox);
        }

        public SavingsBox Update(SavingsBox savingsBox)
        {
            return _repository.Update(savingsBox);
        }

        public void Delete(SavingsBox savingsBox)
        {
            _repository.Delete(savingsBox);
        }

        #endregion

        /// <summary>
        /// depositar dinheiro na caixinha
        /// </summary>
        public async Task<bool> DepositToBox(long accountId, long boxId, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            var box = await _context.SavingsBoxes.FindAsync(boxId);

            if (account == null || box == null)
                return false;

            if (account.funds < amount)
                return false;

            account.funds -= amount;
            box.Balance += amount;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// sacar dinheiro da caixinha
        /// </summary>
        public async Task<bool> WithdrawFromBox(long accountId, long boxId, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            var box = await _context.SavingsBoxes.FindAsync(boxId);

            if (account == null || box == null)
                return false;

            if (box.Balance < amount)
                return false;

            box.Balance -= amount;
            account.funds += amount;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
