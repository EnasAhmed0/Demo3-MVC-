using AutoMapper;
using Demo3MVC.DAL.Models;
using Demo3MVC.PL.ViewModels;

namespace Demo3MVC.PL.MappingProfiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel,Employee>().ReverseMap();
            //CreateMap<EmployeeViewModel,Employee>().ForMember(d=>d.Name,O=>O.MapFrom(S=>S.EmpName));
        }
    }
}
