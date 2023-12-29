using FileStorage.FileManagement.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.FileManagement
{
    public static class IFileAppServiceExtension
    {
        public static Task<FileInfoDto> UploadDoc(this IFileAppService app, [Required] string name, [Required] IFormFile file)
        {
            return app.Upload(name, file);
        }
    }
}
