﻿using PeBusiness.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PeBusiness;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(PeBusinessEntityFrameworkCoreTestModule)
    )]
public class PeBusinessDomainTestModule : AbpModule
{

}
