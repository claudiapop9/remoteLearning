namespace VendingMachineCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountsMoney2 : DbMigration
    {
        
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        CardNO = c.String(),
                        Pin = c.String(),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.CashMoneys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MoneyValue = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);

            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.CashMoneys");
            DropTable("dbo.Accounts");
        }
    }
}
