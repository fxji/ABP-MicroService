using FileStore.FileManagement.Dto;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FileStore.FileManagement
{
    public interface IFileAppService : IApplicationService
    {
         Task<PagedResultDto<FileInfoDto>> GetAllFilesInfoByInput(GetFileInputDto input);

         Task<byte[]> GetFileContentAsyncByName(string name);

         Task<FileInfoDto> CreateFileInfoAndBlobAsync(FileInputDto input);
    }
}
