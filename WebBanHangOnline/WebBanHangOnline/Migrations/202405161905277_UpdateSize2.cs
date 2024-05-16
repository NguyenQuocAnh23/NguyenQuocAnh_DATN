namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSize2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_OrderDetail", "Size", c => c.String());
            AddColumn("dbo.tb_Order", "Size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "Size");
            DropColumn("dbo.tb_OrderDetail", "Size");
        }
    }
}
