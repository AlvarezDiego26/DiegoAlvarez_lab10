namespace DiegoAlvarez.Application.DTOs.Response;

public record ResponseDto(Guid ResponseId, string Message, DateTime? CreatedAt, string Responder);
public record CreateResponseDto(string Message);
