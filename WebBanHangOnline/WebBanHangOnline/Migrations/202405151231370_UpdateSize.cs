namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "SizeGiay", c => c.String());
            AddColumn("dbo.tb_Product", "SizeAo", c => c.String());
            AddColumn("dbo.tb_Product", "SizeQuan", c => c.String());
            AddColumn("dbo.tb_Product", "SizeVay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "SizeVay");
            DropColumn("dbo.tb_Product", "SizeQuan");
            DropColumn("dbo.tb_Product", "SizeAo");
            DropColumn("dbo.tb_Product", "SizeGiay");
        }
    }
}
