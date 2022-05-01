using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RestFit.API.Models;
using RestFit.DataAccess.Abstract.Exceptions;
using System.Net;

namespace RestFit.API.Controllers
{
    public abstract class ControllerWithExceptionHandling : ControllerBase
    {
        private readonly ILogger _logger;

        protected ControllerWithExceptionHandling(ILogger logger)
        {
            _logger = logger;
            _logger.LogInformation("Hello there");
        }

        protected async Task<IActionResult> ExecuteSafeAsync(Func<Task<IActionResult>> act)
        {
            try
            {
                return await act().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"REST request ({Request?.Method ?? string.Empty} {Request?.Path ?? string.Empty} {Request?.QueryString.ToString() ?? string.Empty}) failed with message '{ex.GetType().FullName}:{ex.Message}'");
                return await HandleExceptionAsync(ex);
            }
        }

        protected abstract Task<IActionResult> HandleExceptionAsync(Exception ex);

        protected IActionResult DefaultErrorResponse(Exception ex) => StatusCode((int)HttpStatusCode.InternalServerError, GenerateErrorDataFromException(ex));

        protected static ErrorData GenerateErrorDataFromException(Exception ex) => new(ex.Message)
        {
            AdditionalInfo = ex.ToString()
        };

        protected IActionResult DataAccessMongoDbException(DataAccessMongoDbException ex) => ex.InnerException switch
        {
            MongoWriteException mwe when mwe.WriteError.Category == ServerErrorCategory.DuplicateKey => Conflict(new ErrorData($"Action could not be performed due unique index violation. See {nameof(ErrorData.AdditionalInfo)} for more details") { AdditionalInfo = mwe.WriteError.Message }),
            _ => StatusCode((int)HttpStatusCode.GatewayTimeout, GenerateErrorDataFromException(ex))
        };
    }
}
