using AutoMapper;
using Demo3MVC.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace Demo3MVC.PL.MappingProfiles
{
    public class RoleProfile :Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole,RoleViewModel>()
                .ForMember(d=>d.RoleName,O=>O.MapFrom(S=>S.Name))
                .ReverseMap();

        }
    }
}
