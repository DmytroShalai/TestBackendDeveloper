namespace ERPApi.Models
{
    /// <summary>Таблиця зв'язку між проектом і вимогами до виконавця</summary>
    public  class ProjectsSkills
    {
        public int Id { get; set; }
        /// <summary>id проекту</summary>
        public int ProjectId { get; set; }
        /// <summary>id вимоги</summary>
        public int SkillId { get; set; }

        public virtual Projects Projects { get; set; }

        public virtual Skills Skills { get; set; }
    }
}
