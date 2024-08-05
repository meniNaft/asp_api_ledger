using asp_api_ledger.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_api_ledger.DAL
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ledger> Ledgers{ get; set; }
        public DbSet<Transaction>  Transactions{ get; set; }
        public DbSet<TransactionRow>  TransactionRows{ get; set; }
    }
}
