using Volo.Abp.Application.Dtos;

namespace FileStore.FileManagement.Dto
{
    public class GetFileInputDto: PagedAndSortedResultRequestDto
    {
      
        public string Filter { get; set; }
      
    }
}