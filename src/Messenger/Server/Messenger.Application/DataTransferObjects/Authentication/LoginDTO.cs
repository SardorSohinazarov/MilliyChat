namespace Messenger.Application.DataTransferObjects.Authentication
{
    public record LoginDTO(
        string PhoneNumber,
        string Password
    );
}
