using AutoMapper;
using EmployeeManagement.Models;
namespace WcomBlazorApp.Models
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Employee, EditEmployeeModel>().ForMember(d => d.ConfirmEmail, s => s.MapFrom(src => src.Email));
            CreateMap<EditEmployeeModel,Employee >();
        }
        
    }
}
