using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.BlobStoring;
using FileStore.FileManagement;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Clients;
using System;

namespace FileStore;

[DependsOn(
    typeof(FileStoreDomainModule),
    typeof(FileStoreApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]

[DependsOn(typeof(AbpBlobStoringModule))]
[DependsOn(typeof(AbpBlobStoringFileSystemModule))]
public class FileStoreApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<FileStoreApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<FileStoreApplicationModule>(validate: false);
        });

        Configure<AbpBlobStoringOptions>(options =>
        {

            options.Containers.Configure("AAAFileContainer", container =>
            {
                //TODO...
                //container.IsMultiTenant = false;
                container.UseFileSystem(fileSystem =>
                {
                    fileSystem.BasePath = "D://filesystem";
                });
            });
        });

        Configure<FileOptions>(option => { 
        
        });
    }
}
