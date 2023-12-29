using AutoMapper;
using AAA.A3MemberManagement.Dto;
using AAA.Models;

namespace AAA.A3MemberManagement
{
    public class A3MemberAutoMapperProfile : Profile
    {
        public A3MemberAutoMapperProfile()
        {
            CreateMap<A3Member, A3MemberDto>();
            CreateMap<CreateOrUpdateA3MemberDto, A3Member>();
        }
    }
}
