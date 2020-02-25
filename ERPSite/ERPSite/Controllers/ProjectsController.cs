using ClosedXML.Excel;
using ERPSite.Models;
using ERPSite.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ERPSite.Controllers
{
    /// <summary>Контроллер роботи з проектами</summary>
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IProjectDataStore _dataStore;
        public ProjectsController()
        {


        }
        public ProjectsController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        /// <summary>Менеджер користувачів</summary>
        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        /// <summary>Менеджер ролей користувачів</summary>
        protected ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        /// <summary>Об'єкт для роботи з даними проектів</summary>
        public IProjectDataStore DataStore
        {
            get
            {
                return _dataStore ?? new ProjectsDataStore();
            }
        }

        public async Task<ActionResult> Index()
        {
            var items = await DataStore.GetTypesAsync();
            return View(items);
        }
        /// <summary>Часткова підгрузка списку проектів </summary>
        [HttpPost]
        public async Task<ActionResult> InitiateProjectsPart(int startPosition, int count, string title, string organization, int typeId)
        {
            var projectsPart = await DataStore.GetProjectsPartAsync(startPosition, count, title, organization, typeId);

            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);
            var roleId = user.Roles.FirstOrDefault().RoleId;
            var RolesName = RoleManager.Roles.FirstOrDefault(r => r.Id == roleId).Name;
            if (RolesName.Equals("Администратор"))
            {
                return PartialView("~/Views/Projects/ProjectsPartAdmin.cshtml", projectsPart);
            }
            else
                return PartialView("~/Views/Projects/ProjectsPart.cshtml", projectsPart);

        }
        /// <summary>Видалення проекту</summary>
        [HttpPost]
        public async Task<string> RemoveProject(int projectId)
        {
            var projectsPart = await DataStore.RemoveProjectAsync(projectId);
            return "true";
        }
        /// <summary>Детальна інформація про проект</summary>
        public async Task<ActionResult> ProjectDetalInfo(int id)
        {
            var items = await DataStore.GetProjectDetalInfoAsync(id);
            return View(items);
        }
        /// <summary>Редагування інформації про проект</summary>
        public async Task<ActionResult> ProjectDetalInfoEdit(int id)
        {
            var items = await DataStore.GetProjectDetalInfoAsync(id);
            return View(items);
        }
        [HttpPost]
        public async Task<ActionResult> ProjectDetalInfoEdit(List<HttpPostedFileBase> files, ProjectDetalInfoViewModel projectInfo)
        {
            var volid = ModelState.IsValid;
            if (projectInfo.Title != null && projectInfo.Title.Trim().Length > 0 && projectInfo.Description != null
                && projectInfo.Description.Trim().Length > 0)
            {
                foreach (var item in files)
                {
                    if (item == null)
                        break;
                    byte[] bytes = null;
                    using (BinaryReader br = new BinaryReader(item.InputStream))
                    {
                        bytes = br.ReadBytes(item.ContentLength);
                    }
                    projectInfo.Files.Add(new FileViewModel { FileName = item.FileName, FileType = item.ContentType, File = bytes });

                }
                projectInfo.Files.Remove(null);

                var data = Newtonsoft.Json.JsonConvert.SerializeObject(projectInfo);
                if (data != null)
                    await DataStore.UpdateProjectAsync(data);

                var items = await DataStore.GetTypesAsync();
                return View("Index", items);
            }
            return View(projectInfo);
        }
        public async Task<ActionResult> ProjectCreate()
        {
            var project = new ProjectCreateViewModel();
            project.ProjectTypes = await DataStore.GetTypesAsync();
            project.ProjectsSkills = await DataStore.GetSkillsAsync();

            return View(project);
        }

        [HttpPost]
        public async Task<ActionResult> ProjectCreate(List<HttpPostedFileBase> files, ProjectCreateViewModel project)
        {
            var volid = ModelState.IsValid;
            if (project.Title != null && project.Title.Trim().Length > 0 && project.Description != null
                && project.Description.Trim().Length > 0)
            {

                foreach (var item in files)
                {
                    if (item == null)
                        break;
                    byte[] bytes = null;
                    using (BinaryReader br = new BinaryReader(item.InputStream))
                    {
                        bytes = br.ReadBytes(item.ContentLength);
                    }
                    project.Files.Add(new FileViewModel { FileName = item.FileName, FileType = item.ContentType, File = bytes });

                }
                project.ProjectsSkills.RemoveAll(w => !w.flag);
                project.Files.Remove(null);

                var data = Newtonsoft.Json.JsonConvert.SerializeObject(project);
                if (data != null)
                    await DataStore.CreateProjectAsync(data);


                var items = await DataStore.GetTypesAsync();

                return View("Index", items);
            }
            project.ProjectTypes = await DataStore.GetTypesAsync();
            project.ProjectsSkills = await DataStore.GetSkillsAsync();
            return View(project);
        }
        public async Task<FileResult> LoadFile(int fileId)
        {
            var file = await DataStore.GetProjecFileAsync(fileId);

            return File(file.File, file.FileType, file.FileName);

        }
        public ActionResult ProjectCreateWithFile()
        {
            return View();
        }

        /// <summary>Створення проектів з файлу excel </summary>
        [HttpPost]
        public async Task<ActionResult> ProjectCreateWithFile(HttpPostedFileBase fileExcel)
        {
            var projectTypes = await DataStore.GetTypesAsync();
            var projectsSkills = await DataStore.GetSkillsAsync();

            using (XLWorkbook workBook = new XLWorkbook(fileExcel.InputStream, XLEventTracking.Disabled))
            {
                foreach (IXLWorksheet worksheet in workBook.Worksheets)
                {
                    ProjectCreateViewModel project = new ProjectCreateViewModel();
                    project.Title = worksheet.Name;

                    foreach (IXLColumn column in worksheet.ColumnsUsed())
                    {
                        IXLRow firstRow = worksheet.RowsUsed().Skip(1).First();
                        project.Description = firstRow.Cell(1).Value.ToString();
                        project.Organization = firstRow.Cell(2).Value.ToString();
                        project.Start = (DateTime)firstRow.Cell(3).Value;
                        project.End = (DateTime)firstRow.Cell(4).Value;
                        project.ProjectTypeID = projectTypes.Where(w => w.Name == firstRow.Cell(5).Value.ToString()).ToList().FirstOrDefault()?.Id.ToString();

                        project.ProjectsSkills = new List<SkillViewModel>();
                        foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                        {
                            try
                            {
                                var skill = projectsSkills.Where(w => w.Name == row.Cell(6).Value.ToString()).ToList().FirstOrDefault()?.Id;
                                if (skill != null)
                                    project.ProjectsSkills.Add(new SkillViewModel { Id = (int)skill });

                            }
                            catch (Exception e)
                            {
                                //logging
                            }
                        }
                    }
                    var data = Newtonsoft.Json.JsonConvert.SerializeObject(project);
                    if (data != null)
                        await DataStore.CreateProjectAsync(data);
                }
            }

            var items = await DataStore.GetTypesAsync();

            return View("Index", items);

        }
        /// <summary>Звіт в форматі excel </summary>
        public async Task<FileResult> GetExcel()
        {
            var items = await DataStore.GetProjectsPartAsync(0, 100000, "", "", -1);

            using (XLWorkbook workBook = new XLWorkbook())
            {
                var worksheet = workBook.AddWorksheet("Проекти");
                worksheet.Row(1).Cell(1).Value = "Название проекта";
                worksheet.Row(1).Cell(2).Value = "Описание проекта";
                worksheet.Row(1).Cell(3).Value = "Название компании";

                for (int i = 0; i < items.Count(); i++)
                {
                    worksheet.Row(i + 2).Cell(1).Value = items[i].Title;
                    worksheet.Row(i + 2).Cell(2).Value = items[i].Description;
                    worksheet.Row(i + 2).Cell(3).Value = items[i].Organization;
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    workBook.Dispose();
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Список проектов.xlsx");
                }
            }

        }
    }
}