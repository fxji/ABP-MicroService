using AutoMapper;
using AAA.A3Management.Dto;
using AAA.Models;

namespace AAA.A3Management
{
    public class A3AutoMapperProfile : Profile
    {
        public A3AutoMapperProfile()
        {
            CreateMap<A3, A3Dto>();
            CreateMap<CreateOrUpdateA3Dto, A3>();
        }
    }
}
