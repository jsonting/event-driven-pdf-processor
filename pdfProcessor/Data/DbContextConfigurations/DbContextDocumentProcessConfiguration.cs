using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pdfProcessor.Features.DocumentProcess;

namespace pdfProcessor.Data.DbContextConfigurations;

public class DbContextDocumentProcessConfiguration : IEntityTypeConfiguration<DocumentProcessDto>
{
    public void Configure(EntityTypeBuilder<DocumentProcessDto> builder)
    {
        builder.ToTable("DocumentProcess");

        builder.HasKey(x => x.DocumentProcessId).HasName("DocumentProcessId");
        builder.HasIndex(x => x.DocumentProcessId);
        builder.Property(x => x.DocumentProcessId).HasColumnName("DocumentProcessId");

        builder.HasOne(x => x.Document)
            .WithMany(y => y.DocumentJobs)
            .HasForeignKey(x => x.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.JobStatus).HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnName("JobStatus");

        builder.Property(x => x.DocumentId).HasColumnName("DocumentId");
        builder.Property(x => x.StartedAt).HasColumnName("StartedAt").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.CompletedAt).HasColumnName("CompletedAt").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.ErrorMessage).HasColumnName("ErrorMessage");
    }
}