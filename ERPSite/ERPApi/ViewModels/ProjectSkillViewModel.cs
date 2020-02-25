using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.ViewModels
{

    public class ProjectSkillViewModel
    {
        public int Id { get; set; }
        /// <summary>Назва вимоги до виконавця проекту</summary>
        public string Name { get; set; }
    }
}