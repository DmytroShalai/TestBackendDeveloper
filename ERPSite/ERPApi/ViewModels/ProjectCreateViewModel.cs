using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPApi.ViewModels
{
    [Serializable]
    public class ProjectCreateViewModel
    {
        [Required]
        [Display(Name = "Название ")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Описание проекта")]
        [DataType(DataType.MultilineText)]
        [StringLength(100)]
        public string Description { get; set; }
        [Display(Name = "Название компании")]
        [DataType(DataType.Text)]
        public string Organization { get; set; }
        [Display(Name = "Старт проекта")]
        [DataType(DataType.Date)]
        public DateTime? Start { get; set; }
        [Display(Name = "Окончание проекта")]
        [DataType(DataType.Date)]
        public DateTime? End { get; set; }
        [Display(Name = "Тип проекта")]
        public string ProjectTypeName { get; set; }
        public string ProjectTypeID { get; set; }
        [Display(Name = "Навыки исполнителя")]
        public List<ProjectSkillViewModel> ProjectsSkills { get; set; }
        public List<ProjectTypesViewModel> ProjectTypes { get; set; }
        public List<FileViewModel> Files { get; set; }
    }
}