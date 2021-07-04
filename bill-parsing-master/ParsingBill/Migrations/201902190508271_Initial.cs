namespace ParsingBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bills",
                c => new
                    {
                        bill_id = c.Int(nullable: false, identity: true),
                        dep_id = c.Short(nullable: false),
                        bill_num = c.Short(nullable: false),
                        bill_date = c.DateTime(nullable: false, precision: 0),
                        term_num = c.Int(nullable: false),
                        card_num = c.String(unicode: false),
                        client = c.String(unicode: false),
                        sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.bill_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.bills");
        }
    }
}
