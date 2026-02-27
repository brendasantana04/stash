using StashBankApplication.DTOs.Statement;
using StashBankApplication.DTOs.Transfer;

namespace StashBankApplication.Services
{
    public interface ITransferServices
    {
        Task TransferAsync(TransferRequest request);
        Task<StatementResponse> GetStatementAsync(StatementRequest request);
    }
}
