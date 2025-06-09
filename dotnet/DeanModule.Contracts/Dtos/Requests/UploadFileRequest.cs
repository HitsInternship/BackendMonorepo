using Microsoft.AspNetCore.Http;

namespace DeanModule.Contracts.Dtos.Requests;

public class UploadFileRequest
{
    public required IFormFile File { get; set; }
}