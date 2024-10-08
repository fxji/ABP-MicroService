using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DataExport.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
