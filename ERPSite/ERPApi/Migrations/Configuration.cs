namespace ERPApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ERPApi.ProjectsDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ERPApi.ProjectsDBContext context)
        {
            //context.ProjectType.Add(new Models.ProjectType {Id=1, Name = "Work" });
            //context.ProjectType.Add(new Models.ProjectType {Id=2, Name = "Book" });
            //context.ProjectType.Add(new Models.ProjectType {Id=3, Name = "Course" });
            //context.ProjectType.Add(new Models.ProjectType {Id=4, Name = "Blog" });
            //context.ProjectType.Add(new Models.ProjectType {Id=5, Name = "Other" });
            //context.Skills.Add(new Models.Skills {Id=1, Name = "������ ������ � �����������" });
            //context.Skills.Add(new Models.Skills {Id=2, Name = "������������ ��������" });
            //context.Skills.Add(new Models.Skills {Id=3, Name = "������ ����������" });
            //context.Skills.Add(new Models.Skills {Id=4, Name = "����-����������" });
            //context.Skills.Add(new Models.Skills {Id=5, Name = "���������� ��������" });
            //context.Skills.Add(new Models.Skills {Id=6, Name = "����������� ��������" });
            //context.Skills.Add(new Models.Skills {Id=7, Name = "������� �������" });
            //context.SaveChanges();

        }
    }
}
