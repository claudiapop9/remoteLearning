namespace VendingMachineCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pending : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "Date", c => c.DateTime(nullable: false));
        }
    }
}
