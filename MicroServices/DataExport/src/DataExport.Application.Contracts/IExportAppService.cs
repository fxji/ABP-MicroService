using DataExport.ExportManagement;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DataExport
{
    public interface IExportAppService : IApplicationService
    {
        public byte[] Export(InputExDto input);
    }
}
