using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using pdfProcessor.Data.DbContexts;

namespace pdfProcessor.Features.DocumentUpload;

public static class DocumentUploadEndpoint
{
    public static void RegisterDocumentUploadEndpoint(this WebApplication app)
    {
        app.MapPost("/api/documentUpload", UploadDocument);
    }

    private static async Task<IResult> UploadDocument(
        [FromBody] DocumentUploadRequest documentUploadRequest,
        IMinioClient minioClient,
        IConfiguration configuration,
        AppDbContext dbContext,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        var logger = loggerFactory.CreateLogger("DocumentUploadEndpoint");
        try
        {
            var bucketExistsArgs =
                new BucketExistsArgs().WithBucket(configuration.GetValue<string>("Minio:BucketName"));
            var isBucketExists = await minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);

            if (!isBucketExists)
            {
                var createBucketArgs =
                    new MakeBucketArgs().WithBucket(configuration.GetValue<string>("Minio:BucketName"));
                await minioClient.MakeBucketAsync(createBucketArgs, cancellationToken);
            }

            var presignedObject = new PresignedPutObjectArgs()
                .WithBucket(configuration.GetValue<string>("Minio:BucketName"))
                .WithObject(documentUploadRequest.FileName).WithExpiry(60 * 6);
            var response = await minioClient.PresignedPutObjectAsync(presignedObject);
            return TypedResults.Ok(response);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return TypedResults.InternalServerError(e.Message);
        }
    }
}