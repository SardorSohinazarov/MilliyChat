namespace Messenger.Application.DataTransferObjects.Authentication
{
    public record RefreshTokenDTO(
        string AccessToken,
        string RefreshToken
    );
}
