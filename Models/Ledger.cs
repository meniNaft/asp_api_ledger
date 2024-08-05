namespace asp_api_ledger.Models
{
    public class Ledger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
