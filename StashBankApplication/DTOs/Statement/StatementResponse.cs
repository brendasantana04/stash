namespace StashBankApplication.DTOs.Statement
{
   public class StatementResponse
    {
        public long AccountNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        public List<StatementItem> Transactions { get; set; }
    }

    public class StatementItem
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
