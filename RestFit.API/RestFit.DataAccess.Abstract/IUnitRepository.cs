namespace RestFit.DataAccess.Abstract
{
    public interface IUnitRepository
    {
        public void Insert(Unit unit);
        public ICollection<Unit> GetAll();
    }
}
