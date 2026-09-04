using Amazon.S3;
using Microsoft.AspNetCore.Http.HttpResults;
using pdfProcessor.Data.DbContexts;

namespace pdfProcessor.Features.DocumentUpload;

public static class DocumentUploadEndpoint
{
    public static void RegisterDocumentUploadEndpoint(this WebApplication app)
    {
        app.MapGet("/api/documentUpload", UploadDocument);
    }

    private static async Task<Results<Ok<DocumentDto>, NotFound>> UploadDocument(AmazonS3Client client,
        AppDbContext dbContext)
    {
        throw new NotImplementedException();
    }
}