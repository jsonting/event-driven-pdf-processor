using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pdfProcessor.Features.DocumentProcess;

namespace pdfProcessor.Features.DocumentUpload;

[Table("Documents", Schema = "dbo")]
public sealed record DocumentDto
{
    [Key] public Guid DocumentId { get; init; }
    public string? FileName { get; init; }
    public long? FileSize { get; init; }
    public string? MimeType { get; init; }
    public DateTime UploadedAt { get; init; }

    private readonly List<DocumentProcessDto> _documentJobs = [];
    public IReadOnlyList<DocumentProcessDto> DocumentJobs => _documentJobs.AsReadOnly();
}