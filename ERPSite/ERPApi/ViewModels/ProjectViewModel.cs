using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.ViewModels
{

    public class ProjectViewModel
    {
        public int ID { get; set; }
        /// <summary>Назва проекту</summary>
        public string Title { get; set; }
        /// <summary>Опис проекту</summary>
        public string Description { get; set; }
        /// <summary>Назва компанії</summary>
        public string Organization { get; set; }
        /// <summary>Дата початку проекту</summary>
        public DateTime? Start { get; set; }
        /// <summary>Дата завершення проекту</summary>
        public DateTime? End { get; set; }
        /// <summary>Назва типу проекту</summary>
        public string ProjectTypeName { get; set; }
        /// <summary>Дата створення проекту</summary>
        public DateTime? Created { get; set; }
        /// <summary>Дата останнього оновлення проекту</summary>
        public DateTime? Updated { get; set; }
        /// <summary>Файли проекту</summary>
        public virtual IEnumerable<FileViewModel> Files { get; set; }
        /// <summary>Вимоги до виконавця проекту</summary>
        public virtual IEnumerable<ProjectSkillViewModel> ProjectsSkills { get; set; }
        /// <summary>id типу проекту</summary>
        public string ProjectTypeID { get; set; }
        /// <summary>Типи проектів</summary>
        public List<ProjectTypesViewModel> ProjectTypes { get; set; }
    }
}