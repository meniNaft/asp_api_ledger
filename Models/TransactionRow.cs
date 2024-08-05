namespace asp_api_ledger.Models
{
    public class TransactionRow
    {
        public int Id { get; set; }
        public Transaction Transaction{ get; set; }
        public User  User{ get; set; }
        public bool IsSplit { get; set; }
        public double amount { get; set; }
    }
}
