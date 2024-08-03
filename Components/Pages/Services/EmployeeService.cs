using EmployeeManagement.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace WcomBlazorApp.Components.Pages.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient) 
        {
            this.httpClient = httpClient;
        }

        public async Task<Employee> CreateEmployee(Employee newEmployee)
        {
            var response = await httpClient.PostAsJsonAsync<Employee>(new Uri("https://localhost:7270/api/employees"), newEmployee);
            return await response.Content.ReadFromJsonAsync<Employee>();

        }

        public async Task DeleteEmployee(int id)
        {
            await httpClient.DeleteAsync(new Uri($"https://localhost:7270/api/employees/{id}"));
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await httpClient.GetFromJsonAsync<Employee>(new Uri($"https://localhost:7270/api/employees/{id}"));
            
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await httpClient.GetFromJsonAsync<Employee[]>(new Uri("https://localhost:7270/api/employees"));
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            var response = await httpClient.PutAsJsonAsync<Employee>(new Uri("https://localhost:7270/api/employees"), updatedEmployee);
            var rst = response.Content.ReadAsStringAsync();
            return await response.Content.ReadFromJsonAsync<Employee>();

        }
    }
}
