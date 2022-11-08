using AutoMapper;
using EBS_API.Models;

namespace EBS_API.MapperConfig
{
    public class MapperConfig: Profile
    {

        public MapperConfig()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
        }
    }
}



















