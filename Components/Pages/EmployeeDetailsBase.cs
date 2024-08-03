using EmployeeManagement.Models;
using WcomBlazorApp.Components.Pages.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Connections.Features;

namespace WcomBlazorApp.Components.Pages
{
    public class EmployeeDetailsBase:ComponentBase
    {
        public Employee employee { get; set; }= new Employee();
        public Department dept { get; set; } = new Department();
        protected string coordinates { get; set; }
        public string ButtonText { get; set; } = "Hide Footer";
        public string cssClass { get; set; } = null;

        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Id=Id?? "1";
            employee = await EmployeeService.GetEmployee(int.Parse(Id));
            dept =await DepartmentService.GetDepartment(employee.DepartmentId);
        }

        protected void Mouse_Move(MouseEventArgs e)
        {
            coordinates = $"X ={e.ClientX} Y={e.ClientY}";
        }

        protected void Button_Click()
        {
            if(ButtonText=="Hide Footer")
            {
                ButtonText = "Show Footer";
                cssClass = "displayNone";
            }
            else
            {
                cssClass = null;
                ButtonText = "Hide Footer";
            }
        }

    }
}
