using Microsoft.EntityFrameworkCore;
using pdfProcessor.Features.DocumentProcess;
using pdfProcessor.Features.DocumentUpload;

namespace pdfProcessor.Data.DbContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<DocumentDto> DocumentDto => Set<DocumentDto>();
    public DbSet<DocumentProcessDto> DocumentProcessesDto => Set<DocumentProcessDto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}