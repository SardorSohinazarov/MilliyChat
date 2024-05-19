namespace Messenger.Application.DataTransferObjects.Messages
{
    public record MessageCreationDTO(
        Guid? ParentId,
        Guid ChatId,
        string Text
    );
}
