using System.ComponentModel.DataAnnotations;
using pdfProcessor.Features.DocumentUpload;

namespace pdfProcessor.Features.DocumentProcess;

public sealed record DocumentProcessDto
{
    [Key] public Guid DocumentProcessId { get; init; }
    public Guid DocumentId { get; init; }
    public JobStatusEnum JobStatus { get; init; }
    public DateTime StartedAt { get; init; }
    public DateTime CompletedAt { get; init; }
    public string? ErrorMessage { get; init; }
    public DocumentDto? Document { get; init; }
}