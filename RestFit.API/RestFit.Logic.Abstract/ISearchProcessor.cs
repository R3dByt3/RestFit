using RestFit.DataAccess.Abstract;

namespace RestFit.Logic.Abstract
{
    public interface ISearchProcessor
    {
        public Task<ICollection<Unit>> GetUnits();
    }
}
