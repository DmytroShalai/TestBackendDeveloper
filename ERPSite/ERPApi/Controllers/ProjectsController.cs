using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using ERPApi.DAL;
using ERPApi.Models;
using ERPApi.ViewModels;
using Newtonsoft.Json;

namespace ERPApi.Controllers
{
    /// <summary>
    /// Контроллер роботи з проектами
    /// </summary>
    public class ProjectsController : ApiController
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        /// <summary>Об'єкт для взаємодії з БД</summary>
        public UnitOfWork DB => _unitOfWork ?? new UnitOfWork();
        /// <summary>Повертає масив типів проекту</summary>
        [HttpGet()]
        [Route("api/Project/ProjectTypes")]
        public IEnumerable<ProjectTypesViewModel> GetProjectTypes()
        {
            var items = DB.ProjectTypeRepository.Get(orderBy: q => q.OrderBy(d => d.Name)).Select(s => new { s.Id, s.Name }).ToList();
            var projectTypes = new List<ProjectTypesViewModel>();
            foreach (var item in items)
            {
                projectTypes.Add(new ProjectTypesViewModel { Id = item.Id, Name = item.Name });
            }
            return projectTypes;
        }
        /// <summary>Повертає масив вимог до виконавця</summary>
        [HttpGet()]
        [Route("api/Project/ProjectSkills")]
        public IEnumerable<ProjectSkillViewModel> GetProjectSkills()
        {
            var items = DB.SkillsRepository.Get(orderBy: q => q.OrderBy(d => d.Name)).Select(s => new { s.Id, s.Name }).ToList();
            var projectSkills = new List<ProjectSkillViewModel>();
            foreach (var item in items)
            {
                projectSkills.Add(new ProjectSkillViewModel { Id = item.Id, Name = item.Name });
            }
            return projectSkills;
        }
        /// <summary>Повертає масив проектів</summary>
        /// <param name="startPosition">Початкова позиція</param>
        /// <param name="count">Скільки проектів повернути</param>
        /// <param name="title">Пошук по назві проекту</param>
        /// <param name="organization">пошук по назві компанії</param>
        /// <param name="typeId">Фільтрація по типу проекту</param>
        [HttpGet()]
        [Route("api/ProjectsShortInfo/{startPosition=0}/{count=99999999999999}/{title=!@^$##}/{organization==!@^$##}/{typeId=-1}")]
        public IEnumerable<ProjectsPartViewModel> GetProjectsShortInfo(int startPosition, int count, string title, string organization, int typeId)
        {
            List<Expression<Func<Projects, bool>>> filters = new List<Expression<Func<Projects, bool>>>();
            if (!title.Equals("!@^$##"))
            {
                filters.Add((s) => s.Title.Contains(title));
            }
            if (!organization.Equals("!@^$##"))
            {
                filters.Add((s) => s.Organization.Contains(organization));
            }
            if (typeId != -1)
            {
                filters.Add((s) => s.TypeId == typeId);
            }
            var items = new List<ProjectsPartViewModel>();
            var projects = DB.ProjectsRepository.Get(filter: filters, orderBy: q => q.OrderBy(d => d.Title)).Skip(startPosition).Take(count).Select(s => new { s.ID, s.Title, s.Description, s.Organization, s.ProjectType.Name }).ToList();
            foreach (var item in projects)
            {
                items.Add(new ProjectsPartViewModel { ID = item.ID, Description = item.Description, Organization = item.Organization, Title = item.Title, TypeName = item.Name });
            }
            return items;
        }
        /// <summary>Повертає детальну інформацію по проекту</summary>
        /// <param name="Id">id проекту</param>
        [HttpGet()]
        [Route("api/ProjectDetalInfo/{Id=-1}")]
        public ProjectViewModel ProjectDetalInfo(int Id)
        {
            if (Id == -1)
                return null;

            var projectItem = DB.ProjectsRepository.GetByID(Id);
            ProjectViewModel project = null;

            project = new ProjectViewModel
            {
                ID = projectItem.ID,
                Created = projectItem.Created,
                Description = projectItem.Description,
                End = projectItem.End,
                Organization = projectItem.Organization,
                ProjectTypeName = projectItem.ProjectType.Name,
                Updated = projectItem.Updated,
                Start = projectItem.Start,
                Title = projectItem.Title
            };

            if (projectItem != null)
            {
                var skill = new List<ProjectSkillViewModel>();
                if (projectItem.ProjectsSkills != null)
                {
                    foreach (var item in projectItem.ProjectsSkills)
                    {
                        skill.Add(new ProjectSkillViewModel { Id = item.SkillId, Name = item.Skills.Name });
                    }
                    project.ProjectsSkills = skill;
                }

                if (projectItem.Files != null)
                {
                    var files = new List<FileViewModel>();
                    foreach (var item in projectItem.Files)
                    {
                        files.Add(new FileViewModel { Id = item.Id, FileName = item.FileName });
                    }
                    project.Files = files;
                }
            }
            return project;
        }
        /// <summary>
        /// Створює новий проект
        /// </summary>
        /// <param name="value">Об'єкт типу ProjectCreateViewModel сереалізований в JSON</param>
        [HttpPost()]
        [Route("api/ProjectCreate")]
        public async Task<bool> PostProjectCreate(dynamic value)
        {
            try
            {
                ProjectCreateViewModel bsObj = JsonConvert.DeserializeObject<ProjectCreateViewModel>(value.ToString());
                int.TryParse(bsObj.ProjectTypeID, out int typeId);
                var project = new Projects
                {
                    ID = 1,
                    Title = bsObj.Title,
                    Description = bsObj.Description,
                    Start = bsObj.Start,
                    End = bsObj.End,
                    Organization = bsObj.Organization,
                    TypeId = typeId,
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                DB.ProjectsRepository.Insert(project);
                await DB.Save();
                if (bsObj.Files != null)
                {
                    foreach (var item in bsObj.Files)
                    {
                        DB.FilesRepository.Insert(new Files
                        {
                            FileName = item.FileName,
                            FileType = item.FileType,
                            ProjectId = project.ID,
                            Projects = project,
                            File = item.File
                        });
                    }

                    await DB.Save();
                }
                if (bsObj.ProjectsSkills != null)
                {
                    foreach (var item in bsObj.ProjectsSkills)
                    {
                        DB.ProjectsSkillsRepository.Insert(new ProjectsSkills
                        {
                            SkillId = item.Id,
                            ProjectId = project.ID,
                            Projects = project
                        });
                    }
                    await DB.Save();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Видаляє проект
        /// </summary>
        /// <param name="projectId">id проекту</param>
        [HttpDelete()]
        [Route("api/Project/Remove/{projectId}")]
        public async Task<bool> DeleteProject(int projectId)
        {
            DB.ProjectsRepository.Delete(projectId);
            await DB.Save();
            return true;
        }
        /// <summary>Повкртає файл проекту</summary>
        /// <param name="fileId">id - файлу</param>
        [HttpGet()]
        [Route("api/ProjectFile/{fileId}")]
        public FileViewModel GetProjectFile(int fileId)
        {
            var fileItem = DB.FilesRepository.GetByID(fileId);
            var file = new FileViewModel
            {
                Id = fileItem.Id,
                File = fileItem.File,
                FileName = fileItem.FileName,
                FileType = fileItem.FileType
            };
            return file;
        }
        /// <summary>
        /// Оновлює дані по проекту
        /// </summary>
        /// <param name="value">Об'єкт типу ProjectViewModel сереалізований в JSON </param>
        /// <returns></returns>
        [HttpPut()]
        [Route("api/ProjectUpdate")]
        public async Task<bool> PutProjectUpdate(dynamic value)
        {
            try
            {
                ProjectViewModel obj = JsonConvert.DeserializeObject<ProjectViewModel>(value.ToString());
                var project = DB.ProjectsRepository.GetByID(obj.ID);
                project.Title = obj.Title;
                project.Description = obj.Description;
                project.Organization = obj.Organization;
                project.Start = obj.Start;
                project.End = obj.End;
                DB.ProjectsRepository.Update(project);
                await DB.Save();
                if (obj.Files != null)
                {
                    foreach (var item in obj.Files)
                    {
                        DB.FilesRepository.Insert(new Files
                        {
                            FileName = item.FileName,
                            FileType = item.FileType,
                            ProjectId = project.ID,
                            Projects = project,
                            File = item.File
                        });
                    }
                    await DB.Save();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

    }
}