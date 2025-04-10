using AutoMapper;
using FileStore.FileManagement.Dto;
using FileStore.Models;

namespace FileStore;

public class FileStoreApplicationAutoMapperProfile : Profile
{
    public FileStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<FileDetail, FileInfoDto>();

    }
}
