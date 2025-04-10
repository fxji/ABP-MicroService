using FileStore.Localization;
using Volo.Abp.Application.Services;

namespace FileStore;

public abstract class FileStoreAppService : ApplicationService
{
    protected FileStoreAppService()
    {
        LocalizationResource = typeof(FileStoreResource);
        ObjectMapperContext = typeof(FileStoreApplicationModule);
    }
}
