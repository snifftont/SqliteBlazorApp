using EmployeeManagement.Models;

namespace WcomBlazorApp.Components.Pages.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> CreateEmployee(Employee newEmployee); // C
        Task<Employee> GetEmployee(int id); //R
        Task<Employee> UpdateEmployee(Employee updatedEmployee); // U
        
        Task DeleteEmployee(int id); // D
    }
}
