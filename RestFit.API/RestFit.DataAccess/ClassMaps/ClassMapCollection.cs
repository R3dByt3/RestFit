namespace RestFit.DataAccess.ClassMaps
{
    public static class ClassMapCollection
    {
        public static void Init()
        {
            UnitClassMap.Init();
            UserClassMap.Init();
            UserGroupedUnitClassMap.Init();
            HealthUnitClassMap.Init();
        }
    }
}
