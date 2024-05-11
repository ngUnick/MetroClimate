using MetroClimate.Data.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MetroClimate.Data.Common
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Error occurred in " + context.ActionDescriptor.DisplayName);

            switch (context.Exception.GetType().Name)
            {
                default:
                    ApiResult body = new ApiResult<object>(new 
                        {
                            CorrelationId = context.HttpContext.TraceIdentifier
                        },
                        ErrorCode.ServerError, 
                        context.Exception.Message);
            
                    context.Result = new ObjectResult(body)
                    {
                        StatusCode = 500
                    };
                    
                    break;
            }
            
        }
    }
}