﻿using Email.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Email;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(EmailEntityFrameworkCoreTestModule)
    )]
public class EmailDomainTestModule : AbpModule
{

}
