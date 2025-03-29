using AutoMapper;
using data_Access_layer.model;
using WebApplication8.Models;

namespace WebApplication8.mappingProfile
{
    public class employeeProfile :Profile
    {
       public employeeProfile()
        {
            CreateMap<employeeViewModel, Employee>().ReverseMap();
        }
    }
}
