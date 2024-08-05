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

        public async Task<List<User>> GetUsers() => await dataContext.Users.ToListAsync();
        public async Task<User> GetUserById(int id) => await dataContext.Users.FirstAsync(u => u.Id == id);
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


        public async Task<List<Ledger>> GetLedgers() => await dataContext.Ledgers.ToListAsync();
        public async Task<Ledger> GetLedgerById(int id) => await dataContext.Ledgers.FirstAsync(l => l.Id == id);
        public void CreateLedger(Ledger ledger)
        {
            dataContext.Ledgers.Add(ledger);
            dataContext.SaveChanges();
        }
        public async void AddUserToLedger(User user,int ledgerId)
        {
            var found = await dataContext.Ledgers.FirstAsync(l => l.Id == ledgerId);
            found.Users.Add(user);
            await dataContext.SaveChangesAsync();
        }
        public async void RemoveUserFromLedger(User user, int ledgerId)
        {
            var found = await dataContext.Ledgers.FirstAsync(l => l.Id == ledgerId);
            found.Users.Remove(user);
            await dataContext.SaveChangesAsync();
        }

        public async Task<User> CreateLedger(User user)
        {
            dataContext.Users.Add(user);
            await dataContext.SaveChangesAsync();
            return user;
        }
        public async Task<User?> UpdateLgder(User user)
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
        public async Task<bool> DeleteLedger(int id)
        {
            var user = await dataContext.Users.FindAsync(id);
            if (user == null) return false;
            dataContext.Users.Remove(user);
            await dataContext.SaveChangesAsync();
            return true;
        }



        //public List<Transaction> GetTransactions() => dataContext.Transactions.Include(t => t.TransactionRows).ToList();
        //public Transaction GetTransactionById(int id) => dataContext.Transactions.Include(t => t.TransactionRows).First(t => t.Id == id);
        //public void CreateTransaction(Transaction transaction)
        //{
        //    dataContext.Transactions.Add(transaction);
        //    dataContext.SaveChanges();
        //}



    }
}
