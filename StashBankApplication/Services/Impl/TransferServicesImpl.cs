using Microsoft.EntityFrameworkCore;
using StashBankApplication.Domain.Enums;
using StashBankApplication.DTOs.Statement;
using StashBankApplication.DTOs.Transfer;
using StashBankApplication.Model;
using StashBankApplication.Model.Context;
using StashBankApplication.Repository;
using System.Transactions;
using Transaction = StashBankApplication.Model.Transaction;

namespace StashBankApplication.Services.Impl
{
    public class TransferServicesImpl : ITransferServices
    {
        private MSSQLContext _context;
        
        public TransferServicesImpl(MSSQLContext context)
        {
            _context = context;
        }

        public async Task TransferAsync(TransferRequest request)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var contaOrigem = await _context.Accounts
                    .FirstOrDefaultAsync(c => c.id == request.id_account_from);

                var contaDestino = await _context.Accounts
                    .FirstOrDefaultAsync(c => c.id == request.id_account_to);

                if (contaOrigem == null || contaDestino == null)
                    throw new Exception("Conta não encontrada.");

                if (contaOrigem.funds < request.value)
                    throw new Exception("Saldo insuficiente.");

                contaOrigem.funds -= request.value;
                contaDestino.funds += request.value;

                var transfer = new Transfer
                {
                    id_account_from = request.id_account_from,
                    id_account_to = request.id_account_to,
                    value = request.value,
                    createdon = DateTime.UtcNow
                };
                _context.Transfers.Add(transfer);

                var credito = new Transaction
                {
                    accountid = contaDestino.id,
                    value = request.value,
                    type = TransactionType.Credit,
                    description = $"Transfer from account {contaOrigem.id}",
                    createdon = DateTime.UtcNow
                };

                var debito = new Transaction
                {
                    accountid = contaOrigem.id,
                    value= request.value,
                    type = TransactionType.Debit,
                    description = $"Transfer to account {contaDestino.id}",
                    createdon = DateTime.UtcNow
                };

                _context.Transactions.AddRange(credito, debito);

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task<StatementResponse> GetStatementAsync(StatementRequest request)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.id.ToString() == request.AccountId.ToString());

            if (account == null)
                throw new Exception("Account not found");

            if (request.StartDate > request.EndDate)
                throw new Exception("Invalid period");
            
            var transactions = await _context.Transactions
                .Where(t =>
                    t.accountid == account.id &&
                    t.createdon >= request.StartDate &&
                    t.createdon <= request.EndDate)
                .OrderByDescending(t => t.createdon)
                .ToListAsync();

            var response = new StatementResponse
            {
                AccountNumber = long.Parse(account.numeroconta),
                CurrentBalance = account.funds,
                Transactions = transactions.Select(t => new StatementItem
                {
                    Date = t.createdon,
                    Type = t.type.ToString(),
                    Amount = t.type == TransactionType.Debit ? t.value : t.value
                }).ToList()
            };

            Console.WriteLine("Statement generated for account: " + account.numeroconta);
            return response;
        }
    }
}
