using Microsoft.EntityFrameworkCore;
using Minio;
using pdfProcessor.Data.DbContexts;
using pdfProcessor.Features.DocumentUpload;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabaseConnection")));

builder.Services.AddSingleton<IMinioClient>(sp =>
    new MinioClient()
        .WithEndpoint(builder.Configuration.GetSection("Minio").GetValue<string>("url"))
        .WithCredentials(builder.Configuration.GetSection("Minio").GetValue<string>("username"),
            builder.Configuration.GetSection("Minio").GetValue<string>("password"))
        .WithSSL(false)
        .Build());

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.UseRouting();

app.RegisterDocumentUploadEndpoint();

app.Run();