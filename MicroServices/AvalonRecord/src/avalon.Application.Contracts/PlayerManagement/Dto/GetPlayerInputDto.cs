using Volo.Abp.Application.Dtos;

public class GetPlayerInputDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}