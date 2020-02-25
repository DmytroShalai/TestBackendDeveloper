using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.ViewModels
{

    public class FileViewModel
    {
        public int Id { get; set; }
        /// <summary>Назва файлу</summary>
        public string FileName { get; set; }
        /// <summary>Тип файлу</summary>
        public string FileType { get; set; }
        /// <summary>Файл</summary>
        public byte[] File { get; set; }
    }
}