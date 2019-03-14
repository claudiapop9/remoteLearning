namespace VendingMachineCodeFirst.Migrations
{
    using System.Data.Entity.Migrations;


    internal sealed class Configuration : DbMigrationsConfiguration<VendingMachineCodeFirst.VendMachineDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VendingMachineCodeFirst.VendMachineDbContext context)
        {
            context.Money.AddOrUpdate(x => x.Id,
            new CashMoney() { Id = 1, MoneyValue = 10, Quantity = 10 },
            new CashMoney() { Id = 2, MoneyValue = 5, Quantity = 10 },
            new CashMoney() { Id = 3, MoneyValue = 1, Quantity = 10 },
            new CashMoney() { Id = 4, MoneyValue = 0.5, Quantity = 10 });

            context.Accounts.AddOrUpdate(x => x.AccountId,
            new Account() { AccountId = 1,CardNO = "1234", Pin = "1004",Amount=100 },
            new Account() { AccountId = 2, CardNO = "5678", Pin = "5555", Amount = 50 },
            new Account() { AccountId = 3, CardNO = "4321", Pin = "1234", Amount = 10 }

        );
        }
    }
}
