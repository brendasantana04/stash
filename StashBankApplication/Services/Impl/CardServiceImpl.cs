using StashBankApplication.Domain.Enums;
using StashBankApplication.Domain.Policies;
using StashBankApplication.Model;
using StashBankApplication.Model.Context;
using StashBankApplication.Repository;

namespace StashBankApplication.Services.Impl
{
    public class CardServiceImpl : ICardService
    {
        private MSSQLContext _context;
        private IRepository<Card> _repository;
        private IRepository<Account> _accountRepository;
        private readonly ICardLimitPolicy _cardLimitPolicy;

        public CardServiceImpl(IRepository<Card> repository,
            IRepository<Account> accountRepository,
            ICardLimitPolicy cardLimitPolicy,
            MSSQLContext context)
        {
            _repository = repository;
            _accountRepository = accountRepository;
            _context = context;
            _cardLimitPolicy = cardLimitPolicy;
        }

        #region CRUD
        public List<Card> FindAll()
        {
            return _repository.FindAll();
        }
        public Card FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Card Create(Card card)
        {
            return _repository.Create(card);
        }

        public Card Update(Card card)
        {
            return _repository.Update(card);
        }

        public void Delete(Card card)
        {
            _repository.Delete(card);
        }

        #endregion

        /// <summary>
        /// método responsável por fazer transações com cartao de crédito, 
        /// verificando se o limite disponível é suficiente para a compra 
        /// e registrando a transação
        /// </summary>
        public async Task MakeCreditTransaction(long accountId, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (account.Card.AvailableCredit < amount)
                throw new Exception("Limite de crédito insuficiente");

            account.Card.AvailableCredit -= amount;

            var transaction = new Transaction
            {
                accountid = accountId,
                value = amount,
                type = TransactionType.Credit,
                createdon = DateTime.UtcNow,
                description = "Compra no crédito"
            };

            await _context.Transactions.AddAsync(transaction);
            await _repository.UpdateAsync(account.Card);
        }

        /// <summary>
        /// Reponsável por processar o pagamento da fatura do cartão de crédito
        /// </summary>
        public async Task PayCreditBill(long accountId, decimal amount)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            if (account.funds < amount)
                throw new Exception("Saldo insuficiente");

            account.funds -= amount;
            account.Card.AvailableCredit += amount;

            if (account.Card.AvailableCredit > account.Card.CreditLimit)
                account.Card.AvailableCredit = account.Card.CreditLimit;

            await _accountRepository.UpdateAsync(account);
        }

        /// <summary>
        /// Método responsável por fazer o upgrade do cartão de crédito, 
        /// aumentando o limite de crédito disponível e o tipo do cartao
        /// </summary>
        public async Task UpgradeCard(long accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId, a => a.Card)
                ?? throw new InvalidOperationException($"Account {accountId} not found.");

            var card = account.Card
                ?? throw new InvalidOperationException($"Account {accountId} has no card.");

            if (card.Tier == CardTier.UltraHighLuxury)
                throw new Exception("Cartão já está no nível máximo");

            card.Tier += 1;
            card.LastUpgradeAt = DateTime.UtcNow;

            var newLimit = _cardLimitPolicy.GetLimit(card.Tier);
            card.CreditLimit = newLimit;
            card.AvailableCredit = newLimit;

            await _repository.UpdateAsync(card);
            Console.WriteLine($"Cartão atualizado para {card.Tier} com limite de {card.CreditLimit:C}");
        }
    }
}
