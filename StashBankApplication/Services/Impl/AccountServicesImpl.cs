using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StashBankApplication.Domain.Enums;
using StashBankApplication.DTOs.Deposit;
using StashBankApplication.Model;
using StashBankApplication.Model.Context;
using StashBankApplication.Repository;
using System.Security.Cryptography.Xml;

namespace StashBankApplication.Services.Impl
{
    public class AccountServicesImpl : IAccountServices
    {
        private MSSQLContext _context;
        private IRepository<Account> _repository;
        private IRepository<Card> _cardRepository;

        public AccountServicesImpl(IRepository<Account> repository,
            IRepository<Card> cardRepository,
            MSSQLContext context)
        {
            _repository = repository;
            _cardRepository = cardRepository;
            _context = context;
        }

        #region CRUD
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

        #endregion

        public async Task DepositAsync(DepositRequest request)
        {
            if (request.Amount <= 0)
                throw new Exception("Amount must be greater than zero.");

            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.id == request.AccountId);

            if (account == null)
                throw new Exception("Account not found.");

            account.funds += request.Amount;

            var transaction = new Transaction
            {
                accountid = account.id,
                value = request.Amount,
                type = TransactionType.Credit,
                createdon = DateTime.UtcNow
            };
            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();
        }
    }
}
