namespace Common.Application.FileUtil.StorageServices;

using Common.Application.FileUtil.StorageInterfaces;
using Common.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public class LocalFileService : ILocalFileService
{
    public void DeleteDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
            Directory.Delete(directoryPath, true);
    }

    public void DeleteFile(string path, string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), path,
              fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public async Task SaveFile(IFormFile file, string directoryPath)
    {
        if (file == null)
            throw new NullOrEmptyDomainDataException("file is null");

        var fileName = file.FileName;

        var folderName = Path.Combine(Directory.GetCurrentDirectory(), directoryPath.Replace("/", "\\"));
        if (!Directory.Exists(folderName))
            Directory.CreateDirectory(folderName);

        var path = Path.Combine(folderName, fileName);
        using var stream = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(stream);
    }

    public async Task SaveFile(IFormFile file, string directoryPath, string fileName)
    {
        if (file == null)
            throw new InvalidDataException("file is Null");

        var folderName = Path.Combine(Directory.GetCurrentDirectory(), directoryPath.Replace("/", "\\"));
        if (!Directory.Exists(folderName))
            Directory.CreateDirectory(folderName);

        var path = Path.Combine(folderName, fileName);
        await using var stream = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(stream);
    }

    public async Task<string> SaveFileAndGenerateName(IFormFile file, string directoryPath)
    {
        if (file == null)
            throw new NullOrEmptyDomainDataException("file is null");

        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{DateTime.Now.TimeOfDay.TotalDays}".Replace(".", "");
        fileName += extension;

        var baseDirectory = Directory.GetCurrentDirectory();
        var fullPath = Path.Combine(baseDirectory, directoryPath, fileName);


        if (!Directory.Exists(Path.Combine(baseDirectory, directoryPath)))
            Directory.CreateDirectory(Path.Combine(baseDirectory, directoryPath));


        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return fileName;
    }
}