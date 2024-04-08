namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommonAbstract : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Posts", "CreatedBy", c => c.String());
            AddColumn("dbo.tb_Posts", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Posts", "ModifierDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Posts", "ModifierBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Posts", "ModifierBy");
            DropColumn("dbo.tb_Posts", "ModifierDate");
            DropColumn("dbo.tb_Posts", "CreatedDate");
            DropColumn("dbo.tb_Posts", "CreatedBy");
        }
    }
}
