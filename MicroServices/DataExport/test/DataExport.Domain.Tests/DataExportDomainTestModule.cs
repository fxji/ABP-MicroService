﻿using DataExport.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DataExport;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(DataExportEntityFrameworkCoreTestModule)
    )]
public class DataExportDomainTestModule : AbpModule
{

}
