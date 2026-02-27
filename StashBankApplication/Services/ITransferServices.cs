using StashBankApplication.Model;

namespace StashBankApplication.Services
{
    public interface ITransferServices
    {
        Task TransferAsync(TransferRequest request);
    }
}
