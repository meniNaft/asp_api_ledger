namespace asp_api_ledger.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Ledger> Ledgers { get; set; }
    }
}
