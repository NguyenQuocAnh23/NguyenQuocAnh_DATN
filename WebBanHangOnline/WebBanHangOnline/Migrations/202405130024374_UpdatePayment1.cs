namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePayment1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "CreatedBy", c => c.String());
            AddColumn("dbo.tb_Order", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Order", "ModifierDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Order", "ModifierBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "ModifierBy");
            DropColumn("dbo.tb_Order", "ModifierDate");
            DropColumn("dbo.tb_Order", "CreatedDate");
            DropColumn("dbo.tb_Order", "CreatedBy");
        }
    }
}
