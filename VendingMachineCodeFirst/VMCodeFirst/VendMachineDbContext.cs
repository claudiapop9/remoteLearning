using System.Data.Entity;
using System.Data.Entity.SqlServer;


namespace VendingMachineCodeFirst
{
    [DbConfigurationType(typeof(MyDbContextConfiguration))]
    class VendMachineDbContext:DbContext
    {
        public VendMachineDbContext(): base("name=VendMachineDbContext")
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<CashMoney> Money { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set;}
    }

    internal class MyDbContextConfiguration : DbConfiguration
    {
        public MyDbContextConfiguration()
        {
            SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
        }
    }


}
