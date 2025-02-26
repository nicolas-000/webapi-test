namespace WebApiTest.Services
{
    public interface IFileService
    {
        Task<string> CreateFileAsync(string filename, string content);
        Task<string> EditFileAsync(string filename, string content);
        Task<string> GetFileContentAsync(string filename);
        Task<bool> RenameFileAsync(string oldFilename, string newFilename);
    }
}
