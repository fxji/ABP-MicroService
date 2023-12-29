using AutoMapper;
using AAA.Models;
using AAA.A3AttachmentManagement.Dto;

namespace AAA.A3AttachmentManagement
{
    public class A3AttachmentAutoMapperProfile : Profile
    {
        public A3AttachmentAutoMapperProfile()
        {
            CreateMap<A3Attachment, A3AttachmentDto>();
            CreateMap<CreateOrUpdateA3AttachmentDto, A3Attachment>();
        }
    }
}
