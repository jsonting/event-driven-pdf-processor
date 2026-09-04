using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using pdfProcessor.Data.DbContexts;
using pdfProcessor.Features.DocumentUpload;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabaseConnection")));

builder.Services.AddSingleton<IAmazonS3>(_ =>
{
    var config = new AmazonS3Config
    {
        ServiceURL = builder.Configuration.GetSection("Minio").GetValue<string>("url"),
        ForcePathStyle = true
    };

    return new AmazonS3Client(builder.Configuration.GetSection("Minio").GetValue<string>("username"),
        builder.Configuration.GetSection("Minio").GetValue<string>("password"), config);
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.RegisterDocumentUploadEndpoint();

app.Run();