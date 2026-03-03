namespace StashBankApplication.DTOs.Deposit
{
    public class DepositBoxRequest
    {
        public long AccountId { get; set; }

        public long BoxId { get; set; }

        public decimal Amount { get; set; }
    }
}
