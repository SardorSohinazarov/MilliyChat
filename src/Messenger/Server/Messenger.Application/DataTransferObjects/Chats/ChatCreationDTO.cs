using Messenger.Domain.Enums;

namespace Messenger.Application.DataTransferObjects.Chats
{
    public record ChatCreationDTO(
        string Link,
        string? Title,
        ChatType Type
    );
}
