using FileStore.FileManagement.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Http;

namespace FileStore.FileManagement
{
    [Area(FileStoreRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = FileStoreRemoteServiceConsts.RemoteServiceName)]
    [Route("api/storage/file")]
    public class FileController : FileStoreController, IFileAppService
    {
        private readonly IFileAppService _fileAppService;
        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<FileInfoDto> Upload([Required] string name, IFormFile file)
        {
            var input = new FileInputDto()
            {
                Content = await file.GetAllBytesAsync(),
                Name = file.FileName
            };
            return await CreateFileInfoAndBlobAsync(input);// await _fileAppService.CreateFileInfoAndBlobAsync(input);
            //throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{name}")]
        //[ActionName("GetFileByNameAsync")]
        public async Task<IActionResult> GetFileByNameAsync(string name)
        {
            var result = await GetFileContentAsyncByName(name);

            return File(result, MimeTypes.GetByExtension(Path.GetExtension(name)));
        }

        [HttpGet]
        public async Task<PagedResultDto<FileInfoDto>> GetAllFilesInfoByInput(GetFileInputDto input)
        {
            return await _fileAppService.GetAllFilesInfoByInput(input);
        }

        [HttpGet]
        [Route("content")]
        public Task<byte[]> GetFileContentAsyncByName(string name)
        {
            return  _fileAppService.GetFileContentAsyncByName(name);
        }

        [HttpPost]
        [Route("create")]
        public Task<FileInfoDto> CreateFileInfoAndBlobAsync(FileInputDto input)
        {
            return _fileAppService.CreateFileInfoAndBlobAsync(input);
        }
    }
}
