using RestFit.API.Models;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class ErrorDataExampleProvider : IExamplesProvider<ErrorData>
    {
        public ErrorData GetExamples()
        {
            return new ErrorData("An error occured")
            {
                Message = "Something went wrong",
                AdditionalInfo = "Exception was thrown by xxx"
            };
        }
    }
}
