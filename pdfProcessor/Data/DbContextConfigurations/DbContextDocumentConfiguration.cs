using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pdfProcessor.Features.DocumentUpload;

namespace pdfProcessor.Data.DbContextConfigurations;

public class DbContextDocumentConfiguration : IEntityTypeConfiguration<DocumentDto>
{
    public void Configure(EntityTypeBuilder<DocumentDto> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(x => x.DocumentId);
        builder.Property(x => x.DocumentId).HasColumnName("DocumentId");
        builder.HasIndex(x => x.DocumentId).IsUnique();

        builder.Property(x => x.FileName).HasColumnName("FileName");
        builder.Property(x => x.MimeType).HasColumnName("MimeType");
        builder.Property(x => x.FileSize).HasColumnName("FileSize");

        builder.Property(x => x.UploadedAt).HasColumnName("UploadedAt").HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}