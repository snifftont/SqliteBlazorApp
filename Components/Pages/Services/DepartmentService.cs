using EmployeeManagement.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WcomBlazorApp.Components.Pages.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient httpClient;

        public DepartmentService(HttpClient httpClient) 
        {
            this.httpClient = httpClient;
        }

        public async Task<Department> GetDepartment(int id)
        {
            return  await httpClient.GetFromJsonAsync<Department>(new Uri($"https://localhost:7270/api/departments/{id}"));
        }
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await httpClient.GetFromJsonAsync<Department[]>(new Uri("https://localhost:7270/api/departments"));
        }
    }
}
