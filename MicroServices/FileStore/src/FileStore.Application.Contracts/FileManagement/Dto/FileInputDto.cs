namespace FileStore.FileManagement.Dto
{
    public class FileInputDto
    {
        public byte[] Content { get; set; }
        public string Name { get; set; }
        public FileType Type { get; set; }
    }
}