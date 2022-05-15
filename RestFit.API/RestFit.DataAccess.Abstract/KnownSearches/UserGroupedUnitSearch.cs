namespace RestFit.DataAccess.Abstract.KnownSearches
{
    public class UserGroupedUnitSearch : SearchBase<UserGroupedUnitFields>
    {
        public string? UserId
        {
            get => GetFirst(UserGroupedUnitFields.UserId);
            set => SetSingle(UserGroupedUnitFields.UserId, value);
        }
    }
}
