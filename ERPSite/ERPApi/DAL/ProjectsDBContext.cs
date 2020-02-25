namespace ERPApi
{
    using ERPApi.Models;
    using System.Data.Entity;
    using System.Linq;

    public partial class ProjectsDBContext : DbContext
    {
        public ProjectsDBContext()
            : base("DefaultConnection")
        {
        }
        /// <summary>Таблиця з файлами проекту</summary>
        public virtual DbSet<Files> Files { get; set; }
        /// <summary>Таблиця з проектами</summary>
        public virtual DbSet<Projects> Projects { get; set; }
        /// <summary>проміжна таблиця зв'язку проект/вимоги до виконавця</summary>
        public virtual DbSet<ProjectsSkills> ProjectsSkills { get; set; }
        /// <summary>Таблиця типів проектів </summary>
        public virtual DbSet<ProjectType> ProjectType { get; set; }
        /// <summary>Вимоги до виконавців проекту</summary>
        public virtual DbSet<Skills> Skills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Projects>()
                .HasMany(e => e.Files)
                .WithRequired(e => e.Projects)
                .HasForeignKey(e => e.ProjectId);

            modelBuilder.Entity<Projects>()
                .HasMany(e => e.ProjectsSkills)
                .WithRequired(e => e.Projects)
                .HasForeignKey(e => e.ProjectId);

            modelBuilder.Entity<ProjectType>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.ProjectType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Skills>()
                .HasMany(e => e.ProjectsSkills)
                .WithRequired(e => e.Skills)
                .HasForeignKey(e => e.SkillId);
        }
    }
}
