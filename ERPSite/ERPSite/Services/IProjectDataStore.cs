using ERPSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSite.Services
{
    public interface IProjectDataStore : IDisposable
    {
        Task<List<ProjectTypeViewModel>> GetTypesAsync();
        Task<List<SkillViewModel>> GetSkillsAsync();
        Task<List<ProjectsPartViewModel>> GetProjectsPartAsync(int startPosition, int count, string title, string organization, int typeId);
        Task<bool> RemoveProjectAsync(int projectId);
        Task<ProjectDetalInfoViewModel> GetProjectDetalInfoAsync(int projectId);
        Task<bool> CreateProjectAsync(string data);
        Task<FileViewModel> GetProjecFileAsync(int fileId);
        Task<bool> UpdateProjectAsync(string data);
    }
}
