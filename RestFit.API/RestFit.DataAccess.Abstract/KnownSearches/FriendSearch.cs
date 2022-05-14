namespace RestFit.DataAccess.Abstract.KnownSearches
{
    public class FriendSearch : SearchBase<FriendFields>
    {
        public string? Id
        {
            get => GetFirst(FriendFields.Id);
            set => SetSingle(FriendFields.Id, value);
        }

        public string? FriendId
        {
            get => GetFirst(FriendFields.FriendId);
            set => SetSingle(FriendFields.FriendId, value);
        }

        public string? UserId
        {
            get => GetFirst(FriendFields.UserId);
            set => SetSingle(FriendFields.UserId, value);
        }
    }
}
