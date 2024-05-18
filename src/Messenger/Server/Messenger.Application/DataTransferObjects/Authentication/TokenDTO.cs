namespace Messenger.Application.DataTransferObjects.Authentication
{
    public record TokenDTO(
        string AccessToken,
        string RefreshToken,
        DateTime ExpireDate
    );
}
