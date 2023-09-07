using PeBusiness.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PeBusiness;

public abstract class PeBusinessController : AbpControllerBase
{
    protected PeBusinessController()
    {
        LocalizationResource = typeof(PeBusinessResource);
    }
}
