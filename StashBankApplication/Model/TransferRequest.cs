namespace StashBankApplication.Model
{
    public class TransferRequest : Transfer
    {
        public long id_account_to { get; set; }
        public long id_account_from { get; set; }
        public decimal value { get; set; }
    }
}
