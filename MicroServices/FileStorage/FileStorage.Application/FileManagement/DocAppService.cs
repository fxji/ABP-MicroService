using AutoMapper.Internal.Mappers;
using FileStorage.Enums;
using FileStorage.FileManagement.Dto;
using FileStorage.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using FileInfo = FileStorage.Models.FileInfo;

namespace FileStorage.FileManagement
{
    public class DocAppService : FileAppService
    {
        private readonly IRepository<FileInfo, Guid> _repository;
        private readonly FileManager _fileManager;
        string[] pictureFormatArray = { ".docx", ".xlsx", ".pdf", ".pptx", ".txt", ".mdj", ".csv", ".xmind" };


        public DocAppService(IRepository<FileInfo, Guid> repository, FileManager fileManager) : base(repository, fileManager)
        {
            _repository = repository;
            _fileManager = fileManager;
        }

        public async Task<FileInfoDto> UploadDoc([Required] string name, [Required] IFormFile file)
        {
            if (file == null || file.Length == 0) throw new BusinessException("无法上传空文件");

            //限制100M
            if (file.Length > 104857600)
            {
                throw new BusinessException("上传文件过大");
            }
            //文件格式
            var fileExtension = Path.GetExtension(file.FileName);
            if (!pictureFormatArray.Contains(fileExtension))
            {
                throw new BusinessException("上传文件格式错误");
            }

            var size = "";
            if (file.Length < 1024)
                size = file.Length.ToString() + "B";
            else if (file.Length >= 1024 && file.Length < 1048576)
                size = ((float)file.Length / 1024).ToString("F2") + "KB";
            else if (file.Length >= 1048576 && file.Length < 104857600)
                size = ((float)file.Length / 1024 / 1024).ToString("F2") + "MB";
            else size = file.Length.ToString() + "B";

            string uploadsFolder = Path.Combine(Environment.CurrentDirectory, "wwwroot", "files");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            //TODO：文件md5哈希校验
            var result = await _fileManager.Create(name, uniqueFileName, fileExtension, "", size, filePath, "/files/" + uniqueFileName, FileType.File);
            return ObjectMapper.Map<FileInfo, FileInfoDto>(result);
        }

    }
}
