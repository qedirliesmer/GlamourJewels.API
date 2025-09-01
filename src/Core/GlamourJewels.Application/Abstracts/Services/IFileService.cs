using GlamourJewels.Application.DTOs.FileUploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface IFileService
{
    Task<string> UploadAsync(FileUploadDto file, string folder);
    Task<bool> DeleteAsync(string filePath);
}
