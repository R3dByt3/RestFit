namespace RestFit.DataAccess.Abstract.KnownSearches
{
    public class UserSearch : SearchBase<UserFields>
    {
        public string? Id
        {
            get => GetFirst(UserFields.Id);
            set => SetSingle(UserFields.Id, value);
        }

        public string? Username
        {
            get => GetFirst(UserFields.Username);
            set => SetSingle(UserFields.Username, value);
        }

        public string? Password
        {
            get => GetFirst(UserFields.Password);
            set => SetSingle(UserFields.Password, value);
        }
        public string[]? Ids
        {
            get => GetAll(UserFields.Ids);
            set => SetAll(UserFields.Ids, value);
        }
    }
}
