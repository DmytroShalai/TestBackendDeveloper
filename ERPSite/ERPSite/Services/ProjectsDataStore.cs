using ERPSite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ERPSite.Services
{
    class ProjectsDataStore : IProjectDataStore
    {
        HttpClient client;


        public ProjectsDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(RouteConfig.urlERPApi);
        }


        public async Task<List<ProjectTypeViewModel>> GetTypesAsync()
        {
            var content = await client.GetStringAsync($"api/Project/ProjectTypes").ConfigureAwait(false);
            List<ProjectTypeViewModel> items = await Task.Run(() => JsonConvert.DeserializeObject<List<ProjectTypeViewModel>>(content)).ConfigureAwait(false);
            return items;
        }
        public async Task<List<SkillViewModel>> GetSkillsAsync()
        {
            var content = await client.GetStringAsync($"api/Project/ProjectSkills").ConfigureAwait(false);
            List<SkillViewModel> items = await Task.Run(() => JsonConvert.DeserializeObject<List<SkillViewModel>>(content)).ConfigureAwait(false);
            return items;
        }
        public async Task<List<ProjectsPartViewModel>> GetProjectsPartAsync(int startPosition, int count, string title, string organization, int typeId)
        {
            string str = HttpUtility.UrlEncode("!@^$##");
            if (title.Count() == 0)
            {
                title = str;
            }
            if (organization.Count() == 0)
            {
                organization = str;
            }
            var content = await client.GetStringAsync($"api/ProjectsShortInfo/{startPosition}/{count}/{title}/{organization}/{typeId}").ConfigureAwait(false);
            List<ProjectsPartViewModel> items = await Task.Run(() => JsonConvert.DeserializeObject<List<ProjectsPartViewModel>>(content)).ConfigureAwait(false);
            return items;
        }
        public async Task<bool> RemoveProjectAsync(int projectId)
        {
            var content = await client.DeleteAsync($"api/Project/Remove/{projectId}").ConfigureAwait(false);
            return true;
        }
        public async Task<ProjectDetalInfoViewModel> GetProjectDetalInfoAsync(int projectId)
        {           
            var content = await client.GetStringAsync($"api/ProjectDetalInfo/{projectId}").ConfigureAwait(false);
            ProjectDetalInfoViewModel project = await Task.Run(() => JsonConvert.DeserializeObject<ProjectDetalInfoViewModel>(content)).ConfigureAwait(false);
            return project;
        }
        public async Task<bool> CreateProjectAsync(string data)
        {
            var b = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var content = await client.PostAsync($"api/ProjectCreate/", new StringContent(data, System.Text.Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return true;
        }
        public async Task<FileViewModel> GetProjecFileAsync(int fileId)
        {
            var content = await client.GetStringAsync($"api/ProjectFile/{fileId}").ConfigureAwait(false);
            FileViewModel file = await Task.Run(() => JsonConvert.DeserializeObject<FileViewModel>(content)).ConfigureAwait(false);
            return file;
        }
        public async Task<bool> UpdateProjectAsync(string data)
        {
            var b = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var content = await client.PutAsync($"api/ProjectUpdate/", new StringContent(data, System.Text.Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return true;
        }
        
        public void Dispose()
        {
            ((IDisposable)client).Dispose();
        }
    }
}