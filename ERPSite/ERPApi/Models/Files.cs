namespace ERPApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    /// <summary>Файл проектів</summary>
    public  class Files
    {

        public int Id { get; set; }
        /// <summary>id проекту</summary>
        public int ProjectId { get; set; }
        /// <summary>Файл </summary>
        [Required]
        public byte[] File { get; set; }
        /// <summary>Назва файлу</summary>
        [Required]
        public string FileName { get; set; }
        /// <summary>Тип файлу</summary>
        [Required]
        public string FileType { get; set; }

        public virtual Projects Projects { get; set; }
    }
}
