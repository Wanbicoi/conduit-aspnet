using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace RealWorld.Infrastructure
{
    public class ErrorActionFilter : IActionFilter
    {
        readonly ILogger<ErrorActionFilter> _logger;
        public ErrorActionFilter(ILogger<ErrorActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            /* _logger.LogCritical("vao cái filter 1"); */
            if (!context.ModelState.IsValid)
            {
                /* _logger.LogCritical("vao cái filter 2"); */
                var result = new ContentResult();
                var errors = new Dictionary<string, string[]>();

                foreach (var valuePair in context.ModelState)
                {
                    errors.Add(valuePair.Key, valuePair.Value.Errors.Select(x => x.ErrorMessage).ToArray());
                }

                string content = JsonConvert.SerializeObject(new { errors });
                result.Content = content;
                result.ContentType = "application/json";

                context.HttpContext.Response.StatusCode = 422; //unprocessable entity;
                context.Result = result;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
