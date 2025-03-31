using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApplication8.Models;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<IdentityRole, roleViewModel>().ReverseMap();
        
    }
}