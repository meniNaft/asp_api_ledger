using System.Text.Json.Serialization;

namespace asp_api_ledger.Models
{
    public class Ledger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<User> Users { get; set; } = new List<User>();
    }
}
