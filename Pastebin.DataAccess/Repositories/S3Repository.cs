using Amazon.S3;
using Amazon.S3.Model;
using Pastebin.Core.Abstractions;
using System.Net;

namespace Pastebin.DataAccess.Repositories
{
    public class S3Repository : IS3Repository
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Repository (IAmazonS3 s3Client, string bucketName)
        {
            _s3Client = s3Client;
            _bucketName = bucketName;
        }
        public async Task<string> SaveTextAsync(string text, string fileName)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                ContentBody = text,
            };

            var response = await _s3Client.PutObjectAsync(putRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK) 
            {
                return $"https://{_bucketName}.s3.storage.selcloud.ru/{fileName}";
            }
            throw new Exception();
        }

        public async Task<string> GetTextAsync(string fileName)
        {
            var key = fileName.Split('/').Last();

            var getRequest = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
            };

            using (var response = await _s3Client.GetObjectAsync(getRequest))
            using (var reader = new StreamReader(response.ResponseStream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task DeleteTextAsync(string hash)
        {
            var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = hash,
                };

                var response = await _s3Client.DeleteObjectAsync(deleteRequest);
        }
    }
}
