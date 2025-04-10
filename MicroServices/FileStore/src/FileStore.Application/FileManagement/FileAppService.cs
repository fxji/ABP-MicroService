using FileStore.FileManagement.Dto;
using FileStore.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;

namespace FileStore.FileManagement
{

    public class FileAppService : ApplicationService, IFileAppService
    {
        private readonly IBlobContainer<AAAFileContainer> _blobContainer;
        private readonly FileManager _fileManager;
        private readonly FileOptions _fileOptions;
        //private string[] pictureFormatArray = { ".png", ".jpg", ".jpeg", ".gif", ".PNG", ".JPG", ".JPEG", ".GIF" };
        private readonly AbpBlobStoringOptions _abpBlobStoringOptions;

        public FileAppService(
            IBlobContainer<AAAFileContainer> blobContainer,
            FileManager fileManager,
            IOptions<AbpBlobStoringOptions> options,
            IOptions<FileOptions> fileOptions
            )
        {

            _abpBlobStoringOptions = options.Value;
            _blobContainer = blobContainer;
            _fileManager = fileManager;
            _fileOptions = fileOptions.Value;
        }

        public async Task<FileInfoDto> CreateFileInfoAndBlobAsync(FileInputDto input)
        {
            var temp = _abpBlobStoringOptions.Containers.GetConfiguration<AAAFileContainer>().GetFileSystemConfiguration();
            var extensionName = Path.GetExtension(input.Name);
            var type = _fileOptions.PictureFormatArray.Any(item => item == extensionName) ? FileType.Image : FileType.File;

            await _blobContainer.SaveAsync(input.Name, input.Content);
            var result = await _fileManager.Create(
                input.Name,
                input.Name,
                Path.GetExtension(input.Name),
                "",
                input.Content.Length.ToString(),
                temp.BasePath + "/host/" + nameof(AAAFileContainer) + input.Name,
                "/api/storage/file/" + input.Name,//route path
                type
                );

            return ObjectMapper.Map<FileDetail, FileInfoDto>(result);
        }

        public async Task<PagedResultDto<FileInfoDto>> GetAllFilesInfoByInput(GetFileInputDto input)
        {
            var query = (await _fileManager.GetAll())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Name.Contains(input.Filter));

            var totalCount = query.Count();
            var items = query.OrderBy(input.Sorting ?? "Name")
                                   .Skip(input.SkipCount)
                                   .Take(input.MaxResultCount)
                                   .ToList();

            var dtos = ObjectMapper.Map<List<FileDetail>, List<FileInfoDto>>(items);
            return new PagedResultDto<FileInfoDto>(totalCount, dtos);
        }

        public async Task<byte[]> GetFileContentAsyncByName(string name)
        {
            var content = await _blobContainer.GetAllBytesAsync(name);
            return content;
        }
    }


}
