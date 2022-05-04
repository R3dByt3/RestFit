using RestFit.DataAccess.Abstract;

namespace RestFit.Logic.Abstract
{
    public interface IInsertProcessor
    {
        public Task CreateUnitAsync(Unit unit);
    }
}
