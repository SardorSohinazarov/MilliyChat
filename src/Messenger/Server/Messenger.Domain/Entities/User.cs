using Messenger.Domain.Common;

namespace Messenger.Domain.Entities
{
    public class User : Auditable<long>
    {
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhotoPath { get; set; }
        public virtual List<Chat> Chats { get; set; }
        public virtual List<Message> Messages { get; set; }

        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }


        public void UpdateRefreshToken(
            string refreshToken,
            int expireDateInMinutes = DEFAULT_EXPIRE_DATE_IN_MINUTES)
        {
            RefreshToken = refreshToken;

            RefreshTokenExpireDate = DateTime.UtcNow
                .AddMinutes(expireDateInMinutes);
        }

        private const int DEFAULT_EXPIRE_DATE_IN_MINUTES = 1440;
    }
}
