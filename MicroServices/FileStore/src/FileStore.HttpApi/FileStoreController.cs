using FileStore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace FileStore;

public abstract class FileStoreController : AbpControllerBase
{
    protected FileStoreController()
    {
        LocalizationResource = typeof(FileStoreResource);
    }
}
