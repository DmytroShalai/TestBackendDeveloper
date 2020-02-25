using ERPApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ERPApi.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ProjectsDBContext context = new ProjectsDBContext();
        private GenericRepository<Files> _filesRepository;
        private GenericRepository<Projects> _projectsRepository;
        private GenericRepository<ProjectsSkills> _projectsSkillsRepository;
        private GenericRepository<ProjectType> _projectTypeRepository;
        private GenericRepository<Skills> _skillsRepository;
        /// <summary>Репозиторій для роботи з файлами проектів</summary>
        public GenericRepository<Files> FilesRepository
        {
            get
            {

                if (this._filesRepository == null)
                {
                    this._filesRepository = new GenericRepository<Files>(context);
                }
                return _filesRepository;
            }
        }
        /// <summary>Репозиторій для роботи з проектами</summary>
        public GenericRepository<Projects> ProjectsRepository
        {
            get
            {

                if (this._projectsRepository == null)
                {
                    this._projectsRepository = new GenericRepository<Projects>(context);
                }
                return _projectsRepository;
            }
        }
        /// <summary>Репозиторій для роботи з проміжною таблицею проект/вимоги до виконавця</summary>
        public GenericRepository<ProjectsSkills> ProjectsSkillsRepository
        {
            get
            {

                if (this._projectsSkillsRepository == null)
                {
                    this._projectsSkillsRepository = new GenericRepository<ProjectsSkills>(context);
                }
                return _projectsSkillsRepository;
            }
        }
        /// <summary>Репозиторій для роботи з типами проектів</summary>
        public GenericRepository<ProjectType> ProjectTypeRepository
        {
            get
            {

                if (this._projectTypeRepository == null)
                {
                    this._projectTypeRepository = new GenericRepository<ProjectType>(context);
                }
                return _projectTypeRepository;
            }
        }
        /// <summary>Репозиторій для роботи з вимоги до виконавця</summary>
        public GenericRepository<Skills> SkillsRepository
        {
            get
            {

                if (this._skillsRepository == null)
                {
                    this._skillsRepository = new GenericRepository<Skills>(context);
                }
                return _skillsRepository;
            }
        }
        /// <summary>Зберегти зміни</summary>
        /// <returns></returns>
        public async Task Save()
        {
            try
            {
              await   context.SaveChangesAsync();
            }
            catch (Exception e)
            {
              var a =  e.InnerException;
                throw;
            }
            
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}