using DocumentModule.Domain.Enums;
using Microsoft.VisualBasic.FileIO;
using Minio;
using Minio.DataModel.Args;

namespace DocumentModule.Infrastructure.FileStorage
{
    public class FileStorageContext : IDisposable
    {
        public readonly IMinioClient Client;

        public FileStorageContext(FileStorageSettings settings)
        {
            Client = new MinioClient()
                .WithEndpoint(settings.Endpoint, 9000)
                .WithCredentials(settings.AccessKey, settings.SecretKey)
                .Build();

            InitializeBucketAsync().Wait();
        }

        private async Task InitializeBucketAsync()
        {
            foreach (DocumentType documentType in Enum.GetValues(typeof(DocumentType)))
            {
                if (!await Client.BucketExistsAsync(new BucketExistsArgs().WithBucket(documentType.ToString().ToLower())))
                {
                    await Client.MakeBucketAsync(new MakeBucketArgs().WithBucket(documentType.ToString().ToLower()));
                }
            }
        }

        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}
