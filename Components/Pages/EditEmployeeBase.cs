using AutoMapper;
using EmployeeManagement.Models;
using WcomBlazorApp.Components.Pages.Services;
using WcomBlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace WcomBlazorApp.Components.Pages
{
    public class EditEmployeeBase:ComponentBase
    {
        [CascadingParameter] 
        public HttpContext? HttpContext { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        
        public string PageHeaderText { get; set; }
        public Employee employee { get; set; } = new Employee();
        [SupplyParameterFromForm]
        public EditEmployeeModel editEmployeeModel { get; set; } =new EditEmployeeModel();

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public List<Department> departments { get; set; } = new List<Department>();

        //public string departmentId { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IMapper mapper { get; set; }
        
        [Inject]
        public NavigationManager navigationManager { get; set; }
        protected async override Task OnInitializedAsync()
        {
            int.TryParse(Id, out int employeeId);
            if(employeeId != 0)
            {
                PageHeaderText = "Edit Employee";
                employee = await EmployeeService.GetEmployee(int.Parse(Id));
            }
            else
            {
                PageHeaderText = "Create Employee";
                employee = new Employee
                {
                    DepartmentId = 1,
                    DateOfBirth = DateTime.Now,
                    PhotoPath = "images/nophoto.jpg"
                };
            }
            
            departments=(await DepartmentService.GetDepartments()).ToList();
                mapper.Map(employee, editEmployeeModel);
            
        }
        protected async Task HandleValidSubmit()
        {
            if (Id!=null && int.Parse(Id) != 0)
            {
                editEmployeeModel.EmployeeId = int.Parse(Id);
                
                //editEmployeeModel.department = new Department { DepartmentId = 1, DepartmentName = "IT" };
            }
            editEmployeeModel.PhotoPath = "images/jon.png";
            mapper.Map(editEmployeeModel, employee);
            
            Employee result = null;
            if(employee.EmployeeId!=0)
            {

                result = await EmployeeService.UpdateEmployee(employee);
            }
            else
            {
                result = await EmployeeService.CreateEmployee(employee);
            }
          
            if(result!=null)
            {
                navigationManager.NavigateTo("/");
            }
        }
        //protected Snifftont.Components.ConfirmBase DeleteConfirmation { get; set; }
        //protected void Delete_Click()
        //{
        //    DeleteConfirmation.Show();
        //}
        //protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        //{
        //    if (deleteConfirmed)
        //    {
        //        await EmployeeService.DeleteEmployee(employee.EmployeeId);
        //        navigationManager.NavigateTo("/");
        //    }
        //}
    }
}
