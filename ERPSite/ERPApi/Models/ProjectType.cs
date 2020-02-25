namespace ERPApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("ProjectType")]
    /// <summary>Тип проекту</summary>
    public class ProjectType
    {
        public ProjectType()
        {
            Projects = new HashSet<Projects>();
        }

        public int Id { get; set; }
        /// <summary>Назва типу проекту</summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
    }
}
