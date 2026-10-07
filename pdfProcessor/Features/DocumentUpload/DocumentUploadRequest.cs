namespace pdfProcessor.Features.DocumentUpload;

public sealed record DocumentUploadRequest(
    string FileName,
    string ContentType,
    long FileSize);