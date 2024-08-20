using AutoMapper;
using Demo3MVC.DAL.Models;
using Demo3MVC.PL.ViewModels;

namespace Demo3MVC.PL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,UserViewModel>().ReverseMap();
        }
    }
}
