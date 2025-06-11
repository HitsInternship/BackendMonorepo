using DocumentModule.Contracts.Repositories;
using DocumentModule.Domain.Enums;
using DocumentModule.Infrastructure.FileStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Args;

namespace DocumentModule.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly FileStorageContext _context;

        public FileRepository(FileStorageContext context)
        {
            _context = context;
        }

        public async Task<string> AddFileAsync(Guid fileId, DocumentType documentType, IFormFile file)
        {
            var bucketName = documentType.ToString().ToLower();
            var objectName = fileId.ToString();

            var found = await _context.Client.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!found)
            {
                await _context.Client.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            }

            await using var stream = file.OpenReadStream();

            var encodedName = Uri.EscapeDataString(objectName);

            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType)
                .WithHeaders(new Dictionary<string, string> { { "Name", encodedName } });

            await _context.Client.PutObjectAsync(args);
            return file.ContentType;
        }


        public async Task<string?> GetFileNameAsync(Guid fileId, DocumentType documentType)
        {
            var metadata = await _context.Client.StatObjectAsync(
                new StatObjectArgs()
                    .WithBucket(documentType.ToString().ToLower())
                    .WithObject(fileId.ToString())
            );

            if (metadata == null) return null;
            else return Uri.UnescapeDataString(metadata.MetaData["Name"]);
        }

        public async Task<FileContentResult> GetFileAsync(Guid fileId, DocumentType documentType)
        {
            var bucket = documentType.ToString().ToLower();
            var objectName = fileId.ToString();

            using var memoryStream = new MemoryStream();

            await _context.Client.GetObjectAsync(
                new GetObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(objectName)
                    .WithCallbackStream(stream => stream.CopyTo(memoryStream))
            );

            var stat = await _context.Client.StatObjectAsync(
                new StatObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(objectName)
            );

            stat.MetaData.TryGetValue("Name", out string? encodedName);
            var fileName = encodedName != null ? Uri.UnescapeDataString(encodedName) : objectName;
            var contentType = stat.ContentType ?? "application/octet-stream";

            return new FileContentResult(memoryStream.ToArray(), contentType)
            {
                FileDownloadName = fileName
            };
        }



        public async Task DeleteFileAsync(Guid fileId, DocumentType documentType)
        {
            await _context.Client.RemoveObjectAsync(
                new RemoveObjectArgs()
                    .WithBucket(documentType.ToString().ToLower())
                    .WithObject(fileId.ToString()));
        }
    }
}