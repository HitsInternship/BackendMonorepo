using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentModule.Infrastructure.FileStorage
{
    public class FileStorageSettings
    {
        public string Endpoint { get; set; } = Environment.GetEnvironmentVariable("MINIO_ENDPOINT");
        public string AccessKey { get; set; } = Environment.GetEnvironmentVariable("MINIO_ACCESS_KEY");
        public string SecretKey { get; set; } = Environment.GetEnvironmentVariable("MINIO_SECRET_KEY");
    }
}
