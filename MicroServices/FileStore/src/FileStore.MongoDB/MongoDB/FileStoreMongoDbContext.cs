﻿using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace FileStore.MongoDB;

[ConnectionStringName(FileStoreDbProperties.ConnectionStringName)]
public class FileStoreMongoDbContext : AbpMongoDbContext, IFileStoreMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureFileStore();
    }
}
