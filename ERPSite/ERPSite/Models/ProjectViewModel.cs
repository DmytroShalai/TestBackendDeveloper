using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPSite.Models
{
    [Serializable]
    public class SkillViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool flag { get; set; }
    }
    [Serializable]
    public class ProjectTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ProjectsPartViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Название ")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Display(Name = "Описание проекта")]
        [DataType(DataType.MultilineText)]
        [StringLength(100)]
        public string Description { get; set; }
        [Display(Name = "Название компании")]
        [DataType(DataType.Text)]
        public string Organization { get; set; }
        [Display(Name = "Тип проекта")]
        public string TypeName { get; set; }
    }
    public class ProjectDetalInfoViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Название ")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
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
        [Display(Name = "Дата создания")]
        public DateTime? Created { get; set; }
        [Display(Name = "Дата последнего обновления")]
        public DateTime? Updated { get; set; }
        [Display(Name = "Файлы")]
        public List<FileViewModel> Files { get; set; }
        [Display(Name = "Навыки исполнителя")]
        public List<SkillViewModel> ProjectsSkills { get; set; }
    }
    
   [Serializable]
    public class FileViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название файла")]
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] File { get; set; }
    }
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
        public List<SkillViewModel> ProjectsSkills { get; set; }
        public List<ProjectTypeViewModel> ProjectTypes { get; set; }
        public List<FileViewModel> Files { get; set; }
    }
}