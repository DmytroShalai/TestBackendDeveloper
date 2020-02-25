namespace ERPApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    /// <summary>Вимоги до виконавця проекту</summary>
    public  class Skills
    {
        public Skills()
        {
            ProjectsSkills = new HashSet<ProjectsSkills>();
        }

        public int Id { get; set; }
        /// <summary>Назва вимоги до виконавця проекту</summary>
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<ProjectsSkills> ProjectsSkills { get; set; }
    }
}
