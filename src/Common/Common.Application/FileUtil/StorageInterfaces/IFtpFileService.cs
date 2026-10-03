using Microsoft.AspNetCore.Http;

namespace Common.Application.FileUtil.StorageInterfaces;

public interface IFtpFileService
{
    public Task SaveFile(IFormFile file, string directoryPath);
    public Task<string> SaveFileAndGenerateName(IFormFile file, string directoryPath);
    Task SaveFile(Stream stream, string directoryPath, string fileName);
    Task DeleteFile(string path, string fileName);
    Task DeleteFile(string filePath);
    Task DeleteDirectory(string directoryPath);
}