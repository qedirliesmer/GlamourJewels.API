using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.FileUploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Infrastructure.Services;

public class FileService:IFileService
{

    private readonly string _rootPath;

    // IWebHostEnvironment əvəzinə birbaşa rootPath qəbul et
    public FileService(string rootPath)
    {
        _rootPath = rootPath;
    }

    public async Task<string> UploadAsync(FileUploadDto file, string folder)
    {
        // Faylın yüklənəcəyi qovluğun path-i
        var uploadPath = Path.Combine(_rootPath, folder);

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        // Yeni fayl adı → unikal olsun deyə GUID istifadə edirik
        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadPath, uniqueFileName);

        await File.WriteAllBytesAsync(filePath, file.Content);

        return $"/{folder}/{uniqueFileName}";
    }

    public async Task<bool> DeleteAsync(string filePath)
    {
        var fullPath = Path.Combine(_rootPath, filePath.TrimStart('/'));

        if (File.Exists(fullPath))
        {
            await Task.Run(() => File.Delete(fullPath));
            return true;
        }

        return false;
    }
}
