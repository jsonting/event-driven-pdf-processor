namespace pdfProcessor.Features.DocumentUpload;

public sealed record DocumentUploadResponse(string PresignedUrl, string ErrorMessage = "");