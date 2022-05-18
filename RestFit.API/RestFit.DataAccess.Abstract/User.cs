namespace RestFit.DataAccess.Abstract
{
    public record User
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string[] PendingOutFriendRequestUserIds { get; set; } = Array.Empty<string>();
        public string[] PendingInFriendRequestUserIds { get; set; } = Array.Empty<string>();
        public string[] FriendUserIds { get; set; } = Array.Empty<string>();
    }
}
