﻿using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace FileStore.MongoDB;

[ConnectionStringName(FileStoreDbProperties.ConnectionStringName)]
public interface IFileStoreMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
