namespace StashBankApplication.DTOs.Deposit
{
    public class DepositRequest
    {
        public long AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
