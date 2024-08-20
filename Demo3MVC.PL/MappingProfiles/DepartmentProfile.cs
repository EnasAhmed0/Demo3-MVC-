using AutoMapper;
using Demo3MVC.DAL.Models;
using Demo3MVC.PL.ViewModels;

namespace Demo3MVC.PL.MappingProfiles
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
        }
    }
}
