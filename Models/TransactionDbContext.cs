using Microsoft.EntityFrameworkCore;

namespace BankTransactions.Models {
    public class TransactionDbContext : DbContext {


        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) {

        }

        public DbSet<TransactionCrud> Transactions { get; set; }

    }
}
