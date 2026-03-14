namespace Shared.DTO_s.IdentityModule
{
    public record ForgotPasswordResponseDto(
        string Email,
        string Token,
        string ResetLink
    );
}