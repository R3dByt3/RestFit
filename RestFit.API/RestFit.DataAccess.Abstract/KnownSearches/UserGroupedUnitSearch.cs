namespace RestFit.DataAccess.Abstract.KnownSearches
{
    public class UserGroupedUnitSearch : SearchBase<UserGroupedUnitFields>
    {
        public string[] UserIds
        {
            get => GetAll(UserGroupedUnitFields.UserIds);
            set => SetAll(UserGroupedUnitFields.UserIds, value);
        }

        public string? UserId
        {
            get => GetFirst(UserGroupedUnitFields.UserId);
            set => SetSingle(UserGroupedUnitFields.UserId, value);
        }

        public long? SetsSum
        {
            get => GetFirstInt64(UserGroupedUnitFields.SetsSum);
            set => SetSingle(UserGroupedUnitFields.SetsSum, value);
        }

        public long? RepetitionsSum
        {
            get => GetFirstInt64(UserGroupedUnitFields.RepetitionsSum);
            set => SetSingle(UserGroupedUnitFields.RepetitionsSum, value);
        }

        public double? WeightsSum
        {
            get => GetFirstDouble(UserGroupedUnitFields.WeightsSum);
            set => SetSingle(UserGroupedUnitFields.WeightsSum, value);
        }

        public long? DocumentCount
        {
            get => GetFirstInt64(UserGroupedUnitFields.DocumentCount);
            set => SetSingle(UserGroupedUnitFields.DocumentCount, value);
        }
    }
}
