namespace WebApiTest.Services
{
    public class FileService : IFileService
    {
        private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

        public FileService()
        {
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
        }

        public async Task<string> CreateFileAsync(string filename, string? content)
        {
            var filePath = Path.Combine(_basePath, filename);
            await File.WriteAllTextAsync(filePath, content ?? string.Empty);
            return filePath;
        }

        public async Task<string> EditFileAsync(string filename, string content)
        {
            var filePath = Path.Combine(_basePath, filename);
            await File.WriteAllTextAsync(filePath, content);
            return filePath;
        }

        public async Task<string> GetFileContentAsync(string filename)
        {
            var filePath = Path.Combine(_basePath, filename);
            return await File.ReadAllTextAsync(filePath);
        }

        public async Task<bool> RenameFileAsync(string oldFilename, string newFilename)
        {
            var oldFilePath = Path.Combine(_basePath, oldFilename);
            var newFilePath = Path.Combine(_basePath, newFilename);

            if (!File.Exists(oldFilePath))
            {
                return false;
            }

            if (File.Exists(newFilePath))
            {
                return false;
            }

            File.Move(oldFilePath, newFilePath);
            return true;
        }

    }
}
