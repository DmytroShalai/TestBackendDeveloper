namespace ERPApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        File = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 2000),
                        Organization = c.String(),
                        Start = c.DateTime(),
                        End = c.DateTime(),
                        TypeId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectType", t => t.TypeId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.ProjectsSkills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "TypeId", "dbo.ProjectType");
            DropForeignKey("dbo.ProjectsSkills", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectsSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.Files", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectsSkills", new[] { "SkillId" });
            DropIndex("dbo.ProjectsSkills", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "TypeId" });
            DropIndex("dbo.Files", new[] { "ProjectId" });
            DropTable("dbo.ProjectType");
            DropTable("dbo.Skills");
            DropTable("dbo.ProjectsSkills");
            DropTable("dbo.Projects");
            DropTable("dbo.Files");
        }
    }
}
