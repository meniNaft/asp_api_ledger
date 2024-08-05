using asp_api_ledger.DAL;
using asp_api_ledger.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_api_ledger.BL
{
    public class Logic
    {
        DataContext dataContext;
        public Logic( DataContext context) 
        {
            dataContext = context;
        }

        public Task<List<User>> GetUsers() => dataContext.Users.ToListAsync();
        public Task<User> GetUserById(int id) => dataContext.Users.FirstAsync(u => u.Id == id);
        public async Task<User> CreateUser(User user) 
        { 
            dataContext.Users.Add(user); 
            await dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUser(User user)
        {
            dataContext.Entry(user).State = EntityState.Modified;
            try
            {
                await dataContext.SaveChangesAsync();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await dataContext.Users.FindAsync(id);
            if (user == null) return false;
            dataContext.Users.Remove(user);
            await dataContext.SaveChangesAsync();
            return true;
        }


        public List<Ledger> GetLedgers() => dataContext.Ledgers.ToList();
        public Ledger GetLedgerById(int id) => dataContext.Ledgers.First(l => l.Id == id);
        public void CreateLedger(Ledger ledger)
        {
            dataContext.Ledgers.Add(ledger);
            dataContext.SaveChanges();
        }

        public List<Transaction> GetTransactions() => dataContext.Transactions.Include(t => t.TransactionRows).ToList();
        public Transaction GetTransactionById(int id) => dataContext.Transactions.Include(t => t.TransactionRows).First(t => t.Id == id);
        public void CreateTransaction(Transaction transaction)
        {
            dataContext.Transactions.Add(transaction);
            dataContext.SaveChanges();
        }



    }
}
