using DocumentModule.Domain.Enums;
using Microsoft.VisualBasic.FileIO;
using Minio;
using Minio.DataModel.Args;

namespace DocumentModule.Infrastructure.FileStorage
{
    public class FileStorageContext : IDisposable
    {
        public readonly IMinioClient _client;

        public FileStorageContext(FileStorageSettings settings)
        {
            _client = new MinioClient()
                .WithEndpoint(settings.Endpoint, 9000)
                .WithCredentials(settings.AccessKey, settings.SecretKey)
                .Build();

            InitializeBucketAsync().Wait();
        }

        private async Task InitializeBucketAsync()
        {
            foreach (DocumentType documentType in Enum.GetValues(typeof(DocumentType)))
            {
                if (!await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(documentType.ToString().ToLower())))
                {
                    await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(documentType.ToString().ToLower()));
                }
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
