namespace ERPApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>Проект</summary>
    public  class Projects
    {
        public Projects()
        {
            Files = new HashSet<Files>();
            ProjectsSkills = new HashSet<ProjectsSkills>();
        }

        public int ID { get; set; }
        /// <summary>Назва проекту</summary>
        [Required]
        public string Title { get; set; }
        /// <summary>Опис проекту</summary>
        [Required]
        [StringLength(2000)]
        public string Description { get; set; }
        /// <summary>Назва компанії</summary>
        public string Organization { get; set; }
        /// <summary>Дата початку проекту</summary>
        public DateTime? Start { get; set; }
        /// <summary>Дата завершення проекту</summary>
        public DateTime? End { get; set; }
        /// <summary>id типу проекту</summary>
        public int TypeId { get; set; }
        /// <summary>Дата створення проекту</summary>
        public DateTime Created { get; set; }
        /// <summary>Дата останнього оновлення проекту</summary>
        public DateTime Updated { get; set; }

        public virtual ICollection<Files> Files { get; set; }

        public virtual ProjectType ProjectType { get; set; }

        public virtual ICollection<ProjectsSkills> ProjectsSkills { get; set; }
    }
}
