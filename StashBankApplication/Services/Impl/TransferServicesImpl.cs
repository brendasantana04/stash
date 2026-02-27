using Microsoft.EntityFrameworkCore;
using StashBankApplication.Domain.Enums;
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
                    .FirstOrDefaultAsync(c => c.id == request.id_account_to);

                var contaDestino = await _context.Accounts
                    .FirstOrDefaultAsync(c => c.id == request.id_account_from);

                if (contaOrigem == null || contaDestino == null)
                    throw new Exception("Conta não encontrada.");

                if (contaOrigem.funds < request.value)
                    throw new Exception("Saldo insuficiente.");

                contaOrigem.funds -= request.value;
                contaDestino.funds += request.value;

                var credito = new Transaction
                {
                    accountid = contaOrigem.id,
                    value = request.value,
                    type = TransactionType.Credit,
                    createdon = DateTime.UtcNow
                };

                var debito = new Transaction
                {
                    accountid = contaDestino.id,
                    value= request.value,
                    type = TransactionType.Debit,
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
    }
}
