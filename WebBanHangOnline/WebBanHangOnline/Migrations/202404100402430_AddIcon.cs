namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_ProductCategory", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_ProductCategory", "Icon");
        }
    }
}
