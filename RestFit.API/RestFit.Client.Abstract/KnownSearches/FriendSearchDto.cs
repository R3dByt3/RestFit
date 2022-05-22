namespace RestFit.Client.Abstract.KnownSearches
{
    public class FriendSearchDto : SearchBaseDto<FriendDtoFields>
    {
        public string[] Ids
        {
            get => GetAll(FriendDtoFields.Ids);
            set => SetAll(FriendDtoFields.Ids, value);
        }
    }
}
