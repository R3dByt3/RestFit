namespace RestFit.Client.Abstract.KnownSearches
{
    public class UserSearchDto : SearchBaseDto<UserDtoFields>
    {
        public string[]? Ids
        {
            get => GetAll(UserDtoFields.Ids);
            set => SetAll(UserDtoFields.Ids, value);
        }
    }
}
