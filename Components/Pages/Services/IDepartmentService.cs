using EmployeeManagement.Models;

namespace WcomBlazorApp.Components.Pages.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int id);
    }
}
