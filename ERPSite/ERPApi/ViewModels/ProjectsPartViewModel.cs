using ERPApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.ViewModels
{

    public class ProjectsPartViewModel
    {
        public int ID { get; set; }
        /// <summary>Назва проекту</summary>
        public string Title { get; set; }
        /// <summary>Опис проекту</summary>
        public string Description { get; set; }
        /// <summary>Назва компанії</summary>
        public string Organization { get; set; }
        /// <summary>Назва типу проекту</summary>
        public string TypeName { get; set; }
    }
}