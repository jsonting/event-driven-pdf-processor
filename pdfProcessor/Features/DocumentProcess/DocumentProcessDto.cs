using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pdfProcessor.Features.DocumentUpload;

namespace pdfProcessor.Features.DocumentProcess;

public sealed record DocumentProcessDto
{
    [Key] public Guid DocumentProcessId { get; init; }
    [ForeignKey(nameof(DocumentId))] public Guid DocumentId { get; init; }
    public JobStatusEnum JobStatus { get; init; }
    public DateTime StartedAt { get; init; }
    public DateTime CompletedAt { get; init; }
    public string? ErrorMessage { get; init; }
    public DocumentDto? Document { get; init; }
}