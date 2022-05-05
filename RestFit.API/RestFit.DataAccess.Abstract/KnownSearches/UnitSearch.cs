namespace RestFit.DataAccess.Abstract.KnownSearches
{
    public class UnitSearch : SearchBase<UnitFields>
    {
        public string? Id 
        {
            get => GetFirst(UnitFields.Id);
            set => SetSingle(UnitFields.Id, value);
        }

        public string? UserId
        {
            get => GetFirst(UnitFields.UserId);
            set => SetSingle(UnitFields.UserId, value);
        }

        public string? Type
        {
            get => GetFirst(UnitFields.Type);
            set => SetSingle(UnitFields.Type, value);
        }
    }
}
