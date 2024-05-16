namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSize1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "Size", c => c.String());
            DropColumn("dbo.tb_Product", "SizeGiay");
            DropColumn("dbo.tb_Product", "SizeAo");
            DropColumn("dbo.tb_Product", "SizeQuan");
            DropColumn("dbo.tb_Product", "SizeVay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Product", "SizeVay", c => c.String());
            AddColumn("dbo.tb_Product", "SizeQuan", c => c.String());
            AddColumn("dbo.tb_Product", "SizeAo", c => c.String());
            AddColumn("dbo.tb_Product", "SizeGiay", c => c.String());
            DropColumn("dbo.tb_Product", "Size");
        }
    }
}
