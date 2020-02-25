namespace ERPApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "FileType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "FileType");
        }
    }
}
