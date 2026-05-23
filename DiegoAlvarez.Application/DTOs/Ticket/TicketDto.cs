namespace DiegoAlvarez.Application.DTOs.Ticket;

public record TicketDto(
    Guid TicketId,
    string Title,
    string? Description,
    string Status,
    DateTime? CreatedAt,
    string Username);

public record CreateTicketDto(string Title, string? Description);
public record UpdateTicketStatusDto(string Status);
