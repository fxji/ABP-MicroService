using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStore.FileManagement
{
    public class FileOptions
    {
        /// <summary>
        /// 允许的文件最大大小
        /// </summary>
        public long MaxFileSize { get; set; } = 1048576;//1MB

        /// <summary>
        /// 允许的文件类型
        /// </summary>
        public string[] AllowedUploadFormats { get; set; } = { ".jpg", ".jpeg", ".png", "gif", ".txt" };

        public string[] PictureFormatArray { get; set; } = { ".png", ".jpg", ".jpeg", ".gif", ".PNG", ".JPG", ".JPEG", ".GIF" };
    }

}
