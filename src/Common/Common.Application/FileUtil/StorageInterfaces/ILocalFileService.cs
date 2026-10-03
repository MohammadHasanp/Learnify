namespace Common.Application.FileUtil.StorageInterfaces;

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public interface ILocalFileService
{
    public Task SaveFile(IFormFile file, string directoryPath);
    public Task SaveFile(IFormFile file, string directoryPath, string fileName);
    public Task<string> SaveFileAndGenerateName(IFormFile file, string directoryPath);
    public void DeleteFile(string path, string fileName);
    public void DeleteFile(string filePath);
    public void DeleteDirectory(string directoryPath);
}