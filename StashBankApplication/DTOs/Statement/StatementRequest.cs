namespace StashBankApplication.DTOs.Statement
{
    public class StatementRequest
    {
        public long AccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
