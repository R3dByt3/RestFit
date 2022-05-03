using RestFit.Client.Abstract.Model;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class ErrorDataDtoExampleProvider : IExamplesProvider<ErrorDataDto>
    {
        public ErrorDataDto GetExamples()
        {
            return new ErrorDataDto("An error occured")
            {
                Message = "Something went wrong",
                AdditionalInfo = "Exception was thrown by xxx"
            };
        }
    }
}
