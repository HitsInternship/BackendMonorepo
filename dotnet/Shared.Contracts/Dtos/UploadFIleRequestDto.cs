using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shared.Contracts.Dtos;

public record UploadFileRequestDto
{
    [Required]
    public required IFormFile File { get; set; }
}