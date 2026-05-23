namespace DiegoAlvarez.Application.DTOs.Auth;
public record LoginResponseDto(string Token, string Username, List<string> Roles);
