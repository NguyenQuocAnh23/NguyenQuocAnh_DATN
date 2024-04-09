﻿namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedDatabase1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_News", "SeoTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_News", "SeoTitle");
        }
    }
}