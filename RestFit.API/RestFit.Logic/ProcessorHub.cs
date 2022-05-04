using RestFit.Logic.Abstract;

namespace RestFit.Logic
{
    public class ProcessorHub : IProcessorHub
    {
        public IInsertProcessor InsertProcessor { get; }

        public IDeleteProcessor DeleteProcessor { get; }

        public ISearchProcessor SearchProcessor { get; }

        public IUpdateProcessor UpdateProcessor { get; }

        public ProcessorHub(IInsertProcessor insertProcessor, IDeleteProcessor deleteProcessor, ISearchProcessor searchProcessor, IUpdateProcessor updateProcessor)
        {
            InsertProcessor = insertProcessor;
            DeleteProcessor = deleteProcessor;
            SearchProcessor = searchProcessor;
            UpdateProcessor = updateProcessor;
        }
    }
}
