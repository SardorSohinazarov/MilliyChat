namespace Messenger.Application.DataTransferObjects.Authentication
{
    public record RegisterDTO(
        string FirstName,
        string PhoneNumber,
        string Password,
        string? LastName
    );
}
