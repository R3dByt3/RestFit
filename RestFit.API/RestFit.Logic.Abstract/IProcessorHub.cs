namespace RestFit.Logic.Abstract
{
    public interface IProcessorHub
    {
        public IInsertProcessor InsertProcessor { get; }
        public IDeleteProcessor DeleteProcessor { get; }
        public ISearchProcessor SearchProcessor { get; }
        public IUpdateProcessor UpdateProcessor { get; }
    }
}
