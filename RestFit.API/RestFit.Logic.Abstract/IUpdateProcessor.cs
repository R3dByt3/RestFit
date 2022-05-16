﻿using RestFit.DataAccess.Abstract;

namespace RestFit.Logic.Abstract
{
    public interface IUpdateProcessor
    {
        public Task UpdateUserGroupedUnitAggregations(UserGroupedUnit userGroupedUnit);
        public Task SetProcessedByForUnitsAsync(UserGroupedUnit userGroupedUnit, string processorName);
    }
}
