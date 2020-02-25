namespace ERPApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "FileName");
        }
    }
}
