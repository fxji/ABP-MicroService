using FileStore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace FileStore
{
    public class FileManager : DomainService
    {
        private readonly IRepository<FileDetail, Guid> _repository;

        public FileManager(IRepository<FileDetail, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<FileDetail> Create(string name, string realName, string suffix, string md5, string size, string path, string url, FileType type)
        {
            return await _repository.InsertAsync(new FileDetail(GuidGenerator.Create(),
                                                                                name,
                                                                            realName,
                                                                              suffix,
                                                                                 md5,
                                                                                size,
                                                                                path,
                                                                                 url,
                                                                                type));
        }

        public async Task<IQueryable<FileDetail>> GetAll()
        {
            return await _repository.GetQueryableAsync();
        }
    }
}
