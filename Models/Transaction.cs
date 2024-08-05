namespace asp_api_ledger.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalAmount { get; set; }
        public int LedgerId { get; set; }
        public Ledger  Ledger { get; set; }
        public List<TransactionRow>  TransactionRows { get; set; }
    }
}
